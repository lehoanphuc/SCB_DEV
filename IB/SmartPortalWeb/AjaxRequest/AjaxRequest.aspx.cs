#region XD World Recipe V 3
// FileName: AjaxRequest.cs
// Author: Dexter Zafra
// Date Created: 2/14/2009
// Website: www.ex-designz.net
#endregion
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Security;
using SmartPortal.ExceptionCollection;

public partial class AjaxRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.Clear();

            //using for get
            if (!string.IsNullOrEmpty(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["mode"]))
            {
                string mode = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["mode"];

                switch (mode)
                {
                    case "wp":
                        string portalID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["po"].ToString();
                        string pageID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString();
                        string widgetID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["w"].ToString().Split('-')[0];
                        string widgetPageID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["w"].ToString().Split('-')[1];
                        string position = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ps"].ToString();
                        int order = int.Parse(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["o"].ToString());

                        WidgetPageBLL WP = new WidgetPageBLL();
                        WP.Insert(widgetPageID, pageID, widgetID, position, order);
                        break;
                    case "rwip":
                        string wpid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"].ToString();

                        WidgetPageBLL WP1 = new WidgetPageBLL();
                        WP1.RemoveWidgetInPage(wpid);
                        break;
                    case "aa":
                        DataTable iReadLogin = new DataTable();
                        iReadLogin = new UsersBLL().Login(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["u"].ToString().Trim(), Encryption.Encrypt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString().Trim()));
                        Response.Write(iReadLogin.Rows.Count);
                        break;
                    case "bb":
                        if (Session["serviceID"] != null && Session["serviceID"].ToString() == "IB")
                        {
                            try
                            {
                                //new SmartPortal.IB.User().UpdateIsLogin(Session["UserID"].ToString(), DateTime.Now.AddMinutes(0.3).ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                        break;
                }
            }

            //using for post
            if (!string.IsNullOrEmpty(Request.Form["mode"]))
            {
                string mode = Request.Form["mode"];

                switch (mode)
                {
                    case "aa":
                        DataTable iReadLogin = new DataTable();
                        iReadLogin = new UsersBLL().Login(Request.Form["u"].Trim(), Encryption.Encrypt(Request.Form["p"].Trim()));
                        Response.Write(iReadLogin.Rows.Count);
                        break;
                }
            }           

        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "AjaxRequest", "Page_Load", sex.ToString(), Request.Url.Query);
           // SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            Response.Write(Resources.labels.ajaxerror);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "AjaxRequest", "Page_Load", ex.ToString(), Request.Url.Query);
           // SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            Response.Write(Resources.labels.ajaxerror);
        }

        Response.End();
    }
}
