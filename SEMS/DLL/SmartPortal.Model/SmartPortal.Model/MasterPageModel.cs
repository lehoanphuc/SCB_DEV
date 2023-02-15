using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class MasterPageModel
    {
        private int _masterPageID;

        public int MasterPageID
        {
            get { return _masterPageID; }
            set { _masterPageID = value; }
        }
        private string _masterPageName;

        public string MasterPageName
        {
            get { return _masterPageName; }
            set { _masterPageName = value; }
        }
        private string _masterPagePath;

        public string MasterPagePath
        {
            get { return _masterPagePath; }
            set { _masterPagePath = value; }
        }
        private string _masterPageImage;

        public string MasterPageImage
        {
            get { return _masterPageImage; }
            set { _masterPageImage = value; }
        }
    }
}
