using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SMSUtility
{
    public class Common
    {
        #region PUBLIC CONST
        public static class SMSSTATUS
        {
            public const string BEGIN = "N";
            public const string WAITINGPROCESS = "P";
            public const string FINISHED = "Y";
        }
        public static class KEYNAME
        {
            public const string IPCERRORCODE = "IPCERRORCODE";
            public const string IPCERRORDESC = "IPCERRORDESC";
            public const string MSGID = "MSGID";
            public const string RECEIVEDPHONE = "RECEIVEDPHONE";
            public const string SENDPHONE = "SENDPHONE";
            public const string MSGCONTENT = "MSGCONTENT";
            public const string MSGTYPE = "MSGTYPE";
            public const string PREFIX = "PREFIX";
            public const string MSGDATE = "MSGDATE";
            public const string MSGTIME = "MSGTIME";
            public const string SYSDATE = "SYSDATE";
            public const string STATUS = "STATUS";
            public const string IPCTRANSID = "IPCTRANSID";
            public const string IPCWORKDATE = "IPCWORKDATE";
            public const string PIORITY = "PIORITY";
            public const string IPCTRANCODE = "IPCTRANCODE";
            public const string SOURCEID = "SOURCEID";
            public const string STORENAME = "STORENAME";
            public const string PARA = "PARA";
            public const string TRANDESC = "TRANDESC";
            public const string CUSTTYPE = "CUSTTYPE";
            public const string TIMEINSERT = "TIMEINSERT";
            public const string FREQTIME = "FREQTIME";
            public const string STARTRECORD = "STARTRECORD";
            public const string MAXRECORD = "MAXRECORD";
            public const string AUTOBALANCEDATA = "AUTOBALANCEDATA";
            public const string NUMBERRECORD = "NUMBERRECORD";
            public const string ENDRECORD = "ENDRECORD";
            public const string ISNEXTDATE = "ISNEXTDATE";
			public const string USERID = "USERID";
            public const string ACCOUNTNO = "ACCOUNTNO";
            public const string PHONENO = "PHONENO";
            public const string PhoneNo = "PhoneNo";
            public const string DESTID = "DESTID";
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
        #endregion
    }
}
