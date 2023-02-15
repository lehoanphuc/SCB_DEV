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

using System.Text;
using System.Collections.Generic;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSDeposit_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ltrError.Text = "";
            lblError.Text = "";

            if (!IsPostBack)
            {
                ddlStatus.Items.Add(new ListItem(Resources.labels.tatca, "ALL"));
                ddlStatus.Items.Add(new ListItem(Resources.labels.dangxuly, "0"));
                ddlStatus.Items.Add(new ListItem(Resources.labels.hoanthanh, "1"));
                ddlStatus.Items.Add(new ListItem(Resources.labels.loi, "2"));
                ddlStatus.Items.Add(new ListItem(Resources.labels.choduyet, "3"));
                //load tran app
                //DataSet dsTranApp = new SmartPortal.SEMS.Transactions().LoadTranApp(ref IPCERRORCODE, ref IPCERRORDESC);
                //if (IPCERRORCODE != "0")
                //{
                //    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                //}
                //DataTable dtTranApp = new DataTable();
                //dtTranApp = dsTranApp.Tables[0];

                //ddlTransactionType.DataSource = dtTranApp;
                //ddlTransactionType.DataTextField = "PAGENAME";
                //ddlTransactionType.DataValueField = "TRANCODE";
                //ddlTransactionType.DataBind();

                //ddlTransactionType.Items.Insert(0, new ListItem(Resources.labels.all,SmartPortal.Constant.IPC.ALL));
                BindData();
            }
        }
        catch
        {
        }
    }
    void BindData()
    {
        #region Xem giao dịch
        DataTable dtTran = new DataTable();
        string isBatch = "";
        if (ckbIsBatch.Checked)
        {
            isBatch = "Y";
        }
        string f;
        string t;
        if (txtFromDate.Text.Trim() == "" || txtToDate.Text.Trim() == "")
        {
            f = DateTime.Now.ToString("dd/MM/yyyy");
            t = DateTime.Now.ToString("dd/MM/yyyy");
        }
        else
        {
            f = txtFromDate.Text.Trim();
            t = txtToDate.Text.Trim();
        }

        DataSet dsTran = new SmartPortal.IB.Transactions().ViewLogTransaction("", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtTranID.Text.Trim()), SmartPortal.Constant.IPC.IBTRANSFEROUTBANK, SmartPortal.Common.Utilities.Utility.KillSqlInjection(f), SmartPortal.Common.Utilities.Utility.KillSqlInjection(t), ddlStatus.SelectedValue, SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtAccno.Text.Trim()), DDLAppSta.SelectedValue, ckbIsDelete.Checked, isBatch, Utility.KillSqlInjection(txtBatchRef.Text.Trim()), "", "", "", Utility.KillSqlInjection(txtCheckNo.Text.Trim()), "", "", "","","ALL", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            dtTran = dsTran.Tables[0];

            if (dtTran.Rows.Count == 0)
            {
                ltrError.Text = "<p style='color:red;margin-left:10px; margin-top:20px;'>" + Resources.labels.datanotfound + "</p>";
            }

        }
        else
        {
        }
        #endregion


        //hien len luoi
        gvLTWA.DataSource = dtTran;
        gvLTWA.DataBind();
    }
    protected void gvLTWA_RowDataBound(object sender, GridViewRowEventArgs e)
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
            //Label lblBatchRef;
            Label lblCheckNo;
            HyperLink hpDeposit;

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
                hpDeposit = (HyperLink)e.Row.FindControl("hpDeposit");
                lblDate = (Label)e.Row.FindControl("lblDate");
                lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblCCYID = (Label)e.Row.FindControl("lblCCYID");
                lblAccount = (Label)e.Row.FindControl("lblAccount");
                lblDesc = (Label)e.Row.FindControl("lblDesc");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblResult = (Label)e.Row.FindControl("lblResult");
                //lblBatchRef = (Label)e.Row.FindControl("lblBatchRef");
                lblCheckNo = (Label)e.Row.FindControl("lblCheckNo");  


                hpTranID.Text = drv["IPCTRANSID"].ToString();
                hpTranID.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=272&tranid=" + drv["IPCTRANSID"].ToString());
                lblDesc.Text = drv["TRANDESC"].ToString();
                lblAccount.Text = drv["CHAR01"].ToString();
                //lblBatchRef.Text = drv["BATCHREF"].ToString();
                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["NUM01"].ToString(), drv["CCYID"].ToString().Trim());
                lblCCYID.Text = drv["CCYID"].ToString();
                lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");
                lblCheckNo.Text =double.Parse(drv["NUM05"].ToString()).ToString();
                hpDeposit.Text = Resources.labels.hoantra;
                hpDeposit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=334&cn=" + double.Parse(drv["NUM05"].ToString()).ToString());

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
                            case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                lblResult.Text = Resources.labels.choduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                lblResult.Text = Resources.labels.dangxuly;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                lblResult.Text = Resources.labels.dahoantien;
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
                            case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                lblResult.Text = Resources.labels.choduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                lblResult.Text = Resources.labels.dangxuly;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                lblResult.Text = Resources.labels.dahoantien;
                                break;
                            

                        }
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.ERROR:
                        lblStatus.Text = "Lỗi";
                        switch (drv["APPRSTS"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                lblResult.Text = Resources.labels.duyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                lblResult.Text = Resources.labels.delete;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                lblResult.Text = Resources.labels.choduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                lblResult.Text = Resources.labels.dangxuly;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                lblResult.Text = Resources.labels.dahoantien;
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
            foreach (GridViewRow gvr in gvLTWA.Rows)
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
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=217"));
        }
        else
        {
            lblError.Text = Resources.labels.banvuilongchongiaodichcanduyet;
            BindData();
        }
    }
    protected void gvLTWA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLTWA.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void btnDeposit_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        Label lblCheckNo;

        string Str_ContractNo = "";
        try
        {
            foreach (GridViewRow gvr in gvLTWA.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lblCheckNo = (Label)gvr.Cells[1].FindControl("lblCheckNo");
                    Str_ContractNo += lblCheckNo.Text.Trim() + "#";
                }
            }

            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Widget", "btnDelete_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    EXIT:
        if (Str_ContractNo != "")
        {
            Session["checkno"] = Str_ContractNo.Substring(0, Str_ContractNo.Length - 1);

           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=334"));
        }
        else
        {
            lblError.Text = Resources.labels.banvuilongchongiaodichcanhoantratien;
            BindData();
        }
    }
}
