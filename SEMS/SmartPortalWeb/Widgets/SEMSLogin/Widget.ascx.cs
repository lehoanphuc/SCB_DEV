using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.BLL;
using System.Collections.Generic;
using SmartPortal.Common.Utilities;
using SmartPortal.Security;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common;
using SmartPortal.Model;
using System.Threading;
using System.Text;
using System.Web.SessionState;
using System.Reflection;


public partial class Widgets_SEMSLogin_Widget : WidgetBase
{
    private const string LOGINCOOKIE = "IBLOGIN";
    private string PAGECHANGEPASSFIRST = "1065";
    public string RANDOMIBLOGIN
    {
        get
        {
            return Session["randomIBLogin"] != null ? Session["randomIBLogin"].ToString().Trim() : "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["FLogin"] = "0";
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
                //txtValidateCode.Text = string.Empty;
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Login_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
        }
        if (Session["userName"] != "admin")
        {
            Session["userName"] = new PortalSettings().portalSetting.UserNameDefault;
            Session["branch"] = null;
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(RANDOMIBLOGIN) || txtValidateCode.Text.Trim() != RANDOMIBLOGIN)
        //{
        //    lblAlert.Text = Resources.labels.maxacnhankhongdung;
        //    txtValidateCode.Text = string.Empty;
        //    txtValidateCode.Focus();
        //    return;
        //}
        regenerateId();
        UsersBLL UB = new UsersBLL();
        DataTable tblIsLogin = new DataTable();
        DataTable iReadLogin;
        List<string> lstRoleName = new List<string>();
        lblAlert.Text = string.Empty;
        //Session.Clear();
        ClearCacheItems(); // Clear cache
        try
        {
            //Remember me
            if (cbRememberMe.Checked)
            {
                HttpCookie myCookie = new HttpCookie(LOGINCOOKIE);
                if (myCookie != null)
                {
                    Response.Cookies.Remove(LOGINCOOKIE);
                    Response.Cookies.Add(myCookie);
                    myCookie.Values.Add("username", Utility.KillSqlInjection(txtUserName.Text.Trim()));
                    //myCookie.Values.Add("password", Utility.KillSqlInjection(Encryption.Encrypt(txtPassword.Text.Trim())));
                    myCookie.Expires = DateTime.Now.AddDays(15);
                }
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

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["debug_passnew"].ToString()))
            {

                iReadLogin = UB.Login(Utility.KillSqlInjection(txtUserName.Text.Trim()), SmartPortal.SEMS.O9Encryptpass.sha_shapass(Utility.KillSqlInjection(txtPassword.Text.Trim()), Utility.KillSqlInjection(txtUserName.Text.Trim())));
                SmartPortal.Common.Log.WriteLogFile("", "", "", "SEMS LOGIN PASS ---" + SmartPortal.SEMS.O9Encryptpass.sha_shapass(Utility.KillSqlInjection(txtPassword.Text.Trim()), Utility.KillSqlInjection(txtUserName.Text.Trim())));
                //check login
                if (!checklogin())
                {
                    return;
                }
                //check first login -> resset passs:
                if (iReadLogin.Rows.Count > 0)
                {
                    if (iReadLogin.Rows[0]["IsLogin"] is DBNull || iReadLogin.Rows[0]["IsLogin"].ToString().Trim() == "0")
                    {
                        Session["branchName"] = iReadLogin.Rows[0]["branchName"].ToString();
                        Session["userNameTemp"] = iReadLogin.Rows[0]["userName"].ToString();
                        gotochangepass(Resources.labels.becauseoffirstlogin);
                        return;
                    }
                }
                //11.7.2017 minh bypass this because pass was encrypted by sha256 -> can not compare
                //if (!checkpasswordlogin())
                //{

                //    lblAlert.Text = Resources.labels.matkhaucuabankhongtuanthupolicy;

                //    goto CHANGEPASS;

                //}

            }
            else
            {
                iReadLogin = UB.Login(Utility.KillSqlInjection(txtUserName.Text.Trim()), Utility.KillSqlInjection(Encryption.Encrypt(txtPassword.Text.Trim())));


            }

            //iReadLogin = UB.Login(Utility.KillSqlInjection(txtUserName.Text.Trim()), Utility.KillSqlInjection(Encryption.Encrypt(txtPassword.Text.Trim())));

            if (iReadLogin.Rows.Count == 0)
            {
                lblAlert.Text = Resources.labels.loginfailed;
                return;
            }
            ////get status login         
            //tblIsLogin = UB.GetIsLogin(Utility.KillSqlInjection(txtUserName.Text.Trim()),DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            //if (tblIsLogin.Rows.Count == 0)
            //{
            //    lblAlert.Text = Resources.labels.thisusernameloginbyanotheruser;
            //    return;
            //}
            // update fail login:
            // DataTable tblFailNumber = new SmartPortal.IB.Account().UpdateFailLoginSEMS(Utility.KillSqlInjection(txtUserName.Text.Trim()), "0");
            // UserIB.Failnumber = 0;
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
                Session["branch"] = row["branchID"].ToString();
                Session["serviceID"] = "SEMS";
                Session["userID"] = row["userID"].ToString();
                Session["branchName"] = row["branchName"].ToString();
                //thaity modify at 16/7/2014
                Session["userLevel"] = row["UserLevel"].ToString();
                Session["FLogin"] = "0";
                //Hongnt modify at 01/03/2020
                Session["Workingdate"] = row["Workingdate"].ToString();
            }

            //Write Log
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["LOGIN"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], "", "", "1");


        }

        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Login_Widget", "btnLogin_Click", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Login_Widget", "btnLogin_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

        try
        {
            //update status login
            //UB.UpdateIsLogin(Utility.KillSqlInjection(txtUserName.Text.Trim()), DateTime.Now.AddMinutes(Session.Timeout).ToString("dd/MM/yyyy HH:mm:ss"));
            if (Session["userName"] != null && Session["userName"].ToString() != "guest")
            {

                //SmartPortal.BLL.UsersBLL UB = new SmartPortal.BLL.UsersBLL();
                try
                {
                    UB.UpdateIsLogin(Utility.KillSqlInjection(txtUserName.Text.Trim()), DateTime.Now.AddMinutes(0.3).ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());
                    //UB.UpdateIsLogin(Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());
                    //UB.UpdateIsLogin(Session["userName"].ToString(), DateTime.Now.AddMinutes(0.3).ToString("dd/MM/yyyy HH:mm:ss"), Session["UUID"].ToString());
                }
                catch
                {
                }
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Login_Widget", "btnLogin_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        //redirect
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["SEMSADMIN"].ToString()));
        Response.End();
    CHANGEPASS:
        gotochangepass(lblAlert.Text);

    }

    private void ClearCacheItems()
    {
        try
        {
            List<string> keys = new List<string>();
            IDictionaryEnumerator enumerator = Cache.GetEnumerator();

            while (enumerator.MoveNext())
                keys.Add(enumerator.Key.ToString());

            for (int i = 0; i < keys.Count; i++)
                Cache.Remove(keys[i]);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        
    }

    private bool checklogin()
    {
        string ServiceLogin = SmartPortal.Constant.IPC.SEMS;
        bool IBMultiLogin = Convert.ToBoolean(ConfigurationManager.AppSettings["IBIsMultilogin"].ToString());
        bool checkexpirepolicy = false;
        try
        {
            UsersSEMSModel UserIB = new UsersSEMSModel();
            UserIB = new UsersBLL().GetUserSEMSLogin(ServiceLogin, txtUserName.Text.Trim());
            DataTable dtpolicy = new UsersBLL().GetPolicybyUserID(ServiceLogin, txtUserName.Text.Trim());
            if (dtpolicy.Rows.Count == 0)
            {
                SmartPortal.Common.Log.WriteLogFile("SEMS Login error", "", "", "No exists Policy for this user" + UserIB.UserName);

            }
            if (UserIB == null || UserIB.UserName == null)

            //tbaccountinfo = new SmartPortal.IB.Account().get_accountinfo(ServiceLogin,Utility.KillSqlInjection(txtUserName.Text.Trim()));
            //if (tbaccountinfo.Rows.Count==0)
            {
                //cuongtnp19122019
                lblAlert.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                return false;

            }
            else  //co tai khoan dang ky
            {
                // check policy
                #region CHECK POLICY
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
                            lblAlert.Text = string.Format(Resources.labels.gionaykhongchodangnhaphaydangnhapvaogiochophep, dtpolicy.Rows[0]["lginfr"].ToString(), dtpolicy.Rows[0]["lginto"].ToString());
                            return false;
                        }
                    }
                    //check password age
                    if ((int)dtpolicy.Rows[0]["pwdagemax"] > 0)
                    {
                        if ((int)dtpolicy.Rows[0]["pwdagemax"] - UserIB.pwdage < 0)
                        {
                            lblAlert.Text = Resources.labels.tuoilonnhatcuapassworddahethayresetmatkhau;
                            gotochangepass(lblAlert.Text);
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
                                lblAlert.Text = Resources.labels.accounthasbeenblockbecauseoffailnumber;
                                return false;
                            }
                            else // auto reset failed number
                            {
                                if (UserIB.lastlogin < Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]))
                                {
                                    lblAlert.Text = string.Format(Resources.labels.accounthasbeenblockhaydangnhaplaisau, (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) - UserIB.lastlogin).ToString());
                                    return false;
                                }
                                else
                                {
                                    DataTable tblFailNumber = new SmartPortal.IB.Account().UpdateFailLoginSEMS(Utility.KillSqlInjection(txtUserName.Text.Trim()), "0");
                                    UserIB.Failnumber = 0;
                                    //lblAlert.Text = Resources.labels.accounthasbeenblockbecauseoffailnumber;
                                    //return false;
                                }
                            }

                        }

                    }



                } // end check policy
                #endregion end check policy
                //update last login time
                if (UserIB.Failnumber < Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]))
                {

                    new SmartPortal.IB.User().UpdateLLTwithServiceid(Utility.KillSqlInjection(txtUserName.Text.Trim()), UserIB.SystemTime.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.SEMS);
                    //add failnumber to db
                    //DataTable tblFailNumber = new SmartPortal.IB.Account().UpdateFailLogin(Utility.KillSqlInjection(txtUserName.Text.Trim()), string.Empty);

                }

                ////check expire_date:
                //if (DateTime.Compare(UserIB.Dateexpire.Date, UserIB.SystemTime.Date) < 0)
                //{
                //    lblAlert.Text = Resources.labels.accounthasbeenexpired;
                //    return false;
                //}
                // check password:
                //if (Utility.KillSqlInjection(Encryption.Encrypt(txtPass.Text.Trim())) != UserIB.Password)

                if (SmartPortal.SEMS.O9Encryptpass.sha_shapass(Utility.KillSqlInjection(txtPassword.Text.Trim()), txtUserName.Text.Trim()) != UserIB.Password)
                {

                    //check expire policy
                    if (!checkexpirepolicy)
                    {
                        lblAlert.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                        return false;
                    }
                    //check failed login
                    if (Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]) > 0)
                    {
                        if (UserIB.Failnumber >= Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]))
                        {
                            if (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) == 0) // not auto reset failed number
                            {
                                lblAlert.Text = Resources.labels.accounthasbeenblockbecauseoffailnumber;
                                return false;
                            }
                            else // auto reset failed number
                            {
                                if (UserIB.lastlogin <= Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]))
                                {
                                    lblAlert.Text = string.Format(Resources.labels.accounthasbeenblockhaydangnhaplaisau, (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) - UserIB.lastlogin).ToString());
                                    return false;
                                }
                                else
                                {
                                    lblAlert.Text = Resources.labels.accounthasbeenblockbecauseoffailnumber;
                                    return false;
                                }
                            }

                        }
                        else
                        {
                            //add failnumber to db
                            DataTable tblFailNumber = new SmartPortal.IB.Account().UpdateFailLoginSEMS(Utility.KillSqlInjection(txtUserName.Text.Trim()), string.Empty);
                            //lblAlert.Text = string.Format(Resources.labels.tendangnhaphoacmatkhausaivuilongchuytaikhoansebikhoaneudangnhapsai, dtpolicy.Rows[0]["llkoutthrs"].ToString());
                            lblAlert.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                            return false;
                        }

                    }

                    else
                    {
                        lblAlert.Text = Resources.labels.tendangnhaphocmatkhaukhongdung;
                    }

                    return false;
                }
                else
                {

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
                        DataTable tblFailNumber = new SmartPortal.IB.Account().UpdateFailLoginSEMS(Utility.KillSqlInjection(txtUserName.Text.Trim()), "0");
                    }
                    // check status:
                    switch (UserIB.Status)
                    {
                        case 0:
                            lblAlert.Text = Resources.labels.taikhoanbikhoa;
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
            DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUserName.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());
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
                DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUserName.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());
                return true;
            }
            //if (Session[SmartPortal.Constant.IPC.UUID].ToString() != UUID)
            //{
            // clear uuid ca hai phia db vs client
            // DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUserName.Text.Trim()), string.Empty);
            //   Session[SmartPortal.Constant.IPC.UUID] = null;
            else
            {
                lblAlert.Text = Resources.labels.accountsessionhasbeenexpired;
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
                DataTable tblsessionUUID = new SmartPortal.IB.Account().UpdateUUID(ServiceLogin, Utility.KillSqlInjection(txtUserName.Text.Trim()), Session[SmartPortal.Constant.IPC.UUID].ToString());
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
                    lblAlert.Text = Resources.labels.thisusernameloginbyanotheruser;
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
                    lblAlert.Text = Resources.labels.accountsessionhasbeenexpired;
                    return false;
                }
            }
            else
            {
                lblAlert.Text = Resources.labels.accountsessionhasbeenexpired;
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
    private bool checkpasswordlogin()
    {
        try
        {


            DataTable dtpolicy = new UsersBLL().GetPolicybyUserID(SmartPortal.Constant.IPC.SEMS, txtUserName.Text.Trim());
            DataTable dtpasshis = new UsersBLL().GetPasswordhisbyUserID(SmartPortal.Constant.IPC.SEMS, txtUserName.Text.Trim());

            // check effective date of policy
            // if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare((DateTime)dtpolicy.Rows[0]["efto"], (DateTime)dtpolicy.Rows[0]["systemtime"]) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
            if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare(((DateTime)dtpolicy.Rows[0]["efto"]).Date, ((DateTime)dtpolicy.Rows[0]["systemtime"]).Date) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
            //if (DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare((DateTime)dtpolicy.Rows[0]["efto"], (DateTime)dtpolicy.Rows[0]["systemtime"]) >= 0)
            {

                //check password duplicate with password his:
                if (dtpasshis.Rows.Count != 0)
                {
                    int pwdhis = Convert.ToInt32(dtpolicy.Rows[0]["pwdhis"]);
                    string Passnew = SmartPortal.SEMS.O9Encryptpass.sha_shapass(Utility.KillSqlInjection(txtPassword.Text.Trim()), txtUserName.Text.Trim());
                    for (int i = 0; i < pwdhis; i++)
                    {
                        string passhis = DBNull.Value.Equals(dtpolicy.Rows[0][i]) ? string.Empty : dtpasshis.Rows[0][i].ToString().Trim();
                        if (Passnew == passhis)
                        {
                            //lblError.Text = string.Format(Resources.labels.matkhaukhongduocgiongmatkhaudadattronglichsu, pwdhis.ToString());
                            return false;
                        }
                    }
                }
                // check length of pass
                if (txtPassword.Text.Trim().Length < Convert.ToInt32(dtpolicy.Rows[0]["minpwdlen"]))
                {
                    //lblError.Text = string.Format(Resources.labels.matkhauphaicododaiitnhat, dtpolicy.Rows[0]["minpwdlen"].ToString());

                    return false;
                }

                if (bool.Parse(dtpolicy.Rows[0]["pwdcplx"].ToString()))
                {
                    // check lowercase
                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplxlc"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasLowerCharacter(txtPassword.Text.Trim()))
                        {
                            //lblError.Text = Resources.labels.matkhauphaicoitnhatmotkytuthuong;
                            return false;
                        }

                    }
                    //check upper case
                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplxuc"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasUpperCharacter(txtPassword.Text.Trim()))
                        {
                            //lblError.Text = Resources.labels.matkhauphaicoitnhatmotkytuchuhoa;
                            return false;
                        }
                    }
                    //check symbol
                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplxsc"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasSymbolCharacter(txtPassword.Text.Trim()))
                        {
                            //lblError.Text = Resources.labels.matkhauphaicoitnhatmokytubieutuong;
                            return false;
                        }
                    }
                    // check number
                    if (bool.Parse(dtpolicy.Rows[0]["pwccplxsn"].ToString()))
                    {
                        if (!SmartPortal.SEMS.USERPOLICY.hasNumberCharacter(txtPassword.Text.Trim()))
                        {
                            //lblError.Text = Resources.labels.matkhauphaicoitnhatmokytuso;
                            return false;
                        }
                    }


                }
            }
        }
        catch (Exception ex)
        {

            lblAlert.Text = ex.ToString();
            return false;
        }


        return true;
    }
    private void ShowPopUpMsg(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
    }
    private void gotochangepass(string reason)
    {
        try
        {
            Session["userName"] = txtUserName.Text.Trim();
            string a = reason;
            // lblAlert.Text = string.Empty;
            //Response.Write("<script>alert('Hello');</script>");
            // chú ý sửa cái page đổi pass 1062            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
            "alert('" + a + ". You will be redirected to Change Password.'); window.location='" + Request.ApplicationPath +
            SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=" + PAGECHANGEPASSFIRST) + "';", true);
            //Response.Redirect("~/Default.aspx?p=1079");


        }
        catch (Exception ex)
        {
            lblAlert.Text = ex.ToString();
            // Do nothing.  
            // No need to log exception when we expect ThreadAbortException
        }
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

    protected void lbForgotPassword_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=343"));
    }
}
