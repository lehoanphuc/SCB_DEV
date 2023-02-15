using SmartPortal.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SmartPortal.SEMS
{
   public class CashOutByCashCode
    {
        public DataSet checkCashCode(string cashcode,ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_CHECKCASHCODE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("CASHCODE", cashcode);
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


        public Hashtable getCashInfo(string cashcode, string phone, string amount, ref string errorCode,
            ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS_INFORCASHCODE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("CASHCODE", cashcode);
                hasInput.Add("RCVPHONENO", phone);
                hasInput.Add("AMOUNT", amount);
                hasInput.Add("CASHOUT", "CASHOUT");
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, SmartPortal.Constant.IPC.SEMS);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, "MMK");
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
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet applyCashCode(string trancode , string phone, string cashcode,string amount, ref string errorCode, ref string errorDesc, ref  string tranid)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add("PHONETRAN", phone);
                hasInput.Add("CASHCODE", cashcode);
                hasInput.Add("PHONENO", phone);
                hasInput.Add("RCVPHONENO", phone);
                hasInput.Add("SENDERPHONE",phone);
                hasInput.Add("AMOUNT", amount);
                hasInput.Add("CCYID", "MMK");
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC,"Cash out by cash code ");
             
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();
                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    tranid = hasOutput[SmartPortal.Constant.IPC.TRANREF].ToString();
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
    }
}
