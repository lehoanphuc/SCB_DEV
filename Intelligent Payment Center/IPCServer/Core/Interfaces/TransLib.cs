using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using DBConnection;
using Utility;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Interfaces
{
    public class TransLib
    {
        public delegate bool DelegateLogTransactionDetail(TransactionInfo tran, string LogType);

        public bool AutoRouter(TransactionInfo tran, string TranCode)
        {
            bool result = true;
            Connection con = new Connection();
            try
            {
                string OriDest = (tran.Data[Common.KEYNAME.DESTID] != null) ? tran.Data[Common.KEYNAME.DESTID].ToString() : "";

                if (TranCode.Contains("|"))
                {
                    string[] parmlist = TranCode.Split('|');
                    object[] parms = new object[parmlist.Length - 1];
                    for (int j = 1; j < parmlist.Length; j++)
                    {
                        if (tran.Data.ContainsKey(parmlist[j].ToString()))
                            parms[j - 1] = tran.Data[parmlist[j].ToString()];
                        else
                            parms[j - 1] = parmlist[j].ToString();
                    }

                    string storeName = parmlist[0].ToString();
                    DataTable dt = con.FillDataTable(Common.ConStr, storeName, parms);
                    if (dt.Rows.Count == 0)
                    {
                        return true;
                        //tran.ErrorCode=Common.ERRORCODE.INVALID_MESSAGE_REQUEST;
                        //return false;
                    }
                    else
                    {
                        TranCode = dt.Rows[0][Common.KEYNAME.IPCTRANCODEDEST].ToString();
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            if (!tran.Data.ContainsKey(dt.Columns[col].ColumnName))
                                tran.Data.Add(dt.Columns[col].ColumnName, dt.Rows[0][col]);
                            else
                                tran.Data[dt.Columns[col].ColumnName] = dt.Rows[0][col];
                        }
                    }
                }

                if (tran.Data.ContainsKey(Common.KEYNAME.IPCTRANCODEDEST))
                {
                    tran.Data[Common.KEYNAME.IPCTRANCODEDEST] = TranCode;
                }
                else
                {
                    tran.Data.Add(Common.KEYNAME.IPCTRANCODEDEST, TranCode);
                }
                if (tran.Data[Common.KEYNAME.DESTID] != null)
                {
                    // Execute Component
                    DataTable listExec = con.FillDataTable(Common.ConStr, "IPCCOMPONENT_SELECT",
                        tran.Data[Common.KEYNAME.SOURCEID], tran.Data[Common.KEYNAME.DESTID],
                        TranCode, tran.Data[Common.KEYNAME.REVERSAL]);
                    if (listExec != null && listExec.Rows.Count > 0)
                    {
                        tran.Online = (listExec.Rows[0]["STATUS"].ToString() == Common.SYSTEMSTS.ONLINE);
                        string ORD = "0";
                        for (int i = 0; i < listExec.Rows.Count; i++)
                        {
                            //check condition
                            if (!Utility.Common.CheckCondition(tran, listExec.Rows[i]["CONDITION"].ToString()))
                            {
                              //  ORD = listExec.Rows[i]["SU"].ToString();
                                continue;
                            }

                            if (ORD == "0" || ORD == listExec.Rows[i]["ORD"].ToString())
                            {
                                if (Common.ExecCom(listExec.Rows[i]["ASSEMBLYFILE"].ToString(),
                                    listExec.Rows[i]["ASSEMBLYTITLE"].ToString(),
                                    listExec.Rows[i]["METHOD"].ToString(), listExec.Rows[i]["PARMLIST"].ToString(), tran))
                                {
                                    ORD = listExec.Rows[i]["SU"].ToString();
                                    //thaity modify at 13/11/2014
                                    if (ORD == "-1") // if FA = -1 we will stop transaction
                                    {
                                        result = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    ORD = listExec.Rows[i]["FA"].ToString();
                                    //vutran add log
                                    Utility.ProcessLog.LogInformation(string.Format("Transaction {0} was failed on step {1}, method {2}", tran.IPCTransID.ToString(), listExec.Rows[i]["ORD"].ToString(), listExec.Rows[i]["METHOD"].ToString()));

                                    //thaity modify at 13/11/2014
                                    if (ORD == "-1") // if FA = -1 we will stop transaction
                                    {
                                        result = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    tran.ErrorCode = Common.ERRORCODE.DESTID_NULL;
                    return false;
                }

                tran.Data[Common.KEYNAME.DESTID] = OriDest;
                if (!tran.ErrorCode.Equals(Common.ERRORCODE.OK)) return false;

            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return result;
        }

        public bool CheckReversal(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                DataTable logtrans = con.FillDataTable(Common.ConStr, "IPCCHECKEXIST_TRANREF",
                    tran.Data[Common.KEYNAME.SOURCEID], tran.Data[Common.KEYNAME.DESTID],
                    tran.Data[Common.KEYNAME.IPCTRANCODE],
                    tran.Data[Common.KEYNAME.SOURCETRANREF]);
                if (tran.Data[Common.KEYNAME.REVERSAL].ToString() == "N") // Giao dich binh thuong
                {
                    if (logtrans != null && logtrans.Rows.Count > 0)
                    {
                        tran.ErrorCode = Common.ERRORCODE.SOURCETRANREF_EXISTED; // So Ref nguon da ton tai
                        return false;
                    }
                }
                else // Giao dich Reversal
                {
                    if (logtrans == null || logtrans.Rows.Count <= 0)
                    {
                        tran.ErrorCode = Common.ERRORCODE.SOURCETRANREF_NOTEXIST; // So Ref can Reversal ko ton tai
                        return false;
                    }
                    else
                    {
                        if (logtrans.Rows[0][Common.KEYNAME.DELETED].ToString().ToUpper() == "TRUE")
                        {
                            tran.ErrorCode = Common.ERRORCODE.TRAN_REVERTED; // Giao dich da duoc Reversal
                            return false;
                        }
                        else
                        {
                            tran.IPCTransIDReversal = long.Parse(logtrans.Rows[0][Common.KEYNAME.IPCTRANSID].ToString());

                            //HungNM10
                            if (tran.Data.ContainsKey(Common.KEYNAME.DESTTRANREF))
                                tran.Data[Common.KEYNAME.DESTTRANREF] =
                                    logtrans.Rows[0][Common.KEYNAME.DESTTRANREF].ToString();
                            else
                                tran.Data.Add(Common.KEYNAME.DESTTRANREF,
                                    logtrans.Rows[0][Common.KEYNAME.DESTTRANREF].ToString());

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        public bool ReversalTran(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                tran.Data[Common.KEYNAME.DESTTRANREF] = tran.Data[Common.KEYNAME.DESTTRANREF].ToString() + "-" +
                                                        tran.IPCTransIDReversal.ToString();
                if (!tran.Data.ContainsKey(Common.KEYNAME.DELETED))
                    tran.Data.Add(Common.KEYNAME.DELETED, "1");
                else
                    tran.Data[Common.KEYNAME.DELETED] = "1";

                con.ExecuteNonquery(Common.ConStr, "IPCLOGTRANS_REVERSAL",
                    new object[] { Common.IPCWorkDate, tran.IPCTransID, tran.IPCTransIDReversal });
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        public bool GetLogTranDetailInput(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                DataTable dt = con.FillDataTable(Common.ConStr, "IPCLOGTRANSDETAIL_SELECT",
                    new object[] { Common.IPCWorkDate, tran.IPCTransIDReversal, Common.LOGTYPE.INPUTSOURCE });
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
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        public bool LogTransaction(TransactionInfo tran)
        {
            if (tran.Data.ContainsKey(Common.KEYNAME.APPROVED) && tran.Data[Common.KEYNAME.APPROVED].ToString() == "Y")
            {
                return true;
            }
            Connection con = new Connection();
            try
            {
                object[] parm = new object[68];
                parm[0] = Common.IPCWorkDate;
                parm[1] = tran.IPCTransID;
                parm[2] = tran.Data[Common.KEYNAME.IPCTRANCODE];
                parm[3] = tran.Data.ContainsKey(Common.KEYNAME.CCYID) ? tran.Data[Common.KEYNAME.CCYID].ToString() : string.Empty;
                parm[4] = tran.Data[Common.KEYNAME.SOURCEID];
                parm[5] = tran.Data[Common.KEYNAME.SOURCETRANREF];
                parm[6] = tran.Data[Common.KEYNAME.USERID];
                parm[7] = tran.Data[Common.KEYNAME.TRANDESC];
                parm[8] = Common.TRANSTATUS.BEGIN;
                parm[9] = tran.Data[Common.KEYNAME.APPROVESTATUS];
                parm[10] = Common.OFFLSTS.BEGIN;
                parm[11] = Common.DELSTS.NORMAL;
                parm[12] = tran.Online;
                parm[63] = tran.Data[Common.KEYNAME.ISBATCH];
                parm[64] = tran.Data[Common.KEYNAME.BATCHREF];
                parm[65] = tran.Data[Common.KEYNAME.AUTHENTYPE];
                parm[66] = tran.Data[Common.KEYNAME.AUTHENCODE];
                parm[67] = tran.Data[Common.KEYNAME.DESTID];
                for (int i = parm.Length - 55; i < parm.Length - 25; i++)
                {
                    parm[i] = "";
                }
                for (int i = parm.Length - 25; i < parm.Length - 5; i++)
                {
                    parm[i] = 0;
                }
                //add cac parm char,num
                DataRow[] dtrLogDefine =
                    Common.DBILOGDEFINE.Select("(IPCTRANCODE = '' OR IPCTRANCODE = '" + parm[2].ToString() +
                                               "') AND LOGTYPE = 'I'");
                for (int i = 0; i < dtrLogDefine.Length; i++)
                {
                    if (tran.Data.ContainsKey(dtrLogDefine[i]["FIELDNAME"].ToString()))
                    {
                        if (dtrLogDefine[i]["PARMTYPE"].ToString() == "N")
                        {
                            try
                            {
                                parm[int.Parse(dtrLogDefine[i]["POS"].ToString()) + 42] =
                                    double.Parse(tran.Data[dtrLogDefine[i]["FIELDNAME"].ToString()].ToString());
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            parm[int.Parse(dtrLogDefine[i]["POS"].ToString()) + 12] =
                                tran.Data[dtrLogDefine[i]["FIELDNAME"].ToString()].ToString();
                        }
                    }
                }

                con.ExecuteNonquery(Common.ConStr, "IPCLOGTRANS_INSERT", parm);
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        public bool SetPendingTransaction(TransactionInfo tran)
        {
            if (tran.Status != Common.TRANSTATUS.ERROR)
            {
                tran.Status = Common.TRANSTATUS.PENDDING;
            }
            return UpdateTransaction(tran);
        }

        public bool UpdateTransaction(TransactionInfo tran)
        {
            if (tran.Status == Common.TRANSTATUS.WAITING_APPROVE)
                return true;
            Connection con = new Connection();
            try
            {
                string OfflineStatus = (tran.Data[Common.KEYNAME.OFFLSTS] == null
                    ? Common.OFFLSTS.BEGIN
                    : tran.Data[Common.KEYNAME.OFFLSTS].ToString());
                if (tran.Data[Common.KEYNAME.DELETED] == null)
                {
                    con.ExecuteNonquery(Common.ConStr, "IPCLOGTRANS_UPDATE", Common.IPCWorkDate, tran.IPCTransID,
                        tran.Status, tran.Data[Common.KEYNAME.APPROVESTATUS], OfflineStatus, Common.DELSTS.NORMAL,
                        tran.Data[Common.KEYNAME.DESTID], tran.Data[Common.KEYNAME.DESTTRANREF],
                        tran.Data[Common.KEYNAME.DESTERRORCODE], tran.ErrorCode, tran.ErrorDesc, tran.Online);
                }
                else
                {
                    con.ExecuteNonquery(Common.ConStr, "IPCLOGTRANS_UPDATE", Common.IPCWorkDate, tran.IPCTransID,
                        tran.Status, tran.Data[Common.KEYNAME.APPROVESTATUS], OfflineStatus,
                        tran.Data[Common.KEYNAME.DELETED], tran.Data[Common.KEYNAME.DESTID],
                        tran.Data[Common.KEYNAME.DESTTRANREF], tran.Data[Common.KEYNAME.DESTERRORCODE],
                        tran.ErrorCode, tran.ErrorDesc, tran.Online);
                }
                //log update define
                DataRow[] dtrLogDefine =
                    Common.DBILOGDEFINE.Select("(IPCTRANCODE = '' OR IPCTRANCODE = '" +
                                               tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "') AND LOGTYPE = 'U'");
                if (dtrLogDefine.Length > 0)
                {
                    object[] parm = new object[51];
                    for (int i = 1; i < 31; i++)
                    {
                        parm[i] = "";
                    }
                    for (int i = 31; i < 51; i++)
                    {
                        parm[i] = 0;
                    }
                    //add cac parm char,num
                    parm[0] = tran.Data[Common.KEYNAME.IPCTRANSID].ToString();
                    for (int i = 0; i < dtrLogDefine.Length; i++)
                    {
                        if (tran.Data.ContainsKey(dtrLogDefine[i]["FIELDNAME"].ToString()))
                        {
                            if (dtrLogDefine[i]["PARMTYPE"].ToString() == "N")
                            {
                                try
                                {
                                    parm[int.Parse(dtrLogDefine[i]["POS"].ToString()) + 30] =
                                        double.Parse(tran.Data[dtrLogDefine[i]["FIELDNAME"].ToString()].ToString());
                                }
                                catch
                                {
                                    parm[int.Parse(dtrLogDefine[i]["POS"].ToString()) + 30] = 0;
                                }
                            }
                            else
                            {
                                try
                                {
                                    parm[int.Parse(dtrLogDefine[i]["POS"].ToString())] =
                                        tran.Data[dtrLogDefine[i]["FIELDNAME"].ToString()].ToString();
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
                //LanNTH - insert transaction into new table IPCLOGTRANSFORMULA
                //try
                //{
                //    if (tran.ErrorCode == Common.ERRORCODE.OK)
                //    {
                //        con.ExecuteNonquery(Common.ConStr, "IPCLOGTRANSFORMULA_INSERT", tran.IPCTransID);
                //    }
                //}
                //catch { }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                con.ExecuteNonquery(Common.ConStr, "IPC_CHECKREVERSAL", tran.IPCTransID, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), Common.ERRORCODE.SYSTEM);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        public bool LogTransactionDetail(TransactionInfo tran, string LogType)
        {
            if (tran.Data.ContainsKey(Common.KEYNAME.APPROVED) && tran.Data[Common.KEYNAME.APPROVED].ToString() == "Y")
            {
                return true;
            }
            if (tran.ErrorCode != Common.ERRORCODE.OK) return false;
            if (tran.Data[Common.KEYNAME.REVERSAL].ToString() != "N") return false;
            Connection con = new Connection();
            try
            {
                DataRow[] dr = Common.DBILOGTRANSDETAILFIELD.Select(
                    "SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'" +
                    " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'" +
                    " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "'" +
                    " AND LOGTYPE = '" + LogType + "'");
                if (dr != null)
                {
                    object[] para = new object[dr.Length * 2 + 3];
                    para[0] = Common.IPCWorkDate;
                    para[1] = tran.IPCTransID;
                    para[2] = LogType;
                    for (int i = 0; i < dr.Length; i++)
                    {
                        string value = "";
                        string valuestyle = "";
                        string valuename = "";
                        if (tran.Online)
                        {
                            valuestyle = dr[i]["VALUESTYLEON"].ToString();
                            valuename = dr[i]["VALUENAMEON"].ToString();
                        }
                        else
                        {
                            valuestyle = dr[i]["VALUESTYLEOFF"].ToString();
                            valuename = dr[i]["VALUENAMEOFF"].ToString();
                        }

                        switch (valuestyle)
                        {
                            case "DATA":
                                if (tran.Data.ContainsKey(valuename)) value = tran.Data[valuename].ToString();
                                break;
                            case "PARM":
                                string[] listvaluename = valuename.Split('|');
                                object parmtemp = tran.parm;
                                for (int j = 0; j < listvaluename.Length; j++)
                                {
                                    string[] valueindex = listvaluename[j].Split(',');
                                    if (valueindex.Length == 1)
                                    {
                                        parmtemp = ((object[])parmtemp)[int.Parse(valueindex[0])];
                                    }
                                    else if (valueindex.Length == 2)
                                    {
                                        parmtemp =
                                            ((object[,])parmtemp)[int.Parse(valueindex[0]), int.Parse(valueindex[1])];
                                    }
                                    else if (valueindex.Length == 3)
                                    {
                                        parmtemp =
                                            ((object[,,])parmtemp)[
                                                int.Parse(valueindex[0]), int.Parse(valueindex[1]),
                                                int.Parse(valueindex[2])];
                                    }
                                }
                                value = parmtemp.ToString();
                                break;
                        }

                        para[i * 2 + 3] = dr[i]["FIELDNAME"];
                        para[i * 2 + 4] = value;

                    }
                    con.ExecuteNonquery(Common.ConStr, "IPCLOGTRANSDETAIL_INSERT", false, para);
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        public bool LogTransactionDetailAsync(TransactionInfo tran, string LogType)
        {
            try
            {
                DelegateLogTransactionDetail delLTD = new DelegateLogTransactionDetail(LogTransactionDetail);
                delLTD.BeginInvoke(tran, LogType, null, null);
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }

        public bool ChangeTranStatus(TransactionInfo tran)
        {
            try
            {
                if (tran.Status == Common.TRANSTATUS.WAITING_APPROVE)
                    return true;
                DataRow[] dr = Common.DBIERRORLIST.Select("(IPCTRANCODE = '' OR IPCTRANCODE = '" +
                                                          tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() +
                                                          "') AND ERRORCODE = '" + tran.ErrorCode + "'",
                    "IPCTRANCODE DESC");

                if (dr != null && dr.Length > 0)
                {
                    tran.ErrorDesc = dr[0][Common.KEYNAME.ERRORDESC].ToString();
                }
                if (tran.ErrorCode != "0")
                {
                    tran.Status = Common.TRANSTATUS.ERROR;
                }
                else if (tran.Status == Common.TRANSTATUS.BEGIN)
                {
                    tran.Status = Common.TRANSTATUS.FINISH;
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }

        public static bool LogInput(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                con.ExecuteNonquery(Common.ConStr, "IPCLOGMESSAGE_INSERT", Common.IPCWorkDate,
                    tran.IPCTransID, Common.LOGTYPE.INPUTDEST, tran.MessageTypeDest, tran.InputData, tran.Data["IPCTRANCODE"], tran.Data["SOURCEID"], tran.Data.ContainsKey("DESTID") ? tran.Data["DESTID"] : string.Empty, tran.Data.ContainsKey("USERID") ? tran.Data["USERID"] : string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                return false;
            }
            finally
            {
                con = null;
            }
        }

        public static bool LogOutput(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                con.ExecuteNonquery(Common.ConStr, "IPCLOGMESSAGE_INSERT", Common.IPCWorkDate,
                    tran.IPCTransID, Common.LOGTYPE.OUTPUTDEST, tran.MessageTypeDest, tran.OutputData, tran.Data["IPCTRANCODE"], tran.Data["SOURCEID"], tran.Data.ContainsKey("DESTID") ? tran.Data["DESTID"] : string.Empty, tran.Data.ContainsKey("USERID") ? tran.Data["USERID"] : string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                return false;
            }
            finally
            {
                con = null;
            }
        }

        public static bool PayBillChoLon(TransactionInfo tran)
        {
            try
            {
                bool b = LogWaterBillPaymentFirst(tran.Data["ACCTNO"].ToString());
                string KeyID = tran.Data["KEYID"].ToString();
                string[] ctrs = KeyID.Split('#');
                foreach (string ctr in ctrs)
                {
                    string[] keybill = ctr.Split('|');
                    bool rs;
                    wsChoLon.Service WS = new wsChoLon.Service();
                    WS.Url = System.Configuration.ConfigurationManager.AppSettings["WSWATERPAYMENT"].ToString();
                    rs = WS.payW_Bill(keybill[0].ToString(), "PHUONGNAM", DateTime.Now.Day.ToString());
                    if (rs == true)
                    {
                        bool a = LogWaterBillPayment(keybill[0].ToString(), tran.Data["USERID"].ToString(), "S",
                            tran.Data["ACCTNO"].ToString(), keybill[1].ToString(), keybill[2].ToString());
                    }
                    else
                    {
                        tran.ErrorCode = "9";
                        bool a = LogWaterBillPayment(keybill[0].ToString(), tran.Data["USERID"].ToString(), "F",
                            tran.Data["ACCTNO"].ToString(), keybill[1].ToString(), keybill[2].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                return false;
            }
            return true;
        }

        public static bool GetBillChoLon(TransactionInfo tran)
        {
            try
            {
                wsChoLon.Service WS = new wsChoLon.Service();
                WS.Url = System.Configuration.ConfigurationManager.AppSettings["WSWATERPAYMENT"].ToString();
                DataSet result = WS.getW_Bill(tran.Data["CUSTCODE"].ToString(), "PHUONGNAM", DateTime.Now.Day.ToString());
                tran.Data.Add("BILLINFO", result);
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                return false;
            }
            return true;
        }


        private static bool LogWaterBillPayment(string IDkey, string userid, string status, string PAYERACCTNO,
            string BILLNO, string AMOUNT)
        {
            Connection con = new Connection();
            try
            {
                con.ExecuteNonquery(Common.ConStr, "EBA_INSERT_LOG_BILLNUOCCHOLON", IDkey, userid, status, PAYERACCTNO,
                    BILLNO, AMOUNT);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        private static bool LogWaterBillPaymentFirst(string PAYERACCTNO)
        {
            Connection con = new Connection();
            try
            {
                con.ExecuteNonquery(Common.ConStr, "EBA_INSERT_LOG_BILLNUOCCHOLON_FIRST", PAYERACCTNO);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        public bool GetInfoByAcctNo(TransactionInfo tran, string parmlist)
        {
            try
            {
                Connection con = new Connection();
                string ServiceID = string.Empty;
                if(tran.Data.ContainsKey(Common.KEYNAME.SERVICEID))
                {
                    ServiceID = tran.Data[Common.KEYNAME.SERVICEID].ToString();
                }
                else
                {
                    ServiceID = tran.Data[Common.KEYNAME.SOURCEID].ToString();
                }
                DataTable dtAceptAccount = con.FillDataTable(Common.ConStr, "MB_GETACCTRIGHT", ServiceID, tran.Data[Common.KEYNAME.USERID], tran.Data[Common.KEYNAME.TRANCODETORIGHT], tran.Data[Common.KEYNAME.ACCTTYPE], tran.Data[Common.KEYNAME.CCYID], tran.Data[Common.KEYNAME.TRANCODEMORE]);
                if (tran.Data.Contains(Common.KEYNAME.DATARESULT))
                {
                    if (tran.Data[Common.KEYNAME.DATARESULT] != null && (tran.Data[Common.KEYNAME.DATARESULT] as DataSet).Tables.Count > 0)
                    {
                        DataSet ds = (tran.Data[Common.KEYNAME.DATARESULT] as DataSet);
                        DataTable dt = ds.Tables[0];
                        dt.Columns.Add("UNIQUEID");
                        dt.Columns.Add("ORD");

                        if (!tran.Data[Common.KEYNAME.SOURCEID].ToString().Equals("SEMS"))
                        {
                            int no = dt.Rows.Count - 1;
                            for (int i = no; i >= 0; i--)
                            {
                                string accountno = dt.Rows[i]["accountno"].ToString();
                                DataRow[] rows = dtAceptAccount.Select("ACCTNO='" + accountno + "'");
                                if (rows.Count() == 0)
                                {
                                    dt.Rows[i].Delete();
                                }
                                else
                                {
                                    dt.Rows[i]["UNIQUEID"] = accountno;
                                    dt.Rows[i]["ORD"] = dtAceptAccount.Columns.Contains("ORD") ? rows[0]["ORD"].ToString() : "9";
                                }
                            }
                        }

                        ds.Tables.RemoveAt(0);
                        ds.Tables.Add(dt);
                        tran.Data[Common.KEYNAME.DATARESULT] = ds;

                    }
                }
                if (!tran.Data.ContainsKey("LISTWLACCT"))
                {
                    tran.Data.Add("LISTWLACCT", dtAceptAccount.Select("AcctType like 'WL%'", "AcctType"));
                }
                else
                {
                    tran.Data["LISTWLACCT"] = dtAceptAccount.Select("AcctType like 'WL%'", "AcctType");
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }


        public bool GetTransactionFee(TransactionInfo tran, string parmList)
        {
            Connection con = new Connection();
            TransactionInfo acctInfoTran = new TransactionInfo();

            try
            {
                string[] parmlists = parmList.Split('|');
                string TranCode = parmlists[0];
                string Currency = parmlists[1];
                int debitBranchID;

                if (tran.Data["DEBITBRACHID"] == null)
                {
                    acctInfoTran.Data.Add(Common.KEYNAME.IPCTRANCODE, tran.Data[Common.KEYNAME.IPCTRANCODE]);
                    acctInfoTran.Data.Add(Common.KEYNAME.ACCTNO, tran.Data[Common.KEYNAME.RECEIVERACCOUNT]);
                    acctInfoTran.Data.Add(Common.KEYNAME.SOURCEID, tran.Data[Common.KEYNAME.SOURCEID]);
                    acctInfoTran.Data.Add(Common.KEYNAME.DESTID, tran.Data[Common.KEYNAME.DESTID]);
                    acctInfoTran.Data.Add(Common.KEYNAME.REVERSAL, "N");
                    AutoRouter(acctInfoTran, TranCode);

                    debitBranchID = int.Parse(acctInfoTran.Data["chinhanh"].ToString());
                }
                else
                {
                    debitBranchID = int.Parse(tran.Data["DEBITBRACHID"].ToString());
                }

                DataTable dtFee = con.FillDataTable(Common.ConStr, "IPC_CALCULATORTRANSFEE",
                    tran.Data[Common.KEYNAME.USERID].ToString(), tran.Data[Common.KEYNAME.IPCTRANCODE],
                    tran.Data[Common.KEYNAME.AMOUNT], tran.Data[Common.KEYNAME.ACCTNO], debitBranchID, Currency, "");
                if (tran.Data.ContainsKey(Common.KEYNAME.FEE))
                {
                    tran.Data[Common.KEYNAME.FEE] = dtFee.Rows[0]["FEEAMOUNT"].ToString();
                }
                else
                {
                    tran.Data.Add(Common.KEYNAME.FEE, dtFee.Rows[0]["FEEAMOUNT"].ToString());
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                acctInfoTran = null;
                con = null;
            }

            return true;
        }

        public static bool SendMail(TransactionInfo tran)
        {
            try
            {
                if (tran.Status == Common.TRANSTATUS.FINISH)
                {
                    Connection con = new Connection();
                    DataTable userInfo = con.FillDataTable(Common.ConStr, "SEMS_EBA_USERS_GETUBID",
                        tran.Data[Common.KEYNAME.USERID]);
                    string email = userInfo.Rows[0][Common.KEYNAME.EMAIL].ToString();
                    Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();

                    tmpl = Common.GetEmailTemplate("TransactionSuccessful");

                    tmpl.SetAttribute("senderAccount",
                        tran.Data.ContainsKey(Common.KEYNAME.ACCTNO) ? tran.Data[Common.KEYNAME.ACCTNO].ToString() : "");

                    tmpl.SetAttribute("senderBalance",
                        tran.Data.ContainsKey("REFGLDEBIT") ? tran.Data["REFGLDEBIT"].ToString() : "");

                    tmpl.SetAttribute("ccyid",
                        tran.Data.ContainsKey("currencyid") ? tran.Data["currencyid"].ToString() : "");

                    tmpl.SetAttribute("senderName", userInfo.Rows[0]["FullName"].ToString());

                    tmpl.SetAttribute("recieverAccount",
                        tran.Data.ContainsKey("RECEIVERACCOUNT") ? tran.Data["RECEIVERACCOUNT"].ToString() : "");

                    tmpl.SetAttribute("amount", tran.Data.ContainsKey("AMOUNT") ? tran.Data["AMOUNT"].ToString() : "");

                    tmpl.SetAttribute("amountchu", Utility.Common.AmountToWords(tran.Data["AMOUNT"].ToString()));

                    tmpl.SetAttribute("feeAmount",
                        tran.Data.ContainsKey("feeSenderAmt") ? tran.Data["feeSenderAmt"].ToString() : "");

                    tmpl.SetAttribute("feeType", tran.Data["feeReceiverAmt"] == "0" ? "Receiver" : "Sender");


                    tmpl.SetAttribute("desc", tran.Data.ContainsKey("TRANDESC") ? tran.Data["TRANDESC"].ToString() : "");

                    tmpl.SetAttribute("tranID",
                        tran.Data.ContainsKey("IPCTRANSID") ? tran.Data["IPCTRANSID"].ToString() : "");

                    tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                    //lay branch nguoi gui
                    try
                    {
                        DataTable dtSenderBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS",
                        tran.Data["chinhanh"].ToString());
                        tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                    }
                    catch
                    {
                        tmpl.SetAttribute("senderBranch", "");
                    }


                    try
                    {
                        string creditBranch;
                        //lay ten nguoi nhan
                        DataTable revinfo = con.FillDataTable(Common.ConStr, "MB_GETINFO_BYACCNO", tran.Data["RECEIVERACCOUNT"].ToString());
                        if (revinfo.Columns.Contains("FullName") && revinfo.Rows.Count > 0)
                        {
                            tmpl.SetAttribute("recieverName", revinfo.Rows[0]["FullName"].ToString());
                            creditBranch = revinfo.Rows[0]["BranchID"].ToString();
                        }
                        else
                        {
                            tmpl.SetAttribute("recieverName", tran.Data.ContainsKey("RECEIVERNAME") ? tran.Data["RECEIVERNAME"].ToString() : "");
                            creditBranch = tran.Data.ContainsKey("CREDITBRACHID") ? tran.Data["CREDITBRACHID"].ToString() : "";
                        }

                        //lay branch nguoi nhan
                        DataTable dtReceiverBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS", creditBranch);
                        if (dtReceiverBranch.Columns.Contains("BranchName") && dtReceiverBranch.Rows.Count > 0)
                            tmpl.SetAttribute("receiverBranch", dtReceiverBranch.Rows[0]["BranchName"].ToString());
                        else
                            tmpl.SetAttribute("receiverBranch", "");
                    }
                    catch { }


                    Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email,
                        ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

                    Utility.ProcessLog.LogInformation("======>  Send mail MB transfer success" + email);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                Utility.ProcessLog.LogInformation("======>  Send mail MB transfer fail");
                return true;
            }
        }
        //public static bool SendBatchTransactionMail(TransactionInfo tran)
        //{
        //    try
        //    {
        //        if (tran.Status == Common.TRANSTATUS.FINISH)
        //        {
        //            Connection con = new Connection();
        //            DataTable userInfo = con.FillDataTable(Common.ConStr, "SEMS_EBA_USERS_GETUBID_BATCHTRANSFER",
        //                tran.Data[Common.KEYNAME.USERID]);
        //            string email = userInfo.Rows[0][Common.KEYNAME.EMAIL].ToString();
        //            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();

        //            tmpl = Common.GetEmailTemplate("BatchTransactionSuccessful");

        //            string content = string.Empty;
        //            DataTable dttmp = (DataTable)tran.Data[Common.KEYNAME.BATCHRESULT];
        //            DataView dv = dttmp.DefaultView;
        //            dv.Sort = "TRANREF";
        //            DataTable dt = dv.ToTable();

        //            StringBuilder st = new StringBuilder();

        //            #region lay thong tin nguoi gui
        //            try
        //            {
        //                string sedaccount = "";
        //                string sedname = "";
        //                string debitBranch = "";
        //                string sedbranch = "";

        //                if (dt.Rows.Count > 0)
        //                {
        //                    sedaccount = dt.Rows[0][Common.KEYNAME.FACCTNO].ToString();

        //                    //lay ten nguoi nhan
        //                    DataTable sedinfo = con.FillDataTable(Common.ConStr, "IB_GETCUSTINFOBYACCNO", sedaccount);
        //                    if (sedinfo.Columns.Contains("FULLNAME") && sedinfo.Rows.Count > 0)
        //                    {
        //                        sedname = sedinfo.Rows[0]["FULLNAME"].ToString();
        //                        debitBranch = sedinfo.Rows[0]["BRANCHID"].ToString();
        //                    }

        //                    //lay branch nguoi nhan
        //                    DataTable dtReceiverBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS", debitBranch);
        //                    if (dtReceiverBranch.Columns.Contains("BranchName") && dtReceiverBranch.Rows.Count > 0)
        //                        sedbranch = dtReceiverBranch.Rows[0]["BranchName"].ToString();
        //                }

        //                tmpl.SetAttribute("senderName", sedname);
        //                tmpl.SetAttribute("senderAccount", sedaccount);
        //                tmpl.SetAttribute("senderBranch", sedbranch);
        //            }
        //            catch { }
        //            #endregion



        //            st.Append("<table style='width:100%;' border='2'>");

        //            #region First row
        //            st.Append("<tr>");
        //            st.Append("<td>");
        //            st.Append("<b>"+Common.KEYNAME.TRANSACTION_NO+"</b>");
        //            st.Append("</td>");
        //            st.Append("<td>");
        //            st.Append("<b>"+Common.KEYNAME.SENDER_ACCOUNT+"</b>");
        //            st.Append("</td>");
        //            st.Append("<td>");
        //            st.Append("<b>"+Common.KEYNAME.RECEIVER_ACCOUNT+"</b>");
        //            st.Append("</td>");
        //            st.Append("<td>");
        //            st.Append("<b>"+Common.KEYNAME.RECEIVER_NAME+"</b>");
        //            st.Append("</td>");
        //            st.Append("<td>");
        //            st.Append("<b>" + Common.KEYNAME.RECEIVER_BRANCH + "</b>");
        //            st.Append("</td>");
        //            st.Append("<td>");
        //            st.Append("<b>"+Common.KEYNAME.TRANSACTION_AMOUNT+"</b>");
        //            st.Append("</td>");
        //            st.Append("<td>");
        //            st.Append("<b>"+Common.KEYNAME.PAYMENT_CONTENT+"</b>");
        //            st.Append("</td>");
        //            st.Append("<td>");
        //            st.Append("<b>"+Common.KEYNAME.ACCOUNT_BALANCE+"</b>");
        //            st.Append("</td>");
        //            st.Append("<td>");
        //            st.Append("<b>"+Common.KEYNAME.RESULT+"</b>");
        //            st.Append("</td>");
        //            st.Append("</tr>");
        //            #endregion

        //            #region entry row
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                string tranref = dr[Common.KEYNAME.TRANREF].ToString();
        //                string sedaccount = dr[Common.KEYNAME.FACCTNO].ToString();
        //                string revaccount = dr[Common.KEYNAME.TACCTNO].ToString();
        //                string revname = string.Empty;
        //                string revbranch = string.Empty;
        //                string amount = dr[Common.KEYNAME.AMOUNT].ToString();
        //                string desc = dr[Common.KEYNAME.TRANDESC].ToString();
        //                string accbalance = dr[Common.KEYNAME.DEBITBALANCE].ToString();
        //                string errorcode = dr[Common.KEYNAME.ERRORCODE].ToString();
        //                string result = (string.IsNullOrEmpty(errorcode) || errorcode.Equals("null") || !errorcode.Equals("0")) ? "Fail" : "Success";


        //                #region lay thong tin nguoi nhan
        //                try
        //                {
        //                    string creditBranch="";
        //                    //lay ten nguoi nhan
        //                    DataTable revinfo = con.FillDataTable(Common.ConStr, "IPCLOGTRANSDETAIL_SELECT",Common.IPCWorkDate ,tranref,"I");
        //                    if (revinfo.Rows.Count > 0)
        //                    {
        //                        revname = revinfo.Select("FIELDNAME = 'RECEIVERNAME'")[0][2].ToString();
        //                        creditBranch = revinfo.Select("FIELDNAME = 'CREDITBRACHID'")[0][2].ToString();
        //                    }

        //                    //lay branch nguoi nhan
        //                    DataTable dtReceiverBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS", creditBranch);
        //                    if (dtReceiverBranch.Columns.Contains("BranchName") && dtReceiverBranch.Rows.Count > 0)
        //                        revbranch = dtReceiverBranch.Rows[0]["BranchName"].ToString();

        //                }
        //                catch { }
        //                #endregion

        //                st.Append("<tr>");
        //                st.Append("<td>");
        //                st.Append(tranref);
        //                st.Append("</td>");
        //                st.Append("<td>");
        //                st.Append(sedaccount);
        //                st.Append("</td>");
        //                st.Append("<td>");
        //                st.Append(revaccount);
        //                st.Append("</td>");
        //                st.Append("<td>");
        //                st.Append(revname);
        //                st.Append("</td>");
        //                st.Append("<td>");
        //                st.Append(revbranch);
        //                st.Append("</td>");
        //                st.Append("<td>");
        //                st.Append(amount);
        //                st.Append("</td>");
        //                st.Append("<td>");
        //                st.Append(desc);
        //                st.Append("</td>");
        //                st.Append("<td>");
        //                st.Append(accbalance);
        //                st.Append("</td>");
        //                st.Append("<td>");
        //                st.Append(result);
        //                st.Append("</td>");
        //                st.Append("</tr>");
        //            }
        //            #endregion

        //            st.Append("</table>");

        //            tmpl.SetAttribute("PAYMENTINFO", st.ToString());
        //            tmpl.SetAttribute("tranID",tran.IPCTransID.ToString());
        //            tmpl.SetAttribute("batchID",
        //                tran.Data.ContainsKey("IPCTRANSID") ? tran.Data["BATCHID"].ToString() : "");

        //            tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        //            //lay thong tin giao dich
        //            string TotalAmount="";
        //            string TotalFee="";
        //            string TextAmount="";
        //            string CCYID = "";
        //            try
        //            {
        //                DataTable tranlog = con.FillDataTable(Common.ConStr, "IPC_BATCHSELECTTRANS", tran.Data[Common.KEYNAME.BATCHID]);

        //                if(tranlog.Rows.Count > 0)
        //                {
        //                    TotalAmount= tranlog.Compute("Sum(AMOUNT)","").ToString();
        //                    TotalFee =  tranlog.Compute("Sum(FEE)", "").ToString();
        //                    CCYID = tranlog.Rows[0]["CCYID"].ToString();
        //                    TextAmount = Utility.Common.AmountToWords(TotalAmount) + " " + CCYID;
        //                }
        //            }
        //            catch { }

        //            tmpl.SetAttribute("TotalAmount", TotalAmount);
        //            tmpl.SetAttribute("TotalFee", TotalFee);
        //            tmpl.SetAttribute("TextAmount", TextAmount);

        //            Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email,
        //                ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

        //            Utility.ProcessLog.LogInformation("======>  Send batch transfer success" + email + tran.IPCTransID.ToString());
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //tran.SetErrorInfo(ex);
        //        Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
        //            System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        Utility.ProcessLog.LogInformation("======>  Send mail MB transfer fail" + tran.IPCTransID.ToString());
        //        return true;
        //    }
        //}
        public static bool SendBatchTransactionMail(TransactionInfo tran)
        {
            try
            {
                //Utility.ProcessLog.LogInformation("batch scheduleid===" + (tran.Data.ContainsKey(Common.KEYNAME.SCHEDULEID) ? tran.Data[Common.KEYNAME.SCHEDULEID].ToString() : "ko co scheduleid"));
                //Utility.ProcessLog.LogInformation("batch IPCTRANID===" + (tran.Data.ContainsKey(Common.KEYNAME.IPCTRANSID) ? tran.Data[Common.KEYNAME.IPCTRANSID].ToString() : "ko co scheduleid"));
                //Utility.ProcessLog.LogInformation("userid===" + (tran.Data.ContainsKey(Common.KEYNAME.USERID) ? tran.Data[Common.KEYNAME.USERID].ToString() : "ko co scheduleid"));
                //Utility.ProcessLog.LogInformation("new update for send mail batch");
                //get final transaction ib000498
                Connection con = new Connection();
                DataTable dtschedule = con.FillDataTable(Common.ConStr, "IPC_GET_BATCH_SCHEDULE_INFO", tran.Data[Common.KEYNAME.SCHEDULEID].ToString(),
                        tran.Data[Common.KEYNAME.BATCHID].ToString(), tran.Data[Common.KEYNAME.USERID]);
                if (dtschedule.Rows.Count <= 0)
                {
                    Utility.ProcessLog.LogInformation("Get schedule batch transfer to send mail error");
                    return false;
                }
                else
                {
                    //Utility.ProcessLog.LogInformation("status of schedule is====" + dtschedule.Rows[0][Common.KEYNAME.STATUS].ToString().Trim());
                    //if (tran.Status == Common.TRANSTATUS.FINISH)
                    bool sendwhenerror = false;
                    if (bool.Parse(ConfigurationManager.AppSettings["sendmailwhenbatcherror"]))
                    {
                        if (tran.Status == Common.TRANSTATUS.ERROR || tran.Status == Common.TRANSTATUS.FINISH)
                        {
                            sendwhenerror = true;
                        }
                    }
                    else
                    {

                        if (tran.Status == Common.TRANSTATUS.FINISH)
                        {
                            sendwhenerror = true;
                        }
                        else sendwhenerror = false;
                    }

                    if (dtschedule.Rows[0][Common.KEYNAME.STATUS].ToString().Trim() == Common.KEYNAME.YES && sendwhenerror)
                    {
                        //Utility.ProcessLog.LogInformation("==================== start send batch mail");
                        //Connection con = new Connection();
                        //Duyvk 20190805 edit send mail for contract type MTR
                        //DataTable userInfo = con.FillDataTable(Common.ConStr, "SEMS_EBA_USERS_GETUBID_BATCHTRANSFER",
                        //    tran.Data[Common.KEYNAME.USERID]);
                        DataTable userInfo = con.FillDataTable(Common.ConStr, "SEMS_EBA_USERS_GETUBID_BATCHTRANSFER_V2",
                             tran.Data[Common.KEYNAME.USERID], tran.Data[Common.KEYNAME.BATCHID].ToString());
                        string email = userInfo.Rows[0][Common.KEYNAME.EMAIL].ToString();
                        Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();

                        tmpl = Common.GetEmailTemplate("BatchTransactionSuccessful");

                        string content = string.Empty;
                        DataTable dttmp = (DataTable)tran.Data[Common.KEYNAME.BATCHRESULT];
                        DataView dv = dttmp.DefaultView;
                        dv.Sort = "TRANREF";
                        DataTable dt = dv.ToTable();

                        StringBuilder st = new StringBuilder();

                        #region lay thong tin nguoi gui
                        try
                        {
                            string sedaccount = "";
                            string sedname = "";
                            string debitBranch = "";
                            string sedbranch = "";

                            if (dt.Rows.Count > 0)
                            {
                                sedaccount = dt.Rows[0][Common.KEYNAME.FACCTNO].ToString();

                                //lay ten nguoi nhan
                                DataTable sedinfo = con.FillDataTable(Common.ConStr, "IB_GETCUSTINFOBYACCNO", sedaccount);
                                if (sedinfo.Columns.Contains("FULLNAME") && sedinfo.Rows.Count > 0)
                                {
                                    sedname = sedinfo.Rows[0]["FULLNAME"].ToString();
                                    debitBranch = sedinfo.Rows[0]["BRANCHID"].ToString();
                                }

                                //lay branch nguoi nhan
                                DataTable dtReceiverBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS", debitBranch);
                                if (dtReceiverBranch.Columns.Contains("BranchName") && dtReceiverBranch.Rows.Count > 0)
                                    sedbranch = dtReceiverBranch.Rows[0]["BranchName"].ToString();
                            }

                            tmpl.SetAttribute("senderName", sedname);
                            tmpl.SetAttribute("senderAccount", sedaccount);
                            tmpl.SetAttribute("senderBranch", sedbranch);
                        }
                        catch { }
                        #endregion



                        st.Append("<table style='width:100%;' border='2'>");

                        #region First row
                        st.Append("<tr>");
                        st.Append("<td>");
                        st.Append("<b>" + Common.KEYNAME.TRANSACTION_NO + "</b>");
                        st.Append("</td>");
                        st.Append("<td>");
                        st.Append("<b>" + Common.KEYNAME.SENDER_ACCOUNT + "</b>");
                        st.Append("</td>");
                        st.Append("<td>");
                        st.Append("<b>" + Common.KEYNAME.RECEIVER_ACCOUNT + "</b>");
                        st.Append("</td>");
                        st.Append("<td>");
                        st.Append("<b>" + Common.KEYNAME.RECEIVER_NAME + "</b>");
                        st.Append("</td>");
                        st.Append("<td>");
                        st.Append("<b>" + Common.KEYNAME.RECEIVER_BRANCH + "</b>");
                        st.Append("</td>");
                        st.Append("<td>");
                        st.Append("<b>" + Common.KEYNAME.TRANSACTION_AMOUNT + "</b>");
                        st.Append("</td>");
                        st.Append("<td>");
                        st.Append("<b>" + Common.KEYNAME.PAYMENT_CONTENT + "</b>");
                        st.Append("</td>");
                        //st.Append("<td>");
                        //st.Append("<b>" + Common.KEYNAME.ACCOUNT_BALANCE + "</b>");
                        //st.Append("</td>");
                        st.Append("<td>");
                        st.Append("<b>" + Common.KEYNAME.RESULT + "</b>");
                        st.Append("</td>");
                        st.Append("</tr>");
                        #endregion

                        #region entry row
                        foreach (DataRow dr in dt.Rows)
                        {
                            string tranref = dr[Common.KEYNAME.TRANREF].ToString();
                            string sedaccount = dr[Common.KEYNAME.FACCTNO].ToString();
                            string revaccount = dr[Common.KEYNAME.TACCTNO].ToString();
                            string revname = string.Empty;
                            string revbranch = string.Empty;
                            string amount = dr[Common.KEYNAME.AMOUNT].ToString();
                            string desc = dr[Common.KEYNAME.TRANDESC].ToString();
                            string errorcode = dr[Common.KEYNAME.ERRORCODE].ToString().Equals("00") ? Common.ERRORCODE.OK : dr[Common.KEYNAME.ERRORCODE].ToString();
                            string result = (string.IsNullOrEmpty(errorcode) || errorcode.Equals("null") || !errorcode.Equals("0")) ? "Fail" : "Success";


                            #region lay thong tin nguoi nhan
                            try
                            {
                                string creditBranch = "";
                                //lay ten nguoi nhan
                                DataTable revinfo = con.FillDataTable(Common.ConStr, "IPCLOGTRANSDETAIL_SELECT", Common.IPCWorkDate, tranref, "I");
                                if (revinfo.Rows.Count > 0)
                                {
                                    revname = revinfo.Select("FIELDNAME = 'RECEIVERNAME'")[0][2].ToString();
                                    creditBranch = revinfo.Select("FIELDNAME = 'CREDITBRACHID'")[0][2].ToString();
                                }

                                //lay branch nguoi nhan
                                DataTable dtReceiverBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS", creditBranch);
                                if (dtReceiverBranch.Columns.Contains("BranchName") && dtReceiverBranch.Rows.Count > 0)
                                    revbranch = dtReceiverBranch.Rows[0]["BranchName"].ToString();

                            }
                            catch { }
                            #endregion

                            st.Append("<tr>");
                            st.Append("<td>");
                            st.Append(tranref);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(sedaccount);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(revaccount);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(revname);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(revbranch);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(amount);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(desc);
                            st.Append("</td>");
                            //st.Append("<td>");
                            //st.Append(accbalance);
                            //st.Append("</td>");
                            st.Append("<td>");
                            st.Append(result);
                            st.Append("</td>");
                            st.Append("</tr>");
                        }
                        #endregion

                        st.Append("</table>");

                        tmpl.SetAttribute("PAYMENTINFO", st.ToString());
                        tmpl.SetAttribute("tranID", tran.IPCTransID.ToString());
                        tmpl.SetAttribute("batchID",
                            tran.Data.ContainsKey("IPCTRANSID") ? tran.Data["BATCHID"].ToString() : "");

                        tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                        //lay thong tin giao dich
                        string TotalAmount = "";
                        string TotalFee = "";
                        string TextAmount = "";
                        string CCYID = "";
                        try
                        {
                            DataTable tranlog = con.FillDataTable(Common.ConStr, "IPC_BATCHSELECTTRANS", tran.Data[Common.KEYNAME.BATCHID]);

                            if (tranlog.Rows.Count > 0)
                            {
                                TotalAmount = tranlog.Compute("Sum(AMOUNT)", "").ToString();
                                TotalFee = tranlog.Compute("Sum(FEE)", "").ToString();
                                CCYID = tranlog.Rows[0]["CCYID"].ToString();
                                TextAmount = Utility.Common.AmountToWords(TotalAmount) + " " + CCYID;
                            }
                        }
                        catch { }

                        tmpl.SetAttribute("TotalAmount", TotalAmount);
                        tmpl.SetAttribute("TotalFee", TotalFee);
                        tmpl.SetAttribute("TextAmount", TextAmount);

                        Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email,
                            "APPROVAL CONFIRMATION", tmpl.ToString());

                        Utility.ProcessLog.LogInformation("======>  Send batch transfer success " + email + tran.IPCTransID.ToString());
                        return true;
                    }
                    else
                    {
                        //Utility.ProcessLog.LogInformation("==================== dont send mail batch mail");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                //tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                Utility.ProcessLog.LogInformation("======>  Send mail Batch transfer fail" + tran.IPCTransID.ToString());
                return true;
            }
        }

        public static bool SendTopupMail(TransactionInfo tran)
        {
            try
            {
                if (tran.Status == Common.TRANSTATUS.FINISH)
                {
                    Connection con = new Connection();
                    DataTable userInfo = con.FillDataTable(Common.ConStr, "SEMS_EBA_USERS_GETUBID",
                        tran.Data[Common.KEYNAME.USERID]);
                    string email = userInfo.Rows[0][Common.KEYNAME.EMAIL].ToString();
                    if (string.IsNullOrEmpty(email))
                    {
                        Utility.ProcessLog.LogInformation("======>  Can not Send to empty mail MB topup with ipctransid=" + tran.IPCTransID.ToString() + "/User=" + tran.Data[Common.KEYNAME.USERID].ToString() + "/");
                        return false;
                    }

                    Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();

                    tmpl = Common.GetEmailTemplate("TopupTransactionSuccessful");

                    tmpl.SetAttribute("senderName", userInfo.Rows[0]["FullName"].ToString());

                    tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                    tmpl.SetAttribute("senderAccount",
                        tran.Data.ContainsKey(Common.KEYNAME.ACCTNO) ? tran.Data[Common.KEYNAME.ACCTNO].ToString() : "");

                    tmpl.SetAttribute("senderBalance",
                        tran.Data.ContainsKey("REFGLDEBIT") ? tran.Data["REFGLDEBIT"].ToString() : "");

                    tmpl.SetAttribute("ccyid",
                        tran.Data.ContainsKey("CCYID") ? tran.Data["CCYID"].ToString() : "");

                    tmpl.SetAttribute("telcoName",
                        tran.Data.ContainsKey("RECEIVERNAME") ? tran.Data["RECEIVERNAME"].ToString() : "");

                    tmpl.SetAttribute("amount", tran.Data.ContainsKey("AMOUNT") ? tran.Data["AMOUNT"].ToString() : "");

                    //tmpl.SetAttribute("amountchu", tran.Data["amountchu"].ToString());
                    tmpl.SetAttribute("amountchu", Utility.Common.AmountToWords(tran.Data["AMOUNT"].ToString()) + " " + tran.Data["CCYID"].ToString());

                    tmpl.SetAttribute("feeType", tran.Data["feeReceiverAmt"] == "0" ? "Sender" : "Receiver");

                    tmpl.SetAttribute("feeAmount",
                        tran.Data.ContainsKey("feeSenderAmt") ? tran.Data["feeSenderAmt"].ToString() : "");

                    tmpl.SetAttribute("desc", tran.Data.ContainsKey("TRANDESC") ? tran.Data["TRANDESC"].ToString() : "");

                    tmpl.SetAttribute("tranID",
                        tran.Data.ContainsKey("IPCTRANSID") ? tran.Data["IPCTRANSID"].ToString() : "");

                    tmpl.SetAttribute("tranDate",
                        tran.Data.ContainsKey("lasttransdate") ? tran.Data["lasttransdate"].ToString() : "");

                    tmpl.SetAttribute("softpin", tran.Data.ContainsKey("SOFTPIN") ? tran.Data["SOFTPIN"] : "");

                    Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email,
                        ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

                    Utility.ProcessLog.LogInformation("======>  Send mail MB topup success ipctransid=" + tran.IPCTransID.ToString() + "/userid=" + tran.Data[Common.KEYNAME.USERID].ToString() + "/Email=" + email);


                    return true;
                }
                else
                {
                    Utility.ProcessLog.LogInformation("======>  Send mail MB topup fail with transtatus=" + tran.Status + "/User=" + tran.Data[Common.KEYNAME.USERID].ToString() + "/ipctransid=" + tran.IPCTransID.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                //tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name + " Exception");
                Utility.ProcessLog.LogInformation("======>  Send mail MB topup fail");
                return true;
            }
        }

        public bool SendBillPaymentMail(TransactionInfo tran)
        {
            try
            {
                if (tran.Status == Common.TRANSTATUS.FINISH)
                {
                    Connection con = new Connection();
                    DataTable userInfo = con.FillDataTable(Common.ConStr, "SEMS_EBA_USERS_GETUBID",
                        tran.Data[Common.KEYNAME.USERID]);
                    string email = userInfo.Rows[0][Common.KEYNAME.EMAIL].ToString();
                    Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();

                    tmpl = Common.GetEmailTemplate("BillPaymentSuccessful");

                    tmpl.SetAttribute("senderName", userInfo.Rows[0]["FullName"].ToString());

                    tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                    tmpl.SetAttribute("senderAccount",
                        tran.Data.ContainsKey(Common.KEYNAME.ACCTNO) ? tran.Data[Common.KEYNAME.ACCTNO].ToString() : "");

                    tmpl.SetAttribute("senderBalance",
                        tran.Data.ContainsKey("BALANCE") ?
                        double.Parse(tran.Data["BALANCE"].ToString()).ToString("N02").ToString()
                         : "");

                    tmpl.SetAttribute("ccyid",
                        tran.Data.ContainsKey("CCYID") ? tran.Data["CCYID"].ToString() : "MMK");

                    if (tran.Data.ContainsKey("CORPNAME"))
                    {
                        tmpl.SetAttribute("corpName", tran.Data["CORPNAME"].ToString());
                    }
                    else
                    {
                        string corpname = tran.Data["CORPID"].ToString();
                        #region get corp name
                        TransactionInfo trancorp = new TransactionInfo();
                        trancorp.Data.Add(Common.KEYNAME.IPCTRANCODE, "MB000024");
                        trancorp.Data.Add(Common.KEYNAME.SOURCEID, "MB");
                        trancorp.Data.Add(Common.KEYNAME.DESTID, "PNB");
                        trancorp.Data.Add(Common.KEYNAME.REVERSAL, "N");
                        AutoRouter(trancorp, "HUB0001");

                        if (trancorp.ErrorCode.Equals("0"))
                        {
                            try
                            {
                                DataTable dt = ((DataSet)trancorp.Data["DATARESULT"]).Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    DataRow[] dr = dt.Select("CORPID = '" + tran.Data["CORPID"].ToString() + "'");
                                    if (dr.Count() > 0)
                                    {
                                        corpname = dr[0]["CORPNAME"].ToString();
                                    }
                                }
                            }
                            catch { }
                        }
                        #endregion
                        tmpl.SetAttribute("corpName", corpname);
                    }
                    if (tran.Data.ContainsKey("SERNAME"))
                    {
                        tmpl.SetAttribute("serviceName", tran.Data["SERNAME"].ToString());
                    }
                    else
                    {
                        string servicename = tran.Data["SERVICEID"].ToString();
                        #region get service name
                        TransactionInfo trancorp = new TransactionInfo();
                        trancorp.Data.Add(Common.KEYNAME.IPCTRANCODE, "MB000025");
                        trancorp.Data.Add(Common.KEYNAME.SOURCEID, "MB");
                        trancorp.Data.Add(Common.KEYNAME.DESTID, "PNB");
                        trancorp.Data.Add(Common.KEYNAME.REVERSAL, "N");
                        trancorp.Data.Add("CORPID", tran.Data["CORPID"].ToString());
                        AutoRouter(trancorp, "HUB0002");

                        if (trancorp.ErrorCode.Equals("0"))
                        {
                            try
                            {
                                DataTable dt = ((DataSet)trancorp.Data["DATARESULT"]).Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    DataRow[] dr = dt.Select("SERID = '" + tran.Data["SERVICEID"].ToString() + "'");
                                    if (dr.Count() > 0)
                                    {
                                        servicename = dr[0]["SERNAME"].ToString();
                                    }
                                }
                            }
                            catch { }
                        }
                        #endregion
                        tmpl.SetAttribute("serviceName", servicename);
                    }
                    if (tran.Data.ContainsKey("REFNAME1") && tran.Data.ContainsKey("REFNAME2"))
                    {
                        tmpl.SetAttribute("refindex1", tran.Data["REFNAME1"].ToString());
                        tmpl.SetAttribute("refindex2", tran.Data["REFNAME2"].ToString());
                    }
                    else
                    {
                        string refname1 = "Reference number 1";
                        string refname2 = "Reference number 2";
                        #region get service infor
                        TransactionInfo trancorp = new TransactionInfo();
                        trancorp.Data.Add(Common.KEYNAME.IPCTRANCODE, "MB000026");
                        trancorp.Data.Add(Common.KEYNAME.SOURCEID, "MB");
                        trancorp.Data.Add(Common.KEYNAME.DESTID, "PNB");
                        trancorp.Data.Add(Common.KEYNAME.REVERSAL, "N");
                        trancorp.Data.Add("CORPID", tran.Data["CORPID"].ToString());
                        trancorp.Data.Add("SERVICEID", tran.Data["SERVICEID"].ToString());
                        AutoRouter(trancorp, "HUB0003");

                        if (trancorp.ErrorCode.Equals("0"))
                        {
                            try
                            {
                                DataTable dt = ((DataSet)trancorp.Data["DATARESULT"]).Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    refname1 = dt.Rows[0]["REFNAME1"].ToString();
                                    refname2 = dt.Rows[0]["REFNAME2"].ToString();
                                }
                            }
                            catch { }
                        }
                        #endregion
                        tmpl.SetAttribute("refindex1", refname1);
                        tmpl.SetAttribute("refindex2", refname2);
                    }

                    tmpl.SetAttribute("refvalue1", tran.Data.ContainsKey("REFVA1") ? tran.Data["REFVA1"].ToString() : "");
                    tmpl.SetAttribute("refvalue2", tran.Data.ContainsKey("REFVA2") ? tran.Data["REFVA2"].ToString() : "");


                    tmpl.SetAttribute("amount", tran.Data.ContainsKey("AMOUNT") ? tran.Data["AMOUNT"].ToString() : "");

                    //tmpl.SetAttribute("amountchu", tran.Data["amountchu"].ToString());
                    tmpl.SetAttribute("amountchu", Utility.Common.AmountToWords(tran.Data["AMOUNT"].ToString()));

                    tmpl.SetAttribute("feeType", "Sender");

                    tmpl.SetAttribute("feeAmount",
                        tran.Data.ContainsKey("FEEDR") ? tran.Data["FEEDR"].ToString() : "");

                    tmpl.SetAttribute("desc", tran.Data.ContainsKey("TRANDESC") ? tran.Data["TRANDESC"].ToString() : "");

                    tmpl.SetAttribute("tranID",
                        tran.Data.ContainsKey("IPCTRANSID") ? tran.Data["IPCTRANSID"].ToString() : "");

                    tmpl.SetAttribute("tranDate",
                        tran.Data.ContainsKey("lasttransdate") ? tran.Data["lasttransdate"].ToString() : "");

                    Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email,
                        ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

                    Utility.ProcessLog.LogInformation("======>  Send bill payment email success" + email);


                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                Utility.ProcessLog.LogInformation("======>  Send bill payment email fail");
                return true;
            }
        }
        public static bool SendMailSchedule(TransactionInfo tran)
        {
            try
            {
                if (tran.Data.ContainsKey("SCHEDULEID"))
                {

                    //if (tran.Status == Common.TRANSTATUS.FINISH)
                    //{

                    Connection con = new Connection();
                    DataTable dtschedule = con.FillDataTable(Common.ConStr, "EBA_SCHEDULES_SELECTBYID", tran.Data["SCHEDULEID"]);
                    DataRow drschedule = dtschedule.NewRow();
                    drschedule = dtschedule.Rows[0];
                    if ((drschedule["STATUS"].ToString().Equals("A") || drschedule["STATUS"].ToString().Equals("Y")) && drschedule["ISAPPROVED"].ToString().Equals(Common.KEYNAME.YES))
                    {
                        DataTable dtscheduleprocess = con.FillDataTable(Common.ConStr, "EBA_SCHEDULEPROCESSGETBYID", tran.Data["SCHEDULEID"]);
                        string scheduleprocess = "";
                        DataTable userInfo = con.FillDataTable(Common.ConStr, "SEMS_EBA_USERS_GETUBID", tran.Data[Common.KEYNAME.USERID]);
                        string email = userInfo.Rows[0][Common.KEYNAME.EMAIL].ToString();
                        Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
                        tmpl = Common.GetEmailTemplate("ScheduleComplete");
                        bool isComplete = true;

                        string type = "";
                        switch (drschedule["SCHEDULETYPE"].ToString())
                        {
                            case Common.KEYNAME.ONETIME:
                                type = "One time";
                                break;
                            case Common.KEYNAME.DAILY:
                                type = "Daily";
                                break;
                            case Common.KEYNAME.WEEKLY:
                                type = "Weekly";
                                break;
                            case Common.KEYNAME.MONTHLY:
                                type = "Monthly";
                                break;
                        }
                        tmpl.SetAttribute("scheduletype", type);
                        tmpl.SetAttribute("transfertype", drschedule["PageName"]);
                        tmpl.SetAttribute("debitaccount", drschedule["ACCTNO"]);
                        tmpl.SetAttribute("creditaccount", drschedule["RECEIVERACCOUNT"]);
                        tmpl.SetAttribute("transferamount", drschedule["AMOUNT"]);
                        tmpl.SetAttribute("tranID", tran.Data.ContainsKey("IPCTRANSID") ? tran.Data["IPCTRANSID"].ToString() : "");
                        tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        tmpl.SetAttribute("desc", drschedule["DESCRIPTION"]);
                        tmpl.SetAttribute("schedulename", drschedule["SCHEDULENAME"]);
                        tmpl.SetAttribute("senderName", drschedule["SENDERNAME"].ToString());
                        tmpl.SetAttribute("recieverName", drschedule["RECEIVERNAME"].ToString());
                        try
                        {
                            DataTable dtSenderBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS",
                            drschedule["DEBITBRACHID"].ToString());
                            tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                        }
                        catch
                        {
                            tmpl.SetAttribute("senderBranch", "");
                        }
                        try
                        {
                            DataTable dtSenderBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS",
                            drschedule["CREDITBRACHID"].ToString());
                            tmpl.SetAttribute("receiverBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                        }
                        catch
                        {
                            tmpl.SetAttribute("receiverBranch", "");
                        }
                        scheduleprocess = "<tr><th style='width:200px; text-align:left'>Process time</th><th style='width:200px; text-align:left'>Amount</th><th style='width:200px; text-align:left'>Fee</th><th style='width:200px; text-align:left'>Status</th></tr>";
                        foreach (DataRow dr in dtscheduleprocess.Rows)
                        {
                            string status = "";
                            //switch (dr["STATUS"].ToString())
                            //{
                            //    case "0":
                            //        status = "Begin";
                            //        break;
                            //    case "1":
                            //        status = "Finish";
                            //        break;
                            //    case "2":
                            //        status = "Error";
                            //        isComplete = false;
                            //        break;
                            //    case "3":
                            //        status = "Waiting approve";
                            //        break;
                            //    case "4":
                            //        status = "Rejected";
                            //        break;
                            //}
                            switch (dr["ERRORCODE"].ToString())
                            {
                                case "0":
                                    status = "Successfull";
                                    break;
                                default:
                                    status = dr["ERRORDESC"].ToString();
                                    isComplete = false;
                                    break;
                            }
                            string fee = dr["NUM02"].ToString() + tran.Data["CCYID"];
                            string date = dr["IPCTRANSDATE"].ToString();
                            DateTime date1 = Convert.ToDateTime(date);
                            date = date1.ToString("dd-MM-yyyy hh:mm:ssss");

                            scheduleprocess += "<tr><td>" + date + "</td><td>" + drschedule["AMOUNT"] + "</td><td>" + fee + "</td><td>" + status + "</td></tr>";

                        }
                        tmpl.SetAttribute("scheduleprocess", scheduleprocess);
                        tmpl.SetAttribute("statusall", isComplete ? "Complete" : "Error");
                        Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

                        Utility.ProcessLog.LogInformation("======>  Send mail schedule success" + email);
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                //tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                Utility.ProcessLog.LogInformation("======>  Send mail schedule after transaction fail");
                return true;
            }
        }
        public static bool SendMailScheduleafterSchedule(TransactionInfo tran)
        {
            try
            {
                if (tran.Data.ContainsKey("SCHEDULEID"))
                {

                    //if (tran.Status == Common.TRANSTATUS.FINISH)
                    //{

                    Connection con = new Connection();
                    DataTable dtschedule = con.FillDataTable(Common.ConStr, "EBA_SCHEDULES_SELECTBYID", tran.Data["SCHEDULEID"]);
                    DataRow drschedule = dtschedule.NewRow();
                    drschedule = dtschedule.Rows[0];
                    if (drschedule["STATUS"].ToString().Equals("Y") && drschedule["ISAPPROVED"].ToString().Equals(Common.KEYNAME.YES))
                    {
                        DataTable dtscheduleprocess = con.FillDataTable(Common.ConStr, "EBA_SCHEDULEPROCESSGETBYID_AFTER_SCHEDULE", tran.Data["SCHEDULEID"]);
                        string scheduleprocess = "";
                        DataTable userInfo = con.FillDataTable(Common.ConStr, "SEMS_EBA_USERS_GETUBID", tran.Data[Common.KEYNAME.USERID]);
                        string email = userInfo.Rows[0][Common.KEYNAME.EMAIL].ToString();
                        Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
                        tmpl = Common.GetEmailTemplate("ScheduleComplete_after_schedule");
                        bool isComplete = true;

                        string type = "";
                        switch (drschedule["SCHEDULETYPE"].ToString())
                        {
                            case Common.KEYNAME.ONETIME:
                                type = "One time";
                                break;
                            case Common.KEYNAME.DAILY:
                                type = "Daily";
                                break;
                            case Common.KEYNAME.WEEKLY:
                                type = "Weekly";
                                break;
                            case Common.KEYNAME.MONTHLY:
                                type = "Monthly";
                                break;
                        }
                        tmpl.SetAttribute("scheduletype", type);
                        tmpl.SetAttribute("transfertype", drschedule["PageName"]);
                        tmpl.SetAttribute("debitaccount", drschedule["ACCTNO"]);
                        tmpl.SetAttribute("creditaccount", drschedule["RECEIVERACCOUNT"]);
                        tmpl.SetAttribute("transferamount", drschedule["AMOUNT"]);
                        //tmpl.SetAttribute("tranID", tran.Data.ContainsKey("IPCTRANSID") ? tran.Data["IPCTRANSID"].ToString() : "");
                        tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        tmpl.SetAttribute("desc", drschedule["DESCRIPTION"]);
                        tmpl.SetAttribute("schedulename", drschedule["SCHEDULENAME"]);
                        tmpl.SetAttribute("senderName", drschedule["SENDERNAME"].ToString());
                        tmpl.SetAttribute("recieverName", drschedule["RECEIVERNAME"].ToString());
                        try
                        {
                            DataTable dtSenderBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS",
                            drschedule["DEBITBRACHID"].ToString());
                            tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                        }
                        catch
                        {
                            tmpl.SetAttribute("senderBranch", "");
                        }
                        try
                        {
                            DataTable dtSenderBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS",
                            drschedule["CREDITBRACHID"].ToString());
                            tmpl.SetAttribute("receiverBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                        }
                        catch
                        {
                            tmpl.SetAttribute("receiverBranch", "");
                        }
                        scheduleprocess = "<tr><th style='width:200px; text-align:left'>Process time</th><th style='width:200px; text-align:left'>Amount</th><th style='width:200px; text-align:left'>Fee</th><th style='width:200px; text-align:left'>Status</th></tr>";
                        foreach (DataRow dr in dtscheduleprocess.Rows)
                        {
                            string status = "";
                            //switch (dr["STATUS"].ToString())
                            //{
                            //    case "0":
                            //        status = "Begin";
                            //        break;
                            //    case "1":
                            //        status = "Finish";
                            //        break;
                            //    case "2":
                            //        status = "Error";
                            //        isComplete = false;
                            //        break;
                            //    case "3":
                            //        status = "Waiting approve";
                            //        break;
                            //    case "4":
                            //        status = "Rejected";
                            //        break;
                            //}
                            switch (dr["ERRORCODE"].ToString())
                            {
                                case "0":
                                    status = "Successfull";
                                    break;
                                default:
                                    status = dr["ERRORDESC"].ToString();
                                    isComplete = false;
                                    break;
                            }
                            string fee = dr["NUM02"].ToString() + tran.Data["CCYID"];
                            string date = dr["IPCTRANSDATE"].ToString();
                            DateTime date1 = Convert.ToDateTime(date);
                            date = date1.ToString("dd-MM-yyyy hh:mm:ssss");

                            scheduleprocess += "<tr><td>" + date + "</td><td>" + drschedule["AMOUNT"] + "</td><td>" + fee + "</td><td>" + status + "</td></tr>";

                        }
                        tmpl.SetAttribute("scheduleprocess", scheduleprocess);
                        //tmpl.SetAttribute("statusall", isComplete ? "Complete" : "Error");
                        Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

                        Utility.ProcessLog.LogInformation("======>  Send mail schedule success" + email);
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                //tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                Utility.ProcessLog.LogInformation("======>  Send mail after schedule fail");
                return true;
            }
        }
        public static bool SendMailMonitorSMS(TransactionInfo tran)
        {
            try
            {
                Utility.ProcessLog.LogInformation("======> starting  Send mail sms monitor ......");
                Utility.ProcessLog.LogInformation("======> mail sms monitor TO");

                String EMAILTO = string.Empty;
                string TITLE = string.Empty;
                string BODY = string.Empty;
                EMAILTO = tran.Data["EMAILTO"].ToString();
                TITLE = tran.Data["TITLE"].ToString();
                BODY = tran.Data["BODY"].ToString();
                Utility.ProcessLog.LogInformation("======> mail sms monitor TO" + EMAILTO + "|| TITLE " + TITLE + "||BODY" + BODY);


                //Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
                Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], EMAILTO, TITLE, BODY);
                return true;
            }
            catch (Exception ex)
            {
                //tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                Utility.ProcessLog.LogInformation("======>  Send mail sms monitor fail");
                return true;
            }

        }
        public bool CheckDestTrans(TransactionInfo tran)
        {
            return CheckDestTrans(tran, "N");
        }
        public bool CheckDestTrans(TransactionInfo tran, string isReverse)
        {
            Connection con = new Connection();
            try
            {
                if (isReverse.Trim().Equals("Y") && (!tran.ErrorCode.Equals("0") || !tran.Data[Common.KEYNAME.ERRORCODE].ToString().Equals("0")))
                {
                    DataTable checktrans = con.FillDataTable(Common.ConStr, "IPC_CHECKDESTTRANS",
                    tran.IPCTransID.ToString(),
                                            tran.Data[Common.KEYNAME.SOURCEID].ToString(),
                                            tran.Data[Common.KEYNAME.DESTID].ToString(),
                                            tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                            tran.Data[Common.KEYNAME.IPCTRANCODEDEST].ToString(),
                                            tran.ErrorCode,
                                            tran.Data.ContainsKey(Common.KEYNAME.DESTERRORCODE) ? tran.Data[Common.KEYNAME.DESTERRORCODE].ToString() : "",
                                            Common.HashTable2String(tran.Data)
                                            );
                    //con.ExecuteNonquery(Common.ConStr, "IPC_CHECKREVERSAL", tran.IPCTransID, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), tran.Data[Common.KEYNAME.ERRORCODE].ToString());
                    //insert reversal tran
                }

                if (tran.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                    tran.ErrorCode = tran.Data[Common.KEYNAME.ERRORCODE].ToString();
                if (tran.Data.ContainsKey(Common.KEYNAME.ERRORDESC))
                    tran.ErrorDesc = tran.Data[Common.KEYNAME.ERRORDESC].ToString();

                if (tran.ErrorCode.Equals("0") || tran.ErrorCode.Equals("00"))
                {
                    return true;
                }
                else
                {
                    return false;
                }               
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        public static bool SendEloadMail(TransactionInfo tran)
        {
            try
            {
                if (tran.Status == Common.TRANSTATUS.FINISH)
                {
                    Connection con = new Connection();
                    DataTable userInfo = con.FillDataTable(Common.ConStr, "SEMS_EBA_USERS_GETUBID",
                        tran.Data[Common.KEYNAME.USERID]);
                    string email = userInfo.Rows[0]["Email"].ToString();
                    if (string.IsNullOrEmpty(email))
                    {
                        Utility.ProcessLog.LogInformation("======>  Can not Send to empty mail MB topup with ipctransid=" + tran.IPCTransID.ToString() + "/User=" + tran.Data[Common.KEYNAME.USERID].ToString() + "/");
                        return false;
                    }

                    Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();

                    tmpl = Common.GetEmailTemplate("BuyETopUpSucsessfullen-US");

                    tmpl.SetAttribute("senderName", userInfo.Rows[0]["FullName"].ToString());

                    tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                    tmpl.SetAttribute("senderAccount",
                        tran.Data.ContainsKey(Common.KEYNAME.ACCTNO) ? tran.Data[Common.KEYNAME.ACCTNO].ToString() : "");

                    tmpl.SetAttribute("senderBalance",
                        tran.Data.ContainsKey("REFGLDEBIT") ? tran.Data["REFGLDEBIT"].ToString() : "");

                    tmpl.SetAttribute("ccyid",
                        tran.Data.ContainsKey("CCYID") ? tran.Data["CCYID"].ToString() : "");

                    tmpl.SetAttribute("telecomname",
                        tran.Data.ContainsKey("RECEIVERNAME") ? tran.Data["RECEIVERNAME"].ToString() : "");

                    tmpl.SetAttribute("amount", tran.Data.ContainsKey("AMOUNT") ? Utility.Common.FormatMoneyInput(tran.Data["AMOUNT"].ToString(), "MMK") : "");

                    //tmpl.SetAttribute("amountchu", tran.Data["amountchu"].ToString());
                    tmpl.SetAttribute("amountchu", Utility.Common.AmountToWords(tran.Data["AMOUNT"].ToString()) + " " + tran.Data["CCYID"].ToString());

                    tmpl.SetAttribute("feeType", tran.Data["feeReceiverAmt"] == "0" ? "Sender" : "Receiver");

                    tmpl.SetAttribute("feeAmount",
                        tran.Data.ContainsKey("feeSenderAmt") ? tran.Data["feeSenderAmt"].ToString() : "");

                    tmpl.SetAttribute("desc", tran.Data.ContainsKey("TRANDESC") ? tran.Data["TRANDESC"].ToString() : "");

                    tmpl.SetAttribute("tranID",
                        tran.Data.ContainsKey("IPCTRANSID") ? tran.Data["IPCTRANSID"].ToString() : "");

                    tmpl.SetAttribute("tranDate",
                        tran.Data.ContainsKey("lasttransdate") ? tran.Data["lasttransdate"].ToString() : "");

                    tmpl.SetAttribute("phoneNo", tran.Data.ContainsKey("PHONENO") ? tran.Data["PHONENO"] : "");

                    Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email,
                        ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

                    Utility.ProcessLog.LogInformation("======>  Send mail MB eLoad success ipctransid=" + tran.IPCTransID.ToString() + "/userid=" + tran.Data[Common.KEYNAME.USERID].ToString() + "/Email=" + email);


                    return true;
                }
                else
                {
                    Utility.ProcessLog.LogInformation("======>  Send mail MB eLoad fail with transtatus=" + tran.Status + "/User=" + tran.Data[Common.KEYNAME.USERID].ToString() + "/ipctransid=" + tran.IPCTransID.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                //tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name + " Exception");
                Utility.ProcessLog.LogInformation("======>  Send mail MB eLoad fail");
                return true;
            }
        }
        public static bool SendRequestPasswordNotifyEmail(TransactionInfo tran)
        {
            try
            {
                if (tran.Status == Common.TRANSTATUS.FINISH)
                {
                    Connection con = new Connection();
                    //hunglt fix email 
                    string email = string.Empty;
                    DataTable dtemail = new DataTable();
                    dtemail = con.FillDataTable(Common.ConStr, "IPC_LOAD_EMAILNOTIFY", null);
                    if (dtemail != null && dtemail.Rows.Count > 0 && !string.IsNullOrEmpty(dtemail.Rows[0]["VARVALUE"].ToString()))
                    {
                        email = dtemail.Rows[0]["VARVALUE"].ToString();
                    }
                    else if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ResetPasswordNotifyEmail"].ToString()))
                    {
                        email = ConfigurationManager.AppSettings["ResetPasswordNotifyEmail"].ToString();
                    }

                    //string email = ConfigurationManager.AppSettings["ResetPasswordNotifyEmail"].ToString();
                    Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
                    switch (tran.Data["RESETTYPE"].ToString())
                    {
                        case "PINCODE":

                            tmpl = Common.GetEmailTemplate("ResetPincodeEmail");

                            tmpl.SetAttribute("NAME", tran.Data["NAME"].ToString());
                            tmpl.SetAttribute("PHONENUMBER", tran.Data["PHONENUMBER"].ToString());
                            tmpl.SetAttribute("SERVICEID", tran.Data["SERVICEID"].ToString().Equals("AM") ? "Agent Merchant" : "Consumer");
                            tmpl.SetAttribute("IPCTRANSID", tran.IPCTransID.ToString());

                            Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email,
                                "Request pincode notification", tmpl.ToString());
                            Utility.ProcessLog.LogInformation("======>  Send mail Request pincode notification ipctransid=" + tran.IPCTransID.ToString() + "/Email=" + email);
                            break;
                        default:
                            tmpl = Common.GetEmailTemplate("ResetPasswordEmail");
                            tmpl.SetAttribute("NAME", tran.Data["NAME"].ToString());
                            tmpl.SetAttribute("PHONENUMBER", tran.Data["PHONENUMBER"].ToString());
                            tmpl.SetAttribute("SERVICEID", tran.Data["SERVICEID"].ToString().Equals("AM") ? "Agent Merchant" : "Consumer");

                            tmpl.SetAttribute("IPCTRANSID", tran.IPCTransID.ToString());

                            Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email,
                                "Request password notification", tmpl.ToString());
                            Utility.ProcessLog.LogInformation("======>  Send mail Request password notification ipctransid=" + tran.IPCTransID.ToString() + "/Email=" + email);
                            break;

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name + " Exception");
                Utility.ProcessLog.LogInformation("======>  Send mail Request password notification fail");
                return true;
            }
        }

        //send corp matrix approval notification email
        public static bool SendApprovalNotificationEmail(TransactionInfo tran)
        {
            try
            {
                if (tran.Status == Common.TRANSTATUS.WAITING_APPROVE)
                {
                    string transID = tran.IPCTransID.ToString();
                    string userID = tran.Data[Common.KEYNAME.USERID].ToString().Trim();
                    string emails = "";

                    Connection con = new Connection();
                    DataSet tranInfo = con.FillDataSet(Common.ConStr, "EBA_CM_GETAPPNOTIFICATIONEMAIL", transID, userID);
                    if (tranInfo.Tables[1].Rows.Count < 1)
                    {
                        Utility.ProcessLog.LogInformation("======>  Can not approval notification with ipctransid=" + tran.IPCTransID.ToString() + "/User=" + tran.Data[Common.KEYNAME.USERID].ToString() + "/");
                        return false;
                    }

                    Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
                    tmpl = Common.GetEmailTemplate("MatrixApprovalNotification");
            
                    tmpl.SetAttribute("tranName", tranInfo.Tables[0].Rows[0]["TRANNAME"].ToString());
                    tmpl.SetAttribute("tranID", transID);
                    tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                    tmpl.SetAttribute("senderAccount", tranInfo.Tables[0].Rows[0]["CHAR01"].ToString());
                    tmpl.SetAttribute("amount", Utility.Common.FormatMoneyInput(tranInfo.Tables[0].Rows[0]["NUM01"].ToString(), tranInfo.Tables[0].Rows[0]["CCYID"].ToString()));
                    tmpl.SetAttribute("ccyid", tranInfo.Tables[0].Rows[0]["CCYID"].ToString());
                    tmpl.SetAttribute("amountchu", $"{Utility.Common.AmountToWords(tranInfo.Tables[0].Rows[0]["NUM01"].ToString())} {tranInfo.Tables[0].Rows[0]["CCYID"].ToString()}");
                    tmpl.SetAttribute("desc", tranInfo.Tables[0].Rows[0]["TRANDESC"].ToString());

                    string[] arrworkflowID = tranInfo.Tables[0].Rows[0]["USERCURAPP"].ToString().Trim().Split('|');
                    StringBuilder approvalWorkflow = new StringBuilder();
                    foreach (string iworkflowID in arrworkflowID)
                    {
                        string workflowID = iworkflowID.Split('#')[0].Trim();

                        approvalWorkflow.Append("<table style='width:100%;border-spacing:0px;border-color: lightgray;' border='1'>");

                        #region First row
                        approvalWorkflow.Append("<tr>");
                        approvalWorkflow.Append("<td style='width:25%;'>");
                        approvalWorkflow.Append("<b>Approval workflow ID</b>");
                        approvalWorkflow.Append("</td>");
                        approvalWorkflow.Append("<td style='width:25%;'>");
                        approvalWorkflow.Append("<b>Order</b>");
                        approvalWorkflow.Append("</td>");
                        approvalWorkflow.Append("<td style='width:50%;'>");
                        approvalWorkflow.Append("<b>Formula</b>");
                        approvalWorkflow.Append("</td>");
                        approvalWorkflow.Append("</tr>");
                        #endregion

                        #region entry row

                        if (!string.IsNullOrEmpty(workflowID))
                        {
                            foreach (DataRow drFlow in tranInfo.Tables[2].Select($"WorkflowID = '{workflowID}'"))
                            {
                                approvalWorkflow.Append("<tr>");
                                approvalWorkflow.Append("<td>");
                                approvalWorkflow.Append(drFlow["WorkflowID"].ToString());
                                approvalWorkflow.Append("</td>");
                                approvalWorkflow.Append("<td>");
                                approvalWorkflow.Append(drFlow["Ord"].ToString());
                                approvalWorkflow.Append("</td>");
                                approvalWorkflow.Append("<td>");
                                approvalWorkflow.Append(drFlow["Formula"].ToString());
                                approvalWorkflow.Append("</td>");
                                approvalWorkflow.Append("</tr>");
                            }
                        }

                        #endregion

                        approvalWorkflow.Append("</table>");

                        if (!arrworkflowID.Last().Equals(iworkflowID))
                            approvalWorkflow.Append("<span style = 'font-weight: bold;margin:5px;text-algin:center;'>OR</span>");
                    }
                    tmpl.SetAttribute("approvalWorkflow", approvalWorkflow.ToString());

                    StringBuilder approvalGroup = new StringBuilder();
                    approvalGroup.Append("<table style='width:100%;border-spacing:0px;border-color: lightgray;' border='1'>");

                    #region First row
                    approvalGroup.Append("<tr>");
                    approvalGroup.Append("<td style='width:25%;'>");
                    approvalGroup.Append("<b>Group ID</b>");
                    approvalGroup.Append("</td>");
                    approvalGroup.Append("<td style='width:25%;'>");
                    approvalGroup.Append("<b>Phone</b>");
                    approvalGroup.Append("</td>");
                    approvalGroup.Append("<td style='width:50%;'>");
                    approvalGroup.Append("<b>Full Name</b>");
                    approvalGroup.Append("</td>");
                    approvalGroup.Append("</tr>");
                    #endregion

                    #region entry row
                    foreach (DataRow drFlow in tranInfo.Tables[1].Rows)
                    {
                        if (!string.IsNullOrEmpty(drFlow["GroupID"].ToString()))
                        {
                            approvalGroup.Append("<tr>");
                            approvalGroup.Append("<td>");
                            approvalGroup.Append(drFlow["GroupID"].ToString());
                            approvalGroup.Append("</td>");
                            approvalGroup.Append("<td>");
                            approvalGroup.Append(drFlow["Phone"].ToString().Trim());
                            approvalGroup.Append("</td>");
                            approvalGroup.Append("<td>");
                            approvalGroup.Append(drFlow["FullName"].ToString().Trim());
                            approvalGroup.Append("</td>");
                        }
                    }
                    #endregion

                    approvalGroup.Append("</table>");
                    tmpl.SetAttribute("approvalGroup", approvalGroup.ToString());

                    List<string> lsEmail = new List<string>();
                    foreach (DataRow drUser in tranInfo.Tables[1].Rows)
                    {
                        if (!string.IsNullOrEmpty(drUser["Email"].ToString().Trim()) && !drUser["UserID"].ToString().Equals(userID))
                        {
                            lsEmail.Add(drUser["Email"].ToString().Trim());
                            tmpl.SetAttribute("markerName", $" {drUser["FullName"].ToString()} ");
                            Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], drUser["Email"].ToString().Trim(),
                            "APPROVAL REQUEST", tmpl.ToString());
                        }
                    }
                    emails = string.Join(",", lsEmail);
                    Utility.ProcessLog.LogInformation("======>  Send mail approval notification success ipctransid=" + tran.IPCTransID.ToString() + "/userid=" + tran.Data[Common.KEYNAME.USERID].ToString() + "/Email=" + emails);
                    return true;
                }
                else
                {
                    Utility.ProcessLog.LogInformation("======>  Send approval notification fail with transtatus=" + tran.Status + "/User=" + tran.Data[Common.KEYNAME.USERID].ToString() + "/ipctransid=" + tran.IPCTransID.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                //tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name + " Exception");
                Utility.ProcessLog.LogInformation("======>  Send approval notification fail");
                return true;
            }
        }

        #region vutt 01112017 check ws
        public static bool CheckWSConnection(TransactionInfo tran, string Param)
        {
            string url = "";
            Connection con = new Connection();
            HttpWebRequest myRequest = null;
            HttpWebResponse response = null;

            try
            {
                //get url
                if (Param.Contains("|")) //Do store to get dest infor - input: STORENAME|PARAM - output: IPCTRANCODEDEST|DESTID
                {
                    string[] parmlist = Param.Split('|');
                    object[] parms = new object[parmlist.Length - 1];
                    for (int j = 1; j < parmlist.Length; j++)
                    {
                        if (tran.Data.ContainsKey(parmlist[j].ToString()))
                            parms[j - 1] = tran.Data[parmlist[j].ToString()];
                        else
                            parms[j - 1] = parmlist[j].ToString();
                    }

                    string storeName = parmlist[0].ToString();
                    DataTable dt = con.FillDataTable(Common.ConStr, storeName, parms);
                    if (dt.Rows.Count > 0 && dt.Columns.Contains(Common.KEYNAME.IPCTRANCODEDEST) && dt.Columns.Contains(Common.KEYNAME.DESTID))
                    {
                        url = GetURLByDest(dt.Rows[0][Common.KEYNAME.IPCTRANCODEDEST].ToString(), dt.Rows[0][Common.KEYNAME.DESTID].ToString());
                    }
                }
                else if (Param.ToLower().StartsWith("http")) //Param is url
                {
                    url = Param;
                }
                else //get url by ipctrancodedest, destid will be used default value
                {
                    url = GetURLByDest(Param, tran.Data[Common.KEYNAME.DESTID].ToString());
                }


                if (string.IsNullOrEmpty(url))
                {
                    Utility.ProcessLog.LogInformation("URL not found " + tran.IPCTransID.ToString());
                    return true;
                }

                myRequest = (HttpWebRequest)WebRequest.Create(url);
                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                response = (HttpWebResponse)myRequest.GetResponse();

                if (CheckStatusCode(response))
                {
                    return true;
                }
            }
            catch (WebException we)
            {
                try
                {
                    if (CheckStatusCode(we.Response as System.Net.HttpWebResponse))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name + " Exception");
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name + " Exception");
            }
            finally
            {
                try
                {
                    myRequest.Abort();
                    response.Close();
                }
                catch { }
            }

            tran.ErrorCode = "27";
            tran.ErrorDesc = "Cannot connect to provider, please try again later";
            return false;
        }
        private static string GetURLByDest(string destTranCode, string destID)
        {
            string condition = " DESTID = '" + destID + "'";
            condition += " AND IPCTRANCODE = '" + destTranCode + "'";
            DataRow[] row = Common.DBICONNECTIONWS.Select(condition);
            if (row.Length > 0)
            {
                return row[0]["URLWEBSERVICE"].ToString();
            }
            return "";
        }
        private static bool CheckStatusCode(HttpWebResponse response)
        {
            if (response != null)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return true;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.MethodNotAllowed:
                    case HttpStatusCode.NotAcceptable:
                        Utility.ProcessLog.LogInformation(string.Format("[OK - {0}] {1}", response.StatusCode, response.ResponseUri.ToString()));
                        return true;
                    default:
                        Utility.ProcessLog.LogInformation(string.Format("[FAIL - {0}] {1}", response.StatusCode, response.ResponseUri.ToString()));
                        return false;
                }
            }
            else
            {
                throw new Exception("Response null");
            }
        }
        #endregion
        //duyvk create 20190528
        public bool SendMailCreditCardPayMent(TransactionInfo tran)
        {
            try
            {
                if (tran.Status == Common.TRANSTATUS.FINISH)
                {
                    Connection con = new Connection();
                    #region Get Data
                    DataTable userInfo = con.FillDataTable(Common.ConStr, "SEMS_EBA_USERS_GETUBID", tran.Data[Common.KEYNAME.USERID]);
                    #endregion
                    string email = userInfo.Rows[0][Common.KEYNAME.EMAIL].ToString();
                    Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
                    tmpl = Common.GetEmailTemplate("CR_TransactionSuccessful" + System.Globalization.CultureInfo.CurrentCulture.Name);
                    tmpl.SetAttribute("senderAccount", tran.Data.ContainsKey(Common.KEYNAME.ACCTNO) ? tran.Data[Common.KEYNAME.ACCTNO].ToString() : "");
                    tmpl.SetAttribute("senderBalance", tran.Data.ContainsKey("REFGLDEBIT") ? double.Parse(tran.Data["REFGLDEBIT"].ToString()).ToString("N02").ToString() : "");
                    string sCCYID = tran.Data.ContainsKey("CCYID") ? tran.Data["CCYID"].ToString() : "MMK";
                    tmpl.SetAttribute("ccyid", sCCYID);
                    tmpl.SetAttribute("status", Common.TRANSTATUS.FINISH);
                    tmpl.SetAttribute("senderName", userInfo.Rows[0]["FullName"].ToString());
                    tmpl.SetAttribute("outstandingamount", tran.Data.ContainsKey("outstandingAmt") ? Utility.Common.FormatMoneyInput(tran.Data["outstandingAmt"].ToString(), sCCYID) + " " + sCCYID : "");
                    tmpl.SetAttribute("cardholdername", tran.Data.ContainsKey("RECEIVERNAME") ? tran.Data["RECEIVERNAME"].ToString() : tran.Data.ContainsKey("cardholderName") ? tran.Data["cardholderName"].ToString() : "");
                    tmpl.SetAttribute("cardNo", tran.Data.ContainsKey("CARDMASK") ? tran.Data["CARDMASK"].ToString() : "");
                    tmpl.SetAttribute("amount", tran.Data.ContainsKey("AMOUNT") ? Utility.Common.FormatMoneyInput(tran.Data["AMOUNT"].ToString(), sCCYID) : "");
                    tmpl.SetAttribute("amountchu", Utility.Common.AmountToWords(tran.Data["AMOUNT"].ToString()) + " " + (tran.Data.ContainsKey("CCYID") ? tran.Data["CCYID"].ToString() : ""));
                    tmpl.SetAttribute("feeType", tran.Data.ContainsKey("feeType") ? tran.Data["feeType"].ToString() : "");
                    tmpl.SetAttribute("feeAmount", tran.Data.ContainsKey("feeSenderAmt") ? Utility.Common.FormatMoneyInput(tran.Data["feeSenderAmt"].ToString(), sCCYID) : "");
                    tmpl.SetAttribute("desc", tran.Data.ContainsKey("TRANDESC") ? tran.Data["TRANDESC"].ToString() : "");
                    tmpl.SetAttribute("tranID", tran.Data.ContainsKey("IPCTRANSID") ? tran.Data["IPCTRANSID"].ToString() : "");
                    tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                    DataTable dtSenderBranch = con.FillDataTable(Common.ConStr, "SEMS_EBA_BRANCH_DETAILS",
                        tran.Data["DEBITBRACHID"]);
                    if (dtSenderBranch.Rows.Count != 0)
                    {
                        tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                    }
                    else
                    {
                        tmpl.SetAttribute("senderBranch", "");
                    }

                    Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email,
                        ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

                    Utility.ProcessLog.LogInformation("======>  Send credit card payment email success" + email);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                Utility.ProcessLog.LogInformation("======>  Send credit card payment email fail");
                return true;
            }

        }
        //duyvk create 20191204
        public static bool SendMailATMWithdrawal(TransactionInfo tran)
        {
            try
            {
                if (tran.Status == Common.TRANSTATUS.FINISH)
                {
                    Connection con = new Connection();
                    DataTable userInfo = con.FillDataTable(Common.ConStr, "SEMS_EBA_USERS_GET_INFOR_SENDMAIL",
                        tran.Data[Common.KEYNAME.USERID]);
                    string email = userInfo.Rows[0][Common.KEYNAME.EMAIL].ToString();
                    Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();

                    tmpl = Common.GetEmailTemplate("ATMWithdrawalTransactionSuccessful");

                    tmpl.SetAttribute("senderAccount",
                        tran.Data.ContainsKey(Common.KEYNAME.ACCTNO) ? tran.Data[Common.KEYNAME.ACCTNO].ToString() : "");

                    tmpl.SetAttribute("ccyid",
                        tran.Data.ContainsKey(Common.KEYNAME.CCYID) ? tran.Data[Common.KEYNAME.CCYID].ToString() : "");

                    tmpl.SetAttribute("senderName", userInfo.Rows[0]["FullName"].ToString());

                    tmpl.SetAttribute("amount", tran.Data.ContainsKey(Common.KEYNAME.AMOUNT) ? Utility.Common.FormatMoneyInput(tran.Data[Common.KEYNAME.AMOUNT].ToString(), tran.Data[Common.KEYNAME.CCYID].ToString()) : "");

                    tmpl.SetAttribute("amountchu", Utility.Common.AmountToWords(tran.Data[Common.KEYNAME.AMOUNT].ToString()));

                    tmpl.SetAttribute("desc", tran.Data.ContainsKey(Common.KEYNAME.TRANDESC) ? tran.Data[Common.KEYNAME.TRANDESC].ToString() : "");

                    tmpl.SetAttribute("tranID",
                        tran.Data.ContainsKey(Common.KEYNAME.IPCTRANSID) ? tran.Data[Common.KEYNAME.IPCTRANSID].ToString() : "");

                    tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                    tmpl.SetAttribute("senderBranch", userInfo.Rows[0]["BranchName"].ToString());

                    Common.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email,
                        ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

                    Utility.ProcessLog.LogInformation("======>  Send mail ATM Withdrawal success" + email);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                Utility.ProcessLog.LogInformation("======>  Send mail ATM Withdrawal fail");
                return true;
            }
        }

        public bool LogRequestForLoan(TransactionInfo tran, string LogType)
        {
            Connection con = new Connection();
            try
            {
                DataRow[] dr = Common.DBILOGDEFINEREQUESTFORLOAN.Select(
                    "SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'" +
                    " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "'" +
                    " AND LOGTYPE = '" + LogType + "'");
                if (dr != null)
                {
                    object[] para = new object[6];
                    para[0] = tran.IPCTransID;
                    para[1] = tran.Data[Common.KEYNAME.SOURCEID].ToString();
                    para[2] = LogType;
                    for (int i = 0; i < dr.Length; i++)
                    {
                        Object value = new Object();
                        string valuestyle = dr[i]["VALUESTYLE"].ToString();
                        string valuename = dr[i]["VALUENAME"].ToString();
                        switch (valuestyle)
                        {
                            case "VALUE":
                                value = valuename;
                                break;
                            case "DATA":
                                if (tran.Data.ContainsKey(valuename)) value = tran.Data[valuename];
                                break;
                            case "SQL":
                                string sql = FormatStringParams(tran, valuename);
                                value = con.ExecuteScalarSQL(Common.ConStr, sql);
                                if (value is DataTable)
                                {
                                    DataTable dtSQL = (DataTable)value;
                                    if (dtSQL.Rows.Count > 0)
                                        value = dtSQL.Rows[0][0].ToString();
                                }
                                break;
                        }

                        para[3] = dr[i]["FIELDNAME"];
                        para[4] = value.ToString();
                        para[5] = dr[i]["COLNAME"];
                        con.ExecuteNonquery(Common.ConStr, "IPCLOGREQUESTFORLOAN_INSERT", false, para);
                    }

                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        private static string FormatStringParams(TransactionInfo tran, string str, bool isPassKeyToValue = false)
        {
            try
            {
                if (str.Contains("{"))
                {
                    List<string> param = GetStringParams(str);
                    foreach (string p in param)
                    {
                        string value = (tran.Data.ContainsKey(p)) ? tran.Data[p].ToString() : isPassKeyToValue ? p : "";
                        str = str.Replace("{" + p + "}", value);
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static List<string> GetStringParams(string str)
        {
            List<string> param = new List<string>();
            string[] kk = str.Split('{');
            foreach (string k in kk)
            {
                if (k.Contains("}"))
                {
                    param.Add(k.Split('}')[0].Trim());
                }
            }
            return param;
        }


        //ham GetUpstreamtransaction
        public bool GetUpstreamtransaction(TransactionInfo tran)
        {
            try
            {
                Connection dbObj = new Connection();
                JObject jbody = Common.NewParse(tran.Data["BODYREQ"].ToString());
                string transid = string.Empty;
                string apimethodname = tran.Data["APIMETHODNAME"].ToString();
                List<string> listtransid = new List<string>();
                TransactionInfo SubTrans = null;
                DataTable dtGetUpstream = null;
                DataTable dtwal = null;
                bool ARResult = false;
                JObject jresponse = new JObject();
                switch (apimethodname)
                {
                    case "ADMIN002":
                        transid = jbody.SelectToken("transactionID").Value<string>();
                        SubTrans = new TransactionInfo();
                        SubTrans.IPCTransID = tran.IPCTransID;
                        foreach (string key in tran.Data.Keys)
                        {
                            Common.HashTableAddOrSet(SubTrans.Data, key, tran.Data[key]);
                        }
                        dtGetUpstream = new DataTable();
                        dtGetUpstream = dbObj.FillDataTable(Common.ConStr, "GET_UPSTREAMTRANSACTION", transid);
                        if (dtGetUpstream.Rows.Count > 0)
                        {
                            Common.HashTableAddOrSet(SubTrans.Data, "UPSTREAMTRANCODE", dtGetUpstream.Rows[0]["UPSTREAMTRANCODE"].ToString().Trim());
                            Common.HashTableAddOrSet(SubTrans.Data, "TRNDT", dtGetUpstream.Rows[0]["TRNDT"].ToString());
                            Common.HashTableAddOrSet(SubTrans.Data, "TRNID", dtGetUpstream.Rows[0]["TRNID"].ToString().Trim());
                            Common.HashTableAddOrSet(SubTrans.Data, "UPSTREAMIPCTRANSID", dtGetUpstream.Rows[0]["UPSTREAMIPCTRANSID"].ToString().Trim());
                            Common.HashTableAddOrSet(SubTrans.Data, Common.KEYNAME.SOURCEID, dtGetUpstream.Rows[0][Common.KEYNAME.SOURCEID].ToString().Trim());
                        }
                        ARResult = AutoRouter(SubTrans, "ABREVERT");
                        if (ARResult && SubTrans.Data.ContainsKey("RES_STATUS") && SubTrans.Data["RES_STATUS"].ToString().Equals("SUCCESS"))
                        { 
                            if (SubTrans.Data["UPSTREAMTRANCODE"].Equals("WL_TRANSFEROTHBANK") || SubTrans.Data["UPSTREAMTRANCODE"].Equals("AM_TRANSFEROTHBANK"))
                            {
                                try
                                {
                                    string Txref = SubTrans.Data["UPSTREAMIPCTRANSID"].ToString();
                                    string msglog = $"DoStore INPUT transaction {tran.IPCTransID} Store name = WAL_Inter_REVERSAL param list = Txref paramvalue:" + Environment.NewLine;
                                    msglog += $"{Txref}";
                                    ProcessLog.LogInformation(msglog, Common.FILELOGTYPE.LOGDOSTOREINFO);
                                    dbObj.ExecuteNonquery(Common.ConStr, "WAL_Inter_REVERSAL", Txref);
                                    //revert wal
                                    dbObj.ExecuteNonquery(Common.ConStr, "UPDATE_UPSTREAMTRANSACTION", dtGetUpstream.Rows[0]["UPSTREAMIPCTRANSID"].ToString().Trim(), Common.TRANSTATUS.ERROR);
                                    jresponse.Add("transRef", "0000");
                                    jresponse.Add("status", "Success");
                                    jresponse.Add("description", "Success");
                                }
                                catch (Exception ex)
                                {
                                    ProcessLog.LogInformation($"DoStore OUTPUT transaction {tran.IPCTransID} | Store name WAL_Inter_REVERSAL Return: {Environment.NewLine}{ex.Message}", Common.FILELOGTYPE.LOGDOSTOREINFO);
                                    jresponse.Add("transRef", "0014");
                                    jresponse.Add("status", "Fail");
                                    jresponse.Add("description", SubTrans.Data[Common.KEYNAME.IPCERRORDESC].ToString());
                                }
                            }
                            else
                            {
                                dbObj.ExecuteNonquery(Common.ConStr, "UPDATE_UPSTREAMTRANSACTION", dtGetUpstream.Rows[0]["UPSTREAMIPCTRANSID"].ToString().Trim(), Common.TRANSTATUS.ERROR);
                                jresponse.Add("transRef", "0000");
                                jresponse.Add("status", "Success");
                                jresponse.Add("description", "Success");
                            }
                        }
                        else
                        {
                            jresponse.Add("transRef", "0014");
                            jresponse.Add("status", "Fail");
                            jresponse.Add("description", SubTrans.Data[Common.KEYNAME.IPCERRORDESC].ToString());
                        }
                        
                        break;
                    case "PACS002":
                        transid = jbody.SelectToken("FIToFIPmtStsRpt.TxInfAndSts.OrgnlTxId").Value<string>();
                        dtGetUpstream = new DataTable();
                        dtGetUpstream = dbObj.FillDataTable(Common.ConStr, "GET_UPSTREAMTRANSACTION", transid);
                        if (dtGetUpstream.Rows.Count > 0)
                        {
                            dbObj.ExecuteNonquery(Common.ConStr, "UPDATE_UPSTREAMTRANSACTION", dtGetUpstream.Rows[0]["UPSTREAMIPCTRANSID"].ToString().Trim(), Common.TRANSTATUS.PENDDING);
                            jresponse.Add("transRef", "0000");
                            jresponse.Add("status", "Success");
                            jresponse.Add("description", "Success");
                        }
                        else
                        {
                            jresponse.Add("transRef", "0014");
                            jresponse.Add("status", "Fail");
                            jresponse.Add("description", "Cannot found upstream transaction!");
                        }
                        break;
                    case "PACS008":
                        JArray jccList = jbody.SelectToken("cctList").Value<JArray>();
                        for (int i = 0; i < jccList.Count; i++)
                        {
                            JArray jtransList = jccList[i].SelectToken("transactionList").Value<JArray>();
                            for (int j = 0; j < jtransList.Count; j++)
                            {
                                SubTrans = new TransactionInfo();
                                SubTrans.IPCTransID = tran.IPCTransID;
                                foreach (string key in tran.Data.Keys)
                                {
                                    Common.HashTableAddOrSet(SubTrans.Data, key, tran.Data[key]);
                                }
                                transid = jtransList[j].SelectToken("transactionId").Value<string>();
                                dtGetUpstream = new DataTable();
                                dtGetUpstream = dbObj.FillDataTable(Common.ConStr, "GET_UPSTREAMTRANSACTION", transid);
                                if (dtGetUpstream.Rows.Count > 0)
                                {
                                    Common.HashTableAddOrSet(SubTrans.Data, "UPSTREAMTRANCODE", dtGetUpstream.Rows[0]["UPSTREAMTRANCODE"].ToString().Trim());
                                    Common.HashTableAddOrSet(SubTrans.Data, "AMOUNT", dtGetUpstream.Rows[0]["AMOUNT"].ToString());
                                    Common.HashTableAddOrSet(SubTrans.Data, "TOTALFEE", dtGetUpstream.Rows[0]["TOTALFEE"].ToString());
                                    Common.HashTableAddOrSet(SubTrans.Data, "ACCTNO", dtGetUpstream.Rows[0]["ACCTNO"].ToString().Trim());
                                    Common.HashTableAddOrSet(SubTrans.Data, "RECEIVERACCOUNT", dtGetUpstream.Rows[0]["RECEIVERACCOUNT"].ToString().Trim());
                                    Common.HashTableAddOrSet(SubTrans.Data, "UPSTREAMIPCTRANSID", dtGetUpstream.Rows[0]["UPSTREAMIPCTRANSID"].ToString().Trim());
                                    Common.HashTableAddOrSet(SubTrans.Data, Common.KEYNAME.SOURCEID, dtGetUpstream.Rows[0][Common.KEYNAME.SOURCEID].ToString().Trim());
                                    Common.HashTableAddOrSet(SubTrans.Data, "PageDescription", dtGetUpstream.Rows[0]["PageDescription"].ToString().Trim());
                                }
                                ARResult = AutoRouter(SubTrans, "ABINTERBANKREFUND");
                                if (ARResult && SubTrans.Data.ContainsKey("TRNID"))
                                {
                                    if (SubTrans.Data["UPSTREAMTRANCODE"].Equals("WL_TRANSFEROTHBANK") || SubTrans.Data["UPSTREAMTRANCODE"].Equals("AM_TRANSFEROTHBANK"))
                                    {
                                        try
                                        {
                                            string TxrefRR = tran.Data["IPCTRANSID"].ToString();
                                            int RRID = -1;
                                            string Txref = SubTrans.Data["UPSTREAMIPCTRANSID"].ToString();
                                            string Description = "Interbank refund Wallet transfer other bank with transactionid = " + Txref;
                                            string ipctrancode = SubTrans.Data["UPSTREAMTRANCODE"].ToString();

                                            string msglog = $"DoStore INPUT transaction {tran.IPCTransID} Store name = WAL_INTER_REFUND param list = TxrefRR, RRID, Txref, Description, ipctrancode, UserID  paramvalue:" + Environment.NewLine;
                                            msglog += $"{TxrefRR},{RRID},{Txref}, {Description}, {ipctrancode}, {tran.Data["USERID"].ToString().Trim()}";
                                            ProcessLog.LogInformation(msglog, Common.FILELOGTYPE.LOGDOSTOREINFO);

                                            dbObj.ExecuteNonquery(Common.ConStr, "WAL_INTER_REFUND", TxrefRR, RRID, Txref, Description, ipctrancode,tran.Data["USERID"].ToString().Trim());
                                            //revert wal
                                            dbObj.ExecuteNonquery(Common.ConStr, "UPDATE_UPSTREAMTRANSACTION", dtGetUpstream.Rows[0]["UPSTREAMIPCTRANSID"].ToString().Trim(), Common.TRANSTATUS.ERROR);
                                            jresponse.Add("connSeqNo", "");
                                            jresponse.Add("code", "0000");
                                            jresponse.Add("desc", "Success");
                                        }
                                        catch (Exception ex)
                                        {
                                            dbObj.ExecuteNonquery(Common.ConStr, "UPDATE_UPSTREAMTRANSACTION", dtGetUpstream.Rows[0]["UPSTREAMIPCTRANSID"].ToString().Trim(), Common.TRANSTATUS.ERROR);
                                            jresponse.Add("connSeqNo", "");
                                            jresponse.Add("code", "0014");
                                            jresponse.Add("desc", SubTrans.Data[Common.KEYNAME.IPCERRORDESC].ToString());
                                        }
                                    }
                                    else
                                    {
                                        dbObj.ExecuteNonquery(Common.ConStr, "UPDATE_UPSTREAMTRANSACTION", dtGetUpstream.Rows[0]["UPSTREAMIPCTRANSID"].ToString().Trim(), Common.TRANSTATUS.ERROR);
                                        jresponse.Add("connSeqNo", "");
                                        jresponse.Add("code", "0000");
                                        jresponse.Add("desc", "Success");
                                    }
                                }
                                else
                                {
                                    dbObj.ExecuteNonquery(Common.ConStr, "UPDATE_UPSTREAMTRANSACTION", dtGetUpstream.Rows[0]["UPSTREAMIPCTRANSID"].ToString().Trim(), Common.TRANSTATUS.ERROR);
                                    jresponse.Add("connSeqNo", "");
                                    jresponse.Add("code", "0014");
                                    jresponse.Add("desc", SubTrans.Data[Common.KEYNAME.IPCERRORDESC].ToString());
                                }
                            }
                        }
                        break;
                    case "CAMT057":
                        JArray jtransid = jbody.SelectToken("notificationToReceiveList").Value<JArray>();
                        for (int j = 0; j < jtransid.Count; j++)
                        {
                            dtGetUpstream = new DataTable();
                            transid = jtransid[j].SelectToken("transactionId").Value<string>();
                            dtGetUpstream = dbObj.FillDataTable(Common.ConStr, "GET_UPSTREAMTRANSACTION", transid);
                            if (dtGetUpstream.Rows.Count > 0)
                            {
                                dbObj.ExecuteNonquery(Common.ConStr, "UPDATE_UPSTREAMTRANSACTION", dtGetUpstream.Rows[0]["UPSTREAMIPCTRANSID"].ToString().Trim(), Common.TRANSTATUS.FINISH);
                                jresponse.Add("connSeqNo", "");
                                jresponse.Add("code", "0000");
                                jresponse.Add("desc", "Success");
                            }
                            else
                            {
                                jresponse.Add("connSeqNo", "");
                                jresponse.Add("code", "0014");
                                jresponse.Add("desc", "Cannot found upstream transaction!");
                            }
                        }
                        break;
                }
                Common.HashTableAddOrSet(tran.Data, "APIRESPONSE", jresponse.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }

        }

        public bool ForeachDataTableSubTran(TransactionInfo tran, string param = "")
        {
            Connection con = new Connection();
            try
            {
                string msglog = string.Empty;
                DataTable dt = new DataTable();
                string[] parmlist = param.Split('|');
                object[] parms = new object[parmlist.Length - 1];
                for (int j = 1; j < parmlist.Length; j++)
                {
                    if (tran.Data.ContainsKey(parmlist[j].ToString()))
                        parms[j - 1] = tran.Data[parmlist[j].ToString()];
                    else
                        parms[j - 1] = parmlist[j].ToString();
                }

                string storeName = parmlist[0].ToString();

                msglog = $"DoStore INPUT transaction {tran.IPCTransID} Store name = {storeName} param list = {string.Join("|", JsonConvert.SerializeObject(parmlist.Skip(1).ToArray()))} paramvalue:" + Environment.NewLine;
                msglog += $"{(parms != null ? string.Join("|", JsonConvert.SerializeObject(parms)) : "NULL")}";
                ProcessLog.LogInformation(msglog, Common.FILELOGTYPE.LOGDOSTOREINFO);

                try
                {
                    dt = con.FillDataTable(Common.ConStr, storeName, parms);

                    msglog = $"DoStore OUTPUT transaction {tran.IPCTransID} | {param} with Store name {storeName}" + Environment.NewLine;
                    msglog += $"{JsonConvert.SerializeObject(dt)}";
                    ProcessLog.LogInformation(msglog, Common.FILELOGTYPE.LOGDOSTOREINFO);
                }
                catch (Exception ex)
                {
                    ProcessLog.LogInformation($"DoStore OUTPUT transaction {tran.IPCTransID} | {param} with Store name {storeName} Return: {Environment.NewLine}{ex.Message}", Common.FILELOGTYPE.LOGDOSTOREINFO);
                }

                if (dt.Rows.Count == 0)
                {
                    Utility.ProcessLog.LogInformation($"{tran.IPCTransID} {System.Reflection.MethodBase.GetCurrentMethod().Name} sub transaction not found!", Utility.Common.FILELOGTYPE.LOGFILEPATH);
                    return true;
                }
                else
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        try
                        {
                            if (!dt.Columns.Contains(Common.KEYNAME.IPCTRANCODEDEST) || string.IsNullOrEmpty(dtRow[Common.KEYNAME.IPCTRANCODEDEST].ToString()))
                            {
                                Utility.ProcessLog.LogInformation($"{tran.IPCTransID} {System.Reflection.MethodBase.GetCurrentMethod().Name} sub transaction not found!", Utility.Common.FILELOGTYPE.LOGFILEPATH);
                                break;
                            }

                            TransactionInfo tranInfo = new TransactionInfo();
                            tranInfo.NewIPCTransID();
                            Common.HashTableAddOrSet(tranInfo.Data, Common.KEYNAME.IPCTRANCODE, dtRow[Common.KEYNAME.IPCTRANCODEDEST]);
                            Common.HashTableAddOrSet(tranInfo.Data, Common.KEYNAME.SOURCEID, dt.Columns.Contains(Common.KEYNAME.SOURCEID) ? dtRow[Common.KEYNAME.SOURCEID] : tran.Data[Common.KEYNAME.SOURCEID]);

                            // Add Data define
                            if (!Formatter.AddDataDefine(tranInfo)) return false;

                            tranInfo.Status = Common.TRANSTATUS.BEGIN;
                            Common.HashTableAddOrSet(tranInfo.Data, Common.KEYNAME.STATUS, Common.TRANSTATUS.BEGIN);
                            Common.HashTableAddOrSet(tranInfo.Data, Common.KEYNAME.DESTID, tran.Data[Common.KEYNAME.DESTID]);
                            Common.HashTableAddOrSet(tranInfo.Data, Common.KEYNAME.REVERSAL, "N");

                            foreach (DataColumn column in dt.Columns)
                            {
                                Common.HashTableAddOrSet(tranInfo.Data, column.ColumnName, dtRow[column.ColumnName]);
                            }

                            AutoRouter(tranInfo, dtRow[Common.KEYNAME.IPCTRANCODEDEST].ToString());

                            if (!tranInfo.ErrorCode.Equals(Common.ERRORCODE.OK))
                            {
                                Utility.ProcessLog.LogInformation($"{tranInfo.IPCTransID} sub transaction error!", Utility.Common.FILELOGTYPE.LOGFILEPATH);
                            }
                        }
                        catch (Exception ex)
                        {
                            ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                            tran.SetErrorInfo(ex);
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
            finally
            {
                con = null;
            }
            return true;
        }
    }
}