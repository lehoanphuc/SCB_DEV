using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
public partial class Widgets_SEMSAgentBank_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static int flag = 1;
    SmartPortal.SEMS.Common _common = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                loadCombobox();
            }
            if (flag == 1)
            {
                GridViewPaging.pagingClickArgs += new EventHandler(Search_GridViewPaging_click);
            }
            if (flag == 0)
            {
                GridViewPaging.pagingClickArgs += new EventHandler(AdvanceSearch_GridViewPaging_click);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void loadBanktype()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("WAL_AGENT_BANK", "TYPE", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddBanktype.DataSource = ds;
                ddBanktype.DataValueField = "VALUEID";
                ddBanktype.DataTextField = "CAPTION";
                ddBanktype.DataBind();
            }
        }
    }
    private void loadStatus()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("WAL_AGENT_BANK", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddStatus.DataSource = ds;
                ddStatus.DataValueField = "VALUEID";
                ddStatus.DataTextField = "CAPTION";
                ddStatus.DataBind();
            }
        }
    }

    private void loadCombobox()
    {
        loadBanktype();
        loadStatus();
    }

    protected void Search_GridViewPaging_click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridViewPaging.SelectPageChoose = "1";
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

    void BindData()
    {
        try
        {
            p.Visible = true;
            flag = 1;
            rptData.DataSource = null;
            loadCombobox();
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(txtSearch.Text.Trim()), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _common.common("SEMS_AGENT_SEARCH", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    rptData.DataSource = ds;
                    rptData.DataBind();
                    GridViewPaging.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0][IPC.TRECORDCOUNT].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void BindData_SearchAdvance()
    {
        try
        {
            p.Visible = true;
            rptData.DataSource = null;
            flag = 0;
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(txtBankCode.Text.Trim()), Utility.KillSqlInjection(txtBankName.Text.Trim()), Utility.KillSqlInjection(txtShortName.Text.Trim()), Utility.KillSqlInjection(ddBanktype.SelectedValue), Utility.KillSqlInjection(txtCountryCode.getCountryID()), Utility.KillSqlInjection(ddStatus.SelectedValue), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _common.common("SEMS_AGENT_ADV", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    rptData.DataSource = ds;
                    rptData.DataBind();
                    GridViewPaging.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0][IPC.TRECORDCOUNT].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnClear_click(object sender, EventArgs e)
    {
        loadCombobox();
        txtSearch.Text = string.Empty;
        txtBankCode.Text = string.Empty;
        txtBankName.Text = string.Empty;
        txtShortName.Text = string.Empty;
        txtSearch.Text = string.Empty;
        txtCountryCode.Text = string.Empty;
        txtCountryCode.Value = string.Empty;
        ddBanktype.SelectedValue = string.Empty;
        ddStatus.SelectedValue = string.Empty;
        rptData.DataSource = null;
        rptData.DataBind();
        p.Visible = false;
        hdCLMS_SCO_SCO_PRODUCT.Value = string.Empty;
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        //{
        //    RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        //}
        RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
    }

    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        //if (CheckPermitPageAction(commandName))
        //{
        switch (commandName)
        {
            case IPC.ACTIONPAGE.EDIT:
                RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                break;
            case IPC.ACTIONPAGE.DETAILS:
                RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                break;

            case IPC.ACTIONPAGE.DELETE:
                deleteFunction(commandArg);
                AutoSwitchSearch();
                break;
        }
        //}
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        //{
        string[] lst = hdCLMS_SCO_SCO_PRODUCT.Value.ToString().Split('#');
        if (lst.Length <= 1)
        {
            lblError.Text = Resources.labels.Selectoneormoretodelete;
            return;
        }
        foreach (string item in lst)
        {
            if (item.Equals("") || item.Equals("on")) continue;
            deleteFunction(item);
            if (!IPCERRORCODE.Equals("0"))
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        if (IPCERRORCODE.Equals("0"))
        {
            lblError.Text = Resources.labels.deletesuccessfully;
            if (rptData.Items.Count == 0)
            {
                int SelectPageNo = int.Parse(GridViewPaging.SelectPageChoose.ToString()) - 1;
                GridViewPaging.SelectPageChoose = SelectPageNo.ToString();
            }
        }
        AutoSwitchSearch();
        //}
    }
    public void deleteFunction(string bankId)
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(bankId) };
            _common.common("SEMS_AGENT_BANK_DEL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                lblError.Text = Resources.labels.deletesuccessfully;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void AutoSwitchSearch()
    {
        if (flag == 1)
        {
            BindData();
        }
        if (flag == 0)
        {
            BindData_SearchAdvance();
        }
        hdCLMS_SCO_SCO_PRODUCT.Value = string.Empty;
    }

}