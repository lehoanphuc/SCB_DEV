using SmartPortal.Common;
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


public partial class sems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] != null && Session["userName"].ToString() != "guest")
        {

            SmartPortal.BLL.UsersBLL UB = new SmartPortal.BLL.UsersBLL();
            try
            {
                UB.UpdateIsLogin(Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());
            }
            catch
            {
            }
        }
        Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;

        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=125"));
    }
}
