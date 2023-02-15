using System;
using System.Linq;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI;
using Antlr.Runtime.JavaExtensions;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using SmartPortal.IB;
using System.Text;
using System.Text.RegularExpressions;
using SmartPortal.Common;
using System.Diagnostics;
using System.Web;

public partial class widgets_IBBillPayment2_widget_ascx : WidgetBase
{

    private Stopwatch sw = null;
    string IPCERRORCODE;
    string IPCERRORDESC;
    static Account acct = new Account();
    static string billerType = "BILLER";
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : Utility.createTableDocumnet(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
        if (!IsPostBack)
        {
            LoadAccount();
            LoadDllBill();
            if ((Session["accType"].ToString() != "IND"))
            {
                LblDocument.Visible = true;
                FUDocument.Visible = true;
                LblDocumentExpalainion.Visible = true;
            }
            pnBill.Visible = true;
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnResult.Visible = false;
            hdPhoneNo.Value = new SmartPortal.SEMS.Contract().GETSMSPHONE(Session["userID"].ToString()).Rows[0]["PHONE"].ToString();
        }
    }
    private void LoadAccount()
    {
        try
        {
            DataSet ds = new DataSet();
            Account acct = new Account();
            ds = acct.GetListOfAccounts(Session["userID"].ToString(), "IBBPMBANKACT", "IBBPMWALLET", "DD,CD,WL", "LAK", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                ds.Tables[0].DefaultView.RowFilter = "TYPEID IN ('DD', 'CD','WLM') AND STATUSCD in ('A','Y')";
                ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                ddlSenderAccount.DataTextField = ds.Tables[0].Columns["ACCOUNTNO"].ColumnName.ToString();
                ddlSenderAccount.DataValueField = ds.Tables[0].Columns["UNIQUEID"].ColumnName.ToString();
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
    private void LoadDllBill()
    {
        try
        {
            DataTable dt = new SmartPortal.IB.GiftCard().GETBILLLIST(Session["UserID"].ToString(), string.Empty, "en-US", string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
            ddlBillType.DataSource = dt;
            ddlBillType.DataTextField = "BillerName";
            ddlBillType.DataValueField = "BillerID";
            ddlBillType.DataBind();

            ddlBillType_SelectedIndexChanged(null, EventArgs.Empty);
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
            if (IPCERRORCODE.Equals("0"))
            {
                lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hashtable["AVAILABLEBALANCE"].ToString()), hashtable["CCYID"].ToString());
                lblAvailableBalCCYID.Text = hashtable["CCYID"].ToString();
                lblCurrency.Text = lbCCYID.Text = lblFeeCCYID.Text = lblDiscountCCYID.Text = lblEndDiscountCCYID.Text = lblBalanceCCYID.Text = lblEndAmountCCYID.Text =
                    lblEndFeeCCYID.Text = lblSenderCCYID.Text = lblAvailableBalCCYID.Text;
                hdActTypeSender.Value = hashtable["TYPEID"].ToString();
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
    private Object m_lock = new Object();
    protected void btnAction_Click(object sender, EventArgs e)
    {
        try
        {
            lock (m_lock)
            {
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(lblBalanceSender.Text, true))
                {
                    lblError.Text = Resources.labels.amountinvalid;
                    return;
                }

                string OTPcode = txtOTP.Text;
                txtOTP.Text = "";
                DataTable tbldocument = (DataTable)ViewState["TBLDOCUMENT"];
                Hashtable result = new Hashtable();
                result = new GiftCard().BPMBANKACT(Utility.KillSqlInjection(hdTranCode.Value.Trim()),
                   Session["userID"].ToString(), Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()),
                    Utility.FormatMoneyInput(hdFeeSender.Value, lblCurrency.Text.Trim()),
                     Utility.FormatMoneyInput(hdFeeReceiver.Value, lblCurrency.Text.Trim()),
                   Utility.KillSqlInjection(lblCurrency.Text.Trim()), Utility.KillSqlInjection(hdIDREF.Value.Trim()),
                   Utility.KillSqlInjection(ddlBillType.SelectedValue.Trim()), Utility.KillSqlInjection(ddlSenderAccount.SelectedItem.Value.Trim()),
                   Session["BranchID"].ToString(), Utility.KillSqlInjection(lblSenderName.Text.Trim()), string.Empty,
                   ddlLoaiXacThuc.SelectedValue, Utility.KillSqlInjection(OTPcode),
                   string.IsNullOrEmpty(Utility.KillSqlInjection(txtDesc.Text.Trim())) ? "IB bill payment" : Utility.KillSqlInjection(txtDesc.Text.Trim()), Utility.KillSqlInjection(hdRefVal01.Value.Trim()), Utility.KillSqlInjection(hdRefVal02.Value.Trim()), Utility.KillSqlInjection(hdRefVal03.Value.Trim()), Utility.KillSqlInjection(hdRefVal04.Value.Trim()), Utility.KillSqlInjection(hdRefVal05.Value.Trim()), Utility.KillSqlInjection(lblSenderName.Text.Trim()), Utility.KillSqlInjection(ddlSenderAccount.SelectedItem.Text.Trim()), Utility.KillSqlInjection(lblBill01Value.Text.Trim()), Utility.KillSqlInjection(lblBill02Value.Text.Trim()), Utility.KillSqlInjection(lblBill03Value.Text.Trim()), Utility.KillSqlInjection(lblBill04Value.Text.Trim()), Utility.KillSqlInjection(lblBill05Value.Text.Trim()), hdPhoneNo.Value, hdBILLTYPE.Value, tbldocument,Session["accType"].ToString());
                if (!result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
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
                pnBill.Visible = false;
                pnResult.Visible = true;

                string senderBranch = string.Empty;
                if (!Session["TypeID"].ToString().Equals("CTK"))
                {
                    tranno.Visible = false;
                }
                lblEndTransactionNo.Text = result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : string.Empty;
                lblEndDateTime.Text = result["TRANTIME"] != null ? result["TRANTIME"].ToString() : DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                lblEndSenderAccount.Text = ddlSenderAccount.SelectedItem.Text;
                Hashtable hashtable = acct.GetInfoAccount(Session["userID"].ToString(), Utility.KillSqlInjection(ddlSenderAccount.SelectedItem.Value.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
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

                lblEndBillerCode.Text = lblBillerCode.Text;

                lblEndRef1Verf.Visible = !string.IsNullOrEmpty(lblRef1Verf.Text);
                lblEndRef1Verf.Text = lblRef1Verf.Text;
                Endrefval01.Visible = !string.IsNullOrEmpty(refval01.Text);
                Endrefval01.Text = refval01.Text;

                lblEndRef2Verf.Visible = !string.IsNullOrEmpty(lblRef2Verf.Text);
                lblEndRef2Verf.Text = lblRef2Verf.Text;
                Endrefval02.Visible = !string.IsNullOrEmpty(refval02.Text);
                Endrefval02.Text = refval02.Text;

                lblEndRef3Verf.Visible = !string.IsNullOrEmpty(lblRef3Verf.Text);
                lblEndRef3Verf.Text = lblRef3Verf.Text;
                Endrefval03.Visible = !string.IsNullOrEmpty(refval03.Text);
                Endrefval03.Text = refval03.Text;

                lblEndRef4Verf.Visible = !string.IsNullOrEmpty(lblRef4Verf.Text);
                lblEndRef4Verf.Text = lblRef4Verf.Text;
                Endrefval04.Visible = !string.IsNullOrEmpty(refval04.Text);
                Endrefval04.Text = refval04.Text;

                lblEndRef5Verf.Visible = !string.IsNullOrEmpty(lblRef5Verf.Text);
                lblEndRef5Verf.Text = lblRef5Verf.Text;
                Endrefval05.Visible = !string.IsNullOrEmpty(refval05.Text);
                Endrefval05.Text = refval05.Text;

                lblEndAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), lblBalanceCCYID.Text);
                lblEndPhi.Text = lblPhi.Text;
                lblEndPhiAmount.Text = lblPhiAmount.Text;
                lblEndDiscountAmount.Text = lblDiscountAmount.Text;
                lblEndDesc.Text = lblDesc.Text == "" ? "IB bill payment" : lblDesc.Text;

                if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    lblError.Text = Resources.labels.transactionsuccessful;
                    Hashtable hasPrint = new Hashtable();
                    hasPrint.Add("tranDate", lblEndDateTime.Text);
                    hasPrint.Add("tranID", lblEndTransactionNo.Text);
                    hasPrint.Add("senderName", lblEndSenderName.Text);
                    hasPrint.Add("senderAccount", ddlSenderAccount.SelectedItem.Text);
                    hasPrint.Add("senderBranch", senderBranch);
                    hasPrint.Add("amount", lblEndAmount.Text);
                    double amountIsDouble = SmartPortal.Common.Utilities.Utility.isDouble(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblEndAmount.Text, lblBalanceCCYID.Text), true);
                    hasPrint.Add("amountchu", Utility.NumtoWords(amountIsDouble));
                    hasPrint.Add("ccyid", lblBalanceCCYID.Text);
                    hasPrint.Add("feeType", lblPhi.Text);
                    hasPrint.Add("feeAmount", lblPhiAmount.Text);
                    hasPrint.Add("discount", lblDiscountAmount.Text);
                    hasPrint.Add("desc", lblEndDesc.Text);
                    hasPrint.Add("status", Resources.labels.thanhcong);
                    hasPrint.Add("serviceName", lblBillerCode.Text); 
                 
                    hasPrint.Add("lblRef1Verf", lblRef1Verf.Text);
                    hasPrint.Add("refval01", refval01.Text);
                    hasPrint.Add("lblRef2Verf", lblRef2Verf.Text);
                    hasPrint.Add("refval02", refval02.Text);
                    hasPrint.Add("lblRef3Verf", lblRef3Verf.Text);
                    hasPrint.Add("refval03", refval03.Text);
                    hasPrint.Add("lblRef4Verf", lblRef4Verf.Text);
                    hasPrint.Add("refval04", refval04.Text);
                    hasPrint.Add("lblRef5Verf", lblRef5Verf.Text);
                    hasPrint.Add("refval05", refval05.Text);
                    hasPrint.Add("lblBill01Name", lblBill01Name.Text);
                    hasPrint.Add("lblBill01Value", lblBill01Value.Text);
                    hasPrint.Add("lblBill02Name", lblBill02Name.Text);
                    hasPrint.Add("lblBill02Value", lblBill02Value.Text);
                    hasPrint.Add("lblBill03Name", lblBill03Name.Text);
                    hasPrint.Add("lblBill03Value", lblBill03Value.Text);
                    hasPrint.Add("lblBill04Name", lblBill04Name.Text);
                    hasPrint.Add("lblBill04Value", lblBill04Value.Text);
                    hasPrint.Add("lblBill05Name", lblBill05Name.Text);
                    hasPrint.Add("lblBill05Value", lblBill05Value.Text);

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
    protected void ddlLoaiXacThuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAction.Enabled = false;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Session["print"] == null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (Session["print"] == null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            if (SmartPortal.Common.Utilities.Utility.isDouble(lblAvailableBal.Text, true) < (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) + SmartPortal.Common.Utilities.Utility.isDouble(hdFeeSender.Value, true)))
            {
                lblError.Text = Resources.labels.amountinvalid;
                return;
            }
            if (ViewState["isSendFirst"] == null || (int)ViewState["isSendFirst"] == 0)
            {
                LoadLoaiXacThuc();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
            }
            pnBill.Visible = false;
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
    protected void btnBackTRF_OnClick(object sender, EventArgs e)
    {
        try
        {
            pnBill.Visible = true;
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
    protected void btnBackConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            pnBill.Visible = false;
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
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateRef()) return;
            if (!Validate()) return;
            string balanceSender = string.Empty;
            string senderName = string.Empty;
            string ccyid = string.Empty;
            hdTranCode.Value = hdActTypeSender.Value.Equals("WLM") ? "IBBPMWALLET" : "IBBPMBANKACT";
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
                lblError.Text = Resources.labels.invalidpaymentinformation;
                return;
            }

            lblSenderName.Text = senderName;
            lblSenderAccount.Text = ddlSenderAccount.SelectedItem.Text;
            lblBalanceSender.Text = balanceSender;

            lblBillerCode.Text = ddlBillType.SelectedItem.Text;
            if (lblddlRefVal01.Visible || lbltxtRefVal01.Visible) lblRef1Verf.Visible = true;
            if (lblddlRefVal01.Visible)
            {
                lblRef1Verf.Text = lblddlRefVal01.Text;
            }
            if (lbltxtRefVal01.Visible)
            {
                lblRef1Verf.Text = lbltxtRefVal01.Text.Replace("*", "");
            }

            if (ddlRefVal01.Visible || txtRefVal01.Visible) refval01.Visible = true;
            if (ddlRefVal01.Visible)
            {
                hdRefVal01.Value = ddlRefVal01.SelectedItem.Value;
                refval01.Text = ddlRefVal01.SelectedItem.Text;
            }
            if (txtRefVal01.Visible)
            {
                hdRefVal01.Value = refval01.Text = txtRefVal01.Text;
            }

            if (lblddlRefVal02.Visible || lbltxtRefVal02.Visible)
            {
                lblRef2Verf.Visible = true;
            }
            if (lblddlRefVal02.Visible)
            {
                lblRef2Verf.Text = lblddlRefVal02.Text;
            }
            if (lbltxtRefVal02.Visible)
            {
                lblRef2Verf.Text = lbltxtRefVal02.Text.Replace("*", "");
            }

            if (ddlRefVal02.Visible || txtRefVal02.Visible)
            {
                refval02.Visible = true;
            }
            if (ddlRefVal02.Visible)
            {
                hdRefVal02.Value = ddlRefVal02.SelectedItem.Value;
                refval02.Text = ddlRefVal02.SelectedItem.Text;
            }
            if (txtRefVal02.Visible)
            {
                hdRefVal02.Value = refval02.Text = txtRefVal02.Text;
            }


            if (lblddlRefVal03.Visible || lbltxtRefVal03.Visible) lblRef3Verf.Visible = true;
            if (lblddlRefVal03.Visible)
            {
                lblRef3Verf.Text = lblddlRefVal03.Text;
            }
            if (lbltxtRefVal03.Visible)
            {
                lblRef3Verf.Text = lbltxtRefVal03.Text.Replace("*", "");
            }

            if (ddlRefVal03.Visible || txtRefVal03.Visible) refval03.Visible = true;
            if (ddlRefVal03.Visible)
            {
                hdRefVal03.Value = ddlRefVal03.SelectedItem.Value;
                refval03.Text = ddlRefVal03.SelectedItem.Text;
            }
            if (txtRefVal03.Visible)
            {
                hdRefVal03.Value = refval03.Text = txtRefVal03.Text;
            }

            if (lblddlRefVal04.Visible || lbltxtRefVal04.Visible) lblRef4Verf.Visible = true;
            if (lblddlRefVal04.Visible)
            {
                lblRef4Verf.Text = lblddlRefVal04.Text;
            }
            if (lbltxtRefVal04.Visible)
            {
                lblRef4Verf.Text = lbltxtRefVal04.Text.Replace("*", "");
            }

            if (ddlRefVal04.Visible || txtRefVal04.Visible) refval04.Visible = true;
            if (ddlRefVal04.Visible)
            {
                hdRefVal04.Value = ddlRefVal04.SelectedItem.Value;
                refval04.Text = ddlRefVal04.SelectedItem.Text;
            }
            if (txtRefVal04.Visible)
            {
                hdRefVal04.Value = refval04.Text = txtRefVal04.Text;
            }
            if (lblddlRefVal05.Visible || lbltxtRefVal05.Visible) lblRef5Verf.Visible = true;
            if (lblddlRefVal05.Visible)
            {
                lblRef5Verf.Text = lblddlRefVal05.Text;
            }
            if (lbltxtRefVal05.Visible)
            {
                lblRef5Verf.Text = lbltxtRefVal05.Text.Replace("*", "");
            }

            if (ddlRefVal05.Visible || txtRefVal05.Visible) refval05.Visible = true;
            if (ddlRefVal05.Visible)
            {
                hdRefVal05.Value = ddlRefVal05.SelectedItem.Value;
                refval05.Text = ddlRefVal05.SelectedItem.Text;
            }
            if (txtRefVal05.Visible)
            {
                hdRefVal05.Value = refval05.Text = txtRefVal05.Text;
            }

            string serviceId = string.Empty;

            DataSet dataSetBiller = new SmartPortal.SEMS.Biller().GetBillDetailsById(Utility.KillSqlInjection(ddlBillType.SelectedValue.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                lblError.Text = Resources.labels.invalidpaymentinformation;
                return;
            }
            DataTable bt = dataSetBiller.Tables[0];
            if (bt.Rows.Count != 0)
            {
                serviceId = bt.Rows[0]["BILLERCODE"].ToString();
            }

            hashtable = acct.BILLINQUIRY(Session["userID"].ToString(), Utility.KillSqlInjection(hdTranCode.Value.Trim()), serviceId, Utility.KillSqlInjection(ddlBillType.SelectedValue.Trim()), serviceId, hdRefVal01.Value, hdRefVal02.Value, hdRefVal03.Value, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                lblError.Text = Resources.labels.invalidpaymentinformation;
                return;
            }

            if (hashtable.ContainsKey("BILLAMOUNT") && hashtable.ContainsKey("CCYID"))
            {
                txtAmount.Text = lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(hashtable["BILLAMOUNT"].ToString(), hashtable["CCYID"].ToString());
                if (Utility.isDouble(hashtable["BILLAMOUNT"].ToString(), true) <= 0)
                {
                    lblWarning.Visible = true;
                    lblWarning.Text = Resources.labels.warningpaymentbill;
                }
            }

            if (hashtable.ContainsKey("DEBITFEE"))
            {
                hdFeeSender.Value = SmartPortal.Common.Utilities.Utility.FormatMoney(hashtable["DEBITFEE"].ToString(), ccyid);
                if (Utility.isDouble(hashtable["DEBITFEE"].ToString(), true) < 0)
                {
                    lblPhiAmount.Text = "0";
                    lblDiscountAmount.Text = hdFeeSender.Value;
                }
                else
                {
                    lblPhiAmount.Text = hdFeeSender.Value;
                    lblDiscountAmount.Text = "0";
                }
            }
            if (hashtable.ContainsKey("CREFEE"))
            {
                hdFeeReceiver.Value = SmartPortal.Common.Utilities.Utility.FormatMoney(hashtable["CREFEE"].ToString(), ccyid);
            }
            if (hashtable.ContainsKey("EDITABLE"))
            {
                if (hashtable["EDITABLE"].ToString() == "N")
                {
                    txtAmount.Visible = false;
                }
                divAmount.Visible = true;
            }
            if (hashtable.ContainsKey("IDREF"))
            {
                hdIDREF.Value = hashtable["IDREF"].ToString();
            }
            if (hashtable.ContainsKey("REFINFONAME01") && hashtable.ContainsKey("REFINFOVA01") && !hashtable["REFINFOVA01"].ToString().Equals("-") && !string.IsNullOrEmpty(hashtable["REFINFOVA01"].ToString()))
            {
                lblBill01Name.Visible =
                    lblEndBill01Name.Visible = lblBill01Value.Visible = lblEndBill01Value.Visible = true;
                lblBill01Name.Text = lblEndBill01Name.Text = hashtable["REFINFONAME01"].ToString();
                lblBill01Value.Text = lblEndBill01Value.Text = hashtable["REFINFOVA01"].ToString();
            }
            if (hashtable.ContainsKey("REFINFONAME02") && hashtable.ContainsKey("REFINFOVA02") && !hashtable["REFINFOVA02"].ToString().Equals("-") && !string.IsNullOrEmpty(hashtable["REFINFOVA02"].ToString()))
            {
                lblBill02Name.Visible =
                    lblEndBill02Name.Visible = lblBill02Value.Visible = lblEndBill02Value.Visible = true;
                lblBill02Name.Text = lblEndBill02Name.Text = hashtable["REFINFONAME02"].ToString();
                lblBill02Value.Text = lblEndBill02Value.Text = hashtable["REFINFOVA02"].ToString();
            }
            if (hashtable.ContainsKey("REFINFONAME03") && hashtable.ContainsKey("REFINFOVA03") && !hashtable["REFINFOVA03"].ToString().Equals("-") && !string.IsNullOrEmpty(hashtable["REFINFOVA03"].ToString()))
            {
                lblBill03Name.Visible =
                    lblEndBill03Name.Visible = lblBill03Value.Visible = lblEndBill03Value.Visible = true;
                lblBill03Name.Text = lblEndBill03Name.Text = hashtable["REFINFONAME03"].ToString();
                lblBill03Value.Text = lblEndBill03Value.Text = hashtable["REFINFOVA03"].ToString();
            }
            if (hashtable.ContainsKey("REFINFONAME04") && hashtable.ContainsKey("REFINFOVA04") && !hashtable["REFINFOVA04"].ToString().Equals("-") && !string.IsNullOrEmpty(hashtable["REFINFOVA04"].ToString()))
            {
                lblBill04Name.Visible =
                    lblEndBill04Name.Visible = lblBill04Value.Visible = lblEndBill04Value.Visible = true;
                lblBill04Name.Text = lblEndBill04Name.Text = hashtable["REFINFONAME04"].ToString();
                lblBill04Value.Text = lblEndBill04Value.Text = hashtable["REFINFOVA04"].ToString();
            }
            if (hashtable.ContainsKey("REFINFONAME05") && hashtable.ContainsKey("REFINFOVA05") && !hashtable["REFINFOVA05"].ToString().Equals("-") && !string.IsNullOrEmpty(hashtable["REFINFOVA05"].ToString()))
            {
                lblBill05Name.Visible =
                    lblEndBill05Name.Visible = lblBill05Value.Visible = lblEndBill05Value.Visible = true;
                lblBill05Name.Text = lblEndBill05Name.Text = hashtable["REFINFONAME05"].ToString();
                lblBill05Value.Text = lblEndBill05Value.Text = hashtable["REFINFOVA05"].ToString();
            }
            if (hashtable.ContainsKey("BILLTYPE"))
            {
                hdBILLTYPE.Value = hashtable["BILLTYPE"].ToString();
            }
            if (Session["accType"].ToString() != "IND")
            {
                DataTable dt = Utility.UploadFile(FUDocument, lblError);
                ViewState["TBLDOCUMENT"] = dt;
                
            }

            lblPhi.Text = Resources.labels.nguoigui;
            lblDesc.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text);
            pnBill.Visible = false;
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
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=11200"));
    }
    protected void ddlBillType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string langID;
            langID = Session["langID"] == null ? new PortalSettings().portalSetting.DefaultLang : Session["langID"].ToString();

            DataSet ds = new SmartPortal.IB.GiftCard().BILLERGETREF(langID, ddlBillType.SelectedValue, ref billerType, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtRef = ds.Tables[0];
                DataTable dtRefCDS = ds.Tables[1];
                DataRow[] rows;
                rows = dtRef.Select("PARAMCODE ='REFVAL01' and PARAMTYPE= 'CBB'");
                if (rows.Length > 0)
                {
                    lblddlRefVal01.Visible = true;
                    ddlRefVal01.Visible = true;
                    lblddlRefVal01.Text = rows[0]["PARAMNAME"].ToString();
                }
                else
                {
                    lblddlRefVal01.Visible = false;
                    ddlRefVal01.Visible = false;
                }

                if (ddlRefVal01.Visible)
                {
                    ddlRefVal01.DataSource = GenDataTable(dtRefCDS, "RefVal01", "RefText01");
                    ddlRefVal01.DataTextField = "RefText01";
                    ddlRefVal01.DataValueField = "RefVal01";
                    ddlRefVal01.DataBind();
                }

                rows = dtRef.Select("PARAMCODE ='REFVAL02' and PARAMTYPE= 'CBB'");
                if (rows.Length > 0)
                {
                    lblddlRefVal02.Visible = true;
                    ddlRefVal02.Visible = true;
                    lblddlRefVal02.Text = rows[0]["PARAMNAME"].ToString();
                }
                else
                {
                    lblddlRefVal02.Visible = false;
                    ddlRefVal02.Visible = false;
                }
                if (ddlRefVal02.Visible)
                {
                    ddlRefVal02.DataSource = GenDataTable(dtRefCDS, "RefVal02", "RefText02");
                    ddlRefVal02.DataTextField = "RefText02";
                    ddlRefVal02.DataValueField = "RefVal02";
                    ddlRefVal02.DataBind();
                }
                rows = dtRef.Select("PARAMCODE ='REFVAL03' and PARAMTYPE= 'CBB'");
                if (rows.Length > 0)
                {
                    lblddlRefVal03.Visible = true;
                    ddlRefVal03.Visible = true;
                    lblddlRefVal03.Text = rows[0]["PARAMNAME"].ToString();
                }
                else
                {
                    lblddlRefVal03.Visible = false;
                    ddlRefVal03.Visible = false;
                }
                if (ddlRefVal03.Visible)
                {
                    ddlRefVal03.DataSource = GenDataTable(dtRefCDS, "RefVal03", "RefText03");
                    ddlRefVal03.DataTextField = "RefText03";
                    ddlRefVal03.DataValueField = "RefVal03";
                    ddlRefVal03.DataBind();
                }
                rows = dtRef.Select("PARAMCODE ='REFVAL05' and PARAMTYPE= 'CBB'");
                if (rows.Length > 0)
                {
                    lblddlRefVal04.Visible = true;
                    ddlRefVal04.Visible = true;
                    lblddlRefVal04.Text = rows[0]["PARAMNAME"].ToString();
                }
                else
                {
                    lblddlRefVal04.Visible = false;
                    ddlRefVal04.Visible = false;
                }
                if (ddlRefVal04.Visible)
                {
                    ddlRefVal04.DataSource = GenDataTable(dtRefCDS, "RefVal04", "RefText04");
                    ddlRefVal04.DataTextField = "RefText04";
                    ddlRefVal04.DataValueField = "RefVal04";
                    ddlRefVal04.DataBind();
                }

                rows = dtRef.Select("PARAMCODE ='REFVAL05' and PARAMTYPE= 'CBB'");
                if (rows.Length > 0)
                {
                    lblddlRefVal05.Visible = true;
                    ddlRefVal05.Visible = true;
                    lblddlRefVal05.Text = rows[0]["PARAMNAME"].ToString();
                }
                else
                {
                    lblddlRefVal05.Visible = false;
                    ddlRefVal05.Visible = false;
                }
                if (ddlRefVal05.Visible)
                {
                    ddlRefVal05.DataSource = GenDataTable(dtRefCDS, "RefVal05", "RefText05");
                    ddlRefVal05.DataTextField = "RefText05";
                    ddlRefVal05.DataValueField = "RefVal05";
                    ddlRefVal05.DataBind();
                }

                rows = dtRef.Select("PARAMCODE ='REFVAL01' and PARAMTYPE= 'TXT'");
                if (rows.Length > 0)
                {
                    lbltxtRefVal01.Visible = true;
                    txtRefVal01.Visible = true;
                    lbltxtRefVal01.Text = rows[0]["PARAMNAME"].ToString() + " *";
                }
                else
                {
                    lbltxtRefVal01.Visible = false;
                    txtRefVal01.Visible = false;
                }
                rows = dtRef.Select("PARAMCODE ='REFVAL02' and PARAMTYPE= 'TXT'");
                if (rows.Length > 0)
                {
                    lbltxtRefVal02.Visible = true;
                    txtRefVal02.Visible = true;
                    lbltxtRefVal02.Text = rows[0]["PARAMNAME"].ToString() + " *";
                }
                else
                {
                    lbltxtRefVal02.Visible = false;
                    txtRefVal02.Visible = false;
                }
                rows = dtRef.Select("PARAMCODE ='REFVAL03' and PARAMTYPE= 'TXT'");
                if (rows.Length > 0)
                {
                    lbltxtRefVal03.Visible = true;
                    txtRefVal03.Visible = true;
                    lbltxtRefVal03.Text = rows[0]["PARAMNAME"].ToString() + "*";
                }
                else
                {
                    lbltxtRefVal03.Visible = false;
                    txtRefVal03.Visible = false;
                }
                rows = dtRef.Select("PARAMCODE ='REFVAL04' and PARAMTYPE= 'TXT'");
                if (rows.Length > 0)
                {
                    lbltxtRefVal04.Visible = true;
                    txtRefVal04.Visible = true;
                    lbltxtRefVal04.Text = rows[0]["PARAMNAME"].ToString() + "";
                }
                else
                {
                    lbltxtRefVal04.Visible = false;
                    txtRefVal04.Visible = false;
                }
                rows = dtRef.Select("PARAMCODE ='REFVAL05' and PARAMTYPE= 'TXT'");
                if (rows.Length > 0)
                {
                    lbltxtRefVal05.Visible = true;
                    txtRefVal05.Visible = true;
                    lbltxtRefVal05.Text = rows[0]["PARAMNAME"].ToString() + "";
                }
                else
                {
                    lbltxtRefVal05.Visible = false;
                    txtRefVal05.Visible = false;
                }
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
    protected bool ValidateRef()
    {
        if (txtRefVal01.Visible)
        {
            if (string.IsNullOrEmpty(txtRefVal01.Text))
            {
                lblError.Text = "Please provide " + lbltxtRefVal01.Text;
                return false;
            }
        }
        if (txtRefVal02.Visible)
        {
            if (string.IsNullOrEmpty(txtRefVal02.Text))
            {
                lblError.Text = "Please provide " + lbltxtRefVal02.Text;
                return false;
            }
        }
        if (txtRefVal03.Visible)
        {
            if (string.IsNullOrEmpty(txtRefVal03.Text))
            {
                lblError.Text = "Please provide " + lbltxtRefVal03.Text;
                return false;
            }
        }
        return true;
    }
    protected DataTable GenDataTable(DataTable dt, string dataValueField, string dataTextField)
    {
        DataTable result = new DataTable();
        try
        {
            result.Columns.AddRange(new DataColumn[] { new DataColumn(dataValueField), new DataColumn(dataTextField) });
            DataRow[] rows = dt.Select(dataValueField + " <>'' and " + dataTextField + " <>''");
            foreach (var itemDataRow in rows)
            {
                DataRow[] rows1 = result.Select(dataValueField + " ='" + itemDataRow[dataValueField].ToString() + "'");
                if (rows1.Length == 0)
                {
                    DataRow row = result.NewRow();
                    row[dataValueField] = itemDataRow[dataValueField].ToString();
                    row[dataTextField] = itemDataRow[dataTextField].ToString();
                    result.Rows.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        return result;
    }
    private bool Validate()
    {
        if (string.IsNullOrEmpty(txtDesc.Text))
        {
            lblError.Text = Resources.labels.bancannhapnoidung;
            return false;
        }
        return true;
    }
}
