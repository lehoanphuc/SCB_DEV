using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.Data;
using DBConnection;
using Formatters;
using System.Collections;
using Interfaces;
using Schedules;
using System.Configuration;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;

namespace Notification
{
    public class Push
    {
        public bool SendPushAll(TransactionInfo tran, string parmlist)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Utility.ProcessLog.LogError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }
    }
}
