using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using SmartPortal.DAL;
using System.IO;
using System.Web;
using SmartPortal.Model;

namespace SmartPortal.SEMS
{
    public class Contract
    {

        public DataTable GETACCOUNTQR(string contractno)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@CONTRACTNO";
                p1.Value = contractno;
                p1.SqlDbType = SqlDbType.Text;

                iRead = DataAccess.GetFromDataTable("SEMS_GETACCOUNTQR", p1);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GETUSERACCOUNTDEFAULT(string USERID)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@USERID";
                p1.Value = USERID;
                p1.SqlDbType = SqlDbType.Text;



                iRead = DataAccess.GetFromDataTable("SEMS_GETUSERACCOUNTDEFAULT", p1);


                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #region them hub_cms
        public void AddHubCmsUser(DataTable CMS, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSHUBCMSADDUSER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Thêm cms User");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                #region them bang danh sach OTP
                object[] insertCMS = new object[2];
                insertCMS[0] = "SEMS_EBA_HUBCMS_ADDUSER";

                //add vao phan tu thu 2 mang object
                insertCMS[1] = CMS;

                hasInput.Add(SmartPortal.Constant.IPC.CMSUSER, insertCMS);
                #endregion

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


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void HubCms_Edit(string ID, string Address, string Phone, string Email, string UserID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                object[] para = new object[5];
                para[0] = ID;
                para[1] = Address;
                para[2] = Phone;
                para[3] = Email;
                para[4] = UserID;

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000222");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add("STORENAME", "SEMS_EBA_HUBCMS_EDIT");
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void HubCms_Delete(string ID, string UserID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                object[] para = new object[2];
                para[0] = ID;
                para[1] = UserID;

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000222");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add("STORENAME", "SEMS_EBA_HUBCMS_DETELE");
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable HubCms_Search(string CorpID, string CorpName, string ContractNo, string CustName, string Status, string ID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@CORPID";
                p1.Value = CorpID;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@CORPNAME";
                p2.Value = CorpName;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@CONTRACTNO";
                p3.Value = ContractNo;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@CUSTNAME";
                p4.Value = CustName;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@STATUS";
                p5.Value = Status;
                p5.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@ID";
                p6.Value = ID;
                p6.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("SEMS_EBA_HUBCMS_SEARCH", p1, p2, p3, p4, p5, p6);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SELECT CONTRACT BY CUSTCODE
        public DataSet GetContractByCustcode(string custCode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCONTRACTGBCC");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin hợp đồng theo custcode");
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

        #region Search contract by condition
        public DataSet GetContractByCondition(string contractno, string custname, string usercreate, string createdate, string enddate, string contracttype, string status, string licenseid, string custcode, string PhoneNo, string ProductType, ref string errorCode, ref string errorDesc)
        {
            return GetContractByCondition(contractno, custname, usercreate, createdate, enddate, contracttype, status, licenseid, custcode, PhoneNo, ProductType, 15, 0, ref errorCode, ref errorDesc);
        }
        public DataSet GetContractByCondition(string contractno, string custname, string usercreate, string createdate, string enddate, string contracttype, string status, string licenseid, string custcode, string PhoneNo, string ProductType, int MaxiMumRows, int StartRowIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCONTRACTSEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tìm kiếm hợp đồng Ebanking");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.FULLNAME, custname);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATE, usercreate);
                hasInput.Add(SmartPortal.Constant.IPC.CREATEDATE, createdate);
                hasInput.Add(SmartPortal.Constant.IPC.ENDDATE, enddate);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTTYPE, contracttype);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.LICENSEID, licenseid);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODE, custcode);
                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, PhoneNo);
                hasInput.Add("PRODUCTTYPE", ProductType);

                hasInput.Add(SmartPortal.Constant.IPC.MAXIMUMROWS, MaxiMumRows);
                hasInput.Add(SmartPortal.Constant.IPC.STARTROWINDEX, StartRowIndex);


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

        #region LOAD contract for approve
        public DataSet LoadContractForApprove(string contractno, string custname, string usercreate, string createdate, string enddate, string contracttype, string status, string BranchID, int MaxiMumRows, int StartRowIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLOADCONTRACTAPP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load thông tin hợp đồng cho approve");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.FULLNAME, custname);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATE, usercreate);
                hasInput.Add(SmartPortal.Constant.IPC.CREATEDATE, createdate);
                hasInput.Add(SmartPortal.Constant.IPC.ENDDATE, enddate);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTTYPE, contracttype);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, BranchID);
                hasInput.Add(SmartPortal.Constant.IPC.MAXIMUMROWS, MaxiMumRows);
                hasInput.Add(SmartPortal.Constant.IPC.STARTROWINDEX, StartRowIndex);

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

        #region SELECT CONTRACT BY CONTRACT_NO (VIEW DETAILS CONTRACT)
        public DataSet GetContractByContractNo(string contractno, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCONTRACTDETAILS");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin hợp đồng theo contracno");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

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
        #region SELECT TRANSACTION ALTER OF CONTRACT
        public DataSet GetTransactionAlterOfContract(string contractno, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCONTRACTALTER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin hợp đồng theo contracno");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

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
        #region Select User info of contract by contractNo
        public DataSet GetUserByContractNo(string contractno, string userType, string userLevel, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSEROFCONTRACT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin user theo hợp đồng");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.USERLEVEL, userLevel);

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.USERTYPE, userType);

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

        #region DELETE CONTRACT by contractNo
        public DataSet DeleteContract(string contractno, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCONTRACTDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "xóa hợp đồng then contractno");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

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

        #region GET customer INFO  by CONTRACT NO
        public DataSet GetCustomerByContractNo(string contractno, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETCUSTBYCN");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin khách hàng theo contractno");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

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

        #region APPROVE CONTRACT by contractNo
        public DataSet ApproveContract(string contractno, string status, string userApprove, string flag, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCONTRACTAPPROVE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "duyệt hợp đồng theo contractno");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.USERAPPROVE, userApprove);
                hasInput.Add(SmartPortal.Constant.IPC.FLAG, flag);


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

        #region INSERT CONTRACT
        public DataSet Insert(string branchID, string custID, string contractNo, string contractType, string productID, string createDate, string endDate, string lastModify, string userCreate, string userLastModify, string userApprove, string status, string allAcct, string isSpecialMan, string userID, string UserFullName, string userGender, string userAddress, string userEmail, string userPhone, string UcreateDate, string userModify, string userType, string userLevel, string deptID, string tokenID, string tokenIssueDate, string smsOTP, string userBirthday, string IBUserName, string IBPassword, string SMSPhoneNo, string SMSDefaultAcctno, string MBPhoneNo, string MBPass, string PHOPhoneNo, string PHOPass, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable ContractRoleDetail, DataTable ContractAccount, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCONTRACTINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tạo hợp đồng mới");
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);

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
                row2["colSMSBirthday"] = userBirthday;

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

        #region INSERT CONTRACT CORP
        //danh cho corp advance
        public DataSet InsertCorp(string branchID, string custID, string contractNo, string contractType, string productID, string createDate, string endDate, string lastModify, string userCreate, string userLastModify, string userApprove, string status, string allAcct, string isSpecialMan, string isAutorenew, string typecontract, DataTable tblUser, DataTable tblIbankUser, DataTable tblSMSUser, DataTable tblMBUser, DataTable tblPHOUser, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable ContractRoleDetail, DataTable ContractAccount, DataTable TranrightDetail, DataTable UserAccount, DataTable DeptDefault, DataTable RoleDefault, ref string errorCode, ref string errorDesc)
        {
            return InsertCorp(branchID, custID, contractNo, contractType, productID, createDate, endDate, lastModify, userCreate, userLastModify, userApprove, status, allAcct, isSpecialMan, isAutorenew, typecontract, tblUser, tblIbankUser, tblSMSUser, tblMBUser, tblPHOUser, IBUserRight, SMSUserRight, MBUserRight, PHOUserRight, ContractRoleDetail, ContractAccount, TranrightDetail, UserAccount, DeptDefault, RoleDefault, ref errorCode, ref errorDesc);
        }
        public DataSet InsertCorp(string branchID, string custID, string contractNo, string contractType, string productID, string createDate, string endDate, string lastModify, string userCreate, string userLastModify, string userApprove, string status, string allAcct, string isSpecialMan, string isAutorenew, string typecontract, DataTable tblUser, DataTable tblIbankUser, DataTable tblSMSUser, DataTable tblMBUser, DataTable tblPHOUser, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable ContractRoleDetail, DataTable ContractAccount, DataTable TranrightDetail, DataTable UserAccount, DataTable DeptDefault, DataTable RoleDefault, DataTable SMSNotify, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCONTRACTCOIN");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tạo hợp đồng mới");
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);

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
                row1["colIsAutorenew"] = isAutorenew;
                row1["coltypecontract"] = typecontract;

                tblContract.Rows.Add(row1);

                //add vao phan tu thu 2 mang object
                insertContract[1] = tblContract;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACT, insertContract);
                #endregion

                #region Insert bảng User
                object[] insertUser = new object[2];
                insertUser[0] = "SEMS_EBA_USER_INSERT";
                //tao bang chua thong tin user                

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

        #region INSERT CONTRACT EXISTS CUSTOMER
        //danh cho corp advance
        public DataSet InsertCustomerExists(string walletID, string branchID, string contactLevel, string Amount, string userID, string PhoneMB, string custID, string contractNo, string userType, string productID, string createDate, string endDate, string lastModify, string userCreate, string userLastModify, string userApprove, string status, string allAcct, string isSpecialMan, string isAutorenew, string typecontract, DataTable tblUser, DataTable tblIbankUser, DataTable tblSMSUser, DataTable tblMBUser, DataTable tblPHOUser, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable ContractRoleDetail, DataTable ContractAccount, DataTable TranrightDetail, DataTable UserAccount, DataTable DeptDefault, DataTable RoleDefault, ref string errorCode, ref string errorDesc)
        {
            return InsertCustomerExists(walletID, branchID, contactLevel, Amount, userID, custID, PhoneMB, contractNo, userType, productID, createDate, endDate, lastModify, userCreate, userLastModify, userApprove, status, allAcct, isSpecialMan, isAutorenew, typecontract, tblUser, tblIbankUser, tblSMSUser, tblMBUser, tblPHOUser, IBUserRight, SMSUserRight, MBUserRight, PHOUserRight, ContractRoleDetail, ContractAccount, TranrightDetail, UserAccount, DeptDefault, RoleDefault, new DataTable(), new DataTable(), ref errorCode, ref errorDesc);
        }
        public DataSet InsertCustomerExists(string walletID, string branchID, string contactLevel, string Amount, string userID, string PhoneMB, string custID, string contractNo, string userType, string productID, string createDate, string endDate, string lastModify, string userCreate, string userLastModify, string userApprove, string status, string allAcct, string isSpecialMan, string isAutorenew, string typecontract, DataTable tblUser, DataTable tblIbankUser, DataTable tblSMSUser, DataTable tblMBUser, DataTable tblPHOUser, DataTable IBUserRight, DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable ContractRoleDetail, DataTable ContractAccount, DataTable TranrightDetail, DataTable UserAccount, DataTable DeptDefault, DataTable RoleDefault, DataTable tblKYCInfor, DataTable SMSNotify, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCONTRACTCUSTOMER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tạo hợp đồng mới");
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATED, userCreate);

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
                row1["colContractLevel"] = contactLevel;

                tblContract.Rows.Add(row1);

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
                if (contractNo != "")
                {
                    DataRow rowWaller = ContractPocketByWaller.NewRow();
                    rowWaller["colContractPKNo"] = contractNo;
                    rowWaller["colWalletID"] = walletID;
                    rowWaller["colCCYID"] = "LAK";

                    ContractPocketByWaller.Rows.Add(rowWaller);
                }
                insertContractPocket[1] = ContractPocketByWaller;

                hasInput.Add("INSERTPOCKET", insertContractPocket);
                #endregion


                #region Insert bang kyc infor
                object[] insertKYCInfor = new object[2];
                insertKYCInfor[0] = "SEMS_EBA_KYCINFO_INSERT";
                //tao bang chua thong tin customer

                //add vao phan tu thu 2 mang object
                insertKYCInfor[1] = tblKYCInfor;

                hasInput.Add("INSERTKYCINFOR", insertKYCInfor);
                #endregion

                #region Insert bảng User
                object[] insertUser = new object[2];
                insertUser[0] = "SEMS_EBA_USER_INSERT_CUSTOMER";
                //tao bang chua thong tin user                

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
                insertSMSUser[0] = "SEMS_SMS_USER_INSERT_CUSTOMER_EXITS";
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
                insertMBUser[0] = "SEMS_MB_USER_INSERT_CUSTOMER_EXISTS";
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

        #region UPDATE CONTRACT by contractNo
        public DataSet UpdateContract(string contractno, string status, string opendate, string enddate, string isReceiverList, string isAutorenew, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCONTRACTUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "sửa hợp đồng theo contractno");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.CREATEDATE, opendate);
                hasInput.Add(SmartPortal.Constant.IPC.ENDDATE, enddate);
                hasInput.Add("ISRECEIVERLIST", isReceiverList);
                hasInput.Add("ISAUTORENEW", isAutorenew);


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

        #region UPDATE PRODUCT
        public DataSet UpdateProduct(string contractno, string newproduct, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "UPDATEPRODUCT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "CAP NHAT SAN PHAM CHO HOP DONG");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.NEWPRODUCT, newproduct);


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



        #region UPDATE PRODUCT
        public DataSet UpdateLevelContract(string contractno, string newLevel, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "UPDATELEVELCONTRACT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "CAP NHAT LEVEL CHO HOP DONG");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.NEWLEVEL, newLevel);


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

        #region UPDATE PRODUCT CORP
        public DataSet UpdateProductCorp(string contractno, string newproduct, string usercreate, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "UPDATEPRODUCTCORP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "CAP NHAT SAN PHAM CHO HOP DONG DOANH NGHIEP");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.NEWPRODUCT, newproduct);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATE, usercreate);


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

        #region Load loại hợp đồng
        public DataTable LoadContractType(string custType, string isCustomer)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@custType";
            p1.Value = custType;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@isCustomer";
            p2.Value = isCustomer;
            p2.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("SEMS_EBA_USERTYPE_LOADCONTRACTTYPE", p1, p2);


            return iRead;
        }
        #endregion

        #region lay tat ca quyen hop dong
        public DataTable GetRoleByServiceAndContract(string serviceID, string contractNo, string roleType)
        {

            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@SERVICEID";
            p1.Value = serviceID;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@CONTRACTNO";
            p2.Value = contractNo;
            p2.SqlDbType = SqlDbType.Text;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@ROLETYPE";
            p3.Value = roleType;
            p3.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("SEMS_GETROLEBYSERVICEANDCONTRACT", p1, p2, p3);


            return iRead;

        }
        #endregion
        #region lay tat ca quyen productid
        public DataTable GetRoleByServiceAndProductid(string serviceID, string productID, string roleType)
        {

            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@SERVICEID";
            p1.Value = serviceID;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@PRODUCTID";
            p2.Value = productID;
            p2.SqlDbType = SqlDbType.Text;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@ROLETYPE";
            p3.Value = roleType;
            p3.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("SEMS_GET_ROLEBYSERVICEANDPRODUCT", p1, p2, p3);


            return iRead;

        }
        #endregion

        #region lay tat ca quyen productid
        public DataTable CheckRoleInsertMB(string serviceID, string productID, string roleType)
        {

            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@SERVICEID";
            p1.Value = serviceID;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@PRODUCTID";
            p2.Value = productID;
            p2.SqlDbType = SqlDbType.Text;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@ROLETYPE";
            p3.Value = roleType;
            p3.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("SEMS_CheckRoleInsertMB", p1, p2, p3);


            return iRead;

        }
        #endregion

        #region luu ban cung hop dong
        public void SaveContractReview(string contractNo, string content, string path)
        {

            // create a writer and open the file
            TextWriter tw = new StreamWriter(path, false);

            // write a line of text to the file
            tw.Write(content);


            // close the stream
            tw.Close();
        }
        #endregion

        #region lay hop dong da dang ky
        public string OldContract(string contractNo, string path)
        {
            string str = "";
            TextReader tr = new StreamReader(path);
            str = tr.ReadToEnd();
            tr.Close();

            return str;
        }
        #endregion

        #region Load tất cả ngân hàng khác
        public DataTable LoadContractLimit_His(string FROMDATE, string TODATE, string IPCTRANCODEHIS, string USER, string CONTRACTNO)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@FROMDATE";
            p1.Value = FROMDATE;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@TODATE";
            p2.Value = TODATE;
            p2.SqlDbType = SqlDbType.Text;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@IPCTRANCODEHIS";
            p3.Value = IPCTRANCODEHIS;
            p3.SqlDbType = SqlDbType.Text;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@USER";
            p4.Value = USER;
            p4.SqlDbType = SqlDbType.Text;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@CONTRACTNO";
            p5.Value = CONTRACTNO;
            p5.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("SEMS_EBA_SPCUSTLIMITHIS_SEARCH", p1, p2, p3, p4, p5);


            return iRead;
        }
        #endregion

        #region

        public DataSet UpdateStatusContract(string contractno, string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                object[] para = new object[] { contractno, status };
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000113");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add("STORENAME", "SEMS_EBA_CONTRACT_UPDATESTATUS");
                hasInput.Add("PARA", para);


                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
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
        #region

        public DataSet UpdateStatusForContractBlock(string contractno, string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CONTRACTUNBLOCK");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
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

        #region

        public DataSet RejectPendingDelete(string contractno, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                object[] para = new object[] { contractno };
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000113");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add("STORENAME", "SEMS_EBA_CONTRACT_REJECTPENDINGDELETE");
                hasInput.Add("PARA", para);


                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
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

        public int DeleteSMSPhone(string PhoneNo, string Status, string UserModify, string LastModify)
        {
            try
            {
                int strErr = 0;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@PHONENO";
                p.Value = PhoneNo;
                p.SqlDbType = SqlDbType.VarChar;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@STATUS";
                p1.Value = Status;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@USERMODIFY";
                p2.Value = UserModify;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@LASTMODIFY";
                p3.Value = LastModify;
                p3.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("SEMS_SMS_USER_UPDATE_STATUS", p, p1, p2, p3);
                if (strErr == 0)
                {
                    //throw new Exception();
                    return strErr;
                }
                else
                {
                    return strErr;
                }
            }
            //catch (BusinessExeption bex)
            //{
            //    throw bex;
            //}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void sendSMS_Contract(string PhoneNo, string RESETINFO, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SMS00027");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDSMS);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, SmartPortal.Constant.IPC.SOURCEIDSMS);
                hasInput.Add(SmartPortal.Constant.IPC.RESETINFO, RESETINFO);
                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, PhoneNo);


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


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region getAllProduct on setpermissionforallcontract page
        public DataSet getAllProduct(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS0420");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "GET ALL PRODUCT");
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
        #endregion

        #region getAllTransactionByService on setpermissionforallcontract page
        public DataSet getAllTransactionByService(string service, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS0421");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "GET ALL TRANSACTION BY SERVICE");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SERVICE, service);



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
        #region setPermissionforAllAcount on setpermissionforallcontract page
        public DataSet updateNewServiceAllContract(string productID, string serviceID, string trancode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS0422");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "SET PERMISSION FOR ALL CONTRACT");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, productID);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);

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

        #region sendSMS_RESETPASS
        public static void sendSMS_RESETPASS(string PhoneNo, string RESETINFO, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SMS00028");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDSMS);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, SmartPortal.Constant.IPC.SOURCEIDSMS);
                hasInput.Add(SmartPortal.Constant.IPC.RESETINFO, RESETINFO);
                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, PhoneNo);


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


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region check/lay so dien thoai sms theo user
        public DataTable GETSMSPHONE(string userid)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@userid";
            p1.Value = userid;
            p1.SqlDbType = SqlDbType.Text;


            iRead = DataAccess.GetFromDataTable("SMS_GETPHONENO_BY_USERID", p1);


            return iRead;
        }
        #endregion
        //23.11.2015 minh add this function
        public DataTable CheckContractblock(string contractno, string status)
        {
            DataTable ContractExist = new DataTable();

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@contractno";
            p1.Value = contractno;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@status";
            p2.Value = status;
            p2.SqlDbType = SqlDbType.Text;

            ContractExist = DataAccess.GetFromDataTable("SEMS_Check_Contractblock", p1, p2);


            return ContractExist;
        }
        public DataTable GetCorpType(string contractno)
        {
            return DataAccess.GetFromDataTable("EBA_CONTRACT_GET_CORPTYPE", new SqlParameter[]
            {
            new SqlParameter
            {
                ParameterName = "@contractno",
                Value = contractno,
                SqlDbType = SqlDbType.Text
            }
            });
        }
        public DataTable GetRoleByServiceAndContractForCard(string serviceID, string contractNo)
        {

            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@SERVICEID";
            p1.Value = serviceID;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@CONTRACTNO";
            p2.Value = contractNo;
            p2.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("SEMS_GETROLEBYSERVICEANDCONTRACTFORCARD", p1, p2);


            return iRead;

        }

        //LanNTH
        #region Insert corp Matrix       
        public DataSet InsertCorpMaTrix(ContractModel model,
            DataTable tblUser, DataTable tblIbankUser, DataTable tblSMSUser, DataTable tblMBUser, DataTable tblPHOUser, DataTable IBUserRight,
            DataTable SMSUserRight, DataTable MBUserRight, DataTable PHOUserRight, DataTable tblUserGroup, DataTable ContractRoleDetail, DataTable ContractAccount,
            DataTable TranrightDetail, DataTable UserAccount, DataTable DeptDefault, DataTable RoleDefault, DataTable SMSNotify, DataTable dtTranAlter, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCONTRACTCOIN");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tạo khách hàng mới");
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, model.contractNo);


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

                    tblContract.Rows.Add(row1);
                }

                //add vao phan tu thu 2 mang object
                insertContract[1] = tblContract;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACT, insertContract);
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
        #endregion


        #region Check Contract No
        public string CheckContractNo(string contractNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string result = string.Empty;
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CHECKCONTRACTNO");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check ContractNo");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasInput.Add("CONTRACTNO", contractNo);

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



        #region get CONTRACT_LEVEL_ID
        public string GetContractLevelID(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                string level = string.Empty;
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETLEVELCONTRACT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load Config Level Contract");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");


                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    level = ds.Tables[0].Rows[0]["VARVALUE"].ToString();
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return level;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Load CONTRACT_LEVEL By Cbb
        public DataSet LoadContractLevelCBB(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLOADLVCONTRACT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load  Level Contract cbb");
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
        #endregion

        #region Search product by condition
        public DataSet LoaKYCInfor(string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETKYCINFOR");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load kyc infor");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
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


        #region UPDATE MERCHANT CODE
        public DataSet UpdateMerchantCode(string contractno, string merchantCode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "UPDATEMERCHANTCODE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "CAP NHAT LEVEL CHO HOP DONG");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add("MERCHANTCODE", merchantCode);


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

        #region GetReason
        public DataSet GetReason(string reasonID, string reasonCode, string reasonAction, string reasonType, string reasonEvent, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSREASONSEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "get reason");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add("REASONID", reasonID);
                hasInput.Add("REASONCODE", reasonCode);
                hasInput.Add("REASONACTION", reasonAction);
                hasInput.Add("REASONTYPE", reasonType);
                hasInput.Add("REASONEVENT", reasonEvent);

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
        #region Insert Reject Reason
        public DataSet InsertRejectReason(string reasonID, string id, string desc, string type, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSREASONREJECT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Reject reason");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add("REASONID", reasonID);
                hasInput.Add("ID", id);
                hasInput.Add("DESC", desc);
                hasInput.Add("TYPE", type);

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


        #region Get Reject Reason
        public DataSet GetRejectReason(string id, string type, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMGETREASONREJECT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get Reject reason");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add("ID", id);
                hasInput.Add("TYPE", type);

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
        #region UPDATE SUB USERTYPE
        public DataSet UpdateSubUserType(string contractno, string subUserCode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "UPCONTRACTSUBUSER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "CAP NHAT LEVEL CHO HOP DONG");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add("SUBUSERCODE", subUserCode);


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

        #region UPDATE CONTRACT by contractNo
        public DataSet LinkBankAccount(string contract, DataTable dtContractUD, DataTable dtContractAccount, DataTable dtCustCode, DataTable dtUser, ref string errorCode, ref string errorDesc)
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
                object[] updateContract = new object[2];
                updateContract[0] = "SEMS_EBA_CONTRACT_UPDATE_LINKBANK";

                //add vao phan tu thu 2 mang object
                updateContract[1] = dtContractUD;

                hasInput.Add("UPDATECONTRACT", updateContract);
                #endregion

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


        #region Get CIF Open BanK
        public Hashtable GETCIFOPENBANK(string Custtype, string CustName, string Dob, string Address,
            string City, string Email, string Phone, string License, string Nation, string Sex, string Accttype, string Ccyid, string brandid,
            string branchName,
            ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETCIFOPENBANK");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "GET CIF OPEN BANK");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CUSTNAME, CustName);
                hasInput.Add(SmartPortal.Constant.IPC.DOB, Dob);
                hasInput.Add(SmartPortal.Constant.IPC.ADDRESS, Address);
                hasInput.Add("CITY", City);
                hasInput.Add(SmartPortal.Constant.IPC.EMAIL, Email);
                hasInput.Add(SmartPortal.Constant.IPC.PHONE, Phone);
                hasInput.Add(SmartPortal.Constant.IPC.LICENSE, License);
                hasInput.Add(SmartPortal.Constant.IPC.NATION, Nation);
                hasInput.Add(SmartPortal.Constant.IPC.SEX, Sex);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTTYPE, Accttype);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, Ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, brandid);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHNAME, branchName);

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

                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Get CIF Open BanK
        public Hashtable OpenAccountBank(string requestID, string contractNO, string userid, string custcode, string dpttype, string catcode, string trandesc, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "OPENBANKACT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, trandesc);
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("REQUESTID", requestID);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODE, custcode);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNO);
                hasInput.Add("DPTTYPE", dpttype);
                hasInput.Add("CATCODE", catcode);
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

                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region UPDATE CONTRACT by contractNo
        public DataSet OBankAccount(string contract, DataTable dtContractUD, DataTable dtContractAccount, DataTable dtCustCode, DataTable dtUser, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSOPENBACKACCOUNT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Link bank Account");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contract);

                #region Update table Customer
                object[] updateContract = new object[2];
                updateContract[0] = "SEMS_EBA_CONTRACT_UPDATE_LINKBANK";

                //add vao phan tu thu 2 mang object
                updateContract[1] = dtContractUD;

                hasInput.Add("UPDATECONTRACT", updateContract);
                #endregion

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
                updateUsers[0] = "SEMS_USER_OPEN_BANK";

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

        #region LOAD contract for approve
        public DataSet RejectOpenBank(string userid, string status, string userApprove, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "REJECTOPENBANK");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Reject Open Bank");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.USERAPPROVED, userApprove);

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

        public DataSet GetContractHasGroup(string contractno, string custname, string usercreate, string createdate, string enddate, string contracttype, string usertype, string status, string licenseid, string custcode, string PhoneNo, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCONTRACTHASGROUP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.FULLNAME, custname);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATE, usercreate);
                hasInput.Add(SmartPortal.Constant.IPC.CREATEDATE, createdate);
                hasInput.Add(SmartPortal.Constant.IPC.ENDDATE, enddate);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTTYPE, contracttype);
                hasInput.Add(SmartPortal.Constant.IPC.USERTYPE, usertype);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.LICENSEID, licenseid);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODE, custcode);
                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, PhoneNo);
                hasInput.Add("RECPERPAGE", recPerPage);
                hasInput.Add("RECINDEX", recIndex);
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

        public DataSet UpdateTransactionAlterOfContract(string contractNo, string AlerType, string isbool, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "UPDATETRANALTER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);
                hasInput.Add("ALERTYPE", AlerType);
                hasInput.Add("ISBOOL", isbool);
                DataSet ds = new DataSet();
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
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
        public DataSet GetRequestIDByCustCode(string custcode, string kycType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_GET_REQUESTID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "LOAD ALL KYC");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODE, custcode);
                hasInput.Add("KYCTYPE", kycType);

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
        public DataSet GetRequestIDByPPNumber(string ppNumber, string kycID, string kyctype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_GET_REQUESTID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "LOAD ALL KYC");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("PPNUMBER", ppNumber);
                hasInput.Add("KYCID", kycID);
                hasInput.Add("KYCTYPE", kyctype);

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

        public DataSet GETKYCODEBYKYCID(string kycid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_GET_KYCCODE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "GET KYC CODE");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("KYCID", kycid);

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

        public DataSet GenQRCode(string sourcevalue, string userid, ref string qr, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_UPDATEQR");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin qr code theo contracno");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("ORIGINALQR", sourcevalue);
                hasInput.Add("STATUS", "A");
                hasInput.Add("USERID", userid);
                qr = string.Empty;

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    qr = (string)hasOutput["QR"];
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
        public DataSet GetConfigQr(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETCONFIGQR");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin qr code theo contracno");
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


    }
}
