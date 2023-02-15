using System;
using System.Data;
using System.Text;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSGroupDefinition_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private SmartPortal.SEMS.GroupDefinition _service;
    private SmartPortal.SEMS.Common _common;
    public Widgets_SEMSGroupDefinition_Controls_Widget()
    {
        _service = new SmartPortal.SEMS.GroupDefinition();
        _common = new SmartPortal.SEMS.Common();
    }

    public string _TITLE
    {
        get { return lblTitleGroupDefinition.Text; }
        set { lblTitleGroupDefinition.Text = value; }
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    void loadCombobox()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("ACC_GRPDEF_DTL", "MDL", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddModule.DataSource = ds;
                ddModule.DataValueField = "VALUEID";
                ddModule.DataTextField = "CAPTION";
                ddModule.DataBind();
            }
        }
    }

    void defaultColor()
    {
        txtGroupID.BorderColor = System.Drawing.Color.Empty;
        ddModule.BorderColor = System.Drawing.Color.Empty;
        txtGroupname.BorderColor = System.Drawing.Color.Empty;

    }

    void enableControlView()
    {
        pnAdd.Enabled = false;
        btclear.Visible = false;
        btsave.Visible = false;
    }

    void loadGroupDefinitionData()
    {
        string groupID = GetParamsPage(IPC.ID)[0].Trim();
        DataSet ds = new DataSet();
        ds = _service.GetGroupDefinition(Utility.KillSqlInjection(groupID), ref IPCERRORCODE, ref IPCERRORDESC);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable tb = ds.Tables[0];
            txtGroupID.Text = tb.Rows[0]["GrpID"].ToString();
            ddModule.SelectedValue = tb.Rows[0]["Module"].ToString();
            txtGroupname.Text = tb.Rows[0]["ACGrpdef"].ToString();
            //txtPriority.Text = tb.Rows[0]["PRIORITY"].ToString();
            //ddlStatus.SelectedValue = tb.Rows[0]["STATUS"].ToString();
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
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    loadGroupDefinitionData();
                    enableControlView();
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtGroupID.Enabled = false;
                    loadGroupDefinitionData();
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public bool IsUnicode(string input)
    {
        try
        {
            var asciiBytesCount = Encoding.ASCII.GetByteCount(input);
            var unicodBytesCount = Encoding.UTF8.GetByteCount(input);
            return asciiBytesCount != unicodBytesCount;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    private bool checkValidate()
    {
        #region Validation
        defaultColor();
        if (txtGroupID.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.accountinggroup + " is not null";
            txtGroupID.BorderColor = System.Drawing.Color.Red;
            txtGroupID.Focus();
            return false;
        }
        if (IsUnicode(txtGroupID.Text))
        {
            lblError.Text = Resources.labels.accountinggroup + " is not allowed to have unicode characters";
            txtGroupID.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (ddModule.SelectedValue.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.modulename + " is not null";
            ddModule.BorderColor = System.Drawing.Color.Red;
            ddModule.Focus();
            return false;
        }
        if (txtGroupname.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.groupname + " is not null";
            txtGroupname.BorderColor = System.Drawing.Color.Red;
            txtGroupname.Focus();
            return false;
        }
        if (txtGroupID.Text.Trim().Contains(" "))
        {
            lblError.Text = Resources.labels.accountinggroup + " is not allowed to have whitespace characters";
            txtGroupID.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (txtGroupname.Text.Trim().Contains(" "))
        {
            lblError.Text = Resources.labels.groupname + " is not allowed to have whitespace characters";
            txtGroupname.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (IsUnicode(txtGroupname.Text))
        {
            lblError.Text = Resources.labels.groupname + " is not allowed to have unicode characters";
            txtGroupname.BorderColor = System.Drawing.Color.Red;
            return false;
        }

        return true;
        #endregion
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (!checkValidate()) return;
        txtGroupID.Text = txtGroupID.Text.Replace("+", "");
        txtGroupname.Text = txtGroupname.Text.Replace("+", "");
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                try
                {
                    DataSet ds = _service.InsertGroupDefinition(Utility.KillSqlInjection(txtGroupID.Text.Trim()), Utility.KillSqlInjection(ddModule.SelectedValue), Utility.KillSqlInjection(txtGroupname.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
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
                        pnAdd.Enabled = false;
                        btsave.Enabled = false;
                    }
                    else
                        lblError.Text = IPCERRORDESC;
                    break;
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
                    DataSet ds = _service.UpdateGroupDefinition(Utility.KillSqlInjection(txtGroupID.Text.Trim()), Utility.KillSqlInjection(ddModule.SelectedValue), Utility.KillSqlInjection(txtGroupname.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
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
                        pnAdd.Enabled = false;
                        btsave.Enabled = false;
                        lblError.Text = Resources.labels.thanhcong;
                    }
                    else
                        lblError.Text = IPCERRORDESC;
                    break;
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                }
                break;
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btclear_Click(object sender, EventArgs e)
    {
        txtGroupname.Text = string.Empty;
        ddModule.SelectedValue = string.Empty;
        pnAdd.Enabled = true;
        btsave.Enabled = true;
        lblError.Text = string.Empty;
        defaultColor();
        ACTION = GetActionPage();
        if (ACTION == IPC.ACTIONPAGE.ADD)
        {
            txtGroupID.Text = string.Empty;
        }
    }
}
