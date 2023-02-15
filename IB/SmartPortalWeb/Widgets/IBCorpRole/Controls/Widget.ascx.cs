using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;
using System.Data;

public partial class Widgets_IBCorpRole_Controls_Widget : System.Web.UI.UserControl
{
     static string roleName;
     static string roleID;
     static string roleDescription;
     static string userModified;
     static string dateModified;
     string IPCERRORCODE;
     string IPCERRORDESC;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                #region load service
                DataSet ds = new DataSet();
                ds = new SmartPortal.SEMS.Services().GetAll(SmartPortal.Constant.IPC.ACTIVE,ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtService = new DataTable();
                    dtService = ds.Tables[0];

                    ddlService.DataSource = dtService;
                    ddlService.DataTextField = "SERVICENAME";
                    ddlService.DataValueField = "SERVICEID";
                    ddlService.DataBind();

                    //ddlService.Items.Insert(0, new ListItem("Tất cả", "ALL"));
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                #endregion

                //truong hop edit load thong tin
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"] != null)
                {
                    RoleBLL RB = new RoleBLL();
                    RoleModel RM = new RoleModel();

                    RM = RB.GetByID(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"].ToString()));

                    txtRoleName.Text = RM.RoleName;
                    roleName = RM.RoleName;

                    ddlService.SelectedValue = RM.ServiceID;

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
                        //btnSave.Visible = false;
                        btnSave1.Visible = false;
                        //imgSave.Visible = false;
                        imgSave1.Visible = false;
                        ddlService.Enabled = false;
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
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=193"));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"] == null)
        {
            try
            {
                //lấy contractno theo userid
                string contractNo = "";
                DataSet ds = new SmartPortal.IB.User().GetFullUserByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable userTable = ds.Tables[0];
                if(userTable.Rows.Count!=0)
                {
                    contractNo=userTable.Rows[0]["CONTRACTNO"].ToString();
                }

                //insert
                
                new SmartPortal.IB.Role().Insert(Utility.KillSqlInjection(txtRoleName.Text), Utility.KillSqlInjection(txtRoleDescription.Text), HttpContext.Current.Session["userName"].ToString(),contractNo,ddlService.SelectedValue,ref IPCERRORCODE,ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                goto EXIT;
            }
           
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Group_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
        ERROR:
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_Group_Controls_Widget", "btnSave_Click", IPCERRORDESC, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        EXIT: ;
            //alert
            lblAlert.Text = Resources.labels.insertsucessfull;

            txtRoleName.Text = "";
            txtRoleDescription.Text = "";
        }
        else
        {
            try
            {

                //update
                RoleBLL RB = new RoleBLL();
                string dM=DateTime.Now.ToString("dd/MM/yyyy");
                new SmartPortal.IB.Role().Update(Utility.KillSqlInjection(txtRoleName.Text), Utility.KillSqlInjection(txtRoleDescription.Text), HttpContext.Current.Session["userName"].ToString(),dM,Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["gid"].ToString()),ddlService.SelectedValue,ref IPCERRORCODE,ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                goto EXIT;
            }
            
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Group_Controls_Widget", "btnSave_Click", ex.Message, Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
        ERROR:
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_Group_Controls_Widget", "btnSave_Click", IPCERRORDESC, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        EXIT: ;
            //alert
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=193"));
        }
    }
}
