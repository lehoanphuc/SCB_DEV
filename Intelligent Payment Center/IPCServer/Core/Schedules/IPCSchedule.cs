using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBConnection;
using System.Data;
using System.Timers;
using System.Collections;
using Utility;

namespace Schedules
{
    public class IPCSchedule
    {
        static Timer sysSchedule = null;
        static DataTable scheduleAc = new DataTable();
        private static object _lock = new object();
        private static object _lockInit = new object();
        //public static bool IsRunning = false;
        #region Construstor
        /// <summary>
        /// Construstor
        /// </summary>
        public IPCSchedule()
        {
            Connection dbObj = new Connection();
            try
            {
                lock (_lockInit)
                {
                    if (scheduleAc == null || scheduleAc.Rows.Count == 0 || sysSchedule == null)
                    {
                        sysSchedule = null;
                        sysSchedule = new Timer();

                        try
                        {
                            sysSchedule.Elapsed -= new ElapsedEventHandler(sysSchedule_Elapsed);
                        }
                        catch { }
                        sysSchedule.AutoReset = false;
                        sysSchedule.Elapsed += new ElapsedEventHandler(sysSchedule_Elapsed);
                        scheduleAc = dbObj.FillDataTable(Common.ConStr, "IPC_SCHEDULELOADACTION");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbObj = null;
            }
        }
        #endregion

        #region Public fucntion
        public void RunSchedules()
        {
            lock (_lock)
            {
                //System.Diagnostics.Debugger.Launch();
                try
                {
                    if (sysSchedule != null)
                        sysSchedule.Stop();

                    //vutt 20180207
                    sysSchedule = null;
                    sysSchedule = new Timer();
                    sysSchedule.Elapsed -= new ElapsedEventHandler(sysSchedule_Elapsed);
                    sysSchedule.Elapsed += new ElapsedEventHandler(sysSchedule_Elapsed);
                    sysSchedule.AutoReset = false;
                    double nextTime = 30000;

                    if (Utility.Common.CheckCluster(Common.MODULEID.SCHEDULER))
                    {
                        Utility.ProcessLog.LogInformation("Timer hit, start to run scheduler");

                        DataSet lstSchedule = GetListScheduleExec();

                        try
                        {
                            Utility.ProcessLog.LogInformation("Number of running scheduler = " + lstSchedule.Tables[0].Rows.Count.ToString());
                        }
                        catch { }

                        ExecSchedules(lstSchedule);
                        nextTime = getNextTime();
                        Utility.ProcessLog.LogInformation("nexttime=" + nextTime.ToString());
                        if (nextTime < 0)
                        {
                            Utility.ProcessLog.LogInformation("WARN: Get next schedules error or is empty");
                            return;
                        }
                    }
                    else
                    {
                        nextTime = 10000;
                    }
                    sysSchedule.Interval = nextTime;
                    sysSchedule.Start();
                }
                catch (Exception ex)
                {
                    Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
            }
        }
        #endregion

        #region Private function
        private DataSet GetListScheduleExec()
        {
            //System.Diagnostics.Debugger.Launch();
            DataSet result = new DataSet();
            Connection dbObj = new Connection();
            try
            {
                result = dbObj.FillDataSet(Common.ConStr, "IPC_SCHEDULESGETCURRENT");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbObj = null;
            }
        }

        private void sysSchedule_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                //Utility.ProcessLog.LogInformation("=================start run alapsed");
                RunSchedules();
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        private void ExecSchedules(DataSet inputData)
        {
            //System.Diagnostics.Debugger.Launch();
            try
            {

                foreach (DataRow row in inputData.Tables[0].Rows)
                {
                    string schtype = row["SCHEDULETYPE"].ToString().Trim();
                    if (!schtype.Equals("REPEATEDLY"))
                    {
                        string schid = row["SCHEDULEID"].ToString() + DateTime.Now.ToString("ddMMyyyy");
                        if (Common.LstSchedule.items.Contains(schid))
                        {
                            ProcessLog.LogInformation(string.Format("Schdule {0} rejected bcoz duplicate", schid));
                            continue;
                        }
                        else
                        {
                            Common.LstSchedule.Enqueue(schid);
                        }
                    }

                    DataRow[] action = scheduleAc.Select("ACTIONID = '" + row["ACTIONID"].ToString() + "'");
                    DataRow[] para = inputData.Tables[1].Select("SCHEDULEID = '" + row["SCHEDULEID"].ToString() + "'");
                    object[] parm = null;
                    string[] parmName = null;
                    if (action[0]["HASTABLE"].ToString() == "Y")
                    {
                        parm = new object[1];

                        Hashtable hasInput = new Hashtable();
                        foreach (DataRow p in para)
                        {
                            hasInput.Add(p["PARANAME"].ToString(), p["PARAVALUE"]);
                        }
                        hasInput.Add("SCHEDULEID", para[0]["SCHEDULEID"]);
                        if (hasInput.ContainsKey(Common.KEYNAME.IPCTRANCODE))
                        {
                            hasInput[Common.KEYNAME.IPCTRANCODE] = action[0]["IPCTRANCODE"].ToString();
                        }
                        else
                        {
                            hasInput.Add(Common.KEYNAME.IPCTRANCODE, action[0]["IPCTRANCODE"].ToString());
                        }
                        if (hasInput.ContainsKey(Common.KEYNAME.APPROVED))
                        {
                            hasInput[Common.KEYNAME.ISSCHEDULE] = "Y";
                        }
                        else
                        {
                            hasInput.Add(Common.KEYNAME.ISSCHEDULE, "Y");
                        }
                        parm[0] = hasInput;

                        Common.ExecRemoteMethod(action[0]["ASSEMBLYNAME"].ToString(), action[0]["ASSEMBLYTYPE"].ToString(),
                            action[0]["METHODNAME"].ToString(), action[0]["TPCURL"].ToString(), parm);
                    }
                    else
                    {
                        parm = new object[inputData.Tables[1].Rows.Count];
                        parmName = new string[inputData.Tables[1].Rows.Count];
                        for (int i = 0; i < parm.Length; i++)
                        {
                            parm[i] = inputData.Tables[1].Rows[i]["PARAVALUE"];
                            parmName[i] = inputData.Tables[1].Rows[i]["PARANAME"].ToString();
                        }
                        Common.ExecRemoteMethod(action[0]["ASSEMBLYNAME"].ToString(), action[0]["ASSEMBLYTYPE"].ToString(),
                            action[0]["METHODNAME"].ToString(), action[0]["TPCURL"].ToString(), parm, parmName);
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private double getNextTime()
        {
            try
            {
                DataTable result = new DataTable();
                Connection dbObj = new Connection();
                result = dbObj.FillDataTable(Common.ConStr, "IPC_GETNEXTTIMEEXEC");
                if (result != null && result.Rows.Count > 0 && result.Rows[0] != null && result.Rows[0][0] != null)
                {
                    DateTime nextTime;
                    TimeSpan totalTime;
                    try
                    {
                        nextTime = (DateTime)result.Rows[0][0];
                        totalTime = nextTime.Subtract(new DateTime(1900, 1, 1, 0, 0, 0));
                        return totalTime.TotalMilliseconds;
                    }
                    catch (Exception e)
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return -1;
            }
        }
        #endregion
    }
}
