using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_Widget_Translate_Widget :WidgetBase
{
    static string widgetid;
    static string langid;
    static string widgettitle;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //load language
                ddlLanguage.DataSource = new LanguageBLL().Load();
                ddlLanguage.DataTextField = "LangName";
                ddlLanguage.DataValueField = "LangID";
                ddlLanguage.DataBind();

                BindData();
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_Translate_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_Translate_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    public void BindData()
    {
        if (Request["wid"] != null)
        {
            //Get Info Widget
            WidgetsBLL WB = new WidgetsBLL();
            WidgetsModel WM = new WidgetsModel();
            WM = WB.GetWidgetTitleByID(Utility.KillSqlInjection(Request["wid"].ToString()), ddlLanguage.SelectedValue);

            txtTitle.Text = WM.WidgetTitle;
            widgettitle = WM.WidgetTitle;

            langid = WM.LangID;
            widgetid = WM.WidgetID.ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            WidgetsBLL WB = new WidgetsBLL();
            WB.Translate(Utility.KillSqlInjection(Request["wid"]), ddlLanguage.SelectedValue, Utility.KillSqlInjection(txtTitle.Text.Trim()));


            //Write Log
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["TRANSLATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETLANG"], System.Configuration.ConfigurationManager.AppSettings["WIDGETID"],widgetid, Request["wid"]);
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["TRANSLATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETLANG"], System.Configuration.ConfigurationManager.AppSettings["WIDGETTITLE"], widgettitle, Utility.KillSqlInjection(txtTitle.Text.Trim()));
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGET"], System.Configuration.ConfigurationManager.AppSettings["TRANSLATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETLANG"], System.Configuration.ConfigurationManager.AppSettings["LANGID"], langid, ddlLanguage.SelectedValue);
            
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["widgettiec"], "Widgets_Widget_Translate_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["widgettiec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_Translate_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_Translate_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewwidget"]));
    }
    protected void btnExit_Click1(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewwidget"]));
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindData();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Widget_Translate_Widget", "ddlLanguage_SelectedIndexChanged", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Widget_Translate_Widget", "ddlLanguage_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
