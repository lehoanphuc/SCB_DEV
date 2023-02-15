using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.SEMS;
using Stimulsoft.Report;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_Invoice_Invoice : System.Web.UI.Page
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //string appDirectory = HttpContext.Current.Server.MapPath(string.Empty);
        //string tranID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Request.QueryString["id"].ToString());
        //StiReport report = new StiReport();

        //DataSet ds = new SmartPortal.BLS.Transaction().DoStored("KRC_BUILDINVOICE", new object[1] { tranID }, ref IPCERRORCODE, ref IPCERRORDESC);
        //if (ds.Tables.Count == 2)
        //{
        //    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        //    {
        //        report.Load(appDirectory + "\\Template\\" + ds.Tables[1].Rows[0]["TEMPLATE"]);
        //        report.RegData(ds.Tables[0]);
        //    }
        //}

        //// View report
        //StiWebViewer1.Report = report;
        //p=KRCTransactionPrint&a=PRINT
        string checksession = string.Empty;
        try
        {
            checksession = Session["userName"].ToString();
        }
        catch
        {
            
        }
        if (string.IsNullOrEmpty(checksession) || checksession.ToUpper().Trim().Equals("GUEST"))
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=125"));
            return;
        }

        if (!CheckPermitPageAction())
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=14"));
            return;
        }

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

    public string[] GetParamsPage(params string[] paramnames)
    {
        if (paramnames.Length == 0) return null;
        string[] results = new string[paramnames.Length];
        SmartPortal.Model.DictionaryWithDefault<string, string> urlparams = SmartPortal.Common.Encrypt.GetURLParam(HttpContext.Current.Request.RawUrl);
        for (int i = 0; i < paramnames.Length; i++)
        {
            string param = paramnames[i].Trim();
            if (urlparams.ContainsKey(param)) results[i] = Utility.KillSqlInjection(urlparams[param]);
        }
        return results;
    }

    public bool CheckPermitPageAction()
    {
        string username = Session["userName"].ToString();
        try
        {
            string serviceID = Session["serviceID"].ToString();
        }catch(Exception ex)
        {
            return false;
        }
        return PagesBLL.ChekcPermitPageAction("KRCTransactionPrint", Session["userName"].ToString(), "PRINT");
    }
}