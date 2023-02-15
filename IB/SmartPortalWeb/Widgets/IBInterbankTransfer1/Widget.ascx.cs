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

public partial class Widgets_IBTransferInBank1_Widget : WidgetBase
{
    private Stopwatch sw = null;
    string IPCERRORCODE;
    string IPCERRORDESC;
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : Utility.createTableDocumnet(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
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
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
				if ((Session["accType"].ToString() != "IND"))
                {
                    LblDocument.Visible = true;
                    FUDocument.Visible = true;
                    LblDocumentExpalainion.Visible = true;
                }
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
                ds = objAcct.GetListOfAccounts(Session["userID"].ToString(), "IBINTERBANKTRANSFER", "IBINTERBANKWL", "DD,CD", "", ref errorcode, ref errorDesc);
                ds.Tables[0].DefaultView.RowFilter = "TYPEID IN ('DD', 'CD','WLM') AND STATUSCD in ('A','Y') AND CCYID='LAK'";


                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].DefaultView.Count == 0)
                {
                    throw new BusinessExeption("User not register DD Account To Transfer.");
                }

                ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                ddlSenderAccount.DataValueField = ds.Tables[0].Columns["UNIQUEID"].ColumnName.ToString();
                ddlSenderAccount.DataTextField = ds.Tables[0].Columns["ACCOUNTNO"].ColumnName.ToString();
                ddlSenderAccount.DataBind();

                ddlSenderCCY.DataSource = ds.Tables[0].DefaultView;
                ddlSenderCCY.DataTextField = "CCYID";
                ddlSenderCCY.DataValueField = ds.Tables[0].Columns["ACCOUNTNO"].ColumnName.ToString();
                ddlSenderCCY.DataBind();

                DataSet dtBankList = new SmartPortal.SEMS.PartnerBank().getListBankforInterBank(ref errorcode, ref errorDesc);
                ddlReceiverBank.DataSource = dtBankList;
                ddlReceiverBank.DataTextField = "BANKNAME";
                ddlReceiverBank.DataValueField = "BANKID";
                ddlReceiverBank.DataBind();
                ddlReceiverBank_SelectedIndexChanged(null, EventArgs.Empty);

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
    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        string senderName = "";
        string balanceSender = "";
        string acctCCYID = "";
        string receiverCCYID = "";
        try
        {
            if (string.IsNullOrEmpty(txtReceiverAccount.Text.Trim()))
            {
                lblTextError.Text = Resources.labels.accountnonotexist;
                return;
            }
            ddlSenderCCY.SelectedValue = Utility.KillSqlInjection(ddlSenderAccount.SelectedValue.Trim());
            if (!ddlSenderCCY.SelectedItem.Text.Equals("LAK"))
            {
                lblTextError.Text = Resources.labels.senderacctinvalid;
                return;
            }
            if ((cbmau.Checked == true && txttenmau.Text.ToString().Trim() != "") || (cbmau.Checked == false))
            {
                //CHECK RECEIVER ACCOUNT IS EXISTS
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                string receiverbankid = Utility.KillSqlInjection(ddlReceiverBank.SelectedValue.Trim());
                string receiverbankname = Utility.KillSqlInjection(ddlReceiverBank.SelectedItem.Text.Trim());
                string receiveracct = Utility.KillSqlInjection(txtReceiverAccount.Text.Trim());

                string senderacctno = Utility.KillSqlInjection(ddlSenderAccount.SelectedValue.Trim());
                string receiverName = Utility.KillSqlInjection(txtReceiverName.Text.Trim());


                Hashtable hasSender = objAcct.loadInfobyAcct(senderacctno);
                if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    senderName = hasSender["CUSTOMERNAME"].ToString();
                    balanceSender = Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                    acctCCYID = hasSender["CURRENCYID"].ToString();
                    lblSenderBranch.Text = hasSender[SmartPortal.Constant.IPC.BRID].ToString();
                }
                else
                {
                    lblTextError.Text = "Error : " + hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }
                // CHECK AMOUNT
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(balanceSender, true))
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
                if (Session["accType"].ToString() != "IND")
                {
                    DataTable dt = Utility.UploadFile(FUDocument, lblTextError);
                        ViewState["TBLDOCUMENT"] = dt;
             
                }

                #region LOAD INFO
                lblSenderAccount.Text = senderacctno;
                lblSenderName.Text = senderName;
                lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender, acctCCYID);
                lblSenderCCYID.Text = acctCCYID;
                lblFeeCCYID.Text = acctCCYID;
                lbCCYID.Text = acctCCYID;
                lblConfirmReceiverBankID.Text = receiverbankname;

                lblReceiverAccount.Text = receiveracct;
                lblReceiverName.Text = receiverName;

                #region check same name template transfer
                if (cbmau.Checked == true && txttenmau.Text.ToString().Trim() != "")
                {
                    new SmartPortal.IB.Transfer().CheckNameTransferTemplate(Utility.KillSqlInjection(txttenmau.Text.Trim()), Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {

                    }
                    else
                    {
                        if (IPCERRORDESC == "110211")
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.SAMENAMETRANSFERTEMPLATE);
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                        }
                    }
                }
                #endregion

                lblAmount.Text = Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), acctCCYID);
                lblDesc.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text);

                #region tinh phi
                //edit by VuTran 19/09/2014: tinh lai phi
                string senderfee = "0";
                string receiverfee = "0";
                if (hdTypeID.Value.Equals("WLM"))
                {
                    hdTrancode.Value = "IBINTERBANKWL";
                }
                else
                {
                    hdTrancode.Value = "IBINTERBANKTRANSFER";
                }

                Hashtable HTCheckAmount = new Account().CheckAmountPayment(Session["userID"].ToString(),
 hdTrancode.Value, ddlSenderAccount.SelectedValue, Utility.KillSqlInjection(txtReceiverAccount.Text.Trim()), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ref IPCERRORCODE, ref IPCERRORDESC);
                if (!IPCERRORCODE.ToString().Equals("0"))
                {
                    lblTextError.Text = IPCERRORDESC;
                    return;
                }

                DataTable dtFee = null;


                dtFee = new SmartPortal.IB.Bank().GetFeeV2(Session["userID"].ToString(), hdTrancode.Value, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ddlSenderAccount.SelectedValue, lblReceiverAccount.Text.Trim(), lblCurrency.Text, "");
                if (dtFee != null && dtFee.Rows.Count != 0)
                {
                    SmartPortal.Common.Log.RaiseError("", this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "senderfee Before parse: " + dtFee.Rows[0]["feeSenderAmt"].ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.RaiseError("", this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "receiverfee Before parse: " + dtFee.Rows[0]["feeReceiverAmt"].ToString(), Request.Url.Query);
                    senderfee = Utility.FormatMoney(dtFee.Rows[0]["feeSenderAmt"].ToString(), acctCCYID);
                    receiverfee = Utility.FormatMoney(dtFee.Rows[0]["feeReceiverAmt"].ToString(), acctCCYID);
                }

                CultureInfo culture = new CultureInfo("en-US");
                CultureInfo.CurrentCulture.NumberFormat = culture.NumberFormat;
                double recevfee = Double.Parse(receiverfee, culture);
                double sendfee = Double.Parse(senderfee, culture);
                double totalfee = (recevfee != 0) ? recevfee : sendfee;
                SmartPortal.Common.Log.RaiseError("", this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "senderfee After parse: " + sendfee, Request.Url.Query);
                SmartPortal.Common.Log.RaiseError("", this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "receiverfee After parse: " + recevfee, Request.Url.Query);
                SmartPortal.Common.Log.RaiseError("", this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "Total Fee After parse: " + totalfee, Request.Url.Query);

                double totalamount = Double.Parse(txtAmount.Text.Trim(), culture);
                #endregion

                if (totalamount <= totalfee && (recevfee != 0))
                {
                    lblTextError.Text = Resources.labels.amountgreaterthanfee;
                    return;
                }
                SmartPortal.Common.Log.RaiseError("", this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "Total Fee After parse and calculate: " + totalfee, Request.Url.Query);
                lblPhi.Text = (recevfee != 0) ? "Receiver" : "Sender";
                totalamount = (recevfee != 0) ? (totalamount - totalfee) : totalamount;
                hdfSenderFee.Text = (recevfee != 0) ? "0" : (sendfee).ToString();
                hdfReceiverFee.Text = (recevfee != 0) ? (recevfee).ToString() : "0";
                lblFinalAmount.Text = Utility.FormatMoneyInput(totalamount.ToString(), acctCCYID);
                lblFinalFee.Text = Utility.FormatMoneyInput((recevfee != 0) ? receiverfee : senderfee, acctCCYID);
                lblPhiAmount.Text = (recevfee != 0) ? "0" : Utility.FormatMoneyInputToView(totalfee.ToString(), acctCCYID);

                lblTextError.Text = "";
                pnConfirm.Visible = true;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = false;

                #endregion
            }
            else
            {
                lblTextError.Text = "Template name can not be empty";
            }
           
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
                string receiverbankid = Utility.KillSqlInjection(ddlReceiverBank.SelectedValue.Trim());
                string receiverbankname = Utility.KillSqlInjection(ddlReceiverBank.SelectedItem.Text.Trim());
                string receiveracct = Utility.KillSqlInjection(txtReceiverAccount.Text.Trim());
                //  DataRow drBank = new SmartPortal.SEMS.PartnerBank().LoadParnerBankByID(receiverbankid).Rows[0];
                string determination = string.Empty;
                string rcvbranchname = Utility.KillSqlInjection(ddlReceiverBank.SelectedItem.Text.Trim());
                string rcvbranchcode = Utility.KillSqlInjection(ddlReceiverBank.SelectedItem.Value.Trim());
                string senderacctno = Utility.KillSqlInjection(ddlSenderAccount.SelectedValue.Trim());
                string receiverName = Utility.KillSqlInjection(txtReceiverName.Text.Trim());
                string trandesc = Utility.KillSqlInjection(lblDesc.Text.Trim());
                string amount = Utility.FormatMoneyInput(lblFinalAmount.Text, "LAK");
                string org_amount = Utility.FormatMoneyInput(Utility.KillSqlInjection(txtAmount.Text.Trim()), "LAK");
                string fee = Utility.FormatMoneyInput(lblFinalFee.Text, "LAK");
                string sendername = Utility.KillSqlInjection(lblSenderName.Text);

                Hashtable result = new Hashtable();
                string OTPcode = txtOTP.Text;
                txtOTP.Text = "";
                DataTable tbldocument = (DataTable)ViewState["TBLDOCUMENT"];
                result = new SmartPortal.IB.InterbankTransfer().IBInterbankTransfer(
                    Session["userID"].ToString(),
                    ddlLoaiXacThuc.SelectedValue,
                    OTPcode.Trim(),
                    senderacctno, trandesc,
                    sendername, string.Empty, string.Empty, receiverName,
                    string.Empty, string.Empty, receiveracct, hdSenderBranch.Value,
                    rcvbranchcode, rcvbranchname, amount, "LAK", tbldocument, Session["accType"].ToString());
                if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {

                    lblTextError.Text = Resources.labels.transactionsuccessful;
                    if (cbmau.Checked == true)
                    {
                        new SmartPortal.IB.Transfer().InsertTransferTemplate(txttenmau.Text, txtDesc.Text, senderacctno, receiveracct, amount, lblCurrency.Text, hdTrancode.Value, Session["userID"].ToString(), "0", "", fee, "", "", "", "", "", receiverName, sendername, receiverbankid, "",rcvbranchcode, "", "", ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE == "0")
                        {
                            lblTextError.Text += " & " + Resources.labels.luumauchuyenkhoanthanhcong;
                        }
                        else
                        {
                            throw new IPCException(IPCERRORDESC);
                        }
                    }

                    //ghi vo session dung in
                    Hashtable hasPrint = new Hashtable();
                    hasPrint.Add("status", Resources.labels.thanhcong);
                    hasPrint.Add("senderAccount", lblSenderAccount.Text);
                    hasPrint.Add("senderBalance", lblBalanceSender.Text);
                    hasPrint.Add("ccyid", lblSenderCCYID.Text);
                    hasPrint.Add("senderName", lblSenderName.Text);
                    hasPrint.Add("recieverAccount", lblReceiverAccount.Text);
                    hasPrint.Add("recieverName", lblReceiverName.Text);
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

                    switch (result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblTextError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                            lblTextError.Text = Resources.labels.wattingbankapprove;
                            if (cbmau.Checked == true)
                            {
                                new Transfer().InsertTransferTemplate(txttenmau.Text, txtDesc.Text, senderacctno, receiveracct, amount, lblCurrency.Text, "IBINTERBANKTRANSFER", Session["userID"].ToString(), "0", "", fee, "", "", "", "", "", receiverName, sendername, receiverbankid, "", rcvbranchcode, "", "", ref IPCERRORCODE, ref IPCERRORDESC);

                                if (IPCERRORCODE == "0")
                                {
                                    lblTextError.Text += " & " + Resources.labels.luumauchuyenkhoanthanhcong;
                                }
                                else
                                {
                                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                                }
                            }

                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGPARTOWNERAPPROVE:
                            lblTextError.Text = Resources.labels.wattingpartownerapprove;
                            if (cbmau.Checked == true)
                            {
                                new Transfer().InsertTransferTemplate(txttenmau.Text, txtDesc.Text, senderacctno, receiveracct, amount, lblCurrency.Text, "IBINTERBANKTRANSFER", Session["userID"].ToString(), "0", "", fee, "", "", "", "", "", receiverName, sendername, receiverbankid, "", rcvbranchcode, "", "", ref IPCERRORCODE, ref IPCERRORDESC);

                                if (IPCERRORCODE == "0")
                                {
                                    lblTextError.Text += " & " + Resources.labels.luumauchuyenkhoanthanhcong;
                                }
                                else
                                {
                                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                                }
                            }
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                            lblTextError.Text = Resources.labels.wattinguserapprove;
                            if (cbmau.Checked == true)
                            {
                                new Transfer().InsertTransferTemplate(txttenmau.Text, txtDesc.Text, senderacctno, receiveracct, amount, lblCurrency.Text, "IBINTERBANKTRANSFER", Session["userID"].ToString(), "0", "", fee, "", "", "", "", "", receiverName, sendername, receiverbankid, "", rcvbranchcode, "", "", ref IPCERRORCODE, ref IPCERRORDESC);

                                if (IPCERRORCODE == "0")
                                {
                                    lblTextError.Text += " & " + Resources.labels.luumauchuyenkhoanthanhcong;
                                }
                                else
                                {
                                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                                }
                            }


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
                lblEndBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0]["AVAILABLEBAL"].ToString()), lblSenderCCYID.Text.Trim());
                lblEndSenderName.Text = lblSenderName.Text;

                lblEndReceiverAccount.Text = lblReceiverAccount.Text;
                lblEndReceiverName.Text = lblReceiverName.Text;
                lblEndReceiverBank.Text = lblConfirmReceiverBankID.Text;
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
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=1131"));
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
    protected void ddlReceiverBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";
            //DataTable dtBranch = new SmartPortal.SEMS.PartnerBank().LoadBranchByCondition(ddlReceiverBank.SelectedValue, "", "", "Y");

            //if (dtBranch != null && dtBranch.Rows.Count > 0)
            //{
            //    try
            //    {
            //        DataTable dtBranch2 = dtBranch.Select("Status = 'A'", "BranchName ASC").CopyToDataTable();
            //        if (dtBranch2 != null && dtBranch2.Rows.Count > 0)
            //        {
            //            ddlReceiverBranch.DataSource = dtBranch2;
            //            ddlReceiverBranch.DataTextField = "BranchName";
            //            ddlReceiverBranch.DataValueField = "BranchCode";
            //            ddlReceiverBranch.DataBind();
            //            trReceiverBranch.Visible = true;
            //            //lberror.Visible = true;

            //            ddlReceiverBranch.DataSource = dtBranch2;
            //            ddlReceiverBranch.DataTextField = "BranchName";
            //            ddlReceiverBranch.DataValueField = "BranchCode";
            //            ddlReceiverBranch.DataBind();
            //        }
            //    }
            //    catch
            //    {
            //       trReceiverBranch.Visible = false;
            //    }
            //}
            //else
            //{
            //   trReceiverBranch.Visible = false;
            //}
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
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
                hdBalanceSender.Value = lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0]["AVAILABLEBAL"].ToString()), ds.Tables[0].Rows[0]["CURRENCYID"].ToString().Trim());
                lblAvailableBalCCYID.Text = ds.Tables[0].Rows[0]["CURRENCYID"].ToString();
                lblCurrency.Text = lblAvailableBalCCYID.Text;
                if (ds.Tables[0].Rows[0]["TYPEID"] != null)
                {
                    hdTypeID.Value = ds.Tables[0].Rows[0]["CURRENCYID"].ToString();
                }
                if (ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.BRID] != null)
                {
                    hdSenderBranch.Value = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.BRID].ToString();
                }
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
            //SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

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
}
