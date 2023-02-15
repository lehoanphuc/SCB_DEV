using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSREGIONFEE_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
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
            }
            if (EventPageSize == 1)
            {
                GridViewPaging.pagingClickArgs += new EventHandler(Search_GridViewPaging_click);
            }
            if (EventPageSize == 2)
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

    private void loadCombobox_BalanceSide()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("BALANCESIDE", "ACT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlBalanceSide.DataSource = ds;
                    ddlBalanceSide.DataValueField = "VALUEID";
                    ddlBalanceSide.DataTextField = "CAPTION";
                    ddlBalanceSide.DataBind();
                }
                ddlBalanceSide.Items.Insert(0, new ListItem("All", ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_Group()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("AC_GRP", "ACT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlGroup.DataSource = ds;
                    ddlGroup.DataValueField = "VALUEID";
                    ddlGroup.DataTextField = "CAPTION";
                    ddlGroup.DataBind();
                }
                ddlGroup.Items.Insert(0, new ListItem("All", ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_Classification()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("AC_CLS", "ACT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlClassification.DataSource = ds;
                    ddlClassification.DataValueField = "VALUEID";
                    ddlClassification.DataTextField = "CAPTION";
                    ddlClassification.DataBind();
                }
                ddlClassification.Items.Insert(0, new ListItem("All", ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
       
    }
    private void loadCombobox()
    {

        loadCombobox_BalanceSide();
        loadCombobox_Group();
        loadCombobox_Classification();
        loadCombobox_AccountLevel();
    }
    private void loadCombobox_AccountLevel()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("ACC_LEVEL", "ACC_CHART_LEVEL", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlAccountLevel.DataSource = ds;
                    ddlAccountLevel.DataValueField = "VALUEID";
                    ddlAccountLevel.DataTextField = "CAPTION";
                    ddlAccountLevel.DataBind();
                }
                ddlAccountLevel.Items.Insert(0, new ListItem("All", ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void BindData()
    {
        pnResult.Visible = true;
        try
        {
            pnResult.Visible = true;
            EventPageSize = 1;
            rptData.DataSource = null;
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(txtSearch.Text.Trim()), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_ACC_ACCHRT", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    GridViewPaging.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0][IPC.TRECORDCOUNT].ToString();
                    
                    rptData.DataSource = ds;
                    rptData.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridViewPaging.SelectPageChoose = "1";
        BindData();
    }

    protected void Search_GridViewPaging_click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void AdvanceSearch_GridViewPaging_click(object sender, EventArgs e)
    {
        Bindata_Advance();
    }

    void Bindata_Advance()
    {
        try
        {
            EventPageSize = 2;
            pnResult.Visible = true;
            rptData.DataSource = null;
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(txtAccNumber.Text.Trim()), Utility.KillSqlInjection(txtAccName.Text.Trim()),
                Utility.KillSqlInjection(ddlClassification.SelectedValue), Utility.KillSqlInjection(ddlBalanceSide.SelectedValue),
                Utility.KillSqlInjection(ddlGroup.SelectedValue),  Utility.KillSqlInjection(ddlAccountLevel.SelectedValue), Utility.KillSqlInjection(txtBacno.Text.Trim()),
                GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_ACC_ACCHRT_AV", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    GridViewPaging.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0][IPC.TRECORDCOUNT].ToString();
                    rptData.DataSource = ds;
                    rptData.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnAdvanceSearch_click(object sender, EventArgs e)
    {
        GridViewPaging.SelectPageChoose = "1";
        Bindata_Advance();
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
                deleteAccountChart(commandArg);
                AutoSwitchSearch();
                break;
        }
        //}
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string[] lst = hdCLMS_SCO_SCO_PRODUCT.Value.ToString().Split('#');
            if (lst.Length < 1 || (lst.Length == 1 && lst[0].ToString().Equals("")))
            {
                lblError.Text = Resources.labels.Selectoneormoretodelete;
                return;
            }
            foreach (string item in lst)
            {
                if (item.Equals("") || item.Equals("on")) continue;
                deleteAccountChart(item);
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
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    public void deleteAccountChart(String id)
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(id) };
            _service.common("SEMS_ACC_ACCHRT_DEL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
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
    protected void btnAdd_New_Click(object sender, EventArgs e)
    {
        // if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        // {
        RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        // }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        
        txtSearch.Text = string.Empty;
        txtAccName.Text = string.Empty;
        txtAccNumber.Text = string.Empty;
        txtBacno.Text = string.Empty;
        ddlAccountLevel.SelectedValue = string.Empty;
        ddlBalanceSide.SelectedValue = string.Empty;
        ddlClassification.SelectedValue = string.Empty;
        ddlGroup.SelectedValue = string.Empty;

        rptData.DataSource = null;
        rptData.DataBind();
        pnResult.Visible = false;
        hdCLMS_SCO_SCO_PRODUCT.Value = string.Empty;
    }
    void AutoSwitchSearch()
    {
        if (EventPageSize == 1)
        {
            BindData();
        }
        if (EventPageSize == 2)
        {
            Bindata_Advance();
        }
        hdCLMS_SCO_SCO_PRODUCT.Value = string.Empty;
    }
}
