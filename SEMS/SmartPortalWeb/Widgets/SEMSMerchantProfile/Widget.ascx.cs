using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Constant;
using System.Web.UI.HtmlControls;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSKYCMerchant_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    //string ACTION_SEARCH = "Search";
    public string ACTION_SEARCH
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
                ACTION_SEARCH = "Search";
                loadCombobox();
            }
            if (ACTION_SEARCH.Equals("Search"))
            {
                GridViewPaging.pagingClickArgs += new EventHandler(Search_GridViewPaging_click);
            }
            if (ACTION_SEARCH.Equals("AdvanceSearch"))
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
    private void loadCombobox_Nation()
    {
        // Save list Nation Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_Nation"];
            if (ds == null)
            {
                object[] _object = new object[] { string.Empty, string.Empty, null, null };
                ds = _service.common("WAL_BO_GET_NATION", _object, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddNationality.DataSource = ds;
                        ddNationality.DataValueField = "NationCode";
                        ddNationality.DataTextField = "NationName";
                        ddNationality.DataBind();
                    }
                }
                Cache.Insert("Wallet_Nation", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                ddNationality.Items.Insert(0, new ListItem("All", string.Empty));
            }
            else
            {
                ddNationality.DataSource = (DataSet)Cache["Wallet_Nation"];
                ddNationality.DataValueField = "NationCode";
                ddNationality.DataTextField = "NationName";
                ddNationality.DataBind();
                ddNationality.Items.Insert(0, new ListItem("All", string.Empty));
            }
        }
        catch (Exception ex)
        {

        }

    }

    private void loadCombobox_ProfileStatus()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_Status"];
            if (ds == null)
            {
                ds = _service.GetValueList("WALLET", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
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
                Cache.Insert("Wallet_Status", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                ddStatus.Items.Insert(0, new ListItem("All", string.Empty));
            }
            else
            {
                ddStatus.DataSource = (DataSet)Cache["Wallet_Status"];
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
        loadCombobox_Nation();
        loadCombobox_ProfileStatus();
    }

    void loadData()
    {
        pnResult.Visible = true;
        rptData.DataSource = null;
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { txtSearch.Text.Trim(), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
        ds = _service.common("SEMS_MER_PRO_SEARCH", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                rptData.DataSource = ds;
                rptData.DataBind();
                GridViewPaging.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
                litError.Text = string.Empty;
                rptData.Visible = true;
                GridViewPaging.Visible = true;
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                rptData.Visible = false;
                GridViewPaging.Visible = false;
            }
        }
    }
    void BindData()
    {
        try
        {
            loadCombobox();
            loadData();
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
            rptData.DataSource = null;
            ACTION_SEARCH = "AdvanceSearch";
            
            if (!Utility.CheckSpecialCharacters(txtPhoneNumber.Text.Trim()))
            {
                lblError.Text = Resources.labels.PhoneNumber + Resources.labels.ErrorSpeacialCharacters;
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return;
            }

            if (!Utility.CheckSpecialCharacters(txtMerchantName.Text.Trim()))
            {
                lblError.Text = Resources.labels.MerchantName + Resources.labels.ErrorSpeacialCharacters;
                txtMerchantName.BorderColor = System.Drawing.Color.Red;
                txtMerchantName.Focus();
                return;
            }

            if (!Utility.CheckSpecialCharacters(txtPaperNumber.Text.Trim()))
            {
                lblError.Text = Resources.labels.PaperNumber + Resources.labels.ErrorSpeacialCharacters;
                txtPaperNumber.BorderColor = System.Drawing.Color.Red;
                txtPaperNumber.Focus();
                return;
            }

            rptData.DataSource = null;
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { txtPhoneNumber.Text.Trim(), ddNationality.SelectedValue.Trim(), txtMerchantName.Text.Trim(), 
                ddStatus.SelectedValue.Trim(), txtPaperNumber.Text.Trim(), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_MER_PRO_SEA_AD", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rptData.DataSource = ds;
                    rptData.DataBind();
                    GridViewPaging.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
                    litError.Text = string.Empty;
                    rptData.Visible = true;
                    GridViewPaging.Visible = true;
                }
                else
                {
                    litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                    rptData.Visible = false;
                    GridViewPaging.Visible = false;
                }
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
        ACTION_SEARCH = "Search";
        GridViewPaging.SelectPageChoose = "1";
        if (!Utility.CheckSpecialCharacters(txtSearch.Text.Trim()))
        {
            lblError.Text = Resources.labels.search + Resources.labels.ErrorSpeacialCharacters;
            txtSearch.BorderColor = System.Drawing.Color.Red;
            txtSearch.Focus();
            return;
        }
        BindData();
    }
    protected void btnClear_click(object sender, EventArgs e)
    {
        setDefaultTextbox();
        //BindData();
        pnResult.Visible = false;
    }

    protected void Search_GridViewPaging_click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnAdvanceSearch_click(object sender, EventArgs e)
    {
        GridViewPaging.SelectPageChoose = "1";
        BindData_SearchAdvance();
    }
    protected void AdvanceSearch_GridViewPaging_click(object sender, EventArgs e)
    {
        BindData_SearchAdvance();
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }

    protected void loadKYCLevel(object sender, EventArgs e)
    {
        BindData_SearchAdvance();
    }
    protected void loadStatus(object sender, EventArgs e)
    {
        BindData_SearchAdvance();
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
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }
    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if (rptData.Items.Count < 1)
        //{
        //    if (e.Item.ItemType == ListItemType.Footer)
        //    {
        //        Label lblFooter = (Label)e.Item.FindControl("lblErrorMsg");
        //        lblFooter.Visible = true;
        //    }
        //}

        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            HtmlGenericControl lbStatus = e.Item.FindControl("lbStatus") as HtmlGenericControl;
            string Status = lbStatus.InnerText;
            LinkButton btnlinkEdit = (LinkButton)e.Item.FindControl("btnlinkEdit");
            LinkButton btnlinkApprove = (LinkButton)e.Item.FindControl("btnlinkApprove");
            btnlinkApprove.CssClass = "btn btn-secondary";
            if (Status == "Active")
            {
                btnlinkApprove.Enabled = false;
            }
            if (Status == "Reject")
            {
                btnlinkApprove.Enabled = false;
            }
            //if (Status == "New")
            //{
            //    btnlinkEdit.Visible = false;
            //}
        }
        
    }
}