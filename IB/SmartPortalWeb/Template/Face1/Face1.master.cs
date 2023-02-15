using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;

public partial class MasterPages_Face1 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       toppanel.Visible = Utility.WidgetBaseAdmin(HttpContext.Current.Session["userName"].ToString());
    }
   
}
