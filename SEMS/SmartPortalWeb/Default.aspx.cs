using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.DAL;
using SmartPortal.BLL;
using SmartPortal.Model;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.Common;
using SmartPortal.ExceptionCollection;
using System.IO;
using System.Configuration;
using System.Web.Services;
using System.Text;



public partial class Template_Face1_Default : System.Web.UI.Page
{
    private const String checkActionTimeout = "checkActionTimeout";
    //private Parameter para = new Parameter();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //lay serviceid theo pageid
            string serviceid = Session["serviceID"].ToString();

            var user = Session["userName"] != null ? Session["userName"].ToString() : "";
            if (Session["userName"] != null && Session["userName"].ToString() != "guest")
            {
                string urldirect = string.Empty;
                bool isvalid = new SmartPortal.BLL.UsersBLL().checkInvalidSession(Session["userName"].ToString(), Session["serviceID"].ToString(), Session["UUID"].ToString());
                System.Web.SessionState.SessionIDManager manager = new System.Web.SessionState.SessionIDManager();
                string SessionID = manager.GetSessionID(Context);
                if (!isvalid || SessionID != Session["ASP.NET_SessionId"].ToString())
                {
                    Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;
                    Session["serviceID"] = new PortalSettings().portalSetting.ServiceIDDefault;
                    Session["UUID"] = null;
                    Session["type"] = null;
                    string message = Resources.labels.yoursessionisincorrect;
                    string url = string.Empty;
                    switch (serviceid)
                    {
                        case "IB":
                            url = SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=220");
                            break;
                        case "SEMS":
                            url = SmartPortal.Common.Encrypt.EncryptURL("default.aspx?p=125");
                            break;
                    }

                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                    return;
                }
                int sessionSection = 3;
                int.TryParse(ConfigurationManager.AppSettings["sessionTimeout"].ToString().Trim(), out sessionSection);

                int sessiontimeout = sessionSection * 60;

                int sessionwarning = Convert.ToInt32(ConfigurationManager.AppSettings["SessionWarning"].ToString());
                int TimeoutinSecond = sessionSection * 60 - sessionwarning + 2;


                switch (serviceid)
                {
                    case "IB":
                        urldirect = SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=220");
                        break;
                    case "SEMS":
                        urldirect = SmartPortal.Common.Encrypt.EncryptURL("default.aspx?p=125");
                        break;
                }
                string keepalivepage = ConfigurationManager.AppSettings["KeepAlivePage"].ToString();
                string logout_url = "Default.aspx?p=220";
                string s1 = "timedMsg(" + sessiontimeout + "," + sessionwarning + ",'" + urldirect + "','" + TimeoutinSecond * 1000 + "','" + keepalivepage + "')";
                if(Session["userID"] != null)
                {
                    if (!Session["userID"].ToString().Equals("guest"))
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert2", s1, true);
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            //ShowPopUpMsg("sai session");
            //Session.Clear();
        }


        if (Session != null && Session[checkActionTimeout] != null)
        {
            //update time
            Session[checkActionTimeout] = DateTime.Now;
        }

        CheckPagePermit();

        LoadWidget();
        CheckLink();
    }
    #region Check Page Permit
    private void CheckPagePermit()
    {

        PagesBLL PB = new PagesBLL();
        Boolean flag = false; ;
        try
        {
            flag = PB.CheckPermit(Session["pageID"].ToString(), Session["userName"].ToString(), Session["serviceID"].ToString());
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Template_Face1_Default", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Template_Face1_Default", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        if (flag == false)
        {
            if (Session["userName"] != null)
            {
                if (Session["userName"].ToString().Trim() == new PortalSettings().portalSetting.UserNameDefault.Trim())
                {
                    Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=125"));
                }
                else
                {
                    Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["accessdenied"]));
                }
            }

        }
    }
    #endregion

    #region Load Widget
    public void LoadWidget()
    {
        try
        {
            DataTable iReadLoadWidget = new WidgetRightBLL().GetWidgetInPageForRole(Session["pageID"].ToString(), Session["userName"].ToString(), System.Globalization.CultureInfo.CurrentCulture.ToString(), Session["serviceID"].ToString());

            foreach (DataRow row in iReadLoadWidget.Rows)
            {
                SmartPortal.Control.PanelExt c = (SmartPortal.Control.PanelExt)Master.FindControl(row["Position"].ToString().Trim());
                if (c != null)
                {
                    WidgetBase c1 = (WidgetBase)LoadControl(row["WidgetLink"].ToString());
                    c1.SetID(row["WidgetID"].ToString() + "-" + row["WidgetPageID"].ToString());
                    c1.SetTitle(row["WidgetTitle"].ToString());
                    c1.SetIconpath(row["IconPath"].ToString());
                    c1.SetEnableTheme(bool.Parse(row["EnableTheme"].ToString()));
                    c1.SetShowTitle(bool.Parse(row["ShowTitle"].ToString()));
                    c.Controls.Add(c1);
                }
            }

        }
        catch (Exception ex)
        {
            //check timeout log out
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["debug_timeoutsession"].ToString()))
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Template_Face1_Default", "Page_Load", "error in case exception in load widget-" + ex.Message, Request.Url.Query);
            }

        }
    }
    #endregion

    private void CheckLink()
    {
        try
        {

            if (File.Exists(ConfigurationManager.AppSettings["linkpath"].ToString()))
            {
                StreamReader re = File.OpenText(ConfigurationManager.AppSettings["linkpath"].ToString());
                string input = null;
                while ((input = re.ReadLine()) != null)
                {
                    if (input.Trim() == Request.Url.AbsoluteUri.Trim())
                    {
                        // close the stream
                        re.Close();

                        Response.Redirect(ConfigurationManager.AppSettings["loginpage"].ToString());
                    }
                    else
                    {

                    }
                }
                re.Close();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["linkpath"], "Widgets_Login_Widget", "btnLogin_Click", ex.Message, Request.Url.Query);
        }
    }

    private void UpdateLoginTime()
    {
        try
        {
            UsersBLL UB = new UsersBLL();
            UB.UpdateIsLogin(Session["userName"].ToString(), DateTime.Now.AddMinutes(Session.Timeout).ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Login_Widget", "btnLogin_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            #region Set PortalID,PageID,RoleID in Session

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"] == null)
            {
                if (!Session["FLogin"].ToString().Trim().Equals("1"))
                {
                    Session["pageID"] = new PortalSettings().portalSetting.PageDefaultID;

                }
                else
                {
                    Session["pageID"] = "1065";
                }
            }
            else
            {
                Session["pageID"] = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString();
            }

            if (Session["userName"] == null)
            {
                Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;
            }
            if (Session["serviceID"] == null)
            {
                Session["serviceID"] = new PortalSettings().portalSetting.ServiceIDDefault;
            }

            #endregion

            #region Get pages Infomation
            PagesModel PM = new PagesModel();
            PagesBLL PB = new PagesBLL();

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"] != null)
            {

                PM = PB.GetPageInfo(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString()));

            }
            else
            {
                PM = PB.GetPageInfo(new PortalSettings().portalSetting.PageDefaultID);
            }

            Page.MasterPageFile = PM.MasterPage;
            Page.Theme = PM.Theme;
            Page.Title = ConfigurationManager.AppSettings["prefix"];// +PM.Title;
            #endregion

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Template_Face1_Default", "Page_PreInit", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }


    protected override void InitializeCulture()
    {

        try
        {
            string culture;

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["l"] == null)
            {
                if (Session["langID"] == null)
                {
                    culture = new PortalSettings().portalSetting.DefaultLang;
                }
                else
                {
                    culture = Session["langID"].ToString();
                }
            }
            else
            {
                culture = Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["l"].ToString());
                Session["langID"] = culture;
            }

            //OR This

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

            base.InitializeCulture();
        }

        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Template_Face1_Default", "InitializeCulture", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    [WebMethod]
    public static void UpdateNotifyStatus()
    {
        HttpContext.Current.Session["NotifyStatus"] = "3";
    }
    private bool CheckTimeOut()
    {
        int sessiontimeout = Convert.ToInt32(ConfigurationManager.AppSettings["SessionTimeout"].ToString());
        int iCountDown = Convert.ToInt32(ConfigurationManager.AppSettings["SessionWarning"].ToString());
        if (Session[checkActionTimeout] != null)
        {
            if ((DateTime.Parse(Session[checkActionTimeout].ToString())).AddSeconds(sessiontimeout).AddSeconds(iCountDown) <= DateTime.Now)
            {
                return false;
            }
        }
        return true;
    }
}
