using System;
namespace SmartPortal.Model
{
    [Serializable]
    public class DocumentModel
    {
        private int documentID;
        private int no;
        private string documentCode;
        private string documentName;
        private string documentType;
        private string file;
        private string userID;
        private string custID;
        private string billerID;
        private int requestID;
        private string status;
        private string userCreated;
        private string dateCreated;
        private DateTime expiryDate;
        private string userModified;
        private DateTime lastModified;
        private string userApproved;
        private DateTime dateApproved;
        private bool isNew;
        private bool isUpdate;
        private string valueStatus;


        public int No { get => no; set => no = value; }
        public bool IsNew { get => isNew; set => isNew = value; }
        public string DocumentCode { get => documentCode; set => documentCode = value; }
        public string DocumentName { get => documentName; set => documentName = value; }
        public string DocumentType { get => documentType; set => documentType = value; }
        public string File { get => file; set => file = value; }
        public string UserId { get => userID; set => userID = value; }
        public string CustId { get => custID; set => custID = value; }
        public string BillerId { get => billerID; set => billerID = value; }
        public int RequestId { get => requestID; set => requestID = value; }
        public string Status { get => status; set => status = value; }
        public string UserCreated { get => userCreated; set => userCreated = value; }
        public string DateCreated { get => dateCreated; set => dateCreated = value; }
        public DateTime ExpiryDate { get => expiryDate; set => expiryDate = value; }
        public string UserModified { get => userModified; set => userModified = value; }
        public DateTime LastModified { get => lastModified; set => lastModified = value; }
        public string UserApproved { get => userApproved; set => userApproved = value; }
        public DateTime DateApproved { get => dateApproved; set => dateApproved = value; }
        public int DocumentID { get => documentID; set => documentID = value; }
        public bool IsUpdate { get => isUpdate; set => isUpdate = value; }
        public string ValueStatus { get => valueStatus; set => valueStatus = value; }

        public DocumentModel()
        {
        }
    }
}
