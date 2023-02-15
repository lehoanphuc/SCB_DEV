using InterbankTransferService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InterbankTransferService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                if (Transactions.CheckURLPath(Request.Path))
                {
                    Response.Headers.Add("Content-Security-Policy", "script-src 'self'");

                    int maxLength = string.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxAllowedContentLength"]) ? Common.MAXREQUESTSIZE : int.Parse(ConfigurationManager.AppSettings["MaxAllowedContentLength"].ToString());
                    if (Request.ContentLength > maxLength)
                    {
                        Context.Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
                        Response.Flush();
                        Response.SuppressContent = true;
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    switch (Request.HttpMethod)
                    {
                        case "POST":
                            if (!Request.ContentType.Equals(Common.jsontype))
                            {
                                Context.Response.StatusCode = (int)System.Net.HttpStatusCode.UnsupportedMediaType;
                                Response.Flush();
                                Response.SuppressContent = true;
                                HttpContext.Current.ApplicationInstance.CompleteRequest();
                            }
                            break;
                        case "GET":
                            break;
                        default:
                            Context.Response.StatusCode = (int)System.Net.HttpStatusCode.MethodNotAllowed;
                            Response.Flush();
                            Response.SuppressContent = true;
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                            break;
                    }
                    string accept = string.IsNullOrEmpty(Request.Headers["Accept"]) ? string.Empty : Request.Headers["Accept"];
                    if (!string.IsNullOrEmpty(accept))
                    {
                        if (!accept.Equals(Common.jsontype))
                        {
                            Context.Response.StatusCode = (int)System.Net.HttpStatusCode.NotAcceptable;
                            Response.Flush();
                            Response.SuppressContent = true;
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                    }
                    if (Request.Url.LocalPath.Equals(Path.Combine("/", ConfigurationManager.AppSettings[Common.GetTokenPath].ToString())))
                    {
                        if (!Request.HttpMethod.Equals("POST"))
                        {
                            Context.Response.StatusCode = (int)System.Net.HttpStatusCode.MethodNotAllowed;
                            Response.Flush();
                            Response.SuppressContent = true;
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                Transactions.ReturnErrDefault(Common.SystemError);
                Response.Flush();
                Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
            Response.Headers.Remove("X-Powered-By");
        }
    }
}
