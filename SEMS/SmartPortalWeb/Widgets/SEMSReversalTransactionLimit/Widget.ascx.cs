using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSContracLevel_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAdd_New.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
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

        try
        {
            //load tran app
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { };
            ds = _service.common("SEMSTRANCODEREVERSAL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dtTranApp = new DataTable();
                    dtTranApp = ds.Tables[0];

                    ddlTransactionType.DataSource = dtTranApp;
                    ddlTransactionType.DataTextField = "PAGENAME";
                    ddlTransactionType.DataValueField = "TRANCODE";
                    ddlTransactionType.DataBind();

                    ddlTransactionType.Items.Insert(0, new ListItem(Resources.labels.all, ""));
                    GridViewPaging.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = IPCERRORDESC;
        }
    }

    void loadData()
    {
        pnResult.Visible = true;
        GridViewPaging.Visible = true;
        rptData.DataSource = null;
        string textlimitTMP = string.Empty;
        string isunlimittMP = string.Empty;
        if (checktxtlimit.Checked == true)
        {
            textlimitTMP = string.Empty;
            isunlimittMP = "1";
        }
        else
        {
            textlimitTMP = txtLimit.Text;
            isunlimittMP = "0";
        }
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { Utility.KillSqlInjection(ddlTransactionType.SelectedValue), Utility.KillSqlInjection(textlimitTMP.Trim()), Utility.KillSqlInjection(isunlimittMP.Trim()), Utility.KillSqlInjection(ddlUnit.SelectedValue), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
        ds = _service.common("SEMS_RRT_TRAN_VIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        try
        {
            if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
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
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["IsReversal"].ToString() == "No")
                            ds.Tables[0].Rows[i]["LimitRR"] = "";
                        else
                        {
                            if (ds.Tables[0].Rows[i]["LimitRR"].ToString() == "-1")
                                ds.Tables[0].Rows[i]["LimitRR"] = "Unlimited";

                        }
                    }
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
        catch (Exception ex)
        {
            lblError.Text = IPCERRORDESC;

        }
    }
    protected void checktxtlimit_OnCheckedChanged(object sender, EventArgs e)
    {

        if (checktxtlimit.Checked)
        {
            txtLimit.Text = null;
            txtLimit.Enabled = false;

        }
        else
        {
            txtLimit.Text = null;
            txtLimit.Enabled = true;
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
        hdReversalLimit.Value = string.Empty;
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
            string[] lst = hdReversalLimit.Value.ToString().Split('#');
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
                hdReversalLimit.Value = string.Empty;
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
            ds = _service.common("SEMS_RRT_LIMIT_DEL", _object, ref IPCERRORCODE, ref IPCERRORDESC);
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