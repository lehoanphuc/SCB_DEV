using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_Card_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string Pagemain = "1108";
    string PageEdit = "1113";
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
            lblError.Text = "";
            lbltitle.Text = "Card Management";
            if (!IsPostBack)
            {
                btnAdd.Visible = CheckActionPage(IPC.ACTIONPAGE.ADD);
                btnDelete.Visible = CheckActionPage(IPC.ACTIONPAGE.DELETE);

                GridViewPaging.Visible = false;
                BindData();
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
        try
        {
            gvCardid.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            gvCardid.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
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
            gvCardid.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvCardid.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
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
            Literal2.Text = "";
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.LIST:
                case IPC.ACTIONPAGE.DETAILS:

                    DataSet ds = new DataSet();
                    DataTable custTable = new DataTable();
                    var cardamount = txtCardamountSearch.Text.Trim().Replace(",", "").Split('.')[0];
              
                    ds = new SmartPortal.SEMS.User().GetalltelcoDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString(), Utility.KillSqlInjection(txtShortNameSearch.Text.Trim()), Utility.KillSqlInjection(cardamount), ddlStatusSearch.SelectedValue, gvCardid.PageSize, gvCardid.PageIndex * gvCardid.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    else
                    {
                        custTable = ds.Tables[0];
                    }
                    gvCardid.DataSource = ds;
                    gvCardid.DataBind();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridViewPaging.Visible = true;
                        ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
                    }
                    else
                    {
                        Literal2.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                        GridViewPaging.Visible = false;
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSPrefix_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    #region
    protected void gvCardid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblcardid;
            Label lblshortname;
            Label lblcardamount;
            Label lblrealmoney;
            Label lblccyid;
            Label lblstatus;
            Label lbltype;
            LinkButton lbEditcard;
            LinkButton hpdelete;
            Label lblTelconame;

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

                lblcardid = (Label)e.Row.FindControl("lblcardid");
                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                lblTelconame = (Label)e.Row.FindControl("lblTelconame");
                lblshortname = (Label)e.Row.FindControl("lblshortname");
                lblcardamount = (Label)e.Row.FindControl("lblcardamount");
                lblrealmoney = (Label)e.Row.FindControl("lblrealmoney");
                lblccyid = (Label)e.Row.FindControl("lblccyid");
                lblstatus = (Label)e.Row.FindControl("lblstatus");
                lbltype = (Label)e.Row.FindControl("lbltype");
                lbEditcard = (LinkButton)e.Row.FindControl("lbEditcard");
                hpdelete = (LinkButton)e.Row.FindControl("hpDelete");
                lblcardid.Text = drv["CardID"].ToString();
                lblTelconame.Text = drv["TelcoName"].ToString();
                lblshortname.Text = drv["ShortName"].ToString();
                lblcardamount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["CardAmount"].ToString(), drv["CCYID"].ToString().Trim());
                lblrealmoney.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["RealAmount"].ToString(), drv["CCYID"].ToString().Trim());
                lblccyid.Text = drv["CCYID"].ToString();
                lblstatus.Text = drv["Status"].ToString();
                lbltype.Text = drv["Type"].ToString();
                if (!CheckActionPage(IPC.ACTIONPAGE.EDIT))
                {
                    lbEditcard.Enabled = false;
                    lbEditcard.OnClientClick = string.Empty;
                }
                if (!CheckActionPage(IPC.ACTIONPAGE.DELETE))
                {
                    hpdelete.Enabled = false;
                    hpdelete.OnClientClick = string.Empty;
                }

                switch (drv[IPC.STATUS].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblstatus.Text = Resources.labels.connew;
                        lblstatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblstatus.Text = Resources.labels.condelete;
                        lblstatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblstatus.Text = Resources.labels.conactive;
                        lblstatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblstatus.Text = Resources.labels.conpending;
                        lblstatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblstatus.Text = Resources.labels.conblock;
                        lblstatus.Attributes.Add("class", "label-warning");
                        break;
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
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=4&p=" + PageEdit + "&a=addnew" + "&pi=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString()));
    }

    protected void delete_Click(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (CheckActionPage(IPC.ACTIONPAGE.DELETE))
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["ULPrefix"];
                string cardId = string.Empty;
                GridViewRow gvr = gvCardid.Rows[e.RowIndex];
                cardId = ((LinkButton)gvr.Cells[1].FindControl("lbEditcard")).CommandArgument;
                DataSet ds = new DataSet();
                ds = new SmartPortal.SEMS.User().DeleteCardPrefix(cardId, (int)SmartPortal.Constant.IPC.TOP_CARD_PREFIX_TYPE.CARD, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    BindData2();
                    lblError.Text = "Delete Card Successful";
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
                LinkButton lblCard;
                DataTable dt = new DataTable();
                dt.Columns.Add(IPC.CARD);
                foreach (GridViewRow gvr in gvCardid.Rows)
                {
                    cb = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cb.Checked)
                    {
                        DataRow dr = dt.NewRow();
                        lblCard = (LinkButton)gvr.Cells[1].FindControl("lbEditcard");
                        dr[IPC.CARD] = lblCard.CommandArgument;
                        listid.Add(lblCard.CommandArgument);
                        dt.Rows.Add(dr);
                    }
                }
                if (dt.Rows.Count > 0 && listid.Count > 0)
                {
                    // xoa lan luot prefix
                    foreach (var item in listid)
                    {
                        new SmartPortal.SEMS.User().DeleteCardPrefix(item, (int)SmartPortal.Constant.IPC.TOP_CARD_PREFIX_TYPE.CARD, ref IPCERRORCODE, ref IPCERRORDESC);
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
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSTelco_Control_Widget", "gvCardid_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
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

    protected void btnSearchCard_Click(object sender, EventArgs e)
    {
        try
        {
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSTelco_Control_Widget", "gvCardid_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void Button8_Click1(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + Pagemain));
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (CheckActionPage(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, "&b=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString());
        }
    }
    protected void gvCard_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();

        if (CheckActionPage(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg + "&b=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString());
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg + "&b=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString());
                    break;
            }
        }
    }

    protected bool CheckActionPage(string action)
    {
        return PagesBLL.ChekcPermitPageAction(Pagemain, Session["userName"].ToString(), action);
    }
}