using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using System.Text;
using SmartPortal.BLL;
using System.Data;

public partial class Widgets_QuickLink_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sB = new StringBuilder();
            QuickLinkBLL qlB = new QuickLinkBLL();

            DataTable tblQL = qlB.Load(System.Globalization.CultureInfo.CurrentCulture.ToString());

            sB.Append("<div id='divQL'>");
            foreach (DataRow row in tblQL.Rows)
            {
                sB.Append("<p><img alt='' align='left' src='widgets/quicklink/images/iconarrow.gif' /><a href='" + SmartPortal.Common.Encrypt.EncryptURL(row["Link"].ToString().Equals("#") ? "#" : row["Link"].ToString()) + "'>" + row["Title"].ToString() + "</a></p>");
            }
            sB.Append("</div>");
            ltrQuickLink.Text = sB.ToString();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_QuickLink_Widget", "Page_Load", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_QuickLink_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

}
