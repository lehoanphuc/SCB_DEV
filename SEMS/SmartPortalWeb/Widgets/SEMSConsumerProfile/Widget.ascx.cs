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
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void loadCombobox_Gender()
    {
       
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("GENDER", "SYS", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlGender.DataSource = ds;
                    ddlGender.DataValueField = "VALUEID";
                    ddlGender.DataTextField = "CAPTION";
                    ddlGender.DataBind();
                    ddlGender.Items.Insert(0, new ListItem("All", string.Empty));
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_Nation()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] loadNation = new object[] { string.Empty, string.Empty, null, null };
            ds = _service.common("WAL_BO_GET_NATION", loadNation, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlNationality.DataSource = ds;
                    ddlNationality.DataValueField = "NationCode";
                    ddlNationality.DataTextField = "NationName";
                    ddlNationality.DataBind();
                    ddlNationality.Items.Insert(0, new ListItem("All", string.Empty));
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        
    }
    private void loadCombobox_Status()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("EBA_CustInfo", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlStatus.DataSource = ds;
                    ddlStatus.DataValueField = "VALUEID";
                    ddlStatus.DataTextField = "CAPTION";
                    ddlStatus.DataBind();
                    ddlStatus.Items.Insert(0, new ListItem("All", string.Empty));
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

        loadCombobox_Gender();
        loadCombobox_Nation();
        loadCombobox_Status();
    }
    void BindData()
    {
        pnResult.Visible = true;
        try
        {
            rptData.DataSource = null;
            rptData.DataBind();
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(txtSearch.Text.Trim()),  GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds =  _service.common("SEMS_CON_PROFILE_SEA", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
            {
                if (ds.Tables[0].Rows.Count < 1 & GridViewPaging.pageIndex > 0)
                {
                    GridViewPaging.SelectPageChoose = "1";
                    BindData();
                }
                else
                {
                    countRow  = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0][IPC.TRECORDCOUNT].ToString();
                    rptData.DataSource = ds.Tables[0];
                    rptData.DataBind();
                }
            }
            GridViewPaging.total = countRow;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        EventPageSize = 1;
        rptData.DataSource = null;
        rptData.DataBind();
        BindData();
        GridViewPaging.SelectPageChoose = "1";
    }

    private void Bindata_AdvanceSearch()
    {
        try
        {
            rptData.DataSource = null;
            rptData.DataBind();
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(txtPhoneNumber.Text.Trim()),
                Utility.KillSqlInjection(ddlGender.SelectedValue),
                Utility.KillSqlInjection(txtPaperNumber.Text.Trim()),
                Utility.KillSqlInjection(ddlNationality.SelectedValue),
                Utility.KillSqlInjection(txtFullName.Text.Trim()),
                Utility.KillSqlInjection(ddlStatus.SelectedValue),
                GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_CON_PROFILE_ADV", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
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
                    rptData.DataSource = ds.Tables[0];
                    rptData.DataBind();
                }
            }
            GridViewPaging.total = countRow;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
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
        pnResult.Visible = true;
        EventPageSize = 2;
        Bindata_AdvanceSearch();
    }

    protected void btnAdvanceSearch_click(object sender, EventArgs e)
    {
        pnResult.Visible = true;
        EventPageSize = 2;
        GridViewPaging.SelectPageChoose = "1";
        Bindata_AdvanceSearch();
    }

   

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
            switch (commandName)
            {
                case IPC.ACTIONPAGE.EDIT:
                     RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.DELETE:
                    break;
            }
    }
    public void setDefaultDataSearch()
    {
        rptData.DataSource = null;
        rptData.DataBind();
        GridViewPaging.Visible = false;
        pnResult.Visible = false;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtSearch.Text = String.Empty;
        txtPaperNumber.Text = String.Empty;
        txtPhoneNumber.Text = String.Empty;
        txtFullName.Text = String.Empty;
        loadCombobox();
        setDefaultDataSearch();
    }
}
