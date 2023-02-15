using System;
using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;

namespace SmartPortal.IB
{
    public class Common
    {
        public Hashtable CallStore(string ipcTrancode, Dictionary<object, object> para, string transdescription, string reversql, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasOutput = new Hashtable();
                Hashtable hasInput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, ipcTrancode);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.IB);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, transdescription);
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, reversql);

                if (para != null || para.Count != 0)
                {
                    foreach (KeyValuePair<object, object> item in para)
                    {
                        hasInput.Add(item.Key, item.Value);
                    }

                }

                //hasInput.Add(SmartPortal.Constant.IPC.PARAMDATATYPE, dataType);
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
            catch (Exception e)
            {
                throw (e);
            }
        }

        public DataSet common(string ipcTrancode, object[] para, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasOutput = new Hashtable();
                Hashtable hasInput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, ipcTrancode);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);

                if (para != null || para.Length > 0)
                {
                    hasInput.Add("PARA", para);

                }

                //hasInput.Add(SmartPortal.Constant.IPC.PARAMDATATYPE, dataType);
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
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}
