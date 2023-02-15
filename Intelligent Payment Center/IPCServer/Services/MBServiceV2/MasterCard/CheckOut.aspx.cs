using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace MBService.MasterCard
{
    public partial class CheckOut : System.Web.UI.Page
    {
        static ITransaction.AutoTrans autoTrans;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["SESSION"] == null && Session["INVOINO"] == null)
            {
                lbcontentamount.Visible = true;
                lbcontentccyid.Visible = true;
                string idRequest = Guid.NewGuid().ToString();
                string Output = "";
                string ClientIP = "";
                bool EncryptRequest = true;
                string clientEncryptKey = "";
                Dictionary<string, string> dicResponse = new Dictionary<string, string>();
                try
                {
                    string clientInfo = Request.UserAgent;
                    Utility.ProcessLog.LogInformation($"[{idRequest}] Client Info: {clientInfo}");
                }
                catch { }

                Hashtable InputData = new Hashtable();
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
                    if (Request.QueryString.AllKeys.Contains("d"))
                    {
                        enRequest = Request.QueryString["d"];
                    }
                    else if (Request.Form.AllKeys.Contains("d"))
                    {
                        enRequest = Request.Form["d"];
                    }
                    else
                    {
                        enRequest = GetBodyRequest(Request.InputStream);
                    }

                    Utility.ProcessLog.LogInformation($"[{idRequest}] Body Request after decrypted: " + ClientIP + "===>" + enRequest);
                    hsOgRequest = JsonConvert.DeserializeObject<Dictionary<string, string>>(enRequest);
                }
                else
                {
                    try
                    {
                        if (Request.QueryString.AllKeys.Contains("d"))
                        {
                            enRequest = Request.QueryString["d"];
                            enRequest = UrlDecode(enRequest);
                            Utility.ProcessLog.LogInformation($"[{idRequest}] Body Request from D: {enRequest}");
                        }
                        else if (Request.Form.AllKeys.Contains("d"))
                        {
                            enRequest = Request.Form["d"];
                            Utility.ProcessLog.LogInformation($"[{idRequest}] Body Request from D: {enRequest}");
                        }
                        else
                        {
                            enRequest = GetBodyRequest(Request.InputStream);
                            Utility.ProcessLog.LogInformation($"[{idRequest}] Body Request InputStream: {enRequest}");
                        }

                        if(enRequest == "")
                        {
                            hdTransID.Value = Session["TRANSID"].ToString();
                            hduserid.Value = Session["USERID"].ToString();
                            hdsession.Value = Session["SESSION"].ToString();
                            Session["TRANSID"] = null;
                            Session["USERID"] = null;
                            Session["SESSION"] = null;
                            return;
                        }
                        //enRequest = "gAAAAHqQhqrTzynS7Wu0kL2EV8+XFaKtO9QinccfuAkV86F95zVwmQT2IF6gjcyb66RkvAcIJzC3rJVais5E+uXtEwfQ2gB5YyxulcT1a2V417o34LRuYRNcBGcPLUAnL0xwR2tMyXjfEWpeCMbf6S7AOtRMgpl2Kt23uIS9s/e4DfGaYp7e2vbV9kyxwCDyT2OrQmknlpnYbPqaF4be8aCifFHiPX7NhP5NnuY+jMmCxVBQFJEm2QZmu1KNyuJesWY1YEbwdT8A16krG92IhIaUy4UyCLg4iAY59RBuuTseMtFJbJwtO+XlahFXOpYwqi0258CbnQYYaDz+Zq+S6BBgRdu8hiV557vXB7kJBUF/0esAkrODk7tc6+EYZzmCAxG6UheXyYjBNZTUdmGa6kBa0eRiz6W8+7GE2ht1UkMsXoyO31nxQZWCWcPSaRp1Ogj5L/FjT0XK+h/PVaozNI11QZhI0WLgc0qhTXyzCh/NOSUTkUaGTILKAVGB5BqFFIcMks79W0Iw8kLtbbRi6EXB8DUru4E5H3vcQKNY5ufAyWfdQmnHL6FJbf1TN2YBeOEZYC97RZ5yBGU0YbPSScman9plfwzjyR8K0/ZrRhmkswMpyPaj1hBvtdIXHERmb5QecfY0Po9AD8RsCtYUT7yz3P4ot3Y3MRJ/Twc/YWCpD9Ysgewv1y/fe9/uQvQZi8xD7W/FI9oRuRp2Zf5F/U0cCUchhV0EFSlXBWrmLcuKC6Ebe5FQkiXhhQr283/j4rGPC9masjFAJ7N2uPMz4f/5vhL7QIFAxMmwOpYMy6ePXymsM8QR0A4m1khdBGxxGzqNr/m+yDjS8ZWDq/g59ZEmODhnfKb5LCi0uOAIrCW1TAjxMfG2X4jomGnrJ5XI1laKqe1uJFj7VfK/x8wau/DWO+OqqJMjUFtDHLZ+ejsbPN9LKB0+SAnlw/6UPke9mPjr7J7P2VLong4J7hh2g/LjkUeveQci7k4TkVrJPBR/sE6POrjdCAoh4bnMoVpqJFo5fkRaonmwBE+/ljQb8gwipyVNTZiv8O9+JfB6MypY5Wp8kmBW+R1G/jRSKIgQI1/lLEdmC3yEvm8mxEaxleYuRVWmkw9L49ANUQvWc4r6qA5yR6zTfIU/mC/o5uHTIqsatUcaOrV/pSMMnBeYrNirI6w1jpRnVCz5htzupO5+XmRNun9cabr2QBa6GPZcjTeMzQ==";


                        string[] DeData = Common.DecryptMsgMB(enRequest);
                        Utility.ProcessLog.LogInformation($"[{idRequest}] Body Request after decrypted: " + ClientIP + "===>" + DeData[0]);
                        hsOgRequest = JsonConvert.DeserializeObject<Dictionary<string, string>>(DeData[0]);
                    }
                    catch (Exception ex)
                    {
                        Utility.ProcessLog.LogError(new Exception($"[{idRequest}] Request {Request.Form["d"]} have error {ex.ToString()}"),
                            System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                            System.Reflection.MethodBase.GetCurrentMethod().Name);
                        Response.Redirect("~/500.aspx");
                        return;
                    }
                }

                foreach (var entry in hsOgRequest.Where(entry => entry.Key != null && !string.IsNullOrEmpty(entry.Key.ToString())))
                {
                    Common.HashTableAddOrSet(InputData, entry.Key, Common.KillSqlInjection(entry.Value));
                }
                #endregion

                    #region basic valid
                //check source id
                if (!(InputData[Common.KEYNAME.SOURCEID].ToString().Equals(Common.KEYNAME.MB) || InputData[Common.KEYNAME.SOURCEID].ToString().Equals("AM")))
                {
                    Utility.ProcessLog.LogError(new Exception($"Invalid sourceid {InputData[Common.KEYNAME.SOURCEID].ToString()}"),
                        System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                        System.Reflection.MethodBase.GetCurrentMethod().Name);
                    Response.Redirect("~/500.aspx");
                    return;
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


                    #region DoTran

                try
                {
                    if (autoTrans == null)
                    {
                        autoTrans = (ITransaction.AutoTrans)Activator.GetObject(
                            typeof(ITransaction.AutoTrans),
                            "tcp://" + ConfigurationManager.AppSettings["RemotingAddress"].ToString() + ":" +
                            ConfigurationManager.AppSettings["RemotingPort"].ToString() + "/IPCAutoTrans");
                    }


                    Common.HashTableAddOrSet(InputData, "IPCTRANCODE", "PAYMENT_GETSESSION");
                    Common.HashTableAddOrSet(InputData, "API", "CREATE_CHECKOUT_SESSION");


                    Hashtable OutputTran = autoTrans.ProcessTransHAS(InputData);

                    if (OutputTran["IPCERRORCODE"].ToString() == "0")
                    {
                        hdAmount.Value = InputData["AMOUNT"].ToString();
                        hdCcyid.Value = OutputTran["CCYID"].ToString();
                        hdTransID.Value = InputData["INVOINO"].ToString(); 
                        hdsession.Value = OutputTran["SESSION"].ToString();
                        hdmerchant.Value = OutputTran["MERCHANT"].ToString();
                        hduserid.Value = InputData["USERID"].ToString();
                        hdTranCode.Value = InputData["TRANCODE"].ToString();
                        hdSourceID.Value = InputData["SOURCEID"].ToString();
                        lbAmount.Text = hdAmount.Value;
                        lbccyid.Text = hdCcyid.Value;
                        Session["TRANSID"] = null;
                        Session["USERID"] = null;
                        Session["SESSION"] = null;
                        Session["TRANCODE"] = null;
                        Session["SOURCEID"] = null;

                        Session["TRANSID"] = InputData["INVOINO"].ToString();
                        Session["USERID"] = InputData["USERID"].ToString();
                        Session["SESSION"] = OutputTran["SESSION"].ToString(); ;
                        Session["TRANCODE"] = InputData["TRANCODE"].ToString();
                        Session["SOURCEID"] = InputData["SOURCEID"].ToString();
                    }
                    else
                    {
                        Response.Redirect("~/500.aspx");
                        return;
                    }

                }
                catch (Exception ex)
                {

                    Utility.ProcessLog.LogInformation($"[{idRequest}] Exception: {ex.Message}");
                    Response.Redirect("~/500.aspx");
                    return;
                }
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

            }
            else
            {
                lbcontentamount.Visible = false;
                lbcontentccyid.Visible = false;
                hdTransID.Value = Session["TRANSID"].ToString();
                hdsession.Value = Session["SESSION"].ToString();
                hduserid.Value = Session["USERID"].ToString();
                hdTranCode.Value = Session["TRANCODE"].ToString();
                hdSourceID.Value = Session["SOURCEID"].ToString();
                Session["TRANSID"] = null;
                Session["USERID"] = null;
                Session["SESSION"] = null;
                Session["TRANCODE"] = null;
                Session["SOURCEID"] = null;
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

        public string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            byte[] bytesToEncode = Encoding.UTF8.GetBytes(str);
            String returnVal = System.Convert.ToBase64String(bytesToEncode);

            return returnVal.TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        public string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            str.Replace('-', '+');
            str.Replace('_', '/');

            int paddings = str.Length % 4;
            if (paddings > 0)
            {
                str += new string('=', 4 - paddings);
            }

            byte[] encodedDataAsBytes = System.Convert.FromBase64String(str);
            string returnVal = Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnVal;
        }

    }
}