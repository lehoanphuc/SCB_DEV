using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using System.Data;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_Group_View_Widget : WidgetBase
{
    public static bool isSort = false;
    public static bool isAscend = false;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                #region load service
                DataSet ds = new DataSet();

                //GMS : Group management service
                ds = new SmartPortal.SEMS.Services().GetAll("GMS", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtService = new DataTable();
                    dtService = ds.Tables[0];

                    ddlServiceID.DataSource = dtService;
                    ddlServiceID.DataTextField = "SERVICENAME";
                    ddlServiceID.DataValueField = "SERVICEID";
                    ddlServiceID.DataBind();

                    ddlServiceID.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                ddlServiceID.Items.Remove(ddlServiceID.Items.FindByValue(SmartPortal.Constant.IPC.SMS));
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
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvGroup.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvGroup.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    private void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvGroup.PageIndex * gvGroup.PageSize) return;
            DataTable dtCL = new DataTable();
            dtCL = new RoleBLL().LoadForView(Utility.KillSqlInjection(txtSearch.Text.Trim()), ddlServiceID.SelectedValue, gvGroup.PageSize, gvGroup.PageIndex * gvGroup.PageSize);
            gvGroup.DataSource = dtCL;
            gvGroup.DataBind();
            if (dtCL.Rows.Count > 0)
            {
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dtCL.Rows[0]["TRECORDCOUNT"].ToString();
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
    protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblRoleID, lblRoleDescription, lblDateCreated, lblAuthor, lblService, lblStatus;
            LinkButton lbRoleName, lbEdit, lbDelete;
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
                lbRoleName = (LinkButton)e.Row.FindControl("lbRoleName");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
                lblRoleID = (Label)e.Row.FindControl("lblRoleID");
                lblDateCreated = (Label)e.Row.FindControl("lblDateCreated");
                lblAuthor = (Label)e.Row.FindControl("lblAuthor");
                lblService = (Label)e.Row.FindControl("lblService");
                lblRoleDescription = (Label)e.Row.FindControl("lblRoleDescription");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblRoleID.Text = drv["RoleID"].ToString();
                lbRoleName.Text = drv["RoleName"].ToString();
                lblDateCreated.Text = Utility.FormatDatetime(SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["DateCreated"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.DateMMM);
                lblAuthor.Text = drv["UserCreated"].ToString();
                lblRoleDescription.Text = drv["RoleDescription"].ToString();
                lblRoleDescription.Text = drv["RoleDescription"].ToString();
                switch (drv["Status"].ToString())
                {
                    case "1":
                        lblStatus.Text = Resources.labels.active;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case "0":
                        lblStatus.Text = Resources.labels.inactive;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                }
                switch (drv["ServiceID"].ToString())
                {
                    case IPC.EW:
                        lblService.Text = Resources.labels.walletbanking;
                        break;
                    case IPC.MB:
                        lblService.Text = Resources.labels.mobilebanking;
                        break;
                    case IPC.SMS:
                        lblService.Text = Resources.labels.smsbanking;
                        break;
                    case IPC.IB:
                        lblService.Text = Resources.labels.internetbanking;
                        break;
                    case IPC.SEMS:
                        lblService.Text = Resources.labels.SEMS;
                        break;
                    case IPC.AM:
                        lblService.Text = Resources.labels.agentmerchant;
                        break;
                    default:
                        break;
                }
                DataTable dtRole = new RoleBLL().LoadForUser(Session["userName"].ToString().Trim(), IPC.SEMS);
                bool action = false;
                foreach (DataRow dr in dtRole.Rows)
                {
                    if (dr["ROLEID"].ToString().Trim().Equals("2") && dr["CHECKED"].ToString().Trim().Equals("1"))
                    {
                        action = true;
                    }
                }
                if (Session["userName"] != null)
                {

                    if (Session["userName"].ToString().Trim() == drv["UserCreated"].ToString().Trim() || action)
                    {
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
                    }
                    else
                    {
                        cbxSelect.Enabled = false;
                        lbEdit.Enabled = false;
                        lbEdit.OnClientClick = string.Empty;
                        lbDelete.Enabled = false;
                        lbDelete.OnClientClick = string.Empty;
                    }
                }
                else
                {
                    cbxSelect.Enabled = false;
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
                    lbDelete.Enabled = false;
                    lbDelete.OnClientClick = string.Empty;
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Group_View_Widget", "gvUser_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void lbAddPage_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }
    protected void lbDeleteSelected_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            CheckBox cbxDelete;
            Label lblRoleID;
            string strRoleID = "";
            try
            {
                foreach (GridViewRow gvr in gvGroup.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lblRoleID = (Label)gvr.Cells[1].FindControl("lblRoleID");
                        strRoleID += lblRoleID.Text.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strRoleID))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] roleID = strRoleID.Split('#');
                    for (int i = 0; i < roleID.Length - 1; i++)
                    {
                        string errorCode = string.Empty;
                        RoleBLL MB = new RoleBLL();
                        errorCode = MB.Delete(Utility.IsInt(roleID[i]));
                        if (errorCode == "0" || errorCode == "-1")
                        {
                            BindData2();
                            lblError.Text = Resources.labels.deletegroupsuccessfully;
                        }
                        else
                        {
                            errorCode = IPC.ERRORCODE.GROUPEXISTS;
                            ErrorCodeModel EM = new ErrorCodeModel();
                            EM = new ErrorBLL().Load(Utility.IsInt(errorCode), System.Globalization.CultureInfo.CurrentCulture.ToString());
                            lblError.Text = EM.ErrorDesc;
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
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        BindData2();
    }
    protected void gvGroup_RowCommand(object sender, GridViewCommandEventArgs e)
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
    protected void gvGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string errorCode = string.Empty;
            string commandArg = ((Label)gvGroup.Rows[e.RowIndex].Cells[1].FindControl("lblRoleID")).Text;
            RoleBLL MB = new RoleBLL();
            errorCode = MB.Delete(Utility.IsInt(commandArg));
            if (errorCode == "0" || errorCode == "-1")
            {
                BindData2();
                lblError.Text = Resources.labels.deletegroupsuccessfully;
            }
            else
            {
                errorCode = IPC.ERRORCODE.GROUPEXISTS;
                ErrorCodeModel EM = new ErrorCodeModel();
                EM = new ErrorBLL().Load(Utility.IsInt(errorCode), System.Globalization.CultureInfo.CurrentCulture.ToString());
                lblError.Text = EM.ErrorDesc;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvGroup.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvGroup.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
