using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSCorporate_Controls_Widget : WidgetBase
{
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;

    public string _IMAGE
    {
        get { return imgLoGo.ImageUrl; }
        set { imgLoGo.ImageUrl = value; }
    }
    public string _TITLE
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            pnAdd.Visible = true;
            if (!IsPostBack)
            {
                LoadDLL();
                BindData();
            }
        }
        catch (Exception ex)
        {
            throw ex;
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
                case IPC.ACTIONPAGE.DETAILS:
                    GetInfo();
                    btsave.Visible = false;
                    btback.Text = Resources.labels.back;
                    btback.OnClientClick = "Loading();";
                    DisableControl();
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtCorpID.Enabled = false;
                    GetInfo();
                    break;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void LoadDLL()
    {
        try
        {
            DataSet ds = new Corporate().GetCatalog(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlCatalog.DataSource = dt;
                    ddlCatalog.DataTextField = "CATNAME";
                    ddlCatalog.DataValueField = "CATID";
                    ddlCatalog.DataBind();
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GetInfo()
    {
        try
        {
            DataTable corpTable = new DataTable();
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            if (string.IsNullOrEmpty(ID))
                RedirectBackToMainPage();
            txtCorpID.Text = ID;
            DataSet ds = new Corporate().GetByCorpId(ID, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                corpTable = ds.Tables[0];
                if (corpTable.Rows.Count > 0)
                {
                    txtCorpName.Text = corpTable.Rows[0][IPC.CORPNAME].ToString();
                    ddlCatalog.SelectedValue = corpTable.Rows[0][IPC.CATID].ToString();
                    txtDesc.Text = corpTable.Rows[0][IPC.DESCRIPTION].ToString();
                    ddlStatus.SelectedValue = corpTable.Rows[0][IPC.STATUS].ToString();
                }
                else
                {
                    pnAdd.Visible = false;
                    lblError.Text = Resources.labels.datanotfound;
                    btsave.Visible = false;
                    btback.OnClientClick = "Loading();";
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void DisableControl()
    {
        txtCorpID.Enabled = false;
        txtCorpName.Enabled = false;
        ddlCatalog.Enabled = false;
        txtDesc.Enabled = false;
        ddlStatus.Enabled = false;
    }

    protected bool ValidateInput()
    {
        try
        {
            lblError.Text = "";
            string corpId = txtCorpID.Text.Trim();
            string corpName = Utility.KillSqlInjection(txtCorpName.Text.Trim());
            if (string.IsNullOrEmpty(corpId))
            {
                lblError.Text = Resources.labels.bancannhapcorporateid;
                return false;
            }
            if (!IsUsername(corpId) && corpId.Length > 1)
            {
                lblError.Text = Resources.labels.corporateidspecialcharactervalidate;
                return false;
            }
            if (corpId.Contains(" "))
            {
                lblError.Text = Resources.labels.corporateidwhitespace;
                return false;
            }
            if (string.IsNullOrEmpty(corpName))
            {
                lblError.Text = Resources.labels.pleaseentercorpname;
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            return false;
        }
    }
    public static bool IsUsername(string username)
    {
        string pattern;
        pattern = @"^(?=[a-zA-Z0-9-]{0,30}$)(?!.*[-]{2})[^-].*[^-]$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(username);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string corpID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCorpID.Text.Trim()).ToUpper();
            string corpName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCorpName.Text.Trim());
            string desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.Trim());
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    if (ValidateInput())
                    {
                        new Corporate().Insert(corpID, corpName, int.Parse(ddlCatalog.SelectedValue), desc, ddlStatus.SelectedValue, Session[IPC.USERNAME].ToString(), DateTime.Now.ToString("MM/dd/yyyy"), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            btsave.Visible = false;
                            DisableControl();
                            lblError.Text = Resources.labels.addedrecordsuccessfully;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                    break;

                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    if (ValidateInput())
                    {
                        new Corporate().Update(
                            corpID,
                            corpName,
                            int.Parse(ddlCatalog.SelectedValue),
                            desc,
                            ddlStatus.SelectedValue,
                            Session[IPC.USERNAME].ToString(),
                            DateTime.Now.ToString("MM/dd/yyyy"),
                            ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE.Equals("0"))
                        {
                            btsave.Visible = false;
                            DisableControl();
                            lblError.Text = Resources.labels.editedrecordsuccessfully;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                    break;
                default:
                    RedirectBackToMainPage();
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name,
                System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message,
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

}