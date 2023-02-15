using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSContracLevel_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _common = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAdd_New.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                //BindData();
                loadCombobox();
            }
            GridViewPaging.pagingClickArgs += new EventHandler(btnSearch_Click);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    void loadComboboxStatus()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("STATUS", "WAL_CONTRACT_LEVEL", ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddStatus.DataSource = ds;
            ddStatus.DataValueField = "VALUEID";
            ddStatus.DataTextField = "CAPTION";
            ddStatus.DataBind();
            ddStatus.Items.Insert(0, new ListItem("All", string.Empty));
        }
    }
    private void loadCombobox_SubUserType()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { };
            ds = _common.common("SEMSLoadSubUserType", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddSubUserType.DataSource = ds;
                        ddSubUserType.DataValueField = "SubUserCode";
                        ddSubUserType.DataTextField = "SubUserType";
                        ddSubUserType.DataBind();
                        ddSubUserType.Items.Insert(0, new ListItem("All", string.Empty));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_contractlevelID()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { };
            ds = _common.common("SEMSLoadContractLV", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddLevelID.DataSource = ds;
                        ddLevelID.DataValueField = "ContractLevelID";
                        ddLevelID.DataTextField = "ContractLevelName";
                        ddLevelID.DataBind();
                        ddLevelID.Items.Insert(0, new ListItem("All", ""));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void loadCombobox()
    {
        loadCombobox_SubUserType();
        loadComboboxStatus();
        loadCombobox_contractlevelID();
    }

    void loadData()
    {
        pnResult.Visible = true;
        GridViewPaging.Visible = true;
        rptData.DataSource = null;
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { Utility.KillSqlInjection(ddLevelID.SelectedValue), Utility.KillSqlInjection(ddStatus.SelectedValue), Utility.KillSqlInjection(ddSubUserType.SelectedValue), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
        ds = _common.common("SEMS_CONTRACTLVLDTL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0" || IPCERRORDESC.Equals(""))
        {
            if (ds.Tables[0].Rows.Count < 1)
            {
                if (GridViewPaging.pageIndex > 0)
                {
                    GridViewPaging.SelectPageChoose = (int.Parse(GridViewPaging.SelectPageChoose) - 1).ToString();
                    loadData();
                }
                else
                {
                    litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                    rptData.Visible = false;
                    GridViewPaging.Visible = false;
                }
            }
            else
            {
                rptData.Visible = true;
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                rptData.DataSource = ds;
                rptData.DataBind();
                GridViewPaging.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
        }
        else
        {
            lblError.Text = IPCERRORDESC;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Session["search"] = "true";
        hdContractLevel.Value = string.Empty;
        loadData();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            try
            {
                string[] lst = hdContractLevel.Value.ToString().Split('#');
                if (lst.Length <= 1)
                {
                    lblError.Text = Resources.labels.Selectoneormoretodelete;
                    return;
                }
                foreach (string item in lst)
                {
                    if (item.Equals("") || item.Equals("on")) continue;
                    string id = item.ToString().Split('+')[0].ToString();
                    string subcode = item.ToString().Split('+')[1].ToString();
                    deleteItem(id, subcode);
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
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

            }
        }
        else
        {
            lblError.Text = "User does not have permission.";
            return;
        }
    }


    public void deleteItem(string id, string subcode)
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(id), Utility.KillSqlInjection(subcode) };
            _common.common("SEMS_CONTRACTLVLDEL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData();
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

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        string id = commandArg.ToString().Split('+')[0].ToString();
        string subcode = commandArg.ToString().Split('+')[1].ToString();

        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.DELETE:
                    deleteItem(id, subcode);
                    BindData();
                    break;
            }
        }
        else
        {
            lblError.Text = "User does not have permission.";
            return;
        }
    }

    protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
    }
}