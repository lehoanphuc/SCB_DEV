using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmartPortal.Model
{
    public abstract class ClassBase
    {
        public string IPCTRANCODE { get; set; }
        public string SOURCEID { get; set; }

        protected PropertyInfo GetPropInfo(string PropName)
        {
            return GetType().GetProperty(PropName);
        }
    }
}
