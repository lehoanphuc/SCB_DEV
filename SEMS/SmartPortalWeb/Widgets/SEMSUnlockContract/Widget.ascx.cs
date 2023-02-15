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
using SmartPortal.Security;
using System.Text;
using System.Drawing;
using SmartPortal.ExceptionCollection;
public partial class Widgets_SEMSUnlockContract_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSResetPasswords_Widget", "Page_Load", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSResetPasswords_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {
            new SmartPortal.SEMS.Contract().UpdateStatusForContractBlock(txtcontractno.Text.Trim(), SmartPortal.Constant.IPC.ACTIVE, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.unblockcontractsucessfull;
                SmartPortal.Common.Log.WriteLogDatabase(txtcontractno.Text.Trim(), "", Request.Url.ToString(), Session["userName"].ToString(),
                    Request.UserHostAddress, "TBLCONTRACT",
                    "Unblock Contract", "", "Active");
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSResetPasswords_Widget", "SendInfoLogin", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}


