using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLWWebService.Models
{
    public class CLWResponse
    {
        public string ERRORCODE { get; set; }
        public string ERRORDESC { get; set; }
        public double AMOUNT { get; set; } = 0;
    }

    public class CLWCreateCashCodeResponse
    {
        public string ERRORCODE { get; set; }
        public string ERRORDESC { get; set; }
        public string IPCTRANSID { get; set; }
    }
}