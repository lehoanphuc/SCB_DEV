using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace SmartPortal.IB
{
    public class Transfer
    {
        #region Search template transfer by condition
        public DataSet Load(string TempTransferName, string trancode, string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000017");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load Template Transfer");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TEMPLATENAME, TempTransferName);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);

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

        #region Load Tranfer type
        public DataSet LoadTranferType(string TransferValue, string TranferName, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000109");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Load Transfer Type");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TRANFERVALUE, TransferValue);
                hasInput.Add(SmartPortal.Constant.IPC.TRANFERNAME, TranferName);


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

        #region Insert Transfer template
        public DataSet InsertTransferTemplate(string TEMPLATENAME, string DESCRIPTION, string SENDERACCOUNT, string RECEIVERACCOUNT, string AMOUNT, string CCYID, string IPCTRANCODE, string USERID, string EXECNOW, string EXECDATE, string CHARGEFEE, string CITYCODE, string COUNTRYCODE, string IDENTIFYNO, string ISSUEDATE, string ISSUEPLACE, string RECEIVERNAME, string SENDERNAME, string BANKCODE, string ReceiverAdd, string branch, string BranchDesc, string ReceiverID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000018");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Insert Transfer template");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                //
                hasInput.Add(SmartPortal.Constant.IPC.TEMPLATENAME, TEMPLATENAME);
                hasInput.Add(SmartPortal.Constant.IPC.DESCRIPTION, DESCRIPTION);
                hasInput.Add(SmartPortal.Constant.IPC.SENDERACCOUNT, SENDERACCOUNT);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, RECEIVERACCOUNT);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, AMOUNT);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, IPCTRANCODE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, USERID);
                hasInput.Add(SmartPortal.Constant.IPC.EXECNOW, EXECNOW);
                hasInput.Add(SmartPortal.Constant.IPC.EXECDATE, EXECDATE);
                hasInput.Add(SmartPortal.Constant.IPC.CHARGEFEE, CHARGEFEE);
                hasInput.Add(SmartPortal.Constant.IPC.CITYCODE, CITYCODE);
                hasInput.Add(SmartPortal.Constant.IPC.COUNTRYCODE, COUNTRYCODE);
                hasInput.Add(SmartPortal.Constant.IPC.IDENTIFYNO, IDENTIFYNO);
                hasInput.Add(SmartPortal.Constant.IPC.ISSUEDATE, ISSUEDATE);
                hasInput.Add(SmartPortal.Constant.IPC.ISSUEPLACE, ISSUEPLACE);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERNAME, RECEIVERNAME);
                hasInput.Add(SmartPortal.Constant.IPC.SENDERNAME, SENDERNAME);
                hasInput.Add(SmartPortal.Constant.IPC.BANKCODE, BANKCODE);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERADD, ReceiverAdd);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branch);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHDESC, BranchDesc);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERID, ReceiverID);

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

        #region SELECT TEMPLATE by TEMPLATEID
        public DataSet LoadtemplateByID(string TemplateID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000019");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "SELECT TEMPLATE by TEMPLATEID");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TEMPLATEID, TemplateID);

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

        #region delete TEMPLATE TRANSFER by ID
        public DataSet DeleteTemplateByID(string TempId, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000020");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "delete tempolate transfer");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.TEMPLATEID, TempId);

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

        #region check name transfer template
        public DataSet CheckNameTransferTemplate(string templatename, string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000034");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "delete tempolate transfer");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);

                hasInput.Add(SmartPortal.Constant.IPC.TEMPLATENAME, templatename);

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

        #region Get Rate FX
        public Hashtable GetRateFx(string amount, string senderccyid, string receiverccyid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "MB_GETFXRATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add("FXAMOUNT", amount);
                hasInput.Add("FCCY", senderccyid);
                hasInput.Add("TCCY", receiverccyid);
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
        #endregion

        #region Transfer FX
        public Hashtable TransferFx(string trancode, string userid, string debitacct, string creditacct, string creditname, string amount, string ccyid, string branchID, string creditbranchID, string trandesc, string authencode, string authentype, string senderName, string phoneno, string receiverccyid, string exchangerate, string fxamount, ref string errorCode, ref string errorDesc,DataTable tbldocument,string contracttype)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, debitacct);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, creditacct);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERNAME, creditname);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchID);
                hasInput.Add(SmartPortal.Constant.IPC.CREDITBRACHID, creditbranchID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, trandesc);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                hasInput.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, phoneno);               
                hasInput.Add("TCCY", receiverccyid);
                hasInput.Add("EXCHANGERATE", exchangerate);
                hasInput.Add("FXAMOUNT", fxamount);
                hasInput.Add("FCCY", ccyid);
                if (tbldocument != null)
                {
                    hasInput.Add(SmartPortal.Constant.IPC.DOCUMENT, tbldocument);
                }
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTTYPE, contracttype);
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
        #endregion

        #region Check Amount Payment
        public Hashtable CheckAmountFx(string UserID, string tranright, string senderaccount, string receiveraccount, string amount, string fccy, string toccy, string langid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "MB_CHECKAMTTRANFX");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add("IPCTRANCODERIGHT", tranright);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, UserID);
                hasInput.Add("ACCTNO", senderaccount);
                hasInput.Add("RECEIVERACTNO", receiveraccount);
                hasInput.Add("AMOUNT", amount);
                hasInput.Add("FCCY", fccy);
                hasInput.Add("TCCY", toccy);
                hasInput.Add(SmartPortal.Constant.IPC.LANG, langid);
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
        #endregion

        #region SELECT Western
        public DataSet GetWesternUnionTransfer(string trancode, string senderphone, string sendername, string senderaddress, string cciyd, string status, string date, int pagesize, int pageindex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB_GETWESTERN");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add("CHECKID", trancode);
                hasInput.Add("SENDERPHONE", senderphone); ;
                hasInput.Add("SENDERNAME", sendername);
                hasInput.Add("SENDERADDRESS", senderaddress);
                hasInput.Add("CCYID", cciyd);
                hasInput.Add("STATUS", status);
                hasInput.Add("DATE", date);
                hasInput.Add("PAGESIZE", pagesize);
                hasInput.Add("PAGEINDEX", pageindex);

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


        #region Transfer Western union
        public Hashtable TranferWestern(string trancode, string userid, string debitacct, string creditacct, string creditname, string amount, string ccyid, string branchID, string trandesc, string authencode, string authentype, string senderName, string phoneno, string phonereceiver, string receiveraddress, string quest, string answer, string memo, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, trancode);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, debitacct);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERACCOUNT, creditacct);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERNAME, creditname);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, trandesc);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                hasInput.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                hasInput.Add(SmartPortal.Constant.IPC.PHONENO, phoneno);
                hasInput.Add("PHONERECEIVER", phonereceiver);
                hasInput.Add("RECEIVERADDRESS", receiveraddress);
                hasInput.Add("QUESTION", quest);
                hasInput.Add("ANSWER", answer);
                hasInput.Add("MEMO", memo);
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
        #endregion

        #region Transfer Non Wallet account
        public Hashtable TranferNonWalletAccount(string ipctrancode, string UserID, string SenderAccount, string ReceiverName, string CCYID, string receiverPhone, string Amount, string Desc, string autype, string aucode, string phoneno,DataTable tbldocument , string contracttype)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                input.Add(SmartPortal.Constant.IPC.IPCTRANCODE, ipctrancode);
                input.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(SmartPortal.Constant.IPC.USERID, UserID);
                input.Add(SmartPortal.Constant.IPC.ACCTNO, SenderAccount);
                input.Add(SmartPortal.Constant.IPC.RECEIVERNAME, ReceiverName);
                input.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
                input.Add("TBRANCHID", string.Empty);
                input.Add("RCVPHONENO", receiverPhone);
                input.Add(SmartPortal.Constant.IPC.AMOUNT, Amount);
                input.Add(SmartPortal.Constant.IPC.TRANDESC, Desc);
                input.Add(SmartPortal.Constant.IPC.AUTHENTYPE, autype);
                input.Add(SmartPortal.Constant.IPC.AUTHENCODE, aucode);
                input.Add("TCITY", string.Empty);
                input.Add(SmartPortal.Constant.IPC.PHONENO, phoneno);
                if (tbldocument != null)
                {
                    input.Add(SmartPortal.Constant.IPC.DOCUMENT, tbldocument);
                }
                input.Add(SmartPortal.Constant.IPC.CONTRACTTYPE, contracttype);
                result = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion
    }
}
