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
using System.Configuration;

namespace SMSProcesssMsgMain
{
    public partial class ProcessMain : ServiceBase
    {
        static ITransaction.AutoTrans autoTrans;
        private Thread thread;
        private bool isRunning = true;

        public ProcessMain()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartServer();
            thread = new Thread(new ThreadStart(GSMGetMsgIn));
            thread.Start();
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
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void StopServer()
        {
            autoTrans = null;
            isRunning = false;
            //edit by Vu Tran 10/06/2014
            //this.Stop();
            thread.Abort();
        }
        private void GSMGetMsgIn()
        {
            try
            {
                Lib objLib = new Lib();
                while(isRunning)
                {
                    try
                    {
                        objLib.AutoTrans(autoTrans);
                        Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["SLEEP_TIME"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            EventLog.WriteEntry("SMS mesageMain. Error:" + ex.Message);
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
                //throw ex;
            }

        }
    }
}
