using System.Reflection;

namespace SmartPortal.Model
{
    public class AWDetailModel : ClassBase
    {
        public string WORKFLOWID { get; set; }
        public string ORD { get; set; }
        public string FORMULA { get; set; }
        public bool ISSEQUENCE { get; set; }
        public string USERLEVEL { get; set; }
        public string DESC { get; set; }

        public PropertyInfo[] Delete()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(WORKFLOWID)),
                GetPropInfo(nameof(USERLEVEL)),
            };
        }

        public PropertyInfo[] Insert()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(WORKFLOWID)),
                GetPropInfo(nameof(USERLEVEL)),
                GetPropInfo(nameof(ORD)),
                GetPropInfo(nameof(FORMULA)),
                GetPropInfo(nameof(DESC)),
                GetPropInfo(nameof(ISSEQUENCE)),
            };
        }

        public PropertyInfo[] GetAll()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(WORKFLOWID)),
            };
        }
    }
}
