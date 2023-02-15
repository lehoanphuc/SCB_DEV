using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSFastBanking_ViewDetail_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Widget1.ATMHeader = "XEM THÔNG TIN CHI TIẾT ATM";
        }
    }
}
