using SmartPortal.Common;
using SmartPortal.Constant;
using SmartPortal.SEMS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
public partial class Widgets_SEMSBiller_Widget : WidgetBase
{
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    private int size = 0;
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                //btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                //btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                GridViewPaging.Visible = false;
                divResult.Visible = false;
            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name,
                MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    #endregion
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        try
        {
            gvBillList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            gvBillList.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
            BindData();
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void BindData()
    {
        try
        {
            divResult.Visible = true;
            litError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvBillList.PageIndex * gvBillList.PageSize) return;

            string id = Utility.KillSqlInjection(txtBillerID.Text.Trim());
            string code = Utility.KillSqlInjection(txtBillerCode.Text.Trim());
            string name = Utility.KillSqlInjection(txtBillerName.Text.Trim());
            string sort = Utility.KillSqlInjection(txtShortName.Text.Trim());
            string master = Utility.KillSqlInjection(txtName.Text.Trim());
            DataSet ds = new Biller().SearchBillByCondition(id, code, name, sort, master, gvBillList.PageSize, gvBillList.PageIndex * gvBillList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvBillList.DataSource = ds;
                gvBillList.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
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
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    #region  ROWDATABOUND
    protected void gvBillList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton lbBillerID, lbEdit, lbDelete;
            Label lblBillerCode, lblBillerName, lblShortName, lblStatus, lblMasterName;
            Image lbLogoBin;
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
                TableRow tbRow = (TableRow)e.Row.Cells[0].Controls[0].Controls[0];
                tbRow.Cells.AddAt(0, new TableCell());
                tbRow.Cells[0].Text = "<strong>" + Resources.labels.page + "</strong>";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                lbBillerID = (LinkButton)e.Row.FindControl("lbBillerID");
                lblBillerCode = (Label)e.Row.FindControl("lblBillerCode");
                lblBillerName = (Label)e.Row.FindControl("lblBillerName");
                lblShortName = (Label)e.Row.FindControl("lblShortName");
                lbLogoBin = (Image)e.Row.FindControl("lbLogoBin");
                lblMasterName = (Label)e.Row.FindControl("lblMasterName");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");


                lbBillerID.Text = drv["BillerID"].ToString();
                lblBillerCode.Text = drv["BillerCode"].ToString();
                lblBillerName.Text = drv["BillerName"].ToString();
                lblShortName.Text = drv["ShortName"].ToString();
                if (string.IsNullOrEmpty(drv["LogoBin"].ToString()))
                {
                    lbLogoBin.Visible = false;
                }
                else
                {
                    lbLogoBin.ImageUrl = drv["LogoBin"].ToString();
                }
                lblMasterName.Text = drv["UserCreated"].ToString();
                lblStatus.Text = drv["Status"].ToString();
                switch (drv["STATUS"].ToString().Trim())
                {
                    case IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.active;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case IPC.INACTIVE:
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
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    #endregion
    #region btnADD
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }
    #endregion
    #region btnDELETE
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        LinkButton lbCCYID;
        string strCCYID = "";
        try
        {
            foreach (GridViewRow gvr in gvBillList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    lbCCYID = (LinkButton)gvr.Cells[1].FindControl("lbBillerID");
                    strCCYID += lbCCYID.CommandArgument.ToString().Trim() + "#";
                }
            }

            if (string.IsNullOrEmpty(strCCYID))
            {
                lblError.Text = Resources.labels.pleaseselectbeforedeleting;
            }
            else
            {
                string[] ccyid = strCCYID.Split('#');
                for (int i = 0; i < ccyid.Length - 1; i++)
                {
                    new Biller().Delete(ccyid[i], ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0"))
                    {
                        BindData2();
                        lblError.Text = Resources.labels.deletebillersuccessfully;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    #endregion
    protected void gvBillList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArgument = e.CommandArgument.ToString();
        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + IPC.ID + "=" + commandArgument);
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + IPC.ID + "=" + commandArgument);
                    break;
            }
        }
    }
    #region btnSEARCH
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvBillList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvBillList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    #endregion
    protected void gvBillList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvBillList.Rows[e.RowIndex].Cells[1].FindControl("lbBillerID")).CommandArgument;
            new Biller().Delete(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                BindData2();
                lblError.Text = Resources.labels.deletebillersuccessfully;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }

        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvBillList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvBillList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            Log.RaiseError(ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, MethodBase.GetCurrentMethod().Name, ex.Message, "");
            Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}