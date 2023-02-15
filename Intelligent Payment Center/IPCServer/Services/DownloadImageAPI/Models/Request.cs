using DownloadImageAPI.Providers;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DownloadImageAPI.Models
{
    #region Submitpayment
    public class AmountSender
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class SenderSubmitPayment
    {
        public string entity { get; set; }
        public AmountSender amount { get; set; }
    }

    public class AmountReceiver
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class ReceiverSubmitPayment
    {
        public string entity { get; set; }
        public AmountReceiver amount { get; set; }
    }

    public class MainSubmitPayment
    {
        public SenderSubmitPayment sender { get; set; }
        public ReceiverSubmitPayment receiver { get; set; }
        public DateTime due { get; set; }
    }

    public class StatusSubmitPayment
    {
        public string StatusForPrinciple { get; set; }
        public string StatusForFee { get; set; }
        public string ErrorCodeForPrinciple { get; set; }
        public string ErrorCodeForFee { get; set; }
        public string ErrorDescription { get; set; }
        public string CustRefForPrinciple { get; set; }
        public string CustRefForFee { get; set; }
    }

    public class SubmitPayment
    {
        public MainSubmitPayment main { get; set; }
        public StatusSubmitPayment status { get; set; }
    }
    #endregion

    #region RejectErr
    public class MainRejectErr
    {
        public string type { get; set; }
        public string code { get; set; }
        public string reason { get; set; }
    }

    public class StatusRejectErr
    {
        public string StatusForPrinciple { get; set; }
        public string StatusForFee { get; set; }
        public string ErrorCodeForPrinciple { get; set; }
        public string ErrorCodeForFee { get; set; }
        public string ErrorDescription { get; set; }
        public string CustRefForPrinciple { get; set; }
        public string CustRefForFee { get; set; }
    }

    public class RejectErrTimeOut
    {
        public List<MainRejectErr> main { get; set; }
        public StatusRejectErr status { get; set; }
    }

    #endregion

    #region reject payment
    public class RejectPayment
    {
        public MainRejectErr[] main { get; set; }
    }
    #endregion

    #region UpstreamCCT
    public class UpstreamCCT
    {
        [StringLength(50)]
        public string headerkey { get; set; }
        [StringLength(50)]
        public string type { get; set; }
        [StringLength(50)]
        public string userId { get; set; }
        [StringLength(50)]
        public string messageFormatId { get; set; }
        public List<cct> cctList { get; set; }
    }

    public class cct
    {
        [StringLength(50)]
        public string msgId { get; set; }
        [StringLength(50)]
        public string debtFIBranchCode { get; set; }
        [StringLength(50)]
        public string credtFIBranchCode { get; set; }
        [StringLength(50)]
        public string totalTranNo { get; set; }
        public double totalSettlementAmt { get; set; }
        [StringLength(50)]
        public string settlementAccIdIBAN { get; set; }
        [StringLength(50)]
        public string settlementCredtFIBranchCode { get; set; }
        [StringLength(50)]
        public string settlementFundDebtFIBranchCode { get; set; }
        [StringLength(50)]
        public string settlementMethod { get; set; }
        public List<transaction> transactionList { get; set; }
    }
    public class transaction
    {
        [StringLength(50)]
        public string askACK { get; set; }
        [StringLength(50)]
        public string reasonCode { get; set; }
        [StringLength(50)]
        public string reconciliationNo { get; set; }
        [StringLength(50)]
        public string transactionId { get; set; }
        [StringLength(50)]
        public string settlementDateInstruction { get; set; }
        [StringLength(50)]
        public string eventFundCode { get; set; }
        [Range(0, 9999999999999998, ErrorMessage = "totalFundSttlmAmt must be between 0 and 9999999999999998")]
        public double totalFundSttlmAmt { get; set; }
        [StringLength(50)]
        public string settlementDate { get; set; }
        [StringLength(50)]
        public string transactionDate { get; set; }

        [StringLength(50)]
        public string queuingCategoryCode { get; set; }
        [Range(0, 9999999999999998, ErrorMessage = "totalFundSttlmAmt must be between 0 and 9999999999999998")]
        public double transferAmt { get; set; }
        [StringLength(50)]
        public string chargeBearerCode { get; set; }
        [Range(0, 9999999999999998, ErrorMessage = "chargeAmount must be between 0 and 9999999999999998")]
        public List<double> chargeAmount { get; set; }
        [StringLength(50)]
        public string debtIdCategory { get; set; }
        [StringLength(50)]
        public string debtId { get; set; }
        [StringLength(50)]
        public string tinNo { get; set; }
        [StringLength(50)]
        public string debtPostalAddress { get; set; }
        [StringLength(50)]
        public string debtName { get; set; }
        [StringLength(50)]
        public string debtPhNo { get; set; }
        [StringLength(50)]
        public string debtorAgentFIBranchNo { get; set; }

        [StringLength(50)]
        public string debtAgentFIBranchName { get; set; }
        [StringLength(50)]
        public string credtAgentFIBranchNo { get; set; }
        [StringLength(50)]
        public string credtAgentFIBranchName { get; set; }
        [StringLength(50)]
        public string credtName { get; set; }
        [StringLength(50)]
        public string credtPostalAddress { get; set; }
        [StringLength(50)]
        public string credtIdCategory { get; set; }
        [StringLength(50)]
        public string credtId { get; set; }
        [StringLength(50)]
        public string credtPhNo { get; set; }
        [StringLength(50)]
        public string transactionNote { get; set; }
        public  List<OtherInformation> otherInformation { get; set; }

    }
    public class OtherInformation
    {
        [StringLength(140)]
        public  string COST { get; set; }
    }
    #endregion

    #region Submit error
    public class AmountSenderSubmitError
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class SenderSubmitError
    {
        public string entity { get; set; }
        public AmountSenderSubmitError amount { get; set; }
    }

    public class AmountReceiverSubmitError
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class ReceiverSubmitError
    {
        public string entity { get; set; }
        public AmountReceiverSubmitError amount { get; set; }
    }

    public class MainSubmitError
    {
        public SenderSubmitError sender { get; set; }
        public ReceiverSubmitError receiver { get; set; }
        public DateTime due { get; set; }
    }

    public class StatusSubmitError
    {
        public string StatusForPrinciple { get; set; }
        public string StatusForFee { get; set; }
        public string ErrorCodeForPrinciple { get; set; }
        public string ErrorCodeForFee { get; set; }
        public string ErrorDescription { get; set; }
        public string CustRefForPrinciple { get; set; }
        public string CustRefForFee { get; set; }
    }

    public class SubmitError
    {
        public MainSubmitError main { get; set; }
        public StatusSubmitError status { get; set; }
    }
    #endregion

    #region Rejecttime
    public class Main
    {
        public string type { get; set; }
        public string code { get; set; }
        public string reason { get; set; }
    }

    public class Status
    {
        public string StatusForPrinciple { get; set; }
        public string StatusForFee { get; set; }
        public string ErrorCodeForPrinciple { get; set; }
        public string ErrorCodeForFee { get; set; }
        public string ErrorDescription { get; set; }
        public string CustRefForPrinciple { get; set; }
        public string CustRefForFee { get; set; }
    }

    public class RootObject
    {
        public List<Main> main { get; set; }
        public Status status { get; set; }
    }


    #endregion

    #region new reject payment
    public class failpayment
    {
        public string type { get; set; }
        public string code { get; set; }
        public string reason { get; set; }
    }
    #endregion

    public class subpaymentMain
    {
        public subpaymentsenderorreceiver sender { get; set; }
        public subpaymentsenderorreceiver receiver { get; set; }
        public string due { get; set; }
    }

    public class subpayment
    {
        public subpaymentMain main { get; set; }
        public Status status { get; set; }
    }

    public class subpaymentsenderorreceiver
    {
        public string entity { get; set; }
        public Amount amount { get; set; }
    }

    public class Amount
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class CreateTokenBody
    {
        [Required]
        public string grant_type { get; set; }
    }


    #region UpstreamNotificationToReceive
    public class UpstreamNotificationToReceive
    {
        [StringLength(50)]
        public string headerkey { get; set; }
        [StringLength(50)]
        public string type { get; set; }
        [StringLength(50)]
        public string userId { get; set; }
        [StringLength(50)]
        public string messageFormatId { get; set; }
        public List<notificationToReceive> notificationToReceiveList { get; set; }
    }
    public class notificationToReceive
    {
        [StringLength(50)]
        public string msgId { get; set; }
        [StringLength(50)]
        public string transactionId { get; set; }
        [StringLength(50)]
        public string reconciliationNo { get; set; }
        [StringLength(50)]
        public string credtFIBranchCode { get; set; }
        [StringLength(50)]
        public string debtFIBranchCode { get; set; }
        [StringLength(50)]
        public string messageType { get; set; }
        [StringLength(50)]
        public string message { get; set; }
        [StringLength(50)]
        public string credtPostalAddress { get; set; }
        [StringLength(50)]
        public string cbmnetProcessNo { get; set; }
        [StringLength(50)]
        public string transactionDate { get; set; }
    }
    #endregion

    #region UpstreamCCTFailValidation
    public class UpstreamCCTFailValidation
    {
        [StringLength(50)]
        public string transactionID { get; set; }
        [StringLength(50)]
        public string creditorBankCode { get; set; }
    }
    #endregion
    #region UpstreamCCTSuccessValidation
    public class UpstreamCCTSuccessValidation
    {
        [StringLength(50)]
        public string DwnstrmMsgSeqtlNb { get; set;}
        public  FIToFIPmtStsRpt FIToFIPmtStsRpt { get; set; }
    }

    public class FIToFIPmtStsRpt
    {
        [StringLength(50)]
        public string MsgId { get; set; }
        [StringLength(50)]
        public string CreDtTm { get; set; }
        public txInfAndSts TxInfAndSts { get; set; }
    }
    public class txInfAndSts
    {
        [StringLength(50)]
        public string StsId { get; set; }
        [StringLength(50)]
        public string OrgnlEndToEndId { get; set; }
        [StringLength(50)]
        public string OrgnlTxId { get; set; }
        [StringLength(50)]
        public string AccptncDtTm { get; set; }
        [StringLength(50)]
        public string ClrSysRef { get; set; }
        [StringLength(50)]
        public string IntrBkSttlmDt { get; set; }
        [Range(0, 9999999999999998, ErrorMessage = "totalFundSttlmAmt must be between 0 and 9999999999999998")]
        public double TtlNetNtry { get; set; }
        [Range(0, 9999999999999998, ErrorMessage = "totalFundSttlmAmt must be between 0 and 9999999999999998")]
        public double Avlbty { get; set; }
    }
    #endregion

}