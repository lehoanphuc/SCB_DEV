using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Text;
using System.Windows.Forms.VisualStyles;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSTransactionsApprove_ViewDetail_Widget : WidgetBase
{
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    int count = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            ACTION = GetActionPage();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.APPROVE:
                    btReject.Visible = false;
                    btApprove.Visible = true;
                    break;
                case IPC.ACTIONPAGE.REJECT:
                    btReject.Visible = true;
                    btApprove.Visible = false;
                    break;

            }
            if (ViewState["count"] != null)
            {
                count = int.Parse(ViewState["count"].ToString().Trim());
            }

            if (Session["tranID"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranID"];
                if (lstTran.Count > 1)
                {
                    btnNext.Visible = true;
                    btnPrevious.Visible = true;
                }
                else
                {
                    btnNext.Visible = false;
                    btnPrevious.Visible = false;
                }
            }
            else
            {
                btnNext.Visible = false;
                btnPrevious.Visible = false;
            }

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
            string tranID = "";

            if (Session["tranID"] == null)
            {
                tranID = GetParamsPage(IPC.ID)[0].Trim();
            }
            else
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranID"];
                tranID = lstTran[count];
            }

            #region Trường hợp session null
            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.Transactions().GetTranByTranID(tranID, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {

                    lblTransID.Text = dt.Rows[0]["IPCTRANSID"].ToString();
                    lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(),
                        dt.Rows[0]["CCYID"].ToString().Trim());
                    lblCCYID.Text = dt.Rows[0]["CCYID"].ToString();
                    lblCCYIDPhi.Text = dt.Rows[0]["CCYID"].ToString();

                    lblDesc.Text = dt.Rows[0]["TRANDESC"].ToString();
                    lblDate.Text = SmartPortal.Common.Utilities.Utility
                        .IsDateTime2(dt.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    lblSenderAccount.Text = dt.Rows[0]["CHAR01"].ToString();
                    lblReceiverAccount.Text = dt.Rows[0]["CHAR02"].ToString();
                    lblDesc.Text = dt.Rows[0]["TRANDESC"].ToString();
                    lblUserCreate.Text = dt.Rows[0]["FULLNAME"].ToString();
                    lblAppSts.Text = dt.Rows[0]["APPSTATUS"].ToString();
                    lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM02"].ToString(),
                        dt.Rows[0]["CCYID"].ToString().Trim());

                    //load bank
                    DataTable tblBank = new SmartPortal.SEMS.Bank().LoadByBankCode(dt.Rows[0]["CHAR05"].ToString());
                    if (tblBank.Rows.Count != 0)
                    {
                        lblBank.Text = tblBank.Rows[0]["BANKNAME"].ToString();
                    }


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
                                case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGBANK:
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
                                case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGBANK:
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
                                case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGBANK:
                                    lblResult.Text = Resources.labels.choduyet;
                                    break;
                                case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                    lblResult.Text = Resources.labels.dangxuly;
                                    break;


                            }

                            break;

                    }

                    //Load approve detail
                    DataTable ddTable = new DataTable();
                    ddTable = ds.Tables[1];
                    StringBuilder sT = new StringBuilder();
                    sT.Append("<table class='table table-bordered table-hover' cellspacing='1' cellpadding='5'>");
                    sT.Append("<tr>");
                    sT.Append("<th>");
                    sT.Append(Resources.labels.manhanvien);
                    sT.Append("</th>");
                    sT.Append("<th>");
                    sT.Append(Resources.labels.tennhanvien);
                    sT.Append("</th>");
                    sT.Append("<th>");
                    sT.Append(Resources.labels.kieunhanvien);
                    sT.Append("</th>");
                    sT.Append("<th>");
                    sT.Append(Resources.labels.capbac);
                    sT.Append("</th>");
                    sT.Append("<th>");
                    sT.Append(Resources.labels.loaixacthuc);
                    sT.Append("</th>");
                    sT.Append("<th>");
                    sT.Append(Resources.labels.maxacthuc);
                    sT.Append("</th>");

                    sT.Append("</tr>");
                    foreach (DataRow row in ddTable.Rows)
                    {
                        //DateTime temp = (DateTime)row["Time"];
                        sT.Append("<tr>");
                        sT.Append("<td>");
                        sT.Append(row["USERAPP"].ToString());
                        sT.Append("</td>");
                        sT.Append("<td>");
                        sT.Append(row["FULLNAME"].ToString());
                        sT.Append("</td>");
                        sT.Append("<td>");
                        sT.Append(row["USERTYPE"].ToString());
                        sT.Append("</td>");
                        sT.Append("<td>");
                        sT.Append(row["USERLEVEL"].ToString());
                        sT.Append("</td>");
                        sT.Append("<td>");
                        sT.Append(row["AUTHENTYPE"].ToString());
                        sT.Append("</td>");
                        sT.Append("<td>");
                        sT.Append(row["AUTHENCODE"].ToString());
                        sT.Append("</td>");

                        sT.Append("</tr>");
                    }

                    sT.Append("</table>");

                    ltrTH.Text = sT.ToString();
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
            }
            else
            {
                throw new IPCException(IPCERRORCODE);
            }
            #endregion
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btApprove_Click(object sender, EventArgs e)
    {
        try
        {
            //approve
            string tranID = "";

            if (Session["tranID"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranID"];

                tranID = lstTran[count];

                //xử lý                
                DataSet dsApprove = new SmartPortal.SEMS.Transactions().UserApp(lblTransID.Text.Trim(), Session["userID"].ToString(), txtDesc.Text, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.duyetthanhcong;
                    //cập nhật lại List
                    lstTran.RemoveAt(0);
                    count -= 1;
                    ViewState["count"] = count;
                    Session["tranID"] = lstTran;
                    btApprove.Enabled = false;
                    btReject.Enabled = false;

                    if (lstTran.Count != 0)
                    {
                        Session["tranID"] = lstTran;
                    }
                    else
                    {
                        Session["tranID"] = null;
                        btApprove.Enabled = false;
                        btReject.Enabled = false;
                        btnPrevious.Enabled = false;
                        btnNext.Enabled = false;
                    }
                    return;
                }
                else
                {
                    if (IPCERRORCODE == ConfigurationManager.AppSettings["waittingapproveec"].ToString())
                    {
                        lblError.Text = SmartPortal.Common.Utilities.Utility.GetError(int.Parse(ConfigurationManager.AppSettings["waittingapproveec"].ToString())) + " - " + lblTransID.Text;

                        lstTran.RemoveAt(count);
                        count -= 1;
                        ViewState["count"] = count;
                        Session["tranID"] = lstTran;
                        btApprove.Enabled = false;
                        btReject.Enabled = false;

                        if (lstTran.Count != 0)
                        {
                            Session["tranID"] = lstTran;
                        }
                        else
                        {
                            Session["tranID"] = null;
                            btApprove.Enabled = false;
                            btReject.Enabled = false;
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
                            lblError.Text = Resources.labels.notregotp;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        default:
                            lblError.Text = IPCERRORDESC;
                            break;
                    }
                }
            }
            else
            {
                //approve khi duyệt 1 record
                DataSet dsApprove = new SmartPortal.SEMS.Transactions().UserApp(lblTransID.Text.Trim(), Session["userID"].ToString(), txtDesc.Text, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.duyetthanhcong;
                    btApprove.Enabled = false;
                    btReject.Enabled = false;
                    return;
                }
                else
                {
                    if (IPCERRORCODE == ConfigurationManager.AppSettings["waittingapproveec"].ToString())
                    {
                        lblError.Text = SmartPortal.Common.Utilities.Utility.GetError(int.Parse(ConfigurationManager.AppSettings["waittingapproveec"].ToString())) + " - " + lblTransID.Text;
                        btApprove.Enabled = false;
                        btReject.Enabled = false;
                        return;
                    }
                    switch (IPCERRORCODE)
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblError.Text = IPCERRORDESC;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                            lblError.Text = Resources.labels.notregotp;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        default:
                            lblError.Text = IPCERRORDESC;
                            return;

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
    protected void btReject_Click1(object sender, EventArgs e)
    {
        try
        {
            string tranID = "";

            if (Session["tranID"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranID"];

                tranID = lstTran[count];

                //xử lý              

                DataSet dsApprove = new SmartPortal.SEMS.Transactions().TellerDestroy(lblTransID.Text.Trim(), Session["userID"].ToString(), txtDesc.Text, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.huythanhcong;

                    //cập nhật lại List
                    lstTran.RemoveAt(0);
                    count -= 1;
                    ViewState["count"] = count;

                    Session["tranID"] = lstTran;
                    btApprove.Enabled = false;
                    btReject.Enabled = false;

                    if (lstTran.Count != 0)
                    {
                        Session["tranID"] = lstTran;
                    }
                    else
                    {
                        Session["tranID"] = null;
                        btApprove.Enabled = false;
                        btReject.Enabled = false;
                        btnPrevious.Enabled = false;
                        btnNext.Enabled = false;
                    }

                    return;
                }
                else
                {
                    if (IPCERRORCODE == ConfigurationManager.AppSettings["waittingapproveec"].ToString())
                    {
                        lblError.Text = SmartPortal.Common.Utilities.Utility.GetError(int.Parse(ConfigurationManager.AppSettings["waittingapproveec"].ToString())) + " - " + lblTransID.Text;

                        //cập nhật lại List
                        lstTran.RemoveAt(0);

                        count -= 1;
                        ViewState["count"] = count;

                        Session["tranID"] = lstTran;
                        btApprove.Enabled = false;
                        btReject.Enabled = false;

                        if (lstTran.Count != 0)
                        {
                            Session["tranID"] = lstTran;
                        }
                        else
                        {
                            Session["tranID"] = null;
                            btApprove.Enabled = false;
                            btReject.Enabled = false;
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
                            lblError.Text = Resources.labels.notregotp;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        default:
                            lblError.Text = IPCERRORDESC;
                            return;
                    }
                }
            }
            else
            {
                //approve khi duyệt 1 record
                DataSet dsApprove = new SmartPortal.SEMS.Transactions().TellerDestroy(lblTransID.Text.Trim(), Session["userID"].ToString(), txtDesc.Text, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.huythanhcong;
                    btReject.Enabled = false;
                    btApprove.Enabled = false;
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
                            lblError.Text = Resources.labels.notregotp;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        default:
                            lblError.Text = IPCERRORDESC;
                            return;
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
    protected void btnExit_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        List<string> lstTran = new List<string>();
        lstTran = (List<string>)Session["tranID"];

        if (count < lstTran.Count - 1)
        {
            count += 1;
            ViewState["count"] = count;
        }
        BindData();

        btApprove.Enabled = true;
        btReject.Enabled = true;
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (count > 0)
        {
            count -= 1;
            ViewState["count"] = count;
        }
        BindData();

        btApprove.Enabled = true;
        btReject.Enabled = true;
    }
}
