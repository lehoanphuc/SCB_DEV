using System;
using System.Data;
using SmartPortal.Constant;
using System.Web.UI.WebControls;
using SmartPortal.Model;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

public partial class Widgets_SEMSTransactionEnquiry_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    //IList<TransactionEnquiryModel> listTransactionEnquiryModel = new List<TransactionEnquiryModel>();
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    //public List<TransactionEnquiryModel> listTransactionEnquiryModel
    //{
    //    get
    //    {
    //        if (!(ViewState["listTrans"] is List<TransactionEnquiryModel>))
    //        {
    //            // need to fix the memory and added to viewstate
    //            ViewState["listTrans"] = new List<TransactionEnquiryModel>();
    //        }

    //        return (List<TransactionEnquiryModel>)ViewState["listTrans"];
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.ForeColor = System.Drawing.Color.Red;
        try
        {
            lblError.Text = string.Empty;
            //if (!IsPostBack)
            //{
                BindData();
            //}
            GridViewPaging.pagingClickArgs += new EventHandler(btnEnquiry_click);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    void setControlDefault()
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        lblError.Text = string.Empty;
        txtPhoneNumber.Text = string.Empty;
        txtTransaction.Text = string.Empty;
        txtAccNo.Text = string.Empty;
        txtBiller.Text = string.Empty;
        txtContract.Text = string.Empty;
        txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtKYCLevel.Text = string.Empty;
        txtProduct.Text = string.Empty;
        txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtUserid.Text = string.Empty;
        txtWalletLevel.Text = string.Empty;
    }

    void BindData()
    {
        try
        {
            DateTime FromDate = DateTime.Now;
            DateTime ToDate = DateTime.Now;
            txtToDate.Text = ToDate.ToString("dd/MM/yyyy");
            txtFromDate.Text = ToDate.ToString("dd/MM/yyyy");
            if (!txtFromDate.Text.Equals(string.Empty))
            {
                FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null);
            }
            if (!txtFromDate.Text.Equals(string.Empty))
            {
                ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
            }
            
            DataSet ds = new DataSet();
            object[] _object = new object[] { txtPhoneNumber.Text, txtTransaction.getTransactionCode(), FromDate.ToString("yyyy-MM-dd hh:mm:ss"), ToDate.ToString("yyyy-MM-dd hh:mm:ss"), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_TRANS_ENQUIRY", _object, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridViewPaging.Visible = true;
                    //panList.ScrollBars = ScrollBars.Horizontal;
                    rptData.DataSource = ds;
                    rptData.DataBind();
                    GridViewPaging.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
                }
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
    protected void loadInfo(object sender, EventArgs e)
    {
        txtPhoneNumber.BorderColor = System.Drawing.Color.Empty;
        DataSet ds = new DataSet();
        object[] _object = new object[] { txtPhoneNumber.Text };
        ds = _service.common("SEMS_INFO_ALL_CUST", _object, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtProduct.Text = ds.Tables[0].Rows[0]["Product"].ToString();
                    txtContract.Text = ds.Tables[0].Rows[0]["ContractNo"].ToString();
                    txtWalletLevel.Text = ds.Tables[0].Rows[0]["WalletLevel"].ToString();
                    txtKYCLevel.Text = ds.Tables[0].Rows[0]["KYCLevel"].ToString();
                    txtAccNo.Text = ds.Tables[0].Rows[0]["AcctNo"].ToString();
                    txtBiller.Text = string.Empty;//ds.Tables[0].Rows[0]["Product"].ToString();
                    txtUserid.Text = string.Empty;//ds.Tables[0].Rows[0]["Product"].ToString();
                }
                else
                {
                    setControlDefault();
                    lblError.Text = Resources.labels.PhoneNumber + " is incorrect";
                    txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                    txtPhoneNumber.Focus();
                    return;
                }
            }
            else
            {
                setControlDefault();
                lblError.Text = Resources.labels.PhoneNumber + " is incorrect";
                txtPhoneNumber.BorderColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return;
            }
        }
        else
        {
            lblError.Text = IPCERRORDESC;
        }
    }

    private void loadData_Repeater()
    {
        rptData.DataSource = null;
        DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null);
        DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
        DataSet ds = new DataSet();
        object[] _object = new object[] { txtPhoneNumber.Text, txtTransaction.getTransactionCode(), FromDate.ToString("yyyy-MM-dd hh:mm:ss"), ToDate.ToString("yyyy-MM-dd hh:mm:ss"), GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
        ds = _service.common("SEMS_TRANS_ENQUIRY", _object, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables.Count > 0)
            {
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    TransactionEnquiryModel item = new TransactionEnquiryModel();
                //    item.TransactionNumber = ds.Tables[0].Rows[i]["TransactionNumber"].ToString();
                //    item.TransactionDate = ds.Tables[0].Rows[i]["TransactionDate"].ToString();
                //    item.TransactionName = ds.Tables[0].Rows[i]["TransactionName"].ToString();
                //    item.PhoneNumber = ds.Tables[0].Rows[i]["PhoneNumber"].ToString();
                //    item.FullName = ds.Tables[0].Rows[i]["FullName"].ToString();
                //    item.Amount = ds.Tables[0].Rows[i]["Amount"].ToString();
                //    item.Bonus = ds.Tables[0].Rows[i]["Bonus"].ToString();
                //    item.AmountFee = ds.Tables[0].Rows[i]["AmountFee"].ToString();
                //    item.Currency = ds.Tables[0].Rows[i]["Currency"].ToString();
                //    item.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                //    listTransactionEnquiryModel.Add(item);
                //}
                GridViewPaging.Visible = true;
                //rptData.DataSource = listTransactionEnquiryModel;
                rptData.DataSource = ds;
                rptData.DataBind();
                GridViewPaging.total = ds.Tables[0].Rows.Count == 0 ? "0" : ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
        }
        else
        {
            lblError.Text = IPCERRORDESC;
        }
    }
    protected void btnEnquiry_click(object sender, EventArgs e)
    {
        try
        {
            loadData_Repeater();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnClear_click(object sender, EventArgs e)
    {
        setControlDefault();
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        //if (CheckPermitPageAction(commandName))
        //{
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    HtmlGenericControl lblByUser = (HtmlGenericControl)e.Item.FindControl("lblByUser");
                    HtmlGenericControl lblBiller = (HtmlGenericControl)e.Item.FindControl("lblBiller");
                    txtUserid.Text = lblByUser.InnerText;
                    txtBiller.Text = lblBiller.InnerText;
                    break;
            }
        //}
    }
    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (rptData.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblFooter = (Label)e.Item.FindControl("lblErrorMsg");
                lblFooter.Visible = true;
            }
        }
    }
}