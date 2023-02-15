using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Model;
using SmartPortal.Common.Utilities;

public partial class Widgets_Setting_Widget : WidgetBase
{
    static string smtpserver;
    static string smtpport;
    static string smtpusername;
    static string smtppassword;
    static string roleadminid;
    static string usernamedefault;
    static string portaldefaultid;
    static string pagedefaultid;
    static string langdedaultid;
    static string logpath;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //load role
                ddlRoleAdmin.DataSource = new GroupBLL().Load();
                ddlRoleAdmin.DataTextField = "RoleName";
                ddlRoleAdmin.DataValueField = "RoleID";
                ddlRoleAdmin.DataBind();

                //load username
                ddlUserNameDefault.DataSource = new UsersBLL().Load();
                ddlUserNameDefault.DataTextField = "UserName";
                ddlUserNameDefault.DataValueField = "UserName";
                ddlUserNameDefault.DataBind();

                //load portal
                ddlPortalDefault.DataSource = new PortalBLL().Load();
                ddlPortalDefault.DataTextField = "PortalName";
                ddlPortalDefault.DataValueField = "PortalID";
                ddlPortalDefault.DataBind();

                //load page
                ddlPageDefault.DataSource = new PagesBLL().Load();
                ddlPageDefault.DataTextField = "PageName";
                ddlPageDefault.DataValueField = "PageID";
                ddlPageDefault.DataBind();

                //load language
                ddlLanguageDefault.DataSource = new LanguageBLL().Load();
                ddlLanguageDefault.DataTextField = "LangName";
                ddlLanguageDefault.DataValueField = "LangID";
                ddlLanguageDefault.DataBind();

                SettingsModel SM = new SettingsModel();
                SM = new SettingsBLL().LoadPortalSettings();

                //load common setup
                ddlRoleAdmin.SelectedValue = SM.RoleAdminID.ToString();
                roleadminid = SM.RoleAdminID.ToString();

                ddlUserNameDefault.SelectedValue = SM.UserNameDefault;
                usernamedefault = SM.UserNameDefault;

                ddlPortalDefault.SelectedValue = SM.PortalDefaultID.ToString();
                portaldefaultid = SM.PortalDefaultID.ToString();

                ddlPageDefault.SelectedValue = SM.PageDefaultID.ToString();
                pagedefaultid = SM.PageDefaultID.ToString();

                ddlLanguageDefault.SelectedValue = SM.DefaultLang;
                langdedaultid = SM.DefaultLang;

                txtLogPath.Text = SM.LogPath;
                logpath = SM.LogPath;

                //load mail setup
                txtSMTPServer.Text = SM.SmtpServer;
                smtpserver = SM.SmtpServer;

                txtPort.Text = SM.SmtpPort.ToString();
                smtpport = SM.SmtpPort.ToString();

                txtUserName.Text = SM.SmtpUserName;
                smtpusername = SM.SmtpUserName;

                txtPassword.Text = SM.SmtpPassword;
                smtppassword = SM.SmtpPassword;
            }
            catch (Exception ex)
            {
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SettingsBLL SB = new SettingsBLL();
            SB.Insert(Utility.KillSqlInjection(txtSMTPServer.Text.Trim()), Utility.IsInt(txtPort.Text.Trim()), Utility.KillSqlInjection(txtUserName.Text.Trim()), Utility.KillSqlInjection(txtPassword.Text.Trim()), Utility.IsInt(ddlRoleAdmin.SelectedValue), ddlUserNameDefault.SelectedValue, Utility.IsInt(ddlPortalDefault.SelectedValue), Utility.IsInt(ddlPageDefault.SelectedValue), ddlLanguageDefault.SelectedValue, Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy"), Utility.KillSqlInjection(txtLogPath.Text.Trim()));

            lblAlert.Text = Resources.labels.insertsucessfull;

            //Write Log
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["SETTING"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLSETTING"], System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"], smtpserver, Utility.KillSqlInjection(txtSMTPServer.Text.Trim()));
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["SETTING"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLSETTING"], System.Configuration.ConfigurationManager.AppSettings["SMTPPORT"], smtpport, Utility.KillSqlInjection(txtPort.Text.Trim()));
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["SETTING"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLSETTING"], System.Configuration.ConfigurationManager.AppSettings["SMTPUSERNAME"], smtpusername, Utility.KillSqlInjection(txtUserName.Text.Trim()));
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["SETTING"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLSETTING"], System.Configuration.ConfigurationManager.AppSettings["SMTPPASSWORD"], smtppassword, Utility.KillSqlInjection(txtPassword.Text.Trim()));
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["SETTING"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLSETTING"], System.Configuration.ConfigurationManager.AppSettings["ROLEADMINID"], roleadminid,ddlRoleAdmin.SelectedValue);
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["SETTING"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLSETTING"], System.Configuration.ConfigurationManager.AppSettings["USERNAMEDEFAULT"], usernamedefault, ddlUserNameDefault.SelectedValue);
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["SETTING"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLSETTING"], System.Configuration.ConfigurationManager.AppSettings["PORTALDEFAULTID"], portaldefaultid, ddlPageDefault.SelectedValue);
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["SETTING"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLSETTING"], System.Configuration.ConfigurationManager.AppSettings["PAGEDEFAULTID"], pagedefaultid, ddlPageDefault.SelectedValue);
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["SETTING"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLSETTING"], System.Configuration.ConfigurationManager.AppSettings["DEFAULTLANG"], langdedaultid, ddlLanguageDefault.SelectedValue);
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["SETTING"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLSETTING"], System.Configuration.ConfigurationManager.AppSettings["LOGPATH"], logpath, Utility.KillSqlInjection(txtLogPath.Text.Trim()));

        }
        catch (Exception ex)
        {
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["homeadmin"]));
    }
}
