using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Pages_View_Widget : WidgetBase
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
                BindData();
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Pages_View_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Pages_View_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void BindData()
    {
            gvPages.DataSource = new PagesBLL().Search(Utility.KillSqlInjection(txtSearch.Text.Trim()));
            gvPages.DataBind();       
    }

    protected void gvPages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblPageID;
            HyperLink hpPageName;
            Label lblPageDescription;
            Label lblDateCreated;
            Label lblMasterPageName;
            Label lblThemeName;
            Image imgShow;
            Label lblAuthor;
            HyperLink hpEdit;
            HyperLink hpDelete;
            HyperLink hpPreview;
            
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
                tbRow.Cells[0].Text = "<strong>"+Resources.labels.page+" :</strong>";

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");

                //e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                //e.Row.Attributes.Add("onmouseout", "checkColor('" + cbxSelect.ClientID + "',this)");

                lblPageID = (Label)e.Row.FindControl("lblpageID");
                hpPageName = (HyperLink)e.Row.FindControl("hpPageName");                

                lblPageDescription = (Label)e.Row.FindControl("lblPageDescription");
                lblDateCreated = (Label)e.Row.FindControl("lblDateCreated");
                lblMasterPageName = (Label)e.Row.FindControl("lblMasterPageName");
                lblThemeName = (Label)e.Row.FindControl("lblThemeName");
                imgShow = (Image)e.Row.FindControl("imgShow");
                lblAuthor = (Label)e.Row.FindControl("lblAuthor");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                hpPreview = (HyperLink)e.Row.FindControl("hpPreview");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblPageID.Text = drv["PageID"].ToString();
                hpPageName.Text = drv["PageName"].ToString();
                hpPageName.ToolTip = drv["PageID"].ToString();
                hpPageName.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewdetailpage"] + "&pid=" + drv["PageID"].ToString()+"&type=view";

                lblPageDescription.Text = drv["PageDescription"].ToString();
                lblDateCreated.Text = Utility.FormatDatetime(SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["DateCreated"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.DateMMM);
                lblMasterPageName.Text = drv["MasterPageName"].ToString();
                lblThemeName.Text = drv["ThemeName"].ToString();
                if (drv["IsShow"].ToString().ToLower() == "true")
                {
                    imgShow.ImageUrl = "images/activecheck_icon.gif";
                }
                else
                {
                    imgShow.ImageUrl = "images/lock_user_icon.png";
                }                
                lblAuthor.Text = drv["Author"].ToString();

                if (Session["userName"] != null)
                {
                    //if (Session["userName"].ToString().Trim() == drv["Author"].ToString().Trim())
                    {
                        hpEdit.Text = "<img src='Widgets/Pages/view/images/icon_edit.gif'/>";
                        hpEdit.ToolTip = Resources.labels.edit;
                        hpEdit.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["editpage"] + "&pid=" + drv["PageID"].ToString();

                        hpDelete.Text = "<img src='Widgets/Pages/view/images/icon_delete.gif'/>";
                        hpDelete.ToolTip = Resources.labels.delete;
                        //hpDelete.Attributes.Add("onclick", "DeleteConfirm('" + Resources.labels.areyousuredeletethisrecord+ "')");
                        hpDelete.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["deletepage"] + "&pid=" + drv["PageID"].ToString();

                        hpPreview.Text = "<img src='Widgets/Pages/view/images/preview.png'/>";
                        hpPreview.ToolTip = Resources.labels.preview;
                        hpPreview.NavigateUrl = "~/Default.aspx?p=" + drv["PageID"].ToString();
                    }
                    //else
                    //{
                    //    cbxSelect.Enabled = false;

                    //    hpEdit.Text = "<img src='Widgets/newsmanagement/view/images/stop.gif'/>";
                    //    hpDelete.Text = "<img src='Widgets/newsmanagement/view/images/stop.gif'/>";
                    //    hpPreview.Text = "<img src='Widgets/newsmanagement/view/images/stop.gif'/>";
                    //}
                }
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Pages_View_Widget", "gvPages_RowDataBound", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Pages_View_Widget", "gvPages_RowDataBound", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvPages_Sorting(object sender, GridViewSortEventArgs e)
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Pages_View_Widget", "gvPages_Sorting", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Pages_View_Widget", "gvPages_Sorting", ex.Message, Request.Url.Query);
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
        DataTable dataTable;


        dataTable = new PagesBLL().Search(Utility.KillSqlInjection(txtSearch.Text.Trim()));
        

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvPages.DataSource = dataView;
            gvPages.DataBind();
        }

    }

    protected void gvPages_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPages.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Pages_View_Widget", "gvPages_PageIndexChanging", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Pages_View_Widget", "gvPages_PageIndexChanging", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void lbDeleteSelected_Click(object sender, EventArgs e)
    {
        string pageID = "";
        try
        {
            //delete            
            CheckBox cbxDelete;
            Label lblSID;
            foreach (GridViewRow gvr in gvPages.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lblSID = (Label)gvr.Cells[1].FindControl("lblPageID");
                    pageID += lblSID.Text.Trim() + "-";
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Pages_View_Widget", "lbDeleteSelected_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        if (pageID != "")
        {
            Session["pageIDDelete"] = pageID;
            //redirect to delete page
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["deletepage"]));
        }
        else
        {
            BindData();
        }
    }
    protected void lbAddPage_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["addpage"]));
    }
    protected void ibSearch_Click(object sender, ImageClickEventArgs e)
    {       
        BindData();
    }
}
