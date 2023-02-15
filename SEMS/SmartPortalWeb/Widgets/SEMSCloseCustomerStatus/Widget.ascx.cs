using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSCloseCustomerStatus_Widget : WidgetBase
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

    void setDefaultTextbox()
    {
        lblError.Text = string.Empty;
        lbProductCode.Text = string.Empty;
        lbProductName.Text = string.Empty;
        lbContractCode.Text = string.Empty;

        txtTransactionNumber.Text = string.Empty;
        txtTransactionDate.Text = string.Empty;
        txtPhoneNumber.Text = string.Empty;
        txtFullName.Text = string.Empty;
        txtmakhachhang.Value = string.Empty;
        txtmakhachhang.Text = string.Empty;
    }
    void setControlDefault()
    {
        setDefaultTextbox();
        loadCombobox();
        txtPhoneNumber.Enabled = true;
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
                        ddlStatus.DataSource = ds;
                        ddlStatus.DataValueField = "ValueID";
                        ddlStatus.DataTextField = "Caption";
                        ddlStatus.DataBind();
                    }
                }
                Cache.Insert("Wallet_Status", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlStatus.DataSource = (DataSet)Cache["Wallet_Status"];
                ddlStatus.DataValueField = "ValueID";
                ddlStatus.DataTextField = "Caption";
                ddlStatus.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    void disableControl()
    {
        txtTransactionDate.Enabled = false;
        txtTransactionNumber.Enabled = false;
        txtFullName.Enabled = false;
        ddlStatus.Enabled = false;
        txtmakhachhang.Enabled = false;
    }
    void BindData()
    {
        try
        {
            loadCombobox();
            disableControl();
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { Utility.KillSqlInjection(txtPhoneNumber.Text.Trim()), IPC.PARAMETER.CONSUMER};
            ds = _service.common("SEMS_BO_GET_CUSINFO", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFullName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["CUS_STATUS"].ToString();
                    lbCurrency.Text = ds.Tables[0].Rows[0]["CURRENCY_ID"].ToString();
                    txtmakhachhang.Value = ds.Tables[0].Rows[0]["CUSTID"].ToString();
                    txtmakhachhang.Text = ds.Tables[0].Rows[0]["CUSTID"].ToString();
                    lbContractCode.Text = ds.Tables[0].Rows[0]["CONTRACT_NO"].ToString();
                    lbProductCode.Text = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                    lbProductName.Text = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
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
        try
        {
            defaultColor();
            if (!Utility.CheckSpecialCharacters(txtPhoneNumber.Text))
            {
                lblError.Text = Resources.labels.PhoneNumber + Resources.labels.ErrorSpeacialCharacters;
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return;
            }
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { txtPhoneNumber.Text, IPC.PARAMETER.CONSUMER };
            ds = _service.common("SEMS_BO_GET_CUSINFO", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFullName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["CUS_STATUS"].ToString();
                    lbCurrency.Text = ds.Tables[0].Rows[0]["CURRENCY_ID"].ToString();
                    txtmakhachhang.Value = ds.Tables[0].Rows[0]["CUSTID"].ToString();
                    txtmakhachhang.Text = ds.Tables[0].Rows[0]["CUSTID"].ToString();
                    lbContractCode.Text = ds.Tables[0].Rows[0]["CONTRACT_NO"].ToString();
                    lbProductCode.Text = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                    lbProductName.Text = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
                }
                else
                {
                    setDefaultTextbox();
                    loadCombobox();
                    txtPhoneNumber.Focus();
                    txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                    lblError.Text = Resources.labels.khongtimthaydulieu;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    void setPara(Dictionary<object, object> inforConsumer)
    {
        inforConsumer.Add("PHONE", txtPhoneNumber.Text);
        inforConsumer.Add("USERID", HttpContext.Current.Session["userID"].ToString());
    }

    void defaultColor()
    {
        txtFullName.BorderColor = System.Drawing.Color.Empty;
        txtmakhachhang.SetDefault();
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
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
            if (!Utility.CheckSpecialCharacters(txtPhoneNumber.Text))
            {
                lblError.Text = Resources.labels.PhoneNumber + Resources.labels.ErrorSpeacialCharacters;
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return;
            }
            if (txtFullName.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.fullname + Resources.labels.IsNotNull;
                txtFullName.BorderColor = System.Drawing.Color.Red;
                txtFullName.Focus();
                return;
            }
            if (!Utility.CheckSpecialCharacters(txtFullName.Text))
            {
                lblError.Text = Resources.labels.fullname + Resources.labels.ErrorSpeacialCharacters;
                txtFullName.BorderColor = System.Drawing.Color.Red;
                txtFullName.Focus();
                return;
            }
            if (txtmakhachhang.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.makhachhang + Resources.labels.IsNotNull;
                txtmakhachhang.SetFocus();
                return;
            }
            if (!Utility.CheckSpecialCharacters(txtmakhachhang.Text))
            {
                lblError.Text = Resources.labels.makhachhang + Resources.labels.ErrorSpeacialCharacters;
                txtmakhachhang.SetFocus();
                return;
            }
            #endregion
            DataSet ds = new DataSet();
            Dictionary<object, object> inforCustomer = new Dictionary<object, object>();
            setPara(inforCustomer);
            ds = _service.CallStore("WAL_BO_CLS_CUS_INFO", inforCustomer, "Close customer status","N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTransactionNumber.Text = ds.Tables[0].Rows[0]["TXREFID"].ToString();
                    txtTransactionDate.Text = ds.Tables[0].Rows[0]["TXDT"].ToString();
                    ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUS"].ToString();
                    lblError.Text = Resources.labels.success;
                    //disable control when transaction success
                    txtPhoneNumber.Enabled = false;
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
        setControlDefault();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}