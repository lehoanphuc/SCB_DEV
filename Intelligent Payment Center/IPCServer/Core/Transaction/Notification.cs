using DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using System.Collections;
using Interfaces;

namespace Transaction
{
    public class Notification
    {
        public bool SetPendingStatusForPushNotification(TransactionInfo tran, string parmlist)
        {
            
            if (tran.Status.Equals(Common.TRANSTATUS.PENDDING))
            {
                tran.Status = Common.TRANSTATUS.FINISH;
            }
            return SendTranPush(tran, parmlist);
        }

        public bool SendTranPush(TransactionInfo tran, string parmlist)
        {
            try
            {
                if (tran.Status != Common.TRANSTATUS.FINISH)
                {
                    return true;
                }

                // vutt build push message
                string[] parr = parmlist.Split('|');

                string UserID = tran.Data.ContainsKey(parr[0]) ? tran.Data[parr[0]].ToString() : parr[0];
                string AccountNo = tran.Data.ContainsKey(parr[1]) ? tran.Data[parr[1]].ToString() : parr[1];
                string UserInsert = tran.Data.ContainsKey(parr[2]) ? tran.Data[parr[2]].ToString() : parr[2];
                string PushType = tran.Data.ContainsKey(parr[3]) ? tran.Data[parr[3]].ToString() : parr[3];
                string sourceID = parr.Length > 4 ? parr[4] : tran.Data[Common.KEYNAME.SOURCEID].ToString();


                Hashtable hsMsg = Formatters.Formatter.CreatePushNotification(tran, PushType);

                if (hsMsg != null)
                {
                    Connection con = new Connection();
                    int rs = con.ExecuteNonquery(Common.ConStr, "EBA_PN_MESSAGE_INSERT", new object[] {
                    tran.IPCTransID,
                    sourceID,
                    UserID,
                    AccountNo,
                    tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                    hsMsg[Common.KEYNAME.GROUP].ToString(),
                    hsMsg[Common.KEYNAME.TITLE].ToString(),
                    hsMsg[Common.KEYNAME.BODY].ToString(),
                    hsMsg[Common.KEYNAME.DETAIL].ToString(),
                    hsMsg[Common.KEYNAME.IMGURL].ToString(),
                    hsMsg[Common.KEYNAME.LINK].ToString(),
                    hsMsg[Common.KEYNAME.DATA].ToString(),
                    UserInsert,
                    hsMsg[Common.KEYNAME.ACTION].ToString()
                });

                    if (rs != -1)
                    {
                        ProcessLog.LogInformation($"Cannot send push notification, transaction id: {tran.IPCTransID.ToString()}");
                    }
                }
                else
                {
                    ProcessLog.LogInformation($"Cannot create push message (CreatePushNotification), transaction id: {tran.IPCTransID.ToString()}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return true;
            }
        }
        public bool SendPushNotification(TransactionInfo tran)
        {
            try
            {
                Connection dbObj = new Connection();

                Hashtable hsMsg = Formatters.Formatter.CreatePushNotificationV2(tran);

                DataTable dt = dbObj.FillDataTable(Common.ConStr, "EBA_GETUSERTOPUSH", tran.Data[Common.KEYNAME.ID].ToString(), tran.Data["SENDTYPE"].ToString());
                if (dt.Rows.Count == 0)
                {
                    ProcessLog.LogInformation("Error when get users : " + tran.IPCTransID.ToString());
                    return false;
                }

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            if (tran.Data.ContainsKey(Common.KEYNAME.TRANDESC))
                                tran.Data[Common.KEYNAME.TRANDESC] = hsMsg[Common.KEYNAME.TITLE].ToString();
                            else
                                tran.Data.Add(Common.KEYNAME.TRANDESC, hsMsg[Common.KEYNAME.TITLE].ToString());

                            if (tran.Data.ContainsKey(Common.KEYNAME.ERRORDESC))
                                tran.Data[Common.KEYNAME.ERRORDESC] = string.Empty;
                            else
                                tran.Data.Add(Common.KEYNAME.ERRORDESC, string.Empty);

                            int rs = dbObj.ExecuteNonquery(Common.ConStr, "EBA_PN_MESSAGE_INSERT_V2", new object[] {
                                tran.IPCTransID,
                                dr[Common.KEYNAME.SERVICEID].ToString(),
                                dr[Common.KEYNAME.USERID].ToString(),
                                string.Empty,
                                tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(),
                                dr["TYPENOTIFY"].ToString(),
                                hsMsg[Common.KEYNAME.TITLE].ToString(),
                                hsMsg[Common.KEYNAME.BODY].ToString(),
                                hsMsg[Common.KEYNAME.DETAIL].ToString(),
                                hsMsg[Common.KEYNAME.IMGURL].ToString(),
                                hsMsg[Common.KEYNAME.LINK].ToString(),
                                hsMsg[Common.KEYNAME.DATA].ToString(),
                                dr["USERCREATED"].ToString()
                            });

                            if (rs != 1)
                            {
                                ProcessLog.LogInformation($"Cannot send push notification, transaction id: {tran.IPCTransID.ToString()}");
                            }

                            new TransLib().LogTransaction(tran);
                            tran.NewIPCTransID();
                        }
                        catch (Exception ex)
                        {
                            ProcessLog.LogInformation($"Cannot send push notification, transaction id: {tran.IPCTransID.ToString()}, {ex.Message}");
                        }
                    }
                }
                else
                {
                    ProcessLog.LogInformation($"Cannot create push message, transaction id: {tran.IPCTransID.ToString()}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

    }
}
