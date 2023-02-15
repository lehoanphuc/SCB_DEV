using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;

namespace SmartPortal.BLL
{
    public class HTMLWidgetBLL
    {
        /// <summary>
        /// Load HTMLWidget
        /// </summary>
        /// <param name="langID"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public DataTable Load(string langID , string keyword)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@keyword";
                p2.Value = keyword;
                p2.SqlDbType = SqlDbType.NVarChar;

                return DataAccess.GetFromDataTable("HTMLWidget_Load", p1,p2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load HTMLWidget By ID
        /// </summary>
        /// <param name="widgetID"></param>
        /// <param name="langID"></param>
        /// <returns></returns>
        public HTMLWidgetModel GetByID(int widgetID,string langID)
        {
            try
            {
                IDataReader iRead;
                HTMLWidgetModel HTMLWM = new HTMLWidgetModel();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@WidgetID";
                p1.Value = widgetID;
                p1.SqlDbType = SqlDbType.Int;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@langid";
                p2.Value = langID;
                p2.SqlDbType = SqlDbType.VarChar;
                

                iRead= DataAccess.GetFromReader("HTMLWidget_GetByID", p1, p2);
                while (iRead.Read())
                {
                    HTMLWM.WidgetID = int.Parse(iRead["WidgetID"].ToString());
                    HTMLWM.WidgetContent=iRead["WidgetContent"].ToString();
                    HTMLWM.UserModified = iRead["UserModified"].ToString();
                    HTMLWM.DateModified = iRead["DateModified"].ToString();
                }
                iRead.Close();

                return HTMLWM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="langID"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public int Insert(int widgetID, string content,string userCreated)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@widgetid";
                p1.Value = widgetID;
                p1.SqlDbType = SqlDbType.Int;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@WidgetContent";
                p2.Value = content;
                p2.SqlDbType = SqlDbType.NVarChar;


                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@UserCreated";
                p3.Value = userCreated;
                p3.SqlDbType = SqlDbType.VarChar;


                strErr = DataAccess.Execute("HTMLWidget_Insert", p1, p2, p3);

                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert HTMLWidget");
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
        /// Update
        /// </summary>
        /// <param name="langID"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public int Update(int widgetID, string content, string userModified,string dateModified)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@widgetid";
                p1.Value = widgetID;
                p1.SqlDbType = SqlDbType.Int;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@WidgetContent";
                p2.Value = content;
                p2.SqlDbType = SqlDbType.NVarChar;


                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@UserModified";
                p3.Value = userModified;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@DateModified";
                p4.Value = dateModified;
                p4.SqlDbType = SqlDbType.VarChar;


                strErr= DataAccess.Execute("HTMLWidget_Update", p1, p2, p3,p4);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Update HTMLWidget");
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
        /// Delete
        /// </summary>
        /// <param name="langID"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public int Delete(int widgetID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@widgetid";
                p1.Value = widgetID;
                p1.SqlDbType = SqlDbType.Int;


                strErr= DataAccess.Execute("HTMLWidget_Delete", p1);

                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Delete HTMLWidget");
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
