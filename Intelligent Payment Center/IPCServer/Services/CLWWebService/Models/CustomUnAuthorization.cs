using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Security.Principal;
using CLWWebService.Util;

namespace CLWWebService.Models
{
    public class CustomUnAuthorization : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            Transactions trans = new Transactions();
            string statictoken = trans.DecryptData(ConfigurationManager.AppSettings["statictoken"].ToString().Trim());
            string staticuser = trans.DecryptData(ConfigurationManager.AppSettings["username"].ToString().Trim());
            if (actionContext.Request.Headers.GetValues("Authorization").First().Equals(statictoken))
            {
                actionContext.ControllerContext.RequestContext.Principal = new GenericPrincipal(new GenericIdentity(staticuser), new string[] { });
                return;
            }
            JObject jErrorResponse = new JObject();
            jErrorResponse.Add("ERRORCODE", "401");
            jErrorResponse.Add("ERRORDESC", ConfigurationManager.AppSettings["MsgCheckTokenFail"].ToString().Trim());
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent(jErrorResponse.ToString())
            };
            actionContext.Response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}