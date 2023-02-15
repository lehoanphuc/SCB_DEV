using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using SmartPortal.Model;
using Antlr3.ST;
using SmartPortal.Security;

public partial class Widgets_ResetPassword_Widget :WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            UsersBLL UB = new UsersBLL();
            UsersModel UM = new UsersModel();
            UM=UB.GetUserInfo(Utility.KillSqlInjection(txtUserName.Text.Trim()));
            if (!string.IsNullOrEmpty(UM.UserName))
            {
                if (UM.Email != "")
                {
                    string password = System.IO.Path.GetRandomFileName().Substring(0, 6);

                    StringTemplate st = SmartPortal.Common.ST.GetStringTemplate("ResetPassword", "resetpasswordtemplate");
                    st.SetAttribute("firstname", UM.FirstName);
                    st.SetAttribute("password", password);

                    //update password
                    UB.UpdatePassword(Utility.KillSqlInjection(txtUserName.Text.Trim()), Encryption.Encrypt(password.Trim()));
                    //send mail
                    SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["mailfrom"], UM.Email, System.Configuration.ConfigurationManager.AppSettings["mailtitle"], st.ToString());

                    lblAlert.Text = Resources.labels.youcancheckmailtogetpassword;
                    txtUserName.Text = "";
                }
                else
                {
                    lblAlert.Text = Resources.labels.younothaveemailtosendpassword;
                }
            }
            else
            {
                lblAlert.Text = Resources.labels.usernamenotexists;
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_ResetPassword_Widget", "btnLogin_Click", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_ResetPassword_Widget", "btnLogin_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }
}
