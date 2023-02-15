using System;

public partial class Widgets_SEMSREGIONFEE_Edit_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucEditBranch._TITLE = Resources.labels.edit + ' ' + Resources.labels.feeShareDetail;
    }
}
