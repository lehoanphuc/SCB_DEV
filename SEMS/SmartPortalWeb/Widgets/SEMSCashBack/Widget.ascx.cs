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
using System.IO;
using SmartPortal.Common;
using SmartPortal.Constant;
using SmartPortal.BLL;

public partial class Widgets_SEMSCashBack_Widget : WidgetBase
{
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                //load tran app

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                GridViewPaging.Visible = false;
                BindData();
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
            ltrError.Text = string.Empty;
            DataTable dtTran = new DataTable();
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvLTWA.PageIndex * gvLTWA.PageSize) return;
            DataSet dsTran = new SmartPortal.SEMS.Transactions().LoadCashBackHis(SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtUserCreated.Text.Trim()), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtTranID.Text.Trim()), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFromDate.Text.Trim()), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtToDate.Text.Trim()), string.Empty, SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtToPhone.Text.Trim()), SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlStatus.SelectedValue.Trim()), gvLTWA.PageSize, gvLTWA.PageIndex * gvLTWA.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
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
            GridViewPaging.Visible = true;
            if (dsTran.Tables[0].Rows.Count > 0)
            {
                ltrError.Text = string.Empty;

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
            Label lblDate, lblAmount, lblFrmFullName, lblToFulName, lblToPhone, lblFrmPhone, lblStatus, lblccyid, lblUserCreated, lblDescription;
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                lbTranID = (LinkButton)e.Row.FindControl("lbTranID");
                lblFrmFullName = (Label)e.Row.FindControl("lblFrmFullName");
                lblToFulName = (Label)e.Row.FindControl("lblToFulName");
                lblToPhone = (Label)e.Row.FindControl("lblToPhone");
                lblFrmPhone = (Label)e.Row.FindControl("lblFrmPhone");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblDate = (Label)e.Row.FindControl("lblDate");
                lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblccyid = (Label)e.Row.FindControl("lblccyid");
                lblUserCreated = (Label)e.Row.FindControl("lblUserCreated");
                lblDescription = (Label)e.Row.FindControl("lblDescription");
                
                lbTranID.Text = drv["TRANID"].ToString();
                lblFrmFullName.Text= drv["FROMFULLNAME"].ToString();
                lblToFulName.Text = drv["TOFULLNAME"].ToString();
                lblToPhone.Text = drv["TOPHONE"].ToString();
                lblFrmPhone.Text = drv["FROMPHONE"].ToString();
                lblccyid.Text = drv["CCYID"].ToString();
                lblUserCreated.Text = drv["USERCREATED"].ToString();
                lblDescription.Text = drv["Description"].ToString();

                lblAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["AMOUNT"].ToString(),SmartPortal.Constant.IPC.LAK);
                lblDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["TRANDATE"].ToString()).ToString("dd/MM/yyyy HH:mm");

                lblStatus.Text = drv["STATUS"].ToString();
                lblStatus.Attributes.Add("class","label-success");
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
    protected void bt_Upload_Click(object sender, EventArgs e)
    {
        try
        {
            string pageid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString();
            string link = PagesBLL.GetLinkAction_Page(pageid, IPC.ACTIONPAGE.UPLOAD);
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(link), false);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
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
                    gv.HeaderRow.Cells.RemoveAt(0);
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
                        row.Cells.RemoveAt(0);
                        row.Cells[3].Controls.RemoveAt(2);
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
                    HttpContext.Current.Response.Write(sw.ToString());
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
    protected void gvLTWA_OnRowCommand(object sender, GridViewCommandEventArgs e)
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