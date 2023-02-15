using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using SmartPortal.BLL;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;
using SmartPortal.SEMS;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class Widgets_SEMSCONFIGNPSCURRENCY_Controls_Widget : WidgetBase
{
    private string ACTION = "";
    private string IPCERRORCODE = "";
    private string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                LoadDll();
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }
    private void LoadDll()
    {
        try
        {
            DataTable data = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
           
            ddlCCYID.DataSource = data;
            ddlCCYID.DataTextField = "CCYID";
            ddlCCYID.DataValueField = "CCYID";
            ddlCCYID.DataBind();

            ddlStatus.Items.Add(new ListItem(Resources.labels.active, "A"));
            ddlStatus.Items.Add(new ListItem(Resources.labels.inactive, "I"));
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }
    private void BindData()
    {
        try
        {
            switch (ACTION)
            {              
                case IPC.ACTIONPAGE.DETAILS:
                    lblTitleCurrency.Text = "Details Configure NPS Currency";
                    pnAdd.Enabled = false;
                    btnSave.Visible = false;
                    btnClear.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    lblTitleCurrency.Text = "Edit Configure NPS Currency";
                    ddlCCYID.Enabled = false;
                    btnClear.Enabled = false;
                    break;
            }

            switch (ACTION)
            {
                default:
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataTable currencyTable = new Currency().SearchConfigNPSCurrency(ID, 0, 0, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                    if (IPCERRORCODE.Equals("0"))
                    {
                        if (currencyTable.Rows.Count != 0)
                        {                          
                            ddlCCYID.SelectedValue = currencyTable.Rows[0]["CCYID"].ToString();
                            txtDesc.Text = currencyTable.Rows[0]["DESCRIPTION"].ToString();
                            ddlStatus.Text = currencyTable.Rows[0]["STATUS"].ToString();
                        }
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            string ccyid = Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string desc = Utility.KillSqlInjection(txtDesc.Text.Trim());
            string status = Utility.KillSqlInjection(ddlStatus.SelectedValue);
            string user = Session["userName"].ToString();   
            switch (ACTION)
            {            
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    new Currency().UpdateConfigNPSCurrency(ID, desc, status, user, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.updatesuccessfully;
                        pnAdd.Enabled = false;
                        btnSave.Visible = false;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name,
                ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        pnAdd.Enabled = true;
        ddlCCYID.SelectedIndex = 0;
        txtDesc.Text = string.Empty;
        ddlStatus.SelectedIndex = 0;
        btnSave.Enabled = true;
    }
}