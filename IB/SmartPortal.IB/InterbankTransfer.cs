using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using SmartPortal.DAL;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace SmartPortal.IB
{
    public class InterbankTransfer
    {
        public DataTable LoadFeeCBMNet()
        {
            return DataAccess.FillDataTable("GETCBMNETFEE");
        }
        public Hashtable IBInterbankTransfer(string UserID, string authenType, string authenCode, string SenderAccount,
            string trandesc, string sendername, string didnumber, string senderAddress, string receivername, string cidnumber, string receiverAddress
            , string receiveracctno, string senderBranch, string rcvbranchcode, string rcvbranchname, string amount, string cciyd,DataTable tbldocument,string contracttype)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                input.Add(Constant.IPC.IPCTRANCODE, "IBINTERBANKTRANSFER");
                input.Add(Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(Constant.IPC.SERVICEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(Constant.IPC.TRANDESC, trandesc);
                input.Add(Constant.IPC.USERID, UserID);
                input.Add(Constant.IPC.SENDERNAME, sendername);
                input.Add("DIDNUMBER", didnumber);
                input.Add(Constant.IPC.SENDERADDRESS, senderAddress);
                input.Add(Constant.IPC.ACCTNO, SenderAccount);
                input.Add(Constant.IPC.CCYID, cciyd);
                input.Add(Constant.IPC.BRANCHNAME, senderBranch);
                input.Add(Constant.IPC.RECEIVERNAME, receivername);
                input.Add("CIDNUMBER", cidnumber);
                input.Add(Constant.IPC.RECEIVERADD, receiverAddress);
                input.Add(Constant.IPC.RECEIVERACCOUNT, receiveracctno);
                input.Add("RCVBANKCODE", rcvbranchcode);
                input.Add("RCVBANKNAME", rcvbranchname);

                input.Add(Constant.IPC.AMOUNT, amount);
                input.Add(Constant.IPC.AUTHENTYPE, authenType);
                input.Add(Constant.IPC.AUTHENCODE, authenCode);
                if (tbldocument != null)
                {
                    input.Add(SmartPortal.Constant.IPC.DOCUMENT, tbldocument);
                }
                input.Add(SmartPortal.Constant.IPC.CONTRACTTYPE, contracttype);

                result = RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
