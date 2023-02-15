using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSDocumentManagement_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    String countRow = "0";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public static string m ="Search";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                loadComboboxStatus();
                loadCustomerType();
            }
            if(m=="Search")
            {
                GridViewPaging.pagingClickArgs += new EventHandler(btnSearch_Click);
            }
            else if(m=="AdvanSearch")
            {
                GridViewPaging.pagingClickArgs += new EventHandler(btnAdvanceSearch_click);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
  void BindData()
    {
        try
        {
            pnResult.Visible = true;
            DataSet ds = new DataSet();
            rptData.DataSource = null;
            object[] searchObject = new object[] { Utility.KillSqlInjection(txtSearch.Text.Trim()), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_BO_SEARCH_DOC", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
            {
                if (ds.Tables[0].Rows.Count < 1)
                {
                    if (GridViewPaging.pageIndex > 0)
                    {
                        GridViewPaging.SelectPageChoose = (int.Parse(GridViewPaging.SelectPageChoose) - 1).ToString();
                        BindData();
                    }
                    else
                    {
                        rptData.DataSource = ds;
                        rptData.DataBind();
                    }
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


    void loadComboboxStatus()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("WAL_DOCUMENT", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
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

    void loadCustomerType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("EBA_Contract", "CTT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCustomerType.DataSource = ds;
                    ddlCustomerType.DataValueField = "VALUEID";
                    ddlCustomerType.DataTextField = "CAPTION";
                    ddlCustomerType.DataBind();
                    ddlCustomerType.Items.Insert(0, new ListItem("All", string.Empty));
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
        m = "AdvanSearch";
        lblError.Text = string.Empty;
        try
        {
            pnResult.Visible = true;
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(txtPhoneNumber.Text.Trim()), Utility.KillSqlInjection(txtPaperNumber.Text.Trim()), Utility.KillSqlInjection(txtFullName.Text.Trim()), Utility.KillSqlInjection(ddlCustomerType.SelectedValue),
                Utility.KillSqlInjection(txtCreatedDate.Text), Utility.KillSqlInjection(txtLastModifiedDate.Text), Utility.KillSqlInjection(ddlStatus.SelectedValue), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_BO_SEARCHAD_DOC", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
            {
                if (ds.Tables[0].Rows.Count < 1 & GridViewPaging.pageIndex > 0)
                {
                    GridViewPaging.SelectPageChoose = (int.Parse(GridViewPaging.SelectPageChoose) - 1).ToString();
                    btnAdvanceSearch_click(sender,e);
                    
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
        m = "Search";
        BindData();
    }
    protected void btnClear_click(object sender, EventArgs e)
    {
        txtPhoneNumber.Text = "";
        txtPaperNumber.Text = "";
        txtFullName.Text = "";
        txtSearch.Text = "";
        txtCreatedDate.Text = string.Empty;
        txtLastModifiedDate.Text = string.Empty;
        loadComboboxStatus();
        loadCustomerType();
        pnResult.Visible = false;
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
            }
        }
    }
}