using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.IO;


public partial class Widgets_SEMSCustomerListCorp_ExportToFile : System.Web.UI.UserControl
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string fileName = "Danhsachkhachhang";

        //export to excel
        if (radFile.SelectedValue == "E")
        {
            //DataTable deTable = new DataTable();
            //deTable = (DataTable)Session["DataExport"];
            //SmartPortal.Common.ExportToFile.ExportToExcel(deTable, fileName);

            GridView gv = (GridView)Session["DataExport"];
            DataSet tblExport = (DataSet)Session["TableExport"];
            
            SmartPortal.Common.ExportToFile.ExportGridViewToExcel(gv, fileName,tblExport);
        }

        //export to PDF
        if (radFile.SelectedValue == "P")
        {
            DataTable deTable = new DataTable();
            deTable = (DataTable)Session["DataExport"];
            SmartPortal.Common.ExportToFile.ExportToPDF(deTable, fileName);
        }

        //export to Word
        if (radFile.SelectedValue == "W")
        {
            DataTable deTable = new DataTable();
            deTable = (DataTable)Session["DataExport"];
            SmartPortal.Common.ExportToFile.ExportToWord(deTable, fileName);
        }

        //export to CSV
        if (radFile.SelectedValue == "C")
        {
            DataTable deTable = new DataTable();
            deTable = (DataTable)Session["DataExport"];
            SmartPortal.Common.ExportToFile.ExportToCSV(deTable, fileName);
        }
    }
}
