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

using SmartPortal.Common;
using SmartPortal.Constant;

public partial class Widgets_IBLogout_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // 3.2.2016 log timeout
            if (Session["userName"] == null)
            {

                //check timeout log out
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["debug_timeoutsession"].ToString()))
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Template_Face1_Default", "page_logout", "logout by session not null", Request.Url.Query);
                }

            }
            else
            {
                //check timeout log out
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["debug_timeoutsession"].ToString()))
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Template_Face1_Default", "page_logout", "logout but session not null", Request.Url.Query);
                }

            }


            Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;
            Session["serviceID"] = IPC.IB;
            Session["type"] = null;
            Session["userID"] = null;

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_LoginInfo_Widget", "lbLogout_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        Response.Redirect(ConfigurationManager.AppSettings["loginpage"].ToString());
    }
}
