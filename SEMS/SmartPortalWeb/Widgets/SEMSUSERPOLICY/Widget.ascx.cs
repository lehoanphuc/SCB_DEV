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
using SmartPortal.BLL;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.Model;
using SmartPortal.SEMS;

public partial class Widgets_SEMSUSERPOLICY_Widget : WidgetBase
{
    public static bool isAscend = false;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAdd.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);

                DataSet dsservice = new Services().GetAll("SID", ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    DataTable dtservice = dsservice.Tables[0];
                    ddlService.DataSource = dtservice;
                    ddlService.DataTextField = "ServiceName";
                    ddlService.DataValueField = "ServiceID";
                    ddlService.DataBind();
                    ddlService.Items.Remove(ddlService.Items.FindByValue(SmartPortal.Constant.IPC.SMS));
                }
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
        gvPolicy.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvPolicy.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvPolicy.PageIndex * gvPolicy.PageSize) return;
            DataSet ds = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(Utility.KillSqlInjection(txpolicyid.Text.Trim()), Utility.KillSqlInjection(txdescr.Text.Trim()), Utility.KillSqlInjection(txeffrom.Text.Trim()), Utility.KillSqlInjection(txefto.Text.Trim()), ddlService.SelectedValue, Session["userName"].ToString(), gvPolicy.PageSize, gvPolicy.PageIndex * gvPolicy.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvPolicy.DataSource = ds;
                gvPolicy.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
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
    protected void gvPolicy_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton lbpolicyid;
            Label lbdescr;
            Label lbeffrom;
            Label lbefto;
            Label lbservicename;
            LinkButton lbEdit;
            LinkButton lbDelete;
            Label lbisdefault;
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
                lbpolicyid = (LinkButton)e.Row.FindControl("lbpolicyid");
                lbservicename = (Label)e.Row.FindControl("lbservicename");
                lbdescr = (Label)e.Row.FindControl("lbdescr");
                lbeffrom = (Label)e.Row.FindControl("lbeffrom");
                lbefto = (Label)e.Row.FindControl("lbefto");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
                lbisdefault = (Label)e.Row.FindControl("lbisdefault");
                lbpolicyid.Text = drv["policyID"].ToString();
                lbservicename.Text = drv["ServiceName"].ToString();
                lbdescr.Text = drv["descr"].ToString();
                lbeffrom.Text = drv["effromtx"].ToString();
                lbefto.Text = drv["eftotx"].ToString();
                lbisdefault.Text = drv["isdefaulttx"].ToString();
                if (lbisdefault.Text == "False")
                {
                    lbisdefault.Text = Resources.labels.khong;
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                    {
                        lbDelete.Enabled = false;
                        lbDelete.OnClientClick = string.Empty;
                    }
                }
                else
                {
                    cbxSelect.Enabled = false;
                    lbisdefault.Text = Resources.labels.co;
                    lbDelete.Enabled = false;
                    lbDelete.OnClientClick = string.Empty;
                }
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            string strPolicyID = string.Empty;
            try
            {
                foreach (GridViewRow gvr in gvPolicy.Rows)
                {
                    CheckBox cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        LinkButton lbpolicyid = (LinkButton)gvr.Cells[1].FindControl("lbpolicyid");
                        strPolicyID += lbpolicyid.CommandArgument.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strPolicyID))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] PolicyID = strPolicyID.Split('#');
                    for (int i = 0; i < PolicyID.Length - 1; i++)
                    {
                        string[] obj = PolicyID[i].Split('|');
                        DataTable dtpolicycheck = new SmartPortal.SEMS.USERPOLICY().Checkpolicyused(obj[0], obj[1]);
                        if (dtpolicycheck.Rows.Count != 0)
                        {
                            lblError.Text = Resources.labels.Policydaduocganchouserhayxoausertrongpolicytruoc;
                            return;
                        }
                        DataSet dsPolicy = new SmartPortal.SEMS.USERPOLICY().PolicyDelete(int.Parse(obj[0]), obj[1], ref IPCERRORCODE, ref IPCERRORDESC);
                        SmartPortal.Common.Log.WriteLogFile("DELETE POLICY ", "", "", "serviceid:" + obj[1] + "-" + obj[0] + "-numbers of user belong-" + dtpolicycheck.Rows.Count);
                        if (IPCERRORCODE == "0")
                        {
                            BindData2();
                            lblError.Text = Resources.labels.Xoapolicythanhcong;
                        }
                        else
                        {
                            ErrorCodeModel EM = new ErrorCodeModel();
                            EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.IPC), System.Globalization.CultureInfo.CurrentCulture.ToString());
                            lblError.Text = EM.ErrorDesc;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
            }
        }
    }
    protected void gvPolicy_RowCommand(object sender, GridViewCommandEventArgs e)
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
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
    }
    protected void gvPolicy_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvPolicy.Rows[e.RowIndex].Cells[1].FindControl("lbpolicyid")).CommandArgument.ToString().Trim();
            string[] obj = commandArg.Split('|');
            DataTable dtpolicycheck = new SmartPortal.SEMS.USERPOLICY().Checkpolicyused(obj[0], obj[1]);
            if (dtpolicycheck.Rows.Count != 0)
            {
                lblError.Text = Resources.labels.Policydaduocganchouserhayxoausertrongpolicytruoc;
                return;
            }
            DataSet dsPolicy = new SmartPortal.SEMS.USERPOLICY().PolicyDelete(int.Parse(obj[0]), obj[1], ref IPCERRORCODE, ref IPCERRORDESC);
            SmartPortal.Common.Log.WriteLogFile("DELETE POLICY ", "", "", "serviceid:" + obj[1] + "-" + obj[0] + "-numbers of user belong-" + dtpolicycheck.Rows.Count);
            if (IPCERRORCODE == "0")
            {
                BindData2();
                lblError.Text = Resources.labels.Xoapolicythanhcong;
            }
            else
            {
                ErrorCodeModel EM = new ErrorCodeModel();
                EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.IPC), System.Globalization.CultureInfo.CurrentCulture.ToString());
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
    void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvPolicy.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvPolicy.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
