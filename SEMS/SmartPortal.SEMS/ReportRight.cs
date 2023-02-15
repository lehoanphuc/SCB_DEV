using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;

namespace SmartPortal.SEMS
{
    public class ReportRightBLL
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

                return DataAccess.GetFromDataTable("ReportRight_Load", p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //insert
        public int Insert(int roleID, string ReportID, bool flag)
        {
            try
            {
                int strErr = 0;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@roleid";
                p.Value = roleID;
                p.SqlDbType = SqlDbType.Int;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@reportid";
                p1.Value = ReportID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@flag";
                p2.Value = flag;
                p2.SqlDbType = SqlDbType.Bit;

                strErr = DataAccess.Execute("ReportRight_Insert", p, p1, p2);
                if (strErr == 0)
                {
                    throw new Exception();
                }
                else
                {
                    return strErr;
                }
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
       
    }
}
