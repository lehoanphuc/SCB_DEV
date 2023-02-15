using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;
using System.Data;
using SmartPortal.DAL;
using SmartPortal.Model;

namespace SmartPortal.BLL
{
    public class GroupBLL
    {
        /// <summary>
        /// Load Info Group
        /// </summary>
        /// <returns></returns>
        public DataTable Load()
        {
            try
            {
                DataTable iRead = new DataTable();
                iRead = DataAccess.GetFromDataTable("Roles_GetAll", null);                
                return iRead;            
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        
    }
}
