using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.IB;

public partial class Widgets_SEMSCountry_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    public string _TITLE
    {
        get { return lblTitleCountry.Text; }
        set { lblTitleCountry.Text = value; }
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
    void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.Country().GetCurrencyIDbyStatusA(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlCurrency.DataSource = ds;
                ddlCurrency.DataTextField = "CCYID";
                ddlCurrency.DataValueField = "CCYID";
                ddlCurrency.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }

            DataSet dts = new DataSet();
            dts = new SmartPortal.SEMS.Country().GetAllLanguage(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlLanguage.DataSource = dts;
                ddlLanguage.DataTextField = "LangName";
                ddlLanguage.DataValueField = "LangID";
                ddlLanguage.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    Clear.Enabled = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet dsCountry =
                        new SmartPortal.SEMS.Country().GetCountryDetailsByID(ID, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsCountry != null)
                    {
                        DataTable countryTable = dsCountry.Tables[0];
                        if (countryTable.Rows.Count != 0)
                        {
                            txtCountryID.Text = ID;
                            txtCountryName.Text = countryTable.Rows[0]["CountryName"].ToString();
                            txtM_CountryName.Text = countryTable.Rows[0]["MCountryName"].ToString();
                            txtCapital.Text = countryTable.Rows[0]["CapitalName"].ToString();
                            ddlCurrency.SelectedValue = countryTable.Rows[0]["CurrencyID"].ToString();
                            ddlTimeZone.Text = countryTable.Rows[0]["TimeZone"].ToString();
                            ddlLanguage.SelectedValue = countryTable.Rows[0]["Language"].ToString();
                            ddlStatus.Text = countryTable.Rows[0]["Status"].ToString();
                            txtContryPhone.Text = countryTable.Rows[0]["PhoneCountryCode"].ToString();
                            txtDescription.Text = countryTable.Rows[0]["Description"].ToString();
                            txtOrder.Text = countryTable.Rows[0]["Order"].ToString();
                            txtCountryCode.Text = countryTable.Rows[0]["CountryCode"].ToString();

                        }
                    }
                    break;
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    btsave.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtCountryID.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            //CountryID, Countryname, MCountryname, Countrycode, Capital, Currency, Language, Status, Ordre, Description, Timezone, Usercreate, Phonecode
            string CountryID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCountryID.Text.Trim());
            string Countryname = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCountryName.Text.Trim());
            string MCountryname = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtM_CountryName.Text.Trim());
            string Countrycode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCountryCode.Text.Trim());
            string Capital = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCapital.Text.Trim());
            string Currency = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCurrency.SelectedValue.Trim());
            string Language = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlLanguage.SelectedValue.Trim());
            string Status = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlStatus.SelectedValue.Trim());
            double order = double.Parse(SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtOrder.Text.Trim()));
            string Description = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDescription.Text.Trim());
            string Timezone = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTimeZone.SelectedValue.Trim());
            string Usercreate = Session["userName"].ToString().Trim();
            string Phonecode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContryPhone.Text.Trim());
            string Usermodified = Session["userName"].ToString().Trim();

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    try
                    {
                        int i = Convert.ToInt32(CountryID);
                        new SmartPortal.SEMS.Country().InsertCountry(CountryID, Countryname, MCountryname, Capital, Countrycode, Currency, Language, Status, order, Description,  Usercreate, Timezone, Phonecode, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.addCountrysuccess;
                            pnAdd.Enabled = false;
                            btsave.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
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
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    try
                    {
                        int i = Convert.ToInt32(CountryID);
                        new SmartPortal.SEMS.Country().UpdateCountry(CountryID, Countryname, MCountryname, Countrycode, Capital, Currency, Language, Status, order, Description, Usermodified, Timezone,  Phonecode, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.editcountrysuccess;
                            pnAdd.Enabled = false;
                            btsave.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
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
                    break;
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void Clear_Click(object sender, EventArgs e)
    {
        lblError.Text = String.Empty;
        ClearInputs(Page.Controls);
        pnAdd.Enabled = true;
        btsave.Enabled = true;
    }
    void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            ClearInputs(ctrl.Controls);
        }
    }
}