using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using Interfaces;
using System.Collections;
using DBConnection;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Interfaces.wsChoLon;
using Microsoft.VisualBasic.Logging;

namespace Transaction
{
    class MBProcess
    {
        AutoTrans Autotrans = new AutoTrans();

        public bool UpdateAllAccount(TransactionInfo tran)
        {
            //Get TK tu DB
            string errorCode = string.Empty;
            string errorDesc = string.Empty;
            string AccountList = string.Empty;
            string CfCode = string.Empty;
            string CFType = string.Empty;
            DataSet dsAcctEB = new DataSet();
            DataSet dsAcctCore = new DataSet();
            DataSet dsCustInfo = new DataSet();
            //SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
            dsAcctEB = getAccount(tran.Data["USERID"].ToString(), "IB000200", "", ref errorCode, ref errorDesc);
            DataRow[] dr;
            if (errorCode == "0")
            {
                dsCustInfo = GetCustIDCustType(tran.Data["USERID"].ToString(), ref errorCode, ref errorDesc);
                if (errorCode == "0" && dsCustInfo.Tables[0].Rows.Count == 1)
                {
                    CfCode = dsCustInfo.Tables[0].Rows[0]["CFCODE"].ToString().Replace(" ", "");
                    CFType = dsCustInfo.Tables[0].Rows[0]["CFTYPE"].ToString().Replace(" ", "");
                    for (int i = 0; i < dsAcctEB.Tables[0].Rows.Count; i++)
                    {
                        if (i == dsAcctEB.Tables[0].Rows.Count - 1)
                            AccountList = AccountList + "'" + dsAcctEB.Tables[0].Rows[i]["ACCTNO"].ToString().Trim() + "'";
                        else
                            AccountList = AccountList + "'" + dsAcctEB.Tables[0].Rows[i]["ACCTNO"].ToString().Trim() + "',";
                    }
                    dsAcctCore = GetTKKH(CfCode.Replace(" ", ""), CFType.Replace(" ", ""), ref errorCode, ref errorDesc);
                    //
                    if (dsAcctCore != null && dsAcctCore.Tables.Count > 0)
                    {
                        //dr = dsAcctCore.Tables[0].Select("accountno not in (" + AccountList + ") and statuscd not in" + "('CLS')");
                        if (String.IsNullOrEmpty(AccountList) == false)
                        {
                            dr = dsAcctCore.Tables[0].Select("accountno not in (" + AccountList + ")");
                            for (int j = 0; j < dr.Length; j++)
                            {
                                InsertNewAcct(dr[j]["accountno"].ToString(), dr[j]["typeid"].ToString(), dr[j]["ccyid"].ToString()
                                    , FormatStringCore(dr[j]["chinhanh"].ToString()), dr[j]["statuscd"].ToString(), CfCode
                                    , CFType, ref errorCode, ref errorDesc);
                            }
                        }
                        //thaity modify at 27/6/2014
                        //update all status in all account , not CLS
                        //dr = dsAcctCore.Tables[0].Select("statuscd in" + "('CLS')");
                        dr = dsAcctCore.Tables[0].Select("statuscd in" + "('CLS')");
                        for (int k = 0; k < dr.Length; k++)
                        {
                            UpdateCloseAcct(dr[k]["accountno"].ToString(), dr[k]["statuscd"].ToString(), ref errorCode, ref errorDesc);
                        }

                        dr = dsAcctCore.Tables[0].Select("");
                        for (int k = 0; k < dr.Length; k++)
                        {
                            UpdateAcct(dr[k]["accountno"].ToString(), dr[k]["typeid"].ToString(), dr[k]["statuscd"].ToString(), dr[k]["ccyid"].ToString()
                                , FormatStringCore(dr[k]["chinhanh"].ToString()), CfCode
                                , CFType, ref errorCode, ref errorDesc);
                        }
                    }
                }
                if (errorCode == "0")
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

        #region GET ACCOUNT FROM USERID
        public DataSet getAccount(string userID, string trancodeToRight, string acctType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(Common.KEYNAME.IPCTRANCODE, "IB000200");
                hasInput.Add(Common.KEYNAME.SOURCEID, "IB");
                hasInput.Add(Common.KEYNAME.USERID, userID);
                hasInput.Add(Common.KEYNAME.TRANCODETORIGHT, trancodeToRight);
                hasInput.Add(Common.KEYNAME.ACCTTYPE, acctType);
                hasOutput = Autotrans.ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();
                if (hasOutput[Common.KEYNAME.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[Common.KEYNAME.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[Common.KEYNAME.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[Common.KEYNAME.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Dong bo Online
        // Get Tai khoan khach hang
        public DataSet GetTKKH(string CustCode, string CustType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(Common.KEYNAME.IPCTRANCODE, "IB000117");
                hasInput.Add(Common.KEYNAME.SOURCEID, "IB");
                hasInput.Add(Common.KEYNAME.CUSTCODE, CustCode);
                hasInput.Add(Common.KEYNAME.CUSTTYPE, CustType);
                hasOutput = Autotrans.ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[Common.KEYNAME.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[Common.KEYNAME.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[Common.KEYNAME.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[Common.KEYNAME.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertNewAcct(string Account, string AcctType, string Currency, string BranchID, string Status, string CustCode, string CustType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(Common.KEYNAME.IPCTRANCODE, "IB000118");
                hasInput.Add(Common.KEYNAME.SOURCEID, "IB");
                hasInput.Add(Common.KEYNAME.ACCTNO, Account);
                hasInput.Add(Common.KEYNAME.ACCTTYPE, AcctType);
                hasInput.Add(Common.KEYNAME.CCYID, Currency);
                hasInput.Add(Common.KEYNAME.BRANCHID, BranchID);
                hasInput.Add(Common.KEYNAME.STATUS, Status);
                hasInput.Add(Common.KEYNAME.CUSTCODE, CustCode);
                hasInput.Add(Common.KEYNAME.CUSTTYPE, CustType);
                hasOutput = Autotrans.ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[Common.KEYNAME.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[Common.KEYNAME.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[Common.KEYNAME.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[Common.KEYNAME.IPCERRORDESC].ToString();
                }

                //return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateCloseAcct(string Account, string Status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(Common.KEYNAME.IPCTRANCODE, "IB000119");
                hasInput.Add(Common.KEYNAME.IPCTRANCODE, "IB");
                hasInput.Add(Common.KEYNAME.ACCTNO, Account);
                hasInput.Add(Common.KEYNAME.STATUS, Status);
                hasOutput = Autotrans.ProcessTransHAS(hasInput);

                //DataSet ds = new DataSet();

                if (hasOutput[Common.KEYNAME.IPCERRORCODE].ToString() == "0")
                {
                    //ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[Common.KEYNAME.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[Common.KEYNAME.IPCERRORDESC].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateAcct(string Account, string AcctType, string status, string Currency, string BranchID, string CustCode, string CustType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(Common.KEYNAME.IPCTRANCODE, "IB000501");
                hasInput.Add(Common.KEYNAME.SOURCEID, "IB");
                hasInput.Add(Common.KEYNAME.ACCTNO, Account);
                hasInput.Add(Common.KEYNAME.ACCTTYPE, AcctType);
                hasInput.Add(Common.KEYNAME.STATUS, status);
                hasInput.Add(Common.KEYNAME.CCYID, Currency);
                hasInput.Add(Common.KEYNAME.BRANCHID, BranchID);
                hasInput.Add(Common.KEYNAME.CUSTCODE, CustCode);
                hasInput.Add(Common.KEYNAME.CUSTTYPE, CustType);
                hasOutput = Autotrans.ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[Common.KEYNAME.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[Common.KEYNAME.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[Common.KEYNAME.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[Common.KEYNAME.IPCERRORDESC].ToString();
                }

                //return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetCustIDCustType(string UserID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(Common.KEYNAME.IPCTRANCODE, "IB000125");
                hasInput.Add(Common.KEYNAME.SOURCEID, "IB");
                hasInput.Add(Common.KEYNAME.USERID, UserID);
                hasOutput = Autotrans.ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[Common.KEYNAME.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[Common.KEYNAME.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[Common.KEYNAME.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[Common.KEYNAME.IPCERRORDESC].ToString();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static string FormatStringCore(string input)
        {
            //string oP= input.Split('.')[0];
            //return oP;

            return input;

        }
        public bool GetBalanceOfAccount(TransactionInfo tran)
        {
            try
            {
                DataSet dsInputAccount = new DataSet();
                DataTable dtInputAccount = new DataTable();
                if (!tran.ErrorCode.Equals("0") || (tran.Data.ContainsKey(Common.KEYNAME.ERRORCODE) && !tran.Data[Common.KEYNAME.ERRORCODE].ToString().Equals("0")))
                {
                    Exception ex = new Exception("Core banking is down");
                    Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                    tran.ErrorCode = "0";
                    if (tran.Data.ContainsKey(Common.KEYNAME.ERRORCODE))
                        tran.Data[Common.KEYNAME.ERRORCODE] = "0";
                }

                if (tran.Data[Common.KEYNAME.DATARESULT] == null || ((DataSet)tran.Data[Common.KEYNAME.DATARESULT]).Tables.Count == 0 || ((DataSet)tran.Data[Common.KEYNAME.DATARESULT]).Tables[0].Rows.Count == 0)
                {
                    dtInputAccount.Columns.Add("ACCOUNTNO");
                    dtInputAccount.Columns.Add("STATUSCD");
                    dtInputAccount.Columns.Add("CCYID");
                    dtInputAccount.Columns.Add("BRID");
                    dtInputAccount.Columns.Add("ACCOUNTNAME");
                    dtInputAccount.Columns.Add("TYPEID");
                    dtInputAccount.Columns.Add("BALANCE");
                    dtInputAccount.Columns.Add("AVAILABLEBAL");
                    dtInputAccount.Columns.Add("UNIQUEID");
                    dtInputAccount.Columns.Add("ORD");
                }
                else
                {
                    dtInputAccount = ((DataSet)tran.Data[Common.KEYNAME.DATARESULT]).Tables[0];
                }
                dtInputAccount.Columns.Add("POCKETAMT");
                dtInputAccount.Columns.Add("ISSHOWBALANCE");

                if(dtInputAccount.Rows.Count > 0)
                {
                    foreach (DataRow row in dtInputAccount.Rows)
                    {
                        row["POCKETAMT"] = "0";
                        row["ISSHOWBALANCE"] = tran.Data.ContainsKey("ISSHOWBALANCE") ? tran.Data["ISSHOWBALANCE"].ToString() : "1";
                    }
                }
                

                Connection con = new Connection();

                DataRow[] drWl = null;
                if (tran.Data.ContainsKey("LISTWLACCT"))
                {
                    drWl = (DataRow[])tran.Data["LISTWLACCT"];
                }
                else
                {
                    DataTable dtAceptAccount = con.FillDataTable(Common.ConStr, "MB_GETACCTRIGHT", tran.Data[Common.KEYNAME.SERVICEID], tran.Data[Common.KEYNAME.USERID], tran.Data[Common.KEYNAME.TRANCODETORIGHT], tran.Data[Common.KEYNAME.ACCTTYPE], tran.Data[Common.KEYNAME.CCYID], tran.Data[Common.KEYNAME.TRANCODEMORE]);

                    if (!dtInputAccount.Columns.Contains("UNIQUEID"))
                    {
                        dtInputAccount.Columns.Add("UNIQUEID");
                    }

                    drWl = dtAceptAccount.Select("AcctType like 'WL%'", "AcctType");
                }

                if (drWl != null && drWl.Count() > 0)
                {
                    foreach (DataRow wlRow in drWl)
                    {
                        DataRow wl = dtInputAccount.NewRow();
                        wl["UNIQUEID"] = wlRow["UNIQUEID"];
                        wl["accountno"] = wlRow["ACCTNO"];
                        wl["typeid"] = wlRow["AcctType"];
                        wl["statuscd"] = wlRow["Status"];
                        wl["accountname"] = wlRow["ACCTNO"];
                        wl["balance"] = wlRow["BALANCE"];
                        wl["availablebal"] = wlRow["BALANCE"];
                        wl["ccyid"] = wlRow["CCYID"];
                        wl["POCKETAMT"] = wlRow["POCKETAMT"];
                        wl["ISSHOWBALANCE"] = tran.Data.ContainsKey("ISSHOWBALANCE") ? tran.Data["ISSHOWBALANCE"].ToString() : "1";
                        wl["ORD"] = wlRow["ORD"];
                        dtInputAccount.Rows.InsertAt(wl, 0);
                    }
                }

                if (dtInputAccount.Rows.Count > 0)
                {
                    DataTable dtAccount = dtInputAccount.Select("", "ORD").CopyToDataTable();
                    dsInputAccount.Tables.Add(dtAccount);
                }
                else
                {
                    tran.SetErrorInfo("9090", "You don’t have any available accounts");
                    return false;
                }

                Common.HashTableAddOrSet(tran.Data, Common.KEYNAME.DATARESULT, dsInputAccount);
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                return false;
            }

        }
        public bool ENCRYPPASSWORD(TransactionInfo tran, string user = Common.KEYNAME.USERID)
        {
            bool ret = false;
            try
            {
                //for biometric
                if (string.IsNullOrEmpty(tran.Data[Common.KEYNAME.PASSWORD].ToString()))
                {
                    return true;
                }

                if (tran.Data.ContainsKey(user) && tran.Data.ContainsKey(Common.KEYNAME.PASSWORD))
                {
                    //tran.Data[Common.KEYNAME.PASSWORD] = Common.Decryptpassword(tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    //ProcessLog.LogInformation("==========pass=" + tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    //ProcessLog.LogInformation("==========user=" + tran.Data[Common.KEYNAME.USERID].ToString());
                    tran.Data[Common.KEYNAME.PASSWORD] = Utility.O9Encryptpass.sha_sha256(tran.Data[Common.KEYNAME.PASSWORD].ToString(), tran.Data[user].ToString());
                    ProcessLog.LogInformation("==========passnew=" + tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    ret = true;
                }
                if (tran.Data.ContainsKey(user) && tran.Data.ContainsKey(Common.KEYNAME.NEWPASS))
                {
                    //tran.Data[Common.KEYNAME.NEWPASS] = Common.Decryptpassword(tran.Data[Common.KEYNAME.NEWPASS].ToString());
                    //ProcessLog.LogInformation("==========passnew=" + tran.Data[Common.KEYNAME.NEWPASS].ToString());
                    tran.Data[Common.KEYNAME.NEWPASS] = Utility.O9Encryptpass.sha_sha256(tran.Data[Common.KEYNAME.NEWPASS].ToString(), tran.Data[user].ToString());
                    ProcessLog.LogInformation("==========passnewenc=" + tran.Data[Common.KEYNAME.NEWPASS].ToString());
                    ret = true;
                }

            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                ret = false;
            }
            return ret;
        }
        public bool VALIDATEPASSWORD(TransactionInfo tran)
        {
            try
            {
                tran.Data[Common.KEYNAME.PASSWORD] = Common.Decryptpassword(tran.Data[Common.KEYNAME.PASSWORD].ToString());
                if (tran.Data.ContainsKey(Common.KEYNAME.USERID) && tran.Data.ContainsKey(Common.KEYNAME.NEWPASS))
                {
                    if (CHECKNEWPASSWORD(tran))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                return false;
            }
        }
        public static bool hasLowerCharacter(string str)
        {
            bool result;
            for (int i = 0; i <= str.Length - 1; i++)
            {
                if (char.IsLower(str[i]))
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }

        public static bool hasUpperCharacter(string str)
        {
            bool result;
            for (int i = 0; i <= str.Length - 1; i++)
            {
                if (char.IsUpper(str[i]))
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }

        public static bool hasNumberCharacter(string str)
        {
            bool result;
            for (int i = 0; i <= str.Length - 1; i++)
            {
                if (char.IsDigit(str[i]))
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }

        public static bool hasSymbolCharacter(string str)
        {
            string symbol = "`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?";
            bool result;
            for (int i = 0; i <= str.Length - 1; i++)
            {
                if (symbol.IndexOf(str[i]) >= 0)
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }

        private bool checklogin(TransactionInfo tran)
        {
            bool checkexpirepolicy = false;
            Connection con = new Connection();
            string UserName = string.Empty;
            string Password = string.Empty;
            string Status = string.Empty;
            int Failnumber = 0;
            DateTime SystemTime = DateTime.Now;
            int pwdage = 0;
            int lastlogin = 0;
            try
            {
                string error = string.Empty;
                string username = tran.Data[Common.KEYNAME.USERID].ToString();
                string password = tran.Data[Common.KEYNAME.PASSWORD].ToString();
                string typepass = tran.Data[Common.KEYNAME.LGAUTHENTYPE].ToString();
                string sourceid = tran.Data.Contains(Common.KEYNAME.SOURCEID) ? tran.Data[Common.KEYNAME.SOURCEID].ToString() : Common.KEYNAME.MB;

                DataTable dtpolicy = con.FillDataTable(Common.ConStr, "Users_Policy_by_userid", sourceid, username);
                DataTable dtuser = con.FillDataTable(Common.ConStr, "IB_getaccountinfo", sourceid, username, typepass);

                if (dtuser.Rows.Count == 0)
                {
                    error = "Username not exists";
                    tran.SetErrorInfo("5001", error);
                    Utility.ProcessLog.LogInformation("Check Login Pass Error in 1 : " + error);
                    return false;
                }
                else
                {
                    UserName = dtuser.Rows[0]["userid"].ToString().Trim();
                    Password = dtuser.Rows[0]["passwordnew"].ToString().Trim();
                    Status = dtuser.Rows[0]["status"].ToString().Trim();
                    Failnumber = (int)dtuser.Rows[0]["failnumber"];
                    SystemTime = (DateTime)dtuser.Rows[0]["systemtime"];

                    pwdage = Convert.ToInt32(dtuser.Rows[0]["pwdage"].ToString());
                    lastlogin = Convert.ToInt32(dtuser.Rows[0]["lastlogin"].ToString());
                }

                if (dtpolicy.Rows.Count == 0)
                {
                    error = "No exists Policy for this user" + dtuser.Rows[0]["userid"].ToString();

                    Utility.ProcessLog.LogInformation("Check Login Pass Error in 2 : " + error);
                    tran.SetErrorInfo("5001", error);
                    return false;

                }
                else  //co tai khoan dang ky
                {
                    string pass = Utility.O9Encryptpass.sha_sha256(password, username);
                    if (Utility.O9Encryptpass.sha_sha256(password, username) != Password)
                    {
                        if (Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]) > 0)
                        {
                            if (Failnumber >= Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]))
                            {
                                if (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) == 0) // not auto reset failed number
                                {
                                    error = "Your account has been disable because of fail login times is over regulations of Bank.";

                                    Utility.ProcessLog.LogInformation("Check Login Pass Error in 3: " + error);
                                    tran.SetErrorInfo("5001", error);
                                    return false;
                                }
                                else // auto reset failed number
                                {
                                    if (lastlogin <= Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]))
                                    {
                                        error = string.Format("Your account has been disable because of fail login times is over regulations of Bank. Please try again after {0} minutes.", (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) - lastlogin).ToString());
                                        Utility.ProcessLog.LogInformation("Check Login Pass Error in 4 : " + error);
                                        tran.SetErrorInfo("5001", error);
                                        return false;
                                    }
                                    else
                                    {
                                        error = "Your account has been disable because of fail login times is over regulations of Bank.";

                                        Utility.ProcessLog.LogInformation("Check Login Pass Error in 5: " + error);
                                        tran.SetErrorInfo("5001", error);
                                        return false;
                                    }
                                }

                            }
                            else
                            {
                                //add failnumber to db
                                DataTable updatefailnumber = con.FillDataTable(Common.ConStr, "MB_UpdateFailLogin", username, string.Empty, sourceid);
                                error = string.Format("Your Username or Password is not correct. Please note your account will be temporary disable if you fail to login {0} times.", dtpolicy.Rows[0]["llkoutthrs"].ToString());
                                if (typepass == "PASSWORD")
                                {
                                    tran.SetErrorInfo("5001", error);
                                }
                                else
                                {
                                    tran.SetErrorInfo("5002", error);
                                }

                                Utility.ProcessLog.LogInformation("Check Login Pass Error in 6: " + error);
                                return false;
                            }
                        }
                        else
                        {
                            if (typepass == "PASSWORD")
                            {
                                error = "Your Username or Password is not correct.";
                                tran.SetErrorInfo("5001", error);
                            }
                            else
                            {
                                error = "Your Username or Password is not correct.";
                                tran.SetErrorInfo("5002", error);
                            }
                        }
                        Utility.ProcessLog.LogInformation("Check Login Pass Error in 7: " + error);
                        return false;
                    }
                    else
                    {
                        // check policy
                        #region CHECK POLICY
                        if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare(((DateTime)dtpolicy.Rows[0]["efto"]).Date, ((DateTime)dtpolicy.Rows[0]["systemtime"]).Date) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
                        {
                            checkexpirepolicy = true;
                            //check time login
                            if (bool.Parse(dtpolicy.Rows[0]["timelginrequire"].ToString()))
                            {
                                if (dtpolicy.Rows[0]["timelginOK"].ToString() == "0")
                                {
                                    ProcessLog.LogInformation("timelginOK" + dtpolicy.Rows[0]["timelginOK"].ToString());
                                    ProcessLog.LogInformation("lginfr" + dtpolicy.Rows[0]["lginfr"].ToString());
                                    ProcessLog.LogInformation("lginto" + dtpolicy.Rows[0]["lginto"].ToString());
                                    error = string.Format("This time not allow to login. Please login in allowed time from {0} to {1}", dtpolicy.Rows[0]["lginfr"].ToString(), dtpolicy.Rows[0]["lginto"].ToString());

                                    Utility.ProcessLog.LogInformation("Check Login Policy Error in 1: " + error);
                                    return false;
                                }
                            }
                            //check password age
                            if ((int)dtpolicy.Rows[0]["pwdagemax"] > 0)
                            {
                                if ((int)dtpolicy.Rows[0]["pwdagemax"] - pwdage < 0)
                                {
                                    error = "Password age max has been reached. Please change password to login again.";
                                    //return false;
                                }
                            }

                            //check failed login
                            if (Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]) > 0)
                            {
                                if (Failnumber >= Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]))
                                {
                                    if (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) == 0) // not auto reset failed number
                                    {
                                        error = "Your account has been disable because of fail login times is over regulations of Bank.";
                                        Utility.ProcessLog.LogInformation("Check Login Policy Error in 2: " + error);
                                        tran.SetErrorInfo("5001", error);
                                        return false;
                                    }
                                    else // auto reset failed number
                                    {
                                        if (lastlogin < Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]))
                                        {
                                            error = string.Format("Your account has been disable because of fail login times is over regulations of Bank. Please try again after {0} minutes.", (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) - lastlogin).ToString());
                                            Utility.ProcessLog.LogInformation("Check Login Policy Error in 3: " + error);
                                            tran.SetErrorInfo("5001", error);
                                            return false;
                                        }
                                        else
                                        {
                                            DataTable dtupdate = con.FillDataTable(Common.ConStr, "MB_UpdateFailLogin", username, "0", sourceid);
                                        }
                                    }

                                }

                            }
                        } // end check policy
                        #endregion end check policy
                        //update last login time
                        if (Failnumber < Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]))
                        {
                            DataTable dtupdatellt = con.FillDataTable(Common.ConStr, "IB_UpdateLLTwithServiceid", username, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), sourceid);
                        }
                        //LanNTH - check policy to change password.
                        if (password.Length < Convert.ToInt32(dtpolicy.Rows[0]["minpwdlen"]))
                        {
                            error = "Please change the new password to match user policy";
                        }

                        if (bool.Parse(dtpolicy.Rows[0]["pwdcplx"].ToString()))
                        {
                            // check lowercase
                            if (bool.Parse(dtpolicy.Rows[0]["pwdcplxlc"].ToString()))
                            {
                                if (!hasLowerCharacter(password))
                                {
                                    error = "Please change the new password to match user policy";
                                }

                            }
                            //check upper case
                            if (bool.Parse(dtpolicy.Rows[0]["pwdcplxuc"].ToString()))
                            {
                                if (!hasUpperCharacter(password))
                                {
                                    error = "Please change the new password to match user policy";
                                }
                            }
                            //check symbol
                            if (bool.Parse(dtpolicy.Rows[0]["pwdcplxsc"].ToString()))
                            {
                                if (!hasSymbolCharacter(password))
                                {
                                    error = "Please change the new password to match user policy";
                                }
                            }
                            // check number
                            if (bool.Parse(dtpolicy.Rows[0]["pwccplxsn"].ToString()))
                            {
                                if (!hasNumberCharacter(Password))
                                {
                                    error = "Please change the new password to match user policy";
                                }
                            }
                        }

                        //reset failnumber
                        if (Failnumber > 0)
                        {
                            DataTable dtupdate = con.FillDataTable(Common.ConStr, "MB_UpdateFailLogin", username, "0", sourceid);
                        }
                        // check status:
                        switch (Status)
                        {
                            case "B":
                                error = "This account has been blocked. Please contact Bank for support.";
                                Utility.ProcessLog.LogInformation("Check Login Policy Error in 4: " + error);
                                tran.SetErrorInfo("5001", error);
                                return false;
                            default:
                                break;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                tran.SetErrorInfo(ex);
                return false;
            }
        }

        public bool VALIDATELOGIN(TransactionInfo tran)
        {
            bool ret = false;
            try
            {
                string error = string.Empty;
                string passPolicy = "Y";
                tran.Data.Add(Common.KEYNAME.ISPASSPOLICY, passPolicy);
                tran.Data.Add(Common.KEYNAME.ERRPOLICY, error);
                if (tran.Data.ContainsKey(Common.KEYNAME.PASSWORD) &&
                    !string.IsNullOrEmpty(tran.Data[Common.KEYNAME.PASSWORD].ToString()))
                {
                    tran.Data[Common.KEYNAME.PASSWORD] =
                        Common.Decryptpassword(tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    if (checklogin(tran))
                    {
                        tran.Data[Common.KEYNAME.ISPASSPOLICY] = passPolicy;
                        tran.Data[Common.KEYNAME.ERRPOLICY] = error;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    tran.Data[Common.KEYNAME.ISPASSPOLICY] = passPolicy;
                    tran.Data[Common.KEYNAME.ERRPOLICY] = error;
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");             
                tran.SetErrorInfo(ex);
                return false;
            }
        }

        public bool VALIDATEREGISTER(TransactionInfo tran)
        {
            bool ret = false;
            Connection con = new Connection();
            try
            {

                //if (tran.Data.ContainsKey(Common.KEYNAME.PHONENO) && tran.Data.ContainsKey(Common.KEYNAME.PASSWORD))
                {
                    //ProcessLog.LogInformation("==========passinputtows=" + tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    if (tran.Data.ContainsKey(Common.KEYNAME.PASSWORD) && !string.IsNullOrEmpty(tran.Data[Common.KEYNAME.PASSWORD].ToString()))
                    {
                        tran.Data[Common.KEYNAME.PASSWORD] = Common.Decryptpassword(tran.Data[Common.KEYNAME.PASSWORD].ToString());
                        tran.Data[Common.KEYNAME.PASSWORD] = Utility.O9Encryptpass.sha_sha256(tran.Data[Common.KEYNAME.PASSWORD].ToString(), tran.Data[Common.KEYNAME.USERID].ToString());
                    }
                    if (tran.Data.ContainsKey(Common.KEYNAME.PINCODE) && !string.IsNullOrEmpty(tran.Data[Common.KEYNAME.PINCODE].ToString()))
                    {
                        tran.Data[Common.KEYNAME.PINCODE] = Common.Decryptpassword(tran.Data[Common.KEYNAME.PINCODE].ToString());
                        tran.Data[Common.KEYNAME.PINCODE] = Utility.O9Encryptpass.sha_sha256(tran.Data[Common.KEYNAME.PINCODE].ToString(), tran.Data[Common.KEYNAME.USERID].ToString());
                    }
                    string error = string.Empty;
                    string passPolicy = "Y";
                    tran.Data.Add(Common.KEYNAME.ISPASSPOLICY, passPolicy);
                    tran.Data.Add(Common.KEYNAME.ERRPOLICY, error);


                    object policy = Common.SYSVAR["POLICYDEFAULT"];

                    if (policy != null)
                    {
                        Common.HashTableAddOrSet(tran.Data, Common.KEYNAME.POLICY, policy);
                        Common.HashTableAddOrSet(tran.Data, Common.KEYNAME.ISPASSPOLICY, passPolicy);
                        Common.HashTableAddOrSet(tran.Data, Common.KEYNAME.ERRPOLICY, error);
                    }
                    else return false;
                }
                ret = true;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                ret = false;
            }
            return ret;
        }
        //VuTT Credit card
        public bool GetCreditCardInformation(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                DataTable dtacct = con.FillDataTable(Common.ConStr, "CR_MB_GET_CARDLIST", tran.Data[Common.KEYNAME.USERID].ToString(), "ALL", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString());
                dtacct.Columns.Add(new DataColumn(Common.KEYNAME.CREDITLIMIT));
                dtacct.Columns.Add(new DataColumn(Common.KEYNAME.OUTSTANDINGAMT));
                dtacct.Columns.Add(new DataColumn(Common.KEYNAME.AVAILIMIT));
                dtacct.Columns.Add(new DataColumn(Common.KEYNAME.STATUS));

                DataTable dtresult = dtacct.Clone();
                dtresult.Clear();

                foreach (DataRow dr in dtacct.Rows)
                {
                    try
                    {
                        AutoTrans execTran = new AutoTrans();
                        Hashtable hasInput = new Hashtable();
                        Hashtable hasOutput = new Hashtable();

                        //add key into input
                        hasInput.Add(Common.KEYNAME.IPCTRANCODE, "MB000042");
                        hasInput.Add(Common.KEYNAME.SOURCEID, "MB");
                        //hasInput.Add(Common.KEYNAME.SERVICEID, "MB");
                        hasInput.Add(Common.KEYNAME.TRANDESC, "mb get credit card info");
                        //session info
                        hasInput.Add(Common.KEYNAME.USERID, tran.Data[Common.KEYNAME.USERID].ToString().Trim());
                        hasInput.Add(Common.KEYNAME.UUID, tran.Data[Common.KEYNAME.UUID].ToString().Trim());
                        hasInput.Add(Common.KEYNAME.DEVICEID, tran.Data[Common.KEYNAME.DEVICEID].ToString().Trim());

                        hasInput.Add(Common.KEYNAME.cardNo, dr[Common.KEYNAME.CARDNO].ToString());
                        hasOutput = execTran.ProcessTransHAS(hasInput);

                        if (hasOutput[Common.KEYNAME.IPCERRORCODE].ToString().Equals("0"))
                        {
                            dr[Common.KEYNAME.STATUS] = hasOutput["cardStatus"].ToString();
                            dr[Common.KEYNAME.CREDITLIMIT] = hasOutput["creditLimit"].ToString();
                            dr[Common.KEYNAME.OUTSTANDINGAMT] = hasOutput["outstandingAmt"].ToString();
                            dr[Common.KEYNAME.AVAILIMIT] = hasOutput["avaiLimit"].ToString();
                            dtresult.ImportRow(dr);
                        }
                        else
                        {
                            tran.ErrorCode = hasOutput[Common.KEYNAME.IPCERRORCODE].ToString();
                            tran.ErrorCode = hasOutput[Common.KEYNAME.IPCERRORDESC].ToString();

                            try
                            {
                                tran.Data[Common.KEYNAME.ERRORCODE] = hasOutput[Common.KEYNAME.IPCERRORCODE].ToString();
                                tran.Data[Common.KEYNAME.ERRORDESC] = hasOutput[Common.KEYNAME.IPCERRORDESC].ToString();
                            }
                            catch { }
                        }
                    }
                    catch
                    {
                    }
                }
                DataSet dsacct = new DataSet();
                dsacct.Tables.Add(dtresult);
                if (tran.Data.ContainsKey(Common.KEYNAME.DATARESULT))
                {
                    tran.Data[Common.KEYNAME.DATARESULT] = dsacct;
                }
                else
                {
                    tran.Data.Add(Common.KEYNAME.DATARESULT, dsacct);
                }

                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
                return false;
            }
        }

        public bool GenBioMetricToken(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                string userID = tran.Data[Common.KEYNAME.USERID].ToString();
                string deviceID = tran.Data[Common.KEYNAME.DEVICEID].ToString();
                string biometricToken = O9Encryptpass.sha256(new Random().Next(100000000, 999999999).ToString());
                string encodedToken = Common.EncodeBioMetricToken(userID, deviceID, biometricToken);

                if (con.ExecuteNonquery(Common.ConStr, "MB_ENABLE_BIOMETRIC", new object[] { userID, deviceID, encodedToken, tran.Data[Common.KEYNAME.SERVICEID].ToString() }) > 0)
                {
                    tran.Data.Add(Common.KEYNAME.BIOMETRICTOKEN, biometricToken);
                    //tran.Data.Add(Common.KEYNAME.ENCODEDTOKEN, encodedToken);
                    return true;
                }
                else
                {
                    tran.SetErrorInfo("5006", "Failed to enable Fingerprint/Face ID authentication");
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
            }
            return false;
        }
        public bool CHECKNEWPASSWORD(TransactionInfo tran)
        {
            try
            {
                string error = String.Empty;
                tran.Data[Common.KEYNAME.NEWPASS] = Common.Decryptpassword(tran.Data[Common.KEYNAME.NEWPASS].ToString());
                string userid = tran.Data[Common.KEYNAME.USERID].ToString();
                string serviceID = tran.Data[Common.KEYNAME.SERVICEID].ToString();
                string newpass = tran.Data[Common.KEYNAME.NEWPASS].ToString();
                string typepass = tran.Data["TYPE"].ToString();
                if (string.IsNullOrEmpty(newpass))
                {
                    error = "Password can not be empty";
                    tran.SetErrorInfo("6029", error);
                    return false;
                }
                Connection con = new Connection();
                DataTable dtpolicy = con.FillDataTable(Common.ConStr, "Users_Policy_by_userid", serviceID, userid);
                DataTable dtpasshis = con.FillDataTable(Common.ConStr, "eba_Users_getpasswordhis_byuserid", serviceID, userid, typepass);

                // check effective date of policy
                if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare(((DateTime)dtpolicy.Rows[0]["efto"]).Date, ((DateTime)dtpolicy.Rows[0]["systemtime"]).Date) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
                {
                    //check password duplicate with password his:
                    if (dtpasshis.Rows.Count != 0)
                    {
                        int pwdhis = Convert.ToInt32(dtpolicy.Rows[0]["pwdhis"]);
                        string Passnew = Utility.O9Encryptpass.sha_sha256(newpass, userid);
                        for (int i = 0; i < pwdhis -1; i++)
                        {
                            string passhis = DBNull.Value.Equals(dtpolicy.Rows[0][i]) ? string.Empty : dtpasshis.Rows[0][i].ToString().Trim();
                            if (Passnew == passhis)
                            {
                                if (typepass == "PASSWORD")
                                {
                                    error = pwdhis.ToString();
                                    tran.SetErrorInfo("6030", error);
                                    return false;
                                }
                                else
                                {
                                    error = pwdhis.ToString();
                                    tran.SetErrorInfo("6033", error);
                                    return false;
                                }
                            }
                        }
                    }
                    bool isSecurity = bool.Parse(con.FillDataTable(Common.ConStr, "EBASYSVAR_VIEW", "PASSWORDSECURITY").Rows[0]["VARVALUE"].ToString());
                    if (!isSecurity)
                    {
                        return true;
                    }
                    if (typepass == "PASSWORD")
                    {
                        // check length of pass
                        if (newpass.Length < Convert.ToInt32(dtpolicy.Rows[0]["minpwdlen"]))
                        {
                            error = dtpolicy.Rows[0]["minpwdlen"].ToString();
                            tran.SetErrorInfo("6031", error);
                            return false;
                        }

                        int index = 0;
                        if (bool.Parse(dtpolicy.Rows[0]["pwdcplx"].ToString()))
                        {
                            // check lowercase
                            if (bool.Parse(dtpolicy.Rows[0]["pwdcplxlc"].ToString()))
                            {
                                if (!hasLowerCharacter(newpass))
                                {
                                    index += 1;
                                    if (index > 1)
                                    {
                                        error += ", {LOWERCASE}";
                                    }
                                    else
                                    {
                                        error += "{LOWERCASE}";
                                    }
                                }

                            }
                            //check upper case
                            if (bool.Parse(dtpolicy.Rows[0]["pwdcplxuc"].ToString()))
                            {
                                if (!hasUpperCharacter(newpass))
                                {
                                    index += 1;
                                    if (index > 1)
                                    {
                                        error += ", {UPPERCASE}";
                                    }
                                    else
                                    {
                                        error += "{UPPERCASE}";
                                    }
                                }
                            }
                            //check symbol
                            if (bool.Parse(dtpolicy.Rows[0]["pwdcplxsc"].ToString()))
                            {
                                if (!hasSymbolCharacter(newpass))
                                {
                                    index += 1;
                                    if (index > 1)
                                    {
                                        error += ", {SYMBOL}";
                                    }
                                    else
                                    {
                                        error += "{SYMBOL}";
                                    }
                                }
                            }
                            // check number
                            if (bool.Parse(dtpolicy.Rows[0]["pwccplxsn"].ToString()))
                            {
                                if (!hasNumberCharacter(newpass))
                                {
                                    index += 1;
                                    if (index > 1)
                                    {
                                        error += ", {NUMBER}";
                                    }
                                    else
                                    {
                                        error += "{NUMBER}";
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(error))
                            {
                                error = @"{ERROR} " + error;
                            }
                        }
                        if (!string.IsNullOrEmpty(error))
                        {
                            tran.SetErrorInfo("6032", error);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                tran.SetErrorInfo(ex);
                return false;
            }
        }
        //QuangTV Add check Weak password
        public bool CheckWeakPassword(TransactionInfo tran)
        {
            try
            {
                string userName = string.Empty;
                string password = string.Empty;
                string type = string.Empty;
                if (tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("MB_SETPINLGMETHOD")|| tran.Data[Common.KEYNAME.IPCTRANCODE].Equals("MB_REGISTERWL"))
                {
                    if (tran.Data.ContainsKey(Common.KEYNAME.PASSWORD) && !string.IsNullOrEmpty(tran.Data[Common.KEYNAME.PASSWORD].ToString()))
                    {
                        password = Common.Decryptpassword(tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    }
                    if (tran.Data.ContainsKey(Common.KEYNAME.PINCODE) && !string.IsNullOrEmpty(tran.Data[Common.KEYNAME.PINCODE].ToString()))
                    {
                        password = Common.Decryptpassword(tran.Data[Common.KEYNAME.PINCODE].ToString());
                    }
                }
                else
                {
                    if (tran.Data.ContainsKey(Common.KEYNAME.NEWPASS))
                    {
                        password = tran.Data[Common.KEYNAME.NEWPASS].ToString();
                    }
                    else
                    {
                        password = tran.Data[Common.KEYNAME.PASSWORD].ToString();
                    }
                }
                tran.Data[Common.KEYNAME.USERID].ToString();

                Connection dbObj = new Connection();
                DataTable dtable = dbObj.FillDataTable(Common.ConStr, "IPC_GET_CHECKPOLICY");
                bool isSecurity = bool.Parse(dbObj.FillDataTable(Common.ConStr, "EBASYSVAR_VIEW", "PASSWORDSECURITY").Rows[0]["VARVALUE"].ToString());
                if (!isSecurity)
                {
                    return true;
                }
                if (tran.Data.ContainsKey("TYPE"))
                {
                    type = tran.Data["TYPE"].ToString();
                }
                if (tran.Data["SOURCEID"].Equals("SEMS"))
                {
                    userName = tran.Data[Common.KEYNAME.USERID].ToString();
                    type = "PASSWORD";
                }
                else
                {
                    DataTable dtUsername = dbObj.FillDataTable(Common.ConStr, "IPC_GET_USERNAME", tran.Data[Common.KEYNAME.USERID].ToString(), password);
                    userName = dtUsername.Rows[0]["USERNAME"].ToString().Trim();
                }
                if (type.Equals("PASSWORD"))
                {
                    // disallowing passwords based on the username
                    if (password.ToUpper().Contains(userName.ToUpper()))
                    {
                        tran.SetErrorInfo("2001", "The password cannot be based on the username.");
                        return false;
                    }
                }
                foreach (DataRow dr in dtable.Rows)
                {
                    if (dr["TYPE"].ToString().Trim().Equals("C"))
                    {
                        if (dr["PASSWORD"].ToString().Trim().Contains(password) ||
                            password.Contains(dr["PASSWORD"].ToString().Trim()) ||
                            dr["PASSWORD"].ToString().Trim().ToUpper().Contains(password.ToUpper()))
                        {
                            if (type.Equals("PASSWORD"))
                            {
                                tran.SetErrorInfo("2003", "Password is not secure, please enter a new password.");
                                return false;
                            }
                            else
                            {
                                tran.SetErrorInfo("2013", "Pincode is not secure, please enter a new pincode.");
                                return false;
                            }

                        }
                    }
                    else
                    {
                        if (dr["PASSWORD"].ToString().Trim().Equals(password) ||
                            dr["PASSWORD"].ToString().Trim().ToUpper().Equals(password.ToUpper()))
                        {
                            if (type.Equals("PASSWORD"))
                            {
                                tran.SetErrorInfo("2004", "Password is not secure, please enter a new password.");
                                return false;
                            }
                            else
                            {
                                tran.SetErrorInfo("2014", "Pincode is not secure, please enter a new pincode.");
                                return false;
                            }
                        }
                    }
                    char[] Array = password.ToCharArray();
                    //Password is only 1 character
                    var duplicates_1 = Array.GroupBy(p => p).Where(g => g.Count() >= Array.Count()).Select(g => g.Key).ToList();
                    // Each password has two characters that are repeated over 3 times
                    var duplicates_3 = Array.GroupBy(p => p).Where(g => g.Count() >= 3).Select(g => g.Key).ToList();
                    if (duplicates_3.Count >= 2 || duplicates_1.Count.Equals(1))
                    {
                        if (type.Equals("PASSWORD"))
                        {
                            tran.SetErrorInfo("2005", "Password is not secure, please enter a new password.");
                            return false;
                        }
                        else
                        {
                            tran.SetErrorInfo("2015", "Pincode is not secure, please enter a new pincode.");
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                tran.SetErrorInfo(ex);
                return false;
            }
        }
        //KhanhHD add check length pass 
        public bool DECRYPTPASSWORD(TransactionInfo tran)
        {
            try
            {
                if (tran.Data.ContainsKey(Common.KEYNAME.PASSWORD))
                {
                    tran.Data[Common.KEYNAME.PASSWORD] = Common.Decryptpassword(tran.Data[Common.KEYNAME.PASSWORD].ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(),
                    System.Reflection.MethodBase.GetCurrentMethod().Name);
                tran.SetErrorInfo(ex);
                return false;
            }
        }
    }
}
