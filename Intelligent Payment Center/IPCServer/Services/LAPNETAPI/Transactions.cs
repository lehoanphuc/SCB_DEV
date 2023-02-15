using LAPNETAPI.Models;
using LAPNETAPI.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LAPNETAPI
{
    public class Transactions
    {
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

        public static HttpResponseMessage ResponseErrorCode(HttpRequestMessage request)
        {
            JObject jErrorResponse = new JObject();
            jErrorResponse.Add(Common.result, Common.SystemError);
            return ResponseFromPost(request, jErrorResponse, HttpStatusCode.OK);
        }
        public static HttpResponseMessage ResponseFromPost(HttpRequestMessage Request, object param, HttpStatusCode statusCode)
        {
            HttpResponseMessage response = Request.CreateResponse<object>(statusCode, param);
            return response;
        }
        public static DataSet LAPNET_INCOMINGINQUIRY(InquiryRequest request, ref string errorCode, ref string errorDesc)
        {
            DataSet ds = new DataSet();
            try
            {
                Hashtable InputData = new Hashtable();
                InputData.Add(Utility.Common.KEYNAME.IPCTRANCODE, "LAPNET_INCOMINGINQ");
                InputData.Add(Utility.Common.KEYNAME.USERID, request.userid);
                InputData.Add(Utility.Common.KEYNAME.CCYID, request.currency);
                InputData.Add(Common.SOURCEID, "SEMS");
                Hashtable OutputData = Common.autoTrans.ProcessTransHAS(InputData);
                if (OutputData[Utility.Common.KEYNAME.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)OutputData[Utility.Common.KEYNAME.DATASET];
                }
                else
                {
                    errorCode = OutputData[Utility.Common.KEYNAME.IPCERRORCODE].ToString();
                    errorDesc = OutputData[Utility.Common.KEYNAME.IPCERRORDESC].ToString();
                }
                return ds;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                errorCode = ex.ToString();
                errorDesc = ex.ToString();
                throw ex;
            }
            
        }
        public static Hashtable LAPNET_INCOMINGTRF(TransferRequest transferRequest, ref string errorCode, ref string errorDesc)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable InputData = new Hashtable();
                InputData.Add(Utility.Common.KEYNAME.IPCTRANCODE, "LAPNET_INCOMINGTRF");
                InputData.Add(Utility.Common.KEYNAME.SOURCEID, "SEMS");
                InputData.Add(Utility.Common.KEYNAME.USERID, transferRequest.userid);
                InputData.Add(Utility.Common.KEYNAME.FACCTNO, transferRequest.fromaccount);
                InputData.Add(Utility.Common.KEYNAME.DRGLACCTNO, transferRequest.fromaccountgl);
                InputData.Add(Utility.Common.KEYNAME.WALLETACCT, transferRequest.toaccount);
                InputData.Add(Utility.Common.KEYNAME.AMOUNT, transferRequest.amount);
                InputData.Add(Utility.Common.KEYNAME.TRANDESC, transferRequest.purpose);
                InputData.Add(Utility.Common.KEYNAME.FROMBANK, transferRequest.frommember);
                InputData.Add(Utility.Common.KEYNAME.TOBANK, "PSVB");
                Hashtable OutputData = Common.autoTrans.ProcessTransHAS(InputData);
                if (OutputData[Utility.Common.KEYNAME.IPCERRORCODE].ToString() == "0")
                {
                    result = OutputData;
                }
                else
                {
                    errorCode = OutputData[Utility.Common.KEYNAME.IPCERRORCODE].ToString();
                    errorDesc = OutputData[Utility.Common.KEYNAME.IPCERRORDESC].ToString();
                }
                return result;

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
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
        public static string EncryptData(string Data)
        {
            try
            {
                byte[] PP = Encoding.Unicode.GetBytes("IPC");
                byte[] DataByte = Encoding.Unicode.GetBytes(Data);
                HashAlgorithm HashPassword = HashAlgorithm.Create("MD5");
                byte[] V = { 0, 9, 0, 4, 4, 9, 5, 9, 4, 2, 2, 2, 0, 6, 8, 2 };
                RijndaelManaged EncryptData = new RijndaelManaged();
                EncryptData.Key = HashPassword.ComputeHash(PP);
                ICryptoTransform encryptor = EncryptData.CreateEncryptor(EncryptData.Key, V);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                cs.Write(DataByte, 0, DataByte.Length);
                cs.FlushFinalBlock();
                byte[] Result = ms.ToArray();
                ms.Close();
                cs.Close();
                EncryptData.Clear();
                return Convert.ToBase64String(Result);
            }
            catch
            {
                return "";
            }
        }

        public static string DecryptData(string Data)
        {
            try
            {
                byte[] PP = Encoding.Unicode.GetBytes("IPC");
                byte[] DataEncryptedByte = Convert.FromBase64String(Data);
                HashAlgorithm HashPassword = HashAlgorithm.Create("MD5");
                byte[] V = { 0, 9, 0, 4, 4, 9, 5, 9, 4, 2, 2, 2, 0, 6, 8, 2 };
                RijndaelManaged Decrypt = new RijndaelManaged();
                Decrypt.Key = HashPassword.ComputeHash(PP);
                ICryptoTransform decryptor = Decrypt.CreateDecryptor(Decrypt.Key, V);
                MemoryStream ms = new MemoryStream(DataEncryptedByte);
                CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                byte[] Result = new byte[DataEncryptedByte.Length];
                cs.Read(Result, 0, Result.Length);
                ms.Close();
                cs.Close();
                return Encoding.Unicode.GetString(Result).Replace("\0", "");
            }
            catch
            {
                return "";
            }
        }
        public static void HashTableAddOrSet(Hashtable objInput, object key, object value)
        {
            try
            {
                if (objInput.ContainsKey(key))
                {
                    objInput[key] = value;
                }
                else
                {
                    objInput.Add(key, value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
    }
}