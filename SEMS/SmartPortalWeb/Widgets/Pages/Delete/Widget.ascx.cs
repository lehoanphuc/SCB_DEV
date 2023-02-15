using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Pages_Delete_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewpage"]));
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            PagesBLL PB = new PagesBLL();

            if (Request["pid"] != null)
            {
                PB.Delete(Utility.KillSqlInjection(Request["pid"].Trim()));
                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress,System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"],System.Configuration.ConfigurationManager.AppSettings["PAGEID"],Request["pid"].Trim(),"");

            }
            else
            {
                if (Session["pageIDDelete"] != null)
                {
                    string pageID = Session["pageIDDelete"].ToString();
                    string[] pageIDArray = pageID.Split('-');

                    foreach (string p in pageIDArray)
                    {
                        if (p != "")
                        {
                            PB.Delete(p);
                            //Write Log
                            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["PAGE"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLPAGE"], System.Configuration.ConfigurationManager.AppSettings["PAGEID"], p, "");

                        }
                    }
                }
            }


        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["pdec"], "Widgets_Pages_Delete_Widget", "btnDelete_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["pdec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Pages_Delete_Widget", "btnDelete_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Pages_Delete_Widget", "btnDelete_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        //redirect to page view
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewpage"]));
    }
}
