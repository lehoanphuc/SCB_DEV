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
using log4net.Core;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using Customer = SmartPortal.IB.Customer;

public partial class Widgets_IBCorpUserApproveLimit_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    //public string _IMAGE
    //{
    //    get { return imgLoGo.ImageUrl; }
    //    set { imgLoGo.ImageUrl = value; }
    //}

    //public string _TITLE
    //{
    //    get { return lblTitleProduct.Text; }
    //    set { lblTitleProduct.Text = value; }
    //}


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            btnew.Visible = false;
            txtApprovalLimit.Attributes.Add("onkeyup", "ntt('" + txtApprovalLimit.ClientID + "','" + txtApprovalLimit.ClientID + "',event)");
            txtTotallimit.Attributes.Add("onkeyup", "ntt('" + txtTotallimit.ClientID + "','" + txtTotallimit.ClientID + "',event)");

            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
            if (!IsPostBack)
            {
                //load ten teller

                ddlTeller.DataSource = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userID"].ToString()), "IB", ref IPCERRORCODE, ref IPCERRORDESC);
                ddlTeller.DataTextField = "FULLNAME";
                ddlTeller.DataValueField = "USERID";
                ddlTeller.DataBind();
                //load các giao dịch
                string contractNo = string.Empty;
                DataSet dsContract = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(Session["userID"].ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                DataTable dtContract = new DataTable();
                dtContract = dsContract.Tables[0];
                if (dtContract.Rows.Count != 0)
                {
                    contractNo = dtContract.Rows[0]["CONTRACTNO"].ToString();
                }

                DataSet dsResult = new SmartPortal.SEMS.Product().GetTranNameByContractNo(Utility.KillSqlInjection(contractNo), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                ddlTrans.DataSource = dsResult;
                ddlTrans.DataTextField = "PAGENAME";
                ddlTrans.DataValueField = "TRANCODE";
                ddlTrans.DataBind();

                BindData();
            }
        }
        catch (Exception ex)
        {
        }

    }
    void BindData()
    {
        //DataTable tblRoleDefault = new DataTable();
        try
        {

            switch (ACTION)
            {
                case "add":
                case "ADD":
                    ddlTeller.SelectedValue = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString();
                    ddlTeller.Enabled = false;
                    break;
                default:
                    #region Lấy thông tin san pham

                    DataTable TellerLMTable = new DataTable();
                    TellerLMTable = (new SmartPortal.SEMS.Transactions().GetAllLimitTellerByUID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["trcod"].ToString(), SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cyid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (TellerLMTable.Rows.Count != 0)
                    {
                        ddlTeller.SelectedValue = TellerLMTable.Rows[0]["USERID"].ToString();
                        ddlTrans.SelectedValue = TellerLMTable.Rows[0]["TRANCODE"].ToString();
                        txtTotallimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(TellerLMTable.Rows[0]["TOTALLIMITDAY"].ToString(), TellerLMTable.Rows[0]["CCYID"].ToString());
                        txtApprovalLimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(TellerLMTable.Rows[0]["APPROVALLIMIT"].ToString(), TellerLMTable.Rows[0]["CCYID"].ToString());
                        ddlCCYID.SelectedValue = TellerLMTable.Rows[0]["CCYID"].ToString();
                        txtNumberTran.Text = TellerLMTable.Rows[0]["COUNTLIMIT"].ToString();
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
                case "viewdetail":
                case "DETAILS":
                    ddlTeller.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    txtTotallimit.Enabled = false;
                    btback.Visible = true;
                    btsave.Visible = false;
                    txtApprovalLimit.Enabled = false;
                    txtNumberTran.Enabled = false;
                    break;
                case "edit":
                case "EDIT":
                    ddlTeller.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    txtTotallimit.Enabled = true;
                    btback.Visible = true;
                    btsave.Visible = true;
                    txtApprovalLimit.Enabled = true;
                    txtNumberTran.Enabled = true;
                    break;
            }
            #endregion 

        }
        catch
        {
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string userid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTeller.SelectedValue.Trim());
            string username = HttpContext.Current.Session["userName"].ToString();
            string trancode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string countlimit = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtNumberTran.Text.Trim());
            string approvallimit = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtApprovalLimit.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string totallimit = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtTotallimit.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string desc = string.Empty;
            switch (ACTION)
            {
                case "add":
                case "ADD":

                    new SmartPortal.SEMS.Transactions().InsertUserApproveLimit(userid, trancode, ccyid, approvallimit, totallimit, countlimit, username, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.themhanmucgiaodichthanhcong;
                        btsave.Visible = false;
                        btnew.Visible = true;
                        ddlTrans.Enabled = false;
                        txtApprovalLimit.Enabled = false;
                        txtTotallimit.Enabled = false;
                        txtNumberTran.Enabled = false;
                        ddlCCYID.Enabled = false;
                    }
                    else if (IPCERRORCODE == "7040")
                    {
                        lblError.Text = "Transaction approve limit have already existed.";
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }


                    break;
                case "edit":
                case "EDIT":
                    new SmartPortal.SEMS.Transactions().UpdateUserApproveLimit(userid, trancode, ccyid, approvallimit, totallimit, countlimit, username, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.suahanmucgiaodichthanhcong;
                        btsave.Visible = false;
                    }
                    else
                    {
                        if (IPCERRORDESC == "4014")
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.INVALIDAPPROVELIMIT);

                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                        }
                    }
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        btsave.Visible = true;
        btnew.Visible = false;
        ddlTrans.Enabled = true;
        ddlTrans.SelectedIndex = 0;
        ddlCCYID.SelectedIndex = 0;
        txtApprovalLimit.Text = "";
        txtTotallimit.Text = "";
        txtNumberTran.Text = "";
        ddlTrans.Enabled = true;
        txtApprovalLimit.Enabled = true;
        txtTotallimit.Enabled = true;
        txtNumberTran.Enabled = true;
        ddlCCYID.Enabled = true;
    }
    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

}
