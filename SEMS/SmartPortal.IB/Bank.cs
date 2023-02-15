using System;
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

        public DataTable GetFeeV2(string userID, string ipctrancode, string amount, string senderAcctno,string receiverAcctNo, string CCYID, string CITY)
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
    }
}
