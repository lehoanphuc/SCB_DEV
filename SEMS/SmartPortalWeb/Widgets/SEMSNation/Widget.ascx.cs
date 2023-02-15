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


public partial class Widgets_SEMSNation_Widget : WidgetBase
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
        //Bang chua thong tin tai khoan DD
        DataTable ddTable = new DataTable();
        DataColumn NationCodeCol = new DataColumn("NationCode");
        DataColumn NationNameCol = new DataColumn("NationName");
        DataColumn NationCaptionCol = new DataColumn("NationCaption");


        ddTable.Columns.AddRange(new DataColumn[] { NationCodeCol, NationNameCol, NationCaptionCol });

        for (int i = 1; i < 25; i++)
        {
            if (i % 2 == 0)
            {
                DataRow r = ddTable.NewRow();
                r["NationCode"] = "110211";
                r["NationName"] = "American";
                r["NationCaption"] = "Washington, DC";
                ddTable.Rows.Add(r);
            }
            else {
                DataRow r = ddTable.NewRow();
                r["NationCode"] = "110212";
                r["NationName"] = "France";
                r["NationCaption"] = "Paris by Night";
                ddTable.Rows.Add(r);
            }
        }

        //hien len luoi
        gvNationList.DataSource = ddTable;
        gvNationList.DataBind();
    }
    protected void gvNationList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;           
            Label lblNationCode;
            Label lblNationName;
            Label lblNationCaption;
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                lblNationCode = (Label)e.Row.FindControl("lblMaquocgia");
                lblNationName = (Label)e.Row.FindControl("lblTenquocgia");
                lblNationCaption = (Label)e.Row.FindControl("lblTenthudo");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                lblNationCode.Text = drv["NationCode"].ToString();
                lblNationName.Text = drv["NationName"].ToString();
                lblNationCaption.Text = drv["NationCaption"].ToString();

                hpEdit.Text = Resources.labels.edit;

                hpDelete.Text = Resources.labels.delete;
            }
        }
        catch
        {
        }
    }
    protected void gvNationList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvNationList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvNationList_Sorting(object sender, GridViewSortEventArgs e)
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

            gvNationList.DataSource = dataView;
            gvNationList.DataBind();
        }

    }
}
