using System;
using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;

namespace SmartPortal.IB
{
    public class Bank
    {
        /// <summary>
        /// Lay danh sach ngan hang
        /// </summary>
        /// <returns></returns>
        public DataTable GetBank()
        {
            DataTable iRead;

            iRead = DataAccess.GetFromDataTable("IBS_EBA_BANK_SELECTALL", null);

            return iRead;
        }

        /// <summary>
        /// Lay ma chi nhanh
        /// </summary>
        /// <param name="bankCode"></param>
        /// <returns></returns>
        public DataTable GetBranchID(string bankCode)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@BANKCODE";
            p1.Value = bankCode;
            p1.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IPC_GETTRANSFEROUTBANKINFO", p1);

            return iRead;
        }

        /// <summary>
        /// Tinh phi
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="ipctrancode"></param>
        /// <param name="amount"></param>
        /// <param name="senderAcctno"></param>
        /// <param name="branchID"></param>
        /// <returns></returns>
        public DataTable GetFee(string userID, string ipctrancode, string amount, string senderAcctno, string branchID, string CCYID, string CITY)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@USERID";
            p1.Value = userID;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@IPCTRANCODE";
            p2.Value = ipctrancode;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@AMOUNT";
            p3.Value = amount;
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@FACCTNO";
            p4.Value = senderAcctno;
            p4.SqlDbType = SqlDbType.VarChar;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@TBRANCHID";
            p5.Value = branchID;
            p5.SqlDbType = SqlDbType.VarChar;

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@CCYID";
            p6.Value = CCYID;
            p6.SqlDbType = SqlDbType.VarChar;

            SqlParameter p7 = new SqlParameter();
            p7.ParameterName = "@TCITY";
            p7.Value = CITY;
            p7.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IPC_CALCULATORTRANSFEE", p1, p2, p3, p4, p5, p6, p7);

            return iRead;
        }

        public DataTable GetFeeV2(string userID, string ipctrancode, string amount, string senderAcctno, string receiverAcctNo, string CCYID, string CITY)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@USERID";
            p1.Value = userID;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@IPCTRANCODE";
            p2.Value = ipctrancode;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@AMOUNT";
            p3.Value = amount;
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@FACCTNO";
            p4.Value = senderAcctno;
            p4.SqlDbType = SqlDbType.VarChar;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@TACCTNO";
            p5.Value = receiverAcctNo;
            p5.SqlDbType = SqlDbType.VarChar;

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@CCYID";
            p6.Value = CCYID;
            p6.SqlDbType = SqlDbType.VarChar;

            SqlParameter p7 = new SqlParameter();
            p7.ParameterName = "@TCITY";
            p7.Value = CITY;
            p7.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IPC_CALCULATORTRANSFEEV2", p1, p2, p3, p4, p5, p6, p7);

            return iRead;
        }

        public DataTable GetWaterFee(string userID, string ipctrancode, string amount, string senderAcctno, string branchID, string CCYID, string CITY)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@USERID";
            p1.Value = userID;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@IPCTRANCODE";
            p2.Value = ipctrancode;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@AMOUNT";
            p3.Value = amount;
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@FACCTNO";
            p4.Value = senderAcctno;
            p4.SqlDbType = SqlDbType.VarChar;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@TBRANCHID";
            p5.Value = branchID;
            p5.SqlDbType = SqlDbType.VarChar;

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@CCYID";
            p6.Value = CCYID;
            p6.SqlDbType = SqlDbType.VarChar;

            SqlParameter p7 = new SqlParameter();
            p7.ParameterName = "@TCITY";
            p7.Value = CITY;
            p7.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IPC_CALCULATORWATERFEE", p1, p2, p3, p4, p5, p6, p7);

            return iRead;
        }
        public DataTable GetFeeOtherBank(int BranchID, string amount, string CCYID, string userid)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@BranchID";
            p1.Value = BranchID;
            p1.SqlDbType = SqlDbType.Int;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@AMOUNT";
            p2.Value = amount;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@CCYID";
            p3.Value = CCYID;
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@USERID";
            p4.Value = userid;
            p4.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IPC_CALCULATORTRANSFEEOTHERBANK", p1, p2, p3, p4);

            return iRead;
        }

        public DataTable GetFeeFx(string userID, string ipctrancode, string amount, string Famount, string senderAcctno, string receiverAcctNo, string FCCY, string TCCY)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@USERID";
            p1.Value = userID;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@IPCTRANCODE";
            p2.Value = ipctrancode;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@AMOUNT";
            p3.Value = amount;
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p9 = new SqlParameter();
            p9.ParameterName = "@FXAMOUNT";
            p9.Value = Famount;
            p9.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@FACCTNO";
            p4.Value = senderAcctno;
            p4.SqlDbType = SqlDbType.VarChar;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@TACCTNO";
            p5.Value = receiverAcctNo;
            p5.SqlDbType = SqlDbType.VarChar;

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@FCCY";
            p6.Value = FCCY;
            p6.SqlDbType = SqlDbType.VarChar;

            SqlParameter p7 = new SqlParameter();
            p7.ParameterName = "@TCCY";
            p7.Value = TCCY;
            p7.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IPC_CALCULATORTRANSFEE_FX", p1, p2, p3, p9, p4, p5, p6, p7);

            return iRead;
        }

        public DataSet GetBankbyCountry(string countryId, string ccyID, string lang, ref string errorcode, ref string errordesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataSet ds = new DataSet();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETBANKBYCOUNTRYID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "get list of bank");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add("COUNTRYID", countryId);
                hasInput.Add("CCYID", ccyID);
                hasInput.Add("LANG", lang);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorcode = "0";
                }
                else
                {
                    errorcode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errordesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                return ds;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public DataTable GetFeeWU(string userID, string ipctrancode, string amount, string senderAcctno, string branchID, string CCYID, string CITY, string Payoutcountry)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@USERID";
            p1.Value = userID;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@IPCTRANCODE";
            p2.Value = ipctrancode;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@AMOUNT";
            p3.Value = amount;
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@FACCTNO";
            p4.Value = senderAcctno;
            p4.SqlDbType = SqlDbType.VarChar;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@TACCTNO";
            p5.Value = branchID;
            p5.SqlDbType = SqlDbType.VarChar;

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@CCYID";
            p6.Value = CCYID;
            p6.SqlDbType = SqlDbType.VarChar;

            SqlParameter p7 = new SqlParameter();
            p7.ParameterName = "@TCITY";
            p7.Value = CITY;
            p7.SqlDbType = SqlDbType.VarChar;

            SqlParameter p8 = new SqlParameter();
            p7.ParameterName = "@COUNTRYCODE";
            p7.Value = Payoutcountry;
            p7.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("WU_CALCULATORTRANSFEEV2", p1, p2, p3, p4, p5, p6, p7, p8);

            return iRead;
        }
    }
}
