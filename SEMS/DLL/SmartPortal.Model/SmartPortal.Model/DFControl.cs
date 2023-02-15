using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SmartPortal.Model
{
    public class DFTextBox : System.Web.UI.WebControls.TextBox
    {
        private string _vType = "";
        private string _vRegex = "";
        private int _vMinLen = 0;
        private bool _vRequire = false;
        private string _vName = "";
        public virtual string vType
        {
            get { return _vType; }
            set { _vType = value; }
        }
        public virtual string vRegex
        {
            get { return _vRegex; }
            set { _vRegex = value; }
        }
        public virtual int vMinLen
        {
            get { return _vMinLen; }
            set { _vMinLen = value; }
        }
        public virtual bool vRequire
        {
            get { return _vRequire; }
            set { _vRequire = value; }
        }
        public virtual string vName
        {
            get { return _vName; }
            set { _vName = value; }
        }
    }

    public class DFDropDownList : System.Web.UI.WebControls.DropDownList
    {
        private string _vType = "";
        private string _vRegex = "";
        private int _vMinLen = 0;
        private bool _vRequire = false;
        private string _vName = "";
        public virtual string vType
        {
            get { return _vType; }
            set { _vType = value; }
        }
        public virtual string vRegex
        {
            get { return _vRegex; }
            set { _vRegex = value; }
        }
        public virtual int vMinLen
        {
            get { return _vMinLen; }
            set { _vMinLen = value; }
        }
        public virtual bool vRequire
        {
            get { return _vRequire; }
            set { _vRequire = value; }
        }
        public virtual string vName
        {
            get { return _vName; }
            set { _vName = value; }
        }
    }
}
