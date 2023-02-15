using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using Antlr3.ST;
using OfficeOpenXml;

namespace eMailService
{
    public partial class Service : ServiceBase
    {
        private Thread thread;
        private bool isRunning = true;
        private string ConStr = "";
        private Thread thSendNotify;

        public Service()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            StartServer();
            thSendNotify = new Thread(new ThreadStart(SendMail));
            thSendNotify.Start();
            ProcessLog.LogInformation("[Service] Send mail started");
        }

        protected override void OnStop()
        {
            StopServer();
        }

        private void StartServer()
        {
            //System.Diagnostics.Debugger.Launch();
            try
            {
                isRunning = true;
            }
            catch (Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void StopServer()
        {
            try
            {
                ProcessLog.LogInformation("[Service] Send mail stopping");
                isRunning = false;
                if (thread != null)
                    thread.Abort();
                if (thSendNotify != null)
                    thSendNotify.Abort();
                ProcessLog.LogInformation("[Service] Send mail stopped");
            }
            catch (Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

        }

        private void SendMail()
        {
            int sleeptime = int.Parse(ConfigurationManager.AppSettings["SLEEP_TIME"].ToString());
            try
            {
                while (isRunning)
                {
                    try
                    {
                        try
                        {
                            RunSendMail();
                        }
                        catch
                        {
                        }
                        Thread.Sleep(sleeptime);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (isRunning)
                                ProcessLog.LogInformation("Send mail error : " + ex.ToString());
                        }
                        catch (Exception exex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (isRunning)
                    ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                        System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void RunSendMail()
        {
            try
            {
                string query = ConfigurationManager.AppSettings["QUERY_STRING"].ToString();
                string attachmentFileName = ConfigurationManager.AppSettings["AttachmentFileName"].ToString() + DateTime.Now.ToString("yyyyMMddHHmm"); ;
                DataSet ds = new DataSet();
                ds = runQueryString(query);
                MemoryStream ms = DataSet2Base64Excel(ds);
                Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
                tmpl = GetEmailTemplate(ConfigurationManager.AppSettings["MailTemplate"].ToString());
                ThreadPool.QueueUserWorkItem(delegate { SendMail(tmpl.ToString(), ms, attachmentFileName); });
            }
            catch (Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        public static MemoryStream DataSet2Base64Excel(DataSet ds)
        {
            try
            {
                ExcelPackage pck = new ExcelPackage();
                foreach (DataTable dt in ds.Tables)
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(dt.TableName);
                    ws.Cells["A1"].LoadFromDataTable(dt, true);
                }
                var stream = new MemoryStream(pck.GetAsByteArray());
                return stream;
            }
            catch (Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }
        private DataSet runQueryString(string query)
        {
            try
            {
                ConStr = Common.DecryptData(ConfigurationManager.ConnectionStrings["connectionString"].ToString());
                Connection con = new Connection();
                DataSet ds = new DataSet();
                ds = con.FillDataSetSQL(ConStr, query);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static StringTemplate GetEmailTemplate(string templateFileName)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "MailTemplate";
                StringTemplateGroup group = new StringTemplateGroup("MailTemplate", path);
                StringTemplate query = group.GetInstanceOf(string.Format("{0}", templateFileName));
                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static bool SendMail(string body, MemoryStream attachment, string attachmentFileName)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            smtpClient.Host = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());
            smtpClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            string toListEmailCC = ConfigurationManager.AppSettings["ToEmailAddressCC"].ToString();
            string toListEmailBCC = ConfigurationManager.AppSettings["ToEmailAddressBCC"].ToString();
            string emailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            string emailPassword = Common.DecryptData(ConfigurationManager.AppSettings["FromEmailPassword"].ToString());
            string displayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            string subject = ConfigurationManager.AppSettings["Subject"].ToString();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailAddress, emailPassword);
            try
            {
                message.From = new MailAddress(emailAddress, displayName);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay;
                if (attachment != null && !attachmentFileName.Equals(""))
                {
                    var type = new ContentType();
                    type.MediaType = ConfigurationManager.AppSettings["MediaType"].ToString();
                    type.Name = attachmentFileName + ConfigurationManager.AppSettings["Type"].ToString();
                    message.Attachments.Add(new Attachment(attachment, type));
                }
                if (!string.IsNullOrEmpty(toListEmailCC))
                {
                    var list = toListEmailCC.Split(';');
                    for (int i = 0; i < list.Length; i++)
                    {
                        if (list[i].Trim() != "" && IsValidEmail(list[i].Trim()))
                        {
                            MailAddress to = new MailAddress(list[i]);
                            message.CC.Add(to);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(toListEmailBCC))
                {
                    var list = toListEmailBCC.Split(';');
                    for (int i = 0; i < list.Length; i++)
                    {
                        if (list[i].Trim() != "" && IsValidEmail(list[i].Trim()))
                        {
                            MailAddress to = new MailAddress(list[i]);
                            message.Bcc.Add(to);
                        }
                    }
                }
                try
                {
                    smtpClient.Send(message);
                    ProcessLog.LogInformation("Send mail success");
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {
                            ProcessLog.LogInformation("EMAIL LOG " + ex.ToString() + string.Format(" MailboxBusy - retrying in 1 seconds.Email: {0} " + toListEmailCC + toListEmailBCC));
                            System.Threading.Thread.Sleep(2000);
                            smtpClient.Send(message);
                        }
                        else
                        {
                            ProcessLog.LogInformation("EMAIL LOG " + ex.ToString() + string.Format(" Failed to deliver message to {0} " + ex.InnerExceptions[i].FailedRecipient));
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ProcessLog.LogInformation("EMAIL LOG " + ex.ToString() + " " + toListEmailCC + toListEmailBCC + " " + Convert.ToBase64String(Encoding.UTF8.GetBytes(body)));
                return false;
            }
        }
        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
