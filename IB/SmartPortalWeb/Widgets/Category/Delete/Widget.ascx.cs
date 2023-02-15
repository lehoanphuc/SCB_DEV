using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using System.Data;

public partial class Widgets_Category_Delete_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            CategoryBLL MB = new CategoryBLL();

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"] != null)
            {
                Delete(MB, Utility.IsInt(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"].Trim())));

                MB.Delete(Utility.IsInt(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"].Trim())));
                //Write Log
                SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["CATID"], SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["catid"].ToString(), "");

            }
            else
            {
                if (Session["catIDDelete"] != null)
                {
                    string catID = Session["catIDDelete"].ToString();
                    string[] catIDArray = catID.Split('-');

                    foreach (string p in catIDArray)
                    {
                        if (p != "")
                        {
                            Delete(MB, Utility.IsInt(p));
                            MB.Delete(Utility.IsInt(p));
                            //Write Log
                            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["CATID"], p, "");

                        }
                    }
                }
            }

        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["catdec"], "Widgets_Category_Delete_Widget", "btnDelete_Click", bex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["catdec"], Request.Url.Query);
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_Category_Delete_Widget", "btnDelete_Click", sex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_Category_Delete_Widget", "btnDelete_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        //redirect to page view
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewcategory"]);
    }
    public void Delete(CategoryBLL CB, int catID)
    {
        DataTable tblChildCat= CB.LoadChildCategory1(catID);
        foreach (DataRow row in tblChildCat.Rows)
        {
            CB.Delete(Utility.IsInt(row["CatID"].ToString()));
            //Write Log
            SmartPortal.Common.Log.WriteLogDatabase(System.Configuration.ConfigurationManager.AppSettings["CATEGORY"], System.Configuration.ConfigurationManager.AppSettings["DELETE"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCATEGORY"], System.Configuration.ConfigurationManager.AppSettings["CATID"], row["CatID"].ToString(), "");

            Delete(CB, Utility.IsInt(row["CatID"].ToString()));
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["viewcategory"]);
    }
}
