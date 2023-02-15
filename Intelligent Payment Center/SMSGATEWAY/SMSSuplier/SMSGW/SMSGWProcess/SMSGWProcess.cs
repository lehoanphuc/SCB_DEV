using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using DBConnection;
using Utility;
using System.Threading;
namespace SMSGWProcess
{
    public class SMSGWProcess
    {
        static int sequenceSMS = 0;
        static ITransaction.AutoTrans autoTrans;
        int maxLength = 160;
        static string supplierid = string.Empty;
        string commandCodeBrName = string.Empty;
        Logger _log = new Logger();
        Connection dbObj = new Connection();

        public bool sendSMS(ref Hashtable inputData)
        {
            StartServer();
            
            //System.Diagnostics.Debugger.Launch();
            string errorCode = string.Empty;
            string errorDesc = string.Empty;
            string custPhone = string.Empty;
            int result = 0;
            string temp = string.Empty;
            try
            {
                string msg = inputData["MSGCONTENT"].ToString();

                custPhone = FormatPhoneNo(inputData["RECEIVEDPHONE"].ToString(), inputData["COUNTRYPREFIX"].ToString());
                if (custPhone.Equals("#"))
                {
                    inputData.Add("ERRORCODE", "9901");
                    inputData.Add("ERRORDESC", "Invailid phone number");
                    _log.Log("SendMTMsg error: " + "Invailid phone number");
                    
                    return false;
                }
                while (msg.Length > 0)
                {
                    if (msg.Length <= maxLength)
                    {
                        //result = SendMSG(msg, custPhone, inputData["SENDPHONE"].ToString(), inputData["MSGID"].ToString(), inputData["MSGTYPE"].ToString());
                        result = SendMSG(msg, custPhone, inputData);
                        break;
                    }
                    else
                    {
                        if (msg[maxLength] != ' ')
                        {
                            temp = msg.Substring(0, maxLength);
                            int pos = temp.LastIndexOf(' ');
                            temp = msg.Substring(0, pos);
                            //result = SendMSG(temp, custPhone, inputData["SENDPHONE"].ToString(), inputData["MSGID"].ToString(), inputData["MSGTYPE"].ToString());
                            result = SendMSG(temp, custPhone, inputData);
                            msg = msg.Substring(pos);
                        }
                        else
                        {
                            temp = msg.Substring(0, maxLength);
                            //result = SendMSG(temp, custPhone, inputData["SENDPHONE"].ToString(), inputData["MSGID"].ToString(), inputData["MSGTYPE"].ToString());
                            result = SendMSG(temp, custPhone, inputData);
                            msg = msg.Substring(maxLength);
                        }
                    }
                }
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    inputData.Add("ERRORCODE", result.ToString());
                    inputData.Add("ERRORDESC", "GW ERROR");
                    _log.Log(DateTime.Now.ToString() +  " GW ERROR " + result.ToString());
                    return false;
                }

            }
            catch (Exception ex)
            {
                inputData.Add("ERRORCODE", "9900");
                inputData.Add("ERRORDESC", ex.Message);
                _log.Log(DateTime.Now.ToString() +  " SEND MSG GW ERROR " + ex.Message);
                return false;
            }
        }
        public static string FormatPhoneNo(string phoneNo,string countryprefix)
        {
            //string phpr = "+95";
            //try
            //{
            //    phpr = ConfigurationManager.AppSettings["PhonePrefix"].ToString();
            //}
            //catch { }

            if (phoneNo.Length < 6 || phoneNo.Length > 13)
            {
                return "#";
            }
            else
            {
                if (phoneNo.Substring(0, 3) == countryprefix) //84
                    return phoneNo;
                else
                    //if (phoneNo.Substring(0, 3) == string.Format("+{0}", phpr)) //+84
                    //    return phoneNo;
                    ////return phoneNo.Remove(0, 1);
                    //else
                        if (phoneNo.Substring(0, 1) == "0")  //016,098
                            return phoneNo = countryprefix + phoneNo.Remove(0, 1);
                        else
                            //throw new Exception("Invalid phone number format.");
                            return "#";
            }
        }
        public int SendMSG(string msgContent, string phoneNo,Hashtable input)
        {
            int result = 0;
            try
            {
                //Show hastable result from IPC

                Common.SMSSequence = Common.SMSSequence + 1;

                string sequenceNo = Common.SMSSequence.ToString();
                DataTable dtlogrequest=new DataTable();
                dtlogrequest = dbObj.FillDataTable(Common.ConStr, "SMS_GW_INSERT_LOG", input["SourceIP"].ToString(), input["SourceChannel"].ToString(),
                    input["SourceApplication"].ToString(), input["SENDPHONE"].ToString(), phoneNo, msgContent, input["ReceiptRequestEndPoint"].ToString(),input["ID"],sequenceNo);
                //if (dtlogrequest.Rows.Count == 0)
                //{
                //    _log.Log(DateTime.Now.ToString() + " GW SendMSG Fail. Cannot get sequenceNO");
                //    return 0;
                //}
                //else
                //{
                //    if(!sequenceNo.Equals(dtlogrequest.Rows[0]["SequenceNo"].ToString().Trim()))
                //    {
                //        sequenceNo = dtlogrequest.Rows[0]["SequenceNo"].ToString().Trim();
                //    }
                //}

                //_log.Log(DateTime.Now.ToString() + " GW start log....");
                //_log.Log(DateTime.Now.ToString() + " GW log SMSTRANCODE " + input["SMSTRANCODE"].ToString());
                //_log.Log(DateTime.Now.ToString() + " GW log SOURCEID " + input["SOURCEID"].ToString());
                //_log.Log(DateTime.Now.ToString() + " GW log SOURCETRANREF " + input["SOURCETRANREF"].ToString());
                //_log.Log(DateTime.Now.ToString() + " GW log SourceIP " + input["SourceIP"].ToString());
                //_log.Log(DateTime.Now.ToString() + " GW log SourceChannel " + input["SourceChannel"].ToString());
                //_log.Log(DateTime.Now.ToString() + " GW log SourceApplication " + input["SourceApplication"].ToString());
                //_log.Log(DateTime.Now.ToString() + " GW log SENDPHONE " + input["SENDPHONE"].ToString());
                //_log.Log(DateTime.Now.ToString() + " GW log sequenceNo " + sequenceNo);
                //_log.Log(DateTime.Now.ToString() + " GW log Address " + phoneNo);
                //_log.Log(DateTime.Now.ToString() + " GW log Message " + msgContent);
                //_log.Log(DateTime.Now.ToString() + " GW log ReceiptRequestEndPoint " + input["ReceiptRequestEndPoint"].ToString());
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(Common.KEYNAME.IPCTRANCODE, input["SMSTRANCODE"].ToString());
                hasInput.Add(Common.KEYNAME.SOURCEID, input["SOURCEID"].ToString());
                hasInput.Add("SOURCETRANREF", input["SOURCETRANREF"].ToString());
                hasInput.Add("SourceIP", input["SourceIP"].ToString());
                hasInput.Add("SourceChannel", input["SourceChannel"].ToString());
                hasInput.Add("SourceApplication", input["SourceApplication"].ToString());
                hasInput.Add("SourceAddress", input["SENDPHONE"].ToString());
                hasInput.Add("SequenceNo", sequenceNo);
                //hasInput.Add("Address"] = "+959264817033"; //so a kieu
                //hasInput.Add("Address"] = "+959965060250";
                //hasInput.Add("Address","+959250147251");  //phu L
                //hasInput.Add("Address", "+959000000000");  //for test
                hasInput.Add("Address", phoneNo);  //phu L
                //hasInput.Add("Address","+84936396598");
                //hasInput.Add("Message"] = "We hope that you will find it interesting to start cooperating with us! If you have any questions, you are welcome to contact our customer service at smssupport@vianett.com We hope that you will find it interesting to start cooperating with us! If you have any questions, you are welcome to contact our customer service at smssupport@vianett.com";
                hasInput.Add("Message", msgContent);
                hasInput.Add("ReceiptRequestEndPoint", input["ReceiptRequestEndPoint"].ToString());






                //hasInput.Add(Common.KEYNAME.IPCTRANCODE, "SENDSMSGWSINGLE");
                //hasInput.Add(Common.KEYNAME.SOURCEID, "SMS");
                //hasInput.Add("SOURCETRANREF", "010");
                //hasInput.Add("SourceIP", "127.0.0.1");
                //hasInput.Add("SourceChannel", "IB");
                //hasInput.Add("SourceApplication", "IB");
                //hasInput.Add("SourceAddress", "6777");
                //hasInput.Add("SequenceNo", "1");
                ////hasInput.Add("Address"] = "+959264817033"; //so a kieu
                ////hasInput.Add("Address"] = "+959965060250";
                ////hasInput.Add("Address","+959250147251");  //phu L
                //hasInput.Add("Address", "+959000000000");  //phu L
                ////hasInput.Add("Address","+84936396598");
                ////hasInput.Add("Message"] = "We hope that you will find it interesting to start cooperating with us! If you have any questions, you are welcome to contact our customer service at smssupport@vianett.com We hope that you will find it interesting to start cooperating with us! If you have any questions, you are welcome to contact our customer service at smssupport@vianett.com";
                //hasInput.Add("Message", "TEST MESSAGE");
                //hasInput.Add("ReceiptRequestEndPoint", "http://172.20.171.114:83/Service.svc/Webservice_Test_SMSGW");
                ////trantest.Online = true;

                hasOutput = autoTrans.ProcessTransHAS(hasInput);
                //_log.Log(DateTime.Now.ToString() + " GW log ipcerrorcode " + hasOutput[Common.KEYNAME.IPCERRORCODE].ToString());
                //_log.Log(DateTime.Now.ToString() + " GW log InquiryCode " + (hasOutput["InquiryCode"].Equals(null)?"null":hasOutput["InquiryCode"].ToString()));
                //_log.Log(DateTime.Now.ToString() + " GW log SequenceNo " + hasOutput["SequenceNo"].ToString());
                //_log.Log(DateTime.Now.ToString() + " GW log ResponseCode " + hasOutput["ResponseCode"].ToString());
                //_log.Log(DateTime.Now.ToString() + " GW log ResponseDesc " + hasOutput["ResponseDesc"].ToString());
                //keep check in 50s. set 60 alway timeout so will choose one < 60
                DateTime dtstart = DateTime.Now;
                int checkflagdelay = int.Parse(ConfigurationManager.AppSettings["checkflagdelay"].ToString());
            N:
                while (CHECKFLAG())
                {
                    _log.Log(DateTime.Now.ToString() + "=====thread is busy. system will wait some second");
                    DateTime dtstop = DateTime.Now;
                    if ((dtstop - dtstart).Seconds > 50)
                    {

                        _log.Log(DateTime.Now.ToString() + "=====thread is busy over 50s");
                        goto M;
                    }
                    Thread.Sleep(checkflagdelay);

                }

                //SET FLAG BUSY:
                int flat = UPDATEFLAG(1);
                if (flat.Equals(0))
                {
                    Thread.Sleep(checkflagdelay);
                    _log.Log(DateTime.Now.ToString() + "=====update flag not success . wait some second to update again");
                    goto N;


                }
                M:
                if(hasOutput[Common.KEYNAME.IPCERRORCODE].ToString().Equals("0"))
                {
                    if (hasOutput["InquiryCode"] == null)
                    {
                        dbObj.ExecuteNonquery(Common.ConStr, "SMS_GW_UPDATE_LOG", hasOutput["SequenceNo"].ToString(), hasOutput["ResponseCode"].ToString(), hasOutput["ResponseDesc"].ToString(), "FAIL");
                        result = 0;
                    }
                    else
                    {
                        dbObj.ExecuteNonquery(Common.ConStr, "SMS_GW_UPDATE_LOG", hasOutput["SequenceNo"].ToString(), hasOutput["ResponseCode"].ToString(), hasOutput["ResponseDesc"].ToString(),  hasOutput["InquiryCode"].ToString());
                        result = 1;
                    }
                    
                    
                }
                else
                {
                    dbObj.ExecuteNonquery(Common.ConStr, "SMS_GW_UPDATE_LOG", sequenceNo, hasOutput[Common.KEYNAME.IPCERRORCODE].ToString(), hasOutput[Common.KEYNAME.IPCERRORDESC].ToString(), "0");
                    result = 0;
                }

                
                //free flag:
                UPDATEFLAG(0);
                return result;

            }
            catch (Exception ex)
            {
                _log.Log(DateTime.Now.ToString() + " GW SendMSG  in sequence:" + Common.SMSSequence + "|||exception=" + ex.Message.ToString());
                try
                { 
                UPDATEFLAG(0);
                    }
                catch
                {
                    _log.Log(DateTime.Now.ToString() + " GW SendMSG  cant reset flag");
                }
                return 9;
            }
        }
        private void StartServer()
        {
            try
            {
                if (autoTrans == null)
                {
                    autoTrans = (ITransaction.AutoTrans)Activator.GetObject(
                        typeof(ITransaction.AutoTrans),
                        ConfigurationManager.AppSettings["RMIPC_URL"].ToString());

                }
                //monitor = true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

        }
        private bool CHECKFLAG()
        {
            bool ret = false;

            try
            {
                DataTable dtportstatus = new DataTable();
                dtportstatus = dbObj.FillDataTable(Common.ConStr, "SMS_GW_GET_FLAG");
                ret = (bool)(dtportstatus.Rows[0]["FLAG"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
        }
        private int UPDATEFLAG(int value)
        {
            int ret = 0;
            try
            {
                ret = dbObj.ExecuteNonquery(Common.ConStr, "SMS_GW_UPDATE_FLAG", value);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
        }
    }
}
