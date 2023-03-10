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


    }
}