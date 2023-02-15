using System;
using System.Web;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSContractFee_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(new PortalSettings().portalSetting.DefaultLang);
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

        if (Session["userName"] == null || Session["userName"].ToString() == "guest")
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"));
        try
        {
            string results = "";
            SmartPortal.Model.DictionaryWithDefault<string, string> urlparams = Encrypt.GetURLParam(HttpContext.Current.Request.RawUrl);
            if (urlparams.ContainsKey("ID"))
                results = Utility.KillSqlInjection(urlparams["ID"]);

            string key = "MONEY_" + HttpContext.Current.Session["userID"].ToString() + results.Trim();
            if (Cache[key] != null)
            {
                ltrPrint.Text = (string)Cache[key];
                ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'> window.print();</script>");

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
