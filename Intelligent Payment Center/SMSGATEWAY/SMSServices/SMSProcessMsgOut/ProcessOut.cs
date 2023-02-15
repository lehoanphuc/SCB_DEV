using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using SMSUtility;
using System.Collections;
using System.Configuration;
using DBConnection;

namespace SMSProcessMsgOut
{
    public partial class ProcessOut : ServiceBase
    {
        //static ITransaction.AutoTrans autoTrans;
        private Thread thread;
        private bool isRunning = true;
        Connection con = null;

        public ProcessOut()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartServer();
            thread = new Thread(new ThreadStart(GetQueueOutSendMsgOut));
            thread.Start();
        }
        protected override void OnStop()
        {
            StopServer();
        }
        private void GetQueueOutSendMsgOut()
        {
            try
            {
                while (isRunning)
                {
                    try
                    {
                        int errorCode = 0;
                        Lib objLib = new Lib();
                        Hashtable hasQueueOut = objLib.GetMsgQueueOut(ref errorCode);
                        if (errorCode == 0 && hasQueueOut.Count > 0)
                        {
                            bool sendMsg = SendMsgOut(hasQueueOut[Common.KEYNAME.IPCTRANSID].ToString(), hasQueueOut[Common.KEYNAME.RECEIVEDPHONE].ToString(), hasQueueOut[Common.KEYNAME.SENDPHONE].ToString(),
                                                hasQueueOut[Common.KEYNAME.MSGID].ToString(), hasQueueOut[Common.KEYNAME.MSGCONTENT].ToString(), hasQueueOut[Common.KEYNAME.IPCWORKDATE].ToString(),
                                                hasQueueOut[Common.KEYNAME.PIORITY].ToString(), hasQueueOut[Common.KEYNAME.MSGTYPE].ToString());

                            if (sendMsg)
                            {
                            }
                            else
                            {

                            }
                        }
                        else { }
                        Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["SLEEP_TIME"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            EventLog.WriteEntry("Error SMS messageout, error desc:" + ex.Message);
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
        private bool SendMsgOut(string ipcTransID, string receivedPhone, string sendPhone, string MSGID, string msgContent,
                            string ipcWorkDate, string piority,string msgType)
        {
            bool result = true; 
            try
            {
                //Utility.ProcessLog.LogInformation(ipcTransID + "," + receivedPhone + "," + sendPhone + "," + MSGID + "," + msgContent + "," + ipcWorkDate + "," + piority + "," + msgType);
                //con.ExecuteNonquery(Utility.Common.ConStr, "SMS_MESSAGEOUT_INSERT", ipcTransID, receivedPhone, sendPhone, MSGID, msgContent, ipcWorkDate, piority, msgType);
                Utility.ProcessLog.LogInformation(ipcTransID + "," + receivedPhone + "," + sendPhone + "," + MSGID + "," + msgContent + "," + ipcWorkDate + "," + piority + "," + msgType);
                con.ExecuteNonquery(Utility.Common.ConStr, "SMS_MESSAGEOUT_INSERT", ipcTransID, receivedPhone, sendPhone, MSGID, msgContent, ipcWorkDate, piority, msgType, "SMS00500");

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return result;
        }
    }
}
