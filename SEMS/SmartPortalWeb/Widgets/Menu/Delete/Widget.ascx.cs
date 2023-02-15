using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using System.Data;

public partial class Widgets_Menu_Delete_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            MenuBLL MB = new MenuBLL();

            if (Request["mid"] != null)
            {
                Delete(MB, Utility.KillSqlInjection(Request["mid"].Trim()));
                MB.Delete(Utility.KillSqlInjection(Request["mid"].Trim()));
                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENU"], System.Configuration.ConfigurationManager.AppSettings["MENUID"], Utility.KillSqlInjection(Request["mid"].Trim()),"");
            }
            else
            {
                if (Session["menuIDDelete"] != null)
                {
                    string menuID = Session["menuIDDelete"].ToString();
                    string[] menuIDArray = menuID.Split('-');

                    foreach (string p in menuIDArray)
                    {
                        if (p != "")
                        {
                            Delete(MB, p);
                            MB.Delete(p);
                            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENU"], System.Configuration.ConfigurationManager.AppSettings["MENUID"], p, "");
                        }
                    }
                }
            }

            
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["mdec"], "Widgets_Menu_Delete_Widget", "btnDelete_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["mdec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Menu_Delete_Widget", "btnDelete_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Menu_Delete_Widget", "btnDelete_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        //redirect to page view
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewmenu"]));
    }
    public void Delete(MenuBLL MB, string mID)
    {
        DataTable tblChildCat = MB.GetByParent(mID);
        foreach (DataRow row in tblChildCat.Rows)
        {
            MB.Delete(row["MenuID"].ToString());
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["MENU"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLMENU"], System.Configuration.ConfigurationManager.AppSettings["MENUID"], row["MenuID"].ToString(), "");
            Delete(MB, row["MenuID"].ToString());
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["viewmenu"]));
    }
}
