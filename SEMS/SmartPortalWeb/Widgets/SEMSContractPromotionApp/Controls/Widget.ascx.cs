


using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
using System.ServiceModel.Configuration;
using iTextSharp.text;
using SmartPortal.BLL;
using SmartPortal.Constant;
using SmartPortal.Model;
using SmartPortal.SEMS;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class Widgets_SEMSContractPromotionApp_Controls_Widget : WidgetBase
{
    public static bool isAscend = false;
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    public static string APPTRANID;
    public static int Fuid = 1;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    //check validate flag false cho nhap so am, true khong nhap so am
    public bool allowNegativeFee = false;
    DataTable ProDeTable = new DataTable();
    double reFromLimit, reToLimit, roToLimit, roFromLimit;
    string reBiller, roBiller;
    bool IsLadder, roIsLadder;
    string vDiscountID
    {
        get { return ViewState["vDiscountID"] != null ? ViewState["vDiscountID"].ToString() : ""; }
        set { ViewState["vDiscountID"] = value; }
    }
    string vIsClone
    {
        get { return ViewState["vIsClone"] != null ? ViewState["vIsClone"].ToString() : ""; }
        set { ViewState["vIsClone"] = value; }
    }
    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;

            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                LoadddlAll();
                ViewState["PRODUCTDISTCOUNT"] = null;
                //load các giao dịch
                vDiscountID = "D" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
                object[] searchObject = new object[] { };
                BindData();
                ddlpromotiontype_SelectedIndexChanged(sender, e);
                if (ACTION.Equals(IPC.ACTIONPAGE.REVIEW))
                {
                    List<string> lst = new List<string>();
                    lst.Add(vDiscountID);
                    Session["ListRejectContract"] = lst;
                    Session["ListApproveContract"] = lst;
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
        DataTable tblRoleDefault = new DataTable();
        try
        {
       
            if (ACTION.Equals(IPC.ACTIONPAGE.DETAILS) || ACTION.Equals(IPC.ACTIONPAGE.REVIEW))
            {
                vDiscountID = GetParamsPage(IPC.ID)[0].Trim();
                Session["ListRejectContract"] = null;
                Session["ListApproveContract"] = null;
            }
            switch (ACTION)
            {

                case IPC.ACTIONPAGE.REJECT:
                    btnApprove.Enabled = false;
                    #region Lấy thông tin san pham
                    List<string> lstTran = (List<string>)Session["ListRejectContract"];

                    if (Session["ListRejectContract"] != null && lstTran.Count > 0)
                    {
                        vDiscountID = lstTran[0];
                    }
                    else if (string.IsNullOrEmpty(vDiscountID))
                    {
                        return;
                    }
                    DataSet dsDistcountreject = new DataSet();
                    dsDistcountreject = new SmartPortal.SEMS.PROMOTION().DetailsPromotion(vDiscountID, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsDistcountreject.Tables.Count != 0)
                    {
                        ProDeTable = dsDistcountreject.Tables[0];
                        if (ProDeTable.Rows.Count != 0)
                        {
                            //bind data to panel
                            txtcontractno.Text = ProDeTable.Rows[0]["ObjectID"].ToString();
                            ddlTransactionType.SelectedValue = ProDeTable.Rows[0]["TranCode"].ToString();
                            ddlPromotionSide.SelectedValue = ProDeTable.Rows[0]["BaseType"].ToString();
                            ddlContractLevel.SelectedValue = ProDeTable.Rows[0]["ContractLevel"].ToString();
                            ddlStatus.SelectedValue = ProDeTable.Rows[0]["Status"].ToString();
                            hdfstatus.Value = ProDeTable.Rows[0]["Status"].ToString();
                            radSchedule.SelectedValue = ProDeTable.Rows[0]["TimeType"].ToString();
                            ddlpromotiontype.SelectedValue = ProDeTable.Rows[0]["PromotionType"].ToString();
                            txtdiscountdes.Text = ProDeTable.Rows[0]["Description"].ToString();
                            txtPromotionName.Text = ProDeTable.Rows[0]["PromotionName"].ToString();
                            if (ProDeTable.Rows[0]["UseCount"].ToString().Equals("0") || ProDeTable.Rows[0]["UseCount"].ToString().Equals("-1"))
                            {
                                txtUseCount.Text = string.Empty;
                            }
                            else
                            {
                                txtUseCount.Text = ProDeTable.Rows[0]["UseCount"].ToString();
                            }
                            ddlBeneficiarySide.SelectedValue = ProDeTable.Rows[0]["BeneficiarySide"].ToString();
                            //bin data formular
                            DataTable FormulaTable = dsDistcountreject.Tables[5];
                            DataTable FormulaSubTable = dsDistcountreject.Tables[6];
                            if (FormulaTable.Rows.Count > 0)
                            {
                                hdfFormularID.Value = FormulaTable.Rows[0]["FORMULAID"].ToString();
                                FormulaDevExpress.Name = FormulaTable.Rows[0]["FORMULANAME"].ToString();
                                FormulaDevExpress.Value = FormulaTable.Rows[0]["FORMULA"].ToString();
                                ((Label)FormulaDevExpress.FindControl("lblFilter")).Text = FormulaTable.Rows[0]["FORMULA"].ToString();
                            }
                            else
                            {
                                hdfFormularID.Value = string.Empty;
                            }
                            if (FormulaSubTable.Rows.Count > 0)
                            {
                                FormulaDevExpress.dtFieldName = FormulaSubTable;
                            }

                            if (dsDistcountreject.Tables.Count > 2)
                            {
                                loadSechedualType(dsDistcountreject);
                            }
                            loadPaySide(dsDistcountreject);
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                    if (dsDistcountreject.Tables.Count > 2)
                    {
                        if (ViewState["PRODUCTDISTCOUNT"] == null)
                        {
                            ViewState["PRODUCTDISTCOUNT"] = dsDistcountreject.Tables[2];
                        }
                    }
                    if (dsDistcountreject.Tables.Count > 3)
                    {
                        if (ViewState["CONTRACTDISTCOUNTRELA"] == null)
                        {
                            ViewState["CONTRACTDISTCOUNTRELA"] = dsDistcountreject.Tables[4];
                        }
                    }
                    pnDiscountInfo.Enabled = false;
                    pnWeekly.Enabled = false;
                    pnDaily.Enabled = false;
                    pnMonthly.Enabled = false;
                    pnOnetime.Enabled = false;
                    pnDiscountDetail.Enabled = false;
                    pnPaySide.Enabled = false;
                    #endregion
                    break;

                case IPC.ACTIONPAGE.DETAILS:
                case IPC.ACTIONPAGE.REVIEW:
                case IPC.ACTIONPAGE.APPROVE:
                    btnReject.Enabled = false;
                    #region Lấy thông tin san pham
                    List<string> lstTranapp = (List<string>)Session["ListApproveContract"];

                    if (Session["ListApproveContract"] != null && lstTranapp.Count > 0)
                    {
                        vDiscountID = lstTranapp[0];
                    }
                    else if (string.IsNullOrEmpty(vDiscountID))
                    {
                        return;
                    }
                    DataSet dsDistcountapprove = new DataSet();
                    dsDistcountapprove = new SmartPortal.SEMS.PROMOTION().DetailsPromotion(vDiscountID, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsDistcountapprove.Tables.Count != 0)
                    {
                        ProDeTable = dsDistcountapprove.Tables[0];
                        if (ProDeTable.Rows.Count != 0)
                        {
                            //bind data to panel
                            txtcontractno.Text = ProDeTable.Rows[0]["ObjectID"].ToString();
                            ddlTransactionType.SelectedValue = ProDeTable.Rows[0]["TranCode"].ToString();
                            ddlPromotionSide.SelectedValue = ProDeTable.Rows[0]["BaseType"].ToString();
                            ddlContractLevel.SelectedValue = ProDeTable.Rows[0]["ContractLevel"].ToString();
                            ddlStatus.SelectedValue = ProDeTable.Rows[0]["Status"].ToString();
                            hdfstatus.Value = ProDeTable.Rows[0]["Status"].ToString();
                            radSchedule.SelectedValue = ProDeTable.Rows[0]["TimeType"].ToString();
                            ddlpromotiontype.SelectedValue = ProDeTable.Rows[0]["PromotionType"].ToString();
                            txtdiscountdes.Text = ProDeTable.Rows[0]["Description"].ToString();
                            txtPromotionName.Text = ProDeTable.Rows[0]["PromotionName"].ToString();
                            if (ProDeTable.Rows[0]["UseCount"].ToString().Equals("0") || ProDeTable.Rows[0]["UseCount"].ToString().Equals("-1"))
                            {
                                txtUseCount.Text = string.Empty;
                            }
                            else
                            {
                                txtUseCount.Text = ProDeTable.Rows[0]["UseCount"].ToString();
                            }

                            ddlBeneficiarySide.SelectedValue = ProDeTable.Rows[0]["BeneficiarySide"].ToString();
                            //bin data formular
                            DataTable FormulaTable = dsDistcountapprove.Tables[5];
                            DataTable FormulaSubTable = dsDistcountapprove.Tables[6];
                            if (FormulaTable.Rows.Count > 0)
                            {
                                hdfFormularID.Value = FormulaTable.Rows[0]["FORMULAID"].ToString();
                                FormulaDevExpress.Name = FormulaTable.Rows[0]["FORMULANAME"].ToString();
                                FormulaDevExpress.Value = FormulaTable.Rows[0]["FORMULA"].ToString();
                                ((Label)FormulaDevExpress.FindControl("lblFilter")).Text = FormulaTable.Rows[0]["FORMULA"].ToString();
                            }
                            else
                            {
                                hdfFormularID.Value = string.Empty;
                            }
                            if (FormulaSubTable.Rows.Count > 0)
                            {
                                FormulaDevExpress.dtFieldName = FormulaSubTable;
                            }
                            if (dsDistcountapprove.Tables.Count > 2)
                            {
                                loadSechedualType(dsDistcountapprove);
                            }
                            loadPaySide(dsDistcountapprove);
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                    if (dsDistcountapprove.Tables.Count > 2)
                    {
                        if (ViewState["PRODUCTDISTCOUNT"] == null)
                        {
                            ViewState["PRODUCTDISTCOUNT"] = dsDistcountapprove.Tables[2];
                        }
                    }
                    if (dsDistcountapprove.Tables.Count > 3)
                    {
                        if (ViewState["CONTRACTDISTCOUNTRELA"] == null)
                        {
                            ViewState["CONTRACTDISTCOUNTRELA"] = dsDistcountapprove.Tables[4];
                        }
                    }
                    pnDiscountInfo.Enabled = false;
                    pnWeekly.Enabled = false;
                    pnDaily.Enabled = false;
                    pnMonthly.Enabled = false;
                    pnOnetime.Enabled = false;
                    pnDiscountDetail.Enabled = false;
                    pnPaySide.Enabled = false;
                    #endregion
                    break;
                default:

                    pnDiscountInfo.Enabled = false;
                    pnWeekly.Enabled = false;
                    pnDaily.Enabled = false;
                    pnMonthly.Enabled = false;
                    pnOnetime.Enabled = false;
                    pnDiscountDetail.Enabled = false;
                    pnPaySide.Enabled = false;
                    btnReject.Visible = false;
                    btnApprove.Visible = false;
                    btnReject.Enabled = true;
                    break;

            }
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    txtcontractno.Enabled = false;
                    btback.Visible = true;
                    btnApprove.Enabled = false;
                    DataTable tblProductDiscountde = (DataTable)ViewState["PRODUCTDISTCOUNT"];
                    gvDiscountDetails.DataSource = tblProductDiscountde;
                    gvDiscountDetails.DataBind();

                    DataTable tblProductDiscountCollectde = (DataTable)ViewState["PRODUCTDISCOUNTCOLLECT"];
                    gvDiscountCollect.DataSource = tblProductDiscountCollectde;
                    gvDiscountCollect.DataBind();

                    DataTable tblProductDiscountRelaDe = (DataTable)ViewState["CONTRACTDISTCOUNTRELA"];
                    break;
                case IPC.ACTIONPAGE.APPROVE:
                case IPC.ACTIONPAGE.REVIEW:
                case IPC.ACTIONPAGE.REJECT:
                    DataTable tblProductDiscount = (DataTable)ViewState["PRODUCTDISTCOUNT"];
                    gvDiscountDetails.DataSource = tblProductDiscount;
                    gvDiscountDetails.DataBind();
                    
                    DataTable tblProductDiscountCollect = (DataTable)ViewState["PRODUCTDISCOUNTCOLLECT"];
                    gvDiscountCollect.DataSource = tblProductDiscountCollect;
                    gvDiscountCollect.DataBind();

                    DataTable tblProductDiscountRela = (DataTable)ViewState["CONTRACTDISTCOUNTRELA"];
                    break;

            }
            #endregion

            if (ACTION.Equals(IPC.ACTIONPAGE.REVIEW))
            {
                switch (hdfstatus.Value)
                {
                    case "A":
                    case "I":
                    case "D":
                    case "R":
                        pnlrejectReason.Visible = false;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        break;
                    default:
                        btnApprove.Visible = CheckPermitPageActionPage("CONTRACTPROMOTIONAPP", IPC.ACTIONPAGE.APPROVE);
                        btnReject.Visible = CheckPermitPageActionPage("CONTRACTPROMOTIONAPP", IPC.ACTIONPAGE.REJECT);
                        pnlrejectReason.Visible = CheckPermitPageActionPage("CONTRACTPROMOTIONAPP", IPC.ACTIONPAGE.REJECT);
                        btnApprove.Enabled = true;
                        btnReject.Enabled = true;
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
    protected void ddlFeeType_OnChange(object sender, EventArgs e)
    {

        ViewState["PRODUCTDISTCOUNT"] = null;
        DataTable tblTransDetailsReLoad = (DataTable)ViewState["PRODUCTDISTCOUNT"];
        gvDiscountDetails.DataSource = tblTransDetailsReLoad;
        gvDiscountDetails.DataBind();
        if (tblTransDetailsReLoad != null && tblTransDetailsReLoad.Rows.Count > 0)
        {
            pnGV.Visible = true;
        }
        else
        {
            pnGV.Visible = false;
        }
    }

    protected void gvDiscountDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvDiscountDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void gvDiscountDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblfrom, lblfixedDiscount, lbldiscountrate, lblmaxdiscount, lblDiscountId, lblto;
            LinkButton lbDeleteDiscountdtl;

            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView) e.Row.DataItem;
                lblDiscountId = (Label) e.Row.FindControl("lblDiscountId");
                lblfrom = (Label) e.Row.FindControl("lblfrom");
                lblto = (Label) e.Row.FindControl("lblto");
                lblfixedDiscount = (Label) e.Row.FindControl("lblfixedDiscount");
                lbldiscountrate = (Label) e.Row.FindControl("lbldiscountrate");
                lblmaxdiscount = (Label) e.Row.FindControl("lblmaxdiscount");
                lbDeleteDiscountdtl = (LinkButton) e.Row.FindControl("lbDeleteDiscountdtl");

                lblDiscountId.Text = drv["PromotionID"].ToString();
                lblfrom.Text = Utility.FormatMoney(drv["FromAmt"].ToString(), "LAK");
                lblto.Text = Utility.FormatMoney(drv["ToAmt"].ToString(), "LAK");
                lblfixedDiscount.Text = Utility.FormatMoney(drv["FixAmt"].ToString(), "LAK");
                lbldiscountrate.Text = drv["Rate"].ToString();
                lblmaxdiscount.Text = Utility.FormatMoney(drv["MaxPromotion"].ToString(), "LAK");

                if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                {
                    lbDeleteDiscountdtl.OnClientClick = string.Empty;
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
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable tblProductDiscount = new DataTable();
            DataTable tblTempt = new DataTable();

            if (ViewState["PRODUCTDISTCOUNT"] == null)
            {
                // tạo table tạm chứa TransDetails 
                DataColumn DISCOUNTID = new DataColumn("DiscountId");
                DataColumn FROM = new DataColumn("FromAmt");
                FROM.DataType = typeof(decimal);
                DataColumn TOAMT = new DataColumn("ToAmt");
                DataColumn MINDISCOUNT = new DataColumn("MinDiscount");
                DataColumn MAXDISCOUNT = new DataColumn("MaxDiscount");
                DataColumn DISCOUNTTYPE = new DataColumn("DiscountType");
                DataColumn RATE = new DataColumn("Rate");
                DataColumn FIXED = new DataColumn("FixAmt");
                if (txtFromDiscount.Text == "")
                {
                    txtFromDiscount.Text = "0";
                }
                if (txtRateDetails.Text == "")
                {
                    txtRateDetails.Text = "0";
                }
                if (txtMaxamount.Text == "")
                {
                    txtMaxamount.Text = "0";
                }
                tblProductDiscount.Columns.AddRange(new DataColumn[] { DISCOUNTID, FROM, FIXED, RATE, MAXDISCOUNT, TOAMT, MINDISCOUNT, DISCOUNTTYPE });

                DataRow r = tblProductDiscount.NewRow();
                if (ACTION == IPC.ACTIONPAGE.EDIT)
                {
                    r["DiscountId"] = vDiscountID;
                }
                else
                {
                }
                r["FromAmt"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFromDiscount.Text.Trim());
                r["DiscountId"] = vDiscountID;
                r["FixAmt"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtfixedDiscount.Text.Trim());
                r["Rate"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRateDetails.Text.Trim());
                r["MaxDiscount"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtMaxamount.Text.Trim());
                r["ToAmt"] = "-1";
                r["MinDiscount"] = "0";
                r["DiscountType"] = rdFixed.Checked ? "FIX" : "RATE";
                tblProductDiscount.Rows.Add(r);
                ViewState["PRODUCTDISTCOUNT"] = tblProductDiscount;
            }
            else
            {
                tblProductDiscount = (DataTable)ViewState["PRODUCTDISTCOUNT"];
                tblTempt = (DataTable)ViewState["PRODUCTDISTCOUNT"];
                DataRow r = tblTempt.NewRow();
                string a = "";

                reFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(txtFromDiscount.Text.Trim(), true);

                if (a.Equals("1")) return;
                foreach (DataRow row in tblProductDiscount.Rows)
                {
                    roFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["FromAmt"].ToString(), true);
                    if (ValidateDiscount(rdFixed.Checked, reFromLimit, roFromLimit))
                    {
                        if (ACTION == IPC.ACTIONPAGE.EDIT)
                        {
                            r["DiscountId"] = vDiscountID;
                        }
                        else
                        {
                        }
                        r["FromAmt"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFromDiscount.Text.Trim());
                        r["DiscountId"] = vDiscountID;
                        r["FixAmt"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtfixedDiscount.Text.Trim());
                        r["Rate"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRateDetails.Text.Trim());
                        r["MaxDiscount"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtMaxamount.Text.Trim());
                        r["ToAmt"] = "-1";
                        r["MinDiscount"] = "0";
                        r["DiscountType"] = rdFixed.Checked ? "FIX" : "RATE";
                    }
                    else
                    {
                        a = "1";
                        lblError.Text = Resources.labels.datontaifee;
                        break;
                    }
                }

                if (a != "1")
                {
                    tblTempt.Rows.Add(r);
                    tblProductDiscount = tblTempt;
                    r = tblTempt.NewRow();
                }
                //tblTempt.DefaultView.Sort = "[" + tblTempt.Columns["BILLERID"].ColumnName + "], [" + tblTempt.Columns["FROMLIMIT"].ColumnName + "] ASC";
                ViewState["PRODUCTDISTCOUNT"] = tblTempt;
                tblProductDiscount = (DataTable)ViewState["PRODUCTDISTCOUNT"];
            }
            gvDiscountDetails.DataSource = tblProductDiscount;
            gvDiscountDetails.DataBind();
            if (tblProductDiscount != null && tblProductDiscount.Rows.Count > 0)
            {
                pnGV.Visible = true;
            }
            else
            {
                pnGV.Visible = false;
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
    bool ValidateDiscount(bool rdcheck, double fromnew, double fromold)
    {
        if (txtFromDiscount.Text.Equals(string.Empty) || txtFromDiscount.Text == null)
        {
            lblError.Text = "From discount details not null";
            return false;
        }
        if (fromnew <= fromold)
        {
            lblError.Text = "From discount new  details  than bigger From discount old ";
            return false;
        }
        if (rdcheck)
        {
            if (txtfixedDiscount.Text.Equals(string.Empty) || txtfixedDiscount.Text == null)
            {
                lblError.Text = "Fixed discount amount details not null";
                return false;
            }
        }
        else
        {
            if (txtRateDetails.Text.Equals(string.Empty) || txtRateDetails.Text == null)
            {
                lblError.Text = "Rate details not null";
                return false;
            }
            if (txtMaxamount.Text.Equals(string.Empty) || txtMaxamount.Text == null)
            {
                lblError.Text = "Max amount details not null";
                return false;
            }
        }

        return true;
    }
    protected void ShowWarning()
    {
        try
        {

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
        }
    }

    protected void gvDiscountDetails_OnRowDeletingeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable tblTransDetailsDel = (DataTable)ViewState["PRODUCTDISTCOUNT"];
        Label lblFkID = (Label)gvDiscountDetails.Rows[e.RowIndex].FindControl("lblDiscountId");
        DataRow[] dr = tblTransDetailsDel.Select("DISCOUNTID='" + lblFkID.Text.Trim() + "'");
        tblTransDetailsDel.Rows.Remove(dr[0]);
        gvDiscountDetails.DataSource = tblTransDetailsDel;
        gvDiscountDetails.DataBind();
        if (tblTransDetailsDel.Rows.Count == 0)
        {
            ViewState["PRODUCTDISTCOUNT"] = null;
        }
        else
        {
            //tblTransDetailsDel.DefaultView.Sort = "[" + tblTransDetailsDel.Columns["BILLERID"].ColumnName + "], [" + tblTransDetailsDel.Columns["FROMLIMIT"].ColumnName + "] ASC";
            ViewState["PRODUCTDISTCOUNT"] = tblTransDetailsDel;
        }
        if (tblTransDetailsDel != null && tblTransDetailsDel.Rows.Count > 0)
        {
            pnGV.Visible = true;
        }
        else
        {
            pnGV.Visible = false;
        }
        ShowWarning();
    }

    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        pnDiscountInfo.Enabled = true;
        ViewState["PRODUCTDISTCOUNT"] = null;
        txtcontractno.Text = string.Empty;

        pnGV.Visible = false;
        btnApprove.Enabled = true;
        vDiscountID = "D" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
    }

    protected void cbToLimit_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    #region Load tat ca cac drop downlist
    void LoadddlAll()
    {
        loadradSchedule();
        loadCombobox_WalletLevel();
        loadcombobox_Transaction();
        //loadCombobox_ScheduleType();
        loadCombobox_DiscountType();
        loadCombobox_Status();
        loadtimeDate();
        loadPayside();
        loadShareType();
        loadCombobox_PromotionType();
        loadCombobox_BeneficiarySide();
    }
    private void loadCombobox_BeneficiarySide()
    {
        ddlBeneficiarySide.Items.Insert(0, new ListItem("Sender", "S"));
        ddlBeneficiarySide.Items.Insert(1, new ListItem("Receiver", "R"));
        //ddlBeneficiarySide.Items.Insert(2, new ListItem("Both", "B"));
    }
    private void loadCombobox_PromotionType()
    {
        ddlpromotiontype.Items.Clear();
        ddlpromotiontype.DataBind();
        ddlpromotiontype.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
        //ddlpromotiontype.Items.Insert(1, new ListItem("Discount", "D"));
        ddlpromotiontype.Items.Insert(1, new ListItem("CashBack", "C"));
    }

    private void loadPayside()
    {
        ddlPayside.Items.Insert(0, new ListItem("Bank HO", "HO"));
        ddlPayside.Items.Insert(1, new ListItem("Receiver", "R"));

    }
    private void loadShareType()
    {
        ddlShareType.Items.Insert(0, new ListItem("Fix", "FIX"));
        ddlShareType.Items.Insert(1, new ListItem("Percentage", "PER"));

    }
    private void loadradSchedule()
    {

        radSchedule.Items.Add(new ListItem(Resources.labels.daily, IPC.DAILY));
        radSchedule.Items.Add(new ListItem(Resources.labels.weekly, IPC.WEEKLY));
        radSchedule.Items.Add(new ListItem(Resources.labels.monthly, IPC.MONTHLY));
        radSchedule.Items.Add(new ListItem(Resources.labels.onetime, IPC.ONETIME));
        radSchedule.SelectedIndex = 0;
    }
    private void loadcombobox_Transaction()
    {
        DataSet dsTranApp = new SmartPortal.SEMS.Transactions().LoadTranApp(ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
        }
        DataTable dtTranApp = new DataTable();
        dtTranApp = dsTranApp.Tables[0];

        ddlTransactionType.DataSource = dtTranApp;
        ddlTransactionType.DataTextField = "PAGENAME";
        ddlTransactionType.DataValueField = "TRANCODE";
        ddlTransactionType.DataBind();
        if (ACTION == IPC.ACTIONPAGE.ADD || ACTION == IPC.ACTIONPAGE.DELETE)
        {
        }
        else
        {
            ddlTransactionType.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
        }

    }

    private void loadCombobox_WalletLevel()
    {
        DataSet ds = new DataSet();
        object[] loadContractLevel = new object[] { string.Empty };
        ds = _service.common("SEMS_BO_LST_CON_LV", loadContractLevel, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlContractLevel.DataSource = ds;
                ddlContractLevel.DataValueField = "CONTRACTLEVELID";
                ddlContractLevel.DataTextField = "CONTRACTLEVELNAME";
                ddlContractLevel.DataBind();
            }
        }
    }

    private void loadCombobox_DiscountType()
    {
        ddlPromotionSide.Items.Clear();
        ddlPromotionSide.DataBind();
        if (ACTION == IPC.ACTIONPAGE.ADD || ACTION == IPC.ACTIONPAGE.EDIT)
        {
            ddlPromotionSide.Items.Insert(0, new ListItem("Recevier", "R"));
            ddlPromotionSide.Items.Insert(1, new ListItem("Sender", "S"));
        }
        else
        {
            ddlPromotionSide.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
            ddlPromotionSide.Items.Insert(1, new ListItem("Recevier", "R"));
            ddlPromotionSide.Items.Insert(2, new ListItem("Sender", "S"));
        }

    }
    private void loadCombobox_Status()
    {
        ddlStatus.Items.Clear();
        ddlStatus.DataBind();
        if (ACTION == IPC.ACTIONPAGE.ADD || ACTION == IPC.ACTIONPAGE.EDIT)
        {
            ddlStatus.Items.Insert(0, new ListItem("New", "N"));
            ddlStatus.Items.Insert(1, new ListItem("Active", "A"));
            ddlStatus.Items.Insert(2, new ListItem("Pending", "P"));
            ddlStatus.Items.Insert(3, new ListItem("Deleted", "D"));
            ddlStatus.Items.Insert(4, new ListItem("Rejected", "R"));
            ddlStatus.Items.Insert(5, new ListItem("Inactive", "I"));
            ddlStatus.Items.Insert(6, new ListItem("Pending  For Deleted", "H"));
        }
        else
        {
            ddlStatus.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
            ddlStatus.Items.Insert(1, new ListItem("New", "N"));
            ddlStatus.Items.Insert(2, new ListItem("Active", "A"));
            ddlStatus.Items.Insert(3, new ListItem("Pending", "P"));
            ddlStatus.Items.Insert(4, new ListItem("Deleted", "D"));
            ddlStatus.Items.Insert(5, new ListItem("Rejected", "R"));
            ddlStatus.Items.Insert(6, new ListItem("Inactive", "I"));
            ddlStatus.Items.Insert(7, new ListItem("Pending  For Deleted", "H"));
        }
    }

    private void loadtimeDate()
    {
        for (int i = 1; i < 32; i++)
        {
            ListItem li = new ListItem(i.ToString(), i.ToString());
            cblThuM.Items.Add(li);
            //cblcfM.Items.Add(li);
        }
        //set hour
        for (int j = 0; j < 24; j++)
        {
            ListItem li = new ListItem(j.ToString(), j.ToString());
            ddlHourFrom.Items.Add(li);
            ddlhourDFrom.Items.Add(li);
            ddlhourWFrom.Items.Add(li);
            ddlhourMFrom.Items.Add(li);
        }
        for (int h = 0; h < 24; h++)
        {
            ListItem li = new ListItem(h.ToString(), h.ToString());
            ddlhourWTo.Items.Add(li);
            ddlhourMTo.Items.Add(li);
            ddlhourDTo.Items.Add(li);
            ddlHourTo.Items.Add(li);
        }
        //set minute
        for (int k = 0; k < 60; k += 1)
        {
            ListItem li = new ListItem(k.ToString(), k.ToString());
            ddlMinuteTo.Items.Add(li);
            ddlminuteDTo.Items.Add(li);
            ddlminuteWTo.Items.Add(li);
            ddlminuteMTo.Items.Add(li);
        }
        for (int e = 0; e < 60; e += 1)
        {
            ListItem li = new ListItem(e.ToString(), e.ToString());
            ddlminuteWFrom.Items.Add(li);
            ddlminuteMFrom.Items.Add(li);
            ddlMinuteFrom.Items.Add(li);
            ddlminuteDFrom.Items.Add(li);
        }
    }


    #endregion


    protected void radSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (radSchedule.SelectedValue)
        {
            case IPC.DAILY:
                pnDaily.Visible = true;
                pnWeekly.Visible = false;
                pnMonthly.Visible = false;
                pnOnetime.Visible = false;
                break;
            case IPC.WEEKLY:
                pnDaily.Visible = false;
                pnWeekly.Visible = true;
                pnMonthly.Visible = false;
                pnOnetime.Visible = false;
                break;
            case IPC.MONTHLY:
                pnDaily.Visible = false;
                pnWeekly.Visible = false;
                pnMonthly.Visible = true;
                pnOnetime.Visible = false;
                break;
            case IPC.ONETIME:
                pnDaily.Visible = false;
                pnWeekly.Visible = false;
                pnMonthly.Visible = false;
                pnOnetime.Visible = true;
                break;
            default:
                break;
        }
    }
    #region gvDiscountDetails

    #endregion
    #region discount details radio change
    protected void rdFixed_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rdFixed.Checked)
        {
            txtRateDetails.Enabled = false;
            txtMaxamount.Enabled = false;
            txtRateDetails.Text = string.Empty;
            txtMaxamount.Text = string.Empty;
            txtfixedDiscount.Enabled = true;
        }
        else
        {
            txtRateDetails.Enabled = true;
            txtMaxamount.Enabled = true;
            txtfixedDiscount.Enabled = false;
            txtfixedDiscount.Text = string.Empty;
        }
    }
    protected void rdRate_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rdRate.Checked)
        {
            txtRateDetails.Enabled = true;
            txtMaxamount.Enabled = true;
            txtfixedDiscount.Enabled = false;
            txtfixedDiscount.Text = string.Empty;
        }
        else
        {
            txtfixedDiscount.Enabled = true;
            txtRateDetails.Enabled = false;
            txtMaxamount.Enabled = false;
            txtRateDetails.Text = string.Empty;
            txtMaxamount.Text = string.Empty;
        }
    }
    #endregion
    #region validate Payside 



    #endregion
    protected void loadSechedualType(DataSet ds)
    {
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        DataTable dt1 = new DataTable();
        dt1 = ds.Tables[1];
        switch (dt.Rows[0]["TIMETYPE"].ToString())
        {
            case IPC.DAILY:
                pnDaily.Visible = true;
                pnWeekly.Visible = false;
                pnMonthly.Visible = false;
                pnOnetime.Visible = false;
                txtFromD.Text = Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "dd/MM/yyyy");
                txtToD.Text = Utility.FormatDatetime(dt.Rows[0][IPC.TODATE].ToString(), "dd/MM/yyyy");
                ddlhourDFrom.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[0]).ToString();
                ddlminuteDFrom.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString();
                ddlhourDTo.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[0]).ToString();
				//SmartPortal.Common.Log.WriteLogFile("APPROVEPRODUCTPROMOTION", "TrungTest", "abc", int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString());
                ddlminuteDTo.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString();
                DateTime dtfromd = DateTime.ParseExact(txtFromD.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[0])).AddMinutes(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[1]));
                DateTime dttod = DateTime.ParseExact(txtToD.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[0])).AddMinutes(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]));
                if (dtfromd <= DateTime.Now && DateTime.Now <= dttod && !vIsClone.Equals("true"))
                {
                    DateTime dtfrhour = DateTime.Today
                        .AddHours(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss")
                            .Split(':')[0])).AddMinutes(Double.Parse(Utility
                            .FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[1]));
                    DateTime dttohour = DateTime.Today
                        .AddHours(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss")
                            .Split(':')[0])).AddMinutes(Double.Parse(Utility
                            .FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]));
                    if (dtfrhour <= DateTime.Now && DateTime.Now <= dttohour)
                    {
                        pnDaily.Enabled = false;
                    }
                }
                break;
            case IPC.WEEKLY:
                pnDaily.Visible = false;
                pnWeekly.Visible = true;
                pnMonthly.Visible = false;
                pnOnetime.Visible = false;
                txtFromW.Text = Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "dd/MM/yyyy");
                txtToW.Text = Utility.FormatDatetime(dt.Rows[0][IPC.TODATE].ToString(), "dd/MM/yyyy");
                ddlhourWFrom.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[0]).ToString();
                ddlminuteWFrom.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString();
                ddlhourWTo.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[0]).ToString();
				//SmartPortal.Common.Log.WriteLogFile("APPROVEPRODUCTPROMOTION", "TrungTest", "abc", int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString());
                ddlminuteWTo.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString();

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    int index = int.Parse(dt1.Rows[i]["DAY"].ToString()) - 1;
                    cblThu.Items[index].Selected = true;
                }
                DateTime dtfromdw = DateTime.ParseExact(txtFromW.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[0])).AddMinutes(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[1]));
                DateTime dttodw = DateTime.ParseExact(txtToW.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[0])).AddMinutes(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]));
                DataRow[] ischeck = dt1.Select("Day='" + (int)DateTime.Now.DayOfWeek + "'");
                if (dtfromdw <= DateTime.Now && DateTime.Now <= dttodw && !vIsClone.Equals("true") && ischeck.Length > 0)
                {
                    radSchedule.Enabled = false;
                    pnWeekly.Enabled = false;
                }
                break;
            case IPC.MONTHLY:
                pnDaily.Visible = false;
                pnWeekly.Visible = false;
                pnMonthly.Visible = true;
                pnOnetime.Visible = false;

                txtFromM.Text = Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "dd/MM/yyyy");
                txtToM.Text = Utility.FormatDatetime(dt.Rows[0][IPC.TODATE].ToString(), "dd/MM/yyyy");
                ddlhourMFrom.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[0]).ToString();
                ddlhourMTo.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[0]).ToString();
                ddlminuteMFrom.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString();
                //SmartPortal.Common.Log.WriteLogFile("APPROVEPRODUCTPROMOTION", "TrungTest", "abc", int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString());
				ddlminuteMTo.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString();

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    int index = int.Parse(dt1.Rows[i]["DAY"].ToString()) - 1;
                    cblThuM.Items[index].Selected = true;
                }

                DataRow[] ischeck1 = dt1.Select("Day='" + (int)DateTime.Today.Day + "'");
                DateTime dtfromdm = DateTime.ParseExact(txtFromM.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[0])).AddMinutes(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[1]));
                DateTime dttodm = DateTime.ParseExact(txtToM.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[0])).AddMinutes(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]));
                if (dtfromdm <= DateTime.Now && DateTime.Now <= dttodm && !vIsClone.Equals("true") && ischeck1.Length > 0)
                {
                    radSchedule.Enabled = false;
                    pnMonthly.Enabled = false;
                }
                break;
            case IPC.ONETIME:
                pnDaily.Visible = false;
                pnWeekly.Visible = false;
                pnMonthly.Visible = false;
                pnOnetime.Visible = true;
                txtDateO.Text = Utility.FormatDatetime(dt.Rows[0][IPC.FROMDATE].ToString(), "dd/MM/yyyy");
                ddlHourFrom.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[0]).ToString();
                ddlHourTo.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[0]).ToString();
                ddlMinuteFrom.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString();
                //SmartPortal.Common.Log.WriteLogFile("APPROVEPRODUCTPROMOTION", "TrungTest", "abc", int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString());
				ddlMinuteTo.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString();
                break;
        }
    }
    protected void loadPaySide(DataSet ds)
    {
        DataTable dt = new DataTable();
        if (ds.Tables.Count > 3)
        {
            dt = ds.Tables[3];
            if (ViewState["PRODUCTDISCOUNTCOLLECT"] == null)
            {
                ViewState["PRODUCTDISCOUNTCOLLECT"] = dt;
            }
        }
    }

    protected void btnApprove_OnClick(object sender, EventArgs e)
    {
        try
        {
            string reasonName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRejectReson.Text.Trim());

            DataSet contractTable = new SmartPortal.SEMS.Contract().GetContractByContractNo(txtcontractno.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (contractTable.Tables[0].Rows.Count < 1)
            {
                lblError.Text = "Contract no is not correct";
                return ;
            }

            if (contractTable.Tables[0].Rows[0]["Status"].Equals("D"))
            {
                lblError.Text = "Contract no is deleted";
                return ;
            }

            if (Session["ListApproveContract"] != null)
            {

                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["ListApproveContract"];
                string discountid = string.Empty;
                discountid = lstTran[0].Trim();
                string status = string.Empty;
                switch (hdfstatus.Value)
                {
                    case "P":
                    case "N":
                        status = SmartPortal.Constant.IPC.ACTIVE;
                        break;
                    case "H":
                        status = SmartPortal.Constant.IPC.DELETE;
                        break;
                }
                string username = Session["userName"].ToString();
                new SmartPortal.SEMS.PROMOTION().RejectOrApprovePromotion(discountid, "APPROVE", reasonName, username, ref IPCERRORCODE, ref IPCERRORDESC);
                //xử lý
                if (IPCERRORCODE == "0")
                {
                    lblError.Text = "Approve successfully";
                    //cập nhật lại List
                    SmartPortal.Common.Log.WriteLogDatabase(discountid, Session["userName"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                        Request.UserHostAddress, "EBA_Discount", "Approve ContractDiscount", hdfstatus.Value, status, Session["userName"].ToString(), Session["userName"].ToString());
                    lstTran.RemoveAt(0);
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                if (lstTran.Count != 0)
                {

                    txtRejectReson.Text = string.Empty;
                    Session["ListApproveContract"] = lstTran;
                    //chuyển qua giao dịch kế
                    BindData();
                }
                else
                {
                    Session["ListApproveContract"] = null;
                    txtRejectReson.Enabled = false;
                    goto REDI;
                }
                goto EXIT;
            }
            REDI:
            btnApprove.Visible = false;
            btnReject.Visible = false;
            EXIT:
            ;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnReject_OnClick(object sender, EventArgs e)
    {
        try
        {

            string reasonName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRejectReson.Text.Trim());
            if (reasonName.Equals("") || reasonName == null)
            {
                lblError.Text = Resources.labels.reasonreject;
                return;
            }
            if (Session["ListRejectContract"] != null)
            {

                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["ListRejectContract"];
                string discountid = string.Empty;
                discountid = lstTran[0].Trim();
                string status = string.Empty;
                if (hdfstatus.Value.Equals("H"))
                {
                    status = SmartPortal.Constant.IPC.ACTIVE;
                }
                else
                {
                    status = SmartPortal.Constant.IPC.REJECT;
                }
                string username = Session["userName"].ToString();
                new SmartPortal.SEMS.PROMOTION().RejectOrApprovePromotion(discountid, "REJECT", reasonName, username, ref IPCERRORCODE, ref IPCERRORDESC);
                //xử lý
                if (IPCERRORCODE == "0")
                {
                    lblError.Text = "Reject successfully";
                    //cập nhật lại List
                    SmartPortal.Common.Log.UpdateLogDatabase(discountid, "Reject discount product", Session["userName"].ToString());
                    SmartPortal.Common.Log.WriteLogDatabase(discountid, Session["userName"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                        Request.UserHostAddress, "EBA_Discount", "Reject productdiscount", hdfstatus.Value, SmartPortal.Constant.IPC.REJECT, Session["userName"].ToString(), Session["userName"].ToString());
                    lstTran.RemoveAt(0);
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                if (lstTran.Count != 0)
                {
                    txtRejectReson.Text = string.Empty;
                    Session["contractNo"] = lstTran;
                    //chuyển qua giao dịch kế
                    BindData();
                }
                else
                {
                    Session["ListRejectContract"] = null;
                    txtRejectReson.Enabled = false;
                    goto REDI;
                }
                goto EXIT;
            }
            REDI:
            btnApprove.Visible = false;
            btnReject.Visible = false;
            EXIT:
            ;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }

    protected void gvDiscountCollect_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblFlatAmount, lblPercentage, lblDiscountId, lblPayside, lblShareType, lblPriority;
            LinkButton lbDeleteDiscountdtl;

            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                lblDiscountId = (Label)e.Row.FindControl("lblDiscountId");
                lblPayside = (Label)e.Row.FindControl("lblPayside");
                lblShareType = (Label)e.Row.FindControl("lblShareType");
                lblFlatAmount = (Label)e.Row.FindControl("lblFlatAmount");
                lblPercentage = (Label)e.Row.FindControl("lblPercentage");
                lblPriority = (Label)e.Row.FindControl("lblPriority");
                lbDeleteDiscountdtl = (LinkButton)e.Row.FindControl("lbDeleteDiscountdtl");
                lblDiscountId.Text = drv["PromotionID"].ToString();
                lblPayside.Text = drv["PaySide"].ToString();
                lblShareType.Text = drv["PromotionType"].ToString();
                lblFlatAmount.Text = drv["FixAmt"].ToString();
                lblPercentage.Text = drv["Rate"].ToString();
                lblPriority.Text = drv["Ord"].ToString();
                lblPayside.Text = ddlPayside.Items.FindByValue(drv["PaySide"].ToString()) != null ? ddlPayside.Items.FindByValue(drv["PaySide"].ToString()).ToString() : (drv["PaySide"].ToString());
                lblShareType.Text = ddlShareType.Items.FindByValue(drv["PromotionType"].ToString()) != null ? ddlShareType.Items.FindByValue(drv["PromotionType"].ToString()).ToString() : (drv["PromotionType"].ToString());
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                {
                    lbDeleteDiscountdtl.OnClientClick = string.Empty;
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

    protected void gvDiscountCollect_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    protected void gvDiscountCollect_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        DataTable tblTransDetailsDel = (DataTable)ViewState["PRODUCTDISCOUNTCOLLECT"];
        Label lblFkID = (Label)gvDiscountCollect.Rows[e.RowIndex].FindControl("lblDiscountId");
        DataRow[] dr = tblTransDetailsDel.Select("DISCOUNTID='" + lblFkID.Text.Trim() + "'");
        tblTransDetailsDel.Rows.Remove(dr[0]);
        gvDiscountCollect.DataSource = tblTransDetailsDel;
        gvDiscountCollect.DataBind();
        if (tblTransDetailsDel.Rows.Count == 0)
        {
            ViewState["PRODUCTDISCOUNTCOLLECT"] = null;
        }
        else
        {
            ViewState["PRODUCTDISCOUNTCOLLECT"] = tblTransDetailsDel;
        }
        if (tblTransDetailsDel != null && tblTransDetailsDel.Rows.Count > 0)
        {
            pnGV.Visible = true;
        }
        else
        {
            pnGV.Visible = false;
        }
        ShowWarning();
    }
    public bool CheckPermitPageActionPage(string pageid, string action)
    {
        return PagesBLL.ChekcPermitPageAction(pageid, Session["userName"].ToString(), action);
    }

    protected void ddlpromotiontype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpromotiontype.SelectedValue.ToString().Equals("C"))
        {
            pnPaySide.Visible = false;
        }
        else
        {
            pnPaySide.Visible = true;
        }
    }
}

