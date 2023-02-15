using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SmartPortal.SEMS
{
    public class Schedule
    {

        public DataSet InsertScheduleV3(string userID, string scheid, string schetype, string schetime, string schename, string desc, string status, string usercreate, string approved, string isapproved, string serviceid, string actiontype, string trancode, string nextexecute, string createdate, string enddate, DataTable tblScheduleDay, DataTable tblScheDetail, DataTable dtPN, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSPNSCHADD");
                hasInput.Add(SmartPortal.Constant.IPC.SCHEDULEID, scheid);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

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
                row["trancode"] = "SEMSPUSH";
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

                #region Insert push
                object[] pushNotification = new object[2];
                pushNotification[0] = "EBA_PUSHNOTIFICATION_ADD";

                //add vao phan tu thu 2 mang object
                pushNotification[1] = dtPN;
                #endregion

                hasInput.Add(SmartPortal.Constant.IPC.EBA_PUSHNOTIFICATIONADD, pushNotification);

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
