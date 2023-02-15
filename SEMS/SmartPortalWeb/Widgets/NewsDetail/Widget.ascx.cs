using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Model;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_NewsDetail_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"] != null)
                {
                    NewsBLL NB = new NewsBLL();
                    NewsModel NM = new NewsModel();

                    NM = NB.LoadNewsByID(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"]), 1);

                    //gán GUI
                    lblTitle.Text = NM.Title;
                    lblDateCreated.Text = NM.DateCreated;
                    lblAuthor.Text = NM.Author;
                    lblContent.Text = NM.Content;

                    //set session for send mail
                    Session["url"] = Request.Url.ToString();
                }
                //else
                //{
                //    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tag"] != null)
                //    {
                //        NewsBLL NB = new NewsBLL();
                //        NewsModel NM = new NewsModel();

                //        NM = NB.LoadNewsByTag(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tag"],System.Globalization.CultureInfo.CurrentCulture.ToString());

                //        //gán GUI
                //        lblTitle.Text = NM.Title;
                //        //lblDateCreated.Text = NM.DateCreated;
                //        //lblAuthor.Text = NM.Author;
                //        lblContent.Text = NM.Content;
                //        Label1.Visible = false;
                //    }
                //}
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsDetail_Widget", "Page_Load", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsDetail_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
}
