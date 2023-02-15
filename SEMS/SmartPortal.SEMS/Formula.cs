using SmartPortal.Constant;
using SmartPortal.RemotingServices;
using System;
using System.Collections;
using System.Data;

namespace SmartPortal.SEMS
{
    public class Formula
    {
        public DataSet GetAll(string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();


                //add key into input
                hasInput.Add(IPC.IPCTRANCODE, "SEMSGETFORMULAFIELDNAME");
                hasInput.Add(IPC.SOURCETRANREF, IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.SOURCEID, IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get all formula field name");

                hasInput.Add(IPC.STATUS, status);


                hasOutput = AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();
                errorCode = hasOutput[IPC.IPCERRORCODE].ToString();
                errorDesc = hasOutput[IPC.IPCERRORDESC].ToString();
                if (hasOutput[IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[IPC.DATASET];
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
