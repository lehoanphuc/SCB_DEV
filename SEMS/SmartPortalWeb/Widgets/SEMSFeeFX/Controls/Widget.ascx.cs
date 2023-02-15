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
using SmartPortal.Model;
using SmartPortal.SEMS;

public partial class Widgets_SEMSFeeFX_Controls_Widget : WidgetBase
{
    public static bool isAscend = false;
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    public static string APPTRANID;
    public static int Fuid = 1;

    //check validate flag false cho nhap so am, true khong nhap so am
    public bool allowNegativeFee = false;
    DataTable ProDeTable = new DataTable();
    double reFromLimit, reToLimit, roToLimit, roFromLimit;
    string reBiller, roBiller, frmCCYID, toCCYID, refrmCCYID, retoCCYID;
    bool IsLadder, roIsLadder, sameCCYID;
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
            txtMin.Attributes.Add("onkeypress", "executeComma('" + txtMin.ClientID + "')");
            txtmax.Attributes.Add("onkeypress", "executeComma('" + txtmax.ClientID + "')");
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
                txtRate.Attributes.Add("onkeyup", "executeComma('" + txtRate.ClientID + "')");
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

                txtMin.Attributes.Add("onkeyup", "executeComma('" + txtMin.ClientID + "')");
                txtmax.Attributes.Add("onkeyup", "executeComma('" + txtmax.ClientID + "')");
                txtRate.Attributes.Add("onkeyup", "executeComma('" + txtRate.ClientID + "')");
            }


            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                ViewState["FEEDETAILS"] = null;
                pnWarning.Visible = false;

                ddlCCYIDFrm.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlCCYIDFrm.DataTextField = "CCYID";
                ddlCCYIDFrm.DataValueField = "CCYID";
                ddlCCYIDFrm.DataBind();


                ddlCCYIDTo.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlCCYIDTo.DataTextField = "CCYID";
                ddlCCYIDTo.DataValueField = "CCYID";
                ddlCCYIDTo.DataBind();


                object[] searchObject = new object[] { };
                ddlBiller.DataSource = new SmartPortal.SEMS.Common().common("GET_BILLERFEE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                ddlBiller.DataTextField = "BillerName";
                ddlBiller.DataValueField = "BillerID";
                ddlBiller.DataBind();


                if (!ACTION.Equals(IPC.ACTIONPAGE.ADD))
                {
                    ddlCCYIDFrm.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
                    ddlCCYIDTo.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
                }

                BindData();
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
            trBiller.Visible = false;
            btnSaveDetailsrg.Visible = true;

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    vFeeID = "FEE" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
                    pnFee.Visible = true;
                    tbLadderFee.Visible = false;
                    btnClear.Enabled = true;
                    break;
                default:
                    btnClear.Enabled = false;
                    #region Lấy thông tin san pham
                    tbLadderFee.Visible = false;
                    vFeeID = GetParamsPage(IPC.ID)[0].Trim();

                    DataSet dsfee = new DataSet();
                    dsfee = new SmartPortal.SEMS.Fee().DetailsFee(vFeeID, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsfee.Tables.Count != 0)
                    {
                        ProDeTable = dsfee.Tables[0];
                        if (ProDeTable.Rows.Count != 0)
                        {
                            //bind data to panel
                            txtFeeName.Text = ProDeTable.Rows[0]["FEENAME"].ToString();


                            txtAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ProDeTable.Rows[0]["FIXAMOUNT"].ToString(), ProDeTable.Rows[0]["CCYID"].ToString().Trim());


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
                            DataColumn IsLader = new DataColumn("ISLADDER");
                            DataColumn BILLERID = new DataColumn("BILLERID");
                            DataColumn FROMCCYID = new DataColumn("FROMCCYID");
                            DataColumn TOCCYID = new DataColumn("TOCCYID");
                            tblTransDetailsEdit.Columns.AddRange(new DataColumn[] { FeeID, From, To, Min, Max, Rate, FkID, FixAmount, IsLader, BILLERID, FROMCCYID, TOCCYID });

                            foreach (DataRow ro in ProDeTable.Rows)
                            {
                                DataRow r = tblTransDetailsEdit.NewRow();
                                r["FEEID"] = vFeeID;
                                r["TOLIMIT"] = ro["TOLIMIT"].ToString();
                                r["FROMLIMIT"] = ro["FROMLIMIT"].ToString();
                                r["MINAMOUNT"] = ro["MINAMOUNT"].ToString();
                                r["MAXAMOUNT"] = ro["MAXAMOUNT"].ToString();
                                r["RATE"] = ro["RATE"].ToString();
                                r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                                r["FIXAMOUNT"] = ro["FDFIXAMOUNT"].ToString();
                                r["ISLADDER"] = (bool)(string.IsNullOrEmpty(ro["FDISLADDER"].ToString()) ? cbIsLadder.Checked : ro["FDISLADDER"]);
                                r["BILLERID"] = ro["BILLERID"].ToString();
                                r["FROMCCYID"] = ro["FROMCCYID"].ToString();
                                r["TOCCYID"] = ro["TOCCYID"].ToString();
                                tblTransDetailsEdit.Rows.Add(r);
                            }
                            tblTransDetailsEdit.DefaultView.Sort = "[" + tblTransDetailsEdit.Columns["BILLERID"].ColumnName + "], [" + tblTransDetailsEdit.Columns["FROMLIMIT"].ColumnName + "] ASC";
                            ViewState["FEEDETAILS"] = tblTransDetailsEdit;
                            gvAppTransDetailsList.DataSource = tblTransDetailsEdit;//ProDeTable;
                            gvAppTransDetailsList.DataBind();
                            if (tblTransDetailsEdit != null && tblTransDetailsEdit.Rows.Count > 0)
                            {
                                pnGV.Visible = true;
                            }
                            else
                            {
                                pnGV.Visible = false;
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
                    #endregion
                    break;
            }
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    tbLadderFee.Visible = false;
                    ddlBiller.Enabled = false;
                    ddlCCYIDFrm.Enabled = false;
                    ddlCCYIDTo.Enabled = false;
                    txtFeeName.Enabled = false;
                    txtAmount.Enabled = false;
                    cbIsLadder.Enabled = false;
                    txtFrom.Enabled = false;
                    txtTo.Enabled = false;
                    txtMin.Enabled = false;
                    txtmax.Enabled = false;
                    txtRate.Enabled = false;
                    btback.Visible = true;
                    btnSaveDetailsrg.Visible = false;
                    btsave.Enabled = false;
                    btnSaveDetails.Enabled = false;
                    btnSaveDetailsrg.Visible = cbIsLadder.Checked ? false : true;
                    btnSaveDetails.Visible = cbIsLadder.Checked ? true : false;
                    if (ViewState["FEEDETAILS"] != null)
                    {
                        gvAppTransDetailsList.Columns[gvAppTransDetailsList.Rows[0].Cells.Count - 1].Visible = false;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    ddlCCYIDFrm.Enabled = true;
                    ddlCCYIDTo.Enabled = true;
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
                    btnSaveDetailsrg.Enabled = true;
                    btnSaveDetails.Enabled = true;
                    btnSaveDetailsrg.Visible = cbIsLadder.Checked ? false : true;
                    btnSaveDetails.Visible = cbIsLadder.Checked ? true : false;
                    break;
            }
            ShowWarning();
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
    protected void ddlFeeType_OnChange(object sender, EventArgs e)
    {

        ViewState["FEEDETAILS"] = null;
        DataTable tblTransDetailsReLoad = (DataTable)ViewState["FEEDETAILS"];
        gvAppTransDetailsList.DataSource = tblTransDetailsReLoad;
        gvAppTransDetailsList.DataBind();
        if (tblTransDetailsReLoad != null && tblTransDetailsReLoad.Rows.Count > 0)
        {
            pnGV.Visible = true;
        }
        else
        {
            pnGV.Visible = false;
        }
    }
    protected void gvAppTransDetailsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAppTransDetailsList.PageIndex = e.NewPageIndex;
            BindData();
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
            bool isBillerFee = trBiller.Visible;

            string fixamount = "0";
            if (ViewState["FEEDETAILS"] == null && cbIsLadder.Checked == true)
            {
                lblError.Text = Resources.labels.banphaisetphi;
                return;
            }

            //if (cbIsLadder.Checked == false && trBiller.Visible == false)
            //{
            //    fixamount = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), ddlCCYIDFrm.SelectedValue.Trim()));
            //}
            //else if (cbIsLadder.Checked == false && trBiller.Visible == true)
            //{

            //}
            //else
            //{
            //    if (ViewState["FEEDETAILS"] == null)
            //    {
            //        ErrorCodeModel EM = new ErrorCodeModel();
            //        EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.NOTSETUPLADDERFEE), System.Globalization.CultureInfo.CurrentCulture.ToString());
            //        lblError.Text = EM.ErrorDesc;
            //        return;
            //    }
            //}
            if (allowNegativeFee)
            {
                int n;
                if (Int32.TryParse(txtAmount.Text, out n) && int.Parse(txtAmount.Text) < 0)
                {
                    lblError.Text = Resources.labels.tienphikhongrong;
                    return;
                }
            }
            string feeid = vFeeID;
            string feename = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFeeName.Text.Trim());
            string feetype = "FX";
            bool isladder = cbIsLadder.Checked;
            string chargelater = SmartPortal.Constant.IPC.NEW;
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYIDFrm.SelectedValue.Trim());
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

                    new SmartPortal.SEMS.Fee().InsertFeeFX(vFeeID, feename, "FX", fixamount, isladder, false, chargelater, "B", useraction, dateaction, Session["branch"].ToString(), isBillerFee, tableFeeDetails, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.themphigiaodichthanhcong;
                        pnFee.Enabled = false;
                        pnDetailsFee.Enabled = false;
                        if (ViewState["FEEDETAILS"] != null)
                        {
                            gvAppTransDetailsList.Columns[gvAppTransDetailsList.Rows[0].Cells.Count - 1].Visible = false;
                        }
                        pnWarning.Enabled = false;
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

                    new SmartPortal.SEMS.Fee().UpdateFeeFX(vFeeID, feename, "FX", fixamount, isladder, false, chargelater, "B", useraction, dateaction, Session["branch"].ToString(), isBillerFee, tableFeeDetails, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.suaphigiaodichthanhcong;
                        pnFee.Enabled = false;
                        pnDetailsFee.Enabled = false;
                        if (ViewState["FEEDETAILS"] != null)
                        {
                            gvAppTransDetailsList.Columns[gvAppTransDetailsList.Rows[0].Cells.Count - 1].Visible = false;
                        }
                        pnWarning.Enabled = false;
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
    protected void gvAppTransDetailsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblFeeId, lblfrom, lblto, lblmin, lblmax, lblRate, lblFkID, lblfixedfee, lblisladder, lblBillerName, lblFromCurrency, lblToCurrency;
            LinkButton lbDelete;

            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                lblFeeId = (Label)e.Row.FindControl("lblFeeId");
                lblfrom = (Label)e.Row.FindControl("lblfrom");
                lblto = (Label)e.Row.FindControl("lblto");
                lblmin = (Label)e.Row.FindControl("lblmin");
                lblmax = (Label)e.Row.FindControl("lblmax");
                lblRate = (Label)e.Row.FindControl("lblRate");
                lblFkID = (Label)e.Row.FindControl("lblFkID");
                lblfixedfee = (Label)e.Row.FindControl("lblfixedfee");
                lblisladder = (Label)e.Row.FindControl("lblisladder");
                lblBillerName = (Label)e.Row.FindControl("lblBillerName");
                lblFromCurrency = (Label)e.Row.FindControl("lblFromCurrency");
                lblToCurrency = (Label)e.Row.FindControl("lblToCurrency");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lblFeeId.Text = drv["FEEID"].ToString();
                lblFromCurrency.Text = drv["FROMCCYID"].ToString();
                lblToCurrency.Text = drv["TOCCYID"].ToString();
                lblfrom.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["FROMLIMIT"].ToString(), ddlCCYIDFrm.SelectedValue);
                lblto.Text = double.Parse(drv["TOLIMIT"].ToString()) == -1 ? Resources.labels.unlimit : SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TOLIMIT"].ToString(), ddlCCYIDTo.SelectedValue);
                lblmin.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["MINAMOUNT"].ToString(), ddlCCYIDFrm.SelectedValue);
                lblmax.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["MAXAMOUNT"].ToString(), ddlCCYIDFrm.SelectedValue);
                lblfixedfee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["FIXAMOUNT"].ToString(), ddlCCYIDFrm.SelectedValue);
                lblFkID.Text = drv["FkID"].ToString();
                lblRate.Text = drv["RATE"].ToString();
                //lblfixedfee.Text = drv["FIXAMOUNT"].ToString();
                if (drv["ISLADDER"].ToString() == "True")
                {
                    lblisladder.Text = "<img src='widgets/SEMSFeeManagement/Images/check.png' style='width: 20px; height: 20px; margin-bottom:1px;'/>";
                }
                if (drv["ISLADDER"].ToString() == "False")
                {
                    lblisladder.Text = "<img src='widgets/SEMSFeeManagement/Images/nocheck.png'style='width: 20px; height: 20px; margin-bottom:1px;'/>";
                }
                lblBillerName.Text = string.IsNullOrEmpty(drv["BILLERID"].ToString()) ? "" : ddlBiller.Items.FindByValue(drv["BILLERID"].ToString()).Text;
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                {
                    lbDelete.OnClientClick = string.Empty;
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ViewState["FEEDETAILS"] != null)
        {
            DataTable tblTransDetails = (DataTable)ViewState["FEEDETAILS"];
            DataTable tblTransSearch = tblTransDetails;
            if (!ddlCCYIDFrm.SelectedValue.Equals("") && !ddlCCYIDTo.SelectedValue.Equals(""))
            {
                tblTransSearch = tblTransDetails.Select("FROMCCYID='" + ddlCCYIDFrm.SelectedValue + "' and TOCCYID='" + ddlCCYIDTo.SelectedValue + "'").Any() ? tblTransDetails.Select("FROMCCYID='" + ddlCCYIDFrm.SelectedValue + "' and TOCCYID='" + ddlCCYIDTo.SelectedValue + "'").CopyToDataTable() : null;
            }
            else if (!ddlCCYIDFrm.SelectedValue.Equals("") && ddlCCYIDTo.SelectedValue.Equals(""))
            {
                tblTransSearch = tblTransDetails.Select("FROMCCYID='" + ddlCCYIDFrm.SelectedValue + "'").Any() ? tblTransDetails.Select("FROMCCYID='" + ddlCCYIDFrm.SelectedValue + "'").CopyToDataTable() : null;
            }
            else if (ddlCCYIDFrm.SelectedValue.Equals("") && !ddlCCYIDTo.SelectedValue.Equals(""))
            {
                tblTransSearch = tblTransDetails.Select("TOCCYID='" + ddlCCYIDTo.SelectedValue + "'").Any() ? tblTransDetails.Select("TOCCYID='" + ddlCCYIDTo.SelectedValue + "'").CopyToDataTable() : null;
            }
            if (tblTransSearch == null)
            {
                gvAppTransDetailsList.Visible = false;
            }
            else
            {
                gvAppTransDetailsList.Visible = true;
                gvAppTransDetailsList.DataSource = tblTransSearch;
                gvAppTransDetailsList.DataBind();
            }

        }

    }
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable tblTransDetails = new DataTable();
            DataTable tblTempt = new DataTable();
            if (ddlCCYIDFrm.SelectedValue.Equals(ddlCCYIDTo.SelectedValue) || ddlCCYIDFrm.SelectedValue.Equals("") || ddlCCYIDTo.SelectedValue.Equals(""))
            {
                lblError.Text = Resources.labels.fromcurrencynotsametocurrency;
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
                else if (!cbToLimit.Checked && SmartPortal.Common.Utilities.Utility.isDouble(txtTo.Text, true) <= SmartPortal.Common.Utilities.Utility.isDouble(txtFrom.Text, true))
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
                else if (SmartPortal.Common.Utilities.Utility.isDouble(txtmax.Text, true) < SmartPortal.Common.Utilities.Utility.isDouble(txtMin.Text, true))
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

            if (ViewState["FEEDETAILS"] == null)
            {
                // tạo table tạm chứa TransDetails 
                DataColumn FeeID = new DataColumn("FEEID");
                DataColumn From = new DataColumn("FROMLIMIT");
                From.DataType = typeof(decimal);
                DataColumn To = new DataColumn("TOLIMIT");
                DataColumn Min = new DataColumn("MINAMOUNT");
                DataColumn Max = new DataColumn("MAXAMOUNT");
                DataColumn Rate = new DataColumn("RATE");
                DataColumn FixAmount = new DataColumn("FIXAMOUNT");
                DataColumn IsLadder = new DataColumn("ISLADDER");
                DataColumn BILLERID = new DataColumn("BILLERID");
                DataColumn FROMCCYID = new DataColumn("FROMCCYID");
                DataColumn TOCCYID = new DataColumn("TOCCYID");
                //----thêm cột id----
                DataColumn FkID = new DataColumn("FkID");
                tblTransDetails.Columns.AddRange(new DataColumn[] { FeeID, From, To, Min, Max, Rate, FkID, FixAmount, IsLadder, BILLERID, FROMCCYID, TOCCYID });
                DataRow r = tblTransDetails.NewRow();
                r["FEEID"] = vFeeID;
                r["TOLIMIT"] = cbToLimit.Checked ? "-1" : SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTo.Text.Trim(), ddlCCYIDFrm.SelectedValue);
                r["FROMLIMIT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtFrom.Text.Trim(), ddlCCYIDFrm.SelectedValue);
                r["MINAMOUNT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMin.Text, ddlCCYIDFrm.SelectedValue);
                r["MAXAMOUNT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtmax.Text.Trim(), ddlCCYIDFrm.SelectedValue);
                r["RATE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRate.Text.Trim());
                r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                r["FIXAMOUNT"] = txtAmount.Visible ? SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), ddlCCYIDFrm.SelectedValue) : "0";
                r["ISLADDER"] = cbIsLadder.Checked.ToString();
                r["BILLERID"] = trBiller.Visible ? ddlBiller.SelectedValue.ToString() : "";
                r["FROMCCYID"] = ddlCCYIDFrm.SelectedValue;
                r["TOCCYID"] = ddlCCYIDTo.SelectedValue;
                tblTransDetails.Rows.Add(r);
                tblTransDetails.DefaultView.Sort = "[" + tblTransDetails.Columns["BILLERID"].ColumnName + "], [" + tblTransDetails.Columns["FROMLIMIT"].ColumnName + "] ASC";
                ViewState["FEEDETAILS"] = tblTransDetails;
            }
            else
            {
                tblTransDetails = (DataTable)ViewState["FEEDETAILS"];
                tblTempt = (DataTable)ViewState["FEEDETAILS"];
                DataRow r = tblTempt.NewRow();
                string a = "";
                reFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(txtFrom.Text.Trim(), true);
                reToLimit = cbToLimit.Checked ? -1 : SmartPortal.Common.Utilities.Utility.isDouble(txtTo.Text.Trim(), true);
                IsLadder = cbIsLadder.Checked;
                reBiller = ddlBiller.SelectedValue.ToString();
                refrmCCYID = ddlCCYIDFrm.SelectedValue;
                retoCCYID = ddlCCYIDTo.SelectedValue;
                if (a.Equals("1")) return;
                foreach (DataRow row in tblTransDetails.Rows)
                {
                    roToLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["TOLIMIT"].ToString(), true);
                    roFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["FROMLIMIT"].ToString(), true);
                    frmCCYID = row["FROMCCYID"].ToString();
                    toCCYID = row["TOCCYID"].ToString();
                    roIsLadder = bool.Parse(row["ISLADDER"].ToString());
                    roBiller = row["BILLERID"].ToString();
                    if (ValidateFee())
                    {
                        r["FEEID"] = vFeeID;
                        r["TOLIMIT"] = cbToLimit.Checked ? "-1" : SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTo.Text.Trim(), ddlCCYIDFrm.SelectedValue);
                        r["FROMLIMIT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtFrom.Text.Trim(),
                            ddlCCYIDFrm.SelectedValue.ToString());
                        r["MINAMOUNT"] =
                            SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMin.Text.Trim(),
                                ddlCCYIDFrm.SelectedValue.ToString());
                        r["MAXAMOUNT"] =
                            SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtmax.Text.Trim(),
                                ddlCCYIDFrm.SelectedValue.ToString());
                        r["RATE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRate.Text.Trim());
                        r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                        r["FIXAMOUNT"] = txtAmount.Visible ? SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), ddlCCYIDFrm.SelectedValue) : "0";
                        r["ISLADDER"] = cbIsLadder.Checked.ToString();
                        r["BILLERID"] = trBiller.Visible ? ddlBiller.SelectedValue.ToString() : "";
                        r["FROMCCYID"] = ddlCCYIDFrm.SelectedValue;
                        r["TOCCYID"] = ddlCCYIDTo.SelectedValue;
                        lblError.Text = string.Empty;
                    }
                    else
                    {
                        a = "1";
                        lblError.Text = Resources.labels.datontaifee;
                        break;
                    }
                }

                if (a != "1")
                {
                    tblTempt.Rows.Add(r);
                    tblTransDetails = tblTempt;
                    r = tblTempt.NewRow();
                }
                tblTempt.DefaultView.Sort = "[" + tblTempt.Columns["BILLERID"].ColumnName + "], [" + tblTempt.Columns["FROMLIMIT"].ColumnName + "] ASC";
                ViewState["FEEDETAILS"] = tblTempt;
                tblTransDetails = (DataTable)ViewState["FEEDETAILS"];
            }
            gvAppTransDetailsList.DataSource = tblTransDetails;
            gvAppTransDetailsList.DataBind();
            if (tblTransDetails != null && tblTransDetails.Rows.Count > 0)
            {
                pnGV.Visible = true;
            }
            else
            {
                pnGV.Visible = false;
            }
            txtAmount.Text = string.Empty;
            txtFrom.Text = "0";
            cbToLimit.Checked = false;
            txtTo.Enabled = true;
            txtTo.Text = "0";
            txtMin.Text = "0";
            txtmax.Text = "0";
            txtRate.Text = "0";
            ShowWarning();
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
    bool ValidateFee()
    {
        if ((frmCCYID != refrmCCYID) || (retoCCYID != toCCYID))
        {
            return true;
        }
        bool isLadderRight = (roToLimit <= reFromLimit && roToLimit != -1) || (roFromLimit >= reToLimit && reToLimit != -1);
        if (!cbIsLadder.Checked)
        {
            return false;
        }
        else
        {
            return isLadderRight;
        }
    }
    protected void ShowWarning()
    {
        try
        {
            if (trBiller.Visible)
            {
                lblBillerNameFee.Text = "";
                DataTable dtfee = (DataTable)ViewState["FEEDETAILS"];

                List<string> lsBiller = new List<string>();
                Hashtable hs = new Hashtable();

                for (int i = 0; i < ddlBiller.Items.Count; i++)
                {
                    lsBiller.Add(ddlBiller.Items[i].Value);
                    hs.Add(ddlBiller.Items[i].Value, ddlBiller.Items[i].Text);
                }

                foreach (string db in lsBiller)
                {
                    if (dtfee.Select("BILLERID = '" + db + "'").Count() == 0)
                    {
                        lblBillerNameFee.Text += hs[db] + "<br>";
                    }
                }

                if (string.IsNullOrEmpty(lblBillerNameFee.Text))
                {
                    pnWarning.Visible = false;
                }
                else
                {
                    pnWarning.Visible = true;
                }
            }
            else
            {
                pnWarning.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
        }
    }

    protected void gvAppTransDetailsList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable tblTransDetailsDel = (DataTable)ViewState["FEEDETAILS"];
        Label lblFkID = (Label)gvAppTransDetailsList.Rows[e.RowIndex].FindControl("lblFkID");
        DataRow[] dr = tblTransDetailsDel.Select("FkID='" + lblFkID.Text.Trim() + "'");
        tblTransDetailsDel.Rows.Remove(dr[0]);
        gvAppTransDetailsList.DataSource = tblTransDetailsDel;
        gvAppTransDetailsList.DataBind();
        if (tblTransDetailsDel.Rows.Count == 0)
        {
            ViewState["FEEDETAILS"] = null;
        }
        else
        {
            tblTransDetailsDel.DefaultView.Sort = "[" + tblTransDetailsDel.Columns["BILLERID"].ColumnName + "], [" + tblTransDetailsDel.Columns["FROMLIMIT"].ColumnName + "] ASC";
            ViewState["FEEDETAILS"] = tblTransDetailsDel;
        }
        if (tblTransDetailsDel != null && tblTransDetailsDel.Rows.Count > 0)
        {
            pnGV.Visible = true;
        }
        else
        {
            pnGV.Visible = false;
        }
        ShowWarning();
    }
    protected void cbIsLadder_CheckedChanged(object sender, EventArgs e)
    {
        if (cbIsLadder.Checked)
        {
            txtAmount.Text = "";
            txtAmount.Enabled = false;
            tbLadderFee.Visible = true;
            btnSaveDetailsrg.Visible = false;
            btnSaveDetails.Visible = true;

        }
        else
        {
            txtAmount.Text = "";
            txtAmount.Enabled = true;
            tbLadderFee.Visible = false;
        }
        ViewState["FEEDETAILS"] = null;
        DataTable tblTransDetailsReLoad = (DataTable)ViewState["FEEDETAILS"];
        gvAppTransDetailsList.DataSource = tblTransDetailsReLoad;
        gvAppTransDetailsList.DataBind();
        if (tblTransDetailsReLoad != null && tblTransDetailsReLoad.Rows.Count > 0)
        {
            pnGV.Visible = true;
        }
        else
        {
            pnGV.Visible = false;
        }
        btnSaveDetails.Visible = false;
        btnSaveDetailsrg.Visible = true;
        txtFrom.Text = "0";
        txtTo.Text = "0";
        txtMin.Text = "0";
        txtmax.Text = "0";
        txtRate.Text = "0";
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
        pnWarning.Enabled = true;
        ViewState["FEEDETAILS"] = null;
        tbLadderFee.Visible = false;
        txtFeeName.Text = string.Empty;
        ddlCCYIDFrm.SelectedIndex = 0;
        ddlCCYIDTo.SelectedIndex = 0;
        vFeeID = "FEE" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
        txtAmount.Text = string.Empty;
        txtAmount.Enabled = true;
        cbIsLadder.Checked = false;
        txtFrom.Text = "0";
        txtTo.Text = "0";
        txtMin.Text = "0";
        txtmax.Text = "0";
        txtRate.Text = "0";
        pnGV.Visible = false;
        pnWarning.Visible = false;
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
}

