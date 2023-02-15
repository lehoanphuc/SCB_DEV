using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Constant;
using SmartPortal.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using SmartPortal.SEMS;

public partial class Widgets_SEMSCorpApproveStructure_Controls_Widget : WidgetBase
{
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty; 
    private string CONTRACT_NO
    {
        get
        {
            return ViewState["CONTRACT_NO"] == null ? string.Empty : ViewState["CONTRACT_NO"].ToString();
        }
        set { ViewState["CONTRACT_NO"] = value; }
    }
    private DataTable TableToInsertDBForGroup
    {
        get { return ViewState["TableToInsertDBForGroup"] as DataTable; }
        set
        {
            ViewState["TableToInsertDBForGroup"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                ddlGroup.DataSource = IPC.DICGROUPID;
                ddlGroup.DataTextField = "Value";
                ddlGroup.DataValueField = "Key";
                ddlGroup.DataBind();

                BindData();
                GridViewPaging.Visible = false;
                divResult.Visible = false;
                GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvContractSearch.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvContractSearch.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        GetAllContractByCondition();
    }
    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    lblTitleGroup.Text = Resources.labels.addusergroup;
                    GetAllContractByCondition();
                    pnSeachContract.Visible = true;
                    pnGroup.Visible = false;
                    btnNext.Enabled = true;
                    btnSave.Visible = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    lblTitleGroup.Text = Resources.labels.editusergroup;
                    CONTRACT_NO = GetParamsPage(IPC.ID)[0].Trim();
                    FillGroupData(CONTRACT_NO);
                    pnSeachContract.Visible = false;
                    pnGroup.Visible = true;
                    pnGV.Visible = true;
                    btnNext.Visible = false;
                    btnSave.Enabled = true;
                    break;
                default:
                    lblTitleGroup.Text = Resources.labels.deleteusergroup;
                    CONTRACT_NO = GetParamsPage(IPC.ID)[0].Trim();
                    FillGroupData(CONTRACT_NO);
                    pnSeachContract.Visible = false;
                    pnGroup.Visible = true;
                    pnGV.Visible = true;
                    btnNext.Visible = false;
                    btnSave.Enabled = false;
                    gvGroup.Columns[4].Visible = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void GetAllContractByCondition()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvContractSearch.PageIndex * gvContractSearch.PageSize) return;
            ApprovalWorkflowModel workflow = new ApprovalWorkflowModel();
            string CONTRACTNO = Utility.KillSqlInjection(txtSearchContractNo.Text.Trim());
            string FULLNAME = Utility.KillSqlInjection(txtSearchFullname.Text.Trim());
            string LICENSEID = Utility.KillSqlInjection(txtlicenseid.Text.Trim());
            string CUSTCODE = Utility.KillSqlInjection(txtcustcode.Text.Trim());
            string HASGROUP = "0";
            DataSet dsResult = new SmartPortal.SEMS.ApprovalWorkflow().ApprovalWorkflowGetUserID(CONTRACTNO, FULLNAME, LICENSEID, CUSTCODE, HASGROUP, gvContractSearch.PageSize, gvContractSearch.PageIndex * gvContractSearch.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvContractSearch.DataSource = dsResult;
                gvContractSearch.DataBind();
            }

            if (dsResult.Tables[0].Rows.Count > 0)
            {
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dsResult.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvContractSearch.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvContractSearch.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            GetAllContractByCondition();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void FillGroupData(string CONTRACT_NO)
    {
        try
        {
            DataSet dsResult = new SmartPortal.SEMS.ApprovalStructure().GetContractGroupDetail(CONTRACT_NO, ref IPCERRORCODE, ref IPCERRORDESC);
            DataTable dt = new DataTable();
            if (IPCERRORCODE.Equals("0"))
            {
                if (dsResult != null && dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    dt = dsResult.Tables[0];
                    dt.PrimaryKey = new DataColumn[] { dt.Columns[0], dt.Columns[1] };
                    TableToInsertDBForGroup = dt;
                }
                else
                {
                    dt = null;
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
            BindDataGVGroup();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void BindDataGVGroup()
    {
        try
        {
            if (TableToInsertDBForGroup == null) return;
            gvGroup.DataSource = TableToInsertDBForGroup;
            gvGroup.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void colRbContractNo_onChange(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvContractSearch.Rows.Count; i++)
            {
                GridViewRow rowOld = gvContractSearch.Rows[i];
                RadioButton colRbContractNo = (RadioButton)rowOld.Cells[0].Controls[1];
                colRbContractNo.Checked = false;
            }
            RadioButton item = (RadioButton)sender;
            GridViewRow row = item.Parent.Parent as GridViewRow;
            CONTRACT_NO = (row.FindControl("colContractNo") as Label).Text;
            item.Checked = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvGroup.PageIndex = e.NewPageIndex;
        BindDataGVGroup();
    }
    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtNameGroup.Text))
            {
                lblError.Text = Resources.labels.pleaseinputgroupname;
                return;
            }

            pnGV.Visible = true;
            InitTableToInsertGroups();
            DataTable dt = TableToInsertDBForGroup;
            DataRow row = dt.NewRow();
            row["GroupID"] = ddlGroup.SelectedValue;
            row["ContractNo"] = CONTRACT_NO;
            row["GroupName"] = txtNameGroup.Text;
            row["GroupShortName"] = Utility.KillSqlInjection(txtShortNameGroup.Text.Trim());
            row["GroupDesc"] = Utility.KillSqlInjection(txtDescGroup.Text.Trim());

            try
            {
                dt.Rows.Add(row);
                TableToInsertDBForGroup = dt;
                BindDataGVGroup();
            }
            catch
            {
                lblError.Text = Resources.labels.choosegroupother;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnNext_OnClick(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(CONTRACT_NO))
        {
            lblError.Text = Resources.labels.pleaseselectcontract;
            return;
        }
        pnSeachContract.Visible = false;
        pnGroup.Visible = true;
        btnNext.Visible = false;
        btnSave.Visible = true;
        InitTableToInsertGroups();
        TableToInsertDBForGroup.Clear();
        BindDataGVGroup();
    }
    private void InitTableToInsertGroups()
    {
        DataTable dt = TableToInsertDBForGroup;
        if (dt != null && dt.Rows.Count > 0) return;
        dt = new DataTable();
        string[] cols = new string[] {
            "GroupID",
            "ContractNo",
            "GroupName",
            "GroupShortName",
            "GroupDesc"
        };
        dt = AddColHeaderForTable(dt, cols);
        dt.PrimaryKey = new DataColumn[] { dt.Columns[0], dt.Columns[1] };
        TableToInsertDBForGroup = dt;
    }
    private DataTable AddColHeaderForTable(DataTable dt, string[] arr)
    {
        if (dt == null) dt = new DataTable();
        if (arr.Length > 0)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                dt.Columns.Add(new DataColumn(arr[i]));
            }
        }
        return dt;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvGroup.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.savegroupempty;
                return;
            }
            bool CanSave = true;
            if (!CanSave) return;

            DataTable dtGroup = SaveDataGroupPanel();
            if (dtGroup != null && dtGroup.Rows.Count > 0)
            {
                if (ACTION != IPC.ACTIONPAGE.EDIT)
                {
                    new SmartPortal.SEMS.ApprovalStructure().StructureInsertAll(dtGroup, ref IPCERRORCODE, ref IPCERRORDESC);
                    
                    if (IPCERRORCODE == "0")
                    {
                        pnAdd.Enabled = false;
                        gvGroup.Columns[4].Visible = false;
                        btnSave.Enabled = false;
                        lblError.Text = Resources.labels.addusergroupsuccessfully;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                }
                else
                {
                    try
                    {
                        DataTable dtDel = SaveDataDel();
                        new SmartPortal.SEMS.ApprovalStructure().StructureUpdateAll(dtDel, dtGroup, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            pnAdd.Enabled = false;
                            gvGroup.Columns[4].Visible = false;
                            btnSave.Enabled = false;
                            lblError.Text = Resources.labels.updateusergroupsuccessfully;
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
                }

            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private DataTable SaveDataDel()
    {
        try
        {
            string[] cols = new string[] {
                "ContractNo"
            };
            DataTable dt = new DataTable();
            dt = AddColHeaderForTable(dt, cols);
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
            DataRow row = dt.NewRow();
            int idx = 0;
            row[idx++] = Utility.KillSqlInjection(CONTRACT_NO);
            dt.Rows.Add(row);
            return dt;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        return null;
    }
    private DataTable SaveDataGroupPanel()
    {
        try
        {
            DataTable dt = TableToInsertDBForGroup;
            if (dt == null || dt.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.cannotsavedataempty;
                return null;
            }
            return dt;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        return null;
    }
    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void gvGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvGroup.Rows[e.RowIndex].Cells[1].FindControl("lbDelete")).CommandArgument;
            DataTable dt = TableToInsertDBForGroup;
            DataRow[] rows = dt.Select("GroupID = '" + commandArg + "'");
            if (rows.Length > 0)
            {
                if (ACTION == IPC.ACTIONPAGE.EDIT)
                {
                    new SmartPortal.SEMS.ApprovalStructure().CheckGroupBeforeDelete(CONTRACT_NO, commandArg, ref IPCERRORCODE,
                        ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                }
                dt.Rows.Remove(rows[0]);
            }
            TableToInsertDBForGroup = dt;
            BindDataGVGroup();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

}
