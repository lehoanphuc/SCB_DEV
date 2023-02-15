using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;

public partial class Widgets_GoldPrice_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WHTMLGoldPrice.WidgetID = Utility.IsInt(WidgetID.Split('-')[0]);
        }
    }
    public override void SetID(string id)
    {
        base.SetID(id);
    }
    public override void SetTitle(string title)
    {
        base.SetTitle(title);
    }
}
