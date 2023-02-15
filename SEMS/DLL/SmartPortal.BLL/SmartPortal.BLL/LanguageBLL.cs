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
    public class LanguageBLL
    {
        /// <summary>
        /// Load All Language
        /// </summary>
        /// <returns></returns>
        public DataTable Load()
        {
            try
            {
                return DataAccess.GetFromDataTable("Language_Load", null);
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
        /// <returns></returns>
        public DataTable Search(string keyword)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@keyword";
                p1.Value = keyword;
                p1.SqlDbType = SqlDbType.NVarChar;

                return DataAccess.GetFromDataTable("Language_Search", p1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load By ID
        /// </summary>
        /// <param name="langID"></param>
        /// <returns></returns>
        public LanguageModel LoadByID(string langID)
        {
            try
            {
                IDataReader iRead;
                LanguageModel LM = new LanguageModel();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;

                iRead= DataAccess.GetFromReader("Language_LoadByID", p1);

                while (iRead.Read())
                {
                    LM.LangID = iRead["LangID"].ToString();
                    LM.LangName = iRead["LangName"].ToString();
                    LM.UserModified = iRead["UserModified"].ToString();
                    LM.DateModified = iRead["DateModified"].ToString();
                }
                iRead.Close();

                return LM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public int Insert(string langID,string langName,string userCreated)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@langname";
                p2.Value = langName;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@usercreated";
                p3.Value = userCreated;
                p3.SqlDbType = SqlDbType.VarChar;

                strErr= DataAccess.Execute("Language_Insert", p1,p2,p3);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert Language");
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
        /// <param name="dateModified"></param>
        /// <param name="userModified"></param>
        /// <returns></returns>
        public int Update(string langID, string dateModified, string userModified)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@usermodified";
                p2.Value = userModified;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@datemodified";
                p3.Value = dateModified;
                p3.SqlDbType = SqlDbType.VarChar;

                strErr= DataAccess.Execute("Language_Update", p1, p2, p3);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Update Language");
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
        /// delete
        /// </summary>
        /// <param name="langID"></param>
        /// <param name="dateModified"></param>
        /// <param name="userModified"></param>
        /// <returns></returns>
        public int Delete(string langID)
        {
            try
            {
                int strErr=0;
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;                

                strErr= DataAccess.Execute("Language_Delete", p1);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Delete Language");
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
