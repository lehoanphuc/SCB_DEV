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
using System.Collections;
using System.Configuration;
using SmartPortal.SEMS;
using SmartPortal.Common;


public partial class Widgets_IBChangePassFirst_Widget : WidgetBase
{
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        if (!IsPostBack)
        {
            if (Session["userNameTemp"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            LoadLoaiXacThuc();
            pnOTP.Visible = true;
            pnFocus.Visible = false;
            pnResult.Visible = false;
        }
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable iReadLogin;
        try
        {
            string oldpass = SmartPortal.SEMS.O9Encryptpass.sha_sha256((txtOldPassword.Text.Trim()), Session["userID"].ToString().Trim());
            ds = new SmartPortal.SEMS.BankUser().Login(Session["userNameTemp"].ToString(), oldpass, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
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
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["loginpage"].ToString());
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

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", "alert('" + a + ". You will be redirected to Login page.'); window.location='" + Request.ApplicationPath + url + "';", true);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private void LoadLoaiXacThuc()
    {
        try
        {
            ViewState["isSendFirst"] = 1;
            btnAction.Enabled = false;
            SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
            DataTable dt = objTran.LoadAuthenType(Session["userID"].ToString());
            ddlLoaiXacThuc.DataSource = dt;
            ddlLoaiXacThuc.DataTextField = "TYPENAME";
            ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
            ddlLoaiXacThuc.DataBind();

            btnSendOTP.Text = Resources.labels.send;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlLoaiXacThuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAction.Enabled = false;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
    }

    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            ddlLoaiXacThuc.Enabled = false;
            btnAction.Enabled = true;
            btnSendOTP.Text = Resources.labels.resend;
            if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
            {
                SmartPortal.SEMS.OTP.SendSMSOTP(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            }
            else
            {
                SmartPortal.SEMS.OTP.SendSMSOTPCORP(Session["userID"].ToString(), ddlLoaiXacThuc.SelectedValue.ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                lbnotice.Visible = true;
            }
            if (IPCERRORCODE != "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
            }
            switch (IPCERRORCODE)
            {
                case "0":
                    int time;
                    if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
                    {
                        time = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OTPtimeexpires"]) ? int.Parse(ConfigurationManager.AppSettings["OTPtimeexpires"].ToString()) : 20;
                    }
                    else
                    {
                        time = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OTPCorptimeexpires"]) ? int.Parse(ConfigurationManager.AppSettings["OTPCorptimeexpires"].ToString()) : 20;
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "countdownOTP(" + time + ");", true);
                    ViewState["timeReSendOTP"] = DateTime.Now.AddSeconds(time);
                    break;
                case "7003":
                    lblError.Text = Resources.labels.notregotp;
                    btnAction.Enabled = false;
                    break;
                default:
                    lblError.Text = IPCERRORDESC;
                    btnAction.Enabled = false;
                    pnCountDownOTP.Attributes.Add("class", "hidden");
                    btnSendOTP.CssClass = btnSendOTP.CssClass.Replace("hidden", "").Trim();
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnAction_Click(object sender, EventArgs e)
    {
        try
        {
            Hashtable result = new Hashtable();
            SmartPortal.IB.User objUser = new SmartPortal.IB.User();
            result = objUser.OTPAuthen(Session["userID"].ToString(), ddlLoaiXacThuc.SelectedValue, Utility.KillSqlInjection(txtOTP.Text.Trim()));
            switch (result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
            {
                case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                    lblError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                    if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                    }
                    break;
                case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                    lblError.Text = Resources.labels.authentypeinvalid;
                    break;
                case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                    lblError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                    break;
                case "0":
                    pnOTP.Visible = false;
                    pnFocus.Visible = true;
                    break;
                default:
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
