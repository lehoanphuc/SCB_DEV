using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using System.Configuration;
using SmartPortal.ExceptionCollection;

public partial class Widgets_BankNews_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            NewsBLL NB = new NewsBLL();

            //tin trong ngan hang
            inBankNews.Title.Text = Resources.labels.inbanknews;

            inBankNews.DlICN.DataSource = NB.LoadBankNews(System.Globalization.CultureInfo.CurrentCulture.ToString(), ConfigurationManager.AppSettings["tinnganhangtag"].ToString());
            inBankNews.DataBind();
            inBankNews.DlICN.RepeatLayout = RepeatLayout.Table;
            inBankNews.DlICN.CellSpacing = 3;
            inBankNews.DlICN.CellPadding = 2;

            //tin ngoai ngan hang
            outBankNews.Title.Text = Resources.labels.outbanknews;

            outBankNews.DlICN.DataSource = NB.LoadBankNews(System.Globalization.CultureInfo.CurrentCulture.ToString(), ConfigurationManager.AppSettings["tinngoainganhangtag"].ToString());
            outBankNews.DataBind();            
            outBankNews.DlICN.RepeatLayout = RepeatLayout.Table;
            outBankNews.DlICN.CellSpacing = 3;
            outBankNews.DlICN.CellPadding = 2;
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_BankNews_Widget", "Page_Load", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_BankNews_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
