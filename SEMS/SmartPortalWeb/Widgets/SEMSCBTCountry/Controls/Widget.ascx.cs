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

public partial class Widgets_SEMSCBTCountry_Controls_Widget : WidgetBase
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
            
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    Clear.Enabled = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet dsCountry =
                        new SmartPortal.SEMS.Country().GetCBTCountryDetailsByID(ID, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsCountry != null)
                    {
                        DataTable countryTable = dsCountry.Tables[0];
                        if (countryTable.Rows.Count != 0)
                        {
                            txtCountryID.Text = ID;
                            txtCountryName.Text = countryTable.Rows[0]["CountryName"].ToString();
                            txtNostro.Text = countryTable.Rows[0]["Nostro"].ToString();
                            ddlStatus.Text = countryTable.Rows[0]["Status"].ToString();
                            txtDescription.Text = countryTable.Rows[0]["Description"].ToString();
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
                    btsave.Text = Resources.labels.save;
                    break;
                case IPC.ACTIONPAGE.ADD:
                    txtCountryID.Enabled = false;
                    btsave.Text = Resources.labels.add;
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
            string countryID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCountryID.Text.Trim());
            string Countryname = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCountryName.Text.Trim());
            string Countrycode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCountryCode.Text.Trim());
            string Nostro = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtNostro.Text.Trim());
            string Status = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlStatus.SelectedValue.Trim());
            string Description = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDescription.Text.Trim());
            string Usercreate = Session["userName"].ToString().Trim();
            string Usermodified = Session["userName"].ToString().Trim();

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    try
                    {
                        new SmartPortal.SEMS.Country().InsertCBTCOUNTRY(Countryname, Countrycode, Nostro, Status, Description, Usercreate, ref IPCERRORCODE, ref IPCERRORDESC);
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
                        new SmartPortal.SEMS.Country().UpdateCBTCountry(countryID, Countryname, Countrycode, Nostro,  Status, Description, Usermodified, ref IPCERRORCODE, ref IPCERRORDESC);
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