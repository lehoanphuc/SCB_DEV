using System;
using System.Data;
using SmartPortal.IB;

public partial class Widgets_IBEventLog_Widget : WidgetBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
            if (txtFromDate.Text.Length > 0 && txtToDate.Text.Length > 0)
            {
                DateTime myDate = DateTime.ParseExact(txtFromDate.Text.ToString(), "dd/MM/yyyy", null);
                DateTime targetDate = DateTime.ParseExact(txtToDate.Text.ToString(), "dd/MM/yyyy", null);
                if (myDate > targetDate)
                {
                    lblError.Text = Resources.labels.ngayketthucphailonhonngaybatdau;
                    return;
                }
            }
            //Bang chua thong tin tai khoan DD
            DataTable ddTable = new DataTable();
            DataColumn tranCodeCol = new DataColumn("Date");
            DataColumn tranTypeCol = new DataColumn("Time");
            DataColumn dateCol = new DataColumn("TranType");
            DataColumn inCol = new DataColumn("Desc");
            DataSet ds = new DataSet();
            Customer cf = new Customer();
            string errorCode = string.Empty;
            string errorDesc = string.Empty;
            ds = cf.GetInfo("IB_GETEVENTLOG", new object[] { Session["userName"].ToString(), txtFromDate.Text, txtToDate.Text }, ref errorCode, ref errorDesc);
            ddTable = ds.Tables[0];
            if (ddTable.Rows.Count == 0)
            {
                ltrTH.Text = "<p style='color:red;margin-left:10px; margin-top:20px;'>" + Resources.labels.khongtimthaydulieu + "</p>";
            }
            else
            {
                ltrTH.Text = string.Empty;
            }
            rptLog.DataSource = ddTable;
            rptLog.DataBind();
        }
        catch
        {
        }
    }
}
