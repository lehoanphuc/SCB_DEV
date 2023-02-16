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
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSContractFee_Widget : WidgetBase
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
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.DELETE);
                btnAdd_New.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                LoadDll();
                GridViewPaging.Visible = false;
                divResult.Visible = false;
                //load các giao dịch
                ddlFeeType.DataSource = new SmartPortal.SEMS.Fee().LoadFeeType(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlFeeType.DataTextField = "TYPENAME";
                ddlFeeType.DataValueField = "FEETYPE";
                ddlFeeType.DataBind();
                ddlFeeType.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
                ddlFeeType.Items.Remove(ddlFeeType.Items.FindByValue("INTERBANK"));
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
        gvContractFee.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvContractFee.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void LoadDll()
    {
        try
        {
            ddltran.DataSource = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISPRODUCTFEE), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            ddltran.DataTextField = "PAGENAME";
            ddltran.DataValueField = "TRANCODE";
            ddltran.DataBind();
            ddltran.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));

            //load tien te vutran15122014
            ddlccyid.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlccyid.DataTextField = "CCYID";
            ddlccyid.DataValueField = "CCYID";
            ddlccyid.DataBind();

            //load  fee
            ddlFee.DataSource = new SmartPortal.SEMS.Fee().SearchFee(string.Empty, string.Empty, string.Empty, Utility.KillSqlInjection(ddlccyid.SelectedValue), 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            ddlFee.DataTextField = "FEENAME";
            ddlFee.DataValueField = "FEEID";
            ddlFee.DataBind();
            ddlFee.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
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
            divResult.Visible = true;
            ltrError.Text = string.Empty;
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvContractFee.PageIndex * gvContractFee.PageSize) return;
            DataSet dsContractFee = new DataSet();
            dsContractFee = new SmartPortal.SEMS.Fee().SearchContractFee(Utility.KillSqlInjection(txtContractno.Text.Trim()),  Utility.KillSqlInjection(ddlFee.SelectedValue), Utility.KillSqlInjection(ddlccyid.SelectedValue), Utility.KillSqlInjection(txtcustomername.Text.Trim()),"", "", Utility.KillSqlInjection(ddlFeeType.SelectedValue), gvContractFee.PageSize, gvContractFee.PageIndex * gvContractFee.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                gvContractFee.DataSource = dsContractFee;
                gvContractFee.DataBind();
            }
            if (dsContractFee.Tables[0].Rows.Count > 0)
            {
                ltrError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dsContractFee.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
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
    protected void gvContractFee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblTrans, lblFeeName, lblFeeType, lblstatus, lblccyid, lbfullname, lblusercreated, lbldatecreated, lbluserapproved, lbldateapproved;
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
                lbfullname = (Label)e.Row.FindControl("lblfullname");
                lblTrans = (Label)e.Row.FindControl("lblTrans");
                lblFeeName = (Label)e.Row.FindControl("lblFeeName");
                lblFeeType = (Label)e.Row.FindControl("lblFeeType");
                lblccyid = (Label)e.Row.FindControl("lblccyid");
                lblstatus = (Label)e.Row.FindControl("lblstatus");
                lblusercreated = (Label)e.Row.FindControl("lblusercreated");
                lbldatecreated = (Label)e.Row.FindControl("lbldatecreated");
                lbluserapproved = (Label)e.Row.FindControl("lbluserapproved");
                lbldateapproved = (Label)e.Row.FindControl("lbldateapproved");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbDelete = (LinkButton)e.Row.FindControl("lbDelete");


                lblProductName.Text = drv["CONTRACTNO"].ToString();
                lbfullname.Text = drv["FULLNAME"].ToString();
                lblTrans.Text = drv["PAGENAME"].ToString();
                lblFeeName.Text = drv["FEENAME"].ToString();
                lblFeeType.Text = drv["TYPENAME"].ToString(); 
                lblccyid.Text = drv["CCYID"].ToString();
                lblusercreated.Text = drv["USERCREATED"].ToString();
                lbldatecreated.Text = ((DateTime)drv["DATECREATED"]).ToString("dd/MM/yyyy");
                lbluserapproved.Text = drv["USERAPPROVED"].ToString();
                if (drv["DATEAPPROVED"].ToString().Trim() != "")
                {
                    lbldateapproved.Text = ((DateTime)drv["DATEAPPROVED"]).ToString("dd/MM/yyyy");
                }

                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblstatus.Text = Resources.labels.connew;
                        lblstatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblstatus.Text = Resources.labels.condelete;
                        lblstatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblstatus.Text = Resources.labels.conactive;
                        lblstatus.Attributes.Add("class", "label-success");
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        lblstatus.Text = Resources.labels.pendingfordelete;
                        lblstatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblstatus.Text = Resources.labels.conpending;
                        lblstatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblstatus.Text = Resources.labels.conreject;
                        lblstatus.Attributes.Add("class", "label-warning");
                        break;
                }

                if (drv["branchid"].ToString().Trim() == Session["branch"].ToString().Trim())
                {
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
                    switch (drv["STATUS"].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.DELETE:
                        case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                            lbEdit.Enabled = false;
                            lbEdit.OnClientClick = string.Empty;
                            lbDelete.Enabled = false;
                            lbDelete.OnClientClick = string.Empty;
                            cbxSelect.Enabled = false;
                            break;
                            //case SmartPortal.Constant.IPC.REJECT:
                            //    lbEdit.Enabled = false;
                            //    lbEdit.OnClientClick = string.Empty;
                            //    lbDelete.Enabled = false;
                            //    lbDelete.OnClientClick = string.Empty;
                            //    break;
                    }
                }
                else
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
                    lbDelete.Enabled = false;
                    lbDelete.OnClientClick = string.Empty;
                    cbxSelect.Enabled = false;
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
    protected void gvContractFee_RowCommand(object sender, GridViewCommandEventArgs e)
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
    protected void gvContractFee_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string commandArg = string.Empty;
            commandArg = ((LinkButton)gvContractFee.Rows[e.RowIndex].Cells[1].FindControl("lblProductName")).CommandArgument;
            string[] pros = commandArg.Split('|');
            if (pros[4].ToString() == SmartPortal.Constant.IPC.PENDING || pros[4].ToString() == SmartPortal.Constant.IPC.NEW)
            {
                try
                {
                    SmartPortal.Common.Log.WriteLog("SEMS00021", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTFEE", "CONTRACTNO='" + pros[0] + "' AND TRANCODE='" + pros[1] + "' AND CCYID='" + pros[3] + "' AND FEEID='" + pros[2] + "'");
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
                }
                new SmartPortal.SEMS.Fee().DeleteContractFee(pros[0], pros[1], pros[2], pros[3], pros[4], ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE.Equals("0"))
                {
                    BindData2();
                    lblError.Text = Resources.labels.xoaphichohopdongthanhcong;
                }
                else
                {
                    lblError.Text = IPCERRORDESC;
                    return;
                }
            }
            else if (pros[4].ToString() == SmartPortal.Constant.IPC.ACTIVE)
            {
                try
                {
                    SmartPortal.Common.Log.WriteLog("SEMS00021", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTFEE", "CONTRACTNO='" + pros[0] + "' AND TRANCODE='" + pros[1] + "' AND CCYID='" + pros[3] + "' AND FEEID='" + pros[2] + "'");
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
                }
                new SmartPortal.SEMS.Fee().UpdateContractFee(pros[0], pros[1], pros[2], "", pros[3], "", "", "", "", Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.PENDINGFORDELETE, pros[4], ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE.Equals("0"))
                {
                    BindData2();
                    lblError.Text = Resources.labels.xoaphichohopdongthanhcong;
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            CheckBox cbxDelete;
            LinkButton lblProductName;
            string strProductCode = "";
            try
            {
                foreach (GridViewRow gvr in gvContractFee.Rows)
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
                        if (pros[4].ToString() == SmartPortal.Constant.IPC.PENDING || pros[4].ToString() == SmartPortal.Constant.IPC.NEW)
                        {
                            try
                            {
                                SmartPortal.Common.Log.WriteLog("SEMS00021", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTFEE", "CONTRACTNO='" + pros[0] + "' AND TRANCODE='" + pros[1] + "' AND CCYID='" + pros[3] + "' AND FEEID='" + pros[2] + "'");
                            }
                            catch (Exception ex)
                            {
                                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
                            }
                            new SmartPortal.SEMS.Fee().DeleteContractFee(pros[0], pros[1], pros[2], pros[3], pros[4], ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE.Equals("0"))
                            {
                                BindData2();
                                lblError.Text = Resources.labels.xoaphichohopdongthanhcong;
                            }
                            else
                            {
                                lblError.Text = IPCERRORDESC;
                                return;
                            }
                        }
                        else if (pros[4].ToString() == SmartPortal.Constant.IPC.ACTIVE)
                        {
                            try
                            {
                                SmartPortal.Common.Log.WriteLog("SEMS00021", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTFEE", "CONTRACTNO='" + pros[0] + "' AND TRANCODE='" + pros[1] + "' AND CCYID='" + pros[3] + "' AND FEEID='" + pros[2] + "'");
                            }
                            catch (Exception ex)
                            {
                                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
                                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
                            }
                            new SmartPortal.SEMS.Fee().UpdateContractFee(pros[0], pros[1], pros[2], "", pros[3], "", "", "", "", Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.PENDINGFORDELETE, pros[4], ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE.Equals("0"))
                            {
                                BindData2();
                                lblError.Text = Resources.labels.xoaphichohopdongthanhcong;
                            }
                            else
                            {
                                lblError.Text = IPCERRORDESC;
                                return;
                            }
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
    protected void ddlCCYID_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.Fee().SearchFee(string.Empty, string.Empty, string.Empty, Utility.KillSqlInjection(ddlccyid.SelectedValue), 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
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
            gvContractFee.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvContractFee.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}
