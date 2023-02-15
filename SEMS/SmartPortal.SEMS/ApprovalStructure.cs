using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using SmartPortal.DAL;
using SmartPortal.Constant;
using System.Reflection;
using SmartPortal.Model;

namespace SmartPortal.SEMS
{
    public class ApprovalStructure
    {
        #region Approval Structure GetAllByCondition
        public DataSet ApprovalStructureGetAllByCondition(ApprovalStructureBase structureSearch, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                AddKeyValue2(ref hasInput, structureSearch, structureSearch.Search(), "EBACMASGETALL");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetGroupByCondition(string contractno, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.IPCTRANCODE, "EBACMASGETALL");
                hasInput.Add("CONTRACTNO", contractno);
                hasInput.Add("RECPERPAGE", recPerPage);
                hasInput.Add("RECINDEX", recIndex);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Structure Detail
        public DataSet ApprovalStructureDetail(ApprovalStructureBase structure, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                AddKeyValue2(ref hasInput, structure, structure.Detail(), "EBACMASDETAIL");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        

        #region Approval Structure Delete
        public DataSet ApprovalStructureDelete(ApprovalStructureBase structure, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                AddKeyValue2(ref hasInput, structure, structure.Delete(), "EBACMASDELETE");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Structure Level GetAll
        public DataSet LevelGetAll(ASLevelModel level, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                AddKeyValue2(ref hasInput, level, level.GetAll(), "EBACMASLEVELGETALL");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Structure Group GetAll
        public DataSet GroupGetAll(ASGroupModel group, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                AddKeyValue2(ref hasInput, group, group.GetAll(), "EBACMASGROUPGETALL");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Structure Delete Mul
        public DataSet StructureDeleteMulti(DataTable dtStructure, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "EBACMASDELETEMULTI");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                object[] objs = new object[2];
                int i = 0;
                objs[i++] = "EBA_CM_Structure_Delete";//Store Name
                objs[i++] = dtStructure;//Data Table
                hasInput.Add("DELETEMULTISTRUCTURE", objs);//Key Value
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Structure Insert All
        public DataSet StructureInsertAll(DataTable dtGroups, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "EBACMASINSERTALL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                AddKeyValue(ref hasInput, "EBA_CM_AS_Group_Insert", "INSERTGROUPS", dtGroups);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Structure Update All
        public DataSet StructureUpdateAll(DataTable dtDel, DataTable dtGroups, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "EBACMASUPDATELVLGR");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                AddKeyValue(ref hasInput, "EBA_CM_AS_DelAll_LvlGroup_ByCondition", "DELETEALLLVLGR", dtDel);
                AddKeyValue(ref hasInput, "EBA_CM_AS_Group_Insert", "UPDATEGROUPS", dtGroups);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public DataTable AutoGenStructureID()
        {
            try
            {
                return DataAccess.GetFromDataTable("EBA_CM_AS_AUTOGENSTRUCTUREID", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsSuccess(string errorcode)
        {
            return errorcode.Equals("0") ? true : false;
        }
        private DataSet CheckOutput(Hashtable hasOutput, ref string errorCode, ref string errorDesc)
        {
            DataSet ds = new DataSet();
            if (IsSuccess(hasOutput[IPC.IPCERRORCODE].ToString()))
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
        private void AddKeyValue2(ref Hashtable ht, ClassBase ClassBase, PropertyInfo[] Props, string IPCTRANCODE, string SOURCEID = IPC.SOURCEIDVALUE)
        {
            if (ClassBase == null || Props == null || string.IsNullOrEmpty(IPCTRANCODE) || Props.Length == 0) return;
            if (ht == null)
            {
                ht = new Hashtable();
            }
            ClassBase.IPCTRANCODE = IPCTRANCODE;
            ClassBase.SOURCEID = SOURCEID;
            foreach (PropertyInfo prop in Props)
            {
                ht.Add(prop.Name, prop.GetValue(ClassBase, null));
            }
        }

        private void AddKeyValue(ref Hashtable hashtable, string storename, string key, DataTable dataTable)
        {
            object[] objs = new object[2];
            int i = 0;
            objs[i++] = storename;//Store Name
            objs[i++] = dataTable;//Data Table
            hashtable.Add(key, objs);//Key Value
        }
        //QuanNN 20190412 
        public DataSet GetContractGroupDetail(string contractNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.IPCTRANCODE, "EBACMASGROUPGETALL");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.STRUCTUREID, contractNo);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DeleteGroupContract(string contractNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.IPCTRANCODE, "EBADELETEGROUP");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.CONTRACTNO, contractNo);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DeleteMultiGroupContract(DataTable dtStructure, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "EBADELETEMULTIGROUP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                object[] objs = new object[2];
                int i = 0;
                objs[i++] = "EBA_CM_AS_DelAll_LvlGroup_ByCondition";//Store Name
                objs[i++] = dtStructure;//Data Table
                hasInput.Add("DELETEMULTISTRUCTURE", objs);//Key Value
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet CheckGroupBeforeDelete(string contractNo, string groupID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.IPCTRANCODE, "CMGROUPCHECKDELETE");
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.CONTRACTNO, contractNo);
                hasInput.Add(IPC.GROUPID, groupID);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
