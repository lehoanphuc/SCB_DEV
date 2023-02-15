using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;
using System.Collections;
using SmartPortal.Constant;

namespace SmartPortal.IB
{
    public class Payment
    {
        /// <summary>
        /// Lay danh sach ngan hang
        /// </summary>
        /// <returns></returns>
        /// 
        public DataTable GetCorpList(ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataTable dt =new DataTable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000600");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get corp list");

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    try
                    {
                        ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.CORPLIST];
                        dt = ds.Tables[0];
                        errorCode = "0";
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetServicebyCorpID(string corpID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataTable dt = new DataTable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000601");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.CORPID,corpID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get service list by corpID");

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.SERVICELIST];
                    dt = ds.Tables[0];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetServiceInformation(string serviceID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataTable dt = new DataTable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000602");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceID);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get service info by serviceID");

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.SERVICEINFO];
                    dt = ds.Tables[0];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetFee(string serviceID, string amount, string acctno, string REFVA1, string REFVA2, string REFVA3, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataTable dt = new DataTable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000603");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceID);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, acctno);
                hasInput.Add(SmartPortal.Constant.IPC.REFVA1, REFVA1);
                hasInput.Add(SmartPortal.Constant.IPC.REFVA2, REFVA2);
                hasInput.Add(SmartPortal.Constant.IPC.REFVA3, REFVA3);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Get service info by serviceID");

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.FEE];
                    dt = ds.Tables[0];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public Hashtable ProcessCollection(string userID, string serviceID, string catID, string corpID, string amount, string acctno, string refva1, string refva2, string desc, string authentype, string authencode, ref string errorCode, ref string errorDesc)
        //{
        //    return ProcessCollection(userID, serviceID, catID, corpID, amount, acctno, refva1, refva2, "", desc, authentype, authencode, ref errorCode, ref errorDesc);
        //}
        public Hashtable ProcessCollection(string userID, string serviceID, string catID, string corpID, string amount, string ccyid, string acctno, string refva1, string refva2, string refva3, string refindex1, string refindex2, string refindex3,string SenderName, string SenderBranch,string feeamount, string desc, string authentype, string authencode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataTable dt = new DataTable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000604");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceID);
                hasInput.Add(SmartPortal.Constant.IPC.CATID, catID);
                hasInput.Add(SmartPortal.Constant.IPC.CORPID, corpID);
                hasInput.Add(SmartPortal.Constant.IPC.AMOUNT, amount);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, acctno);
                hasInput.Add(SmartPortal.Constant.IPC.REFVA1, refva1);
                hasInput.Add(SmartPortal.Constant.IPC.REFVA2, refva2);
                hasInput.Add(SmartPortal.Constant.IPC.REFVA3, refva3);
                hasInput.Add("REFINDEX1", refindex1);
                hasInput.Add("REFINDEX2", refindex2);
                hasInput.Add("REFINDEX3", refindex3);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, desc);
                hasInput.Add(SmartPortal.Constant.IPC.CCYID, ccyid);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);
                hasInput.Add(SmartPortal.Constant.IPC.SENDERNAME, SenderName);
                hasInput.Add(SmartPortal.Constant.IPC.CREDITBRACHID, SenderBranch);
                hasInput.Add("FEEAMOUNT", feeamount);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

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
        public Hashtable UploadDirectDebit(string userID, string fileName, string fileData, string desc, string authentype, string authencode, string corpID, string serID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataTable dt = new DataTable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000610");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);
                hasInput.Add(SmartPortal.Constant.IPC.FILENAME, fileName);
                hasInput.Add(SmartPortal.Constant.IPC.PARAM, fileData);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, desc);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENTYPE, authentype);
                hasInput.Add(SmartPortal.Constant.IPC.AUTHENCODE, authencode);
                hasInput.Add(SmartPortal.Constant.IPC.CORPID, corpID);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serID);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

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
        public DataSet GetCorpInfobyUserid(string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataSet dt = new DataSet();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000605");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];

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
        public object GetMis(string corpid, string serviceid, string email, string fromdate, string todate, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataTable dt = new DataTable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000606");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.CORPID, corpid);
                hasInput.Add(SmartPortal.Constant.IPC.SERVICEID, serviceid);
                hasInput.Add(SmartPortal.Constant.IPC.EMAIL, email);
                hasInput.Add(SmartPortal.Constant.IPC.FROMDATE, fromdate);
                hasInput.Add(SmartPortal.Constant.IPC.TODATE, todate);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    errorCode = "0";
                    if(hasOutput.Contains(SmartPortal.Constant.IPC.MISINFOR) && hasOutput[SmartPortal.Constant.IPC.MISINFOR] != null)
                    {
                        return hasOutput[SmartPortal.Constant.IPC.MISINFOR];
                    }
                    else
                    {
                        return null;
                    }
                    //if(hasOutput.Contains(SmartPortal.Constant.IPC.MISINFOR) && hasOutput[SmartPortal.Constant.IPC.MISINFOR] != null)
                    //{
                        
                    //}
                    //ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.MISINFOR];
                    //dt = ds.Tables[0];

                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return null;
                }

                //return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Hashtable CheckAmount(string corpID, string ipctrancode,string ref1,string ref2,ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataTable dt = new DataTable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, ipctrancode);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.CORPID, corpID);
                hasInput.Add(SmartPortal.Constant.IPC.REFVA1, ref1);
                hasInput.Add(SmartPortal.Constant.IPC.REFVA2, ref2);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                try
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                catch { }
                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Hashtable GetAmountFromHUB(string corpID,string serviceID, string accountNumber, string refval1, string refval2, ref string errorCode, ref string errorDesc)
        {
            return GetAmountFromHUB(corpID, serviceID, accountNumber, refval1, refval2, "", ref errorCode, ref errorDesc);
        }

        public Hashtable GetAmountFromHUB(string corpID,string billerID, string accountNumber, string refval1, string refval2,string refval3, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataTable dt = new DataTable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBBILLINQUIRY");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.BILLERID, billerID);
                hasInput.Add(SmartPortal.Constant.IPC.ACCTNO, accountNumber);
                if (!string.IsNullOrEmpty(refval1))
                    hasInput.Add(SmartPortal.Constant.IPC.REFVA1, refval1);
                if (!string.IsNullOrEmpty(refval2))
                    hasInput.Add(SmartPortal.Constant.IPC.REFVA2, refval2);
                if (!string.IsNullOrEmpty(refval3))
                    hasInput.Add(SmartPortal.Constant.IPC.REFVA3, refval3);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                try
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }
                catch { }
                return hasOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet CheckWebService(string storeName, object[] para, ref string errorCode, ref string errorDesc)
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetService()
        {           
            DataTable iRead;

            iRead = DataAccess.GetFromDataTable("IBS_EBA_PAYMENTTYPE_SELECTALL", null);

            return iRead;
        }
        public DataTable GetProvider()
        {
            DataTable iRead;

            iRead = DataAccess.GetFromDataTable("IBS_EBA_PPAYMENTPROVIDER_SELECTALL", null);

            return iRead;
        }

        /// <summary>
        /// Lay ma chi nhanh
        /// </summary>
        /// <param name="bankCode"></param>
        /// <returns></returns>
        public DataTable GetBranchID(string bankCode)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@BANKCODE";
            p1.Value = bankCode;
            p1.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IPC_GETTRANSFEROUTBANKINFO", p1);

            return iRead;
        }

        /// <summary>
        /// Tinh phi
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="ipctrancode"></param>
        /// <param name="amount"></param>
        /// <param name="senderAcctno"></param>
        /// <param name="branchID"></param>
        /// <returns></returns>
        public DataTable GetFee1(string userID, string ipctrancode, string amount, string senderAcctno, string branchID, string CCYID, string CITY)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@USERID";
            p1.Value = userID;
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@IPCTRANCODE";
            p2.Value = ipctrancode;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@AMOUNT";
            p3.Value = amount;
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@FACCTNO";
            p4.Value = senderAcctno;
            p4.SqlDbType = SqlDbType.VarChar;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@TBRANCHID";
            p5.Value = branchID;
            p5.SqlDbType = SqlDbType.VarChar;

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@CCYID";
            p6.Value = CCYID;
            p6.SqlDbType = SqlDbType.VarChar;

            SqlParameter p7 = new SqlParameter();
            p7.ParameterName = "@TCITY";
            p7.Value = CITY;
            p7.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IPC_CALCULATORTRANSFEE", p1, p2, p3, p4, p5, p6, p7);

            return iRead;
        }
        public int LogWaterBillPayment(string IDkey, string userid, string status, string PAYERACCTNO, string BILLNO, string AMOUNT)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@IDKEY";
                p1.Value = IDkey;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@USERID";
                p2.Value = userid;
                p2.SqlDbType = SqlDbType.VarChar;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@STATUS";
                p3.Value = status;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@PAYERACCTNO";
                p4.Value = PAYERACCTNO;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@BILLNO";
                p5.Value = BILLNO;
                p5.SqlDbType = SqlDbType.VarChar;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@AMOUNT";
                p6.Value = AMOUNT;
                p6.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("EBA_INSERT_LOG_BILLNUOCCHOLON", p1, p2, p3,p4,p5,p6);

                if (strErr == 0)
                {
                    return strErr;
                    //throw new BusinessExeption("Unable Approve category");
                }
                else
                {
                    return strErr;
                }
                return strErr;
            }
            //catch (BusinessExeption bex)
            //{
            //    throw bex;
            //}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int LogWaterBillPaymentFirst(string PAYERACCTNO)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@PAYERACCTNO";
                p1.Value = PAYERACCTNO;
                p1.SqlDbType = SqlDbType.VarChar;

                strErr = DataAccess.Execute("EBA_INSERT_LOG_BILLNUOCCHOLON_FIRST", p1);

                if (strErr == 0)
                {
                    return strErr;
                    //throw new BusinessExeption("Unable Approve category");
                }
                else
                {
                    return strErr;
                }
                return strErr;
            }
            //catch (BusinessExeption bex)
            //{
            //    throw bex;
            //}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPaymentProvider(string PaymentTypeID)
        {
            DataTable iRead;

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@PAYMENTTYPEID";
            p1.Value = PaymentTypeID;
            p1.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("IB_EBA_GETPAYMENTPROVIDER", p1);

            return iRead;
        }
        public DataSet ViewlogBillDetails(string ipctransid,ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                object[] para = new object[] { ipctransid };
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000113");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                //hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tìm kiếm thành phố");
                //hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add("STORENAME", "SEMS_EBA_BILLNUOCCHOLON_VIEWLOG");
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetBillPayment(string custcode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000309");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Lấy thông tin hóa đơn nước");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");
                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODE, custcode);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.BILLINFO];
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
        public Hashtable GetTinNo(string tinno, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBGETIRDPAYERPROFILE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(IPC.TINNO, tinno);
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
        public Hashtable TaxPayment(string CRBANKBIC, string SENDERNAME, string SENDERADDRESS, string SENDERPHONE, string RECEIVERNAME, string REVACCOUNT, string ACCTNO, string CCYID, string AMOUNT, string EMAIL, string TAXTYPE, string PAYMENTTYPE, string TAXPERIOD, string INCOMEYEAR, string TINNO, string AUTHENTYPE, string AUTHENCODE, string IRDREFNO, string TAXTYPENAME, string PAYMENTTYPENAME, string TAXPERIODNAME, string INCOMEYEARNAME, string USERID, string DEBITBRACHID, string TRANDESC, string CREDITORBRANCHNAME, string PAYMENTOPTIONNAME, string PAYMENTYEARNAME, DataTable tbldocument, string contracttype, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                DataTable dt = new DataTable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBTAXPAYMENT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(IPC.CRBANKBIC, CRBANKBIC);
                hasInput.Add(IPC.SENDERNAME, SENDERNAME);
                hasInput.Add(IPC.SENDERADDRESS, SENDERADDRESS);
                hasInput.Add(IPC.SENDERPHONE, SENDERPHONE);
                hasInput.Add(IPC.RECEIVERNAME, RECEIVERNAME);
                hasInput.Add(IPC.REVACCOUNT, REVACCOUNT);
                hasInput.Add(IPC.ACCTNO, ACCTNO);
                hasInput.Add(IPC.CCYID, CCYID);
                hasInput.Add(IPC.AMOUNT, AMOUNT);
                hasInput.Add(IPC.EMAIL, EMAIL);
                hasInput.Add(IPC.TAXTYPE, TAXTYPE);
                hasInput.Add(IPC.PAYMENTTYPE, PAYMENTTYPE);
                hasInput.Add(IPC.TAXPERIOD, TAXPERIOD);
                hasInput.Add(IPC.INCOMEYEAR, INCOMEYEAR);
                hasInput.Add(IPC.TINNO, TINNO);
                hasInput.Add(IPC.TRANDESC, TRANDESC);
                hasInput.Add(IPC.AUTHENTYPE, AUTHENTYPE);
                hasInput.Add(IPC.AUTHENCODE, AUTHENCODE);
                hasInput.Add(IPC.IRDREFNO, IRDREFNO);
                hasInput.Add(IPC.TAXTYPENAME, TAXTYPENAME);
                hasInput.Add(IPC.PAYMENTTYPENAME, PAYMENTTYPENAME);
                hasInput.Add(IPC.TAXPERIODNAME, TAXPERIODNAME);
                hasInput.Add(IPC.INCOMEYEARNAME, INCOMEYEARNAME);
                hasInput.Add(IPC.USERID, USERID);
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTTYPE, contracttype);
                if (tbldocument != null)
                {
                    hasInput.Add(SmartPortal.Constant.IPC.DOCUMENT, tbldocument);
                }
                hasInput.Add(IPC.DEBITBRACHID, DEBITBRACHID);
                hasInput.Add("CREDITORBRANCHNAME", CREDITORBRANCHNAME);
                hasInput.Add("PAYMENTOPTIONNAME", PAYMENTOPTIONNAME);
                hasInput.Add("PAYMENTYEARNAME", PAYMENTYEARNAME);

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
    }
}
