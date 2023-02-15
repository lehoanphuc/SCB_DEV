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
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using System.Globalization;
using System.Collections.Generic;
using System.Drawing;



public partial class Widgets_IBSTV_ViewDetails_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBSTV_ViewDetails_Widget", "Page_Load", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBSTV_ViewDetails_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        } 
    }
    void BindData()
    {

        #region Lấy thông tin lịch
        string creditbranchid = string.Empty;
        string amountsche = string.Empty;


        DataSet ScheduleDS = new DataSet();
        DataTable ScheduleTB = new DataTable();
        DataTable ScheduleDetailTB = new DataTable();
        DataTable ScheduleDayTB = new DataTable();
        string SID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"].ToString();
        SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
        ScheduleDS = new SmartPortal.IB.Schedule().GetInfo_Schedule_ByID(SID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ScheduleDS.Tables[0].Rows.Count != 0)
        {
            ScheduleTB = ScheduleDS.Tables[0];
            switch (ScheduleTB.Rows[0]["SCHEDULETYPE"].ToString().Trim())
            {
                case "WEEKLY":
                    lblScheduleType.Text = Resources.labels.hangtuan;
                    break;
                case "DAILY":
                    lblScheduleType.Text = Resources.labels.hangngay;
                    break;
                case "MONTHLY":
                    lblScheduleType.Text = Resources.labels.hangthang;
                    break;
                case "ONETIME":
                    lblScheduleType.Text = Resources.labels.motlan;
                    break;
            }
            
            lbschedulename.Text = ScheduleTB.Rows[0]["SCHEDULENAME"].ToString();
            lblTransferType.Text = ScheduleTB.Rows[0]["PAGENAME"].ToString();
            //lbfromdate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ScheduleTB.Rows[0]["CREATEDATE"].ToString(), "dd/MM/yyyy");
            lbfromdate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ScheduleTB.Rows[0]["SCHEDULETIME"].ToString(), "dd/MM/yyyy");

            lbtodate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ScheduleTB.Rows[0]["ENDDATE"].ToString(), "dd/MM/yyyy");
            lbtime.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ScheduleTB.Rows[0]["NEXTEXECUTE"].ToString(), "dd/MM/yyyy HH:mm:ss");
            //if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["trcd"].ToString() == SmartPortal.Constant.IPC.TOB)
            //{
            //    pnTaiKhoanBaoCo.Visible = true;
            //    pnConfirmCMND.Visible = true;
            //    pnBank.Visible = true;
            //    pnConFirmFee.Visible = true;
            //}
            //else
            //{
                pnTaiKhoanBaoCo.Visible = true;
                pnConfirmCMND.Visible = false;
                pnBank.Visible = false;
                //pnBank.Height = 0;
                //pnConFirmFee.Visible = false;
                pnConFirmFee.Visible = true ;

                //pnConFirmFee.Height = 0;
            //}
            if (ScheduleDS.Tables[1].Rows.Count != 0)
            {
                ScheduleDetailTB = ScheduleDS.Tables[1];
                for (int i = 0; i < ScheduleDetailTB.Rows.Count; i++)
                {
                    string paravalue = ScheduleDetailTB.Rows[i]["PARAVALUE"].ToString();
                    switch (ScheduleDetailTB.Rows[i]["PARANAME"].ToString())
                    {
                       
                        case SmartPortal.Constant.IPC.ACCTNO:
                            lbaccount.Text = paravalue;
                            Hashtable hasSender = objAcct.loadInfobyAcct(paravalue);
                            if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                            {
                                lbsender.Text = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                            }
                            break;
                        case SmartPortal.Constant.IPC.AMOUNT:
                            lbamount.Text = paravalue;
                            amountsche = paravalue;
                            break;
                        case SmartPortal.Constant.IPC.CCYID:
                            lblFeeCCYID.Text = paravalue;
                            lbccyid.Text = paravalue;
                            break;
                        case SmartPortal.Constant.IPC.TRANDESC:
                            lbdesc.Text = paravalue;
                            break;
                        case SmartPortal.Constant.IPC.RECEIVERACCOUNT:
                            lbreceiveaccount.Text = paravalue;
                            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["trcd"].ToString() == SmartPortal.Constant.IPC.TOB )
                            {
                                if (paravalue != "")
                                {
                                    pnTaiKhoanBaoCo.Visible = true;
                                    pnConfirmCMND.Visible = false;
                                    pnBank.Visible = true;
                                    pnConFirmFee.Visible = true;
                                }
                                else
                                {
                                    pnTaiKhoanBaoCo.Visible = false;
                                   // pnTaiKhoanBaoCo.Height = 0;
                                    pnConfirmCMND.Visible = true;
                                    pnBank.Visible = true;
                                    pnConFirmFee.Visible = true;
                                }
                            }
                            else
                            {
                                DataSet dsAcct = objAcct.CheckAccountExists(paravalue);
                                if (dsAcct.Tables[0].Rows.Count > 0)
                                {
                                    lbreceiver.Text = dsAcct.Tables[0].Rows[0][SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                                }
                            }
                            break;
                        case SmartPortal.Constant.IPC.RECEIVERNAME:
                            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["trcd"].ToString() == SmartPortal.Constant.IPC.TOB)
                            {
                                lbreceiver.Text = paravalue;
                            }
                            
                            break;
                        case SmartPortal.Constant.IPC.ISSUEDATE:
                            pnTaiKhoanBaoCo.Visible = false;
                            //pnTaiKhoanBaoCo.Height = 0;
                            pnConfirmCMND.Visible = true;
                            pnBank.Visible = true;
                            pnConFirmFee.Visible = true;
                            lblIssueDate.Text = paravalue;
                            break;
                        case SmartPortal.Constant.IPC.ISSUEPLACE:
                            lblIssuePlace.Text = paravalue;
                            break;
                        case SmartPortal.Constant.IPC.LICENSEID:
                            lblLicense.Text = paravalue;
                            break;
                        case SmartPortal.Constant.IPC.RECEIVERADD:
                            lblConfirmReceiverAdd.Text = paravalue;
                            break;
                        case SmartPortal.Constant.IPC.FEE:
                            lblPhiAmount.Text = paravalue;
                            break;
                        case SmartPortal.Constant.IPC.CHILDBANK:
                            DataTable dtchildbank = new SmartPortal.IB.Schedule().GetChildBank(paravalue);
                            if (dtchildbank.Rows.Count > 0)
                            {
                                lblConfirmChildBank.Text = dtchildbank.Rows[0][SmartPortal.Constant.IPC.BANKNAME].ToString();
                            }
                            break;
                            //9.6.2016 MINH ADD de tinh phi
                        case SmartPortal.Constant.IPC.CREDITBRACHID:
                             creditbranchid = paravalue;
                            break;



                    }
                }
            }

            lbamount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(lbamount.Text, lbccyid.Text.Trim());
            lblPhiAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(lblPhiAmount.Text, lbccyid.Text.Trim());

            if (ScheduleDS.Tables[2].Rows.Count != 0)
            {
                string StrDayno = "";
                ScheduleDayTB = ScheduleDS.Tables[2];
                for (int i = 0; i < ScheduleDayTB.Rows.Count; i++)
                {
                    if (ScheduleDayTB.Rows[i]["DAYNO"].ToString() == "1" && lblScheduleType.Text==SmartPortal.Constant.IPC.WEEKLY)
                    {
                        StrDayno += Resources.labels.chunhat+", ";
                        continue;
                    }
                    StrDayno += ScheduleDayTB.Rows[i]["DAYNO"].ToString() + " , ";
                }
                lbdatetransfer.Text = StrDayno.Substring(0, StrDayno.Length - 2);
                if (ScheduleTB.Rows[0]["SCHEDULETYPE"].ToString().Trim() == "WEEKLY")
                {
                    lbdatetransfer2.Text = (Enum.GetName(typeof(DayOfWeek), (int.Parse(lbdatetransfer.Text) - 1))).ToString();
                }
                else
                {
                    lbdatetransfer2.Text = lbdatetransfer.Text;
                }
       

            }
            // if (ScheduleTB.Rows[0]["SCHEDULETYPE"].ToString().Trim()=="MONTHLY")
            //{
                //tinh lại fee de show
                #region tinh phi
                //edit by VuTran 19/09/2014: tinh lai phi
                string senderfee = "0";
                string receiverfee = "0";
                //DataTable dtFee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), "IB000201", SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ddlSenderAccount.SelectedValue, lblReceiverBranch.Text.Trim(), lblCurrency.Text, "");
                DataTable dtFee = new SmartPortal.IB.Bank().GetFee(ScheduleTB.Rows[0]["USERCREATE"].ToString(), ScheduleTB.Rows[0]["TRANCODE"].ToString(), amountsche, lbaccount.Text, creditbranchid.Trim(), lbccyid.Text, "");
                if (dtFee.Rows.Count != 0)
                {
                    senderfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeSenderAmt"].ToString(), lbccyid.Text);
                    receiverfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeReceiverAmt"].ToString(), lbccyid.Text);
                }
                #endregion
                
            lblPhiAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(senderfee, lblFeeCCYID.Text.Trim());
            //}
            //if (ScheduleTB.Rows[0]["SCHEDULETYPE"].ToString().Trim() == "MONTHLY")
            //{
            //    tbdetailsmonth.Visible = true;
            //    showdetailProcessMonthly(lbfromdate.Text.Trim(), lbtodate.Text.Trim(), SmartPortal.Common.Utilities.Utility.FormatDatetime(ScheduleTB.Rows[0]["NEXTEXECUTE"].ToString(), "HH:mm:ss"), lbdatetransfer.Text, Resources.labels.nguoichuyen, senderfee + ' ' + lblFeeCCYID.Text.Trim());
            //}
            switch (ScheduleTB.Rows[0]["SCHEDULETYPE"].ToString().Trim())
            {
                case SmartPortal.Constant.IPC.MONTHLY:
                    tbdetailsmonth.Visible = true;
                    showdetailProcessMonthly(lbfromdate.Text.Trim(), lbtodate.Text.Trim(), SmartPortal.Common.Utilities.Utility.FormatDatetime(ScheduleTB.Rows[0]["NEXTEXECUTE"].ToString(), "HH:mm:ss"), lbdatetransfer.Text, Resources.labels.nguoichuyen, senderfee + ' ' + lblFeeCCYID.Text.Trim());
                    break;
                default:
                    lbrepeatmonthly.Text = (getRepeateTime(lbfromdate.Text.Trim(), lbtodate.Text.Trim(), SmartPortal.Common.Utilities.Utility.FormatDatetime(ScheduleTB.Rows[0]["NEXTEXECUTE"].ToString(), "HH:mm:ss"), lbdatetransfer.Text, ScheduleTB.Rows[0]["SCHEDULETYPE"].ToString().Trim())).ToString();
                    break;






            }
        }


        #endregion
    }
    private bool showdetailProcessMonthly(string date1, string date2, string time, string day, string feepayer, string fee)
    {
        try
        {
            #region 07/06/2016 minh add details process:
            //lblTextError.Text = string.Empty;

            DateTime dt1 = DateTime.ParseExact(date1 + " " + time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(date2 + " " + time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            int i = (dt2.Year - dt1.Year) * 12 + dt2.Month - dt1.Month;
            if ((dt2.Year - dt1.Year) > int.Parse(ConfigurationManager.AppSettings["LimitYearforSchedule"]))
            {

                //lblTextError.Text = string.Format(Resources.labels.thoigiantacdungcuaschedulebigioihan, ConfigurationManager.AppSettings["LimitYearforSchedule"].ToString());
                return false;
            }
            //(YEAR(LDate)-YEAR(EDate))*12+MONTH(LDate)-MONTH(EDate)
            List<DateTime> allDates = new List<DateTime>();




            for (DateTime date = dt1; date <= dt2; date = date.AddDays(1))
            {
                allDates.Add(date);
            }
            int repeat = 0;
            for (int year = dt1.Year; year <= dt2.Year; year++)
            {
                List<DateTime> SelectedDates = new List<DateTime>();
                foreach (DateTime dt in allDates)
                {
                    if (dt.Year == year)
                    {
                        DateTime endOfMonth = new DateTime(dt.Year,
                                                           dt.Month,
                                                           DateTime.DaysInMonth(dt.Year,
                                                                                dt.Month));
                        if (DateTime.Compare(dt, dt1) >= 0 && DateTime.Compare(dt, dt2) <= 0)
                        {

                            if (dt.Day == int.Parse(day))
                                SelectedDates.Add(dt);
                            else
                                if (endOfMonth.Day < int.Parse(day))
                                {
                                    if (dt.Day == endOfMonth.Day)
                                        SelectedDates.Add(endOfMonth);
                                }
                        }

                    }
                }

                repeat = repeat + SelectedDates.Count;
                lbrepeatmonthly.Text = repeat.ToString();
                trrepeatmonthly.Visible = true;
                //txtRepeat.Text = repeat.ToString();
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                cell1.Text = year.ToString();

                row.Cells.Add(cell1);
                //cell2.Text = string.Format(Resources.labels.contentscheduletransfermonth, Resources.labels.nguoichuyen, fee, year.ToString(), SelectedDates.Count.ToString());
                cell2.Text = string.Format(Resources.labels.contentscheduletransfermonth, year.ToString(), SelectedDates.Count.ToString());

                row.Cells.Add(cell2);
                for (i = 1; i <= 12; i++)
                {
                    TableCell cell3 = new TableCell();
                    cell3.HorizontalAlign = HorizontalAlign.Center;
                    cell3.Text = "";
                    foreach (DateTime dt in SelectedDates)
                    {
                        if (dt.Month == i)
                        {
                            cell3.Text = dt.Day.ToString();
                            if (DateTime.Compare(DateTime.Now, dt) > 0)
                                cell3.BackColor = Color.White;
                            else
                                cell3.BackColor = Color.LightBlue;
                            break;
                        }
                    }
                    row.Cells.Add(cell3);

                }

                tbdetailsmonth.Rows.Add(row);

            }

            return true;

        }
        catch
        {


            //lblTextError.Text = "Error when show processing details";
            return false;
        }
            #endregion
    }
    private int getRepeateTime(string date1, string date2, string time, string day, string schetype)
    {
        int repeat = 0;
        if (schetype.Equals(SmartPortal.Constant.IPC.ONETIME))
        {
            return 1;
        }
        else
        {
            DateTime dt1 = DateTime.ParseExact(date1 + " " + time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(date2 + " " + time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //int i = (dt2.Year - dt1.Year) * 12 + dt2.Month - dt1.Month;

            //(YEAR(LDate)-YEAR(EDate))*12+MONTH(LDate)-MONTH(EDate)
            List<DateTime> allDates = new List<DateTime>();




            for (DateTime date = dt1; date <= dt2; date = date.AddDays(1))
            {
                allDates.Add(date);
            }
            if (schetype.Equals(SmartPortal.Constant.IPC.DAILY))
            {
                return allDates.Count;
            }
            else if (schetype.Equals(SmartPortal.Constant.IPC.WEEKLY))
            {

                List<DateTime> SelectedDates = new List<DateTime>();
                foreach (DateTime dt in allDates)
                {
                    if ((Convert.ToInt32(dt.DayOfWeek) + 1).Equals(int.Parse(day)))
                    {
                        SelectedDates.Add(dt);
                    }
                }
                repeat = repeat + SelectedDates.Count;
                return repeat;
            }
            else return 0;


        }
    }
    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}
