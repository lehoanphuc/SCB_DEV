using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using SmartPortal.ExceptionCollection;

public partial class Widgets_NewsManagement_Delete_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            NewsBLL NB = new NewsBLL();

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"] != null)
            {
                NB.Delete(Utility.IsInt(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"].Trim())));
                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"], System.Configuration.ConfigurationManager.AppSettings["NEWSID"], SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"].Trim(), "");
            }
            else
            {
                if (Session["newsIDDelete"] != null)
                {
                    string newsID = Session["newsIDDelete"].ToString();
                    string[] newsIDArray = newsID.Split('-');

                    foreach (string p in newsIDArray)
                    {
                        if (p != "")
                        {
                            NB.Delete(Utility.IsInt(p));
                            //Write Log
                            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["NEWS"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress,System.Configuration.ConfigurationManager.AppSettings["TBLNEWS"],System.Configuration.ConfigurationManager.AppSettings["NEWSID"],p,"");

                        }
                    }
                }
            }

        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ndec"], "Widgets_NewsManagement_Delete_Widget", "btnDelete_Click", bex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ndec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NewsManagement_Delete_Widget", "btnDelete_Click", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NewsManagement_Delete_Widget", "btnDelete_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        //redirect to page view
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewnews"]));
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewnews"]));
    }
}
