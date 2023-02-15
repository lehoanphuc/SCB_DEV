using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using SmartPortal.DAL;
using System.Data.SqlClient;

namespace SmartPortal.IB
{
    public class User
    {
        #region SELECT FULL INFO USER BY UID
        public DataSet GetFullUserByUID(string uid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERSELECTALL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy hết thông tin user theo USER ID");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, uid);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CHANGE PASS FIRST
        public Hashtable OTPAuthen(string UserID, string authenType, string authenCode)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBFIRSTOTPAUTHEN");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authenCode);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authenType);
                
                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void FirstChangePass(string userName,string newPass, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBFIRSTCHANGEPASS");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Thay đổi pass user lần đầu login");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERNAME, userName);
                hasInput.Add(SmartPortal.Constant.IPC.PASSWORD, newPass);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                              
                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsUsingOTP(string userID)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@userID";
            p1.Value = userID;
            p1.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("IB_GETOTPBYCUSTOMER", p1);

            return iRead.Rows.Count > 0;
        }

        #endregion

        #region Lấy thông tin các tài khoản User theo serviceID
        public DataTable GetAccountDetail(string userID, string serviceID)
        {           
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@userID";
            p1.Value = userID;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@serviceID";
            p2.Value = serviceID;
            p2.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("IB_GETACCOUNTDETAIL", p1, p2);


            return iRead;
        }
        #endregion

        #region Lấy tất cả các quyền của User
        public DataTable GetRoleDetail(string userID)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@userID";
            p1.Value = userID;
            p1.SqlDbType = SqlDbType.Text;


            iRead = DataAccess.GetFromDataTable("IB_GETTRANCODEDETAIL", p1);


            return iRead;
        }
        #endregion

        #region update user
        public DataSet Update(string userID, string UserFullName, string status, string userGender, string userAddress, string userEmail, string userPhone, string userModify, string userType, string userLevel, string deptID, string tokenID, string smsOTP, string userBirthday, string IBUserName, string IBPassword, string IBStatus, string dateModified, string SMSPhoneNo, string SMSDefaultAcctno, string SMSIsDefault, string SMSStatus, string SMSDefaultLang, string MBPhoneNo, string MBPassword, string MBStatus, string PHOPhoneNo, string PHOPassword, string PHOStatus, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00402");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Cập nhật thông tin người dùng doanh nghiệp");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.USERMODIFIED, userModify);

                #region Update bảng User
                object[] updateUser = new object[2];
                updateUser[0] = "SEMS_EBA_USER_UPDATE";
                //tao bang chua thong tin user
                DataTable tblUser = new DataTable();
                DataColumn colUserID = new DataColumn("colUserID");
                DataColumn colUFullName = new DataColumn("colUFullName");
                DataColumn colUGender = new DataColumn("colUGender");
                DataColumn colUAddress = new DataColumn("colUAddress");
                DataColumn colUEmail = new DataColumn("colUEmail");
                DataColumn colUPhone = new DataColumn("colUPhone");
                DataColumn colUStatus = new DataColumn("colUStatus");
                DataColumn colUUserModify = new DataColumn("colUUserModify");
                DataColumn colULastModify = new DataColumn("colULastModify");
                DataColumn colUserType = new DataColumn("colUserType");
                DataColumn colUserLevel = new DataColumn("colUserLevel");
                DataColumn colDeptID = new DataColumn("colDeptID");
                DataColumn colTokenID = new DataColumn("colTokenID");
                DataColumn colSMSOTP = new DataColumn("colSMSOTP");
                DataColumn colSMSBirthday = new DataColumn("colSMSBirthday");


                //add vào table
                tblUser.Columns.Add(colUserID);
                tblUser.Columns.Add(colUFullName);
                tblUser.Columns.Add(colUGender);
                tblUser.Columns.Add(colUAddress);
                tblUser.Columns.Add(colUEmail);
                tblUser.Columns.Add(colUPhone);
                tblUser.Columns.Add(colUStatus);
                tblUser.Columns.Add(colUUserModify);
                tblUser.Columns.Add(colULastModify);
                tblUser.Columns.Add(colUserType);
                tblUser.Columns.Add(colUserLevel);
                tblUser.Columns.Add(colDeptID);
                tblUser.Columns.Add(colTokenID);
                tblUser.Columns.Add(colSMSOTP);
                tblUser.Columns.Add(colSMSBirthday);

                //tao 1 dong du lieu
                DataRow row2 = tblUser.NewRow();
                row2["colUserID"] = userID;
                row2["colUFullName"] = UserFullName;
                row2["colUGender"] = userGender;
                row2["colUAddress"] = userAddress;
                row2["colUEmail"] = userEmail;
                row2["colUPhone"] = userPhone;
                row2["colUStatus"] = status;
                row2["colUUserModify"] = userModify;
                row2["colULastModify"] = dateModified;
                row2["colUserType"] = userType;
                row2["colUserLevel"] = userLevel;
                row2["colDeptID"] = deptID;
                row2["colTokenID"] = tokenID;
                row2["colSMSOTP"] = smsOTP;
                row2["colSMSBirthday"] = userBirthday;

                tblUser.Rows.Add(row2);

                //add vao phan tu thu 2 mang object
                updateUser[1] = tblUser;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSER, updateUser);
                #endregion

                #region update bảng user IB
                DataTable tblUserIB = new DataTable();

                DataColumn colUserName = new DataColumn("colUserName");
                DataColumn colPassword = new DataColumn("colPassword");
                DataColumn colStatus = new DataColumn("colStatus");
                DataColumn colUserModified = new DataColumn("colUserModified");
                DataColumn colLastModified = new DataColumn("colLastModified");

                tblUserIB.Columns.Add(colUserName);
                tblUserIB.Columns.Add(colPassword);
                tblUserIB.Columns.Add(colStatus);
                tblUserIB.Columns.Add(colUserModified);
                tblUserIB.Columns.Add(colLastModified);



                if (IBUserName != "")
                {
                    DataRow rowUserIB = tblUserIB.NewRow();
                    rowUserIB["colUserName"] = IBUserName;
                    rowUserIB["colPassword"] = IBPassword;
                    rowUserIB["colStatus"] = IBStatus;
                    rowUserIB["colUserModified"] = userModify;
                    rowUserIB["colLastModified"] = dateModified;
                    tblUserIB.Rows.Add(rowUserIB);
                }

                object[] insertIbankUser = new object[2];
                insertIbankUser[0] = "SEMS_IBS_USERS_UPDATE";

                //add vao phan tu thu 2 mang object
                insertIbankUser[1] = tblUserIB;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSERIB, insertIbankUser);

                #endregion

                #region update bảng user SMS
                DataTable tblUserSMS = new DataTable();

                DataColumn colSMSUserID = new DataColumn("colSMSUserID");
                DataColumn colSMSPhoneNo = new DataColumn("colSMSPhoneNo");
                DataColumn colSMSDefaultAcctno = new DataColumn("colSMSDefaultAcctno");
                DataColumn colSMSIsDefault = new DataColumn("colSMSIsDefault");
                DataColumn colSMSStatus = new DataColumn("colSMSStatus");
                DataColumn colSMSUserModify = new DataColumn("colSMSUserModify");
                DataColumn colSMSLastModify = new DataColumn("colSMSLastModify");
                DataColumn colSMSDefaultLang = new DataColumn("colSMSDefaultLang");

                tblUserSMS.Columns.Add(colSMSUserID);
                tblUserSMS.Columns.Add(colSMSPhoneNo);
                tblUserSMS.Columns.Add(colSMSDefaultAcctno);
                tblUserSMS.Columns.Add(colSMSIsDefault);
                tblUserSMS.Columns.Add(colSMSStatus);
                tblUserSMS.Columns.Add(colSMSUserModify);
                tblUserSMS.Columns.Add(colSMSLastModify);
                tblUserSMS.Columns.Add(colSMSDefaultLang);


                if (SMSPhoneNo != "")
                {
                    DataRow rowUserSMS = tblUserSMS.NewRow();
                    rowUserSMS["colSMSUserID"] = userID;
                    rowUserSMS["colSMSPhoneNo"] = SMSPhoneNo;
                    rowUserSMS["colSMSDefaultAcctno"] = SMSDefaultAcctno;
                    rowUserSMS["colSMSIsDefault"] = SMSIsDefault;
                    rowUserSMS["colSMSStatus"] = SMSStatus;
                    rowUserSMS["colSMSUserModify"] = userModify;
                    rowUserSMS["colSMSLastModify"] = dateModified;
                    rowUserSMS["colSMSDefaultLang"] = SMSDefaultLang;
                    tblUserSMS.Rows.Add(rowUserSMS);
                }

                object[] insertSMSUser = new object[2];
                insertSMSUser[0] = "SEMS_SMS_USERS_UPDATE";

                //add vao phan tu thu 2 mang object
                insertSMSUser[1] = tblUserSMS;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSERSMS, insertSMSUser);

                #endregion

                #region update bảng user MB
                DataTable tblUserMB = new DataTable();

                DataColumn colMBUserID = new DataColumn("colMBUserID");
                DataColumn colMBPhoneNo = new DataColumn("colMBPhoneNo");
                DataColumn colMBPassword = new DataColumn("colMBPassword");
                DataColumn colMBStatus = new DataColumn("colMBStatus");


                tblUserMB.Columns.Add(colMBUserID);
                tblUserMB.Columns.Add(colMBPhoneNo);
                tblUserMB.Columns.Add(colMBPassword);
                tblUserMB.Columns.Add(colMBStatus);


                if (MBPhoneNo != "")
                {
                    DataRow rowUserMB = tblUserMB.NewRow();
                    rowUserMB["colMBUserID"] = userID;
                    rowUserMB["colMBPhoneNo"] = MBPhoneNo;
                    rowUserMB["colMBPassword"] = MBPassword;
                    rowUserMB["colMBStatus"] = MBStatus;

                    tblUserMB.Rows.Add(rowUserMB);
                }

                object[] insertMBUser = new object[2];
                insertMBUser[0] = "SEMS_MB_USERS_UPDATE";

                //add vao phan tu thu 2 mang object
                insertMBUser[1] = tblUserMB;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSERMB, insertMBUser);

                #endregion

                #region update bảng user PHO
                DataTable tblUserPHO = new DataTable();

                DataColumn colPHOUserID = new DataColumn("colPHOUserID");
                DataColumn colPHOPhoneNo = new DataColumn("colPHOPhoneNo");
                DataColumn colPHOPassword = new DataColumn("colPHOPassword");
                DataColumn colPHOStatus = new DataColumn("colPHOStatus");


                tblUserPHO.Columns.Add(colPHOUserID);
                tblUserPHO.Columns.Add(colPHOPhoneNo);
                tblUserPHO.Columns.Add(colPHOPassword);
                tblUserPHO.Columns.Add(colPHOStatus);


                if (PHOPhoneNo != "")
                {
                    DataRow rowUserPHO = tblUserPHO.NewRow();
                    rowUserPHO["colPHOUserID"] = userID;
                    rowUserPHO["colPHOPhoneNo"] = PHOPhoneNo;
                    rowUserPHO["colPHOPassword"] = PHOPassword;
                    rowUserPHO["colPHOStatus"] = PHOStatus;

                    tblUserPHO.Rows.Add(rowUserPHO);
                }

                object[] insertPHOUser = new object[2];
                insertPHOUser[0] = "SEMS_PHO_USERS_UPDATE";

                //add vao phan tu thu 2 mang object
                insertPHOUser[1] = tblUserPHO;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSERPHO, insertPHOUser);

                #endregion

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public void UpdateLLT(string username, string datetime)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@UserName";
                p1.Value = username;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@llt";
                p2.Value = datetime;
                p2.SqlDbType = SqlDbType.VarChar;


                strErr = DataAccess.Execute("IB_UpdateLLT", p1, p2);
                
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateIsLogin(string userName, string expireTime, string UUID)
        {
            try
            {
                int strErr = DataAccess.Execute("IB_UpdateIslogin", new SqlParameter[]
		{
			new SqlParameter
			{
				ParameterName = "@username",
				Value = userName,
				SqlDbType = SqlDbType.VarChar
			},
			new SqlParameter
			{
				ParameterName = "@expiretime",
				Value = expireTime,
				SqlDbType = SqlDbType.VarChar
			},
			new SqlParameter
			{
				ParameterName = "@UUID",
				Value = UUID,
				SqlDbType = SqlDbType.VarChar
			}
		});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetIsLogin(string username, string expireTime)
        {

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@username";
            p1.Value = username;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@expiretime";
            p2.Value = expireTime;
            p2.SqlDbType = SqlDbType.VarChar;

            return DataAccess.GetFromDataTable("IB_GetIslogin", p1, p2);


        }

        #region CHECK VALID  USER
        public DataTable CheckValidUser(string USERNAME, string EMAIL)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@USERNAME";
                p1.Value = USERNAME;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@EMAIL";
                p2.Value = EMAIL;
                p2.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Users_CHECKVALIDUSERIB", p1, p2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RESET PASSWORDS  USER
        public DataTable ResetPasswordUser(string USERNAME, string PASSWORD)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@USERNAME";
                p1.Value = USERNAME;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@PASSWORD";
                p3.Value = PASSWORD;
                p3.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Users_RESETPASSWORDSIB", p1, p3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public void UpdateLLTwithServiceid(string username, string datetime, string serviceid)
        {
            try
            {
                int strErr = DataAccess.Execute("IB_UpdateLLTwithServiceid", new SqlParameter[]
                {
                    new SqlParameter
                    {
                        ParameterName = "@UserName",
                        Value = username,
                        SqlDbType = SqlDbType.NVarChar
                    },
                    new SqlParameter
                    {
                        ParameterName = "@llt",
                        Value = datetime,
                        SqlDbType = SqlDbType.VarChar
                    },
                    new SqlParameter
                    {
                        ParameterName = "@serviceid",
                        Value = serviceid,
                        SqlDbType = SqlDbType.VarChar
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
