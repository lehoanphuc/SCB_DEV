using System;
using System.Collections;
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
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSWithdrawalEWalletConsumer", "SEMSWithdrawalEWalletConsumer-US");
            string results = "";
            SmartPortal.Model.DictionaryWithDefault<string, string> urlparams = Encrypt.GetURLParam(HttpContext.Current.Request.RawUrl);
            if (urlparams.ContainsKey("ID"))
                results = Utility.KillSqlInjection(urlparams["ID"]);
            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            string key = "WITHDRAWAL_" + HttpContext.Current.Session["userID"].ToString() + results;
            if (Cache[key] != null)
            {
                hasPrint = (Hashtable)Cache[key];
                tmpl.SetAttribute("TXREFID", hasPrint["TXREFID"].ToString());
                tmpl.SetAttribute("TXDT", hasPrint["TXDT"].ToString());
                tmpl.SetAttribute("PHONE_NUMBER", hasPrint["PHONE_NUMBER"].ToString());
                tmpl.SetAttribute("FULL_NAME", hasPrint["FULL_NAME"].ToString());
                tmpl.SetAttribute("AMOUNT", hasPrint["AMOUNT"].ToString());
                tmpl.SetAttribute("FEE_AMOUNT", hasPrint["FEE_AMOUNT"].ToString());
                tmpl.SetAttribute("CCYID", hasPrint["CCYID"].ToString());
                tmpl.SetAttribute("PREVIOUS_BALANCE", hasPrint["PREVIOUS_BALANCE"].ToString());
                tmpl.SetAttribute("BALANCE", hasPrint["BALANCE"].ToString());
                tmpl.SetAttribute("PREVIOUS_COIN", hasPrint["PREVIOUS_COIN"].ToString());
                tmpl.SetAttribute("CoinWallet", hasPrint["CoinWallet"].ToString());
                tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
                tmpl.SetAttribute("datecreated", hasPrint["datecreated"].ToString());
                tmpl.SetAttribute("usercreated", hasPrint["usercreated"].ToString());

                ltrPrint.Text = tmpl.ToString();
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
