using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_EBASYSVAR_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewPaging.Visible = false;
            divResult.Visible = false;
        }
        GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
    }
    void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.EBASYSVAR().GetEBASYSVARLIST(Utility.KillSqlInjection(txtValueName.Text.Trim()), Utility.KillSqlInjection(txtValueCode.Text.Trim()), Utility.KillSqlInjection(txtType.Text.Trim()), gvValuelist.PageSize, gvValuelist.PageIndex * gvValuelist.PageSize, ref IPCERRORCODE, ref IPCERRORDESC
                );
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            if (ds != null)
            {
                gvValuelist.DataSource = ds;
                gvValuelist.DataBind();
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
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSTelco_Widget", "BindData", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSTelco_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvValuelist.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvValuelist.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    protected void gvValuelist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblvarvalue, lblvarname, lblType, lbldesc;
            LinkButton lbEdit;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                lblvarname = (Label)e.Row.FindControl("lblvarname");
                lblvarvalue = (Label)e.Row.FindControl("lblvarvalue");
                lbldesc = (Label)e.Row.FindControl("lbldesc");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lblType = (Label)e.Row.FindControl("lblType");

                if (drv["TYPE"].ToString().Trim().ToUpper().Equals("HIDDEN"))
                {
                    lblvarvalue.Text = "*******************************";
                }
                else
                {
                    lblvarvalue.Text = drv["VARVALUE"].ToString();
                }
                lblvarname.Text = drv["VARNAME"].ToString();
                lblType.Text = drv["Type"].ToString();
                lbldesc.Text = drv["VARDESC"].ToString();
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
                }

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSTelco_Widget", "gvTelco_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvValuelist.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvValuelist.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void gvValuelist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        string[] param = commandArg.Split('|');

        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + "cid" + "=" + param[0]);
                    break;
            }
        }
    }
}