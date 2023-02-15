using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using SmartPortal.Security;
using System.Data;
using SmartPortal.Common;
using SmartPortal.Model;
using SmartPortal.SEMS;

public partial class Widgets_SEMSChangePasswordsTeller_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblError.Text = "";
            txtUserName.Text = Session["userName"].ToString();
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
            lblError.Text = "";
            iReadLogin = UB.Login(Session["userName"].ToString(), O9Encryptpass.sha_sha256(Utility.KillSqlInjection(txtOldPassword.Text.Trim()), Utility.KillSqlInjection(txtUserName.Text.Trim())));
            if (iReadLogin.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.oldpasswordincorrect;
                return;
            }

            //cap nhat mat khau moi
            if (!checkpassword())
            {
                return;
            }
            new SmartPortal.SEMS.BankUser().ChangePasswordsTeller(Session["userName"].ToString().Trim(), O9Encryptpass.sha_sha256(Utility.KillSqlInjection(txtNewPassword.Text.Trim()), Utility.KillSqlInjection(txtUserName.Text.Trim())));
            btnChange.Visible = false;
            btnReset.Visible = false;
            lblError.Text = Resources.labels.changepasswordsuccessful;
            gotologin();

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
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["SEMSADMIN"]));
    }
    private bool checkpassword()
    {
        try
        {
            DataTable dtpolicy = new UsersBLL().GetPolicybyUserID(SmartPortal.Constant.IPC.SEMS, Session["userName"].ToString().Trim());
            DataTable dtpasshis = new UsersBLL().GetPasswordhisbyUserID(SmartPortal.Constant.IPC.SEMS, Session["userName"].ToString().Trim());
            //check duplicate old pass and new pass
            if (txtOldPassword.Text.Trim() == txtNewPassword.Text.Trim())
            {
                lblError.Text = Resources.labels.matkhaucuvamoikhongduocgiongnhau;
                return false;
            }
            // check effective date of policy
            if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare((DateTime)dtpolicy.Rows[0]["efto"], (DateTime)dtpolicy.Rows[0]["systemtime"]) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
            //if (DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare((DateTime)dtpolicy.Rows[0]["efto"], (DateTime)dtpolicy.Rows[0]["systemtime"]) >= 0)
            {
                //check password duplicate with password his:
                if (dtpasshis.Rows.Count != 0)
                {
                    int pwdhis = Convert.ToInt32(dtpolicy.Rows[0]["pwdhis"]);
                    string Passnew = SmartPortal.SEMS.O9Encryptpass.sha_sha256(Utility.KillSqlInjection(Encryption.Encrypt(txtNewPassword.Text.Trim())), Session["userName"].ToString());
                    for (int i = 0; i < pwdhis; i++)
                    {
                        string passhis = DBNull.Value.Equals(dtpolicy.Rows[0][i]) ? string.Empty : dtpasshis.Rows[0][i].ToString().Trim();
                        if (Passnew == passhis)
                        {
                            lblError.Text = string.Format(Resources.labels.matkhaukhongduocgiongmatkhaudadattronglichsu, pwdhis.ToString());
                            return false;
                        }
                    }
                }
                // check length of pass
                if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
                {
                    lblError.Text = Resources.labels.bancannhapmatkhaumoi;
                    return false;
                }
                if (txtNewPassword.Text.Trim().Length < Convert.ToInt32(dtpolicy.Rows[0]["minpwdlen"]))
                {
                    lblError.Text = string.Format(Resources.labels.matkhauphaicododaiitnhat, dtpolicy.Rows[0]["minpwdlen"].ToString());
                    return false;
                }
                if (string.IsNullOrEmpty(txtRePassword.Text))
                {
                    lblError.Text = Resources.labels.bancannhapmatkhauxacnhan;
                    return false;
                }
                if (txtNewPassword.Text.Trim() != txtRePassword.Text.Trim())
                {
                    lblError.Text = Resources.labels.passwordcompare;
                    return false;
                }
                if (bool.Parse(dtpolicy.Rows[0]["pwdcplx"].ToString()))
                {
                    // check lowercase
                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplxlc"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasLowerCharacter(txtNewPassword.Text.Trim()))
                        {
                            lblError.Text = Resources.labels.matkhauphaicoitnhatmotkytuthuong;
                            return false;
                        }
                    }
                    //check upper case
                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplxuc"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasUpperCharacter(txtNewPassword.Text.Trim()))
                        {
                            lblError.Text = Resources.labels.matkhauphaicoitnhatmotkytuchuhoa;
                            return false;
                        }
                    }
                    //check symbol
                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplxsc"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasSymbolCharacter(txtNewPassword.Text.Trim()))
                        {
                            lblError.Text = Resources.labels.matkhauphaicoitnhatmokytubieutuong;
                            return false;
                        }
                    }
                    // check number
                    if (bool.Parse(dtpolicy.Rows[0]["pwccplxsn"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasNumberCharacter(txtNewPassword.Text.Trim()))
                        {
                            lblError.Text = Resources.labels.matkhauphaicoitnhatmokytuso;
                            return false;
                        }
                    }
                }
            }
            else
            {
                lblError.Text = Resources.labels.effectivedateexpired;
                return false;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            return false;
        }
        return true;
    }
    private void gotologin()
    {
        try
        {
            string a = lblError.Text;
            lblError.Text = string.Empty;
            if (Session["userName"] != null && Session["userName"].ToString() != "guest")
            {
                if (!Convert.ToBoolean(ConfigurationManager.AppSettings["IBIsMultilogin"].ToString()))
                {
                    UsersBLL UB = new UsersBLL();
                    UB.UpdateLLT(Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + a + ". You will be redirected to Login page.'); window.location='" + Request.ApplicationPath + "default.aspx?p=125';", true);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}
