using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmartPortal.Model
{
    public class ASGroupModel : ClassBase
    {
        public string STRUCTUREID { get; set; }
        public string USERLEVEL { get; set; }
        public string GROUPID { get; set; }
        public string SHORTNAME { get; set; }
        public string DESC { get; set; }
        public PropertyInfo[] GetAll()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(STRUCTUREID))
            };
        }

        public PropertyInfo[] Insert()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(STRUCTUREID)),
                GetPropInfo(nameof(USERLEVEL)),
                GetPropInfo(nameof(GROUPID)),
                GetPropInfo(nameof(SHORTNAME)),
                GetPropInfo(nameof(DESC)),
            };
        }

        public PropertyInfo[] Delete()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(STRUCTUREID)),
                GetPropInfo(nameof(USERLEVEL)),
                GetPropInfo(nameof(GROUPID)),
            };
        }
    }
}
