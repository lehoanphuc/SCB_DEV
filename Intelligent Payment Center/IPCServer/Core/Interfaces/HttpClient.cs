using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Utility;
using System.Configuration;
using System.Data;
using System.IO;
using Formatters;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json.Linq;

namespace Interfaces
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
        public bool HttpPostMessage(TransactionInfo tran)
        {
            try
            {
                TransLib.LogOutput(tran);

                string _Url = string.Empty;
                string _Action = string.Empty;
                string _ContentType = string.Empty;

                string condition = " DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString() + "'";
                DataRow[] row = Common.DBICONNECTIONWS.Select(condition);
                if (row.Length != 1)
                {
                    tran.ErrorCode = Common.ERRORCODE.INVALID_WSMAST;
                    return false;
                }
                else
                {
                    _Url = row[0]["URLWEBSERVICE"].ToString().Trim();
                    _Action = row[0]["SOAPACTION"].ToString().Trim();
                    _ContentType = row[0]["CONTENTTYPE"].ToString().Trim();
                }

                FormatURL(tran, ref _Url);
                ProcessLog.LogInformation($"Transaction {tran.IPCTransID} sending message to url: {1}: { _Url} | Request: {Environment.NewLine}{tran.OutputData}", Common.FILELOGTYPE.LOGMSGINOUT);
                BypassCertificateError();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_Url);
                request.Method = _Action;
                request.ContentType = _ContentType;
                //header
                JObject jo = new JObject();
                JObject jHeader = new JObject();
                try
                {
                    jo = Common.NewParse(tran.OutputData);
                    jHeader = Common.NewParse(jo.SelectToken(Common.KEYNAME.HTTPHEADER).ToString());
                    List<string> keys = jHeader.Properties().Select(p => p.Name).ToList();
                    foreach (string k in keys)
                    {
                        request.Headers.Add(k, jHeader.SelectToken(k).ToString());
                    }
                }
                catch { }
                

                string postData = string.Empty;
                if (_ContentType.Equals(Common.KEYNAME.HTTPFORMVALUE))
                {
                    //postData = tran.OutputData;
                    JObject jbody = Common.NewParse(jo.SelectToken(Common.KEYNAME.HTTPBODY).ToString());
                    List<string> keysBody = jbody.Properties().Select(p => p.Name).ToList();
                    StringBuilder sb = new StringBuilder();
                    foreach (string k in keysBody)
                    {
                        if (sb.Length > 0) sb.Append('&');
                        sb.Append(System.Web.HttpUtility.UrlEncode(k));
                        sb.Append('=');
                        sb.Append(System.Web.HttpUtility.UrlEncode(jbody.SelectToken(k).ToString()));
                    }
                    postData = sb.ToString();
                    byte[] bytes = Encoding.UTF8.GetBytes(postData);
                    request.ContentLength = bytes.Length;

                    //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Dispose();
                }
                else if (_ContentType.Equals(Common.KEYNAME.HTTPJSONVALUE) || _ContentType.Equals(Common.KEYNAME.HTTPTEXTPLAIN))
                {
                    //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    //body
                    if (_Action.Equals(Common.KEYNAME.POST))
                    {
                        postData = jo.SelectToken(Common.KEYNAME.HTTPBODY).ToString();
                        ProcessLog.LogInformation(string.Format("Transaction {0} sending message to url: {1} and message content is {2}", tran.IPCTransID.ToString(), _Url, postData.ToString()));
                        byte[] bytes = Encoding.UTF8.GetBytes(postData);
                        request.ContentLength = bytes.Length;

                        Stream requestStream = request.GetRequestStream();
                        requestStream.Write(bytes, 0, bytes.Length);
                        requestStream.Dispose();
                    }
                }
                else if (_ContentType.Equals(Common.KEYNAME.HTTPXMLVALUE))
                {
                    ProcessLog.LogInformation(string.Format("Transaction {0} sending message to url: {1} and message content is {2}", tran.IPCTransID.ToString(), _Url, tran.OutputData));
                    byte[] bytes = Encoding.UTF8.GetBytes(tran.OutputData);
                    request.ContentLength = bytes.Length;

                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Dispose();
                }
                else
                {
                    tran.ErrorCode = Common.ERRORCODE.INVALID_WSMAST;
                    return false;
                }
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);


                var result = reader.ReadToEnd();
                stream.Dispose();
                reader.Dispose();

                tran.InputData = result.ToString();
                ProcessLog.LogInformation($"Transaction {tran.IPCTransID} Response: {Environment.NewLine}{tran.InputData}", Common.FILELOGTYPE.LOGMSGINOUT);

                TransLib.LogInput(tran);
                return true;
            }
            catch (WebException exp)
            {
                ProcessLog.LogInformation(exp.ToString());
                string errorCode = Common.ERRORCODE.DESTSYSTEM;
                try
                {
                    errorCode = ((int)(exp.Response as HttpWebResponse).StatusCode).ToString();
                }
                catch
                {
                }
                tran.InputData = new StreamReader(exp.Response.GetResponseStream()).ReadToEnd();
                tran.SetErrorInfo(errorCode, tran.InputData);
                return false;
            }
            catch (Exception ex)
            {
                ProcessLog.LogInformation(ex.ToString());
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString() + tran.OutputData.ToString());
                return false;
            }
        }

        //Post request with private cert
        public bool HttpPostCVRSMessage(TransactionInfo tran)
        {
            //id for Log file
            string idlog = tran.IPCTransID != -1 ? tran.IPCTransID.ToString() : (DateTime.Now.ToString("ddMMyyyyHHmmssfff") + new Random().Next(1, 20).ToString());
            try
            {
                TransLib.LogOutput(tran);
                
                string _Url = string.Empty;
                string _Action = string.Empty;
                string _ContentType = string.Empty;

                string condition = " DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString() + "'";
                DataRow[] row = Common.DBICONNECTIONWS.Select(condition);
                if (row.Length != 1)
                {
                    tran.ErrorCode = Common.ERRORCODE.INVALID_WSMAST;
                    return false;
                }
                else
                {
                    _Url = row[0]["URLWEBSERVICE"].ToString().Trim();
                    _Action = row[0]["SOAPACTION"].ToString().Trim();
                    _ContentType = row[0]["CONTENTTYPE"].ToString().Trim();
                }

                FormatURL(tran, ref _Url);
                try
                {
                    ProcessLog.LogInformation($"Transaction {idlog} sending message to url: {1}: { _Url} | Request HTTP: {Environment.NewLine}{tran.OutputData}", Common.FILELOGTYPE.LOGMSGINOUT);
                }
                catch { }
                BypassCertificateError();

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_Url);
                request.Method = _Action;
                request.ContentType = _ContentType;
                //header
                JObject jo = new JObject();
                JObject jHeader = new JObject();
                try
                {
                    jo = Common.NewParse(tran.OutputData);
                    jHeader = Common.NewParse(jo.SelectToken(Common.KEYNAME.HTTPHEADER).ToString());
                    List<string> keys = jHeader.Properties().Select(p => p.Name).ToList();
                    foreach (string k in keys)
                    {
                        request.Headers.Add(k, jHeader.SelectToken(k).ToString());
                    }
                }
                catch { }


                string postData = string.Empty;
                if (_ContentType.Equals(Common.KEYNAME.HTTPFORMVALUE))
                {
                    //postData = tran.OutputData;
                    JObject jbody = Common.NewParse(jo.SelectToken(Common.KEYNAME.HTTPBODY).ToString());
                    List<string> keysBody = jbody.Properties().Select(p => p.Name).ToList();
                    StringBuilder sb = new StringBuilder();
                    foreach (string k in keysBody)
                    {
                        if (sb.Length > 0) sb.Append('&');
                        sb.Append(System.Web.HttpUtility.UrlEncode(k));
                        sb.Append('=');
                        sb.Append(System.Web.HttpUtility.UrlEncode(jbody.SelectToken(k).ToString()));
                    }
                    postData = sb.ToString();
                    byte[] bytes = Encoding.UTF8.GetBytes(postData);
                    request.ContentLength = bytes.Length;

                    //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Dispose();
                }
                else if (_ContentType.Equals(Common.KEYNAME.HTTPJSONVALUE) || _ContentType.Equals(Common.KEYNAME.HTTPTEXTPLAIN))
                {
                    //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    //body
                    if (_Action.Equals(Common.KEYNAME.POST))
                    {
                        postData = jo.SelectToken(Common.KEYNAME.HTTPBODY).ToString();
                        ProcessLog.LogInformation(string.Format("Transaction {0} sending message to url: {1} and message content is {2}", tran.IPCTransID.ToString(), _Url, postData.ToString()));
                        byte[] bytes = Encoding.UTF8.GetBytes(postData);
                        request.ContentLength = bytes.Length;

                        Stream requestStream = request.GetRequestStream();
                        requestStream.Write(bytes, 0, bytes.Length);
                        requestStream.Dispose();
                    }
                }
                else if (_ContentType.Equals(Common.KEYNAME.HTTPXMLVALUE))
                {
                    //ProcessLog.LogInformation(string.Format("Transaction {0} sending message to url: {1} and message content is {2}", tran.IPCTransID.ToString(), _Url, tran.OutputData));
                    byte[] bytes = Encoding.UTF8.GetBytes(tran.OutputData);
                    request.ContentLength = bytes.Length;

                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Dispose();
                }
                else
                {
                    tran.ErrorCode = Common.ERRORCODE.INVALID_WSMAST;
                    return false;
                }


                #region Add cert to request
                string certpath = string.Empty;
                string certpass = string.Empty;
                try
                {
                    certpath = row[0]["CERTPATH"].ToString();
                    certpass = row[0]["CERTPASS"].ToString();
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
                            ProcessLog.LogInformation(tran.IPCTransID.ToString() + " SendMessage Failed to URL " + _Url + " with exception ax = Certificate Path not Found => " + certpath);
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

                tran.InputData = result.ToString();
                try
                {
                    ProcessLog.LogInformation($"Transaction {idlog} Response HTTP: {Environment.NewLine}{tran.InputData}", Common.FILELOGTYPE.LOGMSGINOUT);
                }
                catch { }
                TransLib.LogInput(tran);
                return true;
            }
            catch (WebException exp)
            {
                try
                {
                    ProcessLog.LogInformation($"Transaction {idlog} Response HTTP: {Environment.NewLine}{exp.Message}", Common.FILELOGTYPE.LOGMSGINOUT);
                }
                catch { }
                Utility.ProcessLog.LogError(exp, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString() + tran.OutputData.ToString());
                string errorCode = Common.ERRORCODE.DESTSYSTEM;
                try
                {
                    errorCode = ((int)(exp.Response as HttpWebResponse).StatusCode).ToString();
                }
                catch
                {
                }
                tran.InputData = new StreamReader(exp.Response.GetResponseStream()).ReadToEnd();
                tran.SetErrorInfo(errorCode, tran.InputData);
                return false;
            }
            catch (Exception ex)
            {
                try
                {
                    ProcessLog.LogInformation($"Transaction {idlog} Response HTTP: {Environment.NewLine}{ex.Message}", Common.FILELOGTYPE.LOGMSGINOUT);
                }
                catch { }
                ProcessLog.LogInformation(ex.ToString());
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString() + tran.OutputData.ToString());
                return false;
            }
        }


        #region VuTT 20180528 de tranh anh huong, su dung ham cu cho ycdc, khi trien khai he thong moi, xoa bo ham nay
        public bool HttpPostMessageOld(TransactionInfo tran)
        {
            try
            {
                try
                {
                    TransLib.LogOutput(tran);

                    string _Url = string.Empty;
                    string condition = " DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                    condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString() + "'";
                    DataRow[] row = Common.DBICONNECTIONWS.Select(condition);
                    if (row.Length != 1)
                    {
                        tran.ErrorCode = Common.ERRORCODE.INVALID_WSMAST;
                        return false;
                    }
                    else
                    {
                        _Url = row[0]["URLWEBSERVICE"].ToString();
                    }

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_Url);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";

                    string postData = tran.OutputData;
                    byte[] bytes = Encoding.UTF8.GetBytes(postData);
                    request.ContentLength = bytes.Length;

                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);

                    WebResponse response = request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);

                    var result = reader.ReadToEnd();
                    stream.Dispose();
                    reader.Dispose();

                    tran.InputData = result.ToString();

                    TransLib.LogInput(tran);
                    return true;
                }
                catch (WebException exp)
                {
                    tran.InputData = new StreamReader(exp.Response.GetResponseStream()).ReadToEnd();
                    //tran.SetErrorInfo
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + tran.IPCTransID.ToString() + tran.OutputData.ToString());
                return false;
            }
        }
        #endregion
        #endregion
    }
}
