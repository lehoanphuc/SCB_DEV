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
using DayPilot.Web.Ui;
using Microsoft.SqlServer.Server;
using Stimulsoft.Report.Components;
using Stimulsoft.Base.Indicator;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
//using SmartPortal.BLS;
using System.Text;
using iTextSharp.text;
using SmartPortal.SEMS;

public partial class Widgets_SEMSPARTNERBANKBRANCHUPLOAD_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static bool IsUpdate = false;
    public static string nameFile = string.Empty;
    public static string TypeCashBack = string.Empty;
    public static bool importex = false;
    DataSet dtsbranch = new DataSet();
    DataSet dtsOtherBranch = new DataSet();
    DataSet dtsOtherBank = new DataSet();
    DataSet dtsRegion = new DataSet();
    Partner partner = new Partner();
    Branch branch = new Branch();

    SmartPortal.SEMS.Common _common = new SmartPortal.SEMS.Common();

    public DataTable OTHERBRANCH
    {
        get { return ViewState["TBLOTHERBRANCH"] != null ? (DataTable)ViewState["TBLOTHERBRANCH"] : new DataTable(); }
        set { ViewState["TBLOTHERBRANCH"] = OTHERBRANCH; }
    }
    public DataTable OTHERBRANCHUPDATE
    {
        get { return ViewState["TBLOTHERBRANCHUPDATE"] != null ? (DataTable)ViewState["TBLOTHERBRANCHUPDATE"] : new DataTable(); }
        set { ViewState["TBLOTHERBRANCHUPDATE"] = OTHERBRANCHUPDATE; }
    }
    public DataTable BRANCHUPDATE
    {
        get { return ViewState["TBLBRANCHUPDATE"] != null ? (DataTable)ViewState["TBLBRANCHUPDATE"] : new DataTable(); }
        set { ViewState["TBLBRANCHUPDATE"] = BRANCHUPDATE; }
    }
    private void clearFileUpload()
    {

        ViewState.Clear();
        FileUpload1.Dispose();
        FileUpload1.Attributes.Clear();
        ViewState["TBLOTHERBRANCH"] = CreateTabel();
        ViewState["TBLOTHERBRANCHUPDATE"] = CreateTabelUpdate();
        ViewState["TBLBRANCHUPDATE"] = CreateTabelBranchUpdate();
        BindData();
        BindData2();
        btsave.Visible = true;
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
                //ViewState["TBLOTHERBRANCH"] = new DataTable();
                //ViewState["TBLOTHERBRANCHUPDATE"] = new DataTable();
                //BindData();
                //BindData2();
                //divResult.Visible = false;
                //btsave.Visible = false;
            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
            GridViewPaging2.pagingClickArgs += new EventHandler(GridViewPaging_Click2);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }

    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        GVOtherBranch.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        GVOtherBranch.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData(GVOtherBranch.PageSize, GVOtherBranch.PageIndex * GVOtherBranch.PageSize);
    }
    private void GridViewPaging_Click2(object sender, EventArgs e)
    {
        GVOtherBranchUpdate.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging2.FindControl("PageRowSize")).SelectedValue);
        GVOtherBranchUpdate.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging2.FindControl("SelectedPageNo")).Text) - 1;
        BindData2(GVOtherBranchUpdate.PageSize, GVOtherBranchUpdate.PageIndex * GVOtherBranchUpdate.PageSize);
    }
    private void BindData(int lenght = 15, int start = 0)
    {

        DataTable dt = OTHERBRANCH;
        var rows = dt.AsEnumerable().Skip(start).Take(lenght);
        DataTable result = CreateTabel();

        foreach (DataRow dr in rows)
        {
            DataRow drow = result.NewRow();
            drow = dr;
            result.ImportRow(drow);
        }
        if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < GVOtherBranch.PageIndex * GVOtherBranch.PageSize) return;
        GVOtherBranch.DataSource = result;
        GVOtherBranch.DataBind();
        if (result.Rows.Count > 0)
        {
            ltrError.Text = string.Empty;
            GridViewPaging.Visible = true;
            int pageNumber = (int)(dt.Rows.Count);
            ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = pageNumber.ToString();
        }
        else
        {
            lblotheradd.Visible = false;
            GridViewPaging.Visible = false;
        }
    }

    private void BindData2(int lenght = 15, int start = 0)
    {

        DataTable dt = OTHERBRANCHUPDATE;
        var rows = dt.AsEnumerable().Skip(start).Take(lenght);
        DataTable result = CreateTabelUpdate();

        foreach (DataRow dr in rows)
        {
            DataRow drow = result.NewRow();
            drow = dr;
            result.ImportRow(drow);
        }
        if (Convert.ToInt32(((HiddenField)GridViewPaging2.FindControl("TotalRows")).Value) < GVOtherBranchUpdate.PageIndex * GVOtherBranchUpdate.PageSize) return;
        GVOtherBranchUpdate.DataSource = result;
        GVOtherBranchUpdate.DataBind();
        if (result.Rows.Count > 0)
        {
            ltrError.Text = string.Empty;
            GridViewPaging2.Visible = true;
            int pageNumber = (int)(dt.Rows.Count);
            ((HiddenField)GridViewPaging2.FindControl("TotalRows")).Value = pageNumber.ToString();
        }
        else
        {
            lblotherupdate.Visible = false;
            GridViewPaging2.Visible = false;
        }
    }
    //public void UpdateTabel(string No, string tranNo, string tranName, string registerPhone, string registerFullName, string invitationCode, string amount)
    //{
    //    DataTable dt = OTHERBRANCH;
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        if (dr["colNo"].Equals(No))
    //        {
    //            //dr["colTransactionNumber"] = tranNo;
    //            dr["colTranName"] = tranName;
    //            dr["colRegisterPhone"] = registerPhone;
    //            dr["colRegisterFullName"] = registerFullName;
    //            dr["colInvitationCode"] = invitationCode;
    //        }
    //    }
    //    ViewState["TBLOTHERBRANCH"] = dt;
    //}

    protected void btnImport_click(object sender, EventArgs e)
    {
        try
        {
            ViewState["TBLOTHERBRANCH"] = null;
            ViewState["TBLOTHERBRANCHUPDATE"] = null;
            ViewState["TBLBRANCHUPDATE"] = null;

            ViewState.Remove("TBLOTHERBRANCHUPDATE");
            bool action = true;
            nameFile = DateTime.Now.Ticks.ToString();
            string fileName = Session["userName"].ToString() + "_" + System.IO.Path.GetRandomFileName().Replace(".", "") + "_" + FileUpload1.FileName;
            string filePath = Server.MapPath("~/Uploaded/") + fileName;
            string extension = System.IO.Path.GetExtension(filePath).ToLower();
            FileUpload1.PostedFile.SaveAs(filePath);
            if (extension.Equals(".xlsx") || extension.Equals(".xls"))
            {
                action = ImportXLSXOrXLS(filePath);
                importex = true;
            }
            btsave.Visible = true;
            if (OTHERBRANCH.Rows.Count > 0)
            {
                lblotheradd.Visible = true;
            }
            if (OTHERBRANCHUPDATE.Rows.Count > 0)
            {
                lblotherupdate.Visible = true;
            }
            BindData();
            BindData2();
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
        OTHERBRANCH = CreateTabel();
        OTHERBRANCHUPDATE = CreateTabelUpdate();
        BRANCHUPDATE = CreateTabelBranchUpdate();
        lblotherupdate.Visible = true;
        lblotheradd.Visible = true;
        GridViewPaging.Visible = false;
        GridViewPaging2.Visible = false;
        GVOtherBranch.DataBind();
        GVOtherBranchUpdate.DataBind();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsResult = new DataSet();
            if (OTHERBRANCH.Rows.Count == 0 && OTHERBRANCHUPDATE.Rows.Count == 0)
            {
                if (!importex)
                {
                    lblError.Text = "Please select file excel.";
                }
                else
                {
                    lblError.Text = "Invalid data format in uploaded excel file, please check and refill the corrected data or contact support center for more information.";
                }
            }
            else
            {
                DataTable dtOtherBranchInsert = OTHERBRANCH.DefaultView.ToTable(true, "colpartnerbankID", "colbranchCode", "colbranchName", "colregionID", "colbranchNameMM");
                DataTable dtOtherBranchUpdate = OTHERBRANCHUPDATE.DefaultView.ToTable(true, "colpartnerbankID", "colbranchCode", "colbranchName", "colregionID", "colbranchNameMM");
                //DataTable dtOBranchUpdate = BRANCHUPDATE.DefaultView.ToTable(true, "colbranchCode", "colbranchName", "colregionID");
                dsResult = new SmartPortal.SEMS.Partner().ImportOtherBranch(dtOtherBranchInsert, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    dsResult = new SmartPortal.SEMS.Partner().UPDATEOtherBranchMuti(dtOtherBranchUpdate, ref IPCERRORCODE, ref IPCERRORDESC);
                    //dsResult = new SmartPortal.SEMS.Partner().UPDATEBranchMuti(dtOBranchUpdate, ref IPCERRORCODE, ref IPCERRORDESC);
                    OTHERBRANCH = new DataTable();
                    OTHERBRANCHUPDATE = new DataTable();
                    ViewState.Clear();
                    BindData();
                    BindData2();
                    lblotheradd.Visible = false;
                    lblotherupdate.Visible = false;
                    lblError.Text = "Import Excel Successfully";
                    btsave.Visible = false;
                }
                else
                {
                    lblError.Text = "Invalid data format in uploaded excel file, please check and refill the corrected data or contact support center for more information.";
                }
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

    protected void GVOtherBranch_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        GVOtherBranch.EditIndex = e.NewEditIndex;
        btsave.Visible = false;
        BindData();
    }
    protected void GVOtherBranch_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        try
        {
            IsUpdate = true;
            Label id = GVOtherBranch.Rows[e.RowIndex].FindControl("lbl_No") as Label;
            TextBox txtTranNo = GVOtherBranch.Rows[e.RowIndex].FindControl("txt_TransactionNumber") as TextBox;
            TextBox txtTran = GVOtherBranch.Rows[e.RowIndex].FindControl("txt_Tran") as TextBox;
            TextBox txtRegisterPhone = GVOtherBranch.Rows[e.RowIndex].FindControl("txt_RegisterPhone") as TextBox;
            TextBox txtRegisterFullName = GVOtherBranch.Rows[e.RowIndex].FindControl("txt_RegisterFullName") as TextBox;
            TextBox txtInvitaionCode = GVOtherBranch.Rows[e.RowIndex].FindControl("txt_Invitationcode") as TextBox;
            TextBox txtAmount = GVOtherBranch.Rows[e.RowIndex].FindControl("txt_Amount") as TextBox;
            //UpdateTabel(id.Text, txtTranNo.Text, txtTran.Text, txtRegisterPhone.Text, txtRegisterFullName.Text, txtInvitaionCode.Text, txtAmount.Text);
            GVOtherBranch.EditIndex = -1;
            btsave.Visible = true;
            BindData();
        }
        catch
        {

        }
    }
    protected void GVOtherBranch_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        GVOtherBranch.EditIndex = -1;
        BindData();
    }
    private DataTable CreateTabel()
    {
        DataTable dtOtherBranch = new DataTable();
        DataColumn colNo = new DataColumn("colNo");
        DataColumn colbranchCode = new DataColumn("colbranchCode");
        DataColumn colbranchName = new DataColumn("colbranchName");
        DataColumn colbranchNameMM = new DataColumn("colbranchNameMM");
        DataColumn colregionName = new DataColumn("colregionName");
        DataColumn colbankCode = new DataColumn("colbankCode");
        DataColumn colDesc = new DataColumn("colDesc");
        DataColumn colpartnerbankID = new DataColumn("colpartnerbankID");
        DataColumn colregionID = new DataColumn("colregionID");
        //add vào table

        dtOtherBranch.Columns.Add(colNo);
        dtOtherBranch.Columns.Add(colbranchCode);
        dtOtherBranch.Columns.Add(colbranchName);
        dtOtherBranch.Columns.Add(colbranchNameMM);
        dtOtherBranch.Columns.Add(colregionName);
        dtOtherBranch.Columns.Add(colbankCode);
        dtOtherBranch.Columns.Add(colDesc);
        dtOtherBranch.Columns.Add(colpartnerbankID);
        dtOtherBranch.Columns.Add(colregionID);
        return dtOtherBranch;
    }

    private DataTable CreateTabelUpdate()
    {
        DataTable dtOtherBranchUpdate = new DataTable();
        DataColumn colNo = new DataColumn("colNo");
        DataColumn colbranchCode = new DataColumn("colbranchCode");
        DataColumn colbranchName = new DataColumn("colbranchName");
        DataColumn colbranchNameMM = new DataColumn("colbranchNameMM");
        DataColumn colregionName = new DataColumn("colregionName");
        DataColumn colbankCode = new DataColumn("colbankCode");
        DataColumn colDesc = new DataColumn("colDesc");
        DataColumn colpartnerbankID = new DataColumn("colpartnerbankID");
        DataColumn colregionID = new DataColumn("colregionID");
        //add vào table
        dtOtherBranchUpdate.Columns.Add(colNo);
        dtOtherBranchUpdate.Columns.Add(colbranchCode);
        dtOtherBranchUpdate.Columns.Add(colbranchName);
        dtOtherBranchUpdate.Columns.Add(colbranchNameMM);
        dtOtherBranchUpdate.Columns.Add(colregionName);
        dtOtherBranchUpdate.Columns.Add(colbankCode);
        dtOtherBranchUpdate.Columns.Add(colDesc);
        dtOtherBranchUpdate.Columns.Add(colpartnerbankID);
        dtOtherBranchUpdate.Columns.Add(colregionID);
        return dtOtherBranchUpdate;
    }
    private DataTable CreateTabelBranchUpdate()
    {
        DataTable dtBranchUpdate = new DataTable();
        DataColumn colNo = new DataColumn("colNo");
        DataColumn colbranchCode = new DataColumn("colbranchCode");
        DataColumn colbranchName = new DataColumn("colbranchName");
        DataColumn colbranchNameMM = new DataColumn("colbranchNameMM");
        DataColumn colregionName = new DataColumn("colregionName");
        DataColumn colbankCode = new DataColumn("colbankCode");
        DataColumn colDesc = new DataColumn("colDesc");
        DataColumn colpartnerbankID = new DataColumn("colpartnerbankID");
        DataColumn colregionID = new DataColumn("colregionID");
        //add vào table
        dtBranchUpdate.Columns.Add(colNo);
        dtBranchUpdate.Columns.Add(colbranchCode);
        dtBranchUpdate.Columns.Add(colbranchName);
        dtBranchUpdate.Columns.Add(colbranchNameMM);
        dtBranchUpdate.Columns.Add(colregionName);
        dtBranchUpdate.Columns.Add(colbankCode);
        dtBranchUpdate.Columns.Add(colDesc);
        dtBranchUpdate.Columns.Add(colpartnerbankID);
        dtBranchUpdate.Columns.Add(colregionID);
        return dtBranchUpdate;
    }

    protected void GVOtherBanch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lbl_branchCode, lbl_No, lbl_BranchName, lbl_BranchNameMM, lbl_RegionName, lbl_Desc;
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
                lbl_branchCode = (Label)e.Row.FindControl("lbl_branchCode");
                lbl_BranchName = (Label)e.Row.FindControl("lbl_BranchName");
                lbl_BranchNameMM = (Label)e.Row.FindControl("lbl_BranchNameMM");
                lbl_RegionName = (Label)e.Row.FindControl("lbl_RegionName");

                lbl_Desc = (Label)e.Row.FindControl("lbl_Desc");

                lbl_No.Text = drv["colNo"].ToString();
                lbl_branchCode.Text = drv["colbranchCode"].ToString();
                lbl_BranchName.Text = drv["colbranchName"].ToString();
                lbl_BranchNameMM.Text = drv["colbranchNameMM"].ToString();
                lbl_RegionName.Text = drv["colregionName"].ToString();

                lbl_Desc.Text = drv["colDesc"].ToString();
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
        if (OTHERBRANCH.Rows.Count != 0)
        {
            No = OTHERBRANCH.Rows.Count;
        }
        else
        {
            ViewState["TBLOTHERBRANCH"] = CreateTabel();
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
                    DataRow row = OTHERBRANCH.NewRow();
                    row["colNo"] = No.ToString();
                    row["colbranchCode"] = Fields[0].ToString();
                    row["colDateTime"] = Fields[1].ToString();
                    TypeCashBack = Fields[2].ToString();
                    row["colTranName"] = Fields[2].ToString().Trim('"');
                    row["colRegisterPhone"] = Fields[3].ToString();
                    row["colRegisterFullName"] = Fields[4].ToString();//SmartPortal.Common.Utilities.Utility.IsInt(Fields[3].ToString());
                    row["colInvitationCode"] = Fields[5].ToString();
                    OTHERBRANCH.Rows.Add(row);
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
        //hunglt lay sheet1
        ViewState.Clear();
        //ViewState["TBLOTHERBRANCH"] = new DataTable();
        //ViewState["TBLOTHERBRANCHUPDATE"] = new DataTable();
        //ViewState["TBLBRANCHUPDATE"] = new DataTable();
        DataTable tab = ReadFileExcel(filePath, "SELECT * FROM [Sheet1$]");
        //hunglt get all list
        dtsRegion = partner.GetRegionALL(ref IPCERRORCODE, ref IPCERRORDESC);
        dtsOtherBank = partner.GetBankALL(ref IPCERRORCODE, ref IPCERRORDESC);
        dtsOtherBranch = partner.GetOtherBranchALL(ref IPCERRORCODE, ref IPCERRORDESC);
        dtsbranch = branch.GetAll(ref IPCERRORCODE, ref IPCERRORDESC);
        //hunglt set lai viewsate
        ViewState["TBLOTHERBRANCH"] = CreateTabel();
        ViewState["TBLOTHERBRANCHUPDATE"] = CreateTabelUpdate();
        ViewState["TBLBRANCHUPDATE"] = CreateTabelBranchUpdate();
        if (tab == null)
        {
            lblError.Text =
                "Invalid data format in uploaded excel file, please check and refill the corrected data or contact support center for more information.";
            return false;
        }
        else
        {
            importex = true;
        }
        if (tab.Rows.Count == 0)
        {
            lblError.Text = "No records found";
            return false;
        }
        else
        {
            importex = true;
        }
        if (string.IsNullOrEmpty(tab.Columns.ToString()))
        {
            lblError.Text = "Invalid data format in uploaded excel file, please check and refill the corrected data or contact support center for more information.";
            return false;
        }
        else
        {
            if (tab.Columns[0].ColumnName.ToString().Trim() != "Branch Code" && tab.Columns[1].ColumnName.ToString().Trim() != "Branch Name" && tab.Columns[2].ColumnName.ToString().Trim() != "Region Name" && tab.Columns[2].ColumnName.ToString().Trim() != "Bank Code")
            {
                lblError.Text = "Invalid data format in uploaded excel file, please check and refill the corrected data or contact support center for more information.";
                return false;
            }
        }
        int No = 0;
        int No1 = 0;
        try
        {
            for (int i = 0; i < tab.Rows.Count; i++)
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
                    if (OTHERBRANCH.Rows.Count != 0)
                    {
                        No = OTHERBRANCH.Rows.Count;
                    }
                    else
                    {
                        ViewState["TBLOTHERBRANCH"] = CreateTabel();
                        No = 0;
                    }
                    No = No + 1;
                    DataRow row1 = OTHERBRANCH.NewRow();
                    DataRow row2 = OTHERBRANCHUPDATE.NewRow();
                    DataRow row3 = BRANCHUPDATE.NewRow();
                    string branchcode = tab.Rows[i][0].ToString().Trim();
                    string branchName = tab.Rows[i][1].ToString().Trim();
                    string branchNameMM = tab.Rows[i][2].ToString().Trim();
                    string regionName = tab.Rows[i][3].ToString().Trim();
                    string bankCode = tab.Rows[i][4].ToString().Trim();
                    string partnerid = "";
                    string regionid = "";
                    row1["colNo"] = No.ToString();
                    row1["colbranchCode"] = branchcode;
                    row1["colbranchName"] = branchName;
                    row1["colregionName"] = regionName;
                    row1["colbankCode"] = bankCode;
                    if (bankCode == "AYFDMMMYYGN")
                    {
                        tab.Rows.Remove(row);
                    }
                    //check
                    if (branchNameMM == null || branchNameMM == "")
                    {
                        row1["colbranchNameMM"] = branchName;
                    }
                    else
                    {
                        row1["colbranchNameMM"] = branchNameMM;
                    }
                    DataRow[] dtrl = getdatarow(dtsOtherBank, "BankCode", bankCode);
                    if (dtrl.Length > 0)
                    {
                        row1["colpartnerbankID"] = dtrl[0].ItemArray[0].ToString().Trim();
                        partnerid = dtrl[0].ItemArray[0].ToString().Trim();
                    }
                    else
                    {
                        row1["colpartnerbankID"] = "";
                        partnerid = "";
                    }

                    DataRow[] dtrl1 = getdatarow(dtsRegion, "RegionName", regionName);
                    if (dtrl1.Length > 0)
                    {
                        row1["colregionID"] = dtrl1[0].ItemArray[0].ToString().Trim();
                        regionid = dtrl1[0].ItemArray[0].ToString().Trim();
                    }
                    else
                    {
                        row1["colregionID"] = "";
                        regionid = "";
                    }
                    int parseint2 = 0;
                    bool checkflag = false;
                    if (Int32.TryParse(partnerid, out parseint2))
                    {
                        var partneridpart2 = Int32.Parse(partnerid);
                        var results2 = dtsOtherBranch.Tables[0].Rows.Cast<DataRow>()
                            .FirstOrDefault(x => x.Field<int>("PartnerBankID") == partneridpart2 && x.Field<string>("BranchCode") == branchcode.ToString().Trim()
                                                 );
                        if (results2 == null)
                        {
                            checkflag = true;
                        }
                    }
                    if (checkflag && regionid != "" && partnerid != null)
                    {
                        //check trung trong viewstate
                        if (OTHERBRANCH.Rows.Count > 0)
                        {
                            var results = OTHERBRANCH.Rows.Cast<DataRow>()
                                .FirstOrDefault(x => x.Field<string>("colbranchCode") == branchcode && x.Field<string>("colbankCode") == bankCode
                                                                                                    && x.Field<string>("colregionName") == regionName);
                            if (results == null)
                            {
                                OTHERBRANCH.Rows.Add(row1);
                            }
                        }
                        else
                        {
                            OTHERBRANCH.Rows.Add(row1);
                        }
                    }
                    else
                    {
                        bool flag, flag1 = true;
                        flag = checkcountdataset(dtsOtherBranch, "BranchCode", branchcode);
                        DataTable dt = new DataTable();
                        dt = dtsOtherBranch.Tables[0];
                        DataRow[] filteredRows;
                        int parseint = 0;
                        if (Int32.TryParse(partnerid, out parseint) && flag && regionid != "" && partnerid != null)
                        {
                            var partneridpart = Int32.Parse(partnerid);
                            var results1 = dtsOtherBranch.Tables[0].Rows.Cast<DataRow>()
                                   .FirstOrDefault(x => x.Field<int>("PartnerBankID") == partneridpart && x.Field<string>("BranchCode") == branchcode.ToString().Trim() && x.Field<string>("BranchName") == branchName.ToString().Trim()
                                                        && x.Field<string>("RegionID") == regionid.ToString().Trim() && x.Field<string>("BranchNameMM") == branchNameMM.ToString().Trim());
                            var results2 = dtsOtherBranch.Tables[0].Rows.Cast<DataRow>()
                                .FirstOrDefault(x => x.Field<int>("PartnerBankID") == partneridpart && x.Field<string>("BranchCode") == branchcode.ToString().Trim());
                            if ((results1 != null && results1.ItemArray.Length > 0))
                            {
                                flag = false;
                            }
                            if (results1 == null && results2 != null)
                            {
                                flag = true;
                            }
                            else if (results1 != null && results1 != null)
                            {
                                flag = false;
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                        flag1 = checkcountdataset(dtsbranch, "BranchID", branchcode);// check branchid  trong branch
                        if (flag)
                        {
                            No1 = No1 + 1;
                            row2["colNo"] = No1.ToString();
                            row2["colbranchCode"] = branchcode;
                            row2["colbranchName"] = branchName;
                            row2["colbranchNameMM"] = branchNameMM;
                            row2["colregionName"] = regionName;
                            row2["colbankCode"] = bankCode;
                            DataRow[] dtrlc = getdatarow(dtsOtherBank, "BankCode", bankCode);
                            if (dtrlc.Length > 0)
                            {
                                row2["colpartnerbankID"] = dtrlc[0].ItemArray[0].ToString().Trim();
                            }
                            DataRow[] dtrlc1 = getdatarow(dtsRegion, "RegionName", regionName);
                            if (dtrlc1.Length > 0)
                            {
                                row2["colregionID"] = dtrlc1[0].ItemArray[0].ToString().Trim();
                            }
                            //check trung trong viewstate
                            if (OTHERBRANCHUPDATE.Rows.Count > 0)
                            {
                                var results = OTHERBRANCHUPDATE.Rows.Cast<DataRow>()
                                    .FirstOrDefault(x => x.Field<string>("colbranchCode") == branchcode && x.Field<string>("colbankCode") == bankCode
                                                                                                        && x.Field<string>("colregionName") == regionName);

                                if (results == null)
                                {
                                    OTHERBRANCHUPDATE.Rows.Add(row2);
                                }
                            }
                            else
                            {
                                OTHERBRANCHUPDATE.Rows.Add(row2);
                            }
                        }
                        else if (flag1)
                        {
                            row3["colNo"] = No.ToString();
                            row3["colbranchCode"] = branchcode;
                            row3["colbranchName"] = branchName;
                            row3["colbranchNameMM"] = branchNameMM;
                            row3["colregionName"] = regionName;
                            row3["colbankCode"] = bankCode;
                            DataRow[] dtrlv = getdatarow(dtsOtherBank, "BankCode", bankCode);
                            if (dtrlv.Length > 0)
                            {
                                row3["colpartnerbankID"] = dtrlv[0].ItemArray[0].ToString().Trim();
                            }
                            else
                            {
                                tab.Rows.Remove(row);
                            }
                            DataRow[] dtrlv1 = getdatarow(dtsRegion, "RegionName", regionName);
                            if (dtrlv1.Length > 0)
                            {
                                row3["colregionID"] = dtrlv1[0].ItemArray[0].ToString().Trim();
                            }
                            else
                            {
                                tab.Rows.Remove(row);
                            }
                            //check trung trong viewstate
                            if (BRANCHUPDATE.Rows.Count > 0)
                            {
                                var results = BRANCHUPDATE.Rows.Cast<DataRow>()
                                    .FirstOrDefault(x => x.Field<string>("colbranchCode") == branchcode && x.Field<string>("colbankCode") == bankCode
                                                                                                        && x.Field<string>("colregionName") == regionName);
                                if (results == null)
                                {
                                    BRANCHUPDATE.Rows.Add(row3);
                                }
                            }
                            else
                            {
                                BRANCHUPDATE.Rows.Add(row3);
                            }
                        }
                    }
                }
            }

            if (OTHERBRANCH.Rows.Count == 0 && OTHERBRANCHUPDATE.Rows.Count == 0)
            {
                lblError.Text = "It is possible that the data has been duplicated. Please check and re-fill corrected data or contact our support center for more information.";
            }
            return true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), "");
            throw;
        }
    }
    private bool ValidateExcel(string branchcode, string branchname, string branchnameMM, string regionName, string bankcode)
    {
        try
        {
            bool flag1, flag2, flag3, flag4, flag5 = true;
            flag1 = checkcountdataset(dtsOtherBranch, "BranchCode", branchcode);// neu co thi false
            flag2 = checkcountdataset(dtsRegion, "RegionName", regionName); // neu co thi true
            flag3 = checkcountdataset(dtsOtherBank, "BankCode", bankcode); //neu co thi true
            if (!flag1 && flag2 && flag3)
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), "");
            throw;
        }
    }


    private bool Validateotherbranch(string partnerid, string branchcode, string branchname, string regionid, string branchnameMM)
    {
        try
        {
            bool flag1, flag2, flag3, flag4, flag5 = true;
            flag1 = checkcountdataset(dtsOtherBranch, "BranchCode", branchcode);// neu co thi false
            flag2 = checkcountdataset(dtsOtherBranch, "PartnerBankID", partnerid); // neu co thi true
            flag3 = checkcountdataset(dtsOtherBranch, "RegionID", regionid); //neu co thi true
            flag4 = checkcountdataset(dtsOtherBranch, "BranchName", branchname); //neu co thi true
            flag5 = checkcountdataset(dtsOtherBranch, "BranchNameMM", branchnameMM); //neu co thi true
            if (!flag1 || !flag2 || !flag3 || !flag4 || !flag5)
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), "");
            throw;
        }
    }

    private bool checkcountdataset(DataSet dts, string colname, string filtername)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = dts.Tables[0];
            DataRow[] filteredRows;
            if (filtername != "")
            {
                filteredRows = dt.Select(string.Format("{0} = '{1}'", dt.Columns[colname].ToString(), filtername.Trim()));
                return filteredRows.Length > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), "");
            throw;
        }
    }

    private bool checkcountdt(DataTable dt, string colname, string filtername)
    {
        try
        {
            DataRow[] filteredRows;
            if (filtername != "")
            {
                filteredRows = dt.Select(string.Format("{0} = '{1}'", dt.Columns[colname].ToString(), filtername.Trim()));
                return filteredRows.Length > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), "");
            throw;
        }
    }

    private DataRow[] getdatarow(DataSet dts, string colname, string filtername)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = dts.Tables[0];
            DataRow[] filteredRows;
            filteredRows = dt.Select(string.Format("{0} = '{1}'", dt.Columns[colname].ToString(), filtername.Trim()));
            return filteredRows;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), "");
            throw;
        }
    }

    protected void GVOtherBanchUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lbl_branchCode, lbl_No, lbl_BranchName, lbl_BranchNameMM, lbl_RegionName, lbl_Desc;
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
                lbl_branchCode = (Label)e.Row.FindControl("lbl_branchCode");
                lbl_BranchName = (Label)e.Row.FindControl("lbl_BranchName");
                lbl_BranchNameMM = (Label)e.Row.FindControl("lbl_BranchNameMM");
                lbl_RegionName = (Label)e.Row.FindControl("lbl_RegionName");

                lbl_Desc = (Label)e.Row.FindControl("lbl_Desc");

                lbl_No.Text = drv["colNo"].ToString();
                lbl_branchCode.Text = drv["colbranchCode"].ToString();
                lbl_BranchName.Text = drv["colbranchName"].ToString();
                lbl_BranchNameMM.Text = drv["colbranchNameMM"].ToString();
                lbl_RegionName.Text = drv["colregionName"].ToString();

                lbl_Desc.Text = drv["colDesc"].ToString();
            }
        }
        catch
        {

        }
    }
}