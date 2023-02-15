using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Constant;
using System.Collections.Generic;
using System.Collections;
using System.Web;

public partial class Widgets_SEMSKYCMerchantProfile_ApproveOrReject_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string MerchantID = string.Empty;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            lbTitle.Text = Resources.labels.ApproveOrRejectMerchantRegistration;
            txtBankName.Enabled = false;
            MerchantID = GetParamsPage(IPC.ID)[0].Trim();
            if (!IsPostBack)
            {
                BindData();
                loadData_Repeater();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    private void loadCombobox_KYCLevel()
    {
        // Save list KYCLevel Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_KYCLevel"];
            if (ds == null)
            {
                object[] searchObject = new object[] { string.Empty };
                ds = _service.common("SEMS_BO_GET_INFO_KYC", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddKYCLevel.DataSource = ds;
                        ddKYCLevel.DataValueField = "KycID";
                        ddKYCLevel.DataTextField = "KycName";
                        ddKYCLevel.DataBind();
                    }
                }
                Cache.Insert("Wallet_KYCLevel", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddKYCLevel.DataSource = (DataSet)Cache["Wallet_KYCLevel"];
                ddKYCLevel.DataValueField = "KycID";
                ddKYCLevel.DataTextField = "KycName";
                ddKYCLevel.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadCombobox_PaperType()
    {
        // Save list PaperType Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_PaperType"];
            if (ds == null)
            {
                object[] searchObject = new object[] { string.Empty, string.Empty, string.Empty, null, null };
                ds = _service.GetValueList("EBA_CustInfo", "PAP", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddPaperType.DataSource = ds;
                        ddPaperType.DataValueField = "ValueID";
                        ddPaperType.DataTextField = "Caption";
                        ddPaperType.DataBind();
                    }
                }
                Cache.Insert("Wallet_PaperType", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddPaperType.DataSource = (DataSet)Cache["Wallet_PaperType"];
                ddPaperType.DataValueField = "ValueID";
                ddPaperType.DataTextField = "Caption";
                ddPaperType.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadCombobox_MoneySource()
    {
        // Save list MoneySource Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_MoneySource"];
            if (ds == null)
            {
                object[] searchObject = new object[] { string.Empty, string.Empty, string.Empty, null, null };
                ds = _service.GetValueList("Money_Source", "TYPE", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddMoneySourceType.DataSource = ds;
                        ddMoneySourceType.DataValueField = "ValueID";
                        ddMoneySourceType.DataTextField = "Caption";
                        ddMoneySourceType.DataBind();
                    }
                }
                Cache.Insert("Wallet_MoneySource", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddMoneySourceType.DataSource = (DataSet)Cache["Wallet_MoneySource"];
                ddMoneySourceType.DataValueField = "ValueID";
                ddMoneySourceType.DataTextField = "Caption";
                ddMoneySourceType.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadCombobox_Nation()
    {
        // Save list Nation Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_Nation"];
            if (ds == null)
            {
                object[] _object = new object[] { string.Empty, string.Empty, null, null };
                ds = _service.common("WAL_BO_GET_NATION", _object, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddNationality.DataSource = ds;
                        ddNationality.DataValueField = "NationCode";
                        ddNationality.DataTextField = "NationName";
                        ddNationality.DataBind();
                    }
                }
                Cache.Insert("Wallet_Nation", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddNationality.DataSource = (DataSet)Cache["Wallet_Nation"];
                ddNationality.DataValueField = "NationCode";
                ddNationality.DataTextField = "NationName";
                ddNationality.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadCombobox_WalletLevel()
    {
        // Save list WalletLevel Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_WalletLevel"];
            if (ds == null)
            {
                object[] _object = new object[] { string.Empty };
                ds = _service.common("SEMS_BO_LST_CON_LV", _object, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddWalletLevel.DataSource = ds;
                        ddWalletLevel.DataValueField = "ContractLevelID";
                        ddWalletLevel.DataTextField = "ContractLevelName";
                        ddWalletLevel.DataBind();
                    }
                }
                Cache.Insert("Wallet_WalletLevel", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddWalletLevel.DataSource = (DataSet)Cache["Wallet_WalletLevel"];
                ddWalletLevel.DataValueField = "ContractLevelID";
                ddWalletLevel.DataTextField = "ContractLevelName";
                ddWalletLevel.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadCombobox()
    {
        loadCombobox_KYCLevel();
        loadCombobox_WalletLevel();
        loadCombobox_PaperType();
        loadCombobox_Nation();
        loadCombobox_MoneySource();
    }

    protected void loadDataMoneySource(object sender, EventArgs e)
    {
        Hashtable ht = new Hashtable();
        Dictionary<object, object> ob = new Dictionary<object, object>();
        ob.Add("ACCTNO", txtMoneySourceNumber.Text);
        ht = _service.CallCore("SEMS_GETINFOBANKACC", ob,"Get info by source number","N", ref IPCERRORCODE, ref IPCERRORDESC);
        if (ht != null)
        {
            lbCurrency.Text = ht["CURRENCYID"].ToString();
            txtEffectiveDate.Text = ht["OPENDATE"].ToString();
        }
        else
        {
            lblError.Text = Resources.labels.MoneySourceNumber + " incorrect!";
        }
    }


    private void loadData()
    {
        DataSet ds = new DataSet();
        object[] _object = new object[] { MerchantID };
        ds = _service.common("WAL_GET_MER_BY_ID", _object, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtMerchantCode.Text = ds.Tables[0].Rows[0]["USERID"].ToString();
                txtMerchantName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                txtPhoneNumber.Text = ds.Tables[0].Rows[0]["PHONE_NUMBER"].ToString();
                //ddPaperType.SelectedValue = ds.Tables[0].Rows[0]["PAPER_NO"].ToString();
                txtPaperNumber.Text = ds.Tables[0].Rows[0]["PAPER_NO"].ToString();
                txtIssueDate.Text = ds.Tables[0].Rows[0]["ISSUEDATE"].ToString();
                txtExpiryDate.Text = ds.Tables[0].Rows[0]["EXPIRY_DATE"].ToString();
                ddNationality.SelectedValue = ds.Tables[0].Rows[0]["NATION"].ToString();
                txtAddress.Value = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                //ddKYCLevel.SelectedValue = ds.Tables[0].Rows[0]["KYC_LEVEL"].ToString();
                //ddWalletLevel.SelectedValue = ds.Tables[0].Rows[0]["WALLET_LEVEL"].ToString();
                string bank_code = ds.Tables[0].Rows[0]["BANKID"].ToString();
                string bank_name = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                txtMoneySourceNumber.Text = ds.Tables[0].Rows[0]["MONEY_SOURCE"].ToString();
                //txtExpiryDate2.Text = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                txtCreateDate.Text = ds.Tables[0].Rows[0]["DATECREATED"].ToString();
                txtCreateBy.Text = ds.Tables[0].Rows[0]["USERCREATED"].ToString();
                if (!bank_code.Equals(string.Empty) || !bank_name.Equals(string.Empty))
                {
                    txtBankName.Text = string.Format("{0} - {1}", bank_code, bank_name);
                }
            }
        }
    }

    void loadData_Repeater()
    {
        rptData.DataSource = null;
        DataSet ds = new DataSet();
        object[] _object = new object[] { txtPhoneNumber.Text, IPC.PARAMETER.MERCHANT_AGENT };
        ds = _service.common("SEMS_DOC_BY_PHONE", _object, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                rptData.DataSource = ds;
                rptData.DataBind();
            }
        }
    }

    void BindData()
    {
        try
        {
            loadCombobox();
            loadData();
            loadData_Repeater();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
   
    protected void btnCancel_click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    private void setDefaultColor()
    {
        txtMoneySourceNumber.BorderColor = System.Drawing.Color.Empty;
        txtMerchantName.BorderColor = System.Drawing.Color.Empty;
        txtPaperNumber.BorderColor = System.Drawing.Color.Empty;
        txtIssueDate.BorderColor = System.Drawing.Color.Empty;
        txtExpiryDate.BorderColor = System.Drawing.Color.Empty;
        txtEmail.BorderColor = System.Drawing.Color.Empty;
        txtBankName.SetDefault();
    }
    protected void btnReject_click(object sender, EventArgs e)
    {
        if (!CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE)) return;
        RedirectToActionPage(IPC.ACTIONPAGE.REJECT, "&" + SmartPortal.Constant.IPC.ID + "=" + MerchantID);
    }

    private bool Validation()
    {
        if (txtMerchantName.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.MerchantName + Resources.labels.IsNotNull;
            txtMerchantName.BorderColor = System.Drawing.Color.Red;
            txtMerchantName.Focus();
            return false;
        }
        if (txtPaperNumber.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.PaperNumber + Resources.labels.IsNotNull;
            txtPaperNumber.BorderColor = System.Drawing.Color.Red;
            txtPaperNumber.Focus();
            return false;
        }
        if (txtIssueDate.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.IssueDate + Resources.labels.IsNotNull;
            txtIssueDate.BorderColor = System.Drawing.Color.Red;
            txtIssueDate.Focus();
            return false;
        }
        if (txtExpiryDate.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.ExpiryDate + Resources.labels.IsNotNull;
            txtExpiryDate.BorderColor = System.Drawing.Color.Red;
            txtExpiryDate.Focus();
            return false;
        }
        if (txtAddress.Value.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.address + Resources.labels.IsNotNull;
            //txtAddress.BorderColor = System.Drawing.Color.Red;
            txtAddress.Focus();
            return false;
        }
        if (txtEmail.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.email + Resources.labels.IsNotNull;
            txtEmail.BorderColor = System.Drawing.Color.Red;
            txtEmail.Focus();
            return false;
        }
        if (txtBankName.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.BankName + Resources.labels.IsNotNull;
            txtBankName.SetError();
            return false;
        }
        if (txtMoneySourceNumber.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.MoneySourceNumber + Resources.labels.IsNotNull;
            txtMoneySourceNumber.BorderColor = System.Drawing.Color.Red;
            txtMoneySourceNumber.Focus();
            return false;
        }
        return true;
    }

    void SetPara(Dictionary<object, object> para)
    {
        para.Add("USERID", MerchantID);
        para.Add("USER_APPROVE", HttpContext.Current.Session["userID"].ToString());
    }
    protected void btnApprove_click(object sender, EventArgs e)
    {
        try
        {
            setDefaultColor();
            if (!Validation()) return;
            DataSet ds = new DataSet();

            Dictionary<object, object> _para = new Dictionary<object, object>();
            SetPara(_para);
            //object[] _object = new object[] { MerchantID, HttpContext.Current.Session["userID"].ToString()};
            ds = _service.CallStore("SEMS_MER_APPROVE", _para,"Approve Profile Merchant","N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.ApproveSuccessfully;
                btnSubmit.Enabled = false;
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

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();

        switch (commandName)
        {
            case IPC.ACTIONPAGE.DETAILS:
                PreviewImage.ViewImage(commandArg);
                //ImageView.Attributes.Add("src", "data:image/png;base64," + commandArg);
                break;
        }
    }
    protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // If the Repeater contains no data.
        if (rptData.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblFooter = (Label)e.Item.FindControl("lblErrorMsg");
                lblFooter.Visible = true;
            }
        }
    }
}