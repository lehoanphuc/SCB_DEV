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
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_SEMSProductLimit_Controls_Widget : WidgetBase
{
    private string ACTION = string.Empty;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
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
            txtlimit.Attributes.Add("onkeypress", "executeComma('" + txtlimit.ClientID + "')");
            txtTotalLimit.Attributes.Add("onkeypress", "executeComma('" + txtTotalLimit.ClientID + "')");
            txtCountLimit.Attributes.Add("onkeypress", "executeComma('" + txtCountLimit.ClientID + "')");
            txtAmountbiometric.Attributes.Add("onkeypress", "executeComma('" + txtAmountbiometric.ClientID + "')");
            txtlimit.Attributes.Add("onkeyup", "executeComma('" + txtlimit.ClientID + "')");
            txtTotalLimit.Attributes.Add("onkeyup", "executeComma('" + txtTotalLimit.ClientID + "')");
            txtCountLimit.Attributes.Add("onkeyup", "executeComma('" + txtCountLimit.ClientID + "')");
            txtAmountbiometric.Attributes.Add("onkeyup", "executeComma('" + txtAmountbiometric.ClientID + "')");
            if (!IsPostBack)
            {
                LoadDll();
                BindData();
                ddlTransaction_OnSelectedIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void LoadDll()
    {
        try
        {
            ddllimittype.Enabled = false;
            //load ten san pham
            ddlProductType.DataSource = new SmartPortal.SEMS.Product().GetProductByCondition("", "", "P", "", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            ddlProductType.DataTextField = "PRODUCTNAME";
            ddlProductType.DataValueField = "PRODUCTID";
            ddlProductType.DataBind();
            //load các giao dịch
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    DataSet dstrans = new SmartPortal.SEMS.Product().GetTranNameByProduct2(Utility.KillSqlInjection(ddlProductType.SelectedValue.ToString()), Utility.KillSqlInjection(ddllimittype.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
                    ddlTrans.DataSource = dstrans;
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    if (dstrans.Tables.Count > 0)
                    {
                        if (dstrans.Tables[0].Rows[0]["ServiceID"].Equals("AM"))
                        {
                            ddllimittype.Enabled = true;
                        }
                    }
                    ddlTrans.DataTextField = "PAGENAME";
                    ddlTrans.DataValueField = "TRANCODE";
                    ddlTrans.DataBind();

                    break;
                case IPC.ACTIONPAGE.EDIT:
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    string[] key = ID.Split('|');
                    ddlTrans.DataSource = new SmartPortal.SEMS.Product().GetTranNameByProduct2(key[0], key[4], ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    ddlTrans.DataTextField = "PAGENAME";
                    ddlTrans.DataValueField = "TRANCODE";
                    ddlTrans.DataBind();
                    break;
            }
            //load tien te vutran15122014
            ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlCCYID.DataTextField = "CCYID";
            ddlCCYID.DataValueField = "CCYID";
            ddlCCYID.DataBind();
            // DataSet ListContractLevel = new SmartPortal.SEMS.Product().LoadContractLevelActive(ref IPCERRORCODE, ref IPCERRORDESC);
            // if (IPCERRORCODE == "0")
            // {
                // ddlContractLevel.DataSource = ListContractLevel;
                // ddlContractLevel.DataTextField = "ContractLevelName";
                // ddlContractLevel.DataValueField = "ContractLevelID";
                // ddlContractLevel.DataBind();
            // }
            // else
            // {
                // lblError.Text = IPCERRORDESC;
            // }
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
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    btnClear.Enabled = false;
                    #region 
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    string[] key = ID.Split('|');
                    DataSet ds = new SmartPortal.SEMS.Product().GetAllProLimitByProID(key[0], key[1], key[2], key[3], key[4], Convert.ToDecimal(key[5].ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable productLMTable = new DataTable();
                        productLMTable = ds.Tables[0];
                        if (productLMTable.Rows.Count != 0)
                        {
                            if (productLMTable.Rows[0]["UnitType"].ToString() == SmartPortal.Constant.IPC.OUT)
                            {
                                txtlimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(productLMTable.Rows[0]["TRANLIMIT"].ToString(), productLMTable.Rows[0]["CCYID"].ToString());
                                txtTotalLimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(productLMTable.Rows[1]["TRANLIMIT"].ToString(), productLMTable.Rows[0]["CCYID"].ToString());
                                txtCountLimit.Text = productLMTable.Rows[1]["COUNTLIMIT"].ToString();
                                ddlCCYID.SelectedValue = productLMTable.Rows[0]["CCYID"].ToString();
                                // ddlContractLevel.SelectedValue = productLMTable.Rows[0]["ContractLevelId"].ToString();
                                ddlUnitType.SelectedValue = productLMTable.Rows[1]["UnitType"].ToString();
                                txtDesc.Text = productLMTable.Rows[0]["Description"].ToString();
                                ddllimittype.SelectedValue = productLMTable.Rows[0]["LimitType"].ToString();
                                txtAmountbiometric.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(productLMTable.Rows[1]["BiometricLimit"].ToString(), productLMTable.Rows[0]["CCYID"].ToString());
                            }
                            else
                            {
                                txtlimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(productLMTable.Rows[1]["TRANLIMIT"].ToString(), productLMTable.Rows[0]["CCYID"].ToString());
                                txtTotalLimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(productLMTable.Rows[0]["TRANLIMIT"].ToString(), productLMTable.Rows[0]["CCYID"].ToString());
                                txtCountLimit.Text = productLMTable.Rows[0]["COUNTLIMIT"].ToString();
                                ddlCCYID.SelectedValue = productLMTable.Rows[1]["CCYID"].ToString();
                                // ddlContractLevel.SelectedValue = productLMTable.Rows[1]["ContractLevelId"].ToString();
                                ddlUnitType.SelectedValue = productLMTable.Rows[0]["UnitType"].ToString();
                                txtDesc.Text = productLMTable.Rows[1]["Description"].ToString();
                                ddllimittype.SelectedValue = productLMTable.Rows[1]["LimitType"].ToString();
                                txtAmountbiometric.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(productLMTable.Rows[1]["BiometricLimit"].ToString(), productLMTable.Rows[0]["CCYID"].ToString());
                            }
                            ddlProductType.SelectedValue = productLMTable.Rows[0]["PRODUCTID"].ToString();
                            ddllimittype_OnSelectedIndexChanged(this, EventArgs.Empty);
                            ddlProductType_OnSelectedIndexChanged(this, EventArgs.Empty);
                            if (ACTION.Equals(IPC.ACTIONPAGE.EDIT) || ACTION.Equals(IPC.ACTIONPAGE.DETAILS))
                            {
                                ddlTrans.DataSource = new SmartPortal.SEMS.Transactions().LoadTranApp(ref IPCERRORCODE, ref IPCERRORDESC);
                                if (IPCERRORCODE != "0")
                                {
                                    throw new IPCException(IPCERRORDESC);
                                }

                                ddlTrans.DataTextField = "PAGENAME";
                                ddlTrans.DataValueField = "TRANCODE";
                                ddlTrans.DataBind();
                                ddlTrans.SelectedValue = productLMTable.Rows[0]["TRANCODE"].ToString();
                            }
                            if (txtlimit.Text == "-1.00" || txtlimit.Text == "-1")
                            {
                                txtlimit.Text = null;
                                txtTotalLimit.Text = null;
                                txtlimit.Enabled = false;
                                txtTotalLimit.Enabled = false;
                                checktxtlimit.Checked = true;
                                checktotallimit.Enabled = false;
                                checktotallimit.Checked = true;
                            }
                            if (checktotallimit.Checked || txtTotalLimit.Text == null || txtTotalLimit.Text == "-1.00" || txtTotalLimit.Text == "-1")
                            {
                                txtTotalLimit.Text = null;
                                txtTotalLimit.Enabled = false;
                                checktotallimit.Checked = true;
                            }
                            if (txtCountLimit.Text == "-1.00" || txtCountLimit.Text == "-1")
                            {
                                txtCountLimit.Text = null;
                                txtCountLimit.Enabled = false;
                                checkcountlimit.Checked = true;
                            }
                            ddllimittype.Enabled = false;
                        }
                    }
                    else
                    {
                        throw new IPCException(IPCERRORCODE);
                    }
                    #endregion
                    break;
            }
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    btsave.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    ddlProductType.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    // ddlContractLevel.Enabled = false;
                    ddlUnitType.Enabled = false;
                    btsave.Enabled = true;
                    break;
            }
            #endregion

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

            //hunglt check unlimit

            if (checktxtlimit.Checked)
            {
                txtlimit.Text = "-1";
                txtTotalLimit.Text = "-1";
            }
            if (checktotallimit.Checked)
            {
                txtTotalLimit.Text = "-1";
            }
            if (checkcountlimit.Checked)
            {
                txtCountLimit.Text = "-1";
            }
            if (divLimitBIO.Visible && string.IsNullOrEmpty(txtAmountbiometric.Text.Trim()))
            {
                lblError.Text = Resources.labels.themhanmucsanphamthanhcong;
            }
            string amountBIO = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmountbiometric.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            if (!divLimitBIO.Visible)
            {
                amountBIO = "0";
            }
            string proID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlProductType.SelectedValue.Trim());
            string trancode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string tranlm = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtlimit.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string countlm = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.IsInt(txtCountLimit.Text.Trim() == "" ? "999999999" : txtCountLimit.Text.Trim()).ToString());
            string totallimit = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTotalLimit.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string contractlevel = "1";
            string unittype = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlUnitType.SelectedValue.ToString());
            string desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.ToString().Trim());
            string limittype = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddllimittype.SelectedValue.ToString());

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    new SmartPortal.SEMS.Product().InsertProLimit(proID, trancode, ccyid, "0", amountBIO, limittype, Convert.ToDecimal(contractlevel), tranlm, totallimit, countlm, unittype, "A", desc, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.themhanmucsanphamthanhcong;
                        btsave.Enabled = false;
                        pnAdd.Enabled = false;
                        btnClear.Enabled = true;
                    }
                    else
                    {
                        ErrorCodeModel EM = new ErrorCodeModel();
                        EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.INSERTPLERROR), System.Globalization.CultureInfo.CurrentCulture.ToString());
                        lblError.Text = EM.ErrorDesc;
                    }
                    if (checktxtlimit.Checked)
                    {
                        txtlimit.Text = null;
                        txtTotalLimit.Text = null;
                    }
                    if (checktotallimit.Checked)
                    {
                        txtTotalLimit.Text = null;
                    }
                    if (checkcountlimit.Checked)
                    {
                        txtCountLimit.Text = null;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    new SmartPortal.SEMS.Product().UpdateProLimit(proID, trancode, ccyid, "0", amountBIO, limittype, Convert.ToDecimal(contractlevel), tranlm, totallimit, countlm, unittype, "A", desc, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.capnhathanmucsanphamthanhcong;
                        btsave.Enabled = false;
                        pnAdd.Enabled = false;
                    }
                    else
                    {
                        ErrorCodeModel EM = new ErrorCodeModel();
                        EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.UPDATEPLERROR), System.Globalization.CultureInfo.CurrentCulture.ToString());
                        lblError.Text = EM.ErrorDesc;
                    }
                    if (checktxtlimit.Checked)
                    {
                        txtlimit.Text = null;
                        txtTotalLimit.Text = null;
                    }
                    if (checktotallimit.Checked)
                    {
                        txtTotalLimit.Text = null;
                    }
                    if (checkcountlimit.Checked)
                    {
                        txtCountLimit.Text = null;
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
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        btsave.Enabled = true;
        pnAdd.Enabled = true;
        txtlimit.Text = string.Empty;
        txtTotalLimit.Text = string.Empty;
        txtCountLimit.Text = string.Empty;

        ddlCCYID.SelectedIndex = 0;
        // ddlContractLevel.SelectedIndex = 0;
        ddlUnitType.SelectedIndex = 0;
        txtDesc.Text = string.Empty;
        ddlProductType.SelectedIndex = 0;
        ddlProductType_OnSelectedIndexChanged(sender, EventArgs.Empty);
    }
    protected void ddlProductType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            ds1 = new SmartPortal.SEMS.Product().GetTranNameByProduct(Utility.KillSqlInjection(ddlProductType.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds1.Tables.Count > 0 && (ds1.Tables[0].Rows[0]["ServiceID"].ToString() == SmartPortal.Constant.IPC.MB
                                         || ds1.Tables[0].Rows[0]["ServiceID"].ToString() == SmartPortal.Constant.IPC.AM))
            {
                ddllimittype.Enabled = true;
            }
            else
            {
                ddllimittype.Enabled = false;
                ddllimittype.SelectedValue = SmartPortal.Constant.IPC.NORMAL;
            }
            ds = new SmartPortal.SEMS.Product().GetTranNameByProduct2(Utility.KillSqlInjection(ddlProductType.SelectedValue.ToString()), Utility.KillSqlInjection(ddllimittype.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            ddlTrans.Items.Clear();
            DataTable dt = ds.Tables[0];
            ddlTrans.DataSource = dt;
            ddlTrans.DataTextField = "PAGENAME";
            ddlTrans.DataValueField = "TRANCODE";
            ddlTrans.DataBind();
            ddlTransaction_OnSelectedIndexChanged(null, null);
            try
            {
                if (ddllimittype.SelectedValue.Equals("BAT"))
                {
                    ddlTrans.SelectedValue = "IB000499";
                }
            }
            catch { }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ddlTransaction_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.Transactions().GetTransactionInfor(Utility.KillSqlInjection(ddlTrans.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null && ds.Tables[0].Columns.Count > 0)
            {
                if (ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.SERVICEID].Equals("IB"))
                {
                    divLimitBIO.Visible = false;
                }
                else
                {
                    divLimitBIO.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddllimittype_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ACTION != SmartPortal.Constant.IPC.EDIT)
            {
                if (ddllimittype.SelectedValue.Equals("BAT"))
                {
                    ddlTrans.DataSource = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    ddlTrans.DataTextField = "PAGENAME";
                    ddlTrans.DataValueField = "TRANCODE";
                    ddlTrans.DataBind();
                    ddlTrans.SelectedValue = "IB000499";
                    LoadBatchTranfer(false);
                    return;
                }
                else
                {
                    LoadBatchTranfer(true);
                    checkcountlimit.Checked = false;
                    checktotallimit.Checked = false;
                }
                ddlTrans.DataSource = new SmartPortal.SEMS.Product().GetTranNameByProduct2(Utility.KillSqlInjection(ddlProductType.SelectedValue.ToString()), Utility.KillSqlInjection(ddllimittype.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                ddlTrans.DataTextField = "PAGENAME";
                ddlTrans.DataValueField = "TRANCODE";
                ddlTrans.DataBind();
            }
            else
            {
                ddllimittype.Enabled = true;
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }


    protected void checktxtlimit_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (checktxtlimit.Checked)
            {
                txtlimit.Text = null;
                txtTotalLimit.Text = null;
                txtlimit.Enabled = false;
                txtTotalLimit.Enabled = false;
                checktotallimit.Enabled = false;
                checktotallimit.Checked = true;
            }
            else
            {
                if (ddllimittype.SelectedValue == "BAT")
                {
                    txtlimit.Enabled = true;
                    txtTotalLimit.Enabled = false;
                    checktotallimit.Enabled = false;
                }
                else
                {
                    txtlimit.Text = null;
                    txtTotalLimit.Text = null;
                    txtlimit.Enabled = true;
                    txtTotalLimit.Enabled = true;
                    checktotallimit.Checked = false;
                    checktotallimit.Enabled = true;
                }

            }


        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }

    protected void checkcountlimit_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (checkcountlimit.Checked)
            {
                txtCountLimit.Text = null;
                txtCountLimit.Enabled = false;
            }
            else
            {
                if (ddllimittype.SelectedValue != "BAT")
                {
                    txtCountLimit.Text = null;
                    txtCountLimit.Enabled = true;
                }

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void checktotallimit_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {

            if (checktotallimit.Checked)
            {
                txtTotalLimit.Text = null;
                txtTotalLimit.Enabled = false;
            }
            else
            {
                if (ddllimittype.SelectedValue != "BAT")
                {
                    txtTotalLimit.Text = null;
                    txtTotalLimit.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadBatchTranfer(bool isbool)
    {

        ddlTrans.Enabled = isbool;
        //ddlProductType.Enabled = false;
        ddlCCYID.Enabled = isbool;
        txtTotalLimit.Enabled = isbool;
        txtCountLimit.Enabled = isbool;
        checkcountlimit.Enabled = isbool;
        checkcountlimit.Checked = true;
        checktotallimit.Enabled = isbool;
        checktotallimit.Checked = true;
        divLimitBIO.Visible = false;
    }
}
