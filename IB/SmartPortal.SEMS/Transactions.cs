using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using SmartPortal.DAL;

namespace SmartPortal.SEMS
{
   public  class Transactions
   {
       #region  Excess Sweeping
       public DataTable ExcessSweepingSearch(string ScheduleName, string ScheduleID, string ContractNo, string ScheduleType, string Status,string userid, ref string errorCode, ref string errorDesc)
       {
           try
           {
               SqlParameter p1 = new SqlParameter();
               p1.ParameterName = "@SCHEDULENAME";
               p1.Value = ScheduleName;
               p1.SqlDbType = SqlDbType.NVarChar;

               SqlParameter p2 = new SqlParameter();
               p2.ParameterName = "@SCHEDULEID";
               p2.Value = ScheduleID;
               p2.SqlDbType = SqlDbType.VarChar;

               SqlParameter p3 = new SqlParameter();
               p3.ParameterName = "@CONTRACTNO";
               p3.Value = ContractNo;
               p3.SqlDbType = SqlDbType.VarChar;

               SqlParameter p4 = new SqlParameter();
               p4.ParameterName = "@SCHEDULETYPE";
               p4.Value = ScheduleType;
               p4.SqlDbType = SqlDbType.VarChar;

               SqlParameter p5 = new SqlParameter();
               p5.ParameterName = "@STATUS";
               p5.Value = Status;
               p5.SqlDbType = SqlDbType.VarChar;

               SqlParameter p6 = new SqlParameter();
               p6.ParameterName = "@USERCREATE";
               p6.Value = userid;
               p6.SqlDbType = SqlDbType.VarChar;

               return DataAccess.GetFromDataTable("IB_EXESSWEEPING_SELECT", p1, p2, p3, p4, p5, p6);
           }
           catch (Exception ex)
           {
               return new DataTable();
           }
       }
       public DataTable GetContractAccountList(string CustID, string ContractNo)
       {

           DataTable iRead;

           SqlParameter p1 = new SqlParameter();
           p1.ParameterName = "@CUSTID";
           p1.Value = CustID;
           p1.SqlDbType = SqlDbType.Text;

           SqlParameter p2 = new SqlParameter();
           p2.ParameterName = "@CONTRACTNO";
           p2.Value = ContractNo;
           p2.SqlDbType = SqlDbType.Text;

           iRead = DataAccess.GetFromDataTable("SEMS_EBA_CONTRACTACCOUNT_GETACCTNO", p1, p2);


           return iRead;
       }
       #endregion
        #region LOAD TRAN FOR APPROVE
        public DataSet LoadTranApp(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLOADTRANFORAPP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load information for app");
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

        #region LOAD TRAN FOR APPROVE BY TRANCODE
        public DataSet LoadTranAppByTrancode(string tranCode,ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLOADTRANBTC");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load information for application by trancode");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, tranCode);

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

        #region LOAD ALL TRAN FOR APPROVE BY TRANCODE
        public DataSet LoadAllTranByTrancode(string tranID, string from, string to, string desc, string result, string ipcTranCode, string status, string branchID, string bank,string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETALLTRANBYCON");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Approve message");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranID);
                hasInput.Add(SmartPortal.Constant.IPC.FROM, from);
                hasInput.Add(SmartPortal.Constant.IPC.TO, to);
                hasInput.Add(SmartPortal.Constant.IPC.DESC, desc);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, ipcTranCode);
                hasInput.Add(SmartPortal.Constant.IPC.RESULT, result);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchID);
                hasInput.Add(SmartPortal.Constant.IPC.BANKCODE, bank);
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

        #region Load các giao dịch chờ tạo điện
        public DataSet LoadAllTranForSwiftCreate(string tranID, string from, string to, string desc, string result, string ipcTranCode, string status,string branchID,string bank, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS0405");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load all transaction wait message creating");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranID);
                hasInput.Add(SmartPortal.Constant.IPC.FROM, from);
                hasInput.Add(SmartPortal.Constant.IPC.TO, to);
                hasInput.Add(SmartPortal.Constant.IPC.DESC, desc);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, ipcTranCode);
                hasInput.Add(SmartPortal.Constant.IPC.RESULT, result);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchID);
                hasInput.Add(SmartPortal.Constant.IPC.BANKCODE, bank);

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

        #region Lấy thông tin chi tiết điện
        public DataSet GetSwiftDetail(string tranID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS0406");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get detail message");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranID);
                
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

        #region Tạo điện
        public bool CreateSwift(string tranID,string route, ref string errorCode, ref string errorDesc)
        {
            try
            {
                ////Show hastable result from IPC
                //Hashtable hasInput = new Hashtable();
                //Hashtable hasOutput = new Hashtable();

                ////add key into input
                //hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000207");
                //hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                //hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                //hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tạo điện");
                //hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                //hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranID);
                //hasInput.Add("ROUTE", route);

                //hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                //DataSet ds = new DataSet();

                //if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                //{
                //    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                //    errorCode = "0";
                //}
                //else
                //{
                //    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                //    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                //}

                //return ds;

                new SmartPortal.IB.Customer().GetInfo("TEMP1", new object[] { tranID }, ref errorCode, ref errorDesc);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET TRAN BY TRANID
        public DataSet GetTranByTranID(string tranID, string userid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETBYTRANID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get transaction information by tranid");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranID);
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

        #region GET TRAN BY TRANID
        public DataSet GetTranByTranID(string tranID,ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSELECTBYTRANID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get transaction information by tranid");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranID);

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

        #region TELLER APPROVE TRANS _SEARCH
        public DataSet SearchTellerApproveTrans(string ApptranID,string trancode,string roleid,string ccyid,string fromlimit,string tolimit, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSTLAPPTRANSEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Search workflow for teller approve");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.APPTRANID, ApptranID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.ROLEID, roleid);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.FROMLIMIT, fromlimit);
                hasInput.Add(SmartPortal.Constant.IPC.TOLIMIT, tolimit);

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

        #region GET DETAILS ALL TELLER APPROVE TRANS
        public DataSet GetDetailsProcessApprove(string ApptranID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSTLAPPTRANGETALL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get all detail of approve workflow");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.APPTRANID, ApptranID);

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

        #region Insert Process Approve fpr teller
        public DataSet InsertProcessApprove(string apptranid, string trancode, string roleid, string contractno,string CCYID,string fromlimit,string tolimit,string lastlevapp,string passlevel,string status,string usercreate ,string userapprove,string usermodify,string lastmodify, DataTable tblAppTranDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPROCESSAPPINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Create a new workflow of approve");

                #region Insert bảng customer
                object[] inserttellerapptran = new object[2];
                inserttellerapptran[0] = "SEMS_IBS_TELLERAPPTRAN_INSERT";
                //tao bang chua thong tin process
                DataTable tbltellerapptran = new DataTable();
                DataColumn colTranAppID = new DataColumn("colTranAppID");
                DataColumn colTranCode = new DataColumn("colTranCode");
                DataColumn colRoleID = new DataColumn("colRoleID");
                DataColumn colContractNo = new DataColumn("colContractNo");
                DataColumn colCCYID = new DataColumn("colCCYID");
                DataColumn colFromLimit = new DataColumn("colFromLimit");
                DataColumn colToLimit = new DataColumn("colToLimit");
                DataColumn colLastLevApp = new DataColumn("colLastLevApp");
                DataColumn colPassLevel = new DataColumn("colPassLevel");
                DataColumn colStatus = new DataColumn("colStatus");
                DataColumn colUserCreate = new DataColumn("colUserCreate");
                DataColumn colUserApprove = new DataColumn("colUserApprove");
                DataColumn colUserModify = new DataColumn("colUserModify");
                DataColumn colLastModify = new DataColumn("colLastModify");


                //add vào table product
                tbltellerapptran.Columns.Add(colTranAppID);
                tbltellerapptran.Columns.Add(colTranCode);
                tbltellerapptran.Columns.Add(colRoleID);
                tbltellerapptran.Columns.Add(colContractNo);
                tbltellerapptran.Columns.Add(colCCYID);
                tbltellerapptran.Columns.Add(colFromLimit);
                tbltellerapptran.Columns.Add(colToLimit);
                tbltellerapptran.Columns.Add(colLastLevApp);
                tbltellerapptran.Columns.Add(colPassLevel);
                tbltellerapptran.Columns.Add(colStatus);
                tbltellerapptran.Columns.Add(colUserCreate);
                tbltellerapptran.Columns.Add(colUserApprove);
                tbltellerapptran.Columns.Add(colUserModify);
                tbltellerapptran.Columns.Add(colLastModify);

                //tao 1 dong du lieu
                DataRow row = tbltellerapptran.NewRow();
                row["colTranAppID"] = apptranid;
                row["colTranCode"] = trancode;
                row["colRoleID"] = roleid;
                row["colContractNo"] = contractno;
                row["colCCYID"] = CCYID;
                row["colFromLimit"] = fromlimit;
                row["colToLimit"] = tolimit;
                row["colLastLevApp"] = lastlevapp;
                row["colPassLevel"] = passlevel;
                row["colStatus"] = status;
                row["colUserCreate"] = usercreate;
                row["colUserApprove"] = userapprove;
                row["colUserModify"] = usermodify;
                row["colLastModify"] = lastmodify;

                tbltellerapptran.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                inserttellerapptran[1] = tbltellerapptran;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTTELLERAPPTRAN, inserttellerapptran);
                #endregion


                #region Insert quyền PRODUCT
                object[] inserttellerapptrandetail= new object[2];
                inserttellerapptrandetail[0] = "SEMS_IBS_TELLERAPPTRANDETAIL_INSERT";

                //add vao phan tu thu 2 mang object
                inserttellerapptrandetail[1] = tblAppTranDetail;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTTELLERAPPTRANDETAIL, inserttellerapptrandetail);
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

        #region Update Process Approve fpr teller
        public DataSet UpdateProcessApprove(string apptranid, string trancode, string roleid, string contractno, string CCYID, string fromlimit, string tolimit, string lastlevapp, string passlevel, string status, string usercreate, string userapprove, string usermodify, string lastmodify, DataTable tblAppTranDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPROCESSAPPUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Update aprove workflow");

                #region Insert bảng customer
                object[] inserttellerapptran = new object[2];
                inserttellerapptran[0] = "SEMS_IBS_TELLERAPPTRAN_UPDATE";
                //tao bang chua thong tin process
                DataTable tbltellerapptran = new DataTable();
                DataColumn colTranAppID = new DataColumn("colTranAppID");
                DataColumn colTranCode = new DataColumn("colTranCode");
                DataColumn colRoleID = new DataColumn("colRoleID");
                DataColumn colContractNo = new DataColumn("colContractNo");
                DataColumn colCCYID = new DataColumn("colCCYID");
                DataColumn colFromLimit = new DataColumn("colFromLimit");
                DataColumn colToLimit = new DataColumn("colToLimit");
                DataColumn colLastLevApp = new DataColumn("colLastLevApp");
                DataColumn colPassLevel = new DataColumn("colPassLevel");
                DataColumn colStatus = new DataColumn("colStatus");
                DataColumn colUserCreate = new DataColumn("colUserCreate");
                DataColumn colUserApprove = new DataColumn("colUserApprove");
                DataColumn colUserModify = new DataColumn("colUserModify");
                DataColumn colLastModify = new DataColumn("colLastModify");


                //add vào table product
                tbltellerapptran.Columns.Add(colTranAppID);
                tbltellerapptran.Columns.Add(colTranCode);
                tbltellerapptran.Columns.Add(colRoleID);
                tbltellerapptran.Columns.Add(colContractNo);
                tbltellerapptran.Columns.Add(colCCYID);
                tbltellerapptran.Columns.Add(colFromLimit);
                tbltellerapptran.Columns.Add(colToLimit);
                tbltellerapptran.Columns.Add(colLastLevApp);
                tbltellerapptran.Columns.Add(colPassLevel);
                tbltellerapptran.Columns.Add(colStatus);
                tbltellerapptran.Columns.Add(colUserCreate);
                tbltellerapptran.Columns.Add(colUserApprove);
                tbltellerapptran.Columns.Add(colUserModify);
                tbltellerapptran.Columns.Add(colLastModify);

                //tao 1 dong du lieu
                DataRow row = tbltellerapptran.NewRow();
                row["colTranAppID"] = apptranid;
                row["colTranCode"] = trancode;
                row["colRoleID"] = roleid;
                row["colContractNo"] = contractno;
                row["colCCYID"] = CCYID;
                row["colFromLimit"] = fromlimit;
                row["colToLimit"] = tolimit;
                row["colLastLevApp"] = lastlevapp;
                row["colPassLevel"] = passlevel;
                row["colStatus"] = status;
                row["colUserCreate"] = usercreate;
                row["colUserApprove"] = userapprove;
                row["colUserModify"] = usermodify;
                row["colLastModify"] = lastmodify;

                tbltellerapptran.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                inserttellerapptran[1] = tbltellerapptran;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATETELLERAPPTRAN, inserttellerapptran);
                #endregion


                #region Insert quyền PRODUCT
                object[] inserttellerapptrandetail = new object[2];
                inserttellerapptrandetail[0] = "SEMS_IBS_TELLERAPPTRANDETAIL_UPDATE";

                //add vao phan tu thu 2 mang object
                inserttellerapptrandetail[1] = tblAppTranDetail;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATETELLERAPPTRANDETAIL, inserttellerapptrandetail);
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

        #region Delete Process approve for teller
        public DataSet DeleteProcess(string apptranID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPROCESSAPPDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Delete workflow");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.APPTRANID, apptranID);

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

        #region Search USERAPPROVELIMIT by condition (LIMIT OF TELLER)
        public DataSet SearchUserApproveLimit(string EmpID, string trancode, string ccyid, string limitapprove, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSETLIMITTELLER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Search approve limit for teller");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, EmpID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITAPPROVE, limitapprove);


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

        #region Get limit of teller by userid
        public DataSet GetAllLimitTellerByUID(string userID,string trancode,string ccyid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSETLMTELLERALL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get limit of teller");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);

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

        #region Get limit level by contractno
        public DataTable GetAllLimitLevelByContract(string contractNo,string level, string tranCode, string CCYID)
        {

            DataTable iRead;

            SqlParameter p0 = new SqlParameter();
            p0.ParameterName = "@contractNo";
            p0.Value = contractNo;
            p0.SqlDbType = SqlDbType.Text;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@level";
            p1.Value = level;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@trancode";
            p2.Value = tranCode;
            p2.SqlDbType = SqlDbType.Text;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@ccyid";
            p3.Value = CCYID;
            p3.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("SEMS_EBA_LEVELAPPROVELIMIT_GETALLBYUSERID", p0, p1, p2, p3);


            return iRead;
        }
        #endregion

        #region Get limit of level
        public DataTable GetLimitLevelByContract(string ID)
        {

            DataTable iRead;

            SqlParameter p0 = new SqlParameter();
            p0.ParameterName = "@ID";
            p0.Value = ID;
            p0.SqlDbType = SqlDbType.Text;


            iRead = DataAccess.GetFromDataTable("IB_GETLIMITOFLEVEL", p0);


            return iRead;
        }
        #endregion

        #region Get limit goup teller by roleid
        public DataTable GetAllLimitGroupTellerByRoleID(string roleID, string tranCode, string CCYID)
        {
            
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@roleID";
            p1.Value = roleID;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@trancode";
            p2.Value = tranCode;
            p2.SqlDbType = SqlDbType.Text;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@ccyid";
            p3.Value = CCYID;
            p3.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("SEMS_EBA_GROUPAPPROVELIMIT_GETALLBYROLEID", p1, p2,p3);


            return iRead;
        }
        #endregion

        #region INSERT USERAPPROVELIMIT  (INSERT LIMIT OF TELLER)
        public DataSet InsertUserApproveLimit(string EmpID, string trancode, string ccyid, string approvallimit,string totallimitday,string countlimit, string usercreated,ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSETLMTELLERINS");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Add limit approve of teller");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, EmpID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.APPROVALLIMIT, approvallimit);
                hasInput.Add(SmartPortal.Constant.IPC.TOTALLIMITDAY, totallimitday);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTLIMIT, countlimit);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATE, usercreated);

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

        #region INSERT LEVELAPPROVELIMIT  (INSERT LIMIT LEVEL)
        public DataSet InsertLevelApproveLimit(string ContractNo,string level, string trancode, string ccyid, string limitapprove, string desc, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSHTL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Add approve limit of level");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO,ContractNo);
                hasInput.Add(SmartPortal.Constant.IPC.LEVEL, level);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITAPPROVE, limitapprove);
                hasInput.Add(SmartPortal.Constant.IPC.DESC, desc);


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


        #region INSERT NHOM TELLER  (INSERT LIMIT OF TELLER)
        public DataSet InsertGroupTellerApproveLimit(string EmpID, string trancode, string ccyid, string limitapprove, string desc, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSLGT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Add approve limit of teller group");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, EmpID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITAPPROVE, limitapprove);
                hasInput.Add(SmartPortal.Constant.IPC.DESC, desc);


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

        #region UPDATE USERAPPROVELIMIT  (UPDATE LIMIT OF TELLER)
        public DataSet UpdateUserApproveLimit(string EmpID, string trancode, string ccyid, string approvallimit, string totallimitday, string countlimit,string usermodified, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSETLMTELLERUPD");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Update approve limit of teller");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.USERID, EmpID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.TOTALLIMITDAY, totallimitday);
                hasInput.Add(SmartPortal.Constant.IPC.APPROVALLIMIT, approvallimit);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTLIMIT, countlimit);
                hasInput.Add(SmartPortal.Constant.IPC.USERMODIFIED, usermodified);


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

        #region (UPDATE LIMIT OF LEVEL)
        public DataSet UpdateLevelApproveLimit(string ContractNo,string level, string trancode, string ccyid, string limitapprove, string desc, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSHTLU");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Update approve limit of level");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO,ContractNo);
                hasInput.Add(SmartPortal.Constant.IPC.LEVEL,level);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITAPPROVE, limitapprove);
                hasInput.Add(SmartPortal.Constant.IPC.DESC, desc);


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

        #region UPDATE GROUP TLLER APPROVELIMIT  (UPDATE LIMIT OF TELLER)
        public DataSet UpdateGroupTellerApproveLimit(string EmpID, string trancode, string ccyid, string limitapprove, string desc, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSLGTU");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Update approve limit of teller group");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, EmpID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITAPPROVE, limitapprove);
                hasInput.Add(SmartPortal.Constant.IPC.DESC, desc);


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

        #region Delete limit of user
        public DataSet DeleteUserApproveLimit(string userid, string trancode, string ccyid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSETLMTELLERDEL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Delete approve limit for teller");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);


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

        #region Delete limit of level
        public DataSet DeleteLevelApproveLimit(string id, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSHTLD");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Delete approve limit for level");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ID, id);
               
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


        #region Delete limit group of user
        public DataSet DeleteGroupApproveLimit(string userid, string trancode, string ccyid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSLGTD");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Delete approve limit for teller group");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);


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

        #region Load các giao dịch chờ duyệt
        public DataSet LoadTranForApprove(string userID, string tranID, string tranCode, string from, string to, string status, string account, string apprsts,string BranchReceiver, string brachID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSAPPROVETRAN");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get list of pendding approve transaction");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, tranCode);
                hasInput.Add(SmartPortal.Constant.IPC.FROM, from);
                hasInput.Add(SmartPortal.Constant.IPC.TO, to);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.ACCOUNT, account);
                hasInput.Add(SmartPortal.Constant.IPC.APPRSTS, apprsts);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHRECEIVER, BranchReceiver);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, brachID);

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

        #region Load các giao dịch chờ duyệt
        public DataTable LoadTranForApproveCloseFD(string userID, string tranID, string tranCode, string from, string to, string status, string account, string apprsts, string BranchReceiver, string brachID,string custname,string contractno, ref string errorCode, ref string errorDesc)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@userID";
                p1.Value = userID;
                p1.SqlDbType = SqlDbType.Text;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@tranid";
                p2.Value = tranID;
                p2.SqlDbType = SqlDbType.Text;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@trancode";
                p3.Value = tranCode;
                p3.SqlDbType = SqlDbType.Text;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@from";
                p4.Value = from;
                p4.SqlDbType = SqlDbType.Text;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@to";
                p5.Value = to;
                p5.SqlDbType = SqlDbType.Text;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@status";
                p6.Value = status;
                p6.SqlDbType = SqlDbType.Text;

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@account";
                p7.Value = account;
                p7.SqlDbType = SqlDbType.Text;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@apprsts";
                p8.Value = apprsts;
                p8.SqlDbType = SqlDbType.Text;

                SqlParameter p9 = new SqlParameter();
                p9.ParameterName = "@BRANCHRECEIVER";
                p9.Value = BranchReceiver;
                p9.SqlDbType = SqlDbType.Text;

                SqlParameter p10 = new SqlParameter();
                p10.ParameterName = "@brid";
                p10.Value = brachID;
                p10.SqlDbType = SqlDbType.Text;

                SqlParameter p11 = new SqlParameter();
                p11.ParameterName = "@custname";
                p11.Value = custname;
                p11.SqlDbType = SqlDbType.Text;

                SqlParameter p12 = new SqlParameter();
                p12.ParameterName = "@contractno";
                p12.Value = contractno;
                p12.SqlDbType = SqlDbType.Text;



                iRead = DataAccess.GetFromDataTable("SEMS_GETLISTTRANAPPROVECLOSEFD", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
                errorCode = "0";

                return iRead;
            }
            catch (Exception ex)
            {
                errorCode = "9999";
                throw ex;
            }
        }
        #endregion

        #region Duyệt giao dịch
        public DataSet UserApp(string IPCTRANSID, string userID,string desc, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000404");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Approve BankSite transaction");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, IPCTRANSID);
                
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add("ERRORDESC", desc);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    MakeApproveDate(IPCTRANSID, DateTime.Now.ToString());
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

        #region Duyệt giao dịch TT
        public DataSet UserAppTT(string IPCTRANSID, string userID, string desc,string ltt,string ldh, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000404");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, desc);
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, IPCTRANSID);

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.LTT, ltt);
                hasInput.Add(SmartPortal.Constant.IPC.LDH, ldh);
                hasInput.Add(SmartPortal.Constant.IPC.APPROVEDATE, DateTime.Now.ToString());
                

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    UpdateInterest(IPCTRANSID, ltt, ldh,desc);
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

        public DataTable UpdateInterest(string ipctransid,string ltt,string ldh,string desc)
        {

            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@ipctransid";
            p1.Value = ipctransid;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@ltt";
            p2.Value = ltt;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@ldh";
            p3.Value = ldh;
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@desc";
            p4.Value = desc+"|"+DateTime.Now.ToString();
            p4.SqlDbType = SqlDbType.NVarChar;


            iRead = DataAccess.GetFromDataTable("eba_updateinterestTT", p1, p2, p3,p4);


            return iRead;
        }

        #region Hủy giao dịch
        public DataSet TellerDestroy(string IPCTRANSID, string userID,string desc, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00403");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Delete BankSite transaction");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, IPCTRANSID);

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add("DESCR", desc);

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

        public DataTable GetLogTranDetail(string tranID)        {
           
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@TRANID";
            p1.Value = tranID;
            p1.SqlDbType = SqlDbType.Text;


            iRead = DataAccess.GetFromDataTable("SEMS_IPCLOGTRANSDETAIL_GETBYTRANID", p1);


            return iRead;
        }

        public DataTable GetTranByParamAndService(string paramm,string status,string serviceid)
        {

            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@PARAM";
            p1.Value = paramm;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@STATUS";
            p2.Value = status;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@SERVICE";
            p3.Value = serviceid;
            p3.SqlDbType = SqlDbType.VarChar;


            iRead = DataAccess.GetFromDataTable("SEMS_SMP_PAGES_GETRANBYPARAMANDSERVICE", p1,p2,p3);


            return iRead;
        }

        #region Deposit
        #region Deposit
        public Hashtable Deposit(string checkno,string status, string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00404");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Hoản tiền chuyển khoản ngoài hệ thống");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CHECKNO, checkno);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);

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
        #endregion

        #region Delete ContractLimit
        public DataSet DeleteContractLimit(string contractno, string trancode, string limittype, string ccyid,string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00025");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Delete limit of contract");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
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
        public DataTable MakeApproveDate(string ipctransid,string date)
        {

            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@ipctransid";
            p1.Value = ipctransid;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@date";
            p2.Value = date;
            p2.SqlDbType = SqlDbType.VarChar;


            iRead = DataAccess.GetFromDataTable("eba_makeApprovedate", p1, p2);


            return iRead;
        }
        public DataTable GetLimitConfigbyContractLevel(string userid, string accno, string creditaccno, string trancode, string ccyid, string limittype,string contractLevel)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@USERID";
            p1.Value = userid;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@ACCNO";
            p2.Value = accno;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@CREDITACCNO";
            p3.Value = creditaccno;
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@TRANCODE";
            p4.Value = trancode;
            p4.SqlDbType = SqlDbType.VarChar;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@CCYID";
            p5.Value = ccyid;
            p5.SqlDbType = SqlDbType.VarChar;

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@LIMITTYPE";
            p6.Value = limittype;
            p6.SqlDbType = SqlDbType.VarChar;

            SqlParameter p7 = new SqlParameter();
            p7.ParameterName = "@CONTRACTLV";
            p7.Value = contractLevel;
            p7.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IB_CHECKTRANSACTIONLIMITBYCONTRACTLV", p1, p2, p3, p4, p5, p6,p7);

            return iRead;
        }
        public DataTable GetLimitConfig(string userid, string accno, string creditaccno, string trancode, string ccyid, string limittype)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@USERID";
            p1.Value = userid;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@ACCNO";
            p2.Value = accno;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@CREDITACCNO";
            p3.Value = creditaccno;
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@TRANCODE";
            p4.Value = trancode;
            p4.SqlDbType = SqlDbType.VarChar;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@CCYID";
            p5.Value = ccyid;
            p5.SqlDbType = SqlDbType.VarChar;

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@LIMITTYPE";
            p6.Value = limittype;
            p6.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IB_CHECKTRANSACTIONLIMIT", p1, p2, p3, p4, p5, p6);

            return iRead;

        }
        public DataTable GetContractLevelByUserID(string userid)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@USERID";
            p1.Value = userid;
            p1.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IB_GETCONTRACTLEVEL", p1);

            return iRead;

        }


        public DataTable ExcessSweepingSearch_forApprove(string ScheduleName, string ScheduleID, string ContractNo, string ScheduleType, string Status, string userid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@SCHEDULENAME";
                p1.Value = ScheduleName;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@SCHEDULEID";
                p2.Value = ScheduleID;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@CONTRACTNO";
                p3.Value = ContractNo;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@SCHEDULETYPE";
                p4.Value = ScheduleType;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@STATUS";
                p5.Value = Status;
                p5.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@USERCREATE";
                p6.Value = userid;
                p6.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("IB_EXESSWEEPING_SELECT_FOR_APPROVE", p1, p2, p3, p4, p5, p6);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }

        #region APPROVE CMS by scheduleID
        public DataSet ExcessSweeping_Approve(string scheduleID, string status, string userApprove, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCMSAPPROVE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "approve CMS by scheduleID");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULEID, scheduleID);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.USERAPPROVE, userApprove);



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
        #region REJECT CMS by scheduleID
        public DataSet ExcessSweeping_Reject(string scheduleID, string status, string userApprove, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCMSREJECT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "reject CMS by scheduleID");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULEID, scheduleID);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.USERAPPROVE, userApprove);



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
        
        public DataTable CaculateTransferTodayByAccountNo(string accountno, string creditAccNo)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@ACCOUNTNO";
            p1.Value = accountno;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@CREDITACCOUNT";
            p2.Value = creditAccNo;
            p2.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("DETAILSTRANFERFROMCONTRACTTOBRANCH", p1, p2);

            return iRead;

        }
        #region SMSNotification
        public DataTable CaculateTransferTodayByAccountNo1(string accountno, string creditAccNo)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@ACCOUNTNO";
            p1.Value = accountno;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@CREDITACCOUNT";
            p2.Value = creditAccNo;
            p2.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("DETAILSTRANFERFROMCONTRACTTOBRANCH", p1, p2);

            return iRead;

        }
        #endregion


        //vutt 25032016: chu y chi dung cho select, ko dung insert update
        public DataSet DoStored(string storeName, object[] para, ref string errorCode, ref string errorDesc)
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

        #region Search limit of by condition
        public DataSet GetContractLimitByCondition(string contractno, string trancode, string ccyid, string tranlimit, string status, string branchid, string custname, string limittype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00022");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Search detail limit contract");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANLIMIT, tranlimit);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTNAME, custname);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);

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

        #region Insert ContracttLimit
        public DataSet InsertContractLimit(string contractno, string trancode, string ccyid, string tranlm, string countlm, string totallmday, string usercreated, string datecreated, string status, string branchid, string limittype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00023");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Create limit contract");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANLIMIT, tranlm);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTLIMIT, countlm);
                hasInput.Add(SmartPortal.Constant.IPC.TOTALLIMITDAY, totallmday);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATED, usercreated);
                hasInput.Add(SmartPortal.Constant.IPC.DATECREATED, datecreated);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);


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

        #region Update ContractLimit
        public DataSet UpdateContractLimit(string contractno, string trancode, string ccyid, string tranlm, string countlm, string totallmday, string userodified, string datemodified, string status, string statuscurrent, string limittype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS00024");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Edit limit contract");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANLIMIT, tranlm);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTLIMIT, countlm);
                hasInput.Add(SmartPortal.Constant.IPC.TOTALLIMITDAY, totallmday);
                hasInput.Add(SmartPortal.Constant.IPC.USERMODIFIED, userodified);
                hasInput.Add(SmartPortal.Constant.IPC.DATEMODIFIED, datemodified);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.STATUSCURRENT, statuscurrent);
                hasInput.Add(SmartPortal.Constant.IPC.LIMITTYPE, limittype);


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

    }

}
