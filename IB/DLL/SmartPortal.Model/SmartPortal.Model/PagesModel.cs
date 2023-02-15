using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class PagesModel
    {
        private string _pageID;

        public string PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }


        private string _pageName;

        public string PageName
        {
            get { return _pageName; }
            set { _pageName = value; }
        }

        private string _masterPage;

        public string MasterPage
        {
            get { return _masterPage; }
            set { _masterPage = value; }
        }


        private string _theme;

        public string Theme
        {
            get { return _theme; }
            set { _theme = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private Boolean _isShow;

        public Boolean IsShow
        {
            get { return _isShow; }
            set { _isShow = value; }
        } 
        private string _isDefault;
        public string IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }
        private string _isApprove;
        public string IsApprove
        {
            get { return _isApprove; }
            set { _isApprove = value; }
        }
        private string _isNotification;
        public string IsNotification
        {
            get { return _isNotification; }
            set { _isNotification = value; }
        }
        private string _isSchedule;
        public string IsSchedule
        {
            get { return _isSchedule; }
            set { _isSchedule = value; }
        }
        private string _isTemplate;
        public string IsTemplate
        {
            get { return _isTemplate; }
            set { _isTemplate = value; }
        }
        //  isReceive     isProductFee    
        //  isReport      isViewReport 
        //  isbeneficiary isReversal    
        //  string  trancode, string linkApprove,
        private string _isReceive;
        public string IsReceive
        {
            get { return _isReceive; }
            set { _isReceive = value; }
        }
        private string _isProductFee;
        public string IsProductFee
        {
            get { return _isProductFee; }
            set { _isProductFee = value; }
        }
        private string _isReport;
        public string IsReport
        {
            get { return _isReport; }
            set { _isReport = value; }
        }
        private string _isViewReport;
        public string IsViewReport
        {
            get { return _isViewReport; }
            set { _isViewReport = value; }
        }
        private string _isbeneficiary;
        public string Isbeneficiary
        {
            get { return _isbeneficiary; }
            set { _isbeneficiary = value; }
        }
        private string _isReversal; 
        public string IsReversal  
        {
            get { return _isReversal; }
            set { _isReversal = value; } 
        } 
        private string _trancode;
         
        public string Trancode
        {
            get { return _trancode; }
            set { _trancode = value; }
        }
        private string _linkApprove;
        public string LinkApprove
        { 
            get { return _linkApprove; }
            set { _linkApprove = value; }
        }

        private int _masterPageID;

        public int MasterPageID
        {
            get { return _masterPageID; }
            set { _masterPageID = value; }
        }
        private int _themeID;

        public int ThemeID
        {
            get { return _themeID; }
            set { _themeID = value; }
        }

        private int _subSystem;

        public int SubSystem
        {
            get { return _subSystem; }
            set { _subSystem = value; }
        }
        private string _userModified;

        public string UserModified
        {
            get { return _userModified; }
            set { _userModified = value; }
        }

        private string _dateModified;

        public string DateModified
        {
            get { return _dateModified; }
            set { _dateModified = value; }
        }
        private string _action;

        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        private string _pageReference;

        public string PageReference
        {
            get { return _pageReference; }
            set { _pageReference = value; }
        }

        private string _serviceID;

        public string ServiceID
        {
            get { return _serviceID; }
            set { _serviceID = value; }
        }

    }
}
