using System;
using System.Collections;
using System.Data;
using SmartPortal.ExceptionCollection;
using System.Web;

public partial class Widgets_IBTransferBAC1_print : System.Web.UI.Page
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
            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBTaxPayment", "TaxPaymenten-US");

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            tmpl.SetAttribute("senderAccount",hasPrint["senderAccount"].ToString());
            tmpl.SetAttribute("senderBalance", hasPrint["senderBalance"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            tmpl.SetAttribute("status", hasPrint["status"].ToString());
            tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
            tmpl.SetAttribute("feeType", hasPrint["feeType"].ToString());
            tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
            tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
            tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
            tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());

            tmpl.SetAttribute("TIN", hasPrint["TIN"].ToString());
            tmpl.SetAttribute("SettlementBank", hasPrint["SettlementBank"].ToString());
            tmpl.SetAttribute("Name", hasPrint["Name"].ToString());
            tmpl.SetAttribute("Address", hasPrint["Address"].ToString());
            tmpl.SetAttribute("Email", hasPrint["Email"].ToString());
            tmpl.SetAttribute("Phone", hasPrint["Phone"].ToString());
            tmpl.SetAttribute("PaymentOption", hasPrint["PaymentOption"].ToString());
            tmpl.SetAttribute("PaymentType", hasPrint["PaymentType"].ToString());
            tmpl.SetAttribute("TaxPeriod", hasPrint["TaxPeriod"].ToString());
            tmpl.SetAttribute("PaymentYear", hasPrint["PaymentYear"].ToString());

            DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["senderBranch"].ToString().Trim(), ref errorCode, ref erroDesc);
            if (errorCode == "0")
            {
                DataTable dtSenderBranch = dsSenderBranch.Tables[0];
                if (dtSenderBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                }
            }
            else
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
            }
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
