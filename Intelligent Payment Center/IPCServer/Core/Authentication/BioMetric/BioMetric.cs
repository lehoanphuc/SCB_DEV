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
    public class BioMetric
    {
        public Hashtable AuthenBioMetric(string userID, string bioToken, string deviceID, string uuid, string serviceID)
        {
            Hashtable htOut = new Hashtable();
            try
            {
                Connection dbObj = new Connection();
                DataTable dt = new DataTable();
                string encodedToken = Utility.Common.EncodeBioMetricToken(userID, deviceID, bioToken);
                dt = dbObj.FillDataTable(Common.ConStr, "IPC_AUTHENBIOMETRIC", userID, encodedToken, deviceID, uuid, serviceID);
                if (dt == null || dt.Rows.Count == 0)
                {
                    throw new Exception("Error connect server");
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
