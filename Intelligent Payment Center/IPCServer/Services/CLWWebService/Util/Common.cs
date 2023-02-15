using System.Collections.Generic;

namespace CLWWebService.Util
{
    public class Common
    {
        public const string APITYPEVALUE = "SCF";
        public const string O9CHECKSANCTION = "O9CHECKSANCTION";
        public const string BRTHDATE = "BRTHDATE";
        public const string CNTRYCODE = "CNTRYCODE";
        public const string DOCNUM = "DOCNUM";
        public const string ENGFSTNAME = "ENGFSTNAME";
        public const string ENGLSTNAME = "ENGLSTNAME";
        public const string ENGMIDNAME = "ENGMIDNAME";
        public const string FUNCTIONNAME = "FUNCTIONNAME";
        public const string NATIONCODE = "NATIONCODE";
        public const string THFSTNAME = "THFSTNAME";
        public const string THLSTNAME = "THLSTNAME";
        public const string THMIDNAME = "THMIDNAME";
        public const string TYPECODE = "TYPECODE";
        public const string USERDEF1 = "USERDEF1";
        public const string USERDEF2 = "USERDEF2";
        public const string USERDEF3 = "USERDEF3";
        public const string USERDEF4 = "USERDEF4";
        public const string USERDEF5 = "USERDEF5";
        public const string SANCTIONFLG = "SANCTIONFLG";
        public const string RSDT = "RSDT";
        public const string STATUSCODE = "STATUSCODE";
        public const string APINAME = "APINAME";
        public const string APITYPE = "APITYPE";
        public const string USERLOGIN = "USERLOGIN";
        public const string SOURCEIDVALUE = "O9";
        public const string O9CHECKROLEUSERAPI = "O9CHECKROLEUSERAPI";
        public const string APIID = "APIID";
        public const string LOGINAPIID = "O9MLESCF";
        public const string CHECKSANCTION = "O9MCS";
        public const string DIRECTDEBITTRANSACTION = "O9MDDESCF";
        public const string ENQUIRY_DIRECTDEBIT = "O9MEDDESCF";
        public const string ENQUIRY_ACOUNTBALANCE = "O9MEABESCF";
        public const string USERNAME = "USERNAME";
        public const string PASSWORD = "PASSWORD";
        public const string SOURCETRANREF = "SOURCETRANREF";
        public const string BANKCODE = "BANKCODE";
        public const string SOURCEACCNO = "SOURCEACCNO";
        public const string DESTACCNO = "DESTACCNO";
        public const string IPCTRANSDATE = "IPCTRANSDATE";
        public const string TRANSAMOUNT = "TRANSAMOUNT";
        public const string CCYID = "CCYID";
        public const string STRDESTRESULT = "STRDESTRESULT";
        public const string ACCNO = "ACCNO";
        public const string TRANSDATE = "TRANSDATE";
        public const string DEBITTIME = "DEBITTIME";
        public const string LATESTTRANSACTIONTIME = "LATESTTRANSACTIONTIME";

        public readonly static Dictionary<string, string> DicErrorCode = new Dictionary<string, string>()
        {
            {ErrorCodeList.PermisstionDenied,"Permission denied" },
            {ErrorCodeList.CannotConnectCoreBank,"Network error" },
            {ErrorCodeList.InvalidBankCode,"Invalid bank code" },
        };

        public static class ErrorCodeList
        {
            public const string PermisstionDenied = "117";
            public const string CannotConnectCoreBank = "114";
            public const string InvalidBankCode = "115";
        }

        public static string DecodeBase62ToString(string value)
        {
            string characterSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var arr = new int[value.Length];
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = characterSet.IndexOf(value[i]);
            }

            return DecodeIntArray2String(arr);
        }

        private static string DecodeIntArray2String(int[] value)
        {
            var converted = BaseConvert(value, 62, 256);
            var builder = new System.Text.StringBuilder();
            for (var i = 0; i < converted.Length; i++)
            {
                builder.Append((char)converted[i]);
            }
            return builder.ToString();
        }

        private static int[] BaseConvert(int[] source, int sourceBase, int targetBase)
        {
            var result = new List<int>();
            int count = 0;
            while ((count = source.Length) > 0)
            {
                var quotient = new List<int>();
                int remainder = 0;
                for (var i = 0; i != count; i++)
                {
                    int accumulator = source[i] + remainder * sourceBase;
                    int digit = accumulator / targetBase;
                    remainder = accumulator % targetBase;
                    if (quotient.Count > 0 || digit > 0)
                    {
                        quotient.Add(digit);
                    }
                }

                result.Insert(0, remainder);
                source = quotient.ToArray();
            }

            return result.ToArray();
        }
    }
}