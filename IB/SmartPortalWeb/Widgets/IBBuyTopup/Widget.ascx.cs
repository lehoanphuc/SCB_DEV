using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using Antlr.Runtime.JavaExtensions;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using SmartPortal.IB;
using System.Text.RegularExpressions;
using System.Text;

public partial class widgets_ibbuytopup_widget_ascx : WidgetBase
{
    private Stopwatch sw = null;
    string IPCERRORCODE;
    string IPCERRORDESC;
    static bool isApprove = false;
    static string revBranchID = null;
    static Hashtable hasPrint = new Hashtable();
    

    Dictionary<string, Dictionary<string, string>> diCard = new Dictionary<string, Dictionary<string, string>>();
    private DataTable TABLE
    {
        get { return ViewState["TABLE"] as DataTable; }
        set
        {
            ViewState["TABLE"] = value;
        }
    }
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : Utility.createTableDocumnet(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }
    public string IDREF
    {
        get { return ViewState["IDREF"] != null ? (string)ViewState["IDREF"] : string.Empty; }
        set { ViewState["IDREF"] = value; }
    }
    public string AMOUNT
    {
        get { return ViewState["AMOUNT"] != null ? (string)ViewState["AMOUNT"] : string.Empty; }
        set { ViewState["AMOUNT"] = value; }
    }

    public string Phone = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            load_unit(sender, e);
            //hide panel
            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
            Phone = new SmartPortal.SEMS.Contract().GETSMSPHONE(Session["userID"].ToString()).Rows[0]["PHONE"].ToString();
            if (!IsPostBack)
            {

                
                DataSet ds = new SmartPortal.SEMS.Topup().GetTecoList(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlTelco.DataTextField = "TelcoName";
                ddlTelco.DataValueField = "TelcoID";
                ddlTelco.DataSource = ds;
                ddlTelco.DataBind();

                LoadCardAmount(ddlTelco.SelectedValue.ToString());

                if ((Session["accType"].ToString() != "IND"))
                {
                    LblDocument.Visible = true;
                    FUDocument.Visible = true;
                    LblDocumentExpalainion.Visible = true;
                }
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResult.Visible = false;
                pnResultTransaction.Visible = false;
                LoadAccountInfo();
                lbEloadnotice.Text = Resources.labels.etopupnotsupportpostpaidmobile;
                ddlAmount_SelectedIndexChanged(null, null);
            }

        }
        catch
        {
        }
    }
    public void ddlTelco_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadCardAmount(ddlTelco.SelectedValue.ToString());
    }
    protected void LoadCardAmount(string selected)
    {
        try
        {
            DataSet dsamount = new SmartPortal.SEMS.Topup().GetAmountbyTelco(selected, ddlType.SelectedValue.ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                dsamount.Tables[0].Columns.Add("CardandCCYID", typeof(string), "CardAmount +' '+ CCYID");
                ddlAmount.DataTextField = "CardandCCYID";
                ddlAmount.DataValueField = "CardAmount";
                ddlAmount.DataSource = dsamount;
                ddlAmount.DataBind();
            }
        }
        catch (Exception EX)
        {

        }

    }
    public void btnApply_Click(object sender, EventArgs e)
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
            btnApply.Text = Resources.labels.confirm;
            pnConfirm.Visible = false;
            pnOTP.Visible = true;
            pnResult.Visible = false;
            pnTIB.Visible = false;
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

    protected void btnBacktoPanel2_Click(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnResult.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    //edit by vutran 06082014: send SMSOTP
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = string.Empty;
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
                    lblTextError.Text = Resources.labels.notregotp;
                    btnAction.Enabled = false;
                    break;
                default:
                    lblTextError.Text = IPCERRORDESC;
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

    private Object m_lock = new Object();

    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {


            pnConfirm.Visible = true;
            pnOTP.Visible = false; 
            pnResult.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
            btnBackTransfer.Visible = true;
            btnApply.Visible = true;
            btnAddnew.Visible = false;
        }
        catch
        {
        }
    }

    protected void btnBackResult_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?po=3&p=436"));
        }
        catch
        {
        }
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        lock (m_lock)
        {
            isApprove = false;
            Hashtable result = new Hashtable();
            hasPrint = new Hashtable();
            string OTPcode = txtOTP.Text;
            txtOTP.Text = String.Empty;
            string desc;
            try
            {
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                DataTable dtTelcoAcc = objAcct.GetTelcoAcc(ddlTelco.SelectedValue.Trim());

                string revname = ddlTelco.SelectedItem.Text.Trim();
                string TelcoAcc = dtTelcoAcc.Rows[0][0].ToString().Trim();
                revBranchID = String.Empty;
                DataTable tbldocument = (DataTable)ViewState["TBLDOCUMENT"];
                string amount = double.Parse(hdAMOUNT.Value).ToString();
                result = objAcct.BuyTopupOnline(Utility.KillSqlInjection(hdTranCode.Value.Trim()), Utility.KillSqlInjection(ddlTelco.SelectedValue.Trim()), Utility.KillSqlInjection(ddlAmount.SelectedValue.Trim()), Utility.KillSqlInjection(hdIDREF.Value), Utility.KillSqlInjection(txtPhoneNumber.Text.Trim()), Utility.KillSqlInjection(lblSenderBranch.Text.Trim()), Utility.KillSqlInjection(hdSenderAccountNo.Value.Trim()),
                        Utility.KillSqlInjection(amount), Utility.KillSqlInjection(hdCCYID.Value), Utility.KillSqlInjection(hdContent.Value), Session["userID"].ToString(), ddlLoaiXacThuc.SelectedValue, OTPcode, hdFeeSender.Value, hdFeeReceiver.Value, hdBILLTYPE.Value,tbldocument,Session["accType"].ToString());
                //DuyVK 20190530
                if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    pnOTP.Visible = false;
                    pnResult.Visible = false;
                    SessionPrint(result);
                    //send mail
                    SmartPortal.Common.EmailHelper.ETopupTransactionSuccess_SendMail(hasPrint, Session["userID"].ToString());
                }
                else
                {
                    ShowError(result);
                    return;
                }
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResult.Visible = false;
                pnResultTransaction.Visible = true;

                #region LOAD RESULT TRANSFER
                string errorCode = "0";
                string errorDesc = string.Empty;
                SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
                //DataSet dsDetailAcc = new DataSet();
                //dsDetailAcc = acct.GetInfoDD(Session["userID"].ToString(), lblSenderAccount.Text.Trim(), ref errorCode, ref errorDesc);
                string account = hdSenderAccountNo.Value.Trim();
                string Acctype = ddlSenderAccount.SelectedItem.Value.ToString();
                string ErrorCode = string.Empty;
                string ErrorDesc = string.Empty;
                string User = Session["userID"].ToString();
                Hashtable htact = new Hashtable();
                htact = acct.GetInfoAccount(User, account, ref ErrorCode, ref ErrorDesc);
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
                lblEndBalanceSender.Text = lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(htact["AVAILABLEBALANCE"].ToString()), htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim());
                lblEndSenderCCYID.Text = lblSenderCCYID.Text;
                lblEndSenderName.Text = lblSenderName.Text;
                lblEndReceiverName.Text = lblTelco.Text;
                lblEndBalanceReceiver.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hdAMOUNT.Value), hdCCYID.Value);
                lblEndReceiverCCYID.Text = lblACCYID.Text;

                lblFeeCCYID.Text = lblFCCYID.Text;
                lblEndAmountCCYID.Text = lblSenderCCYID.Text;
                lblEndAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hdAMOUNT.Value), hdCCYID.Value);
                lblEndPhi.Text = "Sender";
                lblEndPhiAmount.Text = lblPhiAmount.Text;
                if (!isApprove && result.ContainsKey("DEST_DESC"))
                    lblEndDesc.Text = result["DEST_DESC"].ToString();
                else
                    lblEndDesc.Text = hdContent.Value;

                if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0") && ddlType.SelectedValue.Equals("1"))
                {
                    LblSoftpin.Text = result["SOFTPIN"].ToString();
                }
                trPhoneNo.Visible = ddlType.SelectedValue.Equals("2") ? true : false;
                trSoftPin.Visible = ddlType.SelectedValue.Equals("1") ? true : false;
                lblPhoneNo.Text = txtPhoneNumber.Text;
                #endregion

                txtOTP.Text = "";

                if (!Session["TypeID"].ToString().Equals("CTK"))
                {
                    tranno.Visible = false;
                }

            }
            catch (IPCException IPCex)
            {
                SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                    this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                    Request.Url.Query);
                SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                    Request.Url.Query);

            }
        }
    }

    public void btnBacktoPanel1_Click(object sender, EventArgs eventArgs)
    {
        try
        {
            lblTextError.Text = string.Empty;
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnResult.Visible = false;
            pnTIB.Visible = true;
            pnResultTransaction.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    public void btnNewTransaction_Click(object sender, EventArgs eventArgs)
    {
        try
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?po=3&p=436"));
        }
        catch (Exception ex)
        {
        }
    }

    public void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs eventArgs)
    {
        LoadAccountInfo();
    }

    protected void btnTUIBNext_Click(object sender, EventArgs e)
    {
        try
        {
            string senderName = "";
            string balanceSender = "";
            string acctCCYID = "";
            string cardAmount = "";
            string cardCCYID = "";
            string feeReceiverAmt = string.Empty;
            string feeSenderAmt = string.Empty;
            if (hdActTypeSender.Value.Equals("WLM"))
            {
                hdTranCode.Value = "IBMTUWALLET";
            }
            else
            {
                hdTranCode.Value = "IBMTUBANKACT";
            }
            if (!Validate()) return;
            if (ddlType.SelectedValue.Equals("2"))
            {
                //check input empty
                if (string.IsNullOrEmpty(txtPhoneNumber.Text.Trim()))
                {
                    lblTextError.Text = Resources.labels.bannhapsodienthoai;
                    hdfvalidPhone.Value = Resources.labels.bannhapsodienthoai;
                    UpdatePanel.Update();
                    return;
                }
                if (!validateinputphone())
                    return;
                if (Convert.ToInt64(double.Parse(lblAvailableBal.Text, new CultureInfo("en-US"))) < Int32.Parse(double.Parse(txtAmount.Text.Trim(), new CultureInfo("en-US")).ToString()))
                {
                    lblTextError.Text = Resources.labels.insufficientELoad;
                    return;
                }
            }
            if (!lblAvailableBalCCYID.Text.Equals("LAK"))
            {
                lblTextError.Text = Resources.labels.invalidforeigncurrency;
                return;
            }
            DataSet dscode = new SmartPortal.SEMS.Topup().GetTecoCode(ddlTelco.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                lblTextError.Text = Resources.labels.invalidpaymentinformation;
                return;
            }
            if (dscode != null && dscode.Tables[0].Rows.Count != 0)
            {
                hdIDREF.Value = dscode.Tables[0].Rows[0]["TelcoCode"].ToString();
            }
            string[] Amount = ddlAmount.SelectedItem.ToString().Split(' ');
            hdAMOUNT.Value = cardAmount = txtAmount.Text;
            hdCCYID.Value = Amount[1].ToString().Trim();

            Hashtable htFee = new Account().CaculateFeeBill(Session["userID"].ToString(), Utility.KillSqlInjection(hdTranCode.Value.Trim()), hdIDREF.Value, txtPhoneNumber.Text.Trim(), "-", "-", ref IPCERRORCODE, ref IPCERRORDESC);
            if (htFee["IPCERRORCODE"].ToString().Equals("0"))
            {
                hdFeeSender.Value = feeSenderAmt = SmartPortal.Common.Utilities.Utility.FormatMoney(htFee["DEBITFEE"].ToString(), acctCCYID);
                hdFeeReceiver.Value = feeReceiverAmt = SmartPortal.Common.Utilities.Utility.FormatMoney(htFee["CREFEE"].ToString(), acctCCYID);

            }
            else
            {
                lblTextError.Text = Resources.labels.invalidpaymentinformation;
                return;
            }
            if (Session["accType"].ToString() != "IND")
            {
                DataTable dt = Utility.UploadFile(FUDocument, lblTextError);      
                    ViewState["TBLDOCUMENT"] = dt;
           
            }

            if (true)
            {
                string ErrorCode = string.Empty;
                string ErrorDesc = string.Empty;
                string User = Session["userID"].ToString();
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                DataSet ds = new DataSet();

                DataTable dtAccount = TABLE;
                acctCCYID = hdCCYID.Value;
                senderName = hdSenderName.Value;
                lblSenderBranch.Text = hdSenderBranch.Value;
                balanceSender = lblAvailableBal.Text;

                #region LOAD INFO

                hdfAmount.Value = hdAMOUNT.Value;
                hdSenderAccountNo.Value = ddlSenderAccount.SelectedValue.Trim();
                lblSenderAccount.Text = ddlSenderAccount.SelectedItem.Text;
                lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender, acctCCYID);
                lblSenderCCYID.Text = acctCCYID;
                lblSenderName.Text = senderName;
                lblContent.Text = hdContent.Value = txtContent.Text.Trim();
                lblTelco.Text = ddlTelco.SelectedItem.Text;
                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hdAMOUNT.Value), hdCCYID.Value);
                string tmpAmount = ddlAmount.SelectedItem.ToString();

                cardAmount = tmpAmount.Substring(0, tmpAmount.IndexOf(" "));
                cardCCYID = tmpAmount.Substring(tmpAmount.IndexOf(" ") + 1, tmpAmount.Length - tmpAmount.IndexOf(" ") - 1);
                lblFCCYID.Text = tmpAmount.Substring(tmpAmount.IndexOf(" ") + 1, tmpAmount.Length - tmpAmount.IndexOf(" ") - 1);

                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hdAMOUNT.Value), hdCCYID.Value);
                lblPhiAmount.Text = hdFeeSender.Value;
                lblACCYID.Text = cardCCYID;
                lblDiscount.Text = (feeReceiverAmt != "0.00") ? feeReceiverAmt : feeSenderAmt;
                lblDiscountCCIYD.Text = cardCCYID;

                trPhone.Visible = ddlType.SelectedValue.Equals("2") ? true : false;
                txtPhone.Text = txtPhoneNumber.Text;

                lblTextError.Text = "";
                pnConfirm.Visible = true;
                pnOTP.Visible = false;
                pnResult.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = false;
                btnApply.Enabled = true;
                btnBackTransfer.Visible = true;
                btnApply.Visible = true;
                btnAddnew.Visible = false;
                #endregion

            }
            else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.UNNAMEDTRANSFERTEMPLATE);
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
    private void load_unit(object sender, EventArgs e)
    {
        try
        {
                string ErrorCode = string.Empty;
                string ErrorDesc = string.Empty;
                DataSet ds = new DataSet();
                Account acct = new Account();
                ds = acct.GetListOfAccounts(Session["userID"].ToString(), "IBMTUBANKACT", "IBMTUWALLET", "DD,CD,WL", "LAK", ref IPCERRORCODE, ref IPCERRORDESC);
                TABLE = ds.Tables[0];
                if (IPCERRORCODE.Equals("0"))
                {
                    ds.Tables[0].DefaultView.RowFilter = "TYPEID IN ('DD', 'CD','WLM') AND STATUSCD in ('A','Y')";
                    ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                    ddlSenderAccount.DataTextField = ds.Tables[0].Columns["ACCOUNTNO"].ColumnName.ToString();
                    ddlSenderAccount.DataValueField = ds.Tables[0].Columns["UNIQUEID"].ColumnName.ToString();
                    ddlSenderAccount.DataBind();
                    ddlSenderAccount_SelectedIndexChanged(null, null);
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
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
    private void LoadAccountInfo()
    {
        try
        {

            string account = ddlSenderAccount.SelectedItem.Value.ToString();
            string Acctype = ddlSenderAccount.SelectedItem.Value.ToString();
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBBuyTopup_Widget", "LoadAccountInfo", ex.ToString(), Request.Url.Query);
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

    private void ShowDD(string acctno, Hashtable htact)
    {
        try
        {

            if (htact[SmartPortal.Constant.IPC.CCYID] != null)
            {
                hdCCYID.Value = lblAvailableBalCCYID.Text = lblCurrency.Text = htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim();
            }
            if (htact["ACCTNAME"] != null)
            {
                hdAccountName.Value = htact["ACCTNAME"].ToString();
            }
            if (htact["TYPEID"] != null)
            {
                hdActTypeSender.Value = htact["TYPEID"].ToString();
            }
            if (htact["AVAILABLEBALANCE"] != null && htact[SmartPortal.Constant.IPC.CCYID] != null)
            {
                lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(htact["AVAILABLEBALANCE"].ToString()), htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim());
            }
            if (htact[SmartPortal.Constant.IPC.FULLNAME] != null)
            {
                hdSenderName.Value = htact[SmartPortal.Constant.IPC.FULLNAME].ToString();
            }
            if (htact[SmartPortal.Constant.IPC.BRANCHID] != null)
            {
                hdSenderBranch.Value = htact[SmartPortal.Constant.IPC.BRANCHID].ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        //15.12.2015 minh add to fix error 9999 
        if (Session["print"] == null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //15.12.2015 minh add to fix error 9999 
        if (Session["print"] == null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
        }
    }
    public bool IsPhoneNumber(string number)
    {
        return Regex.Match(number, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").Success;
    }
    private bool validateinputphone()
    {
        try
        {
            lblTextError.Text = String.Empty;
            hdfvalidPhone.Value = String.Empty;
            UpdatePanel.Update();
            if (IsPhoneNumber(txtPhoneNumber.Text.Trim()) && !txtPhoneNumber.Text.Trim().Contains(" ") && txtPhoneNumber.Text.Trim().Length >= 9 && txtPhoneNumber.Text.Trim().Length <= 11)//validate phone number
            {
                DataSet ds = new SmartPortal.IB.Transactions().ETopup_GetTecoByPhoneNumber(txtPhoneNumber.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE.Equals("0"))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && !ds.Tables[0].Rows[0]["TelcoID"].ToString().Equals("0"))
                    {
                        string ErrorCode = string.Empty;
                        string ErrorDesc = string.Empty;
                        string phone = txtPhoneNumber.Text.Trim();
                        Hashtable hb = new Hashtable();
                        Account acct = new Account();
                        hb = acct.GetTelePhoneBalance(Session["userID"].ToString(), ds.Tables[0].Rows[0]["TelcoCode"].ToString(), phone, ref ErrorCode, ref ErrorDesc);

                        if (hb["BILLTYPE"] != null)
                        {
                            hdBILLTYPE.Value = hb["BILLTYPE"].ToString().Trim();

                            switch (hb["BILLTYPE"].ToString())
                            {
                                case "POS":
                                    lblTelcoType.Text = "Postpaid";
                                    lbBalanceTitle.Text = "Bill Amount";
                                    divTelcoBalance.Visible = true;
                                    break;
                                default:
                                    lblTelcoType.Text = "Prepaid";
                                    lbBalanceTitle.Text = "Remaining Balance";
                                    divTelcoBalance.Visible = true;
                                    break;
                            }
                        }
                        if (hb["AMOUNT"] != null)
                        {
                            lbBalance.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(hb["AMOUNT"].ToString(), hb["CCYID"].ToString()) + " " + hb["CCYID"].ToString();
                            if (hdBILLTYPE.Value.Equals("POS"))
                            {
                                txtAmount.Text = hb["AMOUNT"].ToString();
                            }
                        }
                        else
                        {
                            divTelcoBalance.Visible = false;
                        }

                        if (hb["EDITABLE"] != null)
                        {
                            txtAmount.Enabled = hb["EDITABLE"].Equals("Y");
                        }
                    }

                    if (ds.Tables[0].Rows.Count.Equals(0) || !ds.Tables[0].Rows[0]["ELOAD"].ToString().Equals("Y"))
                    {
                        lblTextError.Text = Resources.labels.yourtelecomnotsupportetopup;
                        hdfvalidPhone.Value = Resources.labels.yourtelecomnotsupportetopup;
                        UpdatePanel.Update();
                        if (!ds.Tables[0].Rows.Count.Equals(0))
                        {
                            try
                            {
                                lblTextError.Text = ds.Tables[0].Rows[0]["errdesc"].ToString();
                            }
                            catch { }
                        }
                        return false;
                    }
                    else
                    {
                        ddlTelco.SelectedValue = ds.Tables[0].Rows[0]["TelcoID"].ToString();
                        shownoticetelco();

                        LoadCardAmount(ddlTelco.SelectedValue.ToString());

                    }
                }
                else
                {
                    lblTextError.Text = Resources.labels.transactionerror;
                    hdfvalidPhone.Value = Resources.labels.transactionerror;
                    UpdatePanel.Update();
                }
            }
            else
            {
                lblTextError.Text = Resources.labels.phonenumberwrong;
                hdfvalidPhone.Value = Resources.labels.phonenumberwrong;
                UpdatePanel.Update();
                return false;
            }
            return true;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBTransferInBAC1_Widget", "btnApply_Click", IPCex.ToString(), Request.Url.Query);
        }
        catch (Exception ex)
        {
            lblTextError.Text = Resources.labels.phonenumberwrong;
        }
        return false;
    }
    protected void txtPhoneNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        validateinputphone();
    }
    protected void ddlAmount_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAmount.Text = ddlAmount.SelectedValue;
    }
    public string FormatMoneySelect(string m)
    {
        try
        {
            CultureInfo dk = new CultureInfo("en-US");
            string m1 = m;

            if (m == "" || m == "0" || m == "0,00" || m == "0.00")
            {
                m1 = "0";
            }
            else
            {
                m1 = decimal.Parse(m).ToString("N02", dk);
            }

            return m1.Replace(",", "");
        }
        catch (Exception)
        {
            return m;
        }
    }
    protected void ShowError(Hashtable result)
    {
        txtOTP.Text = String.Empty;
        btnPrint.Visible = false;
        btnView.Visible = false;
        switch (result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
        {
            case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                lblTextError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                }
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
                lblTextError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                return;
            case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                lblTextError.Text = Resources.labels.authentypeinvalid;
                return;
            default:
                lblTextError.Text = string.IsNullOrEmpty(result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString()) ? "Transaction error!" : result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                return;
        }

        pnOTP.Visible = false;
        pnConfirm.Visible = true;
        btnApply.Enabled = false;
        btnBackTransfer.Visible = false;
        btnApply.Visible = false;
        btnAddnew.Visible = true;
    }

    protected void SessionPrint(Hashtable result)
    {
        lblTextError.Text = Resources.labels.transactionsuccessful;

        //ghi vo session dung in
        string amount = SmartPortal.Common.Utilities.Utility.FormatMoney(result[SmartPortal.Constant.IPC.AMOUNT].ToString(), lblSenderCCYID.Text);
        hasPrint.Add("status", Resources.labels.thanhcong);
        hasPrint.Add("senderAccount", hdSenderAccountNo.Value);
        hasPrint.Add("senderBalance", lblBalanceSender.Text);
        hasPrint.Add("ccyid", lblSenderCCYID.Text);
        hasPrint.Add("senderName", lblSenderName.Text);
        hasPrint.Add("telecomname", lblTelco.Text);
        hasPrint.Add("cardamount", lblAmount.Text);
        hasPrint.Add("transferType", "");
        hasPrint.Add("amount", SmartPortal.Common.Utilities.Utility.FormatMoney(result[SmartPortal.Constant.IPC.AMOUNT].ToString(), lblSenderCCYID.Text));
        hasPrint.Add("amountchu", SmartPortal.Common.Utilities.Utility.NumtoWords(double.Parse(amount, new CultureInfo("en-US"))));
        hasPrint.Add("feeType", "Sender");
        hasPrint.Add("feeAmount", lblPhiAmount.Text);
        hasPrint.Add("desc", hdContent.Value);
        hasPrint.Add("tranID", result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : "");
        hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        hasPrint.Add("senderBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblSenderBranch.Text));
        hasPrint.Add("receiverBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(revBranchID));
        hasPrint.Add("phoneNo", txtPhoneNumber.Text.Trim());
        Session["print"] = hasPrint;

        btnPrint.Visible = true;
        btnView.Visible = true;
    }
    private void shownoticetelco()
    {
        if (ddlTelco.SelectedValue.ToString().Equals("3"))
        {
            string starttime = ConfigurationManager.AppSettings["Telenormaintainstart"].ToString();
            string endtime = ConfigurationManager.AppSettings["Telenormaintainend"].ToString();
            if (DateTime.Now >= DateTime.Parse(starttime) && DateTime.Now <= DateTime.Parse(endtime))
            {
                ShowPopUpMsg("Mainternance activity by Telenor start from" + DateTime.Parse(starttime).ToString("hh:mm tt dd/MM/yyyy") + " to " + DateTime.Parse(endtime).ToString("hh:mm tt dd/MM/yyyy") + ". Please comeback later.");
                txtPhoneNumber.Text = "";
            }
        }
    }
    private void ShowPopUpMsg(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
    }

    public bool Validate()
    {
        // CHECK AMOUNT
        if (string.IsNullOrEmpty(txtPhoneNumber.Text))
        {
            lblTextError.Text = Resources.labels.phonenumberwrong;
            return false;
        }

        if (string.IsNullOrEmpty(txtAmount.Text))
        {
            lblTextError.Text = Resources.labels.bancannhapsotien;
            return false;
        }
        if (string.IsNullOrEmpty(txtContent.Text))
        {
            lblTextError.Text = Resources.labels.bancannhapnoidung;
            return false;
        }
        if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(lblAvailableBal.Text.Trim(), true))
        {
            lblTextError.Text = Resources.labels.amountinvalid;
            return false;
        }
        return true;
    }
}
