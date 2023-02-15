using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using SmartPortal.DAL;
using System.Data.SqlClient;
using System.Collections;

namespace SmartPortal.SEMS
{
    public class PartnerBank
    {
        #region Partner Bank
        public DataTable LoadParnerBankByID(string bankid)
        {           
            return DataAccess.FillDataTable("SEMS_PARTNERBANKLIST_LOADBYID", bankid);
        }
        public DataTable LoadParnerBankAll(string bankname = "", string status = "")
        {
            return DataAccess.FillDataTable("SEMS_PARTNERBANKLIST_LOADALL", bankname, status);
        }
        public int InsertBank(string Bankname, string bankcode, string determination, string IsManual)
        {
            int strErr = 0;

            strErr = DataAccess.Execute("SEMS_PARTNERBANK_ADDNEW", GetSQLParam("BANKNAME", Bankname, SqlDbType.NVarChar), GetSQLParam("BANKCODE", bankcode, SqlDbType.VarChar), GetSQLParam("DETERMINATION", determination, SqlDbType.VarChar), GetSQLParam("IsManual", IsManual, SqlDbType.VarChar));

            if (strErr == 0)
            {
                return strErr;
            }
            else
            {
                return strErr;
            }
        }
        public int EditBank(int Bankid,string Bankname, string status, string determination, string bankcode, string ismanual)
        {
            int strErr = 0;

            strErr = DataAccess.Execute("SEMS_PARTNERBANK_EDIT", GetSQLParam("BANKID", Bankid, SqlDbType.Int), GetSQLParam("BANKNAME", Bankname, SqlDbType.NVarChar), GetSQLParam("STATUS", status, SqlDbType.VarChar), GetSQLParam("DETERMINATION", determination, SqlDbType.VarChar), GetSQLParam("BANKCODE", bankcode, SqlDbType.VarChar), GetSQLParam("ISMANUAL", ismanual, SqlDbType.VarChar));

            if (strErr == 0)
            {
                return strErr;
            }
            else
            {
                return strErr;
            }
        }
        #endregion

        #region Branch Partner Bank
        public DataTable LoadBranchByCondition(string bankid, string branchcode, string branchname, string ismanual = "")
        {
            return DataAccess.FillDataTable("SEMS_LOADBRANCHBYCONDITION", bankid, branchcode, branchname, ismanual);
        }
        public DataTable LoadBranchByID(string branchid)
        {
            return DataAccess.FillDataTable("SEMS_LOADBRANCHBYID", branchid);
        }
        public int InsertBranchPartner(string branchname, string branchcode, string bankid, string oribranchcode)
        {
            int strErr = 0;

            strErr = DataAccess.Execute("SEMS_INSERTBRANCHPARTNER", GetSQLParam("BRANCHNAME", branchname, SqlDbType.VarChar), GetSQLParam("BANKID", bankid, SqlDbType.Int), GetSQLParam("BRANCHCODE", branchcode, SqlDbType.VarChar), GetSQLParam("ORIBRANCHCODE", oribranchcode, SqlDbType.VarChar));

            if (strErr == 0)
            {
                return strErr;
            }
            else
            {
                return strErr;
            }
        }
        public int DeleteBranchPartner(string branchname, string branchcode, string bankid)
        {
            int strErr = 0;

            strErr = DataAccess.Execute("SEMS_DELETEBRANCHPARTNER", GetSQLParam("BRANCHNAME", branchname, SqlDbType.VarChar), GetSQLParam("BANKID", bankid, SqlDbType.Int), GetSQLParam("BRANCHCODE", branchcode, SqlDbType.VarChar));

            if (strErr == 0)
            {
                return strErr;
            }
            else
            {
                return strErr;
            }
        }
        public int EditBranchPartner(string branchid, string bankid, string branchcode, string branchname, string status, string oribranchcode)
        {
            int strErr = 0;

            strErr = DataAccess.Execute("SEMS_EDITBRANCHPARTNER", GetSQLParam("BRANCHID", branchid, SqlDbType.Int), GetSQLParam("BANKID", bankid, SqlDbType.Int), GetSQLParam("BRANCHCODE", branchcode, SqlDbType.VarChar), GetSQLParam("BRANCHNAME", branchname, SqlDbType.VarChar), GetSQLParam("STATUS", status, SqlDbType.VarChar), GetSQLParam("ORIBRANCODE", oribranchcode, SqlDbType.VarChar));

            if (strErr == 0)
            {
                return strErr;
            }
            else
            {
                return strErr;
            }
        }
        #endregion

        private SqlParameter GetSQLParam(string name, object value, SqlDbType type)
        {
            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@" + name;
            p1.Value = value;
            p1.SqlDbType = type;
            return p1;
        }
        public DataSet getListBankforInterBank(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETBANKINTER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
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

        #region LAPNET
        public DataSet getListBankfor247(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "LAPNET_GETLISTBANK");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
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
    }
}
