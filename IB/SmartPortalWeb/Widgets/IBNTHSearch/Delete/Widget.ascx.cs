using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.IB;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBNTHSearch_Delete_Widgets : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnRole.Visible = true;
            pnResult.Visible = false;

            string nid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"] != null ? SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"].ToString() : "";
            if (nid.Length > 0)
            {
                Session["_ReceiverID"] = nid;
            }
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        Session["_ReceiverID"] = null;
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?po=3&p=114"));
    }
    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {
            Account objAcct = new Account();

            if (Session["_ReceiverID"] != null)
            {
                string SSProductID = Session["_ReceiverID"].ToString();
                string[] pros = SSProductID.Split('#');
                foreach (string pro in pros)
                {
                    objAcct.DeleteReceiverList(pro, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {

                        //Response.Redirect("~/Default.aspx?po=4&p=141");
                    }
                }
                Session["_ReceiverID"] = null;
                //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));

            }
            else
            {
                objAcct.DeleteReceiverList(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["nid"].ToString(), Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            }
            if (IPCERRORCODE == "0")
            {
                pnResult.Visible = true;
                pnRole.Visible = false;
                btsaveandcont.Visible = false;
                lblConfirm.Visible = false;
                Session["_ReceiverID"] = null;
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }


    }
    protected void BtnThoat_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?po=3&p=114"));
    }
}
