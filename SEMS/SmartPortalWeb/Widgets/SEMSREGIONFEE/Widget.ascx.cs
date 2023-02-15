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

public partial class Widgets_SEMSREGIONFEE_Widget : WidgetBase
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
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAdd_New.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                GridViewPaging.Visible = false;
                LoadAllDropDownList();
                BindData();
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

    private void LoadAllDropDownList()
    {
        LoadAllCountry();
        LoadRegionType();
    }

    private void LoadRegionType()
    {
        ddlRegionType.Items.Insert(0, new ListItem("All", ""));
        ddlRegionType.Items.Insert(1, new ListItem("Autonomous Region", "A"));
        ddlRegionType.Items.Insert(2, new ListItem("Bang", "B"));
        ddlRegionType.Items.Insert(3, new ListItem("Federal Territory", "F"));
        ddlRegionType.Items.Insert(3, new ListItem("Region", "R"));
    }

    private void LoadAllCountry()
    {
        DataTable countryTable = new RegionFee().GetAllCountry(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        if (countryTable.Rows.Count > 0)
        {
            ddlCountry.DataSource = countryTable;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("All", ""));
        }
        else
        {
            ddlCountry.Items.Clear();
            ddlCountry.Items.Add(new ListItem(labels.nothing, ""));
        }

    }

    #endregion

    void BindData()
    {
        try
        {
            int pageIndex = gvRegionFeeList.PageIndex;
            int pageSize = gvRegionFeeList.PageSize;
            int recIndex = pageIndex * pageSize;
            if (Convert.ToInt32(((HiddenField) GridViewPaging.FindControl("TotalRows")).Value) <
                recIndex) return;
            DataSet dtRegion = new SmartPortal.SEMS.RegionFee().SearchRegionFeeByCondition(
                Utility.KillSqlInjection(txregionid.Text.Trim()),
                Utility.KillSqlInjection(txtRegionFeeCode.Text.Trim()),
                Utility.KillSqlInjection(txregionname.Text.Trim()),
                Utility.KillSqlInjection(ddlRegionType.SelectedValue.Trim()),
                Utility.KillSqlInjection(ddlCountry.SelectedValue.Trim()),
                pageSize,
                recIndex, ref IPCERRORCODE,
                ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvRegionFeeList.DataSource = dtRegion;
                gvRegionFeeList.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORCODE;
            }

            Session["DataExport"] = gvRegionFeeList;
            if (gvRegionFeeList.Rows.Count > 0)
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

    protected void gvRegionFeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lbregionname, lbdescription, lblRegionCode, lblRegionType, lblOrder, lblCountryName;
            LinkButton lbregionid, lbEdit, lbDelete;

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
                lbregionid = (LinkButton) e.Row.FindControl("lbregionid");
                lbregionname = (Label) e.Row.FindControl("lbregionname");
                lbdescription = (Label) e.Row.FindControl("lbdescription");
                lblRegionCode = (Label) e.Row.FindControl("lblRegionCode");
                lblRegionType = (Label) e.Row.FindControl("lblRegionType");
                lblOrder = (Label) e.Row.FindControl("lblOrder");
                lblCountryName = (Label) e.Row.FindControl("lblCountryName");
                lbEdit = (LinkButton) e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton) e.Row.FindControl("lbDelete");

                lbregionid.Text = drv["RegionID"].ToString();
                lbregionname.Text = drv["RegionName"].ToString();
                lbdescription.Text = drv["Description"].ToString();
                lblRegionCode.Text = drv["RegionCode"].ToString();
                string regionType = drv["RegionType"].ToString();
                switch (regionType)
                {
                    case "A":
                        lblRegionType.Text = Resources.labels.autonomousregion;
                        break;
                    case "B":
                        lblRegionType.Text = Resources.labels.bang;
                        break;
                    case "F":
                        lblRegionType.Text = Resources.labels.federalteritory;
                        break;
                    case "R":
                        lblRegionType.Text = Resources.labels.region;
                        break;
                    case "":
                        lblRegionType.Text = string.Empty;
                        break;
                }

                lblOrder.Text = drv["Order"].ToString();
                lblCountryName.Text = drv["CountryName"].ToString();

                switch (lbregionid.Text.Trim())
                {
                    case "1":
                        lbEdit.Enabled = false;
                        lbEdit.OnClientClick = string.Empty;
                        lbDelete.Enabled = false;
                        lbDelete.OnClientClick = string.Empty;
                        break;
                    default:
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
            gvRegionFeeList.PageSize =
                Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvRegionFeeList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
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
                foreach (GridViewRow gvr in gvRegionFeeList.Rows)
                {
                    CheckBox cbxDelete = (CheckBox) gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked)
                    {
                        LinkButton lbregionid = (LinkButton) gvr.Cells[1].FindControl("lbregionid");
                        strlbregionid += lbregionid.CommandArgument.Trim() + "#";
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
                        new SmartPortal.SEMS.RegionFee().DeleteRegionFee(regionid[i],
                            Session["userName"].ToString(), IPC.DELETE, ref IPCERRORCODE,
                            ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            btnSearch_Click(this,EventArgs.Empty);
                            lblError.Text = Resources.labels.xoaregionfeethanhcong;
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

    protected void gvRegionFeeList_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void gvRegionFeeList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = ((LinkButton) gvRegionFeeList.Rows[e.RowIndex].Cells[1].FindControl("lbregionid"))
                .CommandArgument;
            new SmartPortal.SEMS.RegionFee().DeleteRegionFee(commandArg, Session["userName"].ToString(),
                IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                btnSearch_Click(this, EventArgs.Empty);
                lblError.Text = Resources.labels.xoaregionfeethanhcong;
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
        gvRegionFeeList.PageSize =
            Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvRegionFeeList.PageIndex = Convert.ToInt32(((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlCountry.SelectedIndex = 0;
        ddlRegionType.SelectedIndex = 0;
        txregionid.Text = string.Empty;
        txregionname.Text = string.Empty;
        txtRegionFeeCode.Text = string.Empty;
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