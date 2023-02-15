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

        //Add by Chi Pham 5/5/2015
        public bool GetBalanceOfAccount(TransactionInfo tran)
        {
            DataSet dsInputAccount = (DataSet)tran.Data[Common.KEYNAME.DATARESULT];
            DataTable dtInputAccount = dsInputAccount.Tables[0];

            DataSet dsAcceptAccount = (DataSet)tran.Data[Common.KEYNAME.SELECTRESULT];
            DataTable dtAceptAccount = dsAcceptAccount.Tables[0];

            bool status = false;

            int no = dtInputAccount.Rows.Count - 1;
            for (int i = no; i >= 0; i--)
            {
                status = false;
                string accountno = dtInputAccount.Rows[i]["accountno"].ToString();
                string ac = "";
                for (int j = 0; j < dtAceptAccount.Rows.Count; j++)
                {
                    ac = dtAceptAccount.Rows[j]["AcctNo"].ToString();
                    if (accountno.Equals(ac))
                    {
                        status = true;
                    }
                }
                if (status == false) dtInputAccount.Rows[i].Delete();
            }
            if (tran.Data.ContainsKey("DATARESULT") == false)
            {
                tran.Data.Add("DATARESULT", dsInputAccount);
            }
            else
            {
                tran.Data["DATARESULT"] = dsInputAccount;
            }
            return true;
        }
        public bool ENCRYPPASSWORD(TransactionInfo tran)
        {
            bool ret = false;
            try
            {
                //for biometric
                if (string.IsNullOrEmpty(tran.Data[Common.KEYNAME.PASSWORD].ToString()))
                {
                    return true;
                }

                if (tran.Data.ContainsKey(Common.KEYNAME.USERID) && tran.Data.ContainsKey(Common.KEYNAME.PASSWORD))
                {
                    //tran.Data[Common.KEYNAME.PASSWORD] = Common.Decryptpassword(tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    //ProcessLog.LogInformation("==========pass=" + tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    //ProcessLog.LogInformation("==========user=" + tran.Data[Common.KEYNAME.USERID].ToString());
                    tran.Data[Common.KEYNAME.PASSWORD] = Utility.O9Encryptpass.sha_sha256(tran.Data[Common.KEYNAME.PASSWORD].ToString(), tran.Data[Common.KEYNAME.USERID].ToString());
                    //ProcessLog.LogInformation("==========passnew=" + tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    ret = true;
                }
                if (tran.Data.ContainsKey(Common.KEYNAME.USERID) && tran.Data.ContainsKey(Common.KEYNAME.NEWPASS))
                {
                    //tran.Data[Common.KEYNAME.NEWPASS] = Common.Decryptpassword(tran.Data[Common.KEYNAME.NEWPASS].ToString());
                    //ProcessLog.LogInformation("==========passnew=" + tran.Data[Common.KEYNAME.NEWPASS].ToString());
                    tran.Data[Common.KEYNAME.NEWPASS] = Utility.O9Encryptpass.sha_sha256(tran.Data[Common.KEYNAME.NEWPASS].ToString(), tran.Data[Common.KEYNAME.USERID].ToString());
                    //ProcessLog.LogInformation("==========passnewenc=" + tran.Data[Common.KEYNAME.NEWPASS].ToString());
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
            bool ret = false;
            try
            {
                if (tran.Data.ContainsKey(Common.KEYNAME.USERID) && tran.Data.ContainsKey(Common.KEYNAME.NEWPASS))
                {
                    //ProcessLog.LogInformation("==========passinputtows=" + tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    tran.Data[Common.KEYNAME.PASSWORD] = Common.Decryptpassword(tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    tran.Data[Common.KEYNAME.NEWPASS] = Common.Decryptpassword(tran.Data[Common.KEYNAME.NEWPASS].ToString());
                    string error = string.Empty;
                    if (!CHECKPASSWORD(tran.Data[Common.KEYNAME.USERID].ToString(), tran.Data[Common.KEYNAME.PASSWORD].ToString(), tran.Data[Common.KEYNAME.NEWPASS].ToString(), ref error))
                        //throw new Exception("IPCERROR=5004#" + error);
                        if (System.Configuration.ConfigurationManager.AppSettings["MBCPWTRANCODE"].Equals(tran.Data[Common.KEYNAME.IPCTRANCODE]))
                        {
                            throw new Exception("IPCERROR=5001#" + error);
                        }
                        else
                            throw new Exception("IPCERROR=5001#INVALID USERID OR PASSWORD");


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
        public bool CHECKPASSWORD(string username, string oldpass, string newpass, ref string error)
        {
            bool ret = false;
            try
            {
                if (string.IsNullOrEmpty(newpass))
                {
                    error = "Password can not be empty";
                    return false;
                }

                if (oldpass.Equals(newpass))
                {
                    error = "New password must different old password";
                    return false;
                }
                Connection con = new Connection();
                // DataTable dtacct = con.FillDataTable(Common.ConStr, "SMS_NOTIFICATION_SELECT", null);
                //
                DataTable dtpolicy = con.FillDataTable(Common.ConStr, "Users_Policy_by_userid", Common.KEYNAME.MB, username);
                DataTable dtpasshis = con.FillDataTable(Common.ConStr, "eba_Users_getpasswordhis_byuserid", Common.KEYNAME.MB, username);


                // check effective date of policy
                //if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare((DateTime)dtpolicy.Rows[0]["efto"], (DateTime)dtpolicy.Rows[0]["systemtime"]) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
                if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare(((DateTime)dtpolicy.Rows[0]["efto"]).Date, ((DateTime)dtpolicy.Rows[0]["systemtime"]).Date) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
                //if (DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare((DateTime)dtpolicy.Rows[0]["efto"], (DateTime)dtpolicy.Rows[0]["systemtime"]) >= 0)
                {

                    //check password duplicate with password his:
                    if (dtpasshis.Rows.Count != 0)
                    {
                        int pwdhis = Convert.ToInt32(dtpolicy.Rows[0]["pwdhis"]);
                        string Passnew = Utility.O9Encryptpass.sha_sha256(newpass, username);
                        for (int i = 0; i < pwdhis; i++)
                        {
                            string passhis = DBNull.Value.Equals(dtpolicy.Rows[0][i]) ? string.Empty : dtpasshis.Rows[0][i].ToString().Trim();
                            if (Passnew == passhis)
                            {
                                error = string.Format("Password can not be the same with recent {0} Passwords in history", pwdhis.ToString());
                                return false;
                            }
                        }
                    }
                    // check length of pass
                    if (newpass.Length < Convert.ToInt32(dtpolicy.Rows[0]["minpwdlen"]))
                    {
                        error = string.Format("Password must have length at least {0} characters", dtpolicy.Rows[0]["minpwdlen"].ToString());


                        return false;
                    }

                    if (bool.Parse(dtpolicy.Rows[0]["pwdcplx"].ToString()))
                    {
                        // check lowercase
                        if (bool.Parse(dtpolicy.Rows[0]["pwdcplxlc"].ToString()))
                        {
                            if (!hasLowerCharacter(newpass))
                            {
                                error = "Password must have at least one lowercase character";
                                //lblError.Text = Resources.labels.matkhauphaicoitnhatmotkytuthuong;
                                return false;
                            }

                        }
                        //check upper case
                        if (bool.Parse(dtpolicy.Rows[0]["pwdcplxuc"].ToString()))
                        {
                            if (!hasUpperCharacter(newpass))
                            {
                                error = "Password must have at least one uppercase character";
                                // lblError.Text = Resources.labels.matkhauphaicoitnhatmotkytuchuhoa;
                                return false;
                            }
                        }
                        //check symbol
                        if (bool.Parse(dtpolicy.Rows[0]["pwdcplxsc"].ToString()))
                        {
                            if (!hasSymbolCharacter(newpass))
                            {
                                error = "Password must have at least one symbol character";
                                // lblError.Text = Resources.labels.matkhauphaicoitnhatmokytubieutuong;
                                return false;
                            }
                        }
                        // check number
                        if (bool.Parse(dtpolicy.Rows[0]["pwccplxsn"].ToString()))
                        {
                            if (!hasNumberCharacter(newpass))
                            {
                                error = "Password must have at least one number character";
                                // lblError.Text = Resources.labels.matkhauphaicoitnhatmokytuso;
                                return false;
                            }
                        }


                    }
                }

                ret = true;

            }
            catch (Exception ex)
            {
                error = ex.Message;
                ret = false;

            }
            return ret;
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

        private bool checklogin(string username, string password, ref string passPolicy, ref string errordesc)
        {
            bool checkexpirepolicy = false;
            Connection con = new Connection();
            // DataTable dtacct = con.FillDataTable(Common.ConStr, "SMS_NOTIFICATION_SELECT", null);
            //
            string UserName = string.Empty;
            string Password = string.Empty;
            string Status = string.Empty;
            int Failnumber = 0;
            //DateTime  ExpireTime=DateTime.Now; 
            DateTime SystemTime = DateTime.Now;

            int pwdage = 0;
            int lastlogin = 0;

            try
            {
                DataTable dtpolicy = con.FillDataTable(Common.ConStr, "Users_Policy_by_userid", Common.KEYNAME.MB, username);
                DataTable dtuser = con.FillDataTable(Common.ConStr, "IB_getaccountinfo", Common.KEYNAME.MB, username);
                if (dtuser.Rows.Count == 0)
                {
                    errordesc = "Your Username or Password is not correct.";
                    return false;
                }
                else
                {
                    UserName = dtuser.Rows[0]["userid"].ToString().Trim();
                    Password = dtuser.Rows[0]["passwordnew"].ToString().Trim();
                    Status = dtuser.Rows[0]["status"].ToString().Trim();
                    Failnumber = (int)dtuser.Rows[0]["failnumber"];
                    // ExpireTime = (DateTime)dtuser.Rows[0]["expiretime"];
                    SystemTime = (DateTime)dtuser.Rows[0]["systemtime"];

                    pwdage = Convert.ToInt32(dtuser.Rows[0]["pwdage"].ToString());
                    lastlogin = Convert.ToInt32(dtuser.Rows[0]["lastlogin"].ToString());
                }

                if (dtpolicy.Rows.Count == 0)
                {
                    errordesc = "No exists Policy for this user" + dtuser.Rows[0]["userid"].ToString();
                    return false;

                }

                else  //co tai khoan dang ky
                {


                    ////check expire_date:
                    //if (DateTime.Compare(UserIB.Dateexpire.Date, UserIB.SystemTime.Date) < 0)
                    //{
                    //    lblAlert.Text = Resources.labels.accounthasbeenexpired;
                    //    return false;
                    //}
                    // check password:
                    //if (Utility.KillSqlInjection(Encryption.Encrypt(txtPass.Text.Trim())) != UserIB.Password)

                    //ProcessLog.LogInformation("==========PASSWORDDB=" + Password);
                    //ProcessLog.LogInformation("==========PASSWORDINPUT=" + Utility.O9Encryptpass.sha_sha256(password, username));

                    if (Utility.O9Encryptpass.sha_sha256(password, username) != Password)
                    {

                        //check expire policy
                        if (!checkexpirepolicy)
                        {
                            errordesc = "Your Username or Password is not correct.";
                            return false;
                        }
                        //check failed login
                        if (Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]) > 0)
                        {
                            if (Failnumber >= Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]))
                            {
                                if (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) == 0) // not auto reset failed number
                                {
                                    errordesc = "Your account has been disable because of fail login times is over regulations of Bank.";
                                    passPolicy = ConfigurationManager.AppSettings["ERROVERFAILLOGIN"];
                                    return false;
                                }
                                else // auto reset failed number
                                {
                                    if (lastlogin <= Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]))
                                    {
                                        errordesc = string.Format("Your account has been disable because of fail login times is over regulations of Bank. Please try again after {0} minutes.", (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) - lastlogin).ToString());
                                        passPolicy = ConfigurationManager.AppSettings["ERROVERFAILTRYAGAIN"];
                                        return false;
                                    }
                                    else
                                    {
                                        errordesc = "Your account has been disable because of fail login times is over regulations of Bank.";
                                        passPolicy = ConfigurationManager.AppSettings["ERROVERFAILLOGIN"];
                                        return false;
                                    }
                                }

                            }
                            else
                            {
                                //add failnumber to db
                                DataTable updatefailnumber = con.FillDataTable(Common.ConStr, "MB_UpdateFailLogin", username, string.Empty);
                                // DataTable tblFailNumber = new SmartPortal.IB.Account().UpdateFailLoginSEMS(Utility.KillSqlInjection(txtUserName.Text.Trim()), string.Empty);
                                errordesc = string.Format("Your Username or Password is not correct. Please note your account will be temporary disable if you fail to login {0} times.", dtpolicy.Rows[0]["llkoutthrs"].ToString());
                                return false;
                            }

                        }

                        else
                        {
                            errordesc = "Your Username or Password is not correct.";

                        }

                        return false;
                    }
                    else
                    {


                        //reset failnumber
                        if (Failnumber > 0)
                        {
                            DataTable dtupdate = con.FillDataTable(Common.ConStr, "MB_UpdateFailLogin", username, "0");
                        }
                        // check status:
                        switch (Status)
                        {
                            case "B":
                                errordesc = "This account has been blocked. Please contact AYA Bank for support.";
                                return false;


                            default:
                                break;

                        }
                    }

                    // check policy
                    #region CHECK POLICY
                    //check effect date of policy
                    //if ((DBNull.Value.Equals(dtpolicy.Rows[0]["efto"]) || DateTime.Compare((DateTime)dtpolicy.Rows[0]["efto"], (DateTime)dtpolicy.Rows[0]["systemtime"]) >= 0) && DateTime.Compare((DateTime)dtpolicy.Rows[0]["effrom"], (DateTime)dtpolicy.Rows[0]["systemtime"]) <= 0)
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
                                errordesc = string.Format("This time not allow to login. Please login in allowed time from {0} to {1}", dtpolicy.Rows[0]["lginfr"].ToString(), dtpolicy.Rows[0]["lginto"].ToString());
                                return false;
                            }
                        }
                        //check password age
                        if ((int)dtpolicy.Rows[0]["pwdagemax"] > 0)
                        {
                            if ((int)dtpolicy.Rows[0]["pwdagemax"] - pwdage < 0)
                            {
                                errordesc = "Password age max has been reached. Please contact to Bank to reset password.";
                                passPolicy = ConfigurationManager.AppSettings["ERROVERPASS"];
                                return false;
                            }
                        }

                        //check failed login
                        if (Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]) > 0)
                        {
                            if (Failnumber >= Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]))
                            {
                                if (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) == 0) // not auto reset failed number
                                {
                                    errordesc = "Your account has been disable because of fail login times is over regulations of Bank.";
                                    passPolicy = ConfigurationManager.AppSettings["ERROVERFAILLOGIN"];
                                    return false;
                                }
                                else // auto reset failed number
                                {
                                    if (lastlogin < Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]))
                                    {
                                        errordesc = string.Format("Your account has been disable because of fail login times is over regulations of Bank. Please try again after {0} minutes.", (Convert.ToInt32(dtpolicy.Rows[0]["resetlkout"]) - lastlogin).ToString());
                                        passPolicy = ConfigurationManager.AppSettings["ERROVERFAILTRYAGAIN"];
                                        return false;
                                    }
                                    else
                                    {
                                        //Failnumber = 0;
                                        DataTable dtupdate = con.FillDataTable(Common.ConStr, "MB_UpdateFailLogin", username, "0");


                                        //lblAlert.Text = Resources.labels.accounthasbeenblockbecauseoffailnumber;
                                        //return false;
                                    }
                                }

                            }

                        }
                        if (password.Length < Convert.ToInt32(dtpolicy.Rows[0]["minpwdlen"]))
                        {
                            errordesc = string.Format("Password must have length at least {0} characters", dtpolicy.Rows[0]["minpwdlen"].ToString());
                            passPolicy = ConfigurationManager.AppSettings["ERRMINPASSLEN"];
                            return false;
                        }

                        if (bool.Parse(dtpolicy.Rows[0]["pwdcplx"].ToString()))
                        {
                            passPolicy = ConfigurationManager.AppSettings["ERRPOLICY"];
                            // check lowercase
                            if (bool.Parse(dtpolicy.Rows[0]["pwdcplxlc"].ToString()))
                            {
                                if (!hasLowerCharacter(password))
                                {
                                    errordesc = "Password must have at least one lowercase character";
                                    //lblError.Text = Resources.labels.matkhauphaicoitnhatmotkytuthuong;
                                    return false;
                                }

                            }
                            //check upper case
                            if (bool.Parse(dtpolicy.Rows[0]["pwdcplxuc"].ToString()))
                            {
                                if (!hasUpperCharacter(password))
                                {
                                    errordesc = "Password must have at least one uppercase character";
                                    // lblError.Text = Resources.labels.matkhauphaicoitnhatmotkytuchuhoa;
                                    return false;
                                }
                            }
                            //check symbol
                            if (bool.Parse(dtpolicy.Rows[0]["pwdcplxsc"].ToString()))
                            {
                                if (!hasSymbolCharacter(password))
                                {
                                    errordesc = "Password must have at least one symbol character";
                                    // lblError.Text = Resources.labels.matkhauphaicoitnhatmokytubieutuong;
                                    return false;
                                }
                            }
                            // check number
                            if (bool.Parse(dtpolicy.Rows[0]["pwccplxsn"].ToString()))
                            {
                                if (!hasNumberCharacter(Password))
                                {
                                    errordesc = "Password must have at least one number character";
                                    // lblError.Text = Resources.labels.matkhauphaicoitnhatmokytuso;
                                    return false;
                                }
                            }
                        }


                    } // end check policy
                    #endregion end check policy
                    //update last login time
                    if (Failnumber < Convert.ToInt32(dtpolicy.Rows[0]["llkoutthrs"]))
                    {
                        DataTable dtupdatellt = con.FillDataTable(Common.ConStr, "IB_UpdateLLTwithServiceid", username, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Common.KEYNAME.MB);
                        // new SmartPortal.IB.User().UpdateLLTwithServiceid(Utility.KillSqlInjection(txtUserName.Text.Trim()), UserIB.SystemTime.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.SEMS);
                        //add failnumber to db
                        //DataTable tblFailNumber = new SmartPortal.IB.Account().UpdateFailLogin(Utility.KillSqlInjection(txtUserName.Text.Trim()), string.Empty);

                    }



                }
                return true;

            }
            catch (Exception ex)
            {
                ProcessLog.LogInformation(ex.Message);
                errordesc = ex.Message;
                return false;
            }



        }

        public bool VALIDATELOGIN(TransactionInfo tran)
        {
            bool ret = false;
            try
            {
                //for biometric
                if (string.IsNullOrEmpty(tran.Data[Common.KEYNAME.PASSWORD].ToString()))
                    return true;


                if (tran.Data.ContainsKey(Common.KEYNAME.USERID) && tran.Data.ContainsKey(Common.KEYNAME.PASSWORD))
                {
                    //ProcessLog.LogInformation("==========passinputtows=" + tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    tran.Data[Common.KEYNAME.PASSWORD] = Common.Decryptpassword(tran.Data[Common.KEYNAME.PASSWORD].ToString());
                    string errordesc = string.Empty;
                    string passPolicy = "Y";//ConfigurationManager.AppSettings["ERRWRONGINFOLOGIN"];


                    if (!checklogin(tran.Data[Common.KEYNAME.USERID].ToString(), tran.Data[Common.KEYNAME.PASSWORD].ToString(), ref passPolicy, ref errordesc))
                    //throw new Exception("IPCERROR=5003#" + error);
                    {
                        //if (ConfigurationManager.AppSettings["MBLOGINV1"].Equals(tran.Data[Common.KEYNAME.IPCTRANCODE]))
                        throw new Exception("IPCERROR=5001#INVALID USERID OR PASSWORD");
                        //else //send error message detail for user login by V2
                        //    throw new Exception("IPCERROR=" + passPolicy + "#" + errordesc);
                    }
                    else
                    {
                        tran.Data.Add("ISPASSPOLICY", passPolicy);
                        tran.Data.Add("ERRORDESC", errordesc);
                    }

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

                if (con.ExecuteNonquery(Common.ConStr, "MB_ENABLE_BIOMETRIC", new object[] { userID, deviceID, encodedToken }) > 0)
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
    }
}
