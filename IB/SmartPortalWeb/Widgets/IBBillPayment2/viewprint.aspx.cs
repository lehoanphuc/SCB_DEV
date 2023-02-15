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
            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBBillPayment2", "BillPaymentSuccessful");

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];

            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            tmpl.SetAttribute("status", hasPrint["status"].ToString());
            tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
            tmpl.SetAttribute("feeType", hasPrint["feeType"].ToString());
            tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
            tmpl.SetAttribute("discount", hasPrint["discount"].ToString());
            tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
            tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
            tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());

            tmpl.SetAttribute("serviceName", hasPrint["serviceName"].ToString());
            tmpl.SetAttribute("senderAccount", hasPrint["senderAccount"].ToString());
            if (string.IsNullOrEmpty(hasPrint["lblRef1Verf"].ToString()))
                tmpl.SetAttribute("showlblRef1Verf", "none");
            else
            {
                tmpl.SetAttribute("lblRef1Verf", hasPrint["lblRef1Verf"].ToString());
                tmpl.SetAttribute("refval01", hasPrint["refval01"].ToString());
            }
            if (string.IsNullOrEmpty(hasPrint["lblRef2Verf"].ToString()))
                tmpl.SetAttribute("showlblRef2Verf", "none");
            else
            {
                tmpl.SetAttribute("lblRef2Verf", hasPrint["lblRef2Verf"].ToString());
                tmpl.SetAttribute("refval02", hasPrint["refval02"].ToString());
            }
            if (string.IsNullOrEmpty(hasPrint["lblRef3Verf"].ToString()))
                tmpl.SetAttribute("showlblRef3Verf", "none");
            else
            {
                tmpl.SetAttribute("lblRef3Verf", hasPrint["lblRef3Verf"].ToString());
                tmpl.SetAttribute("refval03", hasPrint["refval03"].ToString());
            }
            if (string.IsNullOrEmpty(hasPrint["lblRef4Verf"].ToString()))
                tmpl.SetAttribute("showlblRef4Verf", "none");
            else
            {
                tmpl.SetAttribute("lblRef4Verf", hasPrint["lblRef4Verf"].ToString());
                tmpl.SetAttribute("refval04", hasPrint["refval04"].ToString());
            }
            if (string.IsNullOrEmpty(hasPrint["lblRef5Verf"].ToString()))
                tmpl.SetAttribute("showlblRef5Verf", "none");
            else
            {
                tmpl.SetAttribute("lblRef5Verf", hasPrint["lblRef5Verf"].ToString());
                tmpl.SetAttribute("refval05", hasPrint["refval05"].ToString());
            }
            if (string.IsNullOrEmpty(hasPrint["lblBill01Name"].ToString()))
                tmpl.SetAttribute("showlblBill01Name", "none");
            else
            {

                tmpl.SetAttribute("lblBill01Name", hasPrint["lblBill01Name"].ToString());
                tmpl.SetAttribute("lblBill01Value", hasPrint["lblBill01Value"].ToString());
            }
            if (string.IsNullOrEmpty(hasPrint["lblBill02Name"].ToString()))
                tmpl.SetAttribute("showlblBill02Name", "none");
            else
            {
                tmpl.SetAttribute("lblBill02Name", hasPrint["lblBill02Name"].ToString());
                tmpl.SetAttribute("lblBill02Value", hasPrint["lblBill02Value"].ToString());
            }
            if (string.IsNullOrEmpty(hasPrint["lblBill03Name"].ToString()))
                tmpl.SetAttribute("showlblBill03Name", "none");
            else
            {
                tmpl.SetAttribute("lblBill03Name", hasPrint["lblBill03Name"].ToString());
                tmpl.SetAttribute("lblBill03Value", hasPrint["lblBill03Value"].ToString());
            }
            if (string.IsNullOrEmpty(hasPrint["lblBill04Name"].ToString()))
                tmpl.SetAttribute("showlblBill04Name", "none");
            else
            {
                tmpl.SetAttribute("lblBill04Name", hasPrint["lblBill04Name"].ToString());
                tmpl.SetAttribute("lblBill04Value", hasPrint["lblBill04Value"].ToString());
            }
            if (string.IsNullOrEmpty(hasPrint["lblBill05Name"].ToString()))
                tmpl.SetAttribute("showlblBill05Name", "none");
            else
            {
                tmpl.SetAttribute("lblBill05Name", hasPrint["lblBill05Name"].ToString());
                tmpl.SetAttribute("lblBill05Value", hasPrint["lblBill05Value"].ToString());
            }


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
