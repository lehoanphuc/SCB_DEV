using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSPrefix_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string Pagemain = "1108";
    string PageEdit = "1113";
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();

        lblError.Text = "";
        if (!IsPostBack)
        {
            btnAddNew.Visible = CheckActionPage(IPC.ACTIONPAGE.ADD);
            btnDelete.Visible = CheckActionPage(IPC.ACTIONPAGE.DELETE);
            DataSet dsa = new SmartPortal.SEMS.Topup().GetSupplierList(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlSupplierID.DataTextField = "SMSSUPPLIERID";
            ddlSupplierID.DataValueField = "SMSSUPPLIERID";
            ddlSupplierID.DataSource = dsa;
            ddlSupplierID.DataBind();
            ddlSupplierID.Items.Insert(0, new ListItem("ALL", "ALL"));
            GridViewPaging.Visible = false;



            BindData();
        }
        GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        try
        {
            gvPrefix.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            gvPrefix.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
            BindData();
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
            gvPrefix.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvPrefix.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
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
            litError.Text = string.Empty;
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.LIST:
                    DataSet dsa = new DataSet();
                    dsa = new SmartPortal.SEMS.User().GetAllPrefix(ddlSupplierID.SelectedValue,
                        Utility.KillSqlInjection(txtPrefix.Text.Trim()),
                        Utility.KillSqlInjection(txtCountryPrefix.Text.Trim()), Utility.KillSqlInjection(txtGroupID.Text.Trim()),
                        SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString(), gvPrefix.PageSize, gvPrefix.PageIndex * gvPrefix.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
                    ViewState["ULPrefix"] = dsa.Tables[0];
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    gvPrefix.DataSource = dsa;
                    gvPrefix.DataBind();
                    if (dsa.Tables[0].Rows.Count > 0)
                    {
                        litError.Text = string.Empty;
                        GridViewPaging.Visible = true;
                        ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dsa.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
                    }
                    else
                    {
                        litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                        GridViewPaging.Visible = false;
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    #region gvPrefix
    protected void gvPrefix_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblcountryprefix;
            Label lblgroupid;
            Label lblphoenlen;
            Label lbltelconame;
            //Label lblSupplierid;

            LinkButton hpDelete, lbEditpre, lblprefix;
            DataRowView drv;
            var ddlGroupid = new DropDownList();
            DataSet dsGroup = new SmartPortal.SEMS.Topup().GetAllGroupId(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlGroupid.DataTextField = "GROUPNAME";
            ddlGroupid.DataValueField = "GROUPID";
            ddlGroupid.DataSource = dsGroup;
            ddlGroupid.DataBind();
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
                lblprefix = (LinkButton)e.Row.FindControl("lblprefix");
                //lblSupplierid = (Label)e.Row.FindControl("lblSupplierid");
                lblcountryprefix = (Label)e.Row.FindControl("lblcountryprefix");
                lblgroupid = (Label)e.Row.FindControl("lblgroupid");
                lbltelconame = (Label)e.Row.FindControl("lbltelconame");
                lblphoenlen = (Label)e.Row.FindControl("lblphoenlen");
                lbEditpre = (LinkButton)e.Row.FindControl("lbEditpre");
                hpDelete = (LinkButton)e.Row.FindControl("hpDelete");
                //lblSupplierid.Text = drv["SUPPLIERID"].ToString();
                lblprefix.Text = drv["PREFIX"].ToString();
                lbltelconame.Text = drv["TelcoName"].ToString();
                lblcountryprefix.Text = drv["COUNTRYPREFIX"].ToString();
                lblgroupid.Text = ddlGroupid.Items.FindByValue(drv["GROUPID"].ToString()) == null ? ddlGroupid.Items[0].Text : ddlGroupid.Items.FindByValue(drv["GROUPID"].ToString()).ToString();
                lblphoenlen.Text = drv["PhoneLen"].ToString();
                if (!CheckActionPage(IPC.ACTIONPAGE.EDIT))
                {
                    lbEditpre.Enabled = false;
                    lbEditpre.OnClientClick = string.Empty;
                }
                if (!CheckActionPage(IPC.ACTIONPAGE.DELETE))
                {
                    hpDelete.Enabled = false;
                    hpDelete.OnClientClick = string.Empty;
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSPrefix_Widget", "gvPrefix_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData2();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (CheckActionPage(IPC.ACTIONPAGE.ADD))
        {
            var cid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"] != null ? SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString() : "";
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, "&b=" + cid);
        }

    }
    protected void delete_Click(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (CheckActionPage(IPC.ACTIONPAGE.DELETE))
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["ULPrefix"];
                string PrefixID = string.Empty;
                GridViewRow gvr = gvPrefix.Rows[e.RowIndex];
                PrefixID = ((LinkButton)gvr.Cells[1].FindControl("lblprefix")).CommandArgument;
                DataSet ds = new DataSet();
                ds = new SmartPortal.SEMS.User().DeleteCardPrefix(PrefixID, (int)SmartPortal.Constant.IPC.TOP_CARD_PREFIX_TYPE.PREFIX, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    BindData2();
                    lblError.Text = "Delete Prefix Successful";
                }
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
        try
        {
            List<string> listid = new List<string>();
            if (CheckActionPage(IPC.ACTIONPAGE.DELETE))
            {
                CheckBox cb;
                LinkButton lblprefix;
                DataTable dt = new DataTable();
                dt.Columns.Add(IPC.PREFIX);
                foreach (GridViewRow gvr in gvPrefix.Rows)
                {
                    cb = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cb.Checked)
                    {
                        DataRow dr = dt.NewRow();
                        lblprefix = (LinkButton)gvr.Cells[1].FindControl("lblprefix");
                        dr[IPC.PREFIX] = lblprefix.CommandArgument;
                        listid.Add(lblprefix.CommandArgument);
                        dt.Rows.Add(dr);
                    }
                }
                if (dt.Rows.Count > 0 && listid.Count > 0)
                {
                    // xoa lan luot prefix
                    foreach (var item in listid)
                    {
                        new SmartPortal.SEMS.User().DeleteCardPrefix(item, (int)SmartPortal.Constant.IPC.TOP_CARD_PREFIX_TYPE.PREFIX, ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    if (IsSuccess())
                    {
                        lblError.Text = "Delete Prefix Successful";
                        BindData2();
                    }
                    else
                    {
                        lblError.Text = "Delete Prefix Successful";
                        BindData2();
                    }
                    return;
                }
                ShowError(Resources.labels.chooserecordtodelete);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
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

    protected void gvPrefix_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        if (CheckActionPage(commandName))
        {
            var cid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"] != null ? SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString() : "";
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg + "&b=" + cid);
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg + "&b=" + cid);
                    break;
            }
        }

    }

    protected void Button8_Click1(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + Pagemain));
    }

    protected bool CheckActionPage(string action)
    {
        return PagesBLL.ChekcPermitPageAction(Pagemain, Session["userName"].ToString(), action);
    }

}