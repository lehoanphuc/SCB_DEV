using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class Widgets_SEMSConsumerProfile_Controls_General : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    string MerchantID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            ACTION = GetActionPage();
            ViewState["UserID"] = HttpContext.Current.Session["userID"].ToString();
            MerchantID = GetParamsPage(IPC.ID)[0].Trim();
            ViewState["MERCHANT_ID"] = MerchantID;
            if (!IsPostBack)
            {
                loadCombobox();
                setControldefault();
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void loadCombobox_Status()
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
        catch(Exception ex)
        {

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
                        ddlKyclevel.DataSource = ds;
                        ddlKyclevel.DataValueField = "KycID";
                        ddlKyclevel.DataTextField = "KycName";
                        ddlKyclevel.DataBind();
                    }
                }
                Cache.Insert("Wallet_KYCLevel", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlKyclevel.DataSource = (DataSet)Cache["Wallet_KYCLevel"];
                ddlKyclevel.DataValueField = "KycID";
                ddlKyclevel.DataTextField = "KycName";
                ddlKyclevel.DataBind();
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
                        ddPaperType_Collapse.DataSource = ds;
                        ddPaperType_Collapse.DataValueField = "ValueID";
                        ddPaperType_Collapse.DataTextField = "Caption";
                        ddPaperType_Collapse.DataBind();
                    }
                }
                Cache.Insert("Wallet_PaperType", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddPaperType_Collapse.DataSource = (DataSet)Cache["Wallet_PaperType"];
                ddPaperType_Collapse.DataValueField = "ValueID";
                ddPaperType_Collapse.DataTextField = "Caption";
                ddPaperType_Collapse.DataBind();
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
                        ddNationality_Collapse.DataSource = ds;
                        ddNationality_Collapse.DataValueField = "NationCode";
                        ddNationality_Collapse.DataTextField = "NationName";
                        ddNationality_Collapse.DataBind();
                    }
                }
                Cache.Insert("Wallet_Nation", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddNationality_Collapse.DataSource = (DataSet)Cache["Wallet_Nation"];
                ddNationality_Collapse.DataValueField = "NationCode";
                ddNationality_Collapse.DataTextField = "NationName";
                ddNationality_Collapse.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void loadCombobox()
    {
        loadCombobox_Status();
        loadCombobox_KYCLevel();
        loadCombobox_WalletLevel();
        loadCombobox_PaperType();
        loadCombobox_Nation();
    }

    private void loadData()
    {
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { MerchantID };
        ds = _service.common("WAL_GET_MER_BY_ID", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count < 1) return;
                DataTable dataTable = ds.Tables[0];
                txtMerchantCode.Text = dataTable.Rows[0]["USERID"].ToString();
                txtDateCreate.Text = dataTable.Rows[0]["DATECREATED"].ToString();
                txtPhoneNumber.Text = dataTable.Rows[0]["PHONE_NUMBER"].ToString();
                txtLastModifiedate.Text = dataTable.Rows[0]["DATEMODIFIED"].ToString();
                ddlStatus.SelectedValue = dataTable.Rows[0]["AM_STATUS"].ToString();
                txtCreateBy.Text = dataTable.Rows[0]["USERCREATED"].ToString();
                ddlKyclevel.SelectedValue = dataTable.Rows[0]["KYC_ID"].ToString();
                txtApproveBy.Text = dataTable.Rows[0]["USER_APPROVED"].ToString();
                ddWalletLevel.SelectedValue = dataTable.Rows[0]["CONTRAC_LEVEL_ID"].ToString();

                txtMerchantName_Collapse.Text = dataTable.Rows[0]["FULLNAME"].ToString();
                txtMerchantCode_Collapse.Text = dataTable.Rows[0]["USERID"].ToString();
                txtPhoneNumber_Collapse.Text = dataTable.Rows[0]["PHONE_NUMBER"].ToString();

                txtlPaperNumber_Collapse.Text = dataTable.Rows[0]["PAPER_NO"].ToString();
                txtIssueDate_Collapse.Text = dataTable.Rows[0]["ISSUEDATE"].ToString();
                txtExpiryDate_Collapse.Text = dataTable.Rows[0]["EXPIRY_DATE"].ToString();
                ddNationality_Collapse.SelectedValue = dataTable.Rows[0]["NATION"].ToString().Trim();
                txtAddress_Collapse.Text = dataTable.Rows[0]["ADDRESS"].ToString();
                txtemail_Collapse.Text = dataTable.Rows[0]["EMAIL"].ToString();
                ddPaperType_Collapse.SelectedValue = dataTable.Rows[0]["PAPER_TYPE"].ToString();
            }
        }
    }
    public void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    txtAddress_Collapse.Enabled = true;
                    btSaveGeneral.Visible = false;
                    loadData();
                    break;
                default:
                    loadData();
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    public void setControldefault()
    {
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                break;
            case IPC.ACTIONPAGE.EDIT:
                break;
            case IPC.ACTIONPAGE.DETAILS:
                pnGeneral.Enabled = false;
                btSaveGeneral.Enabled = false;
                break;

        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
       
        try
        {
            txtExpiryDate_Collapse.BorderColor = System.Drawing.Color.Empty;
            txtIssueDate_Collapse.BorderColor = System.Drawing.Color.Empty;

            if (!Utility.CheckSpecialCharacters(txtIssueDate_Collapse.Text.Trim()))
            {
                lblError.Text = Resources.labels.IssueDate + Resources.labels.ErrorSpeacialCharacters;
                txtIssueDate_Collapse.BorderColor = System.Drawing.Color.Red;
                txtIssueDate_Collapse.Focus();
                return;
            }

            if (!Utility.CheckSpecialCharacters(txtExpiryDate_Collapse.Text.Trim()))
            {
                lblError.Text = Resources.labels.ExpiryDate + Resources.labels.ErrorSpeacialCharacters;
                txtExpiryDate_Collapse.BorderColor = System.Drawing.Color.Red;
                txtExpiryDate_Collapse.Focus();
                return;
            }

            if (txtExpiryDate_Collapse.Text.Trim().ToString().Equals("")) txtExpiryDate_Collapse.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (txtIssueDate_Collapse.Text.Trim().ToString().Equals("")) txtIssueDate_Collapse.Text = DateTime.Now.ToString("dd/MM/yyyy");
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            DateTime IssueDate =  DateTime.ParseExact(txtIssueDate_Collapse.Text.Trim().ToString(), "dd/MM/yyyy", null);
            DateTime ExpiryDate = DateTime.ParseExact(txtExpiryDate_Collapse.Text.Trim().ToString(), "dd/MM/yyyy", null);
            #region validatiton
            if (IssueDate >= ExpiryDate)
            {
                lblError.Text = string.Format(" {0} must be greater than {1}", Resources.labels.ExpiryDate, Resources.labels.IssueDate);
                txtExpiryDate_Collapse.BorderColor = System.Drawing.Color.Red;
                txtIssueDate_Collapse.BorderColor = System.Drawing.Color.Red;
                txtExpiryDate_Collapse.Focus();
                txtIssueDate_Collapse.Focus();
                return;
            }

            if (!Utility.CheckSpecialCharacters(txtAddress_Collapse.Text.Trim()))
            {
                lblError.Text = Resources.labels.address + Resources.labels.ErrorSpeacialCharacters;
                txtAddress_Collapse.Focus();
                return;
            }

            if (txtAddress_Collapse.Text.Trim().Equals(string.Empty))
            {
                lblError.Text = Resources.labels.address + " is not null";
                txtAddress_Collapse.Focus();
                return;
            }
            #endregion

            DataSet ds = new DataSet();
            object[] _object = new object[] { MerchantID, IssueDate, ExpiryDate, ddNationality_Collapse.SelectedValue.Trim(), txtAddress_Collapse.Text.Trim(), HttpContext.Current.Session["userID"].ToString()};
            ds = _service.common("SEMS_MER_EDIT", _object, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.updatesuccessfully;
                BindData();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
      RedirectBackToMainPage();
    }
    protected override void Render(HtmlTextWriter writer)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class=\"content \" >");
        sb.Append("<div>");
        writer.Write(sb.ToString());
        base.Render(writer);
        writer.Write("</div>");
    }
}