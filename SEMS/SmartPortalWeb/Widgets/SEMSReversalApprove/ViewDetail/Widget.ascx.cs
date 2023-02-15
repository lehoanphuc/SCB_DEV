using System;
using System.Collections;
using System.Data;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;
using SmartPortal.SEMS;
using System.Collections.Generic;
using System.Web;

public partial class Widgets_SEMSReversalAprrove_ViewDetails_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                btnApprove.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE);
                btnReject.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.REJECT);
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void BindData()
    {
        try
        {
            string ListID = GetParamsPage(IPC.ID)[0].Trim();
            string tranID = ListID.ToString().Split('+')[1].ToString();
            Hashtable hasPrint = new Hashtable();

            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.Transactions().GetTranByTranID(tranID, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {
                    pnDefault.Visible = true;
                    lblTransID.Text = dt.Rows[0]["IPCTRANSID"].ToString();
                    lblAccountSender.Text = SmartPortal.Common.Utilities.Utility.FormatStringCore(dt.Rows[0]["CHAR01"].ToString());
                    lblAccountReceiver.Text = dt.Rows[0]["CHAR02"].ToString();
                    hdAmount.Value = lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim());
                    string ccyid = dt.Rows[0]["CCYID"].ToString();
                    lblstbc.Text = Utility.NumtoWords(Convert.ToDouble(lblAmount.Text)) + " " + ccyid;
                    hdCCYID.Value = lblCCYID.Text = ccyid;
                    lblPagename.Text = dt.Rows[0]["PageName"].ToString();
                    lblReftype.Text = dt.Rows[0]["CHAR20"].ToString();
                    lblTelco.Text = dt.Rows[0]["TELCONAME"].ToString();
                    lblPhone.Text = dt.Rows[0]["CHAR07"].ToString().Trim();
                    lblCardAmount.Text = lblAmount.Text + " " + lblCCYID.Text;
                    DataTable tblU = new SmartPortal.SEMS.User().GetUBID(dt.Rows[0]["USERCURAPP"].ToString().Trim());

                    if (tblU.Rows.Count != 0)
                    {
                        lblLastApp.Text = tblU.Rows[0]["FULLNAME"].ToString();
                    }
                    lblCCYIDPhi.Text = ccyid;
                    lblCCYIDVAT.Text = ccyid;
                    lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(dt.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");

                    string[] c = dt.Rows[0]["TRANDESC"].ToString().Split('|');
                    lblDesc.Text = c[0].ToString();
                    double feeNum = SmartPortal.Common.Utilities.Utility.isDouble(dt.Rows[0]["NUM02"].ToString(), false);

                    string f = hdFee.Value = (feeNum).ToString();
                    string v = (0).ToString();
                    lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(f, ccyid);
                    lblVAT.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(v, dt.Rows[0]["CCYID"].ToString().Trim());

                    if (!dt.Rows[0]["CHAR01"].ToString().Equals(""))
                    {
                        pnSender.Visible = true;
                        lblSender.Text = dt.Rows[0]["CHAR01"].ToString().Length < 13 ? "Sender Phone" : Resources.labels.debitaccount;
                    }
                    else
                    {
                        pnSender.Visible = false;
                    }
                    if (!dt.Rows[0]["CHAR02"].ToString().Equals(""))
                    {
                        pnReceiver.Visible = true;
                        lblReceiver.Text = dt.Rows[0]["CHAR02"].ToString().Length < 13 ? "Receiver Phone" : Resources.labels.taikhoanbaoco;
                    }
                    else
                    {
                        pnReceiver.Visible = false;
                    }
                    pnTopup.Visible = !dt.Rows[0]["TELCONAME"].ToString().Equals("");
                    switch (dt.Rows[0]["STATUS"].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                            lblStatus.Text = Resources.labels.dangxuly;
                            break;
                        case SmartPortal.Constant.IPC.TRANSTATUS.PAYMENTFAIL:
                            lblStatus.Text = Resources.labels.thanhtoanthatbai;
                            break;
                        case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                            lblStatus.Text = Resources.labels.thanhcong;
                            switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
                            {
                                case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                    lblResult.Text = Resources.labels.duyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                    lblResult.Text = Resources.labels.khongduyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                    lblResult.Text = Resources.labels.choduyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                    lblResult.Text = Resources.labels.dangxuly;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                    lblStatus.Text = Resources.labels.dahoantien;
                                    break;
                            }
                            break;
                        case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
                            lblStatus.Text = Resources.labels.choduyet;
                            switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
                            {
                                case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                    lblResult.Text = Resources.labels.duyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                    lblResult.Text = Resources.labels.huy;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                    lblResult.Text = Resources.labels.choduyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                    lblResult.Text = Resources.labels.dangxuly;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                    lblResult.Text = Resources.labels.dahoantien;
                                    break;
                            }
                            break;
                        case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
                            lblStatus.Text = Resources.labels.loi;
                            switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
                            {
                                case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                    lblResult.Text = Resources.labels.duyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                    lblResult.Text = Resources.labels.huy;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                    lblResult.Text = Resources.labels.choduyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                    lblResult.Text = Resources.labels.dangxuly;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                    lblResult.Text = Resources.labels.dahoantien;
                                    break;
                            }
                            break;
                        case SmartPortal.Constant.IPC.TRANSTATUS.REJECTED:
                            lblStatus.Text = Resources.labels.khongduyet;
                            break;

                    }

                    //get detail
                    lblSenderName.Text = string.Empty;
                    lblRefindex1.Text = string.Empty;
                    lblRefvalue1.Text = string.Empty;
                    lblRefindex2.Text = string.Empty;
                    lblRefvalue2.Text = string.Empty;
                    lblServiceName.Text = string.Empty;
                    lblCorpName.Text = string.Empty;


                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        lblSenderName.Text = ds.Tables[1].Rows[0][0].ToString();
                    }

                    //load account name GL account
                    if (dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("SB_CANCELCASHCODE")
                        || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("C_CANCELCASHCODE")
                        || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("SW_CANCELCASHCODE")
                    )
                    {
                        object[] objects = new object[] { dt.Rows[0]["CHAR01"].ToString() };
                        DataSet dsGLAccount = _service.common("SEMS_ACC_ACCHRT_VIEW", objects, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (dsGLAccount.Tables[0] != null)
                        {
                            lblSenderName.Text = dsGLAccount.Tables[0].Rows[0]["ACName"].ToString();
                        }
                    }


                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        lblReceiverName.Text = ds.Tables[2].Rows[0][0].ToString();
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        if (dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("SB_CANCELCASHCODE")
                            || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("C_CANCELCASHCODE")
                            || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("SW_CANCELCASHCODE")
                        )
                        {
                            lblUserCreate.Text = dt.Rows[0]["USERID"].ToString().Trim();
                        }
                        else
                        {
                            lblUserCreate.Text = string.IsNullOrEmpty(ds.Tables[3].Rows[0][0].ToString()) ? dt.Rows[0]["USERID"].ToString().Trim() : ds.Tables[3].Rows[0][0].ToString();
                        }
                    }
                    if (dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("MBBPMBANKACT")
                      || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("IBBPMBANKACT")
                      || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("AMBPMWALLET")
                      || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("WLBPMWALLET")
                        )
                    {
                        pnBillPayment.Visible = true;
                        DataTable bt = new Biller().GetBillDetailsById(dt.Rows[0]["CHAR23"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                        if (bt.Rows.Count != 0)
                        {
                            lblBillerName.Text = bt.Rows[0]["BILLERNAME"].ToString();
                        }
                    }


                    //Hongnt fix visible button
                    string RRID = ListID.ToString().Split('+')[0].ToString();
                    try
                    {
                        DataSet dsReversal = new DataSet();
                        object[] searchObject = new object[] { Utility.KillSqlInjection(RRID) };
                        dsReversal = _service.common("SEMSGETTREVERSALBYID", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (dsReversal != null)
                        {
                            if (dsReversal.Tables.Count > 0)
                            {
                                if (dsReversal.Tables[0].Rows.Count > 0)
                                {
                                    string statusReversal = dsReversal.Tables[0].Rows[0]["Status"].ToString();
                                    if (statusReversal == "A" || statusReversal == "R")
                                    {
                                        btnApprove.Visible = false;
                                        btnApprove.Enabled = false;
                                        btnReject.Visible = false;
                                        btnReject.Enabled = false;
                                    }
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }

                    AddPrint(hasPrint, dt.Rows[0]["IPCTRANCODE"].ToString().Trim(), lblTransID.Text, lblDate.Text, lblSenderName.Text, lblAccountSender.Text, lblReceiverName.Text,
                      lblAccountReceiver.Text, lblAmount.Text, lblCCYID.Text, lblstbc.Text, lblFee.Text, lblDesc.Text, lblStatus.Text);
                    AddPrint(hasPrint, "refindex1", lblRefindex1.Text);
                    AddPrint(hasPrint, "refvalue1", lblRefvalue1.Text);
                    AddPrint(hasPrint, "refindex2", lblRefindex2.Text);
                    AddPrint(hasPrint, "refvalue2", lblRefvalue2.Text);
                    AddPrint(hasPrint, "BillerName", lblBillerName.Text);
                    AddPrint(hasPrint, "corpName", lblCorpName.Text);
                    AddPrint(hasPrint, "serviceName", lblServiceName.Text);
                    AddPrint(hasPrint, "telecomname", lblTelco.Text);
                    AddPrint(hasPrint, "phoneNo", dt.Rows[0]["CHAR07"].ToString().Trim());
                    AddPrint(hasPrint, "tranType", lblPagename.Text);
                    Session["print"] = hasPrint;

                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private Hashtable AddPrint(Hashtable hasPrint, string ipcTrancode, string tranid, string tranDate, string senderName, string senderAccount, string recieverName
        , string recieverAccount, string amount, string ccyid, string amountchu, string feeAmount, string desc, string status
        )
    {
        hasPrint["IPCTRANCODE"] = ipcTrancode;
        hasPrint["tranID"] = tranid;
        hasPrint["tranDate"] = tranDate;
        hasPrint["senderName"] = senderName;
        hasPrint["senderAccount"] = senderAccount;
        hasPrint["recieverName"] = recieverName;
        hasPrint["recieverAccount"] = recieverAccount;
        hasPrint["amount"] = amount;
        hasPrint["ccyid"] = ccyid;
        hasPrint["amountchu"] = amountchu;
        hasPrint["feeAmount"] = feeAmount;
        hasPrint["desc"] = desc;
        hasPrint["status"] = status;

        return hasPrint;
    }
    private Hashtable AddPrint(Hashtable hasPrint, string file, string value)
    {
        hasPrint[file] = value;
        return hasPrint;
    }

    protected void btnReject_OnClick(object sender, EventArgs e)
    {
        if (!CheckPermitPageAction(IPC.ACTIONPAGE.REJECT)) return;
        string ListID = GetParamsPage(IPC.ID)[0].Trim();
        string RRID = ListID.ToString().Split('+')[0].ToString();
        string tranID = ListID.ToString().Split('+')[1].ToString();
        RedirectToActionPage(IPC.ACTIONPAGE.REJECT, "&" + SmartPortal.Constant.IPC.ID + "=" + RRID);

    }

    protected void btnApprove_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (!CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE)) return;
            string ListID = GetParamsPage(IPC.ID)[0].Trim();
            string RRID = ListID.ToString().Split('+')[0].ToString();
            string TXREFRR = ListID.ToString().Split('+')[1].ToString();
            string DESCRIPTION = "Reversal transaction " + TXREFRR.ToString();
            string IPCTRANCODE = "SEMS_BO_REFUND";
            DataSet ds = new DataSet();
            Dictionary<object, object> inforrefundMoney = new Dictionary<object, object>();

            setPara(inforrefundMoney, RRID, TXREFRR, DESCRIPTION, IPCTRANCODE);
            ds = _service.CallStore("SEMS_BO_REFUND", inforrefundMoney, DESCRIPTION, "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
                lblError.Text = Resources.labels.ApproveSuccessfully;

                try
                {

                    SmartPortal.Common.Log.WriteLogDatabaseTransaction("RR"+RRID, "", Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "IPCLOGTRANS", "Approve request Reversal transaction", "", "", "A", Session["UserID"].ToString(),
                           SmartPortal.Common.Utilities.Utility.FormatMoneyInput(hdAmount.Value, hdCCYID.Value), Utility.KillSqlInjection(hdFee.Value));
                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        //RedirectToActionPage(IPC.ACTIONPAGE.LIST, string.Empty);
        RedirectBackToMainPage();
    }
    void setPara(Dictionary<object, object> infor, string RRID, string TXREFRR, string DESCRIPTION, string IPCTRANCODE)
    {
        infor.Add("RRID", Utility.KillSqlInjection(RRID));
        infor.Add("TXREFRR", Utility.KillSqlInjection(TXREFRR));
        infor.Add("DESCRIPTION", Utility.KillSqlInjection(DESCRIPTION));
        infor.Add("IPCCODE", Utility.KillSqlInjection(IPCTRANCODE));
        infor.Add("USERID", Session["userName"].ToString());
    }
}
