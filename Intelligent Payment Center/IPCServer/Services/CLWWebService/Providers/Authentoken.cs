using CLWWebService.Util;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CLWWebService.Providers
{
    public class Authentoken : OAuthAuthorizationServerProvider
    {
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            try
            {
                string token = context.Request.Headers["Authorization"].ToString().Trim().Split(' ')[1];
                string username = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(':')[0];
                context.Options.AccessTokenExpireTimeSpan = GetTimeSpan();
                ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, username));
                AuthenticationProperties authenpro = new AuthenticationProperties(new Dictionary<string, string> { { "as:client_id", username } });
                authenpro.AllowRefresh = true;
                AuthenticationTicket authenticationTicket = new AuthenticationTicket(oAuthIdentity, authenpro);
                context.Validated(authenticationTicket);
            }
            catch(Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Task.FromResult(0);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                Transactions transactions = new Transactions();
                string token = context.Request.Headers["Authorization"].ToString().Trim().Split(' ')[1];
                string[] tokesplit = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(':');
                string username = tokesplit[0];
                string password = tokesplit[1];
                if (transactions.CheckLogin(username, password))
                {
                    context.OwinContext.Set<string>("as:client_id", username);
                    context.Validated();
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Task.FromResult(0);
        }

        private TimeSpan GetTimeSpan(double value = 0, string timespan = "")
        {
            try
            {
                string timetype = !string.IsNullOrEmpty(timespan.Trim()) ? timespan.Trim().ToUpper() : ConfigurationManager.AppSettings["TimeType"].ToString().Trim().ToUpper();
                double expireTime = (value != 0) ? value : double.Parse(ConfigurationManager.AppSettings["AccessTokenExpireTime"].ToString());
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
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return TimeSpan.FromDays(1);
            }
        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            try
            {
                var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
                var currentClient = context.OwinContext.Get<string>("as:client_id");
                // enforce client binding of refresh token
                if (originalClient != currentClient)
                {
                    context.Rejected();
                    return;
                }
                // chance to change authentication ticket for refresh token requests
                var newId = new ClaimsIdentity(context.Ticket.Identity);
                newId.AddClaim(new Claim("newClaim", "refreshToken"));
                var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);
                context.Validated(newTicket);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}