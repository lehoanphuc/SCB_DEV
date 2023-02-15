using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;

public partial class Widgets_ExchangeRate_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            whtml.WidgetID = Utility.IsInt(WidgetID.Split('-')[0]);
        }
    }
}
