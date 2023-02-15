using System;
using System.Collections.Generic;
using System.Data;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSAccountOfGroupDefinition_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static int tick = 0;

    private SmartPortal.SEMS.AccountOfGroupDefinition _service;
    //private SmartPortal.SEMS.Common _common;
    public Widgets_SEMSAccountOfGroupDefinition_Controls_Widget()
    {
        _service = new SmartPortal.SEMS.AccountOfGroupDefinition();
        //_common = new SmartPortal.SEMS.Common();
    }


    public string _TITLE
    {
        get { return lblTitleAccountOfGroupDefinition.Text; }
        set { lblTitleAccountOfGroupDefinition.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtgroupName.Enabled = false;
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                BindData();
                txtAccName.Enabled = false;
                tick = 0;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }

    protected void loadInfo(object sender, EventArgs e)
    {
        if (ACTION == IPC.ACTIONPAGE.ADD)
        {
            if (txtGroupCode.Text.Equals(string.Empty))
            {
                txtGroupCode.Enabled = true;
                txtAccName.Enabled = false;
            }
            else
            {
                txtGroupCode.Enabled = false;
                if (tick == 0)
                {
                    txtAccName.Enabled = true;
                }
                else
                {
                    txtAccName.Enabled = false;
                }

            }
        }
        else { txtGroupCode.Enabled = false; txtAccName.Enabled = false; }

        string module = txtGroupCode.getModule();
        string groupid = txtGroupCode.Value;
        getGroupName(groupid);
        if (module.Length > 0)
        {
            txtAccName.SetModule(module);
            txtAccName.reload_Bindata();
            //txtAccName.Text = string.Empty;
        }
        else
        {
            //txtAccName.SetModule("");
            return;
        }
    }

    public void getGroupName(string groupid)
    {
        Dictionary<object, object> _object = new Dictionary<object, object>();
        _object.Add("GRPID", groupid);

        SmartPortal.SEMS.Common _ser = new SmartPortal.SEMS.Common();
        DataSet ds = new DataSet();
        ds = _ser.CallStore("SEMS_ACC_GRPDEF_GET", _object, "Get groupname definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable tb = ds.Tables[0];
                    txtgroupName.Text = tb.Rows[0]["ACGrpdef"].ToString();
                }
                    
            }
        }
    }
    void defaultColor()
    {
        txtAccName.SetDefaultAccList();
        txtGroupCode.SetDefaultAccGrp();
        txtAcNo.BorderColor = System.Drawing.Color.Empty;
    }


    void enableControlView()
    {
        txtAcNo.Enabled = false;
        txtGroupCode.Enabled = false;
        txtAccName.Enabled = false;
        pnAdd.Enabled = false;
        btsave.Visible = false;
        btClear.Visible = false;
    }


    void loadEditAndViewData()
    {
        string ListID = GetParamsPage(IPC.ID)[0].Trim();
        string groupID = ListID.ToString().Split('+')[0].ToString();
        string acgrpname = ListID.ToString().Split('+')[1].ToString();
        DataSet ds = new DataSet();
        ds = _service.GetFunction(Utility.KillSqlInjection(groupID), Utility.KillSqlInjection(acgrpname), ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable tb = ds.Tables[0];
                txtGroupCode.Text = tb.Rows[0]["GrpID"].ToString();
                txtAcNo.Text = tb.Rows[0]["Acno"].ToString();
                txtAccName.Text = tb.Rows[0]["ACGrpName"].ToString();
                txtgroupName.Text = tb.Rows[0]["ACGrpdef"].ToString();
            }
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
                    loadEditAndViewData();
                    enableControlView();
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    loadEditAndViewData();
                    txtGroupCode.Enabled = false;
                    txtAccName.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private bool checkvalidate()
    {
        #region Validation
        defaultColor();
        if (txtGroupCode.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.accountinggroup + " is not null";
            txtGroupCode.SetErrorAccGrp();
            return false;
        }

        if (txtAcNo.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.accountnumber + " is not null";
            txtAcNo.BorderColor = System.Drawing.Color.Red;
            txtAcNo.Focus();
            return false;
        }
        if (txtAcNo.Text.Trim().Contains(" "))
        {
            lblError.Text = Resources.labels.accountnumber + " is not allowed to have whitespace characters";
            txtAcNo.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (txtAccName.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.systemaccountname + " is not null";
            //txtAccName.BorderColor = System.Drawing.Color.Red;
            txtAccName.SetErrorAccList();
            return false;
        }
        return true;

        #endregion
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (!checkvalidate()) return;
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                try
                {
                    DataSet ds = _service.InsertFunction(Utility.KillSqlInjection(txtGroupCode.getgroupid()), Utility.KillSqlInjection(txtAcNo.Text.Trim()), Utility.KillSqlInjection(txtAccName.getgroupname()), ref IPCERRORCODE, ref IPCERRORDESC);
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
                        tick = 1;
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
                    DataSet ds = _service.UpdateFunction(Utility.KillSqlInjection(txtGroupCode.Text.Trim()), Utility.KillSqlInjection(txtAcNo.Text.Trim()), Utility.KillSqlInjection(txtAccName.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
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
                        btsave.Enabled = false;
                        pnAdd.Enabled = false;
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
    protected void btClear_Click(object sender, EventArgs e)
    {
        ACTION = GetActionPage();
        if (ACTION == IPC.ACTIONPAGE.ADD)
        {
            txtGroupCode.Text = string.Empty;
            txtGroupCode.Value = string.Empty;
            txtgroupName.Text = string.Empty;
            txtAccName.Text = string.Empty;
            txtGroupCode.Enabled = true;
        }
        tick = 0;
        txtAcNo.Text = string.Empty;
        btsave.Enabled = true;
        pnAdd.Enabled = true;
        lblError.Text = string.Empty;
        defaultColor();
    }
}
