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
using System.Globalization;
using SmartPortal.Constant;

public partial class Widgets_SEMSContractList_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    private int size = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                btnBlock.Visible = CheckPermitPageAction("BLOCK");
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                GridViewPaging.Visible = false;

                #region load loai hop dong
                DataSet dsUserType = new DataSet();
                dsUserType = new SmartPortal.SEMS.Services().GetUserType("", "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    //DataTable dtUserType = new DataTable();
                    //dtUserType = dsUserType.Tables[0];
					//
                    //ddlUserType.DataSource = dtUserType;
                    //ddlUserType.DataTextField = "SUBUSERTYPE";
                    //ddlUserType.DataValueField = "SUBUSERCODE";
                    //ddlUserType.DataBind();
					//
                    //ddlUserType.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                #endregion

                #region hien thị status
                ddlStatus.Items.Add(new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                ddlStatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
                ddlStatus.Items.Add(new ListItem(Resources.labels.conactive, SmartPortal.Constant.IPC.ACTIVE));
                ddlStatus.Items.Add(new ListItem(Resources.labels.condelete, SmartPortal.Constant.IPC.DELETE));
                ddlStatus.Items.Add(new ListItem(Resources.labels.conblock, SmartPortal.Constant.IPC.BLOCK));
                ddlStatus.Items.Add(new ListItem(Resources.labels.conpending, SmartPortal.Constant.IPC.PENDING));
                ddlStatus.Items.Add(new ListItem(Resources.labels.conreject, SmartPortal.Constant.IPC.REJECT));
                ddlStatus.Items.Add(new ListItem(Resources.labels.pendingfordelete, SmartPortal.Constant.IPC.PENDINGFORDELETE));
                ddlStatus.Items.Add(new ListItem(Resources.labels.pendingforapprove, SmartPortal.Constant.IPC.PENDINGFORAPPROVE));

                #endregion
                loadProductType();
                divresult.Visible = false;
                //   BindData();


            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvcontractList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvcontractList.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void loadProductType()
    {
        try
        {
            ddlproducttype.Items.Clear();
            ddlproducttype.DataSource = new SmartPortal.SEMS.Product().GetProductByCondition("", "", "P", "", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            ddlproducttype.DataTextField = "PRODUCTNAME";
            ddlproducttype.DataValueField = "PRODUCTID";
            ddlproducttype.DataBind();
            
            ddlproducttype.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        
    }
    void BindData()
    {
        try
        {
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvcontractList.PageIndex * gvcontractList.PageSize) return;
            DataSet dtContr = new DataSet();
            dtContr = new SmartPortal.SEMS.Contract().GetContractByCondition(txtContractCode.Text.Trim(), txtCustName.Text.Trim(), txtopenPer.Text.Trim(), txtOpenDate.Text.Trim(), txtEndDate.Text.Trim(), "0603", ddlStatus.SelectedValue, txtlicenseid.Text.Trim(), txtcustcode.Text.Trim(), txtPhone.Text.Trim(), ddlproducttype.SelectedValue , gvcontractList.PageSize, gvcontractList.PageIndex * gvcontractList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvcontractList.DataSource = dtContr;
                gvcontractList.DataBind();
            }
            else
            {
                goto ERROR;
            }
            if (dtContr.Tables[0].Rows.Count > 0)
            {
                divresult.Visible = true;
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dtContr.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }

            //litPager.Text = Resources.labels.danghienthi + " <b>" + (((gvcontractList.PageIndex) * gvcontractList.PageSize) + gvcontractList.Rows.Count).ToString() + "</b> " + Resources.labels.cua + " <b>" + dtContr.Tables[0].Rows.Count.ToString() + "</b> " + Resources.labels.dong;

            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_Widget", "BindData", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:

        ;
    }

    protected void gvcontractList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            LinkButton hpcontractCode;
            Label lblcustName;
            Label lblOpen;
            Label lblOpendate;
            Label lblClosedate;
            Label lblcontractType;
            Label lblStatus;
            Label lbllicense;
            Label lblcustcode;
            Label lblcorpType;
            Label lblPhone;

            LinkButton hpEdit;
            LinkButton hpDelete;
            HyperLink hpReview;

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

                // if (drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE || drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.BLOCK)
                if (drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE || drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.PENDINGFORDELETE|| drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.PENDINGFORAPPROVE || drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.PENDING || drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.BLOCK)
                {
                    cbxSelect.Enabled = false;
                }

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                hpcontractCode = (LinkButton)e.Row.FindControl("hpcontractCode");
                lblcustName = (Label)e.Row.FindControl("lblcustName");
                lblOpen = (Label)e.Row.FindControl("lblOpen");
                lblOpendate = (Label)e.Row.FindControl("lblOpendate");
                lblClosedate = (Label)e.Row.FindControl("lblClosedate");
                lblcontractType = (Label)e.Row.FindControl("lblcontractType");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lbllicense = (Label)e.Row.FindControl("lbllicense");
                lblcustcode = (Label)e.Row.FindControl("lblcustcode");
                lblcorpType = (Label)e.Row.FindControl("lblcorpType");
                lblPhone = (Label)e.Row.FindControl("lblPhone");


                hpEdit = (LinkButton)e.Row.FindControl("hpEdit");
                hpDelete = (LinkButton)e.Row.FindControl("hpDelete");
                hpReview = (HyperLink)e.Row.FindControl("hpReview");


                hpcontractCode.Text = drv["ContractNo"].ToString();
                //if (drv["CONTRACTTYPE"].ToString().Trim() == SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL || drv["CONTRACTTYPE"].ToString().Trim() == SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT)
                //{
                //    hpcontractCode.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=146&a=viewdetail&cn=" + drv["ContractNo"].ToString());
                //}
                //else
                //{
                //    hpcontractCode.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=247&a=viewdetail&cn=" + drv["ContractNo"].ToString());
                //}

                lblcorpType.Text = drv["CONTRACTTYPE"].ToString();

                lblcustName.Text = drv["FULLNAME"].ToString();
                lblOpen.Text = drv["UserCreate"].ToString();
                lblOpendate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["CreateDate"].ToString()).ToString("dd/MM/yyyy");
                lblClosedate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["EndDate"].ToString()).ToString("dd/MM/yyyy");
                lbllicense.Text = drv["LICENSEID"].ToString();
                lblcustcode.Text = drv["CUSTCODE"].ToString();
                lblPhone.Text = drv["TEL"].ToString();
                hpReview.Text = Resources.labels.xemlai;
                hpReview.ToolTip = Resources.labels.xemlai;
                hpReview.NavigateUrl = "~/widgets/semscontractreview/registercontract.aspx?cn=" + drv["ContractNo"].ToString() + "&ct=" + drv["USERTYPE"].ToString() ;
                hpReview.Target = "_Blank";

                DataSet dsUserType = new DataSet();
                dsUserType = new SmartPortal.SEMS.Services().GetUserType(drv["USERTYPE"].ToString().Trim(), "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    if (dtUserType.Rows.Count != 0)
                    {
                        lblcontractType.Text = dtUserType.Rows[0]["SUBUSERTYPE"].ToString();
                    }
                }
                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = Resources.labels.connew;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = Resources.labels.condelete;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.conactive;
                        lblStatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblStatus.Text = Resources.labels.conblock;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblStatus.Text = Resources.labels.conpending;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblStatus.Text = Resources.labels.conreject;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        lblStatus.Text = Resources.labels.pendingfordelete;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORAPPROVE:
                        lblStatus.Text = Resources.labels.pendingforapprove;
                        lblStatus.Attributes.Add("class", "label-warning");
                        break;
                }
                bool bAllowTellerEdit = false;
                try
                {
                    //bAllowTellerEdit = System.Configuration.ConfigurationManager.AppSettings["AllowTellerEditContract"].ToString().Equals("Y");
                }
                catch { }
                //if (((int.Parse(Session["userLevel"].ToString().Trim()) > 1) || bAllowTellerEdit) && Session["branch"].ToString().Trim() == drv["BRANCHID"].ToString().Trim())
                //{
                //    if (drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE || drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.PENDINGFORDELETE || drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.PENDING || drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.BLOCK)
                //    {
                //        hpEdit.Enabled = false;
                //        hpDelete.Enabled = false;
                //        hpEdit.Text = Resources.labels.edit;
                //        hpDelete.Text = Resources.labels.delete;
                //    }
                //    else
                //    {
                //        hpEdit.Text = Resources.labels.edit;
                //        hpEdit.ToolTip = Resources.labels.edit;
                //        if (int.Parse(Session["userLevel"].ToString().Trim()) > 1)
                //        {
                //            hpDelete.Text = Resources.labels.delete;
                //            hpDelete.ToolTip = Resources.labels.delete;
                //        }
                //        else
                //        {
                //            hpDelete.Text = Resources.labels.delete;
                //            hpDelete.Enabled = false;
                //        }
                //    }
                //}
                //else
                //{
                //    hpEdit.Text = Resources.labels.edit;
                //    hpDelete.Text = Resources.labels.delete;
                //    hpEdit.Enabled = false;
                //    hpDelete.Enabled = false;
                //    cbxSelect.Enabled = false;
                //}
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                {
                    hpEdit.Enabled = false;
                    hpEdit.OnClientClick = string.Empty;
                }
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                {
                    hpEdit.Enabled = false;
                    hpEdit.OnClientClick = string.Empty;
                }
                hpEdit.Text = Resources.labels.edit;
                hpDelete.Text = Resources.labels.delete;
                if (drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE || drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.PENDINGFORDELETE || drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.PENDING || drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.PENDINGFORAPPROVE || drv["Status"].ToString().Trim() == SmartPortal.Constant.IPC.BLOCK)
                {
                    hpEdit.Enabled = false;
                    hpDelete.Enabled = false;
                    hpEdit.OnClientClick = string.Empty;
                    hpDelete.OnClientClick = string.Empty;
                }
                if (cbxSelect.Enabled)
                {
                    size++;
                }
                hdCounter.Value = "0";
                hdPageSize.Value = size.ToString();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_Widget", "gvcontractList_RowDataBound", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Widget", "gvcontractList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }

    protected void gvcontractList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvcontractList.PageIndex = e.NewPageIndex;
        BindData();

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
            DataSet ds = new DataSet();
            DataTable dataTable;

            ds = new SmartPortal.SEMS.Contract().GetContractByCondition(Utility.KillSqlInjection(txtContractCode.Text.Trim()), Utility.KillSqlInjection(txtCustName.Text.Trim()), Utility.KillSqlInjection(txtopenPer.Text.Trim()), Utility.KillSqlInjection(txtOpenDate.Text.Trim()), Utility.KillSqlInjection(txtEndDate.Text.Trim()), "0603", ddlStatus.SelectedValue, txtlicenseid.Text.Trim(), txtcustcode.Text.Trim(), txtPhone.Text.Trim(), ddlproducttype.SelectedValue, gvcontractList.PageSize, gvcontractList.PageIndex * gvcontractList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC); ;

            if (IPCERRORCODE == "0")
            {
                dataTable = ds.Tables[0];
            }
            else
            {
                dataTable = null;
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = sortExpression + direction;

                gvcontractList.DataSource = dataView;
                gvcontractList.DataBind();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Widget", "SortGridView", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=165"));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        LinkButton hpContractNo;

        string Str_ContractNo = "";
        try
        {
            foreach (GridViewRow gvr in gvcontractList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpContractNo = (LinkButton)gvr.Cells[1].FindControl("hpcontractCode");
                    Str_ContractNo += hpContractNo.Text.Trim() + "#";
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
            Session["_ContractNo"] = Str_ContractNo.Substring(0, Str_ContractNo.Length - 1);

            if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
            {
                RedirectToActionPage(IPC.ACTIONPAGE.DELETE, "&ACTION=" + SmartPortal.Constant.IPC.DELETE);
            }

        }
        else
        {
            lblError.Text = Resources.labels.youmustchoosecontracttodelete;
            BindData();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void btnBlock_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        LinkButton hpContractNo;

        string Str_ContractNo = "";
        try
        {
            foreach (GridViewRow gvr in gvcontractList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpContractNo = (LinkButton)gvr.Cells[1].FindControl("hpcontractCode");
                    Str_ContractNo += hpContractNo.Text.Trim() + "#";
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
            Session["_ContractNo"] = Str_ContractNo.Substring(0, Str_ContractNo.Length - 1);
            //Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=166" + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery) + "&action=" + SmartPortal.Constant.IPC.BLOCK));
            if (CheckPermitPageAction("BLOCK"))
            {
                RedirectToActionPage("BLOCK", "&ACTION=" + SmartPortal.Constant.IPC.BLOCK);
            }
        }
        else
        {
            lblError.Text = Resources.labels.youmustchoosecontracttoblock;
            BindData();
        }
    }
    protected void gvContractList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                case IPC.ACTIONPAGE.DELETE:
                    RedirectToActionPage(IPC.ACTIONPAGE.DELETE, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg + "&ACTION=" + SmartPortal.Constant.IPC.DELETE);
                    break;
            }
        }
    }
}
