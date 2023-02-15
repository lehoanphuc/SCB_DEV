using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using DBConnection;
using Transaction;
using Utility;
using Interfaces;
using System.Reflection;
using System.Text.RegularExpressions;
using NCalc2;
using NCalc2.Expressions;

namespace Transaction
{
    public class ValidateTrans
    {
        #region public method
        public bool CheckSingleTran(TransactionInfo tran)
        {
            if (CheckTranPermission(tran))
            {
                if (CheckApproveTran(tran))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool GetFeeTran(TransactionInfo tran)
        {

            return true;
        }

        public bool UserApp(TransactionInfo tran)
        {
            Connection dbObj = new Connection();
            try
            {
                DataTable result = new DataTable();
                //string nextUserApp = string.Empty;
                List<string> lsNextUserApp = new List<string>();
                //List<string> lsCurrAppFlow = new List<string>();
                Dictionary<string, string> dicCurrAppFlow = new Dictionary<string, string>();
                //string updateCurAppFlow = string.Empty;
                string appStatus = "1";

                bool flagMatrix = false;

                DataSet resultds = dbObj.FillDataSet(Common.ConStr, "IPCAPPROVETRAN_USER", tran.Data[Common.KEYNAME.DESTTRANID],
                    tran.Data[Common.KEYNAME.USERID], tran.Data[Common.KEYNAME.AUTHENTYPE], tran.Data[Common.KEYNAME.AUTHENCODE]);

                result = resultds.Tables[0];
                if (result == null || result.Rows.Count == 0)
                {
                    tran.ErrorCode = Common.ERRORCODE.EXECNONQUERY_FALSE;
                    throw new Exception("Error execute data");
                }
                string Flag = result.Rows[0][0].ToString().Trim();

                //VuTT corp matrix 20190520
                //0: waiting for other approval
                //1: pass all approval
                //2: corp matrix approval, do check on c#
                if (Flag.Equals("2") && resultds.Tables.Count.Equals(4))
                {
                    try
                    {
                        bool exResult = false;

                        string[] currentWorkFlows = resultds.Tables[1].Rows[0]["USERCURAPP"].ToString().Split('|');
                        foreach (string curAppFlow in currentWorkFlows)
                        {
                            string[] arrAppFlow = curAppFlow.Split('#');
                            dicCurrAppFlow.Add(arrAppFlow[0], arrAppFlow[1]);
                        }
                        foreach (string curAppFlow in currentWorkFlows)
                        {
                            List<string> UserApp = resultds.Tables[1].Rows[0]["LISTUSERAPP"].ToString().Split('#').ToList();
                            if (!UserApp.Contains(tran.Data[Common.KEYNAME.USERID].ToString()))
                            {
                                UserApp.Add(tran.Data[Common.KEYNAME.USERID].ToString());
                            }
                            //Duyvk 20190807
                            //if (resultds.Tables[3].Rows[0]["IsAOT"].ToString().Trim().Equals("0"))
                            //{
                            //    UserApp.Remove(resultds.Tables[1].Rows[0]["USERID"].ToString());
                            //}
                            string[] arrAppFlow = curAppFlow.Split('#');
                            if ((resultds.Tables[3].Select($"WorkflowID = '{arrAppFlow[0].Trim()}'"))[0]["IsAOT"].ToString().Equals("0"))
                            {
                                UserApp.Remove(resultds.Tables[1].Rows[0]["USERID"].ToString());
                            }

                            List<string> UserGroupApp = new List<string>();

                            foreach (string user in UserApp)
                            {
                                DataRow[] drs = resultds.Tables[2].Select($"UserID = '{user.ToUpper().Trim()}'");
                                if (drs.Length > 0)
                                    UserGroupApp.Add(drs[0]["GROUPID"].ToString().Trim());
                            }



                            string formula = resultds.Tables[3].Select($"WorkflowID = '{arrAppFlow[0]}' AND Ord = '{arrAppFlow[1]}'")[0]["Formula"].ToString();
                            formula = formula.ToUpper().Replace("OR", "|").Replace("AND", "&").Replace("+", "&");

                            formula = Regex.Replace(formula, @"(\b[A-Z]{1}\b)", "1$1"); //Manhdd 24/04/2021
                            Regex regex = new Regex(@"[\d]{1,2}[A-Z]{1}");
                            var matches = regex.Matches(formula);

                            if (matches.Count > 0)
                            {
                                List<string> lsCon = matches.Cast<Match>().Select(x => x.Value).Distinct().ToList();

                                foreach (string match in lsCon)
                                {
                                    formula = formula.Replace(match, $"[{match}]");
                                }
                                Expression expFormula = new Expression(formula);
                                if (expFormula.HasErrors())
                                {
                                    throw new Exception("Wrong approval workflow configuration");
                                }

                                foreach (string match in lsCon)
                                {
                                    int number = int.Parse(match.Substring(0, match.Length - 1));
                                    string group = match.Substring(match.Length - 1, 1);
                                    expFormula.Parameters[match] = (UserGroupApp.Where(x => x.Equals(group)).Count() >= number);
                                }

                                exResult = Convert.ToBoolean(expFormula.Evaluate());
                                if (!exResult)
                                {
                                    List<string> groupNeedApproves = GetParamsNeedTrue(expFormula);
                                    foreach (string item in groupNeedApproves)
                                    {
                                        string group = item.Substring(item.Length - 1, 1);

                                        if (!lsNextUserApp.Contains(group))
                                        {
                                            lsNextUserApp.Add(group);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("Wrong approval workflow configuration");
                            }

                            if (exResult.Equals(true))
                            {
                                DataRow[] drApp = resultds.Tables[3].Select($"WorkflowID = '{arrAppFlow[0]}' AND Ord > {arrAppFlow[1]}", "Ord");
                                if (drApp.Length > 0)
                                {
                                    bool flagApproveLoop;
                                    do
                                    {
                                        flagApproveLoop = false;
                                        //next approval step
                                        int currentOrd = int.Parse(drApp[0]["Ord"].ToString());
                                        string newcurAppFlow = $"{drApp[0]["WorkflowID"].ToString()}#{drApp[0]["Ord"].ToString()}";
                                        dicCurrAppFlow[drApp[0]["WorkflowID"].ToString()] = drApp[0]["Ord"].ToString();
                                        string newformula = drApp[0]["Formula"].ToString();
                                        newformula = newformula.ToUpper().Replace("OR", "|").Replace("AND", "&").Replace("+", "&");

                                        newformula = Regex.Replace(newformula, @"(\b[A-Z]{1}\b)", "1$1"); //Manhdd 24/04/2021
                                        var newmatches = regex.Matches(newformula);
                                        List<string> lsNewCon = newmatches.Cast<Match>().Select(x => x.Value).Distinct().ToList();
                                        List<string> NewUserGroupApp = new List<string>();

                                        foreach (string match in lsNewCon)
                                        {
                                            newformula = newformula.Replace(match, $"[{match}]");
                                        }
                                        Expression expNewFormula = new Expression(newformula);
                                        if (expNewFormula.HasErrors())
                                        {
                                            throw new Exception("Wrong approval workflow configuration");
                                        }

                                        foreach (string match in lsNewCon)
                                        {
                                            int number = int.Parse(match.Substring(0, match.Length - 1));
                                            string group = match.Substring(match.Length - 1, 1);
                                            expNewFormula.Parameters[match] = (UserGroupApp.Where(x => x.Equals(group)).Count() >= number);
                                        }

                                        var exNewResult = Convert.ToBoolean(expNewFormula.Evaluate());
                                        // new Expression(formula).Evaluate();
                                        if (exNewResult)
                                        {
                                            drApp = resultds.Tables[3].Select($"WorkflowID = '{arrAppFlow[0]}' AND Ord > {currentOrd}", "Ord");
                                            if (drApp.Length == 0)
                                            {
                                                Flag = "1"; //post transaction to core
                                                appStatus = "3";
                                                Utility.ProcessLog.LogInformation($"Transaction {tran.Data[Common.KEYNAME.DESTTRANID]} was approved successful with formula {arrAppFlow[0]} step {currentOrd}");
                                                break;
                                            }
                                            else
                                            {
                                                flagApproveLoop = true;
                                            }
                                        }
                                        else
                                        {
                                            //TODO: send email to next level checkers
                                            List<string> groupNeedApproves = GetParamsNeedTrue(expNewFormula);
                                            foreach (string item in groupNeedApproves)
                                            {
                                                string group = item.Substring(item.Length - 1, 1);

                                                if (!lsNextUserApp.Contains(group))
                                                {
                                                    lsNextUserApp.Add(group);
                                                }
                                            }
                                        }
                                    } while (flagApproveLoop);
                                }
                                else
                                {
                                    if ((!resultds.Tables[1].Columns.Contains("CONTRACTTYPE") || !resultds.Tables[1].Rows[0]["CONTRACTTYPE"].Equals("ADV")) || (drApp.Length != 0))
                                    {
                                        Flag = "1"; //post transaction to core
                                        appStatus = "3";
                                        Utility.ProcessLog.LogInformation($"Transaction {tran.Data[Common.KEYNAME.DESTTRANID]} was approved successful with formula {arrAppFlow[0]} step {arrAppFlow[1]}");
                                        break;
                                    }
                                    else
                                    {
                                        if (!resultds.Tables[2].Select($"UserID = '{tran.Data["USERID"]}'")[0]["GroupID"].Equals("Z"))
                                        {
                                            lsNextUserApp.Add("Z");
                                            Flag = "2";
                                            appStatus = "2";
                                        }
                                        else
                                        {
                                            appStatus = "3";
                                            Flag = "1";
                                            Utility.ProcessLog.LogInformation($"Transaction {tran.Data[Common.KEYNAME.DESTTRANID]} was approved successful with formula {arrAppFlow[0]} step {arrAppFlow[1]}");
                                        }
                                
                                    }
                                }
                            }
                            //else
                            //{
                            //    if (!lsCurrAppFlow.Contains(curAppFlow))
                            //    {
                            //        lsCurrAppFlow.Add(curAppFlow);
                            //    }
                            //}
                        }
                        flagMatrix = true; // to update record in ipclogtrans
                    }
                    catch (Exception ex)
                    {
                        Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw new Exception("Wrong approval workflow configuration");
                    }
                }
                //end corp matrix
                if (Flag.Equals("1"))
                {
                    DataTable resultVal = new DataTable();
                    resultVal = dbObj.FillDataTable(Common.ConStr, "SELECTTRANWAITAPP", tran.Data[Common.KEYNAME.DESTTRANID]);
                    if (result == null || result.Rows.Count == 0)
                    {
                        tran.ErrorCode = Common.ERRORCODE.EXECNONQUERY_FALSE;
                        throw new Exception("Error execute data");
                    }
                    TransactionInfo temp = new TransactionInfo();
                    temp.MessageTypeSource = Common.MESSAGETYPE.HAS;
                    //temp = tran;
                    temp.Data = getHasData(resultVal.Rows[0]["CONTENTVALUE"].ToString());
                    temp.IPCTransID = long.Parse(temp.Data[Common.KEYNAME.IPCTRANSID].ToString());
                    if (temp.Data.ContainsKey(Common.KEYNAME.APPROVED))
                    {
                        temp.Data[Common.KEYNAME.APPROVED] = "Y";
                    }
                    else
                    {
                        temp.Data.Add(Common.KEYNAME.APPROVED, "Y");
                    }
                    if (temp.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                    {
                        temp.Data[Common.KEYNAME.ERRORCODE] = Common.ERRORCODE.OK;
                    }
                    if (temp.Data.ContainsKey(Common.KEYNAME.ERRORDESC))
                    {
                        temp.Data[Common.KEYNAME.ERRORDESC] = string.Empty;
                    }
                    temp.ErrorCode = Common.ERRORCODE.OK;
                    temp.ErrorDesc = string.Empty;

                    AutoTrans execTran = new AutoTrans();
                    execTran.ProcessTrans(temp);
                    tran.ErrorCode = temp.ErrorCode;
                    tran.ErrorDesc = temp.ErrorDesc;

                    if (!(temp.ErrorCode.Equals("0") || temp.ErrorCode.Equals("9006") || temp.ErrorCode.Equals("9005")))
                    {
                        flagMatrix = false; //if transaction successed on core or waiting other approval, this approve is correct and need to update tran record
                    }
                }
                else
                {
                    tran.ErrorCode = Common.ERRORCODE.CONTINUEWAITTING;
                }

                if (flagMatrix)
                {
                    string currentAppFlow = dicCurrAppFlow.Count.Equals(0) ? resultds.Tables[1].Rows[0]["USERCURAPP"].ToString() : string.Join("|", dicCurrAppFlow.Select(x => $"{x.Key}#{x.Value}").ToArray());
                    dbObj.FillDataSet(Common.ConStr, "IPCAPPMETRIX_UPDATETRAN", tran.Data[Common.KEYNAME.DESTTRANID],
                    tran.Data[Common.KEYNAME.USERID], tran.Data[Common.KEYNAME.AUTHENTYPE], tran.Data[Common.KEYNAME.AUTHENCODE],
                    string.Join("#", lsNextUserApp.ToArray()), currentAppFlow, appStatus);
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
                dbObj = null;
            }
            return true;
        }
        #region get user wait for approve
        public List<string> GetParamsNeedTrue(Expression ex)
        {
            NCalc2.Expressions.LogicalExpression cc = ex.ParsedExpression;
            if (cc is BinaryExpression)
            {
                var cd = (cc as BinaryExpression);
                GroupLogical gl = new GroupLogical();
                gl.exName = cd.ToString();
                gl.parName = cd.ToString();
                gl.left = cd.LeftExpression;
                gl.right = cd.RightExpression;
                gl.type = cd.Type;
                gl.Parameters = ex.Parameters;
                gl.value = (bool)ex.Evaluate();
                gl.Analysis();
                return gl.CheckNeedTrue();
            }
            else if (cc is IdentifierExpression)
            {
                var cd = (cc as IdentifierExpression);
                GroupLogical gl = new GroupLogical();
                gl.exName = cd.ToString();
                gl.parName = cd.Name;
                gl.Parameters = ex.Parameters;
                gl.value = (bool)ex.Evaluate();
                gl.Analysis();
                return gl.CheckNeedTrue();
            }
            else
            {
                throw new Exception("Cannot calculate pending approve. Formula may invalid!");
            }
        }

        public class GroupLogical
        {
            public string exName;
            public string parName;
            public LogicalExpression left;
            public LogicalExpression right;
            public GroupLogical groupleft;
            public GroupLogical groupright;
            public BinaryExpressionType type;
            public bool value;
            public Dictionary<string, object> Parameters { get; set; }
            public bool Evaluate()
            {
                Expression ex = new Expression(exName);
                ex.Parameters = Parameters;
                value = (bool)ex.Evaluate();
                return value;
            }
            public List<string> CheckNeedTrue(List<string> lstNeedTrue = null)
            {
                if (lstNeedTrue == null)
                    lstNeedTrue = new List<string>();

                bool flagleft = false;
                bool flagright = false;

                if (type == BinaryExpressionType.BitwiseOr || type == BinaryExpressionType.Or)
                {
                    if ((groupleft != null && groupleft.value == false) && (groupright != null && groupright.value == false))
                    {
                        flagleft = true;
                        flagright = true;
                    }
                    else
                    {
                        if (groupleft != null && groupleft.value == false)
                        {
                            flagleft = true;
                        }
                        if (groupright != null && groupright.value == false)
                        {
                            flagright = true;
                        }
                    }

                }
                if (type == BinaryExpressionType.BitwiseAnd || type == BinaryExpressionType.And)
                {
                    if (groupleft == null || groupleft.value == false)
                    {
                        flagleft = true;
                    }
                    if (groupright == null || groupright.value == false)
                    {
                        flagright = true;
                    }
                }
                //add node
                if (flagleft == true && groupleft == null)
                {
                    lstNeedTrue.Add(parName);
                }
                else if (flagright == true && groupright == null)
                {
                    lstNeedTrue.Add(parName);
                }

                if (groupleft != null && flagleft == true)
                    groupleft.CheckNeedTrue(lstNeedTrue);
                if (groupright != null && flagright == true)
                    groupright.CheckNeedTrue(lstNeedTrue);

                return lstNeedTrue;
            }
            public void Analysis()
            {
                if (left is BinaryExpression || left is IdentifierExpression)
                {
                    //LogicalExpression cd = null;                      
                    groupleft = new GroupLogical();
                    if (left is BinaryExpression)
                    {
                        BinaryExpression cd = (left as BinaryExpression);
                        groupleft.exName = cd.ToString();
                        groupleft.parName = cd.ToString();
                        groupleft.left = cd.LeftExpression;
                        groupleft.right = cd.RightExpression;
                        groupleft.type = cd.Type;
                    }
                    else
                    {
                        IdentifierExpression cd = (left as IdentifierExpression);
                        groupleft.exName = cd.ToString();
                        groupleft.parName = cd.Name;
                    }

                    groupleft.Parameters = Parameters;
                    groupleft.Evaluate();
                    groupleft.Analysis();
                }
                if (right is BinaryExpression || right is IdentifierExpression)
                {
                    groupright = new GroupLogical();
                    if (right is BinaryExpression)
                    {
                        BinaryExpression cd = (right as BinaryExpression);
                        groupright.exName = cd.ToString();
                        groupright.parName = cd.ToString();
                        groupright.left = cd.LeftExpression;
                        groupright.right = cd.RightExpression;
                        groupright.type = cd.Type;
                    }
                    else
                    {
                        IdentifierExpression cd = (right as IdentifierExpression);
                        groupright.exName = cd.ToString();
                        groupright.parName = cd.Name;
                    }
                    groupright.Parameters = Parameters;
                    groupright.Evaluate();
                    groupright.Analysis();
                }
            }

        }

        #endregion

        public bool TellerApp(TransactionInfo tran)
        {
            Connection dbObj = new Connection();
            try
            {
                DataTable result = new DataTable();
                result = dbObj.FillDataTable(Common.ConStr, "IPCAPPROVETRAN_TELLER", tran.Data[Common.KEYNAME.DESTTRANID],
                    tran.Data[Common.KEYNAME.USERID]);
                if (result == null || result.Rows.Count == 0)
                {
                    tran.ErrorCode = Common.ERRORCODE.EXECNONQUERY_FALSE;
                    throw new Exception("Error execute data");
                }
                if (result.Rows[0][0].ToString().Trim() == "1")
                {
                    DataTable resultVal = new DataTable();
                    resultVal = dbObj.FillDataTable(Common.ConStr, "SELECTTRANWAITAPP", tran.Data[Common.KEYNAME.DESTTRANID]);
                    if (result == null || result.Rows.Count == 0)
                    {
                        tran.ErrorCode = Common.ERRORCODE.EXECNONQUERY_FALSE;
                        throw new Exception("Error execute data");
                    }
                    TransactionInfo temp = new TransactionInfo();
                    temp.MessageTypeSource = Common.MESSAGETYPE.HAS;
                    //temp = tran;
                    temp.Data = getHasData(resultVal.Rows[0]["CONTENTVALUE"].ToString());
                    temp.IPCTransID = long.Parse(temp.Data[Common.KEYNAME.IPCTRANSID].ToString());
                    if (temp.Data.ContainsKey(Common.KEYNAME.APPROVED))
                    {
                        temp.Data[Common.KEYNAME.APPROVED] = "Y";
                    }
                    else
                    {
                        temp.Data.Add(Common.KEYNAME.APPROVED, "Y");
                    }
                    AutoTrans execTran = new AutoTrans();
                    execTran.ProcessTrans(temp);
                    tran.ErrorCode = temp.ErrorCode;
                    tran.ErrorDesc = temp.ErrorDesc;
                }
                else
                {
                    tran.ErrorCode = Common.ERRORCODE.CONTINUEWAITTING;
                    return true;
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
                dbObj = null;
            }
            return true;
        }

        public bool AuthenTran(TransactionInfo tran)
        {
            try
            {
                if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["DebugMode"]))
                    return true;
            }
            catch { }

            try
            {
                if (tran.Data.ContainsKey(Common.KEYNAME.APPROVED) && tran.Data[Common.KEYNAME.APPROVED].ToString() == "Y")
                {
                    return true;
                }
                if (tran.Data.ContainsKey(Common.KEYNAME.ISSCHEDULE) && tran.Data[Common.KEYNAME.ISSCHEDULE].ToString() == "Y")
                {
                    return true;
                }
                Connection dbObj = new Connection();
                DataTable rs = new DataTable();
                rs = dbObj.FillDataTable(Common.ConStr, "EBA_GETAUTHENINFO", tran.Data[Common.KEYNAME.USERID], tran.Data[Common.KEYNAME.AUTHENTYPE], tran.Data[Common.KEYNAME.SOURCEID]);
                if (rs == null || rs.Rows.Count == 0)
                {
                    tran.ErrorCode = Common.ERRORCODE.INVALID_AUTHENINFO;
                    tran.ErrorDesc = Common.ERRORDESC.INVALID_AUTHENINFO;
                    return false;
                }
                if (rs.Columns[0].ColumnName == "ERRORCODE" && rs.Rows[0][0].ToString() == "0")
                {
                    return true;
                }
                if (tran.Data.ContainsKey(Common.KEYNAME.AUTHENUSER))
                {
                    tran.Data[Common.KEYNAME.AUTHENUSER] = rs.Rows[0]["DEVICEID"].ToString();
                }
                else
                {
                    tran.Data.Add(Common.KEYNAME.AUTHENUSER, rs.Rows[0]["DEVICEID"].ToString());
                }
                DataRow[] exec = Common.DBIAUTHENTICATION.Select("AUTHENTYPE = '" + tran.Data[Common.KEYNAME.AUTHENTYPE].ToString() + "' AND AUTHENMETHOD = '"
                    + "AUTHENTICATION'");
                if (exec.Length == 0)
                {
                    tran.ErrorCode = Common.ERRORCODE.INVALID_AUTHENTYPE;
                    tran.ErrorDesc = Common.ERRORDESC.INVALID_AUTHENTYPE;
                    return false;
                }
                string[] parmlist = exec[0]["PARMLIST"].ToString().Split('|');
                Hashtable result;
                object[] parm = new object[parmlist.Length];
                //vutt 20180713
                Assembly assembly;
                try
                {
                    assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + exec[0]["ASSEMBLYNAME"].ToString());
                }
                catch
                {
                    assembly = Assembly.LoadFrom(exec[0]["ASSEMBLYNAME"].ToString());
                }
                //end vutt 20180713

                Type type = assembly.GetType(exec[0]["ASSEMBLYTYPE"].ToString());
                for (int i = 0; i < parmlist.Length; i++)
                {
                    if (tran.Data.ContainsKey(parmlist[i]))
                    {
                        parm[i] = tran.Data[parmlist[i]];
                    }
                    else
                    {
                        parm[i] = parmlist[i];
                    }
                }
                object instance = Activator.CreateInstance(type);
                result = (Hashtable)type.InvokeMember(exec[0]["METHODNAME"].ToString(), System.Reflection.BindingFlags.InvokeMethod, null, instance, parm);
                if (result.ContainsKey(Common.KEYNAME.ERRORCODE))
                {
                    if (result[Common.KEYNAME.ERRORCODE].ToString() != "0")
                    {
                        tran.ErrorCode = result[Common.KEYNAME.ERRORCODE].ToString();
                        tran.ErrorDesc = result[Common.KEYNAME.ERRORDESC].ToString();
                        Utility.ProcessLog.LogInformation("Token Validation Error:" + tran.ErrorDesc);
                        return false;
                    }
                }
                else
                {
                    tran.ErrorCode = Common.ERRORCODE.INVALID_AUTHENCODE;
                    tran.ErrorDesc = result.ToString();
                    Utility.ProcessLog.LogInformation("Token Validation Error:" + tran.ErrorDesc);
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                tran.ErrorDesc = "OTP Server Error!";
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }

        #endregion

        #region private method
        private bool CheckTranPermission(TransactionInfo tran)
        {
            Connection objDB = new Connection();

            try
            {
                //vutran 30/08/2015 add receiver accno
                string RevAcctno = tran.Data.ContainsKey(Common.KEYNAME.RECEIVERACCOUNT) ? tran.Data[Common.KEYNAME.RECEIVERACCOUNT].ToString() : "";

                if (!tran.Data.ContainsKey(Common.KEYNAME.APPROVED) || tran.Data[Common.KEYNAME.APPROVED].ToString() != "Y")
                {
                    objDB.ExecuteNonquery(Common.ConStr, "IPC_CHECKPERMISSION", tran.Data[Common.KEYNAME.USERID].ToString(),
                        tran.Data[Common.KEYNAME.IPCTRANCODE], tran.Data[Common.KEYNAME.ACCTNO], tran.Data[Common.KEYNAME.AMOUNT], RevAcctno);
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
                objDB = null;
            }
            return true;
        }
        public bool CheckApproveTran(TransactionInfo tran)
        {
            Connection objDB = new Connection();
            DataTable result = new DataTable();
            try
            {
                if (!tran.Data.ContainsKey(Common.KEYNAME.APPROVED) || tran.Data[Common.KEYNAME.APPROVED].ToString() != "Y")
                {
                    //vutran: hardcode approve schedule
                    if (tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("IB000215"))
                    {
                        result = objDB.FillDataTable(Common.ConStr, "IPC_CHECKTRANAPPROVE", tran.Data[Common.KEYNAME.USERID].ToString(),
                        tran.Data[Common.KEYNAME.TRANCODETORIGHT], tran.Data[Common.KEYNAME.AMOUNT], tran.Data[Common.KEYNAME.CCYID], tran.Data[Common.KEYNAME.ACCTNO]);
                    }
                    //minh add 18/5/2015 check schedule approved
                    #region Check scheduled approved
                    else if (tran.Data.ContainsKey(Common.KEYNAME.ISSCHEDULE))
                    {
                        if (tran.Data[Common.KEYNAME.ISSCHEDULE].Equals("Y"))
                        {
                            result = objDB.FillDataTable(Common.ConStr, "IPC_CHECKAPPROVED_SCHEDULE",
                                tran.Data[Common.KEYNAME.SCHEDULEID].ToString());
                        }
                    }
                    #endregion
                    else
                    {
                        result = objDB.FillDataTable(Common.ConStr, "IPC_CHECKTRANAPPROVE", tran.Data[Common.KEYNAME.USERID].ToString(),
                        tran.Data[Common.KEYNAME.IPCTRANCODE], tran.Data[Common.KEYNAME.AMOUNT], tran.Data[Common.KEYNAME.CCYID], tran.Data[Common.KEYNAME.ACCTNO]);
                    }

                    string currentApp = string.Empty;
                    if (result.Columns.Contains("CURAPPWORKFLOW"))
                    {
                        currentApp = result.Rows[0]["CURAPPWORKFLOW"].ToString();
                    }

                    if (result.Rows[0]["ERRORCODE"].ToString() == "0")
                    {
                        TransLib log = new TransLib();
                        //log.LogTransaction(tran);
                        foreach (DataColumn col in result.Columns)
                        {
                            if (!tran.Data.ContainsKey(col.ColumnName))
                            {
                                tran.Data.Add(col.ColumnName, result.Rows[0][col]);
                            }
                            else
                            {
                                tran.Data[col.ColumnName] = result.Rows[0][col];
                            }

                        }
                        tran.ErrorCode = result.Rows[0]["ERRORCODE"].ToString();
                        tran.ErrorDesc = result.Rows[0]["ERRORDESC"].ToString();

                        objDB.ExecuteNonquery(Common.ConStr, "IPCLOGTRANS_UPDATEAPPROVE", tran.Data[Common.KEYNAME.IPCTRANSID],
                            Common.IPCWorkDate, tran.Data[Common.KEYNAME.IPCTRANCODE], tran.Data[Common.KEYNAME.USERID].ToString(),
                            tran.Data[Common.KEYNAME.NEXTUSERLEV], tran.Status, tran.Data[Common.KEYNAME.APPROVESTATUS], currentApp);

                        return true;
                    }
                    else
                    {
                        TransLib log = new TransLib();
                        //log.LogTransaction(tran);
                        foreach (DataColumn col in result.Columns)
                        {
                            if (!tran.Data.ContainsKey(col.ColumnName))
                            {
                                tran.Data.Add(col.ColumnName, result.Rows[0][col]);
                            }
                            else
                            {
                                tran.Data[col.ColumnName] = result.Rows[0][col];
                            }

                        }
                        tran.ErrorCode = result.Rows[0]["ERRORCODE"].ToString();
                        tran.ErrorDesc = result.Rows[0]["ERRORDESC"].ToString();
                        if (tran.ErrorCode.Equals("9004")) //corp matrix missing approval workflow
                        {
                            tran.Status = Common.TRANSTATUS.ERROR;
                        }
                        else
                        {
                            tran.Status = Common.TRANSTATUS.WAITING_APPROVE;
                            objDB.ExecuteNonquery(Common.ConStr, "IPCLOGTRANS_UPDATEAPPROVE", tran.Data[Common.KEYNAME.IPCTRANSID],
                                Common.IPCWorkDate, tran.Data[Common.KEYNAME.IPCTRANCODE], tran.Data[Common.KEYNAME.USERID].ToString(),
                                tran.Data[Common.KEYNAME.NEXTUSERLEV], Common.TRANSTATUS.WAITING_APPROVE, tran.Data[Common.KEYNAME.APPROVESTATUS], currentApp);

                            objDB.ExecuteNonquery(Common.ConStr, "IPCLOGTRANAPPDETAIL_INSERT", tran.Data[Common.KEYNAME.IPCTRANSID],
                               Common.IPCWorkDate, getStrData(tran.Data), "N");

                            if (!string.IsNullOrEmpty(currentApp)) //send email to corp matrix
                            {
                                Interfaces.TransLib.SendApprovalNotificationEmail(tran);
                            }
                        }

                        return false;
                    }
                }
                else
                {
                    tran.Data[Common.KEYNAME.APPROVESTATUS] = Common.APPROVESTATUS.APPROVED;
                    return true;
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

        #region private method
        private string getStrData(Hashtable objInput)
        {
            try
            {
                string result = string.Empty;
                foreach (DictionaryEntry de in objInput)
                {
                    result = result + "$" + de.Key + "#" + de.Value;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Hashtable getHasData(string objInput)
        {
            Hashtable result = new Hashtable();
            string[] listKey = objInput.Split('$');
            for (int i = 0; i < listKey.Length; i++)
            {
                string[] temp = listKey[i].Split('#');
                if (temp.Length > 1)
                {
                    result.Add(temp[0], temp[1]);
                }
            }
            return result;
        }
        #endregion
    }
}
