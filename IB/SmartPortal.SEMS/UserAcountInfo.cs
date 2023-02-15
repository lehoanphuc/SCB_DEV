using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartPortal.Model
{
    public class UserContractInfo
    {
        public string order { get; set; }
        public string cusCode { get; set; }
        public string fullName { get; set; }
        public string level { get; set; }
        public string group { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string address { get; set; }
        public bool isRenderUserName { get; set; }
        
        public string IBUserName { get; set; }
        public string IBUserPass { get; set; }
        public string SMSPhone { get; set; }
        public string SMSDefaultAcctno { get; set; }
        public string SMSDefaultLang { get; set; }
        public string isDefault { get; set; }
        public string SMSPinCode { get; set; }
        public string MBPhone { get; set; }
        public string MBPass { get; set; }
        public string MBPinCode { get; set; }
        public string PHOPhone { get; set; }
        public string PHOPass { get; set; }
        public string PHODefaultAcctno { get; set; }
        public string pwdreset { get; set; }

    }
}
