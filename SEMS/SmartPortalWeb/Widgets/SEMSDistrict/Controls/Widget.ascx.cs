using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSDistrict_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public string DISTHeader
    {
        get
        {
            return lblDistrictHeader.Text;
        }
        set
        {
            lblDistrictHeader.Text = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                LoadCity();
                BindData();
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
            string DistName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDistName.Text.Trim());
            string DistNameMM = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDistNameMM.Text.Trim());
            string CityCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCity.SelectedValue.Trim());
            string SearchCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtMaSMS.Text.Trim());

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;

                    if (ddlCity.SelectedValue == "" )
                    {
                        lblError.Text = Resources.labels.vuilongchonthanhphoquanhople;
                        return;
                    }
                    new SmartPortal.SEMS.District().InsertDistrict(DistName, CityCode,SearchCode, DistNameMM, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "13")
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    else
                    {
                        lblError.Text = Resources.labels.themmoiquanhuyenthanhcong;
                        btsave.Enabled = false;
                        pnAdd.Enabled = false;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    if (ddlCity.SelectedValue == "" )
                    {
                        lblError.Text = Resources.labels.vuilongchonthanhphoquanhople;
                        return;
                    }
                    new SmartPortal.SEMS.District().EditDistrict(ID, DistName, CityCode, SearchCode, DistNameMM, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "13")
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    else
                    {
                        lblError.Text = Resources.labels.chinhsuaquanhuyenthanhcong;
                        btsave.Visible = false;
                        pnAdd.Enabled = false;
                    }
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

    private void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    #region Lấy thông tin máy Province
                    btClear.Enabled = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet dsDist =  new SmartPortal.SEMS.District().GetDistrictByCondition(ID, "", "","", 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsDist.Tables[0].Rows.Count == 1)
                    {
                        txtDistName.Text = dsDist.Tables[0].Rows[0]["DISTNAME"].ToString();
                        txtDistNameMM.Text = dsDist.Tables[0].Rows[0]["DISTNAMEMM"].ToString();
                        ddlCity.SelectedValue = dsDist.Tables[0].Rows[0]["CITYCODE"].ToString();
                        txtMaSMS.Text = dsDist.Tables[0].Rows[0]["SEARCHCODE"].ToString();
                    }

                    break;
                    #endregion
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    btsave.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    ddlCity.Enabled = false;
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

     private void LoadCity()
     {
        try 
        {
            DataSet dts = new DataSet();
            dts = new SmartPortal.SEMS.District().GetCityById(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlCity.DataSource = dts;
                ddlCity.DataTextField = "CityName";
                ddlCity.DataValueField = "CityCode";
                ddlCity.DataBind();
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

    protected void btClear_Click(object sender, EventArgs e)
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
