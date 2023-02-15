using SmartPortal.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SmartPortal.SEMS
{
    public class Common
    {
        public DataSet CallStore(string ipcTrancode, Dictionary<object, object> para, string transdescription, string reversql, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasOutput = new Hashtable();
                Hashtable hasInput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, ipcTrancode);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
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

        public Hashtable CallCore(string ipcTrancode, Dictionary<object, object> para, string transdescription, string reversql, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasOutput = new Hashtable();
                Hashtable hasInput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, ipcTrancode);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
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

        public DataSet GetSysAllCode(string cdname, string cdtable, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasOutput = new Hashtable();
                Hashtable hasInput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_SYS_ALLCODE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get data from table SYS_ALLCODE");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("CDNAME", cdname);
                hasInput.Add("CDTABLE", cdtable);

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
        public DataSet GetValueList(string value_name, string valuegrp, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasOutput = new Hashtable();
                Hashtable hasInput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_BO_GETVALUELIST");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get data from table ValueList");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add("VALUE_NAME", value_name);
                hasInput.Add("VALUEGRP", valuegrp);

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

        public DataSet GetPara(string group, string name, ref string errorCode, ref string errorDesc)
        {
            try
            {
                object[] para = new object[] { group, name };
                Hashtable hasOutput = new Hashtable();
                Hashtable hasInput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GET_PARAMETER");
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
        public DataTable GetCurrency()
        {
            try
            {
                return DataAccess.GetFromDataTable("SEMS_EBA_PRODUCT_GETALLCCYID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
