using System;
using System.Collections;
using System.Data;
using SmartPortal.Common;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSinternational_print : System.Web.UI.Page
{
    string errorCode = "";
    string erroDesc = "";
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
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            switch (hasPrint["accType"].ToString())
            {
                case "CTK":
                case "DCKT":
                case "IND":
                    tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSinternational", "INDForm");
                    tmpl.SetAttribute("MTCNCODE", hasPrint["MTCNCODE"].ToString());
                    tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
                    tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());
                    tmpl.SetAttribute("status", hasPrint["status"].ToString());

                    tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
                    tmpl.SetAttribute("idType", hasPrint["idType"].ToString());
                    tmpl.SetAttribute("idNumber", hasPrint["idNumber"].ToString());
                    tmpl.SetAttribute("senderCountry", hasPrint["senderCountry"].ToString());
                    tmpl.SetAttribute("expiredDate", hasPrint["expiredDate"].ToString());
                    tmpl.SetAttribute("issueDate", hasPrint["issueDate"].ToString());
                    tmpl.SetAttribute("senderAddress", hasPrint["senderAddress"].ToString());
                    tmpl.SetAttribute("senderPhone", hasPrint["senderPhone"].ToString());

                    tmpl.SetAttribute("bankName", hasPrint["bankName"].ToString());
                    tmpl.SetAttribute("swiftCode", hasPrint["swiftCode"].ToString());
                    tmpl.SetAttribute("benName", hasPrint["benName"].ToString());
                    tmpl.SetAttribute("benAccount", hasPrint["benAccount"].ToString());
                    tmpl.SetAttribute("benAddress", hasPrint["benAddress"].ToString());
                    tmpl.SetAttribute("benPhone", hasPrint["benPhone"].ToString());
                    tmpl.SetAttribute("benEmail", hasPrint["benEmail"].ToString());

                    tmpl.SetAttribute("senderAccount", hasPrint["senderAccount"].ToString());
                    tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
                    tmpl.SetAttribute("feeType", hasPrint["feeType"].ToString());
                    tmpl.SetAttribute("fee", hasPrint["fee"].ToString());
                    tmpl.SetAttribute("totalAmount", hasPrint["totalAmount"].ToString());
                    tmpl.SetAttribute("purpose", hasPrint["purpose"].ToString());
                    tmpl.SetAttribute("purposeDetail", hasPrint["purposeDetail"].ToString());
                    tmpl.SetAttribute("linktracking", hasPrint["linktracking"].ToString());
                    break;
                default :
                    tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSinternational", "MTK");
                    tmpl.SetAttribute("MTCNCODE", hasPrint["MTCNCODE"].ToString());
                    tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
                    tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());
                    tmpl.SetAttribute("status", hasPrint["status"].ToString());

                    tmpl.SetAttribute("enterName", hasPrint["enterName"].ToString());
                    tmpl.SetAttribute("enterAddress", hasPrint["enterAddress"].ToString());
                    tmpl.SetAttribute("enterPhone", hasPrint["enterPhone"].ToString());
                    tmpl.SetAttribute("enterLicense", hasPrint["enterLicense"].ToString());
                    tmpl.SetAttribute("enterTaxCode", hasPrint["enterTaxCode"].ToString());

                    tmpl.SetAttribute("bankName", hasPrint["bankName"].ToString());
                    tmpl.SetAttribute("swiftCode", hasPrint["swiftCode"].ToString());
                    tmpl.SetAttribute("benName", hasPrint["benName"].ToString());
                    tmpl.SetAttribute("benAccount", hasPrint["benAccount"].ToString());
                    tmpl.SetAttribute("benAddress", hasPrint["benAddress"].ToString());
                    tmpl.SetAttribute("benPhone", hasPrint["benPhone"].ToString());
                    tmpl.SetAttribute("benEmail", hasPrint["benEmail"].ToString());

                    tmpl.SetAttribute("senderAccount", hasPrint["senderAccount"].ToString());
                    tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
                    tmpl.SetAttribute("feeType", hasPrint["feeType"].ToString());
                    tmpl.SetAttribute("fee", hasPrint["fee"].ToString());
                    tmpl.SetAttribute("totalAmount", hasPrint["totalAmount"].ToString());
                    tmpl.SetAttribute("purpose", hasPrint["purpose"].ToString());
                    tmpl.SetAttribute("purposeDetail", hasPrint["purposeDetail"].ToString());
                    tmpl.SetAttribute("linktracking", hasPrint["linktracking"].ToString());
                    break;
            }
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
