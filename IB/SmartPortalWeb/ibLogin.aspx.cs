﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.Security;
using SmartPortal.Common.Utilities;
using System.IO;
using SmartPortal.Common;
using System.Configuration;
using SmartPortal.ExceptionCollection;
using System.Threading;

using SmartPortal.BLL;
using SmartPortal.Model;
using System.Text;
using System.Web.SessionState;
using System.Reflection;


public partial class ibLogin : System.Web.UI.Page
{
    string IPCERRORCODE;
    string IPCERRORDESC;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;
        Session["userID"] = null;
        txtPass.AutoCompleteType = AutoCompleteType.Disabled;
        if (Session["userName"] != null)
        {

            SmartPortal.Common.Log.WriteLogFile(DateTime.Now.ToString() + " IB_login_info ", Session["userName"].ToString(), "", "");
        }
        else
        {
            SmartPortal.Common.Log.WriteLogFile(DateTime.Now.ToString() + "IB_login_info ", "no Userid", "", "");
        }
    }

    protected override void InitializeCulture()
    {
        try
        {
            string culture;

            if (string.IsNullOrEmpty(Request.QueryString["l"]))
            {
                if (Session["langID"] == null)
                {
                    culture = new PortalSettings().portalSetting.DefaultLang;
                }
                else
                {
                    culture = Session["langID"].ToString();
                }
            }
            else
            {
                culture = Request.QueryString["l"];
                Session["langID"] = culture;
            }

            //OR This

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

            base.InitializeCulture();
        }

        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Template_Face1_Default", "InitializeCulture", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        regenerateId();
        //21.9.2017 minh add client info:
        string CLIENTINFO = SmartPortal.Common.Utilities.Utility.GetVisitorIPAddress();
        IPCERRORDESC = CLIENTINFO;
        //ShowPopUpMsg(txtPass.Text);
        //return;
        DataTable tblIsLogin = new DataTable();
        DataSet ds = new DataSet();
        DataTable iReadLogin = new DataTable();
        List<string> lstRoleName = new List<string>();
        List<string> lstRoleID = new List<string>();
        //clear session to assign to session ussername
        //Session.Clear();
        try
        {
            //String rd = Session["randomIBLogin"].ToString().Trim();
            //check input username and password is empty or not
            if (string.IsNullOrEmpty(Utility.KillSqlInjection(txtUser.Text.Trim())))
            {
                lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                return;
            }
            if (string.IsNullOrEmpty(Utility.KillSqlInjection(txtPass.Text.Trim())))
            {
                lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                return;
            }
            //check capchar
            if (txtValidateCode.Text.Trim() != Session["randomIBLogin"].ToString().Trim())
            {
                lblInfo.Text = Resources.labels.maxacnhankhongdung;
                return;
            }


        }
        catch (Exception)
        {
            lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
            return;
        }

        try
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["debug_passnew"].ToString()))
            {
                string userid = String.Empty;
                userid = new SmartPortal.SEMS.BankUser().GetUserIDFromUserName(Utility.KillSqlInjection(txtUser.Text.Trim()));
                if (string.IsNullOrEmpty(userid))
                {
                    lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                    return;
                }
                string password = SmartPortal.SEMS.O9Encryptpass.sha_sha256(txtPass.Text.Trim(), userid);
                //string PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_shapass(Utility.KillSqlInjection(txtPass.Text.Trim()), txtUser.Text.Trim());
                ds = new SmartPortal.SEMS.BankUser().Login(txtUser.Text.Trim(), password, ref IPCERRORCODE, ref IPCERRORDESC);
                //if (!checklogin(userid))
                //{
                //    return;
                //}

                //11.7.2017 minh bypass this because pass was encrypted by sha256 -> can not compare
                //if (!checkpasswordlogin())
                //{

                //    lblInfo.Text = Resources.labels.matkhaucuabankhongtuanthupolicy/*;*/
                //   // Session["userNameTemp"] = txtUser.Text.Trim();

                //    gotochangepass();
                //    return;
                //}

            }
            else
            {
                ds = new SmartPortal.SEMS.BankUser().Login(Utility.KillSqlInjection(txtUser.Text.Trim()), Utility.KillSqlInjection(Encryption.Encrypt(txtPass.Text.Trim())), ref IPCERRORCODE, ref IPCERRORDESC);
            }
            if (IPCERRORCODE != "0")
            {
                if (IPCERRORDESC == "4026")
                {
                    throw new IPCException("4026");
                }
                goto ERROR;
            }

            iReadLogin = ds.Tables[0];
            //11.3.2016 minh add check error:
            if (iReadLogin.Rows.Count == 0)
            {
                SmartPortal.Common.Log.WriteLogFile("", "", "", "ERROR LOGIN: iReadLogin.Rows.Count==0 uid+pwd ok ST(SEMS_IBS_USERS_LOGIN)" + txtUser.Text);
                lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                return;
            }

            //update last login time
            //new SmartPortal.IB.User().UpdateLLT(Utility.KillSqlInjection(txtUser.Text.Trim()), UserIB.SystemTime.ToString("dd/MM/yyyy HH:mm:ss"));
            // check dang nhap lan dau
            if (iReadLogin.Rows[0]["IsLogin"].ToString().Trim() == "0")
            {
                //edit by VuTran 07082014: xoa yeu cau OTP lan dau dang nhap IB
                if (int.Parse(ConfigurationManager.AppSettings["IBRequestOTPINFistLogin"].ToString()) == 1)
                {
                    //if (!(int.Parse(iReadLogin.Rows[0]["USERLEVEL"].ToString()) > 3 && string.IsNullOrEmpty(iReadLogin.Rows[0]["TypeID"].ToString())))
                    if (string.IsNullOrEmpty(iReadLogin.Rows[0]["USERLEVEL"].ToString()) || int.Parse(iReadLogin.Rows[0]["USERLEVEL"].ToString()) == 0)
                    {
                        //Session["userNameTemp"] = iReadLogin.Rows[0]["userName"].ToString();
                        //Session["userName"] = iReadLogin.Rows[0]["userName"].ToString();
                        //Session["userNameTemp"] = txtUser.Text.Trim();
						Session["accType"] = "CCO";
                        Session["userID"] = iReadLogin.Rows[0]["userid"].ToString();
                        Session["serviceID"] = "IB";
                        lblInfo.Text = Resources.labels.becauseoffirstlogin;
                        gotochangepass();
                        return;

                    }
                }
            }
            if (iReadLogin.Rows.Count == 0)  // khong co account
            {
                lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                return;
            }
            else // ton tai account
            {

                lstRoleName = new List<string>();
                lstRoleID = new List<string>();
                foreach (DataRow row in iReadLogin.Rows)
                {
                    //write info in session   
                    Session["userName"] = row["UserName"].ToString();
                    Session["userType"] = row["CFTYPE"].ToString();
                    Session["userID"] = row["UserID"].ToString();
                    Session["BranchID"] = row["BranchID"].ToString();
                    Session["userLevel"] = row["UserLevel"].ToString();
                    Session["fullName"] = row["FullName"].ToString();
                    lstRoleName.Add(row["RoleName"].ToString());
                    lstRoleID.Add(row["RoleID"].ToString());
                    Session["lastLoginTime"] = SmartPortal.Common.Utilities.Utility.IsDateTime2(row["LastLoginTime"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    Session["roleName"] = lstRoleName;
                    Session["roleID"] = lstRoleID;
                    Session["serviceID"] = "IB";
                    Session["TypeID"] = row["TypeID"].ToString();
                    Session["checkActionTimeout"] = DateTime.Now;
                    //vutt 10052016 show notify
                    Session["NotifyStatus"] = null;
                    Session["strNotification"] = null;
                    Session["PopupNotifyStatus"] = null;
					
					Session["accType"] = row["ContractType"].ToString();
                }
                //update time login

                //new SmartPortal.IB.User().UpdateIsLogin(Session["userName"].ToString(), DateTime.Now.AddMinutes(0).ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());
                try
                {
                    string langID = Session["langID"] == null ? new PortalSettings().portalSetting.DefaultLang : Session["langID"].ToString();
                    new SmartPortal.IB.User().UpdateChangeLanguage(Session["UserID"].ToString(), langID, Session["UUID"].ToString());
                }
                catch
                {

                }
                goto EXIT;
            }

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
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBLogin_Widget", "btnContractNext_Click", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:

        if (Session["userLevel"].ToString() == "3" || (string.IsNullOrEmpty(Session["TypeID"].ToString()) && Session["userType"].ToString().Equals("CCO")))
        {
            //thaity modify at 24/6/2014
            try
            {
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=336&f=huongdansudungqtht"), true);
            }
            catch (ThreadAbortException)
            {
                // Do nothing.  
                // No need to log exception when we expect ThreadAbortException
            }
        }
        //vutran 01/11 fastbanking
        else if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["fid"] != null)
        {
            try
            {
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1000&fid=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["fid"]));
            }
            catch (ThreadAbortException)
            {
            }
        }
        else
        {
            //thaity modify at 24/6/2014
            try
            {
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=86"), true);
            }
            catch (ThreadAbortException)
            {
                // Do nothing.  
                // No need to log exception when we expect ThreadAbortException
            }

        }
    CHANGEPASS:
        //thaity modify at 24/6/2014
        try
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=206"));
        }
        catch (ThreadAbortException)
        {
            // Do nothing.  
            // No need to log exception when we expect ThreadAbortException
        }

    }
    //private bool checkUUID_multilogin(string ServiceLogin, bool multiLogin, string UUID)
    //{
    //    //bool retval = false;
    //    // check uuid
    //    if (Session[SmartPortal.Constant.IPC.UUID] == null) // neu session uuid la null
    //    {
    //        if (string.IsNullOrEmpty(UUID))   // khong co nguoi dang truy cap
    //        {
    //            //create new uuid + update uuid to db
    //            Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
    //            DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUser.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());
    //            return true;

    //        }
    //        else  // co nguoi dang truy cap
    //        {
    //            //check multilogin
    //            if ((bool)(multiLogin))
    //            {
    //                Session[SmartPortal.Constant.IPC.UUID] = UUID; // lay luon session dang truy cap
    //                return true;
    //            }
    //            else
    //            {
    //                lblInfo.Text = Resources.labels.thisusernameloginbyanotheruser;
    //                return false;
    //            }


    //        }

    //    }
    //    else  // neu session uuid khac null
    //    {
    //        if (Session[SmartPortal.Constant.IPC.UUID].ToString() != UUID)
    //        {
    //            // clear uuid ca hai phia db vs client
    //            DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUser.Text.Trim()), string.Empty);
    //            Session[SmartPortal.Constant.IPC.UUID] = null;
    //            lblInfo.Text = Resources.labels.accountsessionhasbeenexpired;
    //            return false;
    //        }
    //        else
    //        {
    //            if ((bool)(multiLogin))
    //            {
    //                return true;
    //            }
    //            else
    //            {
    //                lblInfo.Text = Resources.labels.thisusernameloginbyanotheruser;
    //                return false;
    //            }

    //        }

    //    }  //ket thuc check session uuid

    //}
    //private bool checkUUID_singlelogin(string ServiceLogin, bool multiLogin, string UUID)
    //{
    //    // check uuid
    //    if (Session[SmartPortal.Constant.IPC.UUID] == null) // neu session uuid la null
    //    {
    //        if (string.IsNullOrEmpty(UUID))   // khong co nguoi dang truy cap
    //        {
    //            //create new uuid + update uuid to db
    //            Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
    //            DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUser.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());
    //            return true;
    //        }
    //        else  // co session tren db
    //        {
    //            if ((bool)(multiLogin))
    //            {
    //                Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
    //                DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUser.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());
    //                return true;

    //            }
    //            else
    //            {
    //                // lblInfo.Text = Resources.labels.thisusernameloginbyanotheruser;
    //                Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
    //                DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUser.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());

    //                return true;
    //            }

    //        }

    //    }
    //    else  // neu session uuid khac null
    //    {
    //        if (Session[SmartPortal.Constant.IPC.UUID].ToString() != UUID)
    //        {
    //            Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
    //            DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUser.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());

    //            return true;
    //            //// clear uuid ca hai phia db vs client
    //            //DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUser.Text.Trim()), string.Empty);
    //            //Session[SmartPortal.Constant.IPC.UUID] = null;
    //            //lblInfo.Text = Resources.labels.accountsessionhasbeenexpired;
    //            //return false;
    //        }
    //        else
    //        {
    //            if ((bool)(multiLogin))
    //            {
    //                Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
    //                DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUser.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());

    //                return true;
    //            }
    //            else
    //            {
    //                Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
    //                DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUser.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());

    //                return true;
    //            }
    //        }

    //    }  //ket thuc check session uuid

    //}

    private bool checklogin(string userid)
    {

        string ServiceLogin = SmartPortal.Constant.IPC.IB;
        bool IBMultiLogin = Convert.ToBoolean(ConfigurationManager.AppSettings["IBIsMultilogin"].ToString());
        bool checkexpirepolicy = false;
        try
        {
            UsersIBModel UserIB = new UsersIBModel();
            UserIB = new UsersBLL().GetUserIBLogin(ServiceLogin, userid);
            DataTable dtpolicy = new UsersBLL().GetPolicybyUserID(ServiceLogin, userid);
            if (dtpolicy.Rows.Count == 0)
            {
                SmartPortal.Common.Log.WriteLogFile("IB Login error", "", "", "No exists Policy for this user" + UserIB.UserName);

            }
            if (UserIB == null || UserIB.UserName == null)
            {
                lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                return false;
            }
            else  //co tai khoan dang ky
            {
                // check policy
                //check effect date of policy
                //if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare((DateTime)dtpolicy.Rows[0]["efto"], (DateTime)dtpolicy.Rows[0]["systemtime"]) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
                if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare(((DateTime)dtpolicy.Rows[0]["efto"]).Date, ((DateTime)dtpolicy.Rows[0]["systemtime"]).Date) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
                {
                    checkexpirepolicy = true;

                    //check time login
                    if (bool.Parse(dtpolicy.Rows[0]["timelginrequire"].ToString()))
                    {
                        if (dtpolicy.Rows[0]["timelginOK"].ToString() == "0")
                        {
                            lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                            return false;
                        }
                    }
                    //check failed login
                    if (Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]) > 0)
                    {
                        if (UserIB.Failnumber >= Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]))
                        {
                            if (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) == 0) // not auto reset failed number
                            {
                                lblInfo.Text = Resources.labels.saiquasolandangnhap;
                                return false;
                            }
                            else // auto reset failed number
                            {
                                if (UserIB.lastlogin < Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]))
                                {
                                    lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                                    return false;
                                }
                                else
                                {
                                    DataTable tblFailNumber = new SmartPortal.IB.Account().UpdateFailLogin(Utility.KillSqlInjection(txtUser.Text.Trim()), "0");
                                    UserIB.Failnumber = 0;
                                    //lblInfo.Text = Resources.labels.accounthasbeenblockbecauseoffailnumber;
                                    //return false;
                                }
                            }

                        }


                    }



                } // end check policy
                //update last login time
                if (UserIB.Failnumber < Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]))
                {

                    new SmartPortal.IB.User().UpdateLLTwithServiceid(userid, UserIB.SystemTime.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.IB);
                }

                //check expire_date:
                if (DateTime.Compare(UserIB.Dateexpire.Date, UserIB.SystemTime.Date) < 0)
                {
                    lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                    return false;
                }
                // check password:
                //if (Utility.KillSqlInjection(Encryption.Encrypt(txtPass.Text.Trim())) != UserIB.Password)
                //SmartPortal.SEMS.O9Encryptpass.sha_sha256((txtOldPassword.Text.Trim()), Session["userNameTemp"].ToString());
                string password = SmartPortal.SEMS.O9Encryptpass.sha_sha256(txtPass.Text.Trim(), userid);
                if (password != UserIB.Password)
                {
                    //check expire policy
                    if (!checkexpirepolicy)
                    {
                        lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                        return false;
                    }

                    //check failed login
                    if (Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]) > 0)
                    {
                        if (UserIB.Failnumber >= Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]))
                        {
                            //if (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) == 0) // not auto reset failed number
                            //{
                            lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                            return false;
                            //}
                            //else // auto reset failed number
                            //{
                            //    if (UserIB.lastlogin <= Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]))
                            //    {
                            //        lblInfo.Text = string.Format(Resources.labels.accounthasbeenblockhaydangnhaplaisau, (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) - UserIB.lastlogin).ToString());
                            //        return false;
                            //    }
                            //    else
                            //    {
                            //        lblInfo.Text = Resources.labels.accounthasbeenblockbecauseoffailnumber;
                            //        return false;
                            //    }
                            //}

                        }
                        else
                        {
                            //add failnumber to db
                            DataTable tblFailNumber = new SmartPortal.IB.Account().UpdateFailLogin(Utility.KillSqlInjection(txtUser.Text.Trim()), string.Empty);
                            lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                            return false;
                        }

                    }

                    else
                    {
                        lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                    }

                    return false;
                }
                else //neu nhap dung mat khau
                {
                    if (checkexpirepolicy)
                    {
                        //check password age
                        if ((int)dtpolicy.Rows[0]["pwdagemax"] > 0)
                        {
                            if ((int)dtpolicy.Rows[0]["pwdagemax"] - UserIB.pwdage < 0)
                            {
                                lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                                gotochangepass();
                                return false;
                            }
                        }
                    }
                    // check session
                    if (DateTime.Compare(UserIB.SystemTime, UserIB.ExpireTime) > 0)
                    {
                        if (!checkUUID_multilogin(ServiceLogin, IBMultiLogin, UserIB.UUID)) return false;
                    }
                    else
                    {
                        if (!checkUUID_singlelogin(ServiceLogin, IBMultiLogin, UserIB.UUID)) return false;
                    }
                    //reset failnumber
                    if (UserIB.Failnumber > 0)
                    {
                        DataTable tblFailNumber = new SmartPortal.IB.Account().UpdateFailLogin(Utility.KillSqlInjection(txtUser.Text.Trim()), "0");
                    }
                    // check status:
                    switch (UserIB.Status)
                    {
                        case SmartPortal.Constant.IPC.BLOCK:
                            lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                            return false;

                        case SmartPortal.Constant.IPC.DELETE:
                            lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                            return false;

                        case SmartPortal.Constant.IPC.PENDING:
                            lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                            return false;
                        case SmartPortal.Constant.IPC.REJECT:
                            lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                            return false;
                        default:
                            break;

                    }
                }





            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }


        return true;
    }
    private bool checkpasswordlogin()
    {
        try
        {

            DataTable dtpolicy = new UsersBLL().GetPolicybyUserID(SmartPortal.Constant.IPC.IB, txtUser.Text.Trim());
            DataTable dtpasshis = new UsersBLL().GetPasswordhisbyUserID(SmartPortal.Constant.IPC.IB, txtUser.Text.Trim());

            // check effective date of policy
            //if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare((DateTime)dtpolicy.Rows[0]["efto"], (DateTime)dtpolicy.Rows[0]["systemtime"]) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
            if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare(((DateTime)dtpolicy.Rows[0]["efto"]).Date, ((DateTime)dtpolicy.Rows[0]["systemtime"]).Date) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
            //if (DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare((DateTime)dtpolicy.Rows[0]["efto"], (DateTime)dtpolicy.Rows[0]["systemtime"]) >= 0)
            {

                // check length of pass
                if (txtPass.Text.Trim().Length < Convert.ToInt32(dtpolicy.Rows[0]["minpwdlen"]))
                {
                    //lblAlert.Text = string.Format(Resources.labels.matkhauphaicododaiitnhat, dtpolicy.Rows[0]["minpwdlen"].ToString());

                    return false;
                }

                if (bool.Parse(dtpolicy.Rows[0]["pwdcplx"].ToString()))
                {
                    // check lowercase
                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplxlc"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasLowerCharacter(txtPass.Text.Trim()))
                        {
                            //lblAlert.Text = Resources.labels.matkhauphaicoitnhatmotkytuthuong;
                            return false;
                        }

                    }
                    //check upper case
                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplxuc"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasUpperCharacter(txtPass.Text.Trim()))
                        {
                            //lblAlert.Text = Resources.labels.matkhauphaicoitnhatmotkytuchuhoa;
                            return false;
                        }
                    }
                    //check symbol
                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplxsc"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasSymbolCharacter(txtPass.Text.Trim()))
                        {
                            //lblAlert.Text = Resources.labels.matkhauphaicoitnhatmokytubieutuong;
                            return false;
                        }
                    }
                    // check number
                    if (bool.Parse(dtpolicy.Rows[0]["pwccplxsn"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasNumberCharacter(txtPass.Text.Trim()))
                        {
                            //lblAlert.Text = Resources.labels.matkhauphaicoitnhatmokytuso;
                            return false;
                        }
                    }


                }
            }
        }
        catch (Exception ex)
        {

            lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
            return false;
        }


        return true;
    }
    private void gotochangepass()
    {
        try
        {
            try
            {
                string isLogin = String.Empty;
                isLogin = new SmartPortal.SEMS.BankUser().GetIsLoginDFromUserName(Utility.KillSqlInjection(txtUser.Text.Trim()));
                if (!isLogin.Equals("1"))
                {
                    if (Session["ShowTerm"] == null || Session["ShowTerm"].ToString() != "0")
                    {
                        Session["userNameTemp"] = txtUser.Text.Trim();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                        "window.location='" +
                        Request.ApplicationPath + "TermLogin.aspx" + "';", true);
                        return;
                    }
                }

            }
            catch
            {

            }
            //Session["userName"] = txtUser.Text.Trim();
            Session["userNameTemp"] = txtUser.Text.Trim();
            //Response.Write("<script>alert('Hello');</script>");
            string a = lblInfo.Text;
            lblInfo.Text = string.Empty;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
"alert('" + a + ". You will be redirected to Change Password.'); window.location='" +
Request.ApplicationPath + SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=206") + "';", true);
            //Response.Redirect("~/Default.aspx?po=4&p=206");
        }
        catch (Exception ex)
        {
            lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
            // Do nothing.  
            // No need to log exception when we expect ThreadAbortException
        }
    }
    private void ShowPopUpMsg(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
    }
    private bool checkUUID_multilogin(string ServiceLogin, bool multiLogin, string UUID)
    {
        //bool retval = false;
        // check uuid
        if (Session[SmartPortal.Constant.IPC.UUID] == null) // neu session uuid la null
        {
            //if (string.IsNullOrEmpty(UUID))   // khong co nguoi dang truy cap
            //{
            //create new uuid + update uuid to db
            Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
            DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUser.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());
            return true;

            //}
            //else  // co nguoi dang truy cap
            //{
            //    //check multilogin
            //    if ((bool)(multiLogin))
            //    {
            //        Session[SmartPortal.Constant.IPC.UUID] = UUID; // lay luon session dang truy cap
            //        return true;
            //    }
            //    else
            //    {
            //        lblAlert.Text = Resources.labels.thisusernameloginbyanotheruser;
            //        return false;
            //    }


            //}

        }
        else  // neu session uuid khac null
        {
            if (Session["userName"].ToString().Equals(new PortalSettings().portalSetting.UserNameDefault))
            {
                Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
                DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUser.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());
                return true;
            }
            //if (Session[SmartPortal.Constant.IPC.UUID].ToString() != UUID)
            //{
            // clear uuid ca hai phia db vs client
            // DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUserName.Text.Trim()), string.Empty);
            //   Session[SmartPortal.Constant.IPC.UUID] = null;
            else
            {
                lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                return false;
            }
            //}
            //else
            //{
            //    if ((bool)(multiLogin))
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        lblAlert.Text = Resources.labels.thisusernameloginbyanotheruser;
            //        return false;
            //    }

            //}

        }  //ket thuc check session uuid

    }
    private bool checkUUID_singlelogin(string ServiceLogin, bool multiLogin, string UUID)
    {
        // check uuid
        if (Session[SmartPortal.Constant.IPC.UUID] == null) // neu session uuid la null
        {
            if (string.IsNullOrEmpty(UUID))   // khong co nguoi dang truy cap
            {
                //create new uuid + update uuid to db
                Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
                DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUser.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());
                return true;
            }
            else  // co session tren db
            {
                if ((bool)(multiLogin))
                {
                    Session[SmartPortal.Constant.IPC.UUID] = UUID;
                    //DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUserName.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());
                    return true;

                }
                else
                {
                    //// lblAlert.Text = Resources.labels.thisusernameloginbyanotheruser;
                    //Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
                    //DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUserName.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());
                    lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                    return false;
                    //return true;
                }

            }

        }
        else  // neu session uuid khac null
        {

            if (Session["userName"].ToString().Equals(new PortalSettings().portalSetting.UserNameDefault))
            {
                //check multilogin
                if ((bool)(multiLogin))
                {
                    Session[SmartPortal.Constant.IPC.UUID] = UUID; // lay luon session dang truy cap
                    return true;
                }
                else
                {
                    lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                    return false;
                }
            }
            else
            {

                lblInfo.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                return false;
            }

            //  if(Session["userName"].ToString().Equals(new PortalSettings().portalSetting.UserNameDefault))

            //    if (Session[SmartPortal.Constant.IPC.UUID].ToString() != UUID)
            //    {
            //        Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
            //        DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUserName.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());

            //        return true;
            //        //// clear uuid ca hai phia db vs client
            //        //DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUserName.Text.Trim()), string.Empty);
            //        //Session[SmartPortal.Constant.IPC.UUID] = null;
            //        //lblAlert.Text = Resources.labels.accountsessionhasbeenexpired;
            //        //return false;
            //    }
            //    else
            //    {
            //if ((bool)(multiLogin))
            //{
            //    Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
            //    DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUserName.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());

            //    return true;
            //}
            //else
            //{
            //    Session[SmartPortal.Constant.IPC.UUID] = Guid.NewGuid().ToString();
            //    DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUserName.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());

            //    return true;
            //}
            //}

        }  //ket thuc check session uuid

    }
    void regenerateId()
    {
        System.Web.SessionState.SessionIDManager manager = new System.Web.SessionState.SessionIDManager();
        string oldId = manager.GetSessionID(Context);
        string newId = manager.CreateSessionID(Context);
        Session["ASP.NET_SessionId"] = newId;
        bool isAdd = false, isRedir = false;
        manager.SaveSessionID(Context, newId, out isRedir, out isAdd);
        HttpApplication ctx = (HttpApplication)HttpContext.Current.ApplicationInstance;
        HttpModuleCollection mods = ctx.Modules;
        System.Web.SessionState.SessionStateModule ssm = (SessionStateModule)mods.Get("Session");
        System.Reflection.FieldInfo[] fields = ssm.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        SessionStateStoreProviderBase store = null;
        System.Reflection.FieldInfo rqIdField = null, rqLockIdField = null, rqStateNotFoundField = null;
        foreach (System.Reflection.FieldInfo field in fields)
        {
            if (field.Name.Equals("_store")) store = (SessionStateStoreProviderBase)field.GetValue(ssm);
            if (field.Name.Equals("_rqId")) rqIdField = field;
            if (field.Name.Equals("_rqLockId")) rqLockIdField = field;
            if (field.Name.Equals("_rqSessionStateNotFound")) rqStateNotFoundField = field;
        }
        object lockId = rqLockIdField.GetValue(ssm);
        if ((lockId != null) && (oldId != null)) store.ReleaseItemExclusive(Context, oldId, lockId);
        rqStateNotFoundField.SetValue(ssm, true);
        rqIdField.SetValue(ssm, newId);
    }

}
