using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartPortal.Model
{
    public class ResultValidate
    {
        public bool result { get; set; }
        public string message { get; set; }
        public ResultValidate()
        {
            result = false;
            message = string.Empty;
        }
    }
}
