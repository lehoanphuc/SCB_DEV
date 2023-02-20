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
using SmartPortal.IB;
using System.Globalization;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;
using SmartPortal.SEMS;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using QRCoder;
using System.Drawing;

public partial class Widgets_SEMSViewLogTransactions_ViewDetails_Widget : WidgetBase
{
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : new DataTable(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string BILLERNAME = string.Empty;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
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
            DataSet dsDocument = new DataSet();
            dsDocument = new SmartPortal.SEMS.Transactions().GetDocumentByTranID(tranID, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
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
                    if (ds.Tables[5].Rows.Count != 0)
                    {
                        for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                        {
                            string dctype = ds.Tables[5].Rows[i]["DocumentType"].ToString();
                            string dcname = "Document " + ds.Tables[5].Rows[i]["ID"].ToString();
                            string base64 = ds.Tables[5].Rows[i]["LINK"].ToString();
                            dtDocument.Rows.Add(dcname, dctype, base64);
                        }

                        pnlDocument.Visible = true;
                        rptDocument.DataSource = dtDocument;
                        rptDocument.DataBind();
                        ViewState["TBLDOCUMENT"] = dtDocument;
                    }
                }
                if (dt.Rows.Count != 0)
                {
                    pnDefault.Visible = true;

                    lblTransID.Text = dt.Rows[0]["IPCTRANSID"].ToString();
                    lblAccountSender.Text = SmartPortal.Common.Utilities.Utility.FormatStringCore(dt.Rows[0]["CHAR01"].ToString());
                    lblAccountReceiver.Text = dt.Rows[0]["CHAR02"].ToString();
                    lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim());
                    string ccyid = dt.Rows[0]["CCYID"].ToString();
                    lblstbc.Text = SmartPortal.Common.Utilities.Utility.NumtoWords(Convert.ToDouble(lblAmount.Text, new CultureInfo("en-US").NumberFormat));
                    lblCCYID.Text = ccyid;
                    lblPagename.Text = dt.Rows[0]["PageName"].ToString();
                    lblReftype.Text = dt.Rows[0]["CHAR20"].ToString();
                    lblTelco.Text = dt.Rows[0]["TELCONAME"].ToString();
                    lblPhone.Text = dt.Rows[0]["CHAR07"].ToString().Trim();
                    lblCardAmount.Text = lblAmount.Text + " " + lblCCYID.Text;
                    BILLERNAME = dt.Rows[0]["CHAR23"].ToString().Trim();
                    if (BILLERNAME.Equals("ETP"))
                    {                     
                        Hashtable Databill = new Hashtable();
                        if (!String.IsNullOrEmpty(dt.Rows[0]["CHAR28"].ToString()) && JArray.Parse(dt.Rows[0]["CHAR28"].ToString().Replace("\r\n", "").Replace("\\", "")).Count > 0)
                        {
                            Session["printbill"] = AddPrintBill(Databill, dt.Rows[0]["IPCTRANSID"].ToString(), SmartPortal.Common.Utilities.Utility.IsDateTime2(dt.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy"), SmartPortal.Common.Utilities.Utility.FormatStringCore(dt.Rows[0]["CHAR01"].ToString()), dt.Rows[0]["FULLNAME"].ToString().Trim(), dt.Rows[0]["CCYID"].ToString().Trim(),  dt.Rows[0]["CHAR25"].ToString().Trim(), JArray.Parse(dt.Rows[0]["CHAR28"].ToString().Replace("\r\n", "").Replace("\\", "")));
                            btnPrintBill.Visible = true;
                        }
                    }

                    if (dt.Rows[0]["ISBATCH"].ToString() == "Y")
                    {
                        string recieverAccount = dt.Rows[0]["CHAR02"].ToString();
                        lblAccountReceiver.Text = recieverAccount.Substring(0, 4) + "*****" + recieverAccount.Substring(recieverAccount.Length - 4, 4);
                        lblAmount.Text = "*****";
                        lblstbc.Text = "*****";
                    }

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

                    string f = (feeNum).ToString();
                    string v = (0).ToString();
                    lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(f, ccyid);
                    lblVAT.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(v, dt.Rows[0]["CCYID"].ToString().Trim());

                    if (!dt.Rows[0]["CHAR02"].ToString().Equals("") || !dt.Rows[0]["CHAR04"].ToString().Equals(""))
                    {
                        pnReceiver.Visible = true;
                        if (dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("IBINTERBANKTRANSFER")
                            || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("MB_TRANSFEROTHBANK") || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("IBCBTRANSFER") || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("MBCBTRANSFER") || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("INTERBANK247") || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("MBINTERBANK247QR") || dt.Rows[0]["IPCTRANCODE"].ToString().Trim().Equals("MBINTERBANK247"))
                        {
                            lblReceiver.Text = Resources.labels.taikhoanbaoco;
                            lblAccountReceiver.Text = dt.Rows[0]["CHAR04"].ToString();
                        }
                        else
                        {
                            lblReceiver.Text = dt.Rows[0]["CHAR02"].ToString().Length < 13 ? "Receiver Phone" : Resources.labels.taikhoanbaoco;
                        }
                      
                    }
                    else
                    {
                        pnReceiver.Visible = false;
                    }
                    pnTopup.Visible = !dt.Rows[0]["TELCONAME"].ToString().Equals("");
                    if (!dt.Rows[0]["CHAR01"].ToString().Equals(""))
                    {
                        pnSender.Visible = true;
                        lblSender.Text = dt.Rows[0]["CHAR01"].ToString().Length < 13 ? "Sender Phone" : Resources.labels.debitaccount;
                    }
                    else
                    {
                        pnSender.Visible = false;
                    }
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
                            lblUserCreate.Text = string.IsNullOrEmpty(ds.Tables[3].Rows[0][0].ToString())? dt.Rows[0]["USERID"].ToString().Trim() : ds.Tables[3].Rows[0][0].ToString();
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
    private Hashtable AddPrintBill(Hashtable hasPrint, string EZY_TransID, string EZY_Date, string EZY_AccountNo, string EZY_AccountName, string EZY_CCYID, string EZY_Tincode, JArray EZY_PAYMENTMORE)
    {
        hasPrint["EZY_TransID"] = EZY_TransID;
        hasPrint["EZY_Date"] = EZY_Date;
        hasPrint["EZY_AccountNo"] = EZY_AccountNo;
        hasPrint["EZY_AccountName"] = EZY_AccountName;
        hasPrint["EZY_CCYID"] = EZY_CCYID;
        hasPrint["EZY_Tincode"] = EZY_Tincode;
        hasPrint["EZY_PAYMENTMORE"] = EZY_PAYMENTMORE;
 

        return hasPrint;
    }
    private Hashtable AddPrint(Hashtable hasPrint, string file, string value)
    {
        hasPrint[file] = value;
        return hasPrint;
    }


    protected void btback_OnClickck_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void rptDocument_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        HyperLink HyperLinkDocument;
        HyperLinkDocument = (HyperLink)rptDocument.FindControl("HyperLinkDocument");
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
