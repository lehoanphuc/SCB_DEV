using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class Widgets_SEMSReason_Controls_General : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";


    SmartPortal.SEMS.Common _common = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            ACTION = GetActionPage();
            ViewState["UserID"] = HttpContext.Current.Session["userID"].ToString();
            if (!IsPostBack)
            {
                loadCombobox();
                setControldefault();
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void loadReasonType()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("WAL_REASON_DEFINITION", "TYPE", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddreasontype.DataSource = ds;
                ddreasontype.DataValueField = "VALUEID";
                ddreasontype.DataTextField = "CAPTION";
                ddreasontype.DataBind();
                ddreasontype.SelectedValue = "IND";
            }
        }
    }
    private void loadReasonAction()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("WAL_REASON_DEFINITION", "ACTION", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddreasonaction.DataSource = ds;
                ddreasonaction.DataValueField = "VALUEID";
                ddreasonaction.DataTextField = "CAPTION";
                ddreasonaction.DataBind();
                ddreasonaction.SelectedValue = "R";
            }
        }
    }
    private void loadEvent()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("WAL_REASON_DEFINITION", "EVENT", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddEvent.DataSource = ds;
                ddEvent.DataValueField = "VALUEID";
                ddEvent.DataTextField = "CAPTION";
                ddEvent.DataBind();
                ddEvent.SelectedValue = "R";
            }
        }
    }
    private void loadStatus()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("WAL_REASON_DEFINITION", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddStatus.DataSource = ds;
                ddStatus.DataValueField = "VALUEID";
                ddStatus.DataTextField = "CAPTION";
                ddStatus.DataBind();
                ddStatus.SelectedValue = "A";
            }
        }
    }

    private void loadCombobox()
    {
        loadReasonAction();
        loadReasonType();
        loadEvent();
        loadStatus();
    }
    void loadEditAndViewData()
    {
        string DepartmentID = GetParamsPage(IPC.ID)[0].Trim();
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { Utility.KillSqlInjection(DepartmentID) };
        ds = _common.common("SEMS_REASON_GET", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        //ds = _service.GetFunction(bankID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable tb = ds.Tables[0];
                txtReasonsCode.Text = tb.Rows[0]["ReasonCode"].ToString();
                txtReasonName.Text = tb.Rows[0]["ReasonName"].ToString();
                txtDesc.Text = tb.Rows[0]["Description"].ToString();

                ddreasonaction.SelectedValue = tb.Rows[0]["ReasonAction"].ToString();
                ddreasontype.SelectedValue = tb.Rows[0]["ReasonType"].ToString();
                ddEvent.SelectedValue = tb.Rows[0]["ReasonEvent"].ToString();
                ddStatus.SelectedValue = tb.Rows[0]["Status"].ToString();
            }
        }
    }
    void enableControlEdit()
    {
        txtReasonsCode.Enabled = false;
        btback.Visible = true;
        btsave.Visible = true;
    }
    void enableControlView()
    {
        txtReasonsCode.Enabled = false;
        btClear.Visible = false;
        btsave.Visible = false;
        pnGeneral.Enabled = false;
        btback.Enabled = true;
    }
    void enableControlAdd()
    {
        btback.Visible = true;
        btsave.Visible = true;
    }
    public void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    enableControlAdd();
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    loadEditAndViewData();
                    enableControlView();
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    loadEditAndViewData();
                    enableControlEdit();
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }


    public void setControldefault()
    {
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                break;
            case IPC.ACTIONPAGE.EDIT:
                enableControlEdit();
                break;
            case IPC.ACTIONPAGE.DETAILS:
                enableControlView();
                break;
        }
    }
    void defaultColor()
    {
        txtReasonsCode.BorderColor = System.Drawing.Color.Empty;
        txtReasonName.BorderColor = System.Drawing.Color.Empty;
        txtDesc.BorderColor = System.Drawing.Color.Empty;
        ddEvent.BorderColor = System.Drawing.Color.Empty;
        ddStatus.BorderColor = System.Drawing.Color.Empty;
        ddreasonaction.BorderColor = System.Drawing.Color.Empty;
        ddreasontype.BorderColor = System.Drawing.Color.Empty;
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
    private bool checkvalidate()
    {
        #region Validation
        defaultColor();
        if (txtReasonsCode.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.ReasonCode + " is not null";
            txtReasonsCode.BorderColor = System.Drawing.Color.Red;
            txtReasonsCode.Focus();
            return false;
        }
        if (txtReasonsCode.Text.Trim().Contains(" "))
        {
            lblError.Text = Resources.labels.ReasonCode + " is not allowed to have whitespace characters";
            txtReasonsCode.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (IsUnicode(txtReasonsCode.Text))
        {
            lblError.Text = Resources.labels.ReasonCode + " is not allowed to have unicode characters";
            txtReasonsCode.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (txtReasonName.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.ReasonName + "  is not null";
            txtReasonName.BorderColor = System.Drawing.Color.Red;
            txtReasonName.Focus();
            return false;
        }
        if (ddreasonaction.SelectedValue.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.reasonAction + " is not null";
            ddreasonaction.BorderColor = System.Drawing.Color.Red;
            ddreasonaction.Focus();
            return false;
        }
        if (ddreasontype.SelectedValue.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.reasonType + " is not null";
            ddreasontype.BorderColor = System.Drawing.Color.Red;
            ddreasontype.Focus();
            return false;
        }
        if (ddEvent.SelectedValue.Equals(string.Empty))
        {
            lblError.Text = Resources.labels._event + " is not null";
            ddEvent.BorderColor = System.Drawing.Color.Red;
            ddEvent.Focus();
            return false;
        }
        if (ddStatus.SelectedValue.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.status + " is not null";
            ddStatus.BorderColor = System.Drawing.Color.Red;
            ddStatus.Focus();
            return false;
        }
        return true;
        #endregion

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!checkvalidate())
            return;
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
                    return;
                try
                {
                    Dictionary<object, object> _insert = new Dictionary<object, object>();
                    SetParaInsert(_insert);
                    DataSet ds = new DataSet();
                    ds = _common.CallStore("SEMS_REASON_ADD", _insert, "Insert reason definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.addsuccessfully;
                        btsave.Enabled = false;
                        pnGeneral.Enabled = false;
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
                    Dictionary<object, object> _update = new Dictionary<object, object>();
                    SetParaUpdate(_update);
                    DataSet ds = new DataSet();
                    ds = _common.CallStore("SEMS_REASON_EDIT", _update, "Update reason definition", "N", ref IPCERRORCODE, ref IPCERRORDESC);
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
                        pnGeneral.Enabled = false;
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


    protected void btnBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected override void Render(HtmlTextWriter writer)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class=\"content \" >");
        sb.Append("<div>");
        writer.Write(sb.ToString());
        base.Render(writer);
        writer.Write("</div>");
    }
    protected void btclear_Click(object sender, EventArgs e)
    {
        if (ACTION == IPC.ACTIONPAGE.ADD)
        {
            txtReasonsCode.Text = string.Empty;
        }
        txtReasonName.Text = string.Empty;
        txtDesc.Text = string.Empty;
        defaultColor();
        pnGeneral.Enabled = true;
        lblError.Text = string.Empty;
        btsave.Enabled = true;
    }
    void SetParaInsert(Dictionary<object, object> paraInsert)
    {
        string UserID = HttpContext.Current.Session["userID"].ToString();
        paraInsert.Add("ReasonCode", Utility.KillSqlInjection(txtReasonsCode.Text.Trim()));
        paraInsert.Add("ReasonName", Utility.KillSqlInjection(txtReasonName.Text.Trim()));
        paraInsert.Add("ReasonAction", Utility.KillSqlInjection(ddreasonaction.SelectedValue));
        paraInsert.Add("ReasonType", Utility.KillSqlInjection(ddreasontype.SelectedValue));
        paraInsert.Add("ReasonEvent", Utility.KillSqlInjection(ddEvent.SelectedValue));
        paraInsert.Add("Status", Utility.KillSqlInjection(ddStatus.SelectedValue));
        paraInsert.Add("Description", Utility.KillSqlInjection(txtDesc.Text));
        paraInsert.Add("userID", Utility.KillSqlInjection(UserID));

    }
    void SetParaUpdate(Dictionary<object, object> paraUpdate)
    {
        string ReasonsID = GetParamsPage(IPC.ID)[0].Trim();
        string UserID = HttpContext.Current.Session["userID"].ToString();
        paraUpdate.Add("ReasonID", Utility.KillSqlInjection(ReasonsID));
        paraUpdate.Add("ReasonCode", Utility.KillSqlInjection(txtReasonsCode.Text.Trim()));
        paraUpdate.Add("ReasonName", Utility.KillSqlInjection(txtReasonName.Text.Trim()));
        paraUpdate.Add("ReasonAction", Utility.KillSqlInjection(ddreasonaction.SelectedValue));
        paraUpdate.Add("ReasonType", Utility.KillSqlInjection(ddreasontype.SelectedValue));
        paraUpdate.Add("ReasonEvent", Utility.KillSqlInjection(ddEvent.SelectedValue));
        paraUpdate.Add("Status", Utility.KillSqlInjection(ddStatus.SelectedValue));
        paraUpdate.Add("Description", Utility.KillSqlInjection(txtDesc.Text.Trim()));
        paraUpdate.Add("userID", Utility.KillSqlInjection(UserID));
    }

}