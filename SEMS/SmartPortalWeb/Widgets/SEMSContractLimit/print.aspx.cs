using System;
using System.Collections;
using System.Data;
using SmartPortal.ExceptionCollection;
using System.Web;
using SmartPortal.Common;

public partial class Widgets_SEMSContractLimit_print : System.Web.UI.Page
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
            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSContractLimit", "ContractLimit" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["printContractLimit"];
            if(hasPrint!=null)
            {
                tmpl.SetAttribute("contractno", hasPrint["contractno"].ToString());
                tmpl.SetAttribute("trantype", hasPrint["trantype"].ToString());
                tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
                tmpl.SetAttribute("tranlimit", hasPrint["tranlimit"].ToString());
                tmpl.SetAttribute("totallimitday", hasPrint["totallimitday"].ToString());
                tmpl.SetAttribute("countlimit", hasPrint["countlimit"].ToString());
                tmpl.SetAttribute("datecreated", hasPrint["datecreated"].ToString());
                tmpl.SetAttribute("dateapproved", hasPrint["dateapproved"].ToString());
                tmpl.SetAttribute("usercreated", hasPrint["usercreated"].ToString());
                tmpl.SetAttribute("userapproved", hasPrint["userapproved"].ToString());

                ltrPrint.Text = tmpl.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'> window.print(); </script>");
                //Session["printContractLimit"] = null;
            }
            else
            {

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
