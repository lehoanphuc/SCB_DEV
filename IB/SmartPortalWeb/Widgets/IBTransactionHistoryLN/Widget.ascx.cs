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
using SmartPortal.Common.Utilities;

public partial class Widgets_IBTransactionHistoryLN_Widget : WidgetBase
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //Bang chua thong tin tai khoan DD
    //        DataTable ddTable = new DataTable();
    //        DataColumn tranCodeCol = new DataColumn("TranCode");
    //        DataColumn tranTypeCol = new DataColumn("TranType");
    //        DataColumn dateCol = new DataColumn("Date");
    //        DataColumn inCol = new DataColumn("In");
    //        DataColumn outCol = new DataColumn("Out");
    //        DataColumn descCol1 = new DataColumn("Desc");
    //        ddTable.Columns.AddRange(new DataColumn[] { tranCodeCol,tranTypeCol, dateCol, inCol, outCol, descCol1 });

    //        DataRow r = ddTable.NewRow();
    //        r["TranCode"] = "A3231";
    //        r["TranType"] = "Rút tiền";
    //        r["Date"] = "12/02/2010";
    //        r["In"] = "3.000.000 LAK";
    //        r["Out"] = "";
    //        r["Desc"] = "Rút tiền ATM";

    //        ddTable.Rows.Add(r);

    //        //xuat len man hinh
    //        StringBuilder sT = new StringBuilder();
    //        sT.Append("<table class='style1' cellpadding='5' cellspacing='0'>");
    //        sT.Append("<tr class='thtr'>");
    //        sT.Append("<td class='thtdff'>");
    //        sT.Append("Mã giao dịch");
    //        sT.Append("</td>");
    //        sT.Append("<td class='thtd'>");
    //        sT.Append("Loại giao dịch");
    //        sT.Append("</td>");
    //        sT.Append("<td class='thtd'>");
    //        sT.Append("Ngày");
    //        sT.Append("</td>");
    //        sT.Append("<td class='thtd'>");
    //        sT.Append("Ghi có");
    //        sT.Append("</td>");
    //        sT.Append("<td class='thtd'>");
    //        sT.Append("Ghi nợ");
    //        sT.Append("</td>");
    //        sT.Append("<td class='thtd'>");
    //        sT.Append("Mô tả");
    //        sT.Append("</td>");
    //        sT.Append("</tr>");
    //        foreach (DataRow row in ddTable.Rows)
    //        {
    //            sT.Append("<tr>");
    //            sT.Append("<td class='thtdff'>");
    //            sT.Append(row["TranCode"].ToString());
    //            sT.Append("</td>");
    //            sT.Append("<td class='thtd'>");
    //            sT.Append(row["TranType"].ToString());
    //            sT.Append("</td>");
    //            sT.Append("<td class='thtd'>");
    //            sT.Append(row["Date"].ToString());
    //            sT.Append("</td>");
    //            sT.Append("<td class='thtd'>");
    //            sT.Append(row["In"].ToString());
    //            sT.Append("</td>");
    //            sT.Append("<td class='thtd'>");
    //            sT.Append(row["Out"].ToString());
    //            sT.Append("</td>");
    //            sT.Append("<td class='thtd'>");
    //            sT.Append(row["Desc"].ToString());
    //            sT.Append("</td>");
    //            sT.Append("</tr>");
    //        }
    //        sT.Append("</table>");

    //        ltrTH.Text = sT.ToString();
    //    }
    //    catch
    //    {
    //    }
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            LoadAccountInfo();

            ltrTH.Text = "";
        }
        catch
        {
        }
    }
    #region 
    private void LoadAccount()
    {
        try
        {
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            DataSet ds = new DataSet();
            Account acct = new Account();
            ds = acct.getAccount(Session["userID"].ToString(), "IB000200", "LN", ref ErrorCode, ref ErrorDesc);

            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.USERNOTREGLN);
            }
            else
            {
                pnLN.Visible = true;

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
            ds = objAcct.GetLNAcctInfo(Session["userID"].ToString(), acct, ref ErrorCode, ref ErrorDesc);
            ShowLN(acct, ds);
        }
        catch
        {
        }
    }
    private void ShowLN(string acctno, DataSet ds)
    {
        try
        {
            lblAccountName_LN.Text = string.Empty;
            lblDO_LN.Text = string.Empty;
            lblExpDate.Text = string.Empty;
            lblCCYID.Text = string.Empty;
            lblAccountNumber_LN.Text = acctno;
            if (ds.Tables[0].Rows.Count == 1)
            {
                lblAccountName_LN.Text = ds.Tables[0].Rows[0]["CUSTOMERNAME"].ToString();
                lblDO_LN.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0]["OPENDATE"].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
                lblExpDate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0]["EXPDATE"].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
                lblCCYID.Text = ds.Tables[0].Rows[0]["CURRENCYID"].ToString();
                lblBalance.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0]["BALANCE"].ToString()), ds.Tables[0].Rows[0]["CURRENCYID"].ToString().Trim());
                //----Thêm chi nhánh---
                Account acct = new Account();
                string[] strBranch = ds.Tables[0].Rows[0]["branch"].ToString().Split('.');
                DataSet dsBranch = acct.GetBranch(strBranch[0].ToString());
                if (dsBranch.Tables[0].Rows.Count == 1)
                {
                    lblBranch_DD.Text = dsBranch.Tables[0].Rows[0][SmartPortal.Constant.IPC.BRANCHNAME].ToString();
                }
               //----Thêm chi nhánh---
            }
        }
        catch
        {
        }
    }
    #endregion
    protected void Button3_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/?po=3&p=112&ACCTNO=" + ddlAccount.SelectedItem.Value.ToString());
        LoadLNSchedule();
    }
    private void LoadLNSchedule()
    {
        try
        {
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            DataSet ds = new DataSet();
            //string acct = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ACCTNO"].ToString();
            Account objAcct = new Account();
            //edit from date, to date != empty
            string str_fdate = string.Empty;
            string str_tdate = string.Empty;
            if (txtToDate.Text != "")
            {
                str_tdate = txtToDate.Text;
            }
            else
            {
                str_tdate = DateTime.Now.ToString("dd/MM/yyyy");
            }
            if (txtFromDate.Text != "")
            {
                str_fdate = txtFromDate.Text;
            }
            else
            {
                str_fdate = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            }
            
            if(DateTime.ParseExact(str_fdate,"dd/MM/yyyy",null) >= DateTime.ParseExact(str_tdate,"dd/MM/yyyy",null) && txtFromDate.Text == "")
            {
                str_fdate = DateTime.ParseExact(str_tdate, "dd/MM/yyyy", null).AddMonths(-1).ToString("dd/MM/yyyy");
            }
            
            ds = objAcct.GetLNSchedulePayment(Session["userID"].ToString(), lblAccountNumber_LN.Text, str_fdate, str_tdate
                    , ref ErrorCode, ref ErrorDesc);
            //if (txtFromDate.Text != "" && txtToDate.Text != "")
            //{
            //    ds = objAcct.GetLNSchedulePayment(Session["userID"].ToString(), lblAccountNumber_LN.Text, txtFromDate.Text, txtToDate.Text
            //        , ref ErrorCode, ref ErrorDesc);
            //}
            //else
            //{
            //    ds = objAcct.GetLNSchedulePayment(Session["userID"].ToString(), lblAccountNumber_LN.Text, DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy")
            //       , ref ErrorCode, ref ErrorDesc);
            //}

            if (ErrorCode != "0")
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
            }
            else
            {
                if (ds.Tables.Count == 0)
                {
                    ltrTH.Text = "<center><span style='font-weight:bold;color:red;'>" + Resources.labels.khongtimthaygiaodichtrongkhoangthoigiannay + ".</span></center>";
                    return;
                }
            }

            //xuat len man hinh
            StringBuilder sT1 = new StringBuilder();
            sT1.Append("<table class='style1' cellpadding='5' cellspacing='0'style='border-left:solid 1px #b9bfc1;border-right:solid 1px #b9bfc1;border-bottom:solid 1px #b9bfc1;'>");
            sT1.Append("<tr class='thtr'>");
            sT1.Append("<td class='thtdff'>");
            sT1.Append(Resources.labels.stt);
            sT1.Append("</td>");

            sT1.Append("<td class='thtd'>");
            sT1.Append(Resources.labels.ngaygiogiaodich);
            sT1.Append("</td>");            
            sT1.Append("<td class='thtd'>");
            sT1.Append(Resources.labels.sotien);
            sT1.Append("</td>");
            sT1.Append("<td class='thtd'>");
            sT1.Append(Resources.labels.mota);
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
                sT1.Append(SmartPortal.Common.Utilities.Utility.FormatDatetime(row["NGAY"].ToString(), "dd/MM/yyyy"));
                sT1.Append("</td>");                
                sT1.Append("<td class='thtd'>");
                sT1.Append(SmartPortal.Common.Utilities.Utility.FormatMoney(row["SOTIEN"].ToString(), lblCCYID.Text.Trim()) + " " + lblCCYID.Text);
                sT1.Append("</td>");


                sT1.Append("<td class='thtd'>");
                //Map trantype 

                DataSet dsMap = new SmartPortal.IB.Transactions().LoadMapTrantype(row["DIENGIAI"].ToString(), System.Globalization.CultureInfo.CurrentCulture.ToString(), ref ErrorCode, ref ErrorDesc);
                if (ErrorCode.Equals("0"))
                {
                    if (dsMap.Tables.Count != 0)
                    {
                        if (dsMap.Tables[0].Rows.Count != 0)
                        {

                            sT1.Append(dsMap.Tables[0].Rows[0]["TRANTYPENAME"].ToString());
                        }
                        else
                        {
                            //throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.UNABLEMAPTRANSACTIONTYPE);
                            sT1.Append(row["DIENGIAI"].ToString());
                        }

                    }
                    else
                    {
                        //throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.UNABLEMAPTRANSACTIONTYPE);
                        sT1.Append(row["DIENGIAI"].ToString());
                    }
                }
                else
                {
                    // throw new SmartPortal.ExceptionCollection.IPCException(ErrorDesc);
                    sT1.Append(row["TRANNAME"].ToString());
                }
                //
                sT1.Append("</td>");

               
                sT1.Append("</tr>");
                i++;
            }
            sT1.Append("</table>");
            ltrTH.Text = sT1.ToString();
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

}
