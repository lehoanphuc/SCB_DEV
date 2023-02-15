using System;
using System.Collections;
using System.Data;
using SmartPortal.ExceptionCollection;
using System.Web;

public partial class Widgets_IBViewLogTransactions_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string errorCode = "";
        string erroDesc = "";

        try
        {
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            if (hasPrint["bill"].ToString() != "")
            {
                tmpl = SmartPortal.Common.ST.GetStringTemplate("IBViewLogTransactions", "BillPayment" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);
                tmpl.SetAttribute("custcodewater", hasPrint["custcodewater"].ToString());
                tmpl.SetAttribute("senderBranch", hasPrint["senderBranch"].ToString());
                tmpl.SetAttribute("receiverBranch", hasPrint["receiverBranch"].ToString());
                tmpl.SetAttribute("address", hasPrint["address"].ToString());
                tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
            }
            else
            {
                tmpl = SmartPortal.Common.ST.GetStringTemplate("IBViewLogTransactions", "ViewLog" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);
            }

            //ghi vo session dung in
            //Hashtable hasPrint = new Hashtable();
            //hasPrint = (Hashtable)Session["print"];
            tmpl.SetAttribute("tranID",hasPrint["tranID"].ToString());
            tmpl.SetAttribute("date", hasPrint["date"].ToString());
            tmpl.SetAttribute("debitAccount", hasPrint["debitAccount"].ToString());
            tmpl.SetAttribute("creditAccount", hasPrint["creditAccount"].ToString());
            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
            tmpl.SetAttribute("VATAmount", hasPrint["VATAmount"].ToString());
            tmpl.SetAttribute("LDH", hasPrint["LDH"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            
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
            tmpl.SetAttribute("bill", hasPrint["bill"].ToString());
            
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
