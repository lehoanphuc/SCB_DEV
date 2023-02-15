using CLWWebService.Models;
using System;
using System.Net.Http;
using System.Web.Http;
using CLWWebService.Util;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Web.Http.Controllers;

namespace CLWWebService.Controllers
{
    [CustomUnAuthorization]
    public class CardLessWithDrawalController : ApiController
    {
        private Transactions transactions = new Transactions();

        [HttpPost]
        [Route("v1/cardlesswithdrawal")]
        public HttpResponseMessage Post([FromBody]CLWRequest clwrequest)
        {
            try
            {
                return transactions.ResponseFromPost(Request, transactions.CardLessWithDrawalTransaction(clwrequest, User.Identity.Name), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            JObject jErrorResponse = new JObject();
            jErrorResponse.Add("MessageError", "Request is invalid");
            return transactions.ResponseFromPost(Request, jErrorResponse, HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("v1/createcashcode")]
        public HttpResponseMessage CreateCashCode([FromBody] CLWCreateCashCodeRequest createcashcode)
        {
            try
            {
                return transactions.ResponseFromPost(Request, transactions.CreateCashCode(createcashcode, User.Identity.Name), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            JObject jErrorResponse = new JObject();
            jErrorResponse.Add("MessageError", "Request is invalid");
            return transactions.ResponseFromPost(Request, jErrorResponse, HttpStatusCode.BadRequest);
        }
    }
}