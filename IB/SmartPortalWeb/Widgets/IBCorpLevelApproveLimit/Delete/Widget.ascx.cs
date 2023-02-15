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

public partial class Widgets_IBCorpUserApproveLimit_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        DataSet ProductLMTable = new DataSet();
        string SSProTranCCYID = "";
        try
        {
            if (Session["_3thamso"] != null)
            {
                SSProTranCCYID = Session["_3thamso"].ToString();
                string[] Alls = SSProTranCCYID.Split('#');
                foreach (string All in Alls)
                {
                    string[] pros = All.Split('|');
                    ProductLMTable = new SmartPortal.SEMS.Transactions().DeleteLevelApproveLimit(pros[0], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {

                            //Response.Redirect("~/Default.aspx?po=4&p=141");
                        }
                    
                }
                Session["_3thamso"] = null;
                //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));

            }
            else
            {
                ProductLMTable = new SmartPortal.SEMS.Transactions().DeleteLevelApproveLimit(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                
            }
        }
        catch (Exception ex)
        { }

        if (IPCERRORCODE == "0")
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?po=3&p=350"));
            //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));
        }
        else 
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSSetLimitTeller_Delete_Widget", "btsaveandcont_Click", IPCERRORDESC, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
    }
}


