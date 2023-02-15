using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_IconSlideRight_Widget : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            slidepage.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewtheme"];
            HyperLink1.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewlanguage"];
            HyperLink2.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["settinglink"];

            HyperLink3.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewuser"];
            HyperLink4.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewgroup"];

            
        }
    }
}
