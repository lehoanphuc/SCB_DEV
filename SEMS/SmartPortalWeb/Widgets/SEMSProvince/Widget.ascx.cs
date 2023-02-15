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
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.SEMS;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_SEMSProvince_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            litError.Text = "";
            if (!IsPostBack)
            {
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAdd_New.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
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
        gvProvinceList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvProvinceList.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = String.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvProvinceList.PageIndex * gvProvinceList.PageSize) return;
            City objCity = new City();
            DataSet ds = new DataSet();
            ds = objCity.GetCityByCondition(Utility.KillSqlInjection(txtCityCode.Text.Trim()), Utility.KillSqlInjection(txtCityName.Text.Trim()), Utility.KillSqlInjection(txtCityNameMM.Text.Trim()), gvProvinceList.PageSize, gvProvinceList.PageIndex * gvProvinceList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvProvinceList.DataSource = ds;
                gvProvinceList.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    litError.Text = String.Empty;
                    GridViewPaging.Visible = true;
                    ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
                }
                else
                {
                    litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                    GridViewPaging.Visible = false;
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
    protected void gvProvinceList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblProvinceName, lblProvinceNameMM, lblStatus;
            LinkButton lblProvinceCode, lbEdit, lbDelete;
            DataRowView drv;

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

                lblProvinceCode = (LinkButton)e.Row.FindControl("lblProvinceCode");
                lblProvinceName = (Label)e.Row.FindControl("lblProvinceName");
                lblProvinceNameMM = (Label)e.Row.FindControl("lblProvinceNameMM");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lblProvinceCode.Text = drv["CITYCODE"].ToString();
                lblProvinceNameMM.Text = drv["CITYNAMEMM"].ToString();
                lblProvinceName.Text = drv["CITYNAME"].ToString();
                switch (drv["Status"].ToString())
                {
                    case "A":
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            CheckBox cbxDelete;
            LinkButton lblProvinceCode;
            string strlblProvinceCode = "";
            try
            {
                foreach (GridViewRow gvr in gvProvinceList.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lblProvinceCode = (LinkButton)gvr.Cells[1].FindControl("lblProvinceCode");
                        strlblProvinceCode += lblProvinceCode.CommandArgument.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strlblProvinceCode))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] province = strlblProvinceCode.Split('#');
                    for (int i = 0; i < province.Length - 1; i++)
                    {
                        new City().DelCity(province[i], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            BindData2();
                            lblError.Text = Resources.labels.xoathongtintinhthanhthanhcong;
                        }
                        else
                        {
                            string errorCode = string.Empty;
                            ErrorCodeModel EM = new ErrorCodeModel();
                            if (IPCERRORDESC == "110211")
                            {
                                errorCode = IPC.ERRORCODE.EXISTDISTRICT;
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
    protected void gvProvinceList_RowCommand(object sender, GridViewCommandEventArgs e)
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
    protected void gvProvinceList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvProvinceList.Rows[e.RowIndex].Cells[1].FindControl("lblProvinceCode")).CommandArgument;
            new City().DelCity(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                BindData2();
                lblError.Text = Resources.labels.xoathongtintinhthanhthanhcong;
            }
            else
            {
                string errorCode = string.Empty;
                ErrorCodeModel EM = new ErrorCodeModel();
                if (IPCERRORDESC == "110211")
                {
                    errorCode = IPC.ERRORCODE.EXISTDISTRICT;
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
            gvProvinceList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvProvinceList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
