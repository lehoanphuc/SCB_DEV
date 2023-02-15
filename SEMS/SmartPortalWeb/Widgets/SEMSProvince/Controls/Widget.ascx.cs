using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System.Data;
using SmartPortal.BLL;
using SmartPortal.Constant;
using SmartPortal.Model;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSProvince_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public string ProvinceHeader
    {
        get
        {
            return lblProvinceHeader.Text;
        }
        set
        {
            lblProvinceHeader.Text = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
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
            
            DataSet dts = new DataSet();
            dts = new SmartPortal.SEMS.City().GETCOUNTRYBYID(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlCountry.DataSource = dts;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryID";
                ddlCountry.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
            ddlStatus.Items.Add(new ListItem(Resources.labels.active, "A"));
            ddlStatus.Items.Add(new ListItem(Resources.labels.inactive, "I"));
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    //load_Region();
                    break;
                default:
                    btClear.Enabled = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();          
                    DataSet dsCity = new DataSet();
                    dsCity = new City().GetCityByCondition(ID, "","", 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        if (dsCity.Tables[0].Rows.Count == 1)
                        {
                            txtCityCode.Text = dsCity.Tables[0].Rows[0]["CITYCODE"].ToString();
                            txtCityName.Text = dsCity.Tables[0].Rows[0]["CITYNAME"].ToString();
                            txtCityNameMM.Text = dsCity.Tables[0].Rows[0]["CITYNAMEMM"].ToString();
                            txtmasms.Text = dsCity.Tables[0].Rows[0]["SEARCHCODE"].ToString();
                            txtDescription.Text = dsCity.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                            ddlCountry.SelectedValue = dsCity.Tables[0].Rows[0]["COUNTRYID"].ToString();
                            //load_Region();
                            //ddlRegion.SelectedValue = dsCity.Tables[0].Rows[0]["REGIONID"].ToString();
                            ddlStatus.SelectedValue = dsCity.Tables[0].Rows[0]["STATUS"].ToString();
                            txtOrder.Text = dsCity.Tables[0].Rows[0]["ORD"].ToString();
                        }
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    txtCityCode.Enabled = false;
                    break;
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnFocus.Enabled = false;
                    btsave.Enabled = false;
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
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string CityCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCityCode.Text.Trim());
            string CityName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCityName.Text.Trim());
            string CityNameMM = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCityNameMM.Text.Trim());
            string SearchCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtmasms.Text.Trim());
            string Description = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDescription.Text.Trim());
            double Ord = double.Parse(SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtOrder.Text.Trim()));
            double Regionid = 0;
            string Countryid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCountry.Text.Trim());
            string Status = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlStatus.Text.Trim());
            string Usercreated = Session["userName"].ToString().Trim();

            City objCity = new City();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    try
                    {
                        objCity.InsertCity(CityCode, CityName, SearchCode, Description, Ord, Regionid, Countryid, Status, Usercreated, CityNameMM, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.themmoitinhthanhthanhcong;
                            btsave.Enabled = false;
                            pnFocus.Enabled = false;
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
                        objCity.EditCity(CityCode, CityName, SearchCode, Description, Ord, Regionid, Countryid, Status, Usercreated,CityNameMM, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.chinhsuatinhthanhthanhcong;
                            btsave.Enabled = false;
                            pnFocus.Enabled = false;
                        }
                        else
                        {
                            ErrorCodeModel EM = new ErrorCodeModel();
                            EM = new ErrorBLL().Load(Utility.IsInt(IPCERRORCODE), System.Globalization.CultureInfo.CurrentCulture.ToString());
                            lblError.Text = EM.ErrorDesc;
                            return;
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
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    //protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    load_Region();
    //}
    //void load_Region()
    //{
    //    DataSet ds = new DataSet();
    //    double id = double.Parse(ddlCountry.SelectedItem.Value);
    //    ds = new SmartPortal.SEMS.City().GETREGIONID(id, ref IPCERRORCODE, ref IPCERRORDESC);
    //    if (IPCERRORCODE == "0")
    //    {
    //        ddlRegion.DataSource = ds;
    //        ddlRegion.DataTextField = "RegionName";
    //        ddlRegion.DataValueField = "RegionID";
    //        ddlRegion.DataBind();
    //    }
    //    else
    //    {
    //        lblError.Text = IPCERRORDESC;
    //    }
    //}

    protected void btClear_Click(object sender, EventArgs e)
    {
        lblError.Text = String.Empty;
        ClearInputs(Page.Controls);
        pnFocus.Enabled = true;
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
