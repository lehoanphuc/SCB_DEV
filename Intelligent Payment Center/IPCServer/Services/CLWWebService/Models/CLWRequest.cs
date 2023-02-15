using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLWWebService.Models
{
    public class CLWRequest
    {
        public string CODE { get; set; }
        public double AMOUNT { get; set; }
        public string OTP { get; set; } = string.Empty;
        public string PHONENO { get; set; } = string.Empty;
        public string AUDITNUMBER { get; set; }
        public string TERMINALID { get; set; }
    }

    public class CLWCreateCashCodeRequest
    {
        public string PHONENO { get; set; }
        public double AMOUNT { get; set; }
        public string CCYID { get; set; }
        public string TRANREF { get; set; }
        public string TRANDESC { get; set; }
        public int EXPIREDTIME { get; set; }
    }
}