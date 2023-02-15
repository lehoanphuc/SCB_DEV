using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net.Core;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.IB;

public partial class Widgets_IBOpenBankAct_Widget : WidgetBase
{
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    Customer ct = new Customer();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblError.ClientID + "',event)");

            if (!IsPostBack)
            {

                DataSet ds = ct.GetInfo("IB_GETCUSTINFO", new object[] { Session["userID"].ToString() }, ref IPCERRORCODE, ref IPCERRORDESC);
                if (ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
                DataTable dataTable = ds.Tables[0];
                hdFullName.Value = lblAccountName.Text = dataTable.Rows[0]["FULLNAME"].ToString();
                hdAddress.Value = lblAddress.Text = dataTable.Rows[0]["ADDRESS"].ToString();
                hdPhone.Value = lblPhone.Text = dataTable.Rows[0]["PHONE"].ToString();
                hdEmail.Value = lblEmail.Text = dataTable.Rows[0]["EMAIL"].ToString();
                string birthDate = SmartPortal.Common.Utilities.Utility.FormatDatetime(dataTable.Rows[0]["BIRTHDAY"].ToString(), "dd/MM/yyyy");
                hdBirthday.Value = lblBirthday.Text = birthDate.Equals("01/01/1900") ? "Unkown" : birthDate;
                hdNRC.Value = lblNRC.Text = dataTable.Rows[0]["LICENSEID"].ToString();

                DataSet dsAcctType = ct.SysVarGetByParGrp("DPTTYPE", ref IPCERRORCODE, ref IPCERRORDESC);
                ddlActType.DataSource = dsAcctType;
                ddlActType.DataTextField = "VARNAME";
                ddlActType.DataValueField = "VARVALUE";
                ddlActType.DataBind();
                ddlActType.SelectedValue = "F";
                ddlActType.Enabled = false;

                DataSet dsTerm = ct.getTerm(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlTerm.DataSource = dsTerm;
                ddlTerm.DataTextField = "TERMDESC";
                ddlTerm.DataValueField = "TERM";
                ddlTerm.DataBind();

                SmartPortal.IB.Account accountList = new SmartPortal.IB.Account();
                ds = accountList.GetListOfAccounts(Session["userID"].ToString(), "IB_OPENBANKACT", "", "DD,CD", "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (ds.Tables[0].DefaultView.Count > 0)
                {
                    ds.Tables[0].DefaultView.RowFilter = "TYPEID IN ('DD', 'CD') AND STATUSCD in ('A')";
                    ddlDebitAccount.DataSource = ds.Tables[0].DefaultView;
                    ddlDebitAccount.DataValueField = ds.Tables[0].Columns["UNIQUEID"].ColumnName.ToString();
                    ddlDebitAccount.DataTextField = ds.Tables[0].Columns["ACCOUNTNO"].ColumnName.ToString();
                    ddlDebitAccount.DataBind();
                    LoadAccountInfo();
                }
                ds = accountList.GetListOfAccounts(Session["userID"].ToString(), "IB_OPENBANKACT", "", "", "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (ds.Tables[0].DefaultView.Count > 0)
                {
                    ds.Tables[0].DefaultView.RowFilter = "TYPEID IN ('FD') AND STATUSCD in ('A')";
                    ddlAccountNoClose.DataSource = ds.Tables[0].DefaultView;
                    ddlAccountNoClose.DataValueField = ds.Tables[0].Columns["UNIQUEID"].ColumnName.ToString();
                    ddlAccountNoClose.DataTextField = ds.Tables[0].Columns["ACCOUNTNO"].ColumnName.ToString();
                    ddlAccountNoClose.DataBind();
                    LoadAccountInfo();
                }
                ddlLoadCategory_SelectedIndexChanged(null, null);
            }
            ddltype_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountInfo();
        ddlLoadCategory_SelectedIndexChanged(null, null);
    }
    protected void ddlLoadCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string culture = Session["langID"] == null ? new PortalSettings().portalSetting.DefaultLang : Session["langID"].ToString();
            ddlCatcode.Items.Clear();
            DataSet dsCodee = ct.getCategoryOpenBank(ddlTerm.SelectedValue, lblAvailableBalCCYID.Text, culture, ref IPCERRORCODE, ref IPCERRORDESC);
            ddlCatcode.DataSource = dsCodee;
            ddlCatcode.DataTextField = "TYPENAME";
            ddlCatcode.DataValueField = "ACTYPE";
            ddlCatcode.DataBind();
        }
        catch
        {

        }

    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue.Equals("O"))
        {
            lblError.Text = "";
            OpenAccount.Visible = true;
            pnClose.Visible = false;
        }
        else
        {
            if(ddlAccountNoClose.Items.Count  < 1)
            {
                lblError.Text = "You have no FD- Fixed deposit";
                OpenAccount.Visible = true;
                pnClose.Visible = false;
                ddltype.SelectedValue = "O";
                return;
            }
            OpenAccount.Visible = false;
            pnClose.Visible = true;
            ddlAccountNoClose_SelectedIndexChanged(sender, e);
        }
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        try
        {
            string OTPcode = txtOTP.Text;
            string amount = txtAmount.Text;
            if (ddltype.SelectedValue.Equals("O"))
            {
                Hashtable hdresult = ct.requestOpenBankAcct(Session["userID"].ToString(), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblAvailableBalCCYID.Text), Utility.KillSqlInjection(lblAvailableBalCCYID.Text.Trim()), ddlDebitAccount.SelectedValue, Utility.KillSqlInjection(ddlActType.SelectedValue.Trim()), Utility.KillSqlInjection(ddlCatcode.SelectedValue.Trim()), Utility.KillSqlInjection(txtDesc.Text.Trim()), OTPcode, ddlLoaiXacThuc.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE.Equals("0"))
                {
                    LoadAccountInfo();
                    lblAccountNameRS.Text = hdFullName.Value;
                    lblBirthdayRS.Text = hdBirthday.Value;
                    lblAddressRS.Text = hdAddress.Value;
                    lblNRCRS.Text = hdNRC.Value;
                    lblEmailRS.Text = hdEmail.Value;
                    lblPhoneRS.Text = hdPhone.Value;
                    lblDebitAccountRS.Text = ddlDebitAccount.SelectedItem.Text;
                    lblActTypeRS.Text = ddlActType.SelectedItem.Text;
                    lblCatcodeRS.Text = hdresult["CATNAME"].ToString(); ;
                    lblAmountRS.Text = hdAmount.Value;
                    lbldescRS.Text = txtDesc.Text;
                    lblAvailableBalResult.Text = lblAvailableBal.Text;
                    pnOTP.Visible = false;
                    pnResult.Visible = true;
                    rsOpen.Visible = true;
                    lblError.Text = Resources.labels.requestopenbankactsuccess;
                    lblAccountNoOpen.Text = hdresult["ACCTNO"].ToString();
                    lblTranID.Text = hdresult["IPCTRANSID"] != null ? hdresult["IPCTRANSID"].ToString() : "";
                    lblEndDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    //ghi vo session dung in
                    btnPrint.Visible = true;
                    Hashtable hasPrint = new Hashtable();
                    hasPrint.Add("accountType", "Fix deposit");
                    hasPrint.Add("debitAccount", lblDebitAccountRS.Text);
                    hasPrint.Add("availableBalance", lblAvailableBalResult.Text);
                    hasPrint.Add("category", lblCatcodeRS.Text);
                    hasPrint.Add("amount", lblAmountRS.Text);
                    hasPrint.Add("accountNoOpen", lblAccountNoOpen.Text);
                    hasPrint.Add("desc", lbldescRS.Text);
                    hasPrint.Add("tranID", hdresult["IPCTRANSID"] != null ? hdresult["IPCTRANSID"].ToString() : "");
                    hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    Session["print"] = hasPrint;
                }
                else
                {
                    txtOTP.Text = "";
                    btnPrint.Visible = false;
                    switch (hdresult[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblError.Text = hdresult[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                            if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                            }
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                            lblError.Text = Resources.labels.wattingbankapprove;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGPARTOWNERAPPROVE:
                            lblError.Text = Resources.labels.wattingpartownerapprove;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                            lblError.Text = Resources.labels.wattinguserapprove;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                            lblError.Text = hdresult[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        default:
                            lblError.Text = string.IsNullOrEmpty(hdresult[SmartPortal.Constant.IPC.IPCERRORDESC].ToString()) ? "Transaction error!" : hdresult[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                            return;
                    }
                    lblAccountNameRS.Text = hdFullName.Value;
                    lblBirthdayRS.Text = hdBirthday.Value;
                    lblAddressRS.Text = hdAddress.Value;
                    lblNRCRS.Text = hdNRC.Value;
                    lblEmailRS.Text = hdEmail.Value;
                    lblPhoneRS.Text = hdPhone.Value;
                    lblDebitAccountRS.Text = ddlDebitAccount.SelectedItem.Text;
                    lblActTypeRS.Text = ddlActType.SelectedItem.Text;
                    lblCatcodeRS.Text = ddlCatcode.SelectedItem.Text;
                    lblAmountRS.Text = hdAmount.Value;
                    lbldescRS.Text = txtDesc.Text;
                    lblAvailableBalResult.Text = lblAvailableBal.Text;
                    pnOTP.Visible = false;
                    pnResult.Visible = true;
                    rsOpen.Visible = true;
                    TranNoResult.Visible = false;
                    TimeResult.Visible = false;
                }
            }
            else
            {
                Hashtable hdresult = ct.CloseBankAcct(Session["userID"].ToString(), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(hdclosebalance.Value.Trim(), hdcloseccyid.Value), Utility.KillSqlInjection(hdcloseccyid.Value.Trim()) , hdfdreceipt.Value, ddlAccountNoClose.SelectedValue, lblCreditAccountCfmCL.Text.Trim(), Utility.KillSqlInjection(txtDescClose.Text.Trim()), OTPcode, ddlLoaiXacThuc.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE.Equals("0"))
                {
                    lblAccountNameRS.Text = hdFullName.Value;
                    lblBirthdayRS.Text = hdBirthday.Value;
                    lblAddressRS.Text = hdAddress.Value;
                    lblNRCRS.Text = hdNRC.Value;
                    lblEmailRS.Text = hdEmail.Value;
                    lblPhoneRS.Text = hdPhone.Value;
                    btnPrint.Visible = false;
                    pnOTP.Visible = false;
                    rsClose.Visible = true;
                    pnResult.Visible = true;
                    lblError.Text = Resources.labels.closebankactsuccess;
                    lblTranID.Text = hdresult["IPCTRANSID"] != null ? hdresult["IPCTRANSID"].ToString() : "";
                    lblEndDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    string ErrorCode = string.Empty;
                    string ErrorDesc = string.Empty;
                    string User = Session["userID"].ToString();
                    Account acct = new Account();
                    Hashtable ht = new Hashtable();
                    ht = acct.GetInfoAccount(User, lblCreditAccountCfmCL.Text, ref ErrorCode, ref ErrorDesc);
                    lblrsCreditAcount.Text = lblCreditAccountCfmCL.Text;
                    lblDebitConfirmClose.Text = lblDebitAccountCfmCL.Text;
                    if (ht[SmartPortal.Constant.IPC.CCYID] != null)
                    {
                        lblrsCurency.Text = ht[SmartPortal.Constant.IPC.CCYID].ToString();
                    }
                    if (ht[SmartPortal.Constant.IPC.AVAILABLEBALANCE] != null)
                    {
                        lblrsCreditBalance.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ht[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString().Trim()), ht[SmartPortal.Constant.IPC.CCYID].ToString().Trim());
                    }

                }
                else
                {
                    txtOTP.Text = "";
                    btnPrint.Visible = false;
                    switch (hdresult[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblError.Text = hdresult[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                            if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                            }
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                            lblError.Text = Resources.labels.wattingbankapprove;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGPARTOWNERAPPROVE:
                            lblError.Text = Resources.labels.wattingpartownerapprove;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                            lblError.Text = Resources.labels.wattinguserapprove;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                            lblError.Text = hdresult[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        default:
                            lblError.Text = string.IsNullOrEmpty(hdresult[SmartPortal.Constant.IPC.IPCERRORDESC].ToString()) ? "Transaction error!" : hdresult[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                            return;
                    }
                    TranNoResult.Visible = false;
                    TimeResult.Visible = false;
                    lblAccountNameRS.Text = hdFullName.Value;
                    lblBirthdayRS.Text = hdBirthday.Value;
                    lblAddressRS.Text = hdAddress.Value;
                    lblNRCRS.Text = hdNRC.Value;
                    lblEmailRS.Text = hdEmail.Value;
                    lblPhoneRS.Text = hdPhone.Value;
                    btnPrint.Visible = false;
                    pnOTP.Visible = false;
                    rsClose.Visible = true;
                    pnResult.Visible = true;
                    lblrsCreditAcount.Text = lblCreditAccountCfmCL.Text;
                    lblDebitConfirmClose.Text = lblDebitAccountCfmCL.Text;
                    lblrsCreditBalance.Text = lblTotalCfmCL.Text;

                }
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnBackRs_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=86"));
    }
    protected void btnBackOTP_Click(object sender, EventArgs e)
    {
        try
        {
            txtOTP.Text = "";
            if (ddltype.SelectedValue.Equals("O"))
            {
                pnComfirm.Visible = true;
            }
            else
            {
                PnComfirmClose.Visible = true;
            }
            pnOTP.Visible = false;
            pnContent.Visible = false;
            pnResult.Visible = false;
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
    protected void ddlLoaiXacThuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAction.Enabled = false;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
    }
    private void LoadLoaiXacThuc()
    {
        try
        {
            ViewState["isSendFirst"] = 1;
            btnAction.Enabled = false;
            SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
            DataTable dt = objTran.LoadAuthenType(Session["userID"].ToString());
            ddlLoaiXacThuc.DataSource = dt;
            ddlLoaiXacThuc.DataTextField = "TYPENAME";
            ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
            ddlLoaiXacThuc.DataBind();

            btnSendOTP.Text = Resources.labels.send;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            ddlLoaiXacThuc.Enabled = false;
            btnAction.Enabled = true;
            btnSendOTP.Text = Resources.labels.resend;
            if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
            {
                SmartPortal.SEMS.OTP.SendSMSOTP(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            }
            else
            {
                SmartPortal.SEMS.OTP.SendSMSOTPCORP(Session["userID"].ToString(), ddlLoaiXacThuc.SelectedValue.ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                lbnotice.Visible = true;
            }
            if (IPCERRORCODE != "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
            }
            if (IPCERRORCODE != "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
            }
            switch (IPCERRORCODE)
            {
                case "0":
                    int time;
                    if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
                    {
                        time = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OTPtimeexpires"]) ? int.Parse(ConfigurationManager.AppSettings["OTPtimeexpires"].ToString()) : 20;
                    }
                    else
                    {
                        time = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OTPCorptimeexpires"]) ? int.Parse(ConfigurationManager.AppSettings["OTPCorptimeexpires"].ToString()) : 20;
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "countdownOTP(" + time + ");", true);
                    ViewState["timeReSendOTP"] = DateTime.Now.AddSeconds(time);
                    break;
                case "7003":
                    lblError.Text = Resources.labels.notregotp;
                    btnAction.Enabled = false;
                    break;
                default:
                    lblError.Text = IPCERRORDESC;
                    btnAction.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnBackCfm_Click(object sender, EventArgs e)
    {
        try
        {
            pnComfirm.Visible = false;
            pnOTP.Visible = false;
            pnContent.Visible = true;
            pnResult.Visible = false;
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
    protected void btnBackCfmClose_Click(object sender, EventArgs e)
    {
        try
        {
            pnComfirm.Visible = false;
            pnOTP.Visible = false;
            PnComfirmClose.Visible = false;
            pnClose.Visible = true;
            pnContent.Visible = true;
            pnResult.Visible = false;
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
    private void LoadAccountInfo()
    {
        try
        {

            string account = ddlDebitAccount.SelectedItem.Value.ToString();
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string User = Session["userID"].ToString();
            Account acct = new Account();
            Hashtable ht = new Hashtable();
            ht = acct.GetInfoAccount(User, account, ref ErrorCode, ref ErrorDesc);
            ShowDD(account, ht);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferBAC1_Widget", "LoadAccountInfo", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void ShowDD(string acctno, Hashtable htact)
    {
        try
        {
            string CurrencyCode = string.Empty;
            if (htact[SmartPortal.Constant.IPC.CCYID] != null)
            {
                lblAvailableBalCCYID.Text = htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim();
                lblAvailableBalCCYID.Text = lblAvailableBalCCYID.Text;
                switch (htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim())
                {
                    case "LAK": CurrencyCode = "CATCODELAK"; break;
                    case "USD": CurrencyCode = "CATCODEUSD"; break;
                    case "THB": CurrencyCode = "CATCODETHB"; break;
                }
            }
            if (htact[SmartPortal.Constant.IPC.AVAILABLEBALANCE] != null)
            {
                hdBalanceSender.Value = SmartPortal.Common.Utilities.Utility.FormatStringCore(htact[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString().Trim());
                lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(hdBalanceSender.Value, htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim());

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
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["isSendFirst"] == null || (int)ViewState["isSendFirst"] == 0)
            {
                LoadLoaiXacThuc();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
            }
            pnComfirm.Visible = false;
            pnOTP.Visible = true;
            pnContent.Visible = false;
            pnResult.Visible = false;
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
    protected void btnConfirmClose_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["isSendFirst"] == null || (int)ViewState["isSendFirst"] == 0)
            {
                LoadLoaiXacThuc();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
            }
            pnComfirm.Visible = false;
            PnComfirmClose.Visible = false;
            pnOTP.Visible = true;
            pnContent.Visible = false;
            pnResult.Visible = false;
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
    protected void btnContineClose_Click(object sender, EventArgs e)
    {
        try
        {
            Hashtable ht = new Hashtable();
            ht = ct.GetInfoAccountClose(ddlAccountNoClose.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                try
                {
                    PnComfirmClose.Visible = true;
                    pnComfirm.Visible = false;
                    pnClose.Visible = false;
                    pnOTP.Visible = false;
                    pnContent.Visible = false;
                    pnResult.Visible = false;
                    //set confirm
                    lblAccountNameCfmCL.Text = hdFullName.Value;
                    lblBirthdayCfmCL.Text = hdBirthday.Value;
                    lblAddressCfmCL.Text = hdAddress.Value;
                    lblNRCCfmCL.Text = hdNRC.Value;
                    lblEmailCfmCL.Text = hdEmail.Value;
                    lblPhoneCfmCL.Text = hdPhone.Value;
                    lblCreditAccountCfmCL.Text = ht["DDACCT"].ToString();
                    lblDebitAccountCfmCL.Text = ddlAccountNoClose.SelectedValue;
                    hdAmount.Value = ht["BALANCE"].ToString();
                    lblTotalCfmCL.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ht["BALANCE"].ToString(), ht["CCYCD"].ToString()) + " " + ht["CCYCD"].ToString();
                    lblprewithdrawcharge.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ht["PREWITHDRAWCHARGE"].ToString(), ht["CCYCD"].ToString()) + " " + ht["CCYCD"].ToString(); ;
                    lblinterestrevert.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ht["INTERESTREVERT"].ToString(), ht["CCYCD"].ToString()) + " " + ht["CCYCD"].ToString();
                    lblinterestpayable.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ht["INTERESTPAYABLE"].ToString(), ht["CCYCD"].ToString()) + " " + ht["CCYCD"].ToString(); ;
                    lbldescCfmCL.Text = txtDescClose.Text;
                    lblrsCurency.Text = lblAvailableBalCCYID.Text;
                    //hdfdreceipt.Value = ht["ERRORCODE"].ToString();
                    ///
                    string User = Session["userID"].ToString();
                    Account acct = new Account();
                    Hashtable ht1 = new Hashtable();
                    ht1 = acct.GetInfoAccount(User, ht["DDACCT"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    hdFullNameCredit.Value = ht1["FULLNAME"].ToString();
                }
                catch
                {
                    lblError.Text = IPCERRORDESC.ToString();
                    btnConfirmClose.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        try
        {
            if (SmartPortal.Common.Utilities.Utility.isDouble(hdBalanceSender.Value, true) < (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true)))
            {
                lblError.Text = Resources.labels.amountinvalid;
                return;
            }
            if (!cbPolicy.Checked)
            {
                lblError.Text = Resources.labels.pleaseagreewithtermsandvonditionsofbankbeforeopennewaccount;
                return;
            }
            pnComfirm.Visible = true;
            pnOTP.Visible = false;
            pnContent.Visible = false;
            pnResult.Visible = false;

            hdAccountType.Value = ddlActType.SelectedItem.Value;
            hdAccountNO.Value = ddlDebitAccount.SelectedValue;
            HdCatCode.Value = ddlCatcode.SelectedValue;
            hdAmount.Value = txtAmount.Text;
            hdDesc.Value = txtDesc.Text;
            lblAvailableBalCfm.Text = lblAvailableBal.Text;
            hdcategoryName.Value = ddlCatcode.SelectedItem.Text;
            //set confirm
            lblAccountNameCfm.Text = hdFullName.Value;
            lblBirthdayCfm.Text = hdBirthday.Value;
            lblAddressCfm.Text = hdAddress.Value;
            lblNRCCfm.Text = hdNRC.Value;
            lblEmailCfm.Text = hdEmail.Value;
            lblPhoneCfm.Text = hdPhone.Value;
            lblDebitAccountCfm.Text = ddlDebitAccount.SelectedItem.Text;
            lblActTypeCfm.Text = ddlActType.SelectedItem.Text;
            lblCatcodeCfm.Text = ddlCatcode.SelectedItem.Text;
            lblAmountfm.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hdAmount.Value), lblAvailableBalCCYID.Text);
            lbldescCfm.Text = txtDesc.Text;


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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Session["print"] == null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
        }
    }

    protected void ddlAccountNoClose_SelectedIndexChanged(object sender, EventArgs e)
    {
        string account = ddlAccountNoClose.SelectedItem.Value.ToString();
        string ErrorCode = string.Empty;
        string ErrorDesc = string.Empty;
        string User = Session["userID"].ToString();
        Account acct = new Account();
        Hashtable ht = new Hashtable();
        ht = acct.GetInfoAccountFD(User, account, ref ErrorCode, ref ErrorDesc);
        string CurrencyCode = string.Empty;
        if (ht["CURRENCYID"] != null)
        {
            hdcloseccyid.Value = ht["CURRENCYID"].ToString();
        }
        if (ht[SmartPortal.Constant.IPC.BALANCE] != null)
        {
            hdclosebalance.Value = ht[SmartPortal.Constant.IPC.BALANCE].ToString();
        }
    }
}