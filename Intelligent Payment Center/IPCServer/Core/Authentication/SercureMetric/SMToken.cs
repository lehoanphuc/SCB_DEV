using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Authentication.SecureMetricServer;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Configuration;

namespace Authentication.SercureMetric
{
    class SMToken
    {
        BasicHttpBinding binding = null;
        EndpointAddress address = null;
        OTPServerClient sm = null;        
        public string AuthenOTP( string UserName, string OTPToken)
        {
            try
            {
                string strUrl = "";
                
                if (sm == null)
                {
                    // Automagically find all client endpoints defined in app.config
                    ClientSection clientSection =
                        ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;
                    strUrl = clientSection.Endpoints[0].Address.AbsoluteUri;
                    Utility.ProcessLog.LogInformation("Login to SMToken:" + strUrl);
                    initCertificatePolicy();
                    //Specify the binding to be used for the client.                
                    binding = new BasicHttpBinding();
                    bool bSSL = true;
                    if (strUrl.IndexOf("https") < 0)
                    {
                        bSSL = false;
                    }
                    if (bSSL)
                    {
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    }

                    //Specify the address to be used for the client.
                    address = new EndpointAddress(strUrl);
                    Utility.ProcessLog.LogInformation("Login to SMToken before create sm");
                    sm = new OTPServerClient(binding, address);
                    Utility.ProcessLog.LogInformation("Login to SMToken after create sm");

                    //SecureMetricServer.OTPServerClient sm = new Authentication.SecureMetricServer.OTPServerClient();
                }
                Utility.ProcessLog.LogInformation("Login to SMToken before auth:" + UserName);
                int authenReturn = sm.auth(UserName, "", OTPToken);
                Utility.ProcessLog.LogInformation("Login to SMToken after auth:" + UserName);
                if (authenReturn == SMServerReturnCode.OTPR_CORE_SUCCESS)
                {
                    Utility.ProcessLog.LogInformation("Login to SMToken is OK");
                    return "OK";
                }
                else
                {
                    Utility.ProcessLog.LogInformation("Login to SMToken is fail:" + SMServerReturnCode.getReturnCodeInfo(authenReturn));
                    return SMServerReturnCode.getReturnCodeInfo(authenReturn);
                }
            }
            catch (Exception e )
            {
                Utility.ProcessLog.LogInformation(e.Message+ "trace:"+  e.StackTrace);
                return "Secure Metric Exception" + e.Message ;
            }

        }

        public static void initCertificatePolicy()
        {
            //Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
           
        }
        
    }
}
