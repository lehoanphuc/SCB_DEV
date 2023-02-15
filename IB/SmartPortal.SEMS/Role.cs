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
        public DataSet GetRoleByServiceID(string serviceID, string contractNo, ref string errorCode, ref string errorDesc)
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
        public DataSet GetRoleDefaultByServiceID(string serviceID, string productID, ref string errorCode, ref string errorDesc)
        {
            return GetRoleDefaultByServiceID(serviceID, productID, Constant.IPC.USER, ref errorCode, ref errorDesc);
        }
        public DataSet GetRoleDefaultByServiceID(string serviceID, string productID, string rightType, ref string errorCode, ref string errorDesc)

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
                hasInput.Add(SmartPortal.Constant.IPC.RIGHTTYPE, rightType);

                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceID);
                hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, productID);


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
        public DataTable GetByServiceAndUserType(string serviceID, string userType, string productID, string roletype ="")
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
                p4.ParameterName = "@ROLETYPE";
                p4.Value = roletype;
                p4.SqlDbType = SqlDbType.Text;
                
                DataTable iRead = new DataTable();
                iRead = DataAccess.GetFromDataTable("SEMS_EBA_ROLES_GETBYSERVICEANDUSERTYPE", p1, p2, p3, p4);
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
    }
}
