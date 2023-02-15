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


public partial class Widgets_SEMSFooter_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new SmartPortal.SEMS.Services().GetVarValue("VERSIONAPP");
            lblVersion.Text = dt.Rows[0]["VARVALUE"].ToString();
        }
    }
}
