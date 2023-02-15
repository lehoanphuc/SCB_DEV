using System;
using System.Data;
using System.Collections.Generic;
using System.Web;

public partial class Widgets_SEMSChangeContractLevelofAgent_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            enableControl();

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
            defaultColor();
            loadCombobox();
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { txtPhoneNumber.Text, "AGT" };
            ds = _service.common("SEMS_BO_GETINFO_WAL", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFullName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    lbProductCode.Text = ds.Tables[0].Rows[0]["ProductID"].ToString();
                    lbProductName.Text = ds.Tables[0].Rows[0]["ProductName"].ToString();
                    lbContractCode.Text = ds.Tables[0].Rows[0]["ContractNo"].ToString();
                    lbCurrency.Text = ds.Tables[0].Rows[0]["CURRENCY_ID"].ToString();
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
    void loadCombobox()
    {
        DataSet ds = new DataSet();
        object[] WalletLevel = new object[] { "A" };
        ds = _service.common("SEMS_BO_GETWALLETLV", WalletLevel, ref IPCERRORCODE, ref IPCERRORDESC);
        ddlSourceWalletLevel.DataSource = ds;
        ddlSourceWalletLevel.DataValueField = "ContractLevelCode";
        ddlSourceWalletLevel.DataTextField = "ContractLevelName";
        ddlSourceWalletLevel.DataBind();

        ddlDestinationWalletLevel.DataSource = ds;
        ddlDestinationWalletLevel.DataValueField = "contractLevelCode";
        ddlDestinationWalletLevel.DataTextField = "ContractLevelName";
        ddlDestinationWalletLevel.DataBind();
    }
    void enableControl()
    {
        txtTransactionNumber.Enabled = false;
        txtTransactionDate.Enabled = false;
        txtFullName.Enabled = false;
        ddlSourceWalletLevel.Enabled = false;
    }

    protected void load_info(object sender, EventArgs e)
    {
        BindData();
    }
    void setPara(Dictionary<object, object> info)
    {
        info.Add("USERID", HttpContext.Current.Session["userID"].ToString());
        info.Add("PHONE", txtPhoneNumber.Text);
        info.Add("LEVEL_CODE", ddlSourceWalletLevel.SelectedValue);
        info.Add("LEVEL_CHANGE_CODE", ddlDestinationWalletLevel.SelectedValue);
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
            ds = _service.CallStore("", info, "Change Consumer Status of Consumer", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTransactionNumber.Text = ds.Tables[0].Rows[0]["TXREFID"].ToString();
                    txtTransactionDate.Text = ds.Tables[0].Rows[0]["TXDT"].ToString();
                    lblError.Text = Resources.labels.success;
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=129"));
    }
    void setControlDefault()
    {
        txtTransactionNumber.Text = "";
        txtTransactionDate.Text = "";
        txtFullName.Text = "";
        lbProductCode.Text = string.Empty;
        lbProductName.Text = string.Empty;
        lbContractCode.Text = string.Empty;
        lbCurrency.Text = string.Empty;
        loadCombobox();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPhoneNumber.Text = "";
        setControlDefault();
    }

  
   
}