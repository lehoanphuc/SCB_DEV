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
    public class MenuBLL
    {
        /// <summary>
        /// Load Menu
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable LoadMenu(string lang, string userName, string serviceID)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = lang;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@username";
                p2.Value = userName;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@serviceID";
                p3.Value = serviceID;
                p3.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromDataTable("Menu_Load", p1, p2, p3);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// load menu for permission
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        /// 
        public DataTable LoadForPermission(int roleid,string lang, string serviceid)
        {
            try
            {
                DataTable iRead;
                SqlParameter p0 = new SqlParameter();
                p0.ParameterName = "@roleID";
                p0.Value = roleid;
                p0.SqlDbType = SqlDbType.VarChar;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = lang;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@serviceid";
                p2.Value = serviceid;
                p2.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromDataTable("SMP_GetAllRoleByID2", p0, p1, p2);
                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //hunglt change portalid => serviceid
        public DataTable LoadForPermission(string lang, string serviceid)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = lang;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@serviceid";
                p2.Value = serviceid;
                p2.SqlDbType = SqlDbType.VarChar;



                iRead = DataAccess.GetFromDataTable("SEMS_Menu_LoadForPermission", p1, p2);

                return iRead;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable LoadForPermission(string lang)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = lang;
                p1.SqlDbType = SqlDbType.VarChar;


                iRead = DataAccess.GetFromDataTable("Menu_LoadForPermission", p1);

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
        /// <returns></returns>
        public int Insert(string menuid, string title, string link, string serviceid, int order, string parent, bool publish, string userCreated, string langid,string pageid)
        {
            try
            {
                int strErr = 0;

                SqlParameter p0 = new SqlParameter();
                p0.ParameterName = "@menuid";
                p0.Value = menuid;
                p0.SqlDbType = SqlDbType.VarChar;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@title";
                p1.Value = title;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@link";
                p2.Value = link;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@serviceid";
                p3.Value = serviceid;
                p3.SqlDbType = SqlDbType.VarChar;


                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@order";
                p4.Value = order;
                p4.SqlDbType = SqlDbType.Int;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@parent";
                p5.Value = parent;
                p5.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@publish";
                p6.Value = publish;
                p6.SqlDbType = SqlDbType.Bit;

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@usercreated";
                p7.Value = userCreated;
                p7.SqlDbType = SqlDbType.VarChar;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@langid";
                p8.Value = langid;
                p8.SqlDbType = SqlDbType.VarChar;

                SqlParameter p9 = new SqlParameter();
                p9.ParameterName = "@pageid";
                p9.Value = pageid;
                p9.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("Menu_Insert", p0, p1, p2, p3, p4, p5, p6, p7, p8,p9);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert Menu");
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
        /// <returns></returns>
        public int Update(string menuID, string title, string link, string serviceid, int order, string parent, bool publish, string userModified, string langid, string dateModified,string pageid)
        {
            try
            {
                int strErr = 0;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@menuid";
                p.Value = menuID;
                p.SqlDbType = SqlDbType.VarChar;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@title";
                p1.Value = title;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@link";
                p2.Value = link;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@serviceid";
                p3.Value = serviceid;
                p3.SqlDbType = SqlDbType.VarChar;


                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@order";
                p4.Value = order;
                p4.SqlDbType = SqlDbType.Int;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@parent";
                p5.Value = parent;
                p5.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@publish";
                p6.Value = publish;
                p6.SqlDbType = SqlDbType.Bit;

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@usermodified";
                p7.Value = userModified;
                p7.SqlDbType = SqlDbType.VarChar;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@langid";
                p8.Value = langid;
                p8.SqlDbType = SqlDbType.VarChar;


                SqlParameter p9 = new SqlParameter();
                p9.ParameterName = "@datemodified";
                p9.Value = dateModified;
                p9.SqlDbType = SqlDbType.VarChar;


                SqlParameter p10 = new SqlParameter();
                p10.ParameterName = "@pageid";
                p10.Value = pageid;
                p10.SqlDbType = SqlDbType.VarChar;


                strErr = DataAccess.Execute("Menu_Update", p, p1, p2, p3, p4, p5, p6, p7, p8, p9,p10);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Update Menu");
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
        /// load menu for permission
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public DataTable LoadForView(string lang, string keyword)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = lang;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@keyword";
                p2.Value = keyword;
                p2.SqlDbType = SqlDbType.NVarChar;


                iRead = DataAccess.GetFromDataTable("Menu_LoadForView", p1, p2);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable LoadForView(string lang, string keyword, string serviceid)
        {
            try
            {
                DataTable iRead;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = lang;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@keyword";
                p2.Value = keyword;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@serviceid";
                p3.Value = serviceid;
                p3.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromDataTable("SEMS_Menu_LoadForView", p1, p2, p3);

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
        /// <returns></returns>
        public DataTable Search(string keyword, string lang)
        {
            try
            {
                DataTable iRead;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@keyword";
                p.Value = keyword;
                p.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = lang;
                p1.SqlDbType = SqlDbType.VarChar;


                iRead = DataAccess.GetFromDataTable("Menu_Search", p, p1);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public MenuModel GetByID(string menuID, string lang)
        {
            MenuModel MM;
            try
            {
                IDataReader iRead;
                MM = new MenuModel();

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@menuid";
                p.Value = menuID;
                p.SqlDbType = SqlDbType.VarChar;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@langid";
                p1.Value = lang;
                p1.SqlDbType = SqlDbType.VarChar;


                iRead = DataAccess.GetFromReader("Menu_GetByID", p, p1);

                while (iRead.Read())
                {
                    MM.MenuID = iRead["MenuID"].ToString();
                    MM.MenuLang = iRead["LangID"].ToString();
                    MM.MenuLink = iRead["Link"].ToString();
                    MM.MenuParent = iRead["MenuParent"].ToString();
                    MM.MenuTitle = iRead["MenuTitle"].ToString();
                    //MM.MenuOrder = int.Parse(iRead["MenuOrder"].ToString());
                    //MM.IsPublished = bool.Parse(iRead["IsPublished"].ToString());
                    //MM.ServiceID = iRead["ServiceID"].ToString();
                }
                iRead.Close();
            }
            catch (Exception ex)
            {
                MM = null;
            }
            return MM;
        }

        /// <summary>
        /// Get Menu By Link
        /// </summary>
        /// <param name="link"></param>
        /// <param name="langID"></param>
        /// <returns></returns>
        public MenuModel GetByLink(string link, string langID)
        {
            try
            {
                IDataReader iRead;
                MenuModel MM = new MenuModel();

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@link";
                p.Value = link;
                p.SqlDbType = SqlDbType.VarChar;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@lang";
                p1.Value = langID;
                p1.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromReader("Menu_GetByLink", p, p1);

                while (iRead.Read())
                {
                    MM.MenuID = iRead["MenuID"].ToString();

                    MM.MenuLink = iRead["Link"].ToString();
                    MM.MenuOrder = int.Parse(iRead["MenuOrder"].ToString());
                    MM.MenuParent = iRead["MenuParent"].ToString();
                    MM.IsPublished = bool.Parse(iRead["IsPublished"].ToString());
                }
                iRead.Close();
                return MM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetByParent(string parentID)
        {
            try
            {
                DataTable iRead;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@parentid";
                p.Value = parentID;
                p.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromDataTable("Menu_GetByParent", p);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get menu Info By ID
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public MenuModel GetInfoByID(string menuID)
        {
            try
            {
                IDataReader iRead;
                MenuModel MM = new MenuModel();

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@menuid";
                p.Value = menuID;
                p.SqlDbType = SqlDbType.VarChar;

                iRead = DataAccess.GetFromReader("Menu_GetInfoByID", p);

                while (iRead.Read())
                {
                    MM.MenuID = iRead["MenuID"].ToString();
                    MM.MenuLink = iRead["Link"].ToString();
                    MM.MenuOrder = int.Parse(iRead["MenuOrder"].ToString());
                    MM.MenuParent = iRead["MenuParent"].ToString();
                    MM.IsPublished = bool.Parse(iRead["IsPublished"].ToString());
                    MM.UserModified = iRead["UserModified"].ToString();
                    MM.DateModified = iRead["DateModified"].ToString();
                    MM.PageName = iRead["PageID"].ToString();
                    MM.ServiceID = iRead["ServiceID"].ToString();
                }
                iRead.Close();
                return MM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public int Delete(string menuID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@menuid";
                p1.Value = menuID;
                p1.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("Menu_Delete", p1);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Delete Menu");
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
        /// translate
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public int Translate(string menuID, string langID, string title)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@menuid";
                p1.Value = menuID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@langid";
                p2.Value = langID;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@title";
                p3.Value = title;
                p3.SqlDbType = SqlDbType.NVarChar;

                strErr = DataAccess.Execute("Menu_Translate", p1, p2, p3);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Translate Language For Menu");
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
        public DataTable LoadGroupById(string roleid)
        {
            try
            {
                DataTable iRead;
                SqlParameter p0 = new SqlParameter();
                p0.ParameterName = "@roleID";
                p0.Value = roleid;
                p0.SqlDbType = SqlDbType.VarChar;
                iRead = DataAccess.GetFromDataTable("Roles_GetByID", p0);

                return iRead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
