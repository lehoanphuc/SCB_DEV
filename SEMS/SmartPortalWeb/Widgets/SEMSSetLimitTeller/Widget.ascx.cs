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

using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSSetLimitTeller_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                LoadDll();
                GridViewPaging.Visible = false;
                BindData();
            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    void LoadDll()
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.User().GetUserByUserType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.TELLER), string.Empty, string.Empty, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlUserName.DataSource = ds;
                ddlUserName.DataTextField = "USERID";
                ddlUserName.DataValueField = "USERID";
                ddlUserName.DataBind();
                ddlUserName.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            ddltran.DataSource = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISPRODUCTFEE), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {

                ddltran.DataTextField = "PAGENAME";
                ddltran.DataValueField = "TRANCODE";
                ddltran.DataBind();
                ddltran.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            ddlccyid.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlccyid.DataTextField = "CCYID";
            ddlccyid.DataValueField = "CCYID";
            ddlccyid.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvUser.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvUser.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            ltrError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvUser.PageIndex * gvUser.PageSize) return;
            DataSet dsLimitTeller = new SmartPortal.SEMS.Transactions().GetAllLimitTellerByUID(Utility.KillSqlInjection(ddlUserName.SelectedValue), Utility.KillSqlInjection(ddltran.SelectedValue), Utility.KillSqlInjection(ddlccyid.SelectedValue), gvUser.PageSize, gvUser.PageIndex * gvUser.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                gvUser.DataSource = dsLimitTeller;
                gvUser.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            if (dsLimitTeller.Tables[0].Rows.Count > 0)
            {
                ltrError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dsLimitTeller.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
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
    protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblTrans, lblLimit, lblCCYID;
            LinkButton lblUserID, lbEdit, lbDelete;

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

                lblUserID = (LinkButton)e.Row.FindControl("lblUserID");
                lblTrans = (Label)e.Row.FindControl("lblTrans");
                lblLimit = (Label)e.Row.FindControl("lblLimit");
                lblCCYID = (Label)e.Row.FindControl("lblCCYID");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lblUserID.Text = drv["USERID"].ToString();
                lblTrans.Text = drv["PAGENAME"].ToString();
                lblLimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["LIMITAPPROVE"].ToString(), drv["CCYID"].ToString().Trim());
                lblCCYID.Text = drv["CCYID"].ToString();

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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
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
    protected void gvUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvUser.Rows[e.RowIndex].Cells[1].FindControl("lblUserID")).CommandArgument;
            string[] pros = commandArg.Split('|');
            new SmartPortal.SEMS.Transactions().DeleteUserApproveLimit(pros[0], pros[1], pros[2], ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData2();
                lblError.Text = Resources.labels.xoahanmucduyetgiaodichthanhcong;
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
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            CheckBox cbxDelete;
            LinkButton lblProductName;
            string strProductCode = "";
            try
            {
                foreach (GridViewRow gvr in gvUser.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lblProductName = (LinkButton)gvr.Cells[1].FindControl("lblUserID");
                        strProductCode += lblProductName.CommandArgument.Trim() + "#";
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
                        string[] pros = ProductCode[i].Split('|');
                        new SmartPortal.SEMS.Transactions().DeleteUserApproveLimit(pros[0], pros[1], pros[2], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            BindData2();
                            lblError.Text = Resources.labels.xoahanmucduyetgiaodichthanhcong;
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
    void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvUser.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvUser.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
