using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SMSUtility
{
    public class ConnectIPC
    {
        public Hashtable DoStore(ITransaction.AutoTrans autostran, string storeName, object[] para)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable input = new Hashtable();
                input.Add(Common.KEYNAME.IPCTRANCODE, "IB000500");
                input.Add(Common.KEYNAME.SOURCEID, "IB");
                input.Add(Common.KEYNAME.STORENAME, storeName);
                input.Add(Common.KEYNAME.PARA, para);
                result = autostran.ProcessTransHAS(input);

            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }
        public string IPCProcessSMSMsg(ITransaction.AutoTrans autoStran,string msgID, string receivedPhone, string msgDate,
                                string msgTime, string msgContent, string prefix)
        {
            string result = "";
            try
            {
                string msgInput = "SMS" + " " + receivedPhone.ToString() + " " + msgDate.ToString() + " " + msgTime.ToString() + " " + msgID.ToString() + " " + msgContent.ToString();
                result = autoStran.ProcessTransSMS(msgInput);
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return result; 
        }
    }
}
