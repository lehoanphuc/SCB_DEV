using System;
using System.Data;
using System.Web.UI.WebControls;
using Resources;
using SmartPortal.BLL;
using SmartPortal.Constant;
using SmartPortal.SEMS;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSATMSearch_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
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
                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                LoadAllDropdownList();
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

    void  BindData()
    {
        try
        {
            divResult.Visible = true;
            int pageSize = gvATMList.PageSize;
            int pageIndex = gvATMList.PageIndex;
            int recordIndex = pageIndex * pageSize;
            litError.Text = string.Empty;
            GridViewPaging.Visible = true;
            if (Convert.ToInt32(((HiddenField) GridViewPaging.FindControl("TotalRows")).Value) < recordIndex)
                return;

            DataTable atmTable = new ATM().GetATMPlaceByCondition(Utility.KillSqlInjection(txtATMid.Text.Trim()),
                Utility.KillSqlInjection(txtATMAdd.Text.Trim()),
                Utility.KillSqlInjection(ddlCountry.SelectedValue.Trim()),
                Utility.KillSqlInjection(ddlCity.SelectedValue.Trim()),
                Utility.KillSqlInjection(ddlDistrict.SelectedValue.Trim()),
                Utility.KillSqlInjection(ddlBranch.SelectedValue.Trim()),
                pageSize,
                recordIndex,
                ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (IPCERRORCODE == "0")
            {
                gvATMList.DataSource = atmTable;
                gvATMList.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORCODE;
            }

            if (atmTable.Rows.Count > 0)
            {
                litError.Text = string.Empty;
                ((HiddenField) GridViewPaging.FindControl("TotalRows")).Value =
                    atmTable.Rows[0]["TRECORDCOUNT"].ToString();
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

    protected void gvATMList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton lblATMCode;
            Label lblATMAddress;
            LinkButton lbEdit;
            LinkButton lbDelete;
            DataRowView drv;
            Label lblCountry, lblCity, lblDistrict, lblBranch;


            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                TableRow tbRow = (TableRow) (e.Row.Cells[0].Controls[0].Controls[0]);
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + " :</strong>";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView) e.Row.DataItem;
                cbxSelect = (CheckBox) e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                lblATMCode = (LinkButton) e.Row.FindControl("lblATMCode");
                lblATMAddress = (Label) e.Row.FindControl("lblAddress");
                lblCountry = (Label) e.Row.FindControl("lblCountry");
                lblCity = (Label) e.Row.FindControl("lblCity");
                lblDistrict = (Label) e.Row.FindControl("lblDistrict");
                lblBranch = (Label) e.Row.FindControl("lblBranch");

                lbEdit = (LinkButton) e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton) e.Row.FindControl("lbDelete");

                lblATMCode.Text = drv["ATMID"].ToString();
                lblATMAddress.Text = drv["ADDRESS"].ToString();
                lblCountry.Text = drv["CountryName"].ToString();
                lblCity.Text = drv["CityName"].ToString();
                lblDistrict.Text = drv["DistName"].ToString();
                lblBranch.Text = drv["BranchName"].ToString();
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


    #region Load DropDownList

    private void LoadAllDropdownList()
    {
        LoadCountry();
        LoadCity();
        LoadDistrict();
        LoadBranch();
    }

    private void LoadCountry()
    {
        try
        {
            DataTable country = new ATM().GetAllCountry(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (country.Rows.Count > 0)
            {
                ddlCountry.DataSource = country;
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("All", ""));
            }
            else
            {
                ddlCountry.Items.Clear();
                ddlCountry.Items.Add(new ListItem(labels.nothing, ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    private void LoadDistrict()
    {
        try
        {
            DataTable distTable = new ATM().GetAllDistrict(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (distTable.Rows.Count > 0)
            {
                ddlDistrict.DataSource = distTable;
                ddlDistrict.DataTextField = "DistName";
                ddlDistrict.DataValueField = "DistCode";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("All", ""));
                ddlDistrict.SelectedIndex = 0;
            }
            else
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Add(new ListItem(labels.nothing, ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }


    private void LoadCity()
    {
        try
        {
            DataTable cityTable = new ATM().GetAllCity(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (cityTable.Rows.Count > 0)
            {
                ddlCity.DataSource = cityTable;
                ddlCity.DataTextField = "CityName";
                ddlCity.DataValueField = "CityCode";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("All", ""));
                ddlCity.SelectedIndex = 0;
            }
            else
            {
                ddlCity.Items.Clear();
                ddlCity.Items.Add(new ListItem(labels.nothing, ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }


    private void LoadBranch()
    {
        try
        {
            DataTable branchTable = new Branch()
                .SearchBranchByCondition("", "", "", "", 0, 0, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (branchTable.Rows.Count > 0)
            {
                ddlBranch.DataSource = branchTable;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("All", ""));
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(new ListItem(labels.nothing, ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField) GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvATMList.PageSize =
                Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvATMList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
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
            LinkButton lblATMCode;
            string strATMCode = "";
            try
            {
                foreach (GridViewRow gvr in gvATMList.Rows)
                {
                    cbxDelete = (CheckBox) gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lblATMCode = (LinkButton) gvr.Cells[1].FindControl("lblATMCode");
                        strATMCode += lblATMCode.Text.Trim() + "#";
                    }
                }

                if (string.IsNullOrEmpty(strATMCode))
                {
                    lblError.Text = labels.pleaseselectbeforedeleting;
                }
                else
                {
                    string[] ATMCode = strATMCode.Split('#');
                    for (int i = 0; i < ATMCode.Length - 1; i++)
                    {
                        new ATM().DeleteATM(ATMCode[i], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            btnSearch_Click(this, EventArgs.Empty);
                            lblError.Text = Resources.labels.xoaatmthanhcong;
                        }
                        else
                        {
                            ErrorCodeModel EM = new ErrorCodeModel();
                            EM = new ErrorBLL().Load(Utility.IsInt(IPCERRORCODE),
                                System.Globalization.CultureInfo.CurrentCulture.ToString());
                            lblError.Text = EM.ErrorDesc;
                            return;
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

    protected void bt_export_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable atmTable = new ATM().GetATMPlaceByCondition(Utility.KillSqlInjection(txtATMid.Text.Trim()),
                Utility.KillSqlInjection(txtATMAdd.Text.Trim()),
                Utility.KillSqlInjection(ddlCountry.SelectedValue.Trim()),
                Utility.KillSqlInjection(ddlCity.SelectedValue.Trim()),
                Utility.KillSqlInjection(ddlDistrict.SelectedValue.Trim()),
                Utility.KillSqlInjection(ddlBranch.SelectedValue.Trim()),
                0,
                0,
                ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            DataTable datatable = new DataTable();

            datatable.Columns.Add(atmTable.Columns["ATMID"].ToString());
            datatable.Columns.Add(atmTable.Columns["Address"].ToString());
            datatable.Columns.Add(atmTable.Columns["CountryName"].ToString());
            datatable.Columns.Add(atmTable.Columns["CityName"].ToString());
            datatable.Columns.Add(atmTable.Columns["DistName"].ToString());
            datatable.Columns.Add(atmTable.Columns["BranchName"].ToString());

            for (int i = 0; i < atmTable.Rows.Count; i++)
            {
                DataRow dr = datatable.NewRow();
                dr[0] = atmTable.Rows[i]["ATMID"].ToString();
                dr[1] = atmTable.Rows[i]["Address"].ToString();
                dr[2] = atmTable.Rows[i]["CountryName"].ToString();
                dr[3] = atmTable.Rows[i]["CityName"].ToString();
                dr[4] = atmTable.Rows[i]["DistName"].ToString();
                dr[5] = atmTable.Rows[i]["BranchName"].ToString();
                datatable.Rows.Add(dr);
            }

            SmartPortal.Common.ExportToFile.ExportToExcel(datatable, "ATM_List");
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void gvATMList_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void gvATMList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton) gvATMList.Rows[e.RowIndex].Cells[1].FindControl("lblATMCode")).Text;
            new ATM().DeleteATM(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                btnSearch_Click(sender, EventArgs.Empty);
                lblError.Text = Resources.labels.xoaatmthanhcong;
            }
            else
            {
                ErrorCodeModel EM = new ErrorCodeModel();
                EM = new ErrorBLL().Load(Utility.IsInt(IPCERRORCODE),
                    System.Globalization.CultureInfo.CurrentCulture.ToString());
                lblError.Text = EM.ErrorDesc;
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
        gvATMList.PageSize =
            Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvATMList.PageIndex = Convert.ToInt32(((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }



}