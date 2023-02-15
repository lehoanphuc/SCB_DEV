using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using Label = System.Web.UI.WebControls.Label;


public partial class Widgets_SEMSProduct_Controls_Widget : WidgetBase
{
    private string ACTION = string.Empty;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        TabProductHelper.LoadConfig();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            ACTION = GetActionPage();
       
            if (!IsPostBack)
            {
                LoadDll();
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    void LoadDll()
    {
        try
        {
            //ddlProductType.Items.Add(new ListItem(Resources.labels.agentmerchant, IPC.AGENTMERCHANT));
            //ddlProductType.Items.Add(new ListItem(Resources.labels.individual, IPC.CONSUMER));
            ddlProductType.Items.Add(new ListItem(Resources.labels.Corporate, IPC.CORPORATECONTRACT));
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
                    ddlProductType_OnSelectedIndexChanged(null, EventArgs.Empty);
                    break;
                default:
                    //btnClear.Enabled = false;
                    cblSubUserType.Enabled = false;
                    ddlStatus.Enabled = false;
                    ddlAccGroup.Enabled = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet ds = new SmartPortal.SEMS.Product().GetProductByID(ID, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable productTable = new DataTable();
                        productTable = ds.Tables[0];
                        if (productTable.Rows.Count != 0)
                        {
                            txtmasp.Text = productTable.Rows[0]["PRODUCTID"].ToString();
                            txttensp.Text = productTable.Rows[0]["PRODUCTNAME"].ToString();
                            ddlProductType.SelectedValue = productTable.Rows[0]["ProductType"].ToString();
                            ddlStatus.SelectedValue = productTable.Rows[0]["Status"].ToString();
                            txtdesc.Text = productTable.Rows[0]["DESCRIPTION"].ToString();
                            ddlProductType_OnSelectedIndexChanged(null, EventArgs.Empty);
                            ddlAccGroup.SelectedValue = productTable.Rows[0]["GRP_ID"].ToString();
                            LoadRoleSelected(productTable.Rows[0]["PRODUCTID"].ToString(), "0", productTable.Rows[0]["ProductType"].ToString());
                        }
                    }
                    else
                    {
                        throw new IPCException(IPCERRORCODE);
                    }
                    break;
            }
            loadSubUseType();
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    Panel1.Enabled = false;
                    cblMB.Enabled = false;
                    cblAM.Enabled = false;
                    cblIB.Enabled = false;
                    cblWallet.Enabled = false;
                    btsave.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtmasp.Enabled = false;
                    ddlProductType.Enabled = false;
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
        try
        {
            string proID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtmasp.Text.Trim());
            string ProName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txttensp.Text.Trim());
            string DESC = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtdesc.Text.Trim());
            string productType = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlProductType.SelectedValue.ToString().Trim());
            string Accgroup = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlAccGroup.SelectedValue.ToString().Trim());
            string Status = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlStatus.SelectedValue.ToString().Trim());
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
            //tao 1 dong du lieu Quyền Wallet của Product
            foreach (ListItem liWallet in cblWallet.Items)
            {
                if (liWallet.Selected)
                {
                    DataRow rowEW = tblProductUserRight.NewRow();
                    rowEW["colProID"] = proID;
                    rowEW["colRoleID"] = liWallet.Value.Trim();
                    rowEW["colCONTRACT_LEVEL_ID"] = 0;
                    tblProductUserRight.Rows.Add(rowEW);
                }
            }
            //tao 1 dong du lieu Quyền MB của Product
            foreach (ListItem liMB in cblMB.Items)
            {
                if (liMB.Selected)
                {
                    DataRow rowMB = tblProductUserRight.NewRow();
                    rowMB["colProID"] = proID;
                    rowMB["colRoleID"] = liMB.Value.Trim();
                    rowMB["colCONTRACT_LEVEL_ID"] = 0;
                    tblProductUserRight.Rows.Add(rowMB);
                }
            }
            //tao 1 dong du lieu Quyền Agent Merchant của Product
            if (productType.Equals(IPC.AGENTMERCHANT) )
            {
                foreach (ListItem liAM in cblAM.Items)
                {
                    if (liAM.Selected)
                    {
                        DataRow rowEW = tblProductUserRight.NewRow();
                        rowEW["colProID"] = proID;
                        rowEW["colRoleID"] = liAM.Value.Trim();
                        rowEW["colCONTRACT_LEVEL_ID"] = 0;
                        tblProductUserRight.Rows.Add(rowEW);
                    }
                }
            }
            else
            {
                //tao 1 dong du lieu Quyền MB của Product
                foreach (ListItem liIB in cblIB.Items)
                {
                    if (liIB.Selected)
                    {
                        DataRow rowIB = tblProductUserRight.NewRow();
                        rowIB["colProID"] = proID;
                        rowIB["colRoleID"] = liIB.Value.Trim();
                        rowIB["colCONTRACT_LEVEL_ID"] = 0;
                        tblProductUserRight.Rows.Add(rowIB);
                    }
                }
            }
            if (tblProductUserRight.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.pleaseselectproductrole;
                return;
            }
            #endregion
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    try
                    {
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                        new SmartPortal.SEMS.Product().Insert(proID, ProName, "P", DESC, productType, Accgroup, Status, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            new SmartPortal.SEMS.Product().InsertRole(proID, 0, tblProductUserRight, ref IPCERRORCODE, ref IPCERRORDESC);
                            if (IPCERRORCODE != "0")
                            {
                                lblError.Text = IPCERRORDESC;
                                return;
                            }
                            //hunglt
                            InsertSubUseType(proID, productType);
                            lblError.Text = Resources.labels.addnewproductsuccessfully;
                            Panel1.Enabled = false;
                            btsave.Enabled = false;
                            cblMB.Enabled = false;
                            cblAM.Enabled = false;
                            cblWallet.Enabled = false;
                            cblSubUserType.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
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
                        new SmartPortal.SEMS.Product().Update(proID, ProName, "P", DESC, productType, Accgroup, Status, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE == "0")
                        {
                            new SmartPortal.SEMS.Product().UpdateRole(proID, 0, tblProductUserRight, ref IPCERRORCODE, ref IPCERRORDESC);
                            if (IPCERRORCODE != "0")
                            {
                                lblError.Text = IPCERRORDESC;
                                return;
                            }
                            InsertSubUseType(proID, productType);
                            lblError.Text = Resources.labels.Editproductsuccessfull;
                            Panel1.Enabled = false;
                            btsave.Enabled = false;
                            cblMB.Enabled = false;
                            cblAM.Enabled = false;
                            cblIB.Enabled = false;
                            cblWallet.Enabled = false;
                            cblSubUserType.Enabled = false;
                            ddlStatus.Enabled = false;
                            ddlAccGroup.Enabled = false;
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
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ddlProductType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAcctGroup();
        LoadRoleByProductType();
        LoadTooltip();
        loadSubUseType();
    }
    protected void LoadRoleByProductType()
    {
        try
        {
            LoadRole(ddlProductType.SelectedValue.ToString());
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
                    cblMB.DataSource = new SmartPortal.SEMS.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.MB, string.Empty, "", ref IPCERRORCODE, ref IPCERRORDESC);
                    cblMB.DataTextField = "ROLENAME";
                    cblMB.DataValueField = "ROLEID";
                    cblMB.DataBind();
                    //hien role cua IB
                    cblAM.DataSource = new SmartPortal.SEMS.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.AM, string.Empty, "", ref IPCERRORCODE, ref IPCERRORDESC);
                    cblAM.DataTextField = "ROLENAME";
                    cblAM.DataValueField = "ROLEID";
                    cblAM.DataBind();
                    break;
                case IPC.CONSUMER:
                    //hien thi cac role cua mobile
                    cblMB.DataSource = new SmartPortal.SEMS.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.MB, string.Empty, "", ref IPCERRORCODE, ref IPCERRORDESC);
                    cblMB.DataTextField = "ROLENAME";
                    cblMB.DataValueField = "ROLEID";
                    cblMB.DataBind();

                    ////hien thi cac role cua wallet
                    //cblWallet.DataSource = new SmartPortal.SEMS.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.MB, string.Empty, "", ref IPCERRORCODE, ref IPCERRORDESC);
                    //cblWallet.DataTextField = "ROLENAME";
                    //cblWallet.DataValueField = "ROLEID";
                    //cblWallet.DataBind();

                    //hien role cua IB
                    cblIB.DataSource = new SmartPortal.SEMS.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.IB, string.Empty, "", ref IPCERRORCODE, ref IPCERRORDESC);
                    cblIB.DataTextField = "ROLENAME";
                    cblIB.DataValueField = "ROLEID";
                    cblIB.DataBind();
                    //hien thi cac role cua agent merchant
                    cblAM.Items.Clear();
                    break;
                case IPC.CORPORATECONTRACT:
                    //hien thi cac role cua mobile
                    cblMB.DataSource = new SmartPortal.SEMS.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.MB, string.Empty, "", ref IPCERRORCODE, ref IPCERRORDESC);
                    cblMB.DataTextField = "ROLENAME";
                    cblMB.DataValueField = "ROLEID";
                    cblMB.DataBind();

                    //hien role cua IB
                    cblIB.DataSource = new SmartPortal.SEMS.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.IB, string.Empty, "", ref IPCERRORCODE, ref IPCERRORDESC);
                    cblIB.DataTextField = "ROLENAME";
                    cblIB.DataValueField = "ROLEID";
                    cblIB.DataBind();
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

                    //lay role mac dinh IB
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.IB, ProductID, Convert.ToDecimal(Contract), "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    foreach (ListItem liIB in cblIB.Items)
                    {
                        if (tblRoleDefault.Select("ROLEID=" + liIB.Value).Length != 0)
                        {
                            liIB.Selected = true;
                        }
                        else
                        {
                            liIB.Selected = false;
                        }
                    }
                    //lay role mac dinh MB
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.MB, ProductID, Convert.ToDecimal(Contract), "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
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
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.AM, ProductID, Convert.ToDecimal(Contract), "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
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

                    //lay role mac dinh IB
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.IB, ProductID, Convert.ToDecimal(Contract), "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    foreach (ListItem liIB in cblIB.Items)
                    {
                        if (tblRoleDefault.Select("ROLEID=" + liIB.Value).Length != 0)
                        {
                            liIB.Selected = true;
                        }
                        else
                        {
                            liIB.Selected = false;
                        }
                    }

                    //lay role mac dinh MB
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.MB, ProductID, Convert.ToDecimal(Contract), "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
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
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.MB, ProductID, Convert.ToDecimal(Contract), "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
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
                case IPC.CORPORATECONTRACT:

                    //lay role mac dinh IB
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.IB, ProductID, Convert.ToDecimal(Contract), "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    foreach (ListItem liIB in cblIB.Items)
                    {
                        if (tblRoleDefault.Select("ROLEID=" + liIB.Value).Length != 0)
                        {
                            liIB.Selected = true;
                        }
                        else
                        {
                            liIB.Selected = false;
                        }
                    }

                    //lay role mac dinh MB
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.MB, ProductID, Convert.ToDecimal(Contract), "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
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
                    tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.WAL, ProductID, Convert.ToDecimal(Contract), "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
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
        subpanel.Visible = true;
        lblaccount.Visible = true;
        try
        {
            if (ddlProductType.SelectedValue.ToString() == IPC.CONSUMER)
            {
                liTabAM.Visible = false;
                liTabIB.Visible = true;
            }
            else if (ddlProductType.SelectedValue.ToString() == IPC.AGENTMERCHANT)
            {
                liTabAM.Visible = true;
                liTabIB.Visible = false;
            }
            else if (ddlProductType.SelectedValue.ToString() == IPC.CORPORATECONTRACT)
            {
                liTabAM.Visible = false;
                liTabIB.Visible = true;
                subpanel.Visible = false;
               lblaccount.Visible = false;
                //ddlProductType.Visible = false;
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
            ////Wallet
            //foreach (ListItem liWL in cblWallet.Items)
            //{
            //    tblRoleDefault = (new SmartPortal.SEMS.Role().GetTranOfRole(liWL.Value, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            //    string tooltip = string.Empty;
            //    foreach (DataRow row in tblRoleDefault.Rows)
            //    {
            //        tooltip += row["PAGENAME"].ToString() + "<br/>";
            //    }
            //    if (tblRoleDefault.Rows.Count != 0)
            //    {
            //        liWL.Attributes.Add("onmouseover", "Tip('" + tooltip + "', LEFT, true, BGCOLOR, '#FF9900', FADEIN, 400)");
            //        liWL.Attributes.Add("onmouseout", "UnTip()");
            //    }
            //}
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

            foreach (ListItem liIB in cblIB.Items)
            {
                tblRoleDefault = (new SmartPortal.SEMS.Role().GetTranOfRole(liIB.Value, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                string tooltip = string.Empty;
                foreach (DataRow row in tblRoleDefault.Rows)
                {
                    tooltip += row["PAGENAME"].ToString() + "<br/>";
                }
                if (tblRoleDefault.Rows.Count != 0)
                {
                    liIB.Attributes.Add("onmouseover", "Tip('" + tooltip + "', LEFT, true, BGCOLOR, '#FF9900', FADEIN, 400)");
                    liIB.Attributes.Add("onmouseout", "UnTip()");
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
    //protected void btnClear_Click(object sender, EventArgs e)
    //{
    //    lblError.Text = string.Empty;
    //    Panel1.Enabled = true;
    //    btsave.Enabled = true;
    //    cblMB.Enabled = true;
    //    cblSubUserType.Enabled = true;
    //    cblAM.Enabled = true;
    //    cblWallet.Enabled = true;
    //    txtmasp.Text = string.Empty;
    //    txttensp.Text = string.Empty;
    //    txtdesc.Text = string.Empty;
    //    cblMB.ClearSelection();
    //    cblWallet.ClearSelection();
    //    cblIB.ClearSelection();
    //    cblAM.ClearSelection();
    //    ddlProductType.SelectedIndex = 0;
    //    ddlProductType_OnSelectedIndexChanged(null, EventArgs.Empty);
    //    ddlStatus.SelectedIndex = 0;
    //}
    protected void btback_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    private void LoadAcctGroup()
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.Product().GetAccountingGroup(Utility.KillSqlInjection(ddlProductType.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlAccGroup.DataSource = ds;
                ddlAccGroup.DataTextField = "VARNAME";
                ddlAccGroup.DataValueField = "VARVALUE";
                ddlAccGroup.DataBind();
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
    }
    protected void gvcheckboxsub_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblSubName, lblSubType;
            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                lblSubName = (Label)e.Row.FindControl("lblSubName");
                lblSubType = (Label)e.Row.FindControl("lblSubType");
                lblSubName.Text = drv["SubUserCode"].ToString();
                lblSubType.Text = drv["SubUserType"].ToString();
                if (drv.Row.Table.Columns.Contains("IsUse"))
                {
                    cbxSelect.Checked = drv["IsUse"].ToString() == "1" ? true : false;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadSubUseType()
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.Product().LoadProductSubUserType(SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtmasp.Text.Trim()), ddlProductType.SelectedValue.ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cblSubUserType.DataSource = ds;
                cblSubUserType.DataTextField = "SubUserType";
                cblSubUserType.DataValueField = "SubUserCode";
                cblSubUserType.DataBind();
                foreach (ListItem liSub in cblSubUserType.Items)
                {
                    DataRow[] dr = ds.Tables[0].Select("SubUserCode=" + liSub.Value);
                    if (dr.Length>0)
                    {
                        liSub.Selected = dr[0]["IsUse"].Equals(1) ? true : false;
                    }
                    else
                    {
                        liSub.Selected = false;
                    }
                }
            }
            else
            {
                DataSet ds1 = new SmartPortal.SEMS.Product().LoadSubUserByType(Utility.KillSqlInjection(ddlProductType.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    cblSubUserType.DataSource = ds1;
                    cblSubUserType.DataTextField = "SubUserType";
                    cblSubUserType.DataValueField = "SubUserCode";
                    cblSubUserType.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void InsertSubUseType(string productId, string productType)
    {
        CheckBox cbxDelete;
        Label lblSubName;
        string strSubName = "";
        string strIsUse = "";
        try
        {
            foreach (ListItem liSub in cblSubUserType.Items)
            {
                if (liSub.Selected)
                {
                    strIsUse += "1" + "#";
                }
                else
                {
                    strIsUse += "0" + "#";
                }
                strSubName += liSub.Value.Trim() + "#";
            }
            if(productType.Equals(SmartPortal.Constant.IPC.CORPORATECONTRACT))
            {
                strIsUse = "1";
            }
            string[] listSubName = strSubName.Split('#');
            string[] listIsUse = strIsUse.Split('#');
            
            for (int i = 0; i < listSubName.Length - 1; i++)
            {
                new SmartPortal.SEMS.Product().InsertProductSubUserType(productId, productType, listSubName[i], listIsUse[i], ref IPCERRORCODE,
                    ref IPCERRORDESC);
            }
         
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                "");
        }
    }

}
