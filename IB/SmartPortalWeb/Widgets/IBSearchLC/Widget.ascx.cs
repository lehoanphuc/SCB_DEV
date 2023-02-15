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

public partial class Widgets_IBSearchLC_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable myDataTable = new DataTable();
        DataColumn SothamchieuCol = new DataColumn();
        DataColumn SohopdongCol = new DataColumn();
        DataColumn TienteCol = new DataColumn();
        DataColumn LoaihinhCol = new DataColumn();
        DataColumn TrangthaiCol = new DataColumn();
        SothamchieuCol.ColumnName = "Sothamchieu";
        SohopdongCol.ColumnName = "Sohopdong";
        TienteCol.ColumnName = "Tiente";
        LoaihinhCol.ColumnName = "Loaihinh";
        TrangthaiCol.ColumnName = "Trangthai";
        myDataTable.Columns.Add(SothamchieuCol);
        myDataTable.Columns.Add(SohopdongCol);
        myDataTable.Columns.Add(TienteCol);
        myDataTable.Columns.Add(LoaihinhCol);
        myDataTable.Columns.Add(TrangthaiCol);

        for (int row = 1; row < 8; row++)
        {
                DataRow dataRow = myDataTable.NewRow();
                dataRow["Sothamchieu"] = "040-2-4100-10-0003";
                dataRow["Sohopdong"] = "HĐ 123";
                dataRow["Tiente"] = "VNĐ";
                dataRow["Loaihinh"] = "Out Ward";
                dataRow["Trangthai"] = "Bình thường";
                myDataTable.Rows.Add(dataRow); 
        }
        GridViewSearchLC.DataSource = myDataTable;
        GridViewSearchLC.DataBind();
       


    }
        
}
