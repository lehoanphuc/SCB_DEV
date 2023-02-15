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

public partial class Widgets_SEMSCurrency_Controls_Widget : WidgetBase
{
    private string ACTION = "";
    private string IPCERRORCODE = "";
    private string IPCERRORDESC = "";

    public string _TITLE
    {
        set { lblTitleCurrency.Text = value; }
        get { return lblTitleCurrency.Text; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                for (int i = 1; i < 21; i++)
                {
                    ListItem li =
                        new ListItem(i.ToString(), i.ToString());
                    ddlOrder.Items.Add(li);
                }

                ListItem l3 =
                    new ListItem(Resources.labels.active, "A");
                ListItem l4 =
                    new ListItem(Resources.labels.inactive, "I");
                ddlStatus.Items.Add(l3);
                ddlStatus.Items.Add(l4);
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

    //Overriden with other method
    private new string[] GetParamsPage(params string[] paramnames)
    {
        if (paramnames.Length == 0) return null;
        string[] results = new string[paramnames.Length];
        DictionaryWithDefault<string, string> urlparams = Encrypt.GetURLParam(Server.UrlDecode(HttpContext.Current.Request.RawUrl));
        for (int i = 0; i < paramnames.Length; i++)
        {
            string param = paramnames[i].Trim();
            if (urlparams.ContainsKey(param)) results[i] = Utility.KillSqlInjection(urlparams[param]);
        }
        return results;
    }

    private void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    btnSave.Visible = false;
                    btnClear.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtCurrencyId.Enabled = false;
                    btnClear.Enabled = false;
                    break;
            }

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;

                default:
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataTable currencyTable = new Currency()
                        .GetCurrencyDetailsById(ID, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                    if (IPCERRORCODE.Equals("0"))
                    {
                        if (currencyTable.Rows.Count != 0)
                        {
                            txtCurrencyId.Text = currencyTable.Rows[0]["CCYID"].ToString();
                            txtSCurrencyId.Text = currencyTable.Rows[0]["SCurrencyId"].ToString();
                            txtCurrencyName.Text = currencyTable.Rows[0]["CURRENCYNAME"].ToString();
                            txtCurrencyNumber.Text = currencyTable.Rows[0]["CURRENCYNUMBER"].ToString();
                            txtCurrencyDesc.Text = currencyTable.Rows[0]["DESC"].ToString();
                            txtMasterName.Text = currencyTable.Rows[0]["MASTERNAME"].ToString();
                            txtDecimalDigits.Text = currencyTable.Rows[0]["DECIMALDIGITS"].ToString();
                            txtRoundingDigit.Text = currencyTable.Rows[0]["ROUNDINGDIGIT"].ToString();
                            ddlOrder.SelectedValue = currencyTable.Rows[0]["ORDER"].ToString();
                            ddlStatus.SelectedValue = currencyTable.Rows[0]["STATUS"].ToString();
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
        string ccyid = Utility.KillSqlInjection(txtCurrencyId.Text.Trim());
        string currencyNumber = Utility.KillSqlInjection(txtCurrencyNumber.Text.Trim());
        string sCurrencyId = Utility.KillSqlInjection(txtSCurrencyId.Text.Trim());
        string currencyName = Utility.KillSqlInjection(txtCurrencyName.Text.Trim());
        string currencyMasterName = Utility.KillSqlInjection(txtMasterName.Text.Trim());
        string currencyDesc = Utility.KillSqlInjection(txtCurrencyDesc.Text.Trim());
        string decimalDigits = Utility.KillSqlInjection(txtDecimalDigits.Text.Trim());
        string roundingDigit = Utility.KillSqlInjection(txtRoundingDigit.Text.Trim());
        string status = Utility.KillSqlInjection(ddlStatus.SelectedValue);
        int order = int.Parse(Utility.KillSqlInjection(ddlOrder.SelectedValue));
        string user = Session["userName"].ToString();

        #region Validation

        if (ccyid == string.Empty)
        {
            lblError.Text = Resources.labels.currencyidrequired;
            return;
        }

        if (sCurrencyId == string.Empty)
        {
            lblError.Text = Resources.labels.scurrencyidrequired;
            return;
        }

        if (currencyName == string.Empty)
        {
            lblError.Text = Resources.labels.currencynamerequired;
            return;
        }

        if (decimalDigits == string.Empty)
        {
            lblError.Text = Resources.labels.decimaldigitsrequired;
            return;
        }

        if (roundingDigit == string.Empty)
        {
            lblError.Text = Resources.labels.roundingdigitreuired;
            return;
        }

        #endregion

        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                try
                {
                    new Currency().InsertCurrency(
                        ccyid,
                        sCurrencyId,
                        currencyNumber,
                        currencyName,
                        currencyMasterName,
                        currencyDesc,
                        decimalDigits, roundingDigit, status, order, user, ref IPCERRORCODE,
                        ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.addcurrencysuccessful;
                        pnAdd.Enabled = false;
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                }
                catch (IPCException IPCex)
                {
                    Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name,
                        IPCex.ToString(), Request.Url.Query);
                    Log.GoToErrorPage(IPCex.ToString(), Request.Url.Query);
                }
                catch (Exception ex)
                {
                    Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                        this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name,
                        ex.ToString(), Request.Url.Query);
                    Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                        Request.Url.Query);
                }

                break;
            case IPC.ACTIONPAGE.EDIT:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                try
                {
                    new Currency().UpdateCurrency(ccyid, sCurrencyId, currencyNumber, currencyName, currencyMasterName,
                        currencyDesc, decimalDigits, roundingDigit, status, order, user, ref IPCERRORCODE,
                        ref IPCERRORDESC);
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
                }
                catch (IPCException IPCex)
                {
                    SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name,
                        System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                        this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name,
                        ex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                        Request.Url.Query);
                }

                break;
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
        txtCurrencyDesc.Text = string.Empty;
        txtCurrencyId.Text = string.Empty;
        txtCurrencyName.Text = string.Empty;
        txtSCurrencyId.Text = string.Empty;
        txtCurrencyNumber.Text = string.Empty;
        txtDecimalDigits.Text = string.Empty;
        txtMasterName.Text = string.Empty;
        txtRoundingDigit.Text = string.Empty;
        ddlOrder.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        btnSave.Enabled = true;
    }
}