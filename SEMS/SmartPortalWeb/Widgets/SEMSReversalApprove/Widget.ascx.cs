using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSReversalApprove_Widget : WidgetBase
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
            }
            if(m=="Search")
            {
                GridViewPaging.pagingClickArgs += new EventHandler(btnSearch_Click);
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
            object[] searchObject = new object[] { Utility.KillSqlInjection(txtrequestID.Text.Trim()), Utility.KillSqlInjection(txtTranid.Text.Trim()), Utility.KillSqlInjection(ddStatus.SelectedValue), Utility.KillSqlInjection(txtFrom.Text.Trim()), Utility.KillSqlInjection(txtTo.Text.Trim()) ,GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_REVERSAL_ADV", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
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
            ds = _service.GetValueList("Status", "Wal_Reversal", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddStatus.DataSource = ds;
                    ddStatus.DataValueField = "VALUEID";
                    ddStatus.DataTextField = "CAPTION";
                    ddStatus.DataBind();
                    ddStatus.Items.Insert(0, new ListItem("All", string.Empty));
                    ddStatus.SelectedValue = "P";
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
        m = "Search";
        BindData();
    }
    protected void btnClear_click(object sender, EventArgs e)
    {
        txtrequestID.Text = "";
        txtTo.Text = "";
        txtFrom.Text = "";
        loadComboboxStatus();
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
                case IPC.ACTIONPAGE.REJECT:
                    RedirectToActionPage(IPC.ACTIONPAGE.APPROVE, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }
}