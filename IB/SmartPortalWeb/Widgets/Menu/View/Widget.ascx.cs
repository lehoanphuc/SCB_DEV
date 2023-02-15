using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Menu_View_Widget : WidgetBase
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
            gvMenu.DataSource = new MenuBLL().LoadForView(System.Globalization.CultureInfo.CurrentCulture.ToString(), Utility.KillSqlInjection(txtSearch.Text.Trim()));
            gvMenu.DataBind();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Menu_View_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Menu_View_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvMenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblMenuID;
            HyperLink hpTitle;
            Label lblDateCreated;
            Label lblParent;
            Label lblOrder;
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

                lblMenuID = (Label)e.Row.FindControl("lblMenuID");
                hpTitle = (HyperLink)e.Row.FindControl("hpTitle");

                lblDateCreated = (Label)e.Row.FindControl("lblDateCreated");
                lblParent = (Label)e.Row.FindControl("lblParent");
                lblOrder = (Label)e.Row.FindControl("lblOrder");
                imgShow = (Image)e.Row.FindControl("imgShow");
                lblAuthor = (Label)e.Row.FindControl("lblAuthor");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                hpTranslate = (HyperLink)e.Row.FindControl("hpTranslate");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblMenuID.Text = drv["MenuID"].ToString();
                hpTitle.Text = drv["MenuTitle"].ToString();
                hpTitle.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewdetailmenu"] + "&mid=" + drv["MenuID"].ToString() + "&type=view";

                lblDateCreated.Text = Utility.FormatDatetime(SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["DateCreated"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.DateMMM);

                if (drv["MenuParent"].ToString() == "0")
                {
                    lblParent.Text = "Root";
                }
                else
                {
                    lblParent.Text = drv["Parent"].ToString();
                }
                lblOrder.Text = drv["MenuOrder"].ToString();

                //isshow
                if (drv["IsPublished"].ToString().ToLower() == "true")
                {
                    imgShow.ImageUrl = "images/activecheck_icon.gif";
                }
                else
                {
                    imgShow.ImageUrl = "images/lock_user_icon.png";
                }
                lblAuthor.Text = drv["UserCreated"].ToString();

                 if (Session["userName"] != null)
                {
                    //if (Session["userName"].ToString().Trim() == drv["UserCreated"].ToString().Trim())
                    {
                        hpEdit.Text = "<img src='Widgets/Menu/view/images/icon_edit.gif'/>";
                        hpEdit.ToolTip = Resources.labels.edit;
                        hpEdit.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["editmenu"] + "&mid=" + drv["MenuID"].ToString();

                        hpDelete.Text = "<img src='Widgets/Menu/view/images/icon_delete.gif'/>";
                        hpDelete.ToolTip = Resources.labels.delete;
                        //hpDelete.Attributes.Add("onclick", "DeleteConfirm('" + Resources.labels.areyousuredeletethisrecord+ "')");
                        hpDelete.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["deletemenu"] + "&mid=" + drv["MenuID"].ToString();

                        hpTranslate.Text = "<img src='Widgets/Menu/view/images/action_refresh.gif'/>";
                        hpTranslate.ToolTip = Resources.labels.translate;
                        //hpDelete.Attributes.Add("onclick", "DeleteConfirm('" + Resources.labels.areyousuredeletethisrecord+ "')");
                        hpTranslate.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["translatemenu"] + "&mid=" + drv["MenuID"].ToString();
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
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Menu_View_Widget", "gvMenu_RowDataBound", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            BindData();
       
    }

    protected void gvMenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
            gvMenu.PageIndex = e.NewPageIndex;
            BindData();
        
    }

    protected void lbAddPage_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["addmenu"]));
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
        DataTable dataTable;


        dataTable = new MenuBLL().LoadForView(System.Globalization.CultureInfo.CurrentCulture.ToString(), Utility.KillSqlInjection(txtSearch.Text.Trim()));          
       

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvMenu.DataSource = dataView;
            gvMenu.DataBind();
        }

    }

    protected void gvMenu_Sorting(object sender, GridViewSortEventArgs e)
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Menu_View_Widget", "gvMenu_Sorting", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Menu_View_Widget", "gvMenu_Sorting", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void lbDeleteSelected_Click(object sender, EventArgs e)
    {
        string menuID = "";
        try
        {
            //delete
           
            CheckBox cbxDelete;
            Label lblSID;
            foreach (GridViewRow gvr in gvMenu.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lblSID = (Label)gvr.Cells[1].FindControl("lblMenuID");
                    menuID += lblSID.Text.Trim() + "-";
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Menu_View_Widget", "lbDeleteSelected_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        if (menuID != "")
        {
            Session["menuIDDelete"] = menuID;
            //redirect to delete page
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["deletemenu"]));
        }
        else
        {
            BindData();
        }
       
    }
}
