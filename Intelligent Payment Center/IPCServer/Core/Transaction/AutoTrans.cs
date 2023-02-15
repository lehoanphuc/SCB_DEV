using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Utility;
using DBConnection;
using System.Reflection;
using System.Net;
using System.Net.Sockets;
using Formatters;

namespace Transaction
{
    public class AutoTrans : ITransaction.AutoTrans
    {
        #region Public Functions
        public override string ProcessTransXML(string InputData)
        {
            TransactionInfo tran = new TransactionInfo();
            try
            {
                tran.MessageTypeSource = Common.MESSAGETYPE.XML;
                tran.InputData = InputData;
                if (Common.ServiceStarted == false)
                {
                    tran.SetErrorInfo(Common.ERRORCODE.SERVICEINTERRUPT,
                        "Service interrupted, please try again until service is started");
                    return Formatter.CreateResponseXML(tran);
                }
                tran.NewIPCTransID();
                Common.IncreaseTranProcessingCount();
                // Log Input Data
                if (TransUtility.LogInput(tran) == false) return Formatter.CreateResponseXML(tran);
                // Analyze Message
                if (Formatter.AnalyzeRequestXML(tran) == false) return Formatter.CreateResponseXML(tran);
                // Process Trans By SourceID & TranCode
                ProcessTrans(tran);
                // Return Result
                return Formatter.CreateResponseXML(tran);
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Formatter.CreateResponseXML(tran);
            }
            finally
            {
                // Log Output Data
                if (tran.IPCTransID >= 0)
                {
                    Common.DecreaseTranProcessingCount();
                    TransUtility.LogOutput(tran);
                }
                tran = null;
            }
        }

        public override string ProcessTransISO(string InputData)
        {
            TransactionInfo tran = new TransactionInfo();
            try
            {
                tran.MessageTypeSource = Common.MESSAGETYPE.ISO;
                tran.InputData = InputData;
                if (Common.ServiceStarted == false)
                {
                    tran.SetErrorInfo(Common.ERRORCODE.SERVICEINTERRUPT,
                        "Service interrupted, please try again until service is started");
                    return Formatter.CreateResponseISO(tran);
                }
                tran.NewIPCTransID();
                Common.IncreaseTranProcessingCount();
                // Log Input Data
                if (TransUtility.LogInput(tran) == false) return Formatter.CreateResponseISO(tran);
                // Analyze Message
                if (Formatter.AnalyzeRequestISO(tran) == false) return Formatter.CreateResponseISO(tran);
                // Process Trans By SourceID & TranCode
                ProcessTrans(tran);
                // Return Result
                return Formatter.CreateResponseISO(tran);
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Formatter.CreateResponseISO(tran);
            }
            finally
            {
                // Log Output Data
                if (tran.IPCTransID >= 0)
                {
                    Common.DecreaseTranProcessingCount();
                    TransUtility.LogOutput(tran);
                }
                Utility.ProcessLog.LogATMDetail(tran.MessageInfo);
                tran = null;
            }
        }

        public override string ProcessTransSEP(string InputData)
        {
            TransactionInfo tran = new TransactionInfo();
            try
            {
                tran.MessageTypeSource = Common.MESSAGETYPE.SEP;
                tran.InputData = InputData;
                if (Common.ServiceStarted == false)
                {
                    tran.SetErrorInfo(Common.ERRORCODE.SERVICEINTERRUPT,
                        "Service interrupted, please try again until service is started");
                    return Formatter.CreateResponseSEP(tran);
                }
                tran.NewIPCTransID();
                Common.IncreaseTranProcessingCount();
                // Log Input Data
                if (TransUtility.LogInput(tran) == false) return Formatter.CreateResponseSEP(tran);
                // Analyze Message
                if (Formatter.AnalyzeRequestSEP(tran) == false) return Formatter.CreateResponseSEP(tran);
                // Process Trans By SourceID & TranCode
                ProcessTrans(tran);
                // Return Result
                return Formatter.CreateResponseSEP(tran);
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Formatter.CreateResponseSEP(tran);
            }
            finally
            {
                // Log Output Data
                if (tran.IPCTransID >= 0)
                {
                    Common.DecreaseTranProcessingCount();
                    TransUtility.LogOutput(tran);
                }
                tran = null;
            }
        }

        public override string ProcessTransSMS(string InputData)
        {
            TransactionInfo tran = new TransactionInfo();
            try
            {
                switch (Common.SYSVAR[Common.SYSVARNAME.SYSTEMSTATUS].ToString())
                {
                    case "0":
                        break;
                    case "1":
                        tran.SetErrorInfo(Common.ERRORCODE.SYSTEMBLOCK,
                                    "Blocking System for End of day Process, please try again later");
                        return Formatter.CreateResponseSMS(tran);
                    case "2":
                        break;
                    case "3":
                    default:
                        tran.SetErrorInfo(Common.ERRORCODE.SYSTEMWAITINGCOREACTIVE,
                                    "Waiting for CoreBank Active, please try again later");
                        return Formatter.CreateResponseSMS(tran);
                }

                tran.MessageTypeSource = Common.MESSAGETYPE.SMS;
                tran.InputData = InputData;
                if (Common.ServiceStarted == false)
                {
                    tran.SetErrorInfo(Common.ERRORCODE.SERVICEINTERRUPT,
                        "Service interrupted, please try again until service is started");
                    return Formatter.CreateResponseSMS(tran);
                }
                tran.NewIPCTransID();
                Common.IncreaseTranProcessingCount();
                // Log Input Data
                if (TransUtility.LogInput(tran) == false) return Formatter.CreateResponseSMS(tran);
                // Analyze Message
                if (Formatter.AnalyzeRequestSMS(tran) == false) return Formatter.CreateResponseSMS(tran);
                // Process Trans By SourceID & TranCode
                ProcessTrans(tran);
                // Return Result
                return Formatter.CreateResponseSMS(tran);
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Formatter.CreateResponseSMS(tran);
            }
            finally
            {
                // Log Output Data
                if (tran.IPCTransID >= 0)
                {
                    Common.DecreaseTranProcessingCount();
                    TransUtility.LogOutput(tran);
                }
                tran = null;
            }
        }

        public override Hashtable ProcessTransHAS(Hashtable InputData)
        {
            TransactionInfo tran = new TransactionInfo();
            try
            {
                switch (Common.SYSVAR[Common.SYSVARNAME.SYSTEMSTATUS].ToString())
                {
                    case "0":
                        break;
                    case "1":
                        tran.SetErrorInfo(Common.ERRORCODE.SYSTEMBLOCK,
                                    "Blocking System for End of day Process, please try again later");
                        return Formatter.CreateResponseHAS(tran);
                    case "2":
                        break;
                    case "3":
                    default:
                        tran.SetErrorInfo(Common.ERRORCODE.SYSTEMWAITINGCOREACTIVE,
                                    "Waiting for CoreBank Active, please try again later");
                        return Formatter.CreateResponseHAS(tran);
                }
                tran.MessageTypeSource = Common.MESSAGETYPE.HAS;
                if (Common.ServiceStarted == false)
                {
                    tran.SetErrorInfo(Common.ERRORCODE.SERVICEINTERRUPT,
                        "Service interrupted, please try again until service is started");
                    return Formatter.CreateResponseHAS(tran);
                }

                tran.NewIPCTransID();
                Common.IncreaseTranProcessingCount();
                // Analyze Message
                if (Formatter.AnalyzeRequestHAS(tran, InputData) == false) return Formatter.CreateResponseHAS(tran);
                // Log Input Data
                if (TransUtility.LogInput(tran) == false) return Formatter.CreateResponseHAS(tran);
                //Check duplicate transaction - vutran 24072015
                if (TransUtility.CheckDuplicateTran(tran, InputData) == false) return Formatter.CreateResponseHAS(tran);
                // VuTT check session
                if (!TransUtility.CheckSession(tran)) return Formatter.CreateResponseHAS(tran);
                if (!TransUtility.CheckUserPermission(tran)) return Formatter.CreateResponseHAS(tran);
                if (!TransUtility.CheckSqlInjection(tran)) return Formatter.CreateResponseHAS(tran);
                
                // Process Trans By SourceID & TranCode
                ProcessTrans(tran);
                // Return Result
                return Formatter.CreateResponseHAS(tran);
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Formatter.CreateResponseHAS(tran);
            }
            finally
            {
                // Log Output Data
                if (tran.IPCTransID >= 0)
                {
                    Common.DecreaseTranProcessingCount();
                    TransUtility.LogOutput(tran);
                }
                tran = null;
            }
        }

        public override Hashtable ProcessOnlyHAS(Hashtable InputData)
        {
            TransactionInfo tran = new TransactionInfo();
            try
            {
                switch (Common.SYSVAR[Common.SYSVARNAME.SYSTEMSTATUS].ToString())
                {
                    case "0":
                        break;
                    case "1":
                        tran.SetErrorInfo(Common.ERRORCODE.SYSTEMBLOCK,
                                    "Blocking System for End of day Process, please try again later");
                        return Formatter.CreateResponseHAS(tran);
                    case "2":
                        break;
                    case "3":
                    default:
                        tran.SetErrorInfo(Common.ERRORCODE.SYSTEMWAITINGCOREACTIVE,
                                    "Waiting for CoreBank Active, please try again later");
                        return Formatter.CreateResponseHAS(tran);
                }

                tran.MessageTypeSource = Common.MESSAGETYPE.HAS;
                if (Common.ServiceStarted == false)
                {
                    tran.SetErrorInfo(Common.ERRORCODE.SERVICEINTERRUPT,
                        "Service interrupted, please try again until service is started");
                    return Formatter.CreateResponseHAS(tran);
                }
                Common.IncreaseTranProcessingCount();
                // Analyze Message
                if (Formatter.AnalyzeRequestHAS(tran, InputData) == false) return Formatter.CreateResponseHAS(tran);
                // Process Trans By SourceID & TranCode
                ProcessTrans(tran);
                // Return Result
                return Formatter.CreateResponseHAS(tran);
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Formatter.CreateResponseHAS(tran);
            }
            finally
            {
                Common.DecreaseTranProcessingCount();
                tran = null;
            }
        }


        public override bool SynchronizeTrans(ref Hashtable InputData)
        {
            TransactionInfo tran = new TransactionInfo();
            Connection con = new Connection();
            try
            {
                tran.IPCTransID = long.Parse(InputData[Common.KEYNAME.IPCTRANSID].ToString());
                tran.Data[Common.KEYNAME.IPCTRANCODE] = InputData[Common.KEYNAME.IPCTRANCODE].ToString();
                tran.Data[Common.KEYNAME.TRANDESC] = InputData[Common.KEYNAME.TRANDESC].ToString();
                tran.Data[Common.KEYNAME.SOURCEID] = InputData[Common.KEYNAME.SOURCEID].ToString();
                tran.Data[Common.KEYNAME.SOURCETRANREF] = InputData[Common.KEYNAME.SOURCETRANREF].ToString();
                tran.Data[Common.KEYNAME.DESTID] = InputData[Common.KEYNAME.DESTID].ToString();
                tran.Data[Common.KEYNAME.DESTTRANREF] = InputData[Common.KEYNAME.DESTTRANREF].ToString();
                tran.Data[Common.KEYNAME.OFFLSTS] = Common.OFFLSTS.BEGINSYN;
                // Get Log Transaction Detail
                if (TransUtility.GetLogTranDetailInputSyn(tran) == false) return false;
                // Add DataDefine
                if (TransUtility.AddDataDefine(tran) == false) return false;
                // Router
                DataTable listExec = con.FillDataTable(Common.ConStr, "IPCCOMPONENTSYN_SELECT",
                        tran.Data[Common.KEYNAME.SOURCEID], tran.Data[Common.KEYNAME.DESTID],
                        tran.Data[Common.KEYNAME.IPCTRANCODE]);
                //
                if (listExec != null && listExec.Rows.Count > 0)
                {
                    for (int i = 0; i < listExec.Rows.Count; i++)
                    {
                        if (Common.ExecCom(listExec.Rows[i]["ASSEMBLYFILE"].ToString(), listExec.Rows[i]["ASSEMBLYTITLE"].ToString(),
                            listExec.Rows[i]["METHOD"].ToString(), listExec.Rows[i]["PARMLIST"].ToString(), tran) == false) return false;
                    }
                }
                else
                {
                    throw new Exception("The system status does not allow to synchronize offline transaction");
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            finally
            {
                // Change Status Transaction
                TransUtility.ChangeTranOfflineStatus(tran);
                // Update Transaction
                TransUtility.UpdateTransactionOffline(tran);
                //
                InputData.Add(Common.KEYNAME.ERRORCODE, tran.ErrorCode);
                InputData.Add(Common.KEYNAME.ERRORDESC, tran.ErrorDesc);
                tran = null;
                con = null;
            }
            return true;
        }
        #endregion

        #region Private Functions
        public void ProcessTrans(TransactionInfo tran)
        {
            //Send Message Trans to Display Transactions
            BroadCastInformationToClient(tran);
            // Get TranDesc
            try
            {
                DataRow[] dr = Common.DBITRANLIST.Select(String.Format("IPCTRANCODE = '{0}'",
                                        tran.Data[Common.KEYNAME.IPCTRANCODE].ToString()));
                if (dr.Length > 0)
                {
                    tran.Data[Common.KEYNAME.IPCTRANDESC] = dr[0][Common.KEYNAME.TRANDESC].ToString();
                    tran.Data[Common.KEYNAME.SHORTTRANDESC] = dr[0][Common.KEYNAME.SHORTTRANDESC].ToString();
                }
            }
            catch { }
            // Add Data define
            if (!Formatter.AddDataDefine(tran)) return;
            //
            Connection con = new Connection();
            try
            {
                // Execute Component
                DataTable listExec = con.FillDataTable(Common.ConStr, "IPCCOMPONENT_SELECT",
                    tran.Data[Common.KEYNAME.SOURCEID], "", tran.Data[Common.KEYNAME.IPCTRANCODE],
                    tran.Data[Common.KEYNAME.REVERSAL]);
                if (listExec != null && listExec.Rows.Count > 0)
                {
                    tran.Online = (listExec.Rows[0]["STATUS"].ToString() == Common.SYSTEMSTS.ONLINE);
                    string ORD = "0";
                    for (int i = 0; i < listExec.Rows.Count; i++)
                    {
                        //check condition
                        if (!Utility.Common.CheckCondition(tran, listExec.Rows[i]["CONDITION"].ToString()))
                        {
                           // ORD = listExec.Rows[i]["SU"].ToString();
                            continue;
                        }

                        if (ORD == "0" || ORD == listExec.Rows[i]["ORD"].ToString())
                        {
                            if (Common.ExecCom(listExec.Rows[i]["ASSEMBLYFILE"].ToString(), listExec.Rows[i]["ASSEMBLYTITLE"].ToString(),
                                listExec.Rows[i]["METHOD"].ToString(), listExec.Rows[i]["PARMLIST"].ToString(), tran))
                            {
                                ORD = listExec.Rows[i]["SU"].ToString();
                            }
                            else
                            {
                                ORD = listExec.Rows[i]["FA"].ToString();

                                //vutran add log
                                Utility.ProcessLog.LogInformation(string.Format("Transaction {0} was failed on step {1}, method {2}", tran.IPCTransID.ToString(), listExec.Rows[i]["ORD"].ToString(), listExec.Rows[i]["METHOD"].ToString()));
                            }
                            if (ORD == "-1") break;
                        }
                    }
                }
                else
                {
                    object status = con.ExecuteScalarSQL(Common.ConStr,
                                        "SELECT VARVALUE FROM IPCSYSVAR WHERE VARNAME = '"
                                        + Common.SYSVARNAME.SYSTEMSTATUS + "'");
                    if (status == null) throw new Exception("The system status has not defined");
                    switch (status.ToString())
                    {
                        case "1": // Block to synchronize account balance
                            throw new Exception("The system is blocked to synchronize account balance");
                        case "3": // Block to synchronize transaction
                            throw new Exception("The system is blocked to synchronize transaction");
                        default:
                            throw new Exception("The transaction is not supported or defined");
                    }
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                con = null;
            }
        }

        private void BroadCastInformationToClient(TransactionInfo tran)
        {
            try
            {

            }
            catch (SocketException se)
            {
                Utility.ProcessLog.LogError(se, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        #endregion
    }
}