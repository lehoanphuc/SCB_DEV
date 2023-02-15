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
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSUnlockAccount_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                DataSet dsservice = new Services().GetAll(SmartPortal.Constant.IPC.ACTIVE, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtservice = dsservice.Tables[0];
                    ddlService.DataSource = dtservice;
                    ddlService.DataTextField = "ServiceName";
                    ddlService.DataValueField = "ServiceID";
                    ddlService.DataBind();
                    ddlService.Items.Remove(ddlService.Items.FindByValue(SmartPortal.Constant.IPC.SMS));
                    LoadTypeByService();
                }

                string value = ddlService.SelectedValue.ToString();
                switch (value)
                {
                    case IPC.SEMS:
                    case IPC.IB:
                        lblUserName.Text = Resources.labels.username;
                        break;
                    case IPC.AM:
                    case IPC.MB:
                        lblUserName.Text = Resources.labels.PhoneNumber;
                        break;
                    default:
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {
            new SmartPortal.SEMS.User().UnblockAccount(ddlService.SelectedValue.ToString(), txtusername.Text.ToString().Trim(), ddlUserType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.mokhoataikhoanthanhcong;
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

    protected void ddlService_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadTypeByService();
            string value = ddlService.SelectedValue.ToString();
            switch (value)
            {
                case IPC.MB:
                    lblUserName.Text = Resources.labels.PhoneNumber ;
                    break;
                case IPC.SEMS:
                case IPC.IB:
                    lblUserName.Text = Resources.labels.username ;
                    break;
                case IPC.AM:
                    lblUserName.Text = Resources.labels.PhoneNumber;
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSResetPasswords_Widget", "SendInfoLogin", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void ddlUserType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string value = ddlUserType.SelectedValue.ToString();
            switch (value)
            {
                case "IND":
                    if (ddlService.SelectedValue.Equals("IB"))
                    {
                        lblUserName.Text = Resources.labels.username ;
                    }
                    else 
                    {
                        lblUserName.Text = Resources.labels.PhoneNumber;
                    }
                    break;
                default:
                    lblUserName.Text = Resources.labels.username;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSResetPasswords_Widget", "SendInfoLogin", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void LoadTypeByService()
    {
        switch (ddlService.SelectedValue)
        {
            case "SEMS":
                ddlUserType.Items.FindByValue("CCO").Enabled = false;
                ddlUserType.Items.FindByValue("IND").Enabled = false;
                ddlUserType.Items.FindByValue("TELLER").Enabled = true;
                ddlUserType.SelectedValue = "TELLER";
                break;
            case "AM":
                ddlUserType.Items.FindByValue("CCO").Enabled = false;
                ddlUserType.Items.FindByValue("TELLER").Enabled = false;
                ddlUserType.Items.FindByValue("IND").Enabled = false;
                break;
            case "IB":
            case "MB":
                ddlUserType.Items.FindByValue("IND").Enabled = false;
                ddlUserType.Items.FindByValue("CCO").Enabled = true;
                ddlUserType.Items.FindByValue("TELLER").Enabled = false;
                
                break;
            default:
                ddlUserType.Items.FindByValue("CCO").Enabled = true;
                ddlUserType.Items.FindByValue("IND").Enabled = true;
                ddlUserType.Items.FindByValue("TELLER").Enabled = false;
                break;
        }
        ddlUserType_OnSelectedIndexChanged(null, null);
    }
}


