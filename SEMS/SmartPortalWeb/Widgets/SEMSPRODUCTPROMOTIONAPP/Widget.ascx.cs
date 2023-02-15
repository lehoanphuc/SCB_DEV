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
using Newtonsoft.Json;
public partial class Widgets_SEMSProductPromotionApp_Widget : WidgetBase
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
                btnApprove.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE);
                btnReject.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.REJECT);
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
        gvProductDiscount.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }

    void LoadddlAll()
    {
        loadProduct();
        loadCombobox_WalletLevel();
        loadcombobox_Transaction();
        loadCombobox_ScheduleType();
        loadCombobox_DiscountType();
        loadCombobox_Status();
        loadCombobox_PromotionType();
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
    private void loadCombobox_PromotionType()
    {
        ddlpromotiontype.Items.Clear();
        ddlpromotiontype.DataBind();
        ddlpromotiontype.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
        //ddlpromotiontype.Items.Insert(1, new ListItem("Discount", "D"));
        ddlpromotiontype.Items.Insert(1, new ListItem("CashBack", "C"));
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
            dsProductDiscount = new SmartPortal.SEMS.PROMOTION().GetPromotionList(Utility.KillSqlInjection(ddlProduct.SelectedValue), Utility.KillSqlInjection(ddlContractLevel.SelectedValue.ToString()),
                Utility.KillSqlInjection(ddlPromotionSide.SelectedValue), Utility.KillSqlInjection(ddlTransactionType.SelectedValue), Utility.KillSqlInjection(ddlscheduleType.SelectedValue), Utility.KillSqlInjection(ddlStatus.SelectedValue), Utility.KillSqlInjection(ddlpromotiontype.SelectedValue), "P", Utility.KillSqlInjection(txtPromotionName.Text), gvProductDiscount.PageSize, gvProductDiscount.PageIndex * gvProductDiscount.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            //dsProductDiscount.Tables[0].DefaultView.RowFilter = "STATUS NOT IN ( 'A', 'D', 'I', 'R') ";
			//SmartPortal.Common.Log.WriteLogFile("ProductApprove", "abcc", "abc", JsonConvert.SerializeObject(dsProductDiscount.Tables[0].DefaultView));
            if (IPCERRORCODE == "0")
            {
                gvProductDiscount.DataSource = dsProductDiscount.Tables[0];
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

    }
    private void loadCombobox_DiscountType()
    {
        ddlPromotionSide.Items.Clear();
        ddlPromotionSide.DataBind();
        ddlPromotionSide.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
        ddlPromotionSide.Items.Insert(1, new ListItem("Recevier", "RECEVIER"));
        ddlPromotionSide.Items.Insert(2, new ListItem("Sender", "SENDER"));
    }
    private void loadCombobox_Status()
    {
        ddlStatus.Items.Clear();
        ddlStatus.DataBind();
        ddlStatus.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
        ddlStatus.Items.Insert(1, new ListItem("New", "N"));
        //ddlStatus.Items.Insert(2, new ListItem("Active", "A"));
        ddlStatus.Items.Insert(2, new ListItem("Pending", "P"));
        //ddlStatus.Items.Insert(4, new ListItem("Deleted", "D"));
        //ddlStatus.Items.Insert(5, new ListItem("Rejected", "R"));
        // ddlStatus.Items.Insert(6, new ListItem("Inactive", "I"));
        ddlStatus.Items.Insert(3, new ListItem("Pending  For Deleted", "H"));
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
            Label lblTranName, lblDiscountDes, lblFromdate, lblTodate, lblFromtime, lblToTime, lblScheduletype, lblAmount, lblCurrency, lblStatus, lblLadder, lblContractLevel, lbldiscountID, lblCreatedDate, lblCreatedUser, lblApprovedate, lblApproveuser;
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
                lblCreatedDate = (Label)e.Row.FindControl("lblCreatedDate");
                lblCreatedUser = (Label)e.Row.FindControl("lblCreatedUser");
                lblApprovedate = (Label)e.Row.FindControl("lblApprovedate");
                lblApproveuser = (Label)e.Row.FindControl("lblApproveuser");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
                lblClone = (LinkButton)e.Row.FindControl("lblClone");
                lbldiscountID = (Label)e.Row.FindControl("lbldiscountID");

                hpDiscountName.Text = drv["ProductName"].ToString();
                lblTranName.Text = drv["PageName"].ToString();
                lblDiscountDes.Text = drv["PromotionName"].ToString();
                

                lbldiscountID.Text = drv["PromotionID"].ToString();

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
                lblCreatedDate.Text =  SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["CreatedDate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                lblCreatedUser.Text = drv["CreatedUser"].ToString();
                if (!String.IsNullOrEmpty(drv["DateApprove"].ToString()))
                {
                    lblApprovedate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["DateApprove"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                }
                else
                {
                    lblApprovedate.Text = drv["DateApprove"].ToString();
                }
                
                lblApproveuser.Text = drv["UserApprove"].ToString();

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
                        //inactive
                        cbxSelect.Enabled = false;
                        lblStatus.Text = Resources.labels.inactive;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case "A":
                        //active
                        cbxSelect.Enabled = false;
                        lblStatus.Text = Resources.labels.conactive;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case "N":
                        //new
                        cbxSelect.Enabled = true;
                        lblStatus.Text = Resources.labels.connew;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case "P":
                        //pending
                        cbxSelect.Enabled = true;
                        lblStatus.Text = Resources.labels.conpending;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case "R":
                        //reject
                        lblStatus.Text = Resources.labels.conreject;
                        lblStatus.Attributes.Add("class", "label-warning");
                        cbxSelect.Enabled = false;
                        break;
                    case "D":
                        //deleted
                        cbxSelect.Enabled = false;
                        lblStatus.Text = Resources.labels.condelete;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case "H":
                        //pending for delete
                        cbxSelect.Enabled = true;
                        lblStatus.Text = Resources.labels.pendingfordelete;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    default:
                        cbxSelect.Enabled = false;
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
                case IPC.ACTIONPAGE.REVIEW:
                    RedirectToActionPage(IPC.ACTIONPAGE.REVIEW, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg + "&IsClone=false");
                    break;
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

    protected void btnReject_OnClick(object sender, EventArgs e)
    {

        CheckBox cbxSelect;
        Label hpDiscountName;
        List<string> lstTran = new List<string>();
        try
        {
            foreach (GridViewRow gvr in gvProductDiscount.Rows) 
            {
                cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxSelect.Checked == true)
                {
                    hpDiscountName = (Label)gvr.Cells[0].FindControl("lbldiscountID");
                    lstTran.Add(hpDiscountName.Text.Trim());
                }
            }
        }
        catch (Exception ex)
        {

        }
        if (lstTran.Count != 0)
        {
            Session["ListRejectProduct"] = lstTran;
            if (CheckPermitPageAction(IPC.ACTIONPAGE.REJECT))
                RedirectToActionPage(IPC.ACTIONPAGE.REJECT, "&returnURL" + "=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query));
        }
        else
        {
            lblError.Text = Resources.labels.youmustchoosecontracttoreject;
            BindData();
        }
    }
    protected void btnApprove_OnClick(object sender, EventArgs e)
    {
        CheckBox cbxSelect;
        Label hpDiscountName;
        List<string> lstTran = new List<string>();
        try
        {
            foreach (GridViewRow gvr in gvProductDiscount.Rows)
            {
                cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxSelect.Checked == true)
                {
                    hpDiscountName = (Label)gvr.Cells[0].FindControl("lbldiscountID");
                    lstTran.Add(hpDiscountName.Text.Trim());
                }
            }
        }
        catch (Exception ex)
        {

        }
        if (lstTran.Count != 0)
        {
            Session["ListApproveProduct"] = lstTran;
            if (CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE))
                RedirectToActionPage(IPC.ACTIONPAGE.APPROVE, "&returnURL" + "=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query));
        }
        else
        {
            lblError.Text = Resources.labels.youmustchoosecontracttoreject;
            BindData();
        }
    }
}
