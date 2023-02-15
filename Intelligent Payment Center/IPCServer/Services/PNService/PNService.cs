using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Configuration;
using DBConnection;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;
using Utility;
using System.IO;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Messaging;

namespace PnService
{
    public partial class PNService : ServiceBase
    {
        private Thread thread;
        private bool isRunning = true;
        private Connection con = null;

        DataTable dtConfig = null;
        Dictionary<string, string> dicMapError = null;
        X509Certificate2 certApns = null;

        static Dictionary<string, FirebaseApp> dicApp = new Dictionary<string, FirebaseApp>();

        public PNService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ProcessLog.LogInformation("Starting push service");
            StartServer();
            thread = new Thread(new ThreadStart(GetPNMsg));
            thread.Start();
            ProcessLog.LogInformation("Push service started");
        }


        protected override void OnStop()
        {
            StopServer();
        }

        private void GetPNMsg()
        {
            try
            {
                while (isRunning)
                {
                    try
                    {
                        Connection con = new Connection();
                        DataTable pushMessages = con.FillDataTable(Utility.Common.ConStr, "EBA_PN_MESSAGE_SELECT_ALL");
                        foreach (DataRow dr in pushMessages.Rows)
                        {
                            switch (dr[Common.KEYNAME.TYPEID].ToString())
                            {
                                case Common.KEYNAME.AND:
                                    PushFirebase(dr);
                                    break;
                                case Common.KEYNAME.IOS:
                                    PushFirebase(dr);
                                    break;
                                default:
                                    PushFirebase(dr);
                                    break;
                            }
                        }

                        Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["SLEEP_TIME"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                        }
                        catch (Exception eex)
                        {
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private FirebaseApp GetApp(string AppID)
        {
            AppID = Common.KEYNAME.ALL;

            if (dicApp.ContainsKey(AppID))
            {
                return dicApp[AppID];
            }
            else
            {
                string configFile = string.Empty;
                if (dtConfig == null)
                {
                    dtConfig = con.FillDataTable(Utility.Common.ConStr, "EBA_PN_CONFIG_SELECT_ALL");
                }
                DataRow[] drConfig = dtConfig.Select($"SERVICEID = '{AppID}' AND TYPE = '{Common.KEYNAME.ALL}'");
                if (drConfig.Length == 0)
                {
                    drConfig = dtConfig.Select($"SERVICEID = '{Common.KEYNAME.ALL}' AND TYPE = '{Common.KEYNAME.ALL}'");
                }
                if (drConfig.Length.Equals(0))
                {
                    ProcessLog.LogInformation($"Push configuration for {AppID} not found, loading default config");
                    configFile = @"C:\Cert\firebase-adminsdk.json";
                }
                else
                {
                    configFile = drConfig[0][Common.KEYNAME.CERTFILE].ToString().Trim();
                    if (string.IsNullOrEmpty(configFile))
                    {
                        ProcessLog.LogInformation($"Push configuration for {AppID} was empty, loading default config");
                        configFile = @"C:\Cert\firebase-adminsdk.json";
                    }
                }

                var fa = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(configFile)
                });



                dicApp.Add(AppID, fa);
                return fa;
            }
        }

        private void PushFirebase(DataRow dr)
        {
            Connection con = new Connection();

            Dictionary<string, string> dicMsg = new Dictionary<string, string>();
            dicMsg.Add("title", dr[Common.KEYNAME.TITLE].ToString());
            dicMsg.Add("message", dr[Common.KEYNAME.TITLE].ToString());
            dicMsg.Add("body", dr[Common.KEYNAME.BODY].ToString());
            dicMsg.Add("subtitle", "");
            dicMsg.Add("imgurl", dr[Common.KEYNAME.IMGURL].ToString());
            dicMsg.Add("link", dr[Common.KEYNAME.LINK].ToString());
            dicMsg.Add("trancode", dr[Common.KEYNAME.TRANCODE].ToString());
            dicMsg.Add("group", dr[Common.KEYNAME.GROUP].ToString());
            dicMsg.Add("datetime", ((DateTime)dr[Common.KEYNAME.DATEINSERT]).ToString("dd/MM/yyyy HH:mm:ss"));
            dicMsg.Add("id", dr[Common.KEYNAME.ID].ToString());
            dicMsg.Add("data", dr[Common.KEYNAME.DATA].ToString());
            dicMsg.Add("detail", dr[Common.KEYNAME.DETAIL].ToString());
            dicMsg.Add("action", dr[Common.KEYNAME.ACTION].ToString());

            List<string> regIDs = new List<string>();
            Message message;
            if (dr[Common.KEYNAME.PUSHID].ToString().Equals(Common.KEYNAME.ALL) && (dr[Common.KEYNAME.SERVICEID].ToString().Equals(Common.KEYNAME.ALL)))
            {
                //send push to all devices
                message = new Message()
                {
                    Data = dicMsg,
                    Topic = Common.KEYNAME.ALL,
                    //Notification = new Notification()
                    //{
                    //    Title = dicMsg["title"],
                    //    Body = dicMsg["body"],
                    //},
                    Apns = new ApnsConfig 
                    { 
                        Aps = new Aps 
                        { 
                            ContentAvailable = true, 
                            Alert = new ApsAlert 
                            { 
                                Title = dicMsg["title"], 
                                Body = dicMsg["body"] 
                            } 
                        } 
                    },
                    Android = new AndroidConfig 
                    { 
                        Priority = Priority.High,
                    }
                };
            }
            else if (dr[Common.KEYNAME.PUSHID].ToString().Equals(Common.KEYNAME.ALL) && (dr[Common.KEYNAME.SERVICEID].ToString().Equals(Common.KEYNAME.MB) || dr[Common.KEYNAME.SERVICEID].ToString().Equals(Common.KEYNAME.AM)))
            {
                //send push to all devices
                message = new Message()
                {
                    Data = dicMsg,
                    Topic = dr[Common.KEYNAME.SERVICEID].ToString(),
                    //Notification = new Notification()
                    //{
                    //    Title = dicMsg["title"],
                    //    Body = dicMsg["body"]
                    //},
                    Apns = new ApnsConfig
                    {
                        Aps = new Aps
                        {
                            ContentAvailable = true,
                            Alert = new ApsAlert
                            {
                                Title = dicMsg["title"],
                                Body = dicMsg["body"]
                            }
                        }
                    },
                    Android = new AndroidConfig
                    {
                        Priority = Priority.High
                    }
                };
            }
            else
            {
                regIDs.Add(dr[Common.KEYNAME.PUSHID].ToString());

                message = new Message()
                {
                    Data = dicMsg,
                    Token = dr[Common.KEYNAME.PUSHID].ToString(),
                    //Notification = new Notification()
                    //{
                    //    Title = dicMsg["title"],
                    //    Body = dicMsg["body"]
                    //},
                    Apns = new ApnsConfig
                    {
                        Aps = new Aps
                        {
                            ContentAvailable = true,
                            Alert = new ApsAlert
                            {
                                Title = dicMsg["title"],
                                Body = dicMsg["body"]
                            },
                        }
                    },
                    Android = new AndroidConfig
                    {
                        Priority = Priority.High
                    }
                };
            }

            FirebaseApp firebaseApp = GetApp(dr[Common.KEYNAME.SERVICEID].ToString());



            string response = string.Empty;

            try
            {
                response = FirebaseMessaging.GetMessaging(firebaseApp).SendAsync(message).GetAwaiter().GetResult();
                Console.WriteLine("Successfully sent message: " + response);
            }
            catch (FirebaseMessagingException fme)
            {
                string errorDesc = $"{fme.MessagingErrorCode} - {fme.Message}";
                ProcessLog.LogInformation($"[Firebase] Push nofication failed, firebase exption: {fme.ToString()}");

                //update false
                con.ExecuteNonquery(Common.ConStr, "EBA_PN_MESSAGE_UPDATE_RESULT", new object[] { Int64.Parse(dr[Common.KEYNAME.ID].ToString()), MappError(errorDesc), errorDesc });
                return;
            }
            catch (FirebaseException fe)
            {
                string errorDesc = fe.Message;
                ProcessLog.LogInformation($"[Firebase] Push nofication failed, firebase exption: {fe.ToString()}");

                //update false
                con.ExecuteNonquery(Common.ConStr, "EBA_PN_MESSAGE_UPDATE_RESULT", new object[] { Int64.Parse(dr[Common.KEYNAME.ID].ToString()), MappError(errorDesc), errorDesc });
                return;
            }
            catch (Exception ex)
            {
                string errorDesc = ex.Message;
                ProcessLog.LogInformation("[Firebase] Push nofication failed " + ex.ToString());

                //update false
                con.ExecuteNonquery(Common.ConStr, "EBA_PN_MESSAGE_UPDATE_RESULT", new object[] { Int64.Parse(dr[Common.KEYNAME.ID].ToString()), MappError(errorDesc), errorDesc });
                return;
            }

            con.ExecuteNonquery(Common.ConStr, "EBA_PN_MESSAGE_UPDATE_RESULT", new object[] { Int64.Parse(dr[Common.KEYNAME.ID].ToString()), Common.KEYNAME.Y, response });
        }

        public JObject BuildAndroidJson(DataRow dr)
        {
            JObject msg = new JObject();
            msg.Add(new JProperty("title", dr[Common.KEYNAME.TITLE].ToString()));
            msg.Add(new JProperty("subtitle", ""));
            msg.Add(new JProperty("body", dr[Common.KEYNAME.BODY].ToString()));
            msg.Add(new JProperty("imgurl", dr[Common.KEYNAME.IMGURL].ToString()));
            msg.Add(new JProperty("link", dr[Common.KEYNAME.LINK].ToString()));
            //msg.Add(new JProperty("data", dr[Common.KEYNAME.DATA].ToString()));
            msg.Add(new JProperty("trancode", dr[Common.KEYNAME.TRANCODE].ToString()));
            msg.Add(new JProperty("group", dr[Common.KEYNAME.GROUP].ToString()));
            msg.Add(new JProperty("datetime", ((DateTime)dr[Common.KEYNAME.DATEINSERT]).ToString("dd/MM/yyyy hh:mm:ss")));

            //vutt 20180522 add msgid
            msg.Add(new JProperty("id", dr[Common.KEYNAME.ID].ToString()));

            //JObject jdata = new JObject();
            //string[] arrData = dr[Common.KEYNAME.DATA].ToString().Split('#');
            //foreach (string data in arrData)
            //{
            // if (!string.IsNullOrEmpty(data) && data.Contains("$"))
            // {
            // string[] kv = data.Split('$');
            // if (jdata[kv[0]] == null)
            // {
            // jdata.Add(new JProperty(kv[0], kv[1]));
            // }
            // }
            //}
            msg.Add("data", dr[Common.KEYNAME.DATA].ToString());
            msg.Add("detail", dr[Common.KEYNAME.DETAIL].ToString());
            return msg;
        }
        public JObject BuildiOSJson(DataRow dr)
        {
            JObject msg = new JObject();

            //node aps
            JObject aps = new JObject();
            JObject alert = new JObject();
            alert.Add(new JProperty("title", dr[Common.KEYNAME.TITLE].ToString()));
            alert.Add(new JProperty("body", dr[Common.KEYNAME.BODY].ToString()));
            aps.Add(new JProperty("alert", alert));
            msg.Add(new JProperty("aps", aps)); //follow apple spec

            msg.Add(new JProperty("title", dr[Common.KEYNAME.TITLE].ToString())); //tomake the same as android
            msg.Add(new JProperty("body", dr[Common.KEYNAME.BODY].ToString())); //tomake the same as android

            msg.Add(new JProperty("imgurl", dr[Common.KEYNAME.IMGURL].ToString()));
            //msg.Add(new JProperty("media-url", dr[Common.KEYNAME.IMGURL].ToString()));
            msg.Add(new JProperty("link", dr[Common.KEYNAME.LINK].ToString()));
            msg.Add(new JProperty("trancode", dr[Common.KEYNAME.TRANCODE].ToString()));
            msg.Add(new JProperty("group", dr[Common.KEYNAME.GROUP].ToString()));
            msg.Add(new JProperty("datetime", ((DateTime)dr[Common.KEYNAME.DATEINSERT]).ToString("dd/MM/yyyy hh:mm:ss")));
            //vutt 20180522 add msgid
            msg.Add(new JProperty("id", dr[Common.KEYNAME.ID].ToString()));
            msg.Add(new JProperty("detail", dr[Common.KEYNAME.DETAIL].ToString()));

            //JObject jdata = new JObject();
            //string[] arrData = dr[Common.KEYNAME.DATA].ToString().Split('#');
            //foreach (string data in arrData)
            //{
            // if (!string.IsNullOrEmpty(data) && data.Contains("$"))
            // {
            // string[] kv = data.Split('$');
            // if (jdata[kv[0]] == null)
            // {
            // jdata.Add(new JProperty(kv[0], kv[1]));
            // }
            // }
            //}
            msg.Add("data", dr[Common.KEYNAME.DATA].ToString());

            return msg;
        }

        public string MappError(string errorDesc)
        {
            string pushStatus = "F";

            if (dicMapError == null)
            {
                dicMapError = new Dictionary<string, string>();
                Connection con = new Connection();
                DataTable dtMap = con.FillDataTable(Common.ConStr, "EBA_PN_MAPERROR_SELECT_ALL");
                foreach (DataRow dr in dtMap.Rows)
                {
                    dicMapError.Add(dr[Common.KEYNAME.ERRORDESC].ToString().Trim(), dr[Common.KEYNAME.MAPSTATUS].ToString().Trim());
                }
            }

            if (dicMapError.ContainsKey(errorDesc.Trim()))
            {
                pushStatus = dicMapError[errorDesc.Trim()];
            }

            return pushStatus;
        }
        private void StartServer()
        {
            try
            {
                Utility.Common.ConStr = ConfigurationManager.ConnectionStrings["ConnectionHost"].ToString();
                Utility.Common.ConStr = Utility.Common.DecryptData(Utility.Common.ConStr);
                isRunning = true;
                con = new Connection();
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

        }
        private void StopServer()
        {
            //autoTrans = null;
            isRunning = false;
            con = null;
            thread.Abort();
        }
    }
}