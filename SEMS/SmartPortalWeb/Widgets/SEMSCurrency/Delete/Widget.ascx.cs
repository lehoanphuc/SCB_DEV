using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSCurrency_Delete_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucDeleteWidget._TITLE = Resources.labels.deletecurrency;
    }
}