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

public partial class Widgets_IBTransactionHistoryTKKKH_Widget : WidgetBase
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string ErrorCode = string.Empty;
                string ErrorDesc = string.Empty;
                DataSet ds = new DataSet();
                Account acct = new Account();
                
                ds = acct.getAccount(Session["userID"].ToString(), "IB000200", "DD", ref ErrorCode, ref ErrorDesc);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.USERNOTREGSAV);
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
            //Bang chua thong tin tai khoan DD
            //DataTable ddTable = new DataTable();
            //DataColumn tranCodeCol = new DataColumn("TranCode");
            //DataColumn tranTypeCol = new DataColumn("TranType");
            //DataColumn dateCol = new DataColumn("Date");
            //DataColumn inCol = new DataColumn("In");
            //DataColumn outCol = new DataColumn("Out");
            //DataColumn descCol1 = new DataColumn("Desc");
            //ddTable.Columns.AddRange(new DataColumn[] { tranCodeCol,tranTypeCol, dateCol, inCol, outCol, descCol1 });

            //DataRow r = ddTable.NewRow();
            //r["TranCode"] = "A3231";
            //r["TranType"] = "Rút tiền";
            //r["Date"] = "12/02/2010";
            //r["In"] = "3.000.000 LAK";
            //r["Out"] = "";
            //r["Desc"] = "Rút tiền ATM";

            //ddTable.Rows.Add(r);
            //string ErrorCode = string.Empty;
            //string ErrorDesc = string.Empty;
            //DataSet ds = new DataSet();
            //Account acct = new Account();
            //ds = acct.getTransactionHis(Session["userID"].ToString(), "", ddlAccount.SelectedItem.Value.ToString(), txtFromDate.Text, txtToDate.Text, ref ErrorCode, ref ErrorDesc);

            //xuat len man hinh
            //StringBuilder sT = new StringBuilder();
            //sT.Append("<table class='style1' cellpadding='5' cellspacing='0'>");
            //sT.Append("<tr class='thtr'>");
            //sT.Append("<td class='thtdff'>");
            //sT.Append("Mã giao dịch");
            //sT.Append("</td>");
            //sT.Append("<td class='thtd'>");
            //sT.Append("Loại giao dịch");
            //sT.Append("</td>");
            //sT.Append("<td class='thtd'>");
            //sT.Append("Ngày");
            //sT.Append("</td>");
            //sT.Append("<td class='thtd'>");
            //sT.Append("Ghi có");
            //sT.Append("</td>");
            //sT.Append("<td class='thtd'>");
            //sT.Append("Ghi nợ");
            //sT.Append("</td>");
            //sT.Append("<td class='thtd'>");
            //sT.Append("Mô tả");
            //sT.Append("</td>");
            //sT.Append("</tr>");
            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            //    sT.Append("<tr>");
            //    sT.Append("<td class='thtdff'>");
            //    sT.Append(row["TRANCODE"].ToString());
            //    sT.Append("</td>");
            //    //sT.Append("<td class='thtd'>");
            //    //sT.Append(row["TranType"].ToString());
            //    //sT.Append("</td>");
            //    sT.Append("<td class='thtd'>");
            //    sT.Append(row["TRANAME"].ToString());
            //    sT.Append("</td>");
            //    sT.Append("<td class='thtd'>");
            //    sT.Append(row["TRANDATE"].ToString());
            //    sT.Append("</td>");
            //    //sT.Append("<td class='thtd'>");
            //    //sT.Append(row["In"].ToString());
            //    //sT.Append("</td>");
            //    //sT.Append("<td class='thtd'>");
            //    //sT.Append(row["Out"].ToString());
            //    //sT.Append("</td>");
            //    sT.Append("<td class='thtd'>");
            //    sT.Append(row["AMOUNT"].ToString());
            //    sT.Append("</td>");
            //    sT.Append("<td class='thtd'>");
            //    sT.Append(row["TRANDESC"].ToString());
            //    sT.Append("</td>");
            //    sT.Append("</tr>");
            //}
            //sT.Append("</table>");

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
            StringBuilder sT = new StringBuilder();
            sT.Append("<div class='table-responsive'>");
            sT.Append("<table class='table footable table-bordered table-hover' data-paging='true'>");
            sT.Append("<thead>");
            sT.Append("<tr class='thtr'>");

            sT.Append("<td class='thtdff' data-breakpoints='xs sm' width='120px'>");
            sT.Append(Resources.labels.ngaygiogiaodich);
            sT.Append("</td>");

            sT.Append("<td class='thtd' width='100px'>");
            sT.Append(Resources.labels.sogiaodich);
            sT.Append("</td>");

            sT.Append("<td class='thtd' data-breakpoints='xs sm' width='100px'>");
            sT.Append(Resources.labels.ghino);
            sT.Append("</td>");

            sT.Append("<td class='thtd' data-breakpoints='xs sm' width='100px'>");
            sT.Append(Resources.labels.ghico);
            sT.Append("</td>");

            sT.Append("<td class='thtd' width='100px'>");
            sT.Append(Resources.labels.sodusaugiaodich);
            sT.Append("</td>");

            sT.Append("<td class='thtd' data-breakpoints='xs sm' width='380px'>");
            sT.Append(Resources.labels.mota);
            sT.Append("</td>");


            sT.Append("<td class='thtd' width='100px'>");
            sT.Append(Resources.labels.loaigiaodich);
            sT.Append("</td>");
            sT.Append("</tr>");

            sT.Append("</thead>");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                sT.Append("<tr>");

                sT.Append("<td class='thtdff'>");
                sT.Append(SmartPortal.Common.Utilities.Utility.FormatDatetime(row["TRANDATE"].ToString(), "dd/MM/yyyy") + " " + SmartPortal.Common.Utilities.Utility.FormatDatetime(row["TRANTIME"].ToString(), "HH:mm:ss"));
                sT.Append("</td>");

                sT.Append("<td class='thtd'>");
                sT.Append(SmartPortal.Common.Utilities.Utility.FormatStringCore(row["TRANREF"].ToString()));
                sT.Append("</td>");

                if (row["AMOUNT"].ToString().Trim().Substring(0, 1) == "-")
                {
                    sT.Append("<td class='thtd'>");
                    sT.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(row["AMOUNT"].ToString()).Trim().Substring(1), row["CCYID"].ToString().Trim()) + " " + row["CCYID"].ToString() + "&nbsp;");
                    sT.Append("</td>");

                    sT.Append("<td class='thtd'>");
                    sT.Append("&nbsp;");
                    sT.Append("</td>");
                }
                else
                {
                    sT.Append("<td class='thtd'>");
                    sT.Append("&nbsp;");
                    sT.Append("</td>");

                    sT.Append("<td class='thtd'>");
                    sT.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(row["AMOUNT"].ToString()), row["CCYID"].ToString().Trim()) + " " + row["CCYID"].ToString() + "&nbsp;");
                    sT.Append("</td>");

                }

                sT.Append("<td class='thtd'>");
                sT.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(row["BALANCE"].ToString()), row["CCYID"].ToString().Trim()) + " " + row["CCYID"].ToString());
                sT.Append("</td>");

                sT.Append("<td class='thtd'>");
                sT.Append(row["TRANDESC"].ToString() + "&nbsp;");
                sT.Append("</td>");

                
                sT.Append("<td class='thtd'>");
                //Map trantype  Q    u     y      e      n

                dsMap = Trans.LoadMapTrantype(row["TRANNAME"].ToString(), System.Globalization.CultureInfo.CurrentCulture.ToString(), ref ErrorCode, ref ErrorDesc);
                if (ErrorCode.Equals("0"))
                {
                    if (dsMap.Tables.Count != 0)
                    {
                        if (dsMap.Tables[0].Rows.Count != 0)
                        {

                            sT.Append(dsMap.Tables[0].Rows[0]["TRANTYPENAME"].ToString());
                        }
                        else
                        {
                            //throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.UNABLEMAPTRANSACTIONTYPE);
                            sT.Append(row["TRANNAME"].ToString());
                        }

                    }
                    else
                    {
                        //throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.UNABLEMAPTRANSACTIONTYPE);
                        sT.Append(row["TRANNAME"].ToString());
                    }
                }
                else
                {
                   // throw new SmartPortal.ExceptionCollection.IPCException(ErrorDesc);
                    sT.Append(row["TRANNAME"].ToString());
                }
                //
                sT.Append("</td>");

                sT.Append("</tr>");
            }
            sT.Append("</table>");
            sT.Append("</div>");

            ltrTH.Text = sT.ToString();
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
            lblDO_DD.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.OPENDATE].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
            lblLTD_DD.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.LASTTRANSDATE].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
            lblCB_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.BALANCE].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
            lblAB_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
            lblHA_DO.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.BLOCKBALANCE].ToString(), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
            lblODPDL_DD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.ODLIMIT].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());

            NumberStyles style;
            CultureInfo culture;
            double laisuat, laicongdon, laithauchi;

            style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
            culture = CultureInfo.CreateSpecificCulture("en-US");

            double.TryParse(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.INTERESTRATE].ToString(), style, culture, out laisuat);
            double.TryParse(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.ACCRUEDODINTEREST].ToString(), style, culture, out laithauchi);
            double.TryParse(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.ACCRUEDCRINTEREST].ToString(), style, culture, out laicongdon);
            lblODPI_DD.Text = (laithauchi ).ToString(culture.NumberFormat);
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
}
