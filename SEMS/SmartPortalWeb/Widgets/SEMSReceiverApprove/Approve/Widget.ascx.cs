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

public partial class Widgets_SEMSProductDelete_Widget : WidgetBase
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
                        lblLabel.Text = Resources.labels.huynguoithuhuong;
                        lblaction.Text = Resources.labels.banchacchanmuonhuynguoithuhuongkhong;
                        lbresult.Text = Resources.labels.huynguoithuhuongthanhcong;
                        break;
                    case "App":
                        lblLabel.Text = Resources.labels.duyetnguoithuhuong;
                        lblaction.Text = Resources.labels.banchacchanmuonduyetnguoithuhuongkhong;
                        lbresult.Text = Resources.labels.duyetnguoithuhuongthanhcong;
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
            string contractNo = "";

            if (Session["_ReceiverApprove"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["_ReceiverApprove"];
                for (int i = 0; i < lstTran.Count; i++)
                {
                    contractNo = lstTran[i].Trim();
                    //xử lý   
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["act"].ToString() == "App")
                    {
                        new SmartPortal.SEMS.Services().ApproveReveiver(contractNo, SmartPortal.Constant.IPC.ACTIVE, ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["act"].ToString() == "Rej")
                    {
                        new SmartPortal.SEMS.Services().ApproveReveiver(contractNo, SmartPortal.Constant.IPC.REJECT, ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    if (IPCERRORCODE == "0")
                    {
                        pnRole.Visible = false;
                        pnResult.Visible = true;
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
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=317"));

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=317"));
    }
}


