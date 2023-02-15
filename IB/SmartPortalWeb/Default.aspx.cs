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
using System.Web.Helpers;
using System.Linq;

public partial class Template_Face1_Default : System.Web.UI.Page
{
    private const string checkActionTimeout = "checkActionTimeout";
    private string PageDefaultID = "220";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            AntiForgery.Validate();
        }
        try
        {
            if (Session["userName"] != null && Session["userName"].ToString() != "guest")
            {
                if (!CheckTimeOut())
                {
                    string url = "iblogin.aspx";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", "alert('Your session has been expired. You will be redirected to Login page.'); window.location='" + Request.ApplicationPath + url + "';", true);
                    return;
                }
                System.Web.SessionState.SessionIDManager manager = new System.Web.SessionState.SessionIDManager();
                string SessionID = manager.GetSessionID(Context);
                string urldirect = string.Empty;
                bool isvalid = false;
                try
                {
                    string uuid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uuid"];
                    if (uuid != null && !uuid.Equals(Session["UUID"].ToString()))
                    {
                        Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;
                        Session["userID"] = new PortalSettings().portalSetting.UserNameDefault;
                        Session["serviceID"] = SmartPortal.Constant.IPC.SOURCEIBVALUE;
                        Session["UUID"] = null;
                        Session["type"] = null;
                        Response.Redirect(ConfigurationManager.AppSettings["loginpage"].ToString());
                        return;
                    }
                }
                catch { }

                //isvalid = new SmartPortal.BLL.UsersBLL().checkInvalidSession(Session["UserID"].ToString(), Session["serviceID"].ToString(), Session["UUID"].ToString());

                //if (!isvalid || SessionID != Session["ASP.NET_SessionId"].ToString())
                //{
                //    Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;
                //    Session["userID"] = new PortalSettings().portalSetting.UserNameDefault;
                //    Session["serviceID"] = SmartPortal.Constant.IPC.SOURCEIBVALUE;
                //    Session["UUID"] = null;
                //    Session["type"] = null;
                //    string message = Resources.labels.yoursessionisincorrect;
                //    string url = url = SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=220");
                //    string script = "window.onload = function(){ alert('";
                //    script += message;
                //    script += "');";
                //    script += "window.location = '";
                //    script += url;
                //    script += "'; }";
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                //    return;

                //}
                //int sessiontimeout = Convert.ToInt32(ConfigurationManager.AppSettings["SessionTimeout"].ToString());
                //int sessionwarning = Convert.ToInt32(ConfigurationManager.AppSettings["SessionWarning"].ToString());
                //int timeoutinSecond = sessiontimeout * 60 - sessionwarning - 5;
                //int setInterval = timeoutinSecond * 1000;
                //urldirect = SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=220");
                //string keepalivepage = ConfigurationManager.AppSettings["KeepAlivePage"].ToString();
                //string s1 = "timedMsg(" + 1 + "," + sessionwarning + ",'" + urldirect + "'," + setInterval + ",'" + keepalivepage + "');";
                //if (Session["pageID"].ToString() != "206")
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert2", s1, true); 
                //}
            }

            if (Session["userID"] == null) Response.Redirect(ConfigurationManager.AppSettings["loginpage"].ToString());
        }
        catch (Exception ex)
        {
            //ShowPopUpMsg("sai session");
            //Session.Clear();
        }

        if (Session != null && Session[checkActionTimeout] != null)
        {
            Session[checkActionTimeout] = DateTime.Now;
        }

        CheckPagePermit();
        LoadWidget();
        CheckLink();
    }
    #region Check Page Permit
    private void CheckPagePermit()
    {
        try
        {
            PagesBLL PB = new PagesBLL();
            Boolean flag = false;
            try
            {
                if (Session["userID"] == null) Response.Redirect(ConfigurationManager.AppSettings["loginpage"].ToString(), false);

                flag = PB.CheckPermit(Session["pageID"].ToString(), Session["userID"].ToString(), SmartPortal.Constant.IPC.SOURCEIBVALUE);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
            if (flag == false)
            {
                if (Session["userName"] != null)
                {
                    Response.Redirect(
                        Session["userName"].ToString().Trim() == new PortalSettings().portalSetting.UserNameDefault.Trim()
                            ? System.Configuration.ConfigurationManager.AppSettings["loginpage"]
                            : SmartPortal.Common.Encrypt.EncryptURL(
                                System.Configuration.ConfigurationManager.AppSettings["accessdenied"]));
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
        }
    }
    #endregion

    #region Load Widget
    public void LoadWidget()
    {
        try
        {
            DataTable iReadLoadWidget = new WidgetRightBLL().GetWidgetInPageForRole(Session["pageID"].ToString(), Session["userID"].ToString(), System.Globalization.CultureInfo.CurrentCulture.ToString(), SmartPortal.Constant.IPC.SOURCEIBVALUE);

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
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
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
                }
                re.Close();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            #region Set PortalID,PageID,RoleID in Session

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"] == null)
            {
                Session["pageID"] = PageDefaultID;
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
                Session["serviceID"] = SmartPortal.Constant.IPC.SOURCEIBVALUE;
            }

            if (Session["userID"] == null) Response.Redirect(ConfigurationManager.AppSettings["loginpage"].ToString(), false);
            #endregion

            #region Get pages Infomation
            PagesModel PM = new PagesModel();
            PagesBLL PB = new PagesBLL();

            PM = PB.GetPageInfo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"] != null ? Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString()) : PageDefaultID);

            Page.MasterPageFile = PM.MasterPage;
            Page.Theme = PM.Theme;
            Page.Title = ConfigurationManager.AppSettings["prefix"];
            #endregion

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }


    protected override void InitializeCulture()
    {
        try
        {
            string culture;

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["l"] == null)
            {
                culture = Session["langID"] == null ? new PortalSettings().portalSetting.DefaultLang : Session["langID"].ToString();
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
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
        if (Session[checkActionTimeout] != null)
        {
            if ((DateTime.Parse(Session[checkActionTimeout].ToString())).AddMinutes(sessiontimeout) <= DateTime.Now)
            {
                return false;
            }
        }
        return true;
    }
}
