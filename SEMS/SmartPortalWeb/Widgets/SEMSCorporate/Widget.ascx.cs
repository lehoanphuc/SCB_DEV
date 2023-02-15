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

public partial class Widgets_SEMSCorporate_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadDLL();
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                GridViewPagingControl.Visible = false;
                BindData();
            }
            GridViewPagingControl.pagingClickArgs += new EventHandler(GridViewPagingControl_Click);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCorporate", "Page_Load", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
    }
    void LoadDLL()
    {
        try
        {
            DataSet ds = new Corporate().GetCatalog(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlCatalog.DataSource = dt;
                    ddlCatalog.DataTextField = "CATNAME";
                    ddlCatalog.DataValueField = "CATID";
                    ddlCatalog.DataBind();
                    ddlCatalog.Items.Insert(0, new ListItem(Resources.labels.tatca, "0"));
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void GridViewPagingControl_Click(object sender, EventArgs e)
    {
        gridView.PageSize = Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("PageRowSize")).SelectedValue);
        gridView.PageIndex = Convert.ToInt32(((TextBox)GridViewPagingControl.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            if (Convert.ToInt32(((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value) < gridView.PageIndex * gridView.PageSize) return;
            DataTable dtCL = new DataTable();

            DataSet ds = new DataSet();
            ds = new Corporate().GetPaged(
                Utility.KillSqlInjection(txtCorpID.Text.Trim()), Utility.KillSqlInjection(txtCorpName.Text.Trim()), int.Parse(ddlCatalog.SelectedValue), gridView.PageSize, gridView.PageIndex * gridView.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                dtCL = ds.Tables[0];
                if (dtCL.Rows.Count == 0)
                {
                    ltrError.Text = "<center><span style='color:red;margin-left:10px; margin-top:10px;font-weight:bold;'>" + Resources.labels.khongtimthaydulieu + "</span></center>";
                    GridViewPagingControl.Visible = false;
                }
                else
                {
                    ltrError.Text = "";
                    GridViewPagingControl.Visible = true;
                    ((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = dtCL.Rows[0]["TRECORDCOUNT"].ToString();
                }
                gridView.DataSource = dtCL;
                gridView.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_SEMSCorporate", "BindData", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCorporate", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        LinkButton lbCorpID;

        string strCorpId = string.Empty;
        try
        {
            foreach (GridViewRow gvr in gridView.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lbCorpID = (LinkButton)gvr.Cells[1].FindControl("lbCorpID");
                    strCorpId += lbCorpID.Text.Trim() + "#";
                }
            }

            if (String.IsNullOrEmpty(strCorpId))
            {
                lblError.Text = Resources.labels.youmustchooseatleastoneitemtodelete;
            }
            else
            {
                string[] corpId = strCorpId.Split('#');
                for (int i = 0; i < corpId.Length - 1; i++)
                {
                    new SmartPortal.SEMS.Corporate().Delete(corpId[i], Session[IPC.USERNAME].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0"))
                    {
                        BindData();
                        lblError.Text = Resources.labels.deleterecordssuccessfully;
                    }
                    else
                    {
                        lblError.Text = Resources.labels.deleteservicebeforedeletecorporate;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton lbCorpID;
            Label lblStatus;
            LinkButton lbEdit;
            LinkButton lbDelete;
            Label lblCorpName;
            Label lblCatalogName;
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

                lbCorpID = (LinkButton)e.Row.FindControl("lbCorpID");
                lblCorpName = (Label)e.Row.FindControl("lblCorpName");
                lblCatalogName = (Label)e.Row.FindControl("lblCatalogName");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;

                lbCorpID.Text = drv[IPC.CORPID].ToString();
                lblCorpName.Text = drv[IPC.CORPNAME].ToString();
                lblCatalogName.Text = drv["CATNAME"].ToString();
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
            throw ex;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        BindData();
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            size = 0;
            gridView.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string corpID = string.Empty;
        LinkButton lbCorpID;
        try
        {
            lbCorpID = (LinkButton)gridView.Rows[e.RowIndex].Cells[1].FindControl("lbCorpID");
            corpID = lbCorpID.Text;
            new SmartPortal.SEMS.Corporate().Delete(corpID, Session[IPC.USERNAME].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData();
                lblError.Text = Resources.labels.deleterecordssuccessfully;
            }
            else
            {
                lblError.Text = Resources.labels.deleteservicebeforedeletecorporate;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}