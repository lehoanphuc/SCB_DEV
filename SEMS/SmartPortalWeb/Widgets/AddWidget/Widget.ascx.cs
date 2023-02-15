using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_AddWidget_Widget : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //load widget
                WidgetsBLL wBLL = new WidgetsBLL();
                ddlWidgets.DataSource = wBLL.LoadReader(System.Globalization.CultureInfo.CurrentCulture.ToString());
                ddlWidgets.DataTextField = "WidgetTitle";
                ddlWidgets.DataValueField = "WidgetID";
                ddlWidgets.DataBind();
                ddlWidgets.Items.Insert(0, new ListItem("--Chọn widget cần thêm--", ""));

                //load position
                PositionBLL PBLL = new PositionBLL();
                ddlPosition.DataSource = PBLL.Load(Session["pageID"].ToString());
                ddlPosition.DataTextField = "PositionID";
                ddlPosition.DataValueField = "PositionID";
                ddlPosition.DataBind();
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_AddWidget_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_AddWidget_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlWidgets.SelectedValue != "")
            {
                WidgetPageBLL wP = new WidgetPageBLL();
                string pageID = Session["pageID"].ToString();
                string widgetID = ddlWidgets.SelectedValue;
                string position = ddlPosition.SelectedValue;
                int order = Utility.IsInt(ddlOrder.SelectedValue);
                int roleID = new SmartPortal.Common.PortalSettings().portalSetting.RoleAdminID;
                string widgetPageID = pageID + pageID.Length + widgetID;
                wP.InsertNew(widgetPageID, pageID, widgetID, position, order, roleID);

                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGETPAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETPAGE"], System.Configuration.ConfigurationManager.AppSettings["PAGEID"], "", pageID.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGETPAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETPAGE"], System.Configuration.ConfigurationManager.AppSettings["WIDGETID"], "", widgetID.ToString());
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGETPAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETPAGE"], System.Configuration.ConfigurationManager.AppSettings["POSITION"], "", position);
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["WIDGETPAGE"], System.Configuration.ConfigurationManager.AppSettings["INSERT"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLWIDGETPAGE"], System.Configuration.ConfigurationManager.AppSettings["ORDER"], "", order.ToString());
            }
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["wpiec"], "Widgets_AddWidget_Widget", "Button1_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["wpiec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_AddWidget_Widget", "Button1_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_AddWidget_Widget", "Button1_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx" + Request.Url.Query));
    }

}
