using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;
using SmartPortal.Model;

namespace SmartPortal.BLL
{
    public class ErrorBLL
    {
        /// <summary>
        /// Load message error from error code
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="langID"></param>
        /// <returns></returns>
        public ErrorCodeModel Load(int errorCode,string langID)
        {
            try
            {
                IDataReader iRead;
                ErrorCodeModel EM = new ErrorCodeModel();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@errorcode";
                p.Value = errorCode;
                p.SqlDbType = SqlDbType.Int;

                iRead = DataAccess.GetFromReader("ErrorCode_Load", p, p1);

                while (iRead.Read())
                {
                    EM.ErrorCode = int.Parse(iRead["ErrorCode"].ToString());
                    EM.ErrorDesc=iRead["ErrorDesc"].ToString();
                }
                iRead.Close();
                return EM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
