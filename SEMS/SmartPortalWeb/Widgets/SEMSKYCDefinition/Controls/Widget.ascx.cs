using System;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSKYCDefinition_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();

    public string _TITLE
    {
        get { return lblTitleKycDefinition.Text; }
        set { lblTitleKycDefinition.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";

            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
              if( ACTION == IPC.ACTIONPAGE.ADD)
              {   
                    pnadd.Visible = false;
                    liTabActivity.Visible = false;
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
    }
}
