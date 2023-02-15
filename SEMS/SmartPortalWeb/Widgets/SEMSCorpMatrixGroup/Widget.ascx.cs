using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Constant;
using SmartPortal.Model;
using System.Web.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SmartPortal.BLL;
using SmartPortal.SEMS;

public partial class Widgets_SEMSCorpApproveStructure_Widget : WidgetBase
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
        gvApprovalStructure.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvApprovalStructure.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }

    void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvApprovalStructure.PageIndex * gvApprovalStructure.PageSize) return;
            DataSet ds = new SmartPortal.SEMS.ApprovalStructure().GetGroupByCondition(Utility.KillSqlInjection(txtAppliedContract.Text.Trim()), gvApprovalStructure.PageSize, gvApprovalStructure.PageIndex * gvApprovalStructure.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvApprovalStructure.DataSource = ds;
                gvApprovalStructure.DataBind();
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            CheckBox cbxDelete;
            LinkButton lbContractNo;
            string strContractNo = "";
            try
            {
                foreach (GridViewRow gvr in gvApprovalStructure.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lbContractNo = (LinkButton)gvr.Cells[1].FindControl("lbContractNo");
                        strContractNo += lbContractNo.CommandArgument.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strContractNo))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] contractNo = strContractNo.Split('#');
                    for (int i = 0; i < contractNo.Length - 1; i++)
                    {
                        new SmartPortal.SEMS.ApprovalStructure().DeleteGroupContract(contractNo[i], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            BindData2();
                            lblError.Text = Resources.labels.deletegroupsuccessfully;
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

    protected void gvApprovalStructure_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label colCustomerName, colLicenseID, colTotalGroup;
            LinkButton lbContractNo, lbEdit, lbDelete;

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
                lbContractNo = (LinkButton)e.Row.FindControl("lbContractNo");
                colCustomerName = (Label)e.Row.FindControl("colCustomerName");
                colLicenseID = (Label)e.Row.FindControl("colLicenseID");
                colTotalGroup = (Label)e.Row.FindControl("colTotalGroup");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lbContractNo.Text = drv[IPC.CONTRACTNO].ToString();
                colCustomerName.Text = drv[IPC.FULLNAME].ToString();
                colLicenseID.Text = drv[IPC.LICENSEID].ToString();
                colTotalGroup.Text = drv["TOTALGROUP"].ToString();

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

    protected void gvApprovalStructure_RowCommand(object sender, GridViewCommandEventArgs e)
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
            }
        }
    }

    protected void gvApprovalStructure_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvApprovalStructure.Rows[e.RowIndex].Cells[1].FindControl("lbContractNo")).CommandArgument;
            new SmartPortal.SEMS.ApprovalStructure().DeleteGroupContract(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData2();
                lblError.Text = Resources.labels.deletegroupsuccessfully;
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

    void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvApprovalStructure.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvApprovalStructure.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
