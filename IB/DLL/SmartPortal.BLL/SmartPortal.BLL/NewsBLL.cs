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
    public class NewsBLL
    {
        /// <summary>
        /// Load Bank News
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public DataTable LoadBankNews(string lang,string tag)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = lang;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@tag";
                p2.Value = tag;
                p2.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromDataTable("News_LoadBankNews", p1, p2);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load tin cua 1 category
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="catID"></param>
        /// <returns></returns>
        public DataTable LoadNewsOfCategory(int catID)
        {
            try
            {
                DataTable iRead;                

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@catid";
                p2.Value = catID;
                p2.SqlDbType = SqlDbType.Int;

                iRead = DataAccess.GetFromDataTable("News_LoadNewsOfCategory", p2);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load tin cho view
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="catID"></param>
        /// <returns></returns>
        public DataTable LoadNewsForView(int catID,string langid)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langid;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@catid";
                p2.Value = catID;
                p2.SqlDbType = SqlDbType.Int;

                iRead = DataAccess.GetFromDataTable("News_LoadNewsForView",p1, p2);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load news by id
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="catID"></param>
        /// <returns></returns>
        public NewsModel LoadNewsByID(int newsID,int flag)
        {
            try
            {
                IDataReader iRead;
                NewsModel NM = new NewsModel();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@newsid";
                p1.Value = newsID;
                p1.SqlDbType = SqlDbType.Int;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@flag";
                p2.Value = newsID;
                p2.SqlDbType = SqlDbType.Int;
                

                iRead = DataAccess.GetFromReader("News_LoadNewsByID", p1,p2);

                while (iRead.Read())
                {
                    NM.NewsID = int.Parse(iRead["NewsID"].ToString());
                    NM.Title = iRead["Title"].ToString();
                    NM.Content = iRead["NewsContent"].ToString();
                    NM.Author = iRead["NewsAuthor"].ToString();
                    NM.CatID = int.Parse(iRead["CatID"].ToString());
                    NM.Summary = iRead["Summary"].ToString();
                    NM.DateCreated = DateTime.Parse(iRead["DateCreated"].ToString()).ToString("dd/MM/yyyy");
                    NM.UserModified = iRead["UserModified"].ToString();
                    NM.DateCreated = iRead["DateCreated"].ToString();
                }
                iRead.Close();

                return NM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load news by tag
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="catID"></param>
        /// <returns></returns>
        public NewsModel LoadNewsByTag(string tag, string langID)
        {
            try
            {
                IDataReader iRead;
                NewsModel NM = new NewsModel();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@tag";
                p1.Value = tag;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@langid";
                p2.Value = langID;
                p2.SqlDbType = SqlDbType.VarChar;


                iRead = DataAccess.GetFromReader("News_LoadNewsByTag", p1, p2);

                while (iRead.Read())
                {
                    NM.NewsID = int.Parse(iRead["NewsID"].ToString());
                    NM.Title = iRead["Title"].ToString();
                    NM.Content = iRead["NewsContent"].ToString();
                    NM.Author = iRead["NewsAuthor"].ToString();
                    NM.CatID = int.Parse(iRead["CatID"].ToString());
                    NM.Summary = iRead["Summary"].ToString();
                    NM.DateCreated = DateTime.Parse(iRead["DateCreated"].ToString()).ToString("dd/MM/yyyy");
                }
                iRead.Close();

                return NM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load tin lien quan
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="catID"></param>
        /// <returns></returns>
        public DataTable LoadRelatedNews(int newsID)
        {
            try
            {
                DataTable iRead;
                

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@newsid";
                p2.Value = newsID;
                p2.SqlDbType = SqlDbType.Int;

                iRead = DataAccess.GetFromDataTable("News_LoadRelatedNews" ,p2);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// search
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="catID"></param>
        /// <returns></returns>
        public DataTable Search(int catID, string keyword,string langid)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@keyword";
                p1.Value = keyword;
                p1.SqlDbType = SqlDbType.NVarChar;
                p1.Size = 200;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@catid";
                p2.Value = catID;
                p2.SqlDbType = SqlDbType.Int;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@langid";
                p3.Value = langid;
                p3.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromDataTable("News_Search", p1, p2,p3);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="title"></param>
        /// <param name="summary"></param>
        /// <param name="content"></param>
        /// <param name="author"></param>
        /// <param name="catID"></param>
        /// <returns></returns>
        public int Insert(string title,string summary,string content,string author,int catID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@title";
                p1.Value = title;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@summary";
                p2.Value = summary;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@content";
                p3.Value = content;
                p3.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@author";
                p4.Value = author;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@catid";
                p5.Value = catID;
                p5.SqlDbType = SqlDbType.Int;

                strErr= DataAccess.Execute("News_Insert", p1, p2, p3, p4, p5);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert News");
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
        /// <param name="title"></param>
        /// <param name="summary"></param>
        /// <param name="content"></param>
        /// <param name="author"></param>
        /// <param name="catID"></param>
        /// <returns></returns>
        public int Update(string title, string summary, string content, string userModified, int catID,string dateModified,int newsID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@title";
                p1.Value = title;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@summary";
                p2.Value = summary;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@content";
                p3.Value = content;
                p3.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@usermodified";
                p4.Value = userModified;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@catid";
                p5.Value = catID;
                p5.SqlDbType = SqlDbType.Int;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@datemodified";
                p6.Value = dateModified;
                p6.SqlDbType = SqlDbType.VarChar;

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@newsid";
                p7.Value = newsID;
                p7.SqlDbType = SqlDbType.Int;

                strErr= DataAccess.Execute("News_Update", p1, p2, p3, p4, p5,p6,p7);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Update News");
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
        /// <param name="newID"></param>
        /// <returns></returns>
        public int Delete(int newID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@newsid";
                p1.Value = newID;
                p1.SqlDbType = SqlDbType.Int;               
              
                strErr= DataAccess.Execute("News_Delete", p1);

                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Delete News");
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
        /// load news approve
        /// </summary>
        /// <param name="catID"></param>
        /// <param name="langid"></param>
        /// <returns></returns>
        public DataTable LoadNewsForApprove(int catID, string langid,int status,string keyword)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = langid;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@catid";
                p2.Value = catID;
                p2.SqlDbType = SqlDbType.Int;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@status";
                p3.Value = status;
                p3.SqlDbType = SqlDbType.Int;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@keyword";
                p4.Value = keyword;
                p4.SqlDbType = SqlDbType.NVarChar;

                iRead = DataAccess.GetFromDataTable("News_LoadNewsForApprove", p1, p2,p3,p4);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// approve
        /// </summary>
        /// <param name="catID"></param>
        /// <param name="langid"></param>
        /// <returns></returns>
        public int Approve(int newsID,int published)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@newsid";
                p1.Value = newsID;
                p1.SqlDbType = SqlDbType.Int;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@ispublished";
                p2.Value = published;
                p2.SqlDbType = SqlDbType.Int;
               

                strErr= DataAccess.Execute("News_Approve", p1, p2);

                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Approve News");
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
