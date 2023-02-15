using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.Data;
using DBConnection;
using Formatters;
using System.Collections;
using Interfaces;
using Schedules;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NCalc2;
using System.Net;

namespace Transaction
{
    public class SpecialTrans
    {
        Assembly assembly = null;
        Type type = null;
        string method = string.Empty;
        object instance = null;
        object result = null;

        public static void CheckCPUUsageAndSleepThread(PerformanceCounter cpuCounter)
        {
            int CPU = 70;
            int sleeptime = 1;
            try
            {
                CPU = int.Parse(ConfigurationManager.AppSettings.Get("VALUEMAXCPU"));
            }
            catch
            {
                CPU = 70;
            }

            try
            {
                sleeptime = int.Parse(ConfigurationManager.AppSettings.Get("TIMESLEEPWHENFULLCPU"));
            }
            catch
            {
                sleeptime = 1;
            }
            if (cpuCounter.NextValue() > CPU) //Check if CPU utilization crosses 80%  
            {
                Thread.Sleep(sleeptime);
            }
        }

        #region PUBLIC FUNCTION

        public bool LogBatchTrans(TransactionInfo batchTran, string parmList)
        {
            try
            {
                if (batchTran.Data.ContainsKey(Common.KEYNAME.APPROVED) && batchTran.Data[Common.KEYNAME.APPROVED].ToString() == "Y")
                {
                    return true;
                }
                string[] parm = parmList.Split('|');
                DataTable lstTran = (DataTable)batchTran.Data[parm[0]];
                string tranCode = string.Empty;
                if (batchTran.Data.ContainsKey(parm[1]))
                {
                    tranCode = batchTran.Data[parm[1]].ToString();
                }
                else
                {
                    tranCode = parm[1];
                }
                if (batchTran.Data.ContainsKey(Common.KEYNAME.APPROVED) && batchTran.Data[Common.KEYNAME.APPROVED].ToString() == "Y")
                {
                    return true;
                }


                ParallelLoopResult result = Parallel.For(0, lstTran.Rows.Count,
                   new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (int i) =>
                   {
                       try
                       {
                           PerformanceCounter cpuCounter =
                                     new PerformanceCounter("Processor", "% Processor Time", "_Total");
                           CheckCPUUsageAndSleepThread(cpuCounter);
                           Thread.Sleep(1);

                           TransactionInfo tran = new TransactionInfo();
                           tran.MessageTypeSource = Common.MESSAGETYPE.HAS;
                           tran.NewIPCTransID();
                           Hashtable InputData = new Hashtable();
                           foreach (DataColumn col in lstTran.Columns)
                           {
                               InputData.Add(col.ColumnName, lstTran.Rows[i][col.ColumnName]);
                           }
                           InputData.Add(Common.KEYNAME.SOURCEID, batchTran.Data[Common.KEYNAME.SOURCEID]);
                           InputData.Add(Common.KEYNAME.USERID, batchTran.Data[Common.KEYNAME.USERID]);
                           InputData.Add(Common.KEYNAME.IPCTRANCODE, tranCode);
                           InputData.Add(Common.KEYNAME.BATCHREF, batchTran.IPCTransID);
                           InputData.Add(Common.KEYNAME.ISBATCH, "Y");

                           if (Formatter.AnalyzeRequestHAS(tran, InputData) == false || Formatter.AddDataDefine(tran) == false)
                           {
                               throw new Exception("Error in batchref: " + batchTran.IPCTransID.ToString() + ", transaction info:" + lstTran.Rows[i][Common.KEYNAME.ACCTNO].ToString() + " amount:" + lstTran.Rows[i][Common.KEYNAME.AMOUNT].ToString());
                           }
                           TransLib log = new TransLib();
                           log.LogTransaction(tran);
                           log.LogTransactionDetail(tran, "I");
                       }
                       catch (Exception ex)
                       {
                           ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                                System.Reflection.MethodBase.GetCurrentMethod().Name);
                       }
                   }
                );

                Connection dbObj = new Connection();
                DataTable lstTransdb = dbObj.FillDataTable(Common.ConStr, "IPC_BATCHSELECTTRANS", batchTran.IPCTransID);
                if (lstTransdb.Rows.Count != lstTran.Rows.Count)
                {
                    ProcessLog.LogInformation("Inserted batch trans does not match with request");
                    throw new Exception("Transaction error, please try again later");
                }

                decimal cTotalAmt = decimal.Parse(batchTran.Data["AMOUNT"].ToString());
                decimal dTotalAmt = lstTransdb.AsEnumerable().Sum(x => x.Field<decimal>("AMOUNT"));

                if (cTotalAmt != dTotalAmt)
                {
                    ProcessLog.LogInformation("Total amount does not match with request");
                    throw new Exception("Transaction error, please try again later");
                }

                return true;

            }
            catch (Exception ex)
            {
                batchTran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                DeleteBatchTrans(batchTran.IPCTransID.ToString());
                return false;
            }
        }

        static object _lockExportBatchTrans = new object();
        public bool ExportBatchTrans(TransactionInfo batchTran)
        {
            try
            {
                Connection dbObj = new Connection();
                DataTable lstTrans = new DataTable();
                AutoTrans execTran = new AutoTrans();
                DataTable dtBatch = new DataTable();
                Double totalSedFee = 0;
                Double totalRevFee = 0;
                lstTrans = dbObj.FillDataTable(Common.ConStr, "IPC_BATCHSELECTTRANS", batchTran.IPCTransID);

                //create table
                dtBatch.Columns.Add(Common.KEYNAME.TRANREF, typeof(string));
                dtBatch.Columns.Add(Common.KEYNAME.BATCHID, typeof(string));
                dtBatch.Columns.Add(Common.KEYNAME.FACCTNO, typeof(string));
                dtBatch.Columns.Add(Common.KEYNAME.TACCTNO, typeof(string));
                dtBatch.Columns.Add(Common.KEYNAME.AMOUNT, typeof(string));
                dtBatch.Columns.Add(Common.KEYNAME.CCYID, typeof(string));
                dtBatch.Columns.Add(Common.KEYNAME.FEESEN, typeof(string));
                dtBatch.Columns.Add(Common.KEYNAME.FEEREC, typeof(string));
                dtBatch.Columns.Add(Common.KEYNAME.TRANDESC, typeof(string));


                ParallelLoopResult result = Parallel.For(0, lstTrans.Rows.Count,
                   new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (int i) =>
                   {
                       try
                       {
                           PerformanceCounter cpuCounter =
                                     new PerformanceCounter("Processor", "% Processor Time", "_Total");
                           CheckCPUUsageAndSleepThread(cpuCounter);
                           Thread.Sleep(1);


                           double sedfee = 0;
                           double revfee = 0;
                           DataTable dtFee = new DataTable();

                           ProcessLog.LogInformation($"ExportBatchTrans - Cal Fee INPUT transaction {lstTrans.Rows[i][Common.KEYNAME.IPCTRANSID].ToString()} Store name = IPC_CALCULATORTRANSFEEV2 paramvalue: { lstTrans.Rows[i][Common.KEYNAME.USERID].ToString()} - {batchTran.Data[Common.KEYNAME.IPCTRANCODE].ToString()} - {lstTrans.Rows[i][Common.KEYNAME.AMOUNT].ToString()} - { lstTrans.Rows[i][Common.KEYNAME.ACCTNO].ToString()} - {lstTrans.Rows[i][Common.KEYNAME.RECEIVERACCOUNT].ToString()} - {lstTrans.Rows[i][Common.KEYNAME.CCYID].ToString()}", Common.FILELOGTYPE.LOGDOSTOREINFO);

                           dtFee = dbObj.FillDataTable(Common.ConStr, "IPC_CALCULATORTRANSFEEV2", lstTrans.Rows[i][Common.KEYNAME.USERID].ToString(), batchTran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), lstTrans.Rows[i][Common.KEYNAME.AMOUNT].ToString(), lstTrans.Rows[i][Common.KEYNAME.ACCTNO].ToString(), lstTrans.Rows[i][Common.KEYNAME.RECEIVERACCOUNT].ToString(), lstTrans.Rows[i][Common.KEYNAME.CCYID].ToString(), "");

                           string msglog = $"DoStore OUTPUT transaction {lstTrans.Rows[i][Common.KEYNAME.IPCTRANSID].ToString()} with Store name IPC_CALCULATORTRANSFEEV2" + Environment.NewLine;
                           msglog += $"{JsonConvert.SerializeObject(dtFee)}";
                           ProcessLog.LogInformation(msglog, Common.FILELOGTYPE.LOGDOSTOREINFO);

                           if (dtFee != null || dtFee.Rows.Count > 0)
                           {
                               sedfee = double.Parse(dtFee.Rows[0][Common.KEYNAME.SEDFEE].ToString());
                               revfee = double.Parse(dtFee.Rows[0][Common.KEYNAME.REVFEE].ToString());
                           }

                           dtBatch.Rows.Add(lstTrans.Rows[i][Common.KEYNAME.IPCTRANSID], lstTrans.Rows[i][Common.KEYNAME.BATCHREF], lstTrans.Rows[i][Common.KEYNAME.ACCTNO], lstTrans.Rows[i][Common.KEYNAME.RECEIVERACCOUNT],
                               lstTrans.Rows[i][Common.KEYNAME.AMOUNT], lstTrans.Rows[i][Common.KEYNAME.CCYID], sedfee.ToString(), revfee.ToString(),
                               batchTran.Data[Common.KEYNAME.TRANDESC].ToString().Replace("'", "''"));
                           try
                           {
                               dbObj.FillDataTable(Common.ConStr, "IPC_UPDATEBATCHTRANS",
                                    int.Parse(lstTrans.Rows[i][Common.KEYNAME.IPCTRANSID].ToString()),
                                    batchTran.Data[Common.KEYNAME.TRANDESC].ToString(), sedfee, revfee);
                           }
                           catch (Exception e)
                           {
                               ProcessLog.LogError(new Exception($"ExportBatchTrans - Update batch trans {lstTrans.Rows[i][Common.KEYNAME.IPCTRANSID].ToString()} - {e.Message}"), Assembly.GetExecutingAssembly().ToString(),
                                   MethodBase.GetCurrentMethod()?.Name);
                           }

                           lock (_lockExportBatchTrans)
                           {
                               totalSedFee += sedfee;
                               totalRevFee += revfee;
                           }
                       }
                       catch (Exception ex)
                       {
                           ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                                System.Reflection.MethodBase.GetCurrentMethod().Name);
                       }
                   }
                );

                string batchBase64 = Common.DataTable2Base64Excel(dtBatch);
                string fileName = batchTran.Data[Common.KEYNAME.USERID].ToString() + batchTran.IPCTransID + "_input.xls";


                //tinh tong fee
                batchTran.Data.Add(Common.KEYNAME.SEDFEE, totalSedFee);
                batchTran.Data.Add(Common.KEYNAME.REVFEE, totalRevFee);

                if (!string.IsNullOrEmpty(batchBase64))
                {
                    if (batchTran.Data.Contains(Common.KEYNAME.BATCHDATABASE64))
                    {
                        batchTran.Data[Common.KEYNAME.BATCHDATABASE64] = batchBase64;
                    }
                    else
                    {
                        batchTran.Data.Add(Common.KEYNAME.BATCHDATABASE64, batchBase64);
                    }

                    if (batchTran.Data.Contains(Common.KEYNAME.FILENAME))
                    {
                        batchTran.Data[Common.KEYNAME.FILENAME] = fileName;
                    }
                    else
                    {
                        batchTran.Data.Add(Common.KEYNAME.FILENAME, fileName);
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                batchTran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                DeleteBatchTrans(batchTran.IPCTransID.ToString());
                return false;
            }
        }


        public bool ScheduleGetBatchResult(TransactionInfo tran)
        {
            try
            {
                string ServerTime = "";

                if (!tran.ErrorCode.Equals(Common.ERRORCODE.OK) && tran.Data.ContainsKey(Common.KEYNAME.STRDESTRESULT))
                {

                    Utility.ProcessLog.LogInformation("Upload batch file error, transid is " + tran.IPCTransID);
                    tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                    return false;
                }
                else
                {
                    ServerTime = tran.Data["RESULTCOREBAT"].ToString();
                }

                if (string.IsNullOrEmpty(ServerTime))
                {
                    Utility.ProcessLog.LogInformation("Server time is null, transid is " + tran.IPCTransID);
                    tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                    return false;
                }

                Connection dbObj = new Connection();
                DataTable lstTrans = new DataTable();
                AutoTrans execTran = new AutoTrans();
                DataTable dtBatch = new DataTable();
                double TRetry = 30;
                double Distance = 60;
                string dtFormat = "dd/MM/yyyy HH:mm:ss";

                //get expand time config
                try
                {
                    TRetry = double.Parse(ConfigurationManager.AppSettings["BatchNumberRetry"].ToString());
                    Distance = double.Parse(ConfigurationManager.AppSettings["BatchDistance"].ToString());
                    dtFormat = ConfigurationManager.AppSettings["ScheduleDateTimeFormat"].ToString();
                }
                catch { }

                //get scheduleid
                DataTable dtScheduleID = dbObj.FillDataTable(Common.ConStr, "SCHEDULE_GETID", null);
                if (dtScheduleID.Rows.Count == 0)
                {
                    throw new Exception("Error when get batch schedule id : " + tran.IPCTransID.ToString());
                }

                //make schedule input
                Hashtable hasSchInput = new Hashtable();
                Hashtable hasSchOuput = new Hashtable();

                string ScheduleID = dtScheduleID.Rows[0][0].ToString();
                hasSchInput.Add(Common.KEYNAME.IPCTRANCODE, "IB100001");
                hasSchInput.Add(Common.KEYNAME.SCHEDULEID, ScheduleID);
                hasSchInput.Add(Common.KEYNAME.SOURCEID, Common.KEYNAME.SOURCEIBVALUE);

                #region Insert table Schedules
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
                row["scheid"] = ScheduleID;
                row["schetype"] = Common.KEYNAME.REPEATEDLY;
                row["schetime"] = DateTime.Now.ToString(dtFormat);
                row["schename"] = tran.Data[Common.KEYNAME.USERID].ToString() + tran.IPCTransID.ToString();
                row["desc"] = tran.Data[Common.KEYNAME.TRANDESC].ToString();
                row["status"] = "A";
                row["usercreate"] = tran.Data[Common.KEYNAME.USERID].ToString();
                row["approved"] = tran.Data[Common.KEYNAME.USERID].ToString();
                row["isapproved"] = "Y";
                row["serviceid"] = Common.KEYNAME.SOURCEIBVALUE;
                row["actiontype"] = "";
                row["trancode"] = Common.KEYNAME.BATCHSCHEDULETRANCODE;
                row["nextexecute"] = DateTime.Now.AddSeconds(Distance).ToString(dtFormat);
                row["createdate"] = DateTime.Now.ToString(dtFormat);
                row["enddate"] = DateTime.Now.AddSeconds(Distance * (TRetry + 1)).ToString(dtFormat); //end date


                tblSchedules.Rows.Add(row);

                //add vao phan tu thu 2 mang object
                insertSchedule[1] = tblSchedules;

                hasSchInput.Add(Common.KEYNAME.IPCSCHEDULESINSERT, insertSchedule);
                #endregion

                #region Insert table ScheduleDay
                object[] insertScheduleDay = new object[2];
                insertScheduleDay[0] = "IB_IPC_SCHEDULEDAY_INSERT";
                DataTable tblScheduleDay = new DataTable();
                DataColumn ScheduleDayIDCol = new DataColumn("ScheduleDayID");
                DataColumn DayNoCol = new DataColumn("DayNo");

                tblScheduleDay.Columns.AddRange(new DataColumn[] { ScheduleDayIDCol, DayNoCol });
                insertScheduleDay[1] = tblScheduleDay;

                hasSchInput.Add(Common.KEYNAME.IPCSCHEDULEDAYINSERT, insertScheduleDay);
                #endregion

                #region Insert table ScheduleDetail
                object[] insertScheduleDetail = new object[2];
                insertScheduleDetail[0] = "IB_IPC_SCHEDULEDETAIL_INSERT";
                DataTable tblScheDetail = new DataTable();

                DataColumn ScheIDCol = new DataColumn("ScheduleID");
                DataColumn ParaNameCol = new DataColumn("ParaName");
                DataColumn ParaValueCol = new DataColumn("ParaValue");

                tblScheDetail.Columns.AddRange(new DataColumn[] { ScheIDCol, ParaNameCol, ParaValueCol });

                DataRow r0 = tblScheDetail.NewRow();
                r0["ScheduleID"] = ScheduleID;
                r0["ParaName"] = Common.KEYNAME.SERVERTIME;
                r0["ParaValue"] = ServerTime;
                tblScheDetail.Rows.Add(r0);

                DataRow r1 = tblScheDetail.NewRow();
                r1["ScheduleID"] = ScheduleID;
                r1["ParaName"] = Common.KEYNAME.BATCHID;
                r1["ParaValue"] = tran.IPCTransID.ToString();
                tblScheDetail.Rows.Add(r1);

                DataRow r2 = tblScheDetail.NewRow();
                r2["ScheduleID"] = ScheduleID;
                r2["ParaName"] = Common.KEYNAME.TRANDESC;
                r2["ParaValue"] = tran.Data[Common.KEYNAME.TRANDESC].ToString();
                tblScheDetail.Rows.Add(r2);

                DataRow r3 = tblScheDetail.NewRow();
                r3["ScheduleID"] = ScheduleID;
                r3["ParaName"] = Common.KEYNAME.USERID;
                r3["ParaValue"] = tran.Data[Common.KEYNAME.USERID].ToString();
                tblScheDetail.Rows.Add(r3);

                DataRow r4 = tblScheDetail.NewRow();
                r4["ScheduleID"] = ScheduleID;
                r4["ParaName"] = Common.KEYNAME.SOURCEID;
                r4["ParaValue"] = Common.KEYNAME.SOURCEIBVALUE;
                tblScheDetail.Rows.Add(r4);

                //add vao phan tu thu 2 mang object
                insertScheduleDetail[1] = tblScheDetail;

                hasSchInput.Add(Common.KEYNAME.IPCSCHEDULEDETAILINSERT, insertScheduleDetail);
                #endregion
                #region Insert table ScheduleRepeat
                object[] insertScheduleRepeat = new object[2];
                insertScheduleRepeat[0] = "IB_IPC_SCHEDULEREPEAT_INSERT";
                DataTable tblScheRepeat = new DataTable();

                DataColumn scherepeatidcol = new DataColumn(Common.KEYNAME.SCHEDULEID);
                DataColumn schedistancecol = new DataColumn("DISTANCE");
                DataColumn schenretrycol = new DataColumn("NRETRY");
                DataColumn schetretrycol = new DataColumn("TRETRY");

                tblScheRepeat.Columns.AddRange(new DataColumn[] { scherepeatidcol, schedistancecol, schenretrycol, schetretrycol });

                DataRow ror = tblScheRepeat.NewRow();
                ror[Common.KEYNAME.SCHEDULEID] = ScheduleID;
                ror["DISTANCE"] = Distance;
                ror["NRETRY"] = 0;
                ror["TRETRY"] = TRetry;
                tblScheRepeat.Rows.Add(ror);

                //add vao phan tu thu 2 mang object
                insertScheduleRepeat[1] = tblScheRepeat;

                hasSchInput.Add(Common.KEYNAME.IPCSCHEDULEREPEATINSERT, insertScheduleRepeat);
                #endregion

                //AutoTrans execTran = new AutoTrans();
                hasSchOuput = execTran.ProcessTransHAS(hasSchInput);
                tran.ErrorCode = hasSchOuput[Common.KEYNAME.IPCERRORCODE].ToString();
                tran.ErrorDesc = hasSchOuput[Common.KEYNAME.IPCERRORDESC].ToString();

                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                DeleteBatchTrans(tran.IPCTransID.ToString());
                return false;
            }
        }
        public bool UpdateBatchResult(TransactionInfo tran)
        {
            try
            {
                //System.Diagnostics.Debugger.Launch();
                Connection con = new Connection();
                DataTable lstTrans = new DataTable();
                AutoTrans execTran = new AutoTrans();
                string base64result = string.Empty;

                if (!tran.ErrorCode.Equals(Common.ERRORCODE.OK) && tran.Data.ContainsKey(Common.KEYNAME.STRDESTRESULT))
                {

                    Utility.ProcessLog.LogInformation("Download batch result error, transid is " + tran.IPCTransID);
                    return false;
                }

                base64result = tran.Data["DATACONTENTBASE64"].ToString();
                if (string.IsNullOrEmpty(base64result))
                {
                    Utility.ProcessLog.LogInformation("Batch result string empty, transid is " + tran.IPCTransID);
                    return false;
                }
                DataTable tblResult = Common.Base64Excel2Datatable(base64result);
                if (tblResult.Rows.Count == 0)
                {
                    Utility.ProcessLog.LogInformation("Batch result datatable empty, transid is " + tran.IPCTransID);
                    return false;
                }

                if (tran.Data.ContainsKey(Common.KEYNAME.BATCHRESULT))
                {
                    tran.Data[Common.KEYNAME.BATCHRESULT] = tblResult;
                }
                else
                {
                    tran.Data.Add(Common.KEYNAME.BATCHRESULT, tblResult);
                }

                ParallelLoopResult result = Parallel.For(0, tblResult.Rows.Count,
                   new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (int i) =>
                   {
                       try
                       {
                           PerformanceCounter cpuCounter =
                                     new PerformanceCounter("Processor", "% Processor Time", "_Total");
                           CheckCPUUsageAndSleepThread(cpuCounter);
                           Thread.Sleep(1);


                           Hashtable hasResult = new Hashtable();
                           string ERRORCODE = tblResult.Rows[i][Common.KEYNAME.ERRORCODE].ToString().Equals("00") ? Common.ERRORCODE.OK : tblResult.Rows[i][Common.KEYNAME.ERRORCODE].ToString();
                           string ERRORDESC = tblResult.Rows[i][Common.KEYNAME.ERRORDESC].ToString();

                           hasResult.Add(Common.KEYNAME.IPCTRANCODE, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString());
                           hasResult.Add(Common.KEYNAME.TRANREF, tblResult.Rows[i][Common.KEYNAME.TRANREF]);
                           hasResult.Add(Common.KEYNAME.BATCHID, tblResult.Rows[i][Common.KEYNAME.BATCHID]);
                           hasResult.Add(Common.KEYNAME.FACCTNO, tblResult.Rows[i][Common.KEYNAME.FACCTNO]);
                           hasResult.Add(Common.KEYNAME.TACCTNO, tblResult.Rows[i][Common.KEYNAME.TACCTNO]);
                           hasResult.Add(Common.KEYNAME.AMOUNT, tblResult.Rows[i][Common.KEYNAME.AMOUNT]);
                           hasResult.Add(Common.KEYNAME.TRANDESC, tblResult.Rows[i][Common.KEYNAME.TRANDESC]);
                           hasResult.Add(Common.KEYNAME.DEBITBALANCE, tblResult.Rows[i][Common.KEYNAME.DEBITBALANCE]);
                           hasResult.Add(Common.KEYNAME.CREDITBALANCE, tblResult.Rows[i][Common.KEYNAME.CREDITBALANCE]);
                           hasResult.Add(Common.KEYNAME.APPRSTS, Common.APPROVESTATUS.APPROVED);
                           hasResult.Add(Common.KEYNAME.OFFLSTS, Common.OFFLSTS.BEGIN);
                           hasResult.Add(Common.KEYNAME.DELETED, Common.DELSTS.NORMAL);
                           hasResult.Add(Common.KEYNAME.DESTID, Common.KEYNAME.DESTIDVALUE);
                           hasResult.Add(Common.KEYNAME.DESTTRANREF, "");

                           hasResult.Add(Common.KEYNAME.DESTERRORCODE, (string.IsNullOrEmpty(ERRORCODE) || ERRORCODE.Equals("null")) ? Common.ERRORCODE.SYSTEM : ERRORCODE);
                           hasResult.Add(Common.KEYNAME.ERRORCODE, (string.IsNullOrEmpty(ERRORCODE) || ERRORCODE.Equals("null")) ? Common.ERRORCODE.SYSTEM : ERRORCODE);
                           if (!ERRORCODE.Equals(Common.ERRORCODE.OK))
                           {
                               hasResult.Add(Common.KEYNAME.ERRORDESC, (string.IsNullOrEmpty(ERRORCODE) || ERRORCODE.Equals("null")) ? "Transaction error" : ERRORDESC);
                           }
                           else
                           {
                               hasResult.Add(Common.KEYNAME.ERRORDESC, "");
                           }
                           hasResult.Add(Common.KEYNAME.STATUS, (ERRORCODE.Equals(Common.ERRORCODE.OK)) ? Common.TRANSTATUS.FINISH : Common.TRANSTATUS.ERROR);



                           try
                           {
                               UpdateBatchTransaction(hasResult);
                           }
                           catch (Exception e)
                           {

                               ProcessLog.LogError(new Exception($"UpdateBatchResult - UpdateBatchTransaction {lstTrans.Rows[i][Common.KEYNAME.IPCTRANSID].ToString()} - {e.Message}"), Assembly.GetExecutingAssembly().ToString(),
                                   MethodBase.GetCurrentMethod()?.Name);
                           }
                       }
                       catch (Exception ex)
                       {
                           ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                                System.Reflection.MethodBase.GetCurrentMethod().Name);
                       }
                   }
                );
                con.ExecuteNonquery(Common.ConStr, "IB_IPC_SCHEDULES_UPDATESTATUS", tran.Data[Common.KEYNAME.SCHEDULEID].ToString());
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public void UpdateBatchTransaction(Hashtable hasResult)
        {
            try
            {

                Connection con = new Connection();
                con.ExecuteNonquery(Common.ConStr, "IPCLOGTRANS_UPDATE",
                                        Common.IPCWorkDate,
                                        hasResult[Common.KEYNAME.TRANREF].ToString(),
                                        hasResult[Common.KEYNAME.STATUS].ToString(),
                                        hasResult[Common.KEYNAME.APPRSTS].ToString(),
                                        hasResult[Common.KEYNAME.OFFLSTS].ToString(),
                                        hasResult[Common.KEYNAME.DELETED].ToString(),
                                        hasResult[Common.KEYNAME.DESTID].ToString(),
                                        hasResult[Common.KEYNAME.DESTTRANREF].ToString(),
                                        hasResult[Common.KEYNAME.DESTERRORCODE].ToString(),
                                        hasResult[Common.KEYNAME.ERRORCODE].ToString(),
                                        hasResult[Common.KEYNAME.ERRORDESC].ToString(),
                                        Common.ServiceStarted);


                //log update define
                DataRow[] dtrLogDefine = Common.DBILOGDEFINE.Select("(IPCTRANCODE = '' OR IPCTRANCODE = '" +
                                               hasResult[Common.KEYNAME.IPCTRANCODE].ToString() + "') AND LOGTYPE = 'U'");
                if (dtrLogDefine.Length > 0)
                {
                    object[] parm = new object[21];
                    for (int i = 1; i < 11; i++)
                    {
                        parm[i] = "";
                    }
                    for (int i = 11; i < 21; i++)
                    {
                        parm[i] = 0;
                    }
                    //add cac parm char,num
                    parm[0] = hasResult[Common.KEYNAME.TRANREF].ToString();
                    for (int i = 0; i < dtrLogDefine.Length; i++)
                    {
                        if (hasResult.ContainsKey(dtrLogDefine[i]["FIELDNAME"].ToString()))
                        {
                            if (dtrLogDefine[i]["PARMTYPE"].ToString() == "N")
                            {
                                try
                                {
                                    parm[int.Parse(dtrLogDefine[i]["POS"].ToString()) + 10] =
                                        double.Parse(hasResult[dtrLogDefine[i]["FIELDNAME"].ToString()].ToString());
                                }
                                catch
                                {
                                    parm[int.Parse(dtrLogDefine[i]["POS"].ToString()) + 10] = 0;
                                }
                            }
                            else
                            {
                                try
                                {
                                    parm[int.Parse(dtrLogDefine[i]["POS"].ToString())] =
                                        hasResult[dtrLogDefine[i]["FIELDNAME"].ToString()].ToString();
                                }
                                catch
                                {
                                    parm[int.Parse(dtrLogDefine[i]["POS"].ToString())] = "";
                                }
                            }
                        }
                    }

                    con.ExecuteNonquery(Common.ConStr, "IPC_UPDATELOGDEFINE", parm);
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogInformation("Error when update IPCLOGTRANS");
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        //vutran 06062015
        public bool ExecReversal(TransactionInfo tran)
        {
            try
            {
                if (tran.ErrorCode.Equals(Common.ERRORCODE.OK))
                {
                    AutoTrans execTran = new AutoTrans();
                    DataTable dtReLst = ((DataSet)tran.Data[Common.KEYNAME.SELECTRESULT]).Tables[0];
                    if (dtReLst.Rows.Count <= 0) return true;

                    foreach (DataRow dr in dtReLst.Rows)
                    {
                        Hashtable hasInput = new Hashtable();
                        Hashtable hasOutput = new Hashtable();

                        hasInput.Add(Common.KEYNAME.IPCTRANCODE, "IB000000");
                        hasInput.Add(Common.KEYNAME.SOURCEID, Common.KEYNAME.SOURCEIBVALUE);
                        hasInput.Add(Common.KEYNAME.TRANREF, dr[Common.KEYNAME.IPCTRANSID]);

                        hasOutput = execTran.ProcessTransHAS(hasInput);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool ExecBatchTrans(TransactionInfo batchTran) //backup 20042015
        {
            try
            {
                Connection dbObj = new Connection();
                DataTable lstTrans = new DataTable();
                lstTrans = dbObj.FillDataTable(Common.ConStr, "IPC_BATCHSELECTTRANS", batchTran.IPCTransID);
                AutoTrans execTran = new AutoTrans();
                foreach (DataRow row in lstTrans.Rows)
                {
                    TransactionInfo tran = new TransactionInfo();
                    Hashtable InputData = new Hashtable();
                    foreach (DataColumn col in lstTrans.Columns)
                    {
                        InputData.Add(col.ColumnName, row[col.ColumnName]);
                    }
                    tran.MessageTypeSource = Common.MESSAGETYPE.HAS;
                    tran.IPCTransID = long.Parse(InputData[Common.KEYNAME.IPCTRANSID].ToString());
                    if (Formatter.AnalyzeRequestHAS(tran, InputData) == false || Formatter.AddDataDefine(tran) == false)
                    {
                        throw new Exception("Error in batchref: " + batchTran.IPCTransID.ToString() +
                            ", transaction info:" + row[Common.KEYNAME.ACCTNO].ToString() + " amount:" +
                            row[Common.KEYNAME.AMOUNT].ToString());
                    }
                    tran.Data.Add(Common.KEYNAME.APPROVED, "Y");

                    //HaiNT edit Fee for batch transfer
                    DataTable result = new DataTable();
                    result = dbObj.FillDataTable(Common.ConStr, "IPC_CALCULATORTRANSFEE", tran.Data[Common.KEYNAME.TRANDESC].ToString(), tran.Data["IPCTRANCODE"].ToString(),
                        tran.Data[Common.KEYNAME.AMOUNT].ToString(), tran.Data[Common.KEYNAME.ACCTNO].ToString(), "",
                        tran.Data[Common.KEYNAME.CCYID].ToString(), "");
                    if (result == null || result.Rows.Count == 0)
                    {
                        tran.ErrorCode = Common.ERRORCODE.EXECNONQUERY_FALSE;
                        throw new Exception("Error execute data");
                    }

                    //vutran 10022015
                    DataTable bra = new DataTable();
                    bra = dbObj.FillDataTable(Common.ConStr, "IB_SELECTBRANCH_BYACCTNO",
                        tran.Data[Common.KEYNAME.ACCTNO].ToString());
                    if (result == null || result.Rows.Count == 0)
                    {
                        tran.ErrorCode = Common.ERRORCODE.EXECNONQUERY_FALSE;
                        throw new Exception("Error execute data");
                    }
                    else
                    {
                        tran.Data.Add(Common.KEYNAME.DEBITBRACHID, bra.Rows[0][0].ToString());
                    }
                    tran.Data[Common.KEYNAME.TRANDESC] = batchTran.Data[Common.KEYNAME.TRANDESC].ToString();

                    execTran.ProcessTrans(tran);
                }
            }
            catch (Exception ex)
            {
                batchTran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                DeleteBatchTrans(batchTran.IPCTransID.ToString());
                return false;
            }
            return true;
        }
        public bool Runschedule(TransactionInfo tran)
        {
            IPCSchedule schedule = new IPCSchedule();
            try
            {
                schedule.RunSchedules();
            }
            catch
            {
                return false;
            }
            finally
            {
                schedule = null;
            }
            return true;
        }

        public bool CheckPrepaidNew(TransactionInfo tran)
        {
            try
            {
                Hashtable inputdata = new Hashtable();
                inputdata.Add("USERNAME", tran.Data["USERNAME"].ToString());
                inputdata.Add("PASSWORD", tran.Data["PASSWORD"].ToString());
                inputdata.Add("NAME", tran.Data["NAME"].ToString());
                inputdata.Add("CREATEID", tran.Data["CREATEID"].ToString());

                if (instance == null) LoadInfo();
                result = type.InvokeMember(method, System.Reflection.BindingFlags.InvokeMethod, null, instance, new object[] { inputdata });
                if ((bool)result == false)
                {
                    tran.ErrorCode = "-1";
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }
        #endregion


        #region
        private bool DeleteBatchTrans(string batchRef)
        {
            try
            {
                Connection dbObj = new Connection();
                dbObj.ExecuteNonquery(Common.ConStr, "IPC_BATCHTRANSDEL", batchRef);
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        private void LoadInfo()
        {
            try
            {
                assembly = Assembly.LoadFrom(ConfigurationManager.AppSettings["ASSEMBLYNAME"].ToString());
                type = assembly.GetType(ConfigurationManager.AppSettings["ASSEMBLYTYPE"].ToString());
                method = ConfigurationManager.AppSettings["MTMETHOD"].ToString();
                instance = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region get set system time
        //get set system time
        public struct SYSTEMTIME
        {
            public ushort wYear, wMonth, wDayOfWeek, wDay,
               wHour, wMinute, wSecond, wMilliseconds;
        }

        /// <summary>
        /// This function retrieves the current system date
        /// and time expressed in Coordinated Universal Time (UTC).
        /// </summary>
        /// <param name="lpSystemTime">[out] Pointer to a SYSTEMTIME structure to
        /// receive the current system date and time.</param>
        [DllImport("kernel32.dll")]
        public extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

        /// <summary>
        /// This function sets the current system date
        /// and time expressed in Coordinated Universal Time (UTC).
        /// </summary>
        /// <param name="lpSystemTime">[in] Pointer to a SYSTEMTIME structure that
        /// contains the current system date and time.</param>
        [DllImport("kernel32.dll")]
        public extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);
        public bool changedatetimesystem(TransactionInfo tran)
        {
            //minh test
            //Utility.ProcessLog.LogInformation("Transfer successfull " + esTran.Data[Common.KEYNAME.SUBACCTNO].ToString() + " - " + esTran.Data[Common.KEYNAME.CENACCTNO].ToString() + " : " + Amount.ToString() + " EC: " + errorcode + " ED: " + errordesc);
            //Utility.ProcessLog.LogInformation("runschedule");
            //Utility.ProcessLog.LogInformation(DateTime.Now.ToString());
            SYSTEMTIME st = new SYSTEMTIME();
            //GetSystemTime(ref st);
            //Utility.ProcessLog.LogInformation("Adding 1 hour...");
            //st.wHour = (ushort)(st.wHour + 10-st.wHour);
            int iYear, iMonth, iDay, iHour, iMinute, iSecond;
            iYear = int.Parse(tran.Data["WYEAR"].ToString());
            iMonth = int.Parse(tran.Data["WMONTH"].ToString());
            iDay = int.Parse(tran.Data["WDAY"].ToString());
            iHour = int.Parse(tran.Data["WHOUR"].ToString());
            iMinute = int.Parse(tran.Data["WMINUTE"].ToString());
            iSecond = int.Parse(tran.Data["WSECOND"].ToString());
            st.wYear = (ushort)iYear;
            st.wMonth = (ushort)iMonth;
            st.wDay = (ushort)iDay;
            st.wHour = iHour - 7 >= 0 ? (ushort)(iHour - 7) : (ushort)(iHour + 17);
            st.wMinute = (ushort)iMinute;
            st.wSecond = (ushort)iSecond;




            //string sdate = "";
            //string stime = "";
            //sdate = tran.Data["SYSTEMDATE"].ToString();
            //stime = tran.Data["SYSTEMTIME"].ToString();
            //st.wYear = tran.Data["SYSTEMTIME"].ToString();
            //st.us

            //Utility.ProcessLog.LogInformation("change systemdatetime"+sdate+"-"+stime);
            if (SetSystemTime(ref st) == 0)
            {
                Utility.ProcessLog.LogInformation("FAILURE: SetSystemTime failed");
                Utility.ProcessLog.LogInformation(DateTime.Now.ToString());
                return false;
            }
            else
            {
                Utility.ProcessLog.LogInformation("change system date sucessfull");
                Utility.ProcessLog.LogInformation(DateTime.Now.ToString());
                Utility.ProcessLog.LogInformation("Wait 3s ......");
                System.Threading.Thread.Sleep(3000);
                return true;

            }


            //
        }
        #endregion
        //dungvt 10072019: Release all cash code expired
        public bool ReleaseCashcode(TransactionInfo tran, string trancode)
        {
            Connection dbObj = new Connection();
            try
            {
                DataTable lstCashcodeExpired = dbObj.FillDataTable(Common.ConStr, "EBA_GETCASHCODEEXPIRED");
                if (lstCashcodeExpired != null && lstCashcodeExpired.Rows.Count > 0)
                {
                    foreach (DataRow rowCashcodeExp in lstCashcodeExpired.Rows)
                    {
                        try
                        {
                            Hashtable InputData = new Hashtable();

                            InputData.Add(Common.KEYNAME.IPCTRANCODE, trancode);
                            InputData.Add(Common.KEYNAME.SOURCEID, "SEMS");
                            InputData.Add(Common.KEYNAME.USERID, "SYSTEM");
                            InputData.Add("ACCTNO", rowCashcodeExp["AccountNo"]);
                            InputData.Add("AMOUNT", rowCashcodeExp["Amount"]);
                            InputData.Add("TRANREF", rowCashcodeExp["IPCTransID"]);

                            AutoTrans aut = new AutoTrans();
                            Hashtable hsOut = aut.ProcessTransHAS(InputData);
                        }
                        catch (Exception ex)
                        {
                            Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                dbObj = null;
            }
            return true;
        }
        public bool UpdateSysvar(TransactionInfo tran, string ParmList)
        {
            string[] parm = ParmList.Split('|');
            Connection con = new Connection();
            try
            {
                string value = parm[1];
                if (tran.Data.ContainsKey(parm[1]))
                {
                    value = tran.Data[parm[1]].ToString();
                }
                con.ExecuteNonquery(Common.ConStr, "IPCSYSVAR_UPDATE", new object[] { parm[0], value });
                Common.SYSVAR[parm[0]] = value;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        public bool ExportLogMessage(TransactionInfo tran)
        {
            try
            {
                int limitDayLog = 30;
                try
                {
                    limitDayLog = int.Parse(ConfigurationManager.AppSettings.Get(Common.LIMITDAYLOGMSG));
                }
                catch { }

                string idtrans = DateTime.Now.ToString("yyyyMMddHHmmssfff" + new Random().Next(1, 10));

                Thread thJobs = new Thread((object transid) =>
                {
                    Connection dbObj = new Connection();

                    int lastID_Exported = -1;

                    try
                    {
                        int countExport = 0;
                        int totalDelete = 0;
                        while (true)
                        {
                            DataTable dtLogMsg = dbObj.FillDataTable(Common.ConStr, "SYS_GETLOGMESSAGE", idtrans);

                            if (dtLogMsg != null && dtLogMsg.Rows.Count > 0)
                            {
                                string info = string.Empty;
                                foreach (DataRow row in dtLogMsg.Rows)
                                {
                                    try
                                    {
                                        info = string.Empty;
                                        info = $"{Environment.NewLine}{row["ID"]}{Environment.NewLine}{row["IPCTRANSDATE"]}{Environment.NewLine}{row["IPCWORKDATE"]}{Environment.NewLine}{row["IPCTRANSID"]}{Environment.NewLine}{row["IPCTRANCODE"]}{Environment.NewLine}{row["SOURCEID"]}{Environment.NewLine}{row["DESTID"]}{Environment.NewLine}{row["USERID"]}{Environment.NewLine}{row["IPCLOGTYPE"]}{Environment.NewLine}{row["MESSAGETYPE"]}{Environment.NewLine}{row["MESSAGE"]}";
                                        ProcessLog.LogInformation(info, Common.FILELOGTYPE.LOGMSGSYSTEM);

                                        lastID_Exported = int.Parse(row["ID"].ToString());

                                        countExport++;
                                    }
                                    catch
                                    {
                                        Utility.ProcessLog.LogError(new Exception(row["IPCTRANSID"] + "|" + row["IPCLOGTYPE"] + " Error"), System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                                    }
                                }

                                dbObj.ExecuteNonquery(Common.ConStr, "IPC_UPDATE_EXPORT_LOGMESSAGE", lastID_Exported, lastID_Exported, limitDayLog);
                            }
                            else
                            {
                                break;
                            }
                        }

                        ProcessLog.LogInformation($"IPCLOGMESSAGE EXPORTED - {countExport}", Common.FILELOGTYPE.LOGFILEPATH);

                        while (true)
                        {
                            DataTable dtLogMsg = dbObj.FillDataTable(Common.ConStr, "IPC_GETID_DELETE_LOGMESSAGE", idtrans);
                            if (dtLogMsg != null && dtLogMsg.Rows.Count > 0 && int.Parse(dtLogMsg.Rows[0][0].ToString()) > 0)
                            {
                                dbObj.ExecuteNonquery(Common.ConStr, "SYS_DELETELOGMESSAGE");
                                int countDelete = int.Parse(dtLogMsg.Rows[0][0].ToString());
                                if (totalDelete <= countDelete)
                                {
                                    totalDelete = countDelete;
                                }
                                else
                                {
                                    totalDelete = totalDelete + countDelete;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }

                        ProcessLog.LogInformation($"IPCLOGMESSAGE DELETED - {totalDelete}", Common.FILELOGTYPE.LOGFILEPATH);

                        dbObj.ExecuteNonquery(Common.ConStr, "RESET_TRANSID_JOB_EXPORT_LOGMESSAGE", idtrans);
                    }
                    catch (Exception ex)
                    {
                        dbObj.ExecuteNonquery(Common.ConStr, "IPC_UPDATE_EXPORT_LOGMESSAGE", lastID_Exported, lastID_Exported, limitDayLog);
                        dbObj.ExecuteNonquery(Common.ConStr, "RESET_TRANSID_JOB_EXPORT_LOGMESSAGE", idtrans);
                        Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                    }
                    finally
                    {
                        dbObj = null;
                    }
                });

                thJobs.Start(idtrans);
                Common.lsRunningThreads.Add("EXPORTLOGMESSAGE_" + idtrans, thJobs);
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool ReadQROriginal(TransactionInfo tran, string qr)
        {
            Connection dbObj = new Connection();
            try
            {
                string QR = tran.Data[qr].ToString().Trim();
                Dictionary<string, object> dicQR = new Dictionary<string, object>();

                if (!QR.StartsWith("00") || !QR.Contains("63"))
                {
                    throw new AggregateException("Invalid QR");
                }
                ParseOneByOne(dicQR, QR);
                foreach (var item in dicQR)
                {
                    HashTableAddOrSetFromDic(tran.Data, dicQR, string.Empty);
                }

                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                dbObj = null;
            }
        }
        private void HashTableAddOrSetFromDic(Hashtable tran, Dictionary<string, object> dic, string preKey)
        {
            foreach (var item in dic)
            {
                if (item.Value is Dictionary<string, object>)
                {
                    HashTableAddOrSetFromDic(tran, (Dictionary<string, object>)item.Value, item.Key);
                }
                Utility.Common.HashTableAddOrSet(tran, preKey + item.Key, item.Value);
            }
        }
        public bool ReadQR(TransactionInfo tran)
        {
            Connection dbObj = new Connection();
            try
            {
                string QR = tran.Data["QR"].ToString().Trim();
                Dictionary<string, object> dicQR = new Dictionary<string, object>();

                if (!QR.StartsWith("00") || !QR.Contains("63"))
                {
                    throw new AggregateException("Invalid QR");
                }
                ParseOneByOne(dicQR, QR);
                if (dicQR.ContainsKey("62"))
                {
                    if (((Dictionary<string, object>)dicQR["62"]).ContainsKey("80"))
                    {
                        Utility.Common.HashTableAddOrSet(tran.Data, Common.KEYNAME.TRANREF, ((Dictionary<string, object>)dicQR["62"])["80"].ToString());
                    }                  
                    else
                    {
                        Utility.Common.HashTableAddOrSet(tran.Data, Common.KEYNAME.TRANREF, ((Dictionary<string, object>)dicQR["62"])["05"].ToString());
                    }
                }
                DataTable dtQr = dbObj.FillDataTable(Common.ConStr, "MB_QRSELECT", tran.Data[Common.KEYNAME.TRANREF]);
                if (dtQr.Rows.Count > 0 && QR.Equals(dtQr.Rows[0]["QR"].ToString()))
                {
                    return true;
                }
                else
                {
                    if (!dicQR.ContainsKey("38"))
                    {
                        tran.ErrorCode = "4003";
                        tran.ErrorDesc = "This QR code is invalid";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                dbObj = null;
            }
        }
        public bool AddQRInfor(TransactionInfo tran)
        {
            Connection dbObj = new Connection();
            try
            {
                string originalQR = tran.Data["ORIGINALQR"].ToString().Trim();
                DataTable dtQr = dbObj.FillDataTable(Common.ConStr, "MB_QRCHECKEXISTS", originalQR);
                if (dtQr.Rows.Count > 0)
                {
                    Utility.Common.HashTableAddOrSet(tran.Data, "QR", dtQr.Rows[0]["QR"].ToString());
                    return true;
                }
                Dictionary<string, object> dicQR = new Dictionary<string, object>();

                if (!originalQR.StartsWith("00") || !originalQR.Contains("63"))
                {
                    throw new AggregateException("Invalid QR");
                }

                ParseOneByOne(dicQR, originalQR);
                if (dicQR.ContainsKey("09"))
                {
                    Utility.Common.HashTableAddOrSet(tran.Data, Common.KEYNAME.AMOUNT, dicQR["09"].ToString());
                }
                if (!dicQR.ContainsKey("62"))
                {
                    dicQR["62"] = new Dictionary<string, object>();
                }
                ((Dictionary<string, object>)dicQR["62"]).Add("05", tran.IPCTransID);
                Dictionary<string, object> dicQRResult = dicQR.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

                string qr = string.Empty;
                GenQRCode(dicQRResult, ref qr);
                Utility.Common.HashTableAddOrSet(tran.Data, "QR", qr);
                dbObj.ExecuteNonquery(Common.ConStr, "MB_QRINSERT", new object[] { tran.IPCTransID, tran.Data["ORIGINALQR"], tran.Data["QR"], tran.Data[Common.KEYNAME.USERID], tran.Data[Common.KEYNAME.STATUS] });
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                dbObj = null;
            }
        }
        private void ParseOneByOne(Dictionary<string, object> dicQR, string input, bool keydefault = false)
        {
           
                string prefix = GetString(0, 2, ref input);
                int len = int.Parse(GetString(0, 2, ref input));
                string data = GetString(0, len, ref input);
                if (keydefault)
                {
                    dicQR.Add(prefix, data);
                    if (!string.IsNullOrEmpty(input))
                        ParseOneByOne(dicQR, input, true);
                    return;
                }
                switch (prefix)
                {
                    case "38":
                    case "50":
                    case "51":
                    case "62":
                        Dictionary<string, object> dicQRChild = new Dictionary<string, object>();
                        ParseOneByOne(dicQRChild, data, true);
                        dicQR.Add(prefix, dicQRChild);
                        ParseOneByOne(dicQR, input);
                        break;
                    case "63"://end
                        dicQR.Add(prefix, data);
                        break;
                    default: //data
                        dicQR.Add(prefix, data);
                        ParseOneByOne(dicQR, input);
                        break;
                }
                
            
            
        }

        #region LAPNETQR

        public bool ReadQRLAPNETOriginal(TransactionInfo tran, string qr)
        {
            Connection dbObj = new Connection();
            try
            {
                string QR = tran.Data[qr].ToString().Trim();
                Dictionary<string, object> dicQR = new Dictionary<string, object>();

                if (!QR.StartsWith("00"))
                {
                    throw new AggregateException("Invalid QR");
                }
                ParseOneByOneQRLAPNET(dicQR, QR);
                foreach (var item in dicQR)
                {
                    HashTableAddOrSetFromDic(tran.Data, dicQR, string.Empty);
                }

                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                dbObj = null;
            }
        }              
        public bool AddQRLAPNETInfor(TransactionInfo tran)
        {
            Connection dbObj = new Connection();
            try
            {
                string originalQR = tran.Data["ORIGINALQR"].ToString().Trim();
                DataTable dtQr = dbObj.FillDataTable(Common.ConStr, "MB_QRCHECKEXISTS", originalQR);
                if (dtQr.Rows.Count > 0)
                {
                    Utility.Common.HashTableAddOrSet(tran.Data, "QR", dtQr.Rows[0]["QR"].ToString());
                    return true;
                }
                Dictionary<string, object> dicQR = new Dictionary<string, object>();

                if (!originalQR.StartsWith("00"))
                {
                    throw new AggregateException("Invalid QR");
                }
                ParseOneByOneQRLAPNET(dicQR, originalQR);
               
                if (!dicQR.ContainsKey("62"))
                {
                    dicQR["62"] = new Dictionary<string, object>();
                }
                ((Dictionary<string, object>)dicQR["62"]).Add("05", tran.IPCTransID);
                Dictionary<string, object> dicQR62 = ((Dictionary<string, object>)dicQR["62"]).OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                if(dicQR.ContainsKey("62") && dicQR62.ContainsKey("05"))
                {
                    dicQR.Remove("62");
                    dicQR.Add("62", dicQR62);
                }                   
                Dictionary<string, object> dicQRResult = dicQR.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

                string qr = string.Empty;
                GenQRCode(dicQRResult, ref qr);
                if (!dicQRResult.ContainsKey("63"))
                {
                    qr += "6304";
                    string CRC = Utility.Common.GenCRC16(qr);
                    qr += CRC;
                }
                Utility.Common.HashTableAddOrSet(tran.Data, "QR", qr);
                dbObj.ExecuteNonquery(Common.ConStr, "MB_QRINSERT", new object[] { tran.IPCTransID, tran.Data["ORIGINALQR"], tran.Data["QR"], tran.Data[Common.KEYNAME.USERID], tran.Data[Common.KEYNAME.STATUS] });
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                dbObj = null;
            }
        }
        private void ParseOneByOneQRLAPNET(Dictionary<string, object> dicQR, string input, bool keydefault = false)
        {
            string prefix = GetString(0, 2, ref input);
            int len = int.Parse(GetString(0, 2, ref input));
            string data = GetString(0, len, ref input);
            if (keydefault)
            {
                dicQR.Add(prefix, data);
                if (!string.IsNullOrEmpty(input))
                    ParseOneByOneQRLAPNET(dicQR, input, true);
                return;
            }
            switch (prefix)
            {
                case "38":
                    Dictionary<string, object> dicQRChild38 = new Dictionary<string, object>();
                    ParseOneByOneQRLAPNET(dicQRChild38, data, true);
                    dicQR.Add(prefix, dicQRChild38);
                    ParseOneByOneQRLAPNET(dicQR, input);
                    break;
                case "50":
                case "51":
                case "62": //end
                    Dictionary<string, object> dicQRChild62 = new Dictionary<string, object>();
                    ParseOneByOneQRLAPNET(dicQRChild62, data, true);
                    dicQR.Add(prefix, dicQRChild62);
                    //ParseOneByOneQRLAPNET(dicQR, input);
                    break;
                default: //data
                    dicQR.Add(prefix, data);
                    ParseOneByOneQRLAPNET(dicQR, input);
                    break;
            }
        }

        #endregion
        private string GetString(int start, int len, ref string input)
        {
         
                string data = input.Substring(start, len);
                input = input.Substring(len, input.Length - len);
                return data;              
        }

        private void GenQRCode(Dictionary<string, object> dicQR, ref string sourcevalue)
        {
            foreach (var item in dicQR)
            {
                string _tag = item.Key;
                string _data = string.Empty;
                if (item.Value is Dictionary<string, object>)
                {
                    GenQRCode((Dictionary<string, object>)item.Value, ref _data);
                }
                else
                {
                    _data = item.Value.ToString().Trim();
                }

                string _len = _data.Length >= 10 ? _data.Length.ToString() : $"0{_data.Length}";
                string _tagdata = $"{_tag}{_len}{_data}";
                sourcevalue += _tagdata;
            }

        }

        public bool LogSynTranHis(TransactionInfo tran)
        {

            Connection dbObj = new Connection();
            try
            {
                if (!tran.Data.ContainsKey("LASTID"))
                {
                    tran.Data["LASTID"] = string.Empty;
                }
                if (tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("WL_TRANHIS") || tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("AM_TRANHIS") || tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("AM_LOADTRANHIS")
                    || tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("AM_EARNING") || tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("WL_LOADTRANHIS"))
                {
                    if (tran.Data.ContainsKey("SELECTRESULT"))
                    {
                        DataSet dsResult = (DataSet)tran.Data[Common.KEYNAME.SELECTRESULT];
                        if (dsResult.Tables.Count > 0)
                        {
                            DataTable dtResult = dsResult.Tables[0];
                            string lastid = dtResult.Rows.Count >= 1 ? dtResult.Rows[dtResult.Rows.Count - 1][0].ToString() : string.Empty;
                            if (!string.IsNullOrEmpty(lastid))
                            {
                                DataTable dtLogSyn = dbObj.FillDataTable(Common.ConStr, "LOGSYNTRANSHIS", tran.Data[Common.KEYNAME.IPCTRANSID], tran.Data["LASTID"], tran.Data["ACCTNO"], string.Empty, string.Empty, lastid, string.Empty, string.Empty, string.Empty);
                                if (dtLogSyn != null && dtLogSyn.Rows.Count > 0)
                                {
                                    if (tran.Data.ContainsKey("LASTID"))
                                    {
                                        tran.Data["LASTID"] = dtLogSyn.Rows[0]["LASTID"].ToString();
                                    }
                                    else
                                    {
                                        tran.Data.Add("LASTID", dtLogSyn.Rows[0]["LASTID"].ToString());
                                    }
                                }
                            }

                        }
                    }
                    return true;
                }
                else
                {
                    if (!tran.Data.ContainsKey(Common.KEYNAME.DATARESULT) || !tran.Data.ContainsKey("FROMDATE") || !tran.Data.ContainsKey("TODATE")) return true;
                    DataSet dsResult = (DataSet)tran.Data[Common.KEYNAME.DATARESULT];
                    if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtRs = dsResult.Tables[0];
                        DataTable dtFinalRs = new DataTable();
                        dtFinalRs = dtRs.Clone();
                        foreach (DataRow row in dtRs.Rows)
                        {
                            string[] formats = { "dd/MM/yyyy" };
                            CultureInfo enUS = new CultureInfo("en-US");
                            DateTime dt = new DateTime();
                            DateTime dtFrom = new DateTime();
                            DateTime dtTo = new DateTime();
                            DateTime.TryParseExact(row["TRANDATE"].ToString(), "dd/MM/yyyy", enUS, DateTimeStyles.None, out dt);
                            DateTime.TryParseExact(tran.Data["FROMDATE"].ToString(), "dd/MM/yyyy", enUS, DateTimeStyles.None, out dtFrom);
                            DateTime.TryParseExact(tran.Data["TODATE"].ToString(), "dd/MM/yyyy", enUS, DateTimeStyles.None, out dtTo);

                            if (dt >= dtFrom && dt <= dtTo)
                            {
                                dtFinalRs.ImportRow(row);
                            }
                        }
                        string amountvalue = dtFinalRs.Rows.Count > 0 ? (dtFinalRs.Rows[dtFinalRs.Rows.Count - 1]["REQ_AMOUNTVALUE"]).ToString() : string.Empty;
                        string currencycode = dtFinalRs.Rows.Count > 0 ? (dtFinalRs.Rows[dtFinalRs.Rows.Count - 1]["REQ_CURRENCYCODE"]).ToString() : string.Empty;
                        string lasttxnid = dtFinalRs.Rows.Count > 0 ? (dtFinalRs.Rows[dtFinalRs.Rows.Count - 1]["REQ_LASTTXNID"]).ToString() : string.Empty;
                        string lasttxndate = dtFinalRs.Rows.Count > 0 ? (dtFinalRs.Rows[dtFinalRs.Rows.Count - 1]["REQ_LASTTXNDATE"]).ToString() : string.Empty;
                        string lastsrlno = dtFinalRs.Rows.Count > 0 ? (dtFinalRs.Rows[dtFinalRs.Rows.Count - 1]["REQ_LASTSRLNO"]).ToString() : string.Empty;
                        string lastpstddate = dtFinalRs.Rows.Count > 0 ? (dtFinalRs.Rows[dtFinalRs.Rows.Count - 1]["REQ_LASTPSTDDATE"]).ToString() : string.Empty;
                        if (!string.IsNullOrEmpty(lasttxnid))
                        {
                            DataTable dtLogSyn = dbObj.FillDataTable(Common.ConStr, "LOGSYNTRANSHIS", tran.Data[Common.KEYNAME.IPCTRANSID], tran.Data["LASTID"], tran.Data["ACCTNO"], amountvalue, currencycode, lasttxnid, lasttxndate, lastsrlno, lastpstddate);
                            if (dtLogSyn != null && dtLogSyn.Rows.Count > 0)
                            {
                                if (tran.Data.ContainsKey("LASTID"))
                                {
                                    tran.Data["LASTID"] = dtLogSyn.Rows[0]["LASTID"].ToString();
                                }
                                else
                                {
                                    tran.Data.Add("LASTID", dtLogSyn.Rows[0]["LASTID"].ToString());
                                }
                            }
                        }

                        dtFinalRs.Columns.Remove("REQ_AMOUNTVALUE");
                        dtFinalRs.Columns.Remove("REQ_CURRENCYCODE");
                        dtFinalRs.Columns.Remove("REQ_LASTTXNID");
                        dtFinalRs.Columns.Remove("REQ_LASTTXNDATE");
                        dtFinalRs.Columns.Remove("REQ_LASTSRLNO");
                        dtFinalRs.Columns.Remove("REQ_LASTPSTDDATE");
                        dsResult.Tables.RemoveAt(0);
                        dsResult.Tables.Add(dtFinalRs);
                        tran.Data[Common.KEYNAME.DATARESULT] = dsResult;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                dbObj = null;
            }
        }



        public bool PrepareDataForInterbanktranfer(TransactionInfo tran)
        {
            try
            {
                Connection dbObj = new Connection();

                //CBMFEE RECEIVERFEE SENDERFEE TOTALFEE
                JArray jarr = new JArray();

                string chargebearercode = tran.Data["CHARGEBEARERCODE"].ToString().Trim();
                var reciverfee = tran.Data["RECEIVERFEE"];
                var senderfee = tran.Data["SENDERFEE"];

                switch (chargebearercode)
                {
                    case "DEBT":
                        jarr.Add(0);
                        jarr.Add(reciverfee);
                        jarr.Add(senderfee);
                        jarr.Add(0);
                        break;
                    default:
                        jarr.Add(0);
                        jarr.Add(0);
                        jarr.Add(senderfee);
                        jarr.Add(0);
                        break;
                }

                Utility.Common.HashTableAddOrSet(tran.Data, "LISTCHARGEAMOUNT", jarr);
                DataTable dtQr = dbObj.FillDataTable(Common.ConStr, "GETTRANSIDINTER");
                if (dtQr.Rows.Count > 0)
                {
                    Utility.Common.HashTableAddOrSet(tran.Data, "TRANSACTIONID", dtQr.Rows[0]["TRANSACTIONID"].ToString());
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }



        public bool RevertTransactionInterbank(TransactionInfo tran)
        {
            Connection dbObj = new Connection();
            try
            {
                if (tran.Data.ContainsKey("RES_STATUS") && tran.Data["RES_STATUS"].ToString().Equals("SUCCESS"))
                {
                    if (tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("WL_TRANSFEROTHBANK") || tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("AM_TRANSFEROTHBANK"))
                    {

                        string Txref = tran.Data[Common.KEYNAME.IPCTRANSID].ToString();
                        string msglog = $"DoStore INPUT transaction {tran.IPCTransID} Store name = WAL_Inter_REVERSAL param list = Txref paramvalue:" + Environment.NewLine;
                        msglog += $"{Txref}";
                        ProcessLog.LogInformation(msglog, Common.FILELOGTYPE.LOGDOSTOREINFO);
                        dbObj.ExecuteNonquery(Common.ConStr, "WAL_Inter_REVERSAL", Txref);
                        //revert wal
                    }
                    dbObj.ExecuteNonquery(Common.ConStr, "UPDATE_UPSTREAMTRANSACTION", tran.Data["IPCTRANSID"].ToString().Trim(), Common.TRANSTATUS.ERROR);
                    tran.Status = Common.TRANSTATUS.ERROR;
                }
                else
                {
                    ProcessLog.LogInformation($"transaction {tran.IPCTransID} reversal failed !!! ", Common.FILELOGTYPE.LOGFILEPATH);
                    dbObj.ExecuteNonquery(Common.ConStr, "UPDATE_UPSTREAMTRANSACTION", tran.Data["IPCTRANSID"].ToString().Trim(), Common.TRANSTATUS.PENDDING);
                    tran.Status = Common.TRANSTATUS.PENDDING;
                }
            }
            catch (Exception ex)
            {
                ProcessLog.LogInformation($"DoStore OUTPUT transaction {tran.IPCTransID} | Store name WAL_Inter_REVERSAL Return: {Environment.NewLine}{ex.Message}", Common.FILELOGTYPE.LOGDOSTOREINFO);
            }
            return true;
        }

        public bool RouterTransaction(TransactionInfo tran)
        {
            try
            {
                if (tran.Data["TARGETTRANCODE"] == null)
                {
                    Utility.ProcessLog.LogInformation($"{tran.IPCTransID} {System.Reflection.MethodBase.GetCurrentMethod().Name} this transaction not found!", Utility.Common.FILELOGTYPE.LOGFILEPATH);
                    return false;
                }

                Hashtable InputData = new Hashtable();
                InputData.Add(Utility.Common.KEYNAME.IPCTRANCODE, tran.Data["TARGETTRANCODE"]);
                InputData.Add(Utility.Common.KEYNAME.SOURCEID, tran.Data["TARGETSOURCEID"]);

                foreach (string key in tran.Data.Keys)
                {
                    if (!InputData.ContainsKey(key) && !key.Equals(Common.KEYNAME.IPCTRANSID))
                    {
                        InputData.Add(key, tran.Data[key]);
                    }
                }

                AutoTrans aut = new AutoTrans();
                Hashtable hsOut = aut.ProcessTransHAS(InputData);

                if (hsOut[Common.KEYNAME.IPCERRORCODE].ToString().Equals(Common.ERRORCODE.OK))
                {
                    foreach (string key in hsOut.Keys)
                    {
                        if (!key.Equals(Common.KEYNAME.IPCERRORCODE) && !key.Equals(Common.KEYNAME.IPCERRORDESC))
                        {
                            if (tran.Data.ContainsKey(key)) tran.Data[key] = hsOut[key];
                            else tran.Data.Add(key, hsOut[key]);
                        }
                    }
                }
                else
                {
                    throw new Exception(hsOut[Utility.Common.KEYNAME.IPCERRORCODE].ToString());
                }
            }
            catch (Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                tran.SetErrorInfo(ex);
                return false;
            }
            return true;
        }

        public bool ValidateBatchResult(TransactionInfo tran)
        {
            try
            {
                Connection con = new Connection();
                DataTable lstTrans = new DataTable();
                AutoTrans execTran = new AutoTrans();
                string base64result = string.Empty;

                if (!tran.ErrorCode.Equals(Common.ERRORCODE.OK))
                {
                    Utility.ProcessLog.LogInformation("Download batch result error, transid is " + tran.IPCTransID);
                    return false;
                }

                base64result = tran.Data["DATACONTENTBASE64"].ToString();
                if (string.IsNullOrEmpty(base64result))
                {
                    Utility.ProcessLog.LogInformation("Batch result string empty, transid is " + tran.IPCTransID);
                    return false;
                }

                DataTable tblResult = Common.Base64Excel2Datatable(base64result);
                if (tblResult.Rows.Count == 0)
                {
                    Utility.ProcessLog.LogInformation("Batch result datatable empty, transid is " + tran.IPCTransID);
                    return false;
                }

                if (tran.Data.Contains(Common.KEYNAME.DATATABLE))
                {
                    tran.Data[Common.KEYNAME.DATATABLE] = tblResult;
                }
                else
                {
                    tran.Data.Add(Common.KEYNAME.DATATABLE, tblResult);
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool ConvertMsgKeyValueToJson(TransactionInfo tran)
        {
            try
            {
                if (tran.InputData != null)
                {
                    JObject jo = new JObject();
                    JObject ObjMPU = new JObject();
                    Hashtable InputData = new Hashtable();
                    if (jo.SelectToken("payload") == null)
                    {
                        string obj = Uri.UnescapeDataString(tran.InputData);
                        string[] objlst = obj.Split('&');
                        if (objlst.Length > 0)
                        {
                            for (int i = 0; i < objlst.Length; i++)
                            {
                                string[] objlstchil = objlst[i].Split('=');
                                if (objlstchil.Length > 1)
                                {
                                    ObjMPU.Add("[" + objlstchil[0].ToString().Trim() + "]", objlstchil[1].ToString().Trim());
                                }
                            }
                            ObjMPU.Add("channelCode", "MP");
                        }

                        DataRow[] mapping = Common.DBIINPUTDEFINEHTTP.Select("IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() +
                        "' OR IPCTRANCODE = '' AND IPCDESTTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString() + "'");
                        if (mapping.Length > 0)
                        {
                            foreach (DataRow row in mapping)
                            {
                                try
                                {
                                    object value = null;

                                    if (string.IsNullOrEmpty(row["TAGNAME"].ToString()))
                                        value = ObjMPU;
                                    else
                                    {
                                        string tagName = row["TAGNAME"].ToString();
                                        value = ObjMPU[tagName].ToString();
                                    }

                                    Formatter.FormatFieldValue(ref value, row["FORMATTYPE"].ToString(), row["FORMATOBJECT"].ToString(), row["FORMATFUNCTION"].ToString(), row["FORMATPARM"].ToString());

                                    Common.HashTableAddOrSet(tran.Data, row["KEYNAME"].ToString(), value);
                                }
                                catch
                                {
                                    Utility.ProcessLog.LogInformation(tran.IPCTransID.ToString() + "ConvertMsgKeyValueToJson - Can't when get json value : " + row["TAGNAME"].ToString());
                                    //to support FBE
                                    if (row["DEFAULTVALUE"] != null)
                                        Common.HashTableAddOrSet(tran.Data, row["KEYNAME"].ToString(), row["DEFAULTVALUE"].ToString());
                                }
                            }
                            tran.InputData = ObjMPU.ToString();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                tran.SetErrorInfo(ex);

            }
            return false;
        }
        public bool CalculateDiscount(TransactionInfo tran)
        {
            return ExcutePromotion(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), "D", true);
        }
        public bool CalculateDiscount(TransactionInfo tran, string ParmList)
        {
            string[] parm = ParmList.Split('|');
            string tranCode = string.Empty;
            if (parm.Length > 0)
            {
                tranCode = !string.IsNullOrEmpty(parm[0]) ? parm[0] : tran.Data[Common.KEYNAME.IPCTRANCODE].ToString();
            }
            bool getCollection = false;
            return ExcutePromotion(tran, tranCode, "D", getCollection);
        }
        public bool CalculateCashBack(TransactionInfo tran, string ParmList)
        {
            string[] parm = ParmList.Split('|');
            string tranCode = string.Empty;
            if (parm.Length > 0)
            {
                tranCode = !string.IsNullOrEmpty(parm[0]) ? parm[0] : tran.Data[Common.KEYNAME.IPCTRANCODE].ToString();
            }
            return ExcutePromotion(tran, tranCode, "C", false);
        }
        public bool AutoCashBack(TransactionInfo tran)
        {
            bool parallel = bool.Parse(ConfigurationManager.AppSettings["ParallelAutoCashback"]);
            if (parallel)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    ExcutePromotion(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), "C", true);
                }).Start();
                return true;
            }
            else
            {
                return ExcutePromotion(tran, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), "C", true);
            }
        }
        private bool ExcutePromotion(TransactionInfo tran, string tranCode, string type, bool getCollection)
        {
            Connection con = new Connection();
            try
            {
                //parm1: Transaction code
                //parm2: Prmotion type : C - auto call cashback tran, D: discount
                //parm3: return collection or not 

                tranCode = (string.IsNullOrEmpty(tranCode) ? tran.Data.ContainsKey(Common.KEYNAME.TRANCODE) ? tran.Data[Common.KEYNAME.TRANCODE] : tran.Data["TRANSACTION"] : tran.Data.ContainsKey(tranCode) ? tran.Data[tranCode] : tranCode).ToString();
                string Acctno = tran.Data.ContainsKey(Common.KEYNAME.ACCTNO) ? tran.Data[Common.KEYNAME.ACCTNO].ToString() : string.Empty;
                string receiverAcctno = tran.Data.ContainsKey(Common.KEYNAME.RECEIVERACCOUNT) ? tran.Data[Common.KEYNAME.RECEIVERACCOUNT].ToString() : string.Empty;
                string amount = tran.Data.ContainsKey(Common.KEYNAME.AMOUNT) ? tran.Data[Common.KEYNAME.AMOUNT].ToString() : "0";

                ProcessLog.LogInformation($"Excute Store Get_PromotionTransaction '{tranCode}', '{type}', '{tran.Data[Common.KEYNAME.USERID]}', '{tran.Data[Common.KEYNAME.ACCTNO]}', '{tran.Data[Common.KEYNAME.RECEIVERACCOUNT]}', '{tran.Data[Common.KEYNAME.CCYID]}', '{tran.Data[Common.KEYNAME.AMOUNT]}'", Common.FILELOGTYPE.LOGDOSTOREINFO);

                DataSet ds = con.FillDataSet(Common.ConStr, "Get_PromotionTransaction", tranCode, type, tran.Data[Common.KEYNAME.USERID],
                    Acctno, receiverAcctno, tran.Data[Common.KEYNAME.CCYID], amount);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtPromotion = ds.Tables[0];
                    DataTable dtCollection = ds.Tables[1];
                    for (int i = dtPromotion.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dtPromotion.Rows[i];
                        if (!checkFormula(tran, dr["Formula"].ToString()))
                            dr.Delete();
                    }
                    dtPromotion.AcceptChanges();

                    if (dtPromotion.Rows.Count > 0)
                    {
                        if (type.Equals("C"))
                        {
                            DataRow[] dtSender = dtPromotion.Select("BeneficiarySide='S'");
                            if (dtSender.Length > 0)
                            {
                                CalculatorPromotion(tran, dtPromotion, dtCollection, "S", decimal.Parse(amount), getCollection);
                                CashBackPromotion(tran);
                            }
                            DataRow[] dtReceiver = dtPromotion.Select("BeneficiarySide='R'");
                            if (dtReceiver.Length > 0)
                            {
                                CalculatorPromotion(tran, dtPromotion, dtCollection, "R", decimal.Parse(amount), getCollection);
                                CashBackPromotion(tran);
                            }
                        }
                        else
                        {
                            CalculatorPromotion(tran, dtPromotion, dtCollection, "S", decimal.Parse(amount), getCollection);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                tran.SetErrorInfo(ex);
            }
            return true;
        }

        private void CalculatorPromotion(TransactionInfo tran, DataTable dtPromotion, DataTable dtCollection, string beneficiarySide, decimal amount, bool getCollection)
        {
            try
            {
                decimal PromotionAmt = 0;
                string PromotionId = string.Empty;
                string PromotionType = string.Empty; /*D: Discount;C:CashBack*/
                decimal PromotionHO = 0;
                decimal PromotionReceiver = 0;

                if (dtPromotion.Rows.Count > 0)
                {
                    foreach (DataRow rowPromotion in dtPromotion.Rows)
                    {
                        if (!rowPromotion["BeneficiarySide"].ToString().Equals(beneficiarySide))
                        {
                            continue;
                        }

                        string id = rowPromotion["PromotionId"].ToString();
                        string pType = rowPromotion["PromotionType"].ToString();
                        bool isTier = (bool)rowPromotion["IsTier"];
                        decimal amt = 0;
                        if (!isTier)
                        {
                            amt = (decimal)rowPromotion["FixAmt"];
                        }
                        else
                        {
                            if (rowPromotion["PromotionType"].ToString() == "PER")
                            {
                                amt = amount * decimal.Parse(rowPromotion["Rate"].ToString()) / 100;
                                if (amt < (decimal)rowPromotion["MinPromotion"])
                                    amt = (decimal)rowPromotion["MinPromotion"];
                                if (amt > (decimal)rowPromotion["MaxPromotion"])
                                    amt = (decimal)rowPromotion["MaxPromotion"];
                            }
                            else
                                amt = (decimal)rowPromotion["FixAmt"];
                        }

                        DataRow[] drCollection = dtCollection.Select(String.Format("PromotionId='{0}' and FromAmt={1} and ToAmt={2}", id, rowPromotion["FromAmt"],
                            rowPromotion["ToAmt"]));
                        if (drCollection.Length == 0)
                        {
                            drCollection = dtCollection.Select(String.Format("PromotionId='{0}' and FromAmt=0 and ToAmt=-1", id));
                        }
                        foreach (DataRow row in drCollection)
                        {
                            decimal amtSide = 0;
                            string shareType = row["PromotionType"].ToString();
                            switch (shareType)
                            {
                                case "PER":
                                    amtSide = amt * decimal.Parse(row["Rate"].ToString()) / 100;
                                    break;
                                case "FIX":
                                    amtSide = (decimal)row["FixAmt"];
                                    if (amtSide > amt) amtSide = amt;
                                    break;
                                default:
                                    amtSide = amt - PromotionHO - PromotionReceiver;
                                    break;
                            }

                            if (row["PaySide"].ToString() == "HO")
                                PromotionHO = amtSide;
                            else PromotionReceiver = amtSide;

                            if (shareType == "REMAINING" || PromotionHO + PromotionReceiver == amt) break;
                        }

                        if (PromotionHO + PromotionReceiver != amt)
                        {
                            break;
                        }

                        //find best solution for customer 
                        if (amt > PromotionAmt)
                        {
                            PromotionAmt = amt;
                            PromotionId = id;
                            PromotionType = pType;
                        }
                    }

                    Common.HashTableAddOrSet(tran.Data, "PromotionAmt", PromotionAmt);
                    Common.HashTableAddOrSet(tran.Data, "PromotionId", PromotionId);
                    Common.HashTableAddOrSet(tran.Data, "PromotionType", PromotionType);
                    Common.HashTableAddOrSet(tran.Data, "BeneficiarySide", beneficiarySide);
                    if (getCollection)
                    {
                        Common.HashTableAddOrSet(tran.Data, "PromotionHO", PromotionHO);
                        Common.HashTableAddOrSet(tran.Data, "PromotionReceiver", PromotionReceiver);
                    }

                    ProcessLog.LogInformation($"Excute Promotion transaction {tran.IPCTransID} | Return: PromotionAmt: {PromotionAmt}; PromotionId: {PromotionId}; PromotionType: {PromotionType}; PromotionHO: {PromotionHO};  PromotionReceiver: {PromotionReceiver}", Common.FILELOGTYPE.LOGFILEPATH);
                }
            }
            catch (Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                tran.SetErrorInfo(ex);
            }
        }

        private void CashBackPromotion(TransactionInfo tran)
        {
            try
            {
                Hashtable InputData = new Hashtable();
                InputData.Add(Common.KEYNAME.IPCTRANCODE, tran.Data["CASHBACKTRANCODE"].ToString());
                InputData.Add(Common.KEYNAME.SERVICEID, "SEMS");
                InputData.Add(Common.KEYNAME.SOURCEID, "SEMS");
                InputData.Add("TXREF", tran.IPCTransID);
                InputData.Add(Common.KEYNAME.IPCTRANDESC, "Cash back for Transaction ref " + tran.IPCTransID);
                InputData.Add(Common.KEYNAME.DESC, "Cash back for Transaction ref " + tran.IPCTransID);
                InputData.Add(Common.KEYNAME.TRANDESC, "Cash back Transaction ref " + tran.IPCTransID);

                foreach (string key in tran.Data.Keys)
                {
                    if (!InputData.ContainsKey(key) && !key.Equals(Common.KEYNAME.IPCTRANSID)
                                                    && !key.Equals(Common.KEYNAME.SOURCEID) && !key.Equals(Common.KEYNAME.SERVICEID))
                    {
                        InputData.Add(key, tran.Data[key]);
                    }
                }

                AutoTrans aut = new AutoTrans();
                Hashtable hsOut = aut.ProcessTransHAS(InputData);

                if (hsOut[Common.KEYNAME.IPCERRORCODE].ToString().Equals(Common.ERRORCODE.OK))
                {
                    foreach (string key in hsOut.Keys)
                    {
                        if (!key.Equals(Common.KEYNAME.IPCERRORCODE) && !key.Equals(Common.KEYNAME.IPCERRORDESC))
                        {
                            if (tran.Data.ContainsKey(key)) tran.Data[key] = hsOut[key];
                            else tran.Data.Add(key, hsOut[key]);
                        }
                    }
                }
                else
                {
                    throw new Exception(hsOut[Common.KEYNAME.IPCERRORCODE].ToString());
                }
            }
            catch (Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                tran.SetErrorInfo(ex);
            }
        }
        private bool checkFormula(TransactionInfo tran, string formulaId)
        {
            try
            {
                Connection con = new Connection();
                if (string.IsNullOrEmpty(formulaId))
                    return true;

                DataSet dsFormulaDetail = con.FillDataSet(Common.ConStr, "SEMS_FORMULA_SELECTDETAIL", formulaId);
                string FormulaStr = dsFormulaDetail.Tables[0].Rows[0]["Formula"].ToString();

                List<string> formulaParamList = Common.GetParams(FormulaStr, '[', ']');
                foreach (string formulaParam in formulaParamList)
                {
                    if (dsFormulaDetail.Tables[1].Rows.Count == 0 || dsFormulaDetail.Tables[1].Select($"FieldName = '{formulaParam}'").Length == 0)
                        continue;
                    DataRow drFormulaSub = dsFormulaDetail.Tables[1].Select($"FieldName = '{formulaParam}'")[0];
                    string formulaCondition = drFormulaSub["Formula"].ToString();
                    string originalFieldName = drFormulaSub["FieldNameOriginal"].ToString();


                    if (!string.IsNullOrEmpty(formulaCondition))
                    {
                        foreach (string field in Common.GetParams(formulaCondition, '[', ']'))
                        {
                            DataRow[] drInputFormat = dsFormulaDetail.Tables[2].Select($"FieldName = '{field}'");
                            string sqlSub = drInputFormat[0]["SQLClause"].ToString();
                            if (drInputFormat.Length > 0 && !string.IsNullOrEmpty(sqlSub))
                            {
                                formulaCondition = formulaCondition.Replace($"[{field}]", $"{sqlSub}");
                            }
                        }
                        //TrungTQ fix

                        formulaCondition = " and (" + formulaCondition + ")";
                       
                    }

                    DataRow drFormulaFieldName = dsFormulaDetail.Tables[2].Select($"FieldName = '{originalFieldName}'")[0];
                    string strSQLExpression = drFormulaFieldName["SQLClause"].ToString();
                    string dataType = drFormulaFieldName["DataType"].ToString();

                    //format lai ten cot formulaCondition
                    foreach (string field in Common.GetParams(strSQLExpression, '{', '}'))
                    {
                        if (field.Equals("CONDITION"))
                            continue;
                        var value = tran.Data.ContainsKey(field) ? tran.Data[field].ToString() : field;

                        strSQLExpression = strSQLExpression.Replace("{" + field + "}", $"{value.Trim()}");
                    }

                    strSQLExpression = strSQLExpression.Replace("{CONDITION}", $"{formulaCondition}");
                    strSQLExpression = CustomOperatorFormulaSub(strSQLExpression);
                    //get other field value
                    foreach (string field in Common.GetParams(strSQLExpression, '[', ']'))
                    {
                        DataRow[] drInputFormat = dsFormulaDetail.Tables[3].Select($"FieldName = '{originalFieldName}' AND ParamName='{field}'");
                        string value = field;
                        if (drInputFormat.Length > 0)
                        {
                            switch (drInputFormat[0]["VALUESTYLE"].ToString())
                            {
                                case "VALUE":
                                    value = drInputFormat[0]["ValueName"].ToString();
                                    break;
                                case "DATA":
                                default:
                                    value = tran.Data.ContainsKey(field) ? tran.Data[field].ToString() : field;
                                    break;
                            }
                        }
                        else
                        {
                            value = field;
                        }

                        strSQLExpression = strSQLExpression.Replace($"[{field}]", $"{value}");
                    }

                    ProcessLog.LogInformation($"Excute Promotion transaction {tran.IPCTransID} | Excute: strSQLExpression: {strSQLExpression}", Common.FILELOGTYPE.LOGFILEPATH);

                    DataTable rs = con.FillDataTableSQL(Common.ConStr, strSQLExpression);

                    string strRs = rs.Rows[0][0].ToString();

                    FormulaStr = FormulaStr.Replace($"[{formulaParam}]", strRs);
                }

                //format C# param expression
                FormulaStr = CustomOperatorFormula(FormulaStr);
                foreach (var fieldName in Common.GetParams(FormulaStr, '[', ']'))
                {
                    //format based on fieldname defination
                    var dtFormulaInput = con.FillDataTable(Common.ConStr, "SEMS_FORMULAFIELDNAME_SELECT", fieldName);
                    if (dtFormulaInput.Rows.Count > 0)
                    {
                        string strFieldExpression = dtFormulaInput.Rows[0]["SQLClause"].ToString();
                        foreach (string field in Common.GetParams(strFieldExpression, '{', '}'))
                        {
                            var value = tran.Data.ContainsKey(field) ? tran.Data[field].ToString() : field;

                            strFieldExpression = strFieldExpression.Replace("{" + field + "}", $"{value.Trim()}");
                        }
                        DataTable rs = con.FillDataTableSQL(Common.ConStr, strFieldExpression);
                        string strRs = rs.Rows[0][0].ToString();

                        switch (dtFormulaInput.Rows[0]["DataType"].ToString())
                        {
                            case "String":
                                strRs = $"'{strRs}'";
                                break;
                            default: break;
                        }

                        FormulaStr = FormulaStr.Replace($"[{fieldName}]", strRs);
                    }
                    //try to get from tran.Data if not exists in fieldname defination
                    else
                    {
                        if (tran.Data.ContainsKey(fieldName))
                        {
                            FormulaStr = FormulaStr.Replace($"[{fieldName}]", tran.Data[fieldName].ToString());
                        }
                    }
                }

                //Evaluate Expression
                Expression expFormula = new Expression(FormulaStr);
                if (expFormula.HasErrors())
                {
                    throw new Exception("Wrong formula syntax");
                }
                expFormula.EvaluateFunction += FormulaExtensionFunctions;
                ProcessLog.LogInformation($"Transaction Promotion FormulaStr {tran.IPCTransID} | " + FormulaStr, Common.FILELOGTYPE.LOGFILEPATH);
                var exResult = Convert.ToBoolean(expFormula.Evaluate());
                if (!exResult)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ProcessLog.LogInformation($"Excute Transaction Promotion Error {tran.IPCTransID} | " +  ex, Common.FILELOGTYPE.LOGFILEPATH);
                return false;
            }
        }

        #region custom operator
        private string CustomOperatorFormula(string formula)
        {
            //replcate bool variable
            formula = formula.Replace("True", "true").Replace("TRUE", "true").Replace("False", "false").Replace("FALSE", "false");
            formula = formula.Replace("And", "and").Replace("AND", "and").Replace("Or", "or").Replace("OR", "or");
            formula = formula.Replace("Not", "!").Replace("NOT", "!");

            //between operator [AMOUNT] Between(100, 999999) => Between([AMOUNT],100, 999999)
            if (formula.Contains("Between("))
            {
                formula = ConvertPreParamToInsideParam(formula, "Between");
            }

            return formula;
        }
        private string ConvertPreParamToInsideParam(string formula, string expOperator)
        {
            string flag = "SimpleeeeeeeeeeeSixxxxxxxxxxxxx";
            while (formula.Contains($"{expOperator}("))
            {
                string subFormula = GetFirstSubFormulaOperatorString(formula, expOperator);
                string param = subFormula.Substring(subFormula.IndexOf("["), subFormula.IndexOf("]") + 1);
                string newSubFormula = subFormula.Replace(param, "");
                newSubFormula = newSubFormula.Insert(newSubFormula.IndexOf("(") + 1, param + ", ").Replace(expOperator, flag);
                formula = formula.Replace(subFormula, newSubFormula);
            }
            formula = formula.Replace(flag, expOperator).Trim();
            return formula;
        }
        private string GetFirstSubFormulaOperatorString(string formula, string expOperator, bool PreParam = true)
        {
            string subFormula = formula;
            if (PreParam)
            {
                int indexb = formula.IndexOf($"{expOperator}(");
                int indexp = formula.Substring(0, indexb).LastIndexOf("[");
                subFormula = formula.Substring(indexp);
                subFormula = subFormula.Substring(0, subFormula.IndexOf(")") + 1);
            }
            else
            {
                int indexb = formula.IndexOf($"{expOperator}(");
                subFormula = formula.Substring(indexb);
                int indexp = subFormula.IndexOf(")");
                subFormula = subFormula.Substring(0, indexp + 1);
            }

            return subFormula;
        }

        private string CustomOperatorFormulaSub(string formula)
        {
            //between operator [FROMTODDATE] Between(#2022-05-29#, #2022-06-14#)
            while (formula.Contains("Between("))
            {
                string subFormula = GetFirstSubFormulaOperatorString(formula, "Between");
                string newSubFormula = subFormula.Replace("(", " ").Replace(")", "");
                //incase datetime
                newSubFormula = newSubFormula.Replace("#", "'");
                newSubFormula = newSubFormula.Replace(",", " AND ");
                formula = formula.Replace(subFormula, newSubFormula);
            }

            while (formula.Contains("Contains("))
            {
                string subFormula = GetFirstSubFormulaOperatorString(formula, "Contains", false);
                string newSubFormula = subFormula.Replace("Contains(", "");
                newSubFormula = newSubFormula.Replace(",", " LIKE ");
                newSubFormula = newSubFormula.Insert(newSubFormula.IndexOf("'") + 1, "%");
                newSubFormula = newSubFormula.Insert(newSubFormula.LastIndexOf("'"), "%");
                newSubFormula = newSubFormula.Replace(")", "");
                formula = formula.Replace(subFormula, newSubFormula);
            }

            while (formula.Contains("StartsWith("))
            {
                string subFormula = GetFirstSubFormulaOperatorString(formula, "StartsWith", false);
                string newSubFormula = subFormula.Replace("StartsWith(", "");
                newSubFormula = newSubFormula.Replace(",", " LIKE ");
                newSubFormula = newSubFormula.Insert(newSubFormula.IndexOf("'") + 1, "%");
                newSubFormula = newSubFormula.Replace(")", "");
                formula = formula.Replace(subFormula, newSubFormula);
            }

            while (formula.Contains("EndsWith("))
            {
                string subFormula = GetFirstSubFormulaOperatorString(formula, "EndsWith", false);
                string newSubFormula = subFormula.Replace("EndsWith(", "");
                newSubFormula = newSubFormula.Replace(",", " LIKE ");
                newSubFormula = newSubFormula.Insert(newSubFormula.LastIndexOf("'"), "%");
                newSubFormula = newSubFormula.Replace(")", "");
                formula = formula.Replace(subFormula, newSubFormula);
            }

            return formula;
        }
        private static void FormulaExtensionFunctions(string name, FunctionArgs functionArgs)
        {
            if (name == "Contains")
            {
                var param1 = functionArgs.Parameters[0].Evaluate();
                var param2 = functionArgs.Parameters[1].Evaluate();
                functionArgs.Result = param1.ToString().Contains(param2.ToString());
            }
            else if (name == "Between")
            {
                var param1 = functionArgs.Parameters[0].Evaluate();
                var param2 = functionArgs.Parameters[1].Evaluate();
                var param3 = functionArgs.Parameters[2].Evaluate();
                functionArgs.Result = IsBetween(param1, param2, param3);
            }
            else if (name == "StartsWith")
            {
                var param1 = functionArgs.Parameters[0].Evaluate();
                var param2 = functionArgs.Parameters[1].Evaluate();
                functionArgs.Result = param1.ToString().StartsWith(param2.ToString());
            }
            else if (name == "EndsWith")
            {
                var param1 = functionArgs.Parameters[0].Evaluate();
                var param2 = functionArgs.Parameters[1].Evaluate();
                functionArgs.Result = param1.ToString().EndsWith(param2.ToString());
            }
        }
        private static bool IsBetween(object param1, object param2, object param3)
        {
            if (param3 is int || param3 is long || param3 is float || param3 is double || param3 is decimal)
            {
                decimal dparam1 = Convert.ToDecimal(param1);
                decimal dparam2 = Convert.ToDecimal(param2);
                decimal dparam3 = Convert.ToDecimal(param3);
                return dparam1 > dparam2 && dparam1 < dparam3;
            }
            else if (param3 is string && param3.ToString().StartsWith("#") && param3.ToString().EndsWith("#"))
            {
                DateTime dparam1 = Convert.ToDateTime(param1);
                DateTime dparam2 = Convert.ToDateTime(param2);
                DateTime dparam3 = Convert.ToDateTime(param3);
                return dparam1 > dparam2 && dparam1 < dparam3;
            }
            return false; //not support
        }
        #endregion


        public bool StoreDocumentFileIB(TransactionInfo tran)
        {
            if (tran.Data.ContainsKey(Common.KEYNAME.APPROVED))
            {
                return true;
            }
            try
            {
                Connection con = new Connection();
                DateTime dateTime = DateTime.UtcNow.Date;
                string folder;
                string storetype = con.FillDataSet(Common.ConStr, "IPCSYSVAR_GETBYVARNAME", "SERVERSTORETYPE").Tables[0].Rows[0]["VARVALUE"].ToString();
                if (storetype.Equals("ftp"))
                {
                    string ftpInfo = con.FillDataSet(Common.ConStr, "IPCSYSVAR_GETBYVARNAME", "FPTSERVERINFO").Tables[0].Rows[0]["VARVALUE"].ToString();
                    string[] ftpParam = ftpInfo.Split(new string[] { "|" }, StringSplitOptions.None);
                    string host = ftpParam[0];
                    string port = ftpParam[1] == "" ? "21" : ftpParam[1];
                    string username = ftpParam[2];
                    string password = ftpParam[3];
                    folder = con.FillDataSet(Common.ConStr, "GETPARAMETER", "PATHFPTDOCUMENT").Tables[0].Rows[0]["VARVALUE"].ToString();

                    DataTable tbldocument = (DataTable)tran.Data["DOCUMENT"];
                    if (tbldocument.Rows.Count > 0)
                    {
                        foreach (DataRow row in tbldocument.Rows)
                        {
                            string extension = Common.GetFileExtension(row["Base64"].ToString());
                            string description = String.IsNullOrEmpty(row["FileVerify"].ToString()) == true? extension : row["FileVerify"].ToString();
                            string filesave = Common.ConvertRandomString();
                            string Subfolder = folder + dateTime.ToString("yyyy_MM_dd");
                            string filepath = host + ":" + port + Subfolder;
                            FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(filepath);
                            ftpReq.Credentials = new NetworkCredential(username, password);
                            try
                            {
                                ftpReq.Method = WebRequestMethods.Ftp.ListDirectory;
                                using (FtpWebResponse response = (FtpWebResponse)ftpReq.GetResponse())
                                {
                                    string filepath1 = host + ":" + port + Subfolder + "/" + filesave + extension;
                                    FtpWebRequest ftpReq1 = (FtpWebRequest)WebRequest.Create(filepath1);
                                    ftpReq1.Credentials = new NetworkCredential(username, password);
                                    ftpReq1.UseBinary = true;
                                    ftpReq1.Method = WebRequestMethods.Ftp.UploadFile;
                                    ftpReq1.KeepAlive = true;
                                    byte[] b = System.Convert.FromBase64String(row["Base64"].ToString());
                                    ftpReq1.ContentLength = b.Length;
                                    Stream s = ftpReq1.GetRequestStream();
                                    s.Write(b, 0, b.Length);
                                    s.Close();
                                    string filepathsave = dateTime.ToString("yyyy_MM_dd") + "/" + filesave + extension;
                                    string filename = tran.Data["IPCTRANSID"].ToString() + "#" + tran.Data["USERID"].ToString() + "#" + filepathsave;
                                    string document = Common.EncryptData(filename);
                                    int rs = con.ExecuteNonquery(Common.ConStr, "EBA_BUSINESS_DOCUMENT_INSERT", new object[] {
                                tran.Data["IPCTRANSID"].ToString(),
                                document,
                                description,
                                filepath,
                                tran.Data["USERID"].ToString(),
                                'P',
                                "",
                                dateTime,
                                });
                                }
                            }
                            catch (WebException ex)
                            {
                                if (ex.Response != null)
                                {
                                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable || 
                                        response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailableOrBusy)
                                    {
                                       FtpWebRequest ftpReqCreate = (FtpWebRequest)WebRequest.Create(filepath);
                                        ftpReqCreate.Credentials = new NetworkCredential(username, password);
                                        ftpReqCreate.Method = WebRequestMethods.Ftp.MakeDirectory;
                                        var resp = (FtpWebResponse)ftpReqCreate.GetResponse();
                                        resp.Close();
                                    }
                                }
                                string filepath1 = host + ":" + port + Subfolder + "/" + filesave + extension;
                                FtpWebRequest ftpReq1 = (FtpWebRequest)WebRequest.Create(filepath1);
                                ftpReq1.Credentials = new NetworkCredential(username, password);
                                ftpReq1.UseBinary = true;
                                ftpReq1.Method = WebRequestMethods.Ftp.UploadFile;
                                ftpReq1.KeepAlive = true;
                                byte[] b = System.Convert.FromBase64String(row["Base64"].ToString());
                                ftpReq1.ContentLength = b.Length;
                                Stream s = ftpReq1.GetRequestStream();
                                s.Write(b, 0, b.Length);
                                s.Close();
                                string filepathsave = dateTime.ToString("yyyy_MM_dd") + "/" + filesave + extension;
                                string filename = tran.Data["IPCTRANSID"].ToString() + "#" + tran.Data["USERID"].ToString() + "#" + filepathsave;
                                string document = Common.EncryptData(filename);
                                int rs = con.ExecuteNonquery(Common.ConStr, "EBA_BUSINESS_DOCUMENT_INSERT", new object[] {
                                tran.Data["IPCTRANSID"].ToString(),
                                document,
                                extension,
                                filepath,
                                tran.Data["USERID"].ToString(),
                                'P',
                                "",
                                dateTime,
                                });
                            }
                        }
                    }
                }
                else
                {
                    folder = con.FillDataSet(Common.ConStr, "GETPARAMETER", "PATHLOCALDOCUMENT").Tables[0].Rows[0]["VARVALUE"].ToString();

                    DataTable tbldocument = (DataTable)tran.Data["DOCUMENT"];
                    if (tbldocument.Rows.Count > 0)
                    {
                        foreach (DataRow row in tbldocument.Rows)
                        {
                            string extension = Common.GetFileExtension(row["Base64"].ToString());
                            string description = String.IsNullOrEmpty(row["FileVerify"].ToString()) == true ? extension : row["FileVerify"].ToString();
                            string filesave = Common.ConvertRandomString();
                            string Subfolder = folder + dateTime.ToString("yyyy_MM_dd");
                            if (!Directory.Exists(folder))
                            {
                                Directory.CreateDirectory(folder);
                            }
                            if (!Directory.Exists(Subfolder))
                            {
                                Directory.CreateDirectory(Subfolder);
                            }
                            string filepathsave = Subfolder + "/" + filesave + extension;
                            string filepath = dateTime.ToString("yyyy_MM_dd") + "/" + filesave + extension;
                            string filename = tran.Data["IPCTRANSID"].ToString() + "#" + tran.Data["USERID"].ToString() + "#" + filepath;
                            File.WriteAllBytes(filepathsave, Convert.FromBase64String(row["Base64"].ToString()));
                            string document = Common.EncryptData(filename);
                            int rs = con.ExecuteNonquery(Common.ConStr, "EBA_BUSINESS_DOCUMENT_INSERT", new object[] {
                        tran.Data["IPCTRANSID"].ToString(),
                        document,
                        description,
                        filepath,
                        tran.Data["USERID"].ToString(),
                        'P',
                        "",
                        dateTime,
                        });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                tran.SetErrorInfo(ex);
                return false;
            }           
            return true;
        }

        public bool StoreDocumentFileMB(TransactionInfo tran)
        {
            if (tran.Data.ContainsKey(Common.KEYNAME.APPROVED))
            {
                return true;
            }
            try
            {
                Connection con = new Connection();
                DateTime dateTime = DateTime.UtcNow.Date;
                string folder;
                string storetype = con.FillDataSet(Common.ConStr, "IPCSYSVAR_GETBYVARNAME", "SERVERSTORETYPE").Tables[0].Rows[0]["VARVALUE"].ToString();
                string tbldocument = tran.Data.ContainsKey("DOCUMENT") ? tran.Data["DOCUMENT"].ToString() : string.Empty;
                var json = Encoding.UTF8.GetString(Convert.FromBase64String(tbldocument));
                var obj = JsonConvert.DeserializeObject<List<UploadFileInfo>>(json);
                if (tbldocument.Equals(string.Empty))
                {
                    return false;
                }
                if (storetype.Equals("ftp"))
                {
                    string ftpInfo = con.FillDataSet(Common.ConStr, "IPCSYSVAR_GETBYVARNAME", "FPTSERVERINFO").Tables[0].Rows[0]["VARVALUE"].ToString();
                    string[] ftpParam = ftpInfo.Split(new string[] { "|" }, StringSplitOptions.None);
                    string host = ftpParam[0];
                    string port = ftpParam[1] == "" ? "21" : ftpParam[1];
                    string username = ftpParam[2];
                    string password = ftpParam[3];
                    folder = con.FillDataSet(Common.ConStr, "GETPARAMETER", "PATHFPTDOCUMENT").Tables[0].Rows[0]["VARVALUE"].ToString();
                    for (int i = 0; i < obj.Count; i++)
                    {
                        string documentItem = obj[i].FileBin;
                        string extension = obj[i].FileExtension;
                        string filesave = Common.ConvertRandomString();
                        string Subfolder = folder + dateTime.ToString("yyyy_MM_dd");
                        string filepath = host + ":" + port + Subfolder + "/" + filesave + extension;
                        FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(filepath);
                        ftpReq.Credentials = new NetworkCredential(username, password);
                        ftpReq.Method = WebRequestMethods.Ftp.ListDirectory;
                        try
                        {
                            ftpReq.Method = WebRequestMethods.Ftp.ListDirectory;
                            using (FtpWebResponse response = (FtpWebResponse)ftpReq.GetResponse())
                            {
                                string filepath1 = host + ":" + port + Subfolder + "/" + filesave + extension;
                                FtpWebRequest ftpReq1 = (FtpWebRequest)WebRequest.Create(filepath1);
                                ftpReq1.Credentials = new NetworkCredential(username, password);
                                ftpReq1.UseBinary = true;
                                ftpReq1.Method = WebRequestMethods.Ftp.UploadFile;
                                ftpReq1.KeepAlive = true;
                                byte[] b = System.Convert.FromBase64String(documentItem);
                                ftpReq1.ContentLength = b.Length;
                                Stream s = ftpReq1.GetRequestStream();
                                s.Write(b, 0, b.Length);
                                s.Close();
                                string filepathsave = dateTime.ToString("yyyy_MM_dd") + "/" + filesave + extension;
                                string filename = tran.Data["TXREF"].ToString() + "#" + tran.Data["USERID"].ToString() + "#" + filepathsave;
                                string document = Common.EncryptData(filename);
                                int rs = con.ExecuteNonquery(Common.ConStr, "EBA_BUSINESS_DOCUMENT_INSERT", new object[] {
                                tran.Data["TXREF"].ToString(),
                                document,
                                extension,
                                filepathsave,
                                tran.Data["USERID"].ToString(),
                                'P',
                                "",
                                dateTime,
                                });
                            }
                        }
                        catch (WebException ex)
                        {
                            if (ex.Response != null)
                            {
                                FtpWebResponse response = (FtpWebResponse)ex.Response;
                                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable ||
                                    response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailableOrBusy)
                                {
                                    FtpWebRequest ftpReqCreate = (FtpWebRequest)WebRequest.Create(filepath);
                                    ftpReqCreate.Credentials = new NetworkCredential(username, password);
                                    ftpReqCreate.Method = WebRequestMethods.Ftp.MakeDirectory;
                                    var resp = (FtpWebResponse)ftpReqCreate.GetResponse();
                                    resp.Close();
                                }
                            }
                            string filepath1 = host + ":" + port + Subfolder + "/" + filesave + extension;
                            FtpWebRequest ftpReq1 = (FtpWebRequest)WebRequest.Create(filepath1);
                            ftpReq1.Credentials = new NetworkCredential(username, password);
                            ftpReq1.UseBinary = true;
                            ftpReq1.Method = WebRequestMethods.Ftp.UploadFile;
                            ftpReq1.KeepAlive = true;
                            byte[] b = System.Convert.FromBase64String(documentItem);
                            ftpReq1.ContentLength = b.Length;
                            Stream s = ftpReq1.GetRequestStream();
                            s.Write(b, 0, b.Length);
                            s.Close();
                            string filepathsave = dateTime.ToString("yyyy_MM_dd") + "/" + filesave + extension;
                            string filename = tran.Data["TXREF"].ToString() + "#" + tran.Data["USERID"].ToString() + "#" + filepathsave;
                            string document = Common.EncryptData(filename);
                            int rs = con.ExecuteNonquery(Common.ConStr, "EBA_BUSINESS_DOCUMENT_INSERT", new object[] {
                                tran.Data["TXREF"].ToString(),
                                document,
                                extension,
                                filepathsave,
                                tran.Data["USERID"].ToString(),
                                'P',
                                "",
                                dateTime,
                                });
                        }
                    }
                }
                else
                {
                    folder = con.FillDataSet(Common.ConStr, "GETPARAMETER", "PATHLOCALDOCUMENT").Tables[0].Rows[0]["VARVALUE"].ToString();
                    if (!tbldocument.Equals(string.Empty))
                    {

                        for (int i = 0; i < obj.Count; i++)
                        {
                            string documentItem = obj[i].FileBin;
                            string extension = obj[i].FileExtension; 
                            string filesave = Common.ConvertRandomString();
                            string Subfolder = folder + dateTime.ToString("yyyy_MM_dd");
                            if (!Directory.Exists(folder))
                            {
                                Directory.CreateDirectory(folder);
                            }
                            if (!Directory.Exists(Subfolder))
                            {
                                Directory.CreateDirectory(Subfolder);
                            }
                            string filepathsave = Subfolder + "/" + filesave + extension;
                            string filepath = dateTime.ToString("yyyy_MM_dd") + "/" + filesave + extension;
                            string filename = tran.Data["TXREF"].ToString() + "#" + tran.Data["USERID"].ToString() + "#" + filepath;
                            File.WriteAllBytes(filepathsave, Convert.FromBase64String(documentItem));
                            string document = Common.EncryptData(filename);
                            int rs = con.ExecuteNonquery(Common.ConStr, "EBA_BUSINESS_DOCUMENT_INSERT", new object[] {
                                tran.Data["TXREF"].ToString(),
                                document,
                                extension,
                                filepathsave,
                                tran.Data["USERID"].ToString(),
                                'P',
                                "",
                                dateTime,
                                });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                tran.SetErrorInfo(ex);
                return false;
            }
            return true;
        }


    }
}
