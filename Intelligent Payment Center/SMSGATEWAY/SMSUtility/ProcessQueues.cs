using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Messaging;
using System.Configuration;
 
namespace SMSUtility
{
    public class ProcessQueues
    {
        #region Private variable
        private MessageQueue smsQueueIn = null;
        private MessageQueue smsQueueOut = null;
        #endregion

        public void SendToQueueIn(Hashtable dataInput)
        {
            try
            {
                smsQueueIn = new MessageQueue(ConfigurationManager.AppSettings["SMSQueueIn_Path"].ToString());
                Message msg = new Message(dataInput, new BinaryMessageFormatter());
                smsQueueIn.Send(msg);
                smsQueueIn.Dispose();
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        public void SendToQueueOut(Hashtable dataInput)
        {
            try
            {
                smsQueueOut = new MessageQueue(ConfigurationManager.AppSettings["SMSQueueOut_Path"].ToString());
                if (dataInput.ContainsKey("connectionString"))
                {
                    dataInput.Remove("connectionString");
                }
                Message msg = new Message(dataInput, new BinaryMessageFormatter());
                smsQueueOut.Send(msg);
                smsQueueOut.Dispose();
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        public Hashtable ReceiveFromQueueIn(ref int errorCode)
        {
            Hashtable msg = new Hashtable();
            try
            {
                smsQueueIn = new MessageQueue(ConfigurationManager.AppSettings["SMSQueueIn_Path"].ToString());
                smsQueueIn.Formatter = new BinaryMessageFormatter();
                msg = (Hashtable)smsQueueIn.Receive(new TimeSpan(0, 0, 1)).Body;
                smsQueueIn.Dispose();
            }
            catch (Exception ex)
            {
                //Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                errorCode = 1000;
            }
            return msg;
        }
        public Hashtable ReceiveFromQueueOut(ref int errorCode)
        {
            Hashtable msg = new Hashtable();
            try
            {
                smsQueueOut = new MessageQueue(ConfigurationManager.AppSettings["SMSQueueOut_Path"].ToString());
                smsQueueOut.Formatter = new BinaryMessageFormatter();
                msg = (Hashtable)smsQueueOut.Receive(new TimeSpan(0, 0, 1)).Body;
                smsQueueOut.Dispose();
                return msg;
            }
            catch (Exception ex)
            {
                //Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                errorCode = 1000;
            }
            return msg;
        }


    }
}
