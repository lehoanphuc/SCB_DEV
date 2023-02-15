using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.SEMS;
using Resources;

public partial class Widgets_SEMSCorpApproveWorkflow_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
               LoadDll();
               GridViewPagingControl.Visible = false;
            }
            GridViewPagingControl.pagingClickArgs += new EventHandler(GridViewPagingControl_Click);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void GridViewPagingControl_Click(object sender, EventArgs e)
    {
        gvApprList.PageSize = Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("PageRowSize")).SelectedValue);
        gvApprList.PageIndex = Convert.ToInt32(((TextBox)GridViewPagingControl.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }

    void LoadDll()
    {
        try
        {
            DataTable dtCurrency = new ExchangeRate().GetAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            ddlCCYID.DataSource = dtCurrency;
            ddlCCYID.DataTextField = "Currency";
            ddlCCYID.DataValueField = "Currency";
            ddlCCYID.DataBind();
            ddlCCYID.Items.Insert(0, new ListItem(IPC.ALL, IPC.ALL));
            LoadCountry();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message,
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    private void LoadCountry()
    {
        try
        {
            DataTable country = new ExchangeRate().GetAllCountry("",ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (country.Rows.Count > 0)
            {
                ddlCountry.DataSource = country;
                ddlCountry.DataValueField = "COUNTRYCODES";
                ddlCountry.DataTextField = "COUNTRYNAME";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem(IPC.ALL, IPC.ALL));
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

    void BindData()
    {
        try
        {
            if (Convert.ToInt32(((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value) < gvApprList.PageIndex * gvApprList.PageSize) return;
            DataSet dtProcess = new DataSet();
            dtProcess = new SmartPortal.SEMS.ExchangeRate().GetWuExchange(Utility.KillSqlInjection(""), Utility.KillSqlInjection(""), Utility.KillSqlInjection("ALL"), Utility.KillSqlInjection("ALL"), gvApprList.PageSize, gvApprList.PageIndex * gvApprList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE.Equals("0") && dtProcess != null && dtProcess.Tables.Count > 0 && dtProcess.Tables[0].Rows.Count > 0)
            {
                DataTable dtCL = dtProcess.Tables[0];

                if (dtCL.Rows.Count > 0)
                {
                    GridViewPagingControl.Visible = true;
                    ((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = dtCL.Rows[0]["TRECORDCCOUNT"].ToString();
                    gvApprList.Visible = true;
                    gvApprList.DataSource = dtCL;
                    gvApprList.DataBind();
                    ltrError.Text = string.Empty;
                    litPager.Visible = true;
                }
                else
                {
                    GridViewPagingControl.Visible = false;
                    ltrError.Text = "<center><span style='color:red;margin-left:10px; margin-top:10px;font-weight:bold;'>" + Resources.labels.khongtimthaydulieu + "</span></center>";
                    litPager.Visible = false;
                    gvApprList.Visible = false;
                }
            }
            else
            {
                GridViewPagingControl.Visible = false;
                ltrError.Text = "<center><span style='color:red;margin-left:10px; margin-top:10px;font-weight:bold;'>" + Resources.labels.khongtimthaydulieu + "</span></center>";
                litPager.Visible = false;
                gvApprList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dtProcess = new DataSet();
            dtProcess = new SmartPortal.SEMS.ExchangeRate().GetWuExchange(Utility.KillSqlInjection(txtExchangeID.Text.Trim()), Utility.KillSqlInjection(txtExchangeName.Text.Trim()), Utility.KillSqlInjection(ddlCCYID.SelectedItem.ToString().Trim()), Utility.KillSqlInjection(ddlCountry.SelectedValue.Trim()), gvApprList.PageSize, gvApprList.PageIndex * gvApprList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0") && dtProcess != null && dtProcess.Tables.Count > 0 && dtProcess.Tables[0].Rows.Count > 0)
            {
                DataTable dtCL = dtProcess.Tables[0];

                if (dtCL.Rows.Count > 0)
                {
                    GridViewPagingControl.Visible = true;
                    ((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = dtCL.Rows[0]["TRECORDCCOUNT"].ToString();
                    gvApprList.Visible = true;
                    gvApprList.DataSource = dtCL;
                    gvApprList.DataBind();
                    ltrError.Text = string.Empty;
                    litPager.Visible = true;
                }
                else
                {
                    GridViewPagingControl.Visible = false;
                    ltrError.Text = "<center><span style='color:red;margin-left:10px; margin-top:10px;font-weight:bold;'>" + Resources.labels.khongtimthaydulieu + "</span></center>";
                    litPager.Visible = false;
                    gvApprList.Visible = false;
                }
            }
            else
            {
                GridViewPagingControl.Visible = false;
                ltrError.Text = "<center><span style='color:red;margin-left:10px; margin-top:10px;font-weight:bold;'>" + Resources.labels.khongtimthaydulieu + "</span></center>";
                litPager.Visible = false;
                gvApprList.Visible = false;
            }
        }
        catch (Exception ex)

        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {

    }


    protected void gvApprList_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void gvApprList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void gvApprList_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        try
        {
            LinkButton lblExchangeID, lbEdit, lbDelete;
            Label lbtExchangeName, lblAmount, lblCCYID, lbtContrycode, lbtContryname, lblStatus;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                lblExchangeID = (LinkButton)e.Row.FindControl("hpExchangeID");

                lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblCCYID = (Label)e.Row.FindControl("lblCCYID");
                lbtExchangeName = (Label)e.Row.FindControl("lbtExchangeName");
                lbtContrycode = (Label)e.Row.FindControl("lbtContrycode");
                lbtContryname = (Label)e.Row.FindControl("lbtContryname");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lbEdit = (LinkButton)e.Row.FindControl("hpEdit");
                lbDelete = (LinkButton)e.Row.FindControl("hpDelete");
                

                lblExchangeID.Text = drv["ExchangeID"].ToString();
                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["Amount"].ToString(), drv["Currency"].ToString().Trim());
                lblCCYID.Text = drv["Currency"].ToString();
                lbtExchangeName.Text = drv["ExchangeName"].ToString();
                lbtContrycode.Text = drv["CountryCode"].ToString();
                lbtContryname.Text = drv["COUNTRYNAME"].ToString();


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
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        lblStatus.Text = Resources.labels.pendingfordelete;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORAPPROVE:
                        lblStatus.Text = Resources.labels.pendingforapprove;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
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

    protected void btnAdd_New_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }

    protected void gvApprList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvApprList.Rows[e.RowIndex].Cells[1].FindControl("hpExchangeID")).Text;
            new SmartPortal.SEMS.ExchangeRate().DelExchangeRate(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                BindData();
                lblError.Text = Resources.labels.deletesuccessfully;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
