using System;
using SmartPortal.ExceptionCollection;
using System.Data;
using SmartPortal.Constant;
using System.Web;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSDocumentManagement_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static string documentId;
    public static string UserId;

    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = HttpContext.Current.Session["userID"].ToString();
            documentId = GetParamsPage(IPC.ID)[0].Trim();
            lblError.Text = "";
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                loadCombobox();
                BindData();

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
                case IPC.ACTIONPAGE.APPROVE:
                    loadDocumentMannageData();
                    CheckAproveOrReject();
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void loadComboboxStatus()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("WAL_DOCUMENT", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
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
    void loadComboboxDocumentType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("WAL_DOCUMENT", "DCT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDocumentType.DataSource = ds;
                    ddlDocumentType.DataValueField = "VALUEID";
                    ddlDocumentType.DataTextField = "CAPTION";
                    ddlDocumentType.DataBind();

                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    void loadCustomerType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("EBA_Contract", "CTT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCustomerType.DataSource = ds;
                    ddlCustomerType.DataValueField = "VALUEID";
                    ddlCustomerType.DataTextField = "CAPTION";
                    ddlCustomerType.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    void loadPaperType()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { string.Empty };
            ds = _service.common("SEMS_BO_GET_INFO_KYC", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlPaperType.DataSource = ds;
                    ddlPaperType.DataValueField = "KycID";
                    ddlPaperType.DataTextField = "KycName";
                    ddlPaperType.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
    void enabledControl()
    {

        txtPhoneNumber.Enabled = false;
        txtFullName.Enabled = false;
        txtPaperNumber.Enabled = false;
        ddlCustomerType.Enabled = false;
        ddlPaperType.Enabled = false;
        ddlDocumentType.Enabled = false;
        ddlStatus.Enabled = false;

    }

    void CheckAproveOrReject()
    {
        if (ddlStatus.SelectedValue != "P" & ddlStatus.SelectedValue != "G")
        {
            btApprove.Enabled = false;
            btReject.Enabled = false;
        }


    }

    void loadCombobox()
    {
        enabledControl();
        loadCustomerType();
        loadComboboxStatus();
        loadPaperType();
        loadComboboxDocumentType();

    }

    void loadDocumentMannageData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(documentId) };
            ds = _service.common("SEMS_BO_DOCUMENTVIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable tb = ds.Tables[0];
                txtPhoneNumber.Text = tb.Rows[0]["Phone"].ToString();
                txtFullName.Text = tb.Rows[0]["FULLNAME"].ToString();
                ddlPaperType.SelectedValue = tb.Rows[0]["LICENSETYPE"].ToString();
                txtPaperNumber.Text = tb.Rows[0]["PaperNO"].ToString();
                ddlCustomerType.SelectedValue = tb.Rows[0]["ContractType"].ToString();
                ddlDocumentType.SelectedValue = tb.Rows[0]["DocumentType"].ToString();
                ddlStatus.SelectedValue = tb.Rows[0]["STATUS"].ToString();

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }

    protected void Show_viewfile(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(documentId) };
            ds = _service.common("SEMS_BO_DOCUMENTVIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable tb = ds.Tables[0];
                Image1.ImageUrl = "data:image/jpg;base64," + tb.Rows[0]["FILE"].ToString();
                myImage.ImageUrl = Image1.ImageUrl;
                caption.InnerHtml = tb.Rows[0]["DocumentName"].ToString();

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    void defaultColor()
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        txtFullName.BorderColor = System.Drawing.Color.Empty;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (!CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE)) return;
        RedirectToActionPage(IPC.ACTIONPAGE.REJECT, "&" + SmartPortal.Constant.IPC.ID + "=" + documentId);


    }

    protected void btnApprove_click(object sender, EventArgs e)
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
            object[] searchObject = new object[] { Utility.KillSqlInjection(documentId), Utility.KillSqlInjection(UserId), Utility.KillSqlInjection(ddlStatus.SelectedValue) };
            ds = _service.common("SEMS_BO_DOC_APPROVE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.ApproveSuccessfully;
                ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                btApprove.Enabled = false;
                btReject.Enabled = false;
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

}
