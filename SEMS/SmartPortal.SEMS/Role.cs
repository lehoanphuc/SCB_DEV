using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using SmartPortal.DAL;

namespace SmartPortal.SEMS
{
    public class Role
    {
        #region Select role by service and contractno
        public DataSet GetRoleByServiceID(string serviceID, string contractNo, string roleType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGRBSID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy thông tin role bỡi serviceid");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceID);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);
                hasInput.Add(SmartPortal.Constant.IPC.ROLETYPE, roleType);



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

        #region Select role default by service
        //public DataSet GetRoleDefaultByServiceID(string serviceID, string productID, ref string errorCode, ref string errorDesc)
        public DataSet GetRoleDefaultByServiceID(string serviceID, string productID, decimal CONTRACT_LEVEL_ID, string roleType, ref string errorCode, ref string errorDesc)
        {
            return GetRoleDefaultByServiceID(serviceID, productID, Constant.IPC.USER, CONTRACT_LEVEL_ID, roleType, ref errorCode, ref errorDesc);
        }
        public DataSet GetRoleDefaultByServiceID(string serviceID, string productID, string rightType, decimal CONTRACT_LEVEL_ID, string roleType, ref string errorCode, ref string errorDesc)

        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGRDBSID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy thông tin role mặc định bỡi serviceid");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceID);
                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, productID);
                hasInput.Add(SmartPortal.Constant.IPC.RIGHTTYPE, rightType);
                hasInput.Add("CONTRACT_LEVEL_ID", CONTRACT_LEVEL_ID);
                hasInput.Add(SmartPortal.Constant.IPC.ROLETYPE, roleType);


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

        #region Select transaction of role
        public DataSet GetTranOfRole(string roleID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSROLELISTTRAN");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy các giao dịch của role");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ROLEID, roleID);


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

        #region Get role by Usertype
        public DataSet GetRoleByUserType(string usertype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETROLEBYUSRTYPE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy hết role bởi usertype");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERTYPE, usertype);

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

        #region Get role by serviceID and userType
        /// <summary>
        /// Load all level
        /// </summary>
        /// <returns></returns>
        public DataTable GetByServiceAndUserType(string serviceID, string userType, string productID ,string roleType="")
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@serviceID";
                p1.Value = serviceID;
                p1.SqlDbType = SqlDbType.Text;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@userType";
                p2.Value = userType;
                p2.SqlDbType = SqlDbType.Text;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@productID";
                p3.Value = productID;
                p3.SqlDbType = SqlDbType.Text;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@roletype";
                p4.Value = roleType;
                p4.SqlDbType = SqlDbType.Text;
                DataTable iRead = new DataTable();
                iRead = DataAccess.GetFromDataTable("SEMS_EBA_ROLES_GETBYSERVICEANDUSERTYPE", p1, p2, p3,p4);
                return iRead;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Get role by type
        /// <summary>
        /// Load all level
        /// </summary>
        /// <returns></returns>
        public DataTable GetRoleByType(string RoleType)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@RoleType";
                p1.Value = RoleType;
                p1.SqlDbType = SqlDbType.Text;

                DataTable iRead = new DataTable();
                iRead = DataAccess.GetFromDataTable("SEMS_LOADROLE_BYTYPE", p1);
                return iRead;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Get role by type
        /// <summary>
        /// Load all level
        /// </summary>
        /// <returns></returns>
        public DataTable GetRoleInforByID(string RoleID)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@ROLEID";
                p1.Value = RoleID;
                p1.SqlDbType = SqlDbType.Text;

                DataTable iRead = new DataTable();
                iRead = DataAccess.GetFromDataTable("SEMS_GET_ROLE_INFO", p1);
                return iRead;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Get role for sms notify

        public DataTable GetRightforSMSnotify(int roleid, string smsnotifytrancode)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@roleid";
                p1.Value = roleid;
                p1.SqlDbType = SqlDbType.Int;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@smsnotifytrancode";
                p2.Value = smsnotifytrancode;
                p2.SqlDbType = SqlDbType.Text;



                DataTable iRead = new DataTable();
                iRead = DataAccess.GetFromDataTable("SEMS_EBA_GET_RIGHT_FOR_SMSNOTIFY", p1, p2);
                return iRead;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        public DataTable GetByServiceAndUserTypeForCard(string serviceID, string userType, string productID)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@serviceID";
                p1.Value = serviceID;
                p1.SqlDbType = SqlDbType.Text;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@userType";
                p2.Value = userType;
                p2.SqlDbType = SqlDbType.Text;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@productID";
                p3.Value = productID;
                p3.SqlDbType = SqlDbType.Text;

                DataTable iRead = new DataTable();
                iRead = DataAccess.GetFromDataTable("SEMS_EBA_ROLES_GETBYSERVICEANDUSERTYPE_FORCARD", p1, p2, p3);
                return iRead;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable GetPageBySS1(int SSID, string serviceID, string ROLEID)
        {
            try
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@ssid";
                p.Value = SSID;
                p.SqlDbType = SqlDbType.Int;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@serviceID";
                p1.Value = serviceID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@ROLEID";
                p2.Value = ROLEID;
                p2.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("Pages_GetPageBySS1", p, p1, p2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Get role report by group
        public DataSet GetAllRoleReport(string serviceID, int groupid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETROLEREPORT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lay ra role report cua group");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceID);
                hasInput.Add(SmartPortal.Constant.IPC.ROLEID, groupid);


                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();
                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                }
                else
                {
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
        #region GET ROLES  BY USERID
        public DataSet GetRolesByUserID(string userid, string serviceid, string roleType, string contractNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETROLESBYUSER");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "lấy các giao dịch của role");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceid);
                hasInput.Add(SmartPortal.Constant.IPC.ROLETYPE, roleType);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);


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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorDesc"></param>
        /// <returns></returns>
        public DataSet GetTypeUserType(string type, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLOAD_TYPECOP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin Role type");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.TYPE, type);

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
        public DataSet GetRoleTypeByService(string serviceid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "LOADROLETYPE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy role type theo serviceid");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.SERVICE, serviceid);

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
    }
}
