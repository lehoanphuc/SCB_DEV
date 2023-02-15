using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSPoster_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        pnResult.Visible = false;
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        DataSet ProductTable = new DataSet();
        string SSPosterID = "";
        try
        {
            if (Session["_PosterID"] != null)
            {
                SSPosterID = Session["_PosterID"].ToString();
                string[] posters = SSPosterID.Split('#');
                foreach (string poster in posters)
                {
                    new SmartPortal.SEMS.Poster().Delete(poster, ref IPCERRORCODE, ref IPCERRORDESC);
                }
                Session["_PosterID"] = null;
            }
            else
            {
                new SmartPortal.SEMS.Poster().Delete(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

        if (IPCERRORCODE == "0")
        {
            pnResult.Visible = true;
            pnRole.Visible = false;
            btsaveandcont.Visible = false;
            //Response.Redirect("~/default.aspx?p=150");
            //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));
        }
    }

    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=416"));
    }
}