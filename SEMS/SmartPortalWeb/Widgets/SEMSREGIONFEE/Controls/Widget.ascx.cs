using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Resources;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSREGIONFEE_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    public string _TITLE
    {
        get { return lblTitleBranch.Text; }
        set { lblTitleBranch.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                ddlRegionType.Items.Insert(0, new ListItem(Resources.labels.autonomousregion, "A"));
                ddlRegionType.Items.Insert(1, new ListItem(Resources.labels.bang, "B"));
                ddlRegionType.Items.Insert(2, new ListItem(Resources.labels.federalteritory, "F"));
                ddlRegionType.Items.Insert(3, new ListItem(Resources.labels.region, "R"));
                for (int i = 1; i < 21; i++)
                {
                    ListItem li =
                        new ListItem(i.ToString(), i.ToString());
                    ddlOrder.Items.Add(li);
                }

                ddlCountryId.DataSource = new RegionFee().GetAllCountry(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlCountryId.DataTextField = "COUNTRYNAME";
                ddlCountryId.DataValueField = "COUNTRYID";
                ddlCountryId.DataBind();
                GridViewPaging.Visible = false;
                BindData();
            }

            GridViewPaging.pagingClickArgs += GridViewPagingClick;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    private void GridViewPagingClick(object sender, EventArgs e)
    {
        gvBranchList.PageSize =
            Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvBranchList.PageIndex = Convert.ToInt32(((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }

    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    string id = GetParamsPage(IPC.ID)[0].Trim();
                    DataTable dtregion = new RegionFee().CheckRegionID(id);
                    if (dtregion.Rows.Count != 0)
                    {
                        txRegionID.Text = dtregion.Rows[0][IPC.REGIONID].ToString().Trim();
                        txRegionName.Text = dtregion.Rows[0][IPC.REGIONNAME].ToString().Trim();
                        txDescription.Text = dtregion.Rows[0][IPC.DESCRIPTION].ToString().Trim();
                        txtRegionCode.Text = dtregion.Rows[0]["regioncode"].ToString();
                        ddlRegionType.SelectedValue = dtregion.Rows[0]["regiontype"].ToString();
                        ddlOrder.SelectedValue = dtregion.Rows[0]["order"].ToString();
                        string countryId = dtregion.Rows[0]["CountryId"].ToString();
                        ddlCountryId.DataSource = new RegionFee().SearchCountryById(int.Parse(countryId));
                        ddlCountryId.SelectedValue = countryId;
                    }

                    break;
            }

            #region Enable/Disable theo Action
            showbranch(false);
            divpnbranchinfo.Visible = false;
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnRegion.Enabled = false;
                    btsave.Visible = false;
                    showbranch(true);
                    divpnbranchinfo.Visible = true;
                    btnClear.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    btnClear.Enabled = false;
                    break;
                default:
                    btnClear.Enabled = true;
                    break;
            }

            #endregion
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string regionId = Utility.KillSqlInjection(txRegionID.Text.Trim());
            string regionName = Utility.KillSqlInjection(txRegionName.Text.Trim());
            string regionCode = Utility.KillSqlInjection(txtRegionCode.Text.Trim());
            string regionType = Utility.KillSqlInjection(ddlRegionType.SelectedValue.Trim());
            int order = int.Parse(Utility.KillSqlInjection(ddlOrder.SelectedValue));
            string description = Utility.KillSqlInjection(txDescription.Text.Trim());
            string countryId = Utility.KillSqlInjection(ddlCountryId.SelectedValue);
            string usercreate = Session["userName"].ToString();

            #region Validations

            #endregion

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    try
                    {
                        btnClear.Enabled = true;
                        string status = IPC.ACTIVE;

                        new RegionFee().InsertRegionFee(regionCode, regionName, regionType, description, usercreate,
                            status,
                            order, countryId, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.themvungphithanhcong;
                            btsave.Visible = false;
                            pnRegion.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                        }

                        // }
                    }
                    catch (IPCException IPCex)
                    {
                        SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name,
                            System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(
                            ConfigurationManager.AppSettings["sysec"],
                            this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name,
                            "", Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(
                            ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }

                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    try
                    {
                        new RegionFee().Updateregionfee(regionId, regionCode, regionName, regionType, description,
                            usercreate,
                            IPC.ACTIVE, order, countryId, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.suavungphithanhcong;
                            btsave.Visible = false;
                            pnRegion.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                        }
                    }
                    catch (IPCException IPCex)
                    {
                        SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name,
                            System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(
                            ConfigurationManager.AppSettings["sysec"],
                            this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name,
                            ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(
                            ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }

                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void gvBranchList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HyperLink lblBranchCode;
            Label lblBranchName;
            Label lblPhone;
            Label lblAddress;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView) e.Row.DataItem;
                lblBranchCode = (HyperLink) e.Row.FindControl("lblBranchCode");
                lblBranchName = (Label) e.Row.FindControl("lblBranchName");
                lblPhone = (Label) e.Row.FindControl("lblPhone");
                lblAddress = (Label) e.Row.FindControl("lblAddress");
                lblBranchCode.Text = drv["BranchID"].ToString();
                lblBranchCode.NavigateUrl =
                    SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=185&a=DETAILS&ID=" +
                                                          drv["BranchID"].ToString());
                lblBranchName.Text = drv["BranchName"].ToString();
                lblPhone.Text = drv["Phone"].ToString();
                lblAddress.Text = drv["Address"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    private void showbranch(bool show)
    {
        try
        {
            if (show)
            {
                int pageSize = gvBranchList.PageSize;
                int pageIndex = gvBranchList.PageIndex;
                int recordIndex = pageIndex * pageSize;
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                if (Convert.ToInt32(((HiddenField) GridViewPaging.FindControl("TotalRows")).Value) < recordIndex)
                    return;
                pnbranchinfor.Visible = true;
                string regionid = GetParamsPage(IPC.ID)[0].Trim();
                DataTable dtregion = new RegionFee()
                    .showbranchbyregionfeeid(regionid, pageSize, recordIndex, ref IPCERRORCODE, ref IPCERRORDESC)
                    .Tables[0];
                if (dtregion.Rows.Count > 0)
                {
                    gvBranchList.DataSource = dtregion;
                    gvBranchList.DataBind();
                    if (IPCERRORCODE == "0")
                    {
                        litError.Text = string.Empty;
                        ((HiddenField) GridViewPaging.FindControl("TotalRows")).Value =
                            dtregion.Rows[0]["TRECORDCOUNT"].ToString();
                    }
                }
                else
                {
                    litError.Text = "<p class='divDataNotFound'>" + labels.datanotfound + "</p>";
                    GridViewPaging.Visible = false;
                    pnbranchinfor.Visible = false;
                }
            }
            else
            {
                pnbranchinfor.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        litError.Text = string.Empty;
        btsave.Visible = true;
        pnRegion.Enabled = true;
        txtRegionCode.Text = string.Empty;
        txRegionName.Text = string.Empty;
        txDescription.Text = string.Empty;
        ddlOrder.SelectedIndex = 0;
        ddlCountryId.SelectedIndex = 0;
        ddlRegionType.SelectedIndex = 0;
    }
}