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
using SmartPortal.Common;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using SmartPortal.IB;
using System.Diagnostics;
using System.Text;

public partial class Widgets_IBQRMerchantHistory_Widget : WidgetBase
{
    private Stopwatch sw = null;
    string IPCERRORCODE;
    string IPCERRORDESC;
    public string TXREF = string.Empty;
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : Utility.createTableDocumnet(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }
    public DataTable TBLFEE
    {
        get { return ViewState["TBLFEE"] != null ? (DataTable)ViewState["TBLFEE"] : new DataTable(); }
        set { ViewState["TBLFEE"] = TBLFEE; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";

            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
            load_unit(sender, e);

            //hide panel
            if (!IsPostBack)
            {
                string culture;
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["l"] == null)
                {
                    if (Session["langID"] == null)
                        culture = new PortalSettings().portalSetting.DefaultLang;
                    else
                        culture = Session["langID"].ToString();
                }
                else
                {
                    culture = Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["l"].ToString());
                    Session["langID"] = culture;
                }
		        if ((Session["accType"].ToString() != "IND"))
                {
                    LblDocument.Visible = true;
                    FUDocument.Visible = true;
                    LblDocumentExpalainion.Visible = true;
                }
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                LoadAccountInfo();
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void load_unit(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                string errorcode = "";
                string errorDesc = "";
                DataSet ds = new DataSet();
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                //ds = objAcct.getAccount(Session["userID"].ToString(), "IBINTERBANKTRANSFER", "", ref errorcode, ref errorDesc);
                ds = objAcct.GetListOfAccounts(Session["userID"].ToString(), "INTERBANK247", "WLINTERBANK247", "DD,CD", "", ref errorcode, ref errorDesc);
                ds.Tables[0].DefaultView.RowFilter = "TYPEID IN ('CD','DD') AND STATUSCD in ('A','Y')";


                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].DefaultView.Count == 0)
                {
                    throw new BusinessExeption("User not register DD Account To Transfer.");
                }

                ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                ddlSenderAccount.DataValueField = ds.Tables[0].Columns["UNIQUEID"].ColumnName.ToString();
                ddlSenderAccount.DataTextField = ds.Tables[0].Columns["ACCOUNTNO"].ColumnName.ToString();
                ddlSenderAccount.DataBind();

                DataSet dtBankList = new SmartPortal.SEMS.PartnerBank().getListBankfor247(ref errorcode, ref errorDesc);
                ddlReceiverBank.DataSource = dtBankList;
                ddlReceiverBank.DataTextField = "BankName";
                ddlReceiverBank.DataValueField = "BankCode";
                ddlReceiverBank.DataBind();

                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"] != null)
                {
                    string TID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"].ToString();
                    DataSet TemplateDS = new DataSet();
                    TemplateDS = new SmartPortal.IB.Transfer().LoadtemplateByID(TID, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (TemplateDS.Tables[0].Rows.Count != 0)
                    {
                        DataTable TemplateTB = TemplateDS.Tables[0];
                        ddlSenderAccount.SelectedValue = TemplateTB.Rows[0]["SENDERACCOUNT"].ToString();
                        txtReceiverAccount.Text = TemplateTB.Rows[0]["RECEIVERACCOUNT"].ToString();
                        ddlReceiverBank.SelectedValue = TemplateTB.Rows[0]["RECEIVERID"].ToString();
                        lblCurrency.Text = TemplateTB.Rows[0]["CCYID"].ToString();
                        txtDesc.Text = TemplateTB.Rows[0]["DESCRIPTION"].ToString();
                    }
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
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
    protected String randomID()
    {
        Random random = new Random();
        StringBuilder result = new StringBuilder(16);
        string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        for (int i = 0; i < 16; i++)
        {
            var a = characters[random.Next(characters.Length)];
            result.Append(characters[random.Next(characters.Length)]);
        }
        return result.ToString();
    }

    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        string receiverCCYID = hdReceiverCCYID.Value;
        string receiverName = "";
        string fee = "";
        try
        {

            if (string.IsNullOrEmpty(txtReceiverAccount.Text.Trim()))
            {
                lblTextError.Text = Resources.labels.accountnonotexist;
                return;
            }
            if (string.IsNullOrEmpty(txtAmount.Text.Trim()))
            {
                lblTextError.Text = Resources.labels.amountinvalid;
                return;
            }
            if (string.IsNullOrEmpty(txtDesc.Text.Trim()))
            {
                lblTextError.Text = Resources.labels.bancannhapnoidung;
                return;
            }
            // CHECK AMOUNT
            if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(hdBalanceSender.Value, true))
            {
                lblTextError.Text = Resources.labels.amountinvalid;
                return;
            }
            // Check same CCYID
            if (!hdReceiverCCYID.Value.Trim().Equals(hdSenderCCYID.Value.Trim()))
            {
                lblTextError.Text = Resources.labels.err_differentcurrency;
                return;
            }
            DataTable DtFee = (DataTable)ViewState["TBLFEE"];
            
            if (Session["accType"].ToString() != "IND")
            {
                DataTable dt = Utility.UploadFile(FUDocument, lblTextError);
                ViewState["TBLDOCUMENT"] = dt;

            }

            if (hdTypeID.Value.Equals("WLM"))
            {
                hdTrancode.Value = "WLINTERBANK247";
            }
            else
            {
                hdTrancode.Value = "INTERBANK247";
            }
            #region tinh phi
            //fee CMS
            foreach (DataRow rowfee in DtFee.Rows)
            {
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) >= SmartPortal.Common.Utilities.Utility.isDouble(rowfee["from"].ToString(), true))
                {
                    fee = rowfee["feeamount"].ToString();
                }
            }
            // Fee Product
            DataTable dtFee = null;
            string senderfee = "0";
            string receiverfee = "0";
            dtFee = new SmartPortal.IB.Bank().GetFeeV2(Session["userID"].ToString(), hdTrancode.Value, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ddlSenderAccount.SelectedValue, lblReceiverAccount.Text.Trim(), lblCurrency.Text, "");
            if (dtFee != null && dtFee.Rows.Count != 0)
            {
                SmartPortal.Common.Log.RaiseError("", this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "senderfee Before parse: " + dtFee.Rows[0]["feeSenderAmt"].ToString(), Request.Url.Query);
                SmartPortal.Common.Log.RaiseError("", this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "receiverfee Before parse: " + dtFee.Rows[0]["feeReceiverAmt"].ToString(), Request.Url.Query);
                senderfee = Utility.FormatMoney(dtFee.Rows[0]["feeSenderAmt"].ToString(), hdSenderCCYID.Value);
                receiverfee = Utility.FormatMoney(dtFee.Rows[0]["feeReceiverAmt"].ToString(), hdSenderCCYID.Value);
            }

            CultureInfo culture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture.NumberFormat = culture.NumberFormat;
            double recevfee = Double.Parse(receiverfee, culture);
            double sendfee = Double.Parse(senderfee, culture);
            double cmsfee = Double.Parse(fee, culture);
            double totalfee = (recevfee != 0) ? recevfee +cmsfee : sendfee + cmsfee;

            hdFeeLapNet.Value = cmsfee.ToString();
            hdTotalFee.Value = totalfee.ToString();
            #endregion
            //LOAD INFOR
            lblSenderAccount.Text = ddlSenderAccount.SelectedItem.ToString();
            hdsendeAccount.Value = ddlSenderAccount.SelectedValue;
            lblSenderName.Text = hdSenderName.Value;
            lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(hdBalanceSender.Value, hdSenderCCYID.Value);

            lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(txtAmount.Text, hdSenderCCYID.Value);
            lblSenderCCYID.Text = hdSenderCCYID.Value;
            lblFeeCCYID.Text = hdSenderCCYID.Value;
            lbCCYID.Text = hdSenderCCYID.Value;
            lblPhi.Text = "Sender";
            lblPhiAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(totalfee.ToString(), hdSenderCCYID.Value);

            lblConfirmReceiverBankID.Text = ddlReceiverBank.SelectedItem.ToString();
            lblReceiverAccount.Text = txtReceiverAccount.Text;
            lblReceiverName.Text = hdReceiverName.Value;
            lblReceiverCCYID.Text = receiverCCYID;

            lblDesc.Text = txtDesc.Text.Trim();

            lblTextError.Text = "";
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message.ToString(), Request.Url.Query);

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
            // check amout
            if (SmartPortal.Common.Utilities.Utility.isDouble(hdBalanceSender.Value, true) < (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) + SmartPortal.Common.Utilities.Utility.isDouble(lblPhiAmount.Text, true)))
            {
                lblTextError.Text = Resources.labels.amountinvalid;
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
            pnTIB.Visible = false;
            pnConfirm.Visible = false;
            pnOTP.Visible = true;
            pnResultTransaction.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private Object m_lock = new Object();
    protected void btnAction_Click(object sender, EventArgs e)
    {
        try
        {
            SmartPortal.IB.Account objAcct = new Account();
            lock (m_lock)
            {
                string transferid = hdtransferID.Value;
                string receiverbankid = Utility.KillSqlInjection(ddlReceiverBank.SelectedValue.Trim());
                string receiverbank = Utility.KillSqlInjection(ddlReceiverBank.SelectedItem.Text.Trim());
                string receiveracct = Utility.KillSqlInjection(txtReceiverAccount.Text.Trim());
                string determination = string.Empty;
                string rcvbranchname = Utility.KillSqlInjection(ddlReceiverBank.SelectedItem.Text.Trim());
                string rcvbranchcode = Utility.KillSqlInjection(ddlReceiverBank.SelectedItem.Value.Trim());
                string senderacctno = Utility.KillSqlInjection(ddlSenderAccount.SelectedValue.Trim());
                string trandesc = Utility.KillSqlInjection(lblDesc.Text.Trim());
                string org_amount = Utility.FormatMoneyInput(Utility.KillSqlInjection(txtAmount.Text.Trim()), hdReceiverCCYID.Value);
                string sendername = Utility.KillSqlInjection(lblSenderName.Text);
                string fee = Utility.FormatMoneyInput(Utility.KillSqlInjection(hdFeeLapNet.Value), hdReceiverCCYID.Value);
                string totalfee = Utility.FormatMoneyInput(Utility.KillSqlInjection(hdTotalFee.Value), hdReceiverCCYID.Value);

                Hashtable result = new Hashtable();
                string OTPcode = txtOTP.Text.Trim();
                DataTable tbldocument = (DataTable)ViewState["TBLDOCUMENT"];

                Hashtable hasInput = new Hashtable();
                //Hashtable hasOutput = new Hashtable();

                hasInput = new Hashtable()
                { 
                    {"TRANSFERID", transferid},
                    {SmartPortal.Constant.IPC.IPCTRANCODE, hdTrancode.Value },
                    {SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE },
                    {SmartPortal.Constant.IPC.TRANDESC, trandesc },
                    {SmartPortal.Constant.IPC.USERID, Session["userID"].ToString() },
                    {SmartPortal.Constant.IPC.AUTHENTYPE, ddlLoaiXacThuc.SelectedValue.ToString().Trim() },
                    {SmartPortal.Constant.IPC.AUTHENCODE, OTPcode },
                    {SmartPortal.Constant.IPC.ACCTNO, senderacctno },
                    {SmartPortal.Constant.IPC.RECEIVERACCOUNT, receiveracct },
                    {SmartPortal.Constant.IPC.SENDERNAME, sendername },
                    {SmartPortal.Constant.IPC.RECEIVERNAME, hdReceiverName.Value },
                    {SmartPortal.Constant.IPC.AMOUNT, org_amount },
                    {SmartPortal.Constant.IPC.FEE, fee },
                    {"TOTALFEE", totalfee },
                    {SmartPortal.Constant.IPC.CCYID, hdSenderCCYID.Value },
                    {SmartPortal.Constant.IPC.CONTRACTTYPE, Session["accType"].ToString() },
                    {"TOBANK", receiverbankid }
                };

                if(tbldocument != null)
                {
                    hasInput.Add("DOCUMENT", tbldocument);
                }

                result = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
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
                    hasPrint.Add("recieverAccount", lblReceiverAccount.Text);
                    hasPrint.Add("recieverName", lblReceiverName.Text);
                    hasPrint.Add("recieverCcyid", lblReceiverCCYID.Text);
                    hasPrint.Add("amount", lblAmount.Text);
                    hasPrint.Add("amountchu", Utility.NumtoWords(SmartPortal.Common.Utilities.Utility.isDouble(lblAmount.Text,true)));
                    hasPrint.Add("feeAmount", lblPhiAmount.Text);
                    hasPrint.Add("desc", lblDesc.Text);
                    hasPrint.Add("tranID", result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : "");
                    hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    hasPrint.Add("receiverBank", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblConfirmReceiverBankID.Text));
                    hasPrint.Add("senderBranch", hdSenderBranch.Value);
                    Session["print"] = hasPrint;

                    btnPrint.Visible = true;
                    btnView.Visible = true;
                }
                else
                {
                    txtOTP.Text = "";
                    btnPrint.Visible = false;
                    if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                    }
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
                            lblTextError.Text = "This OTP has expired. Please request a new OTP by clicking 'Resend OTP'.";
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
                        case "9388":
                            if (string.IsNullOrEmpty(result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString()))
                                throw new IPCException(result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim());
                            else
                            {
                                lblTextError.Text = "Not supported destination account";
                                SmartPortal.Common.Log.RaiseError(result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString(), Request.Url.Query);
                            }

                            return;
                        default:
                            lblTextError.Text = "System is suspended for a moment. Sorry for inconvenience. Please try again later";
                            return;
                    }

                    pnConfirm.Visible = true;
                    btnApply.Enabled = false;
                    btnBackTransfer.Enabled = false;
                    btnView.Visible = false;
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

                Hashtable hbaccno = acct.GetInfoAccount(Session["userID"].ToString(), ddlSenderAccount.SelectedItem.ToString().Trim(), ref errorCode, ref errorDesc);

                if (result["IPCTRANSID"] != null)
                {
                    lblEndTransactionNo.Visible = true;
                    lblEndTransactionNo.Text = result["IPCTRANSID"].ToString();
                }
                else
                {
                    lblEndTransactionNo.Visible = false;
                    Label30.Visible = false;
                }

                lblEndDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                lblEndSenderAccount.Text = lblSenderAccount.Text;
                lblEndBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hbaccno["AVAILABLEBALANCE"].ToString()), lblSenderCCYID.Text.Trim());
                lblEndSenderName.Text = lblSenderName.Text;

                lblEndReceiverAccount.Text = lblReceiverAccount.Text;
                lblEndReceiverName.Text = lblReceiverName.Text;
                lblEndReceiverBank.Text = lblConfirmReceiverBankID.Text;
                lblEndReceiverCCYID.Text = lblReceiverCCYID.Text;

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
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=INTERBANK247"));
    }
    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountInfo();
    }
    //edit by vutran 06082014: send SMSOTP
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
            Hashtable ht = new Hashtable();
            ht = acct.GetInfoAccount(User, account, ref ErrorCode, ref ErrorDesc);
            ShowDD(account, ht);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferBAC1_Widget", "LoadAccountInfo", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void ShowDD(string acctno, Hashtable htact)
    {
        try
        {
            hdSenderName.Value = htact[SmartPortal.Constant.IPC.FULLNAME].ToString();
            hdBalanceSender.Value = lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(htact[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString().Trim()), htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim());
            lblAvailableBalCCYID.Text = htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim();
            lblCurrency.Text = lblAvailableBalCCYID.Text;
            if (htact["TYPEID"] != null)
            {
                hdTypeID.Value = htact["TYPEID"].ToString();
                hdSenderCCYID.Value = htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim();
            }
            if (htact[SmartPortal.Constant.IPC.BRANCHID] != null)
            {
                hdSenderBranch.Value = lblSenderBranch.Text = htact[SmartPortal.Constant.IPC.BRANCHID].ToString();
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

    protected void txtReceiverAccount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string receiverCCYID = "";
            string receiverName = "";
            //CHECK RECEIVER ACCOUNT RECEIVER IS EXISTS
            SmartPortal.IB.Transactions objtrans = new SmartPortal.IB.Transactions();
            string sendername = Utility.KillSqlInjection(hdSenderName.Value);
            string receiverbankid = Utility.KillSqlInjection(ddlReceiverBank.SelectedValue.Trim());
            string transferid = randomID();
            hdtransferID.Value = transferid;
            string receiverbankname = Utility.KillSqlInjection(ddlReceiverBank.SelectedItem.Text.Trim());
            string receiveracct = Utility.KillSqlInjection(txtReceiverAccount.Text.Trim());
            string senderacctno = Utility.KillSqlInjection(ddlSenderAccount.SelectedValue.Trim());


            Hashtable hasReceiver = objtrans.OutgoingInquiry(transferid,sendername, senderacctno, receiverbankname, receiveracct, ref IPCERRORCODE, ref IPCERRORDESC);

            if (hasReceiver[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                receiverName = hdReceiverName.Value = hasReceiver["ACCNAME"].ToString();
                receiverCCYID = hasReceiver["ACCCCYID"].ToString();
                hdReceiverCCYID.Value = hasReceiver["ACCCCYID"].ToString();
                txtReceiverName.Text = hasReceiver["ACCNAME"].ToString();
                TXREF = hasReceiver["Txref"].ToString();
                ViewState["TBLFEE"] = ((DataSet)hasReceiver["DATASET"]).Tables[0];
            }
            else
            {
                lblTextError.Text = "Error : " + hasReceiver[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
