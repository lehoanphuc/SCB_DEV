using System;

public partial class Widgets_SEMSKYCDefinition_ViewDetail_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Widget1._TITLE = Resources.labels.KycDefinitionInformation + " - " + Resources.labels.view;
    }
}
