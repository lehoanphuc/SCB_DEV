using System;


public partial class Widgets_SEMSBranch_Detail_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucDetailBranch._TITLE = Resources.labels.MerchantProfile + " - " + Resources.labels.view;
    }
}
