using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.Security;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Login_Widget : WidgetBase
{
    private const string LOGINCOOKIE = "IBLOGIN";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //get info login from cookie
                if (Request.Cookies[LOGINCOOKIE] != null)
                {
                    HttpCookie getCookie = Request.Cookies.Get(LOGINCOOKIE);
                    string userName = getCookie.Values["username"].ToString();
                    //string password = getCookie.Values["password"].ToString();

                    txtUserName.Text = userName;
                    //txtPassword.Attributes.Add("value",Encryption.Decrypt(password));
                    cbRememberMe.Checked = true;
                }
                txtPassword.AutoCompleteType = AutoCompleteType.Disabled;
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Login_Widget", "Page_Load", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
            UsersBLL UB = new UsersBLL();
            DataTable tblIsLogin = new DataTable();
            DataTable iReadLogin;
            List<string> lstRoleName=new List<string>();

            try
            {     
                //Remember me
                if (cbRememberMe.Checked)
                {
                    HttpCookie myCookie = new HttpCookie(LOGINCOOKIE);
                    Response.Cookies.Remove(LOGINCOOKIE);
                    Response.Cookies.Add(myCookie);
                    myCookie.Values.Add("username", Utility.KillSqlInjection(txtUserName.Text.Trim()));
                    //myCookie.Values.Add("password", Utility.KillSqlInjection(Encryption.Encrypt(txtPassword.Text.Trim())));
                    myCookie.Expires = DateTime.Now.AddDays(15);
                }
                else //thaity modify at 26/6/2014
                {
                    HttpCookie myCookie = new HttpCookie(LOGINCOOKIE);
                    if (myCookie != null)
                    {
                        myCookie.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(myCookie);

                    }


                }

                iReadLogin = UB.Login(Utility.KillSqlInjection(txtUserName.Text.Trim()), Utility.KillSqlInjection(Encryption.Encrypt(txtPassword.Text.Trim())));

                if (iReadLogin.Rows.Count == 0)
                {
                    lblAlert.Text = Resources.labels.loginfailed;
                    return;
                }
                 //get status login         
                tblIsLogin = UB.GetIsLogin(Utility.KillSqlInjection(txtUserName.Text.Trim()),DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                if (tblIsLogin.Rows.Count == 0)
                {
                    lblAlert.Text = Resources.labels.thisusernameloginbyanotheruser;
                    return;
                }

                //update last login time
                UB.UpdateLLT(Utility.KillSqlInjection(txtUserName.Text.Trim()), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                lstRoleName = new List<string>();
                foreach (DataRow row in iReadLogin.Rows)
                {
                    //write info in session   
                    Session["userName"] = row["UserName"].ToString();
                    lstRoleName.Add(row["RoleName"].ToString());
                    Session["lastLoginTime"] = SmartPortal.Common.Utilities.Utility.IsDateTime2(row["LastLoginTime"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    Session["roleName"] = lstRoleName;
                    Session["serviceID"] = "SMP";
                }

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["LOGIN"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress,System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],"","","1");

               
            }

            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Login_Widget", "btnLogin_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Login_Widget", "btnLogin_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }               
            
            try
            {
                //update status login
                UB.UpdateIsLogin(Utility.KillSqlInjection(txtUserName.Text.Trim()), DateTime.Now.AddMinutes(0.3).ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Login_Widget", "btnLogin_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
            //redirect
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["homeadmin"].ToString()));
            Response.End();
             
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        //redirect
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["homebank"].ToString()));
    }
    protected void lbForgotPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["resetpasswordlink"]));
    }
}
