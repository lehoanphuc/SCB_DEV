using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSBillerCategory_Control_Widget : WidgetBase
{ 
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty; 
    string IPCERRORDESC = string.Empty;   
    private static string logoType = IPC.URI;
    public string _TITLE
    {
        set { lbTitle.Text = value; }
        get { return lbTitle.Text; }
    } 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            lbMessage.Text = string.Empty;
            if (!IsPostBack)
            {
                BindData();
            } 
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;

                default:
                    btnClear.Enabled = false;
                    string _id = GetParamsPage(IPC.CATID)[0].Trim();
                    DataSet ds = new SmartPortal.SEMS.EBA_BillerCategory().GetBillerCatByCatID(_id, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        DataTable dataTable = new DataTable();
                        dataTable = ds.Tables[0];

                        if (dataTable.Rows.Count > 0)
                        {
                            txtCatID.Text = dataTable.Rows[0]["CatID"].ToString();
                            txtCatName.Text = dataTable.Rows[0]["CatName"].ToString();
                            txtCatNameLocal.Text = dataTable.Rows[0]["CatNameLocal"].ToString();
                            txtCatShortName.Text = dataTable.Rows[0]["CatShortName"].ToString();
                            ddlStatus.SelectedValue = dataTable.Rows[0]["Status"].ToString(); 
                            Image1.ImageUrl = dataTable.Rows[0]["CatLogoBin"].ToString();
                            txtLinkImage.Text = Image1.ImageUrl;
                            logoType = dataTable.Rows[0]["CatLogoType"].ToString();
                        }
                    }
                    else
                    {
                        throw new IPCException(IPCERRORCODE);
                    }
                    break;
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!(string.IsNullOrEmpty(Image1.ImageUrl)))
                    {
                        lbImg.Visible = true;
                        Image1.Visible = true;
                    }
                    break;

                case IPC.ACTIONPAGE.DETAILS: 
                    Panel1.Enabled = false;
                    btnSave.Enabled = false;
                    //FileUpload1.Visible = false;
                    //cbxURI.Visible = false;
                    //txtWayUpload.Visible = false; 

                    if (!(string.IsNullOrEmpty(Image1.ImageUrl)))
                    {
                        lbImg.Visible = true;
                        Image1.Visible = true;
                    }  
                    break;

                case IPC.ACTIONPAGE.EDIT: 
                    txtCatID.Enabled = false;

                    if (!(string.IsNullOrEmpty(Image1.ImageUrl)))
                    {
                        lbImg.Visible = true;
                        Image1.Visible = true;
                    }

                    if (logoType.Equals(IPC.URI))
                    {
                        //cbxURI.Checked = true;
                        //FileUpload1.Enabled = false;
                        lbPath.Visible = true;
                        txtLinkImage.Visible = true;
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    } 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string catID = Utility.KillSqlInjection(txtCatID.Text.Trim());
            string catName = Utility.KillSqlInjection(txtCatName.Text.Trim());
            string catShortName = Utility.KillSqlInjection(txtCatShortName.Text.Trim());
            string status = Utility.KillSqlInjection(ddlStatus.SelectedValue.ToString().Trim());
            string logoBin = txtLinkImage.Text.Trim();
            Image1.ImageUrl = logoBin;  
            Image1.Visible = true;    

            if (string.IsNullOrEmpty(txtLinkImage.Text)) 
            {
                logoType = string.Empty; 
                Image1.Visible = false;
                lbImg.Visible = false; 
            }
            else
            {
                logoType = IPC.URI;  
                lbImg.Visible = true;
            }
            #region Image type BASE64
            //if (cbxURI.Checked == true)  
            //{
            //    logoType = IPC.URI;
            //    lbImg.Visible = true;
            //    logoBin = txtLinkImage.Text.Trim();
            //    Image1.ImageUrl = logoBin;
            //    Image1.Visible = true;
            //}
            //else if (FileUpload1.HasFile) 
            //{
            //    System.IO.Stream fs = FileUpload1.PostedFile.InputStream;
            //    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            //    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            //    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            //    Image1.ImageUrl = "data:image/png;base64," + base64String;
            //    logoBin = Image1.ImageUrl;  

            //    Image1.Visible = true;
            //    lbImg.Visible = true;
            //    logoType = IPC.BASE64;
            //}
            //else if (string.IsNullOrEmpty(logoType) || string.IsNullOrEmpty(txtLinkImage.Text.Trim()))
            //{
            //    logoType = IPC.BASE64;
            //}
            #endregion
            switch (ACTION) 
            { 
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    try
                    {
                        new SmartPortal.SEMS.EBA_BillerCategory().InsertBillerCat(catID, catName, catShortName, status, logoBin, logoType, Session["userName"].ToString(), Utility.KillSqlInjection(txtCatNameLocal.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            Panel1.Enabled = false;
                            btnSave.Enabled = false;
                            //FileUpload1.Enabled = false;
                            lbMessage.Text = Resources.labels.insertsucessfull;
                        }
                        else
                        {
                            lbMessage.Text = IPCERRORDESC;
                        }
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }
                    break;

                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    try
                    {
                        new SmartPortal.SEMS.EBA_BillerCategory().EditBillerCat(catName, catShortName, status, logoBin, logoType, Session["userName"].ToString(), catID, Utility.KillSqlInjection(txtCatNameLocal.Text.Trim())
                            , ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            Panel1.Enabled = false;
                            //FileUpload1.Enabled = false;
                            btnSave.Enabled = false;
                            lbMessage.Text = Resources.labels.updatesuccessfully;
                        }
                        else
                        {
                            lbMessage.Text = IPCERRORDESC;
                        }
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    } 
    protected void btnbackIndex_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Panel1.Enabled = true;
        btnSave.Enabled = true;
        txtCatID.Text = string.Empty;
        txtCatName.Text = string.Empty;
        txtCatShortName.Text = string.Empty;
        txtLinkImage.Text = string.Empty;
        lbMessage.Text = string.Empty;
        ddlStatus.ClearSelection();
         
        lbImg.Visible = false;
        Image1.Visible = false;   
        //cbxURI.Checked = false;
        //FileUpload1.Enabled = true;
    }
    //protected void cbx_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (cbxURI.Checked == true)
    //    {
    //        lbPath.Visible = true;
    //        txtLinkImage.Visible = true;
    //        txtLinkImage.Text = string.Empty;
    //        txtLinkImage.Enabled = true;
    //        FileUpload1.Enabled = false;
    //    }
    //    else
    //    {
    //        lbPath.Visible = false;
    //        txtLinkImage.Visible = false;
    //        FileUpload1.Enabled = true;
    //        txtLinkImage.Text = string.Empty;
    //        Image1.ImageUrl = string.Empty;
    //    }
    //}

}