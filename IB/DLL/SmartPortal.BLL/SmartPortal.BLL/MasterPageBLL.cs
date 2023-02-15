
using System;
using System.Collections.Generic;

using System.Text;
using SmartPortal.Model;
using System.Data;
using SmartPortal.DAL;

namespace SmartPortal.BLL
{
    public class MasterPageBLL
    {
        /// <summary>
        /// Load All MasterPage
        /// </summary>
        /// <returns></returns>
        public List<MasterPageModel> Load()
        {
            try
            {
                List<MasterPageModel> lstPM = new List<MasterPageModel>();
                IDataReader iRead;

                iRead = DataAccess.GetFromReader("MasterPage_Load", null);
                while (iRead.Read())
                {
                    MasterPageModel PM = new MasterPageModel();
                    PM.MasterPageID = int.Parse(iRead["MasterPageID"].ToString());
                    PM.MasterPageName = iRead["MasterPageName"].ToString();
                    PM.MasterPagePath = iRead["MasterPagePath"].ToString();
                    PM.MasterPageImage = iRead["MasterPageImage"].ToString();
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
