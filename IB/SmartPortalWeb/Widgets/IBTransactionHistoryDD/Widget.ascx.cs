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
using SmartPortal.ExceptionCollection;
using System.Globalization;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBTransactionHistoryDD_Widget : WidgetBase
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            btnExport.Visible = false;
            btnPrint.Visible = false;

            if (!IsPostBack)
            {
                string ErrorCode = string.Empty;
                string ErrorDesc = string.Empty;
                DataSet ds = new DataSet();
                Account acct = new Account();
                
                ds = acct.getAccount(Session["userID"].ToString(), "IB000200", "CD", ref ErrorCode, ref ErrorDesc);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.USERNOTREGDD);
                }
                else
                {
                    pnDD.Visible = true;
                    pnTH.Visible = true;

                    ddlAccount.DataSource = ds.Tables[0];
                    ddlAccount.DataTextField = "ACCTNO";
                    ddlAccount.DataValueField = "ACCTNO";
                    ddlAccount.DataBind();
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ACCTNO"] != null)
                    {
                        ddlAccount.SelectedValue = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ACCTNO"].ToString();
                    }
                }

                LoadDetailAccount();
            }
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
           
            //ltrTH.Text = sT.ToString();
            LoadHistory();
        }
        catch
        {
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            //
            ltrTH.Text = "";

            LoadDetailAccount();
        }
        catch
        {
        }
    }

    #region Load Detail Account , Load History
    private void LoadHistory()
    {
        try
        {
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            double openingBalance = 0;
            double endingBalance = 0;
            double totalDebit = 0;
            double totalCredit = 0;

            DataSet ds = new DataSet();
            Account acct = new Account();
            DataSet dsMap = new DataSet();
            Transactions Trans = new Transactions();
            //ds = acct.getTransactionHis(Session["userID"].ToString(), "", ddlAccount.SelectedItem.Value.ToString(), txtFromDate.Text, txtToDate.Text, ref ErrorCode, ref ErrorDesc);
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                //kiem tra ngay thang hop le va ngày bat dau cach ngay hien tai khong qua 3 thang
                if (SmartPortal.Common.Utilities.Utility.IsDateTime1(txtFromDate.Text.Trim()) > SmartPortal.Common.Utilities.Utility.IsDateTime1(txtToDate.Text.Trim()))
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.TODATEGREATERTHANFROMDATE);
                    //lblError.Text = Resources.labels.ngayketthucphailonhonngaybatdau;
                    return;

                }
                if (DateTime.Now > SmartPortal.Common.Utilities.Utility.IsDateTime1(txtFromDate.Text.Trim()).AddMonths(3))
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.THREEMONTH);
                }

                //ds = acct.getTransactionHis(Session["userID"].ToString(), "", ddlAccount.SelectedItem.Value.ToString(), txtFromDate.Text, txtToDate.Text, ref ErrorCode, ref ErrorDesc);
            }
            else
            {
                //ds = acct.getTransactionHis(Session["userID"].ToString(), "", ddlAccount.SelectedItem.Value.ToString(), DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"), ref ErrorCode, ref ErrorDesc);
            }

            if (ErrorCode != "0")
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
            }
            else
            {
                if (ds.Tables.Count == 0)
                {
                    ltrTH.Text = "<center><span style='font-weight:bold;color:red;'>"+Resources.labels.khongtimthaygiaodichtrongkhoangthoigiannay+".</span></center>";
                    return;
                }
            }

            
            //xuat len man hinh
            endingBalance = double.Parse(ds.Tables[0].Rows[0].Field<string>("endbalance"));
            openingBalance = double.Parse(ds.Tables[0].Rows[0].Field<string>("opnbalance"));
            StringBuilder sT = new StringBuilder();

            sT.Append("<table class='table table-bordered table-hover footable' data-paging='true'");
            sT.Append("<thead>");
            sT.Append("<tr class='thtr'>");
            sT.Append("<td class='thtdff' align='center' width='100px'>");
            sT.Append(Resources.labels.openingbalance);
            sT.Append("</td>");
            sT.Append("<td class='thtd' align='center' width='100px'>");
            sT.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(openingBalance.ToString(), "LAK"));
            sT.Append("</td>");

            sT.Append("<td class='thtd' align='center' width='100px'>");
            sT.Append(Resources.labels.endingbalance);
            sT.Append("</td>");
            sT.Append("<td class='thtd' align='center' width='100px'>");
            sT.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(endingBalance.ToString(), "LAK"));
            sT.Append("</td>");
            sT.Append("<td class='thtd' colspan='2'></td>");
            sT.Append("</tr>");
            sT.Append("</thead>");
            sT.Append("<tr class='thtr'>");

            sT.Append("<td class='thtdff' align='center' width='100px'>");
            sT.Append(Resources.labels.sogiaodich);
            sT.Append("</td>");

            sT.Append("<td class='thtd' align='center' width='100px'>");
            sT.Append(Resources.labels.ngaygiogiaodich);
            sT.Append("</td>");

            sT.Append("<td class='thtd' align='center' width='100px'>");
            sT.Append(Resources.labels.ghino);
            sT.Append("</td>");

            sT.Append("<td class='thtd' align='center' width='100px'>");
            sT.Append(Resources.labels.ghico);
            sT.Append("</td>");

            sT.Append("<td class='thtd' align='center' width='120px'>");
            sT.Append(Resources.labels.sodusaugiaodich);
            sT.Append("</td>");

            sT.Append("<td class='thtd' align=center width='380px'>");
            sT.Append(Resources.labels.diengiai);
            sT.Append("</td>");

            /*sT.Append("<td class='thtd' width='100px' style='text-align:center'>");
            sT.Append(Resources.labels.loaigiaodich);
            sT.Append("</td>");*/
            sT.Append("</tr>");

            DataRow []arrRow= ds.Tables[0].Select("TRANREF>'0'","TRANREF DESC");
            
            foreach (DataRow row in arrRow)
            {
                sT.Append("<tr>");

                /*sT.Append("<td class='thtdff' style='text-align:right'>");
                sT.Append(stt++);
                sT.Append("</td>");*/

                sT.Append("<td align='center' class='thtdff'>");
                sT.Append(SmartPortal.Common.Utilities.Utility.FormatStringCore(row["TRANREF"].ToString()));
                sT.Append("</td>");

                sT.Append("<td class='thtd'>");
                sT.Append(SmartPortal.Common.Utilities.Utility.FormatDatetime(row["TRANDATE"].ToString(), "dd/MM/yyyy"));// + " " + SmartPortal.Common.Utilities.Utility.FormatDatetime(row["TRANTIME"].ToString(), "HH:mm:ss"));
                sT.Append("</td>");

                if (row.Field<string>("CREDIT").Equals(string.Empty))
                {
                    totalDebit += double.Parse(row.Field<string>("debit"));
                    sT.Append("<td class='thtd' style='text-align:right'>");
                    sT.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(row["DEBIT"].ToString()), row["CCYID"].ToString().Trim()) + " " + row["CCYID"].ToString() + "&nbsp;");
                    sT.Append("</td>");

                    sT.Append("<td class='thtd'>");
                    sT.Append("&nbsp;");
                    sT.Append("</td>");
                }
                else
                {
                    totalCredit += double.Parse(row.Field<string>("credit"));
                    sT.Append("<td class='thtd'>");
                    sT.Append("&nbsp;");
                    sT.Append("</td>");

                    sT.Append("<td class='thtd'  style='text-align:right'>");
                    sT.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(row["CREDIT"].ToString()), row["CCYID"].ToString().Trim()) + " " + row["CCYID"].ToString() + "&nbsp;");
                    sT.Append("</td>");
                }

                sT.Append("<td class='thtd' style='text-align:right'>");
                sT.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(row["BALANCE"].ToString()), row["CCYID"].ToString().Trim()) + " " + row["CCYID"].ToString());
                sT.Append("</td>");

                sT.Append("<td class='thtd'>");
                sT.Append(Utility.FormatStringCore(row["TRANDESC"].ToString()));
                sT.Append("</td>");
                
                sT.Append("</td>");
                sT.Append("</tr>");
            }

            sT.Append("<tr class='thtr'>");
            sT.Append("<td class='thtdff' align='center'>");
            sT.Append(Resources.labels.tongcong);
            sT.Append("</td>");
            sT.Append("<td class='thtd'></td>");
            sT.Append("<td class='thtd' align='center'>");
            sT.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(totalDebit.ToString(), "LAK"));
            sT.Append("</td>");

            sT.Append("<td class='thtd' align='center'>");
            sT.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(totalCredit.ToString(), "LAK"));
            sT.Append("</td>");
            sT.Append("<td class='thtd' colspan='2'></td>");
            sT.Append("</tr>");

            sT.Append("</table>");

            ltrTH.Text = sT.ToString();
            Session["tmpl"] = sT.ToString();

            //xuat ra excel
            if (ds.Tables[0].Rows.Count != 0)
            {
                btnExport.Visible = true;
                btnPrint.Visible = true;
            }
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

    private void LoadDetailAccount()
    {
        lblAccountNumber.Text = ddlAccount.SelectedItem.Value.ToString();
        lblODPDL_DD.Text = string.Empty;
        lblCB_DD.Text = string.Empty;
        lblAccountName_DD.Text = string.Empty;
        lblCurrency_DD.Text = string.Empty;
        lblLTD_DD.Text = string.Empty;
        lblAB_DD.Text = string.Empty;
        lblIR_DD.Text = string.Empty;
        lblACR_DD.Text = string.Empty;
        lblHA_DO.Text = string.Empty;
        lblDO_DD.Text = string.Empty;
        //
        string ErrorCode = string.Empty;
        string ErrorDesc = string.Empty;
        DataSet ds = new DataSet();
        Account acct = new Account();
        ds = acct.GetInfoDD(Session["userID"].ToString(), ddlAccount.SelectedItem.Value, ref ErrorCode, ref ErrorDesc);
        if (ds.Tables[0].Rows.Count == 1)
        {
            lblAccountName_DD.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
            lblCurrency_DD.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
            lblDO_DD.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.OPENDATE].ToString(), "dd/MM/yyyy",DateTimeStyle.DateMMM );
            lblLTD_DD.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.LASTTRANSDATE].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
            lblCB_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.BALANCE].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
            lblAB_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
            //lblIR_DD.Text = (float.Parse(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.INTERESTRATE].ToString().Replace('.',','))*100).ToString().Replace(',','.');
            lblHA_DO.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.BLOCKBALANCE].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
            //lblACR_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.ACCRUEDCRINTEREST].ToString()));
            //lblODPI_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.ACCRUEDODINTEREST].ToString()));
            lblODPDL_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.ODLIMIT].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());

            NumberStyles style;
            CultureInfo culture;
            double laisuat,laicongdon, laithauchi;

            style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
            culture = CultureInfo.CreateSpecificCulture("en-US");

            double.TryParse(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.INTERESTRATE].ToString(),style,culture,out laisuat);
            double.TryParse(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.ACCRUEDODINTEREST].ToString(), style, culture, out laithauchi);
            double.TryParse(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.ACCRUEDCRINTEREST].ToString(), style, culture, out laicongdon);
            lblODPI_DD.Text = (laithauchi).ToString(culture.NumberFormat);
            lblIR_DD.Text = (laisuat ).ToString(culture.NumberFormat);
            lblACR_DD.Text = laicongdon.ToString(culture.NumberFormat);

            string[] strBranch = ds.Tables[0].Rows[0]["branch"].ToString().Split('.');
            DataSet dsBranch = acct.GetBranch(strBranch[0].ToString());
            if (dsBranch.Tables[0].Rows.Count == 1)
            {
                lblBranch_DD.Text = dsBranch.Tables[0].Rows[0][SmartPortal.Constant.IPC.BRANCHNAME].ToString();
            }
        }
    }
    #endregion
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string str = "<div style='font-weight:bold; font-size:12pt;'>ABC<br/>"+Resources.labels.chitietgiaodich.ToUpper()+"<br/><br/></div>";
            str += Resources.labels.sotaikhoan + ": " + lblAccountNumber.Text + "<br/><br/>";
            str += ltrTH.Text;
            str += "<div><br/><p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'>www.scb.co.th/en/personal-banking</a></p><span style='font-weight:bold;'>" + Resources.labels.camonquykhachdasudungdichvucuabank+"</span></div>";

            SmartPortal.Common.ExportToFile.ExportStringToExcel("Statement", str);
        }
        catch
        {
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

}
