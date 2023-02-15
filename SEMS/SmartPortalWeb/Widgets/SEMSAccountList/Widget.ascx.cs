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
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void loadCombobox_Module()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("MODULE", "ACT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModuleName.DataSource = ds;
                    ddlModuleName.DataValueField = "VALUEID";
                    ddlModuleName.DataTextField = "CAPTION";
                    ddlModuleName.DataBind();
                }
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

        loadCombobox_Module();
    }
    void BindData()
    {
        try
        {
            pnResult.Visible = true;
            EventPageSize = 1;
            rptData.DataSource = null;
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(txtSearch.Text.Trim() ),  GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds =  _service.common("SEMS_ACGRPDEFDTL_SER", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
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
    protected void Search_GridViewPaging_click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridViewPaging.SelectPageChoose = "1";
        BindData();
    }
    protected void btnClear_click(object sender, EventArgs e)
    {
        txtDescription.Text = String.Empty;
        txtSearch.Text = String.Empty;
        txtSysAccName.Text = String.Empty;
        loadCombobox();
        setDefaultDataSearch();
        hdCLMS_SCO_SCO_PRODUCT.Value = string.Empty;
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

    void BindData_SearchAdvance() 
    {
        try
        {
            pnResult.Visible = true;
            EventPageSize = 2;
            rptData.DataSource = null;
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(ddlModuleName.SelectedValue), Utility.KillSqlInjection(txtSysAccName.Text.Trim()), Utility.KillSqlInjection(txtDescription.Text.Trim()), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_ACGRPDEFDTL_SAV", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
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
                string _module = commandArg.ToString().Split('+')[0].ToString();
                string _acname = commandArg.ToString().Split('+')[1].ToString();
                deleteModuleAccountlist(_module, _acname);
                AutoSwitchSearch();
                break;
            }
        //}
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string[] lst = hdCLMS_SCO_SCO_PRODUCT.Value.ToString().Split('#');
        if(lst.Length <= 1)
        {
            lblError.Text = Resources.labels.Selectoneormoretodelete;
            return;
        }
        foreach (string item in lst)
        {
            if (item.Equals("") || item.Equals("on")) continue;
            string _module = item.ToString().Split('+')[0].ToString();
            string _acname = item.ToString().Split('+')[1].ToString();
            deleteModuleAccountlist(_module, _acname);
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
        //hdCLMS_SCO_SCO_PRODUCT.Value = string.Empty;
    }

    public void deleteModuleAccountlist( string module , string acname)
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(module) , Utility.KillSqlInjection(acname) };
            _service.common("SEMS_ACGRPDEFDTL_DEL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                lblError.Text = Resources.labels.deletesuccessfully;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
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
    public void setDefaultDataSearch()
    {
        pnResult.Visible = false;
        rptData.DataSource = null;
        rptData.DataBind();
        GridViewPaging.Visible = false;
    }
    void AutoSwitchSearch()
    {
        if (EventPageSize == 1)
        {
            BindData();
        }
        if (EventPageSize == 2)
        {
            BindData_SearchAdvance();
        }
        hdCLMS_SCO_SCO_PRODUCT.Value = string.Empty;
    }
}
