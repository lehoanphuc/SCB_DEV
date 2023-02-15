using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Configuration;
using IdentityGuardAuthServiceV3API;
using System.Web.Services;
using System.Xml;
using IdentityGuardAdminServiceV3API;
using DBConnection;
using Utility;
using System.Data;

namespace Authentication
{
    public class EntrustToken
    {
        private static AuthenticationService binding = null;
        private static IdentityGuardAdminServiceV3API.AdminService adminBinding  = null;
        public EntrustToken()
        {
            try
            {
                if (binding == null)
                {
                    binding = new AuthenticationService();
                    binding.Url = ConfigurationManager.AppSettings["EntrustURL"];
                }
                if (adminBinding == null)
                {
                    adminBinding = new AdminService();
                    adminBinding.Url = ConfigurationManager.AppSettings["EntrustAdminURL"];
                }
            }
            catch
            {
            }
        }
        public Hashtable AuthenOTP(string UserName, string Group, string OTPToken)
        {
            Hashtable htOut = new Hashtable();
            try
            {

                GetAllowedAuthenticationTypesCallParms types_parms = new GetAllowedAuthenticationTypesCallParms();
                types_parms.userId = Group + "/" + UserName;
                AllowedAuthenticationTypes types = new AllowedAuthenticationTypes();
                types = binding.getAllowedAuthenticationTypes(types_parms);
                AuthenticateGenericChallengeCallParms authCallParm = new AuthenticateGenericChallengeCallParms();
                authCallParm.userId = Group + "/" + UserName;
                authCallParm.response = new Response();
                authCallParm.parms = new GenericAuthenticateParms();
                authCallParm.parms.AuthenticationType = IdentityGuardAuthServiceV3API.AuthenticationType.TOKENRO;
                authCallParm.response.response = new string[] {OTPToken};
                GenericAuthenticateResponse auth_Response = binding.authenticateGenericChallenge(authCallParm);

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw new Exception(GetEntrustErrorDesc(ex.Detail.InnerXml));
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw new Exception("Connect authentication server fail");
            }
            htOut.Add(Common.KEYNAME.ERRORCODE, "0");
            htOut.Add(Common.KEYNAME.ERRORDESC, "OK");
            return htOut;
        }

        private string GetEntrustErrorDesc(string strXML)
        {
            string result = string.Empty;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXML);
                result = doc.GetElementsByTagName("ErrorCode")[0].InnerText;
            }
            catch (Exception ex)
            {
                return "Authentication Error";
            }
            return result;
        }

        public string SynchronizeToken(string UserName, string Group, string OTPToken1, string OTPToken2, string sernum)
        {
            string result = string.Empty;
            try
            {
                adminBinding.CookieContainer = new System.Net.CookieContainer();
                #region login
                LoginParms loginParms = new LoginParms();
                loginParms.adminId = Common.OTPUser;
                loginParms.password = Common.OTPPass;
                //loginParms.response 
                LoginCallParms callParms = new LoginCallParms();
                callParms.parms = loginParms;

                LoginResult loginResult = adminBinding.login(callParms);
                if (loginResult.state.Equals(LoginState.COMPLETE))
                {
                    UserTokenParms userTokenParms = new UserTokenParms();
                    userTokenParms.resetToken = true;
                    userTokenParms.resetTokenResponse1 = OTPToken1;
                    userTokenParms.resetTokenResponse2 = OTPToken2;
                    UserTokenSetCallParms userTokenSetCallParms = new UserTokenSetCallParms();
                    userTokenSetCallParms.userid = Group + "/" + UserName;

                    UserTokenFilter filter = new UserTokenFilter();

                    filter.SerialNumber = sernum;

                    userTokenSetCallParms.filter = filter;

                    userTokenSetCallParms.parms = userTokenParms;

                    int numSet = adminBinding.userTokenSet(userTokenSetCallParms);
                    adminBinding.Dispose();
                }
                else
                {
                    return "Admin login fail";
                }

                #endregion

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public bool GenSMSOTP(TransactionInfo tran, string userid)
        {
            try
            {   
                string deviceid = "" ;
                if (tran.Data.ContainsKey(userid))
                {
                    deviceid = tran.Data[userid].ToString();
                }
                else
                {
                    deviceid = userid;
                }
                IdentityGuardAdminServiceV3API.AdminService adminBinding = new AdminService();
                adminBinding.CookieContainer = new System.Net.CookieContainer();
                adminBinding.Url = ConfigurationManager.AppSettings["EntrustAdminURL"];
                #region login
                LoginParms loginParms = new LoginParms();
                loginParms.adminId = Common.OTPUser;
                loginParms.password = Common.OTPPass ;
                //loginParms.response 
                LoginCallParms callParms = new LoginCallParms();
                callParms.parms = loginParms;

                LoginResult loginResult = adminBinding.login(callParms);
                if (loginResult.state.Equals(LoginState.COMPLETE))
                {

                }
                else
                {
                    return false;
                }

                #endregion
                // The following code generates the OTP for a given user ID
                UserOTPParms userOTPParms = new UserOTPParms();
                // The lifetime of the OTP in milliseconds
                userOTPParms.Lifetime = Common.OTPLifeTime;
                UserOTPCreateCallParms callParms_lg = new UserOTPCreateCallParms();
                callParms_lg.userid = deviceid;
                callParms_lg.parms = userOTPParms;
                callParms_lg.parms.Force = true;
                UserOTPInfo otp = adminBinding.userOTPCreate(callParms_lg);
                // The following code retrieves the OTP for a given user ID
                UserOTPGetCallParms callParms_getOTP = new UserOTPGetCallParms();
                callParms_getOTP.userid = deviceid;

                //UserOTPInfo otp = adminBinding.userOTPGet(callParms_getOTP);
                otp = adminBinding.userOTPGet(callParms_getOTP);
                tran.Data[Common.KEYNAME.SMSOTP] = otp.OTP;
                //InsertOTP(deviceid, otp.OTP, (DateTime)otp.CreateDate, (DateTime)otp.ExpireDate);
                InsertOTP(deviceid, otp.OTP, int.Parse(Common.OTPLifeTime.ToString()));
                
                // deliver the OTP to the user using an application-specific 
                // mechanism
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public Hashtable AuthenOTPSMS(string userid, string OTP,string trancode)
        {
            Hashtable htOut = new Hashtable();
            try
            {
                Connection dbObj = new Connection();
                DataTable dt = new DataTable();
                dt = dbObj.FillDataTable(Common.ConStr, "IPC_AUTHENOTP", userid, OTP,trancode);
                if (dt == null || dt.Rows.Count == 0)
                {
                    throw new Exception("Error connect server");
                }
                else
                {
                    htOut.Add(Common.KEYNAME.ERRORCODE, dt.Rows[0]["ERRORCODE"].ToString());
                    htOut.Add(Common.KEYNAME.ERRORDESC, dt.Rows[0]["ERRORDESC"].ToString());
                }
            }
            catch(Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw new Exception(ex.Message);
            }
            return htOut;
        }
        #region Sub-Function
        private void InsertOTP(string userid, string OTP, int LifeTime)
        {
            Connection dbObj = new Connection();
            try
            {
                dbObj.ExecuteNonquery(Common.ConStr, "IPC_OTPDETAILINSERT", userid, OTP, LifeTime);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    
}
