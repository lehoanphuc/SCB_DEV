using System.Configuration;
using System.Web.Http;

namespace CLWWebService.Start_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: ConfigurationManager.AppSettings["FuncApiPath"].ToString(),
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}