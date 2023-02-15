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
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;


public partial class Widgets_SEMSREGIONFEE_Controls_Widget : WidgetBase
{
    public static bool isAscend = false;
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public string _TITLE
    {
        get { return lblTitleBranch.Text; }
        set { lblTitleBranch.Text = value; }
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
            if (ACTION.Equals(IPC.ACTIONPAGE.DETAILS))
            {
                btsave.Visible = false;
                pnRegion.Enabled = false;
                btnClear.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_Module()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("MODULE", "ACT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModuleName.DataSource = ds;
                    ddlModuleName.DataValueField = "VALUEID";
                    ddlModuleName.DataTextField = "CAPTION";
                    ddlModuleName.DataBind();
                }
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
            loadCombobox_Module();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    string ListID = GetParamsPage(IPC.ID)[0].Trim();
                    String _module = ListID.ToString().Split('+')[0].ToString();
                    String _acname = ListID.ToString().Split('+')[1].ToString();

                    DataSet ds = new DataSet();
                    object[] searchObject = new object[] { Utility.KillSqlInjection(_module ), Utility.KillSqlInjection(_acname ) };
                    ds = _service.common("SEMS_ACGRPDEFDTL_VIE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                    if( ds != null)
                    {
                        if( ds.Tables.Count > 0)
                        {
                            DataTable dataTable = ds.Tables[0];
                            ddlModuleName.Text = dataTable.Rows[0]["MODULE"].ToString();
                            txtSysAccName.Text = dataTable.Rows[0]["AC_GRP_NAME"].ToString();
                            txtDescription.Text = dataTable.Rows[0]["BAC_GRP_NAME"].ToString();
                        }
                    }
                    ddlModuleName.Enabled = false;
                    txtSysAccName.ReadOnly = true;
                    break;

            }
            
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            defaultColor();
            string usercreate = Session["userName"].ToString();
            if (!CheckValidate()) return;
            txtSysAccName.Text = txtSysAccName.Text.Replace("+", "");
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    try
                    {
                        DataSet ds = new DataSet();
                        object[] searchObject = new object[] { Utility.KillSqlInjection(ddlModuleName.SelectedValue ), Utility.KillSqlInjection( txtSysAccName.Text.Trim() ) , Utility.KillSqlInjection(txtDescription.Text.Trim()) };
                        ds = _service.common("SEMS_ACGRPDEFDTL_INS", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE == "0")
                        {

                            lblError.Text = Resources.labels.addsuccessfully;
                            btsave.Enabled = false;
                            pnRegion.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
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
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    try
                    {
                        DataSet ds = new DataSet();
                        object[] searchObject = new object[] { Utility.KillSqlInjection(ddlModuleName.SelectedValue ), Utility.KillSqlInjection(txtSysAccName.Text.Trim()), Utility.KillSqlInjection(txtDescription.Text.Trim()) };
                        ds = _service.common("SEMS_ACGRPDEFDTL_UPD", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.updatesuccessfully;
                            btsave.Enabled = false;
                            pnRegion.Enabled = false;
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
    void defaultColor()
    {
        lblError.BorderColor = System.Drawing.Color.Empty;
        txtDescription.BorderColor = System.Drawing.Color.Empty;
        txtSysAccName.BorderColor = System.Drawing.Color.Empty;
        txtDescription.BorderColor = System.Drawing.Color.Empty;
    }
    private bool CheckValidate()
    {
        if (string.IsNullOrEmpty(ddlModuleName.SelectedValue.Trim()))
        {
            lblError.Text = Resources.labels.modulename + ' ' + Resources.labels.notallowedtonull;
            txtDescription.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (string.IsNullOrEmpty(txtSysAccName.Text.Trim()))
        {
            lblError.Text = Resources.labels.systemaccountname + ' ' + Resources.labels.notallowedtonull;
            txtSysAccName.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (txtSysAccName.Text.Trim().Contains(" "))
        {
            lblError.Text = Resources.labels.systemaccountname + " is not allowed to have whitespace characters";
            txtSysAccName.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
        {
            lblError.Text = Resources.labels.mota + ' ' + Resources.labels.notallowedtonull;
            txtDescription.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        
        return true;
    }



    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = String.Empty;
        defaultColor();
        if (ACTION.Equals(IPC.ACTIONPAGE.ADD))
        {
            txtSysAccName.Text = String.Empty;
        }
        btsave.Enabled = true;
        pnRegion.Enabled = true;
        txtDescription.Text = String.Empty;
    }
}
