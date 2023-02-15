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
using System.Collections;
using Newtonsoft.Json;
using SmartPortal.SEMS;
using System.Linq;

public partial class Widgets_KYCConsumer_Controls_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string FORMAT_DATE = "dd/MM/yyyy";
    string ACTION = "";
    string FORMAT_IMAGE = "data:image/jpg;base64,";
    public bool enabledDel = true;
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
            lblError.Text = string.Empty;
            ACTION = GetActionPage();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    lblTitleBranch.Text = Resources.labels.kycConsumer + " - " + Resources.labels.add;
                    break;
                default:
                    ViewState["REQUESTNO"] = GetParamsPage(IPC.ID)[0].Trim();
                    switch (ACTION)
                    {
                        case IPC.ACTIONPAGE.EDIT:
                            lblTitleBranch.Text = Resources.labels.kycConsumer + " - " + Resources.labels.edit;
                            break;
                        case IPC.ACTIONPAGE.DETAILS:
                            lblTitleBranch.Text = Resources.labels.kycConsumer + " - " + Resources.labels.view;
                            break;
                        default:
                            lblTitleBranch.Text = "Approve or Reject KYC Consumer";
                            break;

                    }
                    break;
            }
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

    private void LoadDefaultControl()
    {
        loadCombobox();
        // pnAgreement.Visible = true;
        pnBusiness.Visible = false;
        if (ACTION == IPC.ACTIONPAGE.EDIT)
        {
            pnCustCode.Visible = false;
            pnCustInfo.Enabled = false;
            pnImportNewDocument.Enabled = true;
            pnDocumentBusiness.Enabled = true;
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
                        ddlKycType.DataSource = ds;
                        ddlKycType.DataValueField = "KycCode";
                        ddlKycType.DataTextField = "KycName";
                        ddlKycType.DataBind();

                    }
                }
                Cache.Insert("Wallet_KYCLevel", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlKycType.DataSource = (DataSet)Cache["Wallet_KYCLevel"];
                ddlKycType.DataValueField = "KycCode";
                ddlKycType.DataTextField = "KycName";
                ddlKycType.DataBind();
            }
            ddlKycType.SelectedValue = "NRIC";
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
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
                        ddStatus.DataSource = ds;
                        ddStatus.DataValueField = "ValueID";
                        ddStatus.DataTextField = "Caption";
                        ddStatus.DataBind();
                    }
                }
                Cache.Insert("Wallet_STT_KYCRequest", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddStatus.DataSource = (DataSet)Cache["Wallet_STT_KYCRequest"];
                ddStatus.DataValueField = "ValueID";
                ddStatus.DataTextField = "Caption";
                ddStatus.DataBind();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
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
                ViewState["listDocumentName"] = ds.Tables[0];

                ddlDocumentTypeImport.DataSource = ds;
                ddlDocumentTypeImport.DataValueField = "ValueID";
                ddlDocumentTypeImport.DataTextField = "Caption";
                ddlDocumentTypeImport.DataBind();

                ListItem removeItemPP = ddlDocumentTypeImport.Items.FindByValue("PP");
                ListItem removeItemAC = ddlDocumentTypeImport.Items.FindByValue("AC");
                ListItem removeItemBD = ddlDocumentTypeImport.Items.FindByValue("BD");
                ListItem removeItemLC = ddlDocumentTypeImport.Items.FindByValue("LC");
                ddlDocumentTypeImport.Items.Remove(removeItemPP);
                ddlDocumentTypeImport.Items.Remove(removeItemAC);
                ddlDocumentTypeImport.Items.Remove(removeItemBD);
                ddlDocumentTypeImport.Items.Remove(removeItemLC);
            }
            else
            {
                ViewState["listDocumentName"] = ds.Tables[0];

                ddlDocumentTypeImport.DataSource = ds;
                ddlDocumentTypeImport.DataValueField = "ValueID";
                ddlDocumentTypeImport.DataTextField = "Caption";
                ddlDocumentTypeImport.DataBind();

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
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    private void loadCombobox_KYCDocumentName_Repeater()
    {
        // Save list STT KYC Request Cache
        try
        {
            DataSet ds = new DataSet();
            Cache.Remove("Wallet_KYCDocumentName");
            ds = (DataSet)Cache["Wallet_KYCDocumentName"];
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
                    }
                }
                Cache.Insert("Wallet_KYCDocumentName", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlDocumentType.DataSource = ds;
                ddlDocumentType.DataValueField = "ValueID";
                ddlDocumentType.DataTextField = "Caption";
                ddlDocumentType.DataBind();
            }
            if (!IsPostBack)
            {
                ViewState["listDocumentName"] = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    private void loadCombobox()
    {
        loadCombobox_KYCLevel();
        loadCombobox_Nation();
        loadCombobox_WalletLevel();
        loadCombobox_Status();
        loadCombobox_KYCDocumentName();
        loadCombobox_Gender();
    }

    private void loadData()
    {
        DataSet ds = new DataSet();
        object[] _object = new object[] { ViewState["REQUESTNO"].ToString() };
        ds = _service.common("SEMS_GET_INFOKYC_CON", _object, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtPhoneNumber.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();
                txtConsumerName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                txtPaperNumber.Text = txtPaperNumberImport.Text = ds.Tables[0].Rows[0]["PaperNO"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["ADDRRESIDENT"].ToString();
                ddlKycType.SelectedValue = ds.Tables[0].Rows[0]["KYC_CODE"].ToString().Trim();
                ddWalletLevel.SelectedValue = ds.Tables[0].Rows[0]["WALLET_LEVEL"].ToString().Trim();
                ddStatus.SelectedValue = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString().Trim();
                ddNationality.SelectedValue = ds.Tables[0].Rows[0]["NATION"].ToString().Trim();
                txtIssueDate.Text = ds.Tables[0].Rows[0]["ISSUEDATE"].ToString();
                txtExpiryDate.Text = ds.Tables[0].Rows[0]["EXPIRYDATE"].ToString();
                hdfUserID.Value = ds.Tables[0].Rows[0]["USERID"].ToString();
                txtBirthday.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                txtCustCode.Text = txtCustCodeInfo.Text = ds.Tables[0].Rows[0]["CUSTCODE"].ToString();

                if (ACTION.Equals(IPC.ACTIONPAGE.APPROVE) && ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString().Trim().Equals("P"))
                {
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                }
            }
            loadData_Repeater(ds);
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
                    item.No = int.Parse(ds.Tables[0].Rows[i]["No"].ToString());
                    item.DocumentID = int.Parse(ds.Tables[0].Rows[i]["DocumentID"].ToString());
                    item.DocumentName = ds.Tables[0].Rows[i]["DocumentName"].ToString();
                    item.DocumentType = ds.Tables[0].Rows[i]["DocumentType"].ToString();
                    item.File = ds.Tables[0].Rows[i]["FILE"].ToString();
                    item.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                    listDocumentModel.Add(item);
                }
                rptData.DataSource = listDocumentModel;
                rptData.DataBind();
            }
        }
    }

    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    LoadDefaultControl();
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    LoadDefaultControl();
                    loadData();
                    btnClear.Visible = false;
                    break;
                default:
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    LoadDefaultControl();
                    loadData();

                    pnCustCode.Visible = false;
                    pnCustInfo.Enabled = false;
                    pnImportNewDocument.Visible = false;
                    pnDocumentBusiness.Visible = false;

                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void LoadDataMerchantByPhone(object sender, EventArgs e)
    {
        try
        {
            if (!CheckIsPhoneNumer(txtPhoneNumber.Text.Trim()))
            {
                lblError.Text = Resources.labels.phonenumberwrong;
            }
            else
            {
                txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
                lblError.Text = string.Empty;
            }


            DataSet ds = new DataSet();
            object[] inforConsumer = new object[] { Utility.KillSqlInjection(txtPhoneNumber.Text.Trim()) };
            ds = _service.common("SEMS_BO_GET_KYCCSM", inforConsumer, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtConsumerName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    txtPaperNumber.Text = txtPaperNumberImport.Text = ds.Tables[0].Rows[0]["LICENSEID"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["ADDRRESIDENT"].ToString();
                    ddlKycType.SelectedValue = ds.Tables[0].Rows[0]["LICENSETYPE"].ToString().Trim();
                    ddWalletLevel.SelectedValue = ds.Tables[0].Rows[0]["ContractLevelId"].ToString().Trim();
                    ddStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUS"].ToString().Trim();
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["NATION"].ToString().Trim()))
                    {
                        ddNationality.SelectedValue = ds.Tables[0].Rows[0]["NATION"].ToString().Trim();
                    }
                    txtIssueDate.Text = ds.Tables[0].Rows[0]["ISSUEDATE"].ToString();
                    txtExpiryDate.Text = ds.Tables[0].Rows[0]["EXPIRYDATE"].ToString();
                    txtBirthday.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    if (ds.Tables[0].Rows[0]["SEX"].ToString().Trim().Equals("M") || ds.Tables[0].Rows[0]["SEX"].ToString().Trim().Equals("F"))
                    {
                        ddlGender.SelectedValue = ds.Tables[0].Rows[0]["SEX"].ToString();
                    }
                    txtCustCode.Text = ds.Tables[0].Rows[0]["CUSTCODE"].ToString();
                    txtCustCode.Enabled = false;
                    btnSearch.Visible = false;
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

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtDocumentName.Text.Trim()))
            {
                lblError.Text = Resources.labels.DocumentName + Resources.labels.IsNotNull;
                txtDocumentName.Enabled = true;
                txtDocumentName.BorderColor = System.Drawing.Color.Red;
                txtDocumentName.Focus();
                return;
            }

            foreach (DocumentModel item in listDocumentModel)
            {
                if (item.No.ToString() == ViewState["No"].ToString())
                {
                    item.DocumentName = txtDocumentName.Text.Trim();
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

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

    public void deleteDocument(string DocumentID)
    {
        try
        {
            DataSet ds = new DataSet();
            string UserId = HttpContext.Current.Session["userID"].ToString();
            object[] searchObject = new object[] { DocumentID, UserId };
            ds = _service.common("SEMS_DOC_DELETE_FILE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.deletesuccessfully;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Dictionary<object, object> _para = new Dictionary<object, object>();
            if (!ValidateSave())
            {
                return;
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    if (listDocumentModel.Count == 0)
                    {
                        lblError.Text = Resources.labels.document + Resources.labels.IsNotNull;
                        return;
                    }
                    string userid, custid, paperNO;
                    userid = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "USERID", ref IPCERRORCODE, ref IPCERRORDESC);
                    custid = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "CUSTID", ref IPCERRORCODE, ref IPCERRORDESC);
                    paperNO = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtPaperNumber.Text.Trim());

                    _para.Add("CUSTID", custid);
                    _para.Add("FULLNAME", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtConsumerName.Text.Trim()));
                    _para.Add("DOB", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtBirthday.Text.Trim()));
                    _para.Add("ADDRESS", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtAddress.Text.Trim()));
                    _para.Add("GENDER", SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlGender.SelectedValue.Trim()));
                    _para.Add("PHONENO", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtPhoneNumber.Text.Trim()));
                    _para.Add("EMAIL", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtEmail.Text.Trim()));
                    _para.Add("PAPERTYPE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlKycType.SelectedValue.Trim()));
                    _para.Add("PAPERNO", paperNO);
                    _para.Add("ISSUEDATE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtIssueDate.Text.Trim()));
                    _para.Add("EXPIRYDATE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtExpiryDate.Text.Trim()));
                    _para.Add("CUSTCODE", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCustCodeInfo.Text.Trim()));
                    _para.Add("USERCREATE", HttpContext.Current.Session["userID"].ToString());
                    _para.Add("USERID", userid);
                    _para.Add("KYCTYPE", "CSM");

                    DataTable tblDocument = new DataTable();
                    tblDocument.Columns.AddRange(new DataColumn[] { new DataColumn("DOCUMENTTYPE"), new DataColumn("DOCUMENTNAME"), new DataColumn("FILEBIN"), new DataColumn("STATUS") });
                    foreach (var item in listDocumentModel)
                    {
                        DataRow r = tblDocument.NewRow();
                        r["DOCUMENTTYPE"] = item.DocumentType;
                        r["DOCUMENTNAME"] = item.DocumentName;
                        r["FILEBIN"] = item.File;
                        r["STATUS"] = item.Status;
                        tblDocument.Rows.Add(r);
                    }
                    _para.Add("DOCUMENT", JsonConvert.SerializeObject(tblDocument));

                    _service.CallStore("INSERT_KYC_CONSUMER", _para, "Create Consumer Request", "N", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.success;
                        pnCustCode.Enabled = false;
                        pnCustInfo.Enabled = false;
                        pnImportNewDocument.Enabled = false;
                        pnDocumentBusiness.Enabled = false;
                        pnDocument.Enabled = false;
                        btnSave.Enabled = false;
                        btnClear.Enabled = true;
                        enabledDel = false;
                        rptData.DataSource = listDocumentModel;
                        rptData.DataBind();
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    break;
                default:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;

                    _para.Add("REQUESTNO", Utility.KillSqlInjection(ViewState["REQUESTNO"].ToString()));
                    _para.Add("USERID", Utility.KillSqlInjection(hdfUserID.Value));
                    _para.Add("PAPERNO", Utility.KillSqlInjection(txtPaperNumberImport.Text.Trim()));
                    _para.Add("ISSUEDATE", Utility.KillSqlInjection(txtIssueDate.Text.Trim()));
                    _para.Add("EXPIRYDATE", Utility.KillSqlInjection(txtExpiryDate.Text.Trim()));
                    _para.Add("USERCREATE", HttpContext.Current.Session["userID"].ToString());

                    DataTable tblDocumentUpdate = new DataTable();
                    tblDocumentUpdate.Columns.AddRange(new DataColumn[] { new DataColumn("DOCUMENTTYPE"), new DataColumn("DOCUMENTNAME"), new DataColumn("FILEBIN"), new DataColumn("STATUS") });
                    foreach (var item in listDocumentModel)
                    {
                        DataRow r = tblDocumentUpdate.NewRow();
                        r["DOCUMENTTYPE"] = item.DocumentType;
                        r["DOCUMENTNAME"] = item.DocumentName;
                        r["FILEBIN"] = item.File;
                        r["STATUS"] = item.Status;
                        tblDocumentUpdate.Rows.Add(r);
                    }
                    _para.Add("DOCUMENT", JsonConvert.SerializeObject(tblDocumentUpdate));

                    _service.CallStore("SEMS_CON_KYC_UPDATE", _para, "Update KYC Consumer Request", "N", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.success;
                        pnCustCode.Enabled = false;
                        pnCustInfo.Enabled = false;
                        pnImportNewDocument.Enabled = false;
                        pnDocumentBusiness.Enabled = false;
                        pnDocument.Enabled = false;
                        btnSave.Enabled = false;
                        btnClear.Enabled = false;
                        enabledDel = false;
                        rptData.DataSource = listDocumentModel;
                        rptData.DataBind();
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string ctmType = IPC.PERSONAL;
            Hashtable hasCustInfo = new SmartPortal.SEMS.Customer().GetCustInfo(txtCustCode.Text.Trim(), ctmType, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                lblError.Text = Resources.labels.customerinformationreturnisincorrect;
                return;
            }
            if (hasCustInfo[IPC.CUSTCODE] == null)
            {
                lblError.Text = Resources.labels.customerinformationreturnisincorrect;
                return;
            }
            if (hasCustInfo[IPC.PHONE] != null)
            {
                if (CheckIsPhoneNumer(hasCustInfo[IPC.PHONE].ToString()))
                {
                    txtPhoneNumber.Text = hasCustInfo[IPC.PHONE].ToString();
                }
                else
                {
                    lblError.Text = Resources.labels.phonenumberwrong;
                    return;
                }
            }
            if (hasCustInfo[IPC.CUSTCODE] != null)
            {
                txtCustCodeInfo.Text = hasCustInfo[IPC.CUSTCODE].ToString();
            }


            if (hasCustInfo[IPC.CUSTNAME] != null)
            {
                txtConsumerName.Text = hasCustInfo[IPC.CUSTNAME].ToString();
            }
            if (hasCustInfo[IPC.DOB] != null)
            {
                try
                {
                    string birthDate = SmartPortal.Common.Utilities.Utility.IsDateTime1(hasCustInfo[IPC.DOB].ToString()).ToString("dd/MM/yyyy");
                    txtBirthday.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;
                    txtBirthday.Enabled = string.IsNullOrEmpty(txtBirthday.Text.Trim());
                }
                catch
                {
                }
            }



            if (hasCustInfo[IPC.SEX] != null)
            {
                try
                {
                    ddlGender.SelectedValue = SmartPortal.Common.Utilities.Utility.FormatStringCore(int.Parse(hasCustInfo[IPC.SEX].ToString()).ToString());

                    ddlGender.Enabled = string.IsNullOrEmpty(hasCustInfo[IPC.SEX].ToString().Trim());
                }
                catch
                {
                }
            }
            if (hasCustInfo[IPC.ADDRESS] != null)
            {
                txtAddress.Text = hasCustInfo[IPC.ADDRESS].ToString();

                txtAddress.Enabled = string.IsNullOrEmpty(txtAddress.Text.Trim());
            }
            if (hasCustInfo[IPC.LICDATE] != null)
            {
                try
                {
                    txtIssueDate.Text = txtIssueDate.Text = txtIssueDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime1(hasCustInfo[IPC.LICDATE].ToString()).ToString("dd/MM/yyyy");

                    if (string.IsNullOrEmpty(txtIssueDate.Text.Trim()))
                    {
                        txtIssueDate.Enabled = txtIssueDate.Enabled = true;
                    }
                    else
                    {
                        txtIssueDate.Enabled = txtIssueDate.Enabled = false;
                    }
                }
                catch
                {
                }
            }

            if (hasCustInfo["NRIC"] != null && hasCustInfo["IDTYPE"] != null)
            {
                txtPaperNumberImport.Text = txtPaperNumber.Text = hasCustInfo["NRIC"].ToString().Trim();
                try
                {
                    ddlKycType.SelectedValue = GetKYCIDByCustCode(hasCustInfo["IDTYPE"].ToString().Trim()).ToString();
                }
                catch
                {

                }
                ddlKycType.Enabled = false;
                txtPaperNumber.Enabled = false;
                txtPaperNumberImport.Enabled = false;
            }
            else if (hasCustInfo["NRIC"] != null && hasCustInfo["IDTYPE"] == null)
            {
                txtPaperNumberImport.Text = txtPaperNumber.Text = hasCustInfo["NRIC"].ToString().Trim();
                ddlKycType.Enabled = true;
                txtPaperNumber.Enabled = false;
                txtPaperNumberImport.Enabled = false;
            }
            else if (hasCustInfo["NRIC"] == null && hasCustInfo["IDTYPE"] != null)
            {
                try
                {
                    ddlKycType.SelectedValue = GetKYCIDByCustCode(hasCustInfo["IDTYPE"].ToString().Trim()).ToString();
                }
                catch
                {

                }
                ddlKycType.Enabled = false;
                txtPaperNumber.Enabled = true;
                txtPaperNumberImport.Enabled = true;
            }
            else
            {
                ddlKycType.Enabled = true;
                txtPaperNumber.Enabled = true;
            }
            if (!CheckIsPhoneNumer(txtPhoneNumber.Text.Trim()))
            {
                lblError.Text = Resources.labels.PhoneNumber + Resources.labels.Incorrect;
                txtPhoneNumber.Focus();
                return;
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
            case IPC.ACTIONPAGE.EDIT:
                foreach (DocumentModel item in listDocumentModel)
                {
                    if (item.No.ToString() == commandArg)
                    {
                        loadCombobox_KYCDocumentName_Repeater();
                        txtDocumentName.Text = item.DocumentName;
                        ddlDocumentType.SelectedValue = item.DocumentType;
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
                        string[] arrListStr = commandArg.Split('|');
                        lblError.Text = string.Empty;
                        // Trường hợp Del file vừa import
                        if (item.No.ToString() == arrListStr[0])
                        {
                            if (item.IsNew)
                            {
                                listDocumentModel.Remove(item);
                                resetNoDocument();
                            }
                            else // Trường hợp Del file trong database
                            {
                                item.Status = "D";
                            }
                            rptData.DataSource = listDocumentModel;
                            rptData.DataBind();
                            return;
                        }

                    }
                }
                break;
        }
    }

    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                LinkButton lbtnViewFile, lbtnDelete;
                DocumentModel doc = e.Item.DataItem as DocumentModel;
                string status = doc.Status;
                ddlDocumentType.Enabled = false;
                switch (ACTION)
                {
                    case IPC.ACTIONPAGE.ADD:
                        if (enabledDel == false)
                        {
                            lbtnViewFile = (LinkButton)e.Item.FindControl("lbtnViewFile");
                            lbtnViewFile.Enabled = false;

                            lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
                            lbtnDelete.Enabled = false;
                            lbtnDelete.CssClass = "btn btn-secondary";
                            lbtnDelete.OnClientClick = null;

                            fileUpdate.Enabled = false;
                            pannelModal.Enabled = false;
                        }
                        break;
                    case IPC.ACTIONPAGE.EDIT:
                        if (ddStatus.SelectedValue != "P" || status.Equals("D") || enabledDel == false)
                        {
                            lbtnViewFile = (LinkButton)e.Item.FindControl("lbtnViewFile");
                            lbtnViewFile.Enabled = false;

                            lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
                            lbtnDelete.Enabled = false;
                            lbtnDelete.CssClass = "btn btn-secondary";
                            lbtnDelete.OnClientClick = null;

                            fileUpdate.Enabled = false;
                            pannelModal.Enabled = false;
                        }
                        break;
                    case IPC.ACTIONPAGE.APPROVE:
                        fileUpdate.Visible = false;
                        txtDocumentName.Enabled = false;
                        btnImportUpdate.Visible = false;
                        lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
                        lbtnDelete.CssClass = "btn btn-secondary";
                        lbtnDelete.Enabled = false;
                        lbtnDelete.OnClientClick = null;
                        break;
                    default:

                        lbtnViewFile = (LinkButton)e.Item.FindControl("lbtnViewFile");
                        lbtnViewFile.Enabled = false;

                        lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
                        lbtnDelete.Enabled = false;
                        lbtnDelete.CssClass = "btn btn-secondary";
                        lbtnDelete.OnClientClick = null;

                        fileUpdate.Enabled = false;
                        pannelModal.Enabled = false;
                        break;
                }
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
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=SEMS_KYC_CONSUMER_ADD"));
    }

    protected void DocumentTypeImport_OnTextChanged(object sender, EventArgs e)
    {
        txtDocumentNameImport.Text = ddlDocumentTypeImport.SelectedItem.Text.Trim();
    }

    protected void loadKycType(object sender, EventArgs e)
    {
        txtPaperTypeImport.Text = ddlKycType.SelectedItem.Text.Trim();
    }


    public bool CheckIsPhoneNumer(string phone)
    {
        string result = new Customer().CheckPhoneTeLCo(phone, ref IPCERRORCODE, ref IPCERRORDESC);
        if (result == IPC.TRANSTATUS.BEGIN)
        {
            return true;
        }
        else
        {
            return false;
        }

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
    private bool ValidateSave()
    {
        try
        {
            txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
            txtConsumerName.BorderColor = System.Drawing.Color.Empty;
            txtExpiryDate.BorderColor = System.Drawing.Color.Empty;
            txtIssueDate.BorderColor = System.Drawing.Color.Empty;
            txtBirthday.BorderColor = System.Drawing.Color.Empty;
            txtPaperNumber.BorderColor = System.Drawing.Color.Empty;
            txtPaperNumberImport.BorderColor = System.Drawing.Color.Empty;

            if (string.IsNullOrEmpty(txtPhoneNumber.Text.Trim()))
            {
                lblError.Text = Resources.labels.bannhapsodienthoai;
                txtPhoneNumber.Enabled = true;
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return false;
            }
            if (!CheckIsPhoneNumer(txtPhoneNumber.Text.Trim()))
            {
                lblError.Text = Resources.labels.PhoneNumber + Resources.labels.Incorrect;
                txtPhoneNumber.Enabled = true;
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtConsumerName.Text.Trim()))
            {
                lblError.Text = Resources.labels.bancannhapvaluename;
                txtConsumerName.Enabled = true;
                txtConsumerName.BorderColor = System.Drawing.Color.Red;
                txtConsumerName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtBirthday.Text.Trim()))
            {
                lblError.Text = Resources.labels.Youneedtoinputdateofbirth;
                txtBirthday.Enabled = true;
                txtBirthday.BorderColor = System.Drawing.Color.Red;
                txtBirthday.Focus();
                return false;
            }
            DateTime ExpiryDate = new DateTime();
            DateTime IssueDate = new DateTime();
            if (!string.IsNullOrEmpty(txtIssueDate.Text))
            {
                IssueDate = DateTime.ParseExact(txtIssueDate.Text.ToString(), "dd/MM/yyyy", null);
            }

            if (!string.IsNullOrEmpty(txtExpiryDate.Text))
            {
                ExpiryDate = DateTime.ParseExact(txtExpiryDate.Text.ToString(), "dd/MM/yyyy", null);
            }

            if (!string.IsNullOrEmpty(txtIssueDate.Text) && !string.IsNullOrEmpty(txtExpiryDate.Text))
            {
                if (IssueDate >= ExpiryDate)
                {
                    lblError.Text = string.Format(" {0} must be greater than {1}", Resources.labels.ExpiryDate, Resources.labels.IssueDate);
                    txtExpiryDate.BorderColor = System.Drawing.Color.Red;
                    txtIssueDate.BorderColor = System.Drawing.Color.Red;
                    txtExpiryDate.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtPaperNumber.Text.Trim()))
            {
                lblError.Text = Resources.labels.PaperNumber + " " + Resources.labels.IsNotNull;
                txtPaperNumber.Enabled = true;
                txtPaperNumber.BorderColor = System.Drawing.Color.Red;
                txtPaperNumber.Focus();
                return false;
            }
            if (!Utility.CheckSpecialCharacters(txtPaperNumber.Text))
            {
                lblError.Text = Resources.labels.PaperNumber + " " + Resources.labels.ErrorSpeacialCharacters;
                txtPaperNumber.Enabled = true;
                txtPaperNumber.BorderColor = System.Drawing.Color.Red;
                txtPaperNumber.Focus();
                return false;
            }
            string dateExpire_date = new SmartPortal.SEMS.EBASYSVAR().ViewDetail("YEAROLD", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0].Rows[0]["VARVALUE"].ToString(); ;
            DateTime myDate = DateTime.ParseExact(txtBirthday.Text.Trim(), "dd/MM/yyyy",
                                      System.Globalization.CultureInfo.InvariantCulture);
            if (myDate > DateTime.Now.AddYears(-int.Parse(dateExpire_date)))
            {
                lblError.Text = Resources.labels.underageuser;
                txtBirthday.Enabled = true;
                txtBirthday.BorderColor = System.Drawing.Color.Red;
                txtBirthday.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPaperNumberImport.Text.Trim()))
            {
                lblError.Text = Resources.labels.PaperNumber + " " + Resources.labels.IsNotNull;
                txtPaperNumberImport.Enabled = true;
                txtPaperNumberImport.BorderColor = System.Drawing.Color.Red;
                txtPaperNumberImport.Focus();
                return false;
            }
            if (!Utility.CheckSpecialCharacters(txtPaperNumberImport.Text))
            {
                lblError.Text = Resources.labels.PaperNumber + " " + Resources.labels.ErrorSpeacialCharacters;
                txtPaperNumberImport.Enabled = true;
                txtPaperNumberImport.BorderColor = System.Drawing.Color.Red;
                txtPaperNumberImport.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDocumentNameImport.Text.Trim()))
            {
                lblError.Text = Resources.labels.DocumentName + " " + Resources.labels.IsNotNull;
                txtDocumentNameImport.Enabled = true;
                txtDocumentNameImport.BorderColor = System.Drawing.Color.Red;
                txtDocumentNameImport.Focus();
                return false;
            }

            if (listDocumentModel.Count == 0)
            {
                lblError.Text = Resources.labels.document + " " + Resources.labels.IsNotNull;
                return false;
            }

            bool has = true;
            has = listDocumentModel.Any(x => x.Status != "D");
            if (!has)
            {
                lblError.Text = Resources.labels.document + " " + Resources.labels.khonghople;
                return false;
            }
            has = listDocumentModel.Any(x => x.Status != "D" && x.DocumentType == "NF");
            if (!has)
            {
                lblError.Text = "Document Type PP Front " + Resources.labels.khonghople;
                return false;
            }
            has = listDocumentModel.Any(x => x.Status != "D" && x.DocumentType == "NB");
            if (!has)
            {
                lblError.Text = "Document Type PP Back " + Resources.labels.khonghople;
                return false;
            }
            return true;
        }
        catch
        {
            lblError.Text =  Resources.labels.khonghople +" "+ Resources.labels.thongtinkhachhang;
            return false;
        }
    }
    private string GetKYCIDByCustCode(string kycid)
    {
        DataSet ds = new SmartPortal.SEMS.Contract().GETKYCODEBYKYCID(kycid, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
            {
                return dt.Rows[0]["KycCode"].ToString();
            }
        }
        return null;
    }
    protected void btnImportFile_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            txtPaperNumberImport.BorderColor = System.Drawing.Color.Empty;
            txtDocumentNameImport.BorderColor = System.Drawing.Color.Empty;
            txtPaperNumberImport.Text = txtPaperNumber.Text;
            if (string.IsNullOrEmpty(txtPaperNumberImport.Text.Trim()))
            {
                lblError.Text = Resources.labels.PaperNumber + Resources.labels.IsNotNull;
                txtPaperNumberImport.BorderColor = System.Drawing.Color.Red;
                txtPaperNumberImport.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDocumentNameImport.Text.Trim()))
            {
                lblError.Text = Resources.labels.DocumentName + Resources.labels.IsNotNull;
                txtDocumentNameImport.BorderColor = System.Drawing.Color.Red;
                txtDocumentNameImport.Focus();
                return;
            }
            foreach (var item in listDocumentModel)
            {
                if (item.DocumentType == ddlDocumentTypeImport.SelectedValue && item.Status != "D")
                {
                    lblError.Text = Resources.labels.TheDocumentTypeHasAnError;
                    return;
                }
            }

            if (documentUpload.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(documentUpload.FileName).ToLower();
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

                DocumentModel item = new DocumentModel();
                DataTable tb = (DataTable)ViewState["listDocumentName"];
                item.IsNew = true;
                item.DocumentCode = string.Empty;
                item.DateCreated = DateTime.Now.ToString("dd/MM/yyyy");
                item.Status = "P";
                int no = listDocumentModel.Count;
                item.No = no + 1;
                item.DocumentName = txtDocumentNameImport.Text.Trim();
                item.DocumentType = ddlDocumentTypeImport.SelectedValue;

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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected static string GetStatus(object status)
    {
        string result = string.Empty;
        switch (status.ToString())
        {
            case "A":
                result = Resources.labels.active;
                break;
            case "D":
                result = Resources.labels.delete;
                break;
            case "C":
                result = Resources.labels.cancel;
                break;
            case "R":
                result = Resources.labels.reject;
                break;
            case "P":
                result = Resources.labels.pendingforapprove;
                break;
        }

        return result;
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (!CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE)) return;
        try
        {
            if (!ValidateSave())
            {
                return;
            }

            DataSet ds = new DataSet();
            Dictionary<object, object> _para = new Dictionary<object, object>();

            if (ddStatus.SelectedValue.Trim().Equals("P"))
            {
                _para.Add("REQUESTID", int.Parse(ViewState["REQUESTNO"].ToString()));
                _para.Add("USERAPPROVED", HttpContext.Current.Session["userID"].ToString());
                _para.Add("STATUS", "A");
                ds = _service.CallStore("SEMS_BO_APKYC_CON", _para, "Approve Agent/Mechant KYC Request", "N", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.ApproveSuccessfully;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }
                else
                {
                    lblError.Text = IPCERRORDESC;
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (!CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE)) return;
        RedirectToActionPage(IPC.ACTIONPAGE.REJECT, "&" + SmartPortal.Constant.IPC.ID + "=" + ViewState["REQUESTNO"].ToString());
    }
}