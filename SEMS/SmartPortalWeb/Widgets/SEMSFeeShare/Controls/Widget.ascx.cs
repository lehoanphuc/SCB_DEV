using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.Model;

public partial class Widgets_SEMSREGIONFEE_Controls_Widget : WidgetBase
{
    public static bool isAscend = false;
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string checking = "0";
    public static int i = 1;
    string bien = "";
    int ladder = -1;
    public static int a = 0;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    
    public string _TITLE
    {
        get { return lblTitleBranch.Text; }
        set { lblTitleBranch.Text = value; }
    }

    
    public List<FeeShare> getListFeeShare()
    {
            List<FeeShare> dataList = new List<FeeShare>();
            //-- add all existing values to a list
            foreach (RepeaterItem item in rptData.Items)
            {
                dataList.Add(
                                 new FeeShare()
                                 {
                                     FeeShareTypeID = (item.FindControl("txtFeeShareTypeIDTable") as Label).Text,
                                     BillerID = (item.FindControl("txtBillerIDTable") as Label).Text,
                                     BillerName = (item.FindControl("txtBillerNameTable") as Label).Text,
                                     IsLadder = (item.FindControl("txtIsLadderTable") as Label).Text
                                 }); ;

            }

            return dataList;
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            // txtFlatAmount.Attributes.Add("onkeyup", "executeComma('" + txtFlatAmount.ClientID + "')");
            ViewState["UserID"] = HttpContext.Current.Session["userID"].ToString();
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                loadCombobox();
                cbIsLadder_CheckedChanged(sender, e);
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox_TransactionType()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = _service.GetValueList("TransactionType", "WAL_SHAREFEE", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlTransactionType.DataSource = ds;
                    ddlTransactionType.DataValueField = "VALUEID";
                    ddlTransactionType.DataTextField = "CAPTION";
                    ddlTransactionType.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    private void loadCombobox_Biller()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { };
            ds = _service.common("GET_BILLERFEE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddBiller.DataSource = ds;
                        ddBiller.DataValueField = "BILLERID";
                        ddBiller.DataTextField = "BILLERNAME";
                        ddBiller.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCombobox()
    {
        loadCombobox_TransactionType();
        loadCombobox_Biller();
    }
    public void loadEditAndViewData(string id)
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(id), 0, 100 };
            ds = _service.common("SEMS_SF_VIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dataTable = ds.Tables[0];
                    txtShareFeecode.Text = dataTable.Rows[0]["FeeShareCode"].ToString();
                    txtShareFeeName.Text = dataTable.Rows[0]["FeeShareName"].ToString();
                    ddlTransactionType.SelectedValue = dataTable.Rows[0]["TransactionType"].ToString();
                    txtDesc.Text = dataTable.Rows[0]["Description"].ToString();
                    if (dataTable.Rows[0]["IsLadder"].ToString() == "True")
                    {
                        cbIsLadder.Checked = true;
                    }
                    else
                    {
                        cbIsLadder.Checked = false;
                    }
                }
                if (ddlTransactionType.SelectedValue == "B")
                {
                    if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
                    {
                        if(!ds.Tables[0].Rows[0]["BillerID"].ToString().Equals(""))
                        {
                            rptData.DataSource = ds;
                            rptData.DataBind();
                        }
                        else
                        {
                            rptData.DataSource = null;
                            rptData.DataBind();
                        }
                        
                    }
                }
                else
                {
                    //cbIsLadder.Checked = true;
                    pnBiller.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public void loadDataTable(string id)
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(id), 0, 100 };
            ds = _service.common("SEMS_SF_VIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ddlTransactionType.SelectedValue == "B")
            {
                if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
                {
                    rptData.DataSource = ds;
                    rptData.DataBind();
                }
            }
            else
            {
                
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
            setDefault();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    string id3 = GetParamsPage(IPC.ID)[0].Trim();
                    loadEditAndViewData(id3);
                    //loadDataTable(id3);
                    txtShareFeecode.Enabled = false;
                    ddlTransactionType.Enabled = false;
                    pnBiller.Enabled = true;
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    string id2 = GetParamsPage(IPC.ID)[0].Trim();
                    loadEditAndViewData(id2);
                    btsave.Visible = false;
                    pnRegion.Enabled = false;
                    pnBiller.Enabled = false;
                    btClear.Visible = false;
                    btnAdd.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    public void setDefault()
    {
        lblError.Text = string.Empty;
        txtShareFeecode.BorderColor = System.Drawing.Color.Empty;
        txtShareFeeName.BorderColor = System.Drawing.Color.Empty;
        ddlTransactionType.BorderColor = System.Drawing.Color.Empty;
    }
    private bool checkvalidate()
    {
        setDefault();
        #region Validation

        if (txtShareFeecode.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.FeeShareCode + Resources.labels.IsNotNull;
            txtShareFeecode.BorderColor = System.Drawing.Color.Red;
            txtShareFeecode.Focus();
            return false;
        }
        if (txtShareFeeName.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.FeeShareName + Resources.labels.IsNotNull;
            txtShareFeeName.BorderColor = System.Drawing.Color.Red;
            txtShareFeeName.Focus();
            return false;
        }
        if (ddlTransactionType.SelectedValue == "B" && rptData.Items.Count == 0)
        {
            lblError.Text = "Add Biller in table below";
            ddlTransactionType.BorderColor = System.Drawing.Color.Red;
            ddlTransactionType.Focus();
            return false;
        }
        return true;
        #endregion
    }
    void setValue()
    {
        if (cbIsLadder.Checked == true)
        {
            checking = "1";
        }
        else { checking = "0"; }

        if (cbIsLadderBiller.Checked == true)
        {
            ladder = 1;
        }
        else { ladder = 0; }

        if (ddlTransactionType.SelectedValue == "N")
        {
            bien = " ." + checking + ",";
        }
        else
        {
            List<FeeShare> feeShares = getListFeeShare();
            foreach (var item in feeShares)
            {
                if (item.IsLadder == "True")
                {
                    ladder = 1;
                }
                else { ladder = 0; }
                bien = bien.ToString() + item.BillerID.ToString() + "." + ladder.ToString() + ",";
            }
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            
            string UserID = HttpContext.Current.Session["userID"].ToString();
            if (!checkvalidate()) { return; }
            setValue();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
                    {
                        lblError.Text = "User does not have permission.";
                        return;
                    }
                    try
                    {
                        string User = HttpContext.Current.Session["userID"].ToString();
                        DataSet ds = new DataSet();
                        object[] searchObject = new object[] {
                            Utility.KillSqlInjection(txtShareFeecode.Text.Trim()),
                            Utility.KillSqlInjection(txtShareFeeName.Text.Trim()),
                            Utility.KillSqlInjection (ddlTransactionType.SelectedValue),
                            Utility.KillSqlInjection (txtDesc.Text.Trim()),
                            Utility.KillSqlInjection (User) ,
                            Utility.KillSqlInjection (bien)
                        };
                        ds = _service.common("SEMS_SF_ADD", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE == "0")
                        {
                            btsave.Enabled = false;
                            pnRegion.Enabled = false;
                            pnBiller.Enabled = false;
                            lblError.Text = Resources.labels.addsuccessfully;
                            pnresulttable.Enabled = false;
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
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                    {
                        lblError.Text = "User does not have permission.";
                        return;
                    }
                    try
                    {
                        string id = GetParamsPage(IPC.ID)[0].Trim();
                        DataSet ds = new DataSet();
                        object[] searchObject = new object[] { 
                            Utility.KillSqlInjection(id), 
                            Utility.KillSqlInjection(txtShareFeeName.Text.Trim()), 
                            Utility.KillSqlInjection(txtDesc.Text.Trim()),
                            Utility.KillSqlInjection ("") };
                        ds = _service.common("SEMS_SF_UPDATE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            setDefault();
                            btsave.Enabled = false;
                            pnRegion.Enabled = false;
                            pnBiller.Enabled = false;
                            lblError.Text = Resources.labels.updatesuccessfully;
                            pnresulttable.Enabled = false;
                            foreach(RepeaterItem repeater in rptData.Items)
                            {
                                ((LinkButton)repeater.FindControl("linkID")).Enabled = false;
                                ((LinkButton)repeater.FindControl("linkID")).CssClass = "btn btn-secondary";
                            }
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


    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    

    protected void btClear_Click(object sender, EventArgs e)
    {
        setDefault();
        a = 0;
        BindData();
        ACTION = GetActionPage();
        if (ACTION == IPC.ACTIONPAGE.ADD)
        {
            txtShareFeecode.Enabled = true;
            txtShareFeeName.Enabled = true;
            ddlTransactionType.Enabled = true;
            txtShareFeecode.Text = string.Empty;
            pnBiller.Enabled = true;
            rptData.DataSource = null;
            rptData.DataBind();
        }
        if (ACTION == IPC.ACTIONPAGE.EDIT)
        {
            txtShareFeecode.Enabled = false;
            ddlTransactionType.Enabled = false;
        }
        cbIsLadder.Checked = false;
        txtShareFeeName.Text = string.Empty;
        txtDesc.Text = string.Empty;
        pnRegion.Enabled = true;
        btsave.Enabled = true;
        
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        if (CheckPermitPageAction(commandName))
        {
            if (ACTION == IPC.ACTIONPAGE.EDIT)
            {
                switch (commandName)
                {
                    case IPC.ACTIONPAGE.DELETE:
                        DataSet ds = new DataSet();
                        object[] searchObject = new object[] {
                            Utility.KillSqlInjection(commandArg)
                        };
                        ds = _service.common("SEMS_DelFSB", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
                        {
                            lblError.Text = Resources.labels.deletesuccessfully;
                            rptData.DataSource = ds;
                            rptData.DataBind();
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                        }
                        break;
                }
            }
            else
            {
                switch (commandName)
                {
                    case IPC.ACTIONPAGE.DELETE:
                        List<FeeShare> feeShares = getListFeeShare();
                        FeeShare feeShare = feeShares.Find(x => x.BillerID == commandArg);
                        feeShares.Remove(feeShare);
                        lblError.Text = Resources.labels.deletesuccessfully;
                        rptData.DataSource = feeShares;
                        rptData.DataBind();
                        break;
                }
            }
        }
        
        else{
            lblError.Text = "User does not have permission.";
            return;
        }
    }


    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                LinkButton lbDelete;
                //Label thedit, thdelete, tdedit, tddelete, test;

                if (ACTION == IPC.ACTIONPAGE.DETAILS)
                {
                    lbDelete = (LinkButton)e.Item.FindControl("linkID");
                    lbDelete.Enabled = false;
                    lbDelete.CssClass = "btn btn-secondary";
                    lbDelete.OnClientClick = null;
                }
                if (ACTION == IPC.ACTIONPAGE.EDIT && a == 1)
                {
                    lbDelete = (LinkButton)e.Item.FindControl("linkID");
                    lbDelete.Enabled = false;
                    lbDelete.CssClass = "btn btn-secondary";
                    lbDelete.OnClientClick = null;
                }
                if (ACTION == IPC.ACTIONPAGE.ADD && a == 1)
                {
                    lbDelete = (LinkButton)e.Item.FindControl("linkID");
                    lbDelete.Enabled = false;
                    lbDelete.CssClass = "btn btn-secondary";
                    lbDelete.OnClientClick = null;
                }

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void cbIsLadder_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlTransactionType.SelectedValue.Equals("N"))
        {
            pnBiller.Visible = false;
            cbIsLadder.Enabled = true;
        }
        else
        {
            pnBiller.Visible = true;
            cbIsLadder.Enabled = false;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        List<FeeShare> feeShares = getListFeeShare();
        FeeShare feeShare = feeShares.Find(x => x.BillerID == ddBiller.SelectedValue);
        if (feeShare != null)
        {
            lblError.Text = ddBiller.Text + " already exists";
            return;
        }
        try
        {
            if (ACTION == IPC.ACTIONPAGE.ADD)
            {
                feeShares.Add(new FeeShare()
                {
                    FeeShareTypeID = ddBiller.SelectedValue,
                    BillerID = ddBiller.SelectedValue,
                    BillerName = ddBiller.SelectedItem.Text,
                    IsLadder = cbIsLadderBiller.Checked.ToString()
                });
                rptData.DataSource = feeShares;
                rptData.DataBind();
            }
            if (ACTION == IPC.ACTIONPAGE.EDIT)
            {
                
                string _ladder = "0";
                if (cbIsLadderBiller.Checked) _ladder = "1"; 
                string id = GetParamsPage(IPC.ID)[0].Trim();
                DataSet ds = new DataSet();
                object[] searchObject = new object[] {
                            Utility.KillSqlInjection(id),
                            Utility.KillSqlInjection(ddBiller.SelectedValue),
                            Utility.KillSqlInjection (_ladder.ToString())
                        };
                ds = _service.common("SEMS_InsLoadFSB", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
                {
                    rptData.DataSource = ds;
                    rptData.DataBind();
                }
                else
                {
                    lblError.Text = IPCERRORDESC;
                }
               
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
   
}
