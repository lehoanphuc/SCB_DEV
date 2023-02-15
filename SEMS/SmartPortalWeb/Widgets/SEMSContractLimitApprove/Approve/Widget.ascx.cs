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

using System.Collections.Generic;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSContractLimitApprove_Approve_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                switch (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["act"].ToString())
                {
                    case "Rej":
                        lbltitle.Text = Resources.labels.duyethanmuchopdong;
                        lblAppRej.Text = Resources.labels.banchacchankhonghanmuchopdong;
                        lbresult.Text = Resources.labels.khongduyethanmuchopdongthanhcong;
                        btsaveandcont.Text = Resources.labels.reject;
                        break;
                    case "App":
                        lbltitle.Text = Resources.labels.duyethanmuchopdong;
                        lblAppRej.Text = Resources.labels.banchacchanmuonduyethanmuchopdong;
                        lbresult.Text = Resources.labels.duyethanmuchopdongthanhcong;
                        btsaveandcont.Text = Resources.labels.duyet;
                        break;
                }
            }
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {

        try
        {
            //approve
            string userid = "";

            if (Session["_CONTRACTLIMITAPPROVE"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["_CONTRACTLIMITAPPROVE"];
                for (int i = 0; i < lstTran.Count; i++)
                {
                    userid = lstTran[i].Trim();
                    string[] parm = userid.Split('#');
                        //xử lý   
                        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["act"].ToString() == "App")
                        {
                            if (parm[3].ToString() == SmartPortal.Constant.IPC.PENDING || parm[3].ToString() == SmartPortal.Constant.IPC.NEW)
                            {
                                #region Ghi log
                                try
                                {
                                    SmartPortal.Common.Log.WriteLog("SEMS00024", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTLIMIT", "CONTRACTNO='" + parm[0] + "' AND TRANCODE='" + parm[1] + "' AND CCYID='" + parm[2] + "'");
                                }
                                catch
                                {
                                }
                                #endregion
                                new SmartPortal.SEMS.Transactions().UpdateContractLimit(parm[0], parm[1], parm[2], "", "", "", parm[4], Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.ACTIVE, parm[3], ref IPCERRORCODE, ref IPCERRORDESC);
                            }
                            if (parm[3].ToString() == SmartPortal.Constant.IPC.PENDINGFORDELETE)
                            {
                                #region Ghi log
                                try
                                {
                                    SmartPortal.Common.Log.WriteLog("SEMS00024", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTLIMIT", "CONTRACTNO='" + parm[0] + "' AND TRANCODE='" + parm[1] + "' AND CCYID='" + parm[2] + "'");
                                }
                                catch
                                {
                                }
                                #endregion
                                new SmartPortal.SEMS.Transactions().UpdateContractLimit(parm[0], parm[1], parm[2], "", "", "", parm[4], Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.DELETE, parm[3], ref IPCERRORCODE, ref IPCERRORDESC);
                            }
                        }
                        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["act"].ToString() == "Rej")
                        {

                            if (parm[3].ToString() == SmartPortal.Constant.IPC.PENDING || parm[3].ToString() == SmartPortal.Constant.IPC.NEW)
                            {
                                #region Ghi log
                                try
                                {
                                    SmartPortal.Common.Log.WriteLog("SEMS00024", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTLIMIT", "CONTRACTNO='" + parm[0] + "' AND TRANCODE='" + parm[1] + "' AND CCYID='" + parm[2] + "'");
                                }
                                catch
                                {
                                }
                                #endregion
                                new SmartPortal.SEMS.Transactions().UpdateContractLimit(parm[0], parm[1], parm[2], "", "", "", parm[4], Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.REJECT, parm[3], ref IPCERRORCODE, ref IPCERRORDESC);
                            }
                            if (parm[3].ToString() == SmartPortal.Constant.IPC.PENDINGFORDELETE)
                            {
                                #region Ghi log
                                try
                                {
                                    SmartPortal.Common.Log.WriteLog("SEMS00024", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_SPCUSTLIMIT", "CONTRACTNO='" + parm[0] + "' AND TRANCODE='" + parm[1] + "' AND CCYID='" + parm[2] + "'");
                                }
                                catch
                                {
                                }
                                #endregion
                                new SmartPortal.SEMS.Transactions().UpdateContractLimit(parm[0], parm[1], parm[2], "", "", "", parm[4], Session["userName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SmartPortal.Constant.IPC.ACTIVE, parm[3], ref IPCERRORCODE, ref IPCERRORDESC);
                            }

                        }

                        if (IPCERRORCODE == "0")
                        {
                            pnRole.Visible = false;
                            pnResult.Visible = true;
                            btsaveandcont.Visible = false;
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);

                        }
                }
            }
            else
            {
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
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=389"));

    }
}


