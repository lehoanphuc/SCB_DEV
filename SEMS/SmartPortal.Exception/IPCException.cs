using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.ExceptionCollection
{
    public class IPCException : System.Exception
    {
         /// <summary>
       /// Set message error
       /// </summary>
       /// <param name="message"></param>
        public IPCException(string message):base(message)
        {
           
        }
       /// <summary>
       /// Set message error and exception
       /// </summary>
       /// <param name="message"></param>
       /// <param name="inner"></param>
        public IPCException(string message, System.Exception inner)
            : base(message,inner)
        {

        }
    }
}
