using System;

namespace SmartPortal.Model
{
	public class UserPolicyModel
	{
		private int _policyid;

		private string _serviceID;

		private string _descr;

		private bool _isdefault;

		private string _effrom;

		private string _efto;

		private int _pwdhis;

		private int _pwdagemax;

		private int _minpwdlen;

		private bool _pwdcplx;

		private bool _pwdcplxlc;

		private bool _pwdcplxuc;

		private bool _pwdcplxsc;

		private bool _pwdcplxsn;

		private bool _timelginrequire;

		private string _lginfr;

		private string _lginto;

		private int _lkoutthrs;

		private int _resetlkout;

		public int policyid
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

		public string serviceID
		{
			get
			{
				return this._serviceID;
			}
			set
			{
				this._serviceID = value;
			}
		}

		public string descr
		{
			get
			{
				return this._descr;
			}
			set
			{
				this._descr = value;
			}
		}

		public bool isdefault
		{
			get
			{
				return this._isdefault;
			}
			set
			{
				this._isdefault = value;
			}
		}

		public string effrom
		{
			get
			{
				return this._effrom;
			}
			set
			{
				this._effrom = value;
			}
		}

		public string efto
		{
			get
			{
				return this._efto;
			}
			set
			{
				this._efto = value;
			}
		}

		public int pwdhis
		{
			get
			{
				return this._pwdhis;
			}
			set
			{
				this._pwdhis = value;
			}
		}

		public int pwdagemax
		{
			get
			{
				return this._pwdagemax;
			}
			set
			{
				this._pwdagemax = value;
			}
		}

		public int minpwdlen
		{
			get
			{
				return this._minpwdlen;
			}
			set
			{
				this._minpwdlen = value;
			}
		}

		public bool pwdcplx
		{
			get
			{
				return this._pwdcplx;
			}
			set
			{
				this._pwdcplx = value;
			}
		}

		public bool pwdcplxlc
		{
			get
			{
				return this._pwdcplxlc;
			}
			set
			{
				this._pwdcplxlc = value;
			}
		}

		public bool pwdcplxuc
		{
			get
			{
				return this._pwdcplxuc;
			}
			set
			{
				this._pwdcplxuc = value;
			}
		}

		public bool pwdcplxsc
		{
			get
			{
				return this._pwdcplxsc;
			}
			set
			{
				this._pwdcplxsc = value;
			}
		}

		public bool pwdcplxsn
		{
			get
			{
				return this._pwdcplxsn;
			}
			set
			{
				this._pwdcplxsn = value;
			}
		}

		public bool timelginrequire
		{
			get
			{
				return this._timelginrequire;
			}
			set
			{
				this._timelginrequire = value;
			}
		}

		public string lginfr
		{
			get
			{
				return this._lginfr;
			}
			set
			{
				this._lginfr = value;
			}
		}

		public string lginto
		{
			get
			{
				return this._lginto;
			}
			set
			{
				this._lginto = value;
			}
		}

		public int lkoutthrs
		{
			get
			{
				return this._lkoutthrs;
			}
			set
			{
				this._lkoutthrs = value;
			}
		}

		public int resetlkout
		{
			get
			{
				return this._resetlkout;
			}
			set
			{
				this._resetlkout = value;
			}
		}
	}
}
