using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Constant;

public partial class Widgets_SEMSDepartment_Widget : WidgetBase
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
            if (flag == 1)
            {
                GridViewPaging.pagingClickArgs += new EventHandler(Search_GridViewPaging_click);
            }
            if (flag == 0)
            {
                GridViewPaging.pagingClickArgs += new EventHandler(AdvanceSearch_GridViewPaging_click);
            }
            lblError.Text = string.Empty;
            //if (!IsPostBack)
            //{
            //    BindData();
            //}
            loadCombobox();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    private void loadStatus()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("WAL_DEPARTMENT", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
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
        loadStatus();
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
            object[] searchObject = new object[] { txtSearch.Text.Trim(), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _common.common("SEMS_DEPART_SEARCH", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
     
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

    protected void btnClear_click(object sender, EventArgs e)
    {
        txtSearch.Text = string.Empty;
        txtBranch.Value = string.Empty;
        txtBranch.Text = string.Empty;
        txtDepartmentCode.Text = string.Empty;
        txtDepartmentName.Text = string.Empty;
        ddStatus.SelectedValue = string.Empty;
        rptData.DataSource = null;
        rptData.DataBind();
        p.Visible = false;
    }


    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        //{
        //    RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        //}
        RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
    }
    void BindData_SearchAdvance()
    {
        try
        {
            p.Visible = true;
            rptData.DataSource = null;
            flag = 0;
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { txtDepartmentCode.Text.Trim(), txtDepartmentName.Text.Trim(), txtBranch.getId(), ddStatus.SelectedValue, GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _common.common("SEMS_DEPART_ADVANCE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
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
                deleteFunction(commandArg);
                BindData();

                break;
        }
        //}
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        //{
        string[] lst = hdCLMS_SCO_SCO_PRODUCT.Value.ToString().Split('#');
        if (lst.Length == 1)
        {
            foreach (string item in lst)
            {
                if (item.Equals("") || item.Equals("or"))
                {
                    lblError.Text = Resources.labels.Selectoneormoretodelete;
                    return;
                }
            }
        }
        if (lst.Length < 1)
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
                BindData();
                return;
            }
        }
        if (IPCERRORCODE.Equals("0"))
        {
            lblError.Text = Resources.labels.deletesuccessfully;
        }
        BindData();
        if (rptData.Items.Count == 0)
        {
            int SelectPageNo = int.Parse(GridViewPaging.SelectPageChoose.ToString()) - 1;
            GridViewPaging.SelectPageChoose = SelectPageNo.ToString();
            BindData();
        }
        //}
    }
    public void deleteFunction(string departID)
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { departID };
            _common.common("SEMS_DEPART_DEL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

}