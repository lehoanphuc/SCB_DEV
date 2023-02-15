using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.BLL;
using System.Data;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SetRoleForWidget_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {                
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"] != null)
                {
                    RoleBLL RB = new RoleBLL();
                    //gvRoles.DataSource = RB.GetAllReader( Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"].ToString()));
                    //gvRoles.DataBind();
                }
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_SetRoleForWidget_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SetRoleForWidget_Widget", "Page_Load", ex.Message, Request.Url.Query);
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_SetRoleForWidget_Widget", "gvRoles_RowDataBound", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SetRoleForWidget_Widget", "gvRoles_RowDataBound", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
            WidgetRightBLL WR = new WidgetRightBLL();
            
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"] != null)
            {
                try
                {
                    CheckBox cbRoleName;
                    Label lblRoleID;
                    foreach (GridViewRow gvr in gvRoles.Rows)
                    {
                        cbRoleName = (CheckBox)gvr.Cells[1].FindControl("cbRoleName");
                        lblRoleID = (Label)gvr.Cells[0].FindControl("lblRoleID");
                        if (cbRoleName.Checked == true)
                        {
                            //WR.Insert(Utility.IsInt(lblRoleID.Text), int.Parse(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"].ToString()), true);
                            //Write Log
                            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGETPAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"],"", lblRoleID.Text);
                            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGETPAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["WIDGETPAGEID"], "", SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"].ToString());

                        }
                        else
                        {
                            //WR.Insert(Utility.IsInt(lblRoleID.Text), int.Parse(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"].ToString()), false);
                            //Write Log
                            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGETPAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["ROLEID"], "", lblRoleID.Text);
                            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGETPAGE"], System.Configuration.ConfigurationManager.AppSettings["PERMISSION"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETPAGERIGHT"], System.Configuration.ConfigurationManager.AppSettings["WIDGETPAGEID"], "", SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"].ToString());

                        }
                       
                    }
                }
                catch (BusinessExeption bex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["wpiec"], "Widgets_SetRoleForWidget_Widget", "Button1_Click", bex.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["wpiec"], Request.Url.Query);
                }
                catch (SQLException sex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_SetRoleForWidget_Widget", "Button1_Click", sex.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SetRoleForWidget_Widget", "Button1_Click", ex.Message, Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                }

                Redi();
            }

                       
       
    }

    protected void Button2_Click1(object sender, EventArgs e)
    {
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"] != null)
        {
            Redi();
        }
    }

    public void Redi()
    {
        //redirect trang truoc
        WidgetPageBLL wpB = new WidgetPageBLL();
        WidgetPageModel wpM = new WidgetPageModel();
        try
        {           
            //wpM = wpB.GetByID(int.Parse(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["wpid"].ToString()));
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_SetRoleForWidget_Widget", "Redi", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SetRoleForWidget_Widget", "Redi", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

        Response.Redirect("Default.aspx?po=" + wpM.PortalID + "&p=" + wpM.PageID);
    }
}
