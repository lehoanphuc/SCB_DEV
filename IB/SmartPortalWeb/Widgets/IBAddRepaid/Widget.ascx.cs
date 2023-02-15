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

public partial class Widgets_IBAddRepaid_Widget : WidgetBase
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


            //hide panel
            if (!IsPostBack)
            {
                pnConfirm.Visible = false;
                pnOTP.Visible = false;
                pnResultTransaction.Visible = false;
            }
        }
        catch(Exception ex )
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

                //lay CCYID
                LayCCYID();
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
        string balanceSender = "";
        string acctCCYID = "";

        try
        {
                
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                //check validate mastercard
                Hashtable hasValidate = objAcct.CheckValidateRepaidMasterCard(txtCardNo.Text.Trim(), txtCardHolder.Text.Trim());
                if (hasValidate[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {

                }
                else
                {
                    lblTextError.Text = Resources.labels.sothehoactenchuthekhonghople;
                    return;
                }
                //get info sender
                Hashtable hasSender = objAcct.loadInfobyAcct(ddlSenderAccount.Text.Trim());
                if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    senderName = hasSender[SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                    balanceSender =SmartPortal.Common.Utilities.Utility.FormatStringCore(hasSender[SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString());
                    acctCCYID = hasSender[SmartPortal.Constant.IPC.CCYID].ToString();
                    lblSenderBranch.Text = hasSender[SmartPortal.Constant.IPC.BRANCHID].ToString();
                }
                else
                {
                    lblTextError.Text = "Error : " + hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }
                //CHECK RECEIVER ACCOUNT IS EXISTS
                DataSet dsAcct = objAcct.CheckAccountExists(System.Configuration.ConfigurationManager.AppSettings["PULLACCOUNT"].ToString());
                if (dsAcct.Tables.Count != 0)
                {
                    if (dsAcct.Tables[0].Rows.Count > 0)
                    {
                        lblReceiverName.Value = dsAcct.Tables[0].Rows[0][SmartPortal.Constant.IPC.CUSTOMERNAME].ToString();
                        // receiverCCYID = dsAcct.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                    }
                    else
                    {
                        lblTextError.Text = Resources.labels.destacccountinvalid;
                        return;
                    }
                }
                else
                {
                    lblTextError.Text = Resources.labels.destacccountinvalid;
                    return;
                }
                //CHECK ReceiverBranch
                Hashtable hasReceiver = objAcct.loadInfobyAcct(System.Configuration.ConfigurationManager.AppSettings["PULLACCOUNT"].ToString());
                if (hasReceiver[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {
                    lblReceiverBranch.Value = hasReceiver[SmartPortal.Constant.IPC.BRANCHID].ToString();
                }
                else
                {
                    lblTextError.Text = "Error : " + hasReceiver[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                    return;
                }
                #region tinh phi
                string phi = "0";
                DataTable dtFee = new SmartPortal.IB.Bank().GetFee(Session["userID"].ToString(), "IB000208", SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ddlSenderAccount.SelectedValue, lblReceiverBranch.Value.Trim(), lblCurrency.Text, "");

                if (dtFee.Rows.Count != 0)
                {
                    phi = SmartPortal.Common.Utilities.Utility.FormatMoney(dtFee.Rows[0]["FEEAMOUNT"].ToString(), acctCCYID);
                }
                #endregion

                lblPhiAmount.Value = phi;
                lblTextError.Text = "";
                #region LOAD INFO
                lblSenderAccount.Text = ddlSenderAccount.Text;
                lblSenderName.Text = senderName;
                lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender, acctCCYID);
                lblSenderCCYID.Text = acctCCYID;
                lbCCYID.Text = acctCCYID;

                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(txtAmount.Text.Trim(),acctCCYID); 
                lblDesc.Text =SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCardNo.Text.Trim());
                lblCardNo.Text = txtCardNo.Text;
                lblCardHolder.Text = txtCardHolder.Text;
                lblTextError.Text = "";
                pnConfirm.Visible = true;
                pnOTP.Visible = false;
                pnTIB.Visible = false;
                pnResultTransaction.Visible = false;

                #endregion
                

               
            
 
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
                SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();
                result = objAcct.TransferDDAddRepaidMasterCard(Session["userID"].ToString(), lblSenderAccount.Text, System.Configuration.ConfigurationManager.AppSettings["PULLACCOUNT"].ToString(),
                    SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text, lblCurrency.Text.Trim()), lblSenderCCYID.Text, lblSenderName.Text, lblReceiverName.Value, lblSenderBranch.Text, lblReceiverBranch.Value, lblDesc.Text, ddlLoaiXacThuc.SelectedValue, txtOTP.Text.Trim(), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblPhiAmount.Value.Trim(), lblCurrency.Text.Trim()));

            

            
            if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                
                lblTextError.Text = Resources.labels.transactionsuccessful;

                //ghi vo session dung in
                Hashtable hasPrint = new Hashtable();
                hasPrint.Add("status", Resources.labels.thanhcong);
                hasPrint.Add("senderAccount", lblSenderAccount.Text);
                hasPrint.Add("senderBalance", lblBalanceSender.Text);
                hasPrint.Add("ccyid", lblSenderCCYID.Text);
                hasPrint.Add("senderName", lblSenderName.Text);
                hasPrint.Add("recieverAccount", lblCardNo.Text);
                hasPrint.Add("recieverName", lblCardHolder.Text);
                //hasPrint.Add("transferType", lblHinhThuc.Text);
                hasPrint.Add("amount", lblAmount.Text);
                hasPrint.Add("amountchu", txtChu.Value.ToString());
                hasPrint.Add("desc", lblDesc.Text);
                hasPrint.Add("tranID", result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : "");
                hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                hasPrint.Add("senderBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblSenderBranch.Text));
                //hasPrint.Add("receiverBranch", SmartPortal.Common.Utilities.Utility.FormatStringCore(lblReceiverBranch.Text));
                Session["print"] = hasPrint;

                //btnPrint.Visible = true;
                //btnView.Visible = true;
            }
            else
            {
                txtOTP.Text = "";
                //btnPrint.Visible = false;

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
                    default:
                        throw new Exception();
                }           
                
                              
                pnConfirm.Visible = true;
                btnApply.Enabled = false;
                btnBackTransfer.Enabled = false;
               // btnView.Visible = false;
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

            lblCardNoEnd.Text = lblCardNo.Text;
            lblCardHolderEnd.Text = lblCardHolder.Text;
            //lblEndHinhThuc.Text = lblHinhThuc.Text;
            lblEndAmount.Text = lblAmount.Text;
            lblEndDesc.Text = lblDesc.Text;

            lblBalanceCCYID.Text = lblSenderCCYID.Text;
            lblAmountCCYID.Text = lblSenderCCYID.Text;
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = true;
            pnOTP.Visible = false;
            pnTIB.Visible = false;
            pnResultTransaction.Visible = false;
        }
        catch ( Exception ex )
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
        catch ( Exception ex )
        {
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=416"));
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
