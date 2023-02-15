using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using System.Data;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_NewsApprove_Widget : WidgetBase
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

                //load data for ddlStatus
                ddlStatus.Items.Add(new ListItem(Resources.labels.newsnew, "2"));
                ddlStatus.Items.Add(new ListItem(Resources.labels.unapprove, "0"));               
                ddlStatus.Items.Add(new ListItem(Resources.labels.approve, "1"));
                ddlStatus.Items.Add(new ListItem(Resources.labels.all, "3"));

                //add javascript

                lbAddPage.Attributes.Add("onclick", "return ModalPopupsConfirm('" + Session["userName"].ToString() + "','0');");
                lbAddPage1.Attributes.Add("onclick", "return ModalPopupsConfirm('" + Session["userName"].ToString() + "','0');");
                lbDeleteSelected.Attributes.Add("onclick", "return ModalPopupsConfirm('" + Session["userName"].ToString() + "','1');");
                lbDeleteSelected1.Attributes.Add("onclick", "return ModalPopupsConfirm('" + Session["userName"].ToString() + "','1');");
                //lbDeleteSelected.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousureunapprovethisrecord + "')){return true} else {return false}");

                //lbAddPage1.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousureapprovethisrecord + "')){return true} else {return false}");
                //lbDeleteSelected1.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousureunapprovethisrecord + "')){return true} else {return false}");

                BindData();
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsApprove_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsApprove_Widget", "Page_Load", ex.Message, Request.Url.Query);
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
        DataTable tblNewsApprove = new NewsBLL().LoadNewsForApprove(Utility.IsInt(ddlCategory.SelectedValue), System.Globalization.CultureInfo.CurrentCulture.ToString(), Utility.IsInt(ddlStatus.SelectedValue), Utility.KillSqlInjection(txtSearch.Text));
        gvNewsApprove.DataSource = tblNewsApprove;
        gvNewsApprove.DataBind();       

        //hide button approve if no data
        if (tblNewsApprove.Rows.Count == 0)
        {
            lbAddPage.Visible = false;
            lbDeleteSelected.Visible = false;
            imgApprove.Visible = false;
            imgUnApprove.Visible = false;

            lbAddPage1.Visible = false;
            lbDeleteSelected1.Visible = false;
            imgApprove1.Visible = false;
            imgUnApprove1.Visible = false;
        }
        else
        {
            lbAddPage.Visible = true;
            lbDeleteSelected.Visible = true;
            imgApprove.Visible = true;
            imgUnApprove.Visible = true;

            lbAddPage1.Visible = true;
            lbDeleteSelected1.Visible = true;
            imgApprove1.Visible = true;
            imgUnApprove1.Visible = true;
        }
    }
    protected void gvNewsApprove_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblNewsID;
            HyperLink hpTitle;
            Label lblDateCreated;           
            Label lblNewsAuthor;           


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
                lblNewsAuthor = (Label)e.Row.FindControl("lblNewsAuthor");               


                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblNewsID.Text = drv["NewsID"].ToString();
                hpTitle.Text = drv["Title"].ToString();
                hpTitle.NavigateUrl = "~/widgets/newsdetail/ViewDetailNews.aspx?nid=" + drv["NewsID"].ToString();
                hpTitle.Target = "_Blank";

                lblDateCreated.Text = Utility.FormatDatetime(SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["DateCreated"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", DateTimeStyle.DateMMM);

               
                lblNewsAuthor.Text = drv["NewsAuthor"].ToString();
               
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsApprove_Widget", "gvNewsApprove_RowDataBound", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsApprove_Widget", "gvNewsApprove_RowDataBound", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ibSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindData();
    }
    protected void gvNewsApprove_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNewsApprove.PageIndex = e.NewPageIndex;
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

        dataTable = new NewsBLL().LoadNewsForApprove(Utility.IsInt(ddlCategory.SelectedValue), System.Globalization.CultureInfo.CurrentCulture.ToString(), Utility.IsInt(ddlStatus.SelectedValue), Utility.KillSqlInjection(txtSearch.Text));
        
        
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvNewsApprove.DataSource = dataView;
            gvNewsApprove.DataBind();
        }

    }
    protected void gvNewsApprove_Sorting(object sender, GridViewSortEventArgs e)
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsApprove_Widget", "gvNewsApprove_Sorting", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsApprove_Widget", "gvNewsApprove_Sorting", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    protected void lbAddPage_Click(object sender, EventArgs e)
    {
        //approve
        try
        {
            CheckBox cbxApprove;
            Label lblSID;
            foreach (GridViewRow gvr in gvNewsApprove.Rows)
            {
                cbxApprove = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxApprove.Checked == true)
                {
                    lblSID = (Label)gvr.Cells[1].FindControl("lblNewsID");
                    NewsBLL NB = new NewsBLL();
                    NB.Approve(Utility.IsInt(lblSID.Text), 1);                    
                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["APPROVE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress,System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"],System.Configuration.ConfigurationManager.AppSettings["NEWSID"],lblSID.Text,lblSID.Text);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["APPROVE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["ISPUBLISHED"], "", true.ToString());
                }
            }
            BindData();
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["naec"], "Widgets_NewsApprove_Widget", "lbAddPage_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["naec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsApprove_Widget", "lbAddPage_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsApprove_Widget", "lbAddPage_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    protected void lbDeleteSelected_Click(object sender, EventArgs e)
    {
        //unapprove
        try
        {
            CheckBox cbxApprove;
            Label lblSID;
            foreach (GridViewRow gvr in gvNewsApprove.Rows)
            {
                cbxApprove = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxApprove.Checked == true)
                {
                    lblSID = (Label)gvr.Cells[1].FindControl("lblNewsID");
                    NewsBLL NB = new NewsBLL();
                    NB.Approve(Utility.IsInt(lblSID.Text), 0);

                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["UNAPPROVE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["NEWSID"], lblSID.Text, lblSID.Text);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["UNAPPROVE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["ISPUBLISHED"], "", false.ToString());
                }
            }
            BindData();
        }

        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["nuaec"], "Widgets_NewsApprove_Widget", "lbDeleteSelected_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["nuaec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsApprove_Widget", "lbDeleteSelected_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsApprove_Widget", "lbDeleteSelected_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
