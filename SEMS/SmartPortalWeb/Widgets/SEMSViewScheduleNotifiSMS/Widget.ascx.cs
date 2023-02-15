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

using System.Text;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSViewScheduleNotifiSMS_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblAlert.Text = "";
            litPager.Text = "";

            if (!IsPostBack)
            {
                #region load loại giao dịch
                //load tran app
                DataSet dsTranApp = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISNOTIFICATION), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
                DataTable dtTranApp = new DataTable();
                dtTranApp = dsTranApp.Tables[0];
                if (dtTranApp.Rows.Count != 0)
                {
                    ddlTransactionType.DataSource = dtTranApp;
                    ddlTransactionType.DataTextField = "PAGENAME";
                    ddlTransactionType.DataValueField = "TRANCODE";
                    ddlTransactionType.DataBind();
                }
                #endregion

                BindData();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBSTV_Widget", "Page_Load", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBSTV_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public void BindData()
    {
        DataSet dsSchedule = new SmartPortal.IB.Schedule().Load(txtScheduleName.Text, ddlTransactionType.SelectedValue, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            gvSTV.DataSource = dsSchedule;
            gvSTV.DataBind();

            if (dsSchedule.Tables[0].Rows.Count == 0)
            {
                btnDelete.Visible = false;
                lblAlert.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";

            }
            else
            {
                btnDelete.Visible = true;
                litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvSTV.PageIndex) * gvSTV.PageSize) + gvSTV.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dsSchedule.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;

            }

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch
        {
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch
        {
        }
    }
    protected void gvSTV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpScheCode;
            HyperLink lblScheName;
            Label lblScheType;
            Label lbldateExcute;
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


                hpScheCode = (HyperLink)e.Row.FindControl("hpScheCode");

                lblScheName = (HyperLink)e.Row.FindControl("lblScheName");
                lblScheType = (Label)e.Row.FindControl("lblScheType");
                lbldateExcute = (Label)e.Row.FindControl("lbldateExcute");


                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");


                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                hpScheCode.Text = drv["SCHEDULEID"].ToString();

                lblScheName.Text = drv["SCHEDULENAME"].ToString();
                lblScheName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=338&sid=" + drv["SCHEDULEID"].ToString());

                lblScheType.Text = drv["SCHEDULETYPE"].ToString();
                lbldateExcute.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(drv["NEXTEXECUTE"].ToString(), "dd/MM/yyyy HH:mm");

                hpDelete.Text = Resources.labels.huy;
                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=339&sid=" + drv["SCHEDULEID"].ToString());
            }
        }
        catch
        {
        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=320"));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void gvSTV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSTV.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpAppTranID;


        string Str_AppTranID = "";
        try
        {
            foreach (GridViewRow gvr in gvSTV.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbSelect");
                if (cbxDelete.Checked == true)
                {
                    hpAppTranID = (HyperLink)gvr.Cells[1].FindControl("hpScheCode");
                    Str_AppTranID += hpAppTranID.Text.Trim() + "#";
                }
            }
            Session["_SCHEDULEID"] = Str_AppTranID.Substring(0, Str_AppTranID.Length - 1);
        }
        catch (Exception ex)
        {

        }
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=264"));
    }
}
