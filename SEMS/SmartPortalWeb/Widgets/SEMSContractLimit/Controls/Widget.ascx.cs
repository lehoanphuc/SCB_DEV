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

using SmartPortal.ExceptionCollection;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using System.Linq;
using System.Web.Configuration;

public partial class Widgets_SEMSContractLimit_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
 
    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }
    public string CONTRACTNO
    {
        get { return ViewState["CONTRACTNO"] != null ? (string)ViewState["CONTRACTNO"] : string.Empty; }
        set { ViewState["CONTRACTNO"] = value; }
    }
    public string STATUS
    {
        get { return ViewState["STATUS"] != null ? (string)ViewState["STATUS"] : string.Empty; }
        set { ViewState["STATUS"] = value; }
    }
    public string IDURL
    {
        get { return ViewState["IDURL"] != null ? (string)ViewState["IDURL"] : string.Empty; }
        set { ViewState["IDURL"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            txtCountLimit.Attributes.Add("onkeyup", "executeComma('" + txtCountLimit.ClientID + "',event)");
            txtTotalLimitDay.Attributes.Add("onkeyup", "executeComma('" + txtTotalLimitDay.ClientID + "',event)");
            txtlimit.Attributes.Add("onkeyup", "executeComma('" + txtlimit.ClientID + "',event)");
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                cbbUnitype.Visible = false;
                //load các giao dịch
                ddlTrans.DataSource = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlTrans.DataTextField = "PAGENAME";
                ddlTrans.DataValueField = "TRANCODE";

                ddlTrans.DataBind();
				

                //load tien te vutran15122014
                ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlCCYID.DataTextField = "CCYID";
                ddlCCYID.DataValueField = "CCYID";
                ddlCCYID.DataBind();
                ddlCCYID.SelectedValue = SmartPortal.Constant.IPC.LAK;

                #region add limit type
                ddlLimitType.Items.Add(new ListItem(Resources.labels.binhthuong, "NOR"));

                ddlLimitType.SelectedValue = "NOR";
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
        //DataTable tblRoleDefault = new DataTable();
        try
        {

            switch (ACTION)
            {

                case IPC.ACTIONPAGE.ADD:
                    btnPrint.Visible = false;
                    break;
                default:
                    IDURL = GetParamsPage(IPC.ID)[0].Trim();
                    #region Lấy thông tin san pham
                    string[] key = IDURL.Split('|');
                    CONTRACTNO = key[0];
                    STATUS = key[3];
                    DataTable productLMTable = new DataTable();
                    productLMTable = (new SmartPortal.SEMS.Transactions().GetContractLimitByCondition(key[0], key[1], key[2], "", "ALL", key[3], "", "", key[4],15,0, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (productLMTable.Rows.Count != 0)
                    {
                        txtcontractno.Text = productLMTable.Rows[0]["CONTRACTNO"].ToString();
                        ddlTrans.SelectedValue = productLMTable.Rows[0]["TRANCODE"].ToString();
                        //txtlimit.Text = FormatNumber(productLMTable.Rows[0]["TRANLIMIT"].ToString());//string.Format("{0:#,###.##}",Double.Parse(productLMTable.Rows[0]["TRANLIMIT"].ToString()));

                        txtTotalLimitDay.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(productLMTable.Rows[1]["TRANLIMIT"].ToString(), productLMTable.Rows[0]["CCYID"].ToString());
                        ddlUnitType.SelectedValue = productLMTable.Rows[0]["UNITTYPE"].ToString();
                        txtlimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(productLMTable.Rows[0]["TRANLIMIT"].ToString(), productLMTable.Rows[0]["CCYID"].ToString());
                        txtCountLimit.Text = productLMTable.Rows[0]["COUNTLIMIT"].ToString();
                        ddlCCYID.SelectedValue = productLMTable.Rows[0]["CCYID"].ToString();
                        ddlLimitType.SelectedValue = productLMTable.Rows[0]["LIMITTYPE"].ToString();

                        //ghi vo session dung in
                        Hashtable hasPrint = new Hashtable();
                        hasPrint.Add("contractno", txtcontractno.Text);
                        hasPrint.Add("trantype", ddlTrans.SelectedItem.ToString());
                        hasPrint.Add("tranlimit", txtlimit.Text);
                        hasPrint.Add("totallimitday", txtTotalLimitDay.Text);
                        hasPrint.Add("countlimit", txtCountLimit.Text);
                        hasPrint.Add("ccyid", ddlCCYID.SelectedItem.ToString());
                        hasPrint.Add("datecreated", ((DateTime)(productLMTable.Rows[0]["datecreated"])).ToString("dd/MM/yyyy"));
                        if (productLMTable.Rows[0]["dateapproved"].ToString() != "")
                        {
                            hasPrint.Add("dateapproved", ((DateTime)(productLMTable.Rows[0]["dateapproved"])).ToString("dd/MM/yyyy"));
                        }
                        else
                        {
                            hasPrint.Add("dateapproved", "");
                        }
                        hasPrint.Add("usercreated", productLMTable.Rows[0]["usercreated"].ToString());
                        hasPrint.Add("userapproved", productLMTable.Rows[0]["userapproved"].ToString());
                        Session["printContractLimit"] = hasPrint;
                    }
                    else
                    {

                    }
                    #endregion
                    break;
            }
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    txtcontractno.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    txtlimit.Enabled = false;
                    txtTotalLimitDay.Enabled = false;
                    txtCountLimit.Enabled = false;
                    btback.Visible = true;
                    btsave.Visible = false;
                    //btnPrint.Visible = true;
                    btnKiemtra.Visible = false;
                    ddlLimitType.Enabled = false;
                    btnClean.Visible = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtcontractno.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    ddlLimitType.Enabled = false;
                    txtlimit.Enabled = true;
                    txtTotalLimitDay.Enabled = true;
                    txtCountLimit.Enabled = true;
                    btback.Visible = true;
                    btsave.Visible = true;
                    btnPrint.Visible = false;
                    btnKiemtra.Visible = false;
                    switch (ddlLimitType.SelectedValue)
                    {
                        case "BAT":
                            txtlimit.Enabled = true;
                            txtTotalLimitDay.Enabled = false;
                            txtCountLimit.Enabled = false;
                            break;
                        default:
                            txtlimit.Enabled = true;
                            txtTotalLimitDay.Enabled = true;
                            txtCountLimit.Enabled = true;
                            break;
                    }
                    break;
            }
            #endregion 

        }
        catch(Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string contractNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtcontractno.Text.Trim()); 
            string trancode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string tranlm = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtlimit.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string countlm = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.IsInt(txtCountLimit.Text.Trim()).ToString());
            string totallmday = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTotalLimitDay.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string useraction = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString());
            string dateaction = SmartPortal.Common.Utilities.Utility.KillSqlInjection(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            string limittype = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlLimitType.SelectedValue.Trim());

            //ktra han muc <= tong han muc
            switch (limittype)
            {
                case "BAT":
                    if (SmartPortal.Common.Utilities.Utility.isDouble(tranlm.Trim(), true) <= 0)
                    {
                        lblError.Text = Resources.labels.batchlimitmustbegreaterthan0;
                        return;
                    }
                    break;
                default:
                    if (SmartPortal.Common.Utilities.Utility.isDouble(tranlm.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(totallmday.Trim(), true))
                    {
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.LIMITERROR);
                    }
                    break;
            }

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:

                        new SmartPortal.SEMS.Transactions().InsertContractLimit(contractNo, trancode, ccyid, tranlm, countlm, string.Empty, "O", useraction, dateaction, SmartPortal.Constant.IPC.NEW, Session["branch"].ToString(), limittype, ref IPCERRORCODE, ref IPCERRORDESC);
                        new SmartPortal.SEMS.Transactions().InsertContractLimit(contractNo, trancode, ccyid, totallmday,countlm , string.Empty, ddlUnitType.SelectedValue, useraction, dateaction, SmartPortal.Constant.IPC.NEW, Session["branch"].ToString(), limittype, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.themhanmuchopdongthanhcong;
                        pnAdd.Enabled = false;
                        btsave.Visible = false;
                        return;

                    }
                    else
                    {
                        if (IPCERRORDESC == "4018")
                        {
                            lblError.Text = Resources.labels.notexistcontractno;
                        }
                        else
                        {
                            lblError.Text = Resources.labels.existcontractlimit;
                        }
                    }


                    break;
                case IPC.ACTIONPAGE.EDIT:

                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMS00024", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTLIMIT", "CONTRACTNO='" + contractNo + "' AND TRANCODE='" + trancode + "' AND CCYID='" + ccyid + "'");
                    }
                    catch
                    {
                    }
                    #endregion

                        new SmartPortal.SEMS.Transactions().UpdateContractLimit(contractNo, trancode, ccyid, tranlm, countlm, string.Empty, "O", limittype, useraction, dateaction, SmartPortal.Constant.IPC.PENDING, STATUS, ref IPCERRORCODE, ref IPCERRORDESC);
                        new SmartPortal.SEMS.Transactions().UpdateContractLimit(contractNo, trancode, ccyid, totallmday, countlm, string.Empty, ddlUnitType.SelectedValue, limittype, useraction, dateaction, SmartPortal.Constant.IPC.PENDING, STATUS, ref IPCERRORCODE, ref IPCERRORDESC);


                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.suahanmuchopdongthanhcong;
                        pnAdd.Enabled = false;
                        btsave.Visible = false;
                        return;
                    }
                    else
                    {
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnClean_Click(object sender, EventArgs e)
    {
        
        btsave.Visible = true;
        HienThi.Visible = false;
        pnAdd.Enabled = true;
        txtlimit.Text = string.Empty;
        txtCountLimit.Text = string.Empty;
        txtTotalLimitDay.Text = string.Empty;
        if (txtcontractno.Enabled)
        {
            txtcontractno.Text = string.Empty;
            ddlTrans.SelectedIndex = 0;
            ddlCCYID.SelectedIndex = 0;
        }
    }
    protected void btnKiemtra_Click(object sender, EventArgs e)
    {
        try
        {
            lblCustName.Text = string.Empty;
            DataSet dtProLim = new DataSet();
            dtProLim = new SmartPortal.SEMS.Contract().GetContractByCondition(txtcontractno.Text.Trim(), "", "", "", "", "ALL", "ALL", "", "", string.Empty, "ALL", ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {

            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            // if (dtProLim.Tables[0].Rows.Count != 0)
            if (dtProLim != null)
            {
                if (dtProLim.Tables.Count != 0)
                {
                    if (dtProLim.Tables[0].Rows.Count == 1)
                    {
                        lblCustName.Text = dtProLim.Tables[0].Rows[0]["FullName"].ToString();
                        HienThi.Visible = true;
                        if (dtProLim.Tables[0].Rows[0]["STATUS"].ToString().Equals(SmartPortal.Constant.IPC.DELETE) ||
                             dtProLim.Tables[0].Rows[0]["STATUS"].ToString().Equals(SmartPortal.Constant.IPC.REJECT))
                        {
                            lblError.Text = Resources.labels.contracthasbeenDBR;
                            btsave.Visible = false;
                        }
                        else
                        {
                            btsave.Visible = true;
                        }
                    }
                    else
                    {
                        //lblError.Text = Resources.labels.khongtimthaynguoidung;
                        lblError.Text = Resources.labels.khongtimthaydulieu;
                        HienThi.Visible = false;
                    }
                }

            }

            else
            {
                //lblError.Text = Resources.labels.khongtimthaynguoidung;
                lblError.Text = Resources.labels.khongtimthaydulieu;
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
    protected void ddlLimitType_IndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);

        switch (ddlLimitType.SelectedValue)
        {
            case "NOR":
                #region NOR

                ddlTrans.DataSource = ds.Tables[0];
                ddlTrans.DataTextField = "PAGENAME";
                ddlTrans.DataValueField = "TRANCODE";
                ddlTrans.DataBind();
                ddlTrans.Enabled = true;

                txtlimit.Enabled = true;
                txtCountLimit.Enabled = true;
                txtTotalLimitDay.Enabled = true;
                ddlCCYID.Enabled = true;
                #endregion
                break;
            case "DEB":
                #region DEB
                ddlTrans.DataSource = ds.Tables[0].Select("IsReceive='Y'").CopyToDataTable();
                ddlTrans.DataTextField = "PAGENAME";
                ddlTrans.DataValueField = "TRANCODE";
                ddlTrans.DataBind();
                ddlTrans.Enabled = true;

                txtlimit.Enabled = true;
                txtCountLimit.Enabled = true;
                txtTotalLimitDay.Enabled = true;
                ddlCCYID.Enabled = true;
                #endregion
                break;
            case "BAT":
                #region BAT
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Select("TranCode='IB000499'").Any() ? ds.Tables[0].Select("TranCode='IB000499'").CopyToDataTable() : null;
                if (!dt.Equals(null))
                {
                    ddlTrans.DataSource = dt;
                    ddlTrans.DataTextField = "PAGENAME";
                    ddlTrans.DataValueField = "TRANCODE";
                    ddlTrans.DataBind();
                    ddlTrans.Enabled = false;
                }



                txtCountLimit.Text = "0";
                txtTotalLimitDay.Text = "0";
                txtlimit.Enabled = true;
                txtCountLimit.Enabled = false;
                txtTotalLimitDay.Enabled = false;
                ddlCCYID.Enabled = false;
                #endregion
                break;
            default:
                break;
        }
    }
}
