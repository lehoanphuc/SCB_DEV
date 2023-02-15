using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;
using SmartPortal.ExceptionCollection;

namespace SmartPortal.BLL
{
    public class MenuRightBLL
    {
        /// <summary>
        /// Load Menu Right
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataTable Load(int roleID)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@roleid";
                p1.Value = roleID;
                p1.SqlDbType = SqlDbType.Int;

                return DataAccess.GetFromDataTable("MenuRight_Load", p1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertMenuDefault(int roleID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@RoleID";
                p.Value = roleID;
                p.SqlDbType = SqlDbType.Int;


                strErr = DataAccess.Execute("MenuRight_InsertMenuDefault", p);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert MenuRight Default");
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
        //insert
        public int Insert(int roleID, string menuID, bool flag)
        {
            try
            {
                int strErr = 0;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@roleid";
                p.Value = roleID;
                p.SqlDbType = SqlDbType.Int;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@menuid";
                p1.Value = menuID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@flag";
                p2.Value = flag;
                p2.SqlDbType = SqlDbType.Bit;

                strErr= DataAccess.Execute("MenuRight_Insert", p, p1, p2);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert MenuRight");
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
