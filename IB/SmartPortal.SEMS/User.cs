using System;
using System.Collections.Generic;

using System.Text;
using System.Collections;
using System.Data;
using SmartPortal.DAL;

using System.Data.SqlClient;
using SmartPortal.Constant;

namespace SmartPortal.SEMS
{
    public class User
    {
        #region Check service to send email
        public bool CheckServiceIsUsed(string userid, string servicename, ref string errorCode, ref string errorDesc)
        {
            //vutran: kiem tra cac dich vu da dang ky va da duoc su dung cua user
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                object[] para = new object[] { userid, servicename };
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000113");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add("STORENAME", "EBA_CHECKSERVICEBYUSERID");
                hasInput.Add("PARA", para);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    bool result = bool.Parse(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.RESULT].ToString());
                    return result;
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region SELECT user BY CUSTCODE
        public DataSet GetUserByCustcode(string custCode, string Level, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERGBCC");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin user theo custcode");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.USERLEVEL, Level);

                hasInput.Add(SmartPortal.Constant.IPC.CUSTID, custCode);


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

        #region Select role OF USER by serviceID
        public DataSet GetUserRoleByServiceID(string serviceID, string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERROLE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy thông tin role của từng user bỡi serviceid");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceID);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);


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

        #region INSERT USER
        //vutt sms notify danh cho corp
        public DataSet Insert(string contractNo, string endDate, string lastModify, string userCreate, string userLastModify, string userApprove, string status, string userID, string UserFullName, string userGender, string userAddress, string userEmail, string userPhone, string UcreateDate, string userModify, string userType, string userLevel, string type, string deptID, string tokenID, string tokenIssueDate, string smsOTP, string userBirthday, string IBUserName, string IBPassword, string IBStatus, string SMSPhoneNo, string SMSStatus, string SMSDefaultAcctno, string smsDefaultLang, string smsIsDefault, string MBPhoneNo, string MBPass, string MBStatus, string PHOPhoneNo, string PHOPass, string PHOStatus, string PHODefaultAcctno, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable UserGroup, DataTable TranrightDetail, DataTable UserAccount, string IBpolicyid, string SMSpolicyid, string MBpolicyid, string pwdreset, string pwdresetsms, ref string errorCode, ref string errorDesc)
        {
            return Insert(contractNo, endDate, lastModify, userCreate, userLastModify, userApprove, status, userID, UserFullName, userGender, userAddress, userEmail, userPhone, UcreateDate, userModify, userType, userLevel, type, deptID, tokenID, tokenIssueDate, smsOTP, userBirthday, IBUserName, IBPassword, IBStatus, SMSPhoneNo, SMSStatus, SMSDefaultAcctno, smsDefaultLang, smsIsDefault, MBPhoneNo, MBPass, MBStatus, PHOPhoneNo, PHOPass, PHOStatus, PHODefaultAcctno, IBUserRight, SMSUserRight, MBUserRight, PHOUserRight, UserGroup, TranrightDetail, UserAccount, IBpolicyid, SMSpolicyid, MBpolicyid, pwdreset, pwdresetsms, new DataTable(), ref errorCode, ref errorDesc);
        }
        public DataSet Insert(string contractNo, string endDate, string lastModify, string userCreate, string userLastModify, string userApprove, string status, string userID, string UserFullName, string userGender, string userAddress, string userEmail, string userPhone, string UcreateDate, string userModify, string userType, string userLevel, string type, string deptID, string tokenID, string tokenIssueDate, string smsOTP, string userBirthday, string IBUserName, string IBPassword, string IBStatus, string SMSPhoneNo, string SMSStatus, string SMSDefaultAcctno, string smsDefaultLang, string smsIsDefault, string MBPhoneNo, string MBPass, string MBStatus, string PHOPhoneNo, string PHOPass, string PHOStatus, string PHODefaultAcctno, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable UserGroup, DataTable TranrightDetail, DataTable UserAccount, string IBpolicyid, string SMSpolicyid, string MBpolicyid, string pwdreset, string pwdresetsms, DataTable SMSNotify, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tạo user mới");
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);

                #region Insert bảng User
                object[] insertUser = new object[2];
                insertUser[0] = "SEMS_EBA_USER_INSERT";
                //tao bang chua thong tin user
                DataTable tblUser = new DataTable();
                DataColumn colUserID = new DataColumn("colUserID");
                DataColumn colUContractNo = new DataColumn("colUContractNo");
                DataColumn colULocalName = new DataColumn("colULocalName");
                DataColumn colUFullName = new DataColumn("colUFullName");
                DataColumn colUGender = new DataColumn("colUGender");
                DataColumn colUAddress = new DataColumn("colUAddress");
                DataColumn colUEmail = new DataColumn("colUEmail");
                DataColumn colUPhone = new DataColumn("colUPhone");
                DataColumn colUStatus = new DataColumn("colUStatus");
                DataColumn colUUserCreate = new DataColumn("colUUserCreate");
                DataColumn colUDateCreate = new DataColumn("colUDateCreate");
                DataColumn colUUserModify = new DataColumn("colUUserModify");
                DataColumn colULastModify = new DataColumn("colULastModify");
                DataColumn colUUserApprove = new DataColumn("colUUserApprove");
                DataColumn colUserType = new DataColumn("colUserType");
                DataColumn colUserLevel = new DataColumn("colUserLevel");
                DataColumn colDeptID = new DataColumn("colDeptID");
                DataColumn colTokenID = new DataColumn("colTokenID");
                DataColumn colTokenIssueDate = new DataColumn("colTokenIssueDate");
                DataColumn colSMSOTP = new DataColumn("colSMSOTP");
                DataColumn colSMSBirthday = new DataColumn("colSMSBirthday");
                DataColumn colType = new DataColumn("colType");
                DataColumn colCtType = new DataColumn("colCtType");


                //add vào table
                tblUser.Columns.Add(colUserID);
                tblUser.Columns.Add(colUContractNo);
                tblUser.Columns.Add(colULocalName);
                tblUser.Columns.Add(colUFullName);
                tblUser.Columns.Add(colUGender);
                tblUser.Columns.Add(colUAddress);
                tblUser.Columns.Add(colUEmail);
                tblUser.Columns.Add(colUPhone);
                tblUser.Columns.Add(colUStatus);
                tblUser.Columns.Add(colUUserCreate);
                tblUser.Columns.Add(colUDateCreate);
                tblUser.Columns.Add(colUUserModify);
                tblUser.Columns.Add(colULastModify);
                tblUser.Columns.Add(colUUserApprove);
                tblUser.Columns.Add(colUserType);
                tblUser.Columns.Add(colUserLevel);
                tblUser.Columns.Add(colDeptID);
                tblUser.Columns.Add(colTokenID);
                tblUser.Columns.Add(colTokenIssueDate);
                tblUser.Columns.Add(colSMSOTP);
                tblUser.Columns.Add(colSMSBirthday);
                tblUser.Columns.Add(colType);
                tblUser.Columns.Add(colCtType);

                //tao 1 dong du lieu
                DataRow row2 = tblUser.NewRow();
                row2["colUserID"] = userID;
                row2["colUContractNo"] = contractNo;
                row2["colULocalName"] = UserFullName;
                row2["colUFullName"] = UserFullName;
                row2["colUGender"] = userGender;
                row2["colUAddress"] = userAddress;
                row2["colUEmail"] = userEmail;
                row2["colUPhone"] = userPhone;
                row2["colUStatus"] = status;
                row2["colUUserCreate"] = userCreate;
                row2["colUDateCreate"] = UcreateDate;
                row2["colUUserModify"] = userModify;
                row2["colULastModify"] = lastModify;

                row2["colUUserApprove"] = userApprove;
                row2["colUserType"] = userType;
                row2["colUserLevel"] = userLevel;
                row2["colDeptID"] = deptID;
                row2["colTokenID"] = tokenID;
                row2["colTokenIssueDate"] = tokenIssueDate;
                row2["colSMSOTP"] = smsOTP;
                row2["colSMSBirthday"] = userBirthday;
                row2["colType"] = type;
                row2["colCtType"] = "";

                tblUser.Rows.Add(row2);

                //add vao phan tu thu 2 mang object
                insertUser[1] = tblUser;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSER, insertUser);
                #endregion

                if (IBUserName != "")
                {

                    #region Insert bảng user Ibank
                    object[] insertIbankUser = new object[2];
                    insertIbankUser[0] = "SEMS_IBS_USER_INSERT";
                    //tao bang chua thong tin customer
                    DataTable tblIbankUser = new DataTable();
                    DataColumn colUserName = new DataColumn("colUserName");
                    DataColumn colIBUserID = new DataColumn("colIBUserID");
                    DataColumn colIBPassword = new DataColumn("colIBPassword");
                    DataColumn colLastLoginTime = new DataColumn("colLastLoginTime");
                    DataColumn colIBStatus = new DataColumn("colIBStatus");
                    DataColumn colIBUserCreate = new DataColumn("colIBUserCreate");
                    DataColumn colIBDateCreate = new DataColumn("colIBDateCreate");
                    DataColumn colIBUserModify = new DataColumn("colIBUserModify");
                    DataColumn colIBLastModify = new DataColumn("colIBLastModify");
                    DataColumn colIBUserApprove = new DataColumn("colIBUserApprove");
                    DataColumn colIBIsLogin = new DataColumn("colIBIsLogin");
                    DataColumn colIBDateExpire = new DataColumn("colIBDateExpire");
                    DataColumn colIBExpireTime = new DataColumn("colIBExpireTime");
                    DataColumn colIBPolicyusr = new DataColumn("colIBPolicyusr");
                    DataColumn colpwdreset = new DataColumn("colpwdreset");
                    //hunglt 
                    DataColumn colloginMethod = new DataColumn("colloginMethod");
                    DataColumn colauthen = new DataColumn("colauthen");


                    //add vào table
                    tblIbankUser.Columns.Add(colUserName);
                    tblIbankUser.Columns.Add(colIBUserID);
                    tblIbankUser.Columns.Add(colIBPassword);
                    tblIbankUser.Columns.Add(colLastLoginTime);
                    tblIbankUser.Columns.Add(colIBStatus);
                    tblIbankUser.Columns.Add(colIBUserCreate);
                    tblIbankUser.Columns.Add(colIBDateCreate);
                    tblIbankUser.Columns.Add(colIBUserModify);
                    tblIbankUser.Columns.Add(colIBLastModify);
                    tblIbankUser.Columns.Add(colIBUserApprove);
                    tblIbankUser.Columns.Add(colIBIsLogin);
                    tblIbankUser.Columns.Add(colIBDateExpire);
                    tblIbankUser.Columns.Add(colIBExpireTime);
                    tblIbankUser.Columns.Add(colIBPolicyusr);
                    tblIbankUser.Columns.Add(colpwdreset);
                    //hunglt
                    tblIbankUser.Columns.Add(colloginMethod);
                    tblIbankUser.Columns.Add(colauthen);
                    //tao 1 dong du lieu
                    DataRow row3 = tblIbankUser.NewRow();
                    row3["colUserName"] = IBUserName;
                    row3["colIBUserID"] = userID;
                    row3["colIBPassword"] = IBPassword;
                    row3["colLastLoginTime"] = UcreateDate;
                    row3["colIBStatus"] = IBStatus;
                    row3["colIBUserCreate"] = userCreate;
                    row3["colIBDateCreate"] = UcreateDate;
                    row3["colIBUserModify"] = userCreate;
                    row3["colIBLastModify"] = lastModify;
                    row3["colIBUserApprove"] = userApprove;
                    row3["colIBIsLogin"] = "Y";
                    row3["colIBDateExpire"] = endDate;
                    row3["colIBExpireTime"] = UcreateDate;
                    row3["colIBPolicyusr"] = IBpolicyid;
                    row3["colpwdreset"] = pwdreset;
                    row3["colloginMethod"] = "USERNAME";
                    row3["colauthen"] = "PASSWORD";

                    tblIbankUser.Rows.Add(row3);

                    //add vao phan tu thu 2 mang object
                    insertIbankUser[1] = tblIbankUser;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTIBANKUSER, insertIbankUser);
                    #endregion

                    #region Insert quyền user ibank
                    object[] insertIbankUserRight = new object[2];
                    insertIbankUserRight[0] = "SEMS_IBS_USERINROLE_INSERT";

                    //add vao phan tu thu 2 mang object
                    insertIbankUserRight[1] = IBUserRight;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTIBANKUSERRIGHT, insertIbankUserRight);
                    #endregion

                }
                else
                {
                    #region Insert bảng user Ibank null
                    object[] insertIbankUser = new object[2];
                    insertIbankUser[0] = "SEMS_IBS_USER_INSERT";
                    //tao bang chua thong tin customer
                    DataTable tblIbankUser = null;

                    //add vao phan tu thu 2 mang object
                    insertIbankUser[1] = tblIbankUser;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTIBANKUSER, insertIbankUser);
                    #endregion

                    #region Insert quyền user ibank null
                    object[] insertIbankUserRight = new object[2];
                    insertIbankUserRight[0] = "SEMS_IBS_USERINROLE_INSERT";

                    //add vao phan tu thu 2 mang object
                    insertIbankUserRight[1] = null;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTIBANKUSERRIGHT, insertIbankUserRight);
                    #endregion
                }

                if (SMSPhoneNo != "")
                {

                    #region Insert bảng user SMS
                    object[] insertSMSUser = new object[2];
                    insertSMSUser[0] = "SEMS_SMS_USER_INSERT";
                    //tao bang chua thong tin customer
                    DataTable tblSMSUser = new DataTable();
                    DataColumn colSMSUserID = new DataColumn("colSMSUserID");
                    DataColumn colSMSPhoneNo = new DataColumn("colSMSPhoneNo");
                    DataColumn colSMSContractNo = new DataColumn("colSMSContractNo");
                    DataColumn colSMSIsBroadcast = new DataColumn("colSMSIsBroadcast");
                    DataColumn colSMSDefaultAcctno = new DataColumn("colSMSDefaultAcctno");
                    DataColumn colSMSDefaultLang = new DataColumn("colSMSDefaultLang");
                    DataColumn colSMSIsDefault = new DataColumn("colSMSIsDefault");
                    DataColumn colSMSPinCode = new DataColumn("colSMSPinCode");
                    DataColumn colSMSStatus = new DataColumn("colSMSStatus");
                    DataColumn colSMSPhoneType = new DataColumn("colSMSPhoneType");
                    DataColumn colSMSUserCreate = new DataColumn("colSMSUserCreate");
                    DataColumn colSMSUserModify = new DataColumn("colSMSUserModify");
                    DataColumn colSMSUserApprove = new DataColumn("colSMSUserApprove");
                    DataColumn colSMSLastModify = new DataColumn("colSMSLastModify");
                    DataColumn colSMSDateCreate = new DataColumn("colSMSDateCreate");
                    DataColumn colSMSDateExpire = new DataColumn("colSMSDateExpire");
                    DataColumn colSMSPolicyusr = new DataColumn("colSMSPolicyusr");
                    DataColumn colpwdresetSMS = new DataColumn("colpwdresetSMS");


                    //add vào table
                    tblSMSUser.Columns.Add(colSMSUserID);
                    tblSMSUser.Columns.Add(colSMSPhoneNo);
                    tblSMSUser.Columns.Add(colSMSContractNo);
                    tblSMSUser.Columns.Add(colSMSIsBroadcast);
                    tblSMSUser.Columns.Add(colSMSDefaultAcctno);
                    tblSMSUser.Columns.Add(colSMSDefaultLang);
                    tblSMSUser.Columns.Add(colSMSIsDefault);
                    tblSMSUser.Columns.Add(colSMSPinCode);
                    tblSMSUser.Columns.Add(colSMSStatus);
                    tblSMSUser.Columns.Add(colSMSPhoneType);
                    tblSMSUser.Columns.Add(colSMSUserCreate);
                    tblSMSUser.Columns.Add(colSMSUserModify);
                    tblSMSUser.Columns.Add(colSMSUserApprove);
                    tblSMSUser.Columns.Add(colSMSLastModify);
                    tblSMSUser.Columns.Add(colSMSDateCreate);
                    tblSMSUser.Columns.Add(colSMSDateExpire);
                    tblSMSUser.Columns.Add(colSMSPolicyusr);
                    tblSMSUser.Columns.Add(colpwdresetSMS);

                    //tao 1 dong du lieu
                    string pincode = SmartPortal.SEMS.O9Encryptpass.sha_sha256(pwdresetsms, SMSPhoneNo);// 
                    //string pincode = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6);
                    DataRow row4 = tblSMSUser.NewRow();
                    row4["colSMSUserID"] = userID;
                    row4["colSMSPhoneNo"] = SMSPhoneNo;
                    row4["colSMSContractNo"] = contractNo;
                    row4["colSMSIsBroadcast"] = "N";
                    row4["colSMSDefaultAcctno"] = SMSDefaultAcctno;
                    row4["colSMSDefaultLang"] = smsDefaultLang;
                    row4["colSMSIsDefault"] = smsIsDefault;
                    row4["colSMSPinCode"] = pincode;
                    row4["colSMSStatus"] = SMSStatus;
                    row4["colSMSPhoneType"] = "";
                    row4["colSMSUserCreate"] = userCreate;
                    row4["colSMSUserModify"] = userModify;
                    row4["colSMSUserApprove"] = userApprove;
                    row4["colSMSLastModify"] = lastModify;
                    row4["colSMSDateCreate"] = UcreateDate;
                    row4["colSMSDateExpire"] = endDate;
                    row4["colSMSPolicyusr"] = SMSpolicyid;
                    row4["colpwdresetSMS"] = pwdresetsms;


                    tblSMSUser.Rows.Add(row4);

                    //add vao phan tu thu 2 mang object
                    insertSMSUser[1] = tblSMSUser;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTSMSUSER, insertSMSUser);
                    #endregion

                    #region Insert quyền user sms
                    object[] insertSMSUserRight = new object[2];
                    insertSMSUserRight[0] = "SEMS_SMS_USERINROLE_INSERT";

                    //add vao phan tu thu 2 mang object
                    insertSMSUserRight[1] = SMSUserRight;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTSMSUSERRIGHT, insertSMSUserRight);
                    #endregion

                }
                else
                {
                    #region Insert bảng user SMS null
                    object[] insertSMSUser = new object[2];
                    insertSMSUser[0] = "SEMS_SMS_USER_INSERT";
                    //tao bang chua thong tin customer
                    DataTable tblSMSUser = null;

                    //add vao phan tu thu 2 mang object
                    insertSMSUser[1] = tblSMSUser;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTSMSUSER, insertSMSUser);
                    #endregion

                    #region Insert quyền user sms null
                    object[] insertSMSUserRight = new object[2];
                    insertSMSUserRight[0] = "SEMS_SMS_USERINROLE_INSERT";

                    //add vao phan tu thu 2 mang object
                    insertSMSUserRight[1] = null;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTSMSUSERRIGHT, insertSMSUserRight);
                    #endregion
                }

                if (MBPhoneNo != "")
                {

                    #region Insert bảng user Mobile
                    object[] insertMBUser = new object[2];
                    insertMBUser[0] = "SEMS_MB_USER_INSERT";
                    //tao bang chua thong tin customer
                    DataTable tblMBUser = new DataTable();
                    DataColumn colMBUserID = new DataColumn("colMBUserID");
                    DataColumn colMBUserName = new DataColumn("colMBUserName");
                    DataColumn colMBPhoneNo = new DataColumn("colMBPhoneNo");

                    DataColumn colMBloginMethod = new DataColumn("colMBloginMethod");
                    DataColumn colMBAuthentype = new DataColumn("colMBAuthentype");
                    DataColumn colMBNewpass = new DataColumn("colMBNewpass");
                    DataColumn colMBNewpin = new DataColumn("colMBNewpin");

                    DataColumn colMBPass = new DataColumn("colMBPass");
                    DataColumn colMBStatus = new DataColumn("colMBStatus");
                    DataColumn colMBPinCode1 = new DataColumn("colMBPinCode1");
                    DataColumn colMBPolicyusr = new DataColumn("colMBPolicyusr");
                    DataColumn colpwdresetMB = new DataColumn("colpwdresetMB");
                    DataColumn colpwPincode = new DataColumn("colpwPincode");
                    DataColumn colContractType = new DataColumn("colContractType");

                    //add vào table
                    tblMBUser.Columns.Add(colMBUserID);
                    tblMBUser.Columns.Add(colMBUserName);
                    tblMBUser.Columns.Add(colMBPhoneNo);
                    tblMBUser.Columns.Add(colMBloginMethod);
                    tblMBUser.Columns.Add(colMBAuthentype);
                    tblMBUser.Columns.Add(colMBNewpass);
                    tblMBUser.Columns.Add(colMBNewpin);
                    tblMBUser.Columns.Add(colMBPass);
                    tblMBUser.Columns.Add(colMBStatus);
                    tblMBUser.Columns.Add(colMBPinCode1);
                    tblMBUser.Columns.Add(colMBPolicyusr);
                    tblMBUser.Columns.Add(colpwdresetMB);
                    tblMBUser.Columns.Add(colpwPincode);
                    tblMBUser.Columns.Add(colContractType);
                    //tao 1 dong du lieu
                    DataRow row5 = tblMBUser.NewRow();
                    row5["colMBUserID"] = userID;
                    row5["colMBUserName"] = IBUserName;
                    row5["colMBPhoneNo"] = MBPhoneNo;
                    row5["colMBloginMethod"] = "USERNAME";
                    row5["colMBAuthentype"] = "PASSWORD";
                    row5["colMBNewpass"] = "Y";
                    row5["colMBNewpin"] = "N";

                    row5["colMBPass"] = MBPass;
                    row5["colMBStatus"] = MBStatus;
                    row5["colMBPinCode1"] = "";
                    row5["colMBPolicyusr"] = MBpolicyid;
                    row5["colpwdresetMB"] = "";
                    row5["colpwPincode"] = "";
                    row5["colContracttype"] = "";

                    tblMBUser.Rows.Add(row5);

                    //add vao phan tu thu 2 mang object
                    insertMBUser[1] = tblMBUser;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTMBUSER, insertMBUser);
                    #endregion

                    #region Insert quyền user MB
                    object[] insertMBUserRight = new object[2];
                    insertMBUserRight[0] = "SEMS_MB_USERINROLE_INSERT";

                    //add vao phan tu thu 2 mang object
                    insertMBUserRight[1] = MBUserRight;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTMBUSERRIGHT, insertMBUserRight);
                    #endregion

                }
                else
                {
                    #region Insert bảng user Mobile null
                    object[] insertMBUser = new object[2];
                    insertMBUser[0] = "SEMS_MB_USER_INSERT";
                    //tao bang chua thong tin customer
                    DataTable tblMBUser = null;


                    //add vao phan tu thu 2 mang object
                    insertMBUser[1] = tblMBUser;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTMBUSER, insertMBUser);
                    #endregion

                    #region Insert quyền user MB
                    object[] insertMBUserRight = new object[2];
                    insertMBUserRight[0] = "SEMS_MB_USERINROLE_INSERT";

                    //add vao phan tu thu 2 mang object
                    insertMBUserRight[1] = null;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTMBUSERRIGHT, insertMBUserRight);
                    #endregion
                }

                if (PHOPhoneNo != "")
                {

                    #region Insert bảng user Phone
                    object[] insertPHOUser = new object[2];
                    insertPHOUser[0] = "SEMS_PHO_USER_INSERT";
                    //tao bang chua thong tin customer
                    DataTable tblPHOUser = new DataTable();
                    DataColumn colPHOUserID = new DataColumn("colPHOUserID");
                    DataColumn colPHOPhoneNo = new DataColumn("colPHOPhoneNo");
                    DataColumn colPHOPass = new DataColumn("colPHOPass");
                    DataColumn colPHOStatus = new DataColumn("colPHOStatus");
                    DataColumn colPHODefaultAcctno1 = new DataColumn("colPHODefaultAcctno1");

                    //add vào table
                    tblPHOUser.Columns.Add(colPHOUserID);
                    tblPHOUser.Columns.Add(colPHOPhoneNo);
                    tblPHOUser.Columns.Add(colPHOPass);
                    tblPHOUser.Columns.Add(colPHOStatus);
                    tblPHOUser.Columns.Add(colPHODefaultAcctno1);

                    //tao 1 dong du lieu
                    DataRow row6 = tblPHOUser.NewRow();
                    row6["colPHOUserID"] = userID;
                    row6["colPHOPhoneNo"] = PHOPhoneNo;
                    row6["colPHOPass"] = PHOPass;
                    row6["colPHOStatus"] = PHOStatus;
                    row6["colPHODefaultAcctno1"] = PHODefaultAcctno;

                    tblPHOUser.Rows.Add(row6);

                    //add vao phan tu thu 2 mang object
                    insertPHOUser[1] = tblPHOUser;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTPHOUSER, insertPHOUser);
                    #endregion

                    #region Insert quyền user PHO
                    object[] insertPHOUserRight = new object[2];
                    insertPHOUserRight[0] = "SEMS_PHO_USERINROLE_INSERT";

                    //add vao phan tu thu 2 mang object
                    insertPHOUserRight[1] = PHOUserRight;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTPHOUSERRIGHT, insertPHOUserRight);
                    #endregion

                }
                else
                {
                    #region Insert bảng user Phone null
                    object[] insertPHOUser = new object[2];
                    insertPHOUser[0] = "SEMS_PHO_USER_INSERT";
                    //tao bang chua thong tin customer
                    DataTable tblPHOUser = null;


                    //add vao phan tu thu 2 mang object
                    insertPHOUser[1] = tblPHOUser;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTPHOUSER, insertPHOUser);
                    #endregion

                    #region Insert quyền user PHO null
                    object[] insertPHOUserRight = new object[2];
                    insertPHOUserRight[0] = "SEMS_PHO_USERINROLE_INSERT";

                    //add vao phan tu thu 2 mang object
                    insertPHOUserRight[1] = null;

                    hasInput.Add(SmartPortal.Constant.IPC.INSERTPHOUSERRIGHT, insertPHOUserRight);
                    #endregion
                }
                //LanNTH
                #region Insert Group cho user
                object[] insertUserGroup = new object[2];
                insertUserGroup[0] = "SEMS_EBA_USERGROUP_INSERT";

                //add vao phan tu thu 2 mang object
                insertUserGroup[1] = UserGroup;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSERGROUP, insertUserGroup);
                #endregion

                #region Insert bang TranrightDetail
                //remove dong rong

                object[] insertTranrightDetail = new object[2];
                insertTranrightDetail[0] = "SEMS_EBA_TRANRIGHTDETAIL_INSERT";

                //add vao phan tu thu 2 mang object
                insertTranrightDetail[1] = TranrightDetail;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTTRANRIGHTDETAIL, insertTranrightDetail);
                #endregion

                #region Insert bang User Account
                object[] insertUserAccount = new object[2];
                insertUserAccount[0] = "SEMS_EBA_USERACCOUNT_INSERT";

                //add vao phan tu thu 2 mang object
                insertUserAccount[1] = UserAccount;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSERACCOUNT, insertUserAccount);
                #endregion

                #region Insert sms notify detail
                object[] insertSMSNotifyDetails = new object[2];
                insertSMSNotifyDetails[0] = "EBA_SMSNOTIFYDETAILS_INSERT";

                //add vao phan tu thu 2 mang object
                insertSMSNotifyDetails[1] = SMSNotify;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTSMSNODETAILS, insertSMSNotifyDetails);
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

        #region DELETE USER BY USER ID
        public DataSet DeleteUserByID(string uid, string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "xo´a user theo uid");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, uid);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);


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

        #region update user
        public DataSet Update(string contractNo, string userID, string UserFullName, string status, string userGender, string userAddress, string userEmail, string userPhone, string userModify, string userType, string userLevel, string type, string deptID, string tokenID, string smsOTP, string userBirthday, string IBUserName, string IBPassword, string IBStatus, string dateModified, string SMSPhoneNo, string SMSDefaultAcctno, string SMSIsDefault, string SMSStatus, string SMSDefaultLang, string MBPhoneNo, string MBPassword, string MBStatus, string PHOPhoneNo, string PHOPassword, string PHOStatus, string PHODefaultAcctno, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable UserGroup, DataTable TranrightDetail, DataTable userAccount, DataTable contractAccount, string IBpolicyid, string SMSpolicyid, string MBpolicyid, string pwdresetsms, ref string errorCode, ref string errorDesc)
        {
            return Update(contractNo, userID, UserFullName, status, userGender, userAddress, userEmail, userPhone, userModify, userType, userLevel, type, deptID, tokenID, smsOTP, userBirthday, IBUserName, IBPassword, IBStatus, dateModified, SMSPhoneNo, SMSDefaultAcctno, SMSIsDefault, SMSStatus, SMSDefaultLang, MBPhoneNo, MBPassword, MBStatus, PHOPhoneNo, PHOPassword, PHOStatus, PHODefaultAcctno, IBUserRight, SMSUserRight, MBUserRight, PHOUserRight, UserGroup, TranrightDetail, userAccount, contractAccount, IBpolicyid, SMSpolicyid, MBpolicyid, pwdresetsms, new DataTable(), ref errorCode, ref errorDesc);
        }
        public DataSet Update(string contractNo, string userID, string UserFullName, string status, string userGender, string userAddress, string userEmail, string userPhone, string userModify, string userType, string userLevel, string type, string deptID, string tokenID, string smsOTP, string userBirthday, string IBUserName, string IBPassword, string IBStatus, string dateModified, string SMSPhoneNo, string SMSDefaultAcctno, string SMSIsDefault, string SMSStatus, string SMSDefaultLang, string MBPhoneNo, string MBPassword, string MBStatus, string PHOPhoneNo, string PHOPassword, string PHOStatus, string PHODefaultAcctno, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable UserGroup, DataTable TranrightDetail, DataTable userAccount, DataTable contractAccount, string IBpolicyid, string SMSpolicyid, string MBpolicyid, string pwdresetsms, DataTable SMSNotify, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Cập nhật user");


                #region Update bảng User
                object[] updateUser = new object[2];
                updateUser[0] = "SEMS_EBA_USER_UPDATE";
                //tao bang chua thong tin user
                DataTable tblUser = new DataTable();
                DataColumn colUserID = new DataColumn("colUserID");
                DataColumn colUFullName = new DataColumn("colUFullName");
                DataColumn colULocalName = new DataColumn("colULocalName");
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
                DataColumn colType = new DataColumn("colType");


                //add vào table
                tblUser.Columns.Add(colUserID);
                tblUser.Columns.Add(colUFullName);
                tblUser.Columns.Add(colULocalName);
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
                tblUser.Columns.Add(colType);

                //tao 1 dong du lieu
                DataRow row2 = tblUser.NewRow();
                row2["colUserID"] = userID;
                row2["colUFullName"] = UserFullName;
                row2["colULocalName"] = UserFullName;
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
                row2["colType"] = type;

                tblUser.Rows.Add(row2);

                //add vao phan tu thu 2 mang object
                updateUser[1] = tblUser;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSER, updateUser);
                #endregion

                #region update bảng user IB
                DataTable tblUserIB = new DataTable();
                DataColumn colIBUserID = new DataColumn("colIBUserID");
                DataColumn colUserName = new DataColumn("colUserName");
                DataColumn colPassword = new DataColumn("colPassword");
                DataColumn colStatus = new DataColumn("colStatus");
                DataColumn colUserModified = new DataColumn("colUserModified");
                DataColumn colLastModified = new DataColumn("colLastModified");
                DataColumn colIBPolicyusr = new DataColumn("colIBPolicyusr");
                tblUserIB.Columns.Add(colIBUserID);
                tblUserIB.Columns.Add(colUserName);
                tblUserIB.Columns.Add(colPassword);
                tblUserIB.Columns.Add(colStatus);
                tblUserIB.Columns.Add(colUserModified);
                tblUserIB.Columns.Add(colLastModified);
                tblUserIB.Columns.Add(colIBPolicyusr);


                if (IBUserName != "")
                {
                    DataRow rowUserIB = tblUserIB.NewRow();

                    rowUserIB["colIBUserID"] = userID;
                    rowUserIB["colUserName"] = IBUserName;
                    rowUserIB["colPassword"] = IBPassword;
                    rowUserIB["colStatus"] = IBStatus;
                    rowUserIB["colUserModified"] = userModify;
                    rowUserIB["colLastModified"] = dateModified;
                    rowUserIB["colIBPolicyusr"] = IBpolicyid;
                    tblUserIB.Rows.Add(rowUserIB);
                }

                object[] insertIbankUser = new object[2];
                insertIbankUser[0] = "SEMS_IBS_USERS_UPDATE";

                //add vao phan tu thu 2 mang object
                insertIbankUser[1] = tblUserIB;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSERIB, insertIbankUser);

                #endregion

                #region update bảng user SMS
                string pincode = SmartPortal.SEMS.O9Encryptpass.sha_sha256(pwdresetsms, SMSPhoneNo);// 
                DataTable tblUserSMS = new DataTable();

                DataColumn colSMSContractNo = new DataColumn("colSMSContractNo");
                DataColumn colSMSUserID = new DataColumn("colSMSUserID");
                DataColumn colSMSPhoneNo = new DataColumn("colSMSPhoneNo");
                DataColumn colSMSDefaultAcctno = new DataColumn("colSMSDefaultAcctno");
                DataColumn colSMSIsDefault = new DataColumn("colSMSIsDefault");
                DataColumn colSMSStatus = new DataColumn("colSMSStatus");
                DataColumn colSMSUserModify = new DataColumn("colSMSUserModify");
                DataColumn colSMSLastModify = new DataColumn("colSMSLastModify");
                DataColumn colSMSDefaultLang = new DataColumn("colSMSDefaultLang");
                DataColumn colSMSPolicyusr = new DataColumn("colSMSPolicyusr");
                DataColumn colSMSpincode = new DataColumn("colSMSpincode");
                DataColumn colSMSpwdreset = new DataColumn("colSMSpwdreset");



                tblUserSMS.Columns.Add(colSMSContractNo);
                tblUserSMS.Columns.Add(colSMSUserID);
                tblUserSMS.Columns.Add(colSMSPhoneNo);
                tblUserSMS.Columns.Add(colSMSDefaultAcctno);
                tblUserSMS.Columns.Add(colSMSIsDefault);
                tblUserSMS.Columns.Add(colSMSStatus);
                tblUserSMS.Columns.Add(colSMSUserModify);
                tblUserSMS.Columns.Add(colSMSLastModify);
                tblUserSMS.Columns.Add(colSMSDefaultLang);
                tblUserSMS.Columns.Add(colSMSPolicyusr);
                tblUserSMS.Columns.Add(colSMSpincode);
                tblUserSMS.Columns.Add(colSMSpwdreset);


                if (SMSPhoneNo != "" || SMSStatus.Equals(SmartPortal.Constant.IPC.PENDINGFORDELETE))
                {
                    DataRow rowUserSMS = tblUserSMS.NewRow();

                    rowUserSMS["colSMSContractNo"] = contractNo;
                    rowUserSMS["colSMSUserID"] = userID;
                    rowUserSMS["colSMSPhoneNo"] = SMSPhoneNo;
                    rowUserSMS["colSMSDefaultAcctno"] = SMSDefaultAcctno;
                    rowUserSMS["colSMSIsDefault"] = SMSIsDefault;
                    rowUserSMS["colSMSStatus"] = SMSStatus;
                    rowUserSMS["colSMSUserModify"] = userModify;
                    rowUserSMS["colSMSLastModify"] = dateModified;
                    rowUserSMS["colSMSDefaultLang"] = SMSDefaultLang;
                    rowUserSMS["colSMSPolicyusr"] = SMSpolicyid;
                    rowUserSMS["colSMSpincode"] = pincode;
                    rowUserSMS["colSMSpwdreset"] = pwdresetsms;

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
                DataColumn colMBUserName = new DataColumn("colMBUserName");
                DataColumn colMBPhoneNo = new DataColumn("colMBPhoneNo");
                DataColumn colMBLoginMethod = new DataColumn("colMBLoginMethod");
                DataColumn colMBAuthenType = new DataColumn("colMBAuthenType");
                DataColumn colMBPassword = new DataColumn("colMBPassword");
                DataColumn colMBStatus = new DataColumn("colMBStatus");
                DataColumn colMBPolicyusr = new DataColumn("colMBPolicyusr");
                DataColumn colMBContractType = new DataColumn("colMBContractType");

                tblUserMB.Columns.Add(colMBUserID);
                tblUserMB.Columns.Add(colMBUserName);
                tblUserMB.Columns.Add(colMBPhoneNo);
                tblUserMB.Columns.Add(colMBLoginMethod);
                tblUserMB.Columns.Add(colMBAuthenType);
                tblUserMB.Columns.Add(colMBPassword);
                tblUserMB.Columns.Add(colMBStatus);
                tblUserMB.Columns.Add(colMBPolicyusr);
                tblUserMB.Columns.Add(colMBContractType);
                if (MBPhoneNo != "")
                {
                    DataRow rowUserMB = tblUserMB.NewRow();
                    rowUserMB["colMBUserID"] = userID;
                    rowUserMB["colMBUserName"] = IBUserName;
                    rowUserMB["colMBPhoneNo"] = MBPhoneNo;
                    rowUserMB["colMBLoginMethod"] = "USERNAME";
                    rowUserMB["colMBAuthenType"] = "PASSWORD";
                    rowUserMB["colMBPassword"] = MBPassword;
                    rowUserMB["colMBStatus"] = MBStatus;
                    rowUserMB["colMBPolicyusr"] = MBpolicyid;
                    rowUserMB["colMBContractType"] = "IND";
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
                DataColumn colPHODefaultAcctno = new DataColumn("colPHODefaultAcctno");

                tblUserPHO.Columns.Add(colPHOUserID);
                tblUserPHO.Columns.Add(colPHOPhoneNo);
                tblUserPHO.Columns.Add(colPHOPassword);
                tblUserPHO.Columns.Add(colPHOStatus);
                tblUserPHO.Columns.Add(colPHODefaultAcctno);

                if (PHOPhoneNo != "")
                {
                    DataRow rowUserPHO = tblUserPHO.NewRow();
                    rowUserPHO["colPHOUserID"] = userID;
                    rowUserPHO["colPHOPhoneNo"] = PHOPhoneNo;
                    rowUserPHO["colPHOPassword"] = PHOPassword;
                    rowUserPHO["colPHOStatus"] = PHOStatus;
                    rowUserPHO["colPHODefaultAcctno"] = PHODefaultAcctno;

                    tblUserPHO.Rows.Add(rowUserPHO);
                }

                object[] insertPHOUser = new object[2];
                insertPHOUser[0] = "SEMS_PHO_USERS_UPDATE";

                //add vao phan tu thu 2 mang object
                insertPHOUser[1] = tblUserPHO;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSERPHO, insertPHOUser);

                #endregion

                #region delete dữ liệu cũ
                //remove dong rong
                DataTable tblDeleteFirst = new DataTable();
                DataColumn colDelUserName = new DataColumn("colDelUserName");
                DataColumn colDelUserID = new DataColumn("colDelUserID");
                DataColumn colDelMBPhoneNo = new DataColumn("colDelMBPhoneNo");
                DataColumn colDelPHOPhoneNo = new DataColumn("colDelPHOPhoneNo");

                tblDeleteFirst.Columns.AddRange(new[] { colDelUserName, colDelUserID, colDelMBPhoneNo, colDelPHOPhoneNo });

                DataRow rowDel = tblDeleteFirst.NewRow();
                rowDel["colDelUserName"] = IBUserName;
                rowDel["colDelUserID"] = userID;
                rowDel["colDelMBPhoneNo"] = MBPhoneNo;
                rowDel["colDelPHOPhoneNo"] = PHOPhoneNo;

                tblDeleteFirst.Rows.Add(rowDel);

                object[] deleteFirst = new object[2];
                deleteFirst[0] = "SEMS_EBA_USER_UPDATE_DELETEBYID";

                //add vao phan tu thu 2 mang object
                deleteFirst[1] = tblDeleteFirst;

                hasInput.Add(SmartPortal.Constant.IPC.DELETEFIRST, deleteFirst);
                #endregion

                #region update bang quyen user ibank
                //remove dong rong

                object[] updateIbankUserRight = new object[2];
                updateIbankUserRight[0] = "SEMS_EBA_USERINROLE_UPDATE";

                //add vao phan tu thu 2 mang object
                updateIbankUserRight[1] = IBUserRight;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEIBANKUSERRIGHT, updateIbankUserRight);
                #endregion

                #region update bang quyen user sms
                //remove dong rong

                object[] updateSMSUserRight = new object[2];
                updateSMSUserRight[0] = "SEMS_SMS_USERINROLE_UPDATE";

                //add vao phan tu thu 2 mang object
                updateSMSUserRight[1] = SMSUserRight;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATESMSUSERRIGHT, updateSMSUserRight);
                #endregion

                #region update bang quyen user MB
                //remove dong rong

                object[] updateMBUserRight = new object[2];
                updateMBUserRight[0] = "SEMS_MB_USERINROLE_UPDATE";

                //add vao phan tu thu 2 mang object
                updateMBUserRight[1] = MBUserRight;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEMBUSERRIGHT, updateMBUserRight);
                #endregion

                #region update bang quyen user PHO
                //remove dong rong

                object[] updatePHOUserRight = new object[2];
                updatePHOUserRight[0] = "SEMS_PHO_USERINROLE_UPDATE";

                //add vao phan tu thu 2 mang object
                updatePHOUserRight[1] = PHOUserRight;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEPHOUSERRIGHT, updatePHOUserRight);
                #endregion

                //LanNTH
                #region delete Group  user
                object[] deleteUserGroup = new object[2];
                deleteUserGroup[0] = "SEMS_EBA_USERGROUP_DELETE";

                //add vao phan tu thu 2 mang object
                DataTable dtDelUserGroup = new DataTable();
                DataColumn colDelUserGroup = new DataColumn("colUserID");
                dtDelUserGroup.Columns.Add(colDelUserGroup);

                DataRow r = dtDelUserGroup.NewRow();
                r["colUserID"] = userID;
                dtDelUserGroup.Rows.Add(r);

                deleteUserGroup[1] = dtDelUserGroup;

                hasInput.Add(SmartPortal.Constant.IPC.DELETEUSERGROUP, deleteUserGroup);
                #endregion

                #region Insert Group cho user
                object[] insertUserGroup = new object[2];
                insertUserGroup[0] = "SEMS_EBA_USERGROUP_INSERT";

                //add vao phan tu thu 2 mang object
                insertUserGroup[1] = UserGroup;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSERGROUP, insertUserGroup);
                #endregion


                #region update bang TranrightDetail
                //remove dong rong

                object[] updateTranrightDetail = new object[2];
                updateTranrightDetail[0] = "SEMS_EBA_TRANRIGHTDETAIL_UPDATE";

                //add vao phan tu thu 2 mang object
                updateTranrightDetail[1] = TranrightDetail;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATETRANRIGHTDETAIL, updateTranrightDetail);
                #endregion

                #region Insert Contract Account
                object[] insertContractAccount = new object[2];
                insertContractAccount[0] = "SEMS_EBA_CONTRACTACCOUNT_INSERT";

                //add vao phan tu thu 2 mang object
                insertContractAccount[1] = contractAccount;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACTACCOUNT, insertContractAccount);
                #endregion

                #region update bang User Account
                object[] updateUserAccount = new object[2];
                updateUserAccount[0] = "SEMS_EBA_USERACCOUNT_UPDATE";

                //add vao phan tu thu 2 mang object
                updateUserAccount[1] = userAccount;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSERACCOUNT, updateUserAccount);
                #endregion

                #region Insert sms notify detail
                object[] insertSMSNotifyDetails = new object[2];
                insertSMSNotifyDetails[0] = "EBA_SMSNOTIFYDETAILS_INSERT";

                //add vao phan tu thu 2 mang object
                insertSMSNotifyDetails[1] = SMSNotify;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTSMSNODETAILS, insertSMSNotifyDetails);
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

        #region Search User by condition
        public DataSet GetUserByCondition(string fullname, string usertype, string birthday, string email, string phone, string userlevel, string status, string IPCTrancode, string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, SmartPortal.Constant.IPC.SEMSUSERSEARCH);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin user theo condition");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.FULLNAME, fullname);
                hasInput.Add(SmartPortal.Constant.IPC.USERTYPE, usertype);
                hasInput.Add(SmartPortal.Constant.IPC.BIRTHDAY, birthday);
                hasInput.Add(SmartPortal.Constant.IPC.EMAIL, email);
                hasInput.Add(SmartPortal.Constant.IPC.PHONE, phone);
                hasInput.Add(SmartPortal.Constant.IPC.USERLEVEL, userlevel);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, IPCTrancode);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);

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

        #region APPROVE user by uid
        public DataSet ApproveUser(string userid, string status, string sUserApprover, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERAPPROVE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "duyệt user theo uid");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.USERAPPROVE, sUserApprover);


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

        #region Select USER BY DEPTID
        public DataSet GetUserByDeptID(string deptID, string contractno, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBLOADUSERBYDEPTID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin user theo phòng ban");

                hasInput.Add(SmartPortal.Constant.IPC.DEPTID, deptID);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
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

        #region RESET PASSWORD BY SERID & USERNAME (PHONENO)
        public DataSet ResetPassword(string serviceid, string username, string password, string passoldenc, string type, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERRESETPASS");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "reset pass theo serviceid và username");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceid);
                hasInput.Add(SmartPortal.Constant.IPC.USERNAME, username);
                hasInput.Add(SmartPortal.Constant.IPC.PASSWORD, password);
                hasInput.Add(SmartPortal.Constant.IPC.PASSRESET, passoldenc);
                hasInput.Add(SmartPortal.Constant.IPC.TYPE, type);

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


        #region Select user by usertype
        public DataSet GetUserByUserType(string usertype, string userid, string fullname, string email, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERGBUSERTYPE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy thông tin user by userid");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERTYPE, usertype);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add(SmartPortal.Constant.IPC.FULLNAME, fullname);
                hasInput.Add(SmartPortal.Constant.IPC.EMAIL, email);


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

        #region Lay danh sach nhom de thiet lap han muc
        public DataTable GetRoleTeller()
        {
            DataTable iRead;

            iRead = DataAccess.GetFromDataTable("SEMS_GETROLETELLER", null);

            return iRead;
        }
        #endregion

        #region serach serlevel
        public DataSet SearchUserlevel(string userlevel, string desc, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000029");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "search user level");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERLEVEL, userlevel);
                hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, desc);


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

        #region view details  corpuserlevel
        public DataSet DetailsUserlevel(string userlevel, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000030");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "details user leveL");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERLEVEL, userlevel);


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

        #region insert userlevel
        public DataSet AddUserLevel(string userlevel, string description, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000031");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "theêm corp user level");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERLEVEL, userlevel);
                hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, description);

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

        #region Update  userlevel
        public DataSet UpdateUserlevel(string userlevel, string desc, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000032");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "update user level");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERLEVEL, userlevel);
                hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, desc);


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

        #region delete view details  corpuserlevel
        public DataSet DeleteUserlevel(string userlevel, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000033");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "delete user level");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERLEVEL, userlevel);


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

        #region SELECT TYPE BY PARENTTYPE
        public DataSet LoadTypeByParentType(string Parenttype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00014");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load Type of usertype by parent");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.PARENTTYPE, Parenttype);


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

        #region DELETE TELLER BY USERNAEME
        public DataSet DeleteUserTeller(string uname, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00016");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "xoá user là teller");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERNAME, uname);


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

        #region mo khoa tai khoan
        public DataTable UnblockAccount(string serviceid, string username)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@serviceid";
                p1.Value = serviceid;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@username";
                p2.Value = username;
                p2.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("sems_unblockaccount", p1, p2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region lay thong tin user
        public DataTable GetUBID(string userid)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@UID";
                p1.Value = userid;
                p1.SqlDbType = SqlDbType.VarChar;


                return DataAccess.GetFromDataTable("SEMS_EBA_USERS_GETUBID", p1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region change system date
        public bool changesystemdate(string wYear, string wMonth, string wDay, string wHour, string wMinute, string wSecond, ref string errorCode, ref string errorDesc)
        {
            //vutran: kiem tra cac dich vu da dang ky va da duoc su dung cua user
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //object[] para = new object[] { date, time };
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCHANGESYSTEMDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                //hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.WYEAR, wYear);
                hasInput.Add(SmartPortal.Constant.IPC.WMONTH, wMonth);
                hasInput.Add(SmartPortal.Constant.IPC.WDAY, wDay);
                hasInput.Add(SmartPortal.Constant.IPC.WHOUR, wHour);
                hasInput.Add(SmartPortal.Constant.IPC.WMINUTE, wMinute);
                hasInput.Add(SmartPortal.Constant.IPC.WSECOND, wSecond);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    //ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    //bool result = bool.Parse(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.RESULT].ToString());
                    return true;
                    //return result;
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion
        public DataTable GETSMSUSERBYPHONENO(string phonno)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@phoneno";
                p1.Value = phonno;
                p1.SqlDbType = SqlDbType.VarChar;


                return DataAccess.GetFromDataTable("SEMS_EBA_USERS_GETUSER_BYPHONENO", p1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //LanNTH

        #region Get group for user (corp matrix)
        public DataSet GetGroupByUser(string userid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGROUPBYUSER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy group theo userid");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);


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

        #region Get detail Reset pass by usser
        public DataSet GetDetailResetPassByUser(string transid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSDETAILRESET");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, " Get Detail Reset pass by usser");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, transid);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                if (errorCode == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                }
                else
                {
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

        #region Get image Reset pass by usser
        public DataSet GetImageResetPassByUser(string transid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSIMAGERESET");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, " Get Detail Reset pass by usser");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, transid);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                if (errorCode == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                }
                else
                {
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
        #region Get UserID 
        public DataSet GetUserIDByServiceID(string username, string serviceid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETUSERID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, " Get Detail Reset pass by usser");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERNAME, username);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceid);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                if (errorCode == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                }
                else
                {
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
        #region Update status reset pass
        public void UpdateResetPassword(string tranid, string usermodify, string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUPDATERESETPASS");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, " Update status Reset pass by usser");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranid);
                hasInput.Add(SmartPortal.Constant.IPC.USERMODIFIED, usermodify);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);


                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Search User by condition
        public DataSet GetUserGroupByContractNo(string contractNo, string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETUSERGROUP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin user group theo contract");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);

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
        #region Get All User Send Mail Approve success CorpMatrix
        public DataTable GetListUserSendMailApproveSuccess(string sTranID)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@TRANID";
                p1.Value = sTranID;
                p1.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("SEMS_EBA_USERS_ALL_GETUSER_BYTRANID", p1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //Cash code

        public DataSet GetAllCashCodeByCondition(string userid, string ccyid, string phoneno, string accno, string datefrom, string dateto, string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCASHCODESEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tìm kiếm cash code ");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add("PHONENO", phoneno);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, accno);
                hasInput.Add(SmartPortal.Constant.IPC.DATEFROM, datefrom);
                hasInput.Add(SmartPortal.Constant.IPC.DATETO, dateto);
                //hasInput.Add(SmartPortal.Constant.IPC.EXPIREDATE, expirydate);
                //hasInput.Add(SmartPortal.Constant.IPC.CASHCODE, cashcode);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                //hasInput.Add(SmartPortal.Constant.IPC.TERMINALID, terminalid);
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

        public DataSet CashCodeViewDetails(string transID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCASHCODEVIEW");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tìm kiếm cash code by user");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.TRANSID, transID);
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

        public DataTable GetListUserSendMailBatch(string sTranID, string sUserID)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@UID";
                p1.Value = sUserID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@TRANID";
                p2.Value = sTranID;
                p2.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("SEMS_EBA_USERS_GETUBID_BATCHTRANSFER_V2", p1, p2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region telco
        public DataSet GetalltelcobyCondition(string TELCO, string SHORTNAME, string SUNSACCNO, string SUSPACCNO, string INCOMACCNO, string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSTELCOVIEW");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tìm kiếm Telecom ");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TELCO, TELCO);
                hasInput.Add(SmartPortal.Constant.IPC.SHORTNAME, SHORTNAME);
                hasInput.Add(SmartPortal.Constant.IPC.SUNSACCNO, SUNSACCNO);
                hasInput.Add(SmartPortal.Constant.IPC.SUSPACCNO, SUSPACCNO);
                hasInput.Add(SmartPortal.Constant.IPC.INCOMACCNO, INCOMACCNO);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);



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

        public DataSet GetalltelcoDetail(string CARDID, string SHORTNAME, string CARDAMOUNT, string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSTELCODETAIL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tìm kiếm Telecom detail");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CARDID, CARDID);
                hasInput.Add(SmartPortal.Constant.IPC.SHORTNAME, SHORTNAME);
                hasInput.Add(SmartPortal.Constant.IPC.CARDAMOUNT, CARDAMOUNT);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);

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
        public DataSet Updatetelco(string SHORTNAME, string SUNSACCNO, string SUSPACCNO, string INCOMACCNO, string status, string ETopup, string ELoadSundriesAccNo, string ELoadSuspendAccNo, string ELoadIncomAccNo, string DataPack, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSTELOUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Update Telco");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.SHORTNAME, SHORTNAME);
                hasInput.Add(SmartPortal.Constant.IPC.SUNDRIESACCN, SUNSACCNO);
                hasInput.Add(SmartPortal.Constant.IPC.SUSPENDACCNO, SUSPACCNO);
                hasInput.Add(SmartPortal.Constant.IPC.INCOMACCNO, INCOMACCNO);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.ETOPUP, ETopup);
                hasInput.Add(SmartPortal.Constant.IPC.ELOADSUNDRIESACCNO, ELoadSundriesAccNo);
                hasInput.Add(SmartPortal.Constant.IPC.ELOADSUSPENDACCNO, ELoadSuspendAccNo);
                hasInput.Add(SmartPortal.Constant.IPC.ELOADINCOMACCNO, ELoadIncomAccNo);
                hasInput.Add(SmartPortal.Constant.IPC.DATAPACK, DataPack);



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

        public DataSet Deletetelco(string TELCO, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSTELCODELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "DELETE Telco");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.TELCO, TELCO);

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


        public DataSet Createtelco(string TELCO, string SHORTNAME, string SUNSACCNO, string SUSPACCNO, string INCOMACCNO, string status, string ETopup, string ELoadSundriesAccNo, string ELoadSuspendAccNo, string ELoadIncomAccNo, string DataPack, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSTELCOCREATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "CREATE Telco");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                //hasInput.Add(SmartPortal.Constant.IPC.TELCOID, TELCOID);
                hasInput.Add(SmartPortal.Constant.IPC.TELCO, TELCO);
                hasInput.Add(SmartPortal.Constant.IPC.SHORTNAME, SHORTNAME);
                hasInput.Add(SmartPortal.Constant.IPC.SUNDRIESACCN, SUNSACCNO);
                hasInput.Add(SmartPortal.Constant.IPC.SUSPENDACCNO, SUSPACCNO);
                hasInput.Add(SmartPortal.Constant.IPC.INCOMACCNO, INCOMACCNO);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.ETOPUP, ETopup);
                hasInput.Add(SmartPortal.Constant.IPC.ELOADSUNDRIESACCNO, ELoadSundriesAccNo);
                hasInput.Add(SmartPortal.Constant.IPC.ELOADSUSPENDACCNO, ELoadSuspendAccNo);
                hasInput.Add(SmartPortal.Constant.IPC.ELOADINCOMACCNO, ELoadIncomAccNo);
                hasInput.Add(SmartPortal.Constant.IPC.DATAPACK, DataPack);




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

        public DataSet Getallcardtop(string CARDID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCARDTOPVIEW");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "get list card");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.CARDID, CARDID);

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


        public DataSet Updatecardtop(string CARDID, string SHORTNAME, string CARDAMOUNT, string REALAMOUNT, string CCYID, string status, string TYPE, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSEDITCARDTOP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Update Telco");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CARDID, CARDID);
                hasInput.Add(SmartPortal.Constant.IPC.SHORTNAME, SHORTNAME);
                hasInput.Add(SmartPortal.Constant.IPC.CARDAMOUNT, CARDAMOUNT);
                hasInput.Add(SmartPortal.Constant.IPC.REALAMOUNT, REALAMOUNT);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.TYPE, TYPE);



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

        public DataSet Deletecardtop(string CARDID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSDELCARDTOP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "delete card");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.CARDID, CARDID);

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

        public DataSet Addcardtop(string TELCOID, string SHORTNAME, decimal CARDAMOUNT, decimal REALMONEY, string CCYID, string status, string TYPE, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSADDCARDTOP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Update Telco");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TELCOID, TELCOID);
                hasInput.Add(SmartPortal.Constant.IPC.SHORTNAME, SHORTNAME);
                hasInput.Add(SmartPortal.Constant.IPC.CARDAMOUNT, CARDAMOUNT);
                hasInput.Add(SmartPortal.Constant.IPC.REALMONEY, REALMONEY);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.TYPE, TYPE);

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

        #region PrefixManagement

        public DataSet LoadTelcoID(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLOADTELCOID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load Telco ID");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");



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

        public DataSet GetAllPrefix(string SUPPLIERID, string PREFIX, string COUNTRYPREFIX, string GROUPID, string TELCOID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETALLPREFIX");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get All Prefix ");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SUPPLIERID, SUPPLIERID);
                hasInput.Add(SmartPortal.Constant.IPC.PREFIX, PREFIX);
                //hasInput.Add(SmartPortal.Constant.IPC.TELCO, TELCO);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTRYPREFIX, COUNTRYPREFIX);
                hasInput.Add(SmartPortal.Constant.IPC.GROUPID, GROUPID);
                hasInput.Add(SmartPortal.Constant.IPC.TELCOID, TELCOID);

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

        public DataSet AddPrefix(string SUPPLIERID, string PREFIX, string COUNTRYPREFIX, string COUNTRYNAME, string GROUPID, string TELCOID, string PHONELEN, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSADDPREFIX");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Add new Prefix ");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SUPPLIERID, SUPPLIERID);
                hasInput.Add(SmartPortal.Constant.IPC.PREFIX, PREFIX);
                //hasInput.Add(SmartPortal.Constant.IPC.TELCO, TELCO);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTRYPREFIX, COUNTRYPREFIX);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTRYNAME, COUNTRYNAME);
                hasInput.Add(SmartPortal.Constant.IPC.GROUPID, GROUPID);
                hasInput.Add(SmartPortal.Constant.IPC.TELCOID, TELCOID);
                hasInput.Add(SmartPortal.Constant.IPC.PHONELEN, PHONELEN);

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

        public DataSet ViewPrefix(string PREFIX, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSVIEWPREFIX");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "View Prefix ");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.PREFIX, PREFIX);


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

        public DataSet EditPrefix(string PREFIX, string SUPPLIERID, string COUNTRYPREFIX, string COUNTRYNAME, string GROUPID, string TELCOID, string PHONELEN, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSEDITPREFIX");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Edit Prefix ");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.PREFIX, PREFIX);
                hasInput.Add(SmartPortal.Constant.IPC.SUPPLIERID, SUPPLIERID);
                //hasInput.Add(SmartPortal.Constant.IPC.TELCO, TELCO);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTRYPREFIX, COUNTRYPREFIX);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTRYNAME, COUNTRYNAME);
                hasInput.Add(SmartPortal.Constant.IPC.GROUPID, GROUPID);
                hasInput.Add(SmartPortal.Constant.IPC.TELCOID, TELCOID);
                hasInput.Add(SmartPortal.Constant.IPC.PHONELEN, PHONELEN);


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

        public DataSet DeletePrefix(string PREFIX, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSDELETEPREFIX");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Delete Prefix ");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.PREFIX, PREFIX);


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
        public DataSet DeleteMultiCard(DataTable dtStructure, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "EBADELETEMULTICARD");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                object[] objs = new object[2];
                int i = 0;
                objs[i++] = "Deletetopcard";//Store Name
                objs[i++] = dtStructure;//Data Table
                hasInput.Add("DELETEMULTISTRUCTURE", objs);//Key Value
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DeleteMultiTelco(DataTable dtStructure, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "EBADELETEMULTITELCO");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                object[] objs = new object[2];
                int i = 0;
                objs[i++] = "DeleteTelco_TopbyID";//Store Name
                objs[i++] = dtStructure;//Data Table
                hasInput.Add("DELETEMULTISTRUCTURE", objs);//Key Value
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DeleteMultiPrefix(DataTable dtStructure, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "EBADELETEMULTIPREFIX");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                object[] objs = new object[2];
                int i = 0;
                objs[i++] = "DELETEPREFIX";//Store Name
                objs[i++] = dtStructure;//Data Table
                hasInput.Add("DELETEMULTISTRUCTURE", objs);//Key Value
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private DataSet CheckOutput(Hashtable hasOutput, ref string errorCode, ref string errorDesc)
        {
            DataSet ds = new DataSet();
            if (IsSuccess(hasOutput[IPC.IPCERRORCODE].ToString()))
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

        private bool IsSuccess(string errorcode)
        {
            return errorcode.Equals("0") ? true : false;
        }

        #region LanNTH - Get List Reset pass by usser
        public DataSet GetListResetPassByUser(string userid, string nric, string name, string serviceID, string usermodify, string resettype, string status, string fromdate, string todate, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLISTRESET");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, " Get List Reset pass by usser");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add("NRIC", nric);
                hasInput.Add(SmartPortal.Constant.IPC.NAME, name);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceID);
                hasInput.Add(SmartPortal.Constant.IPC.USERMODIFIED, usermodify);
                hasInput.Add(SmartPortal.Constant.IPC.TYPE, resettype);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.FROMDATE, fromdate);
                hasInput.Add(SmartPortal.Constant.IPC.TODATE, todate);
                hasInput.Add("RecPerPage", recPerPage);
                hasInput.Add("RecIndex", recIndex);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                if (errorCode == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                }
                else
                {
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




        public string CreatePrimaryKey(string phoneNo, string name, string type, ref string errorCode, ref string errorDesc)
        {
            try
            {
                string result = string.Empty;
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_REGISTERWL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Create Primary Key");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, phoneNo);
                hasInput.Add(SmartPortal.Constant.IPC.NAME, name);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);


                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    result = ds.Tables[0].Rows[0][type.Trim()].ToString();
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #region username generation 
        public string UsernameGeneration(string userSubType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = string.Empty;
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERNAMEGENER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Username Generation");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.USERTYPE, userSubType);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    result = ds.Tables[0].Rows[0]["RESULT"].ToString();
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Check UserName
        public DataSet CheckUserName(string storeName, object[] para, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000500");
                hasInput.Add("STORENAME", storeName);
                hasInput.Add("PARA", para);
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


        #region Check the phone number TELCO
        public string CheckPhoneTeLCo(string phoneNumber, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = string.Empty;
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CHECKPHONETELCO");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check Phone Telcol");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");



                hasInput.Add("PHONENO", phoneNumber);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    result = errorCode = "0";
                }
                else
                {
                    result = errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CheckPhoneNumber
        public string CheckPhoneNumberCustInfo(string PhoneNo, string ContractType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = "1";
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CHECKPHONECUSTINFO");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check phone Exsits custInfo");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add("PHONENO", PhoneNo);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTTYPE, ContractType);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    result = ds.Tables[0].Rows[0]["RESULT"].ToString();
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Check the phone number TELCO
        public string CheckPhoneInContract(string phoneNumber, string userid, string contractNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = string.Empty;
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CHECKPHONEEXISTCT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check Phone Telcol");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, phoneNumber);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        result = ds.Tables[0].Rows[0]["RESULT"].ToString();
                    }
                    errorCode = "0";
                }
                else
                {
                    result = errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public DataSet GETDetailsUser(string phoneno, string service, string loginmehtod, string authentype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GET_DETAILS_USER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, phoneno);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, service);
                hasInput.Add("LOGINMETHOD", loginmehtod);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);


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

        #region Check Phone Number NonWallet
        public string CheckPhoneNumberNonWallet(string PhoneNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = "1";
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CHECKPHONENONWALLET");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check phone Exsits");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add("PHONENO", PhoneNo);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    result = ds.Tables[0].Rows[0]["RESULT"].ToString();
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }

}
