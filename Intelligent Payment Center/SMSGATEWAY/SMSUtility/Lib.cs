using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Configuration;

namespace SMSUtility
{
    public class Lib
    {
        public bool SendMsgIn(Hashtable input)
        {
            bool result = false;
            try
            {
                ProcessQueues proQ = new ProcessQueues();
                proQ.SendToQueueIn(input);
                result = true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
        public Hashtable GetMsgQueueIn(ref int errorCode)
        {
            Hashtable result = new Hashtable();
            try
            {
                ProcessQueues proQ = new ProcessQueues();
                result = proQ.ReceiveFromQueueIn(ref errorCode);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
        public Hashtable GetMsgQueueOut(ref int errorCode)
        {
            Hashtable result = new Hashtable();
            try
            {
                ProcessQueues proQ = new ProcessQueues();
                result = proQ.ReceiveFromQueueOut(ref errorCode);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
        public void AutoTrans(ITransaction.AutoTrans autoStran)
        {
            int errorcode =0; 
            try
            {
                Hashtable hasMsgIn = GetMsgQueueIn(ref errorcode);
                if (errorcode == 0 && hasMsgIn.Count > 0)
                {
                    ConnectIPC objConnIPC = new ConnectIPC();
                    string msgOut = objConnIPC.IPCProcessSMSMsg(autoStran, hasMsgIn[Common.KEYNAME.MSGID].ToString(), hasMsgIn[Common.KEYNAME.RECEIVEDPHONE].ToString(), hasMsgIn[Common.KEYNAME.MSGDATE].ToString(),
                                                        hasMsgIn[Common.KEYNAME.MSGTIME].ToString(), hasMsgIn[Common.KEYNAME.MSGCONTENT].ToString(), hasMsgIn[Common.KEYNAME.PREFIX].ToString());
                    
                    string ipcWorkDate = msgOut.Substring(0, msgOut.IndexOf(" "));
                    msgOut = msgOut.Substring(ipcWorkDate.Length + 1, msgOut.Length - (ipcWorkDate.Length + 1));
                    string ipcTransID = msgOut.Substring(0, msgOut.IndexOf(" "));
                    msgOut = msgOut.Substring(ipcTransID.Length + 1, msgOut.Length - (ipcTransID.Length + 1));

                    Hashtable hasMsgSendOut = new Hashtable();
                    hasMsgSendOut.Add(Common.KEYNAME.IPCTRANSID, ipcTransID);
                    hasMsgSendOut.Add(Common.KEYNAME.RECEIVEDPHONE, hasMsgIn[Common.KEYNAME.RECEIVEDPHONE].ToString());
                    hasMsgSendOut.Add(Common.KEYNAME.SENDPHONE, hasMsgIn[Common.KEYNAME.SENDPHONE].ToString());
                    hasMsgSendOut.Add(Common.KEYNAME.MSGID, hasMsgIn[Common.KEYNAME.MSGID].ToString());
                    hasMsgSendOut.Add(Common.KEYNAME.MSGCONTENT, msgOut);
                    hasMsgSendOut.Add(Common.KEYNAME.PREFIX, hasMsgIn[Common.KEYNAME.PREFIX].ToString());
                    hasMsgSendOut.Add(Common.KEYNAME.MSGDATE, DateTime.Now.ToString("dd/MM/yyyy"));
                    hasMsgSendOut.Add(Common.KEYNAME.MSGTIME, DateTime.Now.ToString("HH:mm:ss"));
                    hasMsgSendOut.Add(Common.KEYNAME.IPCWORKDATE, ipcWorkDate);
                    hasMsgSendOut.Add(Common.KEYNAME.PIORITY, "");
                    if (hasMsgIn[Common.KEYNAME.MSGCONTENT].ToString().ToUpper().Equals("PNB MOBILE"))
                    {
                        hasMsgSendOut.Add(Common.KEYNAME.MSGTYPE, "W");
                    }
                    else
                    {
                        hasMsgSendOut.Add(Common.KEYNAME.MSGTYPE, "T");
                    }
                    

                    bool isSendOut = SendQueueOut(hasMsgSendOut);
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
        public bool SendQueueOut(Hashtable input)
        {
            bool result = false;
            try
            {
                ProcessQueues proQ = new ProcessQueues();
                proQ.SendToQueueOut(input);
                result = true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
        public static string FormatPhoneNo(string phoneNo)
        {
            string phpr = "95";
            try
            {
                phpr = ConfigurationManager.AppSettings["PhonePrefix"].ToString();
            }
            catch { }

            if (phoneNo.Length < 6 || phoneNo.Length > 13)
            {
                throw new Exception("Invalid phone number format.");
            }
            else
            {
                if (phoneNo.Substring(0, 2) == phpr) //84
                    return phoneNo;
                else
                    if (phoneNo.Substring(0, 3) == string.Format("+{0}", phpr)) //+84
                        return phoneNo.Remove(0, 1);
                    else
                        if (phoneNo.Substring(0, 1) == "0")  //016,098
                            return phoneNo = "0" + phoneNo.Remove(0, 1);
                        else
                            throw new Exception("Invalid phone number format.");
            }
        }
    }
}
