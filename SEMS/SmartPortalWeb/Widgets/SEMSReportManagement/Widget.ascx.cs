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


public partial class Widgets_SEMSReportManagement_Widget : WidgetBase
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
            litPager.Text = "";
            ltrError.Text = "";

            if (!IsPostBack)
            {
                //load feetype

                //ddlFeeType.DataSource = new SmartPortal.SEMS.Fee().LoadFeeType(ref IPCERRORCODE, ref IPCERRORDESC);
                //if (IPCERRORCODE != "0")
                //{
                //    throw new IPCException(IPCERRORDESC);
                //}

                //ddlFeeType.DataTextField = "TYPENAME";
                //ddlFeeType.DataValueField = "FEETYPE";
                //ddlFeeType.DataBind();
                //ddlFeeType.Items.Insert(0, new ListItem("Tất cả", ""));
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

            DataSet dtProcess = new DataSet();
            dtProcess = new SmartPortal.SEMS.Report().SearchReport(Utility.KillSqlInjection(txtReportID.Text.Trim()), Utility.KillSqlInjection(txtReportName.Text.Trim()), Utility.KillSqlInjection(ddlSubSystem.SelectedValue), ref IPCERRORCODE, ref IPCERRORDESC);


            if (IPCERRORCODE == "0")
            {
                gvProcessList.DataSource = dtProcess;
                gvProcessList.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            if (dtProcess!=null)
            {
                if (dtProcess.Tables[0].Rows.Count > 0)
                {
                    litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvProcessList.PageIndex) * gvProcessList.PageSize) + gvProcessList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtProcess.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;
                }
                else
                {
                    ltrError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                }
            }
            else
            {
                ltrError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
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
    protected void gvProcessList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpRptID, hpRptName;
            Label lblSubSystem, lblRptFile;
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

                hpRptID = (HyperLink)e.Row.FindControl("hpRptID");
                hpRptName = (HyperLink)e.Row.FindControl("hpRptName");
                lblSubSystem = (Label)e.Row.FindControl("lblSubSystem");
                lblRptFile = (Label)e.Row.FindControl("lblRptFile");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                hpRptID.Text = drv["RPTID"].ToString();
                hpRptID.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=372&a=viewdetail&rid=" + drv["RPTID"].ToString());
                hpRptName.Text = drv["RPTNAME"].ToString();
                // hpRptName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=372&a=viewdetail&rid=" + drv["RPTID"].ToString());
                lblSubSystem.Text = drv["SERVICEID"].ToString();
                lblRptFile.Text = drv["RPTFILE"].ToString();
                hpEdit.Text = Resources.labels.edit;
                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=373&a=edit&rid=" + drv["RPTID"].ToString());
                hpDelete.Text = Resources.labels.delete;
                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=374&rid=" + drv["RPTID"].ToString()+"&rf=" + drv["RPTFILE"].ToString());
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void gvProcessList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvProcessList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvProcessList_Sorting(object sender, GridViewSortEventArgs e)
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
                dataTable = (new SmartPortal.SEMS.Report().SearchReport(Utility.KillSqlInjection(txtReportID.Text.Trim()), Utility.KillSqlInjection(txtReportName.Text.Trim()), Utility.KillSqlInjection(ddlSubSystem.SelectedValue), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            }
            else
            {
                dataTable = (new SmartPortal.SEMS.Report().SearchReport(Utility.KillSqlInjection(txtReportID.Text.Trim()), Utility.KillSqlInjection(txtReportName.Text.Trim()), Utility.KillSqlInjection(ddlSubSystem.SelectedValue), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            }
        }


        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvProcessList.DataSource = dataView;
            gvProcessList.DataBind();
        }

    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
      Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=371&a=add"));
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpAppTranID;
        Label FileName;


        string Str_AppTranID = "";
        try
        {
            foreach (GridViewRow gvr in gvProcessList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpAppTranID = (HyperLink)gvr.Cells[1].FindControl("hpRptID");
                    Str_AppTranID += hpAppTranID.Text.Trim() + "|";
                    FileName = (Label)gvr.Cells[1].FindControl("lblRptFile");
                    Str_AppTranID += FileName.Text.Trim() + "#";
                }
            }
            Session["_RPTID"] = Str_AppTranID.Substring(0, Str_AppTranID.Length - 1);
        }
        catch (Exception ex)
        {

        }
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=374"));
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
