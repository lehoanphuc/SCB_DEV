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

using SmartPortal.ExceptionCollection;

public partial class Widgets_rptAllReport_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvAllReport.DataSource = new SmartPortal.SEMS.Report().LoadAllReport(Session["userName"].ToString().Trim());
            gvAllReport.DataBind();
        }
    }
    protected void gvAllReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            //Label lblReportID;
            HyperLink lblReportID;
            Label hpReportName;
            //HyperLink hpPreview;
            DataRowView drv;


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

                //e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                //e.Row.Attributes.Add("onmouseout", "checkColor('" + cbxSelect.ClientID + "',this)");

                //lblReportID = (Label)e.Row.FindControl("lblReportID");
                lblReportID = (HyperLink)e.Row.FindControl("lblReportID");
                hpReportName = (Label)e.Row.FindControl("lblReportName");

                //hpPreview = (HyperLink)e.Row.FindControl("hpView");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblReportID.Text = drv["rptID"].ToString();
                hpReportName.Text = drv["rptName"].ToString();

                //hpPreview.Text = Resources.labels.view;
                //hpPreview.NavigateUrl = "~/Default.aspx?p=" + drv["pageparamid"].ToString() + "&rpt=" + drv["rptID"].ToString() + "&rptname=" + drv["rptName"].ToString();
                lblReportID.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + drv["pageparamid"].ToString() + "&rpt=" + drv["rptID"].ToString());

            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}
