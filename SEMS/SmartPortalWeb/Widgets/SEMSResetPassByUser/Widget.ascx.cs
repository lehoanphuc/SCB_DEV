using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSProduct_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            litError.Text = string.Empty;
            if (!IsPostBack)
            {
                txtFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                #region load service
                DataSet ds = new DataSet();
                ds = new SmartPortal.SEMS.Services().GetAll("GMS", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtService = new DataTable();
                    dtService = ds.Tables[0];

                    ddlService.DataSource = dtService;
                    ddlService.DataTextField = "SERVICENAME";
                    ddlService.DataValueField = "SERVICEID";
                    ddlService.DataBind();
                    ddlService.Items.Remove(ddlService.Items.FindByValue(SmartPortal.Constant.IPC.PHO));
                    ddlService.Items.Remove(ddlService.Items.FindByValue(SmartPortal.Constant.IPC.SMS));
                    ddlService.Items.Remove(ddlService.Items.FindByValue(SmartPortal.Constant.IPC.SEMS));
                    ddlService.Items.Insert(0, new ListItem("All", "ALL"));
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                #endregion
                #region hien thị status
                ddlStatus.Items.Add(new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                ddlStatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
                ddlStatus.Items.Add(new ListItem(Resources.labels.conactive, SmartPortal.Constant.IPC.ACTIVE));
                ddlStatus.Items.Add(new ListItem(Resources.labels.conreject, SmartPortal.Constant.IPC.REJECT));
                ddlStatus.SelectedValue = SmartPortal.Constant.IPC.NEW;
                #endregion
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
    void BindData()
    {
        try
        {
            divResult.Visible = true;
            DataSet dtCL = new DataSet();
            dtCL = new SmartPortal.SEMS.User().GetListResetPassByUser(Utility.KillSqlInjection(txtName.Text.Trim()), string.Empty, String.Empty
                , Utility.KillSqlInjection(ddlService.SelectedValue.Trim()), string.Empty, Utility.KillSqlInjection(ddlType.SelectedValue.Trim())
                , Utility.KillSqlInjection(ddlStatus.SelectedValue), txtFrom.Text.Trim(), txtTo.Text.Trim(), gvList.PageSize, gvList.PageIndex * gvList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvList.DataSource = dtCL;
                gvList.DataBind();
            }
            else
            {
                //lblError.Text = IPCERRORDESC;
            }

            if (dtCL.Tables[0].Rows.Count > 0)
            {
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dtCL.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
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
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            LinkButton lblUserID;
            Label lblName;
            Label lblNRIC;
            Label lblEmail;
            Label lblPhone;
            Label lblStatus;
            Label lblSource;
            LinkButton lbEdit;

            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                lblUserID = (LinkButton)e.Row.FindControl("lblUserID");
                lblName = (Label)e.Row.FindControl("lblName");
                lblNRIC = (Label)e.Row.FindControl("lblNRIC");
                lblEmail = (Label)e.Row.FindControl("lblEmail");
                lblPhone = (Label)e.Row.FindControl("lblPhone");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblSource = (Label)e.Row.FindControl("lblSource");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");

                lblUserID.Text = drv["UserID"] is DBNull ? "" : drv["UserID"].ToString();
                lblName.Text = drv["Name"] is DBNull ? "" : drv["Name"].ToString();
                lblEmail.Text = drv["Email"] is DBNull ? "" : drv["Email"].ToString();
                lblNRIC.Text = drv["NRIC"] is DBNull ? "" : drv["NRIC"].ToString();
                lblPhone.Text = drv["PhoneNumber"] is DBNull ? "" : drv["PhoneNumber"].ToString();
                lblSource.Text = drv["SourceID"] is DBNull ? "" : drv["SourceID"].ToString();

                switch (drv["Status"].ToString().Trim())
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
                    case SmartPortal.Constant.IPC.REJECT:
                        lblStatus.Text = Resources.labels.conreject;
                        lblStatus.Attributes.Add("class", "label-warning");

                        break;
                }
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE))
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvList.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtFrom.Text) || string.IsNullOrEmpty(txtTo.Text))
            {
                ShowPopUpMsg(Resources.labels.FromDateAndToDateCanNotBeEpty);
            }
            else
            {
                DateTime dateFromConvert = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", null);
                DateTime dateToConvert = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", null);
                if (dateFromConvert > dateToConvert)
                {
                    ShowPopUpMsg(Resources.labels.ToDateMustBeGreaterThanFromDate);
                }
                else
                {
                    ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
                    ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
                    gvList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
                    string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
                    gvList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
                    BindData();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void ShowPopUpMsg(string msg)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
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
