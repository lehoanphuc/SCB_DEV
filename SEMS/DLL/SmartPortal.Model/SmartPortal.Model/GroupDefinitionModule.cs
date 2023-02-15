using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartPortal.Model
{
    public class GroupDefinitionModule
    {
        private string groupID;
        private string module;
        private string acGroupDef;
        
        public string GroupID { get => groupID; set => groupID = value; }
        public string Module { get => module; set => module = value; }
        public string AcGroupDef { get => acGroupDef; set => acGroupDef = value; }
    }
}
