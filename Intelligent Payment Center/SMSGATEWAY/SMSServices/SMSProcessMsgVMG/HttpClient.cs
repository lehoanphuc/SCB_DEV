using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Utility;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SMSProcessMsgMT
{
    public class HttpClient //vutran19062015 YCDC WS
    {
        #region private client
        private List<string> GetURLParams(string url)
        {
            List<string> param = new List<string>();
            string[] kk = url.Split('{');
            foreach (string k in kk)
            {
                if (k.Contains("}"))
                {
                    param.Add(k.Split('}')[0].Trim());
                }
            }
            return param;
        }
        private void FormatURL(TransactionInfo tran, ref string url)
        {
            try
            {
                if (url.Contains("{"))
                {
                    List<string> param = GetURLParams(url);
                    foreach (string p in param)
                    {
                        string value = (tran.Data.ContainsKey(p)) ? tran.Data[p].ToString() : "";
                        url = url.Replace("{" + p + "}", value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void BypassCertificateError()
        {
            ServicePointManager.ServerCertificateValidationCallback +=

                delegate (
                    Object sender1,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        }
        #endregion
        #region public client
        //Post request with private cert
        public bool HttpPostCVRSMessage(DataTable dtOUTPUTHTTP, DataTable dtOUTPUTHTTPHEADER, DataTable dtCONNECTIONWS, DataRow rowContent)
        {
            //id for Log file
            string InputString = string.Empty;
            string OutputData = string.Empty;
            try
            {
                string _Url = string.Empty;
                string _Action = string.Empty;
                string _ContentType = string.Empty;

                _Url = dtCONNECTIONWS.Rows[0]["URLWEBSERVICE"].ToString().Trim();
                _Action = dtCONNECTIONWS.Rows[0]["SOAPACTION"].ToString().Trim();
                _ContentType = dtCONNECTIONWS.Rows[0]["CONTENTTYPE"].ToString().Trim();

                BypassCertificateError();

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_Url);
                request.Method = _Action;
                request.ContentType = _ContentType;
                //header
                JObject jo = new JObject();
                JObject jHeader = new JObject();
                OutputData = CreateMessageHTTP(rowContent, dtOUTPUTHTTPHEADER.Rows, dtOUTPUTHTTP.Rows);
                try
                {
                    ProcessLog.LogInformation($"Transaction {rowContent[Common.KEYNAME.IPCTRANSID].ToString()} Request HTTP: {Environment.NewLine}{OutputData}", Common.FILELOGTYPE.LOGMSGINOUT);
                }
                catch { }
                try
                {
                    jo = Common.NewParse(OutputData);
                    jHeader = Common.NewParse(jo.SelectToken(Common.KEYNAME.HTTPHEADER).ToString());
                    List<string> keys = jHeader.Properties().Select(p => p.Name).ToList();
                    foreach (string k in keys)
                    {
                        request.Headers.Add(k, jHeader.SelectToken(k).ToString());
                    }
                }
                catch { }


                string postData = string.Empty;
                if (_Action.Equals(Common.KEYNAME.POST))
                {
                    JObject jBody = jo.SelectToken(Common.KEYNAME.HTTPBODY).Value<JObject>();
                    try
                    {
                        jBody["requestId"] = rowContent[Common.KEYNAME.IPCTRANSID].ToString();
                    }
                    catch { }
                    postData = jo.SelectToken(Common.KEYNAME.HTTPBODY).ToString();
                    
                    ProcessLog.LogInformation(string.Format("Transaction {0} sending message to url: {1} and message content is {2}", rowContent[Common.KEYNAME.IPCTRANSID].ToString(), _Url, postData.ToString()), Common.FILELOGTYPE.LOGMSGINOUT);
                    byte[] bytes = Encoding.UTF8.GetBytes(postData);
                    request.ContentLength = bytes.Length;

                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Dispose();
                }


                #region Add cert to request
                string certpath = string.Empty;
                string certpass = string.Empty;
                try
                {
                    certpath = dtCONNECTIONWS.Rows[0]["CERTPATH"].ToString();
                    certpass = dtCONNECTIONWS.Rows[0]["CERTPASS"].ToString();
                }
                catch
                {
                    certpath = string.Empty;
                    certpass = string.Empty;
                }

                string httpresponse = string.Empty;

                if (!string.IsNullOrEmpty(certpath) && !string.IsNullOrEmpty(certpass))
                {
                    //Decrypt Pass
                    certpass = Common.DecryptData(certpass);
                    //Check Path exist
                    if (!File.Exists(certpath))
                    {
                        certpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, certpath);
                        if (!File.Exists(certpath))
                        {
                            ProcessLog.LogInformation(rowContent[Common.KEYNAME.IPCTRANSID].ToString() + " Send Failed to URL " + _Url + " with exception ax = Certificate Path not Found => " + certpath, Common.FILELOGTYPE.LOGFILEPATH);
                            ProcessLog.LogError(new Exception("Certificate Path not Found => " + certpath), System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                            return false;
                        }
                    }

                    //string ret = string.Empty;

                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    request.ClientCertificates.Add(new X509Certificate2(certpath, certpass));
                }
                #endregion



                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);


                var result = reader.ReadToEnd();
                stream.Dispose();
                reader.Dispose();

                InputString = result.ToString();
                try
                {
                    ProcessLog.LogInformation($"Transaction {rowContent[Common.KEYNAME.IPCTRANSID].ToString()} Response HTTP: {Environment.NewLine}{InputString}", Common.FILELOGTYPE.LOGMSGINOUT);
                }
                catch { }
                return true;
            }
            catch (WebException exp)
            {
                try
                {
                    ProcessLog.LogInformation($"Transaction {rowContent[Common.KEYNAME.IPCTRANSID].ToString()} Response HTTP: {Environment.NewLine}{exp.Message}", Common.FILELOGTYPE.LOGMSGINOUT);
                }
                catch { }
                Utility.ProcessLog.LogError(exp, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + rowContent[Common.KEYNAME.IPCTRANSID].ToString() + OutputData);
                string errorCode = Common.ERRORCODE.DESTSYSTEM;
                try
                {
                    errorCode = ((int)(exp.Response as HttpWebResponse).StatusCode).ToString();
                }
                catch
                {
                }
                return false;
            }
            catch (Exception ex)
            {
                try
                {
                    ProcessLog.LogInformation($"Transaction {rowContent[Common.KEYNAME.IPCTRANSID].ToString()} Response HTTP: {Environment.NewLine}{ex.Message}", Common.FILELOGTYPE.LOGMSGINOUT);
                }
                catch { }
                ProcessLog.LogInformation(ex.ToString());
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + rowContent[Common.KEYNAME.IPCTRANSID].ToString() + OutputData);
                return false;
            }
        }
        #endregion

        private string CreateMessageHTTP(DataRow data, DataRowCollection drHeader, DataRowCollection drBody)
        {
            string result = "";
            try
            {
                //build header
                string resultHeader = "";
                for (int i = 0; i < drHeader.Count; i++)
                {
                    string FieldNo = drHeader[i]["FIELDNO"].ToString();
                    string FieldDesc = drHeader[i]["FIELDDESC"].ToString();
                    string FieldStyle = drHeader[i]["FIELDSTYLE"].ToString();
                    string FieldName = drHeader[i]["FIELDNAME"].ToString();
                    string ValueStyle = drHeader[i]["VALUESTYLE"].ToString();
                    string ValueName = drHeader[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = Commons.GetFieldValue(ValueStyle, ValueName, data);
                    value = Commons.GetFullFieldValueJSON(value, FieldStyle, FieldName, drHeader[i]["FORMATTYPE"].ToString(),
                    drHeader[i]["FORMATOBJECT"].ToString(), drHeader[i]["FORMATFUNCTION"].ToString(),
                    drHeader[i]["FORMATPARM"].ToString());
                    resultHeader += value.ToString() + ",";
                }
                if (!string.IsNullOrEmpty(resultHeader))
                    resultHeader = "{" + resultHeader.Substring(0, resultHeader.Length - 1) + "}";

                //build Body
                //string resultBody = "";

                JObject jBody = new JObject();
                for (int i = 0; i < drBody.Count; i++)
                {
                    bool isJarray = false;

                    string FieldNo = drBody[i]["FIELDNO"].ToString();
                    string FieldDesc = drBody[i]["FIELDDESC"].ToString();
                    string FieldStyle = drBody[i]["FIELDSTYLE"].ToString();
                    string FieldName = drBody[i]["FIELDNAME"].ToString();
                    string ValueStyle = drBody[i]["VALUESTYLE"].ToString();
                    string ValueName = drBody[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = Commons.GetFieldValue(ValueStyle, ValueName, data);
                    value = Commons.GetFullFieldValueJSON(value, FieldStyle, FieldName, drBody[i]["FORMATTYPE"].ToString(),
                    drBody[i]["FORMATOBJECT"].ToString(), drBody[i]["FORMATFUNCTION"].ToString(),
                    drBody[i]["FORMATPARM"].ToString());

                    JObject jCurrent = jBody;
                    string[] arrJsonPath = FieldName.Trim().Split('.');

                    for (int j = 0; j < arrJsonPath.Length; j++)
                    {
                        string strPath = arrJsonPath[j].Trim();
                        JObject jSelect = (JObject)jCurrent.SelectToken(strPath);
                        if (jSelect == null)
                        {
                            if (strPath.EndsWith("]"))
                            {
                                string pathKey = strPath.Split('[')[0];
                                int pathPos = int.Parse(strPath.Split('[')[1].Substring(0, strPath.Split('[')[1].Length - 1));
                                if (jCurrent.SelectToken(pathKey) != null)
                                {
                                    JArray jArr = (JArray)jCurrent.SelectToken(pathKey);
                                    if (jArr.Count <= pathPos)
                                    {
                                        for (int kk = jArr.Count; kk <= pathPos; kk++)
                                        {
                                            if (j != arrJsonPath.Length - 1)
                                            {
                                                jArr.Add(new JObject());
                                            }
                                            else
                                            {
                                                jArr.Add(value);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    jSelect = jCurrent;
                                    jSelect.Add(pathKey, new JArray());
                                    JArray jArr = (JArray)jSelect.SelectToken(pathKey);
                                    if (jArr.Count <= pathPos)
                                    {
                                        for (int kk = jArr.Count; kk <= pathPos; kk++)
                                        {
                                            if (j != arrJsonPath.Length - 1)
                                            {
                                                jArr.Add(new JObject());
                                            }
                                            else
                                            {
                                                jArr.Add(value);
                                            }
                                        }

                                    }
                                }

                                if (j != arrJsonPath.Length - 1)
                                {
                                    jSelect = (JObject)jCurrent.SelectToken(strPath);
                                }
                            }
                            else
                            {
                                jSelect = jCurrent;
                                if (arrJsonPath.Length == j + 1)
                                {
                                    jSelect.Add(new JProperty(strPath, value));
                                }
                                else
                                {
                                    jSelect.Add(strPath, new JObject());
                                    jSelect = (JObject)jSelect.SelectToken(strPath);
                                }
                            }
                        }

                        jCurrent = jSelect;
                    }
                }

                //if (!string.IsNullOrEmpty(resultBody))
                // resultBody = "{" + resultBody.Substring(0, resultBody.Length - 1) + "}";

                Dictionary<string, object> dicResult = new Dictionary<string, object>();
                try
                {
                    dicResult.Add(Common.KEYNAME.HTTPHEADER, string.IsNullOrEmpty(resultHeader) ? new JObject() : JObject.Parse(resultHeader));
                    dicResult.Add(Common.KEYNAME.HTTPBODY, jBody);
                }
                catch (Exception exp)
                {
                    Utility.ProcessLog.LogError(exp, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                    dicResult = new Dictionary<string, object>();
                    dicResult.Add(Common.KEYNAME.HTTPHEADER, resultHeader);
                    dicResult.Add(Common.KEYNAME.HTTPBODY, jBody);
                }

                result = JsonConvert.SerializeObject(dicResult);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
    }
}
