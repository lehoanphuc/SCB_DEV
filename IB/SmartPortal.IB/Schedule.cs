using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using SmartPortal.DAL;
using System.Data.SqlClient;

namespace SmartPortal.IB
{
    public class Schedule
    {
        #region Search customer by condition
        public DataSet Load(string scheduleName, string trancode,string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000408");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load Schedule");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULENAME, scheduleName);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID); 

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

        #region Load transfer type
        public DataSet GetTranNameByTrancode(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPRODUCTLMGTRNAME");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get TranName by trancode");
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

        public DataSet LoadTransferType(string param,string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Get trans same ProductLimit
                //return GetTranNameByTrancode(ref errorCode, ref errorDesc);
                ////Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000011");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load transfer type");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.PARAM, param);
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

        #region Insert Schedule
        public DataSet InsertScheduleV2(string userID, string scheid, string schetype, string schetime, string schename, string desc, string status, string usercreate, string approved, string isapproved, string serviceid, string actiontype, string trancode, string nextexecute, string createdate, string enddate, DataTable tblScheduleDay, DataTable tblScheDetail, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB100000");
                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULEID, scheid);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                
                #region Insert bảng Schedules
                object[] insertSchedule = new object[2];
                insertSchedule[0] = "IB_IPC_SCHEDULES_INSERT";
                //tao bang chua thong tin Schedules
                DataTable tblSchedules = new DataTable();
                DataColumn scheidcol = new DataColumn("scheid");
                DataColumn schetypecol = new DataColumn("schetype");
                DataColumn schetimecol = new DataColumn("schetime");
                DataColumn schenamecol = new DataColumn("schename");
                DataColumn desccol = new DataColumn("desc");
                DataColumn statuscol = new DataColumn("status");
                DataColumn usercreatecol = new DataColumn("usercreate");
                DataColumn approvedcol = new DataColumn("approved");
                DataColumn isapprovedcol = new DataColumn("isapproved");
                DataColumn serviceidcol = new DataColumn("serviceid");
                DataColumn actiontypecol = new DataColumn("actiontype");
                DataColumn trancodecol = new DataColumn("trancode");
                DataColumn nextexecutecol = new DataColumn("nextexecute");
                DataColumn createdatecol = new DataColumn("createdate");
                DataColumn enddatecol = new DataColumn("enddate");


                //add vào table product
                tblSchedules.Columns.Add(scheidcol);
                tblSchedules.Columns.Add(schetypecol);
                tblSchedules.Columns.Add(schetimecol);
                tblSchedules.Columns.Add(schenamecol);
                tblSchedules.Columns.Add(desccol);
                tblSchedules.Columns.Add(statuscol);
                tblSchedules.Columns.Add(usercreatecol);
                tblSchedules.Columns.Add(approvedcol);
                tblSchedules.Columns.Add(isapprovedcol);
                tblSchedules.Columns.Add(serviceidcol);
                tblSchedules.Columns.Add(actiontypecol);
                tblSchedules.Columns.Add(trancodecol);
                tblSchedules.Columns.Add(nextexecutecol);
                tblSchedules.Columns.Add(createdatecol);
                tblSchedules.Columns.Add(enddatecol);


                //tao 1 dong du lieu
                DataRow row = tblSchedules.NewRow();
                row["scheid"] = scheid;
                row["schetype"] = schetype;
                row["schetime"] = schetime;
                row["schename"] = schename;
                row["desc"] = desc;
                row["status"] = status;
                row["usercreate"] = usercreate;
                row["approved"] = approved;
                row["isapproved"] = isapproved;
                row["serviceid"] = serviceid;
                row["actiontype"] = actiontype;
                row["trancode"] = trancode;
                row["nextexecute"] = nextexecute;
                row["createdate"] = createdate;
                row["enddate"] = enddate;


                tblSchedules.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                insertSchedule[1] = tblSchedules;

                hasInput.Add(SmartPortal.Constant.IPC.IPCSCHEDULESINSERT, insertSchedule);
                #endregion


                #region Insert ScheduleDay
                object[] insertScheduleDay = new object[2];
                insertScheduleDay[0] = "IB_IPC_SCHEDULEDAY_INSERT";

                //add vao phan tu thu 2 mang object
                insertScheduleDay[1] = tblScheduleDay;

                hasInput.Add(SmartPortal.Constant.IPC.IPCSCHEDULEDAYINSERT, insertScheduleDay);
                #endregion

                #region Insert ScheduleDetail
                object[] insertScheduleDetail = new object[2];
                insertScheduleDetail[0] = "IB_IPC_SCHEDULEDETAIL_INSERT";

                //add vao phan tu thu 2 mang object
                insertScheduleDetail[1] = tblScheDetail;

                hasInput.Add(SmartPortal.Constant.IPC.IPCSCHEDULEDETAILINSERT, insertScheduleDetail);
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
        

        public DataSet InsertSchedule(string userID,string scheid, string schetype, string schetime, string schename, string desc, string status, string usercreate, string approved, string isapproved, string serviceid, string actiontype, string trancode, string nextexecute, string createdate, string enddate, DataTable tblScheduleDay,DataTable tblScheDetail,string autype,string aucode,string receiverName, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000012");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tạo schedule mới");
                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULEID, scheid);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, autype);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, aucode);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receiverName);

                #region Insert bảng Schedules
                object[] insertSchedule = new object[2];
                insertSchedule[0] = "IB_IPC_SCHEDULES_INSERT";
                //tao bang chua thong tin Schedules
                DataTable tblSchedules = new DataTable();
                DataColumn scheidcol = new DataColumn("scheid");
                DataColumn schetypecol = new DataColumn("schetype");
                DataColumn schetimecol = new DataColumn("schetime");
                DataColumn schenamecol = new DataColumn("schename");
                DataColumn desccol = new DataColumn("desc");
                DataColumn statuscol = new DataColumn("status");
                DataColumn usercreatecol = new DataColumn("usercreate");
                DataColumn approvedcol = new DataColumn("approved");
                DataColumn isapprovedcol = new DataColumn("isapproved");
                DataColumn serviceidcol = new DataColumn("serviceid");
                DataColumn actiontypecol = new DataColumn("actiontype");
                DataColumn trancodecol = new DataColumn("trancode");
                DataColumn nextexecutecol = new DataColumn("nextexecute");
                DataColumn createdatecol = new DataColumn("createdate");
                DataColumn enddatecol = new DataColumn("enddate");


                //add vào table product
                tblSchedules.Columns.Add(scheidcol);
                tblSchedules.Columns.Add(schetypecol);
                tblSchedules.Columns.Add(schetimecol);
                tblSchedules.Columns.Add(schenamecol);
                tblSchedules.Columns.Add(desccol);
                tblSchedules.Columns.Add(statuscol);
                tblSchedules.Columns.Add(usercreatecol);
                tblSchedules.Columns.Add(approvedcol);
                tblSchedules.Columns.Add(isapprovedcol);
                tblSchedules.Columns.Add(serviceidcol);
                tblSchedules.Columns.Add(actiontypecol);
                tblSchedules.Columns.Add(trancodecol);
                tblSchedules.Columns.Add(nextexecutecol);
                tblSchedules.Columns.Add(createdatecol);
                tblSchedules.Columns.Add(enddatecol);


                //tao 1 dong du lieu
                DataRow row = tblSchedules.NewRow();
                row["scheid"] = scheid;
                row["schetype"] = schetype;
                row["schetime"] = schetime;
                row["schename"] = schename;
                row["desc"] = desc;
                row["status"] = status;
                row["usercreate"] = usercreate;
                row["approved"] = approved;
                row["isapproved"] = isapproved;
                row["serviceid"] = serviceid;
                row["actiontype"] = actiontype;
                row["trancode"] = trancode;
                row["nextexecute"] = nextexecute;
                row["createdate"] = createdate;
                row["enddate"] = enddate;


                tblSchedules.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                insertSchedule[1] = tblSchedules;

                hasInput.Add(SmartPortal.Constant.IPC.IPCSCHEDULESINSERT, insertSchedule);
                #endregion


                #region Insert ScheduleDay
                object[] insertScheduleDay = new object[2];
                insertScheduleDay[0] = "IB_IPC_SCHEDULEDAY_INSERT";

                //add vao phan tu thu 2 mang object
                insertScheduleDay[1] = tblScheduleDay;

                hasInput.Add(SmartPortal.Constant.IPC.IPCSCHEDULEDAYINSERT, insertScheduleDay);
                #endregion

                #region Insert ScheduleDetail
                object[] insertScheduleDetail = new object[2];
                insertScheduleDetail[0] = "IB_IPC_SCHEDULEDETAIL_INSERT";

                //add vao phan tu thu 2 mang object
                insertScheduleDetail[1] = tblScheDetail;

                hasInput.Add(SmartPortal.Constant.IPC.IPCSCHEDULEDETAILINSERT, insertScheduleDetail);
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

        public DataSet ApproveSchedule(string userID,string scheid, string schetype, string schename, string desc, string trancode,string amount, string ccyid,string sender, string receiver,string receiverName, ref string errorCode,ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000215");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULEID, scheid);
                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULETYPE, schetype);
                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULENAME, schename);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, sender);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, receiver);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receiverName);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODETORIGHT, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, desc);

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
        //CREATE PHUOCDM
        public Hashtable InsertSchedule(object[] ipcSchedules, object[] ipcScheduleDetails)
        {
            Hashtable output = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000209");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.IPCSCHEDULESINSERT, ipcSchedules);
                input.Add(SmartPortal.Constant.IPC.IPCSCHEDULEDETAILINSERT, ipcScheduleDetails);

                output = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }
        #endregion

        #region CREATE PHUOCDM
        public object[] createObjSchedules(string scheid, string schetype, string schetime, string schename, string desc
                                 , string status, string usercreate, string userapproved, string isapproved,
                                string serviceid, string actiontype, string ipctrancode, string nextexecute,
                                string createdate, string enddate)
        {
            object[] objSchedules = new object[2];
            DataTable tblSchedules = new DataTable();
            try
            {
                tblSchedules.Columns.Add("scheid"); tblSchedules.Columns.Add("schetype");
                tblSchedules.Columns.Add("schetime"); tblSchedules.Columns.Add("schename");
                tblSchedules.Columns.Add("desc"); tblSchedules.Columns.Add("status");
                tblSchedules.Columns.Add("usercreate"); tblSchedules.Columns.Add("userapproved");
                tblSchedules.Columns.Add("isapproved"); tblSchedules.Columns.Add("serviceid");
                tblSchedules.Columns.Add("actiontype"); tblSchedules.Columns.Add("trancode");
                tblSchedules.Columns.Add("nextexecute"); tblSchedules.Columns.Add("createdate");
                tblSchedules.Columns.Add("enddate");

                tblSchedules.Rows.Add(scheid, schetype, schetime, schename, desc
                                 , status, usercreate, userapproved, isapproved,
                                 serviceid, actiontype, ipctrancode, nextexecute,
                                 createdate, enddate);
                objSchedules[0] = "IB_IPC_SCHEDULES_INSERT";
                objSchedules[1] = tblSchedules;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objSchedules;
        }
        //CREATE PHUOCDM
        public object[] createObjScheduleDetails(DataTable schedulesInfo)
        {
            object[] objSchedules = new object[2];
            try
            {
                objSchedules[0] = "IB_IPC_SCHEDULEDETAIL_INSERT";
                objSchedules[1] = schedulesInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objSchedules;
        }
        //CREATE PHUOCDM
        public DataTable createObjScheduleDay()
        {
            DataTable tblScheduleDay = new DataTable();
            try
            {
            }
            catch (Exception ex)
            { throw ex; }
            
            return tblScheduleDay;
        }
        #endregion

        #region delete schedule by scheduleid
        public DataSet DeleteScheduleByID(string ScheID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000013");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "delete Schedule");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULEID, ScheID);

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

        #region SELECT schedule by scheduleid
        public DataSet GetInfo_Schedule_ByID(string ScheID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000014");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lay thong tin chi tiet lich");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULEID, ScheID);

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

        #region SELECT scheduleDetails by scheduleid
        public DataSet GetInfo_ScheduleDetail_ByID(string ScheID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000015");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "select info ipc_ScheduleDetail");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULEID, ScheID);

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

        #region SELECT scheduleday by scheduleid
        public DataSet GetInfo_ScheduleDay_ByID(string ScheID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000016");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "select info ipc_ScheduleDay");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULEID, ScheID);

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

        #region Get NAME OF  child bank
        public DataTable GetChildBank(string childbank)
        {
            try
            {
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@BANKCODE";
                p1.Value = childbank;
                p1.SqlDbType = SqlDbType.VarChar;

                return DataAccess.GetFromDataTable("IB_EBA_BANKLIST_GETBANKNAME", p1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Schedule
        public DataSet GetSchedule(string Scheduleid, string UserCreate, string UserApprove, string ScheduleType, string FromDate, string ToDate,string Trancode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS000004");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULEID, Scheduleid);
                hasInput.Add(SmartPortal.Constant.IPC.USERCREATE, UserCreate);
                hasInput.Add(SmartPortal.Constant.IPC.USERAPPROVED, UserApprove);
                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULETYPE, ScheduleType);
                hasInput.Add(SmartPortal.Constant.IPC.FROMDATE, FromDate);
                hasInput.Add(SmartPortal.Constant.IPC.TODATE, ToDate);
                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULEACTION, Trancode);
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
    }
}
