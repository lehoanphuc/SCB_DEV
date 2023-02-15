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
using System.IO.Ports;


namespace SMSProcessMsgMT
{
    public partial class SMSSendMTMsg : ServiceBase
    {
        static string PortTwoWay = string.Empty;  // port for message 2 chieu
        private bool monitor = true;
        //static Thread thread = null;
        List<Thread> listThread = null;
        private Hashtable smsData;
        Assembly assembly = null;

        ServiceController sendMT = null;

        Assembly assemblySML = null;
        Type typeSML = null;
        object instanceSML = null;
        string methodSML = string.Empty;


        public SMSSendMTMsg()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                listThread = new List<Thread>();
                monitor = true;
                sendMT = new ServiceController("SMSSendMTMsg");
                Common.ConStr = ConfigurationManager.ConnectionStrings["ConnectionHost"].ToString();
                Common.ConStr = Common.DecryptData(Common.ConStr);
                Connection con = new Connection();
                //load sms sequence for sms gateway
                DataTable dtsequence = new DataTable();
                dtsequence = con.FillDataTable(Common.ConStr, "SMS_GET_SEQUENCE");
                if (dtsequence.Rows.Count == 0)
                {
                    Utility.ProcessLog.LogInformation("ERROR SMS SENDMTMSG CAN'T GET SMS SEQUENCE");
                }
                else
                {
                    Common.SMSSequence = Convert.ToInt32(dtsequence.Rows[0]["SequenceNo"].ToString());
                    if (Common.SMSSequence >= 99999999)
                        Common.SMSSequence = 1;
                }


                //for load balancing:
                DataTable dtrouter = new DataTable();

                dtrouter = con.FillDataTable(Common.ConStr, "SMS_GETROUTER");
                if (dtrouter.Rows.Count == 0)
                {
                    Utility.ProcessLog.LogInformation("ERROR SMS SENDMTMSG CAN'T GET SMS ROUTER");
                }
                else
                {
                    foreach (DataRow r in dtrouter.Rows)
                    {
                        //Thread thread = new Thread(() => SendMTMessageSMS(r["GROUPID"].ToString()));
                        Thread thread = new Thread(() => SendMTMessageSMS(r["GROUPID"].ToString()));
                        //thread.IsBackground = true;
                        listThread.Add(thread);
                        thread.Start();


                    }
                    // //start thread
                    //for (int i = 0; i < listThread.Count; i++)
                    //{
                    //    listThread[i].Start();

                    //}


                }

                //thread = new Thread(new ThreadStart(SendMTMessage));
                //thread.Start();
                EventLog.WriteEntry("Start SMS send MT service successful");
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                EventLog.WriteEntry("Error start. Error:" + ex.Message);
                OnStop();
            }
        }

        protected override void OnStop()
        {

            try
            {
                //FREE STATUS PORT
                Connection dbObj = new Connection();
                DataTable dtportstatusupdate = new DataTable();
                dtportstatusupdate = dbObj.FillDataTable(Common.ConStr, "SMS_GSM_GETPORT_UPDATESTATUS", PortTwoWay, 0, "OUT");

                EventLog.WriteEntry("Stop SMS send MT service.");
                monitor = false;
                //thread.Abort();
                //Thread.ResetAbort();
                //this.Stop();
                //sendMT.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0,60));
                for (int i = 0; i < listThread.Count; i++)
                {
                    listThread[i].Abort();

                }
                sendMT.Stop();
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation("Thread stop error " + ex.Message);
            }
        }
        private void LoadInfo(string supplierid, ref Type type, ref string method, ref object instance)
        {
            try
            {
                Common.ConStr = ConfigurationManager.ConnectionStrings["ConnectionHost"].ToString();
                Common.ConStr = Common.DecryptData(Common.ConStr);
                DataTable SMSSupplier = new DataTable();
                Connection con = new Connection();
                //SMSSupplier = con.FillDataTable(Common.ConStr, "SMS_GETSMSSUPPLIER");
                SMSSupplier = con.FillDataTable(Common.ConStr, "SMS_GETSMSSUPPLIER_BYID", supplierid);
                assembly = Assembly.LoadFrom(SMSSupplier.Rows[0]["ASSEMBLYNAME"].ToString());
                type = assembly.GetType(SMSSupplier.Rows[0]["ASSEMBLYTYPE"].ToString());
                method = SMSSupplier.Rows[0]["MTMETHOD"].ToString();
                instance = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
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
        private void LoadInfoSML()
        {
            try
            {
                Common.ConStr = ConfigurationManager.ConnectionStrings["ConnectionHost"].ToString();
                Common.ConStr = Common.DecryptData(Common.ConStr);
                DataTable SMSSupplier = new DataTable();
                Connection con = new Connection();
                SMSSupplier = con.FillDataTable(Common.ConStr, "SMS_GETSMSSUPPLIERSML");
                assemblySML = Assembly.LoadFrom(SMSSupplier.Rows[0]["ASSEMBLYNAME"].ToString());
                typeSML = assemblySML.GetType(SMSSupplier.Rows[0]["ASSEMBLYTYPE"].ToString());
                methodSML = SMSSupplier.Rows[0]["MTMETHOD"].ToString();
                instanceSML = Activator.CreateInstance(typeSML);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
        private void SendMTMessageSMS(string GROUPID)
        {
            try
            {
                // MOI THREAD LOAD ASEMBLY KHAC NHAU

                Type type = null;
                object instance = null;
                string method = string.Empty;
                //moi thread se dung 1 supplierid
                string SupplierID = string.Empty;
                List<ListPort> listport = new List<ListPort>();
                List<ListPort> listportAll = new List<ListPort>(); // for all port
                List<ListPort> listportOne = new List<ListPort>();// port for process 
                //List<bool> Flagport = new List<bool>();
                Connection dbObj = new Connection();
                DataTable lstSMS = new DataTable();
                //get supplier ok
                DataTable dtsupplier = new DataTable();
                dtsupplier = dbObj.FillDataTable(Common.ConStr, "SMS_GET_SUPPLIER_BYGROUPID", GROUPID);
                if (dtsupplier.Rows.Count == 0) //ko tim thay supplier
                {
                    Utility.ProcessLog.LogInformation("cannot get supplier from groupid " + GROUPID);
                }
                else
                {
                    SupplierID = dtsupplier.Rows[0]["SUPPLIERID"].ToString();
                    LoadInfo(SupplierID, ref type, ref method, ref instance);
                }

                //get port for gsm ok
                if (!GROUPID.Equals("GROUP1"))
                {
                    DataTable dtport = new DataTable();
                    dtport = dbObj.FillDataTable(Common.ConStr, "SMS_GSM_GETPORT", GROUPID);
                    if (dtport.Rows.Count == 0)
                    {
                        Utility.ProcessLog.LogInformation("ERROR SMS SENDMTMSG GET PORT FAILSE");

                    }
                    else
                    {
                        foreach (DataRow r in dtport.Rows)
                        {
                            SerialPort port = new SerialPort();
                            port = new SerialPort(r["COMPORT"].ToString(), int.Parse(r["BAUDRATE"].ToString()), Parity.None, int.Parse(r["DATABITS"].ToString()), StopBits.One);
                            //port.NewLine = r["NEWLINE"].ToString();
                            //port.WriteTimeout = int.Parse(r["WRITETIMEOUT"].ToString());
                            //port.ReadTimeout = int.Parse(r["READTIMEOUT"].ToString());
                            listport.Add(new ListPort { port = port, Flag = true });
                            if ((bool)r["ISTWOWAYMSG"])
                            {
                                PortTwoWay = r["COMPORT"].ToString();
                                listportOne.Add(new ListPort { port = port, Flag = true });
                                ProcessLog.LogInformation("=========getport: list port one include " + r["COMPORT"].ToString());

                            }
                        }
                        listportAll = listport;
                    }
                }
                while (monitor)
                {
                    try
                    {

                        //goto SLEEP;
                        int loadsmsdelay = int.Parse(ConfigurationManager.AppSettings["loadsmsdelay"].ToString());
                        int checkflagdelay = int.Parse(ConfigurationManager.AppSettings["checkflagdelay"].ToString());
                        lstSMS = dbObj.FillDataTable(Common.ConStr, "SMS_GETMSGOUT_BYROUTERID", GROUPID);
                        //object result = null;
                        foreach (DataRow row in lstSMS.Rows)
                        {
                            //04/02/2017 can nghien cuu lai doan nay
                            ////27.12.2016 hardcode to force message income just send in port two ways:
                            //if (!GROUPID.Equals("GROUP1"))
                            //{
                            //    if (row["IPCTRANCODE"].ToString().Trim().Equals("SMS00500"))
                            //        listport = listportOne;
                            //    else
                            //        listport = listportAll;
                            //}

                            // Thread.Sleep(100);
                            Hashtable msg = new Hashtable();
                            string errorCode = string.Empty;
                            string errorDesc = string.Empty;
                            foreach (DataColumn col in lstSMS.Columns)
                            {
                                msg.Add(col.ColumnName, row[col.ColumnName]);
                            }
                            //check phone

                            if (!PhoneNoFormatValid(msg["RECEIVEDPHONE"].ToString()))
                            {
                                dbObj.ExecuteNonquery(Common.ConStr, "SMS_MESSAGEOUT_UPDATE", msg["ID"].ToString(), "I", errorCode, errorDesc, msg["RECEIVEDPHONE"].ToString());
                            }
                            else
                            {
                                //NEU LA SMS GATEWAY GUI LUON
                                if (SupplierID.Equals("SMSGW"))
                                {
                                    //add phone and country prefix to hashtable
                                    foreach (DataRow r in dtsupplier.Rows)
                                    {


                                        foreach (DataColumn col in dtsupplier.Columns)
                                        {
                                            msg.Add(col.ColumnName, r[col.ColumnName]);

                                        }
                                    }
                                    // ADD TRANCODE TO HASHTABLE
                                    DataTable dtinputgw = new DataTable();
                                    dtinputgw = dbObj.FillDataTable(Common.ConStr, "SMS_GET_SUPPLIER_INRO", SupplierID);
                                    foreach (DataRow r in dtinputgw.Rows)
                                    {


                                        foreach (DataColumn col in dtinputgw.Columns)
                                        {
                                            msg.Add(col.ColumnName, r[col.ColumnName]);

                                        }
                                    }
                                    //LoadInfo(SupplierID);
                                    //SendMTMessageThread(Type type,string method,object instance,Hashtable msg)
                                    List<ListPort> temp = new List<ListPort>();
                                    //SendMTMessageThread(type, method, instance, msg, ref a);
                                    // SendMTMessageThread(type, method, instance, msg, 0, ref temp,0);
                                    int gwinterval = int.Parse(ConfigurationManager.AppSettings["smsgwinterval"].ToString());
                                    Thread thread = new Thread(() => SendMTMessageThread(type, method, instance, msg, 0, ref temp, gwinterval));
                                    // thread.IsBackground = true;
                                    Thread.Sleep(gwinterval);
                                    listThread.Add(thread);
                                    thread.Start();

                                }
                                else // NEU LA GSM GUI SONG SONG QUA CAC PORT
                                {
                                    //check port neu port nao ranh-> gui qua port do
                                    bool portready = false;
                                    while (!portready)
                                    {
                                        bool breakwhile = false;
                                        for (int i = 0; i < listport.Count; i++)
                                        {
                                            //force trancode to specific com
                                            DataTable dttrancode = new DataTable();
                                            dttrancode = dbObj.FillDataTable(Common.ConStr, "SMS_GSM_GETPORT_BYTRANCODE", row["IPCTRANCODE"].ToString().Trim());
                                            if (dttrancode.Rows.Count == 0)
                                            {
                                                //Utility.ProcessLog.LogInformation(">>>SMS MESSAGE NOT FORCE TO PORT");

                                            }
                                            else
                                            {
                                                if (!listport[i].port.PortName.Equals(dttrancode.Rows[0]["COMPORT"].ToString().Trim()))
                                                {
                                                    continue;
                                                }
                                                else
                                                {
                                                    Utility.ProcessLog.LogInformation("======" + listport[i].port.PortName.ToString() + " is used for trancode " + row["IPCTRANCODE"].ToString().Trim());
                                                }


                                            }
                                            if (listport[i].Flag)
                                            {
                                                listport[i].Flag = false;
                                                //CHECK PORT AVAILABLE:
                                                bool portExists = SerialPort.GetPortNames().Any(x => x == listport[i].port.PortName);
                                                if (!portExists)
                                                {
                                                    Utility.ProcessLog.LogInformation("Port not exists. Please check ====" + listport[i].port.ToString());
                                                    Thread.Sleep(100);
                                                    listport[i].Flag = true;
                                                    continue;
                                                }

                                                //neu la port 2 chieu se check va update status trc khi xu ly
                                                if (listport[i].port.PortName.Equals(PortTwoWay))
                                                {
                                                    bool isportbusy = true;
                                                    while (isportbusy)
                                                    {
                                                    N: 
                                                        DataTable dtportstatus = new DataTable();
                                                        dtportstatus = dbObj.FillDataTable(Common.ConStr, "SMS_GSM_GETPORT_DETAIL", PortTwoWay, "OUT");
                                                        isportbusy = (bool)(dtportstatus.Rows[0]["BUSY"]);
                                                        if (isportbusy)
                                                        {
                                                            Thread.Sleep(checkflagdelay);
                                                        }
                                                        else
                                                        {
                                                            //dtportstatus = dbObj.FillDataTable(Common.ConStr, "SMS_GSM_GETPORT_UPDATESTATUS", PortTwoWay, 1, "OUT");
                                                            int queryResult = 0; // for check duplicate update by multithread
                                                            queryResult = dbObj.ExecuteNonquery(Common.ConStr, "SMS_GSM_GETPORT_UPDATESTATUS", PortTwoWay, 1, "OUT");
                                                            //Utility.ProcessLog.LogInformation("===============update port <<<OUT>>> with result effect rows= " + queryResult.ToString());
                                                            if (queryResult <= 0)
                                                            {
                                                                Utility.ProcessLog.LogInformation("===============update port <<<OUT>>> with result effect rows= " + queryResult.ToString());
                                                                Thread.Sleep(checkflagdelay);
                                                                goto N;


                                                            }
                                                        }

                                                    }

                                                }





                                                //send message
                                                msg.Add("PORTSMS", listport[i].port);
                                                int gsminterval = int.Parse(ConfigurationManager.AppSettings["gsminterval"].ToString());
                                                Thread thread = new Thread(() => SendMTMessageThread(type, method, instance, msg, i, ref listport, 200));
                                                //  thread.IsBackground = true;
                                                Thread.Sleep(gsminterval);
                                                listThread.Add(thread);
                                                thread.Start();
                                                portready = true;
                                                breakwhile = true;
                                                break;
                                            }
                                        }
                                        if (!breakwhile)
                                        {
                                            Thread.Sleep(100);
                                        }

                                    }



                                }
                            }
                        }
                    SLEEP:
                        Thread.Sleep(loadsmsdelay);
                        //Thread.ResetAbort();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                            EventLog.WriteEntry("Error send MT message, error desc:" + ex.Message);
                        }
                        catch (Exception sex)
                        {
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Utility.ProcessLog.LogError(e, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                EventLog.WriteEntry("Error send MT message, error desc:" + e.Message);
                SendMTMessageSMS(GROUPID);
            }
        }
        private static void SendMTMessageThread(Type type, string method, object instance, Hashtable msg, int portno, ref List<ListPort> listport, int timesleep)
        {
            Connection dbObj = new Connection();
            try
            {
                if (listport.Count >= 1)
                {
                    Utility.ProcessLog.LogInformation("===============start thread on port " + listport[portno].port.PortName);
                }

                object result = null;
                string errorCode = string.Empty;
                string errorDesc = string.Empty;

                result = type.InvokeMember(method, System.Reflection.BindingFlags.InvokeMethod, null, instance, new object[] { msg });
                //}

                if ((bool)result == true)
                {
                    dbObj.ExecuteNonquery(Common.ConStr, "SMS_MESSAGEOUT_UPDATE", msg["ID"].ToString(), "Y", "0", "", msg["RECEIVEDPHONE"].ToString());
                }
                else
                {
                    try
                    {
                        errorCode = msg[Common.KEYNAME.ERRORCODE].ToString();
                        errorDesc = msg[Common.KEYNAME.ERRORDESC].ToString();
                    }
                    catch { }
                    switch (errorCode)
                    {
                        case "9901":
                            dbObj.ExecuteNonquery(Common.ConStr, "SMS_MESSAGEOUT_UPDATE", msg["ID"].ToString(), "I", errorCode, errorDesc, msg["RECEIVEDPHONE"].ToString());
                            break;
                        case "9900":
                            dbObj.ExecuteNonquery(Common.ConStr, "SMS_MESSAGEOUT_UPDATE", msg["ID"].ToString(), "F", errorCode, errorDesc, msg["RECEIVEDPHONE"].ToString());
                            break;
                        default:
                            dbObj.ExecuteNonquery(Common.ConStr, "SMS_MESSAGEOUT_UPDATE", msg["ID"].ToString(), "F", errorCode, errorDesc, msg["RECEIVEDPHONE"].ToString());
                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation("send thread GSM message error " + ex.Message);
                //update lai message de port khac xu ly
                //Connection dbObj = new Connection();
                dbObj.ExecuteNonquery(Common.ConStr, "SMS_MESSAGEOUT_UPDATE", msg["ID"].ToString(), "N", "9999", "ReProcess", msg["RECEIVEDPHONE"].ToString());
                if (listport.Count >= 1)
                {
                    Utility.ProcessLog.LogInformation("send thread GSM message error on port " + listport[portno].port.PortName);
                }
            }
            finally
            {
                //giai phong port
                Thread.Sleep(timesleep);
                if (listport.Count >= 1)
                {
                    listport[portno].Flag = true;
                    Utility.ProcessLog.LogInformation("===status port " + listport[portno].port.PortName + "is " + listport[portno].Flag.ToString());
                    ////FREE STATUS PORT 2 chieu
                    if (listport[portno].port.PortName.Equals(PortTwoWay))
                    {
                        Utility.ProcessLog.LogInformation("===update db to free port " + listport[portno].port.PortName);
                        DataTable dtportstatusupdate = new DataTable();
                        dtportstatusupdate = dbObj.FillDataTable(Common.ConStr, "SMS_GSM_GETPORT_UPDATESTATUS", PortTwoWay, 0, "OUT");
                    }
                }

            }
        }
    }
    public partial class ListPort
    {
        public SerialPort port { get; set; }
        public bool Flag { get; set; }

    }
}
