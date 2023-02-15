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

using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSContractDelete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            //minh add 23.11.2015
            string action = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["action"].ToString();
            if (action == SmartPortal.Constant.IPC.BLOCK)
            {
                btsaveandcont.Text = Resources.labels.conblock;
                lblquestion.Text = Resources.labels.banchacchanmuonkhoahopdong;
                lbltitle.Text = Resources.labels.khoahopdong;
            }
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {

        //minh add 23.11.2015 for add block contract
        string action = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["action"].ToString();

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
                    contractTable = new SmartPortal.SEMS.Contract().UpdateStatusContract(ctr, action, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                    else
                    {
                        try
                        {
                            if (action == "B")
                                SmartPortal.Common.Log.WriteLogDatabase(ctr, "", Request.Url.ToString(), Session["userName"].ToString(),
                                Request.UserHostAddress, "EBA_Contract", "Block contract", "", "Block contract");
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
                    SmartPortal.Common.Log.WriteLog("SEMSCONTRACTDELETE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString() + "'");
                }
                catch
                {
                }
                #endregion

                //contractTable = new SmartPortal.SEMS.Contract().DeleteContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                //if (IPCERRORCODE != "0")
                //{
                //    goto ERROR;
                //}
                //contractTable = new SmartPortal.SEMS.Contract().UpdateStatusContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(),SmartPortal.Constant.IPC.PENDINGFORDELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                contractTable = new SmartPortal.SEMS.Contract().UpdateStatusContract(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), action, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }
                else
                {
                    try
                    {
                        if (action == "B")
                            SmartPortal.Common.Log.WriteLogDatabase(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), "", Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "EBA_Contract", "Block contract", "", "Block contract");
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
            pnRole.Visible = false;
            pnResult.Visible = true;
            lblError.Text = Resources.labels.deletecontractsuccessful;
            btsaveandcont.Visible = false;
            pnRole.Visible = false;

            //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));
        }
        else
        {
            pnRole.Visible = false;
            pnResult.Visible = true;
            lblError.Text = Resources.labels.deletecontractsuccessful;
            btsaveandcont.Enabled = false;
            //Response.Redirect("/?p=141");
        }
        if (action == SmartPortal.Constant.IPC.BLOCK)
        {
            pnRole.Visible = false;
            pnResult.Visible = true;
            lblError.Text = Resources.labels.blockcontractsucessfull;
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnURL"].ToString()));
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=141"));
    }
}


