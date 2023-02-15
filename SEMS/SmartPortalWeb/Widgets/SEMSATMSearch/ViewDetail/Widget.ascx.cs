using System;

public partial class Widgets_SEMSATMSearch_ViewDetail_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Widget1.ATMHeader = Resources.labels.xemthongtinchitietmayATM;
        }
    }
}
