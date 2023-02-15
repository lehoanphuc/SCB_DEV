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
    public class CategoryBLL
    {
        /// <summary>
        /// Load Category By Language
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public DataTable LoadCategory(string lang)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = lang;
                p1.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromDataTable("Category_Load", p1);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        /// <summary>
        /// InvidualCorporates News
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public DataTable LoadInvidualCorporatesNews(string lang,string tag)
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

                iRead = DataAccess.GetFromDataTable("Category_LoadInvidualCorporatesNews", p1, p2);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// load child category
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public DataTable LoadChildCategory1(int catID)
        {
            try
            {
                DataTable iRead;
                

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@catid";
                p2.Value = catID;
                p2.SqlDbType = SqlDbType.Int;

                iRead = DataAccess.GetFromDataTable("Category_LoadChildCategory1", p2);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// load child category 1
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public DataTable LoadChildCategory(int catID,string langID)
        {
            try
            {
                DataTable iRead;


                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@catid";
                p2.Value = catID;
                p2.SqlDbType = SqlDbType.Int;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@langid";
                p3.Value =langID;
                p3.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromDataTable("Category_LoadChildCategory", p2,p3);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load category by ID
        /// </summary>
        /// <param name="catID"></param>
        /// <returns></returns>
        public CategoryModel LoadByID(int catID)
        {
            try
            {
                IDataReader iRead;
                CategoryModel CM = new CategoryModel();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@catid";
                p1.Value = catID;
                p1.SqlDbType = SqlDbType.Int;

                iRead = DataAccess.GetFromReader("Category_LoadByID", p1);

                while (iRead.Read())
                {
                    CM.CatID = int.Parse(iRead["CatID"].ToString());
                    CM.CatName = iRead["CatName"].ToString();
                    CM.ParentID = int.Parse(iRead["ParentID"].ToString());
                    CM.Link = iRead["Link"].ToString();
                    CM.Tag = iRead["Tag"].ToString();
                    CM.Order = int.Parse(iRead["Order"].ToString());
                    CM.CatDescription = iRead["CatDescription"].ToString();
                    CM.IsPublished = int.Parse(iRead["IsPublished"].ToString());
                    CM.UserModified = iRead["UserModified"].ToString();
                    CM.DateModified = iRead["DateModified"].ToString();
                    CM.LangID = iRead["LangID"].ToString(); ;
                }
                iRead.Close();
                return CM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load for view
        /// </summary>
        /// <param name="langID"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public DataTable LoadForView(string langID, string keyword)
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

                return DataAccess.GetFromDataTable("Category_LoadForView", p1,p2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load for approve
        /// </summary>
        /// <param name="langID"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public DataTable LoadForApprove(string langID, string keyword,string status)
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

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@status";
                p3.Value = status;
                p3.SqlDbType = SqlDbType.VarChar;


                return DataAccess.GetFromDataTable("Category_LoadForApprove", p1, p2,p3);
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
        public int Insert(string catName, string catDesc,int parentID,string langID,int isPublished,int order,string link,string tag,string userCreated)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@catname";
                p1.Value = catName;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@catdesc";
                p2.Value = catDesc;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@parentID";
                p3.Value = parentID;
                p3.SqlDbType = SqlDbType.Int;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@langid";
                p4.Value = langID;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@ispublished";
                p5.Value = isPublished;
                p5.SqlDbType = SqlDbType.Int;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@order";
                p6.Value = order;
                p6.SqlDbType = SqlDbType.Int;

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@link";
                p7.Value = link;
                p7.SqlDbType = SqlDbType.VarChar;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@tag";
                p8.Value = tag;
                p8.SqlDbType = SqlDbType.VarChar;

                SqlParameter p9 = new SqlParameter();
                p9.ParameterName = "@usercreated";
                p9.Value = userCreated;
                p9.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("Category_Insert", p1, p2, p3, p4, p5, p6, p7, p8, p9);

                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable insert category");
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
        public int Update(string catName, string catDesc, int parentID, string langID, int isPublished, int order, string link, string tag, string userModified,string dateModified,int catID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@catname";
                p1.Value = catName;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@catdesc";
                p2.Value = catDesc;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@parentID";
                p3.Value = parentID;
                p3.SqlDbType = SqlDbType.Int;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@langid";
                p4.Value = langID;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@ispublished";
                p5.Value = isPublished;
                p5.SqlDbType = SqlDbType.Int;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@order";
                p6.Value = order;
                p6.SqlDbType = SqlDbType.Int;

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@link";
                p7.Value = link;
                p7.SqlDbType = SqlDbType.VarChar;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@tag";
                p8.Value = tag;
                p8.SqlDbType = SqlDbType.VarChar;

                SqlParameter p9 = new SqlParameter();
                p9.ParameterName = "@usermodified";
                p9.Value = userModified;
                p9.SqlDbType = SqlDbType.VarChar;

                SqlParameter p10 = new SqlParameter();
                p10.ParameterName = "@datemodified";
                p10.Value = dateModified;
                p10.SqlDbType = SqlDbType.VarChar;

                SqlParameter p11 = new SqlParameter();
                p11.ParameterName = "@catid";
                p11.Value = catID;
                p11.SqlDbType = SqlDbType.Int;

                strErr= DataAccess.Execute("Category_Update", p1, p2, p3, p4, p5, p6, p7, p8, p9,p10,p11);
                
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable update category");
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
        /// <param name="catID"></param>
        /// <returns></returns>
        public int Delete(int catID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@catid";
                p1.Value = catID;
                p1.SqlDbType = SqlDbType.Int;
                
                strErr= DataAccess.Execute("Category_Delete", p1);

                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable delete category");
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
        /// Approve
        /// </summary>
        /// <param name="catID"></param>
        /// <returns></returns>
        public int Approve(int catID,int status)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@catid";
                p1.Value = catID;
                p1.SqlDbType = SqlDbType.Int;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@status";
                p2.Value = status;
                p2.SqlDbType = SqlDbType.Int;

                strErr = DataAccess.Execute("Category_Approve", p1,p2);

                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Approve category");
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
