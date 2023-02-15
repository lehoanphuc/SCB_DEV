using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.Common.Utilities;
using SmartPortal.Security;
using SmartPortal;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System.Linq;

public partial class Widgets_SEMSCard_Controls_Widget : WidgetBase
{

    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    DataTable userTable = new DataTable();
    DataTable userTableTemp = new DataTable();
    DataTable tblRoleDefault = new DataTable();
    string contractNo = "";
    string userName = "";
    string userType = "";
    string productType = "";
    string sPageListContract = "141";


    protected void Page_Init(object sender, EventArgs e)
    {
        TabCustomerInfoHelper.LoadConfig();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            ltrError.Text = "";
            lblAlert.Text = "";

            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
            contractNo = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim();
            if (!IsPostBack)
            {
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"] != null)
                {
                    //get contractno
                    //userTableTemp = (new SmartPortal.SEMS.Contract().GetUserByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    DataTable contractTableTemp = (new SmartPortal.SEMS.Contract().GetCustomerByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (contractTableTemp.Rows.Count != 0)
                    {
                        txtOwnContractNo.Text = contractNo;
                        txtOwnCustCode.Text = contractTableTemp.Rows[0]["CFCODE"].ToString();
                        txtOwnFullName.Text = contractTableTemp.Rows[0]["FULLNAME"].ToString();
                        txtOwnEmail.Text = contractTableTemp.Rows[0]["EMAIL"].ToString();
                        txtOwnEmail.Text = contractTableTemp.Rows[0]["EMAIL"].ToString();
                        txtOwnDOB.Text = contractTableTemp.Rows[0]["DOB"].ToString();
                        try
                        {
                            ddlOwnSex.SelectedValue = contractTableTemp.Rows[0]["SEX"].ToString();
                        }
                        catch { }

                        txtOwnPhone.Text = contractTableTemp.Rows[0]["TEL"].ToString();
                        txtOwnLic.Text = contractTableTemp.Rows[0]["LICENSEID"].ToString();
                        txtOwnAddress.Text = contractTableTemp.Rows[0]["ADDR_RESIDENT"].ToString();
                    }

                    DataTable userTableTemp = (new SmartPortal.SEMS.Contract().GetUserByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    DataTable userTableClone = new DataView(userTableTemp).ToTable(true, "UserID", "FullName", "Email", "Phone", "Status");
                    DataColumn colCheckbox = userTableClone.Columns.Add("colCheckbox", typeof(bool));
                    foreach (DataRow dr in userTableClone.Rows)
                    {
                        dr["colCheckbox"] = true;
                    }
                    if (userTableTemp.Select("STATUS <> 'D'").Count() > 0)
                    {
                        gvUserRegister.DataSource = userTableTemp.Select("STATUS <> 'D'").CopyToDataTable();
                    }

                    gvUserRegister.DataBind();
                }
                else
                {
                }

                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"] == null)
                {
                    DataSet ContrNoDS = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException("13");
                    }
                    DataTable ContrNoDT = new DataTable();
                    ContrNoDT = ContrNoDS.Tables[0];
                    if (ContrNoDT.Rows.Count != 0)
                    {
                        contractNo = ContrNoDT.Rows[0]["CONTRACTNO"].ToString();
                    }
                }
                else
                {
                    // contractNo = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString();
                    // minh 26/7/2015 add phan nay cho giong voi create contract
                    //get contractno

                    userTableTemp = (new SmartPortal.SEMS.Contract().GetUserByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (userTableTemp.Rows.Count != 0)
                    {
                        contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();

                        userName = SmartPortal.Common.Utilities.Utility.GetID(userTableTemp.Rows[0]["FULLNAME"].ToString().Trim(), userTableTemp.Rows[0]["CUSTCODE"].ToString().Trim(), userTableTemp.Rows[0]["LICENSEID"].ToString().Trim());
                        if (int.Parse(ConfigurationManager.AppSettings["IBMBSameUser"].ToString()) == 1)
                        {
                            //txtMBPhoneNo.Text = userName;
                        }
                        else
                        {

                            //txtMBPhoneNo.Text = SmartPortal.Common.Utilities.Utility.GetID("", userTableTemp.Rows[0]["CUSTCODE"].ToString().Trim(), "", 10) + "1";
                        }
                        userType = userTableTemp.Rows[0]["USERTYPE"].ToString();
                        productType = userTableTemp.Rows[0]["PRODUCTID"].ToString();
                    }
                    else
                    {
                        userName = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
                    }
                }


                //DataSet dsLevel = new SmartPortal.IB.CorpUser().LoadCorpUserlevelByContractNo(contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
                //if (IPCERRORCODE == "0")
                //{
                //    ddlUserLevel.DataSource = dsLevel;
                //    ddlUserLevel.DataTextField = "DESCRIPTION";
                //    ddlUserLevel.DataValueField = "USERLEVEL";
                //    ddlUserLevel.DataBind();
                //}
                //else
                //{
                //    throw new Exception();
                //}

                if (ViewState["NGUOIQUANTRI"] == null)
                {
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

    void LoadDataInTreeview1(string serviceID, TreeView tvPage, string userType)
    {
        tvPage.Nodes.Clear();
        DataTable tblSS = new DataTable();
        //DataSet ds=new SmartPortal.IB.Role().GetRoleByServiceID(serviceID, cn, ref IPCERRORCODE, ref IPCERRORDESC);
        //tblSS = new SmartPortal.SEMS.Contract().GetRoleByServiceAndContractForCard(serviceID, cn);

        tblSS = new SmartPortal.SEMS.Role().GetByServiceAndUserTypeForCard(serviceID, userType, productType);
        //tblSS = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(serviceID, ddlProduct.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

        foreach (DataRow row in tblSS.Rows)
        {
            TreeNode node = new TreeNode(row["ROLENAME"].ToString(), row["ROLEID"].ToString());
            node.ShowCheckBox = true;
            node.Checked = true;

            DataSet dsTransaction = new DataSet();
            DataTable tblPage = new DataTable();
            dsTransaction = new SmartPortal.SEMS.Role().GetTranOfRole(row["ROLEID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                tblPage = dsTransaction.Tables[0];
            }
            foreach (DataRow row1 in tblPage.Rows)
            {
                TreeNode node1 = new TreeNode(row1["PAGENAME"].ToString(), row1["TRANCODE"].ToString());
                node1.ShowCheckBox = false;
                node1.ToolTip = row1["PageDescription"].ToString();
                //node1.SelectAction = TreeNodeSelectAction.None;

                node.ChildNodes.Add(node1);
            }
            if (tblPage.Rows.Count != 0)
            {
                node.CollapseAll();
                tvPage.Nodes.Add(node);
            }
        }
        tvPage.Attributes.Add("onclick", "OnTreeClick(event)");
    }
    protected void btnBackHome_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Encrypt.DecryptData("/Default.aspx?p=" + sPageListContract)));
    }

    protected void gvUserRegister_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox cbCheckBoxHd = new CheckBox();
                cbCheckBoxHd.ID = "cbxSelectAll";
                cbCheckBoxHd.Attributes.Add("onclick", "gvUserRegister_CheckAll(this);");
                e.Row.Cells[0].Controls.Add(cbCheckBoxHd);
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

                ((Label)e.Row.FindControl("lblUserID")).Text = drv["UserId"].ToString();
                ((Label)e.Row.FindControl("lblFullName")).Text = drv["FullName"].ToString();

                ((Label)e.Row.FindControl("lblEmail")).Text = drv["Email"].ToString();

                ((Label)e.Row.FindControl("lblPhone")).Text = drv["Phone"].ToString();
                ((Label)e.Row.FindControl("lblStatus")).Text = mappingUserStatus(drv["Status"].ToString().Trim(), true);
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_SEMSCard_View_Widget", "gvUserRegister_RowDataBound", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCard_View_Widget", "gvUserRegister_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvCardRegister_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox cbCheckBoxHd = new CheckBox();
                cbCheckBoxHd.ID = "cbxSelectAllCard";
                cbCheckBoxHd.Attributes.Add("onclick", "gvCardRegister_CheckAll(this);");
                e.Row.Cells[0].Controls.Add(cbCheckBoxHd);
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

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                ((Label)e.Row.FindControl("lblCardNo")).Text = drv["cardNo"].ToString();
                ((Label)e.Row.FindControl("lblFullName")).Text = drv["cardholderName"].ToString();
                ((Label)e.Row.FindControl("lblCCYID")).Text = (!drv.Row.Table.Columns.Contains("cardCurrency")) ? SmartPortal.Common.Utilities.Utility.MappingCurrency("104") : SmartPortal.Common.Utilities.Utility.MappingCurrency(drv["cardCurrency"].ToString());
                ((Label)e.Row.FindControl("lblCardType")).Text = mappingCardType(drv["cardType"].ToString().Trim(), true);
                ((Label)e.Row.FindControl("lblStatus")).Text = mappingCardStatus(drv["cardStatus"].ToString().Trim(), true);
            }
        }
        catch (SQLException sex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sqlec"], "Widgets_SEMSCard_View_Widget", "gvCardRegister_RowDataBound", sex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sqlec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCard_View_Widget", "gvCardRegister_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    string mappingUserStatus(string input, bool In2Out)
    {
        string output = "Unknown";
        try
        {
            Dictionary<string, string> stsList = new Dictionary<string, string>
            {
                { SmartPortal.Constant.IPC.NEW,Resources.labels.connew },
                { SmartPortal.Constant.IPC.ACTIVE,Resources.labels.conactive },
                { SmartPortal.Constant.IPC.DELETE,Resources.labels.condelete },
                { SmartPortal.Constant.IPC.BLOCK,Resources.labels.conblock },
                { SmartPortal.Constant.IPC.PENDING,Resources.labels.conpending },
                { SmartPortal.Constant.IPC.REJECT,Resources.labels.conreject },
                { SmartPortal.Constant.IPC.PENDINGFORDELETE,Resources.labels.pendingfordelete }
            };
            return In2Out ? stsList[input.Trim()] : stsList.FirstOrDefault(x => x.Value == input.Trim()).Key;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCard_View_Widget", "mappingCarType", ex.ToString(), Request.Url.Query);
        }
        return output;
    }
    string mappingCardType(string input, bool In2Out)
    {
        string output = "Unknown";
        try
        {
            Dictionary<string, string> stsList = new Dictionary<string, string>
            {
               // { "CR","Credit" },
                { "C","Credit" },
                { "DE","Debit" }
            };
            return In2Out ? stsList[input.Trim()] : stsList.FirstOrDefault(x => x.Value == input.Trim()).Key;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCard_View_Widget", "mappingCardType", ex.ToString(), Request.Url.Query);
        }
        return output;
    }
    string mappingCardStatus(string input, bool In2Out)
    {
        string output = "Unknown";
        try
        {
            Dictionary<string, string> stsList = new Dictionary<string, string>
            {
                { "T","Terminated" },
                { "V","Voluntary Closed" },
                { "A","Active" },
                { "B","Block" },
                { "L","Lost" },
                { "S","Stolen" },
                { "U","Unactived" },
                { "X","Counterfeit" },
                { "D","Destroy" },
                { "R","Restricted Card" },
            };
            return In2Out ? stsList[input.Trim()] : stsList.FirstOrDefault(x => x.Value == input.Trim()).Key;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCard_View_Widget", "mappingCarType", ex.ToString(), Request.Url.Query);
        }
        return output;
    }
    void BindData()
    {

        //enable(disable) theo action
        switch (ACTION)
        {
            case SmartPortal.Constant.IPC.VIEWDETAIL:
                txtCardHolderDOB.Enabled = false;
                txtCardHolderFullName.Enabled = false;
                txtCardHolderPhone.Enabled = false;
                txtCardHolderEmail.Enabled = false;
                ddlCardHolderSex.Enabled = false;
                txtCardHolderAddress.Enabled = false;
                btsaveandcont.Visible = false;
                btnThemNQT.Visible = false;
                btnHuy.Visible = false;
                tvIBQT.Enabled = false;
                tvMBQT.Enabled = false;
                gvCardRegister.Enabled = false;
                gvUserRegister.Enabled = false;
                txtCardHolderCustCode.Enabled = false;
                btnCardHolderDetail.Visible = false;

                break;
            case SmartPortal.Constant.IPC.EDIT:
                txtCardHolderDOB.Enabled = true;
                txtCardHolderFullName.Enabled = false;
                txtCardHolderPhone.Enabled = false;
                txtCardHolderEmail.Enabled = false;
                ddlCardHolderSex.Enabled = false;
                txtCardHolderAddress.Enabled = false;
                btsaveandcont.Visible = true;
                gvCardRegister.Enabled = true;
                gvUserRegister.Enabled = true;
                txtCardHolderCustCode.Enabled = false;
                btnCardHolderDetail.Visible = false;
                break;
            default:
                break;

        }

        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["hcif"] != null)
        {
            //lay thong tin chu the
            txtCardHolderCustCode.Text = hdfCardHolderCustCode.Value = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["hcif"].Trim();
            btnCardHolderDetail_Click(null, null);

            #region lấy thông tin tất cả các quyền
            DataTable tblRoleDetail = new SmartPortal.SEMS.Card().GetCardRoldeDetailByContractNoandCIF(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["hcif"].ToString());
            //vutt 16032017
            ViewState["ROLEDETAIL"] = tblRoleDetail;
            foreach (DataRow dr in tblRoleDetail.Rows)
            {
                dr["CardNo"] = SmartPortal.Control.Security.DecryptCard(dr["CardNo"].ToString());
            }

            ////luu thông tin vào session và hiển thị lên lưới
            #region khoi tao bang
            DataTable tblNguoiUyQuyen = new DataTable();

            DataColumn colContractNo = new DataColumn("colContractNo");
            DataColumn colCardNo = new DataColumn("colCardNo");
            DataColumn colUserID = new DataColumn("colUserID");
            DataColumn colUserName = new DataColumn("colUserName");
            DataColumn colCardType = new DataColumn("colCardType");
            DataColumn colCCYID = new DataColumn("colCCYID");
            DataColumn colCardStatus = new DataColumn("colCardStatus");
            DataColumn colBranchID = new DataColumn("colBranchID");
            DataColumn colLinkType = new DataColumn("colLinkType");
            DataColumn colRightType = new DataColumn("colRightType");
            DataColumn colHolderCFCode = new DataColumn("colHolderCFCode");
            DataColumn colHolderName = new DataColumn("colHolderName");
            DataColumn colRole = new DataColumn("colRole");
            DataColumn colRoleID = new DataColumn("colRoleID");
            DataColumn colTranCode = new DataColumn("colTranCode");
            DataColumn colTranCodeID = new DataColumn("colTranCodeID");
            DataColumn colServiceID = new DataColumn("colServiceID");

            tblNguoiUyQuyen.Columns.Add(colContractNo);
            tblNguoiUyQuyen.Columns.Add(colCardNo);
            tblNguoiUyQuyen.Columns.Add(colUserID);
            tblNguoiUyQuyen.Columns.Add(colUserName);
            tblNguoiUyQuyen.Columns.Add(colCardType);
            tblNguoiUyQuyen.Columns.Add(colCCYID);
            tblNguoiUyQuyen.Columns.Add(colCardStatus);
            tblNguoiUyQuyen.Columns.Add(colBranchID);
            tblNguoiUyQuyen.Columns.Add(colLinkType);
            tblNguoiUyQuyen.Columns.Add(colRightType);
            tblNguoiUyQuyen.Columns.Add(colHolderCFCode);
            tblNguoiUyQuyen.Columns.Add(colHolderName);
            tblNguoiUyQuyen.Columns.Add(colRole);
            tblNguoiUyQuyen.Columns.Add(colRoleID);
            tblNguoiUyQuyen.Columns.Add(colTranCode);
            tblNguoiUyQuyen.Columns.Add(colTranCodeID);
            tblNguoiUyQuyen.Columns.Add(colServiceID);

            #endregion

            #region get card reg data from view
            DataTable dtCardReg = new DataTable();
            dtCardReg.Columns.Add("colCheckbox", typeof(bool));
            dtCardReg.Columns.Add("cardNo", typeof(string));
            dtCardReg.Columns.Add("cardholderName", typeof(string));
            dtCardReg.Columns.Add("cardType", typeof(string));
            dtCardReg.Columns.Add("cardCurrency", typeof(string));
            dtCardReg.Columns.Add("cardStatus", typeof(string));
            #endregion

            GetGridViewData(gvCardRegister, dtCardReg);

            foreach (DataRow rRole in tblRoleDetail.Rows)
            {
                string ccyid = "LAK";
                string cardSts = "A";
                DataRow[] drCard = dtCardReg.Select("cardNo = '" + rRole["CardNo"].ToString().Trim() + "'");
                if (drCard.Length > 0)
                {
                    // SmartPortal.Common.Log.WriteLogFile("=======Cardcurrency log ", "", "", drCard[0]["cardCurrency"].ToString());
                    ccyid = SmartPortal.Common.Utilities.Utility.MappingCurrency(drCard[0]["cardCurrency"].ToString().Trim());
                    cardSts = mappingCardStatus(drCard[0]["cardStatus"].ToString().Trim(), false);
                }

                DataRow r = tblNguoiUyQuyen.NewRow();
                r["colContractNo"] = txtOwnContractNo.Text.Trim();
                r["colCardNo"] = rRole["CardNo"].ToString();
                r["colUserID"] = rRole["UserID"].ToString();
                r["colUserName"] = rRole["FullName"].ToString();
                r["colCardType"] = mappingCardType(rRole["CardType"].ToString(), true);
                r["colCCYID"] = rRole["CCYID"].ToString();

                r["colCardStatus"] = rRole["CCYID"].ToString();
                r["colBranchID"] = "";
                r["colLinkType"] = rRole["LinkType"].ToString();
                r["colRightType"] = rRole["RightType"].ToString();
                r["colHolderCFCode"] = hdfCardHolderCustCode.Value;
                r["colHolderName"] = txtCardHolderFullName.Text.Trim();
                r["colRole"] = rRole["RoleName"].ToString();
                r["colRoleID"] = rRole["RoleID"].ToString();
                r["colTranCode"] = rRole["PageName"].ToString();
                r["colTranCodeID"] = rRole["IPCTRANCODE"].ToString();
                r["colServiceID"] = rRole["SERVICEID"].ToString();

                tblNguoiUyQuyen.Rows.Add(r);
            }

            ViewState["NGUOIQUANTRI"] = tblNguoiUyQuyen;

            gvResultQuanTri.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colUserID", "colUserName", "colCardNo", "colRole");
            gvResultQuanTri.DataBind();
            #endregion
        }
        #region Hien thi tat cac cac role theo serviceid va usertype len cay
        if ((SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == SmartPortal.Constant.IPC.PERSONALCONTRACT) || SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == SmartPortal.Constant.IPC.RP)

        {

            //load for IB
            LoadDataInTreeview(SmartPortal.Constant.IPC.IB, tvIBQT, contractNo);

            //load for MB
            LoadDataInTreeview(SmartPortal.Constant.IPC.MB, tvMBQT, contractNo);
        }
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == SmartPortal.Constant.IPC.CORPORATECONTRACT)
        {
            //load for IB
            LoadDataInTreeview1(SmartPortal.Constant.IPC.IB, tvIBQT, "CTK");

            //load for MB
            LoadDataInTreeview1(SmartPortal.Constant.IPC.MB, tvMBQT, userType);
        }

        #endregion
    }

    void LoadDataInTreeview(string serviceID, TreeView tvPage, string cn)
    {
        tvPage.Nodes.Clear();
        DataTable tblSS = new DataTable();

        //DataSet ds=new SmartPortal.IB.Role().GetRoleByServiceID(serviceID, cn, ref IPCERRORCODE, ref IPCERRORDESC);
        tblSS = new SmartPortal.SEMS.Contract().GetRoleByServiceAndContractForCard(serviceID, cn);

        foreach (DataRow row in tblSS.Rows)
        {
            TreeNode node = new TreeNode(row["ROLENAME"].ToString(), row["ROLEID"].ToString());
            node.ShowCheckBox = true;
            node.Checked = true;


            DataSet dsTransaction = new DataSet();
            DataTable tblPage = new DataTable();
            dsTransaction = new SmartPortal.SEMS.Role().GetTranOfRole(row["ROLEID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                tblPage = dsTransaction.Tables[0];
            }
            foreach (DataRow row1 in tblPage.Rows)
            {
                TreeNode node1 = new TreeNode(row1["PAGENAME"].ToString(), row1["TRANCODE"].ToString());
                node1.ShowCheckBox = false;
                node1.ToolTip = row1["PageDescription"].ToString();
                //
                node1.Checked = true;

                node.ChildNodes.Add(node1);
            }
            if (tblPage.Rows.Count != 0)
            {
                node.CollapseAll();
                tvPage.Nodes.Add(node);
            }
        }
        tvPage.Attributes.Add("onclick", "OnTreeClick(event)");
    }
    private void GetGridViewData(GridView gv, DataTable dt)
    {
        foreach (GridViewRow row in gv.Rows)
        {
            DataRow dr;
            dr = dt.NewRow();

            for (int i = 0; i < row.Cells.Count; i++)
            {
                Control ct = row.Cells[i].Controls[1];
                if (ct.GetType().Equals(typeof(CheckBox)))
                {
                    dr[i] = ((CheckBox)ct).Checked;
                }
                else if (ct.GetType().Equals(typeof(Label)))
                {
                    dr[i] = ((Label)ct).Text.Trim();
                }
            }
            dt.Rows.Add(dr);
        }
    }
    protected void btnThemNQT_Click(object sender, EventArgs e)
    {
        try
        {
            // SmartPortal.Common.Log.WriteLogFile("test befor mapp ", "", "", "");
            string k = SmartPortal.Common.Utilities.Utility.MappingCurrency("104");
            // SmartPortal.Common.Log.WriteLogFile("test after mapp ", "", "", "");
            string action = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString();
            switch (action)
            {
                case SmartPortal.Constant.IPC.ADD:
                    if (string.IsNullOrEmpty(txtCardHolderCustCode.Text.Trim()))
                    {
                        ShowPopUpMsg(Resources.labels.machutaikhoankhongduoctrong);
                        txtCardHolderCustCode.Focus();
                        return;
                    }
                    break;
                case SmartPortal.Constant.IPC.EDIT:
                    break;
            }

            DataTable dtUserReg = new DataTable();
            dtUserReg.Columns.Add("colCheckbox", typeof(bool));
            dtUserReg.Columns.Add("UserID", typeof(string));
            dtUserReg.Columns.Add("FullName", typeof(string));
            dtUserReg.Columns.Add("Email", typeof(string));
            dtUserReg.Columns.Add("Phone", typeof(string));
            dtUserReg.Columns.Add("Status", typeof(string));

            GetGridViewData(gvUserRegister, dtUserReg);

            DataTable dtCardReg = new DataTable();
            dtCardReg.Columns.Add("colCheckbox", typeof(bool));
            dtCardReg.Columns.Add("cardNo", typeof(string));
            dtCardReg.Columns.Add("cardholderName", typeof(string));
            dtCardReg.Columns.Add("cardType", typeof(string));
            dtCardReg.Columns.Add("cardCurrency", typeof(string));
            dtCardReg.Columns.Add("cardStatus", typeof(string));

            GetGridViewData(gvCardRegister, dtCardReg);
            string linkType = (txtOwnCustCode.Text.Trim().Equals(hdfCardHolderCustCode.Value.Trim())) ? "OWN" : "OTH";

            foreach (DataRow drUser in dtUserReg.Rows)
            {
                if (bool.Parse(drUser["colCheckbox"].ToString()))
                {
                    foreach (DataRow drCard in dtCardReg.Rows)
                    {
                        if (bool.Parse(drCard["colCheckbox"].ToString()))
                        {
                            LuuThongTinQuyen("NGUOIQUANTRI", gvResultQuanTri, tvIBQT, tvMBQT, drCard["cardholderName"].ToString(), drCard["cardNo"].ToString(), mappingCardType(drCard["cardType"].ToString(), false), linkType, drUser["UserID"].ToString(), drUser["FullName"].ToString(), drCard["cardCurrency"].ToString(), mappingCardStatus(drCard["cardStatus"].ToString(), false), hdfCardHolderCustCode.Value.Trim());
                        }
                    }
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCard_Add_Widget", "btnThemNQT_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }

        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCard_Add_Widget", "btnThemNQT_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvResultQuanTri_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            gvResultQuanTri.PageIndex = e.NewPageIndex;
            gvResultQuanTri.DataSource = (DataTable)ViewState["NGUOIQUANTRI"];
            gvResultQuanTri.DataBind();
        }
        catch
        {
        }
    }

    protected void gvResultQuanTri_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            DataTable tblResultQT = (DataTable)ViewState["NGUOIQUANTRI"];

            tblResultQT.Rows.RemoveAt(e.RowIndex + (gvResultQuanTri.PageIndex * gvResultQuanTri.PageSize));

            ViewState["NGUOIQUANTRI"] = tblResultQT;
            gvResultQuanTri.DataSource = tblResultQT;
            gvResultQuanTri.DataBind();

            lblAlert.Text = Resources.labels.recorddeleted;
        }
        catch
        {
        }
    }

    void LuuThongTinQuyen(string sessionName, GridView gvResult, TreeView tvRole, TreeView tvMBRole, string cardHolderName, string cardNo, string cardType, string linkType, string userID, string userName, string ccyid, string cardStatus, string cardHolderCustCode)
    {
        if (ViewState[sessionName] == null)
        {
            #region NULL
            #region khoi tao bang
            DataTable tblNguoiUyQuyen = new DataTable();

            DataColumn colContractNo = new DataColumn("colContractNo");
            DataColumn colCardNo = new DataColumn("colCardNo");
            DataColumn colUserID = new DataColumn("colUserID");
            DataColumn colUserName = new DataColumn("colUserName");
            DataColumn colCardType = new DataColumn("colCardType");
            DataColumn colCCYID = new DataColumn("colCCYID");
            DataColumn colCardStatus = new DataColumn("colCardStatus");
            DataColumn colBranchID = new DataColumn("colBranchID");
            DataColumn colLinkType = new DataColumn("colLinkType");
            DataColumn colRightType = new DataColumn("colRightType");
            DataColumn colHolderCFCode = new DataColumn("colHolderCFCode");
            DataColumn colHolderName = new DataColumn("colHolderName");
            DataColumn colRole = new DataColumn("colRole");
            DataColumn colRoleID = new DataColumn("colRoleID");
            DataColumn colTranCode = new DataColumn("colTranCode");
            DataColumn colTranCodeID = new DataColumn("colTranCodeID");
            DataColumn colServiceID = new DataColumn("colServiceID");

            tblNguoiUyQuyen.Columns.Add(colContractNo);
            tblNguoiUyQuyen.Columns.Add(colCardNo);
            tblNguoiUyQuyen.Columns.Add(colUserID);
            tblNguoiUyQuyen.Columns.Add(colUserName);
            tblNguoiUyQuyen.Columns.Add(colCardType);
            tblNguoiUyQuyen.Columns.Add(colCCYID);
            tblNguoiUyQuyen.Columns.Add(colCardStatus);
            tblNguoiUyQuyen.Columns.Add(colBranchID);
            tblNguoiUyQuyen.Columns.Add(colLinkType);
            tblNguoiUyQuyen.Columns.Add(colRightType);
            tblNguoiUyQuyen.Columns.Add(colHolderCFCode);
            tblNguoiUyQuyen.Columns.Add(colHolderName);
            tblNguoiUyQuyen.Columns.Add(colRole);
            tblNguoiUyQuyen.Columns.Add(colRoleID);
            tblNguoiUyQuyen.Columns.Add(colTranCode);
            tblNguoiUyQuyen.Columns.Add(colTranCodeID);
            tblNguoiUyQuyen.Columns.Add(colServiceID);

            #endregion

            if (!string.IsNullOrEmpty(cardNo) && !string.IsNullOrEmpty(userID))
            {
                //load righ IB
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            #region luu quyen IB khi NULL
                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colContractNo"] = contractNo.Trim();
                            rowNguoiUyQuyen["colCardNo"] = cardNo.Trim();
                            rowNguoiUyQuyen["colUserID"] = userID.Trim();
                            rowNguoiUyQuyen["colUserName"] = userName.Trim();
                            rowNguoiUyQuyen["colCardType"] = cardType.Trim();
                            rowNguoiUyQuyen["colCCYID"] = ccyid.Trim();
                            rowNguoiUyQuyen["colCardStatus"] = cardStatus.Trim();
                            rowNguoiUyQuyen["colBranchID"] = "";
                            rowNguoiUyQuyen["colLinkType"] = linkType;
                            rowNguoiUyQuyen["colRightType"] = "";
                            rowNguoiUyQuyen["colHolderCFCode"] = cardHolderCustCode.Trim();
                            rowNguoiUyQuyen["colHolderName"] = cardHolderName.Trim();
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.IB;

                            tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                            #endregion
                        }
                    }
                    else
                    {
                    }
                }

                //load right MB
                foreach (TreeNode nodeRoleMB in tvMBRole.Nodes)
                {
                    if (nodeRoleMB.Checked)
                    {
                        foreach (TreeNode nodeTrancodeMBNguoiUyQuyen in nodeRoleMB.ChildNodes)
                        {
                            #region luu quyen IB khi NULL
                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colContractNo"] = contractNo.Trim();
                            rowNguoiUyQuyen["colCardNo"] = cardNo.Trim();
                            rowNguoiUyQuyen["colUserID"] = userID.Trim();
                            rowNguoiUyQuyen["colUserName"] = userName.Trim();
                            rowNguoiUyQuyen["colCardType"] = cardType.Trim();
                            rowNguoiUyQuyen["colCCYID"] = ccyid.Trim();
                            rowNguoiUyQuyen["colCardStatus"] = cardStatus.Trim();
                            rowNguoiUyQuyen["colBranchID"] = "";
                            rowNguoiUyQuyen["colLinkType"] = linkType;
                            rowNguoiUyQuyen["colRightType"] = "";
                            rowNguoiUyQuyen["colHolderCFCode"] = cardHolderCustCode.Trim();
                            rowNguoiUyQuyen["colHolderName"] = cardHolderName.Trim();
                            rowNguoiUyQuyen["colRole"] = nodeRoleMB.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleMB.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeMBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeMBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.MB;

                            tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                            #endregion
                        }
                    }
                    else
                    {
                    }
                }
            }

            ViewState[sessionName] = tblNguoiUyQuyen;
            gvResult.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colUserID", "colUserName", "colCardNo", "colRole");
            gvResult.DataBind();
            #endregion
        }
        else
        {
            DataTable tblNguoiUyQuyen = (DataTable)ViewState[sessionName];

            if (!string.IsNullOrEmpty(cardNo) && !string.IsNullOrEmpty(userID))
            {
                //IB
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            if (tblNguoiUyQuyen.Select("colUserID='" + userID.Trim() + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colCardNo='" + cardNo + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region luu quyen IB khi NULL
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colContractNo"] = contractNo.Trim();
                                rowNguoiUyQuyen["colCardNo"] = cardNo.Trim();
                                rowNguoiUyQuyen["colUserID"] = userID.Trim();
                                rowNguoiUyQuyen["colUserName"] = userName.Trim();
                                rowNguoiUyQuyen["colCardType"] = cardType.Trim();
                                rowNguoiUyQuyen["colCCYID"] = ccyid.Trim();
                                rowNguoiUyQuyen["colCardStatus"] = cardStatus.Trim();
                                rowNguoiUyQuyen["colBranchID"] = "";
                                rowNguoiUyQuyen["colLinkType"] = linkType;
                                rowNguoiUyQuyen["colRightType"] = "";
                                rowNguoiUyQuyen["colHolderCFCode"] = cardHolderCustCode.Trim();
                                rowNguoiUyQuyen["colHolderName"] = cardHolderName.Trim();
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.IB;

                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            if (tblNguoiUyQuyen.Select("colUserID='" + userID.Trim() + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colCardNo='" + cardNo + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

                            }
                            else
                            {
                                if (tblNguoiUyQuyen.Select("colUserID='" + userID.Trim() + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colCardNo='" + cardNo + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                                {
                                    foreach (DataRow r in tblNguoiUyQuyen.Select("colUserID='" + userID.Trim() + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colCardNo='" + cardNo + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
                                    {
                                        tblNguoiUyQuyen.Rows.Remove(r);
                                    }
                                }
                            }
                        }
                    }

                    //MB
                    foreach (TreeNode nodeRoleMBNguoiUyQuyen in tvMBRole.Nodes)
                    {
                        if (nodeRoleMBNguoiUyQuyen.Checked)
                        {
                            foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleMBNguoiUyQuyen.ChildNodes)
                            {
                                if (tblNguoiUyQuyen.Select("colUserID='" + userID.Trim() + "' and colRoleID='" + nodeRoleMBNguoiUyQuyen.Value + "' and colCardNo='" + cardNo + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                                {
                                    #region luu quyen IB khi NULL
                                    DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                    rowNguoiUyQuyen["colContractNo"] = contractNo.Trim();
                                    rowNguoiUyQuyen["colCardNo"] = cardNo.Trim();
                                    rowNguoiUyQuyen["colUserID"] = userID.Trim();
                                    rowNguoiUyQuyen["colUserName"] = userName.Trim();
                                    rowNguoiUyQuyen["colCardType"] = cardType.Trim();
                                    rowNguoiUyQuyen["colCCYID"] = ccyid.Trim();
                                    rowNguoiUyQuyen["colCardStatus"] = cardStatus.Trim();
                                    rowNguoiUyQuyen["colBranchID"] = "";
                                    rowNguoiUyQuyen["colLinkType"] = linkType;
                                    rowNguoiUyQuyen["colRightType"] = "";
                                    rowNguoiUyQuyen["colHolderCFCode"] = cardHolderCustCode.Trim();
                                    rowNguoiUyQuyen["colHolderName"] = cardHolderName.Trim();
                                    rowNguoiUyQuyen["colRole"] = nodeRoleMBNguoiUyQuyen.Text;
                                    rowNguoiUyQuyen["colRoleID"] = nodeRoleMBNguoiUyQuyen.Value;
                                    rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                    rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                    rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.MB;

                                    tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleMBNguoiUyQuyen.ChildNodes)
                            {
                                if (tblNguoiUyQuyen.Select("colUserID='" + userID.Trim() + "' and colRoleID='" + nodeRoleMBNguoiUyQuyen.Value + "' and colCardNo='" + cardNo + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                                {

                                }
                                else
                                {
                                    if (tblNguoiUyQuyen.Select("colUserID='" + userID.Trim() + "' and colRoleID='" + nodeRoleMBNguoiUyQuyen.Value + "' and colCardNo='" + cardNo + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                                    {
                                        foreach (DataRow r in tblNguoiUyQuyen.Select("colUserID='" + userID.Trim() + "' and colRoleID='" + nodeRoleMBNguoiUyQuyen.Value + "' and colCardNo='" + cardNo + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
                                        {
                                            tblNguoiUyQuyen.Rows.Remove(r);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            ViewState[sessionName] = tblNguoiUyQuyen;
            gvResult.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colUserID", "colUserName", "colCardNo", "colRole");
            gvResult.DataBind();
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["NGUOIQUANTRI"] == null)
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.FULLINFOERROR);
            }

            string actionDate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(DateTime.Now.ToString("dd/MM/yyyy"));
            string actionUser = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString().Trim());
            string status = "P";

            switch (ACTION)
            {
                case SmartPortal.Constant.IPC.ADD:
                    status = SmartPortal.Constant.IPC.NEW;
                    break;
                case SmartPortal.Constant.IPC.EDIT:
                    status = SmartPortal.Constant.IPC.PENDING;
                    break;
                default:
                    break;
            }

            DataTable tblNGUOIQUANTRI = new DataTable();

            if (ViewState["NGUOIQUANTRI"] != null)
            {
                tblNGUOIQUANTRI = (DataTable)ViewState["NGUOIQUANTRI"];
            }

            if (gvResultQuanTri.Rows.Count == 0)
            {
                lblAlert.Text = Resources.labels.banchuadangkydichvu;
                return;
            }

            //vutt 16032017
            foreach (DataRow dr in tblNGUOIQUANTRI.Rows)
            {
                dr["colCardNo"] = SmartPortal.Control.Security.EncryptCard(dr["colCardNo"].ToString());
            }

            #region addin value

            DataColumn colUserCreatetbl = new DataColumn("colUserCreate");
            colUserCreatetbl.DefaultValue = actionUser;
            tblNGUOIQUANTRI.Columns.Add(colUserCreatetbl);

            DataColumn colDateCreatetbl = new DataColumn("colDateCreate");
            colDateCreatetbl.DefaultValue = actionDate;
            tblNGUOIQUANTRI.Columns.Add(colDateCreatetbl);

            DataColumn colUserLastModifytbl = new DataColumn("colUserLastModify");
            colUserLastModifytbl.DefaultValue = actionUser;
            tblNGUOIQUANTRI.Columns.Add(colUserLastModifytbl);

            DataColumn colDateLastModifytbl = new DataColumn("colDateLastModify");
            colDateLastModifytbl.DefaultValue = actionDate;
            tblNGUOIQUANTRI.Columns.Add(colDateLastModifytbl);

            DataColumn colStatustbl = new DataColumn("colStatus");
            colStatustbl.DefaultValue = status;
            tblNGUOIQUANTRI.Columns.Add(colStatustbl);
            #endregion


            #region tao bang EBA_ContractCardHolder
            DataTable dtNQTTemp = tblNGUOIQUANTRI.Copy();

            DataColumn colCFCodetmp = new DataColumn("colCFCode");
            colCFCodetmp.DefaultValue = txtOwnCustCode.Text.Trim();
            dtNQTTemp.Columns.Add(colCFCodetmp);

            dtNQTTemp.Columns.Remove("colHolderName");

            DataColumn colHolderNametmp = new DataColumn("colHolderName");
            colHolderNametmp.DefaultValue = txtCardHolderFullName.Text.Trim();
            dtNQTTemp.Columns.Add(colHolderNametmp);

            DataTable tblContractCardHolder = dtNQTTemp.DefaultView.ToTable(true, "colContractNo", "colHolderCFCode", "colHolderName", "colLinkType", "colStatus", "colUserCreate", "colDateCreate", "colUserLastModify", "colDateLastModify");

            #endregion

            #region tao bang EBA_UserCard
            DataTable tblUserCard = new DataTable();
            DataColumn colUserID = new DataColumn("colUserID");
            DataColumn colContractNo = new DataColumn("colContractNo");
            DataColumn colCardNo = new DataColumn("colCardNo");
            DataColumn colCardType = new DataColumn("colCardType");
            DataColumn colLinkType = new DataColumn("colLinkType");
            DataColumn colRightType = new DataColumn("colRightType");
            DataColumn colHolderCFCode = new DataColumn("colHolderCFCode");
            DataColumn colHolderName = new DataColumn("colHolderName");
            DataColumn colCCYID = new DataColumn("colCCYID");
            DataColumn colStatus = new DataColumn("colStatus");
            DataColumn colUserCreate = new DataColumn("colUserCreate");
            DataColumn colDateCreate = new DataColumn("colDateCreate");
            DataColumn colUserLastModify = new DataColumn("colUserLastModify");
            DataColumn colDateLastModify = new DataColumn("colDateLastModify");

            //add vào table
            tblUserCard.Columns.Add(colUserID);
            tblUserCard.Columns.Add(colContractNo);
            tblUserCard.Columns.Add(colCardNo);
            tblUserCard.Columns.Add(colCardType);
            tblUserCard.Columns.Add(colLinkType);
            tblUserCard.Columns.Add(colRightType);
            tblUserCard.Columns.Add(colHolderCFCode);
            tblUserCard.Columns.Add(colHolderName);
            tblUserCard.Columns.Add(colCCYID);
            tblUserCard.Columns.Add(colStatus);
            tblUserCard.Columns.Add(colUserCreate);
            tblUserCard.Columns.Add(colDateCreate);
            tblUserCard.Columns.Add(colUserLastModify);
            tblUserCard.Columns.Add(colDateLastModify);

            tblUserCard = tblNGUOIQUANTRI.DefaultView.ToTable(true, "colUserID", "colContractNo", "colCardNo", "colCardType", "colLinkType", "colRightType", "colHolderCFCode", "colHolderName", "colCCYID", "colStatus", "colUserCreate", "colDateCreate", "colUserLastModify", "colDateLastModify");
            #endregion

            #region tao bang EBA_UserCardRight
            DataTable tblCardRight = new DataTable();
            DataColumn colUserID_Right = new DataColumn("colUserID");
            DataColumn colCardNo_Right = new DataColumn("colCardNo");
            DataColumn colRoleID_Right = new DataColumn("colRoleID");

            //add vào table
            tblCardRight.Columns.Add(colUserID_Right);
            tblCardRight.Columns.Add(colCardNo_Right);
            tblCardRight.Columns.Add(colRoleID_Right);

            tblCardRight = tblNGUOIQUANTRI.DefaultView.ToTable(true, "colUserID", "colCardNo", "colRoleID");
            #endregion

            #region tao bang EBA_UserCardRightDetail
            DataTable tblCardRightDetail = new DataTable();
            DataColumn colUserID_RightDetail = new DataColumn("colUserID");
            DataColumn colCardNo_RightDetail = new DataColumn("colCardNo");
            DataColumn colTranCode_RightDetail = new DataColumn("colTranCodeID");
            DataColumn colServiceID_RightDetail = new DataColumn("colServiceID");

            //add vào table
            tblCardRightDetail.Columns.Add(colUserID_RightDetail);
            tblCardRightDetail.Columns.Add(colCardNo_RightDetail);
            tblCardRightDetail.Columns.Add(colTranCode_RightDetail);
            tblCardRightDetail.Columns.Add(colServiceID_RightDetail);

            tblCardRightDetail = tblNGUOIQUANTRI.DefaultView.ToTable(true, "colUserID", "colCardNo", "colTranCodeID", "colServiceID");
            #endregion


            #region lay dataset tong hop quyen
            DataSet dsCurrentRoleDetail = new SmartPortal.SEMS.Card().GetCardRoldeDetailForIU(txtOwnContractNo.Text.Trim(), hdfCardHolderCustCode.Value.Trim(), ACTION, ref IPCERRORCODE, ref IPCERRORDESC);
            Dictionary<string, string> dicIBUserName = dsCurrentRoleDetail.Tables[0].AsEnumerable().ToDictionary<DataRow, string, string>(x => x["UserID"].ToString(), x => x["Username"].ToString());
            Dictionary<string, string> dicMBPhoneNo = dsCurrentRoleDetail.Tables[0].AsEnumerable().ToDictionary<DataRow, string, string>(x => x["UserID"].ToString(), x => x["Username"].ToString());
            #endregion

            #region Tạo bảng chứa quyền user Ibank
            //tao 1 dong du lieu
            DataRow[] drarrIBR = tblNGUOIQUANTRI.Select("colServiceID='" + SmartPortal.Constant.IPC.IB + "'");
            DataTable arrIBR = (drarrIBR.Length > 0) ? arrIBR = drarrIBR.CopyToDataTable() : tblNGUOIQUANTRI.Clone();

            DataTable tblIbankUserRightOr = arrIBR.DefaultView.ToTable(true, "colUserID", "colRoleID");
            DataTable tblIbankUserRight = tblIbankUserRightOr.Clone();
            //clean duplicate data
            switch (ACTION)
            {
                case SmartPortal.Constant.IPC.ADD:
                    foreach (DataRow dr in tblIbankUserRightOr.Rows)
                    {
                        if (dsCurrentRoleDetail.Tables[2].AsEnumerable().Count<DataRow>(x => x["USERID"].ToString().Equals(dr["colUserID"].ToString().Trim()) && x["ROLEID"].ToString().Equals(dr["colRoleID"].ToString().Trim())) == 0)
                        {
                            DataRow drf = tblIbankUserRight.NewRow();
                            drf["colUserID"] = dicIBUserName[dr["colUserID"].ToString().Trim()].ToString();
                            drf["colRoleID"] = dr["colRoleID"].ToString().Trim();
                            tblIbankUserRight.Rows.Add(drf);
                        }
                    }
                    break;
                case SmartPortal.Constant.IPC.EDIT:
                    DataTable dtCurrent = tblIbankUserRightOr.Clone();
                    foreach (DataRow drcl in dsCurrentRoleDetail.Tables[2].Rows)
                    {
                        DataRow drclc = dtCurrent.NewRow();
                        drclc["colUserID"] = drcl["USERID"].ToString();
                        drclc["colRoleID"] = drcl["ROLEID"].ToString();
                        dtCurrent.Rows.Add(drclc);
                    }

                    tblIbankUserRightOr.Merge(dtCurrent);
                    tblIbankUserRight = tblIbankUserRightOr.DefaultView.ToTable(true, "colUserID", "colRoleID");
                    foreach (DataRow dr in tblIbankUserRight.Rows)
                    {
                        dr["colUserID"] = dicIBUserName[dr["colUserID"].ToString()].ToString().Trim();
                    }
                    break;
                default:
                    break;
            }

            #endregion

            #region Tạo bảng chứa quyền user MB
            DataRow[] drarrMBR = tblNGUOIQUANTRI.Select("colServiceID='" + SmartPortal.Constant.IPC.MB + "'");
            DataTable arrMBR = (drarrIBR.Length > 0) ? arrIBR = drarrIBR.CopyToDataTable() : tblNGUOIQUANTRI.Clone();

            DataTable tblMBUserRightOr = arrMBR.DefaultView.ToTable(true, "colUserID", "colRoleID");
            DataTable tblMBUserRight = tblMBUserRightOr.Clone();
            // clean duplicate data
            switch (ACTION)
            {
                case SmartPortal.Constant.IPC.ADD:
                    foreach (DataRow dr in tblMBUserRight.Rows)
                    {
                        if (dsCurrentRoleDetail.Tables[3].AsEnumerable().Count<DataRow>(x => x["USERID"].ToString().Equals(dr["colUserID"].ToString().Trim()) && x["ROLEID"].ToString().Equals(dr["colRoleID"].ToString().Trim())) == 0)
                        {
                            DataRow drf = tblIbankUserRight.NewRow();
                            drf["colUserID"] = dicMBPhoneNo[dr["colUserID"].ToString().Trim()].ToString();
                            drf["colRoleID"] = dr["colRoleID"].ToString().Trim();
                            tblMBUserRight.Rows.Add(drf);
                        }
                    }
                    break;
                case SmartPortal.Constant.IPC.EDIT:
                    DataTable dtCurrent = tblMBUserRightOr.Clone();
                    foreach (DataRow drcl in dsCurrentRoleDetail.Tables[3].Rows)
                    {
                        DataRow drclc = dtCurrent.NewRow();
                        drclc["colUserID"] = drcl["USERID"].ToString();
                        drclc["colRoleID"] = drcl["ROLEID"].ToString();
                        dtCurrent.Rows.Add(drclc);
                    }
                    tblMBUserRightOr.Merge(dtCurrent);
                    tblMBUserRight = tblMBUserRightOr.DefaultView.ToTable(true, "colUserID", "colRoleID");
                    foreach (DataRow dr in tblMBUserRight.Rows)
                    {
                        dr["colUserID"] = dicMBPhoneNo[dr["colUserID"].ToString()].ToString().Trim();
                    }
                    break;
                default:
                    break;
            }
            #endregion

            #region Tạo bảng chứa quyền contract
            DataTable tblCTRightOr = tblNGUOIQUANTRI.DefaultView.ToTable(true, "colContractNo", "colRoleID");
            DataTable tblCTRight = tblCTRightOr.Clone();

            // clean duplicate data
            switch (ACTION)
            {
                case SmartPortal.Constant.IPC.ADD:
                    foreach (DataRow dr in tblCTRightOr.Rows)
                    {
                        if (dsCurrentRoleDetail.Tables[4].AsEnumerable().Count<DataRow>(x => x["ContractNo"].ToString().Equals(dr["colContractNo"].ToString().Trim()) && x["RoleID"].ToString().Equals(dr["colRoleID"].ToString().Trim())) == 0)
                        {
                            DataRow drf = tblCTRight.NewRow();
                            drf["colContractNo"] = dr["colContractNo"].ToString().Trim();
                            drf["colRoleID"] = dr["colRoleID"].ToString().Trim();
                            tblCTRight.Rows.Add(drf);
                        }
                    }
                    break;
                case SmartPortal.Constant.IPC.EDIT:
                    DataTable dtCurrent = tblCTRightOr.Clone();
                    foreach (DataRow drcl in dsCurrentRoleDetail.Tables[4].Rows)
                    {
                        DataRow drclc = dtCurrent.NewRow();
                        drclc["colContractNo"] = drcl["ContractNo"].ToString();
                        drclc["colRoleID"] = drcl["ROLEID"].ToString();
                        dtCurrent.Rows.Add(drclc);
                    }

                    tblCTRightOr.Merge(dtCurrent);
                    tblCTRight = tblCTRightOr.DefaultView.ToTable(true, "colContractNo", "colRoleID");
                    break;
                default:
                    break;
            }

            #endregion

            switch (ACTION)
            {
                case SmartPortal.Constant.IPC.ADD:
                    #region insert card
                    new SmartPortal.SEMS.Card().InsertCard(contractNo, hdfCardHolderCustCode.Value.Trim(), tblContractCardHolder, tblUserCard, tblCardRight, tblCardRightDetail, tblIbankUserRight, tblMBUserRight, tblCTRight, actionUser, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        ltrError.Text = Resources.labels.addcardsuccessful;
                        btsaveandcont.Visible = false;
                        btnAddNew.Visible = true;
                        btnBackHome.Visible = true;
                        btback.Visible = false;
                        try
                        {
                            foreach (DataRow dr in tblUserCard.Rows)
                            {
                                SmartPortal.Common.Log.WriteLogDatabase(contractNo, dr["colUserID"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                                Request.UserHostAddress, "EBA_UserCard", "Add Credit Card", "", dr["colCardNo"].ToString(), SmartPortal.Constant.IPC.PENDING);
                            }
                        }
                        catch { }

                    }
                    else
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        ViewState["ROLEDETAIL"] = null;
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.ADDCARDERROR);
                    }

                    #endregion

                    break;
                case SmartPortal.Constant.IPC.EDIT:
                    #region update user
                    //new SmartPortal.SEMS.User().Update(contractNo, userID, userFullName, status, userGender, userAddress, userEmail, userPhone, userCreate, userType, userLevel, type, deptID, tokenID, smsOTP, userBirthday, IBUserName, "", IBStatus, lastModify, SMSPhoneNo, SMSDefaultAcctno, (cbIsDefault.Checked == true ? "Y" : "N"), SMSStatus, ddlDefaultLang.SelectedValue, MBPhoneNo, "", MBStatus, PHOPhoneNo, "", PHOStatus, ddlPHODefaultAcctno.SelectedValue, tblIbankUserRight, tblSMSUserRight, tblMBUserRight, tblPHOUserRight, tblTranrightDetail, tblUserAccount, tblContractAccount, tblSMSNotify, ref IPCERRORCODE, ref IPCERRORDESC);
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMSCARDUPDATE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_UserCard", "USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + contractNo + "')");
                        SmartPortal.Common.Log.WriteLog("SEMSCARDUPDATE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_UserCardRight", "USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + contractNo + "')");
                        SmartPortal.Common.Log.WriteLog("SEMSCARDUPDATE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_UserCardRightDetail", "USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + contractNo + "')");
                    }
                    catch
                    {
                    }
                    new SmartPortal.SEMS.Card().UpdateCard(contractNo, hdfCardHolderCustCode.Value.Trim(), tblContractCardHolder, tblUserCard, tblCardRight, tblCardRightDetail, tblIbankUserRight, tblMBUserRight, tblCTRight, actionUser, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        ltrError.Text = Resources.labels.editcardsuccessful;
                        btsaveandcont.Visible = false;

                        btnBackHome.Visible = true;
                        btback.Visible = false;
                        try
                        {
                            DataTable dtRoleOld = (DataTable)ViewState["ROLEDETAIL"];
                            //add new card
                            foreach (DataRow dr in tblUserCard.Rows)
                            {
                                if (dtRoleOld.Select("CardNo = '" + SmartPortal.Control.Security.DecryptCard(dr["colCardNo"].ToString()) + "' and UserID = '" + dr["colUserID"].ToString() + "'").Count() == 0)
                                {
                                    SmartPortal.Common.Log.WriteLogDatabase(contractNo, dr["colUserID"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                                    Request.UserHostAddress, "EBA_UserCard", "Add Credit Card", "", dr["colCardNo"].ToString(), SmartPortal.Constant.IPC.PENDING);
                                }
                            }
                            //delete card
                            DataTable tblUserCardold = new DataTable();
                            tblUserCardold = dtRoleOld.DefaultView.ToTable(true, "UserID", "CardNo");
                            foreach (DataRow dr1 in tblUserCardold.Rows)
                            {
                                if (tblUserCard.Select("colCardNo = '" + SmartPortal.Control.Security.EncryptCard(dr1["CardNo"].ToString()) + "' and colUserID = '" + dr1["UserID"].ToString() + "'").Count() == 0)
                                {
                                    SmartPortal.Common.Log.WriteLogDatabase(contractNo, dr1["UserID"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                                    Request.UserHostAddress, "EBA_UserCard", "Delete Credit Card", "", SmartPortal.Control.Security.EncryptCard(dr1["CardNo"].ToString()), SmartPortal.Constant.IPC.PENDING);
                                }
                            }
                        }
                        catch { }
                        ViewState["ROLEDETAIL"] = null;
                    }
                    else
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.UPDATECARDERROR);
                    }
                    #endregion
                    break;
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSUser_Widget", "btsaveandcont_Click", ex.StackTrace + IPCERRORDESC, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
    protected void btback_Click(object sender, EventArgs e)
    {
        ViewState["NGUOIQUANTRI"] = null;
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString())));
    }
    protected void btnHuy_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable tblResultQT = (DataTable)ViewState["NGUOIQUANTRI"];

            tblResultQT.Rows.Clear();

            ViewState["NGUOIQUANTRI"] = tblResultQT;
            gvResultQuanTri.DataSource = tblResultQT;
            gvResultQuanTri.DataBind();

            lblAlert.Text = Resources.labels.recorddeleted;
        }
        catch
        {
        }
    }
    protected void btnCardHolderDetail_Click(object sender, EventArgs e)
    {
        try
        {


            hdfCardHolderCustCode.Value = Utility.KillSqlInjection(txtCardHolderCustCode.Text.Trim());
            txtCardHolderFullName.Text = string.Empty;
            txtCardHolderEmail.Text = string.Empty;
            txtCardHolderPhone.Text = string.Empty;
            ddlCardHolderSex.SelectedIndex = 0;
            txtCardHolderDOB.Text = string.Empty;
            txtCardHolderAddress.Text = string.Empty;

            if (string.IsNullOrEmpty(txtCardHolderCustCode.Text.Trim()))
            {
                ShowPopUpMsg(Resources.labels.machutaikhoankhongduoctrong);
                txtCardHolderCustCode.Focus();
                return;
            }

            Hashtable hasCustomerInfo = new Hashtable();
            string ctmType = "P";
            hasCustomerInfo = new SmartPortal.SEMS.Customer().GetCustInfo(Utility.KillSqlInjection(txtCardHolderCustCode.Text.Trim()), ctmType, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                //ltrError.Text = 
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CUSTNOTEXIST);
                return;
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.CUSTCODE] == null)
            {
                ShowPopUpMsg(Resources.labels.makhachhangkhongtontaitronghethong);
                return;
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.CUSTNAME] != null && hasCustomerInfo[SmartPortal.Constant.IPC.LICENSE] != null)
            {
                txtCardHolderFullName.Text = hasCustomerInfo[SmartPortal.Constant.IPC.CUSTNAME].ToString();
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.EMAIL] != null)
                txtCardHolderEmail.Text = hasCustomerInfo[SmartPortal.Constant.IPC.EMAIL].ToString();
            if (hasCustomerInfo[SmartPortal.Constant.IPC.PHONE] != null)
                txtCardHolderPhone.Text = hasCustomerInfo[SmartPortal.Constant.IPC.PHONE].ToString();
            if (hasCustomerInfo[SmartPortal.Constant.IPC.LICENSE] != null)
                txtCardHolderLic.Text = hasCustomerInfo[SmartPortal.Constant.IPC.LICENSE].ToString();
            if (hasCustomerInfo[SmartPortal.Constant.IPC.SEX] != null)
            {
                try
                {
                    ddlCardHolderSex.SelectedValue = SmartPortal.Common.Utilities.Utility.FormatStringCore(int.Parse(hasCustomerInfo[SmartPortal.Constant.IPC.SEX].ToString()).ToString());
                }
                catch
                {
                }
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.DOB] != null)
            {
                try
                {
                    string birthDate = SmartPortal.Common.Utilities.Utility.IsDateTime2(hasCustomerInfo[SmartPortal.Constant.IPC.DOB].ToString()).ToString("dd/MM/yyyy");
                    txtCardHolderDOB.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;
                }
                catch
                {
                }
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.ADDRESS] != null)
            {
                txtCardHolderAddress.Text = hasCustomerInfo[SmartPortal.Constant.IPC.ADDRESS].ToString();
            }
            //if (hasCustomerInfo[SmartPortal.Constant.IPC.BRID] != null)
            //{
            //    ddlCardHolderBranch.SelectedValue = hasCustomerInfo[SmartPortal.Constant.IPC.BRID].ToString();
            //}

            DataSet dsCard = new SmartPortal.SEMS.Card().GetCardListByCustCode(hdfCardHolderCustCode.Value, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                ShowPopUpMsg("Connection to Card web service have problem");
                //ltrError.Text = Resources.labels.cardholdercustcodenotexist;
                return;
                //throw new IPCException("Transaction error");
            }
            else
            {
                if (dsCard.Tables.Count.Equals(0))
                {
                    ShowPopUpMsg(Resources.labels.cardholdercustcodenotexist);
                    //ltrError.Text = Resources.labels.cardholdercustcodenotexist;
                    return;
                }
            }
            gvCardRegister.DataSource = dsCard.Tables[0];
            gvCardRegister.DataBind();
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)

        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCard_Add_Widget", "btnCardHolderDetail_Click", ex.ToString(), Request.Url.Query);
            //SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btaddnew_Click(object sender, EventArgs e)
    {
        ViewState["NGUOIQUANTRI"] = null;
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(Request.RawUrl));
    }
    //28/8/2015 minh add to show message error trong truong hop su dung panel ajax
    private void ShowPopUpMsg(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
    }
}