using System;
using System.Collections.Generic;

using System.Text;
using SmartPortal.DAL;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;

namespace SmartPortal.BLL
{
    public class WidgetPageBLL
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="widgetPageID"></param>
        /// <param name="portalID"></param>
        /// <param name="pageID"></param>
        /// <param name="widgetID"></param>
        /// <param name="position"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int Insert(string widgetPageID,string pageID, string widgetID,string position,int order)
        {
            try
            {
                int strErr = 0;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@pageid";
                p2.Value = pageID;
                p2.SqlDbType = SqlDbType.VarChar;

                 SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@widgetid";
                p3.Value = widgetID;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@position";
                p4.Value = position;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@order";
                p5.Value = order;
                p5.SqlDbType = SqlDbType.Int;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@widgetpageid";
                p6.Value = widgetPageID;
                p6.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("WidgetPage_Insert", p6, p2, p3, p4, p5);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert WidgetPage");
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

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="portalID"></param>
        /// <param name="pageID"></param>
        /// <param name="widgetID"></param>
        /// <param name="position"></param>
        /// <param name="order"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public int InsertNew(string widgetPageID, string pageID, string widgetID, string position, int order, int roleID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p0 = new SqlParameter();
                p0.ParameterName = "@widgetpageid";
                p0.Value = widgetPageID;
                p0.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@pageid";
                p2.Value = pageID;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@widgetid";
                p3.Value = widgetID;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@position";
                p4.Value = position;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@order";
                p5.Value = order;
                p5.SqlDbType = SqlDbType.Int;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@roleid";
                p6.Value = roleID;
                p6.SqlDbType = SqlDbType.Int;

                strErr= DataAccess.Execute("WidgetPage_InsertNew",p0, p2, p3, p4, p5,p6);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert WidgetPage");
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

        /// <summary>
        /// Remove Widget Page
        /// </summary>
        /// <param name="widgetPageID"></param>
        /// <returns></returns>
        public int RemoveWidgetInPage(string widgetPageID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@widgetpageid";
                p6.Value = widgetPageID;
                p6.SqlDbType = SqlDbType.VarChar;

                strErr= DataAccess.Execute("WidgetPage_RemoveWidgetInPage", p6);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Remove WidgetPage");
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

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="widgetPageID"></param>
        /// <returns></returns>
        public WidgetPageModel GetByID(string widgetPageID)
        {
            try
            {
                WidgetPageModel wpM = new WidgetPageModel();
                IDataReader iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@widgetpageid";
                p1.Value = widgetPageID;
                p1.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromReader("WidgetPage_GetByID", p1);

                while (iRead.Read())
                {
                    wpM.PortalID = int.Parse(iRead["PortalID"].ToString());
                    wpM.PageID = iRead["PageID"].ToString();
                }
                iRead.Close();
                return wpM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
