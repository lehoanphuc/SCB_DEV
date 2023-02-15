using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_FeedBack_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //set session for send mail
        Session["url"] = null;
    }
}
