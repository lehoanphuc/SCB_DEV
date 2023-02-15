using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DownloadImageAPI.Models
{
    public class Response
    {
        public string result { get; set; }
    }

    public class ResponseAuthentication
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }
}