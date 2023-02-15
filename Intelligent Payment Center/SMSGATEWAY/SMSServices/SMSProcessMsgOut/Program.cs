using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SMSProcessMsgOut
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new ProcessOut() 
			};
            #if(DEBUG)
            //24-04-2014 Edit by VuTran
            //System.Diagnostics.Debugger.Launch();
            #endif
            ServiceBase.Run(ServicesToRun);
        }
    }
}
