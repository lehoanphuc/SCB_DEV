using System;
using System.Collections;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBTransferInBank1_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string errorCode = "";
        string erroDesc = "";

        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBOpenBankAct", "OpenBanken-US");

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            tmpl.SetAttribute("accountType", hasPrint["accountType"].ToString());
            tmpl.SetAttribute("debitAccount", hasPrint["debitAccount"].ToString());
            tmpl.SetAttribute("availableBalance", hasPrint["availableBalance"].ToString());
            tmpl.SetAttribute("category", hasPrint["category"].ToString());
            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("accountNoOpen", hasPrint["accountNoOpen"].ToString());
            tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
            tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
            tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());

            ltrPrint.Text = tmpl.ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'> window.print(); </script>");
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}
