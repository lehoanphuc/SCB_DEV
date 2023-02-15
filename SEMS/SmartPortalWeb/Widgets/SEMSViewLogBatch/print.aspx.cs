using System;
using System.Collections;
using System.Data;
using SmartPortal.Common;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSViewLogBatch_print : System.Web.UI.Page
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
            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSViewLogBatch", "ViewLogBatch" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["PrintBatchTrans"];
            tmpl.SetAttribute("transid", hasPrint["transid"].ToString());
            tmpl.SetAttribute("senderaccount", hasPrint["senderaccount"].ToString());
            tmpl.SetAttribute("sender", hasPrint["sender"].ToString());
            tmpl.SetAttribute("content", hasPrint["content"].ToString());
            tmpl.SetAttribute("batch", hasPrint["batch"].ToString());



            ltrPrint.Text = tmpl.ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'> window.print(); window.close(); </script>");
            //Session["PrintBatchTrans"] = null;
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
