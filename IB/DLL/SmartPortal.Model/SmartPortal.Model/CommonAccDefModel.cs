using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartPortal.Model
{
    public class CommonAccDefModel
    {
        private string acname;
        private string acno;
        private string refAcNo1;
        private string refAcNo2;

        public string Acname { get => acname; set => acname = value; }
        public string Acno { get => acno; set => acno = value; }
        public string RefAcNo1 { get => refAcNo1; set => refAcNo1 = value; }
        public string RefAcNo2 { get => refAcNo2; set => refAcNo2 = value; }
    }
}
