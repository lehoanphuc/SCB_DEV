using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Category_View_Widget : WidgetBase
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
            gvCategory.DataSource = new CategoryBLL().LoadForView(System.Globalization.CultureInfo.CurrentCulture.ToString(), Utility.KillSqlInjection(txtSearch.Text.Trim()));
            gvCategory.DataBind();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Category_View_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Category_View_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblCatID;
            HyperLink hpCatName;
            Label lblDateCreated;
            Label lblAuthor;
            Label lblParent;
            Image imgShow;
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

                lblCatID = (Label)e.Row.FindControl("lblCatID");
                hpCatName = (HyperLink)e.Row.FindControl("hpCatName");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                imgShow = (Image)e.Row.FindControl("imgShow");

                lblDateCreated = (Label)e.Row.FindControl("lblDateCreated");
                lblAuthor = (Label)e.Row.FindControl("lblAuthor");
                lblParent = (Label)e.Row.FindControl("lblParent");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblCatID.Text = drv["CatID"].ToString();
                hpCatName.Text = drv["CatName"].ToString();
                hpCatName.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewdetailcategory"] + "&catid=" + drv["CatID"].ToString()+"&type=view";
                
                lblDateCreated.Text = Utility.FormatDatetime(drv["DateCreated"].ToString(), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.DateMMM);
                
                lblAuthor.Text = drv["UserCreated"].ToString();

                if (drv["Parent"].ToString() != "")
                {
                    lblParent.Text = drv["Parent"].ToString();
                }
                else
                {
                    lblParent.Text = "Root";
                }
               
                //isshow
                if (int.Parse(drv["IsPublished"].ToString()) == 1)
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
                

                if (Session["userName"] != null)
                {
                    if (Session["userName"].ToString().Trim() == drv["UserCreated"].ToString().Trim())
                    {
                        hpEdit.Text = "<img src='Widgets/Category/view/images/icon_edit.gif'/>";
                        hpEdit.ToolTip = Resources.labels.edit;
                        hpEdit.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["editcategory"] + "&catid=" + drv["CatID"].ToString();

                        hpDelete.Text = "<img src='Widgets/Category/view/images/icon_delete.gif'/>";
                        hpDelete.ToolTip = Resources.labels.delete;
                        //hpDelete.Attributes.Add("onclick", "DeleteConfirm('" + Resources.labels.areyousuredeletethisrecord+ "')");
                        hpDelete.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["deletecategory"] + "&catid=" + drv["CatID"].ToString();

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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Category_View_Widget", "gvCategory_RowDataBound", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ibSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindData();
    }

    protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCategory.PageIndex = e.NewPageIndex;
            BindData();
        }
      
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Category_View_Widget", "Page_Load", ex.Message, Request.Url.Query);
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
        DataTable dataTable = new DataTable();

        dataTable = new CategoryBLL().LoadForView(System.Globalization.CultureInfo.CurrentCulture.ToString(), Utility.KillSqlInjection(txtSearch.Text.Trim()));


        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvCategory.DataSource = dataView;
            gvCategory.DataBind();
        }

    }

    protected void gvCategory_Sorting(object sender, GridViewSortEventArgs e)
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Category_View_Widget", "gvCategory_Sorting", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Category_View_Widget", "gvCategory_Sorting", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void lbAddPage_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["addcategory"]);
    }
    protected void lbDeleteSelected_Click(object sender, EventArgs e)
    {
        string catID = "";
        try
        {
            //delete
            
            CheckBox cbxDelete;
            Label lblSID;
            foreach (GridViewRow gvr in gvCategory.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lblSID = (Label)gvr.Cells[1].FindControl("lblCatID");
                    catID += lblSID.Text.Trim() + "-";
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Category_View_Widget", "lbDeleteSelected_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        if (catID != "")
        {
            Session["catIDDelete"] = catID;
            //redirect to delete page
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["deletecategory"]);
        }
        else
        {
            BindData();
        }
        
    }
}
