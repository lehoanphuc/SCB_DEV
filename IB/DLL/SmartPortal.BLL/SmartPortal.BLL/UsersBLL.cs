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
    public class UsersBLL
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public DataTable Login(string username, string password)
        {
            UsersModel UM = new UsersModel();
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@username";
            p1.Value = username;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@password";
            p2.Value = password;
            p2.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("Users_Login", p1, p2);


            return iRead;
        }

        /// <summary>
        /// Get Role
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public DataTable GetRole(string username, string serviceID)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@username";
            p1.Value = username;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@serviceID";
            p2.Value = serviceID;
            p2.SqlDbType = SqlDbType.Text;


            iRead = DataAccess.GetFromDataTable("Users_GetRole", p1, p2);


            return iRead;
        }

        public int Insert(string UserName, string Password, string FirstName, string MiddleName, string LastName, int Gender, string Address, string Email, string Birthday, string Phone, int Status, string userCreated, string branchID, string userID, string userType, string userLevel, string policyid)
        {
            int result;
            try
            {
                int strErr = DataAccess.Execute("Users_Insert", new SqlParameter[]
        {
            new SqlParameter
            {
                ParameterName = "@UserName",
                Value = UserName,
                SqlDbType = SqlDbType.NVarChar
            },
            new SqlParameter
            {
                ParameterName = "@Password",
                Value = Password,
                SqlDbType = SqlDbType.NVarChar
            },
            new SqlParameter
            {
                ParameterName = "@FirstName",
                Value = FirstName,
                SqlDbType = SqlDbType.NVarChar
            },
            new SqlParameter
            {
                ParameterName = "@LastName",
                Value = LastName,
                SqlDbType = SqlDbType.NVarChar
            },
            new SqlParameter
            {
                ParameterName = "@MiddleName",
                Value = MiddleName,
                SqlDbType = SqlDbType.NVarChar
            },
            new SqlParameter
            {
                ParameterName = "@Gender",
                Value = Gender,
                SqlDbType = SqlDbType.Int
            },
            new SqlParameter
            {
                ParameterName = "@Address",
                Value = Address,
                SqlDbType = SqlDbType.NVarChar
            },
            new SqlParameter
            {
                ParameterName = "@Email",
                Value = Email,
                SqlDbType = SqlDbType.NVarChar
            },
            new SqlParameter
            {
                ParameterName = "@Birthday",
                Value = Birthday,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@Phone",
                Value = Phone,
                SqlDbType = SqlDbType.NVarChar
            },
            new SqlParameter
            {
                ParameterName = "@Status",
                Value = Status,
                SqlDbType = SqlDbType.Int
            },
            new SqlParameter
            {
                ParameterName = "@usercreated",
                Value = userCreated,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@branchID",
                Value = branchID,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@userID",
                Value = userID,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@userType",
                Value = userType,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@userLevel",
                Value = userLevel,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@policyid",
                Value = policyid,
                SqlDbType = SqlDbType.VarChar
            }
        });
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert User");
                }
                result = strErr;
            }
            catch (BusinessExeption bex)
            {
                throw bex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        /// <summary>
        /// Update
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="FirstName"></param>
        /// <param name="MiddleName"></param>
        /// <param name="LastName"></param>
        /// <param name="Gender"></param>
        /// <param name="Address"></param>
        /// <param name="Email"></param>
        /// <param name="Birthday"></param>
        /// <param name="Phone"></param>
        /// <param name="Status"></param>
        /// <param name="userModified"></param>
        /// <param name="dateModified"></param>
        /// <returns></returns>
        // SmartPortal.BLL.UsersBLL
        public int Update(string UserName, string FirstName, string MiddleName, string LastName, int Gender, string Address, string Email, string Birthday, string Phone, int Status, string userModified, string dateModified, string branchID, string userLevel, string policyid)
        {
            int result;
            try
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@UserName";
                p.Value = UserName;
                p.SqlDbType = SqlDbType.NVarChar;
                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@FirstName";
                p2.Value = FirstName;
                p2.SqlDbType = SqlDbType.NVarChar;
                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@LastName";
                p3.Value = LastName;
                p3.SqlDbType = SqlDbType.NVarChar;
                int strErr = DataAccess.Execute("Users_Update", new SqlParameter[]
        {
            p,
            p2,
            new SqlParameter
            {
                ParameterName = "@MiddleName",
                Value = MiddleName,
                SqlDbType = SqlDbType.NVarChar
            },
            p3,
            new SqlParameter
            {
                ParameterName = "@Gender",
                Value = Gender,
                SqlDbType = SqlDbType.Int
            },
            new SqlParameter
            {
                ParameterName = "@Address",
                Value = Address,
                SqlDbType = SqlDbType.NVarChar
            },
            new SqlParameter
            {
                ParameterName = "@Email",
                Value = Email,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@Birthday",
                Value = Birthday,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@Phone",
                Value = Phone,
                SqlDbType = SqlDbType.NVarChar
            },
            new SqlParameter
            {
                ParameterName = "@Status",
                Value = Status,
                SqlDbType = SqlDbType.Int
            },
            new SqlParameter
            {
                ParameterName = "@usermodified",
                Value = userModified,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@datemodified",
                Value = dateModified,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@branchID",
                Value = branchID,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@userLevel",
                Value = userLevel,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@policyid",
                Value = policyid,
                SqlDbType = SqlDbType.VarChar
            }
        });
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Update User");
                }
                result = strErr;
            }
            catch (BusinessExeption bex)
            {
                throw bex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <returns></returns>
        public DataTable Load()
        {
            try
            {
                return DataAccess.GetFromDataTable("Users_Load", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public UsersModel GetUserInfo(string UserName)
        {
            try
            {
                UsersModel UModel = new UsersModel();
                IDataReader iReader;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@username";
                p1.Value = UserName;
                p1.SqlDbType = SqlDbType.NVarChar;

                iReader = DataAccess.GetFromReader("Users_GetUserInfo", p1);
                while (iReader.Read())
                {
                    UModel.UserName = iReader["UserName"].ToString();
                    UModel.Password = iReader["Password"].ToString();
                    UModel.FirstName = iReader["FirstName"].ToString();
                    UModel.MiddleName = iReader["MiddleName"].ToString();
                    UModel.LastName = iReader["LastName"].ToString();
                    UModel.Gender = int.Parse(iReader["Gender"].ToString());
                    UModel.Address = iReader["Address"].ToString();
                    UModel.Email = iReader["Email"].ToString();
                    UModel.Birthday = DateTime.Parse(iReader["Birthday"].ToString()).ToString("dd/MM/yyyy") == "01/01/1900"? string.Empty: DateTime.Parse(iReader["Birthday"].ToString()).ToString("dd/MM/yyyy");
                    UModel.Phone = iReader["Phone"].ToString();
                    UModel.Status = Convert.ToInt32(iReader["Status"].ToString());
                    UModel.UserModified = iReader["UserModified"].ToString();
                    UModel.DateModified = iReader["DateModified"].ToString();
                    UModel.Branch = iReader["BranchID"].ToString();
                    UModel.Level = iReader["UserLevel"].ToString();
                    UModel.policyid = iReader["PolicyID"].ToString();
                }
                iReader.Close();
                // return DataAccess.GetFromDataTable("Users_GetUserInfo", p1);
                return UModel;
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
        /// <param name="letter"></param>
        /// <returns></returns>
        public DataTable Search(string keyword, string letter)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@keyword";
                p1.Value = keyword;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@letter";
                p2.Value = letter;
                p2.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Users_Search", p1, p2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Search(string keyword, string letter, string branchID, int recPerPage, int recIndex)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@keyword";
                p1.Value = keyword;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@letter";
                p2.Value = letter;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@branchID";
                p3.Value = branchID;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@RECPERPAGE";
                p4.Value = recPerPage;
                p4.SqlDbType = SqlDbType.Int;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@RECINDEX";
                p5.Value = recIndex;
                p5.SqlDbType = SqlDbType.Int;


                return DataAccess.GetFromDataTable("SEMS_Users_Search", p1, p2, p3, p4, p5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="letter"></param>
        /// <returns></returns>
        public int Delete(string username)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@UserName";
                p1.Value = username;
                p1.SqlDbType = SqlDbType.NVarChar;


                strErr = DataAccess.Execute("Users_Delete", p1);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Delete User");
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

        public int UpdateLLT(string username, string datetime)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@UserName";
                p1.Value = username;
                p1.SqlDbType = SqlDbType.NVarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@llt";
                p2.Value = datetime;
                p2.SqlDbType = SqlDbType.VarChar;


                strErr = DataAccess.Execute("Users_UpdateLLT", p1, p2);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Update Last Login Time");
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
        /// update password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int UpdatePassword(string username, string password)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@UserName";
                p1.Value = username;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@password";
                p2.Value = password;
                p2.SqlDbType = SqlDbType.VarChar;


                strErr = DataAccess.Execute("Users_UpdatePassword", p1, p2);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Update Password");
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
        /// Change Pass
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int ChangePassword(string username, string oldPassword, string newPassword)
        {
            UsersModel UM = new UsersModel();
            int i = 0;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@username";
            p1.Value = username;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@oldpassword";
            p2.Value = oldPassword;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@newpassword";
            p3.Value = newPassword;
            p3.SqlDbType = SqlDbType.VarChar;

            i = DataAccess.Execute("Users_ChangePassword", p1, p2, p3);

            if (i == 0)
            {
                throw new BusinessExeption("Mật khẩu không đúng");
            }
            else
            {
                return i;
            }
        }

        ///// <summary>
        ///// Update login status
        ///// </summary>
        ///// <param name="username"></param>
        ///// <param name="sessionID"></param>
        ///// <returns></returns>
        //public int UpdateIsLogin(string sessionID)
        //{
        //    int i = 0;           

        //    SqlParameter p2 = new SqlParameter();
        //    p2.ParameterName = "@islogin";
        //    p2.Value = sessionID;
        //    p2.SqlDbType = SqlDbType.VarChar;


        //    i = DataAccess.Execute("Users_UpdateIslogin",p2);

        //    return i;
        //}

        /// <summary>
        /// Update login status
        /// </summary>
        /// <param name="username"></param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public int UpdateIsLogin(string userName, string expireTime, string UUID)
        {
            int i = DataAccess.Execute("Users_UpdateIslogin", new SqlParameter[]
    {
        new SqlParameter
        {
            ParameterName = "@username",
            Value = userName,
            SqlDbType = SqlDbType.VarChar
        },
        new SqlParameter
        {
            ParameterName = "@expiretime",
            Value = expireTime,
            SqlDbType = SqlDbType.VarChar
        },
        new SqlParameter
        {
            ParameterName = "@UUID",
            Value = UUID,
            SqlDbType = SqlDbType.VarChar
        }
    });
            if (i == 0)
            {
                throw new BusinessExeption("Không thể cập nhật tình trạng đăng nhập");
            }
            return i;
        }


        public DataTable GetIsLogin(string username, string expireTime)
        {

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@username";
            p1.Value = username;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@expiretime";
            p2.Value = expireTime;
            p2.SqlDbType = SqlDbType.VarChar;

            return DataAccess.GetFromDataTable("Users_GetIslogin", p1, p2);


        }
        public bool checkInvalidSession(string username, string service, string UUID)
        {
            bool result;
            try
            {
                DataTable dt = new DataTable();
                dt = DataAccess.GetFromDataTable("IB_CheckInvalidSession", new SqlParameter[]
        {
            new SqlParameter
            {
                ParameterName = "@username",
                Value = username,
                SqlDbType = SqlDbType.NVarChar
            },
            new SqlParameter
            {
                ParameterName = "@service",
                Value = service,
                SqlDbType = SqlDbType.VarChar
            },
            new SqlParameter
            {
                ParameterName = "@UUID",
                Value = UUID,
                SqlDbType = SqlDbType.VarChar
            }
        });
                if (dt.Rows.Count == 0)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public UsersSEMSModel GetUserSEMSLogin(string servicelogin, string username)
        {
            UsersSEMSModel result;
            try
            {
                UsersSEMSModel UserIB = new UsersSEMSModel();
                IDataReader iReader = DataAccess.GetFromReader("IB_getaccountinfo", new SqlParameter[]
        {
            new SqlParameter
            {
                ParameterName = "@serviceLogin",
                Value = servicelogin,
                SqlDbType = SqlDbType.Text
            },
            new SqlParameter
            {
                ParameterName = "@username",
                Value = username,
                SqlDbType = SqlDbType.Text
            }
        });
                while (iReader.Read())
                {
                    UserIB.UserName = iReader["userid"].ToString().Trim();
                    UserIB.Password = iReader["passwordnew"].ToString().Trim();
                    UserIB.Status = Convert.ToInt32(iReader["status"].ToString());
                    UserIB.Failnumber = (int)iReader["failnumber"];
                    UserIB.ExpireTime = (DateTime)iReader["expiretime"];
                    UserIB.SystemTime = (DateTime)iReader["systemtime"];
                    UserIB.UUID = iReader["UUID"].ToString().Trim();
                    UserIB.pwdage = Convert.ToInt32(iReader["pwdage"].ToString());
                    UserIB.lastlogin = Convert.ToInt32(iReader["lastlogin"].ToString());
                }
                iReader.Close();
                result = UserIB;
            }
            catch (Exception ex_17C)
            {
                result = null;
            }
            return result;
        }

        public UsersIBModel GetUserIBLogin(string servicelogin, string username)
        {
            UsersIBModel result;
            try
            {
                UsersIBModel UserIB = new UsersIBModel();
                IDataReader iReader = DataAccess.GetFromReader("IB_getaccountinfo", new SqlParameter[]
        {
            new SqlParameter
            {
                ParameterName = "@serviceLogin",
                Value = servicelogin,
                SqlDbType = SqlDbType.Text
            },
            new SqlParameter
            {
                ParameterName = "@username",
                Value = username,
                SqlDbType = SqlDbType.Text
            }
        });
                while (iReader.Read())
                {
                    UserIB.UserName = iReader["userid"].ToString().Trim();
                    UserIB.Password = iReader["passwordnew"].ToString().Trim();
                    UserIB.Status = iReader["status"].ToString().Trim();
                    UserIB.Dateexpire = (DateTime)iReader["enddate"];
                    UserIB.Failnumber = (int)iReader["failnumber"];
                    UserIB.ExpireTime = (DateTime)iReader["expiretime"];
                    UserIB.SystemTime = (DateTime)iReader["systemtime"];
                    UserIB.UUID = iReader["UUID"].ToString().Trim();
                    UserIB.IsLogin = Convert.ToInt32(iReader["islogin"].ToString());
                    UserIB.pwdage = Convert.ToInt32(iReader["pwdage"].ToString());
                    UserIB.lastlogin = Convert.ToInt32(iReader["lastlogin"].ToString());
                }
                iReader.Close();
                result = UserIB;
            }
            catch (Exception ex_1AF)
            {
                result = null;
            }
            return result;
        }
        public DataTable GetPolicybyUserID(string serviceid, string userid)
        {
            return DataAccess.GetFromDataTable("Users_Policy_by_userid", new SqlParameter[]
    {
        new SqlParameter
        {
            ParameterName = "@serviceid",
            Value = serviceid,
            SqlDbType = SqlDbType.VarChar
        },
        new SqlParameter
        {
            ParameterName = "@userid",
            Value = userid,
            SqlDbType = SqlDbType.VarChar
        }
    });
        }
        public DataTable GetPasswordhisbyUserID(string serviceid, string userid)
        {
            return DataAccess.GetFromDataTable("eba_Users_getpasswordhis_byuserid", new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "@serviceid",
                    Value = serviceid,
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter
                {
                    ParameterName = "@userid",
                    Value = userid,
                    SqlDbType = SqlDbType.VarChar
                }
            });
        }

    }
}
