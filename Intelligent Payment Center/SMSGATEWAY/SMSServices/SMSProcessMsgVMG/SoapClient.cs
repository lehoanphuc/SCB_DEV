using DBConnection;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Addressing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utility;

namespace SMSProcessMsgMT
{
    class SoapClient: Microsoft.Web.Services3.Messaging.SoapClient
    {
        public SoapClient()
        {
            
        }

        private string CreateMessageXML(DataRowCollection dr, DataRow data)
        {
            string result = "";
            try
            {
                
                for (int i = 0; i < dr.Count; i++)
                {
                    string FieldNo = dr[i]["FIELDNO"].ToString();
                    string FieldDesc = dr[i]["FIELDDESC"].ToString();
                    string FieldStyle = dr[i]["FIELDSTYLE"].ToString();
                    string FieldName = dr[i]["FIELDNAME"].ToString();
                    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                    string ValueName = dr[i]["VALUENAME"].ToString();
                    // Get Value
                    object value = Commons.GetFieldValue(ValueStyle, ValueName, data);
                    // Get Full Value
                    value = Commons.GetFullFieldValueXML(value, FieldStyle, FieldName, dr[i]["FORMATTYPE"].ToString(),
                                    dr[i]["FORMATOBJECT"].ToString(), dr[i]["FORMATFUNCTION"].ToString(),
                                    dr[i]["FORMATPARM"].ToString());
                    result += value.ToString();
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation(ex.Message, Utility.Common.FILELOGTYPE.LOGFILEPATH);
            }
            return result;
        }

        private bool CreateHeader(string ipctransid, string _HeaderVariableName, string _TargetNameSpace, ref XmlDocument soapInfo, ref SoapEnvelope _requestEnvelop)
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
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation("ERROR - " + ipctransid + ": " + ex.Message, Utility.Common.FILELOGTYPE.LOGFILEPATH);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }

        private bool CreateBody(string ipctransid, string _MethodName, string _TargetNameSpace, ref XmlDocument soapInfo, ref SoapEnvelope _requestEnvelop)
        {
            try
            {
                XmlDocument xmlBody = new XmlDocument();
                XmlNode bodyroot = xmlBody.CreateElement(_MethodName, _TargetNameSpace);
                bodyroot.InnerXml = soapInfo.DocumentElement["BODY"].InnerXml;
                xmlBody.AppendChild(bodyroot);
                xmlBody.LoadXml(xmlBody.InnerXml.Replace("xmlns=\"\"", ""));
                _requestEnvelop.CreateBody();
                _requestEnvelop.Body.AppendChild(_requestEnvelop.ImportNode(xmlBody.DocumentElement, true));
                xmlBody = null;
                bodyroot = null;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation("ERROR - " + ipctransid + ": " + ex.Message, Utility.Common.FILELOGTYPE.LOGFILEPATH);
                return false;
            }
            return true;
        }
        public void SendMessage(DataTable dtOUTXML, DataTable dtWS, DataRow rowContent)
        {
            Connection con = new Connection();
            try
            {
                Utility.ProcessLog.LogInformation($"Start - {rowContent["IPCTRANSID"]}", Utility.Common.FILELOGTYPE.LOGMSGSYSTEM);
                SoapEnvelope _requestEnvelop = new SoapEnvelope();
                string _HeaderVariableName = dtWS.Rows[0]["HEADERVARIABLENAME"].ToString();
                string _MethodName = dtWS.Rows[0]["METHODNAME"].ToString();
                string _SoapAction = dtWS.Rows[0]["SOAPACTION"].ToString();
                string _URLWebservice = dtWS.Rows[0]["URLWEBSERVICE"].ToString();
                string _TargetNameSpace = dtWS.Rows[0]["TARGETNAMESPACE"].ToString();
                bool _CustomFormat = dtWS.Rows[0]["CUSTOMFORMAT"].ToString().Equals("Y") ? true : false;
                XmlDocument soapInfo = new XmlDocument();
                string contentXML = CreateMessageXML(dtOUTXML.Rows, rowContent);
                
                soapInfo.LoadXml(contentXML);
                if (_CustomFormat)
                {
                    _requestEnvelop.LoadXml(contentXML);
                    Utility.ProcessLog.LogInformation($"Done Custom Format - {rowContent["IPCTRANSID"]}", Utility.Common.FILELOGTYPE.LOGMSGSYSTEM);
                }
                else
                {
                    CreateHeader(rowContent["IPCTRANSID"].ToString(), _HeaderVariableName, _TargetNameSpace, ref soapInfo, ref _requestEnvelop);
                    Utility.ProcessLog.LogInformation($"Done Create Header - {rowContent["IPCTRANSID"]}", Utility.Common.FILELOGTYPE.LOGMSGSYSTEM);
                    if (!CreateBody(rowContent["IPCTRANSID"].ToString(), _MethodName, _TargetNameSpace, ref soapInfo, ref _requestEnvelop))
                    {
                        return;
                    }
                    Utility.ProcessLog.LogInformation($"Done Create Body - {rowContent["IPCTRANSID"]}", Utility.Common.FILELOGTYPE.LOGMSGSYSTEM);
                }
                Utility.ProcessLog.LogInformation($"SMSService Request XML - {rowContent["IPCTRANSID"]} : {_requestEnvelop.InnerXml}", Utility.Common.FILELOGTYPE.LOGMSGINOUT);
                base.Destination = new EndpointReference(new Microsoft.Web.Services3.Addressing.Address(new Uri(_URLWebservice)));
                SoapEnvelope responseEnvelop = new SoapEnvelope();
                responseEnvelop = base.SendRequestResponse(_SoapAction, _requestEnvelop);
                Utility.ProcessLog.LogInformation($"SMSService Response XML - {rowContent["IPCTRANSID"]} : {responseEnvelop.InnerXml}", Utility.Common.FILELOGTYPE.LOGMSGINOUT);
                Utility.ProcessLog.LogInformation($"Finish - {rowContent["IPCTRANSID"]}", Utility.Common.FILELOGTYPE.LOGMSGSYSTEM);
                XmlDocument docResponse = new XmlDocument();
                docResponse.LoadXml(responseEnvelop.InnerXml);
                string error_code = string.Empty;
                try
                {
                    error_code = docResponse.GetElementsByTagName("return")[0].InnerText;
                    JObject jRes = JsonConvert.DeserializeObject<JObject>(error_code);
                    error_code = jRes.GetValue("msgid",StringComparison.OrdinalIgnoreCase).Value<string>();
                }
                catch { }
                if (!string.IsNullOrEmpty(error_code))
                {
                    con.ExecuteNonquery(Common.ConStr, "SMSVMG_UPDATESTATUS", rowContent["IPCTRANSID"].ToString(), "Y");
                    Utility.ProcessLog.LogInformation($"{rowContent["IPCTRANSID"].ToString()} System has sent sms to {rowContent["RECEIVEDPHONE"].ToString()} successful", Utility.Common.FILELOGTYPE.LOGFILEPATH);
                }
                else
                {
                    con.ExecuteNonquery(Common.ConStr, "SMSVMG_UPDATESTATUS", rowContent["IPCTRANSID"].ToString(), "F");
                    Utility.ProcessLog.LogInformation($"{rowContent["IPCTRANSID"].ToString()} System has sent sms to {rowContent["RECEIVEDPHONE"].ToString()} fail", Utility.Common.FILELOGTYPE.LOGFILEPATH);
                }
            }
            catch (Exception ex)
            {
                con.ExecuteNonquery(Common.ConStr, "SMSVMG_UPDATESTATUS", rowContent["IPCTRANSID"].ToString(), "F");
                Utility.ProcessLog.LogInformation("ERROR - " + rowContent["IPCTRANSID"].ToString() + ": " + ex.Message, Utility.Common.FILELOGTYPE.LOGFILEPATH);
                Utility.ProcessLog.LogInformation($"Finish with exception - {rowContent["IPCTRANSID"]}", Utility.Common.FILELOGTYPE.LOGMSGSYSTEM);
            }
            finally
            {
                con = null;
            }
        }
    }
}
