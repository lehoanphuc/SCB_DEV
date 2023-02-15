using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Widgets_SEMSCustFXLimit_Detail_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucDetailFX._TITLE = Resources.labels.FxTransactionLimitView;
        ucDetailFX._IMAGE = "~/widgets/SEMSCustFXLimit/Images/limit.png";
    }
}
