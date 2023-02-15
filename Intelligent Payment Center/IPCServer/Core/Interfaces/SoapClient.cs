using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.Services3;
using System.Xml;
using Utility;
using System.Configuration;
using System.Data;
using Microsoft.Web.Services3.Addressing;
using System.IO;
using Formatters;
using DBConnection;
using System.Net;

namespace Interfaces
{
    public class SoapClient: Microsoft.Web.Services3.Messaging.SoapClient
    {
        #region Private Variable
        private string _HeaderVariableName = string.Empty;
        private string _TargetNameSpace = string.Empty;
        private string _MethodName = string.Empty;
        private string _URLWebservice = string.Empty;
        private string _SoapAction = string.Empty;
        private bool _CustomFormat = false;
        SoapEnvelope _requestEnvelop = null;
        int _TimeOut = 120000;
        XmlDocument soapInfo = null;
        #endregion

        #region Constructor
        public SoapClient()
        {
            try
            {
                _requestEnvelop = new SoapEnvelope();
                _TimeOut = int.Parse(ConfigurationManager.AppSettings["WSTimeOut"]);
            }
            catch
            {
                _TimeOut = 120000;
            }
        }
        #endregion

        #region private function
        private bool CreateHeader(TransactionInfo tran)
        {
            try
            {
                XmlDocument xmlHeader = new XmlDocument();
                XmlNode headerroot = xmlHeader.CreateElement(_HeaderVariableName, _TargetNameSpace);
                headerroot.InnerXml = soapInfo.DocumentElement["HEADER"].InnerXml;
                xmlHeader.AppendChild(headerroot);
                xmlHeader.LoadXml(xmlHeader.InnerXml.Replace("xmlns=\"\"", ""));
                
                //
                _requestEnvelop.CreateHeader();
                _requestEnvelop.Header.AppendChild(_requestEnvelop.ImportNode(xmlHeader.DocumentElement, true));
                xmlHeader = null;
                headerroot = null;
            }
            catch(Exception ex)
            {
                //Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }

        private bool CreateBody(TransactionInfo tran)
        {
            try
            {
                XmlDocument xmlBody = new XmlDocument();
                XmlNode bodyroot = xmlBody.CreateElement(_MethodName, _TargetNameSpace);
                bodyroot.InnerXml = soapInfo.DocumentElement["BODY"].InnerXml;
                xmlBody.AppendChild(bodyroot);
                xmlBody.LoadXml(xmlBody.InnerXml.Replace("xmlns=\"\"", ""));
                //
                ////minh add for sms gateway
                if (tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString().Substring(0, 5).Equals("SMSGW"))
                {
                    string addparam = string.Empty;
                    addparam = xmlBody.InnerXml.Substring(xmlBody.InnerXml.IndexOf("://") + 3, 3);
                    xmlBody.InnerXml = xmlBody.InnerXml.Replace("xmlns", "xmlns:" + addparam);
                    xmlBody.InnerXml = xmlBody.InnerXml.Replace(_MethodName.ToString(), addparam + ":" + _MethodName.ToString());
                }
                //
                _requestEnvelop.CreateBody();
                _requestEnvelop.Body.AppendChild(_requestEnvelop.ImportNode(xmlBody.DocumentElement, true));
                xmlBody = null;
                bodyroot = null;
            }
            catch(Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }
        #endregion

        #region public function
        public bool SendMessage(TransactionInfo tran)
        {
            string idlog = tran.IPCTransID != -1 ? tran.IPCTransID.ToString() : (DateTime.Now.ToString("ddMMyyyyHHmmssfff") + new Random().Next(1, 20).ToString());
            SoapEnvelope responseEnvelop = new SoapEnvelope();
            try
            {
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
                    _HeaderVariableName = row[0]["HEADERVARIABLENAME"].ToString();
                    _MethodName = row[0]["METHODNAME"].ToString();
                    _SoapAction = row[0]["SOAPACTION"].ToString();
                    _URLWebservice = row[0]["URLWEBSERVICE"].ToString();
                    _TargetNameSpace = row[0]["TARGETNAMESPACE"].ToString();
                    _CustomFormat = row[0]["CUSTOMFORMAT"].ToString().Equals("Y") ? true : false;
                    if (tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("SMSNOTIFYDELIVERY"))
                    {
                        _URLWebservice = tran.Data["ENDPOINT"].ToString();
                        ProcessLog.LogInformation($"{tran.IPCTransID} =============send request sms notification delivery to url: { _URLWebservice} | Request: {Environment.NewLine}{tran.OutputData}", Common.FILELOGTYPE.LOGMSGINOUT);
                        //File.AppendAllText(@"C:\SMS\" + DateTime.Now.ToString("ddMMyyyy") + ".txt", responseEnvelop.InnerText + "\n");
                    }
                }
                soapInfo = new XmlDocument();                
                soapInfo.LoadXml(tran.OutputData);
                try
                {
                    ProcessLog.LogInformation($"{idlog} sending message to url {_URLWebservice} method name {_MethodName} Request XML: {Environment.NewLine}{tran.OutputData}", Common.FILELOGTYPE.LOGMSGINOUT);
                }
                catch { }
                if (_CustomFormat)
                {
                    _requestEnvelop.LoadXml(tran.OutputData);
                }
                else
                {
                    CreateHeader(tran);
                    if (!CreateBody(tran)) return false;
                }

                TransLib.LogOutput(tran);
                if (tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("SMS00014"))
                {
                    //File.AppendAllText(@"C:\SMS\" + DateTime.Now.ToString("ddMMyyyy") + ".txt", tran.OutputData + "\n");
                }

                //Utility.ProcessLog.LogInformation(tran.IPCTransID.ToString() + " Input soap:" + _requestEnvelop.InnerXml.ToString());
                
                base.Destination = new EndpointReference(new Microsoft.Web.Services3.Addressing.Address(new Uri(_URLWebservice)));
                responseEnvelop = base.SendRequestResponse(_SoapAction, _requestEnvelop);

                //Utility.ProcessLog.LogInformation(tran.IPCTransID.ToString() + " Response soap:" + responseEnvelop.InnerXml.ToString());

                //string responsehttp = getResponsebyHTTPrequest(_URLWebservice, _SoapAction, _requestEnvelop.InnerXml.ToString());
                tran.InputData = responseEnvelop.InnerXml;
                //thaity modify add 8/8/2013                    
                //Formatter.AnalyzeResultXML(tran);
                if (tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("SMS00014"))
                {
                    //File.AppendAllText(@"C:\SMS\" + DateTime.Now.ToString("ddMMyyyy") + ".txt", responseEnvelop.InnerText + "\n");
                }
                try
                {
                    ProcessLog.LogInformation($"{idlog} Response XML: {Environment.NewLine}{tran.InputData}", Common.FILELOGTYPE.LOGMSGINOUT);
                }
                catch { }
                TransLib.LogInput(tran);
            }
            catch (AsynchronousOperationException ax)
            {
                try
                {
                    ProcessLog.LogInformation($"{idlog} Response XML: {Environment.NewLine}{ax.Message}", Common.FILELOGTYPE.LOGMSGINOUT);
                }
                catch { }
                tran.SetErrorInfo(ax);
                Utility.ProcessLog.LogError(ax, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                Connection con = new Connection();
                con.ExecuteNonquery(Common.ConStr, "IPC_CHECKREVERSAL", tran.IPCTransID, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), Common.ERRORCODE.SYSTEM);
                return false;
            }
            catch (Exception ex)
            {
                try
                {
                    ProcessLog.LogInformation($"{idlog} Response XML: {Environment.NewLine}{ex.Message}", Common.FILELOGTYPE.LOGMSGINOUT);
                }
                catch { }
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                if (ex.GetType().FullName.Equals("System.TimeoutException"))
                {
                    Connection con = new Connection();
                    con.ExecuteNonquery(Common.ConStr, "IPC_CHECKREVERSAL", tran.IPCTransID, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), Common.ERRORCODE.SYSTEM);
                }
                return false;
            }
     
            return true;
        }
        public bool SendMessageHTTPRequest(TransactionInfo tran)
        {
            SoapEnvelope responseEnvelop = new SoapEnvelope();
            try
            {
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
                    _HeaderVariableName = row[0]["HEADERVARIABLENAME"].ToString();
                    _MethodName = row[0]["METHODNAME"].ToString();
                    _SoapAction = row[0]["SOAPACTION"].ToString();
                    _URLWebservice = row[0]["URLWEBSERVICE"].ToString();
                    _TargetNameSpace = row[0]["TARGETNAMESPACE"].ToString();
                    _CustomFormat = row[0]["CUSTOMFORMAT"].ToString().Equals("Y") ? true : false;
                }
                soapInfo = new XmlDocument();
                soapInfo.LoadXml(tran.OutputData);
                if (_CustomFormat)
                {
                    _requestEnvelop.LoadXml(tran.OutputData);
                }
                else
                {
                    CreateHeader(tran);
                    if (!CreateBody(tran)) return false;
                }

                TransLib.LogOutput(tran);


                base.Destination = new EndpointReference(new Microsoft.Web.Services3.Addressing.Address(new Uri(_URLWebservice)));
                //responseEnvelop = base.SendRequestResponse(_SoapAction, _requestEnvelop);
               
                string responsehttp = getResponsebyHTTPrequest(_URLWebservice, _SoapAction, _requestEnvelop.InnerXml.ToString());
                tran.InputData = responsehttp;
                Utility.ProcessLog.LogInformation(tran.IPCTransID.ToString() + " Response topup: " + responsehttp);


                TransLib.LogInput(tran);
            }
            catch (Microsoft.Web.Services3.AsynchronousOperationException ax)
            {
                tran.SetErrorInfo(ax);
                Utility.ProcessLog.LogError(ax, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                Connection con = new Connection();
                con.ExecuteNonquery(Common.ConStr, "IPC_CHECKREVERSAL", tran.IPCTransID, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), Common.ERRORCODE.SYSTEM);
                return false;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                if (ex.GetType().FullName.Equals("System.TimeoutException"))
                {
                    Connection con = new Connection();
                    con.ExecuteNonquery(Common.ConStr, "IPC_CHECKREVERSAL", tran.IPCTransID, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), Common.ERRORCODE.SYSTEM);
                }
                return false;
            }


            return true;
        }
        #endregion
        private string getResponsebyHTTPrequest(string url, string action, string xml)
        {
            try
            {
                string newaction = !string.IsNullOrEmpty(action) ? action : "";
                string ret = string.Empty;
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                //webRequest.Headers.Add(@"SOAP:Action");
                webRequest.Headers.Add("SOAPAction", action);
                webRequest.ContentType = "text/xml;charset=\"utf-8\"";
                webRequest.Accept = "text/xml";
                webRequest.Method = "POST";
                webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; " +
                                        "Trident/4.0; SLCC1; .NET CLR 2.0.50727; Media Center PC 5.0; " +
                                        ".NET CLR 3.5.21022; .NET CLR 3.5.30729; .NET CLR 3.0.30618; " +
                                        "InfoPath.2; OfficeLiveConnector.1.3; OfficeLivePatch.0.0)"; 
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(xml);

                using (Stream stream = webRequest.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }
                //using (WebResponse response = webRequest.GetResponse())
                //{
                //    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                //    {
                //        string soapResult = rd.ReadToEnd();
                //        ret = soapResult;
                //    }
                //}
                try
                {
                    using (WebResponse response = webRequest.GetResponse())
                    {
                        using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                        {
                            string soapResult = rd.ReadToEnd();
                            ret = soapResult;
                            Utility.ProcessLog.LogInformation("Response topup : " + ret);
                        }
                    }
                }
                catch (WebException e)
                {
                    using (WebResponse response = e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        Utility.ProcessLog.LogInformation("Error code: " + httpResponse.StatusCode);
                        using (Stream data = response.GetResponseStream())
                        using (var reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            ret = text;
                            Utility.ProcessLog.LogInformation("Response topup fail: " + text);
                        }
                    }
                }
                //Utility.ProcessLog.LogInformation(" webRequest header:  " + webRequest.GetRequestStream().ToString());
                

                return ret;
            }
            catch(Exception ex)
            {
                Utility.ProcessLog.LogInformation(ex.ToString());
                throw ex;
            }
            

        }
    }
}