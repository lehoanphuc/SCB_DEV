using System;

public partial class Widgets_SEMSGroupDefinition_Edit_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucEditGroupDefinition._TITLE = Resources.labels.groupDefinitionEdit;
    }
}
