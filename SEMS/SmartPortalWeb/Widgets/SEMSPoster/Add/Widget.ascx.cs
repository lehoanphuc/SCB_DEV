using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSPoster_Add_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucAddPoster._TITLE = Resources.labels.addposter;
        ucAddPoster._IMAGE = "~/widgets/SEMSProduct/Images/product.png";
    }
}