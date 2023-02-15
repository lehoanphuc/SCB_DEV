using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;


public partial class Widgets_SEMSREGIONFEE_Controls_Widget : WidgetBase
{
    public static bool isAscend = false;
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static int flag = 1;
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
                ViewState["Acno"] = String.Empty;
                ddlAccountLevel_SelectedIndexChanged(sender, e);
                BindData();
            }
            if (ACTION.Equals(IPC.ACTIONPAGE.DETAILS))
            {
                btsave.Visible = false;
                pnRegion.Enabled = false;
                btClear.Visible = false;
                txtBranch.Enabled = false;
            }
            if (ACTION.Equals(IPC.ACTIONPAGE.EDIT))
            {
                ddlAccountLevel.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void LoadDll()
    {
        try
        {
            ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlCCYID.DataTextField = "TextValue";
            ddlCCYID.DataValueField = "CCYID";
            ddlCCYID.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_BalanceSide()
    {

        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("BALANCESIDE", "ACT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlBalanceSide.DataSource = ds;
                    ddlBalanceSide.DataValueField = "VALUEID";
                    ddlBalanceSide.DataTextField = "CAPTION";
                    ddlBalanceSide.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_PostingSide()
    {

        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("POSTING_SIDE", "ACT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlPostingSide.DataSource = ds;
                    ddlPostingSide.DataValueField = "VALUEID";
                    ddlPostingSide.DataTextField = "CAPTION";
                    ddlPostingSide.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_Group()
    {

        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("AC_GRP", "ACT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlAccgroup.DataSource = ds;
                    ddlAccgroup.DataValueField = "VALUEID";
                    ddlAccgroup.DataTextField = "CAPTION";
                    ddlAccgroup.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_Classification()
    {

        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("AC_CLS", "ACT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlAccClS.DataSource = ds;
                    ddlAccClS.DataValueField = "VALUEID";
                    ddlAccClS.DataTextField = "CAPTION";
                    ddlAccClS.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_Status()
    {

        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("STATUS", "ACT", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlStatus.DataSource = ds;
                    ddlStatus.DataValueField = "VALUEID";
                    ddlStatus.DataTextField = "CAPTION";
                    ddlStatus.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_AccountLevel()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("ACC_LEVEL", "ACC_CHART_LEVEL", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlAccountLevel.DataSource = ds;
                    ddlAccountLevel.DataValueField = "VALUEID";
                    ddlAccountLevel.DataTextField = "CAPTION";
                    ddlAccountLevel.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_AccountParent()
    {
        try
        {
            DataSet ds = new DataSet();
            ddlParentID.DataSource = null;
            ddlParentID.DataBind();
            object[] searchObject = new object[] { ddlAccountLevel.SelectedValue };
            ds = _service.common("SEMS_ACCHRT_PARENT", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlParentID.DataSource = ds;
                    ddlParentID.DataValueField = "Acno";
                    ddlParentID.DataTextField = "Acno";
                    ddlParentID.DataBind();
                    ddlParentID.Items.Insert(0, new ListItem("No Parent", "0"));
                }
                else
                {
                    ddlParentID.Items.Clear();
                    ddlParentID.Items.Insert(0, new ListItem("No Parent", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox()
    {
        loadCombobox_BalanceSide();
        loadCombobox_Group();
        loadCombobox_Classification();
        loadCombobox_PostingSide();
        loadCombobox_Status();
        LoadDll();
        loadCombobox_AccountLevel();
        loadCombobox_AccountParent();
    }
    void BindData()
    {
        try
        {
            loadCombobox();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet ds = new DataSet();
                    object[] searchObject = new object[] { ID };
                    ds = _service.common("SEMS_ACC_ACCHRT_VIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dataTable = ds.Tables[0];
                            if (dataTable.Rows.Count < 1)
                            {
                                lblError.Text = "Not found data!";
                                return;
                            }
                            ViewState["Acno"] = dataTable.Rows[0]["Acno"].ToString();
                            txtAccNumber.Text = dataTable.Rows[0]["Acno"].ToString();
                            txtBacno.Text = dataTable.Rows[0]["Bacno"].ToString();
                            txtAccName.Text = dataTable.Rows[0]["ACName"].ToString();
                            txtShortName.Text = dataTable.Rows[0]["ShortName"].ToString();
                            txtEnglishName.Text = dataTable.Rows[0]["OtherName"].ToString();
                            ddlAccClS.SelectedValue = dataTable.Rows[0]["ACCls"].ToString();
                            ddlAccgroup.SelectedValue = dataTable.Rows[0]["ACGrp"].ToString();
                            ddlBalanceSide.SelectedValue = dataTable.Rows[0]["BalanceSide"].ToString();
                            ddlPostingSide.SelectedValue = dataTable.Rows[0]["PostingSide"].ToString();
                            ddlStatus.SelectedValue = dataTable.Rows[0]["IsInactive"].ToString();
                            txtBranch.Value = dataTable.Rows[0]["BranchId"].ToString();
                            txtBranch.ValueName = dataTable.Rows[0]["BranchName"].ToString();
                            txtBranch.Text = dataTable.Rows[0]["TextBranchID"].ToString();
                            ddlCCYID.SelectedValue = dataTable.Rows[0]["CurrencyID"].ToString();
                            ddlAccountLevel.SelectedValue = dataTable.Rows[0]["ACLevel"].ToString();
                            loadCombobox_AccountParent();
                            ddlParentID.SelectedValue = dataTable.Rows[0]["ParentID"].ToString();
                            ddlParentID.SelectedValue = dataTable.Rows[0]["Acno"].ToString().Trim(); ;
                        }
                    }
                    txtAccNumber.ReadOnly = true;
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

            string usercreate = Session["userName"].ToString();
            if (!CheckValidate()) return;
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    try
                    {
                        DataSet ds = new DataSet();
                        object[] searchObject = new object[] { Utility.KillSqlInjection ( txtAccNumber.Text.Trim()),
                            Utility.KillSqlInjection ( txtBacno.Text.Trim()),
                            Utility.KillSqlInjection ( ddlCCYID.SelectedValue),
                            Utility.KillSqlInjection ( txtBranch.Value),
                            Utility.KillSqlInjection ( txtAccName.Text.Trim()),
                            Utility.KillSqlInjection (txtShortName.Text.Trim())
                            ,Utility.KillSqlInjection ( txtEnglishName.Text.Trim()),
                            Utility.KillSqlInjection ( ddlAccClS.SelectedValue),
                            Utility.KillSqlInjection (ddlBalanceSide.SelectedValue),
                            Utility.KillSqlInjection (ddlPostingSide.SelectedValue),Utility.KillSqlInjection ( ddlAccgroup.SelectedValue),
                            Utility.KillSqlInjection (ddlStatus.SelectedValue),
                            Utility.KillSqlInjection ( ddlAccountLevel.SelectedValue),
                            Utility.KillSqlInjection ( ddlParentID.SelectedValue ) };
                        ds = _service.common("SEMS_ACC_ACCHRT_ADD", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE == "0")
                        {

                            lblError.Text = Resources.labels.addsuccessfully;
                            btsave.Enabled = false;
                            pnRegion.Enabled = false;
                            txtBranch.Enabled = false;
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
                        object[] searchObject = new object[] { Utility.KillSqlInjection (  ViewState["Acno"].ToString()),
                            Utility.KillSqlInjection ( txtBacno.Text.Trim()),
                            Utility.KillSqlInjection ( ddlCCYID.SelectedValue),
                             Utility.KillSqlInjection ( txtBranch.Value),
                            Utility.KillSqlInjection ( txtAccName.Text.Trim()),
                             Utility.KillSqlInjection (txtShortName.Text.Trim())
                            ,Utility.KillSqlInjection ( txtEnglishName.Text.Trim()),
                            Utility.KillSqlInjection ( ddlAccClS.SelectedValue),
                            Utility.KillSqlInjection (ddlBalanceSide.SelectedValue),
                            Utility.KillSqlInjection (ddlPostingSide.SelectedValue),Utility.KillSqlInjection ( ddlAccgroup.SelectedValue),
                            Utility.KillSqlInjection (ddlStatus.SelectedValue),
                             Utility.KillSqlInjection ( ddlAccountLevel.SelectedValue),
                            Utility.KillSqlInjection ( ddlParentID.SelectedValue ) };
                        ds = _service.common("SEMS_ACC_ACCHRT_EDIT", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.updatesuccessfully;
                            btsave.Enabled = false;
                            pnRegion.Enabled = false;
                            txtBranch.Enabled = false;
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
    public void setDefault()
    {
        txtAccNumber.BorderColor = System.Drawing.Color.Empty;
        txtAccName.BorderColor = System.Drawing.Color.Empty;
        txtShortName.BorderColor = System.Drawing.Color.Empty;
        txtBranch.SetDefaultBranch();
        ddlCCYID.BorderColor = System.Drawing.Color.Empty;
        txtBacno.BorderColor = System.Drawing.Color.Empty;
        lblError.Text = String.Empty;
    }
    private bool CheckValidate()
    {
        setDefault();
        if (string.IsNullOrEmpty(txtAccNumber.Text.Trim()))
        {
            lblError.Text = Resources.labels.accountnumber + ' ' + Resources.labels.isnotallowedtobenull;
            txtAccNumber.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (string.IsNullOrEmpty(txtBacno.Text.Trim()))
        {
            lblError.Text = Resources.labels.Baseaccountnumber + ' ' + Resources.labels.isnotallowedtobenull;
            txtBacno.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (string.IsNullOrEmpty(txtAccName.Text.Trim()))
        {
            lblError.Text = Resources.labels.accountname + ' ' + Resources.labels.isnotallowedtobenull;
            txtAccName.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (string.IsNullOrEmpty(txtShortName.Text.Trim()))
        {
            lblError.Text = Resources.labels.shortaccountname + ' ' + Resources.labels.isnotallowedtobenull;
            txtShortName.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (ddlAccountLevel.SelectedValue.ToString() == "1" || ddlAccountLevel.SelectedValue.ToString() == "2" )
        {
            txtBranch.Text = string.Empty;
            txtBranch.Value = string.Empty;
        }
        if (ddlAccountLevel.SelectedValue.ToString() == "3" && txtBranch.Text.Equals(""))
        {
            lblError.Text = Resources.labels.branchcode + ' ' + Resources.labels.isnotallowedtobenull;
            txtBranch.SetErrorBranch();
            return false;
        }
        if (string.IsNullOrEmpty(ddlCCYID.SelectedValue.Trim()))
        {
            lblError.Text = Resources.labels.branchcode + ' ' + Resources.labels.isnotallowedtobenull;
            ddlCCYID.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        return true;
    }


    protected void btClear_Click(object sender, EventArgs e)
    {
        setDefault();
        if (ACTION.Equals(IPC.ACTIONPAGE.ADD))
        {
            txtAccNumber.Text = String.Empty;
            pnRegion.Enabled = true;
            btsave.Enabled = true;
        }
        txtAccName.Text = String.Empty;
        txtShortName.Text = String.Empty;
        txtEnglishName.Text = String.Empty;
        txtBranch.Value = String.Empty;
        txtBranch.Text = String.Empty;
        txtBacno.Text = String.Empty;
        txtBranch.Enabled = true;
        btsave.Enabled = true;
        pnRegion.Enabled = true;
        if (ACTION.Equals(IPC.ACTIONPAGE.EDIT))
        {
            ddlAccountLevel.Enabled = false;

        }
    }

    protected void ddlAccountLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCombobox_AccountParent();
        if (ddlAccountLevel.SelectedValue.ToString() == "3")
        {
            txtBranch.Enabled = true;
        }
        else
        {
            txtBranch.Text = string.Empty;
            txtBranch.Enabled = false;
        }
    }
    
}
