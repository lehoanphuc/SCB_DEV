using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace LAPNETAPI.Models
{
    public class Response
    {
        public string result { get; set; }
    }
    public class InquiryResponse
    {
        public string CustomerName { get; set; }
        public string Account { get; set; }
        public string Phone { get; set; }
        public string Currency { get; set; }
        public string message { get; set; }
        public string errorcode { get; set; }

        public static implicit operator HttpContent(InquiryResponse v)
        {
            throw new NotImplementedException();
        }
    }

    public class TransferResponse
    {
        public string transid { get; set; }
        public string message { get; set; }
        public string transDate { get; set; }
        public string errorcode { get; set; }
    }



    public class ResponseAuthentication
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }
}