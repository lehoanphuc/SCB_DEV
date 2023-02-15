using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Web;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSAgentBank_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string UserID = "";
    private SmartPortal.SEMS.Common _common;
    public Widgets_SEMSAgentBank_Controls_Widget()
    {
        _common = new SmartPortal.SEMS.Common();

    }


    public string _TITLE
    {
        get { return lblTitleAgentBank.Text; }
        set { lblTitleAgentBank.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();

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

    void defaultColor()
    {
        txtBankCode.BorderColor = System.Drawing.Color.Empty;
        txtBankName.BorderColor = System.Drawing.Color.Empty;
        ddbanktype.BorderColor = System.Drawing.Color.Empty;
        ddStatus.BorderColor = System.Drawing.Color.Empty;
    }

    void enableControlView()
    {
        pnAdd.Enabled = false;
        txtCountryID.Enabled = false;
        txtCityID.Enabled = false;
        txtRegion.Enabled = false;
        btback.Visible = true;
        btsave.Visible = false;
        btClear.Visible = false;
    }

    void enableControlEdit()
    {
        txtBankCode.Enabled = false;
        txtCreateby.Enabled = false;
        txtCreatedate.Enabled = false;
        txtLastmodifydate.Enabled = false;
        txtApproveby.Enabled = false;
        btback.Visible = true;
        btsave.Visible = true;
    }
    void enableControlAdd()
    {
        fcreatedate.Visible = false;
        fcreateby.Visible = false;
        fmodifydate.Visible = false;
        fapproveby.Visible = false;

        btback.Visible = true;
        btsave.Visible = true;
    }
    private void loadBanktype()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("EBA_NATION", "TYPE", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddbanktype.DataSource = ds;
                ddbanktype.DataValueField = "VALUEID";
                ddbanktype.DataTextField = "CAPTION";
                ddbanktype.DataBind();
                ddbanktype.SelectedValue = "D";
            }
        }
    }
    private void loadStatus()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("WAL_AGENT_BANK", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddStatus.DataSource = ds;
                ddStatus.DataValueField = "VALUEID";
                ddStatus.DataTextField = "CAPTION";
                ddStatus.DataBind();
                ddStatus.SelectedValue = "A";
            }
        }
    }

    private void loadAllCombobox()
    {
        loadBanktype();
        loadStatus();
    }

    void loadEditAndViewData()
    {
        string bankID = GetParamsPage(IPC.ID)[0].Trim();
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { Utility.KillSqlInjection(bankID) };
        ds = _common.common("SEMS_AGENT_BANK_VIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        //ds = _service.GetFunction(bankID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable tb = ds.Tables[0];
                txtBankCode.Text = tb.Rows[0]["BankCode"].ToString();
                txtBankName.Text = tb.Rows[0]["BankName"].ToString();
                txtShortName.Text = tb.Rows[0]["ShortName"].ToString();
                txtOthername.Text = tb.Rows[0]["OtherName"].ToString();
                ddbanktype.SelectedValue = tb.Rows[0]["BankType"].ToString();
                txtTaxCode.Text = tb.Rows[0]["TaxCode"].ToString();
                txtBiccode.Text = tb.Rows[0]["BICCode"].ToString();
                txtSwiftcode.Text = tb.Rows[0]["SWIFTCode"].ToString();
                txtPhonenum.Text = tb.Rows[0]["Phone"].ToString();
                txtWebsite.Text = tb.Rows[0]["Website"].ToString();
                txtEmail.Text = tb.Rows[0]["Email"].ToString();
                txtEstablishdate.Text = tb.Rows[0]["estab"].ToString();
                //txtCountryID.Text = tb.Rows[0]["COUNTRY_ID"].ToString();
                ddStatus.SelectedValue = tb.Rows[0]["Status"].ToString();
                //txtRegion.Text = tb.Rows[0]["REGION_ID"].ToString();
                //txtCityID.Text = tb.Rows[0]["CityID"].ToString();
                txtAddressdesc.Text = tb.Rows[0]["Description"].ToString();

                txtCreatedate.Text = tb.Rows[0]["created"].ToString();
                txtLastmodifydate.Text = tb.Rows[0]["lastmo"].ToString();
                txtCreateby.Text = tb.Rows[0]["UserCreated"].ToString();
                txtApproveby.Text = tb.Rows[0]["UserApproved"].ToString();

                txtCountryID.setCountryID(tb.Rows[0]["CountryID"].ToString());
                txtRegion.setRegionID(tb.Rows[0]["RegionID"].ToString());
                txtCityID.setCityID(tb.Rows[0]["CityID"].ToString());

                txtCountryID.Text = tb.Rows[0]["CountryID"].ToString() + " - " + tb.Rows[0]["CountryName"].ToString();
                txtRegion.Text = tb.Rows[0]["RegionID"].ToString() + " - " + tb.Rows[0]["RegionName"].ToString();
                txtCityID.Text = tb.Rows[0]["CityID"].ToString() + " - " + tb.Rows[0]["CityName"].ToString();
            }
        }

    }
    void BindData()
    {
        try
        {
            loadAllCombobox();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    enableControlAdd();
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    loadEditAndViewData();
                    enableControlView();
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    loadEditAndViewData();
                    enableControlEdit();
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public bool IsEmailValid(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    public bool IsUnicode(string input)
    {
        try
        {
            var asciiBytesCount = Encoding.ASCII.GetByteCount(input);
            var unicodBytesCount = Encoding.UTF8.GetByteCount(input);
            return asciiBytesCount != unicodBytesCount;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    private bool checkvalidate()
    {
        #region Validation
        defaultColor();
        if (txtBankCode.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.BankCode + " is not null";
            txtBankCode.BorderColor = System.Drawing.Color.Red;
            txtBankCode.Focus();
            return false;
        }
        if (txtBankCode.Text.Trim().Contains(" "))
        {
            lblError.Text = Resources.labels.BankCode + " is not allowed to have whitespace characters";
            txtBankCode.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (IsUnicode(txtBankCode.Text))
        {
            lblError.Text = Resources.labels.BankCode + " is not allowed to have unicode characters";
            txtBankCode.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (txtBankName.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.BankName + " is not null";
            txtBankName.BorderColor = System.Drawing.Color.Red;
            txtBankName.Focus();
            return false;
        }
        if (ddbanktype.SelectedValue.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.banktype + " is not null";
            ddbanktype.BorderColor = System.Drawing.Color.Red;
            ddbanktype.Focus();
            return false;
        }
        if (!txtEmail.Text.Equals(string.Empty) && IsEmailValid(txtEmail.Text) != true)
        {
            lblError.Text = Resources.labels.email + "  not valid format";
            txtEmail.BorderColor = System.Drawing.Color.Red;
            txtEmail.Focus();
            return false;
        }
        if (ddStatus.SelectedValue.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.status + " is not null";
            ddStatus.BorderColor = System.Drawing.Color.Red;
            ddStatus.Focus();
            return false;
        }
        return true;
        #endregion
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        UserID = HttpContext.Current.Session["userID"].ToString();
        if (!checkvalidate())
            return;
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
                    return;
                try
                {
                    DataSet ds = new DataSet();
                    object[] searchObject = new object[] {
                        Utility.KillSqlInjection(txtBankCode.Text.Trim()),
                        Utility.KillSqlInjection(txtBankName.Text.Trim()),
                        Utility.KillSqlInjection(txtShortName.Text.Trim()),
                        Utility.KillSqlInjection(txtOthername.Text.Trim()),
                        Utility.KillSqlInjection(ddbanktype.SelectedValue),
                        Utility.KillSqlInjection(txtTaxCode.Text.Trim()),
                        Utility.KillSqlInjection(txtBiccode.Text.Trim()),
                        Utility.KillSqlInjection(txtSwiftcode.Text.Trim()),
                        Utility.KillSqlInjection(txtPhonenum.Text.Trim()),
                        Utility.KillSqlInjection(txtWebsite.Text.Trim()),
                        Utility.KillSqlInjection(txtEmail.Text.Trim()),
                        Utility.KillSqlInjection(txtEstablishdate.Text.Trim()),
                        Utility.KillSqlInjection(txtCountryID.getCountryID()),
                        Utility.KillSqlInjection(ddStatus.SelectedValue),
                        Utility.KillSqlInjection(txtRegion.getRegionId()),
                        Utility.KillSqlInjection(txtCityID.getCityID()),
                        Utility.KillSqlInjection(txtAddressdesc.Text.Trim()),
                        Utility.KillSqlInjection(UserID),     //user created
                    };
                    ds = _common.common("SEMS_AGENT_BANK_ADD", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.addsuccessfully;
                        btsave.Enabled = false;
                        pnAdd.Enabled = false;
                        txtCityID.Enabled = false;
                        txtCountryID.Enabled = false;
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
                break;
            case IPC.ACTIONPAGE.EDIT:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                try
                {
                    string bankID = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet ds = new DataSet();
                    object[] searchObject = new object[] {
                        Utility.KillSqlInjection(bankID),
                        Utility.KillSqlInjection(txtBankCode.Text.Trim()),
                        Utility.KillSqlInjection(txtBankName.Text.Trim()),
                        Utility.KillSqlInjection( txtShortName.Text.Trim()),
                        Utility.KillSqlInjection( txtOthername.Text.Trim()),
                        Utility.KillSqlInjection( ddbanktype.SelectedValue),
                        Utility.KillSqlInjection( txtTaxCode.Text.Trim()),
                        Utility.KillSqlInjection( txtBiccode.Text.Trim()),
                        Utility.KillSqlInjection( txtSwiftcode.Text.Trim()),
                        Utility.KillSqlInjection( txtPhonenum.Text.Trim()),
                        Utility.KillSqlInjection( txtWebsite.Text.Trim()),
                        Utility.KillSqlInjection( txtEmail.Text.Trim()),
                        Utility.KillSqlInjection( txtEstablishdate.Text.Trim()),
                        Utility.KillSqlInjection(  txtCountryID.getCountryID()),
                        Utility.KillSqlInjection(  ddStatus.SelectedValue),
                        Utility.KillSqlInjection( txtRegion.getRegionId()),
                        Utility.KillSqlInjection( txtCityID.getCityID()),
                        Utility.KillSqlInjection( txtAddressdesc.Text.Trim()),
                        Utility.KillSqlInjection( UserID),         //user modify
                         };
                    ds = _common.common("SEMS_AGENT_BANK_UP", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            lblError.Text = ds.Tables[0].Rows[0][0].ToString();
                            return;
                        }
                    }
                    if (IPCERRORCODE.Equals("0"))
                    {
                        lblError.Text = Resources.labels.thanhcong;
                        btsave.Enabled = false;
                        pnAdd.Enabled = false;
                        txtCityID.Enabled = false;
                        txtCountryID.Enabled = false;
                    }
                    else
                        lblError.Text = IPCERRORDESC;
                    break;
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                }
                break;
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btclear_Click(object sender, EventArgs e)
    {
        ACTION = GetActionPage();
        if (ACTION == IPC.ACTIONPAGE.ADD)
        {
            txtBankCode.Text = string.Empty;
            txtCreateby.Text = string.Empty;
            txtCreatedate.Text = string.Empty;
            txtLastmodifydate.Text = string.Empty;
            txtApproveby.Text = string.Empty;
        }
        txtBankName.Text = string.Empty;
        txtShortName.Text = string.Empty;
        txtOthername.Text = string.Empty;
        //ddbanktype.SelectedValue = "D";
        txtTaxCode.Text = string.Empty;
        txtBiccode.Text = string.Empty;
        txtSwiftcode.Text = string.Empty;
        txtPhonenum.Text = string.Empty;
        txtWebsite.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtEstablishdate.Text = string.Empty;
        txtCountryID.Text = string.Empty;
        //ddStatus.SelectedValue = "A";
        txtRegion.Text = string.Empty;
        txtCityID.Text = string.Empty;
        txtCountryID.Enabled = true;
        txtCityID.Enabled = true;
        txtAddressdesc.Text = string.Empty;
        defaultColor();
        pnAdd.Enabled = true;
        lblError.Text = string.Empty;
        btsave.Enabled = true;
    }
    void SetParaInsert(Dictionary<object, object> paraInsert) //not used yet - for log
    {
        string UserID = HttpContext.Current.Session["userID"].ToString();
        paraInsert.Add("BankCode", txtBankCode.Text);
        paraInsert.Add("BankName", txtBankName.Text);
        paraInsert.Add("ShortName", txtShortName.Text);
        paraInsert.Add("OtherName", txtOthername.Text);
        paraInsert.Add("BankType", ddbanktype.SelectedValue);
        paraInsert.Add("TaxCode", txtTaxCode.Text);
        paraInsert.Add("BICCode", txtBiccode.Text);
        paraInsert.Add("SWIFTCode", txtSwiftcode.Text);
        paraInsert.Add("Phone", txtPhonenum.Text);
        paraInsert.Add("Website", txtWebsite.Text);
        paraInsert.Add("Email", txtEmail.Text);
        paraInsert.Add("EstablishedDate", txtEstablishdate.Text);
        paraInsert.Add("CountryID", txtCountryID.getCountryID());
        paraInsert.Add("Status", ddStatus.SelectedValue);
        paraInsert.Add("RegionID", txtRegion.getRegionId());
        paraInsert.Add("CityID", txtCityID.getCityID());
        paraInsert.Add("Description", txtAddressdesc.Text);
        paraInsert.Add("UserCreated", UserID); //user created
        //SEMS_AGENT_BANK_ADD|5|BankCode|BankName|ShortName|OtherName|BankType|TaxCode|BICCode|SWIFTCode|Phone|Website|Email|EstablishedDate|CountryID|Status|RegionID|CityID|Description|UserCreated
    }
    void SetParaUpdate(Dictionary<object, object> paraUpdate) //not used yet - for log
    {
        string AgentBankID = GetParamsPage(IPC.ID)[0].Trim();
        string UserID = HttpContext.Current.Session["userID"].ToString();
        paraUpdate.Add("BankID", AgentBankID);
        paraUpdate.Add("BankCode", txtBankCode.Text);
        paraUpdate.Add("BankName", txtBankName.Text);
        paraUpdate.Add("ShortName", txtShortName.Text);
        paraUpdate.Add("OtherName", txtOthername.Text);
        paraUpdate.Add("BankType", ddbanktype.SelectedValue);
        paraUpdate.Add("TaxCode", txtTaxCode.Text);
        paraUpdate.Add("BICCode", txtBiccode.Text);
        paraUpdate.Add("SWIFTCode", txtSwiftcode.Text);
        paraUpdate.Add("Phone", txtPhonenum.Text);
        paraUpdate.Add("Website", txtWebsite.Text);
        paraUpdate.Add("Email", txtEmail.Text);
        paraUpdate.Add("EstablishedDate", txtEstablishdate.Text);
        paraUpdate.Add("CountryID", txtCountryID.getCountryID());
        paraUpdate.Add("Status", ddStatus.SelectedValue);
        paraUpdate.Add("RegionID", txtRegion.getRegionId());
        paraUpdate.Add("CityID", txtCityID.getCityID());
        paraUpdate.Add("Description", txtAddressdesc.Text);
        paraUpdate.Add("UserModified", UserID); //user modified
        //SEMS_AGENT_BANK_UP|5|BankID|BankCode|BankName|ShortName|OtherName|BankType|TaxCode|BICCode|SWIFTCode|Phone|Website|Email|EstablishedDate|CountryID|Status|RegionID|CityID|Description|UserModified
    }

}
