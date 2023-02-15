using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSBiller_ViewDetail_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucDetailsBiller._TITLE = Resources.labels.billerdetails;
    }
}