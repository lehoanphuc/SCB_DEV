using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSChangeWalletAccountStatus_Widget : WidgetBase
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
            object[] inforConsumer = new object[] { Utility.KillSqlInjection(txtPhoneNumber.Text), "IND" };
            ds = _service.common("SEMS_BO_GETINFO_WAL", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFullName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    ddlSourceStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUS_EBACONTRACT"].ToString();
                    lbProductCode.Text = ds.Tables[0].Rows[0]["PRODUCT_ID"].ToString();
                    lbProductName.Text = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
                    lbContractCode.Text = ds.Tables[0].Rows[0]["ContractNo"].ToString();
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
        catch(Exception e)
        {
            lblError.Text = e.Message;
        }
        
    }
    void loadDropDownList()
    {
        loadCombobox_SourceStatus();
        loadCombobox_DestinationStatus();
    }
    void loadCombobox_SourceStatus()
    {
        DataSet ds = new DataSet();
        ds = _service.GetValueList("STATUS", "EBA_CONTRACT_WAL", ref IPCERRORCODE, ref IPCERRORDESC);
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
        DataSet ds = new DataSet();
        ds = _service.GetValueList("STATUS_BO_TRANS_STATUS", "EBA_CONTRACT_WAL", ref IPCERRORCODE, ref IPCERRORDESC);
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
    void enableControl()
    {
        txtTransactionNumber.Enabled = false;
        txtTransactionDate.Enabled = false;
        txtFullName.Enabled = false;
        ddlSourceStatus.Enabled = false;
    }
    protected void load_info(object sender, EventArgs e)
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        BindData();
        if (txtFullName.Text == string.Empty)
        {
            lblError.Text = Resources.labels.PhoneNumber + " incorrect";
            txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
            txtPhoneNumber.Focus();
        }
    }
    void setPara(Dictionary<object, object> info)
    {

        info.Add("PHONE", Utility.KillSqlInjection( txtPhoneNumber.Text));
        info.Add("CURRENTSTATUS", Utility.KillSqlInjection(ddlSourceStatus.SelectedValue));
        info.Add("RETURNSTATUS", Utility.KillSqlInjection(ddlDestinationStatus.SelectedValue));
        info.Add("USERMODIFY", Utility.KillSqlInjection(HttpContext.Current.Session["userID"].ToString()));
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
                lblError.Text = Resources.labels.PhoneNumber + " is not null";
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
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
            Dictionary<object, object> info = new Dictionary<object, object>();
            setPara(info);
            ds = _service.CallStore("SEMS_BO_CHANGESTATUS", info, "Change status wallet account", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTransactionNumber.Text = ds.Tables[0].Rows[0]["TXREFID"].ToString();
                    txtTransactionDate.Text = ds.Tables[0].Rows[0]["TXDT"].ToString();
                    lblError.Text = Resources.labels.changestatussuccessfully;
                    ddlSourceStatus.SelectedValue = ddlDestinationStatus.SelectedValue;
                    pnAdd.Enabled = false;
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
        //txtPhoneNumber.Text = string.Empty;
        txtFullName.Text = string.Empty;
        lbProductCode.Text = string.Empty;
        lbProductName.Text = string.Empty;
        lbContractCode.Text = string.Empty;
        lbCurrency.Text = string.Empty;
        defaultColor();
        loadDropDownList();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPhoneNumber.Text = string.Empty;
        setControlDefault();
        pnAdd.Enabled = true;
        btnAccept.Enabled = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }


}