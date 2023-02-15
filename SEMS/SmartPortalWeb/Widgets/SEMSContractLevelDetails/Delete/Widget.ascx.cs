using System;
using System.Data;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSContractLevelDelete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = Resources.labels.ConfirmDeleteContractLevel + " " + GetParamsPage(IPC.ID)[0].Trim();
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
       
        try
        {
            DataSet ContractLevel = new DataSet();
            string ContractLevelCode = GetParamsPage(IPC.ID)[0].Trim();
            ContractLevel = new SmartPortal.SEMS.ContractLevel().DeleteContractLevel(ContractLevelCode, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {

                //Response.Redirect("~/Default.aspx?p=141");
            }
            else
            {
                if (IPCERRORDESC == "110211")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.ACTIVEBRANCH);
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
            lblConfirm.Text = Resources.labels.ketquathuchien;
            lblError.Text = Resources.labels.DeleteContractLevelSuccess;
            btsaveandcont.Visible = false;
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}