using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSCorpUserLimit_Controls_Widget : WidgetBase
{
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    private string USERID
    {
        get
        {
            return ViewState["USERID"] == null ? string.Empty : ViewState["USERID"].ToString();
        }
        set { ViewState["USERID"] = value; }
    }
    private string FULLNAME
    {
        get
        {
            return ViewState["FULLNAME"] == null ? string.Empty : ViewState["FULLNAME"].ToString();
        }
        set { ViewState["FULLNAME"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            ACTION = GetActionPage();
            txtLimitPerTrans.Attributes.Add("onkeypress", "executeComma('" + txtLimitPerTrans.ClientID + "')");
            txtTotalTransPerDay.Attributes.Add("onkeypress", "executeComma('" + txtTotalTransPerDay.ClientID + "')");
            txtLimitPerDay.Attributes.Add("onkeypress", "executeComma('" + txtLimitPerDay.ClientID + "')");
            txtLimitPerTrans.Attributes.Add("onkeyup", "executeComma('" + txtLimitPerTrans.ClientID + "')");
            txtTotalTransPerDay.Attributes.Add("onkeyup", "executeComma('" + txtTotalTransPerDay.ClientID + "')");
            txtLimitPerDay.Attributes.Add("onkeyup", "executeComma('" + txtLimitPerDay.ClientID + "')");
            if (!IsPostBack)
            {
                LoadDll();
                FirstGridViewRow();
                BindData();
                GridViewPaging.Visible = false;
                divResult.Visible = false;
                GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvUser.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvUser.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        GetAllUser();
    }

    private void GetAllUser()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvUser.PageIndex * gvUser.PageSize) return;
            DataSet dsResult = new UserLimit().GetCMUser(
                Utility.KillSqlInjection(txtUserID.Text.Trim()),
                Utility.KillSqlInjection(txtFullName.Text),
                Utility.KillSqlInjection(txtContractNo.Text.Trim()), gvUser.PageSize, gvUser.PageIndex * gvUser.PageSize,
                ref IPCERRORCODE, ref IPCERRORDESC
            );
            if (IPCERRORCODE == "0")
            {
                gvUser.DataSource = dsResult;
                gvUser.DataBind();
            }

            if (dsResult.Tables[0].Rows.Count > 0)
            {
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dsResult.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void LoadDll()
    {
        try
        {
            ddlCcyid.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlCcyid.DataTextField = "CCYID";
            ddlCcyid.DataValueField = "CCYID";
            ddlCcyid.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    AddNew();
                    lblTitle.Text = Resources.labels.corporateuserlimit;
                    pnSeachContract.Visible = true;
                    pnLimt.Visible = false;
                    btnNext.Enabled = true;
                    btnSave.Visible = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    GetInfo();
                    lblTitle.Text = Resources.labels.corporateuserlimit;
                    pnSeachContract.Visible = false;
                    pnLimt.Visible = true;
                    btnNext.Visible = false;
                    btnSave.Enabled = true;
                    pnGV.Visible = true;
                    break;
                default:
                    GetInfo();
                    lblTitle.Text = Resources.labels.corporateuserlimit;
                    pnSeachContract.Visible = false;
                    pnLimt.Visible = true;
                    btnNext.Visible = false;
                    btnSave.Enabled = false;
                    pnAdd.Enabled = false;
                    btnSave.Enabled = false;
                    pnAdd.Enabled = false;
                    gvUserLimit.Columns[7].Visible = false;
                    pnGV.Visible = true;
                    btnPrint.Visible = true;
                    PrintData();
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvUser.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string selectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvUser.PageIndex = !string.IsNullOrEmpty(selectedPageNo) ? Convert.ToInt32(selectedPageNo) - 1 : 0;
            GetAllUser();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void gvUserLimit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            gvUserLimit.PageIndex = e.NewPageIndex;
            gvUserLimit.DataSource = dt;
            gvUserLimit.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvUserLimit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                int id = Convert.ToInt32(e.RowIndex + gvUserLimit.PageIndex * gvUserLimit.PageSize);
                dt.Rows.RemoveAt(id);
                ViewState["CurrentTable1"] = dt;
                gvUserLimit.PageIndex = 0;
                gvUserLimit.DataSource = dt;
                gvUserLimit.DataBind();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnNext_OnClick(object sender, EventArgs e)
    {
        RadioButton rbSelect;
        Label lblUserID, lblFullname;
        foreach (GridViewRow gvr in gvUser.Rows)
        {
            rbSelect = (RadioButton)gvr.Cells[0].FindControl("rbUserID");
            if (rbSelect.Checked)
            {
                lblUserID = (Label)gvr.Cells[0].FindControl("lblUserID");
                lblFullname = (Label)gvr.Cells[0].FindControl("lblFullname");
                USERID = lblUserID.Text;
                FULLNAME = lblFullname.Text;
                break;
            }
        }
        if (!string.IsNullOrEmpty(USERID))
        {
            pnSeachContract.Visible = false;
            pnAdd.Visible = true;
            txtUserIDStep2.Text = USERID;
            LoadTranList(USERID);

            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            gvUserLimit.DataSource = dt;
            gvUserLimit.DataBind();

            pnLimt.Visible = true;
            pnGV.Visible = true;
            btnSave.Visible = true;
            btnNext.Visible = false;
        }
        else
        {
            lblError.Text = Resources.labels.pleaseselectuser;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    #region ADD
                    DataTable dt = (DataTable)ViewState["CurrentTable1"];

                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.AddRange(new DataColumn[] { new DataColumn(IPC.USERID), new DataColumn(IPC.TRANCODE), new DataColumn(IPC.CCYID), new DataColumn(IPC.TRANLIMIT, typeof(double)), new DataColumn(IPC.TOTALLIMITDAY, typeof(double)), new DataColumn(IPC.COUNTLIMIT, typeof(int)), new DataColumn(IPC.USERCREATED), new DataColumn(IPC.DATECREATED), new DataColumn(IPC.SOURCEID) });
                    foreach (DataRow row in dt.Rows)
                    {
                        dtTemp.Rows.Add(row[IPC.USERID], row[IPC.TRANCODE], row[IPC.CCYID], row[IPC.TRANLIMIT], row[IPC.TOTALLIMITDAY], row[IPC.COUNTLIMIT], row[IPC.USERCREATED], row[IPC.DATECREATED], row[IPC.SOURCEID]);
                    }

                    try
                    {
                        if (dtTemp.Rows.Count > 0)
                        {
                          new UserLimit().AddCMUserLimit(dtTemp, ref IPCERRORCODE, ref IPCERRORDESC);
                            if (IPCERRORCODE.Equals("0"))
                            {
                                lblError.Text = Resources.labels.themmoithanhcong;
                                btnSave.Enabled = false;
                                btnPrint.Visible = true;
                                PrintData();
                                pnAdd.Enabled = false;
                                gvUserLimit.Columns[7].Visible = false;
                                return;
                            }
                            else
                            {
                                lblError.Text = IPCERRORDESC;
                                return;
                            }
                        }
                        else
                        {
                            lblError.Text = Resources.labels.youmustaddrecordstosave;
                        }

                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }
                    break;
                #endregion
                case IPC.ACTIONPAGE.EDIT:
                    #region EDIT
                    try
                    {
                        if (!ValidateInput())
                        {
                            return;
                        }
                        DataSet dataResult = new UserLimit().UpdateUserLimit(Utility.KillSqlInjection(txtUserIDStep2.Text), Utility.KillSqlInjection(ddlTransType.SelectedValue), Utility.KillSqlInjection(ddlCcyid.SelectedValue),
                            double.Parse(Utility.KillSqlInjection(txtLimitPerTrans.Text.Trim())), double.Parse(Utility.KillSqlInjection(txtLimitPerDay.Text.Trim())), int.Parse(txtTotalTransPerDay.Text.Replace(",", "").Trim()), Session["userName"].ToString(), DateTime.Now.ToString("MM/dd/yyyy"), ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE.Equals("0"))
                        {
                            lblError.Text = Resources.labels.suathanhcong;
                            btnSave.Visible = false;
                            btnPrint.Visible = true;
                            ViewState["CurrentTable1"] = dataResult.Tables[0];
                            PrintData();
                            pnAdd.Enabled = false;
                            gvUserLimit.Columns[7].Visible = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }
                    break;
                    #endregion
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateInput())
            {
                return;
            }

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];

                if (!dtCurrentTable.Columns.Contains(IPC.USERNAME))
                    dtCurrentTable.Columns.Add(IPC.USERNAME, typeof(string));
                if (!dtCurrentTable.Columns.Contains(IPC.TRANSACTION))
                    dtCurrentTable.Columns.Add(IPC.TRANSACTION, typeof(string));

                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        if (!dtCurrentTable.Rows[i][IPC.USERID].ToString().Equals(txtUserIDStep2.Text)
                            || !dtCurrentTable.Rows[i][IPC.TRANCODE].ToString().Equals(ddlTransType.SelectedValue)
                            || !dtCurrentTable.Rows[i][IPC.CCYID].ToString().Equals(ddlCcyid.SelectedValue))
                        {
                            int pageIndex = (dtCurrentTable.Rows.IndexOf(dtCurrentTable.Rows[i]) + 1) / gvUserLimit.PageSize;
                            gvUserLimit.PageIndex = pageIndex;
                        }
                        else
                        {
                            int pageIndex = dtCurrentTable.Rows.IndexOf(dtCurrentTable.Rows[i]) / gvUserLimit.PageSize;
                            gvUserLimit.PageIndex = pageIndex;
                            lblError.Text = "Existed";
                            gvUserLimit.DataSource = dtCurrentTable;
                            gvUserLimit.DataBind();
                            Highlight();
                            return;
                        }
                    }
                }

                dtCurrentTable.Rows.Add(Utility.KillSqlInjection(txtUserIDStep2.Text), FULLNAME, Utility.KillSqlInjection(ddlTransType.SelectedValue), Utility.KillSqlInjection(ddlTransType.SelectedItem.Text), Utility.KillSqlInjection(ddlCcyid.SelectedValue), double.Parse(Utility.KillSqlInjection(txtLimitPerTrans.Text.Trim())), double.Parse(Utility.KillSqlInjection(txtLimitPerDay.Text.Trim())), int.Parse(txtTotalTransPerDay.Text.Replace(",", "").Trim()), Session["userName"].ToString(), DateTime.Now.ToString("MM/dd/yyyy"), IPC.SOURCEIDVALUE);

                ViewState["CurrentTable1"] = dtCurrentTable;
                gvUserLimit.DataSource = dtCurrentTable;
                gvUserLimit.DataBind();

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void Highlight()
    {
        Label userID, transaction, fullname, translimit, transPerDay, transLimitPerDay, currency;

        foreach (GridViewRow gvr in gvUserLimit.Rows)
        {
            transaction = (Label)gvr.Cells[0].FindControl("lblTransaction");
            userID = (Label)gvr.Cells[1].FindControl("lblUserID");
            fullname = (Label)gvr.Cells[2].FindControl("lblUserName");
            translimit = (Label)gvr.Cells[3].FindControl("lblTransLimit");
            transPerDay = (Label)gvr.Cells[4].FindControl("lblTransPerDay");
            transLimitPerDay = (Label)gvr.Cells[5].FindControl("lblTransLimitPerDay");
            currency = (Label)gvr.Cells[6].FindControl("lblCurrency");

            if (transaction.Text.Equals(ddlTransType.SelectedItem.Text)
                && userID.Text.Equals(txtUserIDStep2.Text)
                && currency.Text.Equals(ddlCcyid.SelectedValue))
            {
                transaction.ForeColor = System.Drawing.Color.Red;
                userID.ForeColor = System.Drawing.Color.Red;
                fullname.ForeColor = System.Drawing.Color.Red;
                translimit.ForeColor = System.Drawing.Color.Red;
                transPerDay.ForeColor = System.Drawing.Color.Red;
                transLimitPerDay.ForeColor = System.Drawing.Color.Red;
                currency.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void rbUserID_onChange(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvUser.Rows.Count; i++)
            {
                GridViewRow rowOld = gvUser.Rows[i];
                RadioButton colRbContractNo = (RadioButton)rowOld.Cells[0].Controls[1];
                colRbContractNo.Checked = false;
            }
            RadioButton item = (RadioButton)sender;
            item.Checked = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void PrintData()
    {
        DataTable dtUser = (DataTable)ViewState["CurrentTable1"];
        Session["printUL"] = dtUser;
    }

    protected void FirstGridViewRow()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn(IPC.USERID), new DataColumn(IPC.USERNAME), new DataColumn(IPC.TRANCODE), new DataColumn(IPC.TRANSACTION), new DataColumn(IPC.CCYID), new DataColumn(IPC.TRANLIMIT, typeof(double)), new DataColumn(IPC.TOTALLIMITDAY, typeof(double)), new DataColumn(IPC.COUNTLIMIT, typeof(int)), new DataColumn(IPC.USERCREATED), new DataColumn(IPC.DATECREATED), new DataColumn(IPC.SOURCEID) });
        ViewState["CurrentTable1"] = dt;
    }

    protected bool ValidateInput()
    {
        if (String.IsNullOrEmpty(txtLimitPerTrans.Text))
        {
            lblError.Text = Resources.labels.hanmucmotgiaodich + " can not be empty";
            return false;
        }
        if (String.IsNullOrEmpty(txtLimitPerDay.Text))
        {
            lblError.Text = Resources.labels.tonghanmucngay + " can not be empty";
            return false;
        }
        if (String.IsNullOrEmpty(txtTotalTransPerDay.Text))
        {
            lblError.Text = Resources.labels.sogiaodichtrenngay + " can not be empty";
            return false;
        }
        double limitPerTrans = double.Parse(Utility.KillSqlInjection(txtLimitPerTrans.Text.Trim()));
        double limitPerDay = double.Parse(Utility.KillSqlInjection(txtLimitPerDay.Text.Trim()));
        int countlimit = int.Parse(txtTotalTransPerDay.Text.Trim().Replace(",", ""));

        if (limitPerTrans == 0)
        {
            lblError.Text = Resources.labels.hanmucmotgiaodich + " must be greater than 0";
            return false;
        }
        if (limitPerDay == 0)
        {
            lblError.Text = Resources.labels.tonghanmucngay + " must be greater than 0";
            return false;
        }
        if (countlimit == 0)
        {
            lblError.Text = Resources.labels.sogiaodichtrenngay + " must be greater than 0";
            return false;
        }
        if (limitPerTrans > limitPerDay)
        {
            lblError.Text = Resources.labels.tonghanmucngay + " must be greater than " + Resources.labels.hanmucmotgiaodich;
            return false;
        }

        return true;
    }

    protected void gvUserLimit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblUserID, lblUserName, lblTransLimitPerDay, lblTransaction, lblTransLimit, lblTransPerDay, lblCurrency;
            LinkButton lbDelete;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                TableRow tbRow = (TableRow)(e.Row.Cells[0].Controls[0].Controls[0]);
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + " :</strong>";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                lblUserID = (Label)e.Row.FindControl("lblUserID");
                lblUserName = (Label)e.Row.FindControl("lblUserName");
                lblTransaction = (Label)e.Row.FindControl("lblTransaction");
                lblTransLimit = (Label)e.Row.FindControl("lblTransLimit");
                lblTransPerDay = (Label)e.Row.FindControl("lblTransPerDay");
                lblTransLimitPerDay = (Label)e.Row.FindControl("lblTransLimitPerDay");
                lblCurrency = (Label)e.Row.FindControl("lblCurrency");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lblUserID.Text = drv[IPC.USERID].ToString();
                lblUserName.Text = drv[IPC.USERNAME].ToString();
                lblTransaction.Text = drv[IPC.TRANSACTION].ToString();
                lblTransLimit.Text = Utility.FormatMoneyInputToView(drv[IPC.TRANLIMIT].ToString(), drv[IPC.CCYID].ToString());
                lblTransPerDay.Text = Utility.FormatMoneyInputToView(drv[IPC.COUNTLIMIT].ToString(), drv[IPC.CCYID].ToString()).Split('.')[0];
                lblTransLimitPerDay.Text = Utility.FormatMoneyInputToView(drv[IPC.TOTALLIMITDAY].ToString(), drv[IPC.CCYID].ToString());
                lblCurrency.Text = drv[IPC.CCYID].ToString();
                lbDelete.Text = Resources.labels.delete;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void AddNew()
    {
        try
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            gvUserLimit.DataSource = dt;
            gvUserLimit.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void GetInfo()
    {
        try
        {
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            string[] key = ID.Split('|');
            DataSet ds = new UserLimit().GetCMUserLimitByUserID(
                        Utility.KillSqlInjection(key[0]),
                        Utility.KillSqlInjection(key[1]),
                        Utility.KillSqlInjection(key[2]),
                        ref IPCERRORCODE, ref IPCERRORDESC);


            LoadTranList(key[0].ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtUserIDStep2.Text = key[0].ToString();
                USERID = key[0].ToString();
                ddlTransType.SelectedValue = ds.Tables[0].Rows[0][IPC.TRANCODE].ToString();
                txtLimitPerTrans.Text = Utility.FormatMoney(ds.Tables[0].Rows[0][IPC.TRANLIMIT].ToString(), ds.Tables[0].Rows[0][IPC.CCYID].ToString());
                txtLimitPerDay.Text = Utility.FormatMoney(ds.Tables[0].Rows[0][IPC.TOTALLIMITDAY].ToString(), ds.Tables[0].Rows[0][IPC.CCYID].ToString());
                txtTotalTransPerDay.Text = Utility.FormatMoney(ds.Tables[0].Rows[0][IPC.COUNTLIMIT].ToString(), ds.Tables[0].Rows[0][IPC.CCYID].ToString()).Split('.')[0];
                ddlCcyid.Text = ds.Tables[0].Rows[0][IPC.CCYID].ToString();
                pnSeachContract.Visible = false;
                pnAdd.Visible = true;
                ddlTransType.Enabled = false;
                ddlCcyid.Enabled = false;
                btnAdd.Visible = false;
                ViewState["CurrentTable1"] = ds.Tables[0];
            }
            else
            {
                lblError.Text = Resources.labels.datanotfound;
                pnSeachContract.Visible = false;
                pnAdd.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void LoadTranList(string userid)
    {
        try
        {
            string CONTRACT_NO = string.Empty;
            DataSet dsContract = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(userid, "", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
            }
            DataTable dtContract = new DataTable();
            dtContract = dsContract.Tables[0];
            if (dtContract.Rows.Count != 0)
            {
                CONTRACT_NO = dtContract.Rows[0]["CONTRACTNO"].ToString();
            }
            DataSet dsResult = new SmartPortal.SEMS.Product().GetTranNameByContractNo(Utility.KillSqlInjection(CONTRACT_NO), ref IPCERRORCODE, ref IPCERRORDESC);
            ddlTransType.DataSource = dsResult;
            ddlTransType.DataTextField = "PAGENAME";
            ddlTransType.DataValueField = "TRANCODE";
            ddlTransType.DataBind();
            ddlTransType.Items.Insert(0, new ListItem(IPC.ALL, IPC.ALL));

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}