using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using Utility;
using DBConnection;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;

namespace ServiceControl
{
    public class ServiceController
    {
        #region Public Process Begin Of Day Function
        public bool ChangeWorkingDateSystem()
        {
            Connection con = new Connection();
            Transaction.AutoTrans autoTrans = new Transaction.AutoTrans();
            string temp = string.Empty;
            try
            {
                //DUNG CHO SMB
                //string NextDateSB = Common.RetrievePropertyHost("SYSTEM", "NextDate");
                //if (NextDateSB == "") return false;
                //con.ExecuteNonquery(Common.ConStr, "IPCSYSVAR_UPDATE", new object[] { Common.SYSVARNAME.IPCWORKDATE, NextDateSB });
                //DUNG CHO PNB

                if (Common.CheckCluster(Common.MODULEID.SCHEDULER) && Common.TRANSIPCTHREAD.CONFIG_RUNSCHEDULE == true)
                {
                    Hashtable input = new Hashtable();
                    input.Add(Common.KEYNAME.IPCTRANCODE, "SYS00001");
                    input.Add(Common.KEYNAME.SOURCEID, "SYS");
                    Hashtable result = autoTrans.ProcessTransHAS(input);
                    //string workingDate = DateTime.Parse(result["WORKINGDATE"].ToString()).AddDays(1).ToString("dd/MM/yyyy");
                    temp = result["WORKINGDATE"].ToString();
                    string workingDate = DateTime.ParseExact(result["WORKINGDATE"].ToString().Trim(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
                    if (string.IsNullOrEmpty(workingDate) == false)
                    {
                        con.ExecuteNonquery(Common.ConStr, "IPCSYSVAR_UPDATE", new object[] { Common.SYSVARNAME.IPCWORKDATE, workingDate });
                        Utility.Common.IPCWorkDate = workingDate;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + temp);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }
        #endregion

         #region Other scheduling jobs
        public void RunSchedulingJobs()
        {
            Connection con = new Connection();
            try
            {
                DataSet dsJobs = con.FillDataSet(Common.ConStr, "IPCGETSYSJOBS", new object[1] { "" });
                DataTable dtJobDetail = dsJobs.Tables.Count > 1 && dsJobs.Tables[1] != null ? dsJobs.Tables[1] : new DataTable();
                //ProcessLog.LogInformation($"ALL JOB DATA: {JsonConvert.SerializeObject(dsJobs)}", Common.FILELOGTYPE.LOGFILEPATH);
                foreach (DataRow drJobs in dsJobs.Tables[0].Rows)
                {
                    Thread thJobs = new Thread((object obj) =>
                    {
                        try
                        {
                            //System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
                            DataRow objJob = (DataRow)obj;
                            #region init var
                            string JOBID = objJob["JOBSID"].ToString();
                            string NEXTEXEC = string.IsNullOrEmpty(objJob["NEXTEXEC"].ToString()) ? string.Empty : objJob["NEXTEXEC"].ToString();
                            ProcessLog.LogInformation($"Jobs {drJobs["JOBSID"]} started with NextEXEC {NEXTEXEC}");
                            string METHODNAME = objJob["METHODNAME"].ToString();
                            string JOBSTYPE = objJob["JOBSTYPE"].ToString();
                            string IPCTRANCODE = objJob[Common.KEYNAME.IPCTRANCODE].ToString();
                            string SOURCEID = objJob[Common.KEYNAME.SOURCEID].ToString();
                            string USERCREATE = objJob[Common.KEYNAME.USERCREATE].ToString();
                            string INTERVAL = objJob[Common.KEYNAME.INTERVAL].ToString();
                            string UPDATE_SYSJOBS_NEXTEXEC = "UPDATE_SYSJOBS_NEXTEXEC";
                            #endregion
                            Hashtable hasInput = new Hashtable();
                            Hashtable hasOutput = new Hashtable();

                            int interval = -1;

                            hasInput.Add(Common.KEYNAME.IPCTRANCODE, IPCTRANCODE);
                            hasInput.Add(Common.KEYNAME.SOURCEID, SOURCEID);
                            hasInput.Add(Common.KEYNAME.USERID, USERCREATE);
                            hasInput.Add("ISJOBSYS", "Y");


                            if (dtJobDetail.Rows.Count > 0)
                            {
                                DataRow[] listrows = dtJobDetail.Select("JOBSID = '" + JOBID + "'");
                                if (listrows != null)
                                {
                                    foreach (DataRow rowDetail in listrows)
                                    {
                                        try
                                        {
                                            if (hasInput.ContainsKey(rowDetail["PARANAME"]))
                                            {
                                                hasInput[rowDetail["PARANAME"]] = rowDetail["PARAVALUE"];
                                            }
                                            else
                                            {
                                                hasInput.Add(rowDetail["PARANAME"], rowDetail["PARAVALUE"]);
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                        
                                    }
                                }
                            }

                            try
                            {
                                string JobType = JOBSTYPE;
                                while (true)
                                {
                                    switch (JobType.ToUpper())
                                    {
                                        case "REPEATEDLY":
                                            interval = int.Parse(INTERVAL) * 1000;
                                            hasOutput = ExecSchTrans(METHODNAME, hasInput, JOBID, DateTime.Now.AddMilliseconds(interval));
                                            if (hasOutput.ContainsKey(Common.KEYNAME.IPCERRORCODE) && !hasOutput[Common.KEYNAME.IPCERRORCODE].Equals("0"))
                                            {
                                                ProcessLog.LogInformation($"Jobs {JOBID} {JOBSTYPE} {hasInput[Common.KEYNAME.IPCTRANCODE]} ERROR: {hasOutput[Utility.Common.KEYNAME.IPCERRORDESC]}", Common.FILELOGTYPE.LOGFILEPATH);
                                            }
                                            Thread.Sleep(interval);
                                            break;
                                        case "DAILY":
                                        case "WEEKLY":
                                            TimeSpan sleepTimeSpan = TimeSpan.FromSeconds(30);
                                            if (CheckInterval2Run(JOBSTYPE, INTERVAL, ref NEXTEXEC, out sleepTimeSpan))
                                            {
                                                hasOutput = ExecSchTrans(METHODNAME, hasInput, JOBID, DateTime.Parse(NEXTEXEC));
                                                if (hasOutput.ContainsKey(Common.KEYNAME.IPCERRORCODE) && !hasOutput[Utility.Common.KEYNAME.IPCERRORCODE].Equals("0"))
                                                {
                                                    ProcessLog.LogInformation($"Jobs {JOBID} {JOBSTYPE} {hasInput[Common.KEYNAME.IPCTRANCODE]} ERROR: {hasOutput[Utility.Common.KEYNAME.IPCERRORDESC]}", Common.FILELOGTYPE.LOGFILEPATH);
                                                }
                                                else if(hasOutput.ContainsKey(Common.KEYNAME.IPCERRORCODE) && hasOutput[Utility.Common.KEYNAME.IPCERRORCODE].Equals("0"))
                                                {
                                                    ProcessLog.LogInformation($"Jobs {JOBID} {JOBSTYPE} executed and sleep {sleepTimeSpan}", Common.FILELOGTYPE.LOGFILEPATH);
                                                }
                                            }
                                            else
                                            {
                                                if (Common.CheckCluster(Common.MODULEID.SCHEDULER) && Common.TRANSIPCTHREAD.CONFIG_RUNSCHEDULE == true)
                                                {
                                                    con.ExecuteNonquery(Common.ConStr, UPDATE_SYSJOBS_NEXTEXEC, JOBID, NEXTEXEC);
                                                    ProcessLog.LogInformation($"Jobs {JOBID} {JOBSTYPE} has not executed yet and sleep {sleepTimeSpan}");
                                                }
                                            }
                                            
                                            Thread.Sleep(sleepTimeSpan);
                                            break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ProcessLog.LogInformation(ex.Message, Common.FILELOGTYPE.LOGFILEPATH);
                                Thread.Sleep(30000);
                            }
                        }
                        catch (Exception ex)
                        {
                            Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                            Thread.Sleep(30000);
                        }
                    });

                    thJobs.Start(drJobs);
                    Common.lsRunningThreads.Add(drJobs["JOBSID"].ToString(), thJobs);
                }
            }
            catch (Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + " thReversal");
            }
        }

        private Hashtable ExecSchTrans(string Methodname, Hashtable hasInput, string jobid, DateTime TimeToWait)
        {
            Connection con = new Connection();
            try
            {
                if (Common.CheckCluster(Common.MODULEID.SCHEDULER) && Common.TRANSIPCTHREAD.CONFIG_RUNSCHEDULE == true)
                {
                    Hashtable outhas = new Hashtable();
                    ParallelLoopResult result = Parallel.For(0, 2, (int idx) => {
                        if (idx == 0)
                        {
                            try
                            {
                                con.ExecuteNonquery(Common.ConStr, "UPDATE_SYSJOBS_LASTEXEC", jobid, DateTime.Now.ToString());
                            }
                            catch (Exception ex)
                            {
                                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + " thReversal");
                            }

                        }
                        else
                        {
                            try
                            {
                                Transaction.AutoTrans autoTrans = new Transaction.AutoTrans();
                                switch (Methodname.ToUpper())
                                {
                                    case "PROCESSONLYHAS":
                                        outhas = autoTrans.ProcessOnlyHAS(hasInput);
                                        break;
                                    default:
                                        outhas = autoTrans.ProcessTransHAS(hasInput);
                                        break;
                                }
                                con.ExecuteNonquery(Common.ConStr, "UPDATE_SYSJOBS_NEXTEXEC", jobid, TimeToWait.ToString());
                            }
                            catch(Exception ex)
                            {
                                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + " thReversal");
                            }
                        }
                    });
                    return outhas;
                }
            }
            catch(Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + " thReversal");
            }
            finally
            {
                con = null;
            }
            return new Hashtable();
        }

        private bool CheckInterval2Run(string type, string interval, ref string NextExec, out TimeSpan sleepTime)
        {
            try
            {
                int Hour = 0;
                int Minuter = 0;
                int Second = 0;
                
                switch (type.ToUpper())
                {
                    case "DAILY":
                        //sample: 14:20:00
                        string[] daylyinterval = interval.Split(':');
                        Hour = (int.Parse(daylyinterval[0]) >= 0 && int.Parse(daylyinterval[0]) <= 23) ? int.Parse(daylyinterval[0]) : 0;
                        Minuter = (int.Parse(daylyinterval[1]) >= 0 && int.Parse(daylyinterval[1]) <= 59) ? int.Parse(daylyinterval[1]) : 0;
                        Second = daylyinterval.Length > 2 && (int.Parse(daylyinterval[2]) >= 0 && int.Parse(daylyinterval[2]) <= 59) ? int.Parse(daylyinterval[2]) : 0;
                        DateTime dtNextExec = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minuter, Second);
                        if (!string.IsNullOrEmpty(NextExec))
                        {
                            dtNextExec = DateTime.Parse(NextExec);
                        }

                        if (daylyinterval.Length >= 2)
                        {
                            sleepTime = dtNextExec - DateTime.Now;
                            if((sleepTime.TotalSeconds < 1 && sleepTime.TotalSeconds > 0) || (sleepTime.TotalSeconds <= 0))
                            {
                                dtNextExec = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minuter, Second);
                                dtNextExec = dtNextExec <= DateTime.Now ? dtNextExec.AddDays(1) : dtNextExec;
                                NextExec = dtNextExec.ToString();
                                sleepTime = dtNextExec - DateTime.Now;
                                return true;
                            }
                            else
                            {
                                NextExec = dtNextExec.ToString();
                                return false;
                            }
                        }
                        break;
                    case "WEEKLY":
                        //sample: 2,4,7|14:30:00
                        string[] weeklyinterval = interval.Split('|');

                        if (weeklyinterval.Length >= 1)
                        {
                            List<string> listdays = weeklyinterval[0].Split(',').ToList();
                            string[] time = weeklyinterval.Length >= 2 ? weeklyinterval[1].Split(':') : new string[3] { "0", "0", "0" };
                            if (time.Length >= 2)
                            {
                                Hour = (int.Parse(time[0]) >= 0 && int.Parse(time[0]) <= 23) ? int.Parse(time[0]) : 0;
                                Minuter = (int.Parse(time[1]) >= 0 && int.Parse(time[1]) <= 59) ? int.Parse(time[1]) : 0;
                                Second = time.Length > 2 && (int.Parse(time[2]) >= 0 && int.Parse(time[2]) <= 59) ? int.Parse(time[2]) : 0;
                                if (listdays.Count >= 1)
                                {
                                    bool canrun = false;
                                    DateTime dtWait = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minuter, Second);
                                    if (!string.IsNullOrEmpty(NextExec))
                                    {
                                        dtWait = DateTime.Parse(NextExec);
                                    }
                                    DateTime dtNext = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minuter, Second);
                                    for (int i = 0; i < listdays.Count; i++)
                                    {
                                        int DayOfWeek = int.Parse(listdays[i]);
                                        dtNext = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minuter, Second);
                                        sleepTime = dtWait - DateTime.Now;
                                        if (DayOfWeek > 1 && DayOfWeek < 9)
                                        {
                                            int daysUntil = ((DayOfWeek - 1) - (int)dtNext.DayOfWeek + 7) % 7;

                                            dtNext = dtNext.AddDays(daysUntil);
                                            if(dtNext <= DateTime.Now) dtNext = dtNext.AddDays(7);
                                            //if (i == 0 && dtWait <= DateTime.Now && listdays.Contains(((int)dtWait.DayOfWeek + 1).ToString()))
                                            if (i == 0 && ((sleepTime.TotalSeconds > 0 && sleepTime.TotalSeconds < 1) || sleepTime.TotalSeconds <= 0) && listdays.Contains(((int)dtWait.DayOfWeek + 1).ToString()))
                                            {
                                                canrun = true;
                                                dtWait = dtNext;
                                            }
                                            else if(i == 0 && ((sleepTime.TotalSeconds > 0 && sleepTime.TotalSeconds < 1) || sleepTime.TotalSeconds <= 0) && !listdays.Contains(((int)dtWait.DayOfWeek + 1).ToString()))
                                            {
                                                dtWait = dtNext;
                                            }

                                            if (dtWait > dtNext)
                                            {
                                                dtWait = dtNext;
                                            }
                                        }
                                    }
                                    NextExec = dtWait.ToString();
                                    sleepTime = dtWait - DateTime.Now;
                                    return canrun;
                                }
                            }
                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + " thReversal");
            }
            sleepTime = TimeSpan.FromSeconds(30);
            return false;
        }
        #endregion
    }
}