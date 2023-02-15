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

using System.Text;
using SmartPortal.BLL;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Constant;
using SmartPortal.IB;
using SmartPortal.Model;

public partial class Widgets_SEMSUserLevel_Control_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static string pn = "";
    public string _TITLE
    {
        get { return lblTitleLevel.Text; }
        set { lblTitleLevel.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                for (int i = 1; i < Int32.Parse(ConfigurationManager.AppSettings["USERLEVELTOTAL"]); i++)
                {
                    ListItem li = new ListItem(i.ToString(), i.ToString());
                    ddlLevel.Items.Add(li);
                }
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    btClear.Enabled = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();                  
                    DataSet dsTranApp = new SmartPortal.SEMS.User().DetailsUserlevel(ID, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                    }

                    if (dsTranApp != null)
                    {
                        DataTable dtTranApp = dsTranApp.Tables[0];
                        if (dtTranApp.Rows.Count != 0)
                        {                         
                            ddlLevel.SelectedValue = dtTranApp.Rows[0]["USERLEVEL"].ToString().Trim();
                            txtDesc.Text = dtTranApp.Rows[0]["DESCRIPTION"].ToString().Trim();
                        }
                    }
                    break;
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnFocus.Enabled = false;
                    btsave.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    ddlLevel.Enabled = false;
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string level = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlLevel.SelectedValue);
            string description = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.Trim());

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    try
                    {
                        new SmartPortal.SEMS.User().AddUserLevel(level, description, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.adduserlevelsuccess;
                            btsave.Visible = false;
                            pnFocus.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
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
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    try
                    {
                        new SmartPortal.SEMS.User().UpdateUserlevel(level, description, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.edituserlevelsuccess;
                            btsave.Enabled = false;
                            pnFocus.Enabled = false;
                        }
                        else
                        {
                            ErrorCodeModel EM = new ErrorCodeModel();
                            EM = new ErrorBLL().Load(Utility.IsInt(SmartPortal.Constant.IPC.ERRORCODE.EXISTLEVEL), System.Globalization.CultureInfo.CurrentCulture.ToString());
                            lblError.Text = EM.ErrorDesc;
                            return;
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
                    break;
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

    protected void btClear_Click(object sender, EventArgs e)
    {
        lblError.Text = String.Empty;
        ClearInputs(Page.Controls);
        pnFocus.Enabled = true;
        btsave.Visible = true;
    }

    void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            ClearInputs(ctrl.Controls);
        }
    }
}
