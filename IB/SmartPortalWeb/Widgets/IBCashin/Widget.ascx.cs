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
using SmartPortal.IB;

public partial class Widgets_IBCashin_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
            if (!IsPostBack)
            {
                pnAmount.Visible = true;
                pnConfirm.Visible = false;
                pnResult.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();
            lblAmount.Text = Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), "LAK");
            hdAmount.Value = Utility.isDouble(txtAmount.Text.Trim(), true).ToString();

            hasInput = new Hashtable(){
                {"IPCTRANCODE", "MB_GETLINKANDFEE_CIC"},
                {"SERVICEID", "IB"},
                {"SOURCEID", "IB"},
                {"AMOUNT", hdAmount.Value},
                {"CARDTYPE", "CC"},
                {"TRANCODE", "IBWLCASHIN"},
                {"USERID", Session["userID"].ToString()}
            };

            hasOutput = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                lblFeeAmount.Text = Utility.FormatMoneyInputToView(hasOutput["FEE"].ToString(), "LAK");
                hdTransID.Value = hasOutput["TRANREF"].ToString();

                double totalAmount = Utility.isDouble(txtAmount.Text.Trim(), true) - Utility.isDouble(hasOutput["FEE"].ToString(), true);
                lblTotalAmount.Text = Utility.FormatMoney(totalAmount.ToString(), "LAK");

                lbAmountCCYID.Text = lblFeeCCYID.Text = lblTotalAmountCCYID.Text = "LAK";
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }

            hasInput = new Hashtable(){
                {"IPCTRANCODE", "PAYMENT_GETSESSION"},
                {"SERVICEID", "IB"},
                {"SOURCEID", "IB"},
                {"API", "CREATE_CHECKOUT_SESSION"},
                {"AMOUNT", hdAmount.Value},
                {"INVOINO", hdTransID.Value},
                {"TRANCODE", "IBWLCASHIN"},
                {"USERID", Session["userID"].ToString()}
            };
            hasOutput = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                hdSession.Value = hasOutput["SESSION"].ToString();
                hdMerchant.Value = hasOutput["MERCHANT"].ToString();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }

            pnAmount.Visible = false;
            pnConfirm.Visible = true;
            pnResult.Visible = false;
            btnApply.Enabled = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnBackAmount_OnClick(object sender, EventArgs e)
    {
        pnAmount.Visible = true;
        pnConfirm.Visible = false;
        pnResult.Visible = false;
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        pnAmount.Visible = false;
        pnConfirm.Visible = true;
        pnResult.Visible = false;
        btnApply.Enabled = false;
        Session["INVOINO"] = hdTransID.Value;
        Session["SESSIONCASHIN"] = hdSession.Value;
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["print"] == null)
            {
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["print"] == null)
            {
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=IBWLCASHIN"));
    }

    protected void btnDoTrans_Click(object sender, EventArgs e)
    {
        try
        {
            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();
            if(Session["INVOINO"] != null && hdTransID.Value.Trim() == ""){
                hdTransID.Value = Session["INVOINO"].ToString();
            }
            if (Session["SESSIONCASHIN"] != null && hdSession.Value.Trim() == "")
            {
                hdSession.Value = Session["SESSIONCASHIN"].ToString();
            }
            Session["INVOINO"] = null;
            Session["SESSIONCASHIN"] = null;
            hasInput = new Hashtable(){
                {"IPCTRANCODE", "IBWLCASHIN"},
                {"SERVICEID", "IB"},
                {"SOURCEID", "IB"},
                {"API", "RETRIEVE_ORDER"},
                {"INVOINO", hdTransID.Value.Trim()},
                {"USERID", Session["userID"].ToString()},
                {"SESSION", hdSession.Value.Trim()}
            };

            hasOutput = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
            if (!IPCERRORCODE.Equals("0"))
            {
                lblError.Text = IPCERRORDESC;
                return;
            }

            hasInput = new Hashtable(){
                {"IPCTRANCODE", "MB_TRANRESULT_CIC"},
                {"SERVICEID", "IB"},
                {"SOURCEID", "IB"},
                {"TRANSREF", hdTransID.Value.Trim()},
                {"USERID", Session["userID"].ToString()},
            };
            hasOutput = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                lblError.Text = Resources.labels.transactionsuccessful;
                lblEndSenderName.Text = hasOutput["FULLNAME"].ToString();
                lblEndSenderAccount.Text = hasOutput["ACCTNO"].ToString();
                lblEndReceiverName.Text = hasOutput["RECEIVERNAME"].ToString();
                lblEndReceiverAccount.Text = hasOutput["RECEIVERACCOUNT"].ToString();
                lblEndTransactionNo.Text = hasOutput["TRANSREF"].ToString();
                lblEndDateTime.Text = hasOutput["TRANTIME"] != null ? hasOutput["TRANTIME"].ToString() : DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                lblInvoiceNumber.Text = hasOutput["INVOICEID"].ToString();

                lblEndAmount.Text = Utility.FormatMoney((Utility.isDouble(hasOutput["AMOUNT"].ToString(), true) + Utility.isDouble(hasOutput["FEE"].ToString(), true)).ToString(), "LAK");
                lblEndFeeAmount.Text = Utility.FormatMoney(hasOutput["FEE"].ToString(), "LAK");
                lblEndTotalAmount.Text = Utility.FormatMoney(hasOutput["AMOUNT"].ToString(), "LAK");
                lblEndAmountCCYID.Text = lblEndFeeCCYID.Text = lblEndTotalAmountCCYID.Text = "LAK";
                lblEndDesc.Text = "Cash in by card";

                Hashtable hasPrint = new Hashtable();
                hasPrint.Add("tranDate", lblEndDateTime.Text);
                hasPrint.Add("tranID", lblEndTransactionNo.Text);
                hasPrint.Add("senderName", lblEndSenderName.Text);
                hasPrint.Add("senderAccount", lblEndSenderAccount.Text);
                hasPrint.Add("recieverName", lblEndReceiverName.Text);
                hasPrint.Add("receiverAccount", lblEndReceiverAccount.Text);
                hasPrint.Add("invoiceNumber", lblInvoiceNumber.Text);
                hasPrint.Add("amount", lblEndAmount.Text);
                hasPrint.Add("ccyid", "LAK");
                hasPrint.Add("feeAmount", lblFeeAmount.Text);
                hasPrint.Add("totalAmount", lblTotalAmount.Text);
                hasPrint.Add("desc", lblEndDesc.Text);
                hasPrint.Add("status", Resources.labels.thanhcong);
                Session["print"] = hasPrint;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }

            pnAmount.Visible = false;
            pnConfirm.Visible = false;
            pnResult.Visible = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
