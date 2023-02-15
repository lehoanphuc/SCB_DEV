using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Constant;

public partial class Widgets_SEMSKYCMerchant_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    //string Action_search = "S";
    public string Action_search
    {
        get
        {
            // check if not exist to make new (normally before the post back)
            // and at the same time check that you did not use the same viewstate for other object
            if (!(ViewState["ACTION_SEARCH"] is string))
            {
                // need to fix the memory and added to viewstate
                ViewState["ACTION_SEARCH"] = "";
            }

            return (string)ViewState["ACTION_SEARCH"];
        }
        set
        {
            ViewState["ACTION_SEARCH"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                loadCombobox();
            }
            if (Action_search.Equals("S"))
            {
                GridViewPaging.pagingClickArgs += new EventHandler(Search_GridViewPaging_click);
            }
            if (Action_search.Equals("AS"))
            {
                GridViewPaging.pagingClickArgs += new EventHandler(AdvanceSearch_GridViewPaging_click);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    private void setDefaultTextbox()
    {
        txtMerchantName.Text = string.Empty;
        txtPaperNumber.Text = string.Empty;
        txtPhoneNumber.Text = string.Empty;
        txtSearch.Text = string.Empty;
    }
    private void loadCombobox_KYCLevel()
    {
        // Save list KYCLevel Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_KYCLevel"];
            if (ds == null)
            {
                object[] searchObject = new object[] { string.Empty };
                ds = _service.common("SEMS_BO_GET_INFO_KYC", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddKycLevel.DataSource = ds;
                        ddKycLevel.DataValueField = "KycCode";
                        ddKycLevel.DataTextField = "KycName";
                        ddKycLevel.DataBind();
                    }
                }
                Cache.Insert("Wallet_KYCLevel", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                ddKycLevel.Items.Insert(0, new ListItem("All", string.Empty));
            }
            else
            {
                ddKycLevel.DataSource = (DataSet)Cache["Wallet_KYCLevel"];
                ddKycLevel.DataValueField = "KycCode";
                ddKycLevel.DataTextField = "KycName";
                ddKycLevel.DataBind();
                ddKycLevel.Items.Insert(0, new ListItem("All", string.Empty));
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadCombobox_ProfileStatus()
    {
        
        // Save list STT KYC Request Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_STT_KYCRequest"];
            if (ds == null)
            {
                ds = _service.GetValueList("WAL_KYC_REQUEST", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddStatus.DataSource = ds;
                        ddStatus.DataValueField = "ValueID";
                        ddStatus.DataTextField = "Caption";
                        ddStatus.DataBind();
                    }
                }
                Cache.Insert("Wallet_STT_KYCRequest", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                ddStatus.Items.Insert(0, new ListItem("All", string.Empty));
            }
            else
            {
                ddStatus.DataSource = (DataSet)Cache["Wallet_STT_KYCRequest"];
                ddStatus.DataValueField = "ValueID";
                ddStatus.DataTextField = "Caption";
                ddStatus.DataBind();
                ddStatus.Items.Insert(0, new ListItem("All", string.Empty));
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadCombobox()
    {
        loadCombobox_KYCLevel();
        loadCombobox_ProfileStatus();
    }
    void BindData()
    {
        try
        {
            pnResult.Visible = true;
            loadCombobox();
            Action_search = "S";
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { txtSearch.Text.Trim(), IPC.PARAMETER.MERCHANT_AGENT, GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_BO_SEARCH_KYC", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if(ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    rptData.DataSource = ds;
                    rptData.DataBind();
                }
                GridViewPaging.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    void BindData_SearchAdvance()
    {
        try
        {
            pnResult.Visible = true;
            DataSet ds = new DataSet();
            Action_search = "AS";
            object[] searchObject = new object[] { txtPhoneNumber.Text.Trim(), ddKycLevel.SelectedValue.Trim(), txtPaperNumber.Text.Trim(),
                ddStatus.SelectedValue.Trim(),txtcuscode.Text.Trim(), txtMerchantName.Text.Trim(), "AM", GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_BO_SEARCHAD_KYC", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    rptData.DataSource = ds;
                    rptData.DataBind();
                }
                GridViewPaging.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridViewPaging.SelectPageChoose = "1";
        BindData();
    }
    protected void btnClear_click(object sender, EventArgs e)
    {
        setDefaultTextbox();
        pnResult.Visible = false;
    }

    protected void Search_GridViewPaging_click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void AdvanceSearch_GridViewPaging_click(object sender, EventArgs e)
    {
        BindData_SearchAdvance();
    }

    protected void btnAdvanceSearch_click(object sender, EventArgs e)
    {
        GridViewPaging.SelectPageChoose = "1";
        BindData_SearchAdvance();
    }

    protected void loadKYCLevel(object sender, EventArgs e)
    {
        BindData_SearchAdvance();
    }
    protected void loadStatus(object sender, EventArgs e)
    {
        BindData_SearchAdvance();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
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
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }

    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            DataRowView drv = e.Item.DataItem as DataRowView;
            LinkButton lbtnEdit = (LinkButton)e.Item.FindControl("lbtnEdit");
            LinkButton lbtnApprove = (LinkButton)e.Item.FindControl("lbtnApprove");
            lbtnApprove.CssClass = "btn btn-primary";
            lbtnEdit.CssClass = "btn btn-primary";
            switch (drv["Status"].ToString().Trim())
            {
                case "P":
                    lbtnEdit.Enabled = CheckPermitPageAction(IPC.ACTIONPAGE.EDIT);
                    break;
                case "N":
                    lbtnApprove.Enabled = CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE);
                    break;
                default:
                    lbtnApprove.Enabled = false;
                    lbtnEdit.Enabled = false;
                    break;
            }
        }
    }

}