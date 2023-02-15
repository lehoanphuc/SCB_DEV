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

public partial class Widgets_SEMSFeedBack_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblAlert.Text = "";
            litPager.Text = "";

            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    void BindData()
    {
        try
        {
            litPager.Text = "";
            DataTable dtFeedback = new SmartPortal.IB.Transactions().GetFeedback("", txtsgd.Text, "", ddlstatus.SelectedValue.ToString(),txtcontractno.Text.Trim(),txttieude.Text.Trim());
            if (dtFeedback.Rows.Count != 0)
            {
                PNRESULT.Visible = true;
                gvProvinceList.DataSource = dtFeedback;
                gvProvinceList.DataBind();
                litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvProvinceList.PageIndex) * gvProvinceList.PageSize) + gvProvinceList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtFeedback.Rows.Count.ToString() + "</b> " + Resources.labels.dong;
            }

            else
            {
                PNRESULT.Visible = false;
                litPager.Text = "<div style='text-align:center; padding-top:10px;padding-bottom:10px;font-weight:bold;color:red'>" + Resources.labels.datanotfound + "</div>";
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvProvinceList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpTranCode, hpDelete;
            Label lbltitle, lblFeedid, lblcontractno;
            Label lblcontent;
            Label lblstatus;
            Label lblcomment;



            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                //cbxSelect = new CheckBox();
                //cbxSelect.ID = "cbxSelectAll";
                //cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                //e.Row.Cells[0].Controls.Add(cbxSelect);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                lblcontractno = (Label)e.Row.FindControl("lblcontractno");
                lblFeedid = (Label)e.Row.FindControl("lblFeedid");
                hpTranCode = (HyperLink)e.Row.FindControl("hpTranCode");
                lbltitle = (Label)e.Row.FindControl("lbltitle");
                lblcontent = (Label)e.Row.FindControl("lblcontent");
                lblstatus = (Label)e.Row.FindControl("lblstatus");
                lblcomment = (Label)e.Row.FindControl("lblcomment");


                hpTranCode.Text = drv["IPCTRANSID"].ToString();
                hpTranCode.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=400&a=edit&fid=" + drv["FEEDID"].ToString());

                lblcontractno.Text = drv["CONTRACTNO"].ToString();
                lbltitle.Text = drv["TITLE"].ToString();
                lblcontent.Text = drv["CONTENTFB"].ToString();

                lblcomment.Text = drv["COMMENT"].ToString();


                if (drv["STATUS"].ToString() == SmartPortal.Constant.IPC.YES)
                {
                    lblstatus.Text = Resources.labels.daxuly;
                }
                if (drv["STATUS"].ToString() == SmartPortal.Constant.IPC.NO)
                {
                    lblstatus.Text = Resources.labels.chuaxuly;
                }

            }
        }
        catch
        {
        }
    }
    protected void gvProvinceList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvProvinceList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvProvinceList_Sorting(object sender, GridViewSortEventArgs e)
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

            gvProvinceList.DataSource = dataView;
            gvProvinceList.DataBind();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

}
