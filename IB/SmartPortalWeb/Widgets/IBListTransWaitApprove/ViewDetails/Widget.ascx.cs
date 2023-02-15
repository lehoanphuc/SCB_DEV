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
using SmartPortal.ExceptionCollection;
using System.Text;
using System.Globalization;
using SmartPortal.Constant;
using SmartPortal.Model;
using SmartPortal.SEMS;
using System.Linq;
using System.Text.RegularExpressions;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBListTransWaitApprove_ViewDetails_Widget : WidgetBase
{
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : new DataTable(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }
    string ACTION = "";
    string cn = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    DataSet ApproveContractTable = new DataSet();
    int count = 0;
    private string useridtosend = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"] != null)
            {
                switch (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"].ToString().Trim())
                {
                    case "a":
                        btnReject.Visible = false;
                        btnApprove.Visible = true;
                        break;
                    case "r":
                        btnReject.Visible = true;
                        btnApprove.Visible = false;
                        break;

                }
            }

            if (ViewState["count"] != null)
            {
                count = int.Parse(ViewState["count"].ToString().Trim());
            }

            lblError.Text = "";

            if (Session["tranID"] != null)
            {
                btnNext.Visible = true;
                btnPrevious.Visible = true;
            }
            else
            {
                btnNext.Visible = false;
                btnPrevious.Visible = false;
            }

            if (!IsPostBack)
            {
                //get level of user
                DataSet ds = new SmartPortal.IB.User().GetFullUserByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                }
                else
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        //1.7.2016 gan ten nguoi duyet
                        lblnguoiduyet.Text = ds.Tables[0].Rows[0]["FullName"].ToString().Trim();

                        if (int.Parse(ds.Tables[0].Rows[0]["USERLEVEL"].ToString().Trim()) > 2)
                        {
                            pnToken.Visible = false;
                        }
                        else
                        {
                            pnToken.Visible = true;

                            #region Load Authen Type
                            ViewState["isSendFirst"] = 1;
                            DataTable tblAuthen = new SmartPortal.IB.Transactions().LoadAuthenType(Session["userID"].ToString());
                            ddlAuthenType.DataSource = tblAuthen;
                            ddlAuthenType.DataTextField = "TYPENAME";
                            ddlAuthenType.DataValueField = "AUTHENTYPE";
                            ddlAuthenType.DataBind();

                            btnSendOTP.Text = Resources.labels.send;


                            #endregion
                        }
                    }
                    else
                    {
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                }

                BindData();
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
    void LoadDocument()
    {
        string tranID = "";

        if (Session["tranID"] == null)
        {
            tranID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tranid"].ToString();
        }
        else
        {
            List<string> lstTran = new List<string>();
            lstTran = (List<string>)Session["tranID"];
            tranID = lstTran[count];
        }
        DataSet dsDocument = new DataSet();
        dsDocument = new SmartPortal.IB.Transactions().GetDocumentByTranID(tranID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
        }
        //Trung Add document
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
    }
    void BindData()
    {
        // for tinh fee
        string feeuserid = string.Empty;
        string feetrancode = string.Empty;
        string feecreditbranchid = string.Empty;

        #region Lấy thông tin giao dịch
        try
        {
            string tranID = "";

            if (Session["tranID"] == null)
            {
                tranID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tranid"].ToString();
            }
            else
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranID"];
                tranID = lstTran[count];
            }
            #region Trường hợp Session null
            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.Transactions().GetTranByTranID(tranID, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
           
            //minh add to get branchinfor
            string filtersender = "FIELDNAME='DEBITBRACHID'";
            string filterreceiver = "FIELDNAME='CREDITBRACHID'";
            string stSort = "FIELDNAME asc";
            try
            {
                DataTable dtbranchsender = ds.Tables[4].Select(filtersender, stSort).CopyToDataTable();
                txsenderbranch.Text = dtbranchsender.Rows[0]["FIELDVALUE"].ToString();
            }
            catch
            {
                txsenderbranch.Text = string.Empty;
            }
            try
            {
                DataTable dtbranchreceiver = ds.Tables[4].Select(filterreceiver, stSort).CopyToDataTable();
                txreceiverbranch.Text = dtbranchreceiver.Rows[0]["FIELDVALUE"].ToString();
            }
            catch
            {
                //txreceiverbranch.Text = txsenderbranch.Text;
                try
                {
                    txreceiverbranch.Text = ds.Tables[5].Rows[0]["BRANCHID"].ToString();
                }
                catch
                {
                    txreceiverbranch.Text = txsenderbranch.Text;
                }
            }

            // Trung Add document
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
                        }                    }
                    pnDocument.Visible = true;
                    rptDocument.DataSource = dtDocument;
                    rptDocument.DataBind();
                    ViewState["TBLDOCUMENT"] = dtDocument;
                }
            }

            DataTable dt = new DataTable();
            DataTable dtReceiverName = new DataTable();
            dt = ds.Tables[0];
            dtReceiverName = ds.Tables[2];
            if (dt.Rows.Count != 0)
            {
                //tinh fee
                feetrancode = dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].ToString();
                feecreditbranchid = txreceiverbranch.Text;
                hdTranCode.Value = dt.Rows[0]["IPCTRANCODE"].ToString().Trim();
                lblamountchu.Text = dt.Rows[0]["NUM01"].ToString();
                lblTransID.Text = dt.Rows[0]["IPCTRANSID"].ToString();
                lblAccount.Text = dt.Rows[0]["CHAR01"].ToString();
                tranname.Text = dt.Rows[0]["PAGENAME"].ToString();
                lblAccountNo.Text = (dt.Rows[0]["IPCTRANCODE"].ToString().Trim() == "IB000623" || dt.Rows[0]["IPCTRANCODE"].ToString().Trim() == "IB000624") ? SmartPortal.Common.Utilities.Utility.MaskDigits(dt.Rows[0]["CHAR02"].ToString()) : dt.Rows[0]["CHAR02"].ToString();
                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim());
                lblCCYID.Text = dt.Rows[0]["CCYID"].ToString();
                lblCCYIDPhi.Text = dt.Rows[0]["CCYID"].ToString();

                double sotienbangchu = SmartPortal.Common.Utilities.Utility.isDouble(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblAmount.Text, dt.Rows[0]["CCYID"].ToString()), false);
                lblstbc.Text = Utility.NumtoWords(sotienbangchu) + " " + dt.Rows[0]["CCYID"].ToString();
                lblDesc.Text = dt.Rows[0]["TRANDESC"].ToString();
                lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(dt.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                lblUserCreate.Text = dt.Rows[0]["FULLNAME"].ToString();
                lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM02"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim());
                feeuserid = dt.Rows[0]["USERID"].ToString().Trim();
                txusertosend.Text = dt.Rows[0]["USERID"].ToString();

                switch (dt.Rows[0]["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                        lblStatus.Text = Resources.labels.dangxuly;
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                        lblStatus.Text = Resources.labels.hoanthanh;
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
                                lblResult.Text = Resources.labels.khongduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                lblResult.Text = Resources.labels.choduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                lblResult.Text = Resources.labels.dangxuly;
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
                                lblResult.Text = Resources.labels.khongduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                lblResult.Text = Resources.labels.choduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                lblResult.Text = Resources.labels.dangxuly;
                                break;
                        }
                        break;
                }

                //Sender
                lblSenderName.Text = !string.IsNullOrEmpty(dt.Rows[0]["CHAR01"].ToString()) && dt.Rows[0]["CHAR01"].ToString().Length < 13 ? Resources.labels.senderphone : Resources.labels.debitaccount;
                lblAccount.Text = SmartPortal.Common.Utilities.Utility.FormatStringCore(dt.Rows[0]["CHAR01"].ToString());
                Hashtable hasSender = new SmartPortal.IB.Account().GetInfoAccountCredit(lblAccount.Text, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE.Equals("0"))
                {
                    lblSenderName.Text = hasSender[SmartPortal.Constant.IPC.FULLNAME].ToString();
                }
                else
                {
                   // throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }

                //Receiver
                if (dtReceiverName != null && dtReceiverName.Rows.Count > 0)
                {
                    lblReceiverName.Text = dtReceiverName.Rows[0]["FULLNAME"].ToString();
                }

                lblAccountNo.Text = (dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IB000623") || dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IB000624")) ? SmartPortal.Common.Utilities.Utility.MaskDigits(dt.Rows[0]["CHAR02"].ToString()) : dt.Rows[0]["CHAR02"].ToString();
                DataTable dtLogTranDetail = new SmartPortal.SEMS.Transactions().GetLogTranDetail(tranID);
                foreach (DataRow r in dtLogTranDetail.Rows)
                {
                    switch (r["FIELDNAME"].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.RECEIVERNAME:
                            lblReceiverName.Text = r["FIELDVALUE"].ToString().Trim();
                            break;
                        default:
                            break;
                    }
                }

                //Bill Payment
                if (dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IBBPMBANKACT") || dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("AMBPMWALLET") || dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("WLBPMWALLET") || dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("MBBPMBANKACT"))
                {
                    DataTable bt = new Biller().GetBillDetailsById(dt.Rows[0]["CHAR23"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                    if (bt.Rows.Count != 0)
                        lblBillerName.Text = bt.Rows[0]["BILLERNAME"].ToString();
                    pnBillPayment.Visible = true;
                    pnReceiver.Visible = false;
                    //payment2.Visible = true;
                }
                else
                {
                    pnBillPayment.Visible = false;
                    payment2.Visible = false;
                }

                //Top up
                if (dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("AM_MBTOPUP") || dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("MBMTUBANKACT")
                    || dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("WLMTUWALLET") || dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IBMTUBANKACT"))
                {
                    lblTelco.Text = dt.Rows[0]["TELCONAME"].ToString();
                    lblPhone.Text = dt.Rows[0]["CHAR07"].ToString().Trim();
                    lblCardAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim()) + " " + dt.Rows[0]["CCYID"].ToString();
                    pnTopup.Visible = true;
                    pnReceiver.Visible = false;
                }
                else
                    pnTopup.Visible = false;

                //Schedule
                if (dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IB000215"))
                {
                    pnScheduleInfor.Visible = true;
                }
                else
                {
                    pnScheduleInfor.Visible = false;
                }
                if (dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IB000215") && dt.Rows[0]["CHAR09"].ToString().Length > 0)
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


                //30.06.2016 minh add tinh fee

                #region tinh phi

                string senderfee = "0";
                string receiverfee = "0";
                string fee = "0";
                DataTable dtFee = new DataTable();
                dtFee = new SmartPortal.IB.Bank().GetFee(feeuserid, feetrancode, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblAmount.Text.Trim(), lblCCYID.Text), lblAccount.Text, feecreditbranchid, lblCCYID.Text, "");

                if (dtFee.Rows.Count != 0)
                {
                    senderfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeSenderAmt"].ToString(), lblCCYID.Text);
                    receiverfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["feeReceiverAmt"].ToString(), lblCCYID.Text);
                }
                lblFee.Text = senderfee;
                #endregion

                //Load approve detail
                DataTable ddTable = new DataTable();
                ddTable = ds.Tables[4];
                rptLTWA.DataSource = ddTable;
                rptLTWA.DataBind();

                if (dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IBINTERBANKTRANSFER") ||
                    dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IBCBTRANSFER") ||
                    dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("MB_TRANSFEROTHBANK") ||
                    dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("AM_MBTOPUP") ||
                    dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("MBMTUBANKACT") ||
                    dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IBBPMBANKACT") ||
                    dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("MBBPMBANKACT") ||
                    dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("WLMTUWALLET") ||
                    dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IBMTUBANKACT") ||
                    dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("INTERBANK247") ||
                    dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("MBINTERBANK247QR") ||
                    dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("MBINTERBANK247")
                    )
                {
                    lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM02"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim());
                }
                if (dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IBBUYGIFTCARD"))
                {
                    lblAccountNo.Text = "";
                }
                if (dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IB_TRANFERFX")
                    || dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IBTRANFERFXWL")
                    || dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("IBWLTRANFERFX")
                    || dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("MB_TRFOTHFX")
                    || dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("MB_TRFOTHFXWL")
                    || dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].Equals("WL_TRFOTHFX"))
                {
                    DataTable dtFee1 = new SmartPortal.IB.Bank().GetFeeFx(feeuserid, dt.Rows[0][SmartPortal.Constant.IPC.IPCTRANCODE].ToString(),
                        SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblAmount.Text, lblCCYID.Text), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(dt.Rows[0]["NUM04"].ToString(), lblCCYID.Text),
                        lblAccount.Text, feecreditbranchid, lblCCYID.Text, dt.Rows[0]["CHAR05"].ToString());
                    if (dtFee1.Rows.Count != 0)
                    {
                        lblFee.Text = senderfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee1.Rows[0]["feeSenderAmt"].ToString(), dt.Rows[0]["CCYID"].ToString());
                        receiverfee = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee1.Rows[0]["feeReceiverAmt"].ToString(), dt.Rows[0]["CHAR05"].ToString());
                        lblCCYIDPhi.Text = dt.Rows[0]["CCYID"].ToString().Trim();
                    }
                }
            }
            #endregion

            goto EXIT;
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
    EXIT:;

        #endregion
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            if (!btnApprove.Enabled)
                return;

            //approve
            string tranID = "";

            if (Session["tranID"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranID"];

                tranID = lstTran[count];

                //xử lý
                DataSet dsApprove = new SmartPortal.IB.Transactions().UserApp(lblTransID.Text.Trim(), ddlAuthenType.SelectedValue, txtAuthenCode.Text.Trim(), Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    //25.3.2016 minh add send mail:
                    //get infor to send mail:
                    btnSendOTP.Visible = false;
                    #region SendEmail
                    switch (hdTranCode.Value.ToString())
                    {
                        case "IB000035":
                            TopupTransactionSuccess_SendMail();
                            break;
                        case "IB000604":
                            BillPaymentSuccess_SendMail();
                            break;
                        case "IB000623":
                        case "IB000624":
                        //CR_TransactionSuccess_SendMail();
                        //break;
                        case "IB000208":
                        case "IB000201":
                        case "IB000499":
                        case "IB000405":
                            TransactionSuccess_SendMail();
                            break;
                        default:
                            //already sent by ipc
                            break;
                    }
                    #endregion

                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.duyetthanhcong;
                    lblStatus.Text = "Approved";
                    //lblAppSts.Text = lblnguoiduyet.Text;

                    //cập nhật lại List
                    lstTran.RemoveAt(count);
                    count -= 1;
                    ViewState["count"] = count;

                    Session["tranID"] = lstTran;
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;

                    if (lstTran.Count != 0)
                    {
                        Session["tranID"] = lstTran;
                        //chuyển qua giao dịch kế
                        //BindData();
                    }
                    else
                    {
                        Session["tranID"] = null;
                        if (Session["tranIDBatch"] != null)
                        {
                            btnNext.Enabled = true;
                        }
                        else
                        {
                            btnNext.Enabled = false;
                        }
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                        btnPrevious.Enabled = false;
                    }
                    return;
                }
                else
                {
                    
                    if (IPCERRORCODE == ConfigurationManager.AppSettings["waittingapproveec"].ToString())
                    {
                        btnSendOTP.Visible = false;
                        lblError.Text = SmartPortal.Common.Utilities.Utility.GetError(int.Parse(ConfigurationManager.AppSettings["waittingapproveec"].ToString())) + " - " + lblTransID.Text;

                        //cập nhật lại List
                        lstTran.RemoveAt(count);
                        count -= 1;
                        ViewState["count"] = count;
                        Session["tranID"] = lstTran;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;

                        if (lstTran.Count != 0)
                        {
                            Session["tranID"] = lstTran;
                            //chuyển qua giao dịch kế
                            //BindData();
                        }
                        else
                        {
                            Session["tranID"] = null;
                            if (Session["tranIDBatch"] != null)
                            {
                                btnNext.Enabled = true;
                            }
                            else
                            {
                                btnNext.Enabled = false;
                            }
                            btnApprove.Enabled = false;
                            btnReject.Enabled = false;
                            btnPrevious.Enabled = false;

                        }

                        return;
                    }
                    switch (IPCERRORCODE)
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblError.Text = IPCERRORDESC;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                            lblError.Text = IPCERRORDESC;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        case "9999":
                            lblError.Text = Resources.labels.transactionerror;
                            break;
                        case "12005":
                        case "12006":
                            btnApprove.Enabled = false;
                            btnReject.Enabled = false;
                            btnPrevious.Enabled = false;
                            btnNext.Enabled = false;
                            lblError.Text = IPCERRORDESC;
                            break;
                        default:
                            lblError.Text = IPCERRORDESC;
                            break;

                    }
                }
            }
            else
            {
                //approve khi duyệt 1 record
                DataSet dsApprove = new SmartPortal.IB.Transactions().UserApp(lblTransID.Text.Trim(), ddlAuthenType.SelectedValue, txtAuthenCode.Text.Trim(), Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    //25.3.2016 minh add send mail:
                    //get infor to send mail:
                    btnSendOTP.Visible = false;
                    #region SendEmail
                    switch (hdTranCode.Value.ToString())
                    {
                        case "IB000035":
                            TopupTransactionSuccess_SendMail();
                            break;
                        case "IB000604":
                            BillPaymentSuccess_SendMail();
                            break;
                        case "IB000623":
                        case "IB000624":
                        //CR_TransactionSuccess_SendMail();
                        //break;
                        case "IB000208":
                        case "IB000201":
                        case "IB000499":
                        case "IB000405":
                            TransactionSuccess_SendMail();
                            break;
                        default:
                            //already sent by ipc
                            break;
                    }
                    #endregion
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.duyetthanhcong;
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;
                    lblStatus.Text = "Approved";
                    //lblAppSts.Text = lblnguoiduyet.Text;

                    return;
                }
                else
                {
                    if (IPCERRORCODE == ConfigurationManager.AppSettings["waittingapproveec"].ToString())
                    {
                        lblError.Text = SmartPortal.Common.Utilities.Utility.GetError(int.Parse(ConfigurationManager.AppSettings["waittingapproveec"].ToString())) + " - " + lblTransID.Text;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                        btnSendOTP.Visible = false;
                        return;
                    }
                    switch (IPCERRORCODE)
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblError.Text = IPCERRORDESC;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                            lblError.Text = IPCERRORDESC;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        case "9999":
                            lblError.Text = Resources.labels.transactionerror;
                            break;
                        case "12005":
                        case "12006":
                            btnApprove.Enabled = false;
                            btnReject.Enabled = false;
                            btnPrevious.Enabled = false;
                            btnNext.Enabled = false;
                            lblError.Text = IPCERRORDESC;
                            break;
                        default:
                            lblError.Text = IPCERRORDESC;
                            return;

                    }
                }

                goto REDI;
            }
            goto EXIT;
        }
        catch
        {
        }
    REDI:
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=136"));
    EXIT:
        ;
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            if (!btnReject.Enabled)
                return;

            //huy giao dich
            string tranID = "";

            if (Session["tranID"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranID"];

                tranID = lstTran[count];

                //xử lý
                DataSet dsApprove = new SmartPortal.IB.Transactions().UserDestroy(lblTransID.Text.Trim(), Session["userID"].ToString(), ddlAuthenType.SelectedValue, txtAuthenCode.Text.Trim(), txtDesc.Text, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.huythanhcong;
                    lblStatus.Text = "Rejected";
                    //lblAppSts.Text = lblnguoiduyet.Text;

                    //cập nhật lại List
                    lstTran.RemoveAt(count);
                    count -= 1;
                    ViewState["count"] = count;

                    Session["tranID"] = lstTran;
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;

                    if (lstTran.Count != 0)
                    {
                        Session["tranID"] = lstTran;
                        //chuyển qua giao dịch kế
                        //BindData();
                    }
                    else
                    {
                        Session["tranID"] = null;
                        if (Session["tranIDBatch"] != null)
                        {
                            btnNext.Enabled = true;
                        }
                        else
                        {
                            btnNext.Enabled = false;
                        }
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                        btnPrevious.Enabled = false;
                    }

                    SendEmail();
                    return;
                }
                else
                {
                    if (IPCERRORCODE == ConfigurationManager.AppSettings["waittingapproveec"].ToString())
                    {
                        lblError.Text = SmartPortal.Common.Utilities.Utility.GetError(int.Parse(ConfigurationManager.AppSettings["waittingapproveec"].ToString())) + " - " + lblTransID.Text;

                        //cập nhật lại List
                        lstTran.RemoveAt(count);
                        count -= 1;
                        ViewState["count"] = count;

                        Session["tranID"] = lstTran;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;

                        if (lstTran.Count != 0)
                        {
                            Session["tranID"] = lstTran;
                            //chuyển qua giao dịch kế
                            //BindData();
                        }
                        else
                        {
                            Session["tranID"] = null;
                            btnApprove.Enabled = false;
                            btnReject.Enabled = false;
                            btnPrevious.Enabled = false;
                            btnNext.Enabled = false;
                        }

                        return;
                    }
                    switch (IPCERRORCODE)
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblError.Text = IPCERRORDESC;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                            lblError.Text = IPCERRORDESC;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        case "12005":
                        case "12006":
                            btnApprove.Enabled = false;
                            btnReject.Enabled = false;
                            btnPrevious.Enabled = false;
                            btnNext.Enabled = false;
                            lblError.Text = IPCERRORDESC;
                            break;
                        default:
                            lblError.Text = IPCERRORDESC;
                            return;

                    }

                }


            }
            else
            {
                //huy giao dich 1 record
                DataSet dsApprove = new SmartPortal.IB.Transactions().UserDestroy(lblTransID.Text.Trim(), Session["userID"].ToString(), ddlAuthenType.SelectedValue, txtAuthenCode.Text.Trim(), txtDesc.Text, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.huythanhcong;
                    btnReject.Enabled = false;
                    btnApprove.Enabled = false;
                    lblStatus.Text = "Rejected";
                    //lblAppSts.Text = lblnguoiduyet.Text;
                    SendEmail();
                    return;
                }
                else
                {
                    if (IPCERRORCODE == ConfigurationManager.AppSettings["waittingapproveec"].ToString())
                    {
                        lblError.Text = SmartPortal.Common.Utilities.Utility.GetError(int.Parse(ConfigurationManager.AppSettings["waittingapproveec"].ToString())) + " - " + lblTransID.Text;
                        return;
                    }
                    switch (IPCERRORCODE)
                    {

                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblError.Text = IPCERRORDESC;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                            lblError.Text = IPCERRORDESC;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        case "12005":
                        case "12006":
                            btnApprove.Enabled = false;
                            btnReject.Enabled = false;
                            btnPrevious.Enabled = false;
                            btnNext.Enabled = false;
                            lblError.Text = IPCERRORDESC;
                            break;
                        default:
                            lblError.Text = IPCERRORDESC;
                            return;

                    }
                }

                goto REDI;
            }
            goto EXIT;
        }
        catch
        {
        }
    REDI:
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=136"));
    EXIT:
        ;
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        btnSendOTP.Visible = true;
        if (Session["tranID"] != null)
        {
            List<string> lstTran = new List<string>();
            lstTran = (List<string>)Session["tranID"];

            if (count < lstTran.Count - 1)
            {
                count += 1;
                ViewState["count"] = count;
            }
            else
            {
                if (Session["tranIDBatch"] != null)
                {
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"] != null)
                    {
                        string type = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"].ToString();
                        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=278&type=" + type));
                    }
                    else
                    {
                        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=136"));
                    }
                }
            }
            BindData();
            btnSendOTP.Text = "Send OTP";
            txtAuthenCode.Text = "";
            btnApprove.Enabled = true;
            btnReject.Enabled = true;
        }
        else
        {
            if (Session["tranIDBatch"] != null)
            {
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=278&type=a"));
            }
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=136"));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        btnSendOTP.Visible = true;
        if (count > 0)
        {
            count -= 1;
            ViewState["count"] = count;
        }
        BindData();
        btnApprove.Enabled = true;
        btnReject.Enabled = true;
    }
    protected void ddlAuthenType_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnApprove.Enabled = false;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
    }
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        try
        {

            btnSendOTP.Text = "ReSend";
            ddlAuthenType.Enabled = false;
            btnApprove.Enabled = true;
            if (ddlAuthenType.SelectedValue.ToString() == "ESMSOTP")
            {
                SmartPortal.SEMS.OTP.SendSMSOTP(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            }
            else
            {
                SmartPortal.SEMS.OTP.SendSMSOTPCORP(Session["userID"].ToString(), ddlAuthenType.SelectedValue.ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                lbnotice.Visible = true;
            }
            switch (IPCERRORCODE)
            {
                case "0":
                    int time;
                    if (ddlAuthenType.SelectedValue.ToString() == "ESMSOTP")
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
                    lblError.Text = "User does not register SMS OTP";
                    break;
                default:
                    lblError.Text = IPCERRORDESC;
                    break;
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
    protected void SendEmail()
    {
        //QuanNN 07/05/2019 add send reject mail:
        //get infor to send mail:
        try
        {
            string amountchu = string.Empty;
            amountchu = string.Format("{0} {1}", SmartPortal.Common.Utilities.Utility.NumtoWords(double.Parse(lblamountchu.Text.Trim())), lblCCYID.Text);

            string tranName = string.Empty;

            DataSet ds = new DataSet();
            ds = new SmartPortal.IB.Transactions().GetApprovalTranByTranID(lblTransID.Text,Session["UserID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

            string contractNo = new SmartPortal.SEMS.User().GetUBID(txusertosend.Text.Trim()).Rows[0]["ContractNo"].ToString();

            List<DataTable> result = ds.Tables[2].AsEnumerable()
                       .GroupBy(row => row.Field<string>("WORKFLOWID"))
                       .Select(g => g.CopyToDataTable())
                       .ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add(IPC.GROUPID, typeof(string));

            //Get group by contractno
            DataSet dsGroup = new User().GetUserGroupByContractNo(contractNo, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

            //Get groupID in formula
            foreach (DataTable dtResult in result)
            {
                foreach (DataRow row in dtResult.Rows)
                {
                    string formula = row["FORMULA"].ToString();
                    Regex regex = new Regex(@"[\d]{1,2}[A-Z]{1}");
                    var matches = regex.Matches(formula);
                    if (matches.Count > 0)
                    {
                        List<string> lsCon = matches.Cast<Match>().Select(x => x.Value).Distinct().ToList();

                        foreach (string match in lsCon)
                        {
                            regex = new Regex(@"[A-Z]{1}");
                            matches = regex.Matches(match);
                            if (matches.Count > 0)
                            {
                                DataRow drGroupID = dt.NewRow();
                                drGroupID[IPC.GROUPID] = matches.Cast<Match>().Select(x => x.Value).Distinct().ToList()[0];
                                dt.Rows.Add(drGroupID);
                            }
                        }
                    }
                }
            }

            dt = dt.AsDataView().ToTable(true, IPC.GROUPID);


            DataSet dsTran = new DataSet();
            dsTran = new SmartPortal.SEMS.Transactions().GetTranByTranID(lblTransID.Text, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

            DataTable dtUser = new DataTable();
            dtUser.Columns.Add(IPC.USERID, typeof(string));

            //Add user maker
            DataRow drUs = dtUser.NewRow();
            drUs[IPC.USERID] = dsTran.Tables[0].Rows[0][IPC.USERID].ToString();
            dtUser.Rows.Add(drUs);


            foreach (DataRow dr in dsGroup.Tables[0].Rows)
            {
                if (dt.AsEnumerable().Where(x => x[IPC.GROUPID].ToString().Equals(dr[IPC.GROUPID].ToString())).FirstOrDefault() != null)
                {
                    DataRow drUser = dtUser.NewRow();
                    drUser[IPC.USERID] = dr[IPC.USERID];
                    dtUser.Rows.Add(drUser);
                }
            }

            DataTable dtTran = ds.Tables[0];

            DataSet dsTranApp = new SmartPortal.SEMS.Transactions().LoadTranApp(ref IPCERRORCODE, ref IPCERRORDESC);

            foreach (DataRow dr in dsTranApp.Tables[0].Rows)
            {
                if (dr[IPC.TRANCODE].ToString().Equals(dtTran.Rows[0][IPC.IPCTRANCODE].ToString()))
                {
                    tranName = dr[IPC.PAGENAME].ToString();
                    break;
                }
            }

            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("MatrixApprovalNotification" + CultureInfo.CurrentCulture.Name);

            tmpl.SetAttribute("message", "Transaction has been rejected by user " + Session["userID"].ToString());
            tmpl.SetAttribute("tranName", tranName);
            tmpl.SetAttribute("tranID", lblTransID.Text);
            tmpl.SetAttribute("tranDate", dtTran.Rows[0]["IPCTRANSDATE"].ToString());
            tmpl.SetAttribute("senderAccount", lblAccount.Text);
            tmpl.SetAttribute("amount", lblAmount.Text);
            tmpl.SetAttribute("amountchu", amountchu);
            tmpl.SetAttribute("desc", txtDesc.Text);

            if (dtUser.Rows.Count > 0)
            {
                foreach (DataRow row in dtUser.Rows)
                {
                    string email = new SmartPortal.SEMS.User().GetUBID(row[IPC.USERID].ToString()).Rows[0]["EMAIL"].ToString();

                    SmartPortal.Common.EmailHelper.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"],
                        email,
                        ConfigurationManager.AppSettings["contractapprovemailtitle"],
                        tmpl.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString() + "Error when reject", Request.Url.Query);
            //SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            lblError.Text = "Transactions have been rejected but can't send mail!";
        }
    }
    protected void TopupTransactionSuccess_SendMail()
    {
        try
        {
            DataTable userInfo = new DataTable();
            SmartPortal.Common.EmailHelper.GetListUserSendMail(lblTransID.Text, Session["userID"].ToString(), ref userInfo);
            string email = userInfo.Rows[0][IPC.EMAIL].ToString();

            Hashtable hasSender = new SmartPortal.IB.Account().loadInfobyAcct(lblAccount.Text.Trim());
            DataTable dt = new DataTable();
            SmartPortal.Common.EmailHelper.GetInforsenMail(hdTranCode.Value, SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblTransID.Text.Trim()), ref dt);

            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("BuyETopUpSucsessfull" + CultureInfo.CurrentCulture.Name);
            tmpl.SetAttribute("senderAccount", lblAccount.Text);
            tmpl.SetAttribute("senderBalance", SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), hasSender[SmartPortal.Constant.IPC.CCYID].ToString()));
            tmpl.SetAttribute("ccyid", lblCCYID.Text);
            tmpl.SetAttribute("status", Resources.labels.thanhcong);
            tmpl.SetAttribute("senderName", lblSenderName.Text);
            tmpl.SetAttribute("telecomname", dt.Rows[0]["TELCONAME"].ToString());
            tmpl.SetAttribute("cardamount", lblAmount.Text);
            tmpl.SetAttribute("amount", SmartPortal.Common.Utilities.Utility.FormatMoney(lblAmount.Text, lblCCYID.Text));
            tmpl.SetAttribute("amountchu", string.Format("{0} {1}", SmartPortal.Common.Utilities.Utility.NumtoWords(double.Parse(lblamountchu.Text.Trim())), lblCCYID.Text));
            tmpl.SetAttribute("feeType", lblCCYIDPhi.Text);
            tmpl.SetAttribute("feeAmount", lblFee.Text);
            tmpl.SetAttribute("desc", lblDesc.Text);
            tmpl.SetAttribute("tranID", lblTransID.Text);
            tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            tmpl.SetAttribute("phoneNo", dt.Rows[0]["PHONENO"].ToString());
            DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(SmartPortal.Common.Utilities.Utility.FormatStringCore(txsenderbranch.Text), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtSenderBranch = dsSenderBranch.Tables[0];
                if (dtSenderBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("senderBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("senderBranch", "");
            }

            //lay chi nhanh nhan
            DataSet dsReceiverBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(SmartPortal.Common.Utilities.Utility.FormatStringCore(txreceiverbranch.Text), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtReceiverBranch = dsReceiverBranch.Tables[0];
                if (dtReceiverBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("receiverBranch", dtReceiverBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("receiverBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("receiverBranch", "");
            }
            SmartPortal.Common.EmailHelper.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            lblError.Text = "Transfer success but can't send mail!";
        }
    }
    protected void BillPaymentSuccess_SendMail()
    {
        try
        {
            DataTable userInfo = new DataTable();
            SmartPortal.Common.EmailHelper.GetListUserSendMail(lblTransID.Text, Session["userID"].ToString(), ref userInfo);
            string email = userInfo.Rows[0][IPC.EMAIL].ToString();

            Hashtable hasSender = new SmartPortal.IB.Account().loadInfobyAcct(lblAccount.Text.Trim());

            DataTable dt = new DataTable();
            SmartPortal.Common.EmailHelper.GetInforsenMail(hdTranCode.Value, SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblTransID.Text.Trim()), ref dt);

            string sIPCERRORCODE = "";
            string sIPCERRORDESC = "";
            DataTable dtCorp = new SmartPortal.IB.Payment().GetCorpList(ref sIPCERRORCODE, ref sIPCERRORDESC);

            DataTable dtService = new SmartPortal.IB.Payment().GetServicebyCorpID(dt.Rows[0]["CORPID"].ToString(), ref sIPCERRORCODE, ref sIPCERRORDESC);

            DataTable dtServiceInformation = new SmartPortal.IB.Payment().GetServiceInformation((dtService.Select("SERID = '" + dt.Rows[0]["SERID"].ToString() + "'"))[0]["SERID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

            double fee = 0;
            DataTable dtfee = new SmartPortal.IB.Payment().GetFee((dtService.Select("SERID = '" + dt.Rows[0]["SERID"].ToString() + "'"))[0]["SERID"].ToString(),
                SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblAccount.Text, lblCCYID.Text.Trim()),
                lblAccount.Text, dt.Rows[0]["REFVA1"].ToString(), dt.Rows[0]["REFVA2"].ToString(), "", ref sIPCERRORCODE, ref sIPCERRORDESC);
            if (sIPCERRORCODE.Equals("0") && dtfee.Rows[0][0].ToString() != "")
            {
                fee += Double.Parse(dtfee.Rows[0][SmartPortal.Constant.IPC.CREFEE].ToString());
                fee += Double.Parse(dtfee.Rows[0][SmartPortal.Constant.IPC.DEBITFEE].ToString());
            }

            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("BillPaymentSuccessful" + CultureInfo.CurrentCulture.Name);

            tmpl.SetAttribute("senderAccount", lblAccount.Text);
            tmpl.SetAttribute("senderBalance", SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), hasSender[SmartPortal.Constant.IPC.CCYID].ToString()));
            tmpl.SetAttribute("ccyid", lblCCYID.Text);
            tmpl.SetAttribute("status", Resources.labels.thanhcong);
            tmpl.SetAttribute("senderName", hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString());
            tmpl.SetAttribute("amount", SmartPortal.Common.Utilities.Utility.FormatMoney(lblAmount.Text, lblCCYID.Text));
            tmpl.SetAttribute("amountchu", string.Format("{0} {1}", SmartPortal.Common.Utilities.Utility.NumtoWords(double.Parse(lblamountchu.Text.Trim())), lblCCYID.Text));
            tmpl.SetAttribute("feeType", lblCCYIDPhi.Text);
            tmpl.SetAttribute("feeAmount", SmartPortal.Common.Utilities.Utility.FormatMoney(fee.ToString(), lblCCYID.Text));
            tmpl.SetAttribute("desc", lblDesc.Text);
            tmpl.SetAttribute("tranID", lblTransID.Text);
            tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            tmpl.SetAttribute("serviceName", (dtService.Select("SERID = '" + dt.Rows[0]["SERID"].ToString() + "'"))[0]["SERNAME"].ToString());
            tmpl.SetAttribute("corpName", (dtCorp.Select("CORPID = '" + dt.Rows[0]["CORPID"].ToString() + "'"))[0]["CORPNAME"].ToString());
            tmpl.SetAttribute("refvalue1", dt.Rows[0]["REFVA1"].ToString());
            tmpl.SetAttribute("refvalue2", dt.Rows[0]["REFVA2"].ToString());
            if (dtServiceInformation.Rows.Count != 0)
            {
                tmpl.SetAttribute("refindex1", dtServiceInformation.Rows[0]["REFNAME1"].ToString());
                tmpl.SetAttribute("refindex2", dtServiceInformation.Rows[0]["REFNAME2"].ToString());
            }
            else
            {
                tmpl.SetAttribute("refindex1", "");
                tmpl.SetAttribute("refindex2", "");
            }
            DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.BRANCHID].ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtSenderBranch = dsSenderBranch.Tables[0];
                if (dtSenderBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("senderBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("senderBranch", "");
            }
            SmartPortal.Common.EmailHelper.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            lblError.Text = "Transfer success but can't send mail!";
        }
    }
    protected void CR_TransactionSuccess_SendMail()
    {
        try
        {
            DataTable userInfo = new DataTable();
            SmartPortal.Common.EmailHelper.GetListUserSendMail(lblTransID.Text, Session["userID"].ToString(), ref userInfo);
            string email = userInfo.Rows[0][IPC.EMAIL].ToString();

            Hashtable hasSender = new SmartPortal.IB.Account().loadInfobyAcct(lblAccount.Text.Trim());
            DataTable dt = new DataTable();
            SmartPortal.Common.EmailHelper.GetInforsenMail(hdTranCode.Value, SmartPortal.Common.Utilities.Utility.KillSqlInjection(lblTransID.Text.Trim()), ref dt);

            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("CR_TransactionSuccessful" + CultureInfo.CurrentCulture.Name);

            tmpl.SetAttribute("senderAccount", lblAccount.Text);
            tmpl.SetAttribute("senderBalance", SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), hasSender[SmartPortal.Constant.IPC.CCYID].ToString()));
            tmpl.SetAttribute("ccyid", lblCCYID.Text);
            tmpl.SetAttribute("status", Resources.labels.thanhcong);
            tmpl.SetAttribute("senderName", lblSenderName.Text);
            tmpl.SetAttribute("cardNo", dt.Rows[0]["CARDNO"].ToString());
            tmpl.SetAttribute("cardholdername", dt.Rows[0]["CARDHOLDERNAME"].ToString());
            tmpl.SetAttribute("outstandingamount", dt.Rows[0]["OUTSTANDINGAMOUNT"].ToString());
            tmpl.SetAttribute("amount", SmartPortal.Common.Utilities.Utility.FormatMoney(lblAmount.Text, lblCCYID.Text));
            tmpl.SetAttribute("amountchu", string.Format("{0} {1}", SmartPortal.Common.Utilities.Utility.NumtoWords(double.Parse(lblamountchu.Text.Trim())), lblCCYID.Text));
            tmpl.SetAttribute("feeType", lblCCYIDPhi.Text);
            tmpl.SetAttribute("feeAmount", lblFee.Text);
            tmpl.SetAttribute("desc", lblDesc.Text);
            tmpl.SetAttribute("tranID", lblTransID.Text);
            tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(SmartPortal.Common.Utilities.Utility.FormatStringCore(txsenderbranch.Text), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtSenderBranch = dsSenderBranch.Tables[0];
                if (dtSenderBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("senderBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("senderBranch", "");
            }

            SmartPortal.Common.EmailHelper.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            lblError.Text = "Transfer success but can't send mail!";
        }
    }
    protected void TransactionSuccess_SendMail()
    {
        try
        {
            DataTable userInfo = new DataTable();
            SmartPortal.Common.EmailHelper.GetListUserSendMail(lblTransID.Text, Session["userID"].ToString(), ref userInfo);
            string email = userInfo.Rows[0][IPC.EMAIL].ToString();

            Hashtable hasSender = new SmartPortal.IB.Account().loadInfobyAcct(lblAccount.Text.Trim());

            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetEmailTemplate("TransactionSuccessful" + CultureInfo.CurrentCulture.Name);

            tmpl.SetAttribute("senderAccount", lblAccount.Text);
            tmpl.SetAttribute("senderBalance", SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), hasSender[SmartPortal.Constant.IPC.CCYID].ToString()));
            tmpl.SetAttribute("ccyid", lblCCYID.Text);
            tmpl.SetAttribute("status", Resources.labels.thanhcong);
            tmpl.SetAttribute("senderName", lblSenderName.Text);
            tmpl.SetAttribute("recieverAccount", lblAccountNo.Text);
            tmpl.SetAttribute("recieverName", lblReceiverName.Text);
            tmpl.SetAttribute("amount", lblAmount.Text);
            tmpl.SetAttribute("amountchu", string.Format("{0} {1}", SmartPortal.Common.Utilities.Utility.NumtoWords(double.Parse(lblamountchu.Text.Trim())), lblCCYID.Text));
            tmpl.SetAttribute("feeType", lblCCYIDPhi.Text);
            tmpl.SetAttribute("feeAmount", lblFee.Text);
            tmpl.SetAttribute("desc", lblDesc.Text);
            tmpl.SetAttribute("tranID", lblTransID.Text);
            tmpl.SetAttribute("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            DataSet dsSenderBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(SmartPortal.Common.Utilities.Utility.FormatStringCore(txsenderbranch.Text), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtSenderBranch = dsSenderBranch.Tables[0];
                if (dtSenderBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("senderBranch", dtSenderBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("senderBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("senderBranch", "");
            }

            //lay chi nhanh nhan
            DataSet dsReceiverBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(SmartPortal.Common.Utilities.Utility.FormatStringCore(txreceiverbranch.Text), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtReceiverBranch = dsReceiverBranch.Tables[0];
                if (dtReceiverBranch.Rows.Count != 0)
                {
                    tmpl.SetAttribute("receiverBranch", dtReceiverBranch.Rows[0]["BRANCHNAME"].ToString());
                }
                else
                {
                    tmpl.SetAttribute("receiverBranch", "");
                }
            }
            else
            {
                tmpl.SetAttribute("receiverBranch", "");
            }
            SmartPortal.Common.EmailHelper.SendMailMessageAsync(ConfigurationManager.AppSettings["contractapprovemailfrom"], email, ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            lblError.Text = "Transfer success but can't send mail!";
        }
    }

    protected void rptDocument_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //LinkButton HyperLinkDocument;
        //HyperLinkDocument = (LinkButton)rptDocument.FindControl("lblDownload");
        //HyperLinkDocument.Attributes.Add("target", "_blank");
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
    //public Control FindControlRecursive(Control Root, string Id)
    //{
    //    try
    //    {
    //        if (Root.ID == Id)
    //            return Root;
    //        foreach (Control Ctl in Root.Controls)

    //        {

    //            Control FoundCtl = FindControlRecursive(Ctl, Id);

    //            if (FoundCtl != null)

    //                return FoundCtl;

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
    //        SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
    //    }
    //    return null;
    //}

    //public IEnumerable<Control> FindControlRecursive(Control Root, string Id)
    //{
    //    try
    //    {
    //        var results = new List<Control>();
    //        if (Root.ID == Id)
    //            results.Add(Root);
    //        foreach (Control Ctl in Root.Controls)
    //        {
    //            results.AddRange(FindControlRecursive(Ctl, Id));
    //        }
    //        return results;
    //    }
    //    catch (Exception ex)
    //    {
    //        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
    //        SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
    //    }
    //    return null;
    //}
    //protected void rptDocument_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    try
    //    {
    //        int a = e.Item.ItemIndex;
    //        if (!(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
    //        {
    //            return;
    //        }
    //        //LinkButton linkButton = FindControlRecursive(rptDocument, "lbDownload") as LinkButton;
    //        IEnumerable<Control> listlink = FindControlRecursive(rptDocument, "lbDownload");
    //        foreach(var Linkbutton in listlink)
    //        {
    //            var scriptManager = ScriptManager.GetCurrent(this.Page);
    //            if (scriptManager != null)
    //            {
    //                scriptManager.RegisterPostBackControl(Linkbutton);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
    //        SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
    //    }
    //}
}
