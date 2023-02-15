using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSCashcodemanager_Control_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string SOURCETYPE
    {
        get { return ViewState["SOURCETYPE"] != null ? ViewState["SOURCETYPE"].ToString() : ""; }
        set { ViewState["SOURCETYPE"] = value; }
    }
    string MVID
    {
        get { return ViewState["MVID"] != null ? ViewState["MVID"].ToString() : ""; }
        set { ViewState["MVID"] = value; }
    }
    string USERCREATED
    {
        get { return ViewState["USERCREATED"] != null ? ViewState["USERCREATED"].ToString() : ""; }
        set { ViewState["USERCREATED"] = value; }
    }
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlCCYID.DataTextField = "CCYID";
                ddlCCYID.DataValueField = "CCYID";
                ddlCCYID.DataBind();
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
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnDetail.Enabled = false;
                    divDetail.Visible = true;
                    pnCancel.Visible = false;
                    pnResend.Visible = false;
                    break;
                case IPC.ACTIONPAGE.REJECT:
                    pnDetail.Enabled = false;
                    divDetail.Visible = false;
                    break;
            }
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            string[] key = ID.Split('|');
            MVID = key[0];
            if (key[1] == "RESEND")
            {
                lblTitleProduct.Text = Resources.labels.resendcashcode;
                pnResend.Visible = true;
                pnCancel.Visible = false;
            }
            else if (key[1] == "CANCEL")
            {
                lblTitleProduct.Text = Resources.labels.cancelcashcode;
                pnCancel.Visible = true;
                pnResend.Visible = false;
            }
            else
            {
                lblTitleProduct.Text = Resources.labels.cashcodechitiet;
            }
            DataSet ds = new DataSet();
            DataTable senderTable = new DataTable();
            DataTable receiverTable = new DataTable();
            ds = new SmartPortal.SEMS.User().CashCodeViewDetails(MVID, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORCODE);
            }
            else
            {
                senderTable = ds.Tables[0];
                receiverTable = ds.Tables[1];
            }
            if (senderTable.Rows.Count != 0)
            {
                txtTransID.Text = senderTable.Rows[0]["TXREF"].ToString();
                txtDateCreate.Text = Convert.ToDateTime(senderTable.Rows[0]["DATECREATED"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                txtSenderPhone.Text = senderTable.Rows[0]["SENDERPHONE"].ToString();
                txtSenderName.Text = senderTable.Rows[0]["SENDERNAME"].ToString();
                txtPaperNumber.Text = senderTable.Rows[0]["SENDERPAPERNUMBER"].ToString();
                txtSenderAddress.Text = senderTable.Rows[0]["SENDERADDRESS"].ToString();

                txtReceiverPhone.Text = receiverTable.Rows[0]["RECEIVERPHONE"].ToString();
                txtReceiverName.Text = receiverTable.Rows[0]["RECEIVERNAME"].ToString();
                txtReceiverPaperNumber.Text = receiverTable.Rows[0]["RECEIVERPAPERNUMBER"].ToString();
                txtReceiverAddress.Text = receiverTable.Rows[0]["RECEIVERADDRESS"].ToString();

                txtDesc.Text = senderTable.Rows[0]["SENDERMESSAGE"].ToString();
                hdAmount.Value = txtSenderAmount.Text = Utility.FormatMoney(senderTable.Rows[0]["SENDERAMT"].ToString(), senderTable.Rows[0]["CURRENCYID"].ToString());
                txtPaidAmount.Text = Utility.FormatMoney(senderTable.Rows[0]["PAIDAMT"].ToString(), senderTable.Rows[0]["CURRENCYID"].ToString());
                hdCCYID.Value = ddlCCYID.SelectedValue = senderTable.Rows[0]["CURRENCYID"].ToString();
                ddlStatus.SelectedValue = senderTable.Rows[0]["STATUS"].ToString();
                txtSourceMoney.Text = senderTable.Rows[0]["SOURCEMONEY"].ToString();
                SOURCETYPE = senderTable.Rows[0]["SOURCETYPE"].ToString();
                if (SOURCETYPE == "C")
                {
                    txtSourceType.Text = Resources.labels.cash;
                }
                else if (SOURCETYPE == "W")
                {
                    txtSourceType.Text = Resources.labels.WalletAccount;
                }
                else
                {
                    txtSourceType.Text = Resources.labels.bankaccount;
                }
                switch (ACTION)
                {
                    case IPC.ACTIONPAGE.REJECT:
                        lblAmount.Text = Utility.FormatMoney(senderTable.Rows[0]["SENDERAMT"].ToString(), senderTable.Rows[0]["CURRENCYID"].ToString());
                        lblCurrencyAmount.Text = senderTable.Rows[0]["CURRENCYID"].ToString();
                        double fee = 0;
                        DataTable dtfee = new SmartPortal.SEMS.OtcFee().GetOTCFee("CANCELCASHCODE", SmartPortal.Common.Utilities.Utility.FormatMoneyInput(senderTable.Rows[0]["SENDERAMT"].ToString().Trim(), senderTable.Rows[0]["CURRENCYID"].ToString()), senderTable.Rows[0]["CURRENCYID"].ToString());
                        if (dtfee.Rows.Count > 0)
                        {
                            fee += double.Parse(dtfee.Rows[0]["feerAmt"].ToString());
                        }
                        hdFee.Value = lblFeeAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoneyInputToView(fee.ToString(), senderTable.Rows[0]["CURRENCYID"].ToString());
                        lblCurrencyFeeAmount.Text = senderTable.Rows[0]["CURRENCYID"].ToString();
                        USERCREATED = senderTable.Rows[0]["USERCREATED"].ToString();
                        if (SOURCETYPE == "C")
                        {
                            ddlReceiveType.Items.Clear();
                            ddlReceiveType.Items.Add(new ListItem(Resources.labels.cash, "CASH"));
                        }
                        else if (SOURCETYPE == "W")
                        {
                            ddlReceiveType.Items.Clear();
                            ddlReceiveType.Items.Add(new ListItem(Resources.labels.taikhoannguon, "SENDER"));
                            ddlReceiveType.Items.Add(new ListItem(Resources.labels.cash, "CASH"));
                        }
                        else
                        {
                            ddlReceiveType.Items.Clear();
                            ddlReceiveType.Items.Add(new ListItem(Resources.labels.taikhoannguon, "SENDER"));
                            ddlReceiveType.Items.Add(new ListItem(Resources.labels.cash, "CASH"));
                        }
                        divFeeShare.Visible = !senderTable.Rows[0]["SourceMoneyID"].ToString().Equals("MB");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void btnConfirm_OnClick(object sender, EventArgs e)
    {
        try
        {
            string trancode = string.Empty;
            string shareSender = divFeeShare.Visible
                ? Utility.KillSqlInjection(ddlFeeShareSender.SelectedValue.ToString().Trim())
                : string.Empty;
            switch (SOURCETYPE)
            {
                case "B":
                    switch (ddlReceiveType.SelectedValue.ToString())
                    {
                        case "SENDER":
                            trancode = "SB_CANCELCASHCODE";
                            break;
                        case "CASH":
                            trancode = "C_CANCELCASHCODE";
                            break;
                    }
                    break;
                case "W":
                    switch (ddlReceiveType.SelectedValue.ToString())
                    {
                        case "SENDER":
                            trancode = "SW_CANCELCASHCODE";
                            break;
                        case "CASH":
                            trancode = "C_CANCELCASHCODE";
                            break;
                    }
                    break;
                case "C":
                    trancode = "C_CANCELCASHCODE";
                    break;
                default:
                    return;
            }
            Hashtable result = new SmartPortal.SEMS.User().CancelCashCode(trancode, MVID, SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblFeeAmount.Text.ToString().Trim(), ddlCCYID.SelectedValue.ToString()), Utility.KillSqlInjection(txtMota.Text.ToString().Trim()), Session["userName"].ToString(), Session["branch"].ToString(), shareSender, ref IPCERRORCODE, ref IPCERRORDESC);
            if (result[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                lblError.Text = Resources.labels.transactionsuccessful;
                //ghi vo session dung in
                Hashtable hasPrint = new Hashtable();
                hasPrint.Add("tranID", result["IPCTRANSID"] != null ? result["IPCTRANSID"].ToString() : "");
                hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                hasPrint.Add("senderPhone", txtSenderPhone.Text);
                hasPrint.Add("senderName", txtSenderName.Text);
                hasPrint.Add("senderAddress", txtSenderAddress.Text);
                hasPrint.Add("ccyid", lblCurrencyFeeAmount.Text);
                hasPrint.Add("amount", lblAmount.Text);
                hasPrint.Add("amountchu", Utility.NumtoWords(Convert.ToDouble(lblAmount.Text)) + " " + lblCurrencyFeeAmount.Text.Trim());
                hasPrint.Add("feeAmount", lblFeeAmount.Text);
                hasPrint.Add("receiveType", ddlReceiveType.SelectedItem.Text);
                hasPrint.Add("desc", txtMota.Text == "" ? "Cancel Cash Code" : txtMota.Text);
                hasPrint.Add("status", Resources.labels.thanhcong);

                Session["print"] = hasPrint;
                TransactionSuccess_SendMail(hasPrint, USERCREATED);
                btnPrint.Enabled = true;
                btnConfirm.Enabled = false;

                try
                {
                    SmartPortal.Common.Log.WriteLogDatabaseTransaction(Utility.KillSqlInjection(txtTransID.Text.Trim()), "", Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "IPCLOGTRANS", "Cancel Cash Code", "", "", "A", Session["UserID"].ToString(),
                           SmartPortal.Common.Utilities.Utility.FormatMoneyInput(hdAmount.Value, hdCCYID.Value), Utility.KillSqlInjection(hdFee.Value));
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        if (Session["print"] == null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
        }
    }
    public static void TransactionSuccess_SendMail(Hashtable hasPrint, string userId)
    {
        try
        {
            string email = new SmartPortal.SEMS.User().GetUBID(userId).Rows[0]["EMAIL"].ToString();
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSCashcodemanager", "TransactionSuccessful2");

            tmpl.SetAttribute("tranID", hasPrint["tranID"].ToString());
            tmpl.SetAttribute("tranDate", hasPrint["tranDate"].ToString());
            tmpl.SetAttribute("senderPhone", hasPrint["senderPhone"].ToString());
            tmpl.SetAttribute("senderName", hasPrint["senderName"].ToString());
            tmpl.SetAttribute("senderAddress", hasPrint["senderAddress"].ToString());
            tmpl.SetAttribute("ccyid", hasPrint["ccyid"].ToString());
            tmpl.SetAttribute("amount", hasPrint["amount"].ToString());
            tmpl.SetAttribute("amountchu", hasPrint["amountchu"].ToString());
            tmpl.SetAttribute("feeAmount", hasPrint["feeAmount"].ToString());
            tmpl.SetAttribute("receiveType", hasPrint["receiveType"].ToString());
            tmpl.SetAttribute("desc", hasPrint["desc"].ToString());
            tmpl.SetAttribute("status", hasPrint["status"].ToString());

            SmartPortal.Common.Log.WriteLogFile("Sending email to " + email + " with content", "", "", Convert.ToBase64String(Encoding.UTF8.GetBytes(tmpl.ToString())));
            SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], email, "eBanking SYSTEM - Information for eBanking", tmpl.ToString());
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnResend_OnClick(object sender, EventArgs e)
    {
        try
        {
            Dictionary<object, object> paraObjects = new Dictionary<object, object>();
            paraObjects.Add("MVID", MVID);
            paraObjects.Add("USERID", Session["userName"].ToString());
            DataSet ds = _service.CallStore("SEMS_RESENDCASHCODE", paraObjects, "Resend cancel cash code", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                lblError.Text = Resources.labels.resendcashcodecompleted;
                btnResend.Enabled = false;
                try
                {
                    SmartPortal.Common.Log.WriteLogDatabaseTransaction(Utility.KillSqlInjection(txtTransID.Text.Trim()), "", Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "IPCLOGTRANS", "Resend cancel cash code", "", "", "A", Session["UserID"].ToString(),
                           SmartPortal.Common.Utilities.Utility.FormatMoneyInput(hdAmount.Value, hdCCYID.Value), Utility.KillSqlInjection(hdFee.Value));
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}