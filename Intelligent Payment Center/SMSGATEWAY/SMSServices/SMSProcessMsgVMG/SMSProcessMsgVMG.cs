using System;
using System.Data;
using System.ServiceProcess;
using System.Threading;
using DBConnection;
using Utility;
using System.Configuration;
using System.Threading.Tasks;

namespace SMSProcessMsgMT
{
    public partial class SMSProcessMsgVMG : ServiceBase
    {
        private bool monitor = true;
        ServiceController sendMT = null;
        private Thread thread;


        public SMSProcessMsgVMG()
        {
            InitializeComponent();
        }

        private void SendSMSXMLFormat()
        {
            Connection con = new Connection();
            try
            {
                sendMT = new ServiceController("SMSProcessMsgVMG");

                DataTable dtOUTPUTXML = new DataTable();
                DataTable dtCONNECTIONWS = new DataTable();
                DataSet dsParams = con.FillDataSet(Common.ConStr, "SMSVMG_GETPARAMSTOSENDSMS");
                if (dsParams != null && dsParams.Tables.Count > 0)
                {
                    dtOUTPUTXML = dsParams.Tables[0];
                    dtCONNECTIONWS = dsParams.Tables[1];
                }

                while (monitor)
                {
                    try
                    {
                        if (Common.CheckCluster(Common.MODULEID.SMSPROCESSMSGMT))
                        {
                            DataTable dtListSMS = new DataTable();

                            dtListSMS = con.FillDataTable(Common.ConStr, "SMSVMG_GETALLTRANSFROMSMSMSGOUT");

                            if (dtListSMS != null && dtListSMS.Rows.Count > 0)
                            {
                                SoapClient soapClient = new SoapClient();

                                foreach (DataRow row in dtListSMS.Rows)
                                {
                                    Thread thread = new Thread(() => RunSendMessageThread(row, dtOUTPUTXML, dtCONNECTIONWS, soapClient));
                                    thread.Start();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ProcessLog.LogInformation($"SendSMS: {ex.Message}", Utility.Common.FILELOGTYPE.LOGFILEPATH);
                    }

                    try
                    {
                        Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["smsinterval"].ToString()));
                    }
                    catch
                    {
                        Thread.Sleep(5000);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation($"Error start. Error: {ex.Message}", Utility.Common.FILELOGTYPE.LOGFILEPATH);
                OnStop();
            }
            finally
            {
                con = null;
            }
        }

        private void RunSendMessageThread(DataRow row, DataTable dtOUTPUTXML, DataTable dtCONNECTIONWS, SoapClient soapClient)
        {
            if (PhoneNoFormatValid(row["RECEIVEDPHONE"].ToString()))
            {
                soapClient.SendMessage(dtOUTPUTXML, dtCONNECTIONWS, row);
            }
            else
            {
                Connection con = new Connection();
                con.ExecuteNonquery(Common.ConStr, "SMSVMG_UPDATESTATUS", row["IPCTRANSID"].ToString(), "I");
                con = null;
            }
        }

        private void SendSMSJsonFormat()
        {
            Connection con = new Connection();
            try
            {
                sendMT = new ServiceController("SMSProcessMsgVMG");
                while (monitor)
                {
                    try
                    {
                        if (Common.CheckCluster(Common.MODULEID.SMSPROCESSMSGMT))
                        {
                            DataTable dtListSMS = new DataTable();

                            dtListSMS = con.FillDataTable(Common.ConStr, "SMSVMG_GETALLTRANSFROMSMSMSGOUT");

                            if (dtListSMS != null && dtListSMS.Rows.Count > 0)
                            {
                                DataTable dtOUTPUTHTTP = new DataTable();
                                DataTable dtOUTPUTHTTPHEADER = new DataTable();
                                DataTable dtCONNECTIONWS = new DataTable();
                                DataSet dsParams = con.FillDataSet(Common.ConStr, "SMSVMG_GETPARAMSTOSENDSMS");
                                if (dsParams != null && dsParams.Tables.Count > 0)
                                {
                                    dtOUTPUTHTTP = dsParams.Tables[2];
                                    dtOUTPUTHTTPHEADER = dsParams.Tables[3];
                                    dtCONNECTIONWS = dsParams.Tables[1];
                                }

                                HttpClient httpClient = new HttpClient();
                                foreach (DataRow row in dtListSMS.Rows)
                                {
                                    if (PhoneNoFormatValid(row["RECEIVEDPHONE"].ToString()))
                                    {
                                        httpClient.HttpPostCVRSMessage(dtOUTPUTHTTP, dtOUTPUTHTTPHEADER, dtCONNECTIONWS, row);
                                    }
                                    else
                                    {
                                        con.ExecuteNonquery(Common.ConStr, "SMSVMG_UPDATESTATUS", row["IPCTRANSID"].ToString(), "I");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ProcessLog.LogInformation($"SendSMS: {ex.Message}", Utility.Common.FILELOGTYPE.LOGFILEPATH);
                    }

                    try
                    {
                        Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["smsinterval"].ToString()));
                    }
                    catch
                    {
                        Thread.Sleep(5000);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation($"Error start. Error: {ex.Message}", Utility.Common.FILELOGTYPE.LOGFILEPATH);
                OnStop();
            }
            finally
            {
                con = null;
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

        protected override void OnStart(string[] args)
        {
            Utility.ProcessLog.LogInformation($"Starting push service", Utility.Common.FILELOGTYPE.LOGFILEPATH);
            Common.ConStr = ConfigurationManager.ConnectionStrings["ConnectionHost"].ToString();
            Common.ConStr = Common.DecryptData(Common.ConStr);
            try
            {
                Common.CLUSTERID = int.Parse(ConfigurationManager.AppSettings["ClusterID"].ToString());
            }
            catch
            {
                Common.CLUSTERID = 0;
            }
            monitor = true;
            //thread = new Thread(new ThreadStart(SendSMSJsonFormat));
            thread = new Thread(new ThreadStart(SendSMSXMLFormat));
            thread.Start();
            Utility.ProcessLog.LogInformation($"Push service started", Utility.Common.FILELOGTYPE.LOGFILEPATH);
        }

        protected override void OnStop()
        {
            try
            {
                monitor = false;
                thread.Abort();
                sendMT.Stop();
                Utility.ProcessLog.LogInformation($"Stop SMS send service.", Utility.Common.FILELOGTYPE.LOGFILEPATH);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation($"Thread stop error {ex.Message}", Utility.Common.FILELOGTYPE.LOGFILEPATH);
            }
        }
    }
}
