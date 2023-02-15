using System;
using System.Linq;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using SmartPortal.IB;

using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBCreditPaymentOtherCard_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;
    string trancode = "IB000624";
    string cardtype = "OTH";
    string cardpaymentpage = "1049";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = string.Empty;
            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");

            load_unit(sender, e);
            if (radTS.Checked)
            {
                txtTS.Enabled = true;
            }
            else
            {
                txtTS.Enabled = false;
            }

            //hide panel
            if (!IsPostBack)
            {
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                LoadAccountInfo();
            }
        }
        catch
        {
        }
    }
    public void LayCCYID()
    {
        try
        {
            DataTable tblAcctnoInfo = new SmartPortal.IB.Account().GetAcctnoInfo(ddlSenderAccount.SelectedValue, Session["Userid"].ToString());
            if (tblAcctnoInfo.Rows.Count != 0)
            {
                lblCurrency.Text = tblAcctnoInfo.Rows[0]["CCYID"].ToString();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    private void load_unit(object sender, EventArgs e)
    {
        try
        {
            string errorcode = "";
            string errorDesc = "";
            DataSet ds = new DataSet();

            SmartPortal.IB.Account accountList = new SmartPortal.IB.Account();
            ds = accountList.getAccount(Session["userID"].ToString(), "IB000201", "", ref errorcode, ref errorDesc);
            //get card list
            DataSet dscard = new DataSet();
            dscard = new SmartPortal.IB.CreditCard().GetCardlistfromEbcore(Session["userID"].ToString(), cardtype, trancode, ref errorcode, ref errorDesc);



            if (ds.Tables[0].DefaultView.Count > 0)
            {

                if (!IsPostBack)
                {
                    ds.Tables[0].DefaultView.RowFilter = "accttype in ('DD', 'CD') and status = 'A'";
                    ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                    ddlSenderAccount.DataValueField = ds.Tables[0].Columns["ACCTNO"].ColumnName.ToString();
                    ddlSenderAccount.DataBind();



                    ////thaity modify at 26/6/2014 for permission
                    ////ds.Tables[0].DefaultView.RowFilter = "accttype in ('DD', 'CD') and status not in ('CLS','S','M','V')";
                    //ddlReceiverAccount.DataSource = ds.Tables[0].DefaultView;
                    //ddlReceiverAccount.DataValueField = ds.Tables[0].Columns["ACCTNO"].ColumnName.ToString();
                    //ddlReceiverAccount.DataBind();

                    //lay thong tin ccyid
                    //LayCCYID();

                    // lblCurrency.Text = ds.Tables[0].Rows[0]["CCYID"].ToString();
                    //Load Template của QuyenNPV

                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"] != null)
                    {
                        string TID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"].ToString();
                        DataSet TemplateDS = new DataSet();
                        TemplateDS = new SmartPortal.IB.Transfer().LoadtemplateByID(TID, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (TemplateDS.Tables[0].Rows.Count != 0)
                        {
                            DataTable TemplateTB = TemplateDS.Tables[0];
                            ddlSenderAccount.SelectedValue = TemplateTB.Rows[0]["SENDERACCOUNT"].ToString();
                            // ddlReceiverAccount.SelectedValue = TemplateTB.Rows[0]["RECEIVERACCOUNT"].ToString();
                            txtAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(TemplateTB.Rows[0]["AMOUNT"].ToString(), TemplateTB.Rows[0]["CCYID"].ToString());
                            lblCurrency.Text = TemplateTB.Rows[0]["CCYID"].ToString();
                            txtDesc.Text = TemplateTB.Rows[0]["DESCRIPTION"].ToString();
                        }
                    }

                    if (dscard.Tables[0].DefaultView.Count.Equals(0))
                    {
                        btnTIBNext.Visible = false;
                        lblTextError.Text = Resources.labels.usernotregisterpaymentothercard;
                        pnTIB.Visible = false;
                    }
                    else
                    {
                        //minh add this:
                        //mask all card
                        foreach (DataRow r in dscard.Tables[0].Rows)
                        {
                            r["cardmask"] = SmartPortal.Common.Utilities.Utility.MaskDigits(r["cardmask"].ToString());
                        }
                        ddlcreditcardno.DataSource = dscard.Tables[0].DefaultView;
                        ddlcreditcardno.DataValueField = dscard.Tables[0].Columns["CardNo"].ColumnName.ToString();
                        ddlcreditcardno.DataTextField = dscard.Tables[0].Columns["cardmask"].ColumnName.ToString();

                        ddlcreditcardno.DataBind();
                        OnReceiverAccountChanged(sender, e);
                    }








                }
            }
            else
            {
                throw new BusinessExeption("User not register credit card.");
            }

        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], Request.Url.Query);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
    protected void OnReceiverAccountChanged(object sender, EventArgs e)
    {
        try
        {//mask cardno
         // ddlcreditcardno.SelectedItem.Text = SmartPortal.Common.Utilities.Utility.MaskDigits(ddlcreditcardno.SelectedValue.ToString());
         //ddlcreditcardno. = SmartPortal.Common.Utilities.Utility.MaskDigits(dscard.Tables[0].Columns["CardNo"].ColumnName.ToString());
         //get card info
            string messageid = "abc";
            string hostid = "111";
            string cardno = ddlcreditcardno.SelectedValue.ToString();
            Hashtable dscardinfo = new Hashtable();
            string errorcode = string.Empty;
            string errorDesc = string.Empty;
            //get cifno:
            DataTable dtcarddetail = new DataTable();
            dtcarddetail = new SmartPortal.IB.CreditCard().CR_GetCarddetail(Session["userid"].ToString(), cardno);
            if (dtcarddetail.Rows.Count > 0)
            {
                hfcif.Value = dtcarddetail.Rows[0]["HolderCFCode"].ToString();
                hfcurid.Value = dtcarddetail.Rows[0]["curid"].ToString();
                hfccyid.Value = dtcarddetail.Rows[0]["CCYID"].ToString();

                //lblAvailableBalCCYID.Text = hfccyid.Value;
                lblAvailableBalCCYIDr.Text = hfccyid.Value;
                lblAmountCCYID.Text = hfccyid.Value;
                lbloutstandingconfirmCCYID.Text = hfccyid.Value;
                lbloutstandingrestCCYID.Text = hfccyid.Value;
            }


            dscardinfo = new SmartPortal.IB.CreditCard().GetCardInfo(messageid, hostid, cardno, ref errorcode, ref errorDesc);
            if (errorcode.Equals("0"))
            {

                double am = double.Parse(Utility.FormatMoney(dscardinfo["outstandingAmt"].ToString(), ""));
                txtAmount.Text = am.ToString();
                //txtAmount.Text = dscardinfo["outstandingAmt"].ToString();
                lblcardholdername.Text = dscardinfo["cardholderName"].ToString();
                lbloutstanding.Text = Utility.FormatMoney(dscardinfo["outstandingAmt"].ToString(), "");
                // lblAvailableBalCCYIDr.Text = lblCurrency.Text;
                lblcardholderconfirm.Text = lblcardholdername.Text;
                lbloutstandingconfirm.Text = lbloutstanding.Text;
                lblText.Text = string.Format("({0})", SmartPortal.Common.Utilities.Utility.NumtoWords(double.Parse(txtAmount.Text)));



            }

            //hdflimit.Value = "";
            ////CHECK LIMIT CREDIT BRANCH
            //DataTable dt = new SmartPortal.SEMS.Transactions().GetLimitConfig(Session["UserID"].ToString(), ddlSenderAccount.Text, ddlReceiverAccount.Text, "IB000201", lblCurrency.Text.Trim(), "DEB");
            //if (dt.Rows.Count != 0)
            //{
            //    string TranLimit = dt.Rows[0]["TranLimit"].ToString();
            //    string TotalLimit = dt.Rows[0]["TotalLimitDay"].ToString();
            //    string CountLimit = dt.Rows[0]["CountLimit"].ToString();
            //    string CCYID = dt.Rows[0]["CCYID"].ToString();
            //    string BranchName = dt.Rows[0]["BranchName"].ToString();
            //    string Msg = string.Format("Remittance to {0} is limited to {1} {2} per day. Sorry for inconvenienced cause. \r\n\r\n Do you want continue ?", BranchName, SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(TotalLimit, CCYID), CCYID);
            //    hdflimit.Value = Msg;
            //}

            //LoadAccountInforeceiver();
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        string senderName = "";
        string receiverName = "";
        string balanceSender = "";
        string balanceReceiver = "";
        string acctCCYID = "";
        string receiverCCYID = "";

        try
        {
            // if ((cbmau.Checked == true && txttenmau.Text.ToString().Trim() != "") || (cbmau.Checked == false))
            {
                //validate amount input:
                if (double.Parse(txtAmount.Text) < 0)
                {
                    lblTextError.Text = Resources.labels.khongthethanhtoanvoisoam;
                    return;
                }




                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.Text.Trim());
                if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    senderName = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                    balanceSender = SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                    acctCCYID = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
                    lblSenderBranch.Text = hasSender[SmartPortal.Constant.IPC.BRANCHID].ToString();
                }
                else
                {
                    lblTextError.Text = "Error : " + hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }
                // CHECK SAME ACCOUNT
                //bool sameAcct = objAcct.CheckSameAccount(ddlSenderAccount.Text.ToString(), ddlReceiverAccount.Text.ToString());
                //if (!sameAcct)
                //{
                //    lblTextError.Text = Resources.labels.Accountnotsame;
                //    return;
                //}
                // CHECK AMOUNT
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(balanceSender, true))
                {
                    lblTextError.Text = Resources.labels.amountinvalid;
                    return;
                }
                //Hashtable hasReceiver = objAcct.loadInfobyAcct(ddlReceiverAccount.Text.Trim());
                //if (hasReceiver[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                //{
                //    receiverName = hasReceiver[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                //    balanceReceiver = SmartPortal.Common.Utilities.Utility.FormatStringCore(hasReceiver[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                //    receiverCCYID = hasReceiver[SmartPortal.Constant.IPC.CCYID].ToString();
                //    lblReceiverBranch.Text = hasReceiver[SmartPortal.Constant.IPC.BRANCHID].ToString();
                //}
                //else
                //{
                //    lblTextError.Text = "Error : " + hasReceiver[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                //    return;
                //}
                // CHECK SAME CCYCD
                //if (System.Configuration.ConfigurationManager.AppSettings["AllowTransferMultiCurrency"].ToString().Equals("0"))
                //{
                //    bool sameCCYCD = objAcct.CheckSameCCYCD(acctCCYID, receiverCCYID);
                //    if (!sameCCYCD)
                //    {
                //        lblTextError.Text = Resources.labels.invalidacctCCYCDtotransfer;
                //        return;
                //    }
                //}

                #region LOAD INFO
                lblSenderAccount.Text = ddlSenderAccount.Text;
                lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender, acctCCYID);
                lblSenderCCYID.Text = acctCCYID;
                lblSenderName.Text = senderName;

                lblReceiverAccount.Text = ddlcreditcardno.SelectedItem.Text;
                //lblBalanceReceiver.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceReceiver, receiverCCYID);
                //lblReceiverCCYID.Text = receiverCCYID;
                //lblReceiverName.Text = receiverName;

                lblAmountCCYID.Text = acctCCYID;
                lblFCCYID.Text = acctCCYID;

                if (radTS1.Checked == true)
                {
                    //lblHinhThuc.Text = radTS1.Text;
                }
                else
                {
                    string scheduleTime = txtTS.Text.ToString() + " " + ConfigurationManager.AppSettings.Get("SCHEDULETIME");
                    if (Convert.ToDateTime(scheduleTime) < DateTime.Now)
                    {
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.BE);
                    }
                    //lblHinhThuc.Text = txtTS.Text.Trim().ToString();

                }



                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), acctCCYID);
                //lblPhi.Text = rdNguoiChiuPhi.SelectedValue;

                #region tinh phi
                //edit by VuTran 19/09/2014: tinh lai phi
                string senderfee = "0";
                string receiverfee = "0";
                DataTable dtFee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), trancode, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ddlSenderAccount.SelectedValue, lblReceiverBranch.Text.Trim(), lblCurrency.Text, "");

                if (dtFee.Rows.Count != 0)
                {
                    senderfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeSenderAmt"].ToString(), acctCCYID);
                    receiverfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeReceiverAmt"].ToString(), acctCCYID);
                }
                #endregion
                lblPhiAmount.Text = (Double.Parse(receiverfee) != 0) ? receiverfee : senderfee;
                lblPhi.Text = (Double.Parse(receiverfee) != 0) ? "Receiver" : "Sender";

                lblDesc.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text);
                lblTextError.Text = "";
                pnConfirm.Visible = true;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = false;
                #endregion



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
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            //if (int.Parse(Session["userLevel"].ToString().Trim()) > 2 && Session["TypeID"].ToString().Equals(""))
            if (int.Parse(Session["userLevel"].ToString().Trim()) > 5)
            {
                btnApply.Text = Resources.labels.transfer;
                //chuyen khoan luon doi voi user doanh nghiep level3 tro len
                btnAction_Click(sender, e);
            }
            else
            {
                btnApply.Text = Resources.labels.confirm;

                pnConfirm.Visible = false;
                pnOTP.Visible = true;
                pnTIB.Visible = false;

                SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
                DataTable dt = new DataTable();
                dt = objTran.LoadAuthenType(Session["userID"].ToString());
                ddlLoaiXacThuc.DataSource = dt;
                ddlLoaiXacThuc.DataTextField = "TYPENAME";
                ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
                ddlLoaiXacThuc.DataBind();
            }
            //edit by vutran 18/08/2014: sua loi ko hien nut send neu chi co SMSOTP
            if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
            {
                btnSendOTP.Visible = true;
            }
            else
            {
                btnSendOTP.Visible = false;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBTransferInBAC1_Widget", "btnApply_Click", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferInBAC1_Widget", "btnApply_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private Object m_lock = new Object();
    protected void btnAction_Click(object sender, EventArgs e)
    {

        Hashtable result = new Hashtable();
        string OTPcode = txtOTP.Text;
        txtOTP.Text = "";
        //card parm


        string DEBITACCTNO = ddlSenderAccount.SelectedItem.Text;

        string AMOUNT = txtAmount.Text;
        int BRANCHID = 2;
        string DESC = txtDesc.Text.Trim();
        int TRANREF = 43434343;
        string cardNo = ddlcreditcardno.SelectedValue;
        string cifNo = hfcif.Value;




        try
        {
            lock (m_lock)
            {
                if (radTS1.Checked)
                {



                    //result = new SmartPortal.IB.CreditCard().FinanceAdj(trancode, lblAvailableBalCCYID.Text, Session["userID"].ToString(),DEBITACCTNO,  AMOUNT, BRANCHID,
                    //    DESC, TRANREF, cardNo, cifNo,  ddlLoaiXacThuc.SelectedValue, OTPcode, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblPhiAmount.Text.Trim(), lblCurrency.Text.Trim()), lblPhi.Text,lblcardholdername.Text);
                    result = new SmartPortal.IB.CreditCard().FinanceAdj(trancode, lblAvailableBalCCYID.Text, Session["userID"].ToString(), DEBITACCTNO, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(AMOUNT, lblCurrency.Text.Trim()), BRANCHID,
                                     DESC, TRANREF, cardNo, cifNo, ddlLoaiXacThuc.SelectedValue, OTPcode, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblPhiAmount.Text.Trim(), lblCurrency.Text.Trim()), lblPhi.Text, lblcardholdername.Text, lblSenderName.Text);


                    //SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                    //result = objAcct.TransferDDSameCust(Session["userID"].ToString(), lblSenderAccount.Text, lblReceiverAccount.Text, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()), lblSenderCCYID.Text, lblDesc.Text
                    //    , lblSenderName.Text, "", lblSenderBranch.Text, lblReceiverBranch.Text, ddlLoaiXacThuc.SelectedValue, OTPcode, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblPhiAmount.Text.Trim(), lblCurrency.Text.Trim()), "");
                }
                else
                {
                    SmartPortal.IB.Schedule objSchedule = new SmartPortal.IB.Schedule();
                    string scheduleID = SmartPortal.Constant.IPC.SCHEDULEPREFIX + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
                    object[] ipcSchedules = objSchedule.createObjSchedules(scheduleID, "ONETIME", " " + ConfigurationManager.AppSettings.Get("SCHEDULETIME"),
                                                                        "RUN_SCHEDULE", lblDesc.Text.ToString(), "A", Session["userID"].ToString(),
                                                                        Session["userID"].ToString(), "Y", "IB", "", "IB000201", "",
                                                                        "", "");

                    DataTable dtInfoTran = new DataTable();
                    dtInfoTran.Columns.Add("SCHEDULEID");
                    dtInfoTran.Columns.Add("PARANAME");
                    dtInfoTran.Columns.Add("PARAVALUE");
                    dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.SOURCEID, "IB");
                    dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.USERID, Session["userID"].ToString());
                    dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.ACCTNO, lblSenderAccount.Text);
                    dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.RECEIVERACCOUNT, lblReceiverAccount.Text);
                    dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.AMOUNT, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblAmount.Text, lblSenderCCYID.Text));
                    dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.CCYID, lblSenderCCYID.Text);
                    dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.DESC, lblDesc.Text);
                    object[] ipcScheduleDetails = objSchedule.createObjScheduleDetails(dtInfoTran);

                    result = objSchedule.InsertSchedule(ipcSchedules, ipcScheduleDetails);


                }

                if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    lblTextError.Text = Resources.labels.transactionsuccessful;

                    if (result[SmartPortal.Constant.IPC.respCode].ToString().Equals("00"))
                    {

                        //ghi vo session dung in

                        Hashtable hasPrint = new Hashtable();
                        hasPrint.Add("status", Resources.labels.thanhcong);
                        hasPrint.Add("senderAccount", lblSenderAccount.Text);
                        hasPrint.Add("senderBalance", lblBalanceSender.Text);
                        hasPrint.Add("ccyid", lblSenderCCYID.Text);
                        hasPrint.Add("senderName", lblSenderName.Text);
                        hasPrint.Add("cardNo", lblReceiverAccount.Text);
                        hasPrint.Add("cardholdername", lblcardholderconfirm.Text);
                        hasPrint.Add("outstandingamount", lbloutstandingconfirm.Text + " " + lblAmountCCYID.Text);
                        hasPrint.Add("transferType", "");
                        hasPrint.Add("amount", lblAmount.Text);
                        // hasPrint.Add("amountchu", txtChu.Value.ToString());
                        hasPrint.Add("amountchu", string.Format("{0} {1}", SmartPortal.Common.Utilities.Utility.NumtoWords(double.Parse(txtAmount.Text.Trim())), lblCurrency.Text));
                        //hasPrint.Add("feeType", lblPhi.Text);
                        hasPrint.Add("feeAmount", lblPhiAmount.Text);
                        hasPrint.Add("desc", lblDesc.Text);
                        hasPrint.Add("tranID", result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : "");
                        hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        hasPrint.Add("senderBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblSenderBranch.Text));
                        //hasPrint.Add("receiverBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblReceiverBranch.Text));
                        Session["print"] = hasPrint;

                        btnPrint.Visible = true;
                        btnView.Visible = true;

                        //send mail edit by VuTran
                        try
                        {
                            SmartPortal.Common.EmailHelper.CR_TransactionSuccess_SendMail(hasPrint, Session["userID"].ToString());
                        }
                        catch (Exception ex)
                        {
                            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + "Error when send email from TranserInBAC1", Request.Url.Query);
                            //SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                            lblTextError.Text = "Transfer success but can't send mail!";
                        }
                    }//CARDZONE RESPOND SUCCESS
                    else
                    {
                        lblTextError.Text = result[SmartPortal.Constant.IPC.respDetail].ToString();
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "Error when do transaction finaladj to cardzone ws .errordetail=" + result[SmartPortal.Constant.IPC.respDetail].ToString(), Request.Url.Query);
                    }


                }
                else
                {
                    txtOTP.Text = "";
                    btnPrint.Visible = false;
                    btnView.Visible = false;
                    switch (result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblTextError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                            lblTextError.Text = Resources.labels.wattingbankapprove;

                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGPARTOWNERAPPROVE:
                            lblTextError.Text = Resources.labels.wattingpartownerapprove;

                            break;


                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                            lblTextError.Text = Resources.labels.wattinguserapprove;

                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                            lblTextError.Text = Resources.labels.notregotp;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblTextError.Text = Resources.labels.authentypeinvalid;
                            return;
                        case "9908":
                            lblTextError.Text = Resources.labels.sotienvuothanmucgiaodich;
                            return;
                        case "9909":
                            lblTextError.Text = Resources.labels.sotienvuotquatonghanmuctrongngay;
                            return;
                        case "9907":
                            lblTextError.Text = Resources.labels.solangiaodichvuotquasolangiaodichtrongngay;
                            return;
                        case "9906":
                            lblTextError.Text = Resources.labels.sotienquahanmuccuachinhanhghico;
                            return;
                        case "9905":
                            lblTextError.Text = Resources.labels.tongsotienvuotquahanmuccuachinhanhghico;
                            return;
                        case "9904":
                            lblTextError.Text = Resources.labels.sogiaodichtrongngayquahanmuccuachinhanhghico;
                            return;
                        case "-13524":
                            lblTextError.Text = Resources.labels.destacccountinvalid;
                            return;
                        default:
                            throw new Exception();
                    }

                    pnConfirm.Visible = true;
                    btnApply.Enabled = false;
                    btnBackTransfer.Enabled = false;

                }

                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = true;



                #region LOAD RESULT TRANSFER
                string errorCode = "0";
                string errorDesc = string.Empty;
                SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
                DataSet dsDetailAcc = new DataSet();
                dsDetailAcc = acct.GetInfoDD(Session["userID"].ToString(), lblSenderAccount.Text.Trim(), ref errorCode, ref errorDesc);

                if (result["IPCTRANSID"] != null)
                {
                    lblEndTransactionNo.Visible = true;
                    lblEndTransactionNo.Text = result["IPCTRANSID"].ToString();
                }
                else
                {
                    lblEndTransactionNo.Visible = false;
                }
                lblEndDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                lblendSenderAccount.Text = lblSenderAccount.Text;
                lblEndBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), lblSenderCCYID.Text.Trim());
                lblEndSenderCCYID.Text = lblSenderCCYID.Text;
                lblEndSenderName.Text = lblSenderName.Text;
                //get card info after transfer:
                //get card info


                Hashtable dscardinfo = new Hashtable();
                string errorcode = string.Empty;
                string errordesc = string.Empty;
                string messageid = string.Empty;
                string hostid = string.Empty;
                dscardinfo = new SmartPortal.IB.CreditCard().GetCardInfo(messageid, hostid, cardNo, ref errorcode, ref errorDesc);
                if (errorcode.Equals("0"))
                {
                    float am = float.Parse(Utility.FormatMoney(dscardinfo["outstandingAmt"].ToString(), ""));
                    //  txtAmount.Text = ((int)am).ToString();
                    //txtAmount.Text = dscardinfo["outstandingAmt"].ToString();
                    lblcardholdernameres.Text = lblcardholdername.Text; // dscardinfo["cardholderName"].ToString();
                    lbloutstandingamtres.Text = dscardinfo["outstandingAmt"].ToString();
                    lblEndReceiverAccount.Text = lblReceiverAccount.Text;

                }


                //dsDetailAcc = acct.GetInfoDD(Session["userID"].ToString(), lblReceiverAccount.Text.Trim(), ref errorCode, ref errorDesc);

                //lblEndReceiverAccount.Text = lblReceiverAccount.Text;
                ////lblEndReceiverName.Text = lblReceiverName.Text;
                //lblEndBalanceReceiver.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), lblSenderCCYID.Text.Trim());
                //lblEndReceiverCCYID.Text = lblReceiverCCYID.Text;

                //lblFeeCCYID.Text = lblReceiverCCYID.Text;
                lblEndAmountCCYID.Text = lblAmountCCYID.Text;
                //lblEndHinhThuc.Text = lblHinhThuc.Text;
                lblEndAmount.Text = lblAmount.Text;
                //lblEndPhi.Text = lblPhi.Text;
                lblEndPhiAmount.Text = lblPhiAmount.Text;
                lblEndDesc.Text = lblDesc.Text;
                #endregion

                txtOTP.Text = "";
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
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
    protected void btnBackTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTIB.Visible = true;
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

    protected void Button6_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=" + cardpaymentpage));
    }
    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountInfo();
    }
    //edit by vutran 06082014: send SMSOTP
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        string errorcode = "";
        string errorDesc = "";
        try
        {

            btnSendOTP.Text = "ReSend";
            SmartPortal.SEMS.OTP.SendSMSOTP(Session["userID"].ToString(), ref errorcode, ref errorDesc);
            switch (errorcode)
            {
                case "0":
                    lblTextError.Text = "Send SMS OTP success."; btnAction.Enabled = true;
                    break;
                case "7003":
                    lblTextError.Text = "User does not register SMS OTP"; btnAction.Enabled = false;
                    break;
                default:
                    lblTextError.Text = errorDesc; btnAction.Enabled = false;
                    break;
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
    protected void DropDownListOTP_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
            {
                btnSendOTP.Visible = true;
                if (btnSendOTP.Text == "ReSend")
                {
                    btnAction.Enabled = true;
                }
                else
                {
                    btnAction.Enabled = false;
                }
            }
            else
            {
                btnAction.Enabled = true;
                btnSendOTP.Visible = false;
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

            string account = ddlSenderAccount.SelectedItem.Text.ToString();
            string Acctype = ddlSenderAccount.SelectedItem.Value.ToString();
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string User = Session["userID"].ToString();
            Account acct = new Account();
            DataSet ds = new DataSet();
            ds = acct.GetInfoDD(User, account, ref ErrorCode, ref ErrorDesc);
            ShowDD(account, ds);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferBAC1_Widget", "LoadAccountInfo", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }


    private void ShowDD(string acctno, DataSet ds)
    {
        try
        {
            if (ds.Tables[0].Rows.Count == 1)
            {
                lblLastTranDate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.LASTTRANSDATE].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
                lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
                lblAvailableBalCCYID.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                lblCurrency.Text = lblAvailableBalCCYID.Text;
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
    private void LoadAccountInforeceiver()
    {
        try
        {

            //string account = ddlReceiverAccount.SelectedItem.Text.ToString();
            //string ErrorCode = string.Empty;
            //string ErrorDesc = string.Empty;
            //string User = Session["userID"].ToString();
            //Account acct = new Account();
            //DataSet ds = new DataSet();

            //ds = acct.GetInfoDD(User, account, ref ErrorCode, ref ErrorDesc);
            //ShowDDreceiver(account, ds);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferBAC1_Widget", "LoadAccountInforeceiver", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void ShowDDreceiver(string acctno, DataSet ds)
    {
        try
        {
            if (ds.Tables[0].Rows.Count == 1)
            {
                lblLastTranDater.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.LASTTRANSDATE].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
                //lblAvailableBalr.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
                //lblAvailableBalCCYIDr.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
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

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            //15.12.2015 minh add to fix error 9999 
            if (Session["print"] == null)
            {
                Response.Redirect("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]);
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            //15.12.2015 minh add to fix error 9999 
            if (Session["print"] == null)
            {
                Response.Redirect("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]);
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
