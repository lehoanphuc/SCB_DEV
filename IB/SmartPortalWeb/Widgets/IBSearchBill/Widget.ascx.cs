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

using System.Text;

public partial class Widgets_IBSearchBill_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable myDataTable = new DataTable();
        DataColumn SothamchieuCol = new DataColumn();
        DataColumn SohoiphieuCol = new DataColumn();
        DataColumn NgaytaoCol = new DataColumn();
        DataColumn TrangthaiCol = new DataColumn();
        SothamchieuCol.ColumnName = "Sothamchieu";
        SohoiphieuCol.ColumnName = "Sohoiphieu";
        NgaytaoCol.ColumnName = "Ngaytao";
        TrangthaiCol.ColumnName = "Trangthai";
        myDataTable.Columns.Add(SothamchieuCol);
        myDataTable.Columns.Add(SohoiphieuCol);
        myDataTable.Columns.Add(NgaytaoCol);
        myDataTable.Columns.Add(TrangthaiCol);

        for (int row = 1; row < 8; row++)
        {
                DataRow dataRow = myDataTable.NewRow();
                dataRow["Sothamchieu"] = "040-2-4100-10-00020";
                dataRow["Sohoiphieu"] = "123156358";
                dataRow["Ngaytao"] = "07/04/2010";
                dataRow["Trangthai"] = "Chấp nhận thanh toán";
                myDataTable.Rows.Add(dataRow); 
        }
        GridViewSearchBill.DataSource = myDataTable;
        GridViewSearchBill.DataBind();
       


    }
        
}
