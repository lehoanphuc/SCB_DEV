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


public partial class Widgets_SEMSContractReview_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["tmpl"] != null)
                {
                    string str = Session["tmpl"].ToString();
                    ltrContractReview.Text = str;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnURL"].ToString()));
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=141"));
    }
}
