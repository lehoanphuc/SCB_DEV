using System;
using System.Data;
using System.Collections.Generic;
using System.Web;

public partial class Widgets_SEMSChangeCustomerStatus_Widget : WidgetBase
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
        txtFullName.Text = string.Empty;
        txtmakhachhang.Text = string.Empty;
        txtmakhachhang.Value = string.Empty;
    }
    void setControlDefault()
    {
        setTextboxDefault();
        loadCombobox();
        txtPhoneNumber.Enabled = true;
        ddlcusttype.Enabled = true;
        ddlDestinationStatus.Enabled = true;
    }
    void loadCombobox_SourceStatus()
    {
        // load status
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

    void loadCombobox_DestinationStatus()
    {
        // load status
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
    void loadCombobox_CustomerType()
    {
        // Load customer type
        DataSet ds1 = new DataSet();
        ds1 = _service.GetValueList("WALLET", "TYP", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlcusttype.DataSource = ds1;
                ddlcusttype.DataValueField = "ValueID";
                ddlcusttype.DataTextField = "Caption";
                ddlcusttype.DataBind();
            }
        }
    }
    void loadCombobox()
    {
        loadCombobox_CustomerType();
        loadCombobox_SourceStatus();
        loadCombobox_DestinationStatus();
    }

    void disableControl()
    {
        txtTransactionDate.Enabled = false;
        txtTransactionNumber.Enabled = false;
        txtFullName.Enabled = false;
        txtmakhachhang.Enabled = false;
        ddlSourceStatus.Enabled = false;
    }

    private void loadDataCustomer()
    {
        DataSet ds = new DataSet();
        object[] inforConsumer = new object[] { txtPhoneNumber.Text, ddlcusttype.SelectedValue };
        ds = _service.common("SEMS_BO_GET_CUSINFO", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtFullName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                ddlSourceStatus.SelectedValue = ds.Tables[0].Rows[0]["CUS_STATUS"].ToString();
                txtmakhachhang.Value = ds.Tables[0].Rows[0]["CUSTID"].ToString();
                txtmakhachhang.Text = ds.Tables[0].Rows[0]["CUSTID"].ToString();
                lbCurrency.Text = ds.Tables[0].Rows[0]["CURRENCY_ID"].ToString();
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
    void BindData()
    {
        try
        {
            loadCombobox();
            disableControl();
            loadDataCustomer();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void loadInfo(object sender, EventArgs e)
    {
        loadDataCustomer();
        defaultColor();
        if (txtmakhachhang.Text.Equals(string.Empty))
        {
            txtPhoneNumber.Focus();
            txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
            lblError.Text = Resources.labels.PhoneNumber + " is incorrect";
        }
    }

    protected void changeCustomerType_LoadInfo(object sender, EventArgs e)
    {
        defaultColor();
        DataSet ds = new DataSet();
        object[] inforConsumer = new object[] { txtPhoneNumber.Text, ddlcusttype.SelectedValue };
        ds = _service.common("SEMS_BO_GET_CUSINFO", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtFullName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                ddlSourceStatus.SelectedValue = ds.Tables[0].Rows[0]["CUS_STATUS"].ToString();
                txtmakhachhang.Value = ds.Tables[0].Rows[0]["CUSTID"].ToString();
                txtmakhachhang.Text = ds.Tables[0].Rows[0]["CUSTID"].ToString();
                lbCurrency.Text = ds.Tables[0].Rows[0]["CURRENCY_ID"].ToString();
                lbContractCode.Text = ds.Tables[0].Rows[0]["CONTRACT_NO"].ToString();
                lbProductCode.Text = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                lbProductName.Text = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
            }
            else
            {
                setTextboxDefault();
                loadCombobox_SourceStatus();
                loadCombobox_DestinationStatus();
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
    void setPara(Dictionary<object, object> info)
    {
        info.Add("PHONE", txtPhoneNumber.Text);
        info.Add("CUSTID", txtmakhachhang.Text);
        info.Add("CURRENTSTATUS", ddlSourceStatus.SelectedValue);
        info.Add("RETURNSTATUS", ddlDestinationStatus.SelectedValue);
        info.Add("USERMODIFY", HttpContext.Current.Session["userID"].ToString());
        info.Add("USERTYPE", ddlcusttype.SelectedValue);
    }

    void defaultColor()
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        txtmakhachhang.SetDefault();
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
            if (txtmakhachhang.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.makhachhang + " is not null";
                txtmakhachhang.SetError();
                return;
            }
            if (txtFullName.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.fullname + " is not null";
                txtFullName.BorderColor = System.Drawing.Color.Red;
                txtFullName.Focus();
                return;
            }
            #endregion
            DataSet ds = new DataSet();
            Dictionary<object, object> inforCustomer = new Dictionary<object, object>();
            setPara(inforCustomer);
            ds = _service.CallStore("SEMS_BO_CHAN_STT_CUS", inforCustomer, "Change customer status","N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTransactionNumber.Text = ds.Tables[0].Rows[0]["TXREFID"].ToString();
                    txtTransactionDate.Text = ds.Tables[0].Rows[0]["TXDT"].ToString();
                    ddlSourceStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUS"].ToString();
                    lblError.Text = Resources.labels.success;
                    //disable control when transaction success
                    txtPhoneNumber.Enabled = false;
                    ddlcusttype.Enabled = false;
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
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=129"));
    }
}