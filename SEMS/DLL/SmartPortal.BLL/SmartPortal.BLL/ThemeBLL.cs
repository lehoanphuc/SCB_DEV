using System;
using System.Collections.Generic;

using System.Text;
using SmartPortal.Model;
using SmartPortal.DAL;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.ExceptionCollection;

namespace SmartPortal.BLL
{
    public class ThemeBLL
    {
        /// <summary>
        /// Load
        /// </summary>
        /// <returns></returns>
        public List<ThemeModel> Load()
        {
            try
            {
                List<ThemeModel> lstTM = new List<ThemeModel>();
                IDataReader iRead;

                iRead = DataAccess.GetFromReader("Theme_Load", null);
                while (iRead.Read())
                {
                    ThemeModel TM = new ThemeModel();
                    TM.ThemeID = int.Parse(iRead["ThemeID"].ToString());
                    TM.ThemeName= iRead["ThemeName"].ToString();
                    TM.ThemeDescription = iRead["ThemeDescription"].ToString();
                    TM.ImagePath = iRead["ImagePath"].ToString();
                    lstTM.Add(TM);
                }
                iRead.Close();
                return lstTM;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Load By ID
        /// </summary>
        /// <param name="themeID"></param>
        /// <returns></returns>
        public ThemeModel LoadByID(int themeID)
        {
            try
            {
                ThemeModel TM = new ThemeModel();
                IDataReader iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@themeid";
                p1.Value = themeID;
                p1.SqlDbType = SqlDbType.Int;

                iRead = DataAccess.GetFromReader("Theme_LoadByID", p1);
                while (iRead.Read())
                {                    
                    TM.ThemeID = int.Parse(iRead["ThemeID"].ToString());
                    TM.ThemeName = iRead["ThemeName"].ToString();
                    TM.ThemeDescription = iRead["ThemeDescription"].ToString();
                    TM.ImagePath = iRead["ImagePath"].ToString();
                    TM.UserModified = iRead["UserModified"].ToString();
                    TM.DateModified = iRead["DateModified"].ToString();
                    TM.FileName = iRead["FileName"].ToString(); 
                }
                iRead.Close();
                return TM;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="themeID"></param>
        /// <returns></returns>
        public int Delete(int themeID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@themeid";
                p1.Value = themeID;
                p1.SqlDbType = SqlDbType.Int;

                strErr= DataAccess.Execute("Theme_Delete", p1);

                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Delete Theme");
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
        /// Load Theme
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public DataTable LoadTheme(string keyword)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@keyword";
                p1.Value = keyword;
                p1.SqlDbType = SqlDbType.NVarChar;

                return DataAccess.GetFromDataTable("Theme_LoadTheme", p1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// insert
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public int Insert(string ThemeName,string ThemeDesc, string UserCreated,string fileName)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@themename";
                p1.Value = ThemeName;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@themedesc";
                p2.Value = ThemeDesc;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@usercreated";
                p3.Value = UserCreated;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@filename";
                p4.Value = fileName;
                p4.SqlDbType = SqlDbType.NVarChar;

                strErr= DataAccess.Execute("Theme_Insert", p1,p2,p3,p4);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert Theme");
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
        /// update
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public int Update(int ThemeID,string themeName, string ThemeDesc, string UserModified,string dateModified,string fileName)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@themeid";
                p1.Value = ThemeID;
                p1.SqlDbType = SqlDbType.Int;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@themedesc";
                p2.Value = ThemeDesc;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@usermodified";
                p3.Value = UserModified;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@datemodified";
                p4.Value = dateModified;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@filename";
                p5.Value = fileName;
                p5.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@themename";
                p6.Value = themeName;
                p6.SqlDbType = SqlDbType.NVarChar;

                strErr= DataAccess.Execute("Theme_Update", p1, p2, p3,p4,p5,p6);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Update Theme");
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
