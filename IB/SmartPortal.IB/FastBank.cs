using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using SmartPortal.DAL;

namespace SmartPortal.IB
{
    public class FastBank
    {
        string errorcode = "";
        string errordesc = "";
        public static void InsertTransaction(string order_code, string shopcode, string amount, string ccyid, string trandesc, string returnurl, string sessionid, string secretcode, ref string errorcode, ref string errordesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000212");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.ORDERCODE, order_code);
                hasInput.Add(SmartPortal.Constant.IPC.SHOPCODE, shopcode);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, trandesc);
                hasInput.Add(SmartPortal.Constant.IPC.RETURNURL, returnurl);
                hasInput.Add(SmartPortal.Constant.IPC.SESSIONID, sessionid);
                hasInput.Add(SmartPortal.Constant.IPC.SECRETCODE, secretcode);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                errorcode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errordesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet GetShopInfo(string order_code, ref string errorcode, ref string errordesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataSet ds =new DataSet();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000213");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.ORDERCODE, order_code);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                errorcode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errordesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();

                if(errorcode=="0")
                {
                    ds=(DataSet)hasOutput[SmartPortal.Constant.IPC.DATARESULT];
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Hashtable TransferFastBanking(string order_code,string UserID, string SenderAccount, string senderName, string debitBrachID, string authentype, string authencode,string desc)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000214");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.ORDERCODE, order_code);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                input.Add(SmartPortal.Constant.IPC.DEBITBRACHID, debitBrachID);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, desc);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);

                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static void UpdateTransactionResult(string order_code, string errorcode,string errordesc,string status)
        {
            try
            {
                DataTable tblFD = new DataTable();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@ORDERCODE";
                p1.Value = order_code;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@ERRORCODE";
                p2.Value = errorcode;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@ERRORDESC";
                p3.Value = errordesc;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@STATUS";
                p4.Value = status;
                p4.SqlDbType = SqlDbType.VarChar;

                DataAccess.Execute("FB_TRANSACTION_UPDATERESULT", p1, p2, p3, p4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetShopPasswod(string fid)
        {
            try
            {
                DataTable tblFD = new DataTable();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@SHOPID";
                p1.Value = fid;
                p1.SqlDbType = SqlDbType.VarChar;

                tblFD = DataAccess.GetFromDataTable("FB_GETSHOPPASSWORD", p1);
                return tblFD.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetShopPasswodByOrddercode(string ordercode)
        {
            try
            {
                DataTable tblFD = new DataTable();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@ORDERCODE";
                p1.Value = ordercode;
                p1.SqlDbType = SqlDbType.VarChar;

                tblFD = DataAccess.GetFromDataTable("FB_GETSHOPPASSWORD_BYORDERCODE", p1);
                return tblFD.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
