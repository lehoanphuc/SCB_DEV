using System;
using System.Collections;
using System.Data;
using System.Text;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBViewLogTransactions_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userID"] == null)
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"));
        try
        {
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBViewLogTransactions", "Viewlogen-US");
            switch (hasPrint["IPCTRANCODE"].ToString())
            {
                case "MB000030":
                case "AMBPMWALLET":
                case "MBBPMBANKACT":
                case "WLBPMWALLET":
                case "IBBPMBANKACT":
                    tmpl.SetAttribute("isReceiver", "none");
                    tmpl.SetAttribute("isBatch", "none");
                    tmpl.SetAttribute("isTopup", "none");
                    tmpl.SetAttribute("isBillPayment", "");
                    tmpl.SetAttribute("isSchedule", "none");
                    break;
                case "IB000499":
                    tmpl.SetAttribute("isReceiver", "none");
                    tmpl.SetAttribute("isBatch", "");
                    tmpl.SetAttribute("isTopup", "none");
                    tmpl.SetAttribute("isBillPayment", "none");
                    tmpl.SetAttribute("isSchedule", "none");
                    break;
                case "AM_MBTOPUP":
                case "MBMTUBANKACT":
                case "WLMTUWALLET":
                case "IBMTUBANKACT":
                    tmpl.SetAttribute("isReceiver", "none");
                    tmpl.SetAttribute("isBatch", "none");
                    tmpl.SetAttribute("isTopup", "");
                    tmpl.SetAttribute("isBillPayment", "none");
                    tmpl.SetAttribute("isSchedule", "none");
                    break;
                case "IB000215":
                    tmpl.SetAttribute("isReceiver", "");
                    tmpl.SetAttribute("isBatch", "none");
                    tmpl.SetAttribute("isTopup", "none");
                    tmpl.SetAttribute("isBillPayment", "none");
                    tmpl.SetAttribute("isSchedule", "");
                    break;
                default:
                    tmpl.SetAttribute("isReceiver", "");
                    tmpl.SetAttribute("isBatch", "none");
                    tmpl.SetAttribute("isTopup", "none");
                    tmpl.SetAttribute("isBillPayment", "none");
                    tmpl.SetAttribute("isSchedule", "none");
                    break;
            }

            Hashtable hs = new Hashtable();
            hs = (Hashtable)Session["hs"];
            tmpl.SetAttribute("batchTable", hs["batch"].ToString());

            tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
            tmpl.SetAttribute("date", hasPrint["date"].ToString());
            tmpl.SetAttribute("tranType", hasPrint["tranType"].ToString());
            tmpl.SetAttribute("refNo", hasPrint["refNo"].ToString());
            tmpl.SetAttribute("debitAccount", hasPrint["debitAccount"].ToString());
            tmpl.SetAttribute("debitName", hasPrint["debitName"].ToString());

            tmpl.SetAttribute("creditAccount", hasPrint["creditAccount"].ToString());
            tmpl.SetAttribute("creditName", hasPrint["creditName"].ToString());

            tmpl.SetAttribute("phoneNo", hasPrint["phoneNo"].ToString());
            tmpl.SetAttribute("telecomName", hasPrint["telecomName"].ToString());
            tmpl.SetAttribute("cardAmount", hasPrint["cardAmount"].ToString());

            tmpl.SetAttribute("billerName", hasPrint["billerName"].ToString());

            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());

            
            tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
            tmpl.SetAttribute("status", hasPrint["status"].ToString());
            tmpl.SetAttribute("worker", hasPrint["worker"].ToString());

            tmpl.SetAttribute("scheduleName", hasPrint["scheduleName"].ToString());
            tmpl.SetAttribute("scheduleType", hasPrint["scheduleType"].ToString());
            tmpl.SetAttribute("fromDate", hasPrint["fromDate"].ToString());
            tmpl.SetAttribute("toDate", hasPrint["toDate"].ToString());
            tmpl.SetAttribute("nextExecute", hasPrint["nextExecute"].ToString());


            ltrPrint.Text = tmpl.ToString();
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pt"] != null)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pt"].ToString().ToUpper() == "P")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'> window.print(); </script>");
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}
