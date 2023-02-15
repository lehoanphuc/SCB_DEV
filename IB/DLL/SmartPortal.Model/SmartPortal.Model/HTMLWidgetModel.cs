using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class HTMLWidgetModel
    {
        private int _widgetID;

        public int WidgetID
        {
            get { return _widgetID; }
            set { _widgetID = value; }
        }
        private string _widgetContent;

        public string WidgetContent
        {
            get { return _widgetContent; }
            set { _widgetContent = value; }
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
