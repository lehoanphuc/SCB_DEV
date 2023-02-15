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
using SmartPortal.Common;


public partial class Widgets_SEMSContractReview_Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(new PortalSettings().portalSetting.DefaultLang);
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

        if (Session["userName"] == null || Session["userName"].ToString() == "guest")
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"));
        try
        {
            if (!IsPostBack)
            {
                if (Session["tmpl"] != null)
                {
                    string str = "<div><img src='../../../Images/logo.png' style='height:70px;' /><br/><br/></div>";
                    str += Session["tmpl"].ToString();
                    str += "<div><br/><p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'>www.scb.co.th/en/personal-banking.html</ a></p><span style='font-weight:bold;'>" + Resources.labels.AbankthankyouforusingAYAebankingservices + "!	</span></div>";
                    ltrContractReview.Text = str;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}
