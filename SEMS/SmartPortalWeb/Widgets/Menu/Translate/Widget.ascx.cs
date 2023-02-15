using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_Menu_Translate_Widget : WidgetBase
{
    static string menuTitle;

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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Menu_Translate_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Menu_Translate_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    public void BindData()
    {
        if (Request["mid"] != null)
        {
            //Get Info Widget
            MenuBLL MB = new MenuBLL();
            MenuModel MM = new MenuModel();
            MM = MB.GetByID(Utility.KillSqlInjection(Request["mid"].ToString()), ddlLanguage.SelectedValue);

            txtTitle.Text = MM.MenuTitle;
            menuTitle = MM.MenuTitle;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            MenuBLL MB = new MenuBLL();
            MB.Translate(Utility.KillSqlInjection(Request["mid"]), ddlLanguage.SelectedValue, Utility.KillSqlInjection(txtTitle.Text.Trim()));

            //Write Log
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["TRANSLATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENULANG"], System.Configuration.ConfigurationManager.AppSettings["MENUID"], Request["mid"].ToString(), Request["mid"].ToString());
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["TRANSLATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENULANG"], System.Configuration.ConfigurationManager.AppSettings["LANGID"], ddlLanguage.SelectedValue, ddlLanguage.SelectedValue);
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["TRANSLATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENULANG"], System.Configuration.ConfigurationManager.AppSettings["MENUTITLE"], menuTitle, Utility.KillSqlInjection(txtTitle.Text.Trim()));

        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["mtec"], "Widgets_Menu_Translate_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["mtec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Menu_Translate_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Menu_Translate_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewmenu"]));
    }

    protected void btnExit_Click1(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewmenu"]));
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindData();
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Menu_Translate_Widget", "ddlLanguage_SelectedIndexChanged", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Menu_Translate_Widget", "ddlLanguage_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
