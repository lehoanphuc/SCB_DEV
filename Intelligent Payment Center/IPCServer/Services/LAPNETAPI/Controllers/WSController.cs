using DBConnection;
using LAPNETAPI.Models;
using LAPNETAPI.Providers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LAPNETAPI.Controllers
{
    [DisplayName("Webservice")]
    public class WSController : ApiController
    {

        /// <summary>
        /// Get Session
        /// </summary>
        /// 
        [HttpGet]
        [Route("")]
        public HttpResponseMessage HelloAPI()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent("HelloAPI");
            return result;
        }
        [HttpPost]
        [Route("LAPNET/Inquiry")]
        public HttpResponseMessage InquiryWallet(InquiryRequest requestMessage)
        {
            try
            {
                InquiryResponse response = new InquiryResponse();            
                string errorcode = string.Empty;
                string errordesc = string.Empty;
                string transid = DateTime.Now.ToString("yyyyMMddHHmmssfff" + new Random().Next(1, 10));
                DataSet ds = Transactions.LAPNET_INCOMINGINQUIRY(requestMessage, ref errorcode, ref errordesc);
                if(errorcode.Equals(""))
                {
                    Utility.ProcessLog.LogInformation($" {transid} LOG REQUEST " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + JsonConvert.SerializeObject(requestMessage), Utility.Common.FILELOGTYPE.LOGFILEPATH);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        response.message = "Get Information Successfully!!";
                        response.errorcode = "0";
                        response.CustomerName = ds.Tables[0].Rows[0]["CUSTOMERNAME"].ToString();
                        response.Account = ds.Tables[0].Rows[0]["WALLETACCOUNT"].ToString();
                        response.Phone = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                        response.Currency = ds.Tables[0].Rows[0]["CURRENCY"].ToString();
                    }
                    Utility.ProcessLog.LogInformation($" {transid} LOG RESPONSE " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + response);
                    return Request.CreateResponse(HttpStatusCode.OK, response, "application/json");
                }              
                else
                {
                    Utility.ProcessLog.LogInformation($" {transid} LOG ERROR " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + errordesc);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, errordesc);                  
                }
            }
            catch (Exception ex)
            {
               
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Transactions.ResponseErrorCode(Request);
        }


        [HttpPost]
        [Route("LAPNET/Transfer")]
        public HttpResponseMessage TransferToWallet(TransferRequest transferRequest)
        {
            try
            {
                TransferResponse response = new TransferResponse();
                string errorcode = string.Empty;
                string errordesc = string.Empty;
                string transid = DateTime.Now.ToString("yyyyMMddHHmmssfff" + new Random().Next(1, 10));
                Utility.ProcessLog.LogInformation($" {transid} LOG REQUEST " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + JsonConvert.SerializeObject(transferRequest), Utility.Common.FILELOGTYPE.LOGFILEPATH);
                Hashtable result = Transactions.LAPNET_INCOMINGTRF(transferRequest, ref errorcode, ref errordesc);
                if (errorcode.Equals(""))
                {
                    if (result.ContainsKey("IPCTRANSID"))
                    {
                        response.transid = result["IPCTRANSID"].ToString();
                        response.transDate = result["TRANTIME"].ToString();
                        response.message = "Transaction Successfully!!";
                        response.errorcode = "0";
                        Utility.ProcessLog.LogInformation($" {transid} LOG RESPONSE " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + JsonConvert.SerializeObject(response));
                        return Request.CreateResponse(HttpStatusCode.OK, response, "application/json");
                    }                     
                }
                else
                {
                    Utility.ProcessLog.LogInformation($" {transid} LOG ERROR " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + errordesc);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, errordesc);
                }    
            }
            catch (Exception ex)
            {

                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Transactions.ResponseErrorCode(Request);
        }

    }
}