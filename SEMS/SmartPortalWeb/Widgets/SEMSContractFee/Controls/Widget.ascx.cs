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

public partial class Widgets_SEMSContractFee_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                Session["printContractFee"] = null;
                LoadDll();
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

    void LoadDll()
    {
        ddlPayer.Items.Add(new ListItem(Resources.labels.nguoigui, "0"));
        ddlPayer.Items.Add(new ListItem(Resources.labels.nguoinhan, "1"));

        ddlTrans.DataSource = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISPRODUCTFEE), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            throw new IPCException(IPCERRORDESC);
        }
        ddlTrans.DataTextField = "PAGENAME";
        ddlTrans.DataValueField = "TRANCODE";
        ddlTrans.DataBind();

        //load tien te vutran15122014
        ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
        ddlCCYID.DataTextField = "CCYID";
        ddlCCYID.DataValueField = "CCYID";
        ddlCCYID.DataBind();
    }

    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    btnPrint.Visible = false;
                    ddlTransferFee.Enabled = true;
                    //load  fee
                    DataSet dsFeeAdd = new SmartPortal.SEMS.Fee().LoadFeeCbb(string.Empty, Utility.KillSqlInjection(ddlCCYID.SelectedValue), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        ddlFee.DataSource = dsFeeAdd;
                        ddlFee.DataTextField = "FEENAME";
                        ddlFee.DataValueField = "FEEID";
                        ddlFee.DataBind();
                    }
                    else
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    DataSet dsFeeAdd2 = new SmartPortal.SEMS.Fee().LoadFeeCbb("TRAN", Utility.KillSqlInjection(ddlCCYID.SelectedValue), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        ddlTransferFee.DataSource = dsFeeAdd2;
                        ddlTransferFee.DataTextField = "FEENAME";
                        ddlTransferFee.DataValueField = "FEEID";
                        ddlTransferFee.DataBind();
                    }
                    else
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    break;
                default:
                    btnClear.Enabled = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    string[] key = ID.Split('|');
                    hdStatus.Value = key[4];
                    DataSet ds = new SmartPortal.SEMS.Fee().SearchContractFee(key[0], key[1], key[2], key[3], "", key[4], "",
                        "", 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable productLMTable = ds.Tables[0];
                        if (productLMTable.Rows.Count > 0)
                        {
                            txtContractNo.Text = productLMTable.Rows[0]["CONTRACTNO"].ToString();
                            ddlTrans.SelectedValue = productLMTable.Rows[0]["TRANCODE"].ToString();
                            txtDesc.Text = productLMTable.Rows[0]["DESCRIPTION"].ToString();
                            ddlCCYID.SelectedValue = productLMTable.Rows[0]["CCYID"].ToString();
                            DataSet dsFee = new SmartPortal.SEMS.Fee().LoadFeeCbb(string.Empty, Utility.KillSqlInjection(ddlCCYID.SelectedValue), ref IPCERRORCODE, ref IPCERRORDESC);
                            if (IPCERRORCODE == "0")
                            {
                                ddlFee.DataSource = dsFee;
                                ddlFee.DataTextField = "FEENAME";
                                ddlFee.DataValueField = "FEEID";
                                ddlFee.DataBind();
                            }
                            else
                            {
                                throw new IPCException(IPCERRORDESC);
                            }
                            DataSet dsFee2 = new SmartPortal.SEMS.Fee().LoadFeeCbb("TRAN", Utility.KillSqlInjection(ddlCCYID.SelectedValue), ref IPCERRORCODE, ref IPCERRORDESC);
                            if (IPCERRORCODE == "0")
                            {
                                ddlTransferFee.DataSource = dsFee2;
                                ddlTransferFee.DataTextField = "FEENAME";
                                ddlTransferFee.DataValueField = "FEEID";
                                ddlTransferFee.DataBind();
                            }
                            else
                            {
                                throw new IPCException(IPCERRORDESC);
                            }
                            //DataSet dsFeeTransferFee = new SmartPortal.SEMS.Fee().GetTransFerFeeByContractNo(Utility.KillSqlInjection(productLMTable.Rows[0]["CONTRACTNO"].ToString()), Utility.KillSqlInjection(ddlCCYID.SelectedValue), Utility.KillSqlInjection(productLMTable.Rows[0]["TRANCODE"].ToString()),ref IPCERRORCODE, ref IPCERRORDESC);
                            //if (IPCERRORCODE == "0")
                            //{
                            //    ddlTransferFee.SelectedValue = dsFeeTransferFee.Tables[0].Rows[0]["FEEID"].ToString();
                            //}
                            //else
                            //{
                            //    throw new IPCException(IPCERRORDESC);
                            //}
                            if (productLMTable.Rows[0]["FEETYPE"].ToString().Equals("TRAN"))
                            {
                                ddlTransferFee.SelectedValue = productLMTable.Rows[0]["FEEID"].ToString();
                                ddlFee.SelectedValue = "";
                            }
                            else
                            {
                                ddlTransferFee.SelectedValue = "";
                                ddlFee.SelectedValue = productLMTable.Rows[0]["FEEID"].ToString();
                            }
                            ddlPayer.SelectedValue = productLMTable.Rows[0]["PAYER"].ToString();

                            //ghi vo session dung in
                            Hashtable hasPrint = new Hashtable();
                            hasPrint.Add("contractno", txtContractNo.Text);
                            hasPrint.Add("trantype", ddlTrans.SelectedItem.ToString());
                            hasPrint.Add("fee", ddlFee.SelectedItem.ToString());
                            hasPrint.Add("desc", txtDesc.Text);
                            hasPrint.Add("ccyid", ddlCCYID.SelectedItem.ToString());
                            hasPrint.Add("payer", ddlPayer.SelectedItem.ToString());
                            hasPrint.Add("datecreated", ((DateTime)(productLMTable.Rows[0]["datecreated"])).ToString("dd/MM/yyyy"));
                            if (productLMTable.Rows[0]["dateapproved"].ToString().Trim() != "")
                            {
                                hasPrint.Add("dateapproved", ((DateTime)(productLMTable.Rows[0]["dateapproved"])).ToString("dd/MM/yyyy"));
                            }
                            else
                            {
                                hasPrint.Add("dateapproved", "");
                            }
                            hasPrint.Add("usercreated", productLMTable.Rows[0]["usercreated"].ToString());
                            hasPrint.Add("userapproved", productLMTable.Rows[0]["userapproved"].ToString());
                            Session["printContractFee"] = hasPrint;
                        }
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    break;
            }
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    btback.Visible = true;
                    btsave.Enabled = false;
                    btnPrint.Visible = true;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtContractNo.Enabled = false;
                    ddlFee.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    btback.Visible = true;
                    btsave.Enabled = true;
                    btnPrint.Visible = false;
                    btnKiemtra.Enabled = false;
                    ddlTransferFee.Enabled = false;
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
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string contractno = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text.Trim());
            string useraction = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString());
            string dateaction = SmartPortal.Common.Utilities.Utility.KillSqlInjection(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            string trancode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim());
            string fee = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlFee.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string payer = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlPayer.SelectedValue.Trim());
            string local = "D";
            string desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.Trim());
            string status = SmartPortal.Common.Utilities.Utility.KillSqlInjection(hdStatus.Value.ToString().Trim());
            string transferfeeid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTransferFee.SelectedValue.ToString().Trim());
            if (contractno == "")
            {
                lblError.Text = Resources.labels.mahopdongkhongrong;
                return;
            }
            if (string.IsNullOrEmpty(ddlFee.SelectedValue) && string.IsNullOrEmpty(ddlTransferFee.SelectedValue))
            {
                lblError.Text = Resources.labels.themphichotientetruoc;
                return;
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:

                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    new SmartPortal.SEMS.Fee().InsertContractFee(contractno, trancode, fee, desc, ccyid, payer, local, useraction, dateaction, "", "", SmartPortal.Constant.IPC.NEW, Session["branch"].ToString(), transferfeeid, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.themphichohopdongthanhcong;
                        pnAdd.Enabled = false;
                        btsave.Enabled = false;
                        return;
                    }
                    else
                    {
                        ErrorCodeModel EM = new ErrorCodeModel();
                        string errorCode = string.Empty;
                        if (IPCERRORDESC == "4018")
                        {
                            errorCode = SmartPortal.Constant.IPC.ERRORCODE.NOTEXISTCONTRACTNO;
                        }
                        else
                        {
                            errorCode = SmartPortal.Constant.IPC.ERRORCODE.EXISTCONTRACTFEE;
                        }
                        EM = new ErrorBLL().Load(Utility.IsInt(errorCode), System.Globalization.CultureInfo.CurrentCulture.ToString());
                        lblError.Text = EM.ErrorDesc;
                        return;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMS00020", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTFEE", "CONTRACTNO='" + contractno + "' AND TRANCODE='" + trancode + "' AND CCYID='" + ccyid + "' AND FEEID='" + fee + "'");
                    }
                    catch
                    {
                    }
                    #endregion
                    new SmartPortal.SEMS.Fee().UpdateContractFee(contractno, trancode, fee, desc, ccyid, payer, local, "", "", useraction, dateaction, SmartPortal.Constant.IPC.PENDING, status, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.suaphichohopdongthanhcong;
                        pnAdd.Enabled = false;
                        btsave.Enabled = false;
                        return;
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
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnKiemtra_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtContractNo.Text.Trim() == "")
            {
                lblError.Text = Resources.labels.mahopdongkhongrong;
                return;
            }
            DataSet dtProLim = new DataSet();
            dtProLim = new SmartPortal.SEMS.Contract().GetContractByCondition(SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text.Trim()), "", "", "", "", "ALL", "ALL", "", "", string.Empty, "ALL", 10, 0, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                lblError.Text = IPCERRORDESC;
            }
            if (dtProLim != null)
            {
                if (dtProLim.Tables.Count != 0)
                {
                    if (dtProLim.Tables[0].Rows.Count == 1)
                    {
                        lblCustName.Text = dtProLim.Tables[0].Rows[0]["FullName"].ToString();
                        lblTenKH.Visible = true;
                        lblCustName.Visible = true;
                        if (dtProLim.Tables[0].Rows[0]["STATUS"].ToString() == "R" ||
                            dtProLim.Tables[0].Rows[0]["STATUS"].ToString() == "D")
                        {
                            ErrorCodeModel EM = new ErrorCodeModel();
                            EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.NOTEXISTCONTRACTNO), System.Globalization.CultureInfo.CurrentCulture.ToString());
                            lblError.Text = EM.ErrorDesc;
                        }
                        else
                        {
                            lblError.Text = "";
                        }
                        if (dtProLim.Tables[0].Rows[0]["CONTRACTTYPE"].ToString() == "AM")
                        {
                            ddlTransferFee.Enabled = false;
                            ddlTransferFee.SelectedValue = "";
                        }
                        else
                        {
                            ddlTransferFee.Enabled = true;
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.khongtimthaydulieu;
                        lblTenKH.Visible = false;
                        lblCustName.Visible = false;
                    }
                }
            }
            else
            {
                lblError.Text = Resources.labels.khongtimthaydulieu;
                lblTenKH.Visible = false;
                lblCustName.Visible = false;
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

    protected void ddlCCYID_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet dsFeeAdd = new SmartPortal.SEMS.Fee().LoadFeeCbb(string.Empty, Utility.KillSqlInjection(ddlCCYID.SelectedValue), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlFee.DataSource = dsFeeAdd;
                ddlFee.DataTextField = "FEENAME";
                ddlFee.DataValueField = "FEEID";
                ddlFee.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            DataSet dsFeeAdd2 = new SmartPortal.SEMS.Fee().LoadFeeCbb("TRAN", Utility.KillSqlInjection(ddlCCYID.SelectedValue), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlTransferFee.DataSource = dsFeeAdd2;
                ddlTransferFee.DataTextField = "FEENAME";
                ddlTransferFee.DataValueField = "FEEID";
                ddlTransferFee.DataBind();
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
    public void ddlTrans_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTrans.SelectedValue.Equals("IB_TRANFERFX") || ddlTrans.SelectedValue.Equals("IBTRANFERFXWL") ||
          ddlTrans.SelectedValue.Equals("IBWLTRANFERFX") || ddlTrans.SelectedValue.Equals("MB_TRFOTHFX") ||
              ddlTrans.SelectedValue.Equals("WL_TRFOTHFX") || ddlTrans.SelectedValue.Equals("MB_TRFOTHFXWL"))
            {

                ddlTransferFee.SelectedValue = "";
                ddlTransferFee.Enabled = false;
            }
            else
            {

                ddlTransferFee.Enabled = true;
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
        txtContractNo.Text = string.Empty;
        ddlTrans.SelectedIndex = 0;
        txtDesc.Text = string.Empty;
        ddlCCYID.SelectedIndex = 0;
        lblTenKH.Visible = false;
        lblCustName.Visible = false;

        DataSet dsFeeAdd = new SmartPortal.SEMS.Fee().LoadFeeCbb(string.Empty, Utility.KillSqlInjection(ddlCCYID.SelectedValue), ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            ddlFee.DataSource = dsFeeAdd;
            ddlFee.DataTextField = "FEENAME";
            ddlFee.DataValueField = "FEEID";
            ddlFee.DataBind();
        }
        else
        {
            throw new IPCException(IPCERRORDESC);
        }
        DataSet dsFeeAdd2 = new SmartPortal.SEMS.Fee().LoadFeeCbb("TRAN", Utility.KillSqlInjection(ddlCCYID.SelectedValue), ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            ddlTransferFee.DataSource = dsFeeAdd2;
            ddlTransferFee.DataTextField = "FEENAME";
            ddlTransferFee.DataValueField = "FEEID";
            ddlTransferFee.DataBind();
        }
        else
        {
            throw new IPCException(IPCERRORDESC);
        }
        ddlFee.SelectedIndex = 0;
        ddlPayer.SelectedIndex = 0;
    }
}
