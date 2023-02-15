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

using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBFeedBack_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        int ProcessAppTable = 1;
        string SSAppTranID = "";
        try
        {
            if (Session["_FEEDBACKTRANSACTION"] != null)
            {
                SSAppTranID = Session["_FEEDBACKTRANSACTION"].ToString();
                string[] Procs = SSAppTranID.Split('#');
                foreach (string Proc in Procs)
                {
                    ProcessAppTable = new SmartPortal.IB.Transactions().DeleteFeedBack(Proc);
                    if (ProcessAppTable != 1)
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    
                }
                Session["_FEEDBACKTRANSACTION"] = null;
                //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));

            }
            else
            {
                ProcessAppTable = new SmartPortal.IB.Transactions().DeleteFeedBack(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"].ToString());
                if (ProcessAppTable != 1)
                {
                    throw new IPCException(IPCERRORDESC);
                } 
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

        if (ProcessAppTable == 1)
        {
            btsaveandcont.Visible = false;
            pnRole.Visible = false;
            lblError.Text = "<div class='block1'><div class='divGetInfoCust'><div class='handle'>" + Resources.labels.ketquagiaodich + "</div><div style='padding-top:10px; padding-bottom:10px; color:red; text-align:center'>" + Resources.labels.huytrasoatgiaodichthanhcong + "</div></div></div>";
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?po=3&p=394"));
    }
}


