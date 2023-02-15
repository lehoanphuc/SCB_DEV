using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using DBConnection;
using Utility;
using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
//using iTextSharp.text.pdf;
//using iTextSharp.text;
using System.Threading;
using Formatters;
using System.Net;
using System.Data.OleDb;

namespace Interfaces
{
    public class Email
    {
        public bool CreateEmail(TransactionInfo tran)
        {
            Connection con = new Connection();

            try
            {
                if (tran.Status != Common.TRANSTATUS.FINISH)
                {
                    ProcessLog.LogInformation($"Transaction {tran.IPCTransID} not finish but call send email");
                    return true;
                }
                string tranCode = tran.Data[Common.KEYNAME.IPCTRANCODE].ToString();
                string ipcTranID = tran.IPCTransID.ToString();
                string sourceID = tran.Data[Common.KEYNAME.SOURCEID].ToString();

                DataTable dtDefineOut = con.FillDataTable(Common.ConStr, "IPC_GETOUTPUTDEFINEEMAIL", new object[2] { sourceID, tranCode });
                DataTable dtEmailTemp = con.FillDataTable(Common.ConStr, "IPC_GETEMAILTEMPLATE", new object[2] { sourceID, tranCode });

                if (dtEmailTemp.Rows.Count.Equals(0))
                {
                    throw new Exception($"Email template does not exist, transaction id: {ipcTranID}");
                }
                else
                {
                    string mailSender = dtEmailTemp.Rows[0]["FROM"].ToString();
                    string displayName = dtEmailTemp.Rows[0]["DISPLAYNAME"].ToString();
                    string mailTitle = dtEmailTemp.Rows[0]["TITLE"].ToString();
                    string mailContent = dtEmailTemp.Rows[0]["CONTENT"].ToString();
                    string mailAttachment = dtEmailTemp.Rows[0]["ATTACHMENT"].ToString();

                    //get receiver email
                    DataTable emailInfo = con.FillDataTable(Common.ConStr, "SEMS_EBA_USERS_GETEMAILBYID",
                        tran.Data[Common.KEYNAME.USERID], tran.IPCTransID);
                    string mailReceiver = emailInfo.Rows[0][Common.KEYNAME.EMAIL].ToString();
                    string mailCC = emailInfo.Rows[0]["EMAILCC"].ToString();
                    if (string.IsNullOrEmpty(mailReceiver))
                    {
                        throw new Exception($"Receiver email for user {tran.Data[Common.KEYNAME.USERID]} was not found, cancel sending");
                    }

                    //build mail conent
                    FormatMailContent(ref mailContent, ref mailAttachment, dtDefineOut, tran);

                    int rs = con.ExecuteNonquery(Common.ConStr, "IPC_EMAILOUT_INSERT", new object[9] { ipcTranID, mailSender, displayName, mailReceiver, mailCC, mailTitle, mailContent, mailAttachment, 0 });

                    Utility.Common.HashTableAddOrSet(tran.Data, "SENTEMAIL", "Y");
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                Utility.Common.HashTableAddOrSet(tran.Data, "SENTEMAIL", "N");
            }
            return true;
        }
        private void SaveExcel(TransactionInfo tran, string file)
        {
            Connection con = new Connection();
            string acctno = tran.Data[Common.KEYNAME.ACCTNO].ToString();
            string userid = tran.Data[Common.KEYNAME.USERID].ToString();
            string ipcTranID = tran.IPCTransID.ToString();

            string fromdate = tran.Data["FROMDATE"].ToString();
            string todate = tran.Data["TODATE"].ToString();
            string tranCode = tran.Data[Common.KEYNAME.IPCTRANCODE].ToString();
            string sourceID = tran.Data[Common.KEYNAME.SOURCEID].ToString();
            string email = tran.Data[Common.KEYNAME.EMAIL].ToString();


            DataTable dtRQSTM = con.FillDataTable(Common.ConStr, "IPC_GETRESULT_RQSTM", new object[4] { acctno, userid, fromdate, todate });
            var lines = new List<string>();

            string[] columnNames = dtRQSTM.Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToArray();

            var header = string.Join(",", columnNames.Select(name => $"\"{name}\""));
            lines.Add(header);

            var valueLines = dtRQSTM.AsEnumerable()
                .Select(row => string.Join(",", row.ItemArray.Select(val => $"\"{val}\"")));

            lines.AddRange(valueLines);
            File.WriteAllLines(file, lines,Encoding.UTF8);

            //send mail
            DataTable dtDefineOut = con.FillDataTable(Common.ConStr, "IPC_GETOUTPUTDEFINEEMAIL", new object[2] { sourceID, tranCode });
            DataTable dtEmailTemp = con.FillDataTable(Common.ConStr, "IPC_GETEMAILTEMPLATE", new object[2] { sourceID, tranCode });



            if (dtEmailTemp.Rows.Count.Equals(0))
            {
                throw new Exception($"Email template does not exist, transaction id: {ipcTranID}");
            }
            else
            {
                string mailSender = dtEmailTemp.Rows[0]["FROM"].ToString();
                string displayName = dtEmailTemp.Rows[0]["DISPLAYNAME"].ToString();
                string mailTitle = dtEmailTemp.Rows[0]["TITLE"].ToString();
                string mailContent = dtEmailTemp.Rows[0]["CONTENT"].ToString();
                string mailAttachment = dtEmailTemp.Rows[0]["ATTACHMENT"].ToString();
                string sendAttachment = dtEmailTemp.Rows[0]["SENDATTACHMENT"].ToString();
                //get receiver email

                //build mail conent
                FormatMailContent(ref mailContent, ref mailAttachment, dtDefineOut, tran);

                string mailReceiver = email.Trim();
                if (string.IsNullOrEmpty(mailReceiver))
                {
                    throw new Exception($"Receiver email for user {tran.Data[Common.KEYNAME.USERID]} was not found, cancel sending");
                }
                int rs = con.ExecuteNonquery(Common.ConStr, "IPC_EMAILOUT_INSERT", new object[9] { ipcTranID, mailSender, displayName, mailReceiver, "", mailTitle, mailContent, file, 0 });

                Utility.Common.HashTableAddOrSet(tran.Data, "SENTEMAIL", "Y");

            }
        }
        public bool SendEmailRQSTM(TransactionInfo tran)
        {
            try
            {

                string file = string.Empty;
                string ipcTranID = tran.IPCTransID.ToString();
                file = ConfigurationManager.AppSettings["PathSaveRQSTM"].ToString() + ipcTranID + ".csv";
                ThreadPool.QueueUserWorkItem(delegate { SaveExcel( tran, file); });
              

            }
            catch
            {

            }
            return true;
        }
        public bool Deletefile(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
       
                DataTable dtMailSent =  con.FillDataTable(Common.ConStr, "IPC_GET_MAIL_RQSTM", null);
                foreach(DataRow dr in dtMailSent.Rows)
                {
                    try
                    {
                        if (dr["ATTACHMENT"].ToString().Contains(ConfigurationManager.AppSettings["PathSaveRQSTM"].ToString()))
                        {
                            File.Delete(dr["ATTACHMENT"].ToString());
                        }
                    }
                    catch
                    {

                    }
    
                }
            }
            catch
            {

            }
            return true;
        }
        public bool SendEmailConfig(TransactionInfo tran)
        {
            Connection con = new Connection();

            try
            {
                string tranCode = tran.Data[Common.KEYNAME.IPCTRANCODE].ToString();
                string ipcTranID = tran.IPCTransID.ToString();
                string sourceID = tran.Data[Common.KEYNAME.SOURCEID].ToString();

                DataTable dtDefineOut = con.FillDataTable(Common.ConStr, "IPC_GETOUTPUTDEFINEEMAIL", new object[2] { sourceID, tranCode });
                DataTable dtEmailTemp = con.FillDataTable(Common.ConStr, "IPC_GETEMAILTEMPLATE", new object[2] { sourceID, tranCode });

                if (dtEmailTemp.Rows.Count.Equals(0))
                {
                    throw new Exception($"Email template does not exist, transaction id: {ipcTranID}");
                }
                else
                {
                    string mailSender = dtEmailTemp.Rows[0]["FROM"].ToString();
                    string displayName = dtEmailTemp.Rows[0]["DISPLAYNAME"].ToString();
                    string mailTitle = dtEmailTemp.Rows[0]["TITLE"].ToString();
                    string mailContent = dtEmailTemp.Rows[0]["CONTENT"].ToString();
                    string mailAttachment = dtEmailTemp.Rows[0]["ATTACHMENT"].ToString();

                    //get receiver email
                    DataTable List = con.FillDataTable(Common.ConStr, "IPC_GET_MAIL_CONFIG",
                        tran.Data["VARNAME"]);
                    string[] listmail = List.Rows[0][Common.KEYNAME.EMAIL].ToString().Split(',');

                    //build mail conent
                    FormatMailContent(ref mailContent, ref mailAttachment, dtDefineOut, tran);

                    for (int i = 0; i < listmail.Length; i++)
                    {
                        string mailReceiver = listmail[i].ToString().Trim();
                        if (string.IsNullOrEmpty(mailReceiver))
                        {
                            throw new Exception($"Receiver email for user {tran.Data[Common.KEYNAME.USERID]} was not found, cancel sending");
                        }
                        int rs = con.ExecuteNonquery(Common.ConStr, "IPC_EMAILOUT_INSERT", new object[9] { ipcTranID, mailSender, displayName, mailReceiver, "", mailTitle, mailContent, mailAttachment, 0 });

                        Utility.Common.HashTableAddOrSet(tran.Data, "SENTEMAIL", "Y");

                    }

                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                Utility.Common.HashTableAddOrSet(tran.Data, "SENTEMAIL", "N");
            }
            return true;
        }
        private void FormatMailContent(ref string mailContent, ref string mailAttachment, DataTable dtDefineOut, TransactionInfo tran)
        {
            if (mailContent.Contains("{"))
            {
                List<string> param = GetParams(mailContent);
                foreach (string p in param)
                {
                    DataRow[] drConfigs = dtDefineOut.Select($"FIELDNAME = '{p}'");
                    object value = "";
                    if (drConfigs.Count() > 0)
                    {
                        //switch (drConfigs[0]["VALUESTYLE"])
                        //{
                        //    case "VALUE":
                        //        value = drConfigs[0]["VALUENAME"];
                        //        break;
                        //    case "TRANDATA":
                        //    default:
                        //        value = tran.Data.Contains(drConfigs[0]["VALUENAME"].ToString()) ? tran.Data[drConfigs[0]["VALUENAME"].ToString()].ToString() : "";
                        //        break;
                        //}
                        value = Formatter.GetFieldValue(tran, drConfigs[0]["VALUESTYLE"].ToString(), drConfigs[0]["VALUENAME"].ToString(), tran.Data, tran.parm, null);

                        Formatters.Formatter.FormatFieldValue(ref value, drConfigs[0]["FORMATTYPE"].ToString(), drConfigs[0]["FORMATOBJECT"].ToString(), drConfigs[0]["FORMATFUNCTION"].ToString(), drConfigs[0]["FORMATPARM"].ToString());
                    }
                    else
                    {
                        value = (tran.Data.ContainsKey(p) ? tran.Data[p].ToString() : "");
                    }

                    mailContent = mailContent.Replace("{" + p + "}", value.ToString().Replace("\r\n", "<br/>").Replace("\\r\\n", "<br/>"));
                }
            }

            //build mail attachment
            if (mailAttachment.Contains("{"))
            {
                List<string> param = GetParams(mailAttachment);
                foreach (string p in param)
                {
                    DataRow[] drConfigs = dtDefineOut.Select($"FIELDNAME = '{p}'");
                    object value = "";
                    if (drConfigs.Count() > 0)
                    {
                        //    switch (drConfigs[0]["VALUESTYLE"])
                        //    {
                        //        case "VALUE":
                        //            value = drConfigs[0]["VALUENAME"];
                        //            break;
                        //        case "TRANDATA":
                        //        default:
                        //            value = tran.Data.Contains(drConfigs[0]["VALUENAME"].ToString()) ? tran.Data[drConfigs[0]["VALUENAME"].ToString()].ToString() : "";
                        //            break;
                        //    }

                        //    Formatter format = new Formatter();
                        value = Formatter.GetFieldValue(tran, drConfigs[0]["VALUESTYLE"].ToString(), drConfigs[0]["VALUENAME"].ToString(), tran.Data, tran.parm, null);

                        Formatter.FormatFieldValue(ref value, drConfigs[0]["FORMATTYPE"].ToString(), drConfigs[0]["FORMATOBJECT"].ToString(), drConfigs[0]["FORMATFUNCTION"].ToString(), drConfigs[0]["FORMATPARM"].ToString());
                    }
                    else
                    {
                        value = (tran.Data.ContainsKey(p) ? tran.Data[p].ToString() : "");
                    }

                    mailAttachment = mailAttachment.Replace("{" + p + "}", value.ToString().Replace("\r\n", "<br/>").Replace("\\r\\n", "<br/>"));
                }
            }
        }

        private List<string> GetParams(string content)
        {
            List<string> param = new List<string>();
            string[] kk = content.Split('{');
            foreach (string k in kk)
            {
                if (k.Contains("}"))
                {
                    param.Add(k.Split('}')[0].Trim());
                }
            }
            return param;
        }
        public bool SendEmailOut(TransactionInfo tran)
        {
            try
            {
                Connection db = new Connection();
                DataTable dtEmailOut = db.FillDataTable(Common.ConStr, "IPC_EMAILOUT_SELECT", new object[0]);
                foreach (DataRow dr in dtEmailOut.Rows)
                {
                    try
                    {

                        SendMailMessageAsync(dr["ID"].ToString(), dr["FROM"].ToString().Trim(), dr["DISPLAYNAME"].ToString().Trim(), dr["TO"].ToString().Trim(), dr["CC"].ToString().Trim(),
                        dr["TITLE"].ToString().Trim(), dr["MSGCONTENT"].ToString().Trim(), dr["ATTACHMENT"].ToString().Trim(), $"{dr["TITLE"].ToString().Trim()} - {dr["ID"].ToString().Trim()}");

                    }
                    catch (Exception exx)
                    {
                        Utility.ProcessLog.LogError(exx, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return true;
        }
        public static void SendMailMessageAsync(string ID, string from, string displayName, string to, string cc, string subject, string body, string attachment = "", string attachmentFileName = "")
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                Connection db = new Connection();
                try
                {
                    string errCode = "0";
                    string errDesc = "";
                    if (SendMail(from, displayName, to, cc, subject, body, attachment, attachmentFileName, ref errCode, ref errDesc))
                    {

                        db.ExecuteNonquery(Common.ConStr, "IPC_EMAILOUT_UPDATERESULT", new object[4] { ID, "Y", errCode, errDesc });
                    }
                    else
                    {
                        db.ExecuteNonquery(Common.ConStr, "IPC_EMAILOUT_UPDATERESULT", new object[4] { ID, "F", errCode, errDesc });
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    db = null;
                }
            });
        }

        public static bool SendMail(string from, string displayName, string to, string cc, string subject, string body, string attachment, string attachmentFileName, ref string errorCode, ref string errorDesc)
        {
            // System.Web.Mail.SmtpMail.SmtpServer is obsolete in 2.0
            // System.Net.Mail.SmtpClient is the alternate class for this in 2.0
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            DBConnection.Connection con = new DBConnection.Connection();
            //smtpClient.EnableSsl = true;
            DataTable SM = con.FillDataTable(Common.ConStr, "Settings_LoadPortalSettings");

            smtpClient.Host = SM.Rows[0]["SMTPServer"].ToString();
            smtpClient.Port = int.Parse(SM.Rows[0]["SMTPPort"].ToString());
            smtpClient.EnableSsl = bool.Parse(SM.Rows[0]["SMTPSSL"].ToString());
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);// minh add dongf nayf chi o may test, phai remove o may live
            smtpClient.Credentials = new System.Net.NetworkCredential(SM.Rows[0]["SMTPUserName"].ToString(), SM.Rows[0]["SMTPPassword"].ToString());

            message.DeliveryNotificationOptions =
            DeliveryNotificationOptions.OnFailure |
                   DeliveryNotificationOptions.Delay;
            //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["emaildelivery"].ToString()))
            //{
            //    message.Headers.Add("Disposition-Notification-To", ConfigurationManager.AppSettings["emaildelivery"].ToString());
            //}
            if (!string.IsNullOrEmpty(SM.Rows[0]["EMAILDELIVERY"].ToString()))
            {
                message.Headers.Add("Disposition-Notification-To", SM.Rows[0]["EMAILDELIVERY"].ToString());
            }
            else if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["emaildelivery"].ToString()))
            {
                message.Headers.Add("Disposition-Notification-To", ConfigurationManager.AppSettings["emaildelivery"].ToString());
            }


            try
            {
                message.From = new MailAddress(from, displayName);
                message.To.Add(to);
                if (!string.IsNullOrEmpty(cc))
                {
                    foreach (string ccc in cc.Split(','))
                    {
                        message.CC.Add(ccc.Trim());
                    }
                }

                message.Subject = subject;

                message.IsBodyHtml = true;

                // Message body content
                message.Body = body;
                if (!attachment.Equals("") && !attachmentFileName.Equals(""))
                {
                    if (attachment.Contains(ConfigurationManager.AppSettings["PathSaveRQSTM"].ToString()))
                    {
                        message.Attachments.Add(new Attachment(attachment));
                    }
                    else
                    {
                        MemoryStream pdfStream = new MemoryStream();
                        byte[] arrPdf = Common.HtmlToPdf(attachment);

                        var type = new ContentType();
                        type.MediaType = MediaTypeNames.Application.Pdf;
                        type.Name = attachmentFileName + ".pdf";
                        pdfStream = new MemoryStream(arrPdf);
                        message.Attachments.Add(new Attachment(pdfStream, type));
                    }

                }
                try
                {
                    smtpClient.Send(message);
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {
                            ProcessLog.LogInformation(string.Format("MailboxBusy - retrying in 3 seconds.Email: {0}", to));
                            System.Threading.Thread.Sleep(3000);
                            smtpClient.Send(message);
                        }
                        else
                        {
                            ProcessLog.LogInformation(string.Format("Failed to deliver message to {0}", ex.InnerExceptions[i].FailedRecipient));
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                errorCode = "777";
                errorDesc = ex.Message;
                ProcessLog.LogInformation(string.Format("Exception {0} with {1}", ex.ToString(), to));
                return false;
            }
        }
    }
}
