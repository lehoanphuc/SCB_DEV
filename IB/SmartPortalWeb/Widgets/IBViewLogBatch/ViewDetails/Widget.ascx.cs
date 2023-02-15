using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using SmartPortal.ExceptionCollection;
using System.Web.UI;

public partial class Widgets_IBListTransWaitApprove_ViewDetails_Widget : WidgetBase
{
    string ACTION = "";
    string cn = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    DataSet ApproveContractTable = new DataSet();
    int count = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["PrintBatchTrans"] = null;
            //Session["tranIDBatch"] = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tranid"].ToString();
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

            //lblError.Text = "";

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
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["style"] == null || SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["style"].ToString() != "view")
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "document.getElementById('div_view').style.visibility = 'hidden';", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "document.getElementById('div_approve').style.visibility = 'visible';", true);
                    pn_approve.Visible = true;
                    pn_View.Visible = false;
                    amount.Visible = true;
                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "document.getElementById('div_view').style.visibility = 'visible';", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "document.getElementById('div_approve').style.visibility = 'hidden';", true);    
                    pn_approve.Visible = false;
                    pn_View.Visible = true;
                    amount.Visible = false;
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ddlAuthenType_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnApprove.Enabled = false;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
    }
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        string errorcode = "";
        string errorDesc = "";
        try
        {
            btnSendOTP.Text = "ReSend";
            btnApprove.Enabled = true;
            ddlAuthenType.Enabled = false;
            if (Session["accType"].ToString() == "IND" || ddlAuthenType.SelectedValue.ToString() == "ESMSOTP")
            {
                SmartPortal.SEMS.OTP.SendSMSOTP(Session["userID"].ToString(), ref errorcode, ref errorDesc);
            }
            else
            {
                SmartPortal.SEMS.OTP.SendSMSOTPCORP(Session["userID"].ToString(), ddlAuthenType.SelectedValue.ToString(), ref errorcode, ref errorDesc);
                lbnotice.Visible = true;
            }
            switch (errorcode)
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
                    lblError.Text = errorDesc;
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
    void BindData()
    {
        string tranID = "";

        if (Session["tranIDBatch"] == null)
        {
            tranID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tranid"].ToString();
        }
        else
        {
            List<string> lstTran = new List<string>();
            lstTran = (List<string>)Session["tranIDBatch"];

            tranID = lstTran[count];
        }

        lblTransID.Text = tranID;

        DataSet ds = new DataSet();
        DataTable tblTranInfo;

        ds = new SmartPortal.SEMS.Transactions().GetTranByTranID(tranID, ref IPCERRORCODE, ref IPCERRORDESC);

        if (IPCERRORCODE != "0")
        {
            throw new IPCException(Resources.labels.khongthelaythongtingiaodich);
        }
        else
        {
            tblTranInfo = ds.Tables[0];
        }

        if (tblTranInfo.Rows.Count != 0)
        {
            lblConfirmSenderAcctno.Text = tblTranInfo.Rows[0]["CHAR01"].ToString();
            lblContent.Text = tblTranInfo.Rows[0]["TRANDESC"].ToString();

            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            //lay thong tin nguoi gui
            Hashtable hasSender = objAcct.loadInfobyAcct(tblTranInfo.Rows[0]["CHAR01"].ToString().Trim());
            if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                lblConfirmSenderName.Text = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                lblConfirmBalance.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString().Trim()), hasSender[SmartPortal.Constant.IPC.CURRENCYID].ToString().Trim());
                lblCCYID.Text = hasSender[SmartPortal.Constant.IPC.CURRENCYID].ToString();

            }
            else
            {
                throw new IPCException(Resources.labels.khonglayduocthongtintaikhoan);
            }

            //xem chi tiết
            DataTable tblBatch = new SmartPortal.IB.Transactions().BatchViewDetail(tranID);

            DataTable tempTable = new DataTable();
            DataColumn col1 = new DataColumn("Stt");
            DataColumn col2 = new DataColumn("Account");
            DataColumn col3 = new DataColumn("User");
            DataColumn col4 = new DataColumn("Amount");
            DataColumn col5 = new DataColumn("Desc");
            DataColumn col6 = new DataColumn("Status");
            tempTable.Columns.AddRange(new DataColumn[] { col1, col2, col3, col4, col5, col6 });
            double totalBalance = 0;
            double totalFee = 0;

            for (int i = 0; i < tblBatch.Rows.Count; i++)
            {
                DataRow r1 = tempTable.NewRow();
                r1["Stt"] = i + 1;
                r1["Account"] = tblBatch.Rows[i]["CHAR02"].ToString();
                r1["User"] = tblBatch.Rows[i]["RECEIVERNAME"].ToString();
                r1["Amount"] = SmartPortal.Common.Utilities.Utility.FormatMoney(tblBatch.Rows[i]["NUM01"].ToString().Trim(), lblCCYID.Text.Trim()) + " " + lblCCYID.Text;
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

                }

                tempTable.Rows.Add(r1);
                lblTable.Text += "<tr><td>" + tempTable.Rows[i]["Stt"] + "</td><td>" + tempTable.Rows[i]["Account"] + "</td><td>" + tempTable.Rows[i]["User"] + "</td><td>" + tempTable.Rows[i]["Amount"] + "</td><td>" + tempTable.Rows[i]["Desc"] + "</td><td>" + tempTable.Rows[i]["Status"] + "</td></tr>";
            }
            rptConfirm.DataSource = tempTable;
            rptConfirm.DataBind();
            DataTable tbldocument = new DataTable();
            tbldocument = ds.Tables[5];
            if (tbldocument.Rows.Count > 0)
            {

                lblDocumentLink.NavigateUrl = tbldocument.Rows[0]["LINK"].ToString();
                lblDocumentLink.Text = "Click to View Details";

                lblDocumentLink.Visible = true;
            }          
            lblTotal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(totalBalance.ToString(), lblCCYID.Text.Trim()) + " " + lblCCYID.Text;
            lblTotalFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(totalFee.ToString(), lblCCYID.Text.Trim()) + " " + lblCCYID.Text;

            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();
            hasPrint.Add("transid", lblTransID.Text);
            hasPrint.Add("senderaccount", lblConfirmSenderAcctno.Text);
            hasPrint.Add("sender", lblConfirmSenderName.Text);
            hasPrint.Add("content", lblContent.Text);
            hasPrint.Add("batch", "<table style='width:100%' cellspacing='0' cellpadding='5' border='1'><tr><td style='width:20%'>" + Resources.labels.sothutu + "</td><td style='width:20%'>" + Resources.labels.sotaikhoan + "</td><td style='width:20%'>" + Resources.labels.nguoithuhuong + "</td><td style='width:20%'>" + Resources.labels.sotien + "</td><td style='width:20%'>" + Resources.labels.diengiai + "</td><td style='width:20%'>" + Resources.labels.trangthai + "</td></tr>" + lblTable.Text + "<table cellspacing='0' cellpadding='5'><tr><td style='width:20%'></td>   <td style='width:20%'></td> <td style='width:20%'></td> <td style=width:20%'>" + Resources.labels.tongcong + "</td><td style='width:20%'>" + lblTotal.Text + "</td></tr></table>");
            Session["PrintBatchTrans"] = hasPrint;
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            //approve
            string tranID = "";

            if (Session["tranIDBatch"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranIDBatch"];

                tranID = lstTran[count];

                //xử lý
                DataSet dsApprove = new SmartPortal.IB.Transactions().UserApp(lblTransID.Text.Trim(), ddlAuthenType.SelectedValue, txtAuthenCode.Text.Trim(), Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                //lblError.Text = IPCERRORCODE;

                if (IPCERRORCODE.Equals("0"))
                {
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.duyetthanhcong;


                    //cập nhật lại List
                    lstTran.RemoveAt(count);
                    count -= 1;
                    ViewState["count"] = count;

                    Session["tranIDBatch"] = lstTran;
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;

                    if (lstTran.Count != 0)
                    {
                        Session["tranIDBatch"] = lstTran;
                        //chuyển qua giao dịch kế
                        //BindData();
                    }
                    else
                    {
                        Session["tranIDBatch"] = null;
                        if (Session["tranID"] != null)
                        {
                            btnPrevious.Enabled = true;
                        }
                        else
                        {
                            btnPrevious.Enabled = false;
                        }
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;

                        btnNext.Enabled = false;
                    }
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.duyetthanhcong;
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

                        Session["tranIDBatch"] = lstTran;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;

                        if (lstTran.Count != 0)
                        {
                            Session["tranIDBatch"] = lstTran;
                            //chuyển qua giao dịch kế
                            //BindData();
                        }
                        else
                        {
                            Session["tranIDBatch"] = null;
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
                            lblError.Text = Resources.labels.notregotp;
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        case "9506":
                            lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.duyetthanhcong;
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
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.duyetthanhcong;
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;
                    return;
                }
                else
                {
                    if (IPCERRORCODE == ConfigurationManager.AppSettings["waittingapproveec"].ToString())
                    {
                        lblError.Text = SmartPortal.Common.Utilities.Utility.GetError(int.Parse(ConfigurationManager.AppSettings["waittingapproveec"].ToString())) + " - " + lblTransID.Text;

                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
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
            //huy giao dich
            string tranID = "";

            if (Session["tranIDBatch"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranIDBatch"];

                tranID = lstTran[count];

                //xử lý
                DataSet dsApprove = new SmartPortal.IB.Transactions().UserDestroy(lblTransID.Text.Trim(), Session["userID"].ToString(), ddlAuthenType.SelectedValue, txtAuthenCode.Text.Trim(), txtDesc.Text, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.huythanhcong;

                    //cập nhật lại List
                    lstTran.RemoveAt(count);
                    count -= 1;
                    ViewState["count"] = count;

                    Session["tranIDBatch"] = lstTran;
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;

                    if (lstTran.Count != 0)
                    {
                        Session["tranIDBatch"] = lstTran;
                        //chuyển qua giao dịch kế
                        //BindData();
                    }
                    else
                    {
                        Session["tranIDBatch"] = null;
                        if (Session["tranID"] != null)
                        {
                            btnPrevious.Enabled = true;
                        }
                        else
                        {
                            btnPrevious.Enabled = false;
                        }
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                        btnNext.Enabled = false;
                    }

                    return;
                }
                else
                {
                    if (IPCERRORCODE == ConfigurationManager.AppSettings["waittingapproveec"].ToString())
                    {
                        lblError.Text = SmartPortal.Common.Utilities.Utility.GetError(int.Parse(ConfigurationManager.AppSettings["waittingapproveec"].ToString())) + " - " + lblTransID.Text;
                        //lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.huythanhcong;

                        //cập nhật lại List
                        lstTran.RemoveAt(count);
                        count -= 1;
                        ViewState["count"] = count;

                        Session["tranIDBatch"] = lstTran;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;

                        if (lstTran.Count != 0)
                        {
                            Session["tranIDBatch"] = lstTran;
                            //chuyển qua giao dịch kế
                            //BindData();
                        }
                        else
                        {
                            Session["tranIDBatch"] = null;
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
                //huy giao dich 1 record
                DataSet dsApprove = new SmartPortal.IB.Transactions().UserDestroy(lblTransID.Text.Trim(), Session["userID"].ToString(), ddlAuthenType.SelectedValue, txtAuthenCode.Text.Trim(), txtDesc.Text, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.huythanhcong;
                    btnReject.Enabled = false;
                    btnApprove.Enabled = false;
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
        List<string> lstTran = new List<string>();
        lstTran = (List<string>)Session["tranIDBatch"];

        if (count < lstTran.Count - 1)
        {
            count += 1;
            ViewState["count"] = count;
        }
        BindData();

        btnApprove.Enabled = true;
        btnReject.Enabled = true;
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (count > 0)
        {
            count -= 1;
            ViewState["count"] = count;
        }
        else
        {
            if (Session["tranID"] != null)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"] != null)
                {
                    string type = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"].ToString();
                    Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=217&type=" + type));
                }
                else
                {
                    Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=136"));
                }
            }
        }
        BindData();
        btnApprove.Enabled = true;
        btnReject.Enabled = true;
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=136"));
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=136"));
    }
}
