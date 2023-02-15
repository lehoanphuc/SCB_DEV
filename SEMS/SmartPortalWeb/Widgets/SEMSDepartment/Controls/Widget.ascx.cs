using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Web;
using SmartPortal.Constant;

public partial class Widgets_SEMSAgentBank_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string UserID = "";
    private SmartPortal.SEMS.Common _common;
    public Widgets_SEMSAgentBank_Controls_Widget()
    {
        _common = new SmartPortal.SEMS.Common();

    }


    public string _TITLE
    {
        get { return lblTitleAgentBank.Text; }
        set { lblTitleAgentBank.Text = value; }
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
        txtBrachID.SetDefaultBranch();
        txtDepartmentCode.BorderColor = System.Drawing.Color.Empty;
        txtDepartmentName.BorderColor = System.Drawing.Color.Empty;
        ddStatus.BorderColor = System.Drawing.Color.Empty;

    }

    void enableControlView()
    {
        pnAdd.Enabled = false;
        txtBrachID.Enabled = false;
        btback.Visible = true;
        btsave.Visible = false;
        btClear.Visible = false;
    }
   
    void enableControlEdit()
    {
        txtDepartmentCode.Enabled = false;
        txtApproveby.Enabled = false;
        txtCreatedate.Enabled = false;
        txtCreateby.Enabled = false;
        txtLastmodifydate.Enabled = false;
        btback.Visible = true;
        btsave.Visible = true;
    }
    void enableControlAdd()
    {
       
        fcreatedate.Visible = false;
        fcreateby.Visible = false;
        fmodifydate.Visible = false;
        fapproveby.Visible = false;
        
        btback.Visible = true;
        btsave.Visible = true;
    }
 
    private void loadStatus()
    {
        DataSet ds = new DataSet();
        ds = _common.GetValueList("WAL_DEPARTMENT", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddStatus.DataSource = ds;
                ddStatus.DataValueField = "VALUEID";
                ddStatus.DataTextField = "CAPTION";
                ddStatus.DataBind();
            }
        }
    }

    private void loadAllCombobox()
    {
        loadStatus();
    }

    void loadEditAndViewData()
    {
        string DepartmentID = GetParamsPage(IPC.ID)[0].Trim();
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { DepartmentID };
        ds = _common.common("SEMS_DEPART_GET", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        //ds = _service.GetFunction(bankID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable tb = ds.Tables[0];
                txtDepartmentCode.Text = tb.Rows[0]["DeprtCode"].ToString();
                //txtBrachID.Text = tb.Rows[0]["BRANCH_ID"].ToString();
                txtDepartmentName.Text = tb.Rows[0]["DeprtName"].ToString();
                ddStatus.SelectedValue = tb.Rows[0]["Status"].ToString();

                txtCreatedate.Text = tb.Rows[0]["created"].ToString();
                txtLastmodifydate.Text = tb.Rows[0]["lastmo"].ToString();
                txtCreateby.Text = tb.Rows[0]["UserCreated"].ToString();
                txtApproveby.Text = tb.Rows[0]["UserApproved"].ToString();
                txtBrachID.setBranchID(tb.Rows[0]["BranchID"].ToString());
                txtBrachID.Text = tb.Rows[0]["BranchID"].ToString() +" - "+ tb.Rows[0]["BranchName"].ToString();
            }
        }
        
    }
    void BindData()
    {
        try
        {
            loadAllCombobox();
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
            lblError.Text = ex.Message;
        }
    }
    public bool IsEmailValid(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);
            return true;
        }
        catch (FormatException)
        {
            return false;
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
    private bool checkvalidate()
    {
        #region Validation
        defaultColor();
        if (txtDepartmentCode.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.departmentcode + " is not null";
            txtDepartmentCode.BorderColor = System.Drawing.Color.Red;
            txtDepartmentCode.Focus();
            return false;
        }
        if (IsUnicode(txtDepartmentCode.Text))
        {
            lblError.Text = Resources.labels.departmentcode + " is not allowed to have unicode characters";
            txtDepartmentCode.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (txtDepartmentCode.Text.Trim().Contains(" "))
        {
            lblError.Text = Resources.labels.departmentcode + " is not allowed to have whitespace characters";
            txtDepartmentCode.BorderColor = System.Drawing.Color.Red;
            return false;
        }
        if (txtDepartmentName.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.departmentname + "  is not null";
            txtDepartmentName.BorderColor = System.Drawing.Color.Red;
            txtDepartmentName.Focus();
            return false;
        }
        if (txtBrachID.Text.Equals(string.Empty))
        {
            lblError.Text = Resources.labels.branchcode + " is not null";
            txtBrachID.SetErrorBranch();
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

    protected void btsave_Click(object sender, EventArgs e)
    {
        UserID = HttpContext.Current.Session["userID"].ToString();
    
        if (!checkvalidate())
            return;
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
                    return;
                try
                {
                    DataSet ds = new DataSet();
                    object[] searchObject = new object[] {
                        txtDepartmentCode.Text,
                        txtDepartmentName.Text,
                        txtBrachID.getId(),
                        ddStatus.SelectedValue,
                        UserID,     //user created
                    };
                    ds = _common.common("SEMS_DEPART_ADD", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.addsuccessfully;
                        btsave.Enabled = false;
                        pnAdd.Enabled = false;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
                }
                break;
            case IPC.ACTIONPAGE.EDIT:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                try
                {
                    string DepartmentID = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet ds = new DataSet();
                    object[] searchObject = new object[] {
                        DepartmentID,
                        txtDepartmentCode.Text,
                        txtBrachID.getId(),
                        txtDepartmentName.Text,
                        ddStatus.SelectedValue,
                        UserID,     //user modify
                    };
                    ds = _common.common("SEMS_DEPART_UP", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
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
    protected void btclear_Click(object sender, EventArgs e)
    {
        ACTION = GetActionPage();
        if (ACTION== IPC.ACTIONPAGE.ADD)
        {
            txtDepartmentCode.Text = string.Empty;
            txtCreateby.Text = string.Empty;
            txtCreatedate.Text = string.Empty;
            txtLastmodifydate.Text = string.Empty;
            txtApproveby.Text = string.Empty;
        }
        txtBrachID.Text = string.Empty;
        txtDepartmentName.Text = string.Empty;
        ddStatus.SelectedValue = string.Empty;
        pnAdd.Enabled = true;
        lblError.Text = string.Empty;
        btsave.Enabled = true;
    }
    void SetParaInsert(Dictionary<object, object> paraInsert)
    {
        string UserID = HttpContext.Current.Session["userID"].ToString();
        paraInsert.Add("DeprtCode", txtDepartmentCode.Text);
        paraInsert.Add("DeprtName", txtDepartmentName.Text);
        paraInsert.Add("BranchID", txtBrachID.getId());
        paraInsert.Add("Status", ddStatus.SelectedValue);
        paraInsert.Add("UserCreated", UserID); //user created
        //SEMS_DEPART_ADD|5|DeprtCode|DeprtName|BranchID|Status|UserCreated
    }
    void SetParaUpdate(Dictionary<object, object> paraUpdate)
    {
        string DepartmentID = GetParamsPage(IPC.ID)[0].Trim();
        string UserID = HttpContext.Current.Session["userID"].ToString();
        paraUpdate.Add("DeprtID", DepartmentID);
        paraUpdate.Add("DeprtCode", txtDepartmentCode.Text);
        paraUpdate.Add("BranchID", txtBrachID.getId());
        paraUpdate.Add("DeprtName", txtDepartmentName.Text);
        paraUpdate.Add("Status", ddStatus.SelectedValue);
        paraUpdate.Add("UserModified", UserID); //user modified
        //SEMS_DEPART_UP|5|DeprtID|DeprtCode|BranchID|DeprtName|Status|UserModified
    }


}
