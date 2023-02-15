using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SMSProcessMsgMT
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
				new SMSProcessMsgVMG() 
			};
            #if(DEBUG)
            //System.Diagnostics.Debugger.Launch();
            #endif 
            ServiceBase.Run(ServicesToRun);
        }
    }
}
