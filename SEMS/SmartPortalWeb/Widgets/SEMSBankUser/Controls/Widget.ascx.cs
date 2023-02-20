using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;
using SmartPortal.Security;
using SmartPortal.ExceptionCollection;
using System.Data;
using SmartPortal.Constant;

public partial class Widgets_SEMSBankUser_Controls_Widget : WidgetBase
{
    static string userName;
    static string firstname;
    static string middlename;
    static string lastname;
    static string gender;
    static string address;
    static string email;
    static string birthday;
    static string phone;
    static string status;
    static string usermodi;
    static string datemodi;
    private string ACTION = string.Empty;
    string IPCERRORCODE;
    string IPCERRORDESC;
    static string prevPage = String.Empty;

    public string _TITLE
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                LoadDll();
                BindData();
                prevPage = Request.UrlReferrer.ToString();
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_User_Controls_Widget", "Page_Load", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_User_Controls_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void LoadDll()
    {
        DataSet dspolicy = new DataSet();
        dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, IPC.SEMS, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (dspolicy.Tables[0].Rows.Count == 0)
            {
                lblAlert.Text = string.Format(Resources.labels.haythempolicydefaultchodichvutruoc, "SEMS");
                return;
            }
            ddlpolicySEMS.DataSource = dspolicy.Tables[0];
            ddlpolicySEMS.DataTextField = "policytx";
            ddlpolicySEMS.DataValueField = "policyid";
            ddlpolicySEMS.DataBind();
        }

        ddlBranch.DataSource = new SmartPortal.SEMS.Branch().GetAll(ref IPCERRORCODE, ref IPCERRORDESC);
        ddlBranch.DataTextField = "BRANCHNAME";
        ddlBranch.DataValueField = "BRANCHID";
        ddlBranch.DataBind();

        ddlLevel.DataSource = new SmartPortal.SEMS.BankUser().LoadAllLevel();
        ddlLevel.DataTextField = "DESCRIPTION";
        ddlLevel.DataValueField = "USERLEVEL";
        ddlLevel.DataBind();

        ListItem l1 = new ListItem(Resources.labels.male, "1");
        ListItem l2 = new ListItem(Resources.labels.female, "0");
        ddlGender.Items.Add(l1);
        ddlGender.Items.Add(l2);

        ListItem l3 = new ListItem(Resources.labels.active, "1");
        ListItem l4 = new ListItem(Resources.labels.inactive, "0");
        ddlStatus.Items.Add(l3);
        ddlStatus.Items.Add(l4);

        ddlUserType.DataSource = new SmartPortal.SEMS.BankUser().LoadUserType();
        ddlUserType.DataTextField = "SubUserType";
        ddlUserType.DataValueField = "SubUserCode";
        ddlUserType.DataBind();
    }
    void BindData()
    {
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                DataTable dtRole = new RoleBLL().LoadForUser(string.Empty, IPC.SEMS, Session["userName"].ToString().Trim());
                rptRole.DataSource = dtRole;
                rptRole.DataBind();
                btnSaveAdd.Visible = true;
                btnSave.Visible = false;
                break;
            case IPC.ACTIONPAGE.DETAILS:
                GetInfo();
                btnSave.Enabled = false;
                btnSaveAdd.Visible = false;
                btnSave.Visible = true;
                pnProfile.Enabled = false;
                pnRole.Enabled = false;
                btnClear.Enabled = false;
                break;
            case IPC.ACTIONPAGE.EDIT:
                GetInfo();
                pnProfile.Enabled = true;
                btnSave.Enabled = true;
                btnSave.Visible = true;
                btnSaveAdd.Visible = false;
                btnClear.Enabled = false;
                break;
        }
    }
    void GetInfo()
    {
        string ID = GetParamsPage(IPC.ID)[0].Trim();
        UsersBLL UB = new UsersBLL();
        UsersModel UM = new UsersModel();
        UM = UB.GetUserInfo(ID);
        txtUserName.Text = UM.UserName;
        userName = UM.UserName;
        txtPass.Text = UM.Password;
        txtRePass.Text = UM.Password;
        txtFirstName.Text = UM.FirstName;
        firstname = UM.FirstName;
        txtMiddleName.Text = UM.MiddleName;
        middlename = UM.MiddleName;
        txtLastName.Text = UM.LastName;
        lastname = UM.LastName;
        ddlGender.SelectedValue = UM.Gender.ToString();
        gender = ddlGender.SelectedValue;
        txtAddress.Text = UM.Address;
        address = UM.Address;
        txtEmail.Text = UM.Email;
        email = UM.Email;
        txtBirth.Text = UM.Birthday;
        birthday = UM.Birthday;
        txtPhone.Text = UM.Phone;
        phone = UM.Phone;
        ddlStatus.SelectedValue = UM.Status.ToString();
        status = UM.Status.ToString();
        usermodi = UM.UserModified;
        datemodi = UM.DateModified;
        ddlBranch.SelectedValue = UM.Branch;
        ddlLevel.SelectedValue = UM.Level.Trim();
        ddlpolicySEMS.SelectedValue = UM.policyid;
        DataTable dtRole = new RoleBLL().LoadForUser(UM.UserName, IPC.SEMS, Session["userName"].ToString().Trim());
        rptRole.DataSource = dtRole;
        rptRole.DataBind();
        pnLogin.Visible = false;
    }
    protected void btOK_Click(object sender, EventArgs e)
    {
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                try
                {
                    //insert
                    UsersBLL UB = new UsersBLL();
                    UB.Insert(Utility.KillSqlInjection(txtUserName.Text.Trim()),
                        SmartPortal.SEMS.O9Encryptpass.sha_sha256(Utility.KillSqlInjection(txtPass.Text.Trim()),
                            Utility.KillSqlInjection(txtUserName.Text.Trim())),
                        Utility.KillSqlInjection(txtFirstName.Text.Trim()),
                        Utility.KillSqlInjection(txtMiddleName.Text.Trim()),
                        Utility.KillSqlInjection(txtLastName.Text.Trim()), Utility.IsInt(ddlGender.SelectedValue),
                        Utility.KillSqlInjection(txtAddress.Text.Trim()),
                        Utility.KillSqlInjection(txtEmail.Text.Trim()), Utility.KillSqlInjection(txtBirth.Text.Trim()),
                        Utility.KillSqlInjection(txtPhone.Text.Trim()), Utility.IsInt(ddlStatus.SelectedValue),
                        HttpContext.Current.Session["userName"].ToString(), ddlBranch.SelectedValue,
                        txtUserName.Text.Trim(), Utility.KillSqlInjection(ddlUserType.SelectedValue.Trim()), Utility.KillSqlInjection(ddlLevel.SelectedValue.Trim()),
                        ddlpolicySEMS.SelectedValue.Trim());
                    //alert
                    lblAlert.Text = Resources.labels.insertsucessfull;
                    pnLogin.Enabled = false;
                    pnProfile.Enabled = false;
                    pnRole.Enabled = false;
                    btnSaveAdd.Enabled = false;
                    btnSave.Visible = false;
                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(
                        System.Configuration.ConfigurationManager.AppSettings["USER"],
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(),
                        Session["userName"].ToString(), Request.UserHostAddress,
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],
                        System.Configuration.ConfigurationManager.AppSettings["USERNAME"], "",
                        Utility.KillSqlInjection(txtUserName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(
                        System.Configuration.ConfigurationManager.AppSettings["USER"],
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(),
                        Session["userName"].ToString(), Request.UserHostAddress,
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],
                        System.Configuration.ConfigurationManager.AppSettings["PASSWORD"], "",
                        SmartPortal.SEMS.O9Encryptpass.sha_sha256(
                            Encryption.Encrypt(Utility.KillSqlInjection(txtPass.Text.Trim())),
                            Utility.KillSqlInjection(txtUserName.Text.Trim())));
                    SmartPortal.Common.Log.WriteLogDatabase(
                        System.Configuration.ConfigurationManager.AppSettings["USER"],
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(),
                        Session["userName"].ToString(), Request.UserHostAddress,
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],
                        System.Configuration.ConfigurationManager.AppSettings["FIRSTNAME"], "",
                        Utility.KillSqlInjection(txtFirstName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(
                        System.Configuration.ConfigurationManager.AppSettings["USER"],
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(),
                        Session["userName"].ToString(), Request.UserHostAddress,
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],
                        System.Configuration.ConfigurationManager.AppSettings["MIDDLENAME"], "",
                        Utility.KillSqlInjection(txtMiddleName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(
                        System.Configuration.ConfigurationManager.AppSettings["USER"],
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(),
                        Session["userName"].ToString(), Request.UserHostAddress,
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],
                        System.Configuration.ConfigurationManager.AppSettings["LASTNAME"], "",
                        Utility.KillSqlInjection(txtLastName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(
                        System.Configuration.ConfigurationManager.AppSettings["USER"],
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(),
                        Session["userName"].ToString(), Request.UserHostAddress,
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],
                        System.Configuration.ConfigurationManager.AppSettings["GENDER"], "", ddlGender.SelectedValue);
                    SmartPortal.Common.Log.WriteLogDatabase(
                        System.Configuration.ConfigurationManager.AppSettings["USER"],
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(),
                        Session["userName"].ToString(), Request.UserHostAddress,
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],
                        System.Configuration.ConfigurationManager.AppSettings["ADDRESS"], "",
                        Utility.KillSqlInjection(txtAddress.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(
                        System.Configuration.ConfigurationManager.AppSettings["USER"],
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(),
                        Session["userName"].ToString(), Request.UserHostAddress,
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],
                        System.Configuration.ConfigurationManager.AppSettings["EMAIL"], "",
                        Utility.KillSqlInjection(txtEmail.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(
                        System.Configuration.ConfigurationManager.AppSettings["USER"],
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(),
                        Session["userName"].ToString(), Request.UserHostAddress,
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],
                        System.Configuration.ConfigurationManager.AppSettings["BIRTHDAY"], "",
                        Utility.KillSqlInjection(txtEmail.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(
                        System.Configuration.ConfigurationManager.AppSettings["USER"],
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(),
                        Session["userName"].ToString(), Request.UserHostAddress,
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],
                        System.Configuration.ConfigurationManager.AppSettings["PHONE"], "",
                        Utility.KillSqlInjection(txtPhone.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(
                        System.Configuration.ConfigurationManager.AppSettings["USER"],
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(),
                        Session["userName"].ToString(), Request.UserHostAddress,
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],
                        System.Configuration.ConfigurationManager.AppSettings["STATUS"], "", ddlStatus.SelectedValue);
                    SmartPortal.Common.Log.WriteLogDatabase(
                        System.Configuration.ConfigurationManager.AppSettings["USER"],
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(),
                        Session["userName"].ToString(), Request.UserHostAddress,
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSER"],
                        System.Configuration.ConfigurationManager.AppSettings["USERCREATED"], "",
                        HttpContext.Current.Session["userName"].ToString());
                }
                catch (BusinessExeption bex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["useriec"],
                        "Widgets_User_Controls_Widget", "btOK_Click", bex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(
                        System.Configuration.ConfigurationManager.AppSettings["useriec"], Request.Url.Query);
                }
                catch (SQLException sex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"],
                        "Widgets_User_Controls_Widget", "btOK_Click", sex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"],
                        Request.Url.Query);
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                        "Widgets_User_Controls_Widget", "btOK_Click", ex.ToString(), Request.Url.Query);
                    lblAlert.Text = "Username " + txtUserName.Text.Trim() +
                                    " already existed in database. Please input other username.";
                }
                break;
            case IPC.ACTIONPAGE.EDIT:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                //update
                try
                {
                    string dt = DateTime.Now.ToString("dd/MM/yyyy");
                    if (txtBirth.Text != "")
                    {
                        dt = txtBirth.Text.Trim();
                    }
                    //insert
                    UsersBLL UB = new UsersBLL();
                    string dM = DateTime.Now.ToString("dd/MM/yyyy");
                    UB.Update(Utility.KillSqlInjection(txtUserName.Text.Trim()), Utility.KillSqlInjection(txtFirstName.Text.Trim()), Utility.KillSqlInjection(txtMiddleName.Text.Trim()), Utility.KillSqlInjection(txtLastName.Text.Trim()), Utility.IsInt(ddlGender.SelectedValue), Utility.KillSqlInjection(txtAddress.Text.Trim()), Utility.KillSqlInjection(txtEmail.Text.Trim()), dt, Utility.KillSqlInjection(txtPhone.Text.Trim()), Utility.IsInt(ddlStatus.SelectedValue), HttpContext.Current.Session["userName"].ToString(), dM, ddlBranch.SelectedValue, ddlLevel.SelectedValue.Trim(), ddlpolicySEMS.SelectedValue.Trim());
                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["USERNAME"], userName, Utility.KillSqlInjection(txtUserName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["FIRSTNAME"], firstname, Utility.KillSqlInjection(txtFirstName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["MIDDLENAME"], middlename, Utility.KillSqlInjection(txtMiddleName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["LASTNAME"], lastname, Utility.KillSqlInjection(txtLastName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["GENDER"], gender, ddlGender.SelectedValue);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["ADDRESS"], address, Utility.KillSqlInjection(txtAddress.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["EMAIL"], email, Utility.KillSqlInjection(txtEmail.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["BIRTHDAY"], birthday, Utility.KillSqlInjection(txtBirth.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["PHONE"], phone, Utility.KillSqlInjection(txtPhone.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["UPDATE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["STATUS"], status, ddlStatus.SelectedValue);
                    lblAlert.Text = Resources.labels.updatesuccessfully;
                    pnLogin.Enabled = false;
                    pnProfile.Enabled = false;
                    pnRole.Enabled = false;
                    btnSaveAdd.Visible = false;
                    btnSave.Enabled = false;
                }
                catch (BusinessExeption bex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["useruec"], "Widgets_User_Controls_Widget", "btOK_Click", bex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["useruec"], Request.Url.Query);
                }
                catch (SQLException sex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_User_Controls_Widget", "btOK_Click", sex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_User_Controls_Widget", "btOK_Click", ex.ToString(), Request.Url.Query);
                    lblAlert.Text = ex.ToString();
                }
                break;
        }

        CheckBox cbRoleName, cbRoleNameTemp;
        HiddenField hdRole;
        UserInRole UIR = new UserInRole();
        foreach (RepeaterItem item in rptRole.Items)
        {
            cbRoleName = item.FindControl("cbRole") as CheckBox;
            hdRole = item.FindControl("hdRole") as HiddenField;
            cbRoleNameTemp = item.FindControl("cbRoleTemp") as CheckBox;
            if (cbRoleName.Checked == true)
            {
                if (!cbRoleNameTemp.Checked)
                {
                    UIR.Insert(Utility.IsInt(hdRole.Value), Utility.KillSqlInjection(txtUserName.Text.Trim()), true);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USERINROLE"], 
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], 
                        Request.Url.ToString(), Session["userName"].ToString(),
                        Request.UserHostAddress, 
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSERINROLE"], 
                        System.Configuration.ConfigurationManager.AppSettings["ROLEID"], 
                        IPC.NEW,
                        cbRoleName.Text);
                }
            }
            else
            {
                if (cbRoleNameTemp.Checked)
                {
                    UIR.Insert(Utility.IsInt(hdRole.Value), Utility.KillSqlInjection(txtUserName.Text.Trim()), false);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USERINROLE"], 
                        System.Configuration.ConfigurationManager.AppSettings["INSERT"], 
                        Request.Url.ToString(), 
                        Session["userName"].ToString(),
                        Request.UserHostAddress, 
                        System.Configuration.ConfigurationManager.AppSettings["TBLUSERINROLE"], 
                        System.Configuration.ConfigurationManager.AppSettings["ROLEID"], 
                        IPC.DELETE,
                        cbRoleName.Text);
                }
            }

            
        }
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(prevPage);
        //if (Request.QueryString["returnUrl"] != null)
        //{
        //    Response.Redirect(Request.QueryString["returnUrl"]);
        //}
        //else
        //{
        //    RedirectBackToMainPage();
        //}
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        lblAlert.Text = string.Empty;
        pnLogin.Enabled = true;
        pnProfile.Enabled = true;
        pnRole.Enabled = true;
        btnSaveAdd.Enabled = true;
        txtUserName.Text = string.Empty;
        txtPass.Text = string.Empty;
        txtRePass.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtMiddleName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        ddlGender.SelectedIndex = 0;
        txtAddress.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtBirth.Text = string.Empty;
        txtPhone.Text = string.Empty;
        ddlStatus.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlLevel.SelectedIndex = 0;
        ddlpolicySEMS.SelectedIndex = 0;
        CheckBox cbRoleName;
        foreach (RepeaterItem item in rptRole.Items)
        {
            cbRoleName = item.FindControl("cbRole") as CheckBox;
            cbRoleName.Checked = false;
        }
    }
}
