using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.BLL;

public partial class Widgets_CategoryApprove_Widget : WidgetBase
{
    public static bool isSort = false;
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //load data for ddlStatus
            ddlStatus.Items.Add(new ListItem(Resources.labels.newsnew, "2"));
            ddlStatus.Items.Add(new ListItem(Resources.labels.unapprove, "0"));           
            ddlStatus.Items.Add(new ListItem(Resources.labels.approve, "1"));
            ddlStatus.Items.Add(new ListItem(Resources.labels.all, "3"));

            //lbAddPage.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousureapprovethisrecord + "')){return true} else {return false}");
            //lbDeleteSelected.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousureunapprovethisrecord + "')){return true} else {return false}");

            //lbAddPage1.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousureapprovethisrecord + "')){return true} else {return false}");
            //lbDeleteSelected1.Attributes.Add("onclick", "if(window.confirm('" + Resources.labels.areyousureunapprovethisrecord + "')){return true} else {return false}");
            lbAddPage.Attributes.Add("onclick", "return ModalPopupsConfirm('" + Session["userName"].ToString() + "','0');");
            lbAddPage1.Attributes.Add("onclick", "return ModalPopupsConfirm('" + Session["userName"].ToString() + "','0');");
            lbDeleteSelected.Attributes.Add("onclick", "return ModalPopupsConfirm('" + Session["userName"].ToString() + "','1');");
            lbDeleteSelected1.Attributes.Add("onclick", "return ModalPopupsConfirm('" + Session["userName"].ToString() + "','1');");
        
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            DataTable tblNewsApprove = new CategoryBLL().LoadForApprove(System.Globalization.CultureInfo.CurrentCulture.ToString(), Utility.KillSqlInjection(txtSearch.Text.Trim()), ddlStatus.SelectedValue);
            gvCategory.DataSource = tblNewsApprove;
            gvCategory.DataBind();

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
                hpCatName.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewdetailcategory"] + "&catid=" + drv["CatID"].ToString() + "&type=view";

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

                
                ////isshow
                //if (int.Parse(drv["IsPublished"].ToString()) == 1)
                //{
                //    imgShow.ImageUrl = "images/activecheck_icon.gif";
                //}
                //else
                //{
                //    if (int.Parse(drv["IsPublished"].ToString()) == 0)
                //    {
                //        imgShow.ImageUrl = "images/lock_user_icon.png";
                //    }
                //    else
                //    {
                //        imgShow.ImageUrl = "images/new.gif";
                //    }
                //}

                //hpEdit.Text = "<img src='Widgets/Category/view/images/icon_edit.gif'/>";
                //hpEdit.ToolTip = Resources.labels.edit;
                //hpEdit.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["editcategory"] + "&catid=" + drv["CatID"].ToString();

                //hpDelete.Text = "<img src='Widgets/Category/view/images/icon_delete.gif'/>";
                //hpDelete.ToolTip = Resources.labels.delete;
                ////hpDelete.Attributes.Add("onclick", "DeleteConfirm('" + Resources.labels.areyousuredeletethisrecord+ "')");
                //hpDelete.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["deletecategory"] + "&catid=" + drv["CatID"].ToString();


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

        dataTable = new CategoryBLL().LoadForApprove(System.Globalization.CultureInfo.CurrentCulture.ToString(), Utility.KillSqlInjection(txtSearch.Text.Trim()), ddlStatus.SelectedValue);


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
        //approve
        try
        {
            CheckBox cbxApprove;
            Label lblSID;
            foreach (GridViewRow gvr in gvCategory.Rows)
            {
                cbxApprove = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxApprove.Checked == true)
                {
                    lblSID = (Label)gvr.Cells[1].FindControl("lblCatID");
                    CategoryBLL CB = new CategoryBLL();
                    CB.Approve(Utility.IsInt(lblSID.Text), 1);

                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["APPROVE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["CATID"], lblSID.Text, lblSID.Text);
                }
            }
            BindData();
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["cataec"], "Widgets_CategoryApprove_Widget", "lbAddPage_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["cataec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_CategoryApprove_Widget", "lbAddPage_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_CategoryApprove_Widget", "lbAddPage_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void lbDeleteSelected_Click(object sender, EventArgs e)
    {
        //approve
        try
        {
            CheckBox cbxApprove;
            Label lblSID;
            foreach (GridViewRow gvr in gvCategory.Rows)
            {
                cbxApprove = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxApprove.Checked == true)
                {
                    lblSID = (Label)gvr.Cells[1].FindControl("lblCatID");
                    CategoryBLL CB = new CategoryBLL();
                    CB.Approve(Utility.IsInt(lblSID.Text), 0);

                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["UNAPPROVE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["CATID"], lblSID.Text, lblSID.Text);
                }
            }
            BindData();
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["catuaec"], "Widgets_CategoryApprove_Widget", "lbDeleteSelected_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["catuaec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_CategoryApprove_Widget", "lbDeleteSelected_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_CategoryApprove_Widget", "lbDeleteSelected_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
}
