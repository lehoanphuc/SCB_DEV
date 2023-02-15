using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;
using System.Data;
using SmartPortal.DAL;

namespace SmartPortal.BLL
{
    public class QuickLinkBLL
    {
        public DataTable Load(string langID)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;
               

                return DataAccess.GetFromDataTable("QuickLink_Load", p1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
