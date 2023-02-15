using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_WidgetHTMLManagement_View_Widget : WidgetBase
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_WidgetHTMLManagement_View_Widget", "Page_Load", sex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_WidgetHTMLManagement_View_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }

    private void BindData()
    {
        gvHTMLWidget.DataSource = new HTMLWidgetBLL().Load(System.Globalization.CultureInfo.CurrentCulture.ToString(),Utility.KillSqlInjection(txtSearch.Text.Trim()));
        gvHTMLWidget.DataBind();
    }
    protected void gvHTMLWidget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblWidgetID;
            HyperLink hpWidgetTitle;          
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

                hpWidgetTitle = (HyperLink)e.Row.FindControl("hpWidgetTitle");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                lblWidgetID = (Label)e.Row.FindControl("lblWidgetID");
                lblDateCreated = (Label)e.Row.FindControl("lblDateCreated");
                lblAuthor = (Label)e.Row.FindControl("lblAuthor");                

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblWidgetID.Text = drv["WidgetID"].ToString();
                hpWidgetTitle.Text = drv["WidgetTitle"].ToString();
                hpWidgetTitle.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewdetailhtmlwidget"] + "&wid=" + drv["WidgetID"].ToString() + "&type=view";

                lblDateCreated.Text = Utility.FormatDatetime(SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["DateCreated"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.DateMMM);

                lblAuthor.Text = drv["UserCreated"].ToString();               

                 if (Session["userName"] != null)
                {
                    if (Session["userName"].ToString().Trim() == drv["UserCreated"].ToString().Trim())
                    {
                        hpEdit.Text = "<img src='Widgets/widgethtmlmanagement/view/images/icon_edit.gif'/>";
                        hpEdit.ToolTip = Resources.labels.edit;
                        hpEdit.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["edithtmlwidget"] + "&wid=" + drv["WidgetID"].ToString();

                        hpDelete.Text = "<img src='Widgets/widgethtmlmanagement/view/images/icon_delete.gif'/>";
                        hpDelete.ToolTip = Resources.labels.delete;
                        //hpDelete.Attributes.Add("onclick", "DeleteConfirm('" + Resources.labels.areyousuredeletethisrecord+ "')");
                        hpDelete.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["deletehtmlwidget"] + "&wid=" + drv["WidgetID"].ToString();

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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_WidgetHTMLManagement_View_Widget", "gvHTMLWidget_RowDataBound", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_WidgetHTMLManagement_View_Widget", "gvHTMLWidget_RowDataBound", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvHTMLWidget_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvHTMLWidget.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_WidgetHTMLManagement_View_Widget", "gvHTMLWidget_PageIndexChanging", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_WidgetHTMLManagement_View_Widget", "gvHTMLWidget_PageIndexChanging", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ibSearch_Click(object sender, ImageClickEventArgs e)
    {
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


        dataTable = new HTMLWidgetBLL().Load(System.Globalization.CultureInfo.CurrentCulture.ToString(), Utility.KillSqlInjection(txtSearch.Text.Trim()));

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvHTMLWidget.DataSource = dataView;
            gvHTMLWidget.DataBind();
        }

    }
    protected void gvHTMLWidget_Sorting(object sender, GridViewSortEventArgs e)
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_WidgetHTMLManagement_View_Widget", "gvHTMLWidget_Sorting", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_WidgetHTMLManagement_View_Widget", "gvHTMLWidget_Sorting", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void lbAddPage_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["addhtmlwidget"]);
    }
    protected void lbDeleteSelected_Click(object sender, EventArgs e)
    {
        //delete
        string widgetID = "";
        try
        {
            CheckBox cbxDelete;
            Label lblSID;
            foreach (GridViewRow gvr in gvHTMLWidget.Rows)
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_WidgetHTMLManagement_View_Widget", "lbDeleteSelected_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_WidgetHTMLManagement_View_Widget", "lbDeleteSelected_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

        if (widgetID != "")
        {
            Session["htmlWidgetIDDelete"] = widgetID;
            //redirect to delete page
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["deletehtmlwidget"]);
        }
        else
        {
            BindData();
        }
    }
}
