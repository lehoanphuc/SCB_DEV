using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartPortal.SEMS
{

    public class ContractLevel
    {
        public string IPCERRORCODE = "";
        public string IPCERRORDESC = "";
        public string IPCRECORDCOUNT = "0";
        private SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
        public  DataSet GetAllContractLevel(string ContractLevelCode, string ContractLevelName, string Status, ref string IPCERRORCODE , ref string IPCERRORDESC )
        {

            try
            {
                object[] searchCONTRACT_LEVEL = new object[] { ContractLevelCode, ContractLevelName, Status};
                DataSet ds = new DataSet();
                ds = _service.common("SEMS_ALL_CONTRACT_LV", searchCONTRACT_LEVEL, ref IPCERRORCODE, ref IPCERRORDESC);

                if (!IPCERRORCODE.Equals("0"))
                {
                    if (IPCERRORCODE.Equals("9999"))
                        throw new Exception(IPCERRORDESC);
                    else
                        throw new IPCException(IPC.ERRORCODE.IPC);
                }
                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet SearchAllContractLevel(string ContractLevelCode, string ContractLevelName, string Status, int pageindex, int pagesize, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                object[] searchCONTRACT_LEVEL = new object[] { ContractLevelCode, ContractLevelName, Status, pageindex, pagesize};
                DataSet ds = new DataSet();
                ds = _service.common("SEMS_CONTRACT_LEVEL", searchCONTRACT_LEVEL, ref IPCERRORCODE, ref IPCERRORDESC);
                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet GetContractLevel(string ContractLevelCode, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                object[] viewCONTRACT_LEVEL = new object[] { ContractLevelCode };
                DataSet ds = new DataSet();
                ds = _service.common("SEMS_CONTRACT_LV_VIE", viewCONTRACT_LEVEL, ref IPCERRORCODE, ref IPCERRORDESC);
                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet DeleteContractLevel(string ContractLevelid, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                object[] delCONTRACT_LEVEL = new object[] { ContractLevelid };
                DataSet ds = new DataSet();
                ds = _service.common("SEMS_CONTRACT_LV_DEL", delCONTRACT_LEVEL, ref IPCERRORCODE, ref IPCERRORDESC);
                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet InsertContractLevel(string ContractLevelCode, string ContractLevelName, string Status, string UserCreate, int Order, int Priority, string Condition, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                object[] insertCONTRACT_LEVEL = new object[] { ContractLevelCode, ContractLevelName, Status, UserCreate, Order, Priority, Condition};
                DataSet ds = new DataSet();
                ds = _service.common("SEMS_CONTRACT_LV_INS", insertCONTRACT_LEVEL, ref IPCERRORCODE, ref IPCERRORDESC);
                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet UpdateContractLevel(string ContractLevelID, string ContractLevelCode, string ContractLevelName, string Status, string UserUpdate, int Order, int Priority, string Condition, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                object[] updateCONTRACT_LEVEL = new object[] { ContractLevelID, ContractLevelCode, ContractLevelName, Status, UserUpdate, Order, Priority, Condition };
                DataSet ds = new DataSet();
                ds = _service.common("SEMS_CONTRACT_LV_UPD", updateCONTRACT_LEVEL, ref IPCERRORCODE, ref IPCERRORDESC);
                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }
    }
}
