using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using Utility;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MBService
{
    public partial class handler : System.Web.UI.Page
    {
        static ITransaction.AutoTrans autoTrans;

        protected void Page_Load(object sender, EventArgs e)
        {
            string idRequest = Guid.NewGuid().ToString();
            string Output = "";
            string ClientIP = "";
            bool EncryptRequest = true;
            string clientEncryptKey = "";
            //string clientInfo = Request?.Headers?.FirstOrDefault(s => s.Key.ToLower() == "user-agent").Value;
            try
            {
                string clientInfo = Request.UserAgent;
                Utility.ProcessLog.LogInformation($"[{idRequest}] Client Info: {clientInfo}");
            }
            catch { }

            Dictionary<string, string> dicResponse = new Dictionary<string, string>();

            Hashtable InputData = new Hashtable();
            Dictionary<string, string> dicInput = new Dictionary<string, string>();

            try
            {
                #region Init
                ClientIP = Request.ServerVariables["remote_addr"];

                if (autoTrans == null)
                {
                    autoTrans = (ITransaction.AutoTrans)Activator.GetObject(
                        typeof(ITransaction.AutoTrans),
                        "tcp://" + ConfigurationManager.AppSettings["RemotingAddress"].ToString() + ":" +
                        ConfigurationManager.AppSettings["RemotingPort"].ToString() + "/IPCAutoTrans");
                }

                try
                {
                    if (ConfigurationManager.AppSettings["EncryptRequest"] != null)
                    {
                        bool.TryParse(ConfigurationManager.AppSettings["EncryptRequest"], out EncryptRequest);
                    }
                }
                catch { }
                #endregion

                #region Parse request
                Dictionary<string, string> hsOgRequest = new Dictionary<string, string>();
                string enRequest = string.Empty;
                if (!EncryptRequest)
                {
                    if (Request.Form.AllKeys.Contains("d"))
                    {
                        enRequest = Request.Form["d"];
                    }
                    else
                    {
                        //enRequest = new StreamReader(Request.InputStream).ReadToEnd();
                        enRequest = GetBodyRequest(Request.InputStream);
                    }
                    Utility.ProcessLog.LogInformation($"[{idRequest}] Body Request after decrypted: " + ClientIP + "===>" + enRequest);
                    hsOgRequest = JsonConvert.DeserializeObject<Dictionary<string, string>>(enRequest);
                }
                else
                {
                    try
                    {
                        if (Request.Form.AllKeys.Contains("d"))
                        {
                            enRequest = Request.Form["d"];
                            Utility.ProcessLog.LogInformation($"[{idRequest}] Body Request from D: {enRequest}");
                        }
                        else
                        {
                            //enRequest = new StreamReader(Request.InputStream).ReadToEnd();
                            enRequest = GetBodyRequest(Request.InputStream);
                            Utility.ProcessLog.LogInformation($"[{idRequest}] Body Request InputStream: {enRequest}");
                        }
                        string[] DeData = Common.DecryptMsgMB(enRequest);
                        clientEncryptKey = DeData[1];
                        Utility.ProcessLog.LogInformation($"[{idRequest}] Body Request after decrypted: " + ClientIP + "===>" + DeData[0]);
                        hsOgRequest = JsonConvert.DeserializeObject<Dictionary<string, string>>(DeData[0]);
                    }
                    catch (Exception ex)
                    {
                        Utility.ProcessLog.LogError(new Exception($"[{idRequest}] Request {Request.Form["d"]} have error {ex.ToString()}"),
                        System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                        System.Reflection.MethodBase.GetCurrentMethod().Name);
                        return;
                    }
                }

                foreach (var entry in hsOgRequest.Where(entry => entry.Key != null && !string.IsNullOrEmpty(entry.Key)))
                {
                    if (!entry.Key.Equals("SIGNATURE"))
                    {
                        Common.HashTableAddOrSet(InputData, entry.Key, entry.Value);
                    }

                    if (entry.Key == "PASSWORD" || entry.Key == "NEWPASS" || entry.Key == "PINCODE" || entry.Key == "AUTHENCODE")
                    {
                        Common.HashTableAddOrSet(InputData, entry.Key, string.IsNullOrEmpty(entry.Value) || entry.Key.Equals("AUTHENCODE") ? entry.Value : Common.EncryptPassword(entry.Value));
                    }
                    else
                    {
                        Common.HashTableAddOrSet(InputData, entry.Key, Common.KillSqlInjection(entry.Value));
                    }
                }
                #endregion

                #region basic valid
                //check source id
                if (!(InputData[Common.KEYNAME.SOURCEID].ToString().Equals(Common.KEYNAME.MB) ||
                    InputData[Common.KEYNAME.SOURCEID].ToString().Equals("SMS") ||
                    InputData[Common.KEYNAME.SOURCEID].ToString().Equals("AM")))
                {
                    Utility.ProcessLog.LogError(new Exception($"Invalid sourceid {InputData[Common.KEYNAME.SOURCEID].ToString()}"),
                        System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                        System.Reflection.MethodBase.GetCurrentMethod().Name);
                    return;
                }

                //check signature
                bool checkSignature = false;
                bool.TryParse(ConfigurationManager.AppSettings["CheckSignature"], out checkSignature);
                if (checkSignature)
                {
                    string jsonOut = JsonConvert.SerializeObject(dicInput);
                    string hash = Utility.Common.CreateHash(jsonOut, RSAConfig.strSigKey);
                    if (!InputData["SIGNATURE"].ToString().Equals(hash))
                    {
                        Utility.ProcessLog.LogError(new Exception("Signature is invalid"),
                            System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                            System.Reflection.MethodBase.GetCurrentMethod().Name);
                        return;
                    }
                }

                //check date time
                int checkDateRequest = 0;
                int.TryParse(ConfigurationManager.AppSettings["CheckRequestTime"], out checkDateRequest);
                if (checkDateRequest > 0)
                {
                    long cdt = InputData.ContainsKey("RDT") ? long.Parse(InputData["RDT"].ToString()) : 0;
                    DateTime cDateTime = new DateTime(cdt);

                    if (Math.Abs((DateTime.UtcNow - cDateTime).TotalMinutes) > checkDateRequest)
                    {
                        Utility.ProcessLog.LogError(new Exception($"In correct request time {cDateTime} {DateTime.UtcNow}"),
                            System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                            System.Reflection.MethodBase.GetCurrentMethod().Name);
                        return;
                    }
                }
                //check version
                if (ConfigurationManager.AppSettings["MinimumClientLevelMB"] != null  || ConfigurationManager.AppSettings["MinimumClientLevelAM"] != null)
                {
                    Version currentVersion = new Version(InputData["APPVERSION"].ToString().Trim());
                    string SERVICEID = InputData["SERVICEID"].ToString().Trim();
                    Version minimumClientVersionMB = new Version(ConfigurationManager.AppSettings["MinimumClientLevelMB"]);
                    Version minimumClientVersionAM = new Version(ConfigurationManager.AppSettings["MinimumClientLevelAM"]);               
                    if (currentVersion < minimumClientVersionMB && SERVICEID == "MB")
                    {
                        dicResponse.Add(Common.KEYNAME.IPCERRORCODE, "6666");
                        dicResponse.Add(Common.KEYNAME.IPCERRORDESC, ConfigurationManager.AppSettings["UpgradeNotice"]);
                        dicResponse.Add(Common.KEYNAME.ERRORCODE, "6666");
                        dicResponse.Add(Common.KEYNAME.ERRORDESC, ConfigurationManager.AppSettings["UpgradeNotice"]);

                        dicResponse.Add("LinkUpgradeConsumerAndroid", ConfigurationManager.AppSettings["LinkUpgradeConsumerAndroid"]);
                        dicResponse.Add("LinkUpgradeConsumerIOS", ConfigurationManager.AppSettings["LinkUpgradeConsumerIOS"]);
                        dicResponse.Add("LinkUpgradeAgentAndroid", ConfigurationManager.AppSettings["LinkUpgradeAgentAndroid"]);
                        dicResponse.Add("LinkUpgradeAgentIOS", ConfigurationManager.AppSettings["LinkUpgradeAgentIOS"]);

                        Output = JsonConvert.SerializeObject(dicResponse);
                        Response.Write(Common.EncryptMsgMB(Output, clientEncryptKey));
                        return;
                    }
                    if (currentVersion < minimumClientVersionAM && SERVICEID == "AM")
                    {
                        dicResponse.Add(Common.KEYNAME.IPCERRORCODE, "6666");
                        dicResponse.Add(Common.KEYNAME.IPCERRORDESC, ConfigurationManager.AppSettings["UpgradeNotice"]);
                        dicResponse.Add(Common.KEYNAME.ERRORCODE, "6666");
                        dicResponse.Add(Common.KEYNAME.ERRORDESC, ConfigurationManager.AppSettings["UpgradeNotice"]);

                        dicResponse.Add("LinkUpgradeConsumerAndroid", ConfigurationManager.AppSettings["LinkUpgradeConsumerAndroid"]);
                        dicResponse.Add("LinkUpgradeConsumerIOS", ConfigurationManager.AppSettings["LinkUpgradeConsumerIOS"]);
                        dicResponse.Add("LinkUpgradeAgentAndroid", ConfigurationManager.AppSettings["LinkUpgradeAgentAndroid"]);
                        dicResponse.Add("LinkUpgradeAgentIOS", ConfigurationManager.AppSettings["LinkUpgradeAgentIOS"]);

                        Output = JsonConvert.SerializeObject(dicResponse);
                        Response.Write(Common.EncryptMsgMB(Output, clientEncryptKey));
                        return;
                    }
                }
                #endregion

                #region encode biometric token
                if (InputData.ContainsKey(Common.KEYNAME.BIOMETRICTOKEN) && !string.IsNullOrEmpty(InputData[Common.KEYNAME.BIOMETRICTOKEN].ToString()))
                {
                    string userID = InputData[Common.KEYNAME.USERID].ToString();
                    if (string.IsNullOrEmpty(userID) || userID.Equals("guest"))
                    {
                        //can sua lai lay tu database ra
                        userID = InputData[Common.KEYNAME.PHONENO].ToString();
                    }
                    InputData[Common.KEYNAME.BIOMETRICTOKEN] = Common.EncodeBioMetricToken(userID, InputData[Common.KEYNAME.DEVICEID].ToString(), InputData[Common.KEYNAME.BIOMETRICTOKEN].ToString());
                }
                #endregion

                #region encode authen information

                if (InputData.ContainsKey(Common.KEYNAME.AUTHENTYPE) && InputData.ContainsKey(Common.KEYNAME.USERID) && InputData.ContainsKey(Common.KEYNAME.AUTHENCODE))
                {
                    if (InputData[Common.KEYNAME.AUTHENTYPE].Equals(Common.KEYNAME.PINCODE) || InputData[Common.KEYNAME.AUTHENTYPE].Equals(Common.KEYNAME.PASSWORD))
                        InputData[Common.KEYNAME.AUTHENCODE] = Utility.O9Encryptpass.sha_sha256(InputData[Common.KEYNAME.AUTHENCODE].ToString(), InputData[Common.KEYNAME.USERID].ToString());
                    else
                        InputData[Common.KEYNAME.AUTHENCODE] = Utility.Common.KillSqlInjection(InputData[Common.KEYNAME.AUTHENCODE].ToString());
                }
                else if (InputData.ContainsKey(Common.KEYNAME.AUTHENCODE))
                {
                    InputData[Common.KEYNAME.AUTHENCODE] = Utility.Common.KillSqlInjection(InputData[Common.KEYNAME.AUTHENCODE].ToString());
                }
                #endregion

                #region log
                try
                {
                    bool EnableFullLog = false;
                    try
                    {
                        EnableFullLog = bool.Parse(ConfigurationManager.AppSettings["DebugLog"].ToString());
                    }
                    catch { }
                    if (EnableFullLog)
                    {
                        if (EncryptRequest)
                        {
                            Utility.ProcessLog.LogInformation(ClientIP + "===>" + enRequest);
                        }
                        else
                        {
                            string keys = string.Join("#", InputData.Keys.Cast<object>()
                                         .Select(x => x.ToString() + "=" + InputData[x.ToString()].ToString())
                                         .ToArray());

                            Utility.ProcessLog.LogInformation(ClientIP + "===>" + keys);
                        }
                    }
                }
                catch { }
                #endregion

                if (InputData.Count == 0) return;

                #region service offline
                if (!(InputData["IPCTRANCODE"].ToString().Equals("MB000100") || InputData["IPCTRANCODE"].ToString().Equals("MB000033")))
                {
                    if (ConfigurationManager.AppSettings["ServiceTurnOff"].ToString().Equals("Y"))
                    {
                        string toDesc = "System suspend at the moment, please try again later";
                        string toCode = Common.ERRORCODE.SYSTEM;
                        try
                        {
                            toCode = ConfigurationManager.AppSettings["ServiceTurnOffCode"].ToString();
                            toDesc = ConfigurationManager.AppSettings["ServiceTurnOffDesc"].ToString();
                        }
                        catch { }

                        dicResponse.Add(Common.KEYNAME.IPCERRORCODE, toCode);
                        dicResponse.Add(Common.KEYNAME.IPCERRORDESC, toDesc);
                        dicResponse.Add(Common.KEYNAME.ERRORCODE, toCode);
                        dicResponse.Add(Common.KEYNAME.ERRORDESC, toDesc);

                        Output = JsonConvert.SerializeObject(dicResponse);
                        Utility.ProcessLog.LogInformation($"[{idRequest}] Body response before encrypt: {Output}");
                        //Common.EncryptMsgMB(Output, clientEncryptKey);

                        string responsebody = Common.EncryptMsgMB(Output, clientEncryptKey);
                        Utility.ProcessLog.LogInformation($"[{idRequest}] Body response after encrypted: {responsebody}");

                        Response.Write(responsebody);
                        return;
                    }
                }
                #endregion

                #region map userid for login method
                if (InputData["USERID"].Equals("guest") && InputData.ContainsKey("PHONENO"))
                {
                    InputData["USERID"] = InputData["PHONENO"].ToString().ToUpper();
                }

                //for old version
                if (InputData.ContainsKey("IPCTRANCODE") && InputData["IPCTRANCODE"].ToString().Equals("MB000001") && !InputData.ContainsKey("USERID"))
                {
                    InputData["USERID"] = InputData["PHONENO"].ToString().ToUpper();
                }
                #endregion

                #region DoTran

                Hashtable OutputData = autoTrans.ProcessTransHAS(InputData);

                foreach (string key in OutputData.Keys)
                {
                    if (OutputData[key] != null)
                    {
                        dicResponse.Add(key, OutputData[key].ToString());
                    }
                }

                Output = JsonConvert.SerializeObject(dicResponse);
                Utility.ProcessLog.LogInformation($"[{idRequest}] Body response before encrypt: {Output}");
                #endregion

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);

                dicResponse.Add(Common.KEYNAME.IPCERRORCODE, Common.ERRORCODE.SYSTEM);
                dicResponse.Add(Common.KEYNAME.IPCERRORDESC, "Transaction error!");
                dicResponse.Add(Common.KEYNAME.ERRORCODE, Common.ERRORCODE.SYSTEM);
                dicResponse.Add(Common.KEYNAME.ERRORDESC, "Transaction error!");

                Output = JsonConvert.SerializeObject(dicResponse);
                Utility.ProcessLog.LogInformation($"[{idRequest}] Body response before encrypt: {Output}");
            }

            Response.ContentType = "text/plain";

            if (EncryptRequest)
            {
                string responsebody = Common.EncryptMsgMB(Output, clientEncryptKey);
                Utility.ProcessLog.LogInformation($"[{idRequest}] Body response after encrypted: {responsebody}");
                Response.Write(responsebody);
            }
            else
            {
                Response.Write(Output);
            }
        }

        public string GetBodyRequest(Stream BodyRequest)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                BodyRequest.Seek(0, SeekOrigin.Begin);
                BodyRequest.CopyTo(stream);

                string requestBody = Encoding.UTF8.GetString(stream.ToArray());

                BodyRequest.Seek(0, SeekOrigin.Begin);
                return requestBody;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return string.Empty;
            }
        }
    }
}
