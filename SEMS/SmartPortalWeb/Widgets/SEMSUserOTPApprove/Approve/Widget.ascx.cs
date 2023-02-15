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

public partial class Widgets_SEMSUserOTPApprove_Delete_Widget : WidgetBase
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
                        lbltitle.Text = Resources.labels.duyetnguoidungdangkysudungotp;
                        lblAppRej.Text = Resources.labels.rejectuserotp;
                        lbresult.Text = Resources.labels.rejectuserotpsuccess;
                        btsaveandcont.Text = Resources.labels.reject;
                        break;
                    case "App":
                        lbltitle.Text = Resources.labels.duyetnguoidungdangkysudungotp;
                        lblAppRej.Text = Resources.labels.approveuserotp;
                        lbresult.Text = Resources.labels.approveuserotpsuccess;
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

            if (Session["_UserIDOTPAPPROVE"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["_UserIDOTPAPPROVE"];
                for (int i = 0; i < lstTran.Count; i++)
                {
                    userid = lstTran[i].Trim();
                    string[] parm = userid.Split('#');
                        //xử lý   
                        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["act"].ToString() == "App")
                        {
                            if (parm[1].ToString()== SmartPortal.Constant.IPC.PENDING)
                            {
                                new SmartPortal.SEMS.OTP().ApproveUserOTP(parm[0].ToString(), SmartPortal.Constant.IPC.ACTIVE, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Session["userName"].ToString(), parm[1].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                            }
                            if (parm[1].ToString() == SmartPortal.Constant.IPC.PENDINGFORDELETE)
                            {
                                lbresult.Text = Resources.labels.approvedeletesuccess;
                                new SmartPortal.SEMS.OTP().ApproveUserOTP(parm[0].ToString(), SmartPortal.Constant.IPC.DELETE, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Session["userName"].ToString(), parm[1].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                            }
                            
                        }
                        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["act"].ToString() == "Rej")
                        {
                            if (parm[1].ToString() == SmartPortal.Constant.IPC.PENDING)
                            {
                                new SmartPortal.SEMS.OTP().ApproveUserOTP(parm[0].ToString(), SmartPortal.Constant.IPC.REJECT, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Session["userName"].ToString(), parm[1].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                            }
                            if (parm[1].ToString() == SmartPortal.Constant.IPC.PENDINGFORDELETE)
                            {
                                lbresult.Text = Resources.labels.rejectdeletesuccess;
                                new SmartPortal.SEMS.OTP().ApproveUserOTP(parm[0].ToString(), SmartPortal.Constant.IPC.ACTIVE, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Session["userName"].ToString(), parm[1].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
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
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=340"));

    }
}


