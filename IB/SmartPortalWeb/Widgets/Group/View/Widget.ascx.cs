using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Group_View_Widget : WidgetBase
{
    public static bool isSort = false;
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
            if (!IsPostBack)
            {                
                BindData();
            }
       
    }

    private void BindData()
    {
        try
        {
            //gvGroup.DataSource = new RoleBLL().LoadForView(Utility.KillSqlInjection(txtSearch.Text),"ALL");
            //gvGroup.DataBind();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Group_View_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Group_View_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblRoleID;
            HyperLink hpRoleName;
            Label lblRoleDescription;
            Label lblDateCreated;
            Label lblAuthor;
            
            HyperLink hpEdit;
            HyperLink hpDelete;           

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

                hpRoleName = (HyperLink)e.Row.FindControl("hpRoleName");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                lblRoleID = (Label)e.Row.FindControl("lblRoleID");
                lblDateCreated = (Label)e.Row.FindControl("lblDateCreated");
                lblAuthor = (Label)e.Row.FindControl("lblAuthor");
                lblRoleDescription = (Label)e.Row.FindControl("lblRoleDescription");
               
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblRoleID.Text = drv["RoleID"].ToString();
                hpRoleName.Text = drv["RoleName"].ToString();
                hpRoleName.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewdetailgroup"] + "&gid=" + drv["RoleID"].ToString() + "&type=view";

                lblDateCreated.Text = Utility.FormatDatetime(drv["DateCreated"].ToString(), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.DateMMM);

                lblAuthor.Text = drv["UserCreated"].ToString();
                lblRoleDescription.Text = drv["RoleDescription"].ToString();                

                if (Session["userName"] != null)
                {
                    if (Session["userName"].ToString().Trim() == drv["UserCreated"].ToString().Trim())
                    {
                        hpEdit.Text = "<img src='Widgets/group/view/images/icon_edit.gif'/>";
                        hpEdit.ToolTip = Resources.labels.edit;
                        hpEdit.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["editgroup"] + "&gid=" + drv["RoleID"].ToString();

                        hpDelete.Text = "<img src='Widgets/group/view/images/icon_delete.gif'/>";
                        hpDelete.ToolTip = Resources.labels.delete;
                        //hpDelete.Attributes.Add("onclick", "DeleteConfirm('" + Resources.labels.areyousuredeletethisrecord+ "')");
                        hpDelete.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["deletegroup"] + "&gid=" + drv["RoleID"].ToString();
                    }
                    else
                    {
                        cbxSelect.Enabled = false;

                        hpEdit.Text = "<img src='Widgets/newsmanagement/view/images/stop.gif'/>";
                        hpDelete.Text = "<img src='Widgets/newsmanagement/view/images/stop.gif'/>";

                    }
                }

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Group_View_Widget", "gvUser_RowDataBound", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ibSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindData();
    }
    protected void gvGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvGroup.PageIndex = e.NewPageIndex;
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


        //dataTable = new RoleBLL().LoadForView(Utility.KillSqlInjection(txtSearch.Text),"ALL");

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvGroup.DataSource = dataView;
            gvGroup.DataBind();
        }

    }

    protected void gvGroup_Sorting(object sender, GridViewSortEventArgs e)
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
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Group_View_Widget", "gvGroup_Sorting", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void lbAddPage_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["addgroup"]);
    }
    protected void lbDeleteSelected_Click(object sender, EventArgs e)
    {
        string roleID = "";
        try
        {
            //delete
           
            CheckBox cbxDelete;
            Label lblSID;
            foreach (GridViewRow gvr in gvGroup.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lblSID = (Label)gvr.Cells[1].FindControl("lblRoleID");
                    roleID += lblSID.Text.Trim() + "-";
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Group_View_Widget", "lbDeleteSelected_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        if (roleID != "")
        {
            Session["roleIDDelete"] = roleID;
            //redirect to delete page
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["deletegroup"]);
        }
        else
        {
            BindData();
        }
    }
}
