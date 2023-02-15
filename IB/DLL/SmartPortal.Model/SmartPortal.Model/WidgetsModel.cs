using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class WidgetsModel
    {
        private string _widgetID;

        public string WidgetID
        {
            get { return _widgetID; }
            set { _widgetID = value; }
        }
        private string _widgetTitle;

        public string WidgetTitle
        {
            get { return _widgetTitle; }
            set { _widgetTitle = value; }
        }

        private string _widgetLink;

        public string WidgetLink
        {
            get { return _widgetLink; }
            set { _widgetLink = value; }
        }
        private string _iconPath;

        public string IconPath
        {
            get { return _iconPath; }
            set { _iconPath = value; }
        }
        private bool _enableTheme;

        public bool EnableTheme
        {
            get { return _enableTheme; }
            set { _enableTheme = value; }
        }
        private bool _showTitle;

        public bool ShowTitle
        {
            get { return _showTitle; }
            set { _showTitle = value; }
        }
        private bool _isPublish;

        public bool IsPublish
        {
            get { return _isPublish; }
            set { _isPublish = value; }
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

        private string _langID;

        public string LangID
        {
            get { return _langID; }
            set { _langID = value; }
        }
    }
}
