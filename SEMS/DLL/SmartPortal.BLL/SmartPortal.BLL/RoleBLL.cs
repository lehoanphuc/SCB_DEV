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
    public class RoleBLL
    {
        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="widgetpageID"></param>
        /// <returns></returns>
        public List<RoleModel> GetAll(int widgetpageID)
        {
            try
            {
                List<RoleModel> lstRM = new List<RoleModel>();

                IDataReader iRead;
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@widgetpageid";
                p1.Value = widgetpageID;
                p1.SqlDbType = SqlDbType.Int;

                iRead = DataAccess.GetFromReader("Roles_Load", p1);

                while (iRead.Read())
                {
                    RoleModel RM = new RoleModel();
                    RM.RoleID = int.Parse(iRead["RoleID"].ToString());
                    RM.RoleName = iRead["RoleName"].ToString();
                    RM.RoleDescription = iRead["RoleDescription"].ToString();

                    lstRM.Add(RM);
                }
                iRead.Close();
                return lstRM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get All Reader
        /// </summary>
        /// <param name="widgetpageID"></param>
        /// <returns></returns>
        public DataTable GetAllReader(string widgetpageID)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@widgetpageid";
                p1.Value = widgetpageID;
                p1.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Roles_Load", p1);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load For User
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public DataTable LoadForUser(string username, string serviceID)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@username";
                p1.Value = username;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@serviceID";
                p2.Value = serviceID;
                p2.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Roles_LoadForUser", p1, p2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load For View
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public DataTable LoadForView(string keyword, string serviceID, int recPerPage, int recIndex)
        {
            try
            {

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@keyword";
                p1.Value = keyword;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@serviceID";
                p2.Value = serviceID;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@RECPERPAGE";
                p3.Value = recPerPage;
                p3.SqlDbType = SqlDbType.Int;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@RECINDEX";
                p4.Value = recIndex;
                p4.SqlDbType = SqlDbType.Int;

                return DataAccess.GetFromDataTable("Group_LoadForView", p1, p2, p3, p4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable LoadForUser(string username, string serviceID, string userSesion)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@username";
                p1.Value = username;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@serviceID";
                p2.Value = serviceID;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@userSesion";
                p3.Value = userSesion;
                p3.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Roles_LoadForUserBySession", p1, p2, p3);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// insert
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int Insert(string roleName, string roleDesc, string userCreated, string serviceID, string userType, string status, string roleType)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@rolename";
                p1.Value = roleName;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@roledescription";
                p2.Value = roleDesc;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@usercreated";
                p3.Value = userCreated;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@serviceID";
                p4.Value = serviceID;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@userType";
                p5.Value = userType;
                p5.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@status";
                p6.Value = status;
                p6.SqlDbType = SqlDbType.VarChar;

                SqlParameter p7 = new SqlParameter();
                p7.Direction = ParameterDirection.Output;
                p7.ParameterName = "@roleid";
                p7.SqlDbType = SqlDbType.Int;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@RoleType";
                p8.Value = roleType;
                p8.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("Roles_Insert", p1, p2, p3, p4, p5, p6, p7, p8);

                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert Role");
                }
                else
                {
                    return int.Parse(p7.Value.ToString());
                    //return strErr;
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
        /// <param name="username"></param>
        /// <returns></returns>
        public int Update(int roleID, string roleName, string roleDesc, string userModified, string serviceID, string userType, string status, string roleType)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@rolename";
                p1.Value = roleName;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@roledescription";
                p2.Value = roleDesc;
                p2.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@usermodified";
                p3.Value = userModified;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@roleid";
                p4.Value = roleID;
                p4.SqlDbType = SqlDbType.Int;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@serviceID";
                p5.Value = serviceID;
                p5.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@userType";
                p6.Value = userType;
                p6.SqlDbType = SqlDbType.VarChar;

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@status";
                p7.Value = status;
                p7.SqlDbType = SqlDbType.VarChar;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@RoleType";
                p8.Value = roleType;
                p8.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("Roles_Update", p1, p2, p3, p4, p5, p6, p7, p8);

                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Update Role");
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
        /// <param name="roleID"></param>
        /// <returns></returns>
        public RoleModel GetByID(int roleID)
        {
            try
            {
                IDataReader iRead;
                RoleModel RM = new RoleModel();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@roleid";
                p1.Value = roleID;
                p1.SqlDbType = SqlDbType.Int;

                iRead = DataAccess.GetFromReader("Roles_GetByID", p1);

                while (iRead.Read())
                {
                    RM.RoleID = int.Parse(iRead["RoleID"].ToString());
                    RM.RoleName = iRead["RoleName"].ToString();
                    RM.RoleDescription = iRead["RoleDescription"].ToString();
                    RM.UserModified = iRead["UserModified"].ToString();
                    RM.DateModified = iRead["DateModified"].ToString();
                    RM.ServiceID = iRead["ServiceID"].ToString();
                    RM.UserType = iRead["UserType"].ToString();
                }
                iRead.Close();

                return RM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public string Delete(int roleID)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@roleid";
                p1.Value = roleID;
                p1.SqlDbType = SqlDbType.Int;

                strErr = DataAccess.Execute("Roles_Delete", p1);


                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Delete Role");
                }
                else
                {
                    return strErr.ToString();
                }
            }
            catch (BusinessExeption bex)
            {
                throw bex;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
