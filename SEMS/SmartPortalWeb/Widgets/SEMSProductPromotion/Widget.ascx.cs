using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SmartPortal.BLL;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;
public partial class Widgets_SEMSProductPromotionManager_Widget : WidgetBase
{
    public static bool isAscend = false;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    private int size = 0;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                LoadddlAll();
                GridViewPaging.Visible = false;
                divResult.Visible = false;
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
        gvProductDiscount.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvProductDiscount.PageIndex = Convert.ToInt32(( (TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }

    void LoadddlAll()
    {
        loadCombobox_WalletLevel();
        loadcombobox_Transaction();
        loadCombobox_ScheduleType();
        loadCombobox_DiscountType();
        loadCombobox_Status();
        loadProduct();
        loadCombobox_PromotionType();
    }
    void BindData()
    {
        try
        {
            divResult.Visible = true;
            ltrError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvProductDiscount.PageIndex * gvProductDiscount.PageSize) return;
            DataSet dsProcess = new DataSet();
            DataSet dsProductDiscount = new DataSet();
            dsProductDiscount = new SmartPortal.SEMS.PROMOTION().GetPromotionList(Utility.KillSqlInjection(ddlProduct.SelectedValue), Utility.KillSqlInjection(ddlContractLevel.SelectedValue.ToString()), Utility.KillSqlInjection(ddlPromotionSide.SelectedValue), Utility.KillSqlInjection(ddlTransactionType.SelectedValue), Utility.KillSqlInjection(ddlscheduleType.SelectedValue), Utility.KillSqlInjection(ddlStatus.SelectedValue), Utility.KillSqlInjection(ddlpromotiontype.SelectedValue), "P", Utility.KillSqlInjection(txtPromotionName.Text), gvProductDiscount.PageSize, gvProductDiscount.PageIndex * gvProductDiscount.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            //dsProcess = new SmartPortal.SEMS.Fee().SearchFee(Utility.KillSqlInjection(txtProductName.Text.Trim()), Utility.KillSqlInjection(ddlFeeType.SelectedValue.ToString()), Utility.KillSqlInjection(txtAmount.Text.Trim().Replace(",", "")), Utility.KillSqlInjection(ddlCCYID.SelectedValue), gvProductDiscount.PageSize, gvProductDiscount.PageIndex * gvProductDiscount.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvProductDiscount.DataSource = dsProductDiscount;
                gvProductDiscount.DataBind();
            }
            if (dsProductDiscount.Tables[0].Rows.Count > 0)
            {
                ltrError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dsProductDiscount.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
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

    private void loadCombobox_WalletLevel()
    {
        DataSet ds = new DataSet();
        object[] loadContractLevel = new object[] { string.Empty };
        ds = _service.common("SEMS_BO_LST_CON_LV", loadContractLevel, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlContractLevel.DataSource = ds;
                ddlContractLevel.DataValueField = "CONTRACTLEVELID";
                ddlContractLevel.DataTextField = "CONTRACTLEVELNAME";
                ddlContractLevel.DataBind();
                ddlContractLevel.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
            }
        }
    }
    private void loadCombobox_ScheduleType()
    {
        ddlscheduleType.Items.Clear();
        ddlscheduleType.DataBind();
        ddlscheduleType.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
        ddlscheduleType.Items.Insert(1, new ListItem("Onetime", "ONETIME"));
        ddlscheduleType.Items.Insert(2, new ListItem("Daily", "DAILY"));
        ddlscheduleType.Items.Insert(3, new ListItem("Monthly", "MONTHLY"));
        ddlscheduleType.Items.Insert(4, new ListItem("Weekly", "WEEKLY"));
        //ddlscheduleType.Items.Insert(5, new ListItem("Annual", "ANNUAL"));
    }
    private void loadCombobox_DiscountType()
    {
        ddlPromotionSide.Items.Clear();
        ddlPromotionSide.DataBind();
        ddlPromotionSide.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
        ddlPromotionSide.Items.Insert(1, new ListItem("Recevier", "R"));
        ddlPromotionSide.Items.Insert(2, new ListItem("Sender", "S"));
    }
    private void loadCombobox_Status()
    {
        ddlStatus.Items.Clear();
        ddlStatus.DataBind();
        ddlStatus.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
        ddlStatus.Items.Insert(1, new ListItem("New", "N"));
        ddlStatus.Items.Insert(2, new ListItem("Active", "A"));
        ddlStatus.Items.Insert(3, new ListItem("Pending", "P"));
        ddlStatus.Items.Insert(4, new ListItem("Deleted", "D"));
        ddlStatus.Items.Insert(5, new ListItem("Rejected", "R"));
        ddlStatus.Items.Insert(6, new ListItem("Inactive", "I"));
        ddlStatus.Items.Insert(7, new ListItem("Pending  For Deleted", "H"));
    }
    private void loadCombobox_PromotionType()
    {
        ddlpromotiontype.Items.Clear();
        ddlpromotiontype.DataBind();
        ddlpromotiontype.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
        //ddlpromotiontype.Items.Insert(1, new ListItem("Discount", "D"));
        ddlpromotiontype.Items.Insert(1, new ListItem("CashBack", "C"));
    }

    private void loadProduct()
    {
        ddlProduct.DataSource = new SmartPortal.SEMS.Product().GetProductByCondition("", "", "P", "", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            throw new IPCException(IPCERRORDESC);
        }

        ddlProduct.DataTextField = "PRODUCTNAME";
        ddlProduct.DataValueField = "PRODUCTID";
        ddlProduct.DataBind();
        ddlProduct.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
    }

    private void loadcombobox_Transaction()
    {
        DataSet dsTranApp = new SmartPortal.SEMS.Transactions().LoadTranApp(ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
        }
        DataTable dtTranApp = new DataTable();
        dtTranApp = dsTranApp.Tables[0];

        ddlTransactionType.DataSource = dtTranApp;
        ddlTransactionType.DataTextField = "PAGENAME";
        ddlTransactionType.DataValueField = "TRANCODE";
        ddlTransactionType.DataBind();
        ddlTransactionType.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
    }

    protected void gvProductDiscount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton hpDiscountName, lbEdit, lbDelete, lblClone;
            Label lblTranName, lblDiscountDes, lblFromdate, lblTodate, lblFromtime, lblToTime, lblScheduletype, lblAmount, lblCurrency, lblStatus, lblLadder, lblContractLevel, lblPromotionType;
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
                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");

                //if (drv["branchid"].ToString().Trim() != Session["branch"].ToString().Trim())
                //{
                //    cbxSelect.Enabled = false;
                //}

                hpDiscountName = (LinkButton)e.Row.FindControl("hpDiscountName");
                lblDiscountDes = (Label)e.Row.FindControl("lblDiscountDes");
                lblTranName = (Label)e.Row.FindControl("lblTranName");
                lblContractLevel = (Label)e.Row.FindControl("lblContractLevel");
                lblFromdate = (Label)e.Row.FindControl("lblFromdate");
                lblTodate = (Label)e.Row.FindControl("lblTodate");
                lblFromtime = (Label)e.Row.FindControl("lblFromtime");
                lblToTime = (Label)e.Row.FindControl("lblToTime");
                lblScheduletype = (Label)e.Row.FindControl("lblScheduletype");
                lblCurrency = (Label)e.Row.FindControl("lblCurrency");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                //lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblLadder = (Label)e.Row.FindControl("lblLadder");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
                lblClone = (LinkButton)e.Row.FindControl("lblClone");
               // lblPromotionType = (Label)e.Row.FindControl("lblPromotionType");

                hpDiscountName.Text = drv["ProductName"].ToString();
                lblTranName.Text = drv["PageName"].ToString();
                lblDiscountDes.Text = drv["PromotionName"].ToString();
               //lblPromotionType.Text = drv["PromotionType"].ToString();


                DateTime dateValue = new DateTime();
                string formatedDateFrom = "";
                if (DateTime.TryParse(drv["ToDate"].ToString(), out dateValue))
                {
                    formatedDateFrom = dateValue.ToString("dd/MM/yyyy");
                }
                DateTime dateValue1 = new DateTime();
                string formatedDateTo = "";
                if (DateTime.TryParse(drv["FromDate"].ToString(), out dateValue1))
                {
                    formatedDateTo = dateValue1.ToString("dd/MM/yyyy");
                }

                lblTodate.Text = formatedDateFrom;
                lblFromdate.Text = formatedDateTo;

                lblFromtime.Text = drv["FromTime"].ToString();
                lblToTime.Text = drv["ToTime"].ToString();
                lblScheduletype.Text = drv["TimeType"].ToString();
                //lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["FixAmt"].ToString(), drv["Currency"].ToString().Trim());
                lblCurrency.Text = drv["Currency"].ToString();
                //ddlContractLevel.selecte
                lblContractLevel.Text = ddlContractLevel.Items.FindByValue(drv["ContractLevel"].ToString()).ToString();

                if (drv["IsTier"].ToString() == "True")
                {
                    lblLadder.Text = "<img src='Images/check.png' style='width: 20px; height: 20px; margin-bottom:1px;'/>";
                }
                if (drv["IsTier"].ToString() == "False")
                {
                    lblLadder.Text = "<img src='Images/nocheck.png'style='width: 20px; height: 20px; margin-bottom:1px;'/>";
                }
                switch (drv["Status"].ToString())
                {
                    case "I":
                        lblStatus.Text = Resources.labels.inactive;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case "A":
                        lblStatus.Text = Resources.labels.conactive;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case "N":
                        lblStatus.Text = Resources.labels.connew;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case "P":
                        cbxSelect.Enabled = false;
                        lbEdit.Enabled = false;
                        lbDelete.Enabled = false;
                        lblStatus.Text = Resources.labels.conpending;
                        lblStatus.Attributes.Add("class", "label-warning");
                        cbxSelect.Enabled = false;
                        lbDelete.Enabled = false;
                        lblClone.Enabled = false;
                        break;
                    case "R":
                        lblStatus.Text = Resources.labels.conreject;
                        lblStatus.Attributes.Add("class", "label-warning");
                        cbxSelect.Enabled = false;
                        lblClone.Enabled = false;
                        break;
                    case "D":
                        cbxSelect.Enabled = false;
                        lbEdit.Enabled = false;
                        lbDelete.Enabled = false;
                        lblStatus.Text = Resources.labels.condelete;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case "H":
                        cbxSelect.Enabled = false;
                        lbEdit.Enabled = false;

                        lbDelete.Enabled = false;
                        lblStatus.Text = Resources.labels.pendingfordelete;
                        lblStatus.Attributes.Add("class", "label-warning");
                        cbxSelect.Enabled = false;
                        lbDelete.Enabled = false;
                        lblClone.Enabled = false;
                        break;
                    default:
                        cbxSelect.Enabled = false;
                        lbEdit.Enabled = false;

                        lblStatus.Text = drv["Status"].ToString();
                        lblStatus.Attributes.Add("class", "label-success");
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
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
    }
    protected void gvProductDiscount_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg + "&IsClone=false");
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg + "&IsClone=false");
                    break;
                case "CLONE":
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg + "&IsClone=true");
                    break;
            }
        }

        if (commandName == "CLONE")
        {
            RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg + "&IsClone=true");
        }
    }
    protected void gvProductDiscount_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            string status = "H";
            commandArg = ((LinkButton)gvProductDiscount.Rows[e.RowIndex].Cells[1].FindControl("hpDiscountName")).CommandArgument;
            //status = ((Label)gvProductDiscount.Rows[e.RowIndex].Cells[1].FindControl("lblStatus")).Text;
            //status = status == "NEW" ? "D" : "H";
            //new SmartPortal.SEMS.PROMOTION().DeletePromotion(commandArg, status, ref IPCERRORCODE, ref IPCERRORDESC);
            new SmartPortal.SEMS.PROMOTION().DeletePromotion(commandArg, "DELETE", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData2();
                lblError.Text = Resources.labels.xoapromotionproduct;
            }
            else
            {
                string errorCode = string.Empty;
                ErrorCodeModel EM = new ErrorCodeModel();
                if (IPCERRORDESC == "110211")
                {
                    errorCode = IPC.ERRORCODE.ACTIVEFEE;
                }
                else
                {
                    errorCode = IPC.ERRORCODE.IPC;
                }
                EM = new ErrorBLL().Load(Utility.IsInt(errorCode), System.Globalization.CultureInfo.CurrentCulture.ToString());
                lblError.Text = EM.ErrorDesc;
                return;
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
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            CheckBox cbxDelete;
            LinkButton hpDiscountName;
            string status = "H";
            Label lblStatus;
            string strdistcountid = "";
            try
            {
                foreach (GridViewRow gvr in gvProductDiscount.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        hpDiscountName = (LinkButton)gvr.Cells[1].FindControl("hpDiscountName");
                        strdistcountid += hpDiscountName.CommandArgument.Trim() + "#";
                        lblStatus = (Label)gvr.Cells[1].FindControl("lblStatus");
                        status = lblStatus.Text == "New" ? "D" : "H";
                    }
                }
                if (string.IsNullOrEmpty(strdistcountid))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] DiscountidList = strdistcountid.Split('#');
                    for (int i = 0; i < DiscountidList.Length - 1; i++)
                    {
                        //new SmartPortal.SEMS.PROMOTION().DeletePromotion(DiscountidList[i], status, ref IPCERRORCODE, ref IPCERRORDESC);
                        new SmartPortal.SEMS.PROMOTION().DeletePromotion(DiscountidList[i],"DELETE", ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            lblError.Text = Resources.labels.xoapromotionproduct;
                        }
                        else
                        {
                            string errorCode = string.Empty;
                            ErrorCodeModel EM = new ErrorCodeModel();
                            if (IPCERRORDESC == "110211")
                            {
                                errorCode = IPC.ERRORCODE.ACTIVEFEE;
                            }
                            else
                            {
                                errorCode = IPC.ERRORCODE.IPC;
                            }
                            EM = new ErrorBLL().Load(Utility.IsInt(errorCode), System.Globalization.CultureInfo.CurrentCulture.ToString());
                            lblError.Text = EM.ErrorDesc;
                            return;
                        }
                    }
                    BindData2();

                }
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
            }
        }
    }
    void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvProductDiscount.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvProductDiscount.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnInactive_OnClick(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        LinkButton hpDiscountName;
        Label lblStatus;
        string status = "I";
        List<string> listStatus = new List<string>();
        string strdistcountid = "";
        try
        {
            foreach (GridViewRow gvr in gvProductDiscount.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpDiscountName = (LinkButton)gvr.Cells[1].FindControl("hpDiscountName");
                    strdistcountid += hpDiscountName.CommandArgument.Trim() + "#";
                    lblStatus = (Label)gvr.Cells[1].FindControl("lblStatus");
                    string statusf = lblStatus.Text == "Active" ? "I" : "A";
                    listStatus.Add(statusf);
                    if (lblStatus.Text.Equals("Active") || lblStatus.Text.Equals("InActive"))
                    {

                    }
                    else
                    {
                        lblError.Text = "Invalid status";
                        return;
                    }
                }
            }
            if (string.IsNullOrEmpty(strdistcountid))
            {
                lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                return;
            }
            else
            {
                string[] DiscountidList = strdistcountid.Split('#');
                for (int i = 0; i < DiscountidList.Length - 1; i++)
                {
                    status = listStatus[i];
                    //new SmartPortal.SEMS.PROMOTION().DeletePromotion(DiscountidList[i], status, ref IPCERRORCODE, ref IPCERRORDESC);
                    new SmartPortal.SEMS.PROMOTION().DeletePromotion(DiscountidList[i], "INACTIVE", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0"))
                    {
                        lblError.Text = Resources.labels.inactiveproductpromotionthanhcong;
                    }
                    else
                    {
                        string errorCode = string.Empty;
                        ErrorCodeModel EM = new ErrorCodeModel();
                        if (IPCERRORDESC == "110211")
                        {
                            errorCode = IPC.ERRORCODE.ACTIVEFEE;
                        }
                        else
                        {
                            errorCode = IPC.ERRORCODE.IPC;
                        }
                        EM = new ErrorBLL().Load(Utility.IsInt(errorCode), System.Globalization.CultureInfo.CurrentCulture.ToString());
                        lblError.Text = EM.ErrorDesc;
                        return;
                    }
                }
                BindData2();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
