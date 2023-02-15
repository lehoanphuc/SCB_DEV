using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;
using SmartPortal.Security;
using SmartPortal.ExceptionCollection;
using System.Collections;

public partial class Widgets_User_Controls_Widget : System.Web.UI.UserControl
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

    string IPCERRORCODE;
    string IPCERRORDESC;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //load policy to dropdownlist
                DataSet dspolicy = new DataSet();


                dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, "SEMS", Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
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

                //load chi nhánh
                //ddlBranch.DataSource = new SmartPortal.SEMS.Branch().GetAll(ref IPCERRORCODE, ref IPCERRORDESC);
                //ddlBranch.DataTextField = "BRANCHNAME";
                //ddlBranch.DataValueField = "BRANCHID";
                //ddlBranch.DataBind();

                //load tat ca level
                ddlLevel.DataSource = new SmartPortal.SEMS.BankUser().LoadAllLevel();
                ddlLevel.DataTextField = "DESCRIPTION";
                ddlLevel.DataValueField = "USERLEVEL";
                ddlLevel.DataBind();

               //load thông tin gender
                ListItem l1 = new ListItem(Resources.labels.male, "1");
                ListItem l2 = new ListItem(Resources.labels.female, "0");
                ddlGender.Items.Add(l1);
                ddlGender.Items.Add(l2);
                //load thông tin status
                ListItem l3 = new ListItem(Resources.labels.active, "1");
                ListItem l4 = new ListItem(Resources.labels.unactive, "0");
                ddlStatus.Items.Add(l3);
                ddlStatus.Items.Add(l4);

                //load product
                ddlProduct.DataSource = new SmartPortal.SEMS.Product().GetProductByCondition("", "", "B", "", ref IPCERRORCODE, ref IPCERRORDESC);
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ProductID";
                ddlProduct.DataBind();

                //truong hop edit load thong tin
                if (Request["uid"] != null)
                {
                    btOK.Attributes.Add("onclick", "return updatevalidate();");
                    btnSave1.Attributes.Add("onclick", "return updatevalidate();");

                    UsersBLL UB = new UsersBLL();
                    UsersModel UM = new UsersModel();

                    UM = UB.GetUserInfo(Request["uid"].ToString().Trim());

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
                    ddlpolicySEMS.SelectedValue = UM.policyid.ToString();

                    pnLogin.Visible = false;

                    //kiem tra neu view diable control
                    if (Request["type"] != null)
                    {
                        txtUserName.Enabled = false;
                        txtPass.Enabled = false;
                        txtRePass.Enabled = false;

                        txtFirstName.Enabled = false;
                        txtMiddleName.Enabled = false;
                        txtLastName.Enabled = false;

                        ddlGender.Enabled = false;
                        txtAddress.Enabled = false;
                        txtEmail.Enabled = false;
                        txtBirth.Enabled = false;
                        txtPhone.Enabled = false;
                        ddlStatus.Enabled = false;
                        ddlBranch.Enabled = false;
                        ddlLevel.Enabled = false;

                        btOK.Visible = false;
                        btnSave1.Visible = false;
                        imgSave.Visible = false;
                        imgSave1.Visible = false;
                        ddlpolicySEMS.Enabled = false;
                    }

                }
                else
                {
                    btOK.Attributes.Add("onclick", "return validate();");
                    btnSave1.Attributes.Add("onclick", "return validate();");
                }
            }

        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_User_Controls_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_User_Controls_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void txtCBLGName_Changed(object s, EventArgs e)
    {
        txtCBUserID.Text = "";
        txtUserName.Text = "";
        txtFirstName.Text = "";
        txtMiddleName.Text = "";
        txtLastName.Text = "";
    }
    protected void btnCheck_Click(object s, EventArgs e)
    {
        try
        {
            lblAlert.Text = "";
            //Hashtable hsRs = new SmartPortal.SEMS.User().GetCoreBankingUserInformation(SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCBLGName.Text.Trim()));
            //if (hsRs[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Equals("0"))
            //{
            //    if (hsRs.ContainsKey("USRID") & hsRs["USRID"] != null)
            //    {
            //        txtCBUserID.Text = hsRs["USRID"].ToString();
            //        txtUserName.Text = hsRs["LGNAME"].ToString();
            //        ddlBranch.SelectedValue = hsRs["BRANCHID"].ToString();
            //        string[] USRNAME = hsRs["USRNAME"].ToString().Split(' ');
            //        if (USRNAME.Length > 0) txtFirstName.Text = USRNAME[0]; else txtFirstName.Text = "";
            //        if (USRNAME.Length > 1) txtMiddleName.Text = USRNAME[1]; else txtMiddleName.Text = "";
            //        if (USRNAME.Length > 2) txtLastName.Text = USRNAME[2]; else txtLastName.Text = "";

            //    }
            //    else
            //    {
            //        txtCBUserID.Text = "";
            //        txtUserName.Text = "";
            //        txtFirstName.Text = "";
            //        txtMiddleName.Text = "";
            //        txtLastName.Text = "";
            //        lblAlert.Text = Resources.labels.corebankingusernotexist;
            //    }
            //}
            //else
            //{
            //    lblAlert.Text = Resources.labels.corebankingusernotexist;
            //}
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_User_Controls_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btOK_Click(object sender, EventArgs e)
    {
            if (string.IsNullOrEmpty(txtCBLGName.Text))
            {
                lblAlert.Text = Resources.labels.corebankingloginnameempty;
                return;
            }
            else if (string.IsNullOrEmpty(txtCBUserID.Text))
            {
                lblAlert.Text = Resources.labels.corebankinguseridempty;
                return;
            }
       
            if (Request["uid"] == null)
            {
                try
                {
                   
                    //insert
                    UsersBLL UB = new UsersBLL();
                    //UB.Insert(Utility.KillSqlInjection(txtUserName.Text.Trim()), Encryption.Encrypt(Utility.KillSqlInjection(txtPass.Text.Trim())), Utility.KillSqlInjection(txtFirstName.Text.Trim()), Utility.KillSqlInjection(txtMiddleName.Text.Trim()), Utility.KillSqlInjection(txtLastName.Text.Trim()), Utility.IsInt(ddlGender.SelectedValue), Utility.KillSqlInjection(txtAddress.Text.Trim()), Utility.KillSqlInjection(txtEmail.Text.Trim()), Utility.KillSqlInjection(txtBirth.Text.Trim()), Utility.KillSqlInjection(txtPhone.Text.Trim()), Utility.IsInt(ddlStatus.SelectedValue), HttpContext.Current.Session["userName"].ToString(), "", 'U' + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8), "TEL", "0", ddlProduct.SelectedValue.Trim(), Utility.KillSqlInjection(txtCoreUser.Text.Trim()), Utility.KillSqlInjection(txtCoreUser.Text.Trim()));
                    //UB.Insert(Utility.KillSqlInjection(txtUserName.Text.Trim()), Encryption.Encrypt(Utility.KillSqlInjection(txtPass.Text.Trim())), Utility.KillSqlInjection(txtFirstName.Text.Trim()), Utility.KillSqlInjection(txtMiddleName.Text.Trim()), Utility.KillSqlInjection(txtLastName.Text.Trim()),"", Utility.IsInt(ddlGender.SelectedValue), Utility.KillSqlInjection(txtAddress.Text.Trim()), Utility.KillSqlInjection(txtEmail.Text.Trim()), Utility.KillSqlInjection(txtBirth.Text.Trim()), Utility.KillSqlInjection(txtPhone.Text.Trim()), Utility.IsInt(ddlStatus.SelectedValue), HttpContext.Current.Session["userName"].ToString(), ddlBranch.SelectedValue, txtUserName.Text.Trim(), SmartPortal.Constant.IPC.TELLER, ddlLevel.SelectedValue.Trim(), ddlProduct.SelectedValue.Trim(), ddlpolicySEMS.SelectedValue.Trim());

                    //alert
                    lblAlert.Text = Resources.labels.insertsucessfull;
                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["USERNAME"], "", Utility.KillSqlInjection(txtUserName.Text.Trim()));
                    //SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["PASSWORD"], "", Encryption.Encrypt(Utility.KillSqlInjection(txtPass.Text.Trim())));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["PASSWORD"], "", SmartPortal.SEMS.O9Encryptpass.sha_sha256(Encryption.Encrypt(Utility.KillSqlInjection(txtPass.Text.Trim())), Utility.KillSqlInjection(txtUserName.Text.Trim())));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["FIRSTNAME"], "", Utility.KillSqlInjection(txtFirstName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["MIDDLENAME"], "", Utility.KillSqlInjection(txtMiddleName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["LASTNAME"], "", Utility.KillSqlInjection(txtLastName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["GENDER"], "", ddlGender.SelectedValue);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["ADDRESS"], "", Utility.KillSqlInjection(txtAddress.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["EMAIL"], "", Utility.KillSqlInjection(txtEmail.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["BIRTHDAY"], "", Utility.KillSqlInjection(txtEmail.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["PHONE"], "", Utility.KillSqlInjection(txtPhone.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["STATUS"], "", ddlStatus.SelectedValue);
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["USERCREATED"], "", HttpContext.Current.Session["userName"].ToString());

                    //reset textbox
                    txtUserName.Text = "";
                    txtPhone.Text = "";
                    txtRePass.Text = "";
                    txtFirstName.Text = "";
                    txtMiddleName.Text = "";
                    txtLastName.Text = "";
                    txtAddress.Text = "";
                    txtEmail.Text = "";
                    txtBirth.Text = "";
                    txtPhone.Text = "";
                }
                catch (BusinessExeption bex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["useriec"], "Widgets_User_Controls_Widget", "btOK_Click", bex.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["useriec"], Request.Url.Query);
                }
                catch (SQLException sex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_User_Controls_Widget", "btOK_Click", sex.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_User_Controls_Widget", "btOK_Click", ex.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                }
            }
            else
            {
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
                    //UB.Update(Utility.KillSqlInjection(txtUserName.Text.Trim()), Utility.KillSqlInjection(txtFirstName.Text.Trim()), Utility.KillSqlInjection(txtMiddleName.Text.Trim()), Utility.KillSqlInjection(txtLastName.Text.Trim()), Utility.IsInt(ddlGender.SelectedValue), Utility.KillSqlInjection(txtAddress.Text.Trim()), Utility.KillSqlInjection(txtEmail.Text.Trim()), dt, Utility.KillSqlInjection(txtPhone.Text.Trim()), Utility.IsInt(ddlStatus.SelectedValue), HttpContext.Current.Session["userName"].ToString(), dM, "", "0", ddlProduct.SelectedValue.Trim(), Utility.KillSqlInjection(txtCoreUser.Text.Trim()), Utility.KillSqlInjection(txtCoreUser.Text.Trim()));
                    //UB.Update(Utility.KillSqlInjection(txtUserName.Text.Trim()), Utility.KillSqlInjection(txtFirstName.Text.Trim()), Utility.KillSqlInjection(txtMiddleName.Text.Trim()), Utility.KillSqlInjection(txtLastName.Text.Trim()),"", Utility.IsInt(ddlGender.SelectedValue), Utility.KillSqlInjection(txtAddress.Text.Trim()), Utility.KillSqlInjection(txtEmail.Text.Trim()), dt, Utility.KillSqlInjection(txtPhone.Text.Trim()), Utility.IsInt(ddlStatus.SelectedValue), HttpContext.Current.Session["userName"].ToString(), dM, ddlBranch.SelectedValue, ddlLevel.SelectedValue.Trim(), ddlProduct.SelectedValue.Trim(), ddlpolicySEMS.SelectedValue.Trim());

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
                    
                   
                }
                catch (BusinessExeption bex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["useruec"], "Widgets_User_Controls_Widget", "btOK_Click", bex.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["useruec"], Request.Url.Query);
                }
                catch (SQLException sex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_User_Controls_Widget", "btOK_Click", sex.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_User_Controls_Widget", "btOK_Click", ex.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                }
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewuser"]));
            }

        
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewuser"]));
    }
}
