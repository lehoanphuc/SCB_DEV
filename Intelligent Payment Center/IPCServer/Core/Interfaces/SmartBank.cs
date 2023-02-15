using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Utility;
using DBConnection;
using System.Collections.Specialized;

namespace Interfaces
{
    public class SmartBank
    {
        #region Private Const
        private const int PARM_TRANTELT_LEN = 29;
        private const int PARM_POST1_LEN = 19;
        private const int PARM_POST2_LEN = 7;

        private const int PARM_SUBBRID = 1;
        private const int PARM_TXDATE = 3;
        private const int PARM_TXNUM = 8;
        private const int PARM_TLTXCD = 10;
        private const int PARM_IBT = 12;
        private const int PARM_NUM = 15;
        private const int PARM_CHAR = 16;
        private const int PARM_DELTD = 21;
        private const int PARM_TRANINFO = 24;
        private const int PARM_TRANTELT = 25;
        #endregion

        #region Public Function
        public bool CreateParm(TransactionInfo tran, string tranCode)
        {
            try
            {
                if (CreateParm(tran, ref tran.parm, "-1", tranCode) == true)
                {
                    if (CheckIBT(tran) == true)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }

        public bool TXRouter(TransactionInfo tran)
        {
            try
            {
                object[] args = { tran.parm };
                ParameterModifier p = new ParameterModifier(1);
                p[0] = true;
                ParameterModifier[] mods = { p };

                System.Type type = System.Type.GetTypeFromProgID("ActiveTeller.Router", Common.SYSVAR["SMBHOST"].ToString(), true);
                object app = System.Activator.CreateInstance(type);

                object result = type.InvokeMember("TxRouter", BindingFlags.InvokeMethod, null, app, args, mods, null, null);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                tran.parm = (object[])args[0];

            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                if (ex.InnerException != null)
                {
                    tran.ErrorCode = ex.InnerException.Message.Replace("'", "''");
                    tran.MappingDestErrorCode();
                }
                //
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {

            }
            return true;
        }

        public bool GroupRouter(TransactionInfo tran)
        {
            try
            {
                object[] args = { tran.parm };
                ParameterModifier p = new ParameterModifier(1);
                p[0] = true;
                ParameterModifier[] mods = { p };

                System.Type type = System.Type.GetTypeFromProgID("ActiveTeller.Router", Common.SYSVAR["SMBHOST"].ToString(), true);
                object app = System.Activator.CreateInstance(type);

                object result = type.InvokeMember("GroupRouter", BindingFlags.InvokeMethod, null, app, args, mods, null, null);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                tran.parm = (object[])args[0];
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                if (ex.InnerException != null)
                {
                    tran.ErrorCode = ex.InnerException.Message.Replace("'", "''");
                    tran.MappingDestErrorCode();
                }
                //
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {

            }
            return true;
        }

        public bool GetTranStatus(TransactionInfo tran)
        {
            try
            {
                string statusTran = "";
                object[] args = { tran.Data[Common.KEYNAME.DESTTRANREF].ToString(), tran.Data[Common.KEYNAME.TRANDATE.ToString()], statusTran };
                ParameterModifier p = new ParameterModifier(3);
                p[0] = true;
                p[1] = true;
                p[2] = true;
                ParameterModifier[] mods = { p };

                System.Type type = System.Type.GetTypeFromProgID("ActiveTeller.Router", Common.SYSVAR["SMBHOST"].ToString(), true);
                object app = System.Activator.CreateInstance(type);

                object result = type.InvokeMember("GetTranStatus", BindingFlags.InvokeMethod, null, app, args, mods, null, null);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                tran.Data.Add(Common.KEYNAME.STATUSTRAN, args[2].ToString());

            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                if (ex.InnerException != null)
                {
                    tran.ErrorCode = ex.InnerException.Message.Replace("'", "''");
                    tran.MappingDestErrorCode();
                }
                //
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {

            }
            return true;
        }

        public bool AuToTX(TransactionInfo tran)
        {
            try
            {
                object[] args = { tran.parm, true, "T" };
                ParameterModifier p = new ParameterModifier(1);
                p[0] = true;
                ParameterModifier[] mods = { p };

                System.Type type = System.Type.GetTypeFromProgID("ActiveTeller.AutoTrans", Common.SYSVAR["SMBHOST"].ToString(), true);
                object app = System.Activator.CreateInstance(type);

                object result = type.InvokeMember("AutoTx", BindingFlags.InvokeMethod, null, app, args, mods, null, null);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                tran.parm = (object[])args[0];

            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                if (ex.InnerException != null)
                {
                    tran.ErrorCode = ex.InnerException.Message.Replace("'", "''");
                    tran.MappingDestErrorCode();
                }
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {

            }
            return true;
        }

        public bool LogSMBInfo(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                // Log Parm Num & Parm Char
                object[] parmnum = (object[])tran.parm[PARM_NUM];
                object[] parmchar = (object[])tran.parm[PARM_CHAR];
                con.ExecuteNonquery(Common.ConStr, "IPCSMBLOG_INSERT", Common.IPCWorkDate, tran.IPCTransID,
                                                            tran.Data[Common.KEYNAME.SOURCEID], tran.Data[Common.KEYNAME.SOURCETRANREF], tran.Data[Common.KEYNAME.DESTTRANREF],
                                                            parmnum[1], parmnum[2], parmnum[3], parmnum[4], parmnum[5],
                                                            parmnum[6], parmnum[7], parmnum[8], parmnum[9], parmnum[10],
                                                            parmchar[1], parmchar[2], parmchar[3], parmchar[4], parmchar[5],
                                                            parmchar[6], parmchar[7], parmchar[8], parmchar[9], parmchar[10],
                                                            parmchar[11], parmchar[12], parmchar[13], parmchar[14], parmchar[15],
                                                            parmchar[16], parmchar[17], parmchar[18], parmchar[19], parmchar[20]);
                // Log TranTelt
                if (tran.parm.Length >= PARM_TRANTELT_LEN)
                {
                    object objResult = null;
                    object[,] parmtrantelt = (object[,])tran.parm[PARM_TRANTELT];

                    for (int i = 1; i < parmtrantelt.GetLength(0); i++)
                    {
                        for (int j = 1; j < parmtrantelt.GetLength(1); j++)
                        {
                            if (parmtrantelt[i, j] != null)
                            {
                                objResult = con.ExecuteScalar(Common.ConStr, "IPCSMBTRANTELT_INSERT", Common.IPCWorkDate, tran.IPCTransID,
                                                                            tran.Data[Common.KEYNAME.SOURCEID], tran.Data[Common.KEYNAME.SOURCETRANREF], tran.Data[Common.KEYNAME.DESTTRANREF],
                                                                            tran.parm[PARM_TLTXCD], parmtrantelt[i, 1], parmtrantelt[i, 2].ToString(), parmtrantelt[i, 3].ToString(),
                                                                            parmtrantelt[i, 4], tran.parm[PARM_DELTD], parmtrantelt[i, 5]);
                                j = 7;
                            }
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
            finally
            {
                con = null;
            }
            return true;
        }

        public bool GetParmNum(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                DataTable dt = con.FillDataTable(Common.ConStr, "IPCSMBLOG_SELECT", Common.IPCWorkDate, tran.Data[Common.KEYNAME.SOURCEID],
                                                                                    tran.IPCTransIDReversal, tran.Data[Common.KEYNAME.SOURCETRANREF], tran.Data[Common.KEYNAME.DESTTRANREF]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    object[] parmnum = (object[])tran.parm[PARM_NUM];
                    for (int i = 1; i <= 10; i++)
                    {
                        parmnum[i] = dt.Rows[0]["NUM" + i.ToString().PadLeft(2, '0')];
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

        public bool GetParmChar(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                DataTable dt = con.FillDataTable(Common.ConStr, "IPCSMBLOG_SELECT", Common.IPCWorkDate, tran.Data[Common.KEYNAME.SOURCEID],
                                                                                    tran.IPCTransIDReversal, tran.Data[Common.KEYNAME.SOURCETRANREF], tran.Data[Common.KEYNAME.DESTTRANREF]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    object[] parmchar = (object[])tran.parm[PARM_CHAR];
                    for (int i = 1; i <= 20; i++)
                    {
                        parmchar[i] = dt.Rows[0]["CHAR" + i.ToString().PadLeft(2, '0')];
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

        public bool GetParmTranTelt(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                object[] parmtemp = new object[PARM_TRANTELT_LEN];
                Array.Copy(tran.parm, parmtemp, tran.parm.Length);
                tran.parm = parmtemp;
                //
                object[,] parmtrantelt = new object[PARM_POST1_LEN, PARM_POST2_LEN];
                DataTable dt = con.FillDataTable(Common.ConStr, "IPCSMBTRANTELT_SELECT", Common.IPCWorkDate, tran.Data[Common.KEYNAME.SOURCEID],
                                                                                    tran.Data[Common.KEYNAME.SOURCETRANREF], tran.Data[Common.KEYNAME.DESTTRANREF]);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parmtrantelt[i + 1, 1] = dt.Rows[i]["ACCTNO"];
                    parmtrantelt[i + 1, 2] = dt.Rows[i]["DORC"];
                    parmtrantelt[i + 1, 3] = dt.Rows[i]["AMT"];
                    parmtrantelt[i + 1, 4] = dt.Rows[i]["CCYCD"];
                    parmtrantelt[i + 1, 5] = dt.Rows[i]["SUBTXNO"].ToString().Trim();
                    parmtrantelt[i + 1, 6] = "T";
                }
                tran.parm[PARM_TRANTELT] = parmtrantelt;
                tran.parm[PARM_TXNUM] = tran.Data[Common.KEYNAME.DESTTRANREF];
                tran.parm[PARM_DELTD] = "1";
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

        public bool FillParmNumChar(TransactionInfo tran)
        {
            try
            {
                object[] parmnum = (object[])tran.parm[PARM_NUM];
                for (int i = 0; i < parmnum.Length; i++)
                {
                    if (parmnum[i] == null) parmnum[i] = 0;
                }

                object[] parmchar = (object[])tran.parm[PARM_CHAR];
                for (int i = 0; i < parmchar.Length; i++)
                {
                    if (parmchar[i] == null) parmchar[i] = "";
                }

                if (tran.parm[PARM_TRANINFO] != null)
                {
                    object[] parmtraninfo = (object[])tran.parm[PARM_TRANINFO];
                    for (int i = 0; i < parmtraninfo.Length; i++)
                    {
                        if (parmtraninfo[i] == null) parmtraninfo[i] = "";
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

        public bool GetSMBTXNUM(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                if (!tran.Data.ContainsKey(Common.KEYNAME.DESTTRANREF) || tran.Data[Common.KEYNAME.DESTTRANREF].ToString() == "")
                {
                    tran.Data[Common.KEYNAME.DESTTRANREF] = con.ExecuteScalar(Common.ConStr, "IPCSMB_GETTXNUM").ToString();
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

        public bool GetSMBTXNUM(TransactionInfo tran, string KeyName)
        {
            Connection con = new Connection();
            try
            {
                tran.Data[KeyName] = con.ExecuteScalar(Common.ConStr, "IPCSMB_GETTXNUM").ToString();
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

        public bool GetParmInfo(TransactionInfo tran)
        {
            //NameValueCollection resultParm = new NameValueCollection();

            try
            {
                object[] parmValue = (object[])tran.parm[0];
                int count = 2;
                string temp = (string)parmValue[count];
                string[] temp1 = temp.Split('\t');
                object[,] resultParm = new object[parmValue.Length - 2, temp1.Length];

                for (int i = 0; i < parmValue.Length - 2; i++)
                {
                    for (int j = 0; j < temp1.Length; j++)
                    {
                        resultParm[i, j] = temp1[j].ToString().Trim();

                    }
                    if (count != parmValue.Length - 1)
                    {
                        count++;
                        temp = (string)parmValue[count];
                        temp1 = temp.Split('\t');
                    }
                    else break;
                }
                tran.Data.Add(Common.KEYNAME.HISTORYINFO, resultParm);
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }

        }

        public bool AddObjParm(TransactionInfo tran)
        {
            try
            {
                object[,] parmValue = (object[,])tran.parm[0];
                tran.Data.Add(Common.KEYNAME.HISTORYINFO, parmValue);
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }

        }

        public bool GetTranHis(TransactionInfo tran)
        {
            //NameValueCollection resultParm = new NameValueCollection();

            try
            {
                object[] parmValue = (object[])tran.parm[0];
                if (parmValue.Length > 2)
                {
                    int count = 2;
                    string temp = (string)parmValue[count];
                    string[] temp1 = temp.Split('\t');
                    object[,] resultParm = new object[parmValue.Length - 2, temp1.Length + 1];

                    for (int i = 0; i < parmValue.Length - 2; i++)
                    {
                        for (int j = 0; j < temp1.Length; j++)
                        {
                            resultParm[i, j] = temp1[j].ToString().Trim();
                        }
                        resultParm[i, temp1.Length] = double.Parse(resultParm[i, 3].ToString()) - double.Parse(resultParm[i, 2].ToString());
                        if (count != parmValue.Length - 1)
                        {
                            count++;
                            temp = (string)parmValue[count];
                            temp1 = temp.Split('\t');
                        }
                        else break;
                    }
                    tran.Data.Add(Common.KEYNAME.HISTORYINFO, resultParm);
                }

                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }

        }
        #endregion

        #region Private Function
        private bool CheckIBT(TransactionInfo tran)
        {
            try
            {
                // DucNH change to Equals function
                object[] args = (object[])tran.parm[PARM_CHAR];
                if (args[1] != null && args[2] != null && args[1].ToString().Length >= 3 && args[2].ToString().Length >= 3)
                {
                    try
                    {
                        double.Parse(args[2].ToString());
                    }
                    catch
                    {
                        string parmSubbrid = (string)tran.parm[PARM_SUBBRID];
                        if (!args[1].ToString().Substring(0, 2).Equals(parmSubbrid.Substring(0, 2)))
                        {
                            tran.parm[PARM_IBT] = 1;
                        }
                        else
                            tran.parm[PARM_IBT] = 0;

                        return true;
                    }
                    if (!args[1].ToString().Substring(0, 2).Equals(args[2].ToString().Substring(0, 2)))
                    {
                        tran.parm[PARM_IBT] = 1;
                    }
                    else
                    {
                        tran.parm[PARM_IBT] = 0;
                    }
                }
                else
                {
                    if (args[1] != null && (args[2] == null || args[2].ToString() == "") && tran.parm[PARM_SUBBRID] != null)
                    {
                        string parmSubbrid = (string)tran.parm[PARM_SUBBRID];
                        if (!args[1].ToString().Substring(0, 2).Equals(parmSubbrid.Substring(0, 2)))
                        {
                            tran.parm[PARM_IBT] = 1;
                        }
                        else
                            tran.parm[PARM_IBT] = 0;
                    }
                    else
                        tran.parm[PARM_IBT] = 0;
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

        private bool CreateParm(TransactionInfo tran, ref object[] parm, string ParentNode, string tranCode)
        {
            int i = 0;
            try
            {
                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "' AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + tranCode + "' AND PARENTNODE = '" + ParentNode + "'";
                DataRow[] dr = Common.DBIPARMINFO.Select(condition);
                for (i = 0; i < dr.Length; i++)
                {
                    switch (dr[i]["MAPPINGSTYLE"].ToString())
                    {
                        case "ARRAY1":
                            if (dr[i]["POSITION"].ToString() == "-1")
                            {
                                parm = new object[int.Parse(dr[i]["MAPPINGVALUE"].ToString())];
                                if (CreateParm(tran, ref parm, dr[i]["NODE"].ToString(), tranCode) == false) return false;
                            }
                            else
                            {
                                object[] temp = new object[int.Parse(dr[i]["MAPPINGVALUE"].ToString())];
                                parm[int.Parse(dr[i]["POSITION"].ToString())] = temp;
                                if (CreateParm(tran, ref temp, dr[i]["NODE"].ToString(), tranCode) == false) return false;
                            }
                            break;
                        case "CUSTOM":
                            parm[int.Parse(dr[i]["POSITION"].ToString())] = Common.CUSTOM[dr[i]["MAPPINGVALUE"]];
                            break;
                        case "INPUT":
                            parm[int.Parse(dr[i]["POSITION"].ToString())] = tran.Data[dr[i]["MAPPINGVALUE"]];
                            break;
                        case "RUNTIME":
                            switch (dr[i]["MAPPINGVALUE"].ToString())
                            {
                                case "TIME":
                                    parm[int.Parse(dr[i]["POSITION"].ToString())] = DateTime.Now.ToString("HH:mm:ss");
                                    break;
                                case "DATE":
                                    parm[int.Parse(dr[i]["POSITION"].ToString())] = DateTime.Now.ToString("dd/MM/yyyy");
                                    break;
                                case "TXNUM":
                                    if (tran.Data[Common.KEYNAME.REVERSAL].ToString() == "N")
                                    {
                                        if (GetSMBTXNUM(tran) == false) return false;
                                        parm[int.Parse(dr[i]["POSITION"].ToString())] = tran.Data[Common.KEYNAME.DESTTRANREF];
                                    }
                                    //else
                                    //{
                                    //    //HungNM10 for reversal
                                    //    parm[int.Parse(dr[i]["POSITION"].ToString())] = tran.Data[Common.KEYNAME.DESTTRANREF];
                                    //}
                                    break;
                                default:
                                    if (dr[i]["MAPPINGVALUE"].ToString().Substring(0, 5) == "TXNUM")
                                    {
                                        if (GetSMBTXNUM(tran, dr[i]["MAPPINGVALUE"].ToString()) == false) return false;
                                        parm[int.Parse(dr[i]["POSITION"].ToString())] = tran.Data[dr[i]["MAPPINGVALUE"].ToString()];
                                    }
                                    break;

                            }
                            break;
                        case "SYSVAR":
                            parm[int.Parse(dr[i]["POSITION"].ToString())] = Common.SYSVAR[dr[i]["MAPPINGVALUE"]];
                            break;
                        case "VALUE":
                            parm[int.Parse(dr[i]["POSITION"].ToString())] = dr[i]["MAPPINGVALUE"];
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "[" + i.ToString() + "]");
                return false;
            }
            return true;
        }
        #endregion
    }
}