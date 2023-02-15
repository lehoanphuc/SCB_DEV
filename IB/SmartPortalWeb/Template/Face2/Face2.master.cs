using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;

public partial class MasterPages_Face2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        toppanel.Visible = tab.Visible = Utility.WidgetBaseAdmin(HttpContext.Current.Session["userName"].ToString());
        //toppanel.Visible = true;
    }

}
