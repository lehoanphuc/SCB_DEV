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

public partial class Widgets_IBSearchTransaction_Widget : WidgetBase
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
            }
        }
        catch
        {
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            //Bang chua thong tin tai khoan DD
            DataTable ddTable = new DataTable();
            DataColumn tranCodeCol = new DataColumn("TranCode");
            DataColumn tranTypeCol = new DataColumn("TranType");
            DataColumn dateCol = new DataColumn("Date");
            DataColumn inCol = new DataColumn("In");
            DataColumn outCol = new DataColumn("Out");
            DataColumn descCol1 = new DataColumn("Desc");
            ddTable.Columns.AddRange(new DataColumn[] { tranCodeCol,tranTypeCol, dateCol, inCol, outCol, descCol1 });

            DataRow r = ddTable.NewRow();
            r["TranCode"] = "A3231";
            r["TranType"] = "Rút tiền";
            r["Date"] = "12/02/2010";
            r["In"] = "3.000.000 LAK";
            r["Out"] = "";
            r["Desc"] = "Rút tiền ATM";

            ddTable.Rows.Add(r);

            //xuat len man hinh
            StringBuilder sT = new StringBuilder();
            sT.Append("<table class='style1' cellpadding='5' cellspacing='0'>");
            sT.Append("<tr class='thtr'>");
            sT.Append("<td class='thtdff'>");
            sT.Append("Mã giao dịch");
            sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append("Loại giao dịch");
            sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append("Ngày");
            sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append("Ghi có");
            sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append("Ghi nợ");
            sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append("Mô tả");
            sT.Append("</td>");
            sT.Append("</tr>");
            foreach (DataRow row in ddTable.Rows)
            {
                sT.Append("<tr>");
                sT.Append("<td class='thtdff'>");
                sT.Append(row["TranCode"].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row["TranType"].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row["Date"].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row["In"].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row["Out"].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row["Desc"].ToString());
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
            
        }
        catch
        {
        }
    }
}
