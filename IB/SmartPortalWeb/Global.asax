<%@ Application Language="C#" %>

<script RunAt="server">


    private void LogoutSession()
    {
        try
        {
            //FormsAuthentication.SignOut();
            //Session.Abandon();   

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

            //Response.Redirect("Default.aspx?p=220", false);
            if (Session["serviceID"].ToString() == "IB")
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                Response.Redirect("Default.aspx?po=4&p=129", false);
            }

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
            /*else if (Session != null && Session[checkActionTimeout] != null)
            {
                //update time
                Session[checkActionTimeout] = DateTime.Now;
            }*/
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
        // Code that runs when a new session is started       
        //Session.Timeout = 3;
        //the first time 

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
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.    
        //update last login time
        SmartPortal.BLL.UsersBLL UB = new SmartPortal.BLL.UsersBLL();
        if (Session["userName"] != null)
        {
            UB.UpdateLLT(Session["userName"].ToString().Trim(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        try
        {
            if (Session["serviceID"] != null && Session["serviceID"].ToString() == "IB")
            {
                Session["userName"] = new SmartPortal.Common.PortalSettings().portalSetting.UserNameDefault;
                Session["serviceID"] = new SmartPortal.Common.PortalSettings().portalSetting.ServiceIDDefault;
                Session["type"] = null;
            }
        }
        catch (Exception ex)
        {

        }

    }
    void Application_BeginRequest(Object sender, EventArgs e)
    {
        try
        {
            System.Globalization.CultureInfo newCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            newCulture.DateTimeFormat.DateSeparator = "/";
            System.Threading.Thread.CurrentThread.CurrentCulture = newCulture;
        }
        catch { }
        if (bool.Parse(ConfigurationManager.AppSettings["isEncryptURL"].ToString()))
        {
            if (Request.HttpMethod != "POST")
            //if (true)
            {


                HttpContext context = HttpContext.Current;
                //if (context.Request.RawUrl.ToString().IndexOf("default.aspx?") < 0 && Request.RawUrl.ToString().IndexOf("default.aspx/") > 0)
                //{
                //    Context.RewritePath(HttpContext.Current.Request.Url.ToString().Replace("default.aspx/", ""));
                //}
                //if (context.Request.RawUrl.ToString().IndexOf("default.aspx?") < 0 && Request.RawUrl.ToString().IndexOf("ibLogin.aspx/") > 0)
                //{
                //    Context.RewritePath(HttpContext.Current.Request.Url.ToString().Replace("ibLogin.aspx/", ""));
                //}
                string url = HttpContext.Current.Request.Url.LocalPath.ToString();

                if (context.Request.Url.OriginalString.Contains(ConfigurationManager.AppSettings["routeurlslash"].ToString() + ConfigurationManager.AppSettings["KeepAlivePage"].ToString()))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.OriginalString.Replace(ConfigurationManager.AppSettings["routeurlslash"].ToString(), "/"));
                }

                if (context.Request.Url.OriginalString.Contains("po="))
                {
                    //string fullOrigionalpath = Request.RawUrl.ToString();
                    //int start = fullOrigionalpath.IndexOf("?");
                    //int len = fullOrigionalpath.Length - start;
                    //string replacetext = fullOrigionalpath.Substring(start, len);

                    //string encrypttext = SmartPortal.Common.Encrypt.EncryptURL(replacetext);

                    //  Context.RewritePath(HttpContext.Current.Request.Path + "/fdafdsafdafdafda",true);
                    //HttpContext.Current.Response.Redirect(HttpContext.Current.Request.FilePath + "/" + encrypttext);

                    //HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute("~/" + ConfigurationManager.AppSettings["routeurl"].ToString()) + "/" + encrypttext);

                    //Context.RewritePath("/" + encrypttext);
                    //Context.RewritePath(encrypttext, true);

                    // HttpContext.Current.Response.RedirectLocation = HttpContext.Current.Request.Path + "/fdafdsafdafdafda";

                    //Context.RewritePath(fullOrigionalpath.Replace(replacetext, "?d=fdafdafafadfadfdaffdsfdsfdafafds"));
                    //context.RewritePath(path, string.Empty, decryptedQuery);
                }
                else if (HttpContext.Current.Request.RawUrl.Contains(ConfigurationManager.AppSettings["routeurlslash"].ToString()) && HttpContext.Current.Request.RawUrl.Split('/').Length > 3)
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl.Replace(ConfigurationManager.AppSettings["routeurlslash"].ToString(), "/"));
                }
                else if (HttpContext.Current.Request.RawUrl.Contains(ConfigurationManager.AppSettings["routeurlslash"].ToString() + "ibLogin.aspx"))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl.Replace(ConfigurationManager.AppSettings["routeurlslash"].ToString(), "/"));
                }
                else if (HttpContext.Current.Request.RawUrl.Contains(ConfigurationManager.AppSettings["routeurlslash"].ToString() + "sems.aspx"))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl.Replace(ConfigurationManager.AppSettings["routeurlslash"].ToString(), "/"));
                }
                else if (HttpContext.Current.Request.RawUrl.Contains("&l="))
                {
                    string fullOrigionalpath = Request.RawUrl.ToString();
                    int start = fullOrigionalpath.IndexOf("?");
                    int len = fullOrigionalpath.Length - start;
                    string replacetext = fullOrigionalpath.Substring(start, len);
                    string encrypttext = SmartPortal.Common.Encrypt.EncryptURL(replacetext);
                    HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute("~/" + ConfigurationManager.AppSettings["routeurl"].ToString()) + "/" + encrypttext);
                }
            }
        }//end if check isencrypt
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
