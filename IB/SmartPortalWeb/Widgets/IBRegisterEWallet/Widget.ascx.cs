using System;
using System.Linq;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using SmartPortal.IB;

using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBRegisterEWallet_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";

            load_unit(sender, e);

            //hide panel
            if (!IsPostBack)
            {
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                LoadAccountInfo();
            }
        }
        catch
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
            SmartPortal.IB.Account accountList = new SmartPortal.IB.Account();
            ds = accountList.getAccount(Session["userID"].ToString(), "", "", ref errorcode, ref errorDesc);
            if (ds.Tables[0].DefaultView.Count > 0)
            {
                if (!IsPostBack)
                {
                    ds.Tables[0].DefaultView.RowFilter = "accttype in ('DD', 'CD') and status = 'A'";
                    ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                    ddlSenderAccount.DataValueField = ds.Tables[0].Columns["ACCTNO"].ColumnName.ToString();
                    ddlSenderAccount.DataBind();
                }
            }
            else
            {
                throw new BusinessExeption("User not register DD Account To Transfer.");
            }

            DataSet dsEW = new SmartPortal.SEMS.Transactions().DoStored("EBA_EWALLET_SELECT", new object[] { "", "A" }, ref errorcode, ref errorDesc);
            if (errorcode.Equals("0"))
            {
                if (dsEW.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlWalletType.DataSource = dsEW.Tables[0];
                    ddlWalletType.DataTextField = "EWNAME";
                    ddlWalletType.DataValueField = "EWCODE";
                    ddlWalletType.DataBind();
                }
            }

        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], Request.Url.Query);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        string senderName = "";
        string balanceSender = "";
        string acctCCYID = "";

        try
        {
            string phoneNumber = txtPhoneNumber.Text.Trim();
            long phone;
            if(string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length <9 || phoneNumber.Length > 11 || !phoneNumber.StartsWith("09") || !long.TryParse(phoneNumber,out phone))
            {
                lblTextError.Text = Resources.labels.sodienthoaikhongdungdinhdangso;
                return;
            }
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.Text.Trim());
            if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                senderName = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                balanceSender = SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                acctCCYID = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
                lblSenderBranch.Text = hasSender[SmartPortal.Constant.IPC.BRANCHID].ToString();
            }
            else
            {
                lblTextError.Text = "Error : " + hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                return;
            }

            #region LOAD INFO
            lblSenderAccount.Text = ddlSenderAccount.Text;
            lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender, acctCCYID);
            lblSenderCCYID.Text = acctCCYID;
            lblSenderName.Text = senderName;

            lblWalletType.Text = ddlWalletType.SelectedItem.Text;
            lblPhonenumber.Text = phoneNumber;

            lblTextError.Text = "";
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
            #endregion
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBTransferInBAC1_Widget", "btnApply_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferInBAC1_Widget", "btnApply_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private Object m_lock = new Object();
    protected void btnAction_Click(object sender, EventArgs e)
    {

        Hashtable result = new Hashtable();
        string OTPcode = txtOTP.Text;
        txtOTP.Text = "";
        try
        {
            lock (m_lock)
            {
                SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
                string Desc = string.Format("iBanking register e-wallet account number {0}, phone number {1}, provider {2}", lblSenderAccount.Text.Trim(), lblPhonenumber.Text.Trim(), lblWalletType.Text.Trim());
                result = objTran.RegisterEWallet(Session["userID"].ToString(), lblSenderAccount.Text.Trim(), lblSenderName.Text.Trim(), ddlWalletType.SelectedValue.Trim(), lblWalletType.Text.Trim(), lblPhonenumber.Text.Trim(), lblSenderBranch.Text.Trim(), ddlLoaiXacThuc.SelectedValue.Trim(), txtOTP.Text.Trim(),Desc);


                if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {

                    lblTextError.Text = Resources.labels.transactionsuccessful;

                    //ghi vo session dung in

                    Hashtable hasPrint = new Hashtable();
                    hasPrint.Add("status", Resources.labels.thanhcong);
                    hasPrint.Add("senderAccount", lblSenderAccount.Text);
                    hasPrint.Add("senderBalance", lblBalanceSender.Text);
                    hasPrint.Add("ccyid", lblSenderCCYID.Text);
                    hasPrint.Add("senderName", lblSenderName.Text);
                    hasPrint.Add("tranID", result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : "");
                    hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    hasPrint.Add("RefID", result["REFID"].ToString());
                    hasPrint.Add("WalletType", lblWalletType.Text);
                    hasPrint.Add("PhoneNo", lblPhonenumber.Text);
                    hasPrint.Add("senderBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblSenderBranch.Text));
                    Session["print"] = hasPrint;

                    btnPrint.Visible = true;
                    btnView.Visible = true;

                    //send mail edit by VuTran
                    try
                    {
                        SmartPortal.Common.EmailHelper.RegisterEWalletSuccess_SendMail(hasPrint, Session["userID"].ToString());
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString() + "Error when send email from TranserInBAC1", Request.Url.Query);
                        //SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                        lblTextError.Text = "Transaction successful but can't send mail!";
                    }

                }
                else
                {
                    txtOTP.Text = "";
                    btnPrint.Visible = false;
                    btnView.Visible = false;
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
                        case "9906":
                            lblTextError.Text = Resources.labels.sotienquahanmuccuachinhanhghico;
                            return;
                        case "9905":
                            lblTextError.Text = Resources.labels.tongsotienvuotquahanmuccuachinhanhghico;
                            return;
                        case "9904":
                            lblTextError.Text = Resources.labels.sogiaodichtrongngayquahanmuccuachinhanhghico;
                            return;
                        case "-13524":
                            lblTextError.Text = Resources.labels.destacccountinvalid;
                            return;
                        default:
                            string _desc = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                            lblTextError.Text = string.IsNullOrEmpty(_desc) ? Resources.labels.transactionerror : _desc;
                            break;
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
                lblendSenderAccount.Text = lblSenderAccount.Text;
                lblEndSenderName.Text = lblSenderName.Text;

                lblEndWalletType.Text = lblWalletType.Text;
                lblEndPhoneNumber.Text = lblPhonenumber.Text;
                if (result["REFID"] != null)
                {
                    lblEndRefCode.Text = result["REFID"].ToString();
                }
                

                #endregion

                txtOTP.Text = "";
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnBackTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTIB.Visible = true;
            pnResultTransaction.Visible = false;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1088"));
    }
    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountInfo();
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
                case "0":
                    lblTextError.Text = "Send SMS OTP success."; btnAction.Enabled = true;
                    break;
                case "7003":
                    lblTextError.Text = "User does not register SMS OTP"; btnAction.Enabled = false;
                    break;
                default:
                    lblTextError.Text = errorDesc; btnAction.Enabled = false;
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
        btnSendOTP.Enabled = true;

    }
    protected void DropDownListOTP_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    private void LoadAccountInfo()
    {
        try
        {

            string account = ddlSenderAccount.SelectedItem.Text.ToString();
            string Acctype = ddlSenderAccount.SelectedItem.Value.ToString();
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string User = Session["userID"].ToString();
            Account acct = new Account();
            DataSet ds = new DataSet();
            ds = acct.GetInfoDD(User, account, ref ErrorCode, ref ErrorDesc);
            ShowDD(account, ds);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferBAC1_Widget", "LoadAccountInfo", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }


    private void ShowDD(string acctno, DataSet ds)
    {
        try
        {
            if (ds.Tables[0].Rows.Count == 1)
            {
                lblLastTranDate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.LASTTRANSDATE].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
                lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
                lblAvailableBalCCYID.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            //15.12.2015 minh add to fix error 9999 
            if (Session["print"] == null)
            {
               Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            //15.12.2015 minh add to fix error 9999 
            if (Session["print"] == null)
            {
               Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}
