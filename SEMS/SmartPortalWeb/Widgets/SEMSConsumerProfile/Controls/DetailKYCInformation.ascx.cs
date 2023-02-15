using SmartPortal.Constant;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSConsumerProfile_Controls_DetailKYCInformation : WidgetBase
{
    string ACTION = "";
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    String countRow = "0";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        ACTION = GetActionPage();
        if(!IsPostBack)
        {
            loadCombobox();
            BindData();
        }  
        GridViewPaging.pagingClickArgs += new EventHandler(btnSearch_Click);
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

    private void loadCombobox_Status()
    {
        DataSet ds = new DataSet();
        ds = _service.GetValueList("EBA_CustInfo", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = ds;
                ddlStatus.DataValueField = "VALUEID";
                ddlStatus.DataTextField = "CAPTION";
                ddlStatus.DataBind();
            }
        }
    }

    private void loadCombobox_WalletLevel()
    {
        DataSet ds = new DataSet();
        object[] loadContractLevel = new object[] { string.Empty };
        ds = _service.common("SEMS_BO_LST_CON_LV", loadContractLevel, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlWalletLevel.DataSource = ds;
                ddlWalletLevel.DataValueField = "CONTRACTLEVELID";
                ddlWalletLevel.DataTextField = "CONTRACTLEVELNAME";
                ddlWalletLevel.DataBind();
            }
        }
    }

    public void loadCombobox()
    {
        loadCombobox_Status();
        loadCombobox_WalletLevel();
    }


    void loadData()
    {
        string ID = GetParamsPage(IPC.ID)[0].Trim();
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { ID };
        ds = _service.common("SEMS_CON_INFO_VIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count < 1) return;
                DataTable dataTable = ds.Tables[0];
                ViewState["CUSTID"] = dataTable.Rows[0]["CUSTID"].ToString();
                txtConsumerCode.Text = dataTable.Rows[0]["CUSTID"].ToString();
                txtDateCreate.Text = dataTable.Rows[0]["DATECREATED_FORMAT"].ToString();
                txtPhoneNumber.Text = dataTable.Rows[0]["TEL"].ToString();
                txtLastModifiedate.Text = dataTable.Rows[0]["LASTMODIFIED_FORMAT"].ToString();
                ddlStatus.SelectedValue = dataTable.Rows[0]["STATUS"].ToString();
                txtCreateBy.Text = dataTable.Rows[0]["USERCREATED"].ToString();
                txtApproveBy.Text = dataTable.Rows[0]["USERAPPROVED"].ToString();
                ddlWalletLevel.SelectedValue = dataTable.Rows[0]["ContractLevelId"].ToString();
            }
        }
    }
    void loadData_Repeater()
    {
        try
        {
            DataSet ds = new DataSet();
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            object[] searchObject = new object[] { ID, GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_CO_KYCRE_CUSID", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    countRow = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0][IPC.TRECORDCOUNT].ToString();
                    rptData.DataSource = ds.Tables[0];
                    rptData.DataBind();
                }
            }
            GridViewPaging.total = countRow;
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
                ViewKYC(commandArg);
                break;
        }
        return;
    }
    public void ViewKYC( string RequestID)
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { RequestID };
            ds = _service.common("SEMS_CO_KYCRE_VIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    for( int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if( ds.Tables[0].Rows[i]["DocumentType"].ToString().Equals("NF"))
                        {
                            imgFrontSide.ImageUrl = "data:image/jpg;base64," + ds.Tables[0].Rows[i]["FILE"].ToString();
                            ImageFrontModel3.ImageUrl = imgFrontSide.ImageUrl;
                        }
                        if (ds.Tables[0].Rows[i]["DocumentType"].ToString().Equals("NB"))
                        {
                            imgBackSide.ImageUrl = "data:image/jpg;base64," + ds.Tables[0].Rows[i]["FILE"].ToString();
                            ImageBackModel4.ImageUrl = imgBackSide.ImageUrl;
                        }
                        if (ds.Tables[0].Rows[i]["DocumentType"].ToString().Equals("SN"))
                        {
                            imgSelfie.ImageUrl = "data:image/jpg;base64," + ds.Tables[0].Rows[i]["FILE"].ToString();
                            ImageSelfieModel5.ImageUrl = imgSelfie.ImageUrl;
                        }
                        pnViewImg.Visible = true;
                    }    
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadData_Repeater();
    }
}