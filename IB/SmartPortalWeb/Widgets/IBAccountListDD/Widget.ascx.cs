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

using System.Data.SqlClient;
using System.Text;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using System.Threading;

public partial class Widgets_IBAccountListDD_Widget : WidgetBase
{
    Thread th;
    private Object m_lock = new Object();
    protected void Page_Load(object sender, EventArgs e)
    {
        ltrDD.Text = string.Empty;
    }
    private void LoadAccount()
    {
        lock (m_lock)
        {
            try
            {
                //UpdateAllAccount();

                //Bang chua thong tin tai khoan DD
                DataTable ddTable = new DataTable();
                DataColumn accountCol = new DataColumn("Account");
                DataColumn detailCol = new DataColumn("Detail");
                DataColumn descCol = new DataColumn("Desc");
                DataColumn dateCol = new DataColumn("Date");
                DataColumn totalCol = new DataColumn("Total");
                DataColumn Status = new DataColumn("Status");
                DataColumn StatusImg = new DataColumn("StatusImg");
                DataColumn acttypeCol = new DataColumn("Type");
                ddTable.Columns.AddRange(new DataColumn[] { accountCol, detailCol, descCol, dateCol, totalCol, Status, StatusImg, acttypeCol });
                string errorCode = "0";
                string errorDesc = string.Empty;
                DataSet ds = new DataSet();
                SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
                ds = acct.getAccount(Session["userID"].ToString(), "IB000200", "CD", ref errorCode, ref errorDesc);
                string status = string.Empty;
                if (errorCode == "0")
                {
                    DataSet dsDetailAcc;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow r = ddTable.NewRow();
                        dsDetailAcc = new DataSet();
                        dsDetailAcc = acct.GetInfoDD(Session["userID"].ToString(), ds.Tables[0].Rows[i]["ACCTNO"].ToString(), ref errorCode, ref errorDesc);
                        if (dsDetailAcc.Tables.Count != 0)
                        {
                            status = string.Empty;
                            try
                            {
                                status = dsDetailAcc.Tables[0].Rows[0]["Statuscd"].ToString() as string;
                            }
                            catch (Exception ex)
                            {
                                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
                            }
                            //r["Type"] = ds.Tables[0].Rows[i]["typeid"].ToString();
                            switch (status)
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
                            if (String.IsNullOrEmpty(status) == false && status.ToUpper() != "CLS" && status.ToUpper() != "S")
                            {
                                r["Account"] = ds.Tables[0].Rows[i]["ACCTNO"].ToString();
                                r["Detail"] = "?po=3&p=107&ACCTNO=" + ds.Tables[0].Rows[i]["ACCTNO"].ToString();
                                //r["Desc"] = dsDetailAcc.Tables[0].Rows[0]["ACCTNAME"].ToString();//Resources.labels.tktt;
                                r["Desc"] = Resources.labels.tktt;
                                r["Date"] = SmartPortal.Common.Utilities.Utility.FormatDatetime(dsDetailAcc.Tables[0].Rows[0]["LASTTRANSDATE"].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
                                if (dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim() == SmartPortal.Constant.IPC.LAK)
                                {
                                    r["Total"] = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim()) + " " + dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();

                                }
                                else
                                {
                                    r["Total"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString(), dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim()) + " " + dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                                }
                                ddTable.Rows.Add(r);
                            }
                            else
                            {
                                // Update trang thai Dong cho TK
                                //acct.UpdateStatusAccount(ds.Tables[0].Rows[i]["ACCTNO"].ToString(), ref errorCode, ref errorDesc);
                            }
                        }
                    }
                    rptAccount.DataSource = ddTable;
                    rptAccount.DataBind();
                    if (ddTable.Rows.Count == 0)
                    {
                        ltrDD.Text = Resources.labels.banchuadangkytaikhoan;

                    }
                    if (ddTable == null)
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
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
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
    protected void OnGetAccountListFinish(object sender, EventArgs e)
    {
        // edit by LanNTH - khong dung session de bind du lieu
        LoadAccount();
        Timer1.Enabled = false;
    }
    // Phan nay dac biet dung de update moi tk
    //private void UpdateAllAccount()
    //{
    //    //Get TK tu DB
    //    string errorCode = string.Empty;
    //    string errorDesc = string.Empty;
    //    string AccountList = string.Empty;
    //    string CustCode = string.Empty;
    //    string CFType = string.Empty;
    //    DataSet dsAcctEB = new DataSet();
    //    DataSet dsAcctCore = new DataSet();
    //    DataSet dsCustInfo = new DataSet();
    //    SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
    //    dsAcctEB = acct.getAccount(Session["userID"].ToString(), "IB000200", "", ref errorCode, ref errorDesc);
    //    DataRow[] dr;
    //    if (errorCode == "0")
    //    {
    //        dsCustInfo = acct.GetCustIDCustType(Session["userID"].ToString(), ref errorCode, ref errorDesc);
    //        if (errorCode == "0" && dsCustInfo.Tables[0].Rows.Count == 1)
    //        {
    //            CustCode = dsCustInfo.Tables[0].Rows[0]["CUSTCODE"].ToString().Replace(" ", "");
    //            CFType = dsCustInfo.Tables[0].Rows[0]["CFTYPE"].ToString().Replace(" ", "");
    //            for (int i = 0; i < dsAcctEB.Tables[0].Rows.Count; i++)
    //            {
    //                if (i == dsAcctEB.Tables[0].Rows.Count - 1)
    //                    AccountList = AccountList + "'" + dsAcctEB.Tables[0].Rows[i]["ACCTNO"].ToString().Trim() + "'";
    //                else
    //                    AccountList = AccountList + "'" + dsAcctEB.Tables[0].Rows[i]["ACCTNO"].ToString().Trim() + "',";
    //            }
    //            dsAcctCore = acct.GetTKKH(CustCode.Replace(" ", ""), CFType.Replace(" ", ""), ref errorCode, ref errorDesc);
    //            //
    //            if (dsAcctCore != null && dsAcctCore.Tables.Count > 0)
    //            {
    //                dr = dsAcctCore.Tables[0].Select("accountno not in (" + AccountList + ") and statuscd not in" + "('CLS')");
    //                for (int j = 0; j < dr.Length; j++)
    //                {
    //                    acct.InsertNewAcct(dr[j]["accountno"].ToString(), dr[j]["typeid"].ToString(), dr[j]["ccyid"].ToString()
    //                        , SmartPortal.Common.Utilities.Utility.FormatStringCore(dr[j]["branch"].ToString()), dr[j]["statuscd"].ToString(), CustCode
    //                        , CFType, ref errorCode, ref errorDesc);
    //                }
    //                dr = dsAcctCore.Tables[0].Select("statuscd in" + "('CLS')");
    //                for (int k = 0; k < dr.Length; k++)
    //                {
    //                    acct.UpdateCloseAcct(dr[k]["accountno"].ToString(), dr[k]["statuscd"].ToString(), ref errorCode, ref errorDesc);
    //                }
    //            }
    //        }
    //    }
    //    //Get TK tu Core

    //    //So sanh
    //    //+ Them moi
    //    //+close
    //}
}
