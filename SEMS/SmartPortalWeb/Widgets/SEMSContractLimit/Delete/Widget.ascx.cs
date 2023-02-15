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
using DayPilot.Web.Ui;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSContractLimit_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public string IDURL
    {
        get { return ViewState["IDURL"] != null ? (string)ViewState["IDURL"] : string.Empty; }
        set { ViewState["IDURL"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!IsPostBack)
            {
                IDURL = GetParamsPage(IPC.ID)[0].Trim();
            }

        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        DataSet ProductLMTable = new DataSet();
        string SSProTranCCYID = "";
        try
        {
            if (Session["_CtractTranCCYIDStatus"] != null)
            {
                SSProTranCCYID = Session["_CtractTranCCYIDStatus"].ToString();
                string[] Alls = SSProTranCCYID.Split('#');
                foreach (string All in Alls)
                {
                    string[] pros = All.Split('|');

                    if (pros[3].ToString() == SmartPortal.Constant.IPC.PENDING || pros[3].ToString() == SmartPortal.Constant.IPC.NEW)
                    {
                        #region Ghi log
                        try
                        {
                            SmartPortal.Common.Log.WriteLog("SEMS00025", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTLIMIT", "CONTRACTNO='" + pros[0] + "' AND TRANCODE='" + pros[1] + "' AND CCYID='" + pros[2] + "'");
                        }
                        catch
                        {
                        }
                        #endregion

                        new SmartPortal.SEMS.Transactions().DeleteContractLimit(pros[0], pros[1], pros[4], pros[2], pros[3], ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    if (pros[3].ToString() == SmartPortal.Constant.IPC.ACTIVE)
                    {
                        #region Ghi log
                        try
                        {
                            SmartPortal.Common.Log.WriteLog("SEMS00025", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTLIMIT", "CONTRACTNO='" + pros[0] + "' AND TRANCODE='" + pros[1] + "' AND CCYID='" + pros[2] + "'");
                        }
                        catch
                        {
                        }
                        #endregion

                        new SmartPortal.SEMS.Transactions().UpdateContractLimit(pros[0], pros[1], pros[2], "","", "", "", pros[4], Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.PENDINGFORDELETE, pros[3], ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }

                }
                Session["_CtractTranCCYIDStatus"] = null;
                //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));

            }
            else
            {
                string[] key = IDURL.Split('|');
                string limitType = key[4];
                string status = key[3];
                string ccyid = key[2];
                string trancode= key[1];
                string contractNo = key[0];
                if (status == SmartPortal.Constant.IPC.PENDING || status == SmartPortal.Constant.IPC.NEW)
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMS00025", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTLIMIT",
                            "CONTRACTNO='" + contractNo + "' AND TRANCODE='" + trancode + "' AND CCYID='" + ccyid + "'");
                    }
                    catch
                    {
                    }
                    #endregion

                    new SmartPortal.SEMS.Transactions().DeleteContractLimit(contractNo,trancode,limitType, ccyid,status, ref IPCERRORCODE, ref IPCERRORDESC);
                }
                if (key[3] == SmartPortal.Constant.IPC.ACTIVE)
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("SEMS00025", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTLIMIT",
                             "CONTRACTNO='" + contractNo + "' AND TRANCODE='" + trancode + "' AND CCYID='" + ccyid + "'");
                    }
                    catch
                    {
                    }
                    #endregion

                    new SmartPortal.SEMS.Transactions().UpdateContractLimit(contractNo, trancode, ccyid, "", "", "", "",limitType, Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.PENDINGFORDELETE, status, ref IPCERRORCODE, ref IPCERRORDESC);
                }
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
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

        if (IPCERRORCODE == "0")
        {
            lblKq.Text = Resources.labels.xoathanhcong;
            btsaveandcont.Visible = false;
            //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}


