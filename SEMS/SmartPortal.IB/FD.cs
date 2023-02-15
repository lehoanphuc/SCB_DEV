using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;
using System.Collections;

namespace SmartPortal.IB
{
   public class FD
    {
       public DataTable LoadFDProduct(string id)
       {
           
           DataTable iRead;

           SqlParameter p1 = new SqlParameter();
           p1.ParameterName = "@id";
           p1.Value = id;
           p1.SqlDbType = SqlDbType.Text;



           iRead = DataAccess.GetFromDataTable("eba_loadfdproductbyid", p1);


           return iRead;
       }

       public DataTable LoadFDAccount(string userid)
       {

           DataTable iRead;

           SqlParameter p1 = new SqlParameter();
           p1.ParameterName = "@userid";
           p1.Value = userid;
           p1.SqlDbType = SqlDbType.Text;



           iRead = DataAccess.GetFromDataTable("eba_loadfdonline", p1);


           return iRead;
       }

       //mo tiet kiem online
       #region mo tiet kiem online
       public Hashtable OpenSavingOnline(string userID,string DDAcount,string term,string amount,string branch,string fullName,string Product, string authenType,string authenCode, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000300");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Mở tiết kiệm online");
               hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

               hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
               hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, DDAcount);
               hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branch);
               hasInput.Add(SmartPortal.Constant.IPC.TERM, term);
               hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
               hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authenType);
               hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, authenCode);
               hasInput.Add(SmartPortal.Constant.IPC.SENDERNAME, fullName);
               hasInput.Add(SmartPortal.Constant.IPC.RECEIVERNAME, fullName);
               hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, Product);

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

       //tat toan tiet kiem online
       #region tat toan tiet kiem online
       public Hashtable CloseSavingOnline(string userID,string FDAccount, string DDAcount,string fullName,string amount,string desc,string opendate,string expiredate,string laisuat,string productid,string branchid, string authenType, string authenCode, ref string errorCode, ref string errorDesc)
       {
           try
           {
               //Show hastable result from IPC
               Hashtable hasInput = new Hashtable();
               Hashtable hasOutput = new Hashtable();

               //add key into input
               hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000301");
               hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
               hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, desc);
               hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

               hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
               hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, FDAccount);
               hasInput.Add(SmartPortal.Constant.IPC.DDACCOUNT, DDAcount);
               hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
               hasInput.Add(SmartPortal.Constant.IPC.OPENDATE, opendate);
               hasInput.Add(SmartPortal.Constant.IPC.EXPIREDATE, expiredate);
               hasInput.Add(SmartPortal.Constant.IPC.INTERESTRATE, laisuat);
               hasInput.Add(SmartPortal.Constant.IPC.PRODUCTID, productid);
               hasInput.Add(SmartPortal.Constant.IPC.BRANCHID, branchid);
              
               hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authenType);
               hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, authenCode);
               hasInput.Add(SmartPortal.Constant.IPC.SENDERNAME, fullName);
               hasInput.Add(SmartPortal.Constant.IPC.RECEIVERNAME, fullName);
               //hasInput.Add(SmartPortal.Constant.IPC.LTT, '0');
               //hasInput.Add(SmartPortal.Constant.IPC.LDH, '0');


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

       public DataTable SaveSAO(string userID,string FDAccount,string DDAccount,string expireDate)
       {

           DataTable iRead;

           SqlParameter p1 = new SqlParameter();
           p1.ParameterName = "@userid";
           p1.Value = userID;
           p1.SqlDbType = SqlDbType.Text;

           SqlParameter p2 = new SqlParameter();
           p2.ParameterName = "@account";
           p2.Value = FDAccount;
           p2.SqlDbType = SqlDbType.Text;

           SqlParameter p3 = new SqlParameter();
           p3.ParameterName = "@ddaccount";
           p3.Value = DDAccount;
           p3.SqlDbType = SqlDbType.Text;

           SqlParameter p4 = new SqlParameter();
           p4.ParameterName = "@expiredate";
           p4.Value = expireDate;
           p4.SqlDbType = SqlDbType.Text;

          

           iRead = DataAccess.GetFromDataTable("eba_saveFDOnlineAccount", p1, p2,p3,p4);


           return iRead;
       }

       public DataTable UpdateIsClose(string FDAccount, string isclose)
       {

           DataTable iRead;


           SqlParameter p2 = new SqlParameter();
           p2.ParameterName = "@account";
           p2.Value = FDAccount;
           p2.SqlDbType = SqlDbType.Text;

           SqlParameter p3 = new SqlParameter();
           p3.ParameterName = "@isclose";
           p3.Value = isclose;
           p3.SqlDbType = SqlDbType.Text;

           iRead = DataAccess.GetFromDataTable("ibs_updateisclose", p2,p3);


           return iRead;
       }

       public DataTable UpdateLogOTK(string ipctransid, string opendate, string expitedate, string laisuat)
       {

           DataTable iRead;

           SqlParameter p1 = new SqlParameter();
           p1.ParameterName = "@ipctransid";
           p1.Value = ipctransid;
           p1.SqlDbType = SqlDbType.Text;

           SqlParameter p2 = new SqlParameter();
           p2.ParameterName = "@opendate";
           p2.Value = opendate;
           p2.SqlDbType = SqlDbType.Text;

           SqlParameter p3 = new SqlParameter();
           p3.ParameterName = "@expiredate";
           p3.Value = expitedate;
           p3.SqlDbType = SqlDbType.Text;

           SqlParameter p4 = new SqlParameter();
           p4.ParameterName = "@laisuat";
           p4.Value = laisuat;
           p4.SqlDbType = SqlDbType.Text;



           iRead = DataAccess.GetFromDataTable("eba_UpdateLogOTK", p1, p2, p3, p4);


           return iRead;
       }
    }
}
