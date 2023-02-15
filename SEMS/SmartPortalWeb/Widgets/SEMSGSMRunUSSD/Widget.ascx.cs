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
using System.Globalization;

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSGSMRunUSSD_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";
            
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSGSMRunUSSD_Widget", "Page_Load", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSGSMRunUSSD_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        try
        {
            txtResult.Text = "";
            string USSDCode = txtUSSDCode.Text;
            if(!string.IsNullOrEmpty(USSDCode))
            {
                Hashtable Result = new SmartPortal.SEMS.Services().RunUSSDCode(USSDCode, ref IPCERRORCODE, ref IPCERRORDESC);
                if(IPCERRORCODE.Equals("0"))
                {
                    if(Result.ContainsKey(SmartPortal.Constant.IPC.RESULT))
                    {
                        txtResult.Text = GetUSSDResult(Result[SmartPortal.Constant.IPC.RESULT].ToString());
                    }
                    else
                    {
                        lblTextError.Text = Resources.labels.modembanthulaisau;
                    }
                }
                else
                {
                    lblTextError.Text = Resources.labels.modembanthulaisau;
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSGSMRunUSSD_Widget", "btnCheck_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSGSMRunUSSD_Widget", "btnCheck_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private string GetUSSDResult(string input)
    {
        try
        {
        string output = string.Empty;

        string[] arr = input.Split('\n');
        output = arr[arr.Length-2].ToString();
        output = output.Substring(output.IndexOf("\"") +1, output.Length - (6 + output.IndexOf("\"")));

        return output;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
