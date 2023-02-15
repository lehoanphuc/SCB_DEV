using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Common;
using SmartPortal.BLL;

public partial class Widgets_LoginInfo_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["userName"] != null && Session["roleName"] != null && Session["lastLoginTime"] != null)
                {
                    lblUser.Text = Session["userName"].ToString();

                    List<string> lstRoleName = new List<string>();
                    lstRoleName = (List<string>)Session["roleName"];

                    foreach(string str in lstRoleName)
                    {
                        lblRole.Text += str + " ";
                    }
                    lblLastLoginTime.Text =Utility.FormatDatetime(Session["lastLoginTime"].ToString(),"dd/MM/yyyy HH:mm:ss",DateTimeStyle.DateTime);                    
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_LoginInfo_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
       
    }
    protected void lbLogout_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["IBIsMultilogin"].ToString()))
            {
                UsersBLL UB = new UsersBLL();
                UB.UpdateIsLogin(Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());
                Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;
                Session["serviceID"] = new PortalSettings().portalSetting.ServiceIDDefault;
                Session["type"] = null;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_LoginInfo_Widget", "lbLogout_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        //Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("?p=125"));
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("default.aspx?p=125"));
    }
}
