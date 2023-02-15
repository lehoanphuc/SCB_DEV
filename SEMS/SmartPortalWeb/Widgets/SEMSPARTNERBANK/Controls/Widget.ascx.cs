using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using OfficeOpenXml.FormulaParsing.Utilities;
using Resources;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSPARTNERBANK_Controls_Widget : WidgetBase
{
    private string ACTION = "";
    private string IPCERRORCODE = "";
    private string IPCERRORDESC = "";

    public string _TITLE
    {
        set { lblTitleBank.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (!IsPostBack) BindData();
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }
    #region Load Data
    private void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    btnClear.Enabled = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataTable bankTable = new DataTable();
                    bankTable =
                        new Partner().GetBankDetailsByID(ID, ref IPCERRORCODE, ref IPCERRORDESC)
                            .Tables[0];
                    if (bankTable.Rows.Count != 0)
                    {
                        txtbankcode.Text = bankTable.Rows[0]["BankCode"].ToString();
                        txtbankname.Text = bankTable.Rows[0]["BankName"].ToString();
                        ddlStatus.SelectedValue = bankTable.Rows[0]["Status"].ToString();
                        //ddlmanualpartnerbank.SelectedValue = bankTable.Rows[0]["IsManual"].ToString();
                        //lbldetermination.Text = bankTable.Rows[0]["Determination"].ToString();
                    }
                    break;
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    btsave.Enabled = false;
                    txtbankcode.Enabled = false;
                    ddlStatus.Enabled = false;
                    //lbldetermination.Enabled = false;
                    //ddlmanualpartnerbank.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtbankcode.Enabled = false;
                    btnClear.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.ToString(),
                Request.Url.Query);
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }
    #endregion
    #region Event
    protected void btsave_Click(object sender, EventArgs e)
    {
        string bankcode = Utility.KillSqlInjection(txtbankcode.Text.Trim());
        string bankname = Utility.KillSqlInjection(txtbankname.Text.Trim());
        string status = Utility.KillSqlInjection(ddlStatus.SelectedValue.ToString());
        //string determination = Utility.KillSqlInjection(lbldetermination.Text.Trim());
        //string detectmanual = Utility.KillSqlInjection(ddlmanualpartnerbank.Text.Trim());
        string determination = string.Empty;
        string detectmanual = string.Empty;;
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:

                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                try
                {
                    DataSet ds = new DataSet();
                    ds = new Partner().InsertPartnerBank(bankcode, bankname, determination, status, detectmanual, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = labels.themdoitacthanhcong;
                        pnAdd.Enabled = false;
                        btsave.Enabled = false;
                    }
                    else
                    {
                        lblError.Text = labels.themdoitactrungbankcode;
                    }
                }
                catch (IPCException IPCex)
                {
                    Log.RaiseError(IPCex.ToString(), GetType().BaseType.Name,
                        MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
                    Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
                }
                catch (Exception ex)
                {
                    Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                        GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name,
                        ex.ToString(), Request.Url.Query);
                    Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                        Request.Url.Query);
                }

                break;
            case IPC.ACTIONPAGE.EDIT:
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                try
                {
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet ds = new DataSet();
                    ds = new Partner().UpdatePartnerBank(ID,bankcode, bankname, determination, status, detectmanual, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = labels.editotherbankthanhcong;
                        pnAdd.Enabled = false;
                        btsave.Visible = false;
                    }
                    else
                    {
                        throw new IPCException(IPCERRORCODE);
                    }
                }
                catch (IPCException IPCex)
                {
                    Log.RaiseError(IPCex.ToString(), GetType().BaseType.Name,
                        MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
                    Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
                }
                catch (Exception ex)
                {
                    Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                        GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name,
                        ex.ToString(), Request.Url.Query);
                    Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                        Request.Url.Query);
                }

                break;
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        pnAdd.Enabled = true;
        btsave.Enabled = true;
        txtbankcode.Text = string.Empty;
        txtbankname.Text = string.Empty;
        //lbldetermination.Text = string.Empty;
        //ddlmanualpartnerbank.SelectedIndex = 0;
    }

    #endregion
}