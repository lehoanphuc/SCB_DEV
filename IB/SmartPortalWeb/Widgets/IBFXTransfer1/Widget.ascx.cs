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
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

public partial class Widgets_IBFXTransfer_Widget : WidgetBase
{
    private Stopwatch sw = null;
    static Account acct = new Account();
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    public string IPCTRANCODE = "IB_TRANFERFX";
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
            txtCreditAccount.Attributes.Add("onblur", "OnBlurCreditAccount()");
            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
            if (!IsPostBack)
            {
                LoadAccount();
                pnTRF.Visible = true;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResult.Visible = false;
                hdPhoneNo.Value = new SmartPortal.SEMS.Contract().GETSMSPHONE(Session["userID"].ToString()).Rows[0]["PHONE"].ToString();
                if ((Session["accType"].ToString() != "IND"))
                {
                    LblDocument.Visible = true;
                    FUDocument.Visible = true;
                    LblDocumentExpalainion.Visible = true;
                }
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
            ds = acct.GetListOfAccounts(Session["userID"].ToString(), IPCTRANCODE, "IBWLTRANFERFX", "DD,CD,WL", "", ref IPCERRORCODE, ref IPCERRORDESC);
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
            if (IPCERRORCODE.Equals("0"))
            {
                lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hashtable["AVAILABLEBALANCE"].ToString()), hashtable["CCYID"].ToString());
                lblAvailableBalCCYID.Text = hashtable["CCYID"].ToString();
                //hdTypeID.Value = hashtable["TYPEID"].ToString();
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
            if (string.IsNullOrEmpty(txtCreditAccount.Text.Trim()))
            {
                lblError.Text = "Please input account";
                return;
            }
            if (string.IsNullOrEmpty(txtDesc.Text.Trim()))
            {
                lblError.Text = Resources.labels.bancannhapnoidung;
                return;
            }
            if (string.IsNullOrEmpty(txtDesc.Text.Trim()))
            {
                lblError.Text = Resources.labels.bancannhapnoidung;
                return;
            }
            if (SmartPortal.Common.Utilities.Utility.isDouble(lblAvailableBal.Text, true) == 0)
            {
                lblError.Text = Resources.labels.sodukhongdudechuyenkhoan;
                return;
            }
            //if (hdTypeID.Value.Equals("WLM"))
            //{
            //    hdTrancode.Value = "IBWLTRANFERFX";
            //}
            //else if (hdActTypeReceiver.Value.Equals("WLM"))
            //{
            //    hdTrancode.Value = "IBTRANFERFXWL";
            //}
            //else
            //{
                hdTrancode.Value = "IB_TRANFERFX";
            //}
            Hashtable hashtable = new Hashtable();
            hashtable = acct.GetInfoAccount(Session["userID"].ToString(), Utility.KillSqlInjection(ddlSenderAccount.SelectedItem.Value.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                balanceSender = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hashtable["AVAILABLEBALANCE"].ToString()), hashtable["CCYID"].ToString());
                senderName = hashtable[SmartPortal.Constant.IPC.FULLNAME].ToString();
                ccyid = hashtable[SmartPortal.Constant.IPC.CCYID].ToString().Trim();
                hdBranchID.Value = hashtable[SmartPortal.Constant.IPC.BRANCHID].ToString().Trim();
                lblSenderCCYID.Text = lblFeeCCYID.Text = lblEndFeeCCYID.Text = ccyid;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }

            //CHECK RECEIVER ACCOUNT IS EXISTS
            if(txtCreditAccount.Text != "")
            {
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                Hashtable htact = objAcct.GetInfoAccountCredit(Utility.KillSqlInjection(txtCreditAccount.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE.Equals("0") && htact.Count > 0)
                {
                    try
                    {
                        //lblCreditAccount.Text = txtCreditAccount.Text.Trim();
                        lblCreditAccount.Text = Regex.Replace(txtCreditAccount.Text.Trim().ToString(), "(?<=...).(?=.{3})", "x");
                        hdCreditCCYID.Value = htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim();
                        lblCreditName.Text = htact[SmartPortal.Constant.IPC.FULLNAME].ToString();
                        hdCreditBranchID.Value = htact[SmartPortal.Constant.IPC.BRANCHID].ToString().Trim();
                    }
                    catch (Exception exception)
                    {
                        lblError.Text = Resources.labels.creditacccountinvalid;
                        return;
                    }
                }
                else
                {
                    lblError.Text = Resources.labels.creditacccountinvalid;
                    return;
                }

                // CHECK SAME ACCOUNT
                if (!string.IsNullOrEmpty(txtCreditAccount.Text.Trim()))
                {
                    bool sameAcct = objAcct.CheckSameAccount(ddlSenderAccount.SelectedItem.Text.ToString(), txtCreditAccount.Text.Trim().ToString());
                    if (!sameAcct)
                    {
                        lblError.Text = Resources.labels.Accountnotsame;
                        return;
                    }
                }

                // CHECK SAME CCYCD
                bool sameCCYCD = false;
                sameCCYCD = objAcct.CheckSameCCYCD(ccyid, lblCurrency.Text);
                if (sameCCYCD)
                {
                    lblError.Text = Resources.labels.err_samecurrency;
                    return;
                }
            }
           


            lblSenderName.Text = senderName;
            lblSenderAccount.Text = ddlSenderAccount.SelectedItem.ToString();
            lblBalanceSender.Text = balanceSender;
            lblAmount_CR.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), hdCreditCCYID.Value);
            lbCCYID_CR.Text = hdCreditCCYID.Value;
            string senderfee = "0";
            string receiverfee = "0";
            string payer = "0";
            string fxAmount = "0";

            Hashtable htRate = new SmartPortal.IB.Transfer().GetRateFx(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), hdCreditCCYID.Value.ToString()), ccyid, hdCreditCCYID.Value.ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                hdExchangeRate.Value = htRate["EXCHANGERATE"].ToString();
                hdAmount.Value = htRate["AMOUNT"].ToString();
				if (hdAmount.Value.Equals("0")){
                    lblError.Text = Resources.labels.amounttoosmall;
                    return;
                }
                lblAmount_DB.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(htRate["AMOUNT"].ToString(), ccyid);

                lblRate.Text = ccyid + "/" + hdCreditCCYID.Value.ToString();
                lblRateAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyFX(hdExchangeRate.Value, ccyid);
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
            lbCCYID_DB.Text = ccyid;


            DataTable dtFee = new SmartPortal.IB.Bank().GetFeeFx(Session["userID"].ToString(), hdTrancode.Value, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(hdAmount.Value, ccyid), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), ccyid), ddlSenderAccount.SelectedValue, Utility.KillSqlInjection(txtCreditAccount.Text.Trim()), ccyid, hdCreditCCYID.Value);
            if (dtFee.Rows.Count != 0)
            {
                senderfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeSenderAmt"].ToString(), ccyid);
                receiverfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeReceiverAmt"].ToString(), ccyid);
            }
            lblPhiAmount.Text = payer.Equals("0") ? senderfee : receiverfee;
            lblPhi.Text = payer.Equals("0") ? Resources.labels.nguoigui : Resources.labels.nguoinhan;
            lblDesc.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text);

            if (Session["accType"].ToString() != "IND")
            {
                DataTable dt = Utility.UploadFile(FUDocument, lblError);
                    ViewState["TBLDOCUMENT"] = dt;
              
            }


        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        pnTRF.Visible = false;
        pnConfirm.Visible = true;
        pnOTP.Visible = false;
        pnResult.Visible = false;
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
            if (SmartPortal.Common.Utilities.Utility.isDouble(lblBalanceSender.Text, true) < (SmartPortal.Common.Utilities.Utility.isDouble(hdAmount.Value.Trim(), true) + SmartPortal.Common.Utilities.Utility.isDouble(lblPhiAmount.Text, true)))
            {
                lblError.Text = Resources.labels.amountinvalid;
                return;
            }

            new SmartPortal.IB.Transfer().CheckAmountFx(Session["userID"].ToString(), hdTrancode.Value, ddlSenderAccount.SelectedItem.Value, txtCreditAccount.Text.Trim(), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(hdAmount.Value.Trim(), lblAvailableBalCCYID.Text.Trim()), lblAvailableBalCCYID.Text, hdCreditCCYID.Value, "en-US", ref IPCERRORCODE, ref IPCERRORDESC);
            if (!IPCERRORCODE.Equals("0"))
            {
                lblError.Text = IPCERRORDESC;
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
                result = new SmartPortal.IB.Transfer().TransferFx(hdTrancode.Value, Session["userID"].ToString(), Utility.KillSqlInjection(ddlSenderAccount.SelectedItem.Value.Trim()), Utility.KillSqlInjection(txtCreditAccount.Text.Trim()), Utility.KillSqlInjection(lblCreditName.Text.Trim()), Utility.FormatMoneyInput(hdAmount.Value, lblAvailableBalCCYID.Text.Trim()), Utility.KillSqlInjection(lblAvailableBalCCYID.Text.Trim()), Utility.KillSqlInjection(hdBranchID.Value.Trim()), Utility.KillSqlInjection(hdCreditBranchID.Value.Trim()), Utility.KillSqlInjection(lblDesc.Text == "" ? "Foreign Exchange Transfer" : lblDesc.Text), otpCode.Trim(), ddlLoaiXacThuc.SelectedValue, Utility.KillSqlInjection(lblSenderName.Text.Trim()), Utility.KillSqlInjection(hdPhoneNo.Value), Utility.KillSqlInjection(hdCreditCCYID.Value.Trim()), Utility.KillSqlInjection(hdExchangeRate.Value), Utility.FormatMoneyInput(txtAmount.Text, hdCreditCCYID.Value.Trim()), ref IPCERRORCODE, ref IPCERRORDESC, tbldocument, Session["accType"].ToString());

                if (!result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    SGD.Visible = false;
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
                lblEndReceiverName.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblCreditName.Text);
                lblEndCreditAccount.Text = Utility.MaskDigits(txtCreditAccount.Text.Trim().ToString());
                lblEndAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(hdAmount.Value.Trim(), hdCreditCCYID.Value.Trim());
                lblEndPhi.Text = lblPhi.Text;
                lblEndPhiAmount.Text = lblPhiAmount.Text;
                lblEndDesc.Text = lblDesc.Text == "" ? "Foreign Exchange Transfer" : lblDesc.Text;

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
                    hasPrint.Add("recieverAccount", lblEndCreditAccount.Text);
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
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=IB_TRANFERFX"));
    }
    protected void lbtGetInfoCreditAccount_OnClick(object sender, EventArgs e)
    {
        try
        {
           if (txtCreditAccount.Text != "")
            {
                LoadAccountInforeceiver();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadAccountInforeceiver()
    {
        try
        {
            string account;
            if (txtCreditAccount.Text.ToString() != "")
            {
                account = txtCreditAccount.Text.ToString();
				  string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string User = Session["userID"].ToString();
            Account acct = new Account();
            Hashtable ht = new Hashtable();
            ht = acct.GetInfoAccount(User, account, ref ErrorCode, ref ErrorDesc);
            if (ErrorCode.Equals("0"))
            {
                ShowDDreceiver(account, ht);
            }
            else
            {
                lblError.Text = Resources.labels.creditacccountinvalid;
            }

            }
            else
            {
               lblError.Text = Resources.labels.creditacccountinvalid;

            }

          
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferBAC1_Widget", "LoadAccountInforeceiver", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void ShowDDreceiver(string acctno, Hashtable htact)
    {
        try
        {
            //lblCreditAccount.Text = txtCreditAccount.Text.Trim();
            lblCreditAccount.Text = Utility.MaskDigits(txtCreditAccount.Text.Trim().ToString());
            if (htact[SmartPortal.Constant.IPC.CCYID] != null)
            {
                lblCurrency.Text = htact[SmartPortal.Constant.IPC.CCYID].ToString();
            }
            if (htact[SmartPortal.Constant.IPC.AVAILABLEBALANCE] != null)
            {
                //hdBalanceReceiver.Value = SmartPortal.Common.Utilities.Utility.FormatStringCore(htact[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString().Trim());
                //lblAvailableBalr.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(hdBalanceReceiver.Value, htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim());
            }
            if (htact["TYPEID"] != null)
            {
                hdActTypeReceiver.Value = htact["TYPEID"].ToString();
            }
            if (htact[SmartPortal.Constant.IPC.FULLNAME] != null)
            {
                LabelReceiverName.Text = htact[SmartPortal.Constant.IPC.FULLNAME].ToString();
                LabelReceiverName.Visible = true;
                lblCreditName.Text = htact[SmartPortal.Constant.IPC.FULLNAME].ToString();
            }
            if (htact[SmartPortal.Constant.IPC.BRANCHID] != null)
            {
                hdCreditBranchID.Value = htact[SmartPortal.Constant.IPC.BRANCHID].ToString();
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
}
