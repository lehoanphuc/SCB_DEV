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
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.Model;

public partial class Widgets_SEMSProductFee_Controls_Widget : WidgetBase
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
            if (!IsPostBack)
            {
                LoadDll();
                BindData();
                //ddlProductType_OnSelectedIndexChanged(null, null);
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
        ddlPayer.Items.Add(new ListItem(Resources.labels.nguoigui, "0"));
        ddlPayer.Items.Add(new ListItem(Resources.labels.nguoinhan, "1"));

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
        ddlTrans.DataSource = new SmartPortal.SEMS.Product().GetTranNameByProduct2(Utility.KillSqlInjection(ddlProductType.SelectedValue.ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            throw new IPCException(IPCERRORDESC);
        }
        ddlTrans.DataTextField = "PAGENAME";
        ddlTrans.DataValueField = "TRANCODE";
        ddlTrans.DataBind();

        loadCombobox_FeeCode();
        //load tien te vutran15122014
        ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
        ddlCCYID.DataTextField = "CCYID";
        ddlCCYID.DataValueField = "CCYID";
        ddlCCYID.DataBind();

        DataSet ds = new SmartPortal.SEMS.Fee().LoadFeeCbb(string.Empty, ddlCCYID.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            ddlFee.DataSource = ds;
            ddlFee.DataTextField = "FEENAME";
            ddlFee.DataValueField = "FEEID";
            ddlFee.DataBind();
        }
        else
        {
            throw new IPCException(IPCERRORDESC);
        }


        DataSet ds1 = new SmartPortal.SEMS.Fee().LoadFeeCbb("TRAN", ddlCCYID.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            ddlFeeTransfer.DataSource = ds1;
            ddlFeeTransfer.DataTextField = "FEENAME";
            ddlFeeTransfer.DataValueField = "FEEID";
            ddlFeeTransfer.DataBind();
        }
        else
        {
            throw new IPCException(IPCERRORDESC);
        }

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
    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    ddlTrans_TextChanged(null, null);
                    break;
                default:
                    ddlFeeTransfer.Enabled = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    string[] key = ID.Split('|');
                    DataSet ds = new SmartPortal.SEMS.Fee().SearchProductFee(key[0], key[1], key[2], key[3], key[4], key[5], ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable productLMTable = new DataTable();
                        productLMTable = ds.Tables[0];
                        if (productLMTable.Rows.Count > 0)
                        {
                            ddlProductType.SelectedValue = productLMTable.Rows[0]["PRODUCTID"].ToString();
                            ddlProductType_OnSelectedIndexChanged(this, EventArgs.Empty);
                            ddlTrans.SelectedValue = productLMTable.Rows[0]["TRANCODE"].ToString();
                            txtDesc.Text = productLMTable.Rows[0]["DESCRIPTION"].ToString();
                            ddlCCYID.SelectedValue = productLMTable.Rows[0]["CCYID"].ToString();
                            ddlCCYID_OnSelectedIndexChanged(null, null);
                            ddlFee.SelectedValue = productLMTable.Rows[0]["FEEID"].ToString();
                            ddlPayer.SelectedValue = productLMTable.Rows[0]["PAYER"].ToString();
                            // ddlContractLevel.SelectedValue = productLMTable.Rows[0]["ContractLevelId"].ToString();
                            loadCombobox_FeeCode();
                            // ddlFeeShareCode.SelectedValue = productLMTable.Rows[0]["FeeShareId"].ToString();

                            if (productLMTable.Rows[0]["FEETYPE"].ToString().Equals("TRAN"))
                            {
                                ddlFeeTransfer.SelectedValue = productLMTable.Rows[0]["FEEID"].ToString();
                                ddlFee.SelectedValue = "";
                            }
                            else
                            {
                                ddlFeeTransfer.SelectedValue = "";
                                ddlFee.SelectedValue = productLMTable.Rows[0]["FEEID"].ToString();
                            }
                        }
                        else
                        {
                            throw new IPCException(IPCERRORDESC);
                        }
                    }
                    else
                    {
                        throw new IPCException(IPCERRORCODE);
                    }
                    break;
            }

            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    btnClear.Enabled = false;
                    btsave.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    ddlProductType.Enabled = false;
                    ddlFee.Enabled = false;
                    ddlTrans.Enabled = false;
                    btnClear.Enabled = false;
                    ddlCCYID.Enabled = false;
                    // ddlContractLevel.Enabled = false;
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
            string proID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlProductType.SelectedValue.Trim());
            string trancode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim());
            string fee = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlFee.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string payer = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlPayer.SelectedValue.Trim());
            string contractlevel = "1";
            string desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.Trim());
            string FeeShareId = "";
            string transferFeeID= SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlFeeTransfer.SelectedValue.Trim());
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    try
                    {
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                        new SmartPortal.SEMS.Fee().InsertProductFee(proID, trancode, fee, desc, ccyid, payer, "D", contractlevel, Session["userName"].ToString(), FeeShareId, transferFeeID, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.themphichosanphamthanhcong;
                            pnAdd.Enabled = false;
                            btsave.Enabled = false;
                        }
                        else
                        {
                            ErrorCodeModel EM = new ErrorCodeModel();
                            EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.EXISTFEEFROPRODUCT), System.Globalization.CultureInfo.CurrentCulture.ToString());
                            lblError.Text = EM.ErrorDesc;
                        }
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    try
                    {
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                        new SmartPortal.SEMS.Fee().UpdateProductFee(proID, trancode, fee, desc, ccyid, payer, "D", contractlevel, Session["userName"].ToString(), FeeShareId,transferFeeID, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.Editproductsuccessfull;
                            pnAdd.Enabled = false;
                            btsave.Enabled = false;
                        }
                        else
                        {
                            ErrorCodeModel EM = new ErrorCodeModel();
                            EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.EXISTFEEFROPRODUCT), System.Globalization.CultureInfo.CurrentCulture.ToString());
                            lblError.Text = EM.ErrorDesc;
                        }
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
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
    protected void ddlCCYID_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            DataSet ds = new SmartPortal.SEMS.Fee().LoadFeeCbb(string.Empty, ddlCCYID.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlFee.DataSource = ds;
                ddlFee.DataTextField = "FEENAME";
                ddlFee.DataValueField = "FEEID";
                ddlFee.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }


            DataSet ds1 = new SmartPortal.SEMS.Fee().LoadFeeCbb("TRAN", ddlCCYID.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlFeeTransfer.DataSource = ds1;
                ddlFeeTransfer.DataTextField = "FEENAME";
                ddlFeeTransfer.DataValueField = "FEEID";
                ddlFeeTransfer.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        pnAdd.Enabled = true;
        btsave.Enabled = true;
        ddlProductType.SelectedIndex = 0;
        ddlProductType_OnSelectedIndexChanged(sender, EventArgs.Empty);
        txtDesc.Text = string.Empty;
        ddlCCYID.SelectedIndex = 0;
        ddlPayer.SelectedIndex = 0;
        // ddlContractLevel.SelectedIndex = 0;
        ddlCCYID_OnSelectedIndexChanged(sender, EventArgs.Empty);
        DataSet ds = new SmartPortal.SEMS.Fee().LoadFeeCbb(string.Empty, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            ddlFee.DataSource = ds;
            ddlFee.DataTextField = "FEENAME";
            ddlFee.DataValueField = "FEEID";
            ddlFee.DataBind();
        }
        else
        {
            throw new IPCException(IPCERRORDESC);
        }


        DataSet ds1 = new SmartPortal.SEMS.Fee().LoadFeeCbb("TRAN", string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            ddlFeeTransfer.DataSource = ds1;
            ddlFeeTransfer.DataTextField = "FEENAME";
            ddlFeeTransfer.DataValueField = "FEEID";
            ddlFeeTransfer.DataBind();
        }
        else
        {
            throw new IPCException(IPCERRORDESC);
        }
    }

    protected void ddlProductType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet dt  = new SmartPortal.SEMS.Product().GetTranNameByProduct2(Utility.KillSqlInjection(ddlProductType.SelectedValue.ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);
            ddlTrans.DataSource = dt;
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            DataRow[] arrIBR = dt.Tables[0].Select("ServiceID='" + SmartPortal.Constant.IPC.AM + "'");
            if(arrIBR.Length>0)
            {
                //ddlFeeTransfer.SelectedValue = "";
                ddlFeeTransfer.Enabled = false;
            }
            else
            {
                ddlFeeTransfer.Enabled = true;
            }
            ddlTrans.DataTextField = "PAGENAME";
            ddlTrans.DataValueField = "TRANCODE";
            ddlTrans.DataBind();
            ddlTrans_TextChanged(null, null);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlTrans_TextChanged(object sender, EventArgs e)
    {
        if (ddlTrans.SelectedValue.Equals("IB_TRANFERFX") || ddlTrans.SelectedValue.Equals("IBTRANFERFXWL")||
            ddlTrans.SelectedValue.Equals("IBWLTRANFERFX") || ddlTrans.SelectedValue.Equals("MB_TRFOTHFX") ||
                ddlTrans.SelectedValue.Equals("WL_TRFOTHFX") || ddlTrans.SelectedValue.Equals("MB_TRFOTHFXWL"))
        {
            ddlCCYID.Items.Clear();
            ddlCCYID.Enabled = false;
            //ddlFeeTransfer.SelectedValue = "";
            //ddlFeeTransfer.Enabled = false;
        }
        else
        {
            ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlCCYID.DataTextField = "CCYID";
            ddlCCYID.DataValueField = "CCYID";
            ddlCCYID.DataBind();
            ddlCCYID.Enabled = true;
        }
        loadCombobox_FeeCode();
        ddlCCYID_OnSelectedIndexChanged(sender, EventArgs.Empty);
    }
    private void loadCombobox_FeeCode()
    {
        // try
        // {
            // SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
            // DataSet ds = new DataSet();
            // object[] searchObject = new object[] { ddlTrans.SelectedValue };
            // ds = _service.common("SEMSGETFEESHALIBYTRA", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            // if (ds != null)
            // {
                // if (ds.Tables.Count > 0)
                // {
                    // if (ds.Tables[0].Rows.Count > 0)
                    // {

                        // ddlFeeShareCode.DataSource = ds.Tables[0];
                        // ddlFeeShareCode.DataValueField = "FeeShareID";
                        // ddlFeeShareCode.DataTextField = "FeeShareCode";
                        // ddlFeeShareCode.DataBind();
                        // ddlFeeShareCode.Items.Insert(0, new ListItem("None", ""));
                        // ddlFeeShareCode.Enabled = true;
                    // }
                    // else
                    // {
                        // ddlFeeShareCode.Enabled = false;
                    // }
                // }
                // else
                // {
                    // ddlFeeShareCode.Enabled = false;
                // }
            // }
            // else
            // {
                // ddlFeeShareCode.Enabled = false;
            // }
        // }
        // catch (Exception ex)
        // {
            // SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            // SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        // }
    }
}
