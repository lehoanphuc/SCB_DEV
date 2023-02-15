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

public partial class Widgets_SEMSBankList_Widget : WidgetBase
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
                //load city
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                DataSet dsCity = new DataSet();
                dsCity = objAcct.GetCity();
                if (dsCity.Tables[0].Rows.Count == 0)
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CITYNOTEXISTS);
                }

                ddlProvince.DataSource = dsCity;
                ddlProvince.DataTextField = dsCity.Tables[0].Columns[SmartPortal.Constant.IPC.CITYNAME].ColumnName.ToString();
                ddlProvince.DataValueField = dsCity.Tables[0].Columns[SmartPortal.Constant.IPC.CITYCODE].ColumnName.ToString();
                ddlProvince.DataBind();
                ddlProvince.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));

                //load ngan hang
                DataTable dtBank = new DataTable();
                dtBank = new SmartPortal.IB.Bank().GetBank();
                if (dtBank.Rows.Count == 0)
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.BANKNOTEXISTS);
                }

                ddlbank.DataSource = dtBank;
                ddlbank.DataTextField = "BANKNAME";
                ddlbank.DataValueField = "BANKID";
                ddlbank.DataBind();
                ddlbank.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));


                DataTable dtRemit = new DataTable();
                dtRemit = new SmartPortal.SEMS.Bank().LoadRemittance();
                ddlremit.DataSource = dtRemit;
                ddlremit.DataTextField = "REMITTANCENAME";
                ddlremit.DataValueField = "REMITID";
                ddlremit.DataBind();
                ddlremit.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
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
            dtCL = new SmartPortal.SEMS.Bank().LoadChildBank(txtbankcode.Text.Trim(),ddlProvince.SelectedValue,txtchildbank.Text.Trim(),ddlremit.SelectedValue,ddlbank.SelectedValue);
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
            Label lblbankcode;
            Label lblrootbank;
            Label lblcityname;
            Label lblremittance;


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


                lblbankcode = (Label)e.Row.FindControl("lblbankcode");
                lblrootbank = (Label)e.Row.FindControl("lblrootbank");
                lblcityname = (Label)e.Row.FindControl("lblcityname");
                lblremittance = (Label)e.Row.FindControl("lblremittance");
                hpbankname = (HyperLink)e.Row.FindControl("hpbankname");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblbankcode.Text = drv["Bankcode"].ToString();
                lblrootbank.Text = drv["rootbank"].ToString();
                lblcityname.Text = drv["cityname"].ToString();
                lblremittance.Text = drv["remittance"].ToString();
                hpbankname.Text = drv["BankName"].ToString();
                hpbankname.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=409&a=viewdetail&bid=" + drv["Bankcode"].ToString());

                hpEdit.Text = Resources.labels.edit;
                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=411&a=edit&bid=" + drv["Bankcode"].ToString());
                hpDelete.Text = Resources.labels.delete;
                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=412&bid=" + drv["Bankcode"].ToString());
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
                dataTable = new SmartPortal.SEMS.Bank().LoadChildBank(txtbankcode.Text.Trim(), ddlProvince.SelectedValue, txtchildbank.Text.Trim(), ddlremit.SelectedItem.ToString(), ddlbank.SelectedValue);
            }
            else
            {
                dataTable = new SmartPortal.SEMS.Bank().LoadChildBank(txtbankcode.Text.Trim(), ddlProvince.SelectedValue, txtchildbank.Text.Trim(), ddlremit.SelectedItem.ToString(), ddlbank.SelectedValue);
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
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=410&a=add"));
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        Label lblbankcode;

        string Str_ProID = "";
        try
        {
            foreach (GridViewRow gvr in gvProductList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lblbankcode = (Label)gvr.Cells[1].FindControl("lblbankcode");
                    Str_ProID += lblbankcode.Text.Trim() + "#";
                }
            }
            Session["_OtherBankBranch"] = Str_ProID.Substring(0, Str_ProID.Length - 1);
        }
        catch (Exception ex)
        {

        }
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=412"));
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
