using System;
using System.Collections;
using System.Data;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;
using SmartPortal.SEMS;

public partial class Widgets_RequestReversalTransaction_ViewDetails_Widget : WidgetBase
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
                btnSubmit.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE);
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
            string tranID = GetParamsPage(IPC.ID)[0].Trim();
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
                    hdAmount.Value = dt.Rows[0]["NUM01"].ToString();
                    lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim());
                    string ccyid = hdCCYID.Value = dt.Rows[0]["CCYID"].ToString();
                    lblstbc.Text = Utility.NumtoWords(Convert.ToDouble(lblAmount.Text)) + " " + ccyid;
                    lblCCYID.Text = ccyid;
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

                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btback_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string tranID = GetParamsPage(IPC.ID)[0].Trim();
        object[] searchObject = new object[] { tranID };
        ds = _service.common("SEMS_BO_CHECK_RRT", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

        if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
        {
            string commandName = IPC.ACTIONPAGE.APPROVE;
            string commandArg = GetParamsPage(IPC.ID)[0].Trim();
            if (CheckPermitPageAction(commandName))
            {
                switch (commandName)
                {
                    case IPC.ACTIONPAGE.APPROVE:
                        RedirectToActionPage(IPC.ACTIONPAGE.APPROVE, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                        break;
                }
            }
        }
        else
        {
            lblError.Text = IPCERRORDESC;
        }
       
    }
}
