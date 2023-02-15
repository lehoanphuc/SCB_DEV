using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using Resources;
using SmartPortal.BLL;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.Model;
using SmartPortal.SEMS;

public partial class Widgets_SEMSCONFIGNPSCURRENCY_Widget : WidgetBase
{
    private string IPCERRORCODE = "";
    private string IPCERRORDESC = "";
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                LoadDll();
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
    private void LoadDll()
    {
        try
        {
            DataTable data = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);

            ddlCCYID.DataSource = data;
            ddlCCYID.DataTextField = "CCYID";
            ddlCCYID.DataValueField = "CCYID";
            ddlCCYID.DataBind();
            ddlCCYID.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            int pageSize = gvCurrencyList.PageSize;
            int pageIndex = gvCurrencyList.PageIndex;
            int recordIndex = pageSize * pageIndex;
            litError.Text = string.Empty;
            DataTable dtCurrency = new Currency().SearchConfigNPSCurrency(Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim()), pageSize, recordIndex,  ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (IPCERRORCODE == "0")
            {
                gvCurrencyList.DataSource = dtCurrency;
                gvCurrencyList.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }

            if (dtCurrency.Rows.Count > 0)
            {
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField) GridViewPaging.FindControl("TotalRows")).Value =
                    dtCurrency.Rows[0]["TRECORDCOUNT"].ToString();
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
    //protected void btnAddNew_OnClick(object sender, EventArgs e)
    //{
    //    if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
    //    {
    //        RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
    //    }
    //}
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    CheckBox cbxDelete;
    //    LinkButton lbCCYID;
    //    string strCCYID = "";
    //    try
    //    {
    //        foreach (GridViewRow gvr in gvCurrencyList.Rows)
    //        {
    //            cbxDelete = (CheckBox) gvr.Cells[0].FindControl("cbxSelect");
    //            if (cbxDelete.Checked == true)
    //            {
    //                lbCCYID = (LinkButton) gvr.Cells[1].FindControl("lbFromCCYID");
    //                strCCYID += lbCCYID.CommandArgument.ToString().Trim() + "#";
    //            }
    //        }

    //        if (string.IsNullOrEmpty(strCCYID))
    //        {
    //            lblError.Text = Resources.labels.pleaseselectbeforedeleting;
    //            return;
    //        }
    //        else
    //        {
    //            string[] ccyid = strCCYID.Split('#');
    //            for (int i = 0; i < ccyid.Length - 1; i++)
    //            {
    //                string[] pros = ccyid[i].Split('|');
    //                new Currency().DeleteCurrencyFx(pros[0], pros[1], ref IPCERRORCODE, ref IPCERRORDESC);
    //                if (IPCERRORCODE.Equals("0"))
    //                {
    //                    btnSearch_OnClick(this, EventArgs.Empty);
    //                    lblError.Text = Resources.labels.deletecurrencysuccessfully;
    //                }
    //                else
    //                {
    //                    btnSearch_OnClick(this, EventArgs.Empty);
    //                    lblError.Text = IPCERRORDESC;
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
    //            this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
    //        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
    //    }
    //}
    protected void gvCurrencyList_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            LinkButton lbCCYID, lbEdit;
            Label lblDesc, lblStatus , lblUserModify;
            DataRowView drv;

            

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView) e.Row.DataItem;
                lbCCYID = (LinkButton) e.Row.FindControl("lbCCYID");
                lblDesc = (Label) e.Row.FindControl("lblDesc");
                lblStatus = (Label) e.Row.FindControl("lblStatus");
                lblUserModify = (Label) e.Row.FindControl("lblUserModify");
                lbEdit = (LinkButton) e.Row.FindControl("lbEdit");

                lbCCYID.Text = drv["CCYID"].ToString();
                lblDesc.Text = drv["DESCRIPTION"].ToString();
                lblUserModify.Text = drv["USERMODIFIED"].ToString();

                switch (drv[IPC.STATUS].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.active;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    default:
                        lblStatus.Text = Resources.labels.inactive;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                }

                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
                }    


                hdCounter.Value = "0";
                hdPageSize.Value = size.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvCurrencyList_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArgument = e.CommandArgument.ToString();
        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + IPC.ID + "=" + commandArgument);
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + IPC.ID + "=" + commandArgument);
                    break;
            }
        }
    }
    protected void gvCurrencyList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton) gvCurrencyList.Rows[e.RowIndex].Cells[1].FindControl("lbFromCCYID")).CommandArgument; 
            string[] pros = commandArg.Split('|');
            new Currency().DeleteCurrencyFx(pros[0], pros[1], ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                btnSearch_OnClick(this, EventArgs.Empty);
                lblError.Text = Resources.labels.deletecurrencysuccessfully;
            }
            else
            {
                btnSearch_OnClick(this, EventArgs.Empty);
                lblError.Text = IPCERRORDESC;
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvCurrencyList.PageSize =
                Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvCurrencyList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    //protected void btnClear_OnClick(object sender, EventArgs e)
    //{
    //    lblError.Text = string.Empty;
    //    ddlCCYID.SelectedIndex = 0;
    //    ddlCCYID.SelectedIndex = 0;
    //    btnSearch_OnClick(sender, EventArgs.Empty);
    //}
    private void GridViewPagingClick(object sender, EventArgs e)
    {
        gvCurrencyList.PageSize =
            Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvCurrencyList.PageIndex = Convert.ToInt32(((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
}