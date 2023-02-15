using System;
using System.Collections;
using System.Data;
using SmartPortal.Common;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSViewLogTransactions_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(new PortalSettings().portalSetting.DefaultLang);
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

        if (Session["userName"] == null || Session["userName"].ToString() == "guest")
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"));
        string errorCode = "";
        string erroDesc = "";

        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSViewLogTransactions", "ViewLog" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            tmpl.SetAttribute("tranID",hasPrint["tranID"].ToString());
            tmpl.SetAttribute("date", hasPrint["date"].ToString());
            tmpl.SetAttribute("debitAccount", hasPrint["debitAccount"].ToString());
            tmpl.SetAttribute("creditAccount", hasPrint["creditAccount"].ToString());
            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
            tmpl.SetAttribute("VATAmount", hasPrint["VATAmount"].ToString());
            tmpl.SetAttribute("LDH", hasPrint["LDH"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            tmpl.SetAttribute("lastapp", hasPrint["lastapp"].ToString());
            tmpl.SetAttribute("bank", hasPrint["bank"].ToString());
            tmpl.SetAttribute("license", hasPrint["license"].ToString());
            tmpl.SetAttribute("issueDate", hasPrint["issueDate"].ToString());
            tmpl.SetAttribute("issuePlace", hasPrint["issuePlace"].ToString());
            tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
            tmpl.SetAttribute("receiverName", hasPrint["receiverName"].ToString());
            tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
            tmpl.SetAttribute("receiverAdd", hasPrint["receiverAdd"].ToString());
            tmpl.SetAttribute("status", hasPrint["status"].ToString());
            tmpl.SetAttribute("approver", hasPrint["approver"].ToString());
            tmpl.SetAttribute("worker", hasPrint["worker"].ToString());

            tmpl.SetAttribute("sotienbangchu", hasPrint["sotienbangchu"].ToString());
            tmpl.SetAttribute("city", hasPrint["city"].ToString());
            
            ltrPrint.Text = tmpl.ToString();

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pt"] != null)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pt"].ToString().ToUpper() == "P")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'> window.print();</script>");
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}
