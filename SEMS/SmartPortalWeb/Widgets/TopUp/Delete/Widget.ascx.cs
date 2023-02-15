using System;
using System.Data;
using SmartPortal.ExceptionCollection;

public partial class Widgets_Topup_Delete_Widget : WidgetBase
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
        DataSet ProductLMTable = new DataSet();
        string SSProTranCCYID = "";
        try
        {
            if (Session["_TOPUPSERIAL"] != null)
            {
                SSProTranCCYID = Session["_TOPUPSERIAL"].ToString();
                string[] Alls = SSProTranCCYID.Split('#');
                foreach (string All in Alls)
                {
                    SmartPortal.SEMS.Topup.DeleteTopupCardbySerial(All,"test",DateTime.Now.ToShortDateString(),ref IPCERRORCODE,ref IPCERRORDESC);
                }
                Session["_TOPUPSERIAL"] = null;
            }
            else
            {
                SmartPortal.SEMS.Topup.DeleteTopupCardbySerial(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pid"].ToString(), "test", DateTime.Now.ToShortDateString(), ref IPCERRORCODE, ref IPCERRORDESC);
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
            pnRole.Visible = false;
            pnResult.Visible = true;
            btsaveandcont.Visible = false;
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=424"));
    }
}


