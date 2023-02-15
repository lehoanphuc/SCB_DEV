using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using SmartPortal.DAL;
using System.Data.SqlClient;

namespace SmartPortal.SEMS
{
    public class Notification
    {
        #region Insert SMS Notify config
        public DataSet InsertSMSNotifyConfig(string cfid, string roleid, string trancode, string cfname, string desc, string status, string usercreated, string datecreated, string usermodified, string datemodified, DataTable tblCFDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSSMSNOINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "ADD SMS NOTIFY CONFIG");

                #region Insert bảng sms_notify
                object[] insertsmsnoconfig = new object[2];
                insertsmsnoconfig[0] = "EBA_SMSNOTIFYCF_INSERT";
                //tao bang chua thong tin process
                DataTable tblsmsnocf = new DataTable();
                DataColumn colcfid = new DataColumn("colcfid");
                DataColumn colrolid = new DataColumn("colrolid");
                DataColumn coltrancode = new DataColumn("coltrancode");
                DataColumn colname = new DataColumn("colname");
                DataColumn coldesc = new DataColumn("coldesc");
                DataColumn colusercreated = new DataColumn("colusercreated");
                DataColumn coldatecreated = new DataColumn("coldatecreated");
                DataColumn colusermodified = new DataColumn("colusermodified");
                DataColumn coldatemodified = new DataColumn("coldatemodified");
                DataColumn colstatus = new DataColumn("colstatus");


                //add vào table product
                tblsmsnocf.Columns.Add(colcfid);
                tblsmsnocf.Columns.Add(colrolid);
                tblsmsnocf.Columns.Add(coltrancode);
                tblsmsnocf.Columns.Add(colname);
                tblsmsnocf.Columns.Add(coldesc);
                tblsmsnocf.Columns.Add(colusercreated);
                tblsmsnocf.Columns.Add(coldatecreated);
                tblsmsnocf.Columns.Add(colusermodified);
                tblsmsnocf.Columns.Add(coldatemodified);
                tblsmsnocf.Columns.Add(colstatus);


                //tao 1 dong du lieu
                DataRow row = tblsmsnocf.NewRow();
                row["colcfid"] = cfid;
                row["colrolid"] = roleid;
                row["coltrancode"] = trancode;
                row["colname"] = cfname;
                row["coldesc"] = desc;
                row["colusercreated"] = usercreated;
                row["coldatecreated"] = datecreated;
                row["colusermodified"] = usermodified;
                row["coldatemodified"] = datemodified;
                row["colstatus"] = status;


                tblsmsnocf.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                insertsmsnoconfig[1] = tblsmsnocf;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTSMSNO, insertsmsnoconfig);
                #endregion


                #region Insert SMSNODETAILS
                object[] insertsmsnoconfigdetail = new object[2];
                insertsmsnoconfigdetail[0] = "EBA_SMSNODETAILS_INSERT";

                //add vao phan tu thu 2 mang object
                insertsmsnoconfigdetail[1] = tblCFDetail;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTSMSNODETAILS, insertsmsnoconfigdetail);
                #endregion



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

        #region Search config by codition

        public DataTable SearchSMSConfig(string RoleID, string TranType, string ConfigName)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@ROLEID";
                p1.Value = RoleID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@TRANTYPE";
                p2.Value = TranType;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@CFNAME";
                p3.Value = ConfigName;
                p3.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("SEMS_SMS_NOTIFY_CONFIG_SEARCH", p1, p2, p3);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        
        #region Search notify by condition
        public DataSet GetNotifyByCondition(string ID, string serviceID, string varname, string startdate, string enddate, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSNOTIFYSEARCH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tìm kiếm sản phẩm Ebanking");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.ID, ID);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceID);
                hasInput.Add(SmartPortal.Constant.IPC.STARTDATE, startdate);
                hasInput.Add(SmartPortal.Constant.IPC.ENDDATE, enddate);
                hasInput.Add(SmartPortal.Constant.IPC.VARNAME, varname);


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
        #region DELETE PRODUCT
        public DataSet DeleteNotify(string ID,string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSNOTIFYDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Xóa sản phẩm");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ID, ID);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
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
        #region Update notify
        public DataSet UpdateNotify(string ID,string Serviceid, string varname, string content, string link, string looptime, string devicetype, string mbversion, DateTime starttime, DateTime endtime, string val5, string val6, string val7, string val8, string val9, string val10, string Usercreate, string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSNOTIFYUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "ADD NOTIFY ");
                hasInput.Add(SmartPortal.Constant.IPC.ID, ID);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, Serviceid);
                hasInput.Add(SmartPortal.Constant.IPC.VARNAME, varname);
                hasInput.Add(SmartPortal.Constant.IPC.CONTENT, content);
                hasInput.Add(SmartPortal.Constant.IPC.LINK, link);
                hasInput.Add(SmartPortal.Constant.IPC.LOOPTIME, looptime);
                hasInput.Add(SmartPortal.Constant.IPC.DEVICETYPE, devicetype);
                hasInput.Add(SmartPortal.Constant.IPC.MBVERSION, mbversion);
                hasInput.Add(SmartPortal.Constant.IPC.STARTTIME, starttime);
                hasInput.Add(SmartPortal.Constant.IPC.ENDTIME, endtime);

                hasInput.Add(SmartPortal.Constant.IPC.VALUE5, val5);
                hasInput.Add(SmartPortal.Constant.IPC.VALUE6, val6);
                hasInput.Add(SmartPortal.Constant.IPC.VALUE7, val7);
                hasInput.Add(SmartPortal.Constant.IPC.VALUE8, val8);
                hasInput.Add(SmartPortal.Constant.IPC.VALUE9, val9);
                hasInput.Add(SmartPortal.Constant.IPC.VALUE10, val10);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATE, Usercreate);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
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
        #region Insert notify
        public DataSet InsertNotify(string Serviceid, string varname, string content, string link, string looptime, string devicetype, string mbversion, DateTime starttime, DateTime endtime,string val5,string val6,string val7,string val8,string val9,string val10,string Usercreate,string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSNOTIFYINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "ADD NOTIFY ");

                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, Serviceid);
                hasInput.Add(SmartPortal.Constant.IPC.VARNAME, varname);
                hasInput.Add(SmartPortal.Constant.IPC.CONTENT, content);
                hasInput.Add(SmartPortal.Constant.IPC.LINK, link);
                hasInput.Add(SmartPortal.Constant.IPC.LOOPTIME, looptime);
                hasInput.Add(SmartPortal.Constant.IPC.DEVICETYPE, devicetype);
                hasInput.Add(SmartPortal.Constant.IPC.MBVERSION, mbversion);
                hasInput.Add(SmartPortal.Constant.IPC.STARTTIME, starttime);
                hasInput.Add(SmartPortal.Constant.IPC.ENDTIME, endtime);

                hasInput.Add(SmartPortal.Constant.IPC.VALUE5, val5);
                hasInput.Add(SmartPortal.Constant.IPC.VALUE6, val6);
                hasInput.Add(SmartPortal.Constant.IPC.VALUE7, val7);
                hasInput.Add(SmartPortal.Constant.IPC.VALUE8, val8);
                hasInput.Add(SmartPortal.Constant.IPC.VALUE9, val9);
                hasInput.Add(SmartPortal.Constant.IPC.VALUE10, val10);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATE, Usercreate);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
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


        #region load MB version
        public DataTable GetMBversion()
        {
            try
            {

                return DataAccess.GetFromDataTable("SEMS_LOAD_MB_VERSION", null);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region lay var notify
        public DataTable GetVarnotify()
        {
            try
            {

                return DataAccess.GetFromDataTable("SEMS_LOADVARNOTIFY", null);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region push notification
        public DataSet GetPushNotification(string ID, string name, string notificationType, string type, string sendType, string status, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPUSHNOGETALL");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Search push notification");
                hasInput.Add(SmartPortal.Constant.IPC.ID, ID);
                hasInput.Add(SmartPortal.Constant.IPC.NAME, name);
                hasInput.Add(SmartPortal.Constant.IPC.NOTIFICATIONTYPE, notificationType);
                hasInput.Add(SmartPortal.Constant.IPC.TYPE, type);
                hasInput.Add(SmartPortal.Constant.IPC.SENDTYPE, sendType);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add("RECPERPAGE", recPerPage);
                hasInput.Add("RECINDEX", recIndex);

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

        public DataSet DeletePushNotification(string ID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPUSHNODELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Delete push notification");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ID, ID);
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

        public DataSet GetPushNotificationByID(string ID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPUSHNOGETBYID");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get push notification by ID");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ID, ID);
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

        public DataSet EditPushNotification(string ID, DataTable dtPN, DataTable drSchedule, DataTable dtDel, DataTable dtScheduleDay, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPUSHNOEDIT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Edit push notification");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.ID, ID);

                object[] objPN = new object[2];
                objPN[0] = "EBA_PUSHNOTIFICATION_EDIT";
                objPN[1] = dtPN;

                object[] objSchedule = new object[2];
                objSchedule[0] = "IB_IPC_SCHEDULES_UPDATE";
                objSchedule[1] = drSchedule;

                object[] objDel = new object[2];
                objDel[0] = "IB_IPC_SCHEDULEDAY_DELETE";
                objDel[1] = dtDel;

                object[] objScheduleDay = new object[2];
                objScheduleDay[0] = "IB_IPC_SCHEDULEDAY_INSERT";
                objScheduleDay[1] = dtScheduleDay;

                hasInput.Add(SmartPortal.Constant.IPC.EBA_PUSHNOTIFICATIONEDIT, objPN);
                hasInput.Add(SmartPortal.Constant.IPC.IPC_SCHEDULEDAYDELETE, objDel);
                hasInput.Add(SmartPortal.Constant.IPC.IPCSCHEDULEDAYINSERT, objScheduleDay);
                hasInput.Add(SmartPortal.Constant.IPC.IPC_SCHEDULESUPDATE, objSchedule);

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

        public DataSet LoadPushNotificationType(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Get trans same ProductLimit
                //return GetTranNameByTrancode(ref errorCode, ref errorDesc);
                ////Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "LOADPNTYPE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load push notification type");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

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
        public DataSet ApprovePushNotification(string ID, string STATUS, string USERID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSAPPROVEPUSH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Approve push notification");


                hasInput.Add(SmartPortal.Constant.IPC.ID, ID);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, STATUS);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, USERID);


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

        public DataSet RejectPushNotification(string ID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSREJECTPUSH");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Approve push notification");
                hasInput.Add(SmartPortal.Constant.IPC.ID, ID);


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
