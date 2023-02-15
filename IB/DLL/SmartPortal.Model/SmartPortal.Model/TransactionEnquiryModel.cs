using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartPortal.Model
{
    public class TransactionEnquiryModel
    {
        private string transactionNumber;
        private string transactionDate;
        private string transactionName;
        private string phoneNumber;
        private string fullName;
        private string amount;
        private string bonus;
        private string amountFee;
        private string currency;
        private string status;

        public string TransactionNumber { get => transactionNumber; set => transactionNumber = value; }
        public string TransactionDate { get => transactionDate; set => transactionDate = value; }
        public string TransactionName { get => transactionName; set => transactionName = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string Amount { get => amount; set => amount = value; }
        public string Bonus { get => bonus; set => bonus = value; }
        public string AmountFee { get => amountFee; set => amountFee = value; }
        public string Currency { get => currency; set => currency = value; }
        public string Status { get => status; set => status = value; }
    }
}
