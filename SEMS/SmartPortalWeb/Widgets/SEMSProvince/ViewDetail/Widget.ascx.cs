using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSProvince_ViewDetail_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Widget.ProvinceHeader = Resources.labels.xemthongtinchitietprovince;
    }
}