﻿using System;
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
using System.Threading;

public partial class Widgets_IBAccountListDDTK_Widget : WidgetBase
{
    Thread th;
    private Object m_lock = new Object();
    protected void Page_Load(object sender, EventArgs e)
    {
        ltrDDTK.Text = string.Empty;
        //if (Session["userType"].ToString().Trim() == "O")
        //{
        //    this.Visible = false;
        //    return;
        //}
        //else
        //{
        //    this.Visible = true;
        //}

        //Bang chua thong tin tai khoan DD
    }
    private void LoadAccount()
    {
        try
        {
            DataTable ddTable = new DataTable();
            DataColumn accountCol = new DataColumn("Account");
            DataColumn detailCol = new DataColumn("Detail");
            DataColumn descCol = new DataColumn("Desc");
            DataColumn dateCol = new DataColumn("Date");
            DataColumn totalCol = new DataColumn("Total");
            DataColumn clStatus = new DataColumn("Status");
            DataColumn StatusImg = new DataColumn("StatusImg");
            DataColumn acttypeCol = new DataColumn("Type");
            ddTable.Columns.AddRange(new DataColumn[] { accountCol, detailCol, descCol, dateCol, totalCol, clStatus, StatusImg, acttypeCol });
            string errorCode = "0";
            string errorDesc = string.Empty;
            DataSet ds = new DataSet();
            SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
            ds = acct.getAccount(Session["userID"].ToString(), "IB000200", "DD", ref errorCode, ref errorDesc);
            string Status = string.Empty;
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
                        Status = string.Empty;
                        try
                        {
                            Status = dsDetailAcc.Tables[0].Rows[0]["Statuscd"].ToString() as string;
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
                            r["Detail"] = "?po=3&p=104&ACCTNO=" + ds.Tables[0].Rows[i]["ACCTNO"].ToString();
                            r["Desc"] = Resources.labels.taikhoantietkiemonline;//dsDetailAcc.Tables[0].Rows[0]["ACCTNAME"].ToString();//Resources.labels.tktkkkh;
                            r["Date"] = SmartPortal.Common.Utilities.Utility.FormatDatetime(dsDetailAcc.Tables[0].Rows[0]["LASTTRANSDATE"].ToString(), "dd/MM/yyyy", SmartPortal.Common.Utilities.DateTimeStyle.DateMMM);
                            //r["Total"] = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim()) + " " + dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
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
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].Trim() == "86")
                    {
                        this.Visible = false;
                    }
                    else
                    {
                        ltrDDTK.Text = Resources.labels.banchuadangkytaikhoan;
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
    protected void OnGetAccountListFinish(object sender, EventArgs e)
    {
        // edit by LanNTH - khong dung session de bind du lieu
        LoadAccount();
        Timer1.Enabled = false;
    }
}
