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

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using System.Globalization;
using System.Collections.Generic;
using System.Drawing;



public partial class Widgets_IBScheduleTransfer_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    DataSet dsReceiverList = new DataSet();
    public static string fromdate = "";
    public static string todate = "";
    public static string ccyid = "";
    public static string description = "";
    string senderName = "";
    string receiverName = "";
    string SenderAcc = "";
    string receiverAcc = "";
    string amount = "";
    string desc = "";
    public static string debitBrachID = "";
    public static string crebitBrachID = "";
    public static string ScheType = "";

    // public static string ScheID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
     
            Button5.Attributes.Add("onclick", " this.disabled = true; " + Page.ClientScript.GetPostBackEventReference(Button5, null) + ";");
            //minh add 16/6/2015
            txtScheduleTimeDaily.Text = System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"];


            txtScheduleTimeDaily.Text = Convert.ToDateTime(txtScheduleTimeDaily.Text).ToString("HH:mm:ss");
            txtScheduleTimeOnetime.Text = txtScheduleTimeDaily.Text;
            txtScheduleTimeWeekly.Text = txtScheduleTimeDaily.Text;
            txtScheduleTimeMonthly.Text = txtScheduleTimeDaily.Text;

            lblTextError.Text = "";

            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
            txtAmount1.Attributes.Add("onkeyup", "ntt('" + txtAmount1.ClientID + "','" + lblText1.ClientID + "',event)");
            txtAmount2.Attributes.Add("onkeyup", "ntt('" + txtAmount2.ClientID + "','" + lblText2.ClientID + "',event)");
            if (!IsPostBack)
            {

                //hide panel
                pnSchedule.Visible = true;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = false;
                pnTOB.Visible = false;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = false;

                //load day
                for (int i = 1; i < 32; i++)
                {
                    ListItem li = new ListItem(i.ToString(), i.ToString());

                    ddlMonthlyDayNo.Items.Add(li);

                }
                //default day
                txtFromM.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                txtToM.Text = DateTime.Now.AddDays(1).AddMonths(1).ToString("dd/MM/yyyy");
                //set hour
                for (int j = 0; j < 24; j++)
                {
                    ListItem li = new ListItem(j.ToString(), j.ToString());
                    //ddlHour.Items.Add(li);
                    //ddlhourD.Items.Add(li);
                    //ddlhourW.Items.Add(li);
                    //ddlhourM.Items.Add(li);

                }
                //set minute
                for (int k = 0; k < 56; k += 5)
                {
                    ListItem li = new ListItem(k.ToString(), k.ToString());
                    //ddlMinute.Items.Add(li);
                    //ddlminuteD.Items.Add(li);
                    //ddlminuteW.Items.Add(li);
                    //ddlminuteM.Items.Add(li);
                }
                //load trancode cho ddlTransferType
                ddlTransferType.DataSource = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISSCHEDULE), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                ddlTransferType.DataTextField = "PAGETITLE";
                ddlTransferType.DataValueField = "TRANCODE";
                ddlTransferType.DataBind();
                //ddlTransferType.Items.Remove(ddlTransferType.Items.FindByValue("IBINTERBANKTRANSFER"));
                //Load ddlsenderaccount
                DataSet ds = new DataSet();
                //DataSet dsReceiverList = new DataSet();
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                ds = objAcct.getAccount(Session["userID"].ToString(), "IB000201", "", ref IPCERRORCODE, ref IPCERRORDESC);
                ds.Tables[0].DefaultView.RowFilter = "accttype in ('DD', 'CD') and status = 'A'";
                ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                ddlSenderAccount.DataTextField = SmartPortal.Constant.IPC.ACCTNO;
                ddlSenderAccount.DataValueField = SmartPortal.Constant.IPC.ACCTNO;
                ddlSenderAccount.DataBind();

                ddlSenderAcc.DataSource = ds.Tables[0].DefaultView;
                ddlSenderAcc.DataTextField = SmartPortal.Constant.IPC.ACCTNO;
                ddlSenderAcc.DataValueField = SmartPortal.Constant.IPC.ACCTNO;
                ddlSenderAcc.DataBind();
                //Load ddlnguoihuongthu
                dsReceiverList = objAcct.GetReceiverList(Session["userID"].ToString(), SmartPortal.Constant.IPC.TIB);
                string isSendReceiver = dsReceiverList.Tables[1].Rows[0]["ISRECEIVERLIST"].ToString();
                if (isSendReceiver.Equals("N"))
                {
                    DataRow row = dsReceiverList.Tables[0].NewRow();
                    row["ID"] = "1";
                    row["RECEIVERNAME"] = "Other";
                    dsReceiverList.Tables[0].Rows.InsertAt(row, 0);

                }
                else
                {
                    if (dsReceiverList == null || dsReceiverList.Tables.Count == 0 || dsReceiverList.Tables[0].Rows.Count == 0)
                    {
                        throw new IPCException("4012");

                    }
                }
                ddlNguoiThuHuongTIB.DataSource = dsReceiverList.Tables[0];
                ddlNguoiThuHuongTIB.DataTextField = "RECEIVERNAME";
                ddlNguoiThuHuongTIB.DataValueField = "ID";
                ddlNguoiThuHuongTIB.DataBind();


                ddlNguoiThuHuong.DataSource = ds.Tables[0].DefaultView;
                ddlNguoiThuHuong.DataTextField = SmartPortal.Constant.IPC.ACCTNO;
                ddlNguoiThuHuong.DataValueField = SmartPortal.Constant.IPC.ACCTNO;
                ddlNguoiThuHuong.DataBind();
                txtRecieverAccount.Enabled = true;
                //phuoc edit = Phước đít
                ddlNguoiThuHuongTIB_SelectedIndexChanged(sender, e);

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


    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        try
        {
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            switch (ddlTransferType.SelectedValue)
            {
                case "IB000208":
                    Hashtable hasSender = objAcct.loadInfobyAcct(txtRecieverAccount.Text.Trim(), Session["userID"].ToString());
                    if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                    {
                        //senderName = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                        //balanceSender =SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                        if(hasSender["CURRENCYID"] != null)
                        {
                            lblreceiverCurrency.Text = hasSender["CURRENCYID"].ToString();
                        }                      
                        //lblSenderBranch.Text = hasSender[SmartPortal.Constant.IPC.BRANCHID].ToString();
                    }
                    //LayCCYID(txtRecieverAccount.Text.Trim(), lblreceiverCurrency);
                    // CHECK SAME CCYCD
                    if (System.Configuration.ConfigurationManager.AppSettings["AllowTransferMultiCurrency"].ToString().Equals("0"))
                    {
                        bool sameCCYCD = objAcct.CheckSameCCYCD(lblreceiverCurrency.Text.Trim(), lblCurrency.Text.Trim());
                        if (!sameCCYCD)
                        {
                            lblTextError.Text = Resources.labels.invalidacctCCYCDtotransfer;
                            return;
                        }
                    }
                    break;
                case "IB000201":

                    // CHECK SAME ACCOUNT
                    if (!ddlNguoiThuHuong.Text.Equals(""))
                    {
                        bool sameAcct = objAcct.CheckSameAccount(ddlSenderAcc.Text.ToString(), ddlNguoiThuHuong.Text.Trim().ToString());
                        if (!sameAcct)
                        {
                            lblTextError.Text = Resources.labels.Accountnotsame;
                            return;
                        }
                    }
                    LayCCYID(ddlNguoiThuHuong.SelectedValue.ToString().Trim(), lblreceiverCCYIDBAC);
                    // CHECK SAME CCYCD
                    if (System.Configuration.ConfigurationManager.AppSettings["AllowTransferMultiCurrency"].ToString().Equals("0"))
                    {
                        bool sameCCYCD = objAcct.CheckSameCCYCD(lblreceiverCCYIDBAC.Text.Trim(), lblCCYIDBAC.Text.Trim());
                        if (!sameCCYCD)
                        {
                            lblTextError.Text = Resources.labels.invalidacctCCYCDtotransfer;
                            return;
                        }
                    }
                    break;
            }

            //an/hien panel

            #region Vi-Invi Panel
            //8.6 minh add de tinh phi
            string senderfee = "0";
            string receiverfee = "0";
            DataTable dtFee = new DataTable();


        


            switch (ddlTransferType.SelectedValue)
            {
                case "IB000208":
                    amount = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), lblCurrency.Text.Trim());
                    lbamount.Text = amount;
                    lbrsamount.Text = amount;
                    desc = txtDesc.Text.Trim();
                    lbdesc.Text = desc;
                    lbrsdesc.Text = desc;
                    //CHECK RECEIVER ACCOUNT IS EXISTS

                    DataSet dsAcct = objAcct.CheckAccountExists(txtRecieverAccount.Text.Trim().ToString(), Session["userID"].ToString());

                    Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.Text.Trim(), Session["userID"].ToString());
                    if (dsAcct.Tables.Count != 0)
                    {
                        if (dsAcct.Tables[0].Rows.Count > 0)
                        {
                            receiverAcc = txtRecieverAccount.Text.Trim();
                            lbaccreceive.Text = receiverAcc;
                            lbrsreceiveracc.Text = receiverAcc;
                            receiverName = dsAcct.Tables[0].Rows[0][SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                            crebitBrachID = dsAcct.Tables[0].Rows[0][SmartPortal.Constant.IPC.BRID].ToString();
                            lbreceiver.Text = receiverName;
                            lbrsreceiver.Text = receiverName;
                        }
                        else
                        {
                            lblTextError.Text = Resources.labels.destacccountinvalid;

                        }
                    }
                    else
                    {
                        lblTextError.Text = Resources.labels.destacccountinvalid;
                    }
                    ///DFDFDFDF

                    if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                    {
                        SenderAcc = ddlSenderAccount.Text.Trim();
                        lbaccsent.Text = SenderAcc;
                        lbrssenderacc.Text = SenderAcc;
                        senderName = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                        lbsender.Text = senderName;
                        lbrssender.Text = senderName;
                        debitBrachID = hasSender[SmartPortal.Constant.IPC.BRID].ToString();
                        ccyid = hasSender["CURRENCYID"].ToString();
                        lbccid.Text = ccyid;
                        lblFeeCCYID.Text = ccyid;
                        lbrsccyid.Text = lbccid.Text;
                    }
                    else
                    {

                        lblTextError.Text = hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                        return;
                    }
                    // CHECK SAME ACCOUNT
                    if (!txtRecieverAccount.Text.Equals(""))
                    {
                        bool sameAcct = objAcct.CheckSameAccount(ddlSenderAccount.Text.ToString(), txtRecieverAccount.Text.Trim().ToString());
                        if (!sameAcct)
                        {
                            lblTextError.Text = Resources.labels.Accountnotsame;
                            return;

                        }
                    }
                    //8.6.2016 minh add tinh phi
                    #region tinh phi
                    //edit by VuTran 19/09/2014: tinh lai phi
                    senderfee = "0";
                    receiverfee = "0";

                    dtFee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), "IB000208", SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), lbaccsent.Text.Trim(), crebitBrachID.Trim(), lblCurrency.Text, "");

                    if (dtFee.Rows.Count != 0)
                    {
                        senderfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeSenderAmt"].ToString(), ccyid);
                        receiverfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeReceiverAmt"].ToString(), ccyid);
                    }
                    #endregion


                    break;
                case "IB000201":
                    amount = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount1.Text.Trim(), lblCCYIDBAC.Text.Trim());
                    lbamount.Text = amount;
                    lbrsamount.Text = amount;
                    desc = txtDesc1.Text.Trim();
                    lbdesc.Text = desc;
                    lbrsdesc.Text = desc;
                    //CHECK RECEIVER ACCOUNT IS EXISTS
                    DataSet dsAcctBAC = objAcct.CheckAccountExists(ddlNguoiThuHuong.Text.Trim().ToString(),Session["userID"].ToString());
                    Hashtable hasSenderBAC = objAcct.loadInfobyAcct(ddlSenderAcc.Text.Trim(), Session["userID"].ToString());
                    if (hasSenderBAC[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                    {
                        SenderAcc = ddlSenderAcc.Text.Trim();
                        lbaccsent.Text = SenderAcc;
                        lbrssenderacc.Text = SenderAcc;
                        senderName = hasSenderBAC[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                        lbsender.Text = senderName;
                        lbrssender.Text = senderName;
                        debitBrachID = hasSenderBAC[SmartPortal.Constant.IPC.BRID].ToString();
                        ccyid = hasSenderBAC["CURRENCYID"].ToString();
                        lbccid.Text = ccyid;
                        lblFeeCCYID.Text = ccyid;
                        lbrsccyid.Text = lbccid.Text;
                    }
                    else
                    {
                        lblTextError.Text = IPCERRORDESC.ToString();
                        return;
                    }
                    ///DFDFDFDF

                    if (dsAcctBAC.Tables[0].Rows.Count > 0)
                    {
                        receiverAcc = ddlNguoiThuHuong.Text.Trim();
                        lbaccreceive.Text = receiverAcc;
                        lbrsreceiveracc.Text = receiverAcc;
                        receiverName = dsAcctBAC.Tables[0].Rows[0][SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                        crebitBrachID = dsAcctBAC.Tables[0].Rows[0][SmartPortal.Constant.IPC.BRID].ToString();
                        lbreceiver.Text = receiverName;
                        lbrsreceiver.Text = receiverName;
                    }
                    else
                    {

                        lblTextError.Text = "Error";
                        return;
                    }
                    //8.6.2016 minh add tinh phi
                    #region tinh phi
                    //edit by VuTran 19/09/2014: tinh lai phi
                    senderfee = "0";
                    receiverfee = "0";
                    dtFee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), "IB000201", SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount1.Text.Trim(), lblCCYIDBAC.Text), lbaccsent.Text.Trim(), crebitBrachID.Trim(), lblCCYIDBAC.Text, "");

                    if (dtFee.Rows.Count != 0)
                    {
                        senderfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeSenderAmt"].ToString(), ccyid);
                        receiverfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeReceiverAmt"].ToString(), ccyid);
                    }
                    #endregion


                    break;
            }
            lblPhiAmount.Text = senderfee.ToString();
            switch (radSchedule.SelectedValue)
            {
                case "D":
                    pnConfirmDaily.Visible = true;
                    pnConfirmMonthly.Visible = false;
                    pnConfirmWeekly.Visible = false;
                    pnConfirmOnetime.Visible = false;
                    lbrepeatdaily.Text = getRepeateTime(txtFromD.Text.Trim(), txtToD.Text, txtScheduleTimeDaily.Text, "", SmartPortal.Constant.IPC.DAILY).ToString();
                    lbrepeatdaily2.Text = lbrepeatdaily.Text;


                    break;
                case "W":
                    pnConfirmDaily.Visible = false;
                    pnConfirmMonthly.Visible = false;
                    pnConfirmWeekly.Visible = true;
                    pnConfirmOnetime.Visible = false;
                    lbrepeatweekly.Text = getRepeateTime(txtFromW.Text, txtToW.Text, txtScheduleTimeWeekly.Text, ddlWeekyDayNo.SelectedValue.ToString(), SmartPortal.Constant.IPC.WEEKLY).ToString();
                    lbrepeatweekly2.Text = lbrepeatweekly.Text;


                    break;
                case "M":
                    pnConfirmDaily.Visible = false;
                    pnConfirmMonthly.Visible = true;
                    pnConfirmWeekly.Visible = false;
                    pnConfirmOnetime.Visible = false;
                    //8.6.2016 minh list processing details
                    if (!(showdetailProcessMonthly(txtFromM.Text.Trim(), txtToM.Text.Trim(), txtScheduleTimeMonthly.Text, ddlMonthlyDayNo.Text, Resources.labels.nguoichuyen, lblPhiAmount.Text + ' ' + lblCurrency.Text.Trim())))
                    {
                        return;
                    }


                    break;
                case "O":
                    pnConfirmDaily.Visible = false;
                    pnConfirmMonthly.Visible = false;
                    pnConfirmWeekly.Visible = false;
                    pnConfirmOnetime.Visible = true;
                    lbrepeatonetime.Text = getRepeateTime("", "", "", "", SmartPortal.Constant.IPC.ONETIME).ToString();
                    lbrepeatonetime2.Text = lbrepeatonetime.Text;


                    break;
            }
            //phân biệt TIB-TOB-BAC
            pnTaiKhoanBaoCo.Visible = true;
            pnConfirmCMND.Visible = false;
            pnBank.Visible = false;
            ///pnConFirmFee.Visible = false;
            pnConFirmFee.Visible = true;

            #endregion

            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
            pnBAC.Visible = false;

            lblConfirmScheduleName.Text = txtScheduleName.Text;
            lblEndSchedule.Text = txtScheduleName.Text;
            lblConfirmTransferType.Text = ddlTransferType.SelectedItem.Text;
            lblEndTransferType.Text = ddlTransferType.SelectedItem.Text;

            lblConfirmWeekyDayNo.Text = ddlWeekyDayNo.SelectedItem.Text;
            lblConfirmMonthDayNo.Text = ddlMonthlyDayNo.SelectedItem.Text;


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
            if (ViewState["isSendFirst"] == null || (int)ViewState["isSendFirst"] == 0)
            {
                LoadLoaiXacThuc();
            }
            else
            {
                if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                }
            }
            Button2.Text = Resources.labels.confirm;
            pnConfirm.Visible = false;
            pnOTP.Visible = true;
            pnTIB.Visible = false;
            pnBAC.Visible = false;

        }
        catch (Exception ex)
        {

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlTransferType.SelectedValue == "IB000208")
            {
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnTOB.Visible = false;
                pnTIB.Visible = true;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = false;
            }
            else if (ddlTransferType.SelectedValue == "IB000206")
            {
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnTOB.Visible = true;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = false;
            }
            else
            {
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnTOB.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = true;
            }
        }
        catch
        {
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {


            if (!(showdetailProcessMonthly(txtFromM.Text.Trim(), txtToM.Text.Trim(), txtScheduleTimeMonthly.Text, ddlMonthlyDayNo.Text, Resources.labels.nguoichuyen, lblPhiAmount.Text + ' ' + lblCurrency.Text.Trim())))
            {
                return;
            }

            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
            pnBAC.Visible = false;

        }
        catch
        {
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string ScheName = Utility.KillSqlInjection(txtScheduleName.Text.Trim());
            string trancode = Utility.KillSqlInjection(ddlTransferType.SelectedValue.ToString());
            string usercreate = Session["userID"].ToString();
            string ScheID = "";
            DataSet DsScheID = SmartPortal.Common.Utilities.Utility.GetIDFromSQL(ref ErrorCode, ref ErrorDesc);
            if (ErrorCode == "0")
            {
                if (DsScheID.Tables.Count > 0)
                {
                    if (DsScheID.Tables[0].Rows.Count > 0)
                    {
                        ScheID = DsScheID.Tables[0].Rows[0]["ID"].ToString().Trim();
                    }
                }
                else
                {
                    ScheID = SmartPortal.Common.Utilities.Utility.GetID(SmartPortal.Constant.IPC.SCHEDULEPREFIX, "", "", 15);//+ DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
                }
            }
            else
                throw new IPCException(ErrorDesc);

            string time = "";
            DataTable tblScheDetail = new DataTable();

            // tạo table  tblScheDetail
            DataColumn ScheIDCol = new DataColumn("ScheduleID");
            DataColumn ParaNameCol = new DataColumn("ParaName");
            DataColumn ParaValueCol = new DataColumn("ParaValue");

            //add col vào tblScheDetail
            tblScheDetail.Columns.AddRange(new DataColumn[] { ScheIDCol, ParaNameCol, ParaValueCol });

            //Get tblScheDetail
            DataRow r0 = tblScheDetail.NewRow();
            r0["ScheduleID"] = ScheID;
            r0["ParaName"] = SmartPortal.Constant.IPC.USERID;
            r0["ParaValue"] = Session["userID"].ToString();
            tblScheDetail.Rows.Add(r0);

            DataRow r1 = tblScheDetail.NewRow();
            r1["ScheduleID"] = ScheID;
            r1["ParaName"] = SmartPortal.Constant.IPC.ACCTNO;
            r1["ParaValue"] = lbaccsent.Text;
            tblScheDetail.Rows.Add(r1);

            DataRow r2 = tblScheDetail.NewRow();
            r2["ScheduleID"] = ScheID;
            r2["ParaName"] = SmartPortal.Constant.IPC.RECEIVERACCOUNT;
            r2["ParaValue"] = lbaccreceive.Text;
            tblScheDetail.Rows.Add(r2);

            DataRow r3 = tblScheDetail.NewRow();
            r3["ScheduleID"] = ScheID;
            r3["ParaName"] = SmartPortal.Constant.IPC.AMOUNT;
            r3["ParaValue"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lbamount.Text, ccyid);
            tblScheDetail.Rows.Add(r3);

            DataRow r4 = tblScheDetail.NewRow();
            r4["ScheduleID"] = ScheID;
            r4["ParaName"] = SmartPortal.Constant.IPC.CCYID;
            r4["ParaValue"] = ccyid;
            tblScheDetail.Rows.Add(r4);

            DataRow r5 = tblScheDetail.NewRow();
            r5["ScheduleID"] = ScheID;
            r5["ParaName"] = SmartPortal.Constant.IPC.TRANDESC;
            r5["ParaValue"] = lbdesc.Text;
            tblScheDetail.Rows.Add(r5);

            DataRow r6 = tblScheDetail.NewRow();
            r6["ScheduleID"] = ScheID;
            r6["ParaName"] = SmartPortal.Constant.IPC.SOURCEID;
            r6["ParaValue"] = "IB";
            tblScheDetail.Rows.Add(r6);
            DataRow r7 = tblScheDetail.NewRow();
            r7["ScheduleID"] = ScheID;
            r7["ParaName"] = SmartPortal.Constant.IPC.SENDERNAME;
            r7["ParaValue"] = lbsender.Text;
            tblScheDetail.Rows.Add(r7);
            DataRow r8 = tblScheDetail.NewRow();
            r8["ScheduleID"] = ScheID;
            r8["ParaName"] = SmartPortal.Constant.IPC.RECEIVERNAME;
            r8["ParaValue"] = lbreceiver.Text;
            tblScheDetail.Rows.Add(r8);
            DataRow r9 = tblScheDetail.NewRow();
            r9["ScheduleID"] = ScheID;
            r9["ParaName"] = SmartPortal.Constant.IPC.DEBITBRACHID;
            r9["ParaValue"] = debitBrachID;
            tblScheDetail.Rows.Add(r9);
            DataRow r10 = tblScheDetail.NewRow();
            r10["ScheduleID"] = ScheID;
            r10["ParaName"] = SmartPortal.Constant.IPC.CREDITBRACHID;
            r10["ParaValue"] = crebitBrachID;
            tblScheDetail.Rows.Add(r10);

            //Thêm row cho TOB
            if (ddlTransferType.SelectedValue.ToString() == SmartPortal.Constant.IPC.TOB)
            {
                if (radCMND.Checked)
                {
                    DataRow r11 = tblScheDetail.NewRow();
                    r11["ScheduleID"] = ScheID;
                    r11["ParaName"] = SmartPortal.Constant.IPC.LICENSEID;
                    r11["ParaValue"] = lblLicense.Text;
                    tblScheDetail.Rows.Add(r11);
                    DataRow r12 = tblScheDetail.NewRow();
                    r12["ScheduleID"] = ScheID;
                    r12["ParaName"] = SmartPortal.Constant.IPC.ISSUEDATE;
                    r12["ParaValue"] = lblIssueDate.Text;
                    tblScheDetail.Rows.Add(r12);
                    DataRow r13 = tblScheDetail.NewRow();
                    r13["ScheduleID"] = ScheID;
                    r13["ParaName"] = SmartPortal.Constant.IPC.ISSUEPLACE;
                    r13["ParaValue"] = lblIssuePlace.Text;
                    tblScheDetail.Rows.Add(r13);
                }

                DataRow r14 = tblScheDetail.NewRow();
                r14["ScheduleID"] = ScheID;
                r14["ParaName"] = SmartPortal.Constant.IPC.RECEIVERADD;
                r14["ParaValue"] = lblConfirmReceiverAdd.Text;
                tblScheDetail.Rows.Add(r14);
                //DataRow r15 = tblScheDetail.NewRow();
                //r15["ScheduleID"] = ScheID;
                //r15["ParaName"] = SmartPortal.Constant.IPC.BANKCODE;
                //r15["ParaValue"] = ddlBankRecieve.SelectedValue.ToString();
                //tblScheDetail.Rows.Add(r15);
                DataRow r16 = tblScheDetail.NewRow();
                r16["ScheduleID"] = ScheID;
                r16["ParaName"] = SmartPortal.Constant.IPC.BANKCODE;
                r16["ParaValue"] = ddlChildBank.SelectedValue.ToString();
                tblScheDetail.Rows.Add(r16);
                DataRow r17 = tblScheDetail.NewRow();
                r17["ScheduleID"] = ScheID;
                r17["ParaName"] = SmartPortal.Constant.IPC.CITYCODE;
                r17["ParaValue"] = ddlProvince.SelectedValue.ToString();
                tblScheDetail.Rows.Add(r17);
                DataRow r18 = tblScheDetail.NewRow();
                r18["ScheduleID"] = ScheID;
                r18["ParaName"] = SmartPortal.Constant.IPC.FEE;
                r18["ParaValue"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblPhiAmount.Text.Trim(), ccyid);
                tblScheDetail.Rows.Add(r18);
            }
            ////18/5/2015 thêm trường approved
            //DataRow r19 = tblScheDetail.NewRow();
            //r19["ScheduleID"] = ScheID;
            //r19["ParaName"] = SmartPortal.Constant.IPC.APPROVED;
            //r19["ParaValue"] = "Y";
            //tblScheDetail.Rows.Add(r19);

            //Tạo table ScheduleDay
            DataTable tblScheduleDay = new DataTable();
            DataColumn ScheduleDayIDCol = new DataColumn("ScheduleDayID");
            DataColumn DayNoCol = new DataColumn("DayNo");
            //add col vào SCHEDULEDAY
            tblScheduleDay.Columns.AddRange(new DataColumn[] { ScheduleDayIDCol, DayNoCol });

            //Get ScheduleType & tblScheduleDay
            switch (radSchedule.SelectedValue)
            {
                case "D":
                    ScheType = SmartPortal.Constant.IPC.DAILY;
                    fromdate = lbfromcfD.Text;
                    todate = lbtocfD.Text;
                    time = fromdate + " " + lbcftimeD.Text;
                    break;
                case "W":
                    ScheType = SmartPortal.Constant.IPC.WEEKLY;


                    DataRow row = tblScheduleDay.NewRow();
                    row["ScheduleDayID"] = ScheID;
                    row["DayNo"] = ddlWeekyDayNo.SelectedValue;
                    tblScheduleDay.Rows.Add(row);

                    fromdate = lbfromcfW.Text;
                    todate = lbtocfW.Text;
                    time = fromdate + " " + lbfctimeW.Text;
                    break;
                case "M":
                    ScheType = SmartPortal.Constant.IPC.MONTHLY;

                    DataRow row1 = tblScheduleDay.NewRow();
                    row1["ScheduleDayID"] = ScheID;
                    row1["DayNo"] = ddlMonthlyDayNo.SelectedValue;
                    tblScheduleDay.Rows.Add(row1);

                    fromdate = lbfromcfM.Text;
                    todate = lbtocfM.Text;
                    time = fromdate + " " + lbcftimeM.Text;
                    //8.6.2016 minh list processing details
                    if (!(showdetailProcessMonthly2(txtFromM.Text.Trim(), txtToM.Text.Trim(), txtScheduleTimeMonthly.Text, ddlMonthlyDayNo.Text, Resources.labels.nguoichuyen, lblPhiAmount.Text + ' ' + lblCurrency.Text.Trim())))
                    {
                        return;
                    }


                    break;
                case "O":
                    ScheType = SmartPortal.Constant.IPC.ONETIME;
                    fromdate = lbDateO.Text;
                    todate = lbDateO.Text;
                    time = lbDateO.Text;
                    break;
            }

            //vutran 26032015: approve for corporate user
            //if (int.Parse(Session["userLevel"].ToString().Trim()) > 2)
            //if (int.Parse(Session["userLevel"].ToString().Trim()) >= 1 && !Session["TypeID"].ToString().Equals(""))
            string errocode = "";
            string errordesc = "";
            if (!Session["TypeID"].ToString().Equals("CTK"))
            {
                new SmartPortal.IB.Schedule().InsertSchedule(Session["userID"].ToString(), ScheID, ScheType, time, ScheName, lbdesc.Text, SmartPortal.Constant.IPC.ACTIVE, usercreate, usercreate, SmartPortal.Constant.IPC.NO, "", "", trancode, DateTime.Now.ToString("dd/MM/yyyy"), fromdate, todate, tblScheduleDay, tblScheDetail, ddlLoaiXacThuc.SelectedValue.ToString(), txtOTP.Text.Trim(), lbreceiver.Text, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    new SmartPortal.IB.Schedule().ApproveSchedule(Session["userID"].ToString(), ScheID, ScheType, ScheName, lbdesc.Text, trancode, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lbamount.Text, lbccid.Text), lbccid.Text, lbaccsent.Text, lbaccreceive.Text, lbreceiver.Text, ref errocode, ref errordesc);
                }
            }
            else
            {
                new SmartPortal.IB.Schedule().InsertSchedule(Session["userID"].ToString(), ScheID, ScheType, time, ScheName, lbdesc.Text, SmartPortal.Constant.IPC.ACTIVE, usercreate, usercreate, SmartPortal.Constant.IPC.YES, "", "", trancode, DateTime.Now.ToString("dd/MM/yyyy"), fromdate, todate, tblScheduleDay, tblScheDetail, ddlLoaiXacThuc.SelectedValue.ToString(), txtOTP.Text.Trim(), lbreceiver.Text, ref IPCERRORCODE, ref IPCERRORDESC);
            }

            if (IPCERRORCODE == "0")
            {
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = true;
                lblTextError.Text = Resources.labels.schedulesuccessful;
                pnSchedule.Visible = false;
                pnBAC.Visible = false;
                pnTaiKhoanBaoCoRS.Visible = true;
                pnConfirmCMNDRS.Visible = false;
                pnBankRS.Visible = false;
                //pnConFirmFeeRS.Visible = false;
                pnConFirmFeeRS.Visible = true;
                lblPhiAmountRS.Text = lblPhiAmount.Text;



                if (ddlTransferType.SelectedValue.ToString() == SmartPortal.Constant.IPC.TOB)
                {
                    #region LOAD INFO
                    pnBankRS.Visible = true;
                    pnConFirmFeeRS.Visible = true;
                    if (radCMND.Checked)
                    {
                        pnConfirmCMNDRS.Visible = true;
                        pnTaiKhoanBaoCoRS.Visible = false;
                        //pnTaiKhoanBaoCoRS.Height = 0;
                    }
                    else
                    {
                        pnConfirmCMNDRS.Visible = false;
                        pnTaiKhoanBaoCoRS.Visible = true;
                    }
                    lbrssender.Text = lbsender.Text;
                    lbrssenderacc.Text = lbaccsent.Text;
                    lblLicenseRS.Text = lblLicense.Text;
                    lblIssuePlaceRS.Text = lblIssuePlace.Text;
                    lblIssueDateRS.Text = lblIssueDate.Text;

                    lbrsreceiveracc.Text = lbaccreceive.Text;

                    lbrsreceiver.Text = lbreceiver.Text;
                    lblConfirmReceiverAddRS.Text = lblConfirmReceiverAdd.Text;
                    lblConfirmChildBankRS.Text = lblConfirmChildBank.Text;

                    lbrsamount.Text = lbamount.Text;
                    lbrsdesc.Text = lbdesc.Text;
                    //lblPhiAmountRS.Text = lblPhiAmount.Text;
                    lblFeeCCYIDRS.Text = lblFeeCCYID.Text;
                    #endregion
                }
                //an/hien panel
                switch (radSchedule.SelectedValue)
                {
                    case "D":
                        pnResultDaily.Visible = true;
                        pnResultMonthly.Visible = false;
                        pnResultWeekly.Visible = false;
                        pnResultOnetime.Visible = false;
                        break;
                    case "W":
                        pnResultDaily.Visible = false;
                        pnResultMonthly.Visible = false;
                        pnResultWeekly.Visible = true;
                        pnResultOnetime.Visible = false;
                        lblEndWeekyDayNo.Text = ddlWeekyDayNo.SelectedItem.Text;
                        break;
                    case "M":
                        pnResultDaily.Visible = false;
                        pnResultMonthly.Visible = true;
                        pnResultWeekly.Visible = false;
                        pnResultOnetime.Visible = false;
                        lblEndMonthlyDayNo.Text = ddlMonthlyDayNo.SelectedItem.Text;
                        break;
                    case "O":
                        pnResultDaily.Visible = false;
                        pnResultMonthly.Visible = false;
                        pnResultWeekly.Visible = false;
                        pnResultOnetime.Visible = true;
                        break;
                }

                if (!Session["TypeID"].ToString().Equals("CTK"))
                {
                    switch (errocode)
                    {

                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblTextError.Text = errordesc;
                            if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                            }
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                            lblTextError.Text = Resources.labels.wattingbankapprove;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                            lblTextError.Text = Resources.labels.wattinguserapprove;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                            lblTextError.Text = errordesc;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblTextError.Text = Resources.labels.authentypeinvalid;
                            return;
                        default:
                            lblTextError.Text = string.IsNullOrEmpty(errordesc) ? "Transaction error!" : errordesc;
                            return;
                    }
                }
            }
            else
            {
                switch (IPCERRORCODE)
                {
                    case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                        lblTextError.Text = IPCERRORDESC;
                        if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                        }
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                        lblTextError.Text = Resources.labels.wattingbankapprove;
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                        lblTextError.Text = Resources.labels.wattinguserapprove;
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                        lblTextError.Text = IPCERRORDESC;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                        lblTextError.Text = Resources.labels.authentypeinvalid;
                        return;
                    default:
                        lblTextError.Text = string.IsNullOrEmpty(IPCERRORDESC) ? "Transaction error!" : IPCERRORDESC;
                        return;
                }
            }


        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBScheduleTransfer_Widget", "Button5_Click", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBScheduleTransfer_Widget", "Button5_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            txtScheduleName.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtScheduleName.Text);
            if (string.IsNullOrEmpty(txtScheduleName.Text.Trim()))
            {
                lblTextError.Text = Resources.labels.bancannhaptencholich;
                return;
            }
            LayCCYID(ddlSenderAcc.SelectedValue, lblCCYIDBAC);
            LayCCYID(ddlSenderAccount.SelectedValue, lblCurrency);

            switch (radSchedule.SelectedValue)
            {
                case "D":
                    pnSchedule.Visible = false;
                    pnDaily.Visible = true;
                    pnMonthly.Visible = false;
                    pnWeekly.Visible = false;
                    pnOnetime.Visible = false;
                    pnTIB.Visible = false;
                    pnConfirm.Visible = false;
                    pnOTP.Visible = false;
                    pnResultTransaction.Visible = false;
                    ScheType = SmartPortal.Constant.IPC.DAILY;
                    //lblDailyTD.Text = Resources.labels.scheduletimedefault + " <b>"+System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"]+"</b>";

                    break;
                case "W":
                    pnSchedule.Visible = false;
                    pnDaily.Visible = false;
                    pnMonthly.Visible = false;
                    pnWeekly.Visible = true;
                    pnOnetime.Visible = false;
                    pnTIB.Visible = false;
                    pnConfirm.Visible = false;
                    pnOTP.Visible = false;
                    pnResultTransaction.Visible = false;
                    ScheType = SmartPortal.Constant.IPC.WEEKLY;
                    //lblWeeklyTD.Text = Resources.labels.scheduletimedefault + " <b>" + System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"] + "</b>";
                    break;
                case "M":
                    pnSchedule.Visible = false;
                    pnDaily.Visible = false;
                    pnMonthly.Visible = true;
                    pnWeekly.Visible = false;
                    pnOnetime.Visible = false;
                    pnTIB.Visible = false;
                    pnConfirm.Visible = false;
                    pnOTP.Visible = false;
                    pnResultTransaction.Visible = false;
                    ScheType = SmartPortal.Constant.IPC.MONTHLY;
                    //lblMonthlyTD.Text = Resources.labels.scheduletimedefault  +" <b>"+ System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"]+"</b>";
                    break;
                case "O":
                    pnSchedule.Visible = false;
                    pnDaily.Visible = false;
                    pnMonthly.Visible = false;
                    pnWeekly.Visible = false;
                    pnOnetime.Visible = true;
                    pnTIB.Visible = false;
                    pnConfirm.Visible = false;
                    pnOTP.Visible = false;
                    pnResultTransaction.Visible = false;
                    ScheType = SmartPortal.Constant.IPC.ONETIME;
                    //lblOnetimeTD.Text = Resources.labels.scheduletimedefault + " <b>"+System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"]+"</b>";
                    break;
            }
        }
        catch
        {
        }
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        pnSchedule.Visible = true;
        pnDaily.Visible = false;
        pnMonthly.Visible = false;
        pnWeekly.Visible = false;
        pnOnetime.Visible = false;
        pnTIB.Visible = false;
        pnConfirm.Visible = false;
        pnOTP.Visible = false;
        pnResultTransaction.Visible = false;
    }
    protected void Button13_Click(object sender, EventArgs e)
    {
        pnSchedule.Visible = true;
        pnDaily.Visible = false;
        pnMonthly.Visible = false;
        pnWeekly.Visible = false;
        pnOnetime.Visible = false;
        pnTIB.Visible = false;
        pnConfirm.Visible = false;
        pnOTP.Visible = false;
        pnResultTransaction.Visible = false;
    }
    protected void Button14_Click(object sender, EventArgs e)
    {
        pnSchedule.Visible = true;
        pnDaily.Visible = false;
        pnMonthly.Visible = false;
        pnWeekly.Visible = false;
        pnOnetime.Visible = false;
        pnTIB.Visible = false;
        pnConfirm.Visible = false;
        pnOTP.Visible = false;
        pnResultTransaction.Visible = false;
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        pnSchedule.Visible = true;
        pnDaily.Visible = false;
        pnMonthly.Visible = false;
        pnWeekly.Visible = false;
        pnOnetime.Visible = false;
        pnTIB.Visible = false;
        pnConfirm.Visible = false;
        pnOTP.Visible = false;
        pnResultTransaction.Visible = false;
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        try
        {
            //minh add 8.6.2016 to validate 5 year



            if (!(ValidateYearSchedule(txtFromD.Text.Trim(), txtToD.Text.Trim(), txtScheduleTimeDaily.Text, "")))
            {
                return;
            }


            string timeSchedD = Convert.ToDateTime(txtScheduleTimeDaily.Text).ToString("HH:mm:ss");
            // minh add 04.11.2015 to check time is in range config
            if (!checktimeallow(timeSchedD))
            {
                lblTextError.Text = Resources.labels.youmustinputtimeinrange + "( from " + ConfigurationManager.AppSettings["SCHEDULETIMEFROM"] + " to " +
                    ConfigurationManager.AppSettings["SCHEDULETIMETO"] + ")";
                return;
            }

            if (ddlTransferType.SelectedValue == "IB000208")
            {

                fromdate = txtFromD.Text;
                todate = txtToD.Text;
                lbfromcfD.Text = fromdate;
                lbtocfD.Text = todate;
                //lbcftimeD.Text = System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"];//ddlhourD.SelectedValue.ToString() + ":" + ddlminuteD.SelectedValue.ToString() + ":00";
                lbcftimeD.Text = txtScheduleTimeDaily.Text;
                lbrstimeD.Text = lbcftimeD.Text;

                //result

                lbrsfromD.Text = fromdate;
                lbrstoD.Text = todate;

                //kiem tra thoi gian
                //checkvalidTime(fromdate + " " + txtScheduleTimeDaily.Text);
                if (!checkvalidTime(fromdate + " " + txtScheduleTimeDaily.Text))
                {
                    lblTextError.Text = Resources.labels.Timeschedulemustgreaterthancurrent;
                    return;
                }
                pnSchedule.Visible = false;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = true;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = false;


            }
            else if (ddlTransferType.SelectedValue == "IB000206")
            {

                fromdate = txtFromD.Text;
                todate = txtToD.Text;
                lbfromcfD.Text = fromdate;
                lbtocfD.Text = todate;
                //lbcftimeD.Text = System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"];//ddlhourD.SelectedValue.ToString() + ":" + ddlminuteD.SelectedValue.ToString() + ":00";
                lbcftimeD.Text = txtScheduleTimeDaily.Text;
                lbrstimeD.Text = lbcftimeD.Text;

                //result

                lbrsfromD.Text = fromdate;
                lbrstoD.Text = todate;

                //kiem tra thoi gian
                //checkvalidTime(fromdate + " " + txtScheduleTimeDaily.Text);
                if (!checkvalidTime(fromdate + " " + txtScheduleTimeDaily.Text))
                {
                    lblTextError.Text = Resources.labels.Timeschedulemustgreaterthancurrent;
                    return;
                }
                pnSchedule.Visible = false;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = false;
                pnTOB.Visible = true;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = false;
                LoadInfoForTOB(sender, e);
            }
            else
            {

                fromdate = txtFromD.Text;
                todate = txtToD.Text;
                lbfromcfD.Text = fromdate;
                lbtocfD.Text = todate;
                //lbcftimeD.Text = System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"];
                lbcftimeD.Text = txtScheduleTimeDaily.Text;
                lbrstimeD.Text = lbcftimeD.Text;

                //result

                lbrsfromD.Text = fromdate;
                lbrstoD.Text = todate;

                //kiem tra thoi gian
                //checkvalidTime(fromdate + " " + txtScheduleTimeDaily.Text);
                if (!checkvalidTime(fromdate + " " + txtScheduleTimeDaily.Text))
                {
                    lblTextError.Text = Resources.labels.Timeschedulemustgreaterthancurrent;
                    return;
                }
                pnSchedule.Visible = false;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = false;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = true;
            }
        }
        catch (Exception)
        {
            lblTextError.Text = Resources.labels.errortimeschedule;
            txtScheduleTimeDaily.Focus();

        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        try
        {
            //minh add 8.6.2016 to validate 5 year

            if (!(ValidateYearSchedule(txtFromW.Text.Trim(), txtToW.Text.Trim(), txtScheduleTimeWeekly.Text, ddlWeekyDayNo.Text)))
            {
                return;
            }


            string timeSchedW = Convert.ToDateTime(txtScheduleTimeWeekly.Text).ToString("HH:mm:ss");
            // minh add 04.11.2015 to check time is in range config
            if (!checktimeallow(timeSchedW))
            {
                lblTextError.Text = Resources.labels.youmustinputtimeinrange + "( from " + ConfigurationManager.AppSettings["SCHEDULETIMEFROM"] + " to " +
                    ConfigurationManager.AppSettings["SCHEDULETIMETO"] + ")";
                return;
            }

            if (ddlTransferType.SelectedValue == "IB000208")
            {

                fromdate = txtFromW.Text;
                todate = txtToW.Text;
                lbfromcfW.Text = fromdate;
                lbtocfW.Text = todate;

                //            lbfctimeW.Text = System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"]; //ddlhourW.SelectedValue.ToString() + ":" + ddlminuteW.SelectedValue.ToString() + ":00";
                lbfctimeW.Text = txtScheduleTimeWeekly.Text;
                lbrstimeW.Text = lbfctimeW.Text;
                //result
                lbrsfromW.Text = fromdate;
                lbrstoW.Text = todate;

                //kiem tra ngay thang
                //checkvalidTime(fromdate + " " + txtScheduleTimeWeekly.Text);
                if (!checkvalidTime(fromdate + " " + txtScheduleTimeWeekly.Text))
                {
                    lblTextError.Text = Resources.labels.Timeschedulemustgreaterthancurrent;
                    return;
                }
                pnSchedule.Visible = false;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = true;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = false;

            }
            else if (ddlTransferType.SelectedValue == "IB000206")
            {


                fromdate = txtFromW.Text;
                todate = txtToW.Text;
                lbfromcfW.Text = fromdate;
                lbtocfW.Text = todate;

                //lbfctimeW.Text = System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"]; //ddlhourW.SelectedValue.ToString() + ":" + ddlminuteW.SelectedValue.ToString() + ":00";
                lbfctimeW.Text = txtScheduleTimeWeekly.Text;
                lbrstimeW.Text = lbfctimeW.Text;
                //result
                lbrsfromW.Text = fromdate;
                lbrstoW.Text = todate;

                //kiem tra ngay thang
                //checkvalidTime(fromdate + " " + txtScheduleTimeWeekly.Text);
                if (!checkvalidTime(fromdate + " " + txtScheduleTimeWeekly.Text))
                {
                    lblTextError.Text = Resources.labels.Timeschedulemustgreaterthancurrent;
                    return;
                }
                pnSchedule.Visible = false;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = false;
                pnTOB.Visible = true;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = false;

                LoadInfoForTOB(sender, e);
            }
            else
            {

                fromdate = txtFromW.Text;
                todate = txtToW.Text;
                lbfromcfW.Text = fromdate;
                lbtocfW.Text = todate;

                //lbfctimeW.Text = System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"];//ddlhourW.SelectedValue.ToString() + ":" + ddlminuteW.SelectedValue.ToString() + ":00";
                lbfctimeW.Text = txtScheduleTimeWeekly.Text;
                lbrstimeW.Text = lbfctimeW.Text;
                //result
                lbrsfromW.Text = fromdate;
                lbrstoW.Text = todate;

                //kiem tra ngay thang
                //checkvalidTime(fromdate + " " + txtScheduleTimeWeekly.Text);
                if (!checkvalidTime(fromdate + " " + txtScheduleTimeWeekly.Text))
                {
                    lblTextError.Text = Resources.labels.Timeschedulemustgreaterthancurrent;
                    return;
                }
                pnSchedule.Visible = false;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = false;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = true;


            }
        }
        catch (Exception)
        {
            lblTextError.Text = Resources.labels.errortimeschedule;
            txtScheduleTimeWeekly.Focus();

        }
        //DataColumn ScheduleIDCol = new DataColumn("ScheduleID");
        //DataColumn DayNoCol = new DataColumn("DayNo");
        ////add col vào SCHEDULEDAY
        //tblScheduleDay.Columns.AddRange(new DataColumn[] { ScheduleIDCol, DayNoCol });
        //for (int i = 0; i < cblThu.Items.Count; i++)
        //{
        //    if (cblThu.Items[i].Selected)
        //    {
        //        cblcfW.Items[i].Selected = true;
        //        cblrsW.Items[i].Selected = true;
        //    }
        //}
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        try
        {
            //minh add 8.6.2016 to validate 5 year
            if (!(ValidateYearSchedule(txtFromM.Text.Trim(), txtToM.Text.Trim(), txtScheduleTimeMonthly.Text, ddlMonthlyDayNo.Text)))
            {
                return;
            }



            string timeSchedM = Convert.ToDateTime(txtScheduleTimeMonthly.Text).ToString("HH:mm:ss");
            // minh add 04.11.2015 to check time is in range config
            if (!checktimeallow(timeSchedM))
            {
                lblTextError.Text = Resources.labels.youmustinputtimeinrange + "( from " + ConfigurationManager.AppSettings["SCHEDULETIMEFROM"] + " to " +
                    ConfigurationManager.AppSettings["SCHEDULETIMETO"] + ")";
                return;
            }

            if (ddlTransferType.SelectedValue == "IB000208")
            {

                fromdate = txtFromM.Text;
                todate = txtToM.Text;
                lbfromcfM.Text = fromdate;
                lbtocfM.Text = todate;
                //lbcftimeM.Text = System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"];//ddlhourM.SelectedValue.ToString() + ":" + ddlminuteM.SelectedValue.ToString() + ":00";
                lbcftimeM.Text = txtScheduleTimeMonthly.Text;
                lbrstimeM.Text = lbcftimeM.Text;
                //result   
                lbrsfromM.Text = fromdate;
                lbrstoM.Text = todate;

                //kiem tra ngay thang
                //checkvalidTime(fromdate + " " + txtScheduleTimeMonthly.Text);
                if (!checkvalidTime(fromdate + " " + txtScheduleTimeMonthly.Text))
                {
                    lblTextError.Text = Resources.labels.Timeschedulemustgreaterthancurrent;
                    return;
                }
                pnSchedule.Visible = false;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = true;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = false;
            }
            else if (ddlTransferType.SelectedValue == "IB000206")
            {

                fromdate = txtFromM.Text;
                todate = txtToM.Text;
                lbfromcfM.Text = fromdate;
                lbtocfM.Text = todate;
                //lbcftimeM.Text = System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"];//ddlhourM.SelectedValue.ToString() + ":" + ddlminuteM.SelectedValue.ToString() + ":00";
                lbcftimeM.Text = txtScheduleTimeMonthly.Text;
                lbrstimeM.Text = lbcftimeM.Text;

                //result       
                lbrsfromM.Text = fromdate;
                lbrstoM.Text = todate;

                //kiem tra ngay thang
                //checkvalidTime(fromdate + " " + txtScheduleTimeMonthly.Text);
                if (!checkvalidTime(fromdate + " " + txtScheduleTimeMonthly.Text))
                {
                    lblTextError.Text = Resources.labels.Timeschedulemustgreaterthancurrent;
                    return;
                }

                pnSchedule.Visible = false;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = false;
                pnTOB.Visible = true;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = false;
                LoadInfoForTOB(sender, e);
            }
            else
            {

                fromdate = txtFromM.Text;
                todate = txtToM.Text;
                lbfromcfM.Text = fromdate;
                lbtocfM.Text = todate;

                //lbcftimeM.Text = System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"];//ddlhourM.SelectedValue.ToString() + ":" + ddlminuteM.SelectedValue.ToString() + ":00";
                lbcftimeM.Text = txtScheduleTimeMonthly.Text;
                lbrstimeM.Text = lbcftimeM.Text;
                //result
                lbrsfromM.Text = fromdate;
                lbrstoM.Text = todate;

                //kiem tra ngay thang
                //checkvalidTime(fromdate + " " + txtScheduleTimeMonthly.Text);
                if (!checkvalidTime(fromdate + " " + txtScheduleTimeMonthly.Text))
                {
                    lblTextError.Text = Resources.labels.Timeschedulemustgreaterthancurrent;
                    return;
                }
                pnSchedule.Visible = false;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = false;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = true;
            }
            //DataColumn ScheduleIDCol = new DataColumn("ScheduleID");
            //DataColumn DayNoCol = new DataColumn("DayNo");
            ////add col vào SCHEDULEDAY
            //tblScheduleDay.Columns.AddRange(new DataColumn[] { ScheduleIDCol, DayNoCol });
            //for (int i = 0; i < cblThuM.Items.Count; i++)
            //{
            //    if (cblThuM.Items[i].Selected)
            //    {
            //        cblcfM.Items[i].Selected = true;
            //        cblrsM.Items[i].Selected = true;
            //    }
            //}
        }
        catch (Exception)
        {
            lblTextError.Text = Resources.labels.errortimeschedule;
            txtScheduleTimeMonthly.Focus();

        }
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        try
        {
            //minh add 8.6.2016 to validate 5 year

            if (!(ValidateYearSchedule(DateTime.Now.ToString("dd/MM/yyyy"), txtDateO.Text.Trim(), txtScheduleTimeMonthly.Text, ddlMonthlyDayNo.Text)))
            {
                return;
            }


            string timeSchedO = Convert.ToDateTime(txtScheduleTimeOnetime.Text).ToString("HH:mm:ss");
            // minh add 04.11.2015 to check time is in range config
            if (!checktimeallow(timeSchedO))
            {
                lblTextError.Text = Resources.labels.youmustinputtimeinrange + "( from " + ConfigurationManager.AppSettings["SCHEDULETIMEFROM"] + " to " +
                    ConfigurationManager.AppSettings["SCHEDULETIMETO"] + ")";
                return;
            }

            if (ddlTransferType.SelectedValue == "IB000208")
            {

                //fromdate = txtDateO.Text + " " + System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"];
                fromdate = txtDateO.Text + " " + txtScheduleTimeOnetime.Text;
                lbDateO.Text = fromdate;
                //result
                lbrsDateO.Text = fromdate;

                //kiem tra ngay thang
                //checkvalidTime(fromdate);
                if (!checkvalidTime(fromdate))
                {
                    lblTextError.Text = Resources.labels.Timeschedulemustgreaterthancurrent;
                    return;
                }
                pnSchedule.Visible = false;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = true;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = false;

            }
            else if (ddlTransferType.SelectedValue == "IB000206")
            {

                //fromdate = txtDateO.Text + " " + System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"];
                fromdate = txtDateO.Text + " " + txtScheduleTimeOnetime.Text;
                lbDateO.Text = fromdate;
                //result
                lbrsDateO.Text = fromdate;

                //kiem tra ngay thang
                checkvalidTime(fromdate);
                if (!checkvalidTime(fromdate))
                {
                    lblTextError.Text = Resources.labels.Timeschedulemustgreaterthancurrent;
                    return;
                }
                pnSchedule.Visible = false;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = false;
                pnTOB.Visible = true;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = false;
                LoadInfoForTOB(sender, e);
            }
            else
            {

                //fromdate = txtDateO.Text + " " + System.Configuration.ConfigurationManager.AppSettings["SCHEDULETIMEDEFAULT"];//ddlHour.SelectedValue.ToString() + ":" + ddlMinute.SelectedValue.ToString() + ":00";
                fromdate = txtDateO.Text + " " + txtScheduleTimeOnetime.Text;
                lbDateO.Text = fromdate;
                //result
                lbrsDateO.Text = fromdate;
                //checkvalidTime(fromdate);
                if (!checkvalidTime(fromdate))
                {
                    lblTextError.Text = Resources.labels.Timeschedulemustgreaterthancurrent;
                    return;
                }
                pnSchedule.Visible = false;
                pnDaily.Visible = false;
                pnMonthly.Visible = false;
                pnWeekly.Visible = false;
                pnOnetime.Visible = false;
                pnTIB.Visible = false;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnBAC.Visible = true;

            }
        }
        catch (Exception)
        {
            lblTextError.Text = Resources.labels.errortimeschedule;
            txtScheduleTimeOnetime.Focus();

        }
    }
    protected void Button17_Click(object sender, EventArgs e)
    {
        try
        {
            switch (radSchedule.SelectedValue)
            {
                case "D":
                    pnSchedule.Visible = false;
                    pnDaily.Visible = true;
                    pnMonthly.Visible = false;
                    pnWeekly.Visible = false;
                    pnOnetime.Visible = false;
                    pnTIB.Visible = false;
                    pnTOB.Visible = false;
                    pnConfirm.Visible = false;
                    pnOTP.Visible = false;
                    pnResultTransaction.Visible = false;
                    pnBAC.Visible = false;
                    break;
                case "W":
                    pnSchedule.Visible = false;
                    pnDaily.Visible = false;
                    pnMonthly.Visible = false;
                    pnWeekly.Visible = true;
                    pnOnetime.Visible = false;
                    pnTIB.Visible = false;
                    pnTOB.Visible = false;
                    pnConfirm.Visible = false;
                    pnOTP.Visible = false;
                    pnResultTransaction.Visible = false;
                    pnBAC.Visible = false;
                    break;
                case "M":
                    pnSchedule.Visible = false;
                    pnDaily.Visible = false;
                    pnMonthly.Visible = true;
                    pnWeekly.Visible = false;
                    pnOnetime.Visible = false;
                    pnTIB.Visible = false;
                    pnConfirm.Visible = false;
                    pnTOB.Visible = false;
                    pnOTP.Visible = false;
                    pnResultTransaction.Visible = false;
                    pnBAC.Visible = false;
                    break;
                case "O":
                    pnSchedule.Visible = false;
                    pnDaily.Visible = false;
                    pnMonthly.Visible = false;
                    pnWeekly.Visible = false;
                    pnOnetime.Visible = true;
                    pnTIB.Visible = false;
                    pnTOB.Visible = false;
                    pnConfirm.Visible = false;
                    pnOTP.Visible = false;
                    pnResultTransaction.Visible = false;
                    pnBAC.Visible = false;
                    break;
            }
        }
        catch
        {
        }
    }
    protected void ddlNguoiThuHuongTIB_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlNguoiThuHuongTIB.SelectedValue.ToString() == "1")
            {
                txtRecieverAccount.Text = "";
                txtRecieverAccount.Enabled = true;
            }
            else
            {
                DataSet dsTIB = new SmartPortal.IB.Account().GetReceiverList((Session["userID"].ToString()), SmartPortal.Constant.IPC.TIB);

                if (dsTIB.Tables[0].Rows.Count != 0)
                {
                    DataRow[] dr = dsTIB.Tables[0].Select("ID = '" + ddlNguoiThuHuongTIB.SelectedValue.ToString() + "'");
                    txtRecieverAccount.Text = dr[0][SmartPortal.Constant.IPC.ACCTNO].ToString();

                }
                txtRecieverAccount.Enabled = false;

            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBScheduleTransfer_Widget", "ddlNguoiThuHuongTIB_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    Boolean checkvalidTime(string time)
    {
        try
        {
            if (SmartPortal.Common.Utilities.Utility.IsDateTime1(time).Date > DateTime.Now.Date)
            {
                return true;
            }
            else
            {
                //throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.ErrorTime);
                return false;
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
        return true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        pnSchedule.Visible = true;
        pnDaily.Visible = false;
        pnMonthly.Visible = false;
        pnWeekly.Visible = false;
        pnOnetime.Visible = false;
        pnTIB.Visible = false;
        pnConfirm.Visible = false;
        pnOTP.Visible = false;
        pnResultTransaction.Visible = false;
        pnBAC.Visible = false;
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        //pnSchedule.Visible = true;
        //pnDaily.Visible = false;
        //pnMonthly.Visible = false;
        //pnWeekly.Visible = false;
        //pnOnetime.Visible = false;
        //pnTIB.Visible = false;
        //pnConfirm.Visible = false;
        //pnOTP.Visible = false;
        //pnResultTransaction.Visible = false;
        //pnBAC.Visible = false;
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=117"));
    }
    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LayCCYID(ddlSenderAccount.SelectedValue, lblCurrency);
    }

    public void LayCCYID(string acctno, Label result)
    {
        result.Text = "";
        DataTable tblAcctnoInfo = new SmartPortal.IB.Account().GetAcctnoInfo(acctno, Session["Userid"].ToString());
        if (tblAcctnoInfo.Rows.Count != 0)
        {
            result.Text = tblAcctnoInfo.Rows[0]["CCYID"].ToString();
        }
    }
    protected void ddlSenderAcc_SelectedIndexChanged(object sender, EventArgs e)
    {
        LayCCYID(ddlSenderAcc.SelectedValue, lblCCYIDBAC);
    }
    protected void ddlSenderAccountTOB_SelectedIndexChanged(object sender, EventArgs e)
    {
        LayCCYID(ddlSenderAccountTOB.SelectedValue, lblcurrentcyTOB);
    }
    public void LayThongTinNguoiGui()
    {
        try
        {
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccountTOB.Text.Trim());
            if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                txtSenderName.Text = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
            }
            else
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
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
    public void LoadInfoForTOB(object sender, EventArgs e)
    {
        try
        {
            //Load ddlsenderaccountTOB
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            DataSet dsTOB = objAcct.getAccount(Session["userID"].ToString(), "IB000206", "DD", ref IPCERRORCODE, ref IPCERRORDESC);

            if (dsTOB.Tables.Count == 0)
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.AccountNotRegisted);
            }

            ddlSenderAccountTOB.DataSource = dsTOB;
            ddlSenderAccountTOB.DataValueField = dsTOB.Tables[0].Columns["ACCTNO"].ColumnName.ToString();
            ddlSenderAccountTOB.DataBind();
            LayThongTinNguoiGui();

            //load người thụ hưởng cho TOB
            DataSet dsReceiverList = new DataSet();
            dsReceiverList = objAcct.GetReceiverList(Session["userID"].ToString(), SmartPortal.Constant.IPC.TOB);
            string isSendReceiver = dsReceiverList.Tables[1].Rows[0]["ISRECEIVERLIST"].ToString();
            if (isSendReceiver.Equals("N"))
            {
                DataRow row = dsReceiverList.Tables[0].NewRow();
                row["ID"] = "";
                row["RECEIVERNAME"] = "other";
                dsReceiverList.Tables[0].Rows.InsertAt(row, 0);
            }
            else
            {
                if (dsReceiverList == null || dsReceiverList.Tables.Count == 0 || dsReceiverList.Tables[0].Rows.Count == 0)
                {
                    throw new IPCException("4012");

                }
            }
            ddlNguoiThuHuongTOB.DataSource = dsReceiverList.Tables[0];
            //add to common later
            ddlNguoiThuHuongTOB.DataTextField = dsReceiverList.Tables[0].Columns["RECEIVERNAME"].ColumnName.ToString();
            ddlNguoiThuHuongTOB.DataValueField = dsReceiverList.Tables[0].Columns["ID"].ColumnName.ToString();
            ddlNguoiThuHuongTOB.DataBind();


            //load city
            DataSet dsCity = new DataSet();
            dsCity = objAcct.GetCity();
            if (dsCity.Tables[0].Rows.Count == 0)
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CITYNOTEXISTS);
            }

            ddlProvince.DataSource = dsCity;
            ddlProvince.DataTextField = dsCity.Tables[0].Columns[SmartPortal.Constant.IPC.CITYNAME].ColumnName.ToString();
            ddlProvince.DataValueField = dsCity.Tables[0].Columns[SmartPortal.Constant.IPC.CITYCODE].ColumnName.ToString();
            ddlProvince.DataBind();

            //load ngan hang
            DataTable dtBank = new DataTable();
            dtBank = new SmartPortal.IB.Bank().GetBank();
            if (dtBank.Rows.Count == 0)
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.BANKNOTEXISTS);
            }

            ddlBankRecieve.DataSource = dtBank;
            ddlBankRecieve.DataTextField = "BANKNAME";
            ddlBankRecieve.DataValueField = "BANKID";
            ddlBankRecieve.DataBind();

            ddlNguoiThuHuongTOB_SelectedIndexChanged(sender, e);

            ddlProvince_SelectedIndexChanged(sender, e);
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], Request.Url.Query);
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
    protected void ddlNguoiThuHuongTOB_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtissuedate.Attributes.Add("onclick", " var cal = Calendar.setup({onSelect: function(cal) { cal.hide() } }); cal.manageFields(" + txtissuedate.ClientID + ", " + txtissuedate.ClientID + ", '%d/%m/%Y'); ");
        try
        {
            DataSet dsReceiverList = new DataSet();
            dsReceiverList = new SmartPortal.IB.Account().GetReceiverList(Session["userID"].ToString(), SmartPortal.Constant.IPC.TOB);

            DataRow[] dr = dsReceiverList.Tables[0].Select("ID = '" + ddlNguoiThuHuongTOB.SelectedValue.ToString() + "'");
            if (dr.Length > 0 && !dr[0]["ID"].Equals(""))
            {
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                txtReceiverAccount.Text = dr[0][SmartPortal.Constant.IPC.ACCTNO].ToString();
                //radAcctNo.Checked = true;
                //txtReceiverName.Text = dr[0][SmartPortal.Constant.IPC.RECEIVERNAME].ToString();
                txtCMND.Text = dr[0][SmartPortal.Constant.IPC.LICENSE].ToString();
                txtissueplace.Text = dr[0][SmartPortal.Constant.IPC.ISSUEPLACE].ToString();
                if (dr[0][SmartPortal.Constant.IPC.ISSUEDATE].ToString().Trim().Equals("") || dr[0][SmartPortal.Constant.IPC.ISSUEDATE] == null) { txtissuedate.Text = ""; }
                else
                {
                    txtissuedate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(dr[0][SmartPortal.Constant.IPC.ISSUEDATE].ToString(), "dd/MM/yyyy");
                }
                txtReceiverName.Text = dr[0][SmartPortal.Constant.IPC.RECEIVERNAME].ToString();
                txtReceiverAdd.Text = dr[0][SmartPortal.Constant.IPC.ADDRESS].ToString();

                if (dr[0][SmartPortal.Constant.IPC.ACCTNO].ToString().Trim() != "")
                {
                    ddlProvince.SelectedValue = dr[0][SmartPortal.Constant.IPC.CITYCODE].ToString();
                    ddlBankRecieve.SelectedValue = dr[0][SmartPortal.Constant.IPC.BANKID].ToString();
                    ddlBankRecieve_SelectedIndexChanged(sender, e);
                    ddlChildBank.SelectedValue = dr[0]["BRANCH"].ToString();
                    txtBranchDesc.Text = dr[0][SmartPortal.Constant.IPC.BRANCHDESC].ToString();

                    txtBranchDesc.Enabled = false;
                    ddlProvince.Enabled = false;
                    ddlBankRecieve.Enabled = false;
                    ddlChildBank.Enabled = false;

                    radAcctNo.Checked = true;
                    radCMND.Checked = false;

                    radAcctNo.Enabled = false;
                    radCMND.Enabled = false;
                }
                else
                {
                    txtBranchDesc.Enabled = true;
                    ddlProvince.Enabled = true;
                    ddlBankRecieve.Enabled = true;
                    ddlChildBank.Enabled = true;

                    radCMND.Checked = true;
                    radAcctNo.Checked = false;

                    radAcctNo.Enabled = false;
                    radCMND.Enabled = false;
                }

                txtReceiverAccount.Enabled = false;
                txtReceiverAccount.Enabled = false;
                txtCMND.Enabled = false;
                txtissuedate.Enabled = false;
                txtissueplace.Enabled = false;
                txtReceiverName.Enabled = false;
                txtReceiverAdd.Enabled = false;


            }
            else
            {
                txtReceiverAccount.Text = "";
                txtCMND.Text = "";
                txtissuedate.Text = "";
                txtissueplace.Text = "";
                txtReceiverName.Text = "";
                txtReceiverAdd.Text = "";
                txtBranchDesc.Text = "";

                txtReceiverAccount.Enabled = true;
                txtReceiverAccount.Enabled = true;
                txtCMND.Enabled = true;
                txtissuedate.Enabled = true;
                txtissueplace.Enabled = true;
                txtReceiverName.Enabled = true;
                txtReceiverAdd.Enabled = true;
                radAcctNo.Enabled = true;
                radCMND.Enabled = true;
                ddlProvince.Enabled = true;
                ddlBankRecieve.Enabled = true;
                ddlChildBank.Enabled = true;
                txtBranchDesc.Enabled = true;
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
    protected void ddlBankRecieve_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsBankList = new DataSet();
        dsBankList = new SmartPortal.IB.Account().GetBankList(ddlProvince.SelectedValue, ddlBankRecieve.SelectedValue);

        try
        {
            //DataRow[] dr = dsCity.Tables[0].Select(SmartPortal.Constant.IPC.CITYCODE + " = '" + ddlProvince.Text + "'");
            //if (dr.Length > 0)
            //{
            //DataRow[] drBank = dsBankList.Tables[0].Select(SmartPortal.Constant.IPC.CITYCODE + " = '" + dr[0][SmartPortal.Constant.IPC.CITYCODE] + "'");
            //foreach (DataRow row in drBank)
            //{
            //    temp.ImportRow(row);
            //}
            if (dsBankList.Tables[0].Rows.Count == 0)//|| temp.Columns[SmartPortal.Constant.IPC.BANKCODE].ColumnName.ToString() = null)
            {
                ddlChildBank.Items.Clear();
                ddlChildBank.Items.Insert(0, new ListItem("Chưa có danh sách ngân hàng", ""));
            }
            else
            {
                ddlChildBank.DataSource = dsBankList.Tables[0];
                ddlChildBank.DataTextField = dsBankList.Tables[0].Columns[SmartPortal.Constant.IPC.BANKNAME].ColumnName.ToString();
                ddlChildBank.DataValueField = dsBankList.Tables[0].Columns[SmartPortal.Constant.IPC.BANKCODE].ColumnName.ToString();
                ddlChildBank.DataBind();
            }

            //}
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
    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsBankList = new DataSet();
        dsBankList = new SmartPortal.IB.Account().GetBankList(ddlProvince.SelectedValue, ddlBankRecieve.SelectedValue);


        try
        {
            //DataRow[] dr = dsCity.Tables[0].Select(SmartPortal.Constant.IPC.CITYCODE + " = '" + ddlProvince.Text + "'");
            //if (dr.Length > 0)
            //{
            //DataRow[] drBank = dsBankList.Tables[0].Select(SmartPortal.Constant.IPC.CITYCODE + " = '" + dr[0][SmartPortal.Constant.IPC.CITYCODE] + "'");
            //foreach (DataRow row in drBank)
            //{
            //    temp.ImportRow(row);
            //}
            if (dsBankList.Tables[0].Rows.Count == 0)//|| temp.Columns[SmartPortal.Constant.IPC.BANKCODE].ColumnName.ToString() = null)
            {
                ddlChildBank.Items.Clear();
                ddlChildBank.Items.Insert(0, new ListItem("Chưa có danh sách ngân hàng", ""));
            }
            else
            {
                ddlChildBank.DataSource = dsBankList.Tables[0];
                ddlChildBank.DataTextField = dsBankList.Tables[0].Columns[SmartPortal.Constant.IPC.BANKNAME].ColumnName.ToString();
                ddlChildBank.DataValueField = dsBankList.Tables[0].Columns[SmartPortal.Constant.IPC.BANKCODE].ColumnName.ToString();
                ddlChildBank.DataBind();
            }

            //}
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
    protected void btnNextTOB_Click(object sender, EventArgs e)
    {
        try
        {

            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();



            #region Vi-Invi Panel
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
            pnBAC.Visible = false;
            pnTOB.Visible = false;

            lblConfirmScheduleName.Text = txtScheduleName.Text;
            lblEndSchedule.Text = txtScheduleName.Text;
            lblConfirmTransferType.Text = ddlTransferType.SelectedItem.Text;
            lblEndTransferType.Text = ddlTransferType.SelectedItem.Text;

            lblConfirmWeekyDayNo.Text = ddlWeekyDayNo.SelectedItem.Text;
            lblConfirmMonthDayNo.Text = ddlMonthlyDayNo.SelectedItem.Text;

            //an hien panel

            switch (radSchedule.SelectedValue)
            {
                case "D":
                    pnConfirmDaily.Visible = true;
                    pnConfirmMonthly.Visible = false;
                    pnConfirmWeekly.Visible = false;
                    pnConfirmOnetime.Visible = false;
                    break;
                case "W":
                    pnConfirmDaily.Visible = false;
                    pnConfirmMonthly.Visible = false;
                    pnConfirmWeekly.Visible = true;
                    pnConfirmOnetime.Visible = false;
                    break;
                case "M":
                    pnConfirmDaily.Visible = false;
                    pnConfirmMonthly.Visible = true;
                    pnConfirmWeekly.Visible = false;
                    pnConfirmOnetime.Visible = false;
                    break;
                case "O":
                    pnConfirmDaily.Visible = false;
                    pnConfirmMonthly.Visible = false;
                    pnConfirmWeekly.Visible = false;
                    pnConfirmOnetime.Visible = true;
                    break;
            }
            //phân biệt TIB-TOB-BAC
            pnBank.Visible = true;
            pnConFirmFee.Visible = true;
            # endregion

            string senderName = "";
            string balanceSender = "";
            string acctCCYID = "";
            string phi = "0";

            try
            {
                if (radAcctNo.Checked)
                {
                    pnTaiKhoanBaoCo.Visible = true;
                    pnConfirmCMND.Visible = false;
                }
                else
                {
                    pnTaiKhoanBaoCo.Visible = false;
                    // pnTaiKhoanBaoCo.Height = 0;
                    pnConfirmCMND.Visible = true;
                }

                //ktra ngân hàng
                if (ddlBankRecieve.SelectedValue == "")
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.BANKERROR);
                }

                //SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccountTOB.Text.Trim());
                if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    senderName = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                    balanceSender = SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                    acctCCYID = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
                    debitBrachID = hasSender[SmartPortal.Constant.IPC.BRANCHID].ToString();
                }
                else
                {
                    lblTextError.Text = "Error : " + hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }

                //tinh phi
                DataTable dtBranchID = new SmartPortal.IB.Bank().GetBranchID(ddlChildBank.SelectedValue);
                if (dtBranchID.Rows.Count != 0)
                {
                    DataTable dtFee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), "IB000206", SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ddlSenderAccount.SelectedValue, dtBranchID.Rows[0]["CREDITBRACHID"].ToString(), lblCurrency.Text, ddlProvince.SelectedValue.ToString());

                    if (dtFee.Rows.Count != 0)
                    {
                        phi = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["FEEAMOUNT"].ToString(), acctCCYID);
                    }
                }

                // CHECK AMOUNT
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount2.Text.Trim(), true) + SmartPortal.Common.Utilities.Utility.isDouble(phi, true) > SmartPortal.Common.Utilities.Utility.isDouble(balanceSender, true))
                {
                    lblTextError.Text = Resources.labels.amountinvalid;
                    return;
                }

                #region LOAD INFO


                lblLicense.Text = txtCMND.Text;
                lblIssuePlace.Text = txtissueplace.Text;
                lblIssueDate.Text = txtissuedate.Text;
                lbaccreceive.Text = txtReceiverAccount.Text;

                lbaccsent.Text = ddlSenderAccountTOB.Text;
                lbsender.Text = txtSenderName.Text;
                // lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender);
                lbccid.Text = acctCCYID;
                lblFeeCCYID.Text = acctCCYID;
                ccyid = acctCCYID;

                lbreceiver.Text = txtReceiverName.Text;
                lblConfirmReceiverAdd.Text = txtReceiverAdd.Text;

                //lblBank.Text = ddlBankRecieve.SelectedItem.Text;
                //lblProvince.Text = ddlProvince.SelectedItem.Text;
                lblConfirmChildBank.Text = ddlChildBank.SelectedItem.Text;

                //lblNation.Text = ddlNation.Text;

                lbamount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount2.Text, acctCCYID);
                lbdesc.Text = txtDesc2.Text;
                //lblPhi.Text = rdPhi.SelectedValue.ToString();
                //tinh phi                
                lblPhiAmount.Text = phi;


                lblFeeCCYID.Text = acctCCYID;



                #endregion

                lblTextError.Text = "";
                pnConfirm.Visible = true;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = false;
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
    private void LoadLoaiXacThuc()
    {
        try
        {
            ViewState["isSendFirst"] = 1;
            Button5.Enabled = false;
            SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
            DataTable dt = objTran.LoadAuthenType(Session["userID"].ToString());
            ddlLoaiXacThuc.DataSource = dt;
            ddlLoaiXacThuc.DataTextField = "TYPENAME";
            ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
            ddlLoaiXacThuc.DataBind();

            btnSendOTP.Text = Resources.labels.send;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlLoaiXacThuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button5.Enabled = false;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
    }
    //-----------------------------------------
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = string.Empty;
            ddlLoaiXacThuc.Enabled = false;
            Button5.Enabled = true;
            btnSendOTP.Text = Resources.labels.resend;
            if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
            {
                SmartPortal.SEMS.OTP.SendSMSOTP(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            }
            else
            {
                SmartPortal.SEMS.OTP.SendSMSOTPCORP(Session["userID"].ToString(), ddlLoaiXacThuc.SelectedValue.ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                lbnotice.Visible = true;
            }
            if (IPCERRORCODE != "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
            }
            switch (IPCERRORCODE)
            {
                case "0":
                    int time;
                    if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
                    {
                        time = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OTPtimeexpires"]) ? int.Parse(ConfigurationManager.AppSettings["OTPtimeexpires"].ToString()) : 20;
                    }
                    else
                    {
                        time = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OTPCorptimeexpires"]) ? int.Parse(ConfigurationManager.AppSettings["OTPCorptimeexpires"].ToString()) : 20;
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "countdownOTP(" + time + ");", true);
                    ViewState["timeReSendOTP"] = DateTime.Now.AddSeconds(time);
                    break;
                case "7003":
                    lblTextError.Text = Resources.labels.notregotp;
                    Button5.Enabled = false;
                    break;
                default:
                    lblTextError.Text = IPCERRORDESC;
                    Button5.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    //minh add 04.11.2015
    Boolean checktimeallow(string timeinput)
    {
        // minh add 04.11.2015
        DateTime configtimestart = DateTime.ParseExact(ConfigurationManager.AppSettings["SCHEDULETIMEFROM"], ConfigurationManager.AppSettings["shorttimeformat"], null);
        DateTime configtimeend = DateTime.ParseExact(ConfigurationManager.AppSettings["SCHEDULETIMETO"], ConfigurationManager.AppSettings["shorttimeformat"], null);
        DateTime inputtime = DateTime.ParseExact(timeinput, ConfigurationManager.AppSettings["shorttimeformat"], null);
        if (inputtime >= configtimestart && inputtime <= configtimeend)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private bool showdetailProcessMonthly(string date1, string date2, string time, string day, string feepayer, string fee)
    {
        try
        {
            #region 07/06/2016 minh add details process:
            lblTextError.Text = string.Empty;

            DateTime dt1 = DateTime.ParseExact(date1 + " " + time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(date2 + " " + time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            int i = (dt2.Year - dt1.Year) * 12 + dt2.Month - dt1.Month;
            if ((dt2.Year - dt1.Year) > int.Parse(ConfigurationManager.AppSettings["LimitYearforSchedule"]))
            {

                lblTextError.Text = string.Format(Resources.labels.thoigiantacdungcuaschedulebigioihan, ConfigurationManager.AppSettings["LimitYearforSchedule"].ToString());
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

            lblTextError.Text = "Date is invalid. Please try again";
            return false;
        }
        #endregion
    }
    private bool ValidateYearSchedule(string date1, string date2, string time, string day)
    {
        try
        {
            #region 07/06/2016 minh add details process:
            lblTextError.Text = string.Empty;

            DateTime dt1 = DateTime.ParseExact(date1 + " " + time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(date2 + " " + time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            int i = (dt2.Year - dt1.Year) * 12 + dt2.Month - dt1.Month;
            if ((dt2.Year - dt1.Year) > int.Parse(ConfigurationManager.AppSettings["LimitYearforSchedule"]))
            {

                lblTextError.Text = string.Format(Resources.labels.thoigiantacdungcuaschedulebigioihan, ConfigurationManager.AppSettings["LimitYearforSchedule"].ToString());
                return false;
            }

            return true;

        }
        catch
        {

            lblTextError.Text = "Date is invalid. Please try again";
            return false;
        }
        #endregion
    }
    private bool showdetailProcessMonthly2(string date1, string date2, string time, string day, string feepayer, string fee)
    {
        try
        {
            #region 07/06/2016 minh add details process:
            lblTextError.Text = string.Empty;

            DateTime dt1 = DateTime.ParseExact(date1 + " " + time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(date2 + " " + time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            int i = (dt2.Year - dt1.Year) * 12 + dt2.Month - dt1.Month;
            if ((dt2.Year - dt1.Year) > int.Parse(ConfigurationManager.AppSettings["LimitYearforSchedule"]))
            {

                lblTextError.Text = string.Format(Resources.labels.thoigiantacdungcuaschedulebigioihan, ConfigurationManager.AppSettings["LimitYearforSchedule"].ToString());
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
                lbrepeatmonthly2.Text = repeat.ToString();
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
                            cell3.BackColor = Color.LightBlue;
                            break;
                        }
                    }
                    row.Cells.Add(cell3);

                }

                tbdetailsmonth2.Rows.Add(row);

            }

            return true;

        }
        catch
        {

            lblTextError.Text = "Date is invalid. Please try again";
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

}
