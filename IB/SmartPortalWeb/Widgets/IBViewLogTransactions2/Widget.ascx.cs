using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_IBViewLogTransactions_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string tranisBatch = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ltrError.Text = "";
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //load tran app
                string contractNo = string.Empty;
                DataSet dsContract = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(Session["userID"].ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                DataTable dtContract = new DataTable();
                dtContract = dsContract.Tables[0];
                if (dtContract.Rows.Count != 0)
                {
                    contractNo = dtContract.Rows[0]["CONTRACTNO"].ToString();
                }

                DataSet dsResult = new SmartPortal.SEMS.Product().GetTranNameByContractNo(Utility.KillSqlInjection(contractNo), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                ddlTransactionType.DataSource = dsResult;
                ddlTransactionType.DataTextField = "PAGENAME";
                ddlTransactionType.DataValueField = "TRANCODE";
                ddlTransactionType.DataBind();
                ddlTransactionType.Items.Insert(0, new ListItem(Resources.labels.tatca, IPC.ALL));


                SmartPortal.IB.Account accountList = new SmartPortal.IB.Account();

                DataSet ds = accountList.GetInfoAcct(Session["userID"].ToString(), "IB0002021", "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
                ddlDebitAcct.DataSource = ds;
                ddlDebitAcct.DataTextField = "ACCTNO";
                ddlDebitAcct.DataValueField = "ACCTNO";
                ddlDebitAcct.DataBind();
                ddlDebitAcct.Items.Insert(0, new ListItem(Resources.labels.all, string.Empty));

                ckbIsBatch.Visible = false;
            }
            tranisBatch = System.Configuration.ConfigurationSettings.AppSettings["TRANBATCH"].ToString();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void BindData()
    {
        try
        {
            DataTable dtTran = new DataTable();
            string isBatch = "";
            string isschedule = "ALL";
            if (ckbIsBatch.Checked)
            {
                isBatch = "Y";
            }

            if (cbIsschedule.Checked)
            {
                isschedule = "";
            }

            DataSet dsTran = new SmartPortal.IB.Transactions().ViewLogTransaction(Session["userID"].ToString(),
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtTranID.Text.Trim()),
                ddlTransactionType.SelectedValue,
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFromDate.Text.Trim()),
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtToDate.Text.Trim()), "3",
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlDebitAcct.SelectedValue.Trim()),
                DDLAppSta.SelectedValue, ckbIsDelete.Checked, isBatch, txtBatchRef.Text.Trim(), "", "", "", "",
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCreditAcct.Text.Trim()), "", "", "",
                isschedule, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                dtTran = dsTran.Tables[0];
            }

            rptLTWA.DataSource = dtTran;
            rptLTWA.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (txtFromDate.Text.Length > 0 && txtToDate.Text.Length > 0)
            {
                DateTime myDate = DateTime.ParseExact(txtFromDate.Text.ToString(), "dd/MM/yyyy", null);
                DateTime targetDate = DateTime.ParseExact(txtToDate.Text.ToString(), "dd/MM/yyyy", null);
                if (myDate > targetDate)
                {
                    lblError.Text = Resources.labels.ngayketthucphailonhonngaybatdau;
                    return;
                }
            }
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tranisBatch.IndexOf(ddlTransactionType.SelectedValue) > 0)
        {
            ckbIsBatch.Visible = true;
        }
        else
        {
            ckbIsBatch.Checked = false;
            ckbIsBatch.Visible = false;
        }
        BindData();
    }
    protected void gvLTWA_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string commandName = e.CommandName;

            string commandArg = e.CommandArgument.ToString();
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    if (!IBCheckPermitPageAction(commandName)) return;
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.IPCTRANSID + "=" + commandArg, false);
                    break;
                case IPC.TRANSTATUS.CANCEL:
                    if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DETAILS)) return;
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.IPCTRANSID + "=" + commandArg+"&b=CANCEL", false);
                    break;
                case "EDIT":
                    if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DETAILS)) return;
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.IPCTRANSID + "=" + commandArg + "&b=EDIT", false);
                    break;
                default:
                    return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected static string GetStatus(object dataItem)
    {
        string result = string.Empty;
        string status = Convert.ToString(DataBinder.Eval(dataItem, "STATUS"));
        switch (status.Trim())
        {
            case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                result = Resources.labels.dangxuly;
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                result = Resources.labels.hoanthanh;
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
                result = Resources.labels.loi;
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
                result = Resources.labels.choduyet;
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.REJECTED:
                result = Resources.labels.huy;
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.PENDING:
                result = Resources.labels.conpending;
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.PAYMENTFAIL:
                result = Resources.labels.thanhtoanthatbai;
                break;
            case SmartPortal.Constant.IPC.TRANSTATUS.CANCEL:
                result = Resources.labels.canceled;
                break;
        }

        return result;
    }

    protected void gvLTWA_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                LinkButton lbDetails = (LinkButton)e.Item.FindControl("lbDetails");
                if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DETAILS))
                {
                    lbDetails.Enabled = false;
                    lbDetails.OnClientClick = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
