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

public partial class Widgets_SEMSHUBCMSLink_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //lblError.Text = "";
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {

        try
        {
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["id"] != null && SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["stt"] != null)
            {
                    new SmartPortal.SEMS.Contract().HubCms_Delete(SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["id"].ToString()), Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            }

            if (IPCERRORCODE == "0")
            {
                divDeleteConfirm.Visible = false;
                pnRole.Visible = false;
                pnResult.Visible = true;
                btsaveandcont.Visible = false;
                //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=1004"));
    }
}


