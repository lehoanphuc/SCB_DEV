using SmartPortal.Constant;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SmartPortal.SEMS
{
    public class BankUltility
    {
        public DataSet BankSearch(string bankID, string bankName, string bankCode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "SEMSBANKSEARCH");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.BANKID, bankID);
                hasInput.Add(IPC.BANKNAME, bankName);
                hasInput.Add(IPC.BANKCODE, bankCode);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Equals("0"))
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

        public DataSet BankSender(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "SEMSBANKSENDER");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                if (errorCode.Equals("0"))
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                }
                else
                {
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BankGetByBankID(string bankID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "SEMSBANKGETBYID");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.BANKID, bankID);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Equals("0"))
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

        public DataSet BankInsert(string bankName, string bankCode, string rippleURI, string acceptPath, string rejectPath,
            string status, string userCreated, string dateCreated, string isSender, string countryCode, string autoGetQuote,
            string autoAcceptQuote, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "SEMSBANKINSERT");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                //hasInput.Add(IPC.BANKID, bankID);
                hasInput.Add(IPC.BANKNAME, bankName);
                hasInput.Add(IPC.BANKCODE, bankCode);
                hasInput.Add(IPC.RIPPLEURI, rippleURI);
                hasInput.Add(IPC.MT103ACCEPTPATH, acceptPath);
                hasInput.Add(IPC.MT103REJECTPATH, rejectPath);
                hasInput.Add(IPC.STATUS, status);
                hasInput.Add(IPC.USERCREATED, userCreated);
                hasInput.Add(IPC.DATECREATED, dateCreated);
                hasInput.Add(IPC.ISSENDER, isSender);
                hasInput.Add(IPC.COUNTRYCODE, countryCode);
                hasInput.Add(IPC.AUTOGETQUOTE, autoGetQuote);
                hasInput.Add(IPC.AUTOACCEPTQUOTE, autoAcceptQuote);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Equals("0"))
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

        public DataSet BankUpdate(string bankID, string bankName, string bankCode, string rippleURI, string acceptPath, string rejectPath,
            string status, string userModified, string dateModified, string isSender, string countryCode, string autoGetQuote,
            string autoAcceptQuote, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "SEMSBANKUPDATE");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.BANKID, bankID);
                hasInput.Add(IPC.BANKNAME, bankName);
                hasInput.Add(IPC.BANKCODE, bankCode);
                hasInput.Add(IPC.RIPPLEURI, rippleURI);
                hasInput.Add(IPC.MT103ACCEPTPATH, acceptPath);
                hasInput.Add(IPC.MT103REJECTPATH, rejectPath);
                hasInput.Add(IPC.STATUS, status);
                hasInput.Add(IPC.USERMODIFIED, userModified);
                hasInput.Add(IPC.DATEMODIFIED, dateModified);
                hasInput.Add(IPC.ISSENDER, isSender);
                hasInput.Add(IPC.COUNTRYCODE, countryCode);
                hasInput.Add(IPC.AUTOGETQUOTE, autoGetQuote);
                hasInput.Add(IPC.AUTOACCEPTQUOTE, autoAcceptQuote);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Equals("0"))
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

        public DataSet BankDelete(string bankID, string bankName, string bankCode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "SEMSBANKDELETE");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.BANKID, bankID);
                hasInput.Add(IPC.BANKNAME, bankName);
                hasInput.Add(IPC.BANKCODE, bankCode);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Equals("0"))
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

        //cuongtnp 221019
        public DataSet ChannelSearch(string bankID, string channelName, string channelCode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "SEMSCHANNELSEARCH");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.BANKID, bankID);
                hasInput.Add(IPC.CHANNELNAME, channelName);
                hasInput.Add(IPC.CHANNELCODE, channelCode);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Equals("0"))
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
        public DataSet GetPaged(string bankID, string bankName, string bankCode, int recPerPage, int recIndex, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add(IPC.IPCTRANCODE, "EBA_BANK_GetPaged");
                hasInput.Add(IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(IPC.BANKID, bankID);
                hasInput.Add(IPC.BANKNAME, bankName);
                hasInput.Add(IPC.BANKCODE, bankCode);
                hasInput.Add("RecPerPage", recPerPage);
                hasInput.Add("RecIndex", recIndex);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Equals("0"))
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
