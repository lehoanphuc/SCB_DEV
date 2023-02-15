using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSCorpMatrixUserGroup_Widget : WidgetBase
{
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                ddlStatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
                ddlStatus.Items.Add(new ListItem(Resources.labels.conactive, SmartPortal.Constant.IPC.ACTIVE));
                ddlStatus.Items.Insert(0, new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));

                GridViewPaging.Visible = false;
                divResult.Visible = false;
            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvcontractList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvcontractList.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvcontractList.PageIndex * gvcontractList.PageSize) return;

            DataSet ds = new SmartPortal.SEMS.Contract().GetContractHasGroup(Utility.KillSqlInjection(txtContractCode.Text.Trim()), Utility.KillSqlInjection(txtCustName.Text.Trim()), Utility.KillSqlInjection(txtopenPer.Text.Trim()), txtOpenDate.Text.Trim(), txtEndDate.Text.Trim(), "MTR", "CCO", ddlStatus.SelectedValue, Utility.KillSqlInjection(txtlicenseid.Text.Trim()), Utility.KillSqlInjection(txtcustcode.Text.Trim()), string.Empty, gvcontractList.PageSize, gvcontractList.PageIndex * gvcontractList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                gvcontractList.DataSource = ds;
                gvcontractList.DataBind();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
    }
    void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvcontractList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvcontractList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void gvcontractList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblcustName, lblOpen, lblOpendate, lblClosedate, lblcontractType, lblcorpType, lblStatus, lbllicense, lblcustcode;
            LinkButton lbContractNo;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                lbContractNo = (LinkButton)e.Row.FindControl("lbContractNo");
                lblcustName = (Label)e.Row.FindControl("lblcustName");
                lblOpen = (Label)e.Row.FindControl("lblOpen");
                lblOpendate = (Label)e.Row.FindControl("lblOpendate");
                lblClosedate = (Label)e.Row.FindControl("lblClosedate");
                lblcontractType = (Label)e.Row.FindControl("lblcontractType");
                lblcorpType = (Label)e.Row.FindControl("lblcorpType");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lbllicense = (Label)e.Row.FindControl("lbllicense");
                lblcustcode = (Label)e.Row.FindControl("lblcustcode");

                lbContractNo.Text = drv[IPC.CONTRACTNO].ToString();
                lblcorpType.Text = drv[IPC.CONTRACTTYPE].ToString();
                lblcustName.Text = drv[IPC.FULLNAME].ToString();
                lblOpen.Text = drv[IPC.USERCREATE].ToString();
                lblOpendate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["CreateDate"].ToString()).ToString("dd/MM/yyyy");
                lblClosedate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["EndDate"].ToString()).ToString("dd/MM/yyyy");
                lbllicense.Text = drv[IPC.LICENSEID].ToString();
                lblcustcode.Text = drv[IPC.CUSTCODE].ToString();
                lblcontractType.Text = drv["TYPENAME"].ToString();

                switch (drv[IPC.STATUS].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = Resources.labels.connew;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = Resources.labels.condelete;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.conactive;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblStatus.Text = Resources.labels.conblock;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblStatus.Text = Resources.labels.conpending;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblStatus.Text = Resources.labels.conreject;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        lblStatus.Text = Resources.labels.pendingfordelete;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvcontractList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }
}