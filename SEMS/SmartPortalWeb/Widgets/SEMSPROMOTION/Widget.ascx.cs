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

public partial class Widgets_SEMSPromotion_Widget : WidgetBase
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
        gvPromotion.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvPromotion.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            divResult.Visible = true;
            ltrError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvPromotion.PageIndex * gvPromotion.PageSize) return;
            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.PROMOTION().PROMOTION_SEARCH(Utility.KillSqlInjection(txtPROMOTIONCODE.Text.Trim()), Utility.KillSqlInjection(ddlStatus.SelectedValue.ToString().Trim()), Utility.KillSqlInjection(txtEXPIREDDATE.Text.Trim()), gvPromotion.PageSize, gvPromotion.PageIndex * gvPromotion.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                gvPromotion.DataSource = ds;
                gvPromotion.DataBind();
            }
            else
            {
                //lblError.Text = IPCERRORDESC;
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvPromotion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblExpireDate, lblUserCreated, lblDateCreated, lblStatus;
            LinkButton lbPromotionCode, lbEdit, lbDelete;
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
                lbPromotionCode = (LinkButton)e.Row.FindControl("lbPromotionCode");
                lblExpireDate = (Label)e.Row.FindControl("lblExpireDate");
                lblUserCreated = (Label)e.Row.FindControl("lblUserCreated");
                lblDateCreated = (Label)e.Row.FindControl("lblDateCreated");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lbPromotionCode.Text = drv["PROMOTIONCODE"].ToString();
                lblExpireDate.Text = Utility.FormatDatetime(drv["EXPIREDDATE"].ToString(), "dd/MM/yyyy HH:mm");
                lblUserCreated.Text = drv["USERCREATE"].ToString(); 
                lblDateCreated.Text = Utility.FormatDatetime(drv["CREATEDATE"].ToString(), "dd/MM/yyyy HH:mm");
                switch (drv["Status"].ToString())
                {
                    case "A":
                        lblStatus.Text = Resources.labels.active;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case "I":
                        lblStatus.Text = Resources.labels.inactive;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case "Y":
                        lblStatus.Text = Resources.labels.expired;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                }

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
                    lbPromotionCode.Enabled = false;
                    lbPromotionCode.OnClientClick = string.Empty;
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
    protected void gvPromotion_RowCommand(object sender, GridViewCommandEventArgs e)
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
    protected void gvPromotion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvPromotion.Rows[e.RowIndex].Cells[1].FindControl("lbDelete")).CommandArgument;
            DataSet ds = new SmartPortal.SEMS.PROMOTION().PROMOTIONCODE_GETID(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dataTable = new DataTable();
                dataTable = ds.Tables[0];
                if (dataTable.Rows.Count > 0)
                {
                    SmartPortal.Common.Log.WriteLogDatabase(dataTable.Rows[0]["PROMOTIONCODE"].ToString(), "DELETE", Request.Url.ToString(), Session["userName"].ToString(),
                        Request.UserHostAddress, "EBA_PromotionCode", " PROMOTIONCODE", dataTable.Rows[0]["PROMOTIONCODE"].ToString(), "", "");
                    SmartPortal.Common.Log.WriteLogDatabase(dataTable.Rows[0]["PROMOTIONCODE"].ToString(), "DELETE", Request.Url.ToString(), Session["userName"].ToString(),
                        Request.UserHostAddress, "EBA_PromotionCode", " STATUS", dataTable.Rows[0]["STATUS"].ToString(), "", "");
                    SmartPortal.Common.Log.WriteLogDatabase(dataTable.Rows[0]["PROMOTIONCODE"].ToString(), "DELETE", Request.Url.ToString(), Session["userName"].ToString(),
                        Request.UserHostAddress, "EBA_PromotionCode", " EXPIREDDATE", Utility.FormatDatetime(dataTable.Rows[0]["EXPIREDDATE"].ToString(), "dd/MM/yyyy"), "", "");
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
            }
            else
            {
                throw new IPCException(IPCERRORCODE);
            }
            new SmartPortal.SEMS.PROMOTION().PROMOTIONCODE_DELETE(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData2();
                lblError.Text = Resources.labels.xoapromotionthanhcong;
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
            string strPromotion = "";
            try
            {
                foreach (GridViewRow gvr in gvPromotion.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lbDelete = (LinkButton)gvr.Cells[1].FindControl("lbDelete");
                        strPromotion += lbDelete.CommandArgument.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strPromotion))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] pro = strPromotion.Split('#');
                    for (int i = 0; i < pro.Length - 1; i++)
                    {
                        DataSet ds = new SmartPortal.SEMS.PROMOTION().PROMOTIONCODE_GETID(pro[i].Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            DataTable dataTable = new DataTable();
                            dataTable = ds.Tables[0];
                            if (dataTable.Rows.Count > 0)
                            {
                                SmartPortal.Common.Log.WriteLogDatabase(dataTable.Rows[0]["PROMOTIONCODE"].ToString(), "DELETE", Request.Url.ToString(), Session["userName"].ToString(),
                                    Request.UserHostAddress, "EBA_PromotionCode", " PROMOTIONCODE", dataTable.Rows[0]["PROMOTIONCODE"].ToString(), "", "");
                                SmartPortal.Common.Log.WriteLogDatabase(dataTable.Rows[0]["PROMOTIONCODE"].ToString(), "DELETE", Request.Url.ToString(), Session["userName"].ToString(),
                                    Request.UserHostAddress, "EBA_PromotionCode", " STATUS", dataTable.Rows[0]["STATUS"].ToString(), "", "");
                                SmartPortal.Common.Log.WriteLogDatabase(dataTable.Rows[0]["PROMOTIONCODE"].ToString(), "DELETE", Request.Url.ToString(), Session["userName"].ToString(),
                                    Request.UserHostAddress, "EBA_PromotionCode", " EXPIREDDATE", Utility.FormatDatetime(dataTable.Rows[0]["EXPIREDDATE"].ToString(), "dd/MM/yyyy"), "", "");
                            }
                            else
                            {
                                throw new IPCException(IPCERRORDESC);
                            }
                        }
                        else
                        {
                            throw new IPCException(IPCERRORCODE);
                        }
                        new SmartPortal.SEMS.PROMOTION().PROMOTIONCODE_DELETE(pro[i].Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            BindData2();
                            lblError.Text = Resources.labels.xoapromotionthanhcong;
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
            gvPromotion.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvPromotion.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
