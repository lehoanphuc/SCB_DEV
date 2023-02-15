using System;


public partial class Widgets_SEMSGroupDefinition_Detail_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucDetailGroupDefinition._TITLE = Resources.labels.groupDefinitionViewDetail;
    }
}
