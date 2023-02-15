using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Constant;

public partial class Widgets_SEMSPRODUCTROLE_Controls_Widget : WidgetBase
{
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;

    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }
    public string PRODUCTTYPE
    {
        get { return ViewState["PRODUCTTYPE"] != null ? (string)ViewState["PRODUCTTYPE"] : IPC.CONSUMER; }
        set { ViewState["PRODUCTTYPE"] = value; }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        TabProductHelper.LoadConfig();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
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
            DataSet dsProduct = new SmartPortal.SEMS.Product().GetProductByCondition(string.Empty, string.Empty, "P", string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlProductType.DataSource = dsProduct;
                ddlProductType.DataTextField = "PRODUCTNAME";
                ddlProductType.DataValueField = "PRODUCTID";
                ddlProductType.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }

            DataSet ListContractLevel = new SmartPortal.SEMS.ContractLevel().GetAllContractLevel(string.Empty, string.Empty, "A", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlContractLevel.DataSource = ListContractLevel;
                ddlContractLevel.DataTextField = "CONTRACT_LEVEL_NAME";
                ddlContractLevel.DataValueField = "CONTRACT_LEVEL_ID";
                ddlContractLevel.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    LoadRoleByProductType();
                    break;
                default:
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    string[] key = ID.Split('|');
                    ddlProductType.SelectedValue = key[0].ToString().Trim();
                    ddlContractLevel.SelectedValue = key[1].ToString().Trim();
                    LoadRoleByProductType();
                    LoadRoleSelected(key[0].ToString().Trim(), key[1].ToString().Trim(), PRODUCTTYPE);
                    break;
            }

            LoadTooltip();

            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    btsave.Visible = false;
                    pnFocus.Enabled = false;
                    cblMB.Enabled = false;
                    cblWallet.Enabled = false;
                    cblAM.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    pnFocus.Enabled = false;
                    break;
            }
            #endregion
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string proID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlProductType.SelectedValue.Trim());
        decimal CONTRACT_LEVEL_ID = Convert.ToDecimal(SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlContractLevel.SelectedValue.Trim()));

        #region Tạo bảng chứa quyền product
        //tao bang chua thong tin customer
        DataTable tblProductUserRight = new DataTable();
        DataColumn colProID = new DataColumn("colProID");
        DataColumn colRoleID = new DataColumn("colRoleID");
        DataColumn colCONTRACT_LEVEL_ID = new DataColumn("colCONTRACT_LEVEL_ID");

        //add vào table
        tblProductUserRight.Columns.Add(colProID);
        tblProductUserRight.Columns.Add(colRoleID);
        tblProductUserRight.Columns.Add(colCONTRACT_LEVEL_ID);

        //tao 1 dong du lieu Quyền MB của Product
        foreach (ListItem liMB in cblMB.Items)
        {
            if (liMB.Selected)
            {
                DataRow rowMB = tblProductUserRight.NewRow();
                rowMB["colProID"] = proID;
                rowMB["colRoleID"] = liMB.Value.Trim();
                rowMB["colCONTRACT_LEVEL_ID"] = CONTRACT_LEVEL_ID;
                tblProductUserRight.Rows.Add(rowMB);
            }
        }

        //tao 1 dong du lieu Quyền Wallet của Product
        foreach (ListItem liWallet in cblWallet.Items)
        {
            if (liWallet.Selected)
            {
                DataRow rowEW = tblProductUserRight.NewRow();
                rowEW["colProID"] = proID;
                rowEW["colRoleID"] = liWallet.Value.Trim();
                rowEW["colCONTRACT_LEVEL_ID"] = CONTRACT_LEVEL_ID;
                tblProductUserRight.Rows.Add(rowEW);
            }
        }

        //tao 1 dong du lieu Quyền Agent Merchant của Product
        foreach (ListItem liAM in cblAM.Items)
        {
            if (liAM.Selected)
            {
                DataRow rowEW = tblProductUserRight.NewRow();
                rowEW["colProID"] = proID;
                rowEW["colRoleID"] = liAM.Value.Trim();
                rowEW["colCONTRACT_LEVEL_ID"] = CONTRACT_LEVEL_ID;
                tblProductUserRight.Rows.Add(rowEW);
            }
        }
        # endregion
        if (tblProductUserRight.Rows.Count == 0)
        {
            lblError.Text = Resources.labels.pleaseselectproductrole;
            return;
        }
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                try
                {
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    new SmartPortal.SEMS.Product().InsertRole(proID, CONTRACT_LEVEL_ID, tblProductUserRight, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.themquyenchosanphamthanhcong;
                        btsave.Visible = false;
                        pnFocus.Enabled = false;
                        cblMB.Enabled = false;
                        cblAM.Enabled = false;
                        cblWallet.Enabled = false;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                }
                break;
            case IPC.ACTIONPAGE.EDIT:
                try
                {
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    new SmartPortal.SEMS.Product().UpdateRole(proID, CONTRACT_LEVEL_ID, tblProductUserRight, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.suaquyenchosanphamthanhcong;
                        btsave.Visible = false;
                        pnFocus.Enabled = false;
                        cblMB.Enabled = false;
                        cblAM.Enabled = false;
                        cblWallet.Enabled = false;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                }
                break;
        }
    }
    protected void btback_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void ddlProductType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRoleByProductType();
        LoadTooltip();
    }
    protected void LoadRoleByProductType()
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.Product().GetProductByID(ddlProductType.SelectedValue.ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable productTable = new DataTable();
                productTable = ds.Tables[0];
                if (productTable.Rows.Count != 0)
                {
                    PRODUCTTYPE = productTable.Rows[0]["ProductType"].ToString();
                    LoadRole(PRODUCTTYPE);
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }

            HideTab();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void LoadRole(string ProductType)
    {
        try
        {
            switch (ProductType)
            {
                case IPC.AGENTMERCHANT:
                    //hien thi cac role cua mobile
                    cblMB.DataSource = new SmartPortal.SEMS.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.AM, string.Empty, "MBA", ref IPCERRORCODE, ref IPCERRORDESC);
                    cblMB.DataTextField = "ROLENAME";
                    cblMB.DataValueField = "ROLEID";
                    cblMB.DataBind();
                    //hien thi cac role cua wallet
                    cblWallet.Items.Clear();
                    //hien thi cac role cua agent merchant
                    cblAM.DataSource = new SmartPortal.SEMS.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.AM, string.Empty, "EAM", ref IPCERRORCODE, ref IPCERRORDESC);
                    cblAM.DataTextField = "ROLENAME";
                    cblAM.DataValueField = "ROLEID";
                    cblAM.DataBind();
                    break;
                case IPC.CONSUMER:
                    //hien thi cac role cua mobile
                    cblMB.DataSource = new SmartPortal.SEMS.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.MB, string.Empty, "NOR", ref IPCERRORCODE, ref IPCERRORDESC);
                    cblMB.DataTextField = "ROLENAME";
                    cblMB.DataValueField = "ROLEID";
                    cblMB.DataBind();

                    //hien thi cac role cua wallet
                    cblWallet.DataSource = new SmartPortal.SEMS.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.MB, string.Empty, "WAL", ref IPCERRORCODE, ref IPCERRORDESC);
                    cblWallet.DataTextField = "ROLENAME";
                    cblWallet.DataValueField = "ROLEID";
                    cblWallet.DataBind();

                    //hien thi cac role cua agent merchant
                    cblAM.Items.Clear();
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void LoadRoleSelected(string ProductID, string Contract, string ProductType)
    {
        try
        {
            DataTable tblRoleDefault = new DataTable();
            switch (ProductType)
            {
                case IPC.AGENTMERCHANT:
                    //lay role mac dinh MB
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.AM, ProductID, Convert.ToDecimal(Contract), "MBA", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    foreach (ListItem liMB in cblMB.Items)
                    {
                        if (tblRoleDefault.Select("ROLEID=" + liMB.Value).Length != 0)
                        {
                            liMB.Selected = true;
                        }
                        else
                        {
                            liMB.Selected = false;
                        }
                    }

                    //lay role mac dinh Agent Merchant
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.AM, ProductID, Convert.ToDecimal(Contract), "EAM", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    foreach (ListItem liAM in cblAM.Items)
                    {
                        if (tblRoleDefault.Select("ROLEID=" + liAM.Value).Length != 0)
                        {
                            liAM.Selected = true;
                        }
                        else
                        {
                            liAM.Selected = false;
                        }
                    }
                    break;
                case IPC.CONSUMER:
                    //lay role mac dinh MB
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.MB, ProductID, Convert.ToDecimal(Contract), "NOR", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    foreach (ListItem liMB in cblMB.Items)
                    {
                        if (tblRoleDefault.Select("ROLEID=" + liMB.Value).Length != 0)
                        {
                            liMB.Selected = true;
                        }
                        else
                        {
                            liMB.Selected = false;
                        }
                    }

                    //lay role mac dinh Wallet
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.MB, ProductID, Convert.ToDecimal(Contract), "WAL", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    foreach (ListItem liWallet in cblWallet.Items)
                    {
                        if (tblRoleDefault.Select("ROLEID=" + liWallet.Value).Length != 0)
                        {
                            liWallet.Selected = true;
                        }
                        else
                        {
                            liWallet.Selected = false;
                        }
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public void HideTab()
    {
        try
        {
            if (PRODUCTTYPE == IPC.CONSUMER)
            {
                liTabWL.Visible = true;
                liTabAM.Visible = false;
            } 
            else if (PRODUCTTYPE == IPC.AGENTMERCHANT)
            {
                liTabWL.Visible = false;
                liTabAM.Visible = true;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    public void LoadTooltip()
    {
        try
        {

            #region show tooltip chi tiet ve role
            DataTable tblRoleDefault = new DataTable();
            //MB
            foreach (ListItem liMB in cblMB.Items)
            {
                tblRoleDefault = (new SmartPortal.SEMS.Role().GetTranOfRole(liMB.Value, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                string tooltip = string.Empty;
                foreach (DataRow row in tblRoleDefault.Rows)
                {
                    tooltip += row["PAGENAME"].ToString() + "<br/>";
                }
                if (tblRoleDefault.Rows.Count != 0)
                {
                    liMB.Attributes.Add("onmouseover", "Tip('" + tooltip + "', LEFT, true, BGCOLOR, '#FF9900', FADEIN, 400)");
                    liMB.Attributes.Add("onmouseout", "UnTip()");
                }
            }
            //Wallet
            foreach (ListItem liWL in cblWallet.Items)
            {
                tblRoleDefault = (new SmartPortal.SEMS.Role().GetTranOfRole(liWL.Value, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                string tooltip = string.Empty;
                foreach (DataRow row in tblRoleDefault.Rows)
                {
                    tooltip += row["PAGENAME"].ToString() + "<br/>";
                }
                if (tblRoleDefault.Rows.Count != 0)
                {
                    liWL.Attributes.Add("onmouseover", "Tip('" + tooltip + "', LEFT, true, BGCOLOR, '#FF9900', FADEIN, 400)");
                    liWL.Attributes.Add("onmouseout", "UnTip()");
                }
            }
            //Agent Merchant
            foreach (ListItem liAM in cblAM.Items)
            {
                tblRoleDefault = (new SmartPortal.SEMS.Role().GetTranOfRole(liAM.Value, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                string tooltip = string.Empty;
                foreach (DataRow row in tblRoleDefault.Rows)
                {
                    tooltip += row["PAGENAME"].ToString() + "<br/>";
                }
                if (tblRoleDefault.Rows.Count != 0)
                {
                    liAM.Attributes.Add("onmouseover", "Tip('" + tooltip + "', LEFT, true, BGCOLOR, '#FF9900', FADEIN, 400)");
                    liAM.Attributes.Add("onmouseout", "UnTip()");
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}