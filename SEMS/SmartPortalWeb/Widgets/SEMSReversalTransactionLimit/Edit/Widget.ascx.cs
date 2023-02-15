using System;

public partial class Widgets_SEMSContractLevel_Edit_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucEditContractLevel._TITLE = Resources.labels.ReversalTranLimit + " Edit";
    }
}
