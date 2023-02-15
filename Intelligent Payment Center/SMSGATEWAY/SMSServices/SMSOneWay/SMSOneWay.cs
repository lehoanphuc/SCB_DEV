using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Collections;
using SMSUtility;
using DBConnection;

namespace SMSOneWay
{
    public partial class SMSOneWay : ServiceBase
    {
        static ITransaction.AutoTrans autoTrans;
        private Thread thread;
        private Thread thrGetAutoBalance;
        private Thread thrAutoHappy;

        private bool isRunning = true;
        Connection con = new Connection();
        private string ConStr = "";
        private Thread thRegNotify;
        private Thread thGetNotify;
        private Thread thSendNotify;
        bool isExistData = false;
        int maxrecord = 10;

        public SMSOneWay()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartServer();

            bool isRegSMSNotify = bool.Parse(ConfigurationManager.AppSettings["RegSMSNotify"].ToString());
            bool isGetSMSNotify = bool.Parse(ConfigurationManager.AppSettings["GetSMSNotify"].ToString());
            bool isSendSMSNotify = bool.Parse(ConfigurationManager.AppSettings["SendSMSNotify"].ToString());

            if (isRegSMSNotify)
            {
                thRegNotify = new Thread(new ThreadStart(RegisterNotification));
                thRegNotify.Start();
                Utility.ProcessLog.LogInformation("[Service] Register Notification started");
            }

            if (isGetSMSNotify)
            {
                thGetNotify = new Thread(new ThreadStart(GetNotification));
                thGetNotify.Start();
                Utility.ProcessLog.LogInformation("[Service] Get Notification started");
            }

            if (isSendSMSNotify)
            {
                thSendNotify = new Thread(new ThreadStart(SendNotification));
                thSendNotify.Start();
                Utility.ProcessLog.LogInformation("[Service] Send Notification started");
            }

            #region oldverion
            //bool isSTB = false;
            //try
            //{
            //    isSTB = bool.Parse(ConfigurationManager.AppSettings["isSTB"].ToString());
            //}
            //catch
            //{ }

            //if (isSTB)
            //{
            //    thrGetAutoBalance = new Thread(new ThreadStart(GetAutoBalanceSTB));
            //    thrGetAutoBalance.Start();
            //    Utility.ProcessLog.LogInformation("GetAutoBalanceSTB started");
            //}
            //else
            //{
            //    thrGetAutoBalance = new Thread(new ThreadStart(GetAutoBalance));
            //    thrGetAutoBalance.Start();
            //    Utility.ProcessLog.LogInformation("GetAutoBalanceAYA started");
            //}

            //thread = new Thread(new ThreadStart(AutoBalance));
            //thread.Start();
            #endregion
        }

        protected override void OnStop()
        {
            StopServer();
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
                isRunning = true;

                ConStr = ConfigurationManager.ConnectionStrings["ConnectionHost"].ToString();
                ConStr = Common.DecryptData(ConStr);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

        }
        private void StopServer()
        {
            try
            {
                Utility.ProcessLog.LogInformation("[Service] SMS Oneway stopping");
                autoTrans = null;
                isRunning = false;
                if(thread != null)
                    thread.Abort();
                if (thrGetAutoBalance != null)
                    thrGetAutoBalance.Abort();
                if (thRegNotify != null)
                    thRegNotify.Abort();
                if (thSendNotify != null)
                    thSendNotify.Abort();
                if (thGetNotify != null)
                    thGetNotify.Abort();
                Utility.ProcessLog.LogInformation("[Service] SMS Oneway stopped");
            }
            catch(Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            
        }

        private void RegisterNotification()
        {
            int sleeptime = int.Parse(ConfigurationManager.AppSettings["REGNO_SLEEP_TIME"].ToString());

            try
            {
                while (isRunning)
                {
                    try
                    {
                        RunRegisterNotification();
                        Thread.Sleep(sleeptime);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (isRunning) Utility.ProcessLog.LogInformation("Register notification error : " + ex.ToString());
                        }
                        catch (Exception exex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (isRunning) Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void GetNotification()
        {
            int sleeptime = int.Parse(ConfigurationManager.AppSettings["GETNO_SLEEP_TIME"].ToString());

            try
            {
                while (isRunning)
                {
                    try
                    {
                        isExistData = false;
                        RunGetNotification();
                        if(!isExistData)
                        {
                            Thread.Sleep(sleeptime);
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if(isRunning) Utility.ProcessLog.LogInformation("Get notification error : " + ex.ToString());
                        }
                        catch (Exception exex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (isRunning) Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void SendNotification()
        {
            int sleeptime = int.Parse(ConfigurationManager.AppSettings["SENDNO_SLEEP_TIME"].ToString());

            try
            {
                while (isRunning)
                {
                    try
                    {
                        try
                        {
                            RunSendNotification();
                        }
                        catch { }
                        Thread.Sleep(sleeptime);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (isRunning) Utility.ProcessLog.LogInformation("Get notification error : " + ex.ToString());
                        }
                        catch (Exception exex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (isRunning) Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void RunRegisterNotification()
        {
            try
            {
                DataTable dt = con.FillDataTable(ConStr, "EBA_SMSNOTIFYDETAILS_SELECT", new object[] { });

                foreach(DataRow dr in dt.Rows)
                {
                    Hashtable input = new Hashtable();
                    input.Add(Common.KEYNAME.IPCTRANCODE, "SMS00030");
                    input.Add(Common.KEYNAME.SOURCEID, "SMS");
                    input.Add(Common.KEYNAME.TRANDESC, "Register sms notification");
                    foreach(DataColumn dc in dt.Columns)
                    {
                        input.Add(dc.ColumnName, dr[dc].ToString());
                    }
                    Hashtable output = autoTrans.ProcessOnlyHAS(input);

                    DataTable dts = con.FillDataTable(ConStr, "EBA_SMSNOTIFYDETAILS_UPDATERESULT", new object[3] { input["ID"].ToString(), output["IPCERRORCODE"].ToString(), (output["IPCTRANSID"] == null) ? "" : output["IPCTRANSID"].ToString() });

                    if (!output[Common.KEYNAME.IPCERRORCODE].Equals("0"))
                    {
                        Utility.ProcessLog.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + output[Common.KEYNAME.IPCERRORDESC].ToString());
                    }
                    else
                    {
                        foreach(DataRow drsms in dts.Rows)
                        {
                            Hashtable inputsms = new Hashtable();
                            inputsms.Add(Common.KEYNAME.IPCTRANCODE, "SMS00033");
                            inputsms.Add(Common.KEYNAME.SOURCEID, "SMS");
                            inputsms.Add(Common.KEYNAME.TRANDESC, "Send SMS Alert");
                            inputsms.Add(Common.KEYNAME.ACCOUNTNO, drsms["ACCTNO"].ToString());
                            inputsms.Add(Common.KEYNAME.USERID, drsms["USERID"].ToString());
                            inputsms.Add(Common.KEYNAME.PhoneNo, drsms["PHONENO"].ToString());
                            inputsms.Add(Common.KEYNAME.DESTID, "PNB");
                            Hashtable outputsms = autoTrans.ProcessTransHAS(inputsms);
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return;
        }
        private void RunGetNotification()
        {
            try
            {
                int.TryParse(ConfigurationManager.AppSettings["GETNO_SLEEP_TIME"].ToString(), out maxrecord);

                Hashtable input = new Hashtable();
                input.Add(Common.KEYNAME.IPCTRANCODE, "SMS00031");
                input.Add(Common.KEYNAME.SOURCEID, "SMS");
                input.Add(Common.KEYNAME.MAXRECORD, 10);
                input.Add(Common.KEYNAME.TRANDESC, "Get sms notification");

                Hashtable output = autoTrans.ProcessOnlyHAS(input);

                if (!output[Common.KEYNAME.IPCERRORCODE].Equals("0"))
                {
                    Utility.ProcessLog.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + output[Common.KEYNAME.IPCERRORDESC].ToString());
                }
                else
                {
                    if(output.ContainsKey(Common.KEYNAME.AUTOBALANCEDATA))
                    {
                        InsertNotificationData((DataSet)output[Common.KEYNAME.AUTOBALANCEDATA]);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void RunSendNotification()
        {
            try
            {
                Hashtable input = new Hashtable();
                input.Add(Common.KEYNAME.IPCTRANCODE, "SMS00032");
                input.Add(Common.KEYNAME.SOURCEID, "SMS");
                input.Add(Common.KEYNAME.TRANDESC, "Send sms notification");

                Hashtable output = autoTrans.ProcessTransHAS(input);

                if (!output[Common.KEYNAME.IPCERRORCODE].Equals("0"))
                {
                    Utility.ProcessLog.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + output[Common.KEYNAME.IPCERRORDESC].ToString());
                }

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void InsertNotificationData(DataSet ds)
        {
            DataTable dtAcct = new DataTable();
            try
            {
                if(ds.Tables.Count>0)
                {
                    dtAcct = ds.Tables[0];
                }
                else
                {
                    return;
                }

                isExistData=(dtAcct.Rows.Count >=maxrecord)?true:false;

                for (int i = 0; i < dtAcct.Rows.Count; i++)
                {
                    //strDesc = dtAcct.Rows[i]["DESC"].ToString().Replace('#', ' ');
                    try
                    {
                        con.ExecuteNonquery(ConStr, "SMS_NOTIFICATION_INSERT",
                            GetDataFromDataRow(dtAcct.Rows[i],"ID"),
                            GetDataFromDataRow(dtAcct.Rows[i],"TRANREF"),
                            GetDataFromDataRow(dtAcct.Rows[i],"ACCTNO"),
                            GetDataFromDataRow(dtAcct.Rows[i],"AMOUNT"),
                            GetDataFromDataRow(dtAcct.Rows[i],"DESCRIPTION"),
                            GetDataFromDataRow(dtAcct.Rows[i],"TRANDATETIME"),
                            GetDataFromDataRow(dtAcct.Rows[i],"BALANCE"),
                            GetDataFromDataRow(dtAcct.Rows[i],"MAMT"),
                            GetDataFromDataRow(dtAcct.Rows[i],"CCYID"),
                            GetDataFromDataRow(dtAcct.Rows[i],"CUSTINFO"),
                            GetDataFromDataRow(dtAcct.Rows[i],"DUEDATE"),
                            GetDataFromDataRow(dtAcct.Rows[i],"NUMTRAN"),
                            GetDataFromDataRow(dtAcct.Rows[i],"TRANINFOR"),
                            GetDataFromDataRow(dtAcct.Rows[i],"CHAR01"),
                            GetDataFromDataRow(dtAcct.Rows[i],"CHAR02"),
                            GetDataFromDataRow(dtAcct.Rows[i],"CHAR03"),
                            GetDataFromDataRow(dtAcct.Rows[i],"CHAR04"),
                            GetDataFromDataRow(dtAcct.Rows[i],"CHAR05"),
                            GetDataFromDataRow(dtAcct.Rows[i],"CHAR06"),
                            GetDataFromDataRow(dtAcct.Rows[i],"CHAR07"),
                            GetDataFromDataRow(dtAcct.Rows[i],"CHAR08"),
                            GetDataFromDataRow(dtAcct.Rows[i],"CHAR09"),
                            GetDataFromDataRow(dtAcct.Rows[i],"CHAR10"),
                            GetDataFromDataRow(dtAcct.Rows[i],"TRANCODE")
                            );


                    }
                    catch (Exception ex)
                    {
                        Utility.ProcessLog.LogInformation("InsertAutoBalance error: " + ex.ToString());
                    }
                }

                //update start record
                int lastrecord = Convert.ToInt32(dtAcct.Compute("max(ID)", string.Empty)) + 1;
                UpdateLastRecord(lastrecord);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void UpdateLastRecord(int lastrecord)
        {
            try
            {
                con.ExecuteNonquery(ConStr, "IPCSYSVAR_UPDATE", "SMSNOSTARTRECORD", lastrecord.ToString());
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private string GetDataFromDataRow(DataRow dr,string key)
        {
            try
            {
                return dr[key].ToString();
            }
            catch
            {
                return "";
            }
        }

        #region old version
        private void AutoBalance()
        {
            try
            {
                while (isRunning)
                {
                    try
                    {
                        runAutoBalance();
                        Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AUTOSENT_SLEEP_TIME"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            EventLog.WriteEntry("Autobalance. Error:" + ex.Message);
                        }
                        catch (Exception sex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void runAutoBalance()
        {
            try
            {
                Hashtable input = new Hashtable();
                input.Add(Common.KEYNAME.IPCTRANCODE, "SMS00004");
                input.Add(Common.KEYNAME.SOURCEID, "SMS");
                input.Add(Common.KEYNAME.TRANDESC, "THONG BAO THAY DOI SO DU");
                Hashtable output = autoTrans.ProcessOnlyHAS(input);
                if (!output[Common.KEYNAME.IPCERRORCODE].Equals("0"))
                {
                    Utility.ProcessLog.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + output[Common.KEYNAME.IPCERRORDESC].ToString());
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return;
        }
        private void GetAutoBalanceSTB()
        {
            int startRecord = int.Parse(ConfigurationManager.AppSettings["STARTRECORD"].ToString());
            int maxRecord = int.Parse(ConfigurationManager.AppSettings["MAXRECORD"].ToString());
            string errorCode = "0";
            string tranreflist = string.Empty;
            try
            {
                while (isRunning)
                {
                    try
                    {
                        try
                        {
                            tranreflist = "";
                            //GET AUTOBALANCE DATA FROM COREBANKING
                            Hashtable input = new Hashtable();
                            input.Add(Common.KEYNAME.IPCTRANCODE, "SMS00014");
                            input.Add(Common.KEYNAME.SOURCEID, "SMS");
                            input.Add(Common.KEYNAME.STARTRECORD, startRecord.ToString());
                            input.Add(Common.KEYNAME.MAXRECORD, maxRecord.ToString());
                            Hashtable output = autoTrans.ProcessOnlyHAS(input);

                            errorCode = output[Common.KEYNAME.IPCERRORCODE].ToString();
                            if (errorCode.Equals("0") && output[Common.KEYNAME.AUTOBALANCEDATA] != null)
                            {
                                if (output[Common.KEYNAME.AUTOBALANCEDATA].ToString().Contains("ERRORCODE=9999"))
                                {

                                }
                                else
                                {
                                    tranreflist = GetTranRef(output[Common.KEYNAME.AUTOBALANCEDATA].ToString());
                                }

                            }

                            //UPDATE AUTOBALANCE DATA ON COREBANKING
                            if (!string.IsNullOrEmpty(tranreflist))
                            {
                                Hashtable inputb = new Hashtable();
                                inputb.Add(Common.KEYNAME.IPCTRANCODE, "SMS00114");
                                inputb.Add(Common.KEYNAME.SOURCEID, "SMS");
                                inputb.Add(Common.KEYNAME.PARA, tranreflist);
                                Hashtable outputb = autoTrans.ProcessOnlyHAS(inputb);
                            }                            
                        }
                        catch { }

                        Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["SLEEP_TIME"].ToString()));             
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            EventLog.WriteEntry("GetAutoBalance. Error:" + ex.Message);
                        }
                        catch (Exception sex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return;
        }
        private string GetTranRef(string data)
        {
            string tranreflist=string.Empty;
            try
            {
                string[] tmpar1 = data.Split('&');
                foreach(string tmp1 in tmpar1)
                {
                    string[] tmpar2 = tmp1.Split('|');
                    foreach(string tmp2 in tmpar2)
                    {
                        if (tmp2.Contains("TRANREF"))
                        {
                            string[] tmpar3 = tmp2.Split('=');
                            tranreflist += tmpar3[1].ToString() + ",";
                        }
                    }
                }
                tranreflist = tranreflist.Substring(0,tranreflist.Length - 1);
            }
            catch(Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return "";
            }
            return tranreflist;
        }
        private void GetAutoBalance()
        {
            int startRecord = int.Parse(ConfigurationManager.AppSettings["STARTRECORD"].ToString());
            int maxRecord = int.Parse(ConfigurationManager.AppSettings["MAXRECORD"].ToString());
            int numberRecord = -1;
            int endRecord = -1;
            int lstartRecord = 1;
            bool existRecord = true;
            string errorCode = "0";
            try
            {
                startRecord = lstartRecord = getStartRecord();
                while (isRunning)
                {
                    try
                    {
                        while ((startRecord == lstartRecord || existRecord) && errorCode.Equals("0"))
                        {
                            Hashtable input = new Hashtable();
                            input.Add(Common.KEYNAME.IPCTRANCODE, "SMS00014");
                            input.Add(Common.KEYNAME.SOURCEID, "SMS");
                            input.Add(Common.KEYNAME.STARTRECORD, startRecord.ToString());
                            input.Add(Common.KEYNAME.MAXRECORD, maxRecord.ToString());
                            Hashtable output = autoTrans.ProcessOnlyHAS(input);

                            errorCode = output[Common.KEYNAME.IPCERRORCODE].ToString();
                            if (errorCode.Equals("0") && output[Common.KEYNAME.AUTOBALANCEDATA] != null)
                            {
                                if (output["AUTOBALANCEDATA"].ToString().Contains("ERRORCODE=9999"))
                                    break;

                                GetRecordInfo(output[Common.KEYNAME.AUTOBALANCEDATA].ToString(), ref endRecord, ref numberRecord);

                                existRecord = ((numberRecord == maxRecord + 1 && numberRecord > 0) || endRecord == -1) ? true : false;
                                if ((bool)output[Common.KEYNAME.ISNEXTDATE] && (!existRecord || endRecord == -1))
                                {
                                    startRecord = 1;
                                    UpdateAutoBalanceTime();
                                }
                                else if (endRecord != -1)
                                {
                                    startRecord = endRecord + 1;
                                }
                            }
                            else if ((bool)output[Common.KEYNAME.ISNEXTDATE])
                            {
                                existRecord = false;
                                startRecord = lstartRecord = 1;
                                UpdateAutoBalanceTime();
                            }
                            else
                            {
                                Utility.ProcessLog.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + output[Common.KEYNAME.IPCERRORDESC].ToString());
                            }
                        }
                        lstartRecord = startRecord;
                        Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["SLEEP_TIME"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            EventLog.WriteEntry("GetAutoBalance. Error:" + ex.Message);
                        }
                        catch (Exception sex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return;
        }
        private void AutoHappy()
        {
            try
            {
                while (isRunning)
                {
                    try
                    {
                        string runHappy = DateTime.Now.ToString("ddMMyyyyHHmm");
                        string datetimeHappy = DateTime.Now.ToString("ddMMyyyy") + ConfigurationManager.AppSettings["SETDATETIMEHAPPY"].ToString();
                        //edit by vu tran 15052014
                        runHappy = datetimeHappy;
                        if (runHappy.Equals(datetimeHappy))
                        {
                            Hashtable outputO = ActionHappy("O");
                            Hashtable outputP = ActionHappy("P");
                        }
                        Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["SLEEPHAPPY"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            EventLog.WriteEntry("AutoHappy. Error:" + ex.Message);
                        }
                        catch (Exception sex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return;
        }
        private Hashtable ActionHappy(string custtype)
        {
            Hashtable outputHappy = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                input.Add(Common.KEYNAME.IPCTRANCODE, "SMS00005");
                input.Add(Common.KEYNAME.SOURCEID, "SMS");
                input.Add(Common.KEYNAME.CUSTTYPE, custtype);
                //input.Add(Common.KEYNAME.TIMEINSERT, DateTime.Now.ToString("dd/MM/yyyy"));
                input.Add(Common.KEYNAME.TIMEINSERT, "22/05/2014");
                outputHappy = autoTrans.ProcessTransHAS(input);
                if (!outputHappy[Common.KEYNAME.IPCERRORCODE].Equals("0"))
                {
                    //Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["FORWAITINGHAPPY"].ToString()));
                    //Hashtable output = ActionHappy(custtype); 
                    Utility.ProcessLog.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + outputHappy[Common.KEYNAME.IPCERRORDESC].ToString());
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return outputHappy;
        }

        private void GetRecordInfo(string nRecord, ref int eRecord, ref int mRecord)
        {
            string e = "-1";
            string m = "-1";

            try
            {
                nRecord = nRecord.Substring(nRecord.LastIndexOf("&"), nRecord.Length - nRecord.LastIndexOf("&"));

                string[] arRecord = nRecord.Split('|');
                foreach (string i in arRecord)
                {
                    if (i.Contains(Common.KEYNAME.NUMBERRECORD))
                    {
                        string[] arsRecord = i.Split('=');
                        m = arsRecord[1];
                    }
                    if (i.Contains(Common.KEYNAME.ENDRECORD))
                    {
                        string[] arsRecord = i.Split('=');
                        e = arsRecord[1];
                    }
                }

                mRecord = Convert.ToInt32(m);
                eRecord = Convert.ToInt32(e);
            }
            catch
            {
                mRecord = -1;
                eRecord = -1;
            }
        }
        private void UpdateAutoBalanceTime()
        {
            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            object[] para = new object[] { };
            hasInput.Add(Common.KEYNAME.IPCTRANCODE, "IB000113");
            hasInput.Add(Common.KEYNAME.SOURCEID, "SEMS");

            hasInput.Add("SOURCETRANREF", "010");
            hasInput.Add("STORENAME", "IPCSYSVAR_UPDATE_AUTOBALANCETIME");
            hasInput.Add("PARA", para);

            hasOutput = autoTrans.ProcessTransHAS(hasInput);
            /*
            if (hasOutput[Common.KEYNAME.IPCERRORCODE].ToString() == "0")
                return true;
            else
                return false;
             */
        }
        private int getStartRecord()
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                object[] para = new object[] { };
                hasInput.Add(Common.KEYNAME.IPCTRANCODE, "IB000113");
                hasInput.Add(Common.KEYNAME.SOURCEID, "SEMS");

                hasInput.Add("SOURCETRANREF", "010");
                hasInput.Add("STORENAME", "IPCSYSVAR_SELECT");
                hasInput.Add("PARA", para);

                hasOutput = autoTrans.ProcessTransHAS(hasInput);

                if (hasOutput[Common.KEYNAME.IPCERRORCODE].ToString() == "0")
                {
                    DataSet ds = (DataSet)hasOutput["DATASET"];
                    string s = ds.Tables["Table"].Select("VARNAME='AUTOBALANCESRECORD'")[0][1].ToString();
                    string ad = ds.Tables["Table"].Select("VARNAME='AUTOBALANCETIME'")[0][1].ToString();
                    string wd = ds.Tables["Table"].Select("VARNAME='IPCWORKDATE'")[0][1].ToString();

                    if (ad != wd)
                        return 1;
                    else
                        return Convert.ToInt32(s);
                }
                else
                    return 1;
            }
            catch
            {
                return 1;
            }

        }
        #endregion
    }
}
