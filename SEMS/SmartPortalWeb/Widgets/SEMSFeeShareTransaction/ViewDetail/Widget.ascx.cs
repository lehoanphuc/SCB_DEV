using System;


public partial class Widgets_SEMSREGIONFEE_Detail_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucDetailBranch._TITLE = Resources.labels.view + ' ' + Resources.labels.TransactionShareFee;
    }
}
