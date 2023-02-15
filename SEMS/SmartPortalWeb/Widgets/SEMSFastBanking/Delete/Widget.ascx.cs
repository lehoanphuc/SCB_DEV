using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.SEMS;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSFastBanking_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        pnResult.Visible = false;
    }
    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {
            ATM objATM = new ATM();

            if (Session["_SHOPID"] != null)
            {
               string SSProductID = Session["_ATMID"].ToString();
                string[] pros = SSProductID.Split('#');
                foreach (string pro in pros)
                {
                    objATM.DeleteATM(pro, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {

                        //Response.Redirect("~/Default.aspx?p=141");
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                }
                Session["_ATMID"] = null;
                //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));


            }
            else
            {               
                if (SmartPortal.SEMS.FastBank.DeleteShop(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC))
                {

                    //Response.Redirect("~/Default.aspx?p=141");
                    lblError.Visible = true;
                    lblConfirm.Visible = false;
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                }
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
            //Response.Redirect("~/default.aspx?p=140");
            //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));
        }


    }
    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=1001"));
    }
}
