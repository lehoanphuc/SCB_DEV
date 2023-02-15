using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSProductDelete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    static string pageBackID = "1057";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pnResult.Visible = false;
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        //DataSet ProductTable = new DataSet();
        //string SSProductID = "";
        try
        {
            //if (Session["_ProductID"] != null)
            //{
            //    SSProductID = Session["_ProductID"].ToString();
            //    string[] pros = SSProductID.Split('#');
            //    foreach (string pro in pros)
            //    {
            //        ProductTable = new SmartPortal.SEMS.Product().DeleteProduct(pro, ref IPCERRORCODE, ref IPCERRORDESC);
            //        if (IPCERRORCODE == "0")
            //        {

            //            //Response.Redirect("~/Default.aspx?p=141");
            //        }
            //        else 
            //        {
            //            if (IPCERRORDESC == "110211")
            //            {
            //                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.USINGPRODUCT);
            //            }
            //            else
            //            {
            //                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
            //            }
            //        }
            //    }
            //    Session["_ProductID"] = null;
            //    //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));

            //}
            //else
            //{
             new SmartPortal.SEMS.Notification().DeleteNotify(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pid"].ToString(),SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {

                    lblError.Text = "Delete success";
                }
                else
                {
                    lblError.Text = "Delete Failed";
                }
            //}
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
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + pageBackID));
    }
}


