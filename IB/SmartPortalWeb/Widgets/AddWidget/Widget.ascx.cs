using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;

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
                ddlWidget.DataSource = wBLL.LoadReader(System.Globalization.CultureInfo.CurrentCulture.ToString());
                ddlWidget.DataTextField = "WidgetTitle";
                ddlWidget.DataValueField = "WidgetID";
                ddlWidget.DataBind();
                ddlWidget.Items.Insert(0, new ListItem("--Chọn widget cần thêm--", ""));

                //load position
                PositionBLL PBLL = new PositionBLL();
                ddlPosition.DataSource = PBLL.Load(Session["pageID"].ToString());
                ddlPosition.DataTextField = "PositionID";
                ddlPosition.DataValueField = "PositionID";
                ddlPosition.DataBind();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (ddlWidget.SelectedItem.Value != "")
            {
                WidgetPageBLL wP = new WidgetPageBLL();
                string pageID = Session["pageID"].ToString();
                string widgetID = ddlWidget.SelectedItem.Value;
                string position = ddlPosition.SelectedValue;
                int order = Utility.IsInt(ddlOrder.SelectedValue);
                int roleID = new SmartPortal.Common.PortalSettings().portalSetting.RoleAdminID;
                string widgetPageID = pageID + pageID.Length + widgetID;
                wP.InsertNew(widgetPageID, pageID, widgetID, position, order, roleID);
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + pageID),false);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}