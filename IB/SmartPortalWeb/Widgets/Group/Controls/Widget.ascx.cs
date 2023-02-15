using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Group_Controls_Widget : System.Web.UI.UserControl
{
     static string roleName;
     static string roleID;
     static string roleDescription;
     static string userModified;
     static string dateModified;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {                

                //truong hop edit load thong tin
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"] != null)
                {
                    RoleBLL RB = new RoleBLL();
                    RoleModel RM = new RoleModel();

                    RM = RB.GetByID(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"].ToString()));

                    txtRoleName.Text = RM.RoleName;
                    roleName = RM.RoleName;

                    txtRoleDescription.Text = RM.RoleDescription;
                    roleDescription = RM.RoleDescription;

                    userModified = RM.UserModified;
                    dateModified = RM.DateModified;
                    roleID = RM.RoleID.ToString();

                    //kiem tra neu view diable control
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["type"] != null)
                    {
                        txtRoleName.Enabled = false;
                        txtRoleDescription.Enabled = false;
                        btnSave.Visible = false;
                        btnSave1.Visible = false;
                        imgSave.Visible = false;
                        imgSave1.Visible = false;
                    }

                }
            }

        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Group_Controls_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Group_Controls_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewgroup"]);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"] == null)
        {
            try
            {

                //insert
                RoleBLL RB = new RoleBLL();
                //RB.Insert(Utility.KillSqlInjection(txtRoleName.Text), Utility.KillSqlInjection(txtRoleDescription.Text), HttpContext.Current.Session["userName"].ToString(),"","","");

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["GROUP"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLGROUP"], System.Configuration.ConfigurationManager.AppSettings["ROLENAME"], "", Utility.KillSqlInjection(txtRoleName.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["GROUP"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLGROUP"], System.Configuration.ConfigurationManager.AppSettings["ROLEDESCRIPTION"], "", Utility.KillSqlInjection(txtRoleDescription.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["GROUP"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLGROUP"], System.Configuration.ConfigurationManager.AppSettings["USERCREATED"], "", HttpContext.Current.Session["userName"].ToString());
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["giec"], "Widgets_Group_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["giec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Group_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Group_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

            //alert
            lblAlert.Text = Resources.labels.insertsucessfull;

            txtRoleName.Text = "";
            txtRoleDescription.Text = "";
        }
        else
        {
            try
            {

                //insert
                RoleBLL RB = new RoleBLL();
                string dM=DateTime.Now.ToString("dd/MM/yyyy");
                //RB.Update(Utility.KillSqlInjection(txtRoleName.Text), Utility.KillSqlInjection(txtRoleDescription.Text), HttpContext.Current.Session["userName"].ToString(),dM,Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"].ToString()),"","");
                
                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["GROUP"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLGROUP"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], roleID, roleID);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["GROUP"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLGROUP"], System.Configuration.ConfigurationManager.AppSettings["ROLENAME"], roleName, Utility.KillSqlInjection(txtRoleName.Text));
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["GROUP"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLGROUP"], System.Configuration.ConfigurationManager.AppSettings["ROLEDESCRIPTION"], roleDescription, Utility.KillSqlInjection(txtRoleDescription.Text));
                
            }
            catch (BusinessExeption bex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["guec"], "Widgets_Group_Controls_Widget", "btnSave_Click", bex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["guec"], Request.Url.Query);
            }
            catch (SQLException sex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Group_Controls_Widget", "btnSave_Click", sex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Group_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }

            //alert
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewgroup"]);
        }
    }
}
