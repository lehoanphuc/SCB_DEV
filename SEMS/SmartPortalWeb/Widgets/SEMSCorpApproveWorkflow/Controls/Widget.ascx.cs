using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Constant;
using SmartPortal.Model;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using NCalc;

public partial class Widgets_SEMSCorpApproveWorkflow_Controls_Widget : WidgetBase
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
    private string WORKFLOWID
    {
        get
        {
            return ViewState["WORKFLOWID"] == null ? string.Empty : ViewState["WORKFLOWID"].ToString();
        }
        set { ViewState["WORKFLOWID"] = value; }
    }
    private string ISCLONECONFIG
    {
        get
        {
            return ViewState["ISCLONECONFIG"] == null ? string.Empty : ViewState["ISCLONECONFIG"].ToString();
        }
        set { ViewState["ISCLONECONFIG"] = value; }
    }
    private DataTable DTGRIDLEVELGROUP
    {
        get { return ViewState["DTGRIDLEVELGROUP"] as DataTable; }
        set { ViewState["DTGRIDLEVELGROUP"] = value; }
    }
    private DataTable DTGRIDTRANSACTION
    {
        get { return ViewState["DTGRIDTRANSACTION"] as DataTable; }
        set
        {
            ViewState["DTGRIDTRANSACTION"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            txtFrom.Attributes.Add("onkeypress", "executeComma('" + txtFrom.ClientID + "')");
            txtTo.Attributes.Add("onkeypress", "executeComma('" + txtTo.ClientID + "')");
            txtFrom.Attributes.Add("onkeyup", "executeComma('" + txtFrom.ClientID + "')");
            txtTo.Attributes.Add("onkeyup", "executeComma('" + txtTo.ClientID + "')");
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                LoadDll();
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
    void LoadDll()
    {
        try
        {
            ddlCurrency.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlCurrency.DataTextField = "CCYID";
            ddlCurrency.DataValueField = "CCYID";
            ddlCurrency.DataBind();
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
                    try
                    {
                        WORKFLOWID = GetParamsPage(IPC.ID)[0].Trim();
                        if (string.IsNullOrEmpty(WORKFLOWID))
                        {
                            ISCLONECONFIG = "0";
                        }
                        else
                        {
                            ISCLONECONFIG = "1";
                        }
                    }
                    catch (Exception e)
                    {
                        ISCLONECONFIG = "0";
                    }
                    
                    lblTitleGroup.Text = Resources.labels.addnewapprovalworkflow;
                    pnSeachContract.Visible = true;
                    btnNext.Enabled = true;
                    btnSave.Visible = false;
                    pnWorkflow.Visible = false;
                    GetAllContractByCondition();
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    WORKFLOWID = GetParamsPage(IPC.ID)[0].Trim();
                    lblTitleGroup.Text = Resources.labels.editapprovalworkflow;
                    pnSeachContract.Visible = false;
                    btnNext.Visible = false;
                    btnSave.Enabled = true;
                    pnWorkflow.Visible = true;
                    FillWorkflowData();
                    FillDataForGVUserGroup();
                    FillOrderData();
                    break;
                default:
                    WORKFLOWID = GetParamsPage(IPC.ID)[0].Trim();
                    lblTitleGroup.Text = Resources.labels.detailnewapprovalworkflow;
                    pnSeachContract.Visible = false;
                    btnNext.Visible = false;
                    btnSave.Enabled = false;
                    pnWorkflow.Visible = true;
                    FillWorkflowData();
                    FillDataForGVUserGroup();
                    FillOrderData();
                    pnAdd.Enabled = false;
                    pnApproval.Enabled = false;
                    gvTransaction.Columns[3].Visible = false;
                    gvTransaction.Columns[4].Visible = false;
                    btnSave.Enabled = false;
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
            string HASGROUP = "1";
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
    protected void btnNext_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(CONTRACT_NO))
            {
                lblError.Text = Resources.labels.pleaseselectcontract;
                return;
            }
            pnSeachContract.Visible = false;
            btnNext.Visible = false;
            btnSave.Visible = true;
            pnWorkflow.Visible = true;
            GetDataListAccTran();
            FillDataForGVUserGroup();
            if (ISCLONECONFIG.Equals("0"))
            {
                AutoGenWorkflowID();
            }
            else
            {
                LoadCloneConfigAndValidate();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void GetDataListAccTran()
    {
        try
        {
            ApprovalWorkflowModel workflow = new ApprovalWorkflowModel();
            workflow.CONTRACTNO = CONTRACT_NO;
            DataSet dsAcct = new SmartPortal.SEMS.ApprovalWorkflow().ApprovalWorkflowGetAcct(workflow, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlAccNumber.DataSource = dsAcct;
                ddlAccNumber.DataTextField = "ACCTNOTYPE";
                ddlAccNumber.DataValueField = "ACCTNO";
                ddlAccNumber.DataBind();
                ddlAccNumber.Items.Insert(0, new ListItem(Resources.labels.tatca, IPC.ALL));
                ddlAccNumber.SelectedIndex = 0;

            }

            DataSet dsResult = new SmartPortal.SEMS.Product().GetTranNameByContractNo(Utility.KillSqlInjection(CONTRACT_NO), ref IPCERRORCODE, ref IPCERRORDESC);
            ddlTransaction.DataSource = dsResult;
            ddlTransaction.DataTextField = "PAGENAME";
            ddlTransaction.DataValueField = "TRANCODE";
            ddlTransaction.DataBind();
            ddlTransaction.Items.Insert(0, new ListItem(IPC.ALL, IPC.ALL));
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void FillDataForGVUserGroup()
    {
        DataSet ds = new SmartPortal.SEMS.ApprovalStructure().GetContractGroupDetail(
            Utility.KillSqlInjection(CONTRACT_NO),
            ref IPCERRORCODE, ref IPCERRORDESC);

        if (IPCERRORCODE == "0")
        {
            DataTable dt = ds.Tables[0];
            gvUserGroup.DataSource = dt;
            gvUserGroup.DataBind();
            DTGRIDLEVELGROUP = dt;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AutoGenWorkflowID();
            string workflow = WORKFLOWID;
            DataTable dtWorkflow = SaveDataWorkFlow(workflow);
            DataTable dtDetail = null;
            if (!cbNeedApprove.Checked)
            {
                InitDataTableTransaction();
                dtDetail = DTGRIDTRANSACTION;
                DataRow dr = dtDetail.NewRow();
                dr["WorkflowID"] = workflow;
                dr["Ord"] = 0;
                dr["Formula"] = string.Empty;
                dr["Desc"] = string.Empty;
                dtDetail.Rows.Add(dr);
            }
            else
            {
                dtDetail = SaveDataWorkflowDetail(workflow);
            }
            if (dtDetail == null)
            {
                lblError.Text = Resources.labels.cannotsavedataempty;
                return;
            }

            if (dtWorkflow != null)
            {
                if (ACTION == IPC.ACTIONPAGE.EDIT)
                {
                    DataTable dtDel = SaveDataDel();
                    new SmartPortal.SEMS.ApprovalWorkflow().WorkflowUpdateAll(dtDel, dtWorkflow, dtDetail, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.updateworkflowsuccessful;
                        pnAdd.Enabled = false;
                        pnApproval.Enabled = false;
                        gvTransaction.Columns[3].Visible = false;
                        gvTransaction.Columns[4].Visible = false;
                        btnSave.Enabled = false;
                        return;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                }
                else
                {
                    new SmartPortal.SEMS.ApprovalWorkflow().WorkflowInsertAll(dtWorkflow, dtDetail, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.insertworkflowsuccessful;
                        pnAdd.Enabled = false;
                        pnApproval.Enabled = false;
                        gvTransaction.Columns[3].Visible = false;
                        gvTransaction.Columns[4].Visible = false;
                        btnSave.Enabled = false;
                        return;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
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
            string delworkflowid = GetParamsPage(IPC.ID)[0].Trim();
            string[] cols = new string[] {
                "WorkflowID"
            };
            DataTable dt = new DataTable();
            dt = AddColHeaderForTable(dt, cols);
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
            DataRow row = dt.NewRow();
            row["WorkflowID"] = Utility.KillSqlInjection(delworkflowid);
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
    private DataTable SaveDataWorkFlow(string workflow)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtFrom.Text))
            {
                lblError.Text = Resources.labels.pleaseinputfromlimit;
                return null;
            }
            //if (string.IsNullOrWhiteSpace(txtTo.Text) || txtTo.Text.Equals("0"))
            //{
            //    lblError.Text = Resources.labels.pleaseinputtolimit;
            //    return null;
            //}
            string[] cols = new string[] {
                "WorkflowID",
                "ContractNo",
                "AcctNo",
                "CCYID",
                "TranCode",
                "FromLimit",
                "ToLimit",
                "IsAOT",
                "NeedApprove",
                "DateCreated",
                "UserCreated",
                "SOURCEID"
            };
            DataTable dt = new DataTable();
            dt = AddColHeaderForTable(dt, cols);
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0], dt.Columns[3] };

            DataRow row = dt.NewRow();
            int idx = 0;
            row[idx++] = Utility.KillSqlInjection(workflow);
            row[idx++] = CONTRACT_NO;
            row[idx++] = ddlAccNumber.SelectedValue;
            row[idx++] = ddlCurrency.SelectedValue;
            row[idx++] = ddlTransaction.SelectedValue;
            row[idx++] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtFrom.Text.Trim(), ddlCurrency.SelectedValue);
            row[idx++] = cbToLimit.Checked ? "-1" : SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTo.Text.Trim(), ddlCurrency.SelectedValue); ;
            row[idx++] = cbIsAOT.Checked ? 1 : 0;
            row[idx++] = cbNeedApprove.Checked ? 1 : 0;
            row[idx++] = DateTime.Now.ToString(IPC.SHORTDATEFORMAT);
            row[idx++] = Session["userName"].ToString();
            row[idx++] = IPC.SOURCEIDVALUE;
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
    private DataTable SaveDataWorkflowDetail(string workflow)
    {
        DataTable dtTran = DTGRIDTRANSACTION;
        if (dtTran == null || dtTran.Rows.Count == 0)
            return null;

        for (int i = 0; i < dtTran.Rows.Count; i++)
        {
            DataRow row = dtTran.Rows[i];
            row["WorkflowID"] = workflow;
            row["Ord"] = i + 1;
            if (!string.IsNullOrWhiteSpace(row["Formula"].ToString()))
            {
                row["Formula"] = row["Formula"].ToString();
            }
        }
        if (dtTran.Columns.Contains("TranName")) dtTran.Columns.Remove("TranName");
        if (dtTran.Columns.Contains("LevelShortName")) dtTran.Columns.Remove("LevelShortName");
        if (dtTran.Columns.Contains("IsSequenceText")) dtTran.Columns.Remove("IsSequenceText");
        return dtTran;
    }
    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void cbToLimit_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbToLimit.Checked)
            {
                txtTo.Text = string.Empty;
                txtTo.Enabled = false;
            }
            else
            {
                txtTo.Text = "0";
                txtTo.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void cbNeedApprove_CheckedChanged(object sender, EventArgs e)
    {
        pnFormula.Visible = cbNeedApprove.Checked;
        pnApproval.Visible = cbNeedApprove.Checked;
        pnTransaction.Visible = cbNeedApprove.Checked;


        DTGRIDTRANSACTION = null;
        BindDataGVTransaction();
    }
    protected void gvUserGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUserGroup.PageIndex = e.NewPageIndex;
        FillDataForGVUserGroup();
    }
    protected void btnAddOrder_onclick(object sender, EventArgs e)
    {
        DataTable dt = DTGRIDTRANSACTION;
        if (dt == null)
        {
            InitDataTableTransaction();
            dt = DTGRIDTRANSACTION;
        }
        if (!IsValidFormula())
        {
            lblError.Text = Resources.labels.formulainvalid;
            return;
        }

        DataRow row = dt.NewRow();
        row["WorkflowID"] = Utility.KillSqlInjection(WORKFLOWID);
        row["Ord"] = dt.Rows.Count + 1;
        row["Formula"] = txtFormula.Text.Trim().ToUpper();
        row["Desc"] = Utility.KillSqlInjection(txtOrderDesc.Text.Trim());
        if (string.IsNullOrWhiteSpace(txtFormula.Text))
        {
            lblError.Text = Resources.labels.pleaseinputapproveformula;
            return;
        }

        DataTable GroupofStructure = new SmartPortal.SEMS.ApprovalStructure().GetContractGroupDetail(
                Utility.KillSqlInjection(CONTRACT_NO),
                ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];

        if (GroupofStructure == null)
        {
            lblError.Text = Resources.labels.cannotfindgroup;
            txtFormula.Focus();
            return;
        }
        else if (GroupofStructure != null)
        {
            string formula = AnalysisFormula();
            int countChar = 0;
            foreach (char item in formula.ToCharArray())
            {
                if (GroupofStructure.AsEnumerable().Where(r => r[0].ToString().Equals(item.ToString())).FirstOrDefault() != null)
                {
                    countChar++;
                }
            }
            if (countChar < formula.ToCharArray().Length)
            {
                lblError.Text = Resources.labels.cannotfindgroup;
                txtFormula.Focus();
                return;
            }
        }
        dt.Rows.Add(row);
        DTGRIDTRANSACTION = dt;
        BindDataGVTransaction();
    }
    private bool IsValidFormula()
    {
        if (string.IsNullOrWhiteSpace(txtFormula.Text)) return false;
        try
        {
            string formula = txtFormula.Text;
            formula = formula.ToUpper().Replace("OR", "|").Replace("AND", "&").Replace("+", "&");

            Regex regex = new Regex(@"[\d]{1,2}[A-Z]{1}");
            var matches = regex.Matches(formula);

            if (matches.Count > 0)
            {
                List<string> lsCon = matches.Cast<Match>().Select(x => x.Value).Distinct().ToList();

                foreach (string match in lsCon)
                {
                    formula = formula.Replace(match, "true");
                }

                var exResult = new Expression(formula).Evaluate();
                return Convert.ToBoolean(exResult);
            }
        }
        catch (Exception ex)
        {
        }
        return false;
    }
    private string AnalysisFormula(string value = "")
    {
        Regex regex = new Regex("[A-Z]+");
        string formula = ConvertFormula(value);
        if (string.IsNullOrWhiteSpace(formula)) return string.Empty;
        MatchCollection matchs = regex.Matches(formula);
        formula = string.Empty;
        foreach (Match str in matchs)
        {
            formula += str.Value;
        }
        return formula;
    }
    private void InitDataTableTransaction()
    {
        string[] cols = new string[] {
            "WorkflowID",
            "Ord",
            "Formula",
            "Desc"
        };
        DataTable dt = new DataTable();
        dt = AddColHeaderForTable(dt, cols);
        DTGRIDTRANSACTION = dt;
        gvTransaction.DataSource = dt;
        gvTransaction.DataBind();
    }
    private string ConvertFormula(string value = "")
    {
        string formula = string.IsNullOrWhiteSpace(value) ? Utility.KillSqlInjection(txtFormula.Text) : value;
        if (!string.IsNullOrWhiteSpace(formula))
        {
            formula = formula.ToUpper();
            formula = formula.Replace("OR", "||").Replace("+", "&").Replace(" ", "");
            formula = Regex.Replace(formula, "[a-zA-Z0-9]+", "${0}");
            formula = Regex.Replace(formula, "[0-9]+", "${0}*");
        }
        return formula;
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
    private void BindDataGVTransaction()
    {
        if (DTGRIDTRANSACTION != null)
        {
            gvTransaction.DataSource = DTGRIDTRANSACTION;
            gvTransaction.DataBind();
            if (DTGRIDTRANSACTION.Rows.Count > 0)
            {
                gvTransaction.Visible = true;
            }
            else
            {
                gvTransaction.Visible = false;
            }
        }
        else
        {
            gvTransaction.Visible = false;
        }
    }
    private void AutoGenWorkflowID()
    {
        try
        {
            if (ACTION != IPC.ACTIONPAGE.EDIT)
            {
                WORKFLOWID = new SmartPortal.SEMS.ApprovalWorkflow().GENARATE_WORKFLOWID(IPC.SOURCEIDVALUE, CONTRACT_NO, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvTransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvUserGroup.PageIndex = e.NewPageIndex;
            DataTable dt = DTGRIDTRANSACTION;
            gvUserGroup.DataSource = dt;
            gvUserGroup.DataBind();

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvTransaction_onRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                LinkButton down = (LinkButton)e.Row.FindControl("colUpArrow");
                LinkButton up = (LinkButton)e.Row.FindControl("colDownArrow");
                int idx = DTGRIDTRANSACTION.Rows.IndexOf(drv.Row);
                if (idx == 0) down.Visible = false;
                else if (idx == DTGRIDTRANSACTION.Rows.Count - 1) up.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void colUpArrow_onclick(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).Parent.Parent as GridViewRow;
            Label ord = (Label)row.FindControl("colOrd");
            DataTable dt = DTGRIDTRANSACTION;
            DataRow dataRow = dt.Select("Ord = '" + ord.Text + "'").FirstOrDefault();
            int idx = dt.Rows.IndexOf(dataRow);
            DataRow newRow = dt.NewRow();
            newRow.ItemArray = dataRow.ItemArray.Clone() as object[];
            dt.Rows.Remove(dataRow);
            dt.Rows.InsertAt(newRow, idx - 1);
            DTGRIDTRANSACTION = dt;
            ReOrderApprovalList();
            BindDataGVTransaction();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void colDownArrow_onclick(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).Parent.Parent as GridViewRow;
            Label ord = (Label)row.FindControl("colOrd");
            DataTable dt = DTGRIDTRANSACTION;
            DataRow dataRow = dt.Select("Ord = '" + ord.Text + "'").FirstOrDefault();
            int idx = dt.Rows.IndexOf(dataRow);
            DataRow newRow = dt.NewRow();
            newRow.ItemArray = dataRow.ItemArray.Clone() as object[];
            dt.Rows.Remove(dataRow);
            dt.Rows.InsertAt(newRow, idx + 1);
            DTGRIDTRANSACTION = dt;
            ReOrderApprovalList();
            BindDataGVTransaction();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void ReOrderApprovalList()
    {
        if (DTGRIDTRANSACTION != null)
        {
            DataTable dt = DTGRIDTRANSACTION;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                row["Ord"] = i + 1;
            }
            DTGRIDTRANSACTION = dt;
        }
    }
    protected void colTransactionDelete_onclick(object sender, EventArgs e)
    {
        try
        {
            if (DTGRIDTRANSACTION == null) { return; }
            string userlevel = ((sender as LinkButton).CommandArgument).ToString();
            DataTable dt = DTGRIDTRANSACTION;
            DataRow[] rows = dt.Select("Ord = '" + userlevel + "'");
            if (rows.Length > 0)
            {
                dt.Rows.Remove(rows[0]);
            }
            DTGRIDTRANSACTION = dt;
            ReOrderApprovalList();
            BindDataGVTransaction();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadCloneConfigAndValidate()
    {
        try
        {
            #region Clone Workflow info
            if (string.IsNullOrWhiteSpace(WORKFLOWID)) return;
            ApprovalWorkflowModel workflow = new ApprovalWorkflowModel();
            workflow.WORKFLOWID = WORKFLOWID;
            DataSet dsWorkflow = new SmartPortal.SEMS.ApprovalWorkflow().ApprovalWorkflowDetail(workflow, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0") && IsDataSetNotNull(dsWorkflow))
            {
                DataTable dt = dsWorkflow.Tables[0];
                DataRow row = dt.Rows[0];
                ddlTransaction.SelectedValue = row["TranCode"].ToString();
                txtFrom.Text = Utility.FormatMoney(row["FromLimit"].ToString(), row["CCYID"].ToString());
                if (Math.Abs(double.Parse(row["ToLimit"].ToString()) - (-1)) <= 0)
                {
                    cbToLimit.Checked = true;
                    txtTo.Text = string.Empty;
                    txtTo.Enabled = false;
                }
                else
                {
                    txtTo.Text = Utility.FormatMoney(row["ToLimit"].ToString(), row["CCYID"].ToString());
                }
                cbIsAOT.Checked = row["IsAOT"].ToString().Equals("1") ? true : false;
                cbNeedApprove.Checked = row["NeedApprove"].ToString().Equals("1") ? true : false;
                cbNeedApprove_CheckedChanged(null, EventArgs.Empty);
            }
            #endregion

            #region Clone Workflow Detail
            //Get Workflow Detail
            AWDetailModel detail = new AWDetailModel();
            detail.WORKFLOWID = WORKFLOWID;
            DataSet dsWorkflowDetail = new SmartPortal.SEMS.ApprovalWorkflow().AWDetailGetAll(detail, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0") && IsDataSetNotNull(dsWorkflowDetail))
            {
                DataTable dt = dsWorkflowDetail.Tables[0];
                if (!dt.Columns.Contains("WorkflowID")) dt.Columns.Add(new DataColumn("WorkflowID"));
                dt.Columns["WorkflowID"].SetOrdinal(0);
                List<DataRow> listRow = new List<DataRow>();
                DataTable dtGroup = DTGRIDLEVELGROUP;
                foreach (DataRow row in dt.Rows)
                {
                    row["WorkflowID"] = WORKFLOWID;

                    string formula = AnalysisFormula(row["Formula"].ToString());
                    int countChar = 0;
                    foreach (char item in formula.ToCharArray())
                    {
                        if (dtGroup.AsEnumerable().Where(r => r[0].ToString().Equals(item.ToString())).FirstOrDefault() != null)
                        {
                            countChar++;
                        }
                    }
                    if (countChar < formula.ToCharArray().Length)
                    {
                        listRow.Add(row);
                    }
                }
                foreach (DataRow row in listRow)
                {
                    dt.Rows.Remove(row);
                }
                if (dt.Rows.Count == 0 || dt.Rows[0]["ORD"].ToString().Equals("0"))
                {
                    dt = null;
                    pnFormula.Visible = false;
                    pnAdd.Visible = false;
                }

                DTGRIDTRANSACTION = dt;
                BindDataGVTransaction();
            }
            #endregion
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private bool IsDataSetNotNull(DataSet ds)
    {
        return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 ? true : false;
    }
    private void FillOrderData()
    {
        try
        {
            if (string.IsNullOrEmpty(WORKFLOWID)) return;
            AWDetailModel detail = new AWDetailModel();
            detail.WORKFLOWID = WORKFLOWID;
            DataSet dsResult = new SmartPortal.SEMS.ApprovalWorkflow().AWDetailGetAll(detail, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE.Equals("0") && IsDataSetNotNull(dsResult))
            {
                DataTable dt = dsResult.Tables[0];
                if (!dt.Columns.Contains("WorkflowID")) dt.Columns.Add(new DataColumn("WorkflowID"));
                dt.Columns["WorkflowID"].SetOrdinal(0);
                foreach (DataRow row in dt.Rows)
                {
                    row["WorkflowID"] = WORKFLOWID;
                    row["Formula"] = string.IsNullOrWhiteSpace(row["Formula"].ToString()) ? string.Empty : row["Formula"].ToString();
                }
                if (dt.Rows.Count == 1 && string.IsNullOrEmpty(dt.Rows[0]["Formula"].ToString()))
                {
                    gvTransaction.Visible = false;
                    InitDataTableTransaction();
                }
                else
                {
                    DTGRIDTRANSACTION = dt;
                    BindDataGVTransaction();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void FillWorkflowData()
    {
        try
        {
            if (string.IsNullOrEmpty(WORKFLOWID)) return;
            ApprovalWorkflowModel workflow = new ApprovalWorkflowModel();
            workflow.WORKFLOWID = WORKFLOWID;
            DataSet dsResult = new SmartPortal.SEMS.ApprovalWorkflow().ApprovalWorkflowDetail(workflow, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0") && IsDataSetNotNull(dsResult))
            {
                DataTable dt = dsResult.Tables[0];
                DataRow row = dt.Rows[0];
                txtFrom.Text = Utility.FormatMoney(row["FromLimit"].ToString(), row["CCYID"].ToString());
                if (Math.Abs(double.Parse(row["ToLimit"].ToString()) - (-1)) <= 0)
                {
                    cbToLimit.Checked = true;
                    txtTo.Text = string.Empty;
                    txtTo.Enabled = false;
                }
                else
                {
                    txtTo.Text = Utility.FormatMoney(row["ToLimit"].ToString(), row["CCYID"].ToString());
                }
                ddlTransaction.Enabled = ddlAccNumber.Enabled = ddlCurrency.Enabled = false;
                CONTRACT_NO = row["ContractNo"].ToString();
                GetDataListAccTran();
                ddlAccNumber.SelectedValue = row["AcctNo"].ToString();
                ddlTransaction.SelectedValue = row["TranCode"].ToString();
                cbIsAOT.Checked = row["IsAOT"].ToString().Equals("1") ? true : false;
                cbNeedApprove.Checked = row["NeedApprove"].ToString().Equals("1") ? true : false;
                cbNeedApprove_CheckedChanged(null, EventArgs.Empty);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
