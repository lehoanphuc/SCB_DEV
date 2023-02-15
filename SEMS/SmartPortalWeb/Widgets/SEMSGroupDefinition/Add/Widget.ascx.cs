using System;

public partial class Widgets_SEMSGroupDefinition_Add_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucAddGroupDefinition._TITLE = Resources.labels.groupDefinitionAdd;
    }
}
