using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSCBTBank_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private int size = 0;

protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            litError.Text = "";
            if (!IsPostBack)
            {
                loadCurrency();
                loadNostro();
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAdd_New.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
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
    private void loadCurrency()
    {
        try
        {
            ddlCurrency.Items.Insert(0, new ListItem("ALL", ""));
            ddlCurrency.Items.Insert(1, new ListItem("USD", "USD"));
            ddlCurrency.Items.Insert(2, new ListItem("THB", "THB"));
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void loadNostro()
    {
        try
        {
            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            hasInput = new Hashtable(){
                {"IPCTRANCODE", "SEMSCBTBANKNOSTRO"},
                {"SERVICEID", "SEMS"},
                {"SOURCEID", "SEMS"}
            };
            hasOutput = new SmartPortal.SEMS.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
            DataSet dt = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];

            ddlNostro.DataSource = dt.Tables[0];
            ddlNostro.DataTextField = "Nostro";
            ddlNostro.DataValueField = "Nostro";
            ddlNostro.DataBind();
            ddlNostro.Items.Insert(0, new ListItem("ALL", ""));
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvBankList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvBankList.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {

        try
        {
            divResult.Visible = true;
            litError.Text = String.Empty;

            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvBankList.PageIndex * gvBankList.PageSize) return;
            
            DataSet dtCountry = new DataSet();

            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            hasInput = new Hashtable(){
                {"IPCTRANCODE", "SEMSCBTBANKSEARCH"},
                {"SERVICEID", "SEMS"},
                {"SOURCEID", "SEMS"},
                {"NOSTRO", Utility.KillSqlInjection(ddlNostro.SelectedValue.Trim())},
                {"SWIFTCODE", Utility.KillSqlInjection(txtSwiftCode.Text.Trim())},
                {"CURRENCY", Utility.KillSqlInjection(ddlCurrency.SelectedValue.Trim())},
                {"BANKNAME", Utility.KillSqlInjection(txtBankName.Text.Trim())},
                {"RECPERPAGE", gvBankList.PageSize},
                {"RECINDEX", gvBankList.PageIndex * gvBankList.PageSize}
            };
            hasOutput = new SmartPortal.SEMS.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
            dtCountry = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];

            if (IPCERRORCODE == "0")
            {
                gvBankList.DataSource = dtCountry;
                gvBankList.DataBind();
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
            if (dtCountry.Tables[0].Rows.Count > 0)
            {
                litError.Text = String.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dtCountry.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvBankList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label  lblBankName, lblCurrency, lblAddress, lblNostro ;
            LinkButton lbEdit, lbDelete , lblSwiftCode, lblBankId;
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


                lblBankName = (Label)e.Row.FindControl("lblBankName");
                lblNostro = (Label)e.Row.FindControl("lblNostro");
                lblCurrency = (Label)e.Row.FindControl("lblCurrency");
                lblAddress = (Label)e.Row.FindControl("lblAddress");


                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
                lblSwiftCode = (LinkButton)e.Row.FindControl("lblSwiftCode");
                lblBankId = (LinkButton)e.Row.FindControl("lblBankId");
                
                
                lblBankId.Text = drv["BankID"].ToString();
                lblSwiftCode.Text = drv["SwiftCode"].ToString();
                lblBankName.Text = drv["BankName"].ToString();
                lblNostro.Text = drv["Nostro"].ToString();
                lblCurrency.Text = drv["Currency"].ToString();
                lblAddress.Text = drv["Address"].ToString();

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
    protected void gvBankList_RowCommand(object sender, GridViewCommandEventArgs e)
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
    protected void gvBankList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvBankList.Rows[e.RowIndex].Cells[1].FindControl("lblBankId")).Text;
            //new SmartPortal.SEMS.Bank().DeleteCBTBank(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);

            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            hasInput = new Hashtable(){
                {"IPCTRANCODE", "SEMSCBTBANKDELETE"},
                {"SERVICEID", "SEMS"},
                {"SOURCEID", "SEMS"},
                {"BANKID", commandArg}
            };
            hasOutput = new SmartPortal.SEMS.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                BindData2();
                lblError.Text = Resources.labels.xoanganhangthanhcong;
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
    }
    protected void btnAdd_New_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }
    void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvBankList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvBankList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            CheckBox cbxSelect;
            LinkButton lbBankID;
            string strBankID = "";
            try
            {
                foreach (GridViewRow gvr in gvBankList.Rows)
                {
                    cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxSelect.Checked == true)
                    {
                        lbBankID = (LinkButton)gvr.Cells[1].FindControl("lblBankID");
                        strBankID += lbBankID.Text.Trim() + "#";
                    }
                }
                if (string.IsNullOrEmpty(strBankID))
                {
                    lblError.Text = Resources.labels.pleaseselectbeforedeleting;
                    return;
                }
                else
                {
                    string[] BankID = strBankID.Split('#');
                    for (int i = 0; i < BankID.Length - 1; i++)
                    {
                        Hashtable hasInput = new Hashtable();
                        Hashtable hasOutput = new Hashtable();

                        hasInput = new Hashtable(){
                            {"IPCTRANCODE", "SEMSCBTBANKDELETE"},
                            {"SERVICEID", "SEMS"},
                            {"SOURCEID", "SEMS"},
                            {"BANKID", BankID[i]}
                        };

                        hasOutput = new SmartPortal.SEMS.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (!IPCERRORCODE.Equals("0"))
                        {
                            break;
                        }
                    }
                    if (IPCERRORCODE.Equals("0"))
                    {
                        BindData2();
                        lblError.Text = Resources.labels.xoanganhangthanhcong;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
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
}