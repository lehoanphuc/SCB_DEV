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

public partial class Widgets_SEMSUserDetele_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        DataSet delTable = new DataSet();
        string SSUID="";
        try
        {
            if (Session["_Userid"] != null)
            {
                SSUID = Session["_Userid"].ToString();
                string[] uids = SSUID.Split('#');
                foreach (string uid in uids)
                {
                    delTable = new SmartPortal.SEMS.User().DeleteUserByID(uid, SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        
                        //Response.Redirect("~/Default.aspx?p=141");
                    }
                }
               Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString())));

            }
            else
            {
                //DataSet delTable = new DataSet();
                delTable = new SmartPortal.SEMS.User().DeleteUserByID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                   Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString())));
                    //Response.Redirect("~/Default.aspx?p=141");
                }
            }
        }
        catch (Exception ex)
        { }
    }
}


