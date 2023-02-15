using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Constant;
using System.Collections.Generic;
using System.Collections;
using SmartPortal.Model;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.UI;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSKYCMerchantProfile_Register_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    //IList<DocumentModel> listDocument = new List<DocumentModel>();
    string Contract_no = string.Empty;
    string Custid = string.Empty;
    string Userid = string.Empty;
    string Productid = string.Empty;
    string ContractLevel = string.Empty;
    string KYCID = string.Empty;
    string request_id = string.Empty;

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
            //List<DocumentModel> dsnew = (List<DocumentModel>)ViewState["listDocument"];
            lblError.Text = string.Empty;
            lbTitle.Text = Resources.labels.MerchantProfile + " - " + Resources.labels.Registration;
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
        try
        {
            Hashtable ht = new Hashtable();
            Dictionary<object, object> ob = new Dictionary<object, object>();
            ob.Add("ACCTNO", txtMoneySourceNumber.Text);
            ht = _service.CallCore("SEMS_GETINFOBANKACC", ob, "Get info by source number", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (ht != null)
            {
                if (ht["ACCOUNTNO"] != null)
                {
                    lbCurrency.Text = ht["CURRENCYID"].ToString();
                    txtEffectiveDate.Text = ht["OPENDATE"].ToString();
                }
            }
            else
            {
                lblError.Text = Resources.labels.MoneySourceNumber + " incorrect!";
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
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

    private void loadData_Reepeater()
    {

        //if ((DataTable)ViewState["tbDoc"] != null)
        //{
        //    DataTable datatable = (DataTable)ViewState["tbDoc"];
        //    rptData.DataSource = datatable;
        //    rptData.DataBind();
        //}
        //else
        //{         
        rptData.DataSource = listDocumentModel;
        rptData.DataBind();
        //}

    }
    void BindData()
    {
        try
        {
            loadCombobox();
            loadData_Reepeater();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnImport_click(object sender, EventArgs e)
    {
        try
        {
            DocumentModel doc = new DocumentModel();
            if (documentUpload.HasFile)
            {
                lblError.Text = string.Empty;
                String fileExtension = System.IO.Path.GetExtension(documentUpload.FileName).ToLower();
                string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP", ".PDF", ".WEBP" };
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
                    lblError.Text = Resources.labels.Extensions + " " + fileExtension + Resources.labels.NotSupport;
                    return;
                }
                System.IO.Stream fs = documentUpload.PostedFile.InputStream;
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                DocumentModel item = new DocumentModel();
                item.No = listDocumentModel.Count + 1;
                item.DocumentName = documentUpload.FileName;
                item.File = "data:image/png;base64," + base64String;
                listDocumentModel.Add(item);
                loadData_Reepeater();
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
        txtAddress.BorderColor = System.Drawing.Color.Empty;
        txtEmail.BorderColor = System.Drawing.Color.Empty;
        txtBankName.SetDefault();
    }
    protected void btnClear_click(object sender, EventArgs e)
    {
        setDefaultColor();
        txtPhoneNumber.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtBankName.Text = string.Empty;
        txtBankName.Value = string.Empty;
        txtEffectiveDate.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtExpiryDate.Text = string.Empty;
        txtExpiryDate2.Text = string.Empty;
        txtIssueDate.Text = string.Empty;
        txtMerchantCode.Text = string.Empty;
        txtMerchantName.Text = string.Empty;
        txtPaperNumber.Text = string.Empty;
        txtMoneySourceNumber.Text = string.Empty;
        listDocumentModel.Clear();
        loadData_Reepeater();
        loadCombobox();
    }

    private bool Validation()
    {

        if (!Utility.CheckSpecialCharacters(txtMerchantName.Text.Trim()))
        {
            lblError.Text = Resources.labels.MerchantName + Resources.labels.ErrorSpeacialCharacters;
            txtMerchantName.BorderColor = System.Drawing.Color.Red;
            txtMerchantName.Focus();
            return false;
        }

        if (txtMerchantName.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.MerchantName + Resources.labels.IsNotNull;
            txtMerchantName.BorderColor = System.Drawing.Color.Red;
            txtMerchantName.Focus();
            return false;
        }

        if (!Utility.CheckSpecialCharacters(txtPaperNumber.Text.Trim()))
        {
            lblError.Text = Resources.labels.PaperNumber + Resources.labels.ErrorSpeacialCharacters;
            txtPaperNumber.BorderColor = System.Drawing.Color.Red;
            txtPaperNumber.Focus();
            return false;
        }

        if (txtPaperNumber.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.PaperNumber + Resources.labels.IsNotNull;
            txtPaperNumber.BorderColor = System.Drawing.Color.Red;
            txtPaperNumber.Focus();
            return false;
        }

        if (!Utility.CheckSpecialCharacters(txtIssueDate.Text.Trim()))
        {
            lblError.Text = Resources.labels.IssueDate + Resources.labels.ErrorSpeacialCharacters;
            txtIssueDate.BorderColor = System.Drawing.Color.Red;
            txtIssueDate.Focus();
            return false;
        }

        if (!Utility.CheckSpecialCharacters(txtExpiryDate.Text.Trim()))
        {
            lblError.Text = Resources.labels.ExpiryDate + Resources.labels.ErrorSpeacialCharacters;
            txtExpiryDate.BorderColor = System.Drawing.Color.Red;
            txtExpiryDate.Focus();
            return false;
        }

        if (txtIssueDate.Text.Trim() != string.Empty && txtExpiryDate.Text.Trim() != string.Empty)
        {
            DateTime IssueDate = DateTime.ParseExact(txtIssueDate.Text.Trim(), "dd/MM/yyyy", null);
            DateTime ExpiryDate = DateTime.ParseExact(txtExpiryDate.Text.Trim(), "dd/MM/yyyy", null);
            if (IssueDate >= ExpiryDate)
            {
                lblError.Text = string.Format(" {0} must be greater than {1}", Resources.labels.ExpiryDate, Resources.labels.IssueDate);
                txtExpiryDate.BorderColor = System.Drawing.Color.Red;
                txtIssueDate.BorderColor = System.Drawing.Color.Red;
                txtExpiryDate.Focus();
                return false;
            }
        }
        //if (txtIssueDate.Text.Equals(string.Empty))
        //{
        //    lblError.Text = Resources.labels.IssueDate + " is not null";
        //    txtIssueDate.BorderColor = System.Drawing.Color.Red;
        //    txtIssueDate.Focus();
        //    return false;
        //}
        //if (txtExpiryDate.Text.Equals(string.Empty))
        //{
        //    lblError.Text = Resources.labels.ExpiryDate + " is not null";
        //    txtExpiryDate.BorderColor = System.Drawing.Color.Red;
        //    txtExpiryDate.Focus();
        //    return false;
        //}

        if (!Utility.CheckSpecialCharacters(txtAddress.Text.Trim()))
        {
            lblError.Text = Resources.labels.address + Resources.labels.ErrorSpeacialCharacters;
            txtAddress.BorderColor = System.Drawing.Color.Red;
            txtAddress.Focus();
            return false;
        }

        if (txtAddress.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.address + " is not null";
            txtAddress.BorderColor = System.Drawing.Color.Red;
            txtAddress.Focus();
            return false;
        }

        if (!Utility.CheckSpecialCharacters(txtEmail.Text.Trim()))
        {
            lblError.Text = Resources.labels.email + Resources.labels.ErrorSpeacialCharacters;
            txtEmail.BorderColor = System.Drawing.Color.Red;
            txtEmail.Focus();
            return false;
        }

        if (txtEmail.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.email + " is not null";
            txtEmail.BorderColor = System.Drawing.Color.Red;
            txtEmail.Focus();
            return false;
        }

        if (!Utility.CheckSpecialCharacters(txtBankName.Text.Trim()))
        {
            lblError.Text = Resources.labels.BankName + Resources.labels.ErrorSpeacialCharacters;
            txtBankName.SetError();
            return false;
        }

        if (txtBankName.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.BankName + " is not null";
            txtBankName.SetError();
            return false;
        }

        if (!Utility.CheckSpecialCharacters(txtMoneySourceNumber.Text.Trim()))
        {
            lblError.Text = Resources.labels.MoneySourceNumber + Resources.labels.ErrorSpeacialCharacters;
            txtMoneySourceNumber.BorderColor = System.Drawing.Color.Red;
            txtMoneySourceNumber.Focus();
            return false;
        }

        if (txtMoneySourceNumber.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.MoneySourceNumber + " is not null";
            txtMoneySourceNumber.BorderColor = System.Drawing.Color.Red;
            txtMoneySourceNumber.Focus();
            return false;
        }
        return true;
    }

    public static string convertToUnSign(string s)
    {
        Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
        string temp = s.Normalize(NormalizationForm.FormD);
        return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
    }

    void loadPrimerykey()
    {
        DataSet ds = new DataSet();
        Dictionary<object,object> searchObject = new Dictionary<object, object>();
        searchObject.Add("PHONENO", txtPhoneNumber.Text);
        searchObject.Add("NAME", convertToUnSign(txtMerchantName.Text).Replace(" ",""));
        ds = _service.CallStore("SEMS_REGISTERWL", searchObject, "Merchant Profile - Register", "N", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Contract_no = ds.Tables[0].Rows[0]["CONTRACTNO"].ToString();
                Custid = ds.Tables[0].Rows[0]["CUSTID"].ToString();
                Userid = ds.Tables[0].Rows[0]["USERID"].ToString();
                Productid = ds.Tables[0].Rows[0]["PRODUCTID"].ToString();
                ContractLevel = ds.Tables[0].Rows[0]["CONTRACTLEVEL"].ToString();
                KYCID = ds.Tables[0].Rows[0]["KYCID"].ToString();
            }
        }
    }

    void SetParaRegister(Dictionary<object,object> para)
    {
        DateTime IssueDate = DateTime.ParseExact(txtIssueDate.Text.ToString(), "dd/MM/yyyy", null);
        DateTime ExpiryDate = DateTime.ParseExact(txtExpiryDate.Text.ToString(), "dd/MM/yyyy", null);
        int branchid = int.Parse(HttpContext.Current.Session["Branch"].ToString());
        int contractlevel = int.Parse(ddWalletLevel.SelectedValue);

        para.Add("CONTRACT_NO", Contract_no);
        para.Add("CUSTID", Custid);
        para.Add("USERID", Userid);
        para.Add("MERCHANT_CODE", Userid);
        para.Add("PHONE_NUMBER", txtPhoneNumber.Text);
        para.Add("MERCHANT_NAME", txtMerchantName.Text);
        para.Add("PAPER_TYPE", ddPaperType.SelectedValue);
        para.Add("PAPER_NO", txtPaperNumber.Text);
        para.Add("ISSUE_DATE", IssueDate);
        para.Add("EXPIRY_DATE", ExpiryDate);
        para.Add("NATIONALITY", ddNationality.SelectedValue);
        para.Add("ADDRESS", txtAddress.Text);
        para.Add("EMAIL", txtEmail.Text);
        para.Add("KYC_ID", KYCID);
        para.Add("PRODUCTID", Productid);
        para.Add("WALLETLEVEL", contractlevel);
        para.Add("CCYID", lbCurrency.Text);
        para.Add("MONEY_SOURCE_TYPE", ddMoneySourceType.SelectedValue);
        para.Add("BANK_ID", txtBankName.Value);
        para.Add("BRANCH_ID", branchid);
        para.Add("MONEY_SOURCE_NUMBER", txtMoneySourceNumber.Text);
        para.Add("EFFECTIVE_DATE", txtEffectiveDate.Text);
        para.Add("EXPIRY_DATE2", txtExpiryDate2.Text);
        para.Add("USER_CREATE", HttpContext.Current.Session["userID"].ToString());
    }
    protected void btnSubmit_click(object sender, EventArgs e)
    {
        try
        {
            setDefaultColor();
            loadPrimerykey();
            if (!Validation()) return;
            DataSet ds = new DataSet();
            Dictionary<object, object> searchObject = new Dictionary<object, object>();
            SetParaRegister(searchObject);
             ds = _service.CallStore("SEMS_MERCHANT_REGIS", searchObject, "Merchant Register","N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                string useridnew = ds.Tables[0].Rows[0]["MERCHANT_CODE"].ToString();
                string wallet_idnew = ds.Tables[0].Rows[0]["WALLET_ID"].ToString();
                string Custid_idnew = ds.Tables[0].Rows[0]["CUSTID"].ToString();
                txtMerchantCode.Text = useridnew;
                if (!useridnew.Equals(string.Empty))
                {
                    foreach (var item in listDocumentModel)
                    {
                        object[] insertDoc = new object[] { request_id, useridnew, wallet_idnew, Custid_idnew, ddPaperType.SelectedValue, txtPaperNumber.Text, item.DocumentName, item.File, HttpContext.Current.Session["userID"].ToString()};

                        ds = _service.common("SEMS_INS_KYC_REQUEST", insertDoc, ref IPCERRORCODE, ref IPCERRORDESC);
                        request_id = ds.Tables[0].Rows[0]["REQUEST_ID"].ToString();
                    }
                }
                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.thanhcong;
                    btnSubmit.Enabled = false;
                }
                else
                {
                    lblError.Text = "Import file unsuccess - " + IPCERRORDESC;
                }
            }
            else lblError.Text = IPCERRORDESC;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            txtDocumentName.BorderColor = System.Drawing.Color.Empty;
            if (txtDocumentName.Text.Equals(string.Empty))
            {
                txtDocumentName.BorderColor = System.Drawing.Color.Red;
                txtDocumentName.Focus();
                lblErrorPopup.InnerText = Resources.labels.DocumentName + " is not null";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + Image.ClientID + "');openModal('" + Image.ClientID + "');", true);
            }

            if (!Utility.CheckSpecialCharacters(txtDocumentName.Text.Trim()))
            {
                lblError.Text = Resources.labels.DocumentName + Resources.labels.ErrorSpeacialCharacters;
                txtDocumentName.BorderColor = System.Drawing.Color.Red;
                txtDocumentName.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + Image.ClientID + "');openModal('" + Image.ClientID + "');", true);
            }

            else 
            {
                foreach (DocumentModel item in listDocumentModel)
                {
                    if (item.No.ToString() == ViewState["No"].ToString())
                    {
                        item.DocumentName = txtDocumentName.Text;
                        break;
                    }
                }
                loadData_Reepeater();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + Image.ClientID + "');", true);
            }
        }
        catch (Exception ex) { }
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                        txtDocumentName.Text = item.DocumentName;
                        ImageView.Attributes.Add("src", item.File);
                        ViewState["No"] = item.No.ToString();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + Image.ClientID + "');openModal('" + Image.ClientID + "');", true);
                        return;
                    }
                }
                break;
            case IPC.ACTIONPAGE.DETAILS:

                foreach (DocumentModel item in listDocumentModel)
                {
                    //if (item.No.ToString() == commandArg)
                    //{
                    //    PreviewImage.ViewImage(item.File);
                    //}
                }
                break;
                case IPC.ACTIONPAGE.DELETE:
                foreach (DocumentModel item in listDocumentModel)
                {
                    if (item.No.ToString() == commandArg)
                    {
                        listDocumentModel.Remove(item);
                        resetNoDocument();
                        loadData_Reepeater();
                        return;
                    }
                }
                break;
            }
    }
}