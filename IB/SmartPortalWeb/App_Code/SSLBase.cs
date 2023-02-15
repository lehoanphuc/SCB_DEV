using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for SSLBase
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class RequireSSLAttribute : Attribute
{
    public static void Validate(IHttpHandler handler)
    {
        Type type = handler.GetType();
        Object[] objs = type.GetCustomAttributes(typeof(RequireSSLAttribute), true);
        if (objs != null && objs.Length > 0)
        {
            SwitchToSsl();
        }
    }
    private static void SwitchToSsl()
    {
#if DEBUG
            return;
#endif
        String baseUrl = HttpContext.Current.Request.Url.OriginalString;
        Uri uri = new Uri(baseUrl);
        String url = baseUrl.Replace(uri.Scheme, Uri.UriSchemeHttps);
        HttpContext.Current.Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(url), true);
    }
}
