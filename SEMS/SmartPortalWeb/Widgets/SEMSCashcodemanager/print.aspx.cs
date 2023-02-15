using System;
using System.Collections;
using System.Data;
using SmartPortal.ExceptionCollection;
using System.Web;

public partial class Widgets_SEMSCashcodemanager_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string errorCode = "";
        string erroDesc = "";
        if (Session["userID"] == null)
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"));
        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSCashcodemanager", "TransactionSuccessful");

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
            tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());
            tmpl.SetAttribute("senderPhone", hasPrint["senderPhone"].ToString());
            tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
            tmpl.SetAttribute("senderAddress", hasPrint["senderAddress"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
            tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
            tmpl.SetAttribute("receiveType", hasPrint["receiveType"].ToString());
            tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
            tmpl.SetAttribute("status", hasPrint["status"].ToString());
            ltrPrint.Text = tmpl.ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'> window.print(); </script>");
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
