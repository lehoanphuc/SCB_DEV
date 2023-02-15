using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using iTextSharp.text;
using Resources;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.SEMS;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class Widgets_SEMSREGION_Widget : WidgetBase
{
    private string IPCERRORCODE = "";
    private string IPCERRORDESC = "";
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            litError.Text = "";
            if (!IsPostBack)
            {
                LoadRegionType();
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAdd_New.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                GridViewPaging.Visible = false;
                //BindData();
				divResult.Visible = false;
            }

            GridViewPaging.pagingClickArgs += GridViewPagingClick;
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    #region Load DropDownList

    private void LoadRegionType()
    {
        ddlRegionSpecial.Items.Insert(0, new ListItem("All", ""));
        ddlRegionSpecial.Items.Insert(1, new ListItem("Yes", "Y"));
        ddlRegionSpecial.Items.Insert(2, new ListItem("No", "N"));
    }

    #endregion

    void BindData()
    {
        try
        {
			divResult.Visible = true;
            int pageIndex = gvRegionList.PageIndex;
            int pageSize = gvRegionList.PageSize;
            int recIndex = pageIndex * pageSize;
            if (Convert.ToInt32(((HiddenField) GridViewPaging.FindControl("TotalRows")).Value) <
                recIndex) return;
            DataSet dtRegion = new SmartPortal.SEMS.Region().SearchRegionByCondition(
                Utility.KillSqlInjection(txregionname.Text.Trim()),
                Utility.KillSqlInjection(ddlRegionSpecial.SelectedValue),
                Utility.KillSqlInjection(txtDescription.Text.Trim()),
                pageSize,recIndex, ref IPCERRORCODE,ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvRegionList.DataSource = dtRegion;
                gvRegionList.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORCODE;
            }

            Session["DataExport"] = gvRegionList;
            if (gvRegionList.Rows.Count > 0)
            {
                lblError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField) GridViewPaging.FindControl("TotalRows")).Value =
                    dtRegion.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void gvRegionList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label  lbdescription, lblRegionSpecial,lblStatus , lblregionid;
            LinkButton lbregionname, lbEdit, lbDelete;

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
                lblregionid = (Label)e.Row.FindControl("lblregionid");
                lbregionname = (LinkButton) e.Row.FindControl("lbregionname");
                lbdescription = (Label) e.Row.FindControl("lbdescription");
                lblRegionSpecial = (Label) e.Row.FindControl("lblRegionSpecial");
                lblStatus = (Label) e.Row.FindControl("lblStatus");
                lbEdit = (LinkButton) e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton) e.Row.FindControl("lbDelete");
                lbregionname.Text = drv["RegionName"].ToString();
                lblregionid.Text = drv["RegionID"].ToString();
                lbdescription.Text = drv["Description"].ToString();
                switch (drv["Status"].ToString())
                {
                    case "D":
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
                switch (drv["RegionSpecial"].ToString())
                {
                    case "Y":
                        lblRegionSpecial.Text = Resources.labels.yes;
                        lblRegionSpecial.Attributes.Add("class", "label-success");
                        break;
                    case "N":
                        lblRegionSpecial.Text = Resources.labels.no;
                        lblRegionSpecial.Attributes.Add("class", "label-warning");
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
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField) GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvRegionList.PageSize =
                Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvRegionList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
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
            string strlbregionid = "";
            try
            {
                foreach (GridViewRow gvr in gvRegionList.Rows)
                {
                    CheckBox cbxDelete = (CheckBox) gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked)
                    {
                        Label lbregionid = (Label) gvr.Cells[1].FindControl("lblregionid");
                        strlbregionid += lbregionid.Text + "#";
                    }
                }

                if (string.IsNullOrEmpty(strlbregionid))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] regionid = strlbregionid.Split('#');
                    for (int i = 0; i < regionid.Length - 1; i++)
                    {
                        new SmartPortal.SEMS.Region().DeleteRegion(regionid[i],
                            Session["userName"].ToString(), IPC.DELETE, ref IPCERRORCODE,
                            ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            btnSearch_Click(this,EventArgs.Empty);
                            lblError.Text = Resources.labels.xoaRegionthanhcong;
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
                Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                    this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"],
                    "");
            }
        }
    }

    protected void gvRegionList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }

    protected void gvRegionList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = ((Label) gvRegionList.Rows[e.RowIndex].Cells[1].FindControl("lblregionid"))
                .Text;
            new SmartPortal.SEMS.Region().DeleteRegion(commandArg, Session["userName"].ToString(),
                IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                btnSearch_Click(this, EventArgs.Empty);
                lblError.Text = Resources.labels.xoaRegionthanhcong;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void GridViewPagingClick(object sender, EventArgs e)
    {
        gvRegionList.PageSize =
            Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvRegionList.PageIndex = Convert.ToInt32(((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txregionname.Text = string.Empty;
        txtDescription.Text = string.Empty;
        btnSearch_Click(sender, e);
    }

    protected void ddlCountry_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        btnSearch_Click(sender, e);
    }

    protected void ddlRegionType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        btnSearch_Click(sender, e);
    }
}