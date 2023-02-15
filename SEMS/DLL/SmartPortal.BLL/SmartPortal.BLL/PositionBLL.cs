using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;

namespace SmartPortal.BLL
{
    public class PositionBLL
    {
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="portalID"></param>
        /// <returns></returns>
        public DataTable Load(string portalID)
        {
            //search

            try
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@pageid";
                p.Value = portalID;
                p.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Position_Load", p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
