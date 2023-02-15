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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
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
                //truong hop edit load thong tin
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] != null)
                {
                    btOK.Attributes.Add("onclick", "return updatevalidate();");
                    btnSave1.Attributes.Add("onclick", "return updatevalidate();");

                    UsersBLL UB = new UsersBLL();
                    UsersModel UM = new UsersModel();

                    UM = UB.GetUserInfo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString().Trim());

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

                    pnLogin.Visible = false;

                    //kiem tra neu view diable control
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"] != null)
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

                        btOK.Visible = false;
                        btnSave1.Visible = false;
                        imgSave.Visible = false;
                        imgSave1.Visible = false;
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
    

    protected void btOK_Click(object sender, EventArgs e)
    {   
       
       
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] == null)
            {
                try
                {
                   
                    //insert
                    UsersBLL UB = new UsersBLL();
                    //UB.Insert(Utility.KillSqlInjection(txtUserName.Text.Trim()), Encryption.Encrypt(Utility.KillSqlInjection(txtPass.Text.Trim())), Utility.KillSqlInjection(txtFirstName.Text.Trim()), Utility.KillSqlInjection(txtMiddleName.Text.Trim()), Utility.KillSqlInjection(txtLastName.Text.Trim()), Utility.IsInt(ddlGender.SelectedValue), Utility.KillSqlInjection(txtAddress.Text.Trim()), Utility.KillSqlInjection(txtEmail.Text.Trim()), Utility.KillSqlInjection(txtBirth.Text.Trim()), Utility.KillSqlInjection(txtPhone.Text.Trim()), Utility.IsInt(ddlStatus.SelectedValue), HttpContext.Current.Session["userName"].ToString(), "", 'U' + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8), "TEL","0");

                    //alert
                    lblAlert.Text = Resources.labels.insertsucessfull;
                    //Write Log
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["USERNAME"], "", Utility.KillSqlInjection(txtUserName.Text.Trim()));
                    SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USER"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], System.Configuration.ConfigurationManager.AppSettings["PASSWORD"], "", Encryption.Encrypt(Utility.KillSqlInjection(txtPass.Text.Trim())));
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
                    //UB.Update(Utility.KillSqlInjection(txtUserName.Text.Trim()), Utility.KillSqlInjection(txtFirstName.Text.Trim()), Utility.KillSqlInjection(txtMiddleName.Text.Trim()), Utility.KillSqlInjection(txtLastName.Text.Trim()), Utility.IsInt(ddlGender.SelectedValue), Utility.KillSqlInjection(txtAddress.Text.Trim()), Utility.KillSqlInjection(txtEmail.Text.Trim()), dt, Utility.KillSqlInjection(txtPhone.Text.Trim()), Utility.IsInt(ddlStatus.SelectedValue), HttpContext.Current.Session["userName"].ToString(),dM,"","0");

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
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewuser"]);
            }

        
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewuser"]);
    }
}
