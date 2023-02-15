using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.Model;

public partial class Widgets_SEMSPRODUCTROLE_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            litError.Text = "";
            litPager.Text = "";
            lblError.Text = "";
            if (!IsPostBack)
            {
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                LoadDll();
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    void LoadDll()
    {
        DataSet ListContractLevel = new SmartPortal.SEMS.ContractLevel().GetAllContractLevel("", "", "A", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            ddlContractLevel.DataSource = ListContractLevel;
            ddlContractLevel.DataTextField = "CONTRACT_LEVEL_NAME";
            ddlContractLevel.DataValueField = "CONTRACT_LEVEL_ID";
            ddlContractLevel.DataBind();
            ddlContractLevel.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));
        }
        else
        {
            lblError.Text = IPCERRORDESC;
        }
    }
    void BindData()
    {
        try
        {
            DataSet dtCL = new DataSet();
            dtCL = new SmartPortal.SEMS.Product().GetProductRole(Utility.KillSqlInjection(txtmasp.Text.Trim()), Utility.KillSqlInjection(txttensp.Text.Trim()), Utility.KillSqlInjection(ddlContractLevel.SelectedValue.ToString().Trim()), ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                gvProductList.DataSource = dtCL;
                gvProductList.DataBind();
            }
            else
            {
                //lblError.Text = IPCERRORDESC;
            }

            if (dtCL.Tables[0].Rows.Count > 0)
            {
                int startRow = gvProductList.PageSize * gvProductList.PageIndex + 1;
                int endRow = gvProductList.PageSize * (gvProductList.PageIndex + 1);
                int totalRecords = dtCL.Tables[0].Rows.Count;
                if (totalRecords >= endRow)
                {
                    litPager.Text = Resources.labels.danghienthi + " <b>" + startRow.ToString() + "</b> - <b>" + endRow.ToString() + "</b> " + Resources.labels.cua + " <b>" + totalRecords.ToString() + "</b> " + Resources.labels.dong;
                }
                else
                {
                    litPager.Text = Resources.labels.danghienthi + " <b>" + startRow.ToString() + "</b> - <b>" + totalRecords.ToString() + "</b> " + Resources.labels.cua + " <b>" + totalRecords.ToString() + "</b> " + Resources.labels.dong;
                }
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblProductName, lblCustType, lblProductType, lblContractLevelName;
            LinkButton lblProductCode, lbEdit, lbDelete;

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
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                lblProductCode = (LinkButton)e.Row.FindControl("lblProductCode");
                lblProductName = (Label)e.Row.FindControl("lblProductName");
                lblCustType = (Label)e.Row.FindControl("lblCustType");
                lblProductType = (Label)e.Row.FindControl("lblProductType");
                lblContractLevelName = (Label)e.Row.FindControl("lblContractLevelName");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                lblProductCode.Text = drv["ProductID"].ToString();
                if (drv["CustType"].ToString() == SmartPortal.Constant.IPC.PERSONAL)
                {
                    lblCustType.Text = Resources.labels.canhan;
                }
                else if (drv["CustType"].ToString() == SmartPortal.Constant.IPC.CORPORATE)
                {
                    lblCustType.Text = Resources.labels.doanhnghiep;
                }
                lblProductName.Text = drv["ProductName"].ToString();

                if (drv["ProductType"].ToString() == IPC.AGENTMERCHANT)
                {
                    lblProductType.Text = Resources.labels.agentmerchant;
                }
                else if (drv["ProductType"].ToString() == IPC.CONSUMER)
                {
                    lblProductType.Text = Resources.labels.Consumer;
                }

                lblContractLevelName.Text = drv["CONTRACT_LEVEL_NAME"].ToString();

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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvProductList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            CheckBox cbxDelete;
            LinkButton lblProductCode;
            string strProductCode = "";
            try
            {
                foreach (GridViewRow gvr in gvProductList.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lblProductCode = (LinkButton)gvr.Cells[1].FindControl("lblProductCode");
                        strProductCode += lblProductCode.CommandArgument.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strProductCode))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] ProductCode = strProductCode.Split('#');
                    for (int i = 0; i < ProductCode.Length - 1; i++)
                    {
                        string[] key = ProductCode[i].Split('|');
                        new SmartPortal.SEMS.Product().DeleteRole(key[0], Convert.ToDecimal(key[1].ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            BindData();
                            lblError.Text = Resources.labels.xoaquyenchosanphamthanhcong;
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
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvProductList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }
    protected void gvProductList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvProductList.Rows[e.RowIndex].Cells[1].FindControl("lblProductCode")).CommandArgument;
            string[] key = commandArg.Split('|');
            new SmartPortal.SEMS.Product().DeleteRole(key[0], Convert.ToDecimal(key[1].ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData();
                lblError.Text = Resources.labels.xoaquyenchosanphamthanhcong;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
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