using SmartPortal.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_Face2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        toppanel.Visible = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowAdmin"].ToString()) && Utility.WidgetBaseAdmin(HttpContext.Current.Session["userName"].ToString());
    }

}
