using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSCloseWalletAccount_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    Parameter _Para = new Parameter();
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.ForeColor = System.Drawing.Color.Red;
        try
        {
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
        txtWalletAccount.Text = string.Empty;
        lbProductCode.Text = string.Empty;
        lbProductName.Text = string.Empty;
        lbContractCode.Text = string.Empty;
        lbCurrency.Text = string.Empty;
        pnAdd.Enabled = true;
        btsave.Enabled = true;
        loadCombobox();
    }

    void loadCombobox()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_Status"];
            if (ds == null)
            {
                ds = _service.GetValueList("WALLET", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSourceStatus.DataSource = ds;
                        ddlSourceStatus.DataValueField = "ValueID";
                        ddlSourceStatus.DataTextField = "Caption";
                        ddlSourceStatus.DataBind();
                    }
                }
                Cache.Insert("Wallet_Status", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlSourceStatus.DataSource = (DataSet)Cache["Wallet_Status"];
                ddlSourceStatus.DataValueField = "ValueID";
                ddlSourceStatus.DataTextField = "Caption";
                ddlSourceStatus.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    void disableControl()
    {
        ddlSourceStatus.Enabled = false;
        txtTransactionDate.Enabled = false;
        txtTransactionNumber.Enabled = false;
        txtFullname.Enabled = false;
        txtWalletAccount.Enabled = false;
    }
    void BindData()
    {
        try
        {
            loadCombobox();
            disableControl();
            DataSet ds= new DataSet();
            object[] inforConsumer = new object[] { Utility.KillSqlInjection(txtPhoneNumber.Text) };
            ds = _service.common("SEMS_BO_GET_CONSUMER", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0" )
            {
                if (ds.Tables[0].Rows.Count > 0) {
                    txtFullname.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    txtWalletAccount.Text = ds.Tables[0].Rows[0]["WALLET_ID"].ToString();
                    ddlSourceStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUS_EBACONTRACT"].ToString();
                    lbProductCode.Text = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                    lbProductName.Text = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
                    lbContractCode.Text = ds.Tables[0].Rows[0]["CONTRACTNO"].ToString();
                    lbCurrency.Text = ds.Tables[0].Rows[0]["CURRENCY_ID"].ToString();
                    ViewState["CUSTID"] = ds.Tables[0].Rows[0]["CustID"].ToString();

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
    protected void loadInfo(object sender, EventArgs e)
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        BindData();
        if(txtFullname.Text == string.Empty)
        {
            lblError.Text = Resources.labels.PhoneNumber + Resources.labels.Incorrect;
            txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
            txtPhoneNumber.Focus();
        }
    }
    void setPara(Dictionary<object, object> inforConsumer)
    {
        inforConsumer.Add("PHONE", Utility.KillSqlInjection(txtPhoneNumber.Text));
        inforConsumer.Add("USERID", Utility.KillSqlInjection( ViewState["CUSTID"].ToString()));
        inforConsumer.Add("USERMODIFY", Utility.KillSqlInjection(HttpContext.Current.Session["userID"].ToString()));
        
    }

    void defaultColor()
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        txtFullname.BorderColor = System.Drawing.Color.Empty;
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
            if (txtWalletAccount.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.fullname + Resources.labels.IsNotNull;
                txtWalletAccount.BorderColor = System.Drawing.Color.Red;
                txtWalletAccount.Focus();
                return;
            }
            #endregion
            DataSet ds = new DataSet();
            Dictionary<object, object> inforWallet = new Dictionary<object, object>();
            setPara(inforWallet);
            ds = _service.CallStore("SEMS_BO_CLOSE_WALCON", inforWallet, "Close consumer wallet account","N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTransactionNumber.Text = ds.Tables[0].Rows[0]["TXREFID"].ToString();
                    txtTransactionDate.Text = ds.Tables[0].Rows[0]["TXDT"].ToString();
                    lblError.Text = Resources.labels.success;

                    pnAdd.Enabled = false;
                    btsave.Enabled = false;
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
        txtPhoneNumber.Text = string.Empty;
        defaultColor();
        setControlDefault();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}