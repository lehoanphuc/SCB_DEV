using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using Resources;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.SEMS;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_SEMSATMSearch_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private const string DEFOPENTIME = "00:00:00";
    private const string DEFCLOSETIME = "23:00:00";


    public string ATMHeader
    {
        get { return lblATMHeader.Text; }
        set { lblATMHeader.Text = value; }
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    LoadAllDropDownList();
                    break;
                default:

                    #region Lấy thông tin máy ATM

                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataTable atmTable = new ATM().GetAtmById(ID, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                    if (IPCERRORCODE == "0")
                    {
                        if (atmTable.Rows.Count > 0)
                        {
                            LoadCountryDdl();
                            ddlCountry.SelectedValue = atmTable.Rows[0]["COUNTRYID"].ToString();
                            LoadCityDdl(ddlCountry.SelectedValue);
                            ddlCity.SelectedValue = atmTable.Rows[0]["CITYCODE"].ToString();
                            LoadDistDdl(ddlCity.SelectedValue);
                            ddlDistrict.SelectedValue = atmTable.Rows[0]["DISTCODE"].ToString();

                            DataTable dt = new Branch().GetBranchByDistAndCityCode(ddlCity.SelectedValue,
                                ddlDistrict.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                            ddlBranchId.DataSource = dt;
                            ddlBranchId.DataTextField = "BranchName";
                            ddlBranchId.DataValueField = "BranchId";
                            ddlBranchId.DataBind();

                            ddlBranchId.SelectedValue = atmTable.Rows[0]["BranchID"].ToString();
                            txtATMID.Text = atmTable.Rows[0]["ATMID"].ToString();
                            txtATMCode.Text = atmTable.Rows[0]["ATMCODE"].ToString();
                            txtAddress.Text = atmTable.Rows[0]["ADDRESS"].ToString();
                            txtPosX.Text = atmTable.Rows[0]["POSITIONX"].ToString();
                            txtPosY.Text = atmTable.Rows[0]["POSITIONY"].ToString();
                            txtDescription.Text = atmTable.Rows[0]["DESCRIPTION"].ToString();
                        }
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }

                    break;

                #endregion
            }

            #region Enable/Disable theo Action

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    Panel1.Enabled = false;
                    btsave.Visible = false;
                    btnClear.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtATMID.Enabled = false;
                    btnClear.Enabled = false;
                    break;
            }

            #endregion
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }


    #region Load To Drop Down List

    public void LoadAllDropDownList()
    {
        LoadCountryDdl();
        LoadCityDdl(ddlCountry.SelectedValue);
        LoadDistDdl(ddlCity.SelectedValue);
        LoadBranchDdl(ddlCity.SelectedValue, ddlDistrict.SelectedValue);
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
                ddlCity.Items.Clear();
                ddlCity.Items.Add(new ListItem("", ""));
                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Add(new ListItem("", ""));
                ddlBranchId.Items.Clear();
                ddlBranchId.Items.Add(new ListItem("", ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }


    private void LoadCityDdl(string countryId)
    {
        try
        {
            DataTable cityTable = new Branch().GetAllCityOfCountry(countryId, ref IPCERRORCODE,
                ref IPCERRORDESC).Tables[0];
            if (cityTable.Rows.Count != 0)
            {
                ddlCity.DataSource = cityTable;
                ddlCity.DataTextField = "CityName";
                ddlCity.DataValueField = "CityCode";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("Please Choose City!", "0"));
                ddlCity.SelectedIndex = 0;
            }
            else
            {
                ddlCity.Items.Clear();
                ddlCity.Items.Add(new ListItem("", "0"));
                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Add(new ListItem("", ""));
                ddlBranchId.Items.Clear();
                ddlBranchId.Items.Add(new ListItem("", ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
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
                ddlDistrict.DataSource = distTable;
                ddlDistrict.DataTextField = "DISTNAME";
                ddlDistrict.DataValueField = "DISTCODE";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("(Optional) Select District", ""));
                ddlDistrict.SelectedIndex = 0;
            }
            else
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Add(new ListItem("", ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    private void LoadBranchDdl(string cityCode, string distCode)
    {
        try
        {
            DataTable branchTable = new Branch()
                .GetBranchByDistAndCityCode(cityCode, distCode, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (branchTable.Rows.Count > 0)
            {
                ddlBranchId.DataSource = branchTable;
                ddlBranchId.DataTextField = "BranchName";
                ddlBranchId.DataValueField = "BranchID";
                ddlBranchId.DataBind();
                ddlBranchId.Items.Insert(0, new ListItem("Please Choose Branch!", "0"));
                ddlBranchId.SelectedIndex = 0;
            }
            else
            {
                ddlBranchId.Items.Clear();
                ddlBranchId.Items.Add(new ListItem("", "0"));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    #endregion

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string ATMID = Utility.KillSqlInjection(txtATMID.Text.Trim());
            string atmCode = Utility.KillSqlInjection(txtATMCode.Text.Trim());
            string ATMAddress = Utility.KillSqlInjection(txtAddress.Text.Trim());
            string branchId = Utility.KillSqlInjection(ddlBranchId.SelectedValue.Trim());
            string districtCode = Utility.KillSqlInjection(ddlDistrict.SelectedValue.Trim());
            string cityCode = Utility.KillSqlInjection(ddlCity.SelectedValue.Trim());
            string countryId = Utility.KillSqlInjection(ddlCountry.SelectedValue.Trim());
            string desc = Utility.KillSqlInjection(txtDescription.Text.Trim());
            string CityCode = Utility.KillSqlInjection(ddlCity.SelectedValue.Trim());
            string DistrictCode = Utility.KillSqlInjection(ddlDistrict.SelectedValue.Trim());
            string PosX = Utility.KillSqlInjection(txtPosX.Text.Trim());
            string PosY = Utility.KillSqlInjection(txtPosY.Text.Trim());
            string userCreate = Utility.KillSqlInjection(Session["username"].ToString());

            #region Validattion

            try
            {
                int i = Convert.ToInt32(ATMID);
            }
            catch
            {
                lblError.Text = labels.atmidacceptnumberonly;
                return;
            }

            if (ATMID == string.Empty)
            {
                lblError.Text = labels.atmidrequired;
                return;
            }

            if (atmCode == String.Empty)
            {
                lblError.Text = labels.atmcoderequired;
                return;
            }

            if (ddlCountry.SelectedValue == "0")
            {
                lblError.Text = labels.selectcountryrequired;
                return;
            }


            if (ddlCity.SelectedValue == "0")
            {
                lblError.Text = labels.selectcityrequired;
                return;
            }

            if (ddlDistrict.SelectedValue == "0")
            {
                lblError.Text = labels.selectdistrictrequired;
                return;
            }

            if (ddlBranchId.SelectedValue == "0")
            {
                lblError.Text = labels.selectbranchrequired;
                return;
            }


            #endregion

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    try
                    {
                        new SmartPortal.SEMS.ATM().AddATM(ATMID, atmCode, ATMAddress, branchId, DistrictCode, CityCode,
                            "0", countryId, PosX, PosY, desc, "0",
                            "0", userCreate, ref IPCERRORCODE,
                            ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.themmoimayatmthanhcong;
                            Panel1.Enabled = false;
                            btsave.Visible = false;
                            btnClear.Enabled = true;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                        }
                    }
                    catch (IPCException IPCex)
                    {
                        SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name,
                            System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(
                            System.Configuration.ConfigurationManager.AppSettings["sysec"],
                            this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name,
                            ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(
                            System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }

                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    try
                    {
                        new ATM().EditATM(ATMID, atmCode, ATMAddress, branchId, districtCode, cityCode,
                            "0", countryId, PosX, PosY, desc, "0",
                            "0", userCreate, ref IPCERRORCODE,
                            ref IPCERRORDESC);

                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = labels.chinhsuamayatmthanhcong;
                            Panel1.Enabled = false;
                            btsave.Visible = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                        }
                    }
                    catch (IPCException IPCex)
                    {
                        SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name,
                            System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(
                            System.Configuration.ConfigurationManager.AppSettings["sysec"],
                            this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name,
                            ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(
                            System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }

                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name,
                System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void ddlCountryId_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCityDdl(ddlCountry.SelectedValue);
    }


    protected void ddlCity_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDistDdl(ddlCity.SelectedValue);
        LoadBranchDdl(ddlCity.SelectedValue, ddlDistrict.SelectedValue);
    }

    protected void ddlDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBranchDdl(ddlCity.SelectedValue, ddlDistrict.SelectedValue);
    }

    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        Panel1.Enabled = true;
        txtAddress.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtPosX.Text = string.Empty;
        txtPosY.Text = string.Empty;
        txtATMCode.Text = string.Empty;
        txtATMID.Text = string.Empty;
        lblError.Text = string.Empty;
        if (ACTION == IPC.ACTIONPAGE.ADD)
        {
            btsave.Visible = true;
        }

        LoadAllDropDownList();
    }
}