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
using System.Windows.Forms;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using System.Text;
using SmartPortal.ExceptionCollection;
using System.Collections.Generic;
//using System.Text.RegularExpressions;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using Label = System.Web.UI.WebControls.Label;
using SmartPortal.Constant;

public partial class Widgets_SEMSContractApprove_Widget : WidgetBase
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
            litError.Text = "";

            if (!IsPostBack)
            {


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
                    //ddlUserType.Items.Insert(0, new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                #endregion

                #region hien thị status
                ddlStatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
                ddlStatus.Items.Add(new ListItem(Resources.labels.conpending, SmartPortal.Constant.IPC.PENDING));
                ddlStatus.Items.Add(new ListItem(Resources.labels.pendingfordelete, SmartPortal.Constant.IPC.PENDINGFORDELETE));
                ddlStatus.Items.Add(new ListItem(Resources.labels.pendingforapprove, SmartPortal.Constant.IPC.PENDINGFORAPPROVE));
                ddlStatus.Items.Add(new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                #endregion

                GridViewPaging.Visible = false;
                btnApprove.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE);
                btnReject.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.REJECT);
                divResult.Visible = false;
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
        gvcontractList.PageIndex = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
       BindData();
    }
    protected void gvcontractList_RowCommand(object sender, GridViewCommandEventArgs e)
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
    void BindData()
    {
        try
        {
            //5.10/2016 minh add to reset session
            Session["contractNo"] = null;
            DataSet dtContr = new DataSet();
            if (Convert.ToInt32(((HiddenField)GridViewPaging.FindControl("TotalRows")).Value) < gvcontractList.PageIndex * gvcontractList.PageSize) return;
            dtContr = new SmartPortal.SEMS.Contract().LoadContractForApprove(Utility.KillSqlInjection(txtcontractno.Text.Trim()), Utility.KillSqlInjection(txtcustname.Text.Trim()), Utility.KillSqlInjection(txtcreateuser.Text.Trim()), Utility.KillSqlInjection(txtOpenDate.Text.Trim()), Utility.KillSqlInjection(txtEndDate.Text.Trim()), "0603", ddlStatus.SelectedValue, Session["branch"].ToString().Trim(), gvcontractList.PageSize, gvcontractList.PageIndex * gvcontractList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                divResult.Visible = true;
                //if (dtContr.Tables[0].Rows.Count == 0 || ddlStatus.SelectedValue == SmartPortal.Constant.IPC.DELETE)
                //{
                //    pnbutton.Visible = false;

                //}
                //else { pnbutton.Visible = true; }
                gvcontractList.DataSource = dtContr;
                gvcontractList.DataBind();

            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            if (dtContr.Tables[0].Rows.Count > 0)
            {
                litError.Text = string.Empty;
                GridViewPaging.Visible = true;
                ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dtContr.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
            }
            else
            {
                litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                GridViewPaging.Visible = false;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "BindData", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
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
            Label lblSubUserCode;
            Label lblStatus;

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
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                hpcontractCode = (LinkButton)e.Row.FindControl("hpcontractCode");
                lblcustName = (Label)e.Row.FindControl("lblcustName");
                lblOpen = (Label)e.Row.FindControl("lblOpen");
                lblOpendate = (Label)e.Row.FindControl("lblOpendate");
                lblClosedate = (Label)e.Row.FindControl("lblClosedate");
                lblSubUserCode = (Label)e.Row.FindControl("lblSubUserCode");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                hpcontractCode.Text = drv["ContractNo"].ToString();
                lblcustName.Text = drv["FULLNAME"].ToString();
                lblOpen.Text = drv["UserCreate"].ToString();
                lblOpendate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["CreateDate"].ToString()).ToString("dd/MM/yyyy");
                lblClosedate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["EndDate"].ToString()).ToString("dd/MM/yyyy");

                //hpReview.Target = "_Blank";
                //string contracttype = string.Empty;
                //switch (drv["USERTYPE"].ToString().Trim())
                //{
                //    case "0101":
                //    case "0102":
                //    case "0103":
                //        contracttype = SmartPortal.Constant.IPC.AGENT; break;
                //    case "0201":
                //    case "0202":
                //    case "0203":
                //    case "0204":
                //        contracttype = SmartPortal.Constant.IPC.CONSUMER; break;
                //    case "0301":
                //        contracttype = SmartPortal.Constant.IPC.MERCHANT; break;
                //}


                //DataSet dsUserType = new DataSet();
                //dsUserType = new SmartPortal.SEMS.Services().GetAllUserType(contracttype, "", ref IPCERRORCODE, ref IPCERRORDESC);
                //if (IPCERRORCODE == "0")
                //{
                //    DataTable dtUserType = new DataTable();
                //    dtUserType = dsUserType.Tables[0];

                //    if (dtUserType.Rows.Count != 0)
                //    {
                //        lblcontractType.Text = dtUserType.Rows[0]["TYPENAME"].ToString();
                //    }
                //}
                //else
                //{
                //    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                //}
                DataSet dsUserType = new DataSet();
                dsUserType = new SmartPortal.SEMS.Services().GetUserType(drv["USERTYPE"].ToString().Trim(), "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    if (dtUserType.Rows.Count != 0)
                    {
                        lblSubUserCode.Text = dtUserType.Rows[0]["SUBUSERTYPE"].ToString();
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "gvcontractList_RowDataBound", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "gvcontractList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvcontractList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvcontractList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
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
            DataSet DS = new DataSet();
            DataTable dataTable;

            DS = new SmartPortal.SEMS.Contract().LoadContractForApprove(Utility.KillSqlInjection(txtcontractno.Text.Trim()), Utility.KillSqlInjection(txtcustname.Text.Trim()), Utility.KillSqlInjection(txtcreateuser.Text.Trim()), Utility.KillSqlInjection(txtOpenDate.Text.Trim()), Utility.KillSqlInjection(txtEndDate.Text.Trim()), "0603", ddlStatus.SelectedValue, Session["branch"].ToString().Trim() ,gvcontractList.PageSize, gvcontractList.PageIndex * gvcontractList.PageSize, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                dataTable = DS.Tables[0];
            }
            else
            {
                goto ERROR;
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = sortExpression + direction;

                gvcontractList.DataSource = dataView;
                gvcontractList.DataBind();
            }

            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "SortGridView", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SortGridView", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:

        ;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    //public void Approvereject(string stt)
    //{
    //    CheckBox cbxApprove;
    //    HyperLink hpcontractNo;
    //    DataSet ApproveContractTable = new DataSet();
    //    try
    //    {
    //        foreach (GridViewRow gvr in gvcontractList.Rows)
    //        {
    //            cbxApprove = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
    //            if (cbxApprove.Checked == true)
    //            {
    //                hpcontractNo = (HyperLink)gvr.Cells[1].FindControl("hpcontractCode");
    //                ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(hpcontractNo.Text.Trim(),stt,Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
    //                if (IPCERRORCODE == "0")
    //                {
    //                    if (stt == SmartPortal.Constant.IPC.ACTIVE)
    //                    {
    //                        SendInfoLogin(hpcontractNo);
    //                    }
    //                }
    //                else
    //                {
    //                    //lblError.Text = IPCERRORDESC;
    //                }
    //            }
    //        }
    //       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=4&p=148"));
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    void SendInfoLogin(HyperLink hpcontractNo)
    {

        Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
        tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSContractApprove", "ContractApprove" + System.Globalization.CultureInfo.CurrentCulture.ToString());

        //lay thong tin hop dong de gui mail

        DataSet ApproveContractTable = new DataSet();
        try
        {

            string custID = "";
            tmpl.Reset();

            //lay thong tin hop dong boi contractno
            DataTable contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(hpcontractNo.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }
            //gan thong tin hop dong vao stringtemplate
            if (contractTable.Rows.Count != 0)
            {
                tmpl.SetAttribute("CONTRACTNO", contractTable.Rows[0]["CONTRACTNO"].ToString());
                string contracttype = string.Empty;
                switch (contractTable.Rows[0]["USERTYPE"].ToString().Trim())
                {
                    case "0101":
                    case "0102":
                    case "0103":
                        contracttype = SmartPortal.Constant.IPC.AGENT; break;
                    case "0201":
                    case "0202":
                    case "0203":
                    case "0204":
                        contracttype = SmartPortal.Constant.IPC.CONSUMER; break;
                    case "0301":
                        contracttype = SmartPortal.Constant.IPC.MERCHANT; break;
                }

                DataSet dsUserType = new DataSet();
                dsUserType = new SmartPortal.SEMS.Services().GetAllUserType(contracttype, "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    if (dtUserType.Rows.Count != 0)
                    {
                        tmpl.SetAttribute("CONTRACTTYPE", dtUserType.Rows[0]["TYPENAME"].ToString());
                    }
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }


                tmpl.SetAttribute("OPENDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["CREATEDATE"].ToString()).ToString("dd/MM/yyyy"));
                tmpl.SetAttribute("ENDDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["ENDDATE"].ToString()).ToString("dd/MM/yyyy"));


                tmpl.SetAttribute("PRODUCT", contractTable.Rows[0]["PRODUCTNAME"].ToString());


                custID = contractTable.Rows[0]["CUSTID"].ToString();
            }

            //lay thong tin khach hang
            DataTable custTable = (new SmartPortal.SEMS.Customer().GetCustomerByCustcode(custID, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }
            //gan thong tin hop dong vao stringtemplate
            if (custTable.Rows.Count != 0)
            {
                tmpl.SetAttribute("CUSTID", custTable.Rows[0]["CUSTID"].ToString());
                tmpl.SetAttribute("FULLNAME", custTable.Rows[0]["FULLNAME"].ToString());
                switch (custTable.Rows[0]["CFTYPE"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.PERSONAL:
                        tmpl.SetAttribute("CUSTTYPE", Resources.labels.canhan);
                        break;
                    case SmartPortal.Constant.IPC.CORPORATE:
                        tmpl.SetAttribute("CUSTTYPE", Resources.labels.doanhnghiep);
                        break;
                }
                tmpl.SetAttribute("PHONE", custTable.Rows[0]["TEL"].ToString());
                tmpl.SetAttribute("EMAIL", custTable.Rows[0]["EMAIL"].ToString());
            }

            //lay thong tin tai khoan
            DataTable userTableTemp = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo.Text.Trim(), "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }
            DataTable userTable;
            try
            {
                userTable = userTableTemp.Select("STATUS1 NOT IN ('D','R')").CopyToDataTable();
            }
            catch
            {
                userTable = userTableTemp;
            }


            //gan thong tin user vao stringtemplate
            foreach (DataRow row in userTable.Rows)
            {

                StringBuilder st = new StringBuilder();

                st.Append("<table style='width:100%;'>");

                st.Append("<tr>");
                st.Append("<td>");
                st.Append(Resources.labels.tendaydu + " :");
                st.Append("</td>");
                st.Append("<td>");
                st.Append(row["FULLNAME"].ToString());
                st.Append("</td>");
                st.Append("<td>");
                st.Append("Email :");
                st.Append("</td>");
                st.Append("<td>");
                st.Append(row["EMAIL"].ToString());
                st.Append("</td>");
                st.Append("</tr>");

                st.Append("<tr>");
                st.Append("<td>");
                st.Append(Resources.labels.phone + " :");
                st.Append("</td>");
                st.Append("<td>");
                st.Append(row["PHONE"].ToString());
                st.Append("</td>");
                st.Append("<td>");
                st.Append("");
                st.Append("</td>");
                st.Append("<td>");
                st.Append("");
                st.Append("</td>");
                st.Append("</tr>");

                //lay het các tai khoan Ibank cua user theo userID
                DataSet accountIBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountIBTable = accountIBDataset.Tables[0];
                if (accountIBTable.Rows.Count != 0)
                {

                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<B>INTERNET BANKING</B><hr/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td>");
                    st.Append(Resources.labels.username + " :");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append("<b>" + accountIBTable.Rows[0]["USERNAME"].ToString() + "</b>");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append(Resources.labels.password + " :");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString()) + "</b>");
                    st.Append("</td>");
                    st.Append("</tr>");
                }

                //lay het các tai khoan SMS cua user theo userID
                DataSet accountSMSDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountSMSTable = accountSMSDataset.Tables[0];
                if (accountSMSTable.Rows.Count != 0)
                {
                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<B>SMS</B><hr/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td>");
                    st.Append(Resources.labels.phone + " :");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append("<b>" + accountSMSTable.Rows[0]["UN"].ToString() + "</b>");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append(Resources.labels.taikhoanmacdinh + " :");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append("<b>" + accountSMSTable.Rows[0]["DEFAULTACCTNO"].ToString() + "</b>");
                    st.Append("</td>");
                    st.Append("</tr>");
                }

                //lay het các tai khoan mb cua user theo userid
                // dataset accountmbdataset = new smartportal.sems.user().getuserrolebyserviceid(smartportal.constant.ipc.mb, row["userid"].tostring().trim(), string.empty, ref ipcerrorcode, ref ipcerrordesc);

                // if (ipcerrorcode != "0")
                // {
                    // goto error;
                // }

                // datatable accountmbtable = accountmbdataset.tables[0];
                // if (accountmbtable.rows.count != 0)
                // {
                    // st.append("<tr>");
                    // st.append("<td colspan='4'>");
                    // st.append("<b>mobile</b><hr/>");
                    // st.append("</td>");
                    // st.append("</tr>");

                    // st.append("<tr>");
                    // st.append("<td>");
                    // st.append(resources.labels.phone + " :");
                    // st.append("</td>");
                    // st.append("<td>");
                    // st.append("<b>" + accountmbtable.rows[0]["un"].tostring() + "</b>");
                    // st.append("</td>");
                    // st.append("<td>");
                    // st.append(resources.labels.password + " :");
                    // st.append("</td>");
                    // st.append("<td>");
                    // st.append("<b>" + smartportal.security.encryption.decrypt(accountmbtable.rows[0]["pass"].tostring()) + "</b>");
                    // st.append("</td>");
                    // st.append("</tr>");
                // }

                //lay het các tai khoan PHO cua user theo userID
                DataSet accountPHODataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.PHO, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountPHOTable = accountPHODataset.Tables[0];
                if (accountPHOTable.Rows.Count != 0)
                {
                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<B>PHONE</B><hr/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td>");
                    st.Append(Resources.labels.phone + " :");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append("<b>" + accountPHOTable.Rows[0]["UN"].ToString() + "</b>");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append(Resources.labels.password + " :");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountPHOTable.Rows[0]["PASS"].ToString()) + "</b>");
                    st.Append("</td>");
                    st.Append("</tr>");
                }

                st.Append("</table>");

                tmpl.SetAttribute("USERINFO", st.ToString());
                Session["test1"] = st.ToString();
                //send mail

                SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], row["EMAIL"].ToString(), System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());

                tmpl.RemoveAttribute("USERINFO");

            }
            goto EXIT;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:

        ;
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {

        CheckBox cbxSelect;
        LinkButton hpcontractNo;
        List<string> lstTran = new List<string>();
        try
        {
            foreach (GridViewRow gvr in gvcontractList.Rows)
            {
                cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxSelect.Checked == true)
                {
                    hpcontractNo = (LinkButton)gvr.Cells[1].FindControl("hpcontractCode");


                    lstTran.Add(hpcontractNo.Text.Trim());


                }
            }

        }
        catch (Exception ex)
        {

        }
        if (lstTran.Count != 0)
        {
            Session["contractNo"] = lstTran;
            Session["ActionApproveOrReject"] = "Approve";
            if(CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE))
            RedirectToActionPage(IPC.ACTIONPAGE.APPROVE, "&returnURL" + "=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query));
        }
        else
        {
            lblError.Text = Resources.labels.youmustchoosecontracttoapprove;
            BindData();
        }

    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        //từ chối hợp đồng -Quyềnnnpv
        //Approvereject(SmartPortal.Constant.IPC.BLOCK);

        //Duyệt giao dịch TUANTA

        CheckBox cbxSelect;
        LinkButton hpcontractNo;
        List<string> lstTran = new List<string>();
        try
        {
            foreach (GridViewRow gvr in gvcontractList.Rows)
            {
                cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxSelect.Checked == true)
                {
                    hpcontractNo = (LinkButton)gvr.Cells[1].FindControl("hpcontractCode");

                    //approve
                    //new SmartPortal.SEMS.Transactions().TellerApp(hpTranID.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);

                    lstTran.Add(hpcontractNo.Text.Trim());
                }
            }

        }
        catch (Exception ex)
        {

        }
        if (lstTran.Count != 0)
        {
            Session["contractNo"] = lstTran;
            Session["ActionApproveOrReject"] = "Reject";
            if (CheckPermitPageAction(IPC.ACTIONPAGE.REJECT))
                RedirectToActionPage(IPC.ACTIONPAGE.REJECT, "&returnURL" + "=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query));
        }
        else
        {
            lblError.Text = Resources.labels.youmustchoosecontracttoreject;
            BindData();
        }

    }

}
