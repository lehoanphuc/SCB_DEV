using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;

namespace SmartPortal.SEMS
{
    public class AgentMerchant
    {
        public DataSet InsertAgentMerchantContract(string walletID, string merchantCode, string userCreateed, string phoneContry, string kyc_id, string contractLevel, string custID, string custCode, string fullName, string shortName, string dob, string addr_resident, string township, string region, string addr_temp, string sex, string nation, string tel, string fax, string email, string licenseType, string licenseID, string issueDate, string issuePlace, string desc, string job, string office_addr, string office_phone, string cftype, string cfCode, string cType, string branchID, string custBranchID, string contractNo, string userType, string productID, string createDate, string endDate, string lastModify, string userCreate, string userLastModify, string userApprove, string status, string allAcct, string isSpecialMan, string isAutorenew, string typecontract, DataTable tblUser, DataTable tblIbankUser, DataTable tblSMSUser, DataTable tblMBUser, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable ContractRoleDetail, DataTable ContractAccount, DataTable TranrightDetail, DataTable UserAccount, DataTable DeptDefault, DataTable RoleDefault, DataTable SMSNotify, DataTable tblKYCInfor, DataTable tbltimeOpen,DataTable dtTranAlter, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "AGENTMERCHANTINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tạo khách hàng mới");
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);
                hasInput.Add(SmartPortal.Constant.IPC.USERAPPROVED, userCreateed);

                #region Insert bảng customer
                object[] insertcustinfo = new object[2];
                insertcustinfo[0] = "SEMS_EBA_CUSTINFO_INSERT";
                //tao bang chua thong tin customer
                DataTable tblCustInfo = new DataTable();
                DataColumn colCustID = new DataColumn("colCustID");
                DataColumn colCustCode = new DataColumn("colCustCode");
                DataColumn colFullName = new DataColumn("colFullName");
                DataColumn colShortName = new DataColumn("colShortName");
                DataColumn colDOB = new DataColumn("colDOB");
                DataColumn colAddr_Resident = new DataColumn("colAddr_Resident");
                DataColumn colAddrTemp = new DataColumn("colAddrTemp");
                DataColumn colSex = new DataColumn("colSex");
                DataColumn colNation = new DataColumn("colNation");
                DataColumn colTel = new DataColumn("colTel");
                DataColumn colFax = new DataColumn("colFax");
                DataColumn colEmail = new DataColumn("colEmail");
                DataColumn colLicenseType = new DataColumn("colLicenseType");
                DataColumn colLicenseID = new DataColumn("colLicenseID");
                DataColumn colIssueDate = new DataColumn("colIssueDate");
                DataColumn colIssuePlace = new DataColumn("colIssuePlace");
                DataColumn colDescription = new DataColumn("colDescription");
                DataColumn colJob = new DataColumn("colJob");
                DataColumn colOfficeAddr = new DataColumn("colOfficeAddr");
                DataColumn colOfficePhone = new DataColumn("colOfficePhone");
                DataColumn colCFType = new DataColumn("colCFType");
                DataColumn colBranchID = new DataColumn("colBranchID");
                DataColumn colCustStatus = new DataColumn("colCustStatus");
                DataColumn colCFCode = new DataColumn("colCFCode");
                DataColumn colcType = new DataColumn("colcType");
                DataColumn colcPhoneContryCode = new DataColumn("colcPhoneContryCode");
                DataColumn colKYCID = new DataColumn("colKYCID");
                DataColumn colUserCreated = new DataColumn("colUserCreated");
                DataColumn colTownship = new DataColumn("colTownship");
                DataColumn colRegion = new DataColumn("colRegion");

                //add vào table
                tblCustInfo.Columns.Add(colCustID);
                tblCustInfo.Columns.Add(colCustCode);
                tblCustInfo.Columns.Add(colFullName);
                tblCustInfo.Columns.Add(colShortName);
                tblCustInfo.Columns.Add(colDOB);
                tblCustInfo.Columns.Add(colAddr_Resident);
                tblCustInfo.Columns.Add(colAddrTemp);
                tblCustInfo.Columns.Add(colSex);
                tblCustInfo.Columns.Add(colNation);
                tblCustInfo.Columns.Add(colTel);
                tblCustInfo.Columns.Add(colFax);
                tblCustInfo.Columns.Add(colEmail);
                tblCustInfo.Columns.Add(colLicenseType);
                tblCustInfo.Columns.Add(colLicenseID);
                tblCustInfo.Columns.Add(colIssueDate);
                tblCustInfo.Columns.Add(colIssuePlace);
                tblCustInfo.Columns.Add(colDescription);
                tblCustInfo.Columns.Add(colJob);
                tblCustInfo.Columns.Add(colOfficeAddr);
                tblCustInfo.Columns.Add(colOfficePhone);
                tblCustInfo.Columns.Add(colCFType);
                tblCustInfo.Columns.Add(colBranchID);
                tblCustInfo.Columns.Add(colCustStatus);
                tblCustInfo.Columns.Add(colCFCode);
                tblCustInfo.Columns.Add(colcType);
                tblCustInfo.Columns.Add(colcPhoneContryCode);
                tblCustInfo.Columns.Add(colKYCID);
                tblCustInfo.Columns.Add(colUserCreated);
                tblCustInfo.Columns.Add(colTownship);
                tblCustInfo.Columns.Add(colRegion);

                //tao 1 dong du lieu
                if (custID != "")
                {
                    DataRow row = tblCustInfo.NewRow();
                    row["colCustID"] = custID;
                    row["colCustCode"] = custCode;
                    row["colFullName"] = fullName;
                    row["colShortName"] = shortName;
                    row["colDOB"] = dob;
                    row["colAddr_Resident"] = addr_resident;
                    row["colAddrTemp"] = addr_temp;
                    row["colSex"] = sex;
                    row["colNation"] = nation;
                    row["colTel"] = tel;
                    row["colFax"] = fax;
                    row["colEmail"] = email;
                    row["colLicenseType"] = licenseType;
                    row["colLicenseID"] = licenseID;
                    row["colIssueDate"] = issueDate;
                    row["colIssuePlace"] = issuePlace;
                    row["colDescription"] = desc;
                    row["colJob"] = job;
                    row["colOfficeAddr"] = office_addr;
                    row["colOfficePhone"] = office_phone;
                    row["colCFType"] = cftype;
                    row["colBranchID"] = custBranchID;
                    row["colCustStatus"] = SmartPortal.Constant.IPC.ACTIVE;
                    row["colCFCode"] = cfCode;
                    row["colcType"] = cType;
                    row["colcPhoneContryCode"] = "+95";
                    row["colKYCID"] = kyc_id;
                    row["colUserCreated"] = userCreateed;
                    row["colTownship"] = township;
                    row["colRegion"] = region;
                    tblCustInfo.Rows.Add(row);
                }

                //add vao phan tu thu 2 mang object
                insertcustinfo[1] = tblCustInfo;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCUSTINFO, insertcustinfo);
                #endregion

                #region Insert bảng contract
                object[] insertContract = new object[2];
                insertContract[0] = "SEMS_EBA_CONTRACT_AM_INSERT";
                //tao bang chua thong tin customer
                DataTable tblContract = new DataTable();
                DataColumn colContractNo = new DataColumn("colContractNo");
                DataColumn colConCustID = new DataColumn("colConCustID");
                DataColumn colUsertype = new DataColumn("colUsertype");
                DataColumn colProductID = new DataColumn("colProductID");
                DataColumn colConBranchID = new DataColumn("colConBranchID");
                DataColumn colCreateDate = new DataColumn("colCreateDate");
                DataColumn colEndDate = new DataColumn("colEndDate");
                DataColumn colLastModify = new DataColumn("colLastModify");
                DataColumn colUserCreate = new DataColumn("colUserCreate");
                DataColumn colUserLastModify = new DataColumn("colUserLastModify");
                DataColumn colUserApprove = new DataColumn("colUserApprove");
                DataColumn colStatus = new DataColumn("colStatus");
                DataColumn colAllAcct = new DataColumn("colAllAcct");
                DataColumn colIsSpecialMan = new DataColumn("colIsSpecialMan");
                DataColumn colIsAutorenew = new DataColumn("colIsAutorenew");
                DataColumn coltypecontract = new DataColumn("coltypecontract");
                DataColumn colContractLevel = new DataColumn("colContractLevel");
                DataColumn colMerchantCode = new DataColumn("colMerchantCode");
                //add vào table
                tblContract.Columns.Add(colContractNo);
                tblContract.Columns.Add(colConCustID);
                tblContract.Columns.Add(colUsertype);
                tblContract.Columns.Add(colProductID);
                tblContract.Columns.Add(colConBranchID);
                tblContract.Columns.Add(colCreateDate);
                tblContract.Columns.Add(colEndDate);
                tblContract.Columns.Add(colLastModify);
                tblContract.Columns.Add(colUserCreate);
                tblContract.Columns.Add(colUserLastModify);
                tblContract.Columns.Add(colUserApprove);
                tblContract.Columns.Add(colStatus);
                tblContract.Columns.Add(colAllAcct);
                tblContract.Columns.Add(colIsSpecialMan);
                tblContract.Columns.Add(colIsAutorenew);
                tblContract.Columns.Add(coltypecontract);
                tblContract.Columns.Add(colContractLevel);
                tblContract.Columns.Add(colMerchantCode);


                //tao 1 dong du lieu
                if (contractNo != "")
                {
                    DataRow row1 = tblContract.NewRow();
                    row1["colContractNo"] = contractNo;
                    row1["colConCustID"] = custID;
                    row1["colUsertype"] = userType;
                    row1["colProductID"] = productID;
                    row1["colConBranchID"] = branchID;
                    row1["colCreateDate"] = createDate;
                    row1["colEndDate"] = endDate;
                    row1["colLastModify"] = lastModify;
                    row1["colUserCreate"] = userCreate;
                    row1["colUserLastModify"] = userLastModify;
                    row1["colUserApprove"] = userApprove;
                    row1["colStatus"] = status;
                    row1["colAllAcct"] = allAcct;
                    row1["colIsSpecialMan"] = isSpecialMan;
                    row1["colIsAutorenew"] = isAutorenew;
                    row1["coltypecontract"] = typecontract;
                    row1["colContractLevel"] = contractLevel;
                    row1["colMerchantCode"] = merchantCode;
                    tblContract.Rows.Add(row1);
                }

                //add vao phan tu thu 2 mang object
                insertContract[1] = tblContract;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACT, insertContract);
                #endregion

                #region Insert bang kyc infor
                object[] insertKYCInfor = new object[2];
                insertKYCInfor[0] = "SEMS_EBA_KYCINFO_INSERT";
                //tao bang chua thong tin customer

                //add vao phan tu thu 2 mang object
                insertKYCInfor[1] = tblKYCInfor;

                hasInput.Add("INSERTKYCINFOR", insertKYCInfor);
                #endregion

                #region Insert bang time Open
                object[] timeOpen = new object[2];
                timeOpen[0] = "SEMS_TIME_OPEN_INSERT";
                timeOpen[1] = tbltimeOpen;

                hasInput.Add("INSERTTIMEOPEN", timeOpen);
                #endregion

                #region Tạo bảng chứa CONTRACT_POCKET_BY_WALLETID

                object[] insertContractPocket = new object[2];
                insertContractPocket[0] = "CREATE_CONTRACT_POCKET_BY_WALLETID";
                DataTable ContractPocketByWaller = new DataTable();
                DataColumn colContractPKNo = new DataColumn("colContractPKNo");
                DataColumn colWalletID = new DataColumn("colWalletID");
                DataColumn colCCYID = new DataColumn("colCCYID");
                //add vào table
                ContractPocketByWaller.Columns.Add(colContractPKNo);
                ContractPocketByWaller.Columns.Add(colWalletID);
                ContractPocketByWaller.Columns.Add(colCCYID);

                //tao 1 dong du lieu
                if (contractNo != "")
                {
                    DataRow row1 = ContractPocketByWaller.NewRow();
                    row1["colContractPKNo"] = contractNo;
                    row1["colWalletID"] = walletID;
                    row1["colCCYID"] = "LAK";

                    ContractPocketByWaller.Rows.Add(row1);
                }
                insertContractPocket[1] = ContractPocketByWaller;

                hasInput.Add("CONTRACTWALLET", insertContractPocket);
                #endregion

                #region Insert bảng User
                object[] insertUser = new object[2];
                insertUser[0] = "SEMS_EBA_USER_INSERT";

                //add vao phan tu thu 2 mang object
                insertUser[1] = tblUser;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSER, insertUser);
                #endregion

                #region Insert bảng user Ibank
                object[] insertIbankUser = new object[2];
                insertIbankUser[0] = "SEMS_IBS_USER_INSERT";
                //tao bang chua thong tin customer

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

                #region Insert bảng user SMS
                object[] insertSMSUser = new object[2];
                insertSMSUser[0] = "SEMS_SMS_USER_INSERT";
                //tao bang chua thong tin customer


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

                #region Insert bảng user Mobile
                object[] insertMBUser = new object[2];
                insertMBUser[0] = "SEMS_MB_USER_INSERT";
                //tao bang chua thong tin customer


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

                //LanNTH - add group for user
                #region Insert Group cho user
                object[] insertUserGroup = new object[2];
                insertUserGroup[0] = "SEMS_EBA_USERGROUP_INSERT";

                //add vao phan tu thu 2 mang object
                insertUserGroup[1] = new DataTable();

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSERGROUP, insertUserGroup);
                #endregion


                #region Insert quyền cho Contract
                object[] insertContractRoleDetail = new object[2];
                insertContractRoleDetail[0] = "SEMS_EBA_CONTRACTROLEDETAIL_INSERT";

                //add vao phan tu thu 2 mang object
                insertContractRoleDetail[1] = ContractRoleDetail;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACTROLEDETAIL, insertContractRoleDetail);
                #endregion

                #region Insert Contract Account
                object[] insertContractAccount = new object[2];
                insertContractAccount[0] = "SEMS_EBA_CONTRACTACCOUNTAM_INSERT";

                //add vao phan tu thu 2 mang object
                insertContractAccount[1] = ContractAccount;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACTACCOUNT, insertContractAccount);
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

                #region Insert dept default
                object[] insertDeptDefault = new object[2];
                insertDeptDefault[0] = "IB_EBA_DEPT_INSERT";

                //add vao phan tu thu 2 mang object
                insertDeptDefault[1] = DeptDefault;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTDEPTDEFAULT, insertDeptDefault);
                #endregion

                #region Insert role default
                object[] insertRoleDefault = new object[2];
                insertRoleDefault[0] = "IB_EBA_Roles_Insert_Default";

                //add vao phan tu thu 2 mang object
                insertRoleDefault[1] = RoleDefault;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTROLEDEFAULT, insertRoleDefault);
                #endregion

                #region Insert sms notify detail
                object[] insertSMSNotifyDetails = new object[2];
                insertSMSNotifyDetails[0] = "EBA_SMSNOTIFYDETAILS_INSERT";

                //add vao phan tu thu 2 mang object
                insertSMSNotifyDetails[1] = SMSNotify;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTSMSNODETAILS, insertSMSNotifyDetails);
                #endregion


                #region Insert transaction Alter
                object[] insertTranAlter = new object[2];
                insertTranAlter[0] = "EBA_TRANSACTIONALTER_INSERT";

                //add vao phan tu thu 2 mang object
                insertTranAlter[1] = dtTranAlter;

                hasInput.Add("INSERTTRANALTER", insertTranAlter);
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
        public DataSet LoadMerchantCode(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasOutput = new Hashtable();
                Hashtable hasInput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETMERCODE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "LOAD MERCHANT CODE");
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
            catch (Exception e)
            {
                throw (e);
            }
        }


        public DataSet AddAgentMerchant(string contractNo, string contractType, string localname, string UserNameMB, string lastModify, string userCreate, string userApprove, string status, string userID, string UserFullName, string userGender, string userAddress, string userEmail, string userPhone, string UcreateDate, string userModify, string userType, string userLevel, string type, string deptID, string tokenID, string tokenIssueDate, string smsOTP, string userBirthday, string MBPhoneNo, string MBPass, string MBStatus, DataTable MBUserRight, DataTable UserGroup, DataTable TranrightDetail, DataTable UserAccount, string MBpolicyid, string pwdreset, DataTable SMSNotify, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERAGENTINSERT");
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
                DataColumn colUSContractType = new DataColumn("colUSContractType");

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
                tblUser.Columns.Add(colUSContractType);

                //tao 1 dong du lieu
                DataRow row2 = tblUser.NewRow();
                row2["colUserID"] = userID;
                row2["colUContractNo"] = contractNo;
                row2["colULocalName"] = localname;
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
                row2["colUSContractType"] = contractType;

                tblUser.Rows.Add(row2);

                //add vao phan tu thu 2 mang object
                insertUser[1] = tblUser;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSER, insertUser);
                #endregion

                object[] insertMBUser = new object[2];
                insertMBUser[0] = "SEMS_MB_USER_INSERT";
                //tao bang chua thong tin customer

                #region Tạo bảng chứa User MB
                DataTable tblMBUser = new DataTable();
                DataColumn colMBUserID = new DataColumn("colMBUserID");
                DataColumn colMBUserName = new DataColumn("colMBUserName");
                DataColumn colMBPhoneNo = new DataColumn("colMBPhoneNo");
                DataColumn colMBLoginMethod = new DataColumn("colMBLoginMethod");
                DataColumn colMBAuthenType = new DataColumn("colMBAuthenType");
                DataColumn colMBNewPass = new DataColumn("colMBNewPass");
                DataColumn colMBNewPin = new DataColumn("colMBNewPin");
                DataColumn colMBPassU = new DataColumn("colMBPass");
                DataColumn colMBStatus = new DataColumn("colMBStatus");
                DataColumn colMBPinCode1 = new DataColumn("colMBPinCode1");
                DataColumn colMBPolicyusr = new DataColumn("colMBPolicyusr");
                DataColumn colpwdresetMB = new DataColumn("colpwdresetMB");
                DataColumn colPwdPinMB = new DataColumn("colPwdPinMB");
                DataColumn colContractType = new DataColumn("colContractType");

                //add vào table
                tblMBUser.Columns.Add(colMBUserID);
                tblMBUser.Columns.Add(colMBUserName);
                tblMBUser.Columns.Add(colMBPhoneNo);
                tblMBUser.Columns.Add(colMBLoginMethod);
                tblMBUser.Columns.Add(colMBAuthenType);
                tblMBUser.Columns.Add(colMBNewPass);
                tblMBUser.Columns.Add(colMBNewPin);
                tblMBUser.Columns.Add(colMBPassU);
                tblMBUser.Columns.Add(colMBStatus);
                tblMBUser.Columns.Add(colMBPinCode1);
                tblMBUser.Columns.Add(colMBPolicyusr);
                tblMBUser.Columns.Add(colpwdresetMB);
                tblMBUser.Columns.Add(colPwdPinMB);
                tblMBUser.Columns.Add(colContractType);

                DataRow row5 = tblMBUser.NewRow();
                row5["colMBUserID"] = userID;
                row5["colMBUserName"] = UserNameMB;
                row5["colMBPhoneNo"] = MBPhoneNo;
                row5["colMBLoginMethod"] = SmartPortal.Constant.IPC.USERNAME;
                row5["colMBAuthenType"] = "PASSWORD";
                row5["colMBNewPass"] = "Y";
                row5["colMBNewPin"] = "N";
                row5["colMBPass"] = MBPass;
                row5["colMBStatus"] = status;
                row5["colMBPinCode1"] = string.Empty;
                row5["colMBPolicyusr"] = MBpolicyid;
                row5["colpwdresetMB"] = pwdreset;
                row5["colPwdPinMB"] = string.Empty;
                row5["colContractType"] = contractType;


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

        public DataSet AddConsummer(string contractNo, string contractType, string localname, string UserNameMB, string lastModify, string userCreate, string userApprove, string status, string userID, string UserFullName, string userGender, string userAddress, string userEmail, string userPhone, string UcreateDate, string userModify, string userType, string userLevel, string type, string deptID, string tokenID, string tokenIssueDate, string smsOTP, string userBirthday, string MBPhoneNo, string MBPass, string MBStatus, DataTable MBUserRight, DataTable IBUserRight, DataTable UserGroup, DataTable TranrightDetail, DataTable UserAccount, string MBpolicyid, string IBpolicyid, string pwdreset,string usercreate, DataTable SMSNotify, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERCONSUMER");
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
                DataColumn colUSContractType = new DataColumn("colUSContractType");

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
                tblUser.Columns.Add(colUSContractType);

                //tao 1 dong du lieu
                DataRow row2 = tblUser.NewRow();
                row2["colUserID"] = userID;
                row2["colUContractNo"] = contractNo;
                row2["colULocalName"] = localname;
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
                row2["colUSContractType"] = contractType;

                tblUser.Rows.Add(row2);

                //add vao phan tu thu 2 mang object
                insertUser[1] = tblUser;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSER, insertUser);
                #endregion

                object[] insertMBUser = new object[2];
                insertMBUser[0] = "SEMS_MB_USER_INSERT";
                //tao bang chua thong tin customer

                #region Tạo bảng chứa User MB
                DataTable tblMBUser = new DataTable();
                DataColumn colMBUserID = new DataColumn("colMBUserID");
                DataColumn colMBUserName = new DataColumn("colMBUserName");
                DataColumn colMBPhoneNo = new DataColumn("colMBPhoneNo");
                DataColumn colMBLoginMethod = new DataColumn("colMBLoginMethod");
                DataColumn colMBAuthenType = new DataColumn("colMBAuthenType");
                DataColumn colMBNewPass = new DataColumn("colMBNewPass");
                DataColumn colMBNewPin = new DataColumn("colMBNewPin");
                DataColumn colMBPassU = new DataColumn("colMBPass");
                DataColumn colMBStatus = new DataColumn("colMBStatus");
                DataColumn colMBPinCode1 = new DataColumn("colMBPinCode1");
                DataColumn colMBPolicyusr = new DataColumn("colMBPolicyusr");
                DataColumn colpwdresetMB = new DataColumn("colpwdresetMB");
                DataColumn colPwdPinMB = new DataColumn("colPwdPinMB");
                DataColumn colContractType = new DataColumn("colContractType");

                //add vào table
                tblMBUser.Columns.Add(colMBUserID);
                tblMBUser.Columns.Add(colMBUserName);
                tblMBUser.Columns.Add(colMBPhoneNo);
                tblMBUser.Columns.Add(colMBLoginMethod);
                tblMBUser.Columns.Add(colMBAuthenType);
                tblMBUser.Columns.Add(colMBNewPass);
                tblMBUser.Columns.Add(colMBNewPin);
                tblMBUser.Columns.Add(colMBPassU);
                tblMBUser.Columns.Add(colMBStatus);
                tblMBUser.Columns.Add(colMBPinCode1);
                tblMBUser.Columns.Add(colMBPolicyusr);
                tblMBUser.Columns.Add(colpwdresetMB);
                tblMBUser.Columns.Add(colPwdPinMB);
                tblMBUser.Columns.Add(colContractType);

                DataRow row5 = tblMBUser.NewRow();
                row5["colMBUserID"] = userID;
                row5["colMBUserName"] = UserNameMB;
                row5["colMBPhoneNo"] = MBPhoneNo;
                row5["colMBLoginMethod"] = SmartPortal.Constant.IPC.USERNAME;
                row5["colMBAuthenType"] = "PASSWORD";
                row5["colMBNewPass"] = "Y";
                row5["colMBNewPin"] = "N";
                row5["colMBPass"] = MBPass;
                row5["colMBStatus"] = status;
                row5["colMBPinCode1"] = string.Empty;
                row5["colMBPolicyusr"] = MBpolicyid;
                row5["colpwdresetMB"] = pwdreset;
                row5["colPwdPinMB"] = string.Empty;
                row5["colContractType"] = contractType;


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

                object[] insertIBUser = new object[2];
                insertIBUser[0] = "SEMS_IBS_USER_INSERT";
                //tao bang chua thong tin customer

                #region Tạo bảng chứa User IB
                    DataTable tblIBUser = new DataTable();
                    DataColumn colIBUserName = new DataColumn("colIBUserName");
                    DataColumn colIBUserID = new DataColumn("colIBUserID");
                    DataColumn colIBPassU = new DataColumn("colIBPass");
                    DataColumn colIBlastlogin = new DataColumn("colIBlastlogin");
                    DataColumn colIBStatus = new DataColumn("colIBStatus");
                    DataColumn colIBUserCreate = new DataColumn("colIBUserCreate");
                    DataColumn colIBDateCreate = new DataColumn("colIBDateCreate");
                    DataColumn colIBUserModify = new DataColumn("colIBUserModify");
                    DataColumn colIBDateModify = new DataColumn("colIBDateModify");
                    DataColumn colIBUserApprove = new DataColumn("colIBUserApprove");
                    DataColumn colIBIsLogin = new DataColumn("colIBIsLogin");
                    DataColumn colIBDateExpire = new DataColumn("colIBDateExpire");
                    DataColumn colIBTimeExpire = new DataColumn("colIBTimeExpire");
                    DataColumn colIBPolicyusr = new DataColumn("colIBPolicyusr");
                    DataColumn colpwdresetIB = new DataColumn("colpwdresetIB");
                    DataColumn colIBLoginMethod = new DataColumn("colIBLoginMethod");
                    DataColumn colIBAuthenType = new DataColumn("colIBAuthenType");

                    //add vào table

                    tblIBUser.Columns.Add(colIBUserName);
                    tblIBUser.Columns.Add(colIBUserID);
                    tblIBUser.Columns.Add(colIBPassU);
                    tblIBUser.Columns.Add(colIBlastlogin);
                    tblIBUser.Columns.Add(colIBStatus);
                    tblIBUser.Columns.Add(colIBUserCreate);
                    tblIBUser.Columns.Add(colIBDateCreate);
                    tblIBUser.Columns.Add(colIBUserModify);
                    tblIBUser.Columns.Add(colIBDateModify);
                    tblIBUser.Columns.Add(colIBUserApprove);
                    tblIBUser.Columns.Add(colIBIsLogin);
                    tblIBUser.Columns.Add(colIBDateExpire);
                    tblIBUser.Columns.Add(colIBTimeExpire);
                    tblIBUser.Columns.Add(colIBPolicyusr);
                    tblIBUser.Columns.Add(colpwdresetIB);
                    tblIBUser.Columns.Add(colIBLoginMethod);
                    tblIBUser.Columns.Add(colIBAuthenType);

                    DataRow rowIB = tblIBUser.NewRow();
                    rowIB["colIBUserName"] = UserNameMB;
                    rowIB["colIBUserID"] = userID;
                    rowIB["colIBPass"] = MBPass;
                    rowIB["colIBlastlogin"] = string.Empty;
                    rowIB["colIBStatus"] = status;
                    rowIB["colIBUserCreate"] = usercreate;
                    rowIB["colIBDateCreate"] = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    rowIB["colIBUserModify"] = usercreate;
                    rowIB["colIBDateModify"] = string.Empty;
                    rowIB["colIBUserApprove"] = string.Empty;
                    rowIB["colIBIsLogin"] = "0";
                    rowIB["colIBDateExpire"] = DateTime.Now.AddYears(20).ToString("dd/MM/yyyy hh:mm:ss");
                    rowIB["colIBTimeExpire"] = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    rowIB["colIBPolicyusr"] = IBpolicyid;
                    rowIB["colpwdresetIB"] = pwdreset;
                    rowIB["colIBLoginMethod"] = SmartPortal.Constant.IPC.USERNAME;
                    rowIB["colIBAuthenType"] = "PASSWORD";


                    tblIBUser.Rows.Add(rowIB);

                    //add vao phan tu thu 2 mang object
                    insertIBUser[1] = tblIBUser;

                    hasInput.Add("INSERTIBUSER", insertIBUser);
                    #endregion

                #region Insert quyền user IB
                    object[] insertIBUserRight = new object[2];
                    insertIBUserRight[0] = "SEMS_IBS_USERINROLE_INSERT";

                    //add vao phan tu thu 2 mang object
                    insertIBUserRight[1] = IBUserRight;

                    hasInput.Add("INSERTIBUSERRIGHT", insertIBUserRight);
                    #endregion

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
    }
}
