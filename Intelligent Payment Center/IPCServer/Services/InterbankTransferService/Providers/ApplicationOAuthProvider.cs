using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Configuration;

namespace InterbankTransferService.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            context.Options.AccessTokenExpireTimeSpan = GetTimeSpan();
            ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.ClientId));
            AuthenticationProperties authenpro = new AuthenticationProperties(new Dictionary<string, string> { { "as:client_id", context.ClientId } });

            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "admin"));
            //AuthenticationProperties authenpro = new AuthenticationProperties(new Dictionary<string, string> { { "as:client_id", "admin" } });
            authenpro.AllowRefresh = true;
            AuthenticationTicket authenticationTicket = new AuthenticationTicket(oAuthIdentity, authenpro);
            context.Validated(authenticationTicket);
            return Task.FromResult(0);
        }


        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            string ipaddress;

            ipaddress = context.Request.RemoteIpAddress;
            if (context.TryGetBasicCredentials(out clientId, out clientSecret) || context.TryGetFormCredentials(out clientId, out clientSecret))
            {
                string errordesc = string.Empty;
                if (Transactions.CheckUserPass(clientId, clientSecret, context, ref errordesc))
                {
                    context.OwinContext.Set<string>("as:client_id", clientId);
                    context.Validated();
                }
                else
                {
                    context.SetError(errordesc);
                }
            }
            //context.OwinContext.Set<string>("as:client_id", "admin");
            //context.Validated();

            return Task.FromResult(0);
        }

        private TimeSpan GetTimeSpan(double value = 0, string timespan = "")
        {
            string timetype = !string.IsNullOrEmpty(timespan.Trim()) ? timespan.Trim().ToUpper() : ConfigurationManager.AppSettings[Common.TimeType].ToString().Trim().ToUpper();
            double expireTime = (value != 0) ? value : double.Parse(ConfigurationManager.AppSettings[Common.AccessTokenExpireTime].ToString());
            switch (timetype)
            {
                case "D":
                    return TimeSpan.FromDays(expireTime);

                case "H":
                    return TimeSpan.FromHours(expireTime);

                case "M":
                    return TimeSpan.FromMinutes(expireTime);

                case "S":
                    return TimeSpan.FromSeconds(expireTime);
                default:
                    return TimeSpan.FromDays(1);
            }
        }
    }
}