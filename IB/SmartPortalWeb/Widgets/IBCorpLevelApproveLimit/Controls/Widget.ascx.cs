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


public partial class Widgets_IBCorpUserApproveLimit_Controls_Widget : System.Web.UI.UserControl
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
            txtlimit.Attributes.Add("onkeyup", "executeComma('" + txtlimit.ClientID + "',event)");
            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
           if (!IsPostBack)
            {
                //load ten teller

                ddlTeller.DataSource = new SmartPortal.IB.CorpUser().LoadCorpUserlevelByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"], ref IPCERRORCODE, ref IPCERRORDESC);
                ddlTeller.DataTextField = "DESCRIPTION";
                ddlTeller.DataValueField = "USERLEVEL";
                ddlTeller.DataBind();
                //load các giao dịch
                ddlTrans.DataSource = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
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
                    ddlTeller.SelectedValue = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["lv"].ToString();
                    ddlTeller.Enabled = false;
                    break;
                default:
                    #region Lấy thông tin san pham

                    DataTable TellerLMTable = new DataTable();
                    TellerLMTable =new SmartPortal.SEMS.Transactions().GetLimitLevelByContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["id"].ToString());
                    if (TellerLMTable.Rows.Count != 0)
                    {
                        ddlTeller.SelectedValue = TellerLMTable.Rows[0]["LEVEL"].ToString();
                        ddlTrans.SelectedValue = TellerLMTable.Rows[0]["IPCTRANCODE"].ToString();
                        txtlimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(TellerLMTable.Rows[0]["LIMITAPPROVE"].ToString(), TellerLMTable.Rows[0]["CCYID"].ToString());
                        ddlCCYID.SelectedValue = TellerLMTable.Rows[0]["CCYID"].ToString();
                        txtDesc.Text = TellerLMTable.Rows[0]["DECRIPTION"].ToString();
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
                    ddlTeller.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    txtlimit.Enabled = false;
                    txtDesc.Enabled = false;
                    btback.Visible = true;
                    btsave.Visible = false;
                    break;
                case "edit":
                    ddlTeller.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    txtlimit.Enabled = true;
                    txtDesc.Enabled = true;
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
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string userid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTeller.SelectedValue.Trim());
            string trancode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string limit = SmartPortal.Common.Utilities.Utility.KillSqlInjection( SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtlimit.Text.Trim(),ddlCCYID.SelectedValue.Trim()));
            string desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.Trim());
            

            switch (ACTION)
            {
                case "add":

                    new SmartPortal.SEMS.Transactions().InsertLevelApproveLimit(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"],ddlTeller.SelectedValue, trancode, ccyid, limit, desc, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {

                    }
                    else
                    {
                        if (IPCERRORDESC == SmartPortal.Constant.IPC.ERRORCODE.INVALIDAPPROVELIMIT)
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.INVALIDAPPROVELIMIT);

                        }
                        else
                        {
                            if (IPCERRORDESC == SmartPortal.Constant.IPC.ERRORCODE.INVALIDAPPROVELIMITLEVEL)
                            {
                                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.INVALIDAPPROVELIMIT);
                            }
                            else
                            {
                                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                            }
                        }
                    }


                    break;
                case "edit":
                    new SmartPortal.SEMS.Transactions().UpdateLevelApproveLimit(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"], ddlTeller.SelectedValue, trancode, ccyid, limit, desc, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {

                    }
                    else
                    {
                        if (IPCERRORDESC == SmartPortal.Constant.IPC.ERRORCODE.INVALIDAPPROVELIMIT)
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.INVALIDAPPROVELIMIT);

                        }
                        else
                        {
                            if (IPCERRORDESC == SmartPortal.Constant.IPC.ERRORCODE.INVALIDAPPROVELIMITLEVEL)
                            {
                                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.INVALIDAPPROVELIMIT);
                            }
                            else
                            {
                                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                            }
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
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=3&p=350"));
    }
}
