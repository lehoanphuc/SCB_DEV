using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using SmartPortal.DAL;
using System.Data.SqlClient;

namespace SmartPortal.SEMS
{
    public class Bank
    {
        #region Load tất cả các ngân hàng
        public DataTable Load()
        {           
            DataTable iRead;

            iRead = DataAccess.GetFromDataTable("SEMS_BANKLIST_LOAD", null);


            return iRead;
        }
        #endregion

        #region Load ngân hàng theo mã ngân hàng
        public DataTable LoadByBankCode(string bankCode)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@BANKCODE";
            p1.Value = bankCode;
            p1.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("SEMS_EBA_BANKLIST_LOADBYBANKCODE", p1);


            return iRead;
        }
        #endregion

        #region Load tất cả ngân hàng khác
        public DataTable LoadAllBank(string bankid, string bankname)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@BANKID";
            p1.Value = bankid;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@BANKNAME";
            p2.Value = bankname;
            p2.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("EBA_BANK_SEARCH", p1,p2);


            return iRead;
        }
        #endregion

        public int InsertBank(string Bankname)
        { 
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@BANKNAME";
                p1.Value = Bankname;
                p1.SqlDbType = SqlDbType.NVarChar;

                strErr = DataAccess.Execute("EBA_BANK_INSERT", p1);

                if (strErr == 0)
                {
                    return strErr;
                    //throw new BusinessExeption("Unable Approve category");
                }
                else
                {
                    return strErr;
                }
                return strErr;
            }
            //catch (BusinessExeption bex)
            //{
            //    throw bex;
            //}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditBank(string Bankid,string Bankname)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@BANKID";
                p1.Value = Bankid;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@BANKNAME";
                p2.Value = Bankname;
                p2.SqlDbType = SqlDbType.NVarChar;

                strErr = DataAccess.Execute("EBA_BANK_UPDATE", p1,p2);

                if (strErr == 0)
                {
                    return strErr;
                    //throw new BusinessExeption("Unable Approve category");
                }
                else
                {
                    return strErr;
                }
                return strErr;
            }
            //catch (BusinessExeption bex)
            //{
            //    throw bex;
            //}
            catch (Exception ex)
            {
                throw ex;
            }
        }   

        public int DeleteBank(string Bankid)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@BANKID";
                p1.Value = Bankid;
                p1.SqlDbType = SqlDbType.VarChar;


                strErr = DataAccess.Execute("EBA_BANK_DELETE", p1);

                if (strErr == 0)
                {
                    return strErr;
                    //throw new BusinessExeption("Unable Approve category");
                }
                else
                {
                    return strErr;
                }
                return strErr;
            }
            //catch (BusinessExeption bex)
            //{
            //    throw bex;
            //}
            catch (Exception ex)
            {
                throw ex;
            }
        }

            #region Load Remittance
            public DataTable LoadRemittance()
            {
                DataTable iRead;

                iRead = DataAccess.GetFromDataTable("EBA_REMITTANCE_ALL", null);


                return iRead;
            }
            #endregion

            #region Load chi nhánh fòng giang dịch

            public DataTable LoadChildBank(string BANKCODE,string CITYCODE,string BANKNAME,string REMITTANCE,string BANKID )
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@BANKCODE";
                p1.Value = BANKCODE;
                p1.SqlDbType = SqlDbType.Text;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@CITYCODE";
                p2.Value = CITYCODE;
                p2.SqlDbType = SqlDbType.Text;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@BANKNAME";
                p3.Value = BANKNAME;
                p3.SqlDbType = SqlDbType.Text;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@REMITTANCE";
                p4.Value = REMITTANCE;
                p4.SqlDbType = SqlDbType.Text;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@BANKID";
                p5.Value = BANKID;
                p5.SqlDbType = SqlDbType.Text;

                iRead = DataAccess.GetFromDataTable("EBA_BANKLIST_SEARCH", p1, p2, p3, p4, p5);


                return iRead;
            }
            #endregion

            public int InsertChildBank(string BANKCODE,string CITYCODE,string BANKNAME,string DESCRIPTION,string REMITTANCE,string BANKID)
            {
 
                try
                {
                    int strErr = 0;

                    SqlParameter p1 = new SqlParameter();
                    p1.ParameterName = "@BANKCODE";
                    p1.Value = BANKCODE;
                    p1.SqlDbType = SqlDbType.VarChar;

                    SqlParameter p2 = new SqlParameter();
                    p2.ParameterName = "@CITYCODE";
                    p2.Value = CITYCODE;
                    p2.SqlDbType = SqlDbType.VarChar;

                    SqlParameter p3 = new SqlParameter();
                    p3.ParameterName = "@BANKNAME";
                    p3.Value = BANKNAME;
                    p3.SqlDbType = SqlDbType.NVarChar;

                    SqlParameter p4 = new SqlParameter();
                    p4.ParameterName = "@DESCRIPTION";
                    p4.Value = DESCRIPTION;
                    p4.SqlDbType = SqlDbType.NVarChar;

                    SqlParameter p5 = new SqlParameter();
                    p5.ParameterName = "@REMITTANCE";
                    p5.Value = REMITTANCE;
                    p5.SqlDbType = SqlDbType.VarChar;

                    SqlParameter p6 = new SqlParameter();
                    p6.ParameterName = "@BANKID";
                    p6.Value = BANKID;
                    p6.SqlDbType = SqlDbType.VarChar;
                    strErr = DataAccess.Execute("EBA_BANKLIST_INSERT", p1,p2,p3,p4,p5,p6);

                    if (strErr == 0)
                    {
                        return strErr;
                        //throw new BusinessExeption("Unable Approve category");
                    }
                    else
                    {
                        return strErr;
                    }
                    return strErr;
                }
                //catch (BusinessExeption bex)
                //{
                //    throw bex;
                //}
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public int EditChildBank(string BANKCODE, string CITYCODE, string BANKNAME, string DESCRIPTION, string REMITTANCE, string BANKID)
            {

                try
                {
                    int strErr = 0;

                    SqlParameter p1 = new SqlParameter();
                    p1.ParameterName = "@BANKCODE";
                    p1.Value = BANKCODE;
                    p1.SqlDbType = SqlDbType.VarChar;

                    SqlParameter p2 = new SqlParameter();
                    p2.ParameterName = "@CITYCODE";
                    p2.Value = CITYCODE;
                    p2.SqlDbType = SqlDbType.VarChar;

                    SqlParameter p3 = new SqlParameter();
                    p3.ParameterName = "@BANKNAME";
                    p3.Value = BANKNAME;
                    p3.SqlDbType = SqlDbType.NVarChar;

                    SqlParameter p4 = new SqlParameter();
                    p4.ParameterName = "@DESCRIPTION";
                    p4.Value = DESCRIPTION;
                    p4.SqlDbType = SqlDbType.NVarChar;

                    SqlParameter p5 = new SqlParameter();
                    p5.ParameterName = "@REMITTANCE";
                    p5.Value = REMITTANCE;
                    p5.SqlDbType = SqlDbType.VarChar;

                    SqlParameter p6 = new SqlParameter();
                    p6.ParameterName = "@BANKID";
                    p6.Value = BANKID;
                    p6.SqlDbType = SqlDbType.VarChar;
                    strErr = DataAccess.Execute("EBA_BANKLIST_UPDATE", p1,p2,p3,p4,p5,p6);

                    if (strErr == 0)
                    {
                        return strErr;
                        //throw new BusinessExeption("Unable Approve category");
                    }
                    else
                    {
                        return strErr;
                    }
                    return strErr;
                }
                //catch (BusinessExeption bex)
                //{
                //    throw bex;
                //}
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public int DeleteChildBank(string BANKCODE)
            {
                try
                {
                    int strErr = 0;

                    SqlParameter p1 = new SqlParameter();
                    p1.ParameterName = "@BANKCODE";
                    p1.Value = BANKCODE;
                    p1.SqlDbType = SqlDbType.VarChar;


                    strErr = DataAccess.Execute("EBA_BANKLIST_DELETE", p1);

                    if (strErr == 0)
                    {
                        return strErr;
                        //throw new BusinessExeption("Unable Approve category");
                    }
                    else
                    {
                        return strErr;
                    }
                    return strErr;
                }
                //catch (BusinessExeption bex)
                //{
                //    throw bex;
                //}
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            #region Delete root bank

            public DataTable DeleteRootBank(string BANKID)
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@BANKID";
                p1.Value = BANKID;
                p1.SqlDbType = SqlDbType.Text;


                iRead = DataAccess.GetFromDataTable("EBA_BANK_DELETE", p1);


                return iRead;
            }
            #endregion
    }
}
