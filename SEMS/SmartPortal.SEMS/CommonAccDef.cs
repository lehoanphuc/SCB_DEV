using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using System;
using System.Collections.Generic;
using System.Data;
namespace SmartPortal.SEMS
{

    public class CommonAccDef
    {
        public string IPCERRORCODE = "";
        public string IPCERRORDESC = "";
        public string IPCRECORDCOUNT = "0";
        private SmartPortal.SEMS.Common _common;
        public CommonAccDef()
        {
            _common = new SmartPortal.SEMS.Common();
        }

        private Dictionary<object, object> setParaToSearch(string acname, string acno, string refacno1, string refacno2)
        {
            Dictionary<object, object> search = new Dictionary<object, object>();
            search.Add("ACNAME", acname);
            search.Add("ACNO", acno);
            search.Add("REFACNO", refacno1);
            search.Add("REFACNO2", refacno2);
            return search;
        }

        private Dictionary<object, object> setParaToView(string acname)
        {
            Dictionary<object, object> search = new Dictionary<object, object>();
            search.Add("ACNAME", acname);
            return search;
        }

        private Dictionary<object, object> setParaToInsert(string acname, string acno, string refacno1, string refacno2)
        {
            Dictionary<object, object> Insert = new Dictionary<object, object>();
            Insert.Add("ACNAME", acname);
            Insert.Add("ACNO", acno);
            Insert.Add("REFACNO", refacno1);
            Insert.Add("REFACNO2", refacno2);
            return Insert;
        }

        private Dictionary<object, object> setParaToUpdate(string acname, string acno, string refacno1, string refacno2)
        {
            Dictionary<object, object> Insert = new Dictionary<object, object>();
            Insert.Add("ACNAME", acname);
            Insert.Add("ACNO", acno);
            Insert.Add("REFACNO", refacno1);
            Insert.Add("REFACNO2", refacno2);

            return Insert;
        }
        //public  DataSet GetAllCommonAccountDefinition(string acname, string acno, string refacno1, string refacno2, ref string IPCERRORCODE , ref string IPCERRORDESC )
        //{

        //    try
        //    {
        //        //List<CashBackModel> Groups = new List<CashBackModel>();
        //        Dictionary<object, object> searchFunction = setParaToSearch(acname, acno, refacno1,refacno2);
        //        //object[] searchCONTRACT_LEVEL = new object[] { ContractLevelCode, ContractLevelName, Status };
        //        DataSet ds = new DataSet();
        //        ds = _common.CallStore("SEMS_ACC_GRPDEF", searchFunction, "Search common account definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

        //        return ds;
        //    }
        //    catch (IPCException ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataSet GetCommonAccountDefinition(string acname, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                //List<CommonAccDefModel> Groups = new List<CommonAccDefModel>();
                Dictionary<object, object> viewFunction = setParaToView(acname);
                //object[] searchCONTRACT_LEVEL = new object[] { ContractLevelCode, ContractLevelName, Status };
                DataSet ds = new DataSet();
                ds = _common.CallStore("SEMS_ACCOM_GET", viewFunction, "View Common Account Definition Details", "N", ref IPCERRORCODE, ref IPCERRORDESC);

                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet DeleteCommonAccountDefinition(string acname, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                //List<CommonAccDefModel> ContractLevels = new List<CommonAccDefModel>();
                Dictionary<object, object> viewFuntion = setParaToView(acname);
                //object[] searchCONTRACT_LEVEL = new object[] { ContractLevelCode, ContractLevelName, Status };
                DataSet ds = new DataSet();
                ds = _common.CallStore("SEMS_ACCOM_DEL", viewFuntion, "Delete common account definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet InsertCommonAccountDefinition(string acname, string acno, string refacno1, string refacno2, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                //List<GroupDefinitionModule> Groups = new List<GroupDefinitionModule>();
                Dictionary<object, object> insertfunction = setParaToInsert(acname,acno,refacno1,refacno2);
                DataSet ds = new DataSet();
                ds = _common.CallStore("SEMS_ACCOM_ADD", insertfunction, "Insert common account definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet UpdateGroupDefinition(string acname, string acno, string refacno1, string refacno2, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                //List<ContractLevelModel> Groups = new List<ContractLevelModel>();
                Dictionary<object, object> updateGroupDefinition = setParaToUpdate(acname,acno, refacno1,refacno2);
                DataSet ds = new DataSet();
                ds = _common.CallStore("SEMS_ACCOM_UPDATE", updateGroupDefinition, "Update Group Definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }
    }
}
