using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using System.Reflection;
using Utility;
using DBConnection;
using Formatters;
using System.Configuration;

namespace Transaction
{
    public static class TransUtility
    {
        //vutran 24072015
        public static bool CheckDuplicateTran(TransactionInfo tran, Hashtable InputData)
        {
            return true;
        }
        public static bool CheckSession(TransactionInfo tran)
        {
            try
            {
                try
                {
                    if (bool.Parse(ConfigurationManager.AppSettings["AllowMBVersion1"].ToString()))
                    {
                        if (tran.Data.ContainsKey("APPVER"))
                        {
                            return true;
                        }
                    }
                }
                catch { }

                //for MB only
                if (tran.Data[Common.KEYNAME.SOURCEID].ToString().Equals(Common.KEYNAME.MB) || tran.Data[Common.KEYNAME.SOURCEID].ToString().Equals(Common.KEYNAME.AM))
                {
                    string tranCode = tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Trim();

                    if (Common.DBITRANLIST.Select($"IPCTRANCODE = '{tranCode}' AND SERVICEID IN ('MB','AM') AND ISDEFAULT = 'Y'").Length > 0)
                    {
                        return true;
                    }
                    else if (tran.Data.ContainsKey(Common.KEYNAME.DEVICEID) && tran.Data.ContainsKey(Common.KEYNAME.UUID))
                    {
                        Connection con = new Connection();
                        int count = con.FillDataTable(Common.ConStr, "MB_CHECKSESSION",
                            tran.Data[Common.KEYNAME.USERID].ToString().Trim(),
                            tran.Data[Common.KEYNAME.SOURCEID].ToString(),
                            tran.Data[Common.KEYNAME.UUID].ToString().Trim(),
                            tran.Data[Common.KEYNAME.DEVICEID].ToString().Trim(),
                            tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Trim()
                            ).Rows.Count;
                        con = null;
                        if (count > 0)
                            return true;
                    }
                }
                else //ib, sems
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
            }
            tran.SetErrorInfo("99999", "Invalid session");
            return false;
        }
        public static bool CheckUserPermission(TransactionInfo tran)
        {
            try
            {
                try
                {
                    if (bool.Parse(ConfigurationManager.AppSettings["AllowMBVersion1"].ToString()))
                    {
                        if (tran.Data.ContainsKey("APPVER"))
                        {
                            return true;
                        }
                    }
                }
                catch { }

                //for MB only
                if (tran.Data[Common.KEYNAME.SOURCEID].ToString().Equals(Common.KEYNAME.MB) || tran.Data[Common.KEYNAME.SOURCEID].ToString().Equals(Common.KEYNAME.AM))
                {
                    string tranCode = tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Trim();

                    if (Common.DBITRANLIST.Select($"IPCTRANCODE = '{tranCode}' AND SERVICEID IN ('MB','AM') AND ISDEFAULT = 'Y'").Length > 0)
                    {
                        return true;
                    }
                    else if (tran.Data.ContainsKey(Common.KEYNAME.DEVICEID) && tran.Data.ContainsKey(Common.KEYNAME.UUID))
                    {
                        Connection con = new Connection();
                        int count = con.FillDataTable(Common.ConStr, "MB_CHECKUSERPERMISSION",
                            tran.Data[Common.KEYNAME.USERID].ToString().Trim(),
                            tran.Data[Common.KEYNAME.SOURCEID].ToString(),
                            tran.Data[Common.KEYNAME.IPCTRANCODE].ToString().Trim()
                            ).Rows.Count;
                        con = null;
                        if (count >= 0)
                            return true;
                    }
                }
                else //ib, sems
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
            }
            tran.SetErrorInfo("9978", "Permission Denied");
            return false;
        }
        public static bool LogInput(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                con.ExecuteNonquery(Common.ConStr, "IPCLOGMESSAGE_INSERT", Common.IPCWorkDate,
                        tran.IPCTransID, Common.LOGTYPE.INPUTSOURCE, tran.MessageTypeSource, tran.InputData, tran.Data["IPCTRANCODE"], tran.Data["SOURCEID"], tran.Data.ContainsKey("DESTID") ? tran.Data["DESTID"] : string.Empty, tran.Data.ContainsKey("USERID") ? tran.Data["USERID"] : string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
                return false;
            }
            finally
            {
                con = null;
            }
        }
        public static bool CheckKillSqlInjection(string TexttoValidate)
        {
            string TextVal;

            TextVal = TexttoValidate;
            if (String.IsNullOrEmpty(TextVal))
            {
                return true;
            }

            //Build an array of characters that need to be filter.
            string[] strDirtyQueryString = { "xp_", "$", "#", "%", "*", "^", "&", "!", ";", "--", "<", "＜", "＞", ">" };

            //Loop through all items in the array
            foreach (string item in strDirtyQueryString)
            {
                if (TextVal.IndexOf(item) != -1)
                {
                    return false;
                }
            }

            return true;
        }
        public static bool CheckSqlInjection(TransactionInfo tran)
        {
            try
            {
                if (tran.Data[Common.KEYNAME.SOURCEID].ToString().Equals(Common.KEYNAME.SOURCEIBVALUE))
                {
                    if (!bool.Parse(ConfigurationManager.AppSettings["CheckSqlInjectionTran"].ToString()))
                    {
                            return true;
                    }
                    foreach (string key in tran.Data.Keys)
                    {
                        //tran.Data.Add(key, InputData[key]);
                        //tran.InputData += key + "$" + InputData[key].ToString() + "#";
                        if (!CheckKillSqlInjection(tran.Data[key].ToString()))
                        {
                            tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                            tran.ErrorDesc = "Something is wrong with the system. Please try again";
                            Utility.ProcessLog.LogInformation("Log Check SQL Injection" + " Key " + key + " and values " + tran.Data[key].ToString());
                            return false;
                        }


                    }
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                return false;
            }
            return true;
        }
        public static bool LogOutput(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                con.ExecuteNonquery(Common.ConStr, "IPCLOGMESSAGE_INSERT", Common.IPCWorkDate,
                    tran.IPCTransID, Common.LOGTYPE.OUTPUTSOURCE, tran.MessageTypeSource, tran.OutputData, tran.Data["IPCTRANCODE"], tran.Data["SOURCEID"], tran.Data.ContainsKey("DESTID") ? tran.Data["DESTID"] : string.Empty, tran.Data.ContainsKey("USERID") ? tran.Data["USERID"] : string.Empty);
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        public static bool AddDataDefine(TransactionInfo tran)
        {
            try
            {
                DataRow[] dr = Common.DBIDATADEFINE.Select("SOURCEID = '' OR SOURCEID = '" +
                                                tran.Data[Common.KEYNAME.SOURCEID].ToString() +
                                                "' AND (IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() +
                                                "' OR IPCTRANCODE = '')");
                if (dr != null)
                {
                    for (int row = 0; row < dr.Length; row++)
                    {
                        if (tran.Data.ContainsKey(dr[row]["FIELDNAME"].ToString()) == false)
                        {
                            tran.Data.Add(dr[row]["FIELDNAME"].ToString(), dr[row]["FIELDVALUE"].ToString());
                        }
                    }
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

        public static bool ChangeTranOfflineStatus(TransactionInfo tran)
        {
            try
            {

                DataRow[] dr = Common.DBIERRORLIST.Select("IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() +
                                                          "' AND ERRORCODE = '" + tran.ErrorCode + "'");

                if (dr != null && dr.Length > 0)
                {
                    tran.ErrorDesc = dr[0][Common.KEYNAME.ERRORDESC].ToString();
                }
                if (tran.ErrorCode != "0")
                {
                    tran.Data[Common.KEYNAME.OFFLSTS] = Common.OFFLSTS.ERRORSYN;
                }
                else if (tran.Data[Common.KEYNAME.OFFLSTS].ToString() == Common.OFFLSTS.BEGINSYN)
                {
                    tran.Data[Common.KEYNAME.OFFLSTS] = Common.OFFLSTS.FINISHSYN;
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

        public static bool UpdateTransactionOffline(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                con.ExecuteNonquery(Common.ConStr, "IPCLOGTRANS_OFFLINE_UPDATE", Common.IPCWorkDate, tran.IPCTransID,
                                                                tran.Data[Common.KEYNAME.OFFLSTS].ToString(), tran.Data[Common.KEYNAME.DESTTRANREF],
                                                                tran.ErrorCode, tran.ErrorDesc);
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

        public static bool GetLogTranDetailInputSyn(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                DataTable dt = con.FillDataTable(Common.ConStr, "IPCLOGTRANSDETAIL_SELECT",
                    new object[] { Common.IPCWorkDate, tran.IPCTransID, Common.LOGTYPE.INPUTSOURCE });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (tran.Data.ContainsKey(dt.Rows[i]["FIELDNAME"]) == false)
                    {
                        tran.Data.Add(dt.Rows[i]["FIELDNAME"], dt.Rows[i]["FIELDVALUE"].ToString());
                    }
                }
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
    }
}