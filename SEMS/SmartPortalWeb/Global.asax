<%@ Application Language="C#" %>

<script runat="server">


    private void LogoutSession()
    {
        try
        {
            if (Session.Count == 0)
            {
                Session.Add("first", "true");
                Session.Add(checkActionTimeout, DateTime.Now);

            }
            else if (Session["first"] != null && Session["first"].ToString() == "false")
            {
                Session["first"] = "true";
                Session[checkActionTimeout] = DateTime.Now;
            }
            Session_End(null, null);

            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=129"), false);

        }
        catch (Exception e)
        {

        }
    }
    private const String checkActionTimeout = "checkActionTimeout";
    public void checkStatus()
    {
        try
        {

            if (Session != null && Session[checkActionTimeout] != null &&
                DateTime.Now.Subtract((DateTime)Session[checkActionTimeout]).TotalSeconds > Session.Timeout * 60)
            {
                LogoutSession();
            }
            if (Session.Count > 0 && Session["first"].ToString() == "true")
            {
                Session["first"] = "false";
                return;
            }

            if (Session.IsNewSession)
            {
                LogoutSession();

            }
            if (Session["first"] != null && Session["first"].ToString() == "true")
            {
                Session["first"] = "false";
            }


        }
        catch (Exception e1)
        {
            //LogoutSession();
        }
    }
    public void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
    {
        //checkStatus();
    }
    protected void Application_AcquireRequestState(object sender, EventArgs e)
    {
        checkStatus();

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Session.Add("first", "true");
            Session.Add(checkActionTimeout, DateTime.Now);
        }
        else if (Session["first"] != null && Session["first"].ToString() == "false")
        {
            Session["first"] = "true";
            Session[checkActionTimeout] = DateTime.Now;
        }

    }

    void Session_End(object sender, EventArgs e)
    {
        SmartPortal.BLL.UsersBLL UB = new SmartPortal.BLL.UsersBLL();
        if (Session["userName"] != null)
        {
            UB.UpdateLLT(Session["userName"].ToString().Trim(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }
    }
    void Application_BeginRequest(Object sender, EventArgs e)
    {

        if (bool.Parse(ConfigurationManager.AppSettings["isEncryptURL"].ToString()))
        {
            if (Request.HttpMethod != "POST")
            {
                HttpContext context = HttpContext.Current;
                string url = HttpContext.Current.Request.Url.LocalPath.ToString();
                if (HttpContext.Current.Request.RawUrl.Contains(ConfigurationManager.AppSettings["routeurlslash"].ToString()) && HttpContext.Current.Request.RawUrl.Split('/').Length > 3)
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl.Replace(ConfigurationManager.AppSettings["routeurlslash"].ToString(), "/"));
                }
                if (context.Request.Url.OriginalString.Contains(ConfigurationManager.AppSettings["routeurlslash"].ToString() + ConfigurationManager.AppSettings["KeepAlivePage"].ToString()))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.OriginalString.Replace(ConfigurationManager.AppSettings["routeurlslash"].ToString(), "/"));
                }
                if (HttpContext.Current.Request.RawUrl.Contains(ConfigurationManager.AppSettings["routeurlslash"].ToString()) && HttpContext.Current.Request.RawUrl.Split('/').Length > 3)
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl.Replace(ConfigurationManager.AppSettings["routeurlslash"].ToString(), "/"));
                }
                else if (HttpContext.Current.Request.RawUrl.Contains(ConfigurationManager.AppSettings["routeurlslash"].ToString() + "sems.aspx"))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl.Replace(ConfigurationManager.AppSettings["routeurlslash"].ToString(), "/"));
                }
                else if (HttpContext.Current.Request.RawUrl.Contains(ConfigurationManager.AppSettings["routeurlslash"].ToString() + "CrystalImageHandler.aspx"))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl.Replace(ConfigurationManager.AppSettings["routeurlslash"].ToString(), "/"));
                }
            }
        }
    }
    void Application_Start(object sender, EventArgs e)
    {
        RegisterRoutes(System.Web.Routing.RouteTable.Routes);
    }

    static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
    {
        routes.MapPageRoute(ConfigurationManager.AppSettings["routeurl"].ToString(), ConfigurationManager.AppSettings["routeurl"].ToString(), "~/default.aspx");
        routes.MapPageRoute("EBKPAGES", ConfigurationManager.AppSettings["routeurl"].ToString() + "/{ID}", "~/default.aspx");
    }
    protected void Application_PreSendRequestHeaders()
    {
        Response.Headers.Remove("Server");
        Response.Headers.Remove("X-AspNet-Version");
        Response.Headers.Remove("X-AspNetMvc-Version");
        Response.Headers.Remove("X-Powered-By");
    }
</script>
