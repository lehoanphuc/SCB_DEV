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

public partial class Widgets_IBListTransWaitGetBack_Widget : WidgetBase
{


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Bang chua thong tin tai khoan DD
                DataTable ddTable = new DataTable();
                DataColumn magdCol = new DataColumn("Magiaodich");
                DataColumn loaigdCol = new DataColumn("Loaigiaodich");
                DataColumn ngayCol = new DataColumn("Ngay");
                DataColumn ghicoCol = new DataColumn("Ghico");
                DataColumn ghinoCol = new DataColumn("Ghino");
                DataColumn motaCol = new DataColumn("Mota");
                DataColumn trangthaiCol = new DataColumn("Trangthai");
                ddTable.Columns.AddRange(new DataColumn[] { magdCol, loaigdCol, ngayCol, ghicoCol, ghinoCol, motaCol, trangthaiCol });

                for (int i = 1; i < 5; i++)
                {
                    DataRow r = ddTable.NewRow();
                    r["Magiaodich"] = "A3231";
                    r["Loaigiaodich"] = "abc";
                    r["Ngay"] = "09/09/2009";
                    r["Ghico"] = "3.000.000 LAK";
                    r["Ghino"] = "3.000.000 LAK";
                    r["Mota"] = "nạp tiền";
                    r["Trangthai"] = "Chờ duyệt";

                    ddTable.Rows.Add(r);
                }

                //hien len luoi
                gvLTWA.DataSource = ddTable;
                gvLTWA.DataBind();
            }
        }
        catch
        {
        }
    }
    protected void gvLTWA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpTranCode;
            Label lblSenderAccount;
            Label lblRecieverAccount;
            Label lblAmount;
            Label lblStartDate;
            Label lblEndDate;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    drv = (DataRowView)e.Row.DataItem;


            //    cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");


            //    hpTranCode = (HyperLink)e.Row.FindControl("hpTranCode");

            //    lblSenderAccount = (Label)e.Row.FindControl("lblSenderAccount");
            //    lblRecieverAccount = (Label)e.Row.FindControl("lblRecieverAccount");
            //    lblAmount = (Label)e.Row.FindControl("lblAmount");
            //    lblStartDate = (Label)e.Row.FindControl("lblStartDate");
            //    lblEndDate = (Label)e.Row.FindControl("lblEndDate");

            //    //cbxSelect.Enabled = true;
            //    //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


            //    hpTranCode.Text = drv["TranCode"].ToString();
                 // hpTranCode.NavigateUrl = "~/Default.aspx?po=3&p=217";
            //    lblSenderAccount.Text = drv["SenderAccount"].ToString();
            //    lblRecieverAccount.Text = drv["RecieverAccount"].ToString();
            //    lblAmount.Text = drv["Amount"].ToString();
            //    lblStartDate.Text = drv["StartDate"].ToString();
            //    lblEndDate.Text = drv["EndDate"].ToString();

            //}
        }
        catch
        {
        }
    }
}
