#region XD World Recipe V 3
// FileName: EmailHelper.cs
// Author: Dexter Zafra
// Date Created: 5/19/2008
// Website: www.ex-designz.net
#endregion
using System;
using System.Net.Mail;
using System.Threading;
using SmartPortal.BLL;
using SmartPortal.Model;
using System.Data;
using SmartPortal.ExceptionCollection;
using System.Configuration;
using System.Globalization;
using System.Collections;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Net.Mime;
using System.Net;

namespace SmartPortal.Common
{
    /// <summary>
    /// Object in this class send email using System.Net.Mail
    /// </summary>
    public static class EmailHelper
    {

        /// <summary>
        /// Sends the mail message asynchronously in another thread.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public static void SendMailMessageAsync(string displayName, string to, string subject, string body, string attachment = "", string attachmentFileName = "")
        {
            ThreadPool.QueueUserWorkItem(delegate {
                SendMail(displayName, to, subject, body, attachment, attachmentFileName); 
            });
        }

        /// <summary>
        /// send mail
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static bool SendMail(string displayName, string to, string subject, string body, string attachment = "", string attachmentFileName = "")
        {
            // System.Web.Mail.SmtpMail.SmtpServer is obsolete in 2.0
            // System.Net.Mail.SmtpClient is the alternate class for this in 2.0
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            //smtpClient.EnableSsl = true;

            SmartPortal.BLL.SettingsBLL SB = new SettingsBLL();
            SmartPortal.Model.SettingsModel SM = new SettingsModel();

            SM = SB.LoadPortalSettings();

            smtpClient.Host = SM.SmtpServer;
            smtpClient.Port = SM.SmtpPort;
            smtpClient.EnableSsl = SM.SMTPSSL;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //smtpClient.UseDefaultCredentials = false;
            message.DeliveryNotificationOptions =
            DeliveryNotificationOptions.OnFailure |
                    DeliveryNotificationOptions.Delay;
              if (!string.IsNullOrEmpty(SM.SMTPEmailDelivey))
            {
                message.Headers.Add("Disposition-Notification-To",SM.SMTPEmailDelivey);
            }
            smtpClient.Credentials = new System.Net.NetworkCredential(SM.SmtpUserName, SM.SmtpPassword);
            string fromemail = string.Empty;
            try
            {
                fromemail = SM.SmtpUserName;
                message.From = new MailAddress(fromemail, displayName);
                message.To.Add(to);

                //xmk
                //if (SM.SMTPCCEmail == "" || SM.SMTPCCEmail == null)
                //{
                //}
                //else
                //{
                //    message.CC.Add(SM.SMTPCCEmail);
                //}

                //if (SM.SMTPBCCEmail == "" || SM.SMTPBCCEmail == null)
                //{
                //}
                //else
                //{
                //    message.Bcc.Add(SM.SMTPBCCEmail);
                //}

                message.Subject = displayName + " - " + subject;

                message.IsBodyHtml = true;

                // Message body content
                message.Body = body;
                if (!attachment.Equals("") && !attachmentFileName.Equals(""))
                {
                    MemoryStream pdfStream = new MemoryStream();
                    var document = new Document(PageSize.A4);
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, pdfStream);

                    var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                    document.Open();
                    htmlWorker.StartDocument();
                    htmlWorker.Parse(new StringReader(attachment));
                    htmlWorker.Close();
                    document.Close();

                    var type = new ContentType();
                    type.MediaType = MediaTypeNames.Application.Pdf;
                    type.Name = attachmentFileName + ".pdf";
                    pdfStream = new MemoryStream(pdfStream.ToArray());
                    message.Attachments.Add(new Attachment(pdfStream, type));
                }

                // Send SMTP mail

                try
                {
                    smtpClient.Send(message);
                    Log.WriteLogFile("EMAIL LOG", "", "Email delivery successful", to);
                    try
                    {
                        SB.LogEmailOut(fromemail, displayName, to, "", subject, body, attachment, "Y", "", "");
                    }
                    catch (Exception ex)
                    {
                        Log.WriteLogFile("LOG TO EMAILOUT", "", ex.Message, "");
                    }
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {
                            Log.WriteLogFile("EMAIL LOG", "", ex.ToString(), string.Format("MailboxBusy - retrying in 1 seconds.Email: {0}", to));
                            System.Threading.Thread.Sleep(2000);
                            smtpClient.Send(message);
                        }
                        else
                        {
                            Log.WriteLogFile("EMAIL LOG", "", ex.ToString(), string.Format("Failed to deliver message to {0}", ex.InnerExceptions[i].FailedRecipient));
                        }
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLogFile("EMAIL LOG", "", ex.ToString(), to + Convert.ToBase64String(Encoding.UTF8.GetBytes(body)));
                try
                {
                    SB.LogEmailOut(fromemail, displayName, to, "", subject, body, attachment, "F", "9999", ex.ToString());
                }
                catch(Exception exx)
                {
                    Log.WriteLogFile("LOG TO EMAILOUT", "", exx.Message, "");
                }
                
                return false;
            }
        }

        public static void TransactionSuccess_SendMail(Hashtable hasPrint, string userId)
        {

            string IPCERRORCODE = string.Empty;
            string IPCERRORDESC = string.Empty;

            string email = new SmartPortal.SEMS.User().GetUBID(userId).Rows[0]["EMAIL"].ToString();
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("TransactionSuccessful" + CultureInfo.CurrentCulture.Name);

            tmpl.SetAttribute("senderAccount", hasPrint.ContainsKey("senderAccount") ? hasPrint["senderAccount"].ToString() : "");
            tmpl.SetAttribute("senderBalance", hasPrint.ContainsKey("senderBalance") ? hasPrint["senderBalance"].ToString() : "");
            tmpl.SetAttribute("ccyid", hasPrint.ContainsKey("ccyid") ? hasPrint["ccyid"].ToString() : "");
            tmpl.SetAttribute("status", hasPrint.ContainsKey("status") ? hasPrint["status"].ToString() : "");
            tmpl.SetAttribute("senderName", hasPrint.ContainsKey("senderName") ? hasPrint["senderName"].ToString() : "");
            tmpl.SetAttribute("recieverAccount", hasPrint.ContainsKey("recieverAccount") ? hasPrint["recieverAccount"].ToString() : "");
            tmpl.SetAttribute("recieverName", hasPrint.ContainsKey("recieverName") ? hasPrint["recieverName"].ToString() : "");
            tmpl.SetAttribute("amount", hasPrint.ContainsKey("amount") ? hasPrint["amount"].ToString() : "");
            tmpl.SetAttribute("amountchu", hasPrint.ContainsKey("amountchu") ? hasPrint["amountchu"].ToString() : "");
            tmpl.SetAttribute("feeType", hasPrint.ContainsKey("feeType") ? hasPrint["feeType"].ToString() : "");
            tmpl.SetAttribute("feeAmount", hasPrint.ContainsKey("feeAmount") ? hasPrint["feeAmount"].ToString() : "");
            tmpl.SetAttribute("desc", hasPrint.ContainsKey("desc") ? hasPrint["desc"].ToString() : "");
            tmpl.SetAttribute("tranID", hasPrint.ContainsKey("tranID") ? hasPrint["tranID"].ToString() : "");
            tmpl.SetAttribute("tranDate", hasPrint.ContainsKey("tranDate") ? hasPrint["tranDate"].ToString() : "");

            DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["senderBranch"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtSenderBranch = dsSenderBranch.Tables[0];
                if (dtSenderBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("senderBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("senderBranch", "");
            }

            //lay chi nhanh nhan
            DataSet dsReceiverBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["receiverBranch"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtReceiverBranch = dsReceiverBranch.Tables[0];
                if (dtReceiverBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("receiverBranch", dtReceiverBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("receiverBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("receiverBranch", "");
            }

            SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
            SmartPortal.Common.Log.WriteLogFile("EMAIL NOTIFY", "", "", "  Send transfer email success:  " + email + " TransID: " + hasPrint["tranID"].ToString() + " UserID: "
                                           + userId);
        }
        public static void RegisterEWalletSuccess_SendMail(Hashtable hasPrint, string userId)
        {

            string IPCERRORCODE = string.Empty;
            string IPCERRORDESC = string.Empty;

            string email = new SmartPortal.SEMS.User().GetUBID(userId).Rows[0]["EMAIL"].ToString();
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("IBRegisterEWallet" + CultureInfo.CurrentCulture.Name);

            tmpl.SetAttribute("senderAccount", hasPrint.ContainsKey("senderAccount") ? hasPrint["senderAccount"].ToString() : "");
            tmpl.SetAttribute("senderName", hasPrint.ContainsKey("senderName") ? hasPrint["senderName"].ToString() : "");
            tmpl.SetAttribute("tranID", hasPrint.ContainsKey("tranID") ? hasPrint["tranID"].ToString() : "");
            tmpl.SetAttribute("tranDate", hasPrint.ContainsKey("tranDate") ? hasPrint["tranDate"].ToString() : "");
            tmpl.SetAttribute("WalletType", hasPrint.ContainsKey("WalletType") ? hasPrint["WalletType"].ToString() : "");
            tmpl.SetAttribute("PhoneNo", hasPrint.ContainsKey("PhoneNo") ? hasPrint["PhoneNo"].ToString() : "");
            tmpl.SetAttribute("RefID", hasPrint.ContainsKey("RefID") ? hasPrint["RefID"].ToString() : "");


            DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["senderBranch"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtSenderBranch = dsSenderBranch.Tables[0];
                if (dtSenderBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("senderBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("senderBranch", "");
            }

            SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
            SmartPortal.Common.Log.WriteLogFile("EMAIL NOTIFY", "", "", "  Send transfer email success:  " + email + " TransID: " + hasPrint["tranID"].ToString() + " UserID: "
                                           + userId);
        }
        public static void TopupTransactionSuccess_SendMail(Hashtable hasPrint, string userId)
        {

            string IPCERRORCODE = string.Empty;
            string IPCERRORDESC = string.Empty;

            string email = new SmartPortal.SEMS.User().GetUBID(userId).Rows[0]["EMAIL"].ToString();
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("BuyTopUpSucsessfull" + CultureInfo.CurrentCulture.Name);

            tmpl.SetAttribute("senderAccount", hasPrint.ContainsKey("senderAccount") ? hasPrint["senderAccount"].ToString() : "");
            tmpl.SetAttribute("senderBalance", hasPrint.ContainsKey("senderBalance") ? hasPrint["senderBalance"].ToString() : "");
            tmpl.SetAttribute("ccyid", hasPrint.ContainsKey("ccyid") ? hasPrint["ccyid"].ToString() : "");
            tmpl.SetAttribute("status", hasPrint.ContainsKey("status") ? hasPrint["status"].ToString() : "");
            tmpl.SetAttribute("senderName", hasPrint.ContainsKey("senderName") ? hasPrint["senderName"].ToString() : "");
            tmpl.SetAttribute("telecomname", hasPrint.ContainsKey("telecomname") ? hasPrint["telecomname"].ToString() : "");
            tmpl.SetAttribute("cardamount", hasPrint.ContainsKey("cardamount") ? hasPrint["cardamount"].ToString() : "");
            tmpl.SetAttribute("amount", hasPrint.ContainsKey("amount") ? hasPrint["amount"].ToString() : "");
            tmpl.SetAttribute("amountchu", hasPrint.ContainsKey("amountchu") ? hasPrint["amountchu"].ToString() : "");
            tmpl.SetAttribute("feeType", hasPrint.ContainsKey("feeType") ? hasPrint["feeType"].ToString() : "");
            tmpl.SetAttribute("feeAmount", hasPrint.ContainsKey("feeAmount") ? hasPrint["feeAmount"].ToString() : "");
            tmpl.SetAttribute("desc", hasPrint.ContainsKey("desc") ? hasPrint["desc"].ToString() : "");
            tmpl.SetAttribute("tranID", hasPrint.ContainsKey("tranID") ? hasPrint["tranID"].ToString() : "");
            tmpl.SetAttribute("tranDate", hasPrint.ContainsKey("tranDate") ? hasPrint["tranDate"].ToString() : "");

            try
            {
                tmpl.SetAttribute("softPin", hasPrint["softPin"].ToString());
            }
            catch (Exception e)
            {
                tmpl.SetAttribute("softPin", "");
                SmartPortal.Common.Log.RaiseError("", "", hasPrint["senderAccount"].ToString(), "softpin null", "");
            }


            DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["senderBranch"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtSenderBranch = dsSenderBranch.Tables[0];
                if (dtSenderBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("senderBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("senderBranch", "");
            }

            //lay chi nhanh nhan
            DataSet dsReceiverBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["receiverBranch"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtReceiverBranch = dsReceiverBranch.Tables[0];
                if (dtReceiverBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("receiverBranch", dtReceiverBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("receiverBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("receiverBranch", "");
            }

            SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
            SmartPortal.Common.Log.WriteLogFile("EMAIL NOTIFY", "", "", "  Send topup email success:  " + email + " TransID: " + hasPrint["tranID"].ToString() + " UserID: "
                                               + userId);
        }


        public static void BillPaymentSuccess_SendMail(Hashtable hasPrint, string userId)
        {

            string IPCERRORCODE = string.Empty;
            string IPCERRORDESC = string.Empty;

            string email = new SmartPortal.SEMS.User().GetUBID(userId).Rows[0]["EMAIL"].ToString();
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("BillPaymentSuccessful" + CultureInfo.CurrentCulture.Name);

            tmpl.SetAttribute("senderAccount", hasPrint["senderAccount"].ToString());
            tmpl.SetAttribute("senderBalance", hasPrint["senderBalance"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            tmpl.SetAttribute("status", hasPrint["status"].ToString());
            tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
            tmpl.SetAttribute("feeType", hasPrint["feeType"].ToString());
            tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
            tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
            tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
            tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());


            tmpl.SetAttribute("corpName", hasPrint["corpName"].ToString());
            tmpl.SetAttribute("serviceName", hasPrint["serviceName"].ToString());
            tmpl.SetAttribute("refvalue1", hasPrint["refvalue1"].ToString());
            tmpl.SetAttribute("refvalue2", hasPrint["refvalue2"].ToString());
            tmpl.SetAttribute("refindex1", hasPrint["refindex1"].ToString());
            tmpl.SetAttribute("refindex2", hasPrint["refindex2"].ToString());

            DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["senderBranch"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtSenderBranch = dsSenderBranch.Tables[0];
                if (dtSenderBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                }
            }
            else
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
            }

            SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
            SmartPortal.Common.Log.WriteLogFile("EMAIL NOTIFY", "", "", "  Send billpayment email success:  " + email + " TransID: " + hasPrint["tranID"].ToString() + " UserID: "
                                               + userId);
        }
        public static void AddShopSuccess_SendMail(Hashtable hasPrint)
        {

            string email = hasPrint["Email"].ToString();
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("AddShopSuccessful" + CultureInfo.CurrentCulture.Name);

            tmpl.SetAttribute("ContractNo", hasPrint["ContractNo"].ToString());
            tmpl.SetAttribute("ShopName", hasPrint["ShopName"].ToString());
            tmpl.SetAttribute("Email", hasPrint["Email"].ToString());
            tmpl.SetAttribute("PhoneNo", hasPrint["PhoneNo"].ToString());
            tmpl.SetAttribute("ShopCode", hasPrint["ShopCode"].ToString());
            tmpl.SetAttribute("Password", hasPrint["Password"].ToString());
            tmpl.SetAttribute("AcctNo", hasPrint["AcctNo"].ToString());
            tmpl.SetAttribute("SuspendAcctNo", hasPrint["SuspendAcctNo"].ToString());
            tmpl.SetAttribute("IncomAcctNo", hasPrint["IncomAcctNo"].ToString());
            tmpl.SetAttribute("SecretCode", hasPrint["SecretCode"].ToString());
            tmpl.SetAttribute("ShopID", hasPrint["ShopID"].ToString());

            SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
            SmartPortal.Common.Log.WriteLogFile("EMAIL NOTIFY", "", "", "  Send add shop email success:  " + email + " TransID: " + hasPrint["tranID"].ToString() + " UserID: "
                                       );
        }
        public static void CR_TransactionSuccess_SendMail(Hashtable hasPrint, string userId)
        {

            string IPCERRORCODE = string.Empty;
            string IPCERRORDESC = string.Empty;

            string email = new SmartPortal.SEMS.User().GetUBID(userId).Rows[0]["EMAIL"].ToString();
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("CR_TransactionSuccessful" + CultureInfo.CurrentCulture.Name);

            tmpl.SetAttribute("senderAccount", hasPrint.ContainsKey("senderAccount") ? hasPrint["senderAccount"].ToString() : "");
            tmpl.SetAttribute("senderBalance", hasPrint.ContainsKey("senderBalance") ? hasPrint["senderBalance"].ToString() : "");
            tmpl.SetAttribute("ccyid", hasPrint.ContainsKey("ccyid") ? hasPrint["ccyid"].ToString() : "");
            tmpl.SetAttribute("status", hasPrint.ContainsKey("status") ? hasPrint["status"].ToString() : "");
            tmpl.SetAttribute("senderName", hasPrint.ContainsKey("senderName") ? hasPrint["senderName"].ToString() : "");
            tmpl.SetAttribute("cardNo", hasPrint.ContainsKey("cardNo") ? hasPrint["cardNo"].ToString() : "");
            tmpl.SetAttribute("cardholdername", hasPrint.ContainsKey("cardholdername") ? hasPrint["cardholdername"].ToString() : "");
            tmpl.SetAttribute("outstandingamount", hasPrint.ContainsKey("outstandingamount") ? hasPrint["outstandingamount"].ToString() : "");
            tmpl.SetAttribute("amount", hasPrint.ContainsKey("amount") ? hasPrint["amount"].ToString() : "");
            tmpl.SetAttribute("amountchu", hasPrint.ContainsKey("amountchu") ? hasPrint["amountchu"].ToString() : "");
            tmpl.SetAttribute("feeType", hasPrint.ContainsKey("feeType") ? hasPrint["feeType"].ToString() : "");
            tmpl.SetAttribute("feeAmount", hasPrint.ContainsKey("feeAmount") ? hasPrint["feeAmount"].ToString() : "");
            tmpl.SetAttribute("desc", hasPrint.ContainsKey("desc") ? hasPrint["desc"].ToString() : "");
            tmpl.SetAttribute("tranID", hasPrint.ContainsKey("tranID") ? hasPrint["tranID"].ToString() : "");
            tmpl.SetAttribute("tranDate", hasPrint.ContainsKey("tranDate") ? hasPrint["tranDate"].ToString() : "");

            DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["senderBranch"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtSenderBranch = dsSenderBranch.Tables[0];
                if (dtSenderBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("senderBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("senderBranch", "");
            }

            ////lay chi nhanh nhan
            //DataSet dsReceiverBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["receiverBranch"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            //if (IPCERRORCODE == "0")
            //{
            //    DataTable dtReceiverBranch = dsReceiverBranch.Tables[0];
            //    if (dtReceiverBranch.Rows.Count != 0)
            //    {
            //        tmpl.SetAttribute("receiverBranch", dtReceiverBranch.Rows[0]["BRANCHNAME"].ToString());
            //    }
            //    else
            //    {
            //        tmpl.SetAttribute("receiverBranch", "");
            //    }
            //}
            //else
            //{
            //    tmpl.SetAttribute("receiverBranch", "");
            //}

            SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
            SmartPortal.Common.Log.WriteLogFile("EMAIL NOTIFY", "", "", "  Send transfer email success:  " + email + " TransID: " + hasPrint["tranID"].ToString() + " UserID: "
                                           + userId);
        }

        public static void CR_UpdatecardstatusSuccess_SendMail(Hashtable hasPrint, string userId)
        {

            string IPCERRORCODE = string.Empty;
            string IPCERRORDESC = string.Empty;

            string email = new SmartPortal.SEMS.User().GetUBID(userId).Rows[0]["EMAIL"].ToString();
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("CR_UpdatecardstatusSuccessful" + CultureInfo.CurrentCulture.Name);

            tmpl.SetAttribute("oldstatus", hasPrint.ContainsKey("oldstatus") ? hasPrint["oldstatus"].ToString() : "");
            tmpl.SetAttribute("newstatus", hasPrint.ContainsKey("newstatus") ? hasPrint["newstatus"].ToString() : "");

            tmpl.SetAttribute("status", hasPrint.ContainsKey("status") ? hasPrint["status"].ToString() : "");


            tmpl.SetAttribute("cardNo", hasPrint.ContainsKey("cardNo") ? hasPrint["cardNo"].ToString() : "");
            tmpl.SetAttribute("cardholdername", hasPrint.ContainsKey("cardholdername") ? hasPrint["cardholdername"].ToString() : "");
            tmpl.SetAttribute("outstandingamount", hasPrint.ContainsKey("outstandingamount") ? hasPrint["outstandingamount"].ToString() : "");

            tmpl.SetAttribute("tranID", hasPrint.ContainsKey("tranID") ? hasPrint["tranID"].ToString() : "");
            tmpl.SetAttribute("tranDate", hasPrint.ContainsKey("tranDate") ? hasPrint["tranDate"].ToString() : "");

            //DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["senderBranch"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            //if (IPCERRORCODE == "0")
            //{
            //    DataTable dtSenderBranch = dsSenderBranch.Tables[0];
            //    if (dtSenderBranch.Rows.Count != 0)
            //    {
            //        tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
            //    }
            //    else
            //    {
            //        tmpl.SetAttribute("senderBranch", "");
            //    }
            //}
            //else
            //{
            //    tmpl.SetAttribute("senderBranch", "");
            //}

            ////lay chi nhanh nhan
            //DataSet dsReceiverBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["receiverBranch"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            //if (IPCERRORCODE == "0")
            //{
            //    DataTable dtReceiverBranch = dsReceiverBranch.Tables[0];
            //    if (dtReceiverBranch.Rows.Count != 0)
            //    {
            //        tmpl.SetAttribute("receiverBranch", dtReceiverBranch.Rows[0]["BRANCHNAME"].ToString());
            //    }
            //    else
            //    {
            //        tmpl.SetAttribute("receiverBranch", "");
            //    }
            //}
            //else
            //{
            //    tmpl.SetAttribute("receiverBranch", "");
            //}

            SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
            SmartPortal.Common.Log.WriteLogFile("EMAIL NOTIFY", "", "", "  Send transfer email success:  " + email + " TransID: " + hasPrint["tranID"].ToString() + " UserID: "
                                           + userId);
        }

        public static void ETopupTransactionSuccess_SendMail(Hashtable hasPrint, string userId)
        {

            string IPCERRORCODE = string.Empty;
            string IPCERRORDESC = string.Empty;

            string email = new SmartPortal.SEMS.User().GetUBID(userId).Rows[0]["EMAIL"].ToString();
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("BuyETopUpSucsessfull" + CultureInfo.CurrentCulture.Name);

            tmpl.SetAttribute("senderAccount", hasPrint.ContainsKey("senderAccount") ? hasPrint["senderAccount"].ToString() : "");
            tmpl.SetAttribute("senderBalance", hasPrint.ContainsKey("senderBalance") ? hasPrint["senderBalance"].ToString() : "");
            tmpl.SetAttribute("ccyid", hasPrint.ContainsKey("ccyid") ? hasPrint["ccyid"].ToString() : "");
            tmpl.SetAttribute("status", hasPrint.ContainsKey("status") ? hasPrint["status"].ToString() : "");
            tmpl.SetAttribute("senderName", hasPrint.ContainsKey("senderName") ? hasPrint["senderName"].ToString() : "");
            tmpl.SetAttribute("telecomname", hasPrint.ContainsKey("telecomname") ? hasPrint["telecomname"].ToString() : "");
            tmpl.SetAttribute("cardamount", hasPrint.ContainsKey("cardamount") ? hasPrint["cardamount"].ToString() : "");
            tmpl.SetAttribute("amount", hasPrint.ContainsKey("amount") ? hasPrint["amount"].ToString() : "");
            tmpl.SetAttribute("amountchu", hasPrint.ContainsKey("amountchu") ? hasPrint["amountchu"].ToString() : "");
            tmpl.SetAttribute("feeType", hasPrint.ContainsKey("feeType") ? hasPrint["feeType"].ToString() : "");
            tmpl.SetAttribute("feeAmount", hasPrint.ContainsKey("feeAmount") ? hasPrint["feeAmount"].ToString() : "");
            tmpl.SetAttribute("desc", hasPrint.ContainsKey("desc") ? hasPrint["desc"].ToString() : "");
            tmpl.SetAttribute("tranID", hasPrint.ContainsKey("tranID") ? hasPrint["tranID"].ToString() : "");
            tmpl.SetAttribute("tranDate", hasPrint.ContainsKey("tranDate") ? hasPrint["tranDate"].ToString() : "");
            tmpl.SetAttribute("phoneNo", hasPrint.ContainsKey("phoneNo") ? hasPrint["phoneNo"].ToString() : "");


            DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["senderBranch"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtSenderBranch = dsSenderBranch.Tables[0];
                if (dtSenderBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("senderBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("senderBranch", "");
            }

            //lay chi nhanh nhan
            DataSet dsReceiverBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["receiverBranch"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtReceiverBranch = dsReceiverBranch.Tables[0];
                if (dtReceiverBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("receiverBranch", dtReceiverBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("receiverBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("receiverBranch", "");
            }

            SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
            SmartPortal.Common.Log.WriteLogFile("EMAIL NOTIFY", "", "", "  Send e-topup email success:  " + email + " TransID: " + hasPrint["tranID"].ToString() + " UserID: "
                                               + userId);
        }
        public static void GetInforsenMail(string sTranCode, string sTranID,ref DataTable dt)
        {
            try
            {
                dt = new DataTable();

                System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter();
                p1.ParameterName = "@TRANCODE";
                p1.Value = sTranCode;
                p1.SqlDbType = SqlDbType.VarChar;

                System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter();
                p2.ParameterName = "@TRANID";
                p2.Value = sTranID;
                p2.SqlDbType = SqlDbType.VarChar;

                dt = SmartPortal.DAL.DataAccess.GetFromDataTable("IB_GETINFORSENMAILBYTRANID", p1, p2);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.WriteLogFile("GetInforsenMail", "", "", ex.ToString());
            }
        }
        public static void GetListUserSendMail(string sTranID, string sUserID,ref DataTable dt)
        {
            dt = new DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter();
                p1.ParameterName = "@UID";
                p1.Value = sUserID;
                p1.SqlDbType = SqlDbType.VarChar;

                System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter();
                p2.ParameterName = "@TRANID";
                p2.Value = sTranID;
                p2.SqlDbType = SqlDbType.VarChar;

                dt = SmartPortal.DAL.DataAccess.GetFromDataTable("SEMS_EBA_USERS_GETUBID_BATCHTRANSFER_V2", p1, p2);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.WriteLogFile("GetInforsenMail", "", "", ex.ToString());
            }
        }
    }
}
