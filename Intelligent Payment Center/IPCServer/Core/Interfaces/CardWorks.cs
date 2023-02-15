using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using Formatters;
using DBConnection;
using System.Data;

namespace Interfaces
{
    public class CardWorks
    {
        public bool UpdateSysvar(TransactionInfo tran, string varname, string varvalue)
        {
            Connection con = new Connection();
            try
            {
                con.ExecuteNonquery(Common.ConStr, "IPCSYSVAR_UPDATE", new object[] { varname, varvalue });
                Common.SYSVAR[varname] = varvalue;
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

        public bool UpdateSysvar(TransactionInfo tran, string ParmList)
        {
            string[] parm = ParmList.Split('|');
            Connection con = new Connection();
            try
            {
                con.ExecuteNonquery(Common.ConStr, "IPCSYSVAR_UPDATE", new object[] { parm[0], parm[1] });
                Common.SYSVAR[parm[0]] = parm[1];
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

        public bool ProcessEndOfDay(TransactionInfo tran)
        {
            DateTime begin, end;
            Connection con = new Connection();
            object result;
            try
            {
                // Get End Of Day List
                DataTable dt = con.FillDataTable(Common.ConStr, "IPCGETENDOFDAYLIST", Common.IPCWorkDate);
                // Backup Log to History
                if (dt.Rows[0]["EODSTS"].ToString() != Common.PROCESSSTATUS.FINISH)
                {
                    begin = System.DateTime.Now;
                    result = con.ExecuteScalar(Common.ConStr, "IPC_BACKUPDATALOG");
                    if (result == null || result.ToString() == "0")
                    {
                        tran.SetErrorInfo(Common.ERRORCODE.ENDOFDAYERROR, "");
                        return false;
                    }
                    end = System.DateTime.Now;
                    con.ExecuteNonquery(Common.ConStr, "IPCENDOFDAYSTS_UPDATE",
                        Common.IPCWorkDate, "1", Common.PROCESSSTATUS.FINISH, "Success",
                        begin.ToString(), end.ToString(), end.Subtract(begin).ToString());
                }
                // Reset ID
                if (dt.Rows[1]["EODSTS"].ToString() != Common.PROCESSSTATUS.FINISH)
                {
                    begin = System.DateTime.Now;
                    result = con.ExecuteScalar(Common.ConStr, "IPC_RESETID");
                    if (result == null || result.ToString() == "0")
                    {
                        tran.SetErrorInfo(Common.ERRORCODE.ENDOFDAYERROR, "");
                        return false;
                    }
                    end = System.DateTime.Now;
                    con.ExecuteNonquery(Common.ConStr, "IPCENDOFDAYSTS_UPDATE",
                        Common.IPCWorkDate, "2", Common.PROCESSSTATUS.FINISH, "Success",
                        begin.ToString(), end.ToString(), end.Subtract(begin).ToString());
                }
                tran.Data[Common.KEYNAME.NEWBUSDAY] = Common.RetrievePropertyHost("SYSTEM", "NextDate");
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }

        public bool SynchronizeTrans(TransactionInfo tran)
        {
            bool result = true;
            Connection con = new Connection();
            SocketClient socketClient = new SocketClient();
            try
            {
                // Get End Of Day List
                DataTable dataTable = con.FillDataTable(Common.ConStr, "IPCGETENDOFDAYLIST", Common.IPCWorkDate);
                if (dataTable.Rows[3]["EODSTS"].ToString() != Common.PROCESSSTATUS.FINISH)
                {
                    DateTime begin, end;
                    begin = System.DateTime.Now;
                    //CreateRequestISO
                    Formatter.CreateRequestISO(tran);
                    Common.CUSTOM["STAC"] = "0";
                    //SendMessage
                    socketClient.SendMessage(tran, "true|300000");
                    Formatter.AnalyzeResponseISO(tran);
                    if (tran.ErrorCode != Common.ERRORCODE.OK) return false;
                    // Waiting for 5 minutes
                    int count = 0;
                    while (Common.CUSTOM["STAC"].ToString() == "0" && count < 60)
                    {
                        count++;
                        System.Threading.Thread.Sleep(5000);
                    }
                    if (Common.CUSTOM["STAC"].ToString() == "0")
                    {
                        tran.SetErrorInfo(Common.ERRORCODE.MESSAGE_RECEIVE_TIMEOUT, "");
                        return false;
                    }
                    //
                    end = System.DateTime.Now;
                    con.ExecuteNonquery(Common.ConStr, "IPCENDOFDAYSTS_UPDATE",
                            Common.IPCWorkDate, "4", Common.PROCESSSTATUS.FINISH, "Success",
                            begin.ToString(), end.ToString(), end.Subtract(begin).ToString());
                }
                // Begin SynchronizeTrans
                Common.ServiceStarted = false;
                if (!UpdateSysvar(tran, Common.SYSVARNAME.SYSTEMSTATUS, Common.SYSTEMSTS.ONLINE))
                {
                    Common.ServiceStarted = true;
                    tran.SetErrorInfo(Common.ERRORCODE.SYSTEM, "");
                    return false;
                }
                DataTable dt = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM STIP02 WHERE ProcessFlag <> 'Y'");
                if (dt.Rows.Count > 0)
                {
                    DataTable FieldNameList = con.FillDataTableSQL(Common.ConStr,
                            @"SELECT * FROM ::fn_listextendedproperty 
                                    (NULL, 'user', 'dbo', 'table', 'STIP02', 'column', NULL)
                              WHERE OBJTYPE = 'COLUMN'");
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        TransactionInfo transyn = null;
                        if (CreateTrans(tran, ref transyn, FieldNameList, dt.Rows[row]))
                        {
                            ProcessTrans(transyn);
                            if (transyn.ErrorCode == Common.ERRORCODE.OK)
                            {
                                string SQL = "UPDATE STIP02 SET ProcessFlag='Y' WHERE InternalReferenceCode = '{0}'";
                                con.ExecuteNonquerySQL(Common.ConStr, String.Format(SQL, dt.Rows[row]["InternalReferenceCode"]));
                            }
                            else
                            {
                                tran.SetErrorInfo(transyn.ErrorCode, transyn.ErrorDesc);
                                result = false;
                                return false;
                            }
                        }
                        else
                        {
                            result = false;
                            return false;
                        }
                    }
                }
                string temp = con.ExecuteScalar(Common.ConStr, "STIP02_BACKUP").ToString();
                if (temp != "0")
                {
                    result = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
                return false;
            }
            finally
            {
                if (result == false)
                {
                    // Switch to Offline Mode
                    if (!UpdateSysvar(tran, Common.SYSVARNAME.SYSTEMSTATUS, Common.SYSTEMSTS.OFFLINE))
                    {
                        tran.SetErrorInfo(Common.ERRORCODE.SYSTEM, "");
                    }
                }
                Common.ServiceStarted = true;
                con = null;
                socketClient = null;
            }
            return result;
        }

        public bool NotEODSynchronizeTrans(TransactionInfo tran)
        {
            bool result = true;
            Connection con = new Connection();
            SocketClient socketClient = new SocketClient();
            try
            {
                //CreateRequestISO
                Formatter.CreateRequestISO(tran);
                Common.CUSTOM["STAC"] = "0";
                //SendMessage
                socketClient.SendMessage(tran, "true|300000");
                Formatter.AnalyzeResponseISO(tran);
                if (tran.ErrorCode != Common.ERRORCODE.OK) return false;
                // Waiting for 5 minutes
                int count = 0;
                while (Common.CUSTOM["STAC"].ToString() == "0" && count < 60)
                {
                    count++;
                    System.Threading.Thread.Sleep(5000);
                }
                if (Common.CUSTOM["STAC"].ToString() == "0")
                {
                    tran.SetErrorInfo(Common.ERRORCODE.MESSAGE_RECEIVE_TIMEOUT, "");
                    return false;
                }
                // Begin SynchronizeTrans
                Common.ServiceStarted = false;
                if (!UpdateSysvar(tran, Common.SYSVARNAME.SYSTEMSTATUS, Common.SYSTEMSTS.ONLINE))
                {
                    Common.ServiceStarted = true;
                    tran.SetErrorInfo(Common.ERRORCODE.SYSTEM, "");
                    return false;
                }
                DataTable dt = con.FillDataTableSQL(Common.ConStr, "SELECT * FROM STIP02 WHERE ProcessFlag <> 'Y'");
                if (dt.Rows.Count > 0)
                {
                    DataTable FieldNameList = con.FillDataTableSQL(Common.ConStr,
                            @"SELECT * FROM ::fn_listextendedproperty 
                                    (NULL, 'user', 'dbo', 'table', 'STIP02', 'column', NULL)
                              WHERE OBJTYPE = 'COLUMN'");
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        TransactionInfo transyn = null;
                        if (CreateTrans(tran, ref transyn, FieldNameList, dt.Rows[row]))
                        {
                            ProcessTrans(transyn);
                            if (transyn.ErrorCode == Common.ERRORCODE.OK)
                            {
                                string SQL = "UPDATE STIP02 SET ProcessFlag='Y' WHERE InternalReferenceCode = '{0}'";
                                con.ExecuteNonquerySQL(Common.ConStr, String.Format(SQL, dt.Rows[row]["InternalReferenceCode"]));
                            }
                            else
                            {
                                tran.SetErrorInfo(transyn.ErrorCode, transyn.ErrorDesc);
                                result = false;
                                return false;
                            }
                        }
                        else
                        {
                            result = false;
                            return false;
                        }
                    }
                }
                string temp = con.ExecuteScalar(Common.ConStr, "STIP02_BACKUP").ToString();
                if (temp != "0")
                {
                    result = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
                return false;
            }
            finally
            {
                if (result == false)
                {
                    // Switch to Offline Mode
                    if (!UpdateSysvar(tran, Common.SYSVARNAME.SYSTEMSTATUS, Common.SYSTEMSTS.OFFLINE))
                    {
                        tran.SetErrorInfo(Common.ERRORCODE.SYSTEM, "");
                    }
                }
                Common.ServiceStarted = true;
                con = null;
                socketClient = null;
            }
            return result;
        }

        public bool STAC(TransactionInfo tran)
        {
            Common.CUSTOM["STAC"] = "1";
            return true;
        }

        public bool NERQ(TransactionInfo tran)
        {

            return true;
        }

        private bool CreateTrans(TransactionInfo tran, ref TransactionInfo transyn, DataTable FieldNameList, DataRow FieldValueList)
        {
            try
            {
                transyn = new TransactionInfo();
                transyn.MessageTypeSource = Common.MESSAGETYPE.ISO;
                transyn.NewIPCTransID();
                // Get Message Type 
                string MsgType = FieldValueList["MessageType"].ToString();
                // Get Input Define ISO
                string filter = String.Format("MSGTYPE = '{0}' AND PROCESSCODE = '{1}'", MsgType, "");
                DataRow[] row = Common.DBIINPUTDEFINEISO.Select(filter, "FIELDNO");
                // Analyze Message
                string MapIPCTranCode = "";
                object FieldValue;
                for (int rowindex = 0; rowindex < row.Length; rowindex++)
                {
                    int FieldNo = (int)row[rowindex]["FIELDNO"];
                    // Add Field Value To Data
                    if (row[rowindex]["FIELDMAP"] != null && row[rowindex]["FIELDMAP"].ToString() != "")
                    {
                        // Get Field Value
                        DataRow[] dr = FieldNameList.Select(String.Format("value = '{0}'", row[rowindex]["FIELDMAP"]));
                        if (dr.Length > 0)
                        {
                            FieldValue = FieldValueList[dr[0]["objname"].ToString()];
                            // Format Field Value
                            Formatter.FormatFieldValue(ref FieldValue, row[rowindex]["FORMATTYPE"].ToString(), row[rowindex]["FORMATOBJECT"].ToString(),
                                                        row[rowindex]["FORMATFUNCTION"].ToString(), row[rowindex]["FORMATPARM"].ToString());
                            transyn.Data[row[rowindex]["FIELDMAP"]] = FieldValue;
                            // Check Mapping IPCTranCode
                            if ((bool)row[rowindex]["ISMAPIPCTRANCODE"])
                            {
                                MapIPCTranCode += FieldValue;
                            }
                        }
                    }
                }
                // Get Mapping IPCTranCode
                row = Common.DBIMAPIPCTRANCODEISO.Select(String.Format("MAPVALUE = '{0}'", MapIPCTranCode));
                if (row.Length > 0)
                {
                    transyn.Data[Common.KEYNAME.IPCTRANCODE] = row[0][Common.KEYNAME.IPCTRANCODE];
                }
                else
                {
                    throw new Exception(String.Format("Invalid Mapping IPCTRANCODE [{0}]", MapIPCTranCode));
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

        private void ProcessTrans(TransactionInfo transyn)
        {
            Common.IncreaseTranProcessingCount();
            // Get TranDesc
            try
            {
                DataRow[] dr = Common.DBITRANLIST.Select(String.Format("IPCTRANCODE = '{0}'",
                                        transyn.Data[Common.KEYNAME.IPCTRANCODE].ToString()));
                if (dr.Length > 0)
                {
                    transyn.Data[Common.KEYNAME.IPCTRANDESC] = dr[0][Common.KEYNAME.TRANDESC].ToString();
                    transyn.Data[Common.KEYNAME.SHORTTRANDESC] = dr[0][Common.KEYNAME.SHORTTRANDESC].ToString();
                }
            }
            catch { }
            // Add Data define
            if (!Formatter.AddDataDefine(transyn))
            {
                Common.DecreaseTranProcessingCount();
                return;
            }
            //
            Connection con = new Connection();
            try
            {
                // Execute Component
                DataTable listExec = con.FillDataTable(Common.ConStr, "IPCCOMPONENT_SELECT",
                    transyn.Data[Common.KEYNAME.SOURCEID], "", transyn.Data[Common.KEYNAME.IPCTRANCODE],
                    transyn.Data[Common.KEYNAME.REVERSAL]);
                if (listExec != null && listExec.Rows.Count > 0)
                {
                    transyn.Online = (listExec.Rows[0]["STATUS"].ToString() == Common.SYSTEMSTS.ONLINE);
                    for (int i = 0; i < listExec.Rows.Count; i++)
                    {
                        Common.ExecCom(listExec.Rows[i]["ASSEMBLYFILE"].ToString(), listExec.Rows[i]["ASSEMBLYTITLE"].ToString(),
                            listExec.Rows[i]["METHOD"].ToString(), listExec.Rows[i]["PARMLIST"].ToString(), transyn);
                    }
                }
                else
                {
                    transyn.SetErrorInfo(Common.ERRORCODE.INVALID_MESSAGE_REQUEST, "");
                }
            }
            catch (Exception ex)
            {
                transyn.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                con = null;
            }
            Common.DecreaseTranProcessingCount();
        }

        public bool ChangeWorkingDate(TransactionInfo tran)
        {
            DateTime begin, end;
            Connection con = new Connection();
            try
            {
                // Get End Of Day List
                DataTable dt = con.FillDataTable(Common.ConStr, "IPCGETENDOFDAYLIST", Common.IPCWorkDate);
                // Change Working Date
                if (dt.Rows[2]["EODSTS"].ToString() != Common.PROCESSSTATUS.FINISH)
                {
                    begin = System.DateTime.Now;
                    string NextDateSB = Common.RetrievePropertyHost("SYSTEM", "NextDate");
                    if (NextDateSB == "")
                    {
                        tran.SetErrorInfo(Common.ERRORCODE.ENDOFDAYERROR, "");
                        return false;
                    }
                    con.ExecuteNonquery(Common.ConStr, "IPCSYSVAR_UPDATE", new object[] { Common.SYSVARNAME.IPCWORKDATE, NextDateSB });
                    end = System.DateTime.Now;
                    con.ExecuteNonquery(Common.ConStr, "IPCENDOFDAYSTS_UPDATE",
                        Common.IPCWorkDate, "3", Common.PROCESSSTATUS.FINISH, "Success",
                        begin.ToString(), end.ToString(), end.Subtract(begin).ToString());
                    Common.IPCWorkDate = NextDateSB;
                    Common.SYSVAR[Common.SYSVARNAME.IPCWORKDATE] = Common.IPCWorkDate;
                }
                return true;
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
        }
    }
}