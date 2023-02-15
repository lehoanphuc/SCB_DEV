using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using SMSUtility;
using System.Threading;
using System.Configuration;
using DBConnection;

namespace SMSProcessMsgIn
{
    public partial class ProccessIn : ServiceBase
    {
        //static ITransaction.AutoTrans autoTrans;
        private Thread thread;
        private bool isRunning = true;
        private Connection con = null;

        public ProccessIn()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartServer();
            thread = new Thread(new ThreadStart(GetMsgSendIn));
            thread.Start();
        }


        protected override void OnStop()
        {
            StopServer();
        }

        private void GetMsgSendIn()
        {
            try
            {
                while (isRunning)
                {
                    try
                    {
                        DataTable dtmsgIn = con.FillDataTable(Utility.Common.ConStr, "SMS_MESSAGEIN_SELECT", Common.SMSSTATUS.BEGIN);
                        for (int i = 0; i < dtmsgIn.Rows.Count; i++)
                        {
                            Hashtable hasMsgIn = CreateHasMsgIn(dtmsgIn.Rows[i][Common.KEYNAME.MSGID].ToString(), dtmsgIn.Rows[i][Common.KEYNAME.RECEIVEDPHONE].ToString(), dtmsgIn.Rows[i][Common.KEYNAME.SENDPHONE].ToString(),
                                                    dtmsgIn.Rows[i][Common.KEYNAME.MSGCONTENT].ToString(), dtmsgIn.Rows[i][Common.KEYNAME.PREFIX].ToString(), dtmsgIn.Rows[i][Common.KEYNAME.MSGDATE].ToString(), dtmsgIn.Rows[i][Common.KEYNAME.MSGTIME].ToString(),
                                                    dtmsgIn.Rows[i][Common.KEYNAME.SYSDATE].ToString(), dtmsgIn.Rows[i][Common.KEYNAME.STATUS].ToString());
                            Lib objLib = new Lib();
                            bool isSend = objLib.SendMsgIn(hasMsgIn);
                            if (isSend)
                            {
                                con.ExecuteNonquery(Utility.Common.ConStr, "SMS_MESSAGEIN_UPDATE", dtmsgIn.Rows[i][Common.KEYNAME.MSGID].ToString(), Common.SMSSTATUS.WAITINGPROCESS);
                            }
                            else { }
                        }
                        Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["SLEEP_TIME"].ToString()));
                    }
                    catch(Exception ex)
                    {
                        try
                        {
                            EventLog.WriteEntry("Error SMS messageIN, error desc:" + ex.Message);
                        }
                        catch(Exception sex)
                        {
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private void StartServer()
        {
            try
            {
                Utility.Common.ConStr = ConfigurationManager.ConnectionStrings["ConnectionHost"].ToString();
                Utility.Common.ConStr = Utility.Common.DecryptData(Utility.Common.ConStr);
                isRunning = true;
                con = new Connection();
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            
        }
        private void StopServer()
        {
            //autoTrans = null;
            isRunning = false;
            con = null;
            thread.Abort();
        }
        private Hashtable CreateHasMsgIn(string msgid,string receivedPhone, string sendPhone, string msgContent,
                                            string preFix, string msgDate, string msgTime, string sysDate, string status)
        {
            Hashtable result = new Hashtable();
            try
            {
                result.Add(Common.KEYNAME.MSGID, msgid);
                result.Add(Common.KEYNAME.RECEIVEDPHONE, receivedPhone);
                result.Add(Common.KEYNAME.SENDPHONE, sendPhone);
                result.Add(Common.KEYNAME.MSGCONTENT, msgContent);
                result.Add(Common.KEYNAME.PREFIX, preFix);
                result.Add(Common.KEYNAME.MSGDATE, msgDate);
                result.Add(Common.KEYNAME.MSGTIME, msgTime);
                result.Add(Common.KEYNAME.SYSDATE, sysDate);
                result.Add(Common.KEYNAME.STATUS, status);

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
    }
}
