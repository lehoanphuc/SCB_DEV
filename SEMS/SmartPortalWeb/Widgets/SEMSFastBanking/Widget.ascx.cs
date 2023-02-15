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

using SmartPortal.SEMS;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSFastBanking_Widget : WidgetBase
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
            lblError.Text = "";

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
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string ContractNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text);
        string ShopID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtShopID.Text);
        string ShopCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtShopCode.Text);
        string ShopName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtShopName.Text);
        string Status = ddlStatus.SelectedValue.Trim();

        DataSet ds = new SmartPortal.SEMS.FastBank().SelectShop(ContractNo, ShopID, ShopName, ShopCode, Status, ref IPCERRORCODE, ref IPCERRORDESC);

        if (IPCERRORCODE.Equals("0"))
        {
            gvShopList.DataSource = ds;
            gvShopList.DataBind();
        }

    }
    protected void gvShopList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblShopID;
            Label lblContractNo;
            Label lblShopName;
            Label lblShopCode;
            Label lblUserCreate;
            Label lblDateCreate;
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


                lblShopID = (Label)e.Row.FindControl("lblShopID");
                lblContractNo = (Label)e.Row.FindControl("lblContractNo");
                lblShopName = (Label)e.Row.FindControl("lblShopName");
                lblShopCode = (Label)e.Row.FindControl("lblShopCode");
                lblUserCreate = (Label)e.Row.FindControl("lblUserCreate");
                lblDateCreate = (Label)e.Row.FindControl("lblDateCreate");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblShopID.Text = drv["ShopID"].ToString();
                lblContractNo.Text = drv["ContractNo"].ToString();
                lblShopName.Text = drv["ShopName"].ToString();
                lblShopCode.Text = drv["ShopCode"].ToString();
                lblUserCreate.Text = drv["UserCreate"].ToString();
                lblDateCreate.Text = drv["DateCreate"].ToString();

                hpEdit.Text = Resources.labels.edit;
                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=1002&a=edit&pid=" + drv["ShopID"].ToString());

                hpDelete.Text = Resources.labels.huy;
                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=1003&a=delete&pid=" + drv["ShopID"].ToString());
                
                hpEdit.Enabled=drv["Status"].ToString().Equals("D") ? false : true;
                hpDelete.Enabled = drv["Status"].ToString().Equals("D") ? false : true;
            }
        }
        catch(Exception ex)
        {
        }
    }
    protected void gvShopList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvShopList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvShopList_Sorting(object sender, GridViewSortEventArgs e)
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

            gvShopList.DataSource = dataView;
            gvShopList.DataBind();
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=1002&a=add"));
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpProID;

        string Str_ProID = "";
        try
        {
            foreach (GridViewRow gvr in gvShopList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpProID = (HyperLink)gvr.Cells[1].FindControl("ShopID");
                    Str_ProID += hpProID.Text.Trim() + "#";
                }
            }
        }
        catch (Exception ex)
        {

        }
        if (Str_ProID == "")

        {
            lblError.Text = Resources.labels.banvuilongchonmayatmdexoa;
            BindData();
        } 
        else
        {
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=261"));
        }
    }
}
