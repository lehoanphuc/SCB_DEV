using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.OleDb;
using SmartPortal.ExceptionCollection;
using System.Text;
using System.Threading;
using System.IO;
using SmartPortal.Common.Utilities;
using SmartPortal.IB;
using System.Configuration;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Web;

public partial class Widgets_IBCLGD_Widget : WidgetBase
{
    private Stopwatch sw = null;

    string IPCERRORCODE;
    string IPCERRORDESC;
    string errorcode = "";
    string errorDesc = "";
    private Object m_lock = new Object();
    private DataTable TABLE
    {
        get { return ViewState["TABLE"] as DataTable; }
        set
        {
            ViewState["TABLE"] = value;
        }
    }
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : Utility.createTableDocumnet(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

       

            try
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                load_unit(sender, e);
                pnCKL.Visible = false;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnPT.Visible = true;
                pnResult.Visible = false;
                pnHandInput.Visible = false;               
                #region load loai xac thuc
                SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
                DataTable dt = new DataTable();
                dt = objTran.LoadAuthenType(Session["userID"].ToString());
                ddlLoaiXacThuc.DataSource = dt;
                ddlLoaiXacThuc.DataTextField = "TYPENAME";
                ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
                ddlLoaiXacThuc.DataBind();
                ddlLoaiXacThuc_SelectedIndexChanged(null, null);
                #endregion
                DataTable handTable = innetTable();
                Session["handTable"] = handTable;
                DataTable fileTable = innetTable();
                Session["fileTable"] = fileTable;
                DataTable dtInfo = new DataTable();
                dtInfo = new SmartPortal.SEMS.Transactions().GetContractLevelByUserID(Session["userID"].ToString());
                if (dtInfo != null && dtInfo.Rows.Count > 0)
                {
                    hdContractLV.Value = dtInfo.Rows[0]["CONTRACTLEVELID"].ToString();
                    
                }
                
                if ((Session["accType"].ToString() != "IND"))
                {
                    LblDocument.Visible = true;
                    FUDocument.Visible = true;
                    LblDocumentExpalainion.Visible = true;
                }              
                LoadAccountInfo();
            }
            else
            {
                Type ty = sender.GetType();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private DataTable ReadFileExcelXlsx(string filePath)
    {
        try
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets[1];
                DataTable tbl = new DataTable();
                bool hasHeader = true;
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    var row = tbl.NewRow();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                    tbl.Rows.Add(row);
                }
                return tbl;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            return new DataTable();
        }

    }
    private DataTable ReadFileExcel(string filePath, string query)
    {
        // Create connection string variable. Modify the "Data Source"
        // parameter as appropriate for your environment.
        //String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
        //    "Data Source=" + filePath + ";" +
        //    "Extended Properties=Excel 8.0;";
        String sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + @";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";

        // Create connection object by using the preceding connection string.
        OleDbConnection objConn = new OleDbConnection(sConnectionString);

        // Open connection with the database.
        objConn.Open();

        // The code to follow uses a SQL SELECT command to display the data from the worksheet.

        // Create new OleDbCommand to return data from worksheet.
        OleDbCommand objCmdSelect = new OleDbCommand(query, objConn);

        // Create new OleDbDataAdapter that is used to build a DataSet
        // based on the preceding SQL SELECT statement.
        OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

        // Pass the Select command to the adapter.
        objAdapter1.SelectCommand = objCmdSelect;

        // Create new DataSet to hold information from the worksheet.
        DataSet objDataset1 = new DataSet();

        // Fill the DataSet with the information from the worksheet.
        objAdapter1.Fill(objDataset1, "XLData");

        // Clean up objects.
        objConn.Close();

        // Bind data to DataGrid control.
        return objDataset1.Tables[0];
    }

    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        try
        {
            //xoa 2 session chua cac TK can chuyen     

            if (ddlTransferType.SelectedValue == "F")
            {
                pnCKL.Visible = false;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnPT.Visible = true;
                pnResult.Visible = false;
                pnHandInput.Visible = false;

            }
            else
            {
                pnCKL.Visible = false;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnPT.Visible = false;
                pnResult.Visible = false;
                pnHandInput.Visible = true;
            }
        }
        catch
        {
        }
    }


    protected void btnLNext_Click(object sender, EventArgs e)
    {
        string a = Session["accType"].ToString();
        try
        {
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();

            //lay thong tin nguoi gui
            Hashtable hasSender = objAcct.loadInfobyAcct(ddlAccount.SelectedValue.Trim());

            if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                lblConfirmSenderAcctno.Text = ddlAccount.SelectedValue.Trim();
                lblConfirmSenderName.Text = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                if (double.Parse(hasSender["AVAILABLEBALANCE"].ToString().Trim()) < 0)
                {
                    lblError.Text = Resources.labels.sodukhongdudechuyenkhoan;
                    return;
                }
                lblConfirmBalance.Text = Utility.FormatMoney(Utility.FormatStringCore(hasSender["AVAILABLEBALANCE"].ToString().Trim()), hasSender["CURRENCYID"].ToString().Trim());
                lblCCYID.Text = hasSender["CURRENCYID"].ToString();
                ViewState["AccountBalanceAmount"] = Utility.isDouble(Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString().Trim()), false);
                if (txtLDesc.Text.Length > 200) txtLDesc.Text = txtLDesc.Text.Substring(0, 200);
                lblConfirmDesc.Text = Utility.KillSqlInjection(txtLDesc.Text);
                lblSenderBranch.Text = hasSender[SmartPortal.Constant.IPC.BRID].ToString();
            }
            else
            {
                lblError.Text = hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                return;
            }

            //upload file to host
            if (fuTransfer.HasFile)
            {
                string fileName = Session["userName"].ToString() + "_" + System.IO.Path.GetRandomFileName().Replace(".", "") + "_" + fuTransfer.FileName;
                string extension = System.IO.Path.GetExtension(fileName).ToLower();
                string filePath = Server.MapPath("~/widgets/IBCLGD/xlsFile/") + fileName;
                switch (extension)
                {
                    case ".xls":
                        break;
                    case ".xlsx":
                        break;
                    default:
                        lblError.Text = "Only Upload .xls .xlsx";
                        return;
                }
                fuTransfer.SaveAs(filePath);
                SmartPortal.Common.Log.WriteLogFile("Batch Transfer", "btnLNext_Click", "fileName", filePath);

                //read file excel
                DataTable tab;
                try
                {
                    sw = Stopwatch.StartNew();
                    tab = ReadFileExcel(filePath, "SELECT * FROM [Sheet1$]");
                    sw.Stop();
                    SmartPortal.Common.Log.WriteLogFile("Batch Transfer", "btnLNext_Click", "ReadFileExcel", " took " + sw.Elapsed.TotalSeconds.ToString("0.#######") + " seconds to load");
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    return;
                }
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
                }
                //if (tab.Rows.Count > 600)
                //{
                //    lblError.Text = "The system can support 600 records. Please try again!";
                //    goto FN;
                //}

                //vutt check limit
                Int64 TransLimit = 0;
                DataTable dt = new SmartPortal.SEMS.Transactions().GetLimitConfigbyContractLevel(Session["UserID"].ToString(), ddlAccount.Text, "", "IB000499", "", "BAT", hdContractLV.Value);
                if (dt != null && dt.Rows.Count != 0)
                {
                    TransLimit = Convert.ToInt64(dt.Rows[0]["TranLimit"]);
                }

                if (tab.Rows.Count > TransLimit && TransLimit != -1)
                {
                    lblError.Text = Resources.labels.soluonggiaodichmoilophainhohon + TransLimit.ToString();
                    goto FN;
                }
                Session["tab"] = tab;
                SmartPortal.Common.Log.WriteLogFile("Batch Transfer", "btnLNext_Click", "After Read File Excel", JsonConvert.SerializeObject(tab));

            }
            else
            {
                lblError.Text = Resources.labels.banvuilongchonfiledechuyenkhoanlo;
                goto FN;
            }

            if (Session["accType"].ToString() != "IND")
            {
                DataTable dt = Utility.UploadFile(FUDocument, lblError);
                    ViewState["TBLDOCUMENT"] = dt;
             
            }
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["imageuploadec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["imageuploadec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

        hdfIsFinish.Value = "";
        hdfIsError.Value = "";
        Timer1.Enabled = true;
        gvConfirm.DataSource = null;
        gvConfirm.DataBind();


        pnCKL.Visible = false;
        pnConfirm.Visible = true;
        pnOTP.Visible = false;
        pnPT.Visible = false;
        pnResult.Visible = false;

        Thread th = new Thread(() =>
        {
            CheckAccountInfor();
        });
        th.Start();
    FN:
        return;
    }

    public void CheckAccountInfor()
    {
        int i = 0;
        double totalBalance = 0;
        double totalFee = 0;
        double accountBalanceAmount = double.Parse(ViewState["AccountBalanceAmount"].ToString());
        DataTable fileTable = (DataTable)Session["fileTable"];
        fileTable.Rows.Clear();

        Session["TotalBalance"] = 0;
        Session["CheckAcctFinish"] = false;
        Session["BatchError"] = null;

        DataTable tab = (DataTable)Session["tab"];
        string debitAccountNo = ddlAccount.SelectedValue.Trim();
        string ccyid = lblCCYID.Text.Trim();
        string debitBranch = lblSenderBranch.Text.Trim();

        DataTable dataResult = new SmartPortal.IB.Transactions().ValidateBatchTransfer(tab, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            lblError.Text = IPCERRORDESC;
            return;
        }

        string[] arraystatusCd = { "S", "V", "CLS", "M" };
        foreach (DataRow r in dataResult.Rows)
        {
            try
            {
                i += 1;
                DataRow row = fileTable.NewRow();
                row["Stt"] = i.ToString();
                string creditAccountNo = r["ACCTNO"].ToString().Trim();
                double senderAmount = Convert.ToDouble(r["AMOUNT"].ToString().Trim());
                string creditStatus = r["STATUSCD"].ToString().Trim();
                string creditCustomerName = r["CUSTOMERNAME"].ToString().Trim();
                string creditCurencyID = r["CURRENCYID"].ToString().Trim();
                string creditBranchID = r["BRID"].ToString().Trim();
                string creditBranch = r["BRANCHNAME"].ToString().Trim();
                string creditInputName = r["INPUTNAME"].ToString().Trim();
                string coreErrorCode = r["ERRORCODE"].ToString().Trim().Equals("00") ? "0" : r["ERRORCODE"].ToString().Trim();
                string coreErrorDesc = r["ERRORDESC"].ToString().Trim();
                if (!string.IsNullOrEmpty(creditAccountNo))
                {
                    row["Account"] = creditAccountNo;
                    sw = Stopwatch.StartNew();

                    // check limit DEB
                    DataTable dt = new SmartPortal.SEMS.Transactions().GetLimitConfigbyContractLevel(Session["UserID"].ToString(), "", creditAccountNo, "IB000499", ccyid, "DEB", hdContractLV.Value);

                    if (dt.Rows.Count > 0)
                    {
                        DataTable dt1 = new SmartPortal.SEMS.Transactions().CaculateTransferTodayByAccountNo(debitAccountNo, creditAccountNo);

                        double amountLimit = Convert.ToDouble(dt.Rows[0]["TranLimit"]);
                        int countLimit = Convert.ToInt32(dt.Rows[0]["CountLimit"]);
                        int countTransfer = tab.Rows.Count + Convert.ToInt32(dt1.Rows[0]["NUMBERTRANSACTION"]);
                        double totalAmount = Convert.ToDouble(dt1.Rows[0]["TOTALAMOUNT"]);
                        double totalLimit = Convert.ToDouble(dt.Rows[0]["TranLimit"]);

                        if (totalAmount != 0 && totalLimit != 0)
                        {
                            if (countTransfer > countLimit && countLimit != -1)
                            {
                                row["ErrorDesc"] = Session["BatchError"] = Resources.labels.sogiaodichtrongngayquahanmuccuachinhanhghico;
                            }
                            else if (totalAmount > totalLimit && totalLimit != -1)
                            {
                                row["ErrorDesc"] = Session["BatchError"] = Resources.labels.tongsotienvuotquahanmuccuachinhanhghico;
                            }
                            else if (senderAmount > amountLimit && amountLimit != -1)
                            {
                                row["ErrorDesc"] = Session["BatchError"] = Resources.labels.sotienquahanmuccuachinhanhghico;
                            }
                        }

                    }
                    if (coreErrorCode != "0")
                    {
                        lblError.Text = coreErrorDesc;
                        row["ErrorDesc"] = coreErrorDesc;
                        Session["BatchError"] = coreErrorDesc;
                        goto END;
                    }
                    if (Array.IndexOf(arraystatusCd, creditStatus) != -1)
                    {
                        lblError.Text = Resources.labels.khongthechuyenkhoandentaikhoannay + "  <b>" + creditAccountNo + "</b>";
                        row["ErrorDesc"] = Resources.labels.khongthechuyenkhoandentaikhoannay;
                        Session["BatchError"] = Resources.labels.khongthechuyenkhoandentaikhoannay + "  : " + creditAccountNo;
                        goto END;
                    }
                    if (senderAmount == 0)
                    {
                        lblError.Text = Resources.labels.taikhoan + " <b>" + creditAccountNo + ":</b> " + Resources.labels.sotien + " " + Resources.labels.khonghople;
                        row["ErrorDesc"] = Resources.labels.sotien + " " + Resources.labels.khonghople;
                        Session["BatchError"] = Resources.labels.taikhoan + " " + creditAccountNo + " : " + Resources.labels.sotien + " " + Resources.labels.khonghople;
                        goto END;
                    }

                    row["User"] = creditCustomerName;
                    row["Name"] = creditInputName;
                    row["DebitBranch"] = debitBranch;
                    row["CreditBranch"] = creditBranch;
                    row["Amount"] = senderAmount;

                    // CHECK SAME CCYCD
                    if (!ccyid.Equals(creditCurencyID))
                    {
                        lblError.Text = Resources.labels.invalidacctCCYCDtotransfer + " - <b>" + creditAccountNo + "</b>";
                        row["ErrorDesc"] = Resources.labels.invalidacctCCYCDtotransfer;
                        Session["BatchError"] = Resources.labels.invalidacctCCYCDtotransfer + " - " + creditAccountNo;
                        goto END;
                    }

                    if (senderAmount <= 0)
                    {
                        lblError.Text = Resources.labels.taikhoan + " <b>" + creditAccountNo + ":</b> " + Resources.labels.sotien + " " + Resources.labels.khonghople;
                        row["ErrorDesc"] = Resources.labels.sotien + " " + Resources.labels.khonghople;
                        Session["BatchError"] = Resources.labels.taikhoan + " " + creditAccountNo + " : " + Resources.labels.sotien + " " + Resources.labels.khonghople;
                    }
                    else
                    {
                        totalBalance += senderAmount;
                    }

                    //tinh fee
                    DataTable dtFee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), "IB000499", senderAmount.ToString(), debitAccountNo, creditBranchID, ccyid, "");
                    string sedfee;
                    string revfee;
                    if (dtFee.Rows.Count != 0)
                    {
                        sedfee = Utility.FormatMoney(dtFee.Rows[0][SmartPortal.Constant.IPC.SEDFEE].ToString(), lblCCYID.Text.Trim());
                        revfee = Utility.FormatMoney(dtFee.Rows[0][SmartPortal.Constant.IPC.REVFEE].ToString(), lblCCYID.Text.Trim());
                        totalFee += double.Parse(sedfee);

                        row[SmartPortal.Constant.IPC.SEDFEE] = sedfee;
                        row[SmartPortal.Constant.IPC.REVFEE] = revfee;
                    }

                END:
                    fileTable.Rows.Add(row);

                    sw.Stop();
                    SmartPortal.Common.Log.WriteLogFile("Batch Transfer", "btnLNext_Click", i.ToString() + " CheckAccountInfor", " took " + sw.Elapsed.TotalSeconds.ToString("0.#######") + " seconds to load");
                }

            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.WriteLogFile("Batch Transfer", "btnLNext_Click", i.ToString() + " CheckAccountInfor", ex.Message);
            }
        }
        Session["fileTable"] = fileTable;
        Session["TotalBalance"] = totalBalance;
        Session["TotalFee"] = totalFee;
        Session["CheckAcctFinish"] = true;

        if (totalBalance <= 0)
        {
            lblError.Text = "Unable to get customer information. Please check Batch file again. ";
            Session["BatchError"] = "Unable to get customer information. Please check Batch file again. ";
        }
        else if (accountBalanceAmount < totalBalance + totalFee)
        {
            lblError.Text = Resources.labels.sodukhongdudechuyenkhoan;
            Session["BatchError"] = Resources.labels.sodukhongdudechuyenkhoan;
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["TotalBalance"] != null && Session["TotalFee"] != null)
            {
                lblTotalBalance.Text = Utility.FormatMoney(Session["TotalBalance"].ToString(), lblCCYID.Text.Trim()); ;
                lblTotalCCYID.Text = lblCCYID.Text;
                lblTotalFee.Text = Utility.FormatMoney(Session["TotalFee"].ToString(), lblCCYID.Text.Trim());
                lblTotalFeeCCYID.Text = lblCCYID.Text;
            };

            gvConfirm.DataSource = (DataTable)Session["fileTable"];
            gvConfirm.DataBind();

            if ((bool)Session["CheckAcctFinish"])
            {
                hdfIsFinish.Value = "Yes";
                imgLoading.Visible = false;
                Timer1.Enabled = false;
                if (Session["BatchError"] != null)
                {
                    hdfIsError.Value = Session["BatchError"].ToString();
                }
            }
        }
        catch
        {

        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            if (Utility.isDouble(lblAvailableBal.Text.Trim(), true) < Utility.isDouble(lblTotalBalance.Text.Trim(), true) + Utility.isDouble(lblTotalFee.Text.Trim(), true))
            {
                lblError.Text = "Balance not enough to make transfer";
                return;
            }

            if (Session["BatchError"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + Session["BatchError"].ToString() + "');</script>", false);
                return;
            }
            else
            {

                pnCKL.Visible = false;
                pnConfirm.Visible = false;
                pnOTP.Visible = true;
                pnPT.Visible = false;
                pnResult.Visible = false;
                LoadLoaiXacThuc();
            }
        }
        catch
        {
        }
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        lock (m_lock)
        {
            try
            {
                string OTPcode = txtOTP.Text.Trim();
                txtOTP.Text = "";
                btnSendOTP.Enabled = false;

                DataTable fileTable = (DataTable)Session["fileTable"];

                //ham thuc hien luu vao logtran
                DataTable BatchTable = new DataTable();
                DataColumn ACCTNO = new DataColumn("ACCTNO");
                DataColumn RECEIVERACCOUNT = new DataColumn("RECEIVERACCOUNT");
                DataColumn AMOUNT = new DataColumn("AMOUNT");
                DataColumn CCYID = new DataColumn("CCYID");
                DataColumn DEBITBRACHID = new DataColumn("DEBITBRACHID");
                DataColumn CREDITBRACHID = new DataColumn("CREDITBRACHID");
                DataColumn RECEIVERNAME = new DataColumn("RECEIVERNAME");
                DataColumn SENDERNAME = new DataColumn("SENDERNAME");
                //DataColumn TRANDESC = new DataColumn("TRANDESC");
                BatchTable.Columns.Add(ACCTNO);
                BatchTable.Columns.Add(RECEIVERACCOUNT);
                BatchTable.Columns.Add(AMOUNT);
                BatchTable.Columns.Add(CCYID);
                BatchTable.Columns.Add(DEBITBRACHID);
                BatchTable.Columns.Add(CREDITBRACHID);
                BatchTable.Columns.Add(RECEIVERNAME);
                BatchTable.Columns.Add(SENDERNAME);
                BatchTable.Columns.Add(SmartPortal.Constant.IPC.SEDFEE);
                BatchTable.Columns.Add(SmartPortal.Constant.IPC.REVFEE);
                //BatchTable.Columns.Add(TRANDESC);

                //BatchTable.Columns.AddRange(new[] { ACCTNO,RECEIVERACCOUNT,AMOUNT,CCYID,TRANDESC});
                foreach (DataRow r in fileTable.Rows)
                {
                    DataRow r1 = BatchTable.NewRow();
                    r1["ACCTNO"] = lblConfirmSenderAcctno.Text.Trim();
                    r1["RECEIVERACCOUNT"] = r["ACCOUNT"].ToString();
                    r1["AMOUNT"] = r["AMOUNT"].ToString();
                    r1["CCYID"] = lblCCYID.Text.Trim();
                    r1["RECEIVERNAME"] = r["USER"].ToString();
                    r1["DEBITBRACHID"] = r["DebitBranch"].ToString();
                    r1["CREDITBRACHID"] = r["CreditBranch"].ToString();
                    r1["SENDERNAME"] = lblConfirmSenderName.Text;
                    r1[SmartPortal.Constant.IPC.SEDFEE] = r[SmartPortal.Constant.IPC.SEDFEE].ToString();
                    r1[SmartPortal.Constant.IPC.REVFEE] = r[SmartPortal.Constant.IPC.REVFEE].ToString();

                    BatchTable.Rows.Add(r1);
                }
                DataTable tbldocument = (DataTable)ViewState["TBLDOCUMENT"];
                new SmartPortal.IB.Transactions().BatchTransfer(BatchTable, lblConfirmDesc.Text, Session["userID"].ToString(), lblConfirmSenderAcctno.Text.Trim(), Utility.FormatMoneyInput(lblTotalBalance.Text.Trim(), lblCCYID.Text),
                    Utility.FormatMoneyInput(lblTotalFee.Text.Trim(), lblCCYID.Text),
                    ddlLoaiXacThuc.SelectedValue, OTPcode, lblCCYID.Text, tbldocument,Session["accType"].ToString(), ref errorcode, ref errorDesc);
                if (errorcode == "0")
                {
                    lblError.Text = Resources.labels.transactionsuccessful;
                }
                else
                {
                    switch (errorcode.ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.IPC:
                            throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        case SmartPortal.Constant.IPC.ERRORCODE.OVERLIMIT:
                            lblError.Text = Resources.labels.amountovertransactionlimit;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblError.Text = errorDesc;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                            lblError.Text = Resources.labels.wattingbankapprove;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                            lblError.Text = Resources.labels.chochutaikhoanduyet;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.STVQHMDG:
                            lblError.Text = Resources.labels.sotienvuothanmucgiaodich;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.STVQHMGDTN:
                            lblError.Text = Resources.labels.sotienvuotquatonghanmuctrongngay;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.SLGDVQSLGDTN:
                            lblError.Text = Resources.labels.solangiaodichvuotquasolangiaodichtrongngay;
                            return;
                        case "9909":
                            lblError.Text = Resources.labels.sotienvuotquatonghanmuctrongngay;
                            return;
                        case "9907":
                            lblError.Text = Resources.labels.solangiaodichvuotquasolangiaodichtrongngay;
                            return;
                        case "9906":
                            lblError.Text = Resources.labels.sotienquahanmuccuachinhanhghico;
                            return;
                        case "9905":
                            lblError.Text = Resources.labels.tongsotienvuotquahanmuccuachinhanhghico;
                            return;
                        case "9904":
                            lblError.Text = Resources.labels.sogiaodichtrongngayquahanmuccuachinhanhghico;
                            return;
						case "9004":
                            lblError.Text = Resources.labels.Notyetsetupapprovalworkflow;
                            return;
                        case "-13524":
                            lblError.Text = Resources.labels.destacccountinvalid;
                            return;
                        default:
                            throw new Exception();
                    }
                }

                pnCKL.Visible = false;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnPT.Visible = false;
                pnResult.Visible = true;

                string errorCode = "0";
                SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
                DataSet dsDetailAcc = new DataSet();
                dsDetailAcc = acct.GetInfoDD(Session["userID"].ToString(), lblConfirmSenderAcctno.Text.Trim(), ref errorCode, ref errorDesc);

                lblResultAccount.Text = lblConfirmSenderAcctno.Text;
                lblResultUser.Text = lblConfirmSenderName.Text;
                lblResultBalance.Text = Utility.FormatMoney(Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBAL].ToString()), lblCCYID.Text.Trim());
                lblResultCCYID.Text = lblCCYID.Text;
                lblResultDesc.Text = txtLDesc.Text;

                lblTotalBalanceFN.Text = lblTotalBalance.Text;
                lblTotalCCYIDFN.Text = lblTotalCCYID.Text;
                lblTotalFeeFN.Text = lblTotalFee.Text;
                lblTotalFeeCCYIDFN.Text = lblTotalFeeCCYID.Text;

                //read file excel
                DataTable ddTable = new DataTable();
                if (ddlTransferType.SelectedValue == "F")
                {
                    ddTable = (DataTable)Session["fileTable"];
                }
                else
                {
                    ddTable = (DataTable)Session["handTable"];
                }

                //xuat len man hinh
                gvResult.DataSource = ddTable;
                gvResult.DataBind();

                //LanNTH - 03082018 - add detail batch tranfer when print
                StringWriter tw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                pnDetails.RenderControl(hw);
                Session["details"] = tw.ToString();


                //vutt add print AYA request
                Hashtable hasPrint = new Hashtable();
                hasPrint.Add("status", Resources.labels.thanhcong);
                hasPrint.Add("senderAccount", lblConfirmSenderAcctno.Text.Trim());
                hasPrint.Add("senderBalance", lblConfirmBalance.Text.Trim());
                hasPrint.Add("ccyid", lblCCYID.Text.Trim());
                hasPrint.Add("senderName", lblConfirmSenderName.Text.Trim());
                //hasPrint.Add("recieverAccount", lblReceiverAccount.Text);
                //hasPrint.Add("recieverName", lblReceiverName.Text);
                //hasPrint.Add("transferType", lblHinhThuc.Text);
                hasPrint.Add("totalamount", lblTotalBalance.Text.Trim());
                hasPrint.Add("amountchu", Utility.IntegerToWords((long)(double)Session["TotalBalance"]) + " " + lblCCYID.Text);
                hasPrint.Add("feeType", "Sender");
                hasPrint.Add("feeAmount", lblTotalFee.Text.Trim());
                hasPrint.Add("desc", lblConfirmDesc.Text.Trim());
                hasPrint.Add("tranID", "");
                hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                hasPrint.Add("senderBranch", Utility.FormatStringCore(lblSenderBranch.Text));
                //hasPrint.Add("receiverBranch", Utility.FormatStringCore(lblReceiverBranch.Text));
                Session["print"] = hasPrint;

                btnPrint.Visible = true;
                btnView.Visible = true;

                btnSendOTP.Enabled = true;
            }
            catch (IPCException IPCex)
            {
                SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);
                btnSendOTP.Enabled = true;
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                btnSendOTP.Enabled = true;
            }
        }

    }
    protected void btnBack2_Click(object sender, EventArgs e)
    {
        try
        {
            pnCKL.Visible = false;
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnPT.Visible = true;
            pnResult.Visible = false;
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=116"));
        }
        catch
        {
        }
    }
    protected void btnBackA_Click(object sender, EventArgs e)
    {
        try
        {
            pnCKL.Visible = false;
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnPT.Visible = false;
            pnResult.Visible = false;
        }
        catch
        {
        }
    }
    private DataTable innetTable()
    {
        DataTable tempTable = new DataTable();
        DataColumn col1 = new DataColumn("Stt");
        DataColumn col2 = new DataColumn("Account");
        DataColumn col3 = new DataColumn("User");
        DataColumn col4 = new DataColumn("Name");
        DataColumn col5 = new DataColumn("Amount");
        DataColumn col6 = new DataColumn("ErrorDesc");
        DataColumn col7 = new DataColumn("DebitBranch");
        DataColumn col8 = new DataColumn("CreditBranch");
        DataColumn col9 = new DataColumn(SmartPortal.Constant.IPC.SEDFEE);
        DataColumn col10 = new DataColumn(SmartPortal.Constant.IPC.REVFEE);
        tempTable.Columns.AddRange(new DataColumn[] { col1, col2, col3, col4, col5, col6, col7, col8, col9, col10 });
        //tempTable.Columns.AddRange(new DataColumn[] { col1, col2, col3, col4, col5 });

        return tempTable;
    }
    private void CreateTable(DataTable ddTable)
    {
        try
        {

            //xuat len man hinh
            StringBuilder sT = new StringBuilder();
            sT.Append("<table class='style11' cellpadding='5' cellspacing='0'>");
            sT.Append("<tr class='thtr'>");
            sT.Append("<td class='thtdff'>");
            sT.Append(Resources.labels.sotaikhoan);
            sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append(Resources.labels.nguoithuhuong);
            sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append(Resources.labels.sotien);
            sT.Append("</td>");
            //sT.Append("<td class='thtd'>");
            //sT.Append("Diễn giải");
            //sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append("");
            sT.Append("</td>");
            sT.Append("</tr>");
            foreach (DataRow row in ddTable.Rows)
            {
                sT.Append("<tr>");
                sT.Append("<td class='thtdff'>");
                sT.Append(row[0].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row[1].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row[2].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row[3].ToString());
                sT.Append("</td>");
                sT.Append("<td class='thtd'>");
                sT.Append(row[4].ToString());
                sT.Append("</td>");
                sT.Append("</tr>");
            }
            sT.Append("</table>");

            //ltrHL.Text = sT.ToString();
        }
        catch
        {
        }
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable handTable = (DataTable)Session["handTable"];
            DataRow row = handTable.NewRow();
            row["Stt"] = (handTable.Rows.Count + 1).ToString();
            row["Account"] = txtDestAccount.Text.Trim();
            row["User"] = "Trần Tuấn Vũ";
            row["Amount"] = txtAmount.Text; ;
            row["Desc"] = txtDesc.Text.Trim();

            handTable.Rows.Add(row);

            Session["handTable"] = handTable;

            //hien len luoi
            gvHandTransfer.DataSource = handTable;
            gvHandTransfer.DataBind();
        }
        catch
        {
        }
    }

    protected void gvHandTransfer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable handTable = (DataTable)Session["handTable"];
            handTable.Rows.RemoveAt(e.RowIndex);
            Session["handTable"] = handTable;

            gvHandTransfer.DataSource = handTable;
            gvHandTransfer.DataBind();
        }
        catch
        {
        }
    }
    protected void gvFileTransfer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable fileTable = (DataTable)Session["fileTable"];
            fileTable.Rows.RemoveAt(e.RowIndex);
            Session["fileTable"] = fileTable;

            for (int i = 0; i < fileTable.Rows.Count; i++)
            {
                fileTable.Rows[i]["stt"] = i + 1;
            }

            gvConfirm.DataSource = fileTable;
            gvConfirm.DataBind();
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnBack12_Click(object sender, EventArgs e)
    {
        pnCKL.Visible = true;
        pnConfirm.Visible = false;
        pnOTP.Visible = false;
        pnPT.Visible = false;
        pnResult.Visible = false;
        pnHandInput.Visible = false;
    }
    protected void btnNext19_Click(object sender, EventArgs e)
    {
        try
        {
            pnCKL.Visible = false;
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnPT.Visible = false;
            pnResult.Visible = false;
            pnHandInput.Visible = false;

            //read file excel
            DataTable ddTable = (DataTable)Session["handTable"];

            //xuat len man hinh
            gvConfirm.DataSource = ddTable;
            gvConfirm.DataBind();
        }
        catch
        {
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=86"));
    }
    protected void gvConfirm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblSTT, lblAccount, lblUser, lblName, lblAmount, lblErrorDesc;


            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.TableSection = TableRowSection.TableHeader;
                e.Row.Cells[2].Attributes.Add("data-breakpoints", "xs");
                e.Row.Cells[3].Attributes.Add("data-breakpoints", "xs");
                e.Row.Cells[5].Attributes.Add("data-breakpoints", "xs");
            }
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                TableRow tbRow = (TableRow)(e.Row.Cells[0].Controls[0].Controls[0]);
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + " :</strong>";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                lblSTT = (Label)e.Row.FindControl("lblSTT");
                lblAccount = (Label)e.Row.FindControl("lblAccount");
                lblUser = (Label)e.Row.FindControl("lblUser");
                lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblErrorDesc = (Label)e.Row.FindControl("lblErrorDesc");
                lblName = (Label)e.Row.FindControl("lblName");

                lblSTT.Text = drv["STT"].ToString();
                lblAccount.Text = drv["ACCOUNT"].ToString();
                lblUser.Text = drv["USER"].ToString();
                lblAmount.Text = Utility.FormatMoney(drv["AMOUNT"].ToString(), lblCCYID.Text.Trim());
                lblErrorDesc.Text = drv["ErrorDesc"].ToString();
                lblName.Text = drv["NAME"].ToString();
                if (lblUser.Text.ToUpper() != lblName.Text.ToUpper())
                {
                    lblErrorDesc.Text = Resources.labels.tennguoinhankhongdung;
                }
                else if (!string.IsNullOrEmpty(lblErrorDesc.Text))
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }

            }
        }
        catch
        {
        }
    }
    protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblSTT, lblAccount, lblUser, lblAmount;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.TableSection = TableRowSection.TableHeader;
                e.Row.Cells[2].Attributes.Add("data-breakpoints", "xs");
                e.Row.Cells[3].Attributes.Add("data-breakpoints", "xs");
                e.Row.Cells[5].Attributes.Add("data-breakpoints", "xs");
            }
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                TableRow tbRow = (TableRow)(e.Row.Cells[0].Controls[0].Controls[0]);
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + " :</strong>";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                lblSTT = (Label)e.Row.FindControl("lblSTT");
                lblAccount = (Label)e.Row.FindControl("lblAccount");
                lblUser = (Label)e.Row.FindControl("lblUser");
                lblAmount = (Label)e.Row.FindControl("lblAmount");

                lblSTT.Text = drv["STT"].ToString();
                lblAccount.Text = drv["ACCOUNT"].ToString();
                lblUser.Text = drv["USER"].ToString();
                lblAmount.Text = Utility.FormatMoney(drv["AMOUNT"].ToString(), lblCCYID.Text.Trim());
            }
        }
        catch
        {
        }
    }
    protected void ddlLoaiXacThuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAction.Enabled = false;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
    }

    private void LoadLoaiXacThuc()
    {
        try
        {
            ViewState["isSendFirst"] = 1;
            btnAction.Enabled = false;
            SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
            DataTable dt = objTran.LoadAuthenType(Session["userID"].ToString());
            ddlLoaiXacThuc.DataSource = dt;
            ddlLoaiXacThuc.DataTextField = "TYPENAME";
            ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
            ddlLoaiXacThuc.DataBind();

            btnSendOTP.Text = Resources.labels.send;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {

        try
        {
            ddlLoaiXacThuc.Enabled = false;
            btnAction.Enabled = true;
            btnSendOTP.Text = Resources.labels.resend;
            if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
            {
                SmartPortal.SEMS.OTP.SendSMSOTP(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            }
            else
            {
                SmartPortal.SEMS.OTP.SendSMSOTPCORP(Session["userID"].ToString(), ddlLoaiXacThuc.SelectedValue.ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                lbnotice.Visible = true;
            }
            if (IPCERRORCODE != "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
            }
            switch (IPCERRORCODE)
            {
                case "0":
                    int time;
                    if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
                    {
                        time = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OTPtimeexpires"]) ? int.Parse(ConfigurationManager.AppSettings["OTPtimeexpires"].ToString()) : 20;
                    }
                    else
                    {
                        time = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OTPCorptimeexpires"]) ? int.Parse(ConfigurationManager.AppSettings["OTPCorptimeexpires"].ToString()) : 20;
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "countdownOTP(" + time + ");", true);
                    ViewState["timeReSendOTP"] = DateTime.Now.AddSeconds(time);
                    break;
                case "7003":
                    lblError.Text = Resources.labels.notregotp;
                    btnAction.Enabled = false;
                    break;
                default:
                    lblError.Text = IPCERRORDESC;
                    btnAction.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlAccount_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountInfo();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "getnamefile();", true);
    }
    private void LoadAccountInfo()
    {
        try
        {
            string account = ddlAccount.SelectedItem.Text.ToString();
            string Acctype = ddlAccount.SelectedItem.Value.ToString();
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string User = Session["userID"].ToString();
            Account acct = new Account();
            DataSet ds = new DataSet();
            ds = acct.GetInfoDD(User, account, ref ErrorCode, ref ErrorDesc);
            ShowDD(account, ds);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBBuyTopup_Widget", "LoadAccountInfo", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void ShowDD(string acctno, DataSet ds)
    {
        try
        {
            if (ds.Tables[0].Rows.Count == 1)
            {
                lblAvailableBal.Text = Utility.FormatMoney(Utility.FormatStringCore(ds.Tables[0].Rows[0]["AVAILABLEBAL"].ToString()), ds.Tables[0].Rows[0]["CURRENCYID"].ToString().Trim());
                lblAvailableBalCCYID.Text = ds.Tables[0].Rows[0]["CURRENCYID"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void load_unit(object sender, EventArgs e)
    {
        try
        {
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;

            DataSet dsCustInfo = new SmartPortal.IB.Account().GetCustIDCustType(Session["userID"].ToString(), ref ErrorCode, ref ErrorDesc);
            if (ErrorCode == "0" && dsCustInfo.Tables[0].Rows.Count == 1)
            {
                string CfCode = dsCustInfo.Tables[0].Rows[0]["CFCODE"].ToString().Replace(" ", "");
                string CFType = dsCustInfo.Tables[0].Rows[0]["CFTYPE"].ToString().Replace(" ", "");
                DataSet ds = new SmartPortal.IB.Account().GetTKKH(CfCode, CFType, Session["userID"].ToString(), "IB000201", "IB000201", ref ErrorCode, ref ErrorDesc);
                DataTable dt = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Select("STATUSCD = 'A' AND TYPEID IN ('DD', 'CD')").CopyToDataTable() : null;
                TABLE = dt;
                //ds.Tables[0].DefaultView.RowFilter = "";
                ddlAccount.DataSource = TABLE;
                ddlAccount.DataTextField = "ACCOUNTNO";
                ddlAccount.DataValueField = "ACCOUNTNO";
                ddlAccount.DataBind();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}
