using System;
using System.Data;
using System.Collections.Generic;
using System.Web;

public partial class Widgets_SEMSCloseContractofMerchant_Widget : WidgetBase
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
   protected void BindData()
    {
        try
        {
            loadDropDownList();
            enableControl();
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { txtPhoneNumber.Text};
            ds = _service.common("WAL_BO_GET_MERCHANT", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFullName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["AM_STATUS"].ToString();
                    lbProductCode.Text = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                    lbProductName.Text = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
                    lbContractCode.Text = ds.Tables[0].Rows[0]["CONTRACTNO"].ToString();
                    lbCurrency.Text = ds.Tables[0].Rows[0]["CURRENCY_ID"].ToString();
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
    void loadDropDownList()
    {
        DataSet ds = new DataSet();
        ds = _service.GetValueList("EBA_Contract", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
        if(IPCERRORCODE=="0")
        {
            if(ds.Tables[0].Rows.Count>0 )
            {
                ddlStatus.DataSource = ds;
                ddlStatus.DataValueField = "ValueID";
                ddlStatus.DataTextField = "Caption";
                ddlStatus.DataBind();
            }
        }
    }

    void enableControl()
    {
        txtTransactionNumber.Enabled = false;
        txtTransactionDate.Enabled = false;
        txtFullName.Enabled = false;
        ddlStatus.Enabled = false;
    }

    protected void load_info(object sender, EventArgs e)
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        BindData();
        if(txtFullName.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.PhoneNumber + Resources.labels.Incorrect;
            txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
            txtPhoneNumber.Focus();
        }
    }
    void setPara(Dictionary<object, object> info)
    {
        info.Add("USERMODIFY", HttpContext.Current.Session["userID"].ToString());
        info.Add("PHONE", txtPhoneNumber.Text);
    }
    void defaultColor()
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        txtFullName.BorderColor = System.Drawing.Color.Empty;
    }
    protected void btnAccept_Click(object sender, EventArgs e)
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
            if (txtFullName.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.fullname + Resources.labels.IsNotNull;
                txtFullName.BorderColor = System.Drawing.Color.Red;
                txtFullName.Focus();
                return;
            }
            #endregion
            DataSet ds = new DataSet();
            Dictionary<object, object> info = new Dictionary<object, object>();
            setPara(info);
            ds = _service.CallStore("SEMS_BO_CLOSE_WALMER", info, "Change close contract of merchant", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTransactionNumber.Text = ds.Tables[0].Rows[0]["TXREFID"].ToString();
                    txtTransactionDate.Text = ds.Tables[0].Rows[0]["TXDT"].ToString();
                    ddlStatus.SelectedValue= ds.Tables[0].Rows[0]["AM_USER_STATUS"].ToString(); 
                    lblError.Text = Resources.labels.success;
                    txtPhoneNumber.Enabled = false;
                    btnAccept.Enabled = false;
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

    void setControlDefault()
    {
        lblError.Text = string.Empty;
        txtTransactionNumber.Text = string.Empty;
        txtTransactionDate.Text = string.Empty;
        txtFullName.Text = string.Empty;
        lbProductCode.Text = string.Empty;
        lbProductName.Text = string.Empty;
        lbContractCode.Text = string.Empty;
        lbCurrency.Text = string.Empty;
        loadDropDownList();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPhoneNumber.Text = string.Empty;
        txtPhoneNumber.Enabled = true;
        btnAccept.Enabled = true;
        setControlDefault();
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        txtFullName.BorderColor = System.Drawing.Color.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
  
   
}