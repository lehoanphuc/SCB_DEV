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

using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SmartPortal.Constant;
using SmartPortal.SEMS;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBViewLogTransactions_ViewDetails_Widget : WidgetBase
{
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : new DataTable(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }
    string IPCERRORCODE = String.Empty;
    string IPCERRORDESC = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBListTransWaitApprove_ViewDetails_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void BindData()
    {
        try
        {
            string tranID = string.Empty;
            double totalBalance = 0;
            double totalFee = 0;
            string IPCTRANCODE = string.Empty;
            tranID = GetParamsPage(IPC.IPCTRANSID)[0].Trim();
            DataSet ds = new DataSet();
            ds = new SmartPortal.IB.Transactions().GetApprovalTranByTranID(tranID,Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            DataSet dsDocument = new DataSet();
            dsDocument = new SmartPortal.IB.Transactions().GetDocumentByTranID(tranID, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
            }
            //TrungTQ add document
            
            DataTable dtDocument = new DataTable();

            DataColumn documentname = new DataColumn("DOCUMENTNAME");
            DataColumn documenttype = new DataColumn("DOCUMENTTYPE");
            DataColumn size = new DataColumn("SIZE");
            DataColumn file = new DataColumn("FILE");

            dtDocument.Columns.Add(documentname);
            dtDocument.Columns.Add(documenttype);
            dtDocument.Columns.Add(size);
            dtDocument.Columns.Add(file);
            //dtDocument = dsDocument.Tables[0];
            if (dsDocument != null)
            {
                if (dsDocument.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < dsDocument.Tables[0].Rows.Count; i++)
                    {
                        string dctype = dsDocument.Tables[0].Rows[i]["DOCUMENTTYPE"].ToString();
                        string dcname = dsDocument.Tables[0].Rows[i]["DOCUMENTNAME"].ToString();
                        string base64 = dsDocument.Tables[0].Rows[i]["FILE"].ToString();
                        byte[] bytes = System.Convert.FromBase64String(base64);
                        double filesize = (double)bytes.Length / 1024;
                        dtDocument.Rows.Add(dcname, dctype, Math.Ceiling(filesize).ToString() + "KB", base64);
                    }
                    pnDocument.Visible = true;
                    rptDocument.DataSource = dtDocument;
                    rptDocument.DataBind();
                    ViewState["TBLDOCUMENT"] = dtDocument;
                }
            }

            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
            {
                IPCTRANCODE = dt.Rows[0]["IPCTRANCODE"].ToString().Trim();
                //Ẩn hiện panel
                switch (IPCTRANCODE)
                {
                    case "MB000030":
                    case "AMBPMWALLET":
                    case "MBBPMBANKACT":
                    case "WLBPMWALLET":
                    case "IBBPMBANKACT":
                        pnReceiver.Visible = false;
                        pnBatch.Visible = false;
                        pnBillPayment.Visible = true;
                        pnTopup.Visible = false;
                        pnScheduleInfor.Visible = false;
                        break;
                    case "IB000499":
                        pnReceiver.Visible = false;
                        pnBatch.Visible = true;
                        pnBillPayment.Visible = false;
                        pnTopup.Visible = false;
                        pnScheduleInfor.Visible = false;
                        break;
                    case "IB000215":
                        pnReceiver.Visible = true;
                        pnBatch.Visible = false;
                        pnBillPayment.Visible = false;
                        pnTopup.Visible = false;
                        pnScheduleInfor.Visible = true;
                        break;
                    case "AM_MBTOPUP":
                    case "MBMTUBANKACT":
                    case "WLMTUWALLET":
                    case "IBMTUBANKACT":
                        pnReceiver.Visible = false;
                        pnBatch.Visible = false;
                        pnBillPayment.Visible = false;
                        pnTopup.Visible = true;
                        pnScheduleInfor.Visible = false;
                        break;
                    default:
                        pnReceiver.Visible = true;
                        pnBatch.Visible = false;
                        pnBillPayment.Visible = false;
                        pnTopup.Visible = false;
                        pnScheduleInfor.Visible = false;
                        break;
                }

                if (IPCTRANCODE == "IB000499")
                {

                    DataTable tblBatch = new SmartPortal.IB.Transactions().BatchViewDetail(tranID);
                    SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                    DataTable tempTable = new DataTable();
                    DataColumn col1 = new DataColumn("Stt");
                    DataColumn col2 = new DataColumn("Account");
                    DataColumn col3 = new DataColumn("User");
                    DataColumn col4 = new DataColumn("Amount");
                    DataColumn col5 = new DataColumn("Desc");
                    DataColumn col6 = new DataColumn("Status");
                    tempTable.Columns.AddRange(new DataColumn[] { col1, col2, col3, col4, col5, col6 });

                    for (int i = 0; i < tblBatch.Rows.Count; i++)
                    {
                        DataRow r1 = tempTable.NewRow();
                        r1["Stt"] = i + 1;
                        string account = tblBatch.Rows[i]["CHAR02"].ToString();
                        r1["Account"] = account.Substring(0, 4) +"*****"+ account.Substring(account.Length - 4, 4);
                        r1["User"] = tblBatch.Rows[i]["RECEIVERNAME"].ToString();
                        //r1["Amount"] = SmartPortal.Common.Utilities.Utility.FormatMoney(tblBatch.Rows[i]["NUM01"].ToString().Trim(), lblCCYID.Text.Trim()) + " " + lblCCYID.Text;
                        r1["Amount"] = "*****";
                        totalBalance += double.Parse(tblBatch.Rows[i]["NUM01"].ToString());
                        totalFee += double.Parse(tblBatch.Rows[i]["NUM02"].ToString());
                        r1["Desc"] = tblBatch.Rows[i]["TRANDESC"].ToString();
                        switch (tblBatch.Rows[i]["STATUS"].ToString())
                        {
                            case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                                r1["Status"] = Resources.labels.dangxuly1;
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                                r1["Status"] = Resources.labels.hoanthanh;
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
                                r1["Status"] = Resources.labels.loi;
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
                                r1["Status"] = Resources.labels.chochutaikhoanduyet;
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.REJECTED:
                                r1["Status"] = Resources.labels.reject;
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.PENDING:
                                r1["Status"] = Resources.labels.conpending;
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.PAYMENTFAIL:
                                r1["Status"] = Resources.labels.thanhtoanthatbai;
                                break;

                        }

                        tempTable.Rows.Add(r1);
                        lblTable.Text += "<tr class='trtd'><td>" + tempTable.Rows[i]["Stt"] + "</td><td>" + tempTable.Rows[i]["Account"] + "</td><td>" + tempTable.Rows[i]["User"] + "</td><td>" + tempTable.Rows[i]["Amount"] + "</td><td>" + tempTable.Rows[i]["Desc"] + "</td><td>" + tempTable.Rows[i]["Status"] + "</td></tr>";
                    }

                    gvConfirm.DataSource = tempTable;
                    gvConfirm.DataBind();

                }

                //Info
                lblTransID.Text = dt.Rows[0]["IPCTRANSID"].ToString();
                lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(dt.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                lblPagename.Text = dt.Rows[0]["PageName"].ToString();
                lblReftype.Text = dt.Rows[0]["CHAR20"].ToString();

                //Sender
                lblSender.Text = !string.IsNullOrEmpty(dt.Rows[0]["CHAR01"].ToString()) && dt.Rows[0]["CHAR01"].ToString().Length < 13 ? Resources.labels.senderphone : Resources.labels.debitaccount;
                lblAccount.Text = SmartPortal.Common.Utilities.Utility.FormatStringCore(dt.Rows[0]["CHAR01"].ToString());
                if (!string.IsNullOrEmpty(lblAccount.Text.Trim()))
                {
                    Hashtable hasSender = new SmartPortal.IB.Account().GetInfoAccountCredit(lblAccount.Text, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0"))
                    {
                        if(hasSender.ContainsKey(SmartPortal.Constant.IPC.FULLNAME))
                        {
                            lblSenderName.Text = hasSender[SmartPortal.Constant.IPC.FULLNAME].ToString();
                        }
                    }
                    else
                    {
                        lblSenderName.Text = string.Empty;
                    }

                }
                else
                {
                    pnSender.Visible = false;
                }
                //Receiver
                if (IPCTRANCODE == "IBINTERBANKTRANSFER" || IPCTRANCODE == "MB_TRANSFEROTHBANK")
                {
                    lblReceiver.Text = Resources.labels.taikhoanbaoco;
                }
                else
                {
                    lblReceiver.Text = !string.IsNullOrEmpty(dt.Rows[0]["CHAR02"].ToString()) && dt.Rows[0]["CHAR02"].ToString().Length < 13 ? Resources.labels.receiverphone : Resources.labels.taikhoanbaoco;
                }

                lblReceiverAccount.Text = (IPCTRANCODE == "IB000623" || IPCTRANCODE == "IB000624") ? SmartPortal.Common.Utilities.Utility.MaskDigits(dt.Rows[0]["CHAR02"].ToString()) : dt.Rows[0]["CHAR02"].ToString();
                DataTable dtLogTranDetail = new SmartPortal.SEMS.Transactions().GetLogTranDetail(tranID);
                foreach (DataRow r in dtLogTranDetail.Rows)
                {
                    switch (r["FIELDNAME"].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.RECEIVERNAME:
                            lblReceiverName.Text = r["FIELDVALUE"].ToString().Trim();
                            break;
                        case SmartPortal.Constant.IPC.SENDERNAME:
                            if (string.IsNullOrEmpty(lblSenderName.Text.Trim()))
                            {
                                lblSenderName.Text = r["FIELDVALUE"].ToString().Trim();
                            }
                            break;
                        default:
                            break;
                    }
                }
                if(string.IsNullOrEmpty(lblReceiverAccount.Text.Trim()))
                {
                    pnReceiver.Visible = false;
                }
                //Bill Payment
                if (IPCTRANCODE == "MBBPMBANKACT" || IPCTRANCODE == "AMBPMWALLET" || IPCTRANCODE == "WLBPMWALLET" || IPCTRANCODE == "IBBPMBANKACT")
                {
                    DataTable bt = new Biller().GetBillDetailsById(dt.Rows[0]["CHAR23"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                    if (bt.Rows.Count != 0)
                    {
                        lblBillerName.Text = bt.Rows[0]["BILLERNAME"].ToString();
                    }
                }

                //Top up
                if (IPCTRANCODE == "AM_MBTOPUP" || IPCTRANCODE == "MBMTUBANKACT" || IPCTRANCODE == "WLMTUWALLET" || IPCTRANCODE == "IBMTUBANKACT")
                {
                    lblTelco.Text = dt.Rows[0]["TELCONAME"].ToString();
                    lblPhone.Text = dt.Rows[0]["CHAR07"].ToString().Trim();
                    lblCardAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim()) + " " + dt.Rows[0]["CCYID"].ToString();
                }
                //Payment content

                string ccyid = dt.Rows[0]["CCYID"].ToString().Trim();
                if (IPCTRANCODE == "IB000499")
                {
                    lblSoTien.Text = Resources.labels.tongtien;
                    lblSoTienPhi.Text = Resources.labels.tongphi;
                    lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(totalBalance.ToString(), lblCCYID.Text.Trim());
                    lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(totalFee.ToString(), lblCCYID.Text.Trim());
                }
                else
                {
                    lblSoTien.Text = Resources.labels.sotien;
                    lblSoTienPhi.Text = Resources.labels.sotienphi;
                    lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["AMOUNT"].ToString(), ccyid);
                    lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM02"].ToString(), ccyid);
                }
                if (IPCTRANCODE == "IB_OPENBANKACT")
                {
                    pnReceiver.Visible = true;
                    lblReceiverAccount.Text =string.IsNullOrEmpty(dt.Rows[0]["CHAR02"].ToString())?"_": dt.Rows[0]["CHAR02"].ToString();
                    lblReceiverName.Text = lblSenderName.Text ;
                    pnOpenBank.Visible = true;
                    lblinterestrate.Text = dt.Rows[0]["CHAR09"].ToString();
                    lblTerm.Text = dt.Rows[0]["CHAR11"].ToString();

                }
                lblCCYID.Text = ccyid;
                lblCCYIDPhi.Text = ccyid;
                double sotienbangchu = SmartPortal.Common.Utilities.Utility.isDouble(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblAmount.Text, ccyid), false);
                lblstbc.Text = Utility.NumtoWords(sotienbangchu) + " " + ccyid;
                lblDesc.Text = dt.Rows[0]["TRANDESC"].ToString();
                lblUserCreate.Text = dt.Rows[0]["FULLNAME"].ToString();

                switch (dt.Rows[0]["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                        lblStatus.Text = Resources.labels.dangxuly1;
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                        lblStatus.Text = Resources.labels.hoanthanh;
                        switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                lblResult.Text = Resources.labels.daduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                lblResult.Text = Resources.labels.reject;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                lblResult.Text = Resources.labels.choduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                lblResult.Text = Resources.labels.dangxuly1;
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
                                lblResult.Text = Resources.labels.daduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                lblResult.Text = Resources.labels.reject;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                lblResult.Text = Resources.labels.choduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                lblResult.Text = Resources.labels.dangxuly1;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                lblResult.Text = Resources.labels.dahoantien;
                                break;

                        }
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
                        lblStatus.Text = Resources.labels.choduyet;
                        switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                lblResult.Text = Resources.labels.daduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                lblResult.Text = Resources.labels.reject;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                lblResult.Text = Resources.labels.choduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                lblResult.Text = Resources.labels.dangxuly1;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                lblResult.Text = Resources.labels.dahoantien;
                                break;

                        }
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.REJECTED:
                        lblStatus.Text = Resources.labels.reject;
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.PENDING:
                        lblStatus.Text = Resources.labels.conpending;
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.PAYMENTFAIL:
                        lblStatus.Text = Resources.labels.thanhtoanthatbai;
                        break;

                }

                //Schedule
                if (IPCTRANCODE == "IB000215" && dt.Rows[0]["CHAR09"].ToString().Length > 0)
                {
                    DataSet dsSh = new DataSet();
                    dsSh = new SmartPortal.IB.Schedule().GetInfo_Schedule_ByID(dt.Rows[0]["CHAR09"].ToString(),
                        ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsSh.Tables[0].Rows.Count != 0)
                    {
                        DataTable dtSh = dsSh.Tables[0];
                        switch (dtSh.Rows[0]["SCHEDULETYPE"].ToString().Trim())
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

                        lblScheduleName.Text = dtSh.Rows[0]["SCHEDULENAME"].ToString();
                        lblFromDate.Text =
                            SmartPortal.Common.Utilities.Utility.FormatDatetime(dtSh.Rows[0]["SCHEDULETIME"].ToString(),
                                "dd/MM/yyyy");
                        lblToDate.Text =
                            SmartPortal.Common.Utilities.Utility.FormatDatetime(dtSh.Rows[0]["ENDDATE"].ToString(),
                                "dd/MM/yyyy");
                        lblNextExecute.Text =
                            SmartPortal.Common.Utilities.Utility.FormatDatetime(dtSh.Rows[0]["NEXTEXECUTE"].ToString(),
                                "dd/MM/yyyy HH:mm:ss");
                    }
                }

                Hashtable hs = new Hashtable();
                hs.Add("batch", "<table class='style1' cellspacing='0' cellpadding='5'><thead><tr><th class='thtdf' style='width:10%'>" + Resources.labels.sothutu + "</th><th class='thtdf' style='width:20%'>" + Resources.labels.sotaikhoan + "</th><th class='thtdf' style='width:20%'>" + Resources.labels.nguoithuhuong + "</th><th class='thtdf' style='width:15%'>" + Resources.labels.sotien + "</th><th class='thtdf' style='width:20%'>" + Resources.labels.diengiai + "</th><th class='thtdf' style='width:25%'>" + Resources.labels.trangthai + "</th></tr></thead>" + lblTable.Text + "</table>");

                Session["hs"] = hs;

                Hashtable hasPrint = new Hashtable();
                //Info
                hasPrint["IPCTRANCODE"] = IPCTRANCODE;
                hasPrint["tranID"] = lblTransID.Text;
                hasPrint["date"] = lblDate.Text;
                hasPrint["tranType"] = lblPagename.Text;
                hasPrint["refNo"] = lblReftype.Text;

                //Sender
                hasPrint["debitAccount"] = lblAccount.Text;
                hasPrint["debitName"] = lblSenderName.Text;

                //Receiver
                hasPrint["creditAccount"] = lblReceiverAccount.Text;
                hasPrint["creditName"] = lblReceiverName.Text;

                //Topup
                hasPrint["phoneNo"] = lblPhone.Text;
                hasPrint["telecomName"] = lblTelco.Text;
                hasPrint["cardAmount"] = lblCardAmount.Text;

                //Biller
                hasPrint["billerName"] = lblBillerName.Text;

                //Payment content
                hasPrint["amount"] = lblAmount.Text;
                hasPrint["feeAmount"] = lblFee.Text;
                hasPrint["ccyid"] = lblCCYID.Text;
                hasPrint["desc"] = lblDesc.Text;
                hasPrint["status"] = lblStatus.Text;
                hasPrint["worker"] = lblUserCreate.Text;

                //Schedule
                hasPrint["scheduleName"] = lblScheduleName.Text;
                hasPrint["scheduleType"] = lblScheduleType.Text;
                hasPrint["fromDate"] = lblFromDate.Text;
                hasPrint["toDate"] = lblToDate.Text;
                hasPrint["nextExecute"] = lblNextExecute.Text;

                Session["print"] = hasPrint;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBListTransWaitApprove_ViewDetails_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void rptDocument_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["TBLDOCUMENT"];
        string base64 = dt.Rows[e.Item.ItemIndex]["FILE"].ToString();
        string filename = dt.Rows[e.Item.ItemIndex]["DOCUMENTNAME"].ToString() + dt.Rows[e.Item.ItemIndex]["DOCUMENTTYPE"].ToString().ToLower();
        byte[] bytes = System.Convert.FromBase64String(base64);
        HttpContext.Current.Response.ContentType = "application/octet-stream";
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
        HttpContext.Current.Response.AddHeader("Content-Length", Convert.ToString(bytes.Length));
        HttpContext.Current.Response.BinaryWrite(bytes);
    }
}
