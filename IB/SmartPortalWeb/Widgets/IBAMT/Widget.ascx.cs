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

public partial class Widgets_IBAMT_Widget : WidgetBase
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AMT();
            }
        }
        catch
        {
        }
    }
    private void AMT()
    {
        try
        {
            //Bang chua thong tin tai khoan DD
            DataTable ddTable = new DataTable();
            DataColumn AMTNameCol = new DataColumn("AMTName");
            DataColumn statusCol = new DataColumn("Status");
            DataColumn AMTValueCol = new DataColumn("AMTValue");
            DataColumn valueCol = new DataColumn("Value");
            DataColumn postingAMTCol = new DataColumn("PostingAMT");
            ddTable.Columns.AddRange(new DataColumn[] { AMTNameCol, statusCol, AMTValueCol, valueCol, postingAMTCol });

            DataRow r = ddTable.NewRow();
            r["AMTName"] = "Nhà mặt tiền";
            r["Status"] = "Tồn tại";
            r["AMTValue"] = "2.000.000.000 LAK";
            r["Value"] = "5.000.000.000 LAK";
            r["PostingAMT"] = "200.000.000 LAK";

            ddTable.Rows.Add(r);

            //xuat len man hinh
            StringBuilder sT = new StringBuilder();
            sT.Append("<table class='style1' cellpadding='5' cellspacing='0'>");
            sT.Append("<tr class='thtr'>");
            sT.Append("<td class='thtdff'>");
            sT.Append("Tài sản thế chấp");
            sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append("Trạng thái");
            sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append("Giá trị tài sản thế chấp");
            sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append("Giá trị thế chấp");
            sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append("Gía trị hoạch toán ngoại bảng");
            sT.Append("</td>");
            sT.Append("</tr>");
            foreach (DataRow row in ddTable.Rows)
            {
                sT.Append("<tr>");
                sT.Append("<td class='thtdff'>");
                sT.Append(row["AMTName"].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row["Status"].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row["AMTValue"].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row["Value"].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row["PostingAMT"].ToString());
                sT.Append("</td>");
                sT.Append("</tr>");
            }
            sT.Append("</table>");

            ltrTH.Text = sT.ToString();
        }
        catch
        {
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            AMT();
        }
        catch
        {
        }
    }
}
