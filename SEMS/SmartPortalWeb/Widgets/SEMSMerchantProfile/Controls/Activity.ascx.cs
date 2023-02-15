using SmartPortal.Constant;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSConsumerProfile_Controls_Activity : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    String countRow = "0";
    string MerchantID = string.Empty;
    string ACTION = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        MerchantID = GetParamsPage(IPC.ID)[0].Trim();
        ACTION = GetActionPage();
        loadCombobox();
        BindData();
    }


    private void loadCombobox_Status()
    {
        // Save list Wallet_Status in cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_Status"];
            //Check Wallet Status is null ?
            if (ds == null)
            {
                ds = _service.GetValueList("WALLET", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlStatus.DataSource = ds;
                        ddlStatus.DataValueField = "ValueID";
                        ddlStatus.DataTextField = "Caption";
                        ddlStatus.DataBind();
                    }
                }
                Cache.Insert("Wallet_Status", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlStatus.DataSource = (DataSet)Cache["Wallet_Status"];
                ddlStatus.DataValueField = "ValueID";
                ddlStatus.DataTextField = "Caption";
                ddlStatus.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void loadCombobox_KYCLevel()
    {
        // Save list KYCLevel Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_KYCLevel"];
            if (ds == null)
            {
                object[] searchObject = new object[] { string.Empty };
                ds = _service.common("SEMS_BO_GET_INFO_KYC", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlKyclevel.DataSource = ds;
                        ddlKyclevel.DataValueField = "KycID";
                        ddlKyclevel.DataTextField = "KycName";
                        ddlKyclevel.DataBind();
                    }
                }
                Cache.Insert("Wallet_KYCLevel", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlKyclevel.DataSource = (DataSet)Cache["Wallet_KYCLevel"];
                ddlKyclevel.DataValueField = "KycID";
                ddlKyclevel.DataTextField = "KycName";
                ddlKyclevel.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void loadCombobox_WalletLevel()
    {
        // Save list WalletLevel Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_WalletLevel"];
            if (ds == null)
            {
                object[] _object = new object[] { string.Empty };
                ds = _service.common("SEMS_BO_LST_CON_LV", _object, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddWalletLevel.DataSource = ds;
                        ddWalletLevel.DataValueField = "ContractLevelID";
                        ddWalletLevel.DataTextField = "ContractLevelName";
                        ddWalletLevel.DataBind();
                    }
                }
                //Save list Wallet Level in cache about 10 min, after Wallet_WalletLevel = null
                Cache.Insert("Wallet_WalletLevel", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddWalletLevel.DataSource = (DataSet)Cache["Wallet_WalletLevel"];
                ddWalletLevel.DataValueField = "ContractLevelID";
                ddWalletLevel.DataTextField = "ContractLevelName";
                ddWalletLevel.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void loadCombobox()
    {
        loadCombobox_Status();
        loadCombobox_KYCLevel();
        loadCombobox_WalletLevel();
    }

    void loadData()
    {
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { MerchantID };
        ds = _service.common("WAL_GET_MER_BY_ID", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count < 1) return;
                DataTable dataTable = ds.Tables[0];
                txtMerchantCode.Text = dataTable.Rows[0]["USERID"].ToString();
                txtDateCreate.Text = dataTable.Rows[0]["DATECREATED"].ToString();
                txtPhoneNumber.Text = dataTable.Rows[0]["PHONE_NUMBER"].ToString();
                txtLastModifiedate.Text = dataTable.Rows[0]["DATEMODIFIED"].ToString();
                ddlStatus.SelectedValue = dataTable.Rows[0]["AM_STATUS"].ToString();
                txtCreateBy.Text = dataTable.Rows[0]["USERCREATED"].ToString();
                ddlKyclevel.SelectedValue = dataTable.Rows[0]["KYC_ID"].ToString();
                txtApproveBy.Text = dataTable.Rows[0]["USER_APPROVED"].ToString();
                ddWalletLevel.SelectedValue = dataTable.Rows[0]["CONTRAC_LEVEL_ID"].ToString();
            }
        }
    }

    private void loadData_Repeater()
    {
        rptData.DataSource = null;
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { MerchantID, GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
        ds = _service.common("SEMS_MER_ACTIVITY", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                countRow = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0][IPC.TRECORDCOUNT].ToString();
                rptData.DataSource = ds;
                rptData.DataBind();
            }
        }
        GridViewPaging.total = countRow;
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
                    btnSave.Visible = false;
                    loadData();
                    loadData_Repeater();
                    break;
                default:
                    loadData();
                    loadData_Repeater();
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        switch (commandName)
        {
            case IPC.ACTIONPAGE.DETAILS:
                Response.Redirect("~/default.aspx?p=176&a=DETAILS&ID=" + commandArg);
                break;
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class=\"content \" >");
        sb.Append("<div>");
        writer.Write(sb.ToString());
        base.Render(writer);
        writer.Write("</div>");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}