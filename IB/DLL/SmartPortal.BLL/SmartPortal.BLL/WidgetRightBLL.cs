using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;
using SmartPortal.ExceptionCollection;

namespace SmartPortal.BLL
{
    public class WidgetRightBLL
    {
        /// <summary>
        /// Get Widget In Page For Role
        /// </summary>
        /// <param name="portalID"></param>
        /// <param name="pageID"></param>
        /// <param name="userName"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public DataTable GetWidgetInPageForRole(string pageID,string userName,string lang,string serviceID)
        {
            try
            {
                DataTable iRead;

                 SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@pageid";
                p2.Value = pageID;
                p2.SqlDbType = SqlDbType.VarChar;

                 SqlParameter p3 = new SqlParameter();
                 p3.ParameterName = "@userName";
                 p3.Value = userName;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@lang";
                p4.Value = lang;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@serviceID";
                p5.Value = serviceID;
                p5.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromDataTable("WidgetRight_GetModuleInPageForRole", p2, p3,p4,p5);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="widgetPageID"></param>
        /// <param name="flag"></param>
        public int Insert(int roleID, string widgetPageID,Boolean flag)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@roleid";
                p1.Value = roleID;
                p1.SqlDbType = SqlDbType.Int;

                 SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@widgetpageid";
                p2.Value = widgetPageID;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@flag";
                p3.Value = flag;
                p3.SqlDbType = SqlDbType.Bit;

                strErr= DataAccess.Execute("WidgetRight_Insert",p1,p2,p3);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert WidgetRight");
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
