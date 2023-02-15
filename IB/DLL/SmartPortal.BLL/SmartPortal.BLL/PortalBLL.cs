using System;
using System.Collections.Generic;

using System.Text;
using SmartPortal.Model;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;


namespace SmartPortal.BLL
{
    public class PortalBLL
    {
        /// <summary>
        /// Load
        /// </summary>
        /// <returns></returns>
        public List<PortalModel> Load()
        {
            try
            {
                List<PortalModel> lstPM = new List<PortalModel>();
                IDataReader iRead;

                iRead = DataAccess.GetFromReader("SmartPortal_Load", null);
                while (iRead.Read())
                {
                    PortalModel PM = new PortalModel();
                    PM.PortalID = int.Parse(iRead["PortalID"].ToString());
                    PM.PortalName = iRead["PortalName"].ToString();
                    PM.PortalDescription = iRead["PortalDescription"].ToString();
                    lstPM.Add(PM);
                }
                iRead.Close();
                return lstPM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
