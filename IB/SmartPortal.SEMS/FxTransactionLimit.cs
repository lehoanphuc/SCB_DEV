using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SmartPortal.Constant;

namespace SmartPortal.SEMS
{
    public class FxTransactionLimit
    {
        #region Search PRODUCTLIMIT by condition
        public DataSet GetListFXTransactionLimitByCondition(string sContractNo, string sFullName, string sTranCode, string sFromCCY, string sToCCY, string allowforeign, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSFXTRANLMSEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get list FX Transaction Limit");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, sContractNo);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, sTranCode);
                hasInput.Add(SmartPortal.Constant.IPC.FULLNAME, sFullName);
                hasInput.Add(SmartPortal.Constant.IPC.FROMCCYID, sFromCCY);
                hasInput.Add(SmartPortal.Constant.IPC.TOCCYID, sToCCY);
                hasInput.Add(SmartPortal.Constant.IPC.ALLOWFOREIGN, allowforeign);

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
        #region FX Transaction Limit Delete
        public DataSet FXLimitDelete(DataTable dtFXLimit, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "FXTRANLIMITDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                AddKeyValue(ref hasInput, "EBA_SP_FX_TRANSACTION_LIMIT_DELETE", "DELETEFXTRANSACTIONLIMIT", dtFXLimit);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        private void AddKeyValue(ref Hashtable hashtable, string storename, string key, DataTable dataTable)
        {
            object[] objs = new object[2];
            int i = 0;
            objs[i++] = storename;//Store Name
            objs[i++] = dataTable;//Data Table
            hashtable.Add(key, objs);//Key Value
        }
        private DataSet CheckOutput(Hashtable hasOutput, ref string errorCode, ref string errorDesc)
        {
            DataSet ds = new DataSet();
            if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
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
        #region FX Transaction Limit Delete
        public DataSet FXLimitEdit(DataTable dtFXLimit, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "FXTRANLIMITEDIT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                AddKeyValue(ref hasInput, "EBA_SP_FX_TRANSACTION_LIMIT_EDIT", "EDITFXTRANSACTIONLIMIT", dtFXLimit);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 
        public DataSet GetListContract(string sContractNo, string sFullName, ref string errorCode, ref string errorDesc)
        {
            try
            {

                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSFXCONTRACTSEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get list Contract");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, sContractNo);
                hasInput.Add(SmartPortal.Constant.IPC.FULLNAME, sFullName);

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
        #region FX Transaction Limit Insert
        public DataSet FXLimitInsert(DataTable dtFXLimit, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "FXTRANLIMITINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                AddKeyValue(ref hasInput, "EBA_SP_FX_TRANSACTION_LIMIT_INSERT", "INSERTFXTRANSACTIONLIMIT", dtFXLimit);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Check Fx Transfer Limit
        public DataSet CheckFxLimit(string sUserID, string sTranCode, string sFromCCYID, string sToCCYID, decimal dAmount, string receiverAccount, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CHECKFXLIMIT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.IB);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check limit FX transfer");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, sUserID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, sTranCode);
                hasInput.Add(SmartPortal.Constant.IPC.FROMCCYID, sFromCCYID);
                hasInput.Add(SmartPortal.Constant.IPC.TOCCYID, sToCCYID);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, dAmount);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, receiverAccount);

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
        #region Get Fx Rate
        public Hashtable GetFXRate(string sFromCCYID, string sToCCYID, decimal dAmount, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000406");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.IB);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get rate for FX transfer");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, dAmount);
                hasInput.Add(SmartPortal.Constant.IPC.FROMCCYID, sFromCCYID);
                hasInput.Add(SmartPortal.Constant.IPC.TOCCYID, sToCCYID);

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
