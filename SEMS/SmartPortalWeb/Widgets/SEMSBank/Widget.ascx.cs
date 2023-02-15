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

public partial class Widgets_SEMSBank_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                BindData();
            }
        }
        catch (Exception ex)
        {
        }
    }

    void BindData()
    {

        DataTable dtCL = new DataTable();

            dtCL = new SmartPortal.SEMS.Bank().LoadAllBank("",Utility.KillSqlInjection(txtbank.Text.Trim()));
            gvProductList.DataSource = dtCL;
            gvProductList.DataBind();


        litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvProductList.PageIndex) * gvProductList.PageSize) + gvProductList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtCL.Rows.Count.ToString() + "</b> " + Resources.labels.dong;
    }
    protected void gvProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpbankname;
            HyperLink hpEdit;
            HyperLink hpDelete;
            Label lblID;

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

                lblID = (Label)e.Row.FindControl("lblID");
                hpbankname = (HyperLink)e.Row.FindControl("hpbankname");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblID.Text = drv["bankid"].ToString();
                hpbankname.Text = drv["BankName"].ToString();
                hpbankname.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=406&a=viewdetail&bid=" + drv["Bankid"].ToString());

                hpEdit.Text = Resources.labels.edit;
                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=405&a=edit&bid=" + drv["Bankid"].ToString());
                hpDelete.Text = Resources.labels.delete;
                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=407&bid=" + drv["Bankid"].ToString());
            }
        }
        catch
        {
        }
    }
    protected void gvProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvProductList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvProductList_Sorting(object sender, GridViewSortEventArgs e)
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
            if (Session["search"] != null)
            {
                dataTable = new SmartPortal.SEMS.Bank().LoadAllBank(Utility.KillSqlInjection(txtbank.Text.Trim()),"");
            }
            else
            {
                dataTable = new SmartPortal.SEMS.Bank().LoadAllBank(Utility.KillSqlInjection(txtbank.Text.Trim()),"");
            }
        }


        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvProductList.DataSource = dataView;
            gvProductList.DataBind();
        }

    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=404&a=add"));
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        Label lblID;

        string Str_ProID = "";
        try
        {
            foreach (GridViewRow gvr in gvProductList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lblID = (Label)gvr.Cells[1].FindControl("lblID");
                    Str_ProID += lblID.Text.Trim() + "#";
                }
            }
            Session["_OtherBank"] = Str_ProID.Substring(0, Str_ProID.Length - 1);
        }
        catch (Exception ex)
        {

        }
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=407"));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            Session["search"] = "true";
            BindData();
        }
        catch (Exception ex)
        {
        }
    }
}
