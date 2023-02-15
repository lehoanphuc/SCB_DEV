using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_NewsManagement_View_Widget : WidgetBase
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
                //load category
                CategoryBLL CB = new CategoryBLL();
               

                //load data in treeview
                DataTable tblMBLL = CB.LoadCategory(System.Globalization.CultureInfo.CurrentCulture.ToString());
                DataRow[] arrRow = tblMBLL.Select("ParentID='0'");
                foreach (DataRow row in arrRow)
                {
                    ListItem node = new ListItem(row["CatName"].ToString(), row["CatID"].ToString());
                    ddlCategory.Items.Add(node);

                    AddNodeChild(tblMBLL, row["CatID"].ToString().Trim(), 1);
                }
                ddlCategory.Items.Insert(0, new ListItem(Resources.labels.all, "0"));
               

                BindData();
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsManagement_View_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsManagement_View_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    public void AddNodeChild(DataTable tblMenu, string menuID, int so)
    {
        string chuoi = "";
        for (int i = 0; i < so; i++)
        {
            chuoi += "- ";
        }

        DataRow[] arrRow = tblMenu.Select("ParentID='" + menuID + "'");
        foreach (DataRow row in arrRow)
        {
            ListItem node = new ListItem(chuoi + row["CatName"].ToString(), row["CatID"].ToString());
            ddlCategory.Items.Add(node);

            AddNodeChild(tblMenu, row["CatID"].ToString().Trim(), so + 1);
        }
    }
    private void BindData()
    {

        if (Session["searchNews"] != null)
        {
            gvNewsManagement.DataSource = new NewsBLL().Search(Utility.IsInt(ddlCategory.SelectedValue),Utility.KillSqlInjection(txtSearch.Text), System.Globalization.CultureInfo.CurrentCulture.ToString());
            gvNewsManagement.DataBind();
        }
        else
        {
            gvNewsManagement.DataSource = new NewsBLL().LoadNewsForView(Utility.IsInt(ddlCategory.SelectedValue), System.Globalization.CultureInfo.CurrentCulture.ToString());
            gvNewsManagement.DataBind();
        }
    }
    protected void gvNewsManagement_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblNewsID;
            HyperLink hpTitle;
            Label lblDateCreated;            
            Image imgShow;
            Label lblNewsAuthor;
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

                lblNewsID = (Label)e.Row.FindControl("lblNewsID");
                hpTitle = (HyperLink)e.Row.FindControl("hpTitle");

                lblDateCreated = (Label)e.Row.FindControl("lblDateCreated");             
                imgShow = (Image)e.Row.FindControl("imgShow");
                lblNewsAuthor = (Label)e.Row.FindControl("lblNewsAuthor");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");


                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblNewsID.Text = drv["NewsID"].ToString();
                hpTitle.Text = drv["Title"].ToString();
                hpTitle.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewdetailnews"] + "&nid=" + drv["NewsID"].ToString() + "&type=view";

                lblDateCreated.Text = Utility.FormatDatetime(SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["DateCreated"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.DateMMM);
               
                //isshow
                if (int.Parse(drv["IsPublished"].ToString())==1)
                {
                    imgShow.ImageUrl = "images/activecheck_icon.gif";
                }
                else
                {
                    if (int.Parse(drv["IsPublished"].ToString()) == 0)
                    {
                        imgShow.ImageUrl = "images/lock_user_icon.png";
                    }
                    else
                    {
                        imgShow.ImageUrl = "images/new.gif";
                    }
                }
                lblNewsAuthor.Text = drv["NewsAuthor"].ToString();

                if (Session["userName"] != null)
                {
                    if (Session["userName"].ToString().Trim() == drv["NewsAuthor"].ToString().Trim())
                    {
                        hpEdit.Text = "<img src='Widgets/newsmanagement/view/images/icon_edit.gif'/>";
                        hpEdit.ToolTip = Resources.labels.edit;
                        hpEdit.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["editnews"] + "&nid=" + drv["NewsID"].ToString();

                        hpDelete.Text = "<img src='Widgets/newsmanagement/view/images/icon_delete.gif'/>";
                        hpDelete.ToolTip = Resources.labels.delete;
                        //hpDelete.Attributes.Add("onclick", "DeleteConfirm('" + Resources.labels.areyousuredeletethisrecord+ "')");
                        hpDelete.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["deletenews"] + "&nid=" + drv["NewsID"].ToString();
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
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsManagement_View_Widget", "gvNewsManagement_RowDataBound", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsManagement_View_Widget", "gvNewsManagement_RowDataBound", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvNewsManagement_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvNewsManagement.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsManagement_View_Widget", "gvNewsManagement_PageIndexChanging", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsManagement_View_Widget", "gvNewsManagement_PageIndexChanging", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
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
        DataTable dataTable=new DataTable();

        if (Session["searchNews"] != null)
        {
            dataTable = new NewsBLL().Search(Utility.IsInt(ddlCategory.SelectedValue), Utility.KillSqlInjection(txtSearch.Text), System.Globalization.CultureInfo.CurrentCulture.ToString());
           
        }
        else
        {
            dataTable = new NewsBLL().LoadNewsForView(Utility.IsInt(ddlCategory.SelectedValue), System.Globalization.CultureInfo.CurrentCulture.ToString());
            
        }

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvNewsManagement.DataSource = dataView;
            gvNewsManagement.DataBind();
        }

    }

    protected void gvNewsManagement_Sorting(object sender, GridViewSortEventArgs e)
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsManagement_View_Widget", "gvNewsManagement_Sorting", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsManagement_View_Widget", "gvNewsManagement_Sorting", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ibSearch_Click(object sender, ImageClickEventArgs e)
    {
        Session["searchNews"] = "true";
        BindData();
    }
    protected void lbAddPage_Click(object sender, EventArgs e)
    {
        Response.Redirect((System.Configuration.ConfigurationManager.AppSettings["addnews"]));
    }
    protected void lbDeleteSelected_Click(object sender, EventArgs e)
    {
       
        //delete
        string newsID = "";
        CheckBox cbxDelete;
        Label lblSID;
        try
        {
            foreach (GridViewRow gvr in gvNewsManagement.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lblSID = (Label)gvr.Cells[1].FindControl("lblNewsID");
                    newsID += lblSID.Text.Trim() + "-";
                }
            }
            
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsManagement_View_Widget", "lbDeleteSelected_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        if (newsID != "")
        {
            Session["newsIDDelete"] = newsID;
            //redirect to delete page
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["deletenews"]));
        }
        else
        {
            BindData();
        }
    }
}
