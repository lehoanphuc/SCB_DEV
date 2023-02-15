using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartPortal.Model
{
    public class ContractLevelModel
    {
        private int contracLevelID;
        private string contracLevelCode;
        private string contracLevelName;
        private string status;
        private string userCreate;
        private string dateCreate;
        private string userModify;
        private string lastModify;
        private string userApprove;
        private string dateApprove;
        private int order;
        private int priority;

        public int ContracLevelID { get => contracLevelID; set => contracLevelID = value; }
        public string ContracLevelCode { get => contracLevelCode; set => contracLevelCode = value; }
        public string ContracLevelName { get => contracLevelName; set => contracLevelName = value; }
        public string Status { get => status; set => status = value; }
        public string UserCreate { get => userCreate; set => userCreate = value; }
        public string DateCreate { get => dateCreate; set => dateCreate = value; }
        public string UserModify { get => userModify; set => userModify = value; }
        public string LastModify { get => lastModify; set => lastModify = value; }
        public string UserApprove { get => userApprove; set => userApprove = value; }
        public string DateApprove { get => dateApprove; set => dateApprove = value; }
        public int Order { get => order; set => order = value; }
        public int Priority { get => priority; set => priority = value; }

    }
}
