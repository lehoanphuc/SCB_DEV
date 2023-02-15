using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Text;
using System.Web.UI;
using SmartPortal.IB;
using SmartPortal.ExceptionCollection;
using System.Globalization;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;

public partial class Widgets_IBTransactionHistory1_Widget : WidgetBase
{

    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    public DataTable TBLDATA
    {
        get { return ViewState["TBLDATA"] != null ? (DataTable)ViewState["TBLDATA"] : CreateTableExport(); }
        set { ViewState["TBLDATA"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (!IsPostBack)
            {
                LoadAccount();
                if (!string.IsNullOrEmpty(SmartPortal.Common.Encrypt.GetURLParam(HttpContext.Current.Request.RawUrl)["ACCTNO"]))
                {
                    ddlAccount.SelectedValue = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ACCTNO"].ToString();
                }
                LoadAccountInfo();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ShowDetailDD(ddlAccount.SelectedItem.Value.ToString());
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            ltrTH.Text = "";
            LoadAccountInfo();
            btnExport.Visible = true;
            btnPrint.Visible = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    #region Load Info
    private void LoadAccount()
    {
        try
        {
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            DataSet ds = new DataSet();
            Account acct = new Account();
            ds = acct.GetInfoAcct(Session["userID"].ToString(), "IB000104", "", ref ErrorCode, ref ErrorDesc);
            ddlAccount.DataSource = ds;
            ddlAccount.DataTextField = "ACCTNOTYPE";
            ddlAccount.DataValueField = "ACCTNO";
            ddlAccount.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadAccountInfo()
    {
        try
        {
            string account = ddlAccount.SelectedItem.Value.ToString();
            string Acctype = hdAccountType.Value = ddlAccount.SelectedItem.Text.Split('-')[1].ToString();
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string User = Session["userID"].ToString();
            Account acct = new Account();
            Hashtable hashtable = new Hashtable();
            switch (Acctype.Trim())
            {
                case "CD":
                    pnDD.Visible = true;
                    pnCTGG.Visible = true;
                    hashtable = acct.GetInfoAccount(User, account, ref ErrorCode, ref ErrorDesc);
                    ShowDD(hashtable);
                    pnFD.Visible = false;
                    break;
                case "DD":
                    pnDD.Visible = true;
                    pnCTGG.Visible = true;
                    hashtable = acct.GetInfoAccount(User, account, ref ErrorCode, ref ErrorDesc);
                    ShowDD(hashtable);
                    pnFD.Visible = false;
                    break;
                case "FD":
                    pnDD.Visible = true;
                    pnCTGG.Visible = true;
                    hashtable = acct.GetInfoAccountFD(User, account, ref ErrorCode, ref ErrorDesc);
                    ShowFD(hashtable);
                    pnCTGG.Visible = false;
                    pnFD.Visible = true;
                    break;
                case "WL":
                    pnDD.Visible = true;
                    pnCTGG.Visible = true;
                    hashtable = acct.GetInfoAccount(User, account, ref ErrorCode, ref ErrorDesc);
                    ShowDD(hashtable);
                    pnFD.Visible = false;
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    
    
    private void ShowPopUpMsg(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
    }

    #endregion
    #region Show Panel
    private void ShowDD(Hashtable hashtable)
    {
        try
        {
            if (hashtable != null)
            {
                lblAccountNumber_DD.Text = hashtable["ACCOUNTNO"].ToString();
                lblAccountName_DD.Text = hashtable["FULLNAME"].ToString();
                lblCurrency_DD.Text = hashtable["CCYID"].ToString().Equals("LAK")? "VND": hashtable["CCYID"].ToString();
                lblCB_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hashtable["BALANCE"].ToString()), hashtable["CCYID"].ToString());
                lblAB_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hashtable["AVAILABLEBALANCE"].ToString()), hashtable["CCYID"].ToString());
                lblAB_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hashtable["AVAILABLEBALANCE"].ToString()), hashtable["CCYID"].ToString());
                lblDateOpen.Text = hashtable["OPENDATE"].ToString();
                lblIR_DD.Text = hashtable["BONUSRATE"].ToString();
                lblBranch_DD.Text = hashtable["BRANCHNAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void ShowFD(Hashtable hashtable)
    {
        try
        {
            if (hashtable != null)
            {
                lblAccountNumber_DD.Text = hashtable["ACCTNO"].ToString();
                lblAccountName_DD.Text = hashtable["ACCTNAME"].ToString();
                lblCurrency_DD.Text = hashtable["CURRENCYID"].ToString().Equals("LAK")?"VND": hashtable["CURRENCYID"].ToString();
                lblFromDateFD.Text = hashtable["FROMDATE"].ToString();
                lblToDateFD.Text = hashtable["TODATE"].ToString();
                lblCB_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hashtable["BALANCE"].ToString()), hashtable["CURRENCYID"].ToString());
                lblAB_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hashtable["BALANCE"].ToString()), hashtable["CURRENCYID"].ToString());
                lblDateOpen.Text = hashtable["ORGDATE"].ToString();
                lblCategory.Text = hashtable["CATNAME"].ToString();
                lblDebitAcct.Text = hashtable["DDACCOUNT"].ToString();
                lblIR_DD.Text = hashtable["INTERESTTRATE"].ToString(); ;
                //lblBranch_DD.Text = hashtable["BRANCHNAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    #endregion
    #region Show Detail transaction
    private void ShowDetailDD(string acctno)
    {
        try
        {
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            int stt = 1;
            double openingBalance = 0;
            double endingBalance = 0;
            double totalDebit = 0;
            double totalCredit = 0;
            string lastID = "0";

            string Acctype = ddlAccount.SelectedItem.Text.Split('-')[1].ToString();

            DataSet ds = new DataSet();
            Account acct = new Account();
            DataSet dsMap = new DataSet();
            Transactions Trans = new Transactions();
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                //kiem tra ngay thang hop le va ngày bat dau cach ngay hien tai khong qua 3 thang
                if (SmartPortal.Common.Utilities.Utility.IsDateTime1(txtFromDate.Text.Trim()) > SmartPortal.Common.Utilities.Utility.IsDateTime1(txtToDate.Text.Trim()))
                {
                    // throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.TODATEGREATERTHANFROMDATE);
                    lblError.Text = Resources.labels.ngayketthucphailonhonngaybatdau;
                    return;
                }
                if (DateTime.Now > SmartPortal.Common.Utilities.Utility.IsDateTime1(txtFromDate.Text.Trim()).AddMonths(3))
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.THREEMONTH);
                }
                Session["searchdate"] = "From Date: " + txtFromDate.Text.ToString() + " To Date: " + txtToDate.Text.ToString();
                switch (Acctype.Trim())
                {
                    case "WL":
                        ds = acct.getTransactionHisWalletAcct(Session["userID"].ToString(), "WL_LOADTRANHIS", acctno, txtFromDate.Text, txtToDate.Text, lastID, ref ErrorCode, ref ErrorDesc);
                        break;
                    default:
                        ds = acct.getTransactionHis(Session["userID"].ToString(), "MB000105", acctno, txtFromDate.Text, txtToDate.Text, lastID, ref ErrorCode, ref ErrorDesc);
                        break;
                }
            }
            else
            {
                Session["searchdate"] = "From Date: " + DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy") + " To Date: " + DateTime.Now.ToString("dd/MM/yyyy");
                switch (Acctype.Trim())
                {
                    case "WL":
                        ds = acct.getTransactionHisWalletAcct(Session["userID"].ToString(), "WL_LOADTRANHIS", acctno, DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"), lastID, ref ErrorCode, ref ErrorDesc);
                        break;
                    default:
                        ds = acct.getTransactionHis(Session["userID"].ToString(), "MB000105", acctno, DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"), lastID, ref ErrorCode, ref ErrorDesc);
                        break;
                }
            }

            if (ErrorCode != "0")
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
            }
            StringBuilder sT = new StringBuilder();

            sT.Append("<table class='style1 table table-bordered table-hover footable' data-paging='true'>");
            sT.Append("<thead>");
            sT.Append("<tr>");

            sT.Append("<th align='center' width='5%'>");
            sT.Append(Resources.labels.stt);
            sT.Append("</th>");

            sT.Append("<th align='center' width='20%'>");
            sT.Append(Resources.labels.ngaygiogiaodich);
            sT.Append("</th>");

            sT.Append("<th align='center' width='20%'>");
            sT.Append(Resources.labels.loaigiaodich);
            sT.Append("</th>");

            sT.Append("<th align='center' width='20%'>");
            sT.Append(Resources.labels.sotien);
            sT.Append("</th>");

            sT.Append("<th data-breakpoints='xs' align='center' width='35%'>");
            sT.Append(Resources.labels.desc);
            sT.Append("</th>");

            sT.Append("</tr>");
            sT.Append("</thead>");

            if (ds != null && ds.Tables.Count > 0)
            {
                btnExport.Visible = true;
                btnPrint.Visible = true;
                int i = 0;
                ViewState["TBLDATA"] = CreateTableExport();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    i++;
                    DataRow newRow = TBLDATA.NewRow();
                    newRow["No"] = i.ToString();
                    newRow["Transaction date"] = Utility.FormatDatetime(row["TRANDATE"].ToString(), "dd/MM/yyyy", DateTimeStyle.Date) + " " + row["TRANTIME"].ToString();
                    newRow["Transaction type"] = row["TRANNAME"].ToString();
                    if (row.Field<string>("TRANTYPE").Equals("D"))
                    {
                        newRow["Amount"] = "- " + Utility.FormatMoney(Utility.FormatStringCore(row["AMOUNT"].ToString()), row["CCYID"].ToString().Trim());
                    }
                    else
                    {
                        newRow["Amount"] = "+ " + Utility.FormatMoney(Utility.FormatStringCore(row["AMOUNT"].ToString()), row["CCYID"].ToString().Trim());
                    }
                    newRow["Currency"] = row["CCYID"].ToString().Equals("LAK")?"VND": row["CCYID"].ToString().Trim();
                    newRow["Description"] = Utility.FormatStringCore(row["DESCRIPTION"].ToString());
                    newRow["Type"] = Utility.FormatStringCore(row["TRANTYPE"].ToString() == "D" ? "Debit":"Credit" );
                    TBLDATA.Rows.Add(newRow);
                    
                    sT.Append("<tr>");

                    sT.Append("<td>");
                    sT.Append(stt++);
                    sT.Append("</td>");

                    sT.Append("<td>");
                    sT.Append(Utility.FormatDatetime(row["TRANDATE"].ToString(), "dd/MM/yyyy", DateTimeStyle.Date) + " " + row["TRANTIME"].ToString());
                    sT.Append("</td>");


                    sT.Append("<td>");
                    sT.Append(Utility.FormatStringCore(row["TRANNAME"].ToString()));
                    sT.Append("</td>");

                    sT.Append("<td style='text-align:right'>");
                    if (row.Field<string>("TRANTYPE").Equals("D"))
                    {
                        sT.Append("- " + Utility.FormatMoney(Utility.FormatStringCore(row["AMOUNT"].ToString()), row["CCYID"].ToString().Trim()) + " " + row["CCYID"].ToString().Trim());
                    }
                    else
                    {
                        sT.Append("+ " + Utility.FormatMoney(Utility.FormatStringCore(row["AMOUNT"].ToString()), row["CCYID"].ToString().Trim()) + " " + row["CCYID"].ToString().Trim());
                    }
                    sT.Append("</td>");

                    sT.Append("<td>");
                    sT.Append(Utility.FormatStringCore(row["DESCRIPTION"].ToString()));
                    sT.Append("</td>");


                    sT.Append("</tr>");
                }
            }
            else
            {
                btnExport.Visible = false;
                btnPrint.Visible = false;
            }
            sT.Append("</table>");


            ltrTH.Text = sT.ToString();
            Session["tmpl"] = sT.ToString();
            Session["acctpl"] = ddlAccount.SelectedValue.ToString();

            //btnExport.Visible = IBCheckPermitPageAction(IPC.ACTIONPAGE.EXPORT);
            btnPrint.Visible = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    #endregion
    public DataTable CreateTableExport()
    {
        DataTable tblExport = new DataTable();
        DataColumn colNo = new DataColumn("No");
        DataColumn colTranDate = new DataColumn("Transaction date");
        DataColumn colTranType = new DataColumn("Transaction type");
        DataColumn colAmount = new DataColumn("Amount");
        DataColumn colCCYID = new DataColumn("Currency");
        DataColumn colDesc = new DataColumn("Description");
        DataColumn colType = new DataColumn("Type");

        tblExport.Columns.Add(colNo);
        tblExport.Columns.Add(colTranDate);
        tblExport.Columns.Add(colTranType);
        tblExport.Columns.Add(colAmount);
        tblExport.Columns.Add(colCCYID);
        tblExport.Columns.Add(colDesc);
        tblExport.Columns.Add(colType);
        return tblExport;

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            //if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.EXPORT)) return;

            //string str = "<div style='font-weight: 10pt;  font-size: 14px; font-family: Tahoma, Verdana, Arial, Helvetica, sans-serif;text-align: center;'><b>" + Session["searchdate"].ToString() + "<br/><br/></b></div>"
            //+ "<div><b><br/>" + Resources.labels.chitietgiaodich.ToUpper() + "<br/><br/></b></div>";
            //str += "<p>Account Number: " + lblAccountNumber_DD.Text + "</p>";
            //str += "<p>Currency: " + lblCurrency_DD.Text + "</p>";
            //str += "<p>Account Name " + lblAccountName_DD.Text + "</p>";
            //str += Session["tmpl"].ToString();

            //SmartPortal.Common.ExportToFile.ExportStringToExcel("Statement", str);
            DataTable dt = new DataTable();
            dt = TBLDATA;
            //SmartPortal.Common.ExportToFile.ExportToExcel(TBLDATA, "Statement");
            //SmartPortal.Common.Log.WriteLogFile("Batch Transfer", "btnLNext_Click", "After Read File Excel", JsonConvert.SerializeObject(dt));
            ExportExcel(TBLDATA, "Statement", Resources.labels.chitietgiaodichtaikhoanthanhtoan);

        }
        catch (Exception ex)
        {
 
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["print"] == null)
            {
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
            }
        }
        catch
        {

        }
    }
    protected void ExportExcel(DataTable dt, string fileName, string headerFile)
    {

        //Create a dummy GridView
        GridView GridView1 = new GridView();
        GridView1.AllowPaging = false;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        //Add Header
        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell = new TableCell();
        HeaderCell.Text = Resources.labels.chuyenkhoantronghethongtheolo;
        HeaderCell.Font.Size = 18;
        HeaderCell.ColumnSpan = 7;
        HeaderGridRow.Cells.Add(HeaderCell);
        HeaderGridRow.Attributes.Add("style", "text-align:center;font-weight:bold");
        GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.AddHeader("content-disposition",
         "attachment;filename=" + fileName + ".xls");
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            //Apply text style to each Row
            GridView1.Rows[i].Attributes.Add("class", "textmode");
        }
        GridView1.RenderControl(hw);

        //style to format numbers to string
        string style = @"<style> TD { mso-number-format:\@; } </style>";
        HttpContext.Current.Response.Write(style);
        HttpContext.Current.Response.Output.Write(sw.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
}
