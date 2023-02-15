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
using System.Diagnostics;

public partial class Widgets_IBTransferInBAC1_Widget : WidgetBase
{
    private Stopwatch sw = null;
    string IPCERRORCODE;
    string IPCERRORDESC;
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : Utility.createTableDocumnet(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";

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
                if ((Session["accType"].ToString() != "IND"))
                {
                    LblDocument.Visible = true;
                    FUDocument.Visible = true;
                    LblDocumentExpalainion.Visible = true;
                }
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

            if (!IsPostBack)
            {
                SmartPortal.IB.Account accountList = new SmartPortal.IB.Account();
                ds = accountList.GetListOfAccounts(Session["userID"].ToString(), "IB000201", "IBCASHOUT", "DD,CD,WL", "", ref errorcode, ref errorDesc);
                if (ds.Tables[0].DefaultView.Count > 0)
                {
                    // CHECK PERMISSION SENDER
                    ds.Tables[0].DefaultView.RowFilter = "TYPEID IN ('DD', 'CD','WLM') AND STATUSCD in ('A','Y')";
                    ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                    ddlSenderAccount.DataValueField = ds.Tables[0].Columns["UNIQUEID"].ColumnName.ToString();
                    ddlSenderAccount.DataTextField = ds.Tables[0].Columns["ACCOUNTNO"].ColumnName.ToString();
                    ddlSenderAccount.DataBind();
                    // CHECK PERMISSION RECEVIER
                    ds = accountList.GetListOfAccounts(Session["userID"].ToString(), "IB000201", "IBCASHOUT", "DD,CD,WL", "", ref errorcode, ref errorDesc);
                    ds.Tables[0].DefaultView.RowFilter = "TYPEID IN ('DD', 'CD','WLM') AND STATUSCD in ('A','Y')";
                    ddlReceiverAccount.DataSource = ds.Tables[0].DefaultView;
                    ddlReceiverAccount.DataValueField = ds.Tables[0].Columns["UNIQUEID"].ColumnName.ToString();
                    ddlReceiverAccount.DataTextField = ds.Tables[0].Columns["ACCOUNTNO"].ColumnName.ToString();
                    ddlReceiverAccount.DataBind();

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
                            ddlReceiverAccount.SelectedValue = TemplateTB.Rows[0]["RECEIVERACCOUNT"].ToString();
                            txtAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(TemplateTB.Rows[0]["AMOUNT"].ToString(), TemplateTB.Rows[0]["CCYID"].ToString());
                            lblCurrency.Text = TemplateTB.Rows[0]["CCYID"].ToString();
                            txtDesc.Text = TemplateTB.Rows[0]["DESCRIPTION"].ToString();
                        }
                    }

                    OnReceiverAccountChanged(sender, e);
                }
                else
                {
                    throw new BusinessExeption("User not register DD Account To Transfer.");
                }
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
        {
            hdflimit.Value = "";
            //CHECK LIMIT CREDIT BRANCH
            DataTable dt = new SmartPortal.SEMS.Transactions().GetLimitConfig(Session["UserID"].ToString(), ddlSenderAccount.Text, ddlReceiverAccount.Text, "IB000201", lblCurrency.Text.Trim(), "DEB");
            if (dt.Rows.Count != 0)
            {
                string TranLimit = dt.Rows[0]["TranLimit"].ToString();
                string TotalLimit = dt.Rows[0]["TotalLimit"].ToString();
                string CountLimit = dt.Rows[0]["CountLimit"].ToString();
                string CCYID = dt.Rows[0]["CCYID"].ToString();
                string BranchName = dt.Rows[0]["BRANCHNAME"].ToString();
                string Msg = string.Format("Remittance to {0} is limited to {1} {2} per day. Sorry for inconvenienced cause. \r\n\r\n Do you want continue ?", BranchName, SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(TotalLimit, CCYID), CCYID);
                hdflimit.Value = Msg;
            }

            LoadAccountInforeceiver();
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
        //string senderName = "";
        //string receiverName = "";
        //string balanceSender = "";
        //string balanceReceiver = "";
        //string acctCCYID = "";
        //string receiverCCYID = "";

        try
        {
            string trancode = string.Empty;
            if ((cbmau.Checked == true && txttenmau.Text.ToString().Trim() != "") || (cbmau.Checked == false))
            {
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                bool sameAcct = objAcct.CheckSameAccount(ddlSenderAccount.Text.ToString(), ddlReceiverAccount.Text.ToString());
                if (!sameAcct)
                {
                    lblTextError.Text = Resources.labels.Accountnotsame;
                    return;
                }
                // CHECK AMOUNT
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(hdBalanceSender.Value, true))
                {
                    lblTextError.Text = Resources.labels.amountinvalid;
                    return;
                }
                //Hashtable hasReceiver = objAcct.loadInfobyAcct(ddlReceiverAccount.Text.Trim());
                //if (hasReceiver[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                //{
                //    receiverName = hasReceiver[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                //    balanceReceiver = SmartPortal.Common.Utilities.Utility.FormatStringCore(hasReceiver[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                //    receiverCCYID = hasReceiver["CURRENCYID"].ToString();
                //    lblReceiverBranch.Text = hasReceiver[SmartPortal.Constant.IPC.BRID].ToString();
                //}
                //else
                //{
                //    lblTextError.Text = "Error : " + hasReceiver[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                //    return;
                //}
                // CHECK SAME CCYCD
                if (System.Configuration.ConfigurationManager.AppSettings["AllowTransferMultiCurrency"].ToString().Equals("0"))
                {
                    bool sameCCYCD = objAcct.CheckSameCCYCD(hdSenderCCYID.Value, hdReceiverCCYID.Value);
                    if (!sameCCYCD)
                    {
                        lblTextError.Text = Resources.labels.invalidacctCCYCDtotransfer;
                        return;
                    }
                }
                if (hdActTypeSender.Value.Equals("WLM"))
                {
                    trancode = "IBCASHOUT";
                }
                else if (hdActTypeReceiver.Value.Equals("WLM"))
                {
                    trancode = "IBCASHIN";
                }
                else
                {
                    trancode = "IB000201";
                }
                hdTrancode.Value = trancode;
                Hashtable HTCheckAmount = new Account().CheckAmountPayment(Session["userID"].ToString(),
                trancode, ddlSenderAccount.SelectedValue, ddlReceiverAccount.SelectedValue, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ref IPCERRORCODE, ref IPCERRORDESC);
                if (!IPCERRORCODE.ToString().Equals("0"))
                {
                    lblTextError.Text = IPCERRORDESC;
                    return;
                }
                #region LOAD INFO
                lblSenderAccount.Text = ddlSenderAccount.SelectedItem.ToString();
                lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(hdBalanceSender.Value, hdSenderCCYID.Value);
                lblSenderCCYID.Text = hdSenderCCYID.Value;
                lblSenderName.Text = hdSenderName.Value;

                lblReceiverAccount.Text = ddlReceiverAccount.SelectedItem.ToString();
                lblBalanceReceiver.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(hdBalanceReceiver.Value, hdReceiverCCYID.Value);
                lblReceiverCCYID.Text = hdReceiverCCYID.Value;
                lblReceiverName.Text = hdReceiverName.Value;

                lblAmountCCYID.Text = hdSenderCCYID.Value;
                lblAmountCCYID.Text = hdSenderCCYID.Value;
                lblFCCYID.Text = hdSenderCCYID.Value;

                if (Session["accType"].ToString() != "IND")
                {
                    DataTable dt = Utility.UploadFile(FUDocument, lblTextError);
                        ViewState["TBLDOCUMENT"] = dt;
           
                }

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

                #region check same name template transfer
                if (cbmau.Checked == true && txttenmau.Text.ToString().Trim() != "")
                {
                    new SmartPortal.IB.Transfer().CheckNameTransferTemplate(Utility.KillSqlInjection(txttenmau.Text.Trim()), Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {

                    }
                    else
                    {
                        if (IPCERRORDESC == "110211")
                        {
                            lblTextError.Text = Resources.labels.samenamewithadifferenttemplatetranfer;
                            return;
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                        }
                    }
                }
                #endregion

                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), hdSenderCCYID.Value);
                lblPhi.Text = rdNguoiChiuPhi.SelectedValue;

                #region tinh phi
                //edit by VuTran 19/09/2014: tinh lai phi
                string senderfee = "0";
                string receiverfee = "0";
                string payer = "0";
                DataTable dtFee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), hdTrancode.Value, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ddlSenderAccount.SelectedValue, lblReceiverBranch.Text.Trim(), lblCurrency.Text, "");

                if (dtFee.Rows.Count != 0)
                {
                    senderfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeSenderAmt"].ToString(), hdSenderCCYID.Value);
                    receiverfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeReceiverAmt"].ToString(), hdSenderCCYID.Value);
                    //payer = dtFee.Rows[0]["Payer"].ToString().Trim();
                }
                #endregion
                lblPhiAmount.Text = payer.Equals("0") ? senderfee : receiverfee;
                lblPhi.Text = payer.Equals("0") ? Resources.labels.nguoigui : Resources.labels.nguoinhan;


                lblDesc.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text);
                lblTextError.Text = "";
                pnConfirm.Visible = true;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = false;
                #endregion
            }
            else
            {
                lblTextError.Text = "Template name can not be empty";
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
            // check amout
            if (SmartPortal.Common.Utilities.Utility.isDouble(hdBalanceSender.Value, true) < (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) + SmartPortal.Common.Utilities.Utility.isDouble(lblPhiAmount.Text, true)))
            {
                lblTextError.Text = Resources.labels.amountinvalid;
                return;
            }
            if (ViewState["isSendFirst"] == null || (int)ViewState["isSendFirst"] == 0)
            {
                LoadLoaiXacThuc();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
            }
            pnTIB.Visible = false;
            pnConfirm.Visible = false;
            pnOTP.Visible = true;
            pnResultTransaction.Visible = false;
        }
        catch (Exception ex)
        {
            lblTextError.Text = ex.ToString();
            //SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            //SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            txtOTP.Text = "";

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
    private Object m_lock = new Object();
    protected void btnAction_Click(object sender, EventArgs e)
    {

        Hashtable result = new Hashtable();
        string OTPcode = txtOTP.Text;
        txtOTP.Text = "";
        try
        {
            lock (m_lock)
            {
                if (radTS1.Checked)
                {
                    DataTable tbldocument = (DataTable)ViewState["TBLDOCUMENT"];
                    SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                    result = objAcct.TransferDDSameCust(Session["userID"].ToString(), hdTrancode.Value, lblSenderAccount.Text, ddlReceiverAccount.Text, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()), lblSenderCCYID.Text, lblDesc.Text
                        , lblSenderName.Text, lblReceiverName.Text, lblSenderBranch.Text, lblReceiverBranch.Text, ddlLoaiXacThuc.SelectedValue, OTPcode, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblPhiAmount.Text.Trim(), lblCurrency.Text.Trim()), lblPhi.Text, tbldocument, Session["accType"].ToString());
                }
                else
                {
                    SmartPortal.IB.Schedule objSchedule = new SmartPortal.IB.Schedule();
                    string scheduleID = SmartPortal.Constant.IPC.SCHEDULEPREFIX + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
                    object[] ipcSchedules = objSchedule.createObjSchedules(scheduleID, "ONETIME", " " + ConfigurationManager.AppSettings.Get("SCHEDULETIME"),
                                                                        "RUN_SCHEDULE", lblDesc.Text.ToString(), "A", Session["userID"].ToString(),
                                                                        Session["userID"].ToString(), "Y", "IB", "", hdTrancode.Value, "",
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
                    Label58.Text = Resources.labels.sodusaukhighino;
                    Label17.Text = Resources.labels.sodusaukhighino;
                    lblTextError.Text = Resources.labels.transactionsuccessful;
                    if (cbmau.Checked == true)
                    {
                        string fee = rdNguoiChiuPhi.SelectedIndex.ToString();
                        new SmartPortal.IB.Transfer().InsertTransferTemplate(txttenmau.Text.Trim(), txtDesc.Text, ddlSenderAccount.SelectedItem.ToString(), ddlReceiverAccount.SelectedItem.ToString(), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()), lblSenderCCYID.Text, SmartPortal.Constant.IPC.BAC, Session["userID"].ToString(), "0", "", fee, "", "", "", "", "", lblReceiverName.Text, lblSenderName.Text, "", "", "", "", "", ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE == "0")
                        {
                            lblTextError.Text += " & " + Resources.labels.luumauchuyenkhoanthanhcong;
                        }
                        else
                        {
                            lblTextError.Text = IPCERRORDESC;
                        }
                    }

                    //ghi vo session dung in

                    Hashtable hasPrint = new Hashtable();
                    hasPrint.Add("status", Resources.labels.thanhcong);
                    hasPrint.Add("senderAccount", lblSenderAccount.Text);
                    hasPrint.Add("senderBalance", lblBalanceSender.Text);
                    hasPrint.Add("ccyid", lblSenderCCYID.Text);
                    hasPrint.Add("senderName", lblSenderName.Text);
                    hasPrint.Add("recieverAccount", lblReceiverAccount.Text);
                    hasPrint.Add("recieverName", lblReceiverName.Text);
                    hasPrint.Add("recieverBalance", lblBalanceReceiver.Text);
                    hasPrint.Add("transferType", "");
                    hasPrint.Add("amount", lblAmount.Text);
                    hasPrint.Add("amountchu", txtChu.Value.ToString());
                    hasPrint.Add("feeType", lblPhi.Text);
                    hasPrint.Add("feeAmount", lblPhiAmount.Text);
                    hasPrint.Add("desc", lblDesc.Text);
                    hasPrint.Add("tranID", result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : "");
                    hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    hasPrint.Add("senderBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblSenderBranch.Text));
                    hasPrint.Add("receiverBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblReceiverBranch.Text));
                    Session["print"] = hasPrint;

                    btnPrint.Visible = true;
                    btnView.Visible = true;

                    //send mail edit by VuTran
                    try
                    {
                        SmartPortal.Common.EmailHelper.TransactionSuccess_SendMail(hasPrint, Session["userID"].ToString());
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + "Error when send email from TranserInBAC1", Request.Url.Query);
                    }

                }
                else
                {
                    txtOTP.Text = "";
                    btnPrint.Visible = false;
                    btnView.Visible = false;
                    Label58.Text = Resources.labels.sodutruockhighino;
                    Label17.Text = Resources.labels.sodutruockhighino;
                    switch (result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblTextError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                            if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                            }
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                            lblTextError.Text = Resources.labels.wattingbankapprove;
                            if (cbmau.Checked == true)
                            {
                                string fee = rdNguoiChiuPhi.SelectedIndex.ToString();
                                new SmartPortal.IB.Transfer().InsertTransferTemplate(txttenmau.Text.Trim(), txtDesc.Text, ddlSenderAccount.SelectedItem.ToString(), ddlReceiverAccount.SelectedItem.ToString(), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()), lblSenderCCYID.Text, SmartPortal.Constant.IPC.BAC, Session["userID"].ToString(), "0", "", fee, "", "", "", "", "", lblReceiverName.Text, lblSenderName.Text, "", "", "", "", "", ref IPCERRORCODE, ref IPCERRORDESC);

                                if (IPCERRORCODE == "0")
                                {
                                    lblTextError.Text += " & " + Resources.labels.luumauchuyenkhoanthanhcong;
                                }
                                else
                                {
                                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                                }
                            }
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGPARTOWNERAPPROVE:
                            lblTextError.Text = Resources.labels.wattingpartownerapprove;
                            if (cbmau.Checked == true)
                            {
                                string fee = rdNguoiChiuPhi.SelectedIndex.ToString();
                                new SmartPortal.IB.Transfer().InsertTransferTemplate(txttenmau.Text.Trim(), txtDesc.Text, ddlSenderAccount.SelectedItem.ToString(), ddlReceiverAccount.SelectedItem.ToString(), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()), lblSenderCCYID.Text, SmartPortal.Constant.IPC.BAC, Session["userID"].ToString(), "0", "", fee, "", "", "", "", "", lblReceiverName.Text, lblSenderName.Text, "", "", "", "", "", ref IPCERRORCODE, ref IPCERRORDESC);

                                if (IPCERRORCODE == "0")
                                {
                                    lblTextError.Text += " & " + Resources.labels.luumauchuyenkhoanthanhcong;
                                }
                                else
                                {
                                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                                }
                            }
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                            lblTextError.Text = Resources.labels.wattinguserapprove;
                            if (cbmau.Checked == true)
                            {
                                string fee = rdNguoiChiuPhi.SelectedIndex.ToString();
                                new SmartPortal.IB.Transfer().InsertTransferTemplate(txttenmau.Text.Trim(), txtDesc.Text, ddlSenderAccount.SelectedItem.ToString(), ddlReceiverAccount.SelectedItem.ToString(), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()), lblSenderCCYID.Text, SmartPortal.Constant.IPC.BAC, Session["userID"].ToString(), "0", "", fee, "", "", "", "", "", lblReceiverName.Text, lblSenderName.Text, "", "", "", "", "", ref IPCERRORCODE, ref IPCERRORDESC);

                                if (IPCERRORCODE == "0")
                                {
                                    lblTextError.Text += " & " + Resources.labels.luumauchuyenkhoanthanhcong;
                                }
                                else
                                {
                                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                                }
                            }
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                            lblTextError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblTextError.Text = Resources.labels.authentypeinvalid;
                            return;
                        default:
                            lblTextError.Text = string.IsNullOrEmpty(result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString()) ? "Transaction error!" : result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                            return;
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
                LoadAccountInfo();
                LoadAccountInforeceiver();
                SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
                //DataSet dsDetailAcc = new DataSet();
                //dsDetailAcc = acct.GetInfoDD(Session["userID"].ToString(), lblSenderAccount.Text.Trim(), ref errorCode, ref errorDesc);

                if (result["IPCTRANSID"] != null)
                {
                    lblEndTransactionNo.Visible = true;
                    lblEndTransactionNo.Text = result["IPCTRANSID"].ToString();
                }
                else
                {
                    lblEndTransactionNo.Visible = false;
                    Label30.Visible = false;
                }
                lblEndDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                lblendSenderAccount.Text = ddlSenderAccount.SelectedItem.Text;
                lblEndBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hdBalanceSender.Value), lblSenderCCYID.Text.Trim());
                lblEndSenderCCYID.Text = lblSenderCCYID.Text;
                lblEndSenderName.Text = lblSenderName.Text;

                //dsDetailAcc = acct.GetInfoDD(Session["userID"].ToString(), lblReceiverAccount.Text.Trim(), ref errorCode, ref errorDesc);

                lblEndReceiverAccount.Text = lblReceiverAccount.Text;
                lblEndReceiverName.Text = lblReceiverName.Text;
                lblEndBalanceReceiver.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hdBalanceReceiver.Value), lblSenderCCYID.Text.Trim());
                lblEndReceiverCCYID.Text = lblReceiverCCYID.Text;

                lblFeeCCYID.Text = lblReceiverCCYID.Text;
                lblEndAmountCCYID.Text = lblAmountCCYID.Text;
                //lblEndHinhThuc.Text = lblHinhThuc.Text;
                lblEndAmount.Text = lblAmount.Text;
                lblEndPhi.Text = lblPhi.Text;
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
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=91"));
    }
    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountInfo();
    }
    private void LoadAccountInfo()
    {
        try
        {

            string account = ddlSenderAccount.SelectedItem.Value.ToString();
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string User = Session["userID"].ToString();
            Account acct = new Account();
            Hashtable ht = new Hashtable();
            ht = acct.GetInfoAccount(User, account, ref ErrorCode, ref ErrorDesc);
            ShowDD(account, ht);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferBAC1_Widget", "LoadAccountInfo", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void ShowDD(string acctno, Hashtable htact)
    {
        try
        {
            if (htact[SmartPortal.Constant.IPC.CCYID] != null)
            {
                lblAvailableBalCCYID.Text = htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim();
                hdSenderCCYID.Value = lblCurrency.Text = lblAvailableBalCCYID.Text;
            }
            if (htact[SmartPortal.Constant.IPC.AVAILABLEBALANCE] != null)
            {
                hdBalanceSender.Value = SmartPortal.Common.Utilities.Utility.FormatStringCore(htact[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString().Trim());
                lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(hdBalanceSender.Value, htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim());

            }
            if (htact["TYPEID"] != null)
            {
                hdActTypeSender.Value = htact["TYPEID"].ToString();
            }
            if (htact[SmartPortal.Constant.IPC.FULLNAME] != null)
            {
                hdSenderName.Value = htact[SmartPortal.Constant.IPC.FULLNAME].ToString();
            }
            if (htact[SmartPortal.Constant.IPC.BRANCHID] != null)
            {
                hdBranchReceiver.Value = htact[SmartPortal.Constant.IPC.BRANCHID].ToString();
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

            string account = ddlReceiverAccount.SelectedItem.Value.ToString();
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string User = Session["userID"].ToString();
            Account acct = new Account();
            Hashtable ht = new Hashtable();
            ht = acct.GetInfoAccount(User, account, ref ErrorCode, ref ErrorDesc);
            ShowDDreceiver(account, ht);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferBAC1_Widget", "LoadAccountInforeceiver", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void ShowDDreceiver(string acctno, Hashtable htact)
    {
        try
        {
            if (htact[SmartPortal.Constant.IPC.CCYID] != null)
            {
                hdReceiverCCYID.Value = lblAvailableBalCCYIDr.Text = htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim();
                lblCurrency.Text = lblAvailableBalCCYID.Text;
            }
            if (htact[SmartPortal.Constant.IPC.AVAILABLEBALANCE] != null)
            {
                hdBalanceReceiver.Value = SmartPortal.Common.Utilities.Utility.FormatStringCore(htact[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString().Trim());
                lblAvailableBalr.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(hdBalanceReceiver.Value, htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim());
            }
            if (htact["TYPEID"] != null)
            {
                hdActTypeReceiver.Value = htact["TYPEID"].ToString();
            }
            if (htact[SmartPortal.Constant.IPC.FULLNAME] != null)
            {
                hdReceiverName.Value = htact[SmartPortal.Constant.IPC.FULLNAME].ToString();
            }
            if (htact[SmartPortal.Constant.IPC.BRANCHID] != null)
            {
                hdBranchReceiver.Value = lblSenderBranch.Text = htact[SmartPortal.Constant.IPC.BRANCHID].ToString();
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
        if (Session["print"] == null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Session["print"] == null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
        }
    }
    protected void ddlLoaiXacThuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAction.Enabled = false;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
    }
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        try
        {
            ddlLoaiXacThuc.Enabled = false;
            btnAction.Enabled = true;
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
                    btnAction.Enabled = false;
                    break;
                default:
                    lblTextError.Text = IPCERRORDESC;
                    btnAction.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadLoaiXacThuc()
    {
        try
        {
            ViewState["isSendFirst"] = 1;
            btnAction.Enabled = false;
            SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
            DataTable dt = objTran.LoadAuthenType(Session["userID"].ToString());
            ddlLoaiXacThuc.DataSource = dt;
            ddlLoaiXacThuc.DataTextField = "TYPENAME";
            ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
            ddlLoaiXacThuc.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
