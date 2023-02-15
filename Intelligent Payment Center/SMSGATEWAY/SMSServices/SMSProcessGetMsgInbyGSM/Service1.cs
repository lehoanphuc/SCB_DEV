using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using DBConnection;
using System.Collections;
using Utility;
using System.Reflection;
using System.Configuration;
using System.ServiceProcess;
using System.IO;
using System.IO.Ports;

namespace SMSProcessGetMsgInbyGSM
{
    public partial class Service1 : ServiceBase
    {
        static string PortName = string.Empty;
        private bool monitor = true;
        private Thread thread;
        private Hashtable smsData;
        Assembly assembly = null;
        Type type = null;
        object instance = null;
        string method = string.Empty;
        static ITransaction.AutoTrans autoTrans;
        static FixedSizedQueue<QueueMsg> queueMessage = new FixedSizedQueue<QueueMsg>();
        static DateTime lastestdeleteallmessagetime = new DateTime();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //5.4.2017 minh add queue for avoid duplicated message in
                lastestdeleteallmessagetime = DateTime.Now;
                queueMessage.Limit = 50;
                try
                {
                    queueMessage.Limit = int.Parse(ConfigurationManager.AppSettings["LimitQueueMessage"]);
                }
                catch { }
                monitor = true;
                Common.ConStr = ConfigurationManager.ConnectionStrings["ConnectionHost"].ToString();
                Common.ConStr = Common.DecryptData(Common.ConStr);
                DataTable SMSSupplier = new DataTable();
                Connection con = new Connection();
                SMSSupplier = con.FillDataTable(Common.ConStr, "SMS_GETSMSSUPPLIER");
                assembly = Assembly.LoadFrom(SMSSupplier.Rows[0]["ASSEMBLYNAME"].ToString());
                type = assembly.GetType(SMSSupplier.Rows[0]["ASSEMBLYTYPE"].ToString());
                //method = SMSSupplier.Rows[0]["MTMETHOD"].ToString();
                method = "GetMsgIn";
                instance = Activator.CreateInstance(type);
                // listen on multi port
                DataTable dtrouter = new DataTable();
                dtrouter = con.FillDataTable(Common.ConStr, "SMS_GETROUTER_IN");
                if (dtrouter.Rows.Count == 0)
                {
                    Utility.ProcessLog.LogInformation("ERROR SMS SENDMTMSG CAN'T GET SMS ROUTER");
                }
                else
                {
                    foreach (DataRow r in dtrouter.Rows)
                    {
                        Thread thread = new Thread(() => GSMGetMsgInSMS(r["GROUPID"].ToString()));
                        thread.Start();
                    }
                }



                //thread = new Thread(new ThreadStart(GSMGetMsgIn));
                //thread.Start();
                EventLog.WriteEntry("Start SMSGSMProcessMsgIn service successful");
                SMSGSMReceive();
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                EventLog.WriteEntry("Error start. Error:" + ex.Message);
                OnStop();
            }
        }
        public void SMSGSMReceive()
        {
            try
            {
                if (autoTrans == null)
                {
                    autoTrans = (ITransaction.AutoTrans)Activator.GetObject(
                        typeof(ITransaction.AutoTrans),
                        "tcp://" + ConfigurationManager.AppSettings["RemotingAddress"].ToString() + ":" +
                        ConfigurationManager.AppSettings["RemotingPort"].ToString() + "/IPCAutoTrans");
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void GSMGetMsgIn()
        {
            string result = string.Empty;
            string phpr = "+856";
            int sleept = 1000;
            try
            {
                phpr = "+" + ConfigurationManager.AppSettings["PhonePrefix"].ToString();
            }
            catch { }

            try
            {
                sleept = int.Parse(ConfigurationManager.AppSettings["ReadSleep"].ToString());
            }
            catch
            {
            }

            try
            {
                while (monitor)
                {
                    try
                    {
                        result = (string)type.InvokeMember(method, System.Reflection.BindingFlags.InvokeMethod, null, instance, new object[] { "ALL" });
                        string MSGContent = null;
                        int dem;
                        string ReceivedPhone;
                        string MSGId = "test";
                        string SendPhone = "01672711815";
                        string Prefix = "test";

                        while (result.IndexOf("+CMGL:") >= 0)
                        {
                            result = result.Substring(result.IndexOf("+CMGL:") + 6, result.Length - result.IndexOf("+CMGL:") - 6);
                            dem = result.IndexOf("\",\"") + 3;

                            result = result.Substring(dem, result.Length - dem);
                            dem = result.IndexOf("\"");
                            ReceivedPhone = result.Substring(0, dem);
                            ReceivedPhone = ReceivedPhone.Replace(phpr, "0");
                            //ReceivedPhone = FormatPhoneNo(ReceivedPhone);
                            if (!PhoneNoFormatValid(ReceivedPhone))
                            {
                                ReceivedPhone = "#";
                            }

                            dem = result.IndexOf("\r\n") + 2;
                            result = result.Substring(dem, result.Length - dem);

                            if (result.IndexOf("\r\n") >= 0)
                            {
                                dem = result.IndexOf("\r\n");
                                MSGContent = result.Substring(0, dem);
                                result = result.Substring(dem, result.Length - dem);
                            }

                            Connection con = new Connection();
                            DataTable temp;
                            temp = con.FillDataTableSQL(Common.ConStr, "SELECT MSGID = MAX(MSGID) FROM SMS_MessageIn");
                            if (temp.Rows[0].ItemArray[0].ToString() != "")
                            {
                                MSGId = temp.Rows[0].ItemArray[0].ToString();
                            }
                            else
                            {
                                MSGId = "1000000000";
                            }
                            MSGId = (Convert.ToInt64(MSGId) + 1).ToString();
                            Prefix = MSGId;

                            MSGContent = MSGContent.ToUpper();
                            while (MSGContent.Contains("  "))
                                MSGContent = MSGContent.Replace("  ", " ");
                            while (MSGContent.Substring(0, 1) == " ")
                                MSGContent = MSGContent.Substring(1, MSGContent.Length - 1);
                            while (MSGContent.Substring(MSGContent.Length - 1, 1) == " ")
                                MSGContent = MSGContent.Substring(0, MSGContent.Length - 1);

                            object[] objPara = new object[5];
                            objPara[0] = MSGId;
                            objPara[1] = ReceivedPhone;
                            objPara[2] = SendPhone;
                            objPara[3] = "PNB " + MSGContent;
                            objPara[4] = "0";
                            Hashtable input = new Hashtable();
                            input.Add("IPCTRANCODE", "IB000500");
                            input.Add("SOURCEID", "IB");

                            input.Add("STORENAME", "SMS_MESSAGEIN_INSERT");
                            input.Add("PARA", objPara);
                            Hashtable output = autoTrans.ProcessTransHAS(input);
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                            EventLog.WriteEntry("Error get message, error desc:" + ex.Message);
                        }
                        catch (Exception sex)
                        {
                        }
                    }
                    Thread.Sleep(sleept);
                }
            }
            catch (Exception e)
            {
                Utility.ProcessLog.LogError(e, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                EventLog.WriteEntry("Error get message, error desc:" + e.Message);
                GSMGetMsgIn();
            }
        }

        private bool PhoneNoFormatValid(string format)
        {
            string allowableLetters = "0123456789+";

            foreach (char c in format)
            {
                if (!allowableLetters.Contains(c.ToString()))
                    return false;
            }

            return true;
        }

        protected override void OnStop()
        {
            //FREE STATUS PORT
            Connection dbObj = new Connection();
            DataTable dtportstatusupdate = new DataTable();
            dtportstatusupdate = dbObj.FillDataTable(Common.ConStr, "SMS_GSM_GETPORT_UPDATESTATUS", PortName, 0, "IN");

            EventLog.WriteEntry("Stop SMSGSMProcessMsgIn service.");
            monitor = false;
            this.Stop();
            thread.Abort();
        }
        private void GSMGetMsgInSMS(string GROUPID)
        {

            string result = string.Empty;
            string phpr = "+856";
            int sleept = 1000;
            try
            {
                phpr = "+" + ConfigurationManager.AppSettings["PhonePrefix"].ToString();
            }
            catch { }

            try
            {
                sleept = int.Parse(ConfigurationManager.AppSettings["ReadSleep"].ToString());
            }
            catch
            {
            }

            try
            {
                DataTable dtport = new DataTable();
                SerialPort port = new SerialPort();
                Connection dbObj = new Connection();
                DataTable lstSMS = new DataTable();
                dtport = dbObj.FillDataTable(Common.ConStr, "SMS_GSM_GETPORT", GROUPID);
                if (dtport.Rows.Count == 0)
                {
                    Utility.ProcessLog.LogInformation("ERROR SMS SENDMTMSG GET PORT FAILSE");

                }
                else
                {
                    foreach (DataRow r in dtport.Rows)
                    {

                        PortName = r["COMPORT"].ToString();
                        port = new SerialPort(r["COMPORT"].ToString(), int.Parse(r["BAUDRATE"].ToString()), Parity.None, int.Parse(r["DATABITS"].ToString()), StopBits.One);
                        //port.NewLine = r["NEWLINE"].ToString();
                        //port.WriteTimeout = int.Parse(r["WRITETIMEOUT"].ToString());
                        //port.ReadTimeout = int.Parse(r["READTIMEOUT"].ToString());
                        //msg.Add("PORTSMS", port);
                    }
                    //Common.ListPort.Add(port);

                }
                while (monitor)
                {
                    try
                    {
                        //check port is use for two ways message:
                        bool isportbusy = true;

                        while (isportbusy)
                        {
                        N: 
                            DataTable dtportstatus = new DataTable();
                            dtportstatus = dbObj.FillDataTable(Common.ConStr, "SMS_GSM_GETPORT_DETAIL", PortName, "IN");
                            isportbusy = (bool)(dtportstatus.Rows[0]["BUSY"]);
                            if (isportbusy)
                            {
                                Thread.Sleep(100);
                            }
                            else
                            {
                                //dtportstatus = dbObj.FillDataTable(Common.ConStr, "SMS_GSM_GETPORT_UPDATESTATUS", PortName, 1, "IN");
                                int queryResult = 0; // for check duplicate update by multithread
                                queryResult = dbObj.ExecuteNonquery(Common.ConStr, "SMS_GSM_GETPORT_UPDATESTATUS", PortName, 1, "IN");
                                //Utility.ProcessLog.LogInformation("===============update port <<<IN>>> with result effect rows= " + queryResult.ToString());
                                if (queryResult <= 0)
                                {
                                    Utility.ProcessLog.LogInformation("===============update port <<<IN>>> with result effect rows= " + queryResult.ToString());
                                    Thread.Sleep(100);
                                    goto N;


                                }

                            }

                        }

                        result = (string)type.InvokeMember(method, System.Reflection.BindingFlags.InvokeMethod, null, instance, new object[] { "ALL", (SerialPort)port });
                        string MSGContent = null;
                        int dem;
                        string ReceivedPhone;
                        string MSGId = "test";
                        string SendPhone = "01672711815";
                        string Prefix = "test";

                        while (result.IndexOf("+CMGL:") >= 0)
                        {
                            result = result.Substring(result.IndexOf("+CMGL:") + 6, result.Length - result.IndexOf("+CMGL:") - 6);
                            dem = result.IndexOf("\",\"") + 3;
                            //find msgid:
                            int messageidstart = 1;
                            string messageid = string.Empty;
                            for (int i = messageidstart; i <= result.Length; i++)
                                if (result[i] == ',')
                                {
                                    messageid = result.Substring(messageidstart, i - messageidstart);
                                    break;
                                }
                            result = result.Substring(dem, result.Length - dem);
                            dem = result.IndexOf("\"");
                            ReceivedPhone = result.Substring(0, dem);
                            ReceivedPhone = ReceivedPhone.Replace(phpr, "0");
                            //ReceivedPhone = FormatPhoneNo(ReceivedPhone);
                            if (!PhoneNoFormatValid(ReceivedPhone))
                            {
                                ReceivedPhone = "#";
                            }

                            dem = result.IndexOf("\r\n") + 2;
                            result = result.Substring(dem, result.Length - dem);

                            if (result.IndexOf("\r\n") >= 0)
                            {
                                dem = result.IndexOf("\r\n");
                                MSGContent = result.Substring(0, dem);
                                result = result.Substring(dem, result.Length - dem);
                            }

                            Connection con = new Connection();
                            DataTable temp;
                            temp = con.FillDataTableSQL(Common.ConStr, "SELECT MSGID = MAX(MSGID) FROM SMS_MessageIn");
                            if (temp.Rows[0].ItemArray[0].ToString() != "")
                            {
                                MSGId = temp.Rows[0].ItemArray[0].ToString();
                            }
                            else
                            {
                                MSGId = "1000000000";
                            }
                            MSGId = (Convert.ToInt64(MSGId) + 1).ToString();
                            Prefix = MSGId;

                            MSGContent = MSGContent.ToUpper();
                            //add to queue
                            string message = messageid + "|" + ReceivedPhone + "|" + MSGContent;
                            QueueMsg insertitem = new QueueMsg();
                            insertitem.content = message;
                            insertitem.timeinsert = DateTime.Now;
                            int timeoutmessage = int.Parse(ConfigurationManager.AppSettings["timeoutDuplicateMessageinMinute"]);
                            //DateTime datecompare = DateTime.Now;
                            if (queueMessage.items.Count.Equals(0))
                            {
                                queueMessage.Enqueue(insertitem);
                            }
                            else
                            {
                                bool found = false;
                                DateTime dateold = DateTime.Now;
                                //Utility.ProcessLog.LogInformation("======3");
                                foreach (var item in queueMessage.items)
                                {
                                    if (item.content.Equals(insertitem.content))
                                    {
                                        found = true;
                                        dateold = item.timeinsert;
                                        break;
                                    }
                                    else
                                        found = false;
                                }
                                if (found)
                                {
                                    Utility.ProcessLog.LogInformation("=========Total messages in queue is " + queueMessage.items.Count.ToString());
                                    Utility.ProcessLog.LogInformation("=========Duplicate message " + message);
                                    goto Next;
                                    //if (Convert.ToInt32((insertitem.timeinsert - dateold).Minutes) < timeoutmessage)
                                    //{
                                    //    Utility.ProcessLog.LogInformation("=========Duplicate message " + message);
                                    //    goto Next;
                                    //}
                                    //else //over timeout>>> delete all messagein
                                    //{
                                    //    //check recent delete all messages
                                    //    Utility.ProcessLog.LogInformation("=========Duplicate message excess timeout" + message);
                                    //    if (Convert.ToInt32((DateTime.Now - lastestdeleteallmessagetime).Minutes) < timeoutmessage)
                                    //    {
                                    //        goto Next;
                                    //    }
                                    //    else
                                    //    {
                                    //        lastestdeleteallmessagetime = DateTime.Now;


                                    //        Utility.ProcessLog.LogInformation("=========Duplicate message" + message + " is over timeout->>>will delete all messages ");


                                    //        bool ret = (bool)type.InvokeMember("DeleteAllMsgIn", System.Reflection.BindingFlags.InvokeMethod, null, instance, new object[] { (SerialPort)port });
                                    //        Utility.ProcessLog.LogInformation("=========ret value after delete all " + ret.ToString());
                                    //        if (ret) //free queue
                                    //        {
                                    //            queueMessage.items.Clear();
                                    //        }
                                    //        result = string.Empty; //delete all current messages.
                                    //        break;
                                    //    }
                                    //}
                                }
                                else
                                    queueMessage.Enqueue(insertitem);

                                
                            }
                            while (MSGContent.Contains("  "))
                                MSGContent = MSGContent.Replace("  ", " ");
                            while (MSGContent.Substring(0, 1) == " ")
                                MSGContent = MSGContent.Substring(1, MSGContent.Length - 1);
                            while (MSGContent.Substring(MSGContent.Length - 1, 1) == " ")
                                MSGContent = MSGContent.Substring(0, MSGContent.Length - 1);

                            object[] objPara = new object[5];
                            objPara[0] = MSGId;
                            objPara[1] = ReceivedPhone;
                            objPara[2] = SendPhone;
                            objPara[3] = "PNB " + MSGContent;
                            objPara[4] = "0";
                            Hashtable input = new Hashtable();
                            input.Add("IPCTRANCODE", "IB000500");
                            input.Add("SOURCEID", "IB");

                            input.Add("STORENAME", "SMS_MESSAGEIN_INSERT");
                            input.Add("PARA", objPara);
                            Hashtable output = autoTrans.ProcessTransHAS(input);
                        Next:
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                            EventLog.WriteEntry("Error get message, error desc:" + ex.Message);
                        }
                        catch (Exception sex)
                        {
                        }
                    }
                    finally
                    {
                        //FREE STATUS PORT
                        DataTable dtportstatusupdate = new DataTable();
                        dtportstatusupdate = dbObj.FillDataTable(Common.ConStr, "SMS_GSM_GETPORT_UPDATESTATUS", PortName, 0, "IN");
                        Thread.Sleep(sleept);
                    }

                }
            }
            catch (Exception e)
            {
                Utility.ProcessLog.LogError(e, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                EventLog.WriteEntry("Error get message, error desc:" + e.Message);
                GSMGetMsgInSMS(GROUPID);
            }
        }

    }
}
