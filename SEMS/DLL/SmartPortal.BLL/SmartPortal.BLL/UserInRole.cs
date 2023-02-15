using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;
using SmartPortal.DAL;
using System.Data;
using SmartPortal.ExceptionCollection;

namespace SmartPortal.BLL
{
    public class UserInRole
    {
        public DataTable GetALLRoleOfUser(string userName)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@username";
                p1.Value = userName;
                p1.SqlDbType = SqlDbType.VarChar;
               

                iRead = DataAccess.GetFromDataTable("SMP_GetListRoleUser", p1);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Insert(int roleID, string userName, Boolean flag)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@roleid";
                p1.Value = roleID;
                p1.SqlDbType = SqlDbType.Int;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@username";
                p2.Value = userName;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@flag";
                p3.Value = flag;
                p3.SqlDbType = SqlDbType.Bit;

                strErr= DataAccess.Execute("UserInRole_InsertForUser", p1, p2, p3);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert UserInRole");
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
