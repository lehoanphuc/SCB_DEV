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

using SmartPortal.Common.Utilities;
using SmartPortal.Security;
using System.Text;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSUserOTP_Widget : System.Web.UI.UserControl
{
    int i = 0;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            lblError.Text = "";
            litError.Text = "";
            lblAlert.Text = "";

            if (!IsPostBack)
            {
                if (ViewState["OTP"] == null)
                {

                    #region load loai hop dong
                    DataSet dsUserType = new DataSet();
                    dsUserType = new SmartPortal.SEMS.Services().GetAllUserType("", "Y", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable dtUserType = new DataTable();
                        dtUserType = dsUserType.Tables[0];

                        ddlusertype.DataSource = dtUserType;
                        ddlusertype.DataTextField = "TYPENAME";
                        ddlusertype.DataValueField = "USERTYPE";
                        ddlusertype.DataBind();

                        ddlusertype.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));
                    }
                    else
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    #endregion

                    #region hien thị status
                    ddlstatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
                    ddlstatus.Items.Add(new ListItem(Resources.labels.conactive, SmartPortal.Constant.IPC.ACTIVE));
                    ddlstatus.Items.Add(new ListItem(Resources.labels.conblock, SmartPortal.Constant.IPC.BLOCK));
                    ddlstatus.Items.Insert(0, new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                    #endregion

                    #region Load Level
                    //DataTable tblAllLevel = new SmartPortal.IB.CorpUser().LoadAllLevel();
                    //ddluserlevel.DataSource = tblAllLevel;
                    //ddluserlevel.DataTextField = "DESCRIPTION";
                    //ddluserlevel.DataValueField = "USERLEVEL";
                    //ddluserlevel.DataBind();

                    //ddluserlevel.Items.Insert(0, new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                    #endregion

                    #region hien thị status token
                    // ddlStatusTK.Items.Add(new ListItem(Resources.labels.conactive, SmartPortal.Constant.IPC.ACTIVE));
                    //ddlStatusTK.Items.Add(new ListItem(Resources.labels.condelete, SmartPortal.Constant.IPC.DELETE));

                    #endregion

                    #region Load Authen Type
                    DataTable tblAuthen = new SmartPortal.IB.Transactions().LoadAuthenType(Session["userID"].ToString());
                    ddlAuthenType.DataSource = tblAuthen;
                    ddlAuthenType.DataTextField = "TYPENAME";
                    ddlAuthenType.DataValueField = "AUTHENTYPE";
                    ddlAuthenType.DataBind();

                    #endregion

                    BindData();

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
    void BindData()
    {
        string fullname = Utility.KillSqlInjection(txtfullname.Text.Trim());
        string usertype = Utility.KillSqlInjection(ddlusertype.SelectedValue.ToString());
        string email = Utility.KillSqlInjection(txtemail.Text.Trim());
        string phone = Utility.KillSqlInjection(txtphone.Text.Trim());
        string userlevel = Utility.KillSqlInjection(ddluserlevel.SelectedValue.ToString());
        string status = Utility.KillSqlInjection(ddlstatus.SelectedValue.ToString());
        string contractno = Utility.KillSqlInjection(txtContractNo.Text.Trim());
        string userid = Utility.KillSqlInjection(txtUserName.Text.Trim());
        DataTable dtUser = new DataTable();

        dtUser = new SmartPortal.SEMS.OTP().SearchUser(fullname, usertype, email, phone, userlevel, status, contractno, userid, Session["branch"].ToString());

        gvUserList.DataSource = dtUser;
        gvUserList.DataBind();
        if (dtUser.Rows.Count == 0)
        {
            litError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
        }
        #region load thong tin khi xem hoac sua
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] != null)
        {
            DataSet dsOTPInfo = new SmartPortal.SEMS.OTP().ViewOTP(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

            DataTable tblOTP = new DataTable();

            DataColumn colUserID = new DataColumn("colUserID");
            DataColumn colFullnameCustomer = new DataColumn("colFullnameCustomer");
            DataColumn colUserFullName = new DataColumn("colUserFullName");
            DataColumn colAuthenType = new DataColumn("colAuthenType");
            DataColumn colAuthenTypeID = new DataColumn("colAuthenTypeID");
            DataColumn colAuthenCode = new DataColumn("colAuthenCode");
            DataColumn colIssueDate = new DataColumn("colIssueDate");
            DataColumn colActiveCode = new DataColumn("colActiveCode");
            DataColumn colActiveDate = new DataColumn("colActiveDate");
            DataColumn colStatusID = new DataColumn("colStatusID");
            DataColumn colStatus = new DataColumn("colStatus");

            tblOTP.Columns.Add(colFullnameCustomer);
            tblOTP.Columns.Add(colUserID);
            tblOTP.Columns.Add(colUserFullName);
            tblOTP.Columns.Add(colAuthenType);
            tblOTP.Columns.Add(colAuthenTypeID);
            tblOTP.Columns.Add(colAuthenCode);
            tblOTP.Columns.Add(colIssueDate);
            tblOTP.Columns.Add(colActiveCode);
            tblOTP.Columns.Add(colActiveDate);
            tblOTP.Columns.Add(colStatusID);
            tblOTP.Columns.Add(colStatus);


            //tblOTP.Columns.AddRange(new[] { colFullnameCustomer, colUserID, colUserFullName, colAuthenType, colAuthenTypeID, colAuthenCode, colIssueDate, colActiveCode, colActiveDate, colStatusID, colStatus });

            foreach (DataRow r in dsOTPInfo.Tables[0].Rows)
            {
                DataRow rowOTP = tblOTP.NewRow();
                rowOTP["colUserID"] = r["USERID"].ToString();
                rowOTP["colFullnameCustomer"] = r["FullNameCustomer"].ToString();
                rowOTP["colUserFullName"] = r["FULLNAME"].ToString();
                rowOTP["colAuthenType"] = r["TYPENAME"].ToString();
                rowOTP["colAuthenTypeID"] = r["AUTHENTYPE"].ToString();
                rowOTP["colAuthenCode"] = r["DEVICEID"].ToString();
                rowOTP["colStatusID"] = r["STATUS"].ToString();

                switch (r["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        rowOTP["colStatus"] = Resources.labels.connew;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        rowOTP["colStatus"] = Resources.labels.condelete;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        rowOTP["colStatus"] = Resources.labels.conactive;
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        rowOTP["colStatus"] = Resources.labels.conblock;
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        rowOTP["colStatus"] = Resources.labels.conpending;
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        rowOTP["colStatus"] = Resources.labels.conreject;
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        rowOTP["colStatus"] = Resources.labels.pendingfordelete;
                        break;
                }

                rowOTP["colIssueDate"] = r["ISSUEDATE"].ToString();
                rowOTP["colActiveCode"] = r["ACTIVECODE"].ToString();
                rowOTP["colActiveDate"] = r["ACTIVEDATE"].ToString();
                hidCustName.Value = r["FullNameCustomer"].ToString();

                tblOTP.Rows.Add(rowOTP);
            }

            ViewState["OTP"] = tblOTP;

            gvResultChuTaiKhoan.DataSource = tblOTP;
            gvResultChuTaiKhoan.DataBind();

            pnSearch.Visible = false;
            pnCustInfo.Visible = false;
            hidUserID.Value = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString();

            switch (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim())
            {
                case SmartPortal.Constant.IPC.VIEWDETAIL:
                    btnSave.Visible = false;
                    ddlAuthenType.Enabled = false;
                    //ddlStatusTK.Enabled = false;
                    txtserialNumber.Enabled = false;
                    btnThemChuTaiKhoan.Visible = false;
                    //gvResultChuTaiKhoan.Columns[4].Visible = false;
                    gvResultChuTaiKhoan.Columns[6].Visible = false;
                    break;
            }

        }
        #endregion
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        BindData();

    }
    protected void gvUserList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpfullnname;
            Label lbluserID;
            Label lblcustName;
            Label lblbirth;
            Label lblcontractno;
            Label lblemail;
            Label lblStatus;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (RadioButton)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                lbluserID = (Label)e.Row.FindControl("lbluserID");
                hpfullnname = (HyperLink)e.Row.FindControl("hpfullnname");
                lblcustName = (Label)e.Row.FindControl("lblcustName");
                lblbirth = (Label)e.Row.FindControl("lblbirth");
                //lblusertype = (Label)e.Row.FindControl("lblusertype");
                lblemail = (Label)e.Row.FindControl("lblemail");
                //lbllevel = (Label)e.Row.FindControl("lbllevel");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblcontractno = (Label)e.Row.FindControl("lblcontractno");

                cbxSelect.Attributes.Add("onclick", "SelectRAD(this,'" + drv["UserID"].ToString() + "')");

                if (i == 0)
                {
                    cbxSelect.Checked = true;
                    hidUserID.Value = drv["UserID"].ToString();
                    i += 1;
                }
                lblcontractno.Text = drv["CONTRACTNO"].ToString();
                hpfullnname.Text = drv["FULLNAME"].ToString();
                lbluserID.Text = drv["USERID"].ToString();
                //hpfullnname.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=191&uid=" + drv["USERID"].ToString());
                lblcustName.Text = drv["FULLNAMECUST"].ToString();
                lblbirth.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["BIRTHDAY"].ToString()).ToString("dd/MM/yyyy");
                //lblusertype.Text = drv["USERTYPE"].ToString();
                lblemail.Text = drv["EMAIL"].ToString();
                //lbllevel.Text = drv["USERLEVEL"].ToString();
                switch (drv["Status"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = Resources.labels.moi;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = Resources.labels.huy;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.sudung;
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblStatus.Text = Resources.labels.khoa;
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        lblStatus.Text = Resources.labels.pendingfordelete;
                        break;
                }
            }
        }

        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Add_Widget", "gvCustomerList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["OTP"] == null)
            {
                lblError.Text = Resources.labels.bancanthemdayduthongtinotp;
                return;
            }

            DataTable tblOTP = (DataTable)ViewState["OTP"];
            tblOTP.Columns.Remove("colFullnameCustomer");
            tblOTP.Columns.Remove("colUserFullName");
            tblOTP.Columns.Remove("colAuthenType");
            tblOTP.Columns.Remove("colStatus");

            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] == null)
            {
                //insert
                new SmartPortal.SEMS.OTP().MapOTP(tblOTP, ref IPCERRORCODE, ref IPCERRORDESC);
            }
            else
            {

                DataTable tblDeleteOTP = new DataTable();

                DataColumn colUserID = new DataColumn("colUserID");

                tblDeleteOTP.Columns.Add(colUserID);


                DataRow rDelete = tblDeleteOTP.NewRow();
                rDelete["colUserID"] = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString().Trim();
                tblDeleteOTP.Rows.Add(rDelete);

                //update
                new SmartPortal.SEMS.OTP().Update(tblOTP, tblDeleteOTP, ref IPCERRORCODE, ref IPCERRORDESC);
            }

            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.OTPAUTHENTYPEEXIST);
            }
            else
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] == null)
                {
                    lblError.Text = Resources.labels.themmoiotpthanhcong;
                }
                else
                {
                    lblError.Text = Resources.labels.capnhatotpthanhcong;
                }

                btnSave.Visible = false;
                pnChuTaiKhoan.Visible = false;
                pnCustInfo.Visible = false;
                pnSearch.Visible = false;
                pnToken.Visible = false;
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


    protected void btnExit_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=207"));
    }
    protected void btnThemChuTaiKhoan_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable tblOTP = new DataTable();

            if (ViewState["OTP"] == null)
            {
                DataColumn colFullnameCustomer = new DataColumn("colFullnameCustomer");
                DataColumn colUserID = new DataColumn("colUserID");
                DataColumn colUserFullName = new DataColumn("colUserFullName");
                DataColumn colAuthenType = new DataColumn("colAuthenType");
                DataColumn colAuthenTypeID = new DataColumn("colAuthenTypeID");
                DataColumn colAuthenCode = new DataColumn("colAuthenCode");
                DataColumn colIssueDate = new DataColumn("colIssueDate");
                DataColumn colActiveCode = new DataColumn("colActiveCode");
                DataColumn colActiveDate = new DataColumn("colActiveDate");
                DataColumn colStatusID = new DataColumn("colStatusID");
                DataColumn colStatus = new DataColumn("colStatus");
                DataColumn colDateCreated = new DataColumn("colDateCreated");
                DataColumn colUserCreated = new DataColumn("colUserCreated");
                DataColumn colDateDeleted = new DataColumn("colDateDeleted");
                DataColumn colUserDeleted = new DataColumn("colUserDeleted");
                DataColumn colDateApproved = new DataColumn("colDateApproved");
                DataColumn colUserApproved = new DataColumn("colUserApproved");
                DataColumn colDateApproveDeleted = new DataColumn("colDateApproveDeleted");
                DataColumn colUserApproveDeleted = new DataColumn("colUserApproveDeleted");

                tblOTP.Columns.Add(colFullnameCustomer);
                tblOTP.Columns.Add(colUserFullName);
                tblOTP.Columns.Add(colUserID);
                tblOTP.Columns.Add(colAuthenType);
                tblOTP.Columns.Add(colAuthenTypeID);
                tblOTP.Columns.Add(colAuthenCode);
                tblOTP.Columns.Add(colIssueDate);
                tblOTP.Columns.Add(colActiveCode);
                tblOTP.Columns.Add(colActiveDate);
                tblOTP.Columns.Add(colStatusID);
                tblOTP.Columns.Add(colStatus);
                tblOTP.Columns.Add(colDateCreated);
                tblOTP.Columns.Add(colUserCreated);
                tblOTP.Columns.Add(colDateDeleted);
                tblOTP.Columns.Add(colUserDeleted);
                tblOTP.Columns.Add(colDateApproved);
                tblOTP.Columns.Add(colUserApproved);
                tblOTP.Columns.Add(colDateApproveDeleted);
                tblOTP.Columns.Add(colUserApproveDeleted);


                //tblOTP.Columns.AddRange(new[] { colFullnameCustomer, colUserFullName, colUserID, colAuthenType, colAuthenTypeID, colAuthenCode, colIssueDate, colActiveCode, 
                //colActiveDate, colStatusID, colStatus,colDateCreated,colUserCreated,colDateDeleted,colUserDeleted,colDateApproved,colUserApproved,colDateApproveDeleted,colUserApproveDeleted });
            }
            else
            {
                tblOTP = (DataTable)ViewState["OTP"];
            }
            DataRow rowOTP = tblOTP.NewRow();
            rowOTP["colUserID"] = hidUserID.Value;
            //Thêm cột tên khách hàng khi đăng kí (thêm)
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString() == "edit")
            {
                rowOTP["colFullnameCustomer"] = hidCustName.Value.ToString();
            }
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString() == "add")
            {
                CheckBox cbxSelect;
                Label lblcustName;
                foreach (GridViewRow gvr in gvUserList.Rows)
                {
                    cbxSelect = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                    if (cbxSelect.Checked == true)
                    {
                        lblcustName = (Label)gvr.Cells[1].FindControl("lblcustName");
                        rowOTP["colFullnameCustomer"] = lblcustName.Text.Trim();
                    }
                }
            }

            // 
            rowOTP["colUserFullName"] = new SmartPortal.SEMS.User().GetFullUserByUID(hidUserID.Value, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0].Rows[0]["FULLNAME"].ToString();
            rowOTP["colAuthenType"] = ddlAuthenType.SelectedItem.Text;
            rowOTP["colAuthenTypeID"] = ddlAuthenType.SelectedValue;
            rowOTP["colAuthenCode"] = txtserialNumber.Text.Trim();
            rowOTP["colStatusID"] = SmartPortal.Constant.IPC.PENDING;//ddlStatusTK.SelectedValue;
            rowOTP["colStatus"] = Resources.labels.conpending;//ddlStatusTK.SelectedItem.Text;
            rowOTP["colIssueDate"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            rowOTP["colActiveCode"] = "";
            rowOTP["colActiveDate"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            rowOTP["colDateCreated"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            rowOTP["colUserCreated"] = Session["userName"].ToString();
            rowOTP["colDateDeleted"] = "";
            rowOTP["colUserDeleted"] = "";
            rowOTP["colDateApproved"] = "";
            rowOTP["colUserApproved"] = "";
            rowOTP["colDateApproveDeleted"] = "";
            rowOTP["colUserApproveDeleted"] = "";


            //xoá nếu trùng uid và authentype
            DataRow[] rowArr = tblOTP.Select("colUserID='" + hidUserID.Value + "' and colAuthenTypeID='" + ddlAuthenType.SelectedValue + "'");
            foreach (DataRow r in rowArr)
            {
                tblOTP.Rows.Remove(r);
            }

            tblOTP.Rows.Add(rowOTP);

            ViewState["OTP"] = tblOTP;


            gvResultChuTaiKhoan.DataSource = tblOTP;
            gvResultChuTaiKhoan.DataBind();
        }
        catch (Exception ex)
        {
        }
    }
    protected void gvResultChuTaiKhoan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvResultChuTaiKhoan.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch
        {
        }
    }
    protected void gvResultChuTaiKhoan_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable tblResultChuTaiKhoan = (DataTable)ViewState["OTP"];
            tblResultChuTaiKhoan.Rows.RemoveAt(e.RowIndex + (gvResultChuTaiKhoan.PageIndex * gvResultChuTaiKhoan.PageSize));
            //if ((tblResultChuTaiKhoan.Rows[e.RowIndex]["colStatusID"].ToString() == SmartPortal.Constant.IPC.DELETE) || (tblResultChuTaiKhoan.Rows[e.RowIndex]["colStatusID"].ToString() == SmartPortal.Constant.IPC.PENDINGFORDELETE))
            //{
            //    lblAlert.Text = Resources.labels.invalidhandle;
            //}
            //else if (tblResultChuTaiKhoan.Rows[e.RowIndex]["colStatusID"].ToString() == SmartPortal.Constant.IPC.PENDING)
            //{
            //    tblResultChuTaiKhoan.Rows[e.RowIndex]["colStatusID"] = SmartPortal.Constant.IPC.DELETE;
            //    tblResultChuTaiKhoan.Rows[e.RowIndex]["colStatus"] = Resources.labels.condelete;
            //}
            //else
            //{
            //    tblResultChuTaiKhoan.Rows[e.RowIndex]["colStatusID"] = SmartPortal.Constant.IPC.PENDINGFORDELETE;
            //    tblResultChuTaiKhoan.Rows[e.RowIndex]["colStatus"] = Resources.labels.pendingfordelete;

            //}
            ViewState["OTP"] = tblResultChuTaiKhoan;
            gvResultChuTaiKhoan.DataSource = tblResultChuTaiKhoan;
            gvResultChuTaiKhoan.DataBind();

        }
        catch
        {
        }
    }
}

