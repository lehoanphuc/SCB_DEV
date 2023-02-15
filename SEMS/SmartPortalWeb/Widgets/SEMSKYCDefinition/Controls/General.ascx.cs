using System;
using SmartPortal.Constant;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSKYCDefinition_Controls_General : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            ACTION = GetActionPage();
            ViewState["UserID"] = HttpContext.Current.Session["userID"].ToString();
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                setControldefault();
                BindData();
                loadCombobox_Status();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

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
            ds = _service.GetValueList("STATUS", "WAL_KYC", ref IPCERRORCODE, ref IPCERRORDESC);
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

    public void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    string KycId = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet ds = new DataSet();
                    object[] searchObject = new object[] { Utility.KillSqlInjection(KycId) };
                    ds = _service.common("SEMS_WAL_KYC_VIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count < 1) return;
                            DataTable dataTable = ds.Tables[0];
                            txtKycCode.Text = dataTable.Rows[0]["KycCode"].ToString();
                            txtKycName.Text = dataTable.Rows[0]["KycName"].ToString();
                            txtCreateDate.Text = dataTable.Rows[0]["datecrea"].ToString();
                            ddlStatus.SelectedValue = dataTable.Rows[0]["Status"].ToString();
                            txtApprovedBy.Text = dataTable.Rows[0]["UserApproved"].ToString();
                            txtCreatedBy.Text = dataTable.Rows[0]["UserCreated"].ToString();
                            txtLastModifiedDate.Text = dataTable.Rows[0]["lastmo"].ToString();

                        }
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
    public void setcontrolDisible()
    {
        txtKycCode.Enabled = false;
        txtKycName.Enabled = false;
        ddlStatus.Enabled = false;
    }

    public void setControldefault()
    {
        txtKycCode.BorderColor = System.Drawing.Color.Empty;
        txtKycCode.Focus();
        txtKycName.BorderColor = System.Drawing.Color.Empty;
        txtKycName.Focus();
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                createddate.Visible = false;
                PnAdd.Visible = false;
                break;
            case IPC.ACTIONPAGE.EDIT:
                txtKycCode.Enabled = false;
                txtCreateDate.Enabled = false;
                txtApprovedBy.Enabled = false;
                txtCreatedBy.Enabled = false;
                txtLastModifiedDate.Enabled = false;
                break;
            case IPC.ACTIONPAGE.DETAILS:
                pnGeneral.Enabled = false;
                btSave.Visible = false;
                btClear.Visible = false;
                break;

        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        setControldefault();
        try
        {
            #region validatiton
                if (txtKycCode.Text.Equals(string.Empty))
                {
                    lblError.Text = Resources.labels.KycCode + " is not null";
                    txtKycCode.BorderColor = System.Drawing.Color.Red;
                    txtKycCode.Focus();
                    return;
                }

                if (!Utility.CheckSpecialCharacters(txtKycCode.Text))
                {
                    lblError.Text = Resources.labels.KycCode + Resources.labels.ErrorSpeacialCharacters;
                    txtKycCode.BorderColor = System.Drawing.Color.Red;
                    txtKycCode.Focus();
                    return;
                }

                if (txtKycName.Text.Equals(string.Empty))
                {
                    lblError.Text = Resources.labels.Kycname + " is not null";
                    txtKycName.BorderColor = System.Drawing.Color.Red;
                    txtKycName.Focus();
                    return;
                }
                if (!Utility.CheckSpecialCharacters(txtKycName.Text))
                {
                    lblError.Text = Resources.labels.Kycname + Resources.labels.ErrorSpeacialCharacters;
                    txtKycName.BorderColor = System.Drawing.Color.Red;
                    txtKycName.Focus();
                    return;
                }
            #endregion

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.EDIT:
                    string KycId = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet ds = new DataSet();
                    object[] _object = new object[] { Utility.KillSqlInjection(KycId), Utility.KillSqlInjection(txtKycCode.Text.Trim()), Utility.KillSqlInjection(txtKycName.Text.Trim()),
                        Utility.KillSqlInjection (ddlStatus.SelectedValue), Utility.KillSqlInjection (ViewState["UserID"].ToString())};
                    ds = _service.common("SEMS_WAL_KYC_UPDATE", _object, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        setcontrolDisible();
                        lblError.Text = Resources.labels.updatesuccessfully;
                        btSave.Enabled = false;
                        BindData();
                       
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    break;
                default:

                    DataSet ds1 = new DataSet();
                    object[] _object1 = new object[] { Utility.KillSqlInjection(txtKycCode.Text.Trim()), Utility.KillSqlInjection(txtKycName.Text.Trim()),
                        Utility.KillSqlInjection (ddlStatus.SelectedValue), Utility.KillSqlInjection (ViewState["UserID"].ToString()) };
                    ds = _service.common("SEMS_WAL_KYC_INSERT", _object1, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        setcontrolDisible();
                        lblError.Text = Resources.labels.insertsucessfull;
                        btSave.Enabled = false;
                        BindData();
                        try
                        {
                            Cache.Remove("Wallet_KYCLevel");
                        }
                        catch (Exception ex)
                        {
                            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                        }
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
            lblError.Text = ex.Message;
        }

    }
    protected void btCancel_Click(object sender, EventArgs e)
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
    protected void btClear_Click(object sender, EventArgs e)
    {
        ACTION = GetActionPage();
        if (ACTION == "ADD")
        {
            txtKycCode.Text = String.Empty;
            txtKycCode.Enabled = true;
        }
        if (ACTION == "EDIT")
        {
            txtKycCode.Enabled = false;
        }
        txtKycCode.BorderColor = System.Drawing.Color.Empty;
        txtKycName.BorderColor = System.Drawing.Color.Empty;
        txtKycName.Text = String.Empty; 
        btSave.Enabled = true;
        txtKycName.Enabled = true;
        ddlStatus.Enabled = true;
    }
}