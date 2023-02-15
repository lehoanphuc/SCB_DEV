using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NCalc;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_IBCorpUserApproveProcess_Controls_Widget : WidgetBase
{
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    protected bool IsDelete = true;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            txtFrom.Attributes.Add("onkeyup", "executeComma('" + txtFrom.ClientID + "',event)");
            txtTo.Attributes.Add("onkeyup", "executeComma('" + txtTo.ClientID + "',event)");
            ACTION = GetActionPage();

            if (!IsPostBack)
            {
                DataSet dsContract = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(Session["userID"].ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
                DataTable dtContract = new DataTable();
                dtContract = dsContract.Tables[0];
                if (dtContract.Rows.Count != 0)
                    CONTRACT_NO = dtContract.Rows[0]["CONTRACTNO"].ToString();

                DataSet dsResult = new SmartPortal.SEMS.Product().GetTranNameByContractNo(CONTRACT_NO, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                ddlTrans.DataSource = dsResult;
                ddlTrans.DataTextField = "PAGENAME";
                ddlTrans.DataValueField = "TRANCODE";
                ddlTrans.DataBind();
                ddlTrans.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));

                ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlCCYID.DataTextField = "CCYID";
                ddlCCYID.DataValueField = "CCYID";
                ddlCCYID.DataBind();

                DataSet dsGroup = new SmartPortal.IB.CorpUser().SearchCorpUserlevel(string.Empty, CONTRACT_NO, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                rptGroup.DataSource = dsGroup;
                rptGroup.DataBind();

                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void BindData()
    {
        try
        {
            GetListAccTran();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    pnProcessApprove.Visible = true;
                    WORKFLOWID = new SmartPortal.SEMS.ApprovalWorkflow().GENARATE_WORKFLOWID(IPC.SOURCEIBVALUE, CONTRACT_NO, Session["UserID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                    }
                    break;
                default:
                    pnProcessApprove.Visible = false;
                    WORKFLOWID = GetParamsPage(IPC.WORKFLOWID)[0].Trim();
                    LoadWorkflowData();
                    break;
            }
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnProcessApprove.Visible = false;
                    pnAdd.Enabled = false;
                    btback.Visible = true;
                    btsave.Visible = false;
                    IsDelete = false;
                    LoadDataApproveProcess();
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    pnProcessApprove.Visible = true;
                    btback.Visible = true;
                    btsave.Visible = true;
                    IsDelete = true;
                    LoadDataApproveProcess();
                    break;
            }
            #endregion

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            if (!IBCheckPermitPageAction(ACTION)) return;

            if (string.IsNullOrEmpty(WORKFLOWID)) return;

            DataTable dtDetail = new DataTable();
            if (!cbNeedApprove.Checked)
            {
                dtDetail.Columns.AddRange(new DataColumn[] { new DataColumn("WorkflowID"), new DataColumn("Ord"), new DataColumn("Formula"), new DataColumn("Desc") });
                dtDetail.Rows.Add(WORKFLOWID, 0, string.Empty, string.Empty);
            }
            else
            {
                dtDetail = (DataTable)ViewState["USERAPPTRANS"];
            }

            if (dtDetail == null || dtDetail.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.banvuilongthietlapquitrinhduyetgiaodich;
                return;
            }

            DataTable dtWorkflow = new DataTable();
            dtWorkflow.Columns.AddRange(new DataColumn[] { new DataColumn("WorkflowID"), new DataColumn("ContractNo"), new DataColumn("AcctNo"), new DataColumn("CCYID"), new DataColumn("TranCode"), new DataColumn("FromLimit"), new DataColumn("ToLimit"), new DataColumn("IsAOT"), new DataColumn("NeedApprove"), new DataColumn("DateCreated"), new DataColumn("UserCreated"), new DataColumn("SOURCEID") });
            dtWorkflow.Rows.Add(
                WORKFLOWID,
                CONTRACT_NO,
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlAccNumber.SelectedValue.Trim()),
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim()),
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim()),
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtFrom.Text.Trim(), ddlCCYID.SelectedValue.Trim())),
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTo.Text.Trim(), ddlCCYID.SelectedValue.Trim())),
                cbIsAOT.Checked ? 1 : 0,
                cbNeedApprove.Checked ? 1 : 0,
                DateTime.Now,
                Session["userID"].ToString(),
                IPC.SOURCEIBVALUE
            );

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    new SmartPortal.SEMS.ApprovalWorkflow().WorkflowInsertAll(dtWorkflow, dtDetail, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.addapprovalprocessthanhcong;
                        pnAdd.Enabled = false;
                        pnProcessApprove.Enabled = false;
                        btsave.Visible = false;
                        IsDelete = false;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    DataTable dtDel = new DataTable();
                    dtDel.Columns.AddRange(new DataColumn[] { new DataColumn("WorkflowID") });
                    dtDel.Rows.Add(WORKFLOWID);

                    new SmartPortal.SEMS.ApprovalWorkflow().WorkflowUpdateAll(dtDel, dtWorkflow, dtDetail, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.editapprovalprocessthanhcong;
                        pnAdd.Enabled = false;
                        pnProcessApprove.Enabled = false;
                        btsave.Visible = false;
                        IsDelete = false;
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtFormula.Text.Trim()))
            {
                lblError.Text = Resources.labels.pleaseinputapproveformula;
                return;
            }
            if (!IsValidFormula())
            {
                lblError.Text = Resources.labels.formulainvalid;
                return;
            }
            DataSet dsGroup = new SmartPortal.IB.CorpUser().SearchCorpUserlevel(string.Empty, CONTRACT_NO, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtGroup = dsGroup.Tables[0];
                if (dtGroup.Rows.Count == 0)
                {
                    lblError.Text = Resources.labels.youmustfirstsettheusergroups;
                    return;
                }
                else
                {
                    string formula = AnalysisFormula();
                    int countChar = 0;
                    foreach (char item in formula.ToCharArray())
                    {
                        if (dtGroup.AsEnumerable().FirstOrDefault(r => r[0].ToString().Equals(item.ToString())) != null)
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
            }
            else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
            }

            DataTable tblTransDetails = new DataTable();

            if (ViewState["USERAPPTRANS"] == null)
            {
                tblTransDetails.Columns.AddRange(new DataColumn[] { new DataColumn("WorkflowID"), new DataColumn("Ord", typeof(int)), new DataColumn("Formula"), new DataColumn("Desc") });
                tblTransDetails.Rows.Add(WORKFLOWID, 1, Utility.KillSqlInjection(txtFormula.Text.Trim().ToUpper()), Utility.KillSqlInjection(txtDesc.Text.Trim()));
                ViewState["USERAPPTRANS"] = tblTransDetails;
            }
            else
            {
                tblTransDetails = (DataTable)ViewState["USERAPPTRANS"];
                tblTransDetails.Rows.Add(WORKFLOWID, tblTransDetails.Rows.Count + 1, Utility.KillSqlInjection(txtFormula.Text.Trim().ToUpper()), Utility.KillSqlInjection(txtDesc.Text.Trim()));
                ViewState["USERAPPTRANS"] = tblTransDetails;
            }

            BindOrderApprovalList();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private string AnalysisFormula(string value = "")
    {
        Regex regex = new Regex("[A-Z]+");
        string formula = ConvertFormula(value);
        if (string.IsNullOrEmpty(formula)) return string.Empty;
        MatchCollection matchs = regex.Matches(formula);
        formula = string.Empty;
        foreach (Match str in matchs)
        {
            formula += str.Value;
        }
        return formula;
    }
    private string ConvertFormula(string value = "")
    {
        string formula = string.IsNullOrEmpty(value) ? Utility.KillSqlInjection(txtFormula.Text.Trim()) : value;
        if (!string.IsNullOrEmpty(formula))
        {
            formula = formula.ToUpper();
            formula = formula.Replace("OR", "||").Replace("+", "&").Replace(" ", "");
            formula = Regex.Replace(formula, "[a-zA-Z0-9]+", "${0}");
            formula = Regex.Replace(formula, "[0-9]+", "${0}*");
        }
        return formula;
    }
    private bool IsValidFormula()
    {
        try
        {
            if (string.IsNullOrEmpty(txtFormula.Text.Trim())) return false;

            string formula = txtFormula.Text.Trim();
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
        }
        return false;
    }
    protected void cbNeedApprove_CheckedChanged(object sender, EventArgs e)
    {
        pnGroup.Visible = cbNeedApprove.Checked;
        pnProcessApprove.Visible = cbNeedApprove.Checked;
        pnApproval.Visible = cbNeedApprove.Checked;
        ViewState["USERAPPTRANS"] = null;
        BindOrderApprovalList();
    }
    private void GetListAccTran()
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
    }
    protected void gvAppTransDetailsList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string commandName = e.CommandName;
            string commandArg = e.CommandArgument.ToString();
            DataTable tblTemp = (DataTable)ViewState["USERAPPTRANS"];
            DataRow dataRow = tblTemp.Select("Ord = '" + commandArg.ToString() + "'").FirstOrDefault();
            int idx = tblTemp.Rows.IndexOf(dataRow);
            DataRow newRow = tblTemp.NewRow();
            newRow.ItemArray = dataRow.ItemArray.Clone() as object[];
            switch (commandName)
            {
                case "Delete":
                    tblTemp.Rows.Remove(dataRow);
                    ViewState["USERAPPTRANS"] = tblTemp;
                    BindOrderApprovalList();
                    break;
                case "Up":
                    tblTemp.Rows.Remove(dataRow);
                    tblTemp.Rows.InsertAt(newRow, idx - 1);
                    ViewState["USERAPPTRANS"] = tblTemp;
                    BindOrderApprovalList();
                    break;
                case "Down":
                    tblTemp.Rows.Remove(dataRow);
                    tblTemp.Rows.InsertAt(newRow, idx + 1);
                    ViewState["USERAPPTRANS"] = tblTemp;
                    BindOrderApprovalList();
                    break;
                default:
                    return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void BindOrderApprovalList()
    {
        if (ViewState["USERAPPTRANS"] != null)
        {
            DataTable dt = (DataTable)ViewState["USERAPPTRANS"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                row["Ord"] = i + 1;
            }
            dt.DefaultView.Sort = "[" + dt.Columns["Ord"].ColumnName + "] asc";
            ViewState["USERAPPTRANS"] = dt;
            gvAppTransDetailsList.DataSource = dt;
            gvAppTransDetailsList.DataBind();
        }
        else
        {
            gvAppTransDetailsList.DataSource = null;
            gvAppTransDetailsList.DataBind();
        }
    }
    private void LoadWorkflowData()
    {
        try
        {
            if (string.IsNullOrEmpty(WORKFLOWID)) return;
            ApprovalWorkflowModel workflow = new ApprovalWorkflowModel();
            workflow.WORKFLOWID = WORKFLOWID;
            DataSet dsResult = new SmartPortal.SEMS.ApprovalWorkflow().ApprovalWorkflowDetail(workflow, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0") && !DataSetIsEmpty(dsResult))
            {
                DataTable dt = dsResult.Tables[0];
                DataRow row = dt.Rows[0];
                ddlTrans.SelectedValue = row["TranCode"].ToString();
                ddlAccNumber.SelectedValue = row["AcctNo"].ToString();
                txtFrom.Text = Utility.FormatMoney(row["FromLimit"].ToString(), row["CCYID"].ToString());
                txtTo.Text = Utility.FormatMoney(row["ToLimit"].ToString(), row["CCYID"].ToString());
                ddlCCYID.SelectedValue = row["CCYID"].ToString();
                ddlTrans.Enabled = ddlAccNumber.Enabled = ddlCCYID.Enabled = false;
                CONTRACT_NO = row["ContractNo"].ToString();
                cbIsAOT.Checked = row["IsAOT"].ToString().Equals("1") ? true : false;
                cbNeedApprove.Checked = row["NeedApprove"].ToString().Equals("1") ? true : false;
                cbNeedApprove_CheckedChanged(null, EventArgs.Empty);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadDataApproveProcess()
    {
        try
        {
            if (string.IsNullOrEmpty(WORKFLOWID)) return;
            AWDetailModel detail = new AWDetailModel();
            detail.WORKFLOWID = WORKFLOWID;
            DataSet dsResult = new SmartPortal.SEMS.ApprovalWorkflow().AWDetailGetAll(detail,Session["UserID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE.Equals("0") && !DataSetIsEmpty(dsResult))
            {
                DataTable dt = dsResult.Tables[0];

                DataTable tblTransDetails = new DataTable();
                tblTransDetails.Columns.AddRange(new DataColumn[] { new DataColumn("WorkflowID"), new DataColumn("Ord", typeof(int)), new DataColumn("Formula"), new DataColumn("Desc") });
                foreach (DataRow ro in dt.Rows)
                {
                    tblTransDetails.Rows.Add(WORKFLOWID, int.Parse(ro["Ord"].ToString()), ro["Formula"].ToString(), ro["Desc"].ToString());
                }
                ViewState["USERAPPTRANS"] = tblTransDetails;
                BindOrderApprovalList();
            }
            else
            {
                SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    bool DataSetIsEmpty(DataSet dataSet)
    {
        foreach (DataTable table in dataSet.Tables)
            if (table.Rows.Count != 0) return false;

        return true;
    }
    protected bool VisibleUp(int containerItemIndex)
    {
        if (containerItemIndex == 0) return false;
        return true;
    }
    protected bool VisibleDown(int containerItemIndex)
    {
        if (ViewState["USERAPPTRANS"] == null)
        {
            return false;
        }
        DataTable dtDetail = (DataTable)ViewState["USERAPPTRANS"];
        if (containerItemIndex == dtDetail.Rows.Count - 1) return false;
        return true;
    }
    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}
