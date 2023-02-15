using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SmartPortal.SEMS
{
    public class Print
    {
        public DataSet GetTemplacePrint(string tranid,string bankID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "KRCGETTEMPLATEPRINT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, Constant.IPC.SEMS);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load templage print");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranid);
                hasInput.Add(SmartPortal.Constant.IPC.BANKID, bankID);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATARESULT];
                }
                else
                {
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetDataPrint(string tranid,ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "KRCGETTRANPRINT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, Constant.IPC.SEMS);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load data print transaction detail");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANID, tranid);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataTable ds = new DataTable();

                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataTable)hasOutput[SmartPortal.Constant.IPC.DATARESULT];
                }
                else
                {
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
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
