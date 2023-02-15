using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartPortal.SEMS
{

    public class AccountOfGroupDefinition
    {
        public string IPCERRORCODE = "";
        public string IPCERRORDESC = "";
        public string IPCRECORDCOUNT = "0";
        private SmartPortal.SEMS.Common _common;
        public AccountOfGroupDefinition()
        {
            _common = new SmartPortal.SEMS.Common();
        }

        //private Dictionary<object, object> setParaToSearch(string groupID, string acno, string acgrpname)
        //{
        //    Dictionary<object, object> search = new Dictionary<object, object>();
        //    search.Add("GRPID", groupID);
        //    search.Add("ACNO", acno);
        //    search.Add("AC_GRP_NAME", acgrpname);
        //    return search;
        //}

        private Dictionary<object, object> setParaToView(string groupID,string acgrpname)
        {
            Dictionary<object, object> search = new Dictionary<object, object>();
            search.Add("GRPID", groupID);
            search.Add("ACGRPNAME", acgrpname);
            return search;
        }

        private Dictionary<object, object> setParaToInsert(string groupID, string acno, string acgrpname)
        {
            Dictionary<object, object> Insert = new Dictionary<object, object>();
            Insert.Add("GRPID", groupID);
            Insert.Add("ACNO", acno);
            Insert.Add("ACGRPNAME", acgrpname);
            
            return Insert;
        }

        private Dictionary<object, object> setParaToUpdate(string groupID, string acno, string acgrpname)
        {
            Dictionary<object, object> Insert = new Dictionary<object, object>();
            Insert.Add("GRPID", groupID);
            Insert.Add("ACNO", acno);
            Insert.Add("ACGRPNAME", acgrpname);

            return Insert;
        }
        //public  DataSet GetAllGroupDefinition(string groupId, string module, string acName, ref string IPCERRORCODE , ref string IPCERRORDESC )
        //{

        //    try
        //    {
        //        List<GroupDefinitionModule> Groups = new List<GroupDefinitionModule>();
        //        Dictionary<object, object> searchGroupDefinition = setParaToSearch(groupId, module, acName);
        //        //object[] searchCONTRACT_LEVEL = new object[] { ContractLevelCode, ContractLevelName, Status };
        //        DataSet ds = new DataSet();
        //        ds = _common.CallStore("SEMS_ACC_GRPDEF", searchGroupDefinition, "Search group definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

        //        if (!IPCERRORCODE.Equals("0"))
        //        {
        //            if (IPCERRORCODE.Equals("9999"))
        //                throw new Exception(IPCERRORDESC);
        //            else
        //                throw new IPCException(IPC.ERRORCODE.IPC);
        //        }
        //        return ds;
        //    }
        //    catch (IPCException ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataSet GetFunction(string groupID,string acgrpname, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                Dictionary<object, object> viewFunction = setParaToView(groupID, acgrpname);
                DataSet ds = new DataSet();
                ds = _common.CallStore("SEMS_ACGRP_GET", viewFunction, "View account of group definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        //public DataSet DeleteFunction(string groupID, string acgrpname, ref string IPCERRORCODE, ref string IPCERRORDESC)
        //{

        //    try
        //    {
        //        Dictionary<object, object> delFunction = setParaToView(groupID, acgrpname);
        //        DataSet ds = new DataSet();
        //        ds = _common.CallStore("SEMS_ACGRP_DEL", delFunction, "Delete account of group definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

        //        if (!IPCERRORCODE.Equals("0"))
        //        {
        //            if (IPCERRORCODE.Equals("9999"))
        //                throw new Exception(IPCERRORDESC);
        //            else
        //                throw new IPCException(IPC.ERRORCODE.IPC);
        //        }
        //        return ds;
        //    }
        //    catch (IPCException ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataSet InsertFunction(string groupID, string acno, string acgrpname, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                Dictionary<object, object> insFunction = setParaToInsert(groupID, acno, acgrpname);
                DataSet ds = new DataSet();
                ds = _common.CallStore("SEMS_ACGRP_INS", insFunction, "Insert account of group definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);
                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet UpdateFunction(string groupID, string acno, string acgrpname, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                Dictionary<object, object> upFunction = setParaToUpdate(groupID, acno, acgrpname);
                DataSet ds = new DataSet();
                ds = _common.CallStore("SEMS_ACGRP_UPDATE", upFunction, "Update account of group definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }
    }
}
