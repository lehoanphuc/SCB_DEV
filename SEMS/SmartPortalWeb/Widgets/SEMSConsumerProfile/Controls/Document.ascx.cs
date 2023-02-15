using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;
using SmartPortal.Constant;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSConsumerProfile_Controls_Document : WidgetBase
{
    string ACTION = "";
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    String countRow = "0";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        ACTION = GetActionPage();
        if (!IsPostBack)
        {
            loadCombobox();
            setControldefault();
            BindData();
        }
        GridViewPaging.pagingClickArgs += new EventHandler(btnSearch_Click);
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
                btnImportFile.Enabled = false;
                documentUpload.Enabled = false;
                break;

        }
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
                    loadData();
                    loadData_Repeater();
                    break;
                default:
                    loadData();
                    loadData_Repeater();
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void loadCombobox_Status()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("EBA_CustInfo", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlStatus.DataSource = ds;
                    ddlStatus.DataValueField = "VALUEID";
                    ddlStatus.DataTextField = "CAPTION";
                    ddlStatus.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    private void loadCombobox_WalletLevel()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] loadContractLevel = new object[] { string.Empty };
            ds = _service.common("SEMS_BO_LST_CON_LV", loadContractLevel, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlWalletLevel.DataSource = ds;
                    ddlWalletLevel.DataValueField = "CONTRACTLEVELID";
                    ddlWalletLevel.DataTextField = "CONTRACTLEVELNAME";
                    ddlWalletLevel.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    public void loadCombobox()
    {
        loadCombobox_Status();
        loadCombobox_WalletLevel();
    }


    void loadData()
    {
        try
        {
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { ID };
            ds = _service.common("SEMS_CON_INFO_VIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count < 1) return;
                    DataTable dataTable = ds.Tables[0];
                    ViewState["CUSTID"] = dataTable.Rows[0]["CUSTID"].ToString();
                    txtConsumerCode.Text = dataTable.Rows[0]["CUSTID"].ToString();
                    txtDateCreate.Text = dataTable.Rows[0]["DATECREATED_FORMAT"].ToString();
                    txtPhoneNumber.Text = dataTable.Rows[0]["TEL"].ToString();
                    txtLastModifiedate.Text = dataTable.Rows[0]["LASTMODIFIED_FORMAT"].ToString();
                    ddlStatus.SelectedValue = dataTable.Rows[0]["STATUS"].ToString();
                    txtCreateBy.Text = dataTable.Rows[0]["USERCREATED"].ToString();
                    txtApproveBy.Text = dataTable.Rows[0]["USERAPPROVED"].ToString();
                    ddlWalletLevel.SelectedValue = dataTable.Rows[0]["ContractLevelId"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void loadData_Repeater()
    {
        try
        {
           
            DataSet ds = new DataSet();
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            object[] searchObject = new object[] { ID, GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_CO_DOC_CUSID", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    countRow = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0][IPC.TRECORDCOUNT].ToString();
                    rptData.DataSource = ds.Tables[0];
                    rptData.DataBind();
                }
            }
            GridViewPaging.total = countRow;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        lblError.Text = string.Empty;
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
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
                deleteDocument(commandArg);
                BindData();
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (ACTION == IPC.ACTIONPAGE.DETAILS)
                {

                        LinkButton lbtnDelete;
                        lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDeleteFile");
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

    protected override void Render(HtmlTextWriter writer)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class=\"content \" >");
        sb.Append("<div>");
        writer.Write(sb.ToString());
        base.Render(writer);
        writer.Write("</div>");
    }

    public void importfile( string docName, string file)
    {
        try
        {
            DataSet ds = new DataSet();
            string UserId = HttpContext.Current.Session["userID"].ToString();
            string custID = GetParamsPage(IPC.ID)[0].Trim();
            object[] searchObject = new object[] { UserId, custID, docName, file };
            ds = _service.common("SEMS_DOC_IMPORT_FILE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.topupimportsuccess;
            }
            else lblError.Text = IPCERRORDESC;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }

    protected void btnImportFile_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (documentUpload.HasFile)
            {
                string extension = System.IO.Path.GetExtension(documentUpload.FileName);
                if (extension.ToUpper() != ".JPG" && extension.ToUpper() != ".PNG" && extension.ToUpper() != ".GIF" && extension.ToUpper() != ".JPEG")
                {
                    
                    Response.Write("<script>alert('Select Correct file format')</script>");
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
                string documentname = documentUpload.FileName.Replace(extension, "");
                string file = base64String;
                importfile(documentname, file);
                loadData_Repeater();
            }
           else
            {
                lblError.Text = "You have not selected the file to import. Click choose file!";
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
            lblError.Text = ex.Message;
        }
        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void btnDownloadAll_Click1(object sender, EventArgs e)
    {
        try
        {
            string filename = GetParamsPage(IPC.ID)[0].Trim() + ".zip";
            ZipOutputStream zos = null;
            MemoryStream ms = null;
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            DataSet ds = new DataSet();
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            object[] searchObject = new object[] { ID, GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_CO_DOC_CUSID", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

            if( ds != null )
            {
                if( ds.Tables.Count > 0)
                {
                    ms = new MemoryStream();
                    zos = new ZipOutputStream(ms);
                    for ( int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string ImgStr = ds.Tables[0].Rows[i]["FILE"].ToString();
                        string ImgName = ds.Tables[0].Rows[i]["DOCUMENTCODE"].ToString();
                        int indexname = ImgStr.IndexOf(",");
                        string path = ImgStr.Substring(indexname + 1);
                        string imageName = ImgName + ".jpg";

                        byte[] imageBytes = Convert.FromBase64String(path);

                        ZipEntry imgEntry = new ZipEntry(imageName);
                        imgEntry.Size = imageBytes.Length;

                        zos.PutNextEntry(imgEntry);
                        zos.Write(imageBytes, 0, imageBytes.Length);
                       // GnuObjectIdentifiers sao nua ta
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadData_Repeater();
    }
}