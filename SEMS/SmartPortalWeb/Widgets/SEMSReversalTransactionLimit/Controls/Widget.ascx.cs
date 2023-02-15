using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;
using System.Collections.Generic;

public partial class Widgets_SEMSContractLevel_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    string trancode;

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
            txtLimit.Attributes.Add("onkeyup", "executeComma('" + txtLimit.ClientID + "',event)");
            lblError.Text = string.Empty;
            if (!ACTION.Equals(IPC.ACTIONPAGE.ADD))
            {
                trancode = GetParamsPage(IPC.ID)[0].Trim();
            }
            if (!IsPostBack)
            {
                checkIsReversal_OnCheckedChanged(sender, e);
                checktxtlimit_OnCheckedChanged(sender, e);
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
        try
        {
            //load tran app
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { };
            ds = _service.common("SEMSTRANCODEREVERSAL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dtTranApp = new DataTable();
                    dtTranApp = ds.Tables[0];

                    ddlTransactionType.DataSource = dtTranApp;
                    ddlTransactionType.DataTextField = "PAGENAME";
                    ddlTransactionType.DataValueField = "TRANCODE";
                    ddlTransactionType.DataBind();
                }
            }


        }
        catch (Exception ex)
        {
            lblError.Text = IPCERRORDESC;

        }

        

    }

    void setDefaultTextbox()
    {
        lblError.Text = string.Empty;
        txtLimit.Text = string.Empty;
    }

    protected void checktxtlimit_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (checktxtlimit.Checked)
            {
                txtLimit.Text = null;
                txtLimit.Enabled = false;
                defaultColor();
            }
            else
            {
                txtLimit.Text = null;
                txtLimit.Enabled = true;
            }


        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }


    protected void checkIsReversal_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlIsreversal.SelectedValue=="0")
            {
                txtLimit.BorderColor = System.Drawing.Color.Empty;
                ddlUnit.Enabled = false;
                txtLimit.Enabled = false;
                txtLimit.Text = string.Empty;
                checktxtlimit.Enabled = false;
                checktxtlimit.Checked = false;

            }
            else
            {
                ddlUnit.Enabled = true;
                txtLimit.Enabled = true;
                checktxtlimit.Enabled = true;
                checktxtlimit.Checked = false;
            }


        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }

    void setDefaultControl()
    {
        setDefaultTextbox();
        defaultColor();
    }

    void defaultColor()
    {
        txtLimit.BorderColor = System.Drawing.Color.Empty;
    }

    void enableControlView(bool value)
    {
        ddlTransactionType.Enabled = value;
        ddlIsreversal.Enabled = value;
        ddlUnit.Enabled = value;
        txtLimit.Enabled = value;
        checktxtlimit.Enabled = value;
        btsave.Enabled = value;
    }
    void loadReversalLimit(string ID)
    {
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { Utility.KillSqlInjection(ID) };
        ds = _service.common("SEMS_RRT_LIMITDETAIL", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable tb = ds.Tables[0];
                ddlTransactionType.SelectedValue = tb.Rows[0]["IPCTRANCODE"].ToString();
                txtLimit.Text = tb.Rows[0]["LimitRR"].ToString();
                ddlUnit.SelectedValue = tb.Rows[0]["Unit"].ToString();
                ddlIsreversal.SelectedValue = tb.Rows[0]["IsReversal"].ToString();
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
                    loadReversalLimit(trancode);
                    enableControlView(false);
                    btsave.Visible = false;
                    btback.Visible = true;
                    if (ddlIsreversal.SelectedValue == "1")
                    {
                        if (txtLimit.Text == "-1")
                        {
                            checktxtlimit.Checked = true;
                            txtLimit.Text = "";
                        }
                        else
                        {
                            checktxtlimit.Checked = false;
                        }
                    }
                    else
                    {
                        if (txtLimit.Text == "-1")
                        {
                            checktxtlimit.Checked = false;
                            checktxtlimit.Enabled = false;
                            txtLimit.Text = "";
                            txtLimit.Enabled = false;
                        }
                        else
                        {
                            checktxtlimit.Checked = false;
                            checktxtlimit.Enabled = false;
                            txtLimit.Enabled = false;
                        }
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    loadReversalLimit(trancode);
                    ddlTransactionType.Enabled = false;
                    btnClear.Visible = true;
                    if (ddlIsreversal.SelectedValue == "1")
                    {
                        if (txtLimit.Text == "-1")
                        {
                            checktxtlimit.Checked = true;
                            txtLimit.Text = "";
                            txtLimit.Enabled = false;
                        }
                        else
                        {
                            checktxtlimit.Checked = false;
                            txtLimit.Enabled = true;
                        }
                    }
                    else
                    {
                        if (txtLimit.Text == "-1")
                        {
                            checktxtlimit.Checked = false;
                            checktxtlimit.Enabled = false;
                            txtLimit.Text = "";
                            txtLimit.Enabled = false;
                            ddlUnit.Enabled = false;
                        }
                        else
                        {
                            checktxtlimit.Checked = false;
                            checktxtlimit.Enabled = false;
                            txtLimit.Enabled = false;
                            ddlUnit.Enabled = false;
                        }

                    }
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
            case IPC.ACTIONPAGE.ADD:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                try
                {
                    //Set defalt color
                    defaultColor();
                    //Validatetion
                    // Check not null
                    #region Validation
                    if (txtLimit.Text.Trim().Equals(string.Empty) && ddlIsreversal.SelectedValue == "1" && checktxtlimit.Checked == false)
                    {
                        lblError.Text = Resources.labels.hanmuc + " cannot be null";
                        txtLimit.BorderColor = System.Drawing.Color.Red;
                        txtLimit.Focus();
                        return;
                    }

                    #endregion
                    string textLimit = "";
                   
                    if (ddlIsreversal.SelectedValue == "0")
                    {
                        textLimit = "-1";
                    }
                    else
                    {
                        if (checktxtlimit.Checked == true)
                        {
                            textLimit = "-1";
                        }
                        else
                        {
                            textLimit = txtLimit.Text.Replace(",", "");
                        }
                    }
                    object[] searchObject  = new object[] {
                        Utility.KillSqlInjection(ddlTransactionType.SelectedValue.Trim()),
                        Utility.KillSqlInjection(textLimit),
                        Utility.KillSqlInjection(ddlIsreversal.SelectedValue.Trim()),
                        Utility.KillSqlInjection(ddlUnit.SelectedValue.Trim())};
                  

                    DataSet ds = _service.common("SEMS_RRT_LIMIT_INS", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
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
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    if (checktxtlimit.Checked)
                    {
                        txtLimit.Text = null;
                    }
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

                    if (txtLimit.Text.Trim().Equals(string.Empty) && ddlIsreversal.SelectedValue == "1" && checktxtlimit.Checked == false)
                    {
                        lblError.Text = Resources.labels.hanmuc + " cannot be null";
                        txtLimit.BorderColor = System.Drawing.Color.Red;
                        txtLimit.Focus();
                        return;
                    }
                    string textLimit = string.Empty;
                    if (ddlIsreversal.SelectedValue == "0")
                    {
                        textLimit = "-1";
                    }
                    else
                    {
                        if (checktxtlimit.Checked == true)
                        {
                            textLimit = "-1";
                        }
                        else
                        {
                            textLimit = txtLimit.Text.Replace(",", "");
                        }
                    }
                    object[] searchObject = new object[] {
                        Utility.KillSqlInjection(ddlTransactionType.SelectedValue.Trim()),
                        Utility.KillSqlInjection(textLimit),
                        Utility.KillSqlInjection(ddlIsreversal.SelectedValue.Trim()),
                        Utility.KillSqlInjection(ddlUnit.SelectedValue.Trim())};

                    DataSet ds = _service.common("SEMS_RRT_LIMIT_UP", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0"))
                    {
                        lblError.Text = Resources.labels.thanhcong;
                        btsave.Enabled = false;
                        enableControlView(false);
                    }

                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    if (checktxtlimit.Checked)
                    {
                        txtLimit.Text = null;
                    }
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
            checktxtlimit.Checked = false;
            ddlIsreversal.SelectedValue = "1";

        }
        if (ACTION.Equals(IPC.ACTIONPAGE.EDIT))
        {
            txtLimit.Text = string.Empty;
            defaultColor();
            enableControlView(true);
            ddlTransactionType.Enabled = false;
            checktxtlimit.Checked = false;
            ddlIsreversal.SelectedValue = "1";
        }
        
    }
}
