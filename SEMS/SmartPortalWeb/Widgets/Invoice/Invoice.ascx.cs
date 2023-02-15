using SmartPortal.SEMS;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_Invoice_Invoice : WidgetBase
{

    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            LoadPrint();
        }
    }
    private void LoadPrint()
    {
        string appDirectory = HttpContext.Current.Server.MapPath(string.Empty) + @"\";
        string kRemove = ConfigurationManager.AppSettings["routeurlslash"].ToString().Replace("/", @"\");
        int a = appDirectory.LastIndexOf(kRemove);
        int b = appDirectory.Length;
        if (bool.Parse(ConfigurationManager.AppSettings["isEncryptURL"].ToString()))
            appDirectory = appDirectory.Remove(appDirectory.LastIndexOf(kRemove), kRemove.Length - 1);

        string tranID = GetParamsPage("id")[0];// SmartPortal.Common.Utilities.Utility.KillSqlInjection(Request.QueryString["id"].ToString());
        StiReport report = new StiReport();

        string bankid = Session["bankID"] != null ? Session["bankID"].ToString() : string.Empty;
        DataSet dsTemp = new Print().GetTemplacePrint(tranID, bankid, ref IPCERRORCODE, ref IPCERRORDESC);
        if (!IPCERRORCODE.Equals("0") || dsTemp.Tables.Count == 0 || dsTemp.Tables[0].Rows.Count == 0)
        {
            return;
        }

        DataTable dsData = new Print().GetDataPrint(tranID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (!IPCERRORCODE.Equals("0"))
        {

            return;
        }
        string ttt = Server.MapPath("~/Widgets/Invoice/Template/" + dsTemp.Tables[0].Rows[0]["TEMPLATE"]);

        report.Load(Server.MapPath("~/Widgets/Invoice/Template/" + dsTemp.Tables[0].Rows[0]["TEMPLATE"]));
        //report.Load(appDirectory + "\\Widgets\\Invoice\\Template\\" + dsTemp.Tables[0].Rows[0]["TEMPLATE"]);
        report.RegData(dsData);

        // View report
        StiWebViewer1.Report = report;
    }

}