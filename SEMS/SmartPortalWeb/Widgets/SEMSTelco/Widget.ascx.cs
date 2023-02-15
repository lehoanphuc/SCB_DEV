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
using SmartPortal.BLL;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_SEMSTelco_Widget : WidgetBase
{

    public static bool isAscend = false;
    private int size = 0;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string PageEdit = "1109";
    string PageDelete = "1097";
    string PageCard = "1111";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        if (!IsPostBack)
        {
            btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
            btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
            GridViewPaging.Visible = false;
            divResult.Visible = false;
            //BindData();
        }
        GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvTelco.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvTelco.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvTelco.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvTelco.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void BindData()
    {
        try
        {
            divResult.Visible = true;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvTelco.PageIndex * gvTelco.PageSize) return;
            DataSet ds = new DataSet();

            ds = new SmartPortal.SEMS.User().GetalltelcobyCondition(Utility.KillSqlInjection(txttelconame.Text.Trim()),
                Utility.KillSqlInjection(txtShortName.Text.Trim()), Utility.KillSqlInjection(txtEloadBillerCode.Text.Trim()),
                 Utility.KillSqlInjection(txtEPinBillerCode.Text.Trim()), txtstatus.SelectedValue, Utility.KillSqlInjection(lblGLAccBalance.Text.Trim()), Utility.KillSqlInjection(lblGLAccFee.Text.Trim()), Utility.KillSqlInjection(lblWlBalance.Text.Trim()), Utility.KillSqlInjection(lblWlFee.Text.Trim()), gvTelco.PageSize, gvTelco.PageIndex * gvTelco.PageSize, ref IPCERRORCODE, ref IPCERRORDESC
                );
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            gvTelco.DataSource = ds;
            gvTelco.DataBind();
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
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSTelco_Widget", "BindData", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSTelco_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvTelco_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTelco.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvTelco_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpEdit;
            HyperLink hpCardManagement, hpPrefixManagement;
            Label lblcardid, lblStatus, lblEloadBillCode, lblShortName, lblEPinBillCode, lblAcountBalance, lblWalletBalance;

            LinkButton lbEdit, lblTelecom;
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
                // click select
                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                lblTelecom = (LinkButton)e.Row.FindControl("lblTelecom");
                lblShortName = (Label)e.Row.FindControl("lblShortName");
                lblEloadBillCode = (Label)e.Row.FindControl("lblEloadBillCode");
                lblEPinBillCode = (Label)e.Row.FindControl("lblEPinBillCode");
                lblAcountBalance = (Label)e.Row.FindControl("lblAcountBalance");
                lblWalletBalance = (Label)e.Row.FindControl("lblWalletBalance");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblcardid = (Label)e.Row.FindControl("lblcardid");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                hpCardManagement = (HyperLink)e.Row.FindControl("hpCardManagement");
                hpPrefixManagement = (HyperLink)e.Row.FindControl("hpPrefixManagement");
                lblAcountBalance.Text = drv["SUNDRYACCTNOBANK"].ToString();
                lblWalletBalance.Text = drv["SUNDRYACCTNOWALLET"].ToString();
                lblcardid.Text = drv["TelcoID"].ToString();
                lblTelecom.Text = drv["TelcoName"].ToString();
                lblShortName.Text = drv["ShortName"].ToString();
                lblEloadBillCode.Text = drv["ELoadBillerCode"].ToString();
                lblEPinBillCode.Text = drv["EPinBillerCode"].ToString();
                //lblStatus.Text = drv[IPC.STATUS].ToString();
                switch (drv[IPC.STATUS].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = Resources.labels.connew;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = Resources.labels.condelete;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.conactive;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblStatus.Text = Resources.labels.conpending;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblStatus.Text = Resources.labels.conblock;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                }
          

                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
                }
                //check detail card and prefix folower telco
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.DETAILS))
                {
                    lblTelecom.Enabled = false;
                    lblTelecom.OnClientClick = string.Empty;
                    hpCardManagement.Enabled = false;
                    hpCardManagement.NavigateUrl = string.Empty;
                    hpPrefixManagement.Enabled = false;
                    hpPrefixManagement.NavigateUrl = string.Empty;
                }
                else
                {
                    hpPrefixManagement.NavigateUrl = "~/" + SmartPortal.Common.Encrypt.EncryptURL(PagesBLL.GetLinkAction_Page("1111", IPC.ACTIONPAGE.LIST) + "&cid=" + drv["TelcoID"].ToString());
                    hpCardManagement.NavigateUrl = "~/" + SmartPortal.Common.Encrypt.EncryptURL(PagesBLL.GetLinkAction_Page("CARD", IPC.ACTIONPAGE.LIST) + "&cid=" + drv["TelcoID"].ToString());
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSTelco_Widget", "gvTelco_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData2();
        }
        catch (Exception ex)
        {
            throw ex;
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
        try
        {

            CheckBox cb;
            Label lblcardid;
            DataTable dt = new DataTable();
            List<string> listid = new List<string> { };

            dt.Columns.Add(IPC.CARDID);
            foreach (GridViewRow gvr in gvTelco.Rows)
            {
                cb = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cb.Checked)
                {
                    DataRow dr = dt.NewRow();
                    lblcardid = (Label)gvr.Cells[1].FindControl("lblcardid");
                    dr[IPC.CARDID] = lblcardid.Text;
                    listid.Add(lblcardid.Text);
                    dt.Rows.Add(dr);
                }
            }

            if (dt.Rows.Count > 0 && listid.Count > 0)
            {
                //xoa lan luot tung telco
                foreach (var item in listid)
                {
                    new SmartPortal.SEMS.User().DeleteTelcoById(item, ref IPCERRORCODE, ref IPCERRORDESC);
                }
                if (IsSuccess())
                {
                    ShowError(Resources.labels.deletegroupsuccessfully);
                    BindData2();
                }
                else
                {
                    ShowError(Resources.labels.deletegroupfail);
                    BindData2();
                }
                return;
            }
            ShowError(Resources.labels.chooserecordtodelete);

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private bool IsSuccess()
    {
        return IPCERRORCODE.Equals("0") ? true : false;
    }

    private void ShowError(string msg = "")
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowError", "ShowError('" + msg + "');", true);
    }

    protected void gvTelco_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + "cid" + "=" + commandArg);
                    break;
            }
        }
    }

    protected void gvTelcoList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvTelco.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvTelcoList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvTelco.Rows[e.RowIndex].Cells[1].FindControl("lbltecoID")).CommandArgument;
            new SmartPortal.SEMS.Product().DeleteProduct(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData();
                lblError.Text = Resources.labels.xoasanphamthanhcong;
            }
            else
            {
                string errorCode = string.Empty;
                ErrorCodeModel EM = new ErrorCodeModel();
                if (IPCERRORDESC == "110211")
                {
                    errorCode = IPC.ERRORCODE.USINGPRODUCT;
                }
                else
                {
                    errorCode = IPC.ERRORCODE.IPC;
                }
                EM = new ErrorBLL().Load(Utility.IsInt(errorCode), System.Globalization.CultureInfo.CurrentCulture.ToString());
                lblError.Text = EM.ErrorDesc;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

}