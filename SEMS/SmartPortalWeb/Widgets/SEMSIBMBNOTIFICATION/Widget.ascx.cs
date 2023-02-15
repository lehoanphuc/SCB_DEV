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
using SmartPortal.SEMS;

public partial class Widgets_SEMSProduct_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    static string PageAddID = "1016";
    static string PageEditID = "1017";
    static string PageDeleteID = "1018";
    static string PageViewDetailID = "1019";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            litError.Text = "";
            litPager.Text = "";
            if (!IsPostBack)
            {
                string errorCode = string.Empty;
                string errorDesc = string.Empty;
                DataSet dsservice = new Services().GetAll(SmartPortal.Constant.IPC.ACTIVE, ref errorCode, ref errorDesc);

                if (errorCode == "0")
                {

                    DataTable dtservice = dsservice.Tables[0];
                    ddlService.DataSource = dtservice;
                    ddlService.DataTextField = "ServiceName";
                    ddlService.DataValueField = "ServiceID";
                    ddlService.DataBind();
                    ddlService.Items.Remove(ddlService.Items.FindByValue("PHO"));
                    ddlService.Items.Remove(ddlService.Items.FindByValue("SEMS"));
                    ddlService.Items.Remove(ddlService.Items.FindByValue("SMS"));
                }
                DataTable dtvar = new Notification().GetVarnotify();
                ddlVarname.DataSource = dtvar;
                ddlVarname.DataTextField = "varname";
                ddlVarname.DataValueField = "varid";
                ddlVarname.DataBind();

                BindData();
            }
        }
        catch (Exception ex)
        {
        }
    }

    void BindData()
    {
        try
        {
            /*
            Bang chua thong tin tai khoan DD
            DataTable ddTable = new DataTable();
            DataColumn ProductCodeCol = new DataColumn("ProductCode");
            DataColumn ProductNameCol = new DataColumn("ProductName");
            DataColumn CustTypeCol = new DataColumn("CustType");
            DataColumn ProductDescCol = new DataColumn("ProductDesc");


            ddTable.Columns.AddRange(new DataColumn[] { ProductCodeCol, ProductNameCol, CustTypeCol, ProductDescCol });

            for (int i = 1; i < 25; i++)
            {
                DataRow r = ddTable.NewRow();
                r["ProductCode"] = "SP0001";
                r["ProductName"] = " thực phẩm đa năng ";
                r["CustType"] = " Doanh nghiệp";
                r["ProductDesc"] = "Bổ sung vitamin & khoáng chất";
                ddTable.Rows.Add(r);
            }

            hien len luoi
            gvProductList.DataSource = ddTable;
            gvProductList.DataBind();
              */
            DataSet dtCL = new DataSet();

            dtCL = new Notification().GetNotifyByCondition(string.Empty, Utility.KillSqlInjection(ddlService.SelectedValue.Trim()), Utility.KillSqlInjection(ddlVarname.SelectedValue.Trim()), Utility.KillSqlInjection(txtdatestart.Text.Trim()), Utility.KillSqlInjection(txtdateend.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);



            if (IPCERRORCODE == "0")
            {
                gvProductList.DataSource = dtCL;
                gvProductList.DataBind();
                SmartPortal.Common.Log.WriteLogFile("", "", "", "records notify" + dtCL.Tables[0].Rows.Count);
            }
            else
            {
                //lblError.Text = IPCERRORDESC;
            }

            if (dtCL.Tables[0].Rows.Count > 0)
            {
                litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvProductList.PageIndex) * gvProductList.PageSize) + gvProductList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtCL.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.WriteLogFile("", "", "", "exception notification error" + ex.ToString());
        }
    }
    protected void gvProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink lblID;
            Label lblvarname;
            Label lblserviceid;
            Label lblstartdate;
            Label lblenddate;
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


                lblID = (HyperLink)e.Row.FindControl("lblID");
                lblvarname = (Label)e.Row.FindControl("lblvarname");
                lblserviceid = (Label)e.Row.FindControl("lblserviceid");
                lblstartdate = (Label)e.Row.FindControl("lblstartdate");
                lblenddate = (Label)e.Row.FindControl("lblenddate");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                lblID.Text = drv["ID"].ToString();
                lblID.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + PageViewDetailID + "&a=viewdetail&pid=" + drv["ID"].ToString());

                lblvarname.Text = drv["VARNAME"].ToString();
                lblserviceid.Text = drv["serviceid"].ToString();
                lblstartdate.Text = Convert.ToDateTime(drv["STARTDATE"]).ToString("dd/MM/yyyy HH:mm:ss");
                lblenddate.Text = Convert.ToDateTime(drv["ENDDATE"]).ToString("dd/MM/yyyy HH:mm:ss");

                hpEdit.Text = Resources.labels.edit;
                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + PageEditID + "&a=edit&pid=" + drv["ID"].ToString());
                hpDelete.Text = Resources.labels.delete;
                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + PageDeleteID + " &pid=" + drv["ID"].ToString());
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.WriteLogFile("", "", "", "Error bound data" + ex.ToString());
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
    //protected void gvProductList_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    //isSort = true;

    //    string sortExpression = e.SortExpression;

    //    ViewState["SortExpression"] = sortExpression;
    //    //showImage = true;

    //    if (GridViewSortDirection == SortDirection.Ascending)
    //    {
    //        //isAscend = true;

    //        SortGridView(sortExpression, ASCENDING);
    //        GridViewSortDirection = SortDirection.Descending;

    //    }

    //    else
    //    {
    //        isAscend = false;
    //        SortGridView(sortExpression, DESCENDING);
    //        GridViewSortDirection = SortDirection.Ascending;

    //    }
    //}

    //private SortDirection GridViewSortDirection
    //{
    //    get
    //    {

    //        if (ViewState["sortDirection"] == null)

    //            ViewState["sortDirection"] = SortDirection.Ascending;


    //        return (SortDirection)ViewState["sortDirection"];

    //    }

    //    set { ViewState["sortDirection"] = value; }

    //}

    //protected void SortGridView(string sortExpression, string direction)
    //{
    //    DataTable dataTable;

    //    if (ViewState["s"] != null)
    //    {
    //        dataTable = null;
    //    }

    //    else
    //    {
    //        if (Session["search"] != null)
    //        {
    //            dataTable = (new SmartPortal.SEMS.Product().GetProductByCondition(Utility.KillSqlInjection(txtmasp.Text.Trim()), Utility.KillSqlInjection(txttensp.Text.Trim()), DropDownList.SelectedValue, Utility.KillSqlInjection(txtdesc.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
    //        }
    //        else
    //        {
    //            dataTable = (new SmartPortal.SEMS.Product().GetProductByCondition(Utility.KillSqlInjection(txtmasp.Text.Trim()), Utility.KillSqlInjection(txttensp.Text.Trim()), DropDownList.SelectedValue, Utility.KillSqlInjection(txtdesc.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
    //        }
    //    }


    //    if (dataTable != null)
    //    {
    //        DataView dataView = new DataView(dataTable);
    //        dataView.Sort = sortExpression + direction;

    //        gvProductList.DataSource = dataView;
    //        gvProductList.DataBind();
    //    }

    //}
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + PageAddID + "&a=add"));
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
                    hpProID = (HyperLink)gvr.Cells[1].FindControl("lblID");
                    Str_ProID += hpProID.Text.Trim() + "#";
                }
            }
            Session["_ProductID"] = Str_ProID.Substring(0, Str_ProID.Length - 1);
        }
        catch (Exception ex)
        {

        }
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + PageDeleteID));
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
