using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBTransferFastBanking_Widget : WidgetBase
{
    //string senderName = "";
    //string receiverName = "";
    //string balanceSender = "";
    //string acctCCYID = "";
    string IPCERRORCODE;
    string IPCERRORDESC;
    DataSet dsReceiverList = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";

            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
            load_unit(sender, e);
            
            if (radTS.Checked)
            {
                txtTS.Enabled = true;
            }
            else
            {
                txtTS.Enabled = false;
            }
            //hide panel
            if (!IsPostBack)
            {
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;

                fastbanking_navigation1.Visible = true;
                fastbanking_navigation2.Visible = false;
                fastbanking_navigation3.Visible = false;
                fastbanking_navigation4.Visible = false;
            }
        }
        catch(Exception ex )
        {
        }
    }

    private void load_unit(object sender, EventArgs e)
    {
        try
        {
            string errorcode = "";
            string errorDesc = "";
            DataSet ds = new DataSet();
            DataSet dsshopinfo = new DataSet();

            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            ds = objAcct.getAccount(Session["userID"].ToString(), "IB000208", "", ref errorcode, ref errorDesc);
            ds.Tables[0].DefaultView.RowFilter = "accttype in ('DD', 'CD') and status = 'A'";

            dsshopinfo = SmartPortal.IB.FastBank.GetShopInfo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["fid"].ToString(), ref errorcode, ref errorDesc);
            
            if (!IsPostBack)
            {
               
                ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                ddlSenderAccount.DataValueField = ds.Tables[0].Columns[SmartPortal.Constant.IPC.ACCTNO].ColumnName.ToString();
                ddlSenderAccount.DataBind();

                //thong tin giao dich
                txtReceiverName.Text = dsshopinfo.Tables[0].Rows[0]["ShopName"].ToString();
                lblCurrency.Text = dsshopinfo.Tables[0].Rows[0]["CCYID"].ToString();
                txtAmount.Text=SmartPortal.Common.Utilities.Utility.FormatMoney(dsshopinfo.Tables[0].Rows[0]["Amount"].ToString(),lblCurrency.Text);
                txtDesc.Text = dsshopinfo.Tables[0].Rows[0]["TranDesc"].ToString();
                txtReceiverAccount.Text = dsshopinfo.Tables[0].Rows[0]["ShopAcctNo"].ToString();
                //lay CCYID
                LayCCYID();
            }
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], Request.Url.Query);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
        
    }
    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        string senderName = "";
        string receiverName = "";
        string balanceSender = "";
        string acctCCYID = "";
        string receiverCCYID = "";

        fastbanking_navigation1.Visible = false;
        fastbanking_navigation2.Visible = true;
        fastbanking_navigation3.Visible = false;
        fastbanking_navigation4.Visible = false;

        try
        {
            if (true)
            {
                //CHECK RECEIVER ACCOUNT IS EXISTS
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                DataSet dsAcct = objAcct.CheckAccountExists(txtReceiverAccount.Text.Trim().ToString());
                if (dsAcct.Tables.Count != 0)
                {
                    if (dsAcct.Tables[0].Rows.Count > 0)
                    {
                        receiverName = dsAcct.Tables[0].Rows[0][SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                        receiverCCYID = dsAcct.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                    }
                    else
                    {
                        lblTextError.Text = Resources.labels.destacccountinvalid;
                        return;
                    }
                }
                else
                {
                    lblTextError.Text = Resources.labels.destacccountinvalid;
                    return;
                }

                Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.Text.Trim());
                if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    senderName = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                    balanceSender =SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                    acctCCYID = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
                    lblSenderBranch.Text = hasSender[SmartPortal.Constant.IPC.BRANCHID].ToString();
                }
                else
                {
                    lblTextError.Text = "Error : " + hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }

                Hashtable hasReceiver = objAcct.loadInfobyAcct(txtReceiverAccount.Text.Trim());
                if (hasReceiver[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    lblReceiverBranch.Text = hasReceiver[SmartPortal.Constant.IPC.BRANCHID].ToString();
                }
                else
                {
                    lblTextError.Text = "Error : " + hasReceiver[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }
                // CHECK SAME CCYCD
                bool sameCCYCD = objAcct.CheckSameCCYCD(acctCCYID, receiverCCYID);
                if (!sameCCYCD)
                {
                    lblTextError.Text = Resources.labels.invalidacctCCYCDtotransfer;
                    return;
                }

                // CHECK AMOUNT
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(),true) > SmartPortal.Common.Utilities.Utility.isDouble(balanceSender,true))
                {
                    lblTextError.Text = Resources.labels.amountinvalid;
                    return;
                }
                // CHECK SAME ACCOUNT
                if (!txtReceiverAccount.Text.Equals(""))
                {
                    bool sameAcct = objAcct.CheckSameAccount(ddlSenderAccount.Text.ToString(), txtReceiverAccount.Text.Trim().ToString());
                    if (!sameAcct)
                    {
                        lblTextError.Text = Resources.labels.Accountnotsame;
                        return;
                    }
                }
                
                #region LOAD INFO
                lblSenderAccount.Text = ddlSenderAccount.Text;
                lblSenderName.Text = senderName;
                lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender, acctCCYID);
                lblSenderCCYID.Text = acctCCYID;
                lblFeeCCYID.Text = acctCCYID;
                lbCCYID.Text = acctCCYID;

                lblReceiverAccount.Text = txtReceiverAccount.Text;
                lblReceiverName.Text = txtReceiverName.Text;

                if (radTS1.Checked == true)
                {
                    //lblHinhThuc.Text = radTS1.Text;
                }
                else
                {
                    string scheduleTime = txtTS.Text.ToString() + " " + ConfigurationManager.AppSettings.Get("SCHEDULETIME");
                    if (Convert.ToDateTime(scheduleTime) < DateTime.Now)
                    {
                        lblTextError.Text = Resources.labels.ngaygiaodichkhonghople;
                        return;
                    }
                    //lblHinhThuc.Text = txtTS.Text.Trim().ToString();
                }


                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text.Trim(),acctCCYID); 
                lblPhi.Text = rdNguoiChiuPhi.SelectedValue;
                lblDesc.Text =SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text);

                #region tinh phi
                //edit by VuTran 19/09/2014: tinh lai phi
                string senderfee = "0";
                string receiverfee = "0";

                DataTable dtFee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), "IB000208", SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ddlSenderAccount.SelectedValue, lblReceiverBranch.Text.Trim(), lblCurrency.Text, "");

                if (dtFee.Rows.Count != 0)
                {
                    senderfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeSenderAmt"].ToString(), acctCCYID);
                    receiverfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeReceiverAmt"].ToString(), acctCCYID);
                }
                #endregion

                lblPhiAmount.Text = (Double.Parse(receiverfee) != 0) ? receiverfee : senderfee;
                lblPhi.Text = (Double.Parse(receiverfee)!= 0) ? "Receiver" : "Sender";

                lblTextError.Text = "";
                pnConfirm.Visible = true;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = false;

                #endregion
                

               
            }
        else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.UNNAMEDTRANSFERTEMPLATE);
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    
    protected void btnApply_Click(object sender, EventArgs e)
    {
        fastbanking_navigation1.Visible = false;
        fastbanking_navigation2.Visible = false;
        fastbanking_navigation3.Visible = true;
        fastbanking_navigation4.Visible = false;

        try
        {
            if (int.Parse(Session["userLevel"].ToString().Trim()) > 2)
            {
                btnApply.Text = Resources.labels.transfer;
                //chuyen khoan luon doi voi user doanh nghiep level3 tro len
                btnAction_Click(sender, e);
            }
            else
            {
                btnApply.Text = Resources.labels.confirm;
                pnConfirm.Visible = false;
                pnOTP.Visible = true;
                pnTIB.Visible = false;

                SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
                DataTable dt = new DataTable();
                dt = objTran.LoadAuthenType(Session["userID"].ToString());
                ddlLoaiXacThuc.DataSource = dt;
                ddlLoaiXacThuc.DataTextField = "TYPENAME";
                ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
                ddlLoaiXacThuc.DataBind();
            }
            //edit by vutran 18/08/2014: sua loi ko hien nut send neu chi co SMSOTP
            if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
            {
                btnSendOTP.Visible = true;
            }
            else
            {
                btnSendOTP.Visible = false;
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBTransferInBank1_Widget", "btnApply_Click", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferInBank1_Widget", "btnApply_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private Object m_lock =new Object();
    protected void btnAction_Click(object sender, EventArgs e)
    {
        fastbanking_navigation1.Visible = false;
        fastbanking_navigation2.Visible = false;
        fastbanking_navigation3.Visible = false;
        fastbanking_navigation4.Visible = true;

        lock(m_lock)
        {
        Hashtable result = new Hashtable();
        string OTPcode = txtOTP.Text;
        string orderCode = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["fid"].ToString();
        txtOTP.Text = "";
        try
        {
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            //result = objAcct.TransferDDOtherCust(Session["userID"].ToString(), lblSenderAccount.Text, lblReceiverAccount.Text,SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text,lblCurrency.Text.Trim()), lblSenderCCYID.Text,lblSenderName.Text, lblReceiverName.Text,lblSenderBranch.Text,lblReceiverBranch.Text, lblDesc.Text,ddlLoaiXacThuc.SelectedValue,OTPcode.Trim(),SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblPhiAmount.Text.Trim(),lblCurrency.Text.Trim()),lblPhi.Text);
            result = SmartPortal.IB.FastBank.TransferFastBanking(orderCode, Session["userID"].ToString(), lblSenderAccount.Text, lblSenderName.Text, lblSenderBranch.Text, ddlLoaiXacThuc.SelectedValue, OTPcode.Trim(), lblDesc.Text);


            
            if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                SmartPortal.IB.FastBank.UpdateTransactionResult(orderCode, result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString(), result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString(), "S");
                string password = SmartPortal.Security.Encryption.Decrypt(SmartPortal.IB.FastBank.GetShopPasswodByOrddercode(orderCode).ToString());
                string res = "ordercode=" + orderCode + "&result=OK&ErrorDesc=" + result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString() + "&SessionID=" + result[SmartPortal.Constant.IPC.SESSIONID].ToString();
                lblReturnUrl.Text = result[SmartPortal.Constant.IPC.RETURNURL].ToString() + "?fresult=" + SmartPortal.Common.Encrypt.EncryptDataFB(res,password);
                SmartPortal.Common.Log.WriteLogFile("0", "", "", lblReturnUrl.Text);
                btnContinue.Visible = true;

                lblTextError.Text = Resources.labels.transactionsuccessful;
                //ghi vo session dung in
                Hashtable hasPrint = new Hashtable();
                hasPrint.Add("status", Resources.labels.thanhcong);
                hasPrint.Add("senderAccount", lblSenderAccount.Text);
                hasPrint.Add("senderBalance", lblBalanceSender.Text);
                hasPrint.Add("ccyid", lblSenderCCYID.Text);
                hasPrint.Add("senderName", lblSenderName.Text);
                hasPrint.Add("recieverAccount", lblReceiverAccount.Text);
                hasPrint.Add("recieverName", lblReceiverName.Text);
                //hasPrint.Add("transferType", lblHinhThuc.Text);
                hasPrint.Add("amount", lblAmount.Text);
                hasPrint.Add("amountchu", txtChu.Value.ToString());
                hasPrint.Add("feeType", lblPhi.Text);
                hasPrint.Add("feeAmount", lblPhiAmount.Text);
                hasPrint.Add("desc", lblDesc.Text);
                hasPrint.Add("tranID", result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : "");
                hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                hasPrint.Add("senderBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblSenderBranch.Text));
                hasPrint.Add("receiverBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblReceiverBranch.Text));
                Session["print"] = hasPrint;


                //send mail by Vu Tran
                try
                {
                    SmartPortal.Common.EmailHelper.TransactionSuccess_SendMail(hasPrint, Session["userID"].ToString());
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString() + ex.Message + "Error when send email from TranserInBank1", Request.Url.Query);
                    //SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    lblTextError.Text = "fastbank transaction success but can't send mail!";
                }
            }
            else
            {
                SmartPortal.IB.FastBank.UpdateTransactionResult(orderCode, result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString(), result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString(), "F");
                txtOTP.Text = "";

                switch (result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                        lblTextError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                        return;                        
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                        lblTextError.Text = Resources.labels.wattingbankapprove;                        
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGPARTOWNERAPPROVE:
                        lblTextError.Text = Resources.labels.wattingpartownerapprove;
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                        lblTextError.Text = Resources.labels.wattinguserapprove;
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                        lblTextError.Text = Resources.labels.notregotp;
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                        lblTextError.Text = Resources.labels.authentypeinvalid;
                        return;
                    case "9908":
                        lblTextError.Text = Resources.labels.sotienvuothanmucgiaodich;
                        return;
                    case "9909":
                        lblTextError.Text = Resources.labels.sotienvuotquatonghanmuctrongngay;
                        return;
                    case "9907":
                        lblTextError.Text = Resources.labels.solangiaodichvuotquasolangiaodichtrongngay;
                        return;
                    case "-13524":
                        lblTextError.Text = Resources.labels.destacccountinvalid;
                        return;
                    default:
                        throw new IPCException(result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim());
                }


                pnConfirm.Visible = true;
                btnApply.Enabled = false;
                btnBackTransfer.Enabled = false;
            }

            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = true;

           
            #region LOAD RESULT TRANSFER
            string errorCode = "0";
            string errorDesc = string.Empty;
            SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
            DataSet dsDetailAcc = new DataSet();
            dsDetailAcc = acct.GetInfoDD(Session["userID"].ToString(), lblSenderAccount.Text.Trim(), ref errorCode, ref errorDesc);

            if (result["IPCTRANSID"] != null)
            {
                lblEndTransactionNo.Visible = true;
                lblEndTransactionNo.Text = result["IPCTRANSID"].ToString();
            }
            else
            {
                lblEndTransactionNo.Visible = false;
            }
            
            lblEndDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            lblEndSenderAccount.Text = lblSenderAccount.Text;
            lblEndBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), lblSenderCCYID.Text.Trim());
            lblEndSenderName.Text = lblSenderName.Text;

            lblEndReceiverAccount.Text = lblReceiverAccount.Text;
            lblEndReceiverName.Text = lblReceiverName.Text;
            //lblEndHinhThuc.Text = lblHinhThuc.Text;
            lblEndAmount.Text = lblAmount.Text;
            lblEndPhi.Text = lblPhi.Text;
            lblEndPhiAmount.Text = lblPhiAmount.Text;
            lblEndDesc.Text = lblDesc.Text;
            lblEndPhiAmount.Text = lblPhiAmount.Text;

            lblBalanceCCYID.Text = lblSenderCCYID.Text;
            lblAmountCCYID.Text = lblSenderCCYID.Text;
            lblEndFeeCCYID.Text = lblSenderCCYID.Text;
            #endregion
            txtOTP.Text = "";
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
        }
        
    }
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        Response.Redirect(lblReturnUrl.Text);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        fastbanking_navigation1.Visible = false;
        fastbanking_navigation2.Visible = true;
        fastbanking_navigation3.Visible = false;
        fastbanking_navigation4.Visible = false;
        try
        {
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
        }
        catch ( Exception ex )
        {
        }
    }
    protected void btnBackTransfer_Click(object sender, EventArgs e)
    {
        fastbanking_navigation1.Visible = true;
        fastbanking_navigation2.Visible = false;
        fastbanking_navigation3.Visible = false;
        fastbanking_navigation4.Visible = false;
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTIB.Visible = true;
            pnResultTransaction.Visible = false;
        }
        catch ( Exception ex )
        {
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=89"));
    }
    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LayCCYID();
    }

    public void LayCCYID()
    {
        DataTable tblAcctnoInfo = new SmartPortal.IB.Account().GetAcctnoInfo(ddlSenderAccount.SelectedValue, Session["Userid"].ToString());
        if (tblAcctnoInfo.Rows.Count != 0)
        {
            lblCurrency.Text = tblAcctnoInfo.Rows[0]["CCYID"].ToString();
        }
    }
    //edit by vutran 06082014: send SMSOTP
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        string errorcode = "";
        string errorDesc = "";
        try
        {

            btnSendOTP.Text = "ReSend";
            SmartPortal.SEMS.OTP.SendSMSOTP(Session["userID"].ToString(), ref errorcode, ref errorDesc);
            switch (errorcode)
            {
                case "0": lblTextError.Text = "Send SMS OTP success."; btnAction.Enabled = true;
                    break;
                case "7003": lblTextError.Text = "User does not register SMS OTP"; btnAction.Enabled = false;
                    break;
                default: lblTextError.Text = errorDesc; btnAction.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void DropDownListOTP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
        {
            btnSendOTP.Visible = true;
            if (btnSendOTP.Text == "ReSend")
            {
                btnAction.Enabled = true;
            }
            else
            {
                btnAction.Enabled = false;
            }
        }
        else
        {
            btnAction.Enabled = true;
            btnSendOTP.Visible = false;
        }
    }
}
