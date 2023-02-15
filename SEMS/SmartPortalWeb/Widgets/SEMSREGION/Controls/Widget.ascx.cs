using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Resources;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSREGION_Controls_Widget : WidgetBase
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

                GridViewPaging.Visible = false;
                LoadRegionType();
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
                    DataTable dtregion = new Region().CheckRegionID(id);
                    if (dtregion.Rows.Count != 0)
                    {
                        txRegionID.Text = dtregion.Rows[0][IPC.REGIONID].ToString().Trim();
                        txRegionName.Text = dtregion.Rows[0][IPC.REGIONNAME].ToString().Trim();
                        txDescription.Text = dtregion.Rows[0][IPC.DESCRIPTION].ToString().Trim();
                        ddlRegionSpecial.SelectedValue=(dtregion.Rows[0][IPC.REGIONSPECIAL].ToString().Trim());
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
            string description = Utility.KillSqlInjection(txDescription.Text.Trim());
            string regionspecical = ddlRegionSpecial.SelectedValue.ToString();
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

                        new Region().InsertRegion(regionName, regionspecical, description, usercreate,
                            status,
                            ref IPCERRORCODE, ref IPCERRORDESC);
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
                        new Region().Updateregion(regionId,  regionName, ddlRegionSpecial.SelectedValue.ToString(), description,
                            usercreate,
                            IPC.ACTIVE,  ref IPCERRORCODE, ref IPCERRORDESC);
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
            HyperLink lblBranchID;
            Label lblBankName;
            Label lblBranchName;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView) e.Row.DataItem;
                lblBranchID = (HyperLink) e.Row.FindControl("lblBranchID");
                lblBankName = (Label) e.Row.FindControl("lblBankName");
                lblBranchName = (Label) e.Row.FindControl("lblBranchName");
                switch (drv["Table"].ToString())
                {
                    case "EBA_Branch":
                        lblBranchID.NavigateUrl =
                            SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=185&a=DETAILS&ID=" +
                                                                  drv["BranchID"].ToString());
                        break;
                        default:
                            lblBranchID.NavigateUrl =
                                SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=PARTNERBANKBRANCHVIEW&a=DETAILS&ID=" +
                                                                      drv["BranchID"].ToString());
                        break;
                }
                lblBranchID.Text = drv["BranchID"].ToString();
                lblBankName.Text = drv["BankName"].ToString();
                lblBranchName.Text = drv["BranchName"].ToString();
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
                DataTable dtregion = new Region()
                    .showbranchbyregionid(regionid, pageSize, recordIndex, ref IPCERRORCODE, ref IPCERRORDESC)
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
        txRegionName.Text = string.Empty;
        txDescription.Text = string.Empty;
    }
    private void LoadRegionType()
    {
        ddlRegionSpecial.Items.Insert(0, new ListItem("Yes", "Y"));
        ddlRegionSpecial.Items.Insert(1, new ListItem("No", "N"));
    }
}