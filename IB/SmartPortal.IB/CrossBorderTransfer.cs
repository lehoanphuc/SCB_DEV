using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPortal.IB
{
    public class CrossBorderTransfer
    {
        public Hashtable IBCrossBorderTransfer(string UserId, string trancode, string authenType, string authenCode,
            string SenderAccount, string SenderName, string SenderBranchID, string ReceiverAccount, string ReceiverCountryName, string ReceiverCountryID, string ReceiverBankId,
            string ReceiverBankName, string ReceiverName, string ReceiverPhone, string ReceiverAddress,
            string Amount, string Ccyid, string Desc, ref string errorCode, ref string errorDesc, string document)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                input.Add(Constant.IPC.IPCTRANCODE, trancode);
                input.Add(Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(Constant.IPC.SERVICEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(Constant.IPC.TRANDESC, Desc);
                //insert document
                object[] insertdocument = new object[2];
                insertdocument[0] = "CB_UPDATEDOCUMENT";

                DataTable insert = new DataTable();



                input.Add(Constant.IPC.USERID, UserId);
                input.Add(Constant.IPC.SENDERNAME, SenderName);
                input.Add("BRANCHID", SenderBranchID);
                input.Add(Constant.IPC.ACCTNO, SenderAccount);
                input.Add(Constant.IPC.CCYID, Ccyid);
                input.Add(Constant.IPC.RECEIVERNAME, ReceiverName);
                input.Add("RECEIVERPHONE", ReceiverPhone);
                input.Add("RECEIVERCOUNTRYNAME", ReceiverCountryName);
                input.Add("RECEIVERCOUNTRYID", ReceiverCountryID);
                input.Add("RECEIVERADDRESS", ReceiverAddress);
                input.Add(Constant.IPC.RECEIVERACCOUNT, ReceiverAccount);
                input.Add("RECEIVERBANKCODE", ReceiverBankId);
                input.Add("RECEIVERBANKNAME", ReceiverBankName);

                input.Add(Constant.IPC.AMOUNT, Amount);
                input.Add(Constant.IPC.AUTHENTYPE, authenType);
                input.Add(Constant.IPC.AUTHENCODE, authenCode);
                input.Add("DOCUMENT", document);

                result = RemotingServices.AT.DBTRAN().ProcessTransHAS(input);

                if (result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                }
                else
                {
                    errorCode = result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Hashtable CB_LOGDOCUMENT(DataTable document, ref string errorCode, ref string errorDesc)
        {
            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();
            try
            {
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "CB_LOG_DOCUMENT");
                hasInput.Add(Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(Constant.IPC.SERVICEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Save Document");

                object[] insertdocument = new object[2];
                insertdocument[0] = "CB_UPDATEDOCUMENT";

                DataTable insert = new DataTable();

                insert = document;
                insertdocument[1] = insert;
                hasInput.Add("INSERTDOCUMENT", insertdocument);

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
        public DataSet LoadAllCountry(string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSLOADCOUNTRYNAME");
                hasInput.Add(Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(Constant.IPC.SERVICEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "LOAD ALL COUNTRY");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);

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
