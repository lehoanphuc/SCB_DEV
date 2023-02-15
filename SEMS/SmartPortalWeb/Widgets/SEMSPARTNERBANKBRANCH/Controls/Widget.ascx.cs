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

public partial class Widgets_SEMSPARTNERBANKBRANCH_Controls_Widget : WidgetBase
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
            if (!IsPostBack)
            {
                LoadBank();
                LoadRegion();
                BindData();
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
                        new Partner().GetBankBranchDetailsByID(ID, ref IPCERRORCODE, ref IPCERRORDESC)
                            .Tables[0];
                    if (bankTable.Rows.Count != 0)
                    {
                        txtbranchcode.Text = bankTable.Rows[0]["BranchCode"].ToString();
                        txtbranchname.Text = bankTable.Rows[0]["BranchName"].ToString();
                        ddlStatus.SelectedValue = bankTable.Rows[0]["Status"].ToString();
                        //lbloriginalbranchcode.Text = bankTable.Rows[0]["OriginalBranchCode"].ToString();
                        ddlBank.SelectedValue = bankTable.Rows[0]["PartnerBankID"].ToString();
                        if (!string.IsNullOrEmpty(bankTable.Rows[0]["RegionID"].ToString()))
                        {
                            ddlRegion.SelectedValue = bankTable.Rows[0]["RegionID"].ToString();
                        }
                    }
                    break;
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    btsave.Enabled = false;
                    txtbranchcode.Enabled = false;
                    ddlStatus.Enabled = false;
                    txtbranchname.Enabled = false;
                    //lbloriginalbranchcode.Enabled = false;
                    ddlBank.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtbranchcode.Enabled = false;
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
        string bankcode = Utility.KillSqlInjection(txtbranchcode.Text.Trim());
        string branchname = Utility.KillSqlInjection(txtbranchname.Text.Trim());
        string originalbranchcode = "";
        string partnerbankid = ddlBank.SelectedValue.ToString();
        string regionid = ddlRegion.SelectedValue.ToString();
        string status = Utility.KillSqlInjection(ddlStatus.SelectedValue.ToString());

        #region validate

        if (string.IsNullOrEmpty(bankcode))
        {
            lblError.Text = labels.Branchcodekhongrong;
            return;
        }
        if (string.IsNullOrEmpty(branchname))
        {
            lblError.Text = labels.Branchnamekhongrong;
            return;
        }
        if (string.IsNullOrEmpty(regionid))
        {
            return;
        }
        if (string.IsNullOrEmpty(partnerbankid))
        {
            return;
        }

        #endregion


        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:

                if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                try
                {
                    DataSet ds = new DataSet();
                    ds = new Partner().InsertPartnerBankBranch(bankcode, branchname, partnerbankid, originalbranchcode, regionid ,status, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = labels.themdoitacchinhanhthanhcong;
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
                    ds = new Partner().UpdatePartnerBankBranch(ID, bankcode, branchname, partnerbankid, originalbranchcode, regionid, status, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = labels.suachinhanhdoitacthanhcong;
                        pnAdd.Enabled = false;
                        btsave.Visible = false;
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
        txtbranchcode.Text = string.Empty;
        txtbranchname.Text = string.Empty;
    }
    #endregion


    private void LoadRegion()
    {
        try
        {
            DataSet dtRegion = new DataSet();
            dtRegion = new SmartPortal.SEMS.Partner().GetRegionALL(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlRegion.DataSource = dtRegion;
                ddlRegion.DataTextField = "RegionName";
                ddlRegion.DataValueField = "RegionID";
                ddlRegion.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
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
    private void LoadBank()
    {
        try
        {
            DataSet dts = new DataSet();
            dts = new SmartPortal.SEMS.Partner().GetBankALL(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlBank.DataSource = dts;
                ddlBank.DataTextField = "BankName";
                ddlBank.DataValueField = "BankID";
                ddlBank.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
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

}