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

public partial class Widgets_SEMSReceiverApprove_Widget : WidgetBase
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
            lblAlert.Text = "";
            lblError.Text = "";

            if (!IsPostBack)
            {

                #region hien thị status
                ddlStatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));               
                //ddlStatus.Items.Add(new ListItem(Resources.labels.approve, SmartPortal.Constant.IPC.ACTIVE));
                //ddlStatus.Items.Add(new ListItem(Resources.labels.conreject, SmartPortal.Constant.IPC.REJECT));
                #endregion

                BindData();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    void BindData()
    {
        try
        {
            DataSet dtContr = new DataSet();
            dtContr = new SmartPortal.SEMS.Services().SearchReveiverList(Utility.KillSqlInjection(txtreceivername.Text.Trim()), Utility.KillSqlInjection(txtAcctno.Text.Trim()), ddlStatus.SelectedValue,Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
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
                throw new IPCException(IPCERRORDESC);
            }
            if (dtContr.Tables[0].Rows.Count == 0)
            {
                lblAlert.Text =Resources.labels.datanotfound;
                litPager.Text = "";
            }
            else
            {
                litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvcontractList.PageIndex) * gvcontractList.PageSize) + gvcontractList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtContr.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "BindData", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvcontractList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpID, hpReceiver;
            Label lblAcctno;
            Label lblTransType;
            Label lblFullName;
            Label lblDesc;
            Label lblStatus;

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

                hpID = (HyperLink)e.Row.FindControl("hpID");
                hpReceiver = (HyperLink)e.Row.FindControl("hpReceiver");
                lblAcctno = (Label)e.Row.FindControl("lblAcctno");
                lblTransType = (Label)e.Row.FindControl("lblTransType");
                lblFullName = (Label)e.Row.FindControl("lblFullName");
                lblDesc = (Label)e.Row.FindControl("lblDesc");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                hpID.Text = drv["ID"].ToString();
                hpReceiver.Text = drv["RECEIVERNAME"].ToString();
                hpReceiver.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=318&id=" + drv["ID"].ToString()+"&stt="+ddlStatus.SelectedValue);
                lblAcctno.Text = drv["ACCTNO"].ToString();
                lblTransType.Text = drv["PAGENAME"].ToString();
                lblFullName.Text = drv["FULLNAME"].ToString();
                lblDesc.Text = drv["DESCRIPTION"].ToString();

                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = Resources.labels.connew;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = Resources.labels.condelete;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.conactive;
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblStatus.Text = Resources.labels.conblock;
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblStatus.Text = Resources.labels.conpending;
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblStatus.Text = Resources.labels.conreject;
                        break;
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

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
        try
        {
            DataSet DS = new DataSet();
            DataTable dataTable;

            DS = new SmartPortal.SEMS.Services().SearchReveiverList(Utility.KillSqlInjection(txtreceivername.Text.Trim()), Utility.KillSqlInjection(txtAcctno.Text.Trim()), ddlStatus.SelectedValue,Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                dataTable = DS.Tables[0];
            }
            else
            {
                goto ERROR;
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = sortExpression + direction;

                gvcontractList.DataSource = dataView;
                gvcontractList.DataBind();
            }

            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "SortGridView", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SortGridView", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:

        ;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //Duyệt hợp đồng-Quyềnnpv
        //Approvereject(SmartPortal.Constant.IPC.ACTIVE);

        //Duyệt giao dịch TUANTA

        CheckBox cbxSelect;
        HyperLink hpcontractNo;
        List<string> lstTran = new List<string>();
        try
        {
            foreach (GridViewRow gvr in gvcontractList.Rows)
            {
                cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxSelect.Checked == true)
                {
                    hpcontractNo = (HyperLink)gvr.Cells[1].FindControl("hpID");

                    //approve
                    //new SmartPortal.SEMS.Transactions().TellerApp(hpTranID.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);

                    lstTran.Add(hpcontractNo.Text.Trim());
                }
            }

        }
        catch (Exception ex)
        {

        }
        if (lstTran.Count != 0)
        {
            Session["_ReceiverApprove"] = lstTran;
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=319&act=App"));
            //Response.Redirect("~/Default.aspx?p=149&returnURL=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query)));
        }
        else
        {
            //lblError.Text = Resources.labels.youmustchoosecontracttoapprove;           
            lblError.Text = Resources.labels.vuilongchonnguoithuhuong;
            BindData();
        }
        
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        //từ chối hợp đồng -Quyềnnnpv
        //Approvereject(SmartPortal.Constant.IPC.BLOCK);

        //Duyệt giao dịch TUANTA

        CheckBox cbxSelect;
        HyperLink hpcontractNo;
        List<string> lstTran = new List<string>();
        try
        {
            foreach (GridViewRow gvr in gvcontractList.Rows)
            {
                cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxSelect.Checked == true)
                {
                    hpcontractNo = (HyperLink)gvr.Cells[1].FindControl("hpID");

                    //approve
                    //new SmartPortal.SEMS.Transactions().TellerApp(hpTranID.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);

                    lstTran.Add(hpcontractNo.Text.Trim());
                }
            }

        }
        catch (Exception ex)
        {

        }
        if (lstTran.Count != 0)
        {
            Session["_ReceiverApprove"] = lstTran;
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=319&act=Rej"));
            //Response.Redirect("~/Default.aspx?p=149&returnURL=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query)));
        }
        else
        {
            //lblError.Text = Resources.labels.youmustchoose;
            lblError.Text = Resources.labels.vuilongchonnguoithuhuong;
            BindData();
        }

    }
    
}
