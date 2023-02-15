using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBConnection;
using Utility;
using System.Data;
using System.Configuration;

namespace Authentication
{
    public class SMSOTP
    {
        int smsOTPLen = 0;

        public SMSOTP()
        {
            try
            {
                smsOTPLen = int.Parse(ConfigurationManager.AppSettings["SMSOTPLength"].ToString());
            }
            catch
            {
                smsOTPLen = 8;
            }
        }

        public string AuthenOTPSMS(string userid, string OTP,string trancode)
        {
            try
            {

                Connection dbObj = new Connection();
                DataTable rs = new DataTable();
                rs = dbObj.FillDataTable(Common.ConStr, "IPC_AUTHENOTP", userid, OTP,trancode);
                if (rs == null || rs.Rows.Count == 0)
                {
                    return "Error connect server";
                }
                else
                {
                    return rs.Rows[0]["ERRORDESC"].ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool GenSMSOTP(TransactionInfo tran, string userid)
        {
            try
            {
                string deviceid = "";
                if (tran.Data.ContainsKey(userid))
                {
                    deviceid = tran.Data[userid].ToString();
                }
                else
                {
                    deviceid = userid;
                }
                Random ran = new Random();
                byte[] auCode = new byte[smsOTPLen];
                string strAuCode = string.Empty;
                for (int i = 0; i < smsOTPLen; i++)
                {
                    auCode[i] = (byte)ran.Next(10);
                    strAuCode = strAuCode + auCode[i].ToString();
                }
                //strAuCode = ASCIIEncoding.ASCII.GetString(auCode);
                InsertOTP(deviceid, strAuCode, int.Parse(Common.OTPLifeTime.ToString()));
                tran.Data[Common.KEYNAME.SMSOTP] = strAuCode;
                return true;
            }
            catch(Exception ex)
            {
                tran.SetErrorInfo(ex);
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        #region Sub-Function
        private void InsertOTP(string userid, string OTP, int lifetime)
        {
            Connection dbObj = new Connection();
            try
            {
                dbObj.ExecuteNonquery(Common.ConStr, "IPC_OTPDETAILINSERT", userid, OTP, lifetime);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
