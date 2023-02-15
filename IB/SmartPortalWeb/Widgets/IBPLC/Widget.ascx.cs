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
using SmartPortal.IB;

public partial class Widgets_IBPLC_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        //    //Bang chua thong tin tai khoan DD
        //    DataTable ddTable = new DataTable();
        //    DataColumn STTCol = new DataColumn("STT");
        //    DataColumn NTGCol = new DataColumn("NTG");
        //    DataColumn GDHCol = new DataColumn("GDH");
        //    DataColumn GQHCol = new DataColumn("GQH");
        //    DataColumn GDTCol = new DataColumn("GDT");

        //    ddTable.Columns.AddRange(new DataColumn[] { STTCol, NTGCol, GDHCol, GQHCol, GDTCol });

        //    DataRow r = ddTable.NewRow();
        //    r["STT"] = "1";
        //    r["NTG"] = "10/01/2001";
        //    r["GDH"] = "2.000 LAK";
        //    r["GQH"] = "3.000.000 LAK";
        //    r["GDT"] = "2.000.000 LAK";


        //    ddTable.Rows.Add(r);

        //    //xuat len man hinh
        //    StringBuilder sT = new StringBuilder();
        //    sT.Append("<table class='style1' cellpadding='5' cellspacing='0'>");
        //    sT.Append("<tr class='thtr'>");
        //    sT.Append("<td class='thtdff'>");
        //    sT.Append("STT");
        //    sT.Append("</td>");
        //    sT.Append("<td class='thtd'>");
        //    sT.Append("Ngày trả gốc");
        //    sT.Append("</td>");
        //    sT.Append("<td class='thtd'>");
        //    sT.Append("Gốc đến hạn");
        //    sT.Append("</td>");
        //    sT.Append("<td class='thtd'>");
        //    sT.Append("Gốc quá hạn");
        //    sT.Append("</td>");
        //    sT.Append("<td class='thtdfff'>");
        //    sT.Append("Gốc đã trả");
        //    sT.Append("</td>");

        //    sT.Append("</tr>");
        //    foreach (DataRow row in ddTable.Rows)
        //    {
        //        sT.Append("<tr>");
        //        sT.Append("<td class='thtdff'>");
        //        sT.Append(row["STT"].ToString());
        //        sT.Append("</td>");
        //        sT.Append("<td class='thtd'>");
        //        sT.Append(row["NTG"].ToString());
        //        sT.Append("</td>");
        //        sT.Append("<td class='thtd'>");
        //        sT.Append(row["GDH"].ToString());
        //        sT.Append("</td>");
        //        sT.Append("<td class='thtd'>");
        //        sT.Append(row["GQH"].ToString());
        //        sT.Append("</td>");
        //        sT.Append("<td class='thtdfff'>");
        //        sT.Append(row["GDT"].ToString());
        //        sT.Append("</td>");

        //        sT.Append("</tr>");
        //    }
        //    sT.Append("</table>");
        //    ltrTH.Text = sT.ToString();

        //    //----------------------hhhh-------------------------
        //    //Bang chua thong tin tai khoan DD
        //    DataTable lTable = new DataTable();
        //    DataColumn STT1Col = new DataColumn("STT");            
        //    DataColumn NTLCol = new DataColumn("NTL");
        //    DataColumn LDHCol = new DataColumn("LDH");
        //    DataColumn LQHCol = new DataColumn("LQH");
        //    DataColumn LDTCol = new DataColumn("LDT");
        //    lTable.Columns.AddRange(new DataColumn[] { STT1Col,  NTLCol, LDHCol, LQHCol, LDTCol });

        //    DataRow r1 = lTable.NewRow();
        //    r1["STT"] = "1";

        //    r1["NTL"] = "10/01/2001";
        //    r1["LDH"] = "200.000 LAK";
        //    r1["LQH"] = "100.000 LAK";
        //    r1["LDT"] = "100.000 LAK";

        //    lTable.Rows.Add(r1);


        //    //xuat len man hinh
        //    StringBuilder sT1 = new StringBuilder();
        //    sT1.Append("<table class='style1' cellpadding='5' cellspacing='0'>");
        //    sT1.Append("<tr class='thtr'>");
        //    sT1.Append("<td class='thtdff'>");
        //    sT1.Append("STT");
        //    sT1.Append("</td>");

        //    sT1.Append("<td class='thtd'>");
        //    sT1.Append("Ngày trả lãi");
        //    sT1.Append("</td>");
        //    sT1.Append("<td class='thtd'>");
        //    sT1.Append("Lãi đến hạn");
        //    sT1.Append("</td>");
        //    sT1.Append("<td class='thtd'>");
        //    sT1.Append("Lãi quá hạn");
        //    sT1.Append("</td>");
        //    sT1.Append("<td class='thtdfff'>");
        //    sT1.Append("Lãi đã trả");
        //    sT1.Append("</td>");
        //    sT1.Append("</tr>");
        //    foreach (DataRow row in lTable.Rows)
        //    {
        //        sT1.Append("<tr>");
        //        sT1.Append("<td class='thtdff'>");
        //        sT1.Append(row["STT"].ToString());
        //        sT1.Append("</td>");

        //        sT1.Append("<td class='thtd'>");
        //        sT1.Append(row["NTL"].ToString());
        //        sT1.Append("</td>");
        //        sT1.Append("<td class='thtd'>");
        //        sT1.Append(row["LDH"].ToString());
        //        sT1.Append("</td>");
        //        sT1.Append("<td class='thtd'>");
        //        sT1.Append(row["LQH"].ToString());
        //        sT1.Append("</td>");
        //        sT1.Append("<td class='thtdfff'>");
        //        sT1.Append(row["LDT"].ToString());
        //        sT1.Append("</td>");
        //        sT1.Append("</tr>");
        //    }
        //    sT1.Append("</table>");


        //    ltrL.Text = sT1.ToString();
        //}
        //catch
        //{
        //}
        try
        {
            LoadLNSchedule();
        }
        catch
        {
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/?po=3&p=87");
    }

    #region Load Schedule Loan Info
    private void LoadLNSchedule()
    {
        try
        {
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            DataSet ds = new DataSet();
            string acct = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ACCTNO"].ToString();
            Account objAcct = new Account();
            //ds = objAcct.GetLNSchedulePayment(Session["userID"].ToString(), acct, ref ErrorCode, ref ErrorDesc);
            //xuat len man hinh
            StringBuilder sT1 = new StringBuilder();
            sT1.Append("<table class='style1' cellpadding='5' cellspacing='0'>");
            sT1.Append("<tr class='thtr'>");
            sT1.Append("<td class='thtdff'>");
            sT1.Append("STT");
            sT1.Append("</td>");

            sT1.Append("<td class='thtd'>");
            sT1.Append("Ngày đến hạn");
            sT1.Append("</td>");
            sT1.Append("<td class='thtd'>");
            sT1.Append("Số ngày tính lãi");
            sT1.Append("</td>");
            sT1.Append("<td class='thtd'>");
            sT1.Append("Gốc đến hạn");
            sT1.Append("</td>");            
            sT1.Append("<td class='thtdfff'>");
            sT1.Append("Lãi đến hạn");
            sT1.Append("</td>");
            sT1.Append("</tr>");
            int i = 1;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                sT1.Append("<tr>");
                sT1.Append("<td class='thtdff'>");
                sT1.Append(i.ToString());
                sT1.Append("</td>");
                sT1.Append("<td class='thtd'>");
                sT1.Append(SmartPortal.Common.Utilities.Utility.FormatDatetime(row["TODATE"].ToString(),"dd/MM/yyyy"));
                sT1.Append("</td>");
                sT1.Append("<td class='thtd'>");
                sT1.Append(row["NUMDATE"].ToString());
                sT1.Append("</td>");
                sT1.Append("<td class='thtd'>");
                sT1.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(row["MATURITYAMOUNT"].ToString(),""));
                sT1.Append("</td>");
                sT1.Append("<td class='thtdfff'>");
                sT1.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(row["MATURITYINTEREST"].ToString(),""));
                sT1.Append("</td>");
                sT1.Append("</tr>");
                i++;
            }
            sT1.Append("</table>");
            ltrL.Text = sT1.ToString();
        }
        catch
        {
        }
    }
    #endregion
}
