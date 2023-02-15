using Newtonsoft.Json.Linq;
using CLWWebService.Models;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using System.Net;
using System.IO;

namespace CLWWebService.Util
{
    public class Transactions
    {
        private ITransaction.AutoTrans autoTrans;
        private CLWResponse response = null;

        public object CardLessWithDrawalTransaction(CLWRequest request, string username)
        {
            try
            {
                if (autoTrans == null)
                {
                    autoTrans = (ITransaction.AutoTrans)Activator.GetObject(
                        typeof(ITransaction.AutoTrans),
                        "tcp://" + ConfigurationManager.AppSettings["RemotingAddress"].ToString() + ":" +
                        ConfigurationManager.AppSettings["RemotingPort"].ToString() + "/IPCAutoTrans");
                }

                Hashtable InputData = new Hashtable();
                InputData.Add(Utility.Common.KEYNAME.IPCTRANCODE, "SEMSCARDLESSWD");
                InputData.Add(Utility.Common.KEYNAME.SOURCEID, "SEMS");
                InputData.Add(Utility.Common.KEYNAME.REVERSAL, "N");
                InputData.Add("CASHCODE", request.CODE);
                InputData.Add("OTP", request.OTP);
                InputData.Add("PHONENO", request.PHONENO);
                InputData.Add("AUDITNUMBER", request.AUDITNUMBER);
                InputData.Add("TERMINALID", request.TERMINALID);
                InputData.Add("AMOUNT", request.AMOUNT);
                InputData.Add("USERNAME", username);
                Hashtable OutputData = autoTrans.ProcessTransHAS(InputData);

                return CreateResponseError(request, OutputData[Utility.Common.KEYNAME.IPCERRORCODE].ToString(), OutputData[Utility.Common.KEYNAME.IPCERRORDESC].ToString());
            }
            catch (Exception ex)
            {
                //Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return CreateResponseError(request, "9999", "System is suspended for a moment. Sorry for inconvenience. Please try again later.");
        }

        public object CreateCashCode(CLWCreateCashCodeRequest request, string username)
        {
            try
            {
                if (autoTrans == null)
                {
                    autoTrans = (ITransaction.AutoTrans)Activator.GetObject(
                        typeof(ITransaction.AutoTrans),
                        "tcp://" + ConfigurationManager.AppSettings["RemotingAddress"].ToString() + ":" +
                        ConfigurationManager.AppSettings["RemotingPort"].ToString() + "/IPCAutoTrans");
                }

                Hashtable InputData = new Hashtable();
                InputData.Add(Utility.Common.KEYNAME.IPCTRANCODE, "SEMSCCCTELLER");
                InputData.Add(Utility.Common.KEYNAME.SOURCEID, "SEMS");
                InputData.Add(Utility.Common.KEYNAME.REVERSAL, "N");
                InputData.Add("CCYID", request.CCYID);
                InputData.Add("SOURCETRANREF", request.TRANREF);
                InputData.Add("PHONENO", request.PHONENO);
                InputData.Add("EXPIRYTIME", request.EXPIREDTIME);
                InputData.Add("AMOUNT", request.AMOUNT);
                InputData.Add("TRANDESC", request.TRANDESC);
                InputData.Add("USERID", username);
                Hashtable OutputData = autoTrans.ProcessTransHAS(InputData);

                if (OutputData["IPCERRORCODE"].Equals("0"))
                {
                    return CreateCashCodeResponseError(OutputData[Utility.Common.KEYNAME.IPCERRORCODE].ToString(), OutputData[Utility.Common.KEYNAME.IPCERRORDESC].ToString(), OutputData["IPCTRANSID"].ToString());
                }
            }
            catch (Exception ex)
            {
                //Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return CreateCashCodeResponseError("9999", "System is suspended for a moment. Sorry for inconvenience. Please try again later.", "");
        }

        public object CreateResponseError(CLWRequest reqModel, string resCode, string resDesc, double amount = -1)
        {
            response = new CLWResponse();
            response.ERRORCODE = resCode;
            response.ERRORDESC = resDesc;
            response.AMOUNT = amount == -1 ? reqModel.AMOUNT : amount;
            return response;
        }

        public object CreateCashCodeResponseError(string resCode, string resDesc, string ipctransid)
        {
            CLWCreateCashCodeResponse res = new CLWCreateCashCodeResponse();
            res.ERRORCODE = resCode;
            res.ERRORDESC = resDesc;
            res.IPCTRANSID = string.IsNullOrEmpty(ipctransid) ? string.Empty : ipctransid;
            return res;
        }

        public bool CheckLogin(string clientID, string clientSecret)
        {
            try
            {
                if (autoTrans == null)
                {
                    autoTrans = (ITransaction.AutoTrans)Activator.GetObject(
                        typeof(ITransaction.AutoTrans),
                        "tcp://" + ConfigurationManager.AppSettings["RemotingAddress"].ToString() + ":" +
                        ConfigurationManager.AppSettings["RemotingPort"].ToString() + "/IPCAutoTrans");
                }

                Hashtable InputData = new Hashtable();
                InputData.Add(Utility.Common.KEYNAME.IPCTRANCODE, "CLWGETTOKEN");
                InputData.Add(Utility.Common.KEYNAME.SOURCEID, "SEMS");
                InputData.Add(Common.USERNAME, clientID.Trim());
                InputData.Add(Common.PASSWORD, sha_sha256(sha256(clientID.Trim().ToUpper()+clientSecret.Trim()), clientID.Trim()));
                Hashtable OutputData = autoTrans.ProcessTransHAS(InputData);

                string msgerror = string.Empty;
                if (!OutputData[Utility.Common.KEYNAME.IPCERRORCODE].ToString().Equals("0"))
                {
                    msgerror = OutputData[Utility.Common.KEYNAME.IPCERRORDESC].ToString();
                    Utility.ProcessLog.LogError(new Exception(msgerror), System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
                else
                {
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }

        public HttpResponseMessage ResponseFromPost(HttpRequestMessage Request, object param, HttpStatusCode statusCode)
        {
            HttpResponseMessage response = Request.CreateResponse<object>(statusCode, param);
            return response;
        }

        private string sha_sha256(string password, string loginName)
        {
            string satl = string.Empty;
            string shapassword = string.Empty;
            shapassword = password;
            if (shapassword.Length > 9)
            {
                satl = shapassword.Substring(6, 9).ToLower();
            }
            return sha256(shapassword + satl + loginName.ToUpper());
        }

        private string sha256(string password)
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

        public string EncryptData(string Data)
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

        public string DecryptData(string Data)
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
    }
}