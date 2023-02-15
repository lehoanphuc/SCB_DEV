using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using Resources;
using SmartPortal.BLL;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.Model;
using SmartPortal.SEMS;

public partial class Widgets_SEMSCurrency_Widget : WidgetBase
{
    private string IPCERRORCODE = "";
    private string IPCERRORDESC = "";
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                GridViewPaging.Visible = false;
                divResult.Visible = false;
            }

            GridViewPaging.pagingClickArgs += GridViewPagingClick;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            int pageSize = gvCurrencyList.PageSize;
            int pageIndex = gvCurrencyList.PageIndex;
            int recordIndex = pageSize * pageIndex;
            litError.Text = string.Empty;
            DataTable dtCurrency = new Currency().SearchCurrencyByCondition(
                Utility.KillSqlInjection(txtCCYID.Text.Trim()),
                Utility.KillSqlInjection(txtCurrencyNumber.Text.Trim()),
                Utility.KillSqlInjection(txtCurrencyName.Text.Trim()),
                Utility.KillSqlInjection(txtCurrencyMasterName.Text.Trim()), pageSize, recordIndex,
                ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (IPCERRORCODE == "0")
            {
                gvCurrencyList.DataSource = dtCurrency;
                gvCurrencyList.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }

            if (dtCurrency.Rows.Count > 0)
            {
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField) GridViewPaging.FindControl("TotalRows")).Value =
                    dtCurrency.Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnAddNew_OnClick(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        LinkButton lbCCYID;
        string strCCYID = "";
        try
        {
            foreach (GridViewRow gvr in gvCurrencyList.Rows)
            {
                cbxDelete = (CheckBox) gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lbCCYID = (LinkButton) gvr.Cells[1].FindControl("lbCCYID");
                    strCCYID += lbCCYID.CommandArgument.ToString().Trim() + "#";
                }
            }

            if (string.IsNullOrEmpty(strCCYID))
            {
                lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                return;
            }
            else
            {
                string[] ccyid = strCCYID.Split('#');
                for (int i = 0; i < ccyid.Length - 1; i++)
                {
                    new Currency().DeleteCurrency(ccyid[i], ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0"))
                    {
                        btnSearch_OnClick(this, EventArgs.Empty);
                        lblError.Text = Resources.labels.deletecurrencysuccessfully;
                    }
                    else
                    {
                        btnSearch_OnClick(this, EventArgs.Empty);
                        lblError.Text = Resources.labels.currencyinuse;
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvCurrencyList_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton lbCCYID, lbEdit, lbDelete;
            Label lblCurrencyName, lblCurrencyNumber, lblMasterName, lblSCurrencyId;
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
                drv = (DataRowView) e.Row.DataItem;
                cbxSelect = (CheckBox) e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                lbCCYID = (LinkButton) e.Row.FindControl("lbCCYID");
                lblCurrencyName = (Label) e.Row.FindControl("lblCurrencyName");
                lblSCurrencyId = (Label) e.Row.FindControl("lblSCurrencyId");
                lblCurrencyNumber = (Label) e.Row.FindControl("lblCurrencyNumber");
                lblMasterName = (Label) e.Row.FindControl("lblMasterName");
                lbEdit = (LinkButton) e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton) e.Row.FindControl("lbDelete");
                lbCCYID.Text = drv["CCYID"].ToString();
                lblSCurrencyId.Text = drv["SCurrencyID"].ToString();
                lblCurrencyNumber.Text = drv["CURRENCYNUMBER"].ToString();
                lblCurrencyName.Text = drv["CURRENCYNAME"].ToString();
                lblMasterName.Text = drv["MASTERNAME"].ToString();

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
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvCurrencyList_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArgument = e.CommandArgument.ToString();
        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + IPC.ID + "=" + commandArgument);
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + IPC.ID + "=" + commandArgument);
                    break;
            }
        }
    }
    protected void gvCurrencyList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton) gvCurrencyList.Rows[e.RowIndex].Cells[1].FindControl("lbCCYID")).CommandArgument;
            new Currency().DeleteCurrency(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                btnSearch_OnClick(this, EventArgs.Empty);
                lblError.Text = Resources.labels.deletecurrencysuccessfully;
            }
            else
            {
                btnSearch_OnClick(this, EventArgs.Empty);
                lblError.Text = Resources.labels.currencyinuse;
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvCurrencyList.PageSize =
                Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvCurrencyList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        txtCurrencyName.Text = string.Empty;
        txtCurrencyNumber.Text = string.Empty;
        txtCurrencyMasterName.Text = string.Empty;
        txtCCYID.Text = string.Empty;
        btnSearch_OnClick(sender, EventArgs.Empty);
    }
    private void GridViewPagingClick(object sender, EventArgs e)
    {
        gvCurrencyList.PageSize =
            Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvCurrencyList.PageIndex = Convert.ToInt32(((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
}