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

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using System.Globalization;

public partial class Widgets_IBBillPayment_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string errorcode = "";
    string errorDesc = "";
    private static string KeyID = "";

    protected void Page_Load(object sender, EventArgs e)

    {
        try
        {
            lblTextError.Text = "";
            //.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
            if (!IsPostBack)
            {


                //hide panel pnPayment.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
                pnPayment.Visible = false;
                pnConfirm.Visible = false;


                //load service
                DataTable dtSV = new DataTable();
                dtSV = new SmartPortal.IB.Payment().GetService();
                if (dtSV.Rows.Count == 0)
                {
                    throw new Exception();
                }
                ddlservice.DataSource = dtSV;
                ddlservice.DataTextField = "Paymenttypename";
                ddlservice.DataValueField = "Paymenttypeid";
                ddlservice.DataBind();

                //load provider...
                ddlservice_SelectedIndexChanged(sender, e);

                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                DataSet ds = objAcct.getAccount(Session["userID"].ToString(), "IB000208", "DD", ref errorcode, ref errorDesc);
                ddlSenderAccount.DataSource = ds;
                ddlSenderAccount.DataValueField = ds.Tables[0].Columns[SmartPortal.Constant.IPC.ACCTNO].ColumnName.ToString();
                ddlSenderAccount.DataBind();
            }
            //if (radTS.Checked)
            //{
            //    txtTS.Enabled = true;
            //}
            //else
            //{
            //    txtTS.Enabled = false;
            //}

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

    void BindData()
    {
        try
        {
            lblService.Text = ddlservice.SelectedItem.ToString();
            lblprovider.Text = ddlprovider.SelectedItem.ToString();
            DataSet dtWater = new DataSet();
            //NuocChoLon.Service WS = new NuocChoLon.Service();
            //WS.Url = System.Configuration.ConfigurationManager.AppSettings["WSWATERPAYMENT"];
            //dtWater = WS.getW_Bill(txtCustCode.Text.Trim(), SmartPortal.Constant.IPC.PHUONGNAM, DateTime.Now.Day.ToString());
            dtWater = new SmartPortal.IB.Payment().GetBillPayment(txtCustCode.Text.Trim(), ref errorcode, ref errorDesc);

            if (dtWater == null || errorcode!="0")
            {
                lblTextError.Text = Resources.labels.makhachhangkhonghople;
                return;
            }

            if (dtWater.Tables[0].Rows.Count == 0)
            {
                lblTextError.Text = Resources.labels.khongcohoadonnaodethanhtoan + " / " + Resources.labels.makhachhangkhonghople;
                return;
                //litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvProductList.PageIndex) * gvProductList.PageSize) + gvProductList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtProLim.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;
            }
            if (dtWater.Tables[0].Rows.Count != 0)
            {
                gvProductList.DataSource = dtWater;
                gvProductList.DataBind();
                DataTable dt = new DataTable();
                dt = dtWater.Tables[0];
                lblcustname.Text =  dt.Rows[0]["KHang"].ToString();
                lblCustCode.Text = dt.Rows[0]["DBo"].ToString();
                lbladdress.Text = dt.Rows[0]["Dchi1"].ToString();
                lblstreet.Text = dt.Rows[0]["Dchi2"].ToString();
                //hien thi  menu
                pnPayment.Visible = true;
                pnOTP.Visible = false;
                pnService.Visible = false;
                pnResultTransaction.Visible = false;
                pnConfirm.Visible = false;

            }
            else
            {
                lblTextError.Text = Resources.labels.makhachhangkhonghople;
                return;
                //throw new IPCException(IPCERRORDESC);
                //throw new IPCException("");
            }
            //else
            //{
            //    ltrError.Text = "<p style='color:red;margin-left:10px; margin-top:20px;'>" + Resources.labels.datanotfound + "</p>";
            //}
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlprovider.SelectedValue != "")
            {
                BindData();
                LayCCYID();
            }
            else 
            {
                lblTextError.Text = Resources.labels.khongconhacungcapnao;
            }
            
        }
        catch
        {
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            pnPayment.Visible = false;
            pnOTP.Visible = false;
            pnService.Visible = true;
            pnResultTransaction.Visible = false;
            pnConfirm.Visible = false;
            //BindData();
        }
        catch
        {
        }
    }
    protected void btnBackOTP_Click(object sender, EventArgs e)
    {
        try
        {
            pnPayment.Visible = false;
            pnOTP.Visible = false;
            pnService.Visible = false;
            pnResultTransaction.Visible = false;
            pnConfirm.Visible = true;
            //BindData();
        }
        catch
        {
        }
    }
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
           
        string senderName = "";
        string balanceSender = "";
        string acctCCYID = "";

        string receiverName = "";
        string receiverCCYID = "";

        try
        {
            CheckBox cbxSelect;
            //CheckBox cbxSelectX;
            Label lbsohoadon;
            Label lbnamhoadon;
            Label lbkyhoadon;
            Label lbtongcong;
            Label lbID;


            string sohoadon = "";
            string namhoadon = "";
            string kyhoadon = "";
            string tongcong = "";
            string content = "";
            string sumall = "";
            //string KeyID = "";
            DataTable tblTrue = new DataTable();
            DataColumn Nam = new DataColumn("Nam");
            DataColumn Ky = new DataColumn("Ky");
            tblTrue.Columns.AddRange(new DataColumn[] { Nam, Ky });

            DataTable tblFalse = new DataTable();
            DataColumn Nam1 = new DataColumn("Nam");
            DataColumn Ky1 = new DataColumn("Ky");
            tblFalse.Columns.AddRange(new DataColumn[] { Nam1, Ky1 });
            int count =0;

            try
            {
                //check chọn kỳ trước-năm trước
                foreach (GridViewRow gvr in gvProductList.Rows)
                {
                    count+=1;
                    cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxSelect.Checked == false)
                    {
                        lbnamhoadon = (Label)gvr.Cells[1].FindControl("lblnamhoadon");
                        namhoadon = lbnamhoadon.Text.Trim();
                        lbkyhoadon = (Label)gvr.Cells[1].FindControl("lbkyhoadon");
                        kyhoadon = lbkyhoadon.Text.Trim();

                        DataRow row = tblFalse.NewRow();
                        row["Nam"] = namhoadon;
                        row["Ky"] = kyhoadon;
                        tblFalse.Rows.Add(row);
                    }
                    if (cbxSelect.Checked == true)
                    {

                        lbnamhoadon = (Label)gvr.Cells[1].FindControl("lblnamhoadon");
                        namhoadon = lbnamhoadon.Text.Trim();
                        lbkyhoadon = (Label)gvr.Cells[1].FindControl("lbkyhoadon");
                        kyhoadon = lbkyhoadon.Text.Trim();

                        DataRow row = tblTrue.NewRow();
                        row["Nam"] = namhoadon;
                        row["Ky"] = kyhoadon;
                        tblTrue.Rows.Add(row);
                    }
                   
                }
                if (tblTrue.Rows.Count == 0)
                {
                    lblTextError.Text = Resources.labels.bancanchonhoadondethanhtoan;
                    BindData();
                    return;
                }
                foreach (DataRow drTrue in tblTrue.Rows)
                {
                    foreach (DataRow drFalse in tblFalse.Rows)
                    {
                        if (SmartPortal.Common.Utilities.Utility.IsInt(drTrue["Nam"].ToString()) == SmartPortal.Common.Utilities.Utility.IsInt(drFalse["Nam"].ToString())) 
                        {
                            if (SmartPortal.Common.Utilities.Utility.IsInt(drTrue["Ky"].ToString()) < SmartPortal.Common.Utilities.Utility.IsInt(drFalse["Ky"].ToString()))
                            {
                                
                            }
                            if (SmartPortal.Common.Utilities.Utility.IsInt(drTrue["Ky"].ToString()) > SmartPortal.Common.Utilities.Utility.IsInt(drFalse["Ky"].ToString()))
                            {
                                lblTextError.Text = Resources.labels.bancanthanhtoanhoadoncutruockhithanhtoanhoadonmoi;
                                BindData();
                                return;
                            }
                            
                        }
                        if (SmartPortal.Common.Utilities.Utility.IsInt(drTrue["Nam"].ToString()) < SmartPortal.Common.Utilities.Utility.IsInt(drFalse["Nam"].ToString()))
                        {
                           
                        }

                        if (SmartPortal.Common.Utilities.Utility.IsInt(drTrue["Nam"].ToString()) > SmartPortal.Common.Utilities.Utility.IsInt(drFalse["Nam"].ToString())) 
                        {
                            lblTextError.Text = Resources.labels.bancanthanhtoanhoadoncutruockhithanhtoanhoadonmoi;
                            BindData();
                            return;
                        }
                    }
                }
                
                //get info receiver account
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();


                Hashtable hasReceiver = objAcct.loadInfobyAcct(ddlprovider.Text.Trim());
                if (hasReceiver[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    lblReceiverBranch.Text = hasReceiver[SmartPortal.Constant.IPC.BRANCHID].ToString();
                    lblReceiverAccount.Text = ddlprovider.Text.Trim();
                    lblReceiverName.Text = hasReceiver[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                }
                else
                {
                    lblTextError.Text = "Error : " + hasReceiver[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }
                //get info sender account
                Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.Text.Trim());
                if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    lblSenderName.Text = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                    balanceSender = SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                    lblSenderCCYID.Text = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
                    lblSenderBranch.Text = hasSender[SmartPortal.Constant.IPC.BRANCHID].ToString();
                    lblSenderAccount.Text = ddlSenderAccount.Text.Trim();
                    lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender, lblSenderCCYID.Text);
                }
                else
                {
                    lblTextError.Text = "Error : " + hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }


                #region get view KyhoaDon
                double sum = 0;
                KeyID = "";
                CultureInfo dk = new CultureInfo("en-US");
                foreach (GridViewRow gvr in gvProductList.Rows)
                {
                    cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxSelect.Checked == true)
                    {
                        lbsohoadon = (Label)gvr.Cells[1].FindControl("lbsohoadon");
                        sohoadon = lbsohoadon.Text.Trim();
                        lbnamhoadon = (Label)gvr.Cells[1].FindControl("lblnamhoadon");
                        namhoadon = lbnamhoadon.Text.Trim();
                        lbkyhoadon = (Label)gvr.Cells[1].FindControl("lbkyhoadon");
                        kyhoadon = lbkyhoadon.Text.Trim();
                        lbtongcong = (Label)gvr.Cells[1].FindControl("lbtongcong");
                        tongcong = lbtongcong.Text.Trim();
                        lbID = (Label)gvr.Cells[1].FindControl("lbID");
                        KeyID += lbID.Text.Trim() + "|" + lbsohoadon.Text.Trim() + "|" + SmartPortal.Common.Utilities.Utility.isDouble(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(tongcong, Resources.labels.lak), false) + "#";
                        sum += SmartPortal.Common.Utilities.Utility.isDouble(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(tongcong, Resources.labels.lak), false);

                        content += "<tr><td>" + sohoadon + "</td><td>" + namhoadon + "</td><td>" + kyhoadon + "</td><td>" + tongcong + "</td></tr>";

                    }
                    
                    //KeyID = KeyID.Substring(0, KeyID.Length - 1);
                    lblsumall.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(sum.ToString("0,0",dk), Resources.labels.lak);
                   
                    
                    lblsumallRS.Text = lblsumall.Text;
                }
                KeyID = KeyID.Substring(0, KeyID.Length - 1);
                #endregion

                // CHECK AMOUNT
                if (SmartPortal.Common.Utilities.Utility.isDouble(lblsumall.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(balanceSender, true))
                {
                    lblTextError.Text = Resources.labels.amountinvalid;
                    return;
                }
                #region tinh phi
                // tinh phi
                string phi = "0";
                DataTable dtFee = new SmartPortal.IB.Bank().GetWaterFee(Session["userID"].ToString(), "IB000308", SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblsumall.Text.Trim(), lblCurrency.Text), ddlSenderAccount.SelectedValue, lblReceiverBranch.Text.Trim(), lblCurrency.Text, "");

                if (dtFee.Rows.Count != 0)
                {
                    phi = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["FEEAMOUNT"].ToString(), acctCCYID);
                }
                #endregion

                #region assign value and visible panel
                lblcontent.Text = content;
                lblContentRS.Text = content;
                lblTextError.Text = "";
                lblPhi.Text = phi;

                lbldichvu.Text = lblService.Text;
                lblnhacungcap.Text = lblprovider.Text;
                lblmakhachhang.Text=lblCustCode.Text;
                lbltenkhachhang.Text= lblcustname.Text;
                lbldiachi.Text = lbladdress.Text;
                lblduong.Text = lblstreet.Text;

                lbltaikhoanthanhtoan.Text = ddlSenderAccount.SelectedItem.Text;
                lblsotien.Text = lblsumall.Text;
                lblnoidungthanhtoan.Text = txtDesc.Text;

                pnPayment.Visible = false;
                pnOTP.Visible = false;
                pnService.Visible = false;
                pnConfirm.Visible = true;
                #endregion


            }
            catch (Exception ex)
            {

            }
            //if (radOTP.Checked)
            //{
            //    txtOTP.Enabled = true;
            //    txtOTPBSMS.Enabled = false;
            //}
            //if (radOTPBSMS.Checked)
            //{
            //    txtOTP.Enabled = false;
            //    txtOTPBSMS.Enabled = true;
            //}
        }
        catch
        {
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            BindData();
            //pnPayment.Visible = true;
            //pnOTP.Visible = false;
            //pnService.Visible = false;
            //pnResultTransaction.Visible = false;
            //pnConfirm.Visible = false;
        }
        catch
        {
        }
    }
    protected void btnxacnhanOTP_Click(object sender, EventArgs e)
    {
        try
        {
            pnPayment.Visible = false;
            pnOTP.Visible = true;
            pnService.Visible = false;
            pnResultTransaction.Visible = false;
            pnConfirm.Visible = false;
            #region load otp
            SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
            DataTable dt = new DataTable();
            dt = objTran.LoadAuthenType(Session["userID"].ToString());
            ddlLoaiXacThuc.DataSource = dt;
            ddlLoaiXacThuc.DataTextField = "TYPENAME";
            ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
            ddlLoaiXacThuc.DataBind();
            #endregion
        }
        catch
        {
        }
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        Hashtable result = new Hashtable();
        try
        {
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                result = objAcct.WaterBillPayment(Session["userID"].ToString(), lblSenderAccount.Text, lblReceiverAccount.Text, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblsumall.Text, lblCurrency.Text.Trim()), lblSenderCCYID.Text, lblSenderName.Text, lblReceiverName.Text, lblSenderBranch.Text, lblReceiverBranch.Text, lblnoidungthanhtoan.Text, ddlLoaiXacThuc.SelectedValue, txtOTP.Text.Trim(), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblPhi.Text.Trim(), lblCurrency.Text.Trim()), ddlprovider.SelectedValue, ddlservice.SelectedValue, lblmakhachhang.Text, KeyID,lbldiachi.Text.Trim()+" "+lblduong.Text.Trim());
                
            if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                //ghi log reference qua ipclogtrans
                //new SmartPortal.IB.Payment().LogWaterBillPaymentFirst(lblSenderAccount.Text);
                ////Phản hồi cho cty nước
                //string[] ctrs = KeyID.Split('#');
                //foreach (string ctr in ctrs)
                //{
                //    string[] keybill = ctr.Split('|');
                //    bool rs;
                //    NuocChoLon.Service WS = new NuocChoLon.Service();
                //    WS.Url = System.Configuration.ConfigurationManager.AppSettings["WSWATERPAYMENT"].ToString();
                //    rs = WS.payW_Bill(keybill[0].ToString(), SmartPortal.Constant.IPC.PHUONGNAM, DateTime.Now.Day.ToString());
                //    if (rs == true)
                //    {
                //        new SmartPortal.IB.Payment().LogWaterBillPayment(keybill[0].ToString(), Session["UserName"].ToString(), "S", lblSenderAccount.Text, keybill[1].ToString(), keybill[2].ToString());
                //    }
                //    else
                //    {
                //        new SmartPortal.IB.Payment().LogWaterBillPayment(keybill[0].ToString(), Session["UserName"].ToString(), "F", lblSenderAccount.Text, keybill[1].ToString(), keybill[2].ToString());
                //    }
                //}

                //
                lblTextError.Text = Resources.labels.transactionsuccessful;
                long sotienbangchu = SmartPortal.Common.Utilities.Utility.isLong(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblsumall.Text, lblSenderCCYID.Text), false);
                txtChu.Value = SmartPortal.Common.Utilities.ConvertAmount.changeCurrencyToWords(sotienbangchu, lblSenderCCYID.Text, lblSenderCCYID.Text);
                //ghi vo session dung in
                Hashtable hasPrint = new Hashtable();
                hasPrint.Add("senderAccount", lblSenderAccount.Text);
                hasPrint.Add("senderBalance", lblBalanceSender.Text);
                hasPrint.Add("custcodewater", lblmakhachhang.Text);
                hasPrint.Add("address", lbldiachi.Text+" "+ lblduong.Text);
                hasPrint.Add("ccyid", lblSenderCCYID.Text);
                hasPrint.Add("senderName", lblSenderName.Text);
                hasPrint.Add("recieverAccount", lblReceiverAccount.Text);
                hasPrint.Add("recieverName", lblReceiverName.Text);
                //hasPrint.Add("transferType", lblHinhThuc.Text); 
                hasPrint.Add("bill", "<table style='width:100%' cellspacing='0' cellpadding='5' border='1'><tr><td style='width:25%'>" + Resources.labels.sohoadon + "</td><td style='width:25%'>" + Resources.labels.namhoadon + "</td><td style='width:25%'>" + Resources.labels.kyhoadon + "</td><td style='width:25%'>" + Resources.labels.sotien + "</td></tr>" + lblcontent.Text + "<table cellspacing='0' cellpadding='5'><tr><td style='width:10%'></td>   <td style='width:10%'></td> <td style='width:10%'></td> <td style=width:30%'>" + Resources.labels.tongcong + "</td><td style='width:40%'>" + lblsumallRS.Text +  " " + Resources.labels.lak +"</td></tr></table>");               //<td style='width:23%'></td>
                hasPrint.Add("amount", lblsumall.Text);
                hasPrint.Add("amountchu", txtChu.Value);
                hasPrint.Add("feeType", lblPhi.Text);
                hasPrint.Add("status", Resources.labels.thanhcong);
                hasPrint.Add("feeAmount", lblPhi.Text);
                hasPrint.Add("desc", lblnoidungthanhtoan.Text);
                hasPrint.Add("tranID", result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : "");
                hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                hasPrint.Add("senderBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblSenderBranch.Text));
                hasPrint.Add("receiverBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblReceiverBranch.Text));
                Session["printBill"] = hasPrint;

                btnPrint.Visible = true;
                //btnView.Visible = true;

                LayCCYID();
            }
            else
            {
               // btnPrint.Visible = false;
                txtOTP.Text = "";

                switch (result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                        lblTextError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                        lblTextError.Text = Resources.labels.wattingbankapprove;

                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                        lblTextError.Text = Resources.labels.wattinguserapprove;
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                        lblTextError.Text = Resources.labels.notregotp;
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                        lblTextError.Text = Resources.labels.authentypeinvalid;
                        return;
                    case "9908":
                        lblTextError.Text = Resources.labels.sotienvuothanmucgiaodich;
                        return;
                    case "9909":
                        lblTextError.Text = Resources.labels.sotienvuotquatonghanmuctrongngay;
                        return;
                    case "9907":
                        lblTextError.Text = Resources.labels.solangiaodichvuotquasolangiaodichtrongngay;
                        return;
                    case "2":
                        lblTextError.Text = Resources.labels.giaodich + " " + "thất bại";
                        return;
                    case "9":
                        lblTextError.Text = "Thanh toán thất bại";
                        return;
                    default:
                        throw new Exception();
                }
            }

            #region hien panel result
            pnPayment.Visible = false;
            pnOTP.Visible = false;
            pnService.Visible = false;
            pnResultTransaction.Visible = true;
            pnConfirm.Visible = false;

            lbldichvuRS.Text = lbldichvu.Text;
            lblnhacungcapRS.Text = lblnhacungcap.Text;
            lblmakhachhangRS.Text = lblmakhachhang.Text;
            lbltenkhachhangRS.Text = lbltenkhachhang.Text;
            lbldiachiRS.Text = lbldiachi.Text;
            lblduongRS.Text = lblduong.Text;

            lbltaikhoanthanhtoanRS.Text = lbltaikhoanthanhtoan.Text;
            lblsotienRS.Text = lblsotien.Text;
            lblnoidungthanhtoanRS.Text = lblnoidungthanhtoan.Text;
            #endregion
            txtOTP.Text = "";
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
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            pnPayment.Visible = true;
            pnOTP.Visible = false;
            pnService.Visible = false;
            pnResultTransaction.Visible = false;
        }
        catch
        {
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            pnPayment.Visible = false;
            pnOTP.Visible = false;
            pnService.Visible = false;
            pnResultTransaction.Visible = true;
        }
        catch
        {
        }
    }
    protected void gvProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lbsohoadon;
            Label lblnamhoadon, lbkyhoadon, lbltienphi;
            Label lbtienthue, lbbieuphi, lbdinhmuc, lbgiaban, lbtongcong,lbID;


            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                lbsohoadon = (Label)e.Row.FindControl("lbsohoadon");
                lblnamhoadon = (Label)e.Row.FindControl("lblnamhoadon");
                lbkyhoadon = (Label)e.Row.FindControl("lbkyhoadon");
                lbltienphi = (Label)e.Row.FindControl("lbltienphi");
                lbtienthue = (Label)e.Row.FindControl("lbtienthue");
                lbbieuphi = (Label)e.Row.FindControl("lbbieuphi");
                lbgiaban = (Label)e.Row.FindControl("lbgiaban");
                lbtongcong = (Label)e.Row.FindControl("lbtongcong");
                lbID = (Label)e.Row.FindControl("lbID");

                //hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                //hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                //lblstatus = (Label)e.Row.FindControl("lblstatus");
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                lbsohoadon.Text = drv["SHDON"].ToString();
                lblnamhoadon.Text = drv["NamHD"].ToString();
                lbkyhoadon.Text = drv["KyHD"].ToString();
                lbltienphi.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["PBVMT"].ToString(), Resources.labels.lak);
                lbtienthue.Text =  SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TGTGT"].ToString(), Resources.labels.lak);
                //lblproductid.Text = drv["PRODUCTID"].ToString();
                lbbieuphi.Text = drv["GBieu"].ToString();
                lbgiaban.Text = drv["TThu"].ToString();
                lbID.Text = drv["IDKey"].ToString();
                lbtongcong.Text =  SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TONGCONG"].ToString(), Resources.labels.lak);

            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void gvProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvProductList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvProductList_Sorting(object sender, GridViewSortEventArgs e)
    {
        //isSort = true;

        string sortExpression = e.SortExpression;

        ViewState["SortExpression"] = sortExpression;
        //showImage = true;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            //isAscend = true;

            SortGridView(sortExpression, ASCENDING);
            GridViewSortDirection = SortDirection.Descending;

        }

        else
        {
            isAscend = false;
            SortGridView(sortExpression, DESCENDING);
            GridViewSortDirection = SortDirection.Ascending;

        }
    }

    private SortDirection GridViewSortDirection
    {
        get
        {

            if (ViewState["sortDirection"] == null)

                ViewState["sortDirection"] = SortDirection.Ascending;


            return (SortDirection)ViewState["sortDirection"];

        }

        set { ViewState["sortDirection"] = value; }

    }

    protected void SortGridView(string sortExpression, string direction)
    {
        DataTable dataTable;

        if (ViewState["s"] != null)
        {
            dataTable = null;
        }

        else
        {
            if (Session["search"] != null)
            {
                dataTable = (new SmartPortal.SEMS.Fee().SearchContractFee("", "", "", "", "", "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            }
            else
            {
                dataTable = (new SmartPortal.SEMS.Fee().SearchContractFee("", "", "", "", "", "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            }
        }


        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvProductList.DataSource = dataView;
            gvProductList.DataBind();
        }

    }
    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        LayCCYID();
    }

    public void LayCCYID()
    {
        DataTable tblAcctnoInfo = new SmartPortal.IB.Account().GetAcctnoInfo(ddlSenderAccount.SelectedValue, Session["Userid"].ToString());
        if (tblAcctnoInfo.Rows.Count != 0)
        {
            lblCurrency.Text = tblAcctnoInfo.Rows[0]["CCYID"].ToString();
           
        }
        SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
        Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.Text.Trim());
        if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
        {

            lblsodutaikhoan.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), hasSender[SmartPortal.Constant.IPC.CCYID].ToString());
            lblsdtk.Text = lblsodutaikhoan.Text;
            lblsdtk1.Text = lblsodutaikhoan.Text;

            lblCCYIDDD.Text = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
            lblCCYIDDDC.Text = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
            lblCCYIDDDC1.Text = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
            lblCCYIDDDR.Text = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
            lblCCYIDDDR1.Text = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
        }
        else
        {
            lblTextError.Text = "Error : " + hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
            return;
        }
    }
    protected void ddlservice_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtProvider = new DataTable();
        dtProvider = new SmartPortal.IB.Payment().GetPaymentProvider(ddlservice.SelectedValue);
       
        //load provider...
        //DataTable dtPR = new DataTable();
        //dtPR = new SmartPortal.IB.Payment().GetProvider();
        if (dtProvider.Rows.Count == 0)
        {
            ddlprovider.Items.Clear();
            ddlprovider.Items.Insert(0, new ListItem(Resources.labels.khongconhacungcapnao, ""));
        }
        else
        {
            ddlprovider.DataSource = dtProvider;
            ddlprovider.DataTextField = "Paymentprovidername";
            ddlprovider.DataValueField = "Paymentprovideraccount";
            ddlprovider.DataBind();
        }

    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=121"));
    }
}
