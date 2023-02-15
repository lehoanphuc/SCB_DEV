using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class UsersModel
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _branch;

        public string Branch
        {
            get { return _branch; }
            set { _branch = value; }
        }

        private string _level;

        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        private string _middleName;

        public string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; }
        }
        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        private string _FullName;

        public string FullName
		{
            get { return _FullName; }
            set { _FullName = value; }
        }
        private int _gender;

        public int Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _birthday;

        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        private string _lastLoginTime;

        public string LastLoginTime
        {
            get { return _lastLoginTime; }
            set { _lastLoginTime = value; }
        }
        private int _roleID;

        public int RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }
        private string _roleName;

        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }

        private int _Status;

        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
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
                private string _UUID;

        private string _policyid;
        public string UUID
        {
            get
            {
                return this._UUID;
            }
            set
            {
                this._UUID = value;
            }
        }

        public string policyid
        {
            get
            {
                return this._policyid;
            }
            set
            {
                this._policyid = value;
            }
        }
        private string _UserType;
		public string UserType
		{
            get
            {
                return this._UserType;
            }
            set
            {
                this._UserType = value;
            }
        }
        private string _BankID;
		public string BankID
		{
            get
            {
                return this._BankID;
            }
            set
            {
                this._BankID = value;
            }
        }
        private string _UserCreated;
		public string UserCreated
		{
            get
            {
                return this._UserCreated;
            }
            set
            {
                this._UserCreated = value;
            }
        }
        private string _deptId;
		public string deptId
		{
            get
            {
                return this._deptId;
            }
            set
            {
                this._deptId = value;
            }
        }
        private bool _isSuperAdmin;
		public bool isSuperAdmin
		{
            get
            {
                return this._isSuperAdmin;
            }
            set
            {
                this._isSuperAdmin = value;
            }
        }
    }
    public class UsersSEMSModel
	{
		private string _userName;

		private string _branch;

		private string _level;

		private string _password;

		private string _firstName;

		private string _middleName;

		private string _lastName;

		private string _FullName;

		private string _gender;

		private string _address;

		private string _email;

		private string _birthday;

		private string _phone;

		private string _lastLoginTime;

		private int _roleID;

		private string _roleName;

		private int _Status;

		private string _userModified;

		private string _dateModified;

		private string _UUID;

		private DateTime _Dateexpire;

		private int _Failnumber;

		private DateTime _ExpireTime;

		private DateTime _SystemTime;

		private int _IsLogin;

		private int _pwdage;

		private int _lastlogin;

		public string UserName
		{
			get
			{
				return this._userName;
			}
			set
			{
				this._userName = value;
			}
		}

		public string Branch
		{
			get
			{
				return this._branch;
			}
			set
			{
				this._branch = value;
			}
		}

		public string Level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
			}
		}

		public string FirstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				this._firstName = value;
			}
		}

		public string MiddleName
		{
			get
			{
				return this._middleName;
			}
			set
			{
				this._middleName = value;
			}
		}

		public string LastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				this._lastName = value;
			}
		}
		public string FullName
		{
			get
			{
				return this._FullName;
			}
			set
			{
				this._FullName = value;
			}
		}

		public string Gender
		{
			get
			{
				return this._gender;
			}
			set
			{
				this._gender = value;
			}
		}

		public string Address
		{
			get
			{
				return this._address;
			}
			set
			{
				this._address = value;
			}
		}

		public string Email
		{
			get
			{
				return this._email;
			}
			set
			{
				this._email = value;
			}
		}

		public string Birthday
		{
			get
			{
				return this._birthday;
			}
			set
			{
				this._birthday = value;
			}
		}

		public string Phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				this._phone = value;
			}
		}

		public string LastLoginTime
		{
			get
			{
				return this._lastLoginTime;
			}
			set
			{
				this._lastLoginTime = value;
			}
		}

		public int RoleID
		{
			get
			{
				return this._roleID;
			}
			set
			{
				this._roleID = value;
			}
		}

		public string RoleName
		{
			get
			{
				return this._roleName;
			}
			set
			{
				this._roleName = value;
			}
		}

		public int Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}

		public string UserModified
		{
			get
			{
				return this._userModified;
			}
			set
			{
				this._userModified = value;
			}
		}

		public string DateModified
		{
			get
			{
				return this._dateModified;
			}
			set
			{
				this._dateModified = value;
			}
		}

		public string UUID
		{
			get
			{
				return this._dateModified;
			}
			set
			{
				this._dateModified = value;
			}
		}

		public DateTime Dateexpire
		{
			get
			{
				return this._Dateexpire;
			}
			set
			{
				this._Dateexpire = value;
			}
		}

		public int Failnumber
		{
			get
			{
				return this._Failnumber;
			}
			set
			{
				this._Failnumber = value;
			}
		}

		public DateTime ExpireTime
		{
			get
			{
				return this._ExpireTime;
			}
			set
			{
				this._ExpireTime = value;
			}
		}

		public DateTime SystemTime
		{
			get
			{
				return this._SystemTime;
			}
			set
			{
				this._SystemTime = value;
			}
		}

		public int IsLogin
		{
			get
			{
				return this._IsLogin;
			}
			set
			{
				this._IsLogin = value;
			}
		}

		public int pwdage
		{
			get
			{
				return this._pwdage;
			}
			set
			{
				this._pwdage = value;
			}
		}

		public int lastlogin
		{
			get
			{
				return this._lastlogin;
			}
			set
			{
				this._lastlogin = value;
			}
		}
	}
    public class UsersIBModel
	{
		private string _userName;

		private string _branch;

		private string _level;

		private string _password;

		private string _firstName;

		private string _middleName;

		private string _lastName;

		private string _gender;

		private string _address;

		private string _email;

		private string _birthday;

		private string _phone;

		private string _lastLoginTime;

		private int _roleID;

		private string _roleName;

		private string _Status;

		private string _userModified;

		private string _dateModified;

		private string _UUID;

		private DateTime _Dateexpire;

		private int _Failnumber;

		private DateTime _ExpireTime;

		private DateTime _SystemTime;

		private int _IsLogin;

		private int _pwdage;

		private int _lastlogin;

		public string UserName
		{
			get
			{
				return this._userName;
			}
			set
			{
				this._userName = value;
			}
		}

		public string Branch
		{
			get
			{
				return this._branch;
			}
			set
			{
				this._branch = value;
			}
		}

		public string Level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
			}
		}

		public string FirstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				this._firstName = value;
			}
		}

		public string MiddleName
		{
			get
			{
				return this._middleName;
			}
			set
			{
				this._middleName = value;
			}
		}

		public string LastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				this._lastName = value;
			}
		}

		public string Gender
		{
			get
			{
				return this._gender;
			}
			set
			{
				this._gender = value;
			}
		}

		public string Address
		{
			get
			{
				return this._address;
			}
			set
			{
				this._address = value;
			}
		}

		public string Email
		{
			get
			{
				return this._email;
			}
			set
			{
				this._email = value;
			}
		}

		public string Birthday
		{
			get
			{
				return this._birthday;
			}
			set
			{
				this._birthday = value;
			}
		}

		public string Phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				this._phone = value;
			}
		}

		public string LastLoginTime
		{
			get
			{
				return this._lastLoginTime;
			}
			set
			{
				this._lastLoginTime = value;
			}
		}

		public int RoleID
		{
			get
			{
				return this._roleID;
			}
			set
			{
				this._roleID = value;
			}
		}

		public string RoleName
		{
			get
			{
				return this._roleName;
			}
			set
			{
				this._roleName = value;
			}
		}

		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}

		public string UserModified
		{
			get
			{
				return this._userModified;
			}
			set
			{
				this._userModified = value;
			}
		}

		public string DateModified
		{
			get
			{
				return this._dateModified;
			}
			set
			{
				this._dateModified = value;
			}
		}

		public string UUID
		{
			get
			{
				return this._dateModified;
			}
			set
			{
				this._dateModified = value;
			}
		}

		public DateTime Dateexpire
		{
			get
			{
				return this._Dateexpire;
			}
			set
			{
				this._Dateexpire = value;
			}
		}

		public int Failnumber
		{
			get
			{
				return this._Failnumber;
			}
			set
			{
				this._Failnumber = value;
			}
		}

		public DateTime ExpireTime
		{
			get
			{
				return this._ExpireTime;
			}
			set
			{
				this._ExpireTime = value;
			}
		}

		public DateTime SystemTime
		{
			get
			{
				return this._SystemTime;
			}
			set
			{
				this._SystemTime = value;
			}
		}

		public int IsLogin
		{
			get
			{
				return this._IsLogin;
			}
			set
			{
				this._IsLogin = value;
			}
		}

		public int pwdage
		{
			get
			{
				return this._pwdage;
			}
			set
			{
				this._pwdage = value;
			}
		}

		public int lastlogin
		{
			get
			{
				return this._lastlogin;
			}
			set
			{
				this._lastlogin = value;
			}
		}
	}



}

