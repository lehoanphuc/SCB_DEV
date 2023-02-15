using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using SmartPortal.Security;
using System.Data;

public partial class Widgets_IBResetPasswords_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;
    static string pass;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["userNameTemp"] == null)
        //{
        //    Response.Redirect("~/Default.aspx");
        //}
        pnFocus.Visible = true;
        pnResult.Visible = false;
        lblError.Text = "";
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtUsername.Text = "";
        lblError.Text = "";
        txtEmail.Text = "";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //thoat
        Response.Redirect("~/Default.aspx");
    }
    void SendInfoLogin(string passwords,string fullname)
    {
        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBResetPasswords", "ContractApprove" + System.Globalization.CultureInfo.CurrentCulture.ToString());

            tmpl.SetAttribute("USER", txtUsername.Text.Trim());
            tmpl.SetAttribute("PASS", passwords);
            tmpl.SetAttribute("FULLNAME", fullname);

            //send mail
            SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], txtEmail.Text.Trim(), System.Configuration.ConfigurationManager.AppSettings["resetpasswordemailtitle"], tmpl.ToString());

            tmpl.RemoveAttribute("USER");
            tmpl.RemoveAttribute("PASS");
            tmpl.RemoveAttribute("FULLNAME");

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        DataTable tblIsLogin = new DataTable();
        UsersBLL UB = new UsersBLL();
        DataSet ds = new DataSet();
        DataTable iReadLogin;
        List<string> lstRoleName = new List<string>();

        try
        {


            //tblIsLogin = new SmartPortal.SEMS.BankUser().CheckValidTeller(Session["userName"].ToString(), Utility.KillSqlInjection(txtEmail.Text.Trim()));

            //if (tblIsLogin.Rows.Count==0)
            //{
            //    lblError.Text = Resources.labels.usernameoremailnotexist;
            //    return;
            //}
            iReadLogin = new SmartPortal.IB.User().CheckValidUser(Utility.KillSqlInjection(txtUsername.Text.Trim()), Utility.KillSqlInjection(txtEmail.Text.Trim()));
            if (iReadLogin.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.usernameoremailnotexist;
                return;
            }

            //cap nhat mat khau moi
            DataTable tblUser=new DataTable();
            string fullname = "";

            pass = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 6, 6));
            tblUser=new SmartPortal.IB.User().ResetPasswordUser(Utility.KillSqlInjection(txtUsername.Text.Trim()), Utility.KillSqlInjection(pass));
            pnFocus.Visible = false;
            pnResult.Visible = true;
            btnChange.Visible = false;
            btnReset.Visible = false;
            Button3.Visible = true;
            lblError.Text = "";

            if (tblUser.Rows.Count != 0)
            {
                fullname = tblUser.Rows[0]["FULLNAME"].ToString();
            }
            SendInfoLogin(Encryption.Decrypt(pass),fullname);

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
}
