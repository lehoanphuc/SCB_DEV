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
using System.Linq;
using System.Text.RegularExpressions;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;


public partial class Widgets_IBCorpApprovedTransactions_ViewDetails_Widget : WidgetBase
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
            ds = new SmartPortal.IB.Transactions().GetApprovalTranByTranID(tranID, Session["UserID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            DataSet dsDocument = new DataSet();
            dsDocument = new SmartPortal.IB.Transactions().GetDocumentByTranID(tranID, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
            }

            DataTable dtDocument = new DataTable();

            DataColumn documentnamelink = new DataColumn("DOCUMENTNAME");
            DataColumn documenttypelink = new DataColumn("DOCUMENTTYPE");
            DataColumn filelink = new DataColumn("FILE");

            dtDocument.Columns.Add(documentnamelink);
            dtDocument.Columns.Add(documenttypelink);
            dtDocument.Columns.Add(filelink);
            //dtDocument = dsDocument.Tables[0];
            if (ds.Tables[5] != null)
            {
                if (ds.Tables[5].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                    {
                        string dctype = ds.Tables[5].Rows[i]["DocumentType"].ToString();
                        string dcname = "Document " + ds.Tables[5].Rows[i]["ID"].ToString();
                        string base64 = ds.Tables[5].Rows[i]["LINK"].ToString();
                        if (dctype.Equals(Resources.labels.chitiettrasoatgiaodich))
                        {
                            lblDocumentLink.NavigateUrl = ds.Tables[5].Rows[i]["LINK"].ToString();
                            lblDocumentLink.Visible = true;
                        }
                        else
                        {
                            dtDocument.Rows.Add(dcname, dctype, base64);
                        }
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
                        r1["Account"] = account.Substring(0, 4) + "*****" + account.Substring(account.Length - 4, 4);
                        //ktra so tai khoan
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
                Hashtable hasSender = new SmartPortal.IB.Account().GetInfoAccountCredit(lblAccount.Text, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE.Equals("0"))
                {
                    if (hasSender.ContainsKey(SmartPortal.Constant.IPC.FULLNAME))
                    {
                        lblSenderName.Text = hasSender[SmartPortal.Constant.IPC.FULLNAME].ToString();
                    }

                }
                else
                {
                    lblSenderName.Text = string.Empty;
                }

                //Receiver
                if (IPCTRANCODE == "IBINTERBANKTRANSFER" || IPCTRANCODE == "MB_TRANSFEROTHBANK" || IPCTRANCODE == "INTERBANK247" || IPCTRANCODE == "MBINTERBANK247QR" || IPCTRANCODE == "MBINTERBANK247")
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
                if (string.IsNullOrEmpty(lblReceiverName.Text.Trim()))
                {
                    pnReceiver.Visible = false;
                }
                if (dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IB_OPENBANKACT"))
                {
                    pnReceiver.Visible = true;
                    lblReceiverAccount.Text = dt.Rows[0]["CHAR02"].ToString();
                    lblReceiverName.Text = lblSenderName.Text;
                    pnOpenBank.Visible = true;
                    lblinterestrate.Text = dt.Rows[0]["CHAR09"].ToString();
                    lblTerm.Text = dt.Rows[0]["CHAR11"].ToString();

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


                DataTable ddTable = new DataTable();
                ddTable = ds.Tables[1];

                StringBuilder sT = new StringBuilder();

                DataTable dtGR = new DataTable();
                dtGR = ds.Tables[3];

                DataTable dtStep = new DataTable();
                dtStep = ds.Tables[4];

                List<string> grID = new List<string>();

                foreach (DataRow row in dtGR.Rows)
                {
                    grID.Add(row["GROUPID"].ToString());
                }

                grID = grID.Distinct().ToList();

                //Load Workflow 

                List<DataTable> result = ds.Tables[2].AsEnumerable()
               .GroupBy(row => row.Field<string>("WORKFLOWID"))
               .Select(g => g.CopyToDataTable())
               .ToList();

                string[] lstWF_Step = ds.Tables[0].Rows[0]["USERCURAPP"].ToString().Split('|');

                if (result.Count > 0)
                {
                    foreach (DataTable dtWF in result)
                    {

                        sT.Append("<table class='style1 table table-bordered table-hover footable' cellspacing='0' cellpadding='5'>");
                        sT.Append("<thead>");
                        sT.Append("<tr>");
                        sT.Append("<th class='thtdf'>");
                        sT.Append("Approval workflow ID");
                        sT.Append("</th>");
                        sT.Append("<th class='thtdf'>");
                        sT.Append(Resources.labels.order);
                        sT.Append("</th>");
                        sT.Append("<th class='thtdf'>");
                        sT.Append("Formula");
                        sT.Append("</th>");
                        sT.Append("<th class='thtdf'>");
                        sT.Append(Resources.labels.status);
                        sT.Append("</th>");
                        sT.Append("</tr>");
                        sT.Append("</thead>");
                        sT.Append("<tbody>");

                        foreach (DataRow row in dtWF.Rows)
                        {
                            int iStep = 0;
                            foreach (string sWFLID in lstWF_Step)
                            {
                                if (sWFLID.Split('#')[0].ToString() == row["WORKFLOWID"].ToString())
                                {
                                    iStep = Convert.ToInt32(sWFLID.Split('#')[1].ToString());
                                    break;
                                }
                            }
                            sT.Append("<tr class='trtd'>");
                            sT.Append("<td>");
                            sT.Append(row["WORKFLOWID"].ToString());
                            sT.Append("</td>");
                            sT.Append("<td>");
                            sT.Append(row["ORD"].ToString());
                            sT.Append("</td>");
                            sT.Append("<td>");

                            #region Status: 3
                            string formula = row["FmlFormat"].ToString();
                            // to mau cac buoc lon hon buoc hien tai(store chua xu ly => xu ly trong day)
                            if (dt.Rows[0]["STATUS"].ToString().Trim().Equals(SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE))
                            {
                                Regex regex = new Regex(@"[\d]{1,2}[A-Z]{1}");
                                var matches = regex.Matches(formula);
                                string status = string.Empty;
                                if (int.Parse(row["ORD"].ToString()) > iStep)
                                {
                                    if (matches.Count > 0)
                                    {
                                        regex = new Regex(@"[\d]{1,2}[A-Z]{1}");
                                        matches = regex.Matches(formula);

                                        List<string> lsCon = matches.Cast<Match>().Select(x => x.Value).Distinct().ToList();
                                        foreach (string match in lsCon)
                                        {
                                            formula = formula.Replace(match, "<b><font color='red'>" + match + "</font></b>");
                                        }
                                    }
                                }
                            }
                            #endregion

                            sT.Append(formula.ToUpper());
                            sT.Append("</td>");
                            sT.Append("<td>");
                            sT.Append(row["STATUS"].ToString());
                            sT.Append("</td>");
                            sT.Append("</tr>");
                        }

                        sT.Append("</tbody>");
                        sT.Append("</table>");
                        if (!result.LastOrDefault().Equals(dtWF))
                            sT.Append("<p><b><font color='#003366'>OR</font></b></p>");
                    }
                }
                else
                {
                    sT.Append("<table class='style1 table table-bordered table-hover footable' cellspacing='0' cellpadding='5'>");
                    sT.Append("<thead>");
                    sT.Append("<tr>");
                    sT.Append("<th class='thtdf'>");
                    sT.Append("Approval workflow ID");
                    sT.Append("</th>");
                    sT.Append("<th class='thtdf'>");
                    sT.Append(Resources.labels.order);
                    sT.Append("</th>");
                    sT.Append("<th class='thtdf'>");
                    sT.Append("Formula");
                    sT.Append("</th>");
                    sT.Append("</tr>");
                    sT.Append("</thead>");
                    sT.Append("</table>");
                }

                ltrWF.Text = sT.ToString();

                //Load group 
                sT = new StringBuilder();
                sT.Append("<table class='style1 table table-bordered table-hover footable' cellspacing='0' cellpadding='5'>");
                sT.Append("<thead>");
                sT.Append("<tr>");
                sT.Append("<th class='thtdf'>");
                sT.Append(Resources.labels.groupid);
                sT.Append("</th>");
                sT.Append("<th class='thtdf'>");
                sT.Append(Resources.labels.username);
                sT.Append("</th>");
                sT.Append("<th class='thtdf'>");
                sT.Append(Resources.labels.fullname);
                sT.Append("</th>");
                sT.Append("<th class='thtdf'>");
                sT.Append(Resources.labels.status);
                sT.Append("</th>");
                sT.Append("</tr>");
                sT.Append("</thead>");
                sT.Append("<tbody>");
                foreach (DataRow row in dtGR.Rows)
                {
                    grID.Add(row["GROUPID"].ToString());
                    sT.Append("<tr class='trtd'>");
                    sT.Append("<td>");
                    sT.Append(row["GROUPID"].ToString());
                    sT.Append("</td>");
                    sT.Append("<td>");
                    sT.Append(row["UserName"].ToString());
                    sT.Append("</td>");
                    sT.Append("<td>");
                    sT.Append(row["FULLNAME"].ToString());
                    sT.Append("</td>");
                    sT.Append("<td>");
                    switch (row["STATUS"].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.ACTIVE:
                            sT.Append(Resources.labels.active);
                            break;
                        case SmartPortal.Constant.IPC.DELETE:
                            sT.Append(Resources.labels.delete);
                            break;
                        case SmartPortal.Constant.IPC.REJECT:
                            sT.Append(Resources.labels.reject);
                            break;
                        case SmartPortal.Constant.IPC.PENDING:
                            sT.Append(Resources.labels.conpending1);
                            break;
                        case SmartPortal.Constant.IPC.NEW:
                            sT.Append(Resources.labels.moi);
                            break;
                    }
                    sT.Append("</td>");

                    sT.Append("</tr>");
                }

                sT.Append("</tbody>");
                sT.Append("</table>");

                ltrGR.Text = sT.ToString();


                //Load approval detail

                sT = new StringBuilder();
                sT.Append("<table class='style1 table table-bordered table-hover footable' cellspacing='0' cellpadding='5'>");
                sT.Append("<thead>");
                sT.Append("<tr>");
                sT.Append("<th class='thtdf'>");
                sT.Append(Resources.labels.order);
                sT.Append("</th>");
                sT.Append("<th class='thtdf'>");
                sT.Append(Resources.labels.ngayduyet);
                sT.Append("</th>");
                sT.Append("<th class='thtdf'>");
                sT.Append(Resources.labels.manhanvien);
                sT.Append("</th>");
                sT.Append("<th class='thtdf'>");
                sT.Append(Resources.labels.tennhanvien);
                sT.Append("</th>");
                sT.Append("<th class='thtdf'>");
                sT.Append(Resources.labels.kieunhanvien);
                sT.Append("</th>");
                sT.Append("<th class='thtdf'>");
                sT.Append("Staff group");
                sT.Append("</th>");
                sT.Append("<th class='thtdf'>");
                sT.Append(Resources.labels.status);
                sT.Append("</th>");
                sT.Append("</tr>");
                sT.Append("</thead>");
                sT.Append("<tbody>");

                foreach (DataRow row in ddTable.Rows)
                {
                    string status = string.Empty;
                    switch (row["APPRSTS"].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                            status = Resources.labels.daduyet;
                            break;
                        case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                            status = Resources.labels.reject;
                            break;
                    }
                    sT.Append("<tr class='trtd'>");
                    sT.Append("<td>");
                    sT.Append((ddTable.Rows.IndexOf(row) + 1).ToString());
                    sT.Append("</td>");
                    sT.Append("<td>");
                    sT.Append(row["IPCTRANSDATE"].ToString());
                    sT.Append("</td>");
                    sT.Append("<td>");
                    sT.Append(row["USERNAME"].ToString());
                    sT.Append("</td>");
                    sT.Append("<td>");
                    sT.Append(row["FULLNAME"].ToString());
                    sT.Append("</td>");
                    sT.Append("<td>");
                    sT.Append(row["USERTYPE"].ToString());
                    sT.Append("</td>");
                    sT.Append("<td>");
                    sT.Append(row["GROUPNAME"].ToString());
                    sT.Append("</td>");
                    sT.Append("<td>");
                    sT.Append(status);
                    sT.Append("</td>");
                    sT.Append("</tr>");
                }

                sT.Append("</tbody>");
                sT.Append("</table>");

                ltrTH.Text = sT.ToString();

                Hashtable hs = new Hashtable();
                hs.Add("wf", ltrWF.Text);
                hs.Add("gr", ltrGR.Text);
                hs.Add("th", ltrTH.Text);
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
        if (!base64.StartsWith("http"))
        {
            byte[] bytes = System.Convert.FromBase64String(base64);
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            HttpContext.Current.Response.AddHeader("Content-Length", Convert.ToString(bytes.Length));
            HttpContext.Current.Response.BinaryWrite(bytes);
        }
        else
        {
            HttpContext.Current.Response.Redirect(base64);
        }
    }
}
