using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Configuration;
using System.Collections;

namespace SmartPortal.RemotingServices
{
    public static class AT
    {
         #region Variable

        private static ITransaction.AutoTrans dbTran;

        private static string urlIPCPTH;
        #endregion

        #region Constructor
        public static ITransaction.AutoTrans DBTRAN()
        {
            urlIPCPTH = ConfigurationManager.AppSettings.Get("IPCAT");

            if (dbTran == null)
            {
                dbTran = (ITransaction.AutoTrans)Activator.GetObject(typeof(ITransaction.AutoTrans), urlIPCPTH);

            }

            return dbTran;
        }
        #endregion
       
    }
}
