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

public partial class Widgets_User_View_Widget : WidgetBase
{
    public static bool isSort = false;
    public static bool isAscend = false;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                wgSearchLetter.url = "Default.aspx?p=175";
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAdd.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                //load chi nhánh
                ddlBranch.DataSource = new SmartPortal.SEMS.Branch().GetAll(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlBranch.DataTextField = "BRANCHNAME";
                ddlBranch.DataValueField = "BRANCHID";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));
                GridViewPaging.Visible = false;
                divResult.Visible = false;
                DataTable dtRole = new RoleBLL().LoadForUser(Session["userName"].ToString().Trim(), IPC.SEMS);

                if (dtRole.Select("checked = '1' and roleid = '2'").Length > 0)
                {
                    hfisSupperAdmin.Value = "1";
                }
                if (GetParamsPage("letter")[0] != null)
                {
                    BindData();
                }
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
        gvUser.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvUser.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    private void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvUser.PageIndex * gvUser.PageSize) return;
            string letter = string.Empty;
            if (GetParamsPage("letter")[0] != null)
            {
                letter = GetParamsPage("letter")[0].Trim();
            }
            DataTable dtCL = new DataTable();
            dtCL = new UsersBLL().Search(Utility.KillSqlInjection(txtSearch.Text.Trim()), letter, ddlBranch.SelectedValue.Trim(), gvUser.PageSize, gvUser.PageIndex * gvUser.PageSize);

            gvUser.DataSource = dtCL;
            gvUser.DataBind();

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
            Label lblFirstName, lblDateCreated, lblAuthor, lblLastLoginTime, lblPublish;
            LinkButton lbUserName, lbEdit, lbDelete;
            DataRowView drv;
            bool isSupperAdmin;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                isSupperAdmin = false;
                drv = (DataRowView)e.Row.DataItem;
                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                lbUserName = (LinkButton)e.Row.FindControl("lbUserName");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
                lblPublish = (Label)e.Row.FindControl("lblPublish");
                lblDateCreated = (Label)e.Row.FindControl("lblDateCreated");
                lblAuthor = (Label)e.Row.FindControl("lblAuthor");
                lblFirstName = (Label)e.Row.FindControl("lblFirstName");
                lblLastLoginTime = (Label)e.Row.FindControl("lblLastLoginTime");
                lbUserName.Text = drv["UserName"].ToString();
                lblDateCreated.Text = Utility.FormatDatetime(drv["DateCreated"].ToString(), "dd/MM/yyyy HH:mm");
                lblAuthor.Text = drv["UserCreated"].ToString();
                lblFirstName.Text = drv["FirstName"].ToString();
                lblLastLoginTime.Text = Utility.FormatDatetime(drv["LastLoginTime"].ToString(), "dd/MM/yyyy HH:mm");
                switch (drv["Status"].ToString())
                {
                    case "1":
                        lblPublish.Text = Resources.labels.active;
                        lblPublish.Attributes.Add("class", "label-success");
                        break;
                    case "0":
                        lblPublish.Text = Resources.labels.inactive;
                        lblPublish.Attributes.Add("class", "label-warning");
                        break;
                }
                if (Session["userName"] != null&&  hfisSupperAdmin.Value.Equals("0"))
                {
                    if (Session["userName"].ToString().Trim() == drv["UserCreated"].ToString().Trim())
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
                if (cbxSelect.Enabled)
                {
                    size++;
                }
                hdCounter.Value = "0";
                hdPageSize.Value = size.ToString();
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_User_View_Widget", "gvUser_RowDataBound", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_User_View_Widget", "gvUser_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        BindData2();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            CheckBox cbxDelete;
            LinkButton lbUserName;
            string strUserName = "";
            try
            {
                foreach (GridViewRow gvr in gvUser.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lbUserName = (LinkButton)gvr.Cells[1].FindControl("lbUserName");
                        strUserName += lbUserName.Text.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strUserName))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] userName = strUserName.Split('#');
                    for (int i = 0; i < userName.Length - 1; i++)
                    {
                        new SmartPortal.SEMS.User().DeleteUserTeller(userName[i], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            BindData2();
                            lblError.Text = Resources.labels.deleteusersuccessfully;
                        }
                        else
                        {
                            string errorCode = string.Empty;
                            ErrorCodeModel EM = new ErrorCodeModel();
                            if (IPCERRORDESC == "110211")
                            {
                                errorCode = IPC.ERRORCODE.ACTIVEUSER;
                            }
                            else
                            {
                                errorCode = IPC.ERRORCODE.IPC;
                            }
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
    protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
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
    protected void gvUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvUser.Rows[e.RowIndex].Cells[1].FindControl("lbUserName")).Text;
            new SmartPortal.SEMS.User().DeleteUserTeller(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                BindData2();
                lblError.Text = Resources.labels.deleteusersuccessfully;
            }
            else
            {
                string errorCode = string.Empty;
                ErrorCodeModel EM = new ErrorCodeModel();
                if (IPCERRORDESC == "110211")
                {
                    errorCode = IPC.ERRORCODE.ACTIVEUSER;
                }
                else
                {
                    errorCode = IPC.ERRORCODE.IPC;
                }
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
            divResult.Visible = true;
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvUser.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvUser.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvUser.PageIndex * gvUser.PageSize) return;
            string letter = string.Empty;
            DataTable dtCL = new DataTable();
            dtCL = new UsersBLL().Search(Utility.KillSqlInjection(txtSearch.Text.Trim()), letter, ddlBranch.SelectedValue.Trim(), gvUser.PageSize, gvUser.PageIndex * gvUser.PageSize);

            gvUser.DataSource = dtCL;
            gvUser.DataBind();

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
}
