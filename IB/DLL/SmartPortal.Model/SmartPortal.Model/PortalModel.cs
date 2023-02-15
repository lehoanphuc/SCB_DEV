using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class PortalModel
    {
        private int _PortalID;

        public int PortalID
        {
            get { return _PortalID; }
            set { _PortalID = value; }
        }
        private string _portalName;

        public string PortalName
        {
            get { return _portalName; }
            set { _portalName = value; }
        }
        private string _portalDescription;

        public string PortalDescription
        {
            get { return _portalDescription; }
            set { _portalDescription = value; }
        }
       
    }
}
