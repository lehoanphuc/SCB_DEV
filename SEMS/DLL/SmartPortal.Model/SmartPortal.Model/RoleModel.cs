using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class RoleModel
    {
        private int _roleID;

        public int RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }
        private string _serviceID;

        public string ServiceID
        {
            get { return _serviceID; }
            set { _serviceID = value; }
        }

        private string _userType;

        public string UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }
        private string _RoleType;
        public string RoleType
        {
            get { return _RoleType; }
            set { _RoleType = value; }
        }

        private string _roleName;

        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }
        private string _roleDescription;

        public string RoleDescription
        {
            get { return _roleDescription; }
            set { _roleDescription = value; }
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
    }
}
