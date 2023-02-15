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

public partial class Widgets_IBViewLogTransactions_ViewDetails_Widget : WidgetBase
{
    string ACTION = "";
    string cn = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string content = "";
    DataSet ApproveContractTable = new DataSet();

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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBListTransWaitApprove_ViewDetails_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    void BindData()
    {
        #region Lấy thông tin giao dịch
        try
        {
            string tranID = "";
            Hashtable hasPrint = new Hashtable();

            if (Session["tranID"] == null)
            {
                tranID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tranid"].ToString();
            }
            else
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranID"];

                tranID = lstTran[0];
            }

            #region Trường hợp session null
            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.Transactions().GetTranByTranID(tranID, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }

            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["IPCTRANCODE"].ToString().Trim() == "IB000300")
                {
                    pnOTK.Visible = true;
                    pnDefault.Visible = false;
                    pnCTK.Visible = false;

                    lblTransID_OTK.Text = dt.Rows[0]["IPCTRANSID"].ToString();
                    //hasPrint["tranID"] = lblTransID_OTK.Text;

                    lblAccount_OTK.Text = SmartPortal.Common.Utilities.Utility.FormatStringCore(dt.Rows[0]["CHAR01"].ToString());
                    //hasPrint["debitAccount"] = lblAccount_OTK.Text;



                    lblAccountNo_OTK.Text = dt.Rows[0]["CHAR02"].ToString();
                    //hasPrint["creditAccount"] = lblAccountNo_OTK.Text;

                    lblAmount_OTK.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim());
                    //hasPrint["amount"] = lblAmount_OTK.Text;
                    string ccyid = dt.Rows[0]["CCYID"].ToString();
                    double sotienbangchu = SmartPortal.Common.Utilities.Utility.isDouble(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblAmount_OTK.Text, ccyid), false);
                    lblstbc_OTK.Text = SmartPortal.Common.Utilities.ConvertAmount.changeCurrencyToWords(sotienbangchu, ccyid, ccyid);
                    //hasPrint["sotienbangchu"] = lblstbc_OTK.Text;

                    lblCCYID_OTK.Text = ccyid;
                    //hasPrint["ccyid"] = lblCCYID_OTK.Text;


                    DataTable tblU = new SmartPortal.SEMS.User().GetUBID(dt.Rows[0]["USERCURAPP"].ToString().Trim());


                    if (tblU.Rows.Count != 0)
                    {
                        lblLastApp_OTK.Text = tblU.Rows[0]["FULLNAME"].ToString();
                    }

                    //hasPrint["lastapp"] = lblLastApp_OTK.Text;

                    lblCCYIDPhi_OTK.Text = dt.Rows[0]["CCYID"].ToString();
                    lblCCYIDVAT_OTK.Text = dt.Rows[0]["CCYID"].ToString();

                    lblDesc_OTK.Text = dt.Rows[0]["TRANDESC"].ToString();
                    lblDate_OTK.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(dt.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    //hasPrint["date"] = lblDate_OTK.Text;
                    string[] a = dt.Rows[0]["TRANDESC"].ToString().Split('|');
                    if (a.Length > 1)
                    {
                        lblApproveDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(a[1].ToString()).ToString("dd/MM/yyyy HH:mm");

                    }

                    lblUserCreate_OTK.Text = dt.Rows[0]["FULLNAME"].ToString();
                    lblAppSts_OTK.Text = dt.Rows[0]["APPSTATUS"].ToString();

                    lblDO_FD.Text = dt.Rows[0]["CHAR08"].ToString();
                    lblLT_FD.Text = dt.Rows[0]["CHAR09"].ToString();
                    lblIR_FD.Text = dt.Rows[0]["CHAR07"].ToString();

                    double feeNum = SmartPortal.Common.Utilities.Utility.isDouble(dt.Rows[0]["NUM02"].ToString(), false);

                    //string f = (feeNum / SmartPortal.Common.Utilities.Utility.isDouble("1.1", true)).ToString();
                    //string v = (feeNum - (feeNum / SmartPortal.Common.Utilities.Utility.isDouble("1.1", true))).ToString();

                    lblFee_OTK.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(feeNum.ToString(), ccyid);
                    // hasPrint["feeAmount"] = lblFee_OTK.Text;

                    //Haint edit 15/1/2014
                    lblVAT_OTK.Text = SmartPortal.Common.Utilities.Utility.FormatMoney("0", ccyid);
                    //hasPrint["VATAmount"] = lblVAT_OTK.Text;

                    double ldh = SmartPortal.Common.Utilities.Utility.isDouble(dt.Rows[0]["NUM10"].ToString(), false);
                    lblLDH.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ldh.ToString(), ccyid);
                    //lblCCYIDLDH_OTK.Text = dt.Rows[0]["CCYID"].ToString().Trim();
                    //hasPrint["LDH"] = lblLDH_OTK.Text;

                    //load bank
                    //DataTable tblBank = new SmartPortal.SEMS.Bank().LoadByBankCode(dt.Rows[0]["CHAR05"].ToString());
                    //if (tblBank.Rows.Count != 0)
                    //{
                    //    lblBank_OTK.Text = tblBank.Rows[0]["BANKNAME"].ToString();
                    //    lbltinhthanh_OTK.Text = tblBank.Rows[0]["CITYNAME"].ToString();
                    //}

                    switch (dt.Rows[0]["STATUS"].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                            lblStatus_OTK.Text = Resources.labels.dangxuly1;
                            break;
                        case SmartPortal.Constant.IPC.TRANSTATUS.PAYMENTFAIL:
                            lblStatus_OTK.Text = Resources.labels.thanhtoanthatbai;
                            break;
                        case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                            lblStatus_OTK.Text = Resources.labels.hoanthanh;
                            switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
                            {
                                case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                    lblResult_OTK.Text = Resources.labels.duyet;

                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                    lblResult_OTK.Text = Resources.labels.khongduyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                    lblResult_OTK.Text = Resources.labels.choduyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                    lblResult_OTK.Text = Resources.labels.dangxuly1;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                    lblResult_OTK.Text = Resources.labels.dahoantien;
                                    break;

                            }
                            break;
                        case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
                            lblStatus_OTK.Text = Resources.labels.choduyet;
                            switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
                            {
                                case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                    lblResult_OTK.Text = Resources.labels.duyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                    lblResult_OTK.Text = Resources.labels.huy;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                    lblResult_OTK.Text = Resources.labels.choduyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                    lblResult_OTK.Text = Resources.labels.dangxuly1;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                    lblResult_OTK.Text = Resources.labels.dahoantien;
                                    break;

                            }
                            break;
                        case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
                            lblStatus_OTK.Text = Resources.labels.loi;
                            switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
                            {
                                case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                    lblResult_OTK.Text = Resources.labels.duyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                    lblResult_OTK.Text = Resources.labels.huy;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                    lblResult_OTK.Text = Resources.labels.choduyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                    lblResult_OTK.Text = Resources.labels.dangxuly1;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                    lblResult_OTK.Text = Resources.labels.dahoantien;
                                    break;

                            }
                            break;
                        case SmartPortal.Constant.IPC.TRANSTATUS.REJECTED:
                            lblStatus_OTK.Text = Resources.labels.khongduyet;
                            break;

                    }

                    //get detail
                    DataTable dtLogTranDetail = new SmartPortal.SEMS.Transactions().GetLogTranDetail(tranID);
                    foreach (DataRow r in dtLogTranDetail.Rows)
                    {
                        switch (r["FIELDNAME"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.RECEIVERNAME:
                                lblReceiverName_OTK.Text = r["FIELDVALUE"].ToString().Trim();
                                break;
                            case SmartPortal.Constant.IPC.LICENSE:
                                //lblLicense_OTK.Text = r["FIELDVALUE"].ToString().Trim();
                                break;
                            case SmartPortal.Constant.IPC.CREATEDATE:
                                //lblIssueDate_OTK.Text = r["FIELDVALUE"].ToString().Trim();
                                break;
                            case SmartPortal.Constant.IPC.PLACE:
                                //lblIssuePlace_OTK.Text = r["FIELDVALUE"].ToString().Trim();
                                break;
                            case SmartPortal.Constant.IPC.RECEIVERADD:
                                //lblReceiverAdd_OTK.Text = r["FIELDVALUE"].ToString().Trim();
                                break;
                            case SmartPortal.Constant.IPC.SENDERNAME:
                                lblSenderName_OTK.Text = r["FIELDVALUE"].ToString().Trim();
                                break;
                            case SmartPortal.Constant.IPC.TERM:
                                lblTerm_OTK.Text = r["FIELDVALUE"].ToString().Trim();
                                break;
                            case SmartPortal.Constant.IPC.BRANCHID:

                                DataSet dsBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(r["FIELDVALUE"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                                if (dsBranch.Tables.Count != 0)
                                {
                                    if (ds.Tables[0].Rows.Count != 0)
                                    {
                                        lblBranch_DD.Text = dsBranch.Tables[0].Rows[0]["BRANCHNAME"].ToString();
                                    }
                                }
                                break;
                            case SmartPortal.Constant.IPC.PRODUCTID:

                                DataTable tblProduct = new SmartPortal.IB.FD().LoadFDProduct(r["FIELDVALUE"].ToString().Trim());

                                if (tblProduct.Rows.Count != 0)
                                {
                                    lblAccountName_FD.Text = tblProduct.Rows[0]["FDPRODUCTNAME"].ToString();
                                }

                                break;
                        }
                    }
                    if (lblSenderName_OTK.Text == "" && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        lblSenderName_OTK.Text = ds.Tables[2].Rows[0][0].ToString();
                    }

                    hasPrint.Add("status", lblStatus_OTK.Text);
                    hasPrint.Add("senderAccount", lblAccount_OTK.Text);
                    hasPrint.Add("product", lblAccountName_FD.Text);
                    hasPrint.Add("term", lblTerm_OTK.Text);
                    hasPrint.Add("rate", lblIR_FD.Text);
                    hasPrint.Add("openDate", lblDO_FD.Text);
                    hasPrint.Add("expireDate", lblLT_FD.Text);
                    hasPrint.Add("FDBalance", lblAmount_OTK.Text);

                    hasPrint.Add("receiverAccount", lblAccountNo_OTK.Text);
                    hasPrint.Add("amount", lblAmount_OTK.Text);
                    hasPrint.Add("ccyid", lblCCYID_OTK.Text);
                    hasPrint.Add("amountchu", lblstbc_OTK.Text);

                    hasPrint.Add("senderName", lblSenderName_OTK.Text);

                    hasPrint.Add("tranID", lblTransID_OTK.Text);
                    hasPrint.Add("tranDate", lblDate_OTK.Text);
                    hasPrint.Add("senderBranch", lblBranch_DD.Text);
                    hasPrint.Add("receiverBranch", "");
                    Session["print"] = hasPrint;
                }
                else
                {
                    if (dt.Rows[0]["IPCTRANCODE"].ToString().Trim() == "IB000301")
                    {
                        pnOTK.Visible = false;
                        pnDefault.Visible = false;
                        pnCTK.Visible = true;

                        lblTransID_CTK.Text = dt.Rows[0]["IPCTRANSID"].ToString();
                        //hasPrint["tranID"] = lblTransID_CTK.Text;

                        lblAccount_CTK.Text = SmartPortal.Common.Utilities.Utility.FormatStringCore(dt.Rows[0]["CHAR01"].ToString());
                        //hasPrint["debitAccount"] = lblAccount_CTK.Text;



                        lblAccountNo_CTK.Text = dt.Rows[0]["CHAR02"].ToString();
                        //hasPrint["creditAccount"] = lblAccountNo_CTK.Text;
                        string ccyid = dt.Rows[0]["CCYID"].ToString().Trim();
                        lblAmount_CTK.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), ccyid);
                        //hasPrint["amount"] = lblAmount_CTK.Text;

                        double sotienbangchu = SmartPortal.Common.Utilities.Utility.isDouble(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblAmount_CTK.Text, ccyid), false);
                        //thaity modify at 25/6/2014
                        //lblstbc_CTK.Text = SmartPortal.Common.Utilities.ConvertAmount.DocTienBangChu(sotienbangchu);                        
                        lblstbc_CTK.Text = SmartPortal.Common.Utilities.ConvertAmount.changeCurrencyToWords(sotienbangchu, ccyid, ccyid);

                        //hasPrint["sotienbangchu"] = lblstbc_CTK.Text;

                        lblCCYID_CTK.Text = ccyid;
                        //hasPrint["ccyid"] = lblCCYID_CTK.Text;


                        DataTable tblU = new SmartPortal.SEMS.User().GetUBID(dt.Rows[0]["USERCURAPP"].ToString().Trim());


                        if (tblU.Rows.Count != 0)
                        {
                            lblLastApp_CTK.Text = tblU.Rows[0]["FULLNAME"].ToString();
                        }

                        //hasPrint["lastapp"] = lblLastApp_CTK.Text;

                        lblCCYIDPhi_CTK.Text = ccyid;
                        lblCCYIDVAT_CTK.Text = ccyid;

                        lblDesc_CTK.Text = dt.Rows[0]["TRANDESC"].ToString();
                        lblDate_CTK.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(dt.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                        //hasPrint["date"] = lblDate_CTK.Text;
                        string[] b = dt.Rows[0]["TRANDESC"].ToString().Split('|');
                        if (b.Length > 1)
                        {
                            lblApproveDate_1.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(b[1].ToString()).ToString("dd/MM/yyyy HH:mm");

                        }

                        lblUserCreate_CTK.Text = dt.Rows[0]["FULLNAME"].ToString();
                        lblAppSts_CTK.Text = dt.Rows[0]["APPSTATUS"].ToString();

                        lblDO_CTK.Text = dt.Rows[0]["CHAR08"].ToString();
                        lblED_CTK.Text = dt.Rows[0]["CHAR09"].ToString();
                        lblIR_CTK.Text = dt.Rows[0]["CHAR07"].ToString();

                        double feeNum = SmartPortal.Common.Utilities.Utility.isDouble(dt.Rows[0]["NUM02"].ToString(), false);

                        //string f = (feeNum / SmartPortal.Common.Utilities.Utility.isDouble("1.1", true)).ToString();
                        //string v = (feeNum - (feeNum / SmartPortal.Common.Utilities.Utility.isDouble("1.1", true))).ToString();

                        lblFee_CTK.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(feeNum.ToString(), ccyid);
                        // hasPrint["feeAmount"] = lblFee_CTK.Text;

                        //Haint edit 15/1/2014
                        lblVAT_CTK.Text = SmartPortal.Common.Utilities.Utility.FormatMoney("0", ccyid);
                        //hasPrint["VATAmount"] = lblVAT_CTK.Text;

                        double ldh = SmartPortal.Common.Utilities.Utility.isDouble(dt.Rows[0]["NUM10"].ToString(), false);
                        lblLDH_CTK.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ldh.ToString(), ccyid);
                        lblCCYIDLDH_CTK.Text = dt.Rows[0]["CCYID"].ToString().Trim();
                        hasPrint["LDH"] = lblLDH_CTK.Text;

                        //load bank
                        //DataTable tblBank = new SmartPortal.SEMS.Bank().LoadByBankCode(dt.Rows[0]["CHAR05"].ToString());
                        //if (tblBank.Rows.Count != 0)
                        //{
                        //    lblBank_CTK.Text = tblBank.Rows[0]["BANKNAME"].ToString();
                        //    lbltinhthanh_CTK.Text = tblBank.Rows[0]["CITYNAME"].ToString();
                        //}

                        switch (dt.Rows[0]["STATUS"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                                lblStatus_CTK.Text = Resources.labels.dangxuly1;
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                                lblStatus_CTK.Text = Resources.labels.hoanthanh;
                                switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
                                {
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                        lblResult_CTK.Text = Resources.labels.duyet;

                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                        lblResult_CTK.Text = Resources.labels.khongduyet;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                        lblResult_CTK.Text = Resources.labels.choduyet;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                        lblResult_CTK.Text = Resources.labels.dangxuly1;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                        lblResult_CTK.Text = Resources.labels.dahoantien;
                                        break;

                                }
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
                                lblStatus_CTK.Text = Resources.labels.choduyet;
                                switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
                                {
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                        lblResult_CTK.Text = Resources.labels.duyet;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                        lblResult_CTK.Text = Resources.labels.huy;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                        lblResult_CTK.Text = Resources.labels.choduyet;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                        lblResult_CTK.Text = Resources.labels.dangxuly1;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                        lblResult_CTK.Text = Resources.labels.dahoantien;
                                        break;

                                }
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
                                lblStatus_CTK.Text = Resources.labels.loi;
                                switch (dt.Rows[0]["APPRSTS"].ToString().Trim())
                                {
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                        lblResult_CTK.Text = Resources.labels.duyet;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                        lblResult_CTK.Text = Resources.labels.huy;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                        lblResult_CTK.Text = Resources.labels.choduyet;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                        lblResult_CTK.Text = Resources.labels.dangxuly1;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                        lblResult_CTK.Text = Resources.labels.dahoantien;
                                        break;

                                }
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.REJECTED:
                                lblStatus_CTK.Text = Resources.labels.khongduyet;
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.PAYMENTFAIL:
                                lblStatus_CTK.Text = Resources.labels.thanhtoanthatbai;
                                break;
                        }

                        //get detail
                        DataTable dtLogTranDetail = new SmartPortal.SEMS.Transactions().GetLogTranDetail(tranID);
                        foreach (DataRow r in dtLogTranDetail.Rows)
                        {
                            switch (r["FIELDNAME"].ToString().Trim())
                            {
                                case SmartPortal.Constant.IPC.RECEIVERNAME:
                                    lblReceiverName_CTK.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.LICENSE:
                                    //lblLicense_CTK.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.CREATEDATE:
                                    //lblIssueDate_CTK.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.PLACE:
                                    //lblIssuePlace_CTK.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.RECEIVERADD:
                                    //lblReceiverAdd_CTK.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.SENDERNAME:
                                    lblSenderName_CTK.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.TERM:
                                    lblLDH_CTK.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.BRANCHID:

                                    DataSet dsBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(r["FIELDVALUE"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                                    if (dsBranch.Tables.Count != 0)
                                    {
                                        if (dsBranch.Tables[0].Rows.Count != 0)
                                        {
                                            lblBranch_CTK.Text = dsBranch.Tables[0].Rows[0]["BRANCHNAME"].ToString();
                                        }
                                    }
                                    break;
                                case SmartPortal.Constant.IPC.PRODUCTID:


                                    lblAccountName_CTK.Text = r["FIELDVALUE"].ToString().Trim();


                                    break;
                            }
                        }
                        if (lblSenderName_CTK.Text == "" && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                        {
                            lblSenderName_CTK.Text = ds.Tables[2].Rows[0][0].ToString();
                        }

                        hasPrint.Add("status", lblStatus_CTK.Text);
                        hasPrint.Add("senderAccount", lblAccount_CTK.Text);
                        hasPrint.Add("product", lblAccountName_CTK.Text);

                        hasPrint.Add("rate", lblIR_CTK.Text);
                        hasPrint.Add("openDate", lblDO_CTK.Text);
                        hasPrint.Add("expireDate", lblED_CTK.Text);
                        hasPrint.Add("FDBalance", lblAmount_CTK.Text);

                        hasPrint.Add("receiverAccount", lblAccountNo_CTK.Text);
                        hasPrint.Add("amount", lblAmount_CTK.Text);
                        hasPrint.Add("ccyid", lblCCYID_CTK.Text);
                        hasPrint.Add("amountchu", lblstbc_CTK.Text);

                        hasPrint.Add("senderName", lblSenderName_CTK.Text);

                        hasPrint.Add("tranID", lblTransID_CTK.Text);
                        hasPrint.Add("tranDate", lblDate_CTK.Text);
                        hasPrint.Add("senderBranch", lblBranch_CTK.Text);
                        hasPrint.Add("receiverBranch", "");
                        Session["print"] = hasPrint;
                    }
                        //quyen
                    else
                    {
                        pnOTK.Visible = false;
                        pnDefault.Visible = true;
                        pnCTK.Visible = false;

                        lblTransID.Text = dt.Rows[0]["IPCTRANSID"].ToString();
                        hasPrint["tranID"] = lblTransID.Text;

                        lblAccount.Text = SmartPortal.Common.Utilities.Utility.FormatStringCore(dt.Rows[0]["CHAR01"].ToString());
                        hasPrint["debitAccount"] = lblAccount.Text;

                        lblAccountNo.Text = dt.Rows[0]["CHAR02"].ToString();
                        hasPrint["creditAccount"] = lblAccountNo.Text;
                        string ccyid = dt.Rows[0]["CCYID"].ToString().Trim();
                        lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["AMOUNT"].ToString(), ccyid);
                        hasPrint["amount"] = lblAmount.Text;
                        double sotienbangchu = SmartPortal.Common.Utilities.Utility.isDouble(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblAmount.Text, "LAK"), false);
                        //thaity modify at 25/6/2014
                        //hdsotienbangchu.Value = SmartPortal.Common.Utilities.ConvertAmount.DocTienBangChu(sotienbangchu);
                        hdsotienbangchu.Value = SmartPortal.Common.Utilities.ConvertAmount.changeCurrencyToWords(sotienbangchu, ccyid, ccyid);


                        lblCCYID.Text = ccyid;
                        lblCCYIDVAT.Text = ccyid;
                        hasPrint["ccyid"] = lblCCYID.Text;

                        lblCCYIDPhi.Text = ccyid;

                        lblDesc.Text = dt.Rows[0]["TRANDESC"].ToString();
                        lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(dt.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                        hasPrint["date"] = lblDate.Text;

                        lblUserCreate.Text = dt.Rows[0]["FULLNAME"].ToString();
                        lblAppSts.Text = dt.Rows[0]["APPSTATUS"].ToString();
                        string[] c = dt.Rows[0]["TRANDESC"].ToString().Split('|');
                        if (c.Length > 1)
                        {
                            lblApproveDate_0.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(c[1].ToString()).ToString("dd/MM/yyyy HH:mm");

                        }

                        double feeNum = SmartPortal.Common.Utilities.Utility.isDouble(dt.Rows[0]["NUM02"].ToString(), false);

                        //string f = (feeNum / SmartPortal.Common.Utilities.Utility.isDouble("1.1", true)).ToString();
                        //string v = (feeNum - (feeNum / SmartPortal.Common.Utilities.Utility.isDouble("1.1", true))).ToString();

                        lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(feeNum.ToString(), ccyid);
                        hasPrint["feeAmount"] = lblFee.Text;

                        //Haint edit 15/1/2014
                        lblVAT.Text = SmartPortal.Common.Utilities.Utility.FormatMoney("0", ccyid);
                        hasPrint["VATAmount"] = lblVAT.Text;

                        double ldh = SmartPortal.Common.Utilities.Utility.isDouble(dt.Rows[0]["NUM10"].ToString(), false);
                        lblLDH.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(ldh.ToString(), ccyid);
                        lblCCYIDLDH.Text = ccyid;
                        hasPrint["LDH"] = lblLDH.Text;

                        //load bank
                        DataTable tblBank = new SmartPortal.SEMS.Bank().LoadByBankCode(dt.Rows[0]["CHAR05"].ToString());
                        if (tblBank.Rows.Count != 0)
                        {
                            lblBank.Text = tblBank.Rows[0]["BANKNAME"].ToString();
                        }

                        switch (dt.Rows[0]["STATUS"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                                lblStatus.Text = Resources.labels.dangxuly1;
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.PAYMENTFAIL:
                                lblStatus.Text = Resources.labels.thanhtoanthatbai;
                                break;
                            case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                                lblStatus.Text = Resources.labels.hoanthanh;
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
                                        lblResult.Text = Resources.labels.duyet;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                        lblResult.Text = Resources.labels.huy;
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
                                        lblResult.Text = Resources.labels.duyet;
                                        break;
                                    case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                        lblResult.Text = Resources.labels.huy;
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
                                lblStatus.Text = Resources.labels.huy;
                                break;

                        }



                        //get detail
                        DataTable dtLogTranDetail = new SmartPortal.SEMS.Transactions().GetLogTranDetail(tranID);
                        foreach (DataRow r in dtLogTranDetail.Rows)
                        {
                            switch (r["FIELDNAME"].ToString().Trim())
                            {
                                case SmartPortal.Constant.IPC.RECEIVERNAME:
                                    lblReceiverName.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.LICENSE:
                                    lblLicense.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.CREATEDATE:
                                    lblIssueDate.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.PLACE:
                                    lblIssuePlace.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.RECEIVERADD:
                                    lblReceiverAdd.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.SENDERNAME:
                                    lblSenderName.Text = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.DIRECTNUM:
                                    hdsodanhba.Value = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.ADDRESSCHOLON:
                                    hdaddresscholon.Value = r["FIELDVALUE"].ToString().Trim();
                                    break;
                                case SmartPortal.Constant.IPC.CREDITBRACHID:

                                    DataSet dsBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(SmartPortal.Common.Utilities.Utility.FormatStringCore(r["FIELDVALUE"].ToString().Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                                    if (dsBranch.Tables.Count != 0)
                                    {
                                        if (dsBranch.Tables[0].Rows.Count != 0)
                                        {
                                            hdcreditbranch.Value = dsBranch.Tables[0].Rows[0]["BRANCHNAME"].ToString();
                                        }
                                    }
                                    break;
                                case SmartPortal.Constant.IPC.DEBITBRACHID:

                                    DataSet dsDBranch = new SmartPortal.SEMS.Branch().GetBranchDetailsByID(SmartPortal.Common.Utilities.Utility.FormatStringCore(r["FIELDVALUE"].ToString().Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                                    if (dsDBranch.Tables.Count != 0)
                                    {
                                        if (dsDBranch.Tables[0].Rows.Count != 0)
                                        {
                                            hddebitbranch.Value = dsDBranch.Tables[0].Rows[0]["BRANCHNAME"].ToString();
                                        }
                                    }
                                    break;
                            }
                        }
                        if (lblSenderName.Text == "" && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                        {
                            lblSenderName.Text = ds.Tables[2].Rows[0][0].ToString();
                        }
                        //View lob bill details
                        DataSet dsBilDetails = new SmartPortal.IB.Payment().ViewlogBillDetails(tranID, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }
                        else
                        {
                            if (dsBilDetails.Tables.Count > 0)
                            {
                                if (dsBilDetails.Tables[0].Rows.Count > 0)
                                {
                                    pnBil.Visible = true;
                                    gvLTWA.DataSource = dsBilDetails.Tables[0];
                                    gvLTWA.DataBind();

                                }
                            }
                        }
                        //
                        if (content.Length > 0)
                        {
                            hasPrint["bill"] = ("<tr><td colspan='4'></tr><tr><td colspan='4'><table style='width:100%' cellspacing='0' cellpadding='5' border='1'><tr><td style='width:32%'>" + Resources.labels.sohoadon + "</td><td style='width:32%'>" + Resources.labels.sotien + "</td><td style='width:32%'>" + Resources.labels.trangthai + "</td></tr>" + content + "</table></tr>");
                        }
                        else
                        {
                            hasPrint["bill"] = "";
                        }

                        hasPrint["bank"] = lblBank.Text;
                        hasPrint["license"] = lblLicense.Text;
                        hasPrint["issueDate"] = lblIssueDate.Text;
                        hasPrint["issuePlace"] = lblIssuePlace.Text;
                        hasPrint["senderName"] = lblSenderName.Text;
                        hasPrint["receiverName"] = lblReceiverName.Text;
                        hasPrint["desc"] = lblDesc.Text;
                        hasPrint["receiverAdd"] = lblReceiverAdd.Text;
                        hasPrint["status"] = lblStatus.Text;
                        hasPrint["approver"] = lblAppSts.Text;
                        hasPrint["worker"] = lblUserCreate.Text;
                        hasPrint["custcodewater"] = hdsodanhba.Value;
                        hasPrint["senderBranch"] = hddebitbranch.Value;
                        hasPrint["receiverBranch"] = hdcreditbranch.Value;
                        hasPrint["address"] = hdaddresscholon.Value;
                        hasPrint["amountchu"] = hdsotienbangchu.Value;


                        Session["print"] = hasPrint;
                    }

            #endregion

                   
                }
            }
            
            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBListTransWaitApprove_ViewDetails_Widget", "BindData", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBListTransWaitApprove_ViewDetails_Widget", "BindData", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT: ;

        #endregion


    }
    protected void gvLTWA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblsohoadon;
            Label lblsotien;
            Label lblloaitien;
            Label lbltrangthai;

            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                TableRow tbRow = (TableRow)(e.Row.Cells[0].Controls[0].Controls[0]);
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + " :</strong>";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                lblsohoadon = (Label)e.Row.FindControl("lblsohoadon");
                lblsotien = (Label)e.Row.FindControl("lblsotien");
                lblloaitien = (Label)e.Row.FindControl("lblloaitien");
                lbltrangthai = (Label)e.Row.FindControl("lbltrangthai");

                lblsohoadon.Text = drv["BILLNO"].ToString();
                lblsotien.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["AMOUNT"].ToString(), lblCCYID.Text);
                lblloaitien.Text = lblCCYID.Text;
                

                switch (drv["STATUS"].ToString().Trim())
                {
                    case "F":
                        lbltrangthai.Text = Resources.labels.thanhtoanthatbai;
                        break;
                    case "S":
                        lbltrangthai.Text = Resources.labels.hoanthanh;
                        break;        

                }
                content += "<tr><td>" + lblsohoadon.Text + "</td><td>" + lblsotien.Text + "</td><td>" + lbltrangthai.Text + "</td></tr>";
            }
        }
        catch
        {
        }
    }
}
