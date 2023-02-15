using System;
using System.Data;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSContractLevel_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private SmartPortal.SEMS.Common _common;
    string ContractLevelID;
    public Widgets_SEMSContractLevel_Controls_Widget()
    {
        _common = new SmartPortal.SEMS.Common();
    }


    public string _TITLE
    {
        get { return lblTitleContracLevel.Text; }
        set { lblTitleContracLevel.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtLimitBalance.Attributes.Add("onkeyup", "executeCommaAllowNegative('" + txtLimitBalance.ClientID + "')");
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
    private void loadCombobox_contractlevelID()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { };
            ds = _common.common("SEMSLoadContractLV", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddLevelID.DataSource = ds;
                        ddLevelID.DataValueField = "ContractLevelID";
                        ddLevelID.DataTextField = "ContractLevelName";
                        ddLevelID.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_SubUserType()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { };
            ds = _common.common("SEMSLoadSubUserType", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddSubUserType.DataSource = ds;
                        ddSubUserType.DataValueField = "SubUserCode";
                        ddSubUserType.DataTextField = "SubUserType";
                        ddSubUserType.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }


    void loadComboboxStatus()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("STATUS", "WAL_CONTRACT_LEVEL", ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddStatus.DataSource = ds;
            ddStatus.DataValueField = "VALUEID";
            ddStatus.DataTextField = "CAPTION";
            ddStatus.DataBind();
        }
    }
    void loadCombobox()
    {
        loadCombobox_contractlevelID();
        loadComboboxStatus();
        loadCombobox_SubUserType();
    }

    void setDefaultTextbox()
    {
        lblError.Text = string.Empty;
        txtLimitBalance.Text = string.Empty;
    }

    void setDefaultControl()
    {
        setDefaultTextbox();
        defaultColor();
    }

    void defaultColor()
    {
        ddLevelID.BorderColor = System.Drawing.Color.Empty;
        txtLimitBalance.BorderColor = System.Drawing.Color.Empty;
        ddSubUserType.BorderColor = System.Drawing.Color.Empty;
    }

    void enableControlView(bool value)
    {
        ddLevelID.Enabled = value;
        txtLimitBalance.Enabled = value;
        ddSubUserType.Enabled = value;
        btsave.Enabled = value;
        ddStatus.Enabled = value;
    }

    void loadContracLevelData(string ID, string sub)
    {
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { Utility.KillSqlInjection(ID), Utility.KillSqlInjection(sub) };
        ds = _common.common("SEMS_CONTRACTLVLVIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable tb = ds.Tables[0];
                ddLevelID.SelectedValue = tb.Rows[0]["ContractLevelID"].ToString();
                txtLimitBalance.Text = tb.Rows[0]["LimitBalance"].ToString();
                ddSubUserType.SelectedValue = tb.Rows[0]["SubUserCode"].ToString();
                ddStatus.SelectedValue = tb.Rows[0]["Status"].ToString();
            }
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
                    string ListID = GetParamsPage(IPC.ID)[0].Trim();
                    string contracllvlID = ListID.ToString().Split('+')[0].ToString();
                    string SubCode = ListID.ToString().Split('+')[1].ToString();
                    loadContracLevelData(contracllvlID, SubCode);
                    enableControlView(false);
                    btback.Visible = true;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    string ListID2 = GetParamsPage(IPC.ID)[0].Trim();
                    string contracllvlID2 = ListID2.ToString().Split('+')[0].ToString();
                    string SubCode2 = ListID2.ToString().Split('+')[1].ToString();
                    ddLevelID.Enabled = false;
                    ddSubUserType.Enabled = false;
                    btnClear.Visible = true;
                    loadContracLevelData(contracllvlID2, SubCode2);
                    break;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    private bool checkvalidate()
    {
        #region Validation
        defaultColor();
        //if (ddLevelID.SelectedValue.Trim().Equals(string.Empty))
        //{
        //    lblError.Text = Resources.labels.ContractLevelCode + Resources.labels.IsNotNull;
        //    ddLevelID.BorderColor = System.Drawing.Color.Red;
        //    ddLevelID.Focus();
        //    return false;
        //}

        //if (!Utility.CheckSpecialCharacters(ddLevelID.Text))
        //{
        //    lblError.Text = Resources.labels.ContractLevelCode + Resources.labels.ErrorSpeacialCharacters;
        //    ddLevelID.BorderColor = System.Drawing.Color.Red;
        //    ddLevelID.Focus();
        //    return false;
        //}
        return true;
        #endregion
    }


    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!checkvalidate()) { return; }
            string limit;
            if (!txtLimitBalance.Text.Trim().Equals(""))
            {
                limit = txtLimitBalance.Text.Trim();
            }
            else
            {
                limit = (-1).ToString();
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
                    {
                        lblError.Text = "User does not have permission.";
                        return;
                    }
                    try
                    {
                        DataSet ds = new DataSet();
                        object[] searchObject = new object[] {
                            Utility.KillSqlInjection(ddLevelID.SelectedValue),
                            Utility.KillSqlInjection(ddSubUserType.SelectedValue),
                            Utility.KillSqlInjection(limit),
                            Utility.KillSqlInjection(ddStatus.SelectedValue) };
                        ds = _common.common("SEMS_CONTRACTLVL_INS", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE == "0")
                        {
                            if (txtLimitBalance.Text.Trim().Equals(""))
                            {
                                txtLimitBalance.Text = (-1).ToString();
                            }
                            lblError.Text = Resources.labels.addsuccessfully;
                            btsave.Enabled = false;
                            pnRegion.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                        }

                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                    {
                        lblError.Text = "User does not have permission.";
                        return;
                    }
                    try
                    {
                        DataSet ds = new DataSet();
                        object[] searchObject = new object[] {
                            Utility.KillSqlInjection(ddLevelID.Text.Trim()),
                            Utility.KillSqlInjection(ddSubUserType.SelectedValue),
                            Utility.KillSqlInjection(limit),
                            Utility.KillSqlInjection(ddStatus.SelectedValue) };
                        ds = _common.common("SEMS_CONTRACTLVL_UP", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            btsave.Enabled = false;
                            pnRegion.Enabled = false;
                            if (IPCERRORCODE == "0")
                            {
                                if (txtLimitBalance.Text.Trim().Equals(""))
                                {
                                    txtLimitBalance.Text = (-1).ToString();
                                }
                                lblError.Text = Resources.labels.updatesuccessfully;
                                btsave.Enabled = false;
                                pnRegion.Enabled = false;
                            }
                            else
                            {
                                lblError.Text = IPCERRORDESC;
                            }
                        }
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        if (ACTION.Equals(IPC.ACTIONPAGE.ADD))
        {
            setDefaultControl();
            enableControlView(true);
        }
        if (ACTION.Equals(IPC.ACTIONPAGE.EDIT))
        {
            ddLevelID.Enabled = false;
            ddSubUserType.Enabled = false;
            btsave.Enabled = true;
            txtLimitBalance.Text = string.Empty;
        }
        pnRegion.Enabled = true;
        lblError.Text = string.Empty;
    }
}
