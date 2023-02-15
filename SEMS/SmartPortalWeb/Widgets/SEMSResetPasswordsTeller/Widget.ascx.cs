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

public partial class Widgets_SEMSResetPasswordsTeller_Widget : WidgetBase
{
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    private static string pass = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtUserName.Text = string.Empty;
        lblError.Text = string.Empty;
        txtEmail.Text = string.Empty;
        pnFocus.Enabled = true;
        btnChange.Enabled = true;
    }
    void SendInfoLogin(string passwords)
    {
        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSResetPasswordsTeller", "ContractApprove" + System.Globalization.CultureInfo.CurrentCulture.ToString());
            tmpl.SetAttribute("Username", txtUserName.Text.Trim());
            tmpl.SetAttribute("Password", passwords);
            SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], txtEmail.Text.Trim(), System.Configuration.ConfigurationManager.AppSettings["resetpasswordemailtitle"], tmpl.ToString());
            SmartPortal.Common.Log.WriteLogFile("", "", "", "send email reset password to   ---" + txtEmail.Text.Trim());
            tmpl.RemoveAttribute("USER");
            tmpl.RemoveAttribute("PASS");
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
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
            iReadLogin = new SmartPortal.SEMS.BankUser().CheckValidTeller(Utility.KillSqlInjection(txtUserName.Text.Trim()), Utility.KillSqlInjection(txtEmail.Text.Trim()));
            if (iReadLogin.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.usernameoremailnotexist;
                return;
            }
            int passlen = 0;
            DataSet dspolicy = new DataSet();
            dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyConditionforreset(string.Empty, string.Empty, string.Empty, string.Empty, SmartPortal.Constant.IPC.SEMS, txtUserName.Text.ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dt = dspolicy.Tables[0];
                passlen = Convert.ToInt32(dt.Rows[0]["minpwdlen"].ToString());
            }
            string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlen, passlen).ToString();
            string passoldenc = Encryption.Encrypt(passreveal);
            string pass = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, txtUserName.Text.Trim());
            new SmartPortal.SEMS.BankUser().ResetPasswordTeller(Utility.KillSqlInjection(txtUserName.Text.Trim()), Utility.KillSqlInjection(txtEmail.Text.Trim()), Utility.KillSqlInjection(pass), passoldenc);
            pnFocus.Enabled = false;
            lblError.Text = Resources.labels.laylaimatkhauthanhcongmatkhaumoiduocguiquaemailcuaban;
            btnChange.Enabled = false;
            SendInfoLogin(Encryption.Decrypt(passoldenc));
            SmartPortal.Common.Log.WriteLogFile("", "", "", "RESET PASS TELLER  ---" + passoldenc);
            SmartPortal.Common.Log.WriteLogFile("", "", "", "RESET PASS TELLER DECODE ---" + Encryption.Decrypt(passoldenc));
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
