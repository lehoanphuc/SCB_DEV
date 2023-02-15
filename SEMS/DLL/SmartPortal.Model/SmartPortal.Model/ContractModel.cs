using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartPortal.Model
{
    [Serializable()]
    public class ContractModel
    {
        public bool isNew = true;
        public string cusID { get; set; }
        public string cusCode { get; set; }
        public string cusType { get; set; }
        public string fullName { get; set; }
        public string shortName { get; set; }
        public string dob { get; set; }
        public string fax { get; set; }
        public string addrResident { get; set; }
        public string addrTemp { get; set; }
        public string gender { get; set; }
        public string nation { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string licenseType { get; set; }
        public string licenseId { get; set; }
        public string issueDate { get; set; }
        public string issuePlace { get; set; }
        public string desc { get; set; }
        public string job { get; set; }
        public string officeAddr { get; set; }
        public string officePhone { get; set; }
        //public string cfType { get; set; }
        public string cfCode { get; set; }
        public string branchId { get; set; }
        public string custBranchId { get; set; }
        public string contractNo { get; set; }
        public string contractType { get; set; }
        public string corpType { get; set; }
        public string structureId { get; set; }
        public string productId { get; set; }
        public string createDate { get; set; }
        public string endDate { get; set; }
        public string lastModify { get; set; }
        public string userCreate { get; set; }
        public string userLastModify { get; set; }
        public string userApprove { get; set; }
        public string status { get; set; }
        public string allAcct { get; set; }
        public string isSpecialMan { get; set; }
        public string isAutorenew { get; set; }
        public string regionCust { get; set; }
        public string townshipCust { get; set; }
        public string tranAlter { get; set; }
        public string walletID { get; set; }
    }
}
