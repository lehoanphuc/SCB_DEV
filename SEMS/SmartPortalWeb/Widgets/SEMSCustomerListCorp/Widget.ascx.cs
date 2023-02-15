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

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSCustomerListCorp_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //gan URL cho control search by letter
            LetterSearch.url = "Default.aspx?p=138";
            Session["search"] = null;

            BindData();
        }
    }

    void BindData()
    {
        try
        {
            DataSet dtCL = new DataSet();
            if (Session["search"] != null)
            {
                dtCL = new SmartPortal.SEMS.Customer().GetCustomerByCondition(Utility.KillSqlInjection(txtCustCode.Text.Trim()), Utility.KillSqlInjection(txtFullName.Text.Trim()), Utility.KillSqlInjection(txtTel.Text.Trim()), Utility.KillSqlInjection(txtLicenseID.Text.Trim()), ddlCustType.SelectedValue, SmartPortal.Constant.IPC.ALL, "", ref IPCERRORCODE, ref IPCERRORDESC);

            }
            else
            {
                if (Request.QueryString.Get("letter") != null)
                {
                    dtCL = new SmartPortal.SEMS.Customer().GetCustomerByLetter(Utility.KillSqlInjection(Request.QueryString.Get("letter").ToString().Trim()), ref IPCERRORCODE, ref IPCERRORDESC);

                }
                else
                {
                    dtCL = new SmartPortal.SEMS.Customer().GetCustomerByCondition(Utility.KillSqlInjection(txtCustCode.Text.Trim()), Utility.KillSqlInjection(txtFullName.Text.Trim()), Utility.KillSqlInjection(txtTel.Text.Trim()), Utility.KillSqlInjection(txtLicenseID.Text.Trim()), ddlCustType.SelectedValue, SmartPortal.Constant.IPC.ALL, "", ref IPCERRORCODE, ref IPCERRORDESC);

                }
            }

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            gvCustomerList.DataSource = dtCL;
            gvCustomerList.DataBind();

            //set to session to export
            Session["DataExport"] = gvCustomerList;
            Session["TableExport"] = dtCL;

            litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvCustomerList.PageIndex) * gvCustomerList.PageSize) + gvCustomerList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtCL.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCustomerList_Widget", "BindData", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }

    protected void gvCustomerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink lblCustCode;
            Label lblCustName;
            Label lblPhone;
            Label lblIdentify;
            Label lblCustType;
            //Label lblStatus;
            HyperLink hpEdit;
            //HyperLink hpDelete;

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
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                lblCustCode = (HyperLink)e.Row.FindControl("lblCustCode");
                lblCustName = (Label)e.Row.FindControl("lblCustName");
                lblPhone = (Label)e.Row.FindControl("lblPhone");
                lblIdentify = (Label)e.Row.FindControl("lblIdentify");
                lblCustType = (Label)e.Row.FindControl("lblCustType");
                //lblStatus = (Label)e.Row.FindControl("lblStatus");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                //hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                lblCustCode.Text = drv["CUSTID"].ToString();
                lblCustCode.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=145&a=viewdetail&cid=" + drv["CUSTID"].ToString());
                lblCustName.Text = drv["FULLNAME"].ToString();
                lblPhone.Text = drv["TEL"].ToString();
                lblIdentify.Text = drv["LICENSEID"].ToString();

                switch (drv["CFTYPE"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.PERSONAL:
                        lblCustType.Text = Resources.labels.canhan;
                        break;
                    case SmartPortal.Constant.IPC.CORPORATE:
                        lblCustType.Text = Resources.labels.doanhnghiep;
                        break;
                }
                // switch (drv["STATUS"].ToString().Trim())
                //{
                //    case SmartPortal.Constant.IPC.NEW:
                //         lblStatus.Text = "Mới tạo";
                //        break;
                //        case SmartPortal.Constant.IPC.DELETE:
                //         lblStatus.Text = "Đã xóa";
                //        break;
                //        case SmartPortal.Constant.IPC.ACTIVE:
                //         lblStatus.Text = "Sử dụng";
                //        break;
                //}


                hpEdit.Text = "<img src='Widgets/Pages/view/images/icon_edit.gif'/>";
                hpEdit.ToolTip = Resources.labels.edit;
                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=153&a=edit&cid=" + drv["CUSTID"].ToString());
                //hpDelete.Text = "Resources.labels. delete ";
                //hpDelete.NavigateUrl = "~/Default.aspx?p=181&cid=" + drv["CUSTID"].ToString(); ;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Widget", "gvCustomerList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    protected void gvCustomerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvCustomerList.PageIndex = e.NewPageIndex;
        BindData();

    }

    protected void gvCustomerList_Sorting(object sender, GridViewSortEventArgs e)
    {
        //isSort = true;

        string sortExpression = e.SortExpression;

        ViewState["SortExpression"] = sortExpression;
        //showImage = true;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            //isAscend = true;

            SortGridView(sortExpression, ASCENDING);
            GridViewSortDirection = SortDirection.Descending;

        }

        else
        {
            isAscend = false;
            SortGridView(sortExpression, DESCENDING);
            GridViewSortDirection = SortDirection.Ascending;

        }
    }

    private SortDirection GridViewSortDirection
    {
        get
        {

            if (ViewState["sortDirection"] == null)

                ViewState["sortDirection"] = SortDirection.Ascending;


            return (SortDirection)ViewState["sortDirection"];

        }

        set { ViewState["sortDirection"] = value; }

    }

    protected void SortGridView(string sortExpression, string direction)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dataTable;

            if (ViewState["s"] != null)
            {
                dataTable = null;
            }

            else
            {
                if (Session["search"] != null)
                {
                    ds = (new SmartPortal.SEMS.Customer().GetCustomerByCondition(Utility.KillSqlInjection(txtCustCode.Text.Trim()), Utility.KillSqlInjection(txtFullName.Text.Trim()), Utility.KillSqlInjection(txtTel.Text.Trim()), Utility.KillSqlInjection(txtLicenseID.Text.Trim()), ddlCustType.SelectedValue, SmartPortal.Constant.IPC.ALL, "", ref IPCERRORCODE, ref IPCERRORDESC));
                    if (IPCERRORCODE == "0")
                    {
                        dataTable = ds.Tables[0];
                    }
                    else
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                }
                else
                {
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["letter"] != null)
                    {
                        ds = (new SmartPortal.SEMS.Customer().GetCustomerByLetter(Utility.KillSqlInjection(Request.QueryString.Get("letter").ToString().Trim()), ref IPCERRORCODE, ref IPCERRORDESC));
                        if (IPCERRORCODE == "0")
                        {
                            dataTable = ds.Tables[0];
                        }
                        else
                        {
                            throw new IPCException(IPCERRORDESC);
                        }
                    }
                    else
                    {
                        ds = (new SmartPortal.SEMS.Customer().GetCustomerByCondition(Utility.KillSqlInjection(txtCustCode.Text.Trim()), Utility.KillSqlInjection(txtFullName.Text.Trim()), Utility.KillSqlInjection(txtTel.Text.Trim()), Utility.KillSqlInjection(txtLicenseID.Text.Trim()), ddlCustType.SelectedValue, SmartPortal.Constant.IPC.ALL, "", ref IPCERRORCODE, ref IPCERRORDESC));
                        if (IPCERRORCODE == "0")
                        {
                            dataTable = ds.Tables[0];
                        }
                        else
                        {
                            throw new IPCException(IPCERRORDESC);
                        }
                    }
                }


            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = sortExpression + direction;

                gvCustomerList.DataSource = dataView;
                gvCustomerList.DataBind();
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCustomerList_Widget", "SortGridView", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Widget", "SortGridView", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=147"));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        CheckBox cbxDelete;
        HyperLink hpCustID;

        string Str_CustID = "";
        try
        {
            foreach (GridViewRow gvr in gvCustomerList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpCustID = (HyperLink)gvr.Cells[1].FindControl("lblCustCode");
                    Str_CustID += hpCustID.Text.Trim() + "#";
                }
            }
            Session["_CustomerID"] = Str_CustID.Substring(0, Str_CustID.Length - 1);

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCustomerList_Widget", "btnDelete_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Widget", "btnDelete_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=181"));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        Session["search"] = "true";
        BindData();

    }
}
