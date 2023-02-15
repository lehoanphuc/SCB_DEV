using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;

namespace LAPNETAPI
{
    public static class Common
    {
        private static ITransaction.AutoTrans _autoTrans;
        public static ITransaction.AutoTrans autoTrans
        {
            get
            {

                if (_autoTrans == null)
                {
                    _autoTrans = (ITransaction.AutoTrans)Activator.GetObject(
                        typeof(ITransaction.AutoTrans),
                        "tcp://" + ConfigurationManager.AppSettings["RemotingAddress"].ToString() + ":" +
                        ConfigurationManager.AppSettings["RemotingPort"].ToString() + "/IPCAutoTrans");
                }
                return _autoTrans;
            }
        }
        public static string ConStr = "";
        public readonly static string access_token = "access_token";
        public readonly static string token_type = "token_type";
        public readonly static string expires_in = "expires_in";
        public readonly static string expiration_date = "expiration_date";
        public readonly static string urlencodedtype = "application/x-www-form-urlencoded";
        public readonly static string jsontype = "application/json";
        public readonly static string GetTokenPath = "GetTokenPath";
        public readonly static string AccessTokenExpireTime = "AccessTokenExpireTime";
        public readonly static string TimeType = "TimeType";
        public readonly static string result = "result";
        public readonly static string SystemError = "system suspend at the moment, please try again later";
        public readonly static string CusNm = "additional_info.Cdtr.CusNm";
        public readonly static string ReceiverChannelPath = "additional_info.receiver_channel";
        public readonly static string RevNamePath = "RevNamePath";
        public readonly static string RevChannelPath = "RevChannelPath";
        public readonly static string StatusForPrinciple = "status.StatusForPrinciple";
        public readonly static string StatusForFee = "status.StatusForFee";
        public readonly static string ErrorCodeForFee = "status.ErrorCodeForFee";
        public readonly static string ErrorCodeForPrinciple = "status.ErrorCodeForPrinciple";
        public readonly static string status = "status";
        public readonly static string CustRefForPrinciple = "status.CustRefForPrinciple";
        public readonly static string CustRefForFee = "status.CustRefForFee";
        public readonly static string Complete = "Complete";
        public readonly static string Reject = "Reject";
        public readonly static string main = "main";
        public readonly static string ResultLogin = "ResultLogin";
        public readonly static string USERNAME = "USERNAME";
        public readonly static string PASSWORD = "PASSWORD";
        public readonly static string USERID = "USERID";
        public readonly static string SOURCEID = "SOURCEID";
        public readonly static string TRANSID = "TRANSID";
        public readonly static string IPADDRESS = "IPADDRESS";
        public readonly static string RemotingAddress = "RemotingAddress";
        public readonly static string RemotingPort = "RemotingPort";
        public readonly static string IPCAutoTrans = "/IPCAutoTrans";
        public readonly static string ERRDESC = "ERRDESC";
        public readonly static string ERRORCODE_SUCCESS = "0";
        public readonly static string STATUSKEY = "status";
        public readonly static string RSPAYMENT = "RSPAYMENT";
        public readonly static string LOCKQUOTE = "LOCKQUOTE";
        public readonly static string CORECUSTNAME = "CORECUSTNAME";
        public readonly static string LOCKEDQUOTE = "LOCKEDQUOTE";
        public readonly static string SUBMITPAYMENT = "SUBMITPAYMENT";
        public readonly static string IMEXSTATUS = "IMEXSTATUS";
        public readonly static string CUSTFORPRINCIPLE = "CUSTFORPRINCIPLE";
        public readonly static string CUSTFORFEE = "CUSTFORFEE";
        public readonly static string ERRORCODEFEE = "ERRORCODEFEE";
        public readonly static string ERRORDESC = "ERRORDESC";
        public readonly static string CONTENT = "CONTENT";
        public readonly static string IMEXMAIN = "IMEXMAIN";
        public readonly static string REASONTYPE = "REASONTYPE";
        public readonly static string REASONCODE = "REASONCODE";
        public readonly static string REASONMSG = "REASONMSG";
        public readonly static string PAYMENTINFORMATION = "PAYMENTINFORMATION";
        public readonly static string SENDERACCNO = "SENDERACCNO";
        public readonly static string RECEIVERACCNO = "RECEIVERACCNO";
        public readonly static string SENDERCHANNELCODE = "SENDERCHANNELCODE";
        public readonly static string SENDERBIC = "SENDERBIC";
        public readonly static string RECEIVERBIC = "RECEIVERBIC";
        public readonly static string TRANLIST = "TRANLIST";
        public readonly static int MAXREQUESTSIZE = 4194304;
        public readonly static string[] LISTMETHODS = { "paymentinformation", "submitsending", "fail", "rejectwitherror", "submitwitherror", "sub_payment", "payments", "failpayment", "authen" };
        public static class test { };
    }
}