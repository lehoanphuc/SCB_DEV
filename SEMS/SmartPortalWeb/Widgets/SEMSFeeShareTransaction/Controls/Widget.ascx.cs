using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

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
                loadCombobox();
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_TransactionName()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { };
            ds = _service.common("SEMSGETRANSLISTALL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTransactionName.DataSource = ds;
                        ddlTransactionName.DataValueField = "STORENAME";
                        ddlTransactionName.DataTextField = "TRANSNAME";
                        ddlTransactionName.DataBind();
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

    private void loadCombobox_FeeCode()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { ddlTransactionName.SelectedValue };
            ds = _service.common("SEMSGETFEESHALIBYTRA", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlFeeShareCode.DataSource = ds;
                        ddlFeeShareCode.DataValueField = "FeeShareID";
                        ddlFeeShareCode.DataTextField = "FeeShareCode";
                        ddlFeeShareCode.DataBind();
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
    private void loadCombobox()
    {
        loadCombobox_TransactionName();
        loadCombobox_FeeCode();
    }
    public void loadEditAndViewData(string tranCode)
    {
        try
        {
            
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(tranCode) };
            ds = _service.common("SEMSTRANSFEESHAREVIE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if( ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTransactionName.SelectedValue = ds.Tables[0].Rows[0]["TranCode"].ToString();
                        loadCombobox_FeeCode();
                        ddlFeeShareCode.SelectedValue = ds.Tables[0].Rows[0]["FeeShareID"].ToString();
                    }
                    else
                    {
                        lblError.Text = "Not found transaction share fee";
                    }
                }
                else
                {
                    lblError.Text = "Not found transaction share fee";
                }
            }
            else
            {
                lblError.Text = "Not found transaction share fee";
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
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    ddlTransactionName.Enabled = true;
                    ddlFeeShareCode.Enabled = true;

                    break;
                case IPC.ACTIONPAGE.EDIT:
                    string trancode = GetParamsPage(IPC.ID)[0].Trim();
                   
                    btsave.Enabled = true;
                    btClear.Visible = false;
                    ddlTransactionName.Enabled = false;
                    ddlFeeShareCode.Enabled = true;
                    loadEditAndViewData(trancode);
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    string trancode2 = GetParamsPage(IPC.ID)[0].Trim();
                    ddlTransactionName.Enabled = false;
                    ddlFeeShareCode.Enabled = false;
                    btsave.Visible = false;
                    btClear.Visible = false;
                    loadEditAndViewData(trancode2);
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
       
        #region validate
        setDefault();
        
        return true;
        #endregion
    }


    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
           
            if (!checkvalidate()) { return; }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    try
                    {
                        DataSet ds = new DataSet();
                        object[] searchObject = new object[] { Utility.KillSqlInjection(ddlTransactionName.SelectedValue), Utility.KillSqlInjection(ddlFeeShareCode.SelectedValue), HttpContext.Current.Session["userID"].ToString() };
                        ds = _service.common("SEMSTRANSFEESHAREADD", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE == "0")
                        {
                            btsave.Enabled = false;
                            setDefault();
                            lblError.Text = Resources.labels.addsuccessfully;
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
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    try
                    {
                        DataSet ds = new DataSet();
                        object[] searchObject = new object[] { Utility.KillSqlInjection(ddlTransactionName.SelectedValue), Utility.KillSqlInjection(ddlFeeShareCode.SelectedValue), HttpContext.Current.Session["userID"].ToString() };
                        ds = _service.common("SEMSTRANSFEESHAREEDI", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            btsave.Enabled = false;
                           
                            lblError.Text = Resources.labels.updatesuccessfully;
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
        ddlTransactionName.BorderColor = System.Drawing.Color.Empty;
        ddlFeeShareCode.BorderColor = System.Drawing.Color.Empty;
        lblError.Text = string.Empty;
    }

    protected void btClear_Click(object sender, EventArgs e)
    {
        setDefault();
        ACTION = GetActionPage();
        if (ACTION == IPC.ACTIONPAGE.ADD)
        {
            btsave.Enabled = true;
        }
        loadCombobox();
    }

    protected void ddlTransactionName_TextChanged(object sender, EventArgs e)
    {
        loadCombobox_FeeCode();
    }
}
