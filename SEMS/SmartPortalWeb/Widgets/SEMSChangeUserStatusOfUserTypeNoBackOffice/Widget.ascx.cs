using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSUserTypeNoBackOffice_Widget : WidgetBase
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

    void setTextboxDefault()
    {
        lblError.Text = string.Empty;
        lbContractCode.Text = string.Empty;
        lbProductCode.Text = string.Empty;
        lbProductName.Text = string.Empty;

        txtPhoneNumber.Text = string.Empty;
        txtTransactionNumber.Text = string.Empty;
        txtTransactionDate.Text = string.Empty;
        txtUserName.Text = string.Empty;
        txtUserCode.Text = string.Empty;
    }
    void setControlDefault()
    {
        setTextboxDefault();
        loadCombobox();
        txtPhoneNumber.Enabled = true;
        ddUserType.Enabled = true;
        ddlDestinationStatus.Enabled = true;
    }

    void loadCombobox_UserType()
    {
        DataSet ds = new DataSet();
        ds = _service.GetValueList("WALLET", "TYP", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddUserType.DataSource = ds;
                ddUserType.DataValueField = "ValueID";
                ddUserType.DataTextField = "Caption";
                ddUserType.DataBind();
            }
        }
    }
    void loadCombobox_Status()
    {
        DataSet ds = new DataSet();
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
    }

    void loadCombobox_Destination_Status()
    {
        DataSet ds = new DataSet();
        ds = _service.GetValueList("Change_User_Status", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDestinationStatus.DataSource = ds;
                ddlDestinationStatus.DataValueField = "ValueID";
                ddlDestinationStatus.DataTextField = "Caption";
                ddlDestinationStatus.DataBind();
            }
        }
    }
    void loadCombobox()
    {
        loadCombobox_Status();
        loadCombobox_UserType();
        loadCombobox_Destination_Status();
    }

    void disableControl()
    {
        ddlSourceStatus.Enabled = false;
        txtTransactionDate.Enabled = false;
        txtTransactionNumber.Enabled = false;
        txtUserName.Enabled = false;
        txtUserCode.Enabled = false;
    }

    private void Bindata_Consumer()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { txtPhoneNumber.Text };
            ds = _service.common("SEMS_BO_GET_CONSUMER", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtUserName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    ddlSourceStatus.SelectedValue = ds.Tables[0].Rows[0]["MB_STATUS"].ToString();
                    txtUserCode.Value = ds.Tables[0].Rows[0]["USERID"].ToString();
                    txtUserCode.Text = ds.Tables[0].Rows[0]["USERID"].ToString();
                    lbContractCode.Text = ds.Tables[0].Rows[0]["CONTRACTNO"].ToString();
                    lbProductCode.Text = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                    lbProductName.Text = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
                }
                else
                {
                    setTextboxDefault();
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

    private void Bindata_MerchantAgent()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { txtPhoneNumber.Text };
            ds = _service.common("WAL_BO_GET_MERCHANT", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtUserName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    ddlSourceStatus.SelectedValue = ds.Tables[0].Rows[0]["AM_STATUS"].ToString();
                    txtUserCode.Value = ds.Tables[0].Rows[0]["USERID"].ToString();
                    txtUserCode.Text = ds.Tables[0].Rows[0]["USERID"].ToString();
                    lbContractCode.Text = ds.Tables[0].Rows[0]["CONTRACTNO"].ToString();
                    lbProductCode.Text = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                    lbProductName.Text = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
                }
                else
                {
                    setTextboxDefault();
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
    void BindData()
    {
        try
        {
            loadCombobox();
            disableControl();
            if (ddUserType.SelectedValue == IPC.PARAMETER.CONSUMER)
            {
                Bindata_Consumer();
            }
            if (ddUserType.SelectedValue == IPC.PARAMETER.MERCHANT_AGENT)
            {
                Bindata_MerchantAgent();
            }
            
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void changeCustomerType_LoadInfo(object sender, EventArgs e)
    {
        defaultColor();
        DataSet ds = new DataSet();
        object[] inforConsumer = new object[] { txtPhoneNumber.Text, ddUserType.SelectedValue };
        ds = _service.common("SEMS_BO_GETINFO_USR", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtUserName.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                ddlSourceStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUS_MBUSER"].ToString();
                txtUserCode.Value = ds.Tables[0].Rows[0]["USERCODE"].ToString();
                txtUserCode.Text = ds.Tables[0].Rows[0]["USERCODE"].ToString();
                lbContractCode.Text = ds.Tables[0].Rows[0]["CONTRACTCODE"].ToString();
                lbProductCode.Text = ds.Tables[0].Rows[0]["PRODUCTCODE"].ToString();
                lbProductName.Text = ds.Tables[0].Rows[0]["PRODUCTNAME"].ToString();
            }
            else
            {
                loadCombobox_Status();
                setTextboxDefault();
                txtPhoneNumber.Focus();
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                lblError.Text = Resources.labels.khongtimthaydulieu;
            }
        }
        else
        {
            lblError.Text = IPCERRORDESC;
        }
    }

    protected void loadPopup(object sender, EventArgs e)
    {
        txtUserCode.SetUserType = ddUserType.SelectedValue;
        txtUserCode.refreshPopup();
    }
    protected void loadInfo(object sender, EventArgs e)
    {
        if (!Utility.CheckSpecialCharacters(txtPhoneNumber.Text))
        {
            lblError.Text = Resources.labels.PhoneNumber + Resources.labels.ErrorSpeacialCharacters;
            txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
            txtPhoneNumber.Focus();
            return;
        }
        if (ddUserType.SelectedValue == IPC.PARAMETER.CONSUMER)
        {
            Bindata_Consumer();
        }
        if (ddUserType.SelectedValue == IPC.PARAMETER.MERCHANT_AGENT)
        {
            Bindata_MerchantAgent();
        }
        if (txtUserName.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.PhoneNumber + " incorrect";
            txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
            txtPhoneNumber.Focus();
            return;
        }
    }
    void setPara(Dictionary<object, object> infor)
    {
        infor.Add("USERID", txtUserCode.Text);
        infor.Add("USERTYPE", ddUserType.SelectedValue);
        infor.Add("CURRENTSTATUS", ddlSourceStatus.SelectedValue);
        infor.Add("RETURNSTATUS", ddlDestinationStatus.SelectedValue);
        infor.Add("USERMODIFY", HttpContext.Current.Session["userID"].ToString());
    }

    void defaultColor()
    {
        txtUserName.BorderColor = System.Drawing.Color.Empty;
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        txtUserName.BorderColor = System.Drawing.Color.Empty;
        txtUserCode.SetDefault();
    }
    protected void btnAccept_click(object sender, EventArgs e)
    {
        try
        {
            #region validation
            defaultColor();
            if (txtPhoneNumber.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.PhoneNumber + " is not null";
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
            if (txtUserName.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.tendangnhap + " is not null";
                txtUserName.BorderColor = System.Drawing.Color.Red;
                txtUserName.Focus();
                return;
            }
            if (!Utility.CheckSpecialCharacters(txtUserName.Text))
            {
                lblError.Text = Resources.labels.username + Resources.labels.ErrorSpeacialCharacters;
                txtUserName.BorderColor = System.Drawing.Color.Red;
                txtUserName.Focus();
                return;
            }
            if (txtUserCode.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.UserCode + " is not null";
                txtUserCode.SetError();
                return;
            }
            if (!Utility.CheckSpecialCharacters(txtUserCode.Text))
            {
                lblError.Text = Resources.labels.UserCode + Resources.labels.ErrorSpeacialCharacters;
                txtUserCode.SetError();
                return;
            }
            #endregion
            DataSet ds = new DataSet();
            Dictionary<object, object> inforUser = new Dictionary<object, object>();
            setPara(inforUser);
            ds = _service.CallStore("SEMS_BO_CHAN_STT_USR", inforUser, "Change user status for consumer-agent-merchant","N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTransactionNumber.Text = ds.Tables[0].Rows[0]["TXREFID"].ToString();
                    txtTransactionDate.Text = ds.Tables[0].Rows[0]["TXDT"].ToString();
                    lblError.Text = Resources.labels.success;
                    // disbale control when transaction success
                    txtPhoneNumber.Enabled = false;
                    ddUserType.Enabled = false;
                    ddlDestinationStatus.Enabled = false;
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
        defaultColor();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=129"));
    }
}