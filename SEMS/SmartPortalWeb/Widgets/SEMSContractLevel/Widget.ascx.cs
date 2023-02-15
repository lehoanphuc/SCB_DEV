using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Constant;

public partial class Widgets_SEMSContracLevel_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.ContractLevel _service = new SmartPortal.SEMS.ContractLevel();
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
                loadComboBox();
            }
            GridViewPaging.pagingClickArgs += new EventHandler(btnSearch_Click);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    void loadComboBox()
    {
        // Save list status contract level in cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_ContractLevel_STT"];
            if (ds == null)
            {
                ds = _common.GetValueList("STATUS", "WAL_CONTRACT_LEVEL", ref IPCERRORCODE, ref IPCERRORDESC);
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
                //Save cache 10 min, after Wallet_ContractLevel_STT = null
                Cache.Insert("Wallet_ContractLevel_STT", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                ddStatus.Items.Insert(0, new ListItem("All", string.Empty));
            }
            else
            {
                ddStatus.DataSource = (DataSet)Cache["Wallet_ContractLevel_STT"];
                ddStatus.DataValueField = "VALUEID";
                ddStatus.DataTextField = "CAPTION";
                ddStatus.DataBind();
                ddStatus.Items.Insert(0, new ListItem("All", string.Empty));
            }
        }
        catch (Exception ex)
        {

        }
    }

    void loadData()
    {
        pnResult.Visible = true;
        GridViewPaging.Visible = true;
        rptData.DataSource = null;
        DataSet ListContractLevel = new DataSet();
        ListContractLevel = _service.SearchAllContractLevel(txtContractLevelCode.Text.Trim(), txtContractLevelName.Text.Trim(), ddStatus.SelectedValue.Trim(), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
        {
            if (ListContractLevel.Tables[0].Rows.Count < 1)
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
                rptData.DataSource = ListContractLevel;
                rptData.DataBind();
                GridViewPaging.total = ListContractLevel.Tables[0].Rows.Count == 0 ? "0" : ListContractLevel.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
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
            loadComboBox();
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
            string[] lst = hdContractLevel.Value.ToString().Split('#');
            if (lst.Length == 1)
            {
                foreach (string item in lst)
                {
                    if(item.Equals("") || item.Equals("or"))
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
                if (item.Equals("") || item.Equals("on"))continue;
                deleteItem(item);
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
                hdContractLevel.Value = string.Empty;
                BindData();
            }
        }
    }


    public void deleteItem(String id)
    {
        try
        {
            DataSet ds = new DataSet();
            object[] _object = new object[] { id };
            ds = _service.DeleteContractLevel(id, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                lblError.Text = Resources.labels.deletesuccessfully;
                return;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
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
                deleteItem(commandArg);
                BindData();
                break;
        }
        }
    }

    protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // If the Repeater contains no data.
        //if (rptData.Items.Count < 1)
        //{
            //if (e.Item.ItemType == ListItemType.Footer)
            //{
            //    Label lblFooter = (Label)e.Item.FindControl("lblErrorMsg");
            //    lblFooter.Visible = true;
            //}
        //}

    }
}