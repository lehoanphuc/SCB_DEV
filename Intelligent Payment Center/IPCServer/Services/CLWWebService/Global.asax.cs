using CLWWebService.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CLWWebService
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //protected void Application_Start()
        //{
            
        //}

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            //Response.Headers.Set("Server","My httpd server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
            Response.Headers.Remove("X-Powered-By");
        }

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
            
        //}
    }
}
