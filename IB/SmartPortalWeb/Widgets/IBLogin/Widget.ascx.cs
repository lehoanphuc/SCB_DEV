using System;
using System.Data;
using SmartPortal.Security;
using System.Collections.Generic;
using SmartPortal.Common.Utilities;
using System.IO;
using SmartPortal.Common;
using System.Configuration;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBLogin_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["loginpage"].ToString(), true);
        return;
    }
}
