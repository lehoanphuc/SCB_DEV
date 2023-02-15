using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class MenuModel
    {
        private string _menuID;

        public string MenuID
        {
            get { return _menuID; }
            set { _menuID = value; }
        }
        private string _menuTitle;

        public string MenuTitle
        {
            get { return _menuTitle; }
            set { _menuTitle = value; }
        }
        private string _menuLang;

        public string MenuLang
        {
            get { return _menuLang; }
            set { _menuLang = value; }
        }
        private string _menuLink;

        public string MenuLink
        {
            get { return _menuLink; }
            set { _menuLink = value; }
        }
        private string _menuParent;

        public string MenuParent
        {
            get { return _menuParent; }
            set { _menuParent = value; }
        }
        private int _menuOrder;

        public int MenuOrder
        {
            get { return _menuOrder; }
            set { _menuOrder = value; }
        }
        private bool _isPublished;

        public bool IsPublished
        {
            get { return _isPublished; }
            set { _isPublished = value; }
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

        private string _pageName;

        public string PageName
        {
            get { return _pageName; }
            set { _pageName = value; }
        }


        private string _serviceID;

        public string ServiceID
        {
            get { return _serviceID; }
            set { _serviceID = value; }
        }


    }
}
