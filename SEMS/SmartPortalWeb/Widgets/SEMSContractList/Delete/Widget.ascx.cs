using System;
using System.Activities.Validation;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSContractDelete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public string ACTION

    {
        get { return ViewState["ACTION"] != null ? (string)ViewState["ACTION"] : string.Empty; }
        set { ViewState["ACTION"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //minh add 23.11.2015
            string action = GetActionPage();
            ACTION = GetParamsPage("ACTION")[0].Trim();
            if (ACTION == SmartPortal.Constant.IPC.BLOCK)
            {
                lblTitle.Text = Resources.labels.khoahopdong;
                btsaveandcont.Text = Resources.labels.conblock;
                lblketqua.Text = Resources.labels.khoahopdong;
            }
            else
            {
                lblTitle.Text = Resources.labels.xoahopdong;
                btsaveandcont.Text = Resources.labels.delete;
                lblketqua.Text = Resources.labels.xoahopdong;
            }
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {

        //minh add 23.11.2015 for add block contract

        DataSet contractTable = new DataSet();
        string SSContractNo = "";
        try
        {
            if (Session["_ContractNo"] != null)
            {
                SSContractNo = Session["_ContractNo"].ToString();
                string[] ctrs = SSContractNo.Split('#');
                foreach (string ctr in ctrs)
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMSCONTRACTDELETE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + ctr + "'");
                    }
                    catch
                    {
                    }
                    #endregion

                    //contractTable = new SmartPortal.SEMS.Contract().DeleteContract(ctr, ref IPCERRORCODE, ref IPCERRORDESC);
                    //if (IPCERRORCODE != "0")
                    //{
                    //    goto ERROR;
                    //}
                    //contractTable = new SmartPortal.SEMS.Contract().UpdateStatusContract(ctr,SmartPortal.Constant.IPC.PENDINGFORDELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                    string action = ACTION == SmartPortal.Constant.IPC.DELETE ? SmartPortal.Constant.IPC.PENDINGFORDELETE : ACTION;
                    contractTable = new SmartPortal.SEMS.Contract().UpdateStatusContract(ctr, action, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                    else
                    {
                        try
                        {
                            DataTable dtContract = (new SmartPortal.SEMS.Contract().GetContractByContractNo(ctr, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

                            string stsContract = dtContract.Rows[0]["STATUS"].ToString();
                            string userID = dtContract.Rows[0]["USERID"].ToString();
                            if (ACTION == "B")
                            {
                                if (stsContract.Equals(SmartPortal.Constant.IPC.DELETE))
                                {
                                    SmartPortal.Common.Log.WriteLogDatabase(ctr, userID, Request.Url.ToString(), Session["userName"].ToString(),
                                    Request.UserHostAddress, "EBA_Contract", "Block contract", "", "Deleted", stsContract);
                                    SmartPortal.Common.Log.WriteLogDatabase(ctr, userID, Request.Url.ToString(), Session["userName"].ToString(),
                                    Request.UserHostAddress, "EBA_Users", "Delete User", "", "Deleted", stsContract);
                                }
                                else
                                {
                                    SmartPortal.Common.Log.WriteLogDatabase(ctr, userID, Request.Url.ToString(), Session["userName"].ToString(),
                                     Request.UserHostAddress, "EBA_Contract", "Block contract", "", "Block", stsContract);
                                }
                            }

                            if (ACTION == SmartPortal.Constant.IPC.DELETE)
                            {
                                if (stsContract.Equals(SmartPortal.Constant.IPC.DELETE))
                                {
                                    SmartPortal.Common.Log.WriteLogDatabase(ctr, userID, Request.Url.ToString(), Session["userName"].ToString(),
                                    Request.UserHostAddress, "EBA_Contract", "Delete Contract", "", "Deleted");
                                    SmartPortal.Common.Log.WriteLogDatabase(ctr, userID, Request.Url.ToString(), Session["userName"].ToString(),
                                    Request.UserHostAddress, "EBA_Users", "Delete User", "", "Deleted", stsContract);
                                }

                            }

                        }
                        catch { }
                    }
                }
                Session["_ContractNo"] = null;
            }
            else
            {
                #region Ghi log
                try
                {
                    SmartPortal.Common.Log.WriteLog("SEMSCONTRACTDELETE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + GetParamsPage(IPC.ID)[0].Trim() + "'");
                }
                catch
                {
                }
                #endregion

                //contractTable = new SmartPortal.SEMS.Contract().DeleteContract( GetParamsPage(IPC.ID)[0].Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                //if (IPCERRORCODE != "0")
                //{
                //    goto ERROR;
                //}
                //contractTable = new SmartPortal.SEMS.Contract().UpdateStatusContract( GetParamsPage(IPC.ID)[0].Trim(),SmartPortal.Constant.IPC.PENDINGFORDELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                string action = ACTION == SmartPortal.Constant.IPC.DELETE ? SmartPortal.Constant.IPC.PENDINGFORDELETE : ACTION;
                contractTable = new SmartPortal.SEMS.Contract().UpdateStatusContract(GetParamsPage(IPC.ID)[0].Trim(), action, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }
                else
                {
                    try
                    {
                        DataTable dtContract = new SmartPortal.SEMS.Contract().GetContractByContractNo(GetParamsPage(IPC.ID)[0].Trim(), ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                        string stsContract = dtContract.Rows[0]["STATUS"].ToString();
                        string userID = dtContract.Rows[0]["USERID"].ToString();
                        if (ACTION == "B")
                        {
                            if (stsContract.Equals(SmartPortal.Constant.IPC.DELETE))
                            {
                                SmartPortal.Common.Log.WriteLogDatabase(stsContract, userID, Request.Url.ToString(), Session["userName"].ToString(),
                                Request.UserHostAddress, "EBA_Contract", "Block contract", "", "Deleted", stsContract);
                                SmartPortal.Common.Log.WriteLogDatabase(GetParamsPage(IPC.ID)[0].Trim(), userID, Request.Url.ToString(), Session["userName"].ToString(),
                                Request.UserHostAddress, "EBA_Users", "Delete User", "", "Deleted", stsContract);
                            }
                            else
                            {
                                SmartPortal.Common.Log.WriteLogDatabase(stsContract, userID, Request.Url.ToString(), Session["userName"].ToString(),
                                 Request.UserHostAddress, "EBA_Contract", "Block contract", "", "Block", stsContract);
                            }
                        }
                        if (ACTION == SmartPortal.Constant.IPC.DELETE)
                        {
                            if (stsContract.Equals(SmartPortal.Constant.IPC.DELETE))
                            {
                                SmartPortal.Common.Log.WriteLogDatabase(GetParamsPage(IPC.ID)[0].Trim(), userID, Request.Url.ToString(), Session["userName"].ToString(),
                                Request.UserHostAddress, "EBA_Contract", "Delete Contract", "", "Delete Contract");

                                SmartPortal.Common.Log.WriteLogDatabase(GetParamsPage(IPC.ID)[0].Trim(), userID, Request.Url.ToString(), Session["userName"].ToString(),
                          Request.UserHostAddress, "EBA_Users", "Delete User", "", "Deleted", stsContract);
                            }

                        }
                    }
                    catch { }
                }
            }
            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractDelete_Widget", "btsaveandcont_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractDelete_Widget", "btsaveandcont_Click", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"] != null)
        {
            lblketqua.Text = Resources.labels.deletecontractsuccessful;
            btsaveandcont.Visible = false;

            //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));
        }
        else
        {

            lblketqua.Text = Resources.labels.deletecontractsuccessful;
            btsaveandcont.Enabled = false;
            //Response.Redirect("/?p=141");
        }
        if (ACTION == SmartPortal.Constant.IPC.BLOCK)
        {
            lblketqua.Text = Resources.labels.blockcontractsucessfull;
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}


