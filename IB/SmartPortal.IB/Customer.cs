using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

namespace SmartPortal.IB
{
    public class Customer
    {
        #region SELECT ACCTNO OF CUSTOMER
        public DataSet GetAllAcctno(string custID,string contractNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBGETACCTNO");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy các tài khoản của khách hàng");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CUSTID, custID);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetInfo(string storeName, object[] para, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000500");
                hasInput.Add("STORENAME", storeName);
                hasInput.Add("PARA", para);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                return ds;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region LAY THONG TIN CHU TAI KHOAN
        public Hashtable GetOwner(string Account, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000123");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin chủ tài khoản");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, Account);               

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    //ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Check input phone
        public static bool IsPhoneNumber(string number)
        {
            if (number.Length >= 12 || number.Length <= 8)
                return false;
            else
            return Regex.Match(number, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").Success;
        }
        #endregion


        #region MyRegion

        public DataSet GetAllAcc(string userId, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBGETALLACCOUNT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy các tài khoản của khách hàng");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userId);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        public DataSet SysVarGetByParGrp(string pargrp, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "EBASYSVARBYPARGRP");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add("PARGRP", pargrp);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet getCategoryOpenBank(string term,string ccyid,string lang , ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETCATOPENBANK");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.TERM, term);
                hasInput.Add(SmartPortal.Constant.IPC.CCYCD, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.LANGUAGE, lang);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet getTerm( ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETTERM");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public  Hashtable GetInfoAccountClose(string AcctNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBGETACTDETAILCLOSE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, AcctNo);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Hashtable requestOpenBankAcct(string userID,string amount,string ccyid,string accountNO, string acountType, string ccyidcode, string desc,string otp,string authentype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB_OPENBANKACT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, accountNO);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTTYPE, acountType);
                hasInput.Add(SmartPortal.Constant.IPC.CCYCD, ccyidcode);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, desc);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, otp);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();

                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        public Hashtable CloseBankAcct(string userID, string amount, string ccyid, string fdreceipt, string accountNO, string receiverAct, string desc, string otp, string authentype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB_CLOSEBANKACT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, accountNO);
                hasInput.Add("fdreceipt", fdreceipt);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, receiverAct);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, desc);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, otp);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();

                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();

                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetQuestion(string pargrp, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB_GETQUESTION");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add("PARGRP", pargrp);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet ds = new DataSet();
                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
