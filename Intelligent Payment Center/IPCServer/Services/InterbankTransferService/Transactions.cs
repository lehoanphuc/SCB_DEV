using InterbankTransferService.Models;
using InterbankTransferService.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InterbankTransferService
{
    public class Transactions
    {
        
        public static bool CheckUserPass(string clientId, string clientSecret, OAuthValidateClientAuthenticationContext context, ref string errorDesc)
        {
            string ClientIP = "";
            ClientIP = context.Request.RemoteIpAddress;
            //ClientIP = Request.ServerVariables["remote_addr"];
            if (ClientIP.Equals("::1"))
                ClientIP = "127.0.0.1";
            
            Hashtable InputData = new Hashtable();
            InputData.Add(Utility.Common.KEYNAME.IPCTRANCODE, "DOWNSTREAMAPILOGIN");
            InputData.Add(Utility.Common.KEYNAME.SOURCEID, "SEMS");
            InputData.Add(Common.USERNAME, clientId);
            InputData.Add(Common.PASSWORD, sha_sha256(sha256(clientId.ToUpper() + clientSecret), clientId));
            InputData.Add(Common.IPADDRESS, ClientIP);

            Hashtable OutputData = Common.autoTrans.ProcessTransHAS(InputData);
            errorDesc = OutputData[Utility.Common.KEYNAME.IPCERRORDESC].ToString();
            return OutputData[Utility.Common.KEYNAME.IPCERRORCODE].ToString().Equals(Common.ERRORCODE_SUCCESS);
        }

        public static JToken GetInfoFromJson(string RequestName)
        {
            JArray jar = null;
            JObject js = null;
            JObject jstoken = new JObject();
            JToken result = null;
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataResponse.json");
            var myJsonString = File.ReadAllText(filename);
            try
            {
                jar = JArray.Parse(myJsonString);
            }
            catch
            {
                js = JObject.Parse(myJsonString);
            }
            if (jar == null) js = JObject.Parse(myJsonString);
            else
            {
                result = jar;
            }

            if (js != null) result = js;

            if (result != null)
            {
                return result.SelectToken(RequestName).Value<JToken>();
            }
            else
            {
                throw new Exception("Cannot found Example!");
            }
        }

        public static object InterbankDownstream(string methodname, string bodyrequest)
        {

            JObject jobj = new JObject();
            try
            {
                Hashtable InputData = new Hashtable();
                InputData.Add(Utility.Common.KEYNAME.IPCTRANCODE, "INTERBANKDOWNSTREAM");
                InputData.Add(Utility.Common.KEYNAME.SOURCEID, "SEMS");
                InputData.Add("BODYREQ", bodyrequest);
                InputData.Add("APIMETHODNAME", methodname.ToUpper());

                Hashtable OutputData = Common.autoTrans.ProcessTransHAS(InputData);

                jobj = JObject.Parse(OutputData["APIRESPONSE"].ToString());
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                jobj.Add(Common.result, Common.SystemError);
            }
            return jobj;
        }

        public static async Task<string> GetBodyString(StreamHelper outputCapture, IOwinResponse owinResponse, Stream owinResponseStream)
        {
            string responseBody = string.Empty;
            if (outputCapture.CapturedData.Length == 0)
            {
                owinResponse.Body.Position = 0;
                await owinResponse.Body.CopyToAsync(owinResponseStream);
            }
            else
            {
                outputCapture.CapturedData.Position = 0;
                outputCapture.CapturedData.CopyTo(owinResponse.Body);
            }

            owinResponse.Body.Seek(0, SeekOrigin.Begin);

            responseBody = Transactions.GetBodyRequest(owinResponse.Body);
            return responseBody;
        }

        public JObject ErrResponse(string errcode, string errdesc)
        {
            JObject jobj = new JObject();
            jobj.Add(Utility.Common.KEYNAME.ERRORCODE.ToLower(), errcode);
            jobj.Add(Utility.Common.KEYNAME.ERRORDESC.ToLower(), errdesc);
            return jobj;
        }

        public static string sha_sha256(string password, string loginName)
        {
            string satl = string.Empty;
            string outEnc = string.Empty;
            string shapassword = string.Empty;
            shapassword = password;
            if (shapassword.Length > 9)
            {
                satl = shapassword.Substring(6, 9).ToLower();
            }
            return sha256(shapassword + satl + loginName.ToUpper());
        }
        public static string sha256(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = string.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            byte[] array = crypto;
            for (int i = 0; i < array.Length; i++)
            {
                byte bit = array[i];
                hash += bit.ToString("x2");
            }
            return hash;
        }

        public static HttpResponseMessage ResponseErrorCode(HttpRequestMessage request)
        {
            JObject jErrorResponse = new JObject();
            jErrorResponse.Add(Common.result, Common.SystemError);
            return ResponseFromPost(request, jErrorResponse, HttpStatusCode.OK);
        }

        public static void ResponseAuthenFailed(HttpResponse response, string errorMsg = "")
        {
            response.Clear();
            response.StatusCode = (int)HttpStatusCode.OK;
            JObject jErrorResponse = new JObject();
            jErrorResponse.Add("code", "0014");
            jErrorResponse.Add("desc", errorMsg);
            response.ContentType = Common.jsontype;
            response.Write(jErrorResponse);
        }

        public static HttpResponseMessage ResponseSpecErrorCode(HttpRequestMessage request, string msg = "Request is invalid")
        {
            JObject jErrorResponse = new JObject();
            jErrorResponse.Add(Common.result, msg);
            return ResponseFromPost(request, jErrorResponse, HttpStatusCode.OK);
        }

        public static HttpResponseMessage ResponseFromPost(HttpRequestMessage Request, object param, HttpStatusCode statusCode)
        {
            HttpResponseMessage response = Request.CreateResponse<object>(statusCode, param);
            return response;
        }

        public static string GetBodyRequest(Stream stream)
        {
            try
            {
                var bodyStream = new StreamReader(stream);
                bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                return bodyStream.ReadToEnd();
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return string.Empty;
            }
        }
        public static void ReturnErrDefault(string exceptionMsg, int statusCode = 200)
        {
            JObject jNewRes = new JObject();
            jNewRes.Add("code", "0014");
            jNewRes.Add("desc", exceptionMsg);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.StatusCode = statusCode;
            HttpContext.Current.Response.ContentType = Common.jsontype;
            HttpContext.Current.Response.Write(jNewRes);
        }

        public static bool CheckURLPath(string urlpath)
        {
            return !(urlpath.Contains("swagger") || urlpath.Equals("/") || urlpath.Contains("/Error.aspx"));
        }

        public static bool IsAuthenAPI(string urlpath)
        {
            return CheckURLPath(urlpath) && urlpath.Equals(Path.Combine("/",ConfigurationManager.AppSettings["GetTokenPath"].ToString()));
        }

        public static bool CheckBodyRequest(string body, Type type)
        {
            try
            {
                bool isValid = true;

                var model = JsonConvert.DeserializeObject(body, type);
                ValidationContext context = new ValidationContext(model, null, null);
                List<ValidationResult> validationResults = new List<ValidationResult>();
                isValid = Validator.TryValidateObject(model, context, validationResults, true);
                if (!isValid)
                {
                    string msg = string.Empty;
                    foreach (ValidationResult result in validationResults)
                    {
                        msg += result.ErrorMessage + "|";
                    }
                    Utility.ProcessLog.LogError(new Exception("Request is invalid: " + msg), System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
                return isValid;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }

        public static string KillSqlInjection(string TexttoValidate)
        {
            string TextVal;

            TextVal = TexttoValidate;
            if (String.IsNullOrEmpty(TextVal))
            {
                return TextVal;
            }

            //Build an array of characters that need to be filter.
            string[] strDirtyQueryString = { "xp_", "$", "#", "%", "*", "^", "&", "!", ";", "--", "<", ">", "=", "script", "iframe", "delete", "drop", "exec", "or", "and" };

            //Loop through all items in the array
            foreach (string item in strDirtyQueryString)
            {
                if (TextVal.IndexOf(item) != -1)
                {
                    TextVal = TextVal.Replace(item, "");
                }
            }

            return TextVal;
        }

        public static T CustomJsonParse<T>(string json)
        {
            if (typeof(T) != typeof(JObject) && typeof(T) != typeof(JArray) && typeof(T) != typeof(JToken))
            {
                throw new Exception("This method just support JObject, JArray, JToken");
            }
            JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();
            using (var stringReader = new StringReader(json))
            using (var jsonTextReader = new JsonTextReader(stringReader))
            {
                jsonTextReader.DateParseHandling = DateParseHandling.None;
                jsonTextReader.FloatParseHandling = FloatParseHandling.Decimal;
                JsonLoadSettings loadSettings = new JsonLoadSettings
                {
                    DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Error
                };
                var jtoken = JToken.ReadFrom(jsonTextReader, loadSettings);
                return (T)jtoken.ToObject<T>(jsonSerializer);
            }
        }


    }
}