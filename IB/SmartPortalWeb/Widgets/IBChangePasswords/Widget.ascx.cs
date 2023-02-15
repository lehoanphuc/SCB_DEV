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

public partial class Widgets_ChangePass_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;
    protected void Page_Load(object sender, EventArgs e)
    {
        pnFocus.Visible = true;
        pnResult.Visible = false;
        lblError.Text = "";
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable iReadLogin;
        try
        {
            string oldpass = SmartPortal.SEMS.O9Encryptpass.sha_sha256((txtOldPassword.Text.Trim()), Session["userID"].ToString().Trim());
            ds = new SmartPortal.SEMS.BankUser().Login(Session["userName"].ToString(), oldpass, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }
            iReadLogin = ds.Tables[0];
            if (iReadLogin.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.matkhaucukhongdung;
                return;
            }

            if (!checkpassword())
            {
                return;
            }
            string newpass = SmartPortal.SEMS.O9Encryptpass.sha_sha256((txtNewPassword.Text.Trim()), Session["userID"].ToString().Trim());
            new SmartPortal.IB.User().FirstChangePass(Session["userID"].ToString().Trim(), newpass, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.CHANGEPASSFAILED);
            }

            // goto EXIT;
            pnFocus.Visible = false;
            pnResult.Visible = true;

            gotologin(Resources.labels.thaydoimatkhauthanhcong);
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
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtNewPassword.Text = "";
        txtOldPassword.Text = "";
        txtRePassword.Text = "";
        lblError.Text = "";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx?p=220");
    }
    private bool checkpassword()
    {
        try
        {
            DataTable dtpolicy = new UsersBLL().GetPolicybyUserID(SmartPortal.Constant.IPC.IB, Session["userID"].ToString().Trim());
            DataTable dtpasshis = new UsersBLL().GetPasswordhisbyUserID(SmartPortal.Constant.IPC.IB, Session["userID"].ToString().Trim());
            //check duplicate old pass and new pass
            if (txtOldPassword.Text.Trim() == txtNewPassword.Text.Trim())
            {
                lblError.Text = Resources.labels.matkhaucuvamoikhongduocgiongnhau;
                return false;
            }

            // check effective date of policy
            //if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare((DateTime)dtpolicy.Rows[0]["efto"], (DateTime)dtpolicy.Rows[0]["systemtime"]) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
            if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare(((DateTime)dtpolicy.Rows[0]["efto"]).Date, ((DateTime)dtpolicy.Rows[0]["systemtime"]).Date) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
            //if (DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare((DateTime)dtpolicy.Rows[0]["efto"], (DateTime)dtpolicy.Rows[0]["systemtime"]) >= 0)
            {

                //check password duplicate with password his:
                if (dtpasshis.Rows.Count > 0)
                {
                    int pwdhis = Convert.ToInt32(dtpolicy.Rows[0]["pwdhis"]);
                    string Passnew = SmartPortal.SEMS.O9Encryptpass.sha_sha256((txtNewPassword.Text.Trim()), Session["userID"].ToString());
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
                if (txtNewPassword.Text.Trim().Length < Convert.ToInt32(dtpolicy.Rows[0]["minpwdlen"]))
                {
                    lblError.Text = string.Format(Resources.labels.matkhauphaicododaiitnhat, dtpolicy.Rows[0]["minpwdlen"].ToString());

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
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            return false;
        }
        return true;
    }
    private void gotologin(string a)
    {
        try
        {
            Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;
            string url = "iblogin.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + a + ". You will be redirected to Login page.'); window.location='" + Request.ApplicationPath + url + "';", true);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}
