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

public partial class Widgets_SEMSDeposit_ViewDetails_Widget : WidgetBase
{
    string ACTION = "";
    string cn = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBListTransWaitApprove_ViewDetails_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    void BindData()
    {
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

                lblTransID.Text = dt.Rows[0]["IPCTRANSID"].ToString();
                lblAccount.Text = dt.Rows[0]["CHAR01"].ToString();
                lblAccountNo.Text = dt.Rows[0]["CHAR02"].ToString();
                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim());
                lblCCYID.Text = dt.Rows[0]["CCYID"].ToString();

                lblDesc.Text = dt.Rows[0]["TRANDESC"].ToString();
                lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(dt.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");

                //load bank
                DataTable tblBank = new SmartPortal.SEMS.Bank().LoadByBankCode(dt.Rows[0]["CHAR05"].ToString());
                if (tblBank.Rows.Count != 0)
                {
                    lblBank.Text = tblBank.Rows[0]["BANKNAME"].ToString();
                }

                switch (dt.Rows[0]["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                        lblStatus.Text =Resources.labels.dangxuly;
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
                                lblResult.Text =Resources.labels.dangxuly;
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
                                lblResult.Text =Resources.labels.dangxuly;
                                break;
                           
                        }
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
                        lblStatus.Text = "Lỗi";
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
                                lblResult.Text =Resources.labels.dangxuly;
                                break;
                           
                        }
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

            }
            #endregion

            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBListTransWaitApprove_ViewDetails_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBListTransWaitApprove_ViewDetails_Widget", "BindData", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT: ;

        #endregion


    }
   
}
