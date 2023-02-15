using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.IB;

using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBBillPayment1_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;
    DataSet dsReceiverList = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";
            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
            load_unit(sender, e);
            //hide panel
            if (!IsPostBack)
            {
                pnAmount.Visible = false;
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;

                DataTable dtCorp = new DataTable();
                dtCorp = new SmartPortal.IB.Payment().GetCorpList(ref IPCERRORCODE, ref IPCERRORDESC);
                if (dtCorp.Rows.Count == 0)
                {
                    throw new Exception();
                }
                ddlCorpList.DataSource = dtCorp;
                ddlCorpList.DataTextField = "CORPNAME";
                ddlCorpList.DataValueField = "CORPID";
                ddlCorpList.DataBind();

                //load provider...
                ddlcorp_SelectedIndexChanged(sender, e);
                LoadAccountInfo();
                //LayCCYID();
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void load_unit(object sender, EventArgs e)
    {
        try
        {
            string errorcode = "";
            string errorDesc = "";
            DataSet ds = new DataSet();
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            //ds = objAcct.GetInfoAcct(Session["userID"].ToString(), "IB000604", "", ref errorcode, ref errorDesc);
            ds = objAcct.getAccount(Session["userID"].ToString(), "IB000604", "", ref errorcode, ref errorDesc);

            ds.Tables[0].DefaultView.RowFilter = "accttype in ('DD', 'CD') and status = 'A'";

            if (!IsPostBack)
            {
                ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                ddlSenderAccount.DataValueField = ds.Tables[0].Columns[SmartPortal.Constant.IPC.ACCTNO].ColumnName.ToString();
                ddlSenderAccount.DataBind();
            }
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], Request.Url.Query);
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
    protected void btnNextP2_Click(object sender, EventArgs e)
    {
        string senderName = "";
        string receiverName = "";
        string balanceSender = "";
        string acctCCYID = "";
        string receiverCCYID = "";

        try
        {
            //if (!string.IsNullOrEmpty(Session["BillPaymentNoteTip"].ToString()))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Session["BillPaymentNoteTip"].ToString() + "')", true);
            //}
            #region validate amount
            if (double.Parse(txtAmount.Text.Trim()) < double.Parse(lblMinAmount.Text.Trim()) | double.Parse(txtAmount.Text.Trim()) > double.Parse(lblMaxAmount.Text.Trim()))
            {
                lblTextError.Text = string.Format(Resources.labels.invalidamountbillpayment, Utility.FormatMoneyInputToView(lblMinAmount.Text.Trim(), lblAmountCCYID.Text), Utility.FormatMoneyInputToView(lblMaxAmount.Text.Trim(), lblAmountCCYID.Text));
                return;
            }
            #endregion

            if (txtREF1.Text != "" || txtREF1.Text != "")
            {
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();

                Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.SelectedItem.Text.Trim());
                //Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.Text.Trim());
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

                //check ccyid
                bool sameCCYCD = objAcct.CheckSameCCYCD(acctCCYID, lblCurrency.Text);
                if (!sameCCYCD)
                {
                    lblTextError.Text = Resources.labels.invalidacctCCYCDtotransfer;
                    return;
                }

                // CHECK AMOUNT
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(balanceSender, true))
                {
                    lblTextError.Text = Resources.labels.amountinvalid;
                    return;
                }


                #region LOAD INFO
                lblSenderAccount.Text = ddlSenderAccount.SelectedItem.Text;
                //lblSenderAccount.Text = ddlSenderAccount.Text;
                lblSenderName.Text = senderName;
                lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender, acctCCYID);
                lblSenderCCYID.Text = acctCCYID;
                lblFeeCCYID.Text = acctCCYID;
                lbCCYID.Text = acctCCYID;

                lblCorpName.Text = ddlCorpList.SelectedItem.ToString();
                lblService.Text = ddlservice.SelectedItem.ToString();

                lblcindexref1.Text = lblindexref1.Text;
                lblcindexref2.Text = lblindexref2.Text;
                lblcindexref3.Text = lblindexref3.Text;
                lblcvalueref1.Text = txtREF1.Text;
                lblcvalueref2.Text = txtREF2.Text;
                lblcvalueref3.Text = txtREF3.Text;
                lblcindexref3.Visible = false;
                lblcvalueref3.Visible = false;
                if (!string.IsNullOrEmpty(txtREF3.Text))
                {
                    lblcindexref3.Visible = true;
                    lblcvalueref3.Visible = true;
                }



                #endregion

                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), acctCCYID);
                lblPhi.Text = rdNguoiChiuPhi.SelectedValue;
                lblDesc.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text);

                #region tinh phi
                double fee = 0;
                DataTable dtfee = new SmartPortal.IB.Payment().GetFee(ddlservice.SelectedValue.Trim(),
                     SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()),
                     lblSenderAccount.Text, txtREF1.Text, txtREF2.Text, txtREF3.Text, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE.Equals("0") && dtfee.Rows[0][0] != "")
                {
                    fee += Double.Parse(dtfee.Rows[0][SmartPortal.Constant.IPC.CREFEE].ToString());
                    fee += Double.Parse(dtfee.Rows[0][SmartPortal.Constant.IPC.DEBITFEE].ToString());
                }

                #endregion



                lblPhiAmount.Text = fee.ToString();
                lblPhi.Text = "Sender";
                lblTextError.Text = "";
                pnAmount.Visible = false;
                pnConfirm.Visible = true;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = false;

            }
            else
            {
                lblTextError.Text = "Input error!";
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
    protected void btnP1Next_Click(object sender, EventArgs e)
    {
        try
        {
            lblDebitAccountP2.Text = ddlSenderAccount.SelectedValue;
            lblAvailableBalP2.Text = lblAvailableBal.Text;
            lblAvailableBalCCYIDP2.Text = lblAvailableBalCCYID.Text;
            lblLastTranDateP2.Text = lblLastTranDate.Text;
            lblCurrency.Text= lblAvailableBalCCYID.Text;
            if (txtREF3.Visible && string.IsNullOrEmpty(txtREF3.Text))
            {
                lblTextError.Text = "Please provide " + lblindexref3.Text;
                return;
            }
            if (string.IsNullOrEmpty(txtREF2.Text))
            {
                lblTextError.Text = "Please provide " + lblindexref2.Text;
                return;
            }
            if (string.IsNullOrEmpty(txtREF1.Text))
            {
                lblTextError.Text = "Please provide " + lblindexref1.Text;
                return;
            }


            if (!string.IsNullOrEmpty(hdfTrancode.Value))
            {
                CheckAmountWS();
            }
            else
            {
                CheckAmountHub();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
    protected void btnBackP2_Click(object sender, EventArgs e)
    {
        pnAmount.Visible = false;
        pnTIB.Visible = true;
        pnOTP.Visible = false;
        pnResultTransaction.Visible = false;
    }
    protected void CheckAmountHub()
    {
        try
        {
            Double Amount = 0;
            lblWSError.Text = "";

            Hashtable hs = new SmartPortal.IB.Payment().GetAmountFromHUB(ddlCorpList.SelectedValue, ddlservice.SelectedValue, ddlSenderAccount.SelectedValue, txtREF1.Text, txtREF2.Text, txtREF3.Text, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE.Equals("0"))
            {
                if (hs.Contains("AMOUNT"))
                {
                    Amount = Double.Parse(hs["AMOUNT"].ToString());
                    txtAmount.Enabled = (Amount == 0) ? true : (hs["EDITABLE"].ToString().Equals("Y") ? true : false);
                    if (hs.ContainsKey("CCYID"))
                    {
                        lblCurrency.Text = hs["CCYID"].ToString();
                    }
                    else
                    {
                        lblCurrency.Text = "LAK";
                    }
                    txtAmount.Text = Utility.FormatMoneyInputToView(Amount.ToString(), "");
                }

                pnTIB.Visible = false;
                pnAmount.Visible = true;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
            }
            else
            {
                switch (IPCERRORCODE)
                {
                    default:
                        lblWSError.Text = string.IsNullOrEmpty(IPCERRORDESC) ? Resources.labels.loi : IPCERRORDESC;
                        break;
                }

                txtREF1.Enabled = true;
                txtREF2.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBBillPayment1_Widget", "CheckAmount", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void CheckAmountWS()
    {
        try
        {
            //imgLoading.Visible = true;
            Double Fee = 0;
            Double Amount = 0;
            lblWSError.Text = "";

            Hashtable hs = new SmartPortal.IB.Payment().CheckAmount(ddlCorpList.SelectedValue.ToString(), hdfTrancode.Value, txtREF1.Text.Trim(), txtREF2.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0") && hs.Contains("AMOUNT"))
            {
                lblWSError.Text = "";
                Amount = Double.Parse(hs["AMOUNT"].ToString());

                lblCurrency.Text = hs["CCYID"].ToString();
                lblFeeCCYID.Text = hs["CCYID"].ToString();
                txtAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(Amount.ToString(), lblCurrency.Text);
                lblPhiAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(Fee.ToString(), lblFeeCCYID.Text);
                lblPhi.Text = "Sender";

                txtREF2.Text = txtREF1.Text;
                txtREF1.Enabled = false;
                txtREF2.Enabled = false;

                pnTIB.Visible = false;
                pnAmount.Visible = true;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
            }
            else
            {
                switch (IPCERRORCODE)
                {
                    //errorcode cho YCDC
                    case "1501":
                        lblWSError.Text = Resources.labels.ycdc_error_1501;
                        break;
                    case "1502":
                        lblWSError.Text = Resources.labels.ycdc_error_1502;
                        break;
                    case "1602":
                        lblWSError.Text = Resources.labels.ycdc_error_1602;
                        break;
                    case "2014":
                        lblWSError.Text = Resources.labels.ycdc_error_2014;
                        break;
                    case "1222":
                        lblWSError.Text = Resources.labels.ycdc_error_1222;
                        break;
                    case "10004":
                        lblWSError.Text = Resources.labels.ycdc_error_10004;
                        break;
                    case "1703":
                        lblWSError.Text = Resources.labels.ycdc_error_1703;
                        break;
                    case "2015":
                        lblWSError.Text = Resources.labels.ycdc_error_2015;
                        break;
                    default:
                        lblWSError.Text = Resources.labels.loi;
                        break;
                }

                txtREF1.Enabled = true;
                txtREF2.Enabled = true;
            }
            //imgLoading.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBBillPayment1_Widget", "CheckAmount", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBTransferInBank1_Widget", "btnApply_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferInBank1_Widget", "btnApply_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private Object m_lock = new Object();
    protected void btnAction_Click(object sender, EventArgs e)
    {
        lock (m_lock)
        {
            Hashtable result = new Hashtable();
            string OTPcode = txtOTP.Text;
            txtOTP.Text = "";
            try
            {

                result = new SmartPortal.IB.Payment().ProcessCollection(Session["userID"].ToString(),
                    ddlservice.SelectedValue.Trim(), "1", ddlCorpList.SelectedValue.Trim(),
                    SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()),
                    lblCurrency.Text.Trim(), lblSenderAccount.Text,
                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblcvalueref1.Text.Trim()),
                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblcvalueref2.Text.Trim()),
                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblcvalueref3.Text.Trim()),
                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblcindexref1.Text.Trim()),
                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblcindexref2.Text.Trim()),
                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblcindexref3.Text.Trim()),
                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblSenderName.Text.Trim()),
                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblSenderBranch.Text.Trim()),
                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblPhiAmount.Text.Trim()),
                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblDesc.Text.Trim()), ddlLoaiXacThuc.SelectedValue
                    ,OTPcode.Trim(), ref IPCERRORCODE,
                    ref IPCERRORDESC);


                if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {

                    lblTextError.Text = Resources.labels.transactionsuccessful;

                    //ghi vo session dung in
                    Hashtable hasPrint = new Hashtable();
                    hasPrint.Add("status", Resources.labels.thanhcong);
                    hasPrint.Add("senderAccount", lblSenderAccount.Text);
                    hasPrint.Add("senderBalance", lblBalanceSender.Text);
                    hasPrint.Add("ccyid", lblSenderCCYID.Text);
                    hasPrint.Add("senderName", lblSenderName.Text);
                    //hasPrint.Add("recieverAccount", lblReceiverAccount.Text);
                    //hasPrint.Add("recieverName", lblReceiverName.Text);
                    //hasPrint.Add("transferType", lblHinhThuc.Text);
                    hasPrint.Add("amount", lblAmount.Text);
                    hasPrint.Add("amountchu", Utility.NumtoWords(double.Parse(lblAmount.Text)));
                    hasPrint.Add("feeType", lblPhi.Text);
                    hasPrint.Add("feeAmount", lblPhiAmount.Text);
                    hasPrint.Add("desc", lblDesc.Text);
                    hasPrint.Add("tranID", result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : "");
                    hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    hasPrint.Add("senderBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblSenderBranch.Text));
                    //hasPrint.Add("receiverBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblReceiverBranch.Text));

                    hasPrint.Add("corpName", lblCorpName.Text);
                    hasPrint.Add("serviceName", lblService.Text);
                    hasPrint.Add("refindex1", lblcindexref1.Text);
                    hasPrint.Add("refindex2", lblcindexref2.Text);
                    hasPrint.Add("refindex3", lblcindexref3.Text);
                    hasPrint.Add("refvalue1", lblcvalueref1.Text);
                    hasPrint.Add("refvalue2", lblcvalueref2.Text);
                    hasPrint.Add("refvalue3", lblcvalueref3.Text);
                    Session["print"] = hasPrint;

                    btnPrint.Visible = true;
                    btnView.Visible = true;

                    //send mail by Vu Tran
                    try
                    {
                        SmartPortal.Common.EmailHelper.BillPaymentSuccess_SendMail(hasPrint, Session["userID"].ToString());
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString() + "Error when send email from TranserInBank1", Request.Url.Query);
                        //SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                        lblTextError.Text = "Transfer success but can't send mail!";
                    }
                }
                else
                {
                    txtOTP.Text = "";
                    btnPrint.Visible = false;

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
                        case "-13524":
                            lblTextError.Text = Resources.labels.destacccountinvalid;
                            return;
                        case "-89504":
                            lblTextError.Text = Resources.labels.taikhoankhongtontai;
                            return;
                        case "-1499":
                            lblTextError.Text = Resources.labels.khongthechuyenkhoan;
                            return;
                        case "-1492":
                            lblTextError.Text = Resources.labels.khongthechuyenkhoan;
                            return;
                        case "9999":
                        case "10499":
                            throw new Exception();
                        default:
                            lblTextError.Text = string.IsNullOrEmpty(result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString()) ? "Transaction error!" : result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                            return;
                    }


                    pnConfirm.Visible = true;
                    btnApply.Enabled = false;
                    btnBackTransfer.Enabled = false;
                    btnView.Visible = false;
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
                lblEndSenderAccount.Text = lblSenderAccount.Text;
                lblEndBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), lblSenderCCYID.Text.Trim());
                lblEndSenderName.Text = lblSenderName.Text;

                lblscorp.Text = lblCorpName.Text;
                lblsservice.Text = lblService.Text;
                lblsindexref1.Text = lblcindexref1.Text;
                lblsvalueref1.Text = lblcvalueref1.Text;
                lblsindexref2.Text = lblcindexref2.Text;
                lblsvalueref2.Text = lblcvalueref2.Text;
                lblsindexref3.Text = lblcindexref3.Text;
                lblsvalueref3.Text = lblcvalueref3.Text;
                lblsvalueref3.Visible = false;
                lblsindexref3.Visible = false;
                if (!string.IsNullOrEmpty(lblcvalueref3.Text))
                {
                    lblsvalueref3.Visible = true;
                    lblsindexref3.Visible = true;
                }

                lblEndAmount.Text = lblAmount.Text;
                lblEndPhi.Text = lblPhi.Text;
                lblEndPhiAmount.Text = lblPhiAmount.Text;
                lblEndDesc.Text = lblDesc.Text;
                lblEndPhiAmount.Text = lblPhiAmount.Text;

                lblBalanceCCYID.Text = lblSenderCCYID.Text;
                lblAmountCCYID.Text = lblSenderCCYID.Text;
                lblEndFeeCCYID.Text = lblSenderCCYID.Text;
                #endregion
                txtOTP.Text = "";
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
        }
        catch (Exception ex)
        {
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
        catch (Exception ex)
        {
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=121"));
    }
    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountInfo();
    }

    public void LayCCYID()
    {
        //DataTable tblAcctnoInfo = new SmartPortal.IB.Account().GetAcctnoInfo(ddlSenderAccount.SelectedValue);
        //if (tblAcctnoInfo.Rows.Count != 0)
        //{
        //    lblCurrency.Text = tblAcctnoInfo.Rows[0]["CCYID"].ToString();
        //}
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
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void ddlcorp_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtService = new DataTable();
        dtService = new SmartPortal.IB.Payment().GetServicebyCorpID(ddlCorpList.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);

        if (dtService.Rows.Count == 0)
        {
            ddlservice.Items.Clear();
            ddlservice.Items.Insert(0, new ListItem(Resources.labels.khongconhacungcapnao, ""));
        }
        else
        {
            ddlservice.DataSource = dtService;
            ddlservice.DataTextField = "SERNAME";
            ddlservice.DataValueField = "SERID";
            ddlservice.DataBind();
        }
        ddlservice_SelectedIndexChanged(sender, e);

        txtAmount.Text = "";
        txtREF1.Enabled = true;
        txtREF2.Enabled = true;
        lblWSError.Text = "";
        hdfTrancode.Value = "";
        CheckCorpInforAdd(sender, e);
    }

    protected void CheckCorpInforAdd(object sender, EventArgs e)
    {
        //khong biet lam the nao dang hardcode
        try
        {
            DataSet ds = new DataSet();
            string corpID = ddlCorpList.SelectedValue.ToString();
            Session["BillPaymentNoteTip"] = "";

            lblNoteTip.Visible = false;
            lblNoteTipHeader.Visible = false;
            lblNoteTip.Text = "";


            //default
            txtAmount.Enabled = true;

            ds = new SmartPortal.IB.Payment().CheckWebService("HUB_CHECKWEBSERVICE", new object[] { corpID }, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0") && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //check webservice
                    if (ds.Tables[0].Select("PARANAME='IPCTRANCODE'").Length > 0)
                    {
                        DataRow[] dr = ds.Tables[0].Select("PARANAME='IPCTRANCODE'");
                        hdfTrancode.Value = dr[0]["PARAVALUE"].ToString();
                        txtAmount.Enabled = false;
                    }
                    else
                    {
                        txtAmount.Enabled = true;
                    }

                    //check tip
                    if (ds.Tables[0].Select("PARANAME='NOTETIP'").Length > 0)
                    {
                        DataRow[] dr = ds.Tables[0].Select("PARANAME='NOTETIP'");
                        Session["BillPaymentNoteTip"] = dr[0]["PARAVALUE"].ToString().Replace("<br/>", "\\n");
                        lblNoteTip.Visible = true;
                        lblNoteTipHeader.Visible = true;
                        lblNoteTip.Text = dr[0]["PARAVALUE"].ToString();
                    }
                    else
                    {
                        lblNoteTip.Text = "";
                        Session["BillPaymentNoteTip"] = "";
                    }
                }
            }
            txtAmount.Text = "";

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBBillPayment1_Widget", "CheckWebService", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ddlservice_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = new SmartPortal.IB.Payment().GetServiceInformation(ddlservice.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);

        if (dt.Rows.Count != 0)
        {
            lblindexref1.Text = dt.Rows[0]["REFNAME1"].ToString();
            lblindexref2.Text = dt.Rows[0]["REFNAME2"].ToString();
            lblMinAmount.Text = dt.Rows[0]["MINAMN"].ToString();
            lblMaxAmount.Text = dt.Rows[0]["MAXAMN"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["REFNAME3"].ToString()) && !dt.Rows[0]["REFNAME3"].ToString().Equals("-"))
            {
                lblindexref3.Visible = true;
                lblindexref3.Text = dt.Rows[0]["REFNAME3"].ToString();
                txtREF3.Visible = true;
            }
            else
            {
                lblindexref3.Visible = false;
                lblindexref3.Text = "";
                txtREF3.Visible = false;
            }
        }
        txtAmount.Text = "";
        txtREF1.Enabled = true;
        txtREF2.Enabled = true;
        lblWSError.Text = "";
        hdfTrancode.Value = "";
        CheckCorpInforAdd(sender, e);
    }
    protected void DropDownListOTP_SelectedIndexChanged(object sender, EventArgs e)
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBBillPayment1_Widget", "LoadAccountInfo", ex.ToString(), Request.Url.Query);
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
        catch (Exception ex)
        {
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        //15.12.2015 minh add to fix error 9999 
        if (Session["print"] == null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //15.12.2015 minh add to fix error 9999 
        if (Session["print"] == null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
        }
    }
}
