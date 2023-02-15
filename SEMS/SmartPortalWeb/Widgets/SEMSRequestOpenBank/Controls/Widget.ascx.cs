using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;
using System.Web.UI;
using System.Drawing;
using System.IO;

public partial class Widgets_SEMSREGIONFEE_Controls_Widget : WidgetBase
{
    public static bool isAscend = false;
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string FORMAT_DATE = "dd/MM/yyyy";
    string FORMAT_IMAGE = "data:image/jpg;base64,";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();


    public List<DocumentModel> listDocumentModel
    {
        get
        {
            // check if not exist to make new (normally before the post back)
            // and at the same time check that you did not use the same viewstate for other object
            if (!(ViewState["listDoc"] is List<DocumentModel>))
            {
                // need to fix the memory and added to viewstate
                ViewState["listDoc"] = new List<DocumentModel>();
            }

            return (List<DocumentModel>)ViewState["listDoc"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (ACTION == IPC.ACTIONPAGE.EDIT)
            {
                ViewState["REQUESTNO"] = GetParamsPage(IPC.ID)[0].Trim();
                lblTitleBranch.Text = Resources.labels.kycConsumer + " - " + Resources.labels.edit;
            }
            if (ACTION == IPC.ACTIONPAGE.ADD)
            {
                lblTitleBranch.Text = Resources.labels.kycConsumer + " - " + Resources.labels.add;
            }
            if (!IsPostBack)
            {
                BindData();
            }
            Page.Form.DefaultButton = btnSave.UniqueID;
            pnNewNRIC.Visible = true;
            pnLicense.Visible = false;
            PnPassport.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void setDisableControls()
    {
        txtFullName.Enabled = false;
        //ddlPaperType.Enabled = false;
        txtPaperNumber.Enabled = false;
        txtIssueDate.Enabled = false;
        txtExpireDate.Enabled = false;
        txtBirthday.Enabled = false;
        ddlGender.Enabled = false;
        ddlNationality.Enabled = false;
        txtAddress.Enabled = false;
        ddlKycLevel.Enabled = false;
        txtcreatedate.Enabled = false;
        txtCreateby.Enabled = false;
    }

    private void setEmptymControls()
    {
        lblError.Text = string.Empty;
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                //NRIC
                txtPaperNumberImport.Text = string.Empty;
                txtIssueDateImport.Text = string.Empty;
                //Passport
                txtPassPortNo.Text = string.Empty;
                txtDatetimePassport.Text = string.Empty;
                //License
                txtLicenseNumber.Text = string.Empty;
                //Info
                txtIssueDate.Text = string.Empty;
                txtExpireDate.Text = string.Empty;
                txtPaperNumber.Text = string.Empty;
                txtPhoneNumber.Text = string.Empty;
                txtFullName.Text = string.Empty;
                txtBirthday.Text = string.Empty;
                txtAddress.Text = string.Empty;
                break;
            case IPC.ACTIONPAGE.EDIT:
                txtIssueDate.Text = string.Empty;
                txtExpireDate.Text = string.Empty;
                txtPaperNumber.Text = string.Empty;

                switch (ddlKycLevel.SelectedValue.Trim().ToUpper())
                {
                    case "PASSPORT":
                        txtPassPortNo.Text = string.Empty;
                        txtDatetimePassport.Text = string.Empty;
                        break;
                    case "NRIC":
                        txtPaperNumberImport.Text = string.Empty;
                        txtIssueDateImport.Text = string.Empty;
                        break;
                    case "LICENSE":
                        txtLicenseNumber.Text = string.Empty;
                        break;
                }
                break;
        }
    }

    public void setDefauleControl()
    {
        btnSave.Enabled = true;
        txtPhoneNumber.Enabled = false;
        txtFullName.Enabled = false;
        //ddlPaperType.Enabled = false;
        txtPaperNumber.Enabled = false;
        ddlWalletLevel.Enabled = false;
        ddlStatus.Enabled = false;
        ddlKycLevel.Enabled = false;
        txtIssueDate.Attributes.Add("placeholder", FORMAT_DATE);
        txtExpireDate.Attributes.Add("autocomplete", "off");
        txtIssueDate.Attributes.Add("placeholder", FORMAT_DATE);
        txtExpireDate.Attributes.Add("autocomplete", "off");
        txtBirthday.Attributes.Add("placeholder", FORMAT_DATE);
        txtBirthday.Attributes.Add("autocomplete", "off");
    }
    private void loadCombobox_Gender()
    {
        DataSet ds = new DataSet();
        ds = _service.GetValueList("GENDER", "SYS", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGender.DataSource = ds;
                ddlGender.DataValueField = "VALUEID";
                ddlGender.DataTextField = "CAPTION";
                ddlGender.DataBind();
            }
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
                        ddlKycLevel.DataSource = ds;
                        ddlKycLevel.DataValueField = "KycCode";
                        ddlKycLevel.DataTextField = "KycName";
                        ddlKycLevel.DataBind();
                    }
                }
                Cache.Insert("Wallet_KYCLevel", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlKycLevel.DataSource = (DataSet)Cache["Wallet_KYCLevel"];
                ddlKycLevel.DataValueField = "KycCode";
                ddlKycLevel.DataTextField = "KycName";
                ddlKycLevel.DataBind();
            }
            ddlKycLevel.SelectedValue = "NRIC";
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
                        ddlWalletLevel.DataSource = ds;
                        ddlWalletLevel.DataValueField = "ContractLevelID";
                        ddlWalletLevel.DataTextField = "ContractLevelName";
                        ddlWalletLevel.DataBind();
                    }
                }
                Cache.Insert("Wallet_WalletLevel", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlWalletLevel.DataSource = (DataSet)Cache["Wallet_WalletLevel"];
                ddlWalletLevel.DataValueField = "ContractLevelID";
                ddlWalletLevel.DataTextField = "ContractLevelName";
                ddlWalletLevel.DataBind();
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
                        ddlNationality.DataSource = ds;
                        ddlNationality.DataValueField = "NationCode";
                        ddlNationality.DataTextField = "NationName";
                        ddlNationality.DataBind();

                        ddlContryPassport.DataSource = ds;
                        ddlContryPassport.DataValueField = "NationCode";
                        ddlContryPassport.DataTextField = "NationName";
                        ddlContryPassport.DataBind();
                    }
                }
                Cache.Insert("Wallet_Nation", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlNationality.DataSource = (DataSet)Cache["Wallet_Nation"];
                ddlNationality.DataValueField = "NationCode";
                ddlNationality.DataTextField = "NationName";
                ddlNationality.DataBind();

                ddlContryPassport.DataSource = (DataSet)Cache["Wallet_Nation"];
                ddlContryPassport.DataValueField = "NationCode";
                ddlContryPassport.DataTextField = "NationName";
                ddlContryPassport.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    
    private void loadCombobox_Status()
    {
        // Save list STT KYC Request Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_STT_KYCRequest"];
            if (ds == null)
            {
                ds = _service.GetValueList("WAL_KYC_REQUEST", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
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
                Cache.Insert("Wallet_STT_KYCRequest", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlStatus.DataSource = (DataSet)Cache["Wallet_STT_KYCRequest"];
                ddlStatus.DataValueField = "ValueID";
                ddlStatus.DataTextField = "Caption";
                ddlStatus.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadCombobox_KYCDocumentName()
    {
        // Save list STT KYC Request Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_KYCDocumentName"];
            if (ds == null)
            {
                ds = _service.GetValueList("KYCDocumentName", "TYPE", ref IPCERRORCODE, ref IPCERRORDESC);
                Cache.Insert("Wallet_KYCDocumentName", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                ViewState["listDocumentType"] = ds.Tables[0];

                ddlDocumentTypeImport.DataSource = ds;
                ddlDocumentTypeImport.DataValueField = "ValueID";
                ddlDocumentTypeImport.DataTextField = "Caption";
                ddlDocumentTypeImport.DataBind();

                if (ddlKycLevel.SelectedValue.Trim().ToUpper() == "NRIC")
                {
                    ListItem removeItemPP = ddlDocumentTypeImport.Items.FindByValue("PP");
                    ListItem removeItemAC = ddlDocumentTypeImport.Items.FindByValue("AC");
                    ListItem removeItemBD = ddlDocumentTypeImport.Items.FindByValue("BD");
                    ListItem removeItemLC = ddlDocumentTypeImport.Items.FindByValue("LC");
                    ddlDocumentTypeImport.Items.Remove(removeItemPP);
                    ddlDocumentTypeImport.Items.Remove(removeItemAC);
                    ddlDocumentTypeImport.Items.Remove(removeItemBD);
                    ddlDocumentTypeImport.Items.Remove(removeItemLC);
                }
            }
            else
            {
                ViewState["listDocumentType"] = ds.Tables[0];
                ddlDocumentTypeImport.DataSource = ds;
                ddlDocumentTypeImport.DataValueField = "ValueID";
                ddlDocumentTypeImport.DataTextField = "Caption";
                ddlDocumentTypeImport.DataBind();

                if (ddlKycLevel.SelectedValue.Trim().ToUpper() == "NRIC")
                {
                    ListItem removeItemPP = ddlDocumentTypeImport.Items.FindByValue("PP");
                    ListItem removeItemAC = ddlDocumentTypeImport.Items.FindByValue("AC");
                    ListItem removeItemBD = ddlDocumentTypeImport.Items.FindByValue("BD");
                    ListItem removeItemLC = ddlDocumentTypeImport.Items.FindByValue("LC");
                    ddlDocumentTypeImport.Items.Remove(removeItemPP);
                    ddlDocumentTypeImport.Items.Remove(removeItemAC);
                    ddlDocumentTypeImport.Items.Remove(removeItemBD);
                    ddlDocumentTypeImport.Items.Remove(removeItemLC);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadCombobox_KYCDocumentName_Repeater(string type)
    {
        // Save list STT KYC Request Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_KYCDocumentType"];
            if (ds == null)
            {
                ds = _service.GetValueList("KYCDocumentName", "TYPE", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlDocumentType.DataSource = ds;
                        ddlDocumentType.DataValueField = "ValueID";
                        ddlDocumentType.DataTextField = "Caption";
                        ddlDocumentType.DataBind();

                        if (type.Trim().ToUpper() == "NF" || type.Trim().ToUpper() == "NB" || type.Trim().ToUpper() == "SN")
                        {
                            ddlDocumentType.Items.Remove(ddlDocumentType.Items.FindByValue("PP"));
                            ddlDocumentType.Items.Remove(ddlDocumentType.Items.FindByValue("AC"));
                            ddlDocumentType.Items.Remove(ddlDocumentType.Items.FindByValue("BD"));
                            ddlDocumentType.Items.Remove(ddlDocumentType.Items.FindByValue("LC"));
                        }
                    }
                }
                Cache.Insert("Wallet_KYCDocumentType", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlDocumentType.DataSource = ds;
                ddlDocumentType.DataValueField = "ValueID";
                ddlDocumentType.DataTextField = "Caption";
                ddlDocumentType.DataBind();
                if (type.Trim().ToUpper() == "NF" || type.Trim().ToUpper() == "NB" || type.Trim().ToUpper() == "SN")
                {
                    ddlDocumentType.Items.Remove(ddlDocumentType.Items.FindByValue("PP"));
                    ddlDocumentType.Items.Remove(ddlDocumentType.Items.FindByValue("AC"));
                    ddlDocumentType.Items.Remove(ddlDocumentType.Items.FindByValue("BD"));
                    ddlDocumentType.Items.Remove(ddlDocumentType.Items.FindByValue("LC"));
                }
                
            }
            if (!IsPostBack)
            {
                ViewState["listDocumentType"] = ds.Tables[0];
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
        loadCombobox_Nation();
        loadCombobox_Status();
        loadCombobox_Gender();
        loadCombobox_KYCDocumentName();
    }

    private void loadData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(ViewState["REQUESTNO"].ToString()) };
            ds = _service.common("SEMS_GET_INFOKYC_CON", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count < 1) return;
                    DataTable dataTable = ds.Tables[0];
                    txtPhoneNumber.Text = dataTable.Rows[0]["PHONE"].ToString();
                    txtFullName.Text = dataTable.Rows[0]["FULLNAME"].ToString();
                    //ddlPaperType.SelectedValue = dataTable.Rows[0]["PaperType"].ToString();
                    txtPaperNumber.Text = dataTable.Rows[0]["PaperNO"].ToString();
                    txtIssueDate.Text = dataTable.Rows[0]["ISSUEDATE"].ToString();
                    txtExpireDate.Text = dataTable.Rows[0]["ExpiryDate"].ToString();
                    txtBirthday.Text = dataTable.Rows[0]["DOB"].ToString();
                    ddlGender.Text = dataTable.Rows[0]["SEX"].ToString();
                    txtAddress.Text = dataTable.Rows[0]["ADDRRESIDENT"].ToString();
                    ddlWalletLevel.SelectedValue = dataTable.Rows[0]["WALLET_LEVEL"].ToString().Trim();
                    ddlStatus.SelectedValue = dataTable.Rows[0]["APPROVE_STATUS"].ToString().Trim();
                    ddlNationality.SelectedValue = dataTable.Rows[0]["NATION"].ToString().Trim();
                    ddlKycLevel.SelectedValue = dataTable.Rows[0]["KYC_CODE"].ToString();
                    txtcreatedate.Text = dataTable.Rows[0]["DateCreated"].ToString();
                    txtCreateby.Text = dataTable.Rows[0]["UserCreated"].ToString();
                    ViewState["CustID"] = dataTable.Rows[0]["CustID"].ToString();
                    ViewState["REQUESTNO"] = ID;
                    hdfUserID.Value = dataTable.Rows[0]["UserID"].ToString();

                    string APPROVE_STATUS = dataTable.Rows[0]["APPROVE_STATUS"].ToString();
                    if (!APPROVE_STATUS.Equals("P"))
                    {
                        btnSave.Enabled = false;
                        btnClear.Enabled = false;
                        btnImport.Enabled = false;
                        documentUpload.Enabled = false;
                        pnRegion.Enabled = false;
                        ViewState["APPROVE_STATUS"] = APPROVE_STATUS;
                    }
                    else
                    {
                        ViewState["APPROVE_STATUS"] = APPROVE_STATUS;
                    }
                    loadData_Repeater(ds);

                    if (ddlKycLevel.SelectedValue.Trim().ToUpper() == "PASSPORT")
                    {
                        txtPassPortNo.Text = ds.Tables[0].Rows[0]["PaperNO"].ToString();
                        txtDatetimePassport.Text = ds.Tables[0].Rows[0]["ISSUEDATE"].ToString();
                    }
                    if (ddlKycLevel.SelectedValue.Trim().ToUpper() == "NRIC")
                    {
                        txtPaperNumberImport.Text = ds.Tables[0].Rows[0]["PaperNO"].ToString();
                        txtIssueDateImport.Text = ds.Tables[0].Rows[0]["ISSUEDATE"].ToString();
                    }
                    if (ddlKycLevel.SelectedValue.Trim().ToUpper() == "LICENSE")
                    {
                        txtLicenseNumber.Text = ds.Tables[0].Rows[0]["PaperNO"].ToString();
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    protected void LoadDataConsumerByPhone(object sender, EventArgs e)
    {
        try
        {
            // clear Data
            txtFullName.Text = String.Empty;
            hdfUserID.Value = String.Empty;
            txtPaperNumber.Text = String.Empty;
            txtIssueDate.Text = String.Empty;
            txtExpireDate.Text = String.Empty;
            txtBirthday.Text = String.Empty;
            txtAddress.Text = String.Empty;
            // end clear
            DataSet ds = new DataSet();
            lblError.Text = string.Empty;
            setDefaultColor();

            if (!Utility.CheckSpecialCharacters(txtPhoneNumber.Text))
            {
                lblError.Text = Resources.labels.PhoneNumber + Resources.labels.ErrorSpeacialCharacters;
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return;
            }

            object[] searchObject = new object[] {  txtPhoneNumber.Text.Trim() };
            ds = _service.common("SEMS_BO_GET_CONSUMER", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtFullName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                        hdfUserID.Value = ds.Tables[0].Rows[0]["USERID"].ToString();
                        
                        txtBirthday.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                        txtAddress.Text = ds.Tables[0].Rows[0]["ADDRRESIDENT"].ToString();
                        string nation = ds.Tables[0].Rows[0]["NATION"].ToString().Trim();
                        if (nation.Trim() != string.Empty)
                        {
                            ddlNationality.SelectedValue = nation;
                        }
                        else
                        {
                            ddlNationality.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                            ddlNationality.SelectedValue = string.Empty;
                        }
                        if (ddlKycLevel.Items.FindByValue(ds.Tables[0].Rows[0]["KYC_CODE"].ToString().Trim()) != null)
                        {
                            ddlKycLevel.SelectedValue = ds.Tables[0].Rows[0]["KYC_CODE"].ToString().Trim();

                            txtPaperNumber.Text = ds.Tables[0].Rows[0]["LICENSEID"].ToString();
                            txtIssueDate.Text = ds.Tables[0].Rows[0]["ISSUEDATE"].ToString();
                            txtExpireDate.Text = ds.Tables[0].Rows[0]["EXPIRYDATE"].ToString();

                            if (ddlKycLevel.SelectedValue.Trim().ToUpper() == "PASSPORT")
                            {
                                txtPassPortNo.Text = ds.Tables[0].Rows[0]["LICENSEID"].ToString();
                                txtDatetimePassport.Text = ds.Tables[0].Rows[0]["ISSUEDATE"].ToString();
                            }
                            if (ddlKycLevel.SelectedValue.Trim().ToUpper() == "NRIC")
                            {
                                txtPaperNumberImport.Text = ds.Tables[0].Rows[0]["LICENSEID"].ToString();
                                txtIssueDateImport.Text = ds.Tables[0].Rows[0]["LICENSEID"].ToString();
                            }
                            if (ddlKycLevel.SelectedValue.Trim().ToUpper() == "LICENSE")
                            {
                                txtLicenseNumber.Text = ds.Tables[0].Rows[0]["LICENSEID"].ToString();
                            }
                        }
                        ddlWalletLevel.SelectedValue = ds.Tables[0].Rows[0]["CONTRACTLEVELID"].ToString().Trim();
                        //ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUS_EBACONTRACT"].ToString().Trim();
                        string sex = ds.Tables[0].Rows[0]["SEX"].ToString().Trim();
                        if (sex.Trim() != string.Empty)
                        {
                            ddlGender.SelectedValue = sex;
                        }
                        else
                        {
                            ddlGender.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                            ddlGender.SelectedValue = string.Empty;
                        }

                        string status_Contract = ds.Tables[0].Rows[0]["STATUS_EBACONTRACT"].ToString().Trim();
                        if (status_Contract != "A" && status_Contract != "N")
                        {
                            lblError.Text = "Status of contract is invalid";
                            return;
                        }

                        //ddlPaperType.SelectedValue = ds.Tables[0].Rows[0]["LICENSETYPE"].ToString().Trim();
                        DataSet dscheck = new DataSet();
                        object[] _ObCheckUserApprove = new object[] { Utility.KillSqlInjection(txtPhoneNumber.Text.Trim()) };
                        dscheck = _service.common("SEMS_CHECKUSERAPPR", _ObCheckUserApprove, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            if (dscheck.Tables.Count > 0)
                                if (dscheck.Tables[0].Rows.Count > 0)
                                {
                                    if (dscheck.Tables[0].Rows.Count > 0)
                                        if (!dscheck.Tables[0].Rows[0]["ROWAPPROVE"].ToString().Equals("0"))
                                        {
                                            lblError.Text = Resources.labels.errorstatus;
                                            //lblError.Text = "Phone number is already approved KYC request!";
                                            return;
                                        }


                                }
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.PhoneNumber + Resources.labels.Incorrect;
                        txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                        txtPhoneNumber.Focus();
                    }
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void BindData()
    {
        try
        {

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    loadCombobox();
                    setDisableControls();
                    ddlWalletLevel.Enabled = false;
                    txtPaperNumber.Enabled = true;
                    txtIssueDate.Enabled = true;
                    txtExpireDate.Enabled = true;
                    ddlKycLevel.Enabled = true;
                    lbStatusKYC.Visible = false;
                    ddlStatus.Visible = false;
                    create.Visible = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    setDefauleControl();
                    setDisableControls();
                    loadCombobox();
                    loadData();
                    txtPaperNumber.Enabled = true;
                    txtIssueDate.Enabled = true;
                    txtExpireDate.Enabled = true;
                    //documentUpload.Visible = false;
                    //btnImport.Visible = false;
                    break;
            }
            if (ddlDocumentTypeImport != null)
            {
                txtDocumentNameImport.Text = ddlDocumentTypeImport.SelectedItem.Text;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void loadData_Repeater(DataSet ds)
    {
        if (!IsPostBack)
        {
            loadData_ListDocument(ds);
        }
        else
        {
            if (listDocumentModel != null)
            {
                rptData.DataSource = listDocumentModel;
                rptData.DataBind();
            }
            else
            {
                loadData_ListDocument(ds);
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {

            foreach (DocumentModel item in listDocumentModel)
            {
                if (item.No.ToString() == ViewState["No"].ToString())
                {
                    item.DocumentName = txtDocumentName.Text;
                    item.DocumentType = ddlDocumentType.SelectedValue;
                    item.IsUpdate = true;
                    break;
                }
            }
            rptData.DataSource = listDocumentModel;
            rptData.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + Image.ClientID + "');", true);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void loadData_ListDocument(DataSet ds)
    {
        listDocumentModel.Clear();
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DocumentModel item = new DocumentModel();
                    item.IsNew = false;
                    item.No = int.Parse(ds.Tables[0].Rows[i]["No"].ToString().Trim());
                    item.DocumentID = int.Parse(ds.Tables[0].Rows[i]["DocumentID"].ToString().Trim());
                    item.DocumentName = ds.Tables[0].Rows[i]["DocumentName"].ToString().Trim();
                    item.DocumentType = ds.Tables[0].Rows[i]["DocumentType"].ToString().Trim();
                    item.File = ds.Tables[0].Rows[i]["File"].ToString();
                    listDocumentModel.Add(item);
                }
                rptData.DataSource = listDocumentModel;
                rptData.DataBind();
            }
        }
    }

    private bool Check_Import()
    {
        if (ddlKycLevel.SelectedValue.ToUpper() == "PASSPORT")
        {
            foreach (var item in listDocumentModel)
            {
                foreach (ListItem item2 in ddlDocumentTypeImport.Items)
                {
                    if (item.DocumentType == item2.Value)
                    {
                        return false;
                    }
                }
            }
        }
        if (ddlKycLevel.SelectedValue.ToUpper() == "NRIC")
        {
            foreach (var item in listDocumentModel)
            {
                
                if (item.DocumentType == "PP")
                {
                    return false;
                }
                if (item.DocumentType == "LC")
                {
                    return false;
                }
            }
        }
        if (ddlKycLevel.SelectedValue.ToUpper() == "LICENSE")
        {
            foreach (var item in listDocumentModel)
            {

                if (item.DocumentType == "PP")
                {
                    return false;
                }
                foreach (ListItem item2 in ddlDocumentTypeImport.Items)
                {
                    if (item.DocumentType == item2.Value)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    protected void btnImportFile_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            loadPannelFromKYCLevel();
            #region Validation
            if (!Check_Import())
            {
                lblError.Text = Resources.labels.ErrorKYCType;
                return;
            }
            if (!checkDocumentType())
            {
                lblError.Text = Resources.labels.TheDocumentTypeHasAnError;
                return;
            }
            if (ddlKycLevel.SelectedValue.ToUpper() == "NRIC")
            {
                txtPaperNumberImport.BorderColor = System.Drawing.Color.Empty;
                txtDocumentNameImport.BorderColor = System.Drawing.Color.Empty;
                if (txtPaperNumberImport.Text.Trim().Equals(string.Empty))
                {
                    lblError.Text = Resources.labels.PaperNumber + Resources.labels.IsNotNull;
                    txtPaperNumberImport.BorderColor = System.Drawing.Color.Red;
                    txtPaperNumberImport.Focus();
                    return;
                }

                if (!Utility.CheckSpecialCharacters(txtPaperNumberImport.Text))
                {
                    lblError.Text = Resources.labels.PaperNumber + Resources.labels.ErrorSpeacialCharacters;
                    txtPaperNumberImport.BorderColor = System.Drawing.Color.Red;
                    txtPaperNumberImport.Focus();
                    return;
                }

                if (txtDocumentNameImport.Text.Trim().Equals(string.Empty))
                {
                    lblError.Text = Resources.labels.DocumentName + Resources.labels.IsNotNull;
                    txtDocumentNameImport.BorderColor = System.Drawing.Color.Red;
                    txtDocumentNameImport.Focus();
                    return;
                }

                if (!Utility.CheckSpecialCharacters(txtDocumentNameImport.Text))
                {
                    lblError.Text = Resources.labels.DocumentName + Resources.labels.ErrorSpeacialCharacters;
                    txtDocumentNameImport.BorderColor = System.Drawing.Color.Red;
                    txtDocumentNameImport.Focus();
                    return;
                }

                foreach (var item in listDocumentModel)
                {
                    if (item.DocumentType == ddlDocumentTypeImport.SelectedValue)
                    {
                        lblError.Text = Resources.labels.TheDocumentTypeHasAnError;
                        return;
                    }
                }
            }
            if (ddlKycLevel.SelectedValue.ToUpper() == "PASSPORT")
            {
                txtPassPortNo.BorderColor = System.Drawing.Color.Empty;
                txtDatetimePassport.BorderColor = System.Drawing.Color.Empty;
                if (txtPassPortNo.Text.Trim().Equals(string.Empty))
                {
                    lblError.Text = Resources.labels.passportnonotnull;
                    txtPassPortNo.BorderColor = System.Drawing.Color.Red;
                    txtPassPortNo.Focus();
                    return;
                }

                if (!Utility.CheckSpecialCharacters(txtPassPortNo.Text))
                {
                    lblError.Text = Resources.labels.passport + Resources.labels.ErrorSpeacialCharacters;
                    txtPassPortNo.BorderColor = System.Drawing.Color.Red;
                    txtPassPortNo.Focus();
                    return;
                }

                if (txtDatetimePassport.Text.Trim().Equals(string.Empty))
                {
                    lblError.Text = Resources.labels.Datetimepassport + Resources.labels.IsNotNull;
                    txtDatetimePassport.BorderColor = System.Drawing.Color.Red;
                    txtDatetimePassport.Focus();
                    return;
                }

                if (!Utility.CheckSpecialCharacters(txtDatetimePassport.Text))
                {
                    lblError.Text = Resources.labels.Datetimepassport + Resources.labels.ErrorSpeacialCharacters;
                    txtDatetimePassport.BorderColor = System.Drawing.Color.Red;
                    txtDatetimePassport.Focus();
                    return;
                }

            }
            if (ddlKycLevel.SelectedValue.ToUpper() == "LICENSE")
            {
                txtLicenseNumber.BorderColor = System.Drawing.Color.Empty;
                if (txtLicenseNumber.Text.Trim().Equals(string.Empty))
                {
                    lblError.Text = Resources.labels.licensenonotnull;
                    txtLicenseNumber.BorderColor = System.Drawing.Color.Red;
                    txtLicenseNumber.Focus();
                    return;
                }

                if (!Utility.CheckSpecialCharacters(txtLicenseNumber.Text))
                {
                    lblError.Text = Resources.labels.licensenumber + Resources.labels.ErrorSpeacialCharacters;
                    txtLicenseNumber.BorderColor = System.Drawing.Color.Red;
                    txtLicenseNumber.Focus();
                    return;
                }

            }
            #endregion
            if (documentUpload.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(documentUpload.FileName).ToLower();
                //string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP", ".PDF", ".WEBP" };
                string[] allowedExtensions = {".PNG", ".JPG", ".JPEG", ".BMP"};
                bool checkExtensions = false;
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension.ToUpper() == allowedExtensions[i])
                    {
                        checkExtensions = true;
                        break;
                    }
                }
                if (checkExtensions == false)
                {
                    lblError.Text = "Extensions " + fileExtension + " not support";
                    return;
                }
                //Resize image
                System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(documentUpload.PostedFile.InputStream);
                string base64String;
                int imageHeight = imageToBeResized.Height;
                int imageWidth = imageToBeResized.Width;
                int maxHeight = 400;
                int maxWidth = 600;
                imageHeight = (imageHeight * maxWidth) / imageWidth;
                imageWidth = maxWidth;
                if (imageHeight > maxHeight)
                {
                    imageWidth = (imageWidth * maxHeight) / imageHeight;
                    imageHeight = maxHeight;
                }
                Bitmap bitmap = new Bitmap(imageToBeResized, imageWidth, imageHeight);
                System.IO.MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;
                byte[] byteImage = stream.ToArray();
                base64String = Convert.ToBase64String(byteImage);

                //System.IO.Stream fs = documentUpload.PostedFile.InputStream;
                //System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                //Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                //base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                DocumentModel item = new DocumentModel();
                item.IsNew = true;
                item.DocumentCode = string.Empty;
                item.DateCreated = DateTime.Now.ToString("dd/MM/yyyy");
                item.Status ="P";
                int no = listDocumentModel.Count;
                item.No = no + 1;
                if (pnNewNRIC.Visible == true)
                {
                    item.DocumentName = txtDocumentNameImport.Text.Trim();
                    item.DocumentType = ddlDocumentTypeImport.SelectedValue;
                }
                if (PnPassport.Visible == true)
                {
                    item.DocumentName = txtPassportName.Text.Trim();
                    item.DocumentType = "PP";
                }
                if (pnLicense.Visible == true)
                {
                    item.DocumentName = txtLicenseName.Text.Trim();
                    item.DocumentType = "LC";
                }
                item.File = base64String;
                listDocumentModel.Add(item);
                rptData.DataSource = listDocumentModel;
                rptData.DataBind();
            }
            else
            {
                lblError.Text = Resources.labels.Importfileuploadnotfound;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    //import file in popup
    protected void btnImportFileUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (fileUpdate.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fileUpdate.FileName).ToLower();
                //string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP", ".PDF", ".WEBP" };
                string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP" };
                bool checkExtensions = false;
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension.ToUpper() == allowedExtensions[i])
                    {
                        checkExtensions = true;
                        break;
                    }
                }
                if (checkExtensions == false)
                {
                    lblError.Text = "Extensions " + fileExtension + " not support";
                    return;
                }
                //Resize image
                System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fileUpdate.PostedFile.InputStream);
                string base64String;
                int imageHeight = imageToBeResized.Height;
                int imageWidth = imageToBeResized.Width;
                int maxHeight = 400;
                int maxWidth = 600;
                imageHeight = (imageHeight * maxWidth) / imageWidth;
                imageWidth = maxWidth;
                if (imageHeight > maxHeight)
                {
                    imageWidth = (imageWidth * maxHeight) / imageHeight;
                    imageHeight = maxHeight;
                }
                Bitmap bitmap = new Bitmap(imageToBeResized, imageWidth, imageHeight);
                System.IO.MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;
                byte[] byteImage = stream.ToArray();
                base64String = Convert.ToBase64String(byteImage);

                foreach (DocumentModel item in listDocumentModel)
                {
                    if (item.No.ToString() == ViewState["No"].ToString())
                    {
                        item.File = base64String;
                        item.IsUpdate = true;
                        break;
                    }
                }
                rptData.DataSource = listDocumentModel;
                rptData.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + Image.ClientID + "');", true);
            }
            else
            {
                lblError.Text = Resources.labels.Importfileuploadnotfound;
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void resetNoDocument()
    {
        int index = 1;
        foreach (var item in listDocumentModel)
        {
            item.No = index;
            index++;
        }
    }

    // Delete file in database
    public void deleteDocument(String DocumentID)
    {
        try
        {
            string UserId = HttpContext.Current.Session["userID"].ToString();
            if (!Utility.CheckSpecialCharacters(txtLicenseNumber.Text))
            {
                lblError.Text = Resources.labels.document + Resources.labels.ErrorSpeacialCharacters;
                return;
            }

            DataSet ds = new DataSet();
            object[] searchObject = new object[] { DocumentID, UserId };
            ds = _service.common("SEMS_DOC_DELETE_FILE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.deletesuccessfully;
            }
            else lblError.Text = IPCERRORDESC;

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    private void setDefaultColor()
    {
        txtExpireDate.BorderColor = System.Drawing.Color.Empty;
        txtIssueDate.BorderColor = System.Drawing.Color.Empty;
        txtBirthday.BorderColor = System.Drawing.Color.Empty;
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        txtPaperNumber.BorderColor = System.Drawing.Color.Empty;

        txtPassportName.BorderColor = System.Drawing.Color.Empty;
        ddlContryPassport.BorderColor = System.Drawing.Color.Empty;
        txtPassPortNo.BorderColor = System.Drawing.Color.Empty;
        txtDatetimePassport.BorderColor = System.Drawing.Color.Empty;

        txtDocumentNameImport.BorderColor = System.Drawing.Color.Empty;
        txtPaperNumberImport.BorderColor = System.Drawing.Color.Empty;
        txtIssueDateImport.BorderColor = System.Drawing.Color.Empty;
        ddlDocumentTypeImport.BorderColor = System.Drawing.Color.Empty;

        txtLicenseName.BorderColor = System.Drawing.Color.Empty;
        txtLicenseNumber.BorderColor = System.Drawing.Color.Empty;
    }

    // Check duplicate document name
    private bool checkDocumentType()
    {
        foreach (var item in listDocumentModel)
        {
            foreach (var item2 in listDocumentModel)
            {
                if (item.No != item2.No)
                {
                    if (item.DocumentType == item2.DocumentType)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        setDefaultColor();
        loadPannelFromKYCLevel();
        if (txtPhoneNumber.Text == string.Empty)
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

        if (txtPaperNumber.Text.Trim() == string.Empty)
        {
            lblError.Text = Resources.labels.PaperNumber + Resources.labels.IsNotNull;
            txtPaperNumber.BorderColor = System.Drawing.Color.Red;
            txtPaperNumber.Focus();
            return;
        }
        if ((txtPaperNumber.Text.Trim().Length >= 17 && txtPaperNumber.Text.Trim().Length <= 20)||
            (txtPaperNumber.Text.Trim().Length >= 10 && txtPaperNumber.Text.Trim().Length <= 12))
        {
       
        }
        else
        {
            lblError.Text = Resources.labels.nrcmustcontainonlylettersnumbersandspecialcharactersandbebetween;
            txtPaperNumber.BorderColor = System.Drawing.Color.Red;
            txtPaperNumber.Focus();
            return;
        }
        if (!Utility.CheckSpecialCharacters(txtPaperNumber.Text))
        {
            lblError.Text = Resources.labels.PaperNumber + Resources.labels.ErrorSpeacialCharacters;
            txtPaperNumber.BorderColor = System.Drawing.Color.Red;
            txtPaperNumber.Focus();
            return;
        }

        //if (txtIssueDate.Text.Trim() == string.Empty)
        //{
        //    lblError.Text = Resources.labels.IssueDate + Resources.labels.IsNotNull;
        //    txtIssueDate.BorderColor = System.Drawing.Color.Red;
        //    txtIssueDate.Focus();
        //    return;
        //}

        //if (!Utility.CheckSpecialCharacters(txtIssueDate.Text))
        //{
        //    lblError.Text = Resources.labels.IssueDate + Resources.labels.ErrorSpeacialCharacters;
        //    txtIssueDate.BorderColor = System.Drawing.Color.Red;
        //    txtIssueDate.Focus();
        //    return;
        //}

        if (PnPassport.Visible == true)
        {
            if (txtPassPortNo.Text.Trim() == string.Empty)
            {
                lblError.Text = Resources.labels.PassportNumber + Resources.labels.IsNotNull;
                txtPassPortNo.BorderColor = System.Drawing.Color.Red;
                txtPassPortNo.Focus();
                return;
            }

            if (!Utility.CheckSpecialCharacters(txtPassPortNo.Text))
            {
                lblError.Text = Resources.labels.PassportNumber + Resources.labels.ErrorSpeacialCharacters;
                txtPassPortNo.BorderColor = System.Drawing.Color.Red;
                txtPassPortNo.Focus();
                return;
            }

            //if (txtDatetimePassport.Text.Trim() == string.Empty)
            //{
            //    lblError.Text = Resources.labels.Datetimepassport + Resources.labels.IsNotNull;
            //    txtDatetimePassport.BorderColor = System.Drawing.Color.Red;
            //    txtDatetimePassport.Focus();
            //    return;
            //}

            //if (!Utility.CheckSpecialCharacters(txtDatetimePassport.Text))
            //{
            //    lblError.Text = Resources.labels.Datetimepassport + Resources.labels.ErrorSpeacialCharacters;
            //    txtDatetimePassport.BorderColor = System.Drawing.Color.Red;
            //    txtDatetimePassport.Focus();
            //    return;
            //}

        }
        if (pnNewNRIC.Visible == true)
        {

            if (txtPaperNumberImport.Text.Trim() == string.Empty)
            {
                lblError.Text = Resources.labels.PaperNumber + Resources.labels.IsNotNull;
                txtPaperNumberImport.BorderColor = System.Drawing.Color.Red;
                txtPaperNumberImport.Focus();
                return;
            }

            if (!Utility.CheckSpecialCharacters(txtPaperNumberImport.Text))
            {
                lblError.Text = Resources.labels.PaperNumber + Resources.labels.ErrorSpeacialCharacters;
                txtPaperNumberImport.BorderColor = System.Drawing.Color.Red;
                txtPaperNumberImport.Focus();
                return;
            }

            //if (txtIssueDateImport.Text.Trim() == string.Empty)
            //{
            //    lblError.Text = Resources.labels.IssueDate + Resources.labels.IsNotNull;
            //    txtIssueDateImport.BorderColor = System.Drawing.Color.Red;
            //    txtIssueDateImport.Focus();
            //    return;
            //}

            //if (!Utility.CheckSpecialCharacters(txtIssueDateImport.Text))
            //{
            //    lblError.Text = Resources.labels.IssueDate + Resources.labels.ErrorSpeacialCharacters;
            //    txtIssueDateImport.BorderColor = System.Drawing.Color.Red;
            //    txtIssueDateImport.Focus();
            //    return;
            //}

        }

        if (pnLicense.Visible == true)
        {
            if (txtLicenseNumber.Text.Trim() == string.Empty)
            {
                lblError.Text = Resources.labels.licensenonotnull;
                txtLicenseNumber.BorderColor = System.Drawing.Color.Red;
                txtLicenseNumber.Focus();
                return;
            }

            if (!Utility.CheckSpecialCharacters(txtLicenseNumber.Text))
            {
                lblError.Text = Resources.labels.license + Resources.labels.ErrorSpeacialCharacters;
                txtLicenseNumber.BorderColor = System.Drawing.Color.Red;
                txtLicenseNumber.Focus();
                return;
            }

        }

        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                try
                {
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    string RequestID = string.Empty;
                    DateTime IssueDateAdd = new DateTime();
                    DateTime ExpiryDateAdd = new DateTime();

                    #region validatiton

                    if (listDocumentModel.Count == 0)
                    {
                        lblError.Text = Resources.labels.document + Resources.labels.IsNotNull;
                        return;
                    }

                    if (txtIssueDate.Text != string.Empty)
                    {
                        IssueDateAdd = DateTime.ParseExact(txtIssueDate.Text.ToString(), "dd/MM/yyyy", null);
                    }

                    if (txtExpireDate.Text != string.Empty)
                    {
                        ExpiryDateAdd = DateTime.ParseExact(txtExpireDate.Text.ToString(), "dd/MM/yyyy", null);
                    }

                    if (txtExpireDate.Text.Trim() != string.Empty)
                    {
                        if (IssueDateAdd >= ExpiryDateAdd)
                        {
                            lblError.Text = string.Format(" {0} must be greater than {1}", Resources.labels.ExpiryDate, Resources.labels.IssueDate);
                            txtExpireDate.BorderColor = System.Drawing.Color.Red;
                            txtIssueDate.BorderColor = System.Drawing.Color.Red;
                            txtExpireDate.Focus();
                            return;
                        }
                    }

                    if (!checkDocumentType())
                    {
                        lblError.Text = Resources.labels.TheDocumentTypeHasAnError;
                        return;
                    }
                    DataSet ds0 = new DataSet();
                    object[] _object0 = new object[] { hdfUserID.Value, ddlKycLevel.SelectedValue.Trim(), txtPaperNumber.Text.Trim() };
                    ds0 = _service.common("SEMS_CHECK_PAPER_NO", _object0, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }

                    #endregion

                    if (listDocumentModel.Count == 0)
                    {
                        lblError.Text = Resources.labels.document + Resources.labels.IsNotNull;
                        return;
                    }

                    string str_IssueDateAdd, str_ExpiryDateAdd;
                    str_IssueDateAdd = txtIssueDate.Text.Trim() == string.Empty ? string.Empty : IssueDateAdd.ToString();
                    str_ExpiryDateAdd = txtExpireDate.Text.Trim() == string.Empty ? string.Empty : ExpiryDateAdd.ToString();
                    foreach (var item in listDocumentModel)
                    {
                        DataSet ds = new DataSet();
                        Dictionary<object, object> _para = new Dictionary<object, object>();
                        _para.Add("REQUESTID", RequestID);
                        _para.Add("USERID", hdfUserID.Value);
                        _para.Add("DOCUMENTID", item.DocumentID);
                        _para.Add("DOCUMENTNAME", item.DocumentName);
                        _para.Add("DOCUMENTTYPE", item.DocumentType);
                        _para.Add("FILE", item.File);
                        _para.Add("USERCREATE", HttpContext.Current.Session["userID"].ToString());

                        ds = _service.CallStore("SEMS_CON_KYC_INSERT", _para, "Create KYC Consumer Request", "N", ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.success;
                            RequestID = ds.Tables[0].Rows[0]["RequestID"].ToString();
                            lbStatusKYC.Visible = true;
                            ddlStatus.Visible = true;
                            ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["status_request"].ToString();
                            ddlStatus.Enabled = false;
                            create.Visible = true;
                            txtCreateby.Text = ds.Tables[0].Rows[0]["Usercreate"].ToString();
                            txtcreatedate.Text = ds.Tables[0].Rows[0]["DateCreate"].ToString();
                            //ddlStatus.Visible = true;
                            //txtPhoneNumber.Enabled = false;
                            ////ddlPaperType.Enabled = false;
                            //ddlKycLevel.Enabled = false;
                            //txtPaperNumber.Enabled = false;
                            //txtIssueDate.Enabled = false;
                            //txtExpireDate.Enabled = false;
                            //pnDocument.Enabled = false;
                            //documentUpload.Enabled = false;
                            //btnSave.Enabled = false;

                            rptData.DataSource = listDocumentModel;
                            rptData.DataBind();
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
                        }
                    }
                    DataSet ds1 = new DataSet();
                    object[] _object1 = new object[] { RequestID, hdfUserID.Value, ddlKycLevel.SelectedValue, txtPaperNumber.Text.Trim(), str_IssueDateAdd, str_ExpiryDateAdd,
                            string.Empty, string.Empty, string.Empty, string.Empty, HttpContext.Current.Session["userID"].ToString() };
                    ds1 = _service.common("SEMS_CON_KYC_UPDATE", _object1, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.thanhcong;
                        ddlStatus.Visible = true;
                        txtPhoneNumber.Enabled = false;
                        //ddlPaperType.Enabled = false;
                        ddlKycLevel.Enabled = false;
                        txtPaperNumber.Enabled = false;
                        txtIssueDate.Enabled = false;
                        txtExpireDate.Enabled = false;
                        pnDocument.Enabled = false;
                        documentUpload.Enabled = false;
                        btnSave.Enabled = false;

                        rptData.DataSource = listDocumentModel;
                        rptData.DataBind();
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                }
                catch (Exception ex)
                {

                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
                }
                break;
            case IPC.ACTIONPAGE.EDIT:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                try
                {
                    DateTime ExpiryDate = new DateTime();
                    DateTime IssueDate = new DateTime();
                    #region validatiton

                    if (listDocumentModel.Count == 0)
                    {
                        lblError.Text = Resources.labels.document + Resources.labels.IsNotNull;
                        return;
                    }

                    if (txtIssueDate.Text != string.Empty)
                    {
                        IssueDate = DateTime.ParseExact(txtIssueDate.Text.ToString(), "dd/MM/yyyy", null);
                    }

                    if (txtExpireDate.Text != string.Empty)
                    {
                        ExpiryDate = DateTime.ParseExact(txtExpireDate.Text.ToString(), "dd/MM/yyyy", null);
                    }

                    if (txtExpireDate.Text.Trim() != string.Empty)
                    {
                        if (IssueDate >= ExpiryDate)
                        {
                            lblError.Text = string.Format(" {0} must be greater than {1}", Resources.labels.ExpiryDate, Resources.labels.IssueDate);
                            txtExpireDate.BorderColor = System.Drawing.Color.Red;
                            txtIssueDate.BorderColor = System.Drawing.Color.Red;
                            txtExpireDate.Focus();
                            return;
                        }
                    }
                    
                    if (!checkDocumentType())
                    {
                        lblError.Text = Resources.labels.TheDocumentTypeHasAnError;
                        return;
                    }
                    #endregion
                    DataSet ds = new DataSet();
                    object[] _object;
                    string str_IssueDate, str_ExpiryDate;
                    str_IssueDate = txtIssueDate.Text.Trim() == string.Empty ? string.Empty : IssueDate.ToString();
                    str_ExpiryDate = txtExpireDate.Text.Trim() == string.Empty ? string.Empty : ExpiryDate.ToString();
                    foreach (var item in listDocumentModel)
                    {
                        if (item.IsUpdate || item.IsNew)
                        {
                            if (item.IsNew)
                            {
                                _object = new object[] { ViewState["REQUESTNO"].ToString(), hdfUserID.Value, ddlKycLevel.SelectedValue, txtPaperNumber.Text.Trim(), str_IssueDate, str_ExpiryDate,
                            string.Empty, item.DocumentName, item.DocumentType, item.File, HttpContext.Current.Session["userID"].ToString() };
                            }
                            else
                            {
                                _object = new object[] { ViewState["REQUESTNO"].ToString(), hdfUserID.Value, ddlKycLevel.SelectedValue, txtPaperNumber.Text.Trim(), str_IssueDate, str_ExpiryDate,
                            item.DocumentID, item.DocumentName, item.DocumentType, item.File, HttpContext.Current.Session["userID"].ToString() };
                            }

                            ds = _service.common("SEMS_CON_KYC_UPDATE", _object, ref IPCERRORCODE, ref IPCERRORDESC);
                            if (IPCERRORCODE == "0")
                            {
                                lblError.Text = Resources.labels.thanhcong;
                                txtIssueDate.Enabled = false;
                                txtExpireDate.Enabled = false;
                                txtBirthday.Enabled = false;
                                ddlGender.Enabled = false;
                                ddlNationality.Enabled = false;
                                txtAddress.Enabled = false;
                                ddlKycLevel.Enabled = false;
                                btnSave.Enabled = false;
                                documentUpload.Enabled = false;
                            }
                            else
                            {
                                lblError.Text = IPCERRORDESC;
                                return;
                            }
                        }

                    }
                    _object = new object[] { ViewState["REQUESTNO"].ToString(), hdfUserID.Value, ddlKycLevel.SelectedValue, txtPaperNumber.Text.Trim(), str_IssueDate, str_ExpiryDate,
                            string.Empty, string.Empty, string.Empty, string.Empty, HttpContext.Current.Session["userID"].ToString() };
                    ds = _service.common("SEMS_CON_KYC_UPDATE", _object, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.thanhcong;
                        btnSave.Enabled = false;
                        txtPaperNumber.Enabled = false;
                        txtIssueDate.Enabled = false;
                        txtExpireDate.Enabled = false;
                        txtBirthday.Enabled = false;
                        ddlGender.Enabled = false;
                        ddlNationality.Enabled = false;
                        txtAddress.Enabled = false;
                        ddlKycLevel.Enabled = false;
                        btnSave.Enabled = false;
                        documentUpload.Enabled = false;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
                }
                break;
        }
    }


    protected void btnClear_click(object sender, EventArgs e)
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    setDefaultColor();
                    setDefauleControl();
                    setEmptymControls();
                    loadCombobox();

                    txtIssueDate.Enabled = true;
                    txtExpireDate.Enabled = true;
                    txtPaperNumber.Enabled = true;
                    create.Visible = false;
                    txtPhoneNumber.Enabled = true;
                    pnDocument.Enabled = true;
                    btnImport.Enabled = true;
                    documentUpload.Enabled = true;
                    ddlKycLevel.Enabled = true;

                    listDocumentModel.Clear();
                    rptData.DataSource = listDocumentModel;
                    rptData.DataBind();
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    setDefaultColor();
                    setDefauleControl();
                    setEmptymControls();
                    txtIssueDate.Enabled = true;
                    txtExpireDate.Enabled = true;
                    txtPaperNumber.Enabled = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
        
    }

    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (ACTION == IPC.ACTIONPAGE.EDIT || ACTION == IPC.ACTIONPAGE.APPROVE)
                {
                    if (ViewState["APPROVE_STATUS"].ToString() != "P")
                    {
                        LinkButton lbtnDelete;
                        lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
                        lbtnDelete.Enabled = false;
                        lbtnDelete.CssClass = "btn btn-secondary";
                        lbtnDelete.OnClientClick = null;

                        fileUpdate.Enabled = false;
                        pannelModal.Enabled = false;
                    }
                }
                if (pnDocument.Enabled == false)
                {
                    LinkButton lbtnDelete;
                    lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
                    lbtnDelete.Enabled = false;
                    lbtnDelete.CssClass = "btn btn-secondary";
                    lbtnDelete.OnClientClick = null;
                }
                
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void loadKYCLevel(object sender, EventArgs e)
    {
        loadPannelFromKYCLevel();
    }

    protected void DocumentTypeImport_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            txtDocumentNameImport.Text = ddlDocumentTypeImport.SelectedItem.Text.Trim();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    void VisibleLicense()
    {
        pnNewNRIC.Visible = false;
        pnLicense.Visible = true;
        PnPassport.Visible = false;
    }

    void VisiblePassport()
    {
        pnNewNRIC.Visible = false;
        pnLicense.Visible = false;
        PnPassport.Visible = true;
    }

    void VisibleNRIC()
    {
        pnNewNRIC.Visible = true;
        pnLicense.Visible = false;
        PnPassport.Visible = false;
    }

    void loadPannelFromKYCLevel()
    {
        switch (ddlKycLevel.SelectedValue.Trim().ToUpper())
        {
            case "PASSPORT":
                VisiblePassport();
                break;
            case "LICENSE":
                VisibleLicense();
                break;
            default:
                VisibleNRIC();
                break;
        }
    }

    protected void rptData_ItemCommand1(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string commandName = e.CommandName;
            string commandArg = e.CommandArgument.ToString();
            switch (commandName)
            {
                case IPC.ACTIONPAGE.EDIT:
                    foreach (DocumentModel item in listDocumentModel)
                    {
                        if (item.No.ToString() == commandArg)
                        {
                            loadCombobox_KYCDocumentName_Repeater(item.DocumentType);
                            ddlDocumentType.SelectedValue = item.DocumentType;
                            txtDocumentName.Text = item.DocumentName;
                            Image1.Attributes.Add("src", FORMAT_IMAGE + item.File);
                            ViewState["No"] = item.No.ToString();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + Image.ClientID + "');openModal('" + Image.ClientID + "');", true);
                            return;
                        }
                    }
                    break;
                case IPC.ACTIONPAGE.DELETE:
                    if (listDocumentModel.Count > 0)
                    {
                        foreach (var item in listDocumentModel)
                        {
                            // Trường hợp Del file vừa import
                            string[] arrListStr = commandArg.Split('|');
                            lblError.Text = string.Empty;
                            if (item.No.ToString() == arrListStr[0])
                            {
                                if (item.IsNew)
                                {
                                    listDocumentModel.Remove(item);
                                    resetNoDocument();
                                    rptData.DataSource = listDocumentModel;
                                    rptData.DataBind();
                                    lblError.Text = Resources.labels.deletesuccessfully;
                                    return;
                                }
                                else // Trường hợp Del file trong database
                                {
                                    deleteDocument(arrListStr[1]);
                                    if(IPCERRORCODE.ToString().Equals("0"))
                                    {
                                        listDocumentModel.Remove(item);
                                        resetNoDocument();
                                        rptData.DataSource = listDocumentModel;
                                        rptData.DataBind();
                                        lblError.Text = Resources.labels.deletesuccessfully;
                                    }   
                                    return;
                                }
                            }

                        }
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}