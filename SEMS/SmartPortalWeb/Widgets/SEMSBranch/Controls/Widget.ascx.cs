using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using OfficeOpenXml.FormulaParsing.Utilities;
using Resources;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSBranch_Controls_Widget : WidgetBase
{
    private string ACTION = "";
    private string IPCERRORCODE = "";
    private string IPCERRORDESC = "";

    public string _TITLE
    {
        set { lblTitleBranch.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (!IsPostBack) BindData();
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    #region Load To Drop Down List

    public void LoadAllDropDownList()
    {
        LoadCountryDdl();
        LoadCityDdl(ddlCountry.SelectedValue);
        LoadDistDdl(ddlcity.SelectedValue);
        LoadRegion();
    }

    private void LoadRegion()
    {
        try
        {
            DataSet dtRegion = new DataSet();
            dtRegion = new SmartPortal.SEMS.Partner().GetRegionALL(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlRegion.DataSource = dtRegion;
                ddlRegion.DataTextField = "RegionName";
                ddlRegion.DataValueField = "RegionID";
                ddlRegion.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
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
    private void LoadCountryDdl()
    {
        try
        {
            DataTable countryTable = new Branch().GetAllCountry(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (countryTable.Rows.Count != 0)
            {
                ddlCountry.DataSource = countryTable;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("Please Choose Country!", ""));
                ddlCountry.SelectedIndex = 0;
            }
            else
            {
                ddlCountry.Items.Clear();
                ddlCountry.Items.Add(new ListItem("", ""));
            }
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }


    private void LoadCityDdl(string countryId)
    {
        try
        {
            DataTable cityTable = new Branch().GetAllCityOfCountry(countryId, ref IPCERRORCODE,
                ref IPCERRORDESC).Tables[0];
            if (cityTable.Rows.Count > 0)
            {
                ddlcity.DataSource = cityTable;
                ddlcity.DataTextField = "CityName";
                ddlcity.DataValueField = "CityCode";
                ddlcity.DataBind();
                ddlcity.Items.Insert(0, new ListItem("Please Choose City!", ""));
                ddlcity.SelectedIndex = 0;
            }
            else
            {
                ddlcity.Items.Clear();
                ddlcity.Items.Add(new ListItem("", ""));
                ddlDist.Items.Clear();
                ddlDist.Items.Add(new ListItem("", ""));
            }
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    private void LoadDistDdl(string cityCode)
    {
        try
        {
            DataTable distTable = new Branch().GetAllDistAndBranchOfCity(cityCode,
                ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (distTable.Rows.Count != 0)
            {
                ddlDist.DataSource = distTable;
                ddlDist.DataTextField = "DISTNAME";
                ddlDist.DataValueField = "DISTCODE";
                ddlDist.DataBind();
                ddlDist.Items.Insert(0, new ListItem("Please Choose District!", ""));
                ddlDist.SelectedIndex = 0;
            }
            else
            {
                ddlDist.Items.Clear();
                ddlDist.Items.Add(new ListItem("", ""));
            }
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    #endregion

    #region Load Data

    private void BindData()
    {
        try
        {
            txtTimeOpen.Text = "08:00:00";
            txtTimeClose.Text = "16:00:00";
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    LoadAllDropDownList();
                    break;
                default:
                    btnClear.Enabled = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();

                    DataTable branchTable = new DataTable();
                    branchTable =
                        new Branch().GetBranchDetailsByID(ID, ref IPCERRORCODE, ref IPCERRORDESC)
                            .Tables[0];
                    if (branchTable.Rows.Count != 0)
                    {
                        txtmacn.Text = branchTable.Rows[0]["BRANCHID"].ToString();
                        txttencn.Text = branchTable.Rows[0]["BRANCHNAME"].ToString();
                        txtLatitude.Text = branchTable.Rows[0]["POSITIONX"].ToString();
                        txtLongitude.Text = branchTable.Rows[0]["POSITIONY"].ToString();

                        #region Load dropdown list sequentially (do not modify if dont know other way)
                        LoadCountryDdl();
                        ddlCountry.SelectedValue = branchTable.Rows[0]["COUNTRYID"].ToString();
                        LoadCityDdl(ddlCountry.SelectedValue);
                        ddlcity.SelectedValue = branchTable.Rows[0]["CITYCODE"].ToString();
                        LoadDistDdl(ddlcity.SelectedValue);
                        ddlDist.SelectedValue = branchTable.Rows[0]["DISTCODE"].ToString();
                        #endregion

                        txtTaxCode.Text = branchTable.Rows[0]["TAXCODE"].ToString();
                        txtBicCode.Text = branchTable.Rows[0]["BICCODE"].ToString();
                        txtSwiftCode.Text = branchTable.Rows[0]["SWIFTCODE"].ToString();
                        txtEmail.Text = branchTable.Rows[0]["EMAIL"].ToString();
                        txtTimeOpen.Text = branchTable.Rows[0]["TIMEOPEN"].ToString();
                        txtTimeClose.Text = branchTable.Rows[0]["TIMECLOSE"].ToString();
                        txtphone.Text = branchTable.Rows[0]["PHONE"].ToString();
                        txtaddress.Text = branchTable.Rows[0]["ADDRESS"].ToString();
                        LoadRegion();
                        if (!string.IsNullOrEmpty(branchTable.Rows[0]["RegionID"].ToString()))
                        {
                            ddlRegion.SelectedValue = branchTable.Rows[0]["RegionID"].ToString();
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
                    txtmacn.Enabled = false;
                    btnClear.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    #endregion

    #region Event

    protected void btsave_Click(object sender, EventArgs e)
    {
        string Branchid = Utility.KillSqlInjection(txtmacn.Text.Trim());
        string Branchname = Utility.KillSqlInjection(txttencn.Text.Trim());
        string address = Utility.KillSqlInjection(txtaddress.Text.Trim());
        string phone = Utility.KillSqlInjection(txtphone.Text.Trim());
        string PosX = Utility.KillSqlInjection(txtLatitude.Text.Trim());
        string PosY = Utility.KillSqlInjection(txtLongitude.Text.Trim());
        string city = Utility.KillSqlInjection(ddlcity.SelectedValue.Trim());
        string dist = Utility.KillSqlInjection(ddlDist.SelectedValue.Trim());
        string country = Utility.KillSqlInjection(ddlCountry.SelectedValue.Trim());
        string tax = Utility.KillSqlInjection(txtTaxCode.Text.Trim());
        string bicCode = Utility.KillSqlInjection(txtBicCode.Text.Trim());
        string swiftCode = Utility.KillSqlInjection(txtSwiftCode.Text.Trim());
        string email = Utility.KillSqlInjection(txtEmail.Text.Trim());
        string timeOpen = Utility.KillSqlInjection(txtTimeOpen.Text.Trim());
        string timeClose = Utility.KillSqlInjection(txtTimeClose.Text.Trim());
        string userCreate = Utility.KillSqlInjection(Session["username"].ToString());
        string regionid = Utility.KillSqlInjection(ddlRegion.SelectedValue.Trim());
        #region Validations

        try
        {
            int i = Convert.ToInt32(Branchid);

        }
        catch (Exception ex)
        {
            lblError.Text = labels.branchidchigomcackytutu0den9;
            return;
        }
        if (ACTION == IPC.ACTIONPAGE.ADD)
        {
            DataTable branchTable =
                new Branch().GetBranchDetailsByID(Branchid, ref IPCERRORCODE,
                    ref IPCERRORDESC).Tables[0];
            if (branchTable.Rows.Count != 0)
            {
                lblError.Text = labels.machihnhanhdatontai;
                return;
            }
        }

        if (ddlCountry.SelectedValue == "0")
        {
            lblError.Text = labels.selectcountryrequired;
            return;
        }

        if (ddlcity.SelectedValue == "0")
        {
            lblError.Text = labels.selectcityrequired;
            return;
        }


        if (timeOpen == string.Empty)
        {
            lblError.Text = labels.timeopenrequired;
            return;
        }

        if (timeClose == string.Empty)
        {
            lblError.Text = labels.timecloserequired;
            return;
        }

        if (Convert.ToDateTime(timeOpen) >= Convert.ToDateTime(timeClose))
        {
            lblError.Text = labels.timeopenmustsmallerthantimeclose;
            return;
        }

        #endregion

        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:

                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                try
                {
                    new Branch().InsertBranch(Branchid, Branchname, address, city, dist, regionid, phone,
                        "Y", PosX, PosY, tax, bicCode, swiftCode, country, timeOpen,
                        timeClose, email, userCreate, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = labels.themchinhanhthanhcong;
                        pnAdd.Enabled = false;
                        btsave.Enabled = false;
                    }
                    else
                    {
                        throw new IPCException(IPCERRORCODE);
                    }
                }
                catch (IPCException IPCex)
                {
                    Log.RaiseError(IPCex.ToString(), GetType().BaseType.Name,
                        MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
                    Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
                }
                catch (Exception ex)
                {
                    Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                        GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name,
                        ex.ToString(), Request.Url.Query);
                    Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                        Request.Url.Query);
                }

                break;
            case IPC.ACTIONPAGE.EDIT:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                try
                {
                    new Branch().UpdateBranch(Branchid, Branchname, address, city, dist, phone,
                        "Y", PosX, PosY, regionid, tax, bicCode, swiftCode, country, timeOpen,
                        timeClose, email, userCreate, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = labels.suachinhanhthanhcong;
                        pnAdd.Enabled = false;
                        btsave.Visible = false;
                    }
                    else
                    {
                        throw new IPCException(IPCERRORCODE);
                    }
                }
                catch (IPCException IPCex)
                {
                    Log.RaiseError(IPCex.ToString(), GetType().BaseType.Name,
                        MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
                    Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
                }
                catch (Exception ex)
                {
                    Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                        GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name,
                        ex.ToString(), Request.Url.Query);
                    Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                        Request.Url.Query);
                }

                break;
        }
    }

    protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDistDdl(ddlcity.SelectedValue);
    }

    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void ddlCountry_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCityDdl(ddlCountry.SelectedValue);
    }


    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        pnAdd.Enabled = true;
        btsave.Enabled = true;
        txtaddress.Text = string.Empty;
        txtmacn.Text = string.Empty;
        txtphone.Text = string.Empty;
        txttencn.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtBicCode.Text = string.Empty;
        txtLatitude.Text = string.Empty;
        txtLongitude.Text = string.Empty;
        txtSwiftCode.Text = string.Empty;
        txtTaxCode.Text = string.Empty;
        txtTimeOpen.Text = "08:00:00";
        txtTimeClose.Text = "16:00:00";
        ddlCountry.SelectedIndex = 0;
        LoadAllDropDownList();
    }

    #endregion
}