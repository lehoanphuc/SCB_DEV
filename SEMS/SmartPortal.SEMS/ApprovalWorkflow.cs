using System;
using System.Data;
using System.Collections;
using SmartPortal.Constant;
using SmartPortal.Model;
using System.Reflection;
using SmartPortal.DAL;

namespace SmartPortal.SEMS
{
    public class ApprovalWorkflow
    {
        public DataTable AutoGenWorkflowID()
        {
            try
            {
                return DataAccess.GetFromDataTable("EBA_CM_AW_AUTOGENWORKFLOWID", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #region Approval Workflow Search
        public DataSet ApprovalWorkflowSearch(string TRANCODE, string CONTRACTNO, string ACCTNO, string CCYID, string NEEDAPPROVE, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.IPCTRANCODE, "EBACMAWSEARCH");
                hasInput.Add("TRANCODE", TRANCODE);
                hasInput.Add("CONTRACTNO", CONTRACTNO);
                hasInput.Add("ACCTNO", ACCTNO);
                hasInput.Add("CCYID", CCYID);
                hasInput.Add("NEEDAPPROVE", NEEDAPPROVE);
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

        #region Approval Workflow Get UserID
        public DataSet ApprovalWorkflowGetUserID(string contractno, string FULLNAME, string LICENSEID, string CUSTCODE, string HASGROUP, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.IPCTRANCODE, "EBACMAWGETUSERID");
                hasInput.Add("CONTRACTNO", contractno);
                hasInput.Add("FULLNAME", FULLNAME);
                hasInput.Add("LICENSEID", LICENSEID);
                hasInput.Add("CUSTCODE", CUSTCODE);
                hasInput.Add("HASGROUP", HASGROUP);
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

        #region Approval Workflow Get Acct
        public DataSet ApprovalWorkflowGetAcct(ApprovalWorkflowModel workflow, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                AddKeyValue2(ref hasInput, workflow, workflow.GetAcct(), "EBACMAWGETACCT");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Workflow Get Group
        public DataSet ApprovalWorkflowGetGroup(ApprovalWorkflowModel workflow, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                AddKeyValue2(ref hasInput, workflow, workflow.GetGroup(), "EBACMAWGETGROUP");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Workflow Detail
        public DataSet ApprovalWorkflowDetail(ApprovalWorkflowModel workflow, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                AddKeyValue2(ref hasInput, workflow, workflow.Detail(), "EBACMAWGETDETAIL");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Workflow Detail Get All
        public DataSet AWDetailGetAll(AWDetailModel workflowdetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                AddKeyValue2(ref hasInput, workflowdetail, workflowdetail.GetAll(), "EBACMAWDETAILGETALL");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Workflow Insert All
        public DataSet WorkflowInsertAll(DataTable dtWorkflow, DataTable dtDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "EBACMAWINSERTALL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                AddKeyValue(ref hasInput, "EBA_CM_AW_Insert", "INSERTWORKFLOW", dtWorkflow);
                AddKeyValue(ref hasInput, "EBA_CM_AW_Detail_Insert", "INSERTWORKFLOWDETAIL", dtDetail);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Workflow Delete
        public DataSet WorkflowDelete(string WorkflowID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "EBACNAWDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("WORKFLOWID", WorkflowID);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Workflow Update All
        public DataSet WorkflowUpdateAll(DataTable dtDel, DataTable dtWorkflow, DataTable dtDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "EBACMAWUPDATEALL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                AddKeyValue(ref hasInput, "EBA_CM_AW_Detail_DeleteAll", "DELALLDETAIL", dtDel);
                AddKeyValue(ref hasInput, "EBA_CM_AW_Insert", "INSERTWORKFLOW", dtWorkflow);
                AddKeyValue(ref hasInput, "EBA_CM_AW_Detail_Insert", "INSERTWORKFLOWDETAIL", dtDetail);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Approval Workflow Get AcctTran
        public DataSet ApprovalWorkflowGetAcctTran(ApprovalWorkflowModel workflow, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                AddKeyValue2(ref hasInput, workflow, workflow.GetAcct(), "EBACMAWGETACCTRAN");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                return CheckOutput(hasOutput, ref errorCode, ref errorDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private bool IsSuccess(string errorcode)
        {
            return errorcode.Equals("0") ? true : false;
        }
        private DataSet CheckOutput (Hashtable hasOutput, ref string errorCode, ref string errorDesc)
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

        public DataSet CheckWorkingWorkflow(string workflowID, string contractNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CHECKWORKINGWF");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Check working workflow");

                hasInput.Add(SmartPortal.Constant.IPC.WORKFLOWID, workflowID);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);


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

        #region GENARATE WORKFLOWID
        public string GENARATE_WORKFLOWID(string serviceid, string contractno, ref string errorcode, ref string errordesc)
        {
            try
            {
                string WORKFLOWID = string.Empty;
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGENWORKFLOWID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceid);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractno);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    WORKFLOWID = (string)hasOutput[SmartPortal.Constant.IPC.WORKFLOWID];
                    errorcode = "0";
                }
                else
                {
                    errorcode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errordesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return WORKFLOWID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
