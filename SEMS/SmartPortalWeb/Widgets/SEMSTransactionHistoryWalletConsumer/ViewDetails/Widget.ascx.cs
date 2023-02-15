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

public partial class Widgets_SEMSViewLogTransactions_ViewDetails_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
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
            if (IPCERRORCODE == "0")
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {
                    pnDefault.Visible = true;

                    lblTransID.Text = dt.Rows[0]["IPCTRANSID"].ToString();
                    hasPrint["tranID"] = lblTransID.Text;

                    lblAccount.Text = SmartPortal.Common.Utilities.Utility.FormatStringCore(dt.Rows[0]["CHAR01"].ToString());
                    hasPrint["debitAccount"] = lblAccount.Text;

                    lblAccountNo.Text = dt.Rows[0]["CHAR02"].ToString();
                    hasPrint["creditAccount"] = lblAccountNo.Text;

                    lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim());
                    hasPrint["amount"] = lblAmount.Text;
                    string ccyid = dt.Rows[0]["CCYID"].ToString();
                    lblstbc.Text = Utility.NumtoWords(Convert.ToDouble(lblAmount.Text)) + " " + ccyid;
                    hasPrint["sotienbangchu"] = lblstbc.Text;

                    lblCCYID.Text = ccyid;
                    hasPrint["ccyid"] = lblCCYID.Text;


                    DataTable tblU = new SmartPortal.SEMS.User().GetUBID(dt.Rows[0]["USERCURAPP"].ToString().Trim());


                    if (tblU.Rows.Count != 0)
                    {
                        lblLastApp.Text = tblU.Rows[0]["FULLNAME"].ToString();
                    }

                    hasPrint["lastapp"] = lblLastApp.Text;

                    lblCCYIDPhi.Text = ccyid;
                    lblCCYIDVAT.Text = ccyid;

                    lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(dt.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    hasPrint["date"] = lblDate.Text;

                    lblUserCreate.Text = dt.Rows[0]["FULLNAME"].ToString();
                    lblAppSts.Text = dt.Rows[0]["APPSTATUS"].ToString();

                    string[] c = dt.Rows[0]["TRANDESC"].ToString().Split('|');
                    if (c.Length > 1)
                    {
                        lblApproveDate_0.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(c[1].ToString()).ToString("dd/MM/yyyy HH:mm");

                    }
                    lblDesc.Text = c[0].ToString();
                    double feeNum = SmartPortal.Common.Utilities.Utility.isDouble(dt.Rows[0]["NUM02"].ToString(), false);

                    string f = (feeNum).ToString();
                    string v = (0).ToString();

                    lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(f, ccyid);
                    hasPrint["feeAmount"] = lblFee.Text;

                    lblVAT.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(v, dt.Rows[0]["CCYID"].ToString().Trim());
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
                        lbltinhthanh.Text = tblBank.Rows[0]["CITYNAME"].ToString();
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
                        }
                    }
                    if (lblSenderName.Text == "" && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        lblSenderName.Text = ds.Tables[2].Rows[0][0].ToString();
                    }

                    hasPrint["bank"] = lblBank.Text;
                    hasPrint["city"] = lbltinhthanh.Text;
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
}
