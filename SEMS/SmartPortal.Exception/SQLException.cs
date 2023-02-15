using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.ExceptionCollection
{
   public class SQLException:System.Exception
    {
       /// <summary>
       /// Set message error
       /// </summary>
       /// <param name="message"></param>
        public SQLException(string message):base(message)
        {
           
        }
       /// <summary>
       /// Set message error and exception
       /// </summary>
       /// <param name="message"></param>
       /// <param name="inner"></param>
        public SQLException(string message,System.Exception inner)
            : base(message,inner)
        {

        }
    }
}
