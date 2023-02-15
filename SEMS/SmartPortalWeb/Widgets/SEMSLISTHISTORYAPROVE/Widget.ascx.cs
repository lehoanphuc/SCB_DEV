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

using System.Text;
using System.Collections.Generic;
using System.IO;
using SmartPortal.Constant;

public partial class Widgets_SEMSLISTHISTORYAPROVE_Widget : WidgetBase
{
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    private int size = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["tranID"] = null;

            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                //load tran app
                DataSet dsTranApp = new SmartPortal.SEMS.Transactions().LoadTranApp(ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
                DataTable dtTranApp = new DataTable();
                dtTranApp = dsTranApp.Tables[0];

                ddlTransactionType.DataSource = dtTranApp;
                ddlTransactionType.DataTextField = "PAGENAME";
                ddlTransactionType.DataValueField = "TRANCODE";
                ddlTransactionType.DataBind();

                ddlTransactionType.Items.Insert(0, new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //btnExport.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.EXPORT);
                btnExport.Visible = true;
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
        gvLTWA.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvLTWA.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            string isschedule = "";
            divResult.Visible = true;
            ltrError.Text = string.Empty;
            DataTable dtTran = new DataTable();
            if (cbIsSchedule.Checked)
            {
                isschedule = "SCH";
            }
            DataSet dsTran = new SmartPortal.SEMS.Transactions().ViewLogTran(
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtTranID.Text.Trim()), 
                ddlTransactionType.SelectedValue, 
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFromDate.Text.Trim()), 
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtToDate.Text.Trim()), 
                ddlStatus.SelectedValue, SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtAccno.Text.Trim()),
                "ALL", SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtcustcode.Text.Trim()), 
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtcustname.Text.Trim()), 
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtcontractno.Text.Trim()),
                SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtTranRef.Text.Trim())
                , SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCreditAcct.Text.Trim()), 
                string.Empty, SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCMND.Text.Trim()), 
                isschedule, gvLTWA.PageSize, gvLTWA.PageIndex * gvLTWA.PageSize, 
                ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                dtTran = dsTran.Tables[0];
                gvLTWA.DataSource = dsTran;
                gvLTWA.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
            if (dsTran.Tables[0].Rows.Count > 0)
            {
                ltrError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dsTran.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
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
    protected void gvLTWA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            LinkButton lbTranID;
            CheckBox cbxSelect;
            Label lblDate, lblTrantype, lblAmount, lblCCYID, lblAccount, lblDesc, lblStatus, lblResult, lblBatchRef, lblRefCore, lblcustcodecore, lblErrDesc, lblFee, lblTotalAmount;
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
                //cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                lbTranID = (LinkButton)e.Row.FindControl("lbTranID");

                lblDate = (Label)e.Row.FindControl("lblDate");
                lblTrantype = (Label)e.Row.FindControl("lblTrantype");
                lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblCCYID = (Label)e.Row.FindControl("lblCCYID");
                lblAccount = (Label)e.Row.FindControl("lblAccount");
                lblFee = (Label)e.Row.FindControl("lblFee");
                lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
                lblDesc = (Label)e.Row.FindControl("lblDesc");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblResult = (Label)e.Row.FindControl("lblResult");
                lblBatchRef = (Label)e.Row.FindControl("lblBatchRef");
                lblRefCore = (Label)e.Row.FindControl("lblRefCore");
                lblcustcodecore = (Label)e.Row.FindControl("lblcustcodecore");
                //lblErrDesc = (Label)e.Row.FindControl("lblErrDesc");

                lbTranID.Text = drv["IPCTRANSID"].ToString();
                lblTrantype.Text = drv["PAGENAME"].ToString();
                lblDesc.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(drv["TRANDESC"].ToString());
                lblRefCore.Text = drv["CHAR20"].ToString();
                lblAccount.Text = drv["CHAR01"].ToString();
                lblBatchRef.Text = drv["BATCHREF"].ToString();
                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["NUM01"].ToString(), drv["CCYID"].ToString().Trim());
                lblFee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["NUM02"].ToString(), drv["CCYID"].ToString().Trim());
                double totalAmount;
                switch (drv["IPCTRANCODE"].ToString().Trim())
                {
                    case "C_CANCELCASHCODE":
                    case "SB_CANCELCASHCODE":
                    case "SW_CANCELCASHCODE":
                        totalAmount = Math.Abs(SmartPortal.Common.Utilities.Utility.isDouble(lblAmount.Text, true)) - SmartPortal.Common.Utilities.Utility.isDouble(lblFee.Text, true);
                        break;
                    default:
                        totalAmount = Math.Abs(SmartPortal.Common.Utilities.Utility.isDouble(lblAmount.Text, true)) + SmartPortal.Common.Utilities.Utility.isDouble(lblFee.Text, true);
                        break;
                }

                lblTotalAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(totalAmount.ToString(), drv["CCYID"].ToString().Trim());
                lblCCYID.Text = drv["CCYID"].ToString();
                lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["IPCTRANSDATE"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                lblcustcodecore.Text = drv["CUSTCODE"].ToString();
                //lblErrDesc.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(drv["ERRORDESC"].ToString());

                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.TRANSTATUS.BEGIN:
                        lblStatus.Text = Resources.labels.dangxuly;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.PAYMENTFAIL:
                        lblStatus.Text = Resources.labels.thanhtoanthatbai;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.FINISH:
                        lblStatus.Text = Resources.labels.hoanthanh;
                        lblStatus.Attributes.Add("class", "label-success");
                        switch (drv["APPRSTS"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                lblResult.Text = Resources.labels.duyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                lblResult.Text = Resources.labels.khongduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.WAITTINGCUST:
                                lblResult.Text = Resources.labels.choduyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.BEGIN:
                                lblResult.Text = Resources.labels.dangxuly;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.DEPOSIT:
                                lblStatus.Text = Resources.labels.dahoantien;
                                break;
                        }
                        break;
                    case SmartPortal.Constant.IPC.TRANSTATUS.WAITING_APPROVE:
                        lblStatus.Text = Resources.labels.choduyet;
                        lblStatus.Attributes.Add("class", "label-warning");
                        switch (drv["APPRSTS"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                lblResult.Text = Resources.labels.duyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                lblResult.Text = Resources.labels.khongduyet;
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
                        lblStatus.Text = Resources.labels.loi;
                        lblStatus.Attributes.Add("class", "label-warning");
                        switch (drv["APPRSTS"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.APPROVESTATUS.APPROVED:
                                lblResult.Text = Resources.labels.duyet;
                                break;
                            case SmartPortal.Constant.IPC.APPROVESTATUS.REJECTED:
                                lblResult.Text = Resources.labels.khongduyet;
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
                    case SmartPortal.Constant.IPC.TRANSTATUS.REJECTED:
                        lblStatus.Text = Resources.labels.khongduyet;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case "5":
                        lblStatus.Text = Resources.labels.conpending;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvLTWA.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvLTWA.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void bt_export_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLTWA.Rows.Count > 0)
            {
                Export("result.xls", gvLTWA, lblheaderFile.Text);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnRollback_Click(Object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DETAILS))
        {
            CheckBox cbxApprove;
            LinkButton lbTranID;
            List<string> lstTran = new List<string>();
            try
            {
                foreach (GridViewRow gvr in gvLTWA.Rows)
                {
                    cbxApprove = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxApprove.Checked == true)
                    {
                        lbTranID = (LinkButton)gvr.Cells[1].FindControl("lbTranID");
                        lstTran.Add(lbTranID.CommandArgument.Trim());
                    }
                }
            if (lstTran.Count != 0)
            {
                Session["tranID"] = lstTran;
                string pageid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString();
                string link = PagesBLL.GetLinkAction_Page(pageid, IPC.ACTIONPAGE.DETAILS) + "&ID="+ lstTran[0];
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(link),false);
                //RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, lstTran[0].ToString());
            }
            else
            {
                lblError.Text = Resources.labels.banvuilongchongiaodichcanduyet;
                return;
            }
        }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
            }
        }

    }
    public static void Export(string fileName, GridView gv, string headerFile)
    {
        try
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //gv.HeaderRow.Cells.RemoveAt(0);
                    Table table = new Table();
                    table.GridLines = gv.GridLines;
                    if (gv.HeaderRow != null)
                    {
                        PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        //row.Cells.RemoveAt(0);
                        //row.Cells[3].Controls.RemoveAt(2);
                        //   row.Cells[3].Font.Size = 20;
                        row.Cells[3].Text.ToString();
                        PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }
                    if (gv.FooterRow != null)
                    {
                        PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }
                    TableRow hd = new TableRow();
                    Label lbhder = new Label();
                    lbhder.Text = headerFile;
                    lbhder.Font.Size = 20;
                    lbhder.Font.Bold = true;
                    TableCell hdCell = new TableCell();
                    hdCell.Controls.Add(lbhder);
                    hdCell.Width = new Unit(150, UnitType.Pixel);
                    hdCell.Wrap = false;
                    hd.Cells.Add(hdCell);
                    hd.Cells[0].Controls.Add(lbhder);
                    table.Rows.AddAt(0, hd);
                    table.RenderControl(htw);

                    string style = @"<style> TD { mso-number-format:\@; } </style>";
                    HttpContext.Current.Response.Write(style);
                    HttpContext.Current.Response.Output.Write(sw.ToString());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }

            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }
    protected void gvLTWA_RowCommand(object sender, GridViewCommandEventArgs e)
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
            }
        }
    }
}