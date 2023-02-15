using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Model;
using SmartPortal.Common.Utilities;
using System.Text;
using SmartPortal.ExceptionCollection;

public partial class Widgets_NavigateHomeBank_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"] != null)
                {
                    String s="";                    
                    s=GetParent(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"].ToString()), s);
                    s = "<img class='imgNH' src='widgets/navigatehomebank/images/icon_home.gif'/> <a class='navigatehomebank' href='" + System.Configuration.ConfigurationManager.AppSettings["homebank"] + "'>" + Resources.labels.home + "</a>" + "<img align='top' src='widgets/navigatehomebank/images/iconarrow.gif'/>" + s;
                    ltrNavigate.Text = s;
                }
                else
                {
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"] != null)
                    {
                        NewsBLL NB = new NewsBLL();
                        NewsModel NM = new NewsModel();

                        NM = NB.LoadNewsByID(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"]),1);

                        String s = "";
                        s = GetParent(NM.CatID, s);
                        s = "<img class='imgNH' src='widgets/navigatehomebank/images/icon_home.gif'/> <a class='navigatehomebank' href='" + System.Configuration.ConfigurationManager.AppSettings["homebank"] + "'>" + Resources.labels.home + "</a>" + "<img align='top' src='widgets/navigatehomebank/images/iconarrow.gif'/>" + s;
                        s += "<img align='top' src='widgets/navigatehomebank/images/iconarrow.gif'/><span class='spanNH'>" + NM.Title + "</span>";

                        ltrNavigate.Text = s;
                    }
                }
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_NavigateHomeBank_Widget", "Page_Load", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_NavigateHomeBank_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }

    private string GetParent(int parentID,string s)
    {
        CategoryBLL CB = new CategoryBLL();
        CategoryModel CM = new CategoryModel();

        CM = CB.LoadByID(parentID);
        if (CM.Link == "")
        {
            if (s != "")
            {
                s = "<a class='navigatehomebank' href='" + System.Configuration.ConfigurationManager.AppSettings["newsofcategory"] + "&catid=" + CM.CatID.ToString() + "'>" + CM.CatName + "</a>" + "<img align='top' src='widgets/navigatehomebank/images/iconarrow.gif'/>" + s;
            }
            else
            {
                s = "<a class='navigatehomebank' href='" + System.Configuration.ConfigurationManager.AppSettings["newsofcategory"] + "&catid=" + CM.CatID.ToString() + "'>" + CM.CatName + "</a>";
            }
        }
        else
        {
            if (s != "")
            {
                s = "<a class='navigatehomebank' href='" + CM.Link+"&catid="+CM.CatID + "'>" + CM.CatName + "</a>" + "<img align='top' src='widgets/navigatehomebank/images/iconarrow.gif'/>" + s;
            }
            else
            {
                s = "<a class='navigatehomebank' href='" + CM.Link + "&catid=" + CM.CatID + "'>" + CM.CatName + "</a>";
            }
        }
        if (CM.ParentID != 0)
        {
            s=GetParent(CM.ParentID, s);
        }
        
        return s;
    }
}
