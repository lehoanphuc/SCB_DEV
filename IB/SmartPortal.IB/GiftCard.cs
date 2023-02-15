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
    public class GiftCard
    {
        public DataTable GetAllGiftCardHis(string userid)
        {
            return DataAccess.FillDataTable("GetAllGiftCardHis", userid);
        }

        public DataTable GetAllGiftCardType()
        {
            return DataAccess.FillDataTable("GetAllGiftCardType");
        }

        public DataTable GetAllGiftCardDenomination()
        {
            return DataAccess.FillDataTable("GetAllGiftCardDenomination");
        }


        public Hashtable BuyGiftCard(string UserID, string authenType, string authenCode, string SenderAccount,
            string acctccy, string GiftCardName, string Denominations, string giftcardccy, string acctccyamount,
            string feeacctccy, string EquivalentAmount, string amountbcy, string debitexchangebcy,
            string creditexchangebcy, string crossrate, string giftcardcode, string acctname)
        {
            Hashtable result = new Hashtable();
            DataSet ds = null;
            try
            {
                Hashtable input = new Hashtable();

                input.Add(Constant.IPC.IPCTRANCODE, "IBBUYGIFTCARD");
                input.Add(Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(Constant.IPC.USERID, UserID);
                input.Add(Constant.IPC.AUTHENTYPE, authenType);
                input.Add(Constant.IPC.AUTHENCODE, authenCode);
                input.Add("ACCTNO", SenderAccount);
                input.Add("CCYID", acctccy);
                input.Add("GIFTCARDNAME", GiftCardName);
                input.Add("DENOMINATION", Denominations);
                input.Add("GIFTCARDCCY", giftcardccy);
                input.Add("AMOUNT", acctccyamount);
                input.Add("FEEACCTCCY", feeacctccy);
                input.Add("EQUIVALENTAMOUNT", EquivalentAmount);
                input.Add("AMOUNTBCY", amountbcy);
                input.Add("DEBITEXCHANGEBCY", debitexchangebcy);
                input.Add("CREDITEXCHANGEBCY", creditexchangebcy);
                input.Add("CROSSRATE", crossrate);
                input.Add("GIFTCARDTYPE", giftcardcode);
                input.Add("TRANSDATE", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                input.Add("SENDERNAME", acctname);
                result = RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public Hashtable BPMBANKACT(string trancode, string UserID, string amount, string feesender, string feereceiver, string ccyid, string idref,
            string billerid, string acctno, string tbranchid, string fullname, string promotioncode, string authenType, string authenCode, string memo, string refval01,
            string refval02, string refval03, string refval04, string refval05,string senderName, string acctName, string refinfoval01,
            string refinfoval02, string refinfoval03, string refinfoval04, string refinfoval05, string phoneNo, string billType,DataTable tbldocument, string contracttype)
        {
            Hashtable result = new Hashtable();
            DataSet ds = null;
            try
            {
                Hashtable input = new Hashtable();

                input.Add(Constant.IPC.IPCTRANCODE, trancode);
                input.Add(Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(Constant.IPC.SERVICEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                input.Add(Constant.IPC.TRANDESC, memo);
                input.Add(Constant.IPC.USERID, UserID);
                input.Add("AMOUNT", amount);
                input.Add("feeSenderAmt", feesender);
                input.Add("feeReciverAmt", feesender);
                input.Add("CCYID", ccyid);
                input.Add("IDREF", idref);
                input.Add("BILLERID", billerid);
                input.Add("ACCTNO", acctno);
                input.Add("TBRANCHID", tbranchid);
                input.Add("PROMOTIONCODE", promotioncode);
                input.Add("SENDERNAME", senderName);
                input.Add("ACCTNAME", acctName);
                input.Add("REFVAL01", refval01);
                input.Add("REFVAL02", refval02);
                input.Add("REFVAL03", refval03);
                input.Add("REFVAL04", refval04);
                input.Add("REFVAL05", refval05);
                input.Add("AUTHENTYPE", authenType);
                input.Add("AUTHENCODE", authenCode);
                input.Add("REFINFOVA01", refinfoval01);
                input.Add("REFINFOVA02", refinfoval02);
                input.Add("REFINFOVA03", refinfoval03);
                input.Add("REFINFOVA04", refinfoval04);
                input.Add("REFINFOVA05", refinfoval05);
                input.Add("PHONENO", phoneNo);
                input.Add("BILLTYPE", billType);
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


        public Hashtable CalFeeGiftCard(string UserID, string SenderAccount, string giftcardccy, string GiftCardType,
            string EquivalentAmount, string branchid, string acctccyid)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                input.Add(Constant.IPC.IPCTRANCODE, "CALFEEGIFTCARD");
                input.Add(Constant.IPC.SOURCEID, Constant.IPC.SOURCEIBVALUE);
                input.Add(Constant.IPC.USERID, UserID);
                input.Add("ACCTNO", SenderAccount);
                input.Add("EQUIVALENTAMOUNT", EquivalentAmount);
                input.Add("GIFTCARDTYPE", GiftCardType);
                input.Add("GIFTCARDCCY", giftcardccy);
                input.Add("DEBITBRACHID", branchid);
                input.Add("CCYID", acctccyid);

                result = RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public Hashtable CalFeeBillPayment(string UserID, string billID, string transaction, string facctno,
           string amount, string ccyid)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                input.Add(Constant.IPC.IPCTRANCODE, "IBCALTRANFEEBPM");
                input.Add(Constant.IPC.SOURCEID, Constant.IPC.SOURCEIBVALUE);
                input.Add(Constant.IPC.USERID, UserID);
                input.Add("BILLID", billID);
                input.Add("TRANSACTION", transaction);
                input.Add("FACCTNO", facctno);
                input.Add("AMOUNT", amount);
                input.Add("CCYID", ccyid);

                result = RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public Hashtable IBBILLPAYMENTINQUIRY(string UserID, string billID, string branhid, string facctno, string ccyid,
          string refval1, string refval2, string refval3, string refval4, string refval5)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                input.Add(Constant.IPC.IPCTRANCODE, "IBBILLINQUIRY");
                input.Add(Constant.IPC.SOURCEID, Constant.IPC.SOURCEIBVALUE);
                input.Add(Constant.IPC.USERID, UserID);
                input.Add("BILLERID", billID);
                input.Add("TBRANCHID", branhid);
                input.Add("FACCTNO", facctno);
                input.Add("CCYID", ccyid);
                input.Add("REFVAL01", refval1);
                input.Add("REFVAL02", refval2);
                input.Add("REFVAL03", refval3);
                input.Add("REFVAL04", refval4);
                input.Add("REFVAL05", refval5);
                result = RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public Hashtable CheckPromotionCode(string UserID, string promotioncode)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                input.Add(Constant.IPC.IPCTRANCODE, "IBCHECKPROMOTIONCODE");
                input.Add(Constant.IPC.SOURCEID, Constant.IPC.SOURCEIBVALUE);
                input.Add(Constant.IPC.USERID, UserID);
                input.Add("PROMOTIONCODE", promotioncode);
                result = RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public Hashtable IBCHECKAMOUNTTRAN(string UserID, string acctNo, string receiverAcct, string amount, string ipctrancodeRight)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();

                input.Add(Constant.IPC.IPCTRANCODE, "IB_CHECKAMTTRAN");
                input.Add(Constant.IPC.SOURCEID, Constant.IPC.SOURCEIBVALUE);
                input.Add(Constant.IPC.USERID, UserID);
                input.Add("ACCTNO", acctNo);
                input.Add("RECEIVERACTNO", receiverAcct);
                input.Add("AMOUNT", amount);
                input.Add("IPCTRANCODERIGHT", ipctrancodeRight);
                result = RemotingServices.AT.DBTRAN().ProcessTransHAS(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public DataTable GETEGIFTCARD(ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBGETEGIFTCARD");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataTable dt = new DataTable();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    dt = (DataTable)hasOutput["DATARESULT"];
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
        public DataTable GETBILLLIST(string userid, string catid, string lang, string billername, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB_GETBILLERLIST");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);

                hasInput.Add(SmartPortal.Constant.IPC.CATID, catid);
                hasInput.Add(SmartPortal.Constant.IPC.LANG, lang);
                hasInput.Add(SmartPortal.Constant.IPC.BILLERNAME, billername);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataTable dt = new DataTable();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    DataSet dt1 = (DataSet)hasOutput["DATARESULT"];
                    dt = dt1.Tables[0];
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

        public DataSet BILLERGETREF(string lang, string billerid, ref string Biller_type, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBGETBILLERREF");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.BILLERID, billerid);
                hasInput.Add(SmartPortal.Constant.IPC.LANG, lang);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet dt = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    dt = (DataSet)hasOutput["DATARESULT"];
                    Biller_type = hasOutput["BILLTYPE"].ToString();
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

        public DataSet BILLERGETAMOUNT(string billerid, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IBGETBILLERAMOUNT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.BILLERID, billerid);
                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet dt = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    dt = (DataSet)hasOutput["DATARESULT"];
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

        public DataSet BILLERGATDATAPACKAGE(string userid, string serid, string refval1, string refval2, string refval3, ref string errorCode, ref string errorDesc)
        {
            try
            {
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "GETDATAPACKAGE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIBVALUE);

                hasInput.Add(SmartPortal.Constant.IPC.USERID, userid);
                hasInput.Add("SERID", serid);
                hasInput.Add(SmartPortal.Constant.IPC.REFVA1, refval1);
                hasInput.Add(SmartPortal.Constant.IPC.REFVA2, refval2);
                hasInput.Add(SmartPortal.Constant.IPC.REFVA3, refval3);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                DataSet dt = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    dt = (DataSet)hasOutput["DATALIST"];
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
    }
}
