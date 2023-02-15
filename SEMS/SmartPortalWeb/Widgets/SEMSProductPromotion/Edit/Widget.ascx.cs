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


public partial class Widgets_SEMSProductPromotion_Edit_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string clone = GetParamsPage("IsClone")[0].Trim();
        if (clone.Equals("true"))
        {
            ucEditprocess._TITLE = Resources.labels.addproductpromotion;
        }
        else
        {
            ucEditprocess._TITLE = Resources.labels.EditProdctPromotion;
        }


    }
}
