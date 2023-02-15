using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class Widgets_SEMSTransactionsApprove_ViewCloseFD_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ltrError.Text = "";
            litPager.Text = "";
            lblError.Text = "";

            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch
        {
        }
    }

    void BindData()
    {
        DataTable dtContr = new DataTable();
        dtContr = new SmartPortal.SEMS.Transactions().LoadTranForApproveCloseFD(Session["userName"].ToString(), Utility.KillSqlInjection(txtTranID.Text.Trim()), Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tc"].ToString()), Utility.KillSqlInjection(txtFrom.Text.Trim()), Utility.KillSqlInjection(txtTo.Text.Trim()), ddlStatus.SelectedValue, Utility.KillSqlInjection(txtAccount.Text.Trim()), ddlResult.SelectedValue, Session["branch"].ToString(),"", txtCustName.Text,txtContractNo.Text, ref IPCERRORCODE, ref IPCERRORDESC);

        if (IPCERRORCODE == "0")
        {
            //if(dtContr.Tables[0].Rows.Count==0)
            //{
            //    pnbutton.Visible = false;
            
            //}
            //else { pnbutton.Visible = true; }

            //DataTable dtContr = dsContr.Tables[0];

            gvcontractList.DataSource = dtContr;
            gvcontractList.DataBind();

            if (dtContr.Rows.Count == 0)
            {
                ltrError.Text = "<center><p style='color:red;margin-left:10px; margin-top:10px;font-weight:bold'>"+Resources.labels.datanotfound+"</p></center>";
            }
            else
            {
                litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvcontractList.PageIndex) * gvcontractList.PageSize) + gvcontractList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtContr.Rows.Count.ToString() + "</b> " + Resources.labels.dong;
            }            
            
        }
        else
        {
            //lblError.Text = IPCERRORDESC;
        }

        
    }
    protected void gvcontractList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpTranID;
            Label lblDate;
            Label lblAmount;
            Label lblCCYID;
            Label lblAccount;
            Label lblDesc;
            Label lblStatus;
            Label lblResult;
            Label lblCustName;
            

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
                lblCCYID = (Label)e.Row.FindControl("lblCCYID");
                lblAccount = (Label)e.Row.FindControl("lblAccount");
                lblDesc = (Label)e.Row.FindControl("lblDesc");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblResult = (Label)e.Row.FindControl("lblResult");
                lblCustName = (Label)e.Row.FindControl("lblCustName");


                hpTranID.Text = drv["IPCTRANSID"].ToString();
                if (drv["IPCTRANCODE"].ToString().Trim() == "IB000499")
                {
                    hpTranID.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=383&tranid=" + drv["IPCTRANSID"].ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                }
                else
                {
                    hpTranID.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=383&tranid=" + drv["IPCTRANSID"].ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                }
                lblDesc.Text = drv["TRANDESC"].ToString();
                lblCustName.Text = drv["FULLNAME"].ToString();
                lblAccount.Text = drv["CHAR01"].ToString();
                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["NUM01"].ToString(), drv["CCYID"].ToString().Trim());
                lblCCYID.Text = drv["CCYID"].ToString();
                lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                                
               
                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                        lblStatus.Text = Resources.labels.dangxuly;
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                        lblStatus.Text = Resources.labels.hoanthanh;
                        switch (drv["APPRSTS"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                lblResult.Text = Resources.labels.duyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                lblResult.Text = Resources.labels.delete;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGBANK:
                                lblResult.Text = Resources.labels.choduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                lblResult.Text = Resources.labels.dangxuly;
                                break;
                         

                        }
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
                        lblStatus.Text = Resources.labels.choduyet;
                        switch (drv["APPRSTS"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                lblResult.Text = Resources.labels.duyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                lblResult.Text = Resources.labels.delete;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGBANK:
                                lblResult.Text = Resources.labels.choduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                lblResult.Text = Resources.labels.dangxuly;
                                break;
                           


                        }
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
                        lblStatus.Text = Resources.labels.loi;
                        switch (drv["APPRSTS"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                lblResult.Text = Resources.labels.duyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                lblResult.Text = Resources.labels.delete;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGBANK:
                                lblResult.Text = Resources.labels.choduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                lblResult.Text = Resources.labels.dangxuly;
                                break;
                          

                        }
                        break;

                }
            }
        }
        catch
        {
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

                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tc"].ToString().Trim() == "IB000301")
                {
                   Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=383&type=a&returnURL=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query)));
                }
                else
                {
                   Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=210&type=a&returnURL=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query)));
                }
            }
            else
            {
                lblError.Text = Resources.labels.banvuilongchongiaodichcanduyet;
                BindData();
            }
    }
    protected void btnReject_Click(object sender, EventArgs e)
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

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["tc"].ToString().Trim() == "IB000301")
            {
               Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=383&type=r&returnURL=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query)));
            }
            else
            {
               Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=210&type=r&returnURL=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query)));
            }
        }
        else
        {
            lblError.Text = Resources.labels.banvuilongchongiaodichcanhuy;
            BindData();
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=208"));
    }
}
