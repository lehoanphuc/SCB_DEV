using System;
using System.Data;
using System.Web;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSContractLevel_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private SmartPortal.SEMS.ContractLevel _service;
    private SmartPortal.SEMS.Common _common;
    string ContractLevelID;
    public Widgets_SEMSContractLevel_Controls_Widget()
    {
        _service = new SmartPortal.SEMS.ContractLevel();
        _common = new SmartPortal.SEMS.Common();
    }


    public string _TITLE
    {
        get { return lblTitleContracLevel.Text; }
        set { lblTitleContracLevel.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (!ACTION.Equals(IPC.ACTIONPAGE.ADD))
            {
                ContractLevelID = GetParamsPage(IPC.ID)[0].Trim();
            }
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
    void loadCombobox()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("STATUS", "WAL_CONTRACT_LEVEL", ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlStatus.DataSource = ds;
            ddlStatus.DataValueField = "VALUEID";
            ddlStatus.DataTextField = "CAPTION";
            ddlStatus.DataBind();
        }
    }

    void setDefaultTextbox()
    {
        lblError.Text = string.Empty;
        txtContractLevelCode.Text = string.Empty;
        txtContractLevelName.Text = string.Empty;
        txtOrder.Text = string.Empty;
        txtPriority.Text = string.Empty;
        txtCondition.Text = string.Empty;
    }

    void setDefaultControl()
    {
        setDefaultTextbox();
        defaultColor();
    }

    void defaultColor()
    {
        txtContractLevelCode.BorderColor = System.Drawing.Color.Empty;
        txtContractLevelName.BorderColor = System.Drawing.Color.Empty;
        txtOrder.BorderColor = System.Drawing.Color.Empty;
        txtPriority.BorderColor = System.Drawing.Color.Empty;
        txtCondition.BorderColor = System.Drawing.Color.Empty;
    }

    void enableControlView(bool value)
    {
        txtContractLevelCode.Enabled = value;
        txtOrder.Enabled = value;
        txtContractLevelName.Enabled = value;
        txtPriority.Enabled = value;
        txtCondition.Enabled = value;
        ddlStatus.Enabled = value;
        btsave.Enabled = value;
    }

    void loadContracLevelData()
    {
        DataSet ds = new DataSet();
        ds = _service.GetContractLevel(ContractLevelID, ref IPCERRORCODE, ref IPCERRORDESC);
        
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable tb = ds.Tables[0];
            txtContractLevelCode.Text = tb.Rows[0]["ContractLevelCode"].ToString();
            txtContractLevelName.Text = tb.Rows[0]["ContractLevelName"].ToString();
            txtOrder.Text = tb.Rows[0]["Ord"].ToString();
            txtPriority.Text = tb.Rows[0]["Priority"].ToString();
            ddlStatus.SelectedValue = tb.Rows[0]["Status"].ToString();
            txtCondition.Text = tb.Rows[0]["Condition"].ToString();
        }
    }
    void BindData()
    {
        try
        {
            loadCombobox();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    btnClear.Visible = true;
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    loadContracLevelData();
                    enableControlView(false);
                    btback.Visible = true;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    //Load data contract level
                    txtContractLevelCode.Enabled = false;
                    txtCondition.Enabled = false;
                    btnClear.Visible = true;
                    loadContracLevelData();
                    break;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        switch (ACTION)
        {
            // Add new contract level
            case IPC.ACTIONPAGE.ADD:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                try
                {
                    //Set defalt color
                    defaultColor();
                    //Validatetion
                    // Check not null
                    #region Validation
                    
                    if (txtContractLevelCode.Text.Trim().Equals(string.Empty))
                    {
                        lblError.Text = Resources.labels.ContractLevelCode + Resources.labels.IsNotNull;
                        txtContractLevelCode.BorderColor = System.Drawing.Color.Red;
                        txtContractLevelCode.Focus();
                        return;
                    }

                    if (!Utility.CheckSpecialCharacters(txtContractLevelCode.Text))
                    {
                        lblError.Text = Resources.labels.ContractLevelCode + Resources.labels.ErrorSpeacialCharacters;
                        txtContractLevelCode.BorderColor = System.Drawing.Color.Red;
                        txtContractLevelCode.Focus();
                        return;
                    }

                    if (txtContractLevelName.Text.Trim().Equals(string.Empty))
                    {
                        lblError.Text = Resources.labels.ContractLevelName + Resources.labels.IsNotNull;
                        txtContractLevelName.BorderColor = System.Drawing.Color.Red;
                        txtContractLevelName.Focus();
                        return;
                    }
                    if (!Utility.CheckSpecialCharacters(txtContractLevelName.Text))
                    {
                        lblError.Text = Resources.labels.ContractLevelName + Resources.labels.ErrorSpeacialCharacters;
                        txtContractLevelName.BorderColor = System.Drawing.Color.Red;
                        txtContractLevelName.Focus();
                        return;
                    }
                    if (txtOrder.Text.Trim().Equals(string.Empty))
                    {
                        lblError.Text = Resources.labels.order + Resources.labels.IsNotNull;
                        txtOrder.BorderColor = System.Drawing.Color.Red;
                        txtOrder.Focus();
                        return;
                    }
                    if (txtPriority.Text.Trim().Equals(string.Empty))
                    {
                        lblError.Text = Resources.labels.Priority + Resources.labels.IsNotNull;
                        txtPriority.BorderColor = System.Drawing.Color.Red;
                        txtPriority.Focus();
                        return;
                    }
                    //if (txtCondition.Text.Trim().Equals(string.Empty))
                    //{
                    //    lblError.Text = Resources.labels.Condition + Resources.labels.IsNotNull;
                    //    txtCondition.BorderColor = System.Drawing.Color.Red;
                    //    txtCondition.Focus();
                    //    return;
                    //}
                    if (!Utility.CheckSpecialCharacters(txtCondition.Text))
                    {
                        lblError.Text = Resources.labels.ContractLevelName + Resources.labels.ErrorSpeacialCharacters;
                        txtCondition.BorderColor = System.Drawing.Color.Red;
                        txtCondition.Focus();
                        return;
                    }
                    #endregion
                    DataSet ds = _service.InsertContractLevel(
                        Utility.KillSqlInjection(txtContractLevelCode.Text.Trim()),
                        Utility.KillSqlInjection(txtContractLevelName.Text.Trim()),
                        Utility.KillSqlInjection(ddlStatus.SelectedValue.Trim()),
                        Utility.KillSqlInjection(HttpContext.Current.Session["userID"].ToString().Trim()),
                        int.Parse(txtOrder.Text), int.Parse(txtPriority.Text), Utility.KillSqlInjection(txtCondition.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            lblError.Text = ds.Tables[0].Rows[0][0].ToString();
                            return;
                        }
                    }
                    if (IPCERRORCODE.Equals("0"))
                    {
                        lblError.Text = Resources.labels.thanhcong;
                        enableControlView(false);
                    }
                    else
                        lblError.Text = IPCERRORDESC;
                    break;
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message.ToString();
                }
                break;
            case IPC.ACTIONPAGE.EDIT:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                try
                {
                    defaultColor();
                    if (txtContractLevelName.Text.Trim().Equals(string.Empty))
                    {
                        lblError.Text = Resources.labels.ContractLevelName + Resources.labels.IsNotNull;
                        txtContractLevelName.BorderColor = System.Drawing.Color.Red;
                        txtContractLevelName.Focus();
                        return;
                    }
                    if (!Utility.CheckSpecialCharacters(txtContractLevelName.Text))
                    {
                        lblError.Text = Resources.labels.ContractLevelName + Resources.labels.ErrorSpeacialCharacters;
                        txtContractLevelName.BorderColor = System.Drawing.Color.Red;
                        txtContractLevelName.Focus();
                        return;
                    }
                    if (txtOrder.Text.Trim().Equals(string.Empty))
                    {
                        lblError.Text = Resources.labels.order + Resources.labels.IsNotNull;
                        txtOrder.BorderColor = System.Drawing.Color.Red;
                        txtOrder.Focus();
                        return;
                    }
                    if (txtPriority.Text.Trim().Equals(string.Empty))
                    {
                        lblError.Text = Resources.labels.Priority + Resources.labels.IsNotNull;
                        txtPriority.BorderColor = System.Drawing.Color.Red;
                        txtPriority.Focus();
                        return;
                    }
                    //if (txtCondition.Text.Trim().Equals(string.Empty))
                    //{
                    //    lblError.Text = Resources.labels.Condition + Resources.labels.IsNotNull;
                    //    txtCondition.BorderColor = System.Drawing.Color.Red;
                    //    txtCondition.Focus();
                    //    return;
                    //}
                    if (!Utility.CheckSpecialCharacters(txtCondition.Text))
                    {
                        lblError.Text = Resources.labels.ContractLevelName + Resources.labels.ErrorSpeacialCharacters;
                        txtCondition.BorderColor = System.Drawing.Color.Red;
                        txtCondition.Focus();
                        return;
                    }

                    DataSet ds = _service.UpdateContractLevel(
                        ContractLevelID,
                        Utility.KillSqlInjection(txtContractLevelCode.Text.Trim()),
                        Utility.KillSqlInjection(txtContractLevelName.Text.Trim()),
                        Utility.KillSqlInjection(ddlStatus.SelectedValue.Trim()),
                        Utility.KillSqlInjection(HttpContext.Current.Session["userID"].ToString().Trim()),
                        int.Parse(txtOrder.Text), int.Parse(txtPriority.Text), Utility.KillSqlInjection(txtCondition.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0"))
                    {
                        lblError.Text = Resources.labels.thanhcong;
                        btsave.Enabled = false;
                        enableControlView(false);
                    }

                    else
                        lblError.Text = IPCERRORDESC;
                    break;
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message.ToString();
                }
                break;
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        if (ACTION.Equals(IPC.ACTIONPAGE.ADD))
        {
            setDefaultControl();
            enableControlView(true);
        }
        if (ACTION.Equals(IPC.ACTIONPAGE.EDIT))
        {
            txtContractLevelName.Text = string.Empty;
            txtOrder.Text = string.Empty;
            txtPriority.Text = string.Empty;
            //txtCondition.Text = string.Empty;
            defaultColor();
            enableControlView(true);
            txtContractLevelCode.Enabled = false;
        }
        
    }
}
