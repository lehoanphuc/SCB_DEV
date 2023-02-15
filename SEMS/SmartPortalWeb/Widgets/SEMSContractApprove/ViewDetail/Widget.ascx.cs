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
using System.Drawing;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;
using SmartPortal.Common.Utilities;
using System.Text;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System.ServiceModel.Activities;
using SmartPortal.Model;
using SmartPortal.Constant;
using System.Linq;
using SmartPortal.BLL;
using System.Data.SqlClient;
using SmartPortal.DAL;

public partial class Widgets_SEMSContractList_ViewDetail_Widget : WidgetBase
{
    string ACTION = "";
    string cn = "";
    static string IPCERRORCODE = "";
    static string IPCERRORDESC = "";
    DataSet ApproveContractTable = new DataSet();
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    private static bool pendingforapprove = false;
    public string userType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                pendingforapprove = false;
                liTabWorkingCard.Visible = false;
                BindData();
            }
            loadCombobox_KYCDocumentName();
        }
        catch (Exception ex)
        {
        }
    }
    public string CONTRACTNO
    {
        get { return ViewState["CONTRACTNO"] != null ? (string)ViewState["CONTRACTNO"] : string.Empty; }
        set { ViewState["CONTRACTNO"] = value; }
    }
    public string CONTRACTTYPE
    {
        get { return ViewState["CONTRACTTYPE"] != null ? (string)ViewState["CONTRACTTYPE"] : string.Empty; }
        set { ViewState["CONTRACTTYPE"] = value; }
    }
    public bool WALLETONLY
    {
        get { return ViewState["WALLETONLY"] != null ? (bool)ViewState["WALLETONLY"] : false; }
        set { ViewState["WALLETONLY"] = value; }
    }
    public List<DocumentModel> listDocumentModel
    {
        get
        {
            // check if not exist to make new (normally before the post back)
            // and at the same time check that you did not use the same viewstate for other object
            if (!(ViewState["listDoc"] is List<DocumentModel>))
            {
                // need to fix the memory and added to viewstate
                ViewState["listDoc"] = new List<DocumentModel>();
            }

            return (List<DocumentModel>)ViewState["listDoc"];
        }
    }
    void BindData()
    {
        try
        {
            string contractNo = "";
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.REJECT:
                    trinfo_owner.Visible = false;
                    btReject.Visible = true;
                    btReject.BackColor = Color.FromArgb(108, 117, 125);
                    divReject.Visible = true;
                    break;
                case IPC.ACTIONPAGE.APPROVE:
                    trinfo_owner.Visible = true;
                    btApprove.Visible = true;
                    btReject.BackColor = Color.FromArgb(121, 118, 179);
                    divReject.Visible = false;
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    contractNo = GetParamsPage(IPC.ID)[0].Trim();
                    btReject.Visible = false;
                    btApprove.Visible = false;
                    divReject.Visible = false;
                    trinfo_owner.Visible = false;
                    break;
            }
            if (Session["contractNo"] == null)
            {
                btApprove.Visible = false;
            }
            else
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["contractNo"];
                if (lstTran.Count > 0)
                {
                    contractNo = lstTran[0];
                }
                else
                {
                    string link = PagesBLL.GetLinkMaster_Page("148");
                    Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(link), false);
                    return;
                }

            }
            #region Lấy thông tin hợp đồng
            DataTable contractTable = new DataTable();
            contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(contractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (contractTable.Rows.Count != 0)
            {

                //SmartPortal.Common.Utilities.Utility.IsDateTime2().ToString("dd/MM/yy")
                lbbranch.Text = contractTable.Rows[0]["BRANCHNAME"].ToString();
                CONTRACTNO = lbcontractno.Text = contractTable.Rows[0]["CONTRACTNO"].ToString();
                //lbcontracttype.Text = contractTable.Rows[0]["CONTRACTTYPE"].ToString();
                lbfullname.Text = contractTable.Rows[0]["FULLNAME"].ToString();
                lbenddate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["ENDDATE"].ToString()).ToString("dd/MM/yyyy");
                lblastmodify.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["LASTMODIFY"].ToString()).ToString("dd/MM/yyyy");
                lbopendate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["CREATEDATE"].ToString()).ToString("dd/MM/yyyy");
                lbproducttype.Text = contractTable.Rows[0]["PRODUCTNAME"].ToString();
                lbuserapprove.Text = contractTable.Rows[0]["USERAPPROVE"].ToString();
                lbusercreate.Text = contractTable.Rows[0]["USERCREATE"].ToString();
                lbusermodify.Text = contractTable.Rows[0]["USERLASTMODIFY"].ToString();
                lblcustcode.Text = contractTable.Rows[0]["CFCODE"].ToString();
                lblregion.Text = contractTable.Rows[0]["REGIONNAME"].ToString();
                lblTownship.Text  =contractTable.Rows[0]["TOWNSHIPNAME"].ToString();
                userType = hdusertype.Value = LoadSubUserType(contractTable.Rows[0]["USERTYPE"].ToString());
                switch (userType)
                {
                    case "A":
                        lbcontracttype.Text = SmartPortal.Constant.IPC.AGENT; break;
                    case "M":
                        lbcontracttype.Text = SmartPortal.Constant.IPC.MERCHANT; break;
                    case "C":
                        lbcontracttype.Text = SmartPortal.Constant.IPC.CONSUMER; break;
                    case "CCO":
                        lbcontracttype.Text = Resources.labels.doanhnghiep; break;
                    default:
                        lbcontracttype.Text = SmartPortal.Constant.IPC.AGENT; break;
                }
                if (lbcontracttype.Text.Equals(SmartPortal.Constant.IPC.MERCHANT))
                {
                    DataSet ds = new DataSet();
                    ds = new AgentMerchant().LoadMerchantCode(ref IPCERRORCODE, ref IPCERRORDESC);
                    DataRow dr = ds.Tables[0].Select("").FirstOrDefault(x => (string)x["MERCHANTCODE"] == contractTable.Rows[0]["MER_CODE"].ToString());
                    ddlMerchantCategory.Text = dr["DESCRIPTION"].ToString();
                    MerchantCategory.Visible = true;
                }
                else
                {
                    MerchantCategory.Visible = false;
                }
                CONTRACTTYPE = contractTable.Rows[0]["CONTRACTTYPE"].ToString();

                if (contractTable.Rows[0]["ISRECEIVERLIST"].ToString() == "Y")
                {
                    cbIsReceiver.Checked = true;
                }
                else
                {
                    cbIsReceiver.Checked = false;
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

                DataSet dsUserType = new DataSet();

                dsUserType = new SmartPortal.SEMS.Services().GetUserType(contractTable.Rows[0]["USERTYPE"].ToString().Trim(), "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    if (dtUserType.Rows.Count != 0)
                    {
                        lbcontracttype.Text = dtUserType.Rows[0]["USERTYPE"].ToString();
                    }
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }

                switch (contractTable.Rows[0]["STATUS"].ToString())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lbstatus.Text = Resources.labels.connew;
                        lblSTS.Text = SmartPortal.Constant.IPC.NEW;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lbstatus.Text = Resources.labels.condelete;
                        lblSTS.Text = SmartPortal.Constant.IPC.DELETE;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lbstatus.Text = Resources.labels.conactive;
                        lblSTS.Text = SmartPortal.Constant.IPC.ACTIVE;
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lbstatus.Text = Resources.labels.conblock;
                        lblSTS.Text = SmartPortal.Constant.IPC.BLOCK;
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lbstatus.Text = Resources.labels.conpending;
                        lblSTS.Text = SmartPortal.Constant.IPC.PENDING;
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lbstatus.Text = Resources.labels.conreject;
                        lblSTS.Text = SmartPortal.Constant.IPC.REJECT;
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        lbstatus.Text = Resources.labels.pendingfordelete;
                        lblSTS.Text = SmartPortal.Constant.IPC.PENDINGFORDELETE;
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORAPPROVE:
                        lbstatus.Text = Resources.labels.pendingforapprove;
                        lblSTS.Text = SmartPortal.Constant.IPC.PENDINGFORAPPROVE;
                        pendingforapprove = true;
                        break;
                }
                if (Session["ActionApproveOrReject"] != null)
                {
                    string acction = (string)Session["ActionApproveOrReject"];
                    if (acction == "Approve")
                    {
                        btReject.Visible = false;
                    }
                    else if (acction == "Reject")
                    {
                        btApprove.Visible = false;
                    }
                }
            }
            else
            {
                lblError.Text = "Approve " + contractNo + " contract fail:";
            }
            #endregion
            DataTable tblAlter = new DataTable();
            tblAlter = (new SmartPortal.SEMS.Contract().GetTransactionAlterOfContract(CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (tblAlter.Rows.Count != 0)
            {
                DataRow[] drsms = tblAlter.Select("TransactionAlert='SMS'");
                if (drsms.Length > 0)
                {
                    cbSMS.Checked = true;
                }
                DataRow[] drWhatsApp = tblAlter.Select("TransactionAlert='WhatsApp'");
                if (drWhatsApp.Length > 0)
                {
                    cbWAPP.Checked = true;
                }
                DataRow[] drLine = tblAlter.Select("TransactionAlert='LINE'");
                if (drLine.Length > 0)
                {
                    cbLINE.Checked = true;
                }
                DataRow[] drTelegram = tblAlter.Select("TransactionAlert='Telegram'");
                if (drTelegram.Length > 0)
                {
                    cbTELE.Checked = true;  
                }
            }
            #region Lấy thông tin user của hỡp đồng
            DataTable userTable = new DataTable();
            userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(contractNo, "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

            gvCustomerList.DataSource = userTable;
            gvCustomerList.DataBind();
            #endregion

            #region Lấy thông tin khách hàng
            DataTable custTable = new DataTable();
            custTable = (new SmartPortal.SEMS.Contract().GetCustomerByContractNo(contractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (custTable.Rows.Count != 0)
            {
                lbcustid.Text = custTable.Rows[0]["CUSTID"].ToString();
                //lbcusttype.Text = custTable.Rows[0]["CFTYPE"].ToString();
                lbfullnameCust.Text = custTable.Rows[0]["FULLNAME"].ToString();
                lbshortname.Text = custTable.Rows[0]["SHORTNAME"].ToString();
                //HaiNT Edit birthDate (12/08/2013)
                string birthDate = SmartPortal.Common.Utilities.Utility.IsDateTime2(custTable.Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy");
                lbbirth.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;
                lbmobi.Text = custTable.Rows[0]["TEL"].ToString();
                lbemail.Text = custTable.Rows[0]["EMAIL"].ToString();
                lbresidentaddress.Text = custTable.Rows[0]["ADDRRESIDENT"].ToString();
                lbtempstay.Text = custTable.Rows[0]["ADDRTEMP"].ToString();
                lbpassportcmdn.Text = custTable.Rows[0]["LICENSEID"].ToString();
                if (!custTable.Rows[0]["ISSUEDATE"].ToString().Equals(""))
                    lbreleasedate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(custTable.Rows[0]["ISSUEDATE"].ToString()).ToString("dd/MM/yyyy");
                lbrelease.Text = custTable.Rows[0]["ISSUEPLACE"].ToString();
                lbnation.Text = custTable.Rows[0]["NATIONNAME"].ToString();
                //switch (custTable.Rows[0]["SEX"].ToString())
                //{
                //    case "M":
                //        lbsex.Text = Resources.labels.male;
                //        break;
                //    case "F":
                //        lbsex.Text = Resources.labels.female;
                //        break;
                //}
                lbbranch1.Text = custTable.Rows[0]["BRANCHNAME"].ToString();
                switch (custTable.Rows[0]["CFTYPE"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.PERSONAL:
                        lbcusttype.Text = Resources.labels.canhan;
                        break;
                    case SmartPortal.Constant.IPC.CORPORATE:
                        lbcusttype.Text = Resources.labels.doanhnghiep;
                        break;
                }
                switch (custTable.Rows[0]["SEX"].ToString())
                {
                    case "M":
                        //lbsex.Text = "Nam";
                        break;
                    case "F":
                        //lbsex.Text = "Nữ";
                        break;
                }

            }


            #endregion

            #region Lấy thông tin card của hỡp đồng - vutt card
            DataTable cardTable = new DataTable();
            cardTable = (new SmartPortal.SEMS.Card().GetCardByContractNo(contractNo));

            gvCard.DataSource = cardTable;
            gvCard.DataBind();
            #endregion

            #region lấy thông tin account được register

            if (hdsubuertype.Value == "0201")
            {
                DataSet dsacc = new DataSet();
                object[] searchObject = new object[] { CONTRACTNO };
                dsacc = _service.common("SEMS_GETACCNOREQ", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                if (dsacc != null)
                {
                    if (dsacc.Tables[0].Rows.Count != 0)
                    {
                        pnregister.Visible = true;
                        if (dsacc.Tables[0].Rows[0]["ACCNAME"].ToString() != "NONE")
                        {
                            gvlistacc.DataSource = dsacc.Tables[0];
                            gvlistacc.DataBind();
                        }
                        else
                        {
                            gvlistacc.Visible = false;
                        }
                        txtaccname.Text = dsacc.Tables[0].Rows[0]["ACCNAME"].ToString();
                        txtloginame.Text = dsacc.Tables[0].Rows[0]["LOGINNAME"].ToString();
                        txthouse.Text = dsacc.Tables[0].Rows[0]["HOUSENO"].ToString();
                        txtunit.Text = dsacc.Tables[0].Rows[0]["UNIT"].ToString();
                        txtvillage.Text = dsacc.Tables[0].Rows[0]["VILLAGE"].ToString();
                        txtdistrict.Text = dsacc.Tables[0].Rows[0]["DISTRICT"].ToString();
                        txtprovince.Text = dsacc.Tables[0].Rows[0]["PROVINCE"].ToString();
                        txtphoneno.Text = dsacc.Tables[0].Rows[0]["PHONENO"].ToString();
                        txtPapertype.Text = dsacc.Tables[0].Rows[0]["LICENSETYPE"].ToString();
                        txtPaperID.Text = dsacc.Tables[0].Rows[0]["LICENSEID"].ToString();
                        txtissuedate.Text = dsacc.Tables[0].Rows[0]["ISSUEDATE"].ToString();
                        txtexpirydate.Text = dsacc.Tables[0].Rows[0]["EXPIRYDATE"].ToString();

                    }
                }
            }
            #endregion


            //set to session to export
            Session["DataExport"] = contractTable;
            //minh add 24/7/2015 for send sms when create new contract
            //minh modify 25/8/2015 for show item notification 
            //minh modify 14/09/2015 add dropdownlist for co-owner
            string contracttype = contractTable.Rows[0]["contracttype"].ToString().Trim();
            string email_ctk = string.Empty;
            string email_coowner = string.Empty;
            bool gotemail = false;
            bool gotsms = false;
            string smscoowner = string.Empty;
            string username_ctk = string.Empty;
            string username_coownner = string.Empty;
            bool has_coowner = false;
            bool gotemail_coowner = false;
            bool gotsms_coowner = false;
            switch (contracttype)
            {
                case SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL:
                case SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT:
                    //truong hop personal se tach ra 2 dropdown neu co thong tin co-owner


                    //get email
                    if (userTable.Rows.Count > 1)   //co co-owner
                    {
                        has_coowner = true;
                        foreach (DataRow r in userTable.Rows)
                        {
                            if (r["usertype"].ToString().Trim().Equals(SmartPortal.Constant.IPC.PCO))
                            {
                                email_ctk = r["email"].ToString().Trim();
                                username_ctk = r["userid"].ToString().Trim();


                            }
                            else
                            {
                                email_coowner = r["email"].ToString().Trim();
                                username_coownner = r["userid"].ToString().Trim();
                            }
                        }
                        if (!string.IsNullOrEmpty(email_ctk))
                        {
                            gotemail = true;
                        }
                        else gotemail = false;
                        if (!string.IsNullOrEmpty(email_coowner))
                        {
                            gotemail_coowner = true;

                        }
                        else gotemail_coowner = false;
                        //get sms for co owner
                        gotsms_coowner = UsergotSMS(username_coownner);


                    }
                    else   //khong co coowner
                    {
                        has_coowner = false;
                        username_ctk = userTable.Rows[0]["userid"].ToString().Trim();
                        email_ctk = userTable.Rows[0]["email"].ToString().Trim();
                        if (!string.IsNullOrEmpty(email_ctk))
                        {
                            gotemail = true;
                        }
                        else gotemail = false;

                    }
                    //get sms:
                    //gotsms = UsergotSMS(username_ctk);
                    gotsms = true;

                    //show send info:
                    if (lblSTS.Text != SmartPortal.Constant.IPC.DELETE && lblSTS.Text != SmartPortal.Constant.IPC.PENDINGFORDELETE)
                    {
                        pnsendinfoPersonal.Visible = true;
                        if (has_coowner)
                        {
                            trinfo_coowner.Visible = true;
                            loadnotificationtodrodownlist(ddlSendinfo_coowner, gotemail_coowner, gotsms_coowner);
                        }
                        else
                        {
                            trinfo_coowner.Visible = false;
                        }
                        loadnotificationtodrodownlist(ddlSendinfo, gotemail, gotsms);

                    }
                    break;
                case SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE:
                    if (lblSTS.Text != SmartPortal.Constant.IPC.DELETE && lblSTS.Text != SmartPortal.Constant.IPC.PENDINGFORDELETE)
                        pnsenddinfoSimple.Visible = true;
                    else
                        pnsenddinfoSimple.Visible = false;
                    string username_sim_owner = string.Empty;
                    string username_sim_qlhc = string.Empty;
                    string username_sim_kt = string.Empty;
                    bool username_sim_owner_gotemail = false;
                    bool username_sim_qlhc_gotemail = false;
                    bool username_sim_kt_gotemail = false;
                    bool username_sim_owner_gotsms = true;
                    bool username_sim_qlhc_gotsms = true;
                    bool username_sim_kt_gotsms = false;
                    //if (lblSTS.Text.Equals(SmartPortal.Constant.IPC.PENDING))
                    //{
                    //    trinfo_sim_edit.Visible = true;
                    //    loadnotificationtodrodownlist(ddl_sim_edit, true, true);
                    //}
                    //else
                    {
                        int ctknum = 0;
                        int qltcnum = 0;
                        int ktnum = 0;
                        foreach (DataRow r in userTable.Rows)
                        {
                            switch (r["typeid"].ToString().Trim())
                            {
                                case SmartPortal.Constant.IPC.CHECKER:
                                    username_sim_owner = r["userid"].ToString().Trim();
                                    if (!string.IsNullOrEmpty(r["email"].ToString().Trim()))
                                        username_sim_owner_gotemail = true;
                                    trinfo_sim_owner.Visible = true;
                                    ctknum += 1;
                                    if (ctknum > 1)
                                        loadnotificationtodrodownlist_multiholder(ddl_sim_owner, username_sim_owner_gotemail, username_sim_owner_gotsms);
                                    else
                                        loadnotificationtodrodownlist(ddl_sim_owner, username_sim_owner_gotemail, username_sim_owner_gotsms);
                                    break;
                                case SmartPortal.Constant.IPC.MAKER:
                                    username_sim_qlhc = r["userid"].ToString().Trim();
                                    if (!string.IsNullOrEmpty(r["email"].ToString().Trim()))
                                        username_sim_qlhc_gotemail = true;
                                    trinfo_sim_QLHC.Visible = true;
                                    qltcnum += 1;
                                    if (qltcnum > 1)
                                        loadnotificationtodrodownlist_multiholder(ddl_sim_owner, username_sim_owner_gotemail, username_sim_owner_gotsms);
                                    else
                                        loadnotificationtodrodownlist(ddl_sim_qlhc, username_sim_qlhc_gotemail, username_sim_qlhc_gotsms);
                                    break;
                                case SmartPortal.Constant.IPC.KETOAN:
                                    username_sim_kt = r["userid"].ToString().Trim();
                                    if (!string.IsNullOrEmpty(r["email"].ToString().Trim()))
                                        username_sim_kt_gotemail = true;
                                    trinfo_sim_KT.Visible = true;
                                    ktnum += 1;
                                    if (ktnum > 1)
                                        loadnotificationtodrodownlist_multiholder(ddl_sim_owner, username_sim_owner_gotemail, username_sim_owner_gotsms);
                                    else
                                        loadnotificationtodrodownlist(ddl_sim_kt, username_sim_kt_gotemail, username_sim_kt_gotsms);
                                    break;

                            }
                        }



                    }
                    break;

                case SmartPortal.Constant.IPC.CONTRACTCORPADVANCE:
                    if (lblSTS.Text != SmartPortal.Constant.IPC.DELETE && lblSTS.Text != SmartPortal.Constant.IPC.PENDINGFORDELETE)
                        pnsendinfoAdvance.Visible = true;
                    else
                        pnsendinfoAdvance.Visible = false;

                    string userName = string.Empty;
                    bool userName_gotemail = false;
                    bool userName_gotsms = false;
                    trinfo_adv_owner.Visible = false;
                    trinfo_adv_admin.Visible = false;
                    foreach (DataRow r in userTable.Rows)
                    {
                        userName = r["userid"].ToString().Trim();
                        if (!string.IsNullOrEmpty(r["email"].ToString().Trim()))
                            userName_gotemail = true;

                        userName_gotsms = UsergotSMS(userName);
                        switch (r["typeid"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.CHECKER:
                                trinfo_adv_owner.Visible = true;
                                loadnotificationtodrodownlist_multiholder(ddl_adv_owner, userName_gotemail, userName_gotsms);
                                break;
                            case SmartPortal.Constant.IPC.ADMIN:
                                trinfo_adv_admin.Visible = true;
                                loadnotificationtodrodownlist_multiholder(ddl_adv_admin, userName_gotemail, userName_gotsms);
                                break;
                        }
                    }
                    break;
                ///Lannth 24.9.2018 - corporate matrix
                case SmartPortal.Constant.IPC.CONTRACTCORPMATRIX:
                    if (lblSTS.Text != SmartPortal.Constant.IPC.DELETE && lblSTS.Text != SmartPortal.Constant.IPC.PENDINGFORDELETE)
                        pnsendinfoMatrix.Visible = true;
                    else
                        pnsendinfoMatrix.Visible = false;

                    string userNameMtr = string.Empty;
                    bool userName_gotemailMtr = false;
                    bool userName_gotsmsMtr = false;

                    foreach (DataRow r in userTable.Rows)
                    {
                        userNameMtr = r["userid"].ToString().Trim();
                        if (!string.IsNullOrEmpty(r["email"].ToString().Trim()))
                            userName_gotemailMtr = true;
                        userName_gotsms = UsergotSMS(userNameMtr);
                        switch (r["typeid"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.CHECKER:
                                trinfo_adv_owner.Visible = true;
                                loadnotificationtodrodownlist_multiholder(ddl_matrix, userName_gotemailMtr, userName_gotsmsMtr);
                                break;
                            case SmartPortal.Constant.IPC.MAKER:
                                trinfo_adv_admin.Visible = true;
                                loadnotificationtodrodownlist_multiholder(ddl_matrix, userName_gotemailMtr, userName_gotsmsMtr);
                                break;
                        }


                    }
                    break;

            }

            //if (lbemail.Text.Trim() != string.Empty)
            //{
            //    gotemail = true;
            //}

            //SmartPortal.Common.Log.WriteLogFile("load co-owner to dropdownlist", "", "", "mail cơ-owner " + email_coowner + " sms co-owner " + smscoowner);
            //loadData_Repeater();
            //loadData_Repeater_KYC_Info();
            LoadComboboxReason();
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
    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Load document type
    // =============================================
    private void loadCombobox_KYCDocumentName()
    {
        // Save list STT KYC Request Cache
        try
        {
            DataSet ds = new DataSet();
            Cache.Remove("Wallet_KYCDocumentName");
            ds = (DataSet)Cache["Wallet_KYCDocumentName"];
            if (ds == null)
            {
                ds = _service.GetValueList("KYCDocumentName", "TYPE", ref IPCERRORCODE, ref IPCERRORDESC);
                Cache.Insert("Wallet_KYCDocumentName", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                ViewState["listDocumentType"] = ds.Tables[0];
            }
            else
            {
                ViewState["listDocumentType"] = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {

        }
    }

    // private void loadCombobox_KYCDocumentName_Repeater()
    // {
        // // Save list STT KYC Request Cache
        // try
        // {
            // DataSet ds = new DataSet();
            // ds = (DataSet)Cache["Wallet_KYCDocumentType"];
            // if (ds == null)
            // {
                // ds = _service.GetValueList("KYCDocumentName", "TYPE", ref IPCERRORCODE, ref IPCERRORDESC);
                // if (IPCERRORCODE == "0")
                // {
                    // if (ds.Tables[0].Rows.Count > 0)
                    // {
                        // ddlDocumentType.DataSource = ds;
                        // ddlDocumentType.DataValueField = "ValueID";
                        // ddlDocumentType.DataTextField = "Caption";
                        // ddlDocumentType.DataBind();
                    // }
                // }
                // Cache.Insert("Wallet_KYCDocumentType", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            // }
            // else
            // {
                // ddlDocumentType.DataSource = ds;
                // ddlDocumentType.DataValueField = "ValueID";
                // ddlDocumentType.DataTextField = "Caption";
                // ddlDocumentType.DataBind();
            // }
            // if (!IsPostBack)
            // {
                // ViewState["listDocumentType"] = ds.Tables[0];
            // }
        // }
        // catch (Exception ex)
        // {

        // }
    // }


    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Save edit name file
    // =============================================
    // protected void btnOK_Click(object sender, EventArgs e)
    // {
        // try
        // {
            // txtDocumentName.BorderColor = System.Drawing.Color.Empty;

            // if (!Utility.CheckSpecialCharacters(txtDocumentName.Text.Trim()))
            // {
                // lblError.Text = Resources.labels.DocumentName + Resources.labels.ErrorSpeacialCharacters;
                // txtDocumentName.BorderColor = System.Drawing.Color.Red;
                // txtDocumentName.Focus();
                // return;
            // }

            // string idModal = "Modal" + txtNo.Text;
            // if (txtDocumentName.Text.Trim().Equals(string.Empty))
            // {
                // txtDocumentName.BorderColor = System.Drawing.Color.Red;
                // txtDocumentName.Focus();
                // lblErrorPopup.InnerText = Resources.labels.DocumentName + Resources.labels.IsNotNull;
                // txtDocumentName.Text = txtDocname.Text;
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + idModal + "');", true);
                // //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "ShowModal("+ idModal + ");", true);
            // }
            // else
            // {
                // foreach (DocumentModel item in listDocumentModel)
                // {
                    // if (item.No.ToString() == txtNo.Text)
                    // {
                        // item.DocumentName = txtDocumentName.Text.Trim();
                        // if (item.IsNew == false)
                        // {
                            // item.IsUpdate = true;
                        // }
                        // break;
                    // }
                // }
                // loadData_Repeater();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + idModal + "');", true);
            // }
        // }
        // catch (Exception ex) { }
    // }
    // private void loadData_Repeater()
    // {
        // if (!IsPostBack)
        // {
            // loadData_ListDocument();
        // }
        // else
        // {
            // if (listDocumentModel != null)
            // {
                // rptData.DataSource = listDocumentModel;
                // rptData.DataBind();
            // }
            // else
            // {
                // loadData_ListDocument();
            // }
        // }
    // }

    // void loadData_Repeater_KYC_Info()
    // {
        // try
        // {
            // DataSet ds = new DataSet();
            // string ID = lbcustid.Text.Trim();
            // object[] searchObject = new object[] { ID, GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            // ds = _service.common("SEMS_CO_KYCRE_CUSID", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            // if (ds != null)
            // {
                // if (ds.Tables.Count > 0)
                // {
                    // Repeater1.DataSource = ds.Tables[0];
                    // Repeater1.DataBind();
                // }
            // }
        // }
        // catch (Exception ex)
        // {
            // SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            // SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        // }
    // }


    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Load document list
    // =============================================
    // private void loadData_ListDocument()
    // {
        // listDocumentModel.Clear();
        // DataTable contractTable = new DataTable();
        // contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
        // DataTable ds = new DataTable();
        // ds = new SmartPortal.SEMS.Contract().LoaKYCInfor(contractTable.Rows[0]["USERID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        // if (ds != null)
        // {
            // for (int i = 0; i < ds.Rows.Count; i++)
            // {
                // DocumentModel item = new DocumentModel();
                // item.IsNew = false;
                // item.No = int.Parse(ds.Rows[i]["No"].ToString());
                // item.DocumentID = int.Parse(ds.Rows[i]["DocumentID"].ToString());
                // item.DateCreated = ds.Rows[i]["DateCreated"].ToString();
                // item.UserCreated = ds.Rows[i]["UserCreated"].ToString();
                // item.DocumentCode = ds.Rows[i]["DocumentCode"].ToString();
                // item.DocumentName = ds.Rows[i]["DocumentName"].ToString();
                // item.DocumentType = ds.Rows[i]["DocumentType"].ToString();
                // item.Status = ds.Rows[i]["Caption"].ToString();
                // item.ValueStatus = ds.Rows[i]["ValueStatus"].ToString();
                // item.File = ds.Rows[i]["File"].ToString();
                // listDocumentModel.Add(item);
            // }
            // rptData.DataSource = listDocumentModel;
            // rptData.DataBind();
        // }
    // }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	import file
    // =============================================
    // protected void btnImportFile_Click(object sender, EventArgs e)
    // {
        // try
        // {
            // lblError.Text = string.Empty;
            // DocumentModel doc = new DocumentModel();
            // if (documentUpload.HasFile)
            // {
                // String fileExtension = System.IO.Path.GetExtension(documentUpload.FileName).ToLower();
                // //string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP", ".PDF", ".WEBP" };
                // string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP" };
                // bool checkExtensions = false;
                // for (int i = 0; i < allowedExtensions.Length; i++)
                // {
                    // if (fileExtension.ToUpper() == allowedExtensions[i])
                    // {
                        // checkExtensions = true;
                        // break;
                    // }
                // }
                // if (checkExtensions == false)
                // {
                    // lblError.Text = "Extensions " + fileExtension + " not support";
                    // return;
                // }

                // //Resize image
                // System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(documentUpload.PostedFile.InputStream);
                // string base64String;
                // int imageHeight = imageToBeResized.Height;
                // int imageWidth = imageToBeResized.Width;
                // int maxHeight = 400;
                // int maxWidth = 600;
                // imageHeight = (imageHeight * maxWidth) / imageWidth;
                // imageWidth = maxWidth;
                // if (imageHeight > maxHeight)
                // {
                    // imageWidth = (imageWidth * maxHeight) / imageHeight;
                    // imageHeight = maxHeight;
                // }
                // Bitmap bitmap = new Bitmap(imageToBeResized, imageWidth, imageHeight);
                // System.IO.MemoryStream stream = new MemoryStream();
                // bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                // stream.Position = 0;
                // byte[] byteImage = stream.ToArray();
                // base64String = Convert.ToBase64String(byteImage);

                // DocumentModel item = new DocumentModel();
                // DataTable tb = (DataTable)ViewState["listDocumentType"];
                // item.IsNew = true;
                // item.DocumentCode = string.Empty;
                // item.DateCreated = DateTime.Now.ToString("dd/MM/yyyy");
                // item.Status = string.Empty;
                // int no = listDocumentModel.Count;
                // item.No = no + 1;
                // item.DocumentName = tb.Rows[no]["Caption"].ToString();
                // item.DocumentType = tb.Rows[no]["ValueID"].ToString();
                // item.File = base64String;
                // listDocumentModel.Add(item);
                // loadData_Repeater2();
            // }
            // else
            // {
                // lblError.Text = Resources.labels.Importfileuploadnotfound;
            // }
        // }
        // catch (Exception ex)
        // {
        // }
    // }

    //import file in popup
    // protected void btnImportFileUpdate_Click(object sender, EventArgs e)
    // {
        // try
        // {
            // lblError.Text = string.Empty;
            // string idModal = "Modal" + txtNo.Text;
            // if (fileUpdate.HasFile)
            // {
                // String fileExtension = System.IO.Path.GetExtension(fileUpdate.FileName).ToLower();
                // //string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP", ".PDF", ".WEBP" };
                // string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP" };
                // bool checkExtensions = false;
                // for (int i = 0; i < allowedExtensions.Length; i++)
                // {
                    // if (fileExtension.ToUpper() == allowedExtensions[i])
                    // {
                        // checkExtensions = true;
                        // break;
                    // }
                // }
                // if (checkExtensions == false)
                // {
                    // lblError.Text = "Extensions " + fileExtension + " not support";
                    // return;
                // }
                // //Resize image
                // System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fileUpdate.PostedFile.InputStream);
                // string base64String;
                // int imageHeight = imageToBeResized.Height;
                // int imageWidth = imageToBeResized.Width;
                // int maxHeight = 400;
                // int maxWidth = 600;
                // imageHeight = (imageHeight * maxWidth) / imageWidth;
                // imageWidth = maxWidth;
                // if (imageHeight > maxHeight)
                // {
                    // imageWidth = (imageWidth * maxHeight) / imageHeight;
                    // imageHeight = maxHeight;
                // }
                // Bitmap bitmap = new Bitmap(imageToBeResized, imageWidth, imageHeight);
                // System.IO.MemoryStream stream = new MemoryStream();
                // bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                // stream.Position = 0;
                // byte[] byteImage = stream.ToArray();
                // base64String = Convert.ToBase64String(byteImage);

                // foreach (DocumentModel item in listDocumentModel)
                // {
                    // if (item.No.ToString() == txtNo.Text)
                    // {
                        // item.File = base64String;
                        // item.IsUpdate = true;
                        // break;
                    // }
                // }
                // rptData.DataSource = listDocumentModel;
                // rptData.DataBind();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + idModal + "');", true);
            // }
            // else
            // {
                // lblError.Text = Resources.labels.Importfileuploadnotfound;
            // }
        // }
        // catch (Exception ex)
        // {
        // }
    // }

    // private void loadData_Repeater2()
    // {
        // rptData.DataSource = listDocumentModel;
        // rptData.DataBind();
    // }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Download all document in Zip
    // =============================================
    protected void btnDownloadAll_Click(object sender, EventArgs e)
    {
        try
        {

            ZipOutputStream zos = null;
            MemoryStream ms = null;

            DataTable contractTable = new DataTable();
            contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            DataTable ds = new DataTable();
            ds = new SmartPortal.SEMS.Contract().LoaKYCInfor(contractTable.Rows[0]["CUSTID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            string filename = contractTable.Rows[0]["USERID"].ToString() + "_" + DateTime.Now.ToString("dd/MM/yyyy");
            if (ds != null)
            {

                ms = new MemoryStream();
                zos = new ZipOutputStream(ms);
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    string ImgStr = ds.Rows[i]["File"].ToString();
                    string ImgName = ds.Rows[i]["DocumentCode"].ToString();
                    int indexname = ImgStr.IndexOf(",");
                    string path = ImgStr.Substring(indexname + 1);
                    string imageName = ImgName + ".jpg";
                    byte[] imageBytes = Convert.FromBase64String(path);

                    ZipEntry imgEntry = new ZipEntry(imageName);
                    imgEntry.Size = imageBytes.Length;

                    zos.PutNextEntry(imgEntry);
                    zos.Write(imageBytes, 0, imageBytes.Length);
                }
                zos.Finish();
                zos.Close();

                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".zip");

                Response.Clear();
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Download document
    // =============================================
    public void SaveImage(string ImgName, string ImgStr)
    {
        try
        {
            int indexname = ImgStr.IndexOf(",");
            string path = ImgStr.Substring(indexname + 1);
            string imageName = ImgName + ".jpg";
            byte[] imageBytes = Convert.FromBase64String(path);
            Response.Clear();
            Response.ContentType = "image/jpg";
            Response.AddHeader("content-disposition", "attachment; filename=" + imageName);
            Response.BinaryWrite(imageBytes);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }

    }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	View - Edit - Delete - Download document
    // =============================================
    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        //if (CheckPermitPageAction(commandName))
        //{
        switch (commandName)
        {
            case IPC.ACTIONPAGE.EDIT:
                break;
            case IPC.ACTIONPAGE.DETAILS:

                break;
            case IPC.ACTIONPAGE.EXPORT:
                int indexname = commandArg.IndexOf("---");
                string name = commandArg.Substring(0, indexname);
                string path = commandArg.Substring(indexname + 3);
                SaveImage(name, path);
                break;
            case IPC.ACTIONPAGE.DELETE:
                if (listDocumentModel.Count > 0)
                {
                    foreach (var item in listDocumentModel)
                    {
                        // Trường hợp Del file vừa import
                        string[] arrListStr = commandArg.Split('|');

                        if (item.No.ToString() == arrListStr[0])
                        {
                            if (item.IsNew)
                            {
                                listDocumentModel.Remove(item);
                                resetNoDocument();
                                // rptData.DataSource = listDocumentModel;
                                // rptData.DataBind();
                                return;
                            }
                            else // Trường hợp Del file trong database
                            {
                                deleteDocument(arrListStr[1]);
                                resetNoDocument();
                                // rptData.DataSource = listDocumentModel;
                                // rptData.DataBind();
                                return;
                            }
                        }

                    }
                }
                break;
        }
    }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Reset No for document after delete
    // =============================================
    private void resetNoDocument()
    {
        int index = 1;
        foreach (var item in listDocumentModel)
        {
            item.No = index;
            index++;
        }
    }
    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Delete document
    // =============================================
    public void deleteDocument(String DocumentID)
    {
        try
        {
            DataSet ds = new DataSet();
            string UserId = HttpContext.Current.Session["userID"].ToString();
            object[] searchObject = new object[] { DocumentID, UserId };
            ds = _service.common("SEMS_DOC_DELETE_FILE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.deletesuccessfully;
            }
            else lblError.Text = IPCERRORDESC;

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Load list document and enable button delete or not
    // =============================================
    // protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    // {
        // try
        // {
            // if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            // {
                // TextBox txtDocumentName = (TextBox)e.Item.FindControl("txtDocumentName");
                // LinkButton lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
                // Button btnOK = (Button)e.Item.FindControl("btnOK");
                // DropDownList ddlDocumentType = (DropDownList)e.Item.FindControl("ddlDocumentType");

                // loadCombobox_KYCDocumentName_Repeater();
                // ddlDocumentType.SelectedValue = hdfDocumentType.Value;

                // if (ACTION == IPC.ACTIONPAGE.DETAILS)
                // {
                    // txtDocumentName.Enabled = false;
                    // ddlDocumentType.Enabled = false;
                    // fileUpdate.Visible = false;
                    // btnImportUpdate.Visible = false;
                // }

                // lbtnDelete.CssClass = "btn btn-secondary";
                // lbtnDelete.OnClientClick = null;
                // btnOK.CssClass = "btn btn-primary";
                // HtmlGenericControl lbStatusDocument = (HtmlGenericControl)e.Item.FindControl("lbStatusDocument");
                // string status = lbStatusDocument.InnerText;
                // if (status.Equals("D"))
                // {
                    // lbtnDelete.Enabled = false;
                    // txtDocumentName.Enabled = false;
                    // btnOK.Enabled = false;
                // }

                // // Approve so enable all controls
                // txtDocumentName.Enabled = false;
                // ddlDocumentType.Enabled = false;
                // fileUpdate.Visible = false;
                // btnImportUpdate.Visible = false;
                // btnOK.Enabled = false;
            // }
        // }
        // catch (Exception ex)
        // {

        // }
    // }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Save document in database
    // =============================================
    // protected void btSaveDoc_Click(object sender, EventArgs e)
    // {
        // try
        // {
            // DataSet ds = new DataSet();
            // DataTable contractTable = new DataTable();
            // contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            // string MerchantID = contractTable.Rows[0]["USERID"].ToString();
            // if (listDocumentModel.Count > 0)
            // {
                // foreach (var item in listDocumentModel)
                // {
                    // if (item.IsNew)
                    // {
                        // object[] insertDoc = new object[] { MerchantID, item.DocumentName, item.File, HttpContext.Current.Session["userID"].ToString() };

                        // ds = _service.common("SEMS_IMPORT_DOCUMENT", insertDoc, ref IPCERRORCODE, ref IPCERRORDESC);
                        // if (ds.Tables[0].Rows.Count > 0)
                        // {
                            // item.DocumentCode = ds.Tables[0].Rows[0]["DocumentCode"].ToString();
                            // item.DateCreated = ds.Tables[0].Rows[0]["DateCreate"].ToString();
                            // item.UserCreated = ds.Tables[0].Rows[0]["UserCreated"].ToString();
                            // item.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                        // }
                    // }
                    // else
                    // {
                        // if (item.IsUpdate == true)
                        // {
                            // object[] updateDoc = new object[] { item.DocumentID, item.UserId, txtDocumentName.Text, HttpContext.Current.Session["userID"].ToString() };
                            // ds = _service.common("SEMS_DOC_UPDATE", updateDoc, ref IPCERRORCODE, ref IPCERRORDESC);
                            // if (IPCERRORCODE != "0")
                            // {
                                // lblError.Text = "Update Unsuccess - " + IPCERRORDESC;
                            // }
                        // }
                    // }
                // }
            // }
            // if (IPCERRORCODE == "0")
            // {
                // lblError.Text = Resources.labels.thanhcong;
                // loadData_ListDocument();
                // BindData();
            // }
            // else
            // {
                // lblError.Text = "Import file unsuccess - " + IPCERRORDESC;
            // }
        // }
        // catch (Exception ex)
        // {
            // SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            // SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        // }

    // }

    protected void gvCustomerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpFullName;
            Label lblPhone;
            Label lblEmail;
            Label lblUserType;
            Label lblStatus;
            Label lblType;
            HyperLink hpEdit;
            HyperLink hpDelete;

            DataRowView drv;

            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    cbxSelect = new CheckBox();
            //    cbxSelect.ID = "cbxSelectAll";
            //    cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
            //    e.Row.Cells[0].Controls.Add(cbxSelect);
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");
                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");


                hpFullName = (HyperLink)e.Row.FindControl("hpUserFullName");
                lblPhone = (Label)e.Row.FindControl("lblPhone");
                lblEmail = (Label)e.Row.FindControl("lblEmail");
                lblUserType = (Label)e.Row.FindControl("lblUserType");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblType = (Label)e.Row.FindControl("lblType");
                //hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                //hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");




                lblPhone.Text = drv["PHONE"].ToString();
                lblEmail.Text = drv["EMAIL"].ToString();

                DataSet dsUserType = new DataSet();
                switch (drv["USERTYPE"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.CHECKER:
                    case SmartPortal.Constant.IPC.MAKER:
                    case SmartPortal.Constant.IPC.ADMIN:
                        dsUserType = new SmartPortal.SEMS.Services().GetUserType("0603", "", ref IPCERRORCODE, ref IPCERRORDESC); break;
                    default:
                        dsUserType = new SmartPortal.SEMS.Services().GetUserType(drv["USERTYPE"].ToString().Trim(), "", ref IPCERRORCODE, ref IPCERRORDESC);
                        break;
                }
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    if (dtUserType.Rows.Count != 0)
                    {
                        lblUserType.Text = dtUserType.Rows[0]["USERTYPE"].ToString();
                    }
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
                switch (drv["TYPEID"].ToString())
                {
                    case SmartPortal.Constant.IPC.MAKER:
                        lblType.Text = "Maker";
                        break;
                    case SmartPortal.Constant.IPC.CHECKER:
                        lblType.Text = "Checker";
                        break;
                    case SmartPortal.Constant.IPC.ADMIN:
                        lblType.Text = "Adiministrator";
                        break;
                }
                hpFullName.Text = drv["FULLNAME"].ToString();
                if (drv["CUSTCODE"].ToString().Equals(string.Empty) && drv["CTYPE"].ToString().Trim() == "W")
                {
                    if (ACTION.Equals(IPC.ACTIONPAGE.APPROVE))
                    {
                        hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1119&a=APPROVER&uid=" + drv["USERID"].ToString() + "&ct=" + CONTRACTTYPE + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));

                    }
                    else
                    {

                        hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1119&a=viewdetail&uid=" + drv["USERID"].ToString() + "&ct=" + CONTRACTTYPE + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery) + "&pagefather=" + "SEMSDetailContractApprove");

                    }
                    WALLETONLY = true;
                }
                else
                {
                    if (ACTION.Equals(IPC.ACTIONPAGE.APPROVE))
                    {
                        if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE)
                           || CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE)
                           || CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX)
                           )
                        {
                            hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1157&a=APPROVER&uid=" + drv["USERID"].ToString() + "&ct=" + CONTRACTTYPE + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                        }
                        else
                        {
                            hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=157&a=APPROVER&uid=" + drv["USERID"].ToString() + "&ct=" + CONTRACTTYPE + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                        }

                    }
                    else
                    {
                        if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE)
                          || CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE)
                          || CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX)
                          )
                        {
                            hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1157&a=viewdetail&uid=" + drv["USERID"].ToString() + "&ct=" + CONTRACTTYPE + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery) + "&pagefather=" + "SEMSDetailContractApprove");
                        }
                        else
                        {
                            hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=157&a=viewdetail&uid=" + drv["USERID"].ToString() + "&ct=" + CONTRACTTYPE + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery) + "&pagefather=" + "SEMSDetailContractApprove");
                        }


                    }
                    WALLETONLY = false;
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
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        lblStatus.Text = Resources.labels.pendingfordelete;
                        break;
                    case SmartPortal.Constant.IPC.REJECTFORMNEW:
                        lblStatus.Text = Resources.labels.rejectformnew;
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORAPPROVE:
                        lblStatus.Text = Resources.labels.pendingformnew;
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORACTIVE:
                        lblStatus.Text = Resources.labels.pendingforactive;
                        break;
                }


                //hpEdit.Text = "Resources.labels. edit ";
                //hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=4&p=157&a=edit&uid=" + drv["USERID"].ToString());
                //hpDelete.Text = "Resources.labels. delete ";

            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_ViewDetail_Widget", "gvCustomerList_RowDataBound", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_ViewDetail_Widget", "gvCustomerList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    void SendInfoLogin(string contractNo)
    {
        try
        {
            string type = string.Empty;
            string hpcontractNo = contractNo;
            string custID = "";
            string contracttype = "";
            string UserNameMB = string.Empty;
            string authenType = string.Empty;
            string loginMethod = string.Empty;
            //lay thong tin hop dong boi contractno
            DataTable contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(hpcontractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            if (contractTable.Rows.Count != 0)
            {
                contracttype = contractTable.Rows[0]["contracttype"].ToString().Trim();

                if (contracttype == SmartPortal.Constant.IPC.CONTRACTCORPMATRIX)
                {
                    SendInfoLoginCorpMatrix(contractNo, contractTable);
                    return;
                }
            }
            SmartPortal.Common.Log.WriteLogFile("", "", "", "==start send info");
            Antlr3.ST.StringTemplate tmplAttachment = new Antlr3.ST.StringTemplate();
            tmplAttachment = SmartPortal.Common.ST.GetStringTemplate("SEMSContractApprove", "ContractAttachment" + System.Globalization.CultureInfo.CurrentCulture.ToString());

            Antlr3.ST.StringTemplate tmplBody = new Antlr3.ST.StringTemplate();
            tmplBody = SmartPortal.Common.ST.GetStringTemplate("SEMSContractApprove", "ContractApprove" + System.Globalization.CultureInfo.CurrentCulture.ToString());


            //lay thong tin hop dong de gui mail

            tmplAttachment.Reset();
            tmplBody.Reset();
            //gan thong tin hop dong vao stringtemplate
            if (contractTable.Rows.Count != 0)
            {
                tmplAttachment.SetAttribute("CONTRACTNO", contractTable.Rows[0]["CONTRACTNO"].ToString());
                tmplBody.SetAttribute("CONTRACTNO", contractTable.Rows[0]["CONTRACTNO"].ToString());

                switch (contractTable.Rows[0]["USERTYPE"].ToString())
                {
                    case "0101":
                    case "0102":
                    case "0103":
                    case "0104":
                        tmplAttachment.SetAttribute("CONTRACTTYPE", Resources.labels.Agent); break;
                    case "0201":
                    case "0202":
                    case "0203":
                    case "0204":
                        tmplAttachment.SetAttribute("CONTRACTTYPE", Resources.labels.Consumer); break;
                    case "0301":
                        tmplAttachment.SetAttribute("CONTRACTTYPE", Resources.labels.Merchant); break;
                    case "0603":
                        tmplAttachment.SetAttribute("CONTRACTTYPE", Resources.labels.Corporate); break;
                }
                tmplAttachment.SetAttribute("OPENDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["CREATEDATE"].ToString()).ToString("dd/MM/yyyy"));
                tmplAttachment.SetAttribute("ENDDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["ENDDATE"].ToString()).ToString("dd/MM/yyyy"));


                tmplAttachment.SetAttribute("PRODUCT", contractTable.Rows[0]["PRODUCTNAME"].ToString());


                custID = contractTable.Rows[0]["CUSTID"].ToString();
            }

            //lay thong tin khach hang
            DataTable custTable = (new SmartPortal.SEMS.Customer().GetCustomerByCustcode(custID, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            //gan thong tin hop dong vao stringtemplate
            if (custTable.Rows.Count != 0)
            {
                tmplAttachment.SetAttribute("CUSTID", custTable.Rows[0]["CUSTID"].ToString());
                tmplAttachment.SetAttribute("FULLNAME", custTable.Rows[0]["FULLNAME"].ToString());
                switch (custTable.Rows[0]["CFTYPE"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.PERSONAL:
                        tmplAttachment.SetAttribute("CUSTTYPE", Resources.labels.canhan);
                        break;
                    case SmartPortal.Constant.IPC.CORPORATE:
                        tmplAttachment.SetAttribute("CUSTTYPE", Resources.labels.doanhnghiep);
                        break;
                }
                tmplAttachment.SetAttribute("PHONE", custTable.Rows[0]["TEL"].ToString());
                tmplAttachment.SetAttribute("EMAIL", custTable.Rows[0]["EMAIL"].ToString());
                tmplBody.SetAttribute("EMAIL", custTable.Rows[0]["EMAIL"].ToString());
                tmplAttachment.SetAttribute("CIF", custTable.Rows[0]["CUSTCODE"].ToString());
            }

            //lay thong tin tai khoan

            DataTable userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo, "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            DataView dv = userTable.AsDataView();
            dv.Sort = "UserLevel DESC";
            userTable = dv.ToTable();

            string userInfo = string.Empty;

            //gan thong tin user vao stringtemplate
            foreach (DataRow row in userTable.Rows)
            {
                //04.10.2016 minh modify this like edit user: New or Unused-> send, logined-> not send
                //declare parameters for sms contract

                string IBuser = "";
                string IBpass = "";
                string PassMB = string.Empty;
                string PinCodeMB = string.Empty;
                string SMSphone = "";
                string PhoneNoMB = string.Empty;
                string SMSdefaultAccount = "";
                string SMSpincode = "";
                string PHOuser = "";
                string PHOpass = "";
                string PHOpincode = "";
                string PhoneNo = "";
                //edit by vutran 11102014
                //Kiem tra xem khach hang da dung IBMBSMS_Banking chua
                bool boolIB = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "IB", ref IPCERRORCODE, ref IPCERRORDESC);
                bool boolMB = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "MB", ref IPCERRORCODE, ref IPCERRORDESC);
                bool boolAM = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "AM", ref IPCERRORCODE, ref IPCERRORDESC);
                bool boolSMS = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "SMS", ref IPCERRORCODE, ref IPCERRORDESC);
                //if (boolIB || boolMB || boolSMS)
                //{

                //lay kieu nguoi dung

                switch (contractTable.Rows[0]["CONTRACTTYPE"].ToString())
                {
                    case SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL:
                        type = SmartPortal.Constant.IPC.CONSUMER; break;
                    case SmartPortal.Constant.IPC.AGENTMERCHANT:
                        type = string.Empty; break;
                    case SmartPortal.Constant.IPC.CONTRACTCORPADVANCE:
                    case SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE:
                    case SmartPortal.Constant.IPC.CONTRACTCORPMATRIX:
                        type = row["TYPEID"].ToString(); break;
                }

                string ut = "";
                switch (row["TYPEID"].ToString().Trim())
                {
                    case "":
                        ut = Resources.labels.chutaikhoan;
                        break;
                    case "AD":
                        ut = Resources.labels.quantrihethong;
                        break;
                    case "CTK":
                        ut = Resources.labels.chutaikhoan;
                        break;
                    case "C2":
                        ut = Resources.labels.nguoidungcap2;
                        break;
                    case "NUQ":
                        ut = Resources.labels.nguoiuyquyen;
                        break;
                    case SmartPortal.Constant.IPC.QUANLYTAICHINH:
                        ut = Resources.labels.quanlytaichinh;
                        break;
                    case SmartPortal.Constant.IPC.KETOAN:
                        ut = Resources.labels.ketoan;
                        break;
                    case SmartPortal.Constant.IPC.DONGCHUTAIKHOAN:
                        ut = Resources.labels.dongchutaikhoan;
                        break;
                    case "CK":
                        ut = "Checker";
                        break;
                    case "MK":
                        ut = "Maker";
                        break;
                }

                StringBuilder st = new StringBuilder();
                st.Append("<table style='width:100%;font-size:10px;'>");

                st.Append("<tr>");
                st.Append("<td style='background:silver;background-color: #C0C0C0; line-height:13px;' colspan=4>");
                st.Append("<b>  ACCOUNT INFORMATION</b>");
                st.Append("</td>");
                st.Append("</tr>");

                st.Append("<tr>");
                st.Append("<td witdth='15%' style='width:15%' colspan=1>  ");
                st.Append(Resources.labels.tendaydu + " :");
                st.Append("</td>");
                st.Append("<td witdth='35%' style='width:35%' colspan=1>  ");
                st.Append(row["FULLNAME"].ToString());
                st.Append("</td>");
                st.Append("<td witdth='15%' style='width:15%' colspan=1>  ");
                st.Append("Email :");
                st.Append("</td>");
                st.Append("<td witdth='35%'  style='width:35%' colspan=1>  ");
                st.Append(row["EMAIL"].ToString());
                st.Append("</td>");
                st.Append("</tr>");

                st.Append("<tr>");
                st.Append("<td colspan=1>  ");
                st.Append(Resources.labels.phone + " :");
                st.Append("</td>");
                st.Append("<td colspan=1>  ");
                st.Append(row["PHONE"].ToString());
                st.Append("</td>");
                st.Append("<td colspan=1>  ");
                st.Append(Resources.labels.kieunguoidung + " :");
                st.Append("</td>");
                st.Append("<td colspan=1>  ");
                st.Append(ut);
                st.Append("</td>");
                st.Append("</tr>");
                st.Append("<br><br>");

                //if (boolIB)
                //{
                //lay het các tai khoan Ibank cua user theo userID
                DataSet accountIBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, type, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                DataTable accountIBTable = accountIBDataset.Tables[0];
                if (accountIBTable.Rows.Count != 0 && !string.IsNullOrEmpty(accountIBTable.Rows[0][0].ToString()))
                {
                    if (accountIBTable.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<B>  INTERNET BANKING</B><hr/>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.username + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + accountIBTable.Rows[0]["USERNAME"].ToString() + "</b>");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.password + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + ((boolIB) ? DecryptPass(accountIBTable.Rows[0]["PASSWORD"].ToString()) : "********") + "</b>");
                        st.Append("</td>");
                        st.Append("</tr>");
                        if (boolIB)
                        {
                            IBuser = accountIBTable.Rows[0]["USERNAME"].ToString();
                            IBpass = DecryptPass(accountIBTable.Rows[0]["PASSWORD"].ToString());
                        }
                    }
                }
                //}
                //if (boolSMS)
                //{
                //lay het các tai khoan SMS cua user theo userID
                DataSet accountSMSDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                DataTable accountSMSTable = accountSMSDataset.Tables[0];
                if (accountSMSTable.Rows.Count != 0)
                {
                    if (accountSMSTable.Rows.Count != 0 && !string.IsNullOrEmpty(accountSMSTable.Rows[0][0].ToString()))
                    {
                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<br/>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<B>  SMS</B><hr/>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.phone + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + accountSMSTable.Rows[0]["UN"].ToString() + "</b>");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.taikhoanmacdinh + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + accountSMSTable.Rows[0]["DEFAULTACCTNO"].ToString() + "</b>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.mapin + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        //st.Append("<b>" + accountSMSTable.Rows[0]["PINCODE"].ToString() + "</b>");
                        if (!string.IsNullOrEmpty(accountSMSTable.Rows[0]["PINCODE"].ToString()))
                            st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountSMSTable.Rows[0]["PINCODE"].ToString()) + "</b>");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("");
                        st.Append("</td>");
                        st.Append("</tr>");
                        if (boolSMS)
                        {
                            SMSphone = accountSMSTable.Rows[0]["UN"].ToString();
                            SMSdefaultAccount = accountSMSTable.Rows[0]["DEFAULTACCTNO"].ToString();
                            //SMSpincode = accountSMSTable.Rows[0]["PINCODE"].ToString();
                            if (!string.IsNullOrEmpty(accountSMSTable.Rows[0]["PINCODE"].ToString()))
                                SMSpincode = SmartPortal.Security.Encryption.Decrypt(accountSMSTable.Rows[0]["PINCODE"].ToString());
                        }

                    }
                }
                //}
                //if (boolMB)
                //{
                //lay het các tai khoan MOBILE AM cua user theo userID
                #region MBA

                DataSet accountMBADataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.AM, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                DataTable accountMBATable = accountMBADataset.Tables[0];

                if (accountMBATable.Rows.Count != 0 && !string.IsNullOrEmpty(accountMBATable.Rows[0][0].ToString()))
                {
                    if (accountMBATable.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        authenType = accountMBATable.Rows[0]["AUTHENTYPE"].ToString();
                        loginMethod = accountMBATable.Rows[0]["LOGINMETHOD"].ToString();
                        UserNameMB = accountMBATable.Rows[0]["USERNAME"].ToString();
                        PhoneNoMB = accountMBATable.Rows[0]["UN"].ToString();
                        if (!accountMBATable.Rows[0]["PASS"].ToString().Equals(""))
                        {
                            PassMB = DecryptPass(accountMBATable.Rows[0]["PASS"].ToString());
                        }
                        //PinCodeMB = SmartPortal.Security.Encryption.Decrypt(accountMBATable.Rows[0]["PINCODE"].ToString());
                    }
                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<br/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<B>  MOBILE BANKING</B><hr/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    if (loginMethod.Equals("PHONENO"))
                    {
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.phone + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + accountMBATable.Rows[0]["UN"].ToString() + "</b>");
                        st.Append("</td>");
                    }
                    else
                    {
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.username + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + accountMBATable.Rows[0]["USERNAME"].ToString() + "</b>");
                        st.Append("</td>");
                    }
                    st.Append("<td colspan=1>  ");
                    st.Append(Resources.labels.password + " :");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append("<b>" + accountMBATable.Rows[0]["PASS"].ToString() == "" ? "******" : DecryptPass(accountMBATable.Rows[0]["PASS"].ToString()) + "</b>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    //st.Append("<tr>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append(Resources.labels.pincode + " :");
                    //st.Append("</td>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append("<b>" + accountMBATable.Rows[0]["PINCODE"].ToString() == "" ? "******" : SmartPortal.Security.Encryption.Decrypt(accountMBATable.Rows[0]["PINCODE"].ToString()) + "</b>");
                    //st.Append("</td>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append("</td>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append("</td>");
                    //st.Append("</tr>");
                }
                #endregion

                #region MB
                //lay het các tai khoan MB cua user theo userID
                DataSet accountMBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), string.Empty, type, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                DataTable accountMBTable = accountMBDataset.Tables[0];
                if (accountMBTable.Rows.Count != 0 || pendingforapprove)
                {
                    //if (accountMBTable.Rows[0]["ROLEID"].ToString().Trim() != "" || pendingforapprove)
                    //{
                    authenType = accountMBTable.Rows[0]["AUTHENTYPE"].ToString();
                    loginMethod = accountMBTable.Rows[0]["LOGINMETHOD"].ToString();
                    UserNameMB = accountMBTable.Rows[0]["USERNAME"].ToString();
                    PhoneNoMB = accountMBTable.Rows[0]["UN"].ToString();
                    if (!accountMBTable.Rows[0]["PASS"].ToString().Equals(""))
                    {
                        PassMB = DecryptPass(accountMBTable.Rows[0]["PASS"].ToString());
                    }

                    //}
                    //st.Append("<tr>");
                    //st.Append("<td colspan='4'>");
                    //st.Append("<br/>");
                    //st.Append("</td>");
                    //st.Append("</tr>");

                    //st.Append("<tr>");
                    //st.Append("<td colspan='4'>");
                    //st.Append("<B>  MOBILE</B><hr/>");
                    //st.Append("</td>");
                    //st.Append("</tr>");

                    //st.Append("<tr>");
                    //if (loginMethod.Equals("PHONENO"))
                    //{
                    //    st.Append("<td colspan=1>  ");
                    //    st.Append(Resources.labels.phone + " :");
                    //    st.Append("</td>");
                    //    st.Append("<td colspan=1>  ");
                    //    st.Append("<b>" + accountMBTable.Rows[0]["UN"].ToString() + "</b>");
                    //    st.Append("</td>");
                    //}
                    //else
                    //{
                    //    st.Append("<td colspan=1>  ");
                    //    st.Append(Resources.labels.username + " :");
                    //    st.Append("</td>");
                    //    st.Append("<td colspan=1>  ");
                    //    st.Append("<b>" + accountMBTable.Rows[0]["USERNAME"].ToString() + "</b>");
                    //    st.Append("</td>");
                    //}
                    //st.Append("<td colspan=1>  ");
                    //st.Append(Resources.labels.password + " :");
                    //st.Append("</td>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append("<b>" + accountMBTable.Rows[0]["PASS"].ToString() == "" ? "******" : DecryptPass(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                    //st.Append("</td>");
                    //st.Append("</tr>");

                }


                DataSet accountCardDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID("CAR", row["USERID"].ToString().Trim(), SmartPortal.Constant.IPC.NORMAL, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                DataTable accountCardTable = accountCardDataset.Tables[0];
                if (accountCardTable.Rows.Count != 0)
                {
                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<br/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<B>  LINKED CARD</B><hr/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    foreach (DataRow drcard in accountCardTable.Rows)
                    {
                        SmartPortal.Common.Log.WriteLogFile("", "", "", "==CardNo" + "//" + drcard["CardNo"].ToString());
                        SmartPortal.Common.Log.WriteLogFile("", "", "", "==HolderName" + "//" + drcard["HolderName"].ToString());
                        st.Append("<tr>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.sothe + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + SmartPortal.Common.Utilities.Utility.MaskDigits(drcard["CardNo"].ToString()) + "</b>");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.tendaydu + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + drcard["HolderName"].ToString() + "</b>");
                        st.Append("</td>");
                        st.Append("</tr>");
                    }
                }
                #endregion
                st.Append("</table>");
                userInfo += st.ToString();
                tmplAttachment.RemoveAttribute("USERINFO");
                if (contractTable.Rows[0]["contracttype"].ToString().Trim().Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE)
                    && row["typeid"].ToString().Trim().Equals(SmartPortal.Constant.IPC.CHUTAIKHOAN))
                {
                    tmplAttachment.SetAttribute("USERINFO", userInfo);
                }
                else
                {
                    tmplAttachment.SetAttribute("USERINFO", st.ToString());
                }

                //tmplAttachment.SetAttribute("USERINFO", st.ToString());
                string strAttachment = "";
                string strBody = "";
                string strAttachmentFileName = "Contract Information";
                string strEmailFrom = System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"];
                string strEmailTo = row["EMAIL"].ToString();
                string strSubject = System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"];
                strBody = tmplBody.ToString();

                strAttachment += tmplAttachment.ToString();
                strAttachment += "<div><br/><p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'>www.scb.co.th/en/personal-banking.html</a></p><span style='font-weight:bold;'>" + Resources.labels.AbankthankyouforusingAYAebankingservices + "!	</span></div>";
                PhoneNo = PhoneNoMB;
                SmartPortal.Common.Log.WriteLogFile("SEND MAIL APPROVE CONTRACT: " + contractNo, "", "", "PHONENUMBER: " + PhoneNoMB);
                //send mail


                //load send SMS only 

                string errorcode = "";
                string errorDesc = "";

                //24/7/2015 minh add to send sms contract
                contracttype = string.Empty;
                bool isowner = false;
                bool iscoowner = false;
                //17.9.2015 minh modify template sms message. this template generate from code
                string SENDINFO = string.Empty;
                string ibuserinfo = string.Empty;
                string mbuserinfo = string.Empty;
                string smsuserinfo = string.Empty;
                string contractinfo = string.Empty;
                contractinfo = "Contract info: Contractno." + contractNo;
                if (!IBuser.Equals(string.Empty) || !PhoneNo.Equals(string.Empty))
                {
                    if (!ConfigurationManager.AppSettings["IBMBSamePassword"].ToString().Equals("1"))
                    {
                        if (boolMB)
                        {
                            ibuserinfo = " /IB:User-" + IBuser + ",password-" + IBpass;
                            if (!UserNameMB.Equals(""))
                            {
                                if (loginMethod.Equals("PHONENO"))
                                {
                                    mbuserinfo = " /MB:PhoneNo-" + PhoneNoMB;
                                }
                                else
                                {
                                    mbuserinfo = " /MB:UserName-" + UserNameMB;
                                }
                            }
                            else
                            {
                                mbuserinfo = " /MB:PhoneNo-" + PhoneNoMB + ", password-" + PassMB;
                            }
                            if (!PassMB.Equals(""))
                            {
                                mbuserinfo += ", Password-" + IBpass;
                            }
                        }
                        else
                        {
                            ibuserinfo = " - IB:User-" + UserNameMB + ", password- ********,phoneno-********";
                        }

                    }
                    else
                    {
                        ibuserinfo = " - IB :User-" + UserNameMB + ", password-" + IBpass + ", phoneNo-" + PhoneNoMB;
                    }
                    SENDINFO = contractinfo + ibuserinfo + smsuserinfo + mbuserinfo;
                }
                contracttype = contractTable.Rows[0]["contracttype"].ToString().Trim();

                switch (contracttype)
                {
                    case SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL:
                    case SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT:
                        Sendnotify(ddlSendinfo, strEmailFrom, strEmailTo, strSubject, strBody, strAttachment, strAttachmentFileName, PhoneNo, SENDINFO, errorcode, errorDesc);
                        break;

                    case SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE:
                        //send info owner
                        switch (row["typeid"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.CHECKER:
                            case SmartPortal.Constant.IPC.MAKER:
                                Sendnotify(ddl_sim_owner, strEmailFrom, strEmailTo, strSubject, strBody, strAttachment, strAttachmentFileName, PhoneNo, SENDINFO, errorcode, errorDesc);
                                break;
                        }
                        break;
                    case SmartPortal.Constant.IPC.CONTRACTCORPADVANCE:
                        switch (row["typeid"].ToString().Trim())
                        {
                            case SmartPortal.Constant.IPC.CHECKER:
                                if (!bool.Parse(ConfigurationManager.AppSettings["sendnotifiAllwhenapprovefromeditcontract"].ToString()))
                                {
                                    if (lblSTS.Text.Equals(SmartPortal.Constant.IPC.PENDING))
                                    {
                                        Sendnotify(ddl_adv_owner, strEmailFrom, strEmailTo, strSubject, strBody, strAttachment, strAttachmentFileName, PhoneNo, SENDINFO, errorcode, errorDesc);
                                        break;
                                    }

                                }
                                Sendnotify(ddl_adv_owner, strEmailFrom, strEmailTo, strSubject, strBody, strAttachment, strAttachmentFileName, PhoneNo, SENDINFO, errorcode, errorDesc);
                                break;
                            case SmartPortal.Constant.IPC.ADMIN:
                                if (!bool.Parse(ConfigurationManager.AppSettings["sendnotifiAllwhenapprovefromeditcontract"].ToString()))
                                {
                                    if (lblSTS.Text.Equals(SmartPortal.Constant.IPC.PENDING))
                                        break;
                                }
                                Sendnotify(ddl_adv_admin, strEmailFrom, strEmailTo, strSubject, strBody, strAttachment, strAttachmentFileName, PhoneNo, SENDINFO, errorcode, errorDesc);
                                break;
                        }


                        break;
                        //N:
                        //    tmplAttachment.RemoveAttribute("USERINFO");
                        //    tmplAttachment.RemoveAttribute("NGUOIUYQUYEN");
                        //    tmplAttachment.RemoveAttribute("NUQ");
                        //    tmplAttachment.RemoveAttribute("LEVEL2");

                }
            }
            //        }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.WriteLogFile("SEND MAIL " + contractNo + " ERROR", "", "", "ERROR CONTENT: " + ex.ToString());
        }
    }
    protected void btApprove_Click(object sender, EventArgs e)
    {
        try
        {
            //approve
            string contractNo = "";
            if (Session["contractNo"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["contractNo"];

                contractNo = lstTran[0].Trim();
                //xử lý            
                if (contractNo.Equals(""))
                {
                    lblError.Text = "Approve not Successfully";
                    return;
                }
                if (lblSTS.Text == SmartPortal.Constant.IPC.NEW || lblSTS.Text == SmartPortal.Constant.IPC.PENDINGFORAPPROVE)
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMSCONTRACTAPPROVE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + contractNo + "'");

                    }
                    catch
                    {
                    }
                    #endregion
                    #region pincode
                    int pincodeLenght = int.Parse(ConfigurationManager.AppSettings["PINCODELENGHT"].ToString());

                    DataTable userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(contractNo, "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    #endregion
                    ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(contractNo, SmartPortal.Constant.IPC.ACTIVE, Session["userID"].ToString(), "Y", ref IPCERRORCODE, ref IPCERRORDESC);

                    if(hdusertype.Value == "C")
                    {
                        new SmartPortal.IB.Transfer().TransNonWalletToWallet(lbcustid.Text.Trim(), lbmobi.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                        if(IPCERRORCODE != "0")
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
                        }
                    }

                    if (IPCERRORCODE.Equals("0"))
                    {
                        SmartPortal.Common.Log.UpdateLogDatabase(contractNo, "Add new Account", Session["userName"].ToString());
                        SmartPortal.Common.Log.UpdateLogDatabase(contractNo, "Add new user", Session["userName"].ToString());
                        SendInfoLogin(contractNo);
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC.ToString();
                    }

                }
                else if (lblSTS.Text == SmartPortal.Constant.IPC.PENDINGFORDELETE)
                {
                    ApproveContractTable = new SmartPortal.SEMS.Contract().DeleteContract(contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
                    try
                    {
                        if (IPCERRORCODE.Equals("0"))
                        {
                            DataTable dtContract = new SmartPortal.SEMS.Contract().GetContractByContractNo(contractNo, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                            string userID = dtContract.Rows[0]["USERID"].ToString();
                            string stsContract = dtContract.Rows[0]["STATUS"].ToString();
                            SmartPortal.Common.Log.WriteLogDatabase(contractNo, userID, Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "EBA_Contract", "Delete Contract", "", "Deleted", stsContract, Session["userName"].ToString());

                            SmartPortal.Common.Log.WriteLogDatabase(contractNo, userID, Request.Url.ToString(), Session["userName"].ToString(),
                         Request.UserHostAddress, "EBA_Users", "Delete User", "", "Deleted", stsContract, Session["userName"].ToString());

                        }

                    }
                    catch
                    {

                    }
                }
                else
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMSCONTRACTAPPROVE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + contractNo + "'");
                    }
                    catch
                    {
                    }
                    #endregion

                    ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(contractNo, SmartPortal.Constant.IPC.ACTIVE, Session["userID"].ToString(), "N", ref IPCERRORCODE, ref IPCERRORDESC);
                    //vutran
                    if (IPCERRORCODE.Equals("0"))
                    {
                        if (lblSTS.Text == SmartPortal.Constant.IPC.PENDINGFORAPPROVE)
                        {
                            SmartPortal.Common.Log.UpdateLogDatabase(contractNo, "Link mobile banking", Session["userName"].ToString());

                        }
                        else
                        {
                            SmartPortal.Common.Log.UpdateLogDatabase(contractNo, "Add new Account", Session["userName"].ToString());
                            SmartPortal.Common.Log.UpdateLogDatabase(contractNo, "Add new user", Session["userName"].ToString());
                        }
                        SmartPortal.Common.Log.UpdateLogDatabase(contractNo, "Change contract of Agent merchant", Session["userName"].ToString(), "CHANGE");
                        SmartPortal.Common.Log.UpdateLogDatabase(contractNo, "Change Merchant Category", Session["userName"].ToString(), "CHANGE");
                        SendInfoLogin(contractNo);

                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC.ToString();
                    }
                }

                if (IPCERRORCODE == "0")
                {
                    //cập nhật lại List
                    lstTran.RemoveAt(0);
                    if (lstTran.Count != 0)
                    {
                        lblError.Text = Resources.labels.hopdong + " " + contractNo + " " + Resources.labels.duyetthanhcongmoibanduyethoptieptheo;
                    }
                    else
                    {
                        lblError.Text = Resources.labels.hopdong + " " + contractNo + " " + Resources.labels.duyetthanhcong;
                        goto REDI;
                    }


                }
                else
                {
                    lblError.Text = IPCERRORDESC.ToString();

                }

                if (lstTran.Count != 0)
                {
                    Session["contractNo"] = lstTran;
                    //chuyển qua giao dịch kế
                    BindData();
                }
                else
                {
                    Session["contractNo"] = null;
                    goto REDI;
                }

            }
            else
            {
                //approve khi duyệt 1 record
                if (lblSTS.Text == SmartPortal.Constant.IPC.NEW)
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMSCONTRACTAPPROVE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "'");
                    }
                    catch
                    {
                    }
                    #endregion

                    ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Constant.IPC.ACTIVE, Session["userID"].ToString(), "Y", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE.Equals("0"))
                    {

                        SmartPortal.Common.Log.UpdateLogDatabase(contractNo, "Add new Account", Session["userName"].ToString());
                        SmartPortal.Common.Log.UpdateLogDatabase(contractNo, "Add new user", Session["userName"].ToString());
                        SendInfoLogin(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString());

                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC.ToString();
                    }
                }
                else if (lblSTS.Text == SmartPortal.Constant.IPC.PENDINGFORDELETE)
                {
                    ApproveContractTable = new SmartPortal.SEMS.Contract().DeleteContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE.Equals("0"))
                    {
                        DataTable dtContract = new SmartPortal.SEMS.Contract().GetContractByContractNo(contractNo, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                        string userID = dtContract.Rows[0]["USERID"].ToString();
                        string stsContract = dtContract.Rows[0]["STATUS"].ToString();
                        SmartPortal.Common.Log.WriteLogDatabase(contractNo, userID, Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "EBA_Contract", "Delete Contract", "", "Deleted", stsContract, Session["userName"].ToString());
                        SmartPortal.Common.Log.WriteLogDatabase(contractNo, userID, Request.Url.ToString(), Session["userName"].ToString(),
                  Request.UserHostAddress, "EBA_Contract", "Delete User", "", "Deleted", stsContract, Session["userName"].ToString());
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC.ToString();
                    }
                }
                else
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMSCONTRACTAPPROVE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "'");
                    }
                    catch
                    {
                    }
                    #endregion

                    ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Constant.IPC.ACTIVE, Session["userID"].ToString(), "N", ref IPCERRORCODE, ref IPCERRORDESC);

                    if (lblSTS.Text == SmartPortal.Constant.IPC.PENDINGFORAPPROVE)
                    {
                        SmartPortal.Common.Log.UpdateLogDatabase(contractNo, "Link mobile banking", Session["userName"].ToString());
                    }
                    else
                    {
                        SmartPortal.Common.Log.UpdateLogDatabase(contractNo, "Add new Account", Session["userName"].ToString());
                        SmartPortal.Common.Log.UpdateLogDatabase(contractNo, "Add new user", Session["userName"].ToString());
                    }
                    SendInfoLogin(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString());


                    //vutran
                }

                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.hopdong + " " + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + Resources.labels.duyetthanhcong;

                    goto REDI;
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);

                }

                goto REDI;
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
    REDI:
        btApprove.Visible = false;
        btReject.Visible = false;
    EXIT:
        ;

    }

    private string StripHTML(string source)
    {
        try
        {
            string result;

            // Remove HTML Development formatting
            // Replace line breaks with space
            // because browsers inserts space
            result = source.Replace("\r", " ");
            // Replace line breaks with space
            // because browsers inserts space
            result = result.Replace("\n", " ");
            // Remove step-formatting
            result = result.Replace("\t", string.Empty);
            // Remove repeating spaces because browsers ignore them
            result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                  @"( )+", " ");

            // Remove the header (prepare first by clearing attributes)
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*head([^>])*>", "<head>",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"(<( )*(/)( )*head( )*>)", "</head>",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(<head>).*(</head>)", string.Empty,
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // remove all scripts (prepare first by clearing attributes)
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*script([^>])*>", "<script>",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"(<( )*(/)( )*script( )*>)", "</script>",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //result = System.Text.RegularExpressions.Regex.Replace(result,
            //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
            //         string.Empty,
            //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"(<script>).*(</script>)", string.Empty,
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // remove all styles (prepare first by clearing attributes)
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*style([^>])*>", "<style>",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"(<( )*(/)( )*style( )*>)", "</style>",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(<style>).*(</style>)", string.Empty,
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // insert tabs in spaces of <td> tags
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*td([^>])*>", "\t",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // insert line breaks in places of <BR> and <LI> tags
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*br( )*>", "\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*li( )*>", "\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // insert line paragraphs (double line breaks) in place
            // if <P>, <DIV> and <TR> tags
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*div([^>])*>", "\r\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*tr([^>])*>", "\r\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*p([^>])*>", "\r\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Remove remaining tags like <a>, links, images,
            // comments etc - anything that's enclosed inside < >
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<[^>]*>", string.Empty,
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // replace special characters:
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @" ", " ",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&bull;", " * ",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&lsaquo;", "<",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&rsaquo;", ">",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&trade;", "(tm)",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&frasl;", "/",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&lt;", "<",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&gt;", ">",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&copy;", "(c)",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&reg;", "(r)",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            // Remove all others. More can be added, see
            // http://hotwired.lycos.com/webmonkey/reference/special_characters/
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&(.{2,6});", string.Empty,
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // for testing
            //System.Text.RegularExpressions.Regex.Replace(result,
            //       this.txtRegex.Text,string.Empty,
            //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // make line breaking consistent
            result = result.Replace("\n", "\r");

            // Remove extra line breaks and tabs:
            // replace over 2 breaks with 2 and over 4 tabs with 4.
            // Prepare first to remove any whitespaces in between
            // the escaped characters and remove redundant tabs in between line breaks
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(\r)( )+(\r)", "\r\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(\t)( )+(\t)", "\t\t",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(\t)( )+(\r)", "\t\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(\r)( )+(\t)", "\r\t",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            // Remove redundant tabs
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(\r)(\t)+(\r)", "\r\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            // Remove multiple tabs following a line break with just one tab
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(\r)(\t)+", "\r\t",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            // Initial replacement target string for line breaks
            string breaks = "\r\r\r";
            // Initial replacement target string for tabs
            string tabs = "\t\t\t\t\t";
            for (int index = 0; index < result.Length; index++)
            {
                result = result.Replace(breaks, "\r\r");
                result = result.Replace(tabs, "\t\t\t\t");
                breaks = breaks + "\r";
                tabs = tabs + "\t";
            }

            // That's it.
            return result;
        }
        catch
        {
            //MessageBox.Show("Error");
            return source;
        }
    }
    protected void btReject_Click1(object sender, EventArgs e)
    {
        try
        {
            string des = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDescription.Text);
            string reasonName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlReason.SelectedValue.Trim());
            if (ddlReason.SelectedValue.Equals(""))
            {
                lblError.Text = "Reason name is not empty";
                return;
            }
            //approve
            string contractNo = "";

            if (Session["contractNo"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["contractNo"];

                contractNo = lstTran[0].Trim();

                //xử lý                
                if (lblSTS.Text == SmartPortal.Constant.IPC.NEW)
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMSCONTRACTAPPROVE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + contractNo + "'");
                    }
                    catch
                    {
                    }
                    #endregion

                    ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(contractNo, SmartPortal.Constant.IPC.REJECT, Session["userID"].ToString(), "Y", ref IPCERRORCODE, ref IPCERRORDESC);
                }
                else if (lblSTS.Text == SmartPortal.Constant.IPC.PENDINGFORDELETE)
                {
                    ApproveContractTable = new SmartPortal.SEMS.Contract().RejectPendingDelete(contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
                }
                else
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMSCONTRACTAPPROVE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + contractNo + "'");
                    }
                    catch
                    {
                    }
                    #endregion

                    ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(contractNo, SmartPortal.Constant.IPC.REJECT, Session["userID"].ToString(), "N", ref IPCERRORCODE, ref IPCERRORDESC);

                }
                if (IPCERRORCODE == "0")
                {
                    DataSet dsReject = new SmartPortal.SEMS.Contract().InsertRejectReason(reasonName, contractNo, des, "CONTRACT", ref IPCERRORCODE, ref IPCERRORDESC);
                }
                //ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(contractNo, SmartPortal.Constant.IPC.REJECT, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                //SendInfoLogin(contractNo);

                if (IPCERRORCODE == "0")
                {
                    lblError.Text = Resources.labels.hopdong + " " + contractNo + " reject successfully";

                    //cập nhật lại List
                    lstTran.RemoveAt(0);
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);

                }

                if (lstTran.Count != 0)
                {
                    txtDescription.Text = string.Empty;
                    Session["contractNo"] = lstTran;
                    //chuyển qua giao dịch kế
                    BindData();
                }
                else
                {
                    Session["contractNo"] = null;
                    ddlReason.Enabled = false;
                    txtDescription.Enabled = false;
                    goto REDI;
                }

            }
            else
            {
                //approve khi duyệt 1 record
                //ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Constant.IPC.REJECT, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                //SendInfoLogin(contractNo);
                //xử lý                
                if (lblSTS.Text == SmartPortal.Constant.IPC.NEW)
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMSCONTRACTAPPROVE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + contractNo + "'");
                    }
                    catch
                    {
                    }
                    #endregion

                    ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Constant.IPC.REJECT, Session["userID"].ToString(), "Y", ref IPCERRORCODE, ref IPCERRORDESC);

                    // SendInfoLogin(contractNo);
                }
                else if (lblSTS.Text == SmartPortal.Constant.IPC.PENDINGFORDELETE)
                {
                    ApproveContractTable = new SmartPortal.SEMS.Contract().RejectPendingDelete(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                }
                else
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMSCONTRACTAPPROVE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + contractNo + "'");
                    }
                    catch
                    {
                    }
                    #endregion

                    ApproveContractTable = new SmartPortal.SEMS.Contract().ApproveContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Constant.IPC.REJECT, Session["userID"].ToString(), "N", ref IPCERRORCODE, ref IPCERRORDESC);
                }
                if (IPCERRORCODE == "0")
                {
                    DataTable dtReject = new SmartPortal.SEMS.Contract().InsertRejectReason(ddlReason.SelectedValue, contractNo, txtDescription.Text, "CONTRACT", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.hopdong + " " + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + Resources.labels.khongduocduyet;
                    }

                    goto REDI;
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);

                }

                goto REDI;
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
    REDI:
        btApprove.Visible = false;
        btReject.Visible = false;
    EXIT:
        ;

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            RedirectBackToMainPage();
        }
        catch
        {

        }
    }
    private static void loadnotificationtodrodownlist(DropDownList ddl, bool gotemail, bool gotsms)
    {

        if (gotemail && gotsms)
        {
            ddl.Items.Add(new ListItem(Resources.labels.sendcontractinforsms, SmartPortal.Constant.IPC.SMS));
            ddl.Items.Add(new ListItem(Resources.labels.sendcontractinforemail, SmartPortal.Constant.IPC.EMAIL));
            ddl.Items.Add(new ListItem(Resources.labels.sendcontractinforboth, SmartPortal.Constant.IPC.BOTH));
        }
        else
                   if (gotemail && !gotsms)
        {
            ddl.Items.Add(new ListItem(Resources.labels.sendcontractinforemail, SmartPortal.Constant.IPC.EMAIL));
        }
        else
                       if (!gotemail && gotsms)
        {
            ddl.Items.Add(new ListItem(Resources.labels.sendcontractinforsms, SmartPortal.Constant.IPC.SMS));
        }
        else
        {
            //tr.Visible = false;
        }
    }
    private static void loadnotificationtodrodownlist_multiholder(DropDownList ddl, bool gotemail, bool gotsms)
    {
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem(Resources.labels.sendcontractinforemail, SmartPortal.Constant.IPC.EMAIL));
        ddl.Items.Add(new ListItem(Resources.labels.sendcontractinforsms, SmartPortal.Constant.IPC.SMS));
        ddl.Items.Add(new ListItem(Resources.labels.sendcontractinforboth, SmartPortal.Constant.IPC.BOTH));

    }
    private static bool UsergotSMS(string username)
    {
        try
        {


            //get sms for co owner
            DataSet accountSMSDataset_coowner = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, username, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
            DataTable accountSMSTable_coowner = accountSMSDataset_coowner.Tables[0];
            if (accountSMSTable_coowner.Rows.Count != 0)
            {
                if (accountSMSTable_coowner.Rows[0]["ROLEID"].ToString().Trim() != "")
                {
                    return true;

                }
            }
        }
        catch (Exception ex)
        {

        }
        return false;
    }
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
            //HyperLink hpEdit;
            //HyperLink hpDelete;

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

                //hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                //hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                lblType.Text = drv["LinkType"].ToString().Equals("OWN") ? "Own" : "Other";
                hpHolderCIF.Text = drv["CardHolderCFCode"].ToString();
                hpUID.Text = drv["CardHolderCFCode"].ToString();
                hpHolderCIF.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=4&p=1043&a=viewdetail&hcif=" + drv["CardHolderCFCode"].ToString() + "&ct=" + lblUT.Text.Trim() + "&cn=" + lbcontractno.Text + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));

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
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        lblStatus.Text = Resources.labels.pendingfordelete;
                        break;
                }

                //if (Session["branch"].ToString().Trim() == drv["BRANCHID_CT"].ToString().Trim())
                //{
                //    if (drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                //    {
                //        hpEdit.Text = "<img src='widgets/semscontractlist/images/stop.gif'/>";
                //        hpDelete.Text = "<img src='widgets/semscontractlist/images/stop.gif'/>";
                //    }
                //    else
                //    {
                //        switch (ACTION)
                //        {
                //            case "viewdetail":
                //                hpEdit.Text = "<img src='Widgets/Pages/view/images/icon_edit.gif'/>";

                //                hpDelete.Text = "<img src='Widgets/Pages/view/images/icon_delete.gif'/>";

                //                break;
                //            case "edit":
                //                hpEdit.Text = "<img src='Widgets/Pages/view/images/icon_edit.gif'/>";
                //                hpEdit.ToolTip = Resources.labels.edit;

                //                hpDelete.Text = "<img src='Widgets/Pages/view/images/icon_delete.gif'/>";
                //                hpDelete.ToolTip = Resources.labels.delete;
                //                break;
                //        }

                //    }
                //}
                //else
                //{
                //    hpEdit.Text = "<img src='widgets/semscontractlist/images/stop.gif'/>";

                //    hpDelete.Text = "<img src='widgets/semscontractlist/images/stop.gif'/>";
                //}
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "gvCard_RowDataBound", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "gvCard_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    private static void Sendnotify(DropDownList ddl, string strEmailFrom, string strEmailTo, string strSubject, string strBody, string strAttachment, string strAttachmentFileName, string PhoneNo, string SENDINFO, string errorcode, string errorDesc)
    {
        switch (ddl.SelectedValue)
        {
            case SmartPortal.Constant.IPC.EMAIL:
                SmartPortal.Common.EmailHelper.SendMailMessageAsync(strEmailFrom, strEmailTo, strSubject, strBody, strAttachment, strAttachmentFileName);
                SmartPortal.Common.Log.WriteLogFile("EMAIL LOG", "", "", "sent to " + strEmailTo);
                break;
            case SmartPortal.Constant.IPC.SMS:
                SmartPortal.SEMS.Contract.sendSMS_Contract(PhoneNo, SENDINFO, ref errorcode, ref errorDesc);
                //SmartPortal.SEMS.Contract.sendSMS_Contract(PhoneNo, contractNo, IBuser, IBpass, SMSphone, SMSdefaultAccount, SMSpincode, PHOuser, PHOpass, PHOpincode, ref errorcode, ref errorDesc);
                SmartPortal.Common.Log.WriteLogFile("SMS LOG", "", "", "sent to " + PhoneNo);
                break;
            default://send both sms and email
                SmartPortal.Common.EmailHelper.SendMailMessageAsync(strEmailFrom, strEmailTo, strSubject, strBody, strAttachment, strAttachmentFileName);
                SmartPortal.SEMS.Contract.sendSMS_Contract(PhoneNo, SENDINFO, ref errorcode, ref errorDesc);
                //SmartPortal.SEMS.Contract.sendSMS_Contract(PhoneNo, contractNo, IBuser, IBpass, SMSphone, SMSdefaultAccount, SMSpincode, PHOuser, PHOpass, PHOpincode, ref errorcode, ref errorDesc);
                SmartPortal.Common.Log.WriteLogFile("EMAIL+SMS LOG", "", "", "sent mail to " + strEmailTo + " sms send to " + PhoneNo);
                break;
        }
    }
    void SendInfoLoginCorpMatrix(string contractNo, DataTable contractTable)
    {
        string custID = "";
        string contracttype = "";
        string hpcontractNo = contractNo;

        DataSet dsUserType = new DataSet();
        dsUserType = new SmartPortal.SEMS.Services().GetAllUserType(contractTable.Rows[0]["USERTYPE"].ToString().Trim(), "", ref IPCERRORCODE, ref IPCERRORDESC);

        if (IPCERRORCODE != "0")
        {
            throw new IPCException(IPCERRORDESC);
        }
        DataTable dtUserType = dsUserType.Tables[0];

        //gan thong tin hop dong vao stringtemplate
        if (contractTable.Rows.Count != 0)
        {
            contracttype = contractTable.Rows[0]["contracttype"].ToString().Trim();
            custID = contractTable.Rows[0]["CUSTID"].ToString();

            switch (contractTable.Rows[0]["STATUS"].ToString().Trim())
            {
                case SmartPortal.Constant.IPC.NEW:
                    lbstatus.Text = Resources.labels.connew;
                    break;
                case SmartPortal.Constant.IPC.DELETE:
                    lbstatus.Text = Resources.labels.condelete;
                    break;
                case SmartPortal.Constant.IPC.ACTIVE:
                    lbstatus.Text = Resources.labels.conactive;
                    break;
                case SmartPortal.Constant.IPC.BLOCK:
                    lbstatus.Text = Resources.labels.conblock;
                    break;
                case SmartPortal.Constant.IPC.PENDING:
                    lbstatus.Text = Resources.labels.conpending;
                    break;
                case SmartPortal.Constant.IPC.REJECT:
                    lbstatus.Text = Resources.labels.conreject;
                    break;
                case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                    lbstatus.Text = Resources.labels.pendingfordelete;
                    break;

            }
        }

        //lay thong tin khach hang
        DataTable custTable = (new SmartPortal.SEMS.Customer().GetCustomerByCustcode(custID, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
        if (IPCERRORCODE != "0")
        {
            throw new IPCException(IPCERRORDESC);
        }

        //lay thong tin tai khoan

        DataTable userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo, "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
        if (IPCERRORCODE != "0")
        {
            throw new IPCException(IPCERRORDESC);
        }


        //gan thong tin user vao stringtemplate
        foreach (DataRow row in userTable.Rows)
        {

            SmartPortal.Common.Log.WriteLogFile("", "", "", "==start send info");
            Antlr3.ST.StringTemplate tmplAttachment = new Antlr3.ST.StringTemplate();
            tmplAttachment = SmartPortal.Common.ST.GetStringTemplate("SEMSContractApprove", "ContractAttachment" + System.Globalization.CultureInfo.CurrentCulture.ToString());

            Antlr3.ST.StringTemplate tmplBody = new Antlr3.ST.StringTemplate();
            tmplBody = SmartPortal.Common.ST.GetStringTemplate("SEMSContractApprove", "ContractApprove" + System.Globalization.CultureInfo.CurrentCulture.ToString());

            tmplAttachment.Reset();
            tmplBody.Reset();

            if (contractTable.Rows.Count != 0)
            {
                tmplAttachment.SetAttribute("CONTRACTNO", contractTable.Rows[0]["CONTRACTNO"].ToString());
                tmplBody.SetAttribute("CONTRACTNO", contractTable.Rows[0]["CONTRACTNO"].ToString());

                if (dtUserType.Rows.Count != 0)
                {
                    tmplAttachment.SetAttribute("CONTRACTTYPE", dtUserType.Rows[0]["TYPENAME"].ToString());
                }
                tmplAttachment.SetAttribute("OPENDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["CREATEDATE"].ToString()).ToString("dd/MM/yyyy"));
                tmplAttachment.SetAttribute("ENDDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["ENDDATE"].ToString()).ToString("dd/MM/yyyy"));

                tmplAttachment.SetAttribute("PRODUCT", contractTable.Rows[0]["PRODUCTNAME"].ToString());
            }
            //gan thong tin hop dong vao stringtemplate
            if (custTable.Rows.Count != 0)
            {
                tmplAttachment.SetAttribute("CUSTID", custTable.Rows[0]["CUSTID"].ToString());
                tmplAttachment.SetAttribute("FULLNAME", custTable.Rows[0]["FULLNAME"].ToString());
                switch (custTable.Rows[0]["CFTYPE"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.PERSONAL:
                        tmplAttachment.SetAttribute("CUSTTYPE", Resources.labels.canhan);
                        break;
                    case SmartPortal.Constant.IPC.CORPORATE:
                        tmplAttachment.SetAttribute("CUSTTYPE", Resources.labels.doanhnghiep);
                        break;
                }
                tmplAttachment.SetAttribute("PHONE", custTable.Rows[0]["TEL"].ToString());
                tmplAttachment.SetAttribute("EMAIL", custTable.Rows[0]["EMAIL"].ToString());
                tmplBody.SetAttribute("EMAIL", custTable.Rows[0]["EMAIL"].ToString());
                tmplAttachment.SetAttribute("CIF", custTable.Rows[0]["CUSTCODE"].ToString());
            }

            string IBuser = "";
            string IBpass = "";
            string MBuser = "";
            string MBpass = "";
            string MBPhone = "";
            string SMSphone = "";
            string SMSdefaultAccount = "";
            string SMSpincode = "";
            string PHOuser = "";
            string PHOpass = "";
            string PHOpincode = "";
            string PhoneNo = "";
            //edit by vutran 11102014
            //Kiem tra xem khach hang da dung IBMBSMS_Banking chua
            bool boolIB = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "IB", ref IPCERRORCODE, ref IPCERRORDESC);
            bool boolMB = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "MB", ref IPCERRORCODE, ref IPCERRORDESC);
            bool boolSMS = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "SMS", ref IPCERRORCODE, ref IPCERRORDESC);
            //if (boolIB || boolMB || boolSMS)
            //{

            //lay kieu nguoi dung
            string ut = Resources.labels.user;

            StringBuilder st = new StringBuilder();
            st.Append("<table style='width:100%;font-size:10px;'>");

            st.Append("<tr>");
            st.Append("<td style='background:silver;background-color: #C0C0C0; line-height:13px;' colspan=4>");
            st.Append("<b>  ACCOUNT INFORMATION</b>");
            st.Append("</td>");
            st.Append("</tr>");

            st.Append("<tr>");
            st.Append("<td witdth='15%' style='width:15%' colspan=1>  ");
            st.Append(Resources.labels.tendaydu + " :");
            st.Append("</td>");
            st.Append("<td witdth='35%' style='width:35%' colspan=1>  ");
            st.Append(row["FULLNAME"].ToString());
            st.Append("</td>");
            st.Append("<td witdth='15%' style='width:15%' colspan=1>  ");
            st.Append("Email :");
            st.Append("</td>");
            st.Append("<td witdth='35%'  style='width:35%' colspan=1>  ");
            st.Append(row["EMAIL"].ToString());
            st.Append("</td>");
            st.Append("</tr>");

            st.Append("<tr>");
            st.Append("<td colspan=1>  ");
            st.Append(Resources.labels.phone + " :");
            st.Append("</td>");
            st.Append("<td colspan=1>  ");
            st.Append(row["PHONE"].ToString());
            st.Append("</td>");
            st.Append("<td colspan=1>  ");
            st.Append(Resources.labels.kieunguoidung + " :");
            st.Append("</td>");
            st.Append("<td colspan=1>  ");
            st.Append(ut);
            st.Append("</td>");
            st.Append("</tr>");
            st.Append("<br><br>");

            //if (boolIB)
            //{
            //lay het các tai khoan Ibank cua user theo userID
            DataSet accountIBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            DataTable accountIBTable = accountIBDataset.Tables[0];
            if (accountIBTable.Rows.Count != 0 && !string.IsNullOrEmpty(accountIBTable.Rows[0][0].ToString()))
            {
                if (accountIBTable.Rows[0]["ROLEID"].ToString().Trim() != "")
                {
                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<B>  INTERNET BANKING</B><hr/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td colspan=1>  ");
                    st.Append(Resources.labels.username + " :");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append("<b>" + accountIBTable.Rows[0]["USERNAME"].ToString() + "</b>");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append(Resources.labels.password + " :");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append("<b>" + ((boolIB) ? SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString()) : "********") + "</b>");
                    st.Append("</td>");
                    st.Append("</tr>");
                    if (boolIB)
                    {
                        IBuser = accountIBTable.Rows[0]["USERNAME"].ToString();
                        IBpass = SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString());
                    }
                }
            }
            //}
            //if (boolSMS)
            //{
            //lay het các tai khoan SMS cua user theo userID
            DataSet accountSMSDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            DataTable accountSMSTable = accountSMSDataset.Tables[0];
            if (accountSMSTable.Rows.Count != 0)
            {
                if (accountSMSTable.Rows.Count != 0 && !string.IsNullOrEmpty(accountSMSTable.Rows[0][0].ToString()))
                {
                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<br/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<B>  SMS</B><hr/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td colspan=1>  ");
                    st.Append(Resources.labels.phone + " :");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append("<b>" + accountSMSTable.Rows[0]["UN"].ToString() + "</b>");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append(Resources.labels.taikhoanmacdinh + " :");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append("<b>" + accountSMSTable.Rows[0]["DEFAULTACCTNO"].ToString() + "</b>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td colspan=1>  ");
                    st.Append(Resources.labels.mapin + " :");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountSMSTable.Rows[0]["PINCODE"].ToString()) + "</b>");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append("");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append("");
                    st.Append("</td>");
                    st.Append("</tr>");
                    if (boolSMS)
                    {
                        SMSphone = accountSMSTable.Rows[0]["UN"].ToString();
                        SMSdefaultAccount = accountSMSTable.Rows[0]["DEFAULTACCTNO"].ToString();
                        //SMSpincode = accountSMSTable.Rows[0]["PINCODE"].ToString();
                        SMSpincode = SmartPortal.Security.Encryption.Decrypt(accountSMSTable.Rows[0]["PINCODE"].ToString());
                    }

                }
            }
            //}
            //if (boolMB)
            //{
            //lay het các tai khoan MB cua user theo userID
            DataSet accountMBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            DataTable accountMBTable = accountMBDataset.Tables[0];
            if (accountMBTable.Rows.Count != 0)
            {
                string pass = accountMBTable.Rows[0]["PASS"].ToString() == "" ? "******" : DecryptPass(accountMBTable.Rows[0]["PASS"].ToString());
                MBPhone = accountMBTable.Rows[0]["UN"].ToString();
                MBuser = accountMBTable.Rows[0]["USERNAME"].ToString();
                MBpass = SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString());
                PHOpincode = accountMBTable.Rows[0]["PINCODE"].ToString();
                //st.Append("<tr>");
                //st.Append("<td colspan='4'>");
                //st.Append("<br/>");
                //st.Append("</td>");
                //st.Append("</tr>");

                //st.Append("<tr>");
                //st.Append("<td colspan='4'>");
                //st.Append("<B>  MOBILE</B><hr/>");
                //st.Append("</td>");
                //st.Append("</tr>");

                //st.Append("<tr>");
                //st.Append("<td colspan=1>  ");
                //st.Append(Resources.labels.username + " :");
                //st.Append("</td>");
                //st.Append("<td colspan=1>  ");
                //st.Append("<b>" + accountMBTable.Rows[0]["USERNAME"].ToString() + "</b>");
                //st.Append("</td>");

                //st.Append("<td colspan=1>  ");
                //st.Append(Resources.labels.password + " :");
                //st.Append("</td>");
                //st.Append("<td colspan=1>");
                //st.Append("<b> " + pass + "</b>");
                //st.Append("</td>");
                //st.Append("</tr>");
                //if (boolMB)
                //{

                //}
            }

            DataSet accountCardDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID("CAR", row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            DataTable accountCardTable = accountCardDataset.Tables[0];
            if (accountCardTable.Rows.Count != 0)
            {
                st.Append("<tr>");
                st.Append("<td colspan='4'>");
                st.Append("<br/>");
                st.Append("</td>");
                st.Append("</tr>");

                st.Append("<tr>");
                st.Append("<td colspan='4'>");
                st.Append("<B>  LINKED CARD</B><hr/>");
                st.Append("</td>");
                st.Append("</tr>");

                foreach (DataRow drcard in accountCardTable.Rows)
                {
                    SmartPortal.Common.Log.WriteLogFile("", "", "", "==CardNo" + "//" + drcard["CardNo"].ToString());
                    SmartPortal.Common.Log.WriteLogFile("", "", "", "==HolderName" + "//" + drcard["HolderName"].ToString());
                    st.Append("<tr>");
                    st.Append("<td colspan=1>  ");
                    st.Append(Resources.labels.sothe + " :");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append("<b>" + SmartPortal.Common.Utilities.Utility.MaskDigits(drcard["CardNo"].ToString()) + "</b>");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append(Resources.labels.tendaydu + " :");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append("<b>" + drcard["HolderName"].ToString() + "</b>");
                    st.Append("</td>");
                    st.Append("</tr>");

                }
            }
            st.Append("</table>");

            tmplAttachment.SetAttribute("USERINFO", st.ToString());
            string strAttachment = "";
            string strBody = "";
            string strAttachmentFileName = "Contract Information";
            string strEmailFrom = System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"];
            string strEmailTo = row["EMAIL"].ToString();
            string strSubject = System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"];
            strBody = tmplBody.ToString();
            strAttachment += "<div><h1>" + Resources.labels.nganhangphuongnam + "<br/></h1><br/></div>";
            strAttachment += tmplAttachment.ToString();
            strAttachment += "<div><br/><p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'>www.scb.co.th/en/personal-banking.html</a></p><span style='font-weight:bold;'>" + Resources.labels.AbankthankyouforusingAYAebankingservices + "!	</span></div>";
            PhoneNo = MBPhone;


            string errorcode = "";
            string errorDesc = "";


            //17.9.2015 minh modify template sms message. this template generate from code
            string SENDINFO = string.Empty;
            string ibuserinfo = string.Empty;
            string smsuserinfo = string.Empty;
            string contractinfo = string.Empty;
            contractinfo = "Contract info: Contractno." + contractNo;
            if (!IBuser.Equals(string.Empty))
            {
                ibuserinfo = " - IB :User-" + IBuser + ", password-" + IBpass;
            }
            else if (boolMB)
            {
                ibuserinfo = " - IB :User-" + MBuser + ", password-" + IBpass + ", phoneno-" + PhoneNo;
            }
            else
            {
                ibuserinfo = " - IB :User-" + MBuser + ", password- ********, phoneno-********";
            }
            SENDINFO = contractinfo + ibuserinfo + smsuserinfo;
            Sendnotify(ddl_matrix, strEmailFrom, strEmailTo, strSubject, strBody, strAttachment, strAttachmentFileName, PhoneNo, SENDINFO, errorcode, errorDesc);

        }

    }
    public bool sendPincode()
    {
        try
        {
            string result = new SmartPortal.SEMS.User().CheckSendPincodeSysvar(ref IPCERRORCODE, ref IPCERRORDESC);
            if (result.Equals("1"))
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
        return false;
    }
    private string DecryptPass(string values)
    {
        if (values.Equals(""))
        {
            return "******";
        }
        else
        {
            return SmartPortal.Security.Encryption.Decrypt(values);
        }
    }
    private void LoadComboboxReason()
    {

        try
        {
            if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE) ||
                CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX) ||
                CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE))
            {
                DataTable dtReason = new SmartPortal.SEMS.Contract().GetReason("", "", "", "CCO", "CO", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlReason.DataSource = dtReason;
                ddlReason.DataTextField = "REASONNAME";
                ddlReason.DataValueField = "REASONID";
                ddlReason.DataBind();
            }
            else
            {
                DataTable dtReason = new SmartPortal.SEMS.Contract().GetReason("", "", "", CONTRACTTYPE, "CO", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlReason.DataSource = dtReason;
                ddlReason.DataTextField = "REASONNAME";
                ddlReason.DataValueField = "REASONID";
                ddlReason.DataBind();
            }

        }
        catch (Exception ex)
        {

        }
    }
    private string LoadSubUserType(string SubUserType)
    {
        DataTable dsUserType = new SmartPortal.SEMS.Services().GetUserType(SubUserType, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        if (IPCERRORCODE.Equals("0"))
        {
            lblSubUserType.Text = dsUserType.Rows[0]["SUBUSERTYPE"].ToString();
            hdsubuertype.Value = dsUserType.Rows[0]["SUBUSERCODE"].ToString();
            return dsUserType.Rows[0]["TYPE"].ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    public DataTable TEST(string acctno, string userid, string fromdate,string todate)
    {
        DataTable iRead;

        SqlParameter p1 = new SqlParameter();
        p1.ParameterName = "@ACCTNO";
        p1.Value = acctno;
        p1.SqlDbType = SqlDbType.VarChar;

        SqlParameter p2 = new SqlParameter();
        p2.ParameterName = "@USERID";
        p2.Value = userid;
        p2.SqlDbType = SqlDbType.VarChar;

        SqlParameter p3 = new SqlParameter();
        p3.ParameterName = "@FROMDATE";
        p3.Value = fromdate;
        p3.SqlDbType = SqlDbType.VarChar;


        SqlParameter p4 = new SqlParameter();
        p4.ParameterName = "@TODATE";
        p4.Value = todate;
        p4.SqlDbType = SqlDbType.VarChar;

        iRead = DataAccess.GetFromDataTable("IPC_GETRESULT_RQSTM", p1, p2, p3,p4);

        return iRead;
    }



    protected void gvlistacc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblaccnumber;
            Label lblccyid;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                lblaccnumber = (Label)e.Row.FindControl("lblaccnumber");
                lblccyid = (Label)e.Row.FindControl("lblccyid");

                lblaccnumber.Text = drv["ACCNUMBER"].ToString();
                lblccyid.Text = drv["CURRENCY"].ToString();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_Controls_Widget", "gvlistacc", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Controls_Widget", "gvlistacc", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
}
