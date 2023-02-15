using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Language_Delete_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LanguageBLL MB = new LanguageBLL();

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["lid"] != null)
            {
                MB.Delete(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["lid"].Trim()));
                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["LANGUAGE"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLLANGUAGE"], System.Configuration.ConfigurationManager.AppSettings["LANGID"], Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["lid"].Trim()), "");

            }
            else
            {
                if (Session["langIDDelete"] != null)
                {
                    string langID = Session["langIDDelete"].ToString();
                    string[] langIDArray = langID.Split('#');

                    foreach (string p in langIDArray)
                    {
                        if (p != "")
                        {
                            MB.Delete(p);
                            //Write Log
                            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["LANGUAGE"], System.Configuration.ConfigurationManager.AppSettings["TBLLANGUAGE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress,System.Configuration.ConfigurationManager.AppSettings["TBLLANGUAGE"],System.Configuration.ConfigurationManager.AppSettings["LANGID"],p,"");

                        }
                    }
                }
            }

        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["tdec"], "Widgets_Theme_Delete_Widget", "btnDelete_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["tdec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Theme_Delete_Widget", "btnDelete_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Theme_Delete_Widget", "btnDelete_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        //redirect to page view
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewlanguage"]));
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        //redirect to page view
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewlanguage"]));
    }
}
