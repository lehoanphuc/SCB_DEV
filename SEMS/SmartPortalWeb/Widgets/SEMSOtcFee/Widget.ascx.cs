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
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_SEMSOTCFEE_Widget : WidgetBase
{
    public static bool isAscend = false;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e) 
    {
        try
        {
            lblError.Text = string.Empty;

            if (!IsPostBack)
            {
                ddltran.DataSource = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISOTC), Utility.KillSqlInjection(SmartPortal.Constant.IPC.bittrue), ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                ddltran.DataTextField = "PAGENAME";
                ddltran.DataValueField = "TRANCODE";
                ddltran.DataBind();
                ddltran.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));

                ddlccyid.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlccyid.DataTextField = "CCYID";
                ddlccyid.DataValueField = "CCYID";
                ddlccyid.DataBind();

                DataSet ds = new SmartPortal.SEMS.Fee().SearchFee(string.Empty, string.Empty, string.Empty, Utility.KillSqlInjection(ddlccyid.SelectedValue), 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    ddlFee.DataSource = ds;
                    ddlFee.DataTextField = "FEENAME";
                    ddlFee.DataValueField = "FEEID";
                    ddlFee.DataBind();
                    ddlFee.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }

                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
        
                GridViewPaging.Visible = false;
                divResult.Visible = false;


            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvOtcFee.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvOtcFee.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            divResult.Visible = true;
            ltrError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvOtcFee.PageIndex * gvOtcFee.PageSize) return;

            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.OtcFee().SearchOtcFee(Utility.KillSqlInjection(ddltran.SelectedValue),Utility.KillSqlInjection(ddlccyid.SelectedValue),"ALL","ALL", Utility.KillSqlInjection(ddlFee.SelectedValue), gvOtcFee.PageSize, gvOtcFee.PageIndex * gvOtcFee.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvOtcFee.DataSource = ds;
                gvOtcFee.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ltrError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                ltrError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvOtcFee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblFeeType, lblproductid, lbltrancode, lblfeeid, lblccyid, lblContractLevelName;
            LinkButton  lbEdit, lbDelete, lblTrans;
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
                lblTrans = (LinkButton)e.Row.FindControl("lblTrans");
                lblFeeType = (Label)e.Row.FindControl("lblFeeType");
                lbltrancode = (Label)e.Row.FindControl("lbltrancode");
                lblfeeid = (Label)e.Row.FindControl("lblfeeid");
                lblccyid = (Label)e.Row.FindControl("lblccyid");
                lblContractLevelName = (Label)e.Row.FindControl("lblContractLevelName");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lblTrans.Text = drv["PAGENAME"].ToString();

                lblFeeType.Text = drv["FEENAME"].ToString();
                lbltrancode.Text = drv["TRANCODE"].ToString();
                lblfeeid.Text = drv["FEEID"].ToString();
                lblccyid.Text = drv["CCYID"].ToString();
                lblContractLevelName.Text = drv["ContractLevelName"].ToString();

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
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.DETAILS))
                {
                    lblTrans.Enabled = false;
                    lblTrans.OnClientClick = string.Empty;
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
    }
    protected void gvOtcFee_RowCommand(object sender, GridViewCommandEventArgs e)
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
    protected void gvOtcFee_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvOtcFee.Rows[e.RowIndex].Cells[1].FindControl("lbDelete")).CommandArgument;
            string[] otcfee = commandArg.Split('|');
            new SmartPortal.SEMS.OtcFee().DeleteOtcFee(otcfee[0], otcfee[4], otcfee[1], otcfee[3], otcfee[2], ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData2();
                lblError.Text = Resources.labels.xoaphiotcthanhcong;
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            CheckBox cbxDelete;
            LinkButton lbDelete;
            string strOtcfee = "";
            try
            {
                foreach (GridViewRow gvr in gvOtcFee.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lbDelete = (LinkButton)gvr.Cells[1].FindControl("lbDelete");
                        strOtcfee += lbDelete.CommandArgument.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strOtcfee))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] otcF = strOtcfee.Split('#');
                    for (int i = 0; i < otcF.Length - 1; i++)
                    {
                        string[] otcfee = otcF[i].Split('|');
                        new SmartPortal.SEMS.OtcFee().DeleteOtcFee(otcfee[0], otcfee[4], otcfee[1], otcfee[3], otcfee[2], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            BindData2();
                            lblError.Text = Resources.labels.xoaphiotcthanhcong;
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
    protected void ddlccyid_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.Fee().SearchFee(string.Empty, string.Empty, string.Empty, Utility.KillSqlInjection(ddlccyid.SelectedValue),0,0, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlFee.DataSource = ds;
                ddlFee.DataTextField = "FEENAME";
                ddlFee.DataValueField = "FEEID";
                ddlFee.DataBind();
                ddlFee.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvOtcFee.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvOtcFee.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
