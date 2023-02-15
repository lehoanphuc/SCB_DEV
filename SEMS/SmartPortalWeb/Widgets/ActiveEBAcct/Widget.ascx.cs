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

public partial class Widgets_ActiveEBAcct_Widget : WidgetBase
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

                //BindData(Session["branch"].ToString(), "", "", "", "", "", "");
                BindData(Session["branch"].ToString(), txContractNo.Text, txtCustID.Text, txtCustCode.Text, "", txtFullName.Text, txtDate.Text);
            }

        }
        catch
        {
        }
    }
    protected void gvInactiveAcct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvInactiveAcct.PageIndex = e.NewPageIndex;
            //BindData(Session["branch"].ToString(), "", "", "", "", "", "");
            BindData(Session["branch"].ToString(), txContractNo.Text, txtCustID.Text, txtCustCode.Text, "", txtFullName.Text, txtDate.Text);
        }
        catch
        {
        }
    }
    protected void gvInactiveAcct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblContractNo;
            Label lblCustID;
            Label lblAcctNo;
            Label lblFullName;
            Label lblCustCode;
            Label lblCFType;
            Label lblBranch;


            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                lblContractNo = (Label)e.Row.FindControl("lblContractNo");
                lblCustID = (Label)e.Row.FindControl("lblCustID");
                lblAcctNo = (Label)e.Row.FindControl("lblAcctNo");
                lblFullName = (Label)e.Row.FindControl("lblFullName");
                lblCustCode = (Label)e.Row.FindControl("lblCustCode");
                lblCFType = (Label)e.Row.FindControl("lblCFType");
                lblBranch = (Label)e.Row.FindControl("lblBranch");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblContractNo.Text = drv["CONTRACTNO"].ToString();
                lblCustID.Text = drv["CUSTID"].ToString();
                lblAcctNo.Text = drv["ACCTNO"].ToString();
                lblFullName.Text = drv["FULLNAME"].ToString();
                lblCustCode.Text = drv["CUSTCODE"].ToString();
                lblCFType.Text = drv["CFTYPE"].ToString();
                lblBranch.Text = drv["BRANCHID"].ToString();

            }
        }
        catch
        {
        }
    }
    protected void gvInactiveAcct_Sorting(object sender, GridViewSortEventArgs e)
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

            gvInactiveAcct.DataSource = dataView;
            gvInactiveAcct.DataBind();
        }

    }
    #region Bind Data
    void BindData(string BranchID,string ContracNo,string CustID,string CustCode,string CFType,string FullName,
        string createDate)
    {
        try
        {
            //Bang chua thong tin tai khoan DD
            DataSet ds = new DataSet();
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            string errorCode = string.Empty;
            string errorDesc = string.Empty;
            //if (txtDate.Text == "")
            //{
            //    ds = objEx.GetExchangeRate(DateTime.Now.ToString("dd/MM/yyyy"), ref errorCode, ref errorDesc);
            //}
            //else
            //{
            //    ds = objEx.GetExchangeRate(txtDate.Text, ref errorCode, ref errorDesc);
            //}
            ds = objAcct.GetInActiveAcctInfo(BranchID, ContracNo, CustID, CustCode, CFType, FullName, createDate);
            //hien len luoi
            gvInactiveAcct.DataSource = ds;
            gvInactiveAcct.DataBind();
        }
        catch
        {
        }
    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData(Session["branch"].ToString(), txContractNo.Text, txtCustID.Text, txtCustCode.Text, "", txtFullName.Text, txtDate.Text);
    }
}
