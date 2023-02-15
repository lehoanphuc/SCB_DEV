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
using SmartPortal.ExceptionCollection;
using SmartPortal.IB;
using System.Globalization;

public partial class Widgets_SEMSTransactionsApprove_ViewDetailCloseFD_Widget : WidgetBase
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
            txtNewRate.Attributes.Add("onkeyup", "ntt('" + txtNewRate.ClientID + "','" + lblText.ClientID + "',event)");
            
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"] != null)
            {
                switch (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"].ToString().Trim())
                {
                    case "a":
                        btReject.Visible = false;
                        btApprove.Visible = true;
                        break;
                    case "r":
                        btReject.Visible = true;
                        btApprove.Visible = false;
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

                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_ViewDetail_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    void BindData()
    {
        #region Lấy thông tin giao dịch
        try
        {
            string tranID = "";
            StringBuilder sTPrint = new StringBuilder();

            if (Session["tranID"] == null)
            {
                tranID=SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tranid"].ToString();
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

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORCODE); 
                }

                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {

                    lblTransID.Text = dt.Rows[0]["IPCTRANSID"].ToString();
                    lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM01"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim());
                    lblCCYID.Text = dt.Rows[0]["CCYID"].ToString();
                   

                    lblDesc.Text = dt.Rows[0]["TRANDESC"].ToString();
                    lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(dt.Rows[0]["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    lblSenderAccount.Text = dt.Rows[0]["CHAR01"].ToString();
                    lblReceiverAccount.Text = dt.Rows[0]["CHAR02"].ToString();
                    lblDesc.Text= dt.Rows[0]["TRANDESC"].ToString();
                    lblUserCreate.Text = dt.Rows[0]["FULLNAME"].ToString();
                    lblAppSts.Text = dt.Rows[0]["APPSTATUS"].ToString();
                    //lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(dt.Rows[0]["NUM02"].ToString(), dt.Rows[0]["CCYID"].ToString().Trim());

                    //load bank
                    //DataTable tblBank = new SmartPortal.SEMS.Bank().LoadByBankCode(dt.Rows[0]["CHAR05"].ToString());
                    //if (tblBank.Rows.Count != 0)
                    //{
                    //    lblBank.Text = tblBank.Rows[0]["BANKNAME"].ToString();
                    //}
                    

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
                    
                    sT.Append("<table class='tblVDC' cellspacing='1' cellpadding='5'>");
                    sT.Append("<tr>");
                    sT.Append("<td class='tdVDC'>");
                    sT.Append(Resources.labels.manhanvien);
                    sT.Append("</td>");
                    sT.Append("<td class='tdVDC' style = 'border-left: solid 1px #b9bfc1';>");
                    sT.Append(Resources.labels.tennhanvien);
                    sT.Append("</td>");
                    sT.Append("<td class='tdVDC' style = 'border-left: solid 1px #b9bfc1';>");
                    sT.Append(Resources.labels.kieunhanvien);
                    sT.Append("</td>");
                    sT.Append("<td class='tdVDC' style = 'border-left: solid 1px #b9bfc1';>");
                    sT.Append(Resources.labels.capbac);
                    sT.Append("</td>");
                    sT.Append("<td class='tdVDC' style = 'border-left: solid 1px #b9bfc1';>");
                    sT.Append(Resources.labels.loaixacthuc);
                    sT.Append("</td>");
                    sT.Append("<td class='tdVDC' style = 'border-left: solid 1px #b9bfc1';>");
                    sT.Append(Resources.labels.maxacthuc);
                    sT.Append("</td>");

                    sT.Append("</tr>");
                    //print ...
                    sTPrint.Append("<table style = 'width:100%;' cellspacing='5' cellpadding='5' border='1'>");
                    sTPrint.Append("<tr>");
                    sTPrint.Append("<td style = 'width:17%'>");
                    sTPrint.Append(Resources.labels.manhanvien);
                    sTPrint.Append("</td>");
                    sTPrint.Append("<td style = 'width=17%'>");
                    sTPrint.Append(Resources.labels.tennhanvien);
                    sTPrint.Append("</td>");
                    sTPrint.Append("<td style = 'width=17%'>");
                    sTPrint.Append(Resources.labels.kieunhanvien);
                    sTPrint.Append("</td>");
                    sTPrint.Append("<td style = 'width=17%'>");
                    sTPrint.Append(Resources.labels.capbac);
                    sTPrint.Append("</td>");
                    sTPrint.Append("<td style = 'width=17%'>");
                    sTPrint.Append(Resources.labels.loaixacthuc);
                    sTPrint.Append("</td>");
                    sTPrint.Append("<td style = 'width=17%'>");
                    sTPrint.Append(Resources.labels.maxacthuc);
                    sTPrint.Append("</td>");

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
                        //print ...
                        sTPrint.Append("<tr>");
                        sTPrint.Append("<td>");
                        sTPrint.Append(row["USERAPP"].ToString());
                        sTPrint.Append("</td>");
                        sTPrint.Append("<td>");
                        sTPrint.Append(row["FULLNAME"].ToString());
                        sTPrint.Append("</td>");
                        sTPrint.Append("<td>");
                        sTPrint.Append(row["USERTYPE"].ToString());
                        sTPrint.Append("</td>");
                        sTPrint.Append("<td>");
                        sTPrint.Append(row["USERLEVEL"].ToString());
                        sTPrint.Append("</td>");
                        sTPrint.Append("<td>");
                        sTPrint.Append(row["AUTHENTYPE"].ToString());
                        sTPrint.Append("</td>");
                        sTPrint.Append("<td>");
                        sTPrint.Append(row["AUTHENCODE"].ToString());
                        sTPrint.Append("</td>");

                        sTPrint.Append("</tr>");
                        //print ...
                    }
                    sT.Append("</table>");
                    //print ...
                    sTPrint.Append("</table>");

                    ltrTH.Text = sT.ToString();
                    
                  
                    //get detail
                    //DataTable dtLogTranDetail = new SmartPortal.SEMS.Transactions().GetLogTranDetail(tranID);
                    //foreach (DataRow r in dtLogTranDetail.Rows)
                    //{
                    //    switch (r["FIELDNAME"].ToString().Trim())
                    //    {
                            
                    //        case SmartPortal.Constant.IPC.SENDERNAME:
                    //            lblSenderName.Text = r["FIELDVALUE"].ToString().Trim();
                    //            break;
                    //    }
                    //}

                }
                #endregion

            //load thong tin tai khoan tiet kiem
                try
                {
                    string acct = lblSenderAccount.Text.Trim();
                    string ErrorCode = string.Empty;
                    string ErrorDesc = string.Empty;
                    Account objAcct = new Account();
                    DataSet ds1 = new DataSet();
                    ds1 = objAcct.GetFDAcctInfo(Session["userID"].ToString(), acct, ref ErrorCode, ref ErrorDesc);
                    ShowFD(acct, ds1);

                    //lay thong tin chi nhanh DD
                    string acctdd = lblReceiverAccount.Text.Trim();
                    DataTable tblDD= objAcct.GetAcctnoInfo(acctdd);
                    if (tblDD.Rows.Count != 0)
                    {
                        Account acctobj = new Account();

                        DataSet dsBranch = acctobj.GetBranch(tblDD.Rows[0]["BRANCHID"].ToString());
                        if (dsBranch.Tables[0].Rows.Count == 1)
                        {
                            lblBranch_DD.Text = dsBranch.Tables[0].Rows[0][SmartPortal.Constant.IPC.BRANCHNAME].ToString();
                        }
                    }
                }
                catch
                {
                }
             
            //lay lai tam tinh
                Hashtable dsLaiTamTinh = new SmartPortal.SEMS.FDProductOnline().LaiTruocHan(lblSenderAccount.Text.Trim(),ref IPCERRORCODE,ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {

                    lblLaiTamTinh.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsLaiTamTinh["STRDESTRESULT"].ToString()),lblCCYID.Text);
                }
                else
                {
                    lblError.Text = IPCERRORDESC;
                    return;
                }
                //Lưu session in
                #region Lấy thông tin lưu vào session in
                Hashtable hasPrint = new Hashtable();
                hasPrint.Add("thongtinchitietduyet",sTPrint.ToString());
                hasPrint.Add("sogiaodich", lblTransID.Text);
                hasPrint.Add("ngaygiogiaodich", lblDate.Text);
                hasPrint.Add("taikhoantietkiemcokyhan", lblSenderAccount.Text);
                hasPrint.Add("soduhientai", lblCB_FD.Text+" "+ lblCCYIDB.Text);
                hasPrint.Add("ngaymotaikhoan", lblDO_FD.Text);
                hasPrint.Add("laisuat", lblIR_FD.Text + " " + lblpercentyear.Text);
                hasPrint.Add("ngaydenhan", lblLT_FD.Text);
                hasPrint.Add("tentaikhoan", lblACCTNAME.Text);
                hasPrint.Add("taikhoanthanhtoan", lblReceiverAccount.Text);
                hasPrint.Add("chinhanh",lblBranch_DD.Text);
                hasPrint.Add("chinhanhfd", lblBranchFD.Text);
                hasPrint.Add("sotien", lblAmount.Text + " " + lblCCYID.Text);
                hasPrint.Add("mota", lblDesc.Text);
                hasPrint.Add("tenkhachhang", lblUserCreate.Text);
                hasPrint.Add("nguoiduyet", lblAppSts.Text);
                hasPrint.Add("trangthai", lblStatus.Text);
                hasPrint.Add("ketqua", lblResult.Text);
                hasPrint.Add("laiduochuongtamtinh", lblLaiTamTinh.Text + " " + lblCCYIDTT.Text);
                hasPrint.Add("laicongdon", lblACRI_FD.Text + " " + lblCCYIDACI.Text);
                hasPrint.Add("diengiai", txtDesc.Text);
               
                Session["printDetailCloseFD"] = hasPrint;
                #endregion
                goto EXIT;
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
    EXIT: ;
       
        #endregion

       
    }


    private void ShowFD(string acctno, DataSet ds)
    {
        try
        {
            //lblAccountNumber_FD.Text = acctno;
            Account acct = new Account();
            if (ds.Tables[0].Rows.Count == 1)
            {
                //lblAccountName_FD.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                //lblCurrency_FD.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                lblDO_FD.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.OPENDATE].ToString(), "dd/MM/yyyy");
                lblLT_FD.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.EXPIREDATE].ToString(), "dd/MM/yyyy");
                lblCB_FD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
                //lblAB_FD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()));

                //SmartPortal.Constant.IPC.AVAILABLEBALANCE
                lblACRI_FD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.ACCRUEDCRINTEREST].ToString()), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
                lblCCYIDACI.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim();
                lblCCYIDB.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim();
                lblCCYIDTT.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim();
                lblACCTNAME.Text = ds.Tables[0].Rows[0]["ACCTNAME"].ToString().Trim();
                //lblIR_FD.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.INTERESTRATE].ToString();

                NumberStyles style;
                CultureInfo culture;
                double laisuat;

                style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                culture = CultureInfo.CreateSpecificCulture("en-US");

                double.TryParse(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.INTERESTRATE].ToString(), style, culture, out laisuat);
                lblIR_FD.Text = (laisuat * 100).ToString(culture.NumberFormat);

                string[] strBranch = ds.Tables[0].Rows[0]["CHINHANH"].ToString().Split('.');
                DataSet dsBranch = acct.GetBranch(strBranch[0].ToString());
                if (dsBranch.Tables[0].Rows.Count == 1)
                {
                    lblBranchFD.Text = dsBranch.Tables[0].Rows[0][SmartPortal.Constant.IPC.BRANCHNAME].ToString();
                }
            }
        }
        catch
        {
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

                
                    DataSet dsApprove = new SmartPortal.SEMS.Transactions().UserAppTT(lblTransID.Text.Trim(), Session["userID"].ToString(),txtDesc.Text, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblLaiTamTinh.Text,"en-US"),SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblACRI_FD.Text,"en-US"), ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.giaodich+" "+lblTransID.Text+" "+Resources.labels.duyetthanhcong;
                        Hashtable hasPrint = new Hashtable();
                        hasPrint = (Hashtable)Session["printDetailCloseFD"];
                        hasPrint.Remove("nguoiduyet");
                        hasPrint.Remove("trangthai");
                        hasPrint.Add("nguoiduyet", Session["userID"].ToString());
                        hasPrint.Add("trangthai", Resources.labels.hoanthanh);
                        Session["printDetailCloseFD"] = hasPrint;

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
                            //chuyển qua giao dịch kế
                            //BindData();
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
                        //cap nhat trang thai tai khoan tiet kiem
                        new SmartPortal.IB.FD().UpdateIsClose(lblSenderAccount.Text.Trim(), "N");

                        if (IPCERRORCODE == ConfigurationManager.AppSettings["waittingapproveec"].ToString())
                        {
                            lblError.Text = SmartPortal.Common.Utilities.Utility.GetError(int.Parse(ConfigurationManager.AppSettings["waittingapproveec"].ToString())) + " - " + lblTransID.Text;

                            //cập nhật lại List
                            lstTran.RemoveAt(count);
                            count -= 1;
                            ViewState["count"] = count;
                            Session["tranID"] = lstTran;
                            btApprove.Enabled = false;
                            btReject.Enabled = false;

                            if (lstTran.Count != 0)
                            {
                                Session["tranID"] = lstTran;
                                //chuyển qua giao dịch kế
                                //BindData();
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
                DataSet dsApprove = new SmartPortal.SEMS.Transactions().UserAppTT(lblTransID.Text.Trim(), Session["userID"].ToString(), txtDesc.Text,SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblLaiTamTinh.Text,"en-US"),SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblACRI_FD.Text,"en-US"), ref IPCERRORCODE, ref IPCERRORDESC);


                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.giaodich + " " + lblTransID.Text + " " + Resources.labels.duyetthanhcong;
                    btApprove.Visible = false;
                    btReject.Visible = false;
                    pnChiTiet.Visible = false;
                    pnToken.Visible = false;

                    Hashtable hasPrint = new Hashtable();
                    hasPrint = (Hashtable)Session["printDetailCloseFD"];
                    hasPrint.Remove("nguoiduyet");
                    hasPrint.Remove("trangthai");
                    hasPrint.Add("nguoiduyet", Session["userID"].ToString());
                    hasPrint.Add("trangthai", Resources.labels.hoanthanh);
                    Session["printDetailCloseFD"] = hasPrint;
                    return;                   
                }
                else
                {
                    //cap nhat trang thai tai khoan tiet kiem
                    new SmartPortal.IB.FD().UpdateIsClose(lblSenderAccount.Text.Trim(), "N");

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

                goto REDI;
            }
            goto EXIT;
        }
        catch
        {
        }
    REDI:
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnURL"].ToString())));
    EXIT:
        ;
    }
    protected void btReject_Click1(object sender, EventArgs e)
    {
        try
        {
            //huy
            string tranID = "";

            if (Session["tranID"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["tranID"];

                tranID = lstTran[count];

                //xử lý               


                DataSet dsApprove = new SmartPortal.SEMS.Transactions().TellerDestroy(lblTransID.Text.Trim(), Session["userID"].ToString(),txtDesc.Text, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    //cap nhat trang thai tai khoan tiet kiem
                    new SmartPortal.IB.FD().UpdateIsClose(lblSenderAccount.Text.Trim(), "N");

                    lblError.Text = Resources.labels.giaodich+" " + lblTransID.Text + " "+Resources.labels.huythanhcong;
                    Hashtable hasPrint = new Hashtable();
                    hasPrint = (Hashtable)Session["printDetailCloseFD"];
                    hasPrint.Remove("nguoiduyet");
                    hasPrint.Remove("trangthai");
                    hasPrint.Add("nguoiduyet", Session["userID"].ToString());
                    hasPrint.Add("trangthai", Resources.labels.khongduyet);
                    Session["printDetailCloseFD"] = hasPrint;

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
                        //chuyển qua giao dịch kế
                        //BindData();
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
                            //chuyển qua giao dịch kế
                            //BindData();
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
                DataSet dsApprove = new SmartPortal.SEMS.Transactions().TellerDestroy(lblTransID.Text.Trim(), Session["userID"].ToString(),txtDesc.Text, ref IPCERRORCODE, ref IPCERRORDESC);


                if (IPCERRORCODE == "0")
                {
                    //cap nhat trang thai tai khoan tiet kiem
                    new SmartPortal.IB.FD().UpdateIsClose(lblSenderAccount.Text.Trim(), "N");

                    lblError.Text = Resources.labels.giaodich+" " + lblTransID.Text + " "+Resources.labels.huythanhcong;
                    btApprove.Visible = false;
                    btReject.Visible = false;
                    pnChiTiet.Visible = false;
                    pnToken.Visible = false;
                    Hashtable hasPrint = new Hashtable();
                    hasPrint = (Hashtable)Session["printDetailCloseFD"];
                    hasPrint.Remove("nguoiduyet");
                    hasPrint.Remove("trangthai");
                    hasPrint.Add("nguoiduyet", Session["userID"].ToString());
                    hasPrint.Add("trangthai", Resources.labels.khongduyet);
                    Session["printDetailCloseFD"] = hasPrint;
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
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnURL"].ToString())));
    EXIT:
        ;
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnURL"].ToString())));
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
    protected void btnTinhLai_Click(object sender, EventArgs e)
    {
        try
        {
            //tinh lai
            double tanggiamlai = SmartPortal.Common.Utilities.Utility.isDouble(txtNewRate.Text, true) - SmartPortal.Common.Utilities.Utility.isDouble(lblACRI_FD.Text, true);
            new SmartPortal.SEMS.FDProductOnline().TinhLai(lblSenderAccount.Text, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(tanggiamlai.ToString(),lblCCYID.Text), ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                //hien thi lai lai tich luy

                Hashtable dsLaiTichLuy = new SmartPortal.SEMS.FDProductOnline().LaiTichLuy(lblSenderAccount.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (SmartPortal.Common.Utilities.Utility.FormatStringCore(dsLaiTichLuy["ERRORNBR"].ToString().Trim()) == "1")
                    {
                        lblACRI_FD.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsLaiTichLuy["ACCRUEDINT"].ToString()), lblCCYID.Text);
                        Hashtable hasPrint = new Hashtable();
                        hasPrint = (Hashtable)Session["printDetailCloseFD"];
                        hasPrint.Remove("laicongdon");
                        hasPrint.Add("laicongdon", lblACRI_FD.Text + " " + lblCCYIDACI.Text);
                        Session["printDetailCloseFD"] = hasPrint;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                }
                else
                {
                    lblError.Text = IPCERRORDESC;
                    return;
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
            
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}
