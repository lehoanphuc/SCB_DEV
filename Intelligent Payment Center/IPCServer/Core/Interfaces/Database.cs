using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.Data;
using DBConnection;
using Newtonsoft.Json;

namespace Interfaces
{
    public class Database
    {
        /// <summary>
        /// Thuc hien store va add vao DATA theo tung field cua tung dong
        /// Dong dau tien se lay key theo dung ten field
        /// Cac dong tiep theo lay key theo ten field va stt cua dong
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parmList"></param>
        /// <returns></returns>
        public bool DoStore(TransactionInfo tran, string parmList)
        {
            Connection con = new Connection();
            object[] parms = null;
            string storeName = string.Empty;
            try
            {
                string[] parmlist = parmList.Split('|');
                parms = new object[parmlist.Length - 2];
                for (int j = 2; j < parmlist.Length; j++)
                {
                    if (tran.Data.ContainsKey(parmlist[j].ToString()))
                    {
                        parms[j - 2] = tran.Data[parmlist[j].ToString()];
                    }
                    else
                    {
                        parms[j - 2] = parmlist[j].ToString();
                    }
                }
                if (tran.Data.ContainsKey(parmlist[0].ToString()))
                    storeName = tran.Data[parmlist[0].ToString()].ToString();
                else
                    storeName = parmlist[0].ToString();
                DataSet ds = new DataSet();
                try
                {
                    string msglog = $"DoStore INPUT transaction {tran.IPCTransID} Store name = {storeName} param list = {parmList} paramvalue:" + Environment.NewLine;
                    msglog += $"{(parms != null ? string.Join("|", JsonConvert.SerializeObject(parms)) : "NULL")}";
                    ProcessLog.LogInformation(msglog, Common.FILELOGTYPE.LOGDOSTOREINFO);
                }
                catch { }
                if (parms.Length > 0 && parms[0].GetType().FullName == "System.Object[]")
                {
                    ds = con.FillDataSet(Common.ConStr, storeName, (object[])(parms[0]));
                }
                else
                {
                    ds = con.FillDataSet(Common.ConStr, storeName, parms);
                }
                try
                {
                    string msglog = $"DoStore OUTPUT transaction {tran.IPCTransID} | {parmList} with Store name {storeName}" + Environment.NewLine;
                    msglog += $"{JsonConvert.SerializeObject(ds)}";
                    ProcessLog.LogInformation(msglog, Common.FILELOGTYPE.LOGDOSTOREINFO);
                }
                catch { }
                return GetDataFromDataSet(tran, ds, parmlist[1].ToString());
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf(Common.KEYNAME.IPCERRORCODE) < 0)
                {
                    tran.SetErrorInfo(ex);
                }
                else
                {
                    tran.SetErrorInfo(ex.Message.Substring(ex.Message.IndexOf("=") + 1), "");
                }
                try
                {
                    ProcessLog.LogInformation($"DoStore OUTPUT transaction {tran.IPCTransID} | {parmList} with Store name {storeName} Return: {Environment.NewLine}{ex.Message}", Common.FILELOGTYPE.LOGDOSTOREINFO);
                }
                catch { }
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), $"{System.Reflection.MethodBase.GetCurrentMethod().Name} in transaction {tran.IPCTransID} with param list: {parmList} and paramvalue {(parms != null ? string.Join("|", parms) : "nill")}");
                return false;
            }
            finally
            {
                con = null;
            }
        }

        /// <summary>
        /// Thuc hien store va add vao DATA mot mang 2 chieu voi key la ten bang cua Store tra ve
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parmList"></param>
        /// <returns></returns>
        public bool DoStoreParm(TransactionInfo tran, string parmList)
        {
            Connection con = new Connection();
            try
            {
                string[] parmlist = parmList.Split('|');
                object[] parms = new object[parmlist.Length - 1];
                for (int j = 1; j < parmlist.Length; j++)
                {
                    parms[j - 1] = tran.Data[parmlist[j].ToString()].ToString();
                }
                DataSet ds = con.FillDataSet(Common.ConStr, parmlist[0].ToString(), parms);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    object[,] objParmList = new object[dt.Rows.Count, dt.Columns.Count];
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            objParmList[row, col] = dt.Rows[row][col].ToString();
                        }
                    }
                    string TableName = "DATATABLE";
                    if (ds.Tables.Count > 1 && ds.Tables[ds.Tables.Count - 1].Rows.Count > 0)
                    {
                        TableName = ds.Tables[ds.Tables.Count - 1].Rows[0][0].ToString();
                    }
                    tran.Data.Add(TableName, objParmList);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf(Common.KEYNAME.IPCERRORCODE) < 0)
                {
                    tran.SetErrorInfo(ex);
                }
                else
                {
                    tran.SetErrorInfo(ex.Message.Substring(ex.Message.IndexOf("=") + 1), "");
                }
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        /// <summary>
        /// Thuc hien store, tham so la mang param duoc chi ra trong parm list
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parmList"></param>
        /// <returns></returns>
        public bool DoStoreSelect(TransactionInfo tran, string parmList)
        {
            Connection con = new Connection();
            try
            {
                string[] parmlist = parmList.Split('|');
                if (parmlist.Length != 3)
                {
                    tran.SetErrorInfo(Common.ERRORCODE.INVALID_PARAM, "");
                    return false;
                }
                object[] parms = (object[])tran.Data[parmlist[1]];

                string tagName = tran.Data[parmlist[2]].ToString();
                string storeName = tran.Data[parmlist[0]].ToString();
                string keyName = Common.KEYNAME.DOSTORESELECTRESULT;
                DataSet ds = con.FillDataSet(Common.ConStr, storeName, parms);
                if (ds == null || ds.Tables.Count != 1)
                {
                    tran.SetErrorInfo(Common.ERRORCODE.INVALID_RESULT, "");
                    return false;
                }
                ds.DataSetName = Common.KEYNAME.SELECTRESULT;
                ds.Tables[0].TableName = tagName;
                if (tran.Data.ContainsKey(Common.KEYNAME.SELECTRESULT))
                {
                    tran.Data[keyName] = ds.GetXml();
                }
                else
                {
                    tran.Data.Add(keyName, ds.GetXml());
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf(Common.KEYNAME.IPCERRORCODE) < 0)
                {
                    tran.SetErrorInfo(ex);
                }
                else
                {
                    tran.SetErrorInfo(ex.Message.Substring(ex.Message.IndexOf("=") + 1), "");
                }
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
            return true;
        }

        /// <summary>
        /// Thuc hien store theo destid
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parmList"></param>
        /// <returns></returns>
        public bool DoStoreDest(TransactionInfo tran, string parmList)
        {

            DataSet ds = new DataSet();
            string conStr = string.Empty;
            try
            {
                string[] parmlist = parmList.Split('|');
                object[] parms = new object[parmlist.Length - 2];
                for (int j = 2; j < parmlist.Length; j++)
                {
                    if (tran.Data.ContainsKey(parmlist[j].ToString()))
                    {
                        parms[j - 2] = tran.Data[parmlist[j].ToString()].ToString();
                    }
                    else
                    {
                        parms[j - 2] = parmlist[j].ToString();
                    }
                }
                DataRow[] row = Common.DBICONNECTIONDB.Select("DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'");
                if (row == null || row.Length != 1)
                {
                    tran.ErrorCode = Common.ERRORCODE.INVALID_DESTID;
                    return false;
                }
                {
                    conStr = row[0]["CONNSTRING"].ToString();
                    switch (row[0]["TYPE"].ToString())
                    {
                        case Common.CONNTYPE.SQL:
                            Connection conSQL = null;
                            conSQL = new Connection();
                            ds = conSQL.FillDataSet(conStr, parmlist[0].ToString(), parms);
                            break;
                        case Common.CONNTYPE.ORACLE:
                            ConnectOracle conORA = new ConnectOracle();
                            conORA.FillDataSet(conStr, ds, parmlist[0].ToString(), parms);
                            break;
                        default:
                            tran.ErrorCode = Common.ERRORCODE.INVALID_TYPE_CONN;
                            return false;
                    }
                }
                return GetDataFromDataSet(tran, ds, parmlist[1].ToString());
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf(Common.KEYNAME.IPCERRORCODE) < 0)
                {
                    tran.SetErrorInfo(ex);
                }
                else
                {
                    tran.SetErrorInfo(ex.Message.Substring(ex.Message.IndexOf("=") + 1), "");
                }
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Thuc hien multi store
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parmList"></param>
        /// <returns></returns>
        public bool DoStoreMulti(TransactionInfo tran, string parmList)
        {
            Connection con = new Connection();
            try
            {
                string[] parmlist = parmList.Split('|');
                object[] parms = new object[parmlist.Length];

                for (int j = 0; j < parmlist.Length; j++)
                {
                    if (tran.Data.ContainsKey(parmlist[j].ToString()))
                    {
                        parms[j] = tran.Data[parmlist[j].ToString()];
                    }
                    else
                    {
                        parms[j] = parmlist[j];
                    }
                    try
                    {
                        object[] execStore = (object[])parms[j];
                        string storeName = execStore[0].ToString();
                        string listData = string.Empty;
                        try
                        {
                            listData = JsonConvert.SerializeObject(execStore[1]);
                        }
                        catch { }

                        string msglog = $"DoStoreMulti INPUT transaction {tran.IPCTransID}: KEYNAME = {parmlist[j]}, Store name = {storeName}, paramvalue:" + Environment.NewLine;
                        msglog += $"{(string.IsNullOrEmpty(listData) ? "NULL" : listData)}";

                        ProcessLog.LogInformation(msglog, Common.FILELOGTYPE.LOGDOSTOREINFO);
                    }
                    catch { }
                }

                if (con.ExecuteNonQueryMulStore(Common.ConStr, parms) == false)
                {
                    tran.ErrorCode = Common.ERRORCODE.EXECNONQUERY_FALSE;
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf(Common.KEYNAME.IPCERRORCODE) < 0)
                {
                    tran.SetErrorInfo(ex);
                    tran.ErrorCode = Common.ERRORCODE.EXECNONQUERY_FALSE;
                }
                else
                {
                    tran.SetErrorInfo(ex.Message.Substring(ex.Message.IndexOf("=") + 1), "");
                }
                try
                {
                    string msglog = $"DoStoreMulti OUTPUT transaction {tran.IPCTransID}: " + Environment.NewLine;
                    msglog += $"{ex.Message}";
                    ProcessLog.LogInformation(msglog, Common.FILELOGTYPE.LOGDOSTOREINFO);
                }
                catch { }
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                con = null;
            }
        }

        /// <summary>
        /// Ham lay gia tri cua Dataset dua vao Data theo tung field va tung dong
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="ds"></param>
        /// <param name="TypeGet">
        /// 0: Lay Data khi Key Name khong ton tai
        /// 1: Thay the Data khi Data = null hoac Data = ""
        /// 2: Luon thay the Data</param>
        /// <returns></returns>
        private bool GetDataFromDataSet(TransactionInfo tran, DataSet ds, string TypeGet)
        {
            try
            {
                switch (TypeGet)
                {
                    case "":
                        break;
                    case "0":
                        #region Lay Data khi Key Name khong ton tai
                        foreach (DataTable dt in ds.Tables)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                for (int col = 0; col < dt.Columns.Count; col++)
                                {
                                    if (tran.Data.ContainsKey(dt.Columns[col].ColumnName) == false)
                                    {
                                        tran.Data.Add(dt.Columns[col].ColumnName, dt.Rows[0][col]);
                                    }
                                    for (int row = 1; row < dt.Rows.Count; row++)
                                    {
                                        if (tran.Data.ContainsKey(dt.Columns[col].ColumnName + row.ToString()) == false)
                                        {
                                            tran.Data.Add(dt.Columns[col].ColumnName + row.ToString(), dt.Rows[row][col]);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                        #endregion
                    case "1":
                        #region Thay the Data khi Data = null hoac Data = ""
                        foreach (DataTable dt in ds.Tables)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                for (int col = 0; col < dt.Columns.Count; col++)
                                {
                                    if (tran.Data.ContainsKey(dt.Columns[col].ColumnName) == false)
                                    {
                                        tran.Data.Add(dt.Columns[col].ColumnName, dt.Rows[0][col]);
                                    }
                                    else if (tran.Data[dt.Columns[col].ColumnName] == null || tran.Data[dt.Columns[col].ColumnName].ToString() == "")
                                    {
                                        tran.Data[dt.Columns[col].ColumnName] = dt.Rows[0][col];
                                    }
                                    for (int row = 1; row < dt.Rows.Count; row++)
                                    {
                                        if (tran.Data.ContainsKey(dt.Columns[col].ColumnName + row.ToString()) == false)
                                        {
                                            tran.Data.Add(dt.Columns[col].ColumnName + row.ToString(), dt.Rows[row][col]);
                                        }
                                        else if (tran.Data[dt.Columns[col].ColumnName + row.ToString()] == null ||
                                                 tran.Data[tran.Data[dt.Columns[col].ColumnName + row.ToString()]].ToString() == "")
                                        {
                                            tran.Data[dt.Columns[col].ColumnName + row.ToString()] = dt.Rows[row][col];
                                        }
                                    }
                                }
                            }
                        }
                        break;
                        #endregion
                    case "2":
                        #region Luon thay the Data
                        foreach (DataTable dt in ds.Tables)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                for (int col = 0; col < dt.Columns.Count; col++)
                                {
                                    if (tran.Data.ContainsKey(dt.Columns[col].ColumnName) == false)
                                    {
                                        tran.Data.Add(dt.Columns[col].ColumnName, dt.Rows[0][col]);
                                    }
                                    else
                                    {
                                        tran.Data[dt.Columns[col].ColumnName] = dt.Rows[0][col];
                                    }
                                    for (int row = 1; row < dt.Rows.Count; row++)
                                    {
                                        if (tran.Data.ContainsKey(dt.Columns[col].ColumnName + row.ToString()) == false)
                                        {
                                            tran.Data.Add(dt.Columns[col].ColumnName + row.ToString(), dt.Rows[row][col]);
                                        }
                                        else
                                        {
                                            tran.Data[dt.Columns[col].ColumnName + row.ToString()] = dt.Rows[row][col];
                                        }
                                    }
                                }
                            }
                        }
                        break;
                        #endregion
                    case "3":
                        #region tra ve XML (keyName: table1,row[0][0], value: table0, tagname: table1,row[0][1]), rootname: SELECTRESULT
                        if (ds.Tables.Count != 2)
                        {
                            tran.SetErrorInfo(Common.ERRORCODE.INVALID_RESULT, "");
                            return false;
                        }
                        string keyname = string.Empty;
                        string value = string.Empty;
                        keyname = ds.Tables[1].Rows[0][0].ToString();
                        ds.Tables[0].TableName = ds.Tables[1].Rows[0][1].ToString();
                        ds.Tables.Remove(ds.Tables[1]);
                        ds.DataSetName = Common.KEYNAME.SELECTRESULT;
                        value = ds.GetXml();
                        if (tran.Data.ContainsKey(keyname) == false)
                        {
                            tran.Data.Add(keyname, value);
                        }
                        else
                        {
                            tran.Data[keyname] = value;
                        }
                        #endregion
                        break;
                    case "4":
                        #region luon luon tra ve XML cua dataset, keynam: SELECTRESULT
                        if (tran.Data.ContainsKey(Common.KEYNAME.SELECTRESULT))
                        {
                            tran.Data[Common.KEYNAME.SELECTRESULT] = ds.GetXml();
                        }
                        else
                        {
                            tran.Data.Add(Common.KEYNAME.SELECTRESULT, ds.GetXml());
                        }
                        #endregion
                        break;
                    case "5":
                        #region tra ve ket qua la dataset
                        if (tran.Data.ContainsKey(Common.KEYNAME.SELECTRESULT))
                        {
                            tran.Data[Common.KEYNAME.SELECTRESULT] = ds;
                        }
                        else
                        {
                            tran.Data.Add(Common.KEYNAME.SELECTRESULT, ds);
                        }
                        #endregion
                        break;
                    default:
                        tran.SetErrorInfo(Common.ERRORCODE.INVALID_TYPEGET, "");
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
    }
}
