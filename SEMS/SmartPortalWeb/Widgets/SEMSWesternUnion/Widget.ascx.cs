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
using System.Collections.Generic;

public partial class Widgets_SEMSWesternUnion_Widget : WidgetBase
{
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
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

    void loadinit()
    {

        DataTable dtCurrency = new ExchangeRate().GetAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        dllCCYID.DataSource = dtCurrency;
        dllCCYID.DataTextField = "Currency";
        dllCCYID.DataValueField = "Currency";
        dllCCYID.DataBind();
        dllCCYID.Items.Insert(0, new ListItem(IPC.ALL, IPC.ALL));

        DataTable country = new ExchangeRate().GetAllCountry("", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        if (country.Rows.Count > 0)
        {
            ddlPayout.DataSource = country;
            ddlPayout.DataValueField = "COUNTRYCODES";
            ddlPayout.DataTextField = "COUNTRYNAME";
            ddlPayout.DataBind();
            ddlPayout.Items.Insert(0, new ListItem(IPC.ALL, IPC.ALL));
        }
        else
        {
            ddlPayout.Items.Clear();
            ddlPayout.Items.Add(new ListItem(Resources.labels.nothing, ""));
        }
    }

    void BinData()
    {
        int pageSize = gvLTWA.PageSize;
        int pageIndex = gvLTWA.PageIndex;
        int recordIndex = pageSize * pageIndex;
        divresult1.Visible = true;
        DataSet sb = new SmartPortal.SEMS.Transactions().GetListWestern(txtTranNo.Text.Trim(), txtsenderphone.Text.Trim(), "", ddlPayout.SelectedValue.Trim(), dllCCYID.SelectedValue, ddlStatus.SelectedValue, txtTranDate.Text.Trim(), txttodate.Text.Trim(), "", pageSize, recordIndex, ref IPCERRORCODE, ref IPCERRORDESC);
        ltrError.Text = string.Empty;
        DataTable dtTran = new DataTable();
        dtTran = sb.Tables[0];
        gvLTWA.DataSource = dtTran;
        gvLTWA.DataBind();
        GridViewPaging.Visible = true;
        ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = sb.Tables[0].Rows[0]["RECORDCOUNT"].ToString();
        if (dtTran.Rows.Count > 0)
        {
            ltrError.Text = string.Empty;
        }
        else
        {
            ltrError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
            GridViewPaging.Visible = false;
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
            LinkButton lbTranID, lbEdit, lbDelete;
            Label lblDate, lbAmountsend, lbSendCCYID, lbReCCYID, lblSenderphone, lblStatus;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                lbTranID = (LinkButton)e.Row.FindControl("lbTranID");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblDate = (Label)e.Row.FindControl("lblDate");
                lblSenderphone = (Label)e.Row.FindControl("lblSenderphone");
                lbAmountsend = (Label)e.Row.FindControl("lbAmountsend");
                lbSendCCYID = (Label)e.Row.FindControl("lbSendCCYID");
                lbReCCYID = (Label)e.Row.FindControl("lbReCCYID");

                lbTranID.Text = drv["IPCTRANSID"].ToString();
                lblDate.Text = drv["TRANDATE"].ToString();
                lblSenderphone.Text = drv["PHONENO"].ToString();
                lbAmountsend.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["SENDERAMOUNT"].ToString(), "");
                lbSendCCYID.Text = drv["SCCYID"].ToString();
                lbReCCYID.Text = drv["RCCYID"].ToString();

                lbEdit = (LinkButton)e.Row.FindControl("hpEdit");
                lbDelete = (LinkButton)e.Row.FindControl("hpDelete");

                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = Resources.labels.connew;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
						lbEdit.Text = "Resend";
                        lblStatus.Text = "Cancel";
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.send;
						lbEdit.Text = "Resend";
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
						lbEdit.Text = "Send";
                        lblStatus.Text = Resources.labels.conpending;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblStatus.Text = Resources.labels.conreject;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case "F":
                        lblStatus.Text = Resources.labels.fail;
                        lbEdit.Text = "Send";
                        lbEdit.Enabled = false;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                }


                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT) || drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE || drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.NEW)
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
                    lbDelete.Enabled = false;
                }

                if (drv["STATUS"].ToString().Trim() != "P")
                {
                    lbDelete.Enabled = false;

                }

                if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                {
                    lbDelete.Enabled = false;
                    lbDelete.OnClientClick = string.Empty;
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
            int pageSize = gvLTWA.PageSize;
            int pageIndex = gvLTWA.PageIndex;
            int recordIndex = pageSize * pageIndex;
            DataSet sb = new SmartPortal.SEMS.Transactions().GetListWestern(txtTranNo.Text.Trim(),txtsenderphone.Text.Trim(),"", ddlPayout.SelectedValue.Trim(), dllCCYID.SelectedValue, ddlStatus.SelectedValue, txtTranDate.Text.Trim(),txttodate.Text.Trim(),"", pageSize, recordIndex, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtTran = new DataTable();
                dtTran = sb.Tables[0];
                gvLTWA.DataSource = dtTran;
                gvLTWA.DataBind();
                GridViewPaging.Visible = true;
                divresult1.Visible = true;
                if (dtTran.Rows.Count > 0)
                {
                    ltrError.Text = string.Empty;
                    ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = sb.Tables[0].Rows[0]["RECORDCOUNT"].ToString();
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


