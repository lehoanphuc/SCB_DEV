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

public partial class Widgets_IBCorpUser_Detele_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            string uid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString() ;
            if (uid.Length > 0)
            {
                Session["_Userid"] = uid;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet delTable = new DataSet();
            string SSUID = "";
            if (Session["_Userid"] != null)
            {
                SSUID = Session["_Userid"].ToString();
                string[] uids = SSUID.Split('#');
                foreach (string uid in uids)
                {
                    delTable = new SmartPortal.SEMS.User().DeleteUserByID(uid, SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                }
            }
            else
            {
                //DataSet delTable = new DataSet();
                delTable = new SmartPortal.SEMS.User().DeleteUserByID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
            }
            if (IPCERRORCODE == "0")
            {
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?po=3&p=190"), false);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}


