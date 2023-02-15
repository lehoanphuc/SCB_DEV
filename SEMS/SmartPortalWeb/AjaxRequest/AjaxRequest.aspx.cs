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
            if (!string.IsNullOrEmpty(Request.QueryString["mode"]))
            {
                string mode = Request.QueryString["mode"];

                switch (mode)
                {
                    case "wp":
                        string pageID = Request["p"].ToString();
                        string widgetID = Request["w"].ToString().Split('-')[0];
                        string widgetPageID = Request["w"].ToString().Split('-')[1];
                        string position = Request["ps"].ToString();
                        int order = int.Parse(Request["o"].ToString());

                        WidgetPageBLL WP = new WidgetPageBLL();
                        WP.Insert(widgetPageID, pageID, widgetID, position, order);
                        break;
                    case "rwip":
                        string wpid = Request["wpid"].ToString();

                        WidgetPageBLL WP1 = new WidgetPageBLL();
                        WP1.RemoveWidgetInPage(wpid);
                        break;
                    case "aa":
                        DataTable iReadLogin = new DataTable();
                        iReadLogin = new UsersBLL().Login(Request["u"].ToString().Trim(), Encryption.Encrypt(Request["p"].ToString().Trim()));
                        Response.Write(iReadLogin.Rows.Count);
                        break;
                    case "bb":
                        //update status login
                        if (Session["serviceID"] != null && Session["serviceID"].ToString() == "SEMS")
                        {
                            SmartPortal.BLL.UsersBLL UB = new SmartPortal.BLL.UsersBLL();
                            try
                            {
                                //UB.UpdateIsLogin(Session["userName"].ToString(), DateTime.Now.AddMinutes(0.3).ToString("dd/MM/yyyy HH:mm:ss"));
                                UB.UpdateIsLogin(Session["userName"].ToString(), DateTime.Now.AddMinutes(0.3).ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());
                            }
                            catch(Exception ex)
                            {
                            }
                        }
                        else if (Session["serviceID"] != null && Session["serviceID"].ToString() == "IB")
                        {
                            try
                            {
                                //new SmartPortal.IB.User().UpdateIsLogin(Session["userName"].ToString(), DateTime.Now.AddMinutes(0.3).ToString("dd/MM/yyyy HH:mm:ss"));
                                new SmartPortal.IB.User().UpdateIsLogin(Session["userName"].ToString(), DateTime.Now.AddMinutes(0.3).ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "AjaxRequest", "Page_Load", sex.Message, Request.Url.Query);
           // SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            Response.Write(Resources.labels.ajaxerror);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "AjaxRequest", "Page_Load", ex.Message, Request.Url.Query);
           // SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            Response.Write(Resources.labels.ajaxerror);
        }

        Response.End();
    }
}
