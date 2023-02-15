using DownloadImageAPI.Models;
using DownloadImageAPI.Providers;
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

namespace DownloadImageAPI
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
        public static DataSet GetParameter(string varname)
        {
            DataSet ds = new DataSet();
            try
            {
                Hashtable InputData = new Hashtable();
                InputData.Add(Utility.Common.KEYNAME.IPCTRANCODE, "GETPARAMETER");
                InputData.Add(Utility.Common.KEYNAME.VARNAME, varname);
                InputData.Add(Common.SOURCEID, "IB");
                Hashtable OutputData = Common.autoTrans.ProcessTransHAS(InputData);
                if (OutputData[Utility.Common.KEYNAME.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)OutputData[Utility.Common.KEYNAME.DATASET];                
                }
                
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
               
            }
            return ds;
        }
        public static DataSet GetPathDocument(string documnetname,string contractno)
        {
            DataSet ds = new DataSet();
            try
            {
                Hashtable InputData = new Hashtable();
                InputData.Add(Utility.Common.KEYNAME.IPCTRANCODE, "GETPARAMETER");
                InputData.Add(Utility.Common.KEYNAME.DOCUMENTNAME, documnetname);
                InputData.Add(Utility.Common.KEYNAME.CONTRACTNO, contractno);
                Hashtable OutputData = Common.autoTrans.ProcessTransHAS(InputData);
                if (OutputData[Utility.Common.KEYNAME.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)OutputData[Utility.Common.KEYNAME.DATASET];
                }

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return ds;
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
        public static bool CheckPermission(string transid ,string userid)
        {
        
            Hashtable InputData = new Hashtable();
            InputData.Add(Utility.Common.KEYNAME.IPCTRANCODE, "CHECKPERMDOCUMENT");
            InputData.Add(Common.TRANSID, transid);
            InputData.Add(Common.USERID, userid);
            InputData.Add(Common.SOURCEID, "IB");
            Hashtable OutputData = Common.autoTrans.ProcessTransHAS(InputData);
            return OutputData[Utility.Common.KEYNAME.IPCERRORCODE].ToString().Equals(Common.ERRORCODE_SUCCESS);
        }
    }
}