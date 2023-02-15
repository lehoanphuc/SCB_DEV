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

public partial class Widgets_SEMSFeeManagement_Controls_Widget : WidgetBase
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
    string reBiller, roBiller;
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
            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','',event)");
            txtmax.Attributes.Add("onkeyup", "ntt('" + txtmax.ClientID + "','',event)");
            txtMin.Attributes.Add("onkeyup", "ntt('" + txtMin.ClientID + "','',event)");
            txtTo.Attributes.Add("onkeyup", "ntt('" + txtTo.ClientID + "','',event)");
            txtFrom.Attributes.Add("onkeyup", "ntt('" + txtFrom.ClientID + "','',event)");
            txtRate.Attributes.Add("onkeyup", "ntt('" + txtRate.ClientID + "','',event)");
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                ViewState["FEEDETAILS"] = null;
                pnWarning.Visible = false;
                //load các giao dịch
                ddlType.DataSource = new SmartPortal.SEMS.Fee().LoadFeeType(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlType.DataTextField = "TYPENAME";
                ddlType.DataValueField = "FEETYPE";
                ddlType.DataBind();
                ddlType.Items.Remove(ddlType.Items.FindByValue("INTERBANK"));

                ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlCCYID.DataTextField = "CCYID";
                ddlCCYID.DataValueField = "CCYID";
                ddlCCYID.DataBind();

                object[] searchObject = new object[] { };
                ddlBiller.DataSource = new SmartPortal.SEMS.Common().common("GET_BILLERFEE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                ddlBiller.DataTextField = "BillerName";
                ddlBiller.DataValueField = "BillerID";
                ddlBiller.DataBind();

                ddlTrans.DataSource = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                ddlTrans.DataTextField = "PAGENAME";
                ddlTrans.DataValueField = "TRANCODE";
                ddlTrans.DataBind();
                ddlTrans.SelectedValue = "IB000499";

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
        DataTable tblTransferFee = new DataTable();
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
					PnTran.Visible = true;
                    btnAddTran.Visible = true;
					ddlTrans.Enabled = true;
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
                            ddlType.SelectedValue = ProDeTable.Rows[0]["FEETYPE"].ToString();
                            ddlFeeType_OnChange(null, null);
                            ddlCCYID.SelectedValue = ProDeTable.Rows[0]["CCYID"].ToString();

                            txtAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ProDeTable.Rows[0]["FIXAMOUNT"].ToString(), ProDeTable.Rows[0]["CCYID"].ToString().Trim());

                            if ((bool)(ProDeTable.Rows[0]["ISLADDER"]) == false && (bool)(ProDeTable.Rows[0]["ISBILLPAYMENTFEE"]) == false)
                            {
                                cbIsLadder.Checked = false;
                                //pnDetailsFee.Visible = false;
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
                                DataColumn IsLader = new DataColumn("ISLADDER");
                                DataColumn BILLERID = new DataColumn("BILLERID");
                                tblTransDetailsEdit.Columns.AddRange(new DataColumn[] { FeeID, From, To, Min, Max, Rate, FkID, FixAmount, IsLader, BILLERID });

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
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
                        if (dsfee.Tables.Count.Equals(2))
                        {
                            tblTransferFee = dsfee.Tables[1];
                            if (tblTransferFee.Rows.Count != 0)
                            {
                                ViewState["TRANSFERFEE"] = tblTransferFee;
                                gvTranfer.DataSource = tblTransferFee;
                                gvTranfer.DataBind();
                            }
                            pnTranfer.Visible = true;
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
					PnTran.Visible = true;
                    ddlTrans.Enabled = false;
                    tbLadderFee.Visible = false;
                    ddlType.Enabled = false;
                    ddlBiller.Enabled = false;
                    ddlCCYID.Enabled = false;
                    txtFeeName.Enabled = false;
                    txtAmount.Enabled = false;
                    cbIsLadder.Enabled = false;
                    txtFrom.Enabled = false;
                    txtTo.Enabled = false;
                    txtMin.Enabled = false;
                    txtmax.Enabled = false;
                    txtRate.Enabled = false;
                    btback.Visible = true;
                    btsave.Enabled = false;
                    btnSaveDetails.Enabled = false;
                    btnSaveDetailsrg.Visible = cbIsLadder.Checked ? false : true;
                    btnSaveDetails.Visible = cbIsLadder.Checked ? true : false;
                    gvAppTransDetailsList.Columns[10].Visible = false;
                    ddlCCYID.Enabled = false;
                    ddlType.Enabled = false;
                    if (ViewState["TRANSFERFEE"] != null)
                    {
                        gvTranfer.Columns[gvTranfer.Rows[0].Cells.Count - 1].Visible = false;
                    }

                    if (ViewState["FEEDETAILS"] != null)
                    {
                        gvAppTransDetailsList.Columns[gvAppTransDetailsList.Rows[0].Cells.Count - 1].Visible = false;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    ddlType.Enabled = true;
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
                    btnSaveDetailsrg.Enabled = true;
                    btnSaveDetails.Enabled = true;
                    btnSaveDetailsrg.Visible = cbIsLadder.Checked ? false : true;
                    btnSaveDetails.Visible = cbIsLadder.Checked ? true : false;
                    ddlCCYID.Enabled = false;
                    ddlType.Enabled = false;
                    break;
            }
            CheckBillerFee();
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
        CheckBillerFee();

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
        if (ddlType.SelectedValue.Equals("TRAN"))
        {
            PnTran.Visible = true;
            ViewState["TRANSFERFEE"] = null;
            gvTranfer.DataSource = (DataTable)ViewState["TRANSFERFEE"];
            gvTranfer.DataBind();
            gvTranfer.Visible = true;
            divAdd.Visible = true;
            cbIsLadder.Checked = true;
            PnAmount.Visible = false;
            cbIsLadder_CheckedChanged(null, null);
        }
          else
        {
            gvTranfer.Visible = false;
            if (ddlType.SelectedValue.Equals("NOR"))
            {
                PnTran.Visible = true;
                divAdd.Visible = true;

            }
            if (ddlType.SelectedValue.Equals("FX"))
            {
                PnTran.Visible = false;
                btnSaveDetailsrg.Visible = true;

            }
            else
            {
                PnTran.Visible = false;
                divAdd.Visible = false;

            }
           
            PnAmount.Visible = true;
        }
    }
    protected void CheckBillerFee()
    {
        bool isBillerFee = ddlType.SelectedValue.Equals("BPM");
        trBiller.Visible = isBillerFee;
        btnSaveDetailsrg.Visible = !cbIsLadder.Checked && trBiller.Visible;
        gvAppTransDetailsList.Columns[7].Visible = isBillerFee;
        gvAppTransDetailsList.Columns[8].Visible = isBillerFee;
        gvAppTransDetailsList.Columns[9].Visible = isBillerFee;
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

            if (cbIsLadder.Checked == false && trBiller.Visible == false)
            {
                lbIDGen.Text = "FEE" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
                fixamount = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            }
            else if (cbIsLadder.Checked == false && trBiller.Visible == true)
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
            string feetype = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlType.SelectedValue.Trim());
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
            DataTable tableFeeTransfer = new DataTable();
            if (ViewState["TRANSFERFEE"] != null)
            {
                tableFeeTransfer = (DataTable)ViewState["TRANSFERFEE"];
                System.Data.DataColumn newColumn = new System.Data.DataColumn("UserCreat", typeof(System.String));
                newColumn.DefaultValue = Session["UserName"].ToString();
                tableFeeTransfer.Columns.Add(newColumn);
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:

                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;

                    new SmartPortal.SEMS.Fee().InsertFee(feeid, feename, feetype, fixamount, isladder, false, chargelater, ccyid, useraction, dateaction, Session["branch"].ToString(), isBillerFee, tableFeeDetails, tableFeeTransfer, ref IPCERRORCODE, ref IPCERRORDESC);

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

                    new SmartPortal.SEMS.Fee().UpdateFee(vFeeID, feename, feetype, fixamount, isladder, false, chargelater, ccyid, useraction, dateaction, Session["branch"].ToString(), isBillerFee, tableFeeDetails, tableFeeTransfer, ref IPCERRORCODE, ref IPCERRORDESC);

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
            Label lblFeeId, lblfrom, lblto, lblmin, lblmax, lblRate, lblFkID, lblfixedfee, lblisladder, lblBillerName;
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
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lblFeeId.Text = drv["FEEID"].ToString();
                lblfrom.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["FROMLIMIT"].ToString(), ddlCCYID.SelectedValue);
                lblto.Text = double.Parse(drv["TOLIMIT"].ToString()) == -1 ? Resources.labels.unlimit : SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TOLIMIT"].ToString(), ddlCCYID.SelectedValue);
                lblmin.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["MINAMOUNT"].ToString(), ddlCCYID.SelectedValue);
                lblmax.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["MAXAMOUNT"].ToString(), ddlCCYID.SelectedValue);
                lblfixedfee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["FIXAMOUNT"].ToString(), ddlCCYID.SelectedValue);
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

    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable tblTransDetails = new DataTable();
            DataTable tblTempt = new DataTable();
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

                //----thêm cột id----
                DataColumn FkID = new DataColumn("FkID");
                tblTransDetails.Columns.AddRange(new DataColumn[] { FeeID, From, To, Min, Max, Rate, FkID, FixAmount, IsLadder, BILLERID });
                DataRow r = tblTransDetails.NewRow();
                if (ACTION == IPC.ACTIONPAGE.EDIT)
                {
                    r["FEEID"] = vFeeID;
                }
                else
                {
                    r["FEEID"] = lbIDGen.Text;
                }
                r["TOLIMIT"] = cbToLimit.Checked ? "-1" : SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTo.Text.Trim(), ddlCCYID.SelectedValue);
                r["FROMLIMIT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtFrom.Text.Trim(), ddlCCYID.SelectedValue);
                r["MINAMOUNT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMin.Text, ddlCCYID.SelectedValue);
                r["MAXAMOUNT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtmax.Text.Trim(), ddlCCYID.SelectedValue);
                r["RATE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRate.Text.Trim());
                r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                r["FIXAMOUNT"] = txtAmount.Visible ? SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), ddlCCYID.SelectedValue) : "0";
                r["ISLADDER"] = cbIsLadder.Checked.ToString();
                r["BILLERID"] = trBiller.Visible ? ddlBiller.SelectedValue.ToString() : "";
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
                if (a.Equals("1")) return;
                foreach (DataRow row in tblTransDetails.Rows)
                {
                    roToLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["TOLIMIT"].ToString(), true);
                    roFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["FROMLIMIT"].ToString(), true);
                    roIsLadder = bool.Parse(row["ISLADDER"].ToString());
                    roBiller = row["BILLERID"].ToString();
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

                        r["TOLIMIT"] = cbToLimit.Checked ? "-1" : SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTo.Text.Trim(), ddlCCYID.SelectedValue);
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
                        r["FIXAMOUNT"] = txtAmount.Visible ? SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), ddlCCYID.SelectedValue) : "0";
                        r["ISLADDER"] = cbIsLadder.Checked.ToString();
                        r["BILLERID"] = trBiller.Visible ? ddlBiller.SelectedValue.ToString() : "";
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
        bool isLadderRight = (roToLimit <= reFromLimit && roToLimit != -1) || (roFromLimit >= reToLimit && reToLimit != -1);
        if (trBiller.Visible)
        {
            if (roBiller.Equals(reBiller))
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
            if (!trBiller.Visible)
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
            btnSaveDetailsrg.Visible = trBiller.Visible;
            btnSaveDetails.Visible = false;
        }
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
        ViewState["TRANSFERFEE"] = null;
        PnTran.Visible = false;
        ddlType.SelectedIndex = 0;
        ddlFeeType_OnChange(null,null);
        pnTranfer.Visible = false;
        tbLadderFee.Visible = false;
        ddlType.SelectedIndex = 0;
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
        CheckBillerFee();
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
    private DataTable CreateTableTransferFee()
    {
        // tạo table tạm chứa TransDetails 
        DataTable dtTransferFee = new DataTable();
        DataColumn TranCode = new DataColumn("TranCode");
        DataColumn FeeID = new DataColumn("FeeID");
        DataColumn Transaction = new DataColumn("Transaction");
        DataColumn FeeType = new DataColumn("Fee Type");
        DataColumn lblCCYID = new DataColumn("Currency");
        //----thêm cột id----
        DataColumn FkID = new DataColumn("FkID");
        dtTransferFee.Columns.AddRange(new DataColumn[] { TranCode, FeeID, Transaction, FeeType, lblCCYID });
        DataRow r = dtTransferFee.NewRow();
        ViewState["TRANSFERFEE"] = dtTransferFee;
        return dtTransferFee;
    }
    protected void gvTranfer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable tbtTransfer = (DataTable)ViewState["TRANSFERFEE"];
            Label lblTrancode = (Label)gvTranfer.Rows[e.RowIndex].FindControl("lblTrancode");
            DataRow[] dr = tbtTransfer.Select("TranCode='" + lblTrancode.Text.Trim() + "'");
            tbtTransfer.Rows.Remove(dr[0]);
            gvTranfer.DataSource = tbtTransfer;
            gvTranfer.DataBind();
            if (tbtTransfer.Rows.Count == 0)
            {
                ViewState["TRANSFERFEE"] = null;
            }
            else
            {
                ViewState["TRANSFERFEE"] = tbtTransfer;
            }
            if (tbtTransfer != null && tbtTransfer.Rows.Count > 0)
            {
                pnTranfer.Visible = true;
            }
            else
            {
                pnTranfer.Visible = false;
            }
        }
        catch
        {

        }


    }
    protected void gvTranfer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblTrancode, lblFeeID, lblTransaction, lblFeeType, lblCCYID;
        LinkButton lbDelete;

        DataRowView drv;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            drv = (DataRowView)e.Row.DataItem;

            lblTrancode = (Label)e.Row.FindControl("lblTrancode");
            lblFeeID = (Label)e.Row.FindControl("lblFeeID");
            lblTransaction = (Label)e.Row.FindControl("lblTransaction");
            lblFeeType = (Label)e.Row.FindControl("lblFeeType");
            lblCCYID = (Label)e.Row.FindControl("lblCCYID");
            lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

            lblTrancode.Text = drv["TranCode"].ToString();
            lblFeeID.Text = drv["FeeID"].ToString();
            lblTransaction.Text = drv["Transaction"].ToString();
            lblFeeType.Text = drv["Fee Type"].ToString();
            lblCCYID.Text = drv["Currency"].ToString();
            if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
            {
                lbDelete.OnClientClick = string.Empty;
            }
        }
    }
    protected void gvTranfer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvTranfer.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnAddTran_Click(object sender, EventArgs e)
    {
        pnTranfer.Visible = true;
        try
        {
            string IDFEE = string.Empty;
            if (ACTION == IPC.ACTIONPAGE.EDIT)
            {
                IDFEE = vFeeID;
            }
            else
            {
                IDFEE = lbIDGen.Text;
            }
            if (ViewState["TRANSFERFEE"] == null)
            {
                DataTable dtTransferFee = CreateTableTransferFee();
                DataRow r = dtTransferFee.NewRow();
                r["TranCode"] = ddlTrans.SelectedValue;
                r["FeeID"] = IDFEE;
                r["Transaction"] = ddlTrans.SelectedItem.Text;
                r["Fee Type"] = "Transfet fee";
                r["Currency"] = ddlCCYID.SelectedValue;
                dtTransferFee.Rows.Add(r);
                ViewState["TRANSFERFEE"] = dtTransferFee;

                gvTranfer.DataSource = dtTransferFee;
                gvTranfer.DataBind();
                if (dtTransferFee != null && dtTransferFee.Rows.Count > 0)
                {
                    pnTranfer.Visible = true;
                }
                else
                {
                    pnTranfer.Visible = false;
                }
            }
            else
            {
                DataTable dtTransferFee = (DataTable)ViewState["TRANSFERFEE"];
                if (!dtTransferFee.Rows[0]["Currency"].ToString().Equals(ddlCCYID.SelectedValue))
                {
                    lblError.Text = "Not same Currency";
                    return;
                }
                DataRow[] arrWLR = dtTransferFee.Select("TranCode='" + ddlTrans.SelectedValue + "'");
                if (arrWLR.Count().Equals(0))
                {
                    DataRow r = dtTransferFee.NewRow();
                    r["TranCode"] = ddlTrans.SelectedValue;
                    r["FeeID"] = IDFEE;
                    r["Transaction"] = ddlTrans.SelectedItem.Text;
                    r["Fee Type"] = "Transfer fee";
                    r["Currency"] = ddlCCYID.SelectedValue;
                    dtTransferFee.Rows.Add(r);
                    ViewState["TRANSFERFEE"] = dtTransferFee;
                }
                else
                {

                }
                gvTranfer.DataSource = dtTransferFee;
                gvTranfer.DataBind();
            }
        }
        catch
        {

        }
    }
}

