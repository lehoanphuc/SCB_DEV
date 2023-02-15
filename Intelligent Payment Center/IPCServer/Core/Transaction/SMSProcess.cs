using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using Interfaces;
using System.Collections;
using DBConnection;
using System.Configuration;
using System.IO.Ports;

namespace Transaction
{
    public class SMSProcess
    {
        static int maxlen = 0;
        public SMSProcess()
        {
            try
            {
                maxlen = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MAXLENGTH"]);
            }
            catch
            {
                maxlen = 160;
            }
        }
        public bool SendSMSNotification(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                Database objDatabase = new Database();
                DataTable dtacct = con.FillDataTable(Common.ConStr, "SMS_NOTIFICATION_SELECT", null);

                string template = "";



                if (dtacct.Rows.Count > 0)
                {
                    for (int i = 0; i < dtacct.Rows.Count; i++)
                    {
                        template = "";
                        //get templete
                        DataRow[] drTemp = Common.DBIMAPIPCTRANCODESMS.Select(String.Format("IPCTRANCODE = '{0}'", dtacct.Rows[i]["TRANCODE"].ToString()));
                        if (drTemp.Length > 0)
                        {
                            template = drTemp[0][Common.KEYNAME.RESPONSETEMPLATE].ToString();
                        }
                        else
                        {
                            Utility.ProcessLog.LogInformation(string.Format("Transaction {0} haven't templete", dtacct.Rows[i]["TRANCODE"].ToString()));
                            break;
                        }

                        //get mapping
                        string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                        condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                        condition += " AND IPCTRANCODE = '" + dtacct.Rows[i]["TRANCODE"].ToString() + "'";
                        condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                        DataRow[] dr = Common.DBIOUTPUTDEFINESMS.Select(condition, "FIELDNO");
                        if (dr.Length <= 0)
                        {
                            Utility.ProcessLog.LogInformation(string.Format("Transaction {0} haven't mapping output", dtacct.Rows[i]["TRANCODE"].ToString()));
                            break;
                        }
                        //send sms
                        if (dtacct.Rows[i][Common.KEYNAME.CCYID] != null || dtacct.Rows[i][Common.KEYNAME.CCYID].ToString().Equals("") == false)
                        {
                            string msgContent = template;
                            DataTable dtacctright = con.FillDataTable(Common.ConStr, "SMS_GETACCTRIGHT", dtacct.Rows[i]["ACCOUNTNO"].ToString(), dtacct.Rows[i]["TRANCODE"].ToString());
                            for (int j = 0; j < dtacctright.Rows.Count; j++)
                            {
                                Hashtable hssms = new Hashtable();
                                foreach (DictionaryEntry entry in tran.Data)
                                {
                                    hssms.Add(entry.Key, entry.Value);
                                }
                                GetDataFromDatatable(tran, dtacct, i, ref hssms);
                                //bool isGetCCYID = objDatabase.DoStore(tran, "SMS_AUTOBALANCE_GETCCYID|2|" + dtacct.Rows[i]["ACCOUNTNO"].ToString());
                                for (int b = 0; b < dr.Length; b++)
                                {
                                    //vutt 16102016 mask account no
                                    object value = hssms[dr[b]["VALUENAME"].ToString()].ToString();
                                    Formatters.Formatter.FormatFieldValue(ref value, dr[b]["FORMATTYPE"].ToString(), dr[b]["FORMATOBJECT"].ToString(), dr[b]["FORMATFUNCTION"].ToString(), dr[b]["FORMATPARM"].ToString());
                                    msgContent = msgContent.Replace("[" + dr[b]["FIELDNAME"].ToString() + "]", value.ToString());
                                }
                                DataTable dtUserid = con.FillDataTable(Common.ConStr, "SMS_USER_SELECTBYUSERID", dtacctright.Rows[j][Common.KEYNAME.USERID].ToString());
                                if (dtUserid.Rows.Count > 0)
                                {
                                    if (msgContent.Length > maxlen)
                                    {
                                        //vutt case o lao co dau breakline tinh lenght ko chinh xac
                                        msgContent = msgContent.Substring(0, maxlen - 15) + "...";
                                    }
                                    bool msgOut = SendMsgOut(tran, hssms[Common.KEYNAME.TRANREF].ToString(), dtUserid.Rows[0][Common.KEYNAME.PHONENO].ToString(), "", "", msgContent, Common.IPCWorkDate, "2", "T");
                                }

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
                return false;
            }
            return true;
        }
        public bool AutoBalance(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                string template = "";
                DataRow[] drTemp = Common.DBIMAPIPCTRANCODESMS.Select(String.Format("IPCTRANCODE = '{0}'", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString()));
                if (drTemp.Length > 0)
                {
                    template = drTemp[0][Common.KEYNAME.RESPONSETEMPLATE].ToString();
                }
                Database objDatabase = new Database();
                DataTable dtacct = con.FillDataTable(Common.ConStr, "SMS_AUTOBALANCE_SELECT", null);

                if (dtacct.Rows.Count > 0)
                {
                    string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                    condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                    condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "'";
                    condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                    DataRow[] dr = Common.DBIOUTPUTDEFINESMS.Select(condition, "FIELDNO");
                    if (dr.Length <= 0)
                    {
                        tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                        return false;
                    }
                    for (int i = 0; i < dtacct.Rows.Count; i++)
                    {
                        if (dtacct.Rows[i][Common.KEYNAME.CCYID] != null || dtacct.Rows[i][Common.KEYNAME.CCYID].ToString().Equals("") == false)
                        {
                            string msgContent = template;
                            DataTable dtacctright = con.FillDataTable(Common.ConStr, "SMS_GETACCTRIGHT", dtacct.Rows[i]["ACCOUNTNO"].ToString(), tran.Data[Common.KEYNAME.IPCTRANCODE].ToString());
                            for (int j = 0; j < dtacctright.Rows.Count; j++)
                            {
                                GetDataFromDatatable(tran, dtacct, i);
                                //bool isGetCCYID = objDatabase.DoStore(tran, "SMS_AUTOBALANCE_GETCCYID|2|" + dtacct.Rows[i]["ACCOUNTNO"].ToString());
                                for (int b = 0; b < dr.Length; b++)
                                {
                                    msgContent = msgContent.Replace("[" + dr[b]["FIELDNAME"].ToString() + "]", tran.Data[dr[b]["VALUENAME"]].ToString());
                                }
                                DataTable dtUserid = con.FillDataTable(Common.ConStr, "SMS_USER_SELECTBYUSERID", dtacctright.Rows[j][Common.KEYNAME.USERID].ToString());
                                if (dtUserid.Rows.Count > 0)
                                {
                                    if (msgContent.Length > maxlen)
                                    {
                                        //vutt case o lao co dau breakline tinh lenght ko chinh xac
                                        msgContent = msgContent.Substring(0, maxlen - 15) + "...";
                                    }
                                    bool msgOut = SendMsgOut(tran, tran.Data["TRANREF"].ToString(), dtUserid.Rows[0][Common.KEYNAME.PHONENO].ToString(), "", "", msgContent, Common.IPCWorkDate, "2", "T");
                                }

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
                return false;
            }
            return true;
        }
        private void GetDataFromDatatable(TransactionInfo tran, DataTable dt, int row, ref Hashtable hs)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        if (hs.ContainsKey(dt.Columns[col].ColumnName) == false)
                        {
                            hs.Add(dt.Columns[col].ColumnName, dt.Rows[row][col]);
                        }
                        else
                        {
                            hs[dt.Columns[col].ColumnName] = dt.Rows[row][col];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
            }
        }
        private void GetDataFromDatatable(TransactionInfo tran, DataTable dt, int row)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        if (tran.Data.ContainsKey(dt.Columns[col].ColumnName) == false)
                        {
                            tran.Data.Add(dt.Columns[col].ColumnName, dt.Rows[row][col]);
                        }
                        else
                        {
                            tran.Data[dt.Columns[col].ColumnName] = dt.Rows[row][col];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
            }
        }
        private bool SendMsgOut(TransactionInfo tran, string ipcTransID, string receivedPhone, string sendPhone, string MSGID, string msgContent,
                            string ipcWorkDate, string piority, string msgType)
        {
            Connection con = new Connection();
            try
            {
                //con.ExecuteNonquery(Common.ConStr, "SMS_MESSAGEOUT_INSERT", ipcTransID, receivedPhone,
                //    sendPhone, MSGID, msgContent, ipcWorkDate, piority, msgType);
                //Utility.ProcessLog.LogInformation("ipctrancode is " + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString());
                //26.10.2016 MINH ADD TRANCODE
                con.ExecuteNonquery(Common.ConStr, "SMS_MESSAGEOUT_INSERT", ipcTransID, receivedPhone,
                    sendPhone, MSGID, msgContent, ipcWorkDate, piority, msgType, tran.Data[Common.KEYNAME.IPCTRANCODE].ToString());


            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                tran.SetErrorInfo(ex);
                return false;
            }
            return true;
        }

        public bool HappyBirthday(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                string template = "";
                DataRow[] drTemp = Common.DBIMAPIPCTRANCODESMS.Select(String.Format("IPCTRANCODE = '{0}'", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString()));
                if (drTemp.Length > 0)
                {
                    template = drTemp[0][Common.KEYNAME.RESPONSETEMPLATE].ToString();
                }

                DataSet dsAutoHappy = (DataSet)tran.Data[Common.KEYNAME.DATARESULT];
                DataTable dtBirthday = (DataTable)dsAutoHappy.Tables[0];

                if (dtBirthday.Rows.Count > 0)
                {
                    string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                    condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                    condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "'";
                    condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                    DataRow[] dr = Common.DBIOUTPUTDEFINESMS.Select(condition, "FIELDNO");
                    if (dr.Length <= 0)
                    {
                        tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                        return false;
                    }
                    for (int i = 0; i < dtBirthday.Rows.Count; i++)
                    {
                        string msgContent = template;
                        GetDataFromDatatable(tran, dtBirthday, i);
                        //DEFAULT ACTION AUTO HAPPY NOT CHECK TRANRIGHTDETAIL
                        //1 CUSTID CO NHIEU HOP DONG VA CHI 1 HOP DONG DC ACTIVE, 1 HOP DONG CO NHIEU USERID
                        for (int b = 0; b < dr.Length; b++)
                        {
                            msgContent = msgContent.Replace("[" + dr[b]["FIELDNAME"].ToString() + "]", tran.Data[dr[b]["VALUENAME"]].ToString());
                        }
                        DataTable dtUserid = con.FillDataTable(Common.ConStr, "SMS_GETUSERIDAUTOHAPPY", tran.Data["CUSTCODE"].ToString(), tran.Data["CUSTTYPE"].ToString());
                        if (dtUserid.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtUserid.Rows.Count; j++)
                            {
                                DataTable dtPhone = con.FillDataTable(Common.ConStr, "SMS_USER_SELECTBYUSERID", dtUserid.Rows[j][Common.KEYNAME.USERID].ToString());
                                if (dtPhone.Rows.Count > 0)
                                {
                                    bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), dtPhone.Rows[0][Common.KEYNAME.PHONENO].ToString(), "", "", msgContent, Common.IPCWorkDate, "2", "T");
                                }
                            }
                        }
                        else
                        {
                            if (tran.Data.ContainsKey("mobile"))
                            {
                                bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), tran.Data["mobile"].ToString(), "", "", msgContent, Common.IPCWorkDate, "2", "T");
                            }
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
                return false;
            }
            return true;
        }

        public bool NotificationAdvert(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                string contentMsg = Common.RemoveSign(tran.Data[Common.KEYNAME.MSGCONTENT].ToString());
                contentMsg = Common.ReplaceCharSpec(contentMsg, "*|#|$|'|--");
                if (tran.Data.ContainsKey("SENDTYPE") == false)
                {
                    tran.ErrorCode = Common.ERRORCODE.INVALID_SENDTYPE;
                    return false;
                }
                else
                {
                    string sendType = tran.Data["SENDTYPE"].ToString();
                    switch (sendType)
                    {
                        // SEND TRONG DANH SACH DIEN THOAI DANG KY SMS
                        case "1":
                            DataTable dtPhoneNo = con.FillDataTable(Common.ConStr, "SMS_USER_PHONENO", null);
                            for (int i = 0; i < dtPhoneNo.Rows.Count; i++)
                            {
                                bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), dtPhoneNo.Rows[i][Common.KEYNAME.PHONENO].ToString(), "", "", contentMsg, Common.IPCWorkDate, "3", "T");
                            }
                            break;
                        // SEND TRONG DANH SACH DIEN THOAI BO SUNG
                        case "2":
                            if (tran.Data.ContainsKey("PHONENOLIST") == false)
                            {
                                tran.ErrorCode = Common.ERRORCODE.INVALID_PHONENOLIST;
                                return false;
                            }
                            else if (tran.Data["PHONENOLIST"].ToString().Equals(""))
                            {
                                tran.ErrorCode = Common.ERRORCODE.INVALID_PHONENOLIST;
                                return false;
                            }
                            else
                            {
                                string[] phoneNoList = tran.Data["PHONENOLIST"].ToString().Split(';');
                                DataTable dtPhoneNotAdvert = con.FillDataTable(Common.ConStr, "SMS_PHONENOTADVERT_SELECT", null);
                                if (phoneNoList.Length > 0)
                                {
                                    for (int i = 0; i < phoneNoList.Length; i++)
                                    {
                                        if (dtPhoneNotAdvert.Rows.Count > 0)
                                        {
                                            DataRow[] dr = dtPhoneNotAdvert.Select(String.Format("PHONENO = '{0}'", phoneNoList[i].ToString()));
                                            if (dr.Length == 0)
                                            {
                                                bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), phoneNoList[i].ToString(), "", "", contentMsg, Common.IPCWorkDate, "3", "T");
                                            }
                                        }
                                        else
                                        {
                                            bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), phoneNoList[i].ToString(), "", "", contentMsg, Common.IPCWorkDate, "3", "T");
                                        }
                                    }
                                }
                            }
                            break;
                        // SEND CA 2 TRONG DANH SACH DANG KY SMS VA DS DIEN THOAI BO SUNG
                        case "3":
                            if (tran.Data.ContainsKey("PHONENOLIST") == false)
                            {
                                tran.ErrorCode = Common.ERRORCODE.INVALID_PHONENOLIST;
                                return false;
                            }
                            else if (tran.Data["PHONENOLIST"].ToString().Equals(""))
                            {
                                tran.ErrorCode = Common.ERRORCODE.INVALID_PHONENOLIST;
                                return false;
                            }
                            else
                            {
                                string[] phoneNoList = tran.Data["PHONENOLIST"].ToString().Split(';');
                                DataTable dtPhoneNotAdvert = con.FillDataTable(Common.ConStr, "SMS_PHONENOTADVERT_SELECT", null);
                                if (phoneNoList.Length > 0)
                                {
                                    for (int i = 0; i < phoneNoList.Length; i++)
                                    {
                                        if (dtPhoneNotAdvert.Rows.Count > 0)
                                        {
                                            DataRow[] dr = dtPhoneNotAdvert.Select(String.Format("PHONENO = '{0}'", phoneNoList[i].ToString()));
                                            if (dr.Length == 0)
                                            {
                                                bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), phoneNoList[i].ToString(), "", "", contentMsg, Common.IPCWorkDate, "3", "T");
                                            }
                                        }
                                        else
                                        {
                                            bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), phoneNoList[i].ToString(), "", "", contentMsg, Common.IPCWorkDate, "3", "T");
                                        }
                                    }
                                }
                            }
                            DataTable dtPhoneNoList = con.FillDataTable(Common.ConStr, "SMS_USER_PHONENO", null);
                            for (int i = 0; i < dtPhoneNoList.Rows.Count; i++)
                            {
                                bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), dtPhoneNoList.Rows[i][Common.KEYNAME.PHONENO].ToString(), "", "", contentMsg, Common.IPCWorkDate, "3", "T");
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
                return false;
            }
            return true;
        }

        public bool InsertAutoBalance(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                string strDesc = "";
                DataSet dsAcct = (DataSet)tran.Data[Common.KEYNAME.DATARESULT];
                DataTable dtAcct = dsAcct.Tables[0];
                for (int i = 0; i < dtAcct.Rows.Count; i++)
                {
                    strDesc = dtAcct.Rows[i]["DESC"].ToString().Replace('#', ' ');

                    //vutt hardcode truong hop STBATM
                    bool isSTBATM = false;
                    Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["STBATM"].ToString(), out isSTBATM);
                    if (isSTBATM)
                    {
                        if (strDesc.Contains("|"))
                        {
                            string[] tmp = strDesc.Split('|');
                            strDesc = tmp[tmp.Length - 2];
                        }
                    }
                    //end hardcode STBATM

                    try
                    {
                        con.ExecuteNonquery(Common.ConStr, "SMS_AUTOBALANCE_INSERT", dtAcct.Rows[i][Common.KEYNAME.TRANREF].ToString(),
                            dtAcct.Rows[i][Common.KEYNAME.ACCTNO].ToString(), dtAcct.Rows[i][Common.KEYNAME.AMOUNT].ToString(),
                            strDesc, dtAcct.Rows[i]["BALANCE"].ToString(), dtAcct.Rows[i]["TRANDATE"].ToString() + " " + dtAcct.Rows[i]["TRANTIME"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Utility.ProcessLog.LogInformation("InsertAutoBalance error: " + ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
                return false;
            }
            return true;
        }
        public bool GetSMSOTP(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                string sendType = "";
                string lang = tran.Data.ContainsKey(Common.KEYNAME.LANG) ? tran.Data[Common.KEYNAME.LANG].ToString() : "en-US";
                string template = "";
                DataTable dtTemplate = con.FillDataTable(Common.ConStr, "IPC_GETSMSTEMPLATE", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), sendType, lang);
                if (dtTemplate.Rows.Count > 0)
                {
                    template = dtTemplate.Rows[0][Common.KEYNAME.RESPONSETEMPLATE].ToString();
                }
                Database objDatabase = new Database();

                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "'";
                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] dr = Common.DBIOUTPUTDEFINESMS.Select(condition, "FIELDNO");

                if (dr.Length <= 0)
                {
                    tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                    Utility.ProcessLog.LogError(new Exception($"{condition} not found!"), System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                    return false;
                }
                string msgContent = template;
                //GetDataFromDatatable(tran, dtBirthday, i);
                for (int b = 0; b < dr.Length; b++)
                {
                    msgContent = msgContent.Replace("[" + dr[b]["FIELDNAME"].ToString() + "]", tran.Data[dr[b]["VALUENAME"]].ToString());
                }
                //DataTable dtUserid = con.FillDataTable(Common.ConStr, "SMS_GETUSERIDAUTOHAPPY", tran.Data["CUSTCODE"].ToString(), tran.Data["CUSTTYPE"].ToString());

                bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), tran.Data[Common.KEYNAME.PHONENO].ToString(), "", "", msgContent, Common.IPCWorkDate, "2", "T");
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
                return false;
            }
            return true;
        }

        public bool SendSMS(TransactionInfo tran, string parm)
        {
            Connection con = new Connection();
            try
            {
                string[] parr = parm.Split('|');
                string phoneno = tran.Data.ContainsKey(parr[0]) ? tran.Data[parr[0]].ToString() : parr[0];
                string sendType = (parr.Length > 1) ? (tran.Data.ContainsKey(parr[1]) ? tran.Data[parr[1]].ToString() : parr[1]) : string.Empty;
                string lang = tran.Data.ContainsKey(Common.KEYNAME.LANG) ? tran.Data[Common.KEYNAME.LANG].ToString() : "en-US";
                if (tran.Status != Common.TRANSTATUS.FINISH)
                {
                    return true;
                }
                string template = string.Empty;

                DataTable dtTemplate = con.FillDataTable(Common.ConStr, "IPC_GETSMSTEMPLATE", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), sendType, lang);
                if (dtTemplate.Rows.Count > 0)
                {
                    template = dtTemplate.Rows[0][Common.KEYNAME.RESPONSETEMPLATE].ToString();
                }


                if (string.IsNullOrEmpty(template))
                {
                    Utility.ProcessLog.LogError(new Exception($"Template SMS " + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + " - " + lang + " is empty!"), System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                    return true;
                }
                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "'";
                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                DataRow[] dr = Common.DBIOUTPUTDEFINESMS.Select(condition, "FIELDNO");

                if (dr.Length <= 0)
                {
                    tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                    return false;
                }
                string msgContent = template;

                for (int i = 0; i < dr.Length; i++)
                {
                    string ValueStyle = dr[i]["VALUESTYLE"].ToString();
                    string ValueName = dr[i]["VALUENAME"].ToString();
                    object value = Formatters.Formatter.GetFieldValue(tran, ValueStyle, ValueName, tran.Data, tran.parm, null);
                    Formatters.Formatter.FormatFieldValue(ref value, dr[i]["FORMATTYPE"].ToString(), dr[i]["FORMATOBJECT"].ToString(), dr[i]["FORMATFUNCTION"].ToString(), dr[i]["FORMATPARM"].ToString());
                    msgContent = msgContent.Replace("[" + dr[i]["FIELDNAME"].ToString() + "]", value.ToString());
                }

                phoneno = string.IsNullOrEmpty(phoneno) ? tran.Data[Common.KEYNAME.PHONENO].ToString() : phoneno;
                bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), phoneno, "", "", msgContent, Common.IPCWorkDate, "2", "T");
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
                return false;
            }
            return true;
        }
        public bool GetDataFromDataSet(TransactionInfo tran, string parmlist)
        {
            try
            {
                string[] parms = parmlist.Split('|');
                DataSet ds = (DataSet)tran.Data[parms[0]];
                string TypeGet = parms[1].ToString();
                switch (TypeGet)
                {
                    case "":
                    case "0":
                        #region Lay Data khi Key Name khong ton tai
                        foreach (DataTable dt in ds.Tables)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                for (int col = 0; col < dt.Columns.Count; col++)
                                {
                                    if (tran.Data.ContainsKey(dt.Columns[col].ColumnName) == false)
                                    {
                                        tran.Data.Add(dt.Columns[col].ColumnName, dt.Rows[0][col]);
                                    }
                                    for (int row = 1; row < dt.Rows.Count; row++)
                                    {
                                        if (tran.Data.ContainsKey(dt.Columns[col].ColumnName + row.ToString()) == false)
                                        {
                                            tran.Data.Add(dt.Columns[col].ColumnName + row.ToString(), dt.Rows[row][col]);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    case "1":
                        #region Thay the Data khi Data = null hoac Data = ""
                        foreach (DataTable dt in ds.Tables)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                for (int col = 0; col < dt.Columns.Count; col++)
                                {
                                    if (tran.Data.ContainsKey(dt.Columns[col].ColumnName) == false)
                                    {
                                        tran.Data.Add(dt.Columns[col].ColumnName, dt.Rows[0][col]);
                                    }
                                    else if (tran.Data[dt.Columns[col].ColumnName] == null || tran.Data[dt.Columns[col].ColumnName].ToString() == "")
                                    {
                                        tran.Data[dt.Columns[col].ColumnName] = dt.Rows[0][col];
                                    }
                                    for (int row = 1; row < dt.Rows.Count; row++)
                                    {
                                        if (tran.Data.ContainsKey(dt.Columns[col].ColumnName + row.ToString()) == false)
                                        {
                                            tran.Data.Add(dt.Columns[col].ColumnName + row.ToString(), dt.Rows[row][col]);
                                        }
                                        else if (tran.Data[dt.Columns[col].ColumnName + row.ToString()] == null ||
                                                 tran.Data[tran.Data[dt.Columns[col].ColumnName + row.ToString()]].ToString() == "")
                                        {
                                            tran.Data[dt.Columns[col].ColumnName + row.ToString()] = dt.Rows[row][col];
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    case "2":
                        #region Luon thay the Data
                        foreach (DataTable dt in ds.Tables)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                for (int col = 0; col < dt.Columns.Count; col++)
                                {
                                    if (tran.Data.ContainsKey(dt.Columns[col].ColumnName) == false)
                                    {
                                        tran.Data.Add(dt.Columns[col].ColumnName, dt.Rows[0][col]);
                                    }
                                    else
                                    {
                                        tran.Data[dt.Columns[col].ColumnName] = dt.Rows[0][col];
                                    }
                                    for (int row = 1; row < dt.Rows.Count; row++)
                                    {
                                        if (tran.Data.ContainsKey(dt.Columns[col].ColumnName + row.ToString()) == false)
                                        {
                                            tran.Data.Add(dt.Columns[col].ColumnName + row.ToString(), dt.Rows[row][col]);
                                        }
                                        else
                                        {
                                            tran.Data[dt.Columns[col].ColumnName + row.ToString()] = dt.Rows[row][col];
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    case "3":
                        #region tra ve XML (keyName: table1,row[0][0], value: table0, tagname: table1,row[0][1]), rootname: SELECTRESULT
                        if (ds.Tables.Count != 2)
                        {
                            tran.SetErrorInfo(Common.ERRORCODE.INVALID_RESULT, "");
                            return false;
                        }
                        string keyname = string.Empty;
                        string value = string.Empty;
                        keyname = ds.Tables[1].Rows[0][0].ToString();
                        ds.Tables[0].TableName = ds.Tables[1].Rows[0][1].ToString();
                        ds.Tables.Remove(ds.Tables[1]);
                        ds.DataSetName = Common.KEYNAME.SELECTRESULT;
                        value = ds.GetXml();
                        if (tran.Data.ContainsKey(keyname) == false)
                        {
                            tran.Data.Add(keyname, value);
                        }
                        else
                        {
                            tran.Data[keyname] = value;
                        }
                        #endregion
                        break;
                    case "4":
                        #region luon luon tra ve XML cua dataset, keynam: SELECTRESULT
                        if (tran.Data.ContainsKey(Common.KEYNAME.SELECTRESULT))
                        {
                            tran.Data[Common.KEYNAME.SELECTRESULT] = ds.GetXml();
                        }
                        else
                        {
                            tran.Data.Add(Common.KEYNAME.SELECTRESULT, ds.GetXml());
                        }
                        #endregion
                        break;
                    case "5":
                        #region tra ve ket qua la dataset
                        if (tran.Data.ContainsKey(Common.KEYNAME.SELECTRESULT))
                        {
                            tran.Data[Common.KEYNAME.SELECTRESULT] = ds;
                        }
                        else
                        {
                            tran.Data.Add(Common.KEYNAME.SELECTRESULT, ds);
                        }
                        #endregion
                        break;
                    default:
                        tran.SetErrorInfo(Common.ERRORCODE.INVALID_TYPEGET, "");
                        return false;
                }
            }
            catch (Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
            return true;
        }
        public bool RunUSSD(TransactionInfo tran)
        {
            System.Reflection.Assembly assembly = null;
            Type type = null;
            object instance = null;
            string method = string.Empty;
            string result = null;
            SerialPort port = new SerialPort();
            try
            {
                DataTable SMSSupplier = new DataTable();
                Connection con = new Connection();
                SMSSupplier = con.FillDataTable(Common.ConStr, "SMS_GETSMSSUPPLIER");
                assembly = System.Reflection.Assembly.LoadFrom(SMSSupplier.Rows[0]["ASSEMBLYNAME"].ToString());
                type = assembly.GetType(SMSSupplier.Rows[0]["ASSEMBLYTYPE"].ToString());
                method = Common.KEYNAME.RunUSSD;
                instance = Activator.CreateInstance(type);
                //get port:
                Connection dbObj = new Connection();
                DataTable dtport = new DataTable();
                //temporary hardcode this
                dtport = dbObj.FillDataTable(Common.ConStr, "SMS_GSM_GETPORT", "GROUP2");
                if (dtport.Rows.Count == 0)
                {
                    Utility.ProcessLog.LogInformation("ERROR SMS SENDMTMSG GET PORT FAILSE");

                }
                else
                {
                    foreach (DataRow r in dtport.Rows)
                    {

                        port = new SerialPort(r["COMPORT"].ToString(), int.Parse(r["BAUDRATE"].ToString()), Parity.None, int.Parse(r["DATABITS"].ToString()), StopBits.One);
                        //port.NewLine = r["NEWLINE"].ToString();
                        //port.WriteTimeout = int.Parse(r["WRITETIMEOUT"].ToString());
                        //port.ReadTimeout = int.Parse(r["READTIMEOUT"].ToString());
                        //listport.Add(new ListPort { port = port, Flag = true });
                        //if ((bool)r["ISTWOWAYMSG"])
                        //{
                        //    PortTwoWay = r["COMPORT"].ToString();
                        //}
                    }
                }
                if (!string.IsNullOrEmpty(tran.Data[Common.KEYNAME.USSDCode].ToString()))
                {
                    result = (string)type.InvokeMember(method, System.Reflection.BindingFlags.InvokeMethod, null, instance, new object[] { tran.Data[Common.KEYNAME.USSDCode].ToString(), (SerialPort)port });
                }
                Utility.ProcessLog.LogInformation("Run USSD result : =>" + result + "<=");
                if (string.IsNullOrEmpty(result))
                {
                    tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                    return false;
                }
                else
                {
                    tran.Data.Add(Common.KEYNAME.RESULT, result);
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }

        }
        //public bool SendSweepingSMS(TransactionInfo tran)
        //{
        //    Connection con = new Connection();
        //    try
        //    {
        //        //minh add 26.11.2015:
        //        if (!bool.Parse(ConfigurationManager.AppSettings["SendSMSafterSweeping"]))
        //        {
        //            Utility.ProcessLog.LogInformation("Don't send sms by config");
        //            return true;
        //        }
        //        DataTable sweepinnginfor = con.FillDataTable(Common.ConStr, "eba_get_sweeping_infor_to_sendSMS", DateTime.Now.ToString("dd/MM/yyyy"),
        //            tran.Data[Common.KEYNAME.SCHEDULEID].ToString(), tran.Data[Common.KEYNAME.CENACCTNO].ToString(), tran.Data["CONTRACTNO"].ToString(), tran.Data["IPCTRANSID"].ToString());
        //        string tranID = sweepinnginfor.Rows[0][Common.KEYNAME.IPCTRANSID].ToString();
        //        // Utility.ProcessLog.LogInformation("Send SMS infor " + tranID );
        //        string phoneno = sweepinnginfor.Rows[0][Common.KEYNAME.PHONENO].ToString();
        //        //Utility.ProcessLog.LogInformation("Send SMS infor " + tranID + "," + phoneno );
        //        string msgContent = sweepinnginfor.Rows[0][Common.KEYNAME.MSGCONTENT].ToString();
        //        Utility.ProcessLog.LogInformation("Send SMS infor " + tranID + "," + phoneno + "," + msgContent);

        //        bool msgOut = SendMsgOut(tran, tranID, phoneno, "", "", msgContent, Common.IPCWorkDate, "2", "T");
        //        //bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), tran.Data["PhoneNo"].ToString(), "", "", msgContent, Common.IPCWorkDate, "2", "T");
        //        if (msgOut)
        //        {
        //            Utility.ProcessLog.LogInformation("Send SMS success to phone " + phoneno);
        //        }
        //        else
        //        {
        //            Utility.ProcessLog.LogInformation("Send SMS failed");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
        //        tran.SetErrorInfo(ex);
        //        return false;
        //    }
        //    return true;
        //}
        //public bool SendSMSSchedule(TransactionInfo tran)
        //{
        //    Connection con = new Connection();
        //    try
        //    {
        //        if (tran.Data.ContainsKey("SCHEDULEID"))
        //        {
        //            DataTable dtschedule = con.FillDataTable(Common.ConStr, "EBA_SCHEDULES_SELECTBYID", tran.Data["SCHEDULEID"]);
        //            DataRow drschedule = dtschedule.NewRow();
        //            drschedule = dtschedule.Rows[0];
        //            if (drschedule["ISAPPROVED"].ToString().Equals(Common.KEYNAME.YES))
        //            {
        //                string template = "";
        //                DataRow[] drTemp = Common.DBIMAPIPCTRANCODESMS.Select(String.Format("IPCTRANCODE = '{0}'", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString()));
        //                if (drTemp.Length > 0)
        //                {
        //                    template = drTemp[0][Common.KEYNAME.RESPONSETEMPLATE].ToString();
        //                }
        //                Database objDatabase = new Database();
        //                DataTable dtacct = con.FillDataTable(Common.ConStr, "EBA_SCHEDULES_SELECTBYID", tran.Data["SCHEDULEID"]);

        //                //if (dtacct.Rows.Count > 0)
        //                //{
        //                string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
        //                condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
        //                condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "'";
        //                condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
        //                DataRow[] dr = Common.DBIOUTPUTDEFINESMS.Select(condition, "FIELDNO");
        //                if (dr.Length <= 0)
        //                {
        //                    tran.ErrorCode = Common.ERRORCODE.SYSTEM;
        //                    return false;
        //                }
        //                //for (int i = 0; i < dtacct.Rows.Count; i++)
        //                //{
        //                //if (dtacct.Rows[i][Common.KEYNAME.CCYID] != null || dtacct.Rows[i][Common.KEYNAME.CCYID].ToString().Equals("") == false)
        //                //{
        //                string msgContent = template;
        //                DataTable dtacctright = con.FillDataTable(Common.ConStr, "SMS_GETACCTRIGHT", tran.Data["ACCTNO"], tran.Data[Common.KEYNAME.IPCTRANCODE].ToString());

        //                for (int j = 0; j < dtacctright.Rows.Count; j++)
        //                {
        //                    if (dtacctright.Rows[j]["TypeID"].Equals("CTK"))
        //                    {
        //                        //bool isGetCCYID = objDatabase.DoStore(tran, "SMS_AUTOBALANCE_GETCCYID|2|" + dtacct.Rows[i]["ACCOUNTNO"].ToString());

        //                        //SCHEDULETYPE
        //                        //DataTable scheduletype = con.FillDataTable(Common.ConStr, "EBA_SCHEDULES_SELECTBYID", dtacctright.Rows[j][Common.KEYNAME.SCHEDULEID].ToString());
        //                        GetDataFromDatatable(tran, dtacct, 0);
        //                        if (tran.Data.ContainsKey("STATUS") == false)
        //                        {
        //                            tran.Data.Add("STATUS", "");

        //                        }
        //                        if (tran.Status == "1")
        //                        {
        //                            tran.Data["STATUS"] = "completed";
        //                        }
        //                        else if (tran.Status == "2")
        //                        {
        //                            tran.Data["STATUS"] = "error";
        //                        }
        //                        for (int b = 0; b < dr.Length; b++)
        //                        {
        //                            string tmp = dr[b]["VALUENAME"].ToString();
        //                            msgContent = msgContent.Replace("[" + dr[b]["FIELDNAME"].ToString() + "]", tran.Data[tmp].ToString());
        //                        }
        //                        DataTable dtUserid = con.FillDataTable(Common.ConStr, "EBA_USER_GETUSERBYUSERNAME", dtacctright.Rows[j][Common.KEYNAME.USERID].ToString());

        //                        if (dtUserid.Rows.Count > 0)
        //                        {
        //                            if (msgContent.Length > maxlen)
        //                            {
        //                                //vut case o lao co dau breakline tinh lenght ko chinh xac
        //                                msgContent = msgContent.Substring(0, maxlen - 15) + "...";
        //                            }
        //                            bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), dtUserid.Rows[0][Common.KEYNAME.PHONENO].ToString(), "", "", msgContent, Common.IPCWorkDate, "2", "T");
        //                        }
        //                    }

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
        //        tran.SetErrorInfo(ex);
        //        return false;
        //    }
        //    return true;
        //}
        public bool SendSweepingSMS(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                //minh add 26.11.2015:
                if (!bool.Parse(ConfigurationManager.AppSettings["SendSMSafterSweeping"]))
                {
                    Utility.ProcessLog.LogInformation("Don't send sms by config");
                    return true;
                }
                DataTable sweepinnginfor = con.FillDataTable(Common.ConStr, "eba_get_sweeping_infor_to_sendSMS", DateTime.Now.ToString("dd/MM/yyyy"),
                    tran.Data[Common.KEYNAME.SCHEDULEID].ToString(), tran.Data[Common.KEYNAME.CENACCTNO].ToString(), tran.Data["CONTRACTNO"].ToString(), tran.Data["IPCTRANSID"].ToString());
                string tranID = sweepinnginfor.Rows[0][Common.KEYNAME.IPCTRANSID].ToString();
                // Utility.ProcessLog.LogInformation("Send SMS infor " + tranID );
                string phoneno = sweepinnginfor.Rows[0][Common.KEYNAME.PHONENO].ToString();
                //Utility.ProcessLog.LogInformation("Send SMS infor " + tranID + "," + phoneno );

                if (FormatPhoneNo(phoneno).Equals("#"))
                {
                    Utility.ProcessLog.LogInformation("SMS schedule transfer Error: Invalid receiver phone:" + phoneno);
                    return false;
                }
                string msgContent = sweepinnginfor.Rows[0][Common.KEYNAME.MSGCONTENT].ToString();
                Utility.ProcessLog.LogInformation("Send SMS infor " + tranID + "," + phoneno + "," + msgContent);

                bool msgOut = SendMsgOut(tran, tranID, phoneno, "", "", msgContent, Common.IPCWorkDate, "2", "T");
                //bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), tran.Data["PhoneNo"].ToString(), "", "", msgContent, Common.IPCWorkDate, "2", "T");
                if (msgOut)
                {
                    Utility.ProcessLog.LogInformation("Send SMS success to phone " + phoneno);
                }
                else
                {
                    Utility.ProcessLog.LogInformation("Send SMS failed");
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
                return false;
            }
            return true;
        }
        public bool SendSMSSchedule(TransactionInfo tran)
        {
            Connection con = new Connection();
            try
            {
                if (tran.Data.ContainsKey("SCHEDULEID"))
                {
                    string lang = tran.Data.ContainsKey(Common.KEYNAME.LANG) ? tran.Data[Common.KEYNAME.LANG].ToString() : "en-US";
                    DataTable dtschedule = con.FillDataTable(Common.ConStr, "EBA_SCHEDULES_SELECTBYID", tran.Data["SCHEDULEID"]);
                    DataRow drschedule = dtschedule.NewRow();
                    drschedule = dtschedule.Rows[0];
                    if (drschedule["ISAPPROVED"].ToString().Equals(Common.KEYNAME.YES))
                    {
                        string template = "";
                        DataTable dtTemplate = con.FillDataTable(Common.ConStr, "IPC_GETSMSTEMPLATE", tran.Data[Common.KEYNAME.IPCTRANCODE].ToString(), "", lang);
                        DataRow[] drTemp = dtTemplate.Select();
                        if (drTemp.Length > 0)
                        {
                            template = drTemp[0][Common.KEYNAME.RESPONSETEMPLATE].ToString();
                        }

                        string condition = " SOURCEID = '" + tran.Data[Common.KEYNAME.SOURCEID].ToString() + "'";
                        condition += " AND DESTID = '" + tran.Data[Common.KEYNAME.DESTID].ToString() + "'";
                        condition += " AND IPCTRANCODE = '" + tran.Data[Common.KEYNAME.IPCTRANCODE].ToString() + "'";
                        condition += " AND ONLINE = " + (tran.Online == true ? "1" : "0");
                        DataRow[] dr = Common.DBIOUTPUTDEFINESMS.Select(condition, "FIELDNO");
                        if (dr.Length <= 0)
                        {
                            tran.ErrorCode = Common.ERRORCODE.SYSTEM;
                            return false;
                        }
                        string msgContent = template;
                        DataTable dtacctright = con.FillDataTable(Common.ConStr, "GETUSERSENDSMSSCHEDULETRANSFER", tran.Data["ACCTNO"]);

                        for (int j = 0; j < dtacctright.Rows.Count; j++)
                        {

                            GetDataFromDatatable(tran, dtschedule, 0);
                            if (tran.Data.ContainsKey(Common.KEYNAME.STATUS) == false)
                            {
                                tran.Data.Add(Common.KEYNAME.STATUS, "");

                            }
                            if (tran.Status == "1")
                            {
                                tran.Data[Common.KEYNAME.STATUS] = "Completed";
                            }
                            else if (tran.Status == "2")
                            {
                                if (bool.Parse(ConfigurationManager.AppSettings["Sendsmserrorsameasemail"]))
                                {
                                    DataTable dterror = con.FillDataTable(Common.ConStr, "ipc_get_errordes_by_ipctransid_to_send_sms", tran.Data[Common.KEYNAME.IPCTRANSID]);
                                    //Utility.ProcessLog.LogInformation("errordesce="+tran.Data[Common.KEYNAME.IPCTRANSID].ToString());
                                    tran.Data[Common.KEYNAME.STATUS] = "Error - " + dterror.Rows[0][Common.KEYNAME.ERRORDESC].ToString();
                                }
                                else
                                {
                                    tran.Data[Common.KEYNAME.STATUS] = "Error";
                                }
                            }
                            for (int b = 0; b < dr.Length; b++)
                            {
                                string tmp = dr[b]["VALUENAME"].ToString();
                                msgContent = msgContent.Replace("[" + dr[b]["FIELDNAME"].ToString() + "]", tran.Data[tmp].ToString());
                                //msgContent = msgContent.Replace("- en", "");
                            }
                            //if (msgContent.Length > maxlen)
                            //{
                            //    //vut case o lao co dau breakline tinh lenght ko chinh xac
                            //    msgContent = msgContent.Substring(0, maxlen - 15) + "...";
                            //}
                            bool msgOut = SendMsgOut(tran, tran.IPCTransID.ToString(), dtacctright.Rows[j][Common.KEYNAME.PHONENO].ToString(), "", "", msgContent, Common.IPCWorkDate, "2", "T");
                            if (msgOut) Utility.ProcessLog.LogInformation("======>  Send SMS schedule transfer success");
                            else Utility.ProcessLog.LogInformation("======>  Send SMS schedule transfer error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + tran.IPCTransID.ToString() + ")");
                tran.SetErrorInfo(ex);
                return false;
            }
            return true;
        }

        public static string FormatPhoneNo(string phoneNo)
        {
            string phpr = "95";
            try
            {
                phpr = ConfigurationManager.AppSettings["PhonePrefix"].ToString();
            }
            catch { }

            if (phoneNo.Length < 6 || phoneNo.Length > 13)
            {
                //throw new Exception("Invalid phone number format.");
                Utility.ProcessLog.LogInformation("Length of phoneno not in correct length(6,13)");
                return "#";
            }
            else
            {
                if (phoneNo.Substring(0, 2) == phpr) //84
                    return phoneNo;
                else
                    if (phoneNo.Substring(0, 3) == string.Format("+{0}", phpr)) //+84
                    return phoneNo.Remove(0, 1);
                else
                        if (phoneNo.Substring(0, 1) == "0")  //016,098
                    return phoneNo = "0" + phoneNo.Remove(0, 1);
                else
                    //throw new Exception("Invalid phone number format.");
                    return "#";
            }
        }

    }
}
