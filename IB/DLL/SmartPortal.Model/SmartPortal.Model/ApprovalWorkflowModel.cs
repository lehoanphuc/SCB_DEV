using System.Reflection;

namespace SmartPortal.Model
{
    public class ApprovalWorkflowModel : ClassBase
    {
        public string WORKFLOWID { get; set; }
        public string CONTRACTNO { get; set; }
        public string ACCTNO { get; set; }
        public string CCYID { get; set; }
        public string TRANCODE { get; set; }
        public string FROMLIMIT { get; set; }
        public string TOLIMIT { get; set; }
        public string ISAOT { get; set; }
        public string DATECREATED { get; set; }
        public string USERCREATED { get; set; }
        public string DATEMODIFIED { get; set; }
        public string USERMODIFIED { get; set; }
        public string FULLNAME { get; set; }
        public string USERID { get; set; }
        public string STRUCTUREID { get; set; }
        public string USERLEVEL { get; set; }
        public string LICENSEID { get; set; }
        public string CUSTCODE { get; set; }
        public string HASGROUP { get; set; }
        public string NEEDAPPROVE { get; set; }


        public PropertyInfo[] Search()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(TRANCODE)),
                GetPropInfo(nameof(CONTRACTNO)),
                GetPropInfo(nameof(NEEDAPPROVE)),
                GetPropInfo(nameof(ACCTNO)),
                GetPropInfo(nameof(CCYID)),
            };
        }

        public PropertyInfo[] Delete()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(WORKFLOWID)),
            };
        }

        public PropertyInfo[] Insert()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(WORKFLOWID)),
                GetPropInfo(nameof(CONTRACTNO)),
                GetPropInfo(nameof(ACCTNO)),
                GetPropInfo(nameof(CCYID)),
                GetPropInfo(nameof(TRANCODE)),
                GetPropInfo(nameof(FROMLIMIT)),
                GetPropInfo(nameof(TOLIMIT)),
                GetPropInfo(nameof(ISAOT)),
                GetPropInfo(nameof(NEEDAPPROVE)),
                GetPropInfo(nameof(DATECREATED)),
                GetPropInfo(nameof(USERCREATED)),
            };
        }

        public PropertyInfo[] Update()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(WORKFLOWID)),
                GetPropInfo(nameof(FROMLIMIT)),
                GetPropInfo(nameof(TOLIMIT)),
                GetPropInfo(nameof(ISAOT)),
                GetPropInfo(nameof(NEEDAPPROVE)),
                GetPropInfo(nameof(DATEMODIFIED)),
                GetPropInfo(nameof(USERMODIFIED)),
            };
        }

        public PropertyInfo[] GetUserID()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(CONTRACTNO)),
                GetPropInfo(nameof(FULLNAME)),
                GetPropInfo(nameof(LICENSEID)),
                GetPropInfo(nameof(CUSTCODE)),
                GetPropInfo(nameof(HASGROUP)),
            };
        }

        public PropertyInfo[] GetAcct()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(CONTRACTNO)),
            };
        }

        public PropertyInfo[] GetUserGroup()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(CONTRACTNO)),
                GetPropInfo(nameof(STRUCTUREID)),
                GetPropInfo(nameof(USERLEVEL)),
            };
        }

        public PropertyInfo[] GetGroup()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(STRUCTUREID)),
                GetPropInfo(nameof(USERLEVEL)),
            };
        }

        public PropertyInfo[] Detail()
        {
            return new PropertyInfo[] {
                GetPropInfo(nameof(IPCTRANCODE)),
                GetPropInfo(nameof(SOURCEID)),
                GetPropInfo(nameof(WORKFLOWID)),
            };
        }
    }
}
