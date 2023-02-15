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

using System.Collections.Generic;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Constant;

public partial class Widgets_SEMSContractLimitApprove_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            litError.Text = "";
            if (!IsPostBack)
            {
                btnAddNew.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE);
                btnDelete.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.REJECT);
                GridViewPaging.Visible = false;
                LoadDll();
            }
            GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvUserList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvUserList.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    void LoadDll()
    {
        try
        {
            #region hien thị status
            ddlstatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
            ddlstatus.Items.Add(new ListItem(Resources.labels.conpending, SmartPortal.Constant.IPC.PENDING));
            ddlstatus.Items.Add(new ListItem(Resources.labels.pendingfordelete, SmartPortal.Constant.IPC.PENDINGFORDELETE));
            ddlstatus.Items.Add(new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
            #endregion
            //load các giao dịch
            ddltran.DataSource = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            ddltran.DataTextField = "PAGENAME";
            ddltran.DataValueField = "TRANCODE";
            ddltran.DataBind();
            ddltran.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
            #region add limit type
            ddlLimitType.Items.Add(new ListItem(Resources.labels.binhthuong, "NOR"));
            #endregion
            ddlccyid.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlccyid.DataTextField = "CCYID";
            ddlccyid.DataValueField = "CCYID";
            ddlccyid.DataBind();
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
            DataSet dtCL = new DataSet();
            dtCL = new SmartPortal.SEMS.Transactions().GetContractLimitByCondition(Utility.KillSqlInjection(txtContractno.Text.Trim()), Utility.KillSqlInjection(ddltran.SelectedValue), Utility.KillSqlInjection(ddlccyid.SelectedValue), txtlimit.Text.Trim(), ddlstatus.SelectedValue.ToString(), Session["branch"].ToString(), "", ddlLimitType.SelectedValue, gvUserList.PageSize, gvUserList.PageIndex * gvUserList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvUserList.DataSource = dtCL;
                gvUserList.DataBind();
            }
            if (dtCL.Tables[0].Rows.Count > 0)
            {
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dtCL.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvContractLimit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label /*lbltotallimitday,*/ lblcountlimit, lblcreateuser, lblmodifiuser, lblTrans, lblfullname, lblTranCode, lblCCYID, lbllimit, lblstatus, lblstatusid, lblLimitType;
            LinkButton lblcontractno;
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

                lblcontractno = (LinkButton)e.Row.FindControl("lblcontractno");
                lblfullname = (Label)e.Row.FindControl("lblfullname");
                //lbltotallimitday = (Label)e.Row.FindControl("lbltotallimitday");
                lblTrans = (Label)e.Row.FindControl("lblTrans");
                lblTranCode = (Label)e.Row.FindControl("lblTranCode");
                lblCCYID = (Label)e.Row.FindControl("lblccyid");
                lbllimit = (Label)e.Row.FindControl("lbllimit");
                lblstatus = (Label)e.Row.FindControl("lblstatus");
                lblstatusid = (Label)e.Row.FindControl("lblstatusid");
                lblcountlimit = (Label)e.Row.FindControl("lblcountlimit");
                lblcreateuser = (Label)e.Row.FindControl("lblcreateuser");
                lblmodifiuser = (Label)e.Row.FindControl("lblmodifiuser");
                lblLimitType = (Label)e.Row.FindControl("lblLimitType");

                lblcontractno.Text = drv["ContractNo"].ToString();
                lblcontractno.Attributes.Add("class", "cslinkbutton");
                lblfullname.Text = drv["FullName"].ToString();
                //lbltotallimitday.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["totallimit"].ToString(), drv["CCYID"].ToString().Trim());
                lblstatus.Text = drv["status"].ToString();
                lblTrans.Text = drv["PageName"].ToString();
                lblTranCode.Text = drv["TranCode"].ToString();
                lblCCYID.Text = drv["CCYID"].ToString();
                lbllimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TRANLIMITOK"].ToString(), drv["CCYID"].ToString().Trim());
                lblcountlimit.Text = drv["countlimit"].ToString();
                lblcreateuser.Text = drv["usercreated"].ToString();
                lblmodifiuser.Text = drv["usermodified"].ToString();
                lblstatusid.Text = drv["STATUS"].ToString();
              

                switch (drv["LIMITTYPE"].ToString().Trim())
                {
                    case "DEB":
                        lblLimitType.Text = ddlLimitType.Items.FindByValue("DEB").Text;
                        break;
                    default:
                        lblLimitType.Text = ddlLimitType.Items.FindByValue("NOR").Text;
                        break;
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
                        cbxSelect.Enabled = false;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblstatus.Text = Resources.labels.conactive;
                        lblstatus.Attributes.Add("class", "label-success");
                        cbxSelect.Enabled = false;
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblstatus.Text = Resources.labels.conpending;
                        lblstatus.Attributes.Add("class", "label-warning");
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblstatus.Text = Resources.labels.conreject;
                        lblstatus.Attributes.Add("class", "label-warning");
                        cbxSelect.Enabled = false;
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        lblstatus.Text = Resources.labels.pendingfordelete;
                        lblstatus.Attributes.Add("class", "label-warning");
                        lblstatusid.Text = SmartPortal.Constant.IPC.PENDINGFORDELETE;
                        break;
                }
            }
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

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE))
        {
            try
            {
                CheckBox cbxSelect;
                List<string> lstTran = new List<string>();
                string strlblContractNo = "";
                if (CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE))
                {
                    LinkButton lblContractNo;
                    foreach (GridViewRow gvr in gvUserList.Rows)
                    {
                        cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                        if (cbxSelect.Checked == true)
                        {
                            lblContractNo = (LinkButton)gvr.Cells[1].FindControl("lblcontractno");
                            strlblContractNo += lblContractNo.CommandArgument.Trim() + "#";
                        }
                    }
                    if (!string.IsNullOrEmpty(strlblContractNo))
                    {
                        lblError.Text = "";
                        string[] contract = strlblContractNo.Trim().Split('#');

                        for (int i = 0; i < contract.Length - 1; i++)
                        {
                            string[] parm = contract[i].Split('|');
                            if (parm[3].ToString() == SmartPortal.Constant.IPC.PENDING || parm[3].ToString() == SmartPortal.Constant.IPC.NEW)
                            {
                                #region Ghi log
                                try
                                {
                                    SmartPortal.Common.Log.WriteLog("SEMS00024", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTLIMIT", "CONTRACTNO='" + parm[0] + "' AND TRANCODE='" + parm[1] + "' AND CCYID='" + parm[2] + "'");
                                }
                                catch
                                {
                                }
                                #endregion
                                new SmartPortal.SEMS.Transactions().UpdateContractLimit(parm[0], parm[1], parm[2], "", "", "", parm[4], Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.ACTIVE, parm[3], ref IPCERRORCODE, ref IPCERRORDESC);
                                if (IPCERRORCODE.Equals("0"))
                                {
                                    BindData2();
                                    lblError.Text = Resources.labels.duyethanmuchopdongthanhcong;
                                }
                                else
                                {
                                    lblError.Text = IPCERRORDESC;
                                    return;
                                }
                            }
                            if (parm[3].ToString() == SmartPortal.Constant.IPC.PENDINGFORDELETE)
                            {
                                #region Ghi log
                                try
                                {
                                    SmartPortal.Common.Log.WriteLog("SEMS00024", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTLIMIT", "CONTRACTNO='" + parm[0] + "' AND TRANCODE='" + parm[1] + "' AND CCYID='" + parm[2] + "'");
                                }
                                catch
                                {
                                }
                                #endregion
                                new SmartPortal.SEMS.Transactions().UpdateContractLimit(parm[0], parm[1], parm[2], "", "", "", parm[4], Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.DELETE, parm[3], ref IPCERRORCODE, ref IPCERRORDESC);
                                    lblError.Text = Resources.labels.duyethanmuchopdongthanhcong;

                            }
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.vuilongchonhanmuchopdong;
                        BindData2();
                    }
                }
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
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.REJECT))
        {
            try
            {
                CheckBox cbxSelect;
                LinkButton lblContractNo;
                string strlblContractNo = "";
                List<string> lstTran = new List<string>();
                if (CheckPermitPageAction(IPC.ACTIONPAGE.REJECT))
                {
                    foreach (GridViewRow gvr in gvUserList.Rows)
                    {
                        cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                        if (cbxSelect.Checked == true)
                        {
                            lblContractNo = (LinkButton)gvr.Cells[1].FindControl("lblContractNo");
                            strlblContractNo += lblContractNo.CommandArgument.Trim() + "#";
                        }
                    }

                    if (!string.IsNullOrEmpty(strlblContractNo))
                    {
                        lblError.Text = "";
                        string[] contract = strlblContractNo.Trim().Split('#');
                        for (int i = 0; i < contract.Length - 1; i++)
                        {
                            string[] parm = contract[i].Split('|');
                            if (parm[3].ToString() == SmartPortal.Constant.IPC.PENDING ||
                                parm[3].ToString() == SmartPortal.Constant.IPC.NEW)
                            {
                                #region Ghi log
                                try
                                {
                                    SmartPortal.Common.Log.WriteLog("SEMS00024", DateTime.Now.ToString(),
                                        Session["userName"].ToString(), "EBA_SPCUSTLIMIT",
                                        "CONTRACTNO='" + parm[0] + "' AND TRANCODE='" + parm[1] + "' AND CCYID='" +
                                        parm[2] + "'");
                                }
                                catch
                                {
                                }
                                #endregion

                                new SmartPortal.SEMS.Transactions().UpdateContractLimit(parm[0], parm[1], parm[2], "",
                                    "", "", parm[4], Session["userName"].ToString(),
                                    DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.REJECT,
                                    parm[3], ref IPCERRORCODE, ref IPCERRORDESC);

                                if (IPCERRORCODE.Equals("0"))
                                {
                                    BindData2();
                                    lblError.Text = Resources.labels.khongduyethanmuchopdongthanhcong;
                                }
                                else
                                {
                                    lblError.Text = IPCERRORDESC;
                                    return;
                                }
                            }

                            if (parm[3].ToString() == SmartPortal.Constant.IPC.PENDINGFORDELETE)
                            {
                                #region Ghi log
                                try
                                {
                                    SmartPortal.Common.Log.WriteLog("SEMS00024", DateTime.Now.ToString(),
                                        Session["userName"].ToString(), "EBA_SPCUSTLIMIT",
                                        "CONTRACTNO='" + parm[0] + "' AND TRANCODE='" + parm[1] + "' AND CCYID='" +
                                        parm[2] + "'");
                                }
                                catch
                                {
                                }
                                #endregion
                                new SmartPortal.SEMS.Transactions().UpdateContractLimit(parm[0], parm[1], parm[2], "",
                                    "", "", parm[4], Session["userName"].ToString(),
                                    DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.ACTIVE,
                                    parm[3], ref IPCERRORCODE, ref IPCERRORDESC);
                            }
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.vuilongchonhanmuchopdong;
                        BindData2();
                    }
                }
            }
            catch (IPCException IPCex)
            {
                SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                    this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(),
                    Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                    Request.Url.Query);

            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData2();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlLimitType_IndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);

            switch (ddlLimitType.SelectedValue)
            {
                case "DEB":
                    #region DEB
                    ddltran.DataSource = ds.Tables[0].Select("IsReceive='Y'").CopyToDataTable();
                    ddltran.DataTextField = "PAGENAME";
                    ddltran.DataValueField = "TRANCODE";
                    ddltran.DataBind();
                    ddltran.Enabled = true;
                    ddltran.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));

                    txtlimit.Enabled = true;
                    ddlccyid.Enabled = true;
                    #endregion
                    break;
                //case "BAT":
                //    #region BAT

                //    ddltran.DataSource = ds.Tables[0].Select("TranCode='IB000499'").CopyToDataTable();
                //    ddltran.DataTextField = "PAGENAME";
                //    ddltran.DataValueField = "TRANCODE";
                //    ddltran.DataBind();
                //    ddltran.Enabled = false;

                //    txtlimit.Enabled = true;
                //    ddlccyid.Enabled = false;
                //    #endregion
                //    break;
                default:
                    ddltran.DataSource = ds.Tables[0];
                    ddltran.DataTextField = "PAGENAME";
                    ddltran.DataValueField = "TRANCODE";
                    ddltran.DataBind();
                    ddltran.Enabled = true;
                    ddltran.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));

                    txtlimit.Enabled = true;
                    ddlccyid.Enabled = true;
                    break;
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
            gvUserList.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
            string SelectedPageNo = ((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text;
            gvUserList.PageIndex = !string.IsNullOrEmpty(SelectedPageNo) ? Convert.ToInt32(SelectedPageNo) - 1 : 0;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
