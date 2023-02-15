using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartPortal.Model
{
    [Serializable]
    public class CashBackModel
    {
        private int no;
        private string transNum;
        private string transDate;
        private string stt;

        private string f_Phone;
        private string f_Name;
        private string f_Wallet;
        private string t_Phone;
        private string t_Name;
        private string t_Wallet;
        private double cb_Amount;

        public int No { get => no; set => no = value; }
        public string TransNum { get => transNum; set => transNum = value; }
        public string TransDate { get => transDate; set => transDate = value; }
        public string Stt { get => stt; set => stt = value; }
        public string F_Phone { get => f_Phone; set => f_Phone = value; }
        public string F_Name { get => f_Name; set => f_Name = value; }
        public string F_Wallet { get => f_Wallet; set => f_Wallet = value; }
        public string T_Phone { get => t_Phone; set => t_Phone = value; }
        public string T_Name { get => t_Name; set => t_Name = value; }
        public string T_Wallet { get => t_Wallet; set => t_Wallet = value; }
        public double Cb_Amount { get => cb_Amount; set => cb_Amount = value; }
    }
}
