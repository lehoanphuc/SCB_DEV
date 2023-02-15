using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPortal.Model
{
    public class FieldNameFilter
    {
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
        public Type Type { get; set; }
        public FieldNameFilter(string fieldName, Type type)
        {
            FieldName = fieldName;
            //DisplayName = displayName;
            Type = type;
        }
    }
}
