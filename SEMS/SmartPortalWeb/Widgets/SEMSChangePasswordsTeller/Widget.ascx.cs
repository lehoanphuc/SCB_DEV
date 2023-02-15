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
using SmartPortal.Common;

public partial class Widgets_SEMSChangePasswordsTeller_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;

    protected void Page_Load(object sender, EventArgs e)
    {
        pnFocus.Visible = true;
        lblError.Text = "";
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
            iReadLogin = UB.Login(Session["userName"].ToString(), SmartPortal.SEMS.O9Encryptpass.sha_sha256((txtOldPassword.Text.Trim()), Session["userName"].ToString()));
            if (iReadLogin.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.passwordincorrect;
                return;
            }
            if (!checkpassword())
            {
                return;
            }
            bool result = new SmartPortal.SEMS.BankUser().ChangePasswordsTeller(Session["userName"].ToString().Trim(), SmartPortal.Security.Encryption.Encrypt(txtNewPassword.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (result)
            {
                pnFocus.Enabled = false;
                btnChange.Visible = false;
                btnReset.Visible = false;
                lblError.Text = Resources.labels.thaydoimatkhauthanhcong;
                gotologin();
            }
            else
            {
                if (IPCERRORCODE.Equals("2001") || IPCERRORCODE.Equals("2003") || IPCERRORCODE.Equals("2004") || IPCERRORCODE.Equals("2005"))
                {
                    lblError.Text = IPCERRORDESC;
                }
                else
                {
                    lblError.Text = Resources.labels.thaydoimatkhaukhongthanhcong;
                }
            }
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
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtNewPassword.Text = "";
        txtOldPassword.Text = "";
        txtRePassword.Text = "";
        lblError.Text = "";
    }
    private bool checkpassword()
    {
        try
        {
            DataTable dtpolicy = new UsersBLL().GetPolicybyUserID(SmartPortal.Constant.IPC.SEMS, Session["userName"].ToString().Trim());
            DataTable dtpasshis = new UsersBLL().GetPasswordhisbyUserID(SmartPortal.Constant.IPC.SEMS, Session["userName"].ToString().Trim());
            bool IsSecurity = bool.Parse(new SmartPortal.SEMS.EBASYSVAR().ViewDetail("PASSWORDSECURITY", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0].Rows[0]["VARVALUE"].ToString());
            if (txtOldPassword.Text.Trim() == txtNewPassword.Text.Trim())
            {
                lblError.Text = Resources.labels.matkhaucuvamoikhongduocgiongnhau;
                return false;
            }
            if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare(((DateTime)dtpolicy.Rows[0]["efto"]).Date, ((DateTime)dtpolicy.Rows[0]["systemtime"]).Date) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
            {
                if (dtpasshis.Rows.Count != 0)
                {
                    int pwdhis = Convert.ToInt32(dtpolicy.Rows[0]["pwdhis"]);
                    string Passnew = SmartPortal.SEMS.O9Encryptpass.sha_sha256(txtNewPassword.Text.Trim(), Session["userName"].ToString());
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
                if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
                {
                    lblError.Text = Resources.labels.bancannhapmatkhaumoi;
                    return false;
                }
                if (!IsSecurity)
                {
                    return true;
                }
                if (txtNewPassword.Text.Trim().Length < Convert.ToInt32(dtpolicy.Rows[0]["minpwdlen"]))
                {
                    lblError.Text = string.Format(Resources.labels.matkhauphaicododaiitnhat, dtpolicy.Rows[0]["minpwdlen"].ToString());
                    return false;
                }
                if (bool.Parse(dtpolicy.Rows[0]["pwdcplx"].ToString()))
                {
                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplxlc"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasLowerCharacter(txtNewPassword.Text.Trim()))
                        {
                            lblError.Text = Resources.labels.matkhauphaicoitnhatmotkytuthuong;
                            return false;
                        }

                    }
                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplxuc"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasUpperCharacter(txtNewPassword.Text.Trim()))
                        {
                            lblError.Text = Resources.labels.matkhauphaicoitnhatmotkytuchuhoa;
                            return false;
                        }
                    }
                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplxsc"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasSymbolCharacter(txtNewPassword.Text.Trim()))
                        {
                            lblError.Text = Resources.labels.matkhauphaicoitnhatmokytubieutuong;
                            return false;
                        }
                    }
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
            lblError.Text = ex.ToString();
            return false;
        }
        return true;
    }
    private void gotologin()
    {
        try
        {
            Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;
            string a = lblError.Text;
            lblError.Text = string.Empty;
            string url = "sems.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + a + ". You will be redirected to Login page.'); window.location='" + Request.ApplicationPath + url + "';", true);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.ToString();
        }
    }
}
