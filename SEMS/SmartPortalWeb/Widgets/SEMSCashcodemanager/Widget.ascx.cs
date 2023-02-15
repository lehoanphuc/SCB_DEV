using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSCashcodemanager_Widget : WidgetBase
{
    public static bool isAscend = false;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                txtCreateFromDate.Text = string.IsNullOrEmpty(txtCreateFromDate.Text) ? DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy") : txtCreateFromDate.Text;
                txtCreateTodate.Text = string.IsNullOrEmpty(txtCreateTodate.Text) ? DateTime.Now.ToString("dd/MM/yyyy") : txtCreateTodate.Text;
                LoadDll();
                GridViewPaging.Visible = false;
                divResult.Visible = false;
            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvCashCode.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvCashCode.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void LoadDll()
    {
        ccyid.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
        ccyid.DataTextField = "CCYID";
        ccyid.DataValueField = "CCYID";
        ccyid.DataBind();
    }
    private void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.User().GetAllCashCodeByCondition(Utility.KillSqlInjection(tranid.Text.Trim()),
                ccyid.SelectedValue, Utility.KillSqlInjection(txtTel.Text.Trim()),
                Utility.KillSqlInjection(txtSendername.Text.Trim()), Utility.KillSqlInjection(txtCreateFromDate.Text.Trim()),
                Utility.KillSqlInjection(txtCreateTodate.Text.Trim()), txtStatus.SelectedValue, gvCashCode.PageSize, gvCashCode.PageIndex * gvCashCode.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvCashCode.DataSource = ds;
                gvCashCode.DataBind();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvCashCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblSenderPhone, lblSenderName, lblSenderAmount, lblPaidAmount, lblCCYID, lblcreatedate, lblstatus;
            LinkButton lblTranNo, lbResend, lbCancel;
            DataRowView drv; 
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                lblTranNo = (LinkButton)e.Row.FindControl("lblTranNo");
                lblSenderPhone = (Label)e.Row.FindControl("lblSenderPhone");
                lblSenderName = (Label)e.Row.FindControl("lblSenderName");
                lblSenderAmount = (Label)e.Row.FindControl("lblSenderAmount");
                lblPaidAmount = (Label)e.Row.FindControl("lblPaidAmount");
                lblCCYID = (Label)e.Row.FindControl("lblCCYID");
                lblcreatedate = (Label)e.Row.FindControl("lblcreatedate");
                lblstatus = (Label)e.Row.FindControl("lblstatus");
                lbResend = (LinkButton)e.Row.FindControl("lbResend");
                lbCancel = (LinkButton)e.Row.FindControl("lbCancel");

                lblTranNo.Text = drv["TXREF"].ToString();
                lblSenderPhone.Text = drv["SENDERPHONE"].ToString();
                lblSenderName.Text = drv["SENDERNAME"].ToString();
                lblSenderAmount.Text = Utility.FormatMoney(drv["SENDERAMT"].ToString(), drv["CURRENCYID"].ToString());
                lblPaidAmount.Text = Utility.FormatMoney(drv["PAIDAMT"].ToString(), drv["CURRENCYID"].ToString());
                lblCCYID.Text = drv["CURRENCYID"].ToString();
                lblcreatedate.Text = Convert.ToDateTime(drv["DATECREATED"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                switch (drv["STATUS"].ToString().Trim().ToUpper())
                {
                    case "N":
                        lblstatus.Text = Resources.labels.notyetpaid;
                        lblstatus.Attributes.Add("class", "label-warning");

                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.REJECT))
                        {
                            lbCancel.Enabled = false;
                            lbCancel.OnClientClick = string.Empty;
                            lbResend.Enabled = false;
                            lbResend.OnClientClick = string.Empty;
                        }
                        break;
                    case "P":
                        lblstatus.Text = Resources.labels.partialpaid;
                        lblstatus.Attributes.Add("class", "label-success");
                        lbCancel.Enabled = false;
                        lbCancel.OnClientClick = string.Empty;
                        lbResend.Enabled = false;
                        lbResend.OnClientClick = string.Empty;
                        break;
                    case "F":
                        lblstatus.Text = Resources.labels.fullypaid;
                        lblstatus.Attributes.Add("class", "label-success");
                        lbCancel.Enabled = false;
                        lbCancel.OnClientClick = string.Empty;
                        lbResend.Enabled = false;
                        lbResend.OnClientClick = string.Empty;
                        break;
                    case "D":
                        lblstatus.Text = Resources.labels.condelete;
                        lblstatus.Attributes.Add("class", "label-warning");
                        lbCancel.Enabled = false;
                        lbCancel.OnClientClick = string.Empty;
                        lbResend.Enabled = false;
                        lbResend.OnClientClick = string.Empty;
                        break;
                    case "E":
                        lblstatus.Text = Resources.labels.Expried;
                        lblstatus.Attributes.Add("class", "label-warning");
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.REJECT))
                        {
                            lbCancel.Enabled = false;
                            lbCancel.OnClientClick = string.Empty;
                            lbResend.Enabled = false;
                            lbResend.OnClientClick = string.Empty;
                        }
                        break;
                    case "L":
                        lblstatus.Text = Resources.labels.locked;
                        lblstatus.Attributes.Add("class", "label-warning");
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.REJECT))
                        {
                            lbCancel.Enabled = false;
                            lbCancel.OnClientClick = string.Empty;
                        }
                        lbResend.Enabled = false;
                        lbResend.OnClientClick = string.Empty;
                        break;
                    case "C":
                        lblstatus.Text = Resources.labels.canceled;
                        lblstatus.Attributes.Add("class", "label-warning");
                        lbCancel.Enabled = false;
                        lbCancel.OnClientClick = string.Empty;
                        lbResend.Enabled = false;
                        lbResend.OnClientClick = string.Empty;
                        break;
                }
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
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvCashCode.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvCashCode.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvCashCode_RowCommand(object sender, GridViewCommandEventArgs e)
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
                case IPC.ACTIONPAGE.REJECT:
                    RedirectToActionPage(IPC.ACTIONPAGE.REJECT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }
}