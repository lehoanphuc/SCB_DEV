using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Widget_Delete_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            WidgetsBLL WB = new WidgetsBLL();

            if (Request["wid"] != null)
            {
                WB.Delete(Utility.KillSqlInjection(Request["wid"].Trim()));
                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["WIDGETID"], Request["wid"].Trim(),"");

            }
            else
            {
                if (Session["widgetIDDelete"] != null)
                {
                    string widgetID = Session["widgetIDDelete"].ToString();
                    string[] widgetIDArray = widgetID.Split('-');

                    foreach (string p in widgetIDArray)
                    {
                        if (p != "")
                        {
                            WB.Delete(p);
                            //Write Log
                            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGET"], System.Configuration.ConfigurationManager.AppSettings["WIDGETID"],p, "");

                        }
                    }
                }
            }

        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["widgetdec"], "Widgets_Widget_Delete_Widget", "btnDelete_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["widgetdec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_Delete_Widget", "btnDelete_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_Delete_Widget", "btnDelete_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        //redirect to page view
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewwidget"]));
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewwidget"]));
    }
}
