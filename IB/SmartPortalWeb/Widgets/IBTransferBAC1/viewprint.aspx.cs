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
using System.Drawing.Printing;

public partial class Widgets_IBTransferBAC1_viewprint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string errorCode="";
        string erroDesc="";

        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBTransferBAC1", "TransferBAC" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);
            //02.12.2015 minh add to fix error 9999
            if (Session["print"] == null)
            {
                //check timeout log out
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["debug_timeoutsession"].ToString()))
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Template_Face1_Default", "Page_Load", "error in case view print", Request.Url.Query);
                }
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["loginpage"]);
            }
            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            tmpl.SetAttribute("senderAccount",hasPrint["senderAccount"].ToString());
            tmpl.SetAttribute("senderBalance", hasPrint["senderBalance"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            tmpl.SetAttribute("status", hasPrint["status"].ToString());
            tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
            tmpl.SetAttribute("recieverAccount", hasPrint["recieverAccount"].ToString());
            tmpl.SetAttribute("recieverBalance", hasPrint["recieverBalance"].ToString());
            tmpl.SetAttribute("recieverName", hasPrint["recieverName"].ToString());
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

            //lay chi nhanh nhan
            DataSet dsReceiverBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(hasPrint["receiverBranch"].ToString().Trim(), ref errorCode, ref erroDesc);
            if (errorCode == "0")
            {
                DataTable dtReceiverBranch = dsReceiverBranch.Tables[0];
                if (dtReceiverBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("receiverBranch", dtReceiverBranch.Rows[0]["BRANCHNAME"].ToString());
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
