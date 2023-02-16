using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using SmartPortal.DAL;

namespace SmartPortal.IB
{
    public class Transactions
    {

        #region Get user of contractno by userid
        public DataSet GetUserOfContractNoByUID(string userID, string IsLevel, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000001");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "get list of user in same contract");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.ISLEVEL, IsLevel);

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

        #region Get all limit of user in contract (nếu 2 tham số trancode & ccyid rỗng là lấy danh sách)
        public DataSet GetAllLimitUser(string userID, string sourcid, string contractno, string trancode, string ccyid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000002");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "get details of user level in same contract");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
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

        #region Insert CorpUserLimit
        public DataSet InsertCorpUserLimit(string userid, string trancode, string ccyid, string tranlm, string countlm, string totallmday, string username, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000003");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Create limit for user");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANLIMIT, tranlm);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTLIMIT, countlm);
                hasInput.Add(SmartPortal.Constant.IPC.TOTALLIMITDAY, totallmday);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATED, username);

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

        #region Update CorpUserLimit
        public DataSet UpdateCorpUserLimit(string userid, string trancode, string ccyid, string tranlm, string countlm, string totallmday, string usermodif, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000004");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Edit transaction limit for user corp");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANLIMIT, tranlm);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTLIMIT, countlm);
                hasInput.Add(SmartPortal.Constant.IPC.TOTALLIMITDAY, totallmday);
                hasInput.Add(SmartPortal.Constant.IPC.USERMODIFIED, usermodif);


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

        #region Delete CorpUserLimit
        public DataSet DeleteCorpUserLimit(string userid, string trancode, string ccyid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000005");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Delete limit of product");
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
        public DataSet LoadTranForApprove(string userID, string tranID, string tranCode, string from, string to, string status, string account, string apprsts, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBAPPROVETRAN");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "get list of pendding approve transaction");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, tranCode);
                hasInput.Add(SmartPortal.Constant.IPC.FROM, from);
                hasInput.Add(SmartPortal.Constant.IPC.TO, to);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.ACCOUNT, account);
                hasInput.Add(SmartPortal.Constant.IPC.APPRSTS, apprsts);

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

        #region update status (ONLY USE WHEN CANCEL TRANSACTION)
        public DataTable CancelTransaction(string tranID)
        {

            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@TRANID";
            p1.Value = tranID;
            p1.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("Cancel_Transaction", p1);

            return iRead;
        }

        #endregion



        #region Xem log giao dịch
        public DataSet ViewLogTransaction(string userID, string tranID, string tranCode, string from, string to, string status, string account, string apprsts, bool isdeleted, string isbatch, string batchref, string custcodecore, string custname, string contractno, string checkno, string creditAcct, string userApproved, string licenseid, string custType, string isschedule, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000407");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get log of transaction");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, tranCode);
                hasInput.Add(SmartPortal.Constant.IPC.FROM, from);
                hasInput.Add(SmartPortal.Constant.IPC.TO, to);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.ACCOUNT, account);
                hasInput.Add(SmartPortal.Constant.IPC.APPRSTS, apprsts);
                hasInput.Add(SmartPortal.Constant.IPC.ISDELETED, isdeleted);
                hasInput.Add(SmartPortal.Constant.IPC.ISBATCH, isbatch);
                hasInput.Add(SmartPortal.Constant.IPC.BATCHREF, batchref);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODECORE, custcodecore);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTNAME, custname);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasInput.Add(SmartPortal.Constant.IPC.CHECKNO, checkno);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, creditAcct);
                hasInput.Add(SmartPortal.Constant.IPC.USERAPPROVED, userApproved);
                hasInput.Add(SmartPortal.Constant.IPC.LICENSEID, licenseid);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTTYPE, custType);
                hasInput.Add(SmartPortal.Constant.IPC.ISSCHEDULE, isschedule);
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

        #region CORP USER APPROVE PROCESS _SEARCH
        public DataSet SearchCorpUserApproveProcess(string apptranID, string trancode, string contractno, string ccyid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000006");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "search all proccess for corp user approve");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.APPTRANID, apptranID);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
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

        #region GET DETAILS ALL CORP USER APPROVE TRANS
        public DataSet GetDetailsProcessApprove(string ApptranID, string contractno, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000007");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "get all process approve detail");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.APPTRANID, ApptranID);
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

        #region Insert Process Approve fpr CORP USER
        public DataSet InsertProcessApprove(string apptranid, string trancode, string contractno, string CCYID, string fromlimit, string tolimit, string lastlevapp, string passlevel, string status, string tellerapprove, string lastmodify, DataTable tblAppTranDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000008");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "create process of transaction workflow");

                #region Insert bảng customer
                object[] insertuserapptran = new object[2];
                insertuserapptran[0] = "SEMS_IBS_USERAPPTRAN_INSERT";
                //tao bang chua thong tin process
                DataTable tbluserapptran = new DataTable();
                DataColumn colTranAppID = new DataColumn("colTranAppID");
                DataColumn colTranCode = new DataColumn("colTranCode");
                DataColumn colContractNo = new DataColumn("colContractNo");
                DataColumn colCCYID = new DataColumn("colCCYID");
                DataColumn colFromLimit = new DataColumn("colFromLimit");
                DataColumn colToLimit = new DataColumn("colToLimit");
                DataColumn colLastLevApp = new DataColumn("colLastLevApp");
                DataColumn colPassLevel = new DataColumn("colPassLevel");
                DataColumn colStatus = new DataColumn("colStatus");
                DataColumn colTellerApprove = new DataColumn("colTellerApprove");
                DataColumn colLastModify = new DataColumn("colLastModify");


                //add vào table product
                tbluserapptran.Columns.Add(colTranAppID);
                tbluserapptran.Columns.Add(colTranCode);
                tbluserapptran.Columns.Add(colContractNo);
                tbluserapptran.Columns.Add(colCCYID);
                tbluserapptran.Columns.Add(colFromLimit);
                tbluserapptran.Columns.Add(colToLimit);
                tbluserapptran.Columns.Add(colLastLevApp);
                tbluserapptran.Columns.Add(colPassLevel);
                tbluserapptran.Columns.Add(colStatus);
                tbluserapptran.Columns.Add(colTellerApprove);
                tbluserapptran.Columns.Add(colLastModify);

                //tao 1 dong du lieu
                DataRow row = tbluserapptran.NewRow();
                row["colTranAppID"] = apptranid;
                row["colTranCode"] = trancode;
                row["colContractNo"] = contractno;
                row["colCCYID"] = CCYID;
                row["colFromLimit"] = fromlimit;
                row["colToLimit"] = tolimit;
                row["colLastLevApp"] = lastlevapp;
                row["colPassLevel"] = passlevel;
                row["colStatus"] = status;
                row["colTellerApprove"] = tellerapprove;
                row["colLastModify"] = lastmodify;

                tbluserapptran.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                insertuserapptran[1] = tbluserapptran;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSERAPPTRAN, insertuserapptran);
                #endregion


                #region Insert quyền PRODUCT
                object[] insertuserapptrandetail = new object[2];
                insertuserapptrandetail[0] = "SEMS_IBS_USERAPPTRANDETAIL_INSERT";

                //add vao phan tu thu 2 mang object
                insertuserapptrandetail[1] = tblAppTranDetail;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSERAPPTRANDETAIL, insertuserapptrandetail);
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

        #region Update Process Approve fpr CORP USER
        public DataSet UpdateProcessApprove(string apptranid, string trancode, string contractno, string CCYID, string fromlimit, string tolimit, string lastlevapp, string passlevel, string status, string tellerapprove, string lastmodify, DataTable tblAppTranDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000009");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "ccreate a workflow in transaction approve");

                #region Insert bảng customer
                object[] insertuserapptran = new object[2];
                insertuserapptran[0] = "SEMS_IBS_USERAPPTRAN_UPDATE";
                //tao bang chua thong tin process
                DataTable tbluserapptran = new DataTable();
                DataColumn colTranAppID = new DataColumn("colTranAppID");
                DataColumn colTranCode = new DataColumn("colTranCode");
                DataColumn colContractNo = new DataColumn("colContractNo");
                DataColumn colCCYID = new DataColumn("colCCYID");
                DataColumn colFromLimit = new DataColumn("colFromLimit");
                DataColumn colToLimit = new DataColumn("colToLimit");
                DataColumn colLastLevApp = new DataColumn("colLastLevApp");
                DataColumn colPassLevel = new DataColumn("colPassLevel");
                DataColumn colStatus = new DataColumn("colStatus");
                DataColumn colTellerApprove = new DataColumn("colTellerApprove");
                DataColumn colLastModify = new DataColumn("colLastModify");


                //add vào table product
                tbluserapptran.Columns.Add(colTranAppID);
                tbluserapptran.Columns.Add(colTranCode);
                tbluserapptran.Columns.Add(colContractNo);
                tbluserapptran.Columns.Add(colCCYID);
                tbluserapptran.Columns.Add(colFromLimit);
                tbluserapptran.Columns.Add(colToLimit);
                tbluserapptran.Columns.Add(colLastLevApp);
                tbluserapptran.Columns.Add(colPassLevel);
                tbluserapptran.Columns.Add(colStatus);
                tbluserapptran.Columns.Add(colTellerApprove);
                tbluserapptran.Columns.Add(colLastModify);

                //tao 1 dong du lieu
                DataRow row = tbluserapptran.NewRow();
                row["colTranAppID"] = apptranid;
                row["colTranCode"] = trancode;
                row["colContractNo"] = contractno;
                row["colCCYID"] = CCYID;
                row["colFromLimit"] = fromlimit;
                row["colToLimit"] = tolimit;
                row["colLastLevApp"] = lastlevapp;
                row["colPassLevel"] = passlevel;
                row["colStatus"] = status;
                row["colTellerApprove"] = tellerapprove;

                row["colLastModify"] = lastmodify;

                tbluserapptran.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                insertuserapptran[1] = tbluserapptran;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSERAPPTRAN, insertuserapptran);
                #endregion


                #region Insert quyền PRODUCT
                object[] insertuserapptrandetail = new object[2];
                insertuserapptrandetail[0] = "SEMS_IBS_USERAPPTRANDETAIL_UPDATE";

                //add vao phan tu thu 2 mang object
                insertuserapptrandetail[1] = tblAppTranDetail;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSERAPPTRANDETAIL, insertuserapptrandetail);
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
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000010");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Delete process");
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

        #region Duyệt giao dịch
        public DataSet UserApp(string IPCTRANSID, string authenType, string authenCode, string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB0000403");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Approve transaction");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, IPCTRANSID);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authenType);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, authenCode);
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

        #region Hủy giao dịch
        public DataSet UserDestroy(string IPCTRANSID, string userID, string authenType, string authenCode, string desc, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000401");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Delete transaction in Internet Banking");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, IPCTRANSID);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authenType);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, authenCode);
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

        #region Load thông tin Loại Xác thực
        public DataTable LoadAuthenType(string userID)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@userID";
                p1.Value = userID;
                p1.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromDataTable("IB_LOADAUTHENTYPE", p1);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Xem log giao dịch
        public DataSet ViewLogBatch(string userID, string from, string to, string account, string tranID, string status, string apprsts, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000409");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "View batch transaction");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.FROM, from);
                hasInput.Add(SmartPortal.Constant.IPC.TO, to);
                hasInput.Add(SmartPortal.Constant.IPC.ACCOUNT, account);
                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranID);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.APPRSTS, apprsts);

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

        #region xem chi tiết giao dịch lô
        public DataTable BatchViewDetail(string batchRef)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@batchref";
                p1.Value = batchRef;
                p1.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromDataTable("IB_VIEWDETAILLOGBATCH", p1);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Chuyen lo giao dich cung ngan hang
        public DataSet BatchTransfer(DataTable batchTable, string tranDesc, string userID, string Account, string TotalAmount, string totalFee, string authenType, string authenCode, string ccyid, DataTable tbldocument, string contracttype, ref string errorCode, ref string errorDesc)
        {
            try
            {

                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000499");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, tranDesc);
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.BATCHTABLE, batchTable);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, "IB000208");
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, Account);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, TotalAmount);
                hasInput.Add(SmartPortal.Constant.IPC.FEE, totalFee);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTTYPE, contracttype);
                if (tbldocument != null)
                {
                    hasInput.Add(SmartPortal.Constant.IPC.DOCUMENT, tbldocument);
                }
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authenType);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, authenCode);


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

        #region MAP TRANTYPE
        public DataSet LoadMapTrantype(string TRANTYPECORE, string LANGID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000028");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "map trantype");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANTYPECORE, TRANTYPECORE);
                hasInput.Add(SmartPortal.Constant.IPC.LANGID, LANGID);

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

        #region Search Feedback
        public DataTable GetFeedback(string FEEDID, string IPCTRANSID, string USERID, string STATUS, string CONTRACTNO, string TITLE)
        {
            DataTable iRead;

            SqlParameter p0 = new SqlParameter();
            p0.ParameterName = "@FEEDID";
            p0.Value = FEEDID;
            p0.SqlDbType = SqlDbType.VarChar;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@IPCTRANSID";
            p1.Value = IPCTRANSID;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@USERID";
            p2.Value = USERID;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@STATUS";
            p3.Value = STATUS;
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@CONTRACTNO";
            p4.Value = CONTRACTNO;
            p4.SqlDbType = SqlDbType.VarChar;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@TITLE";
            p5.Value = TITLE;
            p5.SqlDbType = SqlDbType.NVarChar;



            iRead = DataAccess.GetFromDataTable("EBA_FEEDBACK_SEARCH", p0, p1, p2, p3, p4, p5);

            return iRead;
        }
        #endregion

        #region Delete Feedback
        public int DeleteFeedBack(string FEEDID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@FEEDID";
                p1.Value = FEEDID;
                p1.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("EBA_FEEDBACK_DELETE", p1);

                if (strErr == 1)
                {
                    return strErr;
                    //throw new BusinessExeption("Unable Approve category");
                }
                else
                {
                    return strErr;
                }
                return strErr;
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
        #endregion

        #region insert Feedback
        public int InsertFeedBack(string IPCTRANSID, string USERID, string TITLE, string CONTENT, string COMMENT, string STATUS)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@IPCTRANSID";
                p1.Value = IPCTRANSID;
                p1.SqlDbType = SqlDbType.VarChar;


                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@USERID";
                p2.Value = USERID;
                p2.SqlDbType = SqlDbType.VarChar;


                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@TITLE";
                p3.Value = TITLE;
                p3.SqlDbType = SqlDbType.NVarChar;


                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@CONTENT";
                p4.Value = CONTENT;
                p4.SqlDbType = SqlDbType.NVarChar;


                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@COMMENT";
                p5.Value = COMMENT;
                p5.SqlDbType = SqlDbType.NVarChar;


                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@STATUS";
                p6.Value = STATUS;
                p6.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("EBA_FEEFBACK_INSERT", p1, p2, p3, p4, p5, p6);

                if (strErr == 0)
                {
                    return strErr;
                    //throw new BusinessExeption("Unable Approve category");
                }
                else
                {
                    return strErr;
                }
                return strErr;
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
        #endregion

        #region Update Feedback
        public int CommentFeedBack(string FEEDID, string COMMENT, string STATUS)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@FEEDID";
                p1.Value = FEEDID;
                p1.SqlDbType = SqlDbType.VarChar;


                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@COMMENT";
                p2.Value = COMMENT;
                p2.SqlDbType = SqlDbType.NVarChar;


                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@STATUS";
                p3.Value = STATUS;
                p3.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("EBA_FEEDBACK_UPDATE", p1, p2, p3);

                if (strErr == 0)
                {
                    return strErr;
                    //throw new BusinessExeption("Unable Approve category");
                }
                else
                {
                    return strErr;
                }
                return strErr;
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
        #endregion

        public DataTable CheckExistTrancode(string userid, string trancode, string ccyid)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@USERID";
            p1.Value = userid;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p1.ParameterName = "@TRANCODE";
            p1.Value = trancode;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p1.ParameterName = "@CCYID";
            p1.Value = ccyid;
            p1.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("CHECKEXISTTRANCODE", p1, p2, p3);

            return iRead;
        }
        public DataTable CheckMinAmount(string ID, string MinAmount)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@ID";
            p1.Value = ID;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@MINAMOUNT";
            p2.Value = MinAmount;
            p2.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IB_FDPRODUCTONLINE_CHECKMINAMOUNT", p1, p2);

            return iRead;
        }
        public DataTable GetNotify(string ConfigName, string DeviceType = "")
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@CONFIGNAME";
            p1.Value = ConfigName;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@DEVICETYPE";
            p2.Value = DeviceType;
            p2.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IB_GETCONFIG", p1, p2);

            return iRead;
        }

        #region E-TOPUP
        public DataSet ETopup_GetTecoByPhoneNumber(string phoneNumber, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                object[] para = new object[0];
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBETOPUPCHECKPHONE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, phoneNumber);

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

        public Hashtable RegisterEWallet(string UserID, string AccountNo, string senderName, string EWCode, string EWName, string phoneNumber, string debitBrachID, string authenType, string authenCode, string Desc)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000037");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, AccountNo);
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                input.Add(SmartPortal.Constant.IPC.EWCODE, EWCode);
                input.Add(SmartPortal.Constant.IPC.EWNAME, EWName);
                input.Add(SmartPortal.Constant.IPC.PHONE, phoneNumber);
                input.Add(SmartPortal.Constant.IPC.DEBITBRACHID, debitBrachID);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, Desc);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authenType);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authenCode);
                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        //QuanNN
        public DataSet GetApprovalTranByTranID(string tranID, string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBGETAPPRTRANBYID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get transaction information by tranid");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranID);
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
        public DataTable GetInforsenMail(string sTranCode, string sTranID)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@TRANCODE";
            p1.Value = sTranCode;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@TRANID";
            p2.Value = sTranID;
            p2.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IB_GETINFORSENMAILBYTRANID", p1, p2);

            return iRead;
        }

        #region Validate Chuyen lo giao dich cung ngan hang
        public DataTable ValidateBatchTransfer(DataTable batchTable, string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBVALIDATEBATCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.BATCHTABLE, batchTable);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataTable dt = new DataTable();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    dt = (DataTable)hasOutput[SmartPortal.Constant.IPC.DATATABLE];
                    errorCode = "0";
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

        //TrungTQ ADD new
        #region GET DOCUMENT BY TRANID
        public DataSet GetDocumentByTranID(string tranID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETDOCUMENT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "get list document by transid");
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
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region
        public Hashtable CommonProcessTrans(Hashtable hasInput, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
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

        #region LAPNET
        public Hashtable OutgoingInquiry(string transferid, string fromuser, string fromaccount, string tomember, string toaccount, ref string errorCode, ref string errorDesc)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB_OUTGOINGINQUIRY");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add("TRANSFERID", transferid);
                input.Add("FROMUSER", fromuser);
                input.Add(SmartPortal.Constant.IPC.SENDERACCOUNT, fromaccount);
                input.Add("TOMEMBER", tomember);
                input.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, toaccount);
                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Hashtable OutgoingTransfer(string transferid, string trancode, string UserID, string authenType, string authenCode, string SenderAccount, string trandesc, string sendername, string receivername, string receiveracctno, string receiverbank, string amount, string fee, string ccyid, DataTable tbldocument, string contracttype, ref string errorCode, ref string errorDesc)
        {
            Hashtable input = new Hashtable();
            Hashtable Output = new Hashtable();
            try
            {
                input.Add("TRANSFERID", transferid);
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, trancode);
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, trandesc);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authenType);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authenCode);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, receiveracctno);
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, sendername);
                input.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receivername);
                input.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                input.Add(SmartPortal.Constant.IPC.FEE, fee);
                input.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                input.Add(SmartPortal.Constant.IPC.CONTRACTTYPE, contracttype);
                if (tbldocument != null)
                {
                    input.Add(SmartPortal.Constant.IPC.DOCUMENT, tbldocument);
                }
                input.Add("TOBANK", receiverbank);
                input.Add("FROMBANK", "PSVB");

                Output = RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
                if (Output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                }
                else
                {
                    errorCode = Output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = Output[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Output;
        }
        #endregion
    }
}
