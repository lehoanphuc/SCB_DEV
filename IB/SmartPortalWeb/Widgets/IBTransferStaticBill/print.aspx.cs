using System;
using System.Collections;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBTransferStaticBill_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string errorCode = "";
        string erroDesc = "";

        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBTransferStaticBill", "BillPayment" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["printBill"];

            //hasPrint.Add("senderAccount", lblSenderAccount.Text);
            //hasPrint.Add("ccyid", lblSenderCCYID.Text);
            //hasPrint.Add("senderName", lblSenderName.Text);
            //hasPrint.Add("providername", lblReceiverName.Text);

            //hasPrint.Add("recieverAccount", lblReceiverAccount.Text);
            //hasPrint.Add("recieverName", lblReceiverName.Text);
            //hasPrint.Add("amount", lblAmount.Text);
            //hasPrint.Add("amountchu", txtChu.Value.ToString());
            //hasPrint.Add("feeType", lblPhi.Text);
            //hasPrint.Add("feeAmount", lblPhiAmount.Text);
            //hasPrint.Add("narrative", lblDesc.Text);
            //hasPrint.Add("tranID", result["IPCTRANSID"].ToString());
            //hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));


            tmpl.SetAttribute("senderAccount",hasPrint["senderAccount"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
            tmpl.SetAttribute("recieverAccount", hasPrint["recieverAccount"].ToString());
            tmpl.SetAttribute("recieverName", hasPrint["providername"].ToString());
            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
            tmpl.SetAttribute("feeType", hasPrint["feeType"].ToString());
            tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
            tmpl.SetAttribute("desc", hasPrint["narrative"].ToString());
            tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
            tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());

            ////LAY TEN CHI NHANH
            ////lay chi nhanh gui
            //DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["senderBranch"].ToString().Trim(), ref errorCode, ref erroDesc);
            //if (errorCode == "0")
            //{
            //    DataTable dtSenderBranch = dsSenderBranch.Tables[0];
            //    if (dtSenderBranch.Rows.Count != 0)
            //    {
            //        tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
            //    }
            //}
            //else
            //{
            //    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
            //}

            ////lay chi nhanh nhan
            //DataSet dsReceiverBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["receiverBranch"].ToString().Trim(), ref errorCode, ref erroDesc);
            //if (errorCode == "0")
            //{
            //    DataTable dtReceiverBranch = dsReceiverBranch.Tables[0];
            //    if (dtReceiverBranch.Rows.Count != 0)
            //    {
            //        tmpl.SetAttribute("receiverBranch", dtReceiverBranch.Rows[0]["BRANCHNAME"].ToString());
            //    }
            //}
            //else
            //{
            //    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
            //}

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
