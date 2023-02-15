using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class LanguageModel
    {
        private string _langID;

        public string LangID
        {
            get { return _langID; }
            set { _langID = value; }
        }
        private string _langName;

        public string LangName
        {
            get { return _langName; }
            set { _langName = value; }
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
