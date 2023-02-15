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
using SmartPortal.IB;
using System.Diagnostics;

public partial class Widgets_IBTransferNonAccount_Widget : WidgetBase
{
    private Stopwatch sw = null;
    static Account acct = new Account();
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : Utility.createTableDocumnet(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
            if (!IsPostBack)
            {
                LoadAccount();
                pnTRF.Visible = true;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResult.Visible = false;
                if ((Session["accType"].ToString() != "IND"))
                {
                    LblDocument.Visible = true;
                    FUDocument.Visible = true;
                    LblDocumentExpalainion.Visible = true;
                }
                hdPhoneNo.Value = new SmartPortal.SEMS.Contract().GETSMSPHONE(Session["userID"].ToString()).Rows[0]["PHONE"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadAccount()
    {
        try
        {
            DataSet ds = new DataSet();
            Account acct = new Account();
            ds = acct.GetListOfAccounts(Session["userID"].ToString(), "IBTRANSNONWALLET", "IBWLTRANSNONWALLET", "DD,CD,WL", "LAK", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                ds.Tables[0].DefaultView.RowFilter = "TYPEID IN ('DD', 'CD','WLM') AND STATUSCD in ('A','Y')";
                ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                ddlSenderAccount.DataTextField = "ACCOUNTNO";
                ddlSenderAccount.DataValueField = "UNIQUEID";
                ddlSenderAccount.DataBind();
                ddlSenderAccount_SelectedIndexChanged(null, EventArgs.Empty);
            }
            else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string account = ddlSenderAccount.SelectedItem.Value.ToString();
            Hashtable hashtable = acct.GetInfoAccount(Session["userID"].ToString(), account, ref IPCERRORCODE, ref IPCERRORDESC);
            switch (hashtable["TYPEID"].ToString().Trim())
            {
                case "WLM":
                    hdTranCode.Value = "IBWLTRANSNONWALLET";
                    break;
                default:
                    hdTranCode.Value = "IBTRANSNONWALLET";
                    break;
            }
            if (IPCERRORCODE.Equals("0"))
            {
                lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hashtable["AVAILABLEBALANCE"].ToString()), hashtable["CCYID"].ToString());
                lblAvailableBalCCYID.Text = hashtable["CCYID"].ToString();
                lblCurrency.Text = lbCCYID.Text = lblFeeCCYID.Text = lblBalanceCCYID.Text = lblEndAmountCCYID.Text =
                    lblEndFeeCCYID.Text = lblSenderCCYID.Text = lblAvailableBalCCYID.Text;
            }
            else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            string balanceSender = string.Empty;
            string senderName = string.Empty;
            string ccyid = string.Empty;
            if(string.IsNullOrEmpty(txtDesc.Text.Trim()))
            {
              lblError.Text= Resources.labels.bancannhapnoidung;
                return;
            }
            Hashtable hashtable = new Hashtable();
            hashtable = acct.GetInfoAccount(Session["userID"].ToString(), Utility.KillSqlInjection(ddlSenderAccount.SelectedItem.Value.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                balanceSender = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hashtable["AVAILABLEBALANCE"].ToString()), hashtable["CCYID"].ToString());
                senderName = hashtable["FULLNAME"].ToString();
                ccyid = hashtable["CCYID"].ToString();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
            
            hashtable = acct.CheckAmountPayment(Session["userID"].ToString(),  Utility.KillSqlInjection(hdTranCode.Value.Trim()), Utility.KillSqlInjection(ddlSenderAccount.SelectedItem.Value.ToString()), string.Empty, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), ccyid), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                lblError.Text = IPCERRORDESC;
                return;
            }

            lblSenderName.Text = senderName;
            lblSenderAccount.Text = ddlSenderAccount.SelectedItem.ToString();
            lblBalanceSender.Text = balanceSender;
            lblCreditName.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCreditName.Text);
            lblCreditPhoneNo.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCreditPhoneNo.Text);
            lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), ccyid);
            string senderfee = "0";
            string receiverfee = "0";
            string payer = "0";
            DataTable dtFee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), Utility.KillSqlInjection(hdTranCode.Value.Trim()), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), ccyid), ddlSenderAccount.SelectedValue, string.Empty, ccyid, "");

            if (dtFee.Rows.Count != 0)
            {
                senderfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeSenderAmt"].ToString(), ccyid);
                receiverfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeReceiverAmt"].ToString(), ccyid);
            }
            lblPhiAmount.Text = payer.Equals("0") ? senderfee : receiverfee;
            lblPhi.Text = payer.Equals("0") ? Resources.labels.nguoigui : Resources.labels.nguoinhan;
            lblDesc.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text);

            // check amout
            if (SmartPortal.Common.Utilities.Utility.isDouble(lblBalanceSender.Text, true) < (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) + SmartPortal.Common.Utilities.Utility.isDouble(lblPhiAmount.Text, true)))
            {
                lblError.Text = Resources.labels.amountinvalid;
                return;
            }
            if (Session["accType"].ToString() != "IND")
            {
                DataTable dt = Utility.UploadFile(FUDocument, lblError);
                    ViewState["TBLDOCUMENT"] = dt;
            }

            pnTRF.Visible = false;
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnResult.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnBackTRF_OnClick(object sender, EventArgs e)
    {
        try
        {
            pnTRF.Visible = true;
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnResult.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnApply_Click(object sender, EventArgs e)
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
            pnTRF.Visible = false;
            pnConfirm.Visible = false;
            pnOTP.Visible = true;
            pnResult.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
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

    protected void btnBackConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            pnTRF.Visible = false;
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnResult.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private Object m_lock = new Object();

    protected void btnAction_Click(object sender, EventArgs e)
    {
        try
        {
            lock (m_lock)
            {
                Hashtable result = new Hashtable();
                string otpCode = txtOTP.Text;
                txtOTP.Text = string.Empty;
                DataTable tbldocument = (DataTable)ViewState["TBLDOCUMENT"];
                result = new SmartPortal.IB.Transfer().TranferNonWalletAccount(Utility.KillSqlInjection(hdTranCode.Value.Trim()), Session["userID"].ToString(), Utility.KillSqlInjection(ddlSenderAccount.SelectedItem.Value.Trim()), Utility.KillSqlInjection(txtCreditName.Text.Trim()), Utility.KillSqlInjection(lblCurrency.Text.Trim()), Utility.KillSqlInjection(txtCreditPhoneNo.Text.Trim()), Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()), Utility.KillSqlInjection(txtDesc.Text.Trim()), ddlLoaiXacThuc.SelectedValue, otpCode.Trim(), hdPhoneNo.Value, tbldocument,Session["accType"].ToString());

                if (!result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    txtOTP.Text = "";
                    btnPrint.Visible = false;
                    switch (result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
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
                            lblError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        default:
                            lblError.Text = string.IsNullOrEmpty(result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString()) ? "Transaction error!" : result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                            return;
                    }
                    pnConfirm.Visible = true;
                    btnApply.Enabled = false;
                    btnBackTRF.Enabled = false;
                    btnView.Visible = false;
                }
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnTRF.Visible = false;
                pnResult.Visible = true;

                string senderBranch = string.Empty;
                lblEndTransactionNo.Text = result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : string.Empty;
                lblEndDateTime.Text = result["TRANTIME"] != null ? result["TRANTIME"].ToString() : DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                lblEndSenderAccount.Text = ddlSenderAccount.SelectedItem.ToString();
                Hashtable hashtable = acct.GetInfoAccount(Session["userID"].ToString(), Utility.KillSqlInjection(ddlSenderAccount.SelectedItem.ToString().Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE.Equals("0"))
                {
                    lblEndSenderName.Text = hashtable["FULLNAME"].ToString();
                    lblEndBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hashtable["AVAILABLEBALANCE"].ToString()), hashtable["CCYID"].ToString());
                    lblEndAmountCCYID.Text = lblBalanceCCYID.Text = hashtable["CCYID"].ToString();
                    senderBranch = hashtable["BRANCHNAME"].ToString();
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                lblEndReceiverName.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCreditName.Text);
                lblEndCreditPhoneNo.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCreditPhoneNo.Text);
                lblEndAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), lblBalanceCCYID.Text);
                lblEndPhi.Text = lblPhi.Text;
                lblEndPhiAmount.Text = lblPhiAmount.Text;
                lblEndDesc.Text = lblDesc.Text == "" ? "Transfer Unregistered Account" : lblDesc.Text;

                if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    lblError.Text = Resources.labels.transactionsuccessful;
                    Hashtable hasPrint = new Hashtable();
                    hasPrint.Add("tranDate", lblEndDateTime.Text);
                    hasPrint.Add("tranID", lblEndTransactionNo.Text);
                    hasPrint.Add("senderName", lblEndSenderName.Text);
                    hasPrint.Add("senderAccount", ddlSenderAccount.SelectedItem.ToString());
                    hasPrint.Add("senderBranch", senderBranch);
                    hasPrint.Add("recieverName", lblEndReceiverName.Text);
                    hasPrint.Add("phoneNumber", lblEndCreditPhoneNo.Text);
                    hasPrint.Add("amount", lblEndAmount.Text);
                    double amountIsDouble = SmartPortal.Common.Utilities.Utility.isDouble(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblEndAmount.Text, lblBalanceCCYID.Text), false);
                    hasPrint.Add("amountchu", Utility.NumtoWords(amountIsDouble));
                    hasPrint.Add("ccyid", lblBalanceCCYID.Text);
                    hasPrint.Add("feeType", lblPhi.Text);
                    hasPrint.Add("feeAmount", lblPhiAmount.Text);
                    hasPrint.Add("desc", lblEndDesc.Text);
                    hasPrint.Add("status", Resources.labels.thanhcong);
                    Session["print"] = hasPrint;
                    btnPrint.Visible = true;
                    btnView.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlLoaiXacThuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAction.Enabled = false;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
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

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["print"] == null)
            {
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
            }
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
            if (Session["print"] == null)
            {
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=IBTRANSNONWALLET"));
    }

    protected void txtCreditPhoneNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string errorcode = "";
            string errordesc = "";
            DataSet ds = new SmartPortal.IB.Transactions().ETopup_GetTecoByPhoneNumber(txtCreditPhoneNo.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && !ds.Tables[0].Rows[0]["TelcoID"].ToString().Equals("0"))
                {
                    btnNext.Enabled = true;
                    new SmartPortal.IB.User().CheckPhoneNumberNonWallet(txtCreditPhoneNo.Text, ref errorcode, ref errordesc);
                    if (errorcode != "0")
                    {
                        lblError.Text = errordesc;
                        btnNext.Enabled = false;
                        return;
                    }
                }
                else
                {
                    lblError.Text = Resources.labels.phonenumberwrong;
                    btnNext.Enabled = false;
                    return;
                }
            }
            else
            {
                lblError.Text = Resources.labels.phonenumberwrong;
                btnNext.Enabled = false;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
