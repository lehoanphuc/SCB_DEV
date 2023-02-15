using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Text;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Constant;
using SmartPortal.Model;
using SmartPortal.SEMS;

public partial class Widgets_SEMSSchedulePushNotification_Widget : WidgetBase
{
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                btnAdd.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                LoadDll();
                GridViewPaging.Visible = false;
                divResult.Visible = false;
            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvSTV.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvSTV.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    public void LoadDll()
    {
        ddlSendType.Items.Add(new ListItem(Resources.labels.all, IPC.ALL));
        ddlSendType.Items.Add(new ListItem(Resources.labels.Consumer, IPC.CONSUMER));
        //ddlSendType.Items.Add(new ListItem(Resources.labels.agentmerchant, IPC.AGENTMERCHANT));
        ddlSendType.Items.Add(new ListItem(Resources.labels.user, IPC.USER));

        ddlType.Items.Add(new ListItem(Resources.labels.all, IPC.ALL));
        ddlType.Items.Add(new ListItem(Resources.labels.onetime, IPC.ONETIME));
        ddlType.Items.Add(new ListItem(Resources.labels.daily, IPC.DAILY));
        ddlType.Items.Add(new ListItem(Resources.labels.weekly, IPC.WEEKLY));
        ddlType.Items.Add(new ListItem(Resources.labels.monthly, IPC.MONTHLY));

        ddlStatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
        ddlStatus.Items.Add(new ListItem(Resources.labels.active, IPC.ACTIVE));
        ddlStatus.Items.Add(new ListItem(Resources.labels.conpending, IPC.PENDING));
        ddlStatus.Items.Add(new ListItem(Resources.labels.pendingfordelete, SmartPortal.Constant.IPC.PENDINGFORDELETE));
        ddlStatus.Items.Add(new ListItem(Resources.labels.reject, IPC.REJECT));
        ddlStatus.Items.Add(new ListItem(Resources.labels.expired, "Y"));
        ddlStatus.Items.Add(new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
    }
    public void BindData()
    {
        try
        {
            divResult.Visible = true;
            ltrError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvSTV.PageIndex * gvSTV.PageSize) return;
            DataSet ds = new SmartPortal.SEMS.Notification().GetPushNotification(
                Utility.KillSqlInjection(txtNotificationID.Text),
                Utility.KillSqlInjection(txtNotificationName.Text),
                Utility.KillSqlInjection(ddlNotificationType.SelectedValue),
                Utility.KillSqlInjection(ddlType.SelectedValue),
                Utility.KillSqlInjection(ddlSendType.SelectedValue),
                Utility.KillSqlInjection(ddlStatus.SelectedValue), gvSTV.PageSize, gvSTV.PageIndex * gvSTV.PageSize,
                ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvSTV.DataSource = ds;
                gvSTV.DataBind();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ltrError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                ltrError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvSTV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton lbPNName, lbEdit, lbDelete;
            Label lblPNType, lblType, lblSendType, lblstatus, lblFromDate, lblToDate;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                lbPNName = (LinkButton)e.Row.FindControl("lbPNName");
                lblPNType = (Label)e.Row.FindControl("lblPNType");
                lblType = (Label)e.Row.FindControl("lblType");
                lblSendType = (Label)e.Row.FindControl("lblSendType");
                lblstatus = (Label)e.Row.FindControl("lblStatus");
                lblFromDate = (Label)e.Row.FindControl("lblFromDate");
                lblToDate = (Label)e.Row.FindControl("lblToDate");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                cbxSelect.Enabled = true;
                lbPNName.Text = drv[IPC.NAME].ToString();
                lblPNType.Text = drv[IPC.TYPENOTIFY].ToString();
                switch (drv[IPC.SENDTYPE].ToString())
                {
                    case IPC.ALL:
                        lblSendType.Text = Resources.labels.all;
                        break;
                    case IPC.CONSUMER:
                        lblSendType.Text = Resources.labels.Consumer;
                        break;
                    case IPC.AGENTMERCHANT:
                        lblSendType.Text = Resources.labels.agentmerchant;
                        break;
                    case IPC.USER:
                        lblSendType.Text = Resources.labels.user;
                        break;
                }
                lblFromDate.Text = Utility.FormatDatetime(drv[IPC.FROMDATE].ToString(), "dd/MM/yyyy HH:mm");
                lblToDate.Text = Utility.FormatDatetime(drv[IPC.TODATE].ToString(), "dd/MM/yyyy HH:mm");

                switch (drv[IPC.STATUS].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        cbxSelect.Enabled = true;
                        lblstatus.Text = Resources.labels.connew;
                        lblstatus.Attributes.Add("class", "label-success");
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                        {
                            lbEdit.Enabled = false;
                            lbEdit.OnClientClick = string.Empty;
                        }
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                        {
                            lbDelete.Enabled = false;
                            lbDelete.OnClientClick = string.Empty;
                        }
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        cbxSelect.Enabled = true;
                        lblstatus.Text = Resources.labels.conactive;
                        lblstatus.Attributes.Add("class", "label-success");

                        lbEdit.Enabled = false;
                        lbEdit.OnClientClick = string.Empty;
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                        {
                            lbDelete.Enabled = false;
                            lbDelete.OnClientClick = string.Empty;
                        }
                        break;
                    case IPC.PENDING:
                        cbxSelect.Enabled = false;
                        lblstatus.Text = Resources.labels.conpending;
                        lblstatus.Attributes.Add("class", "label-warning");
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                        {
                            lbEdit.Enabled = false;
                            lbEdit.OnClientClick = string.Empty;
                        }
                        lbDelete.Enabled = false;
                        lbDelete.OnClientClick = string.Empty;
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        cbxSelect.Enabled = false;
                        lblstatus.Text = Resources.labels.pendingfordelete;
                        lblstatus.Attributes.Add("class", "label-warning");
                        lbEdit.Enabled = false;
                        lbEdit.OnClientClick = string.Empty;
                        lbDelete.Enabled = false;
                        lbDelete.OnClientClick = string.Empty;
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        cbxSelect.Enabled = false;
                        lblstatus.Text = Resources.labels.conreject;
                        lblstatus.Attributes.Add("class", "label-warning");
                        lbEdit.Enabled = false;
                        lbEdit.OnClientClick = string.Empty;
                        lbDelete.Enabled = false;
                        lbDelete.OnClientClick = string.Empty;
                        break;
                    case "Y":
                        cbxSelect.Enabled = false;
                        lblstatus.Text = Resources.labels.expired;
                        lblstatus.Attributes.Add("class", "label-warning");
                        lbEdit.Enabled = false;
                        lbEdit.OnClientClick = string.Empty;
                        lbDelete.Enabled = false;
                        lbDelete.OnClientClick = string.Empty;
                        break;
                }

                switch (drv[IPC.TYPE].ToString())
                {
                    case IPC.DAILY:
                        lblType.Text = Resources.labels.daily;
                        break;
                    case IPC.WEEKLY:
                        lblType.Text = Resources.labels.weekly;
                        break;
                    case IPC.MONTHLY:
                        lblType.Text = Resources.labels.monthly;
                        break;
                    case IPC.ONETIME:
                        lblType.Text = Resources.labels.onetime;
                        break;
                }
                if (cbxSelect.Enabled)
                {
                    size++;
                }
                hdCounter.Value = "0";
                hdPageSize.Value = size.ToString();
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
    protected void btnAdd_Click(object sender, EventArgs e)
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
            CheckBox cbxDelete;
            LinkButton lbPNName;
            string strPNID = "";
            try
            {
                foreach (GridViewRow gvr in gvSTV.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lbPNName = (LinkButton)gvr.Cells[1].FindControl("lbPNName");
                        strPNID += lbPNName.CommandArgument.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strPNID))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] PNID = strPNID.Split('#');
                    for (int i = 0; i < PNID.Length - 1; i++)
                    {
                        SmartPortal.Common.Log.WriteLog("SEMSPUSHNODELETE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_PUSHNOTIFICATION", "ID='" + PNID[i] + "'");
                        new Notification().DeletePushNotification(PNID[i], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            BindData2();
                            lblError.Text = Resources.labels.deletepushnotificationsuccessfully;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
            }
        }
    }
    protected void gvSTV_RowCommand(object sender, GridViewCommandEventArgs e)
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
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }
    protected void gvSTV_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvSTV.Rows[e.RowIndex].Cells[1].FindControl("lbPNName")).CommandArgument;
            SmartPortal.Common.Log.WriteLog("SEMSPUSHNODELETE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_PUSHNOTIFICATION", "ID='" + commandArg + "'");
            new Notification().DeletePushNotification(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData2();
                lblError.Text = Resources.labels.deletepushnotificationsuccessfully;
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
    void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvSTV.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvSTV.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
