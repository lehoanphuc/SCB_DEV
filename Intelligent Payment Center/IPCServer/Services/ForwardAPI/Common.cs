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

namespace InterbankTransferService
{
    public static class Common
    {
        public static void BypassCertificateError()
        {
            ServicePointManager.ServerCertificateValidationCallback +=

                delegate (
                    Object sender1,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        }

        public static Task<HttpResponseMessage> ForwardRequest(HttpRequestMessage Request, string url)
        {
            Common.BypassCertificateError();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //string url = ConfigurationManager.AppSettings.Get("GetSessionURL").ToString();

            Request.Headers.Remove("Host");
            Request.RequestUri = new Uri(url);

            var client = new HttpClient();
            return client.SendAsync(Request, HttpCompletionOption.ResponseHeadersRead);
        }
    }
}