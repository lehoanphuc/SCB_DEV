using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;
using System.Collections;
namespace SmartPortal.IB
{
   public class CreditCard
    {
       public DataSet GetCardlistfromEbcore(string userid, string type,string trancode, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000620");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "get credit card info");

               hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
               hasInput.Add(SmartPortal.Constant.IPC.TYPE, type);
               hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
               
              



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

           //DataTable iRead;

           //SqlParameter p1 = new SqlParameter();
           //p1.ParameterName = "@type";
           //p1.Value = type;
           //p1.SqlDbType = SqlDbType.Text;

           //iRead = DataAccess.GetFromDataTable("CR_GET_CARDLIST", p1);


           //return iRead;
       }
       public Hashtable GetCardInfo(string messageId, string hostId, string cardNo, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000621");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "get credit card info");

               //hasInput.Add(SmartPortal.Constant.IPC.MESSAGEID, messageId);
              // hasInput.Add(SmartPortal.Constant.IPC.HOSTID, hostId);
               hasInput.Add(SmartPortal.Constant.IPC.CARDNO, cardNo);

            

               hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

               Hashtable ds = new Hashtable();

               if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
               {
                   ds = hasOutput;
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
       public Hashtable UpdateCardStatus(string userid, string messageId, string hostId, string cardNo, string cardPlasticCode,string authenType, string authenCode, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000622");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "update credit card status");

               hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
               hasInput.Add(SmartPortal.Constant.IPC.MESSAGEID, messageId);
               hasInput.Add(SmartPortal.Constant.IPC.HOSTID, hostId);
               hasInput.Add(SmartPortal.Constant.IPC.CARDNO, cardNo);
               hasInput.Add(SmartPortal.Constant.IPC.CARDPLASTICCODE, cardPlasticCode);
               hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authenType);
               hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, authenCode);



               hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

               Hashtable ds = new Hashtable();

               if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
               {
                   ds = hasOutput;
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
       public Hashtable FinanceAdj(string trancode, string CCYID,string userid,  string DEBITACCTNO,  string AMOUNT, int BRANCHID, string DESC, int TRANREF,
           string cardNo, string cifNo, string authenType, string authenCode, string fee, string feepayer, string receiverName, string senderName//, string outstandingamount
           )
       {
           try
           {
               string revfee = (feepayer.Equals("Receiver")) ? fee : "0";
               string sedfee = (feepayer.Equals("Sender")) ? fee : "0";

               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, trancode);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               //hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Credit payment own card");

               //corebanking
               hasInput.Add(SmartPortal.Constant.IPC.CCYID, CCYID);
               hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
               hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, DEBITACCTNO);
               //hasInput.Add(SmartPortal.Constant.IPC.GLACNO, GLACNO);
               hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, AMOUNT);
               hasInput.Add(SmartPortal.Constant.IPC.DEBITBRACHID, BRANCHID);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, DESC);
              // hasInput.Add(SmartPortal.Constant.IPC.TRANREF, TRANREF);
               hasInput.Add(SmartPortal.Constant.IPC.SEDFEE, sedfee);
               hasInput.Add(SmartPortal.Constant.IPC.REVFEE, revfee);
               hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authenType);
               hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, authenCode);
               //cardzone
              // hasInput.Add(SmartPortal.Constant.IPC.MESSAGEID, messageId);
              // hasInput.Add(SmartPortal.Constant.IPC.HOSTID, hostId);
               hasInput.Add(SmartPortal.Constant.IPC.CARDNO, cardNo);
               hasInput.Add(SmartPortal.Constant.IPC.cifNo, cifNo);
                //hasInput.Add(SmartPortal.Constant.IPC.adjAmt, adjAmt);
                //hasInput.Add(SmartPortal.Constant.IPC.currId, currId);
                //hasInput.Add(SmartPortal.Constant.IPC.txnType, txnType);
                // hasInput.Add(SmartPortal.Constant.IPC.desc, desc);
                hasInput.Add(SmartPortal.Constant.IPC.RECEIVERNAME, receiverName);
                hasInput.Add(SmartPortal.Constant.IPC.SENDERNAME, senderName);
                //hasInput.Add("OUTSTANDINGAMOUNT", outstandingamount);


                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

               Hashtable ds = new Hashtable();
               ds = hasOutput;
               

               return ds;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public DataTable CR_GetCarddetail(string userid,string cardno)
       {
           try
           { 
           DataTable iRead;

           SqlParameter p1 = new SqlParameter();
           p1.ParameterName = "@userid";
           p1.Value = userid;
           p1.SqlDbType = SqlDbType.Text;
           SqlParameter p2 = new SqlParameter();
           p2.ParameterName = "@cardno";
           p2.Value = cardno;
           p2.SqlDbType = SqlDbType.Text;

           iRead = DataAccess.GetFromDataTable("CR_GET_CARDDETAIL", p1,p2);


           return iRead;
               }
           catch(Exception ex)
           {
               throw ex;
           }
       }
      
       public DataSet GetTenTransactions(string messageId, string hostId, string cardNo, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000625");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "get last ten transactions");

               hasInput.Add(SmartPortal.Constant.IPC.MESSAGEID, messageId);
               hasInput.Add(SmartPortal.Constant.IPC.HOSTID, hostId);
               hasInput.Add(SmartPortal.Constant.IPC.CARDNO, cardNo);


               hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

               //DataTable ds = new DataTable();
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
       public DataSet CheckCardlistfromEbcore(string userid, string type, string trancode, string cardno, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000627");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "check credit card info");

               hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
               hasInput.Add(SmartPortal.Constant.IPC.TYPE, type);
               hasInput.Add(SmartPortal.Constant.IPC.TRANCODE, trancode);
               hasInput.Add(SmartPortal.Constant.IPC.CARDNO, cardno);

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
