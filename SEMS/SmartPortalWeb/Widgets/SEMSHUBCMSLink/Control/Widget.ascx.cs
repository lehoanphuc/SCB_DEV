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

public partial class Widgets_HUBCMSConfig_Widget : System.Web.UI.UserControl
{
    int i = 0;
    int j = 0;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            lblError.Text = "";

            if (!IsPostBack)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString() == "add")
                {
                    #region load loai hop dong
                    DataSet dsUType = new DataSet();
                    dsUType = new SmartPortal.SEMS.Services().GetAllUserType("", "Y", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable dtUType = new DataTable();
                        dtUType = dsUType.Tables[0];

                        ddlContractType.DataSource = dtUType;
                        ddlContractType.DataTextField = "TYPENAME";
                        ddlContractType.DataValueField = "USERTYPE";
                        ddlContractType.DataBind();

                        ddlContractType.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));

                        ddlContractType.SelectedValue = "CCO";
                        ddlContractType.Enabled = false;
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
                    ddlStatus.Items.Add(new ListItem(Resources.labels.conpending, SmartPortal.Constant.IPC.PENDING));
                    #endregion

                    BindData();

                    pnContract.Visible = true;
                    pnCorp.Visible = false;
                    pnInfor.Visible = false;
                }
                else if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString() == "edit")
                {
                    pnContract.Visible = false;
                    pnCorp.Visible = false;
                    pnInfor.Visible = true;

                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] != null)
                    {
                        DataTable dtinfo = new SmartPortal.SEMS.Contract().HubCms_Search("", "", "", "", "ALL", SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"], ref IPCERRORCODE, ref IPCERRORDESC);
                        txtICorpID.Text = dtinfo.Rows[0]["CorpID"].ToString();
                        txtICorpName.Text = dtinfo.Rows[0]["CorpName"].ToString();
                        txtIContractNo.Text = dtinfo.Rows[0]["ContractNo"].ToString();
                        txtICustName.Text = dtinfo.Rows[0]["CustName"].ToString();
                        txtIAddr.Text = dtinfo.Rows[0]["Address"].ToString();
                        txtIPhoneNo.Text = dtinfo.Rows[0]["PhoneNo"].ToString();
                        txtIEmail.Text = dtinfo.Rows[0]["Email"].ToString();
                    }
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

            DataSet dtContr = new DataSet();
            dtContr = new SmartPortal.SEMS.Contract().GetContractByCondition(txtContractCode.Text.Trim(),txtCustName.Text.Trim(),txtopenPer.Text.Trim(),txtOpenDate.Text.Trim(),txtEndDate.Text.Trim(), ddlContractType.SelectedValue, ddlStatus.SelectedValue,txtlicenseid.Text.Trim(),txtcustcode.Text.Trim(), string.Empty,"", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                gvUserList.DataSource = dtContr;
                gvUserList.DataBind();

                ViewState["CONTRACTSTATE"] = dtContr.Tables[0];
            }
            
            DataTable dtCorp = new DataTable();
            dtCorp = new SmartPortal.IB.Payment().GetCorpList(ref IPCERRORCODE, ref IPCERRORDESC);
            if (dtCorp.Rows.Count == 0)
            {
                throw new Exception();
            }
            string cond = "CORPID like '%" + txtCorpCode.Text + "%' and CORPNAME like '%" + txtCorpName.Text + "%'";
            DataTable ndt = dtCorp.Select(cond).CopyToDataTable();
            ViewState["CORPSTATE"] = ndt;
            gvCorpList.DataSource = ndt;
            gvCorpList.DataBind();
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
            Label lblcustName;
            Label lblcontractno;
            Label lbllicense;
            Label lblOpen;
            Label lblOpendate;
            Label lblClosedate;
            Label lblStatus;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (RadioButton)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                lblcustName = (Label)e.Row.FindControl("lblcustName");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblcontractno = (Label)e.Row.FindControl("lblcontractno");

                lblOpen = (Label)e.Row.FindControl("lblOpen");
                lblOpendate = (Label)e.Row.FindControl("lblOpendate");
                lblClosedate = (Label)e.Row.FindControl("lblClosedate");
                lbllicense = (Label)e.Row.FindControl("lbllicense");

                cbxSelect.Attributes.Add("onclick", "SelectRADContract(this,'" + drv["CONTRACTNO"].ToString() + "')");

                if (i == 0)
                {
                    cbxSelect.Checked = true;
                    hidContractNo.Value = drv["CONTRACTNO"].ToString();
                    i += 1;
                }
                lblcontractno.Text = drv["CONTRACTNO"].ToString();
                lblcustName.Text = drv["FULLNAME"].ToString();
                lblOpen.Text = drv["UserCreate"].ToString();
                lblOpendate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["CreateDate"].ToString()).ToString("dd/MM/yyyy");
                lblClosedate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(drv["EndDate"].ToString()).ToString("dd/MM/yyyy");
                lbllicense.Text = drv["LICENSEID"].ToString();
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_HUBCMS_Add_Widget", "gvUserList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] == null)
            {
                #region create table for save data
                //tao bang
                DataTable dtCMS = new DataTable();
                dtCMS.Columns.Add(new DataColumn("CorpID"));
                dtCMS.Columns.Add(new DataColumn("CorpName"));
                dtCMS.Columns.Add(new DataColumn("ContractNo"));
                dtCMS.Columns.Add(new DataColumn("CustName"));
                dtCMS.Columns.Add(new DataColumn("Address"));
                dtCMS.Columns.Add(new DataColumn("PhoneNo"));
                dtCMS.Columns.Add(new DataColumn("Email"));
                dtCMS.Columns.Add(new DataColumn("UserCreate"));
                dtCMS.Columns.Add(new DataColumn("DateCreate"));
                dtCMS.Columns.Add(new DataColumn("UserDelete"));
                dtCMS.Columns.Add(new DataColumn("DateDelete"));
                dtCMS.Columns.Add(new DataColumn("Status"));

                //add du lieu
                DataRow rowCMS = dtCMS.NewRow();
                rowCMS["CorpID"] = txtICorpID.Text;
                rowCMS["CorpName"] = txtICorpName.Text;
                rowCMS["ContractNo"] = txtIContractNo.Text;
                rowCMS["CustName"] = txtICustName.Text;
                rowCMS["Address"] = txtIAddr.Text;
                rowCMS["PhoneNo"] = txtIPhoneNo.Text;
                rowCMS["Email"] = txtIEmail.Text;
                rowCMS["UserCreate"] = Session["userid"];
                rowCMS["DateCreate"] = DateTime.Now;
                rowCMS["UserDelete"] = "";
                rowCMS["DateDelete"] = DateTime.Parse("1/1/1900");
                rowCMS["Status"] = SmartPortal.Constant.IPC.ACTIVE;

                dtCMS.Rows.Add(rowCMS);
                #endregion

                #region insert to db
                new SmartPortal.SEMS.Contract().AddHubCmsUser(dtCMS, ref IPCERRORCODE, ref IPCERRORDESC);
                #endregion
            }
            else if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString() == "edit")
            {
                new SmartPortal.SEMS.Contract().HubCms_Edit(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(),txtIAddr.Text,txtIPhoneNo.Text,txtIEmail.Text,Session["userid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
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
                //new SmartPortal.SEMS.OTP().Update(tblOTP, tblDeleteOTP, ref IPCERRORCODE, ref IPCERRORDESC);
            }

            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.CMSUSEREXIST);
            }
            else
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] == null)
                {
                    lblError.Text = Resources.labels.thanhcong;
                }
                else
                {
                    lblError.Text = Resources.labels.thanhcong;
                }

                pnContract.Visible = false;
                btnSave.Visible = false;
                btnBack.Visible = false;
                btnExit2.Visible = true;
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
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1004"));
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnContract.Visible = false;
        pnCorp.Visible = true;
    }

    protected void btnSearchCorp_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void gvCorpList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            Label lblcorpID;
            Label lblcorpName;
            Label lblStatus;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (RadioButton)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                lblcorpID = (Label)e.Row.FindControl("lblcorpID");
                lblcorpName = (Label)e.Row.FindControl("lblcorpName");
                lblStatus = (Label)e.Row.FindControl("lblStatus");


                cbxSelect.Attributes.Add("onclick", "SelectRADCorp(this,'" + drv["CORPID"].ToString() + "')");
                cbxSelect.Attributes.Add("style", "text-align:center;");

                if (j == 0)
                {
                    cbxSelect.Checked = true;
                    hidCorpID.Value = drv["CORPID"].ToString();
                    j += 1;
                }
                lblcorpID.Text = drv["CORPID"].ToString();
                lblcorpName.Text = drv["CORPNAME"].ToString();
                lblStatus.Text = Resources.labels.sudung;
            }
        }

        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_HUBCMS_Add_Widget", "gvUserList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnBackC_Click(object sender, EventArgs e)
    {
        pnCorp.Visible = false;
        pnContract.Visible = true;
        pnInfor.Visible = false;
    }
    protected void btnNextC_Click(object sender, EventArgs e)
    {
        try
        {
            pnContract.Visible = false;
            pnCorp.Visible = false;
            pnInfor.Visible = true;

            DataTable dtcontract = (DataTable)ViewState["CONTRACTSTATE"];
            DataTable dtcorp = (DataTable)ViewState["CORPSTATE"];

            txtICorpID.Text = hidCorpID.Value.ToString();
            txtIContractNo.Text = hidContractNo.Value.ToString();
            txtICustName.Text = dtcontract.Select("ContractNo='" + txtIContractNo.Text + "'")[0].Field<string>("FULLNAME");
            txtICorpName.Text = dtcorp.Select("CORPID='" + txtICorpID.Text + "'")[0].Field<string>("CORPNAME");

            DataSet dsInfo = new SmartPortal.SEMS.Contract().GetCustomerByContractNo(txtIContractNo.Text, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Contains("0"))
            {
                txtIAddr.Text = dsInfo.Tables[0].Rows[0]["ADDR_RESIDENT"].ToString();
                txtIPhoneNo.Text = dsInfo.Tables[0].Rows[0]["TEL"].ToString();
                txtIEmail.Text = dsInfo.Tables[0].Rows[0]["Email"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_HUBCMS_Add_Widget", "btnNextC_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnBack_Click(object sender,EventArgs e)
    {
        pnContract.Visible = false;
        pnCorp.Visible = true;
        pnInfor.Visible = false;
    }

    void ClearViewState()
    {
        ViewState["CONTRACTSTATE"] = null;
        ViewState["CORPSTATE"] = null;
    }
}

