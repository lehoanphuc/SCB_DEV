using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SmartPortal.SEMS
{
    public class UserApprovalLimit
    {
        #region SEARCH CORP USER APPROVAL BY CONDITION
        public DataSet GetCMUserApprovalByCondition(string userID, string fullName, string contractNo, string transaction, string currency, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCMUSERAPPBYCON");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.FULLNAME, fullName);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);
                hasInput.Add(SmartPortal.Constant.IPC.TRANSACTION, transaction);
                hasInput.Add(SmartPortal.Constant.IPC.CURRENCY, currency);
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
        #endregion

        #region ADD CORP USER APPROVAL LIMIT
        public DataSet AddCMUserApprovalLimit(DataTable dtUser, ref string errorCode, ref string errorDesc)
        {
            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            //add key into input
            hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCMUSERAPPLMADD");
            hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
            object[] userLimitArgs = new object[2];
            int i = 0;
            userLimitArgs[i++] = "SEMS_CM_USERAPPROVALLIMIT_INSERT";//store;
            userLimitArgs[i++] = dtUser;
            hasInput.Add("SEMSCMUSERAPPROVALLIMITADDMULTI", userLimitArgs);

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
        #endregion

        #region GET CM USER APPROVAL BY USERID
        public DataSet GetCMUserApprovalLimitByUserID(string userID, string tranCode, string currency, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCMUSERAPPBYID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, tranCode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, currency);

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

        #region UPDATE USER APPROVAL LIMIT
        public DataSet UpdateUserApprovalLimit(string userID, string tranCode, string ccyid, double tranLimit, double totalLimit, int countLimit, string userModified, string dateModified, ref string errorCode, ref string errorDesc)
        {
            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            //add key into input
            hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERAPPLMUPDATE");
            hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
            hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
            hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, tranCode);
            hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
            hasInput.Add(SmartPortal.Constant.IPC.TRANLIMIT, tranLimit);
            hasInput.Add(SmartPortal.Constant.IPC.TOTALLIMITDAY, totalLimit);
            hasInput.Add(SmartPortal.Constant.IPC.COUNTLIMIT, countLimit);
            hasInput.Add(SmartPortal.Constant.IPC.USERMODIFIED, userModified);
            hasInput.Add(SmartPortal.Constant.IPC.DATEMODIFIED, dateModified);

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
        #endregion

        #region DELETE USER APPROVAL LIMIT
        public DataSet DeleteUserApprovalLimit(string userID, string trancode, string currency, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSUSERAPPLMDEL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, currency);

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
