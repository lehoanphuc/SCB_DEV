using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using System.Text;
using SmartPortal.ExceptionCollection;
using System.Collections.Generic;
using SmartPortal.Constant;

public partial class Widgets_SEMSUserApprove_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private int size = 0;
    public string TYPEID
    {
        get { return ViewState["TYPEID"] != null ? (string)ViewState["TYPEID"] : string.Empty; }
        set { ViewState["TYPEID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            litError.Text = "";
            Session["userIDA"] = null;

            if (!IsPostBack)
            {
                //btnApprove.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE);
                //btnReject.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.REJECT);

                ddluserlevel.Items.Add(new ListItem(Resources.labels.tatca, "ALL"));
                ddluserlevel.Items.Add(new ListItem("0", "0"));
                ddluserlevel.Items.Add(new ListItem("1", "1"));
                ddluserlevel.Items.Add(new ListItem("2", "2"));
                ddluserlevel.Items.Add(new ListItem("3", "3"));
                #region load loai nguoi dung
                DataSet dsUserType = new DataSet();
                dsUserType = new SmartPortal.SEMS.Services().GetUserType("ALL", "ALL", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    //DataTable dtUserType = new DataTable();
                    //dtUserType = dsUserType.Tables[0];
					//
                    //ddlusertype.DataSource = dtUserType;
                    //ddlusertype.DataTextField = "USERTYPE";
                    //ddlusertype.DataValueField = "USERCODE";
                    //ddlusertype.DataBind();
					//
                    //ddlusertype.Items.Insert(0, new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                #endregion

                #region hien thị status
                ddlstatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
                ddlstatus.Items.Add(new ListItem(Resources.labels.conpending, SmartPortal.Constant.IPC.PENDING));
                ddlstatus.Items.Add(new ListItem(Resources.labels.pendingformnew, SmartPortal.Constant.IPC.PENDINGFORAPPROVE));
                ddlstatus.Items.Add(new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                #endregion

                GridViewPaging.Visible = false;
                //BindData();
                btnApprove.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE);
                btnReject.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.REJECT);

            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSUserApprove_Widget", "Page_Load", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSUserApprove_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvcUserList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvcUserList.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {

        string fullname = Utility.KillSqlInjection(txtfullname.Text.Trim());
        //string usertype = Utility.KillSqlInjection(ddlusertype.SelectedValue.ToString());
        //string birthday = Utility.KillSqlInjection(txtbirth.Text.Trim());
		string usertype="06";
        string email = Utility.KillSqlInjection(txtemail.Text.Trim());
        string phone = Utility.KillSqlInjection(txtphone.Text.Trim());
        string userlevel = Utility.KillSqlInjection(ddluserlevel.SelectedValue.ToString());
        string status = Utility.KillSqlInjection(ddlstatus.SelectedValue.ToString());
        DataSet dtUser = new DataSet();
        if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvcUserList.PageIndex * gvcUserList.PageSize) return;

        dtUser = new SmartPortal.SEMS.User().GetUserByCondition(fullname, usertype, "", email, phone, userlevel, status, SmartPortal.Constant.IPC.SEMSUSERINSERT, Session["userID"].ToString(), gvcUserList.PageSize, gvcUserList.PageIndex * gvcUserList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);


        if (IPCERRORCODE == "0")
        {
            if (dtUser.Tables[0].Rows.Count == 0 || ddlstatus.SelectedValue == SmartPortal.Constant.IPC.DELETE)
            {
                // pnbutton.Visible = false;

            }
            else if (ddlstatus.SelectedValue == SmartPortal.Constant.IPC.ACTIVE)
            {
                btnApprove.Visible = false;

            }
            else
            {
                btnApprove.Visible = true;
                //  pnbutton.Visible = true;
            }
            gvcUserList.DataSource = dtUser;
            gvcUserList.DataBind();

        }
        if (dtUser.Tables[0].Rows.Count > 0)
        {
            litError.Text = string.Empty;
            GridViewPaging.Visible = true;
            ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dtUser.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
        }
        else
        {
            litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
            GridViewPaging.Visible = false;
        }
    }
    protected void gvcUserList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton hpfullnname;
            LinkButton hpApprove;
            LinkButton hpReject;
            HyperLink lbluserID;
            Label lblcustName;
            Label lblbirth;
            Label lblusertype;
            Label lblemail;
            Label lbllevel;
            Label lblStatus;
            Label lblphone;
            Label lbltypeID, lblType;
            DataRowView drv;
            string contractType = string.Empty;
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
                lbltypeID = (Label)e.Row.FindControl("lbltypeID");
                lblType = (Label)e.Row.FindControl("lblType");
                lbluserID = (HyperLink)e.Row.FindControl("lbluserID");
                hpfullnname = (LinkButton)e.Row.FindControl("hpfullnname");
                hpApprove = (LinkButton)e.Row.FindControl("hpApprove");
                hpReject = (LinkButton)e.Row.FindControl("hpReject");
                lblcustName = (Label)e.Row.FindControl("lblcustName");
                lblbirth = (Label)e.Row.FindControl("lblbirth");
                lblusertype = (Label)e.Row.FindControl("lblusertype");
                lblemail = (Label)e.Row.FindControl("lblemail");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblphone = (Label)e.Row.FindControl("lblPhone");


                hpfullnname.Text = drv["FULLNAME"].ToString();
                lbluserID.Text = drv["USERID"].ToString();
                lblphone.Text = drv["PHONE"].ToString();
                lblcustName.Text = drv["FULLNAMECUST"].ToString();
                lblbirth.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["BIRTHDAY"].ToString()).ToString("dd/MM/yyyy");

                DataSet dsUserType = new DataSet();

                switch (drv["USERTYPE"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.CHECKER:
                    case SmartPortal.Constant.IPC.MAKER:
                    case SmartPortal.Constant.IPC.ADMIN:
                        dsUserType = new SmartPortal.SEMS.Services().GetUserType("06", "", ref IPCERRORCODE, ref IPCERRORDESC); break;
                    default:
                        dsUserType = new SmartPortal.SEMS.Services().GetUserType(drv["USERTYPE"].ToString().Trim(), "", ref IPCERRORCODE, ref IPCERRORDESC);
                        break;
                }
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    if (dtUserType.Rows.Count != 0)
                    {
                        lblusertype.Text = dtUserType.Rows[0]["USERTYPE"].ToString();
                    }
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }

                lblemail.Text = drv["EMAIL"].ToString();
                //lbllevel.Text = drv["USERLEVEL"].ToString();
                lblType.Text = drv["TYPEID"].ToString();
                switch (drv["TYPEID"].ToString())
                {
                    case SmartPortal.Constant.IPC.CHECKER:
                        lbltypeID.Text = "Checker";
                        break;
                    case SmartPortal.Constant.IPC.MAKER:
                        lbltypeID.Text = "Maker";
                        break;
                    case SmartPortal.Constant.IPC.ADMIN:
                        lbltypeID.Text = "Administrators";
                        break;
                    case "IN":
                        lbltypeID.Text = "Internal";
                        break;
                    case "CTK":
                        lbltypeID.Text = "Account holder";
                        break;
                    case "DCKT":
                        lbltypeID.Text = "Account Co-owner";
                        break;
                }

                switch (drv["STATUS"].ToString().Trim())
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
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblStatus.Text = Resources.labels.conblock;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblStatus.Text = Resources.labels.conpending;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblStatus.Text = Resources.labels.conreject;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORAPPROVE:
                        lblStatus.Text = Resources.labels.pendingformnew;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                }

                if (!CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE))
                {
                    hpApprove.Enabled = false;
                    hpApprove.OnClientClick = string.Empty;

                }
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.REJECT))
                {
                    hpReject.Enabled = false;
                    hpReject.OnClientClick = string.Empty;
                }
                hpReject.Text = Resources.labels.reject;
                hpApprove.Text = Resources.labels.approve;

                if (cbxSelect.Enabled)
                {
                    size++;
                }
                hdCounter.Value = "0";
                hdPageSize.Value = size.ToString();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSUserApprove_Widget", "gvcUserList_RowDataBound", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSUserApprove_Widget", "gvcUserList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvcUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvcUserList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvcUserList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        string[] lst = commandArg.Split('|');
        string userID = lst[0].ToString();
        string type = lst[1].ToString();
        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    //hard code wait change consumer and agent
                    if (type != SmartPortal.Constant.IPC.CHECKER &&
                       type != SmartPortal.Constant.IPC.MAKER &&
                       type != SmartPortal.Constant.IPC.ADMIN)
                    {
                        //RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=191&a=DETAILS&ID=" + userID));
                    }
                    else
                    {
                        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=SEMSDetailUserApprove&a=DETAILS&ID=" + userID + "&ct=" + "CCO" + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
                    }
                    break;

                case IPC.ACTIONPAGE.APPROVE:
                    //hard code wait change consumer and agent
                    if (type != SmartPortal.Constant.IPC.CHECKER &&
                       type != SmartPortal.Constant.IPC.MAKER &&
                       type != SmartPortal.Constant.IPC.ADMIN)
                    {
                        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=UserApprove&a=APPROVER&ID=" + userID));

                    }
                    else
                    {
                        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=ApproveUser&a=APPROVER&ID=" + userID + "&ct=" + "CCO" + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
                    }
                    break;
                case IPC.ACTIONPAGE.REJECT:
                    //hard code wait change consumer and agent
                    if (type != SmartPortal.Constant.IPC.CHECKER &&
                       type != SmartPortal.Constant.IPC.MAKER &&
                       type != SmartPortal.Constant.IPC.ADMIN)
                    {
                        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=REJECTUSER&a=REJECT&ID=" + userID));
                    }
                    else
                    {
                        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=UserReject&a=REJECT&ID=" + userID + "&ct=" + "CCO" + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
                    }
                    break;
            }
        }
    }
    protected void gvcUserList_Sorting(object sender, GridViewSortEventArgs e)
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
        try
        {
            string fullname = Utility.KillSqlInjection(txtfullname.Text.Trim());
            //string usertype = Utility.KillSqlInjection(ddlusertype.SelectedValue.ToString());
            //string birthday = Utility.KillSqlInjection(txtbirth.Text.Trim());
			string usertype="06";
            string email = Utility.KillSqlInjection(txtemail.Text.Trim());
            string phone = Utility.KillSqlInjection(txtphone.Text.Trim());
            string userlevel = Utility.KillSqlInjection(ddluserlevel.SelectedValue.ToString());
            string status = Utility.KillSqlInjection(ddlstatus.SelectedValue.ToString());

            DataTable dataTable;
            DataSet ds = new DataSet();

            ds = new SmartPortal.SEMS.User().GetUserByCondition(fullname, usertype, "", email, phone, userlevel, status, SmartPortal.Constant.IPC.SEMSUSERINSERT, "", ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                dataTable = ds.Tables[0];
            }
            else
            {
                goto ERROR;
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = sortExpression + direction;

                gvcUserList.DataSource = dataView;
                gvcUserList.DataBind();
            }

            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSUserApprove_Widget", "SortGridView", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSUserApprove_Widget", "SortGridView", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:

        ;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //Duyệt users-Quyềnnpv
        //Approvereject(SmartPortal.Constant.IPC.ACTIVE);
        //SendInfoLogin();

        //Duyệt giao dịch TUANTA

        CheckBox cbxSelect;
        HyperLink hpuserID;
        List<string> lstTran = new List<string>();
        try
        {
            foreach (GridViewRow gvr in gvcUserList.Rows)
            {
                cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxSelect.Checked == true)
                {
                    hpuserID = (HyperLink)gvr.Cells[1].FindControl("lbluserID");

                    //approve
                    // new SmartPortal.SEMS.Transactions().TellerApp(hpTranID.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);

                    lstTran.Add(hpuserID.Text.Trim());
                }
            }
            if (lstTran.Count != 0)
            {
                Session["userIDA"] = lstTran;
                if (CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE))
                    RedirectToActionPage(IPC.ACTIONPAGE.APPROVE, "&returnURL" + "=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query));

            }
            else
            {
                lblError.Text = Resources.labels.youmustchooseusertoapprove;
                BindData();
            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        string type = string.Empty;
        string userID = string.Empty;
        CheckBox cbxSelect;
        HyperLink hpuserID;
        Label lblTypeID;
        List<string> lstTran = new List<string>();
        try
        {
            foreach (GridViewRow gvr in gvcUserList.Rows)
            {
                cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxSelect.Checked == true)
                {
                    lblTypeID = (Label)gvr.Cells[1].FindControl("lblType");
                    type = lblTypeID.Text;
                    hpuserID = (HyperLink)gvr.Cells[1].FindControl("lbluserID");
                    userID = hpuserID.Text;
                    lstTran.Add(hpuserID.Text.Trim());
                }
            }
            if (lstTran.Count != 0)
            {
                //hard code wait change consumer and agent
                if (type != SmartPortal.Constant.IPC.CHECKER &&
                   type != SmartPortal.Constant.IPC.MAKER &&
                   type != SmartPortal.Constant.IPC.ADMIN)
                {
                    Session["userIDA"] = lstTran;
                    if (CheckPermitPageAction(IPC.ACTIONPAGE.REJECT))
                        RedirectToActionPage(IPC.ACTIONPAGE.REJECT, "&returnURL" + "=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query));
                }
                else
                {
                    Session["userIDA"] = lstTran;
                    Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=UserReject&a=REJECT&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
                }
            }
            else
            {
                lblError.Text = Resources.labels.youmustchooseusertoreject;
                BindData();
            }
        }
        catch (Exception ex)
        {

        }

    }

}
