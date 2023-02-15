using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.ExceptionCollection;

public partial class Widgets_IBTransferOutBank1_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string errorCode = "";
        string erroDesc = "";

        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();            

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];

            if (hasPrint["isAccount"].ToString().Trim() == "Y")
            {
                tmpl = SmartPortal.Common.ST.GetStringTemplate("IBTransferOutBank1", "TransferOutBankAccount" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);
                tmpl.SetAttribute("recieverAccount", hasPrint["recieverAccount"].ToString());
            }
            else
            {
                tmpl = SmartPortal.Common.ST.GetStringTemplate("IBTransferOutBank1", "TransferOutBankCMND" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);
                tmpl.SetAttribute("CMND", hasPrint["CMND"].ToString());
                tmpl.SetAttribute("issuePlace", hasPrint["issuePlace"].ToString());
                tmpl.SetAttribute("issuseDate", hasPrint["issuseDate"].ToString());
            }

            tmpl.SetAttribute("senderAccount",hasPrint["senderAccount"].ToString());
            tmpl.SetAttribute("senderBalance", hasPrint["senderBalance"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
            
            //tmpl.SetAttribute("recieverBalance", hasPrint["recieverBalance"].ToString());
            tmpl.SetAttribute("recieverName", hasPrint["recieverName"].ToString());
            tmpl.SetAttribute("recieverAdd", hasPrint["recieverAdd"].ToString());
            tmpl.SetAttribute("recieverBank", hasPrint["recieverBank"].ToString());
            //tmpl.SetAttribute("recieverCity", hasPrint["recieverCity"].ToString());
            //tmpl.SetAttribute("recieverNation", hasPrint["recieverNation"].ToString());
            
            //tmpl.SetAttribute("transferType", hasPrint["transferType"].ToString());
            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
            tmpl.SetAttribute("feeType", hasPrint["feeType"].ToString());
            tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
            tmpl.SetAttribute("desc", hasPrint["desc"].ToString());

            tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
            tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());

            //LAY TEN CHI NHANH
            //lay chi nhanh gui
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
