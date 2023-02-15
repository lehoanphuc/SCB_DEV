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
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSSMSNotify_Widget : WidgetBase
{
    public static bool isAscend = false;
    public const string controlpage = "1031";
    public const string deletepage = "1032";
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            litError.Text = "";
            litPager.Text = "";
            if (!IsPostBack)
            {
                //load role
                ddlRole.DataSource = new SmartPortal.SEMS.Role().GetRoleByType("SNO");
                ddlRole.DataTextField = "RoleName";
                ddlRole.DataValueField = "RoleID";
                ddlRole.DataBind();
                ddlRole.Items.Add(new ListItem("ALL", "ALL"));
                ddlRole.SelectedValue = "ALL";

                //load trantype
                ddlTranType.DataSource = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISNOTIFICATION), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                ddlTranType.DataTextField = "PAGENAME";
                ddlTranType.DataValueField = "TRANCODE";
                ddlTranType.DataBind();
                ddlTranType.Items.Add(new ListItem("ALL", "ALL"));
                ddlTranType.SelectedValue = "ALL";


                BindData();
            }
        }
        catch (Exception ex)
        {
        }
    }

    void BindData()
    {
        DataTable dtCF = new DataTable();
        dtCF = new SmartPortal.SEMS.Notification().SearchSMSConfig(ddlRole.SelectedValue.Trim(), ddlTranType.SelectedValue.Trim(), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtConfigName.Text.Trim()));
        gvProductList.DataSource = dtCF;
        gvProductList.DataBind();

        if (dtCF.Rows.Count > 0)
        {
            litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvProductList.PageIndex) * gvProductList.PageSize) + gvProductList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtCF.Rows.Count.ToString() + "</b> " + Resources.labels.dong;
        }
        else
        {
            litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
        }
    }
    protected void gvProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblCFID;
            HyperLink hpConfigName;
            Label lblRole;
            Label lblTranType;
            Label lblDesc;
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


                lblCFID = (Label)e.Row.FindControl("lblCFID");
                hpConfigName = (HyperLink)e.Row.FindControl("hpConfigName");
                lblRole = (Label)e.Row.FindControl("lblRole");
                lblTranType = (Label)e.Row.FindControl("lblTranType");
                lblDesc = (Label)e.Row.FindControl("lblDesc");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");



                lblCFID.Text = drv["CFID"].ToString();
                hpConfigName.Text = drv["Name"].ToString();
                hpConfigName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + controlpage + "&a=viewdetail&cfid=" + drv["CFID"].ToString());

                lblRole.Text = drv["RoleName"].ToString();
                lblTranType.Text = drv["PageName"].ToString();
                lblDesc.Text = drv["Description"].ToString();

                hpEdit.Text = Resources.labels.edit;
                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + controlpage + "&a=edit&cfid=" + drv["CFID"].ToString());
                hpDelete.Text = Resources.labels.delete;
                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + deletepage + "&cfid=" + drv["CFID"].ToString());
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
                dataTable = new SmartPortal.SEMS.Notification().SearchSMSConfig(ddlRole.SelectedValue.Trim(), ddlTranType.SelectedValue.Trim(), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtConfigName.Text.Trim()));
            }
            else
            {
                dataTable = new SmartPortal.SEMS.Notification().SearchSMSConfig(ddlRole.SelectedValue.Trim(), ddlTranType.SelectedValue.Trim(), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtConfigName.Text.Trim()));
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
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p="+ controlpage +"&a=add"));
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpProID;

        string Str_ProID = "";
        try
        {
            foreach (GridViewRow gvr in gvProductList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpProID = (HyperLink)gvr.Cells[1].FindControl("lblProductCode");
                    Str_ProID += hpProID.Text.Trim() + "#";
                }
            }
            Session["_ProductID"] = Str_ProID.Substring(0, Str_ProID.Length - 1);
        }
        catch (Exception ex)
        {

        }
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p="+deletepage));
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
