using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using System.Configuration;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IndividualCorporatesNews_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CategoryBLL CB = new CategoryBLL();
           
            //ca nhan
            iN.Title.Text = Resources.labels.individual;

            iN.DlICN.DataSource= CB.LoadInvidualCorporatesNews(System.Globalization.CultureInfo.CurrentCulture.ToString(), ConfigurationManager.AppSettings["canhantag"].ToString());
            iN.DataBind();
            iN.DlICN.RepeatColumns = 2;
            iN.DlICN.RepeatLayout = RepeatLayout.Table;
            iN.DlICN.CellSpacing = 3;
            iN.DlICN.CellPadding = 2;

            //ca nhan
            cN.Title.Text = Resources.labels.corporates;

            cN.DlICN.DataSource = CB.LoadInvidualCorporatesNews(System.Globalization.CultureInfo.CurrentCulture.ToString(), ConfigurationManager.AppSettings["doanhnghieptag"].ToString());
            cN.DataBind();
            cN.DlICN.RepeatColumns = 2;
            cN.DlICN.RepeatLayout = RepeatLayout.Table;
            cN.DlICN.CellSpacing = 3;
            cN.DlICN.CellPadding = 2;
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_IndividualCorporatesNews_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IndividualCorporatesNews_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
