using System;
using System.Collections;
using System.Data;
using SmartPortal.Common;
using SmartPortal.ExceptionCollection;
public partial class Widgets_SEMSLISTHISTORYAPROVE_print : System.Web.UI.Page
{
    string errorCode = "";
    string erroDesc = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(new PortalSettings().portalSetting.DefaultLang);
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

        if (Session["userName"] == null || Session["userName"].ToString() == "guest")
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"));
        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            Hashtable hasPrint = new Hashtable();
            hasPrint = (Hashtable)Session["print"];
            switch (hasPrint["IPCTRANCODE"].ToString())
            {
                case "MB000030":
                case "AMBPMWALLET"://AM Bill payment
                case "MBBPMBANKACT"://CMB Bill payment
                case "WLBPMWALLET"://CWL Bill payment
                    tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSViewLogTransactions", "BillPaymentSuccessful");
                    tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
                    tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());
                    tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
                    tmpl.SetAttribute("senderAccount", hasPrint["senderAccount"].ToString());
                    tmpl.SetAttribute("BillerName", hasPrint["BillerName"].ToString());
                    //tmpl.SetAttribute("corpName", hasPrint["corpName"].ToString());
                    //tmpl.SetAttribute("serviceName", hasPrint["serviceName"].ToString());
                    //tmpl.SetAttribute("refindex1", hasPrint["refindex1"].ToString());
                    //tmpl.SetAttribute("refvalue1", hasPrint["refvalue1"].ToString());
                    //tmpl.SetAttribute("refindex2", hasPrint["refindex2"].ToString());
                    //tmpl.SetAttribute("refvalue2", hasPrint["refvalue2"].ToString());
                    tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
                    tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
                    tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
                    tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
                    tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
                    tmpl.SetAttribute("status", hasPrint["status"].ToString());
                    break;
                case "IBCBTRANSFER":// Cross
                case "MBCBTRANSFER":// Cross
                    tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSViewLogTransactions", "TransactionSuccessfulAbank");
                    tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
                    tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());
                    if (hasPrint["senderName"].ToString().Equals("") && hasPrint["senderAccount"].ToString().Equals(""))
                    {
                        tmpl.SetAttribute("showHideSender", "none");
                    }
                    else
                    {
                        tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
                        tmpl.SetAttribute("lblSender", "Debit account");
                        tmpl.SetAttribute("senderAccount", hasPrint["senderAccount"].ToString());
                    }
                    if (hasPrint["recieverName"].ToString().Equals("") && hasPrint["recieverAccount"].ToString().Equals(""))
                    {
                        tmpl.SetAttribute("showHideRecevier", "none");
                    }
                    else
                    {
                        tmpl.SetAttribute("recieverName", hasPrint["recieverName"].ToString());
                        tmpl.SetAttribute("lblReciever", "Credit account");
                        tmpl.SetAttribute("recieverAccount", hasPrint["recieverAccount"].ToString());
                    }
                    if (hasPrint["telecomname"].ToString().Equals(""))
                    {
                        tmpl.SetAttribute("showHideTopUp", "none");
                    }
                    else
                    {
                        tmpl.SetAttribute("phoneNo", hasPrint["phoneNo"].ToString());
                        tmpl.SetAttribute("telecomname", hasPrint["telecomname"].ToString());
                    }
                    tmpl.SetAttribute("tranType", hasPrint["tranType"].ToString());
                    tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
                    tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
                    tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
                    tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
                    tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
                    tmpl.SetAttribute("status", hasPrint["status"].ToString());
                    break;
                default:
                    tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSViewLogTransactions", "TransactionSuccessfulAbank");
                    tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
                    tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());
                    if (hasPrint["senderName"].ToString().Equals("") && hasPrint["senderAccount"].ToString().Equals(""))
                    {
                        tmpl.SetAttribute("showHideSender", "none");
                    }
                    else
                    {
                        tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
                        if (hasPrint["senderAccount"].ToString().Length < 13)
                        {
                            tmpl.SetAttribute("lblSender", "Sender Phone");
                        }
                        else
                        {
                            tmpl.SetAttribute("lblSender", "Debit account");
                        }
                        tmpl.SetAttribute("senderAccount", hasPrint["senderAccount"].ToString());
                    }
                    if (hasPrint["recieverName"].ToString().Equals("") && hasPrint["recieverAccount"].ToString().Equals(""))
                    {
                        tmpl.SetAttribute("showHideRecevier", "none");
                    }
                    else
                    {
                        tmpl.SetAttribute("recieverName", hasPrint["recieverName"].ToString());
                        if (hasPrint["recieverAccount"].ToString().Length < 16)
                        {
                            tmpl.SetAttribute("lblReciever", "Reciever Phone");
                        }
                        else
                        {
                            tmpl.SetAttribute("lblReciever", "Credit account");
                        }
                        tmpl.SetAttribute("recieverAccount", hasPrint["recieverAccount"].ToString());
                    }
                    if (hasPrint["telecomname"].ToString().Equals(""))
                    {
                        tmpl.SetAttribute("showHideTopUp", "none");
                    }
                    else
                    {
                        tmpl.SetAttribute("phoneNo", hasPrint["phoneNo"].ToString());
                        tmpl.SetAttribute("telecomname", hasPrint["telecomname"].ToString());
                    }
                    tmpl.SetAttribute("tranType", hasPrint["tranType"].ToString());
                    tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
                    tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
                    tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
                    tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
                    tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
                    tmpl.SetAttribute("status", hasPrint["status"].ToString());
                    break;
            }



            ltrPrint.Text = tmpl.ToString();
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pt"] != null)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pt"].ToString().ToUpper() == "P")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'> window.print();</script>");
                }
            }
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