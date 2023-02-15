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

public partial class Widgets_SEMSExchangeRate_Widget : WidgetBase
{
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
        catch
        {
        }
    }

    void BindData()
    {
        try
        {
            //Bang chua thong tin tai khoan DD
            DataSet ds = new DataSet();
            ExchangeRate objEx = new ExchangeRate();
            string errorCode = string.Empty;
            string errorDesc = string.Empty;
            if (txtDate.Text == "")
            {
                ds = objEx.GetExchangeRate(DateTime.Now.ToString("dd/MM/yyyy"), ref errorCode, ref errorDesc);
            }
            else
            {
                ds = objEx.GetExchangeRate(txtDate.Text, ref errorCode, ref errorDesc);
            }
            
            //hien len luoi
            gvExchangeList.DataSource = ds;
            gvExchangeList.DataBind();
        }
        catch
        {
        }
    }
    protected void gvExchangeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblCCYCD;
            Label lblExchangeDate;
            Label lblTransS;
            Label lblTransB;
            Label lblCashB;
            Label lblCashS;
            

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                lblCCYCD = (Label)e.Row.FindControl("lblCCYCD");
                lblExchangeDate = (Label)e.Row.FindControl("lblExchangeDate");
                lblTransS = (Label)e.Row.FindControl("lblTransB");
                lblTransB = (Label)e.Row.FindControl("lblTransS");
                lblCashB = (Label)e.Row.FindControl("lblCashB");                
                lblCashS = (Label)e.Row.FindControl("lblCashS");
                
                
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblCCYCD.Text = drv["CURRENCYID"].ToString();
                lblExchangeDate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(drv["EXCHANGEDATE"].ToString(),"dd/MM/yyyy");
                lblTransS.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(drv["BUYTRANSFER"].ToString()), Resources.labels.lak);
                lblTransB.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(drv["SELLTRANSFER"].ToString()), Resources.labels.lak);
                lblCashB.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(drv["BUYCASH"].ToString()), Resources.labels.lak);
                lblCashS.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(drv["SELLCASH"].ToString()), Resources.labels.lak);                
            }
        }
        catch
        {
        }
    }
    protected void gvExchangeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvExchangeList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvExchangeList_Sorting(object sender, GridViewSortEventArgs e)
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

            gvExchangeList.DataSource = dataView;
            gvExchangeList.DataBind();
        }

    }
    protected void BtView_Click(object sender, EventArgs e)
    {
        try
        {
            BindData();       
        }
        catch
        {
        }
    }
}
