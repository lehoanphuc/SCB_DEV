using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Antlr3.ST;


namespace SmartPortal.Common
{
    /// <summary>
    /// Summary description for StringTemplate
    /// </summary>
    public static class ST
    {
       
        public static StringTemplate GetStringTemplate(string widget_name, string templateFileName)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Widgets\\" + widget_name + "\\tmpl";
                StringTemplateGroup group = new StringTemplateGroup("SmartPortalST", path);
                StringTemplate query = group.GetInstanceOf(string.Format("{0}", templateFileName));
                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static StringTemplate GetEmailTemplate(string templateFileName)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "MailTemplate";
                StringTemplateGroup group = new StringTemplateGroup("SmartPortalST", path);
                StringTemplate query = group.GetInstanceOf(string.Format("{0}", templateFileName));
                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
