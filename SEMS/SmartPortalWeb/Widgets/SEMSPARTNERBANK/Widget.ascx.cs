using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;
using SmartPortal.SEMS;

public partial class Widgets_SEMSPartnerBank_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    int size = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAdd_New.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
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

    void BindData()
    {
        try
        {
            divResult.Visible = true;
            int pageSize = gvBankList.PageSize;
            int pageIndex = gvBankList.PageIndex;
            int recordIndex = pageSize * pageIndex;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField) GridViewPaging.FindControl("TotalRows")).Value) <
                recordIndex) return;
            DataSet dtBank = new DataSet();
            dtBank = new Partner().SearchBankByCondition(
                Utility.KillSqlInjection(txtbankcode.Text.Trim()), Utility.KillSqlInjection(txtbankname.Text.Trim()),
                pageSize, recordIndex,
                ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvBankList.DataSource = dtBank;
                gvBankList.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }

            if (dtBank.Tables[0].Rows.Count > 0)
            {
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField) GridViewPaging.FindControl("TotalRows")).Value =
                    dtBank.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
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

    protected void gvBankList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton lblBankCode;
            Label lblBankName;
            Label lblStatus;
            Label lblbankid;
            
            LinkButton lbEdit;
            LinkButton lbDelete;

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
                lblBankCode = (LinkButton) e.Row.FindControl("lblBankCode");
                lblBankName = (Label) e.Row.FindControl("lblBankName");
                lblStatus = (Label) e.Row.FindControl("lblStatus");
                lblbankid = (Label) e.Row.FindControl("lblbankid");


                lbEdit = (LinkButton) e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton) e.Row.FindControl("lbDelete");

                lblBankCode.Text = drv["BankCode"].ToString();
                lblBankName.Text = drv["BankName"].ToString();
                lblbankid.Text = drv["BankID"].ToString();
                switch (drv["Status"].ToString())
                {
                    case "I":
                        lblStatus.Text = Resources.labels.inactive;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case "A":
                        lblStatus.Text = Resources.labels.conactive;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    default:
                        lblStatus.Text = drv["Status"].ToString();
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                }
                    
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField) GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvBankList.PageSize =
                Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvBankList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
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
            Label lblBankCode;
            string strBankID = "";
            try
            {
                foreach (GridViewRow gvr in gvBankList.Rows)
                {
                    cbxDelete = (CheckBox) gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked)
                    {
                        lblBankCode = (Label) gvr.Cells[1].FindControl("lblbankid");
                        strBankID += lblBankCode.Text.Trim() + "#";
                    }
                }

                if (string.IsNullOrEmpty(strBankID))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] bankID = strBankID.Split('#');

                    for (int i = 0; i < bankID.Length - 1; i++)
                    {
                        new Partner().CheckExistsPartnerBankBranch(bankID[i], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORDESC.Equals("B"))
                        {
                            lblError.Text = Resources.labels.duplicatepartnerbank;
                            btnSearch_Click(sender, e);
                            return;
                        }
                    }

                    for (int i = 0; i < bankID.Length - 1; i++)
                    {
                        new Partner().DeletePartnerBank(bankID[i], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            lblError.Text = Resources.labels.deleteotherbankthanhcong;
                            btnSearch_Click(sender, e);
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            btnSearch_Click(sender, e);
                        }
                    }
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



    protected void gvBankList_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void gvBankList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((Label) gvBankList.Rows[e.RowIndex].Cells[1].FindControl("lblbankid")).Text;


            new Partner().DeletePartnerBank(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORDESC.Equals("B"))
            {
                lblError.Text = Resources.labels.duplicatepartnerbank;
                btnSearch_Click(sender, EventArgs.Empty);
                return;
            }
            new Partner().DeletePartnerBank(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.deleteotherbankthanhcong;
                btnSearch_Click(sender, EventArgs.Empty);
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                btnSearch_Click(sender, e);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    private void GridViewPagingClick(object sender, EventArgs e)
    {
        gvBankList.PageSize =
            Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvBankList.PageIndex = Convert.ToInt32(((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
}