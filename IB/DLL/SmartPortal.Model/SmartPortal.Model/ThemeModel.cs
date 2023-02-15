using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class ThemeModel
    {
        private int _themeID;

        public int ThemeID
        {
            get { return _themeID; }
            set { _themeID = value; }
        }
        private string _themeName;

        public string ThemeName
        {
            get { return _themeName; }
            set { _themeName = value; }
        }
        private string _themeDescription;

        public string ThemeDescription
        {
            get { return _themeDescription; }
            set { _themeDescription = value; }
        }
        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
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

        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
    }
}
