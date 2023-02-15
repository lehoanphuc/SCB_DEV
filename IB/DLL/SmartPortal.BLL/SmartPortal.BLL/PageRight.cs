using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;
using SmartPortal.ExceptionCollection;

namespace SmartPortal.BLL
{
    public class PageRightBLL
    {
        /// <summary>
        /// Load Page Right
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataTable Load(int roleID)
        {
            try
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@roleid";
                p.Value = roleID;
                p.SqlDbType = SqlDbType.Int;

                return DataAccess.GetFromDataTable("PageRight_Load", p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //insert
        public int Insert(int roleID, string pageID, bool flag)
        {
            try
            {
                int strErr = 0;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@roleid";
                p.Value = roleID;
                p.SqlDbType = SqlDbType.Int;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@pageid";
                p1.Value = pageID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@flag";
                p2.Value = flag;
                p2.SqlDbType = SqlDbType.Bit;

                strErr= DataAccess.Execute("PageRight_Insert", p, p1, p2);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert PageRight");
                }
                else
                {
                    return strErr;
                }
            }
            catch (BusinessExeption bex)
            {
                throw bex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //insert page mac dinh
        public int InsertPageDefault(int roleID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@roleid";
                p.Value = roleID;
                p.SqlDbType = SqlDbType.Int;


                strErr = DataAccess.Execute("PageRight_InsertPageDefault", p);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert PageRight Default");
                }
                else
                {
                    return strErr;
                }
            }
            catch (BusinessExeption bex)
            {
                throw bex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
