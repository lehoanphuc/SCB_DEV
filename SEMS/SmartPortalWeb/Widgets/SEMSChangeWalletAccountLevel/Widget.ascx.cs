using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSChangeWalletAccountLevel_Widget : WidgetBase
{
    public string contractNo
    {
        get { return ViewState["contractNo"] != null ? ViewState["contractNo"].ToString() : ""; }
        set { ViewState["contractNo"] = value; }
    }
    public string userIDCT
    {
        get { return ViewState["userIDCT"] != null ? ViewState["userIDCT"].ToString() : ""; }
        set { ViewState["userIDCT"] = value; }
    }
    public string contractLV_Old
    {
        get { return ViewState["contractLV_Old"] != null ? ViewState["contractLV_Old"].ToString() : ""; }
        set { ViewState["contractLV_Old"] = value; }
    }
    public string contractLV_New
    {
        get { return ViewState["contractLV_New"] != null ? ViewState["contractLV_New"].ToString() : ""; }
        set { ViewState["contractLV_New"] = value; }
    }
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
    void setControlDefault()
    {
        lblError.Text = string.Empty;
        txtTransactionNumber.Text = string.Empty;
        txtTransactionDate.Text = string.Empty;
        txtFullname.Text = string.Empty;
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
        DataSet ds = new DataSet();
        object[] WalletLevel = new object[] { "A" };
        ds = _service.common("SEMS_BO_GETWALLETLV", WalletLevel, ref IPCERRORCODE, ref IPCERRORDESC);
        ddlSourceWalletAccountLevel.DataSource = ds;
        ddlSourceWalletAccountLevel.DataValueField = "ContractLevelCode";
        ddlSourceWalletAccountLevel.DataTextField = "ContractLevelName";
        ddlSourceWalletAccountLevel.DataBind();

        ddlDestinationWalletAccountLevel.DataSource = ds;
        ddlDestinationWalletAccountLevel.DataValueField = "ContractLevelCode";
        ddlDestinationWalletAccountLevel.DataTextField = "ContractLevelName";
        ddlDestinationWalletAccountLevel.DataBind();
    }

    void disableControl()
    {
        ddlSourceWalletAccountLevel.Enabled = false;
        txtTransactionDate.Enabled = false;
        txtTransactionNumber.Enabled = false;
        txtFullname.Enabled = false;
    }
    void BindData()
    {
        try
        {
            loadCombobox();
            disableControl();
            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { Utility.KillSqlInjection(txtPhoneNumber.Text), "IND" };
            ds = _service.common("SEMS_BO_GETINFO_WAL", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFullname.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    contractLV_Old = ddlSourceWalletAccountLevel.SelectedValue = ds.Tables[0].Rows[0]["CONTRACT_LEVEL_CODE"].ToString();
                    lbProductCode.Text = ds.Tables[0].Rows[0]["PRODUCT_ID"].ToString();
                    lbProductName.Text = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
                    contractNo = lbContractCode.Text = ds.Tables[0].Rows[0]["ContractNo"].ToString();
                    lbCurrency.Text= ds.Tables[0].Rows[0]["CURRENCY_ID"].ToString();
                    userIDCT = ds.Tables[0].Rows[0]["USERID"].ToString();
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
        if (txtFullname.Text == string.Empty)
        {
            lblError.Text = Resources.labels.PhoneNumber + " incorrect";
            txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
            txtPhoneNumber.Focus();
        }
    }

   
    void setPara(Dictionary<object, object> inforConsumer)
    {

        inforConsumer.Add("USERID", HttpContext.Current.Session["userID"].ToString());
        inforConsumer.Add("PHONE", Utility.KillSqlInjection(txtPhoneNumber.Text));
        inforConsumer.Add("LEVEL_CODE", Utility.KillSqlInjection(ddlSourceWalletAccountLevel.SelectedValue));
        inforConsumer.Add("LEVEL_CHANGE_CODE", Utility.KillSqlInjection( ddlDestinationWalletAccountLevel.SelectedValue));
        contractLV_New = ddlDestinationWalletAccountLevel.SelectedValue; ;
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
                lblError.Text = Resources.labels.PhoneNumber + " is not null";
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return;
            }
            if (txtFullname.Text.Equals(string.Empty))
            {
                lblError.Text = Resources.labels.fullname + " is not null";
                txtFullname.BorderColor = System.Drawing.Color.Red;
                txtFullname.Focus();
                return;
            }
            #endregion
            DataSet ds = new DataSet();
            Dictionary<object, object> inforConsumer = new Dictionary<object, object>();
            setPara(inforConsumer);
            ds = _service.CallStore("SEMS_BO_CHANGE_WALLV", inforConsumer, "Change wallet account level", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTransactionNumber.Text = ds.Tables[0].Rows[0]["TXREFID"].ToString();
                    txtTransactionDate.Text = ds.Tables[0].Rows[0]["TXDT"].ToString();
                    ddlSourceWalletAccountLevel.SelectedValue = ddlDestinationWalletAccountLevel.SelectedValue;
                    lblError.Text = Resources.labels.changecontractlevelsuccessfully;
                    pnAdd.Enabled = false;
                    btsave.Enabled = false;
                    SmartPortal.Common.Log.WriteLogDatabase(contractNo, userIDCT, Request.Url.ToString(), Session["userName"].ToString(),
                Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"], Resources.labels.ChangeWalletAccountLevel, contractLV_Old, contractLV_New, SmartPortal.Constant.IPC.ACTIVE);

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