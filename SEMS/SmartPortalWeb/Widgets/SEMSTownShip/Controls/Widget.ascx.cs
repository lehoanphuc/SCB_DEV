using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using SmartPortal.BLL;
public partial class Widgets_SEMSTownShip_Controls_Widget : WidgetBase
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
                LoadDist();
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
        var urlb = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["b"] != null ? SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["b"].ToString() : "";
        if (urlb != "")
        {
            string param = "&a=LIST&cid=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["b"].ToString();
            RedirectBackToMainPage(param);
        }
        else
        {
            string link = PagesBLL.GetLinkMaster_Page("280");
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(link));
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string TownShipName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtTownShipname.Text.Trim());
            string TownShipNameMM = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtTownShipnameMM.Text.Trim());
            string DistCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlDistCode.SelectedValue.Trim());

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (ddlDistCode.SelectedValue == "" )
                    {
                        lblError.Text = Resources.labels.pleasechoosevalidTownship;
                        return;
                    }
                    new SmartPortal.SEMS.Township().InsertTownShip(TownShipName, DistCode, TownShipNameMM, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "13")
                    {
                        lblError.Text = Resources.labels.choosetownshipname;
                    }
                    else
                    {
                        lblError.Text = Resources.labels.addnewtownshipsuccess;
                        btsave.Enabled = false;
                        pnAdd.Enabled = false;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    if (ddlDistCode.SelectedValue == "" )
                    {
                        lblError.Text = Resources.labels.pleasechoosevalidTownship;
                        return;
                    }
                    new SmartPortal.SEMS.Township().EditTownship(ID, TownShipName, DistCode, TownShipNameMM,  ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "13")
                    {
                        lblError.Text = Resources.labels.choosetownshipname;
                    }
                    else
                    {
                        lblError.Text = Resources.labels.edittownshipsuccess;
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
                    DataSet dsTownShip =  new SmartPortal.SEMS.Township().GetTownShipByCondition(ID, "", "","" ,0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsTownShip.Tables[0].Rows.Count == 1)
                    {
                        txtTownShipname.Text = dsTownShip.Tables[0].Rows[0]["TownshipName"].ToString();
                        txtTownShipnameMM.Text = dsTownShip.Tables[0].Rows[0]["TownshipNameMM"].ToString();
                        ddlDistCode.SelectedValue = dsTownShip.Tables[0].Rows[0]["DistCode"].ToString();
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
                    ddlDistCode.Enabled = false;
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

     private void LoadDist()
     {
        try 
        {
            DataSet dts = new DataSet();
            dts = new SmartPortal.SEMS.Township().GetDistList(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlDistCode.DataSource = dts;
                ddlDistCode.DataTextField = "DistName";
                ddlDistCode.DataValueField = "DistCode";
                ddlDistCode.DataBind();
                string IDpage = GetParamsPage("b")[0].Trim();
                ddlDistCode.SelectedValue = IDpage;
                ddlDistCode.Enabled = false;
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
