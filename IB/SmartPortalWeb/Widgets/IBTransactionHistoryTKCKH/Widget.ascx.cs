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

public partial class Widgets_IBTransactionHistoryTKCKH_Widget : WidgetBase
{


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pnTH.Visible = false;

            if (!IsPostBack)
            {
                LoadAccount();
                LoadAccountInfo();
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
            ddTable.Columns.AddRange(new DataColumn[] { tranCodeCol, tranTypeCol, dateCol, inCol, outCol, descCol1 });

            DataRow r = ddTable.NewRow();
            r["TranCode"] = "A3231";
            r["TranType"] = "Rút tiền";
            r["Date"] = "12/02/2010";
            r["In"] = "3.000.000";
            r["Out"] = "";
            r["Desc"] = "Nạp tiền tiết kiệm";

            ddTable.Rows.Add(r);

            //xuat len man hinh
            StringBuilder sT = new StringBuilder();
            sT.Append("<div class='table-responsive'>");
            sT.Append("<table class='table footable table-bordered table-hover' data-paging='true'>");
            sT.Append("<thead>");
            sT.Append("<tr class='thtr'>");
            sT.Append("<td class='thtdff'>");
            sT.Append(Resources.labels.sogiaodich);
            sT.Append("</td>");
            sT.Append("<td class='thtd'>");
            sT.Append(Resources.labels.loaigiaodich);
            sT.Append("</td>");
            sT.Append("<td class='thtd'  data-breakpoints='xs sm' >");
            sT.Append(Resources.labels.ngaygiogiaodich);
            sT.Append("</td>");
            sT.Append("<td class='thtd'>  data-breakpoints='xs' ");
            sT.Append(Resources.labels.sotien);
            sT.Append("</td>");

            sT.Append("<td class='thtd'  data-breakpoints='xs sm' >");
            sT.Append(Resources.labels.mota);
            sT.Append("</td>");
            sT.Append("</tr>");
            sT.Append("</thead>");
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
                sT.Append(row["Desc"].ToString() + "&nbsp;");
                sT.Append("</td>");
                sT.Append("</tr>");
            }
            sT.Append("</table>");
            sT.Append("</div>");

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
            LoadAccountInfo();
        }
        catch
        {
        }
    }

    #region show
    private void LoadAccount()
    {
        try
        {
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            DataSet ds = new DataSet();
            Account acct = new Account();
            ds = acct.getAccount(Session["userID"].ToString(), "IB000200", "FD", ref ErrorCode, ref ErrorDesc);
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.USERNOTREGFD);
            }
            else
            {
                pnFD.Visible = true;

                ddlAccount.DataSource = ds.Tables[0];
                ddlAccount.DataTextField = "ACCTNO";
                ddlAccount.DataValueField = "ACCTNO";
                ddlAccount.DataBind();
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ACCTNO"] != null)
                {
                    ddlAccount.SelectedValue = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ACCTNO"].ToString();
                }
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
    private void LoadAccountInfo()
    {
        try
        {
            string acct = ddlAccount.SelectedItem.Value;
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            Account objAcct = new Account();
            DataSet ds = new DataSet();
            ds = objAcct.GetFDAcctInfo(Session["userID"].ToString(), acct, ref ErrorCode, ref ErrorDesc);
            ShowFD(acct, ds);
        }
        catch
        {
        }
    }
    private void ShowFD(string acctno, DataSet ds)
    {
        try
        {
            lblAccountNumber_FD.Text = acctno;
            Account acct = new Account();
            if (ds.Tables[0].Rows.Count == 1)
            {
                lblAccountName_FD.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                lblCurrency_FD.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                lblDO_FD.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.OPENDATE].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
                lblLT_FD.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.EXPIREDATE].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
                lblCB_FD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
                //lblAB_FD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()));

                //SmartPortal.Constant.IPC.AVAILABLEBALANCE
                lblACRI_FD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.ACCRUEDCRINTEREST].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
                //lblIR_FD.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.INTERESTRATE].ToString();

                NumberStyles style;
                CultureInfo culture;
                double laisuat;

                style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                culture = CultureInfo.CreateSpecificCulture("en-US");

                double.TryParse(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.INTERESTRATE].ToString(), style, culture, out laisuat);
                lblIR_FD.Text = (laisuat).ToString(culture.NumberFormat);

                string[] strBranch = ds.Tables[0].Rows[0]["branch"].ToString().Split('.');
                DataSet dsBranch = acct.GetBranch(strBranch[0].ToString());
                if (dsBranch.Tables[0].Rows.Count == 1)
                {
                    lblBranch_DD.Text = dsBranch.Tables[0].Rows[0][SmartPortal.Constant.IPC.BRANCHNAME].ToString();
                }
            }
        }
        catch
        {
        }
    }
    #endregion
}
