using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System.Data;
using SmartPortal.BLL;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;
using SmartPortal.Model;

public partial class Widgets_SEMSDistrict_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            litError.Text = "";
            if (!IsPostBack)
            {
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAdd_New.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                GridViewPaging.Visible = false;
                LoadCity("");
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
    protected void gvDistrictList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton lblDistCode;
            Label lblDistName;
            Label lblDistNameMM;
            Label lblCityCode;
            LinkButton lbEdit;
            LinkButton lbDelete;
            DataRowView drv;
            HyperLink hpTownShipManagement;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                TableRow tbRow = (TableRow)(e.Row.Cells[0].Controls[0].Controls[0]);
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + " :</strong>";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");

                lblDistCode = (LinkButton)e.Row.FindControl("lblquan");
                lblDistName = (Label)e.Row.FindControl("lbltenquan");
                lblDistNameMM = (Label)e.Row.FindControl("lbltenquanMM");
                lblCityCode = (Label)e.Row.FindControl("lbltinh");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
                hpTownShipManagement = (HyperLink)e.Row.FindControl("hpTownShipManagement");
                lblCityCode.Text = drv["CITYNAME"].ToString();
                lblDistCode.Text = drv["DISTCODE"].ToString();
                lblDistName.Text = drv["DISTNAME"].ToString();
                lblDistNameMM.Text = drv["DISTNAMEMM"].ToString();
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
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.DETAILS))
                {
                    hpTownShipManagement.Enabled = false;
                    hpTownShipManagement.NavigateUrl = string.Empty;
                }
                else
                {
                    hpTownShipManagement.NavigateUrl = "~/" + SmartPortal.Common.Encrypt.EncryptURL(PagesBLL.GetLinkAction_Page("TOWNSHIP", IPC.ACTIONPAGE.LIST) + "&cid=" + drv["DISTCODE"].ToString());
                }
                hdCounter.Value = "0";
                hdPageSize.Value = size.ToString();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
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
            LinkButton lblquan;
            string strtinhID = "";
            try
            {
                foreach (GridViewRow gvr in gvDistrictList.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lblquan = (LinkButton)gvr.Cells[1].FindControl("lblquan");
                        strtinhID += lblquan.CommandArgument.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strtinhID))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] tinh = strtinhID.Split('#');
                    for (int i = 0; i < tinh.Length - 1; i++)
                    {
                        new District().DelDistrict(tinh[i], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            BindData2();
                            lblError.Text = Resources.labels.xoathongtinquanhuyenthanhcong;
                        }
                        else
                        {
                            string errorCode = string.Empty;
                            ErrorCodeModel EM = new ErrorCodeModel();
                            if (IPCERRORDESC == "110211")
                            {
                                errorCode = IPC.ERRORCODE.ACTIVEDISTRICT;
                            }
                            else
                            {
                                errorCode = IPC.ERRORCODE.IPC;
                            }
                            EM = new ErrorBLL().Load(Utility.IsInt(errorCode), System.Globalization.CultureInfo.CurrentCulture.ToString());
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
    protected void gvDistrictList_RowCommand(object sender, GridViewCommandEventArgs e)
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
    protected void gvDistrictList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvDistrictList.Rows[e.RowIndex].Cells[1].FindControl("lblquan")).CommandArgument;
            new District().DelDistrict(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                BindData2();
                lblError.Text = Resources.labels.xoathongtinquanhuyenthanhcong;
            }
            else
            {
                string errorCode = string.Empty;
                ErrorCodeModel EM = new ErrorCodeModel();
                if (IPCERRORDESC == "110211")
                {
                    errorCode = IPC.ERRORCODE.ACTIVEDISTRICT;
                }
                else
                {
                    errorCode = IPC.ERRORCODE.IPC;
                }
                EM = new ErrorBLL().Load(Utility.IsInt(errorCode), System.Globalization.CultureInfo.CurrentCulture.ToString());
                lblError.Text = EM.ErrorDesc;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    #region load data
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvDistrictList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvDistrictList.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            divResult.Visible = true; 
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvDistrictList.PageIndex * gvDistrictList.PageSize) return;
            District objDis = new District();
            DataSet ds = new DataSet();
            ds = objDis.GetDistrictByCondition("",SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDistName.Text.Trim()), SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCity.SelectedValue.Trim()), txtDistNameMM.Text.Trim(), gvDistrictList.PageSize, gvDistrictList.PageIndex * gvDistrictList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0" && ds.Tables.Count > 0)
            {
                gvDistrictList.DataSource = ds;
                gvDistrictList.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    litError.Text = String.Empty;
                    GridViewPaging.Visible = true;
                    ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
                }
                else
                {
                    GridViewPaging.Visible = false;
                    litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                }
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadCity(string cityCode)
    {
        try
        {
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvDistrictList.PageIndex * gvDistrictList.PageSize) return;
            DataSet dts = new DataSet();
            dts = new SmartPortal.SEMS.District().GetCityById(ref IPCERRORCODE, ref IPCERRORDESC);
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            ddlCity.DataSource = dts;
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "CityCode";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    #endregion
    void BindData2()
    {
        try
        {
            ((TextBox) GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField) GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvDistrictList.PageSize = Convert.ToInt32(((DropDownList) GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvDistrictList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings ["sysec"], "");
        }
    }
}
