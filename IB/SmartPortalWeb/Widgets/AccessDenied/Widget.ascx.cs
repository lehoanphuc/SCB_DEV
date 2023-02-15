using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common;

public partial class Widgets_AccessDenied_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["userName"] != null)
        //{
        //    if (Session["userName"].ToString().Trim() == new PortalSettings().portalSetting.UserNameDefault.Trim())
        //    {
        //        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["po"] != null)
        //        {
        //            switch (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["po"].ToString().Trim())
        //            {
        //                case "4":
        //                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["semsloginpage"]);
        //                    break;
        //                case "3":
        //                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["loginpage"]);
        //                    break;
        //                default:
        //                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["loginpage"]);
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["loginpage"]);
        //        }
        //    }
        //}
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/"+System.Configuration.ConfigurationManager.AppSettings["homeadmin"]);
    }
   
}
