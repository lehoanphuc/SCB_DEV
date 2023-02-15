using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITransaction
{
    public abstract class AutoTrans: MarshalByRefObject
    {
        public abstract string ProcessTransXML(string InputData);
        public abstract string ProcessTransISO(string InputData);
        public abstract string ProcessTransSEP(string InputData);
        public abstract string ProcessTransSMS(string InputData);
        public abstract Hashtable ProcessTransHAS(Hashtable InputData);
        public abstract bool SynchronizeTrans(ref Hashtable InputData);
        public abstract Hashtable ProcessOnlyHAS(Hashtable InputData);
    }
}