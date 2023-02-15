using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using SmartPortal.DAL;
using System.Data.SqlClient;
using SmartPortal.Model;

namespace SmartPortal.SEMS
{
    public class Customer
    {
        #region Search customer by condition
        public DataSet GetCustomerByCondition(string custID, string fullName, string tel, string licenseID, string cfType, string status, string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCUSTSEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tìm kiếm thông tin khách hàng");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CUSTID, custID);
                hasInput.Add(SmartPortal.Constant.IPC.FULLNAME, fullName);
                hasInput.Add(SmartPortal.Constant.IPC.TEL, tel);
                hasInput.Add(SmartPortal.Constant.IPC.LICENSEID, licenseID);
                hasInput.Add(SmartPortal.Constant.IPC.CFTYPE, cfType);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
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

        #region Search customer by letter
        public DataSet GetCustomerByLetter(string letter, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCUSTSEARCHLETTER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tìm kiếm thông tin khách hàng theo ký tự");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.LETTER, letter);



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

        #region Search customer by custcode
        public DataSet GetCustomerByCustcode(string custCode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETCUSTBYCC");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin khách hàng theo custcode");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

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

        #region update customer
        public DataSet Update(string custID, string fullname, string shortname, string dob, string addr_resident, string addr_temp, string sex, string nation, string tel, string fax, string email, string licenseType, string licenseID, string issuedate, string issuePlace, string desc, string job, string officeAddr, string officePhone, string cfType, string branchID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCUSTUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Cập nhật thông tin khách hàng");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CUSTID, custID);
                hasInput.Add(SmartPortal.Constant.IPC.FULLNAME, fullname);
                hasInput.Add(SmartPortal.Constant.IPC.SHORTNAME, shortname);
                hasInput.Add(SmartPortal.Constant.IPC.DOB, dob);
                hasInput.Add(SmartPortal.Constant.IPC.ADDR_RESIDENT, addr_resident);
                hasInput.Add(SmartPortal.Constant.IPC.ADDR_TEMP, addr_temp);
                hasInput.Add(SmartPortal.Constant.IPC.SEX, sex);
                hasInput.Add(SmartPortal.Constant.IPC.NATION, nation);
                hasInput.Add(SmartPortal.Constant.IPC.TEL, tel);
                hasInput.Add(SmartPortal.Constant.IPC.FAX, fax);
                hasInput.Add(SmartPortal.Constant.IPC.EMAIL, email);
                hasInput.Add(SmartPortal.Constant.IPC.LICENSETYPE, licenseType);
                hasInput.Add(SmartPortal.Constant.IPC.LICENSEID, licenseID);
                hasInput.Add(SmartPortal.Constant.IPC.ISSUEDATE, issuedate);
                hasInput.Add(SmartPortal.Constant.IPC.ISSUEPLACE, issuePlace);
                hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, desc);
                hasInput.Add(SmartPortal.Constant.IPC.JOB, job);
                hasInput.Add(SmartPortal.Constant.IPC.OFFICEADDR, officeAddr);
                hasInput.Add(SmartPortal.Constant.IPC.OFFICEPHONE, officePhone);
                hasInput.Add(SmartPortal.Constant.IPC.CFTYPE, cfType);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchID);

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

        #region Insert customer
        public DataSet Insert(string custID, string fullName, string shortName, string dob, string addr_resident, string addr_temp, string sex, string nation, string tel, string fax, string email, string licenseType, string licenseID, string issueDate, string issuePlace, string desc, string job, string office_addr, string office_phone, string cftype, string branchID, string contractNo, string contractType, string productID, string createDate, string endDate, string lastModify, string userCreate, string userLastModify, string userApprove, string status, string allAcct, string isSpecialMan, string userID, string UserFullName, string userGender, string userAddress, string userEmail, string userPhone, string UcreateDate, string userModify, string userType, string userLevel, string deptID, string tokenID, string tokenIssueDate, string smsOTP, string UserBirthday, string IBUserName, string IBPassword, string SMSPhoneNo, string SMSDefaultAcctno, string MBPhoneNo, string MBPass, string PHOPhoneNo, string PHOPass, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable ContractRoleDetail, DataTable ContractAccount, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCUSTINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tạo khách hàng mới");
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);

                #region Insert bảng customer
                object[] insertcustinfo = new object[2];
                insertcustinfo[0] = "SEMS_EBA_CUSTINFO_INSERT";
                //tao bang chua thong tin customer
                DataTable tblCustInfo = new DataTable();
                DataColumn colCustID = new DataColumn("colCustID");
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



                //add vào table
                tblCustInfo.Columns.Add(colCustID);
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

                //tao 1 dong du lieu
                DataRow row = tblCustInfo.NewRow();
                row["colCustID"] = custID;
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
                row["colBranchID"] = branchID;
                row["colCustStatus"] = SmartPortal.Constant.IPC.ACTIVE;

                tblCustInfo.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                insertcustinfo[1] = tblCustInfo;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCUSTINFO, insertcustinfo);
                #endregion

                #region Insert bảng contract
                object[] insertContract = new object[2];
                insertContract[0] = "SEMS_EBA_CONTRACT_INSERT";
                //tao bang chua thong tin customer
                DataTable tblContract = new DataTable();
                DataColumn colContractNo = new DataColumn("colContractNo");
                DataColumn colConCustID = new DataColumn("colConCustID");
                DataColumn colContractType = new DataColumn("colContractType");
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


                //add vào table
                tblContract.Columns.Add(colContractNo);
                tblContract.Columns.Add(colConCustID);
                tblContract.Columns.Add(colContractType);
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


                //tao 1 dong du lieu
                DataRow row1 = tblContract.NewRow();
                row1["colContractNo"] = contractNo;
                row1["colConCustID"] = custID;
                row1["colContractType"] = contractType;
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


                tblContract.Rows.Add(row1);

                //add vao phan tu thu 2 mang object
                insertContract[1] = tblContract;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACT, insertContract);
                #endregion

                #region Insert bảng User
                object[] insertUser = new object[2];
                insertUser[0] = "SEMS_EBA_USER_INSERT";
                //tao bang chua thong tin user
                DataTable tblUser = new DataTable();
                DataColumn colUserID = new DataColumn("colUserID");
                DataColumn colUContractNo = new DataColumn("colUContractNo");
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

                //add vào table
                tblUser.Columns.Add(colUserID);
                tblUser.Columns.Add(colUContractNo);
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


                //tao 1 dong du lieu
                DataRow row2 = tblUser.NewRow();
                row2["colUserID"] = userID;
                row2["colUContractNo"] = contractNo;
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
                row2["colSMSBirthday"] = UserBirthday;

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

                    //tao 1 dong du lieu
                    DataRow row3 = tblIbankUser.NewRow();
                    row3["colUserName"] = IBUserName;
                    row3["colIBUserID"] = userID;
                    row3["colIBPassword"] = IBPassword;
                    row3["colLastLoginTime"] = UcreateDate;
                    row3["colIBStatus"] = status;
                    row3["colIBUserCreate"] = userCreate;
                    row3["colIBDateCreate"] = UcreateDate;
                    row3["colIBUserModify"] = userCreate;
                    row3["colIBLastModify"] = lastModify;
                    row3["colIBUserApprove"] = userApprove;
                    row3["colIBIsLogin"] = "Y";
                    row3["colIBDateExpire"] = endDate;
                    row3["colIBExpireTime"] = createDate;


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


                    //add vào table
                    tblSMSUser.Columns.Add(colSMSUserID);
                    tblSMSUser.Columns.Add(colSMSPhoneNo);
                    tblSMSUser.Columns.Add(colSMSContractNo);
                    tblSMSUser.Columns.Add(colSMSIsBroadcast);
                    tblSMSUser.Columns.Add(colSMSDefaultAcctno);
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

                    //tao 1 dong du lieu
                    DataRow row4 = tblSMSUser.NewRow();
                    row4["colSMSUserID"] = userID;
                    row4["colSMSPhoneNo"] = SMSPhoneNo;
                    row4["colSMSContractNo"] = contractNo;
                    row4["colSMSIsBroadcast"] = "N";
                    row4["colSMSDefaultAcctno"] = SMSDefaultAcctno;
                    row4["colSMSIsDefault"] = "Y";
                    row4["colSMSPinCode"] = "";
                    row4["colSMSStatus"] = status;
                    row4["colSMSPhoneType"] = "";
                    row4["colSMSUserCreate"] = userCreate;
                    row4["colSMSUserModify"] = userModify;
                    row4["colSMSUserApprove"] = userApprove;
                    row4["colSMSLastModify"] = lastModify;
                    row4["colSMSDateCreate"] = UcreateDate;
                    row4["colSMSDateExpire"] = endDate;


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
                    DataColumn colMBPhoneNo = new DataColumn("colMBPhoneNo");
                    DataColumn colMBPass = new DataColumn("colMBPass");


                    //add vào table
                    tblMBUser.Columns.Add(colMBUserID);
                    tblMBUser.Columns.Add(colMBPhoneNo);
                    tblMBUser.Columns.Add(colMBPass);


                    //tao 1 dong du lieu
                    DataRow row5 = tblMBUser.NewRow();
                    row5["colMBUserID"] = userID;
                    row5["colMBPhoneNo"] = MBPhoneNo;
                    row5["colMBPass"] = MBPass;


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


                    //add vào table
                    tblPHOUser.Columns.Add(colPHOUserID);
                    tblPHOUser.Columns.Add(colPHOPhoneNo);
                    tblPHOUser.Columns.Add(colPHOPass);


                    //tao 1 dong du lieu
                    DataRow row6 = tblPHOUser.NewRow();
                    row6["colPHOUserID"] = userID;
                    row6["colPHOPhoneNo"] = PHOPhoneNo;
                    row6["colPHOPass"] = PHOPass;


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

                #region Insert quyền cho Contract
                object[] insertContractRoleDetail = new object[2];
                insertContractRoleDetail[0] = "SEMS_EBA_CONTRACTROLEDETAIL_INSERT";

                //add vao phan tu thu 2 mang object
                insertContractRoleDetail[1] = ContractRoleDetail;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACTROLEDETAIL, insertContractRoleDetail);
                #endregion

                #region Insert Contract Account
                object[] insertContractAccount = new object[2];
                insertContractAccount[0] = "SEMS_EBA_CONTRACTACCOUNT_INSERT";

                //add vao phan tu thu 2 mang object
                insertContractAccount[1] = ContractAccount;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACTACCOUNT, insertContractAccount);
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

        #region Insert customer corp
        //public DataSet InsertCorp(string custID,string custCode, string fullName, string shortName, string dob, string addr_resident, string addr_temp, string sex, string nation, string tel, string fax, string email, string licenseType, string licenseID, string issueDate, string issuePlace, string desc, string job, string office_addr, string office_phone, string cftype, string cfCode, string branchID, string custBranchID, string contractNo, string contractType, string productID, string createDate, string endDate, string lastModify, string userCreate, string userLastModify, string userApprove, string status, string allAcct, string isSpecialMan, string NeedToApprove, DataTable tblUser,DataTable tblIbankUser,DataTable tblSMSUser,DataTable tblMBUser,DataTable tblPHOUser, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable ContractRoleDetail, DataTable ContractAccount,DataTable TranrightDetail,DataTable UserAccount,DataTable DeptDefault,DataTable RoleDefault, ref string errorCode, ref string errorDesc)
        //cho corp advance
        public DataSet InsertCorp(string phoneWL, string userCreateed, string kyc_id, string contractLevel, string custID, string custCode, string fullName, string shortName, string dob, string addr_resident, string township, string region, string addr_temp, string sex, string nation, string tel, string fax, string email, string licenseType, string licenseID, string issueDate, string issuePlace, string desc, string job, string office_addr, string office_phone, string cftype, string cfCode, string cType, string branchID, string custBranchID, string contractNo, string userType, string productID, string createDate, string endDate, string lastModify, string userCreate, string userLastModify, string userApprove, string status, string allAcct, string isSpecialMan, string isAutorenew, string typecontract, DataTable tblUser, DataTable tblIbankUser, DataTable tblSMSUser, DataTable tblMBUser, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable ContractRoleDetail, DataTable ContractAccount, DataTable TranrightDetail, DataTable UserAccount, DataTable DeptDefault, DataTable RoleDefault, ref string errorCode, ref string errorDesc)
        {

            return InsertCorp(phoneWL, userCreateed, kyc_id, contractLevel, custID, custCode, fullName, shortName, dob, addr_resident, addr_temp, township, region, sex, nation, tel, fax, email, licenseType, licenseID, issueDate, issuePlace, desc, job, office_addr, office_phone, cftype, cfCode, cType, branchID, custBranchID, contractNo, userType, productID, createDate, endDate, lastModify, userCreate, userLastModify, userApprove, status, allAcct, isSpecialMan, isAutorenew, typecontract, tblUser, tblIbankUser, tblSMSUser, tblMBUser, IBUserRight, SMSUserRight, MBUserRight, ContractRoleDetail, ContractAccount, TranrightDetail, UserAccount, DeptDefault, RoleDefault, new DataTable(), new DataTable(), new DataTable(), ref errorCode, ref errorDesc);
        }
        public DataSet InsertCorp(string phoneWL, string userCreateed, string kyc_id, string contractLevel, string custID, string custCode, string fullName, string shortName, string dob, string addr_resident, string township, string region, string addr_temp, string sex, string nation, string tel, string fax, string email, string licenseType, string licenseID, string issueDate, string issuePlace, string desc, string job, string office_addr, string office_phone, string cftype, string cfCode, string cType, string branchID, string custBranchID, string contractNo, string userType, string productID, string createDate, string endDate, string lastModify, string userCreate, string userLastModify, string userApprove, string status, string allAcct, string isSpecialMan, string isAutorenew, string typecontract, DataTable tblUser, DataTable tblIbankUser, DataTable tblSMSUser, DataTable tblMBUser, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable ContractRoleDetail, DataTable ContractAccount, DataTable TranrightDetail, DataTable UserAccount, DataTable DeptDefault, DataTable RoleDefault, DataTable SMSNotify, DataTable tblKYCInfor, DataTable tbLAlter, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCUSTCORPINSERT");
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
                insertContract[0] = "SEMS_EBA_CONTRACT_INSERT";
                //tao bang chua thong tin customer
                DataTable tblContract = new DataTable();
                DataColumn colContractNo = new DataColumn("colContractNo");
                DataColumn colConCustID = new DataColumn("colConCustID");
                DataColumn colContractType = new DataColumn("colContractType");
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
                //add vào table
                tblContract.Columns.Add(colContractNo);
                tblContract.Columns.Add(colConCustID);
                tblContract.Columns.Add(colContractType);
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

                //tao 1 dong du lieu
                if (contractNo != "")
                {
                    DataRow row1 = tblContract.NewRow();
                    row1["colContractNo"] = contractNo;
                    row1["colConCustID"] = custID;
                    row1["colContractType"] = userType;
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
                    row1["colWalletID"] = phoneWL;
                    row1["colCCYID"] = "LAK";

                    ContractPocketByWaller.Rows.Add(row1);
                }
                insertContractPocket[1] = ContractPocketByWaller;

                hasInput.Add("INSERTPOCKET", insertContractPocket);
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
                insertContractAccount[0] = "SEMS_EBA_CONTRACTACCOUNT_INSERT";

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
                insertTranAlter[1] = tbLAlter;

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
        #endregion

        #region delete customer by cuistID
        public DataSet DeleteUserByUID(string custid, string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCUSTOMERDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Xóa khách hàng");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CUSTID, custid);
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

        #region Check Customer Exists

        public DataTable CheckCustExists(string custCode, string custType)
        {
            return CheckCustExists(custCode, custType, string.Empty);
        }
        public DataTable CheckCustExists(string custCode, string custType, string contractType)
        {

            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@custCode";
            p1.Value = custCode;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@custType";
            p2.Value = custType;
            p2.SqlDbType = SqlDbType.Text;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@contractType";
            p3.Value = contractType;
            p3.SqlDbType = SqlDbType.Text;


            iRead = DataAccess.GetFromDataTable("SEMS_EBA_CUSTINFO_CHECKEXISTS", p1, p2, p3);


            return iRead;
        }
        #endregion

        #region dong bo thong tin khach hang
        public DataTable DongBoKH(string custID, string custName, string birth, string mobi, string email, string residentadd, string license, string issuedate, string issueplace, string officeadd, string sex)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@custID";
            p1.Value = custID;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@custName";
            p2.Value = custName;
            p2.SqlDbType = SqlDbType.Text;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@birth";
            p3.Value = birth;
            p3.SqlDbType = SqlDbType.Text;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@mobi";
            p4.Value = mobi;
            p4.SqlDbType = SqlDbType.Text;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@email";
            p5.Value = email;
            p5.SqlDbType = SqlDbType.Text;

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@residentadd";
            p6.Value = residentadd;
            p6.SqlDbType = SqlDbType.Text;

            SqlParameter p7 = new SqlParameter();
            p7.ParameterName = "@license";
            p7.Value = license;
            p7.SqlDbType = SqlDbType.Text;

            SqlParameter p8 = new SqlParameter();
            p8.ParameterName = "@issuedate";
            p8.Value = issuedate;
            p8.SqlDbType = SqlDbType.Text;

            SqlParameter p9 = new SqlParameter();
            p9.ParameterName = "@issueplace";
            p9.Value = issueplace;
            p9.SqlDbType = SqlDbType.Text;

            SqlParameter p10 = new SqlParameter();
            p10.ParameterName = "@officeaddr";
            p10.Value = officeadd;
            p10.SqlDbType = SqlDbType.Text;

            SqlParameter p11 = new SqlParameter();
            p11.ParameterName = "@sex";
            p11.Value = sex;
            p11.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("eba_DongBoKH", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);


            return iRead;
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
        //minh add 13/5/2015
        #region Check phone number for account default SMS
        public DataTable CheckPhoneNumber(string phoneNo, string defaultAcc)
        {
            DataTable phoneExist = new DataTable();

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@phoneNo";
            p1.Value = phoneNo;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@defaultAcc";
            p2.Value = defaultAcc;
            p2.SqlDbType = SqlDbType.Text;

            phoneExist = DataAccess.GetFromDataTable("EBA_Users_CheckPhoneNumber", p1, p2);


            return phoneExist;
        }
        #endregion

        //LanNTH - Insert corp matrix
        #region Insert customer corp Matrix       
        public DataSet InsertCorpMaTrix(ContractModel model,
            DataTable tblUser, DataTable tblIbankUser, DataTable tblSMSUser, DataTable tblMBUser, DataTable tblPHOUser, DataTable IBUserRight,
            DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable tblUserGroup, DataTable ContractRoleDetail, DataTable ContractAccount,
            DataTable TranrightDetail, DataTable UserAccount, DataTable DeptDefault, DataTable RoleDefault, DataTable SMSNotify, DataTable tblTranAlter, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CORPMATRIXINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tạo khách hàng mới");
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, model.contractNo);

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
                if (model.cusID != "")
                {
                    DataRow row = tblCustInfo.NewRow();
                    row["colCustID"] = model.cusID;
                    row["colCustCode"] = model.cusCode;
                    row["colFullName"] = model.fullName;
                    row["colShortName"] = model.shortName;
                    row["colDOB"] = model.dob;
                    row["colAddr_Resident"] = model.addrResident;
                    row["colAddrTemp"] = model.addrTemp;
                    row["colSex"] = model.gender;
                    row["colNation"] = model.nation;
                    row["colTel"] = model.phone;
                    row["colFax"] = model.fax;
                    row["colEmail"] = model.email;
                    row["colLicenseType"] = model.licenseType;
                    row["colLicenseID"] = model.licenseId;
                    row["colIssueDate"] = model.issueDate;
                    row["colIssuePlace"] = model.issuePlace;
                    row["colDescription"] = model.desc;
                    row["colJob"] = model.job;
                    row["colOfficeAddr"] = model.officeAddr;
                    row["colOfficePhone"] = model.officePhone;
                    row["colCFType"] = model.cusType;
                    row["colBranchID"] = model.custBranchId;
                    row["colCustStatus"] = SmartPortal.Constant.IPC.ACTIVE;
                    row["colCFCode"] = model.cusCode;

                    row["colcType"] = "B";
                    row["colcPhoneContryCode"] = "+95";
                    row["colKYCID"] = "1";
                    row["colUserCreated"] = model.userCreate;
                    row["colTownship"] = model.townshipCust;
                    row["colRegion"] = model.regionCust;

                    tblCustInfo.Rows.Add(row);
                }

                //add vao phan tu thu 2 mang object
                insertcustinfo[1] = tblCustInfo;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCUSTINFO, insertcustinfo);
                #endregion

                #region Insert bảng contract
                object[] insertContract = new object[2];
                insertContract[0] = "SEMS_EBA_CONTRACT_INSERT";
                //tao bang chua thong tin customer
                DataTable tblContract = new DataTable();
                DataColumn colContractNo = new DataColumn("colContractNo");
                DataColumn colConCustID = new DataColumn("colConCustID");
                DataColumn colContractType = new DataColumn("colContractType");
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

                //add vào table
                tblContract.Columns.Add(colContractNo);
                tblContract.Columns.Add(colConCustID);
                tblContract.Columns.Add(colContractType);
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
                //thaity modify at 14/7/2014
                //tblContract.Columns.Add(colNeedToApproveTrans);



                //tao 1 dong du lieu
                if (model.contractNo != "")
                {
                    DataRow row1 = tblContract.NewRow();
                    row1["colContractNo"] = model.contractNo;
                    row1["colConCustID"] = model.cusID;
                    row1["colContractType"] = model.contractType;
                    row1["colProductID"] = model.productId;
                    row1["colConBranchID"] = model.branchId;
                    row1["colCreateDate"] = model.createDate;
                    row1["colEndDate"] = model.endDate;
                    row1["colLastModify"] = model.lastModify;
                    row1["colUserCreate"] = model.userCreate;
                    row1["colUserLastModify"] = model.userLastModify;
                    row1["colUserApprove"] = model.userApprove;
                    row1["colStatus"] = model.status;
                    row1["colAllAcct"] = model.allAcct;
                    row1["colIsSpecialMan"] = model.isSpecialMan;
                    row1["colIsAutorenew"] = model.isAutorenew;
                    row1["coltypecontract"] = model.corpType;
                    row1["colContractLevel"] = "1";
                    //row1["colNeedToApproveTrans"] = NeedToApprove;
                    //thaity modify at 14/7/2014

                    tblContract.Rows.Add(row1);
                }

                //add vao phan tu thu 2 mang object
                insertContract[1] = tblContract;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACT, insertContract);
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
                if (model.contractNo != "" && model.walletID != null)
                {
                    DataRow row1 = ContractPocketByWaller.NewRow();
                    row1["colContractPKNo"] = model.contractNo;
                    row1["colWalletID"] = model.walletID;
                    row1["colCCYID"] = "LAK";

                    ContractPocketByWaller.Rows.Add(row1);
                }
                insertContractPocket[1] = ContractPocketByWaller;

                hasInput.Add("INSERTPOCKET", insertContractPocket);
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

                #region Insert bảng user Phone
                object[] insertPHOUser = new object[2];
                insertPHOUser[0] = "SEMS_PHO_USER_INSERT";
                //tao bang chua thong tin customer


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

                #region Insert Group cho user
                object[] insertUserGroup = new object[2];
                insertUserGroup[0] = "SEMS_EBA_USERGROUP_INSERT";

                //add vao phan tu thu 2 mang object
                insertUserGroup[1] = tblUserGroup;

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
                insertContractAccount[0] = "SEMS_EBA_CONTRACTACCOUNT_INSERT";

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
                insertTranAlter[1] = tblTranAlter;

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
        #endregion

        #region Insert User group
        public DataSet InsertUserGroup(DataTable tblUserGroup, DataTable tblDelUserGroup, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERGROUPINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Insert user group");


                #region Insert Group cho user
                object[] insertUserGroup = new object[2];
                insertUserGroup[0] = "SEMS_EBA_USERGROUP_INSERT";

                //add vao phan tu thu 2 mang object
                insertUserGroup[1] = tblUserGroup;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSERGROUP, insertUserGroup);
                #endregion

                #region Insert Group cho user
                object[] deleteUserGroup = new object[2];
                deleteUserGroup[0] = "SEMS_EBA_USERGROUP_DELETE";

                //add vao phan tu thu 2 mang object
                deleteUserGroup[1] = tblDelUserGroup;

                hasInput.Add(SmartPortal.Constant.IPC.DELETEUSERGROUP, deleteUserGroup);
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

        #region LAY THONG TIN USER
        public DataSet GetCustomerInforByPhoneNO(string PhoneNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETINFOBYPHONE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin userCustInfo");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add("PHONENO", PhoneNo);

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

        #region Check Agent
        public string CheckPhoneNumberAgent(string PhoneNo, string contractno, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = "1";
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CHECKPHONEAGENT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check phone Exsits custInfo");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add("PHONENO", PhoneNo);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);

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

        #region Get Contract by Phone
        public string GetContractByPhone(string PhoneNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = "1";
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETCONTRACTBYPHONE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get Contract by Phone");
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

        #region GetAccountWallet
        public string GetAccountWallet(string contractNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = string.Empty;
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETACCWALLET");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check Account Wallet by contract");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        result = ds.Tables[0].Rows[0]["ACCWALLET"].ToString();
                    }
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


        #region GetAccountWallet
        public DataSet GetWalletID(string contractNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = string.Empty;
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETWALLETIDCONTRACT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check Account Wallet by contract");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
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

        #region Check the phone number TELCO
        public string GetAccountWalletID(string contractNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = string.Empty;
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETWALLETID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check Wallet ID");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");



                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);

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


        #region Check the phone number 
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

        #region Check Phone CustID
        public string CheckPhoneCustID(string phoneNumber, string custID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = string.Empty;
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CHECKPHONECUSTID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check Phone CustID");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add("PHONENO", phoneNumber);
                hasInput.Add("CUSTID", custID);

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

        #region Get User ID By CustID
        public string GetUserIDByCustID(string custID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = string.Empty;
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETUSERIDACCWALLET");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get User ID By CustID");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.CUSTID, custID);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        result = ds.Tables[0].Rows[0]["USERID"].ToString();
                    }
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

        #region LAY THONG TIN KHACH HANG TU CORE BOI CUSTCODE VA CUSTTYPE
        public Hashtable GetCustInfo(string custcode, string custType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCUSTOMERINFO");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin khách hàng trong core bank");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODE, custcode);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTTYPE, custType);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);


                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region LAY THONG TIN TAI KHOAN TU CORE BOI CUSTCODE VA CUSTTYPE
        public DataSet GetAcctNo(string custcode, string custType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETCUSTACCINFO");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin tài khoản trong core bank");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODE, custcode);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTTYPE, custType);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();
                //var strExpr = "ACCOUNTYPE IN(DD,CD)";

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Columns.Contains("TYPEID"))
                        {
                            dt.Columns["TYPEID"].ColumnName = "ACCOUNTTYPE";
                        }
                        if (dt.Columns.Contains("BRID"))
                        {
                            dt.Columns["BRID"].ColumnName = "BRANCHID";
                        }
                        if (dt.Columns.Contains("STATUSCD"))
                        {
                            dt.Columns["STATUSCD"].ColumnName = "STATUS";
                        }
                        ds.Tables.RemoveAt(0);
                        ds.Tables.Add(dt);
                        //ds.Tables[0].DefaultView.RowFilter = strExpr;

                    }
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

        #region LAY THONG TIN KHACH HANG TU CORE BOI SO CHUNG MINH/MST
        public Hashtable GetCustInfoByLicense(string license, string custType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS000001");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin khách hàng trong core bank theo số chứng minh");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ID, license);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTTYPE, custType);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);


                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CHECK CUST_INFO
        public string CheckCustInfo(string custcode, string phoneNo, string CustType, string contractType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = "1";
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CHECKCUSTINFO");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check  custInfo");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODE, custcode);
                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, phoneNo);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTTYPE, CustType);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTTYPE, contractType);

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
        #region CheckPaperNumer
        public string CheckPaperNumer(string PaperNo, string Type, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = "1";
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CHECKPAPERNOEXISTS");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check  paper No");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("PAPERNO", PaperNo);
                hasInput.Add("TYPEKYC", Type);

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

        #region Load KYC Type
        public DataTable LoadKYCType(string type, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLOADKYCTYPE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "LOAD KYC TYPE");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.TYPE, type);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataTable dt = new DataTable();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    DataSet ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                    dt = ds.Tables[0];
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

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

        #region UPDATE CONTRACT by contractNo
        public DataSet LinkBankAccount(string contract, DataTable dtContractAccount, DataTable dtCustCode, DataTable dtUser, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLINKBACKACCOUNT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Link bank Account");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contract);

                #region Update table Customer
                object[] updateCustomer = new object[2];
                updateCustomer[0] = "SEMS_EBA_CUSTINFO_LINKBANK";

                //add vao phan tu thu 2 mang object
                updateCustomer[1] = dtCustCode;

                hasInput.Add("UPDATECUSTINFO", updateCustomer);
                #endregion

                #region Update table ContractAccount
                object[] insertContractAccount = new object[2];
                insertContractAccount[0] = "SEMS_EBA_CONTRACTACCOUNT_INSERT";

                //add vao phan tu thu 2 mang object
                insertContractAccount[1] = dtContractAccount;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACTACCOUNT, insertContractAccount);
                #endregion

                #region Update table User
                object[] updateUsers = new object[2];
                updateUsers[0] = "SEMS_USER_LINK_BANK";

                //add vao phan tu thu 2 mang object
                updateUsers[1] = dtUser;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSER, updateUsers);
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

        //Register Wallet onlly
        #region Get Details Request
        public DataSet GetDetailReqRegister(string transid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_GETDETAILREQ");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get Details Request");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.TRANID, transid);

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

        #region Get Details Account Request
        public DataSet GetDetailAccRegister(string transid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_GETACCNOREQ");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get Details Request");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add(SmartPortal.Constant.IPC.TRANID, transid);

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


        #region Approve request register
        public Hashtable ApproveRegister(Hashtable infor, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_APPROVEREGISTER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Approve register");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                foreach (DictionaryEntry item in infor)
                {
                    hasInput.Add(item.Key, item.Value);
                }

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update request register
        public Hashtable UpdateRegister(Hashtable infor, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_UPDATEREGISTER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Update register");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                foreach (DictionaryEntry item in infor)
                {
                    hasInput.Add(item.Key, item.Value);
                }

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region Update request register
        public Hashtable SyncFromCore(string custID, string custName, string birth, string mobi, string email, string residentadd, string licenseid, string licensetype, string issuedate, string issueplace, string officeadd, string sex, ref string errorCode, ref string errorDesc)
        {
            try
            {

                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_SYNCFROMCORE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Synchronize from Optimal9");
                hasInput.Add(SmartPortal.Constant.IPC.CUSTID, custID);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTNAME, custName);
                hasInput.Add(SmartPortal.Constant.IPC.BIRTHDAY, birth);
                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, mobi);
                hasInput.Add(SmartPortal.Constant.IPC.EMAIL, email);
                hasInput.Add(SmartPortal.Constant.IPC.ADDRESS, residentadd);
                hasInput.Add(SmartPortal.Constant.IPC.LICENSEID, licenseid);
                hasInput.Add(SmartPortal.Constant.IPC.LICENSETYPE, licensetype);
                hasInput.Add(SmartPortal.Constant.IPC.ISSUEDATE, issuedate);
                hasInput.Add(SmartPortal.Constant.IPC.ISSUEPLACE, issueplace);
                hasInput.Add(SmartPortal.Constant.IPC.OFFICEADDR, officeadd);
                hasInput.Add(SmartPortal.Constant.IPC.SEX, sex);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
