using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using System.Globalization;
using SmartPortal.BLL;
using SmartPortal.Constant;
using SmartPortal.DAL;
using SmartPortal.Model;
using SmartPortal.SEMS;

public partial class Widgets_SEMSInterFeeManagement_Controls_Widget : WidgetBase
{
    private int size = 0;
    public static bool isAscend = false;
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    public static string APPTRANID;
    public static int Fuid = 1;

    //check validate flag false cho nhap so am, true khong nhap so am
    public bool allowNegativeFee = true;
    DataTable ProDeTable = new DataTable();
    double reFromLimit, reToLimit, roToLimit, roFromLimit;
    string roDebitRegion, roCreditRegion, roFeeSide, roBankID, reDebitRegion, reCreditRegion, reFeeSide, reBankID;
    bool IsLadder, roIsLadder;
    string vFeeID
    {
        get { return ViewState["vFeeID"] != null ? ViewState["vFeeID"].ToString() : ""; }
        set { ViewState["vFeeID"] = value; }
    }
    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            txtFrom.Attributes.Add("onkeypress", "executeComma('" + txtFrom.ClientID + "')");
            txtTo.Attributes.Add("onkeypress", "executeComma('" + txtTo.ClientID + "')");
            if (allowNegativeFee)
            {
                txtAmount.Attributes.Add("onkeypress", "executeComma('" + txtAmount.ClientID + "')");
                txtMin.Attributes.Add("onkeypress", "executeComma('" + txtMin.ClientID + "')");
                txtmax.Attributes.Add("onkeypress", "executeComma('" + txtmax.ClientID + "')");
                txtFrom.Attributes.Add("onkeyup", "executeComma('" + txtFrom.ClientID + "')");
                txtTo.Attributes.Add("onkeyup", "executeComma('" + txtTo.ClientID + "')");
                txtAmount.Attributes.Add("onkeyup", "executeComma('" + txtAmount.ClientID + "')");
                txtMin.Attributes.Add("onkeyup", "executeComma('" + txtMin.ClientID + "')");
                txtmax.Attributes.Add("onkeyup", "executeComma('" + txtmax.ClientID + "')");

            }
            else
            {
                txtAmount.Attributes.Add("onkeypress", "return isNumberK(event)");
                txtMin.Attributes.Add("onkeypress", "return isNumberK(event)");
                txtmax.Attributes.Add("onkeypress", "return isNumberK(event)");
                txtAmount.Attributes.Add("onkeyup", "return isNumberK(event)");

                txtMin.Attributes.Add("onkeyup", "return isNumberK(event)");
                txtmax.Attributes.Add("onkeyup", "return isNumberK(event)");
                txtFrom.Attributes.Add("onkeyup", "executeComma('" + txtFrom.ClientID + "')");
                txtTo.Attributes.Add("onkeyup", "executeComma('" + txtTo.ClientID + "')");
            }

            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                ViewState["FEEDETAILS"] = null;

                ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlCCYID.DataTextField = "CCYID";
                ddlCCYID.DataValueField = "CCYID";
                ddlCCYID.DataBind();

                DataSet dtBank = new SmartPortal.SEMS.Partner().GetBankALL(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlBank.DataSource = dtBank;
                ddlBank.DataTextField = "BankName";
                ddlBank.DataValueField = "BankID";
                ddlBank.DataBind();
                ddlBank.Items.Insert(0, new ListItem(Resources.labels.allforbank, ""));

                DataSet dtRegion = new SmartPortal.SEMS.Partner().GetRegionALL(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlRegion.DataSource = dtRegion;
                ddlRegion.DataTextField = "RegionName";
                ddlRegion.DataValueField = "RegionID";
                ddlRegion.DataBind();

                LoadRegion();
                BindData();

                lbIDGen.Text = "FEE" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void BindData()
    {
        DataTable tblRoleDefault = new DataTable();
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    pnFee.Visible = true;
                    tbLadderFee.Visible = false;
                    btnClear.Enabled = true;
                    break;
                default:
                    btnClear.Enabled = false;
                    tbLadderFee.Visible = false;
                    vFeeID = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet dsfee = new SmartPortal.SEMS.InterBank().DetailsFee(vFeeID, SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBank.SelectedValue.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsfee.Tables.Count != 0)
                    {
                        ProDeTable = dsfee.Tables[0];
                        if (ProDeTable.Rows.Count != 0)
                        {
                            //bind data to panel
                            txtFeeName.Text = ProDeTable.Rows[0]["FEENAME"].ToString();
                            ddlCCYID.SelectedValue = ProDeTable.Rows[0]["CCYID"].ToString();

                            txtAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ProDeTable.Rows[0]["FIXAMOUNT"].ToString(), ProDeTable.Rows[0]["CCYID"].ToString().Trim());

                            if ((bool)(ProDeTable.Rows[0]["ISLADDER"]) == false && (bool)(ProDeTable.Rows[0]["ISBILLPAYMENTFEE"]) == false && (bool)(ProDeTable.Rows[0]["ISREGIONFEE"]) == false)
                            {
                                cbIsLadder.Checked = false;
                                tbLadderFee.Visible = false;
                                txtAmount.Enabled = true;
                            }
                            else
                            {
                                tbLadderFee.Visible = (bool)(ProDeTable.Rows[0]["ISLADDER"]);
                                cbIsLadder.Checked = (bool)(ProDeTable.Rows[0]["ISLADDER"]);
                                txtAmount.Enabled = false;
                                txtAmount.Text = "";
                                DataTable tblTransDetailsEdit = new DataTable();
                                DataColumn FeeID = new DataColumn("FEEID");
                                DataColumn From = new DataColumn("FROMLIMIT");
                                From.DataType = typeof(decimal);
                                DataColumn To = new DataColumn("TOLIMIT");
                                DataColumn Min = new DataColumn("MINAMOUNT");
                                DataColumn Max = new DataColumn("MAXAMOUNT");
                                DataColumn Rate = new DataColumn("RATE");
                                DataColumn FkID = new DataColumn("FkID");
                                DataColumn FixAmount = new DataColumn("FIXAMOUNT");
                                FixAmount.DataType = typeof(decimal);
                                DataColumn IsLader = new DataColumn("ISLADDER");
                                DataColumn DebitRegion = new DataColumn("DEBITREGION");
                                DataColumn CreditRegion = new DataColumn("CREDITREGION");
                                DataColumn FeeSide = new DataColumn("FEESIDE");
                                DataColumn BankID = new DataColumn("BANKID");
                                DataColumn ServiceFee = new DataColumn("SERVICEFEE");

                                tblTransDetailsEdit.Columns.AddRange(new DataColumn[] { FeeID, From, To, Min, Max, Rate, FkID, FixAmount, IsLader, DebitRegion, CreditRegion, FeeSide, BankID, ServiceFee });
                                foreach (DataRow ro in ProDeTable.Rows)
                                {
                                    DataRow r = tblTransDetailsEdit.NewRow();
                                    if (ro["FROMLIMIT"] is DBNull && ro["TOLIMIT"] is DBNull && ro["MINAMOUNT"] is DBNull && ro["MAXAMOUNT"] is DBNull && ro["FDFIXAMOUNT"] is DBNull && ro["RATE"] is DBNull)
                                    {
                                        break;
                                    }

                                    r["FEEID"] = vFeeID;
                                    r["TOLIMIT"] = ro["TOLIMIT"].ToString();
                                    r["FROMLIMIT"] = ro["FROMLIMIT"].ToString();
                                    r["MINAMOUNT"] = ro["MINAMOUNT"].ToString();
                                    r["MAXAMOUNT"] = ro["MAXAMOUNT"].ToString();
                                    r["RATE"] = ro["RATE"].ToString();
                                    r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                                    r["FIXAMOUNT"] = ro["FDFIXAMOUNT"].ToString();
                                    r["ISLADDER"] = (bool)(string.IsNullOrEmpty(ro["FDISLADDER"].ToString()) ? cbIsLadder.Checked : ro["FDISLADDER"]);
                                    r["DEBITREGION"] = ro["DEBITREGION"].ToString();
                                    r["CREDITREGION"] = ro["CREDITREGION"].ToString();
                                    r["FEESIDE"] = ro["FEESIDE"].ToString();
                                    r["BANKID"] = ro["BANKID"].ToString();
                                    r["SERVICEFEE"] = ro["SERVICEFEE"].ToString();
                                    tblTransDetailsEdit.Rows.Add(r);
                                }
                                if (tblTransDetailsEdit.Rows.Count > 0)
                                {
                                    ViewState["FEEDETAILS"] = tblTransDetailsEdit;
                                }
                                else
                                {
                                    ViewState["FEEDETAILS"] = null;
                                }
                                btnSearch_Click(null, EventArgs.Empty);
                            }
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                    break;
            }
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    ddlCCYID.Enabled = false;
                    txtFeeName.Enabled = false;
                    btback.Visible = true;
                    btsave.Enabled = false;
                    if (ViewState["FEEDETAILS"] != null)
                    {
                        gvFeeInterDetails.Columns[gvFeeInterDetails.Rows[0].Cells.Count - 1].Visible = false;
                    }
                    ddlBank.Enabled = true;
                    pnDetailsFee.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    ddlCCYID.Enabled = true;
                    txtFeeName.Enabled = true;
                    txtAmount.Enabled = !cbIsLadder.Checked;
                    cbIsLadder.Enabled = true;
                    txtFrom.Enabled = true;
                    txtTo.Enabled = true;
                    txtMin.Enabled = true;
                    txtmax.Enabled = true;
                    txtRate.Enabled = true;
                    btback.Visible = true;
                    btsave.Enabled = true;
                    ddlDebitRegion.Enabled = true;
                    ddlCreditRegion.Enabled = true;
                    ddlFeeside.Enabled = true;
                    ddlBank.Enabled = true;
                    break;
            }
            #endregion
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvFeeInterDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            string debitregion = String.Empty;
            string creditregion = String.Empty;
            gvFeeInterDetails.PageIndex = e.NewPageIndex;
            DataTable tblTransDetails = (DataTable)ViewState["FEEDETAILS"];
            DataView dv = new DataView(tblTransDetails);
            if (tblTransDetails != null)
            {
                if (string.IsNullOrEmpty(ddlDebitRegion.SelectedValue))
                {
                    foreach (ListItem item in ddlDebitRegion.Items)
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                            debitregion += "'" + item.Value + "' ,";
                    }
                    if (!string.IsNullOrEmpty(debitregion))
                        debitregion = debitregion.Substring(0, debitregion.Length - 1);
                }
                else
                {
                    debitregion = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlDebitRegion.SelectedValue);
                }

                if (string.IsNullOrEmpty(ddlCreditRegion.SelectedValue))
                {
                    foreach (ListItem item in ddlCreditRegion.Items)
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                            creditregion += "'" + item.Value + "' ,";
                    }
                    if (!string.IsNullOrEmpty(creditregion))
                        creditregion = creditregion.Substring(0, creditregion.Length - 1);
                }
                else
                {
                    creditregion = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCreditRegion.SelectedValue);
                }
                dv.RowFilter = "DEBITREGION IN (" + debitregion + ") AND CREDITREGION IN (" + creditregion + ") AND FEESIDE = '" + SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlFeeside.SelectedValue) + "'";
            }
            gvFeeInterDetails.DataSource = dv;
            gvFeeInterDetails.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            bool isBillerFee = false;
            bool isregionfee = true;

            string fixamount = "0";

            if (cbIsLadder.Checked == false)
            {
                if (ViewState["FEEDETAILS"] == null)
                {
                    lblError.Text = Resources.labels.banphaisetphi;
                    return;
                }
            }
            else
            {
                if (ViewState["FEEDETAILS"] == null)
                {
                    ErrorCodeModel EM = new ErrorCodeModel();
                    EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.NOTSETUPLADDERFEE), System.Globalization.CultureInfo.CurrentCulture.ToString());
                    lblError.Text = EM.ErrorDesc;
                    return;
                }
            }

            if (allowNegativeFee)
            {
                int n;
                if (Int32.TryParse(txtAmount.Text, out n) && int.Parse(txtAmount.Text) < 0)
                {
                    lblError.Text = Resources.labels.tienphikhongrong;
                    return;
                }
            }
            string feeid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(lbIDGen.Text.Trim());
            string feename = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFeeName.Text.Trim());
            string feetype = "INTERBANK";
            bool isladder = cbIsLadder.Checked;
            string chargelater = SmartPortal.Constant.IPC.NEW;
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string useraction = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString());
            string dateaction = SmartPortal.Common.Utilities.Utility.KillSqlInjection(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            DataTable tableFeeDetails = new DataTable();
            if (ViewState["FEEDETAILS"] != null)
            {
                tableFeeDetails = (DataTable)ViewState["FEEDETAILS"];
                DataColumnCollection columns = tableFeeDetails.Columns;
                if (columns.Contains("FkID"))
                {
                    tableFeeDetails.Columns.Remove("FkID");
                }
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:

                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;

                    new SmartPortal.SEMS.InterBank().InsertFee(feeid, feename, feetype, fixamount, isladder, isregionfee, chargelater, ccyid, useraction, dateaction, Session["branch"].ToString(), isBillerFee, tableFeeDetails, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.themphigiaodichthanhcong;
                        pnFee.Enabled = false;
                        pnDetailsFee.Enabled = false;
                        if (ViewState["FEEDETAILS"] != null)
                        {
                            gvFeeInterDetails.Columns[gvFeeInterDetails.Rows[0].Cells.Count - 1].Visible = false;
                        }
                        btsave.Enabled = false;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:

                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;

                    new SmartPortal.SEMS.InterBank().UpdateFee(vFeeID, feename, feetype, fixamount, isladder, isregionfee, chargelater, ccyid, useraction, dateaction, Session["branch"].ToString(), isBillerFee, SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBank.SelectedValue), tableFeeDetails, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.suaphigiaodichthanhcong;
                        pnFee.Enabled = false;
                        pnDetailsFee.Enabled = false;
                        if (ViewState["FEEDETAILS"] != null)
                        {
                            gvFeeInterDetails.Columns[gvFeeInterDetails.Rows[0].Cells.Count - 1].Visible = false;
                        }
                        btsave.Enabled = false;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvFeeInterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblFeeId, lblfrom, lblto, lblmin, lblmax, lblRate, lblFkID, lblfixedfee, lbldebitregion, lblcreditregion, lblfeeside, lblBankName, lblServiceFee;
            LinkButton lbDelete;

            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");

                lblFeeId = (Label)e.Row.FindControl("lblFeeId");
                lblfrom = (Label)e.Row.FindControl("lblfrom");
                lblto = (Label)e.Row.FindControl("lblto");
                lblmin = (Label)e.Row.FindControl("lblmin");
                lblmax = (Label)e.Row.FindControl("lblmax");
                lblRate = (Label)e.Row.FindControl("lblRate");
                lblFkID = (Label)e.Row.FindControl("lblFkID");
                lblfixedfee = (Label)e.Row.FindControl("lblfixedfee");
                lbldebitregion = (Label)e.Row.FindControl("lbldebitregion");
                lblcreditregion = (Label)e.Row.FindControl("lblcreditregion");
                lblfeeside = (Label)e.Row.FindControl("lblfeeside");
                lblBankName = (Label)e.Row.FindControl("lblBankName");
                lblServiceFee = (Label)e.Row.FindControl("lblServiceFee");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lblFeeId.Text = drv["FEEID"].ToString();
                lblfrom.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["FROMLIMIT"].ToString(), ddlCCYID.SelectedValue);
                lblto.Text = double.Parse(drv["TOLIMIT"].ToString()) == -1 ? Resources.labels.unlimit : SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TOLIMIT"].ToString(), ddlCCYID.SelectedValue);
                lblmin.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["MINAMOUNT"].ToString(), ddlCCYID.SelectedValue);
                lblmax.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["MAXAMOUNT"].ToString(), ddlCCYID.SelectedValue);
                lblfixedfee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["FIXAMOUNT"].ToString(), ddlCCYID.SelectedValue);
                try
                {

                    lblFkID.Text = drv["FkID"].ToString();
                }
                catch (Exception exception)
                {
                    lblFkID.Text = "";
                }
                lblRate.Text = drv["RATE"].ToString();
                lbldebitregion.Text = string.IsNullOrEmpty(drv["DEBITREGION"].ToString()) ? "" : ddlRegion.Items.FindByValue(drv["DEBITREGION"].ToString()).Text;
                lblcreditregion.Text = string.IsNullOrEmpty(drv["CREDITREGION"].ToString()) ? "" : ddlRegion.Items.FindByValue(drv["CREDITREGION"].ToString()).Text;
                lblfeeside.Text = string.IsNullOrEmpty(drv["FEESIDE"].ToString()) ? "" : ddlFeeside.Items.FindByValue(drv["FEESIDE"].ToString()).Text;
                try
                {
                    lblBankName.Text = string.IsNullOrEmpty(drv["BANKID"].ToString()) ? Resources.labels.allforbank : ddlBank.Items.FindByValue(drv["BANKID"].ToString()).Text;
                }
                catch (Exception exception)
                {
                    lblBankName.Text = string.Empty;
                }
                lblServiceFee.Text = drv["SERVICEFEE"].ToString();

                if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                {
                    lbDelete.OnClientClick = string.Empty;
                    cbxSelect.Enabled = false;
                }
                if (cbxSelect.Enabled)
                {
                    size++;
                }
                hdCounter.Value = "0";
                hdPageSize.Value = size.ToString();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            DataTable tblTransDetails = new DataTable();
            try
            {
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtServiceFee.Text, true) < 0 && allowNegativeFee &&
                    !string.IsNullOrEmpty(txtServiceFee.Text))
                {
                    lblError.Text = Resources.labels.invalidservicefee;
                    return;
                }
            }
            catch (Exception exception)
            {
                lblError.Text = Resources.labels.invalidservicefee;
                return;
            }

            if (cbIsLadder.Checked)
            {
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtFrom.Text, true) < 0)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (!cbToLimit.Checked && SmartPortal.Common.Utilities.Utility.isDouble(txtTo.Text, true) <= 0)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (!cbToLimit.Checked && SmartPortal.Common.Utilities.Utility.isDouble(txtTo.Text, true) <=
                         SmartPortal.Common.Utilities.Utility.isDouble(txtFrom.Text, true))
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (SmartPortal.Common.Utilities.Utility.isDouble(txtMin.Text, true) < 0 && allowNegativeFee)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (SmartPortal.Common.Utilities.Utility.isDouble(txtmax.Text, true) <= 0 && allowNegativeFee)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (SmartPortal.Common.Utilities.Utility.isDouble(txtmax.Text, true) <
                         SmartPortal.Common.Utilities.Utility.isDouble(txtMin.Text, true))
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }

                try
                {
                    if (SmartPortal.Common.Utilities.Utility.isDouble(txtRate.Text, true) < 0 && allowNegativeFee)
                    {
                        lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                        return;
                    }
                }
                catch (Exception exception)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("DEBITREGION", typeof(string));
            dt.Columns.Add("CREDITREGION", typeof(string));
            try
            {
                if (string.IsNullOrEmpty(ddlDebitRegion.SelectedValue))
                {
                    if (string.IsNullOrEmpty(ddlCreditRegion.SelectedValue))
                    {
                        foreach (ListItem item in ddlDebitRegion.Items)
                        {
                            foreach (ListItem item2 in ddlCreditRegion.Items)
                            {
                                if (!string.IsNullOrEmpty(item.Value) && !string.IsNullOrEmpty(item2.Value))
                                    dt.Rows.Add(new object[] { item.Value, item2.Value });
                            }
                        }
                    }
                    else
                    {
                        foreach (ListItem item in ddlDebitRegion.Items)
                        {
                            if (!string.IsNullOrEmpty(item.Value))
                                dt.Rows.Add(new object[] { item.Value, ddlCreditRegion.SelectedValue });
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(ddlCreditRegion.SelectedValue))
                    {
                        foreach (ListItem item in ddlCreditRegion.Items)
                        {
                            if (!string.IsNullOrEmpty(item.Value))
                                dt.Rows.Add(new object[] { ddlDebitRegion.SelectedValue, item.Value });
                        }
                    }
                    else
                    {
                        dt.Rows.Add(new object[] { ddlDebitRegion.SelectedValue, ddlCreditRegion.SelectedValue });
                    }
                }
            }
            catch (Exception exception)
            {
                if (ddlDebitRegion.Items.Count == 0)
                {
                    lblError.Text = Resources.labels.debitanotfound;
                    return;
                }
                if (ddlCreditRegion.Items.Count == 0)
                {
                    lblError.Text = Resources.labels.creditnotfound;
                    return;
                }
            }

            if (dt.Rows.Count > 0)
            {

                // tạo table tạm 
                DataTable tblTempt = new DataTable();
                DataColumn FeeID = new DataColumn("FEEID");
                DataColumn From = new DataColumn("FROMLIMIT");
                From.DataType = typeof(decimal);
                DataColumn To = new DataColumn("TOLIMIT");
                DataColumn Min = new DataColumn("MINAMOUNT");
                DataColumn Max = new DataColumn("MAXAMOUNT");
                DataColumn Rate = new DataColumn("RATE");
                DataColumn FixAmount = new DataColumn("FIXAMOUNT");
                FixAmount.DataType = typeof(decimal);
                DataColumn Ladder = new DataColumn("ISLADDER");
                DataColumn DebitRegion = new DataColumn("DEBITREGION");
                DataColumn CreditRegion = new DataColumn("CREDITREGION");
                DataColumn FeeSide = new DataColumn("FEESIDE");
                DataColumn BankID = new DataColumn("BANKID");
                DataColumn ServiceFee = new DataColumn("SERVICEFEE");
                DataColumn FkID = new DataColumn("FkID");
                tblTempt.Columns.AddRange(new DataColumn[]
                {
                FeeID, From, To, Min, Max, Rate, FkID, FixAmount, Ladder, DebitRegion, CreditRegion, FeeSide, BankID,
                ServiceFee
                });

                if (ViewState["FEEDETAILS"] == null)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        DataRow r = tblTempt.NewRow();
                        if (ACTION == IPC.ACTIONPAGE.EDIT)
                        {
                            r["FEEID"] = vFeeID;
                        }
                        else
                        {
                            r["FEEID"] = lbIDGen.Text;
                        }

                        r["TOLIMIT"] = cbToLimit.Checked
                            ? "-1"
                            : SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTo.Text.Trim(),
                                ddlCCYID.SelectedValue);
                        r["FROMLIMIT"] =
                            SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtFrom.Text.Trim(),
                                ddlCCYID.SelectedValue);
                        r["MINAMOUNT"] =
                            SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMin.Text, ddlCCYID.SelectedValue);
                        r["MAXAMOUNT"] =
                            SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtmax.Text.Trim(),
                                ddlCCYID.SelectedValue);
                        r["RATE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRate.Text.Trim());
                        r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                        r["FIXAMOUNT"] = txtAmount.Visible
                            ? SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(),
                                ddlCCYID.SelectedValue)
                            : "0";
                        r["ISLADDER"] = cbIsLadder.Checked.ToString();
                        r["DEBITREGION"] =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(dataRow["DEBITREGION"].ToString());
                        r["CREDITREGION"] =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(dataRow["CREDITREGION"].ToString());
                        r["FEESIDE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlFeeside.SelectedValue);
                        r["BANKID"] =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBank.SelectedValue.ToString());
                        r["SERVICEFEE"] = ddlFeeside.SelectedValue.Contains("SENDER")
                            ? SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtServiceFee.Text.Trim())
                            : "0";

                        tblTempt.Rows.Add(r);
                    }

                    ViewState["FEEDETAILS"] = tblTempt;
                }
                else
                {
                    tblTransDetails = (DataTable)ViewState["FEEDETAILS"];
                    bool validate = true;
                    reFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(txtFrom.Text.Trim(), true);
                    reToLimit = cbToLimit.Checked
                        ? -1
                        : SmartPortal.Common.Utilities.Utility.isDouble(txtTo.Text.Trim(), true);
                    IsLadder = cbIsLadder.Checked;
                    reFeeSide = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlFeeside.SelectedValue);
                    reBankID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBank.SelectedValue.ToString());
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        reDebitRegion =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(dataRow["DEBITREGION"].ToString());
                        reCreditRegion =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(dataRow["CREDITREGION"].ToString());

                        if (!validate) break;
                        foreach (DataRow row in tblTransDetails.Rows)
                        {
                            DataRow r = tblTempt.NewRow();
                            roToLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["TOLIMIT"].ToString(), true);
                            roFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["FROMLIMIT"].ToString(), true);
                            roIsLadder = bool.Parse(row["ISLADDER"].ToString());
                            roDebitRegion = row["DEBITREGION"].ToString();
                            roCreditRegion = row["CREDITREGION"].ToString();
                            roFeeSide = row["FEESIDE"].ToString();
                            roBankID = row["BANKID"].ToString();

                            if (ValidateFee())
                            {
                                if (ACTION == IPC.ACTIONPAGE.EDIT)
                                {
                                    r["FEEID"] = vFeeID;
                                }
                                else
                                {
                                    r["FEEID"] = lbIDGen.Text;
                                }

                                r["TOLIMIT"] = cbToLimit.Checked
                                    ? "-1"
                                    : SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTo.Text.Trim(),
                                        ddlCCYID.SelectedValue);
                                r["FROMLIMIT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtFrom.Text.Trim(),
                                    ddlCCYID.SelectedValue.ToString());
                                r["MINAMOUNT"] =
                                    SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMin.Text.Trim(),
                                        ddlCCYID.SelectedValue.ToString());
                                r["MAXAMOUNT"] =
                                    SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtmax.Text.Trim(),
                                        ddlCCYID.SelectedValue.ToString());
                                r["RATE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRate.Text.Trim());
                                r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                                lblError.Text = string.Empty;
                                r["FIXAMOUNT"] = txtAmount.Visible
                                    ? SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(),
                                        ddlCCYID.SelectedValue)
                                    : "0";
                                r["ISLADDER"] = cbIsLadder.Checked.ToString();
                                r["DEBITREGION"] = reDebitRegion;
                                r["CREDITREGION"] = reCreditRegion;
                                r["FEESIDE"] = ddlFeeside.SelectedValue;
                                r["BANKID"] =
                                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBank.SelectedValue.ToString());
                                r["SERVICEFEE"] = ddlFeeside.SelectedValue.Contains("SENDER")
                                    ? SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtServiceFee.Text.Trim())
                                    : "0";
                            }
                            else
                            {
                                validate = false;
                                lblError.Text = Resources.labels.datontaifee;
                                break;
                            }

                            tblTempt.Rows.Add(r);
                            break;
                        }
                    }

                    if (validate)
                    {
                        tblTransDetails.Merge(tblTempt);
                        ViewState["FEEDETAILS"] = tblTransDetails;
                    }
                    else
                    {
                        return;
                    }
                }

                txtAmount.Text = string.Empty;
                txtFrom.Text = "0";
                cbToLimit.Checked = false;
                txtTo.Enabled = true;
                txtTo.Text = "0";
                txtMin.Text = "0";
                txtmax.Text = "0";
                txtRate.Text = "0";
                txtServiceFee.Text = "0";
                btnSearch_Click(null, EventArgs.Empty);
            }
            else
            {
                if (ddlDebitRegion.Items.Count == 0)
                {
                    lblError.Text = Resources.labels.debitanotfound;
                    return;
                }
                if (ddlCreditRegion.Items.Count == 0)
                {
                    lblError.Text = Resources.labels.creditnotfound;
                    return;
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected bool ValidateFee()
    {
        bool isLadderRight = (roToLimit <= reFromLimit && roToLimit != -1) || (roFromLimit >= reToLimit && reToLimit != -1);

        if (roDebitRegion.Equals(reDebitRegion) && roCreditRegion.Equals(reCreditRegion)) // giong du lieu region da co
        {
            if (reDebitRegion.Equals(reCreditRegion)) //cung region
            {
                if (reFeeSide.Equals(roFeeSide)) //cung fee side
                {
                    if (reBankID.Equals(roBankID)) //cung bank
                    {
                        if (roIsLadder && IsLadder)
                        {
                            return isLadderRight;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else //khac region
            {
                if (reFeeSide.Equals(roFeeSide)) //cung fee side
                {
                    if (reBankID.Equals(roBankID)) //cung bank
                    {
                        if (roIsLadder && IsLadder)
                        {
                            return isLadderRight;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
        else //khac du lieu da co
        {
            return true;
        }
    }
    protected void gvFeeInterDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable tblTransDetailsDel = (DataTable)ViewState["FEEDETAILS"];
        Label lblFkID = (Label)gvFeeInterDetails.Rows[e.RowIndex].FindControl("lblFkID");
        DataRow[] dr = tblTransDetailsDel.Select("FkID='" + lblFkID.Text.Trim() + "'");
        tblTransDetailsDel.Rows.Remove(dr[0]);
        lblError.Text = Resources.labels.xoaphigiaodichthanhcong;
        if (tblTransDetailsDel.Rows.Count == 0)
        {
            ViewState["FEEDETAILS"] = null;
            pnGV.Visible = false;
        }
        else
        {
            ViewState["FEEDETAILS"] = tblTransDetailsDel;
            pnGV.Visible = true;
        }
        btnSearch_Click(null, EventArgs.Empty);
    }
    protected void cbIsLadder_CheckedChanged(object sender, EventArgs e)
    {
        if (cbIsLadder.Checked)
        {
            txtAmount.Text = "";
            txtAmount.Enabled = false;
            tbLadderFee.Visible = true;
            if (string.IsNullOrEmpty(lbIDGen.Text))
            {
                lbIDGen.Text = "FEE" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
            }
        }
        else
        {
            txtAmount.Text = "";
            txtAmount.Enabled = true;
            tbLadderFee.Visible = false;
        }
        txtFrom.Text = "0";
        txtTo.Text = "0";
        txtMin.Text = "0";
        txtmax.Text = "0";
        txtRate.Text = "0";
        txtServiceFee.Text = "0";
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        pnFee.Enabled = true;
        pnDetailsFee.Enabled = true;
        ViewState["FEEDETAILS"] = null;
        tbLadderFee.Visible = false;
        txtFeeName.Text = string.Empty;
        ddlCCYID.SelectedIndex = 0;
        lbIDGen.Text = "FEE" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
        txtAmount.Text = string.Empty;
        txtAmount.Enabled = true;
        cbIsLadder.Checked = false;
        txtFrom.Text = "0";
        txtTo.Text = "0";
        txtMin.Text = "0";
        txtmax.Text = "0";
        txtRate.Text = "0";
        txtServiceFee.Text = "0";
        LoadRegion();
        ddlFeeside.SelectedIndex = 0;
        ddlBank.SelectedIndex = 0;
        pnGV.Visible = false;
        btsave.Enabled = true;
    }
    protected void cbToLimit_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbToLimit.Checked)
            {
                txtTo.Text = string.Empty;
                txtTo.Enabled = false;
            }
            else
            {
                txtTo.Text = "0";
                txtTo.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ddlBank_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadRegion();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    ViewState["FEEDETAILS"] = null;
                    btnSearch_Click(null, EventArgs.Empty);
                    break;
                default:
                    DataSet dsfee = new SmartPortal.SEMS.InterBank().DetailsFee(vFeeID, SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBank.SelectedValue.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsfee.Tables.Count != 0)
                    {
                        ProDeTable = dsfee.Tables[0];
                        if (ProDeTable.Rows.Count != 0)
                        {
                            if ((bool)(ProDeTable.Rows[0]["ISLADDER"]) == false && (bool)(ProDeTable.Rows[0]["ISBILLPAYMENTFEE"]) == false && (bool)(ProDeTable.Rows[0]["ISREGIONFEE"]) == false)
                            {
                                cbIsLadder.Checked = false;
                                tbLadderFee.Visible = false;
                                txtAmount.Enabled = true;
                            }
                            else
                            {
                                tbLadderFee.Visible = (bool)(ProDeTable.Rows[0]["ISLADDER"]);
                                cbIsLadder.Checked = (bool)(ProDeTable.Rows[0]["ISLADDER"]);
                                txtAmount.Enabled = false;
                                txtAmount.Text = "";
                                DataTable tblTransDetailsEdit = new DataTable();
                                DataColumn FeeID = new DataColumn("FEEID");
                                DataColumn From = new DataColumn("FROMLIMIT");
                                From.DataType = typeof(decimal);
                                DataColumn To = new DataColumn("TOLIMIT");
                                DataColumn Min = new DataColumn("MINAMOUNT");
                                DataColumn Max = new DataColumn("MAXAMOUNT");
                                DataColumn Rate = new DataColumn("RATE");
                                DataColumn FkID = new DataColumn("FkID");
                                DataColumn FixAmount = new DataColumn("FIXAMOUNT");
                                FixAmount.DataType = typeof(decimal);
                                DataColumn IsLader = new DataColumn("ISLADDER");
                                DataColumn DebitRegion = new DataColumn("DEBITREGION");
                                DataColumn CreditRegion = new DataColumn("CREDITREGION");
                                DataColumn FeeSide = new DataColumn("FEESIDE");
                                DataColumn BankID = new DataColumn("BANKID");
                                DataColumn ServiceFee = new DataColumn("SERVICEFEE");

                                tblTransDetailsEdit.Columns.AddRange(new DataColumn[] { FeeID, From, To, Min, Max, Rate, FkID, FixAmount, IsLader, DebitRegion, CreditRegion, FeeSide, BankID, ServiceFee });
                                foreach (DataRow ro in ProDeTable.Rows)
                                {
                                    DataRow r = tblTransDetailsEdit.NewRow();
                                    if (ro["FROMLIMIT"] is DBNull && ro["TOLIMIT"] is DBNull && ro["MINAMOUNT"] is DBNull && ro["MAXAMOUNT"] is DBNull && ro["FDFIXAMOUNT"] is DBNull && ro["RATE"] is DBNull)
                                    {
                                        break;
                                    }

                                    r["FEEID"] = vFeeID;
                                    r["TOLIMIT"] = ro["TOLIMIT"].ToString();
                                    r["FROMLIMIT"] = ro["FROMLIMIT"].ToString();
                                    r["MINAMOUNT"] = ro["MINAMOUNT"].ToString();
                                    r["MAXAMOUNT"] = ro["MAXAMOUNT"].ToString();
                                    r["RATE"] = ro["RATE"].ToString();
                                    r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                                    r["FIXAMOUNT"] = ro["FDFIXAMOUNT"].ToString();
                                    r["ISLADDER"] = (bool)(string.IsNullOrEmpty(ro["FDISLADDER"].ToString()) ? cbIsLadder.Checked : ro["FDISLADDER"]);
                                    r["DEBITREGION"] = ro["DEBITREGION"].ToString();
                                    r["CREDITREGION"] = ro["CREDITREGION"].ToString();
                                    r["FEESIDE"] = ro["FEESIDE"].ToString();
                                    r["BANKID"] = ro["BANKID"].ToString();
                                    r["SERVICEFEE"] = ro["SERVICEFEE"].ToString();
                                    tblTransDetailsEdit.Rows.Add(r);
                                }
                                if (tblTransDetailsEdit.Rows.Count > 0)
                                {
                                    ViewState["FEEDETAILS"] = tblTransDetailsEdit;
                                }
                                else
                                {
                                    ViewState["FEEDETAILS"] = null;
                                }
                                btnSearch_Click(null, EventArgs.Empty);
                            }
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                    break;
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string debitregion = String.Empty;
            string creditregion = String.Empty;
            gvFeeInterDetails.PageIndex = 0;
            DataTable tblTransDetails = (DataTable)ViewState["FEEDETAILS"];
            if (tblTransDetails != null)
                tblTransDetails.DefaultView.Sort = "[" + tblTransDetails.Columns["FROMLIMIT"].ColumnName + "] , [" + tblTransDetails.Columns["FIXAMOUNT"].ColumnName + "] , [" + tblTransDetails.Columns["DEBITREGION"].ColumnName + "] ASC";
            DataView dv = new DataView(tblTransDetails);
            if (tblTransDetails != null)
            {
                if (string.IsNullOrEmpty(ddlDebitRegion.SelectedValue))
                {
                    foreach (ListItem item in ddlDebitRegion.Items)
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                            debitregion += "'" + item.Value + "' ,";
                    }
                    if (!string.IsNullOrEmpty(debitregion))
                        debitregion = debitregion.Substring(0, debitregion.Length - 1);
                }
                else
                {
                    debitregion = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlDebitRegion.SelectedValue);
                }

                if (string.IsNullOrEmpty(ddlCreditRegion.SelectedValue))
                {
                    foreach (ListItem item in ddlCreditRegion.Items)
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                            creditregion += "'" + item.Value + "' ,";
                    }
                    if (!string.IsNullOrEmpty(creditregion))
                        creditregion = creditregion.Substring(0, creditregion.Length - 1);
                }
                else
                {
                    creditregion = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCreditRegion.SelectedValue);
                }
                dv.RowFilter = "DEBITREGION IN (" + debitregion + ") AND CREDITREGION IN (" + creditregion + ") AND FEESIDE = '" + SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlFeeside.SelectedValue) + "'";
            }
            gvFeeInterDetails.DataSource = dv;
            gvFeeInterDetails.DataBind();
            if (tblTransDetails != null && tblTransDetails.Rows.Count > 0)
            {
                pnGV.Visible = true;
            }
            else
            {
                pnGV.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            try
            {
                CheckBox cbxDelete;
                DataTable tblTransDetailsDel = (DataTable)ViewState["FEEDETAILS"];
                foreach (GridViewRow gvr in gvFeeInterDetails.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        Label lblFkID = (Label)gvr.Cells[1].FindControl("lblFkID");
                        DataRow[] dr = tblTransDetailsDel.Select("FkID='" + lblFkID.Text.Trim() + "'");
                        tblTransDetailsDel.Rows.Remove(dr[0]);
                    }
                }
                if (tblTransDetailsDel.Rows.Count == 0)
                {
                    ViewState["FEEDETAILS"] = null;
                }
                else
                {
                    ViewState["FEEDETAILS"] = tblTransDetailsDel;
                }
                btnSearch_Click(null, EventArgs.Empty);
                lblError.Text = Resources.labels.xoaphigiaodichthanhcong;
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
            }
        }
    }
    protected void LoadRegion()
    {
        try
        {
            ViewState["FILTER_REGION"] = null;
            DataTable dt = new SmartPortal.SEMS.InterBank().GetFilterRegion();
            ViewState["FILTER_REGION"] = dt;

            DataView dvDebit = new DataView(dt);
            dvDebit.RowFilter = "TYPE = 'D'";
            ddlDebitType.DataSource = dvDebit;
            ddlDebitType.DataTextField = "NAME";
            ddlDebitType.DataValueField = "ID";
            ddlDebitType.DataBind();

            ddlDebitType.SelectedIndex = 0;
            ddlDebitType_OnSelectedIndexChanged(null, EventArgs.Empty);

            DataView dvCredit = new DataView(dt);
            if (string.IsNullOrEmpty(ddlBank.SelectedValue.ToString()))
            {
                dvCredit.RowFilter = "TYPE = 'C'";
            }
            else
            {
                dvCredit.RowFilter = "TYPE = 'C' AND (BANKID = '" + ddlBank.SelectedValue.ToString() + "' OR  BANKID = '')";
            }
            ddlCreditType.DataSource = dvCredit;
            ddlCreditType.DataTextField = "NAME";
            ddlCreditType.DataValueField = "ID";
            ddlCreditType.DataBind();

            ddlCreditType.SelectedIndex = 0;
            ddlCreditType_OnSelectedIndexChanged(null, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void ddlDebitType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDebitRegion.Items.Clear();
            DataTable dt = (DataTable)ViewState["FILTER_REGION"];
            DataRow[] dr = dt.Select("ID='" + ddlDebitType.SelectedValue.ToString() + "'");

            DataTable list = DataAccess.FillDataTableSQL(dr[0]["VALUE"].ToString());
            if (list.Rows.Count > 0)
            {
                ddlDebitRegion.DataSource = list;
                ddlDebitRegion.DataTextField = "RegionName";
                ddlDebitRegion.DataValueField = "RegionID";
                ddlDebitRegion.DataBind();

                ddlDebitRegion.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void ddlCreditType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCreditRegion.Items.Clear();
            DataTable dt = (DataTable)ViewState["FILTER_REGION"];
            DataRow[] dr = dt.Select("ID='" + ddlCreditType.SelectedValue.ToString() + "'");
            DataTable list = DataAccess.FillDataTableSQL(dr[0]["VALUE"].ToString());
            if (list.Rows.Count > 0)
            {
                DataTable distinctValues = new DataTable();
                if (string.IsNullOrEmpty(ddlBank.SelectedValue))
                {
                    DataView view = new DataView(list);
                    distinctValues = view.ToTable(true, "RegionID", "RegionName");
                }
                else
                {
                    DataRow[] drBank = list.Select("BANKID = '" + ddlBank.SelectedValue.ToString() + "'");
                    DataTable dtRegion = new DataTable();
                    if (drBank.Length > 0)
                    {
                        dtRegion = drBank.CopyToDataTable();

                        DataView view = new DataView(dtRegion);
                        distinctValues = view.ToTable(true, "RegionID", "RegionName");
                    }
                }

                if (distinctValues.Rows.Count > 0)
                {
                    ddlCreditRegion.DataSource = distinctValues;
                    ddlCreditRegion.DataTextField = "RegionName";
                    ddlCreditRegion.DataValueField = "RegionID";
                    ddlCreditRegion.DataBind();

                    ddlCreditRegion.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnFeeSide_OnClick(object sender, EventArgs e)
    {
        if (ddlFeeside.SelectedValue.Contains("SENDER"))
        {
            txtServiceFee.Enabled = true;
        }
        else
        {
            txtServiceFee.Enabled = false;
        }
        tbLadderFee.Visible = false;
        txtAmount.Text = string.Empty;
        txtAmount.Enabled = true;
        cbIsLadder.Checked = false;
        txtFrom.Text = "0";
        txtTo.Text = "0";
        txtMin.Text = "0";
        txtmax.Text = "0";
        txtRate.Text = "0";
        txtServiceFee.Text = "0";
        LoadRegion();
        btnSearch_Click(null, EventArgs.Empty);
    }
}

