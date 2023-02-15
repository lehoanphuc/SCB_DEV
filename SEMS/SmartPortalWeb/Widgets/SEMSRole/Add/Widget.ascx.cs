using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSRole_Add_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Widget._TITLE = Resources.labels.addgroup;
    }
}
