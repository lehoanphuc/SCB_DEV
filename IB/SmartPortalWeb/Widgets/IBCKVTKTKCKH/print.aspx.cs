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

public partial class Widgets_IBCKVTKTKCKH_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string errorCode="";
        string erroDesc="";

        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBCKVTKTKCKH", "OpenFD" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            tmpl.SetAttribute("senderAccount",hasPrint["senderAccount"].ToString());
            tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
            tmpl.SetAttribute("product", hasPrint["product"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            tmpl.SetAttribute("status", hasPrint["status"].ToString());
            tmpl.SetAttribute("term", hasPrint["term"].ToString());
            tmpl.SetAttribute("rate", hasPrint["rate"].ToString());
            tmpl.SetAttribute("openDate", hasPrint["openDate"].ToString());
            tmpl.SetAttribute("expireDate", hasPrint["expireDate"].ToString());

            tmpl.SetAttribute("FDBalance", hasPrint["FDBalance"].ToString());
            tmpl.SetAttribute("receiverAccount", hasPrint["receiverAccount"].ToString());
            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
            tmpl.SetAttribute("senderBranch", hasPrint["senderBranch"].ToString());
            tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
            tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());
            //tmpl.SetAttribute("receiverBranch", hasPrint["receiverBranch"].ToString());

            

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
