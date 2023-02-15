using SmartPortal.ExceptionCollection;
using System.Collections.Generic;
using System.Data;

namespace SmartPortal.SEMS
{

    public class GroupDefinition
    {
        public string IPCERRORCODE = "";
        public string IPCERRORDESC = "";
        public string IPCRECORDCOUNT = "0";
        private SmartPortal.SEMS.Common _common;
        public GroupDefinition()
        {
            _common = new SmartPortal.SEMS.Common();
        }

        private Dictionary<object, object> setParaToSearch(string groupID, string module, string acName)
        {
            Dictionary<object, object> search = new Dictionary<object, object>();
            search.Add("GRPID", groupID);
            search.Add("MODULE", module);
            search.Add("ACGRPDEF", acName);
            return search;
        }

        private Dictionary<object, object> setParaToView(string groupID)
        {
            Dictionary<object, object> search = new Dictionary<object, object>();
            search.Add("GRPID", groupID);
            return search;
        }

        private Dictionary<object, object> setParaToInsert(string groupID, string module, string acName)
        {
            Dictionary<object, object> Insert = new Dictionary<object, object>();
            Insert.Add("GRPID", groupID);
            Insert.Add("MODULE", module);
            Insert.Add("ACGRPDEF", acName);
            
            return Insert;
        }

        private Dictionary<object, object> setParaToUpdate(string groupID, string module, string acName)
        {
            Dictionary<object, object> Insert = new Dictionary<object, object>();
            Insert.Add("GRPID", groupID);
            Insert.Add("MODULE", module);
            Insert.Add("ACGRPDEF", acName);

            return Insert;
        }
        public  DataSet GetAllGroupDefinition(string groupId, string module, string acName, ref string IPCERRORCODE , ref string IPCERRORDESC )
        {

            try
            {
                Dictionary<object, object> searchGroupDefinition = setParaToSearch(groupId, module, acName);
                DataSet ds = new DataSet();
                ds = _common.CallStore("SEMS_ACC_GRPDEF", searchGroupDefinition, "Search group definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet GetGroupDefinition(string groupID, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                Dictionary<object, object> viewGroupDefinition = setParaToView(groupID);
                DataSet ds = new DataSet();
                ds = _common.CallStore("SEMS_ACC_GRPDEF_GET", viewGroupDefinition, "View group definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet DeleteContractLevel(string ContractLevelCode, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                Dictionary<object, object> viewGroupDefinition = setParaToView(ContractLevelCode);
                DataSet ds = new DataSet();
                ds = _common.CallStore("SEMS_ACC_GRPDEF_DEL", viewGroupDefinition, "Delete group definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet InsertGroupDefinition(string groupID, string module, string acName, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                Dictionary<object, object> insertGroupdefinition = setParaToInsert(groupID, module, acName);
                DataSet ds = new DataSet();
                ds = _common.CallStore("SEMS_ACC_GRPDEF_INS", insertGroupdefinition, "Insert group definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }

        public DataSet UpdateGroupDefinition(string groupID, string module, string acName, ref string IPCERRORCODE, ref string IPCERRORDESC)
        {

            try
            {
                Dictionary<object, object> updateGroupDefinition = setParaToUpdate(groupID, module, acName);
                DataSet ds = new DataSet();
                ds = _common.CallStore("SEMS_ACC_GRPDEF_UP", updateGroupDefinition, "Update Group Definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

                return ds;
            }
            catch (IPCException ex)
            {
                throw ex;
            }
        }
    }
}
