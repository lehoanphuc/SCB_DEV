using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_User_View_Widget : WidgetBase
{
    public static bool isSort = false;
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                wgSearchLetter.url=System.Configuration.ConfigurationManager.AppSettings["searchletter"];
                BindData();
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_User_View_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_User_View_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void BindData()
    {
        string letter="";
        if(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["letter"]!=null)
        {
            letter=SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["letter"].ToString().Trim();
        }
        
        gvUser.DataSource = new UsersBLL().Search(Utility.KillSqlInjection(txtSearch.Text),letter);
        gvUser.DataBind();
    }
    protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;            
            HyperLink hpUserName;
            Label lblFirstName;
            Label lblDateCreated;
            Label lblAuthor;
            Label lblLastLoginTime;
            Image imgShow;
            HyperLink hpEdit;
            HyperLink hpDelete;
            HyperLink hpGroup;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }
            if (e.Row.RowType == DataControlRowType.Pager)
            {

                TableRow tbRow = (TableRow)(e.Row.Cells[0].Controls[0].Controls[0]);
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + " :</strong>";

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");

                //e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                //e.Row.Attributes.Add("onmouseout", "checkColor('" + cbxSelect.ClientID + "',this)");

                hpUserName = (HyperLink)e.Row.FindControl("hpUserName");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                hpGroup = (HyperLink)e.Row.FindControl("hpGroup");
                imgShow = (Image)e.Row.FindControl("imgShow");

                lblDateCreated = (Label)e.Row.FindControl("lblDateCreated");
                lblAuthor = (Label)e.Row.FindControl("lblAuthor");
                lblFirstName = (Label)e.Row.FindControl("lblFirstName");
                lblLastLoginTime = (Label)e.Row.FindControl("lblLastLoginTime");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                hpUserName.Text = drv["UserName"].ToString();
                hpUserName.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewdetailuser"] + "&uid=" + drv["UserName"].ToString() + "&type=view";

                lblDateCreated.Text = Utility.FormatDatetime(SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["DateCreated"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.DateMMM);

                lblAuthor.Text = drv["UserCreated"].ToString();
                lblFirstName.Text = drv["FirstName"].ToString();
                lblLastLoginTime.Text = Utility.FormatDatetime(SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["LastLoginTime"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.DateTime);

                //isshow
                switch(drv["Status"].ToString())
                {
                    case "1":
                        imgShow.ImageUrl = "images/activecheck_icon.gif";
                        break;
                    case "0":
                        imgShow.ImageUrl = "images/lock_user_icon.png";
                        break;
                }                

                
                 if (Session["userName"] != null)
                {
                    if (Session["userName"].ToString().Trim() == drv["UserCreated"].ToString().Trim())
                    {
                        hpEdit.Text = "<img src='Widgets/User/view/images/icon_edit.gif'/>";
                        hpEdit.ToolTip = Resources.labels.edit;
                        hpEdit.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["edituser"] + "&uid=" + drv["UserName"].ToString();

                        hpGroup.Text = "<img src='Widgets/User/view/images/group.gif'/>";
                        hpGroup.ToolTip = Resources.labels.group;
                        hpGroup.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["userinrolelink"] + "&uid=" + drv["UserName"].ToString()+"&t=t";


                        hpDelete.Text = "<img src='Widgets/User/view/images/icon_delete.gif'/>";
                        hpDelete.ToolTip = Resources.labels.delete;
                        //hpDelete.Attributes.Add("onclick", "DeleteConfirm('" + Resources.labels.areyousuredeletethisrecord+ "')");
                        hpDelete.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["deleteuser"] + "&uid=" + drv["UserName"].ToString();

                    }
                    else
                    {
                        cbxSelect.Enabled = false;

                        hpEdit.Text = "<img src='Widgets/newsmanagement/view/images/stop.gif'/>";
                        hpDelete.Text = "<img src='Widgets/newsmanagement/view/images/stop.gif'/>";
                        //hpGroup.Text = "<img src='Widgets/newsmanagement/view/images/stop.gif'/>";
                        hpGroup.Text = "<img src='Widgets/User/view/images/group.gif'/>";
                        hpGroup.ToolTip = Resources.labels.group;
                        hpGroup.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["userinrolelink"] + "&uid=" + drv["UserName"].ToString()+"&t=f";


                    }
                }
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_User_View_Widget", "gvUser_RowDataBound", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_User_View_Widget", "gvUser_RowDataBound", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ibSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindData();
    }
    protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUser.PageIndex = e.NewPageIndex;
        BindData();
    }
    private SortDirection GridViewSortDirection
    {
        get
        {

            if (ViewState["sortDirection"] == null)

                ViewState["sortDirection"] = SortDirection.Ascending;


            return (SortDirection)ViewState["sortDirection"];

        }

        set { ViewState["sortDirection"] = value; }

    }
    protected void SortGridView(string sortExpression, string direction)
    {
        DataTable dataTable = new DataTable();

        string letter = "";
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["letter"] != null)
        {
            letter = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["letter"].ToString().Trim();
        }

        dataTable = new UsersBLL().Search(Utility.KillSqlInjection(txtSearch.Text), letter);

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvUser.DataSource = dataView;
            gvUser.DataBind();
        }

    }
    protected void gvUser_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            //isSort = true;

            string sortExpression = e.SortExpression;

            ViewState["SortExpression"] = sortExpression;
            //showImage = true;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                //isAscend = true;

                SortGridView(sortExpression, ASCENDING);
                GridViewSortDirection = SortDirection.Descending;

            }

            else
            {
                isAscend = false;
                SortGridView(sortExpression, DESCENDING);
                GridViewSortDirection = SortDirection.Ascending;

            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_User_View_Widget", "gvUser_Sorting", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_User_View_Widget", "gvUser_Sorting", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void lbAddPage_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["adduser"]);
    }
    protected void lbDeleteSelected_Click(object sender, EventArgs e)
    {
        //delete
        string username = "";
        try
        {
            CheckBox cbxDelete;
            HyperLink hpUserName;
            foreach (GridViewRow gvr in gvUser.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpUserName = (HyperLink)gvr.Cells[1].FindControl("hpUserName");
                    username += hpUserName.Text.Trim() + "-";
                }
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_User_View_Widget", "lbDeleteSelected_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_User_View_Widget", "lbDeleteSelected_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

        if (username != "")
        {
            Session["userNameDelete"] = username;
            //redirect to delete page
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["deleteuser"]);
        }
        else
        {
            BindData();
        }
    }
}
