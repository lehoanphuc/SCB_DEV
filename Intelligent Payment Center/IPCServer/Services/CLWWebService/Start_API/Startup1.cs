using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using CLWWebService.Providers;
using Owin;
using System;
using System.Configuration;
using System.IO;
using System.Web.Http;

[assembly: OwinStartup(typeof(CLWWebService.Start_API.Startup1))]

namespace CLWWebService.Start_API
{
    public partial class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions2);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }

        public static OAuthAuthorizationServerOptions OAuthOptions2 { get; private set; }

        static Startup1()
        {
            OAuthOptions2 = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString(Path.Combine("/", ConfigurationManager.AppSettings["GetTokenPath"].ToString())),
                Provider = new Authentoken(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                RefreshTokenProvider = new Authenrefreshtoken(),
                AllowInsecureHttp = true
            };
        }
    }
}