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

public partial class Widgets_IBTransferBAC1_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string errorCode="";
        string erroDesc="";
        if (Session["userID"] == null)
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"));
        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBRegisterEWallet", "IBRegisterEWallet" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);
            //02.12.2015 minh add to fix error 9999
            if (Session["print"] == null)
            {
                //check timeout log out
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["debug_timeoutsession"].ToString()))
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Template_Face1_Default", "Page_Load", "error in case print", Request.Url.Query);
                }
               Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
            }
            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            tmpl.SetAttribute("senderAccount", hasPrint.ContainsKey("senderAccount") ? hasPrint["senderAccount"].ToString() : "");
            tmpl.SetAttribute("senderName", hasPrint.ContainsKey("senderName") ? hasPrint["senderName"].ToString() : "");
            tmpl.SetAttribute("tranID", hasPrint.ContainsKey("tranID") ? hasPrint["tranID"].ToString() : "");
            tmpl.SetAttribute("tranDate", hasPrint.ContainsKey("tranDate") ? hasPrint["tranDate"].ToString() : "");
            tmpl.SetAttribute("WalletType", hasPrint.ContainsKey("WalletType") ? hasPrint["WalletType"].ToString() : "");
            tmpl.SetAttribute("PhoneNo", hasPrint.ContainsKey("PhoneNo") ? hasPrint["PhoneNo"].ToString() : "");
            tmpl.SetAttribute("RefID", hasPrint.ContainsKey("RefID") ? hasPrint["RefID"].ToString() : "");

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
            ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'> window.print(); window.close(); </script>");
            SmartPortal.Common.Log.WriteLogFile("test print", "", "", "test");
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
