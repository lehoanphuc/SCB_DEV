using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSCloseUserOfUserTypeIsBackOffice_Widget : WidgetBase
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
    void setTextboxDefault()
    {
        lblError.Text = string.Empty;
        lbProductCode.Text = string.Empty;
        lbProductName.Text = string.Empty;
        lbContractCode.Text = string.Empty;

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
    }

    void loadCombobox_UserType()
    {
        DataSet ds = new DataSet();
        ds = _service.GetValueList("WAL_Value_List", "CLS", ref IPCERRORCODE, ref IPCERRORDESC);
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
        // Delete value Back Office
        ddUserType.Items.Remove(ddUserType.Items.FindByValue("BO"));
    }

    void loadCombobox_Status()
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
    void loadCombobox()
    {
        loadCombobox_Status();
        loadCombobox_UserType();
    }

    void disableControl()
    {
        txtTransactionDate.Enabled = false;
        txtTransactionNumber.Enabled = false;
        txtUserName.Enabled = false;
        ddlStatus.Enabled = false;
        txtUserCode.Enabled = false;
    }
    private void Bindata_Consumer()
    {
        try
        {
            if (!Utility.CheckSpecialCharacters(txtPhoneNumber.Text))
            {
                lblError.Text = Resources.labels.PhoneNumber + Resources.labels.ErrorSpeacialCharacters;
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return;
            }
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { txtPhoneNumber.Text};
            ds = _service.common("SEMS_BO_GET_CONSUMER", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["MB_STATUS"].ToString();
                    txtUserCode.Value = ds.Tables[0].Rows[0]["USERID"].ToString();
                    txtUserCode.Text = ds.Tables[0].Rows[0]["USERID"].ToString();
                    txtUserName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
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
            defaultColor();
            disableControl();
            if (ddUserType.SelectedValue == IPC.PARAMETER.CONSUMER)
            {
                Bindata_Consumer();
            }
            
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }


    protected void changeUserType_LoadInfo(object sender, EventArgs e)
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
        object[] inforConsumer = new object[] { txtPhoneNumber.Text, ddUserType.SelectedValue };
        ds = _service.common("SEMS_BO_GETINFO_USR", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUS_MBUSER"].ToString();
                txtUserCode.Value = ds.Tables[0].Rows[0]["USERCODE"].ToString();
                txtUserCode.Text = ds.Tables[0].Rows[0]["USERCODE"].ToString();
                txtUserName.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
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
        if (txtUserName.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.PhoneNumber + Resources.labels.Incorrect;
            txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
            txtPhoneNumber.Focus();
            return;
        }
    }
    void setPara(Dictionary<object, object> info)
    {

        info.Add("PHONE", txtPhoneNumber.Text);
        info.Add("USERMODIFY", HttpContext.Current.Session["userID"].ToString());
    }

    void defaultColor()
    {
        txtUserName.BorderColor = System.Drawing.Color.Empty;
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
            if (txtUserCode.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.fullname + Resources.labels.IsNotNull;
                txtUserCode.SetError();
                return;
            }
            if (txtUserName.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.fullname + Resources.labels.IsNotNull;
                txtUserName.BorderColor = System.Drawing.Color.Red;
                txtUserName.Focus();
                return;
            }
            #endregion
            DataSet ds = new DataSet();
            Dictionary<object, object> inforConsumer = new Dictionary<object, object>();
            setPara(inforConsumer);
            ds = _service.CallStore("WAL_BO_CLOSE_USER", inforConsumer, "Close User In case of User Type is Consumer", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTransactionNumber.Text = ds.Tables[0].Rows[0]["TXREFID"].ToString();
                    txtTransactionDate.Text = ds.Tables[0].Rows[0]["TXDT"].ToString();
                    lblError.Text = Resources.labels.success;
                    //disable control when success
                    txtPhoneNumber.Enabled = false;
                    ddUserType.Enabled = false;
                    ddUserType.SelectedValue = ds.Tables[0].Rows[0]["STATUS"].ToString(); 
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
        setControlDefault();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}