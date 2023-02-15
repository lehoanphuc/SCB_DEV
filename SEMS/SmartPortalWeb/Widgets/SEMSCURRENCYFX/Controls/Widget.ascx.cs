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

public partial class Widgets_SEMSCURRENCYFX_Controls_Widget : WidgetBase
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
            ddlFromCCYID.DataSource = data;
            ddlFromCCYID.DataTextField = "CCYID";
            ddlFromCCYID.DataValueField = "CCYID";
            ddlFromCCYID.DataBind();

            ddlToCCYID.DataSource = data;
            ddlToCCYID.DataTextField = "CCYID";
            ddlToCCYID.DataValueField = "CCYID";
            ddlToCCYID.DataBind();

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
                case IPC.ACTIONPAGE.ADD:
                    lblTitleCurrency.Text = Resources.labels.addfx1;
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    lblTitleCurrency.Text = "Details Foreign Exchange";
                    pnAdd.Enabled = false;
                    btnSave.Visible = false;
                    btnClear.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    lblTitleCurrency.Text = "Edit Foreign Exchange";
                    ddlFromCCYID.Enabled = false;
                    ddlToCCYID.Enabled = false;
                    btnClear.Enabled = false;
                    break;
            }

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;

                default:
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    string[] pros = ID.Split('|');
                    DataTable currencyTable = new Currency().SearchCurrencyFx(pros[0], pros[1], 0, 0, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                    if (IPCERRORCODE.Equals("0"))
                    {
                        if (currencyTable.Rows.Count != 0)
                        {
                            ddlFromCCYID.SelectedValue = currencyTable.Rows[0]["FROMCCYID"].ToString();
                            ddlToCCYID.SelectedValue = currencyTable.Rows[0]["TOCCYID"].ToString();
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
            string fromccyid = Utility.KillSqlInjection(ddlFromCCYID.SelectedValue.Trim());
            string toccyid = Utility.KillSqlInjection(ddlToCCYID.SelectedValue.Trim());
            string desc = Utility.KillSqlInjection(txtDesc.Text.Trim());
            string status = Utility.KillSqlInjection(ddlStatus.SelectedValue);
            string user = Session["userName"].ToString();
            if (fromccyid == toccyid)
            {
                lblError.Text = "From Currency not Same To Currency";
                return;
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    new Currency().InsertCurrencyFX(fromccyid, toccyid, desc, status, user, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.addsuccessfully;
                        pnAdd.Enabled = false;
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    string[] pros = ID.Split('|');
                    new Currency().UpdateCurrencyFX(pros[0], pros[1], desc, status, user, ref IPCERRORCODE, ref IPCERRORDESC);
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
        ddlFromCCYID.SelectedIndex = 0;
        ddlToCCYID.SelectedIndex = 0;
        txtDesc.Text = string.Empty;
        ddlStatus.SelectedIndex = 0;
        btnSave.Enabled = true;
    }
}