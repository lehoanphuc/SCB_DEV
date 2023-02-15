using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using System.Data;
using SmartPortal.BLL;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Widget_View_Widget : WidgetBase
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_View_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_View_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void BindData()
    {      

            gvWidget.DataSource = new WidgetsBLL().LoadDataTable(System.Globalization.CultureInfo.CurrentCulture.ToString(),Utility.KillSqlInjection(txtSearch.Text.Trim()));
            gvWidget.DataBind();
       
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


        dataTable = new WidgetsBLL().LoadDataTable(System.Globalization.CultureInfo.CurrentCulture.ToString(), Utility.KillSqlInjection(txtSearch.Text.Trim()));
       

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvWidget.DataSource = dataView;
            gvWidget.DataBind();
        }

    }
    protected void gvWidget_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvWidget.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_View_Widget", "gvWidget_PageIndexChanging", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_View_Widget", "gvWidget_PageIndexChanging", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvWidget_Sorting(object sender, GridViewSortEventArgs e)
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_View_Widget", "gvWidget_Sorting", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_View_Widget", "gvWidget_Sorting", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvWidget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblWidgetID;
            HyperLink hpTitle;           
            Label lblDateCreated;
            Image imgEnableTheme;
            Image imgPublishTitle;
            Image imgShow;
            Label lblAuthor;
            HyperLink hpEdit;
            HyperLink hpDelete;
            HyperLink hpTranslate;
           

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

                lblWidgetID = (Label)e.Row.FindControl("lblWidgetID");
                hpTitle = (HyperLink)e.Row.FindControl("hpTitle");
                
                lblDateCreated = (Label)e.Row.FindControl("lblDateCreated");
                imgEnableTheme = (Image)e.Row.FindControl("imgEnableTheme");
                imgPublishTitle = (Image)e.Row.FindControl("imgPublishTitle");
                imgShow = (Image)e.Row.FindControl("imgShow");
                lblAuthor = (Label)e.Row.FindControl("lblAuthor");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                hpTranslate = (HyperLink)e.Row.FindControl("hpTranslate");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblWidgetID.Text = drv["WidgetID"].ToString();
                hpTitle.Text = drv["WidgetTitle"].ToString();
                hpTitle.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewdetailwidget"] + "&wid=" + drv["WidgetID"].ToString() + "&type=view";

                lblDateCreated.Text = Utility.FormatDatetime(SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["DateCreated"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.DateMMM);
                //enable theme
                if (drv["EnableTheme"].ToString().ToLower() == "true")
                {
                    imgEnableTheme.ImageUrl = "images/activecheck_icon.gif";
                }
                else
                {
                    imgEnableTheme.ImageUrl = "images/lock_user_icon.png";
                }
                //publish Title
                if (drv["ShowTitle"].ToString().ToLower() == "true")
                {
                    imgPublishTitle.ImageUrl = "images/activecheck_icon.gif";
                }
                else
                {
                    imgPublishTitle.ImageUrl = "images/lock_user_icon.png";
                }
                //isshow
                if (drv["IsPublish"].ToString().ToLower() == "true")
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
                        hpEdit.Text = "<img src='Widgets/Widget/view/images/icon_edit.gif'/>";
                        hpEdit.ToolTip = Resources.labels.edit;
                        hpEdit.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["editwidget"] + "&wid=" + drv["WidgetID"].ToString();

                        hpDelete.Text = "<img src='Widgets/Widget/view/images/icon_delete.gif'/>";
                        hpDelete.ToolTip = Resources.labels.delete;
                        //hpDelete.Attributes.Add("onclick", "DeleteConfirm('" + Resources.labels.areyousuredeletethisrecord+ "')");
                        hpDelete.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["deletewidget"] + "&wid=" + drv["WidgetID"].ToString();

                        hpTranslate.Text = "<img src='Widgets/Widget/view/images/action_refresh.gif'/>";
                        hpTranslate.ToolTip = Resources.labels.translate;
                        hpTranslate.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["translatewidget"] + "&wid=" + drv["WidgetID"].ToString();
                    }
                    //else
                    //{
                    //    cbxSelect.Enabled = false;

                    //    hpEdit.Text = "<img src='Widgets/newsmanagement/view/images/stop.gif'/>";
                    //    hpDelete.Text = "<img src='Widgets/newsmanagement/view/images/stop.gif'/>";
                    //    hpTranslate.Text = "<img src='Widgets/newsmanagement/view/images/stop.gif'/>";

                    //}
                }
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_View_Widget", "gvWidget_RowDataBound", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_View_Widget", "gvWidget_RowDataBound", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindData();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_View_Widget", "ddlLanguage_SelectedIndexChanged", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_View_Widget", "ddlLanguage_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void lbAddPage_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["addwidget"]));
    }
    protected void lbDeleteSelected_Click(object sender, EventArgs e)
    {
        //delete
        string widgetID = "";
        try
        {
            CheckBox cbxDelete;
            Label lblSID;
            foreach (GridViewRow gvr in gvWidget.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lblSID = (Label)gvr.Cells[1].FindControl("lblWidgetID");
                    widgetID += lblSID.Text.Trim() + "-";
                }
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_View_Widget", "lbDeleteSelected_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_View_Widget", "lbDeleteSelected_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        if (widgetID != "")
        {
            Session["widgetIDDelete"] = widgetID;
            //redirect to delete page
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["deletewidget"]));
        }
        else
        {
            BindData();
        }
    }
    protected void ibSearch_Click(object sender, ImageClickEventArgs e)
    {        
        BindData();
    }
}
