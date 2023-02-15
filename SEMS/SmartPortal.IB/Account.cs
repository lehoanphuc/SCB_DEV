using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using SmartPortal.DAL;

namespace SmartPortal.IB
{
    public class Account
    {
        #region GET ACCOUNT FROM USERID
        public DataSet getAccount(string userID, string trancodeToRight, string acctType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000200");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODETORIGHT, trancodeToRight);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTTYPE, acctType);
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

        #region GET ACCOUNT FROM USERID
        public DataSet getAccountOnline(string userID, string trancodeToRight, string acctType, string ccyid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                DataTable tblFD = new DataTable();

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@userid";
                p1.Value = userID;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@trancodetoright";
                p2.Value = trancodeToRight;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@accttype";
                p3.Value = acctType;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@ccyid";
                p4.Value = ccyid;
                p4.SqlDbType = SqlDbType.VarChar;

                tblFD = DataAccess.GetFromDataTable("IB_GETACCTRIGHTFD", p1, p2, p3, p4);

                DataSet ds = new DataSet();
                ds.Tables.Add(tblFD);

                errorCode = "0";
                return ds;
            }
            catch (Exception ex)
            {
                errorCode = "9999";
                errorDesc = ex.ToString();
                throw ex;
            }
        }
        #endregion

        #region Topup

        public DataTable GetTelcoAcc(string TelcoID)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@telcoid";
            p1.Value = TelcoID;
            p1.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("TU_TELCOACCOUNT_SELECT", p1);

            return iRead;
        }
        #endregion
        //GET INFORMATION FOR ACCTNO 
        public Hashtable loadInfobyAcct(string acctNo)
        {
            Hashtable input = new Hashtable();
            Hashtable hasOutput = new Hashtable();
            input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000202");
            input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
            input.Add(SmartPortal.Constant.IPC.ACCTNO, acctNo);

            hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            return hasOutput;

        }
        public Hashtable BuyTopupCard(string UserID, string SenderAccount, string ReceiverAccount, string CCYID, string senderName, string receiverName, string debitBrachID, string creditBrachID, string Desc, string authentype, string authencode, string telco, string cardtype)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000210");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                input.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receiverName);
                input.Add(SmartPortal.Constant.IPC.CREDITBRACHID, creditBrachID);
                input.Add(SmartPortal.Constant.IPC.DEBITBRACHID, debitBrachID);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, Desc);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);
                input.Add(SmartPortal.Constant.IPC.TELCO, telco);
                input.Add(SmartPortal.Constant.IPC.CARDTYPE, cardtype);

                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        //GET INFORMATION FOR ACCTNO IN EBANK SYSTEM
        public DataTable GetAcctnoInfo(string acctNo)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@acctno";
            p1.Value = acctNo;
            p1.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IB_GETINFOACCTNO", p1);

            return iRead;

        }
        //CHECK ACCOUNT EXISTS
        public DataSet CheckAccountExists(string AccountNo)
        {
            DataSet ds = new DataSet();
            try
            {
                Hashtable input = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000203");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, AccountNo);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
                ds = (DataSet)hasOutput["DATARESULT"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        // TRANSACTION TRANSFER SAME CUSTOMER ACOUNT
        public Hashtable TransferDDSameCust(string UserID, string SenderAccount, string ReceiverAccount, string amount, string CCYID, string Desc
            , string senderName, string receiverName, string debitBrachID, string creditBrachID, string authenType, string authenCode, string fee, string feepayer)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                //edit by vutran 19/09/2014 tinh lai phi
                //edit by vutran 19/09/2014 tinh lai phi
                string revfee = (feepayer.Equals("Receiver")) ? fee : "0";
                string sedfee = (feepayer.Equals("Sender")) ? fee : "0";

                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000201");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, ReceiverAccount);
                input.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                input.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                input.Add(SmartPortal.Constant.IPC.SEDFEE, sedfee);
                input.Add(SmartPortal.Constant.IPC.REVFEE, revfee);
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                input.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receiverName);
                input.Add(SmartPortal.Constant.IPC.CREDITBRACHID, creditBrachID);
                input.Add(SmartPortal.Constant.IPC.DEBITBRACHID, debitBrachID);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, Desc);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authenType);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authenCode);
                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        // TRANSACTION TRANSFER ORTHER CUSTOMER ACOUNT
        public Hashtable TransferDDOtherCust(string UserID, string SenderAccount, string ReceiverAccount, string amount, string CCYID, string senderName, string receiverName, string debitBrachID, string creditBrachID, string Desc, string authentype, string authencode, string fee, string feepayer)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                //edit by vutran 19/09/2014 tinh lai phi
                string revfee = (feepayer.Equals("Receiver")) ? fee : "0";
                string sedfee = (feepayer.Equals("Sender")) ? fee : "0";

                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000208");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, ReceiverAccount);
                input.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                input.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                input.Add(SmartPortal.Constant.IPC.SEDFEE, sedfee);
                input.Add(SmartPortal.Constant.IPC.REVFEE, revfee);
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                input.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receiverName);
                input.Add(SmartPortal.Constant.IPC.CREDITBRACHID, creditBrachID);
                input.Add(SmartPortal.Constant.IPC.DEBITBRACHID, debitBrachID);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, Desc);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);

                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        //TRANSACTION TRANSFER OUT BANK
        public Hashtable TransferOutBank(string UserID, string SenderAccount, string senderName, string ReceiverAccount,
                                    string License, string IssuePlace, string IssueDate, string ReceiverName, string ReceiverAdd,
                                    string debitBrachID, string creditBrachID, string BankCode, string CityCode, string Fee, string Amount, string CCYID, string Desc, string passdate, string autype, string aucode)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000206");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                input.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, ReceiverAccount);
                input.Add(SmartPortal.Constant.IPC.LICENSE, License);
                input.Add(SmartPortal.Constant.IPC.ISSUEPLACE, IssuePlace);
                input.Add(SmartPortal.Constant.IPC.ISSUEDATE, IssueDate);
                input.Add(SmartPortal.Constant.IPC.RECEIVERNAME, ReceiverName);
                input.Add(SmartPortal.Constant.IPC.RECEIVERADD, ReceiverAdd);
                input.Add(SmartPortal.Constant.IPC.CREDITBRACHID, creditBrachID);
                input.Add(SmartPortal.Constant.IPC.DEBITBRACHID, debitBrachID);
                input.Add(SmartPortal.Constant.IPC.BANKCODE, BankCode);
                input.Add(SmartPortal.Constant.IPC.CITYCODE, CityCode);
                input.Add(SmartPortal.Constant.IPC.FEE, Fee);
                input.Add(SmartPortal.Constant.IPC.AMOUNT, Amount);
                input.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, Desc);
                input.Add(SmartPortal.Constant.IPC.PASSDATE, passdate);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, autype);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, aucode);

                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #region RECEIVERLIST
        // TRANSACTION GET RECEIVERLIST
        public DataSet GetReceiverList(string UserID, string TransferType)
        {
            DataSet result = new DataSet();
            try
            {
                Hashtable input = new Hashtable();
                Hashtable output = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000204");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.TRANSFERTYPE, TransferType);
                output = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);

                if (output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    result = (DataSet)output[SmartPortal.Constant.IPC.DATASET];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public DataSet GetReceiverList(string ID, string UserID, string ReceiverName, string AcctNo
           , string TransferType, string License, string IssuePlace, string IssueDate, string Description, ref string errorCode, ref string errorDesc)
        {
            DataSet result = new DataSet();
            try
            {
                Hashtable input = new Hashtable();
                Hashtable output = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000106");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add("ID", ID);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add("RECEIVERNAME", ReceiverName);
                input.Add("ACCTNO", AcctNo);
                input.Add(SmartPortal.Constant.IPC.TRANSFERTYPE, TransferType);
                input.Add(SmartPortal.Constant.IPC.LICENSE, License);
                input.Add(SmartPortal.Constant.IPC.ISSUEPLACE, IssuePlace);
                input.Add(SmartPortal.Constant.IPC.ISSUEDATE, IssueDate);
                input.Add(SmartPortal.Constant.IPC.DESCRIPTION, Description);
                output = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);

                if (output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    result = (DataSet)output[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = output[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void InsertReceiverList(string UserID, string ReceiverName, string AcctNo
           , string TransferType, string License, string IssuePlace, string IssueDate, string Description, string status, string address, string bank, string province, string branch, string branchDesc, ref string errorCode, ref string errorDesc)
        {
            DataSet result = new DataSet();
            Hashtable hasOutput = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                Hashtable output = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000107");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                //input.Add("ID", ID);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add("RECEIVERNAME", ReceiverName);
                input.Add("ACCTNO", AcctNo);
                input.Add(SmartPortal.Constant.IPC.TRANSFERTYPE, TransferType);
                input.Add(SmartPortal.Constant.IPC.LICENSE, License);
                input.Add(SmartPortal.Constant.IPC.ISSUEPLACE, IssuePlace);
                input.Add(SmartPortal.Constant.IPC.ISSUEDATE, IssueDate);
                input.Add(SmartPortal.Constant.IPC.DESCRIPTION, Description);
                input.Add(SmartPortal.Constant.IPC.STATUS, status);
                input.Add(SmartPortal.Constant.IPC.ADDRESS, address);
                input.Add(SmartPortal.Constant.IPC.CITYCODE, province);
                input.Add(SmartPortal.Constant.IPC.BANKID, bank);
                input.Add(SmartPortal.Constant.IPC.BRANCHID, branch);
                input.Add("BRANCHDESC", branchDesc);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteReceiverList(string ID,string sUserID, ref string errorCode, ref string errorDesc)
        {
            DataSet result = new DataSet();
            Hashtable hasOutput = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                Hashtable output = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000108");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add("ID", ID);
                input.Add(SmartPortal.Constant.IPC.USERID, sUserID);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region AcceptList
        public DataSet GetAcceptList(string ContractNo, string SenderName, string AcctNo, string Description, ref string errorCode, ref string errorDesc)
        {
            DataSet result = new DataSet();
            try
            {
                Hashtable input = new Hashtable();
                Hashtable output = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000120");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.CONTRACTNO, ContractNo);
                input.Add("ACCTNO", AcctNo);
                input.Add("SENDERNAME", SenderName);
                input.Add(SmartPortal.Constant.IPC.DESCRIPTION, Description);
                output = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);

                if (output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    result = (DataSet)output[SmartPortal.Constant.IPC.DATASET];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void InsertAcceptList(string ContractNo, string SenderName, string AcctNo, string Description, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable input = new Hashtable();
                Hashtable output = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000121");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.CONTRACTNO, ContractNo);
                input.Add("ACCTNO", AcctNo);
                input.Add("SENDERNAME", SenderName);
                input.Add(SmartPortal.Constant.IPC.DESCRIPTION, Description);
                output = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);

                errorCode = output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errorDesc = output[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DelAcceptList(string ContractNo, string AcctNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable input = new Hashtable();
                Hashtable output = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000122");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.CONTRACTNO, ContractNo);
                input.Add("ACCTNO", AcctNo);
                output = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);

                errorCode = output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errorDesc = output[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region UPDATE STATUS ACCOUNT EBA_CONTRACTACCOUNT
        public void UpdateStatusAccount(string acctNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000124");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.ACCOUNT, acctNo);
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        // TRANSACTION GET BANKLIST
        public DataSet GetBankList(string cityCode, string bankID)
        {
            DataSet result = new DataSet();
            try
            {
                Hashtable input = new Hashtable();
                Hashtable output = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000205");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);

                input.Add(SmartPortal.Constant.IPC.CITYCODE, cityCode);
                input.Add(SmartPortal.Constant.IPC.BANKID, bankID);

                output = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);

                if (output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    result = (DataSet)output[SmartPortal.Constant.IPC.DATASET];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public DataSet GetBranch(string branchid)
        {
            DataSet result = new DataSet();
            try
            {
                Hashtable input = new Hashtable();
                Hashtable output = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000211");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
                output = SmartPortal.RemotingServices.AT.DBTRAN().ProcessOnlyHAS(input);

                if (output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    result = (DataSet)output[SmartPortal.Constant.IPC.DATASET];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        //TRANSACTION GET CITY
        public DataSet GetCity()
        {
            DataSet result = new DataSet();
            try
            {
                Hashtable input = new Hashtable();
                Hashtable output = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000207");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                output = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);

                if (output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    result = (DataSet)output[SmartPortal.Constant.IPC.DATASET];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool CheckSameAccount(string SenderAccount, string ReceiverAccount)
        {
            bool result = false;
            try
            {
                if (SenderAccount.Equals(ReceiverAccount)) result = false;
                else result = true;
            }
            catch (Exception ex)
            { }
            return result;
        }
        public bool CheckSameCCYCD(string sendCCYCD, string receiverCCYCD)
        {
            bool result = false;
            try
            {
                if (sendCCYCD.Equals(receiverCCYCD)) result = true;
                else result = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #region Transaction History
        public DataSet getTransactionHis(string userID, string trancodeToRight, string AcctNo, string FromDate, string ToDate, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000101");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODETORIGHT, trancodeToRight);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, AcctNo);
                hasInput.Add(SmartPortal.Constant.IPC.FROMDATE, FromDate);
                hasInput.Add(SmartPortal.Constant.IPC.TODATE, ToDate);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput["DATARESULT"];
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

        #region Get INFO DD
        public DataSet GetInfoDD(string UserID, string AcctNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000100");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, UserID);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, AcctNo);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput["DATARESULT"];
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

        #region Get full info Account by user
        public DataSet GetInfoAcct(string userID, string trancodeToRight, string acctType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000104");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODETORIGHT, trancodeToRight);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTTYPE, acctType);
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

        #region Get INFO FD
        public DataSet GetFDAcctInfo(string UserID, string AcctNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000102");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, UserID);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, AcctNo);
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
        #region Get INFO LN
        public DataSet GetLNAcctInfo(string UserID, string AcctNo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000103");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, UserID);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, AcctNo);
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

        #region Get Schedule pay Loan
        public DataSet GetLNSchedulePayment(string UserID, string AcctNo, string FromDate, string ToDate, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000105");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, UserID);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, AcctNo);
                hasInput.Add(SmartPortal.Constant.IPC.FROMDATE, FromDate);
                hasInput.Add(SmartPortal.Constant.IPC.TODATE, ToDate);
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

        #region  DetailsAcceptList
        public DataSet DetailsAcceptList(string ContractNo, string AcctNo, ref string errorCode, ref string errorDesc)
        {
            DataSet result = new DataSet();
            try
            {
                Hashtable input = new Hashtable();
                Hashtable output = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000027");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, "search accepter");
                input.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                input.Add(SmartPortal.Constant.IPC.CONTRACTNO, ContractNo);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, AcctNo);
                output = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
                //

                if (output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    result = (DataSet)output[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = output[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = output[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Check TK duoc dang ky EB
        public DataSet GetNewAcct(string BranchID, string ContractNo, string CustID, string CustCode
            , string CFType, string FullName, string CreateDate)
        {
            DataSet ds = new DataSet();
            try
            {
                Hashtable input = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS000003");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                input.Add(SmartPortal.Constant.IPC.BRANCHID, BranchID);
                input.Add(SmartPortal.Constant.IPC.CONTRACTNO, ContractNo);
                input.Add(SmartPortal.Constant.IPC.CUSTID, CustID);
                input.Add(SmartPortal.Constant.IPC.CUSTCODE, CustCode);
                input.Add(SmartPortal.Constant.IPC.CFTYPE, CFType);
                input.Add(SmartPortal.Constant.IPC.FULLNAME, FullName);
                input.Add(SmartPortal.Constant.IPC.CREATEDATE, CreateDate);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
                ds = (DataSet)hasOutput["DATASET"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetInActiveAcct(string LAccount)
        {
            DataSet ds = new DataSet();
            try
            {
                Hashtable input = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMS000002");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, LAccount);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
                ds = (DataSet)hasOutput["DATARESULT"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetInActiveAcctInfo(string BranchID, string ContractNo, string CustID, string CustCode
            , string CFType, string FullName, string CreateDate)
        {
            DataSet ds;
            DataTable result = new DataTable();
            DataSet dsResult = new DataSet();
            try
            {
                ds = GetNewAcct(BranchID, ContractNo, CustID, CustCode, CFType, FullName, CreateDate);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    string LAccount = string.Empty;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i > 0)
                            LAccount = LAccount + "," + dt.Rows[i]["ACCTNO"].ToString();
                        else
                            LAccount = dt.Rows[i]["ACCTNO"].ToString();
                    }
                    //LAccount = "3064608,2996108";
                    DataSet ds1;
                    if (LAccount != string.Empty)
                    {
                        ds1 = GetInActiveAcct(LAccount);
                        if (ds1.Tables.Count > 0)
                        {
                            string expression = "ACCTNO in (";
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                DataRow[] rows;

                                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                                {
                                    if (j != ds1.Tables[0].Rows.Count - 1)
                                        expression = expression + "'" + ds1.Tables[0].Rows[j]["accountno"].ToString() + "',";
                                    else
                                        expression = expression + "'" + ds1.Tables[0].Rows[j]["accountno"].ToString() + "'";
                                }
                                expression += ")";
                                rows = dt.Select(expression);
                                //dt.Rows.Clear();
                                result = dt.Copy();
                                result.Rows.Clear();
                                foreach (DataRow dr in rows)
                                {
                                    result.ImportRow(dr);

                                }
                                dsResult.Tables.Add(result);
                                return dsResult;
                            }
                            else
                                return null;

                        }
                        else
                            return null;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
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
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000117");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODE, CustCode);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTTYPE, CustType);
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

        public void InsertNewAcct(string Account, string AcctType, string Currency, string BranchID, string Status, string CustCode, string CustType, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000118");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, Account);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTTYPE, AcctType);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, Currency);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, BranchID);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, Status);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODE, CustCode);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTTYPE, CustType);
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
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000119");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, Account);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, Status);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                //DataSet ds = new DataSet();

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
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000501");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, Account);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTTYPE, AcctType);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, Currency);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, BranchID);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODE, CustCode);
                hasInput.Add(SmartPortal.Constant.IPC.CUSTTYPE, CustType);
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
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000125");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, UserID);
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

        #region cap nhat so lan dang nhap sai
        public DataTable UpdateFailLogin(string username, string failNumber)
        {

            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@username";
            p1.Value = username;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@failnumber";
            p2.Value = failNumber;
            p2.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("IB_UpdateFailLogin", p1, p2);


            return iRead;
        }
        #endregion

        #region Payment water
        public Hashtable WaterBillPayment(string UserID, string SenderAccount, string ReceiverAccount, string amount, string CCYID, string senderName, string receiverName, string debitBrachID, string creditBrachID, string Desc, string authentype, string authencode, string fee, string PaymentProviderID, string PaymentTypeID, string DirectNum, string KeyID, string addresscholon)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000308");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, ReceiverAccount);
                input.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                input.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                input.Add(SmartPortal.Constant.IPC.FEE, fee);
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                input.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receiverName);
                input.Add(SmartPortal.Constant.IPC.CREDITBRACHID, creditBrachID);
                input.Add(SmartPortal.Constant.IPC.DEBITBRACHID, debitBrachID);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, Desc);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);
                input.Add(SmartPortal.Constant.IPC.PAYMENTPROVIDERID, PaymentProviderID);
                input.Add(SmartPortal.Constant.IPC.PAYMENTTYPEID, PaymentTypeID);
                input.Add(SmartPortal.Constant.IPC.DIRECTNUM, DirectNum);
                input.Add(SmartPortal.Constant.IPC.KEYID, KeyID);
                input.Add(SmartPortal.Constant.IPC.ADDRESSCHOLON, addresscholon);


                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        // TRANSACTION REPAID MASTERCARD NEW
        public Hashtable TransferDDNewRepaidMasterCard(string UserID, string SenderAccount, string ReceiverAccount, string amount, string CCYID, string senderName, string receiverName, string debitBrachID, string creditBrachID, string Desc, string authentype, string authencode, string fee)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000800");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, ReceiverAccount);
                input.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                input.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                input.Add(SmartPortal.Constant.IPC.FEE, fee);
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                input.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receiverName);
                input.Add(SmartPortal.Constant.IPC.CREDITBRACHID, creditBrachID);
                input.Add(SmartPortal.Constant.IPC.DEBITBRACHID, debitBrachID);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, Desc);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);

                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        // TRANSACTION REPAID MASTERCARD ADD
        public Hashtable TransferDDAddRepaidMasterCard(string UserID, string SenderAccount, string ReceiverAccount, string amount, string CCYID, string senderName, string receiverName, string debitBrachID, string creditBrachID, string Desc, string authentype, string authencode, string fee)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000801");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, ReceiverAccount);
                input.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                input.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                input.Add(SmartPortal.Constant.IPC.FEE, fee);
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                input.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receiverName);
                input.Add(SmartPortal.Constant.IPC.CREDITBRACHID, creditBrachID);
                input.Add(SmartPortal.Constant.IPC.DEBITBRACHID, debitBrachID);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, Desc);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);

                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        // CHECK VALIDATE REPAID MASTERCARD NEW
        public Hashtable CheckValidateRepaidMasterCard(string CREATEID, string NAME)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000802");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.CREATEID, CREATEID);
                input.Add(SmartPortal.Constant.IPC.NAME, NAME);

                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #region get status of account login IB
        public DataTable GetAccountStatus(string username, string password)
        {

            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@username";
            p1.Value = username;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@password";
            p2.Value = password;
            p2.SqlDbType = SqlDbType.Text;

            iRead = DataAccess.GetFromDataTable("IB_GETStatusACCTNO", p1, p2);


            return iRead;
        }
        #endregion

        public Hashtable BuyTopupOnline(string UserID, string SenderAccount, string ReceiverAccount, string CCYID, string senderName, string receiverName, string debitBrachID, string creditBrachID, string Desc, string authentype, string authencode, string telco, string amount, string PhoneNo)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                //SerialNo for Ooredoo with format: Sign + yyyyMMddhhmmss + 6 digits serial number For example: T20120809101010002313
                Random generator = new Random();
                String r = generator.Next(0, 1000000).ToString("D6");
                string SeriaNo = "T" + DateTime.Now.ToString("yyyyMMddhhmmss") + r;

                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000035");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                input.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receiverName);
                input.Add(SmartPortal.Constant.IPC.CREDITBRACHID, creditBrachID);
                input.Add(SmartPortal.Constant.IPC.DEBITBRACHID, debitBrachID);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, Desc);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);
                input.Add(SmartPortal.Constant.IPC.TELCO, telco);
                input.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                input.Add(SmartPortal.Constant.IPC.BUYRATE, amount);
                input.Add(SmartPortal.Constant.IPC.PHONENO, PhoneNo);
                input.Add(SmartPortal.Constant.IPC.SERIALNO, SeriaNo);
                //input.Add(SmartPortal.Constant.IPC.SessionID, SessionID);

                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Hashtable LoginTelenor()
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "TOP0007");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);

                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public DataTable UpdateFailLoginSEMS(string username, string failNumber)
        {
            return DataAccess.GetFromDataTable("SEMS_UpdateFailLogin", new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "@username",
                    Value = username,
                    SqlDbType = SqlDbType.Text
                },
                new SqlParameter
                {
                    ParameterName = "@failnumber",
                    Value = failNumber,
                    SqlDbType = SqlDbType.Text
                }
            });
        }

        public DataTable UpdateUUID(string servicelogin, string username, string UUID)
        {
            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@username";
            p2.Value = username;
            p2.SqlDbType = SqlDbType.Text;
            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@UUID";
            p3.Value = UUID;
            p3.SqlDbType = SqlDbType.Text;
            return DataAccess.GetFromDataTable("IB_UpdateUUID", new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "@serviceLogin",
                    Value = servicelogin,
                    SqlDbType = SqlDbType.Text
                },
                p2,
                p3
            });
        }


        public Hashtable FXTransfer(string UserID, string SenderAccount, string ReceiverAccount, string amountDe, string amountCe, string amountBCY, string fromCCYID,
             string toCCYID, string sDebitRate, string sCreditRate, string sCrossRate,
             string senderName, string receiverName, string debitBrachID, string creditBrachID, string Desc, string authentype,
             string authencode, string fee, string feepayer)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                string revfee = (feepayer.Equals("Receiver")) ? fee : "0";
                string sedfee = (feepayer.Equals("Sender")) ? fee : "0";

                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000405");
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, ReceiverAccount);
                input.Add(SmartPortal.Constant.IPC.AMOUNT, amountDe);
                input.Add(SmartPortal.Constant.IPC.FROMCCYID, fromCCYID);
                input.Add(SmartPortal.Constant.IPC.TOCCYID, toCCYID); /////////
                input.Add("CRAMOUNT", amountCe); /////////
                input.Add("CBAMT", amountBCY); /////////
                input.Add("PGEXR", sDebitRate); /////////
                input.Add("CTXEXR", sCreditRate); /////////
                input.Add("CCRRATE", sCrossRate); /////////
                input.Add(SmartPortal.Constant.IPC.SEDFEE, sedfee);
                input.Add(SmartPortal.Constant.IPC.REVFEE, Convert.ToDouble(revfee) * Convert.ToDouble(sDebitRate));
                input.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                input.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receiverName);
                input.Add(SmartPortal.Constant.IPC.CREDITBRACHID, creditBrachID);
                input.Add(SmartPortal.Constant.IPC.DEBITBRACHID, debitBrachID);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, Desc);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);
                input.Add(SmartPortal.Constant.IPC.CCYID, fromCCYID);



                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }




    }
}
