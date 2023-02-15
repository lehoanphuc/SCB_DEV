using System;
using System.Data;
using System.Text;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSCommonAccountDefinition_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private SmartPortal.SEMS.CommonAccDef _service;
    public Widgets_SEMSCommonAccountDefinition_Controls_Widget()
    {
        _service = new SmartPortal.SEMS.CommonAccDef();
    }

    public string _TITLE
    {
        get { return lblTitleCommnonAccountDefinition.Text; }
        set { lblTitleCommnonAccountDefinition.Text = value; }
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

    void defaultColor()
    {
        txtACNAME.BorderColor = System.Drawing.Color.Empty;
        txtACNO.BorderColor = System.Drawing.Color.Empty;
        txtRefAccNum1.BorderColor = System.Drawing.Color.Empty;
        txtRefAccNum2.BorderColor = System.Drawing.Color.Empty;

    }

    void enableControlView()
    {
        pnAdd.Enabled = false;
        txtACNO.Enabled = false;
        btback.Visible = true;
        btsave.Visible = false;
        btClear.Visible = false;
    }

    void loadCommonAccountDefinitionData()
    {
        string acname = GetParamsPage(IPC.ID)[0].Trim();
        DataSet ds = new DataSet();
        ds = _service.GetCommonAccountDefinition(Utility.KillSqlInjection(acname), ref IPCERRORCODE, ref IPCERRORDESC);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable tb = ds.Tables[0];
            txtACNAME.Text = tb.Rows[0]["ACNAME"].ToString();
            txtACNO.Text = tb.Rows[0]["BACNO"].ToString();
            txtRefAccNum1.Text = tb.Rows[0]["REFACNO"].ToString();
            txtRefAccNum2.Text = tb.Rows[0]["REFACNO2"].ToString();
        }
    }
    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    loadCommonAccountDefinitionData();
                    enableControlView();
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtACNAME.Enabled = false;
                    loadCommonAccountDefinitionData();
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
    protected void btsave_Click(object sender, EventArgs e)
    {
        switch (ACTION)
        {
            // Add new contract level
            case IPC.ACTIONPAGE.ADD:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                try
                {
                    #region Validation
                    defaultColor();
                    if (txtACNAME.Text.Trim().Equals(string.Empty))
                    {
                        lblError.Text = Resources.labels.accountname + " is not null";
                        txtACNAME.BorderColor = System.Drawing.Color.Red;
                        txtACNAME.Focus();
                        return;
                    }
                    if (txtACNAME.Text.Trim().Contains(" "))
                    {
                        lblError.Text = Resources.labels.accountname + " is not allowed to have whitespace characters";
                        txtACNAME.BorderColor = System.Drawing.Color.Red;
                        return;
                    }
                    if (IsUnicode(txtACNAME.Text))
                    {
                        lblError.Text = Resources.labels.accountname + " is not allowed to have unicode characters";
                        txtACNAME.BorderColor = System.Drawing.Color.Red;
                        return;
                    }
                    #endregion

                    DataSet ds = _service.InsertCommonAccountDefinition(Utility.KillSqlInjection(txtACNAME.Text.Trim()), Utility.KillSqlInjection(txtACNO.Text.Trim()), Utility.KillSqlInjection(txtRefAccNum1.Text.Trim()), Utility.KillSqlInjection(txtRefAccNum2.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE.Equals("0"))
                    {
                        lblError.Text = Resources.labels.thanhcong;
                        btsave.Enabled = false;
                        pnAdd.Enabled = false;
                        return;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
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
                    DataSet ds = _service.UpdateGroupDefinition(Utility.KillSqlInjection(txtACNAME.Text.Trim()), Utility.KillSqlInjection(txtACNO.Text.Trim()), Utility.KillSqlInjection(txtRefAccNum1.Text.Trim()), Utility.KillSqlInjection(txtRefAccNum2.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
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
                        btsave.Enabled = false;
                        pnAdd.Enabled = false;
                    }
                    else { lblError.Text = IPCERRORDESC; }
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
    protected void btClear_Click(object sender, EventArgs e)
    {
        ACTION = GetActionPage();
        if (ACTION == IPC.ACTIONPAGE.ADD)
        {
            txtACNAME.Text = string.Empty;
        }
        txtACNO.Text = string.Empty;
        txtRefAccNum1.Text = string.Empty;
        txtRefAccNum2.Text = string.Empty;
        btsave.Enabled = true;
        pnAdd.Enabled = true;
        lblError.Text = string.Empty;
        defaultColor();
    }

}
