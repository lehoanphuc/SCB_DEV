using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class ErrorCodeModel
    {
        private int _errorCode;

        public int ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }
        private string _errorDesc;

        public string ErrorDesc
        {
            get { return _errorDesc; }
            set { _errorDesc = value; }
        }
    }
}
