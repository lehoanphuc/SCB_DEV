using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
//using DayPilot.Web.Ui;
using Microsoft.SqlServer.Server;
using Stimulsoft.Report.Components;
using Stimulsoft.Base.Indicator;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
//using SmartPortal.BLS;
using System.Text;
using iTextSharp.text;

public partial class Widgets_SEMSCashback_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static bool IsUpdate = false;
    public static string nameFile = string.Empty;
    public static string TypeCashBack = string.Empty;

    SmartPortal.SEMS.Common _common = new SmartPortal.SEMS.Common();

    public DataTable CASHBACK
    {
        get { return ViewState["TBLCASHBACK"] != null ? (DataTable)ViewState["TBLCASHBACK"] : new DataTable(); }
        set { ViewState["TBLCASHBACK"] = CASHBACK; }
    }
    private void clearFileUpload()
    {
        FileUpload1.Dispose();
        FileUpload1.Attributes.Clear();
        ViewState["CASHBACK"] = CreateTabel();
        BindData();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                btnImport.Visible = true;
                IsUpdate = false;
                clearFileUpload();
                ViewState["TBLCASHBACK"] = CreateTabel();
                BindData();
                //divResult.Visible = false;
                btsave.Visible = false;
            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }

    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        GVCashCode.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        GVCashCode.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData(GVCashCode.PageSize, GVCashCode.PageIndex * GVCashCode.PageSize);
    }
    private void BindData(int lenght = 15, int start = 0)
    {

        DataTable dt = CASHBACK;
        var rows = dt.AsEnumerable().Skip(start).Take(lenght);
        DataTable result = CreateTabel();

        foreach (DataRow dr in rows)
        {
            DataRow drow = result.NewRow();
            drow = dr;
            result.ImportRow(drow);
        }
        if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < GVCashCode.PageIndex * GVCashCode.PageSize) return;
        GVCashCode.DataSource = result;
        GVCashCode.DataBind();
        if (result.Rows.Count > 0)
        {
            ltrError.Text = string.Empty;
            GridViewPaging.Visible = true;
            int pageNumber = (int)(dt.Rows.Count);
            ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = pageNumber.ToString();
        }
        else
        {
            ltrError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
            GridViewPaging.Visible = false;
        }
    }
    public void UpdateTabel(string No, string tranNo, string tranName, string registerPhone, string registerFullName, string invitationCode, string amount)
    {
        DataTable dt = CASHBACK;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["colNo"].Equals(No))
            {
                //dr["colTransactionNumber"] = tranNo;
                dr["colTranName"] = tranName;
                dr["colRegisterPhone"] = registerPhone;
                dr["colRegisterFullName"] = registerFullName;
                dr["colInvitationCode"] = invitationCode;
                dr["colAmount"] = amount == "" ? "" : SmartPortal.Common.Utilities.Utility.FormatMoney(amount, SmartPortal.Constant.IPC.LAK); ;
            }
        }
        ViewState["TBLCASHBACK"] = dt;
    }

    protected void btnImport_click(object sender, EventArgs e)
    {

        try
        {
            bool action = true;
            nameFile = DateTime.Now.Ticks.ToString();
            string fileName = Session["userName"].ToString() + "_" + System.IO.Path.GetRandomFileName().Replace(".", "") + "_" + FileUpload1.FileName;
            string filePath = Server.MapPath("~/Uploaded/") + fileName;
            string extension = System.IO.Path.GetExtension(filePath).ToLower();
            FileUpload1.PostedFile.SaveAs(filePath);
            if (extension.Equals(".xlsx") || extension.Equals(".xls"))
            {
                action = ImportXLSXOrXLS(filePath);

            }
            if (extension.Equals(".csv"))
            {
                action = ImportCSV(filePath);
            }
            if (action)
            {
                btsave.Visible = true;
            }
            else
            {
                lblError.Text = "The information is invalid. Please re-upload file";
                btsave.Visible = false;
            }
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnClearing_Click(object sender, EventArgs e)
    {

        ViewState.Clear();
        FileUpload1.Attributes.Clear();
        FileUpload1.Dispose();
        CASHBACK = CreateTabel();
        GVCashCode.DataSource = CASHBACK;
        GridViewPaging.Visible = false;
        GVCashCode.DataBind();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet dsResult = new DataSet();


            if (TypeCashBack.Equals("MB_REGISTERWL"))
            {
                DataTable dtResult = CASHBACK.DefaultView.ToTable(true, "colTransactionNumber", "colInvitationCode", "colRegisterPhone", "colTranName", "colAmount", "colPocketType", "colCCYID", "colUserID");

                foreach (DataRow dr in dtResult.Rows)
                {
                    dr["colTransactionNumber"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colTransactionNumber"].ToString());
                    dr["colInvitationCode"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colInvitationCode"].ToString());
                    dr["colRegisterPhone"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colRegisterPhone"].ToString());
                    dr["colTranName"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colTranName"].ToString());
                    dr["colPocketType"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colPocketType"].ToString());
                    dr["colCCYID"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colCCYID"].ToString());
                    dr["colUserID"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colUserID"].ToString());
                    dr["colAmount"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(dr["colAmount"].ToString(), "LAK"));
                }
                var rows = dtResult.AsEnumerable().Where(a => a["colAmount"].ToString().Equals(""));
                if (rows.Any())
                {
                    lblError.Text = "Amount cannot be empty";
                    return;
                }
                dsResult = new SmartPortal.SEMS.Transactions().ConfirmCashBack(dtResult, ref IPCERRORCODE, ref IPCERRORDESC);
            }
            else
            {
                DataTable dtResult = CASHBACK.DefaultView.ToTable(true, "colNo", "colTransactionNumber", "colRegisterPhone", "colRegisterFullName", "colTranName", "colAmount", "colPocketType", "colCCYID", "colUserID", "colDesc", "colStatus");

                var rows = dtResult.AsEnumerable().Where(a => a["colAmount"].ToString().Equals(""));
                if (rows.Any())
                {
                    lblError.Text = "Amount cannot be empty";
                    return;
                }
                foreach (DataRow dr in dtResult.Rows)
                {
                    dr["colRegisterPhone"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colRegisterPhone"].ToString());
                    dr["colRegisterFullName"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colRegisterFullName"].ToString());
                    dr["colTranName"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colTranName"].ToString());
                    dr["colPocketType"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colPocketType"].ToString());
                    dr["colCCYID"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colCCYID"].ToString());
                    dr["colUserID"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colUserID"].ToString());
                    dr["colUserID"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colUserID"].ToString());
                    dr["colDesc"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(dr["colDesc"].ToString());
                    switch (dr["colStatus"].ToString())
                    {
                        case "1":
                            dr["colStatus"] = "";
                            break;
                        case "0":
                            dr["colStatus"] = "Phone and fullname are not synchronized";
                            break;
                        case "-1":
                            dr["colStatus"] = "This account is Invalid";
                            break;
                        case "-2":
                            dr["colStatus"] = "Invalid amount";
                            break;
                        default:
                            dr["colStatus"] = "Error";
                            break;
                    }
                    dr["colAmount"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(dr["colAmount"].ToString(), "LAK"));
                    dsResult = new SmartPortal.SEMS.Transactions().ConfirmCashBack(dr["colRegisterPhone"].ToString(), dr["colRegisterFullName"].ToString(), dr["colTranName"].ToString(), dr["colAmount"].ToString(), dr["colPocketType"].ToString(),
                        dr["colCCYID"].ToString(), dr["colUserID"].ToString(), dr["colDesc"].ToString(), dr["colStatus"].ToString(), Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                }
            }
            if (IPCERRORCODE.ToString() != "0")
            {
                lblError.Text = IPCERRORDESC.ToString();
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                IEnumerable<string> columnNames = CASHBACK.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in CASHBACK.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                    sb.AppendLine(string.Join(",", fields));
                }
                if (IsUpdate)
                {
                    string fileName = Server.MapPath("~/Uploaded/") + string.Format("{0}{1}", nameFile + "_UD", ".csv");
                    File.WriteAllText(fileName, sb.ToString());
                }
                lblError.Text = Resources.labels.addsuccessfully;
                DataTable dtnew = CASHBACK;
                dtnew.Clear();
                ViewState["TBLCASHBACK"] = dtnew;
                btsave.Visible = false;
                btsave.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        switch (commandName)
        {
            case IPC.ACTIONPAGE.EDIT:
                RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                break;

        }
    }
    protected void GVCashCode_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        GVCashCode.EditIndex = e.NewEditIndex;
        btsave.Visible = false;
        BindData();
    }
    protected void GVCashCode_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        try
        {
            IsUpdate = true;
            Label id = GVCashCode.Rows[e.RowIndex].FindControl("lbl_No") as Label;
            TextBox txtTranNo = GVCashCode.Rows[e.RowIndex].FindControl("txt_TransactionNumber") as TextBox;
            TextBox txtTran = GVCashCode.Rows[e.RowIndex].FindControl("txt_Tran") as TextBox;
            TextBox txtRegisterPhone = GVCashCode.Rows[e.RowIndex].FindControl("txt_RegisterPhone") as TextBox;
            TextBox txtRegisterFullName = GVCashCode.Rows[e.RowIndex].FindControl("txt_RegisterFullName") as TextBox;
            TextBox txtInvitaionCode = GVCashCode.Rows[e.RowIndex].FindControl("txt_Invitationcode") as TextBox;
            TextBox txtAmount = GVCashCode.Rows[e.RowIndex].FindControl("txt_Amount") as TextBox;
            UpdateTabel(id.Text, txtTranNo.Text, txtTran.Text, txtRegisterPhone.Text, txtRegisterFullName.Text, txtInvitaionCode.Text, txtAmount.Text);
            GVCashCode.EditIndex = -1;
            btsave.Visible = true;
            BindData();
        }
        catch
        {

        }
    }
    protected void GVCashCode_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        GVCashCode.EditIndex = -1;
        BindData();
    }
    private DataTable CreateTabel()
    {

        DataTable dtCashback = new DataTable();
        DataColumn colNo = new DataColumn("colNo");
        DataColumn colTransactionNumber = new DataColumn("colTransactionNumber");
        DataColumn colDateTime = new DataColumn("colDateTime");
        DataColumn colTranName = new DataColumn("colTranName");
        DataColumn colRegisterPhone = new DataColumn("colRegisterPhone");
        DataColumn colRegisterFullName = new DataColumn("colRegisterFullName");
        DataColumn colInvitationCode = new DataColumn("colInvitationCode");
        DataColumn colAmount = new DataColumn("colAmount");
        DataColumn colPocketType = new DataColumn("colPocketType");
        DataColumn colCCYID = new DataColumn("colCCYID");
        DataColumn colUserID = new DataColumn("colUserID");
        DataColumn colDesc = new DataColumn("colDesc");
        DataColumn colStatus = new DataColumn("colStatus");
        //add vào table
        dtCashback.Columns.Add(colNo);
        dtCashback.Columns.Add(colTransactionNumber);
        dtCashback.Columns.Add(colDateTime);
        dtCashback.Columns.Add(colTranName);
        dtCashback.Columns.Add(colRegisterPhone);
        dtCashback.Columns.Add(colRegisterFullName);
        dtCashback.Columns.Add(colInvitationCode);
        dtCashback.Columns.Add(colAmount);
        dtCashback.Columns.Add(colPocketType);
        dtCashback.Columns.Add(colCCYID);
        dtCashback.Columns.Add(colUserID);
        dtCashback.Columns.Add(colDesc);
        dtCashback.Columns.Add(colStatus);
        return dtCashback;

    }
    protected void GVCashCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lbl_TransactionNumber, lbl_No, colDateTime, colTran, lbl_RegisterPhone, lbl_RegisterFullName, lbl_Invitationcode, lbl_Amount, lbl_Status, lbl_Desc;


            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                TableRow tbRow = (TableRow)(e.Row.Cells[0].Controls[0].Controls[0]);
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + " :</strong>";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                lbl_No = (Label)e.Row.FindControl("lbl_No");
                lbl_TransactionNumber = (Label)e.Row.FindControl("lbl_TransactionNumber");
                colTran = (Label)e.Row.FindControl("colTran");
                colDateTime = (Label)e.Row.FindControl("colDateTime");
                lbl_RegisterPhone = (Label)e.Row.FindControl("lbl_RegisterPhone");
                lbl_RegisterFullName = (Label)e.Row.FindControl("lbl_RegisterFullName");
                lbl_Invitationcode = (Label)e.Row.FindControl("lbl_Invitationcode");
                lbl_Amount = (Label)e.Row.FindControl("lbl_Amount");
                lbl_Status = (Label)e.Row.FindControl("lbl_Status");
                lbl_Desc = (Label)e.Row.FindControl("lbl_Desc");

                lbl_No.Text = drv["colNo"].ToString();
                lbl_TransactionNumber.Text = drv["colTransactionNumber"].ToString();
                colTran.Text = drv["colTranName"].ToString();
                colDateTime.Text = drv["colDateTime"].ToString();
                lbl_RegisterPhone.Text = drv["colRegisterPhone"].ToString();
                lbl_RegisterFullName.Text = drv["colRegisterFullName"].ToString();
                lbl_Invitationcode.Text = drv["colInvitationCode"].ToString();
                lbl_Amount.Text = drv["colAmount"].ToString();
                lbl_Desc.Text = drv["colDesc"].ToString();
                switch (drv["colStatus"].ToString())
                {
                    case "1":
                        lbl_Status.Text = "Valid account";
                        lbl_Status.ForeColor = System.Drawing.Color.Green;
                        break;
                    case "0":
                        lbl_Status.Text = "Phone and fullname are not synchronized";
                        lbl_Status.ForeColor = System.Drawing.Color.Orange;
                        break;
                    case "-1":
                        lbl_Status.Text = "This account is Invalid";
                        lbl_Status.ForeColor = System.Drawing.Color.Red;
                        btsave.Visible = false;
                        break;
                    case "-2":
                        lbl_Status.Text = "Invalid amount";
                        lbl_Status.ForeColor = System.Drawing.Color.Red;
                        btsave.Visible = false;
                        break;
                    default:
                        lbl_Status.Text = "Error";
                        lbl_Status.ForeColor = System.Drawing.Color.Red;
                        btsave.Visible = false;
                        break;
                }
            }
        }
        catch
        {

        }
    }
    private DataTable ReadFileExcel(string filePath, string query)
    {
        try
        {
            string sConnectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES""", filePath);
            OleDbConnection objConn = new OleDbConnection(sConnectionString);
            objConn.Open();
            OleDbCommand objCmdSelect = new OleDbCommand(query, objConn);
            OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
            objAdapter1.SelectCommand = objCmdSelect;
            DataSet objDataset1 = new DataSet();
            objAdapter1.Fill(objDataset1, "XLData");
            objConn.Close();
            return objDataset1.Tables[0];
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), "");
            return null;
        }
    }

    private bool ImportCSV(string filePath)
    {

        string[] lines = File.ReadAllLines(filePath);
        string[] Fields;
        int No;
        if (CASHBACK.Rows.Count != 0)
        {
            No = CASHBACK.Rows.Count;
        }
        else
        {
            ViewState["TBLCASHBACK"] = CreateTabel();
            No = 0;
        }
        bool action = true;
        for (int i = 0; i < lines.Length; i++)
        {
            try
            {

                No = No + 1;
                string line = lines[i];
                Fields = line.Replace("\"", "").Split(new char[] { ',' });
                if (Fields.Contains("MB_REGISTERWL"))
                {
                    string result = new SmartPortal.SEMS.Transactions().CheckPhoneAndFullName(Fields[3].ToString().ToString().Trim(), Fields[4].ToString().ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                    DataRow row = CASHBACK.NewRow();
                    row["colNo"] = No.ToString();
                    row["colTransactionNumber"] = Fields[0].ToString();
                    row["colDateTime"] = Fields[1].ToString();
                    TypeCashBack = Fields[2].ToString();
                    row["colTranName"] = Fields[2].ToString().Trim('"');
                    row["colRegisterPhone"] = Fields[3].ToString();
                    row["colRegisterFullName"] = Fields[4].ToString();//SmartPortal.Common.Utilities.Utility.IsInt(Fields[3].ToString());
                    row["colInvitationCode"] = Fields[5].ToString();
                    if (SmartPortal.Common.Utilities.Utility.CheckIsDouble(Fields[6].ToString().Trim(), false) == 0 || double.Parse(Fields[6].ToString()) < 0)
                    {
                        result = "-2";
                    }
                    else
                    {
                        row["colAmount"] = Fields[6].ToString() == "" ? "" : SmartPortal.Common.Utilities.Utility.FormatMoney(Fields[6].ToString(), SmartPortal.Constant.IPC.LAK);
                    }
                    row["colUserID"] = Session["userName"].ToString();
                    row["colPocketType"] = "B";
                    row["colCCYID"] = "LAK";
                    row["colStatus"] = result;
                    CASHBACK.Rows.Add(row);
                    if (result.Equals("-1") || result.Equals("-2"))
                    {
                        action = false;
                    }
                }
            }
            catch
            {
                lblError.Text = "Import file with incorrect format";
            }

        }
        BindData();
        if (!action)
        {
            return false;
        }
        //btnImport.Visible = false;
        btsave.Visible = true;
        return true;
    }


    private bool ImportXLSXOrXLS(string filePath)
    {
        DataTable tab = ReadFileExcel(filePath, "SELECT * FROM [Sheet1$]");

        if (tab == null)
        {
            lblError.Text = "Invalid data format in uploaded excel file, please check and refill the corrected data or contact support center for more information.";
            return false;
        }
        if (tab.Rows.Count == 0)
        {
            lblError.Text = "No records found";
            return false;
        }

        if (string.IsNullOrEmpty(tab.Columns.ToString()))
        {
            lblError.Text = "Invalid data format in uploaded excel file, please check and refill the corrected data or contact support center for more information.";
            return false;
        }
        else
        {
            if (tab.Columns[0].ColumnName.ToString().Trim() != "Phone(*)" && tab.Columns[1].ColumnName.ToString().Trim() != "Full Name" && tab.Columns[2].ColumnName.ToString().Trim() != "Amount")
            {
                lblError.Text = "Invalid data format in uploaded excel file, please check and refill the corrected data or contact support center for more information.";
                return false;
            }
        }
        int No = 0;
        bool action = true;
        for (int i = tab.Rows.Count - 1; i >= 0; i += -1)
        {
            DataRow row = tab.Rows[i];
            if (row[0] == null)
            {
                tab.Rows.Remove(row);
            }
            else if (string.IsNullOrEmpty(row[0].ToString()))
            {
                tab.Rows.Remove(row);
            }
            else
            {
                if (CASHBACK.Rows.Count != 0)
                {
                    No = CASHBACK.Rows.Count;
                }
                else
                {
                    ViewState["TBLCASHBACK"] = CreateTabel();
                    No = 0;
                }
                No = No + 1;
                string result = new SmartPortal.SEMS.Transactions().CheckPhoneAndFullName(tab.Rows[i][0].ToString().Trim(), tab.Rows[i][1].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                DataRow row1 = CASHBACK.NewRow();
                row1["colNo"] = No.ToString();
                row1["colTransactionNumber"] = string.Empty;
                row1["colDateTime"] = string.Empty;
                TypeCashBack = "SEMS_CASHBACK";
                row1["colTranName"] = "SEMS_CASHBACK";
                row1["colRegisterPhone"] = tab.Rows[i][0].ToString().Trim();
                row1["colRegisterFullName"] = tab.Rows[i][1].ToString().Trim();

                if (tab.Rows[i][3].ToString().Trim().Length > 255)
                {
                    row1["colDesc"] = tab.Rows[i][3].ToString().Trim().Substring(0, 255);
                }
                else
                {
                    row1["colDesc"] = tab.Rows[i][3].ToString().Trim();
                }
                row1["colInvitationCode"] = string.Empty;
                if (SmartPortal.Common.Utilities.Utility.CheckIsDouble(tab.Rows[i][2].ToString().Trim(), false) == 0 || double.Parse(tab.Rows[i][2].ToString().Trim()) < 0)
                {
                    result = "-2";
                    if (tab.Rows[i][2].ToString().Trim().Equals("0"))
                    {
                        row1["colAmount"] = "0.00";
                    }
                    else
                    {
                        row1["colAmount"] = SmartPortal.Common.Utilities.Utility.FormatMoney(string.Format(tab.Rows[i][2].ToString(), System.Globalization.CultureInfo.GetCultureInfo("en-us")), SmartPortal.Constant.IPC.LAK);

                    }
                }
                else
                {
                    row1["colAmount"] = tab.Rows[i][2].ToString() == "" ? "" : SmartPortal.Common.Utilities.Utility.FormatMoney(string.Format(tab.Rows[i][2].ToString(), System.Globalization.CultureInfo.GetCultureInfo("en-us")), SmartPortal.Constant.IPC.LAK);
                }
                row1["colUserID"] = Session["userName"].ToString();
                row1["colPocketType"] = "B";
                row1["colCCYID"] = "LAK";
                row1["colStatus"] = result;
                CASHBACK.Rows.Add(row1);
                if (result.Equals("-1") || result.Equals("-2"))
                {
                    action = false;
                }
            }

        }
        if (!action)
        {
            return false;
        }
        return true;
    }
}