using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSCBMNetFee_Widget : WidgetBase
{
    string IPCERRORCODE = String.Empty;
    string IPCERRORDESC = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        if (!IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = new InterBank().DETAIL("CBMNetFee", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtAmount.Text = Utility.FormatMoney(dt.Rows[0]["VARVALUE"].ToString(), "LAK");
                }
            }
            else
            {
                throw new IPCException(IPCERRORCODE);
            }
            btnSave.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.EDIT);
            GridViewPaging.Visible = false;
            divResult.Visible = false;
            BindData();
        }
        GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAmount.Text.Trim() == "")
            {
                lblError.Text = Resources.labels.tienphikhongrong;
                return;
            }
            else
            {
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                new InterBank().UPDATE("CBMNetFee", Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), "LAK")), Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.capnhatphithanhcong;
                    BindData();
                }
                else
                {
                    throw new IPCException(IPCERRORCODE);
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value)
                < gvHisChange.PageIndex * gvHisChange.PageSize) return;

            DataSet dataSet = new DataSet();
            dataSet = new InterBank().HISTORY(gvHisChange.PageSize, gvHisChange.PageIndex * gvHisChange.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                gvHisChange.DataSource = dataSet;
                gvHisChange.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORCODE);
            }
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                litError.Text = String.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dataSet.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                //litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        try
        {
            gvHisChange.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            gvHisChange.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvHisChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblDate, lblValueOld, lblValueNew, lblUserName;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                lblDate = (Label)e.Row.FindControl("lblDate");
                lblValueOld = (Label)e.Row.FindControl("lblValueOld");
                lblValueNew = (Label)e.Row.FindControl("lblValueNew");
                lblUserName = (Label)e.Row.FindControl("lblUserName");

                lblDate.Text = Convert.ToDateTime(drv["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                lblValueOld.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["NUM01"].ToString(), "LAK");
                lblValueNew.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["NUM02"].ToString(), "LAK");
                lblUserName.Text = drv["USERID"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}