using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using SmartPortal.Common.Utilities;
using System.Collections;

public partial class Widgets_SEMSWithdrawalWalletConsumer_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.ForeColor = System.Drawing.Color.Red;
        try
        {
            txtWithdrawalAmount.Attributes.Add("onkeyup", "executeComma('" + txtWithdrawalAmount.ClientID + "',event)");
            txtFeeAmount.Attributes.Add("onkeyup", "executeComma('" + txtFeeAmount.ClientID + "',event)");
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    void setControlDefault()
    {
        lblError.Text = string.Empty;
        txtTransactionNumber.Text = string.Empty;
        txtTransactionDate.Text = string.Empty;
        txtFullname.Text = string.Empty;
        txtWithdrawalAmount.Text = string.Empty;
        txtDiscription.Text = string.Empty;
        lbProductCode.Text = string.Empty;
        lbCurrency.Text = string.Empty;
        lbProductName.Text = string.Empty;
        lbContractCode.Text = string.Empty;
    }


    void disableControl()
    {
        txtTransactionDate.Enabled = false;
        txtTransactionNumber.Enabled = false;
        txtFullname.Enabled = false;
    }
    void BindData()
    {
        try
        {
            disableControl();
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { Utility.KillSqlInjection(txtPhoneNumber.Text), "IND" };
            ds = _service.common("SEMS_BO_GETINFO_WAL", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                txtFeeAmount.Text = CaculateFee().ToString();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFullname.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    lbCurrency.Text = ds.Tables[0].Rows[0]["CURRENCY_ID"].ToString();
                    lbProductCode.Text = ds.Tables[0].Rows[0]["PRODUCT_ID"].ToString();
                    lbProductName.Text = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
                    lbContractCode.Text = ds.Tables[0].Rows[0]["ContractNo"].ToString();
                    lbBalance.Text = String.Format("{0:#,0.####}", Decimal.Parse(ds.Tables[0].Rows[0]["BALANCE"].ToString()));
                    ViewState["PREVIOUS_BALANCE"] = String.Format("{0:#,0.####}", Decimal.Parse(ds.Tables[0].Rows[0]["BALANCE"].ToString()));
                    lblCoinWallet.Text = String.Format("{0:#,0.####}", Decimal.Parse(ds.Tables[0].Rows[0]["CoinWallet"].ToString()));
                    ViewState["PREVIOUS_COIN"] = String.Format("{0:#,0.####}", Decimal.Parse(ds.Tables[0].Rows[0]["CoinWallet"].ToString()));
                }
                else
                {
                    setControlDefault();
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    private void PrintVoucher()
    {
        try
        {
            Hashtable hasPrint = new Hashtable();
            hasPrint.Add("TXREFID", txtTransactionNumber.Text);
            hasPrint.Add("TXDT", txtTransactionDate.Text);
            hasPrint.Add("PHONE_NUMBER", txtPhoneNumber.Text);
            hasPrint.Add("FULL_NAME", txtFullname.Text);
            hasPrint.Add("AMOUNT", "-" + txtWithdrawalAmount.Text);
            hasPrint.Add("FEE_AMOUNT", "-" + txtFeeAmount.Text);
            hasPrint.Add("CCYID", lbCurrency.Text);
            hasPrint.Add("PREVIOUS_BALANCE", ViewState["PREVIOUS_BALANCE"].ToString());
            hasPrint.Add("BALANCE", lbBalance.Text);
            hasPrint.Add("PREVIOUS_COIN", ViewState["PREVIOUS_COIN"].ToString());
            hasPrint.Add("CoinWallet", lblCoinWallet.Text);
            hasPrint.Add("desc", txtDiscription.Text);
            hasPrint.Add("datecreated", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            hasPrint.Add("usercreated", HttpContext.Current.Session["userID"].ToString());
            string key = "WITHDRAWAL_" + HttpContext.Current.Session["userID"].ToString() + txtTransactionNumber.Text;
            Cache.Insert(key, hasPrint);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message + " - " + Resources.labels.PrintFailedToCreate;
        }
    }
    protected void loadInfo(object sender, EventArgs e)
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        BindData();
        if (txtFullname.Text == string.Empty)
        {
            lblError.Text = Resources.labels.PhoneNumber + Resources.labels.Incorrect;
            txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
            txtPhoneNumber.Focus();
        }
    }
    void setPara(Dictionary<object, object> inforConsumer)
    {
        hdAmount.Value = Utility.KillSqlInjection(txtWithdrawalAmount.Text.ToString().Replace(",", ""));
        hdFee.Value = CaculateFee().ToString();
        inforConsumer.Add("USERID", HttpContext.Current.Session["userID"].ToString());
        inforConsumer.Add("AMOUNT", Utility.KillSqlInjection(txtWithdrawalAmount.Text.ToString().Replace(",", "")));
        inforConsumer.Add("PHONE", Utility.KillSqlInjection(txtPhoneNumber.Text));
        inforConsumer.Add("FEEAMOUNT", CaculateFee());
        inforConsumer.Add("POCKETYPE", "M");
        inforConsumer.Add("DESCRIPTION", Utility.KillSqlInjection(txtDiscription.Text) == "" ? "SEMS Withdrawal from own EWallet of consumer" : Utility.KillSqlInjection(txtDiscription.Text));
    }
    void setParaFEE(Dictionary<object, object> inforConsumer)
    {
        inforConsumer.Add("IPCTRANCODE_OTCFEE", "SEMS_BO_WDR_EWAL");
        inforConsumer.Add("PHONE", Utility.KillSqlInjection(txtPhoneNumber.Text));
        inforConsumer.Add("CCYID", Utility.KillSqlInjection(lbCurrency.Text));
    }
    public float CaculateFee()
    {
        string fee = "0";
        if (txtFeeAmount.Text.ToString().Equals(""))
        {
            DataSet ds = new DataSet();
            Dictionary<object, object> inforTopup = new Dictionary<object, object>();
            setParaFEE(inforTopup);
            ds = _service.CallStore("SEMS_CACULOTPFEE", inforTopup, "Caculate Fee Withdrawal  E-Wallet account", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                    fee = ds.Tables[0].Rows[0]["FEE"].ToString();

                return float.Parse(fee);
            }
        }
        else
            return float.Parse(txtFeeAmount.Text.ToString().Replace(",", ""));

        return 0;
    }
    void defaultColor()
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        txtFullname.BorderColor = System.Drawing.Color.Empty;
        txtFeeAmount.BorderColor = System.Drawing.Color.Empty;
        txtWithdrawalAmount.BorderColor = System.Drawing.Color.Empty;
    }
    protected void btnAccept_click(object sender, EventArgs e)
    {
        try
        {
            #region validation
            defaultColor();
            if (txtPhoneNumber.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.PhoneNumber + Resources.labels.IsNotNull;
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return;
            }
            if (txtFullname.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.fullname + Resources.labels.IsNotNull;
                txtFullname.BorderColor = System.Drawing.Color.Red;
                txtFullname.Focus();
                return;
            }
            if (txtWithdrawalAmount.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.WithdrawalAmount + Resources.labels.IsNotNull;
                txtWithdrawalAmount.BorderColor = System.Drawing.Color.Red;
                txtWithdrawalAmount.Focus();
                return;
            }
            if (double.Parse(txtWithdrawalAmount.Text.Replace(",", "")) <= 0)
            {
                lblError.Text = Resources.labels.WithdrawalAmount + Resources.labels.mustbegreaterthan0;
                txtWithdrawalAmount.BorderColor = System.Drawing.Color.Red;
                txtWithdrawalAmount.Focus();
                return;
            }
            #endregion
            Hashtable ds = new Hashtable();
            Dictionary<object, object> inforTopup = new Dictionary<object, object>();
            setPara(inforTopup);
            ds = _service.CallCore("SEMS_BO_WDR_EWAL", inforTopup, Utility.KillSqlInjection(txtDiscription.Text) == "" ? "SEMS Withdrawal from own EWallet of consumer" : Utility.KillSqlInjection(txtDiscription.Text), "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Count > 0)
                {
                    txtTransactionNumber.Text = ds["TXREFID"].ToString();
                    txtTransactionDate.Text = ds["TXDT"].ToString();

                    txtFeeAmount.Text = String.Format("{0:#,0.####}", Decimal.Parse(ds["FEEAMOUNT"].ToString()));
                    txtWithdrawalAmount.Text = String.Format("{0:#,0.####}", Decimal.Parse(txtWithdrawalAmount.Text.ToString()));
                    lbBalance.Text = String.Format("{0:#,0.####}", Decimal.Parse(ds["Balance"].ToString()));
                    lblCoinWallet.Text = String.Format("{0:#,0.####}", Decimal.Parse(ds["CoinWallet"].ToString()));
                    lblError.Text = Resources.labels.WithdrawaltoEWalletofConsumerSuccessfully;
                    pnAdd.Enabled = false;
                    btsave.Enabled = false;
                    btnPrint.Enabled = true;
                    PrintVoucher();

                    try
                    {
                        SmartPortal.Common.Log.WriteLogDatabaseTransaction(lbContractCode.Text, Utility.KillSqlInjection(txtPhoneNumber.Text), Request.Url.ToString(), Session["userName"].ToString(),
                         Request.UserHostAddress, "IPCLOGTRANS", "SEMS Withdrawal from own EWallet of Consumer", "", "", "A", Session["UserID"].ToString(), hdAmount.Value, string.IsNullOrEmpty(hdFee.Value) ? "0" : hdFee.Value);

                    }
                    catch
                    {

                    }

                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnClear_click(object sender, EventArgs e)
    {
        defaultColor();
        txtPhoneNumber.Text = string.Empty;
        setControlDefault();
        pnAdd.Enabled = true;
        btsave.Enabled = true;
        lbBalance.Text = string.Empty;
        txtFeeAmount.Text = string.Empty;
        btnPrint.Enabled = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

}