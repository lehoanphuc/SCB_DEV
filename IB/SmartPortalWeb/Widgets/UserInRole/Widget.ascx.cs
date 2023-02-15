using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using System.Data;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;

public partial class Widgets_UserInRole_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] != null)
                {
                    RoleBLL RB = new RoleBLL();
                    gvRoles.DataSource = RB.LoadForUser(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(),"SEMS");
                    gvRoles.DataBind();
                    
                    //an hien nut save
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["t"] != null)
                    {
                        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["t"].ToString() == "t")
                        {
                            Button1.Visible = true;
                            LinkButton1.Visible = true;
                            imgSave.Visible = true;
                            imgSave1.Visible = true;
                        }
                        else
                        {
                            Button1.Visible = false;
                            LinkButton1.Visible = false;
                            imgSave.Visible = false;
                            imgSave1.Visible = false;
                        }
                    }
                }
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_UserInRole_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_UserInRole_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbRoleName;
            Label lblRoleID;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                cbRoleName = (CheckBox)e.Row.FindControl("cbRoleName");
                lblRoleID = (Label)e.Row.FindControl("lblRoleID");
                lblRoleID.Visible = false;

                lblRoleID.Text = drv["RoleID"].ToString();
                cbRoleName.Text = drv["RoleName"].ToString();
                if (drv["Checked"].ToString() == "1")
                {
                    cbRoleName.Checked = true;
                }
                else
                {
                    cbRoleName.Checked = false;
                }
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_UserInRole_Widget", "gvRoles_RowDataBound", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_UserInRole_Widget", "gvRoles_RowDataBound", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewuser"]);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
        UserInRole UIR = new UserInRole();

        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] != null)
        {
            CheckBox cbRoleName;
            Label lblRoleID;
            foreach (GridViewRow gvr in gvRoles.Rows)
            {
                cbRoleName = (CheckBox)gvr.Cells[1].FindControl("cbRoleName");
                lblRoleID = (Label)gvr.Cells[0].FindControl("lblRoleID");
                if (cbRoleName.Checked == true)
                {
                    UIR.Insert(Utility.IsInt(lblRoleID.Text), SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), true);
                }
                else
                {
                    UIR.Insert(Utility.IsInt(lblRoleID.Text), SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), false);
                }

                 //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USERINROLE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSERINROLE"], System.Configuration.ConfigurationManager.AppSettings["USERNAME"], "", SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["USERINROLE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSERINROLE"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], "", lblRoleID.Text);

            }
        }
        }
         catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["userinroleiec"], "Widgets_UserInRole_Widget", "Button1_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["userinroleiec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_UserInRole_Widget", "Button1_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_UserInRole_Widget", "Button1_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
            //redirect trang truoc
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewuser"]);
        
    }
}
