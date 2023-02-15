using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSService_Control_Widget : WidgetBase
{
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    public string _TITLE
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            pnAdd.Visible = true;
            if (!IsPostBack)
            {
                LoadDll();
                BindData();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void LoadDll()
    {
        try
        {
            DataSet dsCorp = new Corporate().GetListActive(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                if (dsCorp != null && dsCorp.Tables.Count > 0 && dsCorp.Tables[0].Rows.Count > 0)
                {
                    ddlCorp.DataSource = dsCorp.Tables[0];
                    ddlCorp.DataTextField = "CORPNAME";
                    ddlCorp.DataValueField = "CORPID";
                    ddlCorp.DataBind();
                    LoadCatalog();
                }
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void LoadCatalog()
    {
        try
        {
            DataSet dsCorp = new Corporate().GetByCorpId(ddlCorp.SelectedValue.ToString() , ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                if (dsCorp != null && dsCorp.Tables.Count > 0 && dsCorp.Tables[0].Rows.Count > 0)
                {
                    lblCatalog.Text = dsCorp.Tables[0].Rows[0]["CATNAME"].ToString();
                }
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    txtServiceId.Enabled = true;
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    GetInfo();
                    btnSave.Visible = false;
                    btnBack.Text = Resources.labels.back;
                    btnBack.OnClientClick = "Loading();";
                    DisableControl();
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtServiceId.Enabled = false;
                    GetInfo();
                    break;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void GetInfo()
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            txtServiceId.Text = ID;
            if (string.IsNullOrEmpty(ID))
                RedirectBackToMainPage();
            ds = new Service().GetByID(ID, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtServiceCode.Text = dt.Rows[0]["ServiceCode"].ToString();
                    txtServiceName.Text = dt.Rows[0]["ServiceName"].ToString();
                    txtDesc.Text = dt.Rows[0]["Description"].ToString();
                    ddlCorp.SelectedValue = dt.Rows[0]["CorporateID"].ToString();
                    ddlServiceType.SelectedValue = dt.Rows[0]["ServiceType"].ToString();
                    ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                }
                else
                {
                    pnAdd.Visible = false;
                    lblError.Text = Resources.labels.datanotfound;
                    btnBack.Visible = false;
                    btnBack.OnClientClick = "Loading();";
                }
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DisableControl()
    {
        txtServiceId.Enabled = false;
        txtServiceCode.Enabled = false;
        txtServiceName.Enabled = false;
        txtDesc.Enabled = false;
        ddlCorp.Enabled = false;
        ddlServiceType.Enabled = false;
        ddlStatus.Enabled = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string serviceId = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtServiceId.Text.Trim()).ToUpper();
            string serviceCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtServiceCode.Text.Trim());
            string serviceName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtServiceName.Text.Trim());
            string desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.Trim());
            string corp = ddlCorp.SelectedValue;
            string serviceType = ddlServiceType.SelectedValue;
            string status = ddlStatus.SelectedValue;
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    if (ValidateInput())
                    {
                        new Service().Insert(serviceId,
                                serviceCode,
                                serviceName,
                                corp,
                                desc,
                                Session[IPC.USERNAME].ToString(),
                                DateTime.Now,
                                status,
                                serviceType,
                                ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            btnSave.Visible = false;
                            DisableControl();
                            lblError.Text = Resources.labels.addedrecordsuccessfully;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
                        }
                    }
                    break;

                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    if (ValidateInput())
                    {
                        new Service().Update(
                            serviceId,
                            serviceCode,
                            serviceName,
                            corp,
                            desc,
                            Session[IPC.USERNAME].ToString(),
                            DateTime.Now,
                            status,
                            serviceType,
                            ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE.Equals("0"))
                        {
                            btnSave.Visible = false;
                            DisableControl();
                            lblError.Text = Resources.labels.editedrecordsuccessfully;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
                        }
                    }
                    break;
                default:
                    RedirectBackToMainPage();
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name,
                System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message,
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);

        }
    }
    protected bool ValidateInput()
    {
        try
        {
            lblError.Text = "";
            string serviceID = txtServiceId.Text.Trim();
            string serviceCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtServiceCode.Text.Trim());
            string serviceName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtServiceName.Text.Trim());
            if (string.IsNullOrEmpty(serviceID))
            {
                lblError.Text = Resources.labels.bancannhapserviceid;
                return false;
            }
            if (!IsUsername(serviceID) && serviceID.Length > 1)
            {
                lblError.Text = Resources.labels.serviceidspecialcharactervalidate;
                return false;
            }
            if (serviceID.Contains(" "))
            {
                lblError.Text = Resources.labels.serviceidwhitespace;
                return false;
            }
            if (string.IsNullOrEmpty(serviceCode))
            {
                lblError.Text = Resources.labels.bancannhapshortname;
                return false;
            }
            if (string.IsNullOrEmpty(serviceName))
            {
                lblError.Text = Resources.labels.bancannhapservicename;
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            return false;
        }
    }
    public static bool IsUsername(string username)
    {
        string pattern;
        pattern = @"^(?=[a-zA-Z0-9-]{0,30}$)(?!.*[-]{2})[^-].*[^-]$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(username);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void ddlCorp_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCatalog();
    }
}