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
using OfficeOpenXml.FormulaParsing.Utilities;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.IB;

public partial class Widgets_IBCorpUserLimit_Controls_Widget : WidgetBase
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
            txttotallimit.Attributes.Add("onkeyup", "executeComma('" + txttotallimit.ClientID + "',event)");
            txtlimit.Attributes.Add("onkeyup", "executeComma('" + txtlimit.ClientID + "',event)");
            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
            if (!IsPostBack)
            {
                //load ten teller

                DataTable dtUUID = new DataTable();
                string contractUUID = string.Empty;
                dtUUID = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), "IN", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                if (dtUUID.Rows.Count != 0)
                {
                    contractUUID = dtUUID.Rows[0]["CONTRACTNO"].ToString();
                }

                ddlTeller.DataSource = dtUUID;
                ddlTeller.DataTextField = "FULLNAME";
                ddlTeller.DataValueField = "USERID";
                ddlTeller.DataBind();
                //load các giao dịch

                string contractNo = string.Empty;
                DataSet dsContract = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(Session["userID"].ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);
                DataTable dtContract = new DataTable();
                dtContract = dsContract.Tables[0];
                if (dtContract.Rows.Count != 0)
                {
                    contractNo = dtContract.Rows[0]["CONTRACTNO"].ToString();
                }

                if (IPCERRORCODE != "0" || contractUUID != contractNo)
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);

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
                ddlTrans.Items.Insert(0, new ListItem(IPC.ALL, IPC.ALL));

                //ddlTrans.DataTextField = "PAGENAME";
                //ddlTrans.DataValueField = "TRANCODE";
                //ddlTrans.DataBind();

                BindData();
            }
            string userid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTeller.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());

            for (int i = 1; i < ddlTrans.Items.Count; i++)
            {
                DataTable temp = (DataTable)new SmartPortal.IB.Transactions().CheckExistTrancode(userid, ddlTrans.Items[i].Value, ccyid);
                statusTrans.Add(ddlTrans.Items[i].Value, temp.Rows[0]["ERRORCODE"].ToString());
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBCorpUserLimit_Controls_Widget", "btsave_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    void BindData()
    {
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
                    TellerLMTable = (new SmartPortal.IB.Transactions().GetAllLimitUser(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), "IB", "", SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["trcod"].ToString(), SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cyid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (TellerLMTable.Rows.Count != 0)
                    {
                        ddlTeller.SelectedValue = TellerLMTable.Rows[0]["USERID"].ToString();
                        ddlTrans.SelectedValue = TellerLMTable.Rows[0]["TRANCODE"].ToString();
                        txtlimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(TellerLMTable.Rows[0]["TRANLIMIT"].ToString(), TellerLMTable.Rows[0]["CCYID"].ToString());
                        txttotallimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(TellerLMTable.Rows[0]["TOTALLIMITDAY"].ToString(), TellerLMTable.Rows[0]["CCYID"].ToString());
                        txtcountlimit.Text = SmartPortal.Common.Utilities.Utility.IsInt(TellerLMTable.Rows[0]["COUNTLIMIT"].ToString()).ToString();
                        ddlCCYID.SelectedValue = TellerLMTable.Rows[0]["CCYID"].ToString();
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
                    txtlimit.Enabled = false;
                    txttotallimit.Enabled = false;
                    txtcountlimit.Enabled = false;
                    btback.Visible = true;
                    btsave.Visible = false;
                    break;
                case "edit":
                case "EDIT":
                    ddlTeller.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    txtlimit.Enabled = true;
                    txttotallimit.Enabled = true;
                    txtcountlimit.Enabled = true;
                    btback.Visible = true;
                    btsave.Visible = true;
                    break;
            }
            #endregion

        }
        catch
        {
        }
    }

    protected Hashtable statusTrans = new Hashtable();
    //asdasdasd
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IBCheckPermitPageAction(ACTION)) return;
            string userid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTeller.SelectedValue.Trim());
            string trancode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string limit = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtlimit.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string limittotal = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txttotallimit.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string countlimit = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.IsInt(txtcountlimit.Text.Trim()).ToString());

            string username = HttpContext.Current.Session["userName"].ToString();
            switch (ACTION)
            {
                case "add":
                case "ADD":
                    if(trancode == "ALL")
                    {
                        //CHECK EXIST TRANSACTION TYPE
                        
                        if (statusTrans.ContainsValue("0")) 
                            goto Tranexistwhenaddall;
                        for ( int i = 1; i < ddlTrans.Items.Count; i++)
                        {
                            new SmartPortal.IB.Transactions().InsertCorpUserLimit(userid, ddlTrans.Items[i].Value, ccyid, limit, countlimit, limittotal, username, ref IPCERRORCODE, ref IPCERRORDESC);
                        }
                    }
                    else
                    {
                        new SmartPortal.IB.Transactions().InsertCorpUserLimit(userid, trancode, ccyid, limit, countlimit, limittotal, username, ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    if (IPCERRORCODE == "0")
                    {
                        btsave.Visible = false;
                        ddlTrans.Enabled = false;
                        txtlimit.Enabled = false;
                        txttotallimit.Enabled = false;
                        txtcountlimit.Enabled = false;
                        ddlCCYID.Enabled = false;
                        btnew.Visible = true;
                        lblError.Text = Resources.labels.themlimitthanhcong;
                        return;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
                    break;
                case "edit":
                case "EDIT":
                    new SmartPortal.IB.Transactions().UpdateCorpUserLimit(userid, trancode, ccyid, limit, countlimit, limittotal, username, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        btsave.Visible = false;
                        ddlTrans.Enabled = false;
                        txtlimit.Enabled = false;
                        txttotallimit.Enabled = false;
                        txtcountlimit.Enabled = false;
                        ddlCCYID.Enabled = false;
                        btnew.Visible = true;
                        lblError.Text = Resources.labels.sualimitthanhcong;
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
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBCorpUserLimit_Controls_Widget", "btsave_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    EXIT:
        RedirectBackToMainPage();
    Tranexistwhenaddall:
        lblError.Text = "One of Transactions type has existed. Please choose option below !";
        btnOverwrite.Visible = true;
        btnAddMissTrans.Visible = true;
        btnCancel.Visible = true;

        return;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void btnOverwrite_Click(object sender, EventArgs e)
    {
        try
        {
            string userid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTeller.SelectedValue.Trim());
            string trancode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string limit = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtlimit.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string limittotal = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txttotallimit.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string countlimit = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.IsInt(txtcountlimit.Text.Trim()).ToString());

            string username = HttpContext.Current.Session["userName"].ToString();
            foreach (DictionaryEntry de in statusTrans)
            {
                if (de.Value.ToString() == "1")
                {
                    new SmartPortal.IB.Transactions().InsertCorpUserLimit(userid, de.Key.ToString(), ccyid, limit, countlimit, limittotal, username, ref IPCERRORCODE, ref IPCERRORDESC);
                }
                else if (de.Value.ToString() == "0")
                {
                    new SmartPortal.IB.Transactions().UpdateCorpUserLimit(userid, de.Key.ToString(), ccyid, limit, countlimit, limittotal, username, ref IPCERRORCODE, ref IPCERRORDESC);
                }
            }
            btsave.Visible = false;
            ddlTrans.Enabled = false;
            txtlimit.Enabled = false;
            txttotallimit.Enabled = false;
            txtcountlimit.Enabled = false;
            ddlCCYID.Enabled = false;
            btnew.Visible = true;
            lblError.Text = Resources.labels.themlimitthanhcong;
            btnOverwrite.Visible = false;
            btnAddMissTrans.Visible = false;
            btnCancel.Visible = false;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBCorpUserLimit_Controls_Widget", "btsave_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }

    protected void btnAddMissTrans_Click(object sender, EventArgs e)
    {
        try
        {
            string username = HttpContext.Current.Session["userName"].ToString();
            string userid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTeller.SelectedValue.Trim());
            //     string trancode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string limit = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtlimit.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string limittotal = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txttotallimit.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string countlimit = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.IsInt(txtcountlimit.Text.Trim()).ToString());
            foreach (DictionaryEntry de in statusTrans)
            {
                if (de.Value.ToString() == "1")
                {
                    new SmartPortal.IB.Transactions().InsertCorpUserLimit(userid, de.Key.ToString(), ccyid, limit, countlimit, limittotal, username, ref IPCERRORCODE, ref IPCERRORDESC);
                }
            }
            btsave.Visible = false;
            ddlTrans.Enabled = false;
            txtlimit.Enabled = false;
            txttotallimit.Enabled = false;
            txtcountlimit.Enabled = false;
            ddlCCYID.Enabled = false;
            btnew.Visible = true;
            lblError.Text = Resources.labels.themlimitthanhcong;
            btnOverwrite.Visible = false;
            btnAddMissTrans.Visible = false;
            btnCancel.Visible = false;

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBCorpUserLimit_Controls_Widget", "btsave_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        btsave.Visible = true;
        btnew.Visible = false;
        ddlTrans.Enabled = true;
        ddlTrans.SelectedIndex = 0;
        ddlCCYID.SelectedIndex = 0;
        txtlimit.Enabled = true;
        txtlimit.Text = "";
        txttotallimit.Text = "";
        txtcountlimit.Text = "";
        txttotallimit.Enabled = true;
        txtcountlimit.Enabled = true;
        ddlCCYID.Enabled = true;
    }
    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}
