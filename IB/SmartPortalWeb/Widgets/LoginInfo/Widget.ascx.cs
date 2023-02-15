using System;
using System.Collections.Generic;

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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_LoginInfo_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
       
    }
    protected void lbLogout_Click(object sender, EventArgs e)
    {
        try
        {
            //update status login
            UsersBLL UB = new UsersBLL();
            SmartPortal.Common.Log.WriteLogFile("", "", "", "===session= " + Session["userName"].ToString());
            SmartPortal.Common.Log.WriteLogFile("", "", "", "===UUID= " + Session["UUID"].ToString());
            SmartPortal.Common.Log.WriteLogFile("", "", "", "===userName= " + Session["userName"].ToString());
            SmartPortal.Common.Log.WriteLogFile("", "", "", "===serviceID= " + Session["serviceID"].ToString());
            SmartPortal.Common.Log.WriteLogFile("", "", "", "===type= " + Session["type"].ToString());
            UB.UpdateIsLogin(Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());

            Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;
            Session["serviceID"] = new PortalSettings().portalSetting.ServiceIDDefault;
            Session["type"] = null;
            
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_LoginInfo_Widget", "lbLogout_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
         Response.Redirect("?po="+new PortalSettings().portalSetting.PortalDefaultID.ToString()+"&p="+new PortalSettings().portalSetting.PageDefaultID.ToString());
       
    }
}
