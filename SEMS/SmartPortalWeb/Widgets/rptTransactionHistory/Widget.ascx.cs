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

public partial class rptTransactionHistory_Widget : ReportBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string tranisBatch = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //load tran app
            DataSet dsTranApp = new SmartPortal.SEMS.Transactions().LoadTranApp(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }
            DataTable dtTranApp = new DataTable();
            dtTranApp = dsTranApp.Tables[0];

            rptTrancode.DataSource = dtTranApp;
            rptTrancode.DataTextField = "PAGENAME";
            rptTrancode.DataValueField = "TRANCODE";
            rptTrancode.DataBind();

            rptTrancode.Items.Insert(0, new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
            rptFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            rptToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            rptimper.Value = Session["Username"].ToString();

        }
        tranisBatch = System.Configuration.ConfigurationSettings.AppSettings["TRANBATCH"].ToString();
        this.ClearSession();

    }
    public override void ClearSession()
    {
        base.ClearSession();
    }
    public override void SetParam()
    {
        base.SetParam();
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        if (CBXSCD.Checked == true)
        {
            rptIsSchedule.Value = "";
        }
        if (CBXSCD.Checked == false)
        {
            rptIsSchedule.Value = "ALL";
        }
        this.SetParam();
    }
    protected void rptTrancode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tranisBatch.IndexOf(rptTrancode.SelectedValue) > 0)
        {
            rptIsBatch.Visible = true;
        }
        else
        {
            rptIsBatch.Checked = false;
            rptIsBatch.Visible = false;
        }
    }

}
