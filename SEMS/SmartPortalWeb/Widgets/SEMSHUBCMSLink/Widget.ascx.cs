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

public partial class Widgets_HUBCMSView_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    private const string ControlPageID="1005";
    private const string DeletePageID = "1006";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblAlert.Text = ""; 
            
            if (!IsPostBack)
            {
                #region hien thị status
                ddlStatus.Items.Add(new ListItem(Resources.labels.conactive, SmartPortal.Constant.IPC.ACTIVE));
                ddlStatus.Items.Add(new ListItem(Resources.labels.condelete, SmartPortal.Constant.IPC.DELETE));
                //ddlStatus.Items.Add(new ListItem(Resources.labels.conreject, SmartPortal.Constant.IPC.REJECT));
                //ddlStatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
                //ddlStatus.Items.Add(new ListItem(Resources.labels.conblock, SmartPortal.Constant.IPC.BLOCK));
                ddlStatus.Items.Insert(0, new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                #endregion

                BindData();
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

    void BindData()
    {
        
        DataTable  dtCMSUser = new DataTable();

        dtCMSUser = new SmartPortal.SEMS.Contract().HubCms_Search(txtCorpID.Text, txtCorpName.Text, txtContractNo.Text, txtCustName.Text, ddlStatus.SelectedValue, "", ref IPCERRORCODE, ref IPCERRORDESC);

        gvCMSList.DataSource = dtCMSUser;
        gvCMSList.DataBind();

        if (dtCMSUser.Rows.Count != 0)
        {
            litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvCMSList.PageIndex) * gvCMSList.PageSize) + gvCMSList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtCMSUser.Rows.Count.ToString() + "</b> " + Resources.labels.dong;
        }
        else
        {
            lblAlert.Text = Resources.labels.datanotfound;
        }
    }
    protected void gvCMSList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblID;
            Label lblCorpID;
            Label lblCorpName;
            Label lblContractNo;
            Label lblCustName;
            Label lblStatus;
            HyperLink hpDelete;
            HyperLink hpEdit;

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

                lblID = (Label)e.Row.FindControl("lblID");
                lblCorpID = (Label)e.Row.FindControl("lblCorpID");
                lblCorpName = (Label)e.Row.FindControl("lblCorpName");
                lblContractNo = (Label)e.Row.FindControl("lblContractNo");
                lblCustName = (Label)e.Row.FindControl("lblCustName");
                lblStatus = (Label)e.Row.FindControl("lblStatus");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                lblID.Text = drv["ID"].ToString();
                lblCorpID.Text = drv["CorpID"].ToString();
                lblCorpName.Text = drv["CorpName"].ToString();
                lblContractNo.Text = drv["ContractNo"].ToString();
                lblCustName.Text = drv["CustName"].ToString();
                lblStatus.Text = drv["Status"].ToString();

                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = Resources.labels.condelete;
                        hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.conactive;
                        if (drv["BRANCHID"].ToString().Trim() == Session["branch"].ToString().Trim())
                        {
                            hpDelete.Text = Resources.labels.xoa;
                            hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + DeletePageID + "&id=" + drv["ID"].ToString() + "&stt=" + drv["STATUS"].ToString());
                        }
                        else
                        {
                            hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                        }
                        break;
                    default:
                        hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                        break;
                }
                
                hpEdit.Text = Resources.labels.edit;
                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + ControlPageID + "&a=edit&uid=" + drv["ID"].ToString());
                
            }
        }
        catch
        {
        }
    }
    protected void gvCMSList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCMSList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvCMSList_Sorting(object sender, GridViewSortEventArgs e)
    {

        string sortExpression = e.SortExpression;

        ViewState["SortExpression"] = sortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
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
        /*
        DataTable dataTable;

        if (ViewState["s"] != null)
        {
            dataTable = null;
        }

        else
        {

            dataTable = new SmartPortal.SEMS.OTP().Search(txtUser.Text.Trim(), ddlStatus.SelectedValue, ddlAuthenType.SelectedValue, txtauthenCode.Text, Session["userID"].ToString().Trim(), "VIEW");
            
        }


        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvCMSList.DataSource = dataView;
            gvCMSList.DataBind();
        }
         */

    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + ControlPageID + "&a=add"));
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpProID;

        string Str_ProID = "";
        try
        {
            foreach (GridViewRow gvr in gvCMSList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpProID = (HyperLink)gvr.Cells[1].FindControl("lblProductCode");
                    Str_ProID += hpProID.Text.Trim() + "#";
                }
            }
            Session["_ProductID"] = Str_ProID.Substring(0, Str_ProID.Length - 1);
        }
        catch (Exception ex)
        {

        }
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + DeletePageID));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            Session["search"] = "true";
            BindData();
        }
        catch (Exception ex)
        {
        }
    }
}
