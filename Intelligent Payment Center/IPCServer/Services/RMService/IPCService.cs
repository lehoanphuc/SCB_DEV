using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using Utility;
using DBConnection;
using System.Net;
using System.Net.Sockets;
using Schedules;
using System.Timers;
using System.Threading;

namespace Service
{
    public partial class IPCService : ServiceBase
    {
        private System.Timers.Timer synNewAcct = null;
        private System.Timers.Timer synWorkingTime = null;
        double interval = 0;
        ServiceControl.ServiceController _sc = new ServiceControl.ServiceController();
        public static List<Thread> lsThreads = new List<Thread>();
        public IPCService()
        {
            InitializeComponent();
            try
            {
                if (synWorkingTime == null)
                {
                    synWorkingTime = new System.Timers.Timer();
                    synWorkingTime.Elapsed += new ElapsedEventHandler(synChangeWorkingTime_Elapsed);
                    interval = double.Parse(ConfigurationManager.AppSettings["SynWorkingTime"].ToString());
                    synWorkingTime.Interval = interval;
                    synWorkingTime.Start();
                }

                //if (synNewAcct == null)
                //{
                //    synNewAcct.Elapsed += new ElapsedEventHandler(synNewAcct_Elapsed);
                //    interval = double.Parse(ConfigurationManager.AppSettings["SynAcctTime"].ToString());
                //    synNewAcct.Interval = interval;
                //    synNewAcct.Start();
                //}
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                //synNewAcct.Elapsed += new ElapsedEventHandler(synNewAcct_Elapsed);
                //interval = 900000;
                //synNewAcct.Interval = 900000;
                //synNewAcct.Start();
                synWorkingTime.Elapsed += new ElapsedEventHandler(synChangeWorkingTime_Elapsed);
                interval = 900000;
                synWorkingTime.Interval = 900000;
                synWorkingTime.Start();
            }
        }

        void synChangeWorkingTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                synWorkingTime.Stop();
                ServiceControl.ServiceController synTimeObj = new ServiceControl.ServiceController();
                synTimeObj.ChangeWorkingDateSystem();
                synWorkingTime.Interval = interval;
                synWorkingTime.Start();
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                //throw new NotImplementedException();
                synWorkingTime.Start();
            }
        }

        protected override void OnStart(string[] args)
        {
            StartServer();
        }

        protected override void OnStop()
        {
            StopServer();
        }

        protected override void OnPause()
        {
            PauseServer();
        }

        protected override void OnContinue()
        {
            ContinueServer();
        }

        private void StartServer()
        {
            try
            {
                Common.ServiceInformation = "Starting Intelligent Payment Center";

                #region Get OTP Info
                Common.OTPUser = ConfigurationManager.AppSettings["OTPUser"].ToString();
                Common.OTPPass = ConfigurationManager.AppSettings["OTPPass"].ToString();
                Common.OTPPass = Common.DecryptData(Common.OTPPass);
                Common.OTPLifeTime = long.Parse(ConfigurationManager.AppSettings["OTPLifeTime"].ToString());
                #endregion

                #region Get Connection Information
                Common.ConStr = ConfigurationManager.ConnectionStrings["ConnectionHost"].ToString();
                Common.ConStr = Common.DecryptData(Common.ConStr);
                try
                {
                    string[] msDBInfo = Common.ConStr.Split(';');
                    if (msDBInfo.Length > 2)
                    {
                        string[] msTemp = msDBInfo[0].ToString().Split('=');
                        Common.DBServerName = msTemp[1];
                        msTemp = msDBInfo[1].ToString().Split('=');
                        Common.DBName = msTemp[1];
                        msTemp = msDBInfo[2].ToString().Split('=');
                        Common.DBAuthentication = msTemp[1];
                        if (Common.DBAuthentication.ToUpper() == "TRUE")
                        {
                            Common.DBAuthentication = "0";
                        }
                        else
                        {
                            Common.DBAuthentication = "1";
                            if (msDBInfo.Length > 4)
                            {
                                msTemp = msDBInfo[3].ToString().Split('=');
                                Common.DBUserID = msTemp[1];
                                msTemp = msDBInfo[4].ToString().Split('=');
                                Common.DBPassword = msTemp[1];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
                #endregion

                #region  Get cluster infomation
                try
                {
                    Common.CLUSTERID = int.Parse(ConfigurationManager.AppSettings["ClusterID"].ToString());
                }
                catch { }
                #endregion

                #region Register Remoting Service
                foreach (TcpChannel channel in ChannelServices.RegisteredChannels)
                {
                    ChannelServices.UnregisterChannel(channel);
                }
                RemotingConfiguration.Configure(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
                #endregion

                #region Load Sytem Information
                Common.ServiceInformation = "Initialize Intelligent Payment Center...";
                //System.Threading.Thread.Sleep(1000);
                if (InitSystem() == false) return;
                Common.ServiceInformation = "Load system parameter...";
                //System.Threading.Thread.Sleep(1000);
                if (LoadSysvar() == false) return;
                Common.ServiceInformation = "Load database information...";
                //System.Threading.Thread.Sleep(1000);
                if (LoadDBI() == false) return;
                Common.ServiceStarted = true;
                Utility.ProcessLog.LogInformation("Intelligent Payment Center Started");
                #endregion

                #region RunSchedules
                IPCSchedule sc = new IPCSchedule();
                sc.RunSchedules();
                #endregion

                #region Run other scheduling job
                _sc.RunSchedulingJobs();
                #endregion
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
                Utility.ProcessLog.LogInformation("IPC Service will stop.....");
                Common.ServiceInformation = "Stoping Intelligent Payment Center";
                Common.ServiceStarted = false;

                foreach (System.Threading.Thread thread in Common.dicClusterThread.Values)
                    try
                    {
                        thread.Abort();
                    }
                    catch { }

                foreach (KeyValuePair<string,Thread> pair in Common.lsRunningThreads)
                    try
                    {
                        pair.Value.Abort();
                        //thread.Abort();
                    }
                    catch { }

                // waiting for all process complete
                while (Common.TranProcessingCount > 0)
                {
                    Utility.ProcessLog.LogInformation("Waiting for process " + Common.TranProcessingCount.ToString() + " transaction complete");
                    System.Threading.Thread.Sleep(1000);
                }
                Utility.ProcessLog.LogInformation("Intelligent Payment Center Stoped");
                if (synNewAcct != null) synNewAcct.Stop();
                if (synWorkingTime != null) synWorkingTime.Stop();

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void PauseServer()
        {
            try
            {
                Utility.ProcessLog.LogInformation("IPC Service will pause.....");
                Common.ServiceInformation = "Pausing Intelligent Payment Center";
                Common.ServiceStarted = false;
                // waiting for all process complete
                while (Common.TranProcessingCount > 0)
                {
                    Utility.ProcessLog.LogInformation("Waiting for process " + Common.TranProcessingCount.ToString() + " transaction complete");
                    System.Threading.Thread.Sleep(1000);
                }
                Utility.ProcessLog.LogInformation("Intelligent Payment Center Paused");
                if (synNewAcct != null) synNewAcct.Stop();
                if (synWorkingTime != null) synWorkingTime.Stop();
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
            }
        }

        private void ContinueServer()
        {
            try
            {
                Common.ServiceInformation = "Resuming Intelligent Payment Center";
                Common.ServiceStarted = true;
                Utility.ProcessLog.LogInformation("Intelligent Payment Center Continue");
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private bool InitSystem()
        {
            Connection con = new Connection();
            try
            {
                Common.LstSchedule.Limit = 5000;
                try
                {
                    Common.LstSchedule.Limit = int.Parse(ConfigurationManager.AppSettings["LimitQueueSchedule"]);
                }
                catch { }

                //object result = con.ExecuteScalarSQL(Common.ConStr, "SELECT MAX(IPCTRANSID) IPCTRANSID FROM" + 
                //        " (SELECT MAX(IPCTRANSID) IPCTRANSID FROM IPCLOGMESSAGE" +
                //        " UNION ALL" +
                //        " SELECT ISNULL(MAX(IPCTRANSID),0) IPCTRANSID FROM IPCLOGMESSAGEHIS) A");
                //if (result == null || result.ToString() == "")
                //    Common.IPCTransID = 1;
                //else
                //    Common.IPCTransID = long.Parse(result.ToString()) + 1;
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
        }

        private bool LoadSysvar()
        {
            Connection con = new Connection();
            try
            {
                DataTable dt = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCSYSVAR");
                if (dt != null)
                {
                    Common.SYSVAR.Clear();
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        Common.SYSVAR.Add(dt.Rows[row]["VARNAME"].ToString(), dt.Rows[row]["VARVALUE"].ToString());
                    }
                    if (Common.SYSVAR[Common.SYSVARNAME.IPCWORKDATE] != null) Common.IPCWorkDate = Common.SYSVAR[Common.SYSVARNAME.IPCWORKDATE].ToString();
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
        }

        private bool LoadDBI()
        {
            Connection con = new Connection();
            try
            {
                Common.DBITRANLIST = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCTRANSLIST");
                Common.DBIPARMINFO = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCPARMINFO");
                Common.DBIDATADEFINE = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCDATADEFINE");

                Common.DBICONNECTIONDB = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCCONNECTIONDB");
                Common.DBICONNECTIONWS = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCCONNECTIONWS");

                Common.DBIERRORLIST = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM SYSERRORLIST");
                Common.DBIERRORLISTSOURCE = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM SYSERRORLISTSOURCE");
                Common.DBIERRORLISTDEST = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM SYSERRORLISTDEST");

                Common.DBIINPUTDEFINEISO = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCINPUTDEFINEISO");
                Common.DBIINPUTDEFINESMS = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCINPUTDEFINESMS");

                Common.DBIOUTPUTDEFINEXML = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCOUTPUTDEFINEXML");
                Common.DBIOUTPUTDEFINEISO = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCOUTPUTDEFINEISO");
                Common.DBIOUTPUTDEFINESEP = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCOUTPUTDEFINESEP");
                Common.DBIOUTPUTDEFINESMS = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCOUTPUTDEFINESMS");
                Common.DBIOUTPUTDEFINEHAS = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCOUTPUTDEFINEHAS");

                Common.DBIMAPIPCTRANCODEISO = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCMAPIPCTRANCODEISO");
                Common.DBIMAPIPCTRANCODESMS = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCMAPIPCTRANCODESMS");

                Common.DBILOGDEFINE = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCLOGDEFINE");
                Common.DBILOGTRANSDETAILFIELD = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCLOGTRANSDETAILFIELD");
                try
                {
                    Common.DBILOGDEFINEREQUESTFORLOAN = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCLOGDEFINEREQUESTFORLOAN");
                }
                catch { }
                Common.DBIAUTHENTICATION = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM SYS_AUTHENDEVICE");
                Common.DBIINPUTDEFINEXML = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCINPUTDEFINEXML");

                try
                {
                    Common.DBIOUTPUTDEFINEHTTP = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCOUTPUTDEFINEHTTP");
                    Common.DBIOUTPUTDEFINEHTTPHEADER = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCOUTPUTDEFINEHTTPHEADER");
                    Common.DBIINPUTDEFINEHTTP = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCINPUTDEFINEHTTP");

                    Common.DBIMAPIPCTRANCODEPUSH = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCMAPIPCTRANCODEPN");
                    Common.DBIOUTPUTDEFINEPUSH = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCOUTPUTDEFINEPN");
                    Common.DBIEBAPUSHNOTIFICATION = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM EBA_PUSHNOTIFICATION");
                    Common.DBIMAPXMLTOFIELD = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM IPCMAPXMLTOFIELD");
                }
                catch { }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
        }

    }
}