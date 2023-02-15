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

public partial class Widgets_IBAccountListLN_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataTable LnTable = new DataTable();
            DataColumn accountCol = new DataColumn("Account");
            DataColumn detailCol = new DataColumn("Detail");
            DataColumn descCol = new DataColumn("Desc");
            DataColumn dateCol = new DataColumn("Date");
            DataColumn totalCol = new DataColumn("Total");
            DataColumn dcStatus = new DataColumn("Status");
            DataColumn StatusImg = new DataColumn("StatusImg");
            DataColumn acttypeCol = new DataColumn("Type");
            LnTable.Columns.AddRange(new DataColumn[] { accountCol, detailCol, descCol, dateCol, totalCol, dcStatus, StatusImg, acttypeCol });
            string errorCode = "0";
            string errorDesc = string.Empty;
            DataSet ds = new DataSet();
            string Status = string.Empty;
            SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
            ds = acct.getAccount(Session["userID"].ToString(), "IB000200", "LN", ref errorCode, ref errorDesc);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow r = LnTable.NewRow();
                r["Account"] = ds.Tables[0].Rows[i]["ACCTNO"].ToString();
                //r["Desc"] = "Tài khoản vay";

                string ErrorCode = string.Empty;
                string ErrorDesc = string.Empty;
                Account objAcct = new Account();
                DataSet ds1 = new DataSet();
                ds1 = objAcct.GetLNAcctInfo(Session["userID"].ToString(), ds.Tables[0].Rows[i]["ACCTNO"].ToString(), ref ErrorCode, ref ErrorDesc);
                if (ds1.Tables.Count != 0)
                {
                    Status = string.Empty;
                    try
                    {
                        Status = ds1.Tables[0].Rows[0]["Statuscd"].ToString() as string;
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
                    }
                    switch (Status)
                    {
                        case "W":
                            r["Status"] = Resources.labels.moi + "&nbsp;&nbsp;";
                            r["StatusImg"] = "act_sts_new.png";
                            break;
                        case "A":
                            r["Status"] = Resources.labels.binhthuong;
                            r["StatusImg"] = "act_sts_normal.png";
                            break;
                        case "B":
                            r["Status"] = Resources.labels.conblock;
                            r["StatusImg"] = "act_sts_active.png";
                            break;
                        case "O":
                            r["Status"] = Resources.labels.stoppayment;
                            r["StatusImg"] = "act_sts_stoppayment.png";
                            break;
                        case "S":
                        case "CLS":
                            r["Status"] = Resources.labels.close;
                            r["StatusImg"] = "act_sts_close.png";
                            break;
                        case "V":
                            r["Status"] = Resources.labels.active;
                            r["StatusImg"] = "act_sts_active.png";
                            break;
                        case "M":
                            r["Status"] = Resources.labels.maturity;
                            r["StatusImg"] = "act_sts_maturity.png";
                            break;


                    }
                    if (String.IsNullOrEmpty(Status) == false && Status.ToUpper() != "CLS" && Status.ToUpper() != "S")
                    {
                        r["Account"] = ds.Tables[0].Rows[i]["ACCTNO"].ToString();
                        r["Detail"] = "?po=3&p=106&ACCTNO=" + ds.Tables[0].Rows[i]["ACCTNO"].ToString();


                        r["Desc"] = Resources.labels.taikhoanloan;//ds1.Tables[0].Rows[0]["ACCTNAME"].ToString(); ;
                        r["Date"] = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds1.Tables[0].Rows[0][SmartPortal.Constant.IPC.OPENDATE].ToString(), "dd/MM/yyyy", SmartPortal.Common.Utilities.DateTimeStyle.DateMMM);
                        //r["Total"] = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds1.Tables[0].Rows[0][SmartPortal.Constant.IPC.BALANCE].ToString()), ds1.Tables[0].Rows[0]["CURRENCYID"].ToString().Trim()) + " " + ds1.Tables[0].Rows[0]["CURRENCYID"].ToString();
                        if (ds1.Tables[0].Rows[0]["CURRENCYID"].ToString().Trim() == SmartPortal.Constant.IPC.LAK)
                        {
                            r["Total"] = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds1.Tables[0].Rows[0][SmartPortal.Constant.IPC.BALANCE].ToString()), ds1.Tables[0].Rows[0]["CURRENCYID"].ToString().Trim()) + " " + ds1.Tables[0].Rows[0]["CURRENCYID"].ToString();

                        }
                        else
                        {
                            r["Total"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(ds1.Tables[0].Rows[0][SmartPortal.Constant.IPC.BALANCE].ToString(), ds1.Tables[0].Rows[0]["CURRENCYID"].ToString()) + " " + ds1.Tables[0].Rows[0]["CURRENCYID"].ToString();
                        }
                        LnTable.Rows.Add(r);
                    }
                    else
                    {
                        // Update trang thai Dong cho TK
                        //acct.UpdateStatusAccount(ds.Tables[0].Rows[i]["ACCTNO"].ToString(), ref errorCode, ref errorDesc);
                    }
                }

            }
            rptAccount.DataSource = LnTable;
            rptAccount.DataBind();
            if (LnTable.Rows.Count == 0)
            {
                ltrDD.Text = Resources.labels.banchuadangkytaikhoan;

            }
            if (LnTable == null)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].Trim() == "86")
                {
                    this.Visible = false;
                }
                else
                {
                    ltrDD.Text = Resources.labels.banchuadangkytaikhoan;
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

}
