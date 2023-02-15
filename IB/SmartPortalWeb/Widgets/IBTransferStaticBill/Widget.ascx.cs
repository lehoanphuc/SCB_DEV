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

using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBTransferStaticBill_Widget : WidgetBase
{
    //string senderName = "";
    //string receiverName = "";
    //string balanceSender = "";
    //string acctCCYID = "";
    string IPCERRORCODE;
    string IPCERRORDESC;
    DataSet dsReceiverList = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";

            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
            load_unit(sender, e);

            if (radTS.Checked)
            {
                txtTS.Enabled = true;
            }
            else
            {
                txtTS.Enabled = false;
            }

            //hide panel
            if (!IsPostBack)
            {
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void load_unit(object sender, EventArgs e)
    {
        try
        {
            string errorcode = "";
            string errorDesc = "";
            DataSet ds = new DataSet();
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
            ds = objAcct.getAccount(Session["userID"].ToString(), "IB000208", "DD", ref errorcode, ref errorDesc);
            dsReceiverList = objAcct.GetReceiverList("BILLPAYMENT", SmartPortal.Constant.IPC.TIB);

            //string isSendReceiver = dsReceiverList.Tables[1].Rows[0]["ISRECEIVERLIST"].ToString();
            //if (isSendReceiver.Equals("N"))
            //{
            //    DataRow row = dsReceiverList.Tables[0].NewRow();
            //    row["ID"] = "";
            //    row["RECEIVERNAME"] = " other ";
            //    dsReceiverList.Tables[0].Rows.InsertAt(row, 0);
            //}
            //else
            //{
            //    if (dsReceiverList == null || dsReceiverList.Tables.Count == 0 || dsReceiverList.Tables[0].Rows.Count == 0)
            //    {
            //        throw new IPCException("4012");

            //    }
            //}

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                throw new BusinessExeption("User not register DD Account To Transfer.");
                return;
            }

            if (!IsPostBack)
            {

                ddlSenderAccount.DataSource = ds;
                ddlSenderAccount.DataValueField = ds.Tables[0].Columns[SmartPortal.Constant.IPC.ACCTNO].ColumnName.ToString();
                ddlSenderAccount.DataBind();

                ddlNguoiThuHuong.DataSource = dsReceiverList.Tables[0];
                //add to common later
                ddlNguoiThuHuong.DataTextField = dsReceiverList.Tables[0].Columns["RECEIVERNAME"].ColumnName.ToString();
                ddlNguoiThuHuong.DataValueField = dsReceiverList.Tables[0].Columns["ID"].ColumnName.ToString();
                ddlNguoiThuHuong.DataBind();

                ddlNguoiThuHuong_SelectedIndexChanged(sender, e);

                //lay CCYID
                LayCCYID();

                //Load Template của QuyenNPV

                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"] != null)
                {
                    string TID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tid"].ToString();
                    DataSet TemplateDS = new DataSet();
                    TemplateDS = new SmartPortal.IB.Transfer().LoadtemplateByID(TID, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (TemplateDS.Tables[0].Rows.Count != 0)
                    {
                        DataTable TemplateTB = TemplateDS.Tables[0];
                        ddlSenderAccount.SelectedValue = TemplateTB.Rows[0]["SENDERACCOUNT"].ToString();
                        //ddlNguoiThuHuong.SelectedItem.Text = TemplateTB.Rows[0]["RECEIVERNAME"].ToString();
                        txtReceiverAccount.Text = TemplateTB.Rows[0]["RECEIVERACCOUNT"].ToString();
                        ddlNguoiThuHuong.SelectedValue = TemplateTB.Rows[0]["RECEIVERID"].ToString();
                        txtAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(TemplateTB.Rows[0]["AMOUNT"].ToString(), TemplateTB.Rows[0]["CCYID"].ToString());
                        lblCurrency.Text = TemplateTB.Rows[0]["CCYID"].ToString();
                        txtDesc.Text = TemplateTB.Rows[0]["DESCRIPTION"].ToString();
                    }
                }
                //Load Template của QuyenNPV
            }
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], Request.Url.Query);
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
    protected void btnTIBNext_Click(object sender, EventArgs e)
    {
        string senderName = "";
        string receiverName = "";
        string balanceSender = "";
        string acctCCYID = "";
        string receiverCCYID = "";
        txtDesc.Text = "BILL PAYMENT: " + ddlNguoiThuHuong.SelectedItem.Text + " - " + txtSoHD.Text + " - " + txtKyHD.Text;
        try
        {
            if ((cbmau.Checked == true && txttenmau.Text.ToString().Trim() != "") || (cbmau.Checked == false))
            {
                //CHECK RECEIVER ACCOUNT IS EXISTS
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                DataSet dsAcct = objAcct.CheckAccountExists(txtReceiverAccount.Text.Trim().ToString());
                if (dsAcct.Tables.Count != 0)
                {
                    if (dsAcct.Tables[0].Rows.Count > 0)
                    {
                        receiverName = dsAcct.Tables[0].Rows[0][SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                        receiverCCYID = dsAcct.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                    }
                    else
                    {
                        lblTextError.Text = "Provider not found";// Resources.labels.destacccountinvalid;
                        return;
                    }
                }
                else
                {
                    lblTextError.Text = "Provider not found";// Resources.labels.destacccountinvalid;
                    return;
                }

                Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.Text.Trim());
                if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    senderName = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                    balanceSender = SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                    acctCCYID = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
                    lblSenderBranch.Text = hasSender[SmartPortal.Constant.IPC.BRANCHID].ToString();
                }
                else
                {
                    lblTextError.Text = "Error : " + hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }

                Hashtable hasReceiver = objAcct.loadInfobyAcct(txtReceiverAccount.Text.Trim());
                if (hasReceiver[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    lblReceiverBranch.Text = hasReceiver[SmartPortal.Constant.IPC.BRANCHID].ToString();
                }
                else
                {
                    lblTextError.Text = "Error : " + hasReceiver[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }
                // CHECK SAME CCYCD
                bool sameCCYCD = objAcct.CheckSameCCYCD(acctCCYID, receiverCCYID);
                if (!sameCCYCD)
                {
                    lblTextError.Text = Resources.labels.invalidacctCCYCDtotransfer;
                    return;
                }

                // CHECK AMOUNT
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(balanceSender, true))
                {
                    lblTextError.Text = Resources.labels.amountinvalid;
                    return;
                }
                // CHECK SAME ACCOUNT
                if (!txtReceiverAccount.Text.Equals(""))
                {
                    bool sameAcct = objAcct.CheckSameAccount(ddlSenderAccount.Text.ToString(), txtReceiverAccount.Text.Trim().ToString());
                    if (!sameAcct)
                    {
                        lblTextError.Text = Resources.labels.Accountnotsame;
                        return;
                    }
                }

                #region LOAD INFO
                lblSenderAccount.Text = ddlSenderAccount.Text;
                lblSenderName.Text = senderName;
                lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender, acctCCYID);
                lblSenderCCYID.Text = acctCCYID;
                lblFeeCCYID.Text = acctCCYID;
                lbCCYID.Text = acctCCYID;

                lblReceiverAccount.Text = txtReceiverAccount.Text;
                lblReceiverName.Text = receiverName;

                if (radTS1.Checked == true)
                {
                    //lblHinhThuc.Text = radTS1.Text;
                }
                else
                {
                    string scheduleTime = txtTS.Text.ToString() + " " + ConfigurationManager.AppSettings.Get("SCHEDULETIME");
                    if (Convert.ToDateTime(scheduleTime) < DateTime.Now)
                    {
                        lblTextError.Text = Resources.labels.ngaygiaodichkhonghople;
                        return;
                    }
                    //lblHinhThuc.Text = txtTS.Text.Trim().ToString();
                }

                #region check same name template transfer
                if (cbmau.Checked == true && txttenmau.Text.ToString().Trim() != "")
                {
                    new SmartPortal.IB.Transfer().CheckNameTransferTemplate(Utility.KillSqlInjection(txttenmau.Text.Trim()), Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {

                    }
                    else
                    {
                        if (IPCERRORDESC == "110211")
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.SAMENAMETRANSFERTEMPLATE);

                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                        }
                    }
                }
                #endregion

                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), acctCCYID);
                lblPhi.Text = rdNguoiChiuPhi.SelectedValue;
                lblDesc.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text);

                #region tinh phi
                string phi = "0";
                DataTable dtFee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), "IB000208", SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ddlSenderAccount.SelectedValue, lblReceiverBranch.Text.Trim(), lblCurrency.Text, "");

                if (dtFee.Rows.Count != 0)
                {
                    phi = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["FEEAMOUNT"].ToString(), acctCCYID);
                }
                #endregion

                lblPhiAmount.Text = phi;
                lblTextError.Text = "";
                pnConfirm.Visible = true;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = false;

                #endregion



            }
            else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.UNNAMEDTRANSFERTEMPLATE);
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
    protected void ddlNguoiThuHuong_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            DataRow[] dr = dsReceiverList.Tables[0].Select("ID = '" + ddlNguoiThuHuong.SelectedValue.ToString() + "'");
            if (dr.Length > 0 && !dr[0]["ID"].Equals(""))
            {
                txtReceiverAccount.Text = dr[0][SmartPortal.Constant.IPC.ACCTNO].ToString();
                txtReceiverAccount.Enabled = false;
            }
            else
            {
                txtReceiverAccount.Text = "";
                txtReceiverAccount.Enabled = true;
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(Session["userLevel"].ToString().Trim()) > 2)
            {
                btnApply.Text = Resources.labels.transfer;
                //chuyen khoan luon doi voi user doanh nghiep level3 tro len
                btnAction_Click(sender, e);
            }
            else
            {
                btnApply.Text = Resources.labels.confirm;
                pnConfirm.Visible = false;
                pnOTP.Visible = true;
                pnTIB.Visible = false;

                SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
                DataTable dt = new DataTable();
                dt = objTran.LoadAuthenType(Session["userID"].ToString());
                ddlLoaiXacThuc.DataSource = dt;
                ddlLoaiXacThuc.DataTextField = "TYPENAME";
                ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
                ddlLoaiXacThuc.DataBind();
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBTransferInBank1_Widget", "btnApply_Click", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferInBank1_Widget", "btnApply_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        Hashtable result = new Hashtable();
        try
        {
            if (radTS1.Checked)
            {
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                //result = objAcct.TransferDDOtherCust(Session["userID"].ToString(), lblSenderAccount.Text, lblReceiverAccount.Text, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()), lblSenderCCYID.Text, lblSenderName.Text, lblReceiverName.Text, lblSenderBranch.Text, lblReceiverBranch.Text, lblDesc.Text, ddlLoaiXacThuc.SelectedValue, txtOTP.Text.Trim(), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblPhiAmount.Text.Trim(), lblCurrency.Text.Trim()),lblPhi.Text);
            }
            else
            {
                SmartPortal.IB.Schedule objSchedule = new SmartPortal.IB.Schedule();
                string scheduleID = SmartPortal.Constant.IPC.SCHEDULEPREFIX + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
                object[] ipcSchedules = objSchedule.createObjSchedules(scheduleID, "ONETIME", " " + ConfigurationManager.AppSettings.Get("SCHEDULETIME"),
                                                                    "RUN_SCHEDULE", lblDesc.Text.ToString(), "A", Session["userID"].ToString(),
                                                                    Session["userID"].ToString(), "Y", "IB", "", "IB000208", "",
                                                                    "", "");

                DataTable dtInfoTran = new DataTable();
                dtInfoTran.Columns.Add("SCHEDULEID");
                dtInfoTran.Columns.Add("PARANAME");
                dtInfoTran.Columns.Add("PARAVALUE");
                dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.SOURCEID, "IB");
                dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.USERID, Session["userID"].ToString());
                dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.ACCTNO, lblSenderAccount.Text);
                dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.RECEIVERACCOUNT, lblReceiverAccount.Text);
                dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.AMOUNT, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblAmount.Text, lblCurrency.Text.Trim()));
                dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.CCYID, lblSenderCCYID.Text);
                dtInfoTran.Rows.Add(scheduleID, SmartPortal.Constant.IPC.DESC, lblDesc.Text);
                object[] ipcScheduleDetails = objSchedule.createObjScheduleDetails(dtInfoTran);

                result = objSchedule.InsertSchedule(ipcSchedules, ipcScheduleDetails);
            }



            if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {

                lblTextError.Text = Resources.labels.transactionsuccessful;
                if (cbmau.Checked == true)
                {
                    string fee = rdNguoiChiuPhi.SelectedIndex.ToString();
                    new SmartPortal.IB.Transfer().InsertTransferTemplate(txttenmau.Text, txtDesc.Text, ddlSenderAccount.Text, lblReceiverAccount.Text, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()), lblCurrency.Text, SmartPortal.Constant.IPC.TIB, Session["userID"].ToString(), "0", "", fee, "", "", "", "", "", lblReceiverName.Text, lblSenderName.Text, "", "", "", "", ddlNguoiThuHuong.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblTextError.Text += " & " + Resources.labels.luumauchuyenkhoanthanhcong;
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                    }
                }

               

                btnPrint.Visible = true;
                btnView.Visible = true;
            }
            else
            {
                txtOTP.Text = "";
                btnPrint.Visible = false;

                switch (result[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                        lblTextError.Text = result[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                        lblTextError.Text = Resources.labels.wattingbankapprove;
                        if (cbmau.Checked == true)
                        {
                            string fee = rdNguoiChiuPhi.SelectedIndex.ToString();
                            new SmartPortal.IB.Transfer().InsertTransferTemplate(txttenmau.Text, txtDesc.Text, ddlSenderAccount.Text, lblReceiverAccount.Text, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()), lblCurrency.Text, SmartPortal.Constant.IPC.TIB, Session["userID"].ToString(), "0", "", fee, "", "", "", "", "", lblReceiverName.Text, lblSenderName.Text, "", "", "", "", ddlNguoiThuHuong.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE == "0")
                            {
                                lblTextError.Text += " & " + Resources.labels.luumauchuyenkhoanthanhcong;
                            }
                            else
                            {
                                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                            }
                        }

                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                        lblTextError.Text = Resources.labels.wattinguserapprove;
                        if (cbmau.Checked == true)
                        {
                            string fee = rdNguoiChiuPhi.SelectedIndex.ToString();
                            new SmartPortal.IB.Transfer().InsertTransferTemplate(txttenmau.Text, txtDesc.Text, ddlSenderAccount.Text, lblReceiverAccount.Text, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()), lblCurrency.Text, SmartPortal.Constant.IPC.TIB, Session["userID"].ToString(), "0", "", fee, "", "", "", "", "", lblReceiverName.Text, lblSenderName.Text, "", "", "", "", ddlNguoiThuHuong.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE == "0")
                            {
                                lblTextError.Text += " & " + Resources.labels.luumauchuyenkhoanthanhcong;
                            }
                            else
                            {
                                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                            }
                        }


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
                    default:
                        throw new Exception();
                }


                pnConfirm.Visible = true;
                btnApply.Enabled = false;
                btnBackTransfer.Enabled = false;
                btnView.Visible = false;
            }

            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = true;


            #region LOAD RESULT TRANSFER
            string errorCode = "0";
            string errorDesc = string.Empty;
            SmartPortal.IB.Account acct = new SmartPortal.IB.Account();
            DataSet dsDetailAcc = new DataSet();
            dsDetailAcc = acct.GetInfoDD(Session["userID"].ToString(), lblSenderAccount.Text.Trim(), ref errorCode, ref errorDesc);

            lblEndSenderAccount.Text = lblSenderAccount.Text;
            lblEndBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(dsDetailAcc.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString()), lblSenderCCYID.Text.Trim());
            lblEndSenderName.Text = lblSenderName.Text;

            lblEndReceiverAccount.Text = lblReceiverAccount.Text;
            lblEndReceiverName.Text = lblReceiverName.Text;
            //lblEndHinhThuc.Text = lblHinhThuc.Text;
            lblEndAmount.Text = lblAmount.Text;
            lblEndPhi.Text = lblPhi.Text;
            lblEndPhiAmount.Text = lblPhiAmount.Text;
            lblEndDesc.Text = lblDesc.Text;
            lblEndPhiAmount.Text = lblPhiAmount.Text;

            lblBalanceCCYID.Text = lblSenderCCYID.Text;
            lblAmountCCYID.Text = lblSenderCCYID.Text;
            lblEndFeeCCYID.Text = lblSenderCCYID.Text;
            #endregion
            txtOTP.Text = "";
            //ghi vo session dung in
            Hashtable hasPrint = new Hashtable();

            hasPrint.Add("senderAccount", lblSenderAccount.Text);
            hasPrint.Add("ccyid", lblSenderCCYID.Text);
            hasPrint.Add("senderName", lblSenderName.Text);
            hasPrint.Add("providername", lblEndReceiverName.Text);

            hasPrint.Add("recieverAccount", lblReceiverAccount.Text);
            hasPrint.Add("recieverName", lblReceiverName.Text);
            hasPrint.Add("amount", lblAmount.Text);
            hasPrint.Add("amountchu", txtChu.Value.ToString());
            hasPrint.Add("feeType", lblPhi.Text);
            hasPrint.Add("feeAmount", lblPhiAmount.Text);
            hasPrint.Add("narrative", lblEndDesc.Text);
            hasPrint.Add("tranID", result["IPCTRANSID"] !=null ? result["IPCTRANSID"].ToString():"");
            hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            Session["printBill"] = hasPrint;

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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnBackTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnOTP.Visible = false;
            pnTIB.Visible = true;
            pnResultTransaction.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=89"));
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
    }

}
