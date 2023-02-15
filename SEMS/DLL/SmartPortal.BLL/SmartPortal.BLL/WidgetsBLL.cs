using System;
using System.Collections.Generic;

using System.Text;
using SmartPortal.DAL;
using SmartPortal.Model;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.ExceptionCollection;

namespace SmartPortal.BLL
{
    public class WidgetsBLL
    {
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="langID"></param>
        /// <returns></returns>
        public List<WidgetsModel> Load(string langID)
        {
            try
            {
                List<WidgetsModel> lstWM = new List<WidgetsModel>();

                IDataReader iRead;

                 SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@keyword";
                p2.Value = "";

                iRead = DataAccess.GetFromReader("Widgets_Load", p1,p2);

                while (iRead.Read())
                {
                    WidgetsModel WM = new WidgetsModel();
                    WM.WidgetID = iRead["WidgetID"].ToString();
                    WM.WidgetTitle = iRead["WidgetTitle"].ToString();

                    lstWM.Add(WM);
                }
                iRead.Close();
                return lstWM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load Reader
        /// </summary>
        /// <param name="langID"></param>
        /// <returns></returns>
        public DataTable LoadReader(string langID)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@keyword";
                p2.Value = "";

                return DataAccess.GetFromDataTable("Widgets_Load", p1,p2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable LoadReader(string langID,string keyword)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@keyword";
                p2.Value = keyword;

                return DataAccess.GetFromDataTable("Widgets_Load", p1, p2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load data into datatable
        /// </summary>
        /// <param name="langID"></param>
        /// <returns></returns>
        public DataTable LoadDataTable(string langID,string keyword)
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

                return DataAccess.GetFromDataTable("Widgets_Load", p1,p2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //insert
        public int Insert(string widgetid,string langID, string title, string link,string author,string iconpath, bool enableTheme, bool showTitle,bool isPublish)
        {
            try
            {
                int strErr = 0;

                SqlParameter p0 = new SqlParameter();
                p0.ParameterName = "@widgetid";
                p0.Value = widgetid;
                p0.SqlDbType = SqlDbType.VarChar;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@title";
                p2.Value = title;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@link";
                p3.Value = link;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@author";
                p4.Value = author;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@iconpath";
                p5.Value = iconpath;
                p5.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@enabletheme";
                p6.Value = enableTheme;
                p6.SqlDbType = SqlDbType.Bit;

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@showtitle";
                p7.Value = showTitle;
                p7.SqlDbType = SqlDbType.Bit;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@ispublish";
                p8.Value = isPublish;
                p8.SqlDbType = SqlDbType.Bit;

               strErr= DataAccess.Execute("Widgets_Insert",p0, p1, p2, p3, p4, p5, p6, p7, p8);
               if (strErr == 0)
               {
                   throw new BusinessExeption("Unable Insert Widget");
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
        //update
        public int Update(string widgetID,string langID, string title, string link, string author, string iconpath, bool enableTheme, bool showTitle, bool isPublish,DateTime datemodified)
        {
            try
            {
                int strErr = 0;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@widgetid";
                p.Value = widgetID;
                p.SqlDbType = SqlDbType.VarChar;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@title";
                p2.Value = title;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@link";
                p3.Value = link;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@author";
                p4.Value = author;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@iconpath";
                p5.Value = iconpath;
                p5.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@enabletheme";
                p6.Value = enableTheme;
                p6.SqlDbType = SqlDbType.Bit;

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@showtitle";
                p7.Value = showTitle;
                p7.SqlDbType = SqlDbType.Bit;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@ispublish";
                p8.Value = isPublish;
                p8.SqlDbType = SqlDbType.Bit;

                SqlParameter p9 = new SqlParameter();
                p9.ParameterName = "@datemodified";
                p9.Value = datemodified;
                p9.SqlDbType = SqlDbType.DateTime;

                strErr= DataAccess.Execute("Widgets_Update",p, p1, p2, p3, p4, p5, p6, p7, p8,p9);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Update Widget");
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
        /// Delete widget
        /// </summary>
        /// <param name="widgetID"></param>
        public int Delete(string widgetID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@widgetid";
                p.Value = widgetID;
                p.SqlDbType = SqlDbType.VarChar;

                strErr= DataAccess.Execute("Widgets_Delete", p);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Delete Widget");
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
        /// Get Widget By ID
        /// </summary>
        /// <param name="widgetID"></param>
        /// <returns></returns>
        public WidgetsModel GetWidgetByID(string widgetID)
        {
            try
            {
                WidgetsModel WM = new WidgetsModel();

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@widgetid";
                p.Value = widgetID;
                p.SqlDbType = SqlDbType.VarChar;

                //SqlParameter p1 = new SqlParameter();
                //p1.ParameterName = "@languageid";
                //p1.Value = langID;
                //p1.SqlDbType = SqlDbType.VarChar;

                IDataReader iRead = DataAccess.GetFromReader("Widgets_GetWidgetByID", p);

                while (iRead.Read())
                {
                    WM.WidgetID=iRead["WidgetID"].ToString();
                    //WM.WidgetTitle = iRead["WidgetTitle"].ToString();
                    WM.WidgetLink = iRead["WidgetLink"].ToString();
                    WM.IconPath = iRead["IconPath"].ToString();
                    WM.EnableTheme = bool.Parse(iRead["EnableTheme"].ToString());
                    WM.ShowTitle = bool.Parse(iRead["ShowTitle"].ToString());
                    WM.IsPublish = bool.Parse(iRead["IsPublish"].ToString());
                    WM.UserModified = iRead["UserModified"].ToString();
                    WM.DateModified = iRead["DateModified"].ToString();
                }
                iRead.Close();
                return WM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get WidgetTitleByID
        /// </summary>
        /// <param name="widgetID"></param>
        /// <param name="langID"></param>
        /// <returns></returns>
        public WidgetsModel GetWidgetTitleByID(string widgetID, string langID)
        {
            try
            {
                WidgetsModel WM = new WidgetsModel();

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@widgetid";
                p.Value = widgetID;
                p.SqlDbType = SqlDbType.VarChar;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;

                IDataReader iRead = DataAccess.GetFromReader("Widgets_GetWidgetTitleByID", p,p1);

                while (iRead.Read())
                {
                    WM.WidgetID = iRead["WidgetID"].ToString();
                    WM.WidgetTitle = iRead["WidgetTitle"].ToString();
                    WM.LangID = iRead["LangID"].ToString();
                }
                iRead.Close();
                return WM;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="langid"></param>
        /// <returns></returns>
        public DataTable Search(string keyword, string langid)
        {
            try
            {
                 SqlParameter p = new SqlParameter();
                p.ParameterName = "@keyword";
                p.Value = keyword;

                 SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langid;

                return DataAccess.GetFromDataTable("Widgets_Search",p,p1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Translate
        /// </summary>
        /// <param name="widgetID"></param>
        public int Translate(string widgetID,string langID,string title)
        {
            try
            {
                int strErr = 0;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@widgetid";
                p.Value = widgetID;
                p.SqlDbType = SqlDbType.VarChar;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@title";
                p2.Value = title;
                p2.SqlDbType = SqlDbType.NVarChar;

                strErr= DataAccess.Execute("Widgets_Translate", p,p1,p2);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Translate Language Widget");
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
