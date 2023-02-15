using System;


public partial class Widgets_SEMSREGIONFEE_Add_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucAddBranch._TITLE = Resources.labels.add + ' ' + Resources.labels.feeShareDetail;
    }
}
