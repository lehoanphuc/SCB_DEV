using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;
using System;

namespace SmartPortal.BLL
{
    public class SubSystemBLL
    {
        /// <summary>
        /// Load
        /// </summary>
        /// <returns></returns>
        public DataTable Load()
        {
            try
            {
                return DataAccess.GetFromDataTable("SubSystem_Load", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
