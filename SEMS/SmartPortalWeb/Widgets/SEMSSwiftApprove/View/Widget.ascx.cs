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
using System.Text;
using SmartPortal.ExceptionCollection;
using System.Collections.Generic;

public partial class Widgets_SEMSSwiftApprove_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string IPCTRANCODE = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

            if (!IsPostBack)
            {
                #region Load thông tin ngân hàng
                ddlBank.DataSource = new SmartPortal.SEMS.Bank().Load();
                ddlBank.DataTextField = "BANKNAME";
                ddlBank.DataValueField = "BANKCODE";
                ddlBank.DataBind();
                #endregion
                BindData();
            }
        
    }

    void BindData()
    {
        try
        {

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tc"] == null)
            {
                IPCTRANCODE = "IB000207";
            }
            else
            {
                IPCTRANCODE = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tc"].ToString();                
            }

            DataSet dtContr = new DataSet();
            dtContr = new SmartPortal.SEMS.Transactions().LoadAllTranByTrancode(txtTranID.Text.Trim(), txtFrom.Text.Trim(), txtTo.Text.Trim(), "", "ALL", IPCTRANCODE, "ALL",Session["branch"].ToString(),ddlBank.SelectedValue,Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);


            if (IPCERRORCODE == "0")
            {
                if (dtContr.Tables[0].Rows.Count == 0)
                {
                    pnbutton.Visible = false;

                }
                else { pnbutton.Visible = true; }
                gvcontractList.DataSource = dtContr;
                gvcontractList.DataBind();

            }
            else
            {
                goto ERROR;
            }

            litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvcontractList.PageIndex) * gvcontractList.PageSize) + gvcontractList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtContr.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;

            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "BindData", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:

        ;

    }
    protected void gvcontractList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpTranID;
            Label lblDate;
            Label lblAmount;
            Label lblDesc;
            Label lblBank;
            Label lblCity;
            

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


                hpTranID = (HyperLink)e.Row.FindControl("hpTranID");
                lblDate = (Label)e.Row.FindControl("lblDate");
                lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblDesc = (Label)e.Row.FindControl("lblDesc");
                lblBank = (Label)e.Row.FindControl("lblBank");
                lblCity = (Label)e.Row.FindControl("lblCity");


                hpTranID.Text = drv["IPCTRANSID"].ToString();
                hpTranID.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=255&tranid=" + drv["IPCTRANSID"].ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                
                lblAmount.Text =SmartPortal.Common.Utilities.Utility.FormatMoney(drv["NUM01"].ToString(),"")+Resources.labels.lak;
                lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                lblBank.Text = drv["CHAR05"].ToString();
                lblCity.Text = drv["CHAR06"].ToString();                
            }
        }
        catch(Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "gvcontractList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvcontractList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvcontractList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvcontractList_Sorting(object sender, GridViewSortEventArgs e)
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
        DataTable dataTable;

        if (ViewState["s"] != null)
        {
            dataTable = null;
        }

        else
        {
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["letter"] != null)
            {
                dataTable = null;

            }
            else
            {
                dataTable = null;

            }
        }


        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + direction;

            gvcontractList.DataSource = dataView;
            gvcontractList.DataBind();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
       

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //Duyệt giao dịch TUANTA

        CheckBox cbxSelect;
        HyperLink hpTranID;
        List<string> lstTran = new List<string>();
        try
        {
            foreach (GridViewRow gvr in gvcontractList.Rows)
            {
                cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxSelect.Checked == true)
                {
                    hpTranID = (HyperLink)gvr.Cells[1].FindControl("hpTranID");

                    //approve
                    //new SmartPortal.SEMS.Transactions().TellerApp(hpTranID.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);

                    lstTran.Add(hpTranID.Text.Trim());
                }
            }

        }
        catch (Exception ex)
        {

        }
        if (lstTran.Count != 0)
        {
            Session["tranID"] = lstTran;
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=255"+"&returnURL="+SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query)));
        }
        else
        {
            lblError.Text = Resources.labels.banvuilongchongiaodichcanduyet;
            BindData();
        }
        
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        ////từ chối hợp đồng -Quyềnnnpv
        //Approvereject(SmartPortal.Constant.IPC.BLOCK);

    }
    
}
