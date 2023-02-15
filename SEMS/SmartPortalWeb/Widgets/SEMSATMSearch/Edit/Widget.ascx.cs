using System;

public partial class Widgets_SEMSATMSearch_Edit_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Widget1.ATMHeader = Resources.labels.chinhsuathongtinatm;

        }
    }
}
