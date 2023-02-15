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
using SmartPortal.ExceptionCollection;
using System.Globalization;
using SmartPortal.Constant;
using SmartPortal.SEMS;

public partial class Widgets_SEMSinternational_Widget : WidgetBase
{
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                divresult1.Visible = false;
                GridViewPaging.Visible = false;
                loadinit();
				txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
				txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
    void loadCCYID()
    {
        try
        {
            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            hasInput = new Hashtable(){
                {"IPCTRANCODE", "SEMS_SWIFTCCYID"},
                {"SERVICEID", "SEMS"},
                {"SOURCEID", "SEMS"}
            };
            hasOutput = new SmartPortal.SEMS.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
            DataSet dt = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];

            dllCCYID.DataSource = dt.Tables[0];
            dllCCYID.DataTextField = "Currency";
            dllCCYID.DataValueField = "Currency";
            dllCCYID.DataBind();
            dllCCYID.Items.Insert(0, new ListItem("ALL", "ALL"));
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    void loadAllBank()
    {
        try
        {
            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            hasInput = new Hashtable(){
                {"IPCTRANCODE", "SWIFT_GETALLBANK"},
                {"SERVICEID", "SEMS"},
                {"SOURCEID", "SEMS"}
            };
            hasOutput = new SmartPortal.SEMS.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
            DataSet dt = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];

            ddlBankName.DataSource = dt.Tables[0];
            ddlBankName.DataTextField = "BankName";
            ddlBankName.DataValueField = "SwiftCode";
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("ALL", "ALL"));
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    void loadinit()
    {
        loadCCYID();
        loadAllBank();
    }

    void BinData()
    {
        int pageSize = gvLTWA.PageSize;
        int pageIndex = gvLTWA.PageIndex;
        int recordIndex = pageSize * pageIndex;
        divresult1.Visible = true;

        Hashtable hasInput = new Hashtable();
        Hashtable hasOutput = new Hashtable();

        hasInput = new Hashtable(){
                {"IPCTRANCODE", "SEMS_GETSWIFTTRANS"},
                {"SERVICEID", "SEMS"},
                {SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE},
                { "TRANSID", Utility.KillSqlInjection(txtTranNo.Text.Trim())},
                {"STATUS", Utility.KillSqlInjection(ddlStatus.SelectedValue)},
                {"FROMDATE", Utility.KillSqlInjection(txtTranDate.Text.Trim())},
                {"TODATE", Utility.KillSqlInjection(txttodate.Text.Trim())},
                {"SWIFTCODE", Utility.KillSqlInjection(ddlBankName.SelectedValue)},
                {"CCYID", Utility.KillSqlInjection(dllCCYID.SelectedValue)},
                {"SENDERPHONE", Utility.KillSqlInjection(txtsenderphone.Text.Trim())},
                {"PAGESIZE", pageSize},
                {"PAGEINDEX", pageIndex}
            };
        hasOutput = new SmartPortal.SEMS.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
        if(IPCERRORCODE == "0")
        {
            DataSet dt = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
            DataTable dtTran = dt.Tables[0];
            gvLTWA.DataSource = dtTran;
            gvLTWA.DataBind();
            GridViewPaging.Visible = true;
            if (dtTran.Rows.Count > 0)
            {
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dt.Tables[0].Rows[0]["RECORDCOUNT"].ToString();
                ltrError.Text = string.Empty;
            }
            else
            {
                ltrError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        else
        {
            lblError.Text = IPCERRORDESC;
        }
    }



    protected void gvLTWA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();

        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.DELETE:
                    RedirectToActionPage(IPC.ACTIONPAGE.DELETE, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }

    protected void gvLTWA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            LinkButton lbTranID, lbEdit;
            Label lblDate, lbAmountsend, lbSendCCYID, lblSwiftCode, lblSenderphone, lblStatus, lblBankName, lblSenderIDType, lblSenderIDNumber;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                lbTranID = (LinkButton)e.Row.FindControl("lbTranID");
                lblDate = (Label)e.Row.FindControl("lblDate");
                lbAmountsend = (Label)e.Row.FindControl("lbAmountsend");
                lbSendCCYID = (Label)e.Row.FindControl("lbSendCCYID");
                lblSwiftCode = (Label)e.Row.FindControl("lblSwiftCode");
                lblSenderphone = (Label)e.Row.FindControl("lblSenderphone");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblBankName = (Label)e.Row.FindControl("lblBankName");
                //lblReceiverAccount = (Label)e.Row.FindControl("lblReceiverAccount");

                lblSenderIDType = (Label)e.Row.FindControl("lblSenderIDType");
                lblSenderIDNumber = (Label)e.Row.FindControl("lblSenderIDNumber");
                //lblSenderAccount = (Label)e.Row.FindControl("lblSenderAccount");
                //lblSenderName = (Label)e.Row.FindControl("lblSenderName");
                //lblTransref = (Label)e.Row.FindControl("lblTransref");


                lbTranID.Text = drv["TransactionNo"].ToString();
                lblDate.Text = drv["TransactionDate"].ToString();

                lbAmountsend.Text = drv["Amount"].ToString();
                lbSendCCYID.Text = drv["Currency"].ToString();
                lblSwiftCode.Text = drv["SwiftCode"].ToString();
                lblSenderphone.Text = drv["MobilePhone"].ToString();
                lblStatus.Text = drv["Status"].ToString();
                lblBankName.Text = drv["BankName"].ToString();
                //lblReceiverAccount.Text = drv["MobilePhone"].ToString();

                lblSenderIDType.Text = drv["IDType"].ToString();
                lblSenderIDNumber.Text = drv["IDNumber"].ToString();
                //lblSenderAccount.Text = drv["SenderAccount"].ToString();
                //lblSenderName.Text = drv["SenderName"].ToString();
                //lblTransref.Text = drv["TransactionRef"].ToString();
                lbEdit = (LinkButton)e.Row.FindControl("hpEdit");

                switch (drv["STATUS"].ToString().Trim())
                {
                    case "A":
                        lblStatus.Text = Resources.labels.send;
                        lbEdit.Text = "Resend";
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case "P":
                        lblStatus.Text = Resources.labels.conpending;
                        lbEdit.Text = "Send";
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case "F":
                        lblStatus.Text = Resources.labels.loi;
                        lbEdit.Text = "Send";
                        lblStatus.Attributes.Add("class", "label-warning");                        
                        break;                   
                }
                         if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT) || drv["STATUS"].ToString().Trim() == "F")
                {
                            lbEdit.Enabled = false;
                            lbEdit.OnClientClick = string.Empty;
            
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
            BinData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvLTWA.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvLTWA.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BinData();
    }

    protected void gvLTWA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvLTWA.PageIndex = e.NewPageIndex;
        BinData();
    }

}


