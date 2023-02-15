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
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBAccountListFD_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (Session["userType"].ToString().Trim() == "O")
            //{
            //    this.Visible = false;
            //    return;
            //}
            //else
            //{
            //    this.Visible = true;
            //}

            DataTable FdTable = new DataTable();
            DataColumn accountCol = new DataColumn("Account");
            DataColumn descCol = new DataColumn("Desc");
            DataColumn dateCol = new DataColumn("Date");
            DataColumn totalCol = new DataColumn("Total");
            FdTable.Columns.AddRange(new DataColumn[] { accountCol, descCol, dateCol, totalCol });
            string errorCode = "0";
            string errorDesc = string.Empty;
            string Status = string.Empty;
            DataSet ds = new DataSet();
            SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
            ds = acct.getAccount(Session["userID"].ToString(), "IB000200", "FD", ref errorCode, ref errorDesc);
            DataSet dsDetailAcc;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow r = FdTable.NewRow();
                dsDetailAcc = new DataSet();
                dsDetailAcc = acct.GetFDAcctInfo(Session["userID"].ToString(), ds.Tables[0].Rows[i]["ACCTNO"].ToString(), ref errorCode, ref errorDesc);
                if (dsDetailAcc.Tables.Count != 0)
                {
                    Status = string.Empty;
                    try
                    {
                        Status = dsDetailAcc.Tables[0].Rows[0]["Statuscd"].ToString() as string;
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                    }

                    if (String.IsNullOrEmpty(Status) == false && Status.ToUpper() != "CLS" && Status.ToUpper() != "S")
                    {
                        r["Account"] = "<a href='"+ SmartPortal.Common.Encrypt.EncryptURL("?p=105&ACCTNO=" + ds.Tables[0].Rows[i]["ACCTNO"].ToString()) + "'>" + ds.Tables[0].Rows[i]["ACCTNO"].ToString() + "</a>";
                        r["Desc"] = Resources.labels.taikhoantietkiemcokyhan;// dsDetailAcc.Tables[0].Rows[0]["ACCTNAME"].ToString();//"Tiết kiệm có kỳ hạn";
                        r["Date"] = SmartPortal.Common.Utilities.Utility.FormatDatetime(dsDetailAcc.Tables[0].Rows[0]["LASTTRANSDATE"].ToString(), "dd/MM/yyyy",SmartPortal.Common.Utilities.DateTimeStyle.DateMMM );
                        //r["Total"] = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim()) + " " + dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                        if (dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim() == SmartPortal.Constant.IPC.LAK)
                        {
                            r["Total"] = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim()) + " " + dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();

                        }
                        else
                        {
                            r["Total"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString(), dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim()) + " " + dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                        }
                        FdTable.Rows.Add(r);
                    }
                    else
                    {
                        // Update trang thai Dong cho TK
                        //acct.UpdateStatusAccount(ds.Tables[0].Rows[i]["ACCTNO"].ToString(), ref errorCode, ref errorDesc);
                    }
                }
            }
            if (FdTable.Rows.Count > 0)
            {
                StringBuilder sT = new StringBuilder();
                sT.Append("<table class='style1' cellpadding='5' cellspacing='0'>");
                sT.Append("<tr class='trheaderFD'>");
                sT.Append("<td width='25%'>");
                sT.Append(Resources.labels.sotaikhoan);
                sT.Append("</td>");
                sT.Append("<td width='25%'>");
                sT.Append(Resources.labels.loaitaikhoan);
                sT.Append("</td>");
                sT.Append("<td width='25%'>");
                sT.Append(Resources.labels.ngaygiaodichcuoi);
                sT.Append("</td>");
                sT.Append("<td width='25%'>");
                sT.Append(Resources.labels.soduduocsudung);
                sT.Append("</td>");
                sT.Append("</tr>");
                foreach (DataRow row in FdTable.Rows)
                {
                    sT.Append("<tr>");
                    sT.Append("<td>");
                    sT.Append(row["Account"].ToString());
                    sT.Append("</td>");
                    sT.Append("<td>");
                    sT.Append(row["Desc"].ToString());
                    sT.Append("</td>");
                    sT.Append("<td>");
                    sT.Append(row["Date"].ToString());
                    sT.Append("</td>");
                    sT.Append("<td>");
                    sT.Append(row["Total"].ToString());
                    sT.Append("</td>");
                    sT.Append("</tr>");
                }
                sT.Append("</table>");
                ltrFD.Text = sT.ToString();
            }
            else
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].Trim() == "86")
                {
                    this.Visible = false;
                }
                else
                {
                    StringBuilder sT = new StringBuilder();
                    sT.Append("<table class='style1' cellpadding='5' cellspacing='0'>");
                    sT.Append("<tr class='trheaderFD'>");
                    sT.Append("<td width='25%'>");
                    sT.Append(Resources.labels.sotaikhoan);
                    sT.Append("</td>");
                    sT.Append("<td width='25%'>");
                    sT.Append(Resources.labels.loaitaikhoan);
                    sT.Append("</td>");
                    sT.Append("<td width='25%'>");
                    sT.Append(Resources.labels.ngaygiaodichcuoi);
                    sT.Append("</td>");
                    sT.Append("<td width='25%'>");
                    sT.Append(Resources.labels.soduduocsudung);
                    sT.Append("</td>");
                    sT.Append("</tr>");

                    sT.Append("<tr>");
                    sT.Append("<td colspan='4' style='text-align:center;color:red;font-weight:bold'>");
                    sT.Append(Resources.labels.banchuadangkytaikhoan);
                    sT.Append("</td>");

                    sT.Append("</table>");

                    ltrFD.Text = sT.ToString();
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}
