using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSService_Widget : WidgetBase
{
    public static bool isSort = false;
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE;
    string IPCERRORDESC;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblError.Text = string.Empty;
                DataSet ds = new DataSet();
                ds = new Corporate().GetListActive(ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    ddlCorp.DataSource = dt;
                    ddlCorp.DataTextField = "CORPNAME";
                    ddlCorp.DataValueField = "CORPID";
                    ddlCorp.DataBind();
                    ddlCorp.Items.Insert(0, new ListItem(Resources.labels.tatca, "0"));
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                GridViewPagingControl.Visible = false;
                BindData();
            }
            GridViewPagingControl.pagingClickArgs += new EventHandler(GridViewPagingControl_Click);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSService", "Page_Load", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
    }
    private void GridViewPagingControl_Click(object sender, EventArgs e)
    {
        gvService.PageSize = Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("PageRowSize")).SelectedValue);
        gvService.PageIndex = Convert.ToInt32(((TextBox)GridViewPagingControl.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    private void BindData()
    {
        try
        {
            if (Convert.ToInt32(((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value) < gvService.PageIndex * gvService.PageSize) return;

            DataTable dt = new DataTable();
            dt = new Service().GetPaged(Utility.KillSqlInjection(txtServiceID.Text),Utility.KillSqlInjection(txtServiceCode.Text), Utility.KillSqlInjection(txtServiceName.Text), Utility.KillSqlInjection(ddlCorp.SelectedValue.ToString()), gvService.PageSize, gvService.PageIndex * gvService.PageSize, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (dt.Rows.Count == 0)
            {
                ltrError.Text = "<center><span style='color:red;margin-left:10px; margin-top:10px;font-weight:bold;'>" + Resources.labels.khongtimthaydulieu + "</span></center>";
                GridViewPagingControl.Visible = false;
            }
            else
            {
                ltrError.Text = "";
                GridViewPagingControl.Visible = true;
                ((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = dt.Rows[0]["TRECORDCOUNT"].ToString();
            }
            gvService.DataSource = dt;
            gvService.DataBind();

        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_SEMSService", "BindData", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSService", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSeach_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        BindData();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        LinkButton lblServiceID;

        string strCorpId = string.Empty;
        try
        {
            foreach (GridViewRow gvr in gvService.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lblServiceID = (LinkButton)gvr.Cells[1].FindControl("lblServiceID");
                    strCorpId += lblServiceID.Text.Trim() + "#";
                }
            }

            if (String.IsNullOrEmpty(strCorpId))
            {
                lblError.Text = Resources.labels.youmustchooseatleastoneitemtodelete;
            }
            else
            {
                string[] Service = strCorpId.Split('#');
                for (int i = 0; i < Service.Length - 1; i++)
                {
                    new Service().Delete(Service[i], Session[IPC.USERNAME].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (!IPCERRORCODE.Equals("0"))
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                }
                BindData();
                lblError.Text = Resources.labels.deleterecordssuccessfully;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton lblServiceID;
            Label lbServiceCode;
            Label lblServiceName;
            Label lblDescription;
            Label lblCorpName;
            Label lblCATNAME;
            Label lblStatus;
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
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;

                lblServiceID = (LinkButton)e.Row.FindControl("lblServiceID");
                lbServiceCode = (Label)e.Row.FindControl("lbServiceCode");
                lblServiceName = (Label)e.Row.FindControl("lblServiceName");
                lblDescription = (Label)e.Row.FindControl("lblDescription");
                lblCorpName = (Label)e.Row.FindControl("lblCorpName");
                lblCATNAME = (Label)e.Row.FindControl("lblCATNAME");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lblServiceID.Text = drv["ServiceID"].ToString();
                lbServiceCode.Text = drv["ServiceCode"].ToString();
                lblServiceName.Text = drv["ServiceName"].ToString();
                lblDescription.Text = drv["Description"].ToString();
                lblCorpName.Text = drv["CORPNAME"].ToString();
                lblCATNAME.Text = drv["CATNAME"].ToString();
                if (drv["Status"].ToString() == "A")
                {
                    lblStatus.Attributes["class"] = "success";
                    lblStatus.Text = Resources.labels.active;
                }
                else
                {
                    lblStatus.Attributes["class"] = "warning";
                    lblStatus.Text = Resources.labels.inactive;
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
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvService_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void gvServiceList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ServiceID = string.Empty;
        LinkButton lblServiceID;
        try
        {
            lblServiceID = (LinkButton)gvService.Rows[e.RowIndex].Cells[1].FindControl("lblServiceID");
            ServiceID = lblServiceID.Text;
            new Service().Delete(ServiceID, Session[IPC.USERNAME].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData();
                lblError.Text = Resources.labels.deleterecordssuccessfully;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
    }
}