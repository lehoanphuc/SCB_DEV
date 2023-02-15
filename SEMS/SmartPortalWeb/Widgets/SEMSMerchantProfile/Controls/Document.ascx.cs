using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSConsumerProfile_Controls_Document : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string ACTION = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    string MerchantID = string.Empty;
    string requestid = string.Empty;

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
        MerchantID = GetParamsPage(IPC.ID)[0].Trim();
        ACTION = GetActionPage();
        BindData();
        //btnPopup.Attributes["data-target"] = "#" + Image.ClientID;
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
        catch (Exception ex)
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

    public void loadCombobox()
    {
        loadCombobox_Status();
        loadCombobox_KYCLevel();
        loadCombobox_WalletLevel();
    }

    void loadData()
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
            }
        }
    }

    private void loadData_Repeater()
    {
        if (!IsPostBack)
        {
            loadData_ListDocument();
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
                loadData_ListDocument();
            }
        }
    }

    private void loadData_ListDocument()
    {
        listDocumentModel.Clear();
        DataSet ds = new DataSet();
        object[] _object = new object[] { txtPhoneNumber.Text.Trim(), IPC.PARAMETER.MERCHANT_AGENT };
        ds = _service.common("SEMS_DOC_BY_PHONE", _object, ref IPCERRORCODE, ref IPCERRORDESC);
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
                    item.DateCreated = ds.Tables[0].Rows[i]["DateCreated"].ToString();
                    item.UserCreated = ds.Tables[0].Rows[i]["UserCreated"].ToString();
                    item.DocumentCode = ds.Tables[0].Rows[i]["DocumentCode"].ToString();
                    item.DocumentName = ds.Tables[0].Rows[i]["DocumentName"].ToString();
                    item.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                    item.ValueStatus = ds.Tables[0].Rows[i]["ValueStatus"].ToString();
                    item.File = ds.Tables[0].Rows[i]["File"].ToString();
                    listDocumentModel.Add(item);
                }
                rptData.DataSource = listDocumentModel;
                rptData.DataBind();
            }
        }
    }

    private void loadData_Repeater2()
    {
        rptData.DataSource = listDocumentModel;
        rptData.DataBind();
    }
    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    btSaveGeneral.Visible = false;
                    btnImport.Visible = false;
                    documentUpload.Visible = false;
                    loadCombobox();
                    loadData();
                    loadData_Repeater();
                    break;
                default:
                    loadCombobox();
                    loadData();
                    loadData_Repeater();
                    break;
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
    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                TextBox txtDocumentName = (TextBox)e.Item.FindControl("txtDocumentName");
                LinkButton lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
                Button btnOK = (Button)e.Item.FindControl("btnOK");
                if (ACTION == IPC.ACTIONPAGE.DETAILS)
                {
                    txtDocumentName.Enabled = false;
                }
                
                lbtnDelete.CssClass = "btn btn-secondary";
                lbtnDelete.OnClientClick = null;
                btnOK.CssClass = "btn btn-primary";
                HtmlGenericControl lbStatusDocument = (HtmlGenericControl)e.Item.FindControl("lbStatusDocument");
                string status = lbStatusDocument.InnerText;
                if (status.Equals("D"))
                {
                    lbtnDelete.Enabled = false;
                    txtDocumentName.Enabled = false;
                    btnOK.Enabled = false;
                }
            }
        }
        catch(Exception ex)
        {

        }
    }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        //if (CheckPermitPageAction(commandName))
        //{
        switch (commandName)
        {
            case IPC.ACTIONPAGE.DETAILS:

                break;
            case IPC.ACTIONPAGE.EXPORT:
                int indexname = commandArg.IndexOf("---");
                string name = commandArg.Substring(0, indexname);
                string path = commandArg.Substring(indexname + 3);
                SaveImage(name, path);
                break;
            case IPC.ACTIONPAGE.DELETE:
                if (listDocumentModel.Count > 0)
                {
                    foreach (var item in listDocumentModel)
                    {
                        // Trường hợp Del file vừa import
                        if (item.No.ToString() == commandArg)
                        {
                            if (item.IsNew)
                            {
                                listDocumentModel.Remove(item);
                                resetNoDocument();
                                loadData_Repeater();
                                return;
                            }
                            else // Trường hợp Del file trong database
                            {
                                deleteDocument(commandArg);
                                resetNoDocument();
                                loadData_Repeater();
                                return;
                            }
                        }
                        
                    }
                }
                break;
        }
    }
    public void deleteDocument( String DocumentID)
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
            else lblError.Text = IPCERRORDESC;

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
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

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            txtDocumentName.BorderColor = System.Drawing.Color.Empty;

            if (!Utility.CheckSpecialCharacters(txtDocumentName.Text.Trim()))
            {
                lblError.Text = Resources.labels.DocumentName + Resources.labels.ErrorSpeacialCharacters;
                txtDocumentName.BorderColor = System.Drawing.Color.Red;
                txtDocumentName.Focus();
                return;
            }

            string idModal = "Modal" + txtNo.Text;
            if (txtDocumentName.Text.Trim().Equals(string.Empty))
            {
                txtDocumentName.BorderColor = System.Drawing.Color.Red;
                txtDocumentName.Focus();
                lblErrorPopup.InnerText = Resources.labels.DocumentName + " is not null";
                txtDocumentName.Text = txtDocname.Text;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + idModal + "');", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "ShowModal("+ idModal + ");", true);
            }
            else
            {
                foreach (DocumentModel item in listDocumentModel)
                {
                    if (item.No.ToString() == txtNo.Text)
                    {
                        item.DocumentName = txtDocumentName.Text.Trim();
                        if (item.IsNew == false)
                        {
                            item.IsUpdate = true;
                        }
                        break;
                    }
                }
                loadData_Repeater();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + idModal + "');", true);
            }
        }
        catch (Exception ex) { }
    }

    protected void btnImportFile_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            DocumentModel doc = new DocumentModel();
            if (documentUpload.HasFile)
            {
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
                    lblError.Text = "Extensions " + fileExtension + " not support";
                    return;
                }

                System.IO.Stream fs = documentUpload.PostedFile.InputStream;
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                DocumentModel item = new DocumentModel();
                item.IsNew = true;
                item.DocumentCode = string.Empty;
                item.DateCreated = DateTime.Now.ToString("dd/MM/yyyy");
                item.Status = string.Empty;
                item.No = listDocumentModel.Count + 1;
                item.DocumentID = item.No;
                item.DocumentName = documentUpload.FileName;
                item.File = "data:image/png;base64," + base64String;
                listDocumentModel.Add(item);
                loadData_Repeater2();
            }
            else
            {
                lblError.Text = "Import file upload not found";
            }
        }
        catch (Exception ex)
        {
        }
    }


    protected void btnDownloadAll_Click(object sender, EventArgs e)
    {
        try
        {

            ZipOutputStream zos = null;
            MemoryStream ms = null;
            string filename = MerchantID + "_" + DateTime.Now.ToString("dd/MM/yyyy");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".zip");
            DataSet ds = new DataSet();
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            object[] searchObject = new object[] { txtPhoneNumber.Text.Trim(), IPC.PARAMETER.MERCHANT_AGENT };
            ds = _service.common("SEMS_DOC_BY_PHONE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    ms = new MemoryStream();
                    zos = new ZipOutputStream(ms);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string ImgStr = ds.Tables[0].Rows[i]["File"].ToString();
                        string ImgName = ds.Tables[0].Rows[i]["DocumentCode"].ToString();
                        int indexname = ImgStr.IndexOf(",");
                        string path = ImgStr.Substring(indexname + 1);
                        string imageName = ImgName + ".jpg";

                        byte[] imageBytes = Convert.FromBase64String(path);

                        ZipEntry imgEntry = new ZipEntry(imageName);
                        imgEntry.Size = imageBytes.Length;

                        zos.PutNextEntry(imgEntry);
                        zos.Write(imageBytes, 0, imageBytes.Length);
                    }
                    zos.Finish();
                    zos.Close();
                }
                Response.Clear();
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    public  void SaveImage( string ImgName , string ImgStr)
    {
        try
        {
            int indexname = ImgStr.IndexOf(",");
            string path = ImgStr.Substring(indexname + 1);
            string imageName = ImgName + ".jpg";
            byte[] imageBytes = Convert.FromBase64String(path);
            Response.Clear();
            Response.ContentType = "image/jpg";
            Response.AddHeader("content-disposition", "attachment; filename=" + imageName);
            Response.BinaryWrite(imageBytes);
        }
        catch (Exception ex)
        {
          
        }
        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            if (listDocumentModel.Count > 0)
            {
                foreach (var item in listDocumentModel)
                {
                    if (item.IsNew)
                    {
                        object[] insertDoc = new object[] { MerchantID, item.DocumentName, item.DocumentType, item.File, HttpContext.Current.Session["userID"].ToString() };

                        ds = _service.common("SEMS_IMPORT_DOCUMENT", insertDoc, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            item.DocumentCode = ds.Tables[0].Rows[0]["DocumentCode"].ToString();
                            item.DateCreated = ds.Tables[0].Rows[0]["DateCreate"].ToString();
                            item.UserCreated = ds.Tables[0].Rows[0]["UserCreated"].ToString();
                            item.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                        }
                    }
                    else
                    {
                        if (item.IsUpdate == true)
                        {
                            object[] updateDoc = new object[] { item.DocumentID, item.UserId, txtDocumentName.Text, item.DocumentType, item.File, HttpContext.Current.Session["userID"].ToString() };
                            ds = _service.common("SEMS_DOC_UPDATE", updateDoc, ref IPCERRORCODE, ref IPCERRORDESC);
                            if (IPCERRORCODE != "0")
                            {
                                lblError.Text = "Update Unsuccess - " + IPCERRORDESC;
                            }
                        }
                    }
                }
            }
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.thanhcong;
                loadData_ListDocument();
                BindData();
            }
            else
            {
                lblError.Text = "Import file unsuccess - " + IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }

    }

}