using System;
using System.Collections;
using System.Data;
using SmartPortal.ExceptionCollection;
using System.Web;

public partial class Widgets_IBCLGD_viewprint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string errorCode = "";
        string erroDesc = "";

        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBCLGD", "BatchTransfer" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cul"]);

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
            tmpl.SetAttribute("senderAccount", hasPrint["senderAccount"].ToString());
            tmpl.SetAttribute("senderBalance", hasPrint["senderBalance"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            tmpl.SetAttribute("status", hasPrint["status"].ToString());

            tmpl.SetAttribute("amount", hasPrint["totalamount"].ToString());
            tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
            tmpl.SetAttribute("feeType", hasPrint["feeType"].ToString());
            tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
            tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
            tmpl.SetAttribute("tranType", "Batch Transfer");
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
