using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Runtime.Remoting.Channels.Tcp;
using System.Data;
using System.Net;
using System.Net.Sockets;
using Microsoft.VisualBasic;
using DBConnection;
using System.Globalization;
using System.Net.Mail;
using System.Net.Mime;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Configuration;
using Antlr3.ST;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using NCalc2;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Utility
{

    public static class Common
    {
        public static class CARTMASK
        {
            public const string LEFTMASK = "4";
            public const string RIGHTMASK = "4";
        }

        static private Byte[] m_Key = new Byte[8];
        static private Byte[] m_IV = new Byte[8];

        //You need to replace this key with your own Key.
        private static string strThekey = "5DFE6T@H478HGY@562";


        #region Private Variable
        static object LockerTransID = new object();
        static object LockerTranProcessingCount = new object();
        #endregion

        #region Public Variable
        #region Connection Information
        public static string ConStr = "";
        public static string DBServerName = "";
        public static string DBUserID = "";
        public static string DBPassword = "";
        public static string DBAuthentication = "";
        public static string DBName = "";
        public static int ConTimeOut = 0;
        #endregion

        #region Service Information
        public static long IPCTransID = 0;
        public static string IPCWorkDate;
        public static Hashtable SYSVAR = new Hashtable();
        public static Hashtable CUSTOM = new Hashtable();

        public static bool ServiceStarted = false;
        public static long TranProcessingCount = 0;
        public static string ServiceInformation = "";
        public static FixedSizedQueue<string> LstSchedule = new FixedSizedQueue<string>();
        #endregion

        #region Database Information
        public static DataTable DBITRANLIST;
        public static DataTable DBIPARMINFO;
        public static DataTable DBIDATADEFINE;

        public static DataTable DBICONNECTIONDB;
        public static DataTable DBICONNECTIONWS;

        public static DataTable DBIERRORLIST;
        public static DataTable DBIERRORLISTSOURCE;
        public static DataTable DBIERRORLISTDEST;

        public static DataTable DBIINPUTDEFINEISO;
        public static DataTable DBIINPUTDEFINESMS;
        public static DataTable DBIINPUTDEFINEXML;
        public static DataTable DBIINPUTDEFINEHTTP;

        public static DataTable DBIOUTPUTDEFINEXML;
        public static DataTable DBIOUTPUTDEFINEISO;
        public static DataTable DBIOUTPUTDEFINESEP;
        public static DataTable DBIOUTPUTDEFINESMS;
        public static DataTable DBIOUTPUTDEFINEHAS;
        public static DataTable DBIOUTPUTDEFINEPAR;
        public static DataTable DBIOUTPUTDEFINEHTTP;
        public static DataTable DBIOUTPUTDEFINEHTTPHEADER;

        public static DataTable DBIMAPIPCTRANCODEISO;
        public static DataTable DBIMAPIPCTRANCODESMS;

        public static DataTable DBIEBAPUSHNOTIFICATION;
        public static DataTable DBILOGDEFINE;
        public static DataTable DBILOGTRANSDETAILFIELD;
        public static DataTable DBILOGDEFINEREQUESTFORLOAN;

        public static DataTable DBIAUTHENTICATION;

        public static DataTable DBIMAPIPCTRANCODEPUSH;
        public static DataTable DBIOUTPUTDEFINEPUSH;
        public static DataTable DBIMAPXMLTOFIELD;

        #endregion

        #region Config Param
        public static string OTPUser = "";
        public static string OTPPass = "";
        public static long OTPLifeTime = 0;
        #endregion

        #region cluster config
        public static int CLUSTERID = 0;
        #endregion

        #endregion

        #region scheduler threading
        public static Dictionary<string, Thread> lsRunningThreads = new Dictionary<string, Thread>();

        #endregion

        #region Public Const
        public static string IPCINSTANCE = "IPC";
        public static class TRANSTATUS
        {
            public const string BEGIN = "0";
            public const string FINISH = "1";
            public const string ERROR = "2";
            public const string WAITING_APPROVE = "3";
            public const string REJECTED = "4";

            public const string PENDDING = "5";
        }
        public static class TRANSIPCTHREAD
        {
            public static bool CONFIG_RUNSCHEDULE = true;
        }
        public static class APPROVESTATUS
        {
            public const string BEGIN = "0";
            public const string WAITTINGCUST = "1";
            public const string WAITTINGBANK = "2";
            public const string APPROVED = "3";
            public const string REJECT = "4";
            public const string REJECTED = "5";
            public const string TIMEOUT = "10";
            public const string EODREJECT = "11";

        }
        public static class OFFLSTS
        {
            public const string BEGIN = "";
            public const string BEGINSYN = "0";
            public const string FINISHSYN = "1";
            public const string ERRORSYN = "2";
        }
        public static class DELSTS
        {
            public const string NORMAL = "0";
            public const string DELETED = "1";
        }
        public static class SYSTEMSTS
        {
            public const string ONLINE = "0";
            public const string BLOCKSYSTEM = "1";
            public const string OFFLINE = "2";
            public const string WAITINGCOREACTIVE = "3";
        }
        public static class ERRORCODE
        {
            public const string OK = "0";
            public const string SYSTEMBLOCK = "9001";
            public const string SYSTEMWAITINGCOREACTIVE = "9002";
            public const string SYSTEM = "9999";
            public const string SERVICEINTERRUPT = "9998";
            public const string PERMISSONDENIED = "9008";
            public const string CONTINUEWAITTING = "9004";
            public const string ENDOFDAYERROR = "9996";
            public const string DESTSYSTEM = "8888";
            public const string SOURCETRANREF_EXISTED = "1";
            public const string SOURCETRANREF_NOTEXIST = "2";
            public const string TRAN_REVERTED = "3";
            public const string DESTID_NULL = "4";
            public const string INVALID_MESSAGE_REQUEST = "5";
            public const string INVALID_TYPEGET = "6";
            public const string INVALID_RESULT = "7";//DS = null hoac so luong table ko dung 
            public const string INVALID_PARAM = "8";//DostoreSelect
            public const string INVALID_DESTID = "9";//DostoreDest
            public const string INVALID_MESSAGE_RESPONSE = "10";
            public const string MESSAGE_RECEIVE_TIMEOUT = "11";
            public const string INVALID_TYPE_CONN = "12";//DostoreDest
            public const string EXECNONQUERY_FALSE = "13";
            public const string SEND_SOAP_MESSAGE_TIMEOUT = "20";
            public const string INVALID_WS_URL = "21";
            public const string INVALID_WSMAST = "22";
            public const string INVALID_AMOUNT = "23";
            public const string INVALID_AUTHENCODE = "24";
            public const string INVALID_AUTHENINFO = "25";
            public const string INVALID_AUTHENTYPE = "26";
            public const string INVALID_SENDTYPE = "1013";
            public const string INVALID_PHONENOLIST = "1014";
            public const string RESPONSE_OK = "200";
        }
        public static class ERRORDESC
        {
            public const string INVALID_AUTHENINFO = "USER NOT REGISTER AUTHENTICATION TYPE YET.";
            public const string INVALID_AUTHENTYPE = "INVALID AUTHENTICATION TYPE.";
        }
        public static class SYSVARNAME
        {
            public const string SYSTEMSTATUS = "SYSTEMSTATUS";
            public const string IPCWORKDATE = "IPCWORKDATE";
        }
        public static class LOGTYPE
        {
            public const string INPUTSOURCE = "IS";
            public const string OUTPUTSOURCE = "OS";
            public const string INPUTDEST = "ID";
            public const string OUTPUTDEST = "OD";
        }
        public static class MESSAGETYPE
        {
            public const string XML = "XML";
            public const string ISO = "ISO";
            public const string SEP = "SEP";
            public const string SMS = "SMS";
            public const string HAS = "HAS";
            public const string PAR = "PAR";
            public const string JSON = "JSON";
        }
        public static class KEYNAME
        {

            public const string CONTRACTNO = "CONTRACTNO";
            public const string WALLETACCT = "WALLETACCT";
            public const string DDACCTNO = "DDACCTNO";
            public const string DRGLACCTNO = "DRGLACCTNO";
            public const string FROMBANK = "FROMBANK";
            public const string TOBANK = "TOBANK";
            public const string DOCUMENTNAME = "DOCUMENTNAME";
            public const string VARNAME = "VARNAME";
            public const string DOCUMENT = "DOCUMENT";
            public const string TRANSID = "TRANSID";
            public const string TRANREF = "TRANREF";
            public const string IPCTRANSID = "IPCTRANSID";
            public const string IPCTRANCODE = "IPCTRANCODE";
            public const string IPCTRANDESC = "IPCTRANDESC";
            public const string SHORTTRANDESC = "SHORTTRANDESC";
            public const string SOURCEID = "SOURCEID";
            public const string DESTID = "DESTID";
            public const string USERID = "USERID";
            public const string USERLEV = "LEV";
            public const string SOURCETRANREF = "SOURCETRANREF";
            public const string DESTTRANREF = "DESTTRANREF";
            public const string SOURCETRANGETSTS = "SOURCETRANGETSTS";
            public const string STATUSTRAN = "STATUSTRAN";
            public const string TRANDATE = "TRANDATE";
            public const string REVERSAL = "REVERSAL";
            public const string TRANDESC = "TRANDESC";
            public const string OFFLSTS = "OFFLSTS";
            public const string IPCERRORCODE = "IPCERRORCODE";
            public const string IPCERRORDESC = "IPCERRORDESC";
            public const string ERRORCODE = "ERRORCODE";
            public const string ERRORDESC = "ERRORDESC";
            public const string DELETED = "DELETED";
            public const string HISTORYINFO = "HISTORYINFO";
            public const string CONNSTRING = "CONNSTRING";
            public const string PHONENO = "PHONENO";
            public const string PREFIX = "PREFIX";
            public const string FIELDNAME = "FIELDNAME";
            public const string POSITION = "POS";
            public const string SELECTRESULT = "SELECTRESULT";
            public const string DATARESULT = "DATARESULT";
            public const string DOSTORESELECTRESULT = "DOSTORESELECTRESULT";
            public const string DESTERRORCODE = "DESTERRORCODE";
            public const string MSGTYPE = "MSGTYPE";
            public const string PROCESSCODE = "PROCESSCODE";
            public const string MSGTYPERP = "MSGTYPERP";
            public const string PROCESSCODERP = "PROCESSCODERP";
            public const string APPROVESTATUS = "APPRSTS";
            public const string SYSROLOVEIND = "SYSROLOVEIND";
            public const string NEWBUSDAY = "NEWBUSDAY";
            public const string MSGDATE = "MSGDATE";
            public const string MSGTIME = "MSGTIME";
            public const string MSGID = "MSGID";
            public const string SMSTRANCODE = "SMSTRANCODE";
            public const string MSGCONTENT = "MSGCONTENT";
            public const string RESPONSETEMPLATE = "RESPONSETEMPLATE";
            public const string ACCTNO = "ACCTNO";
            public const string AMOUNT = "AMOUNT";
            public const string CCYID = "CCYID";
            public const string APPROVED = "APPROVED";
            public const string NEXTUSERLEV = "NEXTLEVAPP";
            public const string AUTHENTYPE = "AUTHENTYPE";
            public const string AUTHENCODE = "AUTHENCODE";
            public const string DESTTRANID = "TRANID";
            public const string ISBATCH = "ISBATCH";
            public const string BATCHREF = "BATCHREF";
            public const string RECEIVERACCOUNT = "RECEIVERACCOUNT";
            public const string STORENAME = "STORENAME";
            public const string PARA = "PARA";
            public const string IPCTRANCODEDEST = "IPCTRANCODEDEST";
            public const string STRDESTRESULT = "STRDESTRESULT";
            public const string VALIDAUTHENCODE = "OK";
            public const string AUTHENUSER = "AUTHENUSER";
            public const string ISSCHEDULE = "ISSCHEDULE";
            public const string SMSOTP = "SMSOTP";
            public const string STATUSACCT = "STATUSACCT";
            public const string ACCTTYPE = "ACCTTYPE";
            public const string CURRENCY = "CURRENCY";
            public const string BRANCHID = "BRANCHID";
            public const string CUSTCODE = "CUSTCODE";
            public const string CUSTTYPE = "CUSTTYPE";
            public const string FEE = "FEE";
            public const string EMAIL = "EMAIL";
            public const string SCHEDULEID = "SCHEDULEID";
            //add by vu tran
            public const string TRANCODETORIGHT = "TRANCODETORIGHT";
            public const string TRANCODEMORE = "TRANCODEMORE";
            public const string DATASET = "DATASET";
            public const string STATUS = "STATUS";
            public const string PATH = "PATH";
            public const string DATATABLE = "DATATABLE";
            public const string TELCO = "TELCO";
            public const string RPTPATH = "RPTPATH";
            public const string BUYRATE = "BUYRATE";
            public const string DEBITBRACHID = "DEBITBRACHID";
            public const string DEBITBALANCE = "DEBITBALANCE";
            public const string CENACCTNO = "CENACCTNO";
            public const string SUBACCTNO = "SUBACCTNO";
            public const string SURLEVEL = "SURLEVEL";
            public const string SHRLEVEL = "SHRLEVEL";
            public const string SOURCEIBVALUE = "IB";
            public const string AVAILABLEBAL = "AVAILABLEBALANCE";
            public const string SEDFEE = "feeSenderAmt";
            public const string REVFEE = "feeReceiverAmt";
            public const string SENDERNAME = "SENDERNAME";
            public const string RECEIVERNAME = "RECEIVERNAME";
            public const string CREDITBRACHID = "CREDITBRACHID";
            public const string CREDITBALANCE = "CREDITBALANCE";
            public const string FACCTNO = "FACCTNO";
            public const string BATCHID = "BATCHID";
            public const string TACCTNO = "TACCTNO";
            public const string FEESEN = "FEESEN";
            public const string FEEREC = "FEEREC";
            public const string DESC = "DESC";
            public const string BATCHDATABASE64 = "BATCHDATABASE64";
            public const string ONETIME = "ONETIME";
            public const string REPEATEDLY = "REPEATEDLY";
            public const string BATCHSCHEDULETRANCODE = "IB000498";
            public const string FILENAME = "FILENAME";
            public const string SERVERTIME = "SERVERTIME";
            public const string IPCSCHEDULESINSERT = "IPCSCHEDULESINSERT";
            public const string IPCSCHEDULEDAYINSERT = "IPCSCHEDULEDAYINSERT";
            public const string IPCSCHEDULEREPEATINSERT = "IPCSCHEDULEREPEATINSERT";
            public const string IPCSCHEDULEDETAILINSERT = "IPCSCHEDULEDETAILINSERT";
            public const string APPRSTS = "APPRSTS";
            public const string DESTIDVALUE = "PNB";
            public const string BATCHRESULT = "BATCHRESULT";
            //BATCH EMAIL
            public const string SENDER_ACCOUNT = "Sender account";
            public const string RECEIVER_ACCOUNT = "Receiver account";
            public const string RECEIVER_NAME = "Receiver name";
            public const string RECEIVER_BRANCH = "Receiver branch";
            public const string PAYMENT_CONTENT = "Payment content";
            public const string ACCOUNT_BALANCE = "Account balance";
            public const string RESULT = "Result";
            public const string TRANSACTION_NO = "Transaction no";
            public const string TRANSACTION_AMOUNT = "Amount";
            public const string HUBERRORCODEPREFIX = "SBC#";

            public const string USSDCode = "USSDCode";
            public const string RunUSSD = "RunUSSD";
            //chi add 27.11.2015
            public const string YES = "Y";
            public const string DAILY = "DAILY";
            public const string WEEKLY = "WEEKLY";
            public const string MONTHLY = "MONTHLY";
            //2017.04.14 minh add this for MB,SMS policy
            public const string PASSWORD = "PASSWORD";
            public const string PINCODE = "PINCODE";
            public const string MB = "MB";
            public const string AM = "AM";
            public const string NEWPASS = "NEWPASS";
            //public const string TYPEPASS = "TYPEPASS";
            public const string LGAUTHENTYPE = "LGAUTHENTYPE";

            //2019.12.17 LanNTH check policy to change password when login
            public const string ISPASSPOLICY = "ISPASSPOLICY";
            public const string ERRPOLICY = "ERRPOLICY";
            public const string POLICY = "POLICY";


            //CREDIT CARD
            public const string CARDNO = "CARDNO";
            public const string cardNo = "cardNo";
            public const string CREDITLIMIT = "CREDITLIMIT";
            public const string OUTSTANDINGAMT = "OUTSTANDINGAMT";
            public const string AVAILIMIT = "AVAILIMIT";
            public const string SERVICEID = "SERVICEID";

            //HTTP CLIENT
            public const string HTTPHEADER = "HTTPHEADER";
            public const string HTTPBODY = "HTTPBODY";
            public const string POST = "POST";
            public const string GET = "GET";
            public const string HTTPFORMVALUE = "application/x-www-form-urlencoded";
            public const string HTTPJSONVALUE = "application/json";
            public const string HTTPXMLVALUE = "application/xml";
            public const string HTTPTEXTPLAIN = "text/plain";
            public const string ERRRESPONSECODE = "ERRRESPONSECODE";
            public const string ERRRESPONSEDESC = "ERRRESPONSEDESC";

            //Push notification
            public const string TITLE = "TITLE";
            public const string BODY = "BODY";
            public const string DETAIL = "DETAIL";
            public const string IMGURL = "IMGURL";
            public const string LINK = "LINK";
            public const string DATA = "DATA";
            public const string TRANCODE = "TRANCODE";
            public const string TYPEID = "TYPEID";
            public const string AND = "AND";
            public const string IOS = "IOS";
            public const string PUSHID = "PUSHID";
            public const string ALL = "ALL";
            public const string SENDERID = "SENDERID";
            public const string AUTHENTOKEN = "AUTHENTOKEN";
            public const string CERTPASS = "CERTPASS";
            public const string ENV = "ENV";
            public const string DEV = "DEV";
            public const string ID = "ID";
            public const string Y = "Y";
            public const string CERTFILE = "CERTFILE";
            public const string MAPSTATUS = "MAPSTATUS";
            public const string GROUP = "GROUP";
            public const string ACTION = "ACTION";
            public const string DATEINSERT = "DATEINSERT";

            //MB
            public const string UUID = "UUID";
            public const string DEVICEID = "DEVICEID";
            public const string BIOMETRICTOKEN = "BIOMETRICTOKEN";
            public const string ENCODEDTOKEN = "ENCODEDTOKEN";
            public const string APPVERSION = "APPVERSION";
            public const string DBVERSION = "DBVERSION";
            public const string DEVICETYPE = "DEVICETYPE";

            //Wallet

            public const string WALLETACCTTYE = "WL";
            public const string WLB = "WLB";
            public const string WLM = "WLM";

            public const string CLUSTERID = "CLUSTERID";
            public const string MODULEID = "MODULEID";
            public const string ISRUN = "ISRUN";
            public const string IDLETIME = "IDLETIME";
            public const string REMOTEURL = "REMOTEURL";
            public const string RUNNINGCLUSTERID = "RUNNINGCLUSTERID";


            public const string INTERVAL = "INTERVAL";
            public const string JOBSID = "JOBSID";
            public const string METHODNAME = "METHODNAME";
            public const string USERCREATE = "USERCREATE";
            public const string LANG = "LANG";

            public static object TARGETTRANCODE { get; set; }
        }

        public static class CONNTYPE
        {
            public const string SQL = "SQL";
            public const string ORACLE = "ORA";
        }

        #region End Of Day Proccess
        public static class BCHTYPE
        {
            public const string DLL = "DLL";
            public const string COMPONENT = "COMPONENT";
            public const string STORE = "STORE";
        }
        public static class PROCESSSTATUS
        {
            public const string NONE = "0";
            public const string FINISH = "1";
            public const string ERROR = "2";
            public const string RUNNING = "3";
        }
        #endregion
        #endregion

        #region Public Function
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
        public static string ConvertRandomString()
        {
            string output = "";
            try
            {
                Random generator = new Random();
                DateTime dateTime = DateTime.Now;
                string randomNum = generator.Next(0, 1000000000).ToString("D5");
                output = dateTime.ToString("yyyyMMddHHmmssfff")+ randomNum;
            }
            catch { }


            return output;
        }
        public static string HexEncodingToString(byte[] bytes)
        {
            string hexString = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                hexString += bytes[i].ToString("X2");
            }
            return hexString;
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

        public static void ExecuteCommandAsync(string command)
        {
            try
            {
                //Asynchronously start the Thread to process the Execute command request.
                Thread objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));
                //Make the thread as background thread.
                objThread.IsBackground = true;
                //Set the Priority of the thread.
                objThread.Priority = ThreadPriority.AboveNormal;
                //Start the thread.
                objThread.Start(command);
            }
            catch (ThreadStartException objException)
            {
                // Log the exception
            }
            catch (ThreadAbortException objException)
            {
                // Log the exception
            }
            catch (Exception objException)
            {
                // Log the exception
            }
        }

        public static void ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                Console.WriteLine(result);
            }
            catch (Exception objException)
            {
                // Log the exception
            }
        }

        public static string RetrieveProperty(string Group, string Property)
        {
            object app = null;
            try
            {
                System.Type type = System.Type.GetTypeFromProgID("BDSVar.Sysvar", Common.SYSVAR["SMBBDS"].ToString(), true);
                app = System.Activator.CreateInstance(type);
                object[] args = { Group, Property };
                object objResult = type.InvokeMember("RetrieveProperty", BindingFlags.InvokeMethod, null, app, args);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                return objResult.ToString();
            }
            catch (Exception ex)
            {
                if (app != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return "";
            }
        }

        public static string RetrievePropertyHost(string Group, string Property)
        {
            object app = null;
            try
            {
                System.Type type = System.Type.GetTypeFromProgID("SBSystem.Sysvar", Common.SYSVAR["SMBHOST"].ToString(), true);
                app = System.Activator.CreateInstance(type);
                object[] args = { Group, Property };
                object objResult = type.InvokeMember("RetrieveProperty", BindingFlags.InvokeMethod, null, app, args);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                return objResult.ToString();
            }
            catch (Exception ex)
            {
                if (app != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return "";
            }
        }

        public static object RetrieveSMB(string Function)
        {
            object app = null;
            try
            {
                System.Type type = System.Type.GetTypeFromProgID("ActiveTeller.Batch", Common.SYSVAR["SMBHOST"].ToString(), true);
                app = System.Activator.CreateInstance(type);
                //object[] args = { Group, Property };
                object objResult = type.InvokeMember(Function.ToString(), BindingFlags.InvokeMethod, null, app, null);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                return objResult;
            }
            catch (Exception ex)
            {
                if (app != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        public static long GetIPCTransID()
        {
            //lock (LockerTransID)
            //    return IPCTransID++;
            //DoanLT temp edit 
            lock (LockerTransID)
            {
                DataTable rs = new DataTable();
                DBConnection.Connection dbObj = new DBConnection.Connection();
                rs = dbObj.FillDataTable(ConStr, "IPC_GETTRANSID_TEMP");
                return long.Parse(rs.Rows[0][0].ToString());
            }
        }

        public static void IncreaseTranProcessingCount()
        {
            lock (LockerTranProcessingCount)
                TranProcessingCount++;
        }

        public static void DecreaseTranProcessingCount()
        {
            lock (LockerTranProcessingCount)
                TranProcessingCount--;
        }

        public static string GetHostIPAddress()
        {
            string ipAddress = "";
            try
            {
                // Then using host name, get the IP address list..
                IPHostEntry ipEntry = Dns.GetHostByName(Dns.GetHostName());
                IPAddress[] addr = ipEntry.AddressList;
                for (int i = 0; i < addr.Length; i++)
                {
                    ipAddress += addr[i].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ipAddress;
        }

        public static bool ExecCom(string AssemblyFile, string AssemblyTitle, string Method, string parmList, TransactionInfo tran)
        {
            object result;
            try
            {
                Assembly assembly;
                //vutt 08032016 load dll from base dir
                try
                {
                    assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + AssemblyFile);
                }
                catch
                {
                    assembly = Assembly.LoadFrom(AssemblyFile);
                }

                Type type = assembly.GetType(AssemblyTitle);
                object instance = Activator.CreateInstance(type);
                if (parmList == "")
                {
                    result = type.InvokeMember(Method, System.Reflection.BindingFlags.InvokeMethod, null, instance, new object[] { tran });
                }
                else
                {
                    result = type.InvokeMember(Method, System.Reflection.BindingFlags.InvokeMethod, null, instance, new object[] { tran, parmList });
                }
                return (bool)result;
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }
        public static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return ".png";
                case "/9J/4":
                    return ".jpg";
                case "AAAAF":
                    return ".mp4";
                case "JVBER":
                    return ".pdf";
                case "AAABA":
                    return ".ico";
                case "UMFYI":
                    return ".rar";
                case "E1XYD":
                    return ".rtf";
                case "U1PKC":
                    return ".txt";
                case "MQOWM":
                case "77U/M":
                    return ".srt";
                default:
                    return string.Empty;
            }
        }
        public static object ExecRemoteMethod(string assemblyName, string assemblyType, string assemblyMethod, string tcpurl, object[] arg, string[] argName)
        {
            try
            {
                object result;
                Assembly assembly;
                //vutt 08032016 load dll from base dir
                try
                {
                    assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + assemblyName);
                }
                catch
                {
                    assembly = Assembly.LoadFrom(assemblyName);
                }
                Type type = assembly.GetType(assemblyType);
                object tran;
                if (tcpurl == "" || tcpurl == null)
                    tran = Activator.CreateInstance(type);
                else
                    tran = Activator.GetObject(type, tcpurl);

                result = type.InvokeMember(assemblyMethod, System.Reflection.BindingFlags.InvokeMethod,
                    null, tran, arg, null, System.Globalization.CultureInfo.CurrentCulture, argName);
                return result;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        public static object ExecRemoteMethod(string assemblyName, string assemblyType, string assemblyMethod, string tcpurl, object[] arg)
        {
            try
            {
                object result;
                Assembly assembly;
                //vutt 08032016 load dll from base dir
                try
                {
                    assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + assemblyName);
                }
                catch
                {
                    assembly = Assembly.LoadFrom(assemblyName);
                }
                Type type = assembly.GetType(assemblyType);
                object tran;
                if (tcpurl == "" || tcpurl == null)
                    tran = Activator.CreateInstance(type);
                else
                    tran = Activator.GetObject(type, tcpurl);
                result = type.InvokeMember(assemblyMethod, System.Reflection.BindingFlags.InvokeMethod,
                    null, tran, arg);
                return result;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        public static string ReplaceCharSpec(string strValue, string characterSpec)
        {
            string result = strValue;
            try
            {
                string[] parmlist = characterSpec.Split('|');

                result = result.Replace("|", "");

                for (int i = 0; i < parmlist.Length; i++)
                {
                    result = result.Replace(parmlist[i], "");
                }

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return "";
            }
            return result;
        }
        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"

        };

        public static string RemoveSign(string data)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    data = data.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }
            return data;
        }
        #endregion

        public static class FILELOGTYPE
        {
            public const string LOGDOSTOREINFO = "LogDoStoreInfo";
            public const string LOGMSGINOUT = "MsgInOutLog";
            public const string LOGMSGSYSTEM = "LogMessage";
            public const string LOGXMLTOFIELD = "XmlToFieldLog";
            public const string LOGFILEPATH = "LogFilePath";
        }

        public const string LIMITDAYLOGMSG = "LimitDayLogMsg";

        public static string EncryptPassword(String strData)
        {
            string strResult;		//Return Result

            //1. String Length cannot exceed 90Kb. Otherwise, buffer will overflow. See point 3 for reasons
            if (strData.Length > 92160)
            {
                strResult = "Error. Data String too large. Keep within 90Kb.";
                return strResult;
            }

            //2. Generate the Keys
            if (!InitKey(strThekey))
            {
                strResult = "Error. Fail to generate key for encryption";
                return strResult;
            }

            //3. Prepare the String
            //	The first 5 character of the string is formatted to store the actual length of the data.
            //	This is the simplest way to remember to original length of the data, without resorting to complicated computations.
            //	If anyone figure a good way to 'remember' the original length to facilite the decryption without having to use additional function parameters, pls let me know.
            strData = String.Format("{0,5:00000}" + strData, strData.Length);


            //4. Encrypt the Data
            byte[] rbData = new byte[strData.Length];
            ASCIIEncoding aEnc = new ASCIIEncoding();
            aEnc.GetBytes(strData, 0, strData.Length, rbData, 0);

            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();

            ICryptoTransform desEncrypt = descsp.CreateEncryptor(m_Key, m_IV);


            //5. Perpare the streams:
            //	mOut is the output stream. 
            //	mStream is the input stream.
            //	cs is the transformation stream.
            MemoryStream mStream = new MemoryStream(rbData);
            CryptoStream cs = new CryptoStream(mStream, desEncrypt, CryptoStreamMode.Read);
            MemoryStream mOut = new MemoryStream();

            //6. Start performing the encryption
            int bytesRead;
            byte[] output = new byte[1024];
            do
            {
                bytesRead = cs.Read(output, 0, 1024);
                if (bytesRead != 0)
                    mOut.Write(output, 0, bytesRead);
            } while (bytesRead > 0);

            //7. Returns the encrypted result after it is base64 encoded
            //	In this case, the actual result is converted to base64 so that it can be transported over the HTTP protocol without deformation.
            if (mOut.Length == 0)
                strResult = "";
            else
                strResult = Convert.ToBase64String(mOut.GetBuffer(), 0, (int)mOut.Length);

            return strResult;
        }
        public static string Decryptpassword(String strData)
        {
            if (string.IsNullOrEmpty(strData))
                return "";

            string strResult;

            //1. Generate the Key used for decrypting
            if (!InitKey(strThekey))
            {
                strResult = "Error. Fail to generate key for decryption";
                return strResult;
            }

            //2. Initialize the service provider
            int nReturn = 0;
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
            ICryptoTransform desDecrypt = descsp.CreateDecryptor(m_Key, m_IV);

            //3. Prepare the streams:
            //	mOut is the output stream. 
            //	cs is the transformation stream.
            MemoryStream mOut = new MemoryStream();
            CryptoStream cs = new CryptoStream(mOut, desDecrypt, CryptoStreamMode.Write);

            //4. Remember to revert the base64 encoding into a byte array to restore the original encrypted data stream
            byte[] bPlain = new byte[strData.Length];
            try
            {
                bPlain = Convert.FromBase64CharArray(strData.ToCharArray(), 0, strData.Length);
            }
            catch (Exception)
            {
                strResult = "Error. Input Data is not base64 encoded.";
                return strResult;
            }

            long lRead = 0;
            long lTotal = strData.Length;

            try
            {
                //5. Perform the actual decryption
                while (lTotal >= lRead)
                {
                    cs.Write(bPlain, 0, (int)bPlain.Length);
                    //descsp.BlockSize=64
                    lRead = mOut.Length + Convert.ToUInt32(((bPlain.Length / descsp.BlockSize) * descsp.BlockSize));
                };

                ASCIIEncoding aEnc = new ASCIIEncoding();
                strResult = aEnc.GetString(mOut.GetBuffer(), 0, (int)mOut.Length);

                //6. Trim the string to return only the meaningful data
                //	Remember that in the encrypt function, the first 5 character holds the length of the actual data
                //	This is the simplest way to remember to original length of the data, without resorting to complicated computations.
                String strLen = strResult.Substring(0, 5);
                int nLen = Convert.ToInt32(strLen);
                strResult = strResult.Substring(5, nLen);
                nReturn = (int)mOut.Length;

                return strResult;
            }
            catch (Exception)
            {
                strResult = "Error. Decryption Failed. Possibly due to incorrect Key or corrputed data";
                return strResult;
            }
        }

        public static string GenCRC16(string QRContent)
        {
            byte[] data = Encoding.ASCII.GetBytes(QRContent);
            ushort crc = 0xFFFF;
            for (int i = 0; i < data.Length; i++)
            {
                crc ^= (ushort)(data[i] << 8);
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x8000) > 0)
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    else
                        crc <<= 1;
                }
            }
            return crc.ToString("X4");
        }
        public static string DataTable2Base64Excel(DataTable dt)
        {
            try
            {
                ExcelPackage pck = new ExcelPackage();
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].LoadFromDataTable(dt, true);

                var stream = new MemoryStream(pck.GetAsByteArray());

                byte[] br = stream.ToArray();

                return Convert.ToBase64String(br);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return string.Empty;
            }
        }

        public static DataTable Base64Excel2Datatable(string base64string)
        {
            try
            {
                byte[] arrBase64 = Convert.FromBase64String(base64string);

                MemoryStream ms = new MemoryStream();
                ms.Write(arrBase64, 0, arrBase64.Length);

                var pck = new ExcelPackage();
                pck.Load(ms);
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                bool hasHeader = true;
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    var row = tbl.NewRow();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                    tbl.Rows.Add(row);
                }
                pck.Dispose();
                return tbl;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new DataTable();
            }
        }
        #region amount to words
        public static String AmountToWords(String numb)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = ("");
            double t;
            try
            {
                if (Double.TryParse(numb, out t))
                {
                    int decimalPlace = numb.IndexOf(".");
                    if (decimalPlace > 0)
                    {
                        wholeNo = numb.Substring(0, decimalPlace);
                        points = numb.Substring(decimalPlace + 1);
                        if (Convert.ToInt32(points) > 0)
                        {
                            andStr = ("point");// just to separate whole numbers from points/Rupees
                            pointStr = points;
                        }
                    }
                    val = String.Format("{0} {1} {2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, translateWholeNumber(pointStr).Trim(), endStr);
                }
            }
            catch
            {
                ;
            }
            return val;
        }

        private static String translateWholeNumber(String number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX
                bool isDone = false;//test if already translated
                double dblAmt = (Convert.ToDouble(number));
                //if ((dblAmt > 0) && number.StartsWith("0"))

                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric
                    beginsZero = number.StartsWith("0");
                    int numDigits = number.Length;
                    int pos = 0;//store digit grouping
                    String place = "";//digit grouping name:hundres,thousand,etc...
                    switch (numDigits)
                    {
                        case 1://ones' range
                            word = ones(number);
                            isDone = true;
                            break;
                        case 2://tens' range
                            word = tens(number);
                            isDone = true;
                            break;
                        case 3://hundreds' range
                            pos = (numDigits % 3) + 1;
                            place = " hundred ";
                            break;
                        case 4://thousands' range
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " thousand ";
                            break;
                        case 7://millions' range
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " million ";
                            break;
                        case 10://Billions's range
                            pos = (numDigits % 10) + 1;
                            place = " billion ";
                            break;
                        //add extra case options for anything above Billion...
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)
                        word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                        //check for trailing zeros
                        if (beginsZero) word = " and " + word.Trim();
                    }
                    //ignore digit grouping names
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch
            {
                ;
            }
            return word.Trim();
        }

        private static String tens(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = null;
            switch (digt)
            {
                case 10:
                    name = "ten";
                    break;
                case 11:
                    name = "eleven";
                    break;
                case 12:
                    name = "twelve";
                    break;
                case 13:
                    name = "thirteen";
                    break;
                case 14:
                    name = "fourteen";
                    break;
                case 15:
                    name = "fifteen";
                    break;
                case 16:
                    name = "sixteen";
                    break;
                case 17:
                    name = "seventeen";
                    break;
                case 18:
                    name = "eighteen";
                    break;
                case 19:
                    name = "nineteen";
                    break;
                case 20:
                    name = "twenty";
                    break;
                case 30:
                    name = "thirty";
                    break;
                case 40:
                    name = "fourty";
                    break;
                case 50:
                    name = "fifty";
                    break;
                case 60:
                    name = "sixty";
                    break;
                case 70:
                    name = "seventy";
                    break;
                case 80:
                    name = "eighty";
                    break;
                case 90:
                    name = "ninety";
                    break;
                default:
                    if (digt > 0)
                    {
                        name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
                    }
                    break;
            }
            return name;
        }

        private static String ones(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = "";
            switch (digt)
            {
                case 1:
                    name = "one";
                    break;
                case 2:
                    name = "two";
                    break;
                case 3:
                    name = "three";
                    break;
                case 4:
                    name = "four";
                    break;
                case 5:
                    name = "five";
                    break;
                case 6:
                    name = "six";
                    break;
                case 7:
                    name = "seven";
                    break;
                case 8:
                    name = "eight";
                    break;
                case 9:
                    name = "nine";
                    break;
            }
            return name;
        }
        #endregion

        /// <summary>
        /// Performs querystring validation
        /// </summary>
        /// <returns>Validate for potential SQL and XSS injection</returns>
        public static string KillSqlInjection(string TexttoValidate)
        {
            string TextVal;

            TextVal = TexttoValidate;
            if (String.IsNullOrEmpty(TextVal))
            {
                return TextVal;
            }

            //Build an array of characters that need to be filter.
            string[] strDirtyQueryString = { "xp_", "$", "#", "%", "*", "^", "&", "!", ";", "--", "<", ">", "script ", "iframe ", "delete ", "drop ", "exec ", "execute ", "'", "\"",
                ";--","/*","*/","create ","decalre ","insert ","kill ","select ","update " };


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
        public static StringTemplate GetEmailTemplate(string templateFileName)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "MailTemplate";
                StringTemplateGroup group = new StringTemplateGroup("SmartPortalST", path);
                StringTemplate query = group.GetInstanceOf(string.Format("{0}", templateFileName));
                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void SendMailMessageAsync(string displayName, string to, string subject, string body, string attachment = "", string attachmentFileName = "")
        {
            ThreadPool.QueueUserWorkItem(delegate { SendMail(displayName, to, subject, body, attachment, attachmentFileName); });
        }

        public static bool SendMail(string displayName, string to, string subject, string body, string attachment = "", string attachmentFileName = "")
        {
            // System.Web.Mail.SmtpMail.SmtpServer is obsolete in 2.0
            // System.Net.Mail.SmtpClient is the alternate class for this in 2.0
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            DBConnection.Connection con = new DBConnection.Connection();
            //smtpClient.EnableSsl = true;
            DataTable SM = con.FillDataTable(Common.ConStr, "Settings_LoadPortalSettings");

            smtpClient.Host = SM.Rows[0]["SMTPServer"].ToString();
            smtpClient.Port = int.Parse(SM.Rows[0]["SMTPPort"].ToString());
            smtpClient.EnableSsl = bool.Parse(SM.Rows[0]["SMTPSSL"].ToString());
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);// minh add dongf nayf chi o may test, phai remove o may live
            smtpClient.Credentials = new System.Net.NetworkCredential(SM.Rows[0]["SMTPUserName"].ToString(), SM.Rows[0]["SMTPPassword"].ToString());

            message.DeliveryNotificationOptions =
            DeliveryNotificationOptions.OnFailure |
                   DeliveryNotificationOptions.Delay;
            //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["emaildelivery"].ToString()))
            //{
            //    message.Headers.Add("Disposition-Notification-To", ConfigurationManager.AppSettings["emaildelivery"].ToString());
            //}

            if (!string.IsNullOrEmpty(SM.Rows[0]["EMAILDELIVERY"].ToString()))
            {
                message.Headers.Add("Disposition-Notification-To", SM.Rows[0]["EMAILDELIVERY"].ToString());
            }
            else if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["emaildelivery"].ToString()))
            {
                message.Headers.Add("Disposition-Notification-To", ConfigurationManager.AppSettings["emaildelivery"].ToString());
            }

            string fromemail = SM.Rows[0]["SMTPUserName"].ToString();
            string subj = displayName + " - " + subject;
            try
            {
                
                message.From = new MailAddress(fromemail, displayName);
                message.To.Add(to);
                
                message.Subject = subj;

                message.IsBodyHtml = true;

                // Message body content
                message.Body = body;
                if (!attachment.Equals("") && !attachmentFileName.Equals(""))
                {
                    MemoryStream pdfStream = new MemoryStream();
                    var document = new Document(PageSize.A4);
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, pdfStream);

                    var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                    document.Open();
                    htmlWorker.StartDocument();
                    htmlWorker.Parse(new StringReader(attachment));
                    htmlWorker.Close();
                    document.Close();

                    var type = new ContentType();
                    type.MediaType = MediaTypeNames.Application.Pdf;
                    type.Name = attachmentFileName + ".pdf";
                    pdfStream = new MemoryStream(pdfStream.ToArray());
                    message.Attachments.Add(new Attachment(pdfStream, type));
                }
                try
                {
                    smtpClient.Send(message);
                    //try
                    //{
                    //    LogEmailOut(fromemail, displayName, to, "", subj, body, attachment, "Y", "", "");
                    //}
                    //catch (Exception ex)
                    //{
                    //    ProcessLog.LogInformation($"LOG TO EMAILOUTL: {ex.Message}");
                    //}
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {
                            ProcessLog.LogInformation(string.Format("MailboxBusy - retrying in 1 seconds.Email: {0}", to));
                            System.Threading.Thread.Sleep(2000);
                            smtpClient.Send(message);
                        }
                        else
                        {
                            ProcessLog.LogInformation(string.Format("Failed to deliver message to {0}", ex.InnerExceptions[i].FailedRecipient));
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ProcessLog.LogInformation(string.Format("Exception {0} with {1}", ex.ToString(), to));
                //try
                //{
                //    LogEmailOut(fromemail, displayName, to, "", subj, body, attachment, "F", "9999", ex.Message);
                //}
                //catch (Exception exx)
                //{
                //    ProcessLog.LogInformation($"LOG TO EMAILOUTL: {exx.Message}");
                //}
                return false;
            }
        }

        public static void LogEmailOut(string from, string display, string to, string cc, string subject, string body, string attachment, string status, string errorcode, string errordesc, int piority = 0)
        {
            try
            {
                Connection con = new Connection();
                con.ExecuteNonquery(Common.ConStr, "SEMS_EMAILOUT_INSERT", -1, from, display, to, cc, subject, body, attachment, errorcode, errordesc, status, piority);
            }
            catch(Exception ex)
            {
                ProcessLog.LogInformation(string.Format("Insert EMAIL OUT Error: Exception {0} with {1}", ex.ToString(), to));
            }
        }

        static private bool InitKey(String strThekey)
        {
            try
            {
                // Convert Key to byte array
                byte[] bp = new byte[strThekey.Length];
                ASCIIEncoding aEnc = new ASCIIEncoding();
                aEnc.GetBytes(strThekey, 0, strThekey.Length, bp, 0);

                //Hash the key using SHA1
                SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                byte[] bpHash = sha.ComputeHash(bp);

                int i;
                // use the low 64-bits for the key value
                for (i = 0; i < 8; i++)
                    m_Key[i] = bpHash[i];

                for (i = 8; i < 16; i++)
                    m_IV[i - 8] = bpHash[i];

                return true;
            }
            catch (Exception)
            {
                //Error Performing Operations
                return false;
            }
        }
        #region sms gsm load balancing
        //load balancing gsm modem
        public static int SMSSequence;
        #endregion

        #region encrypt/decrypt AES256

        public static String Encrypt(String plainText, String key, string iv)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(key, iv)));
        }

        /// <summary>
        /// Decrypts a base64 encoded string using the given key (AES 128bit key and a Chain Block Cipher)
        /// </summary>
        /// <param name="encryptedText">Base64 Encoded String</param>
        /// <param name="key">Secret Key</param>
        /// <returns>Decrypted String</returns>
        public static String Decrypt(String encryptedText, String key, String iv)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            return Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(key, iv)));
        }
        public static RijndaelManaged GetRijndaelManaged(String secretKey, string IV)
        {
            var keyBytes = new byte[16];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            var IVBytes = Encoding.UTF8.GetBytes(IV);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = IVBytes
            };
        }
        public static byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public static byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        #endregion

        #region encrypt BASE64

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion

        public static string HashTable2String(Hashtable objInput)
        {
            try
            {
                string result = string.Empty;
                foreach (DictionaryEntry de in objInput)
                {
                    result = result + "$" + de.Key + "#" + de.Value;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
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

        public static Hashtable String2HashTable(string objInput)
        {
            Hashtable result = new Hashtable();
            string[] listKey = objInput.Split('$');
            for (int i = 0; i < listKey.Length; i++)
            {
                string[] temp = listKey[i].Split('#');
                if (temp.Length > 1)
                {
                    result.Add(temp[0], temp[1]);
                }
            }
            return result;
        }
        public static string EncodeBioMetricToken(string userID, string deviceID, string biometricToken)
        {
            userID = userID.Trim().ToUpper();
            deviceID = deviceID.Trim().ToUpper();
            biometricToken = biometricToken.Trim().ToUpper();
            return O9Encryptpass.sha256($"J{userID.Substring(0, 5)}I{deviceID.Substring(0, 5)}T{biometricToken}S").Substring(0, 36).ToUpper();
        }
        public static string FormatMoneyInput(string m, string CCYID)
        {
            try
            {
                CultureInfo dk = new CultureInfo("en-US");

                //vutran 03022015 fix error when convert myanmar money
                if (Thread.CurrentThread.CurrentCulture.Name == "mk")
                {
                    m = m.Replace(".", ",");
                }

                string m1 = m;

                if (m == "" || m == "0" || m == "0,00" || m == "0.00")
                {
                    m1 = "0";
                }
                else
                {
                    switch (CCYID)
                    {
                        case ("MMK"):
                            //return double.Parse(m).ToString(",", dk);
                            //return double.Parse(m,dk).ToString("#,##0.##");
                            //m1 = double.Parse(m).ToString("N00", dk); VND
                            m1 = double.Parse(m).ToString("N02", dk);
                            //m1 = double.Parse(m).ToString("N00");
                            //return string.Format(dk, "{0:0,0}", double.Parse(m,dk));
                            //return Double.Parse(m,dk.NumberFormat).ToString("N00", dk.NumberFormat);
                            //return String.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C}",m);
                            break;
                        default:
                            //return double.Parse(m).ToString(",", dk);
                            //return double.Parse(m,dk).ToString("#,##0.##");
                            m1 = double.Parse(m).ToString("N02", dk);
                            //m1 = double.Parse(m).ToString("N02");
                            //return string.Format(dk, "{0:0,0}", double.Parse(m,dk));
                            //return Double.Parse(m,dk.NumberFormat).ToString("N00", dk.NumberFormat);
                            //return String.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C}",m);
                            break;

                    }
                }

                return m1;
            }
            catch (Exception)
            {
                return m;
            }
        }

        #region Card Function
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
        #endregion

        #region Encrypt data MB v2
        static byte[] MB_PP = Encoding.Unicode.GetBytes("V2");
        static byte[] MB_IV = { 2, 4, 6, 8, 1, 1, 2, 2, 9, 9, 1, 7, 8, 9, 1, 9 };
        public static string EncryptMsgMB(string sinput, string key)
        {


            byte[] byteData = Encoding.UTF8.GetBytes(sinput);

            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.FromXmlString(key);

            byte[] bytePass = new byte[(new Random()).Next(8, 32)];
            (new Random()).NextBytes(bytePass);

            byte[] byteDataEn = RijndaelHelper.Encrypt(byteData, bytePass);
            byte[] bytePassRsa = RSAHelper.Encrypt(bytePass, RSA.ExportParameters(false));
            byte[] byteLen = BitConverter.GetBytes(bytePassRsa.Length);

            byte[] byteSum = byteLen.Concat(bytePassRsa).Concat(byteDataEn).ToArray();

            string strResult = Convert.ToBase64String(byteSum);

            return strResult;
        }
        private static RSACryptoServiceProvider _serverRsa = null;
        public static string[] DecryptMsgMB(string data)
        {
            byte[] byteDataSumEn = Convert.FromBase64String(data);

            int aesLen = BitConverter.ToInt32(byteDataSumEn.Take(4).ToArray(), 0);
            byte[] byteDataKeyEn = byteDataSumEn.Skip(4).Take(aesLen).ToArray();
            byte[] byteDataEn = byteDataSumEn.Skip(4 + aesLen).ToArray();

            RSACryptoServiceProvider _serverRsa = new RSACryptoServiceProvider();
            _serverRsa.FromXmlString(RSAConfig.strPivate);

            //if (_serverRsa == null)
            //{
            //    lock (serverRsaLock)
            //    {
            //        _serverRsa = new RSACryptoServiceProvider();
            //        _serverRsa.FromXmlString(RSAConfig.strPivate);
            //    }
            //}
            ProcessLog.LogInformation($"Info of Decrypt msg MBService - [byteDataKeyEn].length = : {byteDataKeyEn.Length}");

            byte[] AesKey = RSAHelper.Decrypt(byteDataKeyEn, _serverRsa.ExportParameters(true));

            ProcessLog.LogInformation($"Info of Decrypt msg MBService - [AesKey].length = : {AesKey.Length}");

            byte[] byteDataAes = RijndaelHelper.Decrypt(byteDataEn, AesKey);

            int rsaLen = BitConverter.ToInt32(byteDataAes.Take(4).ToArray(), 0);
            byte[] byteRsaKey = byteDataAes.Skip(4).Take(rsaLen).ToArray();
            byte[] byteData = byteDataAes.Skip(4 + rsaLen).ToArray();

            string rsaKey = Encoding.UTF8.GetString(byteRsaKey);
            string strdata = Encoding.UTF8.GetString(byteData);

            return new[] { strdata, rsaKey };
        }
        public static Dictionary<string, string> ParseResponseTextToDic(string text, string tagDelimiter = "#", string fieldDelimiter = "$")
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string[] tags = text.Split(new[] { tagDelimiter }, System.StringSplitOptions.None);
            foreach (string tag in tags)
            {
                string[] keyvalues = tag.Split(new[] { fieldDelimiter }, System.StringSplitOptions.None);
                string key;
                string value;
                if (keyvalues.Length == 2)
                {
                    key = keyvalues[0].Trim();
                    value = keyvalues[1].Trim();
                }
                else
                {
                    key = keyvalues[0];
                    value = "";
                }
                if (result.ContainsKey(key))
                {
                    result[key] = value;
                }
                else
                {
                    result.Add(key, value);
                }
            }
            return result;
        }

        /// <summary>
        /// VuTT split params, will be used to format text
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> GetParams(string input, char pcharstart = '{', char pcharend = '}')
        {
            List<string> param = new List<string>();
            string[] kk = input.Split(pcharstart);
            foreach (string k in kk)
            {
                if (k.Contains(pcharend))
                {
                    param.Add(k.Split(pcharend)[0].Trim());
                }
            }
            return param;
        }

        public static bool CheckCondition(TransactionInfo tran, string condition)
        {
            try
            {
                if (string.IsNullOrEmpty(condition))
                    return true;

                List<string> paraVal = GetParams(condition, '[', ']');
                foreach (string para in paraVal)
                {
                    condition = condition.Replace("[" + para + "]", tran.Data[para].ToString());
                }

                Expression expFormula = new Expression(condition);
                bool exResult = Convert.ToBoolean(expFormula.Evaluate());
                return exResult;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        /// <summary>
        /// VuTT create signature from value
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        public static string CreateHash(string message, string key)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] hash;

            using (var hma = new HMACSHA256(keyByte))
            {
                hash = hma.ComputeHash(messageBytes);
            }
            HMACSHA256 hmacsha = new HMACSHA256(keyByte);

            return ByteToString(hash);
        }

        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("A!9HHhi%XjjYY4YP2@Nob009X"));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }
            public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }
        #endregion

       
        #region IPC Cluster
        public static class MODULEID
        {
            public const string SCHEDULER = "SCHEDULER";
            public const string SMSONEWAY = "SMSONEWAY";
            public const string SMSPROCESSMSGMT = "SMSPROCESSMSGMT";
            public const string AUTOREFUNDCASHCODE = "AUTOREFUNDCASHCODE";
        }
        private static Dictionary<string, bool> dicModuleCluster = new Dictionary<string, bool>();
        public static Dictionary<string, Thread> dicClusterThread = new Dictionary<string, Thread>();
        public static string ActiveClusterURL = string.Empty;
        public static bool CheckCluster(string moduleID)
        {
            if (CLUSTERID.Equals(0))
                return true;

            if (!dicClusterThread.ContainsKey(moduleID))
            {
                dicModuleCluster.Add(moduleID, false);
                Thread thread = new Thread(() => LoopCheckCluster(moduleID));
                thread.Start();
                dicClusterThread.Add(moduleID, thread);
            }
            return dicModuleCluster[moduleID];
        }
        private static void LoopCheckCluster(string moduleID)
        {
            Connection con = new Connection();
            do
            {
                try
                {
                    DataTable dt = con.FillDataTable(ConStr, "IPCCLUSTER_CHECKTORUN", new object[2] { Common.CLUSTERID, moduleID });
                    if (dt.Rows[0]["ISRUN"].Equals("1") && !dicModuleCluster[moduleID])
                    {
                        ProcessLog.LogInformation($"Warning: Cluster {ActiveClusterURL} have been down, ative current cluster as master");
                    }

                    dicModuleCluster[moduleID] = dt.Rows[0][KEYNAME.ISRUN].Equals(1);
                    ActiveClusterURL = dt.Rows[0][KEYNAME.ISRUN].Equals(1) ? "" : dt.Rows[0][KEYNAME.REMOTEURL].ToString().Trim();
                    Thread.Sleep(int.Parse(dt.Rows[0][KEYNAME.IDLETIME].ToString()) * 1000);
                }
                catch (Exception ex)
                {
                    Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                }

            }
            while (Common.ServiceStarted);
        }

        #endregion

        #region email helper
        //VuTT html to pdf
        public static Byte[] HtmlToPdf(String html)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4, 0);
                pdf.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }
        #endregion

        public static JObject NewParse(string value)
        {
            return JObject.Load(new JsonTextReader(new StringReader(value)) { FloatParseHandling = FloatParseHandling.Decimal }, null);
        }
    }
    public class FixedSizedQueue<T>
    {
        public Queue<T> items = new Queue<T>();

        public int Limit { get; set; }
        public void Enqueue(T obj)
        {
            items.Enqueue(obj);
            lock (this)
            {
                while (items.Count > Limit)
                    items.Dequeue();
            }
        }

    }
    public class UploadFileInfo
    {
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FullPath { get; set; }
        public string FileBin { get; set; }
    }


}