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

public partial class Widgets_SEMSCustFXLimit_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string EDITPAGEID = "1096";
    string VIEWPAGEID = "1097";
    string ADDPAGEID = "1095";

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        litError.Text = "";
        litPager.Text = "";
        if (!IsPostBack)
        {
            DataTable dt = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
            #region Load From CCY
            ddlFromCCY.DataSource = dt;
            ddlFromCCY.DataTextField = "CCYID";
            ddlFromCCY.DataValueField = "CCYID";
            ddlFromCCY.DataBind();
            ddlFromCCY.Items.Insert(0, new ListItem(SmartPortal.Constant.IPC.ALL, "ALL"));
            #endregion
            #region Load To CCY
            ddlToCCY.DataSource = dt;
            ddlToCCY.DataTextField = "CCYID";
            ddlToCCY.DataValueField = "CCYID";
            ddlToCCY.DataBind();
            ddlToCCY.Items.Insert(0, new ListItem(SmartPortal.Constant.IPC.ALL, "ALL"));
            #endregion
            //#region Load Transaction
            //DataSet dsTran = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
            //dsTran.Tables[0].DefaultView.RowFilter = ("TranCode in ('IB000405','MB000103')");
            //ddltran.DataSource = dsTran.Tables[0];
            //if (IPCERRORCODE != "0")
            //{
            //    throw new IPCException(IPCERRORDESC);
            //}

            //ddltran.DataTextField = "PAGENAME";
            //ddltran.DataValueField = "TRANCODE";
            //ddltran.DataBind();
            //ddltran.Items.Insert(0, new ListItem(SmartPortal.Constant.IPC.ALL, "ALL"));
            //#endregion
            ddlFx.Items.Add(new ListItem(SmartPortal.Constant.IPC.ALL, SmartPortal.Constant.IPC.ALL));
            ddlFx.Items.Add(new ListItem("Yes", SmartPortal.Constant.IPC.YES));
            ddlFx.Items.Add(new ListItem("No", SmartPortal.Constant.IPC.NO));
            BindData();
        }
    }

    void BindData()
    {
        try
        {
            DataSet dtProLim = new DataSet();
            dtProLim = new SmartPortal.SEMS.FxTransactionLimit().GetListFXTransactionLimitByCondition(
                Utility.KillSqlInjection(txtContractNo.Text.Trim()),
                Utility.KillSqlInjection(txtFullName.Text.Trim()),
                string.Empty,
                //Utility.KillSqlInjection(ddltran.SelectedValue.ToString().Trim()),
                Utility.KillSqlInjection(ddlFromCCY.SelectedValue.ToString().Trim()),
                Utility.KillSqlInjection(ddlToCCY.SelectedValue.ToString().Trim()),
                Utility.KillSqlInjection(ddlFx.SelectedValue.ToString().Trim()),
                ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                gvCustomerList.DataSource = dtProLim;
                gvCustomerList.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            if (dtProLim.Tables[0].Rows.Count > 0)
            {
                litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvCustomerList.PageIndex) * gvCustomerList.PageSize) + gvCustomerList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtProLim.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
            }
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
            HyperLink hpContract;
            Label lblOrder;
            Label lblFullName;
            Label lblLimitTran;
            Label lblNumTran;
            Label lblLimitTotal;
            Label lblFromCCY;
            Label lblToCCY;
            HyperLink hpEdit;
            HyperLink hpDelete;
            Label lblTranName;
            DataRowView drv;
            Label lblfx;
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

                //lblOrder = (Label)e.Row.FindControl("Order");
                hpContract = (HyperLink)e.Row.FindControl("hpContract");
                //lblFullName = (Label)e.Row.FindControl("FullName");
                lblLimitTran = (Label)e.Row.FindControl("LimitTran");
                lblNumTran = (Label)e.Row.FindControl("NumberTran");
                lblLimitTotal = (Label)e.Row.FindControl("LimitTotal");
                lblFromCCY = (Label)e.Row.FindControl("FromCCY");
                lblToCCY = (Label)e.Row.FindControl("ToCCY");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                lblTranName = (Label)e.Row.FindControl("TranName");
                lblfx = (Label)e.Row.FindControl("fx");
                cbxSelect.Enabled = true;
                cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");
                //lblOrder.Text = Convert.ToInt32(drv["ORDER"].ToString()).ToString("###.###.###");
                hpContract.Text = drv["CONTRACTNO"].ToString();
                hpContract.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + VIEWPAGEID + "&a=viewdetail&contract=" + drv["CONTRACTNO"].ToString() + "&trand=" + drv["TRANDCODE"].ToString() + "&fromccy=" + drv["FROMCCYID"].ToString() + "&toccy=" + drv["TOCCYID"].ToString());
                //lblFullName.Text = drv["FULLNAME"].ToString();
                lblLimitTran.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TRANLIMIT"].ToString(), drv["FROMCCYID"].ToString().Trim());
                lblNumTran.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.IsInt(drv["COUNTLIMIT"].ToString()).ToString(), "").Replace(".00", "");
                lblLimitTotal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TOTALLIMITDAY"].ToString(), drv["FROMCCYID"].ToString().Trim());
                lblFromCCY.Text = drv["FROMCCYID"].ToString();
                lblToCCY.Text = drv["TOCCYID"].ToString();
                //lblTranName.Text= drv["PAGENAME"].ToString();
                lblfx.Text = drv["ALLOWFOREIGN"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().Name, "gvCustomerList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvCustomerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCustomerList.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=" + ADDPAGEID + "&a=add"), false);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        BindData();
    }

    protected void EditFX_Click(object sender, EventArgs e)
    {
        try
        {
            string argrument = ((sender as LinkButton).CommandArgument).ToString();
            if (!string.IsNullOrWhiteSpace(argrument))
            {
                string[] lstargrument = argrument.Split('#');
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + EDITPAGEID + "&a=edit&contract=" + lstargrument[0].ToString() + "&trand=" + lstargrument[1].ToString() + "&fromccy=" + lstargrument[2].ToString() + "&toccy=" + lstargrument[3].ToString()), false);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().Name, "EditFX_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void DeleteFX_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            DataTable dtFX = new DataTable();
            dtFX.Columns.Add("CONTRACTNO");
            dtFX.Columns.Add("TRANDCODE");
            dtFX.Columns.Add("FROMCCYID");
            dtFX.Columns.Add("TOCCYID");
            dtFX.Columns.Add("USER");
            dtFX.Columns.Add("DATE");
            dtFX.PrimaryKey = new DataColumn[] { dtFX.Columns["CONTRACTNO"], dtFX.Columns["TRANDCODE"], dtFX.Columns["FROMCCYID"], dtFX.Columns["TOCCYID"] };
            string argrument = ((sender as LinkButton).CommandArgument).ToString();
            if (!string.IsNullOrWhiteSpace(argrument))
            {
                DataRow row = dtFX.NewRow();
                string[] lstargrument = argrument.Split('#');
                row["CONTRACTNO"] = lstargrument[0];
                row["TRANDCODE"] = lstargrument[1];
                row["FROMCCYID"] = lstargrument[2];
                row["TOCCYID"] = lstargrument[3];
                row["USER"] = Session["userName"].ToString();
                row["DATE"] = DateTime.Now;
                dtFX.Rows.Add(row);
                new SmartPortal.SEMS.FxTransactionLimit().FXLimitDelete(dtFX, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.xoathanhcong;
                }
                else
                {
                    goto EXIT;
                }
            }
            BindData();
            return;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().Name, "DeleteFX_Click", IPCex.ToString(), Request.Url.Query);
            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().Name, "DeleteFX_Click", ex.ToString(), Request.Url.Query);
            goto EXIT;
        }
    EXIT:
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            DataTable dtFX = new DataTable();
            dtFX.Columns.Add("CONTRACTNO");
            dtFX.Columns.Add("TRANDCODE");
            dtFX.Columns.Add("FROMCCYID");
            dtFX.Columns.Add("TOCCYID");
            dtFX.Columns.Add("USER");
            dtFX.Columns.Add("DATE");
            dtFX.PrimaryKey = new DataColumn[] { dtFX.Columns["CONTRACTNO"], dtFX.Columns["TRANDCODE"], dtFX.Columns["FROMCCYID"], dtFX.Columns["TOCCYID"] };

            foreach (GridViewRow row in gvCustomerList.Rows)
            {
                CheckBox cbx = (CheckBox)row.Cells[0].Controls[1];
                if (cbx.Checked)
                {
                    string argrument = ((LinkButton)row.Cells[9].Controls[1]).CommandArgument;
                    if (!string.IsNullOrWhiteSpace(argrument))
                    {
                        DataRow rowFX = dtFX.NewRow();
                        string[] lstargrument = argrument.Split('#');
                        rowFX["CONTRACTNO"] = lstargrument[0];
                        rowFX["TRANDCODE"] = lstargrument[1];
                        rowFX["FROMCCYID"] = lstargrument[2];
                        rowFX["TOCCYID"] = lstargrument[3];
                        rowFX["USER"] = Session["userName"].ToString();
                        rowFX["DATE"] = DateTime.Now;
                        dtFX.Rows.Add(rowFX);
                    }
                }
            }
            new SmartPortal.SEMS.FxTransactionLimit().FXLimitDelete(dtFX, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.xoathanhcong;
            }
            else
            {
                goto Exit;
            }
            BindData();
            return;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().Name, "btnDelete_Click", IPCex.ToString(), Request.Url.Query);
            goto Exit;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().Name, "btnDelete_Click", ex.ToString(), Request.Url.Query);
            goto Exit;
        }
    Exit:
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
    }
}
