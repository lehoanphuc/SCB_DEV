using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Model;
using SmartPortal.BLL;

public partial class Widgets_NavigateAdminBank_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MenuModel MM = new MenuModel();
                MM = new MenuBLL().GetByLink("?p=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"], System.Globalization.CultureInfo.CurrentCulture.ToString());
                string s = "";

                MenuModel MM1 = new MenuModel();
                if(string.IsNullOrEmpty(MM.MenuID))
                {
                    MM.MenuID = "";
                }    
                MM1 = new MenuBLL().GetByID(MM.MenuID, System.Globalization.CultureInfo.CurrentCulture.ToString());
                s = "<a class='navigatehomebank' href='" + MM1.MenuLink + "'>" + MM1.MenuTitle + "</a>";

                s = GetParent(MM.MenuParent, s);
                s = "<img class='imgNH' src='widgets/navigateadminbank/images/icon_home.gif'/> <a class='navigatehomebank' href='" + System.Configuration.ConfigurationManager.AppSettings["homeadmin"] + "'>" + Resources.labels.home + "</a>" + "<img align='top' src='widgets/navigateadminbank/images/iconarrow.gif'/>" + s;

                ltrNavigate.Text = s;
            }
        }
        catch (Exception ex)
        {

        }
    }

    public string GetParent(string parentID, string s)
    {
        MenuModel MM = new MenuModel();
        MM = new MenuBLL().GetByID(parentID, System.Globalization.CultureInfo.CurrentCulture.ToString());

        if (s != "")
        {
            s = "<a class='navigatehomebank' href='" + MM.MenuLink + "'>" + MM.MenuTitle + "</a>" + "<img align='top' src='widgets/navigateadminbank/images/iconarrow.gif'/>" + s;
        }
        else
        {
            s = "<a class='navigatehomebank' href='" + MM.MenuLink + "'>" + MM.MenuTitle + "</a>";
        }

        if (!string.IsNullOrEmpty(MM.MenuParent))
        {
            s = GetParent(MM.MenuParent, s);
        }

        return s;
    }
}
