using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Text;
using System.Security.Cryptography;
using System.IO;
using SmartPortal.Model;
using System.IO.Compression;
using System.Collections.Specialized;


namespace SmartPortal.Common
{
    /// <summary>
    /// Summary description for Encrypt
    /// </summary>
    public class Encrypt
    {
        /// <summary>
        /// Encrypt Data
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        /// 
        private static string password = "ABC@123*";
        private static int KEY_SIZE = 32;
        private static byte[] IV = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static string EncryptDataFB(string plaintext, string password)
        {
            byte[] key = new byte[KEY_SIZE];
            byte[] passwordbytes = Encoding.UTF8.GetBytes(password);

            for (int i = 0; i < KEY_SIZE; i++)
            {
                if (i >= passwordbytes.Length)
                    key[i] = 0;
                else
                    key[i] = passwordbytes[i];
            }

            byte[] encrypted;

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.KeySize = KEY_SIZE * 8;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(key, IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plaintext);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            string result = Convert.ToBase64String(encrypted);
            return String2Hex(result);
        }
        public static string DecryptDataFB(string cipherText, string password)
        {
            cipherText = Hex2String(cipherText);

            byte[] key = new byte[KEY_SIZE];
            byte[] passwordbytes = Encoding.UTF8.GetBytes(password);

            for (int i = 0; i < KEY_SIZE; i++)
            {
                if (i >= passwordbytes.Length)
                    key[i] = 0;
                else
                    key[i] = passwordbytes[i];
            }

            byte[] CipherTextBytes = Convert.FromBase64String(cipherText);
            string plaintext = null;

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.KeySize = KEY_SIZE * 8;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(key, IV);

                using (MemoryStream msDecrypt = new MemoryStream(CipherTextBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return plaintext;
        }
        private static string Hex2String(string hex)
        {
            string result = "";
            for (int i = 0; i < hex.Length - 1; i += 2)
            {
                int value = Convert.ToInt32(hex.Substring(i, 2), 16);
                string stringValue = Char.ConvertFromUtf32(value);
                char charValue = (char)value;
                result += charValue;
            }
            return result;
        }
        private static string String2Hex(string st)
        {
            string result = "";
            foreach (char letter in st)
            {
                int value = Convert.ToInt32(letter);
                string hexOutput = String.Format("{0:X}", value);
                result += hexOutput;
            }
            return result;
        }
        public static string EncryptData1(string Data)
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

                return HexEncoding.ToString(Result);
            }
            catch
            {
                return "";
            }
        }
        public static String SHA256_Encrypt(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
        public static string EncryptData(string Data)
        {
            return HttpContext.Current.Server.UrlEncode(Data);
        }

        /// <summary>
        /// Decrypt Data
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static string DecryptData1(string Data)
        {
            // try
            {
                int discard;
                byte[] PP = Encoding.Unicode.GetBytes("IPC");
                byte[] DataEncryptedByte = HexEncoding.GetBytes(Data, out discard);
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
            //catch
            {
                // return "";
            }
        }
        public static string EncryptSoftpin(string Data)
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
        public static string DecryptSoftpin(string Data)
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
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        public static string DecryptData(string Data)
        {
            return HttpContext.Current.Server.UrlDecode(Data);
        }
        public static string EncryptURL(string plaintext)
        {
            string uuid = string.Empty;
            string url = string.Empty;

            try
            {
                uuid = HttpContext.Current.Session["UUID"].ToString();
                uuid = "&uuid=" + uuid;
                plaintext = plaintext + uuid;
            }
            catch { }

            if (!bool.Parse(ConfigurationManager.AppSettings["isEncryptURL"].ToString()))
            {
                url = plaintext;
            }
            else if (plaintext.Trim().StartsWith("~/"))
            {
                url = "~/" + ConfigurationManager.AppSettings["routeurl"].ToString() + "/" + RandomString(100) + "?d=" + CompressString(plaintext.Replace("~/", ""), password);
            }
            else
            {
                url = ConfigurationManager.AppSettings["routeurl"].ToString() + "/" + RandomString(100) + "?d=" + CompressString(plaintext, password);

            }

            //if (!bool.Parse(ConfigurationManager.AppSettings["isEncryptURL"].ToString()))
            //{
            //    url = plaintext;
            //}
            //else if (plaintext.Trim().StartsWith("~/"))
            //{
            //    url = "~/" + ConfigurationManager.AppSettings["routeurl"].ToString() + "/" + RandomString(100) + "?d=" + CompressString(plaintext.Replace("~/", ""), password);
            //}
            //else
            //{
            //    url = ConfigurationManager.AppSettings["routeurl"].ToString() + "/" + RandomString(100) + "?d=" + CompressString(plaintext, password);

            //}
            return url;
        }
        public static string DecryptURL(string cipherText)
        {
            string url = string.Empty;
            if (!bool.Parse(ConfigurationManager.AppSettings["isEncryptURL"].ToString()))
                return cipherText;
            cipherText = cipherText.Replace(ConfigurationManager.AppSettings["routeurl"].ToString() + "/", "");
            if (cipherText.Trim().StartsWith("/"))
                cipherText = cipherText.Remove(0, 1);
            if (cipherText.Contains("?d="))
            {
                cipherText = cipherText.Substring(cipherText.IndexOf("?d=") + 3);
            }
                
            url = DecompressString(cipherText, password);
            return url;
        }
        public static DictionaryWithDefault<string, string> GetURLParam(string RawURL)
        {
            try
            {
                string pUrl = string.Empty;
                if (bool.Parse(ConfigurationManager.AppSettings["isEncryptURL"].ToString()))
                {
                    ////19.9.2017 minh add this:
                    //if (RawURL.IndexOf(".aspx") > 0 && !RawURL.ToLower().Contains("dafault.aspx"))
                    //{
                    //    pUrl = RawURL.Substring(RawURL.IndexOf("?"));
                    //}
                    //else
                    //{
                    //    string trimUrl = RawURL.Replace(ConfigurationManager.AppSettings["routeurlslash"].ToString(), "");
                    //    trimUrl = trimUrl.Trim().StartsWith("/") ? trimUrl.Trim().Substring(1) : trimUrl.Trim();
                    //    pUrl = DecryptURL(trimUrl);
                    //}

                    string trimUrl = RawURL.Replace(ConfigurationManager.AppSettings["routeurlslash"].ToString(), "");
                    trimUrl = trimUrl.Trim().StartsWith("/") ? trimUrl.Trim().Substring(1) : trimUrl.Trim();
                    if (!RawURL.Contains("&l="))
                    {
                        pUrl = DecryptURL(trimUrl);
                    }
                    else
                    {
                        pUrl = trimUrl;
                    }

                    if (pUrl.IndexOf(".aspx") > 0)
                    {
                        pUrl = pUrl.Substring(pUrl.IndexOf("?"));
                    }
                }
                else
                {
                    pUrl = RawURL.Substring(RawURL.IndexOf("?"));
                }

                pUrl = pUrl.Replace("?", "").Replace("+", "%2B");
                NameValueCollection qscoll = HttpUtility.ParseQueryString(pUrl);

                DictionaryWithDefault<string, string> lsParam = new DictionaryWithDefault<string, string>();
                foreach (String s in qscoll.AllKeys)
                {
                    lsParam.Add(s, qscoll[s].Trim());
                }
                //foreach (string t1 in pUrl.Split('&'))
                //{
                //    string[] t2 = t1.Split('=');
                //    lsParam.Add(t2[0].Trim(), t2[1].Trim());
                //}

                return lsParam;
            }
            catch (Exception ex)
            {
                //log
                if(RawURL.Contains("ibLogin") || RawURL.Contains("AjaxRequest") || RawURL.Contains("print.aspx"))
                {
                    return new DictionaryWithDefault<string, string>();
                }
                else
                {
                    HttpContext.Current.Response.Redirect(ConfigurationManager.AppSettings["loginpage"].ToString(), false);
                    return new DictionaryWithDefault<string, string>();
                }
            }
        }
        //from a ThaiTY rut ngan ma hoa base 64:

        static private byte[] encrypt(byte[] data, string password)
        {
            byte[] Keys = Encoding.ASCII.GetBytes(password);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ Keys[i % Keys.Length]);
            }
            return data;
        }

        static private byte[] Decrypt(byte[] data, string password)
        {
            byte[] Keys = Encoding.ASCII.GetBytes(password);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(Keys[i % Keys.Length] ^ data[i]);
            }
            return data;
        }
        static string Base64UrlEncode(byte[] arg)
        {
            string s = Convert.ToBase64String(arg); // Regular base64 encoder
            s = s.Split('=')[0]; // Remove any trailing '='s
            s = s.Replace('+', '-'); // 62nd char of encoding
            s = s.Replace('/', '_'); // 63rd char of encoding
            return s;
        }

        static byte[] Base64UrlDecode(string arg)
        {
            string s = arg;
            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding
            switch (s.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: s += "=="; break; // Two pad chars
                case 3: s += "="; break; // One pad char
                default:
                    throw new System.Exception(
             "Illegal base64url string!");
            }
            return Convert.FromBase64String(s); // Standard base64 decoder
        }
        /// <summary>
        /// Compresses the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string CompressString(string text, string pasword)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {

                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            gZipBuffer = encrypt(gZipBuffer, pasword);
            return Base64UrlEncode(gZipBuffer);
        }


        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="compressedText">The compressed text.</param>
        /// <returns></returns>
        public static string DecompressString(string compressedText, string pasword)
        {
            byte[] gZipBuffer = Base64UrlDecode(compressedText);
            gZipBuffer = Decrypt(gZipBuffer, pasword);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

    }
}
