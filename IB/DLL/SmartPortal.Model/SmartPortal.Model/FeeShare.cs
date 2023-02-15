using System;
namespace SmartPortal.Model
{
    [Serializable]
    public class FeeShare
    {
        private string feeShareTypeID;
        private string billerID;
        private string billerName;
        private string isLadder;

        public FeeShare() { }
        public FeeShare(string feeShareTypeID, string billerID, string billerName, string isLadder)
        {
            this.feeShareTypeID = feeShareTypeID;
            this.billerID = billerID;
            this.billerName = billerName;
            this.isLadder = isLadder;
        }

        public string FeeShareTypeID { get => feeShareTypeID; set => feeShareTypeID = value; }
        public string BillerID { get => billerID; set => billerID = value; }
        public string IsLadder { get => isLadder; set => isLadder = value; }
        public string BillerName { get => billerName; set => billerName = value; }
    }
}
