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
using System.Net.Http.Formatting;

namespace InterbankTransferService.Providers
{
    public class CustomUnAuthorization : AuthorizeAttribute
    {
        public CustomUnAuthorization()
        {
            
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!IsAuthorized(actionContext))
            {
                JObject jErrorResponse = new JObject();
                jErrorResponse.Add("result", "Authorization has been denied for this request.");
                if (jErrorResponse != null) actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, jErrorResponse);
            }
        }
    }
}