using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmartPortal.Model
{
    public class ApprovalStructureBase : ClassBase
    {
        public string STRUCTUREID { get; set; }
        public string SHORTNAME { get; set; }
        public string DESC { get; set; }
        public string STATUS { get; set; }
        public string USERCREATED { get; set; }
        public string DATECREATED { get; set; }
        public string USERMODIFIED { get; set; }
        public string DATEMODIFIED { get; set; }
        public string STRUCTURENAME { get; set; }
        public string CONTRACTNO { get; set; }

        public ApprovalStructureBase()
        {
            STRUCTUREID = string.Empty;
            CONTRACTNO = string.Empty;
            STRUCTURENAME = string.Empty;
        }
        public PropertyInfo[] Delete()
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
                GetPropInfo(nameof(SHORTNAME)),
                GetPropInfo(nameof(DESC)),
                GetPropInfo(nameof(DATECREATED)),
                GetPropInfo(nameof(USERCREATED)),
            };
        }
        public PropertyInfo[] Update()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(STRUCTUREID)),
                GetPropInfo(nameof(SHORTNAME)),
                GetPropInfo(nameof(DESC)),
                GetPropInfo(nameof(STATUS)),
                GetPropInfo(nameof(DATEMODIFIED)),
                GetPropInfo(nameof(USERMODIFIED)),
            };
        }

        public PropertyInfo[] Detail()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(STRUCTUREID)),
            };
        }

        public PropertyInfo[] Search()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(STRUCTURENAME)),
                GetPropInfo(nameof(CONTRACTNO))
            };
        }
    }
}
