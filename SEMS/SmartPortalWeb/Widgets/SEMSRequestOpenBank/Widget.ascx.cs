using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSKYCMerchant_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    String countRow = "0";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public static int EventPageSize = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                loadCombobox();
                setDefaultDataSearch();
            }
            if (EventPageSize == 1)
            {
                GridViewPaging.pagingClickArgs += new EventHandler(Search_GridViewPaging_click);
            }
            else if (EventPageSize == 2)
            {
                GridViewPaging.pagingClickArgs += new EventHandler(AdvanceSearch_GridViewPaging_click);
            }
            if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
            {
                btnAdd_New.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    private void loadCombobox_KYCLevel()
    {
        DataSet ds = new DataSet();
        object[] loadKYCLevel = new object[] { string.Empty };
        ds = _service.common("SEMS_BO_GET_INFO_KYC", loadKYCLevel, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddKycLevel.DataSource = ds;
                    ddKycLevel.DataValueField = "KycID";
                    ddKycLevel.DataTextField = "KycName";
                    ddKycLevel.DataBind();
                    ddKycLevel.Items.Insert(0, new ListItem("All", ""));
                }
        }

    }
    public void setDefaultDataSearch()
    {
        pnResult.Visible = false;
        rptData.DataSource = null;
        rptData.DataBind();
        GridViewPaging.Visible = false;
    }
    private void loadCombobox_ProfileStatus()
    {
        ddStatus.Items.Insert(0, new ListItem("New", "N"));
        ddStatus.Items.Insert(1, new ListItem("Active", "A"));
        ddStatus.Items.Insert(2, new ListItem("Reject", "R"));
        ddStatus.Items.Insert(3, new ListItem("All", ""));
    }

    private void loadCombobox()
    {
        loadCombobox_KYCLevel();
        loadCombobox_ProfileStatus();
    }
    void BindData()
    {
        GridViewPaging.Visible = true;
        pnResult.Visible = true;
        try
        {

            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(txtSearch.Text.Trim()), "IND", GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMSSEARCHOPENBANK", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
            {
                if (ds.Tables[0].Rows.Count < 1 & GridViewPaging.pageIndex > 0)
                {
                    GridViewPaging.SelectPageChoose = "1";
                    BindData();
                }
                else
                {
                    countRow = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0][IPC.TRECORDCOUNT].ToString();
                    rptData.DataSource = ds;
                    rptData.DataBind();
                }
            }
            GridViewPaging.total = countRow;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        EventPageSize = 1;
        rptData.DataSource = null;
        GridViewPaging.SelectPageChoose = "1";
        rptData.DataBind();
        BindData();
    }
    protected void Search_GridViewPaging_click(object sender, EventArgs e)
    {
        EventPageSize = 1;
        rptData.DataSource = null;
        rptData.DataBind();
        BindData();
    }
    protected void AdvanceSearch_GridViewPaging_click(object sender, EventArgs e)
    {
        GridViewPaging.Visible = true;
        pnResult.Visible = true;
        EventPageSize = 2;
        Bindata_AdvanceSearch();
    }
    protected void btnClear_click(object sender, EventArgs e)
    {
        txtSearch.Text = String.Empty;
        txtPhoneNumber.Text = String.Empty;
        txtPaperNumber.Text = String.Empty;
        txtMerchantName.Text = String.Empty;
        loadCombobox();
        setDefaultDataSearch();
    }

    private void Bindata_AdvanceSearch()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(txtPhoneNumber.Text.Trim()),
                 Utility.KillSqlInjection(ddKycLevel.SelectedValue),
                Utility.KillSqlInjection(txtPaperNumber.Text.Trim()),
                Utility.KillSqlInjection( ddStatus.SelectedValue)
                ,  Utility.KillSqlInjection(txtMerchantName.Text.Trim()), "IND",
                GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEARCHADOPENBANK", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
            {
                if (ds.Tables[0].Rows.Count < 1 & GridViewPaging.pageIndex > 0)
                {
                    GridViewPaging.SelectPageChoose = "1";
                    Bindata_AdvanceSearch();
                }
                else
                {
                    countRow = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0][IPC.TRECORDCOUNT].ToString();
                    rptData.DataSource = ds;
                    rptData.DataBind();
                }
                GridViewPaging.total = countRow;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnAdvanceSearch_click(object sender, EventArgs e)
    {
        GridViewPaging.Visible = true;
        pnResult.Visible = true;
        EventPageSize = 2;
        GridViewPaging.SelectPageChoose = "1";
        Bindata_AdvanceSearch();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }

    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            LinkButton lbtnEdit = (LinkButton)e.Item.FindControl("lbtnEdit");
            LinkButton lbtnApprove = (LinkButton)e.Item.FindControl("lbtnApprove");
            lbtnApprove.CssClass = "btn btn-primary";
            lbtnEdit.CssClass = "btn btn-primary";
            if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
            {
                lbtnEdit.Enabled = false;
            }
            if (!CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE))
            {
                lbtnApprove.Enabled = false;
            }
        }
    }


    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.APPROVE:
                    RedirectToActionPage(IPC.ACTIONPAGE.APPROVE, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.REJECT:
                    RedirectToActionPage(IPC.ACTIONPAGE.REJECT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }
}