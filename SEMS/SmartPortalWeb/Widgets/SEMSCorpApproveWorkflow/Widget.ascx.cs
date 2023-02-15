using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Constant;
using SmartPortal.Model;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;

public partial class Widgets_SEMSCorpApproveWorkflow_Widget : WidgetBase
{
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                LoadDll();
                GridViewPaging.Visible = false;
                divResult.Visible = false;
            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvApprovalWorkflow.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvApprovalWorkflow.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void LoadDll()
    {
        try
        {
            DataSet dsResult = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlSearchTransaction.DataSource = dsResult;
            ddlSearchTransaction.DataTextField = "PAGENAME";
            ddlSearchTransaction.DataValueField = "TRANCODE";
            ddlSearchTransaction.DataBind();
            ddlSearchTransaction.Items.Insert(0, new ListItem(IPC.ALL, IPC.ALL));

            ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlCCYID.DataTextField = "CCYID";
            ddlCCYID.DataValueField = "CCYID";
            ddlCCYID.DataBind();
            ddlCCYID.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
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
            divResult.Visible = true;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvApprovalWorkflow.PageIndex * gvApprovalWorkflow.PageSize) return;
            DataSet ds = new SmartPortal.SEMS.ApprovalWorkflow().ApprovalWorkflowSearch(Utility.KillSqlInjection(ddlSearchTransaction.SelectedValue), Utility.KillSqlInjection(txtSearchContractNo.Text.Trim()), Utility.KillSqlInjection(txtSearchAccNumber.Text.Trim()), Utility.KillSqlInjection(ddlCCYID.SelectedValue), Utility.KillSqlInjection(ddlNeedApprove.SelectedValue), gvApprovalWorkflow.PageSize, gvApprovalWorkflow.PageIndex * gvApprovalWorkflow.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvApprovalWorkflow.DataSource = ds;
                gvApprovalWorkflow.DataBind();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }

    protected void gvApprovalWorkflow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton lbWorkflowID, lbEdit, lbDelete; 
            Label colTransaction, colContractNo, colAccNumber, colFrom, colTo, colCurrency;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                lbWorkflowID = (LinkButton)e.Row.FindControl("lbWorkflowID");
                colTransaction = (Label)e.Row.FindControl("colTransaction");
                colContractNo = (Label)e.Row.FindControl("colContractNo");
                colAccNumber = (Label)e.Row.FindControl("colAccNumber");
                colFrom = (Label)e.Row.FindControl("colFrom");
                colTo = (Label)e.Row.FindControl("colTo");
                colCurrency = (Label)e.Row.FindControl("colCurrency");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lbWorkflowID.Text = drv["WorkflowID"].ToString();
                colTransaction.Text = GetTranNameByTranCode(drv["TranCode"].ToString());
                colContractNo.Text = drv["ContractNo"].ToString();
                colAccNumber.Text = drv["AcctNo"].ToString();
                colFrom.Text = Utility.FormatMoney(drv["FromLimit"].ToString(), drv["CCYID"].ToString());
                if (Math.Abs(double.Parse(drv["ToLimit"].ToString()) - (-1)) <= 0)
                {
                    colTo.Text = Resources.labels.unlimit;
                }
                else
                {
                    colTo.Text = Utility.FormatMoney(drv["ToLimit"].ToString(), drv["CCYID"].ToString());
                }
                colCurrency.Text = drv["CCYID"].ToString();
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
                }
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                {
                    lbDelete.Enabled = false;
                    lbDelete.OnClientClick = string.Empty;
                }
                if (cbxSelect.Enabled)
                {
                    size++;
                }
                hdCounter.Value = "0";
                hdPageSize.Value = size.ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private string GetTranNameByTranCode(string trancode)
    {
        string tranname = string.Empty;
        foreach (ListItem item in ddlSearchTransaction.Items)
        {
            if (item.Value.Equals(trancode))
            {
                tranname = item.Text;
                break;
            }
        }
        return tranname;
    }
    protected void gvApprovalWorkflow_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.ADD:
                    RedirectToActionPage(IPC.ACTIONPAGE.ADD, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }

    protected void gvApprovalWorkflow_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvApprovalWorkflow.Rows[e.RowIndex].Cells[1].FindControl("lbWorkflowID")).CommandArgument;
            new SmartPortal.SEMS.ApprovalWorkflow().WorkflowDelete(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData2();
                lblError.Text = Resources.labels.workflowdeletesuccessfull;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            CheckBox cbxDelete;
            LinkButton lbWorkflowID;
            string strWorkflow = "";
            try
            {
                foreach (GridViewRow gvr in gvApprovalWorkflow.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lbWorkflowID = (LinkButton)gvr.Cells[1].FindControl("lbWorkflowID");
                        strWorkflow += lbWorkflowID.CommandArgument.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strWorkflow))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] Workflow = strWorkflow.Split('#');
                    for (int i = 0; i < Workflow.Length - 1; i++)
                    {
                        new SmartPortal.SEMS.ApprovalWorkflow().WorkflowDelete(Workflow[i], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            BindData2();
                            lblError.Text = Resources.labels.workflowdeletesuccessfull;
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
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
            }
        }
    }
    void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvApprovalWorkflow.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvApprovalWorkflow.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
