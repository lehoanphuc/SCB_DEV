using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SmartPortal.Control
{
    public class Security
    {
        static byte[] CC_PP = Encoding.Unicode.GetBytes("CC");
        static byte[] CC_IV = { 0, 6, 2, 8, 8, 2, 1, 7, 4, 2, 3, 2, 9, 6, 8, 9 };

        public static string EncryptCard(string sinput)
        {
            try
            {
                byte[] DataByte = Encoding.Unicode.GetBytes(sinput.ToString());
                HashAlgorithm HashPassword = HashAlgorithm.Create("MD5");
                RijndaelManaged EncryptData = new RijndaelManaged();
                EncryptData.Key = HashPassword.ComputeHash(CC_PP);
                ICryptoTransform encryptor = EncryptData.CreateEncryptor(EncryptData.Key, CC_IV);
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
        public static string DecryptCard(string sinput)
        {
            try
            {
                byte[] DataEncryptedByte = Convert.FromBase64String(sinput.ToString());
                HashAlgorithm HashPassword = HashAlgorithm.Create("MD5");
                RijndaelManaged Decrypt = new RijndaelManaged();
                Decrypt.Key = HashPassword.ComputeHash(CC_PP);
                ICryptoTransform decryptor = Decrypt.CreateDecryptor(Decrypt.Key, CC_IV);
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
