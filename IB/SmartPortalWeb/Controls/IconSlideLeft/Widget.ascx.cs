using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_IconSlideLeft_Widget : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            slidepage.NavigateUrl="~/"+System.Configuration.ConfigurationManager.AppSettings["viewpage"];
            HyperLink1.NavigateUrl= "~/"+System.Configuration.ConfigurationManager.AppSettings["viewwidget"];
            HyperLink2.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["viewmenu"];

            HyperLink3.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["pagepermission"];
            HyperLink4.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["menupermission"];

            HyperLink5.NavigateUrl = "~/"+System.Configuration.ConfigurationManager.AppSettings["viewhtmlwidget"];
        }
    }
}
