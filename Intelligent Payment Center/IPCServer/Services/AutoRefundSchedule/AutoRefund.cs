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

namespace AutoRefundSchedule
{
    public partial class AutoRefund : ServiceBase
    {
        static ITransaction.AutoTrans autoTrans;

        private bool isRunning = true;
        private Thread thAutoRefund;

        public AutoRefund()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartServer();

            bool enableService = bool.Parse(ConfigurationManager.AppSettings["ENABLESERVICE"].ToString());

            if (enableService)
            {
                thAutoRefund = new Thread(new ThreadStart(RegisterAutoRefund));
                thAutoRefund.Start();
                Utility.ProcessLog.LogInformation("[Service] Auto refund started");
            }
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
            try
            {
                Utility.ProcessLog.LogInformation("[Service] Auto Refund stopping");
                autoTrans = null;
                isRunning = false;
                if(thAutoRefund != null)
                    thAutoRefund.Abort();
                Utility.ProcessLog.LogInformation("[Service] Auto Refund stopped");
            }
            catch(Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            
        }

        private void RegisterAutoRefund()
        {
            int time_loop = int.Parse(ConfigurationManager.AppSettings["TIME_LOOP"].ToString());
            try
            {
                while (isRunning)
                {
                    try
                    {
                        RunAutoRefund();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (isRunning) Utility.ProcessLog.LogInformation("Auto refund error : " + ex.ToString());
                        }
                        catch (Exception exex)
                        {
                        }
                    }
                    Thread.Sleep(time_loop);
                }
            }
            catch (Exception ex)
            {
                if (isRunning) Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void RunAutoRefund()
        {
            try
            {
                if (autoTrans == null)
                {
                    autoTrans = (ITransaction.AutoTrans)Activator.GetObject(
                        typeof(ITransaction.AutoTrans),
                        ConfigurationManager.AppSettings["RMIPC_URL"].ToString());
                }

                Hashtable InputData = new Hashtable();
                InputData.Add(Utility.Common.KEYNAME.IPCTRANCODE, "SEMSRELEASECASHCODE");
                InputData.Add(Utility.Common.KEYNAME.SOURCEID, "SEMS");
                InputData.Add(Utility.Common.KEYNAME.REVERSAL, "N");              
                Hashtable OutputData = autoTrans.ProcessTransHAS(InputData);

                return;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return;
        }

    }
}
