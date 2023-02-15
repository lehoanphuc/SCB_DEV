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
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSContractListCorp_Controls_Widget : System.Web.UI.UserControl
{
    string ACTION = "";
    string cn = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ddlContractType.Items.Add(new ListItem(Resources.labels.canhan, "PCO"));
            ddlContractType.Items.Add(new ListItem(Resources.labels.doanhnghiep, "CCO"));
            lblError.Text = "";

            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
            cn = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString();

            if (!IsPostBack)
            {
                #region load san pham
                ddlProductType.DataSource = new SmartPortal.SEMS.Product().GetProductByCondition("", "", "O", "", ref IPCERRORCODE, ref IPCERRORDESC);
                ddlProductType.DataTextField = "PRODUCTNAME";
                ddlProductType.DataValueField = "PRODUCTID";
                ddlProductType.DataBind();
                #endregion

                #region hien thị status
                ddlstatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
                ddlstatus.Items.Add(new ListItem(Resources.labels.conactive, SmartPortal.Constant.IPC.ACTIVE));
                ddlstatus.Items.Add(new ListItem(Resources.labels.condelete, SmartPortal.Constant.IPC.DELETE));
                ddlstatus.Items.Add(new ListItem(Resources.labels.conblock, SmartPortal.Constant.IPC.BLOCK));
                ddlstatus.Items.Add(new ListItem(Resources.labels.conpending, SmartPortal.Constant.IPC.PENDING));
                ddlstatus.Items.Add(new ListItem(Resources.labels.conreject, SmartPortal.Constant.IPC.REJECT));
                ddlstatus.Items.Insert(0, new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                #endregion

                #region load loai hop dong
                DataSet dsUserType = new DataSet();
                dsUserType = new SmartPortal.SEMS.Services().GetAllUserType("","Y",ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    ddlContractType.DataSource = dtUserType;
                    ddlContractType.DataTextField = "TYPENAME";
                    ddlContractType.DataValueField = "USERTYPE";
                    ddlContractType.DataBind();
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                #endregion

                #region load branch
                DataSet dsBranch = new DataSet();
                dsBranch = new SmartPortal.SEMS.Branch().SearchBranchByCondition("","","","", 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    ddlBranch.DataSource = dsBranch;
                    ddlBranch.DataTextField = "BRANCHNAME";
                    ddlBranch.DataValueField = "BRANCHID";
                    ddlBranch.DataBind();
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                #endregion

                BindData();
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
        //enable(disable) theo action
        switch (ACTION)
        {
            case "viewdetail":
                ddlBranch.Enabled = false;
                txtcontractno.Enabled = false;
                ddlContractType.Enabled = false;
                txtcustname.Enabled = false;
                txtenddate.Enabled = false;
                txtmodifydate.Enabled = false;
                txtopendate.Enabled = false;
                ddlProductType.Enabled = false;
                ddlstatus.Enabled = false;
                txtuserapprove.Enabled = false;
                txtusercreate.Enabled = false;
                txtuserlastmodify.Enabled = false;
                cbIsReceiver.Enabled = false;
                chkRenew.Enabled = false;
                btSave.Visible = false;
                pnCard.Visible = false;
                break;
            case "edit":
                ddlBranch.Enabled = false;
                txtcontractno.Enabled = false;
                ddlContractType.Enabled = false;
                txtcustname.Enabled = false;
                txtenddate.Enabled = true;
                txtmodifydate.Enabled = false;
                txtopendate.Enabled = true;
                ddlProductType.Enabled = true;
                ddlstatus.Enabled = false;
                txtuserapprove.Enabled = false;
                txtusercreate.Enabled = false;
                txtuserlastmodify.Enabled = false;
                cbIsReceiver.Enabled = false;
                chkRenew.Enabled = false;
                btSave.Visible = true;
                pnCard.Visible = true;
                break;
        }
        #region Lấy thông tin hợp đồng
        DataTable contractTable = new DataTable();
        contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
        if (contractTable.Rows.Count != 0)
        {
            ddlBranch.SelectedValue = contractTable.Rows[0]["BRANCHID"].ToString();
            txtcontractno.Text = contractTable.Rows[0]["CONTRACTNO"].ToString();
            ddlContractType.SelectedValue = contractTable.Rows[0]["USERTYPE"].ToString();
            txtcustname.Text = contractTable.Rows[0]["FULLNAME"].ToString();
            txtenddate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["ENDDATE"].ToString()).ToString("dd/MM/yyyy");
            txtmodifydate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["LASTMODIFY"].ToString()).ToString("dd/MM/yyyy");
            txtopendate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["CREATEDATE"].ToString()).ToString("dd/MM/yyyy");
            lblProductType.Text = contractTable.Rows[0]["PRODUCTNAME"].ToString();
            ddlstatus.SelectedValue = contractTable.Rows[0]["STATUS"].ToString();
            txtuserapprove.Text = contractTable.Rows[0]["USERAPPROVE"].ToString();
            txtusercreate.Text = contractTable.Rows[0]["USERCREATE"].ToString();
            txtuserlastmodify.Text = contractTable.Rows[0]["USERLASTMODIFY"].ToString();
            ddlProductType.SelectedValue = contractTable.Rows[0]["PRODUCTID"].ToString();
            switch (contractTable.Rows[0]["ISRECEIVERLIST"].ToString().Trim())
            {
                case "Y":
                    cbIsReceiver.Checked = true;
                    break;
                case "N":
                    cbIsReceiver.Checked = false;
                    break;
            }
            switch (contractTable.Rows[0]["IsAutorenew"].ToString().Trim())
            {
                case "Y":
                    chkRenew.Checked = true;
                    break;
                default:
                    chkRenew.Checked = false;
                    break;
            }
            //an cac button neu hop dong bi dong
            if (contractTable.Rows[0]["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
            {
                pnToolbar.Visible = false;
            }
            else
            {
                switch (ACTION)
                {
                    case "viewdetail":
                        pnToolbar.Visible = false;
                        break;
                    case "edit":
                        pnToolbar.Visible = true;
                        break;
                }
            }

            //an cac button neu ko thuoc chi nhanh 
            if (contractTable.Rows[0]["BRANCHID"].ToString().Trim() != Session["branch"].ToString().Trim())
            {
                pnToolbar.Visible = false;
            }
            else
            {
                switch (ACTION)
                {
                    case "viewdetail":
                        pnToolbar.Visible = false;
                        break;
                    case "edit":
                        pnToolbar.Visible = true;
                        break;
                }
            }
        }


        #endregion

        #region Lấy thông tin user của hỡp đồng
        DataTable userTable = new DataTable();
        userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(),"","4", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

        gvCustomerList.DataSource = userTable;
        gvCustomerList.DataBind();
        #endregion

        #region Lấy thông tin khách hàng
        DataTable custTable = new DataTable();
        custTable = (new SmartPortal.SEMS.Contract().GetCustomerByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
        if (custTable.Rows.Count != 0)
        {
            lbcustid.Text = custTable.Rows[0]["CUSTID"].ToString();
            //lbcusttype.Text = custTable.Rows[0]["CFTYPE"].ToString();
            lbfullnameCust.Text = custTable.Rows[0]["FULLNAME"].ToString();
            lbshortname.Text = custTable.Rows[0]["SHORTNAME"].ToString();
            lbbirth.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(custTable.Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy");
            lbmobi.Text = custTable.Rows[0]["TEL"].ToString();
            lbemail.Text = custTable.Rows[0]["EMAIL"].ToString();
            lbresidentaddress.Text = custTable.Rows[0]["ADDR_RESIDENT"].ToString();
           
            lbpassportcmdn.Text = custTable.Rows[0]["LICENSEID"].ToString();
            
            lbfax.Text = custTable.Rows[0]["FAX"].ToString();
           
            lbnote.Text = custTable.Rows[0]["DESCRIPTION"].ToString();
           
            switch (custTable.Rows[0]["CFTYPE"].ToString().Trim())
            {
                case SmartPortal.Constant.IPC.PERSONAL:
                    lbcusttype.Text = Resources.labels.canhan;
                    break;
                case SmartPortal.Constant.IPC.CORPORATE:
                    lbcusttype.Text = Resources.labels.doanhnghiep;
                    break;
            }
            
        }


        #endregion

        #region Lấy thông tin card của hỡp đồng - vutt card
        DataTable cardTable = new DataTable();
        cardTable = (new SmartPortal.SEMS.Card().GetCardByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString()));

        gvCard.DataSource = cardTable;
        gvCard.DataBind();
        #endregion
        //set to session to export
        Session["DataExport"] = contractTable;

    }

    protected void gvCustomerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region hRD COD
        //try
        //{
        //    CheckBox cbxSelect;
        //    HyperLink lblCustCode;
        //    Label lblCustName;
        //    Label lblPhone;
        //    Label lblIdentify;
        //    Label lblCustType;
        //    Label lblStatus;
        //    HyperLink hpEdit;
        //    HyperLink hpDelete;

        //    DataRowView drv;

        //    if (e.Row.RowType == DataControlRowType.Header)
        //    {
        //        cbxSelect = new CheckBox();
        //        cbxSelect.ID = "cbxSelectAll";
        //        cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
        //        e.Row.Cells[0].Controls.Add(cbxSelect);
        //    }

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        drv = (DataRowView)e.Row.DataItem;


        //        cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
        //        e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
        //        e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


        //        lblCustCode = (HyperLink)e.Row.FindControl("lblCustCode");
        //        lblCustName = (Label)e.Row.FindControl("lblCustName");
        //        lblPhone = (Label)e.Row.FindControl("lblPhone");
        //        lblIdentify = (Label)e.Row.FindControl("lblIdentify");
        //        lblCustType = (Label)e.Row.FindControl("lblCustType");
        //        lblStatus = (Label)e.Row.FindControl("lblStatus");

        //        hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
        //        hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

        //        //cbxSelect.Enabled = true;
        //        //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


        //        lblCustCode.Text = drv["CustCode"].ToString();
        //        lblCustCode.NavigateUrl = "~/Default.aspx?p=157";
        //        lblCustName.Text = drv["CustName"].ToString();
        //        lblPhone.Text = drv["CustPhone"].ToString();
        //        lblIdentify.Text = drv["CustIdentify"].ToString();
        //        lblCustType.Text = drv["CustType"].ToString();
        //        lblStatus.Text = drv["CustStatus"].ToString();

        //        hpEdit.Text = "Resources.labels. edit ";
        //        hpEdit.NavigateUrl = "~/Default.aspx?p=156";

        //        hpDelete.Text = "Resources.labels. delete ";
        //    }
        //}
        //catch
        //{
        //}
        #endregion

        try
        {
            CheckBox cbxSelect;
            HyperLink hpFullName;
            HyperLink hpUID;
            Label lblPhone;
            Label lblEmail;
            Label lblUserType;
            Label lblStatus;
            HyperLink hpEdit;
            HyperLink hpDelete;

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

                if (drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                {
                    cbxSelect.Enabled = false;
                }

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                hpUID = (HyperLink)e.Row.FindControl("hpUID");
                hpFullName = (HyperLink)e.Row.FindControl("hpUserFullName");
                lblPhone = (Label)e.Row.FindControl("lblPhone");
                lblEmail = (Label)e.Row.FindControl("lblEmail");
                lblUserType = (Label)e.Row.FindControl("lblUserType");
                lblStatus = (Label)e.Row.FindControl("lblStatus");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");


                hpFullName.Text = drv["FULLNAME"].ToString();
                hpUID.Text = drv["USERID"].ToString();
                hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=157&a=viewdetail&uid=" + drv["USERID"].ToString() + "&ct=" + ddlContractType.SelectedValue + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                    
                lblPhone.Text = drv["PHONE"].ToString();
                lblEmail.Text = drv["EMAIL"].ToString();
                //lblUserType.Text = drv["USERTYPE"].ToString();
                DataSet dsUserType = new DataSet();
                dsUserType = new SmartPortal.SEMS.Services().GetAllUserType(drv["USERTYPE"].ToString().Trim(),"", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    if (dtUserType.Rows.Count != 0)
                    {
                        if (dtUserType.Rows[0]["USERTYPE"].ToString() == SmartPortal.Constant.IPC.CORPORATECONTRACT)
                        {
                            switch (drv["TYPEID"].ToString().Trim())
                            {
                                case SmartPortal.Constant.IPC.CHUTAIKHOAN:
                                     lblUserType.Text = Resources.labels.chutaikhoan;
                                    break;
                                case SmartPortal.Constant.IPC.NGUOIUYQUYEN:
                                    lblUserType.Text = Resources.labels.nguoiuyquyen;
                                    break;
                                case SmartPortal.Constant.IPC.QUANTRIHETHONG:
                                    lblUserType.Text = Resources.labels.quantrihethong;
                                    break;
                                case SmartPortal.Constant.IPC.NGUOIDUNGCAP2:
                                     lblUserType.Text = Resources.labels.nguoidungcap2;
                                    break;
                                case SmartPortal.Constant.IPC.QUANLYTAICHINH:
                                    lblUserType.Text = Resources.labels.quanlytaichinh;
                                    break;
                                case SmartPortal.Constant.IPC.KETOAN:
                                    lblUserType.Text = Resources.labels.ketoantruong;
                                    break;
                            }
                        }
                        if (dtUserType.Rows[0]["USERTYPE"].ToString() == SmartPortal.Constant.IPC.PERSONALCONTRACT)
                        {
                            lblUserType.Text = dtUserType.Rows[0]["TYPENAME"].ToString();
                        }
                        
                    }
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }

                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = Resources.labels.connew;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = Resources.labels.condelete;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.conactive;
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblStatus.Text = Resources.labels.conblock;
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblStatus.Text = Resources.labels.conpending;
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblStatus.Text = Resources.labels.conreject;
                        break;
                }
                //edit by vutran 10/03/2015: loi ko sua duoc contract corp
                //if (Session["branch"].ToString().Trim() == drv["BRANCHID"].ToString().Trim())
                if (Session["branch"].ToString().Trim() == drv["BRANCHID_CT"].ToString().Trim())
                {
                    if (drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                    {
                        hpEdit.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                        hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                    }
                    else
                    {
                        switch (ACTION)
                        {
                            case "viewdetail":
                                hpEdit.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                                hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                                break;
                            case "edit":
                                hpEdit.Text = Resources.labels.edit;
                                hpEdit.ToolTip = Resources.labels.edit;
                                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=157&a=edit&uid=" + drv["USERID"].ToString() + "&ct=" + ddlContractType.SelectedValue + "&cn=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                                hpDelete.Text = Resources.labels.delete;
                                hpDelete.ToolTip = Resources.labels.delete;
                                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=167&uid=" + drv["USERID"].ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                                break;
                        }
                        
                    }
                }
                else
                {
                    hpEdit.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                    hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                }

                if (drv["TYPEID"].ToString().Trim() != "QTHT")
                {
                    if (drv["TYPEID"].ToString().Trim() != "CTK")
                    {
                    }
                    else
                    {
                        hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                        hpDelete.NavigateUrl = "";
                    }
                }
                else
                {
                    hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                    hpDelete.NavigateUrl = "";
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
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?a=add&p=155&cn=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "&ct=" + ddlContractType.SelectedValue + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpUID;
        
        string SUserid="";
        try
        {
            foreach (GridViewRow gvr in gvCustomerList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpUID = (HyperLink)gvr.Cells[1].FindControl("hpUID");
                    SUserid += hpUID.Text.Trim() + "#";
                }
            }
            if (SUserid == "")
            {
                lblError.Text = Resources.labels.youmustusertodelete;

                DataTable userTable = new DataTable();
                userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(),"","", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

                gvCustomerList.DataSource = userTable;
                gvCustomerList.DataBind();

                goto EXIT;
            }
            else
            {
                Session["_Userid"] = SUserid.Substring(0, SUserid.Length - 1);
                goto REDI;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Controls_Widget", "btnDelete_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    REDI:
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=167" + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
    EXIT: ;
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            #region Ghi log
            try
            {
                SmartPortal.Common.Log.WriteLog("SEMSCONTRACTUPDATE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "'");
            }
            catch
            {
            }
            #endregion

            //cap nhat hop dong
            //new SmartPortal.SEMS.Contract().UpdateContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Constant.IPC.PENDING, txtopendate.Text.Trim(), txtenddate.Text.Trim(),(cbIsReceiver.Checked==true?"Y":"N"), ref IPCERRORCODE, ref IPCERRORDESC);
            new SmartPortal.SEMS.Contract().UpdateContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Constant.IPC.PENDING, txtopendate.Text.Trim(), txtenddate.Text.Trim(), (cbIsReceiver.Checked == true ? "Y" : "N"), (chkRenew.Checked == true ? "Y" : "N"), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }

            if (ddlProductType.SelectedValue.Trim() != lblProductType.Text.Trim())
            {
                #region Ghi log
                try
                {
                    SmartPortal.Common.Log.WriteLog("UPDATEPRODUCTCORP", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "'");
                    SmartPortal.Common.Log.WriteLog("UPDATEPRODUCTCORP", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACTROLEDETAIL", "CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "'");
                    SmartPortal.Common.Log.WriteLog("UPDATEPRODUCTCORP", DateTime.Now.ToString(), Session["userName"].ToString(), "IBS_USERINROLE", "USERNAME IN (SELECT USERNAME FROM IBS_USERS WHERE USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "'))");
                    SmartPortal.Common.Log.WriteLog("UPDATEPRODUCTCORP", DateTime.Now.ToString(), Session["userName"].ToString(), "SMS_USERINROLE", "USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "')");
                    SmartPortal.Common.Log.WriteLog("UPDATEPRODUCTCORP", DateTime.Now.ToString(), Session["userName"].ToString(), "MB_USERINROLE", "PHONENO IN (SELECT PHONENO FROM MB_USER WHERE USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "'))");
                    SmartPortal.Common.Log.WriteLog("UPDATEPRODUCTCORP", DateTime.Now.ToString(), Session["userName"].ToString(), "PHO_USERINROLE", "PHONENO IN (SELECT PHONENO FROM PHO_USER WHERE USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "'))");
                    SmartPortal.Common.Log.WriteLog("UPDATEPRODUCTCORP", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_USERACCOUNT", "USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "')");
                    SmartPortal.Common.Log.WriteLog("UPDATEPRODUCTCORP", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_TRANRIGHTDETAIL", "USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "')");

                    
                }
                catch
                {
                }
                #endregion
                //cap nhat san pham
                new SmartPortal.SEMS.Contract().UpdateProductCorp(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), ddlProductType.SelectedValue, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                //new SmartPortal.SEMS.Contract().UpdateContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Constant.IPC.PENDING, txtopendate.Text.Trim(), txtenddate.Text.Trim(), (cbIsReceiver.Checked == true ? "Y" : "N"), (chkRenew.Checked == true ? "Y" : "N"), ref IPCERRORCODE, ref IPCERRORDESC);
            }
            
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }

            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Controls_Widget", "btSave_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
        ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_Controls_Widget", "btSave_Click", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnURL"] != null)
        {
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnURL"].ToString())));
        }
        else
        {
           Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=141"));
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=141"));
    }

    #region vutt card

    protected void gvCard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpHolderCIF;
            HyperLink hpUID;
            Label lblFullName;
            Label lblNoCard;
            Label lblType;
            Label lblStatus;
            HyperLink hpEdit;
            HyperLink hpDelete;

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

                if (drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                {
                    cbxSelect.Enabled = false;
                }

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                hpUID = (HyperLink)e.Row.FindControl("hpUID");
                hpHolderCIF = (HyperLink)e.Row.FindControl("hpHolderCIF");
                lblFullName = (Label)e.Row.FindControl("lblFullName");
                lblNoCard = (Label)e.Row.FindControl("lblNoCard");
                lblType = (Label)e.Row.FindControl("lblType");
                lblStatus = (Label)e.Row.FindControl("lblStatus");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                hpUID.Text = txtcontractno.Text.Trim() + "|" + drv["CardHolderCFCode"].ToString().Trim();
                lblType.Text = drv["LinkType"].ToString().Equals("OWN") ? "Own" : "Other";
                hpHolderCIF.Text = drv["CardHolderCFCode"].ToString();
                hpHolderCIF.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1043&a=viewdetail&hcif=" + drv["CardHolderCFCode"].ToString() + "&ct=" + ddlContractType.SelectedValue + "&cn=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));

                lblFullName.Text = drv["CardHolderName"].ToString();
                lblNoCard.Text = drv["TOTAL"].ToString();
                //lblUserType.Text = drv["USERTYPE"].ToString();
                //DataSet dsUserType = new DataSet();
                //dsUserType = new SmartPortal.SEMS.Services().GetAllUserType(drv["USERTYPE"].ToString().Trim(), "", ref IPCERRORCODE, ref IPCERRORDESC);
                //if (IPCERRORCODE == "0")
                //{
                //    DataTable dtUserType = new DataTable();
                //    dtUserType = dsUserType.Tables[0];

                //    if (dtUserType.Rows.Count != 0)
                //    {
                //        lblType.Text = dtUserType.Rows[0]["TYPENAME"].ToString();
                //    }
                //}
                //else
                //{
                //    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                //}

                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = Resources.labels.connew;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = Resources.labels.condelete;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.conactive;
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblStatus.Text = Resources.labels.conblock;
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblStatus.Text = Resources.labels.conpending;
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblStatus.Text = Resources.labels.conreject;
                        break;
                }

                if (Session["branch"].ToString().Trim() == drv["BRANCHID_CT"].ToString().Trim())
                {
                    if (drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                    {
                        //hpEdit.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                        hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";

                        switch (ACTION)
                        {
                            case "viewdetail":
                                hpEdit.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                                break;
                            case "edit":
                                hpEdit.Text = Resources.labels.edit;
                                hpEdit.ToolTip = Resources.labels.edit;
                                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1043&a=edit&hcif=" + drv["CardHolderCFCode"].ToString() + "&ct=" + ddlContractType.SelectedValue + "&cn=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                                break;
                        }
                    }
                    else
                    {
                        switch (ACTION)
                        {
                            case "viewdetail":
                                hpEdit.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                                hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                                break;
                            case "edit":
                                hpEdit.Text = Resources.labels.edit;
                                hpEdit.ToolTip = Resources.labels.edit;
                                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1043&a=edit&hcif=" + drv["CardHolderCFCode"].ToString() + "&ct=" + ddlContractType.SelectedValue + "&cn=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));

                                hpDelete.Text = Resources.labels.delete;
                                hpDelete.ToolTip = Resources.labels.delete;
                                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1044&hcif=" + drv["CardHolderCFCode"].ToString() + "&cn=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                                break;
                        }

                    }
                }
                else
                {
                    hpEdit.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                    hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_Controls_Widget", "gvCard_RowDataBound", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Controls_Widget", "gvCard_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnAddCard_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?a=add&p=1043&cn=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "&ct=" + ddlContractType.SelectedValue + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
    }

    protected void btnDeleteCard_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpUID;

        string SUserid = "";
        try
        {
            foreach (GridViewRow gvr in gvCard.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpUID = (HyperLink)gvr.Cells[1].FindControl("hpUID");
                    SUserid += hpUID.Text.Trim() + "#";
                }
            }

            if (SUserid == "")
            {
                lblError.Text = Resources.labels.youmustchoosecardtodelete;

                //DataTable userTable = new DataTable();
                //userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

                //gvCustomerList.DataSource = userTable;
                //gvCustomerList.DataBind();

                goto EXIT;
            }
            else
            {
                Session["_dCardID"] = SUserid.Substring(0, SUserid.Length - 1);
                goto REDI;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractListCrop_Controls_Widget", "btnDelete_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
        REDI:
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1044" + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
        EXIT:;
    }
    #endregion
}
