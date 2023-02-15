using System;
using System.Data;
using System.Collections;
using SmartPortal.DAL;
using System.Data.SqlClient;

namespace SmartPortal.SEMS
{
    public class BankUser
    {
        #region LOGIN
        public DataSet Login(string username,string password,ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBLOGIN");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Đăng nhập trang Internet banking");                

                hasInput.Add(SmartPortal.Constant.IPC.USERNAME, username);
                hasInput.Add(SmartPortal.Constant.IPC.PASSWORD, password);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, username);
                hasInput.Add(SmartPortal.Constant.IPC.CLIENTINFO, errorDesc==null?string.Empty:errorDesc); 

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region
        public string GetUserIDFromUserName(string UserName)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@USERNAME";
                p1.Value = UserName;
                p1.SqlDbType = SqlDbType.VarChar;

                DataTable iRead = new DataTable();
                iRead = DataAccess.GetFromDataTable("SEMS_EBA_USERID", p1);
                return iRead.Rows.Count > 0 ? iRead.Rows[0][0].ToString() : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region
        public string GetIsLoginDFromUserName(string UserName)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@USERNAME";
                p1.Value = UserName;
                p1.SqlDbType = SqlDbType.VarChar;

                DataTable iRead = new DataTable();
                iRead = DataAccess.GetFromDataTable("SEMS_EBA_ISLOGIN", p1);
                return iRead.Rows.Count > 0 ? iRead.Rows[0][0].ToString() : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Lấy tất cả level user
        /// <summary>
        /// Load all level
        /// </summary>
        /// <returns></returns>
        public DataTable LoadAllLevel()
        {
            try
            {
                DataTable iRead = new DataTable();
                iRead = DataAccess.GetFromDataTable("SEMS_EBA_USERLEVEL_LOAD", null);
                return iRead;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region change pass teller
        public DataTable ChangePasswordsTeller(string USERNAME, string PASSWORD)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@USERNAME";
                p1.Value = USERNAME;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@PASSWORD";
                p2.Value = PASSWORD;
                p2.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Users_CHANGEPASSWORDS", p1, p2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CHECK VALID  teller
        public DataTable CheckValidTeller(string USERNAME, string EMAIL)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@USERNAME";
                p1.Value = USERNAME;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@EMAIL";
                p2.Value = EMAIL;
                p2.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Users_CHECKVALIDUSER", p1, p2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RESET PASSWORDS  teller
        public DataTable ResetPasswordTeller(string USERNAME, string EMAIL, string PASSWORD, string pwdreset)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@USERNAME";
                p1.Value = USERNAME;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@EMAIL";
                p2.Value = EMAIL;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@PASSWORD";
                p3.Value = PASSWORD;
                p3.SqlDbType = SqlDbType.VarChar;
                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@pwdreset";
                p4.Value = pwdreset;
                p4.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Users_RESETPASSWORDS", p1, p2, p3, p4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
