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
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_SEMSProductFee_Widget : WidgetBase
{
    public static bool isAscend = false;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;

            if (!IsPostBack)
            {
                ddlproducttype.DataSource = new SmartPortal.SEMS.Product().GetProductByCondition("", "", "P", "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                ddlproducttype.DataTextField = "PRODUCTNAME";
                ddlproducttype.DataValueField = "PRODUCTID";
                ddlproducttype.DataBind();
                ddlproducttype.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));

                ddltran.DataSource = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISPRODUCTFEE), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                ddltran.DataTextField = "PAGENAME";
                ddltran.DataValueField = "TRANCODE";
                ddltran.DataBind();
                ddltran.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));

                ddlccyid.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlccyid.DataTextField = "CCYID";
                ddlccyid.DataValueField = "CCYID";
                ddlccyid.DataBind();
                ddlccyid.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
                DataSet ds = new SmartPortal.SEMS.Fee().SearchFee(string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    ddlFee.DataSource = ds;
                    ddlFee.DataTextField = "FEENAME";
                    ddlFee.DataValueField = "FEEID";
                    ddlFee.DataBind();
                    ddlFee.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                //load các giao dịch
                ddlfeetype.DataSource = new SmartPortal.SEMS.Fee().LoadFeeType(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlfeetype.DataTextField = "TYPENAME";
                ddlfeetype.DataValueField = "FEETYPE";
                ddlfeetype.DataBind();
                ddlfeetype.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
                ddlfeetype.Items.Remove(ddlfeetype.Items.FindByValue("INTERBANK"));

                // DataSet ListContractLevel = new SmartPortal.SEMS.Product().LoadContractLevelActive(ref IPCERRORCODE, ref IPCERRORDESC);
                // if (IPCERRORCODE == "0")
                // {
                    // ddlContractLevel.DataSource = ListContractLevel;
                    // ddlContractLevel.DataTextField = "ContractLevelName";
                    // ddlContractLevel.DataValueField = "ContractLevelID";
                    // ddlContractLevel.DataBind();
                    // ddlContractLevel.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));
                // }
                // else
                // {
                    // lblError.Text = IPCERRORDESC;
                // }

                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                GridViewPaging.Visible = false;
                divResult.Visible = false;
            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvProductList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvProductList.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void BindData()
    {
        try
        {
            divResult.Visible = true;
            ltrError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvProductList.PageIndex * gvProductList.PageSize) return;

            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.Fee().SearchProductFee(Utility.KillSqlInjection(ddlproducttype.SelectedValue), 
			Utility.KillSqlInjection(ddltran.SelectedValue), Utility.KillSqlInjection(ddlFee.SelectedValue), Utility.KillSqlInjection(ddlccyid.SelectedValue),
			"ALL" , Utility.KillSqlInjection(ddlfeetype.SelectedValue), "ALL",
			gvProductList.PageSize, gvProductList.PageIndex * gvProductList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                gvProductList.DataSource = ds;
                gvProductList.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ltrError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                ltrError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblTrans, lblFeeType, labelFeeType, lblproductid, lbltrancode, lblfeeid, lblccyid, lblContractLevelName , lblFeeShareCode;
            LinkButton lblProductName, lbEdit, lbDelete;

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
                cbxSelect.Attributes.Add("onclick", "ChildClick(this);");
                lblProductName = (LinkButton)e.Row.FindControl("lblProductName");
                lblTrans = (Label)e.Row.FindControl("lblTrans");
                labelFeeType= (Label)e.Row.FindControl("labelFeeType");
                lblFeeType = (Label)e.Row.FindControl("lblFeeType");
                lblproductid = (Label)e.Row.FindControl("lblproductid");
                lbltrancode = (Label)e.Row.FindControl("lbltrancode");
                lblfeeid = (Label)e.Row.FindControl("lblfeeid");
                lblccyid = (Label)e.Row.FindControl("lblccyid");
                // lblContractLevelName = (Label)e.Row.FindControl("lblContractLevelName");
                lblFeeShareCode = (Label)e.Row.FindControl("lblFeeShareCode");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");


                labelFeeType.Text = drv["TYPENAME"].ToString();
                lblProductName.Text = drv["PRODUCTNAME"].ToString();
                lblTrans.Text = drv["PAGENAME"].ToString();
                lblFeeType.Text = drv["FEENAME"].ToString();
                lblproductid.Text = drv["PRODUCTID"].ToString();
                lbltrancode.Text = drv["TRANCODE"].ToString();
                lblfeeid.Text = drv["FEEID"].ToString();
                lblccyid.Text = drv["CCYID"].ToString();
                lblFeeShareCode.Text = drv["FeeShareCode"].ToString();
                // lblContractLevelName.Text = drv["ContractLevelName"].ToString();

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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData2();
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
            commandArg = ((LinkButton)gvProductList.Rows[e.RowIndex].Cells[1].FindControl("lblProductName")).CommandArgument;
            string[] pros = commandArg.Split('|');
            new SmartPortal.SEMS.Fee().DeleteProductFee(pros[0], pros[1], pros[2], pros[3], pros[4], pros[5], ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                BindData2();
                lblError.Text = Resources.labels.xoaphichosanphamthanhcong;
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            CheckBox cbxDelete;
            LinkButton lblProductName;
            string strProductCode = "";
            try
            {
                foreach (GridViewRow gvr in gvProductList.Rows)
                {
                    cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxDelete.Checked == true)
                    {
                        lblProductName = (LinkButton)gvr.Cells[1].FindControl("lblProductName");
                        strProductCode += lblProductName.CommandArgument.Trim() + "#";
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
                        string[] pros = ProductCode[i].Split('|');
                       new SmartPortal.SEMS.Fee().DeleteProductFee(pros[0], pros[1], pros[2], pros[3], pros[4], pros[5], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE.Equals("0"))
                        {
                            BindData2();
                            lblError.Text = Resources.labels.xoaphichosanphamthanhcong;
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
    protected void ddlccyid_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.Fee().SearchFee(string.Empty, string.Empty, string.Empty, Utility.KillSqlInjection(ddlccyid.SelectedValue),0,0, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlFee.DataSource = ds;
                ddlFee.DataTextField = "FEENAME";
                ddlFee.DataValueField = "FEEID";
                ddlFee.DataBind();
                ddlFee.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    void BindData2()
    {
        try
        {
            ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text = "1";
            ((HiddenField)GridViewPaging.FindControl("hdfCurrentPage")).Value = "1";
            gvProductList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvProductList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void ddlproducttype_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlproducttype.SelectedValue.ToString() == "ALL")
            {
                ddltran.DataSource = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                ddltran.DataTextField = "PAGENAME";
                ddltran.DataValueField = "TRANCODE";
                ddltran.DataBind();
                ddltran.Items.Insert(0, new ListItem(Resources.labels.tatcagiaodich, "ALL"));
                ddltran.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
            }
            else
            {
                ddltran.DataSource = new SmartPortal.SEMS.Product().GetTranNameByProduct(Utility.KillSqlInjection(ddlproducttype.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                ddltran.DataTextField = "PAGENAME";
                ddltran.DataValueField = "TRANCODE";
                ddltran.DataBind();
                ddltran.Items.Insert(0, new ListItem(Resources.labels.tatcagiaodich, "ALL"));
                ddltran.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
