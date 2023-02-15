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
using SmartPortal.BLL;
using SmartPortal.Constant;
using SmartPortal.Model;
using SmartPortal.SEMS;
using Newtonsoft.Json;

public partial class Widgets_SEMSContractDiscount_Controls_Widget : WidgetBase
{
    public static bool isAscend = false;
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    public static string APPTRANID;
    public static int Fuid = 1;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    //check validate flag false cho nhap so am, true khong nhap so am
    DataTable ProDeTable = new DataTable();
    double reFromLimit, reToLimit, roToLimit, roFromLimit;
    string vDiscountID
    {
        get { return ViewState["vDiscountID"] != null ? ViewState["vDiscountID"].ToString() : ""; }
        set { ViewState["vDiscountID"] = value; }
    }

    string newVal
    {
        get { return ViewState["newVal"] != null ? ViewState["newVal"].ToString() : ""; }
        set { ViewState["newVal"] = value; }
    }

    string OldVal
    {
        get { return ViewState["OldVal"] != null ? ViewState["OldVal"].ToString() : ""; }
        set { ViewState["OldVal"] = value; }
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
            //txtFromDiscount.Attributes.Add("onkeypress", "executeComma('" + txtFromDiscount.ClientID + "')");
            //txtRateDetails.Attributes.Add("onkeypress", "executeComma('" + txtRateDetails.ClientID + "')");

            txtFromDiscount.Attributes.Add("onkeypress", "executeComma('" + txtFromDiscount.ClientID + "')");
            txtToDiscount.Attributes.Add("onkeypress", "executeComma('" + txtToDiscount.ClientID + "')");
            txtRateDetails.Attributes.Add("onkeypress", "executeComma('" + txtRateDetails.ClientID + "')");
            txtfixedDiscount.Attributes.Add("onkeypress", "executeComma('" + txtfixedDiscount.ClientID + "')");
            txtMaxamount.Attributes.Add("onkeypress", "executeComma('" + txtMaxamount.ClientID + "')");
            txtFlatAmount.Attributes.Add("onkeypress", "executeComma('" + txtFlatAmount.ClientID + "')");


            txtFromDiscount.Attributes.Add("onkeyup", "executeComma('" + txtFromDiscount.ClientID + "')");
            txtToDiscount.Attributes.Add("onkeyup", "executeComma('" + txtToDiscount.ClientID + "')");
            txtRateDetails.Attributes.Add("onkeyup", "executeComma('" + txtRateDetails.ClientID + "')");
            txtfixedDiscount.Attributes.Add("onkeyup", "executeComma('" + txtfixedDiscount.ClientID + "')");
            txtMaxamount.Attributes.Add("onkeyup", "executeComma('" + txtMaxamount.ClientID + "')");
            txtFlatAmount.Attributes.Add("onkeyup", "executeComma('" + txtFlatAmount.ClientID + "')");

            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                LoadddlAll();
                ViewState["CONTRACTPROMOTION"] = null;
                ViewState["CONTRACTPROMOTIONCOLLECT"] = null;

                vDiscountID = "D" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
                
                ddlShareType_OnTextChanged(sender, e);
                ddlPromotionType_SelectedIndexChanged(sender, e);
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
        DataTable tblRoleDefault = new DataTable();
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    radSchedule.SelectedIndex = 0;
                    radSchedule_SelectedIndexChanged(null, null);
                    rdFixed.Checked = true;
                    txtRateDetails.Enabled = false;
                    txtMaxamount.Enabled = false;
                    txtRateDetails.Text = string.Empty;
                    txtMaxamount.Text = string.Empty;
                    txtfixedDiscount.Enabled = true;
                    pnDiscountInfo.Visible = true;
                    btnClear.Enabled = true;
                    rdFixed.Checked = true;
                    btClone.Visible = false;
                    rdFixed_OnCheckedChanged(null, null);
                    break;
                default:
                    btnClear.Enabled = false;
                    #region Lấy thông tin san pham
                    vDiscountID = GetParamsPage(IPC.ID)[0].Trim();
                    vIsClone = GetParamsPage("IsClone")[0].Trim();
                    btClone.Visible = false;
                    rdFixed.Checked = true;
                    rdFixed_OnCheckedChanged(null, null);
                    DataSet dsDistcount = new DataSet();

                    dsDistcount = new SmartPortal.SEMS.PROMOTION().DetailsPromotion(vDiscountID, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dsDistcount.Tables.Count != 0)
                    {
                        ProDeTable = dsDistcount.Tables[0];
                        if (ProDeTable.Rows.Count != 0)
                        {
                            //bind data to panel
                            txtContractNo.Text = ProDeTable.Rows[0]["ObjectID"].ToString();
                            ddlTransactionType.SelectedValue = ProDeTable.Rows[0]["TranCode"].ToString();
                            ddlPromotionSide.SelectedValue = ProDeTable.Rows[0]["BaseType"].ToString();
                            ddlContractLevel.SelectedValue = ProDeTable.Rows[0]["ContractLevel"].ToString();
                            ddlStatus.SelectedValue = ProDeTable.Rows[0]["Status"].ToString();
                            radSchedule.SelectedValue = ProDeTable.Rows[0]["TimeType"].ToString();
                            ddlPromotionType.SelectedValue = ProDeTable.Rows[0]["PromotionType"].ToString();
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
                            DataTable FormulaTable = dsDistcount.Tables[5];
                            DataTable FormulaSubTable = dsDistcount.Tables[6];
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

                            if (dsDistcount.Tables.Count > 2)
                            {
                                loadSechedualType(dsDistcount);
                            }
                            loadPaySide(dsDistcount);
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

                    if (dsDistcount.Tables.Count > 2)
                    {
                        if (ViewState["CONTRACTPROMOTION"] == null)
                        {
                            ViewState["CONTRACTPROMOTION"] = dsDistcount.Tables[2];
                        }
                    }
                    if (dsDistcount.Tables.Count > 3)
                    {
                        if (ViewState["CONTRACTDISTCOUNTRELA"] == null)
                        {
                            ViewState["CONTRACTDISTCOUNTRELA"] = dsDistcount.Tables[4];
                        }
                    }
                    //lay du lieu chua thay doi
                    if (string.IsNullOrEmpty(OldVal))
                    {
                        for (int i = 0; i < dsDistcount.Tables.Count; i++)
                        {
                            OldVal += ConvertDataTabletoString(dsDistcount.Tables[i]) + " | ";
                        }
                    }
                    #endregion
                    break;
            }
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    txtContractNo.Enabled = false;

                    btback.Visible = true;
                    btsave.Enabled = false;

                    DataTable tblProductDiscountde = (DataTable)ViewState["CONTRACTPROMOTION"];
                    gvDiscountDetails.DataSource = tblProductDiscountde;
                    gvDiscountDetails.DataBind();
                    DataTable tblProductDiscountCollectde = (DataTable)ViewState["CONTRACTPROMOTIONCOLLECT"];
                    gvDiscountCollect.DataSource = tblProductDiscountCollectde;
                    gvDiscountCollect.DataBind();
                    //DataTable tblProductDiscountRelaDe = (DataTable)ViewState["CONTRACTDISTCOUNTRELA"];
                    //gvListSubMerchantContract.DataSource = tblProductDiscountRelaDe;
                    //gvListSubMerchantContract.DataBind();
                    //gvListSubMerchantContract.Enabled = false;

                    //txtContractNo_OnTextChanged(null, null);
                    pnPaySide.Enabled = false;
                    pnDiscountDetail.Enabled = false;
                    pnDiscountInfo.Enabled = false;
                    pnOnetime.Enabled = false;
                    pnMonthly.Enabled = false;
                    pnWeekly.Enabled = false;
                    pnDaily.Enabled = false;
                    pnGV.Enabled = false;
                    btback.Enabled = true;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtContractNo.Enabled = true;

                    if (!vIsClone.Equals("true"))
                    {
                        txtContractNo.Enabled = false;
                        ddlPromotionSide.Enabled = false;
                        ddlTransactionType.Enabled = false;
                        ddlContractLevel.Enabled = false;
                        ddlContractLevel.Enabled = false;
                        txtdiscountdes.Enabled = true;
                    }

                    btback.Visible = true;
                    btsave.Enabled = true;

                    DataTable tblProductDiscount = (DataTable)ViewState["CONTRACTPROMOTION"];
                    gvDiscountDetails.DataSource = tblProductDiscount;
                    gvDiscountDetails.DataBind();

                    DataTable tblProductDiscountCollect = (DataTable)ViewState["CONTRACTPROMOTIONCOLLECT"];
                    gvDiscountCollect.DataSource = tblProductDiscountCollect;
                    gvDiscountCollect.DataBind();


                    DataTable tblProductDiscountRela= (DataTable)ViewState["CONTRACTDISTCOUNTRELA"];
                    //gvListSubMerchantContract.DataSource = tblProductDiscountRela;
                    //gvListSubMerchantContract.DataBind();
                    //txtContractNo_OnTextChanged(null, null);


                    btback.Enabled = true;
                    break;
            }
            #endregion
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

        ViewState["CONTRACTPROMOTION"] = null;
        DataTable tblTransDetailsReLoad = (DataTable)ViewState["CONTRACTPROMOTION"];
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
        loadcombobox_Transaction();
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
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            newVal = "";
            if (vIsClone.Equals("true"))
            {
                vDiscountID = "D" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
            }
            string fixamount = "0";

            if (ViewState["CONTRACTPROMOTION"] == null)
            {
                lblError.Text = "You must add Promotion Details";
                return;
            }
            if (ViewState["CONTRACTPROMOTIONCOLLECT"] == null)
            {
                lblError.Text = "You must add Pay side";
                return;
            }


            if (!ValidateSave())
            {
                return;
            }
            string feetype = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text);
            string chargelater = SmartPortal.Constant.IPC.NEW;
            string useraction = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString());
            string dateaction = SmartPortal.Common.Utilities.Utility.KillSqlInjection(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            DataTable tableFeeDetails = new DataTable();
            if (ViewState["CONTRACTPROMOTION"] != null)
            {
                tableFeeDetails = (DataTable)ViewState["CONTRACTPROMOTION"];
                DataColumnCollection columns = tableFeeDetails.Columns;
                if (columns.Contains("FkID"))
                {
                    tableFeeDetails.Columns.Remove("FkID");
                }
            }
            #region tao tham so discount 

            string discountid = vDiscountID;
            string discountname = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtPromotionName.Text);
            string discounttype = ddlPromotionSide.SelectedValue;
            string objectid = txtContractNo.Text.Trim();
            string promotiontype = ddlPromotionType.SelectedValue;
            string objecttype = "C";
            string contractlevel = ddlContractLevel.SelectedValue;
            string trancode = ddlTransactionType.SelectedValue;
            string currency = "LAK";
            //string fomular = FormulaDevExpress.Value;
            string collectistier = "0";
            string istier = "1";
            string fixamt = "0";
            string des = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtdiscountdes.Text);
            string status = "N";
            string usecount = txtUseCount.Text;
            string beneficiary = ddlBeneficiarySide.SelectedValue;
            if (String.IsNullOrEmpty(usecount))
            {
                usecount = "-1";
            }
            else
            {
                if (int.Parse(usecount) <= 0)
                {
                    lblError.Text = "Use Count must be greater than 0";
                    return;
                }
            }
            if (String.IsNullOrEmpty(discountname))
            {
                lblError.Text = "Promotion Name be not null";
                return;
            }
            //radSchedule.SelectedValue;
            #endregion
            #region tao discount day
            //Tạo table ScheduleDay
            DataTable dtDiscountDay = new DataTable();
            DataColumn DiscountDayIDCol = new DataColumn("DiscountID");
            DataColumn DayNoCol = new DataColumn("Day");
            //add col vào ScheduleDay
            dtDiscountDay.Columns.AddRange(new DataColumn[] { DiscountDayIDCol, DayNoCol });
            #endregion
            #region tao tham so time cua discount
            string fromdate = string.Empty;
            string todate = string.Empty;
            string totime = string.Empty;
            string fromtime = string.Empty;
            string timetype = string.Empty;
            switch (radSchedule.SelectedValue)
            {
                case IPC.DAILY:
                    fromdate = txtFromD.Text;
                    todate = txtToD.Text;
                    fromtime = (int.Parse(ddlhourDFrom.SelectedValue.ToString()) < 10 ? "0" + ddlhourDFrom.SelectedValue.ToString() : ddlhourDFrom.SelectedValue.ToString()) + ":" + (int.Parse(ddlminuteDFrom.SelectedValue.ToString()) < 10 ? "0" + ddlminuteDFrom.SelectedValue.ToString() : ddlminuteDFrom.SelectedValue.ToString()) + ":00";
                    totime = (int.Parse(ddlhourDTo.SelectedValue.ToString()) < 10 ? "0" + ddlhourDTo.SelectedValue.ToString() : ddlhourDTo.SelectedValue.ToString()) + ":" + (int.Parse(ddlminuteDTo.SelectedValue.ToString()) < 10 ? "0" + ddlminuteDTo.SelectedValue.ToString() : ddlminuteDTo.SelectedValue.ToString()) + ":00";
                    break;
                case IPC.WEEKLY:
                    for (int i = 0; i < cblThu.Items.Count; i++)
                    {
                        if (cblThu.Items[i].Selected)
                        {
                            DataRow row1 = dtDiscountDay.NewRow();
                            row1["DiscountID"] = discountid;
                            row1["Day"] = cblThu.Items[i].Value.ToString();
                            dtDiscountDay.Rows.Add(row1);
                        }
                    }
                    fromdate = txtFromW.Text;
                    todate = txtToW.Text;
                    fromtime = (int.Parse(ddlhourWFrom.SelectedValue.ToString()) < 10 ? "0" + ddlhourWFrom.SelectedValue.ToString() : ddlhourWFrom.SelectedValue.ToString()) + ":" + (int.Parse(ddlminuteWFrom.SelectedValue.ToString()) < 10 ? "0" + ddlminuteWFrom.SelectedValue.ToString() : ddlminuteWFrom.SelectedValue.ToString()) + ":00";
                    totime = (int.Parse(ddlhourWTo.SelectedValue.ToString()) < 10 ? "0" + ddlhourWTo.SelectedValue.ToString() : ddlhourWTo.SelectedValue.ToString()) + ":" + (int.Parse(ddlminuteWTo.SelectedValue.ToString()) < 10 ? "0" + ddlminuteWTo.SelectedValue.ToString() : ddlminuteWTo.SelectedValue.ToString()) + ":00";
                    break;
                case IPC.MONTHLY:
                    for (int j = 0; j < cblThuM.Items.Count; j++)
                    {
                        if (cblThuM.Items[j].Selected)
                        {
                            DataRow row2 = dtDiscountDay.NewRow();
                            row2["DiscountID"] = discountid;
                            row2["Day"] = cblThuM.Items[j].Value.ToString();
                            dtDiscountDay.Rows.Add(row2);
                        }
                    }
                    fromdate = txtFromM.Text;
                    todate = txtToM.Text;
                    fromtime = /*fromdate + " " +*/ (int.Parse(ddlhourMFrom.SelectedValue.ToString()) < 10 ? "0" + ddlhourMFrom.SelectedValue.ToString() : ddlhourMFrom.SelectedValue.ToString()) + ":" + (int.Parse(ddlminuteMFrom.SelectedValue.ToString()) < 10 ? "0" + ddlminuteMFrom.SelectedValue.ToString() : ddlminuteMFrom.SelectedValue.ToString()) + ":00";
                    totime = /*fromdate + " " +*/ (int.Parse(ddlhourMTo.SelectedValue.ToString()) < 10 ? "0" + ddlhourMTo.SelectedValue.ToString() : ddlhourMTo.SelectedValue.ToString()) + ":" + (int.Parse(ddlminuteMTo.SelectedValue.ToString()) < 10 ? "0" + ddlminuteMTo.SelectedValue.ToString() : ddlminuteMTo.SelectedValue.ToString()) + ":00";
                    break;
                case IPC.ONETIME:
                    fromdate = txtDateO.Text;
                    todate = txtDateO.Text;
                    fromtime = (int.Parse(ddlHourFrom.SelectedValue.ToString()) < 10 ? "0" + ddlHourFrom.SelectedValue.ToString() : ddlHourFrom.SelectedValue.ToString()) + ":" + (int.Parse(ddlMinuteFrom.SelectedValue.ToString()) < 10 ? "0" + ddlMinuteFrom.SelectedValue.ToString() : ddlMinuteFrom.SelectedValue.ToString()) + ":00";
                    totime = (int.Parse(ddlHourTo.SelectedValue.ToString()) < 10 ? "0" + ddlHourTo.SelectedValue.ToString() : ddlHourTo.SelectedValue.ToString()) + ":" + (int.Parse(ddlMinuteTo.SelectedValue.ToString()) < 10 ? "0" + ddlMinuteTo.SelectedValue.ToString() : ddlMinuteTo.SelectedValue.ToString()) + ":00";
                    break;
            }


            timetype = radSchedule.SelectedValue;
            #endregion
            #region HASINPUT

            if (ACTION.Equals(IPC.ACTIONPAGE.EDIT))
            {
                if (ddlStatus.SelectedValue.Equals("N"))
                {
                    status = "N";
                }
                else
                {
                    status = "P";
                }
            }
            if (vIsClone == "true")
            {
                status = "N";
            }

            Hashtable hasInput = new Hashtable();
            object[] insertDiscount = new object[2];
            object[] updateDiscount = new object[2];
            insertDiscount[0] = "EBA_PROMOTION_INSERT";
            updateDiscount[0] = "EBA_PROMOTION_UPDATE";
            //tao bang chua thong tin discount
            DataTable tableDiscount = new DataTable();
            DataColumn colDiscountID = new DataColumn("colDiscountID");
            DataColumn colDiscountName = new DataColumn("colDiscountName");
            DataColumn colBaseType = new DataColumn("colBaseType");
            DataColumn colFromDate = new DataColumn("colFromDate");
            DataColumn colToDate = new DataColumn("colToDate");
            DataColumn colFromTime = new DataColumn("colFromTime");
            DataColumn colToTime = new DataColumn("colToTime");
            DataColumn colTimeType = new DataColumn("colTimeType");
            DataColumn colObjectID = new DataColumn("colObjectID");
            DataColumn colObjectType = new DataColumn("colObjectType");
            DataColumn colContractLevel = new DataColumn("colContractLevel");
            DataColumn colTranCode = new DataColumn("colTranCode");
            DataColumn colCurrency = new DataColumn("colCurrency");
            //DataColumn colFormula = new DataColumn("colFormula");
            DataColumn colCollectionIsTier = new DataColumn("colCollectionIsTier");
            DataColumn colIsTier = new DataColumn("colIsTier");
            DataColumn colFixAmt = new DataColumn("colFixAmt");
            DataColumn colDescription = new DataColumn("colDescription");
            DataColumn colStatus = new DataColumn("colStatus");
            DataColumn colCreatedUser = new DataColumn("colCreatedUser");
            DataColumn colPromotionType = new DataColumn("colPromotionType");
            DataColumn colUseCount = new DataColumn("colUseCount");
            DataColumn colBeneficiarySide = new DataColumn("colBeneficiarySide");



            //add table DiscountRela

            //DataTable tableDiscountRelation = new DataTable();
            //DataColumn colRelaDiscountID = new DataColumn("colRelaDiscountID");
            //DataColumn colRelaObjectID = new DataColumn("colRelaObjectID");
            //DataColumn colContractNo = new DataColumn("colContractNo");
            //DataColumn colRelaStatus = new DataColumn("colRelaStatus");

            //CheckBox cbxCheckSub;
            //Label lblContractNo;
            //tableDiscountRelation.Columns.Add(colRelaDiscountID);
            //tableDiscountRelation.Columns.Add(colRelaObjectID);
            //tableDiscountRelation.Columns.Add(colContractNo);
            //tableDiscountRelation.Columns.Add(colRelaStatus);
            //foreach (GridViewRow gvr in gvListSubMerchantContract.Rows)
            //{
            //    cbxCheckSub = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
            //    if (cbxCheckSub.Checked == true)
            //    {
            //        lblContractNo = (Label)gvr.Cells[1].FindControl("lblContractNo");
            //        DataRow rowRela = tableDiscountRelation.NewRow();
            //        rowRela["colRelaDiscountID"] = vDiscountID;
            //        rowRela["colRelaObjectID"] = lblContractNo.Text.Trim();
            //        rowRela["colContractNo"] = objectid.Trim();
            //        rowRela["colRelaStatus"] = status;
            //        tableDiscountRelation.Rows.Add(rowRela);
            //    }
            //    else
            //    {
            //        lblContractNo = (Label)gvr.Cells[1].FindControl("lblContractNo");
            //        DataRow rowRela = tableDiscountRelation.NewRow();
            //        rowRela["colRelaDiscountID"] = vDiscountID;
            //        rowRela["colRelaObjectID"] = lblContractNo.Text.Trim();
            //        rowRela["colContractNo"] = objectid.Trim();
            //        rowRela["colRelaStatus"] = "D";
            //        tableDiscountRelation.Rows.Add(rowRela);
            //    }
            //}
            //object[] deleteDiscountRela = new object[2];
            //object[] insertDiscountRela = new object[2];
            //deleteDiscountRela[0] = "EBA_PROMOTIONRELA_DELETE";
            //deleteDiscountRela[1] = tableDiscountRelation;

            //insertDiscountRela[0] = "EBA_PROMOTIONRELA_INSERT";
            //insertDiscountRela[1] = tableDiscountRelation;
            //hasInput.Add("DELETEPROMOTIONRELA", deleteDiscountRela);
            //hasInput.Add("INSERTPROMOTIONRELA", insertDiscountRela);

            //string tabledisrela = ConvertDataTabletoString(tableDiscountRelation);
            //newVal += tabledisrela + "|";
            //add vào table product
            tableDiscount.Columns.Add(colDiscountID);
            tableDiscount.Columns.Add(colDiscountName);
            tableDiscount.Columns.Add(colBaseType);
            tableDiscount.Columns.Add(colUseCount);
            tableDiscount.Columns.Add(colFromDate);
            tableDiscount.Columns.Add(colToDate);
            tableDiscount.Columns.Add(colFromTime);
            tableDiscount.Columns.Add(colToTime);
            tableDiscount.Columns.Add(colTimeType);
            tableDiscount.Columns.Add(colObjectID);
            tableDiscount.Columns.Add(colObjectType);
            tableDiscount.Columns.Add(colContractLevel);
            tableDiscount.Columns.Add(colTranCode);
            tableDiscount.Columns.Add(colCurrency);
            //tableDiscount.Columns.Add(colFormula);
            tableDiscount.Columns.Add(colCollectionIsTier);
            tableDiscount.Columns.Add(colIsTier);
            tableDiscount.Columns.Add(colFixAmt);
            tableDiscount.Columns.Add(colDescription);
            tableDiscount.Columns.Add(colStatus);
            tableDiscount.Columns.Add(colPromotionType);



            //tao 1 dong du lieu
            DataRow row = tableDiscount.NewRow();
            row["colDiscountID"] = vDiscountID;
            row["colDiscountName"] = discountname;
            row["colBaseType"] = discounttype;
            row["colUseCount"] = usecount;
            row["colFromDate"] = fromdate;
            row["colToDate"] = todate;
            row["colFromTime"] = fromtime;
            row["colToTime"] = totime;
            row["colTimeType"] = timetype;
            row["colObjectID"] = objectid;
            row["colObjectType"] = objecttype;
            row["colContractLevel"] = contractlevel;
            row["colTranCode"] = trancode;
            row["colCurrency"] = currency;
            //row["colFormula"] = fomular;
            row["colPromotionType"] = promotiontype;
            row["colCollectionIsTier"] = collectistier;
            row["colIsTier"] = istier;
            row["colFixAmt"] = fixamt;
            row["colDescription"] = des;

            row["colStatus"] = status;
            if (ACTION.Equals(IPC.ACTIONPAGE.ADD) || vIsClone.Equals("true"))
            {
                tableDiscount.Columns.Add(colCreatedUser);
                row["colCreatedUser"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString());
            }
            tableDiscount.Columns.Add(colBeneficiarySide);
            row["colBeneficiarySide"] = beneficiary;
            
            tableDiscount.Rows.Add(row);

            //add vao phan tu thu 2 mang object
            insertDiscount[1] = tableDiscount;
            updateDiscount[1] = tableDiscount;

            hasInput.Add("INSERTPROMOTION", insertDiscount);
            hasInput.Add("UPDATEPROMOTION", updateDiscount);


            string tableDiscountstr = ConvertDataTabletoString(tableDiscount);
            newVal += tableDiscountstr + "|";

            #endregion

            #region Insert Product Discount
            object[] inserDiscountdetail = new object[2];
            object[] updateDiscountdetail = new object[2];
            inserDiscountdetail[0] = "EBA_PROMOTIONDETAILS_INSERT";
            updateDiscountdetail[0] = "EBA_PROMOTIONDETAILS_UPDATE";

            //add vao phan tu thu 2 mang object
            if (vIsClone == "true")
            {
                DataTable tblDiscountClone = (DataTable)ViewState["CONTRACTPROMOTION"];
                foreach (DataRow dtRow in tblDiscountClone.Rows)
                {
                    // On all tables' columns
                    dtRow["PromotionID"] = vDiscountID;
                }
                inserDiscountdetail[1] = tblDiscountClone;
                updateDiscountdetail[1] = tblDiscountClone;
            }
            else
            {
                inserDiscountdetail[1] = ViewState["CONTRACTPROMOTION"];
                updateDiscountdetail[1] = ViewState["CONTRACTPROMOTION"];

            }

            hasInput.Add("INSERTPROMOTIONDETAILS", inserDiscountdetail);
            hasInput.Add("UPDATEPROMOTIONDETAILS", updateDiscountdetail);
            //add discount day

            string Discountdetailstr = ConvertDataTabletoString((DataTable)ViewState["CONTRACTPROMOTION"]);
            newVal += Discountdetailstr + "|";

            object[] insertDiscountday = new object[2];
            object[] updateDiscountday = new object[2];
            insertDiscountday[0] = "EBA_PROMOTIONDAY_INSERT";
            updateDiscountday[0] = "EBA_PROMOTIONDAY_UPDATE";

            //add vao phan tu thu 2 mang object
            insertDiscountday[1] = dtDiscountDay;
            updateDiscountday[1] = dtDiscountDay;
            hasInput.Add("INSERTPROMOTIONDAY", insertDiscountday);
            hasInput.Add("UPDATEROMOTIONDAY", updateDiscountday);

            string dtDiscountDaystr = ConvertDataTabletoString(dtDiscountDay);
            newVal += dtDiscountDaystr + "|";



            object[] deleteDiscount = new object[2];
            object[] deleteDiscountdetail = new object[2];
            object[] deleteDiscountday = new object[2];
            object[] deleteDiscountconnect = new object[2];
            deleteDiscount[0] = "EBA_PROMOTION_DELETE";
            deleteDiscountdetail[0] = "EBA_PROMOTIONDETAILS_DELETE";
            deleteDiscountday[0] = "EBA_DISCOUNTDAY_DELETE";
            deleteDiscountconnect[0] = "EBA_DISCOUNTCONNECT_DELETE";


            DataTable tblDelDiscount = new DataTable();
            DataColumn coldelDISCOUNTID = new DataColumn("DISCOUNTID");
            DataColumn coldelSTATUS = new DataColumn("STATUS");

            DataTable userTable = new DataTable();
            //add vào table
            tblDelDiscount.Columns.Add(coldelDISCOUNTID);
            tblDelDiscount.Columns.Add(coldelSTATUS);
            DataRow row3 = tblDelDiscount.NewRow();
            row3["DISCOUNTID"] = vDiscountID;
            row3["STATUS"] = "P";
            tblDelDiscount.Rows.Add(row3);
            deleteDiscountdetail[1] = tblDelDiscount;
            deleteDiscount[1] = tblDelDiscount;
            hasInput.Add("DELETEPROMOTION", deleteDiscount);
            hasInput.Add("DELETEPROMOTIONDETAILS", deleteDiscountdetail);

            //TAO BANG Chua discountconnection
            object[] insertDiscountConnection = new object[2];
            object[] updateDiscountConnection = new object[2];
            insertDiscountConnection[0] = "EBA_PROMOTIONCONNECT_INSERT";
            updateDiscountConnection[0] = "EBA_PROMOTIONCONNECT_UPDATE";
            DataTable tableDiscountconnect = new DataTable();
            tableDiscountconnect = (DataTable)ViewState["CONTRACTPROMOTIONCOLLECT"];
            foreach(DataRow r in tableDiscountconnect.Rows)
            {
                r["PromotionID"] = vDiscountID;
            }
            insertDiscountConnection[1] = tableDiscountconnect;
            updateDiscountConnection[1] = tableDiscountconnect;
            hasInput.Add("PROMOTIONCONNECT", insertDiscountConnection);
            hasInput.Add("PROMOTIONCONNECTUPDATE", updateDiscountConnection);



            string tableDiscountconnectstr = ConvertDataTabletoString(tableDiscountconnect);
            newVal += tableDiscountconnectstr + "|";
            #endregion

            //SubFormular
            DataTable subTblFormular = new DataTable();
            subTblFormular = FormulaDevExpress.dtFieldName;
            string jsonarray = JsonConvert.SerializeObject(subTblFormular);
            //Formular
            DataTable tableFormular = new DataTable();
            DataColumn colFormulaName = new DataColumn("colFormulaName");
            DataColumn colFormulaExpress = new DataColumn("colFormulaExpress");
            DataColumn colFromulaDesc = new DataColumn("colFromolaDesc");
            DataColumn colFormulaStatus = new DataColumn("colFormulaStatus");
            DataColumn colFormulaUserCreate = new DataColumn("colFormulaUserCreate");
            DataColumn colPromotionIDfl = new DataColumn("colPromotionID");
            DataColumn colFormulaSub = new DataColumn("colFormulaSub");
            DataColumn colFormulaID = new DataColumn("colFormulaID");
            tableFormular.Columns.Add(colFormulaName);
            tableFormular.Columns.Add(colFormulaExpress);
            tableFormular.Columns.Add(colFromulaDesc);
            tableFormular.Columns.Add(colFormulaStatus);
            tableFormular.Columns.Add(colFormulaUserCreate);


            DataRow rowfl = tableFormular.NewRow();
            string formulaname = ((TextBox)FormulaDevExpress.FindControl("txtname")).Text.ToString();
            string formulaexpress = FormulaDevExpress.Value;
            string formuladess = ((TextBox)FormulaDevExpress.FindControl("txtname")).Text.ToString();
            string formulastatus = "A";
            
            string formularusercreate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString());
            rowfl["colFormulaName"] = formulaname;
            rowfl["colFormulaExpress"] = formulaexpress;
            rowfl["colFromolaDesc"] = formuladess;
            rowfl["colFormulaStatus"] = formulastatus;
            rowfl["colFormulaUserCreate"] = formularusercreate;
            tableFormular.Columns.Add(colPromotionIDfl);
            rowfl["colPromotionID"] = vDiscountID;
            
            if (jsonarray != "null")
            {
                tableFormular.Columns.Add(colFormulaSub);
                rowfl["colFormulaSub"] = jsonarray;
            }
            else
            {
                tableFormular.Columns.Add(colFormulaSub);
                rowfl["colFormulaSub"] = string.Empty;
            }
            tableFormular.Rows.Add(rowfl);
            object[] insertformula = new object[2];
            object[] updateformula = new object[2];
            insertformula[0] = "SEMS_FORMULAR_INSERT";
            updateformula[0] = "SEMS_FORMULAR_UPDATE";
            insertformula[1] = tableFormular;
            updateformula[1] = tableFormular;
            hasInput.Add("FORMULA", insertformula);
            hasInput.Add("FORMULAUPDATE", updateformula);

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    new PROMOTION().InsertPromotion(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.thempromotioncontractthanhcong;
                        pnDiscountInfo.Enabled = false;
                        pnOnetime.Enabled = false;
                        pnDaily.Enabled = false;
                        pnWeekly.Enabled = false;
                        pnMonthly.Enabled = false;
                        pnDiscountDetail.Enabled = false;
                        pnPaySide.Enabled = false;
                        btsave.Enabled = false;
                        btClone.Visible = true;
                        //log
                        SmartPortal.Common.Log.WriteLogDatabase(Session["userName"].ToString(), System.Configuration.ConfigurationManager.AppSettings["LOGIN"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], "AddContractDiscount", OldVal, newVal);
                        // end log

                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;

                    if (vIsClone.Equals("true"))
                    {
                        new PROMOTION().InsertPromotion(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    else
                    {
                        new PROMOTION().UpdatePromotion(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    if (IPCERRORCODE == "0")
                    {
                        if (vIsClone.Equals("true"))
                        {
                            //log
                            SmartPortal.Common.Log.WriteLogDatabase(Session["userName"].ToString(), System.Configuration.ConfigurationManager.AppSettings["LOGIN"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], "CloneContractDiscount", OldVal, newVal);
                            // end log
                            lblError.Text = Resources.labels.clonepromotioncontractthanhcong;
                        }
                        else
                        {
                            //log
                            SmartPortal.Common.Log.WriteLogDatabase(Session["userName"].ToString(), System.Configuration.ConfigurationManager.AppSettings["LOGIN"], Request.Url.ToString(), Session["userName"].ToString(), Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLUSER"], "EditContractDiscount", OldVal, newVal);
                            // end log
                            lblError.Text = Resources.labels.suapromotioncontractthanhcong;
                        }
                        pnDiscountInfo.Enabled = false;
                        pnOnetime.Enabled = false;
                        pnDaily.Enabled = false;
                        pnWeekly.Enabled = false;
                        pnMonthly.Enabled = false;
                        pnDiscountDetail.Enabled = false;
                        pnPaySide.Enabled = false;
                        btsave.Enabled = false;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
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
                drv = (DataRowView)e.Row.DataItem;
                lblDiscountId = (Label)e.Row.FindControl("lblDiscountId");
                lblfrom = (Label)e.Row.FindControl("lblfrom");
                lblto = (Label)e.Row.FindControl("lblto");
                lblfixedDiscount = (Label)e.Row.FindControl("lblfixedDiscount");
                lbldiscountrate = (Label)e.Row.FindControl("lbldiscountrate");
                lblmaxdiscount = (Label)e.Row.FindControl("lblmaxdiscount");
                lbDeleteDiscountdtl = (LinkButton)e.Row.FindControl("lbDeleteDiscountdtl");

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
        double fromdiscount = SmartPortal.Common.Utilities.Utility.isDouble(txtFromDiscount.Text.Trim(), true);
        double todiscount = SmartPortal.Common.Utilities.Utility.isDouble(txtToDiscount.Text.Trim(), true);
        try
        {
            DataTable tblProductDiscount = new DataTable();
            DataTable tblTempt = new DataTable();
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
            if (txtfixedDiscount.Text == "")
            {
                txtfixedDiscount.Text = "0";
            }

            if (ViewState["CONTRACTPROMOTION"] == null)
            {
                // tạo table tạm chứa TransDetails 
                DataColumn DISCOUNTID = new DataColumn("PromotionID");
                DataColumn FROM = new DataColumn("FromAmt");
                FROM.DataType = typeof(decimal);
                DataColumn TOAMT = new DataColumn("ToAmt");
                DataColumn MINDISCOUNT = new DataColumn("MinPromotion");
                DataColumn MAXDISCOUNT = new DataColumn("MaxPromotion");
                DataColumn DISCOUNTTYPE = new DataColumn("PromotionType");
                DataColumn RATE = new DataColumn("Rate");
                DataColumn FIXED = new DataColumn("FixAmt");

                tblProductDiscount.Columns.AddRange(new DataColumn[] { DISCOUNTID, FROM, FIXED, RATE, MAXDISCOUNT, TOAMT, MINDISCOUNT, DISCOUNTTYPE });

                if (rdFixed.Checked && txtfixedDiscount.Text == "0")
                {
                    lblError.Text = Resources.labels.Fixeddiscountamountnotnull;
                    return;
                }
                if (rdRate.Checked && txtRateDetails.Text == "0")
                {
                    lblError.Text = Resources.labels.Ratenotnull;
                    return;
                }
                if (rdRate.Checked && txtMaxamount.Text == "0")
                {
                    lblError.Text = Resources.labels.Maxdiscountamountnotnull;
                    return;
                }
                if (fromdiscount >= todiscount)
                {
                    lblError.Text = "To discount greater than From discount";
                    return;
                }
                DataRow r = tblProductDiscount.NewRow();
                if (ACTION == IPC.ACTIONPAGE.EDIT)
                {
                    r["PromotionID"] = vDiscountID;
                }
                else
                {
                }
                
                r["PromotionID"] = vDiscountID;
                r["FixAmt"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtfixedDiscount.Text.Trim());
                r["Rate"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRateDetails.Text.Trim());
                r["MaxPromotion"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtMaxamount.Text.Trim());
                r["ToAmt"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtToDiscount.Text.Trim());
                r["MinPromotion"] = "0";
                r["PromotionType"] = rdFixed.Checked ? "FIX" : "PER";
                r["FromAmt"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFromDiscount.Text.ToString().Trim());

                tblProductDiscount.Rows.Add(r);

                ViewState["CONTRACTPROMOTION"] = tblProductDiscount;
            }
            else
            {
                tblProductDiscount = (DataTable)ViewState["CONTRACTPROMOTION"];
                tblTempt = (DataTable)ViewState["CONTRACTPROMOTION"];
                DataRow r = tblTempt.NewRow();
                string a = "";
                if (fromdiscount >= todiscount)
                {
                    lblError.Text = "To discount greater than From discount";
                    return;
                }
                reFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(txtFromDiscount.Text.Trim(), true);
                reToLimit = SmartPortal.Common.Utilities.Utility.isDouble(txtToDiscount.Text.Trim(), true);
                if (a.Equals("1")) return;
                foreach (DataRow row in tblProductDiscount.Rows)
                {
                    roFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["FromAmt"].ToString(), true);
                    roToLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["ToAmt"].ToString(), true);
                    if (ValidateDiscount(rdFixed.Checked, reFromLimit, roFromLimit))
                    {
                        if (ACTION == IPC.ACTIONPAGE.EDIT)
                        {
                            r["PromotionID"] = vDiscountID;
                        }
                        else
                        {
                        }
                        r["FromAmt"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFromDiscount.Text.Trim());
                        r["PromotionID"] = vDiscountID;
                        r["FixAmt"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtfixedDiscount.Text.Trim());
                        r["Rate"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRateDetails.Text.Trim());
                        r["MaxPromotion"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtMaxamount.Text.Trim());
                        r["ToAmt"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtToDiscount.Text.Trim()); ;
                        r["MinPromotion"] = "0";
                        r["PromotionType"] = rdFixed.Checked ? "FIX" : "PER";
                    }
                    else
                    {
                        a = "1";
                        lblError.Text = "Promotion details already exists";
                        break;
                    }

                    //double percentagold = 0;

                    //if (rdRate.Checked == true)
                    //{
                    //    percentagold = tblProductDiscountCollect.Rows[0]["Rate"].Equals("") ? 0 : double.Parse(tblProductDiscountCollect.Rows[0]["Rate"].ToString());
                    //    if (100 - percentage - percentagold != 0)
                    //    {
                    //        lblError.Text = "Total percentage should be 100% ";
                    //        return;
                    //    }
                    //}
                  
               

                }

                if (a != "1")
                {
                    tblTempt.Rows.Add(r);
                    tblProductDiscount = tblTempt;
                    r = tblTempt.NewRow();
                }
                //tblTempt.DefaultView.Sort = "[" + tblTempt.Columns["BILLERID"].ColumnName + "], [" + tblTempt.Columns["FROMLIMIT"].ColumnName + "] ASC";
                ViewState["CONTRACTPROMOTION"] = tblTempt;
                tblProductDiscount = (DataTable)ViewState["CONTRACTPROMOTION"];
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
            ShowWarning();
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
    protected void btnSaveDetailsCollect_Click(object sender, EventArgs e)
    {
        try
        {
            double percentage = 0;
            double fixAmt = 0;
            percentage = txtPercentage.Text.ToString().Equals("") ? 0 : double.Parse(txtPercentage.Text.ToString());
            fixAmt = txtFlatAmount.Text.ToString().Equals("") ? 0 : double.Parse(txtFlatAmount.Text.ToString());

            #region validate

            if (ddlShareType.SelectedValue.Equals("PER"))
            {
                if (percentage > 100)
                {
                    lblError.Text = Resources.labels.Percentage + " do not allow greater than 100%";
                    txtPercentage.BorderColor = System.Drawing.Color.Red;
                    txtPercentage.Focus();
                    return;
                }
                if (percentage <= 0)
                {
                    lblError.Text = Resources.labels.Percentage + " do not allow equal or below 0%";
                    txtPercentage.BorderColor = System.Drawing.Color.Red;
                    txtPercentage.Focus();
                    return;
                }
            }
            if (ddlShareType.SelectedValue.Equals("FIX") & fixAmt < 1)
            {
                lblError.Text = Resources.labels.FlatAmount + " not null.";
                txtFlatAmount.BorderColor = System.Drawing.Color.Red;
                txtFlatAmount.Focus();
                return;
            }
            if (txtPriority.Text.Trim().Equals(""))
            {
                lblError.Text = Resources.labels.Priority + " not null.";
                txtPriority.BorderColor = System.Drawing.Color.Red;
                txtPriority.Focus();
                return;
            }




            #endregion
            DataTable tblProductDiscountCollect = new DataTable();
            DataTable tblTempt = new DataTable();
            if (ViewState["CONTRACTPROMOTIONCOLLECT"] == null)
            {
                // tạo table tạm chứa TransDetails 
                DataColumn DISCOUNTID = new DataColumn("PromotionID");
                DataColumn FROM = new DataColumn("FromAmt");
                FROM.DataType = typeof(decimal);
                DataColumn TOAMT = new DataColumn("ToAmt");
                DataColumn MINDISCOUNT = new DataColumn("PaySide");
                DataColumn DISCOUNTTYPE = new DataColumn("PromotionType");
                DataColumn MAXDISCOUNT = new DataColumn("FixAmt");
                DataColumn RATE = new DataColumn("Rate");
                DataColumn FIXED = new DataColumn("Ord");
                tblProductDiscountCollect.Columns.AddRange(new DataColumn[] { DISCOUNTID, FROM, TOAMT, MINDISCOUNT, DISCOUNTTYPE, MAXDISCOUNT, RATE, FIXED });
                DataRow r = tblProductDiscountCollect.NewRow();

                if (ACTION == IPC.ACTIONPAGE.EDIT)
                {
                    r["PromotionID"] = vDiscountID;
                }
                else
                {
                }
                r["FromAmt"] = "0.00";
                r["ToAmt"] = "-1.00";
                r["PaySide"] = ddlPayside.SelectedValue;
                r["PromotionType"] = ddlShareType.SelectedValue;
                r["PromotionID"] = vDiscountID;
                r["FixAmt"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(fixAmt.ToString());
                r["Rate"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(percentage.ToString());
                r["Ord"] = txtPriority.Text;

                tblProductDiscountCollect.Rows.Add(r);
                ViewState["CONTRACTPROMOTIONCOLLECT"] = tblProductDiscountCollect;
            }
            else
            {
                tblProductDiscountCollect = (DataTable)ViewState["CONTRACTPROMOTIONCOLLECT"];
                tblTempt = (DataTable)ViewState["CONTRACTPROMOTIONCOLLECT"];
                DataRow r = tblTempt.NewRow();
                string a = "";

                #region validate payside
                if (ddlShareType.SelectedValue.Equals("FIX"))
                {


                    if (tblProductDiscountCollect.Select("Ord='" + txtPriority.Text + "'").Length > 0)
                    {

                        lblError.Text = "Duplicate priority";
                        return;
                    }
                    lblError.Text = "Can't add new record";
                    return;
                }
                else
                {

                    if (tblProductDiscountCollect.Select("PromotionType='" + "FIX" + "'").Length > 0)
                    {
                        lblError.Text = "Can't add new record";
                        return;
                    }

                    if (tblProductDiscountCollect.Select("Ord='" + txtPriority.Text + "'").Length > 0)
                    {
                        lblError.Text = "Duplicate priority";
                        return;
                    }
                    if (tblProductDiscountCollect.Select("PaySide='" + ddlPayside.SelectedValue + "'").Length > 0)
                    {
                        lblError.Text = " Duplicate PaySide";
                        return;
                    }
                    double percentagold = 0;
                    percentagold = tblProductDiscountCollect.Rows[0]["Rate"].Equals("") ? 0 : double.Parse(tblProductDiscountCollect.Rows[0]["Rate"].ToString());
                    if (100 - percentage - percentagold != 0)
                    {
                        lblError.Text = "Total percentage should be 100% ";
                        return;
                    }

                }



                #endregion

                if (a.Equals("1")) return;
                foreach (DataRow row in tblProductDiscountCollect.Rows)
                {
                    if (ACTION == IPC.ACTIONPAGE.EDIT)
                    {
                        r["PromotionID"] = vDiscountID;
                    }
                    else
                    {
                    }
                    r["FromAmt"] = "0.00";
                    r["ToAmt"] = "-1.00";
                    r["PaySide"] = ddlPayside.SelectedValue;
                    r["PromotionType"] = ddlShareType.SelectedValue;
                    r["PromotionID"] = vDiscountID;
                    r["FixAmt"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(fixAmt.ToString());
                    r["Rate"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(percentage.ToString());
                    r["Ord"] = txtPriority.Text;
                }

                if (a != "1")
                {
                    tblTempt.Rows.Add(r);
                    tblProductDiscountCollect = tblTempt;
                    r = tblTempt.NewRow();
                }
                //tblTempt.DefaultView.Sort = "[" + tblTempt.Columns["BILLERID"].ColumnName + "], [" + tblTempt.Columns["FROMLIMIT"].ColumnName + "] ASC";
                ViewState["CONTRACTPROMOTIONCOLLECT"] = tblTempt;
                tblProductDiscountCollect = (DataTable)ViewState["CONTRACTPROMOTIONCOLLECT"];
            }
            gvDiscountCollect.DataSource = tblProductDiscountCollect;
            gvDiscountCollect.DataBind();
            if (tblProductDiscountCollect != null && tblProductDiscountCollect.Rows.Count > 0)
            {
                Panel3.Visible = true;
            }
            else
            {
                Panel3.Visible = false;
            }
            ShowWarning();
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
        if (txtToDiscount.Text.Equals(string.Empty) || txtToDiscount.Text == null)
        {
            lblError.Text = "To discount details not null";
            return false;
        }
        if (reFromLimit >= reToLimit)
        {
            lblError.Text = "To discount greater than From discount";
            return false;
        }
        bool isValidateDiscount = (roToLimit <= reFromLimit && roToLimit != -1) || (roFromLimit >= reToLimit && reToLimit != -1);

        return isValidateDiscount;
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
        DataTable tblTransDetailsDel = (DataTable)ViewState["CONTRACTPROMOTION"];
        Label lblFkID = (Label)gvDiscountDetails.Rows[e.RowIndex].FindControl("lblDiscountId");
        DataRow[] dr = tblTransDetailsDel.Select("PROMOTIONID='" + lblFkID.Text.Trim() + "'");
        tblTransDetailsDel.Rows.Remove(dr[e.RowIndex]);
        gvDiscountDetails.DataSource = tblTransDetailsDel;
        gvDiscountDetails.DataBind();
        if (tblTransDetailsDel.Rows.Count == 0)
        {
            ViewState["CONTRACTPROMOTION"] = null;
        }
        else
        {
            //tblTransDetailsDel.DefaultView.Sort = "[" + tblTransDetailsDel.Columns["BILLERID"].ColumnName + "], [" + tblTransDetailsDel.Columns["FROMLIMIT"].ColumnName + "] ASC";
            ViewState["CONTRACTPROMOTION"] = tblTransDetailsDel;
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
        btsave.Enabled = true;
        vDiscountID = "D" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
        ViewState["CONTRACTPROMOTION"] = null;
        ViewState["CONTRACTPROMOTIONCOLLECT"] = null;
        txtRateDetails.Enabled = false;
        txtMaxamount.Enabled = false;
        pnDiscountInfo.Enabled = true;
        radSchedule.SelectedValue = IPC.DAILY;
        pnDaily.Enabled = true;
        pnDiscountDetail.Enabled = true;
        pnPaySide.Enabled = true;
        btClone.Visible = false;
        txtContractNo.Text = "";
        ddlTransactionType.SelectedIndex = 0;
        ddlPromotionSide.SelectedIndex = 0;
        ddlContractLevel.SelectedIndex = 0;
        txtFromD.Text = "";
        txtToD.Text = "";
        ddlhourDFrom.SelectedIndex = 0;
        ddlminuteDFrom.SelectedIndex = 0;
        ddlhourDTo.SelectedIndex = 0;
        ddlminuteDTo.SelectedIndex = 0;
        txtFromDiscount.Text = "";
        txtfixedDiscount.Text = "";
        txtRateDetails.Text = "";
        txtMaxamount.Text = "";
        gvDiscountDetails.DataSource = ViewState["CONTRACTPROMOTION"];
        gvDiscountDetails.DataBind();

        gvDiscountCollect.DataSource = ViewState["CONTRACTPROMOTIONCOLLECT"];
        gvDiscountCollect.DataBind();
        ddlPayside.SelectedIndex = 0;
        ddlShareType.SelectedIndex = 0;
        txtPercentage.Text = "";
        txtPriority.Text = "";
        txtFlatAmount.Text = "";
        ddlShareType_OnTextChanged(sender, e);
        rdFixed.Checked = true;
        rdFixed_OnCheckedChanged(sender, e);

        BindData();

    }



    #region Load tat ca cac drop downlist
    void LoadddlAll()
    {
        loadradSchedule();

        loadCombobox_WalletLevel();
        //loadCombobox_ScheduleType();
        loadCombobox_DiscountType();
        loadCombobox_Status();
        loadtimeDate();
        loadcombobox_Transaction();
        loadPayside();
        loadShareType();
        LoadCombobox_PromotionType();
        loadCombobox_BeneficiarySide();
    }
    private void loadCombobox_BeneficiarySide()
    {
        ddlBeneficiarySide.Items.Insert(0, new ListItem("Sender", "S"));
        ddlBeneficiarySide.Items.Insert(1, new ListItem("Receiver", "R"));
        //ddlBeneficiarySide.Items.Insert(2, new ListItem("Both", "B"));
    }
    private void LoadCombobox_PromotionType()
    {
        ddlPromotionType.Items.Clear();
        ddlPromotionType.DataBind();
        //ddlPromotionType.Items.Insert(0, new ListItem("Discount", "D"));
        ddlPromotionType.Items.Insert(0, new ListItem("CashBack", "C"));
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
            ddlPromotionSide.Items.Insert(0, new ListItem("Sender", "S"));
            ddlPromotionSide.Items.Insert(1, new ListItem("Receiver", "R"));
        }
        else
        {
            ddlPromotionSide.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
            ddlPromotionSide.Items.Insert(1, new ListItem("Sender", "S"));
            ddlPromotionSide.Items.Insert(2, new ListItem("Receiver", "R"));
        }

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
            rdRate.Checked = false;
        }
        else
        {
            txtRateDetails.Enabled = true;
            txtMaxamount.Enabled = true;
            txtfixedDiscount.Enabled = false;
            txtfixedDiscount.Text = string.Empty;
            rdFixed.Checked = false;
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

    protected void btClone_OnClick(object sender, EventArgs e)
    {
        vDiscountID = "D" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
        lblTitleProduct.Text = Resources.labels.AddContractPromotion;
        btClone.Visible = false;
        btsave.Enabled = true;
        pnDiscountInfo.Enabled = true;
        vIsClone = "true";
        btsave.Enabled = true;
        pnDiscountDetail.Enabled = true;
        pnPaySide.Enabled = true;
        pnDaily.Enabled = true;
        pnWeekly.Enabled = true;
        pnMonthly.Enabled = true;
        pnOnetime.Enabled = true;
    }

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
                    if ((dtfrhour <= DateTime.Now && (DateTime.Now <= dttohour || (ddlhourDTo.SelectedValue.Equals("0") && ddlminuteDTo.SelectedValue.Equals("0")))))
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
                ddlminuteWTo.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString();

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    int index = int.Parse(dt1.Rows[i]["DAY"].ToString()) - 1;
                    cblThu.Items[index].Selected = true;
                }
                DateTime dtfromdw = DateTime.ParseExact(txtFromW.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[0])).AddMinutes(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[1]));
                DateTime dttodw = DateTime.ParseExact(txtToW.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[0])).AddMinutes(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]));
                DataRow[] ischeck = dt1.Select("Day='" + (int)DateTime.Now.DayOfWeek + "'");
                if (dtfromdw <= DateTime.Now && (DateTime.Now <= dttodw || (ddlhourWTo.SelectedValue.Equals("0") && ddlminuteWTo.SelectedValue.Equals("0"))) && !vIsClone.Equals("true") && ischeck.Length > 0)
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
                ddlminuteMTo.SelectedValue = int.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]).ToString();

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    int index = int.Parse(dt1.Rows[i]["DAY"].ToString()) - 1;
                    cblThuM.Items[index].Selected = true;
                }

                DataRow[] ischeck1 = dt1.Select("Day='" + (int)DateTime.Today.Day + "'");
                DateTime dtfromdm = DateTime.ParseExact(txtFromM.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[0])).AddMinutes(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["FromTime"].ToString(), "HH:mm:ss").Split(':')[1]));
                DateTime dttodm = DateTime.ParseExact(txtToM.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[0])).AddMinutes(Double.Parse(Utility.FormatDatetime(dt.Rows[0]["ToTime"].ToString(), "HH:mm:ss").Split(':')[1]));
                if (dtfromdm <= DateTime.Now && (DateTime.Now <= dttodm || (ddlhourMTo.SelectedValue.Equals("0") && ddlminuteMTo.SelectedValue.Equals("0"))) && !vIsClone.Equals("true") && ischeck1.Length > 0)
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
            if (ViewState["CONTRACTPROMOTIONCOLLECT"] == null && dt.Rows.Count > 0)
            {
                ViewState["CONTRACTPROMOTIONCOLLECT"] = dt;
            }
        }

    }

    bool ValidateSave()
    {


        if (string.IsNullOrEmpty(txtContractNo.Text))
        {
            lblError.Text = Resources.labels.mahopdongkhongrong;
            return false;
        }
        //GetContractByContractNo

        DataSet contractTable = new SmartPortal.SEMS.Contract().GetContractByContractNo(txtContractNo.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
        if (contractTable.Tables[0].Rows.Count < 1)
        {
            lblError.Text = "Contract no is not correct";
            return false;
        }

        if (contractTable.Tables[0].Rows[0]["Status"].Equals("D"))
        {
            lblError.Text = "Contract no is deleted";
            return false;
        }

        DataTable tblTransDetailsDelCollect = (DataTable)ViewState["CONTRACTPROMOTIONCOLLECT"];
        if (tblTransDetailsDelCollect.Rows.Count < 1)
        {
            lblError.Text = "Payside not empty";
            return false;
        }
        DataTable tblTransDetails = (DataTable)ViewState["CONTRACTPROMOTION"];
        if (tblTransDetails.Rows.Count < 1)
        {
            lblError.Text = "Discount details not empty";
            return false;
        }


        bool flag = false;
        int intflag = 0;
        switch (radSchedule.SelectedValue)
        {
            case IPC.DAILY:
                if (string.IsNullOrEmpty(txtFromD.Text) || string.IsNullOrEmpty(txtToD.Text))
                {
                    lblError.Text = "From date and to date not empty";
                    return flag;
                }
                DateTime dtfromd = DateTime.ParseExact(txtFromD.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dttod = DateTime.ParseExact(txtToD.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dtfromd >= dttod)
                {
                    lblError.Text = "To date must be greater than From date ";
                    return flag;
                }

                flag = true;
                break;
            case IPC.WEEKLY:
                if (string.IsNullOrEmpty(txtFromW.Text) || string.IsNullOrEmpty(txtToW.Text))
                {
                    lblError.Text = "From date and to date not empty";
                    return false;
                }
                DateTime dtfromw = DateTime.ParseExact(txtFromW.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dttow = DateTime.ParseExact(txtToW.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dtfromw >= dttow)
                {
                    lblError.Text = "To date must be greater than From date ";
                    return false;
                }

                if ((dttow - dtfromw).TotalDays < 7)
                {
                    lblError.Text = "From date to Date be greater than a week";
                    return false;
                }
                for (int i = 0; i < cblThu.Items.Count; i++)
                {
                    if (cblThu.Items[i].Selected)
                    {
                        flag = true;
                        intflag = 0;
                        break;
                    }
                    else
                    {
                        intflag = 1;
                    }
                }
                flag = true;
                break;
            case IPC.MONTHLY:

                if (string.IsNullOrEmpty(txtFromM.Text) || string.IsNullOrEmpty(txtToM.Text))
                {
                    lblError.Text = "From date and to date not empty";
                    return false;
                }
                DateTime dtfromm = DateTime.ParseExact(txtFromM.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dttom = DateTime.ParseExact(txtToM.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dtfromm >= dttom)
                {
                    lblError.Text = "To date must be greater than From date ";
                    return false;
                }
                if ((dttom - dtfromm).TotalDays < 30)
                {
                    lblError.Text = "From date to Date be greater than a month";
                    return false;
                }

                for (int j = 0; j < cblThuM.Items.Count; j++)
                {
                    if (cblThuM.Items[j].Selected)
                    {
                        flag = true;
                        intflag = 0;
                        break;
                    }
                    else
                    {
                        intflag = 2;
                    }
                }
                flag = true;
                break;
            case IPC.ONETIME:

                if (string.IsNullOrEmpty(txtDateO.Text))
                {
                    lblError.Text = "Date time not empty";
                    return false;
                }
                int hourfrom = Int32.Parse(ddlHourFrom.SelectedValue);
                int hourto = Int32.Parse(ddlHourTo.SelectedValue);
                int minfrom = Int32.Parse(ddlMinuteFrom.SelectedValue);
                int minto = Int32.Parse(ddlMinuteTo.SelectedValue);

                if (hourfrom > hourto)
                {
                    lblError.Text = "To hour must be greater than From hour ";
                    return false;
                }

                if (hourfrom == hourto && minfrom >= minto)
                {
                    lblError.Text = "To hour must be greater than From hour ";
                    return false;
                }


                flag = true;
                break;
        }

        switch (intflag)
        {
            case 1:
                lblError.Text = "Select day not empty";
                return false;
                break;
            case 2:
                lblError.Text = "Select date not empty";
                return false;
                break;
        }


        double percentagold = 0;
        if (tblTransDetailsDelCollect.Rows[0]["PromotionType"].ToString().Equals("PER"))
        {
            for (int i = 0; i < tblTransDetailsDelCollect.Rows.Count; i++)
            {
                percentagold += double.Parse(tblTransDetailsDelCollect.Rows[i]["Rate"].ToString());
            }

            if (percentagold != 100)
            {
                lblError.Text = "Total percentage should be 100% ";
                return false;
            }
        }

        return flag;
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

        DataTable tblTransDetailsDel = (DataTable)ViewState["CONTRACTPROMOTIONCOLLECT"];
        Label lblFkID = (Label)gvDiscountCollect.Rows[e.RowIndex].FindControl("lblDiscountId");
        DataRow[] dr = tblTransDetailsDel.Select("PromotionID='" + lblFkID.Text.Trim() + "'");
        tblTransDetailsDel.Rows.Remove(dr[0]);
        gvDiscountCollect.DataSource = tblTransDetailsDel;
        gvDiscountCollect.DataBind();
        if (tblTransDetailsDel.Rows.Count == 0)
        {
            ViewState["CONTRACTPROMOTIONCOLLECT"] = null;
        }
        else
        {
            ViewState["CONTRACTPROMOTIONCOLLECT"] = tblTransDetailsDel;
        }
        if (tblTransDetailsDel != null && tblTransDetailsDel.Rows.Count > 0)
        {
            Panel3.Visible = true;
        }
        else
        {
            Panel3.Visible = false;
        }
        ShowWarning();
    }

    protected void ddlShareType_OnTextChanged(object sender, EventArgs e)
    {
        txtFlatAmount.BorderColor = System.Drawing.Color.Empty;
        if (ddlShareType.SelectedValue == "PER")
        {
            txtPercentage.Enabled = true;
            txtFlatAmount.Enabled = false;
            txtFlatAmount.Text = string.Empty;
        }
        if (ddlShareType.SelectedValue == "FIX")
        {
            txtPercentage.Text = string.Empty;
            txtFlatAmount.Enabled = true;
            txtPercentage.Enabled = false;
        }
    }


    #region Submerchant contract
    
    protected void gvListSubMerchantContract_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        //    CheckBox cbxSelect;
        //    Label lblContractNo, lblFullname, lblEmail, lblAddress, lblPhone, lblStatus;
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
        //        cbxSelect.Attributes.Add("onclick", "ChildClick(this);");

        //        lblContractNo = (Label)e.Row.FindControl("lblContractNo");
        //        lblFullname = (Label)e.Row.FindControl("lblFullname");
        //        lblEmail = (Label)e.Row.FindControl("lblEmail");
        //        lblAddress = (Label)e.Row.FindControl("lblAddress");
        //        lblPhone = (Label)e.Row.FindControl("lblPhone");
        //        lblStatus = (Label)e.Row.FindControl("lblStatus");
                
        //        lblContractNo.Text = drv["CONTRACTNO"].ToString();
        //        lblFullname.Text = drv["FULLNAME"].ToString();
        //        lblEmail.Text = drv["Email"].ToString();
        //        lblAddress.Text = drv["ADDRRESIDENT"].ToString();
        //        lblPhone.Text = drv["TEL"].ToString();
             
        //        DataTable tblContractRela = (DataTable)ViewState["CONTRACTDISTCOUNTRELA"];
        //        if (tblContractRela != null && tblContractRela.Rows.Count>0)
        //        {
        //            if (tblContractRela.AsEnumerable().Any(x => (string)x["ObjectId"] == lblContractNo.Text))
        //            {
        //                cbxSelect.Checked = true;
        //                if (tblContractRela.AsEnumerable().Any(x => (string)x["Status"] == "D" && (string)x["ObjectId"] == lblContractNo.Text))
        //                {
        //                    cbxSelect.Checked = false;
        //                }

        //                switch (tblContractRela.AsEnumerable().FirstOrDefault((x => (string)x["ObjectId"] == lblContractNo.Text))["Status"].ToString())
        //                {
        //                    case "I":
        //                        lblStatus.Text = Resources.labels.inactive;
        //                        lblStatus.Attributes.Add("class", "label-danger");
        //                        break;
        //                    case "A":
        //                        lblStatus.Text = Resources.labels.conactive;
        //                        lblStatus.Attributes.Add("class", "label-success");
        //                        break;
        //                    case "N":
        //                        lblStatus.Text = Resources.labels.connew;
        //                        lblStatus.Attributes.Add("class", "label-success");
        //                        break;
        //                    case "P":
        //                        cbxSelect.Enabled = false;
        //                        lblStatus.Text = Resources.labels.conpending;
        //                        lblStatus.Attributes.Add("class", "label-warning");
        //                        cbxSelect.Enabled = false;
        //                        break;
        //                    case "R":
        //                        lblStatus.Text = Resources.labels.conreject;
        //                        lblStatus.Attributes.Add("class", "label-warning");
        //                        cbxSelect.Enabled = false;
        //                        break;
        //                    case "D":
        //                        cbxSelect.Enabled = false;
        //                        lblStatus.Text = Resources.labels.condelete;
        //                        lblStatus.Attributes.Add("class", "label-danger");
        //                        break;
        //                    case "H":
        //                        cbxSelect.Enabled = false;
        //                        lblStatus.Text = Resources.labels.pendingfordelete;
        //                        lblStatus.Attributes.Add("class", "label-warning");
        //                        cbxSelect.Enabled = false;
        //                        break;
        //                    default:
        //                        cbxSelect.Enabled = false;
        //                        lblStatus.Text = drv["Status"].ToString();
        //                        lblStatus.Attributes.Add("class", "label-success");
        //                        break;
        //                }
        //            }

        //        }

        //    }
        //}
        //catch (Exception ex)
        //{
        //    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
        //    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        //}
    }

    protected void gvListSubMerchantContract_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    #endregion
    public string ConvertDataTabletoString(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    protected void ddlPromotionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPromotionType.SelectedValue.ToString().Equals("C"))
        {
            pnPaySide.Visible = false;

            DataTable tblProductDiscountCollect = new DataTable();

            DataColumn DISCOUNTID = new DataColumn("PromotionID");
            DataColumn FROM = new DataColumn("FromAmt");
            FROM.DataType = typeof(decimal);
            DataColumn TOAMT = new DataColumn("ToAmt");
            DataColumn MINDISCOUNT = new DataColumn("PaySide");
            DataColumn MAXDISCOUNT = new DataColumn("FixAmt");
            DataColumn DISCOUNTTYPE = new DataColumn("PromotionType");
            DataColumn RATE = new DataColumn("Rate");
            DataColumn FIXED = new DataColumn("Ord");
            tblProductDiscountCollect.Columns.AddRange(new DataColumn[] { DISCOUNTID, FROM, TOAMT, MINDISCOUNT, DISCOUNTTYPE, MAXDISCOUNT, RATE, FIXED });
            DataRow r = tblProductDiscountCollect.NewRow();

            //if (ACTION == IPC.ACTIONPAGE.EDIT)
            //{
            //    r["PromotionID"] = vDiscountID;
            //}
            //else
            //{
            //}
            r["FromAmt"] = "0.00";
            r["ToAmt"] = "-1.00";
            r["PaySide"] = ddlPayside.SelectedValue;
            r["PromotionType"] = "PER";
            r["PromotionID"] = vDiscountID;
            r["FixAmt"] = "0";
            r["Rate"] = "100";
            r["Ord"] = "1";

            tblProductDiscountCollect.Rows.Add(r);
            ViewState["CONTRACTPROMOTIONCOLLECT"] = tblProductDiscountCollect;
        }
        else
        {
            pnPaySide.Visible = true;
        }
    }
}

