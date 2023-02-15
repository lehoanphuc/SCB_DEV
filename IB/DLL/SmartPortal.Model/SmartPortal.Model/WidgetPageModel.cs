using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class WidgetPageModel
    {
        private string _widgetPageID;

        public string WidgetPageID
        {
            get { return _widgetPageID; }
            set { _widgetPageID = value; }
        }
        private int _portalID;

        public int PortalID
        {
            get { return _portalID; }
            set { _portalID = value; }
        }
        private string _pageID;

        public string PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }
        private string _widgetID;

        public string WidgetID
        {
            get { return _widgetID; }
            set { _widgetID = value; }
        }
        private string _position;

        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private int _order;

        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }
    }
}
