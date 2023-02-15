using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SmartPortal.Test
{
    public partial class RS : Form
    {
        public RS()
        {
            InitializeComponent();
        }

        private void RS_Load(object sender, EventArgs e)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add("IPCTRANCODE", "SEMSCUSTADD");
                hasInput.Add("SOURCEID", "SEMS");
                hasInput.Add("SOURCETRANREF", "012");
                hasInput.Add("TRANDESC", "LAY THONG TIN KHACH HANG");
                hasInput.Add("REVERSAL", "N");

                hasInput.Add("CUSTID", "KH0001");
                hasInput.Add("FULLNAME", "TRAN ANH TUAN A");
                hasInput.Add("TEL", "0908847288");
                hasInput.Add("LICENSEID", "001");
                hasInput.Add("CFTYPE", "1");
                hasInput.Add("STATUS", "A");

                hasOutput= SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
