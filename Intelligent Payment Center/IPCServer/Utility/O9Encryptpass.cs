using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Utility
{
	public class O9Encryptpass
	{
		private static string CONSTKEY = "abhf@311";

		public static string Decrypt(string textToDecrypt)
		{
			RijndaelManaged rijndaelCipher = new RijndaelManaged();
			rijndaelCipher.Mode = CipherMode.CBC;
			rijndaelCipher.Padding = PaddingMode.PKCS7;
			rijndaelCipher.KeySize = 128;
			rijndaelCipher.BlockSize = 128;
			byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
			byte[] pwdBytes = Encoding.UTF8.GetBytes(O9Encryptpass.CONSTKEY);
			byte[] keyBytes = new byte[16];
			int len = pwdBytes.Length;
			if (len > keyBytes.Length)
			{
				len = keyBytes.Length;
			}
			Array.Copy(pwdBytes, keyBytes, len);
			rijndaelCipher.Key = keyBytes;
			rijndaelCipher.IV = keyBytes;
			byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
			return Encoding.UTF8.GetString(plainText);
		}

		public string Encrypt(string textToEncrypt)
		{
			RijndaelManaged rijndaelCipher = new RijndaelManaged();
			rijndaelCipher.Mode = CipherMode.CBC;
			rijndaelCipher.Padding = PaddingMode.PKCS7;
			rijndaelCipher.KeySize = 128;
			rijndaelCipher.BlockSize = 128;
			byte[] pwdBytes = Encoding.UTF8.GetBytes(O9Encryptpass.CONSTKEY);
			byte[] keyBytes = new byte[16];
			int len = pwdBytes.Length;
			if (len > keyBytes.Length)
			{
				len = keyBytes.Length;
			}
			Array.Copy(pwdBytes, keyBytes, len);
			rijndaelCipher.Key = keyBytes;
			rijndaelCipher.IV = keyBytes;
			ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
			byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
			return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
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

		public static string sha_sha256(string password, string loginName)
		{
            loginName = loginName.ToUpper();
            string satl = string.Empty;
			string outEnc = string.Empty;
			string shapassword = string.Empty;
            shapassword = O9Encryptpass.sha256(loginName+password);
			if (shapassword.Length > 9)
			{
				satl = shapassword.Substring(6, 9).ToLower();
			}
			return O9Encryptpass.sha256(shapassword + satl + loginName);
		}

		public static X509Certificate2 GetCertificateBySerialNumber(X509Store store, string serialNumber)
		{
			X509Certificate2 selectedCertificate = null;
			serialNumber = serialNumber.Replace(" ", "").ToUpper();
			try
			{
				store.Open(OpenFlags.OpenExistingOnly);
				X509Certificate2Collection allCertificates = store.Certificates;
				X509Certificate2Enumerator enumerator = allCertificates.GetEnumerator();
				while (enumerator.MoveNext())
				{
					X509Certificate2 x509 = enumerator.Current;
					if (serialNumber.EndsWith(x509.GetSerialNumberString()) && serialNumber.StartsWith(x509.GetSerialNumberString()))
					{
						selectedCertificate = x509;
						break;
					}
				}
			}
			finally
			{
				if (store != null)
				{
					store.Close();
				}
			}
			return selectedCertificate;
		}
        public static string sha_shapass(string shapassword, string loginName)
        {

            string satl = string.Empty;
            if (shapassword.Length > 9)
            {
                satl = shapassword.Substring(6, 9).ToLower();
            }
            return O9Encryptpass.sha256(shapassword + satl + loginName.ToUpper());
        }
	}
}
