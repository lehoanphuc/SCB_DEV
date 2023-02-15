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
using SmartPortal.IB;
using SmartPortal.Common;

using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using System.Text.RegularExpressions;

public partial class Widgets_IBTaxPayment_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : Utility.createTableDocumnet(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }
    private DataTable TABLE
    {
        get { return ViewState["TABLE"] as DataTable; }
        set
        {
            ViewState["TABLE"] = value;
        }
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
                    {
                        culture = new PortalSettings().portalSetting.DefaultLang;
                    }
                    else
                    {
                        culture = Session["langID"].ToString();
                    }
                }
                else
                {
                    culture = Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["l"].ToString());
                    Session["langID"] = culture;
                }

                if (culture != "mk")
                {
                    trNote.Visible = true;
                }
                else
                {
                    trNote.Visible = false;
                }

                if ((Session["accType"].ToString() != "IND"))
                {
                    LblDocument.Visible = true;
                    FUDocument.Visible = true;
                    LblDocumentExpalainion.Visible = true;
                }
                pnAmount.Visible = false;
                pnConfirm.Visible = false;
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
            string errorcode = "";
            string errorDesc = "";
            DataSet ds = new DataSet();
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            //ds = objAcct.GetInfoAcct(Session["userID"].ToString(), "IB000604", "", ref errorcode, ref errorDesc);
            ds = objAcct.getAccount(Session["userID"].ToString(), "IBTAXPAYMENT", "", ref errorcode, ref errorDesc);

            ds.Tables[0].DefaultView.RowFilter = "accttype in ('DD', 'CD') and status = 'A' and CCYID = 'LAK'";

            if (!IsPostBack)
            {
                ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                ddlSenderAccount.DataValueField = ds.Tables[0].Columns[SmartPortal.Constant.IPC.ACCTNO].ColumnName.ToString();
                ddlSenderAccount.DataBind();
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

    protected void btnNextP2_Click(object sender, EventArgs e)
    {
        try
        {
            string senderName = "";
            string receiverName = "";
            string balanceSender = "";
            string acctCCYID = "";
            string receiverCCYID = "";
            if (!string.IsNullOrEmpty(txtTIN.Text.Trim()))
            {
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.SelectedItem.Text.Trim());
                //Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.Text.Trim());
                if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    senderName = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                    balanceSender =
                        SmartPortal.Common.Utilities.Utility.FormatStringCore(
                            hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                    acctCCYID = hasSender["CURRENCYID"].ToString();
                    lblSenderBranch.Text = hasSender[SmartPortal.Constant.IPC.BRID].ToString();
                }
                else
                {
                    lblTextError.Text = "Error : " + hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }

                //check ccyid
                bool sameCCYCD = objAcct.CheckSameCCYCD(acctCCYID, lblCurrency.Text);
                if (!sameCCYCD)
                {
                    lblTextError.Text = Resources.labels.invalidacctCCYCDtotransfer;
                    return;
                }

                #region tinh phi

                double fee = 0;
                DataTable dtfee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), "IBTAXPAYMENT",
                    SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text),
                    ddlSenderAccount.SelectedValue.Trim(), lblSenderBranch.Text, acctCCYID, "");
                if (dtfee.Rows.Count > 0)
                {
                    fee += Double.Parse(dtfee.Rows[0]["feeSenderAmt"].ToString());
                    fee += Double.Parse(dtfee.Rows[0]["feeReceiverAmt"].ToString());
                }

                #endregion

                // CHECK AMOUNT
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) >
                    SmartPortal.Common.Utilities.Utility.isDouble(balanceSender, true))
                {
                    lblTextError.Text = Resources.labels.amountinvalid;
                    return;
                }

                lblSenderAccount.Text = ddlSenderAccount.SelectedItem.Text;
                lblSenderName.Text = senderName;
                lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender, acctCCYID);
                lblSenderCCYID.Text = acctCCYID;
                lbCCYID.Text = acctCCYID;

                lblTaxNo.Text = Utility.KillSqlInjection(txtTIN.Text.Trim());
                lblSettlementbank2.Text = txtSettlementBank.Text;
                lblName2.Text = txtName.Text;
                lblAddress.Text = txtAddr.Text;
                lblEmail2.Text = txtEmail.Text;
                lblPhone2.Text = txtPhone.Text;
                lblTaxtype.Text = ddlpaymentOption.SelectedItem.Text;
                lblPaymenttype.Text = ddlpaymenttype.SelectedItem.Text;
                lblTaxperiod2.Text = ddlTaxPeriod.SelectedItem.Text;
                lblIncomeyear2.Text = ddlpaymentyear.SelectedItem.Text;
                lblAmount.Text =
                    SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), acctCCYID);
                lblDesc.Text = Utility.KillSqlInjection(txtDesc.Text.Trim()) == ""
                    ? "Tax payment"
                    : Utility.KillSqlInjection(txtDesc.Text.Trim());
                lblPhiAmount.Text =
                    SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(fee.ToString(), acctCCYID);
                lblFeeCCYID.Text = lblSenderCCYID.Text;
                lblPhi.Text = "Sender";
                lblTextError.Text = "";
                pnAmount.Visible = false;
                pnConfirm.Visible = true;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = false;
            }
            else
            {
                lblTextError.Text = "Input error!";
            }
            if (Session["accType"].ToString() != "IND")
            {
                DataTable dt = Utility.UploadFile(FUDocument, lblTextError);
                ViewState["TBLDOCUMENT"] = dt;
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

    protected void btnP1Next_Click(object sender, EventArgs e)
    {
        try
        {
            Regex r = new Regex(@"&");
            lblDebitAccountP2.Text = ddlSenderAccount.SelectedValue;
            lblAvailableBalP2.Text = lblAvailableBal.Text;
            lblAvailableBalCCYIDP2.Text = lblAvailableBalCCYID.Text;
            lblLastTranDateP2.Text = lblLastTranDate.Text;
            if (string.IsNullOrEmpty(txtTIN.Text.Trim()))
            {
                lblTextError.Text = "Please provide " + lblTino.Text;
                return;
            }
            if (string.IsNullOrEmpty(txtSettlementBank.Text.Trim()))
            {
                lblTextError.Text = "Please provide " + lblSettlementBank.Text;
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                lblTextError.Text = "Please provide " + lblName.Text;
                return;
            }
            if (string.IsNullOrEmpty(txtAddr.Text.Trim()))
            {
                lblTextError.Text = "Please provide " + lblAddr.Text;
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                lblTextError.Text = "Please provide " + lblEmail.Text;
                return;
            }
            if (string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                lblTextError.Text = "Please provide " + lblPhone.Text;
                return;
            }
            if (ddlpaymentOption.SelectedValue == "0")
            {
                lblTextError.Text = "Please provide " + lblTaxtypes.Text;
                return;
            }
            if (ddlpaymenttype.SelectedValue == "0")
            {
                lblTextError.Text = "Please provide " + lblPaymenttypes.Text;
                return;
            }
            if (ddlTaxPeriod.SelectedValue == "0" && hdHasTaxPeriod.Value != "0")
            {
                lblTextError.Text = "Please provide " + lblTaxPeriods.Text;
                return;
            }
            if (ddlpaymentyear.SelectedValue == "0")
            {
                lblTextError.Text = "Please provide " + lblIncomeYear.Text;
                return;
            }
            if (r.IsMatch(txtAddr.Text))
            {
                lblTextError.Text = lblAddr.Text + " must haven't & special characters";
                return;
            }
            if (txtAddr.Text.Length > 140)
            {
                lblTextError.Text = lblAddr.Text + " only maximum 140 characters";
                return;
            }
            pnTIB.Visible = false;
            pnAmount.Visible = true;
            pnOTP.Visible = false;
            pnResultTransaction.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void CheckTIN()
    {
        try
        {
            txtTIN.Attributes.Add("onblur", "Javascript:Validate3()");
            Hashtable hashtable = new SmartPortal.IB.Payment().GetTinNo(Utility.KillSqlInjection(txtTIN.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblTaxOfficeName.Text = hashtable["TAXOFFICENAME"].ToString();
                txtSettlementBank.Text = hashtable["CREDITORBRANCHNAME"].ToString();
                txtName.Text = hashtable["PROFILENAME"].ToString();
                txtAddr.Text = hashtable["STREET"].ToString();
                txtPhone.Text = hashtable["PHONENUMBER"].ToString();
                txtEmail.Text = hashtable["EMAILADDRESS"].ToString();
                DataSet ds = new DataSet();
                ds = (DataSet)hashtable["TAXTYPE"];
                if (ds != null)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    TABLE = dt;
                    LoadDataPaymentOption();
                    trPaymentOption.Visible = true;
                    LoadDataPaymentType();
                    trPaymentType.Visible = true;
                    LoadDataTaxperiod();
                }
                DataSet dsYear = new DataSet();
                dsYear = (DataSet)hashtable["INCOMEYEAR"];
                if (dsYear != null)
                {
                    DataTable dtYear = new DataTable();
                    dtYear = dsYear.Tables[0];
                    ddlpaymentyear.DataSource = dtYear;
                    ddlpaymentyear.DataValueField = "incomeYearCode";
                    ddlpaymentyear.DataTextField = "incomeYearDesc";
                    ddlpaymentyear.DataBind();
                    ddlpaymentyear.Items.Insert(0, new ListItem("-- Select Income Year --", "0"));
                    trIncomeYear.Visible = true;
                }
                pnTIB.Visible = true;
                pnTINO.Visible = true;
                pnAmount.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                btnCheckTIN.Visible = false;
                btnNext1.Visible = true;
            }
            else
            {
                lblTextError.Text = Resources.labels.tinokhongtontai;
            }
        }
        catch (Exception ex)
        {

        }
    }
    void LoadDataPaymentOption()
    {
        DataTable dtPaymentOption = TABLE.DefaultView.ToTable(true, "TAXTYPECODE", "TAXTYPEDESC");
        ddlpaymentOption.DataSource = dtPaymentOption;
        ddlpaymentOption.DataValueField = "TAXTYPECODE";
        ddlpaymentOption.DataTextField = "TAXTYPEDESC";
        ddlpaymentOption.DataBind();
        ddlpaymentOption.Items.Insert(0, new ListItem("-- Select Tax Type --", "0"));
    }
    void LoadDataPaymentType()
    {
        DataTable result = new DataTable();
        result.Columns.Add("PAYMENTTYPECODE", typeof(string));
        result.Columns.Add("PAYMENTTYPEDESC", typeof(string));
        DataRow[] rows = TABLE.Select("TAXTYPECODE ='" + ddlpaymentOption.SelectedValue.ToString() + "'");
        for (int i = 0; i < rows.Length; i++)
        {
            DataRow row = result.NewRow();
            row["PAYMENTTYPECODE"] = rows[i]["PAYMENTTYPECODE"].ToString();
            row["PAYMENTTYPEDESC"] = rows[i]["PAYMENTTYPEDESC"].ToString();
            result.Rows.Add(row);
        }
        DataTable dtPaymentType = result.DefaultView.ToTable(true, "PAYMENTTYPECODE", "PAYMENTTYPEDESC");
        ddlpaymenttype.DataSource = dtPaymentType;
        ddlpaymenttype.DataValueField = "PAYMENTTYPECODE";
        ddlpaymenttype.DataTextField = "PAYMENTTYPEDESC";
        ddlpaymenttype.DataBind();
        ddlpaymenttype.Items.Insert(0, new ListItem("-- Select Payment Type --", "0"));
    }
    void LoadDataTaxperiod()
    {
        DataTable result = new DataTable();
        result.Columns.Add("TAXPERIODCODE", typeof(string));
        result.Columns.Add("TAXPERIODDESC", typeof(string));
        DataRow[] rows = TABLE.Select("TAXTYPECODE ='" + ddlpaymentOption.SelectedValue.ToString() + "' AND PAYMENTTYPECODE ='" + ddlpaymenttype.SelectedValue.ToString() + "'");
        for (int i = 0; i < rows.Length; i++)
        {
            DataRow row = result.NewRow();
            row["TAXPERIODCODE"] = rows[i]["TAXPERIODCODE"].ToString();
            row["TAXPERIODDESC"] = rows[i]["TAXPERIODDESC"].ToString();
            result.Rows.Add(row);
        }
        DataTable dtTaxperiod = result.DefaultView.ToTable(true, "TAXPERIODCODE", "TAXPERIODDESC");
        if (dtTaxperiod.Rows.Count > 1)
        {
            hdHasTaxPeriod.Value = "1";
            trTaxPeriod.Visible = true;
            trTaxPeriod2.Visible = true;
            trTaxPeriod3.Visible = true;
        }
        else
        {
            hdHasTaxPeriod.Value = "0";
            trTaxPeriod.Visible = false;
            trTaxPeriod2.Visible = false;
            trTaxPeriod3.Visible = false;
        }
        ddlTaxPeriod.DataSource = dtTaxperiod;
        ddlTaxPeriod.DataValueField = "TAXPERIODCODE";
        ddlTaxPeriod.DataTextField = "TAXPERIODDESC";
        ddlTaxPeriod.DataBind();
        ddlTaxPeriod.Items.Insert(0, new ListItem("-- Select Tax Period --", "0"));
    }
    protected void paymentOption_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataPaymentType();
        LoadDataTaxperiod();
    }
    protected void paymenttype_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataTaxperiod();
    }
    protected void btnBackP2_Click(object sender, EventArgs e)
    {
        pnAmount.Visible = false;
        pnTIB.Visible = true;
        pnOTP.Visible = false;
        pnResultTransaction.Visible = false;
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(Session["userLevel"].ToString().Trim()) > 5)
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBTransferInBank1_Widget", "btnApply_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferInBank1_Widget", "btnApply_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private Object m_lock = new Object();
    protected void btnAction_Click(object sender, EventArgs e)
    {
        lock (m_lock)
        {
            string TAXOFFICENAME = String.Empty;
            string CREDITORBRANCHNAME = String.Empty;
            string PROFILENAME = String.Empty;
            string CREDITORBRANCHCODE = String.Empty;
            string MDACCNO = String.Empty;
            string REFNO = String.Empty;

            Hashtable hashtable = new SmartPortal.IB.Payment().GetTinNo(Utility.KillSqlInjection(txtTIN.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (hashtable["TAXOFFICENAME"] != null)
                {
                    TAXOFFICENAME = hashtable["TAXOFFICENAME"].ToString();
                }
                if (hashtable["CREDITORBRANCHNAME"] != null)
                {
                    CREDITORBRANCHNAME = hashtable["CREDITORBRANCHNAME"].ToString();
                }
                if (hashtable["PROFILENAME"] != null)
                {
                    PROFILENAME = hashtable["PROFILENAME"].ToString();
                }
                if (hashtable["CREDITORBRANCHCODE"] != null)
                {
                    CREDITORBRANCHCODE = hashtable["CREDITORBRANCHCODE"].ToString();
                }
                if (hashtable["MDACCNO"] != null)
                {
                    MDACCNO = hashtable["MDACCNO"].ToString();
                }
                if (hashtable["REFNO"] != null)
                {
                    REFNO = hashtable["REFNO"].ToString();
                }
            }
            else
            {
                lblTextError.Text = Resources.labels.tinokhongtontai;
                return;
            }
            Hashtable result = new Hashtable();
            string OTPcode = txtOTP.Text;
            txtOTP.Text = "";
            try
            {

                DataTable tbldocument = (DataTable)ViewState["TBLDOCUMENT"];
                result = new SmartPortal.IB.Payment().TaxPayment(CREDITORBRANCHCODE, Utility.KillSqlInjection(lblSenderName.Text.Trim()), txtAddr.Text.Trim(), Utility.KillSqlInjection(txtPhone.Text.Trim()), PROFILENAME, MDACCNO, Utility.KillSqlInjection(ddlSenderAccount.SelectedItem.Text.Trim()), Utility.KillSqlInjection(lblCurrency.Text.Trim()),
                    SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, Utility.KillSqlInjection(lblCurrency.Text.Trim())), Utility.KillSqlInjection(txtEmail.Text.Trim()), Utility.KillSqlInjection(ddlpaymentOption.SelectedValue.Trim()), Utility.KillSqlInjection(ddlpaymenttype.SelectedValue.Trim()), Utility.KillSqlInjection(ddlTaxPeriod.SelectedValue.Trim()), Utility.KillSqlInjection(ddlpaymentyear.SelectedValue.Trim()), Utility.KillSqlInjection(txtTIN.Text.Trim()), ddlLoaiXacThuc.SelectedValue.ToString(), OTPcode, REFNO, Utility.KillSqlInjection(ddlpaymentOption.SelectedItem.Text), Utility.KillSqlInjection(ddlpaymenttype.SelectedItem.Text.Trim()), ddlTaxPeriod.SelectedValue == "0" ? "" : Utility.KillSqlInjection(ddlTaxPeriod.SelectedItem.Text.Trim()), ddlpaymentyear.SelectedItem.Text.Trim(), Session["userID"].ToString(), Utility.KillSqlInjection(lblSenderBranch.Text.Trim()), Utility.KillSqlInjection(txtDesc.Text.Trim()) == "" ? "Tax payment" : Utility.KillSqlInjection(txtDesc.Text.Trim()), CREDITORBRANCHNAME, ddlpaymentOption.SelectedValue == "0" ? "" : Utility.KillSqlInjection(ddlpaymentOption.SelectedItem.Text.Trim()), Utility.KillSqlInjection(ddlpaymentyear.SelectedItem.Text.Trim()), tbldocument, Session["accType"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);


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

                    hasPrint.Add("TIN", Utility.KillSqlInjection(txtTIN.Text.Trim()));
                    hasPrint.Add("SettlementBank", CREDITORBRANCHNAME);
                    hasPrint.Add("Name", PROFILENAME);
                    hasPrint.Add("Address", txtAddr.Text);
                    hasPrint.Add("Email", txtEmail.Text);
                    hasPrint.Add("Phone", txtPhone.Text);
                    hasPrint.Add("PaymentOption", ddlpaymentOption.SelectedItem.Text);
                    hasPrint.Add("PaymentType", ddlpaymenttype.SelectedItem.Text);
                    hasPrint.Add("TaxPeriod", ddlTaxPeriod.SelectedValue == "0" ? "" : ddlTaxPeriod.SelectedItem.Text);
                    hasPrint.Add("PaymentYear", ddlpaymentyear.SelectedItem.Text);

                    hasPrint.Add("amount", lblAmount.Text);
                    hasPrint.Add("amountchu", Utility.NumtoWords(Convert.ToDouble(lblAmount.Text)) + " " + lblCurrency.Text.Trim());
                    hasPrint.Add("desc", lblDesc.Text == "" ? "Tax payment" : lblDesc.Text);
                    hasPrint.Add("tranID", result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : "");
                    hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    hasPrint.Add("senderBranch", lblSenderBranch.Text);
                    hasPrint.Add("feeType", lblPhi.Text);
                    hasPrint.Add("feeAmount", lblPhiAmount.Text);
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
                        case "-89504":
                            lblTextError.Text = Resources.labels.taikhoankhongtontai;
                            return;
                        case "-1499":
                            lblTextError.Text = Resources.labels.khongthechuyenkhoan;
                            return;
                        case "-1492":
                            lblTextError.Text = Resources.labels.khongthechuyenkhoan;
                            return;
                        //case "9388":
                        //lblTextError.Text = Resources.labels.amountinvalid;
                        //return;
                        case "9999":
                        case "10499":
                            throw new Exception();
                        default:
                            lblTextError.Text = string.IsNullOrEmpty(result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString()) ? "Transaction error!" : result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
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

                lblTaxno3.Text = Utility.KillSqlInjection(txtTIN.Text.Trim());
                lblSettlementbank3.Text = txtSettlementBank.Text;
                lblName3.Text = txtName.Text;
                lblAddress3.Text = txtAddr.Text;
                lblEmail3.Text = txtEmail.Text;
                lblPhone3.Text = txtPhone.Text;
                lblTaxtype3.Text = ddlpaymentOption.SelectedItem.Text;
                lblPaymentType3.Text = ddlpaymenttype.SelectedItem.Text;
                lblTaxperiod3.Text = ddlTaxPeriod.SelectedValue == "0" ? "" : ddlTaxPeriod.SelectedItem.Text;
                lblIncomeyear3.Text = ddlpaymentyear.SelectedItem.Text;

                lblEndDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                lblEndSenderAccount.Text = lblSenderAccount.Text;
                lblEndBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0]["AVAILABLEBAL"].ToString()), lblSenderCCYID.Text.Trim());
                lblEndSenderName.Text = lblSenderName.Text;

                lblBalanceCCYID.Text = lblSenderCCYID.Text;
                lblAmountCCYID.Text = lblSenderCCYID.Text;
                lblEndAmount.Text = lblAmount.Text;
                lblEndFeeCCYID.Text = lblSenderCCYID.Text;
                lblEndPhi.Text = lblPhi.Text;
                lblEndPhiAmount.Text = lblPhiAmount.Text;
                lblEndDesc.Text = lblDesc.Text == "" ? "Tax payment" : lblDesc.Text;
                #endregion
                txtOTP.Text = "";
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
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
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=1119"));
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTaxPayment_Widget", "LoadAccountInfo", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void ShowDD(string acctno, DataSet ds)
    {
        try
        {
            if (ds.Tables[0].Rows.Count == 1)
            {
                //lblLastTranDate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.LASTTRANSDATE].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
                lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0]["AVAILABLEBAL"].ToString()), ds.Tables[0].Rows[0]["CURRENCYID"].ToString().Trim());
                lblAvailableBalCCYID.Text = ds.Tables[0].Rows[0]["CURRENCYID"].ToString();
                lblCurrency.Text = lblAvailableBalCCYID.Text;
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

    protected void btnCheckTIN_OnClick(object sender, EventArgs e)
    {
        try
        {
            var tinno = txtTIN.Text.Trim();
            if (string.IsNullOrEmpty(tinno))
            {
                lblTextError.Text = Resources.labels.nhapvaotino;
            }
            else
            {
                if (tinno.Length < 9 || tinno.Length > 15)
                {
                    lblTextError.Text = Resources.labels.batloitino;
                }
                else
                {
                    CheckTIN();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnBackOTP_Click(object sender, EventArgs e)
    {
        try
        {
            pnTIB.Visible = false;
            pnAmount.Visible = false;
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnResultTransaction.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
