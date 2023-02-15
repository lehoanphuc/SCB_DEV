using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
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


public partial class Widgets_SEMSTellerApproveTrans_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private int size = 0;
    public DataTable TABLE
    {
        get { return ViewState["TABLE"] != null ? (DataTable)ViewState["TABLE"] : new DataTable(); }
        set { ViewState["TABLE"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            txtAmount.Attributes.Add("onkeypress", "executeComma('" + txtAmount.ClientID + "')");
            txtAmount.Attributes.Add("onkeyup", "executeComma('" + txtAmount.ClientID + "')");

            if (!IsPostBack)
            {
                LoadDll();
                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnExport.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.EXPORT);
                ltrError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
                //BindData();
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
        gvProcessList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvProcessList.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void LoadDll()
    {
        try
        {
            ddltrans.Items.Add(new ListItem(Resources.labels.doanhnghiep, "O"));
            ddltrans.Items.Add(new ListItem(Resources.labels.canhan, "P"));
            txtAmount.Attributes.Add("onkeypress", "executeComma('" + txtAmount.ClientID + "',event)");
            txtAmount.Attributes.Add("onkeyup", "executeComma('" + txtAmount.ClientID + "',event)");
            //load role của teller

            ddlRole.DataSource = new SmartPortal.SEMS.Product().GetProductByCondition("", "", "", "", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            ddlRole.DataTextField = "PRODUCTNAME";
            ddlRole.DataValueField = "PRODUCTID";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));

            //load các giao dịch

            ddltrans.DataSource = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            ddltrans.DataTextField = "PAGENAME";
            ddltrans.DataValueField = "TRANCODE";
            ddltrans.DataBind();
            ddltrans.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));

            //load tiền tệ
            ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlCCYID.DataTextField = "CCYID";
            ddlCCYID.DataValueField = "CCYID";
            ddlCCYID.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    void BindData()
    {
        try
        {
            ltrError.Text = string.Empty;
            DataSet dsProcess = new DataSet();
            dsProcess = new SmartPortal.SEMS.Transactions().SearchTellerApproveTrans(Utility.KillSqlInjection(txtapptranID.Text.Trim()), Utility.KillSqlInjection(ddltrans.SelectedValue), Utility.KillSqlInjection(ddlRole.SelectedValue), Utility.KillSqlInjection(ddlCCYID.SelectedValue), Utility.KillSqlInjection(txtAmount.Text.Trim().Replace(",", "").Replace(".", "")), "", gvProcessList.PageSize, gvProcessList.PageIndex * gvProcessList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvProcessList.DataSource = dsProcess;
                gvProcessList.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            if (dsProcess.Tables[0].Rows.Count > 0)
            {
                ltrError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dsProcess.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                ltrError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvProcessList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblRole, lblTrans, lblCCYID, lblFrom, lblTo;
            LinkButton lbProcessid, lbEdit, lbDelete;
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

                lbProcessid = (LinkButton)e.Row.FindControl("lbProcessid");
                lblRole = (Label)e.Row.FindControl("lblRole");
                lblTrans = (Label)e.Row.FindControl("lblTrans");
                lblCCYID = (Label)e.Row.FindControl("lblCCYID");
                lblFrom = (Label)e.Row.FindControl("lblFrom");
                lblTo = (Label)e.Row.FindControl("lblTo");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lbProcessid.Text = drv["APPTRANID"].ToString();
                lblRole.Text = drv["PRODUCTNAME"].ToString();
                lblTrans.Text = drv["PAGENAME"].ToString();
                lblCCYID.Text = drv["CCYID"].ToString();
                lblFrom.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["FromLimit"].ToString(), drv["CCYID"].ToString().Trim());
                lblTo.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TOLIMITOK"].ToString(), drv["CCYID"].ToString().Trim());

                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
                }
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                {
                    lbDelete.Enabled = false;
                    lbDelete.OnClientClick = string.Empty;
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
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
            CheckBox cbxDelete;
            LinkButton lbProcessid;
            string strProductCode = "";
            try
            {
                foreach (GridViewRow gvr in gvProcessList.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lbProcessid = (LinkButton)gvr.Cells[1].FindControl("lbProcessid");
                        strProductCode += lbProcessid.CommandArgument.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strProductCode))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] ProductCode = strProductCode.Split('#');
                    for (int i = 0; i < ProductCode.Length - 1; i++)
                    {
                        new SmartPortal.SEMS.Transactions().DeleteProcess(ProductCode[i], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            BindData2();
                            lblError.Text = Resources.labels.xoaquytrinhduyetthanhcong;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            SmartPortal.Common.ExportToFile.ExportToExcel(TABLE, "Result");
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvProcessList_RowCommand(object sender, GridViewCommandEventArgs e)
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
    protected void gvProcessList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvProcessList.Rows[e.RowIndex].Cells[1].FindControl("lbProcessid")).CommandArgument;
            new SmartPortal.SEMS.Transactions().DeleteProcess(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData2();
                lblError.Text = Resources.labels.xoaquytrinhduyetthanhcong;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
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
            gvProcessList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvProcessList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
