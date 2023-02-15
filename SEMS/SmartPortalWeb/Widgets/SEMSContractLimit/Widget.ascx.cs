using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSContractLimit_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                lblError.Text = "";
                ltrError.Text = "";
                txtlimit.Attributes.Add("onkeyup", "executeComma('" + txtlimit.ClientID + "',event)");
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);

                ddltran.DataSource = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                ddltran.DataTextField = "PAGENAME";
                ddltran.DataValueField = "TRANCODE";
                ddltran.DataBind();
                ddltran.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));

                ddlccyid.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlccyid.DataTextField = "CCYID";
                ddlccyid.DataValueField = "CCYID";
                ddlccyid.DataBind();
                ddlccyid.SelectedValue = "LAK";

                #region add limit type
                ddlLimitType.Items.Add(new ListItem(Resources.labels.binhthuong, "NOR"));
                #endregion
                GridViewPaging.Visible = false;
                divResult.Visible = false;
                //BindData();
            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvContractLimit.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvContractLimit.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            DataSet dtProLim = new DataSet();
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvContractLimit.PageIndex * gvContractLimit.PageSize) return;
            dtProLim = new SmartPortal.SEMS.Transactions().GetContractLimitByCondition(Utility.KillSqlInjection(txtContractno.Text.Trim()), Utility.KillSqlInjection(ddltran.SelectedValue), Utility.KillSqlInjection(ddlccyid.SelectedValue), txtlimit.Text.Replace(",", ""),"O", "", "", txtCustName.Text.Trim(), ddlLimitType.SelectedValue, gvContractLimit.PageSize, gvContractLimit.PageIndex * gvContractLimit.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvContractLimit.DataSource = dtProLim;
                gvContractLimit.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            if (dtProLim.Tables[0].Rows.Count > 0)
            {
                divResult.Visible = true;
                ltrError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dtProLim.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                ltrError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvContractLimit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton lblProductCode;
            Label lblProductName, lblfullname;
            Label lblTrans;
            Label lblTranCode;
            Label lblCCYID, lblLimitType;
            Label lbllimit, lblstatus, lblstatusID, lblusercreated, lbldatecreated, lbluserapproved, lbldateapproved;
            LinkButton hpEdit;
            LinkButton hpDelete;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                TableRow tbRow = (TableRow)(e.Row.Cells[0].Controls[0].Controls[0]);
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + " :</strong>";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                lblProductCode = (LinkButton)e.Row.FindControl("lblProductCode");
                lblProductName = (Label)e.Row.FindControl("lblProductName");
                lblfullname = (Label)e.Row.FindControl("lblfullname");
                lblTrans = (Label)e.Row.FindControl("lblTrans");
                lblTranCode = (Label)e.Row.FindControl("lblTranCode");
                lblCCYID = (Label)e.Row.FindControl("lblccyid");
                lbllimit = (Label)e.Row.FindControl("lbllimit");
                lblstatus = (Label)e.Row.FindControl("lblstatus");
                lblstatusID = (Label)e.Row.FindControl("lblstatusID");
                lblLimitType = (Label)e.Row.FindControl("lblLimitType");
                hpEdit = (LinkButton)e.Row.FindControl("hpEdit");
                hpDelete = (LinkButton)e.Row.FindControl("hpDelete");

                lblusercreated = (Label)e.Row.FindControl("lblusercreated");
                lbldatecreated = (Label)e.Row.FindControl("lbldatecreated");
                lbluserapproved = (Label)e.Row.FindControl("lbluserapproved");
                lbldateapproved = (Label)e.Row.FindControl("lbldateapproved");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                lblProductCode.Text = drv["ContractNo"].ToString();
                lblfullname.Text = drv["FullName"].ToString();
                //lblProductCode.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=366&a=viewdetail&ctrn=" + drv["ContractNo"].ToString() + "&trcod=" + drv["TranCode"].ToString() + "&cyid=" + drv["CCYID"].ToString() + "&stt=" + drv["STATUS"].ToString() + "&lmt=" + drv["LIMITTYPE"].ToString().Trim());
                // lblProductName.Text = drv["ProductName"].ToString();
                lblTrans.Text = drv["PageName"].ToString();
                lblTranCode.Text = drv["TranCode"].ToString();
                lblCCYID.Text = drv["CCYID"].ToString();
                lbllimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TRANLIMITOK"].ToString(), drv["CCYID"].ToString().Trim());
                lblstatusID.Text = drv["STATUS"].ToString().Trim();

                lblusercreated.Text = drv["USERCREATED"].ToString();
                lbldatecreated.Text = ((DateTime)drv["DATECREATED"]).ToString("dd/MM/yyyy");
                lbluserapproved.Text = drv["USERAPPROVED"].ToString();

                switch (drv["LIMITTYPE"].ToString().Trim())
                {
                    case "DEB":
                        lblLimitType.Text = ddlLimitType.Items.FindByValue("DEB").Text;
                        break;
                    case "BAT":
                        lblLimitType.Text = ddlLimitType.Items.FindByValue("BAT").Text;
                        break;
                    default:
                        lblLimitType.Text = ddlLimitType.Items.FindByValue("NOR").Text;
                        break;
                }

                if (drv["DATEAPPROVED"].ToString() != "")
                {
                    lbldateapproved.Text = ((DateTime)drv["DATEAPPROVED"]).ToString("dd/MM/yyyy");
                }
                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblstatus.Text = Resources.labels.connew;
                        lblstatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblstatus.Text = Resources.labels.condelete;
                        lblstatus.Attributes.Add("class", "label-warning");
                        hpEdit.Enabled = false;
                        hpEdit.OnClientClick = string.Empty;
                        hpDelete.Enabled = false;
                        hpDelete.OnClientClick = string.Empty;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblstatus.Text = Resources.labels.conactive;
                        lblstatus.Attributes.Add("class", "label-success");
                        break;
                    //case SmartPortal.Constant.IPC.BLOCK:
                    //    lblStatus.Text = Resources.labels.conblock;
                    //    break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblstatus.Text = Resources.labels.conpending;
                        lblstatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblstatus.Text = Resources.labels.conreject;
                        lblstatus.Attributes.Add("class", "label-warning");
                        hpEdit.Enabled = false;
                        hpEdit.OnClientClick = string.Empty;
                        hpDelete.Enabled = false;
                        hpDelete.OnClientClick = string.Empty;
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        lblstatus.Text = Resources.labels.pendingfordelete;
                        lblstatus.Attributes.Add("class", "label-warning");
                        break;
                }

                if (drv["usercreated"].ToString().Trim() == Session["username"].ToString().Trim())
                {
                    hpEdit.Text = Resources.labels.edit;

                    //hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=368&a=edit&ctrn=" + drv["ContractNo"].ToString() + "&trcod=" + drv["TranCode"].ToString() + "&cyid=" + drv["CCYID"].ToString() + "&stt=" + drv["STATUS"].ToString() + "&lmt=" + drv["LIMITTYPE"].ToString().Trim());
                    hpDelete.Text = Resources.labels.delete;
                    //hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=369&ctrn=" + drv["ContractNo"].ToString() + "&trcod=" + drv["TranCode"].ToString() + "&cyid=" + drv["CCYID"].ToString() + "&stt=" + drv["STATUS"].ToString() + "&lmt=" + drv["LIMITTYPE"].ToString().Trim());


                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                    {
                        hpEdit.Enabled = false;
                        hpEdit.OnClientClick = string.Empty;
                    }
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                    {
                        hpDelete.Enabled = false;
                        hpDelete.OnClientClick = string.Empty;
                    }


                    switch (drv["STATUS"].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.NEW:
                            lblstatus.Text = Resources.labels.connew;
                            break;
                        case SmartPortal.Constant.IPC.DELETE:
                            lblstatus.Text = Resources.labels.condelete;
                            hpDelete.OnClientClick = string.Empty;
                            hpEdit.OnClientClick = string.Empty;
                            cbxSelect.Enabled = false;
                            break;
                        case SmartPortal.Constant.IPC.ACTIVE:
                            lblstatus.Text = Resources.labels.conactive;
                            break;
                        //case SmartPortal.Constant.IPC.BLOCK:
                        //    lblStatus.Text = Resources.labels.conblock;
                        //    break;
                        case SmartPortal.Constant.IPC.PENDING:
                            lblstatus.Text = Resources.labels.conpending;
                            break;
                        case SmartPortal.Constant.IPC.REJECT:
                            lblstatus.Text = Resources.labels.conreject;
                            hpEdit.Text = Resources.labels.edit;
                            hpEdit.OnClientClick = string.Empty;

                            hpDelete.Text = Resources.labels.delete;
                            hpDelete.OnClientClick = string.Empty;
                            cbxSelect.Enabled = false;
                            break;
                        case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                            lblstatus.Text = Resources.labels.pendingfordelete;
                            hpEdit.Text = Resources.labels.edit;
                            hpEdit.OnClientClick = string.Empty;

                            hpDelete.Text = Resources.labels.delete;
                            hpDelete.OnClientClick = string.Empty;
                            cbxSelect.Enabled = false;
                            break;
                    }
                }
                else
                {
                    hpEdit.Text = Resources.labels.edit;
                    hpEdit.OnClientClick = string.Empty;
                    hpEdit.Enabled = false;

                    hpDelete.Text = Resources.labels.delete;
                    hpDelete.OnClientClick = string.Empty;
                    cbxSelect.Enabled = false;
                    hpDelete.Enabled = false;
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
        }
    }
    protected void gvContractLimit_RowCommand(object sender, GridViewCommandEventArgs e)
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
                case IPC.ACTIONPAGE.DELETE:
                    RedirectToActionPage(IPC.ACTIONPAGE.DELETE, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }

    protected void gvContractLimit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvContractLimit.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvContractLimit_Sorting(object sender, GridViewSortEventArgs e)
    {
        //isSort = true;

        string sortExpression = e.SortExpression;

        ViewState["SortExpression"] = sortExpression;
        //showImage = true;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            //isAscend = true;

            SortGridView(sortExpression, ASCENDING);
            GridViewSortDirection = SortDirection.Descending;

        }

        else
        {
            isAscend = false;
            SortGridView(sortExpression, DESCENDING);
            GridViewSortDirection = SortDirection.Ascending;

        }
    }

    private SortDirection GridViewSortDirection
    {
        get
        {

            if (ViewState["sortDirection"] == null)

                ViewState["sortDirection"] = SortDirection.Ascending;


            return (SortDirection)ViewState["sortDirection"];

        }

        set { ViewState["sortDirection"] = value; }

    }

    protected void SortGridView(string sortExpression, string direction)
    {
        DataTable dataTable;

        if (ViewState["s"] != null)
        {
            dataTable = null;
        }

        else
        {
            if (Session["search"] != null)
            {
                dataTable = (new SmartPortal.SEMS.Transactions().GetContractLimitByCondition(Utility.KillSqlInjection(txtContractno.Text.Trim()), Utility.KillSqlInjection(ddltran.SelectedValue), Utility.KillSqlInjection(ddlccyid.SelectedValue), txtlimit.Text.Trim(), "O","", "", txtCustName.Text, ddlLimitType.SelectedValue, gvContractLimit.PageSize, gvContractLimit.PageIndex * gvContractLimit.PageSize, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            }
            else
            {
                dataTable = (new SmartPortal.SEMS.Transactions().GetContractLimitByCondition(Utility.KillSqlInjection(txtContractno.Text.Trim()), Utility.KillSqlInjection(ddltran.SelectedValue), Utility.KillSqlInjection(ddlccyid.SelectedValue), txtlimit.Text.Trim(), "O","" ,"", txtCustName.Text, ddlLimitType.SelectedValue, gvContractLimit.PageSize, gvContractLimit.PageIndex * gvContractLimit.PageSize, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            }
        }


        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvContractLimit.DataSource = dataView;
            gvContractLimit.DataBind();
        }

    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=367&a=add"));
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        LinkButton hpProID;
        Label lbTrancode;
        Label lbCCYID, lbStatus, lblLimitType;

        string Str_ProTranCCYID = "";
        try
        {
            foreach (GridViewRow gvr in gvContractLimit.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpProID = (LinkButton)gvr.Cells[1].FindControl("lblProductCode");
                    Str_ProTranCCYID += hpProID.Text.Trim() + "|";
                    lbTrancode = (Label)gvr.Cells[1].FindControl("lblTranCode");
                    Str_ProTranCCYID += lbTrancode.Text.Trim() + "|";
                    lbCCYID = (Label)gvr.Cells[1].FindControl("lblccyid");
                    Str_ProTranCCYID += lbCCYID.Text.Trim() + "|";
                    lbStatus = (Label)gvr.Cells[1].FindControl("lblstatusID");
                    Str_ProTranCCYID += lbStatus.Text.Trim() + "|";
                    lblLimitType = (Label)gvr.Cells[1].FindControl("lblLimitType");
                    Str_ProTranCCYID += ddlLimitType.Items.FindByText(lblLimitType.Text).Value + "#";
                }
            }
            goto EXIT;


        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    EXIT:
        if (Str_ProTranCCYID != "")
        {
            lblError.Text = "";
            Session["_CtractTranCCYIDStatus"] = Str_ProTranCCYID.Substring(0, Str_ProTranCCYID.Length - 1);
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=369"));
        }
        else
        {
            lblError.Text = Resources.labels.bancanchonhopdong;
            BindData();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            Session["search"] = "true";
            BindData();
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlLimitType_IndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);

        switch (ddlLimitType.SelectedValue)
        {
            case "DEB":
                #region DEB
                ddltran.DataSource = ds.Tables[0].Select("IsReceive='Y'").CopyToDataTable();
                ddltran.DataTextField = "PAGENAME";
                ddltran.DataValueField = "TRANCODE";
                ddltran.DataBind();
                ddltran.Enabled = true;
                ddltran.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));

                txtlimit.Enabled = true;
                ddlccyid.Enabled = true;
                #endregion
                break;
            case "BAT":
                #region BAT

                ddltran.DataSource = ds.Tables[0].Select("TranCode='IB000499'").CopyToDataTable();
                ddltran.DataTextField = "PAGENAME";
                ddltran.DataValueField = "TRANCODE";
                ddltran.DataBind();
                ddltran.Enabled = false;

                txtlimit.Enabled = true;
                ddlccyid.Enabled = false;
                #endregion
                break;
            default:
                ddltran.DataSource = ds.Tables[0];
                ddltran.DataTextField = "PAGENAME";
                ddltran.DataValueField = "TRANCODE";
                ddltran.DataBind();
                ddltran.Enabled = true;
                ddltran.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));

                txtlimit.Enabled = true;
                ddlccyid.Enabled = true;
                break;
        }
    }
}
