using System;
using System.Collections;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBCBTransfer_viewprint : System.Web.UI.Page
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
            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBCBTransfer", "IBCBTransferen-US");

            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());
            tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
            tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
            tmpl.SetAttribute("senderAccount", hasPrint["senderAccount"].ToString());

            tmpl.SetAttribute("recieverName", hasPrint["receiverName"].ToString());
            tmpl.SetAttribute("recieverAccount", hasPrint["receiverAccount"].ToString());
            tmpl.SetAttribute("recieverBank", hasPrint["recieverBank"].ToString());
            tmpl.SetAttribute("swiftCode", hasPrint["swiftCode"].ToString());
            tmpl.SetAttribute("recieverPhone", hasPrint["recieverPhone"].ToString());
            tmpl.SetAttribute("recieverEmail", hasPrint["recieverEmail"].ToString());
            tmpl.SetAttribute("recieverAddress", hasPrint["recieverAddress"].ToString());

            tmpl.SetAttribute("idType", hasPrint["senderidtype"].ToString());
            tmpl.SetAttribute("idNumber", hasPrint["senderidnumber"].ToString());
            tmpl.SetAttribute("country", hasPrint["sendercountry"].ToString());
            tmpl.SetAttribute("expiredDate", hasPrint["expireddate"].ToString());
            tmpl.SetAttribute("issueDate", hasPrint["issuedate"].ToString());

            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("totalAmount", hasPrint["totalAmount"].ToString());
            tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            tmpl.SetAttribute("feeType", hasPrint["feeType"].ToString());
            tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
            tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
            tmpl.SetAttribute("status", hasPrint["status"].ToString());
            tmpl.SetAttribute("purPose", hasPrint["purPose"].ToString());
            tmpl.SetAttribute("DetailpurPose", hasPrint["DetailpurPose"].ToString());
            ltrPrint.Text = tmpl.ToString();
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
