using InterbankTransferService.Models;
using InterbankTransferService.Providers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace InterbankTransferService.Controllers
{
    [DisplayName("Webservice")]
    [CustomUnAuthorization]
    public class WSController : ApiController
    {

        /// <summary>
        /// Upstream CCT API
        /// </summary>
        [HttpPost]
        [Route("CBMAPI/Upstream/UpstreamCCT")]
        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerRequestExample(typeof(UpstreamCCT), typeof(UpstreamCCTModelExample))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(UpstreamCCTResModelExample))]
        public HttpResponseMessage UpstreamCCT([FromBody] UpstreamCCT upstreamCct)
        {
            try
            {
                string transid = DateTime.Now.ToString("yyyyMMddHHmmssfff" + new Random().Next(1, 10));
                string body = Transactions.GetBodyRequest(HttpContext.Current.Request.InputStream);
                Utility.ProcessLog.LogInformation($" {transid} LOG REQUEST " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + body, Utility.Common.FILELOGTYPE.LOGFILEPATH);

                object response = Transactions.InterbankDownstream("pacs008", body);

                Utility.ProcessLog.LogInformation($" {transid} LOG RESPONSE " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + response);
                return Request.CreateResponse(HttpStatusCode.OK, response, "application/json");
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Transactions.ResponseErrorCode(Request);
        }

        /// <summary>
        ///UpstreamNotificationToReceive
        /// </summary>
        [HttpPost]
        [Route("CBMAPI/Upstream/UpstreamNotificationToReceive")]
        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerRequestExample(typeof(UpstreamNotificationToReceive), typeof(UpstreamNotificationToReceiveModelExample))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(UpstreamNotificationToReceiveResModelExample))]
        public HttpResponseMessage UpstreamNotificationToReceive([FromBody] UpstreamNotificationToReceive upstreamNotificationToReceive)
        {
            try
            {
                string transid = DateTime.Now.ToString("yyyyMMddHHmmssfff" + new Random().Next(1, 10));
                string body = Transactions.GetBodyRequest(HttpContext.Current.Request.InputStream);
                Utility.ProcessLog.LogInformation($" {transid} LOG REQUEST " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + body, Utility.Common.FILELOGTYPE.LOGFILEPATH);

                object response = Transactions.InterbankDownstream("camt057", body);

                Utility.ProcessLog.LogInformation($" {transid} LOG RESPONSE " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + response);
                return Request.CreateResponse(HttpStatusCode.OK, response, "application/json");
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Transactions.ResponseErrorCode(Request);
        }


        /// <summary>
        ///UpstreamCCTFailValidation
        /// </summary>
        [HttpPost]
        [Route("CBMAPI/Upstream/UpstreamCCTFailValidation")]
        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerRequestExample(typeof(UpstreamCCTFailValidation), typeof(UpstreamCCTFailValidationModelExample))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(UpstreamCCTFailValidationResModelExample))]
        public HttpResponseMessage UpstreamCCTFailValidation([FromBody] UpstreamCCTFailValidation upstreamCCTFailValidation)
        {
            try
            {
                string transid = DateTime.Now.ToString("yyyyMMddHHmmssfff" + new Random().Next(1, 10));
                string body = Transactions.GetBodyRequest(HttpContext.Current.Request.InputStream);
                Utility.ProcessLog.LogInformation($" {transid} LOG REQUEST " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + body, Utility.Common.FILELOGTYPE.LOGFILEPATH);

                object response = Transactions.InterbankDownstream("admin002", body);

                Utility.ProcessLog.LogInformation($" {transid} LOG RESPONSE " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + response);
                return Request.CreateResponse(HttpStatusCode.OK, response, "application/json");
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Transactions.ResponseErrorCode(Request);
        }

        /// <summary>
        ///UpstreamCCTSuccessValidation
        /// </summary>
        [HttpPost]
        [Route("CBMAPI/Upstream/UpstreamCCTSuccessValidation")]
        [SwaggerConsumes("application/json")]
        [SwaggerProduces("application/json")]
        [SwaggerRequestExample(typeof(UpstreamCCTSuccessValidation), typeof(UpstreamCCTSuccessValidationModelExample))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(UpstreamCCTSuccessValidationResModelExample))]
        public HttpResponseMessage UpstreamCCTSuccessValidation([FromBody] UpstreamCCTSuccessValidation upstreamCCTSuccessValidation)
        {
            try
            {
                string transid = DateTime.Now.ToString("yyyyMMddHHmmssfff" + new Random().Next(1, 10));
                string body = Transactions.GetBodyRequest(HttpContext.Current.Request.InputStream);
                Utility.ProcessLog.LogInformation($" {transid} LOG REQUEST " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + body, Utility.Common.FILELOGTYPE.LOGFILEPATH);

                object response = Transactions.InterbankDownstream("pacs002", body);

                Utility.ProcessLog.LogInformation($" {transid} LOG RESPONSE " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + response);
                return Request.CreateResponse(HttpStatusCode.OK, response, "application/json");
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Transactions.ResponseErrorCode(Request);
        }
    }
}