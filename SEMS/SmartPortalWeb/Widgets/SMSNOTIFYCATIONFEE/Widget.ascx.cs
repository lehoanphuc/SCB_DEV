using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.Common;
using SmartPortal.Common.Utilities;

public partial class Widgets_SMSNOTIFYCATIONFEE_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    static string PageAddID = "1034";
    static string PageEditID = "1035";
    static string PageDeleteID = "1036";
    static string PageViewdetailID = "1037";

    protected void Page_Load(object sender, EventArgs e)
    {
        litError.Text = "";
        litPager.Text = "";
        try
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch
        {
        }
    }

    void BindData()
    {

        DataSet dtBranch = new DataSet();
        //      txRoleid,txfeeid,txrolename,txfeename
        dtBranch = new SmartPortal.SEMS.Fee().SearchFeeSMSNotify(Utility.KillSqlInjection(txRoleid.Text.Trim()), Utility.KillSqlInjection(txfeeid.Text.Trim()), Utility.KillSqlInjection(txrolename.Text.Trim()), Utility.KillSqlInjection(txfeename.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);

        if (IPCERRORCODE == "0")
        {
            gvBranchList.DataSource = dtBranch;
            gvBranchList.DataBind();
        }
        else
        {
            //lblError.Text = IPCERRORDESC;
        }
        Session["DataExport"] = gvBranchList;
        if (dtBranch.Tables[0].Rows.Count > 0)
        {
            litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvBranchList.PageIndex) * gvBranchList.PageSize) + gvBranchList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtBranch.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;
        }
        else
        {
            litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
        }

    }
    protected void gvBranchList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink lbroleid;
            Label lbfeeid;
            Label lbrolename;
            Label lbfeename;
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
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                lbroleid = (HyperLink)e.Row.FindControl("lbroleid");
                lbfeeid = (Label)e.Row.FindControl("lbfeeid");
                lbrolename = (Label)e.Row.FindControl("lbrolename");
                lbfeename = (Label)e.Row.FindControl("lbfeename");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                lbroleid.Text = drv["Roleid"].ToString();
                lbroleid.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + PageViewdetailID + "&a=viewdetail&rid=" + drv["Roleid"].ToString() + "&fid=" + drv["Feeid"].ToString());
                lbfeeid.Text = drv["Feeid"].ToString();
                lbrolename.Text = drv["RoleName"].ToString();
                lbfeename.Text = drv["Feename"].ToString();

                hpEdit.Text = Resources.labels.edit;
                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + PageEditID + "&a=edit&rid=" + drv["Roleid"].ToString() + "&fid=" + drv["Feeid"].ToString());

                hpDelete.Text = Resources.labels.delete;
                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + PageDeleteID + "&rid=" + drv["Roleid"].ToString() + "&fid=" + drv["Feeid"].ToString());
            }
        }
        catch
        {
        }
    }
    protected void gvBranchList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvBranchList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvBranchList_Sorting(object sender, GridViewSortEventArgs e)
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

        if (ViewState["s"] != null)
        {
            dataTable = null;
        }

        else
        {
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["letter"] != null)
            {
                dataTable = null;

            }
            else
            {
                dataTable = null;

            }
        }


        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvBranchList.DataSource = dataView;
            gvBranchList.DataBind();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Session["search"] = "true";
        BindData();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + PageAddID + "&a=add"));
    }
    protected void btnDelete_Click1(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpbrchID;

        string Str_BrchID = "";
        try
        {
            foreach (GridViewRow gvr in gvBranchList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpbrchID = (HyperLink)gvr.Cells[1].FindControl("lblBranchCode");
                    Str_BrchID += hpbrchID.Text.Trim() + "#";
                }
            }
            Session["_SMSFEEID"] = Str_BrchID.Substring(0, Str_BrchID.Length - 1);
        }
        catch (Exception ex)
        {

        }
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + PageDeleteID));
    }
}
