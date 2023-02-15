using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using System;
using System.Collections.Generic;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                loadCombobox();
                setControldefault();
                BindData();
            }
            //BindData();
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
    private void loadCombobox_PaperType()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { string.Empty };
            ds = _service.common("SEMS_BO_GET_INFO_KYC", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlPaperType.DataSource = ds;
                        ddlPaperType.DataValueField = "KYCID";
                        ddlPaperType.DataTextField = "KYCNAME";
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
    private void loadCombobox_Gender()
    {
        try
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
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_Nation()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] loadNation = new object[] { string.Empty, string.Empty, null, null };
            ds = _service.common("WAL_BO_GET_NATION", loadNation, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlNationality.DataSource = ds;
                    ddlNationality.DataValueField = "NationCode";
                    ddlNationality.DataTextField = "NationName";
                    ddlNationality.DataBind();
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
        loadCombobox_PaperType();
        loadCombobox_WalletLevel();
        loadCombobox_Gender();
        loadCombobox_Nation();
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

                            txtFullName.Text = dataTable.Rows[0]["FULLNAME"].ToString();
                            txtFirstName.Text = dataTable.Rows[0]["FIRSTNAME"].ToString();
                            txtMiddleName.Text = dataTable.Rows[0]["MIDDLENAME"].ToString();
                            txtLastname.Text = dataTable.Rows[0]["LASTNAME"].ToString();
                            ddlPaperType.SelectedValue = dataTable.Rows[0]["KYCID"].ToString();
                            txtPaperNumber.Text = dataTable.Rows[0]["LICENSEID"].ToString();
                            txtIssuedate.Text = dataTable.Rows[0]["IssueDateFormat"].ToString();

                            txtExpirydate.Text = dataTable.Rows[0]["ExpireDateFormat"].ToString();
                            txtBirsthday.Text = dataTable.Rows[0]["DOBFormat"].ToString();
                            ddlGender.SelectedValue = dataTable.Rows[0]["SEX"].ToString();
                            ddlNationality.SelectedValue = dataTable.Rows[0]["NATION"].ToString().Trim();
                            txtAddress.Text = dataTable.Rows[0]["ADDRRESIDENT"].ToString();
                            txtEmail.Text = dataTable.Rows[0]["Email"].ToString();
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

    public void setControldefault()
    {
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                break;
            case IPC.ACTIONPAGE.EDIT:
                txtConsumerCode.Enabled = false;
                txtDateCreate.Enabled = false;
                txtPhoneNumber.Enabled = false;
                txtLastModifiedate.Enabled = false;
                ddlStatus.Enabled = false;
                txtCreateBy.Enabled = false;
                txtApproveBy.Enabled = false;
                ddlWalletLevel.Enabled = false;
                txtFullName.Enabled = false;
                ddlPaperType.Enabled = false;
                txtPaperNumber.Enabled = false;
                txtEmail.Enabled = false;
                break;
            case IPC.ACTIONPAGE.DETAILS:
                pnGeneral.Enabled = false;
                btSaveGeneral.Enabled = false;
                btnClear.Visible = false;
                break;

        }
    }

    void setEnable()
    {
        txtFirstName.Enabled = false;
        txtMiddleName.Enabled = false;
        txtLastname.Enabled = false;
        txtLastname.Enabled = false;
        txtIssuedate.Enabled = false;
        txtExpirydate.Enabled = false;
        txtBirsthday.Enabled = false;
        ddlGender.Enabled = false;
        ddlNationality.Enabled = false;
        txtAddress.Enabled = false;
    }

    void setColorDefault()
    {
        txtExpirydate.BorderColor = System.Drawing.Color.Empty;
        txtIssuedate.BorderColor = System.Drawing.Color.Empty;
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
       
        try
        {
            setColorDefault();
            if (txtIssuedate.Text.ToString().Equals("")) txtIssuedate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (txtExpirydate.Text.ToString().Equals("")) txtExpirydate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            DateTime IssueDate =  DateTime.ParseExact(txtIssuedate.Text.ToString(), "dd/MM/yyyy", null);
            DateTime ExpiryDate = DateTime.ParseExact(txtExpirydate.Text.ToString(), "dd/MM/yyyy", null);
            DateTime DOB = DateTime.ParseExact(txtExpirydate.Text.ToString(), "dd/MM/yyyy", null);
            #region validatiton
            if (!Utility.CheckSpecialCharacters(txtIssuedate.Text.Trim()))
            {
                lblError.Text = Resources.labels.IssueDate + Resources.labels.ErrorSpeacialCharacters;
                txtIssuedate.BorderColor = System.Drawing.Color.Red;
                txtIssuedate.Focus();
                return;
            }

            if (!Utility.CheckSpecialCharacters(txtExpirydate.Text.Trim()))
            {
                lblError.Text = Resources.labels.ExpiryDate + Resources.labels.ErrorSpeacialCharacters;
                txtExpirydate.BorderColor = System.Drawing.Color.Red;
                txtExpirydate.Focus();
                return;
            }
            if (IssueDate >= ExpiryDate)
            {
                lblError.Text = string.Format(" {0} must be greater than {1}", Resources.labels.ExpiryDate, Resources.labels.IssueDate);
                txtExpirydate.BorderColor = System.Drawing.Color.Red;
                txtIssuedate.BorderColor = System.Drawing.Color.Red;
                txtExpirydate.Focus();
                return;
            }
            #endregion

             DataSet ds = new DataSet();
            Dictionary<object, object> _para = new Dictionary<object, object>();

            _para.Add("CUSTID", Utility.KillSqlInjection(ID));
            _para.Add("USERMODIFIED", HttpContext.Current.Session["userID"].ToString());
            _para.Add("FIRSTNAME", Utility.KillSqlInjection(txtFirstName.Text));
            _para.Add("MIDDLENAME", Utility.KillSqlInjection(txtMiddleName.Text));
            _para.Add("LASTNAME", Utility.KillSqlInjection(txtLastname.Text));
            _para.Add("ISSUEDATE", IssueDate);
            _para.Add("EXPIRYDATE", ExpiryDate);
            _para.Add("DOB", DOB);
            _para.Add("GENDER", Utility.KillSqlInjection(ddlGender.SelectedValue));
            _para.Add("NATION", Utility.KillSqlInjection(ddlNationality.SelectedValue));
            _para.Add("ADDRESS", Utility.KillSqlInjection(txtAddress.Text));
            ds = _service.CallStore("SEMS_CON_PROFILE_UPD", _para, "Update information Consumer Profile", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.updatesuccessfully;
                txtLastModifiedate.Text = ds.Tables[0].Rows[0]["DateModified"].ToString();
                //BindData();
                setEnable();
                btSaveGeneral.Enabled = false;
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        setColorDefault();
        txtFirstName.Text = string.Empty;
        txtMiddleName.Text = string.Empty;
        txtLastname.Text = string.Empty;
        txtIssuedate.Text = string.Empty;
        txtExpirydate.Text = string.Empty;
        txtBirsthday.Text = string.Empty;
        btSaveGeneral.Enabled = true;
        txtFirstName.Enabled =true;
        txtMiddleName.Enabled = true;
        txtLastname.Enabled = true;
        txtIssuedate.Enabled = true;
        txtExpirydate.Enabled = true;
        txtBirsthday.Enabled = true;
        lblError.Text = string.Empty;
    }
}