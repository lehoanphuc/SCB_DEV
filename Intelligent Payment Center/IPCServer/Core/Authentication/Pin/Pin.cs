using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBConnection;
using Utility;
using System.Data;
using System.Configuration;

namespace Authentication
{
    public class PinPass
    {
        public Hashtable AuthenPP(string userID, string authenCode, string authenType,string serviceid)
        {
            Hashtable htOut = new Hashtable();
            try
            {
                Connection dbObj = new Connection();
                DataTable dt = new DataTable();
                dt = dbObj.FillDataTable(Common.ConStr, "IPC_AUTHENPP", userID, authenCode, authenType, serviceid);
                if (dt == null || dt.Rows.Count == 0)
                {
                    throw new Exception("Error connect to authentication server");
                }
                else
                {
                    htOut.Add(Common.KEYNAME.ERRORCODE, dt.Rows[0]["ERRORCODE"].ToString());
                    htOut.Add(Common.KEYNAME.ERRORDESC, dt.Rows[0]["ERRORDESC"].ToString());
                }
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw new Exception(ex.Message);
            }
            return htOut;
        }
    }
}
